Imports System
Imports System.Windows.Forms
Imports System.DateTime
Imports System.Collections.Generic
Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.HMI

    Public Class FacilityController

        'Ce contrôleur permet de générer la séquence de arming lorsque le bouton du test cell arming est activé.

        'On peut spécifier un délais d'attente maximum par element de facility. Si on met le délais a 0, l'element du facility ne sera pas dans la sequence de arming mais
        'son feedback sera monitoré quand même en cas d'incident. L'utilisateur recevra alors un message et l'element fautif sera desactivé.
        'Cela peut être utile dans le cas qu'on a des scavenges pumps qu'on active en même temps que le power pack et non séquentiellement mais qu"on vet monitorer

        'La sequence de arming informe l'utilisateur pour chaque element en train d'être activer et informera l'utilisateur lorsqu'elle sera complété.
        'Si un item cause problème durant la phase d'arming, l'arming sera interrompu et tout sera désactivé.

        'Si on détecte la perte d'un auxiliaire lorsque le Test Cell Arming button est activer ou bien que le bouton de cet élément est activer dans le FCS screen
        'On informe l'utilisateur et on désactive l'element fautif si "DeactivateFacilityItemOnAuxiliaryFailure" est True

        'Si on détecte un overload, peu importe si l'element est activé ou pas, on informe l'utilisateur et on désactive l'element fautif si "DeactivateFacilityItemonFault"
        'est true. 'On peut également spécifier "Nothing" comme Overload dans le cas qu'on veut inclure un element dans la séquence d'arming qui n'a pas d'overload.

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _Sequence As CEL.KineticaRT.Sequence.Sequence
        Private _Test_Cell_ArmingChannel As IChannel
        Private _DeactivateFacilityItemOnAuxiliaryFailure As Boolean
        Private _DeactivateFacilityItemOnFault As Boolean
        Private _Test_Cell_ArmingCustomMessage As String

        'Variables les elements de facility pour le Test Cell Arming
        Private FacilityItems As New List(Of FacilityItemSignals)
        Private _FacilityItemsSignalChannels As IChannel()
        Private _FacilityItemsFeedbackChannels As IChannel()
        Private _FacilityItemsFaultChannels As IChannel()

        'Variables utiliser lors de l'initialisation des canaux
        Private _i As Integer

        'Variables pour la sequence d'armement
        Private _ts_Arming_Timer As System.TimeSpan
        Private _ArmingElapsedTime As System.DateTime
        Private _ArmingTimeReference As Double = 0 'Base de temps, initialiser a 0 pour chaque facility item
        Private _ArmingCompleted As Boolean = False 'Utilise pour lorsque tous les items ont ete processe sans obtenir un Arming Failed
        Private _CurrentFacilityItemProcessed As Boolean = False '
        Private _InitializingFacilityItem As Boolean = True
        Private _NumberOfFacilityItems As Integer = 0
        Private _CurrentItemIndex As Integer = 0

        Public Class FacilityItemsList
            Public Signal As String
            Public Feedback As String
            Public FeedbackDelay As Double
            Public Fault As String

            Public Sub New(ByVal SignalChannel As String, ByVal FeedbackChannel As String, ByVal FeedbackDelayValue As Double, ByVal FaultChannel As String)
                Signal = SignalChannel
                Feedback = FeedbackChannel
                FeedbackDelay = FeedbackDelayValue
                Fault = FaultChannel
            End Sub
        End Class

        Private Class FacilityItemSignals
            Public Signal As IChannel
            Public Feedback As IChannel
            Public FeedbackDelay As Double
            Public Fault As IChannel

            Public Sub New(ByRef SignalChannel As IChannel, ByRef FeedbackChannel As IChannel, ByRef FeedbackDelayValue As Double, ByRef FaultChannel As IChannel)
                Signal = SignalChannel
                Feedback = FeedbackChannel
                FeedbackDelay = FeedbackDelayValue
                Fault = FaultChannel
            End Sub
        End Class


        Public Sub New(ByRef Sequence As CEL.KineticaRT.Sequence.Sequence, _
                        ByVal Test_Cell_ArmingChannelName As String, _
                        ByVal FacilityControllerItems As List(Of FacilityItemsList), _
                        ByVal DeactivateFacilityItemOnAuxiliaryFailure As Boolean, _
                        ByVal DeactivateFacilityItemOnFault As Boolean, _
                        ByVal Test_Cell_ArmingCustomMessage As String)

            _Sequence = Sequence
            _Sequence.InitializeChannel(_Test_Cell_ArmingChannel, Test_Cell_ArmingChannelName)
            If _Test_Cell_ArmingChannel Is Nothing Then
                clsTopMostMessageBox.Show("Test_Cell_ArmingChannelName does not correspond to a valid signal name", "Test Cell Arming Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            ReDim Preserve _FacilityItemsSignalChannels(FacilityControllerItems.Count - 1)
            ReDim Preserve _FacilityItemsFeedbackChannels(FacilityControllerItems.Count - 1)
            ReDim Preserve _FacilityItemsFaultChannels(FacilityControllerItems.Count - 1)

            For _i = 0 To (FacilityControllerItems.Count - 1)

                _Sequence.InitializeChannel(_FacilityItemsSignalChannels(_i), FacilityControllerItems.Item(_i).Signal)
                _Sequence.InitializeChannel(_FacilityItemsFeedbackChannels(_i), FacilityControllerItems.Item(_i).Feedback)

                'On verifie si on a un signal d'overload pour l'item de facility en cours
                If FacilityControllerItems.Item(_i).Fault <> Nothing Then
                    _Sequence.InitializeChannel(_FacilityItemsFaultChannels(_i), FacilityControllerItems.Item(_i).Fault)

                    If _FacilityItemsFaultChannels(_i) Is Nothing Then
                        clsTopMostMessageBox.Show(FacilityControllerItems.Item(_i).Fault & " does not correspond to a valid signal name", "Test Cell Arming Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                End If

                If Not _FacilityItemsSignalChannels(_i) Is Nothing Then
                    If Not _FacilityItemsFeedbackChannels(_i) Is Nothing Then
                        FacilityItems.Add(New FacilityItemSignals(_FacilityItemsSignalChannels(_i), _FacilityItemsFeedbackChannels(_i), FacilityControllerItems.Item(_i).FeedbackDelay, _FacilityItemsFaultChannels(_i)))
                        _NumberOfFacilityItems += 1
                    Else
                        clsTopMostMessageBox.Show(FacilityControllerItems.Item(_i).Feedback & " does not correspond to a valid signal name", "Test Cell Arming Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Else
                    clsTopMostMessageBox.Show(FacilityControllerItems.Item(_i).Signal & " does not correspond to a valid signal name", "Test Cell Arming Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Next

            _DeactivateFacilityItemOnAuxiliaryFailure = DeactivateFacilityItemOnAuxiliaryFailure
            _DeactivateFacilityItemOnFault = DeactivateFacilityItemOnFault
            _Test_Cell_ArmingCustomMessage = Test_Cell_ArmingCustomMessage

            Me.AddHandlersFacilityController()

        End Sub

        Private Sub AddHandlersFacilityController()
            AddHandler _Test_Cell_ArmingChannel.ValueChanged, AddressOf OnTestCellDeArming

            For Each FacilityItem As FacilityItemSignals In FacilityItems
                AddHandler FacilityItem.Feedback.ValueChanged, AddressOf OnFacilityItemFeedbackFailed
                If Not FacilityItem.Fault Is Nothing Then
                    AddHandler FacilityItem.Fault.ValueChanged, AddressOf OnFacilityItemFaultDetected
                End If
            Next

        End Sub

        Private Sub RemoveHandlersFacilityController()
            If Not _Test_Cell_ArmingChannel Is Nothing Then
                RemoveHandler _Test_Cell_ArmingChannel.ValueChanged, AddressOf OnTestCellDeArming
            End If

            For Each FacilityItem As FacilityItemSignals In FacilityItems
                If Not _Test_Cell_ArmingChannel Is Nothing Then
                    RemoveHandler FacilityItem.Feedback.ValueChanged, AddressOf OnFacilityItemFeedbackFailed
                    If Not FacilityItem.Fault Is Nothing Then
                        RemoveHandler FacilityItem.Fault.ValueChanged, AddressOf OnFacilityItemFaultDetected
                    End If
                End If
            Next

        End Sub

        '***********************
        '* OnTestCellDeArming() *
        '***********************
        Private Sub OnTestCellDeArming(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If CELProject.IsFalse(newVal) Then
                    'Si on de-pese sur le bouton Test Cell Arming : On ferme tout les elements qui faisaient partit du arming
                    For Each FacilityItem As FacilityItemSignals In FacilityItems
                        If FacilityItem.FeedbackDelay > 0 Then
                            FacilityItem.Signal.Value = Values.False
                        End If
                    Next

                    'On Reset les conditions pour le Test Cell Arming
                    _InitializingFacilityItem = True
                    _CurrentItemIndex = 0
                    _ArmingCompleted = False

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnTestCellDeArming: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '***********************
        '* OnTestCellArming() *
        '***********************
        Public Sub TestCellArming()

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If
                'Si on pese sur le bouton Test Cell Arming : On active chaque item du facility sequentiellement dans les delais donnes
                If CELProject.IsTrue(_Test_Cell_ArmingChannel.Value) Then

                    'Si le Arming est Completed, il n'y a aucune action a prendre lorsque le Test Cell Arming BTN est True
                    If _ArmingCompleted = False Then

                        'Si c'est un nouvel item du facility qui est traité lorsqu'on passe dans la fonction, il y a une phase d'initialisation
                        If _InitializingFacilityItem = True Then

                            If FacilityItems.Item(_CurrentItemIndex).FeedbackDelay > 0 Then

                                'On informe l'utilisateur de l'item initialisé en cours durant le test cell arming
                                CELDAS.NotifyIcon.ShowBalloonTip(30000, CELProject.TechnologyName, "Test Cell Arming in Progress : Please Wait up to " & FacilityItems.Item(_CurrentItemIndex).FeedbackDelay & " seconds as the " _
                                                                 & FacilityItems.Item(_CurrentItemIndex).Signal.Name.ToString.Replace("_", " ") & " is being activated", ToolTipIcon.Info)

                                'Le Timer est réinitialisé
                                _ts_Arming_Timer = New System.TimeSpan(0, 0, 0, 0, 0)
                                _ArmingElapsedTime = Now

                                'Premiere Etape : le signal pour activer l'item est envoye.
                                FacilityItems.Item(_CurrentItemIndex).Signal.Value = Values.True

                                'On désactive la phase d'initialisation tant qu'il n'y a pas un nouvel item à traiter
                                _InitializingFacilityItem = False

                            Else

                                'On a un item de facility avec un feedback = 0 : il est exclus de la séquence de arming.
                                _ArmingTimeReference = 0.0
                                _CurrentFacilityItemProcessed = True

                            End If

                        End If

                        'Pour chaque item, on a un timer qui a ete initialiser : Pour chaque fois, on verifie combien de temps est ecouler entre chaque fois qu'on passe dans la fonction
                        'en considérant le fait qu'il y a un 250ms aussi à cause de la thread
                        'Le temps permet de savoir dans quel état on est : on attend encore après le feedback, on a eu le feedback avant le temps max ou a dépasser le temps avant d'avoir le feedback

                        _ts_Arming_Timer = _ts_Arming_Timer.Add(Now.Subtract(_ArmingElapsedTime))
                        _ArmingTimeReference = _ts_Arming_Timer.TotalSeconds
                        _ArmingElapsedTime = Now

                        'Deuxième étape : On traite l'item de facility
                        If _CurrentFacilityItemProcessed = False Then


                            'Cas1 : Nous attendons encore apres le feedback sans depasser le delais
                            If (_ArmingTimeReference < FacilityItems.Item(_CurrentItemIndex).FeedbackDelay) And (FacilityItems.Item(_CurrentItemIndex).Feedback.Value = Values.False) Then
                                'On continu a attendre...

                                'Cas2 : Nous obtenons le feedback sans depasser le delais
                            ElseIf (_ArmingTimeReference < FacilityItems.Item(_CurrentItemIndex).FeedbackDelay) And FacilityItems.Item(_CurrentItemIndex).Feedback.Value = Values.True Then

                                'Le feedback a été obtenu avant la fin du délais, il reste a attendre que le delais soit écoulé
                                _CurrentFacilityItemProcessed = True

                                'Cas3 : Le temps pour obtenir le feedback a ete depasse. Test Cell Arming a echoue. On arrete tout et on informe l'utilisateur du probleme specifique.
                            ElseIf _ArmingTimeReference > FacilityItems.Item(_CurrentItemIndex).FeedbackDelay Then
                                'Utilisateur informé de l'item qui a failed
                                CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, FacilityItems.Item(_CurrentItemIndex).Feedback.Name.ToString.Replace("_", " ") & " failed to received electrical feedback in specified time", ToolTipIcon.Info)
                                'On désactive la séquence de test cell arming : voir le handler sur le bouton dans le controlleur pour la réinitialisation de variables
                                _Test_Cell_ArmingChannel.Value = Values.False
                            End If
                        End If

                        'Si l'item a ete processer :
                        If _CurrentFacilityItemProcessed = True Then

                            'Si on a atteint le delais entre les items du facility a activer
                            If _ArmingTimeReference >= FacilityItems.Item(_CurrentItemIndex).FeedbackDelay Or FacilityItems.Item(_CurrentItemIndex).FeedbackDelay = 0 Then

                                'Préparation des etats pour le prochain item et la reference de temps est remise à 0
                                _ArmingTimeReference = 0.0
                                _CurrentItemIndex += 1
                                _InitializingFacilityItem = True
                                _CurrentFacilityItemProcessed = False

                                'Si on a tout processer
                                If _CurrentItemIndex = _NumberOfFacilityItems Then
                                    'On informe l'utilisateur
                                    CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Test Cell Arming is completed" & Environment.NewLine & _Test_Cell_ArmingCustomMessage, ToolTipIcon.Info)
                                    'Variable qui empêche de passer a travers le code lorsque la séquence est complétée
                                    _ArmingCompleted = True
                                End If
                            Else
                                'Si Pas encore atteint le delais d'arming entre les items, on continu l'attente
                            End If
                        End If

                    Else
                        'Si Le bouton Test Cell Arming est True et que _ArmingCompleted = True, on ne fait rien

                    End If

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("TestCellArming: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnFacilityItemFeedbackFailed(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If
                'Si le bouton "Test Cell Arming" ou celui de l'item du facility est activé et qu'on perd ce feedback, on verifie a quel item du facility il etait associe et on peut desactiver l'item. On informe l'utilisateur.
                'Si on on perd un feedback et qu'il est du a cause qu'on a desactive volontairement un item du facility, aucune action n'est prise. Considerer comme normal.
                If CELProject.IsFalse(newVal) Then
                    For Each FacilityItem As FacilityItemSignals In FacilityItems
                        If _Test_Cell_ArmingChannel.Value = Values.True Or FacilityItem.Signal.Value = Values.True Then

                            Dim AuxiliaryChannelDescription As String = FacilityItem.Feedback.Name.ToString.Replace("_IND", "")
                            Dim AuxiliaryItemDescription As String = FacilityItem.Feedback.Name.ToString.Replace("_AUX_IND", "")

                            If sender.Name = FacilityItem.Feedback.Name.ToString Then
                                If _DeactivateFacilityItemOnAuxiliaryFailure = False Then
                                    CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, AuxiliaryChannelDescription.Replace("_", " ") & " has been lost.", ToolTipIcon.Info)
                                ElseIf _DeactivateFacilityItemOnAuxiliaryFailure = True Then
                                    FacilityItem.Signal.Value = Values.False
                                    CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, AuxiliaryChannelDescription.Replace("_", " ") & " has been lost. " & AuxiliaryItemDescription.Replace("_", " ") & " has been disabled for safety precaution.", ToolTipIcon.Info)
                                End If
                            End If
                        End If
                    Next

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnFacilityItemFeedbackFailed: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnFacilityItemFaultDetected(ByVal sender As IChannel, ByVal newVal As Value)

            'Creation d'un canal temporaire car si j'ai un NOTHING dans le For Each lors de la comparaison a cause qu'il n'y a pas de overload pour les louvers, il y a une exception
            Dim OverloadChannel As IChannel

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If
                'Si on détecte une faute dun item facility, que l'auxiliraire soit activé ou pas, on verifie quel item en question est fautif.
                If CELProject.IsTrue(newVal) Then
                    For Each FacilityItem As FacilityItemSignals In FacilityItems
                        If Not FacilityItem.Fault Is Nothing Then
                            OverloadChannel = FacilityItem.Fault

                            Dim OverloadChannelDescription As String = OverloadChannel.Name.ToString.Replace("_IND", "")
                            Dim OverloadItemDescription As String = OverloadChannel.Name.ToString.Replace("_OVERLOAD_IND", "")

                            If sender.Name = OverloadChannel.Name.ToString Then
                                If _DeactivateFacilityItemOnFault = False Then
                                    CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, OverloadChannelDescription.Replace("_", " ") & " has been detected.", ToolTipIcon.Info)
                                ElseIf _DeactivateFacilityItemOnFault = True Then
                                    FacilityItem.Signal.Value = Values.False
                                    CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, OverloadChannelDescription.Replace("_", " ") & " has been detected. " & OverloadItemDescription.Replace("_", " ") & " has been disabled for safety precaution.", ToolTipIcon.Info)
                                End If
                            End If
                        End If


                    Next

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnFacilityItemFeedbackFailed: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called
                    RemoveHandlersFacilityController()
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class



End Namespace
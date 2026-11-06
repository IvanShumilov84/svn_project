Imports System
Imports System.Windows.Forms
Imports System.DateTime
Imports Microsoft.VisualBasic

Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.HMI

    Public Class TimerController 'Used for time measurement

        'Pour utiliser ce contrôleur, il faut créé une expression booleenne pour le démarrer le timer et l'arrêter. Ensuite, il faut créé les channels de type boolean latch pour les deux conditions
        'Ce sont les channels de type latch qui sont à l'initialisation du controlleur. 

        'Le channel de RESET permet de préparer le contrôleur à refaire le timer.

        'Il y a aussi un channel pour une valeur seuil de temps maximale : lorsque atteinte, enclanche un channel qui permet d'effectuer de la logique dans le HMI

        'L'option IsPersistent est pour le cas que la valeur a été sauvegardé dans un ficher et lorsqu'on réouvre le DAS, on ne veut pas réinitialisé le channel du résultat du timer.

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _Sequence As CEL.KineticaRT.Sequence.Sequence
        Private _StartTimerTriggerChannel As IChannel
        Private _StopTimerTriggerChannel As IChannel
        Private _ResetTimerTriggerChannel As IChannel
        Private _TimerResultChannel As INumericChannel
        Private _TimerResultTextChannel As ITextChannel
        Private _TimerMaxValue As Double
        Private _TimerResultInitialValue As Double = 0.0
        Private _TimerMaxValueReachedChannel As IChannel
        Private _IsPersistent As Boolean

        Private _ts_Timer As System.TimeSpan
        Private _ElapsedTime As System.DateTime
        Private _TimeReference As Double = 0 'Base de temps,
        Private _TimerCompleted As Boolean = False
        Private _MessageToUser As Boolean = False
        Private _InitializingTimer As Boolean = True
        Private _MessageToDisplay As String = ""


        Public Sub New(ByRef Sequence As CEL.KineticaRT.Sequence.Sequence, _
                       Optional ByVal StartTimerTriggerChannelName As String = "TIMER_START_CONDITION", _
                       Optional ByVal StopTimerTriggerChannelName As String = "TIMER_STOP_CONDITION", _
                       Optional ByVal ResetTimerTriggerChannelName As String = "TIMER_RESET_CONDITION", _
                       Optional ByVal TimerResultChannelName As String = "TIMER_IND", _
                       Optional ByVal TimerResultTextChannelName As String = "TIMER_TEXT_IND", _
                       Optional ByVal TimerMaxValue As Double = 60, _
                       Optional ByVal TimerMaxValueReachedChannelName As String = "TIMER_MAX_VALUE_REACHED", _
                       Optional ByVal IsPersistent As Boolean = False, _
                       Optional ByVal MessageToDisplay As String = "")

            _Sequence = Sequence
            _Sequence.InitializeChannel(_StartTimerTriggerChannel, StartTimerTriggerChannelName)
            If Not IsNothing(StopTimerTriggerChannelName) Then
                _Sequence.InitializeChannel(_StopTimerTriggerChannel, StopTimerTriggerChannelName)
            End If

            _Sequence.InitializeChannel(_ResetTimerTriggerChannel, ResetTimerTriggerChannelName)


            Me.AddHandlersTimerController()

            Dim TempChannel As IChannel = Nothing

            If Not IsNothing(TimerResultChannelName) Then
                _Sequence.InitializeChannel(TempChannel, TimerResultChannelName)
                _TimerResultChannel = CType(TempChannel, INumericChannel)
            End If

            If Not IsNothing(TimerResultTextChannelName) Then
                _Sequence.InitializeChannel(TempChannel, TimerResultTextChannelName)
                _TimerResultTextChannel = CType(TempChannel, ITextChannel)
            End If

            _IsPersistent = IsPersistent

            'Initialize text timer on new
            If Not IsNothing(_TimerResultTextChannel) And _IsPersistent = True Then
                _TimerResultTextChannel.Value = New Global.KineticaRT.Values.Text(Format(DateAdd("s", _TimerResultChannel.Value.ToDouble(), "00:00:00"), "HH:mm:ss"))
            ElseIf Not IsNothing(_TimerResultTextChannel) Then
                _TimerResultTextChannel.Value = New Global.KineticaRT.Values.Text(Format(DateAdd("s", Values.Zero.ToDouble(), "00:00:00"), "HH:mm:ss"))
            End If

            _TimerMaxValue = TimerMaxValue

            If _TimerMaxValue > 0 Then
                _Sequence.InitializeChannel(_TimerMaxValueReachedChannel, TimerMaxValueReachedChannelName)
            End If

            _MessageToDisplay = MessageToDisplay

        End Sub

        Private Sub AddHandlersTimerController()
            AddHandler _ResetTimerTriggerChannel.ValueChanged, AddressOf OnResetTimerTrigger
        End Sub

        Private Sub RemoveHandlersTimerController()

            If Not _ResetTimerTriggerChannel Is Nothing Then
                RemoveHandler _ResetTimerTriggerChannel.ValueChanged, AddressOf OnResetTimerTrigger
            End If

        End Sub

        Private Sub OnResetTimerTrigger(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If CELProject.IsTrue(newVal) Then
                    _TimeReference = 0.0
                    If Not IsNothing(_TimerResultChannel) Then
                        _TimerResultChannel.Value = Values.Zero
                        If Not IsNothing(_TimerResultTextChannel) Then
                            _TimerResultTextChannel.Value = New Global.KineticaRT.Values.Text(Format(DateAdd("s", _TimerResultChannel.Value.ToDouble(), "00:00:00"), "HH:mm:ss"))
                        End If
                    End If

                    _InitializingTimer = True
                    _TimerCompleted = False
                    If _TimerMaxValue > 0 Then
                        _TimerMaxValueReachedChannel.Value = Values.False
                        _MessageToUser = False
                    End If

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnStartTimerTrigger: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '***********************
        '* StartTimer() *
        '***********************
        Public Sub Timer()

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If
                'Verifie si la condition qui permet le démarrage du timer est atteinte
                If CELProject.IsTrue(_StartTimerTriggerChannel.Value) Then
                    'Si le Start est Completed, il n'y a aucune action a prendre lorsque la condition de start est TRUE
                    If _TimerCompleted = False Then

                        'Si c'est un décompte qui est traité, on passe dans la fonction d'initialisation
                        If _InitializingTimer = True Then

                            'Le Timer est réinitialisé
                            _ts_Timer = New System.TimeSpan(0, 0, 0, 0, 0)
                            _ElapsedTime = Now

                            'On remet a zero l'indicateur du temps dans l'écran
                            'Si la valeur est persistente, on initialise pas. Si elle est persistente, on sauvegarde sa valeur initiale.

                            If _IsPersistent = False And Not IsNothing(_TimerResultChannel) Then
                                _TimerResultChannel.Value = New Global.KineticaRT.Values.Double(0)
                            ElseIf _IsPersistent = True And Not IsNothing(_TimerResultChannel) Then
                                If Double.IsNaN(_TimerResultChannel.Value.ToDouble()) Then
                                    _TimerResultInitialValue = 0
                                Else
                                    _TimerResultInitialValue = _TimerResultChannel.Value.ToDouble
                                End If

                            End If

                            If Not IsNothing(_TimerResultTextChannel) AndAlso Not IsNothing(_TimerResultChannel) Then
                                _TimerResultTextChannel.Value = New Global.KineticaRT.Values.Text(Format(DateAdd("s", _TimerResultChannel.Value.ToDouble(), "00:00:00"), "HH:mm:ss"))
                            End If

                            'On désactive la phase d'initialisation tant qu'il n'y a pas un nouveau timer
                            _InitializingTimer = False

                        End If

                    End If

                    'Le temps s'additionne jusqu'a ce qu'on atteigne la condition d'arret du timer est atteinte

                    If IsNothing(_StopTimerTriggerChannel) OrElse _StopTimerTriggerChannel.Value = Values.False Then


                        _ts_Timer = _ts_Timer.Add(Now.Subtract(_ElapsedTime))
                        _TimeReference = _ts_Timer.TotalSeconds
                        If Not IsNothing(_TimerResultChannel) Then
                            _TimerResultChannel.Value = New Global.KineticaRT.Values.Double(_TimerResultInitialValue + _TimeReference)
                            If Not IsNothing(_TimerResultTextChannel) Then
                                _TimerResultTextChannel.Value = New Global.KineticaRT.Values.Text(Format(DateAdd("s", _TimerResultChannel.Value.ToDouble(), "00:00:00"), "HH:mm:ss"))
                            End If
                        End If
                        _ElapsedTime = Now

                        'On verifie si on a une valeur maximum associé au timer : 0 est lorsqu'on en a pas
                        If _TimerMaxValue > 0 Then

                            'Si cette valeur est égale ou a dépassé la valeur maximum, on enclanche "True" le channel permettant de prendre action lorsque le timer a atteint ou dépassé le seuil
                            If (_TimeReference >= _TimerMaxValue) Then

                                _TimerMaxValueReachedChannel.Value = Values.True

                                'On informe l'usager, un seule fois
                                If _MessageToUser = False Then
                                    If Not IsNothing(_TimerResultChannel) Then
                                        CEL.HMI.CELDAS.NotifyIcon.ShowBalloonTip(3000, "Timers Controller", _TimerResultChannel.Description & " reached maximum value", ToolTipIcon.Info)
                                    ElseIf _MessageToDisplay <> "" Then
                                        CEL.HMI.CELDAS.NotifyIcon.ShowBalloonTip(3000, "Timers Controller", _MessageToDisplay.ToString(), ToolTipIcon.Info)
                                    Else
                                        CEL.HMI.CELDAS.NotifyIcon.ShowBalloonTip(3000, "Timers Controller", "Timer reached maximum value", ToolTipIcon.Info)
                                    End If

                                    _MessageToUser = True

                                End If

                            End If

                        End If

                        'Condition d'arrêt
                    ElseIf _StopTimerTriggerChannel.Value = Values.True Then

                        _TimerCompleted = True

                    End If


                Else

                    'Utiliser pour les timers sans condition de stop (ex: operating time) pour eviter que lorsque la condition de "start" est interrompue et reobtenue par la suite, que le temps
                    'entre les deux conditions ne soient pas comptabilisé.
                    _ElapsedTime = Now

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("Timer: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called

                    RemoveHandlersTimerController()
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
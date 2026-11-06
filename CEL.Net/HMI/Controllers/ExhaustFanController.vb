Imports System
Imports System.DateTime
Imports System.Linq
Imports System.Windows.Forms
Imports System.Threading
Imports KineticaRT.Channels
Imports KineticaRT.Values
Imports CEL.Data.BusinessObjects

Imports EntitySpaces.DynamicQuery
Imports EntitySpaces.Interfaces
Imports EntitySpaces.Loader

Namespace CEL.HMI
    'NUMÉRIQUES
    'Valeurs COMMUNES utilisées par le HMI sous le cadrage ExhaustFan dans FCS 
    'EXHAUST_FAN_ACTUAL_RATIO           'C'est le canal qui est lu par le PID (Valeur du process) Il ne faut pas le mettre dans le TestSession s'il n'y a pas rien à automatiser
    'EXHAUST_FAN_DEMAND                 'Fan Speed    - sous le mode manuel on peut changer sa valeur
    'EXHAUST_FAN_DEMAND_INCREMENT       'Step         - valeur que l'on peut changer pour incrémenter plus rapidement soit Fan Speed mode MAN ou Ratio Demand mode AUTO 
    'EXHAUST_FAN_DEMAND_RATIO           'Ratio Demand - visible seulement sur le mode Auto
    'EXHAUST_FAN_DEMAND_RATIO_MVDC      'Mapper avec Fan Speed
    'EXHAUST_FAN_DEMAND_RATIO_VDC       'Mapper avec Fan Speed
    'EXHAUST_FAN_DEMAND_TO_OUTPUT_MVDC  'Mapper avec Fan Speed

    'Valeurs spécifique à une gamme en relation avec tous les moteurs de même force par exemple PW206 pour couvrir PW206A PW206B ainsi de suite, mais pas PW207
    'EXHAUST_FAN_DEMAND_PRESET_1    'Valeur persistente utilisée par le mode manuel quand la vitesse est en DESSOUS de GI        
    'EXHAUST_FAN_DEMAND_PRESET_2    'Valeur persistente utilisée par le mode manuel quand la vitesse est au DESSUS de GI

    'Valeur spécifique à la famille des moteurs
    'EXHAUST_FAN_TARGET_RATIO

    'BOOLÉENNES
    'Valeurs COMMUNES utilisées par le HMI sous le cadrage ExhaustFan dans FCS 
    'EXHAUST_FAN_CONTROLLER_ENB         'Non-utilisé
    'EXHAUST_FAN_UP_DEMAND_BTN          'Pour l'incrément
    'EXHAUST_FAN_DOWN_DEMAND_BTN        'Pour l'incrément
    'EXHAUST_FAN_MAN_AUTO_BTN
    'EXHAUST_FAN_PRESET_1_IND           'Indique si on sauvegarde la fan speed preset 1 en mode man, invisible en auto
    'EXHAUST_FAN_PRESET_2_IND           'Indique si on sauvegarde la fan speed preset 2 en mode man, invisible en auto

    'Valeur spécifique à la famille des moteurs
    'EXHAUST_FAN_ON_TARGET_RATIO_IND            'Indique si nous sommes au ratio voulu
    'MOTOR_BELOW_GROUND_IDLE_CONDITION          
    'MOTOR_BELOW_GROUND_IDLE_CONDITION_LATCH

    Public Class ExhaustFanController
        Implements IDisposable

        'Valeur arbitraire
        Private ReadOnly MIN_RATIO As New Global.KineticaRT.Values.Double(0.0)
        Private ReadOnly MAX_RATIO As New Global.KineticaRT.Values.Double(1.5)

        'Valeur objectif
        Private ReadOnly MAX_PCT As New Global.KineticaRT.Values.Double(100.0)
        Private ReadOnly MIN_PCT As New Global.KineticaRT.Values.Double(0.0)
        Private ReadOnly KINETICART_FALSE As New Global.KineticaRT.Values.Boolean(False)
        Private ReadOnly KINETICART_TRUE As New Global.KineticaRT.Values.Boolean(True)

        'Tolérance du PW206B
        Private ReadOnly PRESET_TOLERANCE As New Global.KineticaRT.Values.Double(0.003)

        Private _PIDoutput As Global.KineticaRT.Values.Double

        Private _InitialValue As Global.KineticaRT.Values.Double = Nothing

        Private a As Value
        Private CELProject As CELDAS = CELDAS.Instance()
        Private _PersistentValuesCtrl As PersistentValuesController
        Private _DynamicCollection As New UUTDynamicChannelCollectionCollection
        Private _ShortMotorName As String = String.Empty 'Par exemple pour un PW206B2 la valeur sera PW206

        Private _Sequence As Global.CEL.KineticaRT.Sequence.Sequence

        Private _RatioFeedbackChannel As INumericChannel
        Private _RatioDemandChannel As INumericChannel
        Private _RatioTargetChannel As INumericChannel
        Private _IncrementChannel As INumericChannel
        Private _DemandPreset_1_Channel As INumericChannel
        Private _DemandPreset_2_Channel As INumericChannel

        Private _Preset_1_Indicator As IBooleanChannel
        Private _Preset_2_Indicator As IBooleanChannel
        Private _EnableVFDChannel As IBooleanChannel
        Private _EnabledVFDChannel As Global.KineticaRT.Channels.IBooleanChannel
        Private _UpDemandChannel As IBooleanChannel
        Private _DownDemandChannel As IBooleanChannel
        Private _ManAutoDemandChannel As IBooleanChannel
        Private _IsMotorBelowGroundIdle As IBooleanChannel

        Private _DemandChannel As IChannel

        Private _indExhaustFanPreset1 As Global.KineticaRT.Studio.Controls.LampIndicator
        Private _indExhaustFanPreset2 As Global.KineticaRT.Studio.Controls.LampIndicator
        Private _indExhaustFanOnTargetRatio As Global.KineticaRT.Studio.Controls.LampIndicator

        Private _btnAutoMan As Global.CEL.KineticaRT.Controls.Button

        Private _lblRatioDemand As System.Windows.Forms.Label
        Private _dspRatioDemand As Global.KineticaRT.Studio.Controls.Display
        Private _lblActualRatio As System.Windows.Forms.Label
        Private _dspActualRatio As Global.KineticaRT.Studio.Controls.Display

        Private _PIDController As PIDController

        Private _WorkerThread As Thread = Nothing
        Private _ExitThread As Boolean
        Private _ThreadDelay As Integer = 250 '250 Ms Is default value

        'Variables pour la sequence d'armement
        Private _ts_ExhaustFan_Timer As System.TimeSpan
        Private _ExhaustFanElapsedTime As System.DateTime
        Private _ExhaustFanTimeReference As Double = 0 'Base de temps
        Private _InitializingExhaustFanProtectionControl As Boolean = True

        Private _ExhaustFanProtectionControlTime As Double = 40
        Public Property ExhaustFanProtectionControlTime() As Double
            Get
                Return _ExhaustFanProtectionControlTime
            End Get
            Set(ByVal value As Double)
                _ExhaustFanProtectionControlTime = value
            End Set
        End Property

        Public Sub New(ByRef sequence As CEL.KineticaRT.Sequence.Sequence, _
                       Optional ByVal ratioFeedbackChannelName As String = "EXHAUST_FAN_ACTUAL_RATIO", _
                       Optional ByVal ratioDemandChannelName As String = "EXHAUST_FAN_DEMAND_RATIO", _
                       Optional ByVal targetRatioChannelName As String = "EXHAUST_FAN_TARGET_RATIO", _
                       Optional ByVal incrementChannelName As String = "EXHAUST_FAN_DEMAND_INCREMENT", _
                       Optional ByVal preset_1_ChannelName As String = "EXHAUST_FAN_DEMAND_PRESET_1", _
                       Optional ByVal preset_2_ChannelName As String = "EXHAUST_FAN_DEMAND_PRESET_2", _
                       Optional ByVal preset_1_IndicatorName As String = "EXHAUST_FAN_PRESET_1_IND", _
                       Optional ByVal preset_2_IndicatorName As String = "EXHAUST_FAN_PRESET_2_IND", _
                       Optional ByVal enableVFDChannelName As String = "EXHAUST_FAN_DRIVE_ENABLE_BTN", _
                       Optional ByVal enabledVFDChannelName As String = "EXHAUST_FAN_DRIVE_ENABLED", _
                       Optional ByVal demandChannelName As String = "EXHAUST_FAN_DEMAND", _
                       Optional ByVal upDemandChannelName As String = "EXHAUST_FAN_UP_DEMAND_BTN", _
                       Optional ByVal downDemandChannelName As String = "EXHAUST_FAN_DOWN_DEMAND_BTN", _
                       Optional ByVal manAutoDemandChannelName As String = "EXHAUST_FAN_MAN_AUTO_BTN", _
                       Optional ByVal isMotorBelowGroundIdleChannelName As String = "MOTOR_BELOW_GROUND_IDLE_CONDITION_LATCH")

            Dim disableAutoMode As Boolean = False
            Dim essentialObjects As New Collections.Generic.List(Of Identifier)

            Me._Sequence = sequence
            LoadDynamicCollection()
            GetShortMotorName(preset_1_ChannelName)
            InitialisePersitentValues(_ShortMotorName)

            Me._RatioTargetChannel = CType(_Sequence.SearchObject(targetRatioChannelName), INumericChannel)
            essentialObjects.Add(New Identifier(targetRatioChannelName, Me._RatioTargetChannel))

            Me._RatioFeedbackChannel = CType(_Sequence.SearchObject(ratioFeedbackChannelName), INumericChannel)
            essentialObjects.Add(New Identifier(ratioFeedbackChannelName, Me._RatioFeedbackChannel))

            Me._RatioDemandChannel = CType(_Sequence.SearchObject(ratioDemandChannelName), INumericChannel)
            essentialObjects.Add(New Identifier(ratioDemandChannelName, Me._RatioDemandChannel))

            Me._DemandPreset_1_Channel = CType(Me._Sequence.SearchObject(preset_1_ChannelName), INumericChannel)
            essentialObjects.Add(New Identifier(preset_1_ChannelName, Me._DemandPreset_1_Channel))

            Me._DemandPreset_2_Channel = CType(Me._Sequence.SearchObject(preset_2_ChannelName), INumericChannel)
            essentialObjects.Add(New Identifier(preset_2_ChannelName, Me._DemandPreset_2_Channel))

            Me._IncrementChannel = CType(Me._Sequence.SearchObject(incrementChannelName), INumericChannel)
            essentialObjects.Add(New Identifier(incrementChannelName, Me._IncrementChannel))

            Me._EnableVFDChannel = CType(Me._Sequence.SearchObject(enableVFDChannelName), IBooleanChannel)
            essentialObjects.Add(New Identifier(enableVFDChannelName, Me._EnableVFDChannel))

            Me._EnabledVFDChannel = CType(Me._Sequence.SearchObject(enabledVFDChannelName), Global.KineticaRT.Channels.IBooleanChannel)
            essentialObjects.Add(New Identifier(enabledVFDChannelName, Me._EnabledVFDChannel))

            Me._Sequence.InitializeChannel(_DemandChannel, demandChannelName)
            essentialObjects.Add(New Identifier(demandChannelName, Me._DemandChannel))

            Me._UpDemandChannel = CType(Me._Sequence.SearchObject(upDemandChannelName), IBooleanChannel)
            essentialObjects.Add(New Identifier(upDemandChannelName, Me._UpDemandChannel))

            Me._DownDemandChannel = CType(Me._Sequence.SearchObject(downDemandChannelName), IBooleanChannel)
            essentialObjects.Add(New Identifier(downDemandChannelName, Me._DownDemandChannel))

            Me._ManAutoDemandChannel = CType(Me._Sequence.SearchObject(manAutoDemandChannelName), IBooleanChannel)
            essentialObjects.Add(New Identifier(manAutoDemandChannelName, Me._ManAutoDemandChannel))

            Me._IsMotorBelowGroundIdle = CType(Me._Sequence.SearchObject(isMotorBelowGroundIdleChannelName), IBooleanChannel)
            essentialObjects.Add(New Identifier(isMotorBelowGroundIdleChannelName, Me._IsMotorBelowGroundIdle))

            Me._DemandChannel.Value = Me._DemandPreset_1_Channel.Value
            Me._InitialValue = New Global.KineticaRT.Values.Double(Me._DemandPreset_1_Channel.Value.ToDouble)

            Dim fraExhaustFan As System.Windows.Forms.Control = CType(CELProject.GetObjectCallBack("FCS_Screen"), System.Windows.Forms.Control)

            Me._indExhaustFanPreset1 = CType(CELProject.SearchControl(fraExhaustFan, "indExhaustFanPreset1"), Global.KineticaRT.Studio.Controls.LampIndicator)
            essentialObjects.Add(New Identifier("indExhaustFanPreset1", Me._indExhaustFanPreset1))

            Me._indExhaustFanPreset2 = CType(CELProject.SearchControl(fraExhaustFan, "indExhaustFanPreset2"), Global.KineticaRT.Studio.Controls.LampIndicator)
            essentialObjects.Add(New Identifier("indExhaustFanPreset2", Me._indExhaustFanPreset2))

            Me._indExhaustFanOnTargetRatio = CType(CELProject.SearchControl(fraExhaustFan, "indExhaustFanOnTargetRatio"), Global.KineticaRT.Studio.Controls.LampIndicator)
            essentialObjects.Add(New Identifier("indExhaustFanOnTargetRatio", Me._indExhaustFanOnTargetRatio))

            Me._lblRatioDemand = CType(CELProject.SearchControl(fraExhaustFan, "lblRatioDemand"), System.Windows.Forms.Label)
            essentialObjects.Add(New Identifier("lblRatioDemand", Me._lblRatioDemand))

            Me._dspRatioDemand = CType(CELProject.SearchControl(fraExhaustFan, "dspRatioDemand"), Global.KineticaRT.Studio.Controls.Display)
            essentialObjects.Add(New Identifier("dspRatioDemand", Me._dspRatioDemand))

            Me._lblActualRatio = CType(CELProject.SearchControl(fraExhaustFan, "lblActualRatio"), System.Windows.Forms.Label)
            essentialObjects.Add(New Identifier("lblActualRatio", Me._lblActualRatio))

            Me._dspActualRatio = CType(CELProject.SearchControl(fraExhaustFan, "dspActualRatio"), Global.KineticaRT.Studio.Controls.Display)
            essentialObjects.Add(New Identifier("dspActualRatio", Me._dspActualRatio))

            Me._btnAutoMan = CType(CELProject.SearchControl(fraExhaustFan, "btnExhaustFanManAuto"), Global.CEL.KineticaRT.Controls.Button)
            essentialObjects.Add(New Identifier("btnExhaustFanManAuto", Me._btnAutoMan))

            'RatioDemandChannel c'est le seul canal pouvant ne pas être défini si c'est le cas le mode manuel sera le seul fonctionnel
            If _RatioDemandChannel IsNot Nothing And _RatioFeedbackChannel IsNot Nothing Then
                Me._RatioDemandChannel.Value = Me._RatioTargetChannel.Value
                Me._PIDController = New PIDController(_RatioDemandChannel, _DemandChannel, _RatioFeedbackChannel, 1.2, 0.02, 0, 30)
            Else
                disableAutoMode = True
            End If

            SetManModeView(disableAutoMode)
            AddHandlers()

            Dim undefinedObjects As System.Collections.Generic.List(Of String) = (From undefinedObj In essentialObjects Where undefinedObj.Element Is Nothing Select undefinedObj.Name).ToList()

            If undefinedObjects.Count > 0 Then
                Dim stringBuilder As New System.Text.StringBuilder("Element(s) not defined: ")

                For Each undefinedElement As String In undefinedObjects
                    stringBuilder.AppendLine(undefinedElement)
                Next

                Throw New System.Exception(stringBuilder.ToString)
            End If

        End Sub

        Public Sub StartWorkerThread(ByVal ThreadDelay As Integer)

            _ThreadDelay = ThreadDelay
            'Thread Control
            _ExitThread = False
            _WorkerThread = New Thread(AddressOf WorkingThread)
            _WorkerThread.Priority = ThreadPriority.Normal
            _WorkerThread.Name = "ExhaustFan_WorkerThread"
            _WorkerThread.Start()
        End Sub

        Private Sub WorkingThread()
            Try

                Do While Not _ExitThread
                    Thread.Sleep(_ThreadDelay)
                    If CELProject.IsFalse(CELProject.ARCHIVE_PLAY.Value) Then  'N'execute pas cette fonction si l'archive est en playback
                        ExhaustFanControllerWorkingProtection()
                    End If
                Loop
            Catch ex As Exception
                MessageBox.Show("LoadBankController.WorkingThread: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub AddHandlers()
            If Not (_EnableVFDChannel Is Nothing) Then
                AddHandler _EnableVFDChannel.ValueChanged, AddressOf OnEnableController
            End If
            If Not (_DemandChannel Is Nothing) Then
                AddHandler _DemandChannel.ValueChanged, AddressOf OnDemandValidated
            End If
            If Not (_RatioDemandChannel Is Nothing) Then
                AddHandler _RatioDemandChannel.ValueChanged, AddressOf OnRatioDemandValidated
            End If
            If Not (_IncrementChannel Is Nothing) Then
                AddHandler _IncrementChannel.ValueChanged, AddressOf OnIncrementDemandValidated
            End If
            If Not (_UpDemandChannel Is Nothing) Then
                AddHandler _UpDemandChannel.ValueChanged, AddressOf OnDemandControl
            End If
            If Not (_DownDemandChannel Is Nothing) Then
                AddHandler _DownDemandChannel.ValueChanged, AddressOf OnDemandControl
            End If
            If Not (_ManAutoDemandChannel Is Nothing) Then
                AddHandler _ManAutoDemandChannel.ValueChanged, AddressOf OnModeChange
            End If
            If Not (_IsMotorBelowGroundIdle Is Nothing) Then
                AddHandler _IsMotorBelowGroundIdle.ValueChanged, AddressOf OnGroundIdleState
            End If

        End Sub

        Private Sub RemoveHandlers()
            If Me._EnableVFDChannel IsNot Nothing Then
                RemoveHandler Me._EnableVFDChannel.ValueChanged, AddressOf OnEnableController
            End If
            If Me._DemandChannel IsNot Nothing Then
                RemoveHandler Me._DemandChannel.ValueChanged, AddressOf OnDemandValidated
            End If
            If Me._RatioDemandChannel IsNot Nothing Then
                RemoveHandler Me._RatioDemandChannel.ValueChanged, AddressOf OnRatioDemandValidated
            End If
            If Me._IncrementChannel IsNot Nothing Then
                RemoveHandler Me._IncrementChannel.ValueChanged, AddressOf OnIncrementDemandValidated
            End If
            If Me._UpDemandChannel IsNot Nothing Then
                RemoveHandler Me._UpDemandChannel.ValueChanged, AddressOf OnDemandControl
            End If
            If Me._DownDemandChannel IsNot Nothing Then
                RemoveHandler Me._DownDemandChannel.ValueChanged, AddressOf OnDemandControl
            End If
            If Me._ManAutoDemandChannel IsNot Nothing Then
                RemoveHandler Me._ManAutoDemandChannel.ValueChanged, AddressOf OnModeChange
            End If

            If Me._IsMotorBelowGroundIdle IsNot Nothing Then
                RemoveHandler Me._IsMotorBelowGroundIdle.ValueChanged, AddressOf OnGroundIdleState
            End If

        End Sub

        Private Sub SetManModeView(Optional disableAutoMode As Boolean = False)

            Try
                AddHandler _DemandChannel.ValueChanged, AddressOf OnDemandValidated

                Me._IncrementChannel.Format = "F0"
                Me._IncrementChannel.Minimum = 1
                Me._IncrementChannel.Minimum = 100
                Me._IncrementChannel.Value = New Global.KineticaRT.Values.Double(1.0)

                If disableAutoMode Then
                    Me._lblActualRatio.Visible = False
                    Me._dspActualRatio.Visible = False

                    Me._btnAutoMan.Visible = False
                    Me._btnAutoMan.Enabled = False

                    Me._indExhaustFanOnTargetRatio.Visible = False
                Else
                    Me._lblActualRatio.Visible = True
                    Me._dspActualRatio.Visible = True
                    Me._indExhaustFanPreset1.Visible = True
                    Me._indExhaustFanPreset2.Visible = True

                    Me._btnAutoMan.Visible = True
                    Me._btnAutoMan.Enabled = True

                    Me._indExhaustFanOnTargetRatio.Visible = True
                End If


                Me._lblRatioDemand.Visible = False
                Me._dspRatioDemand.Visible = False

                Me._lblActualRatio.Location = New System.Drawing.Point(6, 120)
                Me._dspActualRatio.Location = New System.Drawing.Point(8, 144)

            Catch ex As Exception
                clsTopMostMessageBox.Show("SetManModeView: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub SetAutoModeView()

            Try
                RemoveHandler Me._DemandChannel.ValueChanged, AddressOf OnDemandValidated

                Me._IncrementChannel.Format = "F3"
                Me._IncrementChannel.Minimum = 0.001
                Me._IncrementChannel.Minimum = 1.0
                Me._IncrementChannel.Value = New Global.KineticaRT.Values.Double(0.001)

                Me._indExhaustFanPreset1.Visible = False
                Me._indExhaustFanPreset2.Visible = False
                Me._indExhaustFanOnTargetRatio.Visible = False

                Me._lblRatioDemand.Visible = True
                Me._dspRatioDemand.Visible = True

                Me._lblActualRatio.Visible = True
                Me._dspActualRatio.Visible = True
                Me._lblActualRatio.Location = New System.Drawing.Point(6, 152)
                Me._dspActualRatio.Location = New System.Drawing.Point(8, 176)

            Catch ex As Exception
                clsTopMostMessageBox.Show("SetAutoModeView: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub OnEnableController(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                _EnabledVFDChannel.Value = KINETICART_TRUE

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnEnableController: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub OnDemandValidated(ByVal sender As IChannel, ByVal newVal As Value)

            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                'If the user delete de value in the input value and then hit enter we will have an exception, so assume miminum value in this case
                'Try
                'Dim TempValue As Integer = CInt(_DemandChannel.Value.ToDouble())
                'Catch

                'End Try
                If newVal IsNot Nothing Then
                    If Me._ManAutoDemandChannel.Value = KINETICART_FALSE Then
                        If newVal < MAX_PCT And newVal > MIN_PCT Then

                            Me._DemandChannel.Value = newVal

                            If Me._IsMotorBelowGroundIdle.Value = KINETICART_TRUE Then
                                Me._DemandPreset_1_Channel.Value = newVal
                            Else
                                Me._DemandPreset_2_Channel.Value = newVal
                            End If

                        ElseIf _DemandChannel.Value > MAX_PCT Then

                            Me._DemandChannel.Value = MAX_PCT

                        ElseIf _DemandChannel.Value < MIN_PCT Then

                            Me._DemandChannel.Value = MIN_PCT

                        End If

                    End If
                Else
                    Me._DemandChannel.Value = Me._InitialValue
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnDemandValidated: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub OnRatioDemandValidated(ByVal sender As IChannel, ByVal newVal As Value)

            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                'If the user delete de value in the input value and then hit enter we will have an exception, so assume miminum value in this case
                'Try
                'Dim TempValue As Integer = CInt(_DemandChannel.Value.ToDouble())
                'Catch

                'End Try
                If newVal IsNot Nothing Then
                    If Me._ManAutoDemandChannel.Value = KINETICART_TRUE Then

                        If newVal < MAX_RATIO And newVal > MIN_RATIO Then

                            Me._RatioDemandChannel.Value = newVal

                        ElseIf _RatioDemandChannel.Value > MAX_RATIO Then

                            Me._RatioDemandChannel.Value = MAX_RATIO

                        ElseIf _RatioDemandChannel.Value < MIN_RATIO Then

                            Me._RatioDemandChannel.Value = MIN_RATIO

                        End If

                    End If
                Else
                    Me._RatioDemandChannel.Value = Me._RatioTargetChannel.Value
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnRatioDemandValidated: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub OnIncrementDemandValidated(ByVal sender As IChannel, ByVal newVal As Value)

            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                'Mode Automatique
                If _ManAutoDemandChannel.Value = KINETICART_TRUE Then

                    'If the user delete de value in the input value and then hit enter we will have an exception, so assume miminum value in this case
                    Try
                        Dim TempValue As Double = CDbl(_IncrementChannel.Value.ToDouble())
                    Catch
                        _IncrementChannel.Value = New Global.KineticaRT.Values.Double(0.001)
                    End Try

                    If _IncrementChannel.Value > MAX_RATIO Then
                        _IncrementChannel.Value = MAX_RATIO
                    ElseIf _IncrementChannel.Value.ToDouble() < 0.001 Then
                        _IncrementChannel.Value = New Global.KineticaRT.Values.Double(0.001)
                    End If

                    'Mode Manuel
                ElseIf _ManAutoDemandChannel.Value = KINETICART_FALSE Then

                    'If the user delete de value in the input value and then hit enter we will have an exception, so assume miminum value in this case
                    Try
                        Dim TempValue As Integer = CInt(_IncrementChannel.Value.ToDouble())
                    Catch
                        _IncrementChannel.Value = New Global.KineticaRT.Values.Double(1.0)
                    End Try

                    If _IncrementChannel.Value > MAX_PCT Then
                        _IncrementChannel.Value = MAX_PCT
                    ElseIf _IncrementChannel.Value.ToDouble() < 1.0 Then
                        _IncrementChannel.Value = New Global.KineticaRT.Values.Double(1.0)
                    End If

                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnIncrementDemandValidated: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub


        'Évenement des boutons up et down
        Private Sub OnDemandControl(ByVal sender As IChannel, ByVal newVal As Value)

            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If CELProject.IsTrue(newVal) Then
                    If Me._ManAutoDemandChannel.Value = KINETICART_FALSE Then
                        'Mode Manuel
                        If sender.Name = _UpDemandChannel.Name.ToString Then
                            Me._DemandChannel.Value = Me._DemandChannel.Value.Add(_IncrementChannel.Value)
                        ElseIf sender.Name = _DownDemandChannel.Name.ToString Then
                            Me._DemandChannel.Value = Me._DemandChannel.Value.Subtract(_IncrementChannel.Value)
                        End If
                    Else
                        'Mode Automatique
                        If sender.Name = _UpDemandChannel.Name.ToString Then
                            Me._RatioDemandChannel.Value = Me._RatioDemandChannel.Value.Add(_IncrementChannel.Value)
                        ElseIf sender.Name = _DownDemandChannel.Name.ToString Then
                            Me._RatioDemandChannel.Value = Me._RatioDemandChannel.Value.Subtract(_IncrementChannel.Value)
                        End If
                    End If
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnDemandControl: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        'Évenement du mode manuel et automatique
        Private Sub OnModeChange(ByVal sender As IChannel, ByVal newVal As Value)

            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If newVal = KINETICART_FALSE Then
                    'Mode manuel

                    'On désactive la rédirection du PID calculée à Fan Speed
                    Me._PIDController.Enable = False

                    'On remet la valeur de son preset en fonction du ground idle
                    If Me._IsMotorBelowGroundIdle.Value = KINETICART_TRUE Then
                        Me._DemandChannel.Value = Me._DemandPreset_1_Channel.Value
                    Else
                        Me._DemandChannel.Value = Me._DemandPreset_2_Channel.Value
                    End If

                    SetManModeView()
                Else
                    'On sauvegarde la valeur de son preset en fonction du ground idle
                    If Me._ManAutoDemandChannel.Value = KINETICART_FALSE Then
                        If Me._IsMotorBelowGroundIdle.Value = KINETICART_TRUE Then
                            Me._DemandPreset_1_Channel.Value = Me._DemandChannel.Value
                        Else
                            Me._DemandPreset_2_Channel.Value = Me._DemandChannel.Value
                        End If
                    End If

                    Me._DemandChannel.Value = Me._PIDController.OutputPCT

                    'On rédirige valeur du PID calculée à Fan Speed
                    Me._PIDController.Enable = True
                    SetAutoModeView()
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnModeChange: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub OnGroundIdleState(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                'On remet la valeur de son preset en fonction du ground idle
                If Me._ManAutoDemandChannel.Value = KINETICART_FALSE Then
                    If Me._IsMotorBelowGroundIdle.Value = KINETICART_TRUE Then
                        Me._DemandChannel.Value = Me._DemandPreset_1_Channel.Value
                    Else
                        Me._DemandChannel.Value = Me._DemandPreset_2_Channel.Value
                    End If
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnGroundIdleState: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        '**********************************************************
        '* ExhaustFanControllerWorkingProtection()                *
        '* Used in the Engine Thread                              *
        '**********************************************************
        Private Sub ExhaustFanControllerWorkingProtection()

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If CELProject.IsFalse(_EnableVFDChannel.Value) And CELProject.IsTrue(_EnabledVFDChannel.Value) Then

                If _InitializingExhaustFanProtectionControl = True Then

                    'Le Timer est réinitialisé
                    _ts_ExhaustFan_Timer = New System.TimeSpan(0, 0, 0, 0, 0)
                    _ExhaustFanElapsedTime = Now

                    _InitializingExhaustFanProtectionControl = False

                    'On informe l'utilisateur de qu'il faut attendre un delais pour que l'exhaust fan soit completement arreter avant de fermer les louvers
                    CELDAS.NotifyIcon.ShowBalloonTip(10000, CELProject.TechnologyName, "Exhaust Fan stopping : Please Wait " & _ExhaustFanProtectionControlTime & " seconds.", ToolTipIcon.Info)

                End If

                _ts_ExhaustFan_Timer = _ts_ExhaustFan_Timer.Add(Now.Subtract(_ExhaustFanElapsedTime))
                _ExhaustFanTimeReference = _ts_ExhaustFan_Timer.TotalSeconds
                _ExhaustFanElapsedTime = Now

                If _ExhaustFanTimeReference >= _ExhaustFanProtectionControlTime Then
                    _EnabledVFDChannel.Value = KINETICART_FALSE
                    _InitializingExhaustFanProtectionControl = True
                End If

            End If

        End Sub

        Private Sub LoadDynamicCollection()
            _DynamicCollection.es.Connection.Name = CELProject.DefaultConnectionName
            _DynamicCollection.es.Connection.Name = CELProject.DefaultConnectionName
            _DynamicCollection.Query.Where(_DynamicCollection.Query.ProjectID = CELProject.Project.Number)
            _DynamicCollection.Query.Where(_DynamicCollection.Query.EngineModel = CELProject.EngineModel)

            If Not (_DynamicCollection.Query.Load()) Then
                clsTopMostMessageBox.Show("Unable to load: UUTDynamicChannelsCollection in ExhaustFanController", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Sub

        Private Sub GetShortMotorName(presetChannelName As String)
            Dim preset_1_Channel As New HMINumericChannels

            Dim openPNum As New esComparison(esParenthesis.Open)
            preset_1_Channel.es.Connection.Name = CELProject.DefaultConnectionName
            Dim dynamicQueryPropsNum As New EntitySpaces.DynamicQuery.esDynamicQuerySerializable.DynamicQueryProps(preset_1_Channel.Query)

            preset_1_Channel.Query.Where(New esComparison(esParenthesis.Open))
            dynamicQueryPropsNum.DefaultConjunction = esConjunction.Or
            For Each collection As UUTDynamicChannelCollection In _DynamicCollection
                preset_1_Channel.Query.Where(preset_1_Channel.Query.CollectionName = collection.CollectionName)
            Next
            preset_1_Channel.Query.Where(New esComparison(esParenthesis.Close))
            dynamicQueryPropsNum.DefaultConjunction = esConjunction.And

            preset_1_Channel.Query.Where(preset_1_Channel.Query.ChannelName = presetChannelName)

            If Not (preset_1_Channel.Query.Load()) Then
                clsTopMostMessageBox.Show("Exhaust Fan preset 1 not found be sure that is define in TestSession", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                _ShortMotorName = preset_1_Channel.CollectionName
            End If
        End Sub

        Private Sub InitialisePersitentValues(shortMotorName As String)
            Dim PresetsDir As New System.IO.DirectoryInfo("C:\CELDAS DATA\Exhaust Fan Presets")

            Dim PersistentValuesChannels() As String = {"EXHAUST_FAN_DEMAND_PRESET_1", "EXHAUST_FAN_DEMAND_PRESET_2"}
            Dim PersistentValuesFileName As String = PresetsDir.FullName & "\" & shortMotorName & "\PersistentValues.xml"

            If Not PresetsDir.Exists Then
                PresetsDir.Create()
            End If

            If Not PresetsDir.GetDirectories.Equals(shortMotorName) Then
                PresetsDir.CreateSubdirectory(shortMotorName)
            End If


            _PersistentValuesCtrl = New PersistentValuesController(Me._Sequence, PersistentValuesChannels, 1, PersistentValuesFileName)
        End Sub

        Public Sub SavePersistentValues(Optional ForceSave As Boolean = False)
            _PersistentValuesCtrl.SavePersistentValues(ForceSave)
        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called
                    RemoveHandlers()
                    _PersistentValuesCtrl.SavePersistentValues(True)

                    'Since this is an indirect command we have to wait for the thread to be stopped
                    Dim NumberIteration As Integer

                    _ExitThread = True
                    For NumberIteration = 0 To 2000
                        Thread.Sleep(25) 'give a chance to the thread to complete  the exit: 2000*25ms = 10 seconds

                        'already stop, do nothing
                        If _WorkerThread Is Nothing Then
                            Exit For
                        End If

                        'the thread is not active anymore
                        If Not _WorkerThread.IsAlive Then
                            Exit For
                        End If
                    Next

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

        Private Class Identifier
            Public Sub New(name As String, element As Object)
                Me.Name = name
                Me.Element = element
            End Sub

            Public Property Name As String
            Public Property Element As Object
        End Class

    End Class

End Namespace
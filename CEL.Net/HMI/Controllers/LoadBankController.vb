Imports System
Imports System.Math
Imports System.Windows.Forms
Imports System.Threading
Imports KineticaRT.Channels
Imports KineticaRT.Values


Namespace CEL.HMI

    Public Class LoadBankController

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Public Structure LB_DataInfo
            Public DelestageKwpersec As Integer
            Public DemandChannelIncrementName As String
            Public InitialDemandChannelName As String
            Public DemandChannelName As String
            Public DisplayName As String
            Public DownDemandChannelName As String
            Public EnableChannelName As String
            Public FanCoolDownSeconds As Integer
            Public FanEnableChannelName As String
            Public FanEnableBtnChannelName As String
            Public FieldFlashingSignalName As String
            Public FieldFlashingTimeMsec As Integer
            Public FieldFlashingTriggerSignalName As String
            Public FrameName As String
            Public Increment As Integer
            Public Maximum As Integer
            Public Memorize As Boolean
            Public ThreadSpeedMsec As Integer
            Public UpDemandChannelName As String
            Public FanEnableChannelPulseName As String
            Public FanStopChannelPulseName As String
            Public BtnLatchSeconds As Integer
            Public PulseEnableInitialType As Boolean
            Public PulseStopInitialType As Boolean
        End Structure

        Private _LBInfo As LB_DataInfo
        Private _LBDemandChannel As IChannel = Nothing
        Private _LBInitialDemandChannel As IChannel = Nothing
        Private _LBDemandChannelIncrement As IChannel = Nothing
        Private _LBDisplay As Global.KineticaRT.Studio.Controls.Display = Nothing
        Private _LBDownDemandChannel As IChannel = Nothing
        Private _LBEnableChannel As IChannel = Nothing
        Private _LBFanEnableChannel As IChannel = Nothing
        Private _LBFanEnableBtnChannel As IChannel = Nothing
        Private _LBFieldFlashingChannel As IChannel = Nothing
        Private _LBFieldFlashingTriggerChannel As IChannel = Nothing
        Private _LBUpDemandChannel As IChannel = Nothing

        Private _LBFanEnableChannelPulse As IChannel = Nothing
        Private _LBFanStopChannelPulse As IChannel = Nothing
        Private _StartDoutTimer As Integer
        Private _StopDoutTimer As Integer
        Private _LBFanWasTrue As Boolean = False

        Private _PulseStopInitialType As Boolean
        Private _PulseEnableInitialType As Boolean

        Private _LBDemandMemorized As Double 'Used to keep the LB demand when disabled
        Private _FanCoolTimer As Integer
        Private _RampDownDemand As Double = 0.0
        Private _LBFanDisable As Boolean = False

        Private _WorkerThread As Thread = Nothing
        Private _ExitThread As Boolean
        Private _ThreadDelay As Integer = 250 '250 Ms Is default value
        Private _ThreadFrequency As Double

        '*************
        '* New()     *
        '*************
        Public Sub New(ByVal LBDataInfo As LB_DataInfo)

            _LBInfo = LBDataInfo

            If _LBInfo.FrameName Is Nothing Then
                If Not (_LBInfo.DisplayName Is Nothing) Then
                    clsTopMostMessageBox.Show("FrameName parameter is required because a DisplayName is specified", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                Dim LB_FRAME As Global.KineticaRT.Controls.Control = Nothing

                CELProject.InitializeObjectCallBack(LB_FRAME, _LBInfo.FrameName)

                If LB_FRAME Is Nothing Then
                    clsTopMostMessageBox.Show("FrameName parameter does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    If Not (_LBInfo.DisplayName Is Nothing) Then
                        _LBDisplay = CType(LB_FRAME.FindObject(_LBInfo.DisplayName), Global.KineticaRT.Studio.Controls.Display)

                        If _LBDisplay Is Nothing Then
                            clsTopMostMessageBox.Show("DisplayName: " + _LBInfo.DisplayName + " does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If
                End If
            End If

            If Not (_LBInfo.FanEnableBtnChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_LBFanEnableBtnChannel, _LBInfo.FanEnableBtnChannelName)

                If _LBFanEnableBtnChannel Is Nothing Then
                    clsTopMostMessageBox.Show("FanEnableBtnChannelName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    _LBFanEnableBtnChannel.Value = Values.False
                End If
            End If

            If Not (_LBInfo.FanEnableChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_LBFanEnableChannel, _LBInfo.FanEnableChannelName)

                If _LBFanEnableChannel Is Nothing Then
                    clsTopMostMessageBox.Show("FanEnableChannelName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    _LBFanEnableChannel.Value = Values.False
                End If
            End If

            If Not (_LBInfo.EnableChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_LBEnableChannel, _LBInfo.EnableChannelName)

                If _LBEnableChannel Is Nothing Then
                    clsTopMostMessageBox.Show("EnableChannelName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

            If Not (_LBInfo.DemandChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_LBDemandChannel, _LBInfo.DemandChannelName)

                If _LBDemandChannel Is Nothing Then
                    clsTopMostMessageBox.Show("DemandChannelName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    _LBDemandChannel.Value = Values.Zero
                End If
            End If

            If Not (_LBInfo.InitialDemandChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_LBInitialDemandChannel, _LBInfo.InitialDemandChannelName)

                If _LBInitialDemandChannel Is Nothing Then
                    clsTopMostMessageBox.Show("InitialDemandChannelName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    _LBInitialDemandChannel.Value = Values.Zero
                End If
            End If

            If Not (_LBInfo.DemandChannelIncrementName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_LBDemandChannelIncrement, _LBInfo.DemandChannelIncrementName)

                If _LBDemandChannelIncrement Is Nothing Then
                    clsTopMostMessageBox.Show("DemandChannelIncrementName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    _LBDemandChannelIncrement.Value = New Global.KineticaRT.Values.Double(_LBInfo.Increment)
                End If
            End If

            If Not (_LBInfo.UpDemandChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_LBUpDemandChannel, _LBInfo.UpDemandChannelName)

                If _LBUpDemandChannel Is Nothing Then
                    clsTopMostMessageBox.Show("UpDemandChannelName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

            If Not (_LBInfo.DownDemandChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_LBDownDemandChannel, _LBInfo.DownDemandChannelName)

                If _LBDownDemandChannel Is Nothing Then
                    clsTopMostMessageBox.Show("DownDemandChannelName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

            If Not (_LBInfo.FieldFlashingSignalName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_LBFieldFlashingChannel, _LBInfo.FieldFlashingSignalName)

                If _LBFieldFlashingChannel Is Nothing Then
                    clsTopMostMessageBox.Show("FieldFlashingSignalName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

            If Not (_LBInfo.FieldFlashingTriggerSignalName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_LBFieldFlashingTriggerChannel, _LBInfo.FieldFlashingTriggerSignalName)

                If _LBFieldFlashingTriggerChannel Is Nothing Then
                    clsTopMostMessageBox.Show("FieldFlashingTriggerSignalName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If

            _PulseEnableInitialType = _LBInfo.PulseEnableInitialType
            _PulseStopInitialType = _LBInfo.PulseStopInitialType

                If Not (_LBInfo.FanEnableChannelPulseName Is Nothing) Then
                    CELProject.InitializeChannelCallBack(_LBFanEnableChannelPulse, _LBInfo.FanEnableChannelPulseName)

                    If _LBFanEnableChannelPulse Is Nothing Then
                    clsTopMostMessageBox.Show("FanEnableChannelPulseName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        _LBFanEnableChannelPulse.Value = New Global.KineticaRT.Values.Boolean(_PulseEnableInitialType)
                    End If
                End If

                If Not (_LBInfo.FanStopChannelPulseName Is Nothing) Then
                    CELProject.InitializeChannelCallBack(_LBFanStopChannelPulse, _LBInfo.FanStopChannelPulseName)

                    If _LBFanStopChannelPulse Is Nothing Then
                    clsTopMostMessageBox.Show("FanStopChannelPulseName does not correspond to a valid signal name", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        _LBFanStopChannelPulse.Value = New Global.KineticaRT.Values.Boolean(_PulseStopInitialType)
                    End If
                End If

                Me.AddHandlersLB()

                _LBDemandMemorized = 0

                _LoadBankStages = New System.Collections.Generic.List(Of LoadBankStage)

                SetLBVisibleEnabled(False)

                _ThreadFrequency = 1000.0 / _LBInfo.ThreadSpeedMsec

        End Sub
        Public Sub StartWorkerThread(ByVal ThreadDelay As Integer)

            _ThreadDelay = ThreadDelay
            'Thread Control
            _ExitThread = False
            _WorkerThread = New Thread(AddressOf WorkingThread)
            _WorkerThread.Priority = ThreadPriority.Normal
            _WorkerThread.Name = "LoadBankController_WorkerThread"
            _WorkerThread.Start()
        End Sub

        Private Sub WorkingThread()
            Try

                Do While Not _ExitThread
                    Thread.Sleep(_ThreadDelay)
                    If CELProject.IsFalse(CELProject.ARCHIVE_PLAY.Value) Then  'N'execute pas cette fonction si l'archive est en playback
                        Rampdown()
                        CheckLoadBankPhase()
                        OnLBMomentaryDOUT()
                    End If
                Loop
            Catch ex As Exception
                clsTopMostMessageBox.Show("LoadBankController.WorkingThread: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub


        '***********************
        '* AddHandlersLB()     *
        '***********************
        Private Sub AddHandlersLB()

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf Me.OnClose_Test
            End If

            If Not (_LBDemandChannel Is Nothing) Then
                AddHandler _LBDemandChannel.ValueChanged, AddressOf OnLBDemand
            End If

            If Not (_LBInitialDemandChannel Is Nothing) Then
                AddHandler _LBInitialDemandChannel.ValueChanged, AddressOf OnLBInitialDemand
            End If

            If Not (_LBDemandChannelIncrement Is Nothing) Then
                AddHandler _LBDemandChannelIncrement.ValueChanged, AddressOf OnDemandIncrementValidate
            End If

            If Not (_LBDownDemandChannel Is Nothing) Then
                AddHandler _LBDownDemandChannel.ValueChanged, AddressOf OnLBDemandCtrl
            End If

            If Not (_LBEnableChannel Is Nothing) Then
                AddHandler _LBEnableChannel.ValueChanged, AddressOf OnLBEnable
            End If

            If Not (_LBFanEnableBtnChannel Is Nothing) Then
                AddHandler _LBFanEnableBtnChannel.ValueChanged, AddressOf OnLBFanEnable
            End If

            If Not (_LBFieldFlashingTriggerChannel Is Nothing) Then
                AddHandler _LBFieldFlashingTriggerChannel.ValueChanged, AddressOf OnLBFlashingTrigger
            End If

            If Not (_LBUpDemandChannel Is Nothing) Then
                AddHandler _LBUpDemandChannel.ValueChanged, AddressOf OnLBDemandCtrl
            End If
        End Sub
        '**************************
        '* RemoveHandlersLB()     *
        '**************************
        Private Sub RemoveHandlersLB()

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf Me.OnClose_Test
            End If

            If Not (_LBDemandChannel Is Nothing) Then
                RemoveHandler _LBDemandChannel.ValueChanged, AddressOf OnLBDemand
            End If

            If Not (_LBInitialDemandChannel Is Nothing) Then
                RemoveHandler _LBInitialDemandChannel.ValueChanged, AddressOf OnLBInitialDemand
            End If

            If Not (_LBDemandChannelIncrement Is Nothing) Then
                RemoveHandler _LBDemandChannelIncrement.ValueChanged, AddressOf OnDemandIncrementValidate
            End If

            If Not (_LBDownDemandChannel Is Nothing) Then
                RemoveHandler _LBDownDemandChannel.ValueChanged, AddressOf OnLBDemandCtrl
            End If

            If Not (_LBEnableChannel Is Nothing) Then
                RemoveHandler _LBEnableChannel.ValueChanged, AddressOf OnLBEnable
            End If

            If Not (_LBFanEnableBtnChannel Is Nothing) Then
                RemoveHandler _LBFanEnableBtnChannel.ValueChanged, AddressOf OnLBFanEnable
            End If

            'If Not (_LBFanEnableChannel Is Nothing) Then
            '    RemoveHandler _LBFanEnableChannel.ValueChanged, AddressOf OnLBFanEnable
            'End If

            If Not (_LBFieldFlashingTriggerChannel Is Nothing) Then
                RemoveHandler _LBFieldFlashingTriggerChannel.ValueChanged, AddressOf OnLBFlashingTrigger
            End If

            If Not (_LBUpDemandChannel Is Nothing) Then
                RemoveHandler _LBUpDemandChannel.ValueChanged, AddressOf OnLBDemandCtrl
            End If
        End Sub
        '********************
        '* OnCloseTest()    *
        '********************
        Private Sub OnClose_Test(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If CELProject.IsTrue(newVal) Then

                    If Not (_LBEnableChannel Is Nothing) Then
                        _LBEnableChannel.Value = Values.False
                    End If

                    If Not (_LBDemandChannel Is Nothing) Then
                        _LBDemandChannel.Value = Values.Zero
                    End If

                    Me.RemoveHandlersLB()

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnClose_Test: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
        '*********************
        '* OnLBDemand()     *
        '*********************
        Private Sub OnLBDemand(ByVal sender As IChannel, ByVal newVal As Value)
            Dim i As Integer
            Dim Use(_LoadBankStages.Count) As Boolean
            Dim User_Demand As Double

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            'If the user delete de value in the input value and then hit enter we will have an exception, so assume miminum value in this case
            Try
                Dim TempValue As Integer = CInt(_LBDemandChannel.Value.ToDouble())
            Catch
                _LBDemandChannel.Value = Values.Zero
            End Try

            Try
                'validate that the demand is between limits
                If _LBDemandChannel.Value.ToDouble() < 0 Then
                    _LBDemandChannel.Value = Values.Zero
                ElseIf _LBDemandChannel.Value.ToDouble() > _LBInfo.Maximum Then
                    _LBDemandChannel.Value = New Global.KineticaRT.Values.Double(_LBInfo.Maximum)
                End If

                'the demand cannot have a fraction value, we round it
                If _LBDemandChannel.Value.ToDouble() <> System.Math.Round(_LBDemandChannel.Value.ToDouble()) Then
                    _LBDemandChannel.Value = New Global.KineticaRT.Values.Double(System.Math.Round(_LBDemandChannel.Value.ToDouble()))
                End If

                User_Demand = _LBDemandChannel.Value.ToDouble()

                For i = _LoadBankStages.Count - 1 To 0 Step -1
                    If User_Demand >= _LoadBankStages.Item(i).PowerKW Then
                        User_Demand = User_Demand - _LoadBankStages.Item(i).PowerKW
                        Use(i) = True
                    Else
                        Use(i) = False
                    End If
                Next

                User_Demand = 0

                For i = 0 To _LoadBankStages.Count - 1
                    If Use(i) = True Then
                        User_Demand += _LoadBankStages.Item(i).PowerKW
                    End If
                Next

                If User_Demand <> _LBDemandChannel.Value.ToDouble() Then
                    clsTopMostMessageBox.Show("Unable to set the specified power on the Load Bank !", "Load Bank Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    For i = 0 To _LoadBankStages.Count - 1
                        If Use(i) = True Then
                            _LoadBankStages.Item(i).Channel.Value = Values.True
                        Else
                            _LoadBankStages.Item(i).Channel.Value = Values.False
                        End If
                    Next
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnLBDemand: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '*********************
        '* OnLBInitialDemand()     *
        '*********************
        Private Sub OnLBInitialDemand(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            'If the user delete de value in the input value and then hit enter we will have an exception, so assume miminum value in this case
            Try
                Dim TempValue As Integer = CInt(_LBInitialDemandChannel.Value.ToDouble())
            Catch
                _LBInitialDemandChannel.Value = Values.Zero
            End Try

            Try
                'validate that the initial demand is between limits
                If _LBInitialDemandChannel.Value.ToDouble() < 0 Then
                    _LBInitialDemandChannel.Value = Values.Zero
                ElseIf _LBInitialDemandChannel.Value.ToDouble() > _LBInfo.Maximum Then
                    _LBInitialDemandChannel.Value = New Global.KineticaRT.Values.Double(_LBInfo.Maximum)
                End If

                'the initial demand cannot have a fraction value, we round it
                If _LBInitialDemandChannel.Value.ToDouble() <> System.Math.Round(_LBInitialDemandChannel.Value.ToDouble()) Then
                    _LBInitialDemandChannel.Value = New Global.KineticaRT.Values.Double(System.Math.Round(_LBInitialDemandChannel.Value.ToDouble()))
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnLBInitialDemand: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '*******************************
        '* OnDemandIncrementValidate() *
        '*******************************
        Private Sub OnDemandIncrementValidate(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            'If the user delete de value in the input value and then hit enter we will have an exception, so assume miminum value in this case
            Try
                Dim TempValue As Integer = CInt(_LBDemandChannelIncrement.Value.ToDouble())
            Catch
                _LBDemandChannelIncrement.Value = New Global.KineticaRT.Values.Double(1.0)
            End Try

            Try
                If _LBDemandChannelIncrement.Value.ToDouble() > _LBInfo.Maximum Then
                    _LBDemandChannelIncrement.Value = New Global.KineticaRT.Values.Double(_LBInfo.Maximum)
                ElseIf _LBDemandChannelIncrement.Value.ToDouble() < 1.0 Then
                    _LBDemandChannelIncrement.Value = New Global.KineticaRT.Values.Double(1.0)
                End If

                _LBInfo.Increment = CInt(_LBDemandChannelIncrement.Value.ToDouble())

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnDemandIncrementValidate: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
        '*************************
        '* OnLBFlashingTrigger() *
        '*************************
        Private Sub OnLBFlashingTrigger(ByVal sender As IChannel, ByVal newVal As Value)
            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If CELProject.IsTrue(newVal) And _LBInfo.FieldFlashingTimeMsec <> 0 Then

                    _LBFieldFlashingChannel.Value = Values.True

                    System.Threading.Thread.Sleep(_LBInfo.FieldFlashingTimeMsec)

                    _LBFieldFlashingChannel.Value = Values.False

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnLBFlashingTrigger: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub
        '**********************
        '* OnLBDemandCtrl() *
        '**********************
        Private Sub OnLBDemandCtrl(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If CELProject.IsTrue(newVal) Then
                If sender.Name = _LBUpDemandChannel.Name.ToString Then
                    _LBDemandChannel.Value = New Global.KineticaRT.Values.Double(_LBDemandChannel.Value.ToDouble() + _LBInfo.Increment)
                ElseIf sender.Name = _LBDownDemandChannel.Name.ToString Then
                    _LBDemandChannel.Value = New Global.KineticaRT.Values.Double(_LBDemandChannel.Value.ToDouble() - _LBInfo.Increment)
                End If
            End If
        End Sub
        '*****************
        '* OnLBEnable() *
        '*****************
        Private Sub OnLBEnable(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If CELProject.IsTrue(newVal) Then

                If Not (_LBInitialDemandChannel Is Nothing) Then
                    'Si on a une valeur initiale de demande, c'est elle qui a priorité
                    If _LBInitialDemandChannel.Value <> 0 Then
                        _LBDemandChannel.Value = _LBInitialDemandChannel.Value
                        'Sinon, c'est qu'on a pas de valeur initiale, on verifie si on a un memorize sur la demande
                    ElseIf _LBInfo.Memorize = True Then
                        _LBDemandChannel.Value = New Global.KineticaRT.Values.Double(_LBDemandMemorized)
                        'Sinon, la valeur de la demande sera egale a 0
                    End If
                End If

                If _LBInfo.Memorize = True Then
                    _LBDemandChannel.Value = New Global.KineticaRT.Values.Double(_LBDemandMemorized)
                End If

                SetLBVisibleEnabled(True)
            Else
                If _LBInfo.Memorize = True Then
                    _LBDemandMemorized = _LBDemandChannel.Value.ToDouble()
                End If

                SetLBVisibleEnabled(False)
                _RampDownDemand = _LBDemandChannel.Value.ToDouble()
            End If
        End Sub

        '*****************
        '* OnLBFanEnable() *
        '*****************
        Private Sub OnLBFanEnable(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If CELProject.IsTrue(newVal) Then
                'Enabling Load Bank Fan
                _LBFanEnableChannel.Value = Values.True
                'On s'assure de ne pas etre dans la logique de cooldown dans le cas qu'on aurait pu l'etre temporairement prealablement
                _LBFanDisable = False
            Else
                '
                _LBFanDisable = True
            End If
        End Sub

        'Use Delegate for Cross Thread Operation
        Private Delegate Sub SetLBVisibleEnabledDelegate(ByVal value As Boolean)

        '*************************
        '* SetLBVisibleEnabled() *
        '*************************
        Private Sub SetLBVisibleEnabled(ByVal value As Boolean)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If (Me._LBDisplay.InvokeRequired) Then
                Me._LBDisplay.BeginInvoke(New SetLBVisibleEnabledDelegate(AddressOf Me.SetLBVisibleEnabled), value)
            Else
                If Not (_LBDisplay Is Nothing) Then
                    _LBDisplay.Enabled = value
                End If
            End If
        End Sub


        Private _CurrentPhases(3) As Global.KineticaRT.Channels.INumericThresholdChannel
        Private _MaximumCurrentAllowedBetweenPhases As Integer
        Private _MaximumCurrent As Double
        Private _VoltagePhases(3) As Global.KineticaRT.Channels.INumericThresholdChannel
        Private _MaximumVoltageAllowedBetweenPhases As Integer
        Private _MaximumVoltage As Double

        '**********************
        '* AddCurrentPhases() *
        '**********************
        Public Sub AddCurrentPhases(ByVal CurrentPhaseA As String, ByVal CurrentPhaseB As String, ByVal CurrentPhaseC As String, ByVal MaximumCurrentAllowedBetweenPhases As Integer, Optional ByVal MaximumCurrent As Double = Double.PositiveInfinity)

            Dim channel As IChannel = Nothing

            CELProject.InitializeChannelCallBack(channel, CurrentPhaseA)
            _CurrentPhases(0) = CType(channel, Global.KineticaRT.Channels.INumericThresholdChannel)
            CELProject.InitializeChannelCallBack(channel, CurrentPhaseB)
            _CurrentPhases(1) = CType(channel, Global.KineticaRT.Channels.INumericThresholdChannel)
            CELProject.InitializeChannelCallBack(channel, CurrentPhaseC)
            _CurrentPhases(2) = CType(channel, Global.KineticaRT.Channels.INumericThresholdChannel)
            _MaximumCurrentAllowedBetweenPhases = MaximumCurrentAllowedBetweenPhases
            _MaximumCurrent = MaximumCurrent
        End Sub
        Public Sub AddVoltagePhases(ByVal VoltagePhaseAB As String, ByVal VoltagePhaseBC As String, ByVal VoltagePhaseCA As String, MaximumVoltageAllowedBetweenPhases As Integer, Optional ByVal MaximumVoltage As Double = Double.PositiveInfinity)

            Dim channel As IChannel = Nothing
            CELProject.InitializeChannelCallBack(channel, VoltagePhaseAB)
            _VoltagePhases(0) = CType(channel, Global.KineticaRT.Channels.INumericThresholdChannel)
            CELProject.InitializeChannelCallBack(channel, VoltagePhaseBC)
            _VoltagePhases(1) = CType(channel, Global.KineticaRT.Channels.INumericThresholdChannel)
            CELProject.InitializeChannelCallBack(channel, VoltagePhaseCA)
            _VoltagePhases(2) = CType(channel, Global.KineticaRT.Channels.INumericThresholdChannel)
            _MaximumVoltageAllowedBetweenPhases = MaximumVoltageAllowedBetweenPhases
            _MaximumVoltage = MaximumVoltage
        End Sub

        Private Sub CheckLoadBankPhase()

            Dim currentDiffAB As Boolean = (Abs(_CurrentPhases(0).Value.ToDouble - _CurrentPhases(1).Value.ToDouble) > _MaximumCurrentAllowedBetweenPhases)
            Dim currentDiffBC As Boolean = (Abs(_CurrentPhases(1).Value.ToDouble - _CurrentPhases(2).Value.ToDouble) > _MaximumCurrentAllowedBetweenPhases)
            Dim currentDiffCA As Boolean = (Abs(_CurrentPhases(2).Value.ToDouble - _CurrentPhases(0).Value.ToDouble) > _MaximumCurrentAllowedBetweenPhases)

            Dim voltageDiffAB As Boolean = (Abs(_VoltagePhases(0).Value.ToDouble - _VoltagePhases(1).Value.ToDouble) > _MaximumVoltageAllowedBetweenPhases)
            Dim voltageDiffBC As Boolean = (Abs(_VoltagePhases(1).Value.ToDouble - _VoltagePhases(2).Value.ToDouble) > _MaximumVoltageAllowedBetweenPhases)
            Dim voltageDiffCA As Boolean = (Abs(_VoltagePhases(2).Value.ToDouble - _VoltagePhases(0).Value.ToDouble) > _MaximumVoltageAllowedBetweenPhases)

            Dim currentProblemA As Boolean = False
            Dim currentProblemB As Boolean = False
            Dim currentProblemC As Boolean = False

            Dim voltageProblemA As Boolean = False
            Dim voltageProblemB As Boolean = False
            Dim voltageProblemC As Boolean = False

            If Not currentDiffAB And Not currentDiffBC And Not currentDiffCA Then '3 identical signals
                currentProblemA = False
                currentProblemB = False
                currentProblemC = False

            ElseIf Not currentDiffAB And Not currentDiffBC And currentDiffCA Then
                currentProblemA = True
                currentProblemC = True

            ElseIf Not currentDiffAB And currentDiffBC And Not currentDiffCA Then
                currentProblemB = True
                currentProblemC = True

            ElseIf Not currentDiffAB And currentDiffBC And currentDiffCA Then
                currentProblemC = True

            ElseIf currentDiffAB And Not currentDiffBC And Not currentDiffCA Then
                currentProblemA = True
                currentProblemB = True

            ElseIf currentDiffAB And Not currentDiffBC And currentDiffCA Then
                currentProblemA = True

            ElseIf currentDiffAB And currentDiffBC And Not currentDiffCA Then
                currentProblemB = True

            ElseIf currentDiffAB And currentDiffBC And currentDiffCA Then '3 differents signals
                currentProblemA = True
                currentProblemB = True
                currentProblemC = True
            End If


            If Not voltageDiffAB And Not voltageDiffBC And Not voltageDiffCA Then '3 identical signals
                voltageProblemA = False
                voltageProblemB = False
                voltageProblemC = False

            ElseIf Not voltageDiffAB And Not voltageDiffBC And voltageDiffCA Then
                voltageProblemA = True
                voltageProblemC = True

            ElseIf Not voltageDiffAB And voltageDiffBC And Not voltageDiffCA Then
                voltageProblemB = True
                voltageProblemC = True

            ElseIf Not voltageDiffAB And voltageDiffBC And voltageDiffCA Then
                voltageProblemC = True

            ElseIf voltageDiffAB And Not voltageDiffBC And Not voltageDiffCA Then
                voltageProblemA = True
                voltageProblemB = True

            ElseIf voltageDiffAB And Not voltageDiffBC And voltageDiffCA Then
                voltageProblemA = True

            ElseIf voltageDiffAB And voltageDiffBC And Not voltageDiffCA Then
                voltageProblemB = True

            ElseIf voltageDiffAB And voltageDiffBC And voltageDiffCA Then '3 differents signals
                voltageProblemA = True
                voltageProblemB = True
                voltageProblemC = True
            End If


            If currentProblemA Then
                _CurrentPhases(0).NormalColor = System.Drawing.Color.Red
            ElseIf _CurrentPhases(0).Value.ToDouble > _MaximumCurrent Then
                _CurrentPhases(0).NormalColor = System.Drawing.Color.Yellow
            Else
                _CurrentPhases(0).NormalColor = System.Drawing.Color.White
            End If

            If currentProblemB Then
                _CurrentPhases(1).NormalColor = System.Drawing.Color.Red
            ElseIf _CurrentPhases(1).Value.ToDouble > _MaximumCurrent Then
                _CurrentPhases(1).NormalColor = System.Drawing.Color.Yellow
            Else
                _CurrentPhases(1).NormalColor = System.Drawing.Color.White
            End If

            If currentProblemC Then
                _CurrentPhases(2).NormalColor = System.Drawing.Color.Red
            ElseIf _CurrentPhases(2).Value.ToDouble > _MaximumCurrent Then
                _CurrentPhases(2).NormalColor = System.Drawing.Color.Yellow
            Else
                _CurrentPhases(2).NormalColor = System.Drawing.Color.White
            End If

            If voltageProblemA Then
                _VoltagePhases(0).NormalColor = System.Drawing.Color.Red
            ElseIf _VoltagePhases(0).Value.ToDouble > _MaximumVoltage Then
                _VoltagePhases(0).NormalColor = System.Drawing.Color.Yellow
            Else
                _VoltagePhases(0).NormalColor = System.Drawing.Color.White
            End If

            If voltageProblemB Then
                _VoltagePhases(1).NormalColor = System.Drawing.Color.Red
            ElseIf _VoltagePhases(1).Value.ToDouble > _MaximumVoltage Then
                _VoltagePhases(1).NormalColor = System.Drawing.Color.Yellow
            Else
                _VoltagePhases(1).NormalColor = System.Drawing.Color.White
            End If

            If voltageProblemC Then
                _VoltagePhases(2).NormalColor = System.Drawing.Color.Red
            ElseIf _VoltagePhases(2).Value.ToDouble > _MaximumVoltage Then
                _VoltagePhases(2).NormalColor = System.Drawing.Color.Yellow
            Else
                _VoltagePhases(2).NormalColor = System.Drawing.Color.White
            End If

        End Sub






        Private _LoadBankStages As System.Collections.Generic.IList(Of LoadBankStage)
        '**************
        '* AddStage() *
        '**************
        Public Sub AddStage(ByVal PowerKW As Integer, ByVal ChannelName As String)
            Dim stage As LoadBankStage = New LoadBankStage()
            stage.PowerKW = PowerKW
            CELProject.InitializeChannelCallBack(stage.Channel, ChannelName)

            If Not (stage.Channel Is Nothing) Then
                stage.Channel.Value = Values.False
            End If

            _LoadBankStages.Add(stage)
        End Sub
        Private Class LoadBankStage
            Public Channel As IChannel
            Public PowerKW As Integer
        End Class
        '*****************************
        '* Rampdown()                *
        '* Used in the Engine Thread *
        '*****************************
        Private Sub Rampdown()

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If CELProject.IsFalse(_LBEnableChannel.Value) Then

                'Current Demand value is diminished by the delestagekWpersec value
                'EX : IF the load is 100kW and we want to ramp to 0 with a delestage of 10kW/sec, we need to diminish by
                '10 kW each time we go into the thread. Although, we go in the thread each 250ms, so we need to divide
                'Also the value by 4 (ThreadFrequency)
                'So : The First time : 100 - 10/4 = 97.5, Second time : 97.5 - 10/4 = 95, ...
                _RampDownDemand = _RampDownDemand - (_LBInfo.DelestageKwpersec / _ThreadFrequency)

                If _RampDownDemand <= 0.0 Then
                    _RampDownDemand = 0.0
                End If

                'The demand to the load bank has to be an integer value!!
                Dim LB_IntegerDemand As Integer = CInt(_RampDownDemand)

                If _LBDemandChannel.Value.ToDouble <> LB_IntegerDemand Then
                    _LBDemandChannel.Value = New Global.KineticaRT.Values.Double((LB_IntegerDemand))
                End If
                'Lorsqu'on desactive le fan, cette condition devient true et on entre dans la logique de cooldown qui lorsque terminee, ferme la fan.
                If _LBFanDisable = True Then

                    'Load Bank Cooldown logic
                    _FanCoolTimer = _FanCoolTimer + 1 'Adding 1 each time we enter the thread at each 250ms

                    If _FanCoolTimer >= (_LBInfo.FanCoolDownSeconds * _ThreadFrequency) Then  'Need to multiply by 4 because thread is at 250ms
                        If _LBFanEnableChannel.Value <> Values.False Then 'Pour éviter de faire des call a tous les 250 ms.  (Ca retire des log inutile pour debugger avec le TRACE actif)
                            _LBFanEnableChannel.Value = Values.False
                            'Maintenant que la fan a ete desactivée, on peut disabler le flag de cooldown : 
                            _LBFanDisable = False
                        End If
                    End If
                Else
                    _FanCoolTimer = 0
                End If

            End If
        End Sub

        '*****************
        '* OnLBMomentaryDOUT() *
        '*****************
        ' La Function Génère 2 DOUT Pulsés(Vrai ou Faux) d'une durée limitée permettant d'utiliser l'interface à un bouton latch de Kineticart pour les LB ayant des boutons momentanés.
        Private Sub OnLBMomentaryDOUT()

            If (Not (_LBFanEnableChannelPulse Is Nothing) And Not (_LBFanStopChannelPulse Is Nothing)) Then


                If CELProject.IsTrue(_LBFanEnableChannel.Value) Then

                    _StartDoutTimer = _StartDoutTimer + 1

                    If (_StartDoutTimer <= (_LBInfo.BtnLatchSeconds * _ThreadFrequency)) Then
                        'Le signal de Start load bank Fan pulsé est maintenu dans l'état Actif( non initial) pendant un lapse de temps.
                        _LBFanEnableChannelPulse.Value = New Global.KineticaRT.Values.Boolean(Not (_PulseEnableInitialType))
                    Else
                        'Le signal de Start Load bank Fan pulsé retour à sa valeur initial de défaut.
                        _LBFanEnableChannelPulse.Value = New Global.KineticaRT.Values.Boolean(_PulseEnableInitialType)
                    End If

                    _LBFanWasTrue = True

                Else
                    _LBFanEnableChannelPulse.Value = New Global.KineticaRT.Values.Boolean(_PulseEnableInitialType)
                    _StartDoutTimer = 0

                    If (_LBFanWasTrue) Then
                        ' Le signal de Stop load bank fan pulsé est mis Actif.
                        _LBFanStopChannelPulse.Value = New Global.KineticaRT.Values.Boolean(Not (_PulseStopInitialType))
                        _LBFanWasTrue = False
                        _StopDoutTimer = 0
                    End If
                End If

                If (_LBFanStopChannelPulse.Value = New Global.KineticaRT.Values.Boolean(Not (_PulseStopInitialType))) Then

                    _StopDoutTimer = _StopDoutTimer + 1

                    If (_StopDoutTimer >= (_LBInfo.BtnLatchSeconds * _ThreadFrequency)) Then
                        ' Le signal de Stop load bank fan pulsé retourne à sa valeur initiale apres un lapse de temps.
                        _LBFanStopChannelPulse.Value = New Global.KineticaRT.Values.Boolean(_PulseStopInitialType)
                        _StopDoutTimer = 0

                    End If
                End If
            End If  ' Null pointer check up loop
        End Sub

        '********************
        '* Dispose()    *
        '********************
        Public Sub Dispose() Implements IDisposable.Dispose

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

            For i As Integer = 0 To 2
                _CurrentPhases(i) = Nothing
                _VoltagePhases(i) = Nothing
            Next

            _LoadBankStages = Nothing
            GC.SuppressFinalize(Me)

        End Sub
    End Class



End Namespace
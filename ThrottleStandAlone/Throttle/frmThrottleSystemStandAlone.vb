Imports System.Runtime.InteropServices

Public Class frmThrottleSystemStandAlone

    Private SimulationMode As Boolean = False
    Private cRIOIPAdress As String = "192.168.1.201" 'Default value : use argument to specify the IP adress

    Private STMSession As CEL.STM.Session = New CEL.STM.Session()
    Private STMSessionReconnectTimer As System.Timers.Timer = New System.Timers.Timer(1000.0)
    Private STMSessionClosing As Boolean = False

    Private CurrentThrottleInstance As Byte = 0
    Private NumberOfServoInstances As Byte = 2 'Default value : use argument to specify the number of servos
    Private NumberofSupportedServoInstances As Byte = 0 'Will be populated by the number of tabs (PLA,CLA,MOR,...)

    Private CurrentReturnToIDLECommanded As Boolean = False
    Private NewCurrentReturnToIDLECommanded As Boolean = False

    Private InitialConsoleText As String = "CompactRIO is not powered/detected/connected : cannot connect to throttle system" & Environment.NewLine &
                                            "Make sure the device Is powered properly and available on the network" & Environment.NewLine &
                                            "If the drives have been unpowered while the throttle system was active, reboot the compactRIO, using NI MAX, for proper re-detection"

    Private Enum ThrottleInstanceName As Byte
        PLA = 0
        CLA = 1
        MOR = 2
    End Enum

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim i As Byte = 0
        Dim j As Byte = 0

        Dim args As String() = Environment.GetCommandLineArgs()

        If args.Length > 0 Then
            For Each arg As String In args
                If arg.ToLower = "/simulation" Then
                    SimulationMode = True
                ElseIf arg.ToLower.StartsWith("/ipadress") Then
                    cRIOIPAdress = arg.Split("=")(1)
                ElseIf arg.ToLower.StartsWith("/numberofservos") Then
                    NumberOfServoInstances = Convert.ToByte(arg.Split("=")(1))
                End If
            Next
        End If

        lblCopyright.Text = "© " & Date.Now.Year & " CEL Aerospace Test Equipment Ltd."
        txtPlaInstructions.Text = InitialConsoleText
        txtCLAInstructions.Text = InitialConsoleText
        txtMORInstructions.Text = InitialConsoleText

        'Show number of tab pages based on the number of servo/drive instances in the throttle system
        'Hide feedbacks (fault/warning) based on number of instances in the throttle system
        NumberofSupportedServoInstances = tabThrottles.TabPages.Count

        For i = NumberOfServoInstances To (NumberOfSupportedServoInstances - 1)
            j += 1
            tabThrottles.TabPages(NumberOfSupportedServoInstances - j).Parent = Nothing

            For Each FeedbackGroup As GroupBox In Me.Controls.OfType(Of GroupBox)
                If FeedbackGroup.Name.StartsWith("grpFeedbacksServoInstance" & Convert.ToString(NumberOfSupportedServoInstances - j)) Then
                    FeedbackGroup.Visible = False
                End If
            Next
        Next

        'Put invisible indicators of unused throttle instances in RunMode
        If NumberOfServoInstances = 1 Then
            dspRunModeCLA.Visible = False
            lblRunModeCLA.Visible = False
            dspRunModeMOR.Visible = False
            lblRunModeMOR.Visible = False
        ElseIf NumberOfServoInstances = 2 Then
            dspRunModeMOR.Visible = False
            lblRunModeMOR.Visible = False
        End If

        'Initialize Handlers
        AddHandler STMSession.Connected, AddressOf Me.OnSTMSessionConnected
        AddHandler STMSession.Closed, AddressOf Me.OnSTMSessionClosed
        AddHandler STMSession.Error, AddressOf Me.OnSTMSessionError
        AddHandler STMSession.MetaDataReceived, AddressOf Me.OnSTMSessionMetaDataReceived
        AddHandler STMSession.NewDataReceived, AddressOf Me.OnSTMSessionNewDataReceived
        AddHandler STMSessionReconnectTimer.Elapsed, AddressOf Me.OnSTMReconnectTimerElapsed

        'Initialize Values : TODO : Implement values per engine model in config file
        dspPLA_MIN_RIG_POS.Text = 35
        dspPLA_MAX_RIG_POS.Text = 75
        dspCLA_MIN_RIG_POS.Text = 0
        dspCLA_MAX_RIG_POS.Text = 50

        'Establish STM connection with cRIO
        If SimulationMode = False Then
            Me.ConnectSTMSession()
        Else
            indCRIO_CONNECTED.On = True
            lblSimulationMode.Visible = True

            'Simulation Mode : Enable all buttons on the screen
            For i = 0 To (NumberOfServoInstances - 1)
                For Each Grp As GroupBox In Me.tabThrottles.TabPages.Item(i).Controls.OfType(Of GroupBox)
                    For Each Btn As Button In Grp.Controls.OfType(Of Button)
                        Btn.Enabled = True
                    Next
                    For Each chkbx As CheckBox In Grp.Controls.OfType(Of CheckBox)
                        chkbx.Enabled = True
                    Next
                Next
            Next

        End If

    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        Dim QuitApplication As DialogResult = MessageBox.Show("Do you want to reset rigging and close the throttle system application ?", "Throttle System", MessageBoxButtons.YesNo)

        If QuitApplication = DialogResult.Yes Then
            DisconnectSTMSession()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub ConnectSTMSession()
        STMSessionClosing = False
        STMSession.Connect(cRIOIPAdress, "55555")
    End Sub

    Private Sub DisconnectSTMSession()
        Dim stmData(0) As Byte
        If STMSession.IsConnected Then
            STMSessionClosing = True
            ResetRigging()
            STMSession.SendMsg("Close", stmData)
            STMSession.Disconnect()
        End If
    End Sub

    Private Delegate Sub OnSTMReconnectTimerElapsedDelegate(sender As Object, e As System.Timers.ElapsedEventArgs)
    Private Sub OnSTMReconnectTimerElapsed(sender As Object, e As System.Timers.ElapsedEventArgs)
        If (Me.txtSystem.InvokeRequired) Then
            Me.txtSystem.Invoke(New OnSTMReconnectTimerElapsedDelegate(AddressOf Me.OnSTMReconnectTimerElapsed), sender, e)
        Else
            Try
                If SimulationMode = False Then
                    txtSystem.AppendText("STM Session Reconnect timer tick." & Environment.NewLine)
                    Me.ConnectSTMSession()
                End If
            Catch ex As Exception
                txtSystem.AppendText("OnSTMReconnectTimerElapsed: " & vbCrLf & vbCrLf &
                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
            End Try
        End If

    End Sub

    Private Delegate Sub OnSTMSesssionConnectedDelegate(sender As Object, e As System.EventArgs)
    Private Sub OnSTMSessionConnected(sender As Object, e As System.EventArgs)
        If (Me.txtSystem.InvokeRequired) Then
            Me.txtSystem.Invoke(New OnSTMSesssionConnectedDelegate(AddressOf Me.OnSTMSessionConnected), sender, e)
        Else
            txtSystem.AppendText("STM Session Connection in progress." & Environment.NewLine)

        End If

    End Sub

    Private Delegate Sub OnSTMSessionClosedDelegate(sender As Object, e As System.EventArgs)
    Private Sub OnSTMSessionClosed(sender As Object, e As System.EventArgs)
        If (Me.txtSystem.InvokeRequired) Then
            Me.txtSystem.Invoke(New OnSTMSessionClosedDelegate(AddressOf Me.OnSTMSessionClosed), sender, e)
        Else
            Try
                txtSystem.AppendText("STM Session Closed." & Environment.NewLine)
                If Not STMSessionClosing Then
                    STMSessionReconnectTimer.AutoReset = False
                    STMSessionReconnectTimer.Start()
                End If
                'Reset all indicators
                ResetUIIndicators()
                txtPlaInstructions.Text = InitialConsoleText
                txtCLAInstructions.Text = InitialConsoleText
                txtMORInstructions.Text = InitialConsoleText

                'Disable the Hardware Enable and Engage Buttons Buttons of each servo instance to Capture the Maximum Position
                For i = 0 To (NumberOfServoInstances - 1)
                    For Each Grp As GroupBox In Me.tabThrottles.TabPages.Item(i).Controls.OfType(Of GroupBox)
                        If Grp.Name.Contains("ServoControl") Then
                            For Each chkbx As CheckBox In Grp.Controls.OfType(Of CheckBox)
                                chkbx.Checked = False
                                Dim NewText As String = chkbx.Text.Replace("ON", "OFF")
                                chkbx.Text = NewText
                                chkbx.Enabled = False
                            Next
                        End If
                    Next
                Next

            Catch ex As Exception
                txtSystem.AppendText("OnSTMSessionClosed: " & vbCrLf & vbCrLf &
                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
            End Try
        End If

    End Sub

    Private Delegate Sub OnSTMSessionErrorDelegate(sender As Object, e As CEL.STM.ErrorEventArgs)
    Private Sub OnSTMSessionError(sender As Object, e As CEL.STM.ErrorEventArgs)

        If (Me.txtSystem.InvokeRequired) Then
            Me.txtSystem.Invoke(New OnSTMSessionErrorDelegate(AddressOf Me.OnSTMSessionError), sender, e)
        Else
            Try

                Select Case e.ErrorCode
                    Case System.Net.Sockets.SocketError.ConnectionRefused
                        txtSystem.AppendText("STM Session error: Connection refused." & Environment.NewLine)
                        STMSessionReconnectTimer.AutoReset = False
                        STMSessionReconnectTimer.Start()
                    Case System.Net.Sockets.SocketError.ConnectionReset
                        txtSystem.AppendText("STM Session error: Connection reset by the remote peer." & Environment.NewLine)
                        STMSessionReconnectTimer.AutoReset = False
                        STMSessionReconnectTimer.Start()
                    Case System.Net.Sockets.SocketError.TimedOut
                        txtSystem.AppendText("STM Session error: Connection timed out." & Environment.NewLine)
                        STMSessionReconnectTimer.AutoReset = False
                        STMSessionReconnectTimer.Start()
                    Case Else
                        txtSystem.AppendText("STM Session error: " & e.ErrorCode & Environment.NewLine)
                End Select
                'Reset all indicators
                ResetUIIndicators()
                txtPlaInstructions.Text = InitialConsoleText
                txtCLAInstructions.Text = InitialConsoleText
                txtMORInstructions.Text = InitialConsoleText

                'Disable the Hardware Enable and Engage Buttons Buttons of each servo instance to Capture the Maximum Position
                For i = 0 To (NumberOfServoInstances - 1)
                    For Each Grp As GroupBox In Me.tabThrottles.TabPages.Item(i).Controls.OfType(Of GroupBox)
                        If Grp.Name.Contains("ServoControl") Then
                            For Each chkbx As CheckBox In Grp.Controls.OfType(Of CheckBox)
                                chkbx.Checked = False
                                Dim NewText As String = chkbx.Text.Replace("ON", "OFF")
                                chkbx.Text = NewText
                                chkbx.Enabled = False
                            Next
                        End If
                    Next
                Next

            Catch ex As Exception
                txtSystem.AppendText("OnSTMSessionError: " & vbCrLf & vbCrLf &
                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
            End Try

        End If

    End Sub

    Private Delegate Sub OnSTMSessionMetaDataReceivedDelegate(sender As Object, e As System.EventArgs)
    Private Sub OnSTMSessionMetaDataReceived(sender As Object, e As System.EventArgs)
        If (Me.txtSystem.InvokeRequired) Then
            Me.txtSystem.Invoke(New OnSTMSessionMetaDataReceivedDelegate(AddressOf Me.OnSTMSessionMetaDataReceived), sender, e)
        Else
            Try

                txtSystem.AppendText("Received MetaData:" & Environment.NewLine)
                For Each metaName As String In STMSession.MetaData
                    txtSystem.AppendText(metaName & Environment.NewLine)
                Next

                indCRIO_CONNECTED.On = True

                Dim Instructions As String = "CompactRIO Is now connected" & Environment.NewLine &
                      "Press the Engage button to enable movement"

                txtPlaInstructions.Text = Instructions
                txtCLAInstructions.Text = Instructions
                txtMORInstructions.Text = Instructions

                'Enable the Hardware Enable and Engage Buttons Buttons of each servo instance to Capture the Maximum Position
                For i = 0 To (NumberOfServoInstances - 1)
                    For Each Grp As GroupBox In Me.tabThrottles.TabPages.Item(i).Controls.OfType(Of GroupBox)
                        For Each Btn As Button In Grp.Controls.OfType(Of Button)
                            Btn.Enabled = True
                        Next
                        For Each chkbx As CheckBox In Grp.Controls.OfType(Of CheckBox)
                            chkbx.Enabled = True
                        Next
                    Next
                Next

            Catch ex As Exception
                txtSystem.AppendText("OnSTMSessionMetaDataReceived: " & vbCrLf & vbCrLf &
                                "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
            End Try
        End If

    End Sub

    Private Delegate Sub OnSTMSessionNewDataReceivedDelegate(sender As Object, e As CEL.STM.NewDataReceivedEventArgs)
    Private Sub OnSTMSessionNewDataReceived(sender As Object, e As CEL.STM.NewDataReceivedEventArgs)

        If (Me.txtDrivesStatus.InvokeRequired) Then
            Me.txtDrivesStatus.Invoke(New OnSTMSessionNewDataReceivedDelegate(AddressOf Me.OnSTMSessionNewDataReceived), sender, e)
        Else

            Dim bCloseConnection As Boolean = False
            Dim position As Integer = 0
            Dim throttleInstance As ThrottleInstanceName
            Dim Active As Boolean
            Dim Engaged As Boolean
            Dim MaxPositionStored As Boolean
            Dim MinPositionStored As Boolean
            Dim RiggingDone As Boolean
            Dim ComputeDone As Boolean
            Dim MaxStopPositionStored As Boolean
            Dim MinStopPositionStored As Boolean
            Dim IdlePositionStored As Boolean
            Dim LeverPosition As Double
            Dim ServoPosition As Double
            Dim FcuServoAngle As Double
            Dim Faults() As UInt32
            Dim Warnings() As UInt32
            Dim ReturnToIdleActivated As Boolean
            Dim ThrottleRearming As Boolean
            Dim Test1 As Boolean

            Select Case e.MetaName
                Case "Throttle.Status"
                    Try

                        throttleInstance = CEL.STM.Convert.ByteUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Byte.MaxValue.GetType)
                        Active = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Byte.MaxValue.GetType)
                        Engaged = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Byte.MaxValue.GetType)
                        MaxPositionStored = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Byte.MaxValue.GetType)
                        MinPositionStored = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Byte.MaxValue.GetType)
                        RiggingDone = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Byte.MaxValue.GetType)
                        ComputeDone = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Byte.MaxValue.GetType)
                        MaxStopPositionStored = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Byte.MaxValue.GetType)
                        MinStopPositionStored = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Byte.MaxValue.GetType)
                        IdlePositionStored = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Byte.MaxValue.GetType)
                        LeverPosition = CEL.STM.Convert.DblUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Double.MaxValue.GetType)
                        ServoPosition = CEL.STM.Convert.DblUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Double.MaxValue.GetType)
                        FcuServoAngle = CEL.STM.Convert.DblUnflatten(e.Data, position)
                        position += Marshal.SizeOf(Double.MaxValue.GetType)
                        Faults = CEL.STM.Convert.UIntArrUnflatten(e.Data, position)
                        Warnings = CEL.STM.Convert.UIntArrUnflatten(e.Data, position)

                        CurrentReturnToIDLECommanded = NewCurrentReturnToIDLECommanded

                        If position < e.Data.Count - 1 Then
                            NewCurrentReturnToIDLECommanded = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                            position += Marshal.SizeOf(Byte.MaxValue.GetType)
                            ThrottleRearming = CEL.STM.Convert.BoolUnflatten(e.Data, position)
                            position = +Marshal.SizeOf(Byte.MaxValue.GetType)
                        End If

                        If CurrentReturnToIDLECommanded <> NewCurrentReturnToIDLECommanded Then
                            OnReturnToIdleCmd(NewCurrentReturnToIDLECommanded, 0)
                            OnReturnToIdleCmd(NewCurrentReturnToIDLECommanded, 1)
                            OnReturnToIdleCmd(NewCurrentReturnToIDLECommanded, 2)

                            indPLA_ReturnToIDLECommanded.On = NewCurrentReturnToIDLECommanded
                            indCLA_ReturnToIDLECommanded.On = NewCurrentReturnToIDLECommanded
                        End If

                    Catch ex As Exception
                        txtDrivesStatus.AppendText("OnSTMSessionNewDataReceived: Unexpected error while Unflatting message: " + ex.Message & Environment.NewLine)
                        bCloseConnection = True
                    End Try

                    If Not bCloseConnection Then
                        Try

                            Select Case throttleInstance
                                Case ThrottleInstanceName.PLA
                                    indPLA_COMPUTE_DONE.On = If(ComputeDone, True, False)
                                    indPLA_IDLE_POS_DONE.On = If(IdlePositionStored, True, False)
                                    dspPLA_LEVER_POS.Text = LeverPosition
                                    indPLA_MAX_POS_DONE.On = If(MaxPositionStored, True, False)
                                    indPLA_MAX_STOP_POS_DONE.On = If(MaxStopPositionStored, True, False)
                                    indPLA_MIN_POS_DONE.On = If(MinPositionStored, True, False)
                                    indPLA_MIN_STOP_POS_DONE.On = If(MinStopPositionStored, True, False)
                                    indPLA_RIGGING_DONE.On = If(RiggingDone, True, False)
                                    dspPLA_SERVO_READING.Text = ServoPosition
                                    dspPLA_SERVO_ANGLE_READING.Text = FcuServoAngle
                                    dspRunModePLA.Text = FcuServoAngle
                                    Dim faultIndex As Integer = 0
                                    Dim NumberOfFaults As Integer = 0
                                    For Each faultCode As UInt32 In Faults
                                        faultIndex += 1
                                        If faultCode <> 0 Then
                                            txtDrivesStatus.AppendText("Drive(" & throttleInstance.ToString() &
                                                                           ") reported fault" & faultIndex.ToString() &
                                                                           "= " & faultCode.ToString() & Environment.NewLine)
                                            NumberOfFaults += 1
                                        End If
                                    Next

                                    If NumberOfFaults > 0 Then
                                        indCRIO_PLA_DRIVE_FAULT.On = True
                                    Else
                                        indCRIO_PLA_DRIVE_FAULT.On = False
                                    End If

                                    Dim warningIndex As Integer = 0
                                    Dim NumberOfWarnings As Integer = 0
                                    For Each warningCode As UInt32 In Warnings
                                        warningIndex += 1
                                        If warningCode <> 0 Then
                                            txtDrivesStatus.AppendText("Drive(" & throttleInstance.ToString() &
                                                                           ") reported warning" & warningIndex.ToString() &
                                                                           "= " & warningCode.ToString() & Environment.NewLine)
                                            NumberOfWarnings += 1
                                        End If
                                    Next

                                    If NumberOfWarnings > 0 Then
                                        indCRIO_PLA_DRIVE_WARNING.On = True
                                    Else
                                        indCRIO_PLA_DRIVE_WARNING.On = False
                                    End If

                                    indPLA_ThrottleRearming.On = If(ThrottleRearming, True, False)

                                Case ThrottleInstanceName.CLA
                                    indCLA_COMPUTE_DONE.On = If(ComputeDone, True, False)
                                    indCLA_IDLE_POS_DONE.On = If(IdlePositionStored, True, False)
                                    dspCLA_LEVER_POS.Text = LeverPosition
                                    indCLA_MAX_POS_DONE.On = If(MaxPositionStored, True, False)
                                    indCLA_MAX_STOP_POS_DONE.On = If(MaxStopPositionStored, True, False)
                                    indCLA_MIN_POS_DONE.On = If(MinPositionStored, True, False)
                                    indCLA_MIN_STOP_POS_DONE.On = If(MinStopPositionStored, True, False)
                                    indCLA_RIGGING_DONE.On = If(RiggingDone, True, False)
                                    dspCLA_SERVO_READING.Text = ServoPosition
                                    dspCLA_SERVO_ANGLE_READING.Text = FcuServoAngle
                                    dspRunModeCLA.Text = FcuServoAngle
                                    Dim faultIndex As Integer = 0
                                    Dim NumberOfFaults As Integer = 0
                                    For Each faultCode As UInt32 In Faults
                                        faultIndex += 1
                                        If faultCode <> 0 Then
                                            txtDrivesStatus.AppendText("Drive(" & throttleInstance.ToString() &
                                                                           ") reported fault" & faultIndex.ToString() &
                                                                           "= " & faultCode.ToString() & Environment.NewLine)
                                            NumberOfFaults += 1
                                        End If
                                    Next

                                    If NumberOfFaults > 0 Then
                                        indCRIO_CLA_DRIVE_FAULT.On = True
                                    Else
                                        indCRIO_CLA_DRIVE_FAULT.On = False
                                    End If

                                    Dim warningIndex As Integer = 0
                                    Dim NumberOfWarnings As Integer = 0
                                    For Each warningCode As UInt32 In Warnings
                                        warningIndex += 1
                                        If warningCode <> 0 Then
                                            txtDrivesStatus.AppendText("Drive(" & throttleInstance.ToString() &
                                                                           ") reported warning" & warningIndex.ToString() &
                                                                           "= " & warningCode.ToString() & Environment.NewLine)
                                            NumberOfWarnings += 1
                                        End If
                                    Next

                                    If NumberOfWarnings > 0 Then
                                        indCRIO_CLA_DRIVE_WARNING.On = True
                                    Else
                                        indCRIO_CLA_DRIVE_WARNING.On = False
                                    End If

                                    indCLA_ThrottleRearming.On = If(ThrottleRearming, True, False)

                                Case ThrottleInstanceName.MOR
                                    indMOR_COMPUTE_DONE.On = If(ComputeDone, True, False)
                                    indMOR_IDLE_POS_DONE.On = If(IdlePositionStored, True, False)
                                    dspMOR_LEVER_POS.Text = LeverPosition
                                    indMOR_MAX_POS_DONE.On = If(MaxPositionStored, True, False)
                                    indMOR_MAX_STOP_POS_DONE.On = If(MaxStopPositionStored, True, False)
                                    indMOR_MIN_POS_DONE.On = If(MinPositionStored, True, False)
                                    indMOR_MIN_STOP_POS_DONE.On = If(MinStopPositionStored, True, False)
                                    indMOR_RIGGING_DONE.On = If(RiggingDone, True, False)
                                    dspMOR_SERVO_READING.Text = ServoPosition
                                    dspMOR_SERVO_ANGLE_READING.Text = FcuServoAngle
                                    dspRunModeMOR.Text = FcuServoAngle
                                    Dim faultIndex As Integer = 0
                                    Dim NumberOfFaults As Integer = 0
                                    For Each faultCode As UInt32 In Faults
                                        faultIndex += 1
                                        If faultCode <> 0 Then
                                            txtDrivesStatus.AppendText("Drive(" & throttleInstance.ToString() &
                                                                           ") reported fault" & faultIndex.ToString() &
                                                                           "= " & faultCode.ToString() & Environment.NewLine)
                                            NumberOfFaults += 1
                                        End If
                                    Next

                                    If NumberOfFaults > 0 Then
                                        indCRIO_MOR_DRIVE_FAULT.On = True
                                    Else
                                        indCRIO_MOR_DRIVE_FAULT.On = False
                                    End If

                                    Dim warningIndex As Integer = 0
                                    Dim NumberOfWarnings As Integer = 0
                                    For Each warningCode As UInt32 In Warnings
                                        warningIndex += 1
                                        If warningCode <> 0 Then
                                            txtDrivesStatus.AppendText("Drive(" & throttleInstance.ToString() &
                                                                           ") reported warning" & warningIndex.ToString() &
                                                                           "= " & warningCode.ToString() & Environment.NewLine)
                                            NumberOfWarnings += 1
                                        End If
                                    Next

                                    If NumberOfWarnings > 0 Then
                                        indCRIO_MOR_DRIVE_WARNING.On = True
                                    Else
                                        indCRIO_MOR_DRIVE_WARNING.On = False
                                    End If

                                Case Else
                                    txtDrivesStatus.AppendText("Unexpected Throttle Instance" & Environment.NewLine)
                            End Select

                        Catch ex As Exception
                            txtSystem.AppendText("OnSTMSessionMetaDataReceived: " & vbCrLf & vbCrLf &
                                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
                        End Try
                    Else
                        STMSession.Disconnect()
                    End If

                Case Else
                    txtDrivesStatus.AppendText("Received Unexpected message: " & e.MetaName & Environment.NewLine)
            End Select
        End If

    End Sub

    Private Sub OnComputeBtn(sender As Object, e As System.EventArgs) Handles btnPLA_COMPUTE.Click, btnCLA_COMPUTE.Click, btnMOR_COMPUTE.Click
        Dim throttleInstance As ThrottleInstanceName
        Try
            If indCRIO_CONNECTED.On = True Then

                Dim ComputeInstructions As String = "Lever is configured and ready to use." & Environment.NewLine &
                                  "Rigging Reset must be pressed to proceed again with the rigging process" & Environment.NewLine &
                                  "Proceed to store the IDLE and Max/Min Stop positions" & Environment.NewLine &
                                  "Those safety steps are very important to protect the test cell equipment and the engine."

                If sender Is btnPLA_COMPUTE Then
                    throttleInstance = ThrottleInstanceName.PLA
                    txtPlaInstructions.Text = ComputeInstructions
                    btnPLA_STORE_MAX_ANGLE.Enabled = True
                    chkPLA_ENGAGE.Enabled = False
                    btnPLA_STORE_MAX_ANGLE.Enabled = False
                    btnPLA_STORE_MIN_ANGLE.Enabled = False
                    btnPLA_COMPUTE.Enabled = False
                    btnPLA_RESET.Enabled = True
                    btnPLA_STORE_MAX_STOP_ANGLE.Enabled = True
                    btnPLA_STORE_MIN_STOP_ANGLE.Enabled = True
                    btnPLA_STORE_IDLE_ANGLE.Enabled = True

                ElseIf sender Is btnCLA_COMPUTE Then
                    throttleInstance = ThrottleInstanceName.CLA
                    txtCLAInstructions.Text = ComputeInstructions
                    btnCLA_STORE_MAX_ANGLE.Enabled = True
                    chkCLA_ENGAGE.Enabled = False
                    btnCLA_STORE_MAX_ANGLE.Enabled = False
                    btnCLA_STORE_MIN_ANGLE.Enabled = False
                    btnCLA_COMPUTE.Enabled = False
                    btnCLA_RESET.Enabled = True
                    btnCLA_STORE_MAX_STOP_ANGLE.Enabled = True
                    btnCLA_STORE_MIN_STOP_ANGLE.Enabled = True
                    btnCLA_STORE_IDLE_ANGLE.Enabled = True

                ElseIf sender Is btnMOR_COMPUTE Then
                    throttleInstance = ThrottleInstanceName.MOR
                    txtMORInstructions.Text = ComputeInstructions
                    btnMOR_STORE_MAX_ANGLE.Enabled = True
                    chkMOR_ENGAGE.Enabled = False
                    btnMOR_STORE_MAX_ANGLE.Enabled = False
                    btnMOR_STORE_MIN_ANGLE.Enabled = False
                    btnMOR_COMPUTE.Enabled = False
                    btnMOR_RESET.Enabled = True
                    btnMOR_STORE_MAX_STOP_ANGLE.Enabled = True
                    btnMOR_STORE_MIN_STOP_ANGLE.Enabled = True
                    btnMOR_STORE_IDLE_ANGLE.Enabled = True

                Else
                    txtSystem.AppendText("OnComputeBtn: Unexpected sender" & Environment.NewLine)
                End If

                Dim msg As String = CByte(throttleInstance).ToString()

                Dim msgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))
                msgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
                CEL.STM.Convert.Flatten(msg, msgData)
                STMSession.SendMsg("Throttle.Compute", msgData)
            End If

        Catch ex As Exception
            txtSystem.AppendText("OnComputeBtn(" & throttleInstance.ToString() & "): " & vbCrLf & vbCrLf &
                            "Error message: " & Environment.NewLine)
        End Try
    End Sub

    Private Sub OnEngageBtn(sender As Object, e As System.EventArgs) Handles chkPLA_ENGAGE.Click, chkCLA_ENGAGE.Click, chkMOR_ENGAGE.Click
        Dim msgName As String = ""
        Dim throttleInstance As ThrottleInstanceName
        Dim EngageButtonDialogBox As DialogResult
        Try

            If DirectCast(sender, CheckBox).Checked = True Then
                EngageButtonDialogBox = MessageBox.Show("Are you sure you want to engage movement?", "Throttle System", MessageBoxButtons.YesNo)
                If EngageButtonDialogBox = DialogResult.No Then
                    DirectCast(sender, CheckBox).Checked = False
                    Exit Sub
                End If
            End If

            If indCRIO_CONNECTED.On = True Then

                Dim EngageInstructions As String = "Set the Maximum/Minimum angle position on the engine using the associated lever." & Environment.NewLine &
                                  "Use the Disengage/Engage button to pull back the lever in order to pursue displacement." & Environment.NewLine &
                                  "When BOTH the lever AND the servo motor have reached the required angle position : REMOVE engage button and press the store button"


                If sender Is chkPLA_ENGAGE Then
                    throttleInstance = ThrottleInstanceName.PLA
                    txtPlaInstructions.Text = EngageInstructions
                    btnPLA_STORE_MAX_ANGLE.Enabled = True
                ElseIf sender Is chkCLA_ENGAGE Then
                    throttleInstance = ThrottleInstanceName.CLA
                    txtCLAInstructions.Text = EngageInstructions
                    btnCLA_STORE_MAX_ANGLE.Enabled = True
                ElseIf sender Is chkMOR_ENGAGE Then
                    throttleInstance = ThrottleInstanceName.MOR
                    txtMORInstructions.Text = EngageInstructions
                    btnMOR_STORE_MIN_ANGLE.Enabled = True
                Else
                    txtSystem.AppendText("Throttle_Sequence.OnEngageBtn: Unexpected sender" & Environment.NewLine)
                End If

                If DirectCast(sender, CheckBox).Checked = True Then
                    DirectCast(sender, CheckBox).Text = "ENGAGE ON"
                    msgName = "Throttle.Engage"
                Else
                    DirectCast(sender, CheckBox).Text = "ENGAGE OFF"
                    msgName = "Throttle.Disengage"
                End If
                Dim msg As String = CByte(throttleInstance).ToString()

                Dim msgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))
                msgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
                CEL.STM.Convert.Flatten(msg, msgData)
                STMSession.SendMsg(msgName, msgData)
            End If
        Catch ex As Exception
            txtSystem.AppendText("OnEngageBtn: " & vbCrLf & vbCrLf &
                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
        End Try
    End Sub

    Private Sub OnResetBtn(sender As Object, e As System.EventArgs) Handles btnPLA_RESET.Click, btnCLA_RESET.Click, btnMOR_RESET.Click
        Dim throttleInstance As ThrottleInstanceName
        Try

            If indCRIO_CONNECTED.On = True Then

                Dim ResetInstructions As String = "Set the Maximum/Minimum angle position on the engine using the associated lever." & Environment.NewLine &
                                  "Use the Disengage/Engage button to pull back the lever in order to pursue displacement." & Environment.NewLine &
                                  "When BOTH the lever AND the servo motor have reached the required angle position : REMOVE engage button and press the store button"

                If sender Is btnPLA_RESET Then
                    throttleInstance = ThrottleInstanceName.PLA
                    chkPLA_ENGAGE.Enabled = True
                    txtPlaInstructions.Text = ResetInstructions
                    btnPLA_STORE_MAX_ANGLE.Enabled = True
                    btnPLA_STORE_MIN_ANGLE.Enabled = True
                    btnPLA_STORE_IDLE_ANGLE.Enabled = True
                    btnPLA_RESET.Enabled = True
                    btnPLA_STORE_MAX_STOP_ANGLE.Enabled = True
                    btnPLA_STORE_MIN_STOP_ANGLE.Enabled = True
                    btnPLA_STORE_IDLE_ANGLE.Enabled = True
                    dspPLA_MAX_STOP_POS.Text = ""
                    dspPLA_IDLE_POS.Text = ""
                    dspPLA_MIN_STOP_POS.Text = ""

                ElseIf sender Is btnCLA_RESET Then
                    throttleInstance = ThrottleInstanceName.CLA
                    chkCLA_ENGAGE.Enabled = True
                    txtCLAInstructions.Text = ResetInstructions
                    btnCLA_STORE_MAX_ANGLE.Enabled = True
                    btnCLA_STORE_MIN_ANGLE.Enabled = True
                    btnCLA_STORE_IDLE_ANGLE.Enabled = True
                    btnCLA_RESET.Enabled = True
                    btnCLA_STORE_MAX_STOP_ANGLE.Enabled = True
                    btnCLA_STORE_MIN_STOP_ANGLE.Enabled = True
                    btnCLA_STORE_IDLE_ANGLE.Enabled = True
                    dspCLA_MAX_STOP_POS.Text = ""
                    dspCLA_IDLE_POS.Text = ""
                    dspCLA_MIN_STOP_POS.Text = ""

                ElseIf sender Is btnMOR_RESET Then
                    throttleInstance = ThrottleInstanceName.MOR
                    chkMOR_ENGAGE.Enabled = True
                    txtMORInstructions.Text = ResetInstructions
                    btnMOR_STORE_MAX_ANGLE.Enabled = True
                    btnMOR_STORE_MIN_ANGLE.Enabled = True
                    btnMOR_STORE_IDLE_ANGLE.Enabled = True
                    btnMOR_RESET.Enabled = True
                    btnMOR_STORE_MAX_STOP_ANGLE.Enabled = True
                    btnMOR_STORE_MIN_STOP_ANGLE.Enabled = True
                    btnMOR_STORE_IDLE_ANGLE.Enabled = True
                    dspMOR_MAX_STOP_POS.Text = ""
                    dspMOR_IDLE_POS.Text = ""
                    dspMOR_MIN_STOP_POS.Text = ""
                Else
                    txtSystem.AppendText("Throttle_Sequence.OnResetBtn: Unexpected sender" & Environment.NewLine)
                End If

                Dim msg = CByte(throttleInstance).ToString()

                Dim msgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))
                msgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
                CEL.STM.Convert.Flatten(msg, msgData)
                STMSession.SendMsg("Throttle.ResetRigging", msgData)

            End If

        Catch ex As Exception
            txtSystem.AppendText("OnResetBtn: " & vbCrLf & vbCrLf &
                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
        End Try
    End Sub

    Private Delegate Sub ResetRiggingDelegate()
    Private Sub ResetRigging()

        If (Me.txtSystem.InvokeRequired) Then
            Me.txtSystem.BeginInvoke(New ResetRiggingDelegate(AddressOf Me.ResetRigging))
        Else

            Dim PLAInstance As ThrottleInstanceName = ThrottleInstanceName.PLA
            Dim CLAInstance As ThrottleInstanceName = ThrottleInstanceName.CLA
            Dim MORInstance As ThrottleInstanceName = ThrottleInstanceName.MOR

            Try

                If indCRIO_CONNECTED.On = True Then

                    Dim plamsg = CByte(PLAInstance).ToString()
                    Dim plamsgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))
                    plamsgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
                    CEL.STM.Convert.Flatten(plamsg, plamsgData)
                    STMSession.SendMsg("Throttle.ResetRigging", plamsgData)

                    Dim clamsg = CByte(CLAInstance).ToString()
                    Dim clamsgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))
                    clamsgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
                    CEL.STM.Convert.Flatten(clamsg, clamsgData)
                    STMSession.SendMsg("Throttle.ResetRigging", clamsgData)

                    Dim mormsg = CByte(MORInstance).ToString()
                    Dim mormsgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))
                    mormsgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
                    CEL.STM.Convert.Flatten(mormsg, mormsgData)
                    STMSession.SendMsg("Throttle.ResetRigging", mormsgData)

                End If

            Catch ex As Exception
                txtSystem.AppendText("ResetRigging: " & vbCrLf & vbCrLf &
                                "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
            End Try
        End If

    End Sub

    Private Sub OnStoreIdleAngleBtn(sender As Object, e As System.EventArgs) Handles btnPLA_STORE_IDLE_ANGLE.Click, btnCLA_STORE_IDLE_ANGLE.Click, btnMOR_STORE_IDLE_ANGLE.Click
        Dim angle As String = "0.0"
        Dim lever As String = "0.0"
        Dim throttleInstance As ThrottleInstanceName

        Try

            If indCRIO_CONNECTED.On = True Then

                Dim StoreIdleInstructions As String = "IDLE Position is Stored" & Environment.NewLine &
                                  "The servo motor will be automatically set at this position in case of an emergency stop" & Environment.NewLine &
                                  "Proceed to store the Max/Min Stop position (if not done yet)" & Environment.NewLine &
                                  "This safety step is very important to protect the test cell equipment and the engine."

                If sender Is btnPLA_STORE_IDLE_ANGLE Then
                    dspPLA_IDLE_POS.Text = dspPLA_SERVO_ANGLE_READING.Text
                    angle = dspPLA_IDLE_POS.Text
                    lever = dspPLA_LEVER_POS.Text
                    throttleInstance = ThrottleInstanceName.PLA
                    txtPlaInstructions.Text = StoreIdleInstructions

                ElseIf sender Is btnCLA_STORE_IDLE_ANGLE Then
                    dspCLA_IDLE_POS.Text = dspCLA_SERVO_ANGLE_READING.Text
                    angle = dspCLA_IDLE_POS.Text
                    lever = dspCLA_LEVER_POS.Text
                    throttleInstance = ThrottleInstanceName.CLA
                    txtCLAInstructions.Text = StoreIdleInstructions

                ElseIf sender Is btnMOR_STORE_IDLE_ANGLE Then
                    dspMOR_IDLE_POS.Text = dspMOR_SERVO_ANGLE_READING.Text
                    angle = dspMOR_IDLE_POS.Text
                    lever = dspMOR_LEVER_POS.Text
                    throttleInstance = ThrottleInstanceName.MOR
                    txtMORInstructions.Text = StoreIdleInstructions

                Else
                    txtSystem.AppendText("Throttle_Sequence.OnStoreIdleAngleBtn: Unexpected sender" & Environment.NewLine)
                End If

                Dim msg As String = CByte(throttleInstance).ToString & ";" & angle & ";" & lever
                Dim msgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))

                msgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
                CEL.STM.Convert.Flatten(msg, msgData)
                STMSession.SendMsg("Throttle.StoreIdlePosition", msgData)

            End If

        Catch ex As Exception
            txtSystem.AppendText("OnStoreIdleAngleBtn(" & throttleInstance.ToString() & "): " & vbCrLf & vbCrLf &
                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
        End Try
    End Sub

    Private Sub OnStoreMaxMinAngleBtn(sender As Object, e As System.EventArgs) Handles btnPLA_STORE_MAX_ANGLE.Click, btnPLA_STORE_MIN_ANGLE.Click, btnCLA_STORE_MAX_ANGLE.Click, btnCLA_STORE_MIN_ANGLE.Click, btnMOR_STORE_MAX_ANGLE.Click, btnMOR_STORE_MIN_ANGLE.Click
        Dim angle As String = "0.0"
        Dim msgName As String = ""
        Dim throttleInstance As ThrottleInstanceName

        Try
            If indCRIO_CONNECTED.On = True Then

                Dim StoreMaxInstructions As String = "Max Angle Position is Stored" & Environment.NewLine &
                            "If both angles are stored, press the Compute button to activate the configured positioning."

                Dim StoreMinInstructions As String = "Min Angle Position is stored." & Environment.NewLine &
                             "If both angles are stored, press the Compute button to activate the configured positioning."

                If sender Is btnPLA_STORE_MAX_ANGLE Then
                    angle = dspPLA_MAX_RIG_POS.Text
                    msgName = "Throttle.StoreMaxPosition"
                    throttleInstance = ThrottleInstanceName.PLA
                    txtPlaInstructions.Text = StoreMaxInstructions
                    btnPLA_STORE_MIN_ANGLE.Enabled = True

                ElseIf sender Is btnPLA_STORE_MIN_ANGLE Then
                    angle = dspPLA_MIN_RIG_POS.Text
                    msgName = "Throttle.StoreMinPosition"
                    throttleInstance = ThrottleInstanceName.PLA
                    txtPlaInstructions.Text = StoreMinInstructions
                    btnPLA_COMPUTE.Enabled = True

                ElseIf sender Is btnCLA_STORE_MAX_ANGLE Then
                    angle = dspCLA_MAX_RIG_POS.Text
                    msgName = "Throttle.StoreMaxPosition"
                    throttleInstance = ThrottleInstanceName.CLA
                    txtCLAInstructions.Text = StoreMaxInstructions
                    btnCLA_STORE_MIN_ANGLE.Enabled = True

                ElseIf sender Is btnCLA_STORE_MIN_ANGLE Then
                    angle = dspCLA_MIN_RIG_POS.Text
                    msgName = "Throttle.StoreMinPosition"
                    throttleInstance = ThrottleInstanceName.CLA
                    txtCLAInstructions.Text = StoreMinInstructions
                    btnCLA_COMPUTE.Enabled = True

                ElseIf sender Is btnMOR_STORE_MAX_ANGLE Then
                    angle = dspMOR_MAX_RIG_POS.Text
                    msgName = "Throttle.StoreMaxPosition"
                    throttleInstance = ThrottleInstanceName.MOR
                    txtMORInstructions.Text = StoreMaxInstructions
                    btnMOR_STORE_MIN_ANGLE.Enabled = True

                ElseIf sender Is btnMOR_STORE_MIN_ANGLE Then
                    angle = dspMOR_MIN_RIG_POS.Text
                    msgName = "Throttle.StoreMinPosition"
                    throttleInstance = ThrottleInstanceName.MOR
                    txtMORInstructions.Text = StoreMinInstructions
                    btnMOR_COMPUTE.Enabled = True
                Else
                    txtSystem.AppendText("Throttle_Sequence.OnStoreMaxMinAngleBtn: Unexpected sender" & Environment.NewLine)
                End If

                Dim msg As String = CByte(throttleInstance).ToString & ";" & angle

                Dim msgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))
                msgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
                CEL.STM.Convert.Flatten(msg, msgData)
                STMSession.SendMsg(msgName, msgData)

            End If

        Catch ex As Exception
            txtSystem.AppendText("OnStoreMaxMinAngleBtn(" & throttleInstance.ToString() & "): " & vbCrLf & vbCrLf &
                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
        End Try

    End Sub

    Private Sub OnStoreMaxMinStopBtn(sender As Object, e As System.EventArgs) Handles btnPLA_STORE_MAX_STOP_ANGLE.Click, btnPLA_STORE_MIN_STOP_ANGLE.Click, btnCLA_STORE_MAX_STOP_ANGLE.Click, btnCLA_STORE_MIN_STOP_ANGLE.Click, btnMOR_STORE_MAX_STOP_ANGLE.Click, btnMOR_STORE_MIN_STOP_ANGLE.Click
        Dim msgName As String = ""
        Dim throttleInstance As ThrottleInstanceName

        Try
            If indCRIO_CONNECTED.On = True Then

                Dim StoreMaxStopInstructions As String = "Max Stop Position is Stored" & Environment.NewLine &
                                  "It is now impossible to move further this position using the lever" & Environment.NewLine &
                                  "Proceed to store the IDLE/MIN positions (if not done yet)." & Environment.NewLine &
                                  "This safety step is very important to protect the engine."

                Dim StoreMinStopInstructions As String = "Min Stop Position is Stored" & Environment.NewLine &
                                  "It is now impossible to move below this position using the lever" & Environment.NewLine &
                                  "Proceed to store the MAX/IDLE positions (if not done yet)." & Environment.NewLine &
                                  "This safety step is very important to protect the engine."

                If sender Is btnPLA_STORE_MAX_STOP_ANGLE Then
                    msgName = "Throttle.StoreMaxStopPosition"
                    dspPLA_MAX_STOP_POS.Text = dspPLA_SERVO_ANGLE_READING.Text
                    throttleInstance = ThrottleInstanceName.PLA
                    txtPlaInstructions.Text = StoreMaxStopInstructions

                ElseIf sender Is btnPLA_STORE_MIN_STOP_ANGLE Then
                    msgName = "Throttle.StoreMinStopPosition"
                    dspPLA_MIN_STOP_POS.Text = dspPLA_SERVO_ANGLE_READING.Text
                    throttleInstance = ThrottleInstanceName.PLA
                    txtPlaInstructions.Text = StoreMinStopInstructions

                ElseIf sender Is btnCLA_STORE_MAX_STOP_ANGLE Then
                    msgName = "Throttle.StoreMaxStopPosition"
                    dspCLA_MAX_STOP_POS.Text = dspCLA_SERVO_ANGLE_READING.Text
                    throttleInstance = ThrottleInstanceName.CLA
                    txtCLAInstructions.Text = StoreMaxStopInstructions

                ElseIf sender Is btnCLA_STORE_MIN_STOP_ANGLE Then
                    msgName = "Throttle.StoreMinStopPosition"
                    dspCLA_MIN_STOP_POS.Text = dspCLA_SERVO_ANGLE_READING.Text
                    throttleInstance = ThrottleInstanceName.CLA
                    txtCLAInstructions.Text = StoreMinStopInstructions

                ElseIf sender Is btnMOR_STORE_MAX_STOP_ANGLE Then
                    msgName = "Throttle.StoreMaxStopPosition"
                    dspMOR_MAX_STOP_POS.Text = dspMOR_SERVO_ANGLE_READING.Text
                    throttleInstance = ThrottleInstanceName.MOR
                    txtMORInstructions.Text = StoreMaxStopInstructions

                ElseIf sender Is btnMOR_STORE_MIN_STOP_ANGLE Then
                    msgName = "Throttle.StoreMinStopPosition"
                    dspMOR_MIN_STOP_POS.Text = dspMOR_SERVO_ANGLE_READING.Text
                    throttleInstance = ThrottleInstanceName.MOR
                    txtMORInstructions.Text = StoreMinStopInstructions
                Else
                    txtSystem.AppendText("Throttle_Sequence.OnStoreMaxMinStopBtn: Unexpected sender" & Environment.NewLine)
                End If

                Dim msg As String = CByte(throttleInstance).ToString

                Dim msgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))
                msgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
                CEL.STM.Convert.Flatten(msg, msgData)
                STMSession.SendMsg(msgName, msgData)

            End If

        Catch ex As Exception
            txtSystem.AppendText("OnStoreMaxMinStopBtn(" & throttleInstance.ToString() & "): " & vbCrLf & vbCrLf &
                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
        End Try

    End Sub

    Private Sub OnThrottleSystemConfigureDigitalOutputs(sender As Object, e As System.EventArgs) Handles chkPLAHardwareEnable.Click, chkCLAHardwareEnable.Click, chkMORHardwareEnable.Click
        Dim msgName As String = ""
        Dim throttleInstance As ThrottleInstanceName
        Dim HardwareEnabledButtonDialogBox As DialogResult

        Try

            If DirectCast(sender, CheckBox).Checked = True Then
                HardwareEnabledButtonDialogBox = MessageBox.Show("Are you sure you want to enable the power on the drives?", "Throttle System", MessageBoxButtons.YesNo)
                If HardwareEnabledButtonDialogBox = DialogResult.No Then
                    DirectCast(sender, CheckBox).Checked = False
                    Exit Sub
                End If
            End If

            'Change Text on Click
            If DirectCast(sender, CheckBox).Checked = True Then
                DirectCast(sender, CheckBox).Text = "HARDWARE ON"
            Else
                DirectCast(sender, CheckBox).Text = "HARDWARE OFF"
            End If

            If indCRIO_CONNECTED.On = True Then

                Dim msg As String = IIf(chkPLAHardwareEnable.Checked, 1, 0).ToString & ";" & 'DO0 PLA Hardware Enable
                                    IIf(chkCLAHardwareEnable.Checked, 1, 0).ToString & ";" & 'DO1 CLA Hardware Enable
                                    IIf(chkMORHardwareEnable.Checked, 1, 0).ToString & ";" & 'DO2 MOR Hardware Enable (Unconfirmed)
                                    "0;" & 'D03
                                    IIf(chkPLAHardwareEnable.Checked, 1, 0).ToString & ";" &  'DO4 PLA Hardware Enable Led
                                    IIf(chkCLAHardwareEnable.Checked, 1, 0).ToString & ";" &  'DO5 CLA Hardware Enable Led
                                    IIf(chkMORHardwareEnable.Checked, 1, 0).ToString & ";" &  'DO6 MOR Hardware Enable Led (Unconfirmed)
                                    "0;0;0;0;0;0;0;0;0"

                Dim msgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))
                msgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
                CEL.STM.Convert.Flatten(msg, msgData)
                STMSession.SendMsg("ConfigureDigitalOutputs", msgData)

            End If

        Catch ex As Exception
            txtSystem.AppendText("OnThrottleSystemConfigureDigitalOutputs(" & throttleInstance.ToString() & "): " & vbCrLf & vbCrLf &
                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
        End Try

    End Sub

    Private Sub OnReturnToIdleCmd(ByVal ReturnToIDLECommand As Boolean, ByVal ThrottleInstance As Byte)

        Dim stmMessage As String
        Try

            If ReturnToIDLECommand = True Then
                stmMessage = "Throttle.ActivateGoToIdlePosition"
            Else
                stmMessage = "Throttle.DeactivateGoToIdlePosition"
            End If

            Dim msg = CByte(throttleInstance).ToString()
            Dim msgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte))

            msgData = New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
            CEL.STM.Convert.Flatten(msg, msgData)
            STMSession.SendMsg(stmMessage, msgData)

        Catch ex As Exception
            txtSystem.AppendText("OnReturnToIdleCmd(" & ThrottleInstance.ToString() & "): " & vbCrLf & vbCrLf &
                            "Error message: " & vbCrLf & ex.ToString & Environment.NewLine)
        End Try
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        frmAboutBox.ShowDialog()
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        frmHelp.ShowDialog()
    End Sub

    Private Sub ResetUIIndicators()
        'Reset indicators
        indCRIO_CONNECTED.On = False

        indCRIO_PLA_DRIVE_FAULT.On = False
        indCRIO_PLA_DRIVE_WARNING.On = False
        indCRIO_CLA_DRIVE_FAULT.On = False
        indCRIO_CLA_DRIVE_WARNING.On = False
        indCRIO_MOR_DRIVE_FAULT.On = False
        indCRIO_MOR_DRIVE_WARNING.On = False

        For i = 0 To (NumberOfServoInstances - 1)
            For Each Grp As GroupBox In Me.tabThrottles.TabPages.Item(i).Controls.OfType(Of GroupBox)
                For Each Indicators As Bulb.LedBulb In Grp.Controls.OfType(Of Bulb.LedBulb)
                    Indicators.On = False
                Next
            Next
        Next
    End Sub

    Private Sub btnDrivesStatus_Click(sender As Object, e As EventArgs) Handles btnDrivesStatus.Click
        txtDrivesStatus.Clear()
    End Sub

    Private Sub btnSystem_Click(sender As Object, e As EventArgs) Handles btnSystem.Click
        txtSystem.Clear()
    End Sub

    Private Sub RunModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RunModeToolStripMenuItem.Click
        If Me.RunModeToolStripMenuItem.Text = "Run Mode" Then
            tabThrottles.Visible = False
            RunModeToolStripMenuItem.Text = "Rigging"
        Else
            tabThrottles.Visible = True
            RunModeToolStripMenuItem.Text = "Run Mode"
        End If
    End Sub

End Class

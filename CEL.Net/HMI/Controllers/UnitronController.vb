Imports System
Imports System.Net.Sockets
Imports System.Net.NetworkInformation
Imports System.Threading
Imports System.Linq
Imports System.Windows.Forms
Imports KineticaRT
Imports KineticaRT.Channels
Imports KineticaRT.Sequence
Imports KineticaRT.Values
Imports Microsoft.VisualBasic
Imports CEL.KineticaRT.Sequence
Imports CEL.Library
Imports Modbus.Data
Imports Modbus.Device
Imports Modbus.Utility

Namespace CEL.HMI

    Public Class UnitronController
        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()
        Private _Sequence As CEL.KineticaRT.Sequence.Sequence

        Private _StarterRectifier As IBooleanChannel = Nothing
        Private _StarterRectifierStatus As IBooleanChannel = Nothing
        Private _StarterRectifierFault As IBooleanChannel = Nothing
        Private _StarterContactor As IBooleanChannel = Nothing
        Private _StarterContactorFeedback As IBooleanChannel = Nothing
        Private _StarterContactorOutputVoltage As IChannel = Nothing
        Private _StarterContactorOutputCurrent As IChannel = Nothing
        Private _StarterContactorPriming As IBooleanChannel = Nothing
        Private _StarterContactorMotoring As IBooleanChannel = Nothing

        Private UnitronConnected As Boolean = False


        Public Sub New(ByRef Sequence As CEL.KineticaRT.Sequence.Sequence, _
                        ByVal StarterRectifierChannelName As String, _
                        ByVal StarterRectifierStatusChannelName As String, _
                        ByVal StarterRectifierFaultChannelName As String, _
                        ByVal StarterContactorChannelName As String, _
                        ByVal StarterContactorFeedbackChannelName As String, _
                        ByVal StarterContactorOutputVoltageChannelName As String, _
                        ByVal StarterContactorOutputCurrentChannelName As String, _
                        Optional ByVal StarterContactorPrimingChannelName As String = "OIL_PRIMING_BTN", _
                        Optional ByVal StarterContactorMotoringChannelName As String = "MOTORING_BTN")

            Dim TempChannel As IChannel = Nothing

            If Not (StarterRectifierChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierChannelName)
                _StarterRectifier = CType(TempChannel, IBooleanChannel)
            End If

            If Not (StarterRectifierStatusChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierStatusChannelName)
                _StarterRectifierStatus = CType(TempChannel, IBooleanChannel)
            End If

            If Not (StarterRectifierFaultChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierFaultChannelName)
                _StarterRectifierFault = CType(TempChannel, IBooleanChannel)
            End If

            If Not (StarterContactorChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactorChannelName)
                _StarterContactor = CType(TempChannel, IBooleanChannel)
            End If

            If Not (StarterContactorFeedbackChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactorFeedbackChannelName)
                _StarterContactorFeedback = CType(TempChannel, IBooleanChannel)
            End If

            If Not (StarterContactorOutputVoltageChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_StarterContactorOutputVoltage, StarterContactorOutputVoltageChannelName)
            End If

            If Not (StarterContactorOutputCurrentChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_StarterContactorOutputCurrent, StarterContactorOutputCurrentChannelName)
            End If

            If Not (StarterContactorPrimingChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactorPrimingChannelName)
                _StarterContactorPriming = CType(TempChannel, IBooleanChannel)
            End If

            If Not (StarterContactorMotoringChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactorMotoringChannelName)
                _StarterContactorMotoring = CType(TempChannel, IBooleanChannel)
            End If

            _Sequence = Sequence

            Initialize()

        End Sub

        Private Sub AddHandlers()

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf Me.OnCLOSE_TEST_BTN
            End If
            If Not (_StarterRectifier Is Nothing) Then
                AddHandler _StarterRectifier.ValueChanged, AddressOf OnStarterRectifier
            End If
            If Not (_StarterContactor Is Nothing) Then
                AddHandler _StarterContactor.ValueChanged, AddressOf OnStarterContactor
            End If
            If Not (_StarterContactorPriming Is Nothing) Then
                AddHandler _StarterContactorPriming.ValueChanged, AddressOf OnStarterContactor
            End If
            If Not (_StarterContactorMotoring Is Nothing) Then
                AddHandler _StarterContactorMotoring.ValueChanged, AddressOf OnStarterContactor
            End If

        End Sub

        Private Sub RemoveHandlers()

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf Me.OnCLOSE_TEST_BTN
            End If
            If Not (_StarterRectifier Is Nothing) Then
                RemoveHandler _StarterRectifier.ValueChanged, AddressOf OnStarterRectifier
            End If
            If Not (_StarterContactor Is Nothing) Then
                RemoveHandler _StarterContactor.ValueChanged, AddressOf OnStarterContactor
            End If
            If Not (_StarterContactorPriming Is Nothing) Then
                RemoveHandler _StarterContactorPriming.ValueChanged, AddressOf OnStarterContactor
            End If
            If Not (_StarterContactorMotoring Is Nothing) Then
                RemoveHandler _StarterContactorMotoring.ValueChanged, AddressOf OnStarterContactor
            End If

        End Sub

        '************************
        '* OnStarterRectifier() *
        '************************
        Private Sub OnStarterRectifier(ByVal sender As IChannel, ByVal newVal As Value)

            'Description of Action	Action to initiate	Results of Action	                                            Feedback
            'Turn On converter	    Write 1 to 40801	Converter Starts producing 28vdc	                            40268 “Power Status” will change to 1.
            'Turn off converter	    Write 1 to 40802	Converter will stop producing power	                            40268 “Power Status” will change to 0.

            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If UnitronConnected = True Then
                    If CELProject.IsTrue(newVal) And CELProject.IsFalse(_StarterRectifierStatus.Value) Then
                        'Only send the activation command to rectifier if feedback is false.
                        Master.WriteSingleRegister(801, 1)
                    ElseIf CELProject.IsFalse(newVal) And CELProject.IsTrue(_StarterRectifierStatus.Value) Then
                        'Only send the deactivation command to rectifier if feedback is true.
                        Master.WriteSingleRegister(802, 1)
                    End If
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("Unitron.OnStarterRectifier: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '************************
        '* OnStarterContactor() *
        '************************
        Private Sub OnStarterContactor(ByVal sender As IChannel, ByVal newVal As Value)

            'Description of Action	Action to initiate	Results of Action	                                            Feedback
            'Turn on Output #1	    Write 1 to 40805	Ouptut 1 Contactor will close providing 28vdc to the load.	    40269 “Output Cord 1 Available” will change to 1.
            'Turn off Output #1	    Write 1 to 40806	Output 1 Contactor will open	                                40269 “Output Cord 1 Available” will change to 0.

            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If UnitronConnected = True Then

                    Dim DCOutputCurrentLimit As UShort

                    If CELProject.EngineFamily = "PW100" Then

                        If sender.Name = "PRIMING_BTN" Then
                            DCOutputCurrentLimit = CELProject.Unitron.PW100OilPriming
                        ElseIf sender.Name = "MOTORING_BTN" Then
                            DCOutputCurrentLimit = CELProject.Unitron.PW100Motoring
                        Else
                            DCOutputCurrentLimit = CELProject.Unitron.PW100Cranking
                        End If
                    Else
                        DCOutputCurrentLimit = CELProject.Unitron.DefaultCurrentLimit
                    End If

                    'Output Current limit range is 150-2000 
                    If DCOutputCurrentLimit < 150 Then
                        DCOutputCurrentLimit = 150
                        CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Warning : Current Out of Range : Unitron Starter Rectifier forced to minimum : 150A", ToolTipIcon.Info)
                    End If
                    If DCOutputCurrentLimit > 2000 Then
                        DCOutputCurrentLimit = 2000
                        CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Warning : Current Out of Range : Unitron Starter Rectifier forced to maximum : 2000A", ToolTipIcon.Info)
                    End If

                    Master.WriteSingleRegister(441, DCOutputCurrentLimit)

                    If CELProject.IsTrue(newVal) And CELProject.IsFalse(_StarterContactorFeedback.Value) Then
                        'Only send the activation command to contactor if feedback is false.
                        Master.WriteSingleRegister(805, 1)
                    ElseIf CELProject.IsFalse(newVal) And CELProject.IsTrue(_StarterContactorFeedback.Value) Then
                        'Only send the deactivation command to contactor if feedback is true.
                        Master.WriteSingleRegister(806, 1)
                    End If
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("Unitron.OnStarterContactor: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ConnectToUnitronStarterRectifier()

            UnitronConnected = False

            Try

                Dim ipAddress As String = CELProject.Unitron.IpAdress
                Dim tcpPort As Integer = CELProject.Unitron.ipPort
                Dim Reconnecting As Boolean = False

                'If we are reconnecting, we are disposing tcpclient
                If TcpClient IsNot Nothing Then
                    TcpClient.Client.Close()
                    TcpClient.Close()
                End If

                TcpClient = New TcpClient()

                'Verifying if the tcp connection was sucessful
                Dim AsyncResult As IAsyncResult = TcpClient.BeginConnect(ipAddress, tcpPort, Nothing, Nothing)
                AsyncResult.AsyncWaitHandle.WaitOne(3000, True)
                If Not (AsyncResult.IsCompleted) Then
                    CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Error : Cannot Connect Unitron Starter Rectifier to " & ipAddress, ToolTipIcon.Info)
                    Exit Sub
                End If

                'If we are reconnecting, we are disposing master
                If Master IsNot Nothing Then
                    Master.Dispose()
                    Reconnecting = True
                End If

                'Creating Master Modbus over TCP/IP with Unitron Starter Rectifier
                Master = ModbusIpMaster.CreateIp(TcpClient)

                'Obtain the current state of the socket underpinning a TcpClient.
                'Unlike TcpClient.Connected this is reliable and does not require 
                'capture of an exception resulting from a failed socket write.
                TcpLink = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().SingleOrDefault(Function(x) x.LocalEndPoint.Equals(TcpClient.Client.LocalEndPoint))
                TcpLinkStatus = If(TcpLink IsNot Nothing, TcpLink.State, TcpState.Unknown)

                'Verify connection is established : using a combination to support unplugging/disabling the network connection 
                If TcpClient IsNot Nothing AndAlso TcpClient.Client IsNot Nothing AndAlso TcpClient.Client.Connected AndAlso TcpLinkStatus = TcpState.Established Then
                    Master.Transport.Retries = 0
                    Master.Transport.ReadTimeout = 1500
                    UnitronConnected = True

                    'If we were reconnecting, inform user that that the reconnection has been sucessful
                    If Reconnecting = True Then
                        CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "CELDAS200 reconnected to Unitron Starter Rectifier at " & ipAddress, ToolTipIcon.Info)
                        Reconnecting = False
                    End If
                End If

            Catch Ex As Exception
                clsTopMostMessageBox.Show("Unitron.Init_Unitron: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
            End Try
        End Sub

        Private Sub Unitron_WorkingThread()

            Do While Not ExitThread

                Thread.Sleep(100)

                'If Archive is not playing, we execute the code
                If CELProject.IsFalse(CELProject.ARCHIVE_PLAY.Value) Then

                    'Connection Was Sucessful
                    If UnitronConnected = True Then

                        ' Detect if client disconnected
                        Try
                            TcpLink = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().SingleOrDefault(Function(x) x.LocalEndPoint.Equals(TcpClient.Client.LocalEndPoint))
                            TcpLinkStatus = If(TcpLink IsNot Nothing, TcpLink.State, TcpState.Unknown)

                            If TcpClient IsNot Nothing AndAlso TcpClient.Client IsNot Nothing AndAlso TcpClient.Client.Connected AndAlso TcpLinkStatus = TcpState.Established Then
                                ' Detect if client disconnected
                                If TcpClient.Client.Poll(0, SelectMode.SelectRead) Then
                                    Dim buff As Byte() = New Byte(0) {}
                                    If TcpClient.Client.Receive(buff, SocketFlags.Peek) = 0 Then
                                        ' Client disconnected
                                        TcpConnectionStatus = False
                                    Else
                                        TcpConnectionStatus = True
                                    End If
                                End If
                                'Client is connected : all good
                                TcpConnectionStatus = True
                            Else
                                TcpConnectionStatus = False
                            End If
                        Catch
                            'Can't read from client
                            TcpConnectionStatus = False
                        End Try

                        'If we have a connection established between Master and Slave, we read the Starter Voltage and Starter Current
                        If TcpConnectionStatus = True Then
                            Try
                                'Retrieving Starter Voltage and Starter Current 
                                Dim DCOutputVoltageAndOutputCurrent As UShort() = Master.ReadHoldingRegisters(169, 2)

                                'Only update values if the new value is different from current value. (This is to avoid unnecessary calls to the OPC server)
                                If Math.Round(CType(DCOutputVoltageAndOutputCurrent(0), Double), 0) <> Math.Round(_StarterContactorOutputVoltage.Value.ToDouble, 0) Then
                                    _StarterContactorOutputVoltage.Value = New Global.KineticaRT.Values.Integer(DCOutputVoltageAndOutputCurrent(0))
                                End If
                                If Math.Round(CType(DCOutputVoltageAndOutputCurrent(1), Double), 0) <> Math.Round(_StarterContactorOutputCurrent.Value.ToDouble, 0) Then
                                    _StarterContactorOutputCurrent.Value = New Global.KineticaRT.Values.Integer(DCOutputVoltageAndOutputCurrent(1))
                                End If

                                'Retrieving Status of Starter Rectifier and Starter Contactor
                                Dim DCOutputStatus As UShort() = Master.ReadHoldingRegisters(268, 2)

                                'Only update values if the new value is different from current value. (This is to avoid unnecessary calls to the OPC server)
                                If CBool(DCOutputStatus(0)) = True Then
                                    _StarterRectifierStatus.Value = Global.KineticaRT.Values.Values.True
                                Else
                                    _StarterRectifierStatus.Value = Global.KineticaRT.Values.Values.False
                                End If

                                'Only update values if the new value is different from current value. (This is to avoid unnecessary calls to the OPC server)
                                If CBool(DCOutputStatus(1)) = True Then
                                    _StarterContactorFeedback.Value = Global.KineticaRT.Values.Values.True
                                Else
                                    _StarterContactorFeedback.Value = Global.KineticaRT.Values.Values.False
                                End If

                                'Retrieving Faults
                                Dim StarterRectifierFaults As UShort() = Master.ReadHoldingRegisters(145, 1)

                                'Only update fault message once per fault. (This is to avoid unnecessary calls to the OPC server)
                                If CELProject.IsFalse(_StarterRectifierFault.Value) And StarterRectifierFaults(0) <> 0 Then
                                    _StarterRectifierFault.Value = New Global.KineticaRT.Values.Boolean(True)

                                    'Inform user, We have an error
                                    CELDAS.NotifyIcon.ShowBalloonTip(3000, _
                                                                     CELProject.TechnologyName, _
                                                                     "Unitron Fault " & vbCr & _
                                                                     "Code: " & StarterRectifierFaults(0).ToString & vbCr & _
                                                                     "Message: " & Unitron_Fault(StarterRectifierFaults(0)), _
                                                                     ToolTipIcon.Info)
                                    'When a critical fault happens. Unitron cuts the DC Output and will not allow it back ON until the fault is reset.
                                    'There doesn't seem to be a command to reset the fault via code.
                                    'The only way to reset the fault is to turn the Unitron unit Off and then back On.
                                    'This could be done by code but we need a way to discriminate between a critical fault and a warning.
                                    'For now, Fault managing is the operators responsibility.
                                ElseIf CELProject.IsTrue(_StarterRectifierFault.Value) And StarterRectifierFaults(0) = 0 Then
                                    _StarterRectifierFault.Value = New Global.KineticaRT.Values.Boolean(False)
                                End If

                            Catch
                            'Sometimes, the connection is not valid but it was reported established : if we can't read the values, it is considered as we are not connected
                            UnitronConnected = False
                        End Try
                        Else
                            'While connected to Unitron, we have lost the connection : Inform user, Attempting to reconnect
                            CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Error : Lost Connection to Unitron Starter Rectifier : Attempting to Reconnect", ToolTipIcon.Info)
                            ConnectToUnitronStarterRectifier()
                        End If

                        'Connection was not sucessful or lost : retrying
                    Else
                        CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Error : Lost Connection to Unitron Starter Rectifier : Attempting to Reconnect", ToolTipIcon.Info)
                        ConnectToUnitronStarterRectifier()
                    End If

                    'CELDAS200 is closing : exiting thread
                    If CELProject.Test_Mgr_Status.Value.ToDouble() = eTestMgrStatus.Closing Then
                        ExitThread = True
                    End If

                End If

            Loop

            'Thread is closed : close connection with Slave
            If Not TcpClient Is Nothing Then
                TcpClient.Close()
            End If
            If Not Master Is Nothing Then
                Master.Dispose()
            End If

        End Sub

        Private Sub Initialize()

            Try
                If CELProject.DataAcquisitionMode = CELDAS.eDataAcquisitionMode.RUN_FOR_TRUE Then

                    Me.AddHandlers()
                    ConnectToUnitronStarterRectifier()

                    ExitThread = False

                    'Create Thread for reading
                    Unitron_WorkerThread = New Thread(AddressOf Unitron_WorkingThread)
                    Unitron_WorkerThread.Priority = ThreadPriority.Normal
                    Unitron_WorkerThread.Name = "Unitron_WorkerThread"
                    Unitron_WorkerThread.Start()
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("Unitron.Initialize: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '********************
        '* OnCloseTest()    *
        '********************
        Private Sub OnCLOSE_TEST_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try

                If CELProject.IsTrue(newVal) Then
                    ThreadHelper.StopOrKillThread(Unitron_WorkerThread, ExitThread)
                    RemoveHandlers()
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("Unitron.OnCLOSE_TEST_BTN: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '********************
        '* Starter Faults() *
        '********************
        Private Function Unitron_Fault(ByRef faultCode As UShort) As String
            Try
                Select Case faultCode
                    Case 23 : Return "OUT2 INTERLOCK"
                    Case 24 : Return "OUT1 INTERLOCK"
                    Case 44 : Return "ILLEGAL DOWNLOAD"
                    Case 45 : Return "DIAGNOSTIC MODE"
                    Case 62 : Return "FAN SPEED ERROR"
                    Case 63 : Return "FAN FAILURE"
                    Case 82 : Return "INPUT UNBALANCED"
                    Case 83 : Return "THERMOCOUPLE ERROR"
                    Case 84 : Return "PROCESSOR BROWNOUT"
                    Case 87 : Return "IGBT MODULE"
                    Case 94 : Return "WATCHDOG TIMEOUT"
                    Case 100 : Return "SYSTEM START ERROR"
                    Case 102 : Return "CUR LIMIT TIMEOUT"
                    Case 112 : Return "INPUT MOV FAILURE"
                    Case 115 : Return "OUT OVERLOAD WARN"
                    Case 120 : Return "POWER NOT PROCESSING"
                    Case 121 : Return "OUTPUT POWER OVERLOAD SHUTDOWN"
                    Case 122 : Return "INPUT ACV MISMATCH"
                    Case 123 : Return "INPUT AMP MISMATCH"
                    Case 124 : Return "INPUT AMP OVERLOAD"
                    Case 141 : Return "CONFIGURATION FLT"
                    Case 142 : Return "STATUS SCAN FAULT"
                    Case 143 : Return "COMMUNICATIONS FLT"
                    Case 153 : Return "REGULATOR -12V FLT"
                    Case 154 : Return "REGULATOR +12V FLT"
                    Case 155 : Return "REGULATOR -5V FLT"
                    Case 156 : Return "REGULATOR +5V FLT"
                    Case 157 : Return "REGULATOR +24V FLT"
                    Case 158 : Return "48V PWR SUPPLY FLT"
                    Case 159 : Return "REGULATOR FUSE FLT"
                    Case 169 : Return "REGULATR START FLT"
                    Case 170 : Return "SOFT START FAULT"
                    Case 171 : Return "INPUT POWER LOSS"
                    Case 218 : Return "PWR TRANSISTOR FLT"
                    Case 219 : Return "RECT DCV VARIANCE"
                    Case 220 : Return "RECT DC VOLT FAULT"
                    Case 221 : Return "IN ACV RANGE FAULT"
                    Case 222 : Return "INPUT FUSE FAULT"
                    Case 223 : Return "DISPLAY SCAN FAULT"
                    Case 224 : Return "ILLEGAL CMD RQST"
                    Case 225 : Return "CONTACTOR CMD ERR"
                    Case 226 : Return "CONTACTOR FAULT"
                    Case 227 : Return "STOP RQST FAILURE"
                    Case 228 : Return "SYSTEM START ERROR"
                    Case 229 : Return "RECT DC LINK LOW"
                    Case 230 : Return "CUR LIMIT TIMEOUT"
                    Case 231 : Return "FPGA VERSION ERROR"
                    Case 232 : Return "SW VERSION ERROR"
                    Case 233 : Return "MEMORY ERROR"
                    Case 234 : Return "SOFTWARE INCORRECT FOR BOARD"
                    Case 249 : Return "INPUT PEAK OVERCURRENT"
                    Case 250 : Return "SYSTEM LOGIC"
                    Case 251 : Return "INPUT POWER FAILURE"
                    Case 252 : Return "INPUT CURRENT OVERLOAD"
                    Case 253 : Return "INPUT LINE FAULT"
                    Case 254 : Return "DOOR OPEN"
                    Case 255 : Return "EPO ENGAGED"
                    Case Else : Return "UNKOWN ERROR"
                End Select
            Catch ex As Exception
                clsTopMostMessageBox.Show("Unitron_Faults: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function


        Private Client As TcpClient
        Private Master As ModbusIpMaster

        Private TcpClient As TcpClient
        Private TcpLink As Object
        Private TcpLinkStatus As Object
        Private TcpConnectionStatus As Boolean

        Private Unitron_WorkerThread As Thread
        Private ExitThread As Boolean = False


#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose

            Try
                ThreadHelper.StopOrKillThread(Unitron_WorkerThread, Me.ExitThread)

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionPostClosing: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace



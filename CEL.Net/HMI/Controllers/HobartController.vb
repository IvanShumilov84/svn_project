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

    Public Class HobartController
        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()
        Private _Sequence As CEL.KineticaRT.Sequence.Sequence

        Const ContactorIndex_1 As Integer = 0
        Const ContactorIndex_2 As Integer = 1


        Private _StarterRectifierReady28VoltON As IBooleanChannel = Nothing
        Private _StarterRectifierModbusState As ITextChannel = Nothing
        Private _StarterRectifierModbusFault As ITextChannel = Nothing
        Private _StarterRectifierModbusOutputVoltage As INumericChannel = Nothing
        Private _StarterRectifierModbusOutputCurrent As INumericChannel = Nothing
        Private _StarterRectifierModbusOutputPower As INumericChannel = Nothing
        Private _StarterRectifierModbusCurrentLimit As INumericChannel = Nothing
        Private _StarterRectifierReset As IBooleanChannel = Nothing
        Private _StarterContactor_RequestCmd() As IBooleanChannel = {Nothing, Nothing}
        Private _StarterContactor_Cmd() As IBooleanChannel = {Nothing, Nothing}
        Private _StarterContactor_28VoltON() As IBooleanChannel = {Nothing, Nothing}
        Private _StarterContactor_ModbusState() As ITextChannel = {Nothing, Nothing}

        Private _LastStarterContactor_CommandSentToHobart() As Boolean = {False, False}
        Private _Pulse_Length() As Integer = {0, 0}

        Private HobartConnected As Boolean = False
        Private FilterNotifyBaloon As Integer = 0

        Private ContactorEnabled() As Boolean = {True, True}

        Public Sub New(ByRef Sequence As CEL.KineticaRT.Sequence.Sequence, _
                        ByVal StarterRectifierReady28VoltONChannelName As String, _
                        ByVal StarterRectifierModbusStateChannelName As String, _
                        ByVal StarterRectifierModbusFaultChannelName As String, _
                        ByVal StarterRectifierModbusOutputVoltageChannelName As String, _
                        ByVal StarterRectifierModbusOutputCurrentChannelName As String, _
                        ByVal StarterRectifierModbusOutputPowerChannelName As String, _
                        ByVal StarterRectifierModbusCurrentLimitChannelName As String, _
                        ByVal StarterRectifierResetChannelName As String, _
                        ByVal StarterContactor1_RequestCmdChannelName As String, _
                        ByVal StarterContactor2_RequestCmdChannelName As String, _
                        ByVal StarterContactor1_CmdChannelName As String, _
                        ByVal StarterContactor2_CmdChannelName As String, _
                        ByVal StarterContactor1_28VoltONChannelName As String, _
                        ByVal StarterContactor2_28VoltONChannelName As String, _
                        ByVal StarterContactor1_ModbusStateChannelName As String, _
                        ByVal StarterContactor2_ModbusStateChannelName As String)

            Dim TempChannel As IChannel = Nothing

            If Not (StarterRectifierReady28VoltONChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierReady28VoltONChannelName)
                _StarterRectifierReady28VoltON = CType(TempChannel, IBooleanChannel)
            End If

            If Not (StarterRectifierModbusStateChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierModbusStateChannelName)
                _StarterRectifierModbusState = CType(TempChannel, ITextChannel)
            End If

            If Not (StarterRectifierModbusFaultChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierModbusFaultChannelName)
                _StarterRectifierModbusFault = CType(TempChannel, ITextChannel)
            End If

            If Not (StarterRectifierModbusOutputVoltageChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierModbusOutputVoltageChannelName)
                _StarterRectifierModbusOutputVoltage = CType(TempChannel, INumericChannel)
            End If

            If Not (StarterRectifierModbusOutputCurrentChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierModbusOutputCurrentChannelName)
                _StarterRectifierModbusOutputCurrent = CType(TempChannel, INumericChannel)
            End If

            If Not (StarterRectifierModbusOutputPowerChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierModbusOutputPowerChannelName)
                _StarterRectifierModbusOutputPower = CType(TempChannel, INumericChannel)
            End If

            If Not (StarterRectifierModbusCurrentLimitChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierModbusCurrentLimitChannelName)
                _StarterRectifierModbusCurrentLimit = CType(TempChannel, INumericChannel)
            End If

            If Not (StarterRectifierResetChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterRectifierResetChannelName)
                _StarterRectifierReset = CType(TempChannel, IBooleanChannel)
            End If

            If Not (StarterContactor1_RequestCmdChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactor1_RequestCmdChannelName)
                _StarterContactor_RequestCmd(ContactorIndex_1) = CType(TempChannel, IBooleanChannel)
            Else
                ContactorEnabled(ContactorIndex_1) = False
                Console.WriteLine("HobartController: Contactor 1 Request Command Channel Name is Nothing. Control of Contactor 1 will be disabled")
            End If

            If Not (StarterContactor2_RequestCmdChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactor2_RequestCmdChannelName)
                _StarterContactor_RequestCmd(ContactorIndex_2) = CType(TempChannel, IBooleanChannel)
            Else
                ContactorEnabled(ContactorIndex_2) = False
                Console.WriteLine("HobartController: Contactor 2 Request Command Channel Name is Nothing. Control of Contactor 2 will be disabled")
            End If

            If Not (StarterContactor1_CmdChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactor1_CmdChannelName)
                _StarterContactor_Cmd(ContactorIndex_1) = CType(TempChannel, IBooleanChannel)
            Else
                ContactorEnabled(ContactorIndex_1) = False
                Console.WriteLine("HobartController: Contactor 1 Command Channel Name is Nothing. Control of Contactor 1 will be disabled")
            End If

            If Not (StarterContactor2_CmdChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactor2_CmdChannelName)
                _StarterContactor_Cmd(ContactorIndex_2) = CType(TempChannel, IBooleanChannel)
            Else
                ContactorEnabled(ContactorIndex_2) = False
                Console.WriteLine("HobartController: Contactor 2 Command Channel Name is Nothing. Control of Contactor 2 will be disabled")
            End If

            If Not (StarterContactor1_28VoltONChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactor1_28VoltONChannelName)
                _StarterContactor_28VoltON(ContactorIndex_1) = CType(TempChannel, IBooleanChannel)
            Else
                ContactorEnabled(ContactorIndex_1) = False
                Console.WriteLine("HobartController: Contactor 1 28 Volt ON Channel Name is Nothing. Control of Contactor 1 will be disabled")
            End If

            If Not (StarterContactor2_28VoltONChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactor1_28VoltONChannelName)
                _StarterContactor_28VoltON(ContactorIndex_2) = CType(TempChannel, IBooleanChannel)
            Else
                ContactorEnabled(ContactorIndex_2) = False
                Console.WriteLine("HobartController: Contactor 2 28 Volt ON Channel Name is Nothing. Control of Contactor 2 will be disabled")
            End If

            If Not (StarterContactor1_ModbusStateChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactor1_ModbusStateChannelName)
                _StarterContactor_ModbusState(ContactorIndex_1) = CType(TempChannel, ITextChannel)
            Else
                ContactorEnabled(ContactorIndex_1) = False
                Console.WriteLine("HobartController: Contactor 1 Modbus State Channel Name is Nothing. Control of Contactor 1 will be disabled")
            End If

            If Not (StarterContactor2_ModbusStateChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, StarterContactor2_ModbusStateChannelName)
                _StarterContactor_ModbusState(ContactorIndex_2) = CType(TempChannel, ITextChannel)
            Else
                ContactorEnabled(ContactorIndex_2) = False
                Console.WriteLine("HobartController: Contactor 2 Modbus State Channel Name is Nothing. Control of Contactor 2 will be disabled")
            End If

            _Sequence = Sequence

            Initialize()

        End Sub

        Private Sub AddHandlers()

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf Me.OnCLOSE_TEST_BTN
            End If

        End Sub

        Private Sub RemoveHandlers()

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf Me.OnCLOSE_TEST_BTN
            End If

        End Sub


        Private Sub ConnectToHobartStarterRectifier()

            HobartConnected = False

            Try

                Dim ipAddress As String = CELProject.Hobart.IpAdress
                Dim tcpPort As Integer = CELProject.Hobart.ipPort
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
                    If FilterNotifyBaloon = 0 Then
                        CELDAS.NotifyIcon.ShowBalloonTip(1000, CELProject.TechnologyName, "Error : Cannot Connect Hobart Starter Rectifier to " & ipAddress, ToolTipIcon.Info)
                        FilterNotifyBaloon = 3 'this way we have around 10 seconds between message 
                        Exit Sub
                    End If
                End If
                'If we are reconnecting, we are disposing master
                If Master IsNot Nothing Then
                    Master.Dispose()
                    Reconnecting = True
                End If

                'Creating Master Modbus over TCP/IP with Hobart Starter Rectifier
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
                    HobartConnected = True

                    'If we were reconnecting, inform user that that the reconnection has been sucessful
                    If Reconnecting = True Then
                        CELDAS.NotifyIcon.ShowBalloonTip(1000, CELProject.TechnologyName, "CELDAS200 reconnected to Hobart Starter Rectifier at " & ipAddress, ToolTipIcon.Info)
                        Reconnecting = False
                    End If
                End If

            Catch Ex As Exception
                clsTopMostMessageBox.Show("Hobart.Init_Hobart: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
            End Try
        End Sub

        Private Sub Hobart_ReadStarterRectifierState()

            Dim StarterRectifierState As UShort()
            Dim StarterRectifierStateText As String

            If TcpConnectionStatus = True And HobartConnected = True Then
                Try
                    'Retrieving State
                    StarterRectifierState = Master.ReadHoldingRegisters(1027, 1) '0X0403 = 1027, result is one unsigned
                    StarterRectifierStateText = Hobart_State(StarterRectifierState(0))
                Catch
                    'Sometimes, the connection is not valid but it was reported established : if we can't read the values, it is considered as we are not connected
                    StarterRectifierStateText = "No Communication"
                    HobartConnected = False
                    CELDAS.NotifyIcon.ShowBalloonTip(1000, CELProject.TechnologyName, "Error : Hobart: Exception was trapped when reading register from Hobart", ToolTipIcon.Info)
                End Try

            Else
                StarterRectifierStateText = "No Communication"

            End If

            'Only update fault message once per fault. (This is to avoid unnecessary calls to the OPC server)
            If (_StarterRectifierModbusState.Value.ToString <> StarterRectifierStateText) Then

                _StarterRectifierModbusState.Value = New Global.KineticaRT.Values.Text(StarterRectifierStateText)

            End If
        End Sub

        Private Sub Hobart_ReadStarterRectifierFaults()

            Dim StarterRectifierFaults As UShort()
            Dim StarterRectifierFaultText As String

            If TcpConnectionStatus = True And HobartConnected = True Then
                Try
                    'Retrieving Faults
                    StarterRectifierFaults = Master.ReadHoldingRegisters(1047, 1) '0X0417 = 1047, result is one unsigned
                    StarterRectifierFaultText = Hobart_Fault(StarterRectifierFaults(0))
                Catch
                    'Sometimes, the connection is not valid but it was reported established : if we can't read the values, it is considered as we are not connected
                    StarterRectifierFaultText = "No Communication"
                    HobartConnected = False
                    CELDAS.NotifyIcon.ShowBalloonTip(1000, CELProject.TechnologyName, "Error : Hobart: Exception was trapped when reading register from Hobart", ToolTipIcon.Info)
                End Try
            Else
                StarterRectifierFaultText = "No Communication"
            End If

            'Only update fault message once per fault. (This is to avoid unnecessary calls to the OPC server)
            If (_StarterRectifierModbusFault.Value.ToString <> StarterRectifierFaultText) Then

                _StarterRectifierModbusFault.Value = New Global.KineticaRT.Values.Text(StarterRectifierFaultText)

            End If
        End Sub

        Private Sub Hobart_ReadStarterRectifierVoltage()

            Dim OutputVoltageShort As UShort()
            Dim OutputVoltage As Double

            If TcpConnectionStatus = True And HobartConnected = True Then
                Try
                    'Retrieving Voltage output
                    OutputVoltageShort = Master.ReadHoldingRegisters(1041, 1) '0X0411 = 1041, result is one unsigned, Scale factor = 25.46
                    OutputVoltage = OutputVoltageShort(0) / 25.46
                Catch
                    'Sometimes, the connection is not valid but it was reported established : if we can't read the values, it is considered as we are not connected
                    OutputVoltage = -1.0
                    HobartConnected = False
                    CELDAS.NotifyIcon.ShowBalloonTip(1000, CELProject.TechnologyName, "Error : Hobart: Exception was trapped when reading register from Hobart", ToolTipIcon.Info)
                End Try
            Else
                OutputVoltage = -1.0
            End If

            'Only update fault message once per fault. (This is to avoid unnecessary calls to the OPC server)
            If (_StarterRectifierModbusOutputVoltage.Value.ToDouble <> OutputVoltage) Then

                _StarterRectifierModbusOutputVoltage.Value = New Global.KineticaRT.Values.Double(OutputVoltage)

            End If
        End Sub

        Private Sub Hobart_ReadStarterRectifierCurrent()

            Dim OutputCurrentShort As UShort()
            Dim OutputCurrent As Double

            If TcpConnectionStatus = True And HobartConnected = True Then
                Try
                    'Retrieving Voltage output
                    OutputCurrentShort = Master.ReadHoldingRegisters(1042, 1) '0X0412 = 1042, result is one unsigned, Scale factor = 4.74
                    OutputCurrent = OutputCurrentShort(0) / 4.74
                Catch
                    'Sometimes, the connection is not valid but it was reported established : if we can't read the values, it is considered as we are not connected
                    OutputCurrent = -1.0
                    HobartConnected = False
                    CELDAS.NotifyIcon.ShowBalloonTip(1000, CELProject.TechnologyName, "Error : Hobart: Exception was trapped when reading register from Hobart", ToolTipIcon.Info)
                End Try
            Else
                OutputCurrent = -1.0
            End If

            'Only update fault message once per fault. (This is to avoid unnecessary calls to the OPC server)
            If (_StarterRectifierModbusOutputCurrent.Value.ToDouble <> OutputCurrent) Then

                _StarterRectifierModbusOutputCurrent.Value = New Global.KineticaRT.Values.Double(OutputCurrent)

            End If
        End Sub

        Private Sub Hobart_ReadStarterRectifierPower()

            Dim OutputPowerShort As UShort()
            Dim OutputPower As Double

            If TcpConnectionStatus = True And HobartConnected = True Then
                Try
                    'Retrieving Voltage output
                    OutputPowerShort = Master.ReadHoldingRegisters(1043, 1) '0X0413 = 1043, result is one unsigned, Scale factor = 29.4
                    OutputPower = OutputPowerShort(0) / 29.4
                Catch
                    'Sometimes, the connection is not valid but it was reported established : if we can't read the values, it is considered as we are not connected
                    OutputPower = -1.0
                    HobartConnected = False
                    CELDAS.NotifyIcon.ShowBalloonTip(1000, CELProject.TechnologyName, "Error : Hobart: Exception was trapped when reading register from Hobart", ToolTipIcon.Info)
                End Try
            Else
                OutputPower = -1.0
            End If

            'Only update fault message once per fault. (This is to avoid unnecessary calls to the OPC server)
            If (_StarterRectifierModbusOutputPower.Value.ToDouble <> OutputPower) Then

                _StarterRectifierModbusOutputPower.Value = New Global.KineticaRT.Values.Double(OutputPower)

            End If
        End Sub

        Private Sub Hobart_ReadStarterRectifierCurrentLimit()

            Dim CurrentLimitShort As UShort()
            Dim CurrentLimit As Double

            If TcpConnectionStatus = True And HobartConnected = True Then
                Try
                    'Retrieving Voltage output
                    CurrentLimitShort = Master.ReadHoldingRegisters(531, 1) '0X0213 = 531, result is one unsigned, Scale factor = 4.74
                    CurrentLimit = CurrentLimitShort(0) / 4.74
                Catch
                    'Sometimes, the connection is not valid but it was reported established : if we can't read the values, it is considered as we are not connected
                    CurrentLimit = -1.0
                    HobartConnected = False
                    CELDAS.NotifyIcon.ShowBalloonTip(1000, CELProject.TechnologyName, "Error : Hobart: Exception was trapped when reading register from Hobart", ToolTipIcon.Info)
                End Try
            Else
                CurrentLimit = -1.0
            End If

            'Only update fault message once per fault. (This is to avoid unnecessary calls to the OPC server)
            If (_StarterRectifierModbusCurrentLimit.Value.ToDouble <> CurrentLimit) Then

                _StarterRectifierModbusCurrentLimit.Value = New Global.KineticaRT.Values.Double(CurrentLimit)

            End If
        End Sub

        Private Sub Hobart_ReadStarterContactorState(ByRef Index As Integer)

            Dim StarterOutputState As UShort()
            Dim StarterOutputStateText As String

            If TcpConnectionStatus = True And HobartConnected = True Then

                If CELProject.IsFalse(_StarterContactor_RequestCmd(Index).Value) Then
                    Return
                End If

                Try
                    'Retrieving Output1 or Output2 State
                    If Index = ContactorIndex_1 Then
                        StarterOutputState = Master.ReadHoldingRegisters(1029, 1) 'Contactor 1: 0X0405 = 1029, result is one unsigned
                    ElseIf Index = ContactorIndex_2 Then
                        StarterOutputState = Master.ReadHoldingRegisters(1030, 1) 'Contactor 2: 0X0406 = 1030, result is one unsigned
                    End If

                    StarterOutputStateText = Hobart_Output_State(StarterOutputState(0))

                Catch
                    'Sometimes, the connection is not valid but it was reported established : if we can't read the values, it is considered as we are not connected
                    StarterOutputStateText = "No Communication"
                    HobartConnected = False
                    CELDAS.NotifyIcon.ShowBalloonTip(1000, CELProject.TechnologyName, "Error : Hobart: Exception was trapped when reading register from Hobart", ToolTipIcon.Info)
                End Try
            Else
                StarterOutputStateText = "No Communication"
            End If

            'Only update fault message once per fault. (This is to avoid unnecessary calls to the OPC server)
            If (_StarterContactor_ModbusState(Index).Value.ToString <> StarterOutputStateText) Then

                _StarterContactor_ModbusState(Index).Value = New Global.KineticaRT.Values.Text(StarterOutputStateText)

            End If
        End Sub

        Private Sub ControlContactor(ByRef Index As Integer)

            'The Contactor are controller by a pulse command. At each pulse the contactor is closed or opened
            'The requested command is a maitained value

            Dim RequestCmd As Boolean = CELProject.IsTrue(_StarterContactor_RequestCmd(Index).Value)
            Dim LastCmd As Boolean = _LastStarterContactor_CommandSentToHobart(Index)

            If CELProject.IsTrue(_StarterRectifierReset.Value) Then 'Reset Hobart mean send a pulse on the starter contactor 1
                If _Pulse_Length(0) = 0 Then
                    _Pulse_Length(0) = 10
                    _StarterContactor_Cmd(0).Value = Global.KineticaRT.Values.Values.True 'Set the output value to True
                End If

            ElseIf _StarterRectifierModbusState.Value.ToString = Hobart_State(40) Then 'Hobart is in emergency stop

                _Pulse_Length(Index) = 0
                _StarterContactor_Cmd(Index).Value = Global.KineticaRT.Values.Values.False 'Set the output value to False
                _LastStarterContactor_CommandSentToHobart(Index) = False 'last command sent is stop since the Hobart is in emergency stop

            ElseIf RequestCmd <> LastCmd And _Pulse_Length(Index) = 0 Then 'The comnnand has change, no pulse being sent right now And Hobart is not in emergency stop: send a pulse to start/stop the unit

                _Pulse_Length(Index) = 10
                _StarterContactor_Cmd(Index).Value = Global.KineticaRT.Values.Values.True 'Set the output value to True

            End If

            If _Pulse_Length(Index) <> 0 Then 'Just decrease the timer until the time expired

                _Pulse_Length(Index) = _Pulse_Length(Index) - 1

                If _Pulse_Length(Index) = 0 Then
                    If CELProject.IsTrue(_StarterRectifierReset.Value) Then 'Reset Hobart mean send a pulse on the starter contactor 1

                        _LastStarterContactor_CommandSentToHobart(Index) = False 'last command sent is stop since the Hobart is in emergency stop
                        _StarterRectifierReset.Value = Global.KineticaRT.Values.Values.False 'Set the output value to False 

                    Else
                        _LastStarterContactor_CommandSentToHobart(Index) = Not LastCmd 'inverse the last command sent

                    End If

                    _StarterContactor_Cmd(Index).Value = Global.KineticaRT.Values.Values.False 'Set the output value to False
                End If
            End If

        End Sub

        Private Sub Hobart_WorkingThread()

            Do While Not ExitThread

                Thread.Sleep(100)

                'If Archive is not playing, we execute the code
                If CELProject.IsFalse(CELProject.ARCHIVE_PLAY.Value) Then

                    If ContactorEnabled(ContactorIndex_1) Then
                        ControlContactor(ContactorIndex_1)                  'Control of the contactor 1
                    End If

                    If ContactorEnabled(ContactorIndex_2) Then
                        ControlContactor(ContactorIndex_2)                  'Control of the contactor 2
                    End If

                    Hobart_ReadStarterRectifierState()                  'Retrieving the Starter Rectifier State
                    Hobart_ReadStarterRectifierFaults()                 'Retrieving the Starter Rectifier Faults
                    Hobart_ReadStarterRectifierVoltage()                'Retrieving the Starter Rectifier Output Voltage
                    Hobart_ReadStarterRectifierCurrent()                'Retrieving the Starter Rectifier Output Current
                    Hobart_ReadStarterRectifierPower()                  'Retrieving the Starter Rectifier Output Power
                    Hobart_ReadStarterRectifierCurrentLimit()           'Retrieving the Starter Rectifier Output Current Limit

                    If ContactorEnabled(ContactorIndex_1) Then
                        Hobart_ReadStarterContactorState(ContactorIndex_1)  'Retrieving the Starter Contactor 1 State
                    End If

                    If ContactorEnabled(ContactorIndex_2) Then
                        Hobart_ReadStarterContactorState(ContactorIndex_2)  'Retrieving the Starter Contactor 2 State
                    End If

                    'Connection Was Sucessful
                    If HobartConnected = True Then

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

                        'Connection was not sucessful or lost : retrying
                    Else

                        If FilterNotifyBaloon = 0 Then
                            CELDAS.NotifyIcon.ShowBalloonTip(1000, CELProject.TechnologyName, "Error : Lost Connection to Hobart Starter Rectifier : Retrying Attempting to Reconnect", ToolTipIcon.Info)
                            FilterNotifyBaloon = 3 'this way we have around 10 seconds between message 
                        End If

                        ConnectToHobartStarterRectifier()

                    End If

                    If FilterNotifyBaloon <> 0 Then
                        FilterNotifyBaloon = FilterNotifyBaloon - 1
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
                    ConnectToHobartStarterRectifier()

                    ExitThread = False

                    'Create Thread for reading
                    Hobart_WorkerThread = New Thread(AddressOf Hobart_WorkingThread)
                    Hobart_WorkerThread.Priority = ThreadPriority.Normal
                    Hobart_WorkerThread.Name = "Hobart_WorkerThread"
                    Hobart_WorkerThread.Start()
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("Hobart.Initialize: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '********************
        '* OnCloseTest()    *
        '********************
        Private Sub OnCLOSE_TEST_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try

                If CELProject.IsTrue(newVal) Then
                    ThreadHelper.StopOrKillThread(Hobart_WorkerThread, ExitThread)
                    RemoveHandlers()
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("Hobart.OnCLOSE_TEST_BTN: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '********************
        '* Hobart Faults() *
        '********************
        Private Function Hobart_Fault(ByRef faultCode As UShort) As String
            Try
                Select Case faultCode
                    Case 0 : Return "No Error"
                    Case 100 : Return "Logging Unsuccessful"
                    Case 200 : Return "Ctlr Board Fail0"
                    Case 201 : Return "Ctlr Board Fail1"
                    Case 202 : Return "Wrong Soft Version"
                    Case 300 : Return "Ctlr Volt Low 0"
                    Case 301 : Return "Ctlr Volt Low 1"
                    Case 400 : Return "ESTOP Activated"
                    Case 1000 : Return "SoftStart Error"
                    Case 1100 : Return "DC Volt Too Low"
                    Case 1200 : Return "DC Volt Too Hi 0"
                    Case 1201 : Return "DC Volt Too Hi 1"
                    Case 1202 : Return "DC Volt Too Hi 2"
                    Case 1300 : Return "DC Capacitor Sharing Error"
                    Case 1400 : Return "DC Ripple Too Hi"
                    Case 1500 : Return "Inverter Temp Too Hi"
                    Case 2000 : Return "Gate Volt Error"
                    Case 4000 : Return "Overload >100%-60s"
                    Case 4700 : Return "Filter Current Too Hi"
                    Case 4800 : Return "Short Circuit At Output"
                    Case 5000 : Return "Transformer Temp Too Hi"
                    Case 5100 : Return "Rectifier Temp Too Hi"
                    Case 6100 : Return "Plug Temp Too Hi"
                    Case 6300 : Return "EF Signal Drop Out - Output"
                    Case 7000 : Return "Output Volt Too Hi 0"
                    Case 7100 : Return "Output Volt Too Hi 1"
                    Case 7200 : Return "Output Volt Too Low"
                    Case 9000 : Return "End Of Factory Test"
                    Case Else
                        If faultCode > 2000 And faultCode < 3000 Then
                            Return "GATE VOLTAGE ERROR (" + faultCode.ToString + ")"
                        ElseIf faultCode > 4700 And faultCode < 4800 Then
                            Return "FILTER CURRENT TOO HIGH (" + faultCode.ToString + ")"
                        Else
                            Return "Invalid Error (" + faultCode.ToString + ")"
                        End If

                End Select
            Catch ex As Exception
                clsTopMostMessageBox.Show("Hobart_Faults: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        '*******************
        '* Hobart State() *
        '*******************
        Private Function Hobart_State(ByRef StateCode As UShort) As String
            Try
                Select Case StateCode
                    Case 10 : Return "Initializing"
                    Case 20 : Return "Door Open"
                    Case 30 : Return "Disabled"
                    Case 40 : Return "Error"
                    Case 50 : Return "Error Repeat"
                    Case 60 : Return "Standby"
                    Case 70 : Return "DC Softstart"
                    Case 80 : Return "AC Softstart"
                    Case 90 : Return "Run output active"
                    Case 200 : Return "Update mode"
                    Case 205 : Return "Load Config"
                    Case Else
                        Return "Invalid Code (" + StateCode.ToString + ")"

                End Select
            Catch ex As Exception
                clsTopMostMessageBox.Show("Hobart_State: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        '*************************
        '* Hobart Output State() *
        '*************************
        Private Function Hobart_Output_State(ByRef StateCode As UShort) As String
            Try
                Select Case StateCode
                    Case 1 : Return "Disabled"
                    Case 2 : Return "Ready to use"
                    Case 8 : Return "Activate"
                    Case 16 : Return "Enable Missing"
                    Case 32 : Return "90% Switch Missing"
                    Case 64 : Return "Interlock Missing"
                    Case 4096 : Return "Plug Temp High"
                    Case 8192 : Return "NCR Tripped"
                    Case 16384 : Return "Interlock Ripple High"
                    Case Else
                        Return "Invalid Code (" + StateCode.ToString + ")"

                End Select
            Catch ex As Exception
                clsTopMostMessageBox.Show("Hobart_State: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Private Client As TcpClient
        Private Master As ModbusIpMaster

        Private TcpClient As TcpClient
        Private TcpLink As Object
        Private TcpLinkStatus As Object
        Private TcpConnectionStatus As Boolean

        Private Hobart_WorkerThread As Thread
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
                ThreadHelper.StopOrKillThread(Hobart_WorkerThread, Me.ExitThread)

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



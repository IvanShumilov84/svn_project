Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms
Imports KineticaRT
Imports KineticaRT.Channels
Imports KineticaRT.Sequence
Imports KineticaRT.Values
Imports KineticaRT.Controls.Frame
Imports Microsoft.Office.Interop 'Excel
Imports System.Data
Imports System.Data.OleDb
Imports Microsoft.VisualBasic
Imports vxlapi_NET20

Imports EntitySpaces.Interfaces

Imports CEL.HMI
Imports CEL.Library
Imports CEL.Data.BusinessObjects

Namespace StudioDemonstration

    Public Class CANBus_Sequence
        Inherits CEL.KineticaRT.Sequence.Sequence

        Private CELProject As CELDAS = CELDAS.Instance()

        Private OPTION_DISPLAY As Integer = 1

        Private ReadOnly Property EngineConfiguration() As EngineInformationItem
            Get
                Return UsingConfigurationManager.engineInfoCollection.Item("APS5000A")
            End Get
        End Property

        Sub DataBindToDataGrid()
            If Not (CANBUSReceiveADataGrid Is Nothing) Then
                CANBUSReceiveADataGrid.SetDataBinding(CANBUSReceiveAChannelList)
            End If

            If Not (CANBUSReceiveBDataGrid Is Nothing) Then
                CANBUSReceiveBDataGrid.SetDataBinding(CANBUSReceiveBChannelList)
            End If

            If Not (CANBUSTransmitADataGrid Is Nothing) Then
                CANBUSTransmitADataGrid.SetDataBinding(CANBUSTransmitAChannelList)
            End If

        End Sub

        Protected Overrides Sub OnEngineWorkingThread()
            'DoNothing
        End Sub
        Public Overloads Overrides Sub Disconnect()

            Try
                ExitThread = True

                If Me.Connected = True Then
                    MyBase.Disconnect()

                    CommCHA.SetExitThread(ExitThread)
                    ThreadHelper.StopOrKillThread(rxThreadCHA, ExitThread)
                    ThreadHelper.StopOrKillThread(txThreadCHA, ExitThread)

                    CommCHB.SetExitThread(ExitThread)
                    ThreadHelper.StopOrKillThread(rxThreadCHB, ExitThread)
                    ThreadHelper.StopOrKillThread(txThreadCHB, ExitThread)

                    Me.Connected = False
                End If

            Catch ex As Exception
                MessageBox.Show("Disconnect: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Protected Overrides Sub OnTestSessionClosing(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try

                MyBase.OnTestSessionClosing(newStatus)

                If CELProject.EngineModel = "APS5000A" Then

                    ExitThread = True

                    'First Stop the thread of processing, for all engine
                    CommCHA.SetExitThread(ExitThread)
                    ThreadHelper.StopOrKillThread(rxThreadCHA, ExitThread)
                    ThreadHelper.StopOrKillThread(txThreadCHA, ExitThread)

                    CommCHB.SetExitThread(ExitThread)
                    ThreadHelper.StopOrKillThread(rxThreadCHB, ExitThread)
                    ThreadHelper.StopOrKillThread(txThreadCHB, ExitThread)

                    CleanUpCANBusLabels()

                    CANCaseXL.XL_ClosePort(portHandleCHA)
                    CANCaseXL.XL_ClosePort(portHandleCHB)
                    CANCaseXL.XL_CloseDriver()
                    CANStatus = 0

                    'Close the connection to the database
                    DatabaseHelper.CloseConnection(cnMain)

                End If

            Catch ex As Exception
                MessageBox.Show("CANBus_Sequence: OnTestSessionClosing: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionOpened(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Dim ApplConfig As Boolean = False
            Dim i As Integer
            Dim OpenDriver As Boolean = False
            Dim OpenPortCHA As Boolean = False
            Dim OpenPortCHB As Boolean = False

            Try
                MyBase.OnTestSessionOpened(newStatus)

                If CELProject.EngineModel = "APS5000A" Then

                    For i = 1 To 5
                        If CANCaseXL.XL_OpenDriver() = XLClass.XLstatus.XL_SUCCESS Then
                            i = 5
                            OpenDriver = True
                        End If
                    Next

                    If Not OpenDriver Then
                        CANStatus = 1
                        MessageBox.Show("CANBus_Sequence: OnTestSessionOpened(): Unable to open CANBus Driver", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        CANCaseXL.XL_GetDriverConfig(driverConfig)

                        'Set the Channel A Configuration
                        For i = 1 To 5
                            If CANCaseXL.XL_SetApplConfig(appName, CANBusCHA, hwType, CANcaseXL_1, CANBusCHA, busType) = XLClass.XLstatus.XL_SUCCESS Then
                                i = 5
                                ApplConfig = True
                            End If
                        Next

                        If Not ApplConfig Then
                            CANStatus = 1
                            MessageBox.Show("CANBus_Sequence: OnTestSessionOpened(): Unable to set Channel A configuration", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            accessMaskCHA = CANCaseXL.XL_GetChannelMask(hwType, CANcaseXL_1, CANBusCHA)
                        End If

                        permissionMaskCHA = accessMaskCHA
                        ApplConfig = False

                        'Set the Channel B Configuration
                        For i = 1 To 5
                            If CANCaseXL.XL_SetApplConfig(appName, CANBusCHB, hwType, CANcaseXL_1, CANBusCHB, busType) = XLClass.XLstatus.XL_SUCCESS Then
                                i = 5
                                ApplConfig = True
                            End If
                        Next

                        If Not ApplConfig Then
                            CANStatus = 1
                            MessageBox.Show("CANBus_Sequence: OnTestSessionOpened(): Unable to set Channel B configuration", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            accessMaskCHB = CANCaseXL.XL_GetChannelMask(hwType, CANcaseXL_1, CANBusCHB)
                        End If

                        permissionMaskCHB = accessMaskCHB

                        'Open port Channel A
                        For i = 1 To 5
                            If CANCaseXL.XL_OpenPort(Handle, appName, accessMaskCHA, permissionMaskCHA, rxQueueSize, busType) = XLClass.XLstatus.XL_SUCCESS Then
                                i = 5
                                OpenPortCHA = True
                            End If
                        Next

                        If Not OpenPortCHA Then
                            CANStatus = 1
                            MessageBox.Show("CANBus_Sequence: OnTestSessionOpened(): Unable to open CANBus Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            portHandleCHA = Handle
                        End If

                        'Open port Channel B
                        For i = 1 To 5
                            If CANCaseXL.XL_OpenPort(Handle, appName, accessMaskCHB, permissionMaskCHB, rxQueueSize, busType) = XLClass.XLstatus.XL_SUCCESS Then
                                i = 5
                                OpenPortCHB = True
                            End If
                        Next

                        If Not OpenPortCHB Then
                            CANStatus = 1
                            MessageBox.Show("CANBus_Sequence: OnTestSessionOpened(): Unable to open CANBus Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            portHandleCHB = Handle
                        End If
                    End If

                End If

            Catch ex As Exception
                MessageBox.Show("CANBus_Sequence: OnTestSessionOpened: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionLoaded(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Dim ActivateChannel As Boolean = False
            Dim i As Integer
            Dim SetBitRate As Boolean = False
            Dim SetNotification As Boolean = False

            Try
                MyBase.OnTestSessionLoaded(newStatus)

                If CELProject.EngineModel = "APS5000A" Then

                    ReDim eventHandle(NUMBER_OF_CANBUS_CHANNELS)

                    'Set Bitrate Channel A
                    For i = 1 To 5
                        If CANCaseXL.XL_CanSetChannelBitrate(portHandleCHA, accessMaskCHA, BITRATE) = XLClass.XLstatus.XL_SUCCESS Then
                            i = 5
                            SetBitRate = True
                        End If
                    Next

                    If Not SetBitRate Then
                        CANStatus = 1
                        MessageBox.Show("CANBus_Sequence: OnTestSessionLoaded(): Unable to set Channel A bit rate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                    'Set Bitrate Channel B
                    For i = 1 To 5
                        If CANCaseXL.XL_CanSetChannelBitrate(portHandleCHB, accessMaskCHB, BITRATE) = XLClass.XLstatus.XL_SUCCESS Then
                            i = 5
                            SetBitRate = True
                        End If
                    Next

                    If Not SetBitRate Then
                        CANStatus = 1
                        MessageBox.Show("CANBus_Sequence: OnTestSessionLoaded(): Unable to set Channel B bit rate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                    'Activate Channel A
                    For i = 1 To 5
                        If CANCaseXL.XL_ActivateChannel(portHandleCHA, accessMaskCHA, busType, flags) = XLClass.XLstatus.XL_SUCCESS Then
                            i = 5
                            ActivateChannel = True
                        End If
                    Next

                    If Not ActivateChannel Then
                        CANStatus = 1
                        MessageBox.Show("CANBus_Sequence: OnTestSessionLoaded(): Unable to activate the Channel A", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                    'Activate Channel B
                    For i = 1 To 5
                        If CANCaseXL.XL_ActivateChannel(portHandleCHB, accessMaskCHB, busType, flags) = XLClass.XLstatus.XL_SUCCESS Then
                            i = 5
                            ActivateChannel = True
                        End If
                    Next
                    If Not ActivateChannel Then
                        CANStatus = 1
                        MessageBox.Show("CANBus_Sequence: OnTestSessionLoaded(): Unable to activate the Channel B", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                    'Set notification on Channel A
                    For i = 1 To 5
                        If CANCaseXL.XL_SetNotification(portHandleCHA, eventHandle(CANBusCHA), 1) = XLClass.XLstatus.XL_SUCCESS Then
                            i = 5
                            SetNotification = True
                        End If
                    Next

                    If Not SetNotification Then
                        CANStatus = 1
                        MessageBox.Show("CANBus_Sequence: OnTestSessionLoaded(): Unable to set notification on Channel A", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                    'Set notification on Channel B
                    For i = 1 To 5
                        If CANCaseXL.XL_SetNotification(portHandleCHB, eventHandle(CANBusCHB), 1) = XLClass.XLstatus.XL_SUCCESS Then
                            i = 5
                            SetNotification = True
                        End If
                    Next

                    If Not SetNotification Then
                        CANStatus = 1
                        MessageBox.Show("CANBus_Sequence: OnTestSessionLoaded(): Unable to set notification on Channel B", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                End If

            Catch ex As Exception
                MessageBox.Show("CANBus_Sequence: OnTestSessionLoaded: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionInitialized(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)

            Try
                MyBase.OnTestSessionInitialized(newStatus)

                If CELProject.EngineModel = "APS5000A" Then

                    'Set The Connection (Global scope)
                    If Not DatabaseHelper.OpenConnection(CELProject.Database.OleDbConnectionString, cnMain, True) Then
                        'Close Test if unable to open connection to database
                        CELProject.Test_Mgr_Cmd.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Cmd.Close)
                    End If

                    EngineModel = CELProject.EngineModel.Replace(" ", "_")

                    SetupCANBusLabels()

                End If

            Catch ex As Exception
                MessageBox.Show("CANBus_Sequence: OnTestSessionInitialized: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionRunning(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                Dim Frame1 As KineticaRT.Controls.Control = Nothing
                Dim Frame2 As KineticaRT.Controls.Control

                MyBase.OnTestSessionRunning(newStatus)

                If CELProject.EngineModel = "APS5000A" Then

                    If CANStatus = 0 Then

                        ExitThread = False

                        CommCHA.SetExitThread(ExitThread)
                        CommCHB.SetExitThread(ExitThread)

                        'These Threads control message transmission/reception on Channel A
                        CommCHA.SetComm(eventHandle(CANBusCHA), portHandleCHA, accessMaskCHA, CANBUSchADictRx, CANBUSchADictTx, Me, "CMSC_POWER_BTN", CANCaseXL)
                        rxThreadCHA = New Thread(AddressOf CommCHA.RX_Thread)
                        rxThreadCHA.Priority = ThreadPriority.Normal
                        rxThreadCHA.IsBackground = True
                        rxThreadCHA.Name = "rxThreadCHA"
                        rxThreadCHA.Start()
                        txThreadCHA = New Thread(AddressOf CommCHA.TX_Thread)
                        txThreadCHA.Priority = ThreadPriority.Normal
                        txThreadCHA.IsBackground = True
                        txThreadCHA.Name = "txThreadCHA"
                        txThreadCHA.Start()

                        'This Thread controls message reception on Channel B
                        CommCHB.SetComm(eventHandle(CANBusCHB), portHandleCHB, accessMaskCHB, CANBUSchBDictRx, CANBUSchBDictTx, Me, "CMSC_POWER_BTN", CANCaseXL)
                        rxThreadCHB = New Thread(AddressOf CommCHB.RX_Thread)
                        rxThreadCHB.Priority = ThreadPriority.Normal
                        rxThreadCHB.IsBackground = True
                        rxThreadCHB.Name = "rxThreadCHB"
                        rxThreadCHB.Start()
                        txThreadCHB = New Thread(AddressOf CommCHB.TX_Thread)
                        txThreadCHB.Priority = ThreadPriority.Normal
                        txThreadCHB.IsBackground = True
                        txThreadCHB.Name = "txThreadCHB"
                        txThreadCHB.Start()
                    End If

                    If OPTION_DISPLAY Then
                        'CANBus
                        Frame1 = GetObject("APS5000A_Screen.APS5000A_EngineRun")

                        If Frame1 Is Nothing Then
                            MessageBox.Show("Frame1")
                        End If

                        Frame2 = Frame1.FindObject("CANBusARxFrame")
                        If Frame2 Is Nothing Then
                            MessageBox.Show("Frame2")
                        End If

                        'CANBus Receive Ch. A
                        CANBUSReceiveADataGrid = Frame2.FindObject("CANBusRxDataDisplay")
                        If (CANBUSReceiveADataGrid Is Nothing) Then
                            MsgBox("Missing CANBus A Rx DataGrid")
                        End If

                        Frame2 = Frame1.FindObject("CANBusBRxFrame")
                        If Frame2 Is Nothing Then
                            MessageBox.Show("Frame2")
                        End If

                        'CANBus Receive Ch. B
                        CANBUSReceiveBDataGrid = Frame2.FindObject("CANBusRxDataDisplay")
                        If (CANBUSReceiveBDataGrid Is Nothing) Then
                            MsgBox("Missing CANBus B Rx DataGrid")
                        End If

                        Frame2 = Frame1.FindObject("CANBusATxFrame")
                        If Frame2 Is Nothing Then
                            MessageBox.Show("Frame2")
                        End If

                        'CANBus Transmit Ch. A
                        CANBUSTransmitADataGrid = Frame2.FindObject("CANBusTxDataDisplay")
                        If (CANBUSTransmitADataGrid Is Nothing) Then
                            MsgBox("Missing CANBus A Tx DataGrid")
                        End If

                        Me.DataBindToDataGrid()
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show("CANBus_Sequence: OnTestSessionRunning: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Sub CleanUpCANBusLabels()
            Dim IsDone As Boolean
            Dim i, j, k As Integer
            Dim ChannelsInfo As KineticaRT.Studio.Application.Project.ChannelsInfo
            Dim CurCANBusChannelName As String = Nothing
            Dim CurCANBusKTChannel As KineticaRT.Channels.NumericChannel
            Dim HMIProject As KineticaRT.Studio.Application.Project

            For i = 0 To 1
                For j = 0 To 1
                    IsDone = False
                    k = 0

                    While (Not (IsDone))
                        k += 1

                        Select Case i
                            Case CANBusRX
                                CurCANBusChannelName = "User_Int_CANBusIN"
                            Case CANBusTX
                                CurCANBusChannelName = "User_Int_CANBusOUT"
                        End Select

                        Select Case j
                            Case CANBusCHA
                                CurCANBusChannelName += "_CHA_"
                            Case CANBusCHB
                                CurCANBusChannelName += "_CHB_"
                        End Select

                        CurCANBusChannelName += k.ToString()
                        CurCANBusKTChannel = GetChannel(CurCANBusChannelName)
                        If Not (CurCANBusKTChannel Is Nothing) Then
                            CurCANBusKTChannel.Units = ""
                            CurCANBusKTChannel.Format = "G"
                            CurCANBusKTChannel.Description = ""
                        Else
                            IsDone = True
                        End If
                    End While
                Next j
            Next i

            Me.CANBUSReceiveAChannelList.Clear()
            Me.CANBUSReceiveBChannelList.Clear()
            Me.CANBUSTransmitAChannelList.Clear()

            'For each entry in the dictionary, clear the associated list
            For Each Item As Generic.KeyValuePair(Of String, List(Of CANBusMsg)) In CANBUSchADictRx
                Item.Value.Clear()
            Next
            'Clear the dictionary
            CANBUSchADictRx.Clear()

            'For each entry in the dictionary, clear the associated list
            For Each Item As Generic.KeyValuePair(Of String, List(Of CANBusMsg)) In CANBUSchADictTx
                Item.Value.Clear()
            Next
            'Clear the dictionary
            CANBUSchADictTx.Clear()

            'For each entry in the dictionary, clear the associated list
            For Each Item As Generic.KeyValuePair(Of String, List(Of CANBusMsg)) In CANBUSchBDictRx
                Item.Value.Clear()
            Next
            'Clear the dictionary
            CANBUSchBDictRx.Clear()

            'For each entry in the dictionary, clear the associated list
            For Each Item As Generic.KeyValuePair(Of String, List(Of CANBusMsg)) In CANBUSchBDictTx
                Item.Value.Clear()
            Next
            'Clear the dictionary
            CANBUSchBDictTx.Clear()

            'Initialise project 
            HMIProject = GetObject("Project")

            If HMIProject Is Nothing Then
                MessageBox.Show("Error: Project Is Nothing")
                Exit Sub
            End If

            ChannelsInfo = HMIProject.GetChannelsInfo(EngineModel + "_CANBus")
            If Not (ChannelsInfo Is Nothing) Then
                HMIProject.RemoveChannels(ChannelsInfo)
            End If

        End Sub

        Sub SetupCANBusLabels()

            Dim CANBusCollection As KineticaRT.ChannelCollection
            Dim NumericLink_Channel As INumericChannel

            Try
                'Initialise project 
                HMIProject = GetObject("Project")

                If HMIProject Is Nothing Then
                    MessageBox.Show("SetupCANBusLabels: HMIProject is not defined")
                    Exit Sub
                End If

                CANBusCollection = GetObject("CANBus")

                If CANBusCollection Is Nothing Then
                    MessageBox.Show("SetupCANBusLabels: CANBusCollection CANBus is not defined")
                    Exit Sub
                End If

                Dim dacnCANBusChannelCollection As Dacn1CanbusLabelCollection = New Dacn1CanbusLabelCollection()

                dacnCANBusChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                dacnCANBusChannelCollection.Query.Where(dacnCANBusChannelCollection.Query.EngineModel = CELProject.EngineModel And dacnCANBusChannelCollection.Query.Unavailable = False)
                dacnCANBusChannelCollection.Query.OrderBy(dacnCANBusChannelCollection.Query.Description.Ascending)
                dacnCANBusChannelCollection.Query.Load()

                For Each channel As Dacn1CanbusLabel In dacnCANBusChannelCollection

                    NumericLink_Channel = GetChannel(channel.ChannelName)

                    If NumericLink_Channel Is Nothing Then
                        MessageBox.Show("SetupCANBusLabels: Missing CANBus channel : " & channel.ChannelName)
                        Exit Sub
                    End If

                    'Create CANBus Labels
                    Dim CANBusLabel As New CANBusMsg

                    CANBusLabel.SignedInt = channel.SignedInteger
                    CANBusLabel.IEEE754Type = channel.IEEE754Type
                    CANBusLabel.LSB = channel.Lsb
                    CANBusLabel.LSBResol = channel.LSBResolution
                    CANBusLabel.MSB = channel.Msb
                    CANBusLabel.Word = channel.Word
                    CANBusLabel.KineticaRTLabel = NumericLink_Channel

                    'Add the CANBus Label to the correct dictionary
                    Select Case channel.TxRx

                        Case CANBusRX
                            Select Case channel.CANBusChannel
                                Case CANBusCHA

                                    'If this is a new packet, add it to the directory
                                    If Not CANBUSchADictRx.ContainsKey(channel.CANBusID) Then
                                        CANBUSchADictRx.Add(channel.CANBusID, New List(Of CANBusMsg))
                                    End If

                                    CANBUSchADictRx(channel.CANBusID).Add(CANBusLabel)

                                Case CANBusCHB

                                    'If this is a new packet, add it to the directory
                                    If Not CANBUSchBDictRx.ContainsKey(channel.CANBusID) Then
                                        CANBUSchBDictRx.Add(channel.CANBusID, New List(Of CANBusMsg))
                                    End If

                                    CANBUSchBDictRx(channel.CANBusID).Add(CANBusLabel)
                            End Select

                        Case CANBusTX
                            Select Case channel.CANBusChannel
                                Case CANBusCHA

                                    'If this is a new packet, add it to the directory
                                    If Not CANBUSchADictTx.ContainsKey(channel.CANBusID) Then
                                        CANBUSchADictTx.Add(channel.CANBusID, New List(Of CANBusMsg))
                                    End If

                                    CANBUSchADictTx(channel.CANBusID).Add(CANBusLabel)

                                Case CANBusCHB

                                    'If this is a new packet, add it to the directory
                                    If Not CANBUSchBDictTx.ContainsKey(channel.CANBusID) Then
                                        CANBUSchBDictTx.Add(channel.CANBusID, New List(Of CANBusMsg))
                                    End If

                                    CANBUSchBDictTx(channel.CANBusID).Add(CANBusLabel)
                            End Select
                    End Select

                    'Add the Numeric Channels to the correct display list if Display is requested
                    If channel.DisplayOnCELDAS Then

                        Select Case channel.TxRx

                            Case CANBusRX
                                Select Case channel.CANBusChannel
                                    Case CANBusCHA
                                        CANBUSReceiveAChannelList.Add(New CEL.ARINCChannel(NumericLink_Channel))
                                    Case CANBusCHB
                                        CANBUSReceiveBChannelList.Add(New CEL.ARINCChannel(NumericLink_Channel))
                                End Select

                            Case CANBusTX
                                Select Case channel.CANBusChannel
                                    Case CANBusCHA
                                        CANBUSTransmitAChannelList.Add(New CEL.ARINCChannel(NumericLink_Channel))
                                End Select
                        End Select
                    End If
                Next

            Catch exception As System.Exception
                MessageBox.Show("CANBus_Sequence : SetupCANBusLabels: " & exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        '********************************************************************
        '* Private Members **************************************************
        '********************************************************************
        '********
        '* Misc *
        '********
        Private appChannel As UInt32
        Private accessMaskCHA As UInt64
        Private accessMaskCHB As UInt64
        Private appName As String = "CELDAS200_CANBus"
        Private busType As Integer = CInt(XLClass.XLbusTypes.XL_BUS_TYPE_CAN)
        Private CANBUSchADictRx As New Dictionary(Of String, List(Of CANBusMsg))
        Private CANBUSchBDictRx As New Dictionary(Of String, List(Of CANBusMsg))
        Private CANBUSchADictTx As New Dictionary(Of String, List(Of CANBusMsg))
        Private CANBUSchBDictTx As New Dictionary(Of String, List(Of CANBusMsg))
        Private CANBUSReceiveAChannelList As New CEL.ARINCChannelCollection
        Private CANBUSReceiveADataGrid As CEL.Controls.MultiChannelDisplay
        Private CANBUSReceiveBChannelList As New CEL.ARINCChannelCollection
        Private CANBUSReceiveBDataGrid As CEL.Controls.MultiChannelDisplay
        Private CANBUSTransmitAChannelList As New CEL.ARINCChannelCollection
        Private CANBUSTransmitADataGrid As CEL.Controls.MultiChannelDisplay
        Private CANCaseXL As New XLDriver()
        Private ChannelsInfo As KineticaRT.Studio.Application.Project.ChannelsInfo
        Private CommCHA As CANBusCommunication = New CANBusCommunication
        Private CommCHB As CANBusCommunication = New CANBusCommunication
        Private cnMain As ADODB.Connection
        Private driverConfig As New XLClass.xl_driver_config
        Private eventHandle() As Int32
        Private EngineModel As String
        Private ExitThread As Boolean = False
        Private flags As UInt32 = CInt(XLClass.XLChannelFlags.XL_ACTIVATE_RESET_CLOCK)
        Private Handle As Int32
        Private HMIProject As KineticaRT.Studio.Application.Project
        Private hwChannel As UInt32
        Private hwIndex As UInt32
        Private hwType As UInt32 = CInt(XLClass.XLhwTypes.XL_HWTYPE_CANCASEXL)
        Private permissionMaskCHA As UInt64
        Private permissionMaskCHB As UInt64
        Private portHandleCHA As Int32
        Private portHandleCHB As Int32
        Private rxQueueSize As Int32 = 1024
        Private Status As Int32

        Private CANStatus As Integer = 0

        Const BITRATE As UInt32 = 500000
        Const CANcaseXL_1 As UInt32 = 0
        Const CANBusCHA As UInt32 = 0
        Const CANBusCHB As UInt32 = 1
        Const CANBusRX As UInt32 = 0
        Const CANBusTX As UInt32 = 1
        Const NUMBER_OF_CANBUS_CHANNELS As Integer = 2

        '**********
        '* Thread *
        '**********
        Private rxThreadCHA As Thread = Nothing
        Private rxThreadCHB As Thread = Nothing
        Private txThreadCHA As Thread = Nothing
        Private txThreadCHB As Thread = Nothing

    End Class

    Class CANBusCommunication

        Private accessMask As UInt64
        Private CANCaseXL As XLDriver
        Private Channel As UInt32
        Private eventHandle As Int32
        Private ElapsedTimeSinceValidMessage As System.TimeSpan
        Private ExitThread As Boolean = False
        Private LastValidMessageTime As Date
        Private portHandle As Int32
        Private ReceptionOn As IChannel
        Private RxDictionary As New Dictionary(Of String, List(Of CANBusMsg))
        Private TxDictionary As New Dictionary(Of String, List(Of CANBusMsg))

        Private CELProject As CELDAS = CELDAS.Instance()

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Shared Function WaitForSingleObject(ByVal handle As Integer, ByVal timeOut As Integer) As Integer
        End Function

        Private Enum WaitResults : int

            WAIT_ABANDONED = &H80
            WAIT_FAILED = &HFFFFFFF
            WAIT_OBJECT_0 = &H0
            WAIT_TIMEOUT = &H102
            INFINITE = &HFFFF
        End Enum

        Public Sub RX_Thread()

            Dim receivedEvent As XLClass.xl_event = New XLClass.xl_event
            Dim xlStatus As XLClass.XLstatus = XLClass.XLstatus.XL_SUCCESS
            Dim waitResult As WaitResults = New WaitResults
            Dim Value As Double

            Try

                Do While Not ExitThread
                    ' wait for hardware events
                    waitResult = WaitForSingleObject(eventHandle, 1000)

                    ' if event occured... 
                    If (waitResult <> WaitResults.WAIT_TIMEOUT) Then

                        ' init xlStatus
                        xlStatus = XLClass.XLstatus.XL_SUCCESS

                        ' while hw queue not empty
                        While (xlStatus <> XLClass.XLstatus.XL_ERR_QUEUE_IS_EMPTY)

                            ' ...receive data from hardware queue...
                            xlStatus = CANCaseXL.XL_Receive(portHandle, receivedEvent)

                            '  if receive succeed....
                            If (xlStatus = XLClass.XLstatus.XL_SUCCESS) Then

                                ' ...print rx data
                                If (receivedEvent.tag = XLClass.XLeventType.XL_RECEIVE_MSG) Then ' ...and data was Rx msg...

                                    ' print only "real" Rx messages
                                    If ((System.Convert.ToInt32(receivedEvent.tagData.can_Msg.flags) And System.Convert.ToInt32(XLClass.XLmessageFlags.XL_CAN_MSG_FLAG_TX_COMPLETED)) = 0) Then
                                        If receivedEvent.tagData.can_Msg.id.ToString("x").Length > 6 Then
                                            If RxDictionary.ContainsKey(receivedEvent.tagData.can_Msg.id.ToString("x").ToUpper.Substring(0, 6)) Then
                                                For Each Item As CANBusMsg In RxDictionary(receivedEvent.tagData.can_Msg.id.ToString("x").ToUpper.Substring(0, 6))

                                                    Value = Item.ExtractValue(receivedEvent)

                                                    If Value <> Item.KineticaRTLabel.Value.ToDouble Then
                                                        If Value = 0 Then
                                                            Item.KineticaRTLabel.Value = Values.Zero

                                                        ElseIf Value = 1 Then
                                                            Item.KineticaRTLabel.Value = Values.One

                                                        Else
                                                            Item.KineticaRTLabel.Value = New KineticaRT.Values.Double(Value)

                                                        End If
                                                    End If

                                                Next
                                                'Last time we received a valid message
                                                LastValidMessageTime = Now
                                            End If
                                        End If
                                    End If

                                End If
                            End If
                        End While
                    End If

                    ElapsedTimeSinceValidMessage = Now.Subtract(LastValidMessageTime)
                    ' If no valid message for 2 seconds
                    If ElapsedTimeSinceValidMessage.TotalSeconds > 2.0 Then
                        'For each packet...
                        For Each Item As Generic.KeyValuePair(Of String, List(Of CANBusMsg)) In RxDictionary
                            'For each message in the packet...
                            For Each Item2 As CANBusMsg In RxDictionary(Item.Key.ToString)
                                'Reset the value to zero
                                Item2.KineticaRTLabel.Value = Values.NaN
                            Next
                        Next
                    End If
                Loop

            Catch exception As System.Exception
                MessageBox.Show("CANBus_Sequence : RX_Thread: " & exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End Sub

        Public Sub SetComm(ByVal eh As UInt32, ByVal ph As UInt32, ByVal am As UInt64, ByVal RxDict As Dictionary(Of String, List(Of CANBusMsg)), ByVal TxDict As Dictionary(Of String, List(Of CANBusMsg)), sequence As KineticaRT.Sequence.Sequence, ro As String, CANCaseXLDriver As XLDriver)
            CANCaseXL = CANCaseXLDriver
            accessMask = am
            ReceptionOn = sequence.GetChannel(ro)

            If ReceptionOn Is Nothing Then
                MessageBox.Show("SetComm: ReceptionOn : Kineticart Channel not found" & ro)
            End If

            eventHandle = eh
            portHandle = ph
            RxDictionary = RxDict
            TxDictionary = TxDict
        End Sub

        Public Sub SetExitThread(ByVal et As Boolean)
            ExitThread = et
        End Sub

        Public Sub TX_Thread()
            Dim transmittedEvent As New XLClass.xl_event
            Dim xlStatus As XLClass.XLstatus = XLClass.XLstatus.XL_SUCCESS
            Dim waitResult As WaitResults = New WaitResults

            Dim i As Integer
            Dim dataBytes(8) As Byte
            Dim ts As System.TimeSpan
            Dim Transmit_Start As Date
            Dim Transmit_Stop As Date

            Do While Not ExitThread
                Transmit_Start = Now
                For Each Item As KeyValuePair(Of String, List(Of CANBusMsg)) In TxDictionary
                    transmittedEvent.tag = System.Convert.ToByte(XLClass.XLeventType.XL_TRANSMIT_MSG)
                    transmittedEvent.tagData.can_Msg.id = UInt32.Parse(Item.Key, Globalization.NumberStyles.AllowHexSpecifier)
                    transmittedEvent.tagData.can_Msg.dlc = System.Convert.ToUInt16(8)
                    For i = 0 To 7
                        transmittedEvent.tagData.can_Msg.data(i) = System.Convert.ToByte(0)
                    Next
                    For Each Msg As CANBusMsg In Item.Value
                        dataBytes = Msg.EncodeValue(Msg.KineticaRTLabel.Value.ToDouble)
                        For i = 0 To 7
                            transmittedEvent.tagData.can_Msg.data(i) = transmittedEvent.tagData.can_Msg.data(i) Or dataBytes(i)
                        Next
                    Next
                    xlStatus = CANCaseXL.XL_CanTransmit(portHandle, accessMask, transmittedEvent)
                Next
                Transmit_Stop = Now
                ts = Transmit_Stop.Subtract(Transmit_Start)
                If (40 - ts.TotalMilliseconds) > 0 Then
                    Thread.Sleep(40 - ts.TotalMilliseconds)
                Else
                    Thread.Sleep(0)   'give the time to other thread to be executed
                End If
            Loop
        End Sub

    End Class

    Class CANBusMsg
        Public KineticaRTLabel As IChannel
        Public Word As Integer
        Public MSB As Integer
        Public LSB As Integer
        Public LSBResol As Double
        Public IEEE754Type As Boolean
        Public SignedInt As Boolean

        Private Function CalculateStartByte() As Integer
            Dim Startbyte As Integer
            Startbyte = 4 * (Word - 1) + 3 - MSB \ 8
            Return Startbyte
        End Function

        Private Function CalculateStopByte() As Integer
            Dim Stopbyte As Integer
            Stopbyte = 4 * (Word - 1) + 3 - LSB \ 8
            Return Stopbyte
        End Function

        Public Function EncodeValue(ByVal ValueToTransmit As Double) As Byte()
            Dim dataByte(7) As Byte
            Dim i As Integer
            Dim mask As Byte = 255
            Dim Startbyte As Integer
            Dim Stopbyte As Integer
            Dim TempLSB As Integer = LSB

            Startbyte = CalculateStartByte()
            Stopbyte = CalculateStopByte()

            ValueToTransmit = ValueToTransmit / LSBResol

            If Double.IsNaN(ValueToTransmit) Then
                ValueToTransmit = 0
            End If

            If IEEE754Type Then
                Dim IEEE754Conversion(4) As Byte
                IEEE754Conversion = BitConverter.GetBytes(CSng(ValueToTransmit))
                For i = 0 To 3
                    dataByte(Stopbyte - i) = IEEE754Conversion(i)
                Next
            ElseIf Startbyte = Stopbyte Then
                While TempLSB > 7
                    TempLSB -= 8
                End While
                dataByte(Startbyte) = ValueToTransmit << TempLSB Or dataByte(Startbyte)
            Else
                For i = 7 To 0 Step -1
                    If i < Startbyte Or i > Stopbyte Then
                        dataByte(i) = System.Convert.ToByte(0)
                    Else
                        dataByte(i) = ValueToTransmit And mask
                        ValueToTransmit >>= 8
                    End If
                Next
            End If

            Return dataByte

        End Function

        Public Function ExtractValue(ByVal receivedEvent As XLClass.xl_event) As Double
            Dim i As Integer
            Dim j As Integer
            Dim Startbyte As Integer
            Dim Stopbyte As Integer
            Dim TempLSB As Integer = LSB
            Dim TempMSB As Integer = MSB
            Dim Value As Double = 0.0
            Dim dataByte(3) As Byte

            Startbyte = CalculateStartByte()
            Stopbyte = CalculateStopByte()

            If Startbyte = Stopbyte Then
                While TempMSB > 7
                    TempMSB -= 8
                    TempLSB -= 8
                End While
                For i = TempMSB To TempLSB Step -1
                    Value = 2 * Value + (receivedEvent.tagData.can_Msg.data(Startbyte) >> i And 1)
                Next
            Else
                If (Stopbyte - Startbyte) > 3 Then
                    ReDim dataByte(7)
                    j = 7 - (Stopbyte - Startbyte)
                    For i = Stopbyte To Startbyte Step -1
                        dataByte(j) = receivedEvent.tagData.can_Msg.data(i)
                        j += 1
                    Next
                    If SignedInt Then
                        Value = BitConverter.ToInt64(dataByte, 0)
                    Else
                        Value = BitConverter.ToUInt64(dataByte, 0)
                    End If
                    For i = 1 To 7 - (Stopbyte - Startbyte)
                        Value = Value / 256
                    Next
                Else
                    j = 3 - (Stopbyte - Startbyte)
                    For i = Stopbyte To Startbyte Step -1
                        dataByte(j) = receivedEvent.tagData.can_Msg.data(i)
                        j += 1
                    Next
                    If SignedInt Then
                        Value = BitConverter.ToInt32(dataByte, 0)
                    Else
                        Value = BitConverter.ToUInt32(dataByte, 0)
                    End If
                    For i = 1 To 3 - (Stopbyte - Startbyte)
                        Value = Value / 256
                    Next
                End If
            End If

            Value = Value * LSBResol

            Return Value
        End Function

    End Class
End Namespace

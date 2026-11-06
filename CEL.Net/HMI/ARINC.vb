'Option Strict On
'Imports Excel
Imports KineticaRT
Imports KineticaRT.Sequence
Imports KineticaRT.Servers.OpcClient
Imports KineticaRT.Logic.Message
Imports KineticaRT.Values
Imports KineticaRT.Channels
Imports KineticaRT.Studio.Application
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.OleDb
Imports System.Diagnostics
Imports System.IO
Imports System.Security
Imports System.Security.Permissions
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms

Imports EntitySpaces.Interfaces
Imports CEL.Data.BusinessObjects
Imports CEL.Data.BusinessObjects.ARINC429DiscreteTemplate
Imports CEL.Data.BusinessObjects.ARINC429LabelTemplate
Imports KineticaRT.Studio.Application.Project

Namespace CEL.HMI

    Public Class ARINC

        Private CELProject As CELDAS = CELDAS.Instance()

        Dim frmHmiMaster As System.Windows.Forms.Control = Nothing
        Dim frmHmiSlave As System.Windows.Forms.Control = Nothing


        Const ArincLabelFormatBCD As Byte = 1
        Const ArincLabelFormatBNR As Byte = 0
        Const ArincLabelFormatDiscrete As Byte = 2
        Const ArincLabelFormatPW100LRUCode As Byte = 3

        Const UARTDataType32BitDiscrete As Byte = 4
        Const UARTDataType32BitInteger As Byte = 3
        Const UARTDataTypeDiscrete As Byte = 2
        Const UARTDataTypeReal As Byte = 1
        Const UARTDataTypeInteger As Byte = 0
        Const UARTChannelA As Byte = 0
        Const UARTChannelB As Byte = 1
        Const UARTSourceEec As String = "EEC"
        Const UARTSourceCockpit As String = "Cockpit"

        '********************************************************************
        '* Variable Declaration *********************************************
        '********************************************************************		

        'ARINC
        Private arincRXDiscreteHandlers As System.Collections.ArrayList 'Used in SetupArincFault
        Private arincTXDiscreteHandlers As System.Collections.ArrayList

        Private arincDiscreteRXHandlers As System.Collections.ArrayList 'Used in SetupArincDiscrete
        Private arincDiscreteTXHandlers As System.Collections.ArrayList

        Private arincReceiveAChannelList As CEL.ARINCChannelCollection
        Private arincReceiveBChannelList As CEL.ARINCChannelCollection
        Private arincTransmitAChannelList As CEL.ARINCChannelCollection
        Private arincTransmitBChannelList As CEL.ARINCChannelCollection
        Private arincFaultAChannelList As CEL.ACMCChannelCollection
        Private arincFaultBChannelList As CEL.ACMCChannelCollection
        Private arincStatusAChannelList As CEL.ACMCChannelCollection
        Private arincStatusBChannelList As CEL.ACMCChannelCollection
        Private arincReceiveADataGrid As CEL.Controls.MultiChannelDisplay = Nothing
        Private arincReceiveBDataGrid As CEL.Controls.MultiChannelDisplay = Nothing
        Private arincTransmitADataGrid As CEL.Controls.MultiChannelDisplay = Nothing
        Private arincTransmitBDataGrid As CEL.Controls.MultiChannelDisplay = Nothing

        Private arincAFaultView As CEL.Controls.FaultView
        Private arincAStatusView As CEL.Controls.FaultView
        Private arincBFaultView As CEL.Controls.FaultView
        Private arincBStatusView As CEL.Controls.FaultView
        Private arincKineticaRTPW100LRUCodeChannel As IChannel
        Private arincKineticaRTPW100LRUDescriptionChannel As IChannel
        Private CallDataBindToDataGrid As New MethodInvoker(AddressOf Me.DataBindToDataGrid)
        Private PW100LRUCodeDescriptions(99) As Global.KineticaRT.Values.Text

        'Development UART
        Private devUartRxDiscreteHandlers As System.Collections.ArrayList 'Used in SetupUartDiscrete

        Private devUartRxChannelListChA As CEL.ARINCChannelCollection
        Private devUartRxChannelListChB As CEL.ARINCChannelCollection
        Private devUartFaultChannelListChA As CEL.ACMCChannelCollection
        Private devUartFaultChannelListChB As CEL.ACMCChannelCollection
        Private devUartStatusChannelListChA As CEL.ACMCChannelCollection
        Private devUartStatusChannelListChB As CEL.ACMCChannelCollection
        Private devUartDataGridChA As CEL.Controls.MultiChannelDisplay = Nothing
        Private devUartDataGridChB As CEL.Controls.MultiChannelDisplay = Nothing

        Private devUartFaultViewChA As CEL.Controls.FaultView
        Private devUartStatusViewChA As CEL.Controls.FaultView
        Private devUartFaultViewChB As CEL.Controls.FaultView
        Private devUartStatusViewChB As CEL.Controls.FaultView

        'Cockpit UART
        Private cockpitUartRxDiscreteHandlers As System.Collections.ArrayList 'Used in SetupUartDiscrete

        Private cockpitUartRxChannelListChA As CEL.ARINCChannelCollection
        Private cockpitUartRxChannelListChB As CEL.ARINCChannelCollection
        Private cockpitUartFaultChannelListChA As CEL.ACMCChannelCollection
        Private cockpitUartFaultChannelListChB As CEL.ACMCChannelCollection
        Private cockpitUartStatusChannelListChA As CEL.ACMCChannelCollection
        Private cockpitUartStatusChannelListChB As CEL.ACMCChannelCollection
        Private cockpitUartDataGridChA As CEL.Controls.MultiChannelDisplay = Nothing
        Private cockpitUartDataGridChB As CEL.Controls.MultiChannelDisplay = Nothing

        Private cockpitUartFaultViewChA As CEL.Controls.FaultView
        Private cockpitUartStatusViewChA As CEL.Controls.FaultView
        Private cockpitUartFaultViewChB As CEL.Controls.FaultView
        Private cockpitUartStatusViewChB As CEL.Controls.FaultView

        Private DicArchiveChannelList As New Collections.Generic.Dictionary(Of String, Integer) 'used to keep channels that will be recorded and their recorder Index

        Public Sub New()
            MyBase.New()

            Dim i As Integer
            Me.arincRXDiscreteHandlers = New System.Collections.ArrayList
            Me.arincTXDiscreteHandlers = New System.Collections.ArrayList
            Me.arincReceiveAChannelList = New CEL.ARINCChannelCollection
            Me.arincReceiveBChannelList = New CEL.ARINCChannelCollection
            Me.arincTransmitAChannelList = New CEL.ARINCChannelCollection
            Me.arincTransmitBChannelList = New CEL.ARINCChannelCollection
            Me.arincFaultAChannelList = New CEL.ACMCChannelCollection
            Me.arincFaultBChannelList = New CEL.ACMCChannelCollection
            Me.arincStatusAChannelList = New CEL.ACMCChannelCollection
            Me.arincStatusBChannelList = New CEL.ACMCChannelCollection

            Me.devUartRxDiscreteHandlers = New System.Collections.ArrayList
            Me.devUartRxChannelListChA = New CEL.ARINCChannelCollection
            Me.devUartRxChannelListChB = New CEL.ARINCChannelCollection
            Me.devUartFaultChannelListChA = New CEL.ACMCChannelCollection
            Me.devUartFaultChannelListChB = New CEL.ACMCChannelCollection
            Me.devUartStatusChannelListChA = New CEL.ACMCChannelCollection
            Me.devUartStatusChannelListChB = New CEL.ACMCChannelCollection

            Me.cockpitUartRxDiscreteHandlers = New System.Collections.ArrayList
            Me.cockpitUartRxChannelListChA = New CEL.ARINCChannelCollection
            Me.cockpitUartRxChannelListChB = New CEL.ARINCChannelCollection
            Me.cockpitUartFaultChannelListChA = New CEL.ACMCChannelCollection
            Me.cockpitUartFaultChannelListChB = New CEL.ACMCChannelCollection
            Me.cockpitUartStatusChannelListChA = New CEL.ACMCChannelCollection
            Me.cockpitUartStatusChannelListChB = New CEL.ACMCChannelCollection

            For i = 0 To Me.PW100LRUCodeDescriptions.GetUpperBound(0)
                Me.PW100LRUCodeDescriptions(i) = Values.EmptyString
            Next
        End Sub

        Public Sub UpdateLayout(ByVal arincConfiguration As CEL.HMI.EngineInformationItem.eArincConfiguration)

            'ARINC 
            If Not (arincReceiveADataGrid Is Nothing) Then
                If arincReceiveAChannelList.Count = 0 Or arincConfiguration <> EngineInformationItem.eArincConfiguration.Omnibusbox Then
                    Me.arincReceiveADataGrid.Parent.Parent.Visible = False
                Else
                    Me.arincReceiveADataGrid.Parent.Parent.Visible = True

                End If
            End If
            If Not (arincReceiveBDataGrid Is Nothing) Then
                If arincReceiveBChannelList.Count = 0 Or arincConfiguration <> EngineInformationItem.eArincConfiguration.Omnibusbox Then
                    Me.arincReceiveBDataGrid.Parent.Parent.Visible = False
                Else
                    Me.arincReceiveBDataGrid.Parent.Parent.Visible = True

                End If
            End If
            If Not (arincTransmitADataGrid Is Nothing) Then
                If arincTransmitAChannelList.Count = 0 Or arincConfiguration <> EngineInformationItem.eArincConfiguration.Omnibusbox Then
                    Me.arincTransmitADataGrid.Parent.Parent.Visible = False
                Else
                    Me.arincTransmitADataGrid.Parent.Parent.Visible = True

                End If
            End If
            If Not (arincTransmitBDataGrid Is Nothing) Then
                If arincTransmitBChannelList.Count = 0 Or arincConfiguration <> EngineInformationItem.eArincConfiguration.Omnibusbox Then
                    Me.arincTransmitBDataGrid.Parent.Parent.Visible = False
                Else
                    Me.arincTransmitBDataGrid.Parent.Parent.Visible = True
                End If
            End If

            If Not (arincAFaultView Is Nothing) Then
                If arincFaultAChannelList.Count = 0 Or arincConfiguration <> EngineInformationItem.eArincConfiguration.Omnibusbox Then
                    Me.arincAFaultView.Parent.Parent.Visible = False
                Else
                    Me.arincAFaultView.Parent.Parent.Visible = True
                End If
            End If
            If Not (arincBFaultView Is Nothing) Then
                If arincFaultBChannelList.Count = 0 Or arincConfiguration <> EngineInformationItem.eArincConfiguration.Omnibusbox Then
                    Me.arincBFaultView.Parent.Parent.Visible = False
                Else
                    Me.arincBFaultView.Parent.Parent.Visible = True
                End If
            End If
            If Not (arincAStatusView Is Nothing) Then
                If arincStatusAChannelList.Count = 0 Or arincConfiguration <> EngineInformationItem.eArincConfiguration.Omnibusbox Then
                    Me.arincAStatusView.Parent.Parent.Visible = False
                Else
                    Me.arincAStatusView.Parent.Parent.Visible = True
                End If
            End If
            If Not (arincBStatusView Is Nothing) Then
                If arincStatusBChannelList.Count = 0 Or arincConfiguration <> EngineInformationItem.eArincConfiguration.Omnibusbox Then
                    Me.arincBStatusView.Parent.Parent.Visible = False
                Else
                    Me.arincBStatusView.Parent.Parent.Visible = True
                End If
            End If

            'UART
            UpdateUartLayout()

            'Honeywell
            Dim frameHoneywell As Global.KineticaRT.Controls.Frame = Nothing
            If Not IsNothing(frmHmiMaster) Then
                For i As Integer = 1 To 10
                    frameHoneywell = CType(CELProject.SearchControl(frmHmiMaster, "fraArincHoneywell" + i.ToString), Global.KineticaRT.Controls.Frame)
                    If Not IsNothing(frameHoneywell) Then
                        frameHoneywell.Visible = (arincConfiguration = EngineInformationItem.eArincConfiguration.HoneywellComputer)
                    End If
                Next
            ElseIf Not IsNothing(frmHmiSlave) Then
                For i As Integer = 1 To 10
                    frameHoneywell = CType(CELProject.SearchControl(frmHmiSlave, "fraArincHoneywell" + i.ToString), Global.KineticaRT.Controls.Frame)
                    If Not IsNothing(frameHoneywell) Then
                        frameHoneywell.Visible = (arincConfiguration = EngineInformationItem.eArincConfiguration.HoneywellComputer)
                    End If
                Next
            End If


        End Sub

        Sub DataBindToDataGrid()
            'Arinc
            If Not (arincReceiveADataGrid Is Nothing) AndAlso arincReceiveAChannelList.Count > 0 Then
                arincReceiveADataGrid.SetDataBinding(arincReceiveAChannelList)
            End If
            If Not (arincReceiveBDataGrid Is Nothing) AndAlso arincReceiveBChannelList.Count > 0 Then
                arincReceiveBDataGrid.SetDataBinding(arincReceiveBChannelList)
            End If
            If Not (arincTransmitADataGrid Is Nothing) AndAlso arincTransmitAChannelList.Count > 0 Then
                arincTransmitADataGrid.SetDataBinding(arincTransmitAChannelList)
            End If
            If Not (arincTransmitBDataGrid Is Nothing) AndAlso arincTransmitBChannelList.Count > 0 Then
                arincTransmitBDataGrid.SetDataBinding(arincTransmitBChannelList)
            End If
            If Not (arincAFaultView Is Nothing) AndAlso arincFaultAChannelList.Count > 0 Then
                arincAFaultView.SetDataBinding(arincFaultAChannelList)
            End If
            If Not (arincBFaultView Is Nothing) AndAlso arincFaultBChannelList.Count > 0 Then
                arincBFaultView.SetDataBinding(arincFaultBChannelList)
            End If
            If Not (arincAStatusView Is Nothing) AndAlso arincStatusAChannelList.Count > 0 Then
                arincAStatusView.SetDataBinding(arincStatusAChannelList)
            End If
            If Not (arincBStatusView Is Nothing) AndAlso arincStatusBChannelList.Count > 0 Then
                arincBStatusView.SetDataBinding(arincStatusBChannelList)
            End If

            UartDataBindToDataGrid()

        End Sub

        Private Sub FindAndInitializeWindow(ByVal engineModel As String)
            If engineModel = "SYSTEM CHECK" Or engineModel = "DAS CALIBRATION" Then
                Exit Sub
            End If


            'Query Database
            esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
            Dim windowCollection As UUTHMIWindowCollection = New UUTHMIWindowCollection()
            windowCollection.es.Connection.Name = CELProject.DefaultConnectionName
            windowCollection.Query.Where(windowCollection.Query.ProjectID = CELProject.Project.Number)
            windowCollection.Query.Where(windowCollection.Query.MainScreen = True)
            windowCollection.Query.Where(windowCollection.Query.EngineModel = engineModel)
            windowCollection.Query.Where(windowCollection.Query.MeasurementUnitId = CELProject.MeasurementUnitInUse)
            windowCollection.Query.OrderBy(windowCollection.Query.HMIWindowName.Ascending)
            windowCollection.Query.Load()
            '"SELECT * FROM UUT_HMI_Window WHERE Model = '" + engine_model + "' " + "AND MainScreen = True ORDER BY HMI_Window_Name"

            For Each hmiWindow As UUTHMIWindow In windowCollection

                If hmiWindow.Master = True Then
                    frmHmiMaster = CType(CELProject.GetObjectCallBack(hmiWindow.HMIWindowName), System.Windows.Forms.Control)
                    Debug.Assert(Not IsNothing(frmHmiMaster))
                End If

                If hmiWindow.Slave = True Then
                    frmHmiSlave = CType(CELProject.GetObjectCallBack(hmiWindow.HMIWindowName), System.Windows.Forms.Control)
                    Debug.Assert(Not IsNothing(frmHmiSlave))
                End If

            Next
        End Sub

        Sub InitializeGridLists(ByVal engine_model As String)

            If engine_model = "SYSTEM CHECK" Or engine_model = "DAS CALIBRATION" Then
                Exit Sub
            End If

            'ARINC data
            arincReceiveADataGrid = CType(CELProject.SearchControl(frmHmiMaster, "ArincChARxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            If IsNothing(arincReceiveADataGrid) And Not IsNothing(frmHmiSlave) Then
                arincReceiveADataGrid = CType(CELProject.SearchControl(frmHmiSlave, "ArincChARxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            End If

            arincReceiveBDataGrid = CType(CELProject.SearchControl(frmHmiMaster, "ArincChBRxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            If IsNothing(arincReceiveBDataGrid) And Not IsNothing(frmHmiSlave) Then
                arincReceiveBDataGrid = CType(CELProject.SearchControl(frmHmiSlave, "ArincChBRxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            End If

            arincTransmitADataGrid = CType(CELProject.SearchControl(frmHmiMaster, "ArincChATxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            If IsNothing(arincTransmitADataGrid) And Not IsNothing(frmHmiSlave) Then
                arincTransmitADataGrid = CType(CELProject.SearchControl(frmHmiSlave, "ArincChATxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            End If

            arincTransmitBDataGrid = CType(CELProject.SearchControl(frmHmiMaster, "ArincChBTxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            If IsNothing(arincTransmitBDataGrid) And Not IsNothing(frmHmiSlave) Then
                arincTransmitBDataGrid = CType(CELProject.SearchControl(frmHmiSlave, "ArincChBTxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            End If

            'ARINC faults and status
            arincAFaultView = CType(CELProject.SearchControl(frmHmiMaster, "FaultChADataDisplay"), CEL.Controls.FaultView)
            If IsNothing(arincAFaultView) And Not IsNothing(frmHmiSlave) Then
                arincAFaultView = CType(CELProject.SearchControl(frmHmiSlave, "FaultChADataDisplay"), CEL.Controls.FaultView)
            End If

            arincBFaultView = CType(CELProject.SearchControl(frmHmiMaster, "FaultChBDataDisplay"), CEL.Controls.FaultView)
            If IsNothing(arincBFaultView) And Not IsNothing(frmHmiSlave) Then
                arincBFaultView = CType(CELProject.SearchControl(frmHmiSlave, "FaultChBDataDisplay"), CEL.Controls.FaultView)
            End If

            arincAStatusView = CType(CELProject.SearchControl(frmHmiMaster, "StatusChADataDisplay"), CEL.Controls.FaultView)
            If IsNothing(arincAStatusView) And Not IsNothing(frmHmiSlave) Then
                arincAStatusView = CType(CELProject.SearchControl(frmHmiSlave, "StatusChADataDisplay"), CEL.Controls.FaultView)
            End If

            arincBStatusView = CType(CELProject.SearchControl(frmHmiMaster, "StatusChBDataDisplay"), CEL.Controls.FaultView)
            If IsNothing(arincBStatusView) And Not IsNothing(frmHmiSlave) Then
                arincBStatusView = CType(CELProject.SearchControl(frmHmiSlave, "StatusChBDataDisplay"), CEL.Controls.FaultView)
            End If

            InitializeUartGridLists(engine_model)

        End Sub

        Function SetupArincLabels(ByVal collectionName As String, ByRef channelCollection As ChannelCollection) As Boolean

            SetupArincLabels = False

            Dim ArincCounter(1, 1) As Integer
            Dim CurArincKTChannel As INumericChannel
            Dim CurArincChannelName As String = Nothing
            Dim CurArincRXTX As eArincTxRx
            Dim CurArincChannel As eArincChannel
            Dim CurArincFormat As Byte
            Dim newChannel As CEL.KineticaRT.Servers.Logger.NumericThresholdLink

            Try

                If Not IsNothing(channelCollection) Then
                    'Initialize Variables
                    ArincCounter(0, 0) = 1
                    ArincCounter(0, 1) = 1
                    ArincCounter(1, 0) = 1
                    ArincCounter(1, 1) = 1

                    'Query Database
                    'esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                    Dim labelCollection As ARINC429LabelTemplateCollection = New ARINC429LabelTemplateCollection()
                    labelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                    labelCollection.Query.es.QuerySource = "DACN1_ARINC429_LABEL" 'TODO.    Remap sur la table via app.config?
                    labelCollection.Query.Where(labelCollection.Query.ExportToCsv = True)
                    labelCollection.Query.Where(labelCollection.Query.EngineModel = collectionName)
                    labelCollection.Query.OrderBy(labelCollection.Query.EngineModel.Ascending)
                    labelCollection.Query.OrderBy(labelCollection.Query.TxRx.Ascending)
                    labelCollection.Query.OrderBy(labelCollection.Query.Description.Ascending)
                    labelCollection.Query.OrderBy(labelCollection.Query.ChannelNumber.Ascending)
                    labelCollection.Query.Load()
                    'Get the Arinc Labels associated with the selected Engine Model
                    ' "SELECT * FROM DACN1_ARINC429_LABEL WHERE Model = '" + collectionName + "' " +  ' "ORDER BY Model, [TX/RX], Description, Channel_Number"

                    For Each labelInfo As ARINC429LabelTemplate In labelCollection

                        CurArincRXTX = labelInfo.ArincRxTx
                        CurArincChannel = labelInfo.ArincChannelAB
                        CurArincFormat = System.Convert.ToByte(labelInfo.Format)

                        Select Case CurArincRXTX
                            Case eArincTxRx.Rx
                                CurArincChannelName = "User_Int_A429IN"
                            Case eArincTxRx.Tx
                                CurArincChannelName = "User_Int_A429OUT"
                            Case Else
                                Debug.Assert(False)
                        End Select

                        Select Case CurArincChannel
                            Case eArincChannel.CHA
                                CurArincChannelName += "_CHA_"
                            Case eArincChannel.CHB
                                CurArincChannelName += "_CHB_"
                            Case Else
                                Debug.Assert(False)
                        End Select
                        CurArincChannelName += ArincCounter(CurArincRXTX, CurArincChannel).ToString()
                        ArincCounter(CurArincRXTX, CurArincChannel) = ArincCounter(CurArincRXTX, CurArincChannel) + 1

                        CurArincKTChannel = CELProject.GetChannelCallBack(CurArincChannelName)

                        'The data from the Arinc_Channels.ksc is generic and all OPC tag should be connected to ...engunit
                        'with the exception of the Discrete that should be connected to ...data OPC variable instead.

                        Dim CurArincChannelOPC As Item
                        CurArincChannelOPC = CurArincKTChannel

                        CurArincChannelOPC.ItemID = CurArincChannelOPC.ItemID.Remove(CurArincChannelOPC.ItemID.LastIndexOf("."), CurArincChannelOPC.ItemID.Length - CurArincChannelOPC.ItemID.LastIndexOf("."))

                        If labelInfo.ArincRxTx = eArincTxRx.Tx And labelInfo.Format = UARTDataTypeDiscrete Then

                            'Force a code path to make sure the .data is the suffix if we have a discrete
                            CurArincChannelOPC.ItemID = CurArincChannelOPC.ItemID & ".data"

                        Else
                            'Force a code path to make sure the .engunit is the suffix if we don't have a discrete
                            CurArincChannelOPC.ItemID = CurArincChannelOPC.ItemID & ".engunit"

                        End If


                        If Not (CurArincKTChannel Is Nothing) Then
                            If labelInfo.Units & "" <> "" Then
                                CurArincKTChannel.Units = " " + labelInfo.Units
                            Else
                                CurArincKTChannel.Units = ""
                            End If
                            CurArincKTChannel.Description = labelInfo.Description

                            If (CurArincFormat = ArincLabelFormatDiscrete) Then
                                CurArincKTChannel.Format = "X"
                                CurArincKTChannel.Units = " Hex"

                                If (CurArincRXTX = eArincTxRx.Rx) Then
                                    If (CurArincKTChannel.Description.IndexOf("STAT") > 0) Then
                                        SetupArincDiscrete(CELProject.EngineModel, _
                                                           labelInfo.Label, _
                                                            CByte(labelInfo.Sdi), _
                                                           CurArincChannel, _
                                                           CurArincKTChannel)

                                    Else
                                        SetupArincFault(collectionName, _
                                                        channelCollection, _
                                                        labelInfo.Label, _
                                                         CByte(labelInfo.Sdi), _
                                                        CurArincChannel, _
                                                        CurArincKTChannel, _
                                                        labelInfo.Name, _
                                                        CurArincRXTX)

                                    End If
                                Else
                                    SetupArincFault(collectionName, _
                                                    channelCollection, _
                                                    labelInfo.Label, _
                                                     CByte(labelInfo.Sdi), _
                                                    CurArincChannel, _
                                                    CurArincKTChannel, _
                                                    labelInfo.Name, _
                                                    CurArincRXTX)
                                End If

                            ElseIf (CurArincFormat = ArincLabelFormatPW100LRUCode And labelInfo.Label = "0240") Then
                                If (CurArincRXTX = eArincTxRx.Rx) Then
                                    SetupArincPW100LRUCodes(CELProject.EngineModel, _
                                      CurArincKTChannel)
                                End If
                            End If

                            CurArincKTChannel.Format = labelInfo.DisplayFormat

                            'Create Numeric Link Channels
                            newChannel = New CEL.KineticaRT.Servers.Logger.NumericThresholdLink

                            newChannel.Description = labelInfo.Description
                            newChannel.ToolTip = CurArincKTChannel.ToolTip

                            If (CurArincRXTX = eArincTxRx.Rx) Then
                                newChannel.Destination = ""
                            Else
                                newChannel.Destination = CurArincChannelName
                            End If
                            newChannel.Source = CurArincChannelName

                            'Archive
                            newChannel.Enabled = DicArchiveChannelList.ContainsKey(labelInfo.Name)
                            If newChannel.Enabled Then
                                newChannel.Index = DicArchiveChannelList(labelInfo.Name) 'Index
                            Else
                                newChannel.Index = 0
                            End If

                            'newChannel.Enabled = labelInfo.LoggerEnabled

                            'If labelInfo.LoggerEnabled = False OrElse IsNothing(labelInfo.LoggerIndex) Then
                            '    newChannel.Index = 0
                            'Else
                            '    newChannel.Index = labelInfo.LoggerIndex
                            'End If
                            newChannel.Logger = "ARCHIVE"
                            newChannel.EventLogger = "EVENT_ARCHIVE"
                            newChannel.DecimalDigits = CurArincKTChannel.DecimalDigits
                            newChannel.Format = CurArincKTChannel.Format

                            newChannel.Units = CurArincKTChannel.Units
                            newChannel.Name = labelInfo.Name

                            channelCollection.Add(newChannel)

                            Select Case CurArincRXTX
                                Case eArincTxRx.Rx
                                    Select Case CurArincChannel
                                        Case eArincChannel.CHA
                                            arincReceiveAChannelList.Add(New CEL.ARINCChannel(CurArincKTChannel))
                                        Case eArincChannel.CHB
                                            arincReceiveBChannelList.Add(New CEL.ARINCChannel(CurArincKTChannel))
                                    End Select
                                Case eArincTxRx.Tx
                                    Select Case CurArincChannel
                                        Case eArincChannel.CHA
                                            arincTransmitAChannelList.Add(New CEL.ARINCChannel(CurArincKTChannel))
                                        Case eArincChannel.CHB
                                            arincTransmitBChannelList.Add(New CEL.ARINCChannel(CurArincKTChannel))
                                    End Select
                                Case Else
                                    Debug.Assert(False)
                            End Select
                        End If
                    Next

                    SetupArincLabels = True
                Else
                    Debug.Assert(False, "ARINC.SetupArincLabels: channelCollection is nothing")
                End If

            Catch exception As System.Exception

                clsTopMostMessageBox.Show("ARINC.SetupArincLabels: " & exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try

        End Function

        Public Function SetupArincDiscrete(ByVal engineModel As String, _
                                           ByVal arincLabel As String, _
                                           ByVal SDI As Byte, _
                                           ByVal arincChannel As eArincChannel, _
                                           ByVal KineticaRTArincDiscreteChannel As IChannel) As Boolean

            SetupArincDiscrete = False

            Dim curArincChannelName As String
            Dim CurArincKTChannel As INumericChannel
            Dim curPrefix As String = Nothing

            Try

                'Query Database
                esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                Dim inDiscreteCollection As ARINC429DiscreteTemplateCollection = New ARINC429DiscreteTemplateCollection()
                inDiscreteCollection.es.Connection.Name = CELProject.DefaultConnectionName
                inDiscreteCollection.Query.es.QuerySource = " DACN1_ARINC429IN_DISCRETE" 'TODO.    Remap sur la table via app.config?
                inDiscreteCollection.Query.Where(inDiscreteCollection.Query.ExportToCsv = True)
                inDiscreteCollection.Query.Where(inDiscreteCollection.Query.EngineModel = engineModel)
                inDiscreteCollection.Query.Where(inDiscreteCollection.Query.Label = arincLabel)
                inDiscreteCollection.Query.Where(inDiscreteCollection.Query.Sdi = SDI)
                inDiscreteCollection.Query.OrderBy(inDiscreteCollection.Query.ParameterNo.Ascending)
                inDiscreteCollection.Query.Load()
                'sqlString = "SELECT * FROM DACN1_ARINC429IN_DISCRETE WHERE Model = ? AND Label = ? ORDER BY Parameter_No"


                Dim valueChangedHandler As New DiscreteRXValueChangedHandler(KineticaRTArincDiscreteChannel)
                arincDiscreteRXHandlers.Add(valueChangedHandler)

                For Each discrete As ARINC429DiscreteTemplate In inDiscreteCollection


                    curArincChannelName = "A429Discrete"

                    Select Case arincChannel
                        Case eArincChannel.CHA
                            curArincChannelName += "_CHA_"
                            curPrefix = "AA"
                        Case eArincChannel.CHB
                            curArincChannelName += "_CHB_"
                            curPrefix = "AB"
                        Case Else
                            Debug.Assert(False)
                    End Select

                    curArincChannelName += discrete.ParameterNo.ToString

                    CurArincKTChannel = CELProject.GetChannelCallBack(curArincChannelName)
                    If Not (CurArincKTChannel Is Nothing) Then
                        CurArincKTChannel.Description = curPrefix + discrete.DiscreteCode.ToString

                        valueChangedHandler.AddBitFieldChannel(discrete.StartBit, _
                                                               discrete.StopBit, _
                                                               discrete.DiscreteCode, _
                                                               discrete.LSBValue, _
                                                               discrete.Description, _
                                                               CurArincKTChannel)

                    End If

                Next

                SetupArincDiscrete = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("ARINC.SetupArincDiscrete: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Function

        'Public Function SetupArincFault(ByVal collectionName As String, ByVal destinationCollectionName As String, ByVal arincLabel As String, ByVal arincChannel As Integer, ByVal KineticaRTArincDiscreteChannel As KineticaRT.Channels.Channel, ByVal arincName As String, ByVal arincRXTX As Byte) As Boolean
        Public Function SetupArincFault(ByVal collectionName As String, _
                                        ByRef channelCollection As ChannelCollection, _
                                        ByVal arincLabel As String, _
                                        ByVal arincSDI As Byte, _
                                        ByVal arincChannel As eArincChannel, _
                                        ByVal KineticaRTArincDiscreteChannel As IChannel, _
                                        ByVal arincName As String, _
                                        ByVal arincRXTX As eArincTxRx) As Boolean

            SetupArincFault = False

            Dim arincFaultChannelList As CEL.ACMCChannelCollection = Nothing
            Dim arincStatusChannelList As CEL.ACMCChannelCollection = Nothing
            Dim bitfield_Channel As IChannel

            Try

                If Not IsNothing(channelCollection) Then

                    'Query Database
                    esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                    Dim discreteCollection As ARINC429DiscreteTemplateCollection = New ARINC429DiscreteTemplateCollection()
                    discreteCollection.es.Connection.Name = CELProject.DefaultConnectionName

                    If (arincRXTX = eArincTxRx.Rx) Then
                        'sqlString = "SELECT * FROM DACN1_ARINC429IN_DISCRETE WHERE Model = ? AND Label = ? ORDER BY Parameter_No"
                        discreteCollection.Query.es.QuerySource = "DACN1_ARINC429IN_DISCRETE" 'TODO.    Remap sur la table via app.config?
                    Else
                        'sqlString = "SELECT * FROM DACN1_ARINC429OUT_DISCRETE WHERE Model = ? AND Label = ? ORDER BY Parameter_No"
                        discreteCollection.Query.es.QuerySource = "DACN1_ARINC429OUT_DISCRETE" 'TODO.    Remap sur la table via app.config?
                    End If
                    discreteCollection.Query.Where(discreteCollection.Query.ExportToCsv = True)
                    discreteCollection.Query.Where(discreteCollection.Query.EngineModel = collectionName)
                    discreteCollection.Query.Where(discreteCollection.Query.Label = arincLabel)
                    discreteCollection.Query.Where(discreteCollection.Query.Sdi = arincSDI)
                    discreteCollection.Query.OrderBy(discreteCollection.Query.ParameterNo.Ascending)
                    discreteCollection.Query.Load()


                    If (arincRXTX = eArincTxRx.Rx) Then
                        Dim valueRXChangedHandler As New DiscreteRXValueChangedHandler(KineticaRTArincDiscreteChannel)
                        arincRXDiscreteHandlers.Add(valueRXChangedHandler)

                        Select Case arincChannel
                            Case eArincChannel.CHA
                                arincFaultChannelList = arincFaultAChannelList
                                arincStatusChannelList = arincStatusAChannelList
                            Case eArincChannel.CHB
                                arincFaultChannelList = arincFaultBChannelList
                                arincStatusChannelList = arincStatusBChannelList
                            Case Else
                                Debug.Assert(False)
                        End Select

                        Dim cmcFaultChannel As New CEL.ACMCChannel(KineticaRTArincDiscreteChannel)
                        Dim cmcStatusChannel As New CEL.ACMCChannel(KineticaRTArincDiscreteChannel)
                        arincFaultChannelList.Add(cmcFaultChannel)
                        arincStatusChannelList.Add(cmcStatusChannel)

                        For Each discrete As ARINC429DiscreteTemplate In discreteCollection

                            Dim startBit, stopBit As Byte
                            Dim startBitID, stopBitID As Byte

                            startBit = CByte(discrete.StartBit)
                            stopBit = CByte(discrete.StopBit)
                            startBitID = CByte(startBit + 10)
                            stopBitID = CByte(stopBit + 10)



                            If startBit = stopBit Then
                                Select Case discrete.DiscreteCode
                                    Case eDiscreteCode.Fault
                                        cmcFaultChannel.AddBitField(startBit, _
                                           (stopBit - startBit + 1), _
                                           discrete.DiscreteCode, _
                                           discrete.Description)
                                    Case eDiscreteCode.Status
                                        cmcStatusChannel.AddBitField(startBit, _
                                           (stopBit - startBit + 1), _
                                           discrete.DiscreteCode, _
                                           discrete.Description)
                                    Case Else
                                        Debug.Assert(False)
                                End Select
                            End If


                            'Create Bitfield Channels
                            bitfield_Channel = New Channel
                            If startBit = stopBit Then
                                bitfield_Channel.Name = arincName + "_SDI" + arincSDI.ToString + "_" + startBitID.ToString
                            Else
                                bitfield_Channel.Name = arincName + "_SDI" + arincSDI.ToString + "_" + startBitID.ToString + "_" + stopBitID.ToString
                            End If

                            valueRXChangedHandler.AddBitFieldChannel(startBit, _
                                                                     stopBit, _
                                                                     discrete.DiscreteCode, _
                                                                     discrete.LSBValue, _
                                                                     discrete.Description, _
                                                                     bitfield_Channel)

                            channelCollection.Add(bitfield_Channel)

                        Next
                    Else
                        Dim valueTXChangedHandler As New DiscreteTXValueChangedHandler(KineticaRTArincDiscreteChannel)
                        arincTXDiscreteHandlers.Add(valueTXChangedHandler)

                        For Each discrete As ARINC429DiscreteTemplate In discreteCollection
                            Dim startBit, stopBit As Byte
                            Dim startBitID, stopBitID As Byte

                            startBit = CByte(discrete.StartBit)
                            stopBit = CByte(discrete.StopBit)
                            startBitID = CByte(startBit + 10)
                            stopBitID = CByte(stopBit + 10)

                            'Create Bitfield Channels
                            bitfield_Channel = New Channel
                            If startBit = stopBit Then
                                bitfield_Channel.Name = arincName + "_SDI" + arincSDI.ToString + "_" + startBitID.ToString
                            Else
                                bitfield_Channel.Name = arincName + "_SDI" + arincSDI.ToString + "_" + startBitID.ToString + "_" + stopBitID.ToString
                            End If

                            valueTXChangedHandler.AddBitFieldChannel(startBit, _
                                                                     stopBit, _
                                                                     discrete.DiscreteCode, _
                                                                     discrete.LSBValue, _
                                                                     discrete.Description, _
                                                                     bitfield_Channel)
                            channelCollection.Add(bitfield_Channel)

                        Next
                    End If

                    SetupArincFault = True

                Else
                    Debug.Assert(False)
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("ARINC.SetupArincFault: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Function

        Public Function SetupArincPW100LRUCodes(ByVal UUT_Model As String, ByVal KineticaRTArincLRUCodeChannel As IChannel) As Boolean

            SetupArincPW100LRUCodes = False

            Try

                'Query Database
                esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                Dim lruCodeCollection As ARINC429LRUCodesTemplateCollection = New ARINC429LRUCodesTemplateCollection()
                lruCodeCollection.es.Connection.Name = CELProject.DefaultConnectionName
                lruCodeCollection.Query.es.QuerySource = "DACN1_ARINC429_PW100LRUCODES" 'TODO: App.config ??
                lruCodeCollection.Query.Where(lruCodeCollection.Query.EngineModel = UUT_Model)
                lruCodeCollection.Query.Where(lruCodeCollection.Query.ExportToCsv = True)
                lruCodeCollection.Query.OrderBy(lruCodeCollection.Query.LRUCode.Ascending)
                lruCodeCollection.Query.Load()
                'sqlString = "SELECT * FROM DACN1_ARINC429_PW100LRUCODES WHERE Model = ? ORDER BY LRUCode"

                For Each lruCode As ARINC429LRUCodesTemplate In lruCodeCollection

                    If Not IsNothing(lruCode.Description) Then
                        Me.PW100LRUCodeDescriptions(lruCode.LRUCode) = _
                         New Global.KineticaRT.Values.Text(lruCode.Description)
                    Else
                        Me.PW100LRUCodeDescriptions(lruCode.LRUCode) = _
                         Values.EmptyString
                    End If

                Next

                Me.arincKineticaRTPW100LRUCodeChannel = KineticaRTArincLRUCodeChannel

                Me.arincKineticaRTPW100LRUDescriptionChannel = CELProject.GetChannelCallBack("LRUFaultDescription")


                If Not (Me.arincKineticaRTPW100LRUDescriptionChannel Is Nothing) Then
                    Try
                        Dim lruCode As Integer

                        lruCode = CInt(Me.arincKineticaRTPW100LRUCodeChannel.Value.ToDouble())

                        Me.arincKineticaRTPW100LRUDescriptionChannel.Value = Me.PW100LRUCodeDescriptions(lruCode)
                    Catch
                        Me.arincKineticaRTPW100LRUDescriptionChannel.Value = Values.EmptyString
                    End Try
                End If

                AddHandler Me.arincKineticaRTPW100LRUCodeChannel.ValueChanged, AddressOf Me.OnPW100LRUCodeValueChanged

                SetupArincPW100LRUCodes = True

            Catch exception As System.Exception

                clsTopMostMessageBox.Show("ARINC.SetupArincPW100LRUCodes: " & exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try
        End Function

        Public Function SetupUartParameters(ByVal collectionName As String, ByRef channelCollection As ChannelCollection) As Boolean

            SetupUartParameters = False

            Dim curUartDataType As Byte
            Dim curUartKTChannel As INumericChannel

            Dim curUartChannelName As String
            Dim newChannel As CEL.KineticaRT.Servers.Logger.NumericThresholdLink

            Try

                If Not IsNothing(channelCollection) Then

                    'Query Database
                    esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                    Dim omnibusboxUartCollection As OmniBusBoxUartTemplateCollection = New OmniBusBoxUartTemplateCollection()
                    omnibusboxUartCollection.es.Connection.Name = CELProject.DefaultConnectionName
                    omnibusboxUartCollection.Query.es.QuerySource = "DACN1_OmniBusBox_IOMGR_Uart" 'TODO: App.config ??

                    omnibusboxUartCollection.Query.Where(omnibusboxUartCollection.Query.ExportToCsv = True)
                    omnibusboxUartCollection.Query.Where(omnibusboxUartCollection.Query.EngineModel = collectionName)
                    omnibusboxUartCollection.Query.Where(omnibusboxUartCollection.Query.PwcPn = CELProject.PWCEECPartNumber)
                    omnibusboxUartCollection.Query.OrderBy(omnibusboxUartCollection.Query.EngineModel.Ascending)
                    omnibusboxUartCollection.Query.OrderBy(omnibusboxUartCollection.Query.ArincChannel.Ascending)
                    omnibusboxUartCollection.Query.OrderBy(omnibusboxUartCollection.Query.ParameterNo.Ascending)
                    omnibusboxUartCollection.Query.Load()

                    For Each uartInfo As OmniBusBoxUartTemplate In omnibusboxUartCollection
                        If ((uartInfo.Parameter.StartsWith("EAPOS") Or uartInfo.Parameter.StartsWith("EBPOS"))) Then

                        Else
                            'Channel Name
                            curUartChannelName = "User_Int_Uart"

                            Select Case uartInfo.ArincChannel 'Inserer la gestion du EEC/Cockpit 
                                Case UARTChannelA
                                    curUartChannelName += "_CHA_"
                                Case UARTChannelB
                                    curUartChannelName += "_CHB_"
                                Case Else
                                    Debug.Assert(False)
                            End Select
                            curUartChannelName += uartInfo.ParameterNo.ToString




                            curUartDataType = System.Convert.ToByte(uartInfo.DataType)

                            curUartKTChannel = TryCast(CELProject.GetChannelCallBack(curUartChannelName), INumericChannel)


                            Dim CurUartChannelOPC As Item
                            CurUartChannelOPC = curUartKTChannel

                            CurUartChannelOPC.ItemID = CurUartChannelOPC.ItemID.Remove(CurUartChannelOPC.ItemID.LastIndexOf("."), CurUartChannelOPC.ItemID.Length - CurUartChannelOPC.ItemID.LastIndexOf("."))


                            If uartInfo.DataType = UARTDataTypeReal Or uartInfo.DataType = UARTDataTypeInteger Or uartInfo.DataType = UARTDataTypeDiscrete Then
                                'Force a code path to make sure the .engunit is the suffix if we don't have a discrete
                                CurUartChannelOPC.ItemID = CurUartChannelOPC.ItemID & ".engunit"
                            Else
                                'Force a code path to make sure the .data is the suffix if we have a discrete
                                CurUartChannelOPC.ItemID = CurUartChannelOPC.ItemID & ".data"


                            End If


                            If Not (curUartKTChannel Is Nothing) Then

                                If uartInfo.Units & "" <> "" Then
                                    curUartKTChannel.Units = " " + uartInfo.Units
                                ElseIf (curUartDataType = UARTDataTypeDiscrete) Then
                                    curUartKTChannel.Units = "Oct"
                                Else
                                    curUartKTChannel.Units = ""
                                End If

                                curUartKTChannel.Description = uartInfo.Parameter

                                If uartInfo.Format & "" <> "" Then
                                    curUartKTChannel.Format = uartInfo.Format
                                ElseIf (curUartDataType = UARTDataTypeDiscrete) Then
                                    curUartKTChannel.Format = "O"
                                ElseIf uartInfo.Slope < 1 Then
                                    Dim Digits As Single = CSng(Math.Ceiling(Math.Abs(Math.Log10(uartInfo.Slope.GetValueOrDefault))))

                                    curUartKTChannel.Format = "F" + Digits.ToString("F0")
                                Else
                                    curUartKTChannel.Format = "G"
                                End If

                                'Generation dynamique d'un channel discrete en fonction du startbit/stopbit dans la table DACN1_OmniBusBox_IOMGR_UartIN_Discrete
                                If curUartDataType = UARTDataTypeDiscrete Then
                                    SetupUartDiscrete(collectionName, channelCollection, uartInfo.Source.ToString, uartInfo.ArincChannel.GetValueOrDefault, uartInfo.ParameterNo.ToString, uartInfo.Name, curUartKTChannel)
                                End If

                                'Create Numeric Link Channels
                                newChannel = New CEL.KineticaRT.Servers.Logger.NumericThresholdLink

                                newChannel.Description = uartInfo.Parameter
                                newChannel.ToolTip = curUartKTChannel.ToolTip
                                newChannel.Destination = curUartChannelName
                                newChannel.Source = curUartChannelName

                                'Archive
                                newChannel.Enabled = DicArchiveChannelList.ContainsKey(uartInfo.Name)
                                If newChannel.Enabled Then
                                    newChannel.Index = DicArchiveChannelList(uartInfo.Name) 'Index
                                Else
                                    newChannel.Index = 0
                                End If

                                newChannel.Logger = "ARCHIVE"
                                newChannel.EventLogger = "EVENT_ARCHIVE"
                                newChannel.DecimalDigits = curUartKTChannel.DecimalDigits
                                newChannel.Format = curUartKTChannel.Format
                                newChannel.Units = curUartKTChannel.Units
                                newChannel.Name = uartInfo.Name

                                channelCollection.Add(newChannel)

                                Select Case uartInfo.ArincChannel
                                    Case UARTChannelA
                                        If uartInfo.Source = UARTSourceEec Then
                                            devUartRxChannelListChA.Add(New CEL.ARINCChannel(curUartKTChannel))
                                        ElseIf uartInfo.Source = UARTSourceCockpit Then
                                            cockpitUartRxChannelListChA.Add(New CEL.ARINCChannel(curUartKTChannel))
                                        End If
                                    Case UARTChannelB
                                        If uartInfo.Source = UARTSourceEec Then
                                            devUartRxChannelListChB.Add(New CEL.ARINCChannel(curUartKTChannel))
                                        ElseIf uartInfo.Source = UARTSourceCockpit Then
                                            cockpitUartRxChannelListChB.Add(New CEL.ARINCChannel(curUartKTChannel))
                                        End If
                                    Case Else
                                        Debug.Assert(False)
                                End Select
                            End If
                        End If
                    Next

                Else
                    Debug.Assert(False)
                End If

                SetupUartParameters = True

            Catch ex As Exception

                clsTopMostMessageBox.Show("ARINC.SetupUARTParameters: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try

        End Function

        Public Function SetupUartDiscrete(ByVal collectionName As String, _
                                          ByRef channelCollection As ChannelCollection, _
                                          ByVal source As String, _
                                          ByVal channel As Byte, _
                                          ByVal parameterNo As String, _
                                          ByVal label As String, _
                                          ByVal KineticaRTUartDiscreteChannel As IChannel) As Boolean

            SetupUartDiscrete = False

            Dim uartFaultChannelList As CEL.ACMCChannelCollection = Nothing
            Dim uartStatusChannelList As CEL.ACMCChannelCollection = Nothing

            Try

                If Not IsNothing(channelCollection) Then

                    'Query Database
                    Dim discreteCollection As New OmniBusBoxUartDiscreteTemplateCollection()
                    discreteCollection.es.Connection.Name = CELProject.DefaultConnectionName
                    discreteCollection.Query.es.QuerySource = "DACN1_OmniBusBox_IOMGR_UartIN_DISCRETE" 'TODO.    Remap sur la table via app.config?

                    discreteCollection.Query.Where(discreteCollection.Query.ExportToCsv = True)
                    discreteCollection.Query.Where(discreteCollection.Query.EngineModel = collectionName)
                    discreteCollection.Query.Where(discreteCollection.Query.ParameterNo = parameterNo)
                    discreteCollection.Query.Where(discreteCollection.Query.UartChannel = channel)
                    discreteCollection.Query.Where(discreteCollection.Query.Source = source)
                    discreteCollection.Query.Where(discreteCollection.Query.PwcPn = CELProject.PWCEECPartNumber)
                    discreteCollection.Query.OrderBy(discreteCollection.Query.ParameterNo.Ascending)
                    discreteCollection.Query.OrderBy(discreteCollection.Query.StartBit.Ascending)

                    discreteCollection.Query.Load()

                    'Handler for interrupt when value changes
                    Dim valueRXChangedHandler As New DiscreteRXValueChangedHandler(KineticaRTUartDiscreteChannel)
                    devUartRxDiscreteHandlers.Add(valueRXChangedHandler)

                    '(sh) Set proper channel to do operations on
                    Select Case channel
                        Case UARTChannelA
                            If source = UARTSourceEec Then
                                uartFaultChannelList = devUartFaultChannelListChA
                                uartStatusChannelList = devUartStatusChannelListChA
                            ElseIf source = UARTSourceCockpit Then
                                uartFaultChannelList = cockpitUartFaultChannelListChA
                                uartStatusChannelList = cockpitUartStatusChannelListChA
                            End If
                        Case UARTChannelB
                            If source = UARTSourceEec Then
                                uartFaultChannelList = devUartFaultChannelListChB
                                uartStatusChannelList = devUartStatusChannelListChB
                            ElseIf source = UARTSourceCockpit Then
                                uartFaultChannelList = cockpitUartFaultChannelListChB
                                uartStatusChannelList = cockpitUartStatusChannelListChB
                            End If
                        Case Else
                            Debug.Assert(False)
                    End Select

                    Dim cmcFaultChannel As CEL.ACMCChannel = Nothing
                    Dim cmcStatusChannel As CEL.ACMCChannel = Nothing

                    For Each discrete As OmniBusBoxUartDiscreteTemplate In discreteCollection

                        Dim startBit, stopBit As Byte
                        Dim startBitID, stopBitID As Byte

                        startBit = CByte(discrete.StartBit)
                        stopBit = CByte(discrete.StopBit)
                        startBitID = startBit
                        stopBitID = stopBit

                        Select Case discrete.DiscreteCode
                            Case OmniBusBoxUartDiscreteTemplate.eDiscreteCode.Fault
                                If cmcFaultChannel Is Nothing Then
                                    cmcFaultChannel = New CEL.ACMCChannel(KineticaRTUartDiscreteChannel)
                                    uartFaultChannelList.Add(cmcFaultChannel)
                                End If
                                cmcFaultChannel.AddBitField(
                                    startBit, _
                                   (stopBit - startBit + 1), _
                                   discrete.DiscreteCode, _
                                   discrete.Description)
                            Case OmniBusBoxUartDiscreteTemplate.eDiscreteCode.Status
                                If cmcStatusChannel Is Nothing Then
                                    cmcStatusChannel = New CEL.ACMCChannel(KineticaRTUartDiscreteChannel)
                                    uartStatusChannelList.Add(cmcStatusChannel)
                                End If
                                cmcStatusChannel.AddBitField(
                                    startBit, _
                                   (stopBit - startBit + 1), _
                                   discrete.DiscreteCode, _
                                   discrete.Description)
                            Case Else
                                Debug.Assert(False)
                        End Select

                        'Create bitfield Channel
                        Dim bitfield_Channel As New Channel
                        If startBit = stopBit Then
                            bitfield_Channel.Name = label + "_" + startBitID.ToString
                        Else
                            bitfield_Channel.Name = label + "_" + startBitID.ToString + "_" + stopBitID.ToString
                        End If

                        valueRXChangedHandler.AddBitFieldChannel(startBit, _
                                                           stopBit, _
                                                           1, _
                                                           1, _
                                                           discrete.Description, _
                                                           bitfield_Channel)
                        channelCollection.Add(bitfield_Channel)

                    Next

                    SetupUartDiscrete = True

                Else
                    Debug.Assert(False)
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("ARINC.SetupUartDiscrete: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Function

        Sub CleanUpArincLabels()
            Dim isDone As Boolean
            Dim i, j, k As Integer
            Dim channelsInfo As ChannelsInfo
            Dim curArincChannelName As String = Nothing
            Dim curArincKTChannel As INumericChannel
            Dim discreteRXHandler As DiscreteRXValueChangedHandler
            Dim discreteTXHandler As DiscreteTXValueChangedHandler

            For Each discreteRXHandler In arincRXDiscreteHandlers
                discreteRXHandler.Unregister()
            Next

            For Each discreteTXHandler In arincTXDiscreteHandlers
                discreteTXHandler.Unregister()
            Next

            If Not (Me.arincKineticaRTPW100LRUCodeChannel Is Nothing) Then
                RemoveHandler Me.arincKineticaRTPW100LRUCodeChannel.ValueChanged, AddressOf Me.OnPW100LRUCodeValueChanged
                Me.arincKineticaRTPW100LRUCodeChannel = Nothing
            End If

            Me.arincRXDiscreteHandlers.Clear()
            Me.arincTXDiscreteHandlers.Clear()
            Me.arincReceiveAChannelList.Clear()
            Me.arincReceiveBChannelList.Clear()
            Me.arincTransmitAChannelList.Clear()
            Me.arincTransmitBChannelList.Clear()
            Me.arincFaultAChannelList.Clear()
            Me.arincFaultBChannelList.Clear()
            Me.arincStatusAChannelList.Clear()
            Me.arincStatusBChannelList.Clear()

            For i = 0 To 2
                For j = 0 To 1
                    isDone = False
                    k = 0

                    While (Not (isDone))
                        k += 1

                        Select Case i
                            Case eArincTxRx.Rx
                                curArincChannelName = "User_Int_A429IN"
                            Case eArincTxRx.Tx
                                curArincChannelName = "User_Int_A429OUT"
                            Case 2
                                curArincChannelName = "A429Discrete"
                            Case Else
                                Debug.Assert(False)
                        End Select

                        Select Case j
                            Case eArincChannel.CHA
                                curArincChannelName += "_CHA_"
                            Case eArincChannel.CHB
                                curArincChannelName += "_CHB_"
                            Case Else
                                Debug.Assert(False)
                        End Select

                        curArincChannelName += k.ToString()

                        curArincKTChannel = CELProject.GetChannelCallBack(curArincChannelName)

                        If Not (curArincKTChannel Is Nothing) Then
                            curArincKTChannel.Units = ""
                            curArincKTChannel.Format = "G"
                            curArincKTChannel.Description = ""
                        Else
                            isDone = True
                        End If
                    End While
                Next j
            Next i


            If Not (CELProject.HMIProject Is Nothing) Then
                channelsInfo = CELProject.HMIProject.GetChannelsInfo(CELProject.EngineModel + "_Arinc_Uart") 'TODO: 
                If Not (channelsInfo Is Nothing) Then
                    CELProject.HMIProject.RemoveChannels(channelsInfo)
                End If
            End If

            For i = 0 To Me.PW100LRUCodeDescriptions.GetUpperBound(0)
                Me.PW100LRUCodeDescriptions(i) = Values.EmptyString
            Next

        End Sub

        Sub CleanUpUARTParameters()
            Dim isDone As Boolean
            Dim i, j As Integer
            Dim curUARTChannelName As String
            Dim curUARTKTChannel As INumericChannel

            Dim discreteRXHandler As DiscreteRXValueChangedHandler

            For Each discreteRXHandler In devUartRxDiscreteHandlers
                discreteRXHandler.Unregister()
            Next

            Me.devUartRxDiscreteHandlers.Clear()
            Me.devUartRxChannelListChA.Clear()
            Me.devUartRxChannelListChB.Clear()
            Me.devUartFaultChannelListChA.Clear()
            Me.devUartFaultChannelListChB.Clear()
            Me.devUartStatusChannelListChA.Clear()
            Me.devUartStatusChannelListChB.Clear()

            Me.cockpitUartRxDiscreteHandlers.Clear()
            Me.cockpitUartRxChannelListChA.Clear()
            Me.cockpitUartRxChannelListChB.Clear()
            Me.cockpitUartFaultChannelListChA.Clear()
            Me.cockpitUartFaultChannelListChB.Clear()
            Me.cockpitUartStatusChannelListChA.Clear()
            Me.cockpitUartStatusChannelListChB.Clear()

            For i = 0 To 1
                isDone = False
                j = 0

                While (Not (isDone))
                    curUARTChannelName = "User_Int_Uart"
                    j += 1

                    Select Case i
                        Case eArincChannel.CHA
                            curUARTChannelName += "_CHA_"
                        Case eArincChannel.CHB
                            curUARTChannelName += "_CHB_"
                        Case Else
                            Debug.Assert(False)
                    End Select


                    curUARTChannelName += j.ToString()

                    curUARTKTChannel = CELProject.GetChannelCallBack(curUARTChannelName)
                    If Not (curUARTKTChannel Is Nothing) Then
                        curUARTKTChannel.Units = ""
                        curUARTKTChannel.Format = "G"
                        curUARTKTChannel.Description = ""
                    Else
                        'TODO: Ici il manque une gestion d'erreur dans le cas ou la liste Statique ARINC_Channels.ksc ne contient pas autant de channel que nécessaire
                        'comme ca s'est produit pour le GTCP131-9B ou il avait besoin de 27 channels et qu'on en comptait que 25 dans la liste statique
                        isDone = True
                    End If
                End While
            Next i
        End Sub

        '********************************************************************
        '* Handlers *********************************************************
        '********************************************************************
        Private Sub OnPW100LRUCodeValueChanged(ByVal sender As IChannel, ByVal newVal As Value)
            Dim LRUCode As Integer

            Try
                LRUCode = CInt(newVal.ToDouble())

                Me.arincKineticaRTPW100LRUDescriptionChannel.Value = Me.PW100LRUCodeDescriptions(LRUCode)
            Catch
                Me.arincKineticaRTPW100LRUDescriptionChannel.Value = Values.EmptyString
            End Try
        End Sub



        Public Function Initialize(ByVal collectionName As String, ByRef channelCollection As ChannelCollection, ByRef dicArchiveLayout As Collections.Generic.Dictionary(Of String, Integer), ByVal arincConfiguration As CEL.HMI.EngineInformationItem.eArincConfiguration) As Boolean

            Initialize = False

            Try

                DicArchiveChannelList = dicArchiveLayout
                'ARINC stuff
                SetupArincLabels(collectionName, channelCollection)

                'UART stuff
                SetupUartParameters(collectionName, channelCollection)

                FindAndInitializeWindow(CELProject.EngineModel)
                'Initialise Gridlists
                Me.InitializeGridLists(CELProject.EngineModel) ' TODO ???? ' This fonction find all grid lists


                'Bind the list to the appropriate control
                Me.DataBindToDataGrid()

                Me.UpdateLayout(arincConfiguration)

                Initialize = True

            Catch ex As Exception

                clsTopMostMessageBox.Show("ARINC.Initialize: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try

        End Function

        Public Sub Dispose()

            Try

                CleanUpArincLabels()
                CleanUpUARTParameters()
                arincReceiveADataGrid = Nothing
                arincTransmitADataGrid = Nothing
                devUartDataGridChA = Nothing
                devUartDataGridChB = Nothing
                cockpitUartDataGridChA = Nothing
                cockpitUartDataGridChB = Nothing
            Catch ex As Exception
                clsTopMostMessageBox.Show("ARINC_Sequence : Finalize: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub


        '********
        '* Misc *
        '********

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub




        '********************************************
        '* Class DiscreteRXValueChangedHandler *
        '********************************************

        Public Class DiscreteRXValueChangedHandler
            Private Class BitFieldChannelCollection
                Inherits System.Collections.CollectionBase

                Public Function AddBitFieldChannel(ByVal startBit As Byte, _
                                                   ByVal length As Byte, _
                                                   ByVal code As eDiscreteCode, _
                                                   ByVal scalingFactor As Double, _
                                                   ByVal description As String, _
                                                   ByVal destinationChannel As IChannel) As BitFieldChannel

                    Dim newBitFieldChannel As New BitFieldChannel(startBit, length, code, scalingFactor, description, destinationChannel)

                    MyBase.List.Add(newBitFieldChannel)
                    Return newBitFieldChannel
                End Function

                Public Property Item(ByVal index As Integer) As BitFieldChannel
                    Get
                        Return CType(MyBase.List.Item(index), BitFieldChannel)
                    End Get
                    Set(ByVal value As BitFieldChannel)
                        If (Not value Is MyBase.List.Item(index)) Then
                            MyBase.List.Item(index) = value
                        End If
                    End Set
                End Property
            End Class

            Private Class BitFieldChannel
                Public Sub New(ByVal startBit As Byte, _
                               ByVal length As Byte, _
                               ByVal code As eDiscreteCode, _
                               ByVal scalingFactor As Double, _
                               ByVal description As String, _
                               ByVal destinationChannel As IChannel)

                    Dim count As Byte
                    Dim baseMask As Long = (1 << (startBit - 1))
                    Me.itsShift = (startBit - 1)
                    Me.itsCodeType = code
                    Me.itsScalingFactor = scalingFactor
                    Me.itsDescription = description
                    Me.itsMask = baseMask
                    Me.itsLength = length

                    If Me.itsCodeType = eDiscreteCode.BNR Then 'Binary Number Representation of Two's Complement Number
                        Me.itsBitSignMask = (1 << (length - 1))
                    End If

                    For count = 1 To (length - 1)
                        Me.itsMask = ((Me.itsMask << 1) Or baseMask)
                    Next

                    Me.itsChannel = destinationChannel

                    If Not (Me.itsChannel Is Nothing) Then
                        Me.itsChannel.Value = New Global.KineticaRT.Values.Double(Me.itsValue)
                    End If

                End Sub

                'Public Property Value As Long
                Public Property Value As Double 'Modifie la valeur de Long A double, puisque certains parametre on scaling factor de 0.125 et incompatible avec type Long
                    Get
                        Return Me.itsValue
                    End Get
                    Set(newVal As Double)
                        If (newVal <> Me.itsValue) Then
                            Me.itsValue = CLng(newVal)

                            If Not (Me.itsChannel Is Nothing) Then
                                Me.itsChannel.Value = New Global.KineticaRT.Values.Double(newVal)
                            End If
                        End If
                    End Set
                End Property

                Public ReadOnly Property Channel As IChannel
                    Get
                        Return Me.itsChannel
                    End Get
                End Property
                Private Function FieldSignIsNegative(ByVal value As Long) As Boolean
                    Return System.Convert.ToBoolean(value And Me.itsBitSignMask) 'Bitwise logical And
                End Function
                Private Function ExtendNegativeSign(ByVal value As Long) As Long
                    Return ((&HFFFFFFFFFFFFFFFF << Me.itsLength) + value)
                End Function


                Protected Friend Function GetFieldValue(word As Long) As Double
                    Dim fieldValue As Long = ((word And Me.itsMask) >> Me.itsShift)
                    If Me.itsCodeType = eDiscreteCode.BNR AndAlso FieldSignIsNegative(fieldValue) Then
                        'BNR Numbers of Two's Complement number (Negative)
                        Return (ExtendNegativeSign(fieldValue) * Me.itsScalingFactor)
                    Else
                        Return (fieldValue * Me.itsScalingFactor)
                    End If

                End Function

                Private itsMask As Long
                Private itsShift As Byte
                Private itsCodeType As eDiscreteCode
                Private itsScalingFactor As Double = 1 'Default Value is 1
                Private itsDescription As String
                Private itsValue As Long = System.Int32.MaxValue
                Private itsChannel As IChannel
                Private itsBitSignMask As Long = 0 'Numero du bit de signe, Default value is 0 meaning not applicable (Signed Number)
                Private itsLength As Byte
            End Class

            Public Sub New(channel As IChannel)
                Me.bitFieldChannels = New BitFieldChannelCollection
                Me.itsChannel = channel
                AddHandler itsChannel.ValueChanged, AddressOf Me.OnValueChanged
            End Sub

            Public Sub AddBitFieldChannel(ByVal startBit As Byte, _
                                          ByVal stopBit As Byte, _
                                          ByVal code As eDiscreteCode, _
                                          ByVal scalingFactor As Double, _
                                          ByVal description As String, _
                                          ByVal destinationChannel As IChannel)
                Dim value As Double
                Dim newBitField As BitFieldChannel

                newBitField = Me.bitFieldChannels.AddBitFieldChannel(startBit, _
                                                                     stopBit - startBit + 1, _
                                                                     code, _
                                                                     scalingFactor, _
                                                                     description, _
                                                                     destinationChannel)

                If Not (Me.itsChannel Is Nothing) Then
                    value = Me.itsChannel.Value.ToDouble()
                Else
                    value = 0
                End If

                newBitField.Value = newBitField.GetFieldValue(CLng(value))
            End Sub

            Public Sub OnValueChanged(sender As IChannel, newVal As Value)
                Dim value As Long
                Dim bitField As BitFieldChannel

                Try
                    If Not newVal.ToString() = "Empty" And Not newVal.ToString() = "Not Connected" Then 'Si la valeur est "Empty" le ToDOuble va lancer une exception
                        value = CLng(newVal.ToDouble())
                    Else
                        value = 0
                    End If
                Catch e As Exception
                    value = 0
                End Try

                For Each bitField In Me.bitFieldChannels
                    bitField.Value = bitField.GetFieldValue(value)
                Next
            End Sub

            Public Sub Unregister()
                RemoveHandler itsChannel.ValueChanged, AddressOf Me.OnValueChanged
            End Sub

            Private bitFieldChannels As BitFieldChannelCollection
            Private itsChannel As IChannel
        End Class

        '********************************************
        '* Class DscreteTXValueChangedHandler *
        '********************************************

        Public Class DiscreteTXValueChangedHandler

            Private Class BitFieldChannelCollection
                Inherits System.Collections.CollectionBase

                Public Function AddBitFieldChannel(ByVal startBit As Byte, _
                                                   ByVal length As Byte, _
                                                   ByVal code As eDiscreteCode, _
                                                   ByVal scalingFactor As Double, _
                                                   ByVal description As String, _
                                                   ByVal destinationChannel As IChannel) As BitFieldChannel

                    Dim newBitFieldChannel As New BitFieldChannel(startBit, length, code, scalingFactor, description, destinationChannel)

                    MyBase.List.Add(newBitFieldChannel)

                    Return newBitFieldChannel
                End Function

                Public Property Item(ByVal index As Integer) As BitFieldChannel
                    Get
                        Return CType(MyBase.List.Item(index), BitFieldChannel)
                    End Get
                    Set(ByVal value As BitFieldChannel)
                        If (Not value Is MyBase.List.Item(index)) Then
                            MyBase.List.Item(index) = value
                        End If
                    End Set
                End Property
            End Class

            Private Class BitFieldChannel
                Public Sub New(ByVal startBit As Byte, _
                               ByVal length As Byte, _
                               ByVal code As eDiscreteCode, _
                               ByVal scalingFactor As Double, _
                               ByVal description As String, _
                               ByVal destinationChannel As IChannel)

                    Dim count As Byte
                    Dim baseMask As Long = (1 << (startBit - 1))
                    Me.itsShift = (startBit - 1)
                    Me.itsCode = code
                    Me.itsScalingFactor = scalingFactor
                    Me.itsDescription = description
                    Me.itsMask = baseMask

                    For count = 1 To CByte((length - 1))
                        Me.itsMask = ((Me.itsMask << 1) Or baseMask)
                    Next

                    Me.itsChannel = destinationChannel

                    If Not (Me.itsChannel Is Nothing) Then
                        Me.itsChannel.Value = New Global.KineticaRT.Values.Double(Me.itsValue)
                    End If

                End Sub

                Public Property Value As Long
                    Get
                        Return Me.itsValue
                    End Get
                    Set(newVal As Long)
                        If (newVal <> Me.itsValue) Then
                            Me.itsValue = newVal

                            If Not (Me.itsChannel Is Nothing) Then
                                Me.itsChannel.Value = New Global.KineticaRT.Values.Double(newVal)
                            End If
                        End If
                    End Set
                End Property

                Public ReadOnly Property Channel As IChannel
                    Get
                        Return Me.itsChannel
                    End Get
                End Property

                Protected Friend Function GetFieldValue(word As Long) As Double
                    Return (((word And Me.itsMask) >> Me.itsShift) * Me.itsScalingFactor)
                End Function

                Protected Friend Function PutFieldValue(word As Long) As Double
                    Dim result As Long
                    result = CLng((((word << Me.itsShift) And Me.itsMask) / Me.itsScalingFactor))
                    Me.itsValue = result
                    Return result
                End Function

                Private itsMask As Long
                Private itsShift As Byte
                Private itsCode As eDiscreteCode
                Private itsScalingFactor As Double
                Private itsDescription As String
                Private itsValue As Long = System.Int32.MaxValue
                Private itsChannel As IChannel
            End Class

            Public Sub New(channel As IChannel)
                Me.bitFieldChannels = New BitFieldChannelCollection
                Me.itsChannel = channel
            End Sub

            Public Sub AddBitFieldChannel(ByVal startBit As Byte, _
                                          ByVal stopBit As Byte, _
                                          ByVal code As eDiscreteCode, _
                                          ByVal scalingFactor As Double, _
                                          ByVal description As String, _
                                          ByVal destinationChannel As IChannel)
                Dim value As Double
                Dim newBitField As BitFieldChannel

                newBitField = Me.bitFieldChannels.AddBitFieldChannel(startBit, _
                                                                     CByte(stopBit - startBit + 1), _
                                                                     code, _
                                                                     scalingFactor, _
                                                                     description, _
                                                                     destinationChannel)

                AddHandler destinationChannel.ValueChanged, AddressOf Me.OnValueChanged
                Try
                    value = destinationChannel.Value.ToDouble()
                Catch e As Exception
                    value = 0
                End Try
                newBitField.Value = CLng(newBitField.GetFieldValue(CLng(value)))
            End Sub

            Public Sub OnValueChanged(sender As IChannel, newVal As Value)
                Dim value As Long
                Dim bitField As BitFieldChannel
                Dim labelValue As Long

                Try
                    If Not newVal.ToString() = "Empty" And Not newVal.ToString() = "Not Connected" Then 'Si la valeur est "Empty" ou "Not Connected" le ToDOuble va lancer une exception
                        value = CLng(newVal.ToDouble())
                    Else
                        value = 0
                    End If

                Catch e As Exception
                    value = 0

                    Exit Sub
                End Try

                labelValue = 0

                For Each bitField In Me.bitFieldChannels
                    If sender.Name = bitField.Channel.Name Then
                        labelValue += CLng(bitField.PutFieldValue(value))
                    Else
                        labelValue += bitField.Value
                    End If
                Next

                If Not (Me.itsChannel Is Nothing) Then
                    Me.itsChannel.Value = New Global.KineticaRT.Values.Double(labelValue)
                End If

            End Sub

            Public Sub Unregister()
                Dim bitField As BitFieldChannel

                For Each bitField In Me.bitFieldChannels
                    RemoveHandler bitField.Channel.ValueChanged, AddressOf Me.OnValueChanged
                Next
            End Sub

            Private bitFieldChannels As BitFieldChannelCollection
            Private itsChannel As IChannel
        End Class


        '--------------------------\
        '- Uart Specific Functions -|
        '--------------------------/

        Private Class UartFrameManipulator

            Private Structure Layout
                Dim Top As System.Drawing.Point
                Dim Bottom As System.Drawing.Point
            End Structure

            Private Left As Layout
            Private CenterLeft As Layout
            Private CenterRight As Layout
            Private Right As Layout

            Private FrameCount As Integer
            Private DataFrame() As System.Windows.Forms.Control
            Private FaultFrame() As System.Windows.Forms.Control


            Public Enum ePosition
                Left
                CenterLeft
                CenterRight
                Right
            End Enum

            Public Sub New()
                Me.Left.Top = New System.Drawing.Point(8, 8)
                Me.Left.Bottom = New System.Drawing.Point(8, 560)
                Me.CenterLeft.Top = New System.Drawing.Point(464, 8)
                Me.CenterLeft.Bottom = New System.Drawing.Point(464, 560)
                Me.CenterRight.Top = New System.Drawing.Point(960, 8)
                Me.CenterRight.Bottom = New System.Drawing.Point(960, 560)
                Me.Right.Top = New System.Drawing.Point(1416, 8)
                Me.Right.Bottom = New System.Drawing.Point(1416, 560)

                Me.FrameCount = 0
                ReDim Me.DataFrame(3)
                ReDim Me.FaultFrame(3)
            End Sub

            Public Sub Load(ByRef DataFrameHandler As System.Windows.Forms.Control, ByRef FaultFrameHandler As System.Windows.Forms.Control)

                If (DataFrameHandler Is Nothing) Or (FaultFrameHandler Is Nothing) Then
                    Exit Sub
                End If

                If (FrameCount < 4) And (FrameCount >= 0) Then
                    DataFrame(FrameCount) = DataFrameHandler
                    FaultFrame(FrameCount) = FaultFrameHandler
                    FrameCount += 1
                Else
                    clsTopMostMessageBox.Show("UartFrameManipulator.Load : " & Environment.NewLine & "Max number of frames reached. Cannot load more frames", _
                                    "Warning", _
                                    MessageBoxButtons.OK, _
                                    MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End Sub

            Public Sub Place(ByRef DataFrameHandler As System.Windows.Forms.Control, ByRef FaultFrameHandler As System.Windows.Forms.Control, ByRef Position As ePosition)

                If (DataFrameHandler Is Nothing) Or (FaultFrameHandler Is Nothing) Then
                    clsTopMostMessageBox.Show("UartFrameManipulator.Place" & Environment.NewLine & "Frame handlers evaluate to nothing", _
                                    "Warning", _
                                    MessageBoxButtons.OK, _
                                    MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Select Case Position
                    Case ePosition.Left
                        DataFrameHandler.Location = Left.Top
                        FaultFrameHandler.Location = Left.Bottom
                    Case ePosition.CenterLeft
                        DataFrameHandler.Location = CenterLeft.Top
                        FaultFrameHandler.Location = CenterLeft.Bottom
                    Case ePosition.CenterRight
                        DataFrameHandler.Location = CenterRight.Top
                        FaultFrameHandler.Location = CenterRight.Bottom
                    Case ePosition.Right
                        DataFrameHandler.Location = Right.Top
                        FaultFrameHandler.Location = Right.Bottom
                End Select

            End Sub

            Public Sub Commit()

                If (FrameCount <= 4) And (FrameCount > 0) Then

                    Select Case FrameCount
                        Case 1
                            Place(DataFrame(0), FaultFrame(0), ePosition.Left)
                        Case 2
                            Place(DataFrame(0), FaultFrame(0), ePosition.CenterLeft)
                            Place(DataFrame(1), FaultFrame(1), ePosition.CenterRight)
                        Case 3
                            Place(DataFrame(0), FaultFrame(0), ePosition.Left)
                            Place(DataFrame(1), FaultFrame(1), ePosition.CenterLeft)
                            Place(DataFrame(2), FaultFrame(2), ePosition.CenterRight)
                        Case 4
                            Place(DataFrame(0), FaultFrame(0), ePosition.Left)
                            Place(DataFrame(1), FaultFrame(1), ePosition.CenterLeft)
                            Place(DataFrame(2), FaultFrame(2), ePosition.CenterRight)
                            Place(DataFrame(3), FaultFrame(3), ePosition.Right)
                    End Select

                Else
                    'MessageBox.Show("UartFrameManipulator.Commit : " & Environment.NewLine & "FrameCount is invalid. Maximum number of frames for the Uart screen is four", _
                    '                "Warning", _
                    '                MessageBoxButtons.OK, _
                    '                MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Sub

            Public Sub Reset()
                If DataFrame IsNot Nothing Then
                    Array.Clear(DataFrame, 0, DataFrame.Length)
                End If
                If FaultFrame IsNot Nothing Then
                    Array.Clear(FaultFrame, 0, FaultFrame.Length)
                End If
                FrameCount = 0
            End Sub

        End Class

        Public Sub UpdateUartLayout()

            Dim UartLayout As New UartFrameManipulator

            'Development UART
            If (devUartDataGridChA IsNot Nothing) Then
                If ((devUartFaultViewChA IsNot Nothing) And (devUartStatusViewChA IsNot Nothing)) Then
                    If devUartRxChannelListChA.Count > 0 Then
                        Me.devUartDataGridChA.Parent.Parent.Visible = True
                        Me.devUartFaultViewChA.Parent.Parent.Visible = True
                        UartLayout.Load(devUartDataGridChA.Parent.Parent, devUartFaultViewChA.Parent.Parent)
                    Else
                        Me.devUartDataGridChA.Parent.Parent.Visible = False
                        Me.devUartFaultViewChA.Parent.Parent.Visible = False
                    End If
                End If
            End If

            If (devUartDataGridChB IsNot Nothing) Then
                If ((devUartFaultViewChB IsNot Nothing) And (devUartStatusViewChB IsNot Nothing)) Then
                    If devUartRxChannelListChB.Count > 0 Then
                        Me.devUartDataGridChB.Parent.Parent.Visible = True
                        Me.devUartFaultViewChB.Parent.Parent.Visible = True
                        UartLayout.Load(devUartDataGridChB.Parent.Parent, devUartFaultViewChB.Parent.Parent)
                    Else
                        Me.devUartDataGridChB.Parent.Parent.Visible = False
                        Me.devUartFaultViewChB.Parent.Parent.Visible = False
                    End If

                End If
            End If

            'Cockpit Uart
            If (cockpitUartDataGridChA IsNot Nothing) Then
                If ((cockpitUartFaultViewChA IsNot Nothing) And (cockpitUartStatusViewChA IsNot Nothing)) Then
                    If cockpitUartRxChannelListChA.Count > 0 Then
                        Me.cockpitUartDataGridChA.Parent.Parent.Visible = True
                        Me.cockpitUartFaultViewChA.Parent.Parent.Visible = True
                        UartLayout.Load(cockpitUartDataGridChA.Parent.Parent, cockpitUartFaultViewChA.Parent.Parent)
                    Else
                        Me.cockpitUartDataGridChA.Parent.Parent.Visible = False
                        Me.cockpitUartFaultViewChA.Parent.Parent.Visible = False
                    End If
                End If
            End If

            If (cockpitUartDataGridChB IsNot Nothing) Then
                If ((cockpitUartFaultViewChB IsNot Nothing) And (cockpitUartStatusViewChB IsNot Nothing)) Then
                    If cockpitUartRxChannelListChB.Count > 0 Then
                        Me.cockpitUartDataGridChB.Parent.Parent.Visible = True
                        Me.cockpitUartFaultViewChB.Parent.Parent.Visible = True
                        UartLayout.Load(cockpitUartDataGridChB.Parent.Parent, cockpitUartFaultViewChB.Parent.Parent)

                    Else
                        Me.cockpitUartDataGridChB.Parent.Parent.Visible = False
                        Me.cockpitUartFaultViewChB.Parent.Parent.Visible = False
                    End If
                End If
            End If

            UartLayout.Commit()
        End Sub

        Public Sub InitializeUartGridLists(ByVal EngineModel As String)
            If EngineModel = "SYSTEM CHECK" Or EngineModel = "DAS CALIBRATION" Then
                Exit Sub
            End If

            'Development UART data
            devUartDataGridChA = CType(CELProject.SearchControl(frmHmiMaster, "DevUartRxChA"), CEL.Controls.MultiChannelDisplay)
            If IsNothing(devUartDataGridChA) And Not IsNothing(frmHmiSlave) Then
                devUartDataGridChA = CType(CELProject.SearchControl(frmHmiSlave, "DevUartRxChA"), CEL.Controls.MultiChannelDisplay)
            End If

            devUartDataGridChB = CType(CELProject.SearchControl(frmHmiMaster, "DevUartRxChB"), CEL.Controls.MultiChannelDisplay)
            If IsNothing(devUartDataGridChB) And Not IsNothing(frmHmiSlave) Then
                devUartDataGridChB = CType(CELProject.SearchControl(frmHmiSlave, "DevUartRxChB"), CEL.Controls.MultiChannelDisplay)
            End If

            'Development UART faults/status
            devUartFaultViewChA = CType(CELProject.SearchControl(frmHmiMaster, "DevUartFaultChA"), CEL.Controls.FaultView)
            If IsNothing(devUartFaultViewChA) And Not IsNothing(frmHmiSlave) Then
                devUartFaultViewChA = CType(CELProject.SearchControl(frmHmiSlave, "DevUartFaultChA"), CEL.Controls.FaultView)
            End If

            devUartFaultViewChB = CType(CELProject.SearchControl(frmHmiMaster, "DevUartFaultChB"), CEL.Controls.FaultView)
            If IsNothing(devUartFaultViewChB) And Not IsNothing(frmHmiSlave) Then
                devUartFaultViewChB = CType(CELProject.SearchControl(frmHmiSlave, "DevUartFaultChB"), CEL.Controls.FaultView)
            End If

            devUartStatusViewChA = CType(CELProject.SearchControl(frmHmiMaster, "DevUartStatusChA"), CEL.Controls.FaultView)
            If IsNothing(devUartStatusViewChA) And Not IsNothing(frmHmiSlave) Then
                devUartStatusViewChA = CType(CELProject.SearchControl(frmHmiSlave, "DevUartStatusChA"), CEL.Controls.FaultView)
            End If

            devUartStatusViewChB = CType(CELProject.SearchControl(frmHmiMaster, "DevUartStatusChB"), CEL.Controls.FaultView)
            If IsNothing(devUartStatusViewChB) And Not IsNothing(frmHmiSlave) Then
                devUartStatusViewChB = CType(CELProject.SearchControl(frmHmiSlave, "DevUartStatusChB"), CEL.Controls.FaultView)
            End If

            'cockpit UART data
            cockpitUartDataGridChA = CType(CELProject.SearchControl(frmHmiMaster, "CockpitUartRxChA"), CEL.Controls.MultiChannelDisplay)
            If IsNothing(cockpitUartDataGridChA) And Not IsNothing(frmHmiSlave) Then
                cockpitUartDataGridChA = CType(CELProject.SearchControl(frmHmiSlave, "CockpitUartRxChA"), CEL.Controls.MultiChannelDisplay)
            End If

            cockpitUartDataGridChB = CType(CELProject.SearchControl(frmHmiMaster, "CockpitUartRxChB"), CEL.Controls.MultiChannelDisplay)
            If IsNothing(cockpitUartDataGridChB) And Not IsNothing(frmHmiSlave) Then
                cockpitUartDataGridChB = CType(CELProject.SearchControl(frmHmiSlave, "CockpitUartRxChB"), CEL.Controls.MultiChannelDisplay)
            End If

            'cockpit UART faults/Status
            cockpitUartFaultViewChA = CType(CELProject.SearchControl(frmHmiMaster, "CockpitUartFaultChA"), CEL.Controls.FaultView)
            If IsNothing(cockpitUartFaultViewChA) And Not IsNothing(frmHmiSlave) Then
                cockpitUartFaultViewChA = CType(CELProject.SearchControl(frmHmiSlave, "CockpitUartFaultChA"), CEL.Controls.FaultView)
            End If

            cockpitUartFaultViewChB = CType(CELProject.SearchControl(frmHmiMaster, "CockpitUartFaultChB"), CEL.Controls.FaultView)
            If IsNothing(cockpitUartFaultViewChB) And Not IsNothing(frmHmiSlave) Then
                cockpitUartFaultViewChB = CType(CELProject.SearchControl(frmHmiSlave, "CockpitUartFaultChB"), CEL.Controls.FaultView)
            End If

            cockpitUartStatusViewChA = CType(CELProject.SearchControl(frmHmiMaster, "CockpitUartStatusChA"), CEL.Controls.FaultView)
            If IsNothing(cockpitUartStatusViewChA) And Not IsNothing(frmHmiSlave) Then
                cockpitUartStatusViewChA = CType(CELProject.SearchControl(frmHmiSlave, "CockpitUartStatusChA"), CEL.Controls.FaultView)
            End If

            cockpitUartStatusViewChB = CType(CELProject.SearchControl(frmHmiMaster, "CockpitUartStatusChB"), CEL.Controls.FaultView)
            If IsNothing(cockpitUartStatusViewChB) And Not IsNothing(frmHmiSlave) Then
                cockpitUartStatusViewChB = CType(CELProject.SearchControl(frmHmiSlave, "CockpitUartStatusChB"), CEL.Controls.FaultView)
            End If
        End Sub

        Public Sub UartDataBindToDataGrid()
            'Development Uart
            If Not (devUartDataGridChA Is Nothing) AndAlso devUartRxChannelListChA.Count > 0 Then
                devUartDataGridChA.SetDataBinding(devUartRxChannelListChA)
            End If
            If Not (devUartDataGridChB Is Nothing) AndAlso devUartRxChannelListChB.Count > 0 Then
                devUartDataGridChB.SetDataBinding(devUartRxChannelListChB)
            End If
            If Not (devUartFaultViewChA Is Nothing) AndAlso devUartFaultChannelListChA.Count > 0 Then
                devUartFaultViewChA.SetDataBinding(devUartFaultChannelListChA)
            End If
            If Not (devUartFaultViewChB Is Nothing) AndAlso devUartFaultChannelListChB.Count > 0 Then
                devUartFaultViewChB.SetDataBinding(devUartFaultChannelListChB)
            End If
            If Not (devUartStatusViewChA Is Nothing) AndAlso devUartStatusChannelListChA.Count > 0 Then
                devUartStatusViewChA.SetDataBinding(devUartStatusChannelListChA)
            End If
            If Not (devUartStatusViewChB Is Nothing) AndAlso devUartStatusChannelListChB.Count > 0 Then
                devUartStatusViewChB.SetDataBinding(devUartStatusChannelListChB)
            End If

            'Cockpit Uart
            If Not (cockpitUartDataGridChA Is Nothing) AndAlso cockpitUartRxChannelListChA.Count > 0 Then
                cockpitUartDataGridChA.SetDataBinding(cockpitUartRxChannelListChA)
            End If
            If Not (cockpitUartDataGridChB Is Nothing) AndAlso cockpitUartRxChannelListChB.Count > 0 Then
                cockpitUartDataGridChB.SetDataBinding(cockpitUartRxChannelListChB)
            End If
            If Not (cockpitUartFaultViewChA Is Nothing) AndAlso cockpitUartFaultChannelListChA.Count > 0 Then
                cockpitUartFaultViewChA.SetDataBinding(cockpitUartFaultChannelListChA)
            End If
            If Not (cockpitUartFaultViewChB Is Nothing) AndAlso cockpitUartFaultChannelListChB.Count > 0 Then
                cockpitUartFaultViewChB.SetDataBinding(cockpitUartFaultChannelListChB)
            End If
            If Not (cockpitUartStatusViewChA Is Nothing) AndAlso cockpitUartStatusChannelListChA.Count > 0 Then
                cockpitUartStatusViewChA.SetDataBinding(cockpitUartStatusChannelListChA)
            End If
            If Not (cockpitUartStatusViewChB Is Nothing) AndAlso cockpitUartStatusChannelListChB.Count > 0 Then
                cockpitUartStatusViewChB.SetDataBinding(cockpitUartStatusChannelListChB)
            End If
        End Sub

    End Class


End Namespace
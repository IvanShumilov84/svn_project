'Option Strict On
'Imports Excel
Imports KineticaRT
Imports KineticaRT.Sequence
Imports KineticaRT.Servers.OpcClient
Imports KineticaRT.Logic.Message
Imports KineticaRT.Values
Imports KineticaRT.Channels
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
Imports CEL.Data.BusinessObjects.Dacn1AfdxSignal
Imports CEL.Data.BusinessObjects.Dacn1AfdxMessage
Imports CEL.Data.BusinessObjects.Dacn1AfdxVirtualLink
Imports CEL.Data.BusinessObjects.Dacn1Afdx
Imports System.Collections.Generic
Imports KineticaRT.Studio.Application.Project

Namespace CEL.HMI

    Public Class AFDX

        Private CELProject As CELDAS = CELDAS.Instance()


        Const ArincLabelFormatDiscrete As Byte = 2


        '********************************************************************
        '* Variable Declaration *********************************************
        '********************************************************************		

        Private colRxFaultDiscreteHandlers As System.Collections.Generic.Dictionary(Of String, DiscreteRXValueChangedHandler)
        Private colTxFaultDiscreteHandlers As System.Collections.Generic.Dictionary(Of String, DiscreteTXValueChangedHandler)


        Private colRxFdsMessages As CEL.ARINCChannelCollection = Nothing 'Receive  Each AFDX Message is splitted in multiple Fds (Functional Data Sets)
        Private colTxFdsMessages As CEL.ARINCChannelCollection = Nothing 'Transmit Each AFDX Message is splitted in multiple Fds (Functional Data Sets)

        Private colFaultChannelList As CEL.ACMCChannelCollection = Nothing
        Private colStatusChannelList As CEL.ACMCChannelCollection = Nothing

        Private dgvReceive As CEL.Controls.MultiChannelDisplay = Nothing
        Private dgvTransmit As CEL.Controls.MultiChannelDisplay = Nothing


        Private ctrlFaultView As CEL.Controls.FaultView = Nothing
        Private ctrlStatusView As CEL.Controls.FaultView = Nothing

        Private CallDataBindToDataGrid As New MethodInvoker(AddressOf Me.DataBindToDataGrid)



        Private DicArchiveChannelList As New Collections.Generic.Dictionary(Of String, Integer) 'used to keep channels that will be recorded and their recorder Index


        Private ChannelList As Dictionary(Of String, String) 'Used in OpenTest to get a list of All created channel with their corresponding Unit

        Public Sub New()
            MyBase.New()

            Me.colRxFaultDiscreteHandlers = New Dictionary(Of String, DiscreteRXValueChangedHandler)  'New System.Collections.ArrayList
            Me.colTxFaultDiscreteHandlers = New Dictionary(Of String, DiscreteTXValueChangedHandler)   'New System.Collections.ArrayList
            Me.colRxFdsMessages = New CEL.ARINCChannelCollection
            Me.colTxFdsMessages = New CEL.ARINCChannelCollection
            Me.colFaultChannelList = New CEL.ACMCChannelCollection
            Me.colStatusChannelList = New CEL.ACMCChannelCollection


        End Sub


        Private Sub DataBindToDataGrid()

            If Not (dgvReceive Is Nothing) Then
                If colRxFdsMessages.Count = 0 Then
                    Me.dgvReceive.Parent.Parent.Visible = False
                Else
                    Me.dgvReceive.Parent.Parent.Visible = True
                    dgvReceive.SetDataBinding(colRxFdsMessages)
                End If
            End If

            If Not (dgvTransmit Is Nothing) Then
                If colTxFdsMessages.Count = 0 Then
                    Me.dgvTransmit.Parent.Parent.Visible = False
                Else
                    Me.dgvTransmit.Parent.Parent.Visible = True
                    dgvTransmit.SetDataBinding(colTxFdsMessages)
                End If
            End If

            If Not (ctrlFaultView Is Nothing) Then
                Me.ctrlFaultView.Visible = True
                If colFaultChannelList.Count > 0 Then
                    ctrlFaultView.SetDataBinding(colFaultChannelList)
                End If
            End If


            If Not (ctrlStatusView Is Nothing) Then
                Me.ctrlStatusView.Visible = True
                If colStatusChannelList.Count > 0 Then
                    ctrlStatusView.SetDataBinding(colStatusChannelList)
                End If
            End If

            If Not IsNothing(ctrlFaultView) Then
                If colStatusChannelList.Count = 0 And colFaultChannelList.Count = 0 Then
                    Me.ctrlFaultView.Parent.Parent.Visible = False
                Else
                    Me.ctrlStatusView.Parent.Parent.Visible = True
                End If
            End If

        End Sub

        Private Sub InitializeGridLists(ByVal engine_model As String)

            If engine_model = "SYSTEM CHECK" Or engine_model = "DAS CALIBRATION" Then
                Exit Sub
            End If

            Dim HMI_Master As System.Windows.Forms.Control = Nothing
            Dim HMI_Slave As System.Windows.Forms.Control = Nothing


            'Query Database
            esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
            Dim windowCollection As UUTHMIWindowCollection = New UUTHMIWindowCollection()
            windowCollection.es.Connection.Name = CELProject.DefaultConnectionName
            windowCollection.Query.Where(windowCollection.Query.ProjectID = CELProject.Project.Number)
            windowCollection.Query.Where(windowCollection.Query.MainScreen = True)
            windowCollection.Query.Where(windowCollection.Query.EngineModel = engine_model)
            windowCollection.Query.Where(windowCollection.Query.MeasurementUnitId = CELProject.MeasurementUnitInUse)
            windowCollection.Query.OrderBy(windowCollection.Query.HMIWindowName.Ascending)
            windowCollection.Query.Load()


            For Each hmiWindow As UUTHMIWindow In windowCollection

                If hmiWindow.Master = True Then
                    HMI_Master = CELProject.GetObjectCallBack(hmiWindow.HMIWindowName)
                    Debug.Assert(Not IsNothing(HMI_Master))
                End If

                If hmiWindow.Slave = True Then
                    HMI_Slave = CELProject.GetObjectCallBack(hmiWindow.HMIWindowName)
                    Debug.Assert(Not IsNothing(HMI_Slave))
                End If

            Next


            'ARINC data
            dgvReceive = CType(CELProject.SearchControl(HMI_Master, "AFDXChARxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            If IsNothing(dgvReceive) And Not IsNothing(HMI_Slave) Then
                dgvReceive = CType(CELProject.SearchControl(HMI_Slave, "AFDXChARxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            End If

            dgvTransmit = CType(CELProject.SearchControl(HMI_Master, "AFDXChATxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            If IsNothing(dgvTransmit) And Not IsNothing(HMI_Slave) Then
                dgvTransmit = CType(CELProject.SearchControl(HMI_Slave, "AFDXChATxDataDisplay"), CEL.Controls.MultiChannelDisplay)
            End If

            'ARINC faults and status
            ctrlFaultView = CType(CELProject.SearchControl(HMI_Master, "AFDXFaultChADataDisplay"), CEL.Controls.FaultView)
            If IsNothing(ctrlFaultView) And Not IsNothing(HMI_Slave) Then
                ctrlFaultView = CType(CELProject.SearchControl(HMI_Slave, "AFDXFaultChADataDisplay"), CEL.Controls.FaultView)
            End If

            ctrlStatusView = CType(CELProject.SearchControl(HMI_Master, "AFDXStatusChADataDisplay"), CEL.Controls.FaultView)
            If IsNothing(ctrlStatusView) And Not IsNothing(HMI_Slave) Then
                ctrlStatusView = CType(CELProject.SearchControl(HMI_Slave, "AFDXStatusChADataDisplay"), CEL.Controls.FaultView)
            End If



        End Sub


        Private Function CreateChannelNumericOpcItem(ByVal channelName As String, _
                                                     ByVal description As String, _
                                                     ByVal tooltip As String, _
                                                     ByVal opcGroup As String, _
                                                     ByVal opcItemId As String, _
                                                     ByVal unit As String) As Global.KineticaRT.Servers.OpcClient.Item

            Dim newChannel As Global.KineticaRT.Servers.OpcClient.Item = New Global.KineticaRT.Servers.OpcClient.Item


            newChannel.Description = description
            newChannel.Name = channelName
            newChannel.Group = opcGroup
            newChannel.ItemID = opcItemId
            newChannel.Units = unit

            Return newChannel


        End Function
        Private Function CreateChannelTextOpcItem(ByVal channelName As String, _
                                                  ByVal description As String, _
                                                  ByVal tooltip As String, _
                                                  ByVal opcGroup As String, _
                                                  ByVal opcItemId As String) As TextItem

            Dim newChannel As New TextItem

            newChannel.Description = description
            newChannel.ToolTip = tooltip
            newChannel.ItemID = opcItemId
            newChannel.Group = opcGroup
            newChannel.Name = channelName

            Return newChannel

        End Function

        Private Function SetupMessages(ByVal collectionName As String, ByRef channelCollection As ChannelCollection) As Boolean

            SetupMessages = False

            Dim dicAfdxOpcFdsSignalChannelList As New Dictionary(Of String, String)
            Dim dicAfdxOpcFsChannelList As New Dictionary(Of String, String)

            Try

                If Not IsNothing(channelCollection) Then

                    Dim counter(1, 4) As Integer
                    counter(eTransmissionMode.Rx, eDataType.String) = 1  '0,0
                    counter(eTransmissionMode.Rx, eDataType.Integer) = 1 '0,1
                    counter(eTransmissionMode.Rx, eDataType.Float) = 1   '0,2
                    counter(eTransmissionMode.Rx, 3) = 1   '0,3 le FS utilise un USINT
                    counter(eTransmissionMode.Rx, 4) = 1   '0,3  utilise un UDINT
                    counter(eTransmissionMode.Tx, eDataType.String) = 1  '1,0
                    counter(eTransmissionMode.Tx, eDataType.Integer) = 1  '1,1
                    counter(eTransmissionMode.Tx, eDataType.Float) = 1   '1,2
                    counter(eTransmissionMode.Tx, 3) = 1   '1,3  le FS utilise un USDINT
                    counter(eTransmissionMode.Tx, 4) = 1   '1,3   utilise un UDINT


                    'Query Database
                    Dim messageCollection As Dacn1AfdxMessageCollection = New Dacn1AfdxMessageCollection()
                    messageCollection.es.Connection.Name = CELProject.DefaultConnectionName
                    messageCollection.Query.es.QuerySource = "DACN1_AFDX_MESSAGE" 'TODO.    Remap sur la table via app.config?
                    messageCollection.Query.Where(messageCollection.Query.ExportToCsv = True)
                    messageCollection.Query.Where(messageCollection.Query.CollectionName = collectionName)
                    messageCollection.Query.OrderBy(messageCollection.Query.CollectionName.Ascending)
                    messageCollection.Query.OrderBy(messageCollection.Query.Vlid.Ascending)
                    messageCollection.Query.OrderBy(messageCollection.Query.UdpPortSource.Ascending)
                    messageCollection.Query.OrderBy(messageCollection.Query.UdpPortDestination.Ascending)
                    messageCollection.Query.OrderBy(messageCollection.Query.FdsAddress.Ascending)
                    messageCollection.Query.Load()


                    For Each fds As Dacn1AfdxMessage In messageCollection

                        If fds.UpToDacn1AfdxPortByCollectionName.UpToDacn1AfdxVirtualLinkByCollectionName.ExportToCsv = True Then

                            Dim signalCollection As Dacn1AfdxSignalCollection = fds.Dacn1AfdxSignalCollectionByCollectionName

                            signalCollection.Query.OrderBy(signalCollection.Query.SignalAddress.Ascending)
                            signalCollection.Query.OrderBy(signalCollection.Query.StartBit.Ascending)
                            signalCollection.Query.OrderBy(signalCollection.Query.StopBit.Ascending)
                            signalCollection.Query.Load()

                            For Each signal As Dacn1AfdxSignal In signalCollection

                                Dim channelAlreadyAdded As Boolean = False


                                'Important ne pas utiliser le FdsName dans la ke pcq Pratt a des doublons de Nom de Fds ex. VL 6854 port 107171 FdsAddess 36 
                                Dim keyFdsSignal As String = fds.Vlid.ToString + "_" + _
                                                             fds.UdpPortSource.ToString + "_" + _
                                                             fds.UdpPortSource.ToString + "_" + _
                                                             fds.FdsAddress.ToString + "_" + _
                                                             signal.SignalAddress.ToString

                                Dim keyFs As String = fds.Vlid.ToString + "_" + _
                                                      fds.UdpPortSource.ToString + "_" + _
                                                      fds.UdpPortSource.ToString + "_" + _
                                                      fds.FsAddress.ToString



                                Dim supported As Boolean = True
                                Dim opcFsChannelName As String = String.Empty
                                Dim opcFdsChannelName As String = String.Empty
                                Dim opcItemId As String = "dacn1.dacn1_afdx_iomgr"
                                Dim opcItemIdDirection As String = String.Empty



                                If Not dicAfdxOpcFdsSignalChannelList.ContainsKey(keyFdsSignal) Then


                                    Select Case fds.TxRxEnum
                                        Case eTransmissionMode.Rx
                                            opcFdsChannelName = "User_Int_A664IN"
                                            opcItemIdDirection = "in"

                                        Case eTransmissionMode.Tx
                                            opcFdsChannelName = "User_Int_A664OUT"
                                            opcItemIdDirection = "out"
                                        Case Else
                                            Debug.Assert(False)
                                    End Select



                                    'Creation of default OPC Channels
                                    Select Case signal.DataTypeEnum

                                        Case eDataType.String
                                            opcFdsChannelName = String.Format("{0}.STRING_{1}", opcFdsChannelName, counter(fds.TxRxEnum, eDataType.String))
                                            opcItemId = String.Format("{0}.signal_string_{1}[{2}].value", opcItemId, opcItemIdDirection, counter(fds.TxRxEnum, eDataType.String))
                                            counter(fds.TxRxEnum, eDataType.String) = counter(fds.TxRxEnum, eDataType.String) + 1
                                            Dim opcFdsChannel As TextItem = CreateChannelTextOpcItem(opcFdsChannelName, _
                                                                                                                               signal.FdsName.Replace("_", " "), _
                                                                                                                               signal.FdsName, _
                                                                                                                               "Group_Fast", _
                                                                                                                               opcItemId)
                                            channelCollection.Add(opcFdsChannel)
                                            dicAfdxOpcFdsSignalChannelList.Add(keyFdsSignal, opcFdsChannelName)



                                        Case eDataType.Boolean, eDataType.Enumeration

                                            opcFdsChannelName = String.Format("{0}.UDINTEGER_{1}", opcFdsChannelName, counter(fds.TxRxEnum, 4))
                                            opcItemId = String.Format("{0}.signal_udint_{1}[{2}].value", opcItemId, opcItemIdDirection, counter(fds.TxRxEnum, 4))
                                            counter(fds.TxRxEnum, 4) = counter(fds.TxRxEnum, 4) + 1 'DataType UDINT
                                            Dim opcFdsChannel As Item = CreateChannelNumericOpcItem(opcFdsChannelName, _
                                                                                                                              signal.FdsName.Replace("_", " "), _
                                                                                                                              signal.FdsName, _
                                                                                                                              "Group_Fast", _
                                                                                                                              opcItemId, _
                                                                                                                              signal.UpToEngineeringUnitByEngUnitID.DisplayName)
                                            'If message.DataTypeEnum = eDataType.Boolean Then 'For discrete displays HEXA


                                            If SetupDiscrete(signal,
                                                             channelCollection, _
                                                             fds.FdsName, _
                                                             fds.UdpPortDestination.ToString(), _
                                                             opcFdsChannel, _
                                                             fds.TxRxEnum) Then
                                                opcFdsChannel.Format = "X"
                                                opcFdsChannel.Units = " Hex"
                                                channelAlreadyAdded = True
                                            Else
                                                opcFdsChannel.Format = signal.DisplayFormat
                                            End If

                                            channelCollection.Add(opcFdsChannel)
                                            dicAfdxOpcFdsSignalChannelList.Add(keyFdsSignal, opcFdsChannelName)

                                        Case eDataType.Integer


                                            opcFdsChannelName = String.Format("{0}.INTEGER_{1}", opcFdsChannelName, counter(fds.TxRxEnum, eDataType.Integer))
                                            opcItemId = String.Format("{0}.signal_int_{1}[{2}].value", opcItemId, opcItemIdDirection, counter(fds.TxRxEnum, eDataType.Integer))
                                            counter(fds.TxRxEnum, eDataType.Integer) = counter(fds.TxRxEnum, eDataType.Integer) + 1
                                            Dim opcFdsChannel As Item = CreateChannelNumericOpcItem(opcFdsChannelName, _
                                                                                                                              signal.FdsName.Replace("_", " "), _
                                                                                                                              signal.FdsName, _
                                                                                                                              "Group_Fast", _
                                                                                                                              opcItemId, _
                                                                                                                              signal.UpToEngineeringUnitByEngUnitID.DisplayName)
                                            'If message.DataTypeEnum = eDataType.Boolean Then 'For discrete displays HEXA


                                            If SetupDiscrete(signal,
                                                             channelCollection, _
                                                             fds.FdsName, _
                                                             fds.UdpPortDestination.ToString(), _
                                                             opcFdsChannel, _
                                                             fds.TxRxEnum) Then
                                                opcFdsChannel.Format = "X"
                                                opcFdsChannel.Units = " Hex"
                                                channelAlreadyAdded = True
                                            Else
                                                opcFdsChannel.Format = signal.DisplayFormat
                                            End If

                                            channelCollection.Add(opcFdsChannel)
                                            dicAfdxOpcFdsSignalChannelList.Add(keyFdsSignal, opcFdsChannelName)


                                        Case eDataType.Float

                                            'Creation of FDS signal
                                            opcFdsChannelName = String.Format("{0}.REAL_{1}", opcFdsChannelName, counter(fds.TxRxEnum, eDataType.Float))
                                            opcItemId = String.Format("{0}.signal_real_{1}[{2}].value", opcItemId, opcItemIdDirection, counter(fds.TxRxEnum, eDataType.Float))
                                            counter(fds.TxRxEnum, eDataType.Float) = counter(fds.TxRxEnum, eDataType.Float) + 1
                                            Dim opcFdsChannel As Item = CreateChannelNumericOpcItem(opcFdsChannelName, _
                                                                                                                              signal.FdsName, _
                                                                                                                              signal.FdsName.Replace("_", " "), _
                                                                                                                              "Group_Fast", _
                                                                                                                              opcItemId, _
                                                                                                                              signal.UpToEngineeringUnitByEngUnitID.DisplayName)
                                            opcFdsChannel.Format = signal.DisplayFormat
                                            channelCollection.Add(opcFdsChannel)
                                            dicAfdxOpcFdsSignalChannelList.Add(keyFdsSignal, opcFdsChannelName)


                                        Case Else
                                            'not supported
                                            'Debug.Assert(False)
                                            supported = False
                                    End Select


                                    Select Case signal.DataTypeEnum

                                        Case eDataType.String

                                            Dim newFdsChannel As New CEL.KineticaRT.Servers.Logger.TextChannel

                                            newFdsChannel.Source = opcFdsChannelName
                                            newFdsChannel.Enabled = DicArchiveChannelList.ContainsKey(fds.FdsName)
                                            If newFdsChannel.Enabled Then
                                                newFdsChannel.Index = DicArchiveChannelList(fds.FdsName) 'Index
                                            Else
                                                newFdsChannel.Index = 0
                                            End If
                                            newFdsChannel.Logger = "ARCHIVE"
                                            newFdsChannel.EventLogger = "EVENT_ARCHIVE"
                                            'newFdsChannel.Name = fds.FdsName + "_PORT_" + fds.UdpPortDestination.ToString()
                                            newFdsChannel.Name = signal.FdsName + "_" + signal.ParameterName + "_VL_" + signal.Vlid.ToString + "_PORT_" + signal.UdpPortDestination.ToString

                                            newFdsChannel.Description = newFdsChannel.Name.Replace("_", " ")
                                            newFdsChannel.ToolTip = newFdsChannel.Name

                                            channelCollection.Add(newFdsChannel)
                                            'CELProject.ChannelList.Add(newFdsChannel.Name, CELProject.GetUnitNameById(signal.EngUnitID))

                                            Select Case fds.TxRxEnum
                                                Case eTransmissionMode.Rx
                                                    If Not signal.HideInGrid Then
                                                        colRxFdsMessages.Add(New CEL.ARINCChannel(newFdsChannel))
                                                    End If

                                                Case eTransmissionMode.Tx
                                                    If Not signal.HideInGrid Then
                                                        colTxFdsMessages.Add(New CEL.ARINCChannel(newFdsChannel))
                                                    End If
                                                Case Else
                                                    Debug.Assert(False)
                                            End Select

                                        Case eDataType.Boolean, eDataType.Integer, eDataType.Float, eDataType.Enumeration


                                            Dim newFdsChannel As New CEL.KineticaRT.Servers.Logger.NumericThresholdLink
                                            newFdsChannel.Description = fds.FdsName
                                            newFdsChannel.ToolTip = fds.FdsName
                                            Select Case fds.TxRxEnum

                                                Case eTransmissionMode.Rx
                                                    newFdsChannel.Destination = ""
                                                    newFdsChannel.Source = opcFdsChannelName

                                                Case eTransmissionMode.Tx
                                                    newFdsChannel.Destination = opcFdsChannelName
                                                    newFdsChannel.Source = opcFdsChannelName

                                            End Select
                                            newFdsChannel.Enabled = DicArchiveChannelList.ContainsKey(fds.FdsName)
                                            If newFdsChannel.Enabled Then
                                                newFdsChannel.Index = DicArchiveChannelList(fds.FdsName) 'Index
                                            Else
                                                newFdsChannel.Index = 0
                                            End If
                                            newFdsChannel.Logger = "ARCHIVE"
                                            newFdsChannel.EventLogger = "EVENT_ARCHIVE"
                                            newFdsChannel.DecimalDigits = CInt(signal.DecimalDigits)
                                            newFdsChannel.Format = signal.Format

                                            newFdsChannel.Units = signal.UpToEngineeringUnitByEngUnitID.DisplayName
                                            'newFdsChannel.Name = signal.FdsName + "_PORT_" + fds.UdpPortDestination.ToString()

                                            newFdsChannel.Minimum = CDbl(signal.MinValue)
                                            newFdsChannel.Maximum = CDbl(signal.MaxValue)

                                            If channelAlreadyAdded = False Then
                                                newFdsChannel.Name = signal.FdsName + "_" + signal.ParameterName + "_VL_" + signal.Vlid.ToString + "_PORT_" + signal.UdpPortDestination.ToString
                                            Else
                                                'This is the RAW channel for Boolean vlues
                                                newFdsChannel.Name = signal.FdsName + "_VL_" + signal.Vlid.ToString + "_PORT_" + signal.UdpPortDestination.ToString
                                                newFdsChannel.Format = "X"
                                                newFdsChannel.Units = "Hex"
                                            End If

                                            'Cas Special pour le FDS_Aircraft1 du PW980A Qui doit être séparé en 2 deux pcq 1 des bit tombe dans un integer de plus
                                            'If (CELProject.ChannelList.ContainsKey(newFdsChannel.Name)) Then
                                            If (channelCollection.ContainsKey(newFdsChannel.Name)) Then
                                                newFdsChannel.Name = newFdsChannel.Name + "_" + signal.SignalAddress.ToString
                                            End If

                                            If signal.DataTypeEnum <> eDataType.Boolean And signal.DataTypeEnum <> eDataType.Enumeration Then
                                                newFdsChannel.Description = signal.FdsName.Replace("_", " ") + "->" + signal.ParameterName.Replace("_", " ")
                                            Else
                                                newFdsChannel.Description = signal.FdsName.Replace("_", " ")
                                            End If
                                            newFdsChannel.ToolTip = newFdsChannel.Name

                                            Try
                                                Dim unitName As String = CELProject.GetUnitNameById(CInt(signal.EngUnitID))
                                                If signal.DataTypeEnum = eDataType.Float Or signal.DataTypeEnum = eDataType.Integer Then
                                                    If unitName <> "None" And unitName <> "" Then
                                                        newFdsChannel.Name += "_" + CELProject.GetUnitNameById(CInt(signal.EngUnitID))
                                                    End If
                                                End If

                                                channelCollection.Add(newFdsChannel)
                                                If unitName <> "None" And unitName <> "" Then
                                                    CELProject.ChannelList.Add(newFdsChannel.Name, CELProject.GetUnitNameById(CInt(signal.EngUnitID)))
                                                End If

                                                Select Case fds.TxRxEnum
                                                    Case eTransmissionMode.Rx
                                                        If Not signal.HideInGrid Then
                                                            colRxFdsMessages.Add(New CEL.ARINCChannel(newFdsChannel))
                                                        End If

                                                    Case eTransmissionMode.Tx
                                                        If Not signal.HideInGrid Then
                                                            colTxFdsMessages.Add(New CEL.ARINCChannel(newFdsChannel))
                                                        End If

                                                    Case Else
                                                        'Debug.Assert(False)
                                                End Select
                                            Catch ex As System.Exception

                                                clsTopMostMessageBox.Show("AFDX.SetupMessages.Add: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                            End Try
                                    End Select


                                Else
                                    'Le channel OPC existe deja alors recupère son nom
                                    opcFdsChannelName = dicAfdxOpcFdsSignalChannelList(keyFdsSignal)
                                    supported = True

                                    SetupDiscrete(signal,
                                                channelCollection, _
                                                fds.FdsName, _
                                                fds.UdpPortDestination.ToString(), _
                                                CELProject.GetChannelCallBack(opcFdsChannelName), _
                                                fds.TxRxEnum)

                                End If


                                If supported Then


                                    Select Case fds.TxRxEnum
                                        Case eTransmissionMode.Rx
                                            opcFdsChannelName = "User_Int_A664IN"
                                            opcItemIdDirection = "in"

                                        Case eTransmissionMode.Tx
                                            opcFdsChannelName = "User_Int_A664OUT"
                                            opcItemIdDirection = "out"
                                        Case Else
                                            Debug.Assert(False)
                                    End Select


                                    If Not dicAfdxOpcFsChannelList.ContainsKey(keyFs) Then

                                        'Creation of FS signal
                                        opcFsChannelName = String.Format("{0}.USINTEGER_{1}", opcFdsChannelName, counter(fds.TxRxEnum, 3))  'Suffixe FS for Functional Status
                                        opcItemId = String.Format("dacn1.dacn1_afdx_iomgr.fs_{0}[{1}].value", opcItemIdDirection, counter(fds.TxRxEnum, 3))
                                        counter(fds.TxRxEnum, 3) = counter(fds.TxRxEnum, 3) + 1
                                        Dim opcFsChannel As Item = CreateChannelNumericOpcItem(opcFsChannelName, _
                                                                                                                          fds.FsName, _
                                                                                                                          fds.FsName, _
                                                                                                                          "Group_Fast", _
                                                                                                                          opcItemId, _
                                                                                                                          "")
                                        opcFsChannel.Format = signal.DisplayFormat
                                        channelCollection.Add(opcFsChannel)
                                        dicAfdxOpcFsChannelList.Add(keyFs, opcFsChannelName)


                                        'Creation of FS signal
                                        Dim newFsChannel As New CEL.KineticaRT.Servers.Logger.NumericThresholdLink
                                        newFsChannel.Description = fds.FsName.Replace("_", " ")
                                        newFsChannel.ToolTip = fds.FsName
                                        Select Case fds.TxRxEnum

                                            Case eTransmissionMode.Rx
                                                newFsChannel.Destination = ""
                                                newFsChannel.Source = opcFsChannelName

                                            Case eTransmissionMode.Tx
                                                newFsChannel.Destination = opcFsChannelName
                                                newFsChannel.Source = ""

                                        End Select
                                        newFsChannel.Enabled = DicArchiveChannelList.ContainsKey(fds.FsName)
                                        If newFsChannel.Enabled Then
                                            newFsChannel.Index = DicArchiveChannelList(fds.FsName) 'Index
                                        Else
                                            newFsChannel.Index = 0
                                        End If
                                        newFsChannel.Logger = "ARCHIVE"
                                        newFsChannel.EventLogger = "EVENT_ARCHIVE"
                                        newFsChannel.DecimalDigits = 0
                                        newFsChannel.Format = "F0"

                                        newFsChannel.Units = ""
                                        newFsChannel.Name = fds.FdsName + "_VL_" + fds.Vlid.ToString + "_PORT_" + fds.UdpPortDestination.ToString + "_FS"

                                        channelCollection.Add(newFsChannel)

                                    Else

                                        opcFsChannelName = dicAfdxOpcFsChannelList(keyFs) 'Retreive info for FS channel

                                    End If





                                    'Select Case message.TxRxEnum
                                    '    Case eTransmissionMode.Rx
                                    '        colRxFdsMessages.Add(New CEL.ARINCChannel(newFsChannel))

                                    '    Case eTransmissionMode.Tx
                                    '        colTxFdsMessages.Add(New CEL.ARINCChannel(newFsChannel))

                                    '    Case Else
                                    '        'Debug.Assert(False)
                                    'End Select

                                End If
                            Next
                        End If
                    Next

                    SetupMessages = True

                Else
                    Debug.Assert(False, "AFDX.SetupMessages: channelCollection is nothing")
                End If

            Catch ex As System.Exception

                clsTopMostMessageBox.Show("AFDX.SetupMessages: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try

        End Function
        Private Function SetupDiscrete(ByVal discrete As Dacn1AfdxSignal, _
                                     ByRef channelCollection As ChannelCollection, _
                                     ByVal fdsName As String, _
                                     ByVal updPort As String, _
                                     ByVal opcChannel As IChannel, _
                                     ByVal arincRXTX As eTransmissionMode) As Boolean

            SetupDiscrete = False

            Try

                If Not IsNothing(channelCollection) Then

                    If IsNumeric(discrete.StartBit) And IsNumeric(discrete.StopBit) And discrete.StartBit <> -1 And discrete.StopBit <> -1 Then


                        Dim bitfieldChannel As IChannel
                        bitfieldChannel = New Channel
                        'NB ici le discrete.ParameterName n'est pas unique, il se retrouve dans plusieurs Fds Differents...
                        If discrete.StartBit = discrete.StopBit Then
                            bitfieldChannel.Name = discrete.FdsName + "_" + discrete.ParameterName + "_VL_" + discrete.Vlid.ToString + "_PORT_" + updPort '+ "_BIT_" + discrete.StartBit.ToString
                        Else
                            bitfieldChannel.Name = discrete.FdsName + "_" + discrete.ParameterName + "_VL_" + discrete.Vlid.ToString + "_PORT_" + updPort  '+ "_BIT_" + discrete.StartBit.ToString + "_TO_" + discrete.StopBit.ToString
                        End If


                        Select Case discrete.UpToDacn1AfdxMessageByCollectionName.TxRxEnum
                            Case eTransmissionMode.Rx

                                Dim valueRXChangedHandler As DiscreteRXValueChangedHandler
                                If colRxFaultDiscreteHandlers.ContainsKey(opcChannel.Name) Then
                                    valueRXChangedHandler = colRxFaultDiscreteHandlers(opcChannel.Name)
                                Else
                                    valueRXChangedHandler = New DiscreteRXValueChangedHandler(opcChannel)
                                    colRxFaultDiscreteHandlers.Add(opcChannel.Name, valueRXChangedHandler)
                                End If



                                If discrete.StartBit = discrete.StopBit Then
                                    'TODO
                                    Select Case discrete.CodingTypeEnum

                                        Case eCodingType.Fault

                                            Dim cmcFaultChannel As CEL.ACMCChannel = Nothing
                                            For Each item As CEL.ACMCChannel In colFaultChannelList 'Retreive channel if exist
                                                If item.ChannelName = opcChannel.Name Then
                                                    cmcFaultChannel = item
                                                End If
                                            Next
                                            If IsNothing(cmcFaultChannel) Then
                                                cmcFaultChannel = New CEL.ACMCChannel(opcChannel)
                                                colFaultChannelList.Add(cmcFaultChannel)
                                            End If
                                            cmcFaultChannel.AddBitField(discrete.StartBit, _
                                               (discrete.StopBit - discrete.StartBit + 1), _
                                               discrete.CodingTypeEnum, _
                                               discrete.ParameterName.Replace("_", " "))

                                        Case eCodingType.Status

                                            Dim cmcStatusChannel As CEL.ACMCChannel = Nothing
                                            For Each item As CEL.ACMCChannel In colStatusChannelList 'Retreive channel if exist
                                                If item.ChannelName = opcChannel.Name Then
                                                    cmcStatusChannel = item
                                                End If
                                            Next
                                            If IsNothing(cmcStatusChannel) Then
                                                cmcStatusChannel = New CEL.ACMCChannel(opcChannel)
                                                colStatusChannelList.Add(cmcStatusChannel)
                                            End If
                                            cmcStatusChannel.AddBitField(discrete.StartBit, _
                                               (discrete.StopBit - discrete.StartBit + 1), _
                                               discrete.CodingTypeEnum, _
                                               discrete.ParameterName.Replace("_", " "))
                                        Case Else
                                            'Do Nothing
                                    End Select
                                End If


                                valueRXChangedHandler.AddBitFieldChannel(discrete.StartBit, _
                                                                         discrete.StopBit, _
                                                                         discrete.CodingTypeEnum, _
                                                                         CDbl(discrete.LSBValue), _
                                                                         discrete.ParameterName, _
                                                                         bitfieldChannel)

                            Case eTransmissionMode.Tx
                                Dim valueTXChangedHandler As DiscreteTXValueChangedHandler
                                If colTxFaultDiscreteHandlers.ContainsKey(opcChannel.Name) Then

                                    valueTXChangedHandler = colTxFaultDiscreteHandlers(opcChannel.Name)

                                Else

                                    valueTXChangedHandler = New DiscreteTXValueChangedHandler(opcChannel)
                                    colTxFaultDiscreteHandlers.Add(opcChannel.Name, valueTXChangedHandler)
                                End If

                                valueTXChangedHandler.AddBitFieldChannel(discrete.StartBit, _
                                                                         discrete.StopBit, _
                                                                         discrete.CodingTypeEnum, _
                                                                         discrete.LSBValue, _
                                                                         discrete.ParameterName, _
                                                                         bitfieldChannel)


                        End Select

                        channelCollection.Add(bitfieldChannel)
                        If Not IsNothing(discrete.EngUnitID) Then
                            CELProject.ChannelList.Add(bitfieldChannel.Name, CELProject.GetUnitNameById(CInt(discrete.EngUnitID)))
                        Else
                            CELProject.ChannelList.Add(bitfieldChannel.Name, CELProject.GetUnitNameById(EngineeringUnit.eEngineeringUnit.Empty))
                        End If

                        SetupDiscrete = True
                    End If
                Else
                    Debug.Assert(False)
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("AFDX.SetupDiscrete: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Function


        Sub CleanUpArincLabels()
            Dim channelsInfo As ChannelsInfo
            Dim curArincChannelName As String = Nothing
            Dim discreteRXHandler As DiscreteRXValueChangedHandler
            Dim discreteTXHandler As DiscreteTXValueChangedHandler

            For Each obj As KeyValuePair(Of String, DiscreteRXValueChangedHandler) In colRxFaultDiscreteHandlers
                discreteRXHandler = obj.Value
                discreteRXHandler.Unregister()
            Next
            For Each obj As KeyValuePair(Of String, DiscreteTXValueChangedHandler) In colTxFaultDiscreteHandlers
                discreteTXHandler = obj.Value
                discreteTXHandler.Unregister()
            Next


            Me.colRxFaultDiscreteHandlers.Clear()
            Me.colTxFaultDiscreteHandlers.Clear()
            Me.colRxFdsMessages.Clear()
            Me.colTxFdsMessages.Clear()
            Me.colFaultChannelList.Clear()
            Me.colStatusChannelList.Clear()


            If Not (CELProject.HMIProject Is Nothing) Then
                channelsInfo = CELProject.HMIProject.GetChannelsInfo(CELProject.EngineModel + "_Arinc_Uart") 'TODO: 
                If Not (channelsInfo Is Nothing) Then
                    CELProject.HMIProject.RemoveChannels(channelsInfo)
                End If
            End If

        End Sub



        '********************************************************************
        '* Handlers *********************************************************
        '********************************************************************


        Public Function Initialize(ByVal collectionName As String, _
                                   ByRef channelCollection As ChannelCollection, _
                                   ByRef dicArchiveLayout As Collections.Generic.Dictionary(Of String, Integer)) As Boolean

            Initialize = False

            Try


                DicArchiveChannelList = dicArchiveLayout
                'ARINC stuff
                SetupMessages(collectionName, channelCollection)

                Initialize = True

            Catch ex As Exception

                clsTopMostMessageBox.Show("AFDX.Initialize: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try

        End Function
        Public Function InitializeGrid(engineModel As String) As Boolean
            'Initialise Gridlists
            Me.InitializeGridLists(engineModel) ' TODO ????

            'Bind the list to the appropriate control
            Me.DataBindToDataGrid()
            Return True
        End Function

        Public Sub Dispose()

            Try

                CleanUpArincLabels()
                dgvReceive = Nothing
                dgvTransmit = Nothing

            Catch ex As Exception
                clsTopMostMessageBox.Show("AFDX : Finalize: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                                                   ByVal code As eCodingType, _
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
                               ByVal code As eCodingType, _
                               ByVal scalingFactor As Double, _
                               ByVal description As String, _
                               ByVal destinationChannel As IChannel)

                    Dim count As Byte
                    Dim baseMask As Long = (1 << (startBit - 1))
                    Me.itsShift = CByte((startBit - 1))
                    Me.itsCodeType = code
                    Me.itsScalingFactor = scalingFactor
                    Me.itsDescription = description
                    Me.itsMask = baseMask
                    Me.itsLength = length

                    If Me.itsCodeType = eCodingType.BNR2 Then 'Binary Number Representation of Two's Complement Number
                        Me.itsBitSignMask = (1 << (length - 1))
                    End If

                    For count = 1 To CByte((length - 1))
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
                    If Me.itsCodeType = eCodingType.BNR2 AndAlso FieldSignIsNegative(fieldValue) Then
                        'BNR Numbers of Two's Complement number (Negative)
                        Return (ExtendNegativeSign(fieldValue) * Me.itsScalingFactor)
                    Else
                        Return (fieldValue * Me.itsScalingFactor)
                    End If

                End Function

                Private itsMask As Long
                Private itsShift As Byte
                Private itsCodeType As eCodingType
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
                                          ByVal code As eCodingType, _
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

                If Not (Me.itsChannel Is Nothing) And Me.itsChannel.Value.ToString() <> "Not Connected" Then
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
                                                   ByVal code As eCodingType, _
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
                               ByVal code As eCodingType, _
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
                Private itsShift As Integer
                Private itsCode As eCodingType
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
                                          ByVal code As eCodingType, _
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

    End Class



End Namespace
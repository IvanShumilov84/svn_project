Imports KineticaRT
Imports KineticaRT.Sequence
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
Imports CEL.Data.BusinessObjects
Imports System.Text.RegularExpressions
Imports System.Collections.Generic

'Imports System.Threading
'Imports System.Diagnostics
'Imports System.Windows.Forms
'Imports System.IO
'Imports System.Text.RegularExpressions

''Imports Excel
'Imports Microsoft.Office.Interop
'Imports System.Data
'Imports System.Data.OleDb
'Imports Microsoft.VisualBasic
'Imports EntitySpaces.Interfaces
'Imports CEL.Data.BusinessObjects

Namespace CEL.HMI

    '<System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Unrestricted:=True)> _
    Public Class SystemCheck

        Private CELProject As CELDAS = CELDAS.Instance()
        Private _HmiCreateChannels As New HMIChannelCreation(CELProject)

        Public Sub New(ByRef RecorderName As String, ByRef EventRecorderName As String, _
                       ByRef BoolInitChannel() As Object, ByRef BoolInitChannelValue() As Value, ByRef BoolInitIndex As Integer, _
                       ByRef ChannelCollection As ChannelCollection)
            _RecorderName = RecorderName
            _EventRecorderName = EventRecorderName
            _BoolInitChannel = BoolInitChannel
            _BoolInitChannelValue = BoolInitChannelValue
            _BoolInitIndex = BoolInitIndex
            _ChannelCollection = ChannelCollection
        End Sub

        Public Function CreateChannelsForSerialBoolPanel() As Boolean
            CreateChannelsForSerialBoolDout()
            CreateChannelsForSerialBoolDinAndLight()
            HideNotUsedButtonLight()
            CreateChannelsForSreialText()
        End Function
        Private Function CreateChannelsForSerialBoolDout() As Boolean

            Dim newChannel As CEL.KineticaRT.Servers.Logger.BooleanLink
            Try
                'Query Database Bool (DOUT)
                Dim dacnBoolChannelCollection As New DacnTemplateBoolCollection()
                dacnBoolChannelCollection.Query.es.QuerySource = "DACN1_Serial_IOMGR_Bool" 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.ExportToCsv = True)
                dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.Name.Like("DOUT%"))
                dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dacnBoolChannelCollection.Query.OrderBy(dacnBoolChannelCollection.Query.RDataID.Ascending)
                dacnBoolChannelCollection.Query.Load()

                _SerialBoolDoutCounter = dacnBoolChannelCollection.Count

                Dim i As Integer = 1
                For Each channel As DacnTemplateBool In dacnBoolChannelCollection

                    Dim hmiBooleanChannel As New HMIBooleanChannels()

                    hmiBooleanChannel.ChannelName = "Button" & i.ToString()
                    hmiBooleanChannel.Source = channel.Name
                    hmiBooleanChannel.Destination = channel.Name
                    hmiBooleanChannel.Description = channel.Name
                    hmiBooleanChannel.FalseText = "False"
                    hmiBooleanChannel.TrueText = "True"
                    hmiBooleanChannel.Tooltip = String.Empty

                    newChannel = _HmiCreateChannels.CreateChannelBooleanLink(hmiBooleanChannel, _BoolInitChannel, _BoolInitChannelValue, _BoolInitIndex)
                    newChannel.Value = Values.False

                    _ChannelCollection.Add(newChannel)

                    i += 1
                Next

            Catch ex As Exception
                Return False
            End Try

            Return True

        End Function
        Private Function CreateChannelsForSerialBoolDinAndLight() As Boolean

            Dim newOpcChannel As Servers.OpcClient.BooleanItem
            Dim newchannel As IBooleanLinkChannel
            Try
                'Query Database Bool (DIN)
                Dim dacnBoolChannelCollection As New DacnTemplateBoolCollection()
                dacnBoolChannelCollection.Query.es.QuerySource = "DACN1_Serial_IOMGR_Bool" 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.ExportToCsv = True)
                dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.Name.Like("DIN%"))
                dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dacnBoolChannelCollection.Query.OrderBy(dacnBoolChannelCollection.Query.RDataID.Ascending)
                dacnBoolChannelCollection.Query.Load()

                _SerialBoolDinCounter = dacnBoolChannelCollection.Count

                Dim i As Integer = 1
                For Each channel As DacnTemplateBool In dacnBoolChannelCollection

                    Dim hmiBooleanChannel As New HMIBooleanChannels()

                    'For DIN channels
                    hmiBooleanChannel.ChannelName = "ButtonDin" & i.ToString()
                    hmiBooleanChannel.Source = "CMD_" & channel.Name
                    hmiBooleanChannel.Destination = "CMD_" & channel.Name
                    hmiBooleanChannel.Description = channel.Name
                    hmiBooleanChannel.FalseText = "False"
                    hmiBooleanChannel.TrueText = "True"
                    hmiBooleanChannel.Tooltip = String.Empty

                    hmiBooleanChannel.OPCGroup = "Group_Fast"
                    hmiBooleanChannel.OPCItemID = "dacn1.dacn1_serial_iomgr.din[" & Regex.Replace(channel.Name, "[^.0-9]", "") & "]"

                    newOpcChannel = _HmiCreateChannels.CreateChannelBooleanOpcItem(hmiBooleanChannel, _BoolInitChannel, _BoolInitChannelValue, _BoolInitIndex)
                    newOpcChannel.Value = Values.False

                    _ChannelCollection.Add(newOpcChannel)

                    'For Light Channels
                    hmiBooleanChannel.ChannelName = "Light" & i.ToString()
                    hmiBooleanChannel.Source = channel.Name
                    hmiBooleanChannel.Destination = String.Empty
                    hmiBooleanChannel.Description = channel.Name

                    hmiBooleanChannel.OPCGroup = String.Empty
                    hmiBooleanChannel.OPCItemID = String.Empty

                    newChannel = _HmiCreateChannels.CreateChannelBooleanLink(hmiBooleanChannel, _BoolInitChannel, _BoolInitChannelValue, _BoolInitIndex)

                    _ChannelCollection.Add(newChannel)

                    i += 1
                Next

            Catch ex As Exception
                Return False
            End Try

            Return True

        End Function
        Private Function CreateChannelsForSreialText() As Boolean
            Dim newOpcChannel As Servers.OpcClient.TextItem

            Try
                Dim hmiTextChannel As New HMITextChannels()

                hmiTextChannel.ChannelName = "DACN1_SERIAL_IOMGR_ERR_MESSAGE"
                hmiTextChannel.Source = String.Empty
                hmiTextChannel.Destination = String.Empty
                hmiTextChannel.Description = "SERIAL DEVICE ERROR"
                hmiTextChannel.Tooltip = String.Empty
                hmiTextChannel.OPCGroup = "Group_Fast"
                hmiTextChannel.OPCItemID = "dacn1.dacn1_user_interface.dacn1_serial_iomgr_err_message"

                newOpcChannel = _HmiCreateChannels.CreateChannelTextOpcItem(hmiTextChannel)
                _ChannelCollection.Add(newOpcChannel)
            Catch ex As Exception

            End Try

        End Function
        Private Function HideNotUsedButtonLight() As Boolean

            Dim fraDigitalOutput As Global.KineticaRT.Controls.Frame = Nothing
            Dim fraDigitalInput As Global.KineticaRT.Controls.Frame = Nothing
            Dim btnDigitalOutput As Studio.Controls.Button = Nothing
            Dim btnDigitalInput As Studio.Controls.Button = Nothing
            Dim lmpDigitalInput As Studio.Controls.LampIndicator = Nothing

            Try
                CELProject.InitializeObjectCallBack(fraDigitalOutput, "SystemCheck_Screen.DAS_Channels_Tab.DOUT_Frame")
                CELProject.InitializeObjectCallBack(fraDigitalInput, "SystemCheck_Screen.DAS_Channels_Tab.DIN_Frame")

                'Hide unused DOUT buttons
                Dim doutButtonIndex As Integer = 1

                For Each control As Object In fraDigitalOutput.Panel.Controls
                    If TypeOf control Is Studio.Controls.Button Then
                        btnDigitalOutput = CType(fraDigitalOutput.FindObject("btn" + doutButtonIndex.ToString()), Studio.Controls.Button)
                        btnDigitalOutput.Visible = doutButtonIndex <= _SerialBoolDoutCounter
                        doutButtonIndex += 1
                    End If
                Next


                'Hide unused DIN buttons and Lights
                Dim dinButtonIndex As Integer = 1
                Dim dinLightIndex As Integer = 1

                For Each control As Object In fraDigitalInput.Panel.Controls
                    If TypeOf control Is Studio.Controls.Button Then
                        btnDigitalInput = CType(fraDigitalInput.FindObject("btnDIN" & dinButtonIndex.ToString()), Studio.Controls.Button)
                        btnDigitalInput.Visible = dinButtonIndex <= _SerialBoolDinCounter
                        dinButtonIndex += 1
                    ElseIf TypeOf control Is Studio.Controls.LampIndicator Then
                        lmpDigitalInput = CType(fraDigitalInput.FindObject("LampIndicator" & dinLightIndex.ToString()), Studio.Controls.LampIndicator)
                        lmpDigitalInput.Visible = dinLightIndex <= _SerialBoolDinCounter
                        dinLightIndex += 1
                    End If
                Next

            Catch ex As Exception
                Return False
            End Try

            Return True

        End Function

        Public Function CreateChannelsForSerialRealPanel() As Boolean
            Try
                CreateChannelsForSerialReal()
                HideNotUsedDisplayForSerialReal()
            Catch ex As Exception
                Return False
            End Try

            Return True

        End Function
        Private Function CreateChannelsForSerialReal() As Boolean

            Dim fraSerialReal As Global.KineticaRT.Controls.Frame = Nothing
            Dim dspSerialReal As Studio.Controls.Display = Nothing

            Try
                CELProject.InitializeObjectCallBack(fraSerialReal, "SystemCheck_Screen.DAS_Channels_Tab.Serial_Real_Frame")

                'Query Database Real
                Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
                dacnRealChannelCollection.Query.es.QuerySource = "DACN1_Serial_IOMGR_Real" 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dacnRealChannelCollection.Query.OrderBy(dacnRealChannelCollection.Query.RdataID.Ascending)
                dacnRealChannelCollection.Query.Load()

                _SerialRealCounter = dacnRealChannelCollection.Count

                Dim newChannel As CEL.KineticaRT.Servers.Logger.NumericThresholdLink
                Dim inputChannelCount As Integer = 1
                Dim outputChannelCount As Integer = 1
                Dim serialDisplayIndex As Integer = 1

                For Each channel As DacnTemplateReal In dacnRealChannelCollection

                    'Create Numeric Link Channels
                    newChannel = New CEL.KineticaRT.Servers.Logger.NumericThresholdLink
                    newChannel.Description = channel.Name
                    newChannel.Source = channel.Name + "_" + CELProject.GetUnitNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.Name
                    newChannel.Enabled = False
                    newChannel.EventLogger = _EventRecorderName
                    newChannel.Index = 1
                    newChannel.Logger = _RecorderName
                    newChannel.DecimalDigits = 3
                    newChannel.Format = "F3"
                    newChannel.Units = channel.EngUnit

                    If CELProject.DebugMode And CELProject.DataAcquisitionMode = CELDAS.eDataAcquisitionMode.SIMULATION Then

                        'Create OPC Client Channels
                        Dim newOpcChannel As New Servers.OpcClient.Item
                        newOpcChannel.DecimalDigits = 3
                        newOpcChannel.Format = "F3"
                        newOpcChannel.Units = channel.EngUnit
                        newOpcChannel.Group = "Group_Fast"

                        Dim channelName As String = channel.Name
                        Dim channelFVType As String = Regex.Replace(channelName, "[^.A-Za-z]", "")
                        Dim channelIndex As String = Regex.Replace(channelName, "[^.0-9]", "")

                        If channel.Name.IndexOf("OUT") > 0 Then
                            newOpcChannel.Description = "SERIAL_Real_Channel_AO" + outputChannelCount.ToString()
                            newOpcChannel.Name = "SERIAL_Real_Channel_AO" + outputChannelCount.ToString()
                            newOpcChannel.ItemID = "dacn1.dacn1_serial_iomgr." + channelFVType.ToLower + "[" & channelIndex & "]"
                        Else

                            newOpcChannel.Description = "SERIAL_Real_Channel_AI" + inputChannelCount.ToString()
                            newOpcChannel.Name = "SERIAL_Real_Channel_AI" + inputChannelCount.ToString()
                            newOpcChannel.ItemID = "dacn1.dacn1_serial_iomgr." + channelFVType.ToLower + "[" & channelIndex & "].mean_value"
                        End If
                        newChannel.Destination = newOpcChannel.Name
                        _ChannelCollection.Add(newOpcChannel)


                    End If

                    'Get handle for display
                    dspSerialReal = CType(fraSerialReal.FindObject("Serial_Real_Display" & serialDisplayIndex.ToString()), Studio.Controls.Display)

                    If channel.Name.IndexOf("OUT") > 0 Then
                        newChannel.Name = "Serial_Real_Channel_Out" + outputChannelCount.ToString()


                        dspSerialReal.Source = "SYSTEM_CHECK.Serial_Real_Channel_Out" + outputChannelCount.ToString()
                        newChannel.Destination = channel.Name + "_" + CELProject.GetUnitNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.Name
                        dspSerialReal.InputEnabled = True

                        outputChannelCount += 1
                    Else
                        newChannel.Name = "Serial_Real_Channel_In" + inputChannelCount.ToString()

                        If CELProject.DebugMode And CELProject.DataAcquisitionMode = CELDAS.eDataAcquisitionMode.SIMULATION Then
                            dspSerialReal.InputEnabled = True
                        Else
                            newChannel.Destination = ""
                            dspSerialReal.InputEnabled = False
                        End If

                        dspSerialReal.Source = "SYSTEM_CHECK.Serial_Real_Channel_In" + inputChannelCount.ToString()
                        dspSerialReal.InputEnabled = True

                        inputChannelCount += 1

                    End If

                    _ChannelCollection.Add(newChannel)

                    serialDisplayIndex += 1
                Next

            Catch ex As Exception
                Return False
            End Try

            Return True

        End Function
        Private Function HideNotUsedDisplayForSerialReal() As Boolean

            Dim fraSerialReal As Global.KineticaRT.Controls.Frame = Nothing
            Dim dspSerialReal As Studio.Controls.Display = Nothing

            Try
                CELProject.InitializeObjectCallBack(fraSerialReal, "SystemCheck_Screen.DAS_Channels_Tab.Serial_Real_Frame")

                Dim serialDisplayIndex As Integer = 1
                For Each control As Object In fraSerialReal.Panel.Controls
                    If TypeOf control Is Studio.Controls.Display Then
                        dspSerialReal = CType(fraSerialReal.FindObject("Serial_Real_Display" & serialDisplayIndex.ToString()), Studio.Controls.Display)
                        dspSerialReal.Visible = serialDisplayIndex <= _SerialRealCounter
                        serialDisplayIndex += 1
                    End If
                Next
            Catch ex As Exception
                Return False
            End Try

            Return True

        End Function

        Public Function CreateChannelsForNidaqBoolPanel() As Boolean
            Try
                CreateChannelsForNidaqBool()
                HideNotUsedDisplayNidaqBool()
            Catch ex As Exception
                Return False
            End Try

            Return True

        End Function
        Private Function CreateChannelsForNidaqBool() As Boolean

            Dim fraNidaqBool As Global.KineticaRT.Controls.Frame = Nothing

            Dim dspNidaqBoolDout As Studio.Controls.Button = Nothing
            Dim dspNidaqBoolDin As Studio.Controls.LampIndicator = Nothing

            Dim newChannel As CEL.KineticaRT.Servers.Logger.BooleanLink
            _NidaqBoolDinCounter = 1
            _NidaqBoolDoutCounter = 1

            Try
                'Query Database Bool
                Dim dacnBoolChannelCollection As DacnTemplateBoolCollection = New DacnTemplateBoolCollection()
                dacnBoolChannelCollection.Query.es.QuerySource = "DACN1_NIDAQ_IOMGR_Bool" 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.ExportToCsv = True)
                dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dacnBoolChannelCollection.Query.OrderBy(dacnBoolChannelCollection.Query.Name.Ascending)
                dacnBoolChannelCollection.Query.Load()

                CELProject.InitializeObjectCallBack(fraNidaqBool, "SystemCheck_Screen.DAS_Channels_Tab.NIDAQ_Bool_Frame")

                For Each channel As DacnTemplateBool In dacnBoolChannelCollection

                    dspNidaqBoolDout = CType(fraNidaqBool.FindObject("NIDAQ_Bool_DO" & _NidaqBoolDoutCounter.ToString()), Studio.Controls.Button)
                    dspNidaqBoolDin = CType(fraNidaqBool.FindObject("NIDAQ_Bool_DI" & _NidaqBoolDinCounter.ToString()), Studio.Controls.LampIndicator)

                    'Create Numeric Link Channels
                    newChannel = New CEL.KineticaRT.Servers.Logger.BooleanLink
                    newChannel.Description = channel.Name
                    newChannel.Enabled = False
                    newChannel.EventLogger = _EventRecorderName
                    newChannel.Index = 1
                    newChannel.Logger = _RecorderName

                    If channel.Name.StartsWith("DOUT") Then
                        newChannel.Destination = channel.Name
                        newChannel.Source = ""
                        newChannel.Name = "NIDAQ_Bool_Channel_DO" + _NidaqBoolDoutCounter.ToString

                        dspNidaqBoolDout.Source = newChannel.Name
                        dspNidaqBoolDout.Visible = True
                        _NidaqBoolDoutCounter = _NidaqBoolDoutCounter + 1
                    Else
                        newChannel.Destination = ""
                        newChannel.Source = channel.Name
                        newChannel.Name = "NIDAQ_Bool_Channel_DI" + _NidaqBoolDinCounter.ToString

                        dspNidaqBoolDin.Source = newChannel.Name
                        dspNidaqBoolDin.Visible = True
                        _NidaqBoolDinCounter = _NidaqBoolDinCounter + 1
                    End If

                    _ChannelCollection.Add(newChannel)
                Next

                _NidaqBoolDinCounter -= 1
                _NidaqBoolDoutCounter -= 1

                'For i = DigitalOutputChannelCount - 1 To btnNidaqBoolDigitalOutputs.GetUpperBound(0) - 1
                '    btnNidaqBoolDigitalOutputs(i).Visible = False
                'Next

                'For i = DigitalInputChannelCount - 1 To lmpNidaqBoolDigitalInputs.GetUpperBound(0) - 1
                '    lmpNidaqBoolDigitalInputs(i).Visible = False
                'Next

                '_ChannelCollection.Disconnect()
                '_ChannelCollection.Connect()

                ''Refresh Explorer  
                'tabDasChannels.BeginInvoke(New MethodInvoker(AddressOf Me.Refresh_Channel_Collection))

            Catch ex As Exception
                Return False
            End Try

            Return True
        End Function
        Private Function HideNotUsedDisplayNidaqBool() As Boolean
            Dim fraNidaqBool As Global.KineticaRT.Controls.Frame = Nothing

            Dim dspNidaqBoolDout As Studio.Controls.Button = Nothing
            Dim dspNidaqBoolDin As Studio.Controls.LampIndicator = Nothing

            Try

                CELProject.InitializeObjectCallBack(fraNidaqBool, "SystemCheck_Screen.DAS_Channels_Tab.NIDAQ_Bool_Frame")
                Dim nidaqBoolDoutIndex As Integer = 1
                Dim nidaqBoolDinIndex As Integer = 1

                For Each control As Object In fraNidaqBool.Panel.Controls
                    If TypeOf control Is Studio.Controls.Button Then 'DOUT
                        dspNidaqBoolDout = CType(fraNidaqBool.FindObject("NIDAQ_Bool_DO" & nidaqBoolDoutIndex.ToString()), Studio.Controls.Button)
                        dspNidaqBoolDout.Visible = nidaqBoolDoutIndex <= _NidaqBoolDoutCounter
                        nidaqBoolDoutIndex += 1
                    ElseIf TypeOf control Is Studio.Controls.LampIndicator Then 'DIN
                        dspNidaqBoolDin = CType(fraNidaqBool.FindObject("NIDAQ_Bool_DI" & nidaqBoolDinIndex.ToString()), Studio.Controls.LampIndicator)
                        dspNidaqBoolDin.Visible = nidaqBoolDinIndex <= _NidaqBoolDinCounter
                        nidaqBoolDinIndex += 1
                    End If
                Next

            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

        Public Function CreateChannelsForNidaqRealPanel() As Boolean
            Try
                CreateChannelsForNidaqReal()
                HideNotUsedDisplayNidaqReal()
                CreateChannelsForNidaqRealDev()
                HideNotUsedDisplayNidaqDev()
            Catch ex As Exception
                Return False
            End Try

            Return True

        End Function
        Private Function CreateChannelsForNidaqReal() As Boolean

            Dim newChannel As CEL.KineticaRT.Servers.Logger.NumericThresholdLink
            Dim newOpcChannel As Servers.OpcClient.Item

            Try
                'Query Database Bool
                Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
                dacnRealChannelCollection.Query.es.QuerySource = "DACN1_NIDAQ_IOMGR_Real" 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dacnRealChannelCollection.Query.OrderBy(dacnRealChannelCollection.Query.RdataID.Ascending)
                dacnRealChannelCollection.Query.Load()

                _NidaqRealCounter = dacnRealChannelCollection.Count

                Dim fraNidaqReal As Global.KineticaRT.Controls.Frame = Nothing
                Dim dspNidaqReal As Studio.Controls.Display = Nothing

                CELProject.InitializeObjectCallBack(fraNidaqReal, "SystemCheck_Screen.DAS_Channels_Tab.NIDAQ_Real_Frame")

                Dim nidaqRealDisplayIndex As Integer = 1

                For Each channel As DacnTemplateReal In dacnRealChannelCollection
                    'Create Numeric Link Channels
                    newChannel = New CEL.KineticaRT.Servers.Logger.NumericThresholdLink
                    newChannel.Name = "NIDAQ_Real_Channel" + nidaqRealDisplayIndex.ToString()
                    newChannel.Description = channel.Name
                    newChannel.Source = channel.Name + "_" + CELProject.GetUnitNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.Name
                    newChannel.Enabled = False
                    newChannel.EventLogger = _EventRecorderName
                    newChannel.Index = 1
                    newChannel.Logger = _RecorderName
                    newChannel.DecimalDigits = 3
                    newChannel.Format = "F3"
                    newChannel.Units = channel.EngUnit

                    dspNidaqReal = CType(fraNidaqReal.FindObject("NIDAQ_Real_Display" & nidaqRealDisplayIndex.ToString()), Studio.Controls.Display)

                    dspNidaqReal.Source = "SYSTEM_CHECK.NIDAQ_Real_Channel" + nidaqRealDisplayIndex.ToString()

                    If channel.Name.StartsWith("VOUT") Then
                        newChannel.Destination = channel.Name + "_" + CELProject.GetUnitNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.Name
                        dspNidaqReal.InputEnabled = True
                    Else
                        newChannel.Destination = ""
                        dspNidaqReal.InputEnabled = False
                    End If

                    If CELProject.DebugMode And CELProject.DataAcquisitionMode = CELDAS.eDataAcquisitionMode.SIMULATION Then
                        'Create OPC Client Channels
                        newOpcChannel = New Servers.OpcClient.Item
                        newOpcChannel.DecimalDigits = 3
                        newOpcChannel.Format = "F3"
                        newOpcChannel.Units = channel.EngUnit
                        newOpcChannel.Group = "Group_Fast"

                        Dim channelName As String = channel.Name
                        Dim channelFVType As String = Regex.Replace(channelName, "[^.A-Za-z]", "")
                        Dim channelIndex As String = Regex.Replace(channelName, "[^.0-9]", "")

                        If channel.Name.StartsWith("VOUT") Then
                            newOpcChannel.Description = "NIDAQ_Real_Channel_AO" + nidaqRealDisplayIndex.ToString()
                            newOpcChannel.Name = "NIDAQ_Real_Channel_AO" + nidaqRealDisplayIndex.ToString()
                            newOpcChannel.ItemID = "dacn1.dacn1_nidaq_iomgr." + channelFVType.ToLower + "[" & channelIndex & "]"
                        Else

                            newOpcChannel.Description = "NIDAQ_Real_Channel_AI" + nidaqRealDisplayIndex.ToString()
                            newOpcChannel.Name = "NIDAQ_Real_Channel_AI" + nidaqRealDisplayIndex.ToString()
                            newOpcChannel.ItemID = "dacn1.dacn1_nidaq_iomgr." + channelFVType.ToLower + "[" & channelIndex & "].mean_value"

                        End If

                        newChannel.Destination = newOpcChannel.Name

                        dspNidaqReal.InputEnabled = True
                        _ChannelCollection.Add(newOpcChannel)
                    End If

                    _ChannelCollection.Add(newChannel)

                    nidaqRealDisplayIndex = nidaqRealDisplayIndex + 1
                Next

            Catch ex As Exception
                Return False
            End Try

            Return True
        End Function
        Private Function CreateChannelsForNidaqRealDev() As Boolean

            Dim newOpcChannel As Servers.OpcClient.Item
            Dim tabNidaqRealDevices As Global.KineticaRT.Controls.Tab

            Try
                CELProject.InitializeObjectCallBack(tabNidaqRealDevices, "SystemCheck_Screen.DAS_Channels_Tab.NIDAQ_Real_NIDAQDevices_Tab")

                For i As Integer = 1 To 2
                    'Query Database Real
                    Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
                    dacnRealChannelCollection.Query.es.QuerySource = "DACN1_NIDAQ_IOMGR_Real" 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                    dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
                    dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.DeviceID = "dev" & i.ToString())
                    dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.Channel >= 0)
                    dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
                    dacnRealChannelCollection.Query.OrderBy(dacnRealChannelCollection.Query.Channel.Ascending)
                    dacnRealChannelCollection.Query.Load()

                    Dim nidaqRealDisplayIndex As Integer = 1
                    Dim nidaqRealInputIndex As Integer = 1
                    Dim dspNidaqRealDisplay As Studio.Controls.Display = Nothing
                    Dim dspNidaqRealInput As Studio.Controls.Display = Nothing

                    For Each channel As DacnTemplateReal In dacnRealChannelCollection

                        newOpcChannel = New Servers.OpcClient.Item
                        newOpcChannel.DecimalDigits = 3
                        newOpcChannel.Format = "F3"
                        newOpcChannel.Units = "Vdc"
                        newOpcChannel.Group = "Group_Fast"


                        If channel.Name.StartsWith("VOUT") Then

                            dspNidaqRealInput = CType(tabNidaqRealDevices.FindObject("NIDAQDevice" & i.ToString() & "_Real_Input" & nidaqRealInputIndex.ToString()), Studio.Controls.Display)

                            newOpcChannel.Description = "AO " & channel.Channel
                            newOpcChannel.Name = "NIDaqDevices" & i.ToString() & "_Vao" & channel.Channel & "_" & channel.Name
                            newOpcChannel.ItemID = "dacn1.dacn1_nidaq_iomgr.nidaqdevices[" & i.ToString() & "].vao[" & channel.Channel & "]"
                            dspNidaqRealInput.Source = "SYSTEM_CHECK.NIDaqDevices" & i.ToString() & "_Vao" & channel.Channel & "_" & channel.Name
                            dspNidaqRealInput.Visible = True
                            dspNidaqRealInput.InputEnabled = True

                            nidaqRealInputIndex += 1

                        Else
                            If nidaqRealDisplayIndex <= 80 Then

                                dspNidaqRealDisplay = CType(tabNidaqRealDevices.FindObject("NIDAQDevice" & i.ToString() & "_Real_Display" + nidaqRealDisplayIndex.ToString()), Studio.Controls.Display)

                                newOpcChannel.Description = "AI " & channel.Channel
                                newOpcChannel.Name = "NIDaqDevices" & i.ToString() & "_Vai" & channel.Channel & "_" & channel.Name
                                newOpcChannel.ItemID = "dacn1.dacn1_nidaq_iomgr.nidaqdevices[" & i.ToString() & "].vai[" & channel.Channel & "]"
                                dspNidaqRealDisplay.Source = "SYSTEM_CHECK.NIDaqDevices" & i.ToString() & "_Vai" & channel.Channel & "_" & channel.Name
                                dspNidaqRealDisplay.Visible = True
                                If CELProject.DebugMode And CELProject.DataAcquisitionMode = CELDAS.eDataAcquisitionMode.SIMULATION Then
                                    dspNidaqRealDisplay.InputEnabled = True
                                Else
                                    dspNidaqRealDisplay.InputEnabled = False
                                End If

                                nidaqRealDisplayIndex += 1

                            Else

                                'Le nombre maximal de channel sur la cart NI 6225 est de 80.
                                'TODO Amelioration, ici le code ne prends pas en considération le nombre de channel virtuel
                                'i.e. que l'on peut avoir 80 channels physique mais avec 100 channel logique
                                'toutes les variables sont des array fix, mais il faudrait transformer ca en variable
                                'et qu'ils soient ajustés en fonction de la base de données.
                                'ReDim Preserve NIDAQDevices_Real_Displays(2, NbrChannels)
                                'temporairement jusqu,a ce qu'on fix ce probleme
                                Exit For

                            End If

                        End If

                        _ChannelCollection.Add(newOpcChannel)

                    Next

                    If i = 1 Then
                        _NidaqRealDev1DisplayCounter = nidaqRealDisplayIndex - 1
                        _NidaqRealDev1InputCounter = nidaqRealInputIndex - 1
                    ElseIf i = 2 Then
                        _NidaqRealDev2DisplayCounter = nidaqRealDisplayIndex - 1
                        _NidaqRealDev2InputCounter = nidaqRealInputIndex - 1
                    End If

                Next

            Catch ex As Exception
                Return False
            End Try

            Return True

        End Function
        Private Function HideNotUsedDisplayNidaqReal() As Boolean

            Dim fraNidaqReal As Global.KineticaRT.Controls.Frame = Nothing
            Dim dspNidaqReal As Studio.Controls.Display = Nothing

            Try

                CELProject.InitializeObjectCallBack(fraNidaqReal, "SystemCheck_Screen.DAS_Channels_Tab.NIDAQ_Real_Frame")
                Dim nidaqRealDisplayIndex As Integer = 1

                For Each control As Object In fraNidaqReal.Panel.Controls
                    If TypeOf control Is Studio.Controls.Display Then
                        dspNidaqReal = CType(fraNidaqReal.FindObject("NIDAQ_Real_Display" & nidaqRealDisplayIndex.ToString()), Studio.Controls.Display)
                        dspNidaqReal.Visible = nidaqRealDisplayIndex <= _NidaqRealCounter
                        nidaqRealDisplayIndex += 1
                    End If
                Next

            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        Private Function HideNotUsedDisplayNidaqDev() As Boolean

            Dim tabNidaqRealDev As Global.KineticaRT.Controls.Tab = Nothing
            Dim dspNidaqRealInput As Studio.Controls.Display = Nothing
            Dim dspNidaqRealDisplay As Studio.Controls.Display = Nothing
            Dim nidaqRealDisplayIndex As Integer
            Dim nidaqRealInputIndex As Integer

            Try

                CELProject.InitializeObjectCallBack(tabNidaqRealDev, "SystemCheck_Screen.DAS_Channels_Tab.NIDAQ_Real_NIDAQDevices_Tab")

                For i As Integer = 1 To 2
                    nidaqRealDisplayIndex = 1
                    nidaqRealInputIndex = 1
                    For Each control As Object In tabNidaqRealDev.Panels(i - 1).Panel.Controls
                        If TypeOf control Is Studio.Controls.Display Then

                            dspNidaqRealInput = CType(tabNidaqRealDev.FindObject("NIDAQDevice" & i.ToString() & "_Real_Input" & nidaqRealInputIndex.ToString()), Studio.Controls.Display)
                            dspNidaqRealDisplay = CType(tabNidaqRealDev.FindObject("NIDAQDevice" & i.ToString() & "_Real_Display" & nidaqRealDisplayIndex.ToString()), Studio.Controls.Display)

                            If i = 1 Then
                                If Not IsNothing(dspNidaqRealInput) Then
                                    dspNidaqRealInput.Visible = nidaqRealInputIndex <= _NidaqRealDev1InputCounter
                                    nidaqRealInputIndex += 1
                                End If
                                If Not IsNothing(dspNidaqRealDisplay) Then
                                    dspNidaqRealDisplay.Visible = nidaqRealDisplayIndex <= _NidaqRealDev1DisplayCounter
                                    nidaqRealDisplayIndex += 1
                                End If
                            ElseIf i = 2 Then
                                If Not IsNothing(dspNidaqRealInput) Then
                                    dspNidaqRealInput.Visible = nidaqRealInputIndex <= _NidaqRealDev2InputCounter
                                    nidaqRealInputIndex += 1
                                End If
                                If Not IsNothing(dspNidaqRealDisplay) Then
                                    dspNidaqRealDisplay.Visible = nidaqRealDisplayIndex <= _NidaqRealDev2DisplayCounter
                                    nidaqRealDisplayIndex += 1
                                End If
                            End If
                        End If
                    Next
                Next

            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        Private Function HideNotUsedDisplayNidaqDev2() As Boolean

            Dim tabNidaqRealDev2 As Global.KineticaRT.Controls.Tab = Nothing
            Dim dspNidaqRealInput As Studio.Controls.Display = Nothing
            Dim dspNidaqRealDisplay As Studio.Controls.Display = Nothing
            Try

                CELProject.InitializeObjectCallBack(tabNidaqRealDev2, "SystemCheck_Screen.DAS_Channels_Tab.NIDAQ_Real_NIDAQDevices_Tab")
                Dim nidaqRealDisplayIndex As Integer = 1
                Dim nidaqRealInputIndex As Integer = 1

                For Each control As Object In tabNidaqRealDev2.Panels(1).Panel.Controls
                    If TypeOf control Is Studio.Controls.Display Then

                        dspNidaqRealInput = CType(tabNidaqRealDev2.FindObject("NIDAQDevice2_Real_Input" & nidaqRealInputIndex.ToString()), Studio.Controls.Display)
                        dspNidaqRealDisplay = CType(tabNidaqRealDev2.FindObject("NIDAQDevice2_Real_Display" & nidaqRealDisplayIndex.ToString()), Studio.Controls.Display)

                        If Not IsNothing(dspNidaqRealInput) Then
                            dspNidaqRealInput.Visible = nidaqRealInputIndex <= _NidaqRealDev2InputCounter
                            nidaqRealInputIndex += 1
                        End If

                        If Not IsNothing(dspNidaqRealDisplay) Then
                            dspNidaqRealDisplay.Visible = nidaqRealDisplayIndex <= _NidaqRealDev2DisplayCounter
                            nidaqRealDisplayIndex += 1
                        End If

                    End If
                Next
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

        Public Function CreateChannelsForPowerDnaPanel() As Boolean
            Try
                CreateChannelsForPowerDna()
                HideNotUsedDisplayPowerDna()
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        Private Function CreateChannelsForPowerDna() As Boolean

            Dim newChannel As CEL.KineticaRT.Servers.Logger.NumericThresholdLink
            Dim channelCount As Integer

            Dim fraPowerDna As Global.KineticaRT.Controls.Frame = Nothing
            Dim dspPowerDna As Studio.Controls.Display = Nothing

            Try

                CELProject.InitializeObjectCallBack(fraPowerDna, "SystemCheck_Screen.DAS_Channels_Tab.PowerDNA_Real_Frame")

                'Query Database Real
                Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
                dacnRealChannelCollection.Query.es.QuerySource = "DACN1_PowerDNA_IOMGR_Real_1" 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dacnRealChannelCollection.Query.OrderBy(dacnRealChannelCollection.Query.RdataID.Ascending)
                dacnRealChannelCollection.Query.Load()

                _PowerDnaCounter = dacnRealChannelCollection.Count

                channelCount = 1

                For Each channel As DacnTemplateReal In dacnRealChannelCollection
                    'Create Numeric Link Channels
                    newChannel = New CEL.KineticaRT.Servers.Logger.NumericThresholdLink
                    newChannel.Description = channel.Name
                    newChannel.Source = channel.Name + "_" + CELProject.GetUnitNameById(CInt(channel.EngUnitID))
                    newChannel.Enabled = False
                    newChannel.EventLogger = _EventRecorderName
                    newChannel.Index = 1
                    newChannel.Logger = _RecorderName
                    newChannel.DecimalDigits = 3
                    newChannel.Format = "F3"
                    newChannel.Units = channel.EngUnit

                    dspPowerDna = CType(fraPowerDna.FindObject("PowerDNA_Real_Display" + channelCount.ToString()), Studio.Controls.Display)
                    'dspPowerDna.Visible = False

                    newChannel.Name = "PowerDNA_Real_Channel" + channelCount.ToString()
                    newChannel.Destination = channel.Name + "_" + CELProject.GetUnitNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.Name

                    dspPowerDna.Source = "SYSTEM_CHECK.PowerDNA_Real_Channel" + channelCount.ToString()
                    dspPowerDna.InputEnabled = False

                    If CELProject.DebugMode And CELProject.DataAcquisitionMode = CELDAS.eDataAcquisitionMode.SIMULATION Then

                        dspPowerDna.InputEnabled = True
                        'Create OPC Client Channels
                        Dim newOpcChannel As New Servers.OpcClient.Item
                        newOpcChannel.DecimalDigits = 3
                        newOpcChannel.Format = "F3"
                        newOpcChannel.Units = channel.EngUnit
                        newOpcChannel.Group = "Group_Fast"

                        Dim channelName As String = channel.Name
                        Dim channelFVType As String = Regex.Replace(channelName, "[^.A-Za-z]", "")
                        Dim channelIndex As String = Regex.Replace(channelName, "[^.0-9]", "")

                        If channel.Name.StartsWith("VOUT") Then
                            newOpcChannel.Description = "POWERDNA_Real_Channel_AO" + channelCount.ToString()
                            newOpcChannel.Name = "POWERDNA_Real_Channel_AO" + channelCount.ToString()
                            newOpcChannel.ItemID = "dacn1.dacn1_powerdna_iomgr." + channelFVType.ToLower + "[" & channelIndex & "]"
                        Else
                            newOpcChannel.Description = "POWERDNA_Real_Channel_AI" + channelCount.ToString()
                            newOpcChannel.Name = "POWERDNA_Real_Channel_AI" + channelCount.ToString()
                            newOpcChannel.ItemID = "dacn1.dacn1_powerdna_iomgr." + channelFVType.ToLower + "[" & channelIndex & "].mean_value"

                        End If
                        newChannel.Destination = newOpcChannel.Name
                        _ChannelCollection.Add(newOpcChannel)

                    End If

                    channelCount = channelCount + 1

                    _ChannelCollection.Add(newChannel)
                Next


            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        Private Function HideNotUsedDisplayPowerDna() As Boolean
            Dim fraPowerDna As Global.KineticaRT.Controls.Frame = Nothing
            Dim dspPowerDna As Studio.Controls.Display = Nothing

            Try
                CELProject.InitializeObjectCallBack(fraPowerDna, "SystemCheck_Screen.DAS_Channels_Tab.PowerDNA_Real_Frame")

                Dim powerDnaDisplayIndex As Integer = 1

                For Each control As Object In fraPowerDna.Panel.Controls
                    If TypeOf control Is Studio.Controls.Display Then
                        dspPowerDna = CType(fraPowerDna.FindObject("PowerDNA_Real_Display" & powerDnaDisplayIndex.ToString()), Studio.Controls.Display)
                        dspPowerDna.Visible = powerDnaDisplayIndex <= _PowerDnaCounter
                        powerDnaDisplayIndex += 1
                    End If
                Next

            Catch ex As Exception
                Return False
            End Try
            Return True

        End Function

        Public Function CreateChannelsForScanivalvePanel() As Boolean
            Try
                CreateChannelsForScanivalve()
                HideNotUsedDisplayScanivalve()
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        Private Function CreateChannelsForScanivalve() As Boolean

            Dim newChannel As CEL.KineticaRT.Servers.Logger.NumericThresholdLink
            Dim channelCount As Integer

            Dim fraScanivalve As Global.KineticaRT.Controls.Frame = Nothing
            Dim dspScanivalve As Studio.Controls.Display = Nothing

            Try

                CELProject.InitializeObjectCallBack(fraScanivalve, "SystemCheck_Screen.DAS_Channels_Tab.Scanivalve_Real_Frame")

                'Query Database Real
                Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
                dacnRealChannelCollection.Query.es.QuerySource = "DACN1_SCANIVALVE_IOMGR_REAL" 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dacnRealChannelCollection.Query.OrderBy(dacnRealChannelCollection.Query.RdataID.Ascending)
                dacnRealChannelCollection.Query.Load()

                _scanivalveCounter = dacnRealChannelCollection.Count

                channelCount = 1

                For Each channel As DacnTemplateReal In dacnRealChannelCollection
                    'Create Numeric Link Channels
                    newChannel = New CEL.KineticaRT.Servers.Logger.NumericThresholdLink
                    newChannel.Description = channel.Name
                    newChannel.Source = channel.Name + "_" + CELProject.GetUnitNameById(CInt(channel.EngUnitID))
                    newChannel.Enabled = False
                    newChannel.EventLogger = _EventRecorderName
                    newChannel.Index = 1
                    newChannel.Logger = _RecorderName
                    newChannel.DecimalDigits = 3
                    newChannel.Format = "F3"
                    newChannel.Units = channel.EngUnit

                    dspScanivalve = CType(fraScanivalve.FindObject("Scanivalve_Real_Display" + channelCount.ToString()), Studio.Controls.Display)
                    'dspScanivalve.Visible = False

                    newChannel.Name = "Scanivalve_Real_Channel" + channelCount.ToString()
                    newChannel.Destination = channel.Name + "_" + CELProject.GetUnitNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.Name

                    dspScanivalve.Source = "SYSTEM_CHECK.Scanivalve_Real_Channel" + channelCount.ToString()
                    dspScanivalve.InputEnabled = False

                    If CELProject.DebugMode And CELProject.DataAcquisitionMode = CELDAS.eDataAcquisitionMode.SIMULATION Then

                        dspScanivalve.InputEnabled = True
                        'Create OPC Client Channels
                        Dim newOpcChannel As New Servers.OpcClient.Item
                        newOpcChannel.DecimalDigits = 3
                        newOpcChannel.Format = "F3"
                        newOpcChannel.Units = channel.EngUnit
                        newOpcChannel.Group = "Group_Fast"

                        Dim channelName As String = channel.Name
                        Dim channelFVType As String = Regex.Replace(channelName, "[^.A-Za-z]", "")
                        Dim channelIndex As String = Regex.Replace(channelName, "[^.0-9]", "")

                        newOpcChannel.Description = "Scanivalve_Real_Channel_AI" + channelCount.ToString()
                        newOpcChannel.Name = "Scanivalve_Real_Channel_AI" + channelCount.ToString()
                        newOpcChannel.ItemID = "dacn1.dacn1_scanivalve_pressure_iomgr." + channelFVType.ToLower + "[" & channelIndex & "].mean_value"

                        newChannel.Destination = newOpcChannel.Name
                        _ChannelCollection.Add(newOpcChannel)

                    End If

                    channelCount = channelCount + 1

                    _ChannelCollection.Add(newChannel)
                Next


            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
        Private Function HideNotUsedDisplayScanivalve() As Boolean
            Dim fraScanivalve As Global.KineticaRT.Controls.Frame = Nothing
            Dim dspScanivalve As Studio.Controls.Display = Nothing

            Try
                CELProject.InitializeObjectCallBack(fraScanivalve, "SystemCheck_Screen.DAS_Channels_Tab.Scanivalve_Real_Frame")

                Dim scanivalveDisplayIndex As Integer = 1

                For Each control As Object In fraScanivalve.Panel.Controls
                    If TypeOf control Is Studio.Controls.Display Then
                        dspScanivalve = CType(fraScanivalve.FindObject("Scanivalve_Real_Display" & scanivalveDisplayIndex.ToString()), Studio.Controls.Display)
                        dspScanivalve.Visible = scanivalveDisplayIndex <= _ScanivalveCounter
                        scanivalveDisplayIndex += 1
                    End If
                Next

            Catch ex As Exception
                Return False
            End Try
            Return True

        End Function


        Private dicEngineMappedFVNameList As New Dictionary(Of String, String)  'For DebugMode list all FVName that an engine is using...  

        Private _RecorderName As String
        Private _EventRecorderName As String
        Private _BoolInitChannel() As Object
        Private _BoolInitChannelValue() As Value
        Private _BoolInitIndex As Integer
        Private _ChannelCollection As ChannelCollection

        Private _SerialBoolDoutCounter As Integer 'Number of DOUT entries in the DACN1_Serial_IOMGR_Bool table
        Private _SerialBoolDinCounter As Integer 'Number DIN entries in the DACN1_Serial_IOMGR_Bool table
        Private _SerialRealCounter As Integer
        Private _PowerDnaCounter As Integer 'Number of entries in the DACN1_PowerDNA_IOMGR_Real_1 table
        Private _scanivalveCounter As Integer 'Number of entries in the DACN1_PowerDNA_IOMGR_Real_1 table
        Private _NidaqBoolDinCounter As Integer
        Private _NidaqBoolDoutCounter As Integer
        Private _NidaqRealCounter As Integer
        Private _NidaqRealDev1DisplayCounter As Integer
        Private _NidaqRealDev1InputCounter As Integer
        Private _NidaqRealDev2DisplayCounter As Integer
        Private _NidaqRealDev2InputCounter As Integer

    End Class
End Namespace


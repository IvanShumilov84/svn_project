Imports EntitySpaces.DynamicQuery
Imports CEL.Data.BusinessObjects
Imports System
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports System.Diagnostics
Imports CEL.HMI
Imports System.Collections.Generic
Imports CEL.Library
Imports System.Drawing

Public Class HMIChannelCreation

    Private CELProject As CELDAS

    Public Sub New(ByRef Project As CELDAS)
        CELProject = Project
    End Sub


#Region " HMI Boolean Channels "

    Public Enum eBooleanChannelType
        BooleanChannel
        BooleanExpression
        BooleanOpcItem
        BooleanLink
        BooleanLatch
    End Enum

    Public Function GetTypeOfChannel(ByRef channel As HMIBooleanChannels) As eBooleanChannelType

        Select Case True
            Case channel.Expression <> ""
                Return eBooleanChannelType.BooleanExpression

            Case (channel.OPCGroup <> "" AndAlso _
                  channel.OPCItemID <> "")
                Return eBooleanChannelType.BooleanOpcItem

            Case (channel.Source <> "" AndAlso _
                  channel.Destination <> "" AndAlso _
                  channel.Source <> channel.Destination) _
                OrElse _
                 (channel.Source = "" AndAlso _
                  channel.Destination <> "")
                Return eBooleanChannelType.BooleanLink

            Case channel.ResetLatch <> "" AndAlso _
                 channel.Destination = ""
                Return eBooleanChannelType.BooleanLatch

            Case (channel.Source <> "" AndAlso _
                  channel.Destination = "" _
                  OrElse (channel.Source <> "" AndAlso channel.Source = channel.Destination)) _
                  AndAlso channel.Source <> "" AndAlso _
                  channel.Expression = "" AndAlso _
                  channel.OPCGroup = ""
                Return eBooleanChannelType.BooleanChannel

            Case Else

                If channel.Source = "" AndAlso _
                    channel.Destination = "" AndAlso _
                    channel.ResetLatch = "" AndAlso _
                    channel.Expression = "" AndAlso _
                    channel.OPCGroup = "" AndAlso _
                    channel.OPCItemID = "" Then

                    Return eBooleanChannelType.BooleanChannel  'Channel Flottant dynamique utilisé comme variable temporaire
                Else
                    Debug.Assert(False) 'Ne devrait pas se rendre ici
                End If

        End Select

        Return Nothing

    End Function

    Public Sub CreateChannelsEngineBoolean(ByVal collectionName As String, _
                                            ByRef EngineChannelCollection As KineticaRT.ChannelCollection, _
                                            ByRef EngineMappingList As Dictionary(Of String, String), _
                                            ByRef BoolInitChannel() As Object, _
                                            ByRef BoolInitChannelValue() As KineticaRT.Values.Value, _
                                            ByRef BoolInitIndex As Integer)

        'Query Database
        'esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
        Dim booleanChannelsCollection As HMIBooleanChannelsCollection = New HMIBooleanChannelsCollection()
        booleanChannelsCollection.es.Connection.Name = CELProject.DefaultConnectionName
        booleanChannelsCollection.Query.Where(booleanChannelsCollection.Query.CollectionName = collectionName)
        booleanChannelsCollection.Query.Where(booleanChannelsCollection.Query.Unavailable = False)
        booleanChannelsCollection.Query.Load()

        For Each channel As HMIBooleanChannels In booleanChannelsCollection

            Dim newChannel As Object = Nothing
            Dim tooltipIsEmpty As Boolean = channel.Tooltip & "" = ""

            If Not IsNothing(EngineMappingList) Then
                'Substitution du channelMappe pour la source et la destination seulement
                If channel.Source <> "" AndAlso EngineMappingList.ContainsKey(channel.Source) Then
                    channel.Source = EngineMappingList(channel.Source) 'Une indirection supplementaire pour ne pas utiliser les FV_NAME directement dans les tables HMI_NUMERIC
                    'Affectation automatique du TOOLTIP avec les INPUTS PXX TXX VINXXX 
                    If tooltipIsEmpty Then
                        channel.Tooltip += channel.Source
                    End If

                End If
                If channel.Destination <> "" AndAlso EngineMappingList.ContainsKey(channel.Destination) Then
                    channel.Destination = EngineMappingList(channel.Destination) 'Une indirection supplementaire pour ne pas utiliser les FV_NAME directement dans les tables HMI_NUMERIC

                    'Affectation automatique du TOOLTIP avec les INPUTS PXX TXX VINXXX 
                    If tooltipIsEmpty Then
                        If channel.Source <> "" And channel.Source <> channel.Destination Then
                            channel.Tooltip &= IIf(channel.Source <> "" And channel.Tooltip <> "", "/" + channel.Destination, channel.Destination)
                        End If
                    End If

                End If
            End If

            'il n'y a pas de DOUT, IOUT,VOUT sur le slave donc la destination va generer une erreur dans la fenetre Kineticart Studio KSp. On ajoute donc ce filtre pour eleiminer les message de nuissance.
            If CELProject.HierarchyType = CELDAS.eHierarchyType.Slave AndAlso channel.Destination <> "" AndAlso (channel.Destination.ToUpper.Contains("DOUT") Or channel.Destination.ToUpper.Contains("IOUT") Or channel.Destination.ToUpper.Contains("VOUT") Or channel.Destination.ToUpper.Contains("A429OUT")) Then
                channel.Destination = "" 'TODO fix temporaire pour eviter des prob de comm entre un slave et un master
            End If

            ValidateChannel(channel)

            Select Case GetTypeOfChannel(channel)

                Case eBooleanChannelType.BooleanChannel
                    newChannel = CreateChannelBoolean(channel, BoolInitChannel, BoolInitChannelValue, BoolInitIndex)

                Case eBooleanChannelType.BooleanLink
                    newChannel = CreateChannelBooleanLink(channel, BoolInitChannel, BoolInitChannelValue, BoolInitIndex)

                Case eBooleanChannelType.BooleanExpression
                    newChannel = CreateChannelBooleanExpression(channel)

                Case eBooleanChannelType.BooleanLatch
                    newChannel = CreateChannelBooleanLatch(channel, BoolInitChannel, BoolInitChannelValue, BoolInitIndex)

                Case eBooleanChannelType.BooleanOpcItem
                    newChannel = CreateChannelBooleanOpcItem(channel, BoolInitChannel, BoolInitChannelValue, BoolInitIndex)

                Case Else
                    Debug.Assert(False)

            End Select

            EngineChannelCollection.Add(newChannel)

        Next

    End Sub

    Public Function CreateChannelBoolean(ByRef channel As HMIBooleanChannels, _
                                          ByRef BoolInitChannel() As Object, _
                                          ByRef BoolInitChannelValue() As KineticaRT.Values.Value, _
                                          ByRef BoolInitIndex As Integer) _
                                      As CEL.KineticaRT.Servers.Logger.BooleanLink

        Dim newChannel As New CEL.KineticaRT.Servers.Logger.BooleanLink

        newChannel.FalseText = channel.FalseText
        newChannel.TrueText = channel.TrueText
        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.Source = channel.Source
        newChannel.Destination = channel.Destination
        newChannel.Enabled = CELProject.DicArchiveChannelList.ContainsKey(channel.ChannelName)

        If newChannel.Enabled Then
            newChannel.Index = CELProject.DicArchiveChannelList(channel.ChannelName) 'Index
        Else
            newChannel.Index = 0
        End If
        newChannel.Logger = "ARCHIVE"
        newChannel.EventLogger = "EVENT_ARCHIVE"

        newChannel.Name = channel.ChannelName

        'If Not channel.InitialValue Is DBNull.Value Then
        If channel.InitialValue <> eBooleanState.NoState Then
            If channel.InitialValue = eBooleanState.True Then 'True
                BoolInitChannel(BoolInitIndex) = newChannel
                BoolInitChannelValue(BoolInitIndex) = KineticaRT.Values.Values.True
                BoolInitIndex = BoolInitIndex + 1
            ElseIf channel.InitialValue = eBooleanState.False Then 'False
                BoolInitChannel(BoolInitIndex) = newChannel
                BoolInitChannelValue(BoolInitIndex) = KineticaRT.Values.Values.False
                BoolInitIndex = BoolInitIndex + 1
            End If
        End If

        Return newChannel

    End Function

    Public Function CreateChannelBooleanLink(ByRef channel As HMIBooleanChannels, _
                                              ByRef BoolInitChannel() As Object, _
                                              ByRef BoolInitChannelValue() As KineticaRT.Values.Value, _
                                              ByRef BoolInitIndex As Integer) _
                                          As CEL.KineticaRT.Servers.Logger.BooleanLink

        Dim newChannel As New CEL.KineticaRT.Servers.Logger.BooleanLink

        newChannel.FalseText = channel.FalseText
        newChannel.TrueText = channel.TrueText
        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.Destination = channel.Destination
        newChannel.Source = channel.Source
        newChannel.Enabled = CELProject.DicArchiveChannelList.ContainsKey(channel.ChannelName)

        If newChannel.Enabled Then
            newChannel.Index = CELProject.DicArchiveChannelList(channel.ChannelName) 'Index
        Else
            newChannel.Index = 0
        End If
        newChannel.Logger = "ARCHIVE"
        newChannel.EventLogger = "EVENT_ARCHIVE"

        newChannel.Name = channel.ChannelName

        'If Not channel.InitialValue Is DBNull.Value Then
        If channel.InitialValue <> eBooleanState.NoState Then
            If channel.InitialValue = eBooleanState.True Then 'True
                BoolInitChannel(BoolInitIndex) = newChannel
                BoolInitChannelValue(BoolInitIndex) = KineticaRT.Values.Values.True
                BoolInitIndex = BoolInitIndex + 1
            ElseIf channel.InitialValue = eBooleanState.False Then 'False
                BoolInitChannel(BoolInitIndex) = newChannel
                BoolInitChannelValue(BoolInitIndex) = KineticaRT.Values.Values.False
                BoolInitIndex = BoolInitIndex + 1
            End If
        End If

        Return newChannel

    End Function

    Public Function CreateChannelBooleanExpression(ByRef channel As HMIBooleanChannels) As CEL.KineticaRT.Servers.Logger.BooleanExpression

        Dim newChannel As New CEL.KineticaRT.Servers.Logger.BooleanExpression

        newChannel.FalseText = channel.FalseText
        newChannel.TrueText = channel.TrueText
        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.Enabled = CELProject.DicArchiveChannelList.ContainsKey(channel.ChannelName)

        If newChannel.Enabled Then
            newChannel.Index = CELProject.DicArchiveChannelList(channel.ChannelName) 'Index
        Else
            newChannel.Index = 0
        End If
        newChannel.Logger = "ARCHIVE"
        newChannel.EventLogger = "EVENT_ARCHIVE"

        newChannel.Text = channel.Expression
        newChannel.Name = channel.ChannelName

        Return newChannel

    End Function

    Public Function CreateChannelBooleanLatch(ByRef channel As HMIBooleanChannels, _
                                               ByRef BoolInitChannel() As Object, _
                                               ByRef BoolInitChannelValue() As KineticaRT.Values.Value, _
                                               ByRef BoolInitIndex As Integer) _
                                           As CEL.KineticaRT.Servers.Logger.BooleanLatch

        Dim newChannel As New CEL.KineticaRT.Servers.Logger.BooleanLatch

        newChannel.FalseText = channel.FalseText
        newChannel.TrueText = channel.TrueText
        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.Reset = channel.ResetLatch
        newChannel.Source = channel.Source
        newChannel.Enabled = CELProject.DicArchiveChannelList.ContainsKey(channel.ChannelName)

        If newChannel.Enabled Then
            newChannel.Index = CELProject.DicArchiveChannelList(channel.ChannelName) 'Index
        Else
            newChannel.Index = 0
        End If
        newChannel.Logger = "ARCHIVE"
        newChannel.EventLogger = "EVENT_ARCHIVE"

        newChannel.Name = channel.ChannelName

        'If Not channel.InitialValue Is DBNull.Value Then
        If channel.InitialValue <> eBooleanState.NoState Then
            If channel.InitialValue = eBooleanState.True Then
                BoolInitChannel(BoolInitIndex) = newChannel
                BoolInitChannelValue(BoolInitIndex) = KineticaRT.Values.Values.True
                BoolInitIndex = BoolInitIndex + 1
            ElseIf channel.InitialValue = eBooleanState.False Then
                BoolInitChannel(BoolInitIndex) = newChannel
                BoolInitChannelValue(BoolInitIndex) = KineticaRT.Values.Values.False
                BoolInitIndex = BoolInitIndex + 1
            End If
        End If

        Return newChannel

    End Function

    Public Function CreateChannelBooleanOpcItem(ByRef channel As HMIBooleanChannels, _
                                                 ByRef BoolInitChannel() As Object, _
                                                 ByRef BoolInitChannelValue() As KineticaRT.Values.Value, _
                                                 ByRef BoolInitIndex As Integer) _
                                             As KineticaRT.Servers.OpcClient.BooleanItem

        Dim newChannel As New KineticaRT.Servers.OpcClient.BooleanItem

        newChannel.FalseText = channel.FalseText
        newChannel.TrueText = channel.TrueText
        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.ItemID = channel.OPCItemID
        newChannel.Group = channel.OPCGroup
        newChannel.Name = channel.ChannelName

        'If Not channel.InitialValue Is DBNull.Value Then
        If channel.InitialValue <> eBooleanState.NoState Then
            If channel.InitialValue = eBooleanState.True Then 'True
                BoolInitChannel(BoolInitIndex) = newChannel
                BoolInitChannelValue(BoolInitIndex) = KineticaRT.Values.Values.True
                BoolInitIndex = BoolInitIndex + 1
            ElseIf channel.InitialValue = eBooleanState.False Then 'False
                BoolInitChannel(BoolInitIndex) = newChannel
                BoolInitChannelValue(BoolInitIndex) = KineticaRT.Values.Values.False
                BoolInitIndex = BoolInitIndex + 1
            End If
        End If

        Return newChannel

    End Function

    Public Function CreateChannelBooleanOpcItem(ByRef channel As DacnTemplateBool) As KineticaRT.Servers.OpcClient.BooleanItem

        Dim newChannel As New KineticaRT.Servers.OpcClient.BooleanItem

        newChannel.Description = channel.Name
        newChannel.Name = channel.Name
        newChannel.Group = channel.OPCGroup
        newChannel.ItemID = channel.OPCItemID

        Return newChannel

    End Function

#End Region 'HMI Boolean Channels

#Region " HMI Numeric Channels "

    Public Enum eNumericChannelType
        NumericExpression = 1
        NumericOpcItem = 2
        NumericLink = 3
        NumericChannel = 4
    End Enum

    Public Function GetTypeOfChannel(ByRef channel As HMINumericChannels) As eNumericChannelType

        Select Case True
            Case (channel.Expression <> "")
                Return eNumericChannelType.NumericExpression

            Case (channel.OPCGroup <> "" AndAlso channel.OPCItemID <> "")
                Return eNumericChannelType.NumericOpcItem

            Case channel.Source <> "" AndAlso _
                 channel.Destination <> "" AndAlso _
                 channel.Source <> channel.Destination OrElse _
                 channel.Source = "" AndAlso _
                 channel.Destination <> ""
                Return eNumericChannelType.NumericLink

            Case (channel.Source <> "" AndAlso _
                 channel.Destination = "" OrElse _
                 (channel.Source <> "" AndAlso channel.Destination <> "" AndAlso channel.Source = channel.Destination)) AndAlso _
                 channel.Source <> "" AndAlso _
                 channel.Expression = "" AndAlso _
                 channel.OPCGroup = ""
                Return eNumericChannelType.NumericChannel

            Case Else
                If channel.Source = "" AndAlso _
                   channel.Destination = "" AndAlso
                   channel.Expression = "" AndAlso _
                   channel.OPCGroup = "" AndAlso _
                   channel.OPCItemID = "" Then

                    Return eNumericChannelType.NumericLink  'Channel Flottant dynamique utilisé comme variable temporaire
                Else
                    Debug.Assert(False) 'Ne devrait pas se rendre ici
                End If

        End Select

        Return Nothing

    End Function

    Public Sub CreateChannelsEngineNumeric(ByVal collectionName As String, _
                                            ByRef engineChannelCollection As KineticaRT.ChannelCollection, _
                                            ByRef numericChannelCollection As KineticaRT.ChannelCollection, _
                                            ByRef numericCollectionName As String, _
                                            ByRef EngineMappingList As Dictionary(Of String, String), _
                                            ByRef InputOutputRegEx As String, _
                                            ByRef RegexOptions As System.Text.RegularExpressions.RegexOptions)

        'Query Database
        Dim numericChannelsCollection As HMINumericChannelsCollection = New HMINumericChannelsCollection()
        numericChannelsCollection.es.Connection.Name = CELProject.DefaultConnectionName
        numericChannelsCollection.Query.Where(numericChannelsCollection.Query.CollectionName = collectionName)
        numericChannelsCollection.Query.Where(numericChannelsCollection.Query.Unavailable = False)
        numericChannelsCollection.Query.Load()

        For Each channel As HMINumericChannels In numericChannelsCollection

            Dim tooltipIsEmpty As Boolean = channel.Tooltip & "" = ""

            If Not IsNothing(EngineMappingList) Then
                'Substitution du channelMappe pour la source et la destination seulement
                If channel.Source <> "" AndAlso EngineMappingList.ContainsKey(channel.Source) Then
                    channel.Source = EngineMappingList(channel.Source) 'Une indirection supplementaire pour ne pas utiliser les FV_NAME directement dans les tables HMI_NUMERIC
                    'Affectation automatique du TOOLTIP avec les INPUTS PXX TXX VINXXX 
                    If tooltipIsEmpty Then
                        channel.Tooltip += " " + channel.Source
                    End If
                End If
                If channel.Destination <> "" AndAlso EngineMappingList.ContainsKey(channel.Destination) Then
                    channel.Destination = EngineMappingList(channel.Destination) 'Une indirection supplementaire pour ne pas utiliser les FV_NAME directement dans les tables HMI_NUMERIC

                    'Affectation automatique du TOOLTIP avec les INPUTS PXX TXX VINXXX 
                    If tooltipIsEmpty Then
                        If channel.Source & "" <> "" And channel.Source <> channel.Destination Then
                            channel.Tooltip &= IIf(channel.Source & "" <> "" And channel.Tooltip & "" <> "", "/" + channel.Destination, channel.Destination)
                        End If
                    End If

                End If
            End If

            If CELProject.HierarchyType = CELDAS.eHierarchyType.Slave AndAlso channel.Destination <> "" AndAlso (channel.Destination.ToUpper.Contains("DOUT") Or channel.Destination.ToUpper.Contains("IOUT") Or channel.Destination.ToUpper.Contains("VOUT") Or channel.Destination.ToUpper.Contains("A429OUT")) Then
                channel.Destination = "" 'TODO fix temporaire pour eviter des prob de comm entre un slave et un master
            End If

            ValidateChannel(channel)

            Dim newChannel As Object = Nothing
            Dim channelType As HMIChannelCreation.eNumericChannelType = GetTypeOfChannel(channel)

            Select Case channelType

                Case HMIChannelCreation.eNumericChannelType.NumericChannel
                    newChannel = CreateChannelNumericThreshold(channel)

                Case HMIChannelCreation.eNumericChannelType.NumericLink
                    newChannel = CreateChannelNumericThresholdLink(channel)

                Case HMIChannelCreation.eNumericChannelType.NumericExpression
                    newChannel = CreateChannelNumericThresholdExpression(channel)

                Case HMIChannelCreation.eNumericChannelType.NumericOpcItem
                    newChannel = CreateChannelNumericThresholdOpcItem(channel)

                Case Else
                    Debug.Assert(False)

            End Select

            If channelType = HMIChannelCreation.eNumericChannelType.NumericChannel OrElse _
                channelType = HMIChannelCreation.eNumericChannelType.NumericLink Then

                Dim channelToTreat As String = ""

                Dim unitName As String = If(IsNothing(channel.EngUnitID), "", CELProject.GetUnitNameById(CInt(channel.EngUnitID)))

                If unitName <> "None" And unitName <> "" Then

                    For i As Integer = 0 To 1 'First treat source, then second time tread destination

                        If i = 0 Then
                            channelToTreat = channel.Source
                        Else
                            If channelType <> HMIChannelCreation.eNumericChannelType.NumericChannel Then 'La propriété Destination n'existe pas sur les channel CEL.KineticaRT.Servers.Logger.NumericThresholdChannel
                                channelToTreat = channel.Destination
                            End If
                        End If

                        If channelToTreat = "" Then
                            Continue For
                        End If

                        Dim resolved As Boolean = False

                        If CELProject.ChannelList.ContainsKey(channelToTreat) Then

                            If CELProject.ChannelList(channelToTreat) = unitName OrElse (CELProject.ChannelList.ContainsKey(channelToTreat + "_" + unitName) AndAlso _
                                CELProject.ChannelList(channelToTreat + "_" + unitName) = unitName) Then '  'Check si l'Input ou Output Original existe Or Check si un derive de l input ou Output original a ete genere  ex.:  VIN5_PCT a partir d'un VIN5 (VDC)
                                If i = 0 Then
                                    If Not channelToTreat.EndsWith(unitName) Then
                                        newChannel.Source = channelToTreat + "_" + unitName
                                    Else
                                        newChannel.Source = channelToTreat
                                    End If
                                Else
                                    If channelType <> HMIChannelCreation.eNumericChannelType.NumericChannel Then 'La propriété Destination n'existe pas sur les channel CEL.KineticaRT.Servers.Logger.NumericThresholdChannel
                                        If Not channelToTreat.EndsWith(unitName) Then
                                            newChannel.Destination = channelToTreat + "_" + unitName
                                        Else
                                            newChannel.Destination = channelToTreat
                                        End If
                                    End If
                                End If
                                resolved = True
                            Else

                                'Ici la channelToTreat (Source uniquement) existe mais n'est pas dans la même unité que le channel en cours
                                'Alors il est possible de generer un nouveau channel de type expression
                                Dim fromUnit As String = CELProject.ChannelList(channelToTreat) 'CELProject.GetUnitNameByDisplayName(CELProject.ChannelList(channelToTreat))
                                Dim toUnit As String = unitName

                                If fromUnit <> "None" And toUnit <> "None" And fromUnit <> "" And fromUnit <> toUnit Then
                                    If i = 0 Then 'Pour la source seulement on peut generer automatiquement les unites
                                        Dim channelCollectionNumeric As KineticaRT.ChannelCollection
                                        channelCollectionNumeric = CELProject.GetObjectCallBack(numericCollectionName)

                                        Dim generatedChannel As KineticaRT.Logic.Channels.NumericExpression
                                        generatedChannel = CreateChannelNumericExpressionConversion(channel) 'Attention cette fonction utilise uniquement la source

                                        Try
                                            CELProject.ChannelList.Add(generatedChannel.Name, unitName)
                                        Catch ex As Exception
                                            Dim msg As String = "OpenTest_Sequence.CreateChannelsEngineNumeric: The generated channel " + generatedChannel.Name.ToString + " already exist in the ChannelList "
                                            If CELProject.NoPopupError Then
                                                Console.WriteLine(msg)
                                            Else
                                                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            End If
                                        End Try

                                        Try
                                            channelCollectionNumeric.Add(generatedChannel)
                                        Catch ex As Exception
                                            Dim msg As String = "OpenTest_Sequence.CreateChannelsEngineNumeric: The generated channel " + generatedChannel.Name.ToString + " already exist in the NUMERIC collection "
                                            If CELProject.NoPopupError Then
                                                Console.WriteLine(msg)
                                            Else
                                                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            End If
                                        End Try


                                        newChannel.Source = generatedChannel.Name 'Mise a jour du input ex.  P2 devient alors P2_PSIG
                                        resolved = True
                                    Else ' Pour la destination on ne supporte pas le changement d'unites.  Le changement d'unite est base sur une expression, qui est incompatible avec un channel Destination de type OPC
                                        If channelType <> HMIChannelCreation.eNumericChannelType.NumericChannel Then 'La propriété Destination n'existe pas sur les channel CEL.KineticaRT.Servers.Logger.NumericThresholdChannel
                                            Dim msg As String = "The Destination " + channelToTreat + "  is invalid.  Unit conversion not possible for channel " + channel.ChannelName
                                            If CELProject.NoPopupError Then
                                                Console.WriteLine(msg)
                                            Else
                                                clsTopMostMessageBox.Show(msg, "Unit conversion not supported", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        If Not resolved Then

                            Dim trySubstitution As Boolean = False

                            If Regex.IsMatch(channelToTreat, InputOutputRegEx, RegexOptions) Then
                                Dim result As String = Regex.Match(channelToTreat, InputOutputRegEx, RegexOptions).Value
                                If result = channelToTreat Then

                                    If i = 0 Then 'Pour la source seulement on peut generer automatiquement les unites
                                        Dim channelCollectionNumeric As KineticaRT.ChannelCollection
                                        channelCollectionNumeric = CELProject.GetObjectCallBack(numericCollectionName)

                                        Dim generatedChannel As KineticaRT.Logic.Channels.NumericExpression
                                        generatedChannel = CreateChannelNumericExpressionConversion(channel) 'Attention cette fonction utilise uniquement la source

                                        Try
                                            CELProject.ChannelList.Add(generatedChannel.Name, unitName)
                                        Catch ex As Exception
                                            Dim msg As String = "OpenTest_Sequence.CreateChannelsEngineNumeric: The generated channel " + generatedChannel.Name.ToString + " aready exist in the ChannelList "
                                            If CELProject.NoPopupError Then
                                                Console.WriteLine(msg)
                                            Else
                                                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            End If

                                        End Try

                                        Try
                                            channelCollectionNumeric.Add(generatedChannel)
                                        Catch ex As Exception
                                            Dim msg As String = "OpenTest_Sequence.CreateChannelsEngineNumeric: The generated channel " + generatedChannel.Name.ToString + " aready exist in the NUMERIC collection "
                                            If CELProject.NoPopupError Then
                                                Console.WriteLine(msg)
                                            Else
                                                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            End If
                                        End Try


                                        newChannel.Source = generatedChannel.Name 'Mise a jour du input ex.  P2 devient alors P2_PSIG
                                    Else ' Pour la destination on ne supporte pas le changement d'unites.  Le changement d'unite est base sur une expression, qui est incompatible avec un channel Destination de type OPC
                                        Dim msg As String = "The Destination " + channelToTreat + "  is invalid.  Unit conversion not possible for channel " + channel.ChannelName
                                        If CELProject.NoPopupError Then
                                            Console.WriteLine(msg)
                                        Else
                                            clsTopMostMessageBox.Show(msg, "Unit conversion not supported", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        End If
                                    End If
                                Else
                                    trySubstitution = True

                                End If
                            Else
                                trySubstitution = True
                            End If



                            'If Regex.IsMatch(channelToTreat, InputOutputRegEx, Me.GetRegexOptions()) Then
                            '    Dim result As String = Regex.Match(channelToTreat, InputOutputRegEx, Me.GetRegexOptions()).Value
                            '    If result = channelToTreat Then
                            '        If i = 0 Then
                            '           clsTopMostMessageBox.Show("The Source " + channelToTreat + "  is invalid or not found for channel " + channel.ChannelName, "Input signal not found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                            '        Else
                            '           clsTopMostMessageBox.Show("The Destination " + channelToTreat + "  is invalid or not found for channel " + channel.ChannelName, "Output signal not found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                            '        End If
                            '    Else
                            '        trySubstitution = True
                            '    End If
                            'Else
                            '    trySubstitution = True
                            'End If



                            'Substitution de la source pour une expression qui converti la source dans l'unite desire du channel en cours
                            'ex.:  le channel P_ORIFICE_PSIA dont la SOURCE est P_ORIFICE_PSIG deviendra une EXPRESSION =  P_ORIFICE_PSIG + P_BARO_PSIA
                            ' La substitution aura lieu seulement si l'unite du channel en cours est differente de l unite de la source.

                            If i = 0 And trySubstitution Then  'Traitement de la SOURCE uniquement (pas la destination)

                                Dim numColl As New HMINumericChannelsCollection()
                                Dim query As New HMINumericChannelsQuery()
                                query.es.DefaultConjunction = esConjunction.Or


                                If collectionName <> "CORE" Then
                                    'ATTENTION LA Collection CORE est fait dans le OpenTest_Sequence.Connect et A ce moment le CELPRoject.EngineModel est vide et ca plante
                                    Dim dynamicChannelCollection As New UUTDynamicChannelCollectionCollection()
                                    dynamicChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                                    dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.ProjectID = CELProject.Project.Number)
                                    dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.EngineModel = CELProject.EngineModel)
                                    dynamicChannelCollection.Query.OrderBy(dynamicChannelCollection.Query.LoadOrder.Ascending)
                                    dynamicChannelCollection.Query.Load()

                                    numColl.es.Connection.Name = CELProject.DefaultConnectionName

                                    If dynamicChannelCollection.Count > 0 Then

                                        'Load All possible mapping for different collection name in a SPECIFIC ENGINE MODEL
                                        For Each record As UUTDynamicChannelCollection In dynamicChannelCollection

                                            query.Where(query.CollectionName = record.CollectionName And query.Unavailable = False And query.ChannelName = channel.Source)

                                        Next

                                    End If

                                Else

                                    query.Where(query.CollectionName = collectionName And query.Unavailable = False And query.ChannelName = channel.Source)


                                End If

                                numColl.Load(query)

                                If numColl.Count = 0 Then

                                    'Now last possible location is CANBus
                                    Dim queryCanbus As New Dacn1CanbusLabelQuery()
                                    Dim numCollCanbus As New Dacn1CanbusLabelCollection()

                                    queryCanbus.Where(queryCanbus.EngineModel = CELProject.EngineModel And queryCanbus.Unavailable = False And queryCanbus.ChannelName = channel.Source)
                                    numCollCanbus.Load(queryCanbus)

                                    If numCollCanbus.Count = 1 Then
                                        Dim queryChannel As Dacn1CanbusLabel = numCollCanbus.Item(0)

                                        Dim fromUnit As String = CELProject.GetUnitNameById(CInt(queryChannel.EngUnitID))
                                        Dim toUnit As String = unitName

                                        If fromUnit <> toUnit Then
                                            Dim msg As String = "Can't perform conversion for a signal from CANbus " & fromUnit & " to " & toUnit + " on source " + channel.Source + " of channel " + channel.CollectionName + "." + channel.ChannelName
                                            If CELProject.NoPopupError Then
                                                Console.WriteLine(msg)
                                            Else
                                                clsTopMostMessageBox.Show(msg, "Expression Unit Conversion error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                            End If
                                        End If
                                    Else
                                        Dim msg As String = "Engineering Unit not Found / Substitution not possible: The Source " + channel.Source + " on Channel " + channel.ChannelName + " not found"
                                        If CELProject.NoPopupError Then
                                            Console.WriteLine(msg)
                                        Else
                                            clsTopMostMessageBox.Show(msg, "Engineering Unit not Found / Substitution not possible", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        End If
                                    End If

                                ElseIf numColl.Count = 1 Then
                                    Dim queryChan As HMINumericChannels = numColl.Item(0)

                                    Dim fromUnit As String = queryChan.UpToEngineeringUnitByEngUnitID.Name
                                    Dim toUnit As String = unitName

                                    If fromUnit <> "None" And toUnit <> "None" And fromUnit <> toUnit Then

                                        Dim expressionUnitConversion As New ExpressionUnitConversion()
                                        expressionUnitConversion.SetBaroChannel("P_BARO_PSIA", "PSIA")
                                        newChannel = CreateChannelNumericThresholdExpression(channel)
                                        newChannel.ToolTip = newChannel.ToolTip & "(Generated channel)"

                                        If expressionUnitConversion.ProcessUnitConversion(queryChan.ChannelName, fromUnit, toUnit, newChannel.Text) = True Then  'ft ne pas faire newChannel.Text.ToString
                                            Dim msg As String = "Can't perform conversion from " & fromUnit & " to " & toUnit + " on source " + channel.Source + " of channel " + channel.CollectionName + "." + channel.ChannelName
                                            If CELProject.NoPopupError Then
                                                Console.WriteLine(msg)
                                            Else
                                                clsTopMostMessageBox.Show(msg, "Expression Unit Conversion error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                            End If
                                        Else
                                            'ft  Console.WriteLine("The SOURCE of Channel " + channel.ChannelName + " changed for EXPRESSION=" + newChannel.Text + " (its source unit is different from channel unit)")
                                        End If

                                    End If
                                Else
                                    Dim msg As String = "The Source " + channel.Source + " on Channel " + channel.ChannelName.ToString + " not found"
                                    If CELProject.NoPopupError Then
                                        Console.WriteLine(msg)
                                    Else
                                        clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    End If
                                End If
                            End If

                        End If
                    Next
                End If
            End If

            If channelType = HMIChannelCreation.eNumericChannelType.NumericOpcItem Then

                numericChannelCollection.Add(newChannel)
                Dim meanChannel As Object = CreateChannelNumericExpressionMean(channel, CELProject.EngineConfiguration.PerformanceAndLogAveragingPeriod * 1000)
                numericChannelCollection.Add(meanChannel)

            Else
                engineChannelCollection.Add(newChannel)
                If channel.MeanEnabled Then

                    Dim meanChannel As Object = CreateChannelNumericExpressionMean(channel)
                    engineChannelCollection.Add(meanChannel)

                End If
            End If

        Next
    End Sub

    Public Function ValidateChannel(ByRef channel As HMINumericChannels) As Boolean
        If (channel.Expression <> "" And channel.Source <> "") Or _
        (channel.Expression <> "" And channel.Destination <> "") Or _
        (channel.Expression <> "" And channel.OPCGroup <> "") Or _
        (channel.Expression <> "" And channel.OPCItemID <> "") Or _
        (channel.Source <> "" And channel.OPCGroup <> "") Or _
        (channel.Destination <> "" And channel.OPCGroup <> "") Or _
        (channel.Source <> "" And channel.OPCItemID <> "") Or _
        (channel.Destination <> "" And channel.OPCItemID <> "") Then
            Dim msg As String = "ValidateChannel:  Channel " + channel.ChannelName + ": Source/Destination or Expression or OPC Group/ItemId are in conflict."
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If
        If channel.ChannelName.Contains(".") Then
            Dim msg As String = "ValidateChannel:  Channel " + channel.ChannelName + ": Name is invalid.  The character '.' cannot be be used. "
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If
        If channel.Source <> "" AndAlso (Not IsNothing(channel.InitialValue) Or Not IsNothing(channel.FinalValue)) AndAlso channel.Source <> channel.Destination Then 'Si la destination = source, c'est possible d'utiliser Le IntialValue dans la base données, sinon ca ne respecte pas l'integrité du systeme, alors il faut setter le InitialValue directement sur la Source
            Dim msg As String = "ValidateChannel: Cannot set initial/final value on channel numeric channel " + channel.CollectionName + "." + channel.ChannelName
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If
        If channel.Expression.Contains("_MAP") Then
            Dim msg As String = "ValidateChannel:  Channel " + channel.ChannelName + ": contains a field ending with _MAP and is not support in EXPRESSIONS."
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If

        If (channel.Source.Contains("_MAP") Or channel.Destination.Contains("_MAP")) And (channel.EngUnitID = 0 Or channel.EngUnitID = 10) Then
            Dim msg As String = "ValidateChannel:  Channel " + channel.ChannelName + ": engUnit could not be none."
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If
     

        Return True

    End Function

    Public Function ValidateChannel(ByRef channel As HMIBooleanChannels) As Boolean
        If (channel.Expression <> "" And channel.Source <> "") Or _
         (channel.Expression <> "" And channel.Destination <> "") Or _
         (channel.Expression <> "" And channel.OPCGroup <> "") Or _
         (channel.Expression <> "" And channel.OPCItemID <> "") Or _
         (channel.Source <> "" And channel.OPCGroup <> "") Or _
         (channel.Destination <> "" And channel.OPCGroup <> "") Or _
         (channel.Source <> "" And channel.OPCItemID <> "") Or _
         (channel.Destination <> "" And channel.OPCItemID <> "") Then
            Dim msg As String = "ValidateChannel:  Channel " + channel.ChannelName + ": Source/Destination or Expression or OPC Group/ItemId are in conflict."
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If
        If channel.ChannelName.Contains(".") Then
            Dim msg As String = "ValidateChannel:  Channel " + channel.ChannelName + ": Name is invalid.  The character '.' cannot be be used. "
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If
        If channel.Source <> "" AndAlso (Not IsNothing(channel.InitialValue) Or Not IsNothing(channel.FinalValue)) AndAlso channel.InitialValue <> 0 AndAlso channel.FinalValue <> 0 AndAlso channel.Source <> channel.Destination Then 'Si la destination = source, c'est possible d'utiliser Le IntialValue dans la base données, sinon ca ne respecte pas l'integrité du systeme, alors il faut setter le InitialValue directement sur la Source
            Dim msg As String = "ValidateChannel: Cannot set initial/final value on channel boolean channel " + channel.CollectionName + "." + channel.ChannelName
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If
        If channel.Expression.Contains("_MAP") Then
            Dim msg As String = "ValidateChannel:  Channel " + channel.ChannelName + ": contains a field ending with _MAP and is not support in EXPRESSIONS."
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If
        If channel.Expression.Contains("Test_Mgr_UUT") Then
            Dim msg As String = "ValidateChannel:  Channel " + channel.ChannelName + ": contains Test_Mgr_UUT_Model/Family in the logic : please use ENGINE_MODEL or ENGINE_FAMILY CORE channels" +
            " or specific engine channels instead. Test_Mgr_UUT_Model/Family can cause intermittent logic states and channels may not be reset properly at the close test"
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End If

        Return True

    End Function

    Public Function CreateChannelNumericExpressionConversion(ByRef channel As HMINumericChannels) As KineticaRT.Logic.Channels.NumericExpression
        Dim newChannel As KineticaRT.Logic.Channels.NumericExpression = Nothing

        Dim expressionUnitConversion As New ExpressionUnitConversion()

        Dim channelKineticaRT As KineticaRT.Channels.INumericChannel
        Dim channelNameAlreadyContainsUnit As Boolean = False
        Dim fromUnitName As String = String.Empty

        If CELProject.ChannelList.ContainsKey(channel.Source) Then
            fromUnitName = CELProject.ChannelList(channel.Source)
        Else
            Dim msg As String = "OpenTest_Sequence.CreateChannelNumericExpressionConversion: " + channel.Source + " does not exist in the Master Channel List please review DACN* Input/Output parameters"
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Channel List Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If
        End If


        If channel.Source.EndsWith(fromUnitName) Then  'Le nom du channel contient deja son unite
            channelKineticaRT = CELProject.GetChannelCallBack(channel.Source) 'ex.: P8_PSIA
            channelNameAlreadyContainsUnit = True
        Else
            channelKineticaRT = CELProject.GetChannelCallBack(channel.Source + "_" + fromUnitName) 'ex.: P8_PSIA
        End If

        If Not IsNothing(channelKineticaRT) Then

            Debug.Assert(Not IsNothing(channel.EngUnitID), "You must specify unit on channel " + channel.ChannelName + " in DACN Table")

            Dim toUnitName As String = CELProject.GetUnitNameById(CInt(channel.EngUnitID))
            newChannel = New KineticaRT.Logic.Channels.NumericExpression
            If (channelNameAlreadyContainsUnit) Then
                newChannel.Name = channel.Source.Replace(fromUnitName, toUnitName)
            Else
                newChannel.Name = channel.Source + "_" + toUnitName
            End If


            If Not IsNothing(channel.EngUnitID) Then
                newChannel.Units = CELProject.GetUnitDisplayNameById(CInt(channel.EngUnitID))
            Else
                Dim msg As String = " EngUnitID is invalid on channel " + channel.CollectionName + "." + channel.ChannelName
                If CELProject.NoPopupError Then
                    Console.WriteLine(msg)
                Else
                    clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

            newChannel.Description = "Generated channel " + newChannel.Name
            newChannel.ToolTip = "Generated channel " + newChannel.Name

            expressionUnitConversion.SetBaroChannel("P_BARO_PSIA", "PSIA")

            'If expressionUnitConversion.ProcessUnitConversion(channel.Source + "_" + CELProject.ChannelList(channel.Source), CELProject.ChannelList(channel.Source), unitName, newChannel.Text) = True Then
            If expressionUnitConversion.ProcessUnitConversion(channelKineticaRT.Name, fromUnitName, toUnitName, newChannel.Text) = True Then
                Dim msg As String = "Can't perform conversion from " & fromUnitName & " to " & toUnitName + " on channel " + channel.Source
                If CELProject.NoPopupError Then
                    Console.WriteLine(msg)
                Else
                    clsTopMostMessageBox.Show(msg, "Expression Unit Conversion error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End If

            End If

            newChannel.DecimalDigits = "3"
            newChannel.Format = "F3"
            newChannel.Maximum = 100
            newChannel.Minimum = 0
        Else
            Console.WriteLine("The channel " + channel.Source + " does not exist")
            Debug.Assert(False)
        End If

        Return newChannel

    End Function

    Public Function CreateChannelMessageEvent(ByVal collectionName As String, ByRef channelCollection As KineticaRT.ChannelCollection) As Boolean

        CreateChannelMessageEvent = False

        Try

            If Not IsNothing(channelCollection) Then

                'Query Database
                'esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                Dim booleanChannelsCollection As HMIBooleanChannelsCollection = New HMIBooleanChannelsCollection()
                booleanChannelsCollection.es.Connection.Name = CELProject.DefaultConnectionName
                booleanChannelsCollection.Query.Where(booleanChannelsCollection.Query.CollectionName = collectionName)
                booleanChannelsCollection.Query.Where(booleanChannelsCollection.Query.Unavailable = False)
                booleanChannelsCollection.Query.Load()

                For Each channel As HMIBooleanChannels In booleanChannelsCollection


                    If channel.FalseMessageText <> "" Or channel.TrueMessageText <> "" Then

                        'Create Boolean Channels
                        Dim messageChannel As New KineticaRT.Logic.Message.Event

                        messageChannel.Destination = "Messages"
                        If channel.TrueMessageTextColor = "" Then
                            messageChannel.TextColor = Color.Empty
                        Else
                            messageChannel.TextColor = Color.FromName(channel.TrueMessageTextColor)
                        End If
                        If channel.FalseMessageTextColor = "" Then
                            messageChannel.ReturnTextColor = Color.Empty
                        Else
                            messageChannel.ReturnTextColor = Color.FromName(channel.FalseMessageTextColor)
                        End If
                        If channel.FalseMessageText <> "" Then
                            messageChannel.ReturnText = channel.Description + " is now " + channel.FalseMessageText
                        Else
                            messageChannel.ReturnText = ""
                        End If
                        messageChannel.Source = channel.ChannelName
                        messageChannel.SourceText = channel.ChannelName
                        If channel.TrueMessageText <> "" Then
                            messageChannel.Text = channel.Description + " is now " + channel.TrueMessageText
                        Else
                            messageChannel.Text = ""
                        End If
                        messageChannel.Name = channel.ChannelName + "_Event"

                        channelCollection.Add(messageChannel)

                    End If
                Next

                CreateChannelMessageEvent = True

            Else
                Debug.Assert(False)
            End If

        Catch ex As Exception

            Dim msg As String = "OpenTest_Sequence.CreateChannelMessageEvent: " & ex.ToString()
            If CELProject.NoPopupError Then
                Console.WriteLine(msg)
            Else
                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try

    End Function

    Public Function CreateChannelNumericThreshold(ByRef channel As HMINumericChannels) As CEL.KineticaRT.Servers.Logger.NumericThresholdChannel

        Dim c As System.Drawing.Color
        Dim newChannel As New CEL.KineticaRT.Servers.Logger.NumericThresholdChannel

        newChannel.Source = channel.Source

        'Archive
        newChannel.Enabled = CELProject.DicArchiveChannelList.ContainsKey(channel.ChannelName)

        If newChannel.Enabled Then
            newChannel.Index = CELProject.DicArchiveChannelList(channel.ChannelName) 'Index
        Else
            newChannel.Index = 0
        End If
        newChannel.Logger = "ARCHIVE"
        newChannel.EventLogger = "EVENT_ARCHIVE"


        If Not IsNothing(channel.HighTrigger) AndAlso channel.HighTriggerEnabled = True Then
            newChannel.HighTrigger = channel.HighTrigger
        End If

        If Not IsNothing(channel.LowTrigger) AndAlso channel.LowTriggerEnabled = True Then
            newChannel.LowTrigger = channel.LowTrigger
        End If

        If IsNothing(channel.ThresholdHigh) AndAlso channel.ThresholdHighSource = "" Then
            newChannel.High = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            newChannel.HighColor = System.Drawing.Color.White
            newChannel.HighSource = ""
        Else
            If IsNothing(channel.ThresholdHigh) Then
                newChannel.High = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            Else
                newChannel.High = CDbl(channel.ThresholdHigh)
            End If

            If channel.ThresholdHighColor <> "" Then
                Try
                    If IsNumeric(channel.ThresholdHighColor) Then
                        c = System.Drawing.ColorTranslator.FromOle(CInt(channel.ThresholdHighColor))
                    Else
                        c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdHighColor), System.Drawing.Color)
                    End If
                Catch ex As Exception
                    clsTopMostMessageBox.Show("OpenTest_Sequence.CreateChannelNumericThreshold: Invalid High Color " + channel.ThresholdHighColor + " for channel " + channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    c = Color.White
                End Try
            Else
                c = Color.White
            End If

            newChannel.HighColor = c

            newChannel.HighSource = channel.ThresholdHighSource
        End If

        If IsNothing(channel.ThresholdHighHigh) AndAlso channel.ThresholdHighHighSource = "" Then
            newChannel.HighHigh = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            newChannel.HighHighColor = Color.White
            newChannel.HighHighSource = ""
        Else
            If IsNothing(channel.ThresholdHighHigh) Then
                newChannel.HighHigh = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            Else
                newChannel.HighHigh = CDbl(channel.ThresholdHighHigh)
            End If

            If channel.ThresholdHighHighColor <> "" Then
                Try
                    If IsNumeric(channel.ThresholdHighHighColor) Then
                        c = System.Drawing.ColorTranslator.FromOle(CInt(channel.ThresholdHighHighColor))
                    Else
                        c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdHighHighColor), System.Drawing.Color)
                    End If
                Catch ex As Exception
                    clsTopMostMessageBox.Show("OpenTest_Sequence.CreateChannelNumericThreshold: Invalid HighHigh Color " + channel.ThresholdHighHighColor + " for channel " + channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    c = Color.White
                End Try
            Else
                c = Color.White
            End If

            newChannel.HighHighColor = c
            newChannel.HighHighSource = channel.ThresholdHighHighSource
        End If
        If IsNothing(channel.ThresholdLow) AndAlso channel.ThresholdLowSource = "" Then
            newChannel.Low = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            newChannel.LowColor = System.Drawing.Color.White
            newChannel.LowSource = ""
        Else
            If IsNothing(channel.ThresholdLow) Then
                newChannel.Low = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            Else
                newChannel.Low = CDbl(channel.ThresholdLow)
            End If

            If channel.ThresholdLowColor <> "" Then
                Try
                    If IsNumeric(channel.ThresholdLowColor) Then
                        c = System.Drawing.ColorTranslator.FromOle(CInt(channel.ThresholdLowColor))
                    Else
                        c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdLowColor), System.Drawing.Color)
                    End If
                Catch ex As Exception
                    clsTopMostMessageBox.Show("OpenTest_Sequence.CreateChannelNumericThreshold: Invalid Low Color " + channel.ThresholdLowColor + " for channel " + channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    c = Color.White
                End Try
            Else
                c = Color.White
            End If

            newChannel.LowColor = c

            newChannel.LowSource = channel.ThresholdLowSource
        End If

        If IsNothing(channel.ThresholdLowLow) AndAlso channel.ThresholdLowLowSource = "" Then
            newChannel.LowLow = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            newChannel.LowLowColor = System.Drawing.Color.White
            newChannel.LowLowSource = ""
        Else
            If IsNothing(channel.ThresholdLowLow) Then
                newChannel.LowLow = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            Else
                newChannel.LowLow = CDbl(channel.ThresholdLowLow)
            End If

            If channel.ThresholdLowLowColor <> "" Then
                Try
                    If IsNumeric(channel.ThresholdLowLowColor) Then
                        c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdLowLowColor)
                    Else
                        c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdLowLowColor), System.Drawing.Color)
                    End If
                Catch ex As Exception
                    clsTopMostMessageBox.Show("OpenTest_Sequence.CreateChannelNumericThreshold: Invalid LOWLOW Color " + channel.ThresholdLowLowColor + " for channel " + channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    c = Color.White
                End Try

            Else
                c = Color.White
            End If

            newChannel.LowLowColor = c
            newChannel.LowLowSource = channel.ThresholdLowLowSource
        End If

        If channel.ThresholdNormalColor <> "" Then
            Try
                If IsNumeric(channel.ThresholdNormalColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(CInt(channel.ThresholdNormalColor))
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdNormalColor), System.Drawing.Color)
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.CreateChannelNumericThreshold: Invalid Normal Color " + channel.ThresholdNormalColor + " for channel " + channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                c = Color.White
            End Try
        Else
            c = Color.White
        End If

        newChannel.NormalColor = c

        If IsNothing(channel.ThresholdNormal) Then
            newChannel.Normal = 0
        Else
            newChannel.Normal = CDbl(channel.ThresholdNormal)
        End If

        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.DecimalDigits = CInt(channel.DecimalDigits)
        newChannel.Format = channel.Format
        If Not IsNothing(channel.EngUnitID) Then
            newChannel.Units = CELProject.GetUnitDisplayNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.DisplayName 'channel.Units
        Else
            clsTopMostMessageBox.Show("EngUnitID is invalid on " + channel.CollectionName + "." + channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        newChannel.Name = channel.ChannelName

        'Validate Maximum
        If IsNothing(channel.Maximum) Then
            clsTopMostMessageBox.Show("Channel " & channel.ChannelName & ":  Value for Maximum not set", CELProject.TechnologyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        newChannel.Maximum = IIf(channel.Maximum & "" = "", 0, channel.Maximum)
        'Validate Minimum
        If IsNothing(channel.Minimum) Then
            clsTopMostMessageBox.Show("Channel " & channel.ChannelName & ":  Value for Minimum not set", CELProject.TechnologyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        newChannel.Minimum = IIf(channel.Minimum & "" = "", 0, channel.Minimum)

        If Not IsNothing(channel.InitialValue) Then
            newChannel.Value = New KineticaRT.Values.Double(channel.InitialValue)
        End If

        Return newChannel

    End Function

    Public Function CreateChannelNumericThresholdLink(ByRef channel As HMINumericChannels) As CEL.KineticaRT.Servers.Logger.NumericThresholdLink

        Dim c As System.Drawing.Color
        Dim newChannel As New CEL.KineticaRT.Servers.Logger.NumericThresholdLink

        newChannel.Destination = channel.Destination
        newChannel.Source = channel.Source

        'Archive
        newChannel.Enabled = CELProject.DicArchiveChannelList.ContainsKey(channel.ChannelName)

        If newChannel.Enabled Then
            newChannel.Index = CELProject.DicArchiveChannelList(channel.ChannelName) 'Index
        Else
            newChannel.Index = 0
        End If
        newChannel.Logger = "ARCHIVE"
        newChannel.EventLogger = "EVENT_ARCHIVE"


        If Not IsNothing(channel.HighTrigger) AndAlso channel.HighTriggerEnabled = True Then
            newChannel.HighTrigger = CDbl(channel.HighTrigger)
        End If

        If Not IsNothing(channel.LowTrigger) AndAlso channel.LowTriggerEnabled = True Then
            newChannel.LowTrigger = CDbl(channel.LowTrigger)
        End If


        If IsNothing(channel.ThresholdHigh) AndAlso channel.ThresholdHighSource = "" Then
            newChannel.High = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            newChannel.HighColor = System.Drawing.Color.White
            newChannel.HighSource = ""
        Else
            If IsNothing(channel.ThresholdHigh) Then
                newChannel.High = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            Else
                newChannel.High = CDbl(channel.ThresholdHigh)
            End If

            If channel.ThresholdHighColor <> "" Then
                If IsNumeric(channel.ThresholdHighColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdHighColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdHighColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.HighColor = c
            newChannel.HighSource = channel.ThresholdHighSource
        End If

        If IsNothing(channel.ThresholdHighHigh) AndAlso channel.ThresholdHighHighSource = "" Then
            newChannel.HighHigh = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            newChannel.HighHighColor = System.Drawing.Color.White
            newChannel.HighHighSource = ""
        Else
            If IsNothing(channel.ThresholdHighHigh) Then
                newChannel.HighHigh = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            Else
                newChannel.HighHigh = CDbl(channel.ThresholdHighHigh)
            End If

            If channel.ThresholdHighHighColor <> "" Then
                If IsNumeric(channel.ThresholdHighHighColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdHighHighColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdHighHighColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.HighHighColor = c

            newChannel.HighHighSource = channel.ThresholdHighHighSource
        End If
        If IsNothing(channel.ThresholdLow) AndAlso channel.ThresholdLowSource = "" Then
            newChannel.Low = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            newChannel.LowColor = System.Drawing.Color.White
            newChannel.LowSource = ""
        Else
            If IsNothing(channel.ThresholdLow) Then
                newChannel.Low = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            Else
                newChannel.Low = CDbl(channel.ThresholdLow)
            End If

            If channel.ThresholdLowColor <> "" Then
                If IsNumeric(channel.ThresholdLowColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdLowColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdLowColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.LowColor = c

            newChannel.LowSource = channel.ThresholdLowSource
        End If
        If IsNothing(channel.ThresholdLowLow) AndAlso channel.ThresholdLowLowSource = "" Then
            newChannel.LowLow = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            newChannel.LowLowColor = System.Drawing.Color.White
            newChannel.LowLowSource = ""
        Else
            If IsNothing(channel.ThresholdLowLow) Then
                newChannel.LowLow = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            Else
                newChannel.LowLow = CDbl(channel.ThresholdLowLow)
            End If

            If channel.ThresholdLowLowColor <> "" Then
                If IsNumeric(channel.ThresholdLowLowColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdLowLowColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdLowLowColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.LowLowColor = c
            newChannel.LowLowSource = channel.ThresholdLowLowSource
        End If
        If IsNothing(channel.ThresholdNormal) Then
            newChannel.Normal = 0
            newChannel.NormalColor = System.Drawing.Color.White
        Else
            newChannel.Normal = CDbl(channel.ThresholdNormal)

            If channel.ThresholdNormalColor <> "" Then
                If IsNumeric(channel.ThresholdNormalColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdNormalColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdNormalColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.NormalColor = c
        End If

        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.DecimalDigits = CInt(channel.DecimalDigits)
        newChannel.Format = channel.Format
        If Not IsNothing(channel.EngUnitID) Then
            newChannel.Units = CELProject.GetUnitDisplayNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.DisplayName 'channel.Units
        Else
            clsTopMostMessageBox.Show(" EngUnitID is invalid on channel " + channel.CollectionName + "." + channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        newChannel.Name = channel.ChannelName

        'Validate Maximum
        If IsNothing(channel.Maximum) Then
            clsTopMostMessageBox.Show("Channel " & channel.ChannelName & ":  Value for Maximum not set", CELProject.TechnologyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        newChannel.Maximum = IIf(IsNothing(channel.Maximum), 0, channel.Maximum)
        'Validate Minimum
        If IsNothing(channel.Minimum) Then
            clsTopMostMessageBox.Show("Channel " & channel.ChannelName & ":  Value for Minimum not set", CELProject.TechnologyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        newChannel.Minimum = IIf(IsNothing(channel.Minimum), 0, channel.Minimum)

        If Not IsNothing(channel.InitialValue) Then
            newChannel.Value = New KineticaRT.Values.Double(channel.InitialValue)
        End If

        Return newChannel

    End Function

    Public Function CreateChannelNumericThresholdOpcItem(ByRef channel As HMINumericChannels) As KineticaRT.Servers.OpcClient.NumericThresholdItem

        Dim c As System.Drawing.Color
        Dim newChannel As New KineticaRT.Servers.OpcClient.NumericThresholdItem()

        newChannel.Name = channel.ChannelName
        newChannel.ItemID = channel.OPCItemID
        newChannel.Group = channel.OPCGroup

        If IsNothing(channel.ThresholdHigh) AndAlso channel.ThresholdHighSource = "" Then
            newChannel.High = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            newChannel.HighColor = System.Drawing.Color.White
            newChannel.ItemIdHigh = ""
        Else
            If IsNothing(channel.ThresholdHigh) Then
                newChannel.High = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            Else
                newChannel.High = CDbl(channel.ThresholdHigh)
            End If

            If channel.ThresholdHighColor <> "" Then
                If IsNumeric(channel.ThresholdHighColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdHighColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdHighColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.HighColor = c

            newChannel.ItemIdHigh = channel.ThresholdHighSource
        End If

        If IsNothing(channel.ThresholdHighHigh) AndAlso channel.ThresholdHighHighSource = "" Then
            newChannel.HighHigh = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            newChannel.HighHighColor = System.Drawing.Color.White
            newChannel.ItemIdHighHigh = ""
        Else
            If IsNothing(channel.ThresholdHighHigh) Then
                newChannel.HighHigh = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            Else
                newChannel.HighHigh = CDbl(channel.ThresholdHighHigh)
            End If

            If channel.ThresholdHighHighColor <> "" Then
                If IsNumeric(channel.ThresholdHighHighColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdHighHighColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdHighHighColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.HighHighColor = c
            newChannel.ItemIdHighHigh = channel.ThresholdHighHighSource
        End If

        If IsNothing(channel.ThresholdLow) AndAlso channel.ThresholdLowSource = "" Then
            newChannel.Low = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            newChannel.LowColor = System.Drawing.Color.White
            newChannel.ItemIdLow = ""
        Else
            If IsNothing(channel.ThresholdLow) Then
                newChannel.Low = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            Else
                newChannel.Low = CDbl(channel.ThresholdLow)
            End If

            If channel.ThresholdLowColor <> "" Then
                If IsNumeric(channel.ThresholdLowColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdLowColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdLowColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.LowColor = c
            newChannel.ItemIdLow = channel.ThresholdLowSource
        End If
        If IsNothing(channel.ThresholdLowLow) AndAlso channel.ThresholdLowLowSource = "" Then
            newChannel.LowLow = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            newChannel.LowLowColor = System.Drawing.Color.White
            newChannel.ItemIdLowLow = ""
        Else
            If IsNothing(channel.ThresholdLowLow) Then
                newChannel.LowLow = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            Else
                newChannel.LowLow = CDbl(channel.ThresholdLowLow)
            End If

            If channel.ThresholdLowLowColor <> "" Then
                If IsNumeric(channel.ThresholdLowLowColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdLowLowColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdLowLowColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.LowLowColor = c
            newChannel.ItemIdLowLow = channel.ThresholdLowLowSource
        End If
        If IsNothing(channel.ThresholdNormal) Then
            newChannel.Normal = 0
            newChannel.NormalColor = System.Drawing.Color.White
        Else
            newChannel.Normal = CDbl(channel.ThresholdNormal)

            If channel.ThresholdNormalColor <> "" Then
                If IsNumeric(channel.ThresholdNormalColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdNormalColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdNormalColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.NormalColor = c

        End If

        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.DecimalDigits = CInt(channel.DecimalDigits)
        newChannel.Format = channel.Format
        If Not IsNothing(channel.EngUnitID) Then
            newChannel.Units = CELProject.GetUnitDisplayNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.DisplayName 'channel.Units
        Else
            clsTopMostMessageBox.Show(" EngUnitID is invalid on channel " + channel.CollectionName + "." + channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        newChannel.Name = channel.ChannelName

        'Validate Maximum
        If IsNothing(channel.Maximum) Then
            clsTopMostMessageBox.Show("Channel " & channel.ChannelName & ":  Value for Maximum not set", CELProject.TechnologyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        newChannel.Maximum = IIf(channel.Maximum & "" = "", 0, channel.Maximum)
        'Validate Minimum
        If IsNothing(channel.Minimum) Then
            clsTopMostMessageBox.Show("Channel " & channel.ChannelName & ":  Value for Minimum not set", CELProject.TechnologyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        newChannel.Minimum = IIf(IsNothing(channel.Minimum), 0, channel.Minimum)

        If Not IsNothing(channel.InitialValue) Then
            newChannel.Value = New KineticaRT.Values.Double(channel.InitialValue)
        End If

        Return newChannel

    End Function

    Public Function CreateChannelNumericThresholdExpression(ByRef channel As HMINumericChannels) As CEL.KineticaRT.Servers.Logger.NumericThresholdExpression

        Dim c As System.Drawing.Color
        Dim newChannel As CEL.KineticaRT.Servers.Logger.NumericThresholdExpression

        newChannel = New CEL.KineticaRT.Servers.Logger.NumericThresholdExpression
        newChannel.Text = channel.Expression

        'Archive
        newChannel.Enabled = CELProject.DicArchiveChannelList.ContainsKey(channel.ChannelName)

        If newChannel.Enabled Then
            newChannel.Index = CELProject.DicArchiveChannelList(channel.ChannelName) 'Index
        Else
            newChannel.Index = 0
        End If
        newChannel.Logger = "ARCHIVE"
        newChannel.EventLogger = "EVENT_ARCHIVE"


        If Not IsNothing(channel.HighTrigger) AndAlso channel.HighTriggerEnabled = True Then
            newChannel.HighTrigger = CDbl(channel.HighTrigger)
        End If

        If Not IsNothing(channel.LowTrigger) AndAlso channel.LowTriggerEnabled = True Then
            newChannel.LowTrigger = CDbl(channel.LowTrigger)
        End If


        If IsNothing(channel.ThresholdHigh) AndAlso channel.ThresholdHighSource = "" Then
            newChannel.High = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            newChannel.HighColor = System.Drawing.Color.White
            newChannel.HighSource = ""
        Else
            If IsNothing(channel.ThresholdHigh) Then
                newChannel.High = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            Else
                newChannel.High = CDbl(channel.ThresholdHigh)
            End If

            If channel.ThresholdHighColor <> "" Then
                If IsNumeric(channel.ThresholdHighColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdHighColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdHighColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If
            newChannel.HighColor = c
            newChannel.HighSource = channel.ThresholdHighSource
        End If

        If IsNothing(channel.ThresholdHighHigh) AndAlso channel.ThresholdHighHighSource = "" Then
            newChannel.HighHigh = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            newChannel.HighHighColor = System.Drawing.Color.White
            newChannel.HighHighSource = ""
        Else
            If IsNothing(channel.ThresholdHighHigh) Then
                newChannel.HighHigh = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            Else
                newChannel.HighHigh = CDbl(channel.ThresholdHighHigh)
            End If

            If channel.ThresholdHighHighColor <> "" Then
                If IsNumeric(channel.ThresholdHighHighColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdHighHighColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdHighHighColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.HighHighColor = c
            newChannel.HighHighSource = channel.ThresholdHighHighSource
        End If
        If IsNothing(channel.ThresholdLow) AndAlso channel.ThresholdLowSource = "" Then
            newChannel.Low = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            newChannel.LowColor = System.Drawing.Color.White
            newChannel.LowSource = ""
        Else
            If IsNothing(channel.ThresholdLow) Then
                newChannel.Low = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            Else
                newChannel.Low = CDbl(channel.ThresholdLow)
            End If

            If channel.ThresholdLowColor <> "" Then
                If IsNumeric(channel.ThresholdLowColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdLowColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdLowColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.LowColor = c
            newChannel.LowSource = channel.ThresholdLowSource
        End If
        If IsNothing(channel.ThresholdLowLow) AndAlso channel.ThresholdLowLowSource = "" Then
            newChannel.LowLow = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            newChannel.LowLowColor = System.Drawing.Color.White
            newChannel.LowLowSource = ""
        Else
            If IsNothing(channel.ThresholdLowLow) Then
                newChannel.LowLow = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            Else
                newChannel.LowLow = CDbl(channel.ThresholdLowLow)
            End If

            If channel.ThresholdLowLowColor <> "" Then
                If IsNumeric(channel.ThresholdLowLowColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdLowLowColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdLowLowColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.LowLowColor = c
            newChannel.LowLowSource = channel.ThresholdLowLowSource
        End If
        If IsNothing(channel.ThresholdNormal) Then
            newChannel.Normal = 0
            newChannel.NormalColor = System.Drawing.Color.White
        Else
            newChannel.Normal = CDbl(channel.ThresholdNormal)

            If channel.ThresholdNormalColor <> "" Then
                If IsNumeric(channel.ThresholdNormalColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdNormalColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdNormalColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.NormalColor = c
        End If

        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.DecimalDigits = CInt(channel.DecimalDigits)
        newChannel.Format = channel.Format
        If Not IsNothing(channel.EngUnitID) Then
            newChannel.Units = CELProject.GetUnitDisplayNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.DisplayName 'channel.Units
        Else
            clsTopMostMessageBox.Show(" EngUnitID is invalid on channel " + channel.CollectionName + "." + channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        newChannel.Name = channel.ChannelName

        'Validate Maximum
        If IsNothing(channel.Maximum) Then
            clsTopMostMessageBox.Show("Channel " & channel.ChannelName & ":  Value for Maximum not set", CELProject.TechnologyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        newChannel.Maximum = IIf(IsNothing(channel.Maximum), 0, channel.Maximum)
        'Validate Minimum
        If IsNothing(channel.Minimum) Then
            clsTopMostMessageBox.Show("Channel " & channel.ChannelName & ":  Value for Minimum not set", CELProject.TechnologyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        newChannel.Minimum = IIf(IsNothing(channel.Minimum), 0, channel.Minimum)

        If Not IsNothing(channel.InitialValue) Then 'ft ajout du set pour une expression, pour permettre d'initialiser un cas special comme par exemple "FLOW_SENSOR_PRESSURE_PSIG = If(CAPTURE_FLOW_SENSOR_PRESSURE_BTN,P_BLEED_PSIG_AVG ,FLOW_SENSOR_PRESSURE_PSIG)" pour eviter que l'expression Affiche Not Connected
            newChannel.Value = New KineticaRT.Values.Double(channel.InitialValue)
        End If

        Return newChannel

    End Function

    Public Function CreateChannelNumericOpcItem(ByRef channel As DacnTemplateReal) As KineticaRT.Servers.OpcClient.Item
        Dim newChannel As New KineticaRT.Servers.OpcClient.Item

        Debug.Assert(Not IsNothing(channel.EngUnitID), "You must specify engineering unit on channel " + channel.Name)

        Dim name As String = CELProject.GetUnitNameById(CInt(channel.EngUnitID))
        newChannel.Description = channel.Name + "_" + name
        newChannel.Name = channel.Name + "_" + name
        newChannel.Group = channel.OPCGroup
        newChannel.ItemID = channel.OPCItemID
        If Not IsNothing(channel.EngUnitID) Then
            newChannel.Units = CELProject.GetUnitDisplayNameById(CInt(channel.EngUnitID))
        Else
            clsTopMostMessageBox.Show(" EngUnitID is invalid on channel " + channel.Name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Return newChannel


    End Function

    Public Function CreateChannelNumericExpressionMean(ByRef channel As HMINumericChannels, Optional meanPeriod As Integer = 0) As CEL.KineticaRT.Servers.Logger.NumericThresholdExpression 'KineticaRT.Logic.Expressions.NumericExpression

        Dim c As System.Drawing.Color
        Dim newChannel As New CEL.KineticaRT.Servers.Logger.NumericThresholdExpression 'KineticaRT.Logic.Expressions.NumericExpression

        newChannel.Description = "Mean " + channel.Description
        newChannel.ToolTip = "Mean " + channel.Tooltip

        newChannel.Enabled = CELProject.DicArchiveChannelList.ContainsKey(channel.ChannelName)

        If newChannel.Enabled Then
            newChannel.Index = CELProject.DicArchiveChannelList(channel.ChannelName) 'Index
        Else
            newChannel.Index = 0
        End If
        newChannel.Logger = "ARCHIVE"
        newChannel.EventLogger = "EVENT_ARCHIVE"


        If IsNothing(channel.MeanPeriod) Then
            If meanPeriod = 0 Then
                clsTopMostMessageBox.Show("OpenTest.CreateEngineNumericChannels.CreateMeanChannel Error: the MeanPeriod field is missing on " & channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                channel.MeanPeriod = meanPeriod
            End If
        End If

        newChannel.Text = "RollingMean(" + channel.ChannelName + "," + CStr(CDbl(channel.MeanPeriod) / 1000) + ")"
        newChannel.DecimalDigits = CInt(channel.DecimalDigits)
        newChannel.Format = channel.Format
        'Validate Maximum
        If IsNothing(channel.Maximum) Then
            clsTopMostMessageBox.Show("Channel " & channel.ChannelName & ":  Value for Maximum not set", CELProject.TechnologyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        newChannel.Maximum = IIf(IsNothing(channel.Maximum), 0, channel.Maximum)
        'Validate Minimum
        If IsNothing(channel.Minimum) Then
            clsTopMostMessageBox.Show("Channel " & channel.ChannelName & ":  Value for Minimum not set", CELProject.TechnologyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        newChannel.Minimum = IIf(IsNothing(channel.Minimum), 0, channel.Minimum)
        If Not IsNothing(channel.EngUnitID) Then
            newChannel.Units = CELProject.GetUnitDisplayNameById(CInt(channel.EngUnitID)) 'channel.UpToEngineeringUnitByEngUnitID.DisplayName 'channel.Units
        Else
            clsTopMostMessageBox.Show(" EngUnitID is invalid on channel " + channel.CollectionName + "." + channel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        newChannel.Name = channel.ChannelName + "_MEAN"


        'Ajout des Threshold
        If IsNothing(channel.ThresholdHigh) AndAlso channel.ThresholdHighSource = "" Then
            newChannel.High = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            newChannel.HighColor = System.Drawing.Color.White
            newChannel.HighSource = ""
        Else
            If IsNothing(channel.ThresholdHigh) Then
                newChannel.High = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            Else
                newChannel.High = CDbl(channel.ThresholdHigh)
            End If

            If channel.ThresholdHighColor <> "" Then
                If IsNumeric(channel.ThresholdHighColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdHighColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdHighColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If
            newChannel.HighColor = c
            newChannel.HighSource = channel.ThresholdHighSource
        End If

        If IsNothing(channel.ThresholdHighHigh) AndAlso channel.ThresholdHighHighSource = "" Then
            newChannel.HighHigh = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            newChannel.HighHighColor = System.Drawing.Color.White
            newChannel.HighHighSource = ""
        Else
            If IsNothing(channel.ThresholdHighHigh) Then
                newChannel.HighHigh = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
            Else
                newChannel.HighHigh = CDbl(channel.ThresholdHighHigh)
            End If

            If channel.ThresholdHighHighColor <> "" Then
                If IsNumeric(channel.ThresholdHighHighColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdHighHighColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdHighHighColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.HighHighColor = c
            newChannel.HighHighSource = channel.ThresholdHighHighSource
        End If
        If IsNothing(channel.ThresholdLow) AndAlso channel.ThresholdLowSource = "" Then
            newChannel.Low = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            newChannel.LowColor = System.Drawing.Color.White
            newChannel.LowSource = ""
        Else
            If IsNothing(channel.ThresholdLow) Then
                newChannel.Low = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            Else
                newChannel.Low = CDbl(channel.ThresholdLow)
            End If

            If channel.ThresholdLowColor <> "" Then
                If IsNumeric(channel.ThresholdLowColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdLowColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdLowColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.LowColor = c
            newChannel.LowSource = channel.ThresholdLowSource
        End If
        If IsNothing(channel.ThresholdLowLow) AndAlso channel.ThresholdLowLowSource = "" Then
            newChannel.LowLow = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            newChannel.LowLowColor = System.Drawing.Color.White
            newChannel.LowLowSource = ""
        Else
            If IsNothing(channel.ThresholdLowLow) Then
                newChannel.LowLow = KineticaRT.Values.Values.NegativeInfinity.ToDouble()
            Else
                newChannel.LowLow = CDbl(channel.ThresholdLowLow)
            End If

            If channel.ThresholdLowLowColor <> "" Then
                If IsNumeric(channel.ThresholdLowLowColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdLowLowColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdLowLowColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.LowLowColor = c
            newChannel.LowLowSource = channel.ThresholdLowLowSource
        End If
        If IsNothing(channel.ThresholdNormal) Then
            newChannel.Normal = 0
            newChannel.NormalColor = System.Drawing.Color.White
        Else
            newChannel.Normal = CDbl(channel.ThresholdNormal)

            If channel.ThresholdNormalColor <> "" Then
                If IsNumeric(channel.ThresholdNormalColor) Then
                    c = System.Drawing.ColorTranslator.FromOle(channel.ThresholdNormalColor)
                Else
                    c = CType(System.ComponentModel.TypeDescriptor.GetConverter(GetType(System.Drawing.Color)).ConvertFromString(channel.ThresholdNormalColor), System.Drawing.Color)
                End If
            Else
                c = Color.White
            End If

            newChannel.NormalColor = c
        End If

        Return newChannel

    End Function
    Public Function CreateChannelNumericExpressionMean(ByRef channel As KineticaRT.Servers.OpcClient.Item) As KineticaRT.Logic.Expressions.NumericExpression

        Dim newChannel As New KineticaRT.Logic.Expressions.NumericExpression

        newChannel.Name = channel.Name + "_MEAN"
        newChannel.Description = "Mean " + "_" + channel.Name
        newChannel.ToolTip = "Mean " + channel.Name
        newChannel.Text = "RollingMean(" + channel.Name + ", 5)"  '5 Secondes par DEFAUT

        newChannel.DecimalDigits = "3"
        newChannel.Format = "F3"
        newChannel.Maximum = 100
        newChannel.Minimum = 0

        Return newChannel

    End Function
    Public Function CreateChannelNumericExpressionMean(ByRef channel As KineticaRT.Servers.OpcClient.NumericThresholdItem) As KineticaRT.Logic.Expressions.NumericExpression

        Dim newChannel As New KineticaRT.Logic.Expressions.NumericExpression

        newChannel.Name = channel.Name + "_MEAN"
        newChannel.Description = "Mean " + "_" + channel.Name
        newChannel.ToolTip = "Mean " + channel.Name
        newChannel.Text = "RollingMean(" + channel.Name + ", 5)"  '5 Secondes par DEFAUT

        newChannel.DecimalDigits = "3"
        newChannel.Format = "F3"
        newChannel.Maximum = 100
        newChannel.Minimum = 0

        Return newChannel

    End Function
    Public Function CreateChannelNumericExpressionMean(ByRef channel As DacnTemplateReal) As KineticaRT.Logic.Expressions.NumericExpression

        Dim newChannel As New KineticaRT.Logic.Expressions.NumericExpression

        Debug.Assert(Not IsNothing(channel.EngUnitID), "You must specify engineering unit on channel " + channel.Name)

        Dim name As String = CELProject.GetUnitNameById(CInt(channel.EngUnitID))
        newChannel.Name = channel.Name + "_" + name + "_MEAN"
        newChannel.Description = "Mean " + "_" + channel.Name + name
        newChannel.ToolTip = "Mean " + channel.Name + "_" + name
        newChannel.Text = "RollingMean(" + channel.Name + "_" + name + ", " + CELProject.EngineConfiguration.PerformanceAndLogAveragingPeriod.ToString() + ")"

        newChannel.DecimalDigits = "3"
        newChannel.Format = "F3"
        newChannel.Maximum = 100
        newChannel.Minimum = 0

        Return newChannel

    End Function

    Public Function CreateChanneNumericExpressionMeanForOpcNumericItem(ByVal collectionName As String, ByRef numericChannelCollection As Global.KineticaRT.ChannelCollection) As Boolean
        Dim collection As Global.KineticaRT.ChannelCollection = CELProject.GetObjectCallBack(collectionName)
        Try
            If Not IsNothing(collection) Then
                For Each channelEntry As System.Collections.DictionaryEntry In collection
                    If TypeOf (channelEntry.Value) Is Global.KineticaRT.Servers.OpcClient.Item Then
                        Dim opcNumericItem As Global.KineticaRT.Servers.OpcClient.Item = CType(channelEntry.Value, Global.KineticaRT.Servers.OpcClient.Item)
                        Dim numericExpression As KineticaRT.Logic.Expressions.NumericExpression
                        numericExpression = CreateChannelNumericExpressionMean(CType(opcNumericItem, KineticaRT.Servers.OpcClient.Item))
                        numericChannelCollection.Add(numericExpression)
                    ElseIf TypeOf (channelEntry.Value) Is Global.KineticaRT.Servers.OpcClient.NumericThresholdItem Then
                        Dim opcNumericItem As Global.KineticaRT.Servers.OpcClient.NumericThresholdItem = CType(channelEntry.Value, Global.KineticaRT.Servers.OpcClient.NumericThresholdItem)
                        Dim numericExpression As KineticaRT.Logic.Expressions.NumericExpression
                        numericExpression = CreateChannelNumericExpressionMean(CType(opcNumericItem, KineticaRT.Servers.OpcClient.NumericThresholdItem))
                        numericChannelCollection.Add(numericExpression)
                    End If
                Next
            End If
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

#End Region 'HMI Numeric Channels

#Region " HMI Text Channels "

    Public Enum eTextChannelType
        TextOpcItem
        TextLink
        TextChannel
    End Enum

    Public Function GetTypeOfChannel(ByRef channel As HMITextChannels) As eTextChannelType

        Select Case True

            Case (channel.OPCGroup <> "" AndAlso channel.OPCItemID <> "")
                Return eTextChannelType.TextOpcItem

            Case channel.Source <> "" AndAlso _
                 channel.Destination <> "" AndAlso _
                 channel.Source <> channel.Destination OrElse _
                 channel.Source = "" AndAlso _
                 channel.Destination <> ""
                Return eTextChannelType.TextLink

            Case (channel.Source <> "" AndAlso _
                 channel.Destination = "" OrElse _
                 (channel.Source <> "" AndAlso channel.Destination <> "" AndAlso channel.Source = channel.Destination)) AndAlso _
                 channel.Source <> "" AndAlso _
                 channel.OPCGroup = ""
                Return eTextChannelType.TextChannel

            Case Else
                If channel.Source = "" AndAlso _
                   channel.Destination = "" AndAlso
                   channel.OPCGroup = "" AndAlso _
                   channel.OPCItemID = "" Then

                    Return eTextChannelType.TextChannel 'TextLink  'Channel Flottant dynamique utilisé comme variable temporaire
                Else
                    Debug.Assert(False) 'Ne devrait pas se rendre ici
                End If

        End Select

        Return Nothing

    End Function

    Public Sub CreateChannelsEngineText(ByVal collectionName As String, ByRef EngineChannelCollection As KineticaRT.ChannelCollection)

        'Query Database
        'esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
        Dim textChannelsCollection As HMITextChannelsCollection = New HMITextChannelsCollection()
        textChannelsCollection.es.Connection.Name = CELProject.DefaultConnectionName
        textChannelsCollection.Query.Where(textChannelsCollection.Query.CollectionName = collectionName)
        textChannelsCollection.Query.Where(textChannelsCollection.Query.Unavailable = False)
        textChannelsCollection.Query.Load()

        For Each channel As HMITextChannels In textChannelsCollection

            Dim newChannel As Object = Nothing

            Select Case GetTypeOfChannel(channel)

                Case eTextChannelType.TextChannel
                    newChannel = CreateChannelText(channel)

                Case eTextChannelType.TextLink
                    newChannel = CreateChannelTextLink(channel)

                Case eTextChannelType.TextOpcItem
                    newChannel = CreateChannelTextOpcItem(channel)

                Case Else
                    Debug.Assert(False)

            End Select

            EngineChannelCollection.Add(newChannel)

        Next

    End Sub

    Public Function CreateChannelText(ByRef channel As HMITextChannels) As CEL.KineticaRT.Servers.Logger.TextChannel
        Dim newChannel As New CEL.KineticaRT.Servers.Logger.TextChannel

        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip

        newChannel.Source = channel.Source
        newChannel.Enabled = CELProject.DicArchiveChannelList.ContainsKey(channel.ChannelName)

        If newChannel.Enabled Then
            newChannel.Index = CELProject.DicArchiveChannelList(channel.ChannelName) 'Index
        Else
            newChannel.Index = 0
        End If
        newChannel.Logger = "ARCHIVE"
        newChannel.EventLogger = "EVENT_ARCHIVE"

        newChannel.Name = channel.ChannelName

        If Not IsNothing(channel.InitialValue) Then
            newChannel.Value = New KineticaRT.Values.Text(channel.InitialValue)
        End If

        Return newChannel

    End Function
    Public Function CreateChannelTextLink(ByRef channel As HMITextChannels) As CEL.KineticaRT.Servers.Logger.TextLink
        Dim newChannel As New CEL.KineticaRT.Servers.Logger.TextLink
        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.Destination = channel.Destination
        newChannel.Source = channel.Source
        newChannel.Enabled = CELProject.DicArchiveChannelList.ContainsKey(channel.ChannelName)

        If newChannel.Enabled Then
            newChannel.Index = CELProject.DicArchiveChannelList(channel.ChannelName) 'Index
        Else
            newChannel.Index = 0
        End If
        newChannel.Logger = "ARCHIVE"
        newChannel.EventLogger = "EVENT_ARCHIVE"

        newChannel.Name = channel.ChannelName

        If Not IsNothing(channel.InitialValue) Then
            newChannel.Value = New KineticaRT.Values.Text(channel.InitialValue)
        End If

        Return newChannel

    End Function

    Public Function CreateChannelTextOpcItem(ByRef channel As HMITextChannels) As KineticaRT.Servers.OpcClient.TextItem
        Dim newChannel As New KineticaRT.Servers.OpcClient.TextItem

        newChannel.Description = channel.Description
        newChannel.ToolTip = channel.Tooltip
        newChannel.ItemID = channel.OPCItemID
        newChannel.Group = channel.OPCGroup
        newChannel.Name = channel.ChannelName

        If Not IsNothing(channel.InitialValue) Then
            newChannel.Value = New KineticaRT.Values.Text(channel.InitialValue)
        End If

        Return newChannel

    End Function

#End Region 'HMI Text Channels

    Public Function CreateChannelsMessagesThreshold(ByVal collectionName As String, _
                                                     ByRef channelCollection As KineticaRT.ChannelCollection, _
                                                     ByRef folderName As String) _
                                                 As Boolean

        CreateChannelsMessagesThreshold = False

        Dim newChannel As KineticaRT.Logic.Message.Threshold
        Dim textLogChannel As KineticaRT.Logic.Message.TextLog

        Try

            'Ajoute le Channel TextLog pour les messages
            textLogChannel = channelCollection.FindObject("TextLog")
            If textLogChannel Is Nothing Then
                textLogChannel = New KineticaRT.Logic.Message.TextLog
                textLogChannel.Name = "TextLog"
                textLogChannel.Format = "G"
                textLogChannel.Source = "Messages"
                channelCollection.Add(textLogChannel)
            End If

            'TODO: rendre generique
            'Dim folderName As String = "C:\CELDAS DATA\" + CELProject.EngineModel + "\" + CELProject.Test_Mgr_UUT_Answer(0).Value.ToString() + "\" + CELProject.Test_Mgr_UUT_Answer(1).Value.ToString() + "\log\Messages.Log"
            textLogChannel.FilePath = folderName

            Dim numericChannelsCollection As HMINumericChannelsCollection = New HMINumericChannelsCollection()
            numericChannelsCollection.es.Connection.Name = CELProject.DefaultConnectionName
            numericChannelsCollection.Query.Where(numericChannelsCollection.Query.CollectionName = collectionName)
            numericChannelsCollection.Query.Where(numericChannelsCollection.Query.Unavailable = False)
            numericChannelsCollection.Query.Load()

            For Each channel As HMINumericChannels In numericChannelsCollection
                If channel.ThresholdHighHighMessageText <> "" Then

                    'Create Message Channels
                    newChannel = New KineticaRT.Logic.Message.Threshold

                    newChannel.Destination = "Messages"
                    If IsNothing(channel.ThresholdHighHigh) Then
                        newChannel.Level = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
                    Else
                        newChannel.Level = CDbl(channel.ThresholdHighHigh)
                    End If
                    If channel.SpeechMessageText = True Then
                        newChannel.Priority = 10
                    Else
                        newChannel.Priority = 0
                    End If
                    newChannel.TextColor = Color.FromName(channel.ThresholdHighHighColor)
                    newChannel.ReturnTextColor = Color.FromName(channel.ThresholdHighColor)

                    newChannel.ReturnText = channel.ThresholdHighMessageText
                    newChannel.Source = channel.ChannelName
                    newChannel.SourceText = channel.ChannelName
                    newChannel.ThresholdSource = channel.ThresholdHighHighSource
                    newChannel.Text = channel.ThresholdHighHighMessageText
                    newChannel.Name = channel.ChannelName + "_HighHighLimit"
                    newChannel.Value = New KineticaRT.Logic.Message.Message(channel.Description + " Has Reached the HighHigh Limit", collectionName + "." + channel.ChannelName, 10, newChannel.TextColor)

                    channelCollection.Add(newChannel)
                End If
                If channel.ThresholdHighMessageText <> "" Then

                    'Create Message Channels
                    newChannel = New KineticaRT.Logic.Message.Threshold

                    newChannel.Destination = "Messages"
                    If IsNothing(channel.ThresholdHigh) Then
                        newChannel.Level = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
                    Else
                        newChannel.Level = CDbl(channel.ThresholdHigh)
                    End If
                    If channel.SpeechMessageText = True Then
                        newChannel.Priority = 10
                    Else
                        newChannel.Priority = 0
                    End If
                    newChannel.TextColor = Color.FromName(channel.ThresholdHighColor)

                    newChannel.ReturnText = channel.Description + " is now OK"
                    newChannel.Source = channel.ChannelName
                    newChannel.SourceText = channel.ChannelName
                    newChannel.ThresholdSource = channel.ThresholdHighSource
                    newChannel.Text = channel.ThresholdHighMessageText
                    newChannel.Name = channel.ChannelName + "_HighLimit"
                    newChannel.Value = New KineticaRT.Logic.Message.Message(channel.Description + " Has Reached the High Limit", collectionName + "." + channel.ChannelName, 10, newChannel.TextColor)

                    channelCollection.Add(newChannel)
                End If
                If channel.ThresholdLowMessageText <> "" Then

                    'Create Message Channels
                    newChannel = New KineticaRT.Logic.Message.Threshold

                    newChannel.Destination = "Messages"
                    If IsNothing(channel.ThresholdLow) Then
                        newChannel.Level = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
                    Else
                        newChannel.Level = CDbl(channel.ThresholdLow)
                    End If
                    If channel.SpeechMessageText = True Then
                        newChannel.Priority = 10
                    Else
                        newChannel.Priority = 0
                    End If
                    newChannel.TextColor = Color.FromName(channel.ThresholdLowColor)
                    newChannel.ReturnTextColor = Color.FromName(channel.ThresholdLowColor)

                    newChannel.Text = channel.Description + " is now OK"
                    newChannel.Source = channel.ChannelName
                    newChannel.SourceText = channel.ChannelName
                    newChannel.ThresholdSource = channel.ThresholdLowSource
                    newChannel.ReturnText = channel.ThresholdLowMessageText
                    newChannel.Name = channel.ChannelName + "_LowLimit"
                    newChannel.Value = New KineticaRT.Logic.Message.Message(channel.Description + " Has Reached the Low Limit", collectionName + "." + channel.ChannelName, 10, newChannel.TextColor)

                    channelCollection.Add(newChannel)
                End If
                If channel.ThresholdLowLowMessageText <> "" Then

                    'Create Message Channels
                    newChannel = New KineticaRT.Logic.Message.Threshold

                    newChannel.Destination = "Messages"
                    If IsNothing(channel.ThresholdLowLow) Then
                        newChannel.Level = KineticaRT.Values.Values.PositiveInfinity.ToDouble()
                    Else
                        newChannel.Level = CDbl(channel.ThresholdLowLow)
                    End If
                    If channel.SpeechMessageText = True Then
                        newChannel.Priority = 10
                    Else
                        newChannel.Priority = 0
                    End If
		    newChannel.TextColor = Color.FromName(channel.ThresholdLowLowColor)
		    
                    newChannel.Text = channel.ThresholdLowMessageText
                    newChannel.Source = channel.ChannelName
                    newChannel.SourceText = channel.ChannelName
                    newChannel.ThresholdSource = channel.ThresholdLowLowSource
                    newChannel.ReturnText = channel.ThresholdLowLowMessageText
                    newChannel.Name = channel.ChannelName + "_LowLowLimit"
                    newChannel.Value = New KineticaRT.Logic.Message.Message(channel.Description + " Has Reached the LowLow Limit", collectionName + "." + channel.ChannelName, 10, newChannel.TextColor)

                    channelCollection.Add(newChannel)
                End If

            Next

            CreateChannelsMessagesThreshold = True

        Catch ex As Exception
            clsTopMostMessageBox.Show("OpenTest_Sequence.CreateChannelsMessagesThreshold: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Function


End Class

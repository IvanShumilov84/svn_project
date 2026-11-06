Option Strict Off

Imports System.Collections

Imports KineticaRT.Channels
Imports KineticaRT.Values

'
' Created by SharpDevelop.
' User: Martin Leduc
' Date: 7/12/2012
' Time: 2:09 PM
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
''' <summary>
'' Description of LogPerfSynch.
''' </summary>
Imports System
Imports System.Text
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports System.Diagnostics
Imports Microsoft.VisualBasic

Namespace CEL
    Public Interface ITransferFunct
        Sub SetValue()
    End Interface

    Public NotInheritable Class LogPerfSynch


        Private Shared _instance As LogPerfSynch
        'TODO ft Critical Le regex n'est plus valide: il faut ajouter _UNITE et P2_PSIA
        Private _InputOutputRegEx As String ' = "(P|T|VIN|VIB|F|VOUT|IIN|IOUT)\d+"

        Public Shared ReadOnly Property Instance() As LogPerfSynch
            Get
                If _instance Is Nothing Then
                    _instance = New LogPerfSynch()
                End If

                Return _instance
            End Get
        End Property
        Private _ChannelCollection As Global.KineticaRT.ChannelCollection
        Public Property ChannelCollection() As Global.KineticaRT.ChannelCollection
            Get
                Return _ChannelCollection
            End Get
            Set(ByVal value As Global.KineticaRT.ChannelCollection)
                _ChannelCollection = value
            End Set
        End Property


        '*************************
        '* Variables definitions *
        '*************************
        Private Class CLinkTransferFunct
            Implements ITransferFunct
            Public LogPerfChannel As IChannel
            Public SourceChannel As IChannel
            Public Sub SetValue() Implements ITransferFunct.SetValue
                LogPerfChannel.Value = SourceChannel.Value
            End Sub
        End Class
        Public ListOfChannels As New List(Of ITransferFunct)
        Public ListOfExpressionHandlers As New List(Of Global.KineticaRT.Channels.IInfoChannel)


        'Sequence Delegate / CallBack
        Public Delegate Function GetChannelFPtr(ByVal ChannelName As String) As Global.KineticaRT.Channels.Channel
        Public GetChannelCallBack As GetChannelFPtr

        Public Delegate Function GetObjectFPtr(ByVal ObjectName As String) As Global.KineticaRT.ChannelCollection
        Public GetObjectCallBack As GetObjectFPtr

        Public Sub SetCallBackFunctions(ByVal GetChannelCallBackPtr As GetChannelFPtr, GetObjectCallBackPtr As GetObjectFPtr)
            GetChannelCallBack = GetChannelCallBackPtr
            GetObjectCallBack = GetObjectCallBackPtr
        End Sub

        Public Sub SetInputOutputRegExString(ByVal regexString As String)
            _InputOutputRegEx = regexString
        End Sub


        Public Function AddCollection(ByVal collectionName As String, Optional ByVal skipMeanChannel As Boolean = False) As Boolean
            AddCollection = False

            Dim collection As Global.KineticaRT.ChannelCollection = Nothing
            collection = GetObjectCallBack(collectionName)
            If Not collection Is Nothing Then
                For Each channelEntry As System.Collections.DictionaryEntry In collection
                    Try
                        Dim channel As IChannel = CType(channelEntry.Value, IChannel)
                        If skipMeanChannel And channel.Name.EndsWith("_MEAN") Then
                            Continue For
                        End If
                       
                        Add(channel)
                        AddCollection = True
                    Catch ex As Exception
                        clsTopMostMessageBox.Show("LogPerfSynch.AddCollection: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Next
            Else
                'Debug.Assert(False, "LogPerfSynch.AddCollection" + collectionName + " Is Nothing")  
                'desactive le assert pcq il arrive dans certain cas qu on ne veux pas pupper derreur comme lorsque l on load le DAS CALIBRATION 
                'on n a pas besoin de la collection ARINC
            End If
        End Function
        Public Overloads Sub Add(ByRef Channel As IChannel)

            If TypeOf Channel Is CEL.KineticaRT.Servers.Logger.NumericThresholdChannel Then 'Le CEL NumericThresholdChannel a une propriete Source que celui de KineticaRT n'a pas
                Add(CType(Channel, CEL.KineticaRT.Servers.Logger.NumericThresholdChannel))
            ElseIf TypeOf Channel Is Global.KineticaRT.Channels.IBooleanExpressionChannel Then
                Add(CType(Channel, Global.KineticaRT.Channels.IBooleanExpressionChannel))
            ElseIf TypeOf Channel Is IBooleanChannel Then
                Add(CType(Channel, IBooleanChannel))
            ElseIf TypeOf Channel Is Global.KineticaRT.Channels.INumericExpressionChannel Then
                Add(CType(Channel, Global.KineticaRT.Channels.INumericExpressionChannel))
            ElseIf TypeOf Channel Is Global.KineticaRT.Channels.INumericLinkChannel Then
                Add(CType(Channel, Global.KineticaRT.Channels.INumericLinkChannel))
            ElseIf TypeOf Channel Is Global.KineticaRT.Channels.ITextExpressionChannel Then
                Add(CType(Channel, Global.KineticaRT.Channels.ITextExpressionChannel))
            ElseIf TypeOf Channel Is ITextChannel Then
                Add(CType(Channel, ITextChannel))
            ElseIf TypeOf Channel Is Global.KineticaRT.Servers.OpcClient.Item Then
                'Add(CType(Channel, Global.KineticaRT.Servers.OpcClient.Item))
                Exit Sub 'On skip les Item numerique raw, on ne conserve que les _MEAN ( les valeurs moyennes sont le point d'entrée dans l'arboressence de LOG_PERF_SYHCH (pour tous les I/O  Numeriques)
            ElseIf TypeOf Channel Is Global.KineticaRT.Servers.OpcClient.NumericThresholdItem Then
                'Add(CType(Channel, Global.KineticaRT.Servers.OpcClient.Item))
                Exit Sub 'On skip les Item numerique raw, on ne conserve que les _MEAN ( les valeurs moyennes sont le point d'entrée dans l'arboressence de LOG_PERF_SYHCH (pour tous les I/O  Numeriques)
            ElseIf TypeOf Channel Is Global.KineticaRT.Servers.OpcClient.BooleanItem Then
                Add(CType(Channel, Global.KineticaRT.Servers.OpcClient.BooleanItem))
            ElseIf TypeOf Channel Is Global.KineticaRT.Servers.OpcClient.NumericThresholdItem Then
                Add(CType(Channel, Global.KineticaRT.Servers.OpcClient.NumericThresholdItem))
            ElseIf TypeOf Channel Is Global.KineticaRT.Servers.OpcClient.TextItem Then
                Add(CType(Channel, Global.KineticaRT.Servers.OpcClient.TextItem))
            ElseIf TypeOf Channel Is Global.KineticaRT.Channels.VariableChannel Then
                'Do Nothing
            ElseIf TypeOf Channel Is Global.KineticaRT.Logic.Message.Message Then
                'DoNothing
            ElseIf TypeOf Channel Is Global.KineticaRT.Logic.Message.Event Then
                'Do Nothing
            ElseIf TypeOf Channel Is Global.KineticaRT.Logic.Message.TextLog Then
                'Do Nothing
            ElseIf TypeOf Channel Is Global.KineticaRT.Channels.Channel Then 'Prend en charge le VariableChannel
                Add(CType(Channel, Global.KineticaRT.Channels.Channel))
            Else
                System.Windows.Forms.MessageBox.Show("LogperfSynch.Add: Cannot Find Type " + Channel.GetType().ToString())
            End If

        End Sub

        Public Overloads Sub Add(ByRef Channel As Global.KineticaRT.Channels.Channel) 'Inclus VariableChannel

            Dim TransferFunct As New CLinkTransferFunct
            Dim LogPerfChannel As New Global.KineticaRT.Channels.NumericChannel

            LogPerfChannel.DecimalDigits = 2
            LogPerfChannel.Format = "F2"

            TransferFunct.LogPerfChannel = LogPerfChannel
            TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")
            TransferFunct.LogPerfChannel.Value = New Global.KineticaRT.Values.Double(Double.NaN)
            TransferFunct.SourceChannel = Channel
            ListOfChannels.Add(TransferFunct)

            _ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))

        End Sub
        Public Overloads Sub Add(ByRef Channel As IBooleanChannel)

            Dim TransferFunct As New CLinkTransferFunct

            TransferFunct.LogPerfChannel = New Global.KineticaRT.Channels.BooleanChannel
            TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")
            TransferFunct.LogPerfChannel.Value = Values.False
            TransferFunct.SourceChannel = Channel
            ListOfChannels.Add(TransferFunct)

            _ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))

        End Sub
        Public Overloads Sub Add(ByRef Channel As Global.KineticaRT.Channels.IBooleanExpressionChannel)

            'Dim TransferFunct As New CLinkTransferFunct

            'TransferFunct.LogPerfChannel = New Global.KineticaRT.Channels.BooleanChannel
            'TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")
            'TransferFunct.LogPerfChannel.Value = Values.False
            'TransferFunct.SourceChannel = Channel
            'ListOfChannels.Add(TransferFunct)

            '_ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))
            Dim LogPerfChannel As New Global.KineticaRT.Logic.Expressions.Expression

            LogPerfChannel.Text = UpdateExpression(Channel.Text.Replace("_MEAN", ""))

            If LogPerfChannel.Text = "" Then
                LogPerfChannel.Text = Channel.Name.Replace("_MEAN", "")
            End If
            LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")

            AddExpressionChangeHandler(Channel)
            _ChannelCollection.Add(LogPerfChannel)

        End Sub
        Public Overloads Sub Add(ByRef Channel As Global.KineticaRT.Channels.INumericExpressionChannel)
            Dim TransferFunct As New CLinkTransferFunct

            If Channel.Name.EndsWith("_MEAN") Then

                Dim LogPerfChannel As New Global.KineticaRT.Channels.NumericChannel

                LogPerfChannel.DecimalDigits = Channel.DecimalDigits
                LogPerfChannel.Format = Channel.Format
                LogPerfChannel.Units = Channel.Units

                TransferFunct.LogPerfChannel = LogPerfChannel
                TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")
                TransferFunct.LogPerfChannel.Value = New Global.KineticaRT.Values.Double(Double.NaN)
                TransferFunct.SourceChannel = Channel
                ListOfChannels.Add(TransferFunct)

                _ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))
            Else
                Dim LogPerfChannel As New Global.KineticaRT.Logic.Expressions.NumericExpression

                LogPerfChannel.DecimalDigits = Channel.DecimalDigits
                LogPerfChannel.Format = Channel.Format
                LogPerfChannel.Units = Channel.Units

                LogPerfChannel.Text = UpdateExpression(Channel.Text.Replace("_MEAN", ""))

                If LogPerfChannel.Text = "" Then
                    LogPerfChannel.Text = Channel.Name.Replace("_MEAN", "")
                End If
                LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")

                AddExpressionChangeHandler(Channel)
                _ChannelCollection.Add(LogPerfChannel)
            End If
        End Sub
        'Public Overloads Sub Add(ByRef Channel As INumericChannel)
        Public Overloads Sub Add(ByRef Channel As Global.KineticaRT.Channels.INumericLinkChannel)
            Try
                If Not IsNothing(Channel.Source) AndAlso Channel.Source & "" <> "" AndAlso (Regex.IsMatch(Channel.Source, _InputOutputRegEx, Me.GetRegexOptions())) Then

                    '"The regex matches part or all of the subject"
                    Dim result As String = Regex.Match(Channel.Source, _InputOutputRegEx, Me.GetRegexOptions()).Value

                    If result = Channel.Source.Trim() Then

                        Dim TransferFunct As New CLinkTransferFunct
                        Dim LogPerfChannel As New Global.KineticaRT.Channels.NumericChannel

                        LogPerfChannel.DecimalDigits = Channel.DecimalDigits
                        LogPerfChannel.Format = Channel.Format
                        LogPerfChannel.Units = Channel.Units

                        TransferFunct.LogPerfChannel = LogPerfChannel
                        TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")
                        TransferFunct.LogPerfChannel.Value = New Global.KineticaRT.Values.Double(Double.NaN)
                        TransferFunct.SourceChannel = Me.GetChannelCallBack(result + "_MEAN") 'Les Numeric Inputs_MEAN ex. P2_MEAN ou T43_MEAN
                        ListOfChannels.Add(TransferFunct)

                        _ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))

                    Else

                        Dim LogPerfChannel As New Global.KineticaRT.Logic.Expressions.NumericExpression 'TODO: Ici pour vraiment garder l'intégrité des  données il faut garder en LINK pour setter la destionation

                        LogPerfChannel.Text = "LOG_PERF_" + Channel.Source.Replace("_MEAN", "")

                        If LogPerfChannel.Text = "" Then
                            LogPerfChannel.Text = Channel.Name.Replace("_MEAN", "")
                        End If
                        LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")

                        _ChannelCollection.Add(LogPerfChannel)

                    End If

                Else 'Pour les channels flottant sans source et destination.  Ces variables sont normalement WB_CALC et setté par le Code VB de la séquence.
                    '"The regex cannot be matched in the subject"

                    Dim TransferFunct As New CLinkTransferFunct
                    Dim LogPerfChannel As New Global.KineticaRT.Channels.NumericChannel()

                    LogPerfChannel.DecimalDigits = Channel.DecimalDigits
                    LogPerfChannel.Format = Channel.Format
                    LogPerfChannel.Units = Channel.Units

                    TransferFunct.LogPerfChannel = LogPerfChannel
                    TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")
                    TransferFunct.LogPerfChannel.Value = New Global.KineticaRT.Values.Double(Double.NaN)
                    TransferFunct.SourceChannel = Me.GetChannelCallBack(Channel.Name + "_MEAN")
                    If TransferFunct.SourceChannel Is Nothing Then
                        TransferFunct.SourceChannel = Channel
                    End If
                    ListOfChannels.Add(TransferFunct)

                    _ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))

                End If
            Catch ex As Exception

                ' Most likely cause is a syntax error in the regular expression
                System.Diagnostics.Debug.WriteLine("CEL.LogPerfSynch.Add.Regex.IsMatch() threw an exception:\r\n" + ex.Message)

            End Try
        End Sub
        Public Overloads Sub Add(ByRef Channel As ITextChannel)

            Dim LogPerfChannel As New Global.KineticaRT.Channels.TextChannel
            Dim TransferFunct As New CLinkTransferFunct

            TransferFunct.LogPerfChannel = LogPerfChannel
            TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name
            TransferFunct.LogPerfChannel.Value = New Global.KineticaRT.Values.Text("")
            TransferFunct.SourceChannel = Channel
            ListOfChannels.Add(TransferFunct)

            _ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))

        End Sub
        Public Overloads Sub Add(ByRef Channel As CEL.KineticaRT.Servers.Logger.NumericThresholdChannel) 'On ne peut pas utiliser le INumericChannel, pcq On utilise la propriété Source non défini dans l'interface

            If Channel.Source & "" <> "" Then

                Dim LogPerfChannel As New Global.KineticaRT.Logic.Expressions.NumericExpression

                LogPerfChannel.DecimalDigits = Channel.DecimalDigits
                LogPerfChannel.Format = Channel.Format
                LogPerfChannel.Units = Channel.Units

                LogPerfChannel.Text = "LOG_PERF_" + Channel.Source.Replace("_MEAN", "")

                If LogPerfChannel.Text = "" Then
                    LogPerfChannel.Text = Channel.Name.Replace("_MEAN", "")
                End If
                LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")

                _ChannelCollection.Add(LogPerfChannel)
            Else
                Dim TransferFunct As New CLinkTransferFunct
                Dim LogPerfChannel As New Global.KineticaRT.Channels.NumericChannel

                LogPerfChannel.DecimalDigits = Channel.DecimalDigits
                LogPerfChannel.Format = Channel.Format
                LogPerfChannel.Units = Channel.Units

                TransferFunct.LogPerfChannel = LogPerfChannel
                TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")
                TransferFunct.LogPerfChannel.Value = New Global.KineticaRT.Values.Double(Double.NaN)
                TransferFunct.SourceChannel = Channel
                ListOfChannels.Add(TransferFunct)

                _ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))
            End If
        End Sub
    
        Public Overloads Sub Add(ByRef Channel As Global.KineticaRT.Servers.OpcClient.TextItem)

            Dim LogPerfChannel As New Global.KineticaRT.Channels.TextChannel
            Dim TransferFunct As New CLinkTransferFunct

            TransferFunct.LogPerfChannel = LogPerfChannel
            TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")
            TransferFunct.LogPerfChannel.Value = New Global.KineticaRT.Values.Double(Double.NaN)
            TransferFunct.SourceChannel = Channel
            ListOfChannels.Add(TransferFunct)

            _ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))

        End Sub
   
        Public Overloads Sub Add(ByRef Channel As Global.KineticaRT.Servers.OpcClient.BooleanItem)

            Dim TransferFunct As New CLinkTransferFunct

            TransferFunct.LogPerfChannel = New Global.KineticaRT.Channels.BooleanChannel
            TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name
            TransferFunct.LogPerfChannel.Value = Values.False
            TransferFunct.SourceChannel = Channel
            ListOfChannels.Add(TransferFunct)

            _ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))

        End Sub
        Public Overloads Sub Add(ByRef Channel As Global.KineticaRT.Servers.OpcClient.NumericThresholdItem)

            Dim LogPerfChannel As New Global.KineticaRT.Channels.NumericChannel
            Dim TransferFunct As New CLinkTransferFunct

            LogPerfChannel.DecimalDigits = Channel.DecimalDigits
            LogPerfChannel.Format = Channel.Format
            LogPerfChannel.Units = Channel.Units

            TransferFunct.LogPerfChannel = LogPerfChannel
            TransferFunct.LogPerfChannel.Name = "LOG_PERF_" + Channel.Name.Replace("_MEAN", "")
            TransferFunct.LogPerfChannel.Value = New Global.KineticaRT.Values.Double(Double.NaN)
            TransferFunct.SourceChannel = Channel
            ListOfChannels.Add(TransferFunct)

            _ChannelCollection.Add(CType(TransferFunct.LogPerfChannel, Global.KineticaRT.Object))

        End Sub

        Private Function UpdateExpression(ByVal text As String) As String
            Dim EndsWithWord As Boolean
            Dim i As Integer
            Dim index As Integer
            Dim KineticaRTReserved() As String = {"NOT", "ABS", "COS", "ACOS", "EXP", "FLOOR", "LIMIT", "LOG", _
                       "MAX", "MEAN", "MIN", "POW", "SIN", "ASIN", "SQRT", "TAN", _
                       "IF", "CHOOSE", "ATAN", "FILTER", "TRUE", "FALSE", _
                       "ROLLINGMEAN", "ROLLINGSD", "MATRIX", "AND", _
                       "TEST_MGR_MODE", "ARCHIVE_RECORDING_ON_SLAVE_OPC", "ARCHIVE_RECORDING_ON_MASTER_OPC", _
                       "OR", "XOR", "NAN", "PULSE", "PULSE2", "ARCHIVE_RECORD_BTN", "ROLLINGMIN", "ROLLINGMAX"} 'ft ajout de PULSE, PPB ajout de ARCHIVE_RECORD et PULSE2
            Dim modified As Boolean
            Dim UpdatedText As String = ""
            Dim word As String = ""

            'If we have an empty, text, return an empty string
            If text.Length = 0 Then
                Return UpdatedText
            End If

            While index < text.Length - 1
                'If we are at the beginning of a word
                If Char.IsLetter(CType(text.Substring(index, 1), Char)) Then
                    word = text.Substring(index, 1)
                    index += 1
                    'Find the remaining of the word
                    While Char.IsLetterOrDigit(CType(text.Substring(index, 1), Char)) Or text.Substring(index, 1) = "_"
                        word += text.Substring(index, 1)
                        index += 1
                        If index > text.Length - 1 Then
                            Exit While
                        End If
                    End While

                    'At the end of the word, we advanced once too much
                    index -= 1

                    'Look if the word is a KineticaRT reserved word
                    For i = 0 To KineticaRTReserved.Length - 1
                        modified = False
                        If word.ToUpper.Equals(KineticaRTReserved(i)) Then
                            'We have a reserved word to be left as is
                            If word.ToUpper = "PULSE" Then 'Substitution du channel PULSE par True
                                word = "True"
                            End If
                            UpdatedText += word
                            modified = True
                            i = KineticaRTReserved.Length
                        End If
                    Next
                    'If not, it is a Channel name to be modified
                    If Not modified Then
                        UpdatedText += "LOG_PERF_" + word
                    End If
                    'If we have the beginning of a quote
                ElseIf text.Substring(index, 1) = """" Then
                    word = text.Substring(index, 1)
                    index += 1
                    'Find the remaining of the quote
                    While text.Substring(index, 1) <> """"
                        word += text.Substring(index, 1)
                        index += 1
                        If index > text.Length - 1 Then
                            Exit While
                        End If
                    End While
                    'As soon as we find the other ", we add the quote and the closing "
                    UpdatedText += word + text.Substring(index, 1)
                Else
                    UpdatedText += text.Substring(index, 1)
                End If
                If index < text.Length - 1 Then
                    index += 1
                    EndsWithWord = False
                Else
                    EndsWithWord = True
                End If
            End While
            If Not EndsWithWord Then
                UpdatedText += text.Substring(index, 1)
            End If
            Return UpdatedText.Replace("_MEAN", "")
        End Function

        Public Function UpdateValues() As Boolean
            UpdateValues = False
            For Each obj As ITransferFunct In ListOfChannels
                obj.SetValue()
            Next
            'Application.DoEvents()
            UpdateValues = True
        End Function
        Public Sub OnExpressionChannelPropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
            'Cette fonction re-update l'expression dans le cas ou le code vb change les expressions.  Ca arrive lorsque l'on delete des probes sur les T_INLET, T_EGT , T_BLEED ...
            If e.PropertyName = "Text" Then

                Dim channel As Global.KineticaRT.Channels.IExpressionChannel = CType(sender, Global.KineticaRT.Channels.IExpressionChannel)

                If Not channel Is Nothing Then

                    CType(_ChannelCollection.Item("LOG_PERF_" + channel.Name), Global.KineticaRT.Channels.IExpressionChannel).Text = UpdateExpression(channel.Text)
                    CType(_ChannelCollection.Item("LOG_PERF_" + channel.Name), Global.KineticaRT.Channels.IExpressionChannel).Disconnect()
                    CType(_ChannelCollection.Item("LOG_PERF_" + channel.Name), Global.KineticaRT.Channels.IExpressionChannel).Connect()
                End If

            End If
        End Sub
        Public Sub AddExpressionChangeHandler(ByVal Channel As Global.KineticaRT.Channels.IInfoChannel)

            AddHandler Channel.PropertyChanged, AddressOf OnExpressionChannelPropertyChanged
            ListOfExpressionHandlers.Add(Channel)

        End Sub
        Public Sub Clear()
            'Dim counter As Integer = 0 'Decommenter cette ligne pour arreter le debugger... VS2005 buzzz
            Try

                For Each channel As Global.KineticaRT.Channels.IInfoChannel In ListOfExpressionHandlers
                    RemoveHandler channel.PropertyChanged, AddressOf OnExpressionChannelPropertyChanged
                    'counter = counter + 1  'Decommenter cette ligne pour arreter le debugger sur le RemoveHandler ici haut... VS2005 buzzz
                Next
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show(ex.ToString())

            End Try
            ListOfExpressionHandlers.Clear()
            ListOfChannels.Clear()

        End Sub

        Private Function GetRegexOptions() As System.Text.RegularExpressions.RegexOptions

            Dim DotAll As Boolean = False
            Dim MultiLine As Boolean = False
            Dim IgnoreCase As Boolean = False
            Dim ECMAScript As Boolean = False

            ' "Dot all" and "ECMAScript" are mutually exclusive options.
            ' If we include them both, then the Regex() constructor or the 
            ' Regex.Match() method will raise an exception


            'System.Diagnostics.Trace.Assert(!(checkDotAll.Checked && checkECMAScript.Checked), 
            '       "DotAll and ECMAScript options are mutually exclusive")


            ' Construct a RegexOptions object
            ' If the options are predetermined, you don't need to use a RegexOptions object
            ' You can simply pass something like RegexOptions.Multiline | RegexOptions.Ignorecase
            ' directly to the Regex() constructor or the Regex.Match() method
            Dim options As RegexOptions = New RegexOptions()
            ' If true, the dot matches any character, including a newline
            ' If false, the dot matches any character, except a newline
            If (DotAll) Then
                options = options Or RegexOptions.Singleline
            End If
            ' If true, the caret ^ matches after a newline, and the dollar $ matches 
            ' before a newline, as well as at the start and end of the subject string
            ' If false, the caret only matches at the start of the string
            ' and the dollar only at the end of the string
            If (MultiLine) Then
                options = options Or RegexOptions.Multiline
            End If

            ' If true, literal characters and character classes are matched case insensitively
            If (IgnoreCase) Then
                options = options Or RegexOptions.IgnoreCase
            End If
            ' If true, \w, \d and \s match ASCII characters only,
            ' and \10 is backreference 1 followed by a literal 0 rather than octal escape 10.
            If (ECMAScript) Then
                options = options Or RegexOptions.ECMAScript
            End If

            Return options
        End Function

    End Class
End Namespace

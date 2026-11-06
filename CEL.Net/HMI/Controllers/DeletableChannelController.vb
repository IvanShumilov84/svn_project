Imports System
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.HMI


    Public Class DeletableChannelController

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _DeletedChannels As IBooleanChannel()
        Private _NumberMaxDeleteProbeAllowed As Integer
        Private _NumberOfProbesAvailable As Integer
        Private _NumberTotalOfProbes As Integer
        Private _ChannelName As String() = Nothing
        Private _MinChannelName As String = Nothing
        Private _MaxChannelName As String = Nothing
        Private _MinChannel As Global.KineticaRT.Channels.IExpressionChannel 'CEL.KineticaRT.Servers.Logger.NumericThresholdExpression = Nothing
        Private _MaxChannel As Global.KineticaRT.Channels.IExpressionChannel 'CEL.KineticaRT.Servers.Logger.NumericThresholdExpression = Nothing
        Private _AvgChannel As Global.KineticaRT.Channels.IExpressionChannel 'CEL.KineticaRT.Servers.Logger.NumericThresholdExpression = Nothing
        Private _SpreadChannel As Global.KineticaRT.Channels.IExpressionChannel 'CEL.KineticaRT.Servers.Logger.NumericThresholdExpression = Nothing



        Public Sub New(ByRef Sequence As CEL.KineticaRT.Sequence.Sequence, _
                       ByVal NumberOfProbesAvailable As Integer, _
                        ByVal NumberTotalOfProbes As Integer, _
                        ByVal NumberMaxDeleteProbeAllowed As Integer, _
                        ByVal DefaultChannelName As String, _
                        ByVal ChannelSuffix As String, _
                        ByVal ChannelDeleteSuffix As String, _
                        ByVal AvgChannelName As String, _
                         ByVal MinChannelName As String, _
                         ByVal MaxChannelName As String, _
                         ByVal SpreadChannelName As String)

            ReDim _DeletedChannels(NumberTotalOfProbes - 1) 'Resize of array Note:we need to put -1 because this is the last element in the array not the number of element!!

            ReDim _ChannelName(NumberTotalOfProbes - 1) 'Resize of array Note:we need to put -1 because this is the last element in the array not the number of element!!

            _NumberMaxDeleteProbeAllowed = NumberMaxDeleteProbeAllowed
            _NumberOfProbesAvailable = NumberOfProbesAvailable
            _NumberTotalOfProbes = NumberTotalOfProbes

            If _ChannelName.GetUpperBound(0) = 0 Then
                clsTopMostMessageBox.Show("You cannot use a DeletableChannelController for only one input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            For i As Integer = 0 To _ChannelName.GetUpperBound(0)
                _ChannelName(i) = DefaultChannelName + (i + 1).ToString() + CType(IIf(ChannelSuffix Is Nothing, "", ChannelSuffix), String)
            Next

            If Not (MinChannelName Is Nothing) Then
                _MinChannel = CType(Sequence.GetChannel(MinChannelName), Global.KineticaRT.Channels.IExpressionChannel)
                _MinChannelName = MinChannelName
                If IsNothing(MinChannelName) Then
                    clsTopMostMessageBox.Show("The MaxChannelName " + MinChannelName + " not found in collection for controller DeletableChannelController ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

            If Not (MaxChannelName Is Nothing) Then
                _MaxChannel = CType(Sequence.GetChannel(MaxChannelName), Global.KineticaRT.Channels.IExpressionChannel)
                _MaxChannelName = MaxChannelName
                If IsNothing(MaxChannelName) Then
                    clsTopMostMessageBox.Show("The MaxChannelName " + MaxChannelName + " not found in collection for controller DeletableChannelController ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

            If Not (AvgChannelName Is Nothing) Then
                _AvgChannel = CType(Sequence.GetChannel(AvgChannelName), Global.KineticaRT.Channels.IExpressionChannel)
                If IsNothing(_AvgChannel) Then
                    clsTopMostMessageBox.Show("The AvgChannelName " + AvgChannelName + " not found in collection for controller DeletableChannelController ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

            If Not (SpreadChannelName Is Nothing) Then
                _SpreadChannel = CType(Sequence.GetChannel(SpreadChannelName), Global.KineticaRT.Channels.IExpressionChannel)
                If IsNothing(_SpreadChannel) Then
                    clsTopMostMessageBox.Show("The SpreadChannelName " + SpreadChannelName + " not found in collection for controller DeletableChannelController ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

            For i As Integer = 0 To _DeletedChannels.GetUpperBound(0)
                _DeletedChannels(i) = CType(Sequence.GetChannel(DefaultChannelName + (i + 1).ToString() + ChannelDeleteSuffix), IBooleanChannel)
            Next
            For i As Integer = NumberOfProbesAvailable To _DeletedChannels.GetUpperBound(0)
                _DeletedChannels(i).Value = Values.True 'Dans certains cas un moteur peut aller jusqu'a 10 probres, mais le dress kit n'a que 3 probes, alors desactive les 7 autres
            Next
            UpdateDeletedChannels()

            Me.AddHandlers()

        End Sub

        Private Sub AddHandlers()

            For i As Integer = 0 To _DeletedChannels.GetUpperBound(0)
                AddHandler _DeletedChannels(i).ValueChanged, AddressOf OnDeletedChannel
            Next

        End Sub

        Private Sub RemoveHandlers()

            If Not (_DeletedChannels(0) Is Nothing) Then
                For i As Integer = 0 To _DeletedChannels.GetUpperBound(0)
                    RemoveHandler _DeletedChannels(i).ValueChanged, AddressOf OnDeletedChannel
                Next
            End If

        End Sub


        Private Sub OnDeletedChannel(ByVal sender As IChannel, ByVal newVal As Value)
            UpdateDeletedChannels()
        End Sub
        Public Sub UpdateDeletedChannels()

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            Const NumberMaxOfItem As Integer = 10

            Dim Avg_Text As String = Nothing
            Dim Spread_Text As String = Nothing
            Dim MaxText As String = Nothing
            Dim MinText As String = Nothing

            Dim Count As Integer = 0
            Dim NumberProbe As Integer = _DeletedChannels.GetUpperBound(0) + 1
            Dim i As Integer

            Try

                For i = 0 To _DeletedChannels.GetUpperBound(0)
                    If CELProject.IsFalse(_DeletedChannels(i).Value) Then
                        Count = Count + 1
                        If Count = 1 Then
                            Avg_Text = "(" + _ChannelName(i)
                        Else
                            Avg_Text = Avg_Text + " + " + _ChannelName(i)
                        End If
                    End If
                Next

                If Count = 0 Then
                    Avg_Text = """NaN"""
                Else
                    Avg_Text = Avg_Text + ")/" + Count.ToString()
                End If

                If Not (_AvgChannel Is Nothing) Then
                    _AvgChannel.Text = Avg_Text
                End If

                If (NumberProbe - Count) > (_NumberMaxDeleteProbeAllowed + (_NumberTotalOfProbes - _NumberOfProbesAvailable)) Then
                    CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Number of deleted probe is exceeded", ToolTipIcon.Info)
                End If

                Dim NumElementInMinMax As Integer = Count
                Count = 0

                'the command Max() and Min() take a maximum of 10 parameters
                'We have to do Min( Min(), Min())

                For i = 0 To _DeletedChannels.GetUpperBound(0)
                    If CELProject.IsFalse(_DeletedChannels(i).Value) Then
                        Count = Count + 1
                        If Count = 1 Then ' First element to be added
                            If NumElementInMinMax > NumberMaxOfItem Then
                                MaxText = "Max( Max(" + _ChannelName(i)
                                MinText = "Min( Min(" + _ChannelName(i)
                            Else
                                MaxText = "Max(" + _ChannelName(i)
                                MinText = "Min(" + _ChannelName(i)
                            End If

                        ElseIf (Count Mod NumberMaxOfItem) = 1 Then ' find the next item following a multiple value of 10: 10, 20, 30...
                            MaxText = MaxText + "), Max(" + _ChannelName(i)
                            MinText = MinText + "), Min(" + _ChannelName(i)
                        Else
                            MaxText = MaxText + ", " + _ChannelName(i)
                            MinText = MinText + ", " + _ChannelName(i)
                        End If
                    End If
                Next

                If Count = 0 Then
                    MaxText = """NaN"""
                    MinText = """NaN"""
                ElseIf NumElementInMinMax > NumberMaxOfItem Then
                    MaxText = MaxText + "))"
                    MinText = MinText + "))"
                Else
                    MaxText = MaxText + ")"
                    MinText = MinText + ")"
                End If

                If Count = 0 Then
                    Spread_Text = """NaN"""
                ElseIf Count = 1 Then
                    Spread_Text = "0.0"
                Else
                    If (_MaxChannel Is Nothing) Or (_MinChannel Is Nothing) Then
                        Spread_Text = "Abs( " + MaxText + " - " + MinText + " )"
                    Else
                        Spread_Text = "Abs( " + _MaxChannelName + " - " + _MinChannelName + " )"
                    End If
                End If

                If Not (_MaxChannel Is Nothing) Then
                    _MaxChannel.Text = MaxText
                End If

                If Not (_MinChannel Is Nothing) Then
                    _MinChannel.Text = MinText
                End If

                If Not (_SpreadChannel Is Nothing) Then
                    _SpreadChannel.Text = Spread_Text
                End If

            Catch Ex As Exception
                clsTopMostMessageBox.Show("UpdateDeletedChannels: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
            End Try
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called
                    RemoveHandlers()
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements System.IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace
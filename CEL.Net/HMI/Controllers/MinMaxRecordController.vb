Imports System
Imports System.Windows.Forms
Imports KineticaRT.Channels
Imports KineticaRT.Values


Namespace CEL.HMI

    Public Class MinMaxRecordController 'Used to record Min or Max for Current, Voltage (Starter)
        Implements IDisposable

        Public Enum eRecordType
            Minimum = 1
            Maximum = 2
        End Enum

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _RecordType As eRecordType = Nothing
        Private _SourceChannel As INumericChannel = Nothing
        Private _DestinationChannel As INumericChannel = Nothing
        Private _ResetSourceChannel As IBooleanChannel = Nothing
        Private _StopSourceChannelName As IBooleanChannel = Nothing

        Public Sub New(ByRef Sequence As CEL.KineticaRT.Sequence.Sequence, _
                       ByVal RecordType As eRecordType, _
                       ByVal SourceChannelName As String, _
                       ByVal DestinationChannelName As String, _
                       ByVal ResetSourceChannelName As String, _
                       Optional ByVal StopSourceChannelName As String = "")

            _RecordType = RecordType

            _SourceChannel = CType(Sequence.GetChannel(SourceChannelName), INumericChannel)

            If _SourceChannel Is Nothing Then
                clsTopMostMessageBox.Show("MinMaxRecordController, The channel passed as SourceChannel does not exist: " & SourceChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            _DestinationChannel = CType(Sequence.GetChannel(DestinationChannelName), INumericChannel)

            If _DestinationChannel Is Nothing Then
                clsTopMostMessageBox.Show("MinMaxRecordController, The channel passed as DestinationChannel does not exist: " & DestinationChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            _ResetSourceChannel = CType(Sequence.GetChannel(ResetSourceChannelName), IBooleanChannel)

            If _ResetSourceChannel Is Nothing Then
                clsTopMostMessageBox.Show("MinMaxRecordController, The channel passed as ResetChannel does not exist: " & ResetSourceChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            If StopSourceChannelName <> "" Then
                _StopSourceChannelName = CType(Sequence.GetChannel(StopSourceChannelName), IBooleanChannel)
            End If

            Dim StopCheckValue As Boolean = False

            If Not (_StopSourceChannelName Is Nothing) Then
                StopCheckValue = CELProject.IsTrue(_StopSourceChannelName.Value)
            End If

            If StopCheckValue = False Then
                _DestinationChannel.Value = _SourceChannel.Value
            Else
                _DestinationChannel.Value = Global.KineticaRT.Values.Values.Zero
            End If

            Me.AddHandlers()

        End Sub

        Private Sub AddHandlers()
            AddHandler _SourceChannel.ValueChanged, AddressOf OnSourceValueChanged
            AddHandler _ResetSourceChannel.ValueChanged, AddressOf OnResetDestination
        End Sub

        Private Sub RemoveHandlers()
            RemoveHandler _SourceChannel.ValueChanged, AddressOf OnSourceValueChanged
            RemoveHandler _ResetSourceChannel.ValueChanged, AddressOf OnResetDestination
        End Sub
        Private Sub OnSourceValueChanged(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            Dim StopCheckValue As Boolean = False

            If Not (_StopSourceChannelName Is Nothing) Then
                StopCheckValue = CELProject.IsTrue(_StopSourceChannelName.Value)
            End If

            Select Case _RecordType
                Case eRecordType.Minimum
                    If (_SourceChannel.Value.ToDouble() < _DestinationChannel.Value.ToDouble() And StopCheckValue = False) Then
                        _DestinationChannel.Value = _SourceChannel.Value
                    End If
                Case eRecordType.Maximum
                    If (_SourceChannel.Value.ToDouble() > _DestinationChannel.Value.ToDouble() And StopCheckValue = False) Then
                        _DestinationChannel.Value = _SourceChannel.Value
                    End If
            End Select
        End Sub
        Private Sub OnResetDestination(ByVal sender As IChannel, ByVal newVal As Value)
            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            Dim StopCheckValue As Boolean = False

            If Not (_StopSourceChannelName Is Nothing) Then
                StopCheckValue = CELProject.IsTrue(_StopSourceChannelName.Value)
            End If

            If CELProject.IsTrue(newVal) Then
                If StopCheckValue = False Then
                    _DestinationChannel.Value = _SourceChannel.Value
                Else
                    _DestinationChannel.Value = Global.KineticaRT.Values.Values.Zero
                End If
            End If
        End Sub
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called
                    Me.RemoveHandlers()
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class



End Namespace
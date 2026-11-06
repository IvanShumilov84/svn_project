Imports System
Imports System.Windows.Forms
Imports System.Timers
Imports KineticaRT.Channels
Imports KineticaRT.Values

Imports System.Threading


Namespace CEL.HMI

    Public Class WatchdogController

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _Sequence As CEL.KineticaRT.Sequence.Sequence
        Private _TimeLimitValue As Double

        Private _TriggerChannel As IChannel
        Private _WatchdogDINChannel As IChannel
        Private _TimeLimitChannel As IChannel

        Private _ChannelAndDataValid As Boolean

        Private _StarTime As System.DateTime

        Public Sub New(ByRef pSequence As CEL.KineticaRT.Sequence.Sequence, _
                ByVal pTriggerChannel As String, _
                ByVal pTimeLimitChannel As String, _
                ByVal pWatchdogDINChannel As String)

            _ChannelAndDataValid = False
            _StarTime = System.DateTime.Now
            _Sequence = pSequence

            Try
                If InitializeAndValidateChannel(_Sequence, _TriggerChannel, pTriggerChannel, "TriggerChannel", "Trigger") And
                   InitializeAndValidateChannel(_Sequence, _TimeLimitChannel, pTimeLimitChannel, "TimeLimitChannel", "Time Limit") And
                   InitializeAndValidateChannel(_Sequence, _WatchdogDINChannel, pWatchdogDINChannel, "WatchdogDINChannel", "Watchdog DIN") Then

                    _TimeLimitValue = _TimeLimitChannel.Value.ToDouble
                    _TriggerChannel.Value = Values.False

                    If _TimeLimitValue <= 0 Then
                        _TimeLimitValue = 10
                    End If

                    AddHandlers()

                    _ChannelAndDataValid = True

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("Constructeur: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        '**********************************
        '* InitializeAndValidateChannel() *
        '**********************************
        Private Function InitializeAndValidateChannel(ByVal pSequence As CEL.KineticaRT.Sequence.Sequence, ByRef pChannel As IChannel, ByVal pChannelName As String, ByVal pErrorMessage1 As String, ByVal pErrorMessage2 As String) As Boolean
            pSequence.InitializeChannel(pChannel, pChannelName)
            If pChannel Is Nothing Then
                clsTopMostMessageBox.Show(pErrorMessage1 + " does not correspond to a valid signal name", pErrorMessage2 + " Channel Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
            Return True
        End Function

        '*****************
        '* AddHandlers() *
        '*****************
        Private Sub AddHandlers()
            If Not _WatchdogDINChannel Is Nothing Then
                AddHandler _WatchdogDINChannel.ValueChanged, AddressOf OnWatchdogDINChannel
            End If
            If Not _TimeLimitChannel Is Nothing Then
                AddHandler _TimeLimitChannel.ValueChanged, AddressOf OnTimeLimitChannel
            End If
        End Sub

        '********************
        '* RemoveHandlers() *
        '********************
        Private Sub RemoveHandlers()
            If Not _WatchdogDINChannel Is Nothing Then
                RemoveHandler _WatchdogDINChannel.ValueChanged, AddressOf OnWatchdogDINChannel
            End If
            If Not _TimeLimitChannel Is Nothing Then
                RemoveHandler _TimeLimitChannel.ValueChanged, AddressOf OnTimeLimitChannel
            End If
        End Sub

        '**************************
        '* OnWatchdogDINChannel() *
        '**************************
        Private Sub OnWatchdogDINChannel(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                ' The watchdog was triggered, reset the timer
                _StarTime = System.DateTime.Now

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnWatchdogDINChannel: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '************************
        '* OnTimeLimitChannel() *
        '************************
        Private Sub OnTimeLimitChannel(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                _TimeLimitValue = _TimeLimitChannel.Value.ToDouble

                If _TimeLimitValue <= 0 Then
                    _TimeLimitValue = 10
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnTimeLimitChannel: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '****************************
        '* WatchdogElapsedHandler() *
        '****************************
        Public Sub WatchdogElapsedHandler()
            ' Must be called from OnEngineWorkingThread()

            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If _ChannelAndDataValid Then
                    Dim span As System.TimeSpan = System.DateTime.Now.Subtract(_StarTime)

                    If span.TotalSeconds > _TimeLimitValue Then
                        _TriggerChannel.Value = Values.True
                    Else
                        _TriggerChannel.Value = Values.False
                    End If
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("WatchdogElapsedHandler: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    RemoveHandlers()
                End If

            End If
            Me.disposedValue = True
        End Sub

#Region " _Sequence Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class



End Namespace
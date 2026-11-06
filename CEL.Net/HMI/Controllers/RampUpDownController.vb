Imports System
Imports System.Windows.Forms
Imports System.Timers
Imports KineticaRT.Channels
Imports KineticaRT.Values

Imports System.Threading


Namespace CEL.HMI

    Public Class RampUpDownController

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _Sequence As CEL.KineticaRT.Sequence.Sequence
        Private _LowRangeValue As Double
        Private _HighRangeValue As Double
        Private _RampUpDownTimeValue As Double

        Private _StartingButtonChannel As IChannel
        Private _OutputChannel As IChannel
        Private _LowRangeChannel As IChannel
        Private _HighRangeChannel As IChannel
        Private _RampUpDownTimeChannel As IChannel

        Private _ChannelAndDataValid As Boolean
        Private _TimerEnable As Boolean

        Private _StarTime As System.DateTime

        Public Sub New(ByRef pSequence As CEL.KineticaRT.Sequence.Sequence, _
                ByVal pLowRangeChannel As String, _
                ByVal pHighRangeChannel As String, _
                ByVal pRampUpDownTimeChannel As String, _
                ByVal pStartingButtonChannel As String, _
                ByVal pOutputChannel As String)

            _ChannelAndDataValid = False
            _TimerEnable = False

            _Sequence = pSequence

            Try
                If InitializeAndValidateChannel(_Sequence, _LowRangeChannel, pLowRangeChannel, "LowRangeChannel", "Low Range") And
                   InitializeAndValidateChannel(_Sequence, _HighRangeChannel, pHighRangeChannel, "HighRangeChannel", "High Range") And
                   InitializeAndValidateChannel(_Sequence, _RampUpDownTimeChannel, pRampUpDownTimeChannel, "RampUpTimeCHannel", "RampUp Time") And
                   InitializeAndValidateChannel(_Sequence, _StartingButtonChannel, pStartingButtonChannel, "StartingButtonChannel", "Starting Button") And
                   InitializeAndValidateChannel(_Sequence, _OutputChannel, pOutputChannel, "OutputChannel", "Output") Then

                    _ChannelAndDataValid = True

                End If

                If _ChannelAndDataValid Then

                    _LowRangeValue = _LowRangeChannel.Value.ToDouble()
                    _HighRangeValue = _HighRangeChannel.Value.ToDouble()
                    _RampUpDownTimeValue = _RampUpDownTimeChannel.Value.ToDouble() * 1000

                    Validatedata()
                End If

                If _ChannelAndDataValid Then

                    If CELProject.IsTrue(_StartingButtonChannel.Value) Then
                        _OutputChannel.Value = New Global.KineticaRT.Values.Double(_HighRangeValue)
                    Else
                        _OutputChannel.Value = New Global.KineticaRT.Values.Double(_LowRangeValue)
                    End If

                    AddHandlers()
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

        '******************
        '* Validatedata() *
        '******************
        Private Sub Validatedata()
            If _LowRangeValue < 0.0 Then
                clsTopMostMessageBox.Show("CurrentLowRangeValue must be >= 0.0 mA", "Current Low Range Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _ChannelAndDataValid = False
            End If

            If _HighRangeValue <= _LowRangeValue Then
                clsTopMostMessageBox.Show("CurrentHighRangeValue must be greather than CurrentLowRangeValue", "Current High Range Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _ChannelAndDataValid = False
            End If

            If _RampUpDownTimeValue < 1 Then
                clsTopMostMessageBox.Show("RampUpTime must be greather than 0", "RampUpTime Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _ChannelAndDataValid = False
            End If

        End Sub

        '*****************
        '* AddHandlers() *
        '*****************
        Private Sub AddHandlers()
            If Not _StartingButtonChannel Is Nothing Then
                AddHandler _StartingButtonChannel.ValueChanged, AddressOf OnStartingButtonChanged
            End If
        End Sub

        '********************
        '* RemoveHandlers() *
        '********************
        Private Sub RemoveHandlers()
            If Not _StartingButtonChannel Is Nothing Then
                RemoveHandler _StartingButtonChannel.ValueChanged, AddressOf OnStartingButtonChanged
            End If
        End Sub

        '*****************************
        '* OnStartingButtonChanged() *
        '*****************************
        Private Sub OnStartingButtonChanged(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                StartTheTimer()

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnStartingButtonChanged: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        '*******************************
        '* RampUpElapsedEventHandler() *
        '*******************************
        Public Sub RampUpDownElapsedHandler()
            ' Must be called from OnEngineWorkingThread()

            Dim newValue As Double = 0.0

            Try
                If _TimerEnable Then

                    If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                        Exit Sub 'Sort de cette fonction si l'archive est en playback
                    End If

                    Dim span As System.TimeSpan = System.DateTime.Now.Subtract(_StarTime)

                    If CELProject.IsTrue(_StartingButtonChannel.Value) Then
                        newValue = (_HighRangeValue - _OutputChannel.Value.ToDouble()) * span.TotalMilliseconds / _RampUpDownTimeValue + _OutputChannel.Value.ToDouble()
                    Else
                        newValue = (_LowRangeValue - _OutputChannel.Value.ToDouble()) * span.TotalMilliseconds / _RampUpDownTimeValue + _OutputChannel.Value.ToDouble()
                    End If

                    If Not _OutputChannel Is Nothing Then
                        _OutputChannel.Value = New Global.KineticaRT.Values.Double(newValue)
                    End If
                    If span.TotalMilliseconds > _RampUpDownTimeValue Then
                        StopTheTimer()
                    End If
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("RampUpDownElapsedHandler: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '*******************
        '* StartTheTimer() *
        '*******************
        Public Sub StartTheTimer()

            Try
                _TimerEnable = True
                _StarTime = System.DateTime.Now
            Catch ex As Exception
                clsTopMostMessageBox.Show("StartTheTimer: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '******************
        '* StopTheTimer() *
        '******************
        Public Sub StopTheTimer()

            Try
                _TimerEnable = False
            Catch ex As Exception
                clsTopMostMessageBox.Show("StopTheTimer: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
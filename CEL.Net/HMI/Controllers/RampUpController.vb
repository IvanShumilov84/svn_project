Imports System
Imports System.Windows.Forms
Imports System.Timers
Imports KineticaRT.Channels
Imports KineticaRT.Values

Imports System.Threading


Namespace CEL.HMI

    Public Class RampUpController

        'Ce contrôleur permet de faire une augmentation graduel du courant pour des pompes, 
        'des valves... pour éviter les effets "béliers" si toute la puissance était appliqué d'un coup.

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _Sequence As CEL.KineticaRT.Sequence.Sequence
        Private _CurrentLowRangeValue As Double
        Private _CurrentHighRangeValue As Double
        Private _RampUpTimeValue As Integer

        Private _StartingButtonChannel As IChannel
        Private _IoutChannel As IChannel
        Private _CurrentLowRangeChannel As IChannel
        Private _CurrentHighRangeChannel As IChannel
        Private _RampUpTimeChannel As IChannel

        Private _TimerCountUp As Integer
        Private _ChannelAndDataValid As Boolean

        Private mutex1 As New Mutex()
        Private mutex1Result As Boolean

        Private _timer As System.Timers.Timer

        ' Constructeur qui utilise des noms de canaux comme parametres.
        ' pCurrentLowRangeChannel et pCurrentHighRangeChannel sont les limites hardware, typiquement 4 ma et 20 ma
        ' pCurrentForLowSpeedChannel est la valeur de courant a appliquer quand la vitesse est plus base que pEngineLowSpeedTriggerChannel
        ' pRampUpTimeChannel delais en secondes ou le courant va passer de la valeur minimum a la valeur maximum
        ' pStartingButtonChannel est le ON/OFF du device
        Public Sub New(ByRef pSequence As CEL.KineticaRT.Sequence.Sequence, _
                ByVal pCurrentLowRangeChannel As String, _
                ByVal pCurrentHighRangeChannel As String, _
                ByVal pRampUpTimeChannel As String, _
                ByVal pStartingButtonChannel As String, _
                ByVal pIoutChannel As String)

            _ChannelAndDataValid = False

            _Sequence = pSequence

            Try
                If InitializeAndValidateChannel(_Sequence, _CurrentLowRangeChannel, pCurrentLowRangeChannel, "CurrentLowRangeChannel", "Current Low Range") And
                   InitializeAndValidateChannel(_Sequence, _CurrentHighRangeChannel, pCurrentHighRangeChannel, "CurrentHighRangeChannel", "Current High Range") And
                   InitializeAndValidateChannel(_Sequence, _RampUpTimeChannel, pRampUpTimeChannel, "RampUpTimeCHannel", "RampUp Time") And
                   InitializeAndValidateChannel(_Sequence, _StartingButtonChannel, pStartingButtonChannel, "StartingButtonChannel", "Starting Button") And
                   InitializeAndValidateChannel(_Sequence, _IoutChannel, pIoutChannel, "IoutChannel", "IOUT") Then

                    _ChannelAndDataValid = True

                End If

                If _ChannelAndDataValid Then

                    _CurrentLowRangeValue = _CurrentLowRangeChannel.Value.ToDouble()
                    _CurrentHighRangeValue = _CurrentHighRangeChannel.Value.ToDouble()
                    _RampUpTimeValue = CType(_RampUpTimeChannel.Value.ToDouble(), Integer)

                    Validatedata()
                End If

                _timer = New System.Timers.Timer(1000)
                _timer.Enabled = False

                If _ChannelAndDataValid Then

                    _IoutChannel.Value = New Global.KineticaRT.Values.Double(_CurrentLowRangeValue)

                    AddHandlers()
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("Constructeur: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        ' Autre constructeur qui utilise des valeurs numeriques comme parametres. Principalement utilise lors des tests unitaires.
        Public Sub New(ByRef pSequence As CEL.KineticaRT.Sequence.Sequence, _
                ByVal pCurrentLowRangevalue As Double, _
                ByVal pCurrentHighRangeValue As Double, _
                ByVal pRampUpTimeValue As Integer, _
                ByVal pStartingButtonChannel As String, _
                ByVal pIoutChannel As String)

            Try
                _ChannelAndDataValid = False

                _Sequence = pSequence

                If InitializeAndValidateChannel(_Sequence, _StartingButtonChannel, pStartingButtonChannel, "StartingButtonChannel", "Starting Button") And
                   InitializeAndValidateChannel(_Sequence, _IoutChannel, pIoutChannel, "IoutChannel", "IOUT") Then

                    _ChannelAndDataValid = True

                End If

                If _ChannelAndDataValid Then

                    _CurrentLowRangeValue = pCurrentLowRangevalue
                    _CurrentHighRangeValue = pCurrentHighRangeValue
                    _RampUpTimeValue = pRampUpTimeValue

                    Validatedata()
                End If

                _timer = New System.Timers.Timer(1000)
                _timer.Enabled = False

                If _ChannelAndDataValid Then

                    _IoutChannel.Value = New Global.KineticaRT.Values.Double(_CurrentLowRangeValue)

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
            If _CurrentLowRangeValue < 0.0 Then
                clsTopMostMessageBox.Show("CurrentLowRangeValue must be >= 0.0 mA", "Current Low Range Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _ChannelAndDataValid = False
            End If

            If _CurrentHighRangeValue <= _CurrentLowRangeValue Then
                clsTopMostMessageBox.Show("CurrentHighRangeValue must be greather than CurrentLowRangeValue", "Current High Range Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _ChannelAndDataValid = False
            End If

            If _RampUpTimeValue < 1 Then
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
            If Not _CurrentHighRangeChannel Is Nothing Then
                AddHandler _CurrentHighRangeChannel.ValueChanged, AddressOf OnCurrentHighRangeChannelChanged
            End If

            AddHandler _timer.Elapsed, New ElapsedEventHandler(AddressOf RampUpElapsedEventHandler)
        End Sub

        '********************
        '* RemoveHandlers() *
        '********************
        Private Sub RemoveHandlers()
            If Not _StartingButtonChannel Is Nothing Then
                RemoveHandler _StartingButtonChannel.ValueChanged, AddressOf OnStartingButtonChanged
            End If

            If Not _CurrentHighRangeChannel Is Nothing Then
                RemoveHandler _CurrentHighRangeChannel.ValueChanged, AddressOf OnCurrentHighRangeChannelChanged
            End If

            RemoveHandler _timer.Elapsed, AddressOf RampUpElapsedEventHandler
            _timer.Dispose()
        End Sub

        '*****************************
        '* OnStartingButtonChanged() *
        '*****************************
        Private Sub OnStartingButtonChanged(ByVal sender As IChannel, ByVal newVal As Value)
            mutex1Result = mutex1.WaitOne()
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If CELProject.IsFalse(newVal) Then

                    StopTheTimer()
                    _IoutChannel.Value = New Global.KineticaRT.Values.Double(_CurrentLowRangeValue)
                Else
                    _IoutChannel.Value = New Global.KineticaRT.Values.Double(_CurrentLowRangeValue)
                    StartTheTimer()
                    WaitForChannelUpdate(_IoutChannel, _CurrentLowRangeValue)
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnStartingButtonChanged: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            mutex1.ReleaseMutex()
        End Sub

        '*****************************
        '* OnCurrentHighRangeChannelChanged() *
        '*****************************
        Private Sub OnCurrentHighRangeChannelChanged(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If newVal.ToDouble() > 20.0 Then
                    _CurrentHighRangeValue = 20.0
                ElseIf newVal.ToDouble() < _CurrentLowRangeValue Then
                    _CurrentHighRangeValue = _CurrentLowRangeValue
                Else
                    _CurrentHighRangeValue = newVal.ToDouble()
                End If

                If CELProject.IsTrue(_StartingButtonChannel.Value) Then
                    If _timer.Enabled = False Then
                        _IoutChannel.Value = New Global.KineticaRT.Values.Double(_CurrentHighRangeValue)
                    End If
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnStartingButtonChanged: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            mutex1.ReleaseMutex()
        End Sub

        '*******************************
        '* RampUpElapsedEventHandler() *
        '*******************************
        Public Sub RampUpElapsedEventHandler(ByVal sender As Object, ByVal e As ElapsedEventArgs)

            Dim newCurrentValue As Double = 0.0
            mutex1Result = mutex1.WaitOne()

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                _TimerCountUp += 1

                '                newCurrentValue = (_CurrentHighRangeValue - _CurrentLowRangeValue) * _TimerCountUp / CType(_RampUpTimeValue, Double) + _CurrentLowRangeValue
                newCurrentValue = (_CurrentHighRangeValue - _IoutChannel.Value.ToDouble()) * _TimerCountUp / CType(_RampUpTimeValue, Double) + _IoutChannel.Value.ToDouble()

                If Not _IoutChannel Is Nothing And _timer.Enabled Then
                    _IoutChannel.Value = New Global.KineticaRT.Values.Double(newCurrentValue)
                    WaitForChannelUpdate(_IoutChannel, newCurrentValue)
                End If

                If _TimerCountUp >= _RampUpTimeValue Then
                    StopTheTimer()
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("ProcessRampUp: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            mutex1.ReleaseMutex()
        End Sub

        '*******************
        '* StartTheTimer() *
        '*******************
        Public Sub StartTheTimer()

            Try
                _TimerCountUp = 0
                _timer.Enabled = True
            Catch ex As Exception
                clsTopMostMessageBox.Show("StartTheTimer: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '******************
        '* StopTheTimer() *
        '******************
        Public Sub StopTheTimer()

            Try
                _timer.Enabled = False
            Catch ex As Exception
                clsTopMostMessageBox.Show("StopTheTimer: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub WaitForChannelUpdate(ByRef updatedChannel As IChannel, ByVal newVal As Double)

            Dim iLoop As Integer = 0
            For iLoop = 0 To 20
                If Math.Abs(newVal - updatedChannel.Value.ToDouble()) < 0.05 Then
                    Exit For
                End If
                System.Threading.Thread.Sleep(100)
            Next
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
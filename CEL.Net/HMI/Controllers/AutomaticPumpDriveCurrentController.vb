Imports System
Imports System.Windows.Forms
Imports System.Timers
Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.HMI

    Public Class AutomaticPumpDriveCurrentController

        ' Ce contrôleur permet d'ajuster automatiquement la puissance de la pompe en fonction de la
        ' vitesse du moteur. Il utilise un principe d'hystérésis avec des valeurs "trigger" basse et haute.
        ' Le trigger bas est utilisé quand la vitesse diminue et le trigger haut lorsqu'elle accélère.

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _Sequence As CEL.KineticaRT.Sequence.Sequence
        Private _deviceStarted As Boolean
        Private _ChannelAndDataValid As Boolean

        Private _CurrentLowRangeValue As Double
        Private _CurrentHighRangeValue As Double
        Private _CurrentForLowSpeedValue As Double
        Private _CurrentForHighSpeedValue As Double
        Private _EngineLowSpeedTriggerValue As Double
        Private _EngineHighSpeedTriggerValue As Double

        Private _StartingButtonChannel As IBooleanChannel
        Private _IoutChannel As IChannel
        Private _CurrentLowRangeChannel As IChannel
        Private _CurrentHighRangeChannel As IChannel
        Private _CurrentForLowSpeedChannel As IChannel
        Private _CurrentForHighSpeedChannel As IChannel
        Private _EngineLowSpeedTriggerChannel As IChannel
        Private _EngineHighSpeedTriggerChannel As IChannel
        Private _SpeedChannel As IChannel

        ' Constructeur qui utilise des noms de canaux comme parametres.
        ' pCurrentLowRangeChannel et pCurrentHighRangeChannel sont les limites hardware, typiquement 4 ma et 20 ma
        ' pCurrentForLowSpeedChannel est la valeur de courant a appliquer quand la vitesse est plus base que pEngineLowSpeedTriggerChannel
        ' pCurrentForHighSpeedChannel est la valeur de courant a appliquer quand la vitesse est plus grande que pEngineHighSpeedTriggerChannel
        ' pStartingButtonChannel est le ON/OFF du device a controler
        ' pSpeedChannel est la source de la vitesse de reference
        ' pIoutChannel est le canal ou appliquer le courant
        Public Sub New(ByRef pSequence As CEL.KineticaRT.Sequence.Sequence, _
                ByVal pCurrentLowRangeChannel As String, _
                ByVal pCurrentHighRangeChannel As String, _
                ByVal pCurrentForLowSpeedChannel As String, _
                ByVal pCurrentForHighSpeedChannel As String, _
                ByVal pEngineLowSpeedTriggerChannel As String, _
                ByVal pEngineHighSpeedTriggerChannel As String, _
                ByVal pStartingButtonChannel As String, _
                ByVal pSpeedChannel As String, _
                ByVal pIoutChannel As String)

            _ChannelAndDataValid = False

            _Sequence = pSequence

            Try

                If InitializeAndValidateChannel(_Sequence, _CurrentLowRangeChannel, pCurrentLowRangeChannel, "CurrentLowRangeChannel", "Current Low Range") And
                           InitializeAndValidateChannel(_Sequence, _CurrentHighRangeChannel, pCurrentHighRangeChannel, "CurrentHighRangeChannel", "Current High Range") And
                           InitializeAndValidateChannel(_Sequence, _CurrentForLowSpeedChannel, pCurrentForLowSpeedChannel, "CurrentForLowSpeedChannel", "Current for low speed") And
                           InitializeAndValidateChannel(_Sequence, _CurrentForHighSpeedChannel, pCurrentForHighSpeedChannel, "CurrentForHighSpeedChannel", "Current for high speed") And
                           InitializeAndValidateChannel(_Sequence, _EngineLowSpeedTriggerChannel, pEngineLowSpeedTriggerChannel, "EngineLowSpeedTriggerChannel", "Engine Low Speed Trigger") And
                           InitializeAndValidateChannel(_Sequence, _EngineHighSpeedTriggerChannel, pEngineHighSpeedTriggerChannel, "EngineHighSpeedTriggerChannel", "Engine High Speed Trigger") And
                           InitializeAndValidateChannel(_Sequence, _StartingButtonChannel, pStartingButtonChannel, "StartingButtonChannel", "Starting Button") And
                           InitializeAndValidateChannel(_Sequence, _SpeedChannel, pSpeedChannel, "SpeedChannel", "Speed") And
                           InitializeAndValidateChannel(_Sequence, _IoutChannel, pIoutChannel, "IoutChannel", "IOUT") Then

                    _ChannelAndDataValid = True

                End If

                If _ChannelAndDataValid Then

                    _CurrentLowRangeValue = _CurrentLowRangeChannel.Value.ToDouble()
                    _CurrentHighRangeValue = _CurrentHighRangeChannel.Value.ToDouble()
                    _CurrentForLowSpeedValue = _CurrentForLowSpeedChannel.Value.ToDouble()
                    _CurrentForHighSpeedValue = _CurrentForHighSpeedChannel.Value.ToDouble()
                    _EngineLowSpeedTriggerValue = _EngineLowSpeedTriggerChannel.Value.ToDouble()
                    _EngineHighSpeedTriggerValue = _EngineHighSpeedTriggerChannel.Value.ToDouble()

                    Validatedata()
                End If


                If _ChannelAndDataValid Then

                    _IoutChannel.Value = New Global.KineticaRT.Values.Double(_CurrentLowRangeValue)

                    _deviceStarted = CELProject.IsTrue(_StartingButtonChannel.Value)

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

            If _CurrentForLowSpeedValue <= _CurrentLowRangeValue Then
                clsTopMostMessageBox.Show("CurrentForLowSpeedValue must be greather than CurrentLowRangeValue", "Current for Low Speed Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _ChannelAndDataValid = False
            End If

            If _CurrentForLowSpeedValue > _CurrentHighRangeValue Then
                clsTopMostMessageBox.Show("lower than CurrentHighRangeValue", "Current for Low Speed Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _ChannelAndDataValid = False
            End If

            If _CurrentForHighSpeedValue > _CurrentHighRangeValue Then
                clsTopMostMessageBox.Show("CurrentForHighSpeedValue must be lower or equal to CurrentHighRangeValue", "Current for High Speed Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _ChannelAndDataValid = False
            End If

            If _CurrentForLowSpeedValue >= _CurrentForHighSpeedValue Then
                clsTopMostMessageBox.Show("CurrentForLowSpeedValue must be lower than CurrentForHighSpeedValue", "Current for Low Speed Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _ChannelAndDataValid = False
            End If

            If _EngineLowSpeedTriggerValue <= 0.0 Then
                clsTopMostMessageBox.Show("EngineLowSpeedTriggerValue must be > 0.0", "Engine Low Speed Trigger Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                _ChannelAndDataValid = False
            End If

            If _EngineHighSpeedTriggerValue <= _EngineLowSpeedTriggerValue Then
                clsTopMostMessageBox.Show("EngineHighSpeedTriggerValue must be > EngineLowSpeedTriggerValue", "Engine High Speed Trigger Value Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

                If CELProject.IsFalse(newVal) Then
                    _deviceStarted = False
                    _IoutChannel.Value = New Global.KineticaRT.Values.Double(_CurrentLowRangeValue)
                Else
                    _deviceStarted = True
                    OnSpeedChanged()
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnStartingButtonChanged: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '********************
        '* OnSpeedChanged() *
        '********************
        Public Sub OnSpeedChanged()
            Try
                If _ChannelAndDataValid And _deviceStarted Then
                    If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                        Exit Sub 'Sort de cette fonction si l'archive est en playback
                    End If

                    _CurrentForLowSpeedValue = _CurrentForLowSpeedChannel.Value.ToDouble()
                    _CurrentForHighSpeedValue = _CurrentForHighSpeedChannel.Value.ToDouble()

                    Dim speed As Double = _SpeedChannel.Value.ToDouble()
                    If speed >= _EngineHighSpeedTriggerValue Then
                        _IoutChannel.Value = New Global.KineticaRT.Values.Double(_CurrentForHighSpeedValue)
                    ElseIf speed <= _EngineLowSpeedTriggerValue Then
                        _IoutChannel.Value = New Global.KineticaRT.Values.Double(_CurrentForLowSpeedValue)
                    End If
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnStartingButtonChanged: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
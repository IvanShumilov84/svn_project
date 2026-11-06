Imports KineticaRT.Channels
Imports KineticaRT.Values


'Source: http://www.codeproject.com/Articles/36459/PID-process-control-a-Cruise-Control-example

Public Class PIDController
    Private ReadOnly MIN_PCT As New Global.KineticaRT.Values.Double(0.0)
    Private ReadOnly MAX_PCT As New Global.KineticaRT.Values.Double(100.0)

    Private _Enable As Boolean = False

    'Constantes de PIDController
    Private _P As Double
    Private _I As Double
    Private _D As Double

    'Must be probably changed
    Private _OutputTrim As Double = 0
    Private _Noise As Double = 0

    'Variable utilisées par la classe
    Private _Integral As Double = 0
    Private _Derivative As Double = 0
    Private _LastError As Double = 0
    Private _Error As Double = 0

    'L'entrée du PID
    Private _Demand As Double
    Private _DemandChannel As INumericChannel

    'La sortie de la commande du PID est le canal de la demand du mode manuel
    Private _Output As Double
    Private _OutputChannel As IChannel
    Private _OutputPCT As Double

    'Le retour du procède
    Private _ProcessValue As Double
    Private _ProcessChannel As INumericChannel

    Private _ProcessValueTimerInterval As Double
    Private _ReadProcessValueTimer As System.Timers.Timer
    Private _ComputeNewProcessValueTimer As System.Timers.Timer

    Public Sub New(ByRef demandChannel As INumericChannel,
                   ByRef outputChannel As IChannel,
                   ByRef processChannel As INumericChannel,
                   p As Double, i As Double, d As Double, timerInterval As Double)

        Me._DemandChannel = demandChannel
        Me._OutputChannel = outputChannel
        Me._ProcessChannel = processChannel

        Me._P = p
        Me._I = i
        Me._D = d

        Me._ProcessValueTimerInterval = timerInterval
        Me._ReadProcessValueTimer = New System.Timers.Timer(timerInterval)
        Me._ReadProcessValueTimer.Enabled = True

        Me._ComputeNewProcessValueTimer = New System.Timers.Timer(timerInterval * 10.0)
        Me._ComputeNewProcessValueTimer.Enabled = True

        AddHandler _ReadProcessValueTimer.Elapsed, AddressOf OnReadPV
        AddHandler _ComputeNewProcessValueTimer.Elapsed, AddressOf OnComputeNewProcessValue
    End Sub

    'Must be done at least twice faster than PID Computing
    Private Sub OnReadPV(source As Object, e As System.Timers.ElapsedEventArgs)
        'Valeur corrigée au besoin
        _ProcessValue = _ProcessChannel.Value.ToDouble + (_Output * _OutputTrim) + _Noise
    End Sub

    'Will be executed 10 times slower than OnReadPV
    Public Sub OnComputeNewProcessValue(source As Object, e As System.Timers.ElapsedEventArgs)

        Me._Demand = Me._DemandChannel.Value.ToDouble

        Me._Error = Me._Demand - Me._ProcessValue
        Me._Integral += (Me._Error / (Me._ProcessValueTimerInterval * 10))
        Me._Derivative = (Me._Error - Me._LastError) / (Me._ProcessValueTimerInterval * 10)

        Me._Output = (Me._P * Me._Error) + (Me._I * Me._Integral) + (Me._D * Me._Derivative)
        Me._LastError = Me._Error

        Me._OutputPCT = Me._Output * 100

        If Me._Enable Then
            If Me._OutputPCT >= 100.0 Then
                Me._OutputChannel.Value = MAX_PCT
            ElseIf Me._OutputPCT <= 0.0 Then
                Me._OutputChannel.Value = MIN_PCT
            Else
                Me._OutputChannel.Value = (New Global.KineticaRT.Values.Double(Me._OutputPCT))
            End If
        End If

    End Sub

    Public Property Enable As Boolean
        Get
            Return _Enable
        End Get
        Set(value As Boolean)
            _Enable = value
        End Set
    End Property

    Public Property P As Double
        Get
            Return Me._P
        End Get
        Set(value As Double)
            Me._P = value
        End Set
    End Property

    Public Property I As Double
        Get
            Return Me._I
        End Get
        Set(value As Double)
            Me._I = value
        End Set
    End Property

    Public Property D As Double
        Get
            Return Me._D
        End Get
        Set(value As Double)
            Me._D = value
        End Set
    End Property

    Public ReadOnly Property OutputPCT As Global.KineticaRT.Values.Double
        Get
            Return New Global.KineticaRT.Values.Double(Me._OutputPCT)
        End Get

    End Property

    Public Property TimerInterval As Double
        Get
            Return _ProcessValueTimerInterval
        End Get
        Set(value As Double)
            _ProcessValueTimerInterval = value
        End Set
    End Property

    Public Property OutputTrim As Double
        Get
            Return Me._OutputTrim
        End Get
        Set(value As Double)
            Me._OutputTrim = value
        End Set
    End Property


End Class

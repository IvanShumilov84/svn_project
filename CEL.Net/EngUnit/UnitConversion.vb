Namespace CEL.EngUnit

    Public Class UnitConversion
#Region "Constants"

        ' 1 cm = 0.3937008 inch
        Private Const _inches_per_centimeter As Double = 0.3937008
        Private Const _inH2O_per_inHg As Double = 13.5945
        Private Const _inH2O_per_kPa As Double = 4.0147
        Private Const _inH2O_per_psi As Double = 27.6806
        Private Const _kg_per_pound As Double = 0.45359237
        ' 1 US Gallon = 3.785411784 liters
        Private Const _liters_per_usgallon As Double = 3.785411784
        ' 1 Hour = 60 minutes
        Private Const _minutes_per_hour As Double = 60.0
        Private Const _mseconds_per_hour As Double = _mseconds_per_minute * _minutes_per_hour
        Private Const _mseconds_per_minute As Double = _mseconds_per_second * _seconds_per_minute
        Private Const _mseconds_per_second As Double = 1000.0
        Private Const _psi_per_inHg As Double = 0.4911541
        Private Const _psi_per_kPa As Double = 0.1450377
        Private Const _seconds_per_minute As Double = 60.0

#End Region

#Region "Shared (static) Properties"

        Public Shared ReadOnly Property Inches_Per_Centimeter As Double
            Get
                Return _inches_per_centimeter
            End Get
        End Property

        Public Shared ReadOnly Property inH20_Per_inHg As Double
            Get
                Return _inH2O_per_inHg
            End Get
        End Property

        Public Shared ReadOnly Property inH2O_Per_kPa As Double
            Get
                Return _inH2O_per_kPa
            End Get
        End Property

        Public Shared ReadOnly Property inH2O_Per_Psi As Double
            Get
                Return _inH2O_per_psi
            End Get
        End Property

        Public Shared ReadOnly Property kg_Per_Pound As Double
            Get
                Return _kg_per_pound
            End Get
        End Property

        Public Shared ReadOnly Property Liters_Per_UsGallon As Double
            Get
                Return _liters_per_usgallon
            End Get
        End Property

        Public Shared ReadOnly Property Minutes_Per_Hour As Double
            Get
                Return _minutes_per_hour
            End Get
        End Property

        Public Shared ReadOnly Property mSeconds_Per_Hour As Double
            Get
                Return _mseconds_per_hour
            End Get
        End Property

        Public Shared ReadOnly Property mSeconds_Per_Minute As Double
            Get
                Return _mseconds_per_minute
            End Get
        End Property

        Public Shared ReadOnly Property Psi_Per_inHg As Double
            Get
                Return _psi_per_inHg
            End Get
        End Property

        Public Shared ReadOnly Property Psi_Per_kPa As Double
            Get
                Return _psi_per_kPa
            End Get
        End Property

        Public Shared ReadOnly Property Seconds_Per_Minute As Double
            Get
                Return _seconds_per_minute
            End Get
        End Property
#End Region

    End Class

End Namespace

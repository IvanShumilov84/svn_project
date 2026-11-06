Option Strict On
Option Explicit On

Imports System.Text

Namespace CEL.TestProgress
    Public MustInherit Class BaseCheckListItem
        Implements ICheckListItem

        Protected Sub New(parameter As String, unit As String, lowLimit As Double, highLimit As Double)
            m_parameter = parameter
            m_unit = unit
            m_lowLimit = lowLimit
            m_highLimit = highLimit
            m_limitStr = String.Empty

            If Not Double.IsNegativeInfinity(lowLimit) Then
                m_limitStr = lowLimit.ToString()
            End If
            If Not Double.IsPositiveInfinity(highLimit) Then
                If m_limitStr <> String.Empty Then
                    m_limitStr += " to "
                End If
                m_limitStr += highLimit.ToString()
            End If

        End Sub

        Public Overridable Property Active As Boolean Implements ICheckListItem.Active
            Get
                Return m_active
            End Get
            Set(value As Boolean)
                If value <> m_active Then
                    m_active = value
                End If
            End Set
        End Property

        Public Overridable Function Failed() As Boolean Implements ICheckListItem.Failed
            Return Not Passed()
        End Function

        Public Overridable ReadOnly Property Limit As String Implements ICheckListItem.Limit
            Get
                Return m_limitStr
            End Get
        End Property

        Public Overridable ReadOnly Property Parameter As String Implements ICheckListItem.Parameter
            Get
                Return m_parameter
            End Get
        End Property

        Public MustOverride Function Passed() As Boolean Implements ICheckListItem.Passed

        Public Overridable ReadOnly Property Unit As String Implements ICheckListItem.Unit
            Get
                Return m_unit
            End Get
        End Property

        Public MustOverride Property Value As String Implements ICheckListItem.Value

        Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Public Overridable Sub OnPropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
            RaiseEvent PropertyChanged(sender, e)
        End Sub

        Protected ReadOnly Property LowLimit As Double
            Get
                Return m_lowLimit
            End Get
        End Property

        Protected ReadOnly Property HighLimit As Double
            Get
                Return m_highLimit
            End Get
        End Property

        Public Overrides Function ToString() As String
            Dim strBuilder As New StringBuilder()

            strBuilder.Append(Parameter)
            strBuilder.Append(" - Read: ")
            strBuilder.Append(Value)
            If (Unit <> String.Empty) Then
                strBuilder.Append(" ")
                strBuilder.Append(Unit)
            End If
            If Not Double.IsNegativeInfinity(LowLimit) Then
                strBuilder.Append(" Minimum: ")
                strBuilder.Append(LowLimit.ToString())
                If (Unit <> String.Empty) Then
                    strBuilder.Append(" ")
                    strBuilder.Append(Unit)
                End If
            End If
            If Not Double.IsPositiveInfinity(HighLimit) Then
                strBuilder.Append(" Maximum: ")
                strBuilder.Append(HighLimit.ToString())
                If (Unit <> String.Empty) Then
                    strBuilder.Append(" ")
                    strBuilder.Append(Unit)
                End If
            End If

            Return strBuilder.ToString()
        End Function

        Private m_active As Boolean = False
        Private ReadOnly m_highLimit As Double
        Private ReadOnly m_lowLimit As Double
        Private ReadOnly m_parameter As String
        Private ReadOnly m_unit As String
        Private ReadOnly m_limitStr As String
    End Class
End Namespace


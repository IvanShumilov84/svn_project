Option Strict On
Option Explicit On

Imports System.Collections.Generic

Namespace CEL.TestProgress

    Public Class ManualEntryCheckListItem
        Inherits BaseCheckListItem

        Public Sub New(parameter As String, unit As String, lowLimit As Double, highLimit As Double)
            MyBase.New(parameter, unit, lowLimit, highLimit)
        End Sub

        Public Overrides Property Active As Boolean
            Get
                Return MyBase.Active
            End Get
            Set(value As Boolean)
                If value <> MyBase.Active Then
                    MyBase.Active = value
                    If False = MyBase.Active Then
                        m_value = String.Empty
                    End If
                End If
            End Set
        End Property

        Public Overrides Property Value As String
            Get
                Return m_value
            End Get
            Set(theValue As String)
                m_value = theValue
            End Set
        End Property

        Public Overrides Function Passed() As Boolean
            Dim result As Boolean = True
            Dim dblValue As Double
            If (Double.TryParse(Value, dblValue)) Then
                result = (dblValue >= MyBase.LowLimit) And (dblValue <= MyBase.HighLimit)
            End If
            Return result
        End Function

        Private m_value As String
    End Class

End Namespace


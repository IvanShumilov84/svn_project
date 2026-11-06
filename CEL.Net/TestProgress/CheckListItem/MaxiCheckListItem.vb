Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.TestProgress

    Public Class MaxiCheckListItem
        Inherits ChannelRequiredCheckListItem

        Public Sub New(parameter As String, unit As String, lowLimit As Double, highLimit As Double, channel As INumericChannel)
            MyBase.New(parameter, unit, lowLimit, highLimit, channel)
        End Sub

        Public Overrides Property Active As Boolean
            Get
                Return MyBase.Active
            End Get
            Set(value As Boolean)
                If value <> MyBase.Active Then
                    MyBase.Active = value
                    If MyBase.Active = False Then
                        m_max = Double.NegativeInfinity
                    Else
                        If MyBase.Channel IsNot Nothing Then
                            Dim theDoubleValue As Double = MyBase.Channel.Value.ToDouble()
                            m_max = theDoubleValue
                            m_returnValue = MyBase.Channel.Value.ToString(MyBase.Channel.Format, Nothing)
                        End If
                    End If
                End If
            End Set
        End Property

        Public Overrides ReadOnly Property Parameter As String
            Get
                Return "Maximum " + MyBase.Parameter
            End Get
        End Property

        Public Overrides Property Value As String
            Get
                If Double.IsNegativeInfinity(m_max) Then
                    Return String.Empty
                Else
                    Return m_returnValue
                End If
            End Get
            Set(value As String)
                Throw New System.InvalidOperationException()
            End Set
        End Property

        Public Overrides Function Passed() As Boolean
            Return (m_max >= LowLimit) And (m_max <= HighLimit)
        End Function

        Protected Overrides Sub OnValueChanged(sender As IChannel, newValue As Value)
            Dim theDoubleValue As Double

            theDoubleValue = newValue.ToDouble()
            If theDoubleValue > m_max Then
                m_max = theDoubleValue
                m_returnValue = newValue.ToString(MyBase.Channel.Format, Nothing)
                MyBase.OnValueChanged(sender, newValue)
            End If
        End Sub

        Private m_max As Double = Double.NegativeInfinity
        Private m_returnValue As String
    End Class
End Namespace


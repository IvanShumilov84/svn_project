Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.CompilerServices

Imports KineticaRT.Channels

Namespace CEL.TestProgress

    Public Class InstantCheckListItem
        Inherits ChannelRequiredCheckListItem
        Implements ICheckListItem

        Public Sub New(parameter As String, unit As String, lowLimit As Double, highLimit As Double, channel As INumericChannel)
            MyBase.New(parameter, unit, lowLimit, highLimit, channel)
        End Sub

        Public Overrides Property Value As String
            Get
                Return MyBase.Channel.Value.ToString(MyBase.Channel.Format, Nothing)
            End Get
            Set(value As String)
                Throw New System.InvalidOperationException()
            End Set
        End Property

        Public Overrides Function Passed() As Boolean
            Dim dblValue As Double = MyBase.Channel.Value.ToDouble()
            Return (dblValue >= LowLimit) And (dblValue <= HighLimit)
        End Function
    End Class

End Namespace


Option Strict On
Option Explicit On

Imports System.Text
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.TestProgress

    Public MustInherit Class ChannelRequiredCheckListItem
        Inherits BaseCheckListItem
        Implements System.IDisposable, System.ComponentModel.INotifyPropertyChanged

        Public Sub New(parameter As String, unit As String, lowLimit As Double, highLimit As Double, channel As INumericChannel)
            MyBase.New(parameter, unit, lowLimit, highLimit)
            m_krtChannel = channel
        End Sub

        Public Overloads Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
            System.GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                Active = False
            End If
        End Sub

        Protected Overrides Sub Finalize()
            Dispose(False)
        End Sub

        Public Overrides Property Active As Boolean
            Get
                Return MyBase.Active
            End Get
            Set(value As Boolean)
                If value <> MyBase.Active Then
                    MyBase.Active = value
                    If MyBase.Active = True Then
                        If m_krtChannel IsNot Nothing Then
                            AddHandler m_krtChannel.ValueChanged, AddressOf OnValueChanged
                        End If
                    Else
                        If m_krtChannel IsNot Nothing Then
                            RemoveHandler m_krtChannel.ValueChanged, AddressOf OnValueChanged
                        End If
                    End If
                End If
            End Set
        End Property

        Protected ReadOnly Property Channel As INumericChannel
            Get
                Return m_krtChannel
            End Get
        End Property

        Protected Overridable Sub OnValueChanged(sender As IChannel, newValue As Value)
            OnPropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs("Value"))
        End Sub

        Private ReadOnly m_krtChannel As INumericChannel
    End Class
End Namespace

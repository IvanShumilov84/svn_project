Option Strict On
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Threading

Namespace CEL.TestProgress

    Public Class TestProgressSheetEntryCollection
        Inherits BindingList(Of TestProgressSheetEntry)

        Public Sub New(Optional context As SynchronizationContext = Nothing)
            Me.ctx = context
        End Sub

        Friend Property SynchronizationContext As SynchronizationContext
            Get
                Return Me.ctx
            End Get
            Set(value As SynchronizationContext)
                Me.ctx = value
            End Set
        End Property
        Protected Overrides Sub OnAddingNew(e As AddingNewEventArgs)
            If ctx Is Nothing OrElse ctx Is SynchronizationContext.Current Then
                MyBase.OnAddingNew(e)
            Else
                ctx.Send((Sub(state As Object) MyBase.OnAddingNew(CType(state, AddingNewEventArgs))), e)
            End If
        End Sub

        Protected Overrides Sub OnListChanged(e As ListChangedEventArgs)
            If ctx Is Nothing OrElse ctx Is SynchronizationContext.Current Then
                MyBase.OnListChanged(e)
            Else
                ctx.Send((Sub(state As Object) MyBase.OnListChanged(CType(state, ListChangedEventArgs))), e)
            End If
        End Sub

        Private ctx As SynchronizationContext
    End Class
End Namespace

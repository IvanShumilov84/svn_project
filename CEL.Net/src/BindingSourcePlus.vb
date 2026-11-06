Option Strict On
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Threading

Namespace CEL.Net

    ''' <summary>
    '''  Replacement for the standard BindingSource.  
    '''  Uses a SynchronizationContext based on the thread that constructs it.
    ''' </summary>
    ''' 
    <DesignerCategory("")> _
    Public Class BindingSourcePlus
        Inherits BindingSource

        Private _context As SynchronizationContext
        Private _postback As _
             New SendOrPostCallback(AddressOf Me.OnListChangedPost)

        Public Sub New(ByVal container As IContainer)
            MyBase.New(container)
            _context = SynchronizationContext.Current
        End Sub
        Public Sub New() 'ft ajout
            _context = SynchronizationContext.Current
        End Sub

        Protected Overrides Sub OnListChanged(ByVal e As ListChangedEventArgs)
            If _context Is Nothing Then
                MyBase.OnListChanged(e)
            Else
                _context.Post(_postback, e)
            End If
        End Sub

        Private Sub OnListChangedPost(ByVal e As Object)
            MyBase.OnListChanged(TryCast(e, ListChangedEventArgs))
        End Sub

    End Class

End Namespace

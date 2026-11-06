Option Strict On
Option Explicit On

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Namespace CEL.TestProgress
    Public Class TestProgressSheetGridView
        Inherits GridView

        Public Sub New(ownerGrid As GridControl)
            MyBase.New(ownerGrid)
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Friend Const CustomName As String = "TestProgressSheetGridView"
        Protected Overrides ReadOnly Property ViewName As String
            Get
                Return CustomName
            End Get
        End Property
    End Class
End Namespace

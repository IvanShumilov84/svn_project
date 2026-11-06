Option Strict On
Option Explicit On

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.Base.ViewInfo

Namespace CEL.TestProgress
    Public Class TestProgressSheetGridViewInfoRegistrator
        Inherits GridInfoRegistrator

        Public Overrides ReadOnly Property ViewName As String
            Get
                Return TestProgressSheetGridView.CustomName
            End Get
        End Property

        Public Overrides Function CreateView(grid As GridControl) As BaseView
            Return New TestProgressSheetGridView(grid)
        End Function

        Public Overrides Function CreateViewInfo(view As BaseView) As BaseViewInfo
            Return New TestProgressSheetGridViewInfo(CType(view, GridView))
        End Function
    End Class
End Namespace


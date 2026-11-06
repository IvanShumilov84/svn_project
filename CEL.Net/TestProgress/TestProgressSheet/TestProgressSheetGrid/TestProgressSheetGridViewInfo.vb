Option Strict On
Option Explicit On

Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Namespace CEL.TestProgress
    Public Class TestProgressSheetGridViewInfo
        Inherits GridViewInfo

        Public Sub New(gridView As GridView)
            MyBase.New(gridView)
        End Sub

        Public Overrides Sub CalcRects(bounds As System.Drawing.Rectangle, partital As Boolean)
            MyBase.CalcRects(bounds, partital)
            Dim g As System.Drawing.Graphics = GInfo.AddGraphics(Nothing)
            Try
                ViewRects.IndicatorWidth = Text.TextUtils.GetStringSize(g, "99:99", PaintAppearance.HeaderPanel.Font).Width + 21
            Finally
                GInfo.ReleaseGraphics()
            End Try
        End Sub
    End Class
End Namespace


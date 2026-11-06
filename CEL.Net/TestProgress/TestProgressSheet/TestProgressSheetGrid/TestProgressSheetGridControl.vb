Option Strict On
Option Explicit On

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Registrator

Namespace CEL.TestProgress
    Public Class TestProgressSheetGridControl
        Inherits GridControl

        Protected Overrides Sub RegisterAvailableViewsCore(collection As InfoCollection)
            MyBase.RegisterAvailableViewsCore(collection)
            collection.Add(New TestProgressSheetGridViewInfoRegistrator())
        End Sub
    End Class
End Namespace


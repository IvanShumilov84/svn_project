Option Strict On
Option Explicit On

Imports System.Collections.Generic
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Threading

Namespace CEL.TestProgress

    Public Class TestProgressSheetView
        Inherits System.Windows.Forms.Form

        Public Sub New(theTestProgressSheet As CEL.TestProgress.TestProgressSheet,
                       theTestProgressMgr As CEL.TestProgress.TestProgressMgr)
            Me.InitializeComponent()
            theTestProgressSheet.Entries.SynchronizationContext = SynchronizationContext.Current
            m_bindingSource.DataSource = theTestProgressSheet.Entries
            m_testProgressMgr = theTestProgressMgr
        End Sub

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.m_bindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.m_testProgressSheetGridControl = New CEL.TestProgress.TestProgressSheetGridControl()
            Me.m_testProgressSheetGridView = New CEL.TestProgress.TestProgressSheetGridView()
            Me.colResult = New DevExpress.XtraGrid.Columns.GridColumn()
            CType(Me.m_bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.m_testProgressSheetGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.m_testProgressSheetGridView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'm_testProgressSheetGridControl
            '
            Me.m_testProgressSheetGridControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.m_testProgressSheetGridControl.DataSource = Me.m_bindingSource
            Me.m_testProgressSheetGridControl.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.m_testProgressSheetGridControl.Location = New System.Drawing.Point(12, 12)
            Me.m_testProgressSheetGridControl.LookAndFeel.UseDefaultLookAndFeel = False
            Me.m_testProgressSheetGridControl.LookAndFeel.UseWindowsXPTheme = True
            Me.m_testProgressSheetGridControl.MainView = Me.m_testProgressSheetGridView
            Me.m_testProgressSheetGridControl.Name = "m_testProgressSheetGridControl"
            Me.m_testProgressSheetGridControl.Size = New System.Drawing.Size(1184, 321)
            Me.m_testProgressSheetGridControl.TabIndex = 1
            Me.m_testProgressSheetGridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.m_testProgressSheetGridView})
            '
            'm_testProgressSheetGridView
            '
            Me.m_testProgressSheetGridView.Appearance.FixedLine.BackColor = System.Drawing.Color.Blue
            Me.m_testProgressSheetGridView.Appearance.FixedLine.BackColor2 = System.Drawing.Color.Blue
            Me.m_testProgressSheetGridView.Appearance.FixedLine.BorderColor = System.Drawing.Color.Blue
            Me.m_testProgressSheetGridView.Appearance.FixedLine.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.m_testProgressSheetGridView.Appearance.FixedLine.ForeColor = System.Drawing.Color.White
            Me.m_testProgressSheetGridView.Appearance.FixedLine.Options.UseBackColor = True
            Me.m_testProgressSheetGridView.Appearance.FixedLine.Options.UseBorderColor = True
            Me.m_testProgressSheetGridView.Appearance.FixedLine.Options.UseFont = True
            Me.m_testProgressSheetGridView.Appearance.FixedLine.Options.UseForeColor = True
            Me.m_testProgressSheetGridView.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Blue
            Me.m_testProgressSheetGridView.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.Blue
            Me.m_testProgressSheetGridView.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Blue
            Me.m_testProgressSheetGridView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.m_testProgressSheetGridView.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White
            Me.m_testProgressSheetGridView.Appearance.HeaderPanel.Options.UseBackColor = True
            Me.m_testProgressSheetGridView.Appearance.HeaderPanel.Options.UseBorderColor = True
            Me.m_testProgressSheetGridView.Appearance.HeaderPanel.Options.UseFont = True
            Me.m_testProgressSheetGridView.Appearance.HeaderPanel.Options.UseForeColor = True
            Me.m_testProgressSheetGridView.Appearance.Preview.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.m_testProgressSheetGridView.Appearance.Preview.ForeColor = System.Drawing.Color.Black
            Me.m_testProgressSheetGridView.Appearance.Preview.Options.UseFont = True
            Me.m_testProgressSheetGridView.Appearance.Preview.Options.UseForeColor = True
            Me.m_testProgressSheetGridView.Appearance.Preview.Options.UseTextOptions = True
            Me.m_testProgressSheetGridView.Appearance.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            Me.m_testProgressSheetGridView.Appearance.Row.BorderColor = System.Drawing.Color.Blue
            Me.m_testProgressSheetGridView.Appearance.Row.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.m_testProgressSheetGridView.Appearance.Row.ForeColor = System.Drawing.Color.Black
            Me.m_testProgressSheetGridView.Appearance.Row.Options.UseBorderColor = True
            Me.m_testProgressSheetGridView.Appearance.Row.Options.UseFont = True
            Me.m_testProgressSheetGridView.Appearance.Row.Options.UseForeColor = True
            Me.m_testProgressSheetGridView.Appearance.VertLine.BackColor = System.Drawing.Color.Blue
            Me.m_testProgressSheetGridView.Appearance.VertLine.BackColor2 = System.Drawing.Color.Blue
            Me.m_testProgressSheetGridView.Appearance.VertLine.BorderColor = System.Drawing.Color.Blue
            Me.m_testProgressSheetGridView.Appearance.VertLine.Options.UseBackColor = True
            Me.m_testProgressSheetGridView.Appearance.VertLine.Options.UseBorderColor = True
            Me.m_testProgressSheetGridView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colResult})
            Me.m_testProgressSheetGridView.GridControl = Me.m_testProgressSheetGridControl
            Me.m_testProgressSheetGridView.Name = "m_testProgressSheetGridView"
            Me.m_testProgressSheetGridView.OptionsCustomization.AllowColumnMoving = False
            Me.m_testProgressSheetGridView.OptionsCustomization.AllowFilter = False
            Me.m_testProgressSheetGridView.OptionsCustomization.AllowGroup = False
            Me.m_testProgressSheetGridView.OptionsCustomization.AllowSort = False
            Me.m_testProgressSheetGridView.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.CheckAllDetails
            Me.m_testProgressSheetGridView.OptionsFind.AllowFindPanel = False
            Me.m_testProgressSheetGridView.OptionsMenu.EnableColumnMenu = False
            Me.m_testProgressSheetGridView.OptionsView.AutoCalcPreviewLineCount = True
            Me.m_testProgressSheetGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
            Me.m_testProgressSheetGridView.OptionsView.ShowDetailButtons = False
            Me.m_testProgressSheetGridView.OptionsView.ShowGroupPanel = False
            Me.m_testProgressSheetGridView.OptionsView.ShowPreview = True
            Me.m_testProgressSheetGridView.PaintStyleName = "Flat"
            Me.m_testProgressSheetGridView.PreviewIndent = 45
            '
            'colResult
            '
            Me.colResult.FieldName = "Result"
            Me.colResult.MinWidth = 60
            Me.colResult.Name = "colResult"
            Me.colResult.Visible = True
            Me.colResult.VisibleIndex = 0
            Me.colResult.Width = 641
            '
            'TestProgressSheetView
            '
            Me.BackColor = System.Drawing.Color.Black
            Me.ClientSize = New System.Drawing.Size(1208, 345)
            Me.Controls.Add(Me.m_testProgressSheetGridControl)
            Me.Name = "TestProgressSheetView"
            Me.ShowIcon = False
            Me.Text = "Test Progress Sheet View"
            CType(Me.m_bindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.m_testProgressSheetGridControl, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.m_testProgressSheetGridView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents m_testProgressSheetGridControl As TestProgressSheetGridControl
        Friend WithEvents m_testProgressSheetGridView As TestProgressSheetGridView
        Private components As System.ComponentModel.IContainer
        Friend WithEvents colResult As DevExpress.XtraGrid.Columns.GridColumn
        Friend WithEvents m_bindingSource As System.Windows.Forms.BindingSource
        Private m_testProgressMgr As CEL.TestProgress.TestProgressMgr

        Private Const CP_NOCLOSE_BUTTON As Integer = &H200
        Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
            Get
                Dim myCp As System.Windows.Forms.CreateParams = MyBase.CreateParams
                myCp.ClassStyle = (myCp.ClassStyle Or CP_NOCLOSE_BUTTON)
                Return myCp
            End Get
        End Property


        Public Property FocusedRowHandle As Integer
            Get
                Return Me.m_testProgressSheetGridView.FocusedRowHandle
            End Get
            Set(value As Integer)
                Me.m_testProgressSheetGridView.FocusedRowHandle = value
            End Set
        End Property

        Public Function GetFocusedDataSourceRowIndex() As Integer
            Return Me.m_testProgressSheetGridView.GetFocusedDataSourceRowIndex
        End Function

        Private Sub m_testProgressSheetGridView_CalcPreviewText(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventArgs) Handles m_testProgressSheetGridView.CalcPreviewText
            Dim checkItemEntries As TestProgressSheetEntryCollection = CType(m_bindingSource.DataSource, TestProgressSheetEntryCollection)
            Dim checkItemStr As New System.Text.StringBuilder
            Dim index As Integer = 0

            If e.RowHandle <> GridControl.NewItemRowHandle Then
                'If e.DataSourceRowIndex >= 0 AndAlso e.DataSourceRowIndex < checkItemEntries.Count Then
                Dim checkItemEntry As TestProgressSheetEntry = checkItemEntries.Item(e.DataSourceRowIndex)

                For Each str As String In checkItemEntry.CheckItemResults
                    If index > 0 Then
                        checkItemStr.AppendLine()
                    End If
                    checkItemStr.Append(str)
                    index += 1
                Next
            End If
            e.PreviewText = checkItemStr.ToString()
        End Sub

        Private Sub m_testProgressSheetGridView_CustomDrawRowIndicator(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs) Handles m_testProgressSheetGridView.CustomDrawRowIndicator
            If e.Info.IsRowIndicator AndAlso e.RowHandle >= 0 Then
                Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                Dim checkItemEntries As TestProgressSheetEntryCollection = CType(m_bindingSource.DataSource, TestProgressSheetEntryCollection)
                Dim checkItemEntry As TestProgressSheetEntry = checkItemEntries.Item(view.ViewRowHandleToDataSourceIndex(e.RowHandle))

                e.Info.DisplayText = checkItemEntry.DateTime
            End If
        End Sub

        Private Sub m_testProgressSheetGridView_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles m_testProgressSheetGridView.KeyDown
            If e.KeyCode = System.Windows.Forms.Keys.Delete Then
                Dim view As GridView = CType(sender, GridView)
                If view.FocusedRowHandle <> GridControl.NewItemRowHandle Then
                    If System.Windows.Forms.MessageBox.Show("Delete row?", "Confirmation", System.Windows.Forms.MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        view.DeleteRow(view.FocusedRowHandle)
                    End If
                End If
            ElseIf e.KeyCode = System.Windows.Forms.Keys.Insert Then
                Dim view As GridView = CType(sender, GridView)
                Dim checkItemEntries As TestProgressSheetEntryCollection = CType(m_bindingSource.DataSource, TestProgressSheetEntryCollection)
                If view.FocusedRowHandle <> GridControl.NewItemRowHandle And _
                    view.FocusedRowHandle <> GridControl.InvalidRowHandle Then
                    m_testProgressMgr.AddFreeFormTextEntry(String.Empty)
                End If
            End If
        End Sub
    End Class
End Namespace

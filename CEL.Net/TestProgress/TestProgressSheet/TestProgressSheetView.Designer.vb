<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestProgressSheetView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvTestProgressSheetGrid = New System.Windows.Forms.DataGridView()
        CType(Me.dgvTestProgressSheetGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvTestProgressSheetGrid
        '
        Me.dgvTestProgressSheetGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTestProgressSheetGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTestProgressSheetGrid.Location = New System.Drawing.Point(12, 12)
        Me.dgvTestProgressSheetGrid.Name = "dgvTestProgressSheetGrid"
        Me.dgvTestProgressSheetGrid.Size = New System.Drawing.Size(564, 430)
        Me.dgvTestProgressSheetGrid.TabIndex = 0
        '
        'TestProgressSheetView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(588, 454)
        Me.Controls.Add(Me.dgvTestProgressSheetGrid)
        Me.Name = "TestProgressSheetView"
        Me.Text = "TestProgressSheetView"
        CType(Me.dgvTestProgressSheetGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvTestProgressSheetGrid As System.Windows.Forms.DataGridView
End Class

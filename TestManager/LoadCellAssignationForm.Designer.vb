<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoadCellAssignationForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoadCellAssignationForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LoadCells1CollectionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LoadCells2CollectionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.m_dgvLoadCell2 = New System.Windows.Forms.DataGridView()
        Me.ProjectDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PlugDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SerialNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.m_dgvLoadCell1 = New System.Windows.Forms.DataGridView()
        Me.ProjectDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumberDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PlugDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SerialNumberDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        CType(Me.LoadCells1CollectionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LoadCells2CollectionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.m_dgvLoadCell2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.m_dgvLoadCell1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LoadCells1CollectionBindingSource
        '
        Me.LoadCells1CollectionBindingSource.AllowNew = False
        Me.LoadCells1CollectionBindingSource.DataSource = GetType(CEL.Data.BusinessObjects.LoadCellsCollection)
        '
        'LoadCells2CollectionBindingSource
        '
        Me.LoadCells2CollectionBindingSource.AllowNew = False
        Me.LoadCells2CollectionBindingSource.DataSource = GetType(CEL.Data.BusinessObjects.LoadCellsCollection)
        '
        'GroupControl1
        '
        Me.GroupControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupControl1.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.GroupControl1.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl1.Appearance.Options.UseBackColor = True
        Me.GroupControl1.Appearance.Options.UseBorderColor = True
        Me.GroupControl1.Controls.Add(Me.PictureEdit1)
        Me.GroupControl1.Controls.Add(Me.btnClose)
        Me.GroupControl1.Controls.Add(Me.btnCancel)
        Me.GroupControl1.Controls.Add(Me.btnSave)
        Me.GroupControl1.Controls.Add(Me.GroupControl3)
        Me.GroupControl1.Controls.Add(Me.GroupControl2)
        Me.GroupControl1.Controls.Add(Me.Label3)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.lblCopyright)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(948, 779)
        Me.GroupControl1.TabIndex = 105
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(25, 21)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.PictureEdit1.Size = New System.Drawing.Size(119, 100)
        Me.PictureEdit1.TabIndex = 125
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(866, 744)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 124
        Me.btnClose.Text = "Close"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(785, 744)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 123
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(704, 744)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 122
        Me.btnSave.Text = "Save"
        '
        'GroupControl3
        '
        Me.GroupControl3.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.GroupControl3.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.GroupControl3.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl3.Appearance.Options.UseBackColor = True
        Me.GroupControl3.Appearance.Options.UseBorderColor = True
        Me.GroupControl3.Controls.Add(Me.m_dgvLoadCell2)
        Me.GroupControl3.Location = New System.Drawing.Point(482, 154)
        Me.GroupControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(437, 573)
        Me.GroupControl3.TabIndex = 121
        '
        'm_dgvLoadCell2
        '
        Me.m_dgvLoadCell2.AllowUserToAddRows = False
        Me.m_dgvLoadCell2.AllowUserToDeleteRows = False
        Me.m_dgvLoadCell2.AllowUserToResizeColumns = False
        Me.m_dgvLoadCell2.AllowUserToResizeRows = False
        Me.m_dgvLoadCell2.AutoGenerateColumns = False
        Me.m_dgvLoadCell2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.m_dgvLoadCell2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ProjectDataGridViewTextBoxColumn, Me.NumberDataGridViewTextBoxColumn, Me.PlugDataGridViewTextBoxColumn, Me.SourceDataGridViewTextBoxColumn, Me.SerialNumberDataGridViewTextBoxColumn})
        Me.m_dgvLoadCell2.DataSource = Me.LoadCells2CollectionBindingSource
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.m_dgvLoadCell2.DefaultCellStyle = DataGridViewCellStyle1
        Me.m_dgvLoadCell2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_dgvLoadCell2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.m_dgvLoadCell2.Location = New System.Drawing.Point(3, 18)
        Me.m_dgvLoadCell2.MultiSelect = False
        Me.m_dgvLoadCell2.Name = "m_dgvLoadCell2"
        Me.m_dgvLoadCell2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.m_dgvLoadCell2.Size = New System.Drawing.Size(431, 552)
        Me.m_dgvLoadCell2.TabIndex = 117
        '
        'ProjectDataGridViewTextBoxColumn
        '
        Me.ProjectDataGridViewTextBoxColumn.DataPropertyName = "Project"
        Me.ProjectDataGridViewTextBoxColumn.HeaderText = "Project"
        Me.ProjectDataGridViewTextBoxColumn.Name = "ProjectDataGridViewTextBoxColumn"
        Me.ProjectDataGridViewTextBoxColumn.Visible = False
        '
        'NumberDataGridViewTextBoxColumn
        '
        Me.NumberDataGridViewTextBoxColumn.DataPropertyName = "Number"
        Me.NumberDataGridViewTextBoxColumn.HeaderText = "Number"
        Me.NumberDataGridViewTextBoxColumn.Name = "NumberDataGridViewTextBoxColumn"
        Me.NumberDataGridViewTextBoxColumn.Visible = False
        '
        'PlugDataGridViewTextBoxColumn
        '
        Me.PlugDataGridViewTextBoxColumn.DataPropertyName = "Plug"
        Me.PlugDataGridViewTextBoxColumn.HeaderText = "Plug"
        Me.PlugDataGridViewTextBoxColumn.Name = "PlugDataGridViewTextBoxColumn"
        Me.PlugDataGridViewTextBoxColumn.Visible = False
        '
        'SourceDataGridViewTextBoxColumn
        '
        Me.SourceDataGridViewTextBoxColumn.DataPropertyName = "Source"
        Me.SourceDataGridViewTextBoxColumn.HeaderText = "Source"
        Me.SourceDataGridViewTextBoxColumn.Name = "SourceDataGridViewTextBoxColumn"
        Me.SourceDataGridViewTextBoxColumn.ReadOnly = True
        '
        'SerialNumberDataGridViewTextBoxColumn
        '
        Me.SerialNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SerialNumberDataGridViewTextBoxColumn.DataPropertyName = "SerialNumber"
        Me.SerialNumberDataGridViewTextBoxColumn.HeaderText = "SerialNumber"
        Me.SerialNumberDataGridViewTextBoxColumn.Name = "SerialNumberDataGridViewTextBoxColumn"
        '
        'GroupControl2
        '
        Me.GroupControl2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.GroupControl2.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.GroupControl2.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl2.Appearance.Options.UseBackColor = True
        Me.GroupControl2.Appearance.Options.UseBorderColor = True
        Me.GroupControl2.Controls.Add(Me.m_dgvLoadCell1)
        Me.GroupControl2.Location = New System.Drawing.Point(25, 154)
        Me.GroupControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(437, 573)
        Me.GroupControl2.TabIndex = 120
        '
        'm_dgvLoadCell1
        '
        Me.m_dgvLoadCell1.AllowUserToAddRows = False
        Me.m_dgvLoadCell1.AllowUserToDeleteRows = False
        Me.m_dgvLoadCell1.AllowUserToResizeColumns = False
        Me.m_dgvLoadCell1.AllowUserToResizeRows = False
        Me.m_dgvLoadCell1.AutoGenerateColumns = False
        Me.m_dgvLoadCell1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.m_dgvLoadCell1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ProjectDataGridViewTextBoxColumn1, Me.NumberDataGridViewTextBoxColumn1, Me.PlugDataGridViewTextBoxColumn1, Me.SourceDataGridViewTextBoxColumn1, Me.SerialNumberDataGridViewTextBoxColumn1})
        Me.m_dgvLoadCell1.DataSource = Me.LoadCells1CollectionBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.m_dgvLoadCell1.DefaultCellStyle = DataGridViewCellStyle2
        Me.m_dgvLoadCell1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_dgvLoadCell1.Location = New System.Drawing.Point(3, 18)
        Me.m_dgvLoadCell1.MultiSelect = False
        Me.m_dgvLoadCell1.Name = "m_dgvLoadCell1"
        Me.m_dgvLoadCell1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.m_dgvLoadCell1.Size = New System.Drawing.Size(431, 552)
        Me.m_dgvLoadCell1.TabIndex = 115
        '
        'ProjectDataGridViewTextBoxColumn1
        '
        Me.ProjectDataGridViewTextBoxColumn1.DataPropertyName = "Project"
        Me.ProjectDataGridViewTextBoxColumn1.HeaderText = "Project"
        Me.ProjectDataGridViewTextBoxColumn1.Name = "ProjectDataGridViewTextBoxColumn1"
        Me.ProjectDataGridViewTextBoxColumn1.Visible = False
        '
        'NumberDataGridViewTextBoxColumn1
        '
        Me.NumberDataGridViewTextBoxColumn1.DataPropertyName = "Number"
        Me.NumberDataGridViewTextBoxColumn1.HeaderText = "Number"
        Me.NumberDataGridViewTextBoxColumn1.Name = "NumberDataGridViewTextBoxColumn1"
        Me.NumberDataGridViewTextBoxColumn1.Visible = False
        '
        'PlugDataGridViewTextBoxColumn1
        '
        Me.PlugDataGridViewTextBoxColumn1.DataPropertyName = "Plug"
        Me.PlugDataGridViewTextBoxColumn1.HeaderText = "Plug"
        Me.PlugDataGridViewTextBoxColumn1.Name = "PlugDataGridViewTextBoxColumn1"
        Me.PlugDataGridViewTextBoxColumn1.Visible = False
        '
        'SourceDataGridViewTextBoxColumn1
        '
        Me.SourceDataGridViewTextBoxColumn1.DataPropertyName = "Source"
        Me.SourceDataGridViewTextBoxColumn1.HeaderText = "Source"
        Me.SourceDataGridViewTextBoxColumn1.Name = "SourceDataGridViewTextBoxColumn1"
        Me.SourceDataGridViewTextBoxColumn1.ReadOnly = True
        '
        'SerialNumberDataGridViewTextBoxColumn1
        '
        Me.SerialNumberDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SerialNumberDataGridViewTextBoxColumn1.DataPropertyName = "SerialNumber"
        Me.SerialNumberDataGridViewTextBoxColumn1.HeaderText = "SerialNumber"
        Me.SerialNumberDataGridViewTextBoxColumn1.Name = "SerialNumberDataGridViewTextBoxColumn1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial Black", 15.75!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(477, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 30)
        Me.Label3.TabIndex = 118
        Me.Label3.Text = "Plug 2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial Black", 15.75!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(30, 124)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 30)
        Me.Label2.TabIndex = 117
        Me.Label2.Text = "Plug 1"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial Black", 26.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(203, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(563, 50)
        Me.Label1.TabIndex = 112
        Me.Label1.Text = "Load Cell Assignment Editor"
        '
        'lblCopyright
        '
        Me.lblCopyright.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.BackColor = System.Drawing.Color.Transparent
        Me.lblCopyright.ForeColor = System.Drawing.Color.White
        Me.lblCopyright.Location = New System.Drawing.Point(6, 763)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(215, 13)
        Me.lblCopyright.TabIndex = 111
        Me.lblCopyright.Text = "© 2018 CEL Aerospace Test Equipment Ltd"
        '
        'LoadCellAssignationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(948, 779)
        Me.Controls.Add(Me.GroupControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "LoadCellAssignationForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Load Cell Assignment Editor"
        CType(Me.LoadCells1CollectionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LoadCells2CollectionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.m_dgvLoadCell2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.m_dgvLoadCell1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents LoadCells1CollectionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LoadCells2CollectionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents m_dgvLoadCell2 As System.Windows.Forms.DataGridView
    Friend WithEvents ProjectDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PlugDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SourceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SerialNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents m_dgvLoadCell1 As System.Windows.Forms.DataGridView
    Friend WithEvents ProjectDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NumberDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PlugDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SourceDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SerialNumberDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
End Class

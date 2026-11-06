<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserEditorForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserEditorForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.m_SecurityLevelCollection = New CEL.Data.BusinessObjects.SecurityLevelCollection()
        Me.UsersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_UsersCollection = New CEL.Data.BusinessObjects.UsersCollection()
        Me.SecurityLevelBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.dgvUsers = New System.Windows.Forms.DataGridView()
        Me.ProjectIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OperatorIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PasswordDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OperatorNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SecurityLevelIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.UsersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SecurityLevelBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'm_SecurityLevelCollection
        '
        Me.m_SecurityLevelCollection.AllowDelete = True
        Me.m_SecurityLevelCollection.AllowEdit = True
        Me.m_SecurityLevelCollection.AllowNew = True
        Me.m_SecurityLevelCollection.EnableHierarchicalBinding = False
        Me.m_SecurityLevelCollection.Filter = Nothing
        '
        'UsersBindingSource
        '
        Me.UsersBindingSource.DataSource = Me.m_UsersCollection
        '
        'm_UsersCollection
        '
        Me.m_UsersCollection.AllowDelete = True
        Me.m_UsersCollection.AllowEdit = True
        Me.m_UsersCollection.AllowNew = True
        Me.m_UsersCollection.EnableHierarchicalBinding = False
        Me.m_UsersCollection.Filter = Nothing
        '
        'SecurityLevelBindingSource
        '
        Me.SecurityLevelBindingSource.DataSource = Me.m_SecurityLevelCollection
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
        Me.GroupControl1.Controls.Add(Me.GroupControl2)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.btnClose)
        Me.GroupControl1.Controls.Add(Me.btnCancel)
        Me.GroupControl1.Controls.Add(Me.btnSave)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(710, 552)
        Me.GroupControl1.TabIndex = 114
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(15, 21)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.PictureEdit1.Size = New System.Drawing.Size(119, 100)
        Me.PictureEdit1.TabIndex = 119
        '
        'GroupControl2
        '
        Me.GroupControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupControl2.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.GroupControl2.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl2.Appearance.Options.UseBackColor = True
        Me.GroupControl2.Appearance.Options.UseBorderColor = True
        Me.GroupControl2.Controls.Add(Me.dgvUsers)
        Me.GroupControl2.Location = New System.Drawing.Point(12, 125)
        Me.GroupControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(686, 370)
        Me.GroupControl2.TabIndex = 118
        '
        'dgvUsers
        '
        Me.dgvUsers.AutoGenerateColumns = False
        Me.dgvUsers.BackgroundColor = System.Drawing.Color.Black
        Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUsers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ProjectIDDataGridViewTextBoxColumn, Me.OperatorIDDataGridViewTextBoxColumn, Me.PasswordDataGridViewTextBoxColumn, Me.OperatorNameDataGridViewTextBoxColumn, Me.SecurityLevelIDDataGridViewTextBoxColumn})
        Me.dgvUsers.DataSource = Me.UsersBindingSource
        Me.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUsers.Location = New System.Drawing.Point(3, 18)
        Me.dgvUsers.Name = "dgvUsers"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.dgvUsers.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvUsers.Size = New System.Drawing.Size(680, 349)
        Me.dgvUsers.TabIndex = 118
        '
        'ProjectIDDataGridViewTextBoxColumn
        '
        Me.ProjectIDDataGridViewTextBoxColumn.DataPropertyName = "ProjectID"
        Me.ProjectIDDataGridViewTextBoxColumn.HeaderText = "ProjectID"
        Me.ProjectIDDataGridViewTextBoxColumn.Name = "ProjectIDDataGridViewTextBoxColumn"
        Me.ProjectIDDataGridViewTextBoxColumn.Visible = False
        '
        'OperatorIDDataGridViewTextBoxColumn
        '
        Me.OperatorIDDataGridViewTextBoxColumn.DataPropertyName = "OperatorID"
        Me.OperatorIDDataGridViewTextBoxColumn.HeaderText = "OperatorID"
        Me.OperatorIDDataGridViewTextBoxColumn.Name = "OperatorIDDataGridViewTextBoxColumn"
        '
        'PasswordDataGridViewTextBoxColumn
        '
        Me.PasswordDataGridViewTextBoxColumn.DataPropertyName = "Password"
        Me.PasswordDataGridViewTextBoxColumn.HeaderText = "Password"
        Me.PasswordDataGridViewTextBoxColumn.Name = "PasswordDataGridViewTextBoxColumn"
        '
        'OperatorNameDataGridViewTextBoxColumn
        '
        Me.OperatorNameDataGridViewTextBoxColumn.DataPropertyName = "OperatorName"
        Me.OperatorNameDataGridViewTextBoxColumn.FillWeight = 200.0!
        Me.OperatorNameDataGridViewTextBoxColumn.HeaderText = "OperatorName"
        Me.OperatorNameDataGridViewTextBoxColumn.Name = "OperatorNameDataGridViewTextBoxColumn"
        Me.OperatorNameDataGridViewTextBoxColumn.Width = 200
        '
        'SecurityLevelIDDataGridViewTextBoxColumn
        '
        Me.SecurityLevelIDDataGridViewTextBoxColumn.DataPropertyName = "SecurityLevelID"
        Me.SecurityLevelIDDataGridViewTextBoxColumn.DataSource = Me.m_SecurityLevelCollection
        Me.SecurityLevelIDDataGridViewTextBoxColumn.DisplayMember = "Description"
        Me.SecurityLevelIDDataGridViewTextBoxColumn.FillWeight = 150.0!
        Me.SecurityLevelIDDataGridViewTextBoxColumn.HeaderText = "SecurityLevelID"
        Me.SecurityLevelIDDataGridViewTextBoxColumn.Name = "SecurityLevelIDDataGridViewTextBoxColumn"
        Me.SecurityLevelIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SecurityLevelIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.SecurityLevelIDDataGridViewTextBoxColumn.ValueMember = "SecurityLevelID"
        Me.SecurityLevelIDDataGridViewTextBoxColumn.Width = 150
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial Black", 26.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(230, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(260, 50)
        Me.Label1.TabIndex = 116
        Me.Label1.Text = "Users Editor"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(623, 519)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 115
        Me.btnClose.Text = "Close"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(542, 519)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 114
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(461, 519)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 113
        Me.btnSave.Text = "Save"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(6, 536)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(215, 13)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "© 2018 CEL Aerospace Test Equipment Ltd"
        '
        'UserEditorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(710, 552)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "UserEditorForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Users Editor"
        CType(Me.UsersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SecurityLevelBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_UsersCollection As CEL.Data.BusinessObjects.UsersCollection
    Friend WithEvents UsersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SecurityLevelDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SecurityLevelBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents m_SecurityLevelCollection As CEL.Data.BusinessObjects.SecurityLevelCollection
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgvUsers As System.Windows.Forms.DataGridView
    Friend WithEvents ProjectIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OperatorIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PasswordDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OperatorNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SecurityLevelIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
End Class

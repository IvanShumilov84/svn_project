
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogReportDetailForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LogReportDetailForm))
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnMoveLast = New System.Windows.Forms.Button()
        Me.btnMoveFirst = New System.Windows.Forms.Button()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.btnAddLeft = New System.Windows.Forms.Button()
        Me.btnAddRight = New System.Windows.Forms.Button()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.btnConfirm = New DevExpress.XtraEditors.SimpleButton()
        Me.cboPartNumber = New System.Windows.Forms.ComboBox()
        Me.lblPartNumber = New System.Windows.Forms.Label()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.dgvLeft = New System.Windows.Forms.DataGridView()
        Me.colChannelName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFormat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.dgvRight = New System.Windows.Forms.DataGridView()
        Me.colCategoryRight = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colCategoryViewOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChannelNameRight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescriptionRight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUnitRight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFormatRight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colViewOrderInCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnEditCategories = New DevExpress.XtraEditors.SimpleButton()
        Me.lblEngineSelection = New System.Windows.Forms.Label()
        Me.cboEngine = New System.Windows.Forms.ComboBox()
        Me.lblLogReportEditor = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.dgvLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.dgvRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnMoveLast
        '
        Me.btnMoveLast.BackColor = System.Drawing.Color.Transparent
        Me.btnMoveLast.BackgroundImage = Global.TestManager.My.Resources.Resources.arrow_down_3
        Me.btnMoveLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMoveLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMoveLast.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMoveLast.Location = New System.Drawing.Point(632, 631)
        Me.btnMoveLast.Name = "btnMoveLast"
        Me.btnMoveLast.Size = New System.Drawing.Size(75, 67)
        Me.btnMoveLast.TabIndex = 122
        Me.ToolTip1.SetToolTip(Me.btnMoveLast, "Move Last")
        Me.btnMoveLast.UseVisualStyleBackColor = False
        '
        'btnMoveFirst
        '
        Me.btnMoveFirst.BackColor = System.Drawing.Color.Transparent
        Me.btnMoveFirst.BackgroundImage = Global.TestManager.My.Resources.Resources.arrow_up_3
        Me.btnMoveFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMoveFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMoveFirst.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMoveFirst.Location = New System.Drawing.Point(632, 442)
        Me.btnMoveFirst.Name = "btnMoveFirst"
        Me.btnMoveFirst.Size = New System.Drawing.Size(75, 67)
        Me.btnMoveFirst.TabIndex = 121
        Me.ToolTip1.SetToolTip(Me.btnMoveFirst, "Mode First")
        Me.btnMoveFirst.UseVisualStyleBackColor = False
        '
        'btnMoveDown
        '
        Me.btnMoveDown.BackColor = System.Drawing.Color.Transparent
        Me.btnMoveDown.BackgroundImage = Global.TestManager.My.Resources.Resources.arrow_down
        Me.btnMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMoveDown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMoveDown.Location = New System.Drawing.Point(632, 568)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(75, 67)
        Me.btnMoveDown.TabIndex = 120
        Me.ToolTip1.SetToolTip(Me.btnMoveDown, "Move Down")
        Me.btnMoveDown.UseVisualStyleBackColor = False
        '
        'btnMoveUp
        '
        Me.btnMoveUp.BackColor = System.Drawing.Color.Transparent
        Me.btnMoveUp.BackgroundImage = Global.TestManager.My.Resources.Resources.arrow_up
        Me.btnMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMoveUp.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMoveUp.Location = New System.Drawing.Point(632, 505)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(75, 67)
        Me.btnMoveUp.TabIndex = 119
        Me.ToolTip1.SetToolTip(Me.btnMoveUp, "Move Up")
        Me.btnMoveUp.UseVisualStyleBackColor = False
        '
        'btnAddLeft
        '
        Me.btnAddLeft.BackColor = System.Drawing.Color.Transparent
        Me.btnAddLeft.BackgroundImage = Global.TestManager.My.Resources.Resources.arrow_left
        Me.btnAddLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnAddLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddLeft.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddLeft.Location = New System.Drawing.Point(632, 347)
        Me.btnAddLeft.Name = "btnAddLeft"
        Me.btnAddLeft.Size = New System.Drawing.Size(75, 67)
        Me.btnAddLeft.TabIndex = 116
        Me.ToolTip1.SetToolTip(Me.btnAddLeft, "Mode Left")
        Me.btnAddLeft.UseVisualStyleBackColor = False
        '
        'btnAddRight
        '
        Me.btnAddRight.BackColor = System.Drawing.Color.Transparent
        Me.btnAddRight.BackgroundImage = Global.TestManager.My.Resources.Resources.arrow_right
        Me.btnAddRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnAddRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddRight.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddRight.Location = New System.Drawing.Point(632, 276)
        Me.btnAddRight.Name = "btnAddRight"
        Me.btnAddRight.Size = New System.Drawing.Size(75, 67)
        Me.btnAddRight.TabIndex = 115
        Me.ToolTip1.SetToolTip(Me.btnAddRight, "Mode Right")
        Me.btnAddRight.UseVisualStyleBackColor = False
        '
        'GroupControl1
        '
        Me.GroupControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupControl1.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.GroupControl1.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl1.Appearance.Options.UseBackColor = True
        Me.GroupControl1.Appearance.Options.UseBorderColor = True
        Me.GroupControl1.Controls.Add(Me.btnConfirm)
        Me.GroupControl1.Controls.Add(Me.cboPartNumber)
        Me.GroupControl1.Controls.Add(Me.lblPartNumber)
        Me.GroupControl1.Controls.Add(Me.PictureEdit1)
        Me.GroupControl1.Controls.Add(Me.GroupControl3)
        Me.GroupControl1.Controls.Add(Me.GroupControl2)
        Me.GroupControl1.Controls.Add(Me.btnEditCategories)
        Me.GroupControl1.Controls.Add(Me.btnMoveLast)
        Me.GroupControl1.Controls.Add(Me.btnMoveFirst)
        Me.GroupControl1.Controls.Add(Me.btnMoveDown)
        Me.GroupControl1.Controls.Add(Me.btnMoveUp)
        Me.GroupControl1.Controls.Add(Me.btnAddLeft)
        Me.GroupControl1.Controls.Add(Me.btnAddRight)
        Me.GroupControl1.Controls.Add(Me.lblEngineSelection)
        Me.GroupControl1.Controls.Add(Me.cboEngine)
        Me.GroupControl1.Controls.Add(Me.lblLogReportEditor)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.btnClose)
        Me.GroupControl1.Controls.Add(Me.btnSave)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1406, 816)
        Me.GroupControl1.TabIndex = 114
        '
        'btnConfirm
        '
        Me.btnConfirm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfirm.Location = New System.Drawing.Point(843, 77)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(102, 23)
        Me.btnConfirm.TabIndex = 135
        Me.btnConfirm.Text = "Confirm"
        '
        'cboPartNumber
        '
        Me.cboPartNumber.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboPartNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPartNumber.FormattingEnabled = True
        Me.cboPartNumber.Location = New System.Drawing.Point(600, 110)
        Me.cboPartNumber.Name = "cboPartNumber"
        Me.cboPartNumber.Size = New System.Drawing.Size(227, 22)
        Me.cboPartNumber.TabIndex = 134
        '
        'lblPartNumber
        '
        Me.lblPartNumber.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblPartNumber.AutoSize = True
        Me.lblPartNumber.BackColor = System.Drawing.Color.Transparent
        Me.lblPartNumber.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPartNumber.ForeColor = System.Drawing.Color.White
        Me.lblPartNumber.Location = New System.Drawing.Point(431, 106)
        Me.lblPartNumber.Name = "lblPartNumber"
        Me.lblPartNumber.Size = New System.Drawing.Size(116, 22)
        Me.lblPartNumber.TabIndex = 133
        Me.lblPartNumber.Text = "Part Number"
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(12, 18)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.PictureEdit1.Size = New System.Drawing.Size(119, 100)
        Me.PictureEdit1.TabIndex = 113
        '
        'GroupControl3
        '
        Me.GroupControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupControl3.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.GroupControl3.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl3.Appearance.Options.UseBackColor = True
        Me.GroupControl3.Appearance.Options.UseBorderColor = True
        Me.GroupControl3.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupControl3.AppearanceCaption.ForeColor = System.Drawing.Color.Silver
        Me.GroupControl3.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3.AppearanceCaption.Options.UseForeColor = True
        Me.GroupControl3.Controls.Add(Me.dgvLeft)
        Me.GroupControl3.Location = New System.Drawing.Point(12, 168)
        Me.GroupControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(611, 601)
        Me.GroupControl3.TabIndex = 132
        Me.GroupControl3.Text = "Channels Available"
        '
        'dgvLeft
        '
        Me.dgvLeft.AllowUserToAddRows = False
        Me.dgvLeft.AllowUserToDeleteRows = False
        Me.dgvLeft.AllowUserToOrderColumns = True
        Me.dgvLeft.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLeft.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colChannelName, Me.colDescription, Me.colUnit, Me.colFormat})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvLeft.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvLeft.Location = New System.Drawing.Point(10, 25)
        Me.dgvLeft.Name = "dgvLeft"
        Me.dgvLeft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLeft.Size = New System.Drawing.Size(595, 567)
        Me.dgvLeft.TabIndex = 1
        '
        'colChannelName
        '
        Me.colChannelName.HeaderText = "Channel Name"
        Me.colChannelName.Name = "colChannelName"
        Me.colChannelName.ReadOnly = True
        Me.colChannelName.Width = 200
        '
        'colDescription
        '
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.ReadOnly = True
        Me.colDescription.Width = 225
        '
        'colUnit
        '
        Me.colUnit.HeaderText = "Unit"
        Me.colUnit.Name = "colUnit"
        Me.colUnit.ReadOnly = True
        Me.colUnit.Width = 50
        '
        'colFormat
        '
        Me.colFormat.HeaderText = "Format"
        Me.colFormat.Name = "colFormat"
        Me.colFormat.Width = 75
        '
        'GroupControl2
        '
        Me.GroupControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupControl2.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.GroupControl2.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl2.Appearance.Options.UseBackColor = True
        Me.GroupControl2.Appearance.Options.UseBorderColor = True
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.Silver
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.AppearanceCaption.Options.UseForeColor = True
        Me.GroupControl2.Controls.Add(Me.dgvRight)
        Me.GroupControl2.Location = New System.Drawing.Point(719, 168)
        Me.GroupControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(668, 601)
        Me.GroupControl2.TabIndex = 131
        Me.GroupControl2.Text = "Log Report Layout"
        '
        'dgvRight
        '
        Me.dgvRight.AllowDrop = True
        Me.dgvRight.AllowUserToAddRows = False
        Me.dgvRight.AllowUserToDeleteRows = False
        Me.dgvRight.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRight.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCategoryRight, Me.colCategoryViewOrder, Me.colChannelNameRight, Me.colDescriptionRight, Me.colUnitRight, Me.colFormatRight, Me.colViewOrderInCategory})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvRight.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgvRight.Location = New System.Drawing.Point(13, 23)
        Me.dgvRight.Name = "dgvRight"
        Me.dgvRight.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRight.Size = New System.Drawing.Size(645, 569)
        Me.dgvRight.TabIndex = 2
        '
        'colCategoryRight
        '
        Me.colCategoryRight.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.colCategoryRight.HeaderText = "Category"
        Me.colCategoryRight.Name = "colCategoryRight"
        Me.colCategoryRight.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colCategoryViewOrder
        '
        Me.colCategoryViewOrder.HeaderText = "Category View Order"
        Me.colCategoryViewOrder.Name = "colCategoryViewOrder"
        Me.colCategoryViewOrder.Visible = False
        '
        'colChannelNameRight
        '
        Me.colChannelNameRight.HeaderText = "Channel Name"
        Me.colChannelNameRight.Name = "colChannelNameRight"
        Me.colChannelNameRight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colChannelNameRight.Width = 150
        '
        'colDescriptionRight
        '
        Me.colDescriptionRight.HeaderText = "Description"
        Me.colDescriptionRight.Name = "colDescriptionRight"
        Me.colDescriptionRight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colDescriptionRight.Width = 225
        '
        'colUnitRight
        '
        Me.colUnitRight.HeaderText = "Unit"
        Me.colUnitRight.Name = "colUnitRight"
        Me.colUnitRight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colUnitRight.Width = 50
        '
        'colFormatRight
        '
        Me.colFormatRight.HeaderText = "Format"
        Me.colFormatRight.Name = "colFormatRight"
        Me.colFormatRight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colFormatRight.Width = 75
        '
        'colViewOrderInCategory
        '
        Me.colViewOrderInCategory.HeaderText = "Order In Category"
        Me.colViewOrderInCategory.Name = "colViewOrderInCategory"
        Me.colViewOrderInCategory.Visible = False
        '
        'btnEditCategories
        '
        Me.btnEditCategories.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditCategories.Location = New System.Drawing.Point(1069, 779)
        Me.btnEditCategories.Name = "btnEditCategories"
        Me.btnEditCategories.Size = New System.Drawing.Size(102, 23)
        Me.btnEditCategories.TabIndex = 129
        Me.btnEditCategories.Text = "Edit Categories"
        '

        'lblEngineSelection
        '
        Me.lblEngineSelection.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblEngineSelection.AutoSize = True
        Me.lblEngineSelection.BackColor = System.Drawing.Color.Transparent
        Me.lblEngineSelection.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEngineSelection.ForeColor = System.Drawing.Color.White
        Me.lblEngineSelection.Location = New System.Drawing.Point(431, 73)
        Me.lblEngineSelection.Name = "lblEngineSelection"
        Me.lblEngineSelection.Size = New System.Drawing.Size(153, 22)
        Me.lblEngineSelection.TabIndex = 114
        Me.lblEngineSelection.Text = "Engine Selection"
        '
        'cboEngine
        '
        Me.cboEngine.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboEngine.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEngine.FormattingEnabled = True
        Me.cboEngine.Location = New System.Drawing.Point(600, 77)
        Me.cboEngine.Name = "cboEngine"
        Me.cboEngine.Size = New System.Drawing.Size(227, 22)
        Me.cboEngine.TabIndex = 113
        '
        'lblLogReportEditor
        '
        Me.lblLogReportEditor.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblLogReportEditor.AutoSize = True
        Me.lblLogReportEditor.BackColor = System.Drawing.Color.Transparent
        Me.lblLogReportEditor.Font = New System.Drawing.Font("Arial Black", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogReportEditor.ForeColor = System.Drawing.Color.White
        Me.lblLogReportEditor.Location = New System.Drawing.Point(461, 18)
        Me.lblLogReportEditor.Name = "lblLogReportEditor"
        Me.lblLogReportEditor.Size = New System.Drawing.Size(359, 50)
        Me.lblLogReportEditor.TabIndex = 112
        Me.lblLogReportEditor.Text = "Log Report Editor"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(6, 798)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(215, 13)
        Me.Label1.TabIndex = 111
        Me.Label1.Text = "© 2018 CEL Aerospace Test Equipment Ltd"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(1285, 779)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(102, 23)
        Me.btnClose.TabIndex = 109
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(1177, 779)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(102, 23)
        Me.btnSave.TabIndex = 108
        Me.btnSave.Text = "Save"
        '
        'LogReportDetailForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1406, 816)
        Me.Controls.Add(Me.GroupControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "LogReportDetailForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Log Report Editor"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.dgvLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.dgvRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnMoveLast As System.Windows.Forms.Button
    Friend WithEvents btnMoveFirst As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnAddLeft As System.Windows.Forms.Button
    Friend WithEvents btnAddRight As System.Windows.Forms.Button
    Friend WithEvents lblEngineSelection As System.Windows.Forms.Label
    Friend WithEvents cboEngine As System.Windows.Forms.ComboBox
    Friend WithEvents lblLogReportEditor As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnEditCategories As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgvLeft As System.Windows.Forms.DataGridView
    Friend WithEvents colChannelName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUnit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFormat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dgvRight As System.Windows.Forms.DataGridView
    Friend WithEvents colCategoryRight As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colCategoryViewOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChannelNameRight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescriptionRight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUnitRight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFormatRight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colViewOrderInCategory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents btnConfirm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cboPartNumber As System.Windows.Forms.ComboBox
    Friend WithEvents lblPartNumber As System.Windows.Forms.Label
End Class


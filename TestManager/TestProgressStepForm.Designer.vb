<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestProgressStepForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestProgressStepForm))
        Me.TestProgressStepTypeCollectionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_cmsTestProgressStep = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.m_cmsTestProgressStepCutMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_cmsTestProgressStepCopyMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_cmsTestProgressStepPasteMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TestProgressCheckTypeCollectionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TestProgressCheckListItemCollectionByTestStepIDBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.m_btnMoveLast = New System.Windows.Forms.Button()
        Me.m_btnMoveFirst = New System.Windows.Forms.Button()
        Me.m_btnMoveDown = New System.Windows.Forms.Button()
        Me.m_btnMoveUp = New System.Windows.Forms.Button()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.m_btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.m_btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.m_btnChannelSelect = New System.Windows.Forms.Button()
        Me.m_dgvNumericChannels = New System.Windows.Forms.DataGridView()
        Me.ChannelName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Units = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.m_dgvTestProgressTestStepCheckListItems = New System.Windows.Forms.DataGridView()
        Me.Parameter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LowLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HighLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheckTypeID = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.m_dgvTestProgressStep = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.lblEngineSelection = New System.Windows.Forms.Label()
        Me.m_cboEngine = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.TestProgressStepTypeCollectionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.m_cmsTestProgressStep.SuspendLayout()
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TestProgressCheckTypeCollectionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TestProgressCheckListItemCollectionByTestStepIDBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.m_dgvNumericChannels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.m_dgvTestProgressTestStepCheckListItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.m_dgvTestProgressStep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TestProgressStepTypeCollectionBindingSource
        '
        Me.TestProgressStepTypeCollectionBindingSource.DataSource = GetType(CEL.Data.BusinessObjects.TestProgressStepTypeCollection)
        '
        'm_cmsTestProgressStep
        '
        Me.m_cmsTestProgressStep.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_cmsTestProgressStepCutMenuItem, Me.m_cmsTestProgressStepCopyMenuItem, Me.m_cmsTestProgressStepPasteMenuItem})
        Me.m_cmsTestProgressStep.Name = "m_cmsTestProgressStep"
        Me.m_cmsTestProgressStep.Size = New System.Drawing.Size(145, 70)
        '
        'm_cmsTestProgressStepCutMenuItem
        '
        Me.m_cmsTestProgressStepCutMenuItem.Name = "m_cmsTestProgressStepCutMenuItem"
        Me.m_cmsTestProgressStepCutMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.m_cmsTestProgressStepCutMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.m_cmsTestProgressStepCutMenuItem.Text = "Cut"
        '
        'm_cmsTestProgressStepCopyMenuItem
        '
        Me.m_cmsTestProgressStepCopyMenuItem.Name = "m_cmsTestProgressStepCopyMenuItem"
        Me.m_cmsTestProgressStepCopyMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.m_cmsTestProgressStepCopyMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.m_cmsTestProgressStepCopyMenuItem.Text = "Copy"
        '
        'm_cmsTestProgressStepPasteMenuItem
        '
        Me.m_cmsTestProgressStepPasteMenuItem.Name = "m_cmsTestProgressStepPasteMenuItem"
        Me.m_cmsTestProgressStepPasteMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.m_cmsTestProgressStepPasteMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.m_cmsTestProgressStepPasteMenuItem.Text = "Paste"
        '
        'bindingSource
        '
        Me.bindingSource.DataSource = GetType(CEL.Data.BusinessObjects.TestProgressTestStepCollection)
        '
        'TestProgressCheckTypeCollectionBindingSource
        '
        Me.TestProgressCheckTypeCollectionBindingSource.DataSource = GetType(CEL.Data.BusinessObjects.TestProgressCheckTypeCollection)
        '
        'TestProgressCheckListItemCollectionByTestStepIDBindingSource
        '
        Me.TestProgressCheckListItemCollectionByTestStepIDBindingSource.DataMember = "TestProgressCheckListItemCollectionByTestStepID"
        Me.TestProgressCheckListItemCollectionByTestStepIDBindingSource.DataSource = Me.bindingSource
        '
        'm_btnMoveLast
        '
        Me.m_btnMoveLast.BackColor = System.Drawing.Color.Transparent
        Me.m_btnMoveLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.m_btnMoveLast.Enabled = False
        Me.m_btnMoveLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.m_btnMoveLast.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_btnMoveLast.Location = New System.Drawing.Point(1405, 382)
        Me.m_btnMoveLast.Name = "m_btnMoveLast"
        Me.m_btnMoveLast.Size = New System.Drawing.Size(75, 65)
        Me.m_btnMoveLast.TabIndex = 120
        Me.ToolTip1.SetToolTip(Me.m_btnMoveLast, "Move Last")
        Me.m_btnMoveLast.UseVisualStyleBackColor = False
        '
        'm_btnMoveFirst
        '
        Me.m_btnMoveFirst.BackColor = System.Drawing.Color.Transparent
        Me.m_btnMoveFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.m_btnMoveFirst.Enabled = False
        Me.m_btnMoveFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.m_btnMoveFirst.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_btnMoveFirst.Location = New System.Drawing.Point(1405, 193)
        Me.m_btnMoveFirst.Name = "m_btnMoveFirst"
        Me.m_btnMoveFirst.Size = New System.Drawing.Size(75, 65)
        Me.m_btnMoveFirst.TabIndex = 119
        Me.ToolTip1.SetToolTip(Me.m_btnMoveFirst, "Move First")
        Me.m_btnMoveFirst.UseVisualStyleBackColor = False
        '
        'm_btnMoveDown
        '
        Me.m_btnMoveDown.BackColor = System.Drawing.Color.Transparent
        Me.m_btnMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.m_btnMoveDown.Enabled = False
        Me.m_btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.m_btnMoveDown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_btnMoveDown.Location = New System.Drawing.Point(1405, 319)
        Me.m_btnMoveDown.Name = "m_btnMoveDown"
        Me.m_btnMoveDown.Size = New System.Drawing.Size(75, 65)
        Me.m_btnMoveDown.TabIndex = 118
        Me.ToolTip1.SetToolTip(Me.m_btnMoveDown, "Move Down")
        Me.m_btnMoveDown.UseVisualStyleBackColor = False
        '
        'm_btnMoveUp
        '
        Me.m_btnMoveUp.BackColor = System.Drawing.Color.Transparent
        Me.m_btnMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.m_btnMoveUp.Enabled = False
        Me.m_btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.m_btnMoveUp.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_btnMoveUp.Location = New System.Drawing.Point(1405, 256)
        Me.m_btnMoveUp.Name = "m_btnMoveUp"
        Me.m_btnMoveUp.Size = New System.Drawing.Size(75, 65)
        Me.m_btnMoveUp.TabIndex = 117
        Me.ToolTip1.SetToolTip(Me.m_btnMoveUp, "Move Up")
        Me.m_btnMoveUp.UseVisualStyleBackColor = False
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
        Me.GroupControl1.Controls.Add(Me.m_btnClose)
        Me.GroupControl1.Controls.Add(Me.m_btnSave)
        Me.GroupControl1.Controls.Add(Me.m_btnChannelSelect)
        Me.GroupControl1.Controls.Add(Me.m_dgvNumericChannels)
        Me.GroupControl1.Controls.Add(Me.m_btnMoveLast)
        Me.GroupControl1.Controls.Add(Me.m_btnMoveFirst)
        Me.GroupControl1.Controls.Add(Me.m_btnMoveDown)
        Me.GroupControl1.Controls.Add(Me.m_btnMoveUp)
        Me.GroupControl1.Controls.Add(Me.m_dgvTestProgressTestStepCheckListItems)
        Me.GroupControl1.Controls.Add(Me.m_dgvTestProgressStep)
        Me.GroupControl1.Controls.Add(Me.lblEngineSelection)
        Me.GroupControl1.Controls.Add(Me.m_cboEngine)
        Me.GroupControl1.Controls.Add(Me.Label1)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1484, 768)
        Me.GroupControl1.TabIndex = 114
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(22, 21)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.PictureEdit1.Size = New System.Drawing.Size(119, 100)
        Me.PictureEdit1.TabIndex = 125
        '
        'm_btnClose
        '
        Me.m_btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.m_btnClose.Location = New System.Drawing.Point(1328, 726)
        Me.m_btnClose.Name = "m_btnClose"
        Me.m_btnClose.Size = New System.Drawing.Size(75, 23)
        Me.m_btnClose.TabIndex = 124
        Me.m_btnClose.Text = "Close"
        '
        'm_btnSave
        '
        Me.m_btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_btnSave.Location = New System.Drawing.Point(1247, 726)
        Me.m_btnSave.Name = "m_btnSave"
        Me.m_btnSave.Size = New System.Drawing.Size(75, 23)
        Me.m_btnSave.TabIndex = 123
        Me.m_btnSave.Text = "Save"
        '
        'm_btnChannelSelect
        '
        Me.m_btnChannelSelect.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.m_btnChannelSelect.BackColor = System.Drawing.Color.Transparent
        Me.m_btnChannelSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.m_btnChannelSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.m_btnChannelSelect.Location = New System.Drawing.Point(644, 618)
        Me.m_btnChannelSelect.Name = "m_btnChannelSelect"
        Me.m_btnChannelSelect.Size = New System.Drawing.Size(75, 65)
        Me.m_btnChannelSelect.TabIndex = 122
        Me.m_btnChannelSelect.UseVisualStyleBackColor = False
        '
        'm_dgvNumericChannels
        '
        Me.m_dgvNumericChannels.AllowUserToAddRows = False
        Me.m_dgvNumericChannels.AllowUserToDeleteRows = False
        Me.m_dgvNumericChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.m_dgvNumericChannels.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ChannelName, Me.Description, Me.Units})
        Me.m_dgvNumericChannels.Location = New System.Drawing.Point(22, 125)
        Me.m_dgvNumericChannels.MultiSelect = False
        Me.m_dgvNumericChannels.Name = "m_dgvNumericChannels"
        Me.m_dgvNumericChannels.ReadOnly = True
        Me.m_dgvNumericChannels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.m_dgvNumericChannels.Size = New System.Drawing.Size(622, 583)
        Me.m_dgvNumericChannels.TabIndex = 121
        '
        'ChannelName
        '
        Me.ChannelName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ChannelName.FillWeight = 45.0!
        Me.ChannelName.HeaderText = "Channel Name"
        Me.ChannelName.Name = "ChannelName"
        Me.ChannelName.ReadOnly = True
        '
        'Description
        '
        Me.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Description.FillWeight = 45.0!
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        '
        'Units
        '
        Me.Units.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Units.FillWeight = 10.0!
        Me.Units.HeaderText = "Units"
        Me.Units.Name = "Units"
        Me.Units.ReadOnly = True
        '
        'm_dgvTestProgressTestStepCheckListItems
        '
        Me.m_dgvTestProgressTestStepCheckListItems.AutoGenerateColumns = False
        Me.m_dgvTestProgressTestStepCheckListItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.m_dgvTestProgressTestStepCheckListItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Parameter, Me.LowLimit, Me.HighLimit, Me.Unit, Me.CheckTypeID})
        Me.m_dgvTestProgressTestStepCheckListItems.DataSource = Me.TestProgressCheckListItemCollectionByTestStepIDBindingSource
        Me.m_dgvTestProgressTestStepCheckListItems.Enabled = False
        Me.m_dgvTestProgressTestStepCheckListItems.Location = New System.Drawing.Point(719, 584)
        Me.m_dgvTestProgressTestStepCheckListItems.Name = "m_dgvTestProgressTestStepCheckListItems"
        Me.m_dgvTestProgressTestStepCheckListItems.Size = New System.Drawing.Size(684, 124)
        Me.m_dgvTestProgressTestStepCheckListItems.TabIndex = 115
        '
        'Parameter
        '
        Me.Parameter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Parameter.DataPropertyName = "ParameterName"
        Me.Parameter.HeaderText = "Parameter"
        Me.Parameter.Name = "Parameter"
        '
        'LowLimit
        '
        Me.LowLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.LowLimit.DataPropertyName = "LowLimit"
        Me.LowLimit.FillWeight = 10.0!
        Me.LowLimit.HeaderText = "Low Limit"
        Me.LowLimit.Name = "LowLimit"
        Me.LowLimit.Width = 76
        '
        'HighLimit
        '
        Me.HighLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.HighLimit.DataPropertyName = "HighLimit"
        Me.HighLimit.FillWeight = 10.0!
        Me.HighLimit.HeaderText = "High Limit"
        Me.HighLimit.Name = "HighLimit"
        Me.HighLimit.Width = 78
        '
        'Unit
        '
        Me.Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Unit.DataPropertyName = "Unit"
        Me.Unit.FillWeight = 5.0!
        Me.Unit.HeaderText = "Unit"
        Me.Unit.Name = "Unit"
        Me.Unit.Width = 51
        '
        'CheckTypeID
        '
        Me.CheckTypeID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.CheckTypeID.DataPropertyName = "CheckTypeID"
        Me.CheckTypeID.DataSource = Me.TestProgressCheckTypeCollectionBindingSource
        Me.CheckTypeID.DisplayMember = "Description"
        Me.CheckTypeID.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.CheckTypeID.FillWeight = 18.0!
        Me.CheckTypeID.HeaderText = "Check Type"
        Me.CheckTypeID.Name = "CheckTypeID"
        Me.CheckTypeID.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CheckTypeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CheckTypeID.ValueMember = "CheckTypeID"
        Me.CheckTypeID.Width = 90
        '
        'm_dgvTestProgressStep
        '
        Me.m_dgvTestProgressStep.AutoGenerateColumns = False
        Me.m_dgvTestProgressStep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.m_dgvTestProgressStep.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7})
        Me.m_dgvTestProgressStep.ContextMenuStrip = Me.m_cmsTestProgressStep
        Me.m_dgvTestProgressStep.DataSource = Me.bindingSource
        Me.m_dgvTestProgressStep.Enabled = False
        Me.m_dgvTestProgressStep.Location = New System.Drawing.Point(719, 125)
        Me.m_dgvTestProgressStep.Name = "m_dgvTestProgressStep"
        Me.m_dgvTestProgressStep.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.m_dgvTestProgressStep.Size = New System.Drawing.Size(684, 437)
        Me.m_dgvTestProgressStep.TabIndex = 116
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "StepName"
        Me.DataGridViewTextBoxColumn4.FillWeight = 13.0!
        Me.DataGridViewTextBoxColumn4.HeaderText = "Step Name"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Title"
        Me.DataGridViewTextBoxColumn6.FillWeight = 72.0!
        Me.DataGridViewTextBoxColumn6.HeaderText = "Title"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "StepTypeID"
        Me.DataGridViewTextBoxColumn7.DataSource = Me.TestProgressStepTypeCollectionBindingSource
        Me.DataGridViewTextBoxColumn7.DisplayMember = "Description"
        Me.DataGridViewTextBoxColumn7.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.DataGridViewTextBoxColumn7.FillWeight = 15.0!
        Me.DataGridViewTextBoxColumn7.HeaderText = "Step Type"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewTextBoxColumn7.ValueMember = "StepTypeID"
        '
        'lblEngineSelection
        '
        Me.lblEngineSelection.AutoSize = True
        Me.lblEngineSelection.BackColor = System.Drawing.Color.Transparent
        Me.lblEngineSelection.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEngineSelection.ForeColor = System.Drawing.Color.White
        Me.lblEngineSelection.Location = New System.Drawing.Point(483, 74)
        Me.lblEngineSelection.Name = "lblEngineSelection"
        Me.lblEngineSelection.Size = New System.Drawing.Size(167, 24)
        Me.lblEngineSelection.TabIndex = 114
        Me.lblEngineSelection.Text = "Engine Selection"
        '
        'm_cboEngine
        '
        Me.m_cboEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.m_cboEngine.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_cboEngine.FormattingEnabled = True
        Me.m_cboEngine.Location = New System.Drawing.Point(662, 78)
        Me.m_cboEngine.Name = "m_cboEngine"
        Me.m_cboEngine.Size = New System.Drawing.Size(227, 22)
        Me.m_cboEngine.TabIndex = 113
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial Black", 26.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(406, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(514, 50)
        Me.Label1.TabIndex = 112
        Me.Label1.Text = "Test Progress Step Editor"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(6, 752)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(215, 13)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "© 2018 CEL Aerospace Test Equipment Ltd"
        '
        'TestProgressStepForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.CancelButton = Me.m_btnClose
        Me.ClientSize = New System.Drawing.Size(1484, 768)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "TestProgressStepForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Progress Step Editor"
        CType(Me.TestProgressStepTypeCollectionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.m_cmsTestProgressStep.ResumeLayout(False)
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TestProgressCheckTypeCollectionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TestProgressCheckListItemCollectionByTestStepIDBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.m_dgvNumericChannels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.m_dgvTestProgressTestStepCheckListItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.m_dgvTestProgressStep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TestProgressCheckListItemCollectionByTestStepIDBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TestProgressCheckTypeCollectionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TestProgressStepTypeCollectionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents m_cmsTestProgressStep As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents m_cmsTestProgressStepCutMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_cmsTestProgressStepCopyMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_cmsTestProgressStepPasteMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents m_btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents m_btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents m_btnChannelSelect As System.Windows.Forms.Button
    Friend WithEvents m_dgvNumericChannels As System.Windows.Forms.DataGridView
    Friend WithEvents ChannelName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Units As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents m_btnMoveLast As System.Windows.Forms.Button
    Friend WithEvents m_btnMoveFirst As System.Windows.Forms.Button
    Friend WithEvents m_btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents m_btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents m_dgvTestProgressTestStepCheckListItems As System.Windows.Forms.DataGridView
    Friend WithEvents Parameter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LowLimit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HighLimit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CheckTypeID As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents m_dgvTestProgressStep As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents lblEngineSelection As System.Windows.Forms.Label
    Friend WithEvents m_cboEngine As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
End Class

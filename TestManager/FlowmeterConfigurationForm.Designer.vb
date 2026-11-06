<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FlowmeterConfigurationForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FlowmeterConfigurationForm))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.m_FlowmeterMasterCollection = New CEL.Data.BusinessObjects.FlowmeterMasterCollection()
        Me.m_FlowmeterMapCollection = New CEL.Data.BusinessObjects.FlowmeterMapCollection()
        Me.m_FlowMeterOutputUnitCollection = New CEL.Data.BusinessObjects.FlowMeterOutputUnitCollection()
        Me.m_FlowmeterCollection = New CEL.Data.BusinessObjects.FlowmeterCollection()
        Me.m_FlowMeterInputUnitCollection = New CEL.Data.BusinessObjects.FlowMeterInputUnitCollection()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.dgvMapping = New System.Windows.Forms.DataGridView()
        Me.IdDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FlowmeterIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cboFlometerMappingName = New System.Windows.Forms.ComboBox()
        Me.cboMappedToFlowmeter = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.navBoolean = New DevExpress.XtraEditors.DataNavigator()
        Me.dgvFlowmeter = New System.Windows.Forms.DataGridView()
        Me.ProjectIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EngineModelDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FlowmeterMapIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.RawChannelDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CalChannelDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotChannelDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomOutputUnitDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.cboMappingName = New System.Windows.Forms.ComboBox()
        Me.cboOutputUnit = New System.Windows.Forms.ComboBox()
        Me.txtCalibratedChannelName = New System.Windows.Forms.TextBox()
        Me.txtTotalChannelName = New System.Windows.Forms.TextBox()
        Me.txtRawChannelName = New System.Windows.Forms.TextBox()
        Me.cboCollectionName = New System.Windows.Forms.ComboBox()
        Me.lblBooleanFalseText = New System.Windows.Forms.Label()
        Me.lblBooleanTooltip = New System.Windows.Forms.Label()
        Me.lblBooleanDescription = New System.Windows.Forms.Label()
        Me.lblBooleanCollectionName = New System.Windows.Forms.Label()
        Me.lblBooleanChannelName = New System.Windows.Forms.Label()
        Me.lblBooleanChannelSource = New System.Windows.Forms.Label()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.dgvFlowmeterDetail = New System.Windows.Forms.DataGridView()
        Me.FlowmeterIDDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataPointDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InputValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutputValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvFlowmeterMaster = New System.Windows.Forms.DataGridView()
        Me.IdDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableInputUnitDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TableOutputUnitDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.SerialNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CalibrationDueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SgDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.dgvMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.dgvFlowmeter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.dgvFlowmeterDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvFlowmeterMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'm_FlowmeterMasterCollection
        '
        Me.m_FlowmeterMasterCollection.AllowDelete = True
        Me.m_FlowmeterMasterCollection.AllowEdit = True
        Me.m_FlowmeterMasterCollection.AllowNew = True
        Me.m_FlowmeterMasterCollection.EnableHierarchicalBinding = True
        Me.m_FlowmeterMasterCollection.Filter = Nothing
        '
        'm_FlowmeterMapCollection
        '
        Me.m_FlowmeterMapCollection.AllowDelete = True
        Me.m_FlowmeterMapCollection.AllowEdit = True
        Me.m_FlowmeterMapCollection.AllowNew = True
        Me.m_FlowmeterMapCollection.EnableHierarchicalBinding = True
        Me.m_FlowmeterMapCollection.Filter = Nothing
        '
        'm_FlowMeterOutputUnitCollection
        '
        Me.m_FlowMeterOutputUnitCollection.AllowDelete = True
        Me.m_FlowMeterOutputUnitCollection.AllowEdit = True
        Me.m_FlowMeterOutputUnitCollection.AllowNew = True
        Me.m_FlowMeterOutputUnitCollection.EnableHierarchicalBinding = True
        Me.m_FlowMeterOutputUnitCollection.Filter = Nothing
        '
        'm_FlowmeterCollection
        '
        Me.m_FlowmeterCollection.AllowDelete = False
        Me.m_FlowmeterCollection.AllowEdit = False
        Me.m_FlowmeterCollection.AllowNew = False
        Me.m_FlowmeterCollection.EnableHierarchicalBinding = True
        Me.m_FlowmeterCollection.Filter = Nothing
        '
        'm_FlowMeterInputUnitCollection
        '
        Me.m_FlowMeterInputUnitCollection.AllowDelete = True
        Me.m_FlowMeterInputUnitCollection.AllowEdit = True
        Me.m_FlowMeterInputUnitCollection.AllowNew = True
        Me.m_FlowMeterInputUnitCollection.EnableHierarchicalBinding = True
        Me.m_FlowMeterInputUnitCollection.Filter = Nothing
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(884, 799)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 102
        Me.btnClose.Text = "Close"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(803, 799)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 101
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(722, 799)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 100
        Me.btnSave.Text = "Save"
        '
        'GroupControl2
        '
        Me.GroupControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupControl2.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.GroupControl2.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl2.Appearance.Options.UseBackColor = True
        Me.GroupControl2.Appearance.Options.UseBorderColor = True
        Me.GroupControl2.Controls.Add(Me.PictureEdit1)
        Me.GroupControl2.Controls.Add(Me.Label1)
        Me.GroupControl2.Controls.Add(Me.XtraTabControl1)
        Me.GroupControl2.Controls.Add(Me.Label2)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(969, 829)
        Me.GroupControl2.TabIndex = 113
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(11, 16)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.PictureEdit1.Size = New System.Drawing.Size(119, 100)
        Me.PictureEdit1.TabIndex = 114
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial Black", 26.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(258, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(495, 50)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "Flowmeter Configuration"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XtraTabControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.XtraTabControl1.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.XtraTabControl1.Appearance.Options.UseBackColor = True
        Me.XtraTabControl1.Appearance.Options.UseBorderColor = True
        Me.XtraTabControl1.Location = New System.Drawing.Point(11, 122)
        Me.XtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.XtraTabControl1.LookAndFeel.UseWindowsXPTheme = True
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(946, 664)
        Me.XtraTabControl1.TabIndex = 112
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Appearance.PageClient.BackColor = System.Drawing.Color.Black
        Me.XtraTabPage1.Appearance.PageClient.Options.UseBackColor = True
        Me.XtraTabPage1.Controls.Add(Me.dgvMapping)
        Me.XtraTabPage1.Controls.Add(Me.GroupControl1)
        Me.XtraTabPage1.Controls.Add(Me.navBoolean)
        Me.XtraTabPage1.Controls.Add(Me.dgvFlowmeter)
        Me.XtraTabPage1.Controls.Add(Me.GroupControl3)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(938, 636)
        Me.XtraTabPage1.Text = "Engine Configuration"
        '
        'dgvMapping
        '
        Me.dgvMapping.AllowUserToAddRows = False
        Me.dgvMapping.AllowUserToDeleteRows = False
        Me.dgvMapping.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMapping.AutoGenerateColumns = False
        Me.dgvMapping.BackgroundColor = System.Drawing.Color.Black
        Me.dgvMapping.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMapping.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdDataGridViewTextBoxColumn, Me.DescriptionDataGridViewTextBoxColumn, Me.FlowmeterIDDataGridViewTextBoxColumn})
        Me.dgvMapping.DataSource = Me.m_FlowmeterMapCollection
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvMapping.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvMapping.Location = New System.Drawing.Point(186, 328)
        Me.dgvMapping.Name = "dgvMapping"
        Me.dgvMapping.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvMapping.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMapping.Size = New System.Drawing.Size(566, 183)
        Me.dgvMapping.TabIndex = 83
        '
        'IdDataGridViewTextBoxColumn
        '
        Me.IdDataGridViewTextBoxColumn.DataPropertyName = "Id"
        Me.IdDataGridViewTextBoxColumn.HeaderText = "Id"
        Me.IdDataGridViewTextBoxColumn.Name = "IdDataGridViewTextBoxColumn"
        '
        'DescriptionDataGridViewTextBoxColumn
        '
        Me.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        Me.DescriptionDataGridViewTextBoxColumn.Width = 200
        '
        'FlowmeterIDDataGridViewTextBoxColumn
        '
        Me.FlowmeterIDDataGridViewTextBoxColumn.DataPropertyName = "FlowmeterID"
        Me.FlowmeterIDDataGridViewTextBoxColumn.DataSource = Me.m_FlowmeterMasterCollection
        Me.FlowmeterIDDataGridViewTextBoxColumn.DisplayMember = "SerialNumber"
        Me.FlowmeterIDDataGridViewTextBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FlowmeterIDDataGridViewTextBoxColumn.HeaderText = "FlowmeterID"
        Me.FlowmeterIDDataGridViewTextBoxColumn.Name = "FlowmeterIDDataGridViewTextBoxColumn"
        Me.FlowmeterIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FlowmeterIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.FlowmeterIDDataGridViewTextBoxColumn.ValueMember = "Id"
        Me.FlowmeterIDDataGridViewTextBoxColumn.Width = 200
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GroupControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupControl1.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.GroupControl1.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl1.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.Appearance.ForeColor = System.Drawing.Color.White
        Me.GroupControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl1.Appearance.Options.UseBackColor = True
        Me.GroupControl1.Appearance.Options.UseBorderColor = True
        Me.GroupControl1.Appearance.Options.UseFont = True
        Me.GroupControl1.Appearance.Options.UseForeColor = True
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Silver
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseForeColor = True
        Me.GroupControl1.Controls.Add(Me.cboFlometerMappingName)
        Me.GroupControl1.Controls.Add(Me.cboMappedToFlowmeter)
        Me.GroupControl1.Controls.Add(Me.Label5)
        Me.GroupControl1.Controls.Add(Me.Label6)
        Me.GroupControl1.Location = New System.Drawing.Point(6, 517)
        Me.GroupControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(929, 85)
        Me.GroupControl1.TabIndex = 82
        Me.GroupControl1.Text = "Engine Mapping to Flowmeter"
        '
        'cboFlometerMappingName
        '
        Me.cboFlometerMappingName.BackColor = System.Drawing.Color.LightGray
        Me.cboFlometerMappingName.DataSource = Me.m_FlowmeterMapCollection
        Me.cboFlometerMappingName.DisplayMember = "Description"
        Me.cboFlometerMappingName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFlometerMappingName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFlometerMappingName.FormattingEnabled = True
        Me.cboFlometerMappingName.Location = New System.Drawing.Point(12, 46)
        Me.cboFlometerMappingName.Name = "cboFlometerMappingName"
        Me.cboFlometerMappingName.Size = New System.Drawing.Size(445, 24)
        Me.cboFlometerMappingName.TabIndex = 74
        Me.cboFlometerMappingName.ValueMember = "Id"
        '
        'cboMappedToFlowmeter
        '
        Me.cboMappedToFlowmeter.BackColor = System.Drawing.Color.LightGray
        Me.cboMappedToFlowmeter.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.m_FlowmeterMapCollection, "FlowmeterID", True))
        Me.cboMappedToFlowmeter.DataSource = Me.m_FlowmeterMasterCollection
        Me.cboMappedToFlowmeter.DisplayMember = "SerialNumber"
        Me.cboMappedToFlowmeter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMappedToFlowmeter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMappedToFlowmeter.FormattingEnabled = True
        Me.cboMappedToFlowmeter.Location = New System.Drawing.Point(471, 46)
        Me.cboMappedToFlowmeter.Name = "cboMappedToFlowmeter"
        Me.cboMappedToFlowmeter.Size = New System.Drawing.Size(445, 24)
        Me.cboMappedToFlowmeter.TabIndex = 62
        Me.cboMappedToFlowmeter.ValueMember = "Id"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(468, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 16)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Mapped to Flowmeter"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(158, 16)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "Flowmter Mapping Name"
        '
        'navBoolean
        '
        Me.navBoolean.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.navBoolean.Location = New System.Drawing.Point(3, 608)
        Me.navBoolean.Name = "navBoolean"
        Me.navBoolean.Size = New System.Drawing.Size(373, 24)
        Me.navBoolean.TabIndex = 79
        Me.navBoolean.Text = "DataNavigator1"
        '
        'dgvFlowmeter
        '
        Me.dgvFlowmeter.AllowUserToAddRows = False
        Me.dgvFlowmeter.AllowUserToDeleteRows = False
        Me.dgvFlowmeter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFlowmeter.AutoGenerateColumns = False
        Me.dgvFlowmeter.BackgroundColor = System.Drawing.Color.Black
        Me.dgvFlowmeter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFlowmeter.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ProjectIDDataGridViewTextBoxColumn, Me.EngineModelDataGridViewTextBoxColumn, Me.FlowmeterMapIDDataGridViewTextBoxColumn, Me.RawChannelDataGridViewTextBoxColumn, Me.CalChannelDataGridViewTextBoxColumn, Me.TotChannelDataGridViewTextBoxColumn, Me.CustomOutputUnitDataGridViewTextBoxColumn})
        Me.dgvFlowmeter.DataSource = Me.m_FlowmeterCollection
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvFlowmeter.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvFlowmeter.Location = New System.Drawing.Point(3, 3)
        Me.dgvFlowmeter.Name = "dgvFlowmeter"
        Me.dgvFlowmeter.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvFlowmeter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFlowmeter.Size = New System.Drawing.Size(932, 141)
        Me.dgvFlowmeter.TabIndex = 80
        '
        'ProjectIDDataGridViewTextBoxColumn
        '
        Me.ProjectIDDataGridViewTextBoxColumn.DataPropertyName = "ProjectID"
        Me.ProjectIDDataGridViewTextBoxColumn.HeaderText = "ProjectID"
        Me.ProjectIDDataGridViewTextBoxColumn.Name = "ProjectIDDataGridViewTextBoxColumn"
        Me.ProjectIDDataGridViewTextBoxColumn.Width = 75
        '
        'EngineModelDataGridViewTextBoxColumn
        '
        Me.EngineModelDataGridViewTextBoxColumn.DataPropertyName = "EngineModel"
        Me.EngineModelDataGridViewTextBoxColumn.HeaderText = "EngineModel"
        Me.EngineModelDataGridViewTextBoxColumn.Name = "EngineModelDataGridViewTextBoxColumn"
        Me.EngineModelDataGridViewTextBoxColumn.Width = 150
        '
        'FlowmeterMapIDDataGridViewTextBoxColumn
        '
        Me.FlowmeterMapIDDataGridViewTextBoxColumn.DataPropertyName = "FlowmeterMapID"
        Me.FlowmeterMapIDDataGridViewTextBoxColumn.DataSource = Me.m_FlowmeterMapCollection
        Me.FlowmeterMapIDDataGridViewTextBoxColumn.DisplayMember = "Description"
        Me.FlowmeterMapIDDataGridViewTextBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FlowmeterMapIDDataGridViewTextBoxColumn.HeaderText = "FlowmeterMapID"
        Me.FlowmeterMapIDDataGridViewTextBoxColumn.Name = "FlowmeterMapIDDataGridViewTextBoxColumn"
        Me.FlowmeterMapIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FlowmeterMapIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.FlowmeterMapIDDataGridViewTextBoxColumn.ValueMember = "Id"
        Me.FlowmeterMapIDDataGridViewTextBoxColumn.Width = 150
        '
        'RawChannelDataGridViewTextBoxColumn
        '
        Me.RawChannelDataGridViewTextBoxColumn.DataPropertyName = "RawChannel"
        Me.RawChannelDataGridViewTextBoxColumn.HeaderText = "RawChannel"
        Me.RawChannelDataGridViewTextBoxColumn.Name = "RawChannelDataGridViewTextBoxColumn"
        Me.RawChannelDataGridViewTextBoxColumn.Width = 125
        '
        'CalChannelDataGridViewTextBoxColumn
        '
        Me.CalChannelDataGridViewTextBoxColumn.DataPropertyName = "CalChannel"
        Me.CalChannelDataGridViewTextBoxColumn.HeaderText = "CalChannel"
        Me.CalChannelDataGridViewTextBoxColumn.Name = "CalChannelDataGridViewTextBoxColumn"
        Me.CalChannelDataGridViewTextBoxColumn.Width = 125
        '
        'TotChannelDataGridViewTextBoxColumn
        '
        Me.TotChannelDataGridViewTextBoxColumn.DataPropertyName = "TotChannel"
        Me.TotChannelDataGridViewTextBoxColumn.HeaderText = "TotChannel"
        Me.TotChannelDataGridViewTextBoxColumn.Name = "TotChannelDataGridViewTextBoxColumn"
        Me.TotChannelDataGridViewTextBoxColumn.Width = 125
        '
        'CustomOutputUnitDataGridViewTextBoxColumn
        '
        Me.CustomOutputUnitDataGridViewTextBoxColumn.DataPropertyName = "CustomOutputUnit"
        Me.CustomOutputUnitDataGridViewTextBoxColumn.DataSource = Me.m_FlowMeterOutputUnitCollection
        Me.CustomOutputUnitDataGridViewTextBoxColumn.DisplayMember = "Description"
        Me.CustomOutputUnitDataGridViewTextBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CustomOutputUnitDataGridViewTextBoxColumn.HeaderText = "CustomOutputUnit"
        Me.CustomOutputUnitDataGridViewTextBoxColumn.Name = "CustomOutputUnitDataGridViewTextBoxColumn"
        Me.CustomOutputUnitDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CustomOutputUnitDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CustomOutputUnitDataGridViewTextBoxColumn.ValueMember = "Id"
        Me.CustomOutputUnitDataGridViewTextBoxColumn.Width = 150
        '
        'GroupControl3
        '
        Me.GroupControl3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GroupControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupControl3.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.GroupControl3.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl3.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl3.Appearance.ForeColor = System.Drawing.Color.White
        Me.GroupControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl3.Appearance.Options.UseBackColor = True
        Me.GroupControl3.Appearance.Options.UseBorderColor = True
        Me.GroupControl3.Appearance.Options.UseFont = True
        Me.GroupControl3.Appearance.Options.UseForeColor = True
        Me.GroupControl3.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl3.AppearanceCaption.ForeColor = System.Drawing.Color.Silver
        Me.GroupControl3.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3.AppearanceCaption.Options.UseForeColor = True
        Me.GroupControl3.Controls.Add(Me.cboMappingName)
        Me.GroupControl3.Controls.Add(Me.cboOutputUnit)
        Me.GroupControl3.Controls.Add(Me.txtCalibratedChannelName)
        Me.GroupControl3.Controls.Add(Me.txtTotalChannelName)
        Me.GroupControl3.Controls.Add(Me.txtRawChannelName)
        Me.GroupControl3.Controls.Add(Me.cboCollectionName)
        Me.GroupControl3.Controls.Add(Me.lblBooleanFalseText)
        Me.GroupControl3.Controls.Add(Me.lblBooleanTooltip)
        Me.GroupControl3.Controls.Add(Me.lblBooleanDescription)
        Me.GroupControl3.Controls.Add(Me.lblBooleanCollectionName)
        Me.GroupControl3.Controls.Add(Me.lblBooleanChannelName)
        Me.GroupControl3.Controls.Add(Me.lblBooleanChannelSource)
        Me.GroupControl3.Location = New System.Drawing.Point(6, 150)
        Me.GroupControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(929, 172)
        Me.GroupControl3.TabIndex = 81
        Me.GroupControl3.Text = "Engine Configuration"
        '
        'cboMappingName
        '
        Me.cboMappingName.BackColor = System.Drawing.Color.LightGray
        Me.cboMappingName.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.m_FlowmeterCollection, "FlowmeterMapID", True))
        Me.cboMappingName.DataSource = Me.m_FlowmeterMapCollection
        Me.cboMappingName.DisplayMember = "Description"
        Me.cboMappingName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMappingName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMappingName.FormattingEnabled = True
        Me.cboMappingName.Location = New System.Drawing.Point(12, 46)
        Me.cboMappingName.Name = "cboMappingName"
        Me.cboMappingName.Size = New System.Drawing.Size(445, 24)
        Me.cboMappingName.TabIndex = 74
        Me.cboMappingName.ValueMember = "Id"
        '
        'cboOutputUnit
        '
        Me.cboOutputUnit.BackColor = System.Drawing.Color.LightGray
        Me.cboOutputUnit.DataSource = Me.m_FlowMeterOutputUnitCollection
        Me.cboOutputUnit.DisplayMember = "Description"
        Me.cboOutputUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOutputUnit.Enabled = False
        Me.cboOutputUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOutputUnit.FormattingEnabled = True
        Me.cboOutputUnit.Location = New System.Drawing.Point(471, 133)
        Me.cboOutputUnit.Name = "cboOutputUnit"
        Me.cboOutputUnit.Size = New System.Drawing.Size(445, 24)
        Me.cboOutputUnit.TabIndex = 73
        Me.cboOutputUnit.ValueMember = "Id"
        '
        'txtCalibratedChannelName
        '
        Me.txtCalibratedChannelName.BackColor = System.Drawing.Color.LightGray
        Me.txtCalibratedChannelName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.m_FlowmeterCollection, "CalChannel", True))
        Me.txtCalibratedChannelName.Enabled = False
        Me.txtCalibratedChannelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCalibratedChannelName.Location = New System.Drawing.Point(12, 135)
        Me.txtCalibratedChannelName.Name = "txtCalibratedChannelName"
        Me.txtCalibratedChannelName.Size = New System.Drawing.Size(445, 22)
        Me.txtCalibratedChannelName.TabIndex = 72
        '
        'txtTotalChannelName
        '
        Me.txtTotalChannelName.BackColor = System.Drawing.Color.LightGray
        Me.txtTotalChannelName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.m_FlowmeterCollection, "TotChannel", True))
        Me.txtTotalChannelName.Enabled = False
        Me.txtTotalChannelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalChannelName.Location = New System.Drawing.Point(471, 91)
        Me.txtTotalChannelName.Name = "txtTotalChannelName"
        Me.txtTotalChannelName.Size = New System.Drawing.Size(445, 22)
        Me.txtTotalChannelName.TabIndex = 66
        '
        'txtRawChannelName
        '
        Me.txtRawChannelName.BackColor = System.Drawing.Color.LightGray
        Me.txtRawChannelName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.m_FlowmeterCollection, "RawChannel", True))
        Me.txtRawChannelName.Enabled = False
        Me.txtRawChannelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRawChannelName.Location = New System.Drawing.Point(12, 91)
        Me.txtRawChannelName.Name = "txtRawChannelName"
        Me.txtRawChannelName.Size = New System.Drawing.Size(445, 22)
        Me.txtRawChannelName.TabIndex = 64
        '
        'cboCollectionName
        '
        Me.cboCollectionName.BackColor = System.Drawing.Color.LightGray
        Me.cboCollectionName.DisplayMember = "CollectionName"
        Me.cboCollectionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCollectionName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCollectionName.FormattingEnabled = True
        Me.cboCollectionName.Location = New System.Drawing.Point(471, 46)
        Me.cboCollectionName.Name = "cboCollectionName"
        Me.cboCollectionName.Size = New System.Drawing.Size(445, 24)
        Me.cboCollectionName.TabIndex = 62
        Me.cboCollectionName.ValueMember = "Id"
        Me.cboCollectionName.Visible = False
        '
        'lblBooleanFalseText
        '
        Me.lblBooleanFalseText.AutoSize = True
        Me.lblBooleanFalseText.BackColor = System.Drawing.Color.Transparent
        Me.lblBooleanFalseText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanFalseText.Location = New System.Drawing.Point(468, 116)
        Me.lblBooleanFalseText.Name = "lblBooleanFalseText"
        Me.lblBooleanFalseText.Size = New System.Drawing.Size(72, 16)
        Me.lblBooleanFalseText.TabIndex = 68
        Me.lblBooleanFalseText.Text = "Output Unit"
        '
        'lblBooleanTooltip
        '
        Me.lblBooleanTooltip.AutoSize = True
        Me.lblBooleanTooltip.BackColor = System.Drawing.Color.Transparent
        Me.lblBooleanTooltip.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanTooltip.Location = New System.Drawing.Point(468, 72)
        Me.lblBooleanTooltip.Name = "lblBooleanTooltip"
        Me.lblBooleanTooltip.Size = New System.Drawing.Size(131, 16)
        Me.lblBooleanTooltip.TabIndex = 67
        Me.lblBooleanTooltip.Text = "Total Channel Name"
        '
        'lblBooleanDescription
        '
        Me.lblBooleanDescription.AutoSize = True
        Me.lblBooleanDescription.BackColor = System.Drawing.Color.Transparent
        Me.lblBooleanDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanDescription.Location = New System.Drawing.Point(9, 72)
        Me.lblBooleanDescription.Name = "lblBooleanDescription"
        Me.lblBooleanDescription.Size = New System.Drawing.Size(127, 16)
        Me.lblBooleanDescription.TabIndex = 65
        Me.lblBooleanDescription.Text = "Raw Channel Name"
        '
        'lblBooleanCollectionName
        '
        Me.lblBooleanCollectionName.AutoSize = True
        Me.lblBooleanCollectionName.BackColor = System.Drawing.Color.Transparent
        Me.lblBooleanCollectionName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanCollectionName.Location = New System.Drawing.Point(468, 26)
        Me.lblBooleanCollectionName.Name = "lblBooleanCollectionName"
        Me.lblBooleanCollectionName.Size = New System.Drawing.Size(91, 16)
        Me.lblBooleanCollectionName.TabIndex = 63
        Me.lblBooleanCollectionName.Text = "Engine Model"
        Me.lblBooleanCollectionName.Visible = False
        '
        'lblBooleanChannelName
        '
        Me.lblBooleanChannelName.AutoSize = True
        Me.lblBooleanChannelName.BackColor = System.Drawing.Color.Transparent
        Me.lblBooleanChannelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanChannelName.Location = New System.Drawing.Point(9, 26)
        Me.lblBooleanChannelName.Name = "lblBooleanChannelName"
        Me.lblBooleanChannelName.Size = New System.Drawing.Size(101, 16)
        Me.lblBooleanChannelName.TabIndex = 61
        Me.lblBooleanChannelName.Text = "Mapping Name"
        '
        'lblBooleanChannelSource
        '
        Me.lblBooleanChannelSource.AutoSize = True
        Me.lblBooleanChannelSource.BackColor = System.Drawing.Color.Transparent
        Me.lblBooleanChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanChannelSource.Location = New System.Drawing.Point(9, 116)
        Me.lblBooleanChannelSource.Name = "lblBooleanChannelSource"
        Me.lblBooleanChannelSource.Size = New System.Drawing.Size(162, 16)
        Me.lblBooleanChannelSource.TabIndex = 70
        Me.lblBooleanChannelSource.Text = "Calibrated Channel Name"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Appearance.PageClient.BackColor = System.Drawing.Color.Black
        Me.XtraTabPage2.Appearance.PageClient.Options.UseBackColor = True
        Me.XtraTabPage2.Controls.Add(Me.dgvFlowmeterDetail)
        Me.XtraTabPage2.Controls.Add(Me.dgvFlowmeterMaster)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(938, 636)
        Me.XtraTabPage2.Text = "Flowmeter Defintitions"
        '
        'dgvFlowmeterDetail
        '
        Me.dgvFlowmeterDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFlowmeterDetail.AutoGenerateColumns = False
        Me.dgvFlowmeterDetail.BackgroundColor = System.Drawing.Color.Black
        Me.dgvFlowmeterDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFlowmeterDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FlowmeterIDDataGridViewTextBoxColumn1, Me.DataPointDataGridViewTextBoxColumn, Me.InputValueDataGridViewTextBoxColumn, Me.OutputValueDataGridViewTextBoxColumn})
        Me.dgvFlowmeterDetail.DataBindings.Add(New System.Windows.Forms.Binding("Tag", Me.m_FlowmeterMasterCollection, "FlowmeterDetailCollectionByFlowmeterID.FlowmeterID", True))
        Me.dgvFlowmeterDetail.DataMember = "FlowmeterDetailCollectionByFlowmeterID"
        Me.dgvFlowmeterDetail.DataSource = Me.m_FlowmeterMasterCollection
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvFlowmeterDetail.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvFlowmeterDetail.Location = New System.Drawing.Point(226, 138)
        Me.dgvFlowmeterDetail.Name = "dgvFlowmeterDetail"
        Me.dgvFlowmeterDetail.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvFlowmeterDetail.Size = New System.Drawing.Size(445, 494)
        Me.dgvFlowmeterDetail.TabIndex = 85
        '
        'FlowmeterIDDataGridViewTextBoxColumn1
        '
        Me.FlowmeterIDDataGridViewTextBoxColumn1.DataPropertyName = "FlowmeterID"
        Me.FlowmeterIDDataGridViewTextBoxColumn1.HeaderText = "FlowmeterID"
        Me.FlowmeterIDDataGridViewTextBoxColumn1.Name = "FlowmeterIDDataGridViewTextBoxColumn1"
        '
        'DataPointDataGridViewTextBoxColumn
        '
        Me.DataPointDataGridViewTextBoxColumn.DataPropertyName = "DataPoint"
        Me.DataPointDataGridViewTextBoxColumn.HeaderText = "DataPoint"
        Me.DataPointDataGridViewTextBoxColumn.Name = "DataPointDataGridViewTextBoxColumn"
        '
        'InputValueDataGridViewTextBoxColumn
        '
        Me.InputValueDataGridViewTextBoxColumn.DataPropertyName = "InputValue"
        Me.InputValueDataGridViewTextBoxColumn.HeaderText = "InputValue"
        Me.InputValueDataGridViewTextBoxColumn.Name = "InputValueDataGridViewTextBoxColumn"
        '
        'OutputValueDataGridViewTextBoxColumn
        '
        Me.OutputValueDataGridViewTextBoxColumn.DataPropertyName = "OutputValue"
        Me.OutputValueDataGridViewTextBoxColumn.HeaderText = "OutputValue"
        Me.OutputValueDataGridViewTextBoxColumn.Name = "OutputValueDataGridViewTextBoxColumn"
        '
        'dgvFlowmeterMaster
        '
        Me.dgvFlowmeterMaster.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFlowmeterMaster.AutoGenerateColumns = False
        Me.dgvFlowmeterMaster.BackgroundColor = System.Drawing.Color.Black
        Me.dgvFlowmeterMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFlowmeterMaster.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdDataGridViewTextBoxColumn1, Me.TableInputUnitDataGridViewTextBoxColumn, Me.TableOutputUnitDataGridViewTextBoxColumn, Me.SerialNumberDataGridViewTextBoxColumn, Me.CalibrationDueDataGridViewTextBoxColumn, Me.SgDataGridViewTextBoxColumn, Me.NoteDataGridViewTextBoxColumn})
        Me.dgvFlowmeterMaster.DataSource = Me.m_FlowmeterMasterCollection
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvFlowmeterMaster.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgvFlowmeterMaster.Location = New System.Drawing.Point(0, 3)
        Me.dgvFlowmeterMaster.Name = "dgvFlowmeterMaster"
        Me.dgvFlowmeterMaster.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvFlowmeterMaster.Size = New System.Drawing.Size(932, 129)
        Me.dgvFlowmeterMaster.TabIndex = 83
        '
        'IdDataGridViewTextBoxColumn1
        '
        Me.IdDataGridViewTextBoxColumn1.DataPropertyName = "Id"
        Me.IdDataGridViewTextBoxColumn1.HeaderText = "Id"
        Me.IdDataGridViewTextBoxColumn1.Name = "IdDataGridViewTextBoxColumn1"
        Me.IdDataGridViewTextBoxColumn1.Width = 75
        '
        'TableInputUnitDataGridViewTextBoxColumn
        '
        Me.TableInputUnitDataGridViewTextBoxColumn.DataPropertyName = "TableInputUnit"
        Me.TableInputUnitDataGridViewTextBoxColumn.DataSource = Me.m_FlowMeterInputUnitCollection
        Me.TableInputUnitDataGridViewTextBoxColumn.DisplayMember = "Description"
        Me.TableInputUnitDataGridViewTextBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TableInputUnitDataGridViewTextBoxColumn.HeaderText = "TableInputUnit"
        Me.TableInputUnitDataGridViewTextBoxColumn.Name = "TableInputUnitDataGridViewTextBoxColumn"
        Me.TableInputUnitDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TableInputUnitDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.TableInputUnitDataGridViewTextBoxColumn.ValueMember = "Id"
        '
        'TableOutputUnitDataGridViewTextBoxColumn
        '
        Me.TableOutputUnitDataGridViewTextBoxColumn.DataPropertyName = "TableOutputUnit"
        Me.TableOutputUnitDataGridViewTextBoxColumn.DataSource = Me.m_FlowMeterOutputUnitCollection
        Me.TableOutputUnitDataGridViewTextBoxColumn.DisplayMember = "Description"
        Me.TableOutputUnitDataGridViewTextBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TableOutputUnitDataGridViewTextBoxColumn.HeaderText = "TableOutputUnit"
        Me.TableOutputUnitDataGridViewTextBoxColumn.Name = "TableOutputUnitDataGridViewTextBoxColumn"
        Me.TableOutputUnitDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TableOutputUnitDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.TableOutputUnitDataGridViewTextBoxColumn.ValueMember = "Id"
        '
        'SerialNumberDataGridViewTextBoxColumn
        '
        Me.SerialNumberDataGridViewTextBoxColumn.DataPropertyName = "SerialNumber"
        Me.SerialNumberDataGridViewTextBoxColumn.HeaderText = "SerialNumber"
        Me.SerialNumberDataGridViewTextBoxColumn.Name = "SerialNumberDataGridViewTextBoxColumn"
        Me.SerialNumberDataGridViewTextBoxColumn.Width = 150
        '
        'CalibrationDueDataGridViewTextBoxColumn
        '
        Me.CalibrationDueDataGridViewTextBoxColumn.DataPropertyName = "CalibrationDue"
        Me.CalibrationDueDataGridViewTextBoxColumn.HeaderText = "CalibrationDue"
        Me.CalibrationDueDataGridViewTextBoxColumn.Name = "CalibrationDueDataGridViewTextBoxColumn"
        '
        'SgDataGridViewTextBoxColumn
        '
        Me.SgDataGridViewTextBoxColumn.DataPropertyName = "Sg"
        Me.SgDataGridViewTextBoxColumn.HeaderText = "Sg"
        Me.SgDataGridViewTextBoxColumn.Name = "SgDataGridViewTextBoxColumn"
        '
        'NoteDataGridViewTextBoxColumn
        '
        Me.NoteDataGridViewTextBoxColumn.DataPropertyName = "Note"
        Me.NoteDataGridViewTextBoxColumn.HeaderText = "Note"
        Me.NoteDataGridViewTextBoxColumn.Name = "NoteDataGridViewTextBoxColumn"
        Me.NoteDataGridViewTextBoxColumn.Width = 175
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(4, 811)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(215, 13)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "© 2018 CEL Aerospace Test Equipment Ltd"
        '
        'FlowmeterConfigurationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(969, 829)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupControl2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FlowmeterConfigurationForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Flowmeter Configuration"
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.dgvMapping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.dgvFlowmeter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.dgvFlowmeterDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvFlowmeterMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents m_FlowmeterMasterCollection As CEL.Data.BusinessObjects.FlowmeterMasterCollection
    Friend WithEvents m_FlowmeterMapCollection As CEL.Data.BusinessObjects.FlowmeterMapCollection
    Friend WithEvents m_FlowmeterCollection As CEL.Data.BusinessObjects.FlowmeterCollection
    Friend WithEvents m_FlowMeterOutputUnitCollection As CEL.Data.BusinessObjects.FlowMeterOutputUnitCollection
    Friend WithEvents m_FlowMeterInputUnitCollection As CEL.Data.BusinessObjects.FlowMeterInputUnitCollection
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgvMapping As System.Windows.Forms.DataGridView
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cboFlometerMappingName As System.Windows.Forms.ComboBox
    Friend WithEvents cboMappedToFlowmeter As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents navBoolean As DevExpress.XtraEditors.DataNavigator
    Friend WithEvents dgvFlowmeter As System.Windows.Forms.DataGridView
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents cboMappingName As System.Windows.Forms.ComboBox
    Friend WithEvents cboOutputUnit As System.Windows.Forms.ComboBox
    Friend WithEvents txtCalibratedChannelName As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalChannelName As System.Windows.Forms.TextBox
    Friend WithEvents txtRawChannelName As System.Windows.Forms.TextBox
    Friend WithEvents cboCollectionName As System.Windows.Forms.ComboBox
    Friend WithEvents lblBooleanFalseText As System.Windows.Forms.Label
    Friend WithEvents lblBooleanTooltip As System.Windows.Forms.Label
    Friend WithEvents lblBooleanDescription As System.Windows.Forms.Label
    Friend WithEvents lblBooleanCollectionName As System.Windows.Forms.Label
    Friend WithEvents lblBooleanChannelName As System.Windows.Forms.Label
    Friend WithEvents lblBooleanChannelSource As System.Windows.Forms.Label
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents dgvFlowmeterDetail As System.Windows.Forms.DataGridView
    Friend WithEvents FlowmeterIDDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataPointDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InputValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutputValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvFlowmeterMaster As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents IdDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FlowmeterIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ProjectIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EngineModelDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FlowmeterMapIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents RawChannelDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalChannelDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotChannelDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CustomOutputUnitDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents IdDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TableInputUnitDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents TableOutputUnitDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents SerialNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalibrationDueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SgDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
End Class

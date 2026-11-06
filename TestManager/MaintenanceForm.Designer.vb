<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MaintenanceForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MaintenanceForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.m_UutCollection = New CEL.Data.BusinessObjects.UutCollection()
        Me.QuestionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.UutBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OptionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ChoiceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ChoiceItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_UutChoiceItemsCollection = New CEL.Data.BusinessObjects.UUTChoiceItemsCollection()
        Me.CalibrationConstantBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TripTypeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_UutTripTypeCollection = New CEL.Data.BusinessObjects.UUTTripTypeCollection()
        Me.m_UutChoiceCollection = New CEL.Data.BusinessObjects.UUTChoiceCollection()
        Me.TripsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CustomParametersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SecurityLevelBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_SecurityLevelCollection = New CEL.Data.BusinessObjects.SecurityLevelCollection()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblSecurityLevel = New System.Windows.Forms.Label()
        Me.lblEngineModel = New System.Windows.Forms.Label()
        Me.cboSecurityLevel = New System.Windows.Forms.ComboBox()
        Me.cboEngineModel = New System.Windows.Forms.ComboBox()
        Me.tabMaintenance = New System.Windows.Forms.TabControl()
        Me.tabQuestions = New System.Windows.Forms.TabPage()
        Me.dgvQuestions = New System.Windows.Forms.DataGridView()
        Me.QuestionIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QuestionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataTypeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DefaultValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HiLimitDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LoLimitDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MandatoryDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.NoteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DefaultValueUpdatableDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.UnavailableDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabOptions = New System.Windows.Forms.TabPage()
        Me.dgvOptions = New System.Windows.Forms.DataGridView()
        Me.OptionIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OptionTitleDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DefaultValueDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoteDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnavailableDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabChoices = New System.Windows.Forms.TabPage()
        Me.dgvChoices = New System.Windows.Forms.DataGridView()
        Me.ChoiceIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChoiceTitleDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChoiceStyleDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataTypeDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DefaultIndexDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MandatoryDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.NoteDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomSelectQueryDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomUpdateQueryDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnavailableDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabChoiceItems = New System.Windows.Forms.TabPage()
        Me.dgvChoiceItems = New System.Windows.Forms.DataGridView()
        Me.ChoiceItemsProjectIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChoiceItemsEngineModelDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChoiceItemsChoiceIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChoiceItemsItemIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChoiceItemsItemDescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChoiceItemsNoteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChoiceItemsUnavailableDataGridViewCheckBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.tabCalibrationConstants = New System.Windows.Forms.TabPage()
        Me.dgvCalibrationConstants = New System.Windows.Forms.DataGridView()
        Me.Constant1NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CalibrationConstant1DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Constant2NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CalibrationConstant2DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Constant3NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CalibrationConstant3DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabLimits = New System.Windows.Forms.TabPage()
        Me.dgvLimits = New System.Windows.Forms.DataGridView()
        Me.tabServiceBulletin = New System.Windows.Forms.TabPage()
        Me.dgvServiceBulletin = New System.Windows.Forms.DataGridView()
        Me.tabCustomParameters = New System.Windows.Forms.TabPage()
        Me.dgvCustomParameters = New System.Windows.Forms.DataGridView()
        Me.ParameterNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AvailableDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TripIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TripTypeIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoteDataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Expression = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.QuestionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UutBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChoiceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChoiceItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CalibrationConstantBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TripTypeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TripsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomParametersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SecurityLevelBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMaintenance.SuspendLayout()
        Me.tabQuestions.SuspendLayout()
        CType(Me.dgvQuestions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOptions.SuspendLayout()
        CType(Me.dgvOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabChoices.SuspendLayout()
        CType(Me.dgvChoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabChoiceItems.SuspendLayout()
        CType(Me.dgvChoiceItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabCalibrationConstants.SuspendLayout()
        CType(Me.dgvCalibrationConstants, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLimits.SuspendLayout()
        CType(Me.dgvLimits, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabServiceBulletin.SuspendLayout()
        CType(Me.dgvServiceBulletin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabCustomParameters.SuspendLayout()
        CType(Me.dgvCustomParameters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'm_UutCollection
        '
        Me.m_UutCollection.AllowDelete = True
        Me.m_UutCollection.AllowEdit = True
        Me.m_UutCollection.AllowNew = True
        Me.m_UutCollection.EnableHierarchicalBinding = True
        Me.m_UutCollection.Filter = Nothing
        '
        'QuestionBindingSource
        '
        Me.QuestionBindingSource.DataMember = "UUTQuestionCollectionByProjectID"
        Me.QuestionBindingSource.DataSource = Me.UutBindingSource
        '
        'UutBindingSource
        '
        Me.UutBindingSource.DataSource = Me.m_UutCollection
        '
        'OptionBindingSource
        '
        Me.OptionBindingSource.DataMember = "UUTOptionCollectionByProjectID"
        Me.OptionBindingSource.DataSource = Me.UutBindingSource
        '
        'ChoiceBindingSource
        '
        Me.ChoiceBindingSource.DataMember = "UUTChoiceCollectionByProjectID"
        Me.ChoiceBindingSource.DataSource = Me.UutBindingSource
        '
        'ChoiceItemsBindingSource
        '
        Me.ChoiceItemsBindingSource.DataSource = Me.m_UutChoiceItemsCollection
        '
        'm_UutChoiceItemsCollection
        '
        Me.m_UutChoiceItemsCollection.AllowDelete = True
        Me.m_UutChoiceItemsCollection.AllowEdit = True
        Me.m_UutChoiceItemsCollection.AllowNew = True
        Me.m_UutChoiceItemsCollection.EnableHierarchicalBinding = True
        Me.m_UutChoiceItemsCollection.Filter = Nothing
        '
        'CalibrationConstantBindingSource
        '
        Me.CalibrationConstantBindingSource.AllowNew = False
        Me.CalibrationConstantBindingSource.DataSource = Me.UutBindingSource
        '
        'TripTypeBindingSource
        '
        Me.TripTypeBindingSource.DataSource = Me.m_UutTripTypeCollection
        '
        'm_UutTripTypeCollection
        '
        Me.m_UutTripTypeCollection.AllowDelete = True
        Me.m_UutTripTypeCollection.AllowEdit = True
        Me.m_UutTripTypeCollection.AllowNew = True
        Me.m_UutTripTypeCollection.EnableHierarchicalBinding = True
        Me.m_UutTripTypeCollection.Filter = Nothing
        '
        'm_UutChoiceCollection
        '
        Me.m_UutChoiceCollection.AllowDelete = True
        Me.m_UutChoiceCollection.AllowEdit = True
        Me.m_UutChoiceCollection.AllowNew = True
        Me.m_UutChoiceCollection.EnableHierarchicalBinding = True
        Me.m_UutChoiceCollection.Filter = Nothing
        '
        'TripsBindingSource
        '
        Me.TripsBindingSource.AllowNew = False
        Me.TripsBindingSource.DataMember = "UUTTripCollectionByProjectID"
        Me.TripsBindingSource.DataSource = Me.UutBindingSource
        '
        'CustomParametersBindingSource
        '
        Me.CustomParametersBindingSource.DataMember = "CustomParametersCollectionByProjectID"
        Me.CustomParametersBindingSource.DataSource = Me.UutBindingSource
        '
        'SecurityLevelBindingSource
        '
        Me.SecurityLevelBindingSource.DataSource = Me.m_SecurityLevelCollection
        '
        'm_SecurityLevelCollection
        '
        Me.m_SecurityLevelCollection.AllowDelete = True
        Me.m_SecurityLevelCollection.AllowEdit = True
        Me.m_SecurityLevelCollection.AllowNew = True
        Me.m_SecurityLevelCollection.EnableHierarchicalBinding = False
        Me.m_SecurityLevelCollection.Filter = Nothing
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
        Me.GroupControl1.Controls.Add(Me.lblTitle)
        Me.GroupControl1.Controls.Add(Me.btnSave)
        Me.GroupControl1.Controls.Add(Me.btnClose)
        Me.GroupControl1.Controls.Add(Me.btnCancel)
        Me.GroupControl1.Controls.Add(Me.lblSecurityLevel)
        Me.GroupControl1.Controls.Add(Me.lblEngineModel)
        Me.GroupControl1.Controls.Add(Me.cboSecurityLevel)
        Me.GroupControl1.Controls.Add(Me.cboEngineModel)
        Me.GroupControl1.Controls.Add(Me.tabMaintenance)
        Me.GroupControl1.Controls.Add(Me.Label2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(958, 629)
        Me.GroupControl1.TabIndex = 114
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(12, 19)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.PictureEdit1.Size = New System.Drawing.Size(119, 100)
        Me.PictureEdit1.TabIndex = 121
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTitle.Font = New System.Drawing.Font("Arial Black", 26.25!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(284, 19)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(362, 50)
        Me.lblTitle.TabIndex = 120
        Me.lblTitle.Text = "UUT Maintenance"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(710, 594)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 119
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(872, 593)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 118
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(791, 593)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 117
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblSecurityLevel
        '
        Me.lblSecurityLevel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblSecurityLevel.AutoSize = True
        Me.lblSecurityLevel.BackColor = System.Drawing.Color.Transparent
        Me.lblSecurityLevel.ForeColor = System.Drawing.Color.White
        Me.lblSecurityLevel.Location = New System.Drawing.Point(442, 93)
        Me.lblSecurityLevel.Name = "lblSecurityLevel"
        Me.lblSecurityLevel.Size = New System.Drawing.Size(74, 13)
        Me.lblSecurityLevel.TabIndex = 116
        Me.lblSecurityLevel.Text = "Security Level"
        '
        'lblEngineModel
        '
        Me.lblEngineModel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblEngineModel.AutoSize = True
        Me.lblEngineModel.BackColor = System.Drawing.Color.Transparent
        Me.lblEngineModel.ForeColor = System.Drawing.Color.White
        Me.lblEngineModel.Location = New System.Drawing.Point(143, 93)
        Me.lblEngineModel.Name = "lblEngineModel"
        Me.lblEngineModel.Size = New System.Drawing.Size(72, 13)
        Me.lblEngineModel.TabIndex = 115
        Me.lblEngineModel.Text = "Engine Model"
        '
        'cboSecurityLevel
        '
        Me.cboSecurityLevel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboSecurityLevel.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.UutBindingSource, "SecurityLevelID", True))
        Me.cboSecurityLevel.DataSource = Me.SecurityLevelBindingSource
        Me.cboSecurityLevel.DisplayMember = "Description"
        Me.cboSecurityLevel.FormattingEnabled = True
        Me.cboSecurityLevel.Location = New System.Drawing.Point(522, 90)
        Me.cboSecurityLevel.Name = "cboSecurityLevel"
        Me.cboSecurityLevel.Size = New System.Drawing.Size(171, 21)
        Me.cboSecurityLevel.TabIndex = 114
        Me.cboSecurityLevel.ValueMember = "SecurityLevelID"
        '
        'cboEngineModel
        '
        Me.cboEngineModel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboEngineModel.DisplayMember = "Id"
        Me.cboEngineModel.FormattingEnabled = True
        Me.cboEngineModel.Location = New System.Drawing.Point(221, 90)
        Me.cboEngineModel.Name = "cboEngineModel"
        Me.cboEngineModel.Size = New System.Drawing.Size(171, 21)
        Me.cboEngineModel.TabIndex = 113
        Me.cboEngineModel.ValueMember = "Id"
        '
        'tabMaintenance
        '
        Me.tabMaintenance.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabMaintenance.Controls.Add(Me.tabQuestions)
        Me.tabMaintenance.Controls.Add(Me.tabOptions)
        Me.tabMaintenance.Controls.Add(Me.tabChoices)
        Me.tabMaintenance.Controls.Add(Me.tabChoiceItems)
        Me.tabMaintenance.Controls.Add(Me.tabCalibrationConstants)
        Me.tabMaintenance.Controls.Add(Me.tabLimits)
        Me.tabMaintenance.Controls.Add(Me.tabServiceBulletin)
        Me.tabMaintenance.Controls.Add(Me.tabCustomParameters)
        Me.tabMaintenance.Location = New System.Drawing.Point(11, 131)
        Me.tabMaintenance.Name = "tabMaintenance"
        Me.tabMaintenance.SelectedIndex = 0
        Me.tabMaintenance.Size = New System.Drawing.Size(936, 453)
        Me.tabMaintenance.TabIndex = 112
        '
        'tabQuestions
        '
        Me.tabQuestions.Controls.Add(Me.dgvQuestions)
        Me.tabQuestions.Location = New System.Drawing.Point(4, 22)
        Me.tabQuestions.Name = "tabQuestions"
        Me.tabQuestions.Padding = New System.Windows.Forms.Padding(3)
        Me.tabQuestions.Size = New System.Drawing.Size(928, 427)
        Me.tabQuestions.TabIndex = 0
        Me.tabQuestions.Text = "Questions"
        Me.tabQuestions.UseVisualStyleBackColor = True
        '
        'dgvQuestions
        '
        Me.dgvQuestions.AutoGenerateColumns = False
        Me.dgvQuestions.BackgroundColor = System.Drawing.Color.Black
        Me.dgvQuestions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvQuestions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.QuestionIDDataGridViewTextBoxColumn, Me.QuestionDataGridViewTextBoxColumn, Me.DataTypeDataGridViewTextBoxColumn, Me.DefaultValueDataGridViewTextBoxColumn, Me.HiLimitDataGridViewTextBoxColumn, Me.LoLimitDataGridViewTextBoxColumn, Me.MandatoryDataGridViewTextBoxColumn, Me.NoteDataGridViewTextBoxColumn, Me.DefaultValueUpdatableDataGridViewTextBoxColumn, Me.UnavailableDataGridViewTextBoxColumn})
        Me.dgvQuestions.DataBindings.Add(New System.Windows.Forms.Binding("Tag", Me.m_UutCollection, "UUTQuestionCollectionByProjectID.ProjectID", True))
        Me.dgvQuestions.DataSource = Me.QuestionBindingSource
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvQuestions.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvQuestions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvQuestions.Location = New System.Drawing.Point(3, 3)
        Me.dgvQuestions.Name = "dgvQuestions"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvQuestions.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvQuestions.Size = New System.Drawing.Size(922, 421)
        Me.dgvQuestions.TabIndex = 0
        '
        'QuestionIDDataGridViewTextBoxColumn
        '
        Me.QuestionIDDataGridViewTextBoxColumn.DataPropertyName = "QuestionID"
        Me.QuestionIDDataGridViewTextBoxColumn.HeaderText = "QuestionID"
        Me.QuestionIDDataGridViewTextBoxColumn.Name = "QuestionIDDataGridViewTextBoxColumn"
        Me.QuestionIDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'QuestionDataGridViewTextBoxColumn
        '
        Me.QuestionDataGridViewTextBoxColumn.DataPropertyName = "Question"
        Me.QuestionDataGridViewTextBoxColumn.HeaderText = "Question"
        Me.QuestionDataGridViewTextBoxColumn.Name = "QuestionDataGridViewTextBoxColumn"
        Me.QuestionDataGridViewTextBoxColumn.ReadOnly = True
        '
        'DataTypeDataGridViewTextBoxColumn
        '
        Me.DataTypeDataGridViewTextBoxColumn.DataPropertyName = "DataType"
        Me.DataTypeDataGridViewTextBoxColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.DataTypeDataGridViewTextBoxColumn.HeaderText = "DataType"
        Me.DataTypeDataGridViewTextBoxColumn.Items.AddRange(New Object() {"TEXT", "VALUE"})
        Me.DataTypeDataGridViewTextBoxColumn.Name = "DataTypeDataGridViewTextBoxColumn"
        Me.DataTypeDataGridViewTextBoxColumn.ReadOnly = True
        Me.DataTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DefaultValueDataGridViewTextBoxColumn
        '
        Me.DefaultValueDataGridViewTextBoxColumn.DataPropertyName = "DefaultValue"
        Me.DefaultValueDataGridViewTextBoxColumn.HeaderText = "DefaultValue"
        Me.DefaultValueDataGridViewTextBoxColumn.Name = "DefaultValueDataGridViewTextBoxColumn"
        '
        'HiLimitDataGridViewTextBoxColumn
        '
        Me.HiLimitDataGridViewTextBoxColumn.DataPropertyName = "HiLimit"
        Me.HiLimitDataGridViewTextBoxColumn.HeaderText = "HiLimit"
        Me.HiLimitDataGridViewTextBoxColumn.Name = "HiLimitDataGridViewTextBoxColumn"
        '
        'LoLimitDataGridViewTextBoxColumn
        '
        Me.LoLimitDataGridViewTextBoxColumn.DataPropertyName = "LoLimit"
        Me.LoLimitDataGridViewTextBoxColumn.HeaderText = "LoLimit"
        Me.LoLimitDataGridViewTextBoxColumn.Name = "LoLimitDataGridViewTextBoxColumn"
        '
        'MandatoryDataGridViewTextBoxColumn
        '
        Me.MandatoryDataGridViewTextBoxColumn.DataPropertyName = "Mandatory"
        Me.MandatoryDataGridViewTextBoxColumn.HeaderText = "Mandatory"
        Me.MandatoryDataGridViewTextBoxColumn.Name = "MandatoryDataGridViewTextBoxColumn"
        Me.MandatoryDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MandatoryDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'NoteDataGridViewTextBoxColumn
        '
        Me.NoteDataGridViewTextBoxColumn.DataPropertyName = "Note"
        Me.NoteDataGridViewTextBoxColumn.HeaderText = "Note"
        Me.NoteDataGridViewTextBoxColumn.Name = "NoteDataGridViewTextBoxColumn"
        Me.NoteDataGridViewTextBoxColumn.Visible = False
        '
        'DefaultValueUpdatableDataGridViewTextBoxColumn
        '
        Me.DefaultValueUpdatableDataGridViewTextBoxColumn.DataPropertyName = "DefaultValueUpdatable"
        Me.DefaultValueUpdatableDataGridViewTextBoxColumn.HeaderText = "DefaultValueUpdatable"
        Me.DefaultValueUpdatableDataGridViewTextBoxColumn.Name = "DefaultValueUpdatableDataGridViewTextBoxColumn"
        Me.DefaultValueUpdatableDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DefaultValueUpdatableDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'UnavailableDataGridViewTextBoxColumn
        '
        Me.UnavailableDataGridViewTextBoxColumn.DataPropertyName = "Unavailable"
        Me.UnavailableDataGridViewTextBoxColumn.HeaderText = "Unavailable"
        Me.UnavailableDataGridViewTextBoxColumn.Name = "UnavailableDataGridViewTextBoxColumn"
        Me.UnavailableDataGridViewTextBoxColumn.Visible = False
        '
        'tabOptions
        '
        Me.tabOptions.Controls.Add(Me.dgvOptions)
        Me.tabOptions.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions.Name = "tabOptions"
        Me.tabOptions.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOptions.Size = New System.Drawing.Size(928, 427)
        Me.tabOptions.TabIndex = 1
        Me.tabOptions.Text = "Options"
        Me.tabOptions.UseVisualStyleBackColor = True
        '
        'dgvOptions
        '
        Me.dgvOptions.AutoGenerateColumns = False
        Me.dgvOptions.BackgroundColor = System.Drawing.Color.Black
        Me.dgvOptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOptions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OptionIDDataGridViewTextBoxColumn, Me.OptionTitleDataGridViewTextBoxColumn, Me.DefaultValueDataGridViewTextBoxColumn1, Me.NoteDataGridViewTextBoxColumn1, Me.UnavailableDataGridViewTextBoxColumn1})
        Me.dgvOptions.DataSource = Me.OptionBindingSource
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvOptions.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvOptions.Location = New System.Drawing.Point(3, 3)
        Me.dgvOptions.Name = "dgvOptions"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvOptions.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvOptions.Size = New System.Drawing.Size(922, 421)
        Me.dgvOptions.TabIndex = 0
        '
        'OptionIDDataGridViewTextBoxColumn
        '
        Me.OptionIDDataGridViewTextBoxColumn.DataPropertyName = "OptionID"
        Me.OptionIDDataGridViewTextBoxColumn.HeaderText = "OptionID"
        Me.OptionIDDataGridViewTextBoxColumn.Name = "OptionIDDataGridViewTextBoxColumn"
        Me.OptionIDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'OptionTitleDataGridViewTextBoxColumn
        '
        Me.OptionTitleDataGridViewTextBoxColumn.DataPropertyName = "OptionTitle"
        Me.OptionTitleDataGridViewTextBoxColumn.HeaderText = "OptionTitle"
        Me.OptionTitleDataGridViewTextBoxColumn.Name = "OptionTitleDataGridViewTextBoxColumn"
        Me.OptionTitleDataGridViewTextBoxColumn.ReadOnly = True
        Me.OptionTitleDataGridViewTextBoxColumn.Width = 200
        '
        'DefaultValueDataGridViewTextBoxColumn1
        '
        Me.DefaultValueDataGridViewTextBoxColumn1.DataPropertyName = "DefaultValue"
        Me.DefaultValueDataGridViewTextBoxColumn1.HeaderText = "DefaultValue"
        Me.DefaultValueDataGridViewTextBoxColumn1.Name = "DefaultValueDataGridViewTextBoxColumn1"
        '
        'NoteDataGridViewTextBoxColumn1
        '
        Me.NoteDataGridViewTextBoxColumn1.DataPropertyName = "Note"
        Me.NoteDataGridViewTextBoxColumn1.HeaderText = "Note"
        Me.NoteDataGridViewTextBoxColumn1.Name = "NoteDataGridViewTextBoxColumn1"
        Me.NoteDataGridViewTextBoxColumn1.Visible = False
        '
        'UnavailableDataGridViewTextBoxColumn1
        '
        Me.UnavailableDataGridViewTextBoxColumn1.DataPropertyName = "Unavailable"
        Me.UnavailableDataGridViewTextBoxColumn1.HeaderText = "Unavailable"
        Me.UnavailableDataGridViewTextBoxColumn1.Name = "UnavailableDataGridViewTextBoxColumn1"
        Me.UnavailableDataGridViewTextBoxColumn1.Visible = False
        '
        'tabChoices
        '
        Me.tabChoices.Controls.Add(Me.dgvChoices)
        Me.tabChoices.Location = New System.Drawing.Point(4, 22)
        Me.tabChoices.Name = "tabChoices"
        Me.tabChoices.Padding = New System.Windows.Forms.Padding(3)
        Me.tabChoices.Size = New System.Drawing.Size(928, 427)
        Me.tabChoices.TabIndex = 2
        Me.tabChoices.Text = "Choices"
        Me.tabChoices.UseVisualStyleBackColor = True
        '
        'dgvChoices
        '
        Me.dgvChoices.AutoGenerateColumns = False
        Me.dgvChoices.BackgroundColor = System.Drawing.Color.Black
        Me.dgvChoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChoices.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ChoiceIDDataGridViewTextBoxColumn, Me.ChoiceTitleDataGridViewTextBoxColumn, Me.ChoiceStyleDataGridViewTextBoxColumn, Me.DataTypeDataGridViewTextBoxColumn1, Me.DefaultIndexDataGridViewTextBoxColumn, Me.MandatoryDataGridViewTextBoxColumn1, Me.NoteDataGridViewTextBoxColumn2, Me.CustomSelectQueryDataGridViewTextBoxColumn, Me.CustomUpdateQueryDataGridViewTextBoxColumn, Me.UnavailableDataGridViewTextBoxColumn2})
        Me.dgvChoices.DataSource = Me.ChoiceBindingSource
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvChoices.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvChoices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvChoices.Location = New System.Drawing.Point(3, 3)
        Me.dgvChoices.Name = "dgvChoices"
        Me.dgvChoices.Size = New System.Drawing.Size(922, 421)
        Me.dgvChoices.TabIndex = 0
        '
        'ChoiceIDDataGridViewTextBoxColumn
        '
        Me.ChoiceIDDataGridViewTextBoxColumn.DataPropertyName = "ChoiceID"
        Me.ChoiceIDDataGridViewTextBoxColumn.HeaderText = "ChoiceID"
        Me.ChoiceIDDataGridViewTextBoxColumn.Name = "ChoiceIDDataGridViewTextBoxColumn"
        Me.ChoiceIDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ChoiceTitleDataGridViewTextBoxColumn
        '
        Me.ChoiceTitleDataGridViewTextBoxColumn.DataPropertyName = "ChoiceTitle"
        Me.ChoiceTitleDataGridViewTextBoxColumn.HeaderText = "ChoiceTitle"
        Me.ChoiceTitleDataGridViewTextBoxColumn.Name = "ChoiceTitleDataGridViewTextBoxColumn"
        Me.ChoiceTitleDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ChoiceStyleDataGridViewTextBoxColumn
        '
        Me.ChoiceStyleDataGridViewTextBoxColumn.DataPropertyName = "ChoiceStyle"
        Me.ChoiceStyleDataGridViewTextBoxColumn.HeaderText = "ChoiceStyle"
        Me.ChoiceStyleDataGridViewTextBoxColumn.Name = "ChoiceStyleDataGridViewTextBoxColumn"
        Me.ChoiceStyleDataGridViewTextBoxColumn.ReadOnly = True
        '
        'DataTypeDataGridViewTextBoxColumn1
        '
        Me.DataTypeDataGridViewTextBoxColumn1.DataPropertyName = "DataType"
        Me.DataTypeDataGridViewTextBoxColumn1.HeaderText = "DataType"
        Me.DataTypeDataGridViewTextBoxColumn1.Name = "DataTypeDataGridViewTextBoxColumn1"
        Me.DataTypeDataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DefaultIndexDataGridViewTextBoxColumn
        '
        Me.DefaultIndexDataGridViewTextBoxColumn.DataPropertyName = "DefaultIndex"
        Me.DefaultIndexDataGridViewTextBoxColumn.HeaderText = "DefaultIndex"
        Me.DefaultIndexDataGridViewTextBoxColumn.Name = "DefaultIndexDataGridViewTextBoxColumn"
        '
        'MandatoryDataGridViewTextBoxColumn1
        '
        Me.MandatoryDataGridViewTextBoxColumn1.DataPropertyName = "Mandatory"
        Me.MandatoryDataGridViewTextBoxColumn1.HeaderText = "Mandatory"
        Me.MandatoryDataGridViewTextBoxColumn1.Name = "MandatoryDataGridViewTextBoxColumn1"
        Me.MandatoryDataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MandatoryDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'NoteDataGridViewTextBoxColumn2
        '
        Me.NoteDataGridViewTextBoxColumn2.DataPropertyName = "Note"
        Me.NoteDataGridViewTextBoxColumn2.HeaderText = "Note"
        Me.NoteDataGridViewTextBoxColumn2.Name = "NoteDataGridViewTextBoxColumn2"
        '
        'CustomSelectQueryDataGridViewTextBoxColumn
        '
        Me.CustomSelectQueryDataGridViewTextBoxColumn.DataPropertyName = "CustomSelectQuery"
        Me.CustomSelectQueryDataGridViewTextBoxColumn.HeaderText = "CustomSelectQuery"
        Me.CustomSelectQueryDataGridViewTextBoxColumn.Name = "CustomSelectQueryDataGridViewTextBoxColumn"
        Me.CustomSelectQueryDataGridViewTextBoxColumn.ReadOnly = True
        Me.CustomSelectQueryDataGridViewTextBoxColumn.Visible = False
        '
        'CustomUpdateQueryDataGridViewTextBoxColumn
        '
        Me.CustomUpdateQueryDataGridViewTextBoxColumn.DataPropertyName = "CustomUpdateQuery"
        Me.CustomUpdateQueryDataGridViewTextBoxColumn.HeaderText = "CustomUpdateQuery"
        Me.CustomUpdateQueryDataGridViewTextBoxColumn.Name = "CustomUpdateQueryDataGridViewTextBoxColumn"
        Me.CustomUpdateQueryDataGridViewTextBoxColumn.ReadOnly = True
        Me.CustomUpdateQueryDataGridViewTextBoxColumn.Visible = False
        '
        'UnavailableDataGridViewTextBoxColumn2
        '
        Me.UnavailableDataGridViewTextBoxColumn2.DataPropertyName = "Unavailable"
        Me.UnavailableDataGridViewTextBoxColumn2.HeaderText = "Unavailable"
        Me.UnavailableDataGridViewTextBoxColumn2.Name = "UnavailableDataGridViewTextBoxColumn2"
        Me.UnavailableDataGridViewTextBoxColumn2.Visible = False
        '
        'tabChoiceItems
        '
        Me.tabChoiceItems.Controls.Add(Me.dgvChoiceItems)
        Me.tabChoiceItems.Location = New System.Drawing.Point(4, 22)
        Me.tabChoiceItems.Name = "tabChoiceItems"
        Me.tabChoiceItems.Padding = New System.Windows.Forms.Padding(3)
        Me.tabChoiceItems.Size = New System.Drawing.Size(928, 427)
        Me.tabChoiceItems.TabIndex = 11
        Me.tabChoiceItems.Text = "ChoiceItems"
        Me.tabChoiceItems.UseVisualStyleBackColor = True
        '
        'dgvChoiceItems
        '
        Me.dgvChoiceItems.AutoGenerateColumns = False
        Me.dgvChoiceItems.BackgroundColor = System.Drawing.Color.Black
        Me.dgvChoiceItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChoiceItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ChoiceItemsProjectIDDataGridViewTextBoxColumn, Me.ChoiceItemsEngineModelDataGridViewTextBoxColumn, Me.ChoiceItemsChoiceIDDataGridViewTextBoxColumn, Me.ChoiceItemsItemIDDataGridViewTextBoxColumn, Me.ChoiceItemsItemDescriptionDataGridViewTextBoxColumn, Me.ChoiceItemsNoteDataGridViewTextBoxColumn, Me.ChoiceItemsUnavailableDataGridViewCheckBoxColumn})
        Me.dgvChoiceItems.DataSource = Me.ChoiceItemsBindingSource
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvChoiceItems.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvChoiceItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvChoiceItems.Location = New System.Drawing.Point(3, 3)
        Me.dgvChoiceItems.Name = "dgvChoiceItems"
        Me.dgvChoiceItems.Size = New System.Drawing.Size(922, 421)
        Me.dgvChoiceItems.TabIndex = 0
        '
        'ChoiceItemsProjectIDDataGridViewTextBoxColumn
        '
        Me.ChoiceItemsProjectIDDataGridViewTextBoxColumn.DataPropertyName = "ProjectID"
        Me.ChoiceItemsProjectIDDataGridViewTextBoxColumn.HeaderText = "ProjectID"
        Me.ChoiceItemsProjectIDDataGridViewTextBoxColumn.Name = "ChoiceItemsProjectIDDataGridViewTextBoxColumn"
        '
        'ChoiceItemsEngineModelDataGridViewTextBoxColumn
        '
        Me.ChoiceItemsEngineModelDataGridViewTextBoxColumn.DataPropertyName = "EngineModel"
        Me.ChoiceItemsEngineModelDataGridViewTextBoxColumn.HeaderText = "EngineModel"
        Me.ChoiceItemsEngineModelDataGridViewTextBoxColumn.Name = "ChoiceItemsEngineModelDataGridViewTextBoxColumn"
        '
        'ChoiceItemsChoiceIDDataGridViewTextBoxColumn
        '
        Me.ChoiceItemsChoiceIDDataGridViewTextBoxColumn.DataPropertyName = "ChoiceID"
        Me.ChoiceItemsChoiceIDDataGridViewTextBoxColumn.HeaderText = "ChoiceID"
        Me.ChoiceItemsChoiceIDDataGridViewTextBoxColumn.Name = "ChoiceItemsChoiceIDDataGridViewTextBoxColumn"
        '
        'ChoiceItemsItemIDDataGridViewTextBoxColumn
        '
        Me.ChoiceItemsItemIDDataGridViewTextBoxColumn.DataPropertyName = "ItemID"
        Me.ChoiceItemsItemIDDataGridViewTextBoxColumn.HeaderText = "ItemID"
        Me.ChoiceItemsItemIDDataGridViewTextBoxColumn.Name = "ChoiceItemsItemIDDataGridViewTextBoxColumn"
        '
        'ChoiceItemsItemDescriptionDataGridViewTextBoxColumn
        '
        Me.ChoiceItemsItemDescriptionDataGridViewTextBoxColumn.DataPropertyName = "ItemDescription"
        Me.ChoiceItemsItemDescriptionDataGridViewTextBoxColumn.HeaderText = "ItemDescription"
        Me.ChoiceItemsItemDescriptionDataGridViewTextBoxColumn.Name = "ChoiceItemsItemDescriptionDataGridViewTextBoxColumn"
        Me.ChoiceItemsItemDescriptionDataGridViewTextBoxColumn.Width = 280
        '
        'ChoiceItemsNoteDataGridViewTextBoxColumn
        '
        Me.ChoiceItemsNoteDataGridViewTextBoxColumn.DataPropertyName = "Note"
        Me.ChoiceItemsNoteDataGridViewTextBoxColumn.HeaderText = "Note"
        Me.ChoiceItemsNoteDataGridViewTextBoxColumn.Name = "ChoiceItemsNoteDataGridViewTextBoxColumn"
        Me.ChoiceItemsNoteDataGridViewTextBoxColumn.Width = 200
        '
        'ChoiceItemsUnavailableDataGridViewCheckBoxColumn
        '
        Me.ChoiceItemsUnavailableDataGridViewCheckBoxColumn.DataPropertyName = "Unavailable"
        Me.ChoiceItemsUnavailableDataGridViewCheckBoxColumn.HeaderText = "Unavailable"
        Me.ChoiceItemsUnavailableDataGridViewCheckBoxColumn.Name = "ChoiceItemsUnavailableDataGridViewCheckBoxColumn"
        '
        'tabCalibrationConstants
        '
        Me.tabCalibrationConstants.Controls.Add(Me.dgvCalibrationConstants)
        Me.tabCalibrationConstants.Location = New System.Drawing.Point(4, 22)
        Me.tabCalibrationConstants.Name = "tabCalibrationConstants"
        Me.tabCalibrationConstants.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCalibrationConstants.Size = New System.Drawing.Size(928, 427)
        Me.tabCalibrationConstants.TabIndex = 3
        Me.tabCalibrationConstants.Text = "Calibration Constants"
        Me.tabCalibrationConstants.UseVisualStyleBackColor = True
        '
        'dgvCalibrationConstants
        '
        Me.dgvCalibrationConstants.AllowUserToAddRows = False
        Me.dgvCalibrationConstants.AllowUserToDeleteRows = False
        Me.dgvCalibrationConstants.AutoGenerateColumns = False
        Me.dgvCalibrationConstants.BackgroundColor = System.Drawing.Color.Black
        Me.dgvCalibrationConstants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCalibrationConstants.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Constant1NameDataGridViewTextBoxColumn, Me.CalibrationConstant1DataGridViewTextBoxColumn, Me.Constant2NameDataGridViewTextBoxColumn, Me.CalibrationConstant2DataGridViewTextBoxColumn, Me.Constant3NameDataGridViewTextBoxColumn, Me.CalibrationConstant3DataGridViewTextBoxColumn})
        Me.dgvCalibrationConstants.DataSource = Me.CalibrationConstantBindingSource
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCalibrationConstants.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvCalibrationConstants.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCalibrationConstants.Location = New System.Drawing.Point(3, 3)
        Me.dgvCalibrationConstants.Name = "dgvCalibrationConstants"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.Silver
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCalibrationConstants.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvCalibrationConstants.Size = New System.Drawing.Size(922, 421)
        Me.dgvCalibrationConstants.TabIndex = 0
        '
        'Constant1NameDataGridViewTextBoxColumn
        '
        Me.Constant1NameDataGridViewTextBoxColumn.DataPropertyName = "Constant1Name"
        Me.Constant1NameDataGridViewTextBoxColumn.HeaderText = "Constant1Name"
        Me.Constant1NameDataGridViewTextBoxColumn.Name = "Constant1NameDataGridViewTextBoxColumn"
        Me.Constant1NameDataGridViewTextBoxColumn.ReadOnly = True
        Me.Constant1NameDataGridViewTextBoxColumn.Width = 150
        '
        'CalibrationConstant1DataGridViewTextBoxColumn
        '
        Me.CalibrationConstant1DataGridViewTextBoxColumn.DataPropertyName = "CalibrationConstant1"
        Me.CalibrationConstant1DataGridViewTextBoxColumn.HeaderText = "Value 1"
        Me.CalibrationConstant1DataGridViewTextBoxColumn.Name = "CalibrationConstant1DataGridViewTextBoxColumn"
        '
        'Constant2NameDataGridViewTextBoxColumn
        '
        Me.Constant2NameDataGridViewTextBoxColumn.DataPropertyName = "Constant2Name"
        Me.Constant2NameDataGridViewTextBoxColumn.HeaderText = "Constant2Name"
        Me.Constant2NameDataGridViewTextBoxColumn.Name = "Constant2NameDataGridViewTextBoxColumn"
        Me.Constant2NameDataGridViewTextBoxColumn.ReadOnly = True
        Me.Constant2NameDataGridViewTextBoxColumn.Width = 150
        '
        'CalibrationConstant2DataGridViewTextBoxColumn
        '
        Me.CalibrationConstant2DataGridViewTextBoxColumn.DataPropertyName = "CalibrationConstant2"
        Me.CalibrationConstant2DataGridViewTextBoxColumn.HeaderText = "Value 2"
        Me.CalibrationConstant2DataGridViewTextBoxColumn.Name = "CalibrationConstant2DataGridViewTextBoxColumn"
        '
        'Constant3NameDataGridViewTextBoxColumn
        '
        Me.Constant3NameDataGridViewTextBoxColumn.DataPropertyName = "Constant3Name"
        Me.Constant3NameDataGridViewTextBoxColumn.HeaderText = "Constant3Name"
        Me.Constant3NameDataGridViewTextBoxColumn.Name = "Constant3NameDataGridViewTextBoxColumn"
        Me.Constant3NameDataGridViewTextBoxColumn.ReadOnly = True
        Me.Constant3NameDataGridViewTextBoxColumn.Width = 150
        '
        'CalibrationConstant3DataGridViewTextBoxColumn
        '
        Me.CalibrationConstant3DataGridViewTextBoxColumn.DataPropertyName = "CalibrationConstant3"
        Me.CalibrationConstant3DataGridViewTextBoxColumn.HeaderText = "Value 3"
        Me.CalibrationConstant3DataGridViewTextBoxColumn.Name = "CalibrationConstant3DataGridViewTextBoxColumn"
        '
        'tabLimits
        '
        Me.tabLimits.Controls.Add(Me.dgvLimits)
        Me.tabLimits.Location = New System.Drawing.Point(4, 22)
        Me.tabLimits.Name = "tabLimits"
        Me.tabLimits.Size = New System.Drawing.Size(928, 427)
        Me.tabLimits.TabIndex = 4
        Me.tabLimits.Text = "Limits"
        Me.tabLimits.UseVisualStyleBackColor = True
        '
        'dgvLimits
        '
        Me.dgvLimits.AllowUserToAddRows = False
        Me.dgvLimits.AllowUserToDeleteRows = False
        Me.dgvLimits.AutoGenerateColumns = False
        Me.dgvLimits.BackgroundColor = System.Drawing.Color.Black
        Me.dgvLimits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLimits.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TripIDDataGridViewTextBoxColumn, Me.TripTypeIDDataGridViewTextBoxColumn, Me.NameDataGridViewTextBoxColumn, Me.NoteDataGridViewTextBoxColumn3, Me.Expression})
        Me.dgvLimits.DataSource = Me.TripsBindingSource
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvLimits.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgvLimits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLimits.Location = New System.Drawing.Point(0, 0)
        Me.dgvLimits.Name = "dgvLimits"
        Me.dgvLimits.Size = New System.Drawing.Size(928, 427)
        Me.dgvLimits.TabIndex = 0
        '
        'tabServiceBulletin
        '
        Me.tabServiceBulletin.Controls.Add(Me.dgvServiceBulletin)
        Me.tabServiceBulletin.Location = New System.Drawing.Point(4, 22)
        Me.tabServiceBulletin.Name = "tabServiceBulletin"
        Me.tabServiceBulletin.Size = New System.Drawing.Size(928, 427)
        Me.tabServiceBulletin.TabIndex = 5
        Me.tabServiceBulletin.Text = "Service Bulletin"
        Me.tabServiceBulletin.UseVisualStyleBackColor = True
        '
        'dgvServiceBulletin
        '
        Me.dgvServiceBulletin.BackgroundColor = System.Drawing.Color.Black
        Me.dgvServiceBulletin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvServiceBulletin.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgvServiceBulletin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvServiceBulletin.Location = New System.Drawing.Point(0, 0)
        Me.dgvServiceBulletin.Name = "dgvServiceBulletin"
        Me.dgvServiceBulletin.Size = New System.Drawing.Size(928, 427)
        Me.dgvServiceBulletin.TabIndex = 0
        '
        'tabCustomParameters
        '
        Me.tabCustomParameters.Controls.Add(Me.dgvCustomParameters)
        Me.tabCustomParameters.Location = New System.Drawing.Point(4, 22)
        Me.tabCustomParameters.Name = "tabCustomParameters"
        Me.tabCustomParameters.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCustomParameters.Size = New System.Drawing.Size(928, 427)
        Me.tabCustomParameters.TabIndex = 6
        Me.tabCustomParameters.Text = "Custom Parameters"
        Me.tabCustomParameters.UseVisualStyleBackColor = True
        '
        'dgvCustomParameters
        '
        Me.dgvCustomParameters.AutoGenerateColumns = False
        Me.dgvCustomParameters.BackgroundColor = System.Drawing.Color.Black
        Me.dgvCustomParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCustomParameters.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ParameterNameDataGridViewTextBoxColumn, Me.TextDataGridViewTextBoxColumn, Me.AvailableDataGridViewTextBoxColumn1})
        Me.dgvCustomParameters.DataSource = Me.CustomParametersBindingSource
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCustomParameters.DefaultCellStyle = DataGridViewCellStyle11
        Me.dgvCustomParameters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCustomParameters.Location = New System.Drawing.Point(3, 3)
        Me.dgvCustomParameters.Name = "dgvCustomParameters"
        Me.dgvCustomParameters.Size = New System.Drawing.Size(922, 421)
        Me.dgvCustomParameters.TabIndex = 0
        '
        'ParameterNameDataGridViewTextBoxColumn
        '
        Me.ParameterNameDataGridViewTextBoxColumn.DataPropertyName = "ParameterName"
        Me.ParameterNameDataGridViewTextBoxColumn.HeaderText = "ParameterName"
        Me.ParameterNameDataGridViewTextBoxColumn.Name = "ParameterNameDataGridViewTextBoxColumn"
        Me.ParameterNameDataGridViewTextBoxColumn.ReadOnly = True
        Me.ParameterNameDataGridViewTextBoxColumn.Width = 225
        '
        'TextDataGridViewTextBoxColumn
        '
        Me.TextDataGridViewTextBoxColumn.DataPropertyName = "Text"
        Me.TextDataGridViewTextBoxColumn.HeaderText = "Text"
        Me.TextDataGridViewTextBoxColumn.Name = "TextDataGridViewTextBoxColumn"
        Me.TextDataGridViewTextBoxColumn.Width = 375
        '
        'AvailableDataGridViewTextBoxColumn1
        '
        Me.AvailableDataGridViewTextBoxColumn1.DataPropertyName = "Available"
        Me.AvailableDataGridViewTextBoxColumn1.HeaderText = "Available"
        Me.AvailableDataGridViewTextBoxColumn1.Name = "AvailableDataGridViewTextBoxColumn1"
        Me.AvailableDataGridViewTextBoxColumn1.ReadOnly = True
        Me.AvailableDataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AvailableDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(7, 611)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(215, 13)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "© 2018 CEL Aerospace Test Equipment Ltd"
        '
        'TripIDDataGridViewTextBoxColumn
        '
        Me.TripIDDataGridViewTextBoxColumn.DataPropertyName = "TripID"
        Me.TripIDDataGridViewTextBoxColumn.HeaderText = "TripID"
        Me.TripIDDataGridViewTextBoxColumn.Name = "TripIDDataGridViewTextBoxColumn"
        Me.TripIDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'TripTypeIDDataGridViewTextBoxColumn
        '
        Me.TripTypeIDDataGridViewTextBoxColumn.DataPropertyName = "TripTypeID"
        Me.TripTypeIDDataGridViewTextBoxColumn.DataSource = Me.TripTypeBindingSource
        Me.TripTypeIDDataGridViewTextBoxColumn.DisplayMember = "Description"
        Me.TripTypeIDDataGridViewTextBoxColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.TripTypeIDDataGridViewTextBoxColumn.HeaderText = "TripTypeID"
        Me.TripTypeIDDataGridViewTextBoxColumn.Name = "TripTypeIDDataGridViewTextBoxColumn"
        Me.TripTypeIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.TripTypeIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TripTypeIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.TripTypeIDDataGridViewTextBoxColumn.ValueMember = "TripTypeID"
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Name"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        Me.NameDataGridViewTextBoxColumn.Width = 200
        '
        'NoteDataGridViewTextBoxColumn3
        '
        Me.NoteDataGridViewTextBoxColumn3.DataPropertyName = "Note"
        Me.NoteDataGridViewTextBoxColumn3.HeaderText = "Note"
        Me.NoteDataGridViewTextBoxColumn3.Name = "NoteDataGridViewTextBoxColumn3"
        Me.NoteDataGridViewTextBoxColumn3.Visible = False
        '
        'Expression
        '
        Me.Expression.DataPropertyName = "Expression"
        Me.Expression.HeaderText = "Expression"
        Me.Expression.Name = "Expression"
        '
        'MaintenanceForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(958, 629)
        Me.Controls.Add(Me.GroupControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MaintenanceForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Maintenance"
        CType(Me.QuestionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UutBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChoiceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChoiceItemsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CalibrationConstantBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TripTypeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TripsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomParametersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SecurityLevelBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMaintenance.ResumeLayout(False)
        Me.tabQuestions.ResumeLayout(False)
        CType(Me.dgvQuestions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOptions.ResumeLayout(False)
        CType(Me.dgvOptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabChoices.ResumeLayout(False)
        CType(Me.dgvChoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabChoiceItems.ResumeLayout(False)
        CType(Me.dgvChoiceItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabCalibrationConstants.ResumeLayout(False)
        CType(Me.dgvCalibrationConstants, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLimits.ResumeLayout(False)
        CType(Me.dgvLimits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabServiceBulletin.ResumeLayout(False)
        CType(Me.dgvServiceBulletin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabCustomParameters.ResumeLayout(False)
        CType(Me.dgvCustomParameters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents QuestionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents OptionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ChoiceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ChoiceItemsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents UutBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents m_UutCollection As CEL.Data.BusinessObjects.UutCollection
    Friend WithEvents CalibrationConstantBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TripsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents m_SecurityLevelCollection As CEL.Data.BusinessObjects.SecurityLevelCollection
    Friend WithEvents SecurityLevelBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents m_UutTripTypeCollection As CEL.Data.BusinessObjects.UUTTripTypeCollection
    Friend WithEvents m_UutChoiceCollection As CEL.Data.BusinessObjects.UUTChoiceCollection
    Friend WithEvents m_UutChoiceItemsCollection As CEL.Data.BusinessObjects.UUTChoiceItemsCollection
    Friend WithEvents TripTypeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CustomParametersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblSecurityLevel As System.Windows.Forms.Label
    Friend WithEvents lblEngineModel As System.Windows.Forms.Label
    Friend WithEvents cboSecurityLevel As System.Windows.Forms.ComboBox
    Friend WithEvents cboEngineModel As System.Windows.Forms.ComboBox
    Friend WithEvents tabMaintenance As System.Windows.Forms.TabControl
    Friend WithEvents tabQuestions As System.Windows.Forms.TabPage
    Friend WithEvents dgvQuestions As System.Windows.Forms.DataGridView
    Friend WithEvents tabOptions As System.Windows.Forms.TabPage
    Friend WithEvents dgvOptions As System.Windows.Forms.DataGridView
    Friend WithEvents tabChoices As System.Windows.Forms.TabPage
    Friend WithEvents dgvChoices As System.Windows.Forms.DataGridView
    Friend WithEvents tabChoiceItems As System.Windows.Forms.TabPage
    Friend WithEvents dgvChoiceItems As System.Windows.Forms.DataGridView
    Friend WithEvents tabCalibrationConstants As System.Windows.Forms.TabPage
    Friend WithEvents dgvCalibrationConstants As System.Windows.Forms.DataGridView
    Friend WithEvents tabLimits As System.Windows.Forms.TabPage
    Friend WithEvents dgvLimits As System.Windows.Forms.DataGridView
    Friend WithEvents ValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tabServiceBulletin As System.Windows.Forms.TabPage
    Friend WithEvents dgvServiceBulletin As System.Windows.Forms.DataGridView
    Friend WithEvents tabCustomParameters As System.Windows.Forms.TabPage
    Friend WithEvents dgvCustomParameters As System.Windows.Forms.DataGridView
    Friend WithEvents ParameterNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AvailableDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents QuestionIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QuestionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataTypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DefaultValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HiLimitDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LoLimitDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MandatoryDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NoteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DefaultValueUpdatableDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents UnavailableDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OptionIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OptionTitleDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DefaultValueDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoteDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnavailableDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChoiceIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChoiceTitleDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChoiceStyleDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataTypeDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DefaultIndexDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MandatoryDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NoteDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CustomSelectQueryDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CustomUpdateQueryDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnavailableDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Constant1NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalibrationConstant1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Constant2NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalibrationConstant2DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Constant3NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalibrationConstant3DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChoiceItemsProjectIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChoiceItemsEngineModelDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChoiceItemsChoiceIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChoiceItemsItemIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChoiceItemsItemDescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChoiceItemsNoteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChoiceItemsUnavailableDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents TripIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TripTypeIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoteDataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Expression As System.Windows.Forms.DataGridViewTextBoxColumn

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChannelEditorForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChannelEditorForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BooleanChannelsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_HmiBooleanChannelsCollection = New CEL.Data.BusinessObjects.HMIBooleanChannelsCollection()
        Me.m_UutDynamicChannelCollectionCollection = New CEL.Data.BusinessObjects.UUTDynamicChannelCollectionCollection()
        Me.BooleanStateBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_BooleanStateCollection = New CEL.Data.BusinessObjects.BooleanStateCollection()
        Me.NumericChannelsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_HmiNumericChannelsCollection = New CEL.Data.BusinessObjects.HMINumericChannelsCollection()
        Me.EngineeringUnitBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_EngineeringUnitCollection = New CEL.Data.BusinessObjects.EngineeringUnitCollection()
        Me.m_UutCollection = New CEL.Data.BusinessObjects.UutCollection()
        Me.TextChannelsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_HmiTextChannelsCollection = New CEL.Data.BusinessObjects.HMITextChannelsCollection()
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.HMIChannelsMappingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.m_HmiChannelsMappingCollection = New CEL.Data.BusinessObjects.HMIChannelsMappingCollection()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.navBoolean = New DevExpress.XtraEditors.DataNavigator()
        Me.dgvBooleanChannels = New System.Windows.Forms.DataGridView()
        Me.CollectionNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChannelNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DestinationDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ResetLatchDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpressionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OPCGroupDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OPCItemIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TooltipDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FalseTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TrueTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FalseMessageTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TrueMessageTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SoundMessageDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InitialValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.FinalValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.txtBooleanOpcItemId = New System.Windows.Forms.TextBox()
        Me.lblBooleanOpcItemId = New System.Windows.Forms.Label()
        Me.txtBooleanOpcGroup = New System.Windows.Forms.TextBox()
        Me.lblBooleanOpcGroup = New System.Windows.Forms.Label()
        Me.cboBooleanFinalValue = New System.Windows.Forms.ComboBox()
        Me.m_BooleanStateCollection2 = New CEL.Data.BusinessObjects.BooleanStateCollection()
        Me.cboBooleanInitialValue = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBooleanChannelDestination = New System.Windows.Forms.TextBox()
        Me.txtBooleanChannelSource = New System.Windows.Forms.TextBox()
        Me.txtBooleanTooltip = New System.Windows.Forms.TextBox()
        Me.txtBooleanDescription = New System.Windows.Forms.TextBox()
        Me.cboCollectionNameBoolean = New System.Windows.Forms.ComboBox()
        Me.txtBooleanExpression = New System.Windows.Forms.TextBox()
        Me.txtBooleanTrueText = New System.Windows.Forms.TextBox()
        Me.txtBooleanFalseText = New System.Windows.Forms.TextBox()
        Me.txtBooleanChannelName = New System.Windows.Forms.TextBox()
        Me.lblBooleanChannelDestination = New System.Windows.Forms.Label()
        Me.lblBooleanTrueText = New System.Windows.Forms.Label()
        Me.lblBooleanFalseText = New System.Windows.Forms.Label()
        Me.lblBooleanTooltip = New System.Windows.Forms.Label()
        Me.lblBooleanDescription = New System.Windows.Forms.Label()
        Me.lblBooleanCollectionName = New System.Windows.Forms.Label()
        Me.lblBooleanChannelName = New System.Windows.Forms.Label()
        Me.lblBooleanExpression = New System.Windows.Forms.Label()
        Me.lblBooleanChannelSource = New System.Windows.Forms.Label()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.grpNumericChannelInformation = New DevExpress.XtraEditors.GroupControl()
        Me.txtNumericOpcItemId = New System.Windows.Forms.TextBox()
        Me.lblNumericOpcItemId = New System.Windows.Forms.Label()
        Me.txtNumericOpcGroup = New System.Windows.Forms.TextBox()
        Me.lblNumericOpcGroup = New System.Windows.Forms.Label()
        Me.btnViewThresholdSection = New DevExpress.XtraEditors.SimpleButton()
        Me.chkNumericPersistent = New System.Windows.Forms.CheckBox()
        Me.txtNumericFinalValue = New System.Windows.Forms.TextBox()
        Me.txtNumericInitialValue = New System.Windows.Forms.TextBox()
        Me.lblNumericFinalValue = New System.Windows.Forms.Label()
        Me.lblNumericInitialValue = New System.Windows.Forms.Label()
        Me.txtNumericEngUnit = New System.Windows.Forms.ComboBox()
        Me.txtNumericMaximum = New System.Windows.Forms.TextBox()
        Me.txtNumericNbDecimal = New System.Windows.Forms.TextBox()
        Me.txtNumericChannelDestination = New System.Windows.Forms.TextBox()
        Me.txtNumericChannelSource = New System.Windows.Forms.TextBox()
        Me.txtNumericTooltip = New System.Windows.Forms.TextBox()
        Me.txtNumericDescription = New System.Windows.Forms.TextBox()
        Me.cboCollectionNameNumeric = New System.Windows.Forms.ComboBox()
        Me.txtNumericExpression = New System.Windows.Forms.TextBox()
        Me.txtNumericMinimum = New System.Windows.Forms.TextBox()
        Me.txtNumericFormat = New System.Windows.Forms.TextBox()
        Me.txtNumericChannelName = New System.Windows.Forms.TextBox()
        Me.lblNumericExpression = New System.Windows.Forms.Label()
        Me.lblNumericDescription = New System.Windows.Forms.Label()
        Me.lblNumericCollectionName = New System.Windows.Forms.Label()
        Me.lblNumericChannelName = New System.Windows.Forms.Label()
        Me.lblNumericEngUnit = New System.Windows.Forms.Label()
        Me.lblNumericMaximum = New System.Windows.Forms.Label()
        Me.lblNumericNbDecimal = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblNumericChannelSource = New System.Windows.Forms.Label()
        Me.lblNumericMinimum = New System.Windows.Forms.Label()
        Me.lblNumericFormat = New System.Windows.Forms.Label()
        Me.lblNumericTooltiptext = New System.Windows.Forms.Label()
        Me.grpNumericThresholdInformation = New DevExpress.XtraEditors.GroupControl()
        Me.chkNumericLowTriggerEnabled = New System.Windows.Forms.CheckBox()
        Me.chkNumericHighTriggerEnabled = New System.Windows.Forms.CheckBox()
        Me.lblNumericLowTrigger = New System.Windows.Forms.Label()
        Me.txtNumericLowTriggerValue = New System.Windows.Forms.TextBox()
        Me.txtNumericHighTriggerValue = New System.Windows.Forms.TextBox()
        Me.lblNumericTrigger = New System.Windows.Forms.Label()
        Me.ColorPickEdit5 = New DevExpress.XtraEditors.ColorPickEdit()
        Me.ColorPickEdit4 = New DevExpress.XtraEditors.ColorPickEdit()
        Me.ColorPickEdit3 = New DevExpress.XtraEditors.ColorPickEdit()
        Me.ColorPickEdit2 = New DevExpress.XtraEditors.ColorPickEdit()
        Me.ColorPickEdit1 = New DevExpress.XtraEditors.ColorPickEdit()
        Me.txtLowLowMessage = New System.Windows.Forms.TextBox()
        Me.txtLowMessage = New System.Windows.Forms.TextBox()
        Me.txtHighMessage = New System.Windows.Forms.TextBox()
        Me.txtHighHighMessage = New System.Windows.Forms.TextBox()
        Me.txtLowLowColor = New System.Windows.Forms.TextBox()
        Me.txtLowColor = New System.Windows.Forms.TextBox()
        Me.txtNormalColor = New System.Windows.Forms.TextBox()
        Me.txtHighColor = New System.Windows.Forms.TextBox()
        Me.txtHighHighColor = New System.Windows.Forms.TextBox()
        Me.txtLowLowChannelSource = New System.Windows.Forms.TextBox()
        Me.txtLowChannelSource = New System.Windows.Forms.TextBox()
        Me.txtHighChannelSource = New System.Windows.Forms.TextBox()
        Me.txtHighHighChannelSource = New System.Windows.Forms.TextBox()
        Me.txtLowLowValue = New System.Windows.Forms.TextBox()
        Me.lblLowLow = New System.Windows.Forms.Label()
        Me.txtLowValue = New System.Windows.Forms.TextBox()
        Me.lblLow = New System.Windows.Forms.Label()
        Me.txtNormalValue = New System.Windows.Forms.TextBox()
        Me.lblNormal = New System.Windows.Forms.Label()
        Me.txtHighValue = New System.Windows.Forms.TextBox()
        Me.lblHigh = New System.Windows.Forms.Label()
        Me.txtHighHighValue = New System.Windows.Forms.TextBox()
        Me.lblHighHigh = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblColor = New System.Windows.Forms.Label()
        Me.lblChannelSource = New System.Windows.Forms.Label()
        Me.lblValue = New System.Windows.Forms.Label()
        Me.dgvNumericChannels = New System.Windows.Forms.DataGridView()
        Me.CollectionNameDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChannelNameDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DestinationDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpressionDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OPCGroupDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OPCItemIDDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TooltipDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DecimalDigitsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FormatDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MinimumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaximumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EngUnitIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.MeanEnabledDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MeanPeriodDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdHighHighDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdHighHighSourceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdHighHighColorDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdHighHighMessageTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdHighDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdHighSourceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdHighColorDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdHighMessageTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdLowLowDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdLowLowSourceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdLowLowColorDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdLowLowMessageTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdLowDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdLowSourceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdLowColorDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdLowMessageTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdNormalDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ThresholdNormalColorDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HighTriggerDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HighTriggerEnabledDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LowTriggerDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LowTriggerEnabledDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SpeechMessageTextDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InitialValueDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FinalValueDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PersistentDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.navNumeric = New DevExpress.XtraEditors.DataNavigator()
        Me.XtraTabPage3 = New DevExpress.XtraTab.XtraTabPage()
        Me.navText = New DevExpress.XtraEditors.DataNavigator()
        Me.grpTextChannels = New DevExpress.XtraEditors.GroupControl()
        Me.txtTextOpcItemId = New System.Windows.Forms.TextBox()
        Me.lblTextOpcItemId = New System.Windows.Forms.Label()
        Me.txtTextOpcGroup = New System.Windows.Forms.TextBox()
        Me.lblTextOpcGroup = New System.Windows.Forms.Label()
        Me.txtTextFinalValue = New System.Windows.Forms.TextBox()
        Me.txtTextInitialValue = New System.Windows.Forms.TextBox()
        Me.lblTextFinalValue = New System.Windows.Forms.Label()
        Me.lblTextInitialValue = New System.Windows.Forms.Label()
        Me.txtTextChannelDestination = New System.Windows.Forms.TextBox()
        Me.txtTextChannelSource = New System.Windows.Forms.TextBox()
        Me.txtTextTooltip = New System.Windows.Forms.TextBox()
        Me.txtTextDescription = New System.Windows.Forms.TextBox()
        Me.cboTextCollectionName = New System.Windows.Forms.ComboBox()
        Me.txtTextChannelName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.dgvTextChannels = New System.Windows.Forms.DataGridView()
        Me.CollectionNameDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChannelNameDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DestinationDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OPCGroupDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OPCItemIDDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TooltipDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InitialValueDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FinalValueDataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.XtraTabPage4 = New DevExpress.XtraTab.XtraTabPage()
        Me.navMapping = New DevExpress.XtraEditors.DataNavigator()
        Me.grpMappingInformation = New DevExpress.XtraEditors.GroupControl()
        Me.txtMappingNote = New System.Windows.Forms.TextBox()
        Me.lblMappingNote = New System.Windows.Forms.Label()
        Me.txtMappingFVName = New System.Windows.Forms.TextBox()
        Me.cboMappingCollectionName = New System.Windows.Forms.ComboBox()
        Me.txtMappingChannelName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dgvChannelsMapping = New System.Windows.Forms.DataGridView()
        Me.CollectionNameDataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChannelNameDataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FVNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnavailableDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.NoteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblEngineModel = New System.Windows.Forms.Label()
        Me.cboEngineModel = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.BooleanChannelsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BooleanStateBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericChannelsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EngineeringUnitBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextChannelsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HMIChannelsMappingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.dgvBooleanChannels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.grpNumericChannelInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpNumericChannelInformation.SuspendLayout()
        CType(Me.grpNumericThresholdInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpNumericThresholdInformation.SuspendLayout()
        CType(Me.ColorPickEdit5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ColorPickEdit4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ColorPickEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ColorPickEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ColorPickEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvNumericChannels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage3.SuspendLayout()
        CType(Me.grpTextChannels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTextChannels.SuspendLayout()
        CType(Me.dgvTextChannels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage4.SuspendLayout()
        CType(Me.grpMappingInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMappingInformation.SuspendLayout()
        CType(Me.dgvChannelsMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BooleanChannelsBindingSource
        '
        Me.BooleanChannelsBindingSource.AllowNew = False
        Me.BooleanChannelsBindingSource.DataSource = Me.m_HmiBooleanChannelsCollection
        '
        'm_HmiBooleanChannelsCollection
        '
        Me.m_HmiBooleanChannelsCollection.AllowDelete = True
        Me.m_HmiBooleanChannelsCollection.AllowEdit = True
        Me.m_HmiBooleanChannelsCollection.AllowNew = True
        Me.m_HmiBooleanChannelsCollection.EnableHierarchicalBinding = True
        Me.m_HmiBooleanChannelsCollection.Filter = Nothing
        '
        'm_UutDynamicChannelCollectionCollection
        '
        Me.m_UutDynamicChannelCollectionCollection.AllowDelete = True
        Me.m_UutDynamicChannelCollectionCollection.AllowEdit = True
        Me.m_UutDynamicChannelCollectionCollection.AllowNew = True
        Me.m_UutDynamicChannelCollectionCollection.EnableHierarchicalBinding = True
        Me.m_UutDynamicChannelCollectionCollection.Filter = Nothing
        '
        'BooleanStateBindingSource
        '
        Me.BooleanStateBindingSource.AllowNew = False
        Me.BooleanStateBindingSource.DataSource = Me.m_BooleanStateCollection
        '
        'm_BooleanStateCollection
        '
        Me.m_BooleanStateCollection.AllowDelete = True
        Me.m_BooleanStateCollection.AllowEdit = True
        Me.m_BooleanStateCollection.AllowNew = True
        Me.m_BooleanStateCollection.EnableHierarchicalBinding = True
        Me.m_BooleanStateCollection.Filter = Nothing
        '
        'NumericChannelsBindingSource
        '
        Me.NumericChannelsBindingSource.AllowNew = False
        Me.NumericChannelsBindingSource.DataSource = Me.m_HmiNumericChannelsCollection
        '
        'm_HmiNumericChannelsCollection
        '
        Me.m_HmiNumericChannelsCollection.AllowDelete = True
        Me.m_HmiNumericChannelsCollection.AllowEdit = True
        Me.m_HmiNumericChannelsCollection.AllowNew = True
        Me.m_HmiNumericChannelsCollection.EnableHierarchicalBinding = True
        Me.m_HmiNumericChannelsCollection.Filter = Nothing
        '
        'EngineeringUnitBindingSource
        '
        Me.EngineeringUnitBindingSource.DataSource = Me.m_EngineeringUnitCollection
        '
        'm_EngineeringUnitCollection
        '
        Me.m_EngineeringUnitCollection.AllowDelete = True
        Me.m_EngineeringUnitCollection.AllowEdit = True
        Me.m_EngineeringUnitCollection.AllowNew = True
        Me.m_EngineeringUnitCollection.EnableHierarchicalBinding = True
        Me.m_EngineeringUnitCollection.Filter = Nothing
        '
        'm_UutCollection
        '
        Me.m_UutCollection.AllowDelete = True
        Me.m_UutCollection.AllowEdit = True
        Me.m_UutCollection.AllowNew = True
        Me.m_UutCollection.EnableHierarchicalBinding = True
        Me.m_UutCollection.Filter = Nothing
        '
        'TextChannelsBindingSource
        '
        Me.TextChannelsBindingSource.AllowNew = False
        Me.TextChannelsBindingSource.DataSource = Me.m_HmiTextChannelsCollection
        '
        'm_HmiTextChannelsCollection
        '
        Me.m_HmiTextChannelsCollection.AllowDelete = True
        Me.m_HmiTextChannelsCollection.AllowEdit = True
        Me.m_HmiTextChannelsCollection.AllowNew = True
        Me.m_HmiTextChannelsCollection.EnableHierarchicalBinding = True
        Me.m_HmiTextChannelsCollection.Filter = Nothing
        '
        'StyleController1
        '
        Me.StyleController1.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.StyleController1.LookAndFeel.SkinName = "Office 2013"
        Me.StyleController1.LookAndFeel.UseDefaultLookAndFeel = False
        '
        'HMIChannelsMappingBindingSource
        '
        Me.HMIChannelsMappingBindingSource.DataSource = Me.m_HmiChannelsMappingCollection
        '
        'm_HmiChannelsMappingCollection
        '
        Me.m_HmiChannelsMappingCollection.AllowDelete = True
        Me.m_HmiChannelsMappingCollection.AllowEdit = True
        Me.m_HmiChannelsMappingCollection.AllowNew = True
        Me.m_HmiChannelsMappingCollection.EnableHierarchicalBinding = True
        Me.m_HmiChannelsMappingCollection.Filter = Nothing
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
        Me.GroupControl2.Controls.Add(Me.XtraTabControl1)
        Me.GroupControl2.Controls.Add(Me.btnClose)
        Me.GroupControl2.Controls.Add(Me.btnCancel)
        Me.GroupControl2.Controls.Add(Me.btnSave)
        Me.GroupControl2.Controls.Add(Me.Label1)
        Me.GroupControl2.Controls.Add(Me.lblEngineModel)
        Me.GroupControl2.Controls.Add(Me.cboEngineModel)
        Me.GroupControl2.Controls.Add(Me.Label2)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(976, 887)
        Me.GroupControl2.TabIndex = 113
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(17, 20)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.PictureEdit1.Size = New System.Drawing.Size(119, 100)
        Me.PictureEdit1.TabIndex = 119
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XtraTabControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.XtraTabControl1.Appearance.Options.UseBackColor = True
        Me.XtraTabControl1.Location = New System.Drawing.Point(17, 126)
        Me.XtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.XtraTabControl1.LookAndFeel.UseWindowsXPTheme = True
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(943, 721)
        Me.XtraTabControl1.TabIndex = 118
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2, Me.XtraTabPage3, Me.XtraTabPage4})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.XtraTabPage1.Appearance.PageClient.Options.UseBackColor = True
        Me.XtraTabPage1.Controls.Add(Me.navBoolean)
        Me.XtraTabPage1.Controls.Add(Me.dgvBooleanChannels)
        Me.XtraTabPage1.Controls.Add(Me.GroupControl3)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(935, 693)
        Me.XtraTabPage1.Text = "Boolean Channels"
        '
        'navBoolean
        '
        Me.navBoolean.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.navBoolean.DataSource = Me.BooleanChannelsBindingSource
        Me.navBoolean.Location = New System.Drawing.Point(3, 665)
        Me.navBoolean.Name = "navBoolean"
        Me.navBoolean.Size = New System.Drawing.Size(373, 24)
        Me.navBoolean.StyleController = Me.StyleController1
        Me.navBoolean.TabIndex = 79
        Me.navBoolean.Text = "DataNavigator1"
        '
        'dgvBooleanChannels
        '
        Me.dgvBooleanChannels.AllowUserToAddRows = False
        Me.dgvBooleanChannels.AllowUserToDeleteRows = False
        Me.dgvBooleanChannels.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvBooleanChannels.AutoGenerateColumns = False
        Me.dgvBooleanChannels.BackgroundColor = System.Drawing.Color.Black
        Me.dgvBooleanChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBooleanChannels.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CollectionNameDataGridViewTextBoxColumn, Me.ChannelNameDataGridViewTextBoxColumn, Me.DescriptionDataGridViewTextBoxColumn, Me.SourceDataGridViewTextBoxColumn, Me.DestinationDataGridViewTextBoxColumn, Me.ResetLatchDataGridViewTextBoxColumn, Me.ExpressionDataGridViewTextBoxColumn, Me.OPCGroupDataGridViewTextBoxColumn, Me.OPCItemIDDataGridViewTextBoxColumn, Me.TooltipDataGridViewTextBoxColumn, Me.FalseTextDataGridViewTextBoxColumn, Me.TrueTextDataGridViewTextBoxColumn, Me.FalseMessageTextDataGridViewTextBoxColumn, Me.TrueMessageTextDataGridViewTextBoxColumn, Me.SoundMessageDataGridViewTextBoxColumn, Me.InitialValueDataGridViewTextBoxColumn, Me.FinalValueDataGridViewTextBoxColumn})
        Me.dgvBooleanChannels.DataSource = Me.BooleanChannelsBindingSource
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvBooleanChannels.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvBooleanChannels.Location = New System.Drawing.Point(3, 3)
        Me.dgvBooleanChannels.Name = "dgvBooleanChannels"
        Me.dgvBooleanChannels.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvBooleanChannels.Size = New System.Drawing.Size(929, 272)
        Me.dgvBooleanChannels.TabIndex = 80
        '
        'CollectionNameDataGridViewTextBoxColumn
        '
        Me.CollectionNameDataGridViewTextBoxColumn.DataPropertyName = "CollectionName"
        Me.CollectionNameDataGridViewTextBoxColumn.Frozen = True
        Me.CollectionNameDataGridViewTextBoxColumn.HeaderText = "CollectionName"
        Me.CollectionNameDataGridViewTextBoxColumn.Name = "CollectionNameDataGridViewTextBoxColumn"
        '
        'ChannelNameDataGridViewTextBoxColumn
        '
        Me.ChannelNameDataGridViewTextBoxColumn.DataPropertyName = "ChannelName"
        Me.ChannelNameDataGridViewTextBoxColumn.Frozen = True
        Me.ChannelNameDataGridViewTextBoxColumn.HeaderText = "ChannelName"
        Me.ChannelNameDataGridViewTextBoxColumn.Name = "ChannelNameDataGridViewTextBoxColumn"
        Me.ChannelNameDataGridViewTextBoxColumn.Width = 200
        '
        'DescriptionDataGridViewTextBoxColumn
        '
        Me.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        Me.DescriptionDataGridViewTextBoxColumn.Width = 200
        '
        'SourceDataGridViewTextBoxColumn
        '
        Me.SourceDataGridViewTextBoxColumn.DataPropertyName = "Source"
        Me.SourceDataGridViewTextBoxColumn.HeaderText = "Source"
        Me.SourceDataGridViewTextBoxColumn.Name = "SourceDataGridViewTextBoxColumn"
        Me.SourceDataGridViewTextBoxColumn.Width = 200
        '
        'DestinationDataGridViewTextBoxColumn
        '
        Me.DestinationDataGridViewTextBoxColumn.DataPropertyName = "Destination"
        Me.DestinationDataGridViewTextBoxColumn.HeaderText = "Destination"
        Me.DestinationDataGridViewTextBoxColumn.Name = "DestinationDataGridViewTextBoxColumn"
        Me.DestinationDataGridViewTextBoxColumn.Width = 200
        '
        'ResetLatchDataGridViewTextBoxColumn
        '
        Me.ResetLatchDataGridViewTextBoxColumn.DataPropertyName = "ResetLatch"
        Me.ResetLatchDataGridViewTextBoxColumn.HeaderText = "ResetLatch"
        Me.ResetLatchDataGridViewTextBoxColumn.Name = "ResetLatchDataGridViewTextBoxColumn"
        '
        'ExpressionDataGridViewTextBoxColumn
        '
        Me.ExpressionDataGridViewTextBoxColumn.DataPropertyName = "Expression"
        Me.ExpressionDataGridViewTextBoxColumn.HeaderText = "Expression"
        Me.ExpressionDataGridViewTextBoxColumn.Name = "ExpressionDataGridViewTextBoxColumn"
        '
        'OPCGroupDataGridViewTextBoxColumn
        '
        Me.OPCGroupDataGridViewTextBoxColumn.DataPropertyName = "OPCGroup"
        Me.OPCGroupDataGridViewTextBoxColumn.HeaderText = "OPCGroup"
        Me.OPCGroupDataGridViewTextBoxColumn.Name = "OPCGroupDataGridViewTextBoxColumn"
        '
        'OPCItemIDDataGridViewTextBoxColumn
        '
        Me.OPCItemIDDataGridViewTextBoxColumn.DataPropertyName = "OPCItemID"
        Me.OPCItemIDDataGridViewTextBoxColumn.HeaderText = "OPCItemID"
        Me.OPCItemIDDataGridViewTextBoxColumn.Name = "OPCItemIDDataGridViewTextBoxColumn"
        Me.OPCItemIDDataGridViewTextBoxColumn.Width = 200
        '
        'TooltipDataGridViewTextBoxColumn
        '
        Me.TooltipDataGridViewTextBoxColumn.DataPropertyName = "Tooltip"
        Me.TooltipDataGridViewTextBoxColumn.HeaderText = "Tooltip"
        Me.TooltipDataGridViewTextBoxColumn.Name = "TooltipDataGridViewTextBoxColumn"
        '
        'FalseTextDataGridViewTextBoxColumn
        '
        Me.FalseTextDataGridViewTextBoxColumn.DataPropertyName = "FalseText"
        Me.FalseTextDataGridViewTextBoxColumn.HeaderText = "FalseText"
        Me.FalseTextDataGridViewTextBoxColumn.Name = "FalseTextDataGridViewTextBoxColumn"
        '
        'TrueTextDataGridViewTextBoxColumn
        '
        Me.TrueTextDataGridViewTextBoxColumn.DataPropertyName = "TrueText"
        Me.TrueTextDataGridViewTextBoxColumn.HeaderText = "TrueText"
        Me.TrueTextDataGridViewTextBoxColumn.Name = "TrueTextDataGridViewTextBoxColumn"
        '
        'FalseMessageTextDataGridViewTextBoxColumn
        '
        Me.FalseMessageTextDataGridViewTextBoxColumn.DataPropertyName = "FalseMessageText"
        Me.FalseMessageTextDataGridViewTextBoxColumn.HeaderText = "FalseMessageText"
        Me.FalseMessageTextDataGridViewTextBoxColumn.Name = "FalseMessageTextDataGridViewTextBoxColumn"
        '
        'TrueMessageTextDataGridViewTextBoxColumn
        '
        Me.TrueMessageTextDataGridViewTextBoxColumn.DataPropertyName = "TrueMessageText"
        Me.TrueMessageTextDataGridViewTextBoxColumn.HeaderText = "TrueMessageText"
        Me.TrueMessageTextDataGridViewTextBoxColumn.Name = "TrueMessageTextDataGridViewTextBoxColumn"
        '
        'SoundMessageDataGridViewTextBoxColumn
        '
        Me.SoundMessageDataGridViewTextBoxColumn.DataPropertyName = "SoundMessage"
        Me.SoundMessageDataGridViewTextBoxColumn.HeaderText = "SoundMessage"
        Me.SoundMessageDataGridViewTextBoxColumn.Name = "SoundMessageDataGridViewTextBoxColumn"
        '
        'InitialValueDataGridViewTextBoxColumn
        '
        Me.InitialValueDataGridViewTextBoxColumn.DataPropertyName = "InitialValue"
        Me.InitialValueDataGridViewTextBoxColumn.DataSource = Me.BooleanStateBindingSource
        Me.InitialValueDataGridViewTextBoxColumn.DisplayMember = "Description"
        Me.InitialValueDataGridViewTextBoxColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.InitialValueDataGridViewTextBoxColumn.HeaderText = "InitialValue"
        Me.InitialValueDataGridViewTextBoxColumn.Name = "InitialValueDataGridViewTextBoxColumn"
        Me.InitialValueDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.InitialValueDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.InitialValueDataGridViewTextBoxColumn.ValueMember = "Id"
        '
        'FinalValueDataGridViewTextBoxColumn
        '
        Me.FinalValueDataGridViewTextBoxColumn.DataPropertyName = "FinalValue"
        Me.FinalValueDataGridViewTextBoxColumn.DataSource = Me.BooleanStateBindingSource
        Me.FinalValueDataGridViewTextBoxColumn.DisplayMember = "Description"
        Me.FinalValueDataGridViewTextBoxColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.FinalValueDataGridViewTextBoxColumn.HeaderText = "FinalValue"
        Me.FinalValueDataGridViewTextBoxColumn.Name = "FinalValueDataGridViewTextBoxColumn"
        Me.FinalValueDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FinalValueDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.FinalValueDataGridViewTextBoxColumn.ValueMember = "Id"
        '
        'GroupControl3
        '
        Me.GroupControl3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GroupControl3.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.GroupControl3.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.GroupControl3.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl3.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl3.Appearance.ForeColor = System.Drawing.Color.White
        Me.GroupControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.GroupControl3.Appearance.Options.UseBackColor = True
        Me.GroupControl3.Appearance.Options.UseBorderColor = True
        Me.GroupControl3.Appearance.Options.UseFont = True
        Me.GroupControl3.Appearance.Options.UseForeColor = True
        Me.GroupControl3.AppearanceCaption.BorderColor = System.Drawing.Color.Blue
        Me.GroupControl3.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl3.AppearanceCaption.ForeColor = System.Drawing.Color.Silver
        Me.GroupControl3.AppearanceCaption.Options.UseBorderColor = True
        Me.GroupControl3.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3.AppearanceCaption.Options.UseForeColor = True
        Me.GroupControl3.Controls.Add(Me.txtBooleanOpcItemId)
        Me.GroupControl3.Controls.Add(Me.lblBooleanOpcItemId)
        Me.GroupControl3.Controls.Add(Me.txtBooleanOpcGroup)
        Me.GroupControl3.Controls.Add(Me.lblBooleanOpcGroup)
        Me.GroupControl3.Controls.Add(Me.cboBooleanFinalValue)
        Me.GroupControl3.Controls.Add(Me.cboBooleanInitialValue)
        Me.GroupControl3.Controls.Add(Me.Label4)
        Me.GroupControl3.Controls.Add(Me.Label5)
        Me.GroupControl3.Controls.Add(Me.txtBooleanChannelDestination)
        Me.GroupControl3.Controls.Add(Me.txtBooleanChannelSource)
        Me.GroupControl3.Controls.Add(Me.txtBooleanTooltip)
        Me.GroupControl3.Controls.Add(Me.txtBooleanDescription)
        Me.GroupControl3.Controls.Add(Me.cboCollectionNameBoolean)
        Me.GroupControl3.Controls.Add(Me.txtBooleanExpression)
        Me.GroupControl3.Controls.Add(Me.txtBooleanTrueText)
        Me.GroupControl3.Controls.Add(Me.txtBooleanFalseText)
        Me.GroupControl3.Controls.Add(Me.txtBooleanChannelName)
        Me.GroupControl3.Controls.Add(Me.lblBooleanChannelDestination)
        Me.GroupControl3.Controls.Add(Me.lblBooleanTrueText)
        Me.GroupControl3.Controls.Add(Me.lblBooleanFalseText)
        Me.GroupControl3.Controls.Add(Me.lblBooleanTooltip)
        Me.GroupControl3.Controls.Add(Me.lblBooleanDescription)
        Me.GroupControl3.Controls.Add(Me.lblBooleanCollectionName)
        Me.GroupControl3.Controls.Add(Me.lblBooleanChannelName)
        Me.GroupControl3.Controls.Add(Me.lblBooleanExpression)
        Me.GroupControl3.Controls.Add(Me.lblBooleanChannelSource)
        Me.GroupControl3.Location = New System.Drawing.Point(3, 281)
        Me.GroupControl3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.GroupControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(929, 378)
        Me.GroupControl3.TabIndex = 81
        Me.GroupControl3.Text = "Boolean Channel Information"
        '
        'txtBooleanOpcItemId
        '
        Me.txtBooleanOpcItemId.BackColor = System.Drawing.Color.LightGray
        Me.txtBooleanOpcItemId.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "OPCItemID", True))
        Me.txtBooleanOpcItemId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooleanOpcItemId.Location = New System.Drawing.Point(308, 225)
        Me.txtBooleanOpcItemId.Name = "txtBooleanOpcItemId"
        Me.txtBooleanOpcItemId.Size = New System.Drawing.Size(608, 22)
        Me.txtBooleanOpcItemId.TabIndex = 83
        '
        'lblBooleanOpcItemId
        '
        Me.lblBooleanOpcItemId.AutoSize = True
        Me.lblBooleanOpcItemId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanOpcItemId.Location = New System.Drawing.Point(305, 205)
        Me.lblBooleanOpcItemId.Name = "lblBooleanOpcItemId"
        Me.lblBooleanOpcItemId.Size = New System.Drawing.Size(75, 16)
        Me.lblBooleanOpcItemId.TabIndex = 82
        Me.lblBooleanOpcItemId.Text = "OPC ItemId"
        '
        'txtBooleanOpcGroup
        '
        Me.txtBooleanOpcGroup.BackColor = System.Drawing.Color.LightGray
        Me.txtBooleanOpcGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "OPCGroup", True))
        Me.txtBooleanOpcGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooleanOpcGroup.Location = New System.Drawing.Point(12, 225)
        Me.txtBooleanOpcGroup.Name = "txtBooleanOpcGroup"
        Me.txtBooleanOpcGroup.Size = New System.Drawing.Size(290, 22)
        Me.txtBooleanOpcGroup.TabIndex = 81
        '
        'lblBooleanOpcGroup
        '
        Me.lblBooleanOpcGroup.AutoSize = True
        Me.lblBooleanOpcGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanOpcGroup.Location = New System.Drawing.Point(9, 205)
        Me.lblBooleanOpcGroup.Name = "lblBooleanOpcGroup"
        Me.lblBooleanOpcGroup.Size = New System.Drawing.Size(76, 16)
        Me.lblBooleanOpcGroup.TabIndex = 80
        Me.lblBooleanOpcGroup.Text = "OPC Group"
        '
        'cboBooleanFinalValue
        '
        Me.cboBooleanFinalValue.BackColor = System.Drawing.Color.LightGray
        Me.cboBooleanFinalValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "FinalValue", True))
        Me.cboBooleanFinalValue.DataSource = Me.m_BooleanStateCollection2
        Me.cboBooleanFinalValue.DisplayMember = "Description"
        Me.cboBooleanFinalValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBooleanFinalValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBooleanFinalValue.FormattingEnabled = True
        Me.cboBooleanFinalValue.Location = New System.Drawing.Point(699, 182)
        Me.cboBooleanFinalValue.Name = "cboBooleanFinalValue"
        Me.cboBooleanFinalValue.Size = New System.Drawing.Size(217, 24)
        Me.cboBooleanFinalValue.TabIndex = 79
        Me.cboBooleanFinalValue.ValueMember = "Id"
        '
        'm_BooleanStateCollection2
        '
        Me.m_BooleanStateCollection2.AllowDelete = True
        Me.m_BooleanStateCollection2.AllowEdit = True
        Me.m_BooleanStateCollection2.AllowNew = True
        Me.m_BooleanStateCollection2.EnableHierarchicalBinding = True
        Me.m_BooleanStateCollection2.Filter = Nothing
        '
        'cboBooleanInitialValue
        '
        Me.cboBooleanInitialValue.BackColor = System.Drawing.Color.LightGray
        Me.cboBooleanInitialValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "InitialValue", True))
        Me.cboBooleanInitialValue.DataSource = Me.m_BooleanStateCollection
        Me.cboBooleanInitialValue.DisplayMember = "Description"
        Me.cboBooleanInitialValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBooleanInitialValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBooleanInitialValue.FormattingEnabled = True
        Me.cboBooleanInitialValue.Location = New System.Drawing.Point(471, 180)
        Me.cboBooleanInitialValue.Name = "cboBooleanInitialValue"
        Me.cboBooleanInitialValue.Size = New System.Drawing.Size(217, 24)
        Me.cboBooleanInitialValue.TabIndex = 78
        Me.cboBooleanInitialValue.ValueMember = "Id"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(696, 163)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 16)
        Me.Label4.TabIndex = 77
        Me.Label4.Text = "Final Value"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(468, 163)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 16)
        Me.Label5.TabIndex = 76
        Me.Label5.Text = "Initial Value"
        '
        'txtBooleanChannelDestination
        '
        Me.txtBooleanChannelDestination.BackColor = System.Drawing.Color.LightGray
        Me.txtBooleanChannelDestination.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "Destination", True))
        Me.txtBooleanChannelDestination.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooleanChannelDestination.Location = New System.Drawing.Point(12, 180)
        Me.txtBooleanChannelDestination.Name = "txtBooleanChannelDestination"
        Me.txtBooleanChannelDestination.Size = New System.Drawing.Size(445, 22)
        Me.txtBooleanChannelDestination.TabIndex = 73
        '
        'txtBooleanChannelSource
        '
        Me.txtBooleanChannelSource.BackColor = System.Drawing.Color.LightGray
        Me.txtBooleanChannelSource.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "Source", True))
        Me.txtBooleanChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooleanChannelSource.Location = New System.Drawing.Point(12, 135)
        Me.txtBooleanChannelSource.Name = "txtBooleanChannelSource"
        Me.txtBooleanChannelSource.Size = New System.Drawing.Size(445, 22)
        Me.txtBooleanChannelSource.TabIndex = 72
        '
        'txtBooleanTooltip
        '
        Me.txtBooleanTooltip.BackColor = System.Drawing.Color.LightGray
        Me.txtBooleanTooltip.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "Tooltip", True))
        Me.txtBooleanTooltip.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooleanTooltip.Location = New System.Drawing.Point(471, 91)
        Me.txtBooleanTooltip.Name = "txtBooleanTooltip"
        Me.txtBooleanTooltip.Size = New System.Drawing.Size(445, 22)
        Me.txtBooleanTooltip.TabIndex = 66
        '
        'txtBooleanDescription
        '
        Me.txtBooleanDescription.BackColor = System.Drawing.Color.LightGray
        Me.txtBooleanDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "Description", True))
        Me.txtBooleanDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooleanDescription.Location = New System.Drawing.Point(12, 91)
        Me.txtBooleanDescription.Name = "txtBooleanDescription"
        Me.txtBooleanDescription.Size = New System.Drawing.Size(445, 22)
        Me.txtBooleanDescription.TabIndex = 64
        '
        'cboCollectionNameBoolean
        '
        Me.cboCollectionNameBoolean.BackColor = System.Drawing.Color.LightGray
        Me.cboCollectionNameBoolean.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "CollectionName", True))
        Me.cboCollectionNameBoolean.DataSource = Me.m_UutDynamicChannelCollectionCollection
        Me.cboCollectionNameBoolean.DisplayMember = "CollectionName"
        Me.cboCollectionNameBoolean.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCollectionNameBoolean.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCollectionNameBoolean.FormattingEnabled = True
        Me.cboCollectionNameBoolean.Location = New System.Drawing.Point(640, 46)
        Me.cboCollectionNameBoolean.Name = "cboCollectionNameBoolean"
        Me.cboCollectionNameBoolean.Size = New System.Drawing.Size(276, 24)
        Me.cboCollectionNameBoolean.TabIndex = 62
        Me.cboCollectionNameBoolean.ValueMember = "Id"
        '
        'txtBooleanExpression
        '
        Me.txtBooleanExpression.BackColor = System.Drawing.Color.LightGray
        Me.txtBooleanExpression.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "Expression", True))
        Me.txtBooleanExpression.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooleanExpression.Location = New System.Drawing.Point(12, 269)
        Me.txtBooleanExpression.Multiline = True
        Me.txtBooleanExpression.Name = "txtBooleanExpression"
        Me.txtBooleanExpression.Size = New System.Drawing.Size(904, 98)
        Me.txtBooleanExpression.TabIndex = 59
        '
        'txtBooleanTrueText
        '
        Me.txtBooleanTrueText.BackColor = System.Drawing.Color.LightGray
        Me.txtBooleanTrueText.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "TrueText", True))
        Me.txtBooleanTrueText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooleanTrueText.Location = New System.Drawing.Point(699, 133)
        Me.txtBooleanTrueText.Name = "txtBooleanTrueText"
        Me.txtBooleanTrueText.Size = New System.Drawing.Size(217, 22)
        Me.txtBooleanTrueText.TabIndex = 58
        '
        'txtBooleanFalseText
        '
        Me.txtBooleanFalseText.BackColor = System.Drawing.Color.LightGray
        Me.txtBooleanFalseText.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "FalseText", True))
        Me.txtBooleanFalseText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooleanFalseText.Location = New System.Drawing.Point(471, 133)
        Me.txtBooleanFalseText.Name = "txtBooleanFalseText"
        Me.txtBooleanFalseText.Size = New System.Drawing.Size(217, 22)
        Me.txtBooleanFalseText.TabIndex = 57
        '
        'txtBooleanChannelName
        '
        Me.txtBooleanChannelName.BackColor = System.Drawing.Color.LightGray
        Me.txtBooleanChannelName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BooleanChannelsBindingSource, "ChannelName", True))
        Me.txtBooleanChannelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooleanChannelName.Location = New System.Drawing.Point(12, 46)
        Me.txtBooleanChannelName.Name = "txtBooleanChannelName"
        Me.txtBooleanChannelName.Size = New System.Drawing.Size(613, 22)
        Me.txtBooleanChannelName.TabIndex = 56
        '
        'lblBooleanChannelDestination
        '
        Me.lblBooleanChannelDestination.AutoSize = True
        Me.lblBooleanChannelDestination.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanChannelDestination.Location = New System.Drawing.Point(9, 160)
        Me.lblBooleanChannelDestination.Name = "lblBooleanChannelDestination"
        Me.lblBooleanChannelDestination.Size = New System.Drawing.Size(127, 16)
        Me.lblBooleanChannelDestination.TabIndex = 71
        Me.lblBooleanChannelDestination.Text = "Channel Destination"
        '
        'lblBooleanTrueText
        '
        Me.lblBooleanTrueText.AutoSize = True
        Me.lblBooleanTrueText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanTrueText.Location = New System.Drawing.Point(696, 116)
        Me.lblBooleanTrueText.Name = "lblBooleanTrueText"
        Me.lblBooleanTrueText.Size = New System.Drawing.Size(65, 16)
        Me.lblBooleanTrueText.TabIndex = 69
        Me.lblBooleanTrueText.Text = "True Text"
        '
        'lblBooleanFalseText
        '
        Me.lblBooleanFalseText.AutoSize = True
        Me.lblBooleanFalseText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanFalseText.Location = New System.Drawing.Point(468, 116)
        Me.lblBooleanFalseText.Name = "lblBooleanFalseText"
        Me.lblBooleanFalseText.Size = New System.Drawing.Size(71, 16)
        Me.lblBooleanFalseText.TabIndex = 68
        Me.lblBooleanFalseText.Text = "False Text"
        '
        'lblBooleanTooltip
        '
        Me.lblBooleanTooltip.AutoSize = True
        Me.lblBooleanTooltip.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanTooltip.Location = New System.Drawing.Point(468, 72)
        Me.lblBooleanTooltip.Name = "lblBooleanTooltip"
        Me.lblBooleanTooltip.Size = New System.Drawing.Size(79, 16)
        Me.lblBooleanTooltip.TabIndex = 67
        Me.lblBooleanTooltip.Text = "Tooltip Text"
        '
        'lblBooleanDescription
        '
        Me.lblBooleanDescription.AutoSize = True
        Me.lblBooleanDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanDescription.Location = New System.Drawing.Point(9, 72)
        Me.lblBooleanDescription.Name = "lblBooleanDescription"
        Me.lblBooleanDescription.Size = New System.Drawing.Size(76, 16)
        Me.lblBooleanDescription.TabIndex = 65
        Me.lblBooleanDescription.Text = "Description"
        '
        'lblBooleanCollectionName
        '
        Me.lblBooleanCollectionName.AutoSize = True
        Me.lblBooleanCollectionName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanCollectionName.Location = New System.Drawing.Point(637, 26)
        Me.lblBooleanCollectionName.Name = "lblBooleanCollectionName"
        Me.lblBooleanCollectionName.Size = New System.Drawing.Size(107, 16)
        Me.lblBooleanCollectionName.TabIndex = 63
        Me.lblBooleanCollectionName.Text = "Collection Name"
        '
        'lblBooleanChannelName
        '
        Me.lblBooleanChannelName.AutoSize = True
        Me.lblBooleanChannelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanChannelName.Location = New System.Drawing.Point(9, 26)
        Me.lblBooleanChannelName.Name = "lblBooleanChannelName"
        Me.lblBooleanChannelName.Size = New System.Drawing.Size(97, 16)
        Me.lblBooleanChannelName.TabIndex = 61
        Me.lblBooleanChannelName.Text = "Channel Name"
        '
        'lblBooleanExpression
        '
        Me.lblBooleanExpression.AutoSize = True
        Me.lblBooleanExpression.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanExpression.Location = New System.Drawing.Point(9, 250)
        Me.lblBooleanExpression.Name = "lblBooleanExpression"
        Me.lblBooleanExpression.Size = New System.Drawing.Size(134, 16)
        Me.lblBooleanExpression.TabIndex = 60
        Me.lblBooleanExpression.Text = "Formula / Expression"
        '
        'lblBooleanChannelSource
        '
        Me.lblBooleanChannelSource.AutoSize = True
        Me.lblBooleanChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooleanChannelSource.Location = New System.Drawing.Point(9, 116)
        Me.lblBooleanChannelSource.Name = "lblBooleanChannelSource"
        Me.lblBooleanChannelSource.Size = New System.Drawing.Size(103, 16)
        Me.lblBooleanChannelSource.TabIndex = 70
        Me.lblBooleanChannelSource.Text = "Channel Source"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.XtraTabPage2.Appearance.PageClient.Options.UseBackColor = True
        Me.XtraTabPage2.Controls.Add(Me.grpNumericChannelInformation)
        Me.XtraTabPage2.Controls.Add(Me.grpNumericThresholdInformation)
        Me.XtraTabPage2.Controls.Add(Me.dgvNumericChannels)
        Me.XtraTabPage2.Controls.Add(Me.navNumeric)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(935, 693)
        Me.XtraTabPage2.Text = "Numeric Channels"
        '
        'grpNumericChannelInformation
        '
        Me.grpNumericChannelInformation.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.grpNumericChannelInformation.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.grpNumericChannelInformation.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.grpNumericChannelInformation.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.grpNumericChannelInformation.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpNumericChannelInformation.Appearance.ForeColor = System.Drawing.Color.White
        Me.grpNumericChannelInformation.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grpNumericChannelInformation.Appearance.Options.UseBackColor = True
        Me.grpNumericChannelInformation.Appearance.Options.UseBorderColor = True
        Me.grpNumericChannelInformation.Appearance.Options.UseFont = True
        Me.grpNumericChannelInformation.Appearance.Options.UseForeColor = True
        Me.grpNumericChannelInformation.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpNumericChannelInformation.AppearanceCaption.ForeColor = System.Drawing.Color.Silver
        Me.grpNumericChannelInformation.AppearanceCaption.Options.UseFont = True
        Me.grpNumericChannelInformation.AppearanceCaption.Options.UseForeColor = True
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericOpcItemId)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericOpcItemId)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericOpcGroup)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericOpcGroup)
        Me.grpNumericChannelInformation.Controls.Add(Me.btnViewThresholdSection)
        Me.grpNumericChannelInformation.Controls.Add(Me.chkNumericPersistent)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericFinalValue)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericInitialValue)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericFinalValue)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericInitialValue)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericEngUnit)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericMaximum)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericNbDecimal)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericChannelDestination)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericChannelSource)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericTooltip)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericDescription)
        Me.grpNumericChannelInformation.Controls.Add(Me.cboCollectionNameNumeric)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericExpression)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericMinimum)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericFormat)
        Me.grpNumericChannelInformation.Controls.Add(Me.txtNumericChannelName)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericExpression)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericDescription)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericCollectionName)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericChannelName)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericEngUnit)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericMaximum)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericNbDecimal)
        Me.grpNumericChannelInformation.Controls.Add(Me.Label3)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericChannelSource)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericMinimum)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericFormat)
        Me.grpNumericChannelInformation.Controls.Add(Me.lblNumericTooltiptext)
        Me.grpNumericChannelInformation.Location = New System.Drawing.Point(0, 156)
        Me.grpNumericChannelInformation.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.grpNumericChannelInformation.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grpNumericChannelInformation.Name = "grpNumericChannelInformation"
        Me.grpNumericChannelInformation.Size = New System.Drawing.Size(935, 330)
        Me.grpNumericChannelInformation.TabIndex = 102
        Me.grpNumericChannelInformation.Text = "Numeric Channel Information"
        '
        'txtNumericOpcItemId
        '
        Me.txtNumericOpcItemId.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericOpcItemId.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericOpcItemId.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "OPCItemID", True))
        Me.txtNumericOpcItemId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericOpcItemId.Location = New System.Drawing.Point(313, 225)
        Me.txtNumericOpcItemId.Name = "txtNumericOpcItemId"
        Me.txtNumericOpcItemId.Size = New System.Drawing.Size(603, 22)
        Me.txtNumericOpcItemId.TabIndex = 120
        '
        'lblNumericOpcItemId
        '
        Me.lblNumericOpcItemId.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericOpcItemId.AutoSize = True
        Me.lblNumericOpcItemId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericOpcItemId.Location = New System.Drawing.Point(310, 205)
        Me.lblNumericOpcItemId.Name = "lblNumericOpcItemId"
        Me.lblNumericOpcItemId.Size = New System.Drawing.Size(75, 16)
        Me.lblNumericOpcItemId.TabIndex = 119
        Me.lblNumericOpcItemId.Text = "OPC ItemId"
        '
        'txtNumericOpcGroup
        '
        Me.txtNumericOpcGroup.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericOpcGroup.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericOpcGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "OPCGroup", True))
        Me.txtNumericOpcGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericOpcGroup.Location = New System.Drawing.Point(12, 225)
        Me.txtNumericOpcGroup.Name = "txtNumericOpcGroup"
        Me.txtNumericOpcGroup.Size = New System.Drawing.Size(290, 22)
        Me.txtNumericOpcGroup.TabIndex = 118
        '
        'lblNumericOpcGroup
        '
        Me.lblNumericOpcGroup.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericOpcGroup.AutoSize = True
        Me.lblNumericOpcGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericOpcGroup.Location = New System.Drawing.Point(9, 205)
        Me.lblNumericOpcGroup.Name = "lblNumericOpcGroup"
        Me.lblNumericOpcGroup.Size = New System.Drawing.Size(76, 16)
        Me.lblNumericOpcGroup.TabIndex = 117
        Me.lblNumericOpcGroup.Text = "OPC Group"
        '
        'btnViewThresholdSection
        '
        Me.btnViewThresholdSection.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnViewThresholdSection.Location = New System.Drawing.Point(774, 282)
        Me.btnViewThresholdSection.Name = "btnViewThresholdSection"
        Me.btnViewThresholdSection.Size = New System.Drawing.Size(138, 23)
        Me.btnViewThresholdSection.StyleController = Me.StyleController1
        Me.btnViewThresholdSection.TabIndex = 116
        Me.btnViewThresholdSection.Text = "View Threshold Info"
        '
        'chkNumericPersistent
        '
        Me.chkNumericPersistent.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkNumericPersistent.AutoSize = True
        Me.chkNumericPersistent.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.NumericChannelsBindingSource, "Persistent", True))
        Me.chkNumericPersistent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.chkNumericPersistent.Location = New System.Drawing.Point(767, 181)
        Me.chkNumericPersistent.Name = "chkNumericPersistent"
        Me.chkNumericPersistent.Size = New System.Drawing.Size(138, 20)
        Me.chkNumericPersistent.TabIndex = 96
        Me.chkNumericPersistent.Text = "Persistent Channel"
        Me.chkNumericPersistent.UseVisualStyleBackColor = True
        '
        'txtNumericFinalValue
        '
        Me.txtNumericFinalValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericFinalValue.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericFinalValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "FinalValue", True))
        Me.txtNumericFinalValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericFinalValue.Location = New System.Drawing.Point(616, 180)
        Me.txtNumericFinalValue.Name = "txtNumericFinalValue"
        Me.txtNumericFinalValue.Size = New System.Drawing.Size(139, 22)
        Me.txtNumericFinalValue.TabIndex = 94
        '
        'txtNumericInitialValue
        '
        Me.txtNumericInitialValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericInitialValue.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericInitialValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "InitialValue", True))
        Me.txtNumericInitialValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericInitialValue.Location = New System.Drawing.Point(471, 180)
        Me.txtNumericInitialValue.Name = "txtNumericInitialValue"
        Me.txtNumericInitialValue.Size = New System.Drawing.Size(139, 22)
        Me.txtNumericInitialValue.TabIndex = 92
        '
        'lblNumericFinalValue
        '
        Me.lblNumericFinalValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericFinalValue.AutoSize = True
        Me.lblNumericFinalValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericFinalValue.Location = New System.Drawing.Point(613, 161)
        Me.lblNumericFinalValue.Name = "lblNumericFinalValue"
        Me.lblNumericFinalValue.Size = New System.Drawing.Size(75, 16)
        Me.lblNumericFinalValue.TabIndex = 95
        Me.lblNumericFinalValue.Text = "Final Value"
        '
        'lblNumericInitialValue
        '
        Me.lblNumericInitialValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericInitialValue.AutoSize = True
        Me.lblNumericInitialValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericInitialValue.Location = New System.Drawing.Point(468, 161)
        Me.lblNumericInitialValue.Name = "lblNumericInitialValue"
        Me.lblNumericInitialValue.Size = New System.Drawing.Size(73, 16)
        Me.lblNumericInitialValue.TabIndex = 93
        Me.lblNumericInitialValue.Text = "Intial Value"
        '
        'txtNumericEngUnit
        '
        Me.txtNumericEngUnit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericEngUnit.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericEngUnit.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.NumericChannelsBindingSource, "EngUnitID", True))
        Me.txtNumericEngUnit.DataSource = Me.EngineeringUnitBindingSource
        Me.txtNumericEngUnit.DisplayMember = "DisplayName"
        Me.txtNumericEngUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericEngUnit.FormattingEnabled = True
        Me.txtNumericEngUnit.Location = New System.Drawing.Point(761, 135)
        Me.txtNumericEngUnit.Name = "txtNumericEngUnit"
        Me.txtNumericEngUnit.Size = New System.Drawing.Size(155, 24)
        Me.txtNumericEngUnit.TabIndex = 90
        Me.txtNumericEngUnit.ValueMember = "Id"
        '
        'txtNumericMaximum
        '
        Me.txtNumericMaximum.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericMaximum.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericMaximum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "Maximum", True))
        Me.txtNumericMaximum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericMaximum.Location = New System.Drawing.Point(616, 135)
        Me.txtNumericMaximum.Name = "txtNumericMaximum"
        Me.txtNumericMaximum.Size = New System.Drawing.Size(139, 22)
        Me.txtNumericMaximum.TabIndex = 88
        '
        'txtNumericNbDecimal
        '
        Me.txtNumericNbDecimal.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericNbDecimal.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericNbDecimal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "DecimalDigits", True))
        Me.txtNumericNbDecimal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericNbDecimal.Location = New System.Drawing.Point(836, 91)
        Me.txtNumericNbDecimal.Name = "txtNumericNbDecimal"
        Me.txtNumericNbDecimal.Size = New System.Drawing.Size(80, 22)
        Me.txtNumericNbDecimal.TabIndex = 86
        '
        'txtNumericChannelDestination
        '
        Me.txtNumericChannelDestination.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericChannelDestination.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericChannelDestination.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "Destination", True))
        Me.txtNumericChannelDestination.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericChannelDestination.Location = New System.Drawing.Point(12, 180)
        Me.txtNumericChannelDestination.Name = "txtNumericChannelDestination"
        Me.txtNumericChannelDestination.Size = New System.Drawing.Size(445, 22)
        Me.txtNumericChannelDestination.TabIndex = 85
        '
        'txtNumericChannelSource
        '
        Me.txtNumericChannelSource.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericChannelSource.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericChannelSource.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "Source", True))
        Me.txtNumericChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericChannelSource.Location = New System.Drawing.Point(12, 135)
        Me.txtNumericChannelSource.Name = "txtNumericChannelSource"
        Me.txtNumericChannelSource.Size = New System.Drawing.Size(445, 22)
        Me.txtNumericChannelSource.TabIndex = 84
        '
        'txtNumericTooltip
        '
        Me.txtNumericTooltip.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericTooltip.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericTooltip.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "Tooltip", True))
        Me.txtNumericTooltip.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericTooltip.Location = New System.Drawing.Point(471, 91)
        Me.txtNumericTooltip.Name = "txtNumericTooltip"
        Me.txtNumericTooltip.Size = New System.Drawing.Size(276, 22)
        Me.txtNumericTooltip.TabIndex = 78
        '
        'txtNumericDescription
        '
        Me.txtNumericDescription.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericDescription.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "Description", True))
        Me.txtNumericDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericDescription.Location = New System.Drawing.Point(12, 91)
        Me.txtNumericDescription.Name = "txtNumericDescription"
        Me.txtNumericDescription.Size = New System.Drawing.Size(445, 22)
        Me.txtNumericDescription.TabIndex = 76
        '
        'cboCollectionNameNumeric
        '
        Me.cboCollectionNameNumeric.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboCollectionNameNumeric.BackColor = System.Drawing.Color.LightGray
        Me.cboCollectionNameNumeric.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "CollectionName", True))
        Me.cboCollectionNameNumeric.DataSource = Me.m_UutCollection
        Me.cboCollectionNameNumeric.DisplayMember = "EngineModel"
        Me.cboCollectionNameNumeric.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCollectionNameNumeric.FormattingEnabled = True
        Me.cboCollectionNameNumeric.Location = New System.Drawing.Point(640, 46)
        Me.cboCollectionNameNumeric.Name = "cboCollectionNameNumeric"
        Me.cboCollectionNameNumeric.Size = New System.Drawing.Size(276, 24)
        Me.cboCollectionNameNumeric.TabIndex = 74
        Me.cboCollectionNameNumeric.ValueMember = "Id"
        '
        'txtNumericExpression
        '
        Me.txtNumericExpression.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericExpression.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericExpression.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "Expression", True))
        Me.txtNumericExpression.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericExpression.Location = New System.Drawing.Point(12, 269)
        Me.txtNumericExpression.Multiline = True
        Me.txtNumericExpression.Name = "txtNumericExpression"
        Me.txtNumericExpression.Size = New System.Drawing.Size(743, 50)
        Me.txtNumericExpression.TabIndex = 71
        '
        'txtNumericMinimum
        '
        Me.txtNumericMinimum.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericMinimum.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericMinimum.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "Minimum", True))
        Me.txtNumericMinimum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericMinimum.Location = New System.Drawing.Point(471, 135)
        Me.txtNumericMinimum.Name = "txtNumericMinimum"
        Me.txtNumericMinimum.Size = New System.Drawing.Size(139, 22)
        Me.txtNumericMinimum.TabIndex = 70
        '
        'txtNumericFormat
        '
        Me.txtNumericFormat.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericFormat.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericFormat.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "Format", True))
        Me.txtNumericFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericFormat.Location = New System.Drawing.Point(753, 91)
        Me.txtNumericFormat.Name = "txtNumericFormat"
        Me.txtNumericFormat.Size = New System.Drawing.Size(77, 22)
        Me.txtNumericFormat.TabIndex = 69
        '
        'txtNumericChannelName
        '
        Me.txtNumericChannelName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericChannelName.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericChannelName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ChannelName", True))
        Me.txtNumericChannelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericChannelName.Location = New System.Drawing.Point(12, 46)
        Me.txtNumericChannelName.Name = "txtNumericChannelName"
        Me.txtNumericChannelName.Size = New System.Drawing.Size(613, 22)
        Me.txtNumericChannelName.TabIndex = 68
        '
        'lblNumericExpression
        '
        Me.lblNumericExpression.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericExpression.AutoSize = True
        Me.lblNumericExpression.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericExpression.Location = New System.Drawing.Point(9, 250)
        Me.lblNumericExpression.Name = "lblNumericExpression"
        Me.lblNumericExpression.Size = New System.Drawing.Size(134, 16)
        Me.lblNumericExpression.TabIndex = 72
        Me.lblNumericExpression.Text = "Formula / Expression"
        '
        'lblNumericDescription
        '
        Me.lblNumericDescription.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericDescription.AutoSize = True
        Me.lblNumericDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericDescription.Location = New System.Drawing.Point(9, 72)
        Me.lblNumericDescription.Name = "lblNumericDescription"
        Me.lblNumericDescription.Size = New System.Drawing.Size(76, 16)
        Me.lblNumericDescription.TabIndex = 77
        Me.lblNumericDescription.Text = "Description"
        '
        'lblNumericCollectionName
        '
        Me.lblNumericCollectionName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericCollectionName.AutoSize = True
        Me.lblNumericCollectionName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericCollectionName.Location = New System.Drawing.Point(637, 26)
        Me.lblNumericCollectionName.Name = "lblNumericCollectionName"
        Me.lblNumericCollectionName.Size = New System.Drawing.Size(107, 16)
        Me.lblNumericCollectionName.TabIndex = 75
        Me.lblNumericCollectionName.Text = "Collection Name"
        '
        'lblNumericChannelName
        '
        Me.lblNumericChannelName.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericChannelName.AutoSize = True
        Me.lblNumericChannelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericChannelName.Location = New System.Drawing.Point(9, 26)
        Me.lblNumericChannelName.Name = "lblNumericChannelName"
        Me.lblNumericChannelName.Size = New System.Drawing.Size(97, 16)
        Me.lblNumericChannelName.TabIndex = 73
        Me.lblNumericChannelName.Text = "Channel Name"
        '
        'lblNumericEngUnit
        '
        Me.lblNumericEngUnit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericEngUnit.AutoSize = True
        Me.lblNumericEngUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericEngUnit.Location = New System.Drawing.Point(759, 116)
        Me.lblNumericEngUnit.Name = "lblNumericEngUnit"
        Me.lblNumericEngUnit.Size = New System.Drawing.Size(61, 16)
        Me.lblNumericEngUnit.TabIndex = 91
        Me.lblNumericEngUnit.Text = "Eng. Unit"
        '
        'lblNumericMaximum
        '
        Me.lblNumericMaximum.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericMaximum.AutoSize = True
        Me.lblNumericMaximum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericMaximum.Location = New System.Drawing.Point(613, 116)
        Me.lblNumericMaximum.Name = "lblNumericMaximum"
        Me.lblNumericMaximum.Size = New System.Drawing.Size(65, 16)
        Me.lblNumericMaximum.TabIndex = 89
        Me.lblNumericMaximum.Text = "Maximum"
        '
        'lblNumericNbDecimal
        '
        Me.lblNumericNbDecimal.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericNbDecimal.AutoSize = True
        Me.lblNumericNbDecimal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericNbDecimal.Location = New System.Drawing.Point(833, 72)
        Me.lblNumericNbDecimal.Name = "lblNumericNbDecimal"
        Me.lblNumericNbDecimal.Size = New System.Drawing.Size(79, 16)
        Me.lblNumericNbDecimal.TabIndex = 87
        Me.lblNumericNbDecimal.Text = "Nb Decimal"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(127, 16)
        Me.Label3.TabIndex = 83
        Me.Label3.Text = "Channel Destination"
        '
        'lblNumericChannelSource
        '
        Me.lblNumericChannelSource.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericChannelSource.AutoSize = True
        Me.lblNumericChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericChannelSource.Location = New System.Drawing.Point(9, 116)
        Me.lblNumericChannelSource.Name = "lblNumericChannelSource"
        Me.lblNumericChannelSource.Size = New System.Drawing.Size(103, 16)
        Me.lblNumericChannelSource.TabIndex = 82
        Me.lblNumericChannelSource.Text = "Channel Source"
        '
        'lblNumericMinimum
        '
        Me.lblNumericMinimum.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericMinimum.AutoSize = True
        Me.lblNumericMinimum.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericMinimum.Location = New System.Drawing.Point(468, 116)
        Me.lblNumericMinimum.Name = "lblNumericMinimum"
        Me.lblNumericMinimum.Size = New System.Drawing.Size(61, 16)
        Me.lblNumericMinimum.TabIndex = 81
        Me.lblNumericMinimum.Text = "Minimum"
        '
        'lblNumericFormat
        '
        Me.lblNumericFormat.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericFormat.AutoSize = True
        Me.lblNumericFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericFormat.Location = New System.Drawing.Point(750, 72)
        Me.lblNumericFormat.Name = "lblNumericFormat"
        Me.lblNumericFormat.Size = New System.Drawing.Size(50, 16)
        Me.lblNumericFormat.TabIndex = 80
        Me.lblNumericFormat.Text = "Format"
        '
        'lblNumericTooltiptext
        '
        Me.lblNumericTooltiptext.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericTooltiptext.AutoSize = True
        Me.lblNumericTooltiptext.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericTooltiptext.Location = New System.Drawing.Point(468, 72)
        Me.lblNumericTooltiptext.Name = "lblNumericTooltiptext"
        Me.lblNumericTooltiptext.Size = New System.Drawing.Size(79, 16)
        Me.lblNumericTooltiptext.TabIndex = 79
        Me.lblNumericTooltiptext.Text = "Tooltip Text"
        '
        'grpNumericThresholdInformation
        '
        Me.grpNumericThresholdInformation.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.grpNumericThresholdInformation.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.grpNumericThresholdInformation.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.grpNumericThresholdInformation.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.grpNumericThresholdInformation.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpNumericThresholdInformation.Appearance.ForeColor = System.Drawing.Color.White
        Me.grpNumericThresholdInformation.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grpNumericThresholdInformation.Appearance.Options.UseBackColor = True
        Me.grpNumericThresholdInformation.Appearance.Options.UseBorderColor = True
        Me.grpNumericThresholdInformation.Appearance.Options.UseFont = True
        Me.grpNumericThresholdInformation.Appearance.Options.UseForeColor = True
        Me.grpNumericThresholdInformation.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpNumericThresholdInformation.AppearanceCaption.ForeColor = System.Drawing.Color.Silver
        Me.grpNumericThresholdInformation.AppearanceCaption.Options.UseFont = True
        Me.grpNumericThresholdInformation.AppearanceCaption.Options.UseForeColor = True
        Me.grpNumericThresholdInformation.Controls.Add(Me.chkNumericLowTriggerEnabled)
        Me.grpNumericThresholdInformation.Controls.Add(Me.chkNumericHighTriggerEnabled)
        Me.grpNumericThresholdInformation.Controls.Add(Me.lblNumericLowTrigger)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtNumericLowTriggerValue)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtNumericHighTriggerValue)
        Me.grpNumericThresholdInformation.Controls.Add(Me.lblNumericTrigger)
        Me.grpNumericThresholdInformation.Controls.Add(Me.ColorPickEdit5)
        Me.grpNumericThresholdInformation.Controls.Add(Me.ColorPickEdit4)
        Me.grpNumericThresholdInformation.Controls.Add(Me.ColorPickEdit3)
        Me.grpNumericThresholdInformation.Controls.Add(Me.ColorPickEdit2)
        Me.grpNumericThresholdInformation.Controls.Add(Me.ColorPickEdit1)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtLowLowMessage)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtLowMessage)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtHighMessage)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtHighHighMessage)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtLowLowColor)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtLowColor)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtNormalColor)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtHighColor)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtHighHighColor)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtLowLowChannelSource)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtLowChannelSource)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtHighChannelSource)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtHighHighChannelSource)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtLowLowValue)
        Me.grpNumericThresholdInformation.Controls.Add(Me.lblLowLow)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtLowValue)
        Me.grpNumericThresholdInformation.Controls.Add(Me.lblLow)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtNormalValue)
        Me.grpNumericThresholdInformation.Controls.Add(Me.lblNormal)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtHighValue)
        Me.grpNumericThresholdInformation.Controls.Add(Me.lblHigh)
        Me.grpNumericThresholdInformation.Controls.Add(Me.txtHighHighValue)
        Me.grpNumericThresholdInformation.Controls.Add(Me.lblHighHigh)
        Me.grpNumericThresholdInformation.Controls.Add(Me.Label14)
        Me.grpNumericThresholdInformation.Controls.Add(Me.lblColor)
        Me.grpNumericThresholdInformation.Controls.Add(Me.lblChannelSource)
        Me.grpNumericThresholdInformation.Controls.Add(Me.lblValue)
        Me.grpNumericThresholdInformation.Location = New System.Drawing.Point(0, 489)
        Me.grpNumericThresholdInformation.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.grpNumericThresholdInformation.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grpNumericThresholdInformation.Name = "grpNumericThresholdInformation"
        Me.grpNumericThresholdInformation.Size = New System.Drawing.Size(935, 174)
        Me.grpNumericThresholdInformation.TabIndex = 104
        Me.grpNumericThresholdInformation.Text = "Threshold Information"
        Me.grpNumericThresholdInformation.Visible = False
        '
        'chkNumericLowTriggerEnabled
        '
        Me.chkNumericLowTriggerEnabled.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkNumericLowTriggerEnabled.AutoSize = True
        Me.chkNumericLowTriggerEnabled.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.NumericChannelsBindingSource, "LowTriggerEnabled", True))
        Me.chkNumericLowTriggerEnabled.Location = New System.Drawing.Point(819, 123)
        Me.chkNumericLowTriggerEnabled.Name = "chkNumericLowTriggerEnabled"
        Me.chkNumericLowTriggerEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkNumericLowTriggerEnabled.TabIndex = 129
        Me.chkNumericLowTriggerEnabled.UseVisualStyleBackColor = True
        '
        'chkNumericHighTriggerEnabled
        '
        Me.chkNumericHighTriggerEnabled.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chkNumericHighTriggerEnabled.AutoSize = True
        Me.chkNumericHighTriggerEnabled.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.NumericChannelsBindingSource, "HighTriggerEnabled", True))
        Me.chkNumericHighTriggerEnabled.Location = New System.Drawing.Point(819, 70)
        Me.chkNumericHighTriggerEnabled.Name = "chkNumericHighTriggerEnabled"
        Me.chkNumericHighTriggerEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkNumericHighTriggerEnabled.TabIndex = 128
        Me.chkNumericHighTriggerEnabled.UseVisualStyleBackColor = True
        '
        'lblNumericLowTrigger
        '
        Me.lblNumericLowTrigger.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericLowTrigger.AutoSize = True
        Me.lblNumericLowTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericLowTrigger.Location = New System.Drawing.Point(840, 101)
        Me.lblNumericLowTrigger.Name = "lblNumericLowTrigger"
        Me.lblNumericLowTrigger.Size = New System.Drawing.Size(79, 16)
        Me.lblNumericLowTrigger.TabIndex = 127
        Me.lblNumericLowTrigger.Text = "Low Trigger"
        '
        'txtNumericLowTriggerValue
        '
        Me.txtNumericLowTriggerValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericLowTriggerValue.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericLowTriggerValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "LowTrigger", True))
        Me.txtNumericLowTriggerValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericLowTriggerValue.Location = New System.Drawing.Point(840, 120)
        Me.txtNumericLowTriggerValue.Name = "txtNumericLowTriggerValue"
        Me.txtNumericLowTriggerValue.Size = New System.Drawing.Size(80, 22)
        Me.txtNumericLowTriggerValue.TabIndex = 126
        '
        'txtNumericHighTriggerValue
        '
        Me.txtNumericHighTriggerValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNumericHighTriggerValue.BackColor = System.Drawing.Color.LightGray
        Me.txtNumericHighTriggerValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "HighTrigger", True))
        Me.txtNumericHighTriggerValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumericHighTriggerValue.Location = New System.Drawing.Point(840, 65)
        Me.txtNumericHighTriggerValue.Name = "txtNumericHighTriggerValue"
        Me.txtNumericHighTriggerValue.Size = New System.Drawing.Size(80, 22)
        Me.txtNumericHighTriggerValue.TabIndex = 125
        '
        'lblNumericTrigger
        '
        Me.lblNumericTrigger.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNumericTrigger.AutoSize = True
        Me.lblNumericTrigger.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericTrigger.Location = New System.Drawing.Point(837, 45)
        Me.lblNumericTrigger.Name = "lblNumericTrigger"
        Me.lblNumericTrigger.Size = New System.Drawing.Size(83, 16)
        Me.lblNumericTrigger.TabIndex = 124
        Me.lblNumericTrigger.Text = "High Trigger"
        '
        'ColorPickEdit5
        '
        Me.ColorPickEdit5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ColorPickEdit5.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.NumericChannelsBindingSource, "ThresholdLowLowColor", True))
        Me.ColorPickEdit5.EditValue = System.Drawing.Color.Empty
        Me.ColorPickEdit5.Location = New System.Drawing.Point(567, 147)
        Me.ColorPickEdit5.Name = "ColorPickEdit5"
        Me.ColorPickEdit5.Properties.Appearance.BackColor = System.Drawing.Color.LightGray
        Me.ColorPickEdit5.Properties.Appearance.Options.UseBackColor = True
        Me.ColorPickEdit5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ColorPickEdit5.Size = New System.Drawing.Size(51, 20)
        Me.ColorPickEdit5.TabIndex = 123
        '
        'ColorPickEdit4
        '
        Me.ColorPickEdit4.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ColorPickEdit4.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.NumericChannelsBindingSource, "ThresholdLowColor", True))
        Me.ColorPickEdit4.EditValue = System.Drawing.Color.Empty
        Me.ColorPickEdit4.Location = New System.Drawing.Point(567, 122)
        Me.ColorPickEdit4.Name = "ColorPickEdit4"
        Me.ColorPickEdit4.Properties.Appearance.BackColor = System.Drawing.Color.LightGray
        Me.ColorPickEdit4.Properties.Appearance.Options.UseBackColor = True
        Me.ColorPickEdit4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ColorPickEdit4.Size = New System.Drawing.Size(51, 20)
        Me.ColorPickEdit4.TabIndex = 122
        '
        'ColorPickEdit3
        '
        Me.ColorPickEdit3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ColorPickEdit3.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.NumericChannelsBindingSource, "ThresholdNormalColor", True))
        Me.ColorPickEdit3.EditValue = System.Drawing.Color.Empty
        Me.ColorPickEdit3.Location = New System.Drawing.Point(567, 95)
        Me.ColorPickEdit3.Name = "ColorPickEdit3"
        Me.ColorPickEdit3.Properties.Appearance.BackColor = System.Drawing.Color.LightGray
        Me.ColorPickEdit3.Properties.Appearance.Options.UseBackColor = True
        Me.ColorPickEdit3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ColorPickEdit3.Size = New System.Drawing.Size(51, 20)
        Me.ColorPickEdit3.TabIndex = 121
        '
        'ColorPickEdit2
        '
        Me.ColorPickEdit2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ColorPickEdit2.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.NumericChannelsBindingSource, "ThresholdHighColor", True))
        Me.ColorPickEdit2.EditValue = System.Drawing.Color.Empty
        Me.ColorPickEdit2.Location = New System.Drawing.Point(567, 67)
        Me.ColorPickEdit2.Name = "ColorPickEdit2"
        Me.ColorPickEdit2.Properties.Appearance.BackColor = System.Drawing.Color.LightGray
        Me.ColorPickEdit2.Properties.Appearance.Options.UseBackColor = True
        Me.ColorPickEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ColorPickEdit2.Size = New System.Drawing.Size(51, 20)
        Me.ColorPickEdit2.TabIndex = 120
        '
        'ColorPickEdit1
        '
        Me.ColorPickEdit1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ColorPickEdit1.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.NumericChannelsBindingSource, "ThresholdHighHighColor", True))
        Me.ColorPickEdit1.EditValue = System.Drawing.Color.Empty
        Me.ColorPickEdit1.Location = New System.Drawing.Point(567, 42)
        Me.ColorPickEdit1.Name = "ColorPickEdit1"
        Me.ColorPickEdit1.Properties.Appearance.BackColor = System.Drawing.Color.LightGray
        Me.ColorPickEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.ColorPickEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ColorPickEdit1.Size = New System.Drawing.Size(51, 20)
        Me.ColorPickEdit1.TabIndex = 119
        '
        'txtLowLowMessage
        '
        Me.txtLowLowMessage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtLowLowMessage.BackColor = System.Drawing.Color.LightGray
        Me.txtLowLowMessage.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdLowLowMessageText", True))
        Me.txtLowLowMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLowLowMessage.Location = New System.Drawing.Point(624, 146)
        Me.txtLowLowMessage.Name = "txtLowLowMessage"
        Me.txtLowLowMessage.Size = New System.Drawing.Size(182, 22)
        Me.txtLowLowMessage.TabIndex = 115
        '
        'txtLowMessage
        '
        Me.txtLowMessage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtLowMessage.BackColor = System.Drawing.Color.LightGray
        Me.txtLowMessage.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdLowMessageText", True))
        Me.txtLowMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLowMessage.Location = New System.Drawing.Point(624, 120)
        Me.txtLowMessage.Name = "txtLowMessage"
        Me.txtLowMessage.Size = New System.Drawing.Size(182, 22)
        Me.txtLowMessage.TabIndex = 114
        '
        'txtHighMessage
        '
        Me.txtHighMessage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtHighMessage.BackColor = System.Drawing.Color.LightGray
        Me.txtHighMessage.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdHighMessageText", True))
        Me.txtHighMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHighMessage.Location = New System.Drawing.Point(624, 66)
        Me.txtHighMessage.Name = "txtHighMessage"
        Me.txtHighMessage.Size = New System.Drawing.Size(182, 22)
        Me.txtHighMessage.TabIndex = 112
        '
        'txtHighHighMessage
        '
        Me.txtHighHighMessage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtHighHighMessage.BackColor = System.Drawing.Color.LightGray
        Me.txtHighHighMessage.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdHighHighMessageText", True))
        Me.txtHighHighMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHighHighMessage.Location = New System.Drawing.Point(624, 40)
        Me.txtHighHighMessage.Name = "txtHighHighMessage"
        Me.txtHighHighMessage.Size = New System.Drawing.Size(182, 22)
        Me.txtHighHighMessage.TabIndex = 111
        '
        'txtLowLowColor
        '
        Me.txtLowLowColor.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtLowLowColor.BackColor = System.Drawing.Color.LightGray
        Me.txtLowLowColor.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdLowLowColor", True))
        Me.txtLowLowColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLowLowColor.Location = New System.Drawing.Point(457, 146)
        Me.txtLowLowColor.Name = "txtLowLowColor"
        Me.txtLowLowColor.Size = New System.Drawing.Size(104, 22)
        Me.txtLowLowColor.TabIndex = 110
        '
        'txtLowColor
        '
        Me.txtLowColor.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtLowColor.BackColor = System.Drawing.Color.LightGray
        Me.txtLowColor.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdLowColor", True))
        Me.txtLowColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLowColor.Location = New System.Drawing.Point(457, 120)
        Me.txtLowColor.Name = "txtLowColor"
        Me.txtLowColor.Size = New System.Drawing.Size(104, 22)
        Me.txtLowColor.TabIndex = 109
        '
        'txtNormalColor
        '
        Me.txtNormalColor.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNormalColor.BackColor = System.Drawing.Color.LightGray
        Me.txtNormalColor.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdNormalColor", True))
        Me.txtNormalColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNormalColor.Location = New System.Drawing.Point(457, 94)
        Me.txtNormalColor.Name = "txtNormalColor"
        Me.txtNormalColor.Size = New System.Drawing.Size(104, 22)
        Me.txtNormalColor.TabIndex = 108
        '
        'txtHighColor
        '
        Me.txtHighColor.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtHighColor.BackColor = System.Drawing.Color.LightGray
        Me.txtHighColor.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdHighColor", True))
        Me.txtHighColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHighColor.Location = New System.Drawing.Point(457, 68)
        Me.txtHighColor.Name = "txtHighColor"
        Me.txtHighColor.Size = New System.Drawing.Size(104, 22)
        Me.txtHighColor.TabIndex = 107
        '
        'txtHighHighColor
        '
        Me.txtHighHighColor.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtHighHighColor.BackColor = System.Drawing.Color.LightGray
        Me.txtHighHighColor.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdHighHighColor", True))
        Me.txtHighHighColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHighHighColor.Location = New System.Drawing.Point(457, 42)
        Me.txtHighHighColor.Name = "txtHighHighColor"
        Me.txtHighHighColor.Size = New System.Drawing.Size(104, 22)
        Me.txtHighHighColor.TabIndex = 106
        '
        'txtLowLowChannelSource
        '
        Me.txtLowLowChannelSource.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtLowLowChannelSource.BackColor = System.Drawing.Color.LightGray
        Me.txtLowLowChannelSource.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdLowLowSource", True))
        Me.txtLowLowChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLowLowChannelSource.Location = New System.Drawing.Point(172, 146)
        Me.txtLowLowChannelSource.Name = "txtLowLowChannelSource"
        Me.txtLowLowChannelSource.Size = New System.Drawing.Size(280, 22)
        Me.txtLowLowChannelSource.TabIndex = 105
        '
        'txtLowChannelSource
        '
        Me.txtLowChannelSource.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtLowChannelSource.BackColor = System.Drawing.Color.LightGray
        Me.txtLowChannelSource.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdLowLowSource", True))
        Me.txtLowChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLowChannelSource.Location = New System.Drawing.Point(172, 120)
        Me.txtLowChannelSource.Name = "txtLowChannelSource"
        Me.txtLowChannelSource.Size = New System.Drawing.Size(280, 22)
        Me.txtLowChannelSource.TabIndex = 104
        '
        'txtHighChannelSource
        '
        Me.txtHighChannelSource.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtHighChannelSource.BackColor = System.Drawing.Color.LightGray
        Me.txtHighChannelSource.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdHighSource", True))
        Me.txtHighChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHighChannelSource.Location = New System.Drawing.Point(172, 68)
        Me.txtHighChannelSource.Name = "txtHighChannelSource"
        Me.txtHighChannelSource.Size = New System.Drawing.Size(280, 22)
        Me.txtHighChannelSource.TabIndex = 102
        '
        'txtHighHighChannelSource
        '
        Me.txtHighHighChannelSource.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtHighHighChannelSource.BackColor = System.Drawing.Color.LightGray
        Me.txtHighHighChannelSource.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdHighHighSource", True))
        Me.txtHighHighChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHighHighChannelSource.Location = New System.Drawing.Point(172, 42)
        Me.txtHighHighChannelSource.Name = "txtHighHighChannelSource"
        Me.txtHighHighChannelSource.Size = New System.Drawing.Size(280, 22)
        Me.txtHighHighChannelSource.TabIndex = 101
        '
        'txtLowLowValue
        '
        Me.txtLowLowValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtLowLowValue.BackColor = System.Drawing.Color.LightGray
        Me.txtLowLowValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdLowLow", True))
        Me.txtLowLowValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLowLowValue.Location = New System.Drawing.Point(80, 146)
        Me.txtLowLowValue.Name = "txtLowLowValue"
        Me.txtLowLowValue.Size = New System.Drawing.Size(86, 22)
        Me.txtLowLowValue.TabIndex = 99
        '
        'lblLowLow
        '
        Me.lblLowLow.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblLowLow.AutoSize = True
        Me.lblLowLow.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLowLow.Location = New System.Drawing.Point(11, 146)
        Me.lblLowLow.Name = "lblLowLow"
        Me.lblLowLow.Size = New System.Drawing.Size(59, 16)
        Me.lblLowLow.TabIndex = 98
        Me.lblLowLow.Text = "Low Low"
        '
        'txtLowValue
        '
        Me.txtLowValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtLowValue.BackColor = System.Drawing.Color.LightGray
        Me.txtLowValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdLow", True))
        Me.txtLowValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLowValue.Location = New System.Drawing.Point(80, 120)
        Me.txtLowValue.Name = "txtLowValue"
        Me.txtLowValue.Size = New System.Drawing.Size(86, 22)
        Me.txtLowValue.TabIndex = 97
        '
        'lblLow
        '
        Me.lblLow.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblLow.AutoSize = True
        Me.lblLow.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLow.Location = New System.Drawing.Point(11, 120)
        Me.lblLow.Name = "lblLow"
        Me.lblLow.Size = New System.Drawing.Size(32, 16)
        Me.lblLow.TabIndex = 96
        Me.lblLow.Text = "Low"
        '
        'txtNormalValue
        '
        Me.txtNormalValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtNormalValue.BackColor = System.Drawing.Color.LightGray
        Me.txtNormalValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdNormal", True))
        Me.txtNormalValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNormalValue.Location = New System.Drawing.Point(80, 94)
        Me.txtNormalValue.Name = "txtNormalValue"
        Me.txtNormalValue.Size = New System.Drawing.Size(86, 22)
        Me.txtNormalValue.TabIndex = 95
        '
        'lblNormal
        '
        Me.lblNormal.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblNormal.AutoSize = True
        Me.lblNormal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNormal.Location = New System.Drawing.Point(9, 94)
        Me.lblNormal.Name = "lblNormal"
        Me.lblNormal.Size = New System.Drawing.Size(52, 16)
        Me.lblNormal.TabIndex = 94
        Me.lblNormal.Text = "Normal"
        '
        'txtHighValue
        '
        Me.txtHighValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtHighValue.BackColor = System.Drawing.Color.LightGray
        Me.txtHighValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdHigh", True))
        Me.txtHighValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHighValue.Location = New System.Drawing.Point(80, 68)
        Me.txtHighValue.Name = "txtHighValue"
        Me.txtHighValue.Size = New System.Drawing.Size(86, 22)
        Me.txtHighValue.TabIndex = 93
        '
        'lblHigh
        '
        Me.lblHigh.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblHigh.AutoSize = True
        Me.lblHigh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHigh.Location = New System.Drawing.Point(11, 68)
        Me.lblHigh.Name = "lblHigh"
        Me.lblHigh.Size = New System.Drawing.Size(36, 16)
        Me.lblHigh.TabIndex = 92
        Me.lblHigh.Text = "High"
        '
        'txtHighHighValue
        '
        Me.txtHighHighValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtHighHighValue.BackColor = System.Drawing.Color.LightGray
        Me.txtHighHighValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.NumericChannelsBindingSource, "ThresholdHighHigh", True))
        Me.txtHighHighValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHighHighValue.Location = New System.Drawing.Point(80, 42)
        Me.txtHighHighValue.Name = "txtHighHighValue"
        Me.txtHighHighValue.Size = New System.Drawing.Size(86, 22)
        Me.txtHighHighValue.TabIndex = 78
        '
        'lblHighHigh
        '
        Me.lblHighHigh.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblHighHigh.AutoSize = True
        Me.lblHighHigh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHighHigh.Location = New System.Drawing.Point(9, 42)
        Me.lblHighHigh.Name = "lblHighHigh"
        Me.lblHighHigh.Size = New System.Drawing.Size(67, 16)
        Me.lblHighHigh.TabIndex = 79
        Me.lblHighHigh.Text = "High High"
        '
        'Label14
        '
        Me.Label14.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(624, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 16)
        Me.Label14.TabIndex = 118
        Me.Label14.Text = "Message"
        '
        'lblColor
        '
        Me.lblColor.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblColor.AutoSize = True
        Me.lblColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColor.Location = New System.Drawing.Point(455, 22)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(40, 16)
        Me.lblColor.TabIndex = 117
        Me.lblColor.Text = "Color"
        '
        'lblChannelSource
        '
        Me.lblChannelSource.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblChannelSource.AutoSize = True
        Me.lblChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChannelSource.Location = New System.Drawing.Point(169, 23)
        Me.lblChannelSource.Name = "lblChannelSource"
        Me.lblChannelSource.Size = New System.Drawing.Size(103, 16)
        Me.lblChannelSource.TabIndex = 116
        Me.lblChannelSource.Text = "Channel Source"
        '
        'lblValue
        '
        Me.lblValue.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblValue.AutoSize = True
        Me.lblValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValue.Location = New System.Drawing.Point(77, 23)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(43, 16)
        Me.lblValue.TabIndex = 100
        Me.lblValue.Text = "Value"
        '
        'dgvNumericChannels
        '
        Me.dgvNumericChannels.AllowUserToAddRows = False
        Me.dgvNumericChannels.AllowUserToDeleteRows = False
        Me.dgvNumericChannels.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvNumericChannels.AutoGenerateColumns = False
        Me.dgvNumericChannels.BackgroundColor = System.Drawing.Color.Black
        Me.dgvNumericChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNumericChannels.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CollectionNameDataGridViewTextBoxColumn1, Me.ChannelNameDataGridViewTextBoxColumn1, Me.DescriptionDataGridViewTextBoxColumn1, Me.SourceDataGridViewTextBoxColumn1, Me.DestinationDataGridViewTextBoxColumn1, Me.ExpressionDataGridViewTextBoxColumn1, Me.OPCGroupDataGridViewTextBoxColumn1, Me.OPCItemIDDataGridViewTextBoxColumn1, Me.TooltipDataGridViewTextBoxColumn1, Me.DecimalDigitsDataGridViewTextBoxColumn, Me.FormatDataGridViewTextBoxColumn, Me.MinimumDataGridViewTextBoxColumn, Me.MaximumDataGridViewTextBoxColumn, Me.EngUnitIDDataGridViewTextBoxColumn, Me.MeanEnabledDataGridViewTextBoxColumn, Me.MeanPeriodDataGridViewTextBoxColumn, Me.ThresholdHighHighDataGridViewTextBoxColumn, Me.ThresholdHighHighSourceDataGridViewTextBoxColumn, Me.ThresholdHighHighColorDataGridViewTextBoxColumn, Me.ThresholdHighHighMessageTextDataGridViewTextBoxColumn, Me.ThresholdHighDataGridViewTextBoxColumn, Me.ThresholdHighSourceDataGridViewTextBoxColumn, Me.ThresholdHighColorDataGridViewTextBoxColumn, Me.ThresholdHighMessageTextDataGridViewTextBoxColumn, Me.ThresholdLowLowDataGridViewTextBoxColumn, Me.ThresholdLowLowSourceDataGridViewTextBoxColumn, Me.ThresholdLowLowColorDataGridViewTextBoxColumn, Me.ThresholdLowLowMessageTextDataGridViewTextBoxColumn, Me.ThresholdLowDataGridViewTextBoxColumn, Me.ThresholdLowSourceDataGridViewTextBoxColumn, Me.ThresholdLowColorDataGridViewTextBoxColumn, Me.ThresholdLowMessageTextDataGridViewTextBoxColumn, Me.ThresholdNormalDataGridViewTextBoxColumn, Me.ThresholdNormalColorDataGridViewTextBoxColumn, Me.HighTriggerDataGridViewTextBoxColumn, Me.HighTriggerEnabledDataGridViewTextBoxColumn, Me.LowTriggerDataGridViewTextBoxColumn, Me.LowTriggerEnabledDataGridViewTextBoxColumn, Me.SpeechMessageTextDataGridViewTextBoxColumn, Me.InitialValueDataGridViewTextBoxColumn1, Me.FinalValueDataGridViewTextBoxColumn1, Me.PersistentDataGridViewTextBoxColumn})
        Me.dgvNumericChannels.DataSource = Me.NumericChannelsBindingSource
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvNumericChannels.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvNumericChannels.Location = New System.Drawing.Point(0, 0)
        Me.dgvNumericChannels.Name = "dgvNumericChannels"
        Me.dgvNumericChannels.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvNumericChannels.Size = New System.Drawing.Size(935, 157)
        Me.dgvNumericChannels.TabIndex = 101
        '
        'CollectionNameDataGridViewTextBoxColumn1
        '
        Me.CollectionNameDataGridViewTextBoxColumn1.DataPropertyName = "CollectionName"
        Me.CollectionNameDataGridViewTextBoxColumn1.Frozen = True
        Me.CollectionNameDataGridViewTextBoxColumn1.HeaderText = "CollectionName"
        Me.CollectionNameDataGridViewTextBoxColumn1.Name = "CollectionNameDataGridViewTextBoxColumn1"
        '
        'ChannelNameDataGridViewTextBoxColumn1
        '
        Me.ChannelNameDataGridViewTextBoxColumn1.DataPropertyName = "ChannelName"
        Me.ChannelNameDataGridViewTextBoxColumn1.Frozen = True
        Me.ChannelNameDataGridViewTextBoxColumn1.HeaderText = "ChannelName"
        Me.ChannelNameDataGridViewTextBoxColumn1.Name = "ChannelNameDataGridViewTextBoxColumn1"
        Me.ChannelNameDataGridViewTextBoxColumn1.Width = 200
        '
        'DescriptionDataGridViewTextBoxColumn1
        '
        Me.DescriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description"
        Me.DescriptionDataGridViewTextBoxColumn1.HeaderText = "Description"
        Me.DescriptionDataGridViewTextBoxColumn1.Name = "DescriptionDataGridViewTextBoxColumn1"
        Me.DescriptionDataGridViewTextBoxColumn1.Width = 200
        '
        'SourceDataGridViewTextBoxColumn1
        '
        Me.SourceDataGridViewTextBoxColumn1.DataPropertyName = "Source"
        Me.SourceDataGridViewTextBoxColumn1.HeaderText = "Source"
        Me.SourceDataGridViewTextBoxColumn1.Name = "SourceDataGridViewTextBoxColumn1"
        Me.SourceDataGridViewTextBoxColumn1.Width = 200
        '
        'DestinationDataGridViewTextBoxColumn1
        '
        Me.DestinationDataGridViewTextBoxColumn1.DataPropertyName = "Destination"
        Me.DestinationDataGridViewTextBoxColumn1.HeaderText = "Destination"
        Me.DestinationDataGridViewTextBoxColumn1.Name = "DestinationDataGridViewTextBoxColumn1"
        Me.DestinationDataGridViewTextBoxColumn1.Width = 200
        '
        'ExpressionDataGridViewTextBoxColumn1
        '
        Me.ExpressionDataGridViewTextBoxColumn1.DataPropertyName = "Expression"
        Me.ExpressionDataGridViewTextBoxColumn1.HeaderText = "Expression"
        Me.ExpressionDataGridViewTextBoxColumn1.Name = "ExpressionDataGridViewTextBoxColumn1"
        Me.ExpressionDataGridViewTextBoxColumn1.Width = 200
        '
        'OPCGroupDataGridViewTextBoxColumn1
        '
        Me.OPCGroupDataGridViewTextBoxColumn1.DataPropertyName = "OPCGroup"
        Me.OPCGroupDataGridViewTextBoxColumn1.HeaderText = "OPCGroup"
        Me.OPCGroupDataGridViewTextBoxColumn1.Name = "OPCGroupDataGridViewTextBoxColumn1"
        '
        'OPCItemIDDataGridViewTextBoxColumn1
        '
        Me.OPCItemIDDataGridViewTextBoxColumn1.DataPropertyName = "OPCItemID"
        Me.OPCItemIDDataGridViewTextBoxColumn1.HeaderText = "OPCItemID"
        Me.OPCItemIDDataGridViewTextBoxColumn1.Name = "OPCItemIDDataGridViewTextBoxColumn1"
        Me.OPCItemIDDataGridViewTextBoxColumn1.Width = 200
        '
        'TooltipDataGridViewTextBoxColumn1
        '
        Me.TooltipDataGridViewTextBoxColumn1.DataPropertyName = "Tooltip"
        Me.TooltipDataGridViewTextBoxColumn1.HeaderText = "Tooltip"
        Me.TooltipDataGridViewTextBoxColumn1.Name = "TooltipDataGridViewTextBoxColumn1"
        '
        'DecimalDigitsDataGridViewTextBoxColumn
        '
        Me.DecimalDigitsDataGridViewTextBoxColumn.DataPropertyName = "DecimalDigits"
        Me.DecimalDigitsDataGridViewTextBoxColumn.HeaderText = "DecimalDigits"
        Me.DecimalDigitsDataGridViewTextBoxColumn.Name = "DecimalDigitsDataGridViewTextBoxColumn"
        '
        'FormatDataGridViewTextBoxColumn
        '
        Me.FormatDataGridViewTextBoxColumn.DataPropertyName = "Format"
        Me.FormatDataGridViewTextBoxColumn.HeaderText = "Format"
        Me.FormatDataGridViewTextBoxColumn.Name = "FormatDataGridViewTextBoxColumn"
        '
        'MinimumDataGridViewTextBoxColumn
        '
        Me.MinimumDataGridViewTextBoxColumn.DataPropertyName = "Minimum"
        Me.MinimumDataGridViewTextBoxColumn.HeaderText = "Minimum"
        Me.MinimumDataGridViewTextBoxColumn.Name = "MinimumDataGridViewTextBoxColumn"
        '
        'MaximumDataGridViewTextBoxColumn
        '
        Me.MaximumDataGridViewTextBoxColumn.DataPropertyName = "Maximum"
        Me.MaximumDataGridViewTextBoxColumn.HeaderText = "Maximum"
        Me.MaximumDataGridViewTextBoxColumn.Name = "MaximumDataGridViewTextBoxColumn"
        '
        'EngUnitIDDataGridViewTextBoxColumn
        '
        Me.EngUnitIDDataGridViewTextBoxColumn.DataPropertyName = "EngUnitID"
        Me.EngUnitIDDataGridViewTextBoxColumn.DataSource = Me.EngineeringUnitBindingSource
        Me.EngUnitIDDataGridViewTextBoxColumn.DisplayMember = "DisplayName"
        Me.EngUnitIDDataGridViewTextBoxColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.EngUnitIDDataGridViewTextBoxColumn.HeaderText = "EngUnitID"
        Me.EngUnitIDDataGridViewTextBoxColumn.Name = "EngUnitIDDataGridViewTextBoxColumn"
        Me.EngUnitIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EngUnitIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.EngUnitIDDataGridViewTextBoxColumn.ValueMember = "Id"
        '
        'MeanEnabledDataGridViewTextBoxColumn
        '
        Me.MeanEnabledDataGridViewTextBoxColumn.DataPropertyName = "MeanEnabled"
        Me.MeanEnabledDataGridViewTextBoxColumn.HeaderText = "MeanEnabled"
        Me.MeanEnabledDataGridViewTextBoxColumn.Name = "MeanEnabledDataGridViewTextBoxColumn"
        '
        'MeanPeriodDataGridViewTextBoxColumn
        '
        Me.MeanPeriodDataGridViewTextBoxColumn.DataPropertyName = "MeanPeriod"
        Me.MeanPeriodDataGridViewTextBoxColumn.HeaderText = "MeanPeriod"
        Me.MeanPeriodDataGridViewTextBoxColumn.Name = "MeanPeriodDataGridViewTextBoxColumn"
        '
        'ThresholdHighHighDataGridViewTextBoxColumn
        '
        Me.ThresholdHighHighDataGridViewTextBoxColumn.DataPropertyName = "ThresholdHighHigh"
        Me.ThresholdHighHighDataGridViewTextBoxColumn.HeaderText = "ThresholdHighHigh"
        Me.ThresholdHighHighDataGridViewTextBoxColumn.Name = "ThresholdHighHighDataGridViewTextBoxColumn"
        '
        'ThresholdHighHighSourceDataGridViewTextBoxColumn
        '
        Me.ThresholdHighHighSourceDataGridViewTextBoxColumn.DataPropertyName = "ThresholdHighHighSource"
        Me.ThresholdHighHighSourceDataGridViewTextBoxColumn.HeaderText = "ThresholdHighHighSource"
        Me.ThresholdHighHighSourceDataGridViewTextBoxColumn.Name = "ThresholdHighHighSourceDataGridViewTextBoxColumn"
        '
        'ThresholdHighHighColorDataGridViewTextBoxColumn
        '
        Me.ThresholdHighHighColorDataGridViewTextBoxColumn.DataPropertyName = "ThresholdHighHighColor"
        Me.ThresholdHighHighColorDataGridViewTextBoxColumn.HeaderText = "ThresholdHighHighColor"
        Me.ThresholdHighHighColorDataGridViewTextBoxColumn.Name = "ThresholdHighHighColorDataGridViewTextBoxColumn"
        '
        'ThresholdHighHighMessageTextDataGridViewTextBoxColumn
        '
        Me.ThresholdHighHighMessageTextDataGridViewTextBoxColumn.DataPropertyName = "ThresholdHighHighMessageText"
        Me.ThresholdHighHighMessageTextDataGridViewTextBoxColumn.HeaderText = "ThresholdHighHighMessageText"
        Me.ThresholdHighHighMessageTextDataGridViewTextBoxColumn.Name = "ThresholdHighHighMessageTextDataGridViewTextBoxColumn"
        '
        'ThresholdHighDataGridViewTextBoxColumn
        '
        Me.ThresholdHighDataGridViewTextBoxColumn.DataPropertyName = "ThresholdHigh"
        Me.ThresholdHighDataGridViewTextBoxColumn.HeaderText = "ThresholdHigh"
        Me.ThresholdHighDataGridViewTextBoxColumn.Name = "ThresholdHighDataGridViewTextBoxColumn"
        '
        'ThresholdHighSourceDataGridViewTextBoxColumn
        '
        Me.ThresholdHighSourceDataGridViewTextBoxColumn.DataPropertyName = "ThresholdHighSource"
        Me.ThresholdHighSourceDataGridViewTextBoxColumn.HeaderText = "ThresholdHighSource"
        Me.ThresholdHighSourceDataGridViewTextBoxColumn.Name = "ThresholdHighSourceDataGridViewTextBoxColumn"
        '
        'ThresholdHighColorDataGridViewTextBoxColumn
        '
        Me.ThresholdHighColorDataGridViewTextBoxColumn.DataPropertyName = "ThresholdHighColor"
        Me.ThresholdHighColorDataGridViewTextBoxColumn.HeaderText = "ThresholdHighColor"
        Me.ThresholdHighColorDataGridViewTextBoxColumn.Name = "ThresholdHighColorDataGridViewTextBoxColumn"
        '
        'ThresholdHighMessageTextDataGridViewTextBoxColumn
        '
        Me.ThresholdHighMessageTextDataGridViewTextBoxColumn.DataPropertyName = "ThresholdHighMessageText"
        Me.ThresholdHighMessageTextDataGridViewTextBoxColumn.HeaderText = "ThresholdHighMessageText"
        Me.ThresholdHighMessageTextDataGridViewTextBoxColumn.Name = "ThresholdHighMessageTextDataGridViewTextBoxColumn"
        '
        'ThresholdLowLowDataGridViewTextBoxColumn
        '
        Me.ThresholdLowLowDataGridViewTextBoxColumn.DataPropertyName = "ThresholdLowLow"
        Me.ThresholdLowLowDataGridViewTextBoxColumn.HeaderText = "ThresholdLowLow"
        Me.ThresholdLowLowDataGridViewTextBoxColumn.Name = "ThresholdLowLowDataGridViewTextBoxColumn"
        '
        'ThresholdLowLowSourceDataGridViewTextBoxColumn
        '
        Me.ThresholdLowLowSourceDataGridViewTextBoxColumn.DataPropertyName = "ThresholdLowLowSource"
        Me.ThresholdLowLowSourceDataGridViewTextBoxColumn.HeaderText = "ThresholdLowLowSource"
        Me.ThresholdLowLowSourceDataGridViewTextBoxColumn.Name = "ThresholdLowLowSourceDataGridViewTextBoxColumn"
        '
        'ThresholdLowLowColorDataGridViewTextBoxColumn
        '
        Me.ThresholdLowLowColorDataGridViewTextBoxColumn.DataPropertyName = "ThresholdLowLowColor"
        Me.ThresholdLowLowColorDataGridViewTextBoxColumn.HeaderText = "ThresholdLowLowColor"
        Me.ThresholdLowLowColorDataGridViewTextBoxColumn.Name = "ThresholdLowLowColorDataGridViewTextBoxColumn"
        '
        'ThresholdLowLowMessageTextDataGridViewTextBoxColumn
        '
        Me.ThresholdLowLowMessageTextDataGridViewTextBoxColumn.DataPropertyName = "ThresholdLowLowMessageText"
        Me.ThresholdLowLowMessageTextDataGridViewTextBoxColumn.HeaderText = "ThresholdLowLowMessageText"
        Me.ThresholdLowLowMessageTextDataGridViewTextBoxColumn.Name = "ThresholdLowLowMessageTextDataGridViewTextBoxColumn"
        '
        'ThresholdLowDataGridViewTextBoxColumn
        '
        Me.ThresholdLowDataGridViewTextBoxColumn.DataPropertyName = "ThresholdLow"
        Me.ThresholdLowDataGridViewTextBoxColumn.HeaderText = "ThresholdLow"
        Me.ThresholdLowDataGridViewTextBoxColumn.Name = "ThresholdLowDataGridViewTextBoxColumn"
        '
        'ThresholdLowSourceDataGridViewTextBoxColumn
        '
        Me.ThresholdLowSourceDataGridViewTextBoxColumn.DataPropertyName = "ThresholdLowSource"
        Me.ThresholdLowSourceDataGridViewTextBoxColumn.HeaderText = "ThresholdLowSource"
        Me.ThresholdLowSourceDataGridViewTextBoxColumn.Name = "ThresholdLowSourceDataGridViewTextBoxColumn"
        '
        'ThresholdLowColorDataGridViewTextBoxColumn
        '
        Me.ThresholdLowColorDataGridViewTextBoxColumn.DataPropertyName = "ThresholdLowColor"
        Me.ThresholdLowColorDataGridViewTextBoxColumn.HeaderText = "ThresholdLowColor"
        Me.ThresholdLowColorDataGridViewTextBoxColumn.Name = "ThresholdLowColorDataGridViewTextBoxColumn"
        '
        'ThresholdLowMessageTextDataGridViewTextBoxColumn
        '
        Me.ThresholdLowMessageTextDataGridViewTextBoxColumn.DataPropertyName = "ThresholdLowMessageText"
        Me.ThresholdLowMessageTextDataGridViewTextBoxColumn.HeaderText = "ThresholdLowMessageText"
        Me.ThresholdLowMessageTextDataGridViewTextBoxColumn.Name = "ThresholdLowMessageTextDataGridViewTextBoxColumn"
        '
        'ThresholdNormalDataGridViewTextBoxColumn
        '
        Me.ThresholdNormalDataGridViewTextBoxColumn.DataPropertyName = "ThresholdNormal"
        Me.ThresholdNormalDataGridViewTextBoxColumn.HeaderText = "ThresholdNormal"
        Me.ThresholdNormalDataGridViewTextBoxColumn.Name = "ThresholdNormalDataGridViewTextBoxColumn"
        '
        'ThresholdNormalColorDataGridViewTextBoxColumn
        '
        Me.ThresholdNormalColorDataGridViewTextBoxColumn.DataPropertyName = "ThresholdNormalColor"
        Me.ThresholdNormalColorDataGridViewTextBoxColumn.HeaderText = "ThresholdNormalColor"
        Me.ThresholdNormalColorDataGridViewTextBoxColumn.Name = "ThresholdNormalColorDataGridViewTextBoxColumn"
        '
        'HighTriggerDataGridViewTextBoxColumn
        '
        Me.HighTriggerDataGridViewTextBoxColumn.DataPropertyName = "HighTrigger"
        Me.HighTriggerDataGridViewTextBoxColumn.HeaderText = "HighTrigger"
        Me.HighTriggerDataGridViewTextBoxColumn.Name = "HighTriggerDataGridViewTextBoxColumn"
        '
        'HighTriggerEnabledDataGridViewTextBoxColumn
        '
        Me.HighTriggerEnabledDataGridViewTextBoxColumn.DataPropertyName = "HighTriggerEnabled"
        Me.HighTriggerEnabledDataGridViewTextBoxColumn.HeaderText = "HighTriggerEnabled"
        Me.HighTriggerEnabledDataGridViewTextBoxColumn.Name = "HighTriggerEnabledDataGridViewTextBoxColumn"
        '
        'LowTriggerDataGridViewTextBoxColumn
        '
        Me.LowTriggerDataGridViewTextBoxColumn.DataPropertyName = "LowTrigger"
        Me.LowTriggerDataGridViewTextBoxColumn.HeaderText = "LowTrigger"
        Me.LowTriggerDataGridViewTextBoxColumn.Name = "LowTriggerDataGridViewTextBoxColumn"
        '
        'LowTriggerEnabledDataGridViewTextBoxColumn
        '
        Me.LowTriggerEnabledDataGridViewTextBoxColumn.DataPropertyName = "LowTriggerEnabled"
        Me.LowTriggerEnabledDataGridViewTextBoxColumn.HeaderText = "LowTriggerEnabled"
        Me.LowTriggerEnabledDataGridViewTextBoxColumn.Name = "LowTriggerEnabledDataGridViewTextBoxColumn"
        '
        'SpeechMessageTextDataGridViewTextBoxColumn
        '
        Me.SpeechMessageTextDataGridViewTextBoxColumn.DataPropertyName = "SpeechMessageText"
        Me.SpeechMessageTextDataGridViewTextBoxColumn.HeaderText = "SpeechMessageText"
        Me.SpeechMessageTextDataGridViewTextBoxColumn.Name = "SpeechMessageTextDataGridViewTextBoxColumn"
        '
        'InitialValueDataGridViewTextBoxColumn1
        '
        Me.InitialValueDataGridViewTextBoxColumn1.DataPropertyName = "InitialValue"
        Me.InitialValueDataGridViewTextBoxColumn1.HeaderText = "InitialValue"
        Me.InitialValueDataGridViewTextBoxColumn1.Name = "InitialValueDataGridViewTextBoxColumn1"
        '
        'FinalValueDataGridViewTextBoxColumn1
        '
        Me.FinalValueDataGridViewTextBoxColumn1.DataPropertyName = "FinalValue"
        Me.FinalValueDataGridViewTextBoxColumn1.HeaderText = "FinalValue"
        Me.FinalValueDataGridViewTextBoxColumn1.Name = "FinalValueDataGridViewTextBoxColumn1"
        '
        'PersistentDataGridViewTextBoxColumn
        '
        Me.PersistentDataGridViewTextBoxColumn.DataPropertyName = "Persistent"
        Me.PersistentDataGridViewTextBoxColumn.HeaderText = "Persistent"
        Me.PersistentDataGridViewTextBoxColumn.Name = "PersistentDataGridViewTextBoxColumn"
        '
        'navNumeric
        '
        Me.navNumeric.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.navNumeric.DataSource = Me.NumericChannelsBindingSource
        Me.navNumeric.Location = New System.Drawing.Point(1, 667)
        Me.navNumeric.Name = "navNumeric"
        Me.navNumeric.Size = New System.Drawing.Size(373, 24)
        Me.navNumeric.StyleController = Me.StyleController1
        Me.navNumeric.TabIndex = 103
        Me.navNumeric.Text = "DataNavigator2"
        '
        'XtraTabPage3
        '
        Me.XtraTabPage3.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.XtraTabPage3.Appearance.PageClient.Options.UseBackColor = True
        Me.XtraTabPage3.Controls.Add(Me.navText)
        Me.XtraTabPage3.Controls.Add(Me.grpTextChannels)
        Me.XtraTabPage3.Controls.Add(Me.dgvTextChannels)
        Me.XtraTabPage3.Name = "XtraTabPage3"
        Me.XtraTabPage3.Size = New System.Drawing.Size(935, 693)
        Me.XtraTabPage3.Text = "Text Channels"
        '
        'navText
        '
        Me.navText.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.navText.DataSource = Me.TextChannelsBindingSource
        Me.navText.Location = New System.Drawing.Point(3, 665)
        Me.navText.Name = "navText"
        Me.navText.Size = New System.Drawing.Size(373, 24)
        Me.navText.StyleController = Me.StyleController1
        Me.navText.TabIndex = 96
        Me.navText.Text = "DataNavigator3"
        '
        'grpTextChannels
        '
        Me.grpTextChannels.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.grpTextChannels.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.grpTextChannels.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.grpTextChannels.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.grpTextChannels.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTextChannels.Appearance.ForeColor = System.Drawing.Color.Silver
        Me.grpTextChannels.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grpTextChannels.Appearance.Options.UseBackColor = True
        Me.grpTextChannels.Appearance.Options.UseBorderColor = True
        Me.grpTextChannels.Appearance.Options.UseFont = True
        Me.grpTextChannels.Appearance.Options.UseForeColor = True
        Me.grpTextChannels.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTextChannels.AppearanceCaption.ForeColor = System.Drawing.Color.White
        Me.grpTextChannels.AppearanceCaption.Options.UseFont = True
        Me.grpTextChannels.AppearanceCaption.Options.UseForeColor = True
        Me.grpTextChannels.Controls.Add(Me.txtTextOpcItemId)
        Me.grpTextChannels.Controls.Add(Me.lblTextOpcItemId)
        Me.grpTextChannels.Controls.Add(Me.txtTextOpcGroup)
        Me.grpTextChannels.Controls.Add(Me.lblTextOpcGroup)
        Me.grpTextChannels.Controls.Add(Me.txtTextFinalValue)
        Me.grpTextChannels.Controls.Add(Me.txtTextInitialValue)
        Me.grpTextChannels.Controls.Add(Me.lblTextFinalValue)
        Me.grpTextChannels.Controls.Add(Me.lblTextInitialValue)
        Me.grpTextChannels.Controls.Add(Me.txtTextChannelDestination)
        Me.grpTextChannels.Controls.Add(Me.txtTextChannelSource)
        Me.grpTextChannels.Controls.Add(Me.txtTextTooltip)
        Me.grpTextChannels.Controls.Add(Me.txtTextDescription)
        Me.grpTextChannels.Controls.Add(Me.cboTextCollectionName)
        Me.grpTextChannels.Controls.Add(Me.txtTextChannelName)
        Me.grpTextChannels.Controls.Add(Me.Label12)
        Me.grpTextChannels.Controls.Add(Me.Label13)
        Me.grpTextChannels.Controls.Add(Me.Label16)
        Me.grpTextChannels.Controls.Add(Me.Label17)
        Me.grpTextChannels.Controls.Add(Me.Label18)
        Me.grpTextChannels.Controls.Add(Me.Label19)
        Me.grpTextChannels.Location = New System.Drawing.Point(3, 387)
        Me.grpTextChannels.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.grpTextChannels.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grpTextChannels.Name = "grpTextChannels"
        Me.grpTextChannels.Size = New System.Drawing.Size(929, 272)
        Me.grpTextChannels.TabIndex = 95
        Me.grpTextChannels.Text = "Text Channel Information"
        '
        'txtTextOpcItemId
        '
        Me.txtTextOpcItemId.BackColor = System.Drawing.Color.LightGray
        Me.txtTextOpcItemId.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TextChannelsBindingSource, "OPCItemID", True))
        Me.txtTextOpcItemId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextOpcItemId.Location = New System.Drawing.Point(315, 223)
        Me.txtTextOpcItemId.Name = "txtTextOpcItemId"
        Me.txtTextOpcItemId.Size = New System.Drawing.Size(601, 22)
        Me.txtTextOpcItemId.TabIndex = 124
        '
        'lblTextOpcItemId
        '
        Me.lblTextOpcItemId.AutoSize = True
        Me.lblTextOpcItemId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTextOpcItemId.Location = New System.Drawing.Point(312, 204)
        Me.lblTextOpcItemId.Name = "lblTextOpcItemId"
        Me.lblTextOpcItemId.Size = New System.Drawing.Size(75, 16)
        Me.lblTextOpcItemId.TabIndex = 123
        Me.lblTextOpcItemId.Text = "OPC ItemId"
        '
        'txtTextOpcGroup
        '
        Me.txtTextOpcGroup.BackColor = System.Drawing.Color.LightGray
        Me.txtTextOpcGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TextChannelsBindingSource, "OPCGroup", True))
        Me.txtTextOpcGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextOpcGroup.Location = New System.Drawing.Point(14, 223)
        Me.txtTextOpcGroup.Name = "txtTextOpcGroup"
        Me.txtTextOpcGroup.Size = New System.Drawing.Size(290, 22)
        Me.txtTextOpcGroup.TabIndex = 122
        '
        'lblTextOpcGroup
        '
        Me.lblTextOpcGroup.AutoSize = True
        Me.lblTextOpcGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTextOpcGroup.Location = New System.Drawing.Point(9, 204)
        Me.lblTextOpcGroup.Name = "lblTextOpcGroup"
        Me.lblTextOpcGroup.Size = New System.Drawing.Size(76, 16)
        Me.lblTextOpcGroup.TabIndex = 121
        Me.lblTextOpcGroup.Text = "OPC Group"
        '
        'txtTextFinalValue
        '
        Me.txtTextFinalValue.BackColor = System.Drawing.Color.LightGray
        Me.txtTextFinalValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TextChannelsBindingSource, "FinalValue", True))
        Me.txtTextFinalValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextFinalValue.Location = New System.Drawing.Point(699, 135)
        Me.txtTextFinalValue.Name = "txtTextFinalValue"
        Me.txtTextFinalValue.Size = New System.Drawing.Size(217, 22)
        Me.txtTextFinalValue.TabIndex = 98
        '
        'txtTextInitialValue
        '
        Me.txtTextInitialValue.BackColor = System.Drawing.Color.LightGray
        Me.txtTextInitialValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TextChannelsBindingSource, "InitialValue", True))
        Me.txtTextInitialValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextInitialValue.Location = New System.Drawing.Point(471, 135)
        Me.txtTextInitialValue.Name = "txtTextInitialValue"
        Me.txtTextInitialValue.Size = New System.Drawing.Size(217, 22)
        Me.txtTextInitialValue.TabIndex = 96
        '
        'lblTextFinalValue
        '
        Me.lblTextFinalValue.AutoSize = True
        Me.lblTextFinalValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTextFinalValue.Location = New System.Drawing.Point(696, 116)
        Me.lblTextFinalValue.Name = "lblTextFinalValue"
        Me.lblTextFinalValue.Size = New System.Drawing.Size(75, 16)
        Me.lblTextFinalValue.TabIndex = 99
        Me.lblTextFinalValue.Text = "Final Value"
        '
        'lblTextInitialValue
        '
        Me.lblTextInitialValue.AutoSize = True
        Me.lblTextInitialValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTextInitialValue.Location = New System.Drawing.Point(468, 116)
        Me.lblTextInitialValue.Name = "lblTextInitialValue"
        Me.lblTextInitialValue.Size = New System.Drawing.Size(73, 16)
        Me.lblTextInitialValue.TabIndex = 97
        Me.lblTextInitialValue.Text = "Intial Value"
        '
        'txtTextChannelDestination
        '
        Me.txtTextChannelDestination.BackColor = System.Drawing.Color.LightGray
        Me.txtTextChannelDestination.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TextChannelsBindingSource, "Destination", True))
        Me.txtTextChannelDestination.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextChannelDestination.Location = New System.Drawing.Point(12, 179)
        Me.txtTextChannelDestination.Name = "txtTextChannelDestination"
        Me.txtTextChannelDestination.Size = New System.Drawing.Size(445, 22)
        Me.txtTextChannelDestination.TabIndex = 73
        '
        'txtTextChannelSource
        '
        Me.txtTextChannelSource.BackColor = System.Drawing.Color.LightGray
        Me.txtTextChannelSource.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TextChannelsBindingSource, "Source", True))
        Me.txtTextChannelSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextChannelSource.Location = New System.Drawing.Point(12, 135)
        Me.txtTextChannelSource.Name = "txtTextChannelSource"
        Me.txtTextChannelSource.Size = New System.Drawing.Size(445, 22)
        Me.txtTextChannelSource.TabIndex = 72
        '
        'txtTextTooltip
        '
        Me.txtTextTooltip.BackColor = System.Drawing.Color.LightGray
        Me.txtTextTooltip.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TextChannelsBindingSource, "Tooltip", True))
        Me.txtTextTooltip.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextTooltip.Location = New System.Drawing.Point(471, 91)
        Me.txtTextTooltip.Name = "txtTextTooltip"
        Me.txtTextTooltip.Size = New System.Drawing.Size(445, 22)
        Me.txtTextTooltip.TabIndex = 66
        '
        'txtTextDescription
        '
        Me.txtTextDescription.BackColor = System.Drawing.Color.LightGray
        Me.txtTextDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TextChannelsBindingSource, "Description", True))
        Me.txtTextDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextDescription.Location = New System.Drawing.Point(12, 91)
        Me.txtTextDescription.Name = "txtTextDescription"
        Me.txtTextDescription.Size = New System.Drawing.Size(445, 22)
        Me.txtTextDescription.TabIndex = 64
        '
        'cboTextCollectionName
        '
        Me.cboTextCollectionName.BackColor = System.Drawing.Color.LightGray
        Me.cboTextCollectionName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TextChannelsBindingSource, "CollectionName", True))
        Me.cboTextCollectionName.DataSource = Me.m_UutDynamicChannelCollectionCollection
        Me.cboTextCollectionName.DisplayMember = "CollectionName"
        Me.cboTextCollectionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTextCollectionName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTextCollectionName.FormattingEnabled = True
        Me.cboTextCollectionName.Location = New System.Drawing.Point(640, 46)
        Me.cboTextCollectionName.Name = "cboTextCollectionName"
        Me.cboTextCollectionName.Size = New System.Drawing.Size(276, 24)
        Me.cboTextCollectionName.TabIndex = 62
        Me.cboTextCollectionName.ValueMember = "Id"
        '
        'txtTextChannelName
        '
        Me.txtTextChannelName.BackColor = System.Drawing.Color.LightGray
        Me.txtTextChannelName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.TextChannelsBindingSource, "ChannelName", True))
        Me.txtTextChannelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextChannelName.Location = New System.Drawing.Point(12, 46)
        Me.txtTextChannelName.Name = "txtTextChannelName"
        Me.txtTextChannelName.Size = New System.Drawing.Size(613, 22)
        Me.txtTextChannelName.TabIndex = 56
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(9, 160)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(127, 16)
        Me.Label12.TabIndex = 71
        Me.Label12.Text = "Channel Destination"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(9, 116)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(103, 16)
        Me.Label13.TabIndex = 70
        Me.Label13.Text = "Channel Source"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(468, 72)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(79, 16)
        Me.Label16.TabIndex = 67
        Me.Label16.Text = "Tooltip Text"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(9, 72)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(76, 16)
        Me.Label17.TabIndex = 65
        Me.Label17.Text = "Description"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(637, 26)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(107, 16)
        Me.Label18.TabIndex = 63
        Me.Label18.Text = "Collection Name"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(9, 26)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(97, 16)
        Me.Label19.TabIndex = 61
        Me.Label19.Text = "Channel Name"
        '
        'dgvTextChannels
        '
        Me.dgvTextChannels.AllowUserToAddRows = False
        Me.dgvTextChannels.AllowUserToDeleteRows = False
        Me.dgvTextChannels.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTextChannels.AutoGenerateColumns = False
        Me.dgvTextChannels.BackgroundColor = System.Drawing.Color.Black
        Me.dgvTextChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTextChannels.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CollectionNameDataGridViewTextBoxColumn2, Me.ChannelNameDataGridViewTextBoxColumn2, Me.DescriptionDataGridViewTextBoxColumn2, Me.SourceDataGridViewTextBoxColumn2, Me.DestinationDataGridViewTextBoxColumn2, Me.OPCGroupDataGridViewTextBoxColumn2, Me.OPCItemIDDataGridViewTextBoxColumn2, Me.TooltipDataGridViewTextBoxColumn2, Me.InitialValueDataGridViewTextBoxColumn2, Me.FinalValueDataGridViewTextBoxColumn2})
        Me.dgvTextChannels.DataSource = Me.TextChannelsBindingSource
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTextChannels.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvTextChannels.Location = New System.Drawing.Point(3, 3)
        Me.dgvTextChannels.Name = "dgvTextChannels"
        Me.dgvTextChannels.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTextChannels.Size = New System.Drawing.Size(929, 378)
        Me.dgvTextChannels.TabIndex = 94
        '
        'CollectionNameDataGridViewTextBoxColumn2
        '
        Me.CollectionNameDataGridViewTextBoxColumn2.DataPropertyName = "CollectionName"
        Me.CollectionNameDataGridViewTextBoxColumn2.Frozen = True
        Me.CollectionNameDataGridViewTextBoxColumn2.HeaderText = "CollectionName"
        Me.CollectionNameDataGridViewTextBoxColumn2.Name = "CollectionNameDataGridViewTextBoxColumn2"
        '
        'ChannelNameDataGridViewTextBoxColumn2
        '
        Me.ChannelNameDataGridViewTextBoxColumn2.DataPropertyName = "ChannelName"
        Me.ChannelNameDataGridViewTextBoxColumn2.Frozen = True
        Me.ChannelNameDataGridViewTextBoxColumn2.HeaderText = "ChannelName"
        Me.ChannelNameDataGridViewTextBoxColumn2.Name = "ChannelNameDataGridViewTextBoxColumn2"
        Me.ChannelNameDataGridViewTextBoxColumn2.Width = 200
        '
        'DescriptionDataGridViewTextBoxColumn2
        '
        Me.DescriptionDataGridViewTextBoxColumn2.DataPropertyName = "Description"
        Me.DescriptionDataGridViewTextBoxColumn2.HeaderText = "Description"
        Me.DescriptionDataGridViewTextBoxColumn2.Name = "DescriptionDataGridViewTextBoxColumn2"
        Me.DescriptionDataGridViewTextBoxColumn2.Width = 200
        '
        'SourceDataGridViewTextBoxColumn2
        '
        Me.SourceDataGridViewTextBoxColumn2.DataPropertyName = "Source"
        Me.SourceDataGridViewTextBoxColumn2.HeaderText = "Source"
        Me.SourceDataGridViewTextBoxColumn2.Name = "SourceDataGridViewTextBoxColumn2"
        Me.SourceDataGridViewTextBoxColumn2.Width = 200
        '
        'DestinationDataGridViewTextBoxColumn2
        '
        Me.DestinationDataGridViewTextBoxColumn2.DataPropertyName = "Destination"
        Me.DestinationDataGridViewTextBoxColumn2.HeaderText = "Destination"
        Me.DestinationDataGridViewTextBoxColumn2.Name = "DestinationDataGridViewTextBoxColumn2"
        Me.DestinationDataGridViewTextBoxColumn2.Width = 200
        '
        'OPCGroupDataGridViewTextBoxColumn2
        '
        Me.OPCGroupDataGridViewTextBoxColumn2.DataPropertyName = "OPCGroup"
        Me.OPCGroupDataGridViewTextBoxColumn2.HeaderText = "OPCGroup"
        Me.OPCGroupDataGridViewTextBoxColumn2.Name = "OPCGroupDataGridViewTextBoxColumn2"
        '
        'OPCItemIDDataGridViewTextBoxColumn2
        '
        Me.OPCItemIDDataGridViewTextBoxColumn2.DataPropertyName = "OPCItemID"
        Me.OPCItemIDDataGridViewTextBoxColumn2.HeaderText = "OPCItemID"
        Me.OPCItemIDDataGridViewTextBoxColumn2.Name = "OPCItemIDDataGridViewTextBoxColumn2"
        Me.OPCItemIDDataGridViewTextBoxColumn2.Width = 200
        '
        'TooltipDataGridViewTextBoxColumn2
        '
        Me.TooltipDataGridViewTextBoxColumn2.DataPropertyName = "Tooltip"
        Me.TooltipDataGridViewTextBoxColumn2.HeaderText = "Tooltip"
        Me.TooltipDataGridViewTextBoxColumn2.Name = "TooltipDataGridViewTextBoxColumn2"
        '
        'InitialValueDataGridViewTextBoxColumn2
        '
        Me.InitialValueDataGridViewTextBoxColumn2.DataPropertyName = "InitialValue"
        Me.InitialValueDataGridViewTextBoxColumn2.HeaderText = "InitialValue"
        Me.InitialValueDataGridViewTextBoxColumn2.Name = "InitialValueDataGridViewTextBoxColumn2"
        '
        'FinalValueDataGridViewTextBoxColumn2
        '
        Me.FinalValueDataGridViewTextBoxColumn2.DataPropertyName = "FinalValue"
        Me.FinalValueDataGridViewTextBoxColumn2.HeaderText = "FinalValue"
        Me.FinalValueDataGridViewTextBoxColumn2.Name = "FinalValueDataGridViewTextBoxColumn2"
        '
        'XtraTabPage4
        '
        Me.XtraTabPage4.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.XtraTabPage4.Appearance.PageClient.Options.UseBackColor = True
        Me.XtraTabPage4.Controls.Add(Me.navMapping)
        Me.XtraTabPage4.Controls.Add(Me.grpMappingInformation)
        Me.XtraTabPage4.Controls.Add(Me.dgvChannelsMapping)
        Me.XtraTabPage4.Name = "XtraTabPage4"
        Me.XtraTabPage4.Size = New System.Drawing.Size(935, 693)
        Me.XtraTabPage4.Text = "Mapping"
        '
        'navMapping
        '
        Me.navMapping.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.navMapping.DataSource = Me.HMIChannelsMappingBindingSource
        Me.navMapping.Location = New System.Drawing.Point(3, 665)
        Me.navMapping.Name = "navMapping"
        Me.navMapping.Size = New System.Drawing.Size(373, 24)
        Me.navMapping.StyleController = Me.StyleController1
        Me.navMapping.TabIndex = 99
        Me.navMapping.Text = "DataNavigator3"
        '
        'grpMappingInformation
        '
        Me.grpMappingInformation.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.grpMappingInformation.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.grpMappingInformation.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.grpMappingInformation.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.grpMappingInformation.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMappingInformation.Appearance.ForeColor = System.Drawing.Color.Silver
        Me.grpMappingInformation.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grpMappingInformation.Appearance.Options.UseBackColor = True
        Me.grpMappingInformation.Appearance.Options.UseBorderColor = True
        Me.grpMappingInformation.Appearance.Options.UseFont = True
        Me.grpMappingInformation.Appearance.Options.UseForeColor = True
        Me.grpMappingInformation.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMappingInformation.AppearanceCaption.ForeColor = System.Drawing.Color.White
        Me.grpMappingInformation.AppearanceCaption.Options.UseFont = True
        Me.grpMappingInformation.AppearanceCaption.Options.UseForeColor = True
        Me.grpMappingInformation.Controls.Add(Me.txtMappingNote)
        Me.grpMappingInformation.Controls.Add(Me.lblMappingNote)
        Me.grpMappingInformation.Controls.Add(Me.txtMappingFVName)
        Me.grpMappingInformation.Controls.Add(Me.cboMappingCollectionName)
        Me.grpMappingInformation.Controls.Add(Me.txtMappingChannelName)
        Me.grpMappingInformation.Controls.Add(Me.Label6)
        Me.grpMappingInformation.Controls.Add(Me.Label7)
        Me.grpMappingInformation.Controls.Add(Me.Label8)
        Me.grpMappingInformation.Location = New System.Drawing.Point(3, 451)
        Me.grpMappingInformation.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.grpMappingInformation.LookAndFeel.UseDefaultLookAndFeel = False
        Me.grpMappingInformation.Name = "grpMappingInformation"
        Me.grpMappingInformation.Size = New System.Drawing.Size(929, 208)
        Me.grpMappingInformation.TabIndex = 98
        Me.grpMappingInformation.Text = "Mapping Information"
        '
        'txtMappingNote
        '
        Me.txtMappingNote.BackColor = System.Drawing.Color.LightGray
        Me.txtMappingNote.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.HMIChannelsMappingBindingSource, "Note", True))
        Me.txtMappingNote.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMappingNote.Location = New System.Drawing.Point(12, 137)
        Me.txtMappingNote.Multiline = True
        Me.txtMappingNote.Name = "txtMappingNote"
        Me.txtMappingNote.Size = New System.Drawing.Size(904, 63)
        Me.txtMappingNote.TabIndex = 66
        '
        'lblMappingNote
        '
        Me.lblMappingNote.AutoSize = True
        Me.lblMappingNote.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMappingNote.Location = New System.Drawing.Point(9, 118)
        Me.lblMappingNote.Name = "lblMappingNote"
        Me.lblMappingNote.Size = New System.Drawing.Size(37, 16)
        Me.lblMappingNote.TabIndex = 67
        Me.lblMappingNote.Text = "Note"
        '
        'txtMappingFVName
        '
        Me.txtMappingFVName.BackColor = System.Drawing.Color.LightGray
        Me.txtMappingFVName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.HMIChannelsMappingBindingSource, "FVName", True))
        Me.txtMappingFVName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMappingFVName.Location = New System.Drawing.Point(12, 91)
        Me.txtMappingFVName.Name = "txtMappingFVName"
        Me.txtMappingFVName.Size = New System.Drawing.Size(445, 22)
        Me.txtMappingFVName.TabIndex = 64
        '
        'cboMappingCollectionName
        '
        Me.cboMappingCollectionName.BackColor = System.Drawing.Color.LightGray
        Me.cboMappingCollectionName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.HMIChannelsMappingBindingSource, "CollectionName", True))
        Me.cboMappingCollectionName.DataSource = Me.m_UutDynamicChannelCollectionCollection
        Me.cboMappingCollectionName.DisplayMember = "CollectionName"
        Me.cboMappingCollectionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMappingCollectionName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMappingCollectionName.FormattingEnabled = True
        Me.cboMappingCollectionName.Location = New System.Drawing.Point(640, 46)
        Me.cboMappingCollectionName.Name = "cboMappingCollectionName"
        Me.cboMappingCollectionName.Size = New System.Drawing.Size(276, 24)
        Me.cboMappingCollectionName.TabIndex = 62
        Me.cboMappingCollectionName.ValueMember = "Id"
        '
        'txtMappingChannelName
        '
        Me.txtMappingChannelName.BackColor = System.Drawing.Color.LightGray
        Me.txtMappingChannelName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.HMIChannelsMappingBindingSource, "ChannelName", True))
        Me.txtMappingChannelName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMappingChannelName.Location = New System.Drawing.Point(12, 46)
        Me.txtMappingChannelName.Name = "txtMappingChannelName"
        Me.txtMappingChannelName.Size = New System.Drawing.Size(613, 22)
        Me.txtMappingChannelName.TabIndex = 56
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 16)
        Me.Label6.TabIndex = 65
        Me.Label6.Text = "FV Name"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(637, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(107, 16)
        Me.Label7.TabIndex = 63
        Me.Label7.Text = "Collection Name"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(9, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 16)
        Me.Label8.TabIndex = 61
        Me.Label8.Text = "Channel Name"
        '
        'dgvChannelsMapping
        '
        Me.dgvChannelsMapping.AllowUserToAddRows = False
        Me.dgvChannelsMapping.AllowUserToDeleteRows = False
        Me.dgvChannelsMapping.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvChannelsMapping.AutoGenerateColumns = False
        Me.dgvChannelsMapping.BackgroundColor = System.Drawing.Color.Black
        Me.dgvChannelsMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChannelsMapping.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CollectionNameDataGridViewTextBoxColumn3, Me.ChannelNameDataGridViewTextBoxColumn3, Me.FVNameDataGridViewTextBoxColumn, Me.UnavailableDataGridViewTextBoxColumn, Me.NoteDataGridViewTextBoxColumn})
        Me.dgvChannelsMapping.DataSource = Me.HMIChannelsMappingBindingSource
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvChannelsMapping.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvChannelsMapping.Location = New System.Drawing.Point(3, 3)
        Me.dgvChannelsMapping.Name = "dgvChannelsMapping"
        Me.dgvChannelsMapping.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvChannelsMapping.Size = New System.Drawing.Size(929, 442)
        Me.dgvChannelsMapping.TabIndex = 97
        '
        'CollectionNameDataGridViewTextBoxColumn3
        '
        Me.CollectionNameDataGridViewTextBoxColumn3.DataPropertyName = "CollectionName"
        Me.CollectionNameDataGridViewTextBoxColumn3.HeaderText = "CollectionName"
        Me.CollectionNameDataGridViewTextBoxColumn3.Name = "CollectionNameDataGridViewTextBoxColumn3"
        '
        'ChannelNameDataGridViewTextBoxColumn3
        '
        Me.ChannelNameDataGridViewTextBoxColumn3.DataPropertyName = "ChannelName"
        Me.ChannelNameDataGridViewTextBoxColumn3.HeaderText = "ChannelName"
        Me.ChannelNameDataGridViewTextBoxColumn3.Name = "ChannelNameDataGridViewTextBoxColumn3"
        Me.ChannelNameDataGridViewTextBoxColumn3.Width = 200
        '
        'FVNameDataGridViewTextBoxColumn
        '
        Me.FVNameDataGridViewTextBoxColumn.DataPropertyName = "FVName"
        Me.FVNameDataGridViewTextBoxColumn.HeaderText = "FVName"
        Me.FVNameDataGridViewTextBoxColumn.Name = "FVNameDataGridViewTextBoxColumn"
        Me.FVNameDataGridViewTextBoxColumn.Width = 200
        '
        'UnavailableDataGridViewTextBoxColumn
        '
        Me.UnavailableDataGridViewTextBoxColumn.DataPropertyName = "Unavailable"
        Me.UnavailableDataGridViewTextBoxColumn.HeaderText = "Unavailable"
        Me.UnavailableDataGridViewTextBoxColumn.Name = "UnavailableDataGridViewTextBoxColumn"
        Me.UnavailableDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.UnavailableDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'NoteDataGridViewTextBoxColumn
        '
        Me.NoteDataGridViewTextBoxColumn.DataPropertyName = "Note"
        Me.NoteDataGridViewTextBoxColumn.HeaderText = "Note"
        Me.NoteDataGridViewTextBoxColumn.Name = "NoteDataGridViewTextBoxColumn"
        Me.NoteDataGridViewTextBoxColumn.Width = 300
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(885, 853)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.StyleController = Me.StyleController1
        Me.btnClose.TabIndex = 117
        Me.btnClose.Text = "Close"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(804, 853)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.StyleController = Me.StyleController1
        Me.btnCancel.TabIndex = 116
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(723, 853)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.StyleController = Me.StyleController1
        Me.btnSave.TabIndex = 115
        Me.btnSave.Text = "Save"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial Black", 26.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(298, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(303, 50)
        Me.Label1.TabIndex = 114
        Me.Label1.Text = "Channel Editor"
        '
        'lblEngineModel
        '
        Me.lblEngineModel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lblEngineModel.AutoSize = True
        Me.lblEngineModel.BackColor = System.Drawing.Color.Transparent
        Me.lblEngineModel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblEngineModel.ForeColor = System.Drawing.Color.White
        Me.lblEngineModel.Location = New System.Drawing.Point(291, 87)
        Me.lblEngineModel.Name = "lblEngineModel"
        Me.lblEngineModel.Size = New System.Drawing.Size(103, 16)
        Me.lblEngineModel.TabIndex = 113
        Me.lblEngineModel.Text = "Engine Model"
        '
        'cboEngineModel
        '
        Me.cboEngineModel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cboEngineModel.DisplayMember = "Id"
        Me.cboEngineModel.FormattingEnabled = True
        Me.cboEngineModel.Location = New System.Drawing.Point(397, 86)
        Me.cboEngineModel.Name = "cboEngineModel"
        Me.cboEngineModel.Size = New System.Drawing.Size(204, 21)
        Me.cboEngineModel.TabIndex = 112
        Me.cboEngineModel.ValueMember = "Id"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(6, 871)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(215, 13)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "© 2018 CEL Aerospace Test Equipment Ltd"
        '
        'ChannelEditorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(976, 887)
        Me.Controls.Add(Me.GroupControl2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ChannelEditorForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Channel Editor"
        CType(Me.BooleanChannelsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BooleanStateBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericChannelsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EngineeringUnitBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextChannelsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HMIChannelsMappingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.dgvBooleanChannels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.grpNumericChannelInformation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpNumericChannelInformation.ResumeLayout(False)
        Me.grpNumericChannelInformation.PerformLayout()
        CType(Me.grpNumericThresholdInformation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpNumericThresholdInformation.ResumeLayout(False)
        Me.grpNumericThresholdInformation.PerformLayout()
        CType(Me.ColorPickEdit5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ColorPickEdit4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ColorPickEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ColorPickEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ColorPickEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvNumericChannels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage3.ResumeLayout(False)
        CType(Me.grpTextChannels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTextChannels.ResumeLayout(False)
        Me.grpTextChannels.PerformLayout()
        CType(Me.dgvTextChannels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage4.ResumeLayout(False)
        CType(Me.grpMappingInformation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMappingInformation.ResumeLayout(False)
        Me.grpMappingInformation.PerformLayout()
        CType(Me.dgvChannelsMapping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_UutCollection As CEL.Data.BusinessObjects.UutCollection
    Friend WithEvents BooleanChannelsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents NumericChannelsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TextChannelsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents m_UutDynamicChannelCollectionCollection As CEL.Data.BusinessObjects.UUTDynamicChannelCollectionCollection
    Friend WithEvents m_HmiBooleanChannelsCollection As CEL.Data.BusinessObjects.HMIBooleanChannelsCollection
    Friend WithEvents m_HmiNumericChannelsCollection As CEL.Data.BusinessObjects.HMINumericChannelsCollection
    Friend WithEvents m_HmiTextChannelsCollection As CEL.Data.BusinessObjects.HMITextChannelsCollection
    Friend WithEvents m_EngineeringUnitCollection As CEL.Data.BusinessObjects.EngineeringUnitCollection
    Friend WithEvents EngineeringUnitBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents m_BooleanStateCollection As CEL.Data.BusinessObjects.BooleanStateCollection
    Friend WithEvents BooleanStateBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents m_HmiChannelsMappingCollection As CEL.Data.BusinessObjects.HMIChannelsMappingCollection
    Friend WithEvents HMIChannelsMappingBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents navBoolean As DevExpress.XtraEditors.DataNavigator
    Friend WithEvents dgvBooleanChannels As System.Windows.Forms.DataGridView
    Friend WithEvents CollectionNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChannelNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SourceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DestinationDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ResetLatchDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpressionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OPCGroupDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OPCItemIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TooltipDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FalseTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TrueTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FalseMessageTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TrueMessageTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SoundMessageDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InitialValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents FinalValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtBooleanChannelDestination As System.Windows.Forms.TextBox
    Friend WithEvents txtBooleanChannelSource As System.Windows.Forms.TextBox
    Friend WithEvents txtBooleanTooltip As System.Windows.Forms.TextBox
    Friend WithEvents txtBooleanDescription As System.Windows.Forms.TextBox
    Friend WithEvents cboCollectionNameBoolean As System.Windows.Forms.ComboBox
    Friend WithEvents txtBooleanChannelName As System.Windows.Forms.TextBox
    Friend WithEvents lblBooleanChannelDestination As System.Windows.Forms.Label
    Friend WithEvents lblBooleanTooltip As System.Windows.Forms.Label
    Friend WithEvents lblBooleanDescription As System.Windows.Forms.Label
    Friend WithEvents lblBooleanCollectionName As System.Windows.Forms.Label
    Friend WithEvents lblBooleanChannelName As System.Windows.Forms.Label
    Friend WithEvents lblBooleanChannelSource As System.Windows.Forms.Label
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents navText As DevExpress.XtraEditors.DataNavigator
    Friend WithEvents grpTextChannels As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtTextChannelDestination As System.Windows.Forms.TextBox
    Friend WithEvents txtTextChannelSource As System.Windows.Forms.TextBox
    Friend WithEvents txtTextTooltip As System.Windows.Forms.TextBox
    Friend WithEvents txtTextDescription As System.Windows.Forms.TextBox
    Friend WithEvents cboTextCollectionName As System.Windows.Forms.ComboBox
    Friend WithEvents txtTextChannelName As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents dgvTextChannels As System.Windows.Forms.DataGridView
    Friend WithEvents CollectionNameDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChannelNameDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SourceDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DestinationDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OPCGroupDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OPCItemIDDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TooltipDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InitialValueDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FinalValueDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents XtraTabPage4 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents navMapping As DevExpress.XtraEditors.DataNavigator
    Friend WithEvents grpMappingInformation As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtMappingNote As System.Windows.Forms.TextBox
    Friend WithEvents lblMappingNote As System.Windows.Forms.Label
    Friend WithEvents txtMappingFVName As System.Windows.Forms.TextBox
    Friend WithEvents cboMappingCollectionName As System.Windows.Forms.ComboBox
    Friend WithEvents txtMappingChannelName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dgvChannelsMapping As System.Windows.Forms.DataGridView
    Friend WithEvents CollectionNameDataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChannelNameDataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FVNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnavailableDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NoteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblEngineModel As System.Windows.Forms.Label
    Friend WithEvents cboEngineModel As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents txtBooleanExpression As System.Windows.Forms.TextBox
    Friend WithEvents txtBooleanTrueText As System.Windows.Forms.TextBox
    Friend WithEvents txtBooleanFalseText As System.Windows.Forms.TextBox
    Friend WithEvents lblBooleanTrueText As System.Windows.Forms.Label
    Friend WithEvents lblBooleanFalseText As System.Windows.Forms.Label
    Friend WithEvents lblBooleanExpression As System.Windows.Forms.Label
    Friend WithEvents cboBooleanFinalValue As System.Windows.Forms.ComboBox
    Friend WithEvents cboBooleanInitialValue As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents m_BooleanStateCollection2 As CEL.Data.BusinessObjects.BooleanStateCollection
    Friend WithEvents txtTextFinalValue As System.Windows.Forms.TextBox
    Friend WithEvents txtTextInitialValue As System.Windows.Forms.TextBox
    Friend WithEvents lblTextFinalValue As System.Windows.Forms.Label
    Friend WithEvents lblTextInitialValue As System.Windows.Forms.Label
    Friend WithEvents txtBooleanOpcItemId As System.Windows.Forms.TextBox
    Friend WithEvents lblBooleanOpcItemId As System.Windows.Forms.Label
    Friend WithEvents txtBooleanOpcGroup As System.Windows.Forms.TextBox
    Friend WithEvents lblBooleanOpcGroup As System.Windows.Forms.Label
    Friend WithEvents txtTextOpcItemId As System.Windows.Forms.TextBox
    Friend WithEvents lblTextOpcItemId As System.Windows.Forms.Label
    Friend WithEvents txtTextOpcGroup As System.Windows.Forms.TextBox
    Friend WithEvents lblTextOpcGroup As System.Windows.Forms.Label
    Friend WithEvents grpNumericChannelInformation As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtNumericOpcItemId As System.Windows.Forms.TextBox
    Friend WithEvents lblNumericOpcItemId As System.Windows.Forms.Label
    Friend WithEvents txtNumericOpcGroup As System.Windows.Forms.TextBox
    Friend WithEvents lblNumericOpcGroup As System.Windows.Forms.Label
    Friend WithEvents btnViewThresholdSection As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkNumericPersistent As System.Windows.Forms.CheckBox
    Friend WithEvents txtNumericFinalValue As System.Windows.Forms.TextBox
    Friend WithEvents txtNumericInitialValue As System.Windows.Forms.TextBox
    Friend WithEvents lblNumericFinalValue As System.Windows.Forms.Label
    Friend WithEvents lblNumericInitialValue As System.Windows.Forms.Label
    Friend WithEvents txtNumericEngUnit As System.Windows.Forms.ComboBox
    Friend WithEvents txtNumericMaximum As System.Windows.Forms.TextBox
    Friend WithEvents txtNumericNbDecimal As System.Windows.Forms.TextBox
    Friend WithEvents txtNumericChannelDestination As System.Windows.Forms.TextBox
    Friend WithEvents txtNumericChannelSource As System.Windows.Forms.TextBox
    Friend WithEvents txtNumericTooltip As System.Windows.Forms.TextBox
    Friend WithEvents txtNumericDescription As System.Windows.Forms.TextBox
    Friend WithEvents cboCollectionNameNumeric As System.Windows.Forms.ComboBox
    Friend WithEvents txtNumericExpression As System.Windows.Forms.TextBox
    Friend WithEvents txtNumericMinimum As System.Windows.Forms.TextBox
    Friend WithEvents txtNumericFormat As System.Windows.Forms.TextBox
    Friend WithEvents txtNumericChannelName As System.Windows.Forms.TextBox
    Friend WithEvents lblNumericExpression As System.Windows.Forms.Label
    Friend WithEvents lblNumericDescription As System.Windows.Forms.Label
    Friend WithEvents lblNumericCollectionName As System.Windows.Forms.Label
    Friend WithEvents lblNumericChannelName As System.Windows.Forms.Label
    Friend WithEvents lblNumericEngUnit As System.Windows.Forms.Label
    Friend WithEvents lblNumericMaximum As System.Windows.Forms.Label
    Friend WithEvents lblNumericNbDecimal As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblNumericChannelSource As System.Windows.Forms.Label
    Friend WithEvents lblNumericMinimum As System.Windows.Forms.Label
    Friend WithEvents lblNumericFormat As System.Windows.Forms.Label
    Friend WithEvents lblNumericTooltiptext As System.Windows.Forms.Label
    Friend WithEvents grpNumericThresholdInformation As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkNumericLowTriggerEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkNumericHighTriggerEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents lblNumericLowTrigger As System.Windows.Forms.Label
    Friend WithEvents txtNumericLowTriggerValue As System.Windows.Forms.TextBox
    Friend WithEvents txtNumericHighTriggerValue As System.Windows.Forms.TextBox
    Friend WithEvents lblNumericTrigger As System.Windows.Forms.Label
    Friend WithEvents ColorPickEdit5 As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents ColorPickEdit4 As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents ColorPickEdit3 As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents ColorPickEdit2 As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents ColorPickEdit1 As DevExpress.XtraEditors.ColorPickEdit
    Friend WithEvents txtLowLowMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtLowMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtHighMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtHighHighMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtLowLowColor As System.Windows.Forms.TextBox
    Friend WithEvents txtLowColor As System.Windows.Forms.TextBox
    Friend WithEvents txtNormalColor As System.Windows.Forms.TextBox
    Friend WithEvents txtHighColor As System.Windows.Forms.TextBox
    Friend WithEvents txtHighHighColor As System.Windows.Forms.TextBox
    Friend WithEvents txtLowLowChannelSource As System.Windows.Forms.TextBox
    Friend WithEvents txtLowChannelSource As System.Windows.Forms.TextBox
    Friend WithEvents txtHighChannelSource As System.Windows.Forms.TextBox
    Friend WithEvents txtHighHighChannelSource As System.Windows.Forms.TextBox
    Friend WithEvents txtLowLowValue As System.Windows.Forms.TextBox
    Friend WithEvents lblLowLow As System.Windows.Forms.Label
    Friend WithEvents txtLowValue As System.Windows.Forms.TextBox
    Friend WithEvents lblLow As System.Windows.Forms.Label
    Friend WithEvents txtNormalValue As System.Windows.Forms.TextBox
    Friend WithEvents lblNormal As System.Windows.Forms.Label
    Friend WithEvents txtHighValue As System.Windows.Forms.TextBox
    Friend WithEvents lblHigh As System.Windows.Forms.Label
    Friend WithEvents txtHighHighValue As System.Windows.Forms.TextBox
    Friend WithEvents lblHighHigh As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblColor As System.Windows.Forms.Label
    Friend WithEvents lblChannelSource As System.Windows.Forms.Label
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents dgvNumericChannels As System.Windows.Forms.DataGridView
    Friend WithEvents CollectionNameDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChannelNameDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SourceDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DestinationDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpressionDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OPCGroupDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OPCItemIDDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TooltipDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DecimalDigitsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FormatDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MinimumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaximumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EngUnitIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents MeanEnabledDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MeanPeriodDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdHighHighDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdHighHighSourceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdHighHighColorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdHighHighMessageTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdHighDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdHighSourceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdHighColorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdHighMessageTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdLowLowDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdLowLowSourceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdLowLowColorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdLowLowMessageTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdLowDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdLowSourceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdLowColorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdLowMessageTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdNormalDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ThresholdNormalColorDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HighTriggerDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HighTriggerEnabledDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LowTriggerDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LowTriggerEnabledDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SpeechMessageTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InitialValueDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FinalValueDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PersistentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents navNumeric As DevExpress.XtraEditors.DataNavigator
End Class

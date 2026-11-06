<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HmiNumericChannelForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HmiNumericChannelForm))
        Me.lblNumericChannel = New System.Windows.Forms.Label()
        Me.dgvNumericChannels = New System.Windows.Forms.DataGridView()
        Me.CollectionNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChannelNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TempNoteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DestinationDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpressionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OPCGroupDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OPCItemIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TooltipDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoteDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DecimalDigitsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FormatDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MinimumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaximumDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EngUnitIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.FormulaVisibleForAdminOnlyDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InitialValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FinalValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UnavailableDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HMINumericChannelsCollectionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblEngineSelection = New System.Windows.Forms.Label()
        Me.cboEngine = New System.Windows.Forms.ComboBox()
        CType(Me.dgvNumericChannels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HMINumericChannelsCollectionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblNumericChannel
        '
        Me.lblNumericChannel.AutoSize = True
        Me.lblNumericChannel.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumericChannel.ForeColor = System.Drawing.Color.White
        Me.lblNumericChannel.Location = New System.Drawing.Point(434, 25)
        Me.lblNumericChannel.Name = "lblNumericChannel"
        Me.lblNumericChannel.Size = New System.Drawing.Size(297, 37)
        Me.lblNumericChannel.TabIndex = 0
        Me.lblNumericChannel.Text = "Numeric Channels"
        '
        'dgvNumericChannels
        '
        Me.dgvNumericChannels.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvNumericChannels.AutoGenerateColumns = False
        Me.dgvNumericChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNumericChannels.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CollectionNameDataGridViewTextBoxColumn, Me.ChannelNameDataGridViewTextBoxColumn, Me.DescriptionDataGridViewTextBoxColumn, Me.TempNoteDataGridViewTextBoxColumn, Me.SourceDataGridViewTextBoxColumn, Me.DestinationDataGridViewTextBoxColumn, Me.ExpressionDataGridViewTextBoxColumn, Me.OPCGroupDataGridViewTextBoxColumn, Me.OPCItemIDDataGridViewTextBoxColumn, Me.TooltipDataGridViewTextBoxColumn, Me.NoteDataGridViewTextBoxColumn, Me.DecimalDigitsDataGridViewTextBoxColumn, Me.FormatDataGridViewTextBoxColumn, Me.MinimumDataGridViewTextBoxColumn, Me.MaximumDataGridViewTextBoxColumn, Me.EngUnitIDDataGridViewTextBoxColumn, Me.MeanEnabledDataGridViewTextBoxColumn, Me.MeanPeriodDataGridViewTextBoxColumn, Me.ThresholdHighHighDataGridViewTextBoxColumn, Me.ThresholdHighHighSourceDataGridViewTextBoxColumn, Me.ThresholdHighHighColorDataGridViewTextBoxColumn, Me.ThresholdHighHighMessageTextDataGridViewTextBoxColumn, Me.ThresholdHighDataGridViewTextBoxColumn, Me.ThresholdHighSourceDataGridViewTextBoxColumn, Me.ThresholdHighColorDataGridViewTextBoxColumn, Me.ThresholdHighMessageTextDataGridViewTextBoxColumn, Me.ThresholdLowLowDataGridViewTextBoxColumn, Me.ThresholdLowLowSourceDataGridViewTextBoxColumn, Me.ThresholdLowLowColorDataGridViewTextBoxColumn, Me.ThresholdLowLowMessageTextDataGridViewTextBoxColumn, Me.ThresholdLowDataGridViewTextBoxColumn, Me.ThresholdLowSourceDataGridViewTextBoxColumn, Me.ThresholdLowColorDataGridViewTextBoxColumn, Me.ThresholdLowMessageTextDataGridViewTextBoxColumn, Me.ThresholdNormalDataGridViewTextBoxColumn, Me.ThresholdNormalColorDataGridViewTextBoxColumn, Me.HighTriggerDataGridViewTextBoxColumn, Me.HighTriggerEnabledDataGridViewTextBoxColumn, Me.LowTriggerDataGridViewTextBoxColumn, Me.LowTriggerEnabledDataGridViewTextBoxColumn, Me.SpeechMessageTextDataGridViewTextBoxColumn, Me.FormulaVisibleForAdminOnlyDataGridViewTextBoxColumn, Me.InitialValueDataGridViewTextBoxColumn, Me.FinalValueDataGridViewTextBoxColumn, Me.UnavailableDataGridViewTextBoxColumn})
        Me.dgvNumericChannels.DataSource = Me.HMINumericChannelsCollectionBindingSource
        Me.dgvNumericChannels.Location = New System.Drawing.Point(12, 76)
        Me.dgvNumericChannels.Name = "dgvNumericChannels"
        Me.dgvNumericChannels.Size = New System.Drawing.Size(1205, 432)
        Me.dgvNumericChannels.TabIndex = 1
        '
        'CollectionNameDataGridViewTextBoxColumn
        '
        Me.CollectionNameDataGridViewTextBoxColumn.DataPropertyName = "CollectionName"
        Me.CollectionNameDataGridViewTextBoxColumn.HeaderText = "CollectionName"
        Me.CollectionNameDataGridViewTextBoxColumn.Name = "CollectionNameDataGridViewTextBoxColumn"
        '
        'ChannelNameDataGridViewTextBoxColumn
        '
        Me.ChannelNameDataGridViewTextBoxColumn.DataPropertyName = "ChannelName"
        Me.ChannelNameDataGridViewTextBoxColumn.HeaderText = "ChannelName"
        Me.ChannelNameDataGridViewTextBoxColumn.Name = "ChannelNameDataGridViewTextBoxColumn"
        '
        'DescriptionDataGridViewTextBoxColumn
        '
        Me.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.HeaderText = "Description"
        Me.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn"
        '
        'TempNoteDataGridViewTextBoxColumn
        '
        Me.TempNoteDataGridViewTextBoxColumn.DataPropertyName = "TempNote"
        Me.TempNoteDataGridViewTextBoxColumn.HeaderText = "TempNote"
        Me.TempNoteDataGridViewTextBoxColumn.Name = "TempNoteDataGridViewTextBoxColumn"
        '
        'SourceDataGridViewTextBoxColumn
        '
        Me.SourceDataGridViewTextBoxColumn.DataPropertyName = "Source"
        Me.SourceDataGridViewTextBoxColumn.HeaderText = "Source"
        Me.SourceDataGridViewTextBoxColumn.Name = "SourceDataGridViewTextBoxColumn"
        '
        'DestinationDataGridViewTextBoxColumn
        '
        Me.DestinationDataGridViewTextBoxColumn.DataPropertyName = "Destination"
        Me.DestinationDataGridViewTextBoxColumn.HeaderText = "Destination"
        Me.DestinationDataGridViewTextBoxColumn.Name = "DestinationDataGridViewTextBoxColumn"
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
        '
        'TooltipDataGridViewTextBoxColumn
        '
        Me.TooltipDataGridViewTextBoxColumn.DataPropertyName = "Tooltip"
        Me.TooltipDataGridViewTextBoxColumn.HeaderText = "Tooltip"
        Me.TooltipDataGridViewTextBoxColumn.Name = "TooltipDataGridViewTextBoxColumn"
        '
        'NoteDataGridViewTextBoxColumn
        '
        Me.NoteDataGridViewTextBoxColumn.DataPropertyName = "Note"
        Me.NoteDataGridViewTextBoxColumn.HeaderText = "Note"
        Me.NoteDataGridViewTextBoxColumn.Name = "NoteDataGridViewTextBoxColumn"
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
        Me.EngUnitIDDataGridViewTextBoxColumn.HeaderText = "EngUnitID"
        Me.EngUnitIDDataGridViewTextBoxColumn.Name = "EngUnitIDDataGridViewTextBoxColumn"
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
        'FormulaVisibleForAdminOnlyDataGridViewTextBoxColumn
        '
        Me.FormulaVisibleForAdminOnlyDataGridViewTextBoxColumn.DataPropertyName = "FormulaVisibleForAdminOnly"
        Me.FormulaVisibleForAdminOnlyDataGridViewTextBoxColumn.HeaderText = "FormulaVisibleForAdminOnly"
        Me.FormulaVisibleForAdminOnlyDataGridViewTextBoxColumn.Name = "FormulaVisibleForAdminOnlyDataGridViewTextBoxColumn"
        '
        'InitialValueDataGridViewTextBoxColumn
        '
        Me.InitialValueDataGridViewTextBoxColumn.DataPropertyName = "InitialValue"
        Me.InitialValueDataGridViewTextBoxColumn.HeaderText = "InitialValue"
        Me.InitialValueDataGridViewTextBoxColumn.Name = "InitialValueDataGridViewTextBoxColumn"
        '
        'FinalValueDataGridViewTextBoxColumn
        '
        Me.FinalValueDataGridViewTextBoxColumn.DataPropertyName = "FinalValue"
        Me.FinalValueDataGridViewTextBoxColumn.HeaderText = "FinalValue"
        Me.FinalValueDataGridViewTextBoxColumn.Name = "FinalValueDataGridViewTextBoxColumn"
        '
        'UnavailableDataGridViewTextBoxColumn
        '
        Me.UnavailableDataGridViewTextBoxColumn.DataPropertyName = "Unavailable"
        Me.UnavailableDataGridViewTextBoxColumn.HeaderText = "Unavailable"
        Me.UnavailableDataGridViewTextBoxColumn.Name = "UnavailableDataGridViewTextBoxColumn"
        '
        'HMINumericChannelsCollectionBindingSource
        '
        Me.HMINumericChannelsCollectionBindingSource.DataSource = GetType(CEL.Data.BusinessObjects.HMINumericChannelsCollection)
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.BindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem})
        Me.BindingNavigator1.Location = New System.Drawing.Point(0, 523)
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1.Size = New System.Drawing.Size(1229, 25)
        Me.BindingNavigator1.TabIndex = 2
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(35, 22)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'lblEngineSelection
        '
        Me.lblEngineSelection.AutoSize = True
        Me.lblEngineSelection.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEngineSelection.ForeColor = System.Drawing.Color.White
        Me.lblEngineSelection.Location = New System.Drawing.Point(997, 13)
        Me.lblEngineSelection.Name = "lblEngineSelection"
        Me.lblEngineSelection.Size = New System.Drawing.Size(167, 24)
        Me.lblEngineSelection.TabIndex = 59
        Me.lblEngineSelection.Text = "Engine Selection"
        '
        'cboEngine
        '
        Me.cboEngine.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEngine.FormattingEnabled = True
        Me.cboEngine.Location = New System.Drawing.Point(972, 40)
        Me.cboEngine.Name = "cboEngine"
        Me.cboEngine.Size = New System.Drawing.Size(227, 22)
        Me.cboEngine.TabIndex = 58
        '
        'HmiNumericChannelForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1229, 548)
        Me.Controls.Add(Me.lblEngineSelection)
        Me.Controls.Add(Me.cboEngine)
        Me.Controls.Add(Me.BindingNavigator1)
        Me.Controls.Add(Me.dgvNumericChannels)
        Me.Controls.Add(Me.lblNumericChannel)
        Me.Name = "HmiNumericChannelForm"
        Me.Text = "Numeric Channels "
        CType(Me.dgvNumericChannels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HMINumericChannelsCollectionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblNumericChannel As System.Windows.Forms.Label
    Friend WithEvents dgvNumericChannels As System.Windows.Forms.DataGridView
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CollectionNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChannelNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TempNoteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SourceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DestinationDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpressionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OPCGroupDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OPCItemIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TooltipDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoteDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DecimalDigitsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FormatDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MinimumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaximumDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EngUnitIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnitsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LoggerEnabledDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LoggerIndexDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
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
    Friend WithEvents LogReportCategorieDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LogReportOrderInCategorieDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SpeechMessageTextDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FormulaVisibleForAdminOnlyDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InitialValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FinalValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnavailableDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HMINumericChannelsCollectionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents lblEngineSelection As System.Windows.Forms.Label
    Friend WithEvents cboEngine As System.Windows.Forms.ComboBox
End Class

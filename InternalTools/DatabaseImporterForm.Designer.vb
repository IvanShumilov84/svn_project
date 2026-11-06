<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatabaseImporterForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.cboEngineList = New System.Windows.Forms.ComboBox()
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.txtDestination = New System.Windows.Forms.TextBox()
        Me.btnSelectSource = New System.Windows.Forms.Button()
        Me.btnSelectDestination = New System.Windows.Forms.Button()
        Me.lblSource = New System.Windows.Forms.Label()
        Me.lblDestination = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.lblEngine = New System.Windows.Forms.Label()
        Me.lblXmlSource = New System.Windows.Forms.Label()
        Me.btnXmlSource = New System.Windows.Forms.Button()
        Me.txtXmlSource = New System.Windows.Forms.TextBox()
        Me.btnReadXmlFile = New System.Windows.Forms.Button()
        Me.txtSubstituteEngine = New System.Windows.Forms.TextBox()
        Me.chkSubstitute = New System.Windows.Forms.CheckBox()
        Me.chkAliasNumeric = New System.Windows.Forms.CheckBox()
        Me.chkAliasBoolean = New System.Windows.Forms.CheckBox()
        Me.chkCalCulatedNumeric = New System.Windows.Forms.CheckBox()
        Me.chkCalculatedBoolean = New System.Windows.Forms.CheckBox()
        Me.chkArinc429LAbel = New System.Windows.Forms.CheckBox()
        Me.chkArinc429InDiscrete = New System.Windows.Forms.CheckBox()
        Me.chkArinc429OutDiscrete = New System.Windows.Forms.CheckBox()
        Me.chkLogReport = New System.Windows.Forms.CheckBox()
        Me.btnAddRight = New System.Windows.Forms.Button()
        Me.btnAddLeft = New System.Windows.Forms.Button()
        Me.btnReadChannelCollection = New System.Windows.Forms.Button()
        Me.lstSource = New System.Windows.Forms.ListBox()
        Me.lstDestination = New System.Windows.Forms.ListBox()
        Me.btnImportChannels = New System.Windows.Forms.Button()
        Me.btnAddAllToRight = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.ChkHMIBoolean = New System.Windows.Forms.CheckBox()
        Me.ChkHMINumeric = New System.Windows.Forms.CheckBox()
        Me.chkUUT = New System.Windows.Forms.CheckBox()
        Me.lblSelectDbFormat = New System.Windows.Forms.Label()
        Me.rdoDatabaseV1 = New System.Windows.Forms.RadioButton()
        Me.rdoDatabaseV2 = New System.Windows.Forms.RadioButton()
        Me.chkDACN1_NIDAQ_BOOL = New System.Windows.Forms.CheckBox()
        Me.txtProjectIdSource = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtProjectIdDestination = New System.Windows.Forms.TextBox()
        Me.chkDACN1_NIDAQ_REAL = New System.Windows.Forms.CheckBox()
        Me.chkDACN1_POWERDNA_REAL = New System.Windows.Forms.CheckBox()
        Me.chkDACN1_SERIAL_REAL = New System.Windows.Forms.CheckBox()
        Me.chkDACN1_SERIAL_BOOL = New System.Windows.Forms.CheckBox()
        Me.chkOmnibusboxUart = New System.Windows.Forms.CheckBox()
        Me.chkOmnibusboxUartInDiscrete = New System.Windows.Forms.CheckBox()
        Me.chkHmiChannelMapping = New System.Windows.Forms.CheckBox()
        Me.chkHmiPageMapping = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkUseLike = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(700, 146)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(169, 23)
        Me.btnImport.TabIndex = 0
        Me.btnImport.Text = "Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'cboEngineList
        '
        Me.cboEngineList.FormattingEnabled = True
        Me.cboEngineList.Location = New System.Drawing.Point(134, 146)
        Me.cboEngineList.Name = "cboEngineList"
        Me.cboEngineList.Size = New System.Drawing.Size(224, 21)
        Me.cboEngineList.TabIndex = 2
        '
        'txtSource
        '
        Me.txtSource.Location = New System.Drawing.Point(134, 53)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.Size = New System.Drawing.Size(428, 20)
        Me.txtSource.TabIndex = 5
        Me.txtSource.Text = "C:\My Projects\08618 Vector\Prj08618\TestSession.mdb"
        '
        'txtDestination
        '
        Me.txtDestination.Location = New System.Drawing.Point(134, 79)
        Me.txtDestination.Name = "txtDestination"
        Me.txtDestination.Size = New System.Drawing.Size(428, 20)
        Me.txtDestination.TabIndex = 6
        Me.txtDestination.Text = "C:\My Projects\CELDAS 200\Workbench\TestSession.mdb"
        '
        'btnSelectSource
        '
        Me.btnSelectSource.Location = New System.Drawing.Point(568, 53)
        Me.btnSelectSource.Name = "btnSelectSource"
        Me.btnSelectSource.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectSource.TabIndex = 7
        Me.btnSelectSource.Text = "Select"
        Me.btnSelectSource.UseVisualStyleBackColor = True
        '
        'btnSelectDestination
        '
        Me.btnSelectDestination.Location = New System.Drawing.Point(568, 79)
        Me.btnSelectDestination.Name = "btnSelectDestination"
        Me.btnSelectDestination.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectDestination.TabIndex = 8
        Me.btnSelectDestination.Text = "Select"
        Me.btnSelectDestination.UseVisualStyleBackColor = True
        '
        'lblSource
        '
        Me.lblSource.AutoSize = True
        Me.lblSource.Location = New System.Drawing.Point(72, 58)
        Me.lblSource.Name = "lblSource"
        Me.lblSource.Size = New System.Drawing.Size(41, 13)
        Me.lblSource.TabIndex = 9
        Me.lblSource.Text = "Source"
        '
        'lblDestination
        '
        Me.lblDestination.AutoSize = True
        Me.lblDestination.Location = New System.Drawing.Point(72, 82)
        Me.lblDestination.Name = "lblDestination"
        Me.lblDestination.Size = New System.Drawing.Size(60, 13)
        Me.lblDestination.TabIndex = 10
        Me.lblDestination.Text = "Destination"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'lblEngine
        '
        Me.lblEngine.AutoSize = True
        Me.lblEngine.Location = New System.Drawing.Point(68, 154)
        Me.lblEngine.Name = "lblEngine"
        Me.lblEngine.Size = New System.Drawing.Size(40, 13)
        Me.lblEngine.TabIndex = 11
        Me.lblEngine.Text = "Engine"
        '
        'lblXmlSource
        '
        Me.lblXmlSource.AutoSize = True
        Me.lblXmlSource.Location = New System.Drawing.Point(66, 444)
        Me.lblXmlSource.Name = "lblXmlSource"
        Me.lblXmlSource.Size = New System.Drawing.Size(66, 13)
        Me.lblXmlSource.TabIndex = 14
        Me.lblXmlSource.Text = "XML Source"
        '
        'btnXmlSource
        '
        Me.btnXmlSource.Location = New System.Drawing.Point(587, 439)
        Me.btnXmlSource.Name = "btnXmlSource"
        Me.btnXmlSource.Size = New System.Drawing.Size(75, 23)
        Me.btnXmlSource.TabIndex = 13
        Me.btnXmlSource.Text = "Select"
        Me.btnXmlSource.UseVisualStyleBackColor = True
        '
        'txtXmlSource
        '
        Me.txtXmlSource.Location = New System.Drawing.Point(144, 441)
        Me.txtXmlSource.Name = "txtXmlSource"
        Me.txtXmlSource.Size = New System.Drawing.Size(428, 20)
        Me.txtXmlSource.TabIndex = 12
        '
        'btnReadXmlFile
        '
        Me.btnReadXmlFile.Location = New System.Drawing.Point(749, 439)
        Me.btnReadXmlFile.Name = "btnReadXmlFile"
        Me.btnReadXmlFile.Size = New System.Drawing.Size(75, 23)
        Me.btnReadXmlFile.TabIndex = 15
        Me.btnReadXmlFile.Text = "Read File"
        Me.btnReadXmlFile.UseVisualStyleBackColor = True
        '
        'txtSubstituteEngine
        '
        Me.txtSubstituteEngine.Enabled = False
        Me.txtSubstituteEngine.Location = New System.Drawing.Point(134, 183)
        Me.txtSubstituteEngine.Name = "txtSubstituteEngine"
        Me.txtSubstituteEngine.Size = New System.Drawing.Size(224, 20)
        Me.txtSubstituteEngine.TabIndex = 16
        '
        'chkSubstitute
        '
        Me.chkSubstitute.AutoSize = True
        Me.chkSubstitute.Location = New System.Drawing.Point(32, 186)
        Me.chkSubstitute.Name = "chkSubstitute"
        Me.chkSubstitute.Size = New System.Drawing.Size(73, 17)
        Me.chkSubstitute.TabIndex = 18
        Me.chkSubstitute.Text = "Substitute"
        Me.chkSubstitute.UseVisualStyleBackColor = True
        '
        'chkAliasNumeric
        '
        Me.chkAliasNumeric.AutoSize = True
        Me.chkAliasNumeric.Location = New System.Drawing.Point(377, 171)
        Me.chkAliasNumeric.Name = "chkAliasNumeric"
        Me.chkAliasNumeric.Size = New System.Drawing.Size(121, 17)
        Me.chkAliasNumeric.TabIndex = 19
        Me.chkAliasNumeric.Text = "Alias Name Numeric"
        Me.chkAliasNumeric.UseVisualStyleBackColor = True
        '
        'chkAliasBoolean
        '
        Me.chkAliasBoolean.AutoSize = True
        Me.chkAliasBoolean.Location = New System.Drawing.Point(377, 148)
        Me.chkAliasBoolean.Name = "chkAliasBoolean"
        Me.chkAliasBoolean.Size = New System.Drawing.Size(121, 17)
        Me.chkAliasBoolean.TabIndex = 20
        Me.chkAliasBoolean.Text = "Alias Name Boolean"
        Me.chkAliasBoolean.UseVisualStyleBackColor = True
        '
        'chkCalCulatedNumeric
        '
        Me.chkCalCulatedNumeric.AutoSize = True
        Me.chkCalCulatedNumeric.Location = New System.Drawing.Point(377, 217)
        Me.chkCalCulatedNumeric.Name = "chkCalCulatedNumeric"
        Me.chkCalCulatedNumeric.Size = New System.Drawing.Size(148, 17)
        Me.chkCalCulatedNumeric.TabIndex = 21
        Me.chkCalCulatedNumeric.Text = "Calculated Value Numeric"
        Me.chkCalCulatedNumeric.UseVisualStyleBackColor = True
        '
        'chkCalculatedBoolean
        '
        Me.chkCalculatedBoolean.AutoSize = True
        Me.chkCalculatedBoolean.Location = New System.Drawing.Point(377, 194)
        Me.chkCalculatedBoolean.Name = "chkCalculatedBoolean"
        Me.chkCalculatedBoolean.Size = New System.Drawing.Size(148, 17)
        Me.chkCalculatedBoolean.TabIndex = 22
        Me.chkCalculatedBoolean.Text = "Calculated Value Boolean"
        Me.chkCalculatedBoolean.UseVisualStyleBackColor = True
        '
        'chkArinc429LAbel
        '
        Me.chkArinc429LAbel.AutoSize = True
        Me.chkArinc429LAbel.Location = New System.Drawing.Point(377, 246)
        Me.chkArinc429LAbel.Name = "chkArinc429LAbel"
        Me.chkArinc429LAbel.Size = New System.Drawing.Size(100, 17)
        Me.chkArinc429LAbel.TabIndex = 23
        Me.chkArinc429LAbel.Text = "Arinc 429 Label"
        Me.chkArinc429LAbel.UseVisualStyleBackColor = True
        '
        'chkArinc429InDiscrete
        '
        Me.chkArinc429InDiscrete.AutoSize = True
        Me.chkArinc429InDiscrete.Location = New System.Drawing.Point(377, 269)
        Me.chkArinc429InDiscrete.Name = "chkArinc429InDiscrete"
        Me.chkArinc429InDiscrete.Size = New System.Drawing.Size(127, 17)
        Me.chkArinc429InDiscrete.TabIndex = 24
        Me.chkArinc429InDiscrete.Text = "Arinc 429 IN Discrete"
        Me.chkArinc429InDiscrete.UseVisualStyleBackColor = True
        '
        'chkArinc429OutDiscrete
        '
        Me.chkArinc429OutDiscrete.AutoSize = True
        Me.chkArinc429OutDiscrete.Location = New System.Drawing.Point(377, 292)
        Me.chkArinc429OutDiscrete.Name = "chkArinc429OutDiscrete"
        Me.chkArinc429OutDiscrete.Size = New System.Drawing.Size(139, 17)
        Me.chkArinc429OutDiscrete.TabIndex = 25
        Me.chkArinc429OutDiscrete.Text = "Arinc 429 OUT Discrete"
        Me.chkArinc429OutDiscrete.UseVisualStyleBackColor = True
        '
        'chkLogReport
        '
        Me.chkLogReport.AutoSize = True
        Me.chkLogReport.Location = New System.Drawing.Point(554, 217)
        Me.chkLogReport.Name = "chkLogReport"
        Me.chkLogReport.Size = New System.Drawing.Size(79, 17)
        Me.chkLogReport.TabIndex = 26
        Me.chkLogReport.Text = "Log Report"
        Me.chkLogReport.UseVisualStyleBackColor = True
        '
        'btnAddRight
        '
        Me.btnAddRight.Location = New System.Drawing.Point(384, 538)
        Me.btnAddRight.Name = "btnAddRight"
        Me.btnAddRight.Size = New System.Drawing.Size(75, 23)
        Me.btnAddRight.TabIndex = 29
        Me.btnAddRight.Text = ">"
        Me.btnAddRight.UseVisualStyleBackColor = True
        '
        'btnAddLeft
        '
        Me.btnAddLeft.Location = New System.Drawing.Point(384, 596)
        Me.btnAddLeft.Name = "btnAddLeft"
        Me.btnAddLeft.Size = New System.Drawing.Size(75, 23)
        Me.btnAddLeft.TabIndex = 30
        Me.btnAddLeft.Text = "<"
        Me.btnAddLeft.UseVisualStyleBackColor = True
        '
        'btnReadChannelCollection
        '
        Me.btnReadChannelCollection.Location = New System.Drawing.Point(668, 439)
        Me.btnReadChannelCollection.Name = "btnReadChannelCollection"
        Me.btnReadChannelCollection.Size = New System.Drawing.Size(75, 23)
        Me.btnReadChannelCollection.TabIndex = 31
        Me.btnReadChannelCollection.Text = "Load"
        Me.btnReadChannelCollection.UseVisualStyleBackColor = True
        '
        'lstSource
        '
        Me.lstSource.FormattingEnabled = True
        Me.lstSource.Location = New System.Drawing.Point(12, 476)
        Me.lstSource.Name = "lstSource"
        Me.lstSource.Size = New System.Drawing.Size(324, 251)
        Me.lstSource.TabIndex = 32
        '
        'lstDestination
        '
        Me.lstDestination.FormattingEnabled = True
        Me.lstDestination.Location = New System.Drawing.Point(511, 489)
        Me.lstDestination.Name = "lstDestination"
        Me.lstDestination.Size = New System.Drawing.Size(347, 238)
        Me.lstDestination.TabIndex = 33
        '
        'btnImportChannels
        '
        Me.btnImportChannels.Enabled = False
        Me.btnImportChannels.Location = New System.Drawing.Point(652, 733)
        Me.btnImportChannels.Name = "btnImportChannels"
        Me.btnImportChannels.Size = New System.Drawing.Size(113, 23)
        Me.btnImportChannels.TabIndex = 34
        Me.btnImportChannels.Text = "Import Channels"
        Me.btnImportChannels.UseVisualStyleBackColor = True
        '
        'btnAddAllToRight
        '
        Me.btnAddAllToRight.Location = New System.Drawing.Point(384, 567)
        Me.btnAddAllToRight.Name = "btnAddAllToRight"
        Me.btnAddAllToRight.Size = New System.Drawing.Size(75, 23)
        Me.btnAddAllToRight.TabIndex = 35
        Me.btnAddAllToRight.Text = ">>"
        Me.btnAddAllToRight.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(384, 625)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 36
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'ChkHMIBoolean
        '
        Me.ChkHMIBoolean.AutoSize = True
        Me.ChkHMIBoolean.Location = New System.Drawing.Point(554, 146)
        Me.ChkHMIBoolean.Name = "ChkHMIBoolean"
        Me.ChkHMIBoolean.Size = New System.Drawing.Size(88, 17)
        Me.ChkHMIBoolean.TabIndex = 38
        Me.ChkHMIBoolean.Text = "HMI Boolean"
        Me.ChkHMIBoolean.UseVisualStyleBackColor = True
        '
        'ChkHMINumeric
        '
        Me.ChkHMINumeric.AutoSize = True
        Me.ChkHMINumeric.Location = New System.Drawing.Point(554, 169)
        Me.ChkHMINumeric.Name = "ChkHMINumeric"
        Me.ChkHMINumeric.Size = New System.Drawing.Size(88, 17)
        Me.ChkHMINumeric.TabIndex = 37
        Me.ChkHMINumeric.Text = "HMI Numeric"
        Me.ChkHMINumeric.UseVisualStyleBackColor = True
        '
        'chkUUT
        '
        Me.chkUUT.AutoSize = True
        Me.chkUUT.Location = New System.Drawing.Point(554, 192)
        Me.chkUUT.Name = "chkUUT"
        Me.chkUUT.Size = New System.Drawing.Size(49, 17)
        Me.chkUUT.TabIndex = 39
        Me.chkUUT.Text = "UUT"
        Me.chkUUT.UseVisualStyleBackColor = True
        '
        'lblSelectDbFormat
        '
        Me.lblSelectDbFormat.AutoSize = True
        Me.lblSelectDbFormat.Location = New System.Drawing.Point(690, 22)
        Me.lblSelectDbFormat.Name = "lblSelectDbFormat"
        Me.lblSelectDbFormat.Size = New System.Drawing.Size(179, 13)
        Me.lblSelectDbFormat.TabIndex = 40
        Me.lblSelectDbFormat.Text = "Selectionner le format de DB Source"
        '
        'rdoDatabaseV1
        '
        Me.rdoDatabaseV1.AutoSize = True
        Me.rdoDatabaseV1.Checked = True
        Me.rdoDatabaseV1.Location = New System.Drawing.Point(728, 38)
        Me.rdoDatabaseV1.Name = "rdoDatabaseV1"
        Me.rdoDatabaseV1.Size = New System.Drawing.Size(114, 17)
        Me.rdoDatabaseV1.TabIndex = 41
        Me.rdoDatabaseV1.TabStop = True
        Me.rdoDatabaseV1.Text = "Engine_Model (v1)"
        Me.rdoDatabaseV1.UseVisualStyleBackColor = True
        '
        'rdoDatabaseV2
        '
        Me.rdoDatabaseV2.AutoSize = True
        Me.rdoDatabaseV2.Location = New System.Drawing.Point(728, 61)
        Me.rdoDatabaseV2.Name = "rdoDatabaseV2"
        Me.rdoDatabaseV2.Size = New System.Drawing.Size(120, 17)
        Me.rdoDatabaseV2.TabIndex = 42
        Me.rdoDatabaseV2.Text = "CollectionName (v2)"
        Me.rdoDatabaseV2.UseVisualStyleBackColor = True
        '
        'chkDACN1_NIDAQ_BOOL
        '
        Me.chkDACN1_NIDAQ_BOOL.AutoSize = True
        Me.chkDACN1_NIDAQ_BOOL.Location = New System.Drawing.Point(377, 329)
        Me.chkDACN1_NIDAQ_BOOL.Name = "chkDACN1_NIDAQ_BOOL"
        Me.chkDACN1_NIDAQ_BOOL.Size = New System.Drawing.Size(137, 17)
        Me.chkDACN1_NIDAQ_BOOL.TabIndex = 43
        Me.chkDACN1_NIDAQ_BOOL.Text = "DACN1_NIDAQ_BOOL"
        Me.chkDACN1_NIDAQ_BOOL.UseVisualStyleBackColor = True
        '
        'txtProjectIdSource
        '
        Me.txtProjectIdSource.Location = New System.Drawing.Point(116, 329)
        Me.txtProjectIdSource.Name = "txtProjectIdSource"
        Me.txtProjectIdSource.Size = New System.Drawing.Size(224, 20)
        Me.txtProjectIdSource.TabIndex = 44
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 336)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "ProjectId Source"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 358)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "ProjectId Destination"
        '
        'txtProjectIdDestination
        '
        Me.txtProjectIdDestination.Location = New System.Drawing.Point(116, 355)
        Me.txtProjectIdDestination.Name = "txtProjectIdDestination"
        Me.txtProjectIdDestination.Size = New System.Drawing.Size(224, 20)
        Me.txtProjectIdDestination.TabIndex = 47
        '
        'chkDACN1_NIDAQ_REAL
        '
        Me.chkDACN1_NIDAQ_REAL.AutoSize = True
        Me.chkDACN1_NIDAQ_REAL.Location = New System.Drawing.Point(377, 352)
        Me.chkDACN1_NIDAQ_REAL.Name = "chkDACN1_NIDAQ_REAL"
        Me.chkDACN1_NIDAQ_REAL.Size = New System.Drawing.Size(136, 17)
        Me.chkDACN1_NIDAQ_REAL.TabIndex = 48
        Me.chkDACN1_NIDAQ_REAL.Text = "DACN1_NIDAQ_REAL"
        Me.chkDACN1_NIDAQ_REAL.UseVisualStyleBackColor = True
        '
        'chkDACN1_POWERDNA_REAL
        '
        Me.chkDACN1_POWERDNA_REAL.AutoSize = True
        Me.chkDACN1_POWERDNA_REAL.Location = New System.Drawing.Point(703, 352)
        Me.chkDACN1_POWERDNA_REAL.Name = "chkDACN1_POWERDNA_REAL"
        Me.chkDACN1_POWERDNA_REAL.Size = New System.Drawing.Size(166, 17)
        Me.chkDACN1_POWERDNA_REAL.TabIndex = 49
        Me.chkDACN1_POWERDNA_REAL.Text = "DACN1_POWERDNA_REAL"
        Me.chkDACN1_POWERDNA_REAL.UseVisualStyleBackColor = True
        '
        'chkDACN1_SERIAL_REAL
        '
        Me.chkDACN1_SERIAL_REAL.AutoSize = True
        Me.chkDACN1_SERIAL_REAL.Location = New System.Drawing.Point(554, 352)
        Me.chkDACN1_SERIAL_REAL.Name = "chkDACN1_SERIAL_REAL"
        Me.chkDACN1_SERIAL_REAL.Size = New System.Drawing.Size(140, 17)
        Me.chkDACN1_SERIAL_REAL.TabIndex = 50
        Me.chkDACN1_SERIAL_REAL.Text = "DACN1_SERIAL_REAL"
        Me.chkDACN1_SERIAL_REAL.UseVisualStyleBackColor = True
        '
        'chkDACN1_SERIAL_BOOL
        '
        Me.chkDACN1_SERIAL_BOOL.AutoSize = True
        Me.chkDACN1_SERIAL_BOOL.Location = New System.Drawing.Point(554, 329)
        Me.chkDACN1_SERIAL_BOOL.Name = "chkDACN1_SERIAL_BOOL"
        Me.chkDACN1_SERIAL_BOOL.Size = New System.Drawing.Size(141, 17)
        Me.chkDACN1_SERIAL_BOOL.TabIndex = 51
        Me.chkDACN1_SERIAL_BOOL.Text = "DACN1_SERIAL_BOOL"
        Me.chkDACN1_SERIAL_BOOL.UseVisualStyleBackColor = True
        '
        'chkOmnibusboxUart
        '
        Me.chkOmnibusboxUart.AutoSize = True
        Me.chkOmnibusboxUart.Location = New System.Drawing.Point(554, 246)
        Me.chkOmnibusboxUart.Name = "chkOmnibusboxUart"
        Me.chkOmnibusboxUart.Size = New System.Drawing.Size(107, 17)
        Me.chkOmnibusboxUart.TabIndex = 52
        Me.chkOmnibusboxUart.Text = "Omnibusbox Uart"
        Me.chkOmnibusboxUart.UseVisualStyleBackColor = True
        '
        'chkOmnibusboxUartInDiscrete
        '
        Me.chkOmnibusboxUartInDiscrete.AutoSize = True
        Me.chkOmnibusboxUartInDiscrete.Location = New System.Drawing.Point(554, 269)
        Me.chkOmnibusboxUartInDiscrete.Name = "chkOmnibusboxUartInDiscrete"
        Me.chkOmnibusboxUartInDiscrete.Size = New System.Drawing.Size(161, 17)
        Me.chkOmnibusboxUartInDiscrete.TabIndex = 53
        Me.chkOmnibusboxUartInDiscrete.Text = "Omnibusbox Uart In Discrete"
        Me.chkOmnibusboxUartInDiscrete.UseVisualStyleBackColor = True
        '
        'chkHmiChannelMapping
        '
        Me.chkHmiChannelMapping.AutoSize = True
        Me.chkHmiChannelMapping.Location = New System.Drawing.Point(377, 375)
        Me.chkHmiChannelMapping.Name = "chkHmiChannelMapping"
        Me.chkHmiChannelMapping.Size = New System.Drawing.Size(132, 17)
        Me.chkHmiChannelMapping.TabIndex = 54
        Me.chkHmiChannelMapping.Text = "HMI Channel Mapping"
        Me.chkHmiChannelMapping.UseVisualStyleBackColor = True
        '
        'chkHmiPageMapping
        '
        Me.chkHmiPageMapping.AutoSize = True
        Me.chkHmiPageMapping.Location = New System.Drawing.Point(554, 375)
        Me.chkHmiPageMapping.Name = "chkHmiPageMapping"
        Me.chkHmiPageMapping.Size = New System.Drawing.Size(118, 17)
        Me.chkHmiPageMapping.TabIndex = 55
        Me.chkHmiPageMapping.Text = "HMI Page Mapping"
        Me.chkHmiPageMapping.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(11, 310)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(855, 8)
        Me.GroupBox1.TabIndex = 56
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(13, 425)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(855, 8)
        Me.GroupBox2.TabIndex = 57
        Me.GroupBox2.TabStop = False
        '
        'chkUseLike
        '
        Me.chkUseLike.AutoSize = True
        Me.chkUseLike.Location = New System.Drawing.Point(728, 85)
        Me.chkUseLike.Name = "chkUseLike"
        Me.chkUseLike.Size = New System.Drawing.Size(69, 17)
        Me.chkUseLike.TabIndex = 59
        Me.chkUseLike.Text = "use LIKE"
        Me.chkUseLike.UseVisualStyleBackColor = True
        '
        'DatabaseImporterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(881, 815)
        Me.Controls.Add(Me.chkUseLike)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkHmiPageMapping)
        Me.Controls.Add(Me.chkHmiChannelMapping)
        Me.Controls.Add(Me.chkOmnibusboxUartInDiscrete)
        Me.Controls.Add(Me.chkOmnibusboxUart)
        Me.Controls.Add(Me.chkDACN1_SERIAL_BOOL)
        Me.Controls.Add(Me.chkDACN1_SERIAL_REAL)
        Me.Controls.Add(Me.chkDACN1_POWERDNA_REAL)
        Me.Controls.Add(Me.chkDACN1_NIDAQ_REAL)
        Me.Controls.Add(Me.txtProjectIdDestination)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtProjectIdSource)
        Me.Controls.Add(Me.chkDACN1_NIDAQ_BOOL)
        Me.Controls.Add(Me.rdoDatabaseV2)
        Me.Controls.Add(Me.rdoDatabaseV1)
        Me.Controls.Add(Me.lblSelectDbFormat)
        Me.Controls.Add(Me.chkUUT)
        Me.Controls.Add(Me.ChkHMIBoolean)
        Me.Controls.Add(Me.ChkHMINumeric)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnAddAllToRight)
        Me.Controls.Add(Me.btnImportChannels)
        Me.Controls.Add(Me.lstDestination)
        Me.Controls.Add(Me.lstSource)
        Me.Controls.Add(Me.btnReadChannelCollection)
        Me.Controls.Add(Me.btnAddLeft)
        Me.Controls.Add(Me.btnAddRight)
        Me.Controls.Add(Me.chkLogReport)
        Me.Controls.Add(Me.chkArinc429OutDiscrete)
        Me.Controls.Add(Me.chkArinc429InDiscrete)
        Me.Controls.Add(Me.chkArinc429LAbel)
        Me.Controls.Add(Me.chkCalculatedBoolean)
        Me.Controls.Add(Me.chkCalCulatedNumeric)
        Me.Controls.Add(Me.chkAliasBoolean)
        Me.Controls.Add(Me.chkAliasNumeric)
        Me.Controls.Add(Me.chkSubstitute)
        Me.Controls.Add(Me.txtSubstituteEngine)
        Me.Controls.Add(Me.btnReadXmlFile)
        Me.Controls.Add(Me.lblXmlSource)
        Me.Controls.Add(Me.btnXmlSource)
        Me.Controls.Add(Me.txtXmlSource)
        Me.Controls.Add(Me.lblEngine)
        Me.Controls.Add(Me.lblDestination)
        Me.Controls.Add(Me.lblSource)
        Me.Controls.Add(Me.btnSelectDestination)
        Me.Controls.Add(Me.btnSelectSource)
        Me.Controls.Add(Me.txtDestination)
        Me.Controls.Add(Me.txtSource)
        Me.Controls.Add(Me.cboEngineList)
        Me.Controls.Add(Me.btnImport)
        Me.Name = "DatabaseImporterForm"
        Me.Text = "Database Importer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents cboEngineList As System.Windows.Forms.ComboBox
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents txtDestination As System.Windows.Forms.TextBox
    Friend WithEvents btnSelectSource As System.Windows.Forms.Button
    Friend WithEvents btnSelectDestination As System.Windows.Forms.Button
    Friend WithEvents lblSource As System.Windows.Forms.Label
    Friend WithEvents lblDestination As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblEngine As System.Windows.Forms.Label
    Friend WithEvents lblXmlSource As System.Windows.Forms.Label
    Friend WithEvents btnXmlSource As System.Windows.Forms.Button
    Friend WithEvents txtXmlSource As System.Windows.Forms.TextBox
    Friend WithEvents btnReadXmlFile As System.Windows.Forms.Button
    Friend WithEvents txtSubstituteEngine As System.Windows.Forms.TextBox
    Friend WithEvents chkSubstitute As System.Windows.Forms.CheckBox
    Friend WithEvents chkAliasNumeric As System.Windows.Forms.CheckBox
    Friend WithEvents chkAliasBoolean As System.Windows.Forms.CheckBox
    Friend WithEvents chkCalCulatedNumeric As System.Windows.Forms.CheckBox
    Friend WithEvents chkCalculatedBoolean As System.Windows.Forms.CheckBox
    Friend WithEvents chkArinc429LAbel As System.Windows.Forms.CheckBox
    Friend WithEvents chkArinc429InDiscrete As System.Windows.Forms.CheckBox
    Friend WithEvents chkArinc429OutDiscrete As System.Windows.Forms.CheckBox
    Friend WithEvents chkLogReport As System.Windows.Forms.CheckBox
    Friend WithEvents btnAddRight As System.Windows.Forms.Button
    Friend WithEvents btnAddLeft As System.Windows.Forms.Button
    Friend WithEvents btnReadChannelCollection As System.Windows.Forms.Button
    Friend WithEvents lstSource As System.Windows.Forms.ListBox
    Friend WithEvents lstDestination As System.Windows.Forms.ListBox
    Friend WithEvents btnImportChannels As System.Windows.Forms.Button
    Friend WithEvents btnAddAllToRight As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ChkHMIBoolean As System.Windows.Forms.CheckBox
    Friend WithEvents ChkHMINumeric As System.Windows.Forms.CheckBox
    Friend WithEvents chkUUT As System.Windows.Forms.CheckBox
    Friend WithEvents lblSelectDbFormat As System.Windows.Forms.Label
    Friend WithEvents rdoDatabaseV1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDatabaseV2 As System.Windows.Forms.RadioButton
    Friend WithEvents chkDACN1_NIDAQ_BOOL As System.Windows.Forms.CheckBox
    Friend WithEvents txtProjectIdSource As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtProjectIdDestination As System.Windows.Forms.TextBox
    Friend WithEvents chkDACN1_NIDAQ_REAL As System.Windows.Forms.CheckBox
    Friend WithEvents chkDACN1_POWERDNA_REAL As System.Windows.Forms.CheckBox
    Friend WithEvents chkDACN1_SERIAL_REAL As System.Windows.Forms.CheckBox
    Friend WithEvents chkDACN1_SERIAL_BOOL As System.Windows.Forms.CheckBox
    Friend WithEvents chkOmnibusboxUart As System.Windows.Forms.CheckBox
    Friend WithEvents chkOmnibusboxUartInDiscrete As System.Windows.Forms.CheckBox
    Friend WithEvents chkHmiChannelMapping As System.Windows.Forms.CheckBox
    Friend WithEvents chkHmiPageMapping As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkUseLike As CheckBox
End Class

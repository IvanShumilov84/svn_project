

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.btnLogReportLayout = New System.Windows.Forms.Button()
        Me.btnHmiNumericChannels = New System.Windows.Forms.Button()
        Me.btnArchiveReportLayout = New System.Windows.Forms.Button()
        Me.BtnExitApplication = New System.Windows.Forms.Button()
        Me.btnTestProgressStepEditor = New System.Windows.Forms.Button()
        Me.btnLoadCellAssignationEditor = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnUserEditor = New System.Windows.Forms.Button()
        Me.btnUUTForm = New System.Windows.Forms.Button()
        Me.lblSetup = New System.Windows.Forms.Label()
        Me.btnChannelEditor = New System.Windows.Forms.Button()
        Me.statusBar = New System.Windows.Forms.StatusStrip()
        Me.lblStatusBar = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnHardwareConfiguration = New System.Windows.Forms.Button()
        Me.btnFlowmeterConfiguration = New System.Windows.Forms.Button()
        Me.btnConfigurationFileExport = New System.Windows.Forms.Button()
        Me.btnCSVFileUpload = New System.Windows.Forms.Button()
        Me.btnCSVFileExport = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RebootQNXToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RetrieveELogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RebootCRIOToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RetrieveCRIOLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.statusBar.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLogReportLayout
        '
        Me.btnLogReportLayout.BackColor = System.Drawing.Color.DimGray
        Me.btnLogReportLayout.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogReportLayout.Location = New System.Drawing.Point(808, 337)
        Me.btnLogReportLayout.Name = "btnLogReportLayout"
        Me.btnLogReportLayout.Size = New System.Drawing.Size(181, 45)
        Me.btnLogReportLayout.TabIndex = 3
        Me.btnLogReportLayout.Text = " Log Report Editor"
        Me.btnLogReportLayout.UseVisualStyleBackColor = False
        '
        'btnHmiNumericChannels
        '
        Me.btnHmiNumericChannels.BackColor = System.Drawing.Color.DimGray
        Me.btnHmiNumericChannels.Location = New System.Drawing.Point(91, 345)
        Me.btnHmiNumericChannels.Name = "btnHmiNumericChannels"
        Me.btnHmiNumericChannels.Size = New System.Drawing.Size(181, 45)
        Me.btnHmiNumericChannels.TabIndex = 0
        Me.btnHmiNumericChannels.Text = "HMI Numeric Channels"
        Me.btnHmiNumericChannels.UseVisualStyleBackColor = False
        Me.btnHmiNumericChannels.Visible = False
        '
        'btnArchiveReportLayout
        '
        Me.btnArchiveReportLayout.BackColor = System.Drawing.Color.DimGray
        Me.btnArchiveReportLayout.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnArchiveReportLayout.Location = New System.Drawing.Point(808, 388)
        Me.btnArchiveReportLayout.Name = "btnArchiveReportLayout"
        Me.btnArchiveReportLayout.Size = New System.Drawing.Size(181, 45)
        Me.btnArchiveReportLayout.TabIndex = 4
        Me.btnArchiveReportLayout.Text = "Archive Report Editor"
        Me.btnArchiveReportLayout.UseVisualStyleBackColor = False
        '
        'BtnExitApplication
        '
        Me.BtnExitApplication.BackColor = System.Drawing.Color.DimGray
        Me.BtnExitApplication.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExitApplication.Location = New System.Drawing.Point(808, 598)
        Me.BtnExitApplication.Name = "BtnExitApplication"
        Me.BtnExitApplication.Size = New System.Drawing.Size(181, 45)
        Me.BtnExitApplication.TabIndex = 5
        Me.BtnExitApplication.Text = "Exit Application"
        Me.BtnExitApplication.UseVisualStyleBackColor = False
        '
        'btnTestProgressStepEditor
        '
        Me.btnTestProgressStepEditor.BackColor = System.Drawing.Color.DimGray
        Me.btnTestProgressStepEditor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTestProgressStepEditor.Location = New System.Drawing.Point(808, 286)
        Me.btnTestProgressStepEditor.Name = "btnTestProgressStepEditor"
        Me.btnTestProgressStepEditor.Size = New System.Drawing.Size(181, 45)
        Me.btnTestProgressStepEditor.TabIndex = 2
        Me.btnTestProgressStepEditor.Text = "Test Progress Step Editor"
        Me.btnTestProgressStepEditor.UseVisualStyleBackColor = False
        '
        'btnLoadCellAssignationEditor
        '
        Me.btnLoadCellAssignationEditor.BackColor = System.Drawing.Color.DimGray
        Me.btnLoadCellAssignationEditor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadCellAssignationEditor.Location = New System.Drawing.Point(808, 235)
        Me.btnLoadCellAssignationEditor.Name = "btnLoadCellAssignationEditor"
        Me.btnLoadCellAssignationEditor.Size = New System.Drawing.Size(181, 45)
        Me.btnLoadCellAssignationEditor.TabIndex = 1
        Me.btnLoadCellAssignationEditor.Text = "Load Cell Assignment Editor"
        Me.btnLoadCellAssignationEditor.UseVisualStyleBackColor = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1020, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(92, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'btnUserEditor
        '
        Me.btnUserEditor.BackColor = System.Drawing.Color.DimGray
        Me.btnUserEditor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUserEditor.Location = New System.Drawing.Point(808, 184)
        Me.btnUserEditor.Name = "btnUserEditor"
        Me.btnUserEditor.Size = New System.Drawing.Size(181, 45)
        Me.btnUserEditor.TabIndex = 7
        Me.btnUserEditor.Text = "Users"
        Me.btnUserEditor.UseVisualStyleBackColor = False
        '
        'btnUUTForm
        '
        Me.btnUUTForm.BackColor = System.Drawing.Color.DimGray
        Me.btnUUTForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUUTForm.Location = New System.Drawing.Point(808, 133)
        Me.btnUUTForm.Name = "btnUUTForm"
        Me.btnUUTForm.Size = New System.Drawing.Size(181, 45)
        Me.btnUUTForm.TabIndex = 8
        Me.btnUUTForm.Text = "Maintenance"
        Me.btnUUTForm.UseVisualStyleBackColor = False
        '
        'lblSetup
        '
        Me.lblSetup.AutoSize = True
        Me.lblSetup.BackColor = System.Drawing.Color.Transparent
        Me.lblSetup.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSetup.ForeColor = System.Drawing.Color.Silver
        Me.lblSetup.Location = New System.Drawing.Point(363, 77)
        Me.lblSetup.Name = "lblSetup"
        Me.lblSetup.Size = New System.Drawing.Size(229, 22)
        Me.lblSetup.TabIndex = 9
        Me.lblSetup.Text = "Database Configuration"
        '
        'btnChannelEditor
        '
        Me.btnChannelEditor.BackColor = System.Drawing.Color.DimGray
        Me.btnChannelEditor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChannelEditor.Location = New System.Drawing.Point(808, 82)
        Me.btnChannelEditor.Name = "btnChannelEditor"
        Me.btnChannelEditor.Size = New System.Drawing.Size(181, 45)
        Me.btnChannelEditor.TabIndex = 10
        Me.btnChannelEditor.Text = "Channels Editor"
        Me.btnChannelEditor.UseVisualStyleBackColor = False
        '
        'statusBar
        '
        Me.statusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatusBar})
        Me.statusBar.Location = New System.Drawing.Point(0, 769)
        Me.statusBar.Name = "statusBar"
        Me.statusBar.Size = New System.Drawing.Size(1020, 22)
        Me.statusBar.TabIndex = 11
        '
        'lblStatusBar
        '
        Me.lblStatusBar.ForeColor = System.Drawing.Color.White
        Me.lblStatusBar.Name = "lblStatusBar"
        Me.lblStatusBar.Size = New System.Drawing.Size(121, 17)
        Me.lblStatusBar.Text = "ToolStripStatusLabel1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Silver
        Me.Label1.Location = New System.Drawing.Point(296, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(201, 34)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Test Manager"
        '
        'btnHardwareConfiguration
        '
        Me.btnHardwareConfiguration.BackColor = System.Drawing.Color.DimGray
        Me.btnHardwareConfiguration.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHardwareConfiguration.Location = New System.Drawing.Point(91, 294)
        Me.btnHardwareConfiguration.Name = "btnHardwareConfiguration"
        Me.btnHardwareConfiguration.Size = New System.Drawing.Size(181, 45)
        Me.btnHardwareConfiguration.TabIndex = 13
        Me.btnHardwareConfiguration.Text = "Hardware Configuration"
        Me.btnHardwareConfiguration.UseVisualStyleBackColor = False
        Me.btnHardwareConfiguration.Visible = False
        '
        'btnFlowmeterConfiguration
        '
        Me.btnFlowmeterConfiguration.BackColor = System.Drawing.Color.DimGray
        Me.btnFlowmeterConfiguration.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFlowmeterConfiguration.Location = New System.Drawing.Point(808, 31)
        Me.btnFlowmeterConfiguration.Name = "btnFlowmeterConfiguration"
        Me.btnFlowmeterConfiguration.Size = New System.Drawing.Size(181, 45)
        Me.btnFlowmeterConfiguration.TabIndex = 14
        Me.btnFlowmeterConfiguration.Text = "Flowmeter Configuration"
        Me.btnFlowmeterConfiguration.UseVisualStyleBackColor = False
        '
        'btnConfigurationFileExport
        '
        Me.btnConfigurationFileExport.BackColor = System.Drawing.Color.DimGray
        Me.btnConfigurationFileExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfigurationFileExport.Location = New System.Drawing.Point(808, 490)
        Me.btnConfigurationFileExport.Name = "btnConfigurationFileExport"
        Me.btnConfigurationFileExport.Size = New System.Drawing.Size(181, 45)
        Me.btnConfigurationFileExport.TabIndex = 15
        Me.btnConfigurationFileExport.Text = "Configuration File Export"
        Me.btnConfigurationFileExport.UseVisualStyleBackColor = False
        '
        'btnCSVFileUpload
        '
        Me.btnCSVFileUpload.BackColor = System.Drawing.Color.DimGray
        Me.btnCSVFileUpload.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCSVFileUpload.Location = New System.Drawing.Point(918, 439)
        Me.btnCSVFileUpload.Name = "btnCSVFileUpload"
        Me.btnCSVFileUpload.Size = New System.Drawing.Size(71, 45)
        Me.btnCSVFileUpload.TabIndex = 18
        Me.btnCSVFileUpload.Text = "and Upload"
        Me.btnCSVFileUpload.UseVisualStyleBackColor = False
        '
        'btnCSVFileExport
        '
        Me.btnCSVFileExport.BackColor = System.Drawing.Color.DimGray
        Me.btnCSVFileExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCSVFileExport.Location = New System.Drawing.Point(808, 439)
        Me.btnCSVFileExport.Name = "btnCSVFileExport"
        Me.btnCSVFileExport.Size = New System.Drawing.Size(110, 45)
        Me.btnCSVFileExport.TabIndex = 17
        Me.btnCSVFileExport.Text = "Csv File Create"
        Me.btnCSVFileExport.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.TestManager.My.Resources.Resources.avion_2_GIF1
        Me.PictureBox1.Location = New System.Drawing.Point(-1425, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(2450, 1048)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 19
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Silver
        Me.Label2.Location = New System.Drawing.Point(527, 682)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(488, 78)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = Global.TestManager.My.Resources.Resources.CEL__bleu___Beland_PMS_534CMYK_haute_définition
        Me.PictureBox2.Location = New System.Drawing.Point(553, 178)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(187, 162)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 21
        Me.PictureBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(554, 345)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(189, 22)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Building test solutions"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Trebuchet MS", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(563, 365)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(171, 20)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "www.cel-aerospace.ca"
        '
        'MenuStrip2
        '
        Me.MenuStrip2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.MenuStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolsToolStripMenuItem})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(176, 24)
        Me.MenuStrip2.TabIndex = 25
        Me.MenuStrip2.Text = "MenuStrip2"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RebootQNXToolStripMenuItem, Me.RetrieveELogToolStripMenuItem, Me.RebootCRIOToolStripMenuItem, Me.RetrieveCRIOLogToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'RebootQNXToolStripMenuItem
        '
        Me.RebootQNXToolStripMenuItem.Name = "RebootQNXToolStripMenuItem"
        Me.RebootQNXToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RebootQNXToolStripMenuItem.Text = "Reboot QNX"
        '
        'RetrieveELogToolStripMenuItem
        '
        Me.RetrieveELogToolStripMenuItem.Name = "RetrieveELogToolStripMenuItem"
        Me.RetrieveELogToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RetrieveELogToolStripMenuItem.Text = "Retrieve ELog"
        '
        'RebootCRIOToolStripMenuItem
        '
        Me.RebootCRIOToolStripMenuItem.Name = "RebootCRIOToolStripMenuItem"
        Me.RebootCRIOToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RebootCRIOToolStripMenuItem.Text = "Reboot cRIO"
        '
        'RetrieveCRIOLogToolStripMenuItem
        '
        Me.RetrieveCRIOLogToolStripMenuItem.Name = "RetrieveCRIOLogToolStripMenuItem"
        Me.RetrieveCRIOLogToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.RetrieveCRIOLogToolStripMenuItem.Text = "Retrieve cRIO Log"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.TestManager.My.Resources.Resources.fond_bleu1
        Me.ClientSize = New System.Drawing.Size(1020, 791)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnCSVFileUpload)
        Me.Controls.Add(Me.btnCSVFileExport)
        Me.Controls.Add(Me.btnConfigurationFileExport)
        Me.Controls.Add(Me.btnFlowmeterConfiguration)
        Me.Controls.Add(Me.btnHardwareConfiguration)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.statusBar)
        Me.Controls.Add(Me.btnChannelEditor)
        Me.Controls.Add(Me.lblSetup)
        Me.Controls.Add(Me.btnUUTForm)
        Me.Controls.Add(Me.btnUserEditor)
        Me.Controls.Add(Me.btnLoadCellAssignationEditor)
        Me.Controls.Add(Me.btnTestProgressStepEditor)
        Me.Controls.Add(Me.BtnExitApplication)
        Me.Controls.Add(Me.btnArchiveReportLayout)
        Me.Controls.Add(Me.btnHmiNumericChannels)
        Me.Controls.Add(Me.btnLogReportLayout)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Manager"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.statusBar.ResumeLayout(False)
        Me.statusBar.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnLogReportLayout As System.Windows.Forms.Button
    Friend WithEvents btnHmiNumericChannels As System.Windows.Forms.Button
    Friend WithEvents btnArchiveReportLayout As System.Windows.Forms.Button
    Friend WithEvents BtnExitApplication As System.Windows.Forms.Button
    Friend WithEvents btnTestProgressStepEditor As System.Windows.Forms.Button
    Friend WithEvents btnLoadCellAssignationEditor As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnUserEditor As System.Windows.Forms.Button
    Friend WithEvents btnUUTForm As System.Windows.Forms.Button
    Friend WithEvents lblSetup As System.Windows.Forms.Label
    Friend WithEvents btnChannelEditor As System.Windows.Forms.Button
    Friend WithEvents statusBar As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatusBar As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnHardwareConfiguration As System.Windows.Forms.Button
    Friend WithEvents btnFlowmeterConfiguration As System.Windows.Forms.Button
    Friend WithEvents btnConfigurationFileExport As System.Windows.Forms.Button
    Friend WithEvents btnCSVFileUpload As System.Windows.Forms.Button
    Friend WithEvents btnCSVFileExport As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents MenuStrip2 As Windows.Forms.MenuStrip
    Friend WithEvents ToolsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents RebootQNXToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents RetrieveELogToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents RebootCRIOToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents RetrieveCRIOLogToolStripMenuItem As Windows.Forms.ToolStripMenuItem
End Class


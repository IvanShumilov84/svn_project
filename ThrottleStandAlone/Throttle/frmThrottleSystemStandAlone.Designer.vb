<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmThrottleSystemStandAlone
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmThrottleSystemStandAlone))
        Me.lblCRIO_CONNECTED = New System.Windows.Forms.Label()
        Me.txtDrivesStatus = New System.Windows.Forms.TextBox()
        Me.tabThrottles = New System.Windows.Forms.TabControl()
        Me.tabPLA = New System.Windows.Forms.TabPage()
        Me.lblPlaInstructions = New System.Windows.Forms.Label()
        Me.grpPlaThrottleSecurityLimits = New System.Windows.Forms.GroupBox()
        Me.dspPLA_MIN_STOP_POS = New System.Windows.Forms.TextBox()
        Me.lblPLA_MIN_STOP_POS = New System.Windows.Forms.Label()
        Me.btnPLA_STORE_MIN_STOP_ANGLE = New System.Windows.Forms.Button()
        Me.dspPLA_IDLE_POS = New System.Windows.Forms.TextBox()
        Me.lblPLA_IDLE_POS_DONE = New System.Windows.Forms.Label()
        Me.btnPLA_STORE_IDLE_ANGLE = New System.Windows.Forms.Button()
        Me.dspPLA_MAX_STOP_POS = New System.Windows.Forms.TextBox()
        Me.lblPLA_MAX_STOP_POS_DEG = New System.Windows.Forms.Label()
        Me.btnPLA_STORE_MAX_STOP_ANGLE = New System.Windows.Forms.Button()
        Me.lblPLA_LEVER_ANGLE_READING = New System.Windows.Forms.Label()
        Me.dspPLA_SERVO_ANGLE_READING = New System.Windows.Forms.TextBox()
        Me.grpPlaServoControl = New System.Windows.Forms.GroupBox()
        Me.chkPLAHardwareEnable = New System.Windows.Forms.CheckBox()
        Me.chkPLA_ENGAGE = New System.Windows.Forms.CheckBox()
        Me.lblPLA_SERVO_READING = New System.Windows.Forms.Label()
        Me.dspPLA_SERVO_READING = New System.Windows.Forms.TextBox()
        Me.txtPlaInstructions = New System.Windows.Forms.TextBox()
        Me.grpPlaUserLeverAngles = New System.Windows.Forms.GroupBox()
        Me.lblPLA_RIGGING_DONE = New System.Windows.Forms.Label()
        Me.btnPLA_RESET = New System.Windows.Forms.Button()
        Me.btnPLA_COMPUTE = New System.Windows.Forms.Button()
        Me.btnPLA_STORE_MIN_ANGLE = New System.Windows.Forms.Button()
        Me.btnPLA_STORE_MAX_ANGLE = New System.Windows.Forms.Button()
        Me.lblPLA_MIN_RIG_POS = New System.Windows.Forms.Label()
        Me.dspPLA_MIN_RIG_POS = New System.Windows.Forms.TextBox()
        Me.dspPLA_LEVER_POS = New System.Windows.Forms.TextBox()
        Me.lblPLA_LEVER_POS = New System.Windows.Forms.Label()
        Me.lblPLA_MAX_RIG_POS = New System.Windows.Forms.Label()
        Me.dspPLA_MAX_RIG_POS = New System.Windows.Forms.TextBox()
        Me.tabCLA = New System.Windows.Forms.TabPage()
        Me.lblClaInstructions = New System.Windows.Forms.Label()
        Me.txtCLAInstructions = New System.Windows.Forms.TextBox()
        Me.grpClaThrottleSecurityLimits = New System.Windows.Forms.GroupBox()
        Me.dspCLA_MIN_STOP_POS = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCLA_STORE_MIN_STOP_ANGLE = New System.Windows.Forms.Button()
        Me.dspCLA_IDLE_POS = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnCLA_STORE_IDLE_ANGLE = New System.Windows.Forms.Button()
        Me.dspCLA_MAX_STOP_POS = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnCLA_STORE_MAX_STOP_ANGLE = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dspCLA_SERVO_ANGLE_READING = New System.Windows.Forms.TextBox()
        Me.grpClaServoControl = New System.Windows.Forms.GroupBox()
        Me.chkCLAHardwareEnable = New System.Windows.Forms.CheckBox()
        Me.chkCLA_ENGAGE = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dspCLA_SERVO_READING = New System.Windows.Forms.TextBox()
        Me.grpClaUserLeverAngles = New System.Windows.Forms.GroupBox()
        Me.lblCLA_RIGGING_DONE = New System.Windows.Forms.Label()
        Me.btnCLA_RESET = New System.Windows.Forms.Button()
        Me.btnCLA_COMPUTE = New System.Windows.Forms.Button()
        Me.btnCLA_STORE_MIN_ANGLE = New System.Windows.Forms.Button()
        Me.btnCLA_STORE_MAX_ANGLE = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dspCLA_MIN_RIG_POS = New System.Windows.Forms.TextBox()
        Me.dspCLA_LEVER_POS = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dspCLA_MAX_RIG_POS = New System.Windows.Forms.TextBox()
        Me.tabMOR = New System.Windows.Forms.TabPage()
        Me.lblMORInstructions = New System.Windows.Forms.Label()
        Me.txtMORInstructions = New System.Windows.Forms.TextBox()
        Me.grpMorThrottleSecurityLimits = New System.Windows.Forms.GroupBox()
        Me.dspMOR_MIN_STOP_POS = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnMOR_STORE_MIN_STOP_ANGLE = New System.Windows.Forms.Button()
        Me.dspMOR_IDLE_POS = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnMOR_STORE_IDLE_ANGLE = New System.Windows.Forms.Button()
        Me.dspMOR_MAX_STOP_POS = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnMOR_STORE_MAX_STOP_ANGLE = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dspMOR_SERVO_ANGLE_READING = New System.Windows.Forms.TextBox()
        Me.grpMORServoControl = New System.Windows.Forms.GroupBox()
        Me.chkMORHardwareEnable = New System.Windows.Forms.CheckBox()
        Me.chkMOR_ENGAGE = New System.Windows.Forms.CheckBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dspMOR_SERVO_READING = New System.Windows.Forms.TextBox()
        Me.grpMORUserLeverAngles = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnMOR_RESET = New System.Windows.Forms.Button()
        Me.btnMOR_COMPUTE = New System.Windows.Forms.Button()
        Me.btnMOR_STORE_MIN_ANGLE = New System.Windows.Forms.Button()
        Me.btnMOR_STORE_MAX_ANGLE = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dspMOR_MIN_RIG_POS = New System.Windows.Forms.TextBox()
        Me.dspMOR_LEVER_POS = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dspMOR_MAX_RIG_POS = New System.Windows.Forms.TextBox()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.lblCRIO_PLA_DRIVE_FAULT = New System.Windows.Forms.Label()
        Me.lblCRIO_PLA_DRIVE_WARNING = New System.Windows.Forms.Label()
        Me.lblCRIO_CLA_DRIVE_FAULT = New System.Windows.Forms.Label()
        Me.lblCRIO_CLA_DRIVE_WARNING = New System.Windows.Forms.Label()
        Me.lblCRIO_MOR_DRIVE_FAULT = New System.Windows.Forms.Label()
        Me.lblCRIO_MOR_DRIVE_WARNING = New System.Windows.Forms.Label()
        Me.lblCustomerSupport = New System.Windows.Forms.Label()
        Me.grpFeedbacksServoInstance0 = New System.Windows.Forms.GroupBox()
        Me.grpFeedbacksServoInstance1 = New System.Windows.Forms.GroupBox()
        Me.grpFeedbacksServoInstance2 = New System.Windows.Forms.GroupBox()
        Me.lblSimulationMode = New System.Windows.Forms.Label()
        Me.MenThrottleSystemStandAlone = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtSystem = New System.Windows.Forms.TextBox()
        Me.btnDrivesStatus = New System.Windows.Forms.Button()
        Me.btnSystem = New System.Windows.Forms.Button()
        Me.dspRunModePLA = New System.Windows.Forms.TextBox()
        Me.lblRunModePLA = New System.Windows.Forms.Label()
        Me.dspRunModeCLA = New System.Windows.Forms.TextBox()
        Me.lblRunModeCLA = New System.Windows.Forms.Label()
        Me.dspRunModeMOR = New System.Windows.Forms.TextBox()
        Me.lblRunModeMOR = New System.Windows.Forms.Label()
        Me.lblPLAReturnToIDLECommanded = New System.Windows.Forms.Label()
        Me.lblPLA_ThrottleRearming = New System.Windows.Forms.Label()
        Me.lblCLAReturnToIDLECommanded = New System.Windows.Forms.Label()
        Me.lblCLA_ThrottleRearming = New System.Windows.Forms.Label()
        Me.indCRIO_MOR_DRIVE_WARNING = New Bulb.LedBulb()
        Me.indCRIO_MOR_DRIVE_FAULT = New Bulb.LedBulb()
        Me.indCRIO_CLA_DRIVE_FAULT = New Bulb.LedBulb()
        Me.indCRIO_CLA_DRIVE_WARNING = New Bulb.LedBulb()
        Me.indCRIO_PLA_DRIVE_WARNING = New Bulb.LedBulb()
        Me.indCRIO_PLA_DRIVE_FAULT = New Bulb.LedBulb()
        Me.indPLA_MIN_STOP_POS_DONE = New Bulb.LedBulb()
        Me.indPLA_IDLE_POS_DONE = New Bulb.LedBulb()
        Me.indPLA_MAX_STOP_POS_DONE = New Bulb.LedBulb()
        Me.indPLA_RIGGING_DONE = New Bulb.LedBulb()
        Me.indPLA_COMPUTE_DONE = New Bulb.LedBulb()
        Me.indPLA_MIN_POS_DONE = New Bulb.LedBulb()
        Me.indPLA_MAX_POS_DONE = New Bulb.LedBulb()
        Me.indCLA_MIN_STOP_POS_DONE = New Bulb.LedBulb()
        Me.indCLA_IDLE_POS_DONE = New Bulb.LedBulb()
        Me.indCLA_MAX_STOP_POS_DONE = New Bulb.LedBulb()
        Me.indCLA_RIGGING_DONE = New Bulb.LedBulb()
        Me.indCLA_COMPUTE_DONE = New Bulb.LedBulb()
        Me.indCLA_MIN_POS_DONE = New Bulb.LedBulb()
        Me.indCLA_MAX_POS_DONE = New Bulb.LedBulb()
        Me.indMOR_MIN_STOP_POS_DONE = New Bulb.LedBulb()
        Me.indMOR_IDLE_POS_DONE = New Bulb.LedBulb()
        Me.indMOR_MAX_STOP_POS_DONE = New Bulb.LedBulb()
        Me.indMOR_RIGGING_DONE = New Bulb.LedBulb()
        Me.indMOR_COMPUTE_DONE = New Bulb.LedBulb()
        Me.indMOR_MIN_POS_DONE = New Bulb.LedBulb()
        Me.indMOR_MAX_POS_DONE = New Bulb.LedBulb()
        Me.indCRIO_CONNECTED = New Bulb.LedBulb()
        Me.indCLA_ThrottleRearming = New Bulb.LedBulb()
        Me.indCLA_ReturnToIDLECommanded = New Bulb.LedBulb()
        Me.indPLA_ThrottleRearming = New Bulb.LedBulb()
        Me.indPLA_ReturnToIDLECommanded = New Bulb.LedBulb()
        Me.tabThrottles.SuspendLayout()
        Me.tabPLA.SuspendLayout()
        Me.grpPlaThrottleSecurityLimits.SuspendLayout()
        Me.grpPlaServoControl.SuspendLayout()
        Me.grpPlaUserLeverAngles.SuspendLayout()
        Me.tabCLA.SuspendLayout()
        Me.grpClaThrottleSecurityLimits.SuspendLayout()
        Me.grpClaServoControl.SuspendLayout()
        Me.grpClaUserLeverAngles.SuspendLayout()
        Me.tabMOR.SuspendLayout()
        Me.grpMorThrottleSecurityLimits.SuspendLayout()
        Me.grpMORServoControl.SuspendLayout()
        Me.grpMORUserLeverAngles.SuspendLayout()
        Me.grpFeedbacksServoInstance0.SuspendLayout()
        Me.grpFeedbacksServoInstance1.SuspendLayout()
        Me.grpFeedbacksServoInstance2.SuspendLayout()
        Me.MenThrottleSystemStandAlone.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCRIO_CONNECTED
        '
        Me.lblCRIO_CONNECTED.AutoSize = True
        Me.lblCRIO_CONNECTED.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCRIO_CONNECTED.ForeColor = System.Drawing.SystemColors.Control
        Me.lblCRIO_CONNECTED.Location = New System.Drawing.Point(1173, 33)
        Me.lblCRIO_CONNECTED.Name = "lblCRIO_CONNECTED"
        Me.lblCRIO_CONNECTED.Size = New System.Drawing.Size(190, 29)
        Me.lblCRIO_CONNECTED.TabIndex = 2
        Me.lblCRIO_CONNECTED.Text = "cRIO Connected"
        '
        'txtDrivesStatus
        '
        Me.txtDrivesStatus.Location = New System.Drawing.Point(16, 736)
        Me.txtDrivesStatus.Multiline = True
        Me.txtDrivesStatus.Name = "txtDrivesStatus"
        Me.txtDrivesStatus.Size = New System.Drawing.Size(388, 78)
        Me.txtDrivesStatus.TabIndex = 3
        '
        'tabThrottles
        '
        Me.tabThrottles.Controls.Add(Me.tabPLA)
        Me.tabThrottles.Controls.Add(Me.tabCLA)
        Me.tabThrottles.Controls.Add(Me.tabMOR)
        Me.tabThrottles.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabThrottles.Location = New System.Drawing.Point(16, 67)
        Me.tabThrottles.Name = "tabThrottles"
        Me.tabThrottles.SelectedIndex = 0
        Me.tabThrottles.Size = New System.Drawing.Size(1446, 638)
        Me.tabThrottles.TabIndex = 4
        '
        'tabPLA
        '
        Me.tabPLA.Controls.Add(Me.lblPlaInstructions)
        Me.tabPLA.Controls.Add(Me.grpPlaThrottleSecurityLimits)
        Me.tabPLA.Controls.Add(Me.grpPlaServoControl)
        Me.tabPLA.Controls.Add(Me.txtPlaInstructions)
        Me.tabPLA.Controls.Add(Me.grpPlaUserLeverAngles)
        Me.tabPLA.Location = New System.Drawing.Point(4, 38)
        Me.tabPLA.Name = "tabPLA"
        Me.tabPLA.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPLA.Size = New System.Drawing.Size(1438, 596)
        Me.tabPLA.TabIndex = 0
        Me.tabPLA.Text = "PLA"
        Me.tabPLA.UseVisualStyleBackColor = True
        '
        'lblPlaInstructions
        '
        Me.lblPlaInstructions.AutoSize = True
        Me.lblPlaInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlaInstructions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPlaInstructions.Location = New System.Drawing.Point(28, 405)
        Me.lblPlaInstructions.Name = "lblPlaInstructions"
        Me.lblPlaInstructions.Size = New System.Drawing.Size(84, 18)
        Me.lblPlaInstructions.TabIndex = 19
        Me.lblPlaInstructions.Text = "Instructions"
        '
        'grpPlaThrottleSecurityLimits
        '
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.dspPLA_MIN_STOP_POS)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.lblPLA_MIN_STOP_POS)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.indPLA_MIN_STOP_POS_DONE)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.btnPLA_STORE_MIN_STOP_ANGLE)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.dspPLA_IDLE_POS)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.lblPLA_IDLE_POS_DONE)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.indPLA_IDLE_POS_DONE)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.btnPLA_STORE_IDLE_ANGLE)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.dspPLA_MAX_STOP_POS)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.lblPLA_MAX_STOP_POS_DEG)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.indPLA_MAX_STOP_POS_DONE)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.btnPLA_STORE_MAX_STOP_ANGLE)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.lblPLA_LEVER_ANGLE_READING)
        Me.grpPlaThrottleSecurityLimits.Controls.Add(Me.dspPLA_SERVO_ANGLE_READING)
        Me.grpPlaThrottleSecurityLimits.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpPlaThrottleSecurityLimits.Location = New System.Drawing.Point(665, 276)
        Me.grpPlaThrottleSecurityLimits.Name = "grpPlaThrottleSecurityLimits"
        Me.grpPlaThrottleSecurityLimits.Size = New System.Drawing.Size(747, 316)
        Me.grpPlaThrottleSecurityLimits.TabIndex = 3
        Me.grpPlaThrottleSecurityLimits.TabStop = False
        Me.grpPlaThrottleSecurityLimits.Text = "Throttle Security Limits"
        '
        'dspPLA_MIN_STOP_POS
        '
        Me.dspPLA_MIN_STOP_POS.Enabled = False
        Me.dspPLA_MIN_STOP_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspPLA_MIN_STOP_POS.Location = New System.Drawing.Point(493, 251)
        Me.dspPLA_MIN_STOP_POS.Name = "dspPLA_MIN_STOP_POS"
        Me.dspPLA_MIN_STOP_POS.Size = New System.Drawing.Size(224, 49)
        Me.dspPLA_MIN_STOP_POS.TabIndex = 13
        '
        'lblPLA_MIN_STOP_POS
        '
        Me.lblPLA_MIN_STOP_POS.AutoSize = True
        Me.lblPLA_MIN_STOP_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLA_MIN_STOP_POS.Location = New System.Drawing.Point(396, 257)
        Me.lblPLA_MIN_STOP_POS.Name = "lblPLA_MIN_STOP_POS"
        Me.lblPLA_MIN_STOP_POS.Size = New System.Drawing.Size(75, 29)
        Me.lblPLA_MIN_STOP_POS.TabIndex = 12
        Me.lblPLA_MIN_STOP_POS.Text = "Angle"
        '
        'btnPLA_STORE_MIN_STOP_ANGLE
        '
        Me.btnPLA_STORE_MIN_STOP_ANGLE.Enabled = False
        Me.btnPLA_STORE_MIN_STOP_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPLA_STORE_MIN_STOP_ANGLE.Location = New System.Drawing.Point(26, 257)
        Me.btnPLA_STORE_MIN_STOP_ANGLE.Name = "btnPLA_STORE_MIN_STOP_ANGLE"
        Me.btnPLA_STORE_MIN_STOP_ANGLE.Size = New System.Drawing.Size(265, 43)
        Me.btnPLA_STORE_MIN_STOP_ANGLE.TabIndex = 10
        Me.btnPLA_STORE_MIN_STOP_ANGLE.Text = "Store Min Stop Position"
        Me.btnPLA_STORE_MIN_STOP_ANGLE.UseVisualStyleBackColor = True
        '
        'dspPLA_IDLE_POS
        '
        Me.dspPLA_IDLE_POS.Enabled = False
        Me.dspPLA_IDLE_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspPLA_IDLE_POS.Location = New System.Drawing.Point(493, 184)
        Me.dspPLA_IDLE_POS.Name = "dspPLA_IDLE_POS"
        Me.dspPLA_IDLE_POS.Size = New System.Drawing.Size(224, 49)
        Me.dspPLA_IDLE_POS.TabIndex = 9
        '
        'lblPLA_IDLE_POS_DONE
        '
        Me.lblPLA_IDLE_POS_DONE.AutoSize = True
        Me.lblPLA_IDLE_POS_DONE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLA_IDLE_POS_DONE.Location = New System.Drawing.Point(396, 190)
        Me.lblPLA_IDLE_POS_DONE.Name = "lblPLA_IDLE_POS_DONE"
        Me.lblPLA_IDLE_POS_DONE.Size = New System.Drawing.Size(75, 29)
        Me.lblPLA_IDLE_POS_DONE.TabIndex = 8
        Me.lblPLA_IDLE_POS_DONE.Text = "Angle"
        '
        'btnPLA_STORE_IDLE_ANGLE
        '
        Me.btnPLA_STORE_IDLE_ANGLE.Enabled = False
        Me.btnPLA_STORE_IDLE_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPLA_STORE_IDLE_ANGLE.Location = New System.Drawing.Point(26, 190)
        Me.btnPLA_STORE_IDLE_ANGLE.Name = "btnPLA_STORE_IDLE_ANGLE"
        Me.btnPLA_STORE_IDLE_ANGLE.Size = New System.Drawing.Size(265, 43)
        Me.btnPLA_STORE_IDLE_ANGLE.TabIndex = 6
        Me.btnPLA_STORE_IDLE_ANGLE.Text = "Store Idle Position"
        Me.btnPLA_STORE_IDLE_ANGLE.UseVisualStyleBackColor = True
        '
        'dspPLA_MAX_STOP_POS
        '
        Me.dspPLA_MAX_STOP_POS.Enabled = False
        Me.dspPLA_MAX_STOP_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspPLA_MAX_STOP_POS.Location = New System.Drawing.Point(493, 122)
        Me.dspPLA_MAX_STOP_POS.Name = "dspPLA_MAX_STOP_POS"
        Me.dspPLA_MAX_STOP_POS.Size = New System.Drawing.Size(224, 49)
        Me.dspPLA_MAX_STOP_POS.TabIndex = 5
        '
        'lblPLA_MAX_STOP_POS_DEG
        '
        Me.lblPLA_MAX_STOP_POS_DEG.AutoSize = True
        Me.lblPLA_MAX_STOP_POS_DEG.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLA_MAX_STOP_POS_DEG.Location = New System.Drawing.Point(396, 129)
        Me.lblPLA_MAX_STOP_POS_DEG.Name = "lblPLA_MAX_STOP_POS_DEG"
        Me.lblPLA_MAX_STOP_POS_DEG.Size = New System.Drawing.Size(75, 29)
        Me.lblPLA_MAX_STOP_POS_DEG.TabIndex = 4
        Me.lblPLA_MAX_STOP_POS_DEG.Text = "Angle"
        '
        'btnPLA_STORE_MAX_STOP_ANGLE
        '
        Me.btnPLA_STORE_MAX_STOP_ANGLE.Enabled = False
        Me.btnPLA_STORE_MAX_STOP_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPLA_STORE_MAX_STOP_ANGLE.Location = New System.Drawing.Point(26, 128)
        Me.btnPLA_STORE_MAX_STOP_ANGLE.Name = "btnPLA_STORE_MAX_STOP_ANGLE"
        Me.btnPLA_STORE_MAX_STOP_ANGLE.Size = New System.Drawing.Size(265, 43)
        Me.btnPLA_STORE_MAX_STOP_ANGLE.TabIndex = 2
        Me.btnPLA_STORE_MAX_STOP_ANGLE.Text = "Store Max Stop Position"
        Me.btnPLA_STORE_MAX_STOP_ANGLE.UseVisualStyleBackColor = True
        '
        'lblPLA_LEVER_ANGLE_READING
        '
        Me.lblPLA_LEVER_ANGLE_READING.AutoSize = True
        Me.lblPLA_LEVER_ANGLE_READING.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLA_LEVER_ANGLE_READING.Location = New System.Drawing.Point(34, 47)
        Me.lblPLA_LEVER_ANGLE_READING.Name = "lblPLA_LEVER_ANGLE_READING"
        Me.lblPLA_LEVER_ANGLE_READING.Size = New System.Drawing.Size(241, 29)
        Me.lblPLA_LEVER_ANGLE_READING.TabIndex = 1
        Me.lblPLA_LEVER_ANGLE_READING.Text = "Servo Angle Reading"
        '
        'dspPLA_SERVO_ANGLE_READING
        '
        Me.dspPLA_SERVO_ANGLE_READING.Enabled = False
        Me.dspPLA_SERVO_ANGLE_READING.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspPLA_SERVO_ANGLE_READING.Location = New System.Drawing.Point(493, 19)
        Me.dspPLA_SERVO_ANGLE_READING.Name = "dspPLA_SERVO_ANGLE_READING"
        Me.dspPLA_SERVO_ANGLE_READING.Size = New System.Drawing.Size(224, 80)
        Me.dspPLA_SERVO_ANGLE_READING.TabIndex = 0
        '
        'grpPlaServoControl
        '
        Me.grpPlaServoControl.Controls.Add(Me.chkPLAHardwareEnable)
        Me.grpPlaServoControl.Controls.Add(Me.chkPLA_ENGAGE)
        Me.grpPlaServoControl.Controls.Add(Me.lblPLA_SERVO_READING)
        Me.grpPlaServoControl.Controls.Add(Me.dspPLA_SERVO_READING)
        Me.grpPlaServoControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpPlaServoControl.Location = New System.Drawing.Point(22, 276)
        Me.grpPlaServoControl.Name = "grpPlaServoControl"
        Me.grpPlaServoControl.Size = New System.Drawing.Size(638, 126)
        Me.grpPlaServoControl.TabIndex = 1
        Me.grpPlaServoControl.TabStop = False
        Me.grpPlaServoControl.Text = "Servo Control"
        '
        'chkPLAHardwareEnable
        '
        Me.chkPLAHardwareEnable.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkPLAHardwareEnable.BackColor = System.Drawing.Color.Maroon
        Me.chkPLAHardwareEnable.Enabled = False
        Me.chkPLAHardwareEnable.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPLAHardwareEnable.ForeColor = System.Drawing.SystemColors.Control
        Me.chkPLAHardwareEnable.Location = New System.Drawing.Point(386, 74)
        Me.chkPLAHardwareEnable.Name = "chkPLAHardwareEnable"
        Me.chkPLAHardwareEnable.Size = New System.Drawing.Size(228, 43)
        Me.chkPLAHardwareEnable.TabIndex = 7
        Me.chkPLAHardwareEnable.Text = "HARDWARE OFF"
        Me.chkPLAHardwareEnable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkPLAHardwareEnable.UseVisualStyleBackColor = False
        '
        'chkPLA_ENGAGE
        '
        Me.chkPLA_ENGAGE.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkPLA_ENGAGE.BackColor = System.Drawing.Color.Maroon
        Me.chkPLA_ENGAGE.Enabled = False
        Me.chkPLA_ENGAGE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPLA_ENGAGE.ForeColor = System.Drawing.SystemColors.Control
        Me.chkPLA_ENGAGE.Location = New System.Drawing.Point(386, 19)
        Me.chkPLA_ENGAGE.Name = "chkPLA_ENGAGE"
        Me.chkPLA_ENGAGE.Size = New System.Drawing.Size(228, 43)
        Me.chkPLA_ENGAGE.TabIndex = 6
        Me.chkPLA_ENGAGE.Text = "ENGAGE OFF"
        Me.chkPLA_ENGAGE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkPLA_ENGAGE.UseVisualStyleBackColor = False
        '
        'lblPLA_SERVO_READING
        '
        Me.lblPLA_SERVO_READING.AutoSize = True
        Me.lblPLA_SERVO_READING.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLA_SERVO_READING.Location = New System.Drawing.Point(6, 26)
        Me.lblPLA_SERVO_READING.Name = "lblPLA_SERVO_READING"
        Me.lblPLA_SERVO_READING.Size = New System.Drawing.Size(173, 29)
        Me.lblPLA_SERVO_READING.TabIndex = 1
        Me.lblPLA_SERVO_READING.Text = "Servo Reading"
        '
        'dspPLA_SERVO_READING
        '
        Me.dspPLA_SERVO_READING.Enabled = False
        Me.dspPLA_SERVO_READING.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspPLA_SERVO_READING.Location = New System.Drawing.Point(203, 19)
        Me.dspPLA_SERVO_READING.Name = "dspPLA_SERVO_READING"
        Me.dspPLA_SERVO_READING.Size = New System.Drawing.Size(161, 49)
        Me.dspPLA_SERVO_READING.TabIndex = 0
        '
        'txtPlaInstructions
        '
        Me.txtPlaInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlaInstructions.Location = New System.Drawing.Point(22, 425)
        Me.txtPlaInstructions.Multiline = True
        Me.txtPlaInstructions.Name = "txtPlaInstructions"
        Me.txtPlaInstructions.Size = New System.Drawing.Size(638, 164)
        Me.txtPlaInstructions.TabIndex = 17
        '
        'grpPlaUserLeverAngles
        '
        Me.grpPlaUserLeverAngles.Controls.Add(Me.lblPLA_RIGGING_DONE)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.indPLA_RIGGING_DONE)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.indPLA_COMPUTE_DONE)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.btnPLA_RESET)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.btnPLA_COMPUTE)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.indPLA_MIN_POS_DONE)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.indPLA_MAX_POS_DONE)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.btnPLA_STORE_MIN_ANGLE)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.btnPLA_STORE_MAX_ANGLE)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.lblPLA_MIN_RIG_POS)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.dspPLA_MIN_RIG_POS)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.dspPLA_LEVER_POS)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.lblPLA_LEVER_POS)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.lblPLA_MAX_RIG_POS)
        Me.grpPlaUserLeverAngles.Controls.Add(Me.dspPLA_MAX_RIG_POS)
        Me.grpPlaUserLeverAngles.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpPlaUserLeverAngles.Location = New System.Drawing.Point(22, 14)
        Me.grpPlaUserLeverAngles.Name = "grpPlaUserLeverAngles"
        Me.grpPlaUserLeverAngles.Size = New System.Drawing.Size(1390, 256)
        Me.grpPlaUserLeverAngles.TabIndex = 0
        Me.grpPlaUserLeverAngles.TabStop = False
        Me.grpPlaUserLeverAngles.Text = "User Lever Angles"
        '
        'lblPLA_RIGGING_DONE
        '
        Me.lblPLA_RIGGING_DONE.AutoSize = True
        Me.lblPLA_RIGGING_DONE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLA_RIGGING_DONE.Location = New System.Drawing.Point(1158, 35)
        Me.lblPLA_RIGGING_DONE.Name = "lblPLA_RIGGING_DONE"
        Me.lblPLA_RIGGING_DONE.Size = New System.Drawing.Size(161, 29)
        Me.lblPLA_RIGGING_DONE.TabIndex = 15
        Me.lblPLA_RIGGING_DONE.Text = "Rigging Done"
        '
        'btnPLA_RESET
        '
        Me.btnPLA_RESET.Enabled = False
        Me.btnPLA_RESET.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPLA_RESET.Location = New System.Drawing.Point(669, 170)
        Me.btnPLA_RESET.Name = "btnPLA_RESET"
        Me.btnPLA_RESET.Size = New System.Drawing.Size(264, 43)
        Me.btnPLA_RESET.TabIndex = 12
        Me.btnPLA_RESET.Text = "Reset Rigging"
        Me.btnPLA_RESET.UseVisualStyleBackColor = True
        '
        'btnPLA_COMPUTE
        '
        Me.btnPLA_COMPUTE.Enabled = False
        Me.btnPLA_COMPUTE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPLA_COMPUTE.Location = New System.Drawing.Point(669, 27)
        Me.btnPLA_COMPUTE.Name = "btnPLA_COMPUTE"
        Me.btnPLA_COMPUTE.Size = New System.Drawing.Size(264, 43)
        Me.btnPLA_COMPUTE.TabIndex = 11
        Me.btnPLA_COMPUTE.Text = "Compute"
        Me.btnPLA_COMPUTE.UseVisualStyleBackColor = True
        '
        'btnPLA_STORE_MIN_ANGLE
        '
        Me.btnPLA_STORE_MIN_ANGLE.Enabled = False
        Me.btnPLA_STORE_MIN_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPLA_STORE_MIN_ANGLE.Location = New System.Drawing.Point(386, 170)
        Me.btnPLA_STORE_MIN_ANGLE.Name = "btnPLA_STORE_MIN_ANGLE"
        Me.btnPLA_STORE_MIN_ANGLE.Size = New System.Drawing.Size(228, 43)
        Me.btnPLA_STORE_MIN_ANGLE.TabIndex = 8
        Me.btnPLA_STORE_MIN_ANGLE.Text = "Store Min Position"
        Me.btnPLA_STORE_MIN_ANGLE.UseVisualStyleBackColor = True
        '
        'btnPLA_STORE_MAX_ANGLE
        '
        Me.btnPLA_STORE_MAX_ANGLE.Enabled = False
        Me.btnPLA_STORE_MAX_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPLA_STORE_MAX_ANGLE.Location = New System.Drawing.Point(386, 28)
        Me.btnPLA_STORE_MAX_ANGLE.Name = "btnPLA_STORE_MAX_ANGLE"
        Me.btnPLA_STORE_MAX_ANGLE.Size = New System.Drawing.Size(228, 43)
        Me.btnPLA_STORE_MAX_ANGLE.TabIndex = 7
        Me.btnPLA_STORE_MAX_ANGLE.Text = "Store Max Position"
        Me.btnPLA_STORE_MAX_ANGLE.UseVisualStyleBackColor = True
        '
        'lblPLA_MIN_RIG_POS
        '
        Me.lblPLA_MIN_RIG_POS.AutoSize = True
        Me.lblPLA_MIN_RIG_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLA_MIN_RIG_POS.Location = New System.Drawing.Point(6, 170)
        Me.lblPLA_MIN_RIG_POS.Name = "lblPLA_MIN_RIG_POS"
        Me.lblPLA_MIN_RIG_POS.Size = New System.Drawing.Size(120, 29)
        Me.lblPLA_MIN_RIG_POS.TabIndex = 6
        Me.lblPLA_MIN_RIG_POS.Text = "Min Angle"
        '
        'dspPLA_MIN_RIG_POS
        '
        Me.dspPLA_MIN_RIG_POS.Enabled = False
        Me.dspPLA_MIN_RIG_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspPLA_MIN_RIG_POS.Location = New System.Drawing.Point(203, 169)
        Me.dspPLA_MIN_RIG_POS.Name = "dspPLA_MIN_RIG_POS"
        Me.dspPLA_MIN_RIG_POS.Size = New System.Drawing.Size(161, 49)
        Me.dspPLA_MIN_RIG_POS.TabIndex = 5
        '
        'dspPLA_LEVER_POS
        '
        Me.dspPLA_LEVER_POS.Enabled = False
        Me.dspPLA_LEVER_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspPLA_LEVER_POS.Location = New System.Drawing.Point(203, 97)
        Me.dspPLA_LEVER_POS.Name = "dspPLA_LEVER_POS"
        Me.dspPLA_LEVER_POS.Size = New System.Drawing.Size(161, 49)
        Me.dspPLA_LEVER_POS.TabIndex = 4
        '
        'lblPLA_LEVER_POS
        '
        Me.lblPLA_LEVER_POS.AutoSize = True
        Me.lblPLA_LEVER_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLA_LEVER_POS.Location = New System.Drawing.Point(6, 97)
        Me.lblPLA_LEVER_POS.Name = "lblPLA_LEVER_POS"
        Me.lblPLA_LEVER_POS.Size = New System.Drawing.Size(170, 29)
        Me.lblPLA_LEVER_POS.TabIndex = 3
        Me.lblPLA_LEVER_POS.Text = "Lever Reading"
        '
        'lblPLA_MAX_RIG_POS
        '
        Me.lblPLA_MAX_RIG_POS.AutoSize = True
        Me.lblPLA_MAX_RIG_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLA_MAX_RIG_POS.Location = New System.Drawing.Point(6, 27)
        Me.lblPLA_MAX_RIG_POS.Name = "lblPLA_MAX_RIG_POS"
        Me.lblPLA_MAX_RIG_POS.Size = New System.Drawing.Size(125, 29)
        Me.lblPLA_MAX_RIG_POS.TabIndex = 2
        Me.lblPLA_MAX_RIG_POS.Text = "Max Angle"
        '
        'dspPLA_MAX_RIG_POS
        '
        Me.dspPLA_MAX_RIG_POS.Enabled = False
        Me.dspPLA_MAX_RIG_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspPLA_MAX_RIG_POS.Location = New System.Drawing.Point(203, 27)
        Me.dspPLA_MAX_RIG_POS.Name = "dspPLA_MAX_RIG_POS"
        Me.dspPLA_MAX_RIG_POS.Size = New System.Drawing.Size(161, 49)
        Me.dspPLA_MAX_RIG_POS.TabIndex = 1
        '
        'tabCLA
        '
        Me.tabCLA.Controls.Add(Me.lblClaInstructions)
        Me.tabCLA.Controls.Add(Me.txtCLAInstructions)
        Me.tabCLA.Controls.Add(Me.grpClaThrottleSecurityLimits)
        Me.tabCLA.Controls.Add(Me.grpClaServoControl)
        Me.tabCLA.Controls.Add(Me.grpClaUserLeverAngles)
        Me.tabCLA.Location = New System.Drawing.Point(4, 38)
        Me.tabCLA.Name = "tabCLA"
        Me.tabCLA.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCLA.Size = New System.Drawing.Size(1438, 596)
        Me.tabCLA.TabIndex = 1
        Me.tabCLA.Text = "CLA"
        Me.tabCLA.UseVisualStyleBackColor = True
        '
        'lblClaInstructions
        '
        Me.lblClaInstructions.AutoSize = True
        Me.lblClaInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClaInstructions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblClaInstructions.Location = New System.Drawing.Point(28, 405)
        Me.lblClaInstructions.Name = "lblClaInstructions"
        Me.lblClaInstructions.Size = New System.Drawing.Size(84, 18)
        Me.lblClaInstructions.TabIndex = 21
        Me.lblClaInstructions.Text = "Instructions"
        '
        'txtCLAInstructions
        '
        Me.txtCLAInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCLAInstructions.Location = New System.Drawing.Point(22, 425)
        Me.txtCLAInstructions.Multiline = True
        Me.txtCLAInstructions.Name = "txtCLAInstructions"
        Me.txtCLAInstructions.Size = New System.Drawing.Size(637, 162)
        Me.txtCLAInstructions.TabIndex = 20
        '
        'grpClaThrottleSecurityLimits
        '
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.dspCLA_MIN_STOP_POS)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.Label2)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.indCLA_MIN_STOP_POS_DONE)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.btnCLA_STORE_MIN_STOP_ANGLE)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.dspCLA_IDLE_POS)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.Label3)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.indCLA_IDLE_POS_DONE)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.btnCLA_STORE_IDLE_ANGLE)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.dspCLA_MAX_STOP_POS)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.Label4)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.indCLA_MAX_STOP_POS_DONE)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.btnCLA_STORE_MAX_STOP_ANGLE)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.Label5)
        Me.grpClaThrottleSecurityLimits.Controls.Add(Me.dspCLA_SERVO_ANGLE_READING)
        Me.grpClaThrottleSecurityLimits.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpClaThrottleSecurityLimits.Location = New System.Drawing.Point(665, 276)
        Me.grpClaThrottleSecurityLimits.Name = "grpClaThrottleSecurityLimits"
        Me.grpClaThrottleSecurityLimits.Size = New System.Drawing.Size(747, 317)
        Me.grpClaThrottleSecurityLimits.TabIndex = 6
        Me.grpClaThrottleSecurityLimits.TabStop = False
        Me.grpClaThrottleSecurityLimits.Text = "Throttle Security Limits"
        '
        'dspCLA_MIN_STOP_POS
        '
        Me.dspCLA_MIN_STOP_POS.Enabled = False
        Me.dspCLA_MIN_STOP_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspCLA_MIN_STOP_POS.Location = New System.Drawing.Point(493, 251)
        Me.dspCLA_MIN_STOP_POS.Name = "dspCLA_MIN_STOP_POS"
        Me.dspCLA_MIN_STOP_POS.Size = New System.Drawing.Size(229, 49)
        Me.dspCLA_MIN_STOP_POS.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(396, 257)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 29)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Angle"
        '
        'btnCLA_STORE_MIN_STOP_ANGLE
        '
        Me.btnCLA_STORE_MIN_STOP_ANGLE.Enabled = False
        Me.btnCLA_STORE_MIN_STOP_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCLA_STORE_MIN_STOP_ANGLE.Location = New System.Drawing.Point(26, 257)
        Me.btnCLA_STORE_MIN_STOP_ANGLE.Name = "btnCLA_STORE_MIN_STOP_ANGLE"
        Me.btnCLA_STORE_MIN_STOP_ANGLE.Size = New System.Drawing.Size(265, 43)
        Me.btnCLA_STORE_MIN_STOP_ANGLE.TabIndex = 10
        Me.btnCLA_STORE_MIN_STOP_ANGLE.Text = "Store Min Stop Position"
        Me.btnCLA_STORE_MIN_STOP_ANGLE.UseVisualStyleBackColor = True
        '
        'dspCLA_IDLE_POS
        '
        Me.dspCLA_IDLE_POS.Enabled = False
        Me.dspCLA_IDLE_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspCLA_IDLE_POS.Location = New System.Drawing.Point(493, 184)
        Me.dspCLA_IDLE_POS.Name = "dspCLA_IDLE_POS"
        Me.dspCLA_IDLE_POS.Size = New System.Drawing.Size(229, 49)
        Me.dspCLA_IDLE_POS.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(396, 190)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 29)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Angle"
        '
        'btnCLA_STORE_IDLE_ANGLE
        '
        Me.btnCLA_STORE_IDLE_ANGLE.Enabled = False
        Me.btnCLA_STORE_IDLE_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCLA_STORE_IDLE_ANGLE.Location = New System.Drawing.Point(26, 190)
        Me.btnCLA_STORE_IDLE_ANGLE.Name = "btnCLA_STORE_IDLE_ANGLE"
        Me.btnCLA_STORE_IDLE_ANGLE.Size = New System.Drawing.Size(265, 43)
        Me.btnCLA_STORE_IDLE_ANGLE.TabIndex = 6
        Me.btnCLA_STORE_IDLE_ANGLE.Text = "Store Idle Position"
        Me.btnCLA_STORE_IDLE_ANGLE.UseVisualStyleBackColor = True
        '
        'dspCLA_MAX_STOP_POS
        '
        Me.dspCLA_MAX_STOP_POS.Enabled = False
        Me.dspCLA_MAX_STOP_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspCLA_MAX_STOP_POS.Location = New System.Drawing.Point(493, 122)
        Me.dspCLA_MAX_STOP_POS.Name = "dspCLA_MAX_STOP_POS"
        Me.dspCLA_MAX_STOP_POS.Size = New System.Drawing.Size(229, 49)
        Me.dspCLA_MAX_STOP_POS.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(396, 129)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 29)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Angle"
        '
        'btnCLA_STORE_MAX_STOP_ANGLE
        '
        Me.btnCLA_STORE_MAX_STOP_ANGLE.Enabled = False
        Me.btnCLA_STORE_MAX_STOP_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCLA_STORE_MAX_STOP_ANGLE.Location = New System.Drawing.Point(26, 128)
        Me.btnCLA_STORE_MAX_STOP_ANGLE.Name = "btnCLA_STORE_MAX_STOP_ANGLE"
        Me.btnCLA_STORE_MAX_STOP_ANGLE.Size = New System.Drawing.Size(265, 43)
        Me.btnCLA_STORE_MAX_STOP_ANGLE.TabIndex = 2
        Me.btnCLA_STORE_MAX_STOP_ANGLE.Text = "Store Max Stop Position"
        Me.btnCLA_STORE_MAX_STOP_ANGLE.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(34, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(241, 29)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Servo Angle Reading"
        '
        'dspCLA_SERVO_ANGLE_READING
        '
        Me.dspCLA_SERVO_ANGLE_READING.Enabled = False
        Me.dspCLA_SERVO_ANGLE_READING.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspCLA_SERVO_ANGLE_READING.Location = New System.Drawing.Point(493, 19)
        Me.dspCLA_SERVO_ANGLE_READING.Name = "dspCLA_SERVO_ANGLE_READING"
        Me.dspCLA_SERVO_ANGLE_READING.Size = New System.Drawing.Size(229, 80)
        Me.dspCLA_SERVO_ANGLE_READING.TabIndex = 0
        '
        'grpClaServoControl
        '
        Me.grpClaServoControl.Controls.Add(Me.chkCLAHardwareEnable)
        Me.grpClaServoControl.Controls.Add(Me.chkCLA_ENGAGE)
        Me.grpClaServoControl.Controls.Add(Me.Label6)
        Me.grpClaServoControl.Controls.Add(Me.dspCLA_SERVO_READING)
        Me.grpClaServoControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpClaServoControl.Location = New System.Drawing.Point(22, 276)
        Me.grpClaServoControl.Name = "grpClaServoControl"
        Me.grpClaServoControl.Size = New System.Drawing.Size(638, 126)
        Me.grpClaServoControl.TabIndex = 5
        Me.grpClaServoControl.TabStop = False
        Me.grpClaServoControl.Text = "Servo Control"
        '
        'chkCLAHardwareEnable
        '
        Me.chkCLAHardwareEnable.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkCLAHardwareEnable.BackColor = System.Drawing.Color.Maroon
        Me.chkCLAHardwareEnable.Enabled = False
        Me.chkCLAHardwareEnable.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCLAHardwareEnable.ForeColor = System.Drawing.SystemColors.Control
        Me.chkCLAHardwareEnable.Location = New System.Drawing.Point(386, 74)
        Me.chkCLAHardwareEnable.Name = "chkCLAHardwareEnable"
        Me.chkCLAHardwareEnable.Size = New System.Drawing.Size(228, 43)
        Me.chkCLAHardwareEnable.TabIndex = 8
        Me.chkCLAHardwareEnable.Text = "HARDWARE OFF"
        Me.chkCLAHardwareEnable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkCLAHardwareEnable.UseVisualStyleBackColor = False
        '
        'chkCLA_ENGAGE
        '
        Me.chkCLA_ENGAGE.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkCLA_ENGAGE.BackColor = System.Drawing.Color.Maroon
        Me.chkCLA_ENGAGE.Enabled = False
        Me.chkCLA_ENGAGE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCLA_ENGAGE.ForeColor = System.Drawing.SystemColors.Control
        Me.chkCLA_ENGAGE.Location = New System.Drawing.Point(386, 19)
        Me.chkCLA_ENGAGE.Name = "chkCLA_ENGAGE"
        Me.chkCLA_ENGAGE.Size = New System.Drawing.Size(228, 43)
        Me.chkCLA_ENGAGE.TabIndex = 7
        Me.chkCLA_ENGAGE.Text = "ENGAGE OFF"
        Me.chkCLA_ENGAGE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkCLA_ENGAGE.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(173, 29)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Servo Reading"
        '
        'dspCLA_SERVO_READING
        '
        Me.dspCLA_SERVO_READING.Enabled = False
        Me.dspCLA_SERVO_READING.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspCLA_SERVO_READING.Location = New System.Drawing.Point(203, 19)
        Me.dspCLA_SERVO_READING.Name = "dspCLA_SERVO_READING"
        Me.dspCLA_SERVO_READING.Size = New System.Drawing.Size(161, 49)
        Me.dspCLA_SERVO_READING.TabIndex = 0
        '
        'grpClaUserLeverAngles
        '
        Me.grpClaUserLeverAngles.Controls.Add(Me.lblCLA_RIGGING_DONE)
        Me.grpClaUserLeverAngles.Controls.Add(Me.indCLA_RIGGING_DONE)
        Me.grpClaUserLeverAngles.Controls.Add(Me.indCLA_COMPUTE_DONE)
        Me.grpClaUserLeverAngles.Controls.Add(Me.btnCLA_RESET)
        Me.grpClaUserLeverAngles.Controls.Add(Me.btnCLA_COMPUTE)
        Me.grpClaUserLeverAngles.Controls.Add(Me.indCLA_MIN_POS_DONE)
        Me.grpClaUserLeverAngles.Controls.Add(Me.indCLA_MAX_POS_DONE)
        Me.grpClaUserLeverAngles.Controls.Add(Me.btnCLA_STORE_MIN_ANGLE)
        Me.grpClaUserLeverAngles.Controls.Add(Me.btnCLA_STORE_MAX_ANGLE)
        Me.grpClaUserLeverAngles.Controls.Add(Me.Label8)
        Me.grpClaUserLeverAngles.Controls.Add(Me.dspCLA_MIN_RIG_POS)
        Me.grpClaUserLeverAngles.Controls.Add(Me.dspCLA_LEVER_POS)
        Me.grpClaUserLeverAngles.Controls.Add(Me.Label9)
        Me.grpClaUserLeverAngles.Controls.Add(Me.Label10)
        Me.grpClaUserLeverAngles.Controls.Add(Me.dspCLA_MAX_RIG_POS)
        Me.grpClaUserLeverAngles.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpClaUserLeverAngles.Location = New System.Drawing.Point(22, 14)
        Me.grpClaUserLeverAngles.Name = "grpClaUserLeverAngles"
        Me.grpClaUserLeverAngles.Size = New System.Drawing.Size(1390, 256)
        Me.grpClaUserLeverAngles.TabIndex = 4
        Me.grpClaUserLeverAngles.TabStop = False
        Me.grpClaUserLeverAngles.Text = "User Lever Angles"
        '
        'lblCLA_RIGGING_DONE
        '
        Me.lblCLA_RIGGING_DONE.AutoSize = True
        Me.lblCLA_RIGGING_DONE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCLA_RIGGING_DONE.Location = New System.Drawing.Point(1158, 35)
        Me.lblCLA_RIGGING_DONE.Name = "lblCLA_RIGGING_DONE"
        Me.lblCLA_RIGGING_DONE.Size = New System.Drawing.Size(161, 29)
        Me.lblCLA_RIGGING_DONE.TabIndex = 15
        Me.lblCLA_RIGGING_DONE.Text = "Rigging Done"
        '
        'btnCLA_RESET
        '
        Me.btnCLA_RESET.Enabled = False
        Me.btnCLA_RESET.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCLA_RESET.Location = New System.Drawing.Point(669, 170)
        Me.btnCLA_RESET.Name = "btnCLA_RESET"
        Me.btnCLA_RESET.Size = New System.Drawing.Size(264, 43)
        Me.btnCLA_RESET.TabIndex = 12
        Me.btnCLA_RESET.Text = "Reset Rigging"
        Me.btnCLA_RESET.UseVisualStyleBackColor = True
        '
        'btnCLA_COMPUTE
        '
        Me.btnCLA_COMPUTE.Enabled = False
        Me.btnCLA_COMPUTE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCLA_COMPUTE.Location = New System.Drawing.Point(669, 27)
        Me.btnCLA_COMPUTE.Name = "btnCLA_COMPUTE"
        Me.btnCLA_COMPUTE.Size = New System.Drawing.Size(264, 43)
        Me.btnCLA_COMPUTE.TabIndex = 11
        Me.btnCLA_COMPUTE.Text = "Compute"
        Me.btnCLA_COMPUTE.UseVisualStyleBackColor = True
        '
        'btnCLA_STORE_MIN_ANGLE
        '
        Me.btnCLA_STORE_MIN_ANGLE.Enabled = False
        Me.btnCLA_STORE_MIN_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCLA_STORE_MIN_ANGLE.Location = New System.Drawing.Point(386, 170)
        Me.btnCLA_STORE_MIN_ANGLE.Name = "btnCLA_STORE_MIN_ANGLE"
        Me.btnCLA_STORE_MIN_ANGLE.Size = New System.Drawing.Size(228, 43)
        Me.btnCLA_STORE_MIN_ANGLE.TabIndex = 8
        Me.btnCLA_STORE_MIN_ANGLE.Text = "Store Min Position"
        Me.btnCLA_STORE_MIN_ANGLE.UseVisualStyleBackColor = True
        '
        'btnCLA_STORE_MAX_ANGLE
        '
        Me.btnCLA_STORE_MAX_ANGLE.Enabled = False
        Me.btnCLA_STORE_MAX_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCLA_STORE_MAX_ANGLE.Location = New System.Drawing.Point(386, 28)
        Me.btnCLA_STORE_MAX_ANGLE.Name = "btnCLA_STORE_MAX_ANGLE"
        Me.btnCLA_STORE_MAX_ANGLE.Size = New System.Drawing.Size(228, 43)
        Me.btnCLA_STORE_MAX_ANGLE.TabIndex = 7
        Me.btnCLA_STORE_MAX_ANGLE.Text = "Store Max Position"
        Me.btnCLA_STORE_MAX_ANGLE.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 170)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 29)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Min Angle"
        '
        'dspCLA_MIN_RIG_POS
        '
        Me.dspCLA_MIN_RIG_POS.Enabled = False
        Me.dspCLA_MIN_RIG_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspCLA_MIN_RIG_POS.Location = New System.Drawing.Point(203, 169)
        Me.dspCLA_MIN_RIG_POS.Name = "dspCLA_MIN_RIG_POS"
        Me.dspCLA_MIN_RIG_POS.Size = New System.Drawing.Size(161, 49)
        Me.dspCLA_MIN_RIG_POS.TabIndex = 5
        '
        'dspCLA_LEVER_POS
        '
        Me.dspCLA_LEVER_POS.Enabled = False
        Me.dspCLA_LEVER_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspCLA_LEVER_POS.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dspCLA_LEVER_POS.Location = New System.Drawing.Point(203, 97)
        Me.dspCLA_LEVER_POS.Name = "dspCLA_LEVER_POS"
        Me.dspCLA_LEVER_POS.Size = New System.Drawing.Size(161, 49)
        Me.dspCLA_LEVER_POS.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 97)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(170, 29)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Lever Reading"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 27)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(125, 29)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Max Angle"
        '
        'dspCLA_MAX_RIG_POS
        '
        Me.dspCLA_MAX_RIG_POS.Enabled = False
        Me.dspCLA_MAX_RIG_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspCLA_MAX_RIG_POS.Location = New System.Drawing.Point(203, 27)
        Me.dspCLA_MAX_RIG_POS.Name = "dspCLA_MAX_RIG_POS"
        Me.dspCLA_MAX_RIG_POS.Size = New System.Drawing.Size(161, 49)
        Me.dspCLA_MAX_RIG_POS.TabIndex = 1
        '
        'tabMOR
        '
        Me.tabMOR.Controls.Add(Me.lblMORInstructions)
        Me.tabMOR.Controls.Add(Me.txtMORInstructions)
        Me.tabMOR.Controls.Add(Me.grpMorThrottleSecurityLimits)
        Me.tabMOR.Controls.Add(Me.grpMORServoControl)
        Me.tabMOR.Controls.Add(Me.grpMORUserLeverAngles)
        Me.tabMOR.Location = New System.Drawing.Point(4, 38)
        Me.tabMOR.Name = "tabMOR"
        Me.tabMOR.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMOR.Size = New System.Drawing.Size(1438, 596)
        Me.tabMOR.TabIndex = 2
        Me.tabMOR.Text = "MOR"
        Me.tabMOR.UseVisualStyleBackColor = True
        '
        'lblMORInstructions
        '
        Me.lblMORInstructions.AutoSize = True
        Me.lblMORInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMORInstructions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMORInstructions.Location = New System.Drawing.Point(28, 405)
        Me.lblMORInstructions.Name = "lblMORInstructions"
        Me.lblMORInstructions.Size = New System.Drawing.Size(84, 18)
        Me.lblMORInstructions.TabIndex = 23
        Me.lblMORInstructions.Text = "Instructions"
        '
        'txtMORInstructions
        '
        Me.txtMORInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMORInstructions.Location = New System.Drawing.Point(22, 425)
        Me.txtMORInstructions.Multiline = True
        Me.txtMORInstructions.Name = "txtMORInstructions"
        Me.txtMORInstructions.Size = New System.Drawing.Size(638, 116)
        Me.txtMORInstructions.TabIndex = 22
        '
        'grpMorThrottleSecurityLimits
        '
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.dspMOR_MIN_STOP_POS)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.Label7)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.indMOR_MIN_STOP_POS_DONE)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.btnMOR_STORE_MIN_STOP_ANGLE)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.dspMOR_IDLE_POS)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.Label11)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.indMOR_IDLE_POS_DONE)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.btnMOR_STORE_IDLE_ANGLE)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.dspMOR_MAX_STOP_POS)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.Label12)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.indMOR_MAX_STOP_POS_DONE)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.btnMOR_STORE_MAX_STOP_ANGLE)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.Label13)
        Me.grpMorThrottleSecurityLimits.Controls.Add(Me.dspMOR_SERVO_ANGLE_READING)
        Me.grpMorThrottleSecurityLimits.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMorThrottleSecurityLimits.Location = New System.Drawing.Point(665, 276)
        Me.grpMorThrottleSecurityLimits.Name = "grpMorThrottleSecurityLimits"
        Me.grpMorThrottleSecurityLimits.Size = New System.Drawing.Size(747, 317)
        Me.grpMorThrottleSecurityLimits.TabIndex = 9
        Me.grpMorThrottleSecurityLimits.TabStop = False
        Me.grpMorThrottleSecurityLimits.Text = "Throttle Security Limits"
        '
        'dspMOR_MIN_STOP_POS
        '
        Me.dspMOR_MIN_STOP_POS.Enabled = False
        Me.dspMOR_MIN_STOP_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspMOR_MIN_STOP_POS.Location = New System.Drawing.Point(493, 251)
        Me.dspMOR_MIN_STOP_POS.Name = "dspMOR_MIN_STOP_POS"
        Me.dspMOR_MIN_STOP_POS.Size = New System.Drawing.Size(229, 49)
        Me.dspMOR_MIN_STOP_POS.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(396, 257)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 29)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Angle"
        '
        'btnMOR_STORE_MIN_STOP_ANGLE
        '
        Me.btnMOR_STORE_MIN_STOP_ANGLE.Enabled = False
        Me.btnMOR_STORE_MIN_STOP_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMOR_STORE_MIN_STOP_ANGLE.Location = New System.Drawing.Point(26, 257)
        Me.btnMOR_STORE_MIN_STOP_ANGLE.Name = "btnMOR_STORE_MIN_STOP_ANGLE"
        Me.btnMOR_STORE_MIN_STOP_ANGLE.Size = New System.Drawing.Size(265, 43)
        Me.btnMOR_STORE_MIN_STOP_ANGLE.TabIndex = 10
        Me.btnMOR_STORE_MIN_STOP_ANGLE.Text = "Store Min Stop Position"
        Me.btnMOR_STORE_MIN_STOP_ANGLE.UseVisualStyleBackColor = True
        '
        'dspMOR_IDLE_POS
        '
        Me.dspMOR_IDLE_POS.Enabled = False
        Me.dspMOR_IDLE_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspMOR_IDLE_POS.Location = New System.Drawing.Point(493, 184)
        Me.dspMOR_IDLE_POS.Name = "dspMOR_IDLE_POS"
        Me.dspMOR_IDLE_POS.Size = New System.Drawing.Size(229, 49)
        Me.dspMOR_IDLE_POS.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(396, 190)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 29)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Angle"
        '
        'btnMOR_STORE_IDLE_ANGLE
        '
        Me.btnMOR_STORE_IDLE_ANGLE.Enabled = False
        Me.btnMOR_STORE_IDLE_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMOR_STORE_IDLE_ANGLE.Location = New System.Drawing.Point(26, 190)
        Me.btnMOR_STORE_IDLE_ANGLE.Name = "btnMOR_STORE_IDLE_ANGLE"
        Me.btnMOR_STORE_IDLE_ANGLE.Size = New System.Drawing.Size(265, 43)
        Me.btnMOR_STORE_IDLE_ANGLE.TabIndex = 6
        Me.btnMOR_STORE_IDLE_ANGLE.Text = "Store Idle Position"
        Me.btnMOR_STORE_IDLE_ANGLE.UseVisualStyleBackColor = True
        '
        'dspMOR_MAX_STOP_POS
        '
        Me.dspMOR_MAX_STOP_POS.Enabled = False
        Me.dspMOR_MAX_STOP_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspMOR_MAX_STOP_POS.Location = New System.Drawing.Point(493, 122)
        Me.dspMOR_MAX_STOP_POS.Name = "dspMOR_MAX_STOP_POS"
        Me.dspMOR_MAX_STOP_POS.Size = New System.Drawing.Size(229, 49)
        Me.dspMOR_MAX_STOP_POS.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(396, 129)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 29)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Angle"
        '
        'btnMOR_STORE_MAX_STOP_ANGLE
        '
        Me.btnMOR_STORE_MAX_STOP_ANGLE.Enabled = False
        Me.btnMOR_STORE_MAX_STOP_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMOR_STORE_MAX_STOP_ANGLE.Location = New System.Drawing.Point(26, 128)
        Me.btnMOR_STORE_MAX_STOP_ANGLE.Name = "btnMOR_STORE_MAX_STOP_ANGLE"
        Me.btnMOR_STORE_MAX_STOP_ANGLE.Size = New System.Drawing.Size(265, 43)
        Me.btnMOR_STORE_MAX_STOP_ANGLE.TabIndex = 2
        Me.btnMOR_STORE_MAX_STOP_ANGLE.Text = "Store Max Stop Position"
        Me.btnMOR_STORE_MAX_STOP_ANGLE.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(34, 47)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(241, 29)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "Servo Angle Reading"
        '
        'dspMOR_SERVO_ANGLE_READING
        '
        Me.dspMOR_SERVO_ANGLE_READING.Enabled = False
        Me.dspMOR_SERVO_ANGLE_READING.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspMOR_SERVO_ANGLE_READING.Location = New System.Drawing.Point(493, 19)
        Me.dspMOR_SERVO_ANGLE_READING.Name = "dspMOR_SERVO_ANGLE_READING"
        Me.dspMOR_SERVO_ANGLE_READING.Size = New System.Drawing.Size(229, 80)
        Me.dspMOR_SERVO_ANGLE_READING.TabIndex = 0
        '
        'grpMORServoControl
        '
        Me.grpMORServoControl.Controls.Add(Me.chkMORHardwareEnable)
        Me.grpMORServoControl.Controls.Add(Me.chkMOR_ENGAGE)
        Me.grpMORServoControl.Controls.Add(Me.Label14)
        Me.grpMORServoControl.Controls.Add(Me.dspMOR_SERVO_READING)
        Me.grpMORServoControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMORServoControl.Location = New System.Drawing.Point(22, 276)
        Me.grpMORServoControl.Name = "grpMORServoControl"
        Me.grpMORServoControl.Size = New System.Drawing.Size(638, 126)
        Me.grpMORServoControl.TabIndex = 8
        Me.grpMORServoControl.TabStop = False
        Me.grpMORServoControl.Text = "Servo Control"
        '
        'chkMORHardwareEnable
        '
        Me.chkMORHardwareEnable.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkMORHardwareEnable.BackColor = System.Drawing.Color.Maroon
        Me.chkMORHardwareEnable.Enabled = False
        Me.chkMORHardwareEnable.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMORHardwareEnable.ForeColor = System.Drawing.SystemColors.Control
        Me.chkMORHardwareEnable.Location = New System.Drawing.Point(386, 74)
        Me.chkMORHardwareEnable.Name = "chkMORHardwareEnable"
        Me.chkMORHardwareEnable.Size = New System.Drawing.Size(228, 43)
        Me.chkMORHardwareEnable.TabIndex = 9
        Me.chkMORHardwareEnable.Text = "HARDWARE OFF"
        Me.chkMORHardwareEnable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkMORHardwareEnable.UseVisualStyleBackColor = False
        '
        'chkMOR_ENGAGE
        '
        Me.chkMOR_ENGAGE.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkMOR_ENGAGE.BackColor = System.Drawing.Color.Maroon
        Me.chkMOR_ENGAGE.Enabled = False
        Me.chkMOR_ENGAGE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMOR_ENGAGE.ForeColor = System.Drawing.SystemColors.Control
        Me.chkMOR_ENGAGE.Location = New System.Drawing.Point(386, 19)
        Me.chkMOR_ENGAGE.Name = "chkMOR_ENGAGE"
        Me.chkMOR_ENGAGE.Size = New System.Drawing.Size(228, 43)
        Me.chkMOR_ENGAGE.TabIndex = 8
        Me.chkMOR_ENGAGE.Text = "ENGAGE OFF"
        Me.chkMOR_ENGAGE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkMOR_ENGAGE.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(6, 26)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(173, 29)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "Servo Reading"
        '
        'dspMOR_SERVO_READING
        '
        Me.dspMOR_SERVO_READING.Enabled = False
        Me.dspMOR_SERVO_READING.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspMOR_SERVO_READING.Location = New System.Drawing.Point(203, 19)
        Me.dspMOR_SERVO_READING.Name = "dspMOR_SERVO_READING"
        Me.dspMOR_SERVO_READING.Size = New System.Drawing.Size(161, 49)
        Me.dspMOR_SERVO_READING.TabIndex = 0
        '
        'grpMORUserLeverAngles
        '
        Me.grpMORUserLeverAngles.Controls.Add(Me.Label15)
        Me.grpMORUserLeverAngles.Controls.Add(Me.indMOR_RIGGING_DONE)
        Me.grpMORUserLeverAngles.Controls.Add(Me.indMOR_COMPUTE_DONE)
        Me.grpMORUserLeverAngles.Controls.Add(Me.btnMOR_RESET)
        Me.grpMORUserLeverAngles.Controls.Add(Me.btnMOR_COMPUTE)
        Me.grpMORUserLeverAngles.Controls.Add(Me.indMOR_MIN_POS_DONE)
        Me.grpMORUserLeverAngles.Controls.Add(Me.indMOR_MAX_POS_DONE)
        Me.grpMORUserLeverAngles.Controls.Add(Me.btnMOR_STORE_MIN_ANGLE)
        Me.grpMORUserLeverAngles.Controls.Add(Me.btnMOR_STORE_MAX_ANGLE)
        Me.grpMORUserLeverAngles.Controls.Add(Me.Label16)
        Me.grpMORUserLeverAngles.Controls.Add(Me.dspMOR_MIN_RIG_POS)
        Me.grpMORUserLeverAngles.Controls.Add(Me.dspMOR_LEVER_POS)
        Me.grpMORUserLeverAngles.Controls.Add(Me.Label17)
        Me.grpMORUserLeverAngles.Controls.Add(Me.Label18)
        Me.grpMORUserLeverAngles.Controls.Add(Me.dspMOR_MAX_RIG_POS)
        Me.grpMORUserLeverAngles.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMORUserLeverAngles.Location = New System.Drawing.Point(22, 14)
        Me.grpMORUserLeverAngles.Name = "grpMORUserLeverAngles"
        Me.grpMORUserLeverAngles.Size = New System.Drawing.Size(1390, 256)
        Me.grpMORUserLeverAngles.TabIndex = 7
        Me.grpMORUserLeverAngles.TabStop = False
        Me.grpMORUserLeverAngles.Text = "User Lever Angles"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(1158, 35)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(161, 29)
        Me.Label15.TabIndex = 15
        Me.Label15.Text = "Rigging Done"
        '
        'btnMOR_RESET
        '
        Me.btnMOR_RESET.Enabled = False
        Me.btnMOR_RESET.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMOR_RESET.Location = New System.Drawing.Point(669, 170)
        Me.btnMOR_RESET.Name = "btnMOR_RESET"
        Me.btnMOR_RESET.Size = New System.Drawing.Size(264, 43)
        Me.btnMOR_RESET.TabIndex = 12
        Me.btnMOR_RESET.Text = "Reset Rigging"
        Me.btnMOR_RESET.UseVisualStyleBackColor = True
        '
        'btnMOR_COMPUTE
        '
        Me.btnMOR_COMPUTE.Enabled = False
        Me.btnMOR_COMPUTE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMOR_COMPUTE.Location = New System.Drawing.Point(669, 27)
        Me.btnMOR_COMPUTE.Name = "btnMOR_COMPUTE"
        Me.btnMOR_COMPUTE.Size = New System.Drawing.Size(264, 43)
        Me.btnMOR_COMPUTE.TabIndex = 11
        Me.btnMOR_COMPUTE.Text = "Compute"
        Me.btnMOR_COMPUTE.UseVisualStyleBackColor = True
        '
        'btnMOR_STORE_MIN_ANGLE
        '
        Me.btnMOR_STORE_MIN_ANGLE.Enabled = False
        Me.btnMOR_STORE_MIN_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMOR_STORE_MIN_ANGLE.Location = New System.Drawing.Point(386, 170)
        Me.btnMOR_STORE_MIN_ANGLE.Name = "btnMOR_STORE_MIN_ANGLE"
        Me.btnMOR_STORE_MIN_ANGLE.Size = New System.Drawing.Size(228, 43)
        Me.btnMOR_STORE_MIN_ANGLE.TabIndex = 8
        Me.btnMOR_STORE_MIN_ANGLE.Text = "Store Min Position"
        Me.btnMOR_STORE_MIN_ANGLE.UseVisualStyleBackColor = True
        '
        'btnMOR_STORE_MAX_ANGLE
        '
        Me.btnMOR_STORE_MAX_ANGLE.Enabled = False
        Me.btnMOR_STORE_MAX_ANGLE.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMOR_STORE_MAX_ANGLE.Location = New System.Drawing.Point(386, 28)
        Me.btnMOR_STORE_MAX_ANGLE.Name = "btnMOR_STORE_MAX_ANGLE"
        Me.btnMOR_STORE_MAX_ANGLE.Size = New System.Drawing.Size(228, 43)
        Me.btnMOR_STORE_MAX_ANGLE.TabIndex = 7
        Me.btnMOR_STORE_MAX_ANGLE.Text = "Store Max Position"
        Me.btnMOR_STORE_MAX_ANGLE.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 170)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(120, 29)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "Min Angle"
        '
        'dspMOR_MIN_RIG_POS
        '
        Me.dspMOR_MIN_RIG_POS.Enabled = False
        Me.dspMOR_MIN_RIG_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspMOR_MIN_RIG_POS.Location = New System.Drawing.Point(203, 169)
        Me.dspMOR_MIN_RIG_POS.Name = "dspMOR_MIN_RIG_POS"
        Me.dspMOR_MIN_RIG_POS.Size = New System.Drawing.Size(161, 49)
        Me.dspMOR_MIN_RIG_POS.TabIndex = 5
        '
        'dspMOR_LEVER_POS
        '
        Me.dspMOR_LEVER_POS.Enabled = False
        Me.dspMOR_LEVER_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspMOR_LEVER_POS.Location = New System.Drawing.Point(203, 97)
        Me.dspMOR_LEVER_POS.Name = "dspMOR_LEVER_POS"
        Me.dspMOR_LEVER_POS.Size = New System.Drawing.Size(161, 49)
        Me.dspMOR_LEVER_POS.TabIndex = 4
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(6, 97)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(170, 29)
        Me.Label17.TabIndex = 3
        Me.Label17.Text = "Lever Reading"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(6, 27)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(125, 29)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "Max Angle"
        '
        'dspMOR_MAX_RIG_POS
        '
        Me.dspMOR_MAX_RIG_POS.Enabled = False
        Me.dspMOR_MAX_RIG_POS.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspMOR_MAX_RIG_POS.Location = New System.Drawing.Point(203, 27)
        Me.dspMOR_MAX_RIG_POS.Name = "dspMOR_MAX_RIG_POS"
        Me.dspMOR_MAX_RIG_POS.Size = New System.Drawing.Size(161, 49)
        Me.dspMOR_MAX_RIG_POS.TabIndex = 1
        '
        'lblCopyright
        '
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.lblCopyright.ForeColor = System.Drawing.SystemColors.Control
        Me.lblCopyright.Location = New System.Drawing.Point(13, 816)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(0, 13)
        Me.lblCopyright.TabIndex = 2
        '
        'lblCRIO_PLA_DRIVE_FAULT
        '
        Me.lblCRIO_PLA_DRIVE_FAULT.AutoSize = True
        Me.lblCRIO_PLA_DRIVE_FAULT.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCRIO_PLA_DRIVE_FAULT.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCRIO_PLA_DRIVE_FAULT.Location = New System.Drawing.Point(3, 12)
        Me.lblCRIO_PLA_DRIVE_FAULT.Name = "lblCRIO_PLA_DRIVE_FAULT"
        Me.lblCRIO_PLA_DRIVE_FAULT.Size = New System.Drawing.Size(116, 29)
        Me.lblCRIO_PLA_DRIVE_FAULT.TabIndex = 6
        Me.lblCRIO_PLA_DRIVE_FAULT.Text = "PLA Fault"
        '
        'lblCRIO_PLA_DRIVE_WARNING
        '
        Me.lblCRIO_PLA_DRIVE_WARNING.AutoSize = True
        Me.lblCRIO_PLA_DRIVE_WARNING.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCRIO_PLA_DRIVE_WARNING.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCRIO_PLA_DRIVE_WARNING.Location = New System.Drawing.Point(3, 64)
        Me.lblCRIO_PLA_DRIVE_WARNING.Name = "lblCRIO_PLA_DRIVE_WARNING"
        Me.lblCRIO_PLA_DRIVE_WARNING.Size = New System.Drawing.Size(152, 29)
        Me.lblCRIO_PLA_DRIVE_WARNING.TabIndex = 8
        Me.lblCRIO_PLA_DRIVE_WARNING.Text = "PLA Warning"
        '
        'lblCRIO_CLA_DRIVE_FAULT
        '
        Me.lblCRIO_CLA_DRIVE_FAULT.AutoSize = True
        Me.lblCRIO_CLA_DRIVE_FAULT.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCRIO_CLA_DRIVE_FAULT.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCRIO_CLA_DRIVE_FAULT.Location = New System.Drawing.Point(3, 12)
        Me.lblCRIO_CLA_DRIVE_FAULT.Name = "lblCRIO_CLA_DRIVE_FAULT"
        Me.lblCRIO_CLA_DRIVE_FAULT.Size = New System.Drawing.Size(117, 29)
        Me.lblCRIO_CLA_DRIVE_FAULT.TabIndex = 10
        Me.lblCRIO_CLA_DRIVE_FAULT.Text = "CLA Fault"
        '
        'lblCRIO_CLA_DRIVE_WARNING
        '
        Me.lblCRIO_CLA_DRIVE_WARNING.AutoSize = True
        Me.lblCRIO_CLA_DRIVE_WARNING.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCRIO_CLA_DRIVE_WARNING.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCRIO_CLA_DRIVE_WARNING.Location = New System.Drawing.Point(4, 64)
        Me.lblCRIO_CLA_DRIVE_WARNING.Name = "lblCRIO_CLA_DRIVE_WARNING"
        Me.lblCRIO_CLA_DRIVE_WARNING.Size = New System.Drawing.Size(153, 29)
        Me.lblCRIO_CLA_DRIVE_WARNING.TabIndex = 12
        Me.lblCRIO_CLA_DRIVE_WARNING.Text = "CLA Warning"
        '
        'lblCRIO_MOR_DRIVE_FAULT
        '
        Me.lblCRIO_MOR_DRIVE_FAULT.AutoSize = True
        Me.lblCRIO_MOR_DRIVE_FAULT.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCRIO_MOR_DRIVE_FAULT.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCRIO_MOR_DRIVE_FAULT.Location = New System.Drawing.Point(8, 12)
        Me.lblCRIO_MOR_DRIVE_FAULT.Name = "lblCRIO_MOR_DRIVE_FAULT"
        Me.lblCRIO_MOR_DRIVE_FAULT.Size = New System.Drawing.Size(128, 29)
        Me.lblCRIO_MOR_DRIVE_FAULT.TabIndex = 14
        Me.lblCRIO_MOR_DRIVE_FAULT.Text = "MOR Fault"
        '
        'lblCRIO_MOR_DRIVE_WARNING
        '
        Me.lblCRIO_MOR_DRIVE_WARNING.AutoSize = True
        Me.lblCRIO_MOR_DRIVE_WARNING.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCRIO_MOR_DRIVE_WARNING.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblCRIO_MOR_DRIVE_WARNING.Location = New System.Drawing.Point(8, 64)
        Me.lblCRIO_MOR_DRIVE_WARNING.Name = "lblCRIO_MOR_DRIVE_WARNING"
        Me.lblCRIO_MOR_DRIVE_WARNING.Size = New System.Drawing.Size(164, 29)
        Me.lblCRIO_MOR_DRIVE_WARNING.TabIndex = 16
        Me.lblCRIO_MOR_DRIVE_WARNING.Text = "MOR Warning"
        '
        'lblCustomerSupport
        '
        Me.lblCustomerSupport.AutoSize = True
        Me.lblCustomerSupport.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.lblCustomerSupport.Location = New System.Drawing.Point(240, 816)
        Me.lblCustomerSupport.Name = "lblCustomerSupport"
        Me.lblCustomerSupport.Size = New System.Drawing.Size(276, 13)
        Me.lblCustomerSupport.TabIndex = 20
        Me.lblCustomerSupport.Text = " customer.support@cel-aerospace.ca / +1 450-442-9994"
        '
        'grpFeedbacksServoInstance0
        '
        Me.grpFeedbacksServoInstance0.BackColor = System.Drawing.SystemColors.HighlightText
        Me.grpFeedbacksServoInstance0.Controls.Add(Me.indCRIO_PLA_DRIVE_WARNING)
        Me.grpFeedbacksServoInstance0.Controls.Add(Me.indCRIO_PLA_DRIVE_FAULT)
        Me.grpFeedbacksServoInstance0.Controls.Add(Me.lblCRIO_PLA_DRIVE_FAULT)
        Me.grpFeedbacksServoInstance0.Controls.Add(Me.lblCRIO_PLA_DRIVE_WARNING)
        Me.grpFeedbacksServoInstance0.Location = New System.Drawing.Point(824, 708)
        Me.grpFeedbacksServoInstance0.Name = "grpFeedbacksServoInstance0"
        Me.grpFeedbacksServoInstance0.Size = New System.Drawing.Size(202, 108)
        Me.grpFeedbacksServoInstance0.TabIndex = 21
        Me.grpFeedbacksServoInstance0.TabStop = False
        '
        'grpFeedbacksServoInstance1
        '
        Me.grpFeedbacksServoInstance1.BackColor = System.Drawing.SystemColors.HighlightText
        Me.grpFeedbacksServoInstance1.Controls.Add(Me.lblCRIO_CLA_DRIVE_WARNING)
        Me.grpFeedbacksServoInstance1.Controls.Add(Me.indCRIO_CLA_DRIVE_FAULT)
        Me.grpFeedbacksServoInstance1.Controls.Add(Me.lblCRIO_CLA_DRIVE_FAULT)
        Me.grpFeedbacksServoInstance1.Controls.Add(Me.indCRIO_CLA_DRIVE_WARNING)
        Me.grpFeedbacksServoInstance1.Location = New System.Drawing.Point(1026, 708)
        Me.grpFeedbacksServoInstance1.Name = "grpFeedbacksServoInstance1"
        Me.grpFeedbacksServoInstance1.Size = New System.Drawing.Size(202, 108)
        Me.grpFeedbacksServoInstance1.TabIndex = 22
        Me.grpFeedbacksServoInstance1.TabStop = False
        '
        'grpFeedbacksServoInstance2
        '
        Me.grpFeedbacksServoInstance2.BackColor = System.Drawing.SystemColors.HighlightText
        Me.grpFeedbacksServoInstance2.Controls.Add(Me.indCRIO_MOR_DRIVE_WARNING)
        Me.grpFeedbacksServoInstance2.Controls.Add(Me.indCRIO_MOR_DRIVE_FAULT)
        Me.grpFeedbacksServoInstance2.Controls.Add(Me.lblCRIO_MOR_DRIVE_FAULT)
        Me.grpFeedbacksServoInstance2.Controls.Add(Me.lblCRIO_MOR_DRIVE_WARNING)
        Me.grpFeedbacksServoInstance2.Location = New System.Drawing.Point(1227, 708)
        Me.grpFeedbacksServoInstance2.Name = "grpFeedbacksServoInstance2"
        Me.grpFeedbacksServoInstance2.Size = New System.Drawing.Size(227, 108)
        Me.grpFeedbacksServoInstance2.TabIndex = 23
        Me.grpFeedbacksServoInstance2.TabStop = False
        '
        'lblSimulationMode
        '
        Me.lblSimulationMode.AutoSize = True
        Me.lblSimulationMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSimulationMode.ForeColor = System.Drawing.Color.Maroon
        Me.lblSimulationMode.Location = New System.Drawing.Point(574, 27)
        Me.lblSimulationMode.Name = "lblSimulationMode"
        Me.lblSimulationMode.Size = New System.Drawing.Size(208, 25)
        Me.lblSimulationMode.TabIndex = 24
        Me.lblSimulationMode.Text = "SIMULATION MODE"
        Me.lblSimulationMode.Visible = False
        '
        'MenThrottleSystemStandAlone
        '
        Me.MenThrottleSystemStandAlone.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem, Me.AboutToolStripMenuItem, Me.RunModeToolStripMenuItem})
        Me.MenThrottleSystemStandAlone.Location = New System.Drawing.Point(0, 0)
        Me.MenThrottleSystemStandAlone.Name = "MenThrottleSystemStandAlone"
        Me.MenThrottleSystemStandAlone.Size = New System.Drawing.Size(1472, 24)
        Me.MenThrottleSystemStandAlone.TabIndex = 25
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'RunModeToolStripMenuItem
        '
        Me.RunModeToolStripMenuItem.Name = "RunModeToolStripMenuItem"
        Me.RunModeToolStripMenuItem.Size = New System.Drawing.Size(74, 20)
        Me.RunModeToolStripMenuItem.Text = "Run Mode"
        '
        'txtSystem
        '
        Me.txtSystem.Location = New System.Drawing.Point(418, 737)
        Me.txtSystem.Multiline = True
        Me.txtSystem.Name = "txtSystem"
        Me.txtSystem.Size = New System.Drawing.Size(388, 79)
        Me.txtSystem.TabIndex = 26
        '
        'btnDrivesStatus
        '
        Me.btnDrivesStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDrivesStatus.Location = New System.Drawing.Point(14, 706)
        Me.btnDrivesStatus.Name = "btnDrivesStatus"
        Me.btnDrivesStatus.Size = New System.Drawing.Size(151, 30)
        Me.btnDrivesStatus.TabIndex = 28
        Me.btnDrivesStatus.Text = "Drive Status"
        Me.btnDrivesStatus.UseVisualStyleBackColor = True
        '
        'btnSystem
        '
        Me.btnSystem.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSystem.Location = New System.Drawing.Point(416, 705)
        Me.btnSystem.Name = "btnSystem"
        Me.btnSystem.Size = New System.Drawing.Size(151, 30)
        Me.btnSystem.TabIndex = 29
        Me.btnSystem.Text = "System"
        Me.btnSystem.UseVisualStyleBackColor = True
        '
        'dspRunModePLA
        '
        Me.dspRunModePLA.Enabled = False
        Me.dspRunModePLA.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspRunModePLA.Location = New System.Drawing.Point(243, 285)
        Me.dspRunModePLA.Name = "dspRunModePLA"
        Me.dspRunModePLA.Size = New System.Drawing.Size(300, 80)
        Me.dspRunModePLA.TabIndex = 14
        '
        'lblRunModePLA
        '
        Me.lblRunModePLA.AutoSize = True
        Me.lblRunModePLA.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRunModePLA.Location = New System.Drawing.Point(354, 245)
        Me.lblRunModePLA.Name = "lblRunModePLA"
        Me.lblRunModePLA.Size = New System.Drawing.Size(78, 37)
        Me.lblRunModePLA.TabIndex = 30
        Me.lblRunModePLA.Text = "PLA"
        '
        'dspRunModeCLA
        '
        Me.dspRunModeCLA.Enabled = False
        Me.dspRunModeCLA.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspRunModeCLA.Location = New System.Drawing.Point(565, 285)
        Me.dspRunModeCLA.Name = "dspRunModeCLA"
        Me.dspRunModeCLA.Size = New System.Drawing.Size(300, 80)
        Me.dspRunModeCLA.TabIndex = 31
        '
        'lblRunModeCLA
        '
        Me.lblRunModeCLA.AutoSize = True
        Me.lblRunModeCLA.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRunModeCLA.Location = New System.Drawing.Point(668, 245)
        Me.lblRunModeCLA.Name = "lblRunModeCLA"
        Me.lblRunModeCLA.Size = New System.Drawing.Size(80, 37)
        Me.lblRunModeCLA.TabIndex = 32
        Me.lblRunModeCLA.Text = "CLA"
        '
        'dspRunModeMOR
        '
        Me.dspRunModeMOR.Enabled = False
        Me.dspRunModeMOR.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dspRunModeMOR.Location = New System.Drawing.Point(883, 285)
        Me.dspRunModeMOR.Name = "dspRunModeMOR"
        Me.dspRunModeMOR.Size = New System.Drawing.Size(300, 80)
        Me.dspRunModeMOR.TabIndex = 33
        '
        'lblRunModeMOR
        '
        Me.lblRunModeMOR.AutoSize = True
        Me.lblRunModeMOR.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRunModeMOR.Location = New System.Drawing.Point(982, 245)
        Me.lblRunModeMOR.Name = "lblRunModeMOR"
        Me.lblRunModeMOR.Size = New System.Drawing.Size(90, 37)
        Me.lblRunModeMOR.TabIndex = 34
        Me.lblRunModeMOR.Text = "MOR"
        '
        'lblPLAReturnToIDLECommanded
        '
        Me.lblPLAReturnToIDLECommanded.AutoSize = True
        Me.lblPLAReturnToIDLECommanded.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLAReturnToIDLECommanded.Location = New System.Drawing.Point(272, 371)
        Me.lblPLAReturnToIDLECommanded.Name = "lblPLAReturnToIDLECommanded"
        Me.lblPLAReturnToIDLECommanded.Size = New System.Drawing.Size(225, 29)
        Me.lblPLAReturnToIDLECommanded.TabIndex = 15
        Me.lblPLAReturnToIDLECommanded.Text = "Return to IDLE Cmd"
        '
        'lblPLA_ThrottleRearming
        '
        Me.lblPLA_ThrottleRearming.AutoSize = True
        Me.lblPLA_ThrottleRearming.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPLA_ThrottleRearming.Location = New System.Drawing.Point(290, 417)
        Me.lblPLA_ThrottleRearming.Name = "lblPLA_ThrottleRearming"
        Me.lblPLA_ThrottleRearming.Size = New System.Drawing.Size(207, 29)
        Me.lblPLA_ThrottleRearming.TabIndex = 38
        Me.lblPLA_ThrottleRearming.Text = "Throttle Rearming"
        '
        'lblCLAReturnToIDLECommanded
        '
        Me.lblCLAReturnToIDLECommanded.AutoSize = True
        Me.lblCLAReturnToIDLECommanded.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCLAReturnToIDLECommanded.Location = New System.Drawing.Point(593, 371)
        Me.lblCLAReturnToIDLECommanded.Name = "lblCLAReturnToIDLECommanded"
        Me.lblCLAReturnToIDLECommanded.Size = New System.Drawing.Size(225, 29)
        Me.lblCLAReturnToIDLECommanded.TabIndex = 42
        Me.lblCLAReturnToIDLECommanded.Text = "Return to IDLE Cmd"
        '
        'lblCLA_ThrottleRearming
        '
        Me.lblCLA_ThrottleRearming.AutoSize = True
        Me.lblCLA_ThrottleRearming.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCLA_ThrottleRearming.Location = New System.Drawing.Point(611, 421)
        Me.lblCLA_ThrottleRearming.Name = "lblCLA_ThrottleRearming"
        Me.lblCLA_ThrottleRearming.Size = New System.Drawing.Size(207, 29)
        Me.lblCLA_ThrottleRearming.TabIndex = 38
        Me.lblCLA_ThrottleRearming.Text = "Throttle Rearming"
        '
        'indCRIO_MOR_DRIVE_WARNING
        '
        Me.indCRIO_MOR_DRIVE_WARNING.Color = System.Drawing.Color.Yellow
        Me.indCRIO_MOR_DRIVE_WARNING.Location = New System.Drawing.Point(178, 57)
        Me.indCRIO_MOR_DRIVE_WARNING.Name = "indCRIO_MOR_DRIVE_WARNING"
        Me.indCRIO_MOR_DRIVE_WARNING.On = False
        Me.indCRIO_MOR_DRIVE_WARNING.Size = New System.Drawing.Size(40, 40)
        Me.indCRIO_MOR_DRIVE_WARNING.TabIndex = 15
        Me.indCRIO_MOR_DRIVE_WARNING.Text = "MOR Warning"
        '
        'indCRIO_MOR_DRIVE_FAULT
        '
        Me.indCRIO_MOR_DRIVE_FAULT.Color = System.Drawing.Color.Red
        Me.indCRIO_MOR_DRIVE_FAULT.Location = New System.Drawing.Point(178, 6)
        Me.indCRIO_MOR_DRIVE_FAULT.Name = "indCRIO_MOR_DRIVE_FAULT"
        Me.indCRIO_MOR_DRIVE_FAULT.On = False
        Me.indCRIO_MOR_DRIVE_FAULT.Size = New System.Drawing.Size(40, 40)
        Me.indCRIO_MOR_DRIVE_FAULT.TabIndex = 13
        Me.indCRIO_MOR_DRIVE_FAULT.Text = "MOR Fault"
        '
        'indCRIO_CLA_DRIVE_FAULT
        '
        Me.indCRIO_CLA_DRIVE_FAULT.Color = System.Drawing.Color.Red
        Me.indCRIO_CLA_DRIVE_FAULT.Location = New System.Drawing.Point(156, 6)
        Me.indCRIO_CLA_DRIVE_FAULT.Name = "indCRIO_CLA_DRIVE_FAULT"
        Me.indCRIO_CLA_DRIVE_FAULT.On = False
        Me.indCRIO_CLA_DRIVE_FAULT.Size = New System.Drawing.Size(40, 40)
        Me.indCRIO_CLA_DRIVE_FAULT.TabIndex = 9
        Me.indCRIO_CLA_DRIVE_FAULT.Text = "CLA Fault"
        '
        'indCRIO_CLA_DRIVE_WARNING
        '
        Me.indCRIO_CLA_DRIVE_WARNING.Color = System.Drawing.Color.Yellow
        Me.indCRIO_CLA_DRIVE_WARNING.Location = New System.Drawing.Point(156, 57)
        Me.indCRIO_CLA_DRIVE_WARNING.Name = "indCRIO_CLA_DRIVE_WARNING"
        Me.indCRIO_CLA_DRIVE_WARNING.On = False
        Me.indCRIO_CLA_DRIVE_WARNING.Size = New System.Drawing.Size(40, 40)
        Me.indCRIO_CLA_DRIVE_WARNING.TabIndex = 11
        Me.indCRIO_CLA_DRIVE_WARNING.Text = "CLA Warning"
        '
        'indCRIO_PLA_DRIVE_WARNING
        '
        Me.indCRIO_PLA_DRIVE_WARNING.Color = System.Drawing.Color.Yellow
        Me.indCRIO_PLA_DRIVE_WARNING.Location = New System.Drawing.Point(152, 57)
        Me.indCRIO_PLA_DRIVE_WARNING.Name = "indCRIO_PLA_DRIVE_WARNING"
        Me.indCRIO_PLA_DRIVE_WARNING.On = False
        Me.indCRIO_PLA_DRIVE_WARNING.Size = New System.Drawing.Size(40, 40)
        Me.indCRIO_PLA_DRIVE_WARNING.TabIndex = 7
        Me.indCRIO_PLA_DRIVE_WARNING.Text = "PLA Warning"
        '
        'indCRIO_PLA_DRIVE_FAULT
        '
        Me.indCRIO_PLA_DRIVE_FAULT.Color = System.Drawing.Color.Red
        Me.indCRIO_PLA_DRIVE_FAULT.Location = New System.Drawing.Point(152, 6)
        Me.indCRIO_PLA_DRIVE_FAULT.Name = "indCRIO_PLA_DRIVE_FAULT"
        Me.indCRIO_PLA_DRIVE_FAULT.On = False
        Me.indCRIO_PLA_DRIVE_FAULT.Size = New System.Drawing.Size(40, 40)
        Me.indCRIO_PLA_DRIVE_FAULT.TabIndex = 5
        Me.indCRIO_PLA_DRIVE_FAULT.Text = "PLA Fault"
        '
        'indPLA_MIN_STOP_POS_DONE
        '
        Me.indPLA_MIN_STOP_POS_DONE.Location = New System.Drawing.Point(294, 259)
        Me.indPLA_MIN_STOP_POS_DONE.Name = "indPLA_MIN_STOP_POS_DONE"
        Me.indPLA_MIN_STOP_POS_DONE.On = False
        Me.indPLA_MIN_STOP_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indPLA_MIN_STOP_POS_DONE.TabIndex = 11
        Me.indPLA_MIN_STOP_POS_DONE.Text = "LedBulb1"
        '
        'indPLA_IDLE_POS_DONE
        '
        Me.indPLA_IDLE_POS_DONE.Location = New System.Drawing.Point(294, 192)
        Me.indPLA_IDLE_POS_DONE.Name = "indPLA_IDLE_POS_DONE"
        Me.indPLA_IDLE_POS_DONE.On = False
        Me.indPLA_IDLE_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indPLA_IDLE_POS_DONE.TabIndex = 7
        Me.indPLA_IDLE_POS_DONE.Text = "LedBulb1"
        '
        'indPLA_MAX_STOP_POS_DONE
        '
        Me.indPLA_MAX_STOP_POS_DONE.Location = New System.Drawing.Point(294, 130)
        Me.indPLA_MAX_STOP_POS_DONE.Name = "indPLA_MAX_STOP_POS_DONE"
        Me.indPLA_MAX_STOP_POS_DONE.On = False
        Me.indPLA_MAX_STOP_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indPLA_MAX_STOP_POS_DONE.TabIndex = 3
        Me.indPLA_MAX_STOP_POS_DONE.Text = "LedBulb1"
        '
        'indPLA_RIGGING_DONE
        '
        Me.indPLA_RIGGING_DONE.Location = New System.Drawing.Point(1325, 31)
        Me.indPLA_RIGGING_DONE.Name = "indPLA_RIGGING_DONE"
        Me.indPLA_RIGGING_DONE.On = False
        Me.indPLA_RIGGING_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indPLA_RIGGING_DONE.TabIndex = 14
        Me.indPLA_RIGGING_DONE.Text = "LedBulb1"
        '
        'indPLA_COMPUTE_DONE
        '
        Me.indPLA_COMPUTE_DONE.Location = New System.Drawing.Point(936, 30)
        Me.indPLA_COMPUTE_DONE.Name = "indPLA_COMPUTE_DONE"
        Me.indPLA_COMPUTE_DONE.On = False
        Me.indPLA_COMPUTE_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indPLA_COMPUTE_DONE.TabIndex = 13
        Me.indPLA_COMPUTE_DONE.Text = "LedBulb1"
        '
        'indPLA_MIN_POS_DONE
        '
        Me.indPLA_MIN_POS_DONE.Location = New System.Drawing.Point(616, 173)
        Me.indPLA_MIN_POS_DONE.Name = "indPLA_MIN_POS_DONE"
        Me.indPLA_MIN_POS_DONE.On = False
        Me.indPLA_MIN_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indPLA_MIN_POS_DONE.TabIndex = 10
        Me.indPLA_MIN_POS_DONE.Text = "LedBulb1"
        '
        'indPLA_MAX_POS_DONE
        '
        Me.indPLA_MAX_POS_DONE.Location = New System.Drawing.Point(616, 29)
        Me.indPLA_MAX_POS_DONE.Name = "indPLA_MAX_POS_DONE"
        Me.indPLA_MAX_POS_DONE.On = False
        Me.indPLA_MAX_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indPLA_MAX_POS_DONE.TabIndex = 9
        Me.indPLA_MAX_POS_DONE.Text = "LedBulb1"
        '
        'indCLA_MIN_STOP_POS_DONE
        '
        Me.indCLA_MIN_STOP_POS_DONE.Location = New System.Drawing.Point(294, 259)
        Me.indCLA_MIN_STOP_POS_DONE.Name = "indCLA_MIN_STOP_POS_DONE"
        Me.indCLA_MIN_STOP_POS_DONE.On = False
        Me.indCLA_MIN_STOP_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indCLA_MIN_STOP_POS_DONE.TabIndex = 11
        Me.indCLA_MIN_STOP_POS_DONE.Text = "LedBulb1"
        '
        'indCLA_IDLE_POS_DONE
        '
        Me.indCLA_IDLE_POS_DONE.Location = New System.Drawing.Point(294, 192)
        Me.indCLA_IDLE_POS_DONE.Name = "indCLA_IDLE_POS_DONE"
        Me.indCLA_IDLE_POS_DONE.On = False
        Me.indCLA_IDLE_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indCLA_IDLE_POS_DONE.TabIndex = 7
        Me.indCLA_IDLE_POS_DONE.Text = "LedBulb1"
        '
        'indCLA_MAX_STOP_POS_DONE
        '
        Me.indCLA_MAX_STOP_POS_DONE.Location = New System.Drawing.Point(294, 130)
        Me.indCLA_MAX_STOP_POS_DONE.Name = "indCLA_MAX_STOP_POS_DONE"
        Me.indCLA_MAX_STOP_POS_DONE.On = False
        Me.indCLA_MAX_STOP_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indCLA_MAX_STOP_POS_DONE.TabIndex = 3
        Me.indCLA_MAX_STOP_POS_DONE.Text = "LedBulb1"
        '
        'indCLA_RIGGING_DONE
        '
        Me.indCLA_RIGGING_DONE.Location = New System.Drawing.Point(1325, 31)
        Me.indCLA_RIGGING_DONE.Name = "indCLA_RIGGING_DONE"
        Me.indCLA_RIGGING_DONE.On = False
        Me.indCLA_RIGGING_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indCLA_RIGGING_DONE.TabIndex = 14
        Me.indCLA_RIGGING_DONE.Text = "LedBulb1"
        '
        'indCLA_COMPUTE_DONE
        '
        Me.indCLA_COMPUTE_DONE.Location = New System.Drawing.Point(936, 30)
        Me.indCLA_COMPUTE_DONE.Name = "indCLA_COMPUTE_DONE"
        Me.indCLA_COMPUTE_DONE.On = False
        Me.indCLA_COMPUTE_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indCLA_COMPUTE_DONE.TabIndex = 13
        Me.indCLA_COMPUTE_DONE.Text = "LedBulb1"
        '
        'indCLA_MIN_POS_DONE
        '
        Me.indCLA_MIN_POS_DONE.Location = New System.Drawing.Point(616, 173)
        Me.indCLA_MIN_POS_DONE.Name = "indCLA_MIN_POS_DONE"
        Me.indCLA_MIN_POS_DONE.On = False
        Me.indCLA_MIN_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indCLA_MIN_POS_DONE.TabIndex = 10
        Me.indCLA_MIN_POS_DONE.Text = "LedBulb1"
        '
        'indCLA_MAX_POS_DONE
        '
        Me.indCLA_MAX_POS_DONE.Location = New System.Drawing.Point(616, 29)
        Me.indCLA_MAX_POS_DONE.Name = "indCLA_MAX_POS_DONE"
        Me.indCLA_MAX_POS_DONE.On = False
        Me.indCLA_MAX_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indCLA_MAX_POS_DONE.TabIndex = 9
        Me.indCLA_MAX_POS_DONE.Text = "LedBulb1"
        '
        'indMOR_MIN_STOP_POS_DONE
        '
        Me.indMOR_MIN_STOP_POS_DONE.Location = New System.Drawing.Point(294, 259)
        Me.indMOR_MIN_STOP_POS_DONE.Name = "indMOR_MIN_STOP_POS_DONE"
        Me.indMOR_MIN_STOP_POS_DONE.On = False
        Me.indMOR_MIN_STOP_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indMOR_MIN_STOP_POS_DONE.TabIndex = 11
        Me.indMOR_MIN_STOP_POS_DONE.Text = "LedBulb1"
        '
        'indMOR_IDLE_POS_DONE
        '
        Me.indMOR_IDLE_POS_DONE.Location = New System.Drawing.Point(294, 192)
        Me.indMOR_IDLE_POS_DONE.Name = "indMOR_IDLE_POS_DONE"
        Me.indMOR_IDLE_POS_DONE.On = False
        Me.indMOR_IDLE_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indMOR_IDLE_POS_DONE.TabIndex = 7
        Me.indMOR_IDLE_POS_DONE.Text = "LedBulb1"
        '
        'indMOR_MAX_STOP_POS_DONE
        '
        Me.indMOR_MAX_STOP_POS_DONE.Location = New System.Drawing.Point(294, 130)
        Me.indMOR_MAX_STOP_POS_DONE.Name = "indMOR_MAX_STOP_POS_DONE"
        Me.indMOR_MAX_STOP_POS_DONE.On = False
        Me.indMOR_MAX_STOP_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indMOR_MAX_STOP_POS_DONE.TabIndex = 3
        Me.indMOR_MAX_STOP_POS_DONE.Text = "LedBulb1"
        '
        'indMOR_RIGGING_DONE
        '
        Me.indMOR_RIGGING_DONE.Location = New System.Drawing.Point(1325, 31)
        Me.indMOR_RIGGING_DONE.Name = "indMOR_RIGGING_DONE"
        Me.indMOR_RIGGING_DONE.On = False
        Me.indMOR_RIGGING_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indMOR_RIGGING_DONE.TabIndex = 14
        Me.indMOR_RIGGING_DONE.Text = "LedBulb1"
        '
        'indMOR_COMPUTE_DONE
        '
        Me.indMOR_COMPUTE_DONE.Location = New System.Drawing.Point(936, 30)
        Me.indMOR_COMPUTE_DONE.Name = "indMOR_COMPUTE_DONE"
        Me.indMOR_COMPUTE_DONE.On = False
        Me.indMOR_COMPUTE_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indMOR_COMPUTE_DONE.TabIndex = 13
        Me.indMOR_COMPUTE_DONE.Text = "LedBulb1"
        '
        'indMOR_MIN_POS_DONE
        '
        Me.indMOR_MIN_POS_DONE.Location = New System.Drawing.Point(616, 173)
        Me.indMOR_MIN_POS_DONE.Name = "indMOR_MIN_POS_DONE"
        Me.indMOR_MIN_POS_DONE.On = False
        Me.indMOR_MIN_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indMOR_MIN_POS_DONE.TabIndex = 10
        Me.indMOR_MIN_POS_DONE.Text = "LedBulb1"
        '
        'indMOR_MAX_POS_DONE
        '
        Me.indMOR_MAX_POS_DONE.Location = New System.Drawing.Point(616, 29)
        Me.indMOR_MAX_POS_DONE.Name = "indMOR_MAX_POS_DONE"
        Me.indMOR_MAX_POS_DONE.On = False
        Me.indMOR_MAX_POS_DONE.Size = New System.Drawing.Size(40, 40)
        Me.indMOR_MAX_POS_DONE.TabIndex = 9
        Me.indMOR_MAX_POS_DONE.Text = "LedBulb1"
        '
        'indCRIO_CONNECTED
        '
        Me.indCRIO_CONNECTED.Location = New System.Drawing.Point(1365, 27)
        Me.indCRIO_CONNECTED.Name = "indCRIO_CONNECTED"
        Me.indCRIO_CONNECTED.On = False
        Me.indCRIO_CONNECTED.Size = New System.Drawing.Size(40, 40)
        Me.indCRIO_CONNECTED.TabIndex = 1
        '
        'indCLA_ThrottleRearming
        '
        Me.indCLA_ThrottleRearming.Location = New System.Drawing.Point(825, 417)
        Me.indCLA_ThrottleRearming.Name = "indCLA_ThrottleRearming"
        Me.indCLA_ThrottleRearming.On = False
        Me.indCLA_ThrottleRearming.Size = New System.Drawing.Size(40, 40)
        Me.indCLA_ThrottleRearming.TabIndex = 41
        Me.indCLA_ThrottleRearming.Text = "lblPLAThrottleRearming"
        '
        'indCLA_ReturnToIDLECommanded
        '
        Me.indCLA_ReturnToIDLECommanded.Location = New System.Drawing.Point(824, 371)
        Me.indCLA_ReturnToIDLECommanded.Name = "indCLA_ReturnToIDLECommanded"
        Me.indCLA_ReturnToIDLECommanded.On = False
        Me.indCLA_ReturnToIDLECommanded.Size = New System.Drawing.Size(40, 40)
        Me.indCLA_ReturnToIDLECommanded.TabIndex = 39
        Me.indCLA_ReturnToIDLECommanded.Text = "LedBulb1"
        '
        'indPLA_ThrottleRearming
        '
        Me.indPLA_ThrottleRearming.Location = New System.Drawing.Point(503, 417)
        Me.indPLA_ThrottleRearming.Name = "indPLA_ThrottleRearming"
        Me.indPLA_ThrottleRearming.On = False
        Me.indPLA_ThrottleRearming.Size = New System.Drawing.Size(40, 40)
        Me.indPLA_ThrottleRearming.TabIndex = 37
        Me.indPLA_ThrottleRearming.Text = "lblPLAThrottleRearming"
        '
        'indPLA_ReturnToIDLECommanded
        '
        Me.indPLA_ReturnToIDLECommanded.Location = New System.Drawing.Point(503, 371)
        Me.indPLA_ReturnToIDLECommanded.Name = "indPLA_ReturnToIDLECommanded"
        Me.indPLA_ReturnToIDLECommanded.On = False
        Me.indPLA_ReturnToIDLECommanded.Size = New System.Drawing.Size(40, 40)
        Me.indPLA_ReturnToIDLECommanded.TabIndex = 14
        Me.indPLA_ReturnToIDLECommanded.Text = "LedBulb1"
        '
        'frmThrottleSystemStandAlone
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.ClientSize = New System.Drawing.Size(1472, 832)
        Me.Controls.Add(Me.btnSystem)
        Me.Controls.Add(Me.btnDrivesStatus)
        Me.Controls.Add(Me.txtSystem)
        Me.Controls.Add(Me.lblSimulationMode)
        Me.Controls.Add(Me.grpFeedbacksServoInstance2)
        Me.Controls.Add(Me.grpFeedbacksServoInstance1)
        Me.Controls.Add(Me.grpFeedbacksServoInstance0)
        Me.Controls.Add(Me.lblCustomerSupport)
        Me.Controls.Add(Me.tabThrottles)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.txtDrivesStatus)
        Me.Controls.Add(Me.lblCRIO_CONNECTED)
        Me.Controls.Add(Me.indCRIO_CONNECTED)
        Me.Controls.Add(Me.MenThrottleSystemStandAlone)
        Me.Controls.Add(Me.dspRunModePLA)
        Me.Controls.Add(Me.lblRunModePLA)
        Me.Controls.Add(Me.dspRunModeCLA)
        Me.Controls.Add(Me.lblRunModeCLA)
        Me.Controls.Add(Me.dspRunModeMOR)
        Me.Controls.Add(Me.lblRunModeMOR)
        Me.Controls.Add(Me.indCLA_ThrottleRearming)
        Me.Controls.Add(Me.indCLA_ReturnToIDLECommanded)
        Me.Controls.Add(Me.lblCLA_ThrottleRearming)
        Me.Controls.Add(Me.lblPLA_ThrottleRearming)
        Me.Controls.Add(Me.indPLA_ThrottleRearming)
        Me.Controls.Add(Me.lblPLAReturnToIDLECommanded)
        Me.Controls.Add(Me.indPLA_ReturnToIDLECommanded)
        Me.Controls.Add(Me.lblCLAReturnToIDLECommanded)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmThrottleSystemStandAlone"
        Me.Text = "CEL Throttle System Stand Alone"
        Me.tabThrottles.ResumeLayout(False)
        Me.tabPLA.ResumeLayout(False)
        Me.tabPLA.PerformLayout()
        Me.grpPlaThrottleSecurityLimits.ResumeLayout(False)
        Me.grpPlaThrottleSecurityLimits.PerformLayout()
        Me.grpPlaServoControl.ResumeLayout(False)
        Me.grpPlaServoControl.PerformLayout()
        Me.grpPlaUserLeverAngles.ResumeLayout(False)
        Me.grpPlaUserLeverAngles.PerformLayout()
        Me.tabCLA.ResumeLayout(False)
        Me.tabCLA.PerformLayout()
        Me.grpClaThrottleSecurityLimits.ResumeLayout(False)
        Me.grpClaThrottleSecurityLimits.PerformLayout()
        Me.grpClaServoControl.ResumeLayout(False)
        Me.grpClaServoControl.PerformLayout()
        Me.grpClaUserLeverAngles.ResumeLayout(False)
        Me.grpClaUserLeverAngles.PerformLayout()
        Me.tabMOR.ResumeLayout(False)
        Me.tabMOR.PerformLayout()
        Me.grpMorThrottleSecurityLimits.ResumeLayout(False)
        Me.grpMorThrottleSecurityLimits.PerformLayout()
        Me.grpMORServoControl.ResumeLayout(False)
        Me.grpMORServoControl.PerformLayout()
        Me.grpMORUserLeverAngles.ResumeLayout(False)
        Me.grpMORUserLeverAngles.PerformLayout()
        Me.grpFeedbacksServoInstance0.ResumeLayout(False)
        Me.grpFeedbacksServoInstance0.PerformLayout()
        Me.grpFeedbacksServoInstance1.ResumeLayout(False)
        Me.grpFeedbacksServoInstance1.PerformLayout()
        Me.grpFeedbacksServoInstance2.ResumeLayout(False)
        Me.grpFeedbacksServoInstance2.PerformLayout()
        Me.MenThrottleSystemStandAlone.ResumeLayout(False)
        Me.MenThrottleSystemStandAlone.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents indCRIO_CONNECTED As Bulb.LedBulb
    Friend WithEvents lblCRIO_CONNECTED As Label
    Friend WithEvents txtDrivesStatus As TextBox
    Friend WithEvents tabThrottles As TabControl
    Friend WithEvents tabPLA As TabPage
    Friend WithEvents grpPlaUserLeverAngles As GroupBox
    Friend WithEvents tabCLA As TabPage
    Friend WithEvents dspPLA_MAX_RIG_POS As TextBox
    Friend WithEvents lblPLA_LEVER_POS As Label
    Friend WithEvents lblPLA_MAX_RIG_POS As Label
    Friend WithEvents dspPLA_LEVER_POS As TextBox
    Friend WithEvents grpPlaThrottleSecurityLimits As GroupBox
    Friend WithEvents lblCopyright As Label
    Friend WithEvents grpPlaServoControl As GroupBox
    Friend WithEvents lblPLA_SERVO_READING As Label
    Friend WithEvents dspPLA_SERVO_READING As TextBox
    Friend WithEvents indPLA_COMPUTE_DONE As Bulb.LedBulb
    Friend WithEvents btnPLA_RESET As Button
    Friend WithEvents btnPLA_COMPUTE As Button
    Friend WithEvents indPLA_MIN_POS_DONE As Bulb.LedBulb
    Friend WithEvents indPLA_MAX_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnPLA_STORE_MIN_ANGLE As Button
    Friend WithEvents btnPLA_STORE_MAX_ANGLE As Button
    Friend WithEvents lblPLA_MIN_RIG_POS As Label
    Friend WithEvents dspPLA_MIN_RIG_POS As TextBox
    Friend WithEvents lblPLA_MIN_STOP_POS As Label
    Friend WithEvents indPLA_MIN_STOP_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnPLA_STORE_MIN_STOP_ANGLE As Button
    Friend WithEvents lblPLA_IDLE_POS_DONE As Label
    Friend WithEvents indPLA_IDLE_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnPLA_STORE_IDLE_ANGLE As Button
    Friend WithEvents dspPLA_MAX_STOP_POS As TextBox
    Friend WithEvents lblPLA_MAX_STOP_POS_DEG As Label
    Friend WithEvents indPLA_MAX_STOP_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnPLA_STORE_MAX_STOP_ANGLE As Button
    Friend WithEvents lblPLA_LEVER_ANGLE_READING As Label
    Friend WithEvents dspPLA_SERVO_ANGLE_READING As TextBox
    Friend WithEvents tabMOR As TabPage
    Friend WithEvents indCRIO_PLA_DRIVE_FAULT As Bulb.LedBulb
    Friend WithEvents lblCRIO_PLA_DRIVE_FAULT As Label
    Friend WithEvents indCRIO_PLA_DRIVE_WARNING As Bulb.LedBulb
    Friend WithEvents lblCRIO_PLA_DRIVE_WARNING As Label
    Friend WithEvents indCRIO_CLA_DRIVE_FAULT As Bulb.LedBulb
    Friend WithEvents lblCRIO_CLA_DRIVE_FAULT As Label
    Friend WithEvents indCRIO_CLA_DRIVE_WARNING As Bulb.LedBulb
    Friend WithEvents lblCRIO_CLA_DRIVE_WARNING As Label
    Friend WithEvents indCRIO_MOR_DRIVE_FAULT As Bulb.LedBulb
    Friend WithEvents lblCRIO_MOR_DRIVE_FAULT As Label
    Friend WithEvents indCRIO_MOR_DRIVE_WARNING As Bulb.LedBulb
    Friend WithEvents lblCRIO_MOR_DRIVE_WARNING As Label
    Friend WithEvents lblPLA_RIGGING_DONE As Label
    Friend WithEvents grpClaThrottleSecurityLimits As GroupBox
    Friend WithEvents dspCLA_MIN_STOP_POS As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents indCLA_MIN_STOP_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnCLA_STORE_MIN_STOP_ANGLE As Button
    Friend WithEvents dspCLA_IDLE_POS As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents indCLA_IDLE_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnCLA_STORE_IDLE_ANGLE As Button
    Friend WithEvents dspCLA_MAX_STOP_POS As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents indCLA_MAX_STOP_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnCLA_STORE_MAX_STOP_ANGLE As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents dspCLA_SERVO_ANGLE_READING As TextBox
    Friend WithEvents grpClaServoControl As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents dspCLA_SERVO_READING As TextBox
    Friend WithEvents grpClaUserLeverAngles As GroupBox
    Friend WithEvents lblCLA_RIGGING_DONE As Label
    Friend WithEvents indCLA_RIGGING_DONE As Bulb.LedBulb
    Friend WithEvents indCLA_COMPUTE_DONE As Bulb.LedBulb
    Friend WithEvents btnCLA_RESET As Button
    Friend WithEvents btnCLA_COMPUTE As Button
    Friend WithEvents indCLA_MIN_POS_DONE As Bulb.LedBulb
    Friend WithEvents indCLA_MAX_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnCLA_STORE_MIN_ANGLE As Button
    Friend WithEvents btnCLA_STORE_MAX_ANGLE As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents dspCLA_MIN_RIG_POS As TextBox
    Friend WithEvents dspCLA_LEVER_POS As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents dspCLA_MAX_RIG_POS As TextBox
    Friend WithEvents grpMorThrottleSecurityLimits As GroupBox
    Friend WithEvents dspMOR_MIN_STOP_POS As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents indMOR_MIN_STOP_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnMOR_STORE_MIN_STOP_ANGLE As Button
    Friend WithEvents dspMOR_IDLE_POS As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents indMOR_IDLE_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnMOR_STORE_IDLE_ANGLE As Button
    Friend WithEvents dspMOR_MAX_STOP_POS As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents indMOR_MAX_STOP_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnMOR_STORE_MAX_STOP_ANGLE As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents dspMOR_SERVO_ANGLE_READING As TextBox
    Friend WithEvents grpMORServoControl As GroupBox
    Friend WithEvents Label14 As Label
    Friend WithEvents dspMOR_SERVO_READING As TextBox
    Friend WithEvents grpMORUserLeverAngles As GroupBox
    Friend WithEvents Label15 As Label
    Friend WithEvents indMOR_RIGGING_DONE As Bulb.LedBulb
    Friend WithEvents indMOR_COMPUTE_DONE As Bulb.LedBulb
    Friend WithEvents btnMOR_RESET As Button
    Friend WithEvents btnMOR_COMPUTE As Button
    Friend WithEvents indMOR_MIN_POS_DONE As Bulb.LedBulb
    Friend WithEvents indMOR_MAX_POS_DONE As Bulb.LedBulb
    Friend WithEvents btnMOR_STORE_MIN_ANGLE As Button
    Friend WithEvents btnMOR_STORE_MAX_ANGLE As Button
    Friend WithEvents Label16 As Label
    Friend WithEvents dspMOR_MIN_RIG_POS As TextBox
    Friend WithEvents dspMOR_LEVER_POS As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents dspMOR_MAX_RIG_POS As TextBox
    Friend WithEvents txtPlaInstructions As TextBox
    Friend WithEvents lblPlaInstructions As Label
    Friend WithEvents lblClaInstructions As Label
    Friend WithEvents txtCLAInstructions As TextBox
    Friend WithEvents lblMORInstructions As Label
    Friend WithEvents txtMORInstructions As TextBox
    Friend WithEvents chkPLA_ENGAGE As CheckBox
    Friend WithEvents chkCLA_ENGAGE As CheckBox
    Friend WithEvents chkMOR_ENGAGE As CheckBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblCustomerSupport As Label
    Friend WithEvents grpFeedbacksServoInstance0 As GroupBox
    Friend WithEvents grpFeedbacksServoInstance1 As GroupBox
    Friend WithEvents grpFeedbacksServoInstance2 As GroupBox
    Friend WithEvents lblSimulationMode As Label
    Friend WithEvents chkPLAHardwareEnable As CheckBox
    Friend WithEvents chkCLAHardwareEnable As CheckBox
    Friend WithEvents chkMORHardwareEnable As CheckBox
    Friend WithEvents MenThrottleSystemStandAlone As MenuStrip
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents txtSystem As TextBox
    Friend WithEvents btnDrivesStatus As Button
    Friend WithEvents btnSystem As Button
    Friend WithEvents RunModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents dspRunModePLA As TextBox
    Friend WithEvents lblRunModePLA As Label
    Friend WithEvents dspRunModeCLA As TextBox
    Friend WithEvents lblRunModeCLA As Label
    Friend WithEvents dspRunModeMOR As TextBox
    Friend WithEvents lblRunModeMOR As Label
    Friend WithEvents dspPLA_MIN_STOP_POS As TextBox
    Friend WithEvents dspPLA_IDLE_POS As TextBox
    Friend WithEvents indPLA_RIGGING_DONE As Bulb.LedBulb
    Friend WithEvents indPLA_ReturnToIDLECommanded As Bulb.LedBulb
    Friend WithEvents lblPLAReturnToIDLECommanded As Label
    Friend WithEvents indPLA_ThrottleRearming As Bulb.LedBulb
    Friend WithEvents lblPLA_ThrottleRearming As Label
    Friend WithEvents indCLA_ReturnToIDLECommanded As Bulb.LedBulb
    Friend WithEvents indCLA_ThrottleRearming As Bulb.LedBulb
    Friend WithEvents lblCLAReturnToIDLECommanded As Label
    Friend WithEvents lblCLA_ThrottleRearming As Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FTP
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
        Me.btnFileDetailList = New System.Windows.Forms.Button()
        Me.btnLstFiles = New System.Windows.Forms.Button()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.lstFiles = New System.Windows.Forms.ListBox()
        Me.txtServerIP = New System.Windows.Forms.TextBox()
        Me.btnFTPSave = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.groupBox5 = New System.Windows.Forms.GroupBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.txtNewFilename = New System.Windows.Forms.TextBox()
        Me.txtCurrentFilename = New System.Windows.Forms.TextBox()
        Me.button1 = New System.Windows.Forms.Button()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.groupBox7 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDirectory = New System.Windows.Forms.TextBox()
        Me.groupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtNewDir = New System.Windows.Forms.TextBox()
        Me.btnewDir = New System.Windows.Forms.Button()
        Me.grpBoxUpload = New System.Windows.Forms.GroupBox()
        Me.txtUpload = New System.Windows.Forms.TextBox()
        Me.btnFileSize = New System.Windows.Forms.Button()
        Me.btnUpload = New System.Windows.Forms.Button()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.btnDownloadCeldasSo = New System.Windows.Forms.Button()
        Me.btnDeleteAppOnTarget = New System.Windows.Forms.Button()
        Me.txtIPTarget1 = New System.Windows.Forms.TextBox()
        Me.btnUploadCsvOnTarget = New System.Windows.Forms.Button()
        Me.btnUploadCeldasSoOnTarget = New System.Windows.Forms.Button()
        Me.txtIPTarget2 = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.BtnDownloadAndUpload = New System.Windows.Forms.Button()
        Me.groupBox2.SuspendLayout()
        Me.groupBox5.SuspendLayout()
        Me.groupBox7.SuspendLayout()
        Me.groupBox6.SuspendLayout()
        Me.grpBoxUpload.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnFileDetailList
        '
        Me.btnFileDetailList.Location = New System.Drawing.Point(7, 243)
        Me.btnFileDetailList.Name = "btnFileDetailList"
        Me.btnFileDetailList.Size = New System.Drawing.Size(537, 23)
        Me.btnFileDetailList.TabIndex = 2
        Me.btnFileDetailList.Text = "List File Details"
        Me.btnFileDetailList.UseVisualStyleBackColor = True
        '
        'btnLstFiles
        '
        Me.btnLstFiles.Location = New System.Drawing.Point(6, 19)
        Me.btnLstFiles.Name = "btnLstFiles"
        Me.btnLstFiles.Size = New System.Drawing.Size(538, 23)
        Me.btnLstFiles.TabIndex = 0
        Me.btnLstFiles.Text = "List Files"
        Me.btnLstFiles.UseVisualStyleBackColor = True
        '
        'groupBox2
        '
        Me.groupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.groupBox2.Controls.Add(Me.btnFileDetailList)
        Me.groupBox2.Controls.Add(Me.lstFiles)
        Me.groupBox2.Controls.Add(Me.btnLstFiles)
        Me.groupBox2.Location = New System.Drawing.Point(13, 292)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(550, 272)
        Me.groupBox2.TabIndex = 13
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "File Listing: "
        '
        'lstFiles
        '
        Me.lstFiles.FormattingEnabled = True
        Me.lstFiles.Location = New System.Drawing.Point(7, 45)
        Me.lstFiles.Name = "lstFiles"
        Me.lstFiles.Size = New System.Drawing.Size(537, 186)
        Me.lstFiles.TabIndex = 1
        '
        'txtServerIP
        '
        Me.txtServerIP.Location = New System.Drawing.Point(5, 28)
        Me.txtServerIP.Name = "txtServerIP"
        Me.txtServerIP.Size = New System.Drawing.Size(163, 20)
        Me.txtServerIP.TabIndex = 1
        '
        'btnFTPSave
        '
        Me.btnFTPSave.Enabled = False
        Me.btnFTPSave.Location = New System.Drawing.Point(5, 93)
        Me.btnFTPSave.Name = "btnFTPSave"
        Me.btnFTPSave.Size = New System.Drawing.Size(539, 23)
        Me.btnFTPSave.TabIndex = 0
        Me.btnFTPSave.Text = "Apply"
        Me.btnFTPSave.UseVisualStyleBackColor = True
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(2, 16)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(51, 13)
        Me.label1.TabIndex = 4
        Me.label1.Text = "Server IP"
        '
        'groupBox5
        '
        Me.groupBox5.BackColor = System.Drawing.SystemColors.Control
        Me.groupBox5.Controls.Add(Me.label5)
        Me.groupBox5.Controls.Add(Me.label4)
        Me.groupBox5.Controls.Add(Me.txtNewFilename)
        Me.groupBox5.Controls.Add(Me.txtCurrentFilename)
        Me.groupBox5.Controls.Add(Me.button1)
        Me.groupBox5.Location = New System.Drawing.Point(13, 97)
        Me.groupBox5.Name = "groupBox5"
        Me.groupBox5.Size = New System.Drawing.Size(550, 104)
        Me.groupBox5.TabIndex = 10
        Me.groupBox5.TabStop = False
        Me.groupBox5.Text = "File Rename"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(9, 48)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(79, 13)
        Me.label5.TabIndex = 4
        Me.label5.Text = "New File Name"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(9, 22)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(73, 13)
        Me.label4.TabIndex = 3
        Me.label4.Text = "Old File Name"
        '
        'txtNewFilename
        '
        Me.txtNewFilename.Location = New System.Drawing.Point(94, 45)
        Me.txtNewFilename.Name = "txtNewFilename"
        Me.txtNewFilename.Size = New System.Drawing.Size(450, 20)
        Me.txtNewFilename.TabIndex = 2
        '
        'txtCurrentFilename
        '
        Me.txtCurrentFilename.Location = New System.Drawing.Point(94, 19)
        Me.txtCurrentFilename.Name = "txtCurrentFilename"
        Me.txtCurrentFilename.Size = New System.Drawing.Size(450, 20)
        Me.txtCurrentFilename.TabIndex = 1
        '
        'button1
        '
        Me.button1.Location = New System.Drawing.Point(5, 71)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(539, 23)
        Me.button1.TabIndex = 0
        Me.button1.Text = "Rename"
        Me.button1.UseVisualStyleBackColor = True
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(381, 28)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(163, 20)
        Me.txtPassword.TabIndex = 7
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(378, 16)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(53, 13)
        Me.label3.TabIndex = 8
        Me.label3.Text = "Password"
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(193, 28)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(163, 20)
        Me.txtUsername.TabIndex = 5
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(190, 16)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(55, 13)
        Me.label2.TabIndex = 6
        Me.label2.Text = "Username"
        '
        'groupBox7
        '
        Me.groupBox7.BackColor = System.Drawing.SystemColors.Control
        Me.groupBox7.Controls.Add(Me.Label6)
        Me.groupBox7.Controls.Add(Me.txtDirectory)
        Me.groupBox7.Controls.Add(Me.txtPassword)
        Me.groupBox7.Controls.Add(Me.label3)
        Me.groupBox7.Controls.Add(Me.txtUsername)
        Me.groupBox7.Controls.Add(Me.label2)
        Me.groupBox7.Controls.Add(Me.txtServerIP)
        Me.groupBox7.Controls.Add(Me.btnFTPSave)
        Me.groupBox7.Controls.Add(Me.label1)
        Me.groupBox7.Location = New System.Drawing.Point(13, 570)
        Me.groupBox7.Name = "groupBox7"
        Me.groupBox7.Size = New System.Drawing.Size(550, 126)
        Me.groupBox7.TabIndex = 12
        Me.groupBox7.TabStop = False
        Me.groupBox7.Text = "FTP Configurations"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Directory"
        '
        'txtDirectory
        '
        Me.txtDirectory.Location = New System.Drawing.Point(7, 67)
        Me.txtDirectory.Name = "txtDirectory"
        Me.txtDirectory.Size = New System.Drawing.Size(537, 20)
        Me.txtDirectory.TabIndex = 9
        '
        'groupBox6
        '
        Me.groupBox6.BackColor = System.Drawing.SystemColors.Control
        Me.groupBox6.Controls.Add(Me.txtNewDir)
        Me.groupBox6.Controls.Add(Me.btnewDir)
        Me.groupBox6.Location = New System.Drawing.Point(12, 207)
        Me.groupBox6.Name = "groupBox6"
        Me.groupBox6.Size = New System.Drawing.Size(551, 79)
        Me.groupBox6.TabIndex = 11
        Me.groupBox6.TabStop = False
        Me.groupBox6.Text = "New Directory"
        '
        'txtNewDir
        '
        Me.txtNewDir.Location = New System.Drawing.Point(6, 19)
        Me.txtNewDir.Name = "txtNewDir"
        Me.txtNewDir.Size = New System.Drawing.Size(539, 20)
        Me.txtNewDir.TabIndex = 1
        '
        'btnewDir
        '
        Me.btnewDir.Location = New System.Drawing.Point(6, 45)
        Me.btnewDir.Name = "btnewDir"
        Me.btnewDir.Size = New System.Drawing.Size(539, 23)
        Me.btnewDir.TabIndex = 0
        Me.btnewDir.Text = "Make Directory"
        Me.btnewDir.UseVisualStyleBackColor = True
        '
        'grpBoxUpload
        '
        Me.grpBoxUpload.BackColor = System.Drawing.SystemColors.Control
        Me.grpBoxUpload.Controls.Add(Me.txtUpload)
        Me.grpBoxUpload.Controls.Add(Me.btnFileSize)
        Me.grpBoxUpload.Controls.Add(Me.btnUpload)
        Me.grpBoxUpload.Controls.Add(Me.btndelete)
        Me.grpBoxUpload.Controls.Add(Me.btnDownload)
        Me.grpBoxUpload.Location = New System.Drawing.Point(12, 12)
        Me.grpBoxUpload.Name = "grpBoxUpload"
        Me.grpBoxUpload.Size = New System.Drawing.Size(551, 79)
        Me.grpBoxUpload.TabIndex = 9
        Me.grpBoxUpload.TabStop = False
        Me.grpBoxUpload.Text = "Common FTP Operations"
        '
        'txtUpload
        '
        Me.txtUpload.Location = New System.Drawing.Point(6, 19)
        Me.txtUpload.Name = "txtUpload"
        Me.txtUpload.Size = New System.Drawing.Size(539, 20)
        Me.txtUpload.TabIndex = 1
        '
        'btnFileSize
        '
        Me.btnFileSize.Location = New System.Drawing.Point(417, 45)
        Me.btnFileSize.Name = "btnFileSize"
        Me.btnFileSize.Size = New System.Drawing.Size(128, 23)
        Me.btnFileSize.TabIndex = 0
        Me.btnFileSize.Text = "Size"
        Me.btnFileSize.UseVisualStyleBackColor = True
        '
        'btnUpload
        '
        Me.btnUpload.Location = New System.Drawing.Point(6, 45)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Size = New System.Drawing.Size(128, 23)
        Me.btnUpload.TabIndex = 0
        Me.btnUpload.Text = "Upload"
        Me.btnUpload.UseVisualStyleBackColor = True
        '
        'btndelete
        '
        Me.btndelete.Location = New System.Drawing.Point(280, 45)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(128, 23)
        Me.btndelete.TabIndex = 0
        Me.btndelete.Text = "Delete"
        Me.btndelete.UseVisualStyleBackColor = True
        '
        'btnDownload
        '
        Me.btnDownload.Location = New System.Drawing.Point(143, 45)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(128, 23)
        Me.btnDownload.TabIndex = 0
        Me.btnDownload.Text = "Download"
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'btnDownloadCeldasSo
        '
        Me.btnDownloadCeldasSo.Location = New System.Drawing.Point(713, 194)
        Me.btnDownloadCeldasSo.Name = "btnDownloadCeldasSo"
        Me.btnDownloadCeldasSo.Size = New System.Drawing.Size(161, 23)
        Me.btnDownloadCeldasSo.TabIndex = 14
        Me.btnDownloadCeldasSo.Text = "Download celdas.so"
        Me.btnDownloadCeldasSo.UseVisualStyleBackColor = True
        '
        'btnDeleteAppOnTarget
        '
        Me.btnDeleteAppOnTarget.Location = New System.Drawing.Point(713, 223)
        Me.btnDeleteAppOnTarget.Name = "btnDeleteAppOnTarget"
        Me.btnDeleteAppOnTarget.Size = New System.Drawing.Size(161, 23)
        Me.btnDeleteAppOnTarget.TabIndex = 15
        Me.btnDeleteAppOnTarget.Text = "Delete App On Target"
        Me.btnDeleteAppOnTarget.UseVisualStyleBackColor = True
        '
        'txtIPTarget1
        '
        Me.txtIPTarget1.Location = New System.Drawing.Point(880, 197)
        Me.txtIPTarget1.Name = "txtIPTarget1"
        Me.txtIPTarget1.Size = New System.Drawing.Size(161, 20)
        Me.txtIPTarget1.TabIndex = 5
        Me.txtIPTarget1.Text = "192.168.1.202"
        '
        'btnUploadCsvOnTarget
        '
        Me.btnUploadCsvOnTarget.Location = New System.Drawing.Point(713, 252)
        Me.btnUploadCsvOnTarget.Name = "btnUploadCsvOnTarget"
        Me.btnUploadCsvOnTarget.Size = New System.Drawing.Size(161, 23)
        Me.btnUploadCsvOnTarget.TabIndex = 16
        Me.btnUploadCsvOnTarget.Text = "Upload CSV On Target"
        Me.btnUploadCsvOnTarget.UseVisualStyleBackColor = True
        '
        'btnUploadCeldasSoOnTarget
        '
        Me.btnUploadCeldasSoOnTarget.Location = New System.Drawing.Point(713, 281)
        Me.btnUploadCeldasSoOnTarget.Name = "btnUploadCeldasSoOnTarget"
        Me.btnUploadCeldasSoOnTarget.Size = New System.Drawing.Size(161, 23)
        Me.btnUploadCeldasSoOnTarget.TabIndex = 17
        Me.btnUploadCeldasSoOnTarget.Text = "Upload Celdas.so On Target"
        Me.btnUploadCeldasSoOnTarget.UseVisualStyleBackColor = True
        '
        'txtIPTarget2
        '
        Me.txtIPTarget2.Location = New System.Drawing.Point(880, 281)
        Me.txtIPTarget2.Name = "txtIPTarget2"
        Me.txtIPTarget2.Size = New System.Drawing.Size(161, 20)
        Me.txtIPTarget2.TabIndex = 18
        Me.txtIPTarget2.Text = "192.168.1.160"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(753, 402)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(37, 13)
        Me.lblStatus.TabIndex = 19
        Me.lblStatus.Text = "Status"
        '
        'BtnDownloadAndUpload
        '
        Me.BtnDownloadAndUpload.Location = New System.Drawing.Point(713, 435)
        Me.BtnDownloadAndUpload.Name = "BtnDownloadAndUpload"
        Me.BtnDownloadAndUpload.Size = New System.Drawing.Size(281, 23)
        Me.BtnDownloadAndUpload.TabIndex = 20
        Me.BtnDownloadAndUpload.Text = "Download Upload celdas.so"
        Me.BtnDownloadAndUpload.UseVisualStyleBackColor = True
        '
        'FTP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1079, 734)
        Me.Controls.Add(Me.BtnDownloadAndUpload)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.txtIPTarget2)
        Me.Controls.Add(Me.btnUploadCeldasSoOnTarget)
        Me.Controls.Add(Me.btnUploadCsvOnTarget)
        Me.Controls.Add(Me.txtIPTarget1)
        Me.Controls.Add(Me.btnDeleteAppOnTarget)
        Me.Controls.Add(Me.btnDownloadCeldasSo)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.groupBox5)
        Me.Controls.Add(Me.groupBox7)
        Me.Controls.Add(Me.groupBox6)
        Me.Controls.Add(Me.grpBoxUpload)
        Me.Name = "FTP"
        Me.Text = "Form5"
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox5.ResumeLayout(False)
        Me.groupBox5.PerformLayout()
        Me.groupBox7.ResumeLayout(False)
        Me.groupBox7.PerformLayout()
        Me.groupBox6.ResumeLayout(False)
        Me.groupBox6.PerformLayout()
        Me.grpBoxUpload.ResumeLayout(False)
        Me.grpBoxUpload.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents btnFileDetailList As System.Windows.Forms.Button
    Private WithEvents btnLstFiles As System.Windows.Forms.Button
    Private WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents lstFiles As System.Windows.Forms.ListBox
    Private WithEvents txtServerIP As System.Windows.Forms.TextBox
    Private WithEvents btnFTPSave As System.Windows.Forms.Button
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents groupBox5 As System.Windows.Forms.GroupBox
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents txtNewFilename As System.Windows.Forms.TextBox
    Private WithEvents txtCurrentFilename As System.Windows.Forms.TextBox
    Private WithEvents button1 As System.Windows.Forms.Button
    Private WithEvents txtPassword As System.Windows.Forms.TextBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents txtUsername As System.Windows.Forms.TextBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents groupBox7 As System.Windows.Forms.GroupBox
    Private WithEvents groupBox6 As System.Windows.Forms.GroupBox
    Private WithEvents txtNewDir As System.Windows.Forms.TextBox
    Private WithEvents btnewDir As System.Windows.Forms.Button
    Private WithEvents grpBoxUpload As System.Windows.Forms.GroupBox
    Private WithEvents txtUpload As System.Windows.Forms.TextBox
    Private WithEvents btnFileSize As System.Windows.Forms.Button
    Private WithEvents btnUpload As System.Windows.Forms.Button
    Private WithEvents btndelete As System.Windows.Forms.Button
    Private WithEvents btnDownload As System.Windows.Forms.Button
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents txtDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnDownloadCeldasSo As System.Windows.Forms.Button
    Friend WithEvents btnDeleteAppOnTarget As System.Windows.Forms.Button
    Private WithEvents txtIPTarget1 As System.Windows.Forms.TextBox
    Friend WithEvents btnUploadCsvOnTarget As System.Windows.Forms.Button
    Friend WithEvents btnUploadCeldasSoOnTarget As System.Windows.Forms.Button
    Private WithEvents txtIPTarget2 As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents BtnDownloadAndUpload As System.Windows.Forms.Button
End Class

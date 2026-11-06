<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CeldasSoUpdateForm
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
        Me.BtnDownloadAndUpload = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtIPTarget2 = New System.Windows.Forms.TextBox()
        Me.txtIPTarget1 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'BtnDownloadAndUpload
        '
        Me.BtnDownloadAndUpload.Location = New System.Drawing.Point(28, 156)
        Me.BtnDownloadAndUpload.Name = "BtnDownloadAndUpload"
        Me.BtnDownloadAndUpload.Size = New System.Drawing.Size(281, 23)
        Me.BtnDownloadAndUpload.TabIndex = 24
        Me.BtnDownloadAndUpload.Text = "Download Upload celdas.so"
        Me.BtnDownloadAndUpload.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(80, 129)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(37, 13)
        Me.lblStatus.TabIndex = 23
        Me.lblStatus.Text = "Status"
        '
        'txtIPTarget2
        '
        Me.txtIPTarget2.Location = New System.Drawing.Point(92, 94)
        Me.txtIPTarget2.Name = "txtIPTarget2"
        Me.txtIPTarget2.Size = New System.Drawing.Size(161, 20)
        Me.txtIPTarget2.TabIndex = 22
        Me.txtIPTarget2.Text = "192.168.1.160"
        '
        'txtIPTarget1
        '
        Me.txtIPTarget1.Location = New System.Drawing.Point(92, 68)
        Me.txtIPTarget1.Name = "txtIPTarget1"
        Me.txtIPTarget1.Size = New System.Drawing.Size(161, 20)
        Me.txtIPTarget1.TabIndex = 21
        Me.txtIPTarget1.Text = "192.168.1.202"
        '
        'CeldasSoUpdateForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(347, 192)
        Me.Controls.Add(Me.BtnDownloadAndUpload)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.txtIPTarget2)
        Me.Controls.Add(Me.txtIPTarget1)
        Me.Name = "CeldasSoUpdateForm"
        Me.Text = "CeldasSoUpdateForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnDownloadAndUpload As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Private WithEvents txtIPTarget2 As System.Windows.Forms.TextBox
    Private WithEvents txtIPTarget1 As System.Windows.Forms.TextBox
End Class

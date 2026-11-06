<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgressBarForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProgressBarForm))
        Me.lblCpyright = New System.Windows.Forms.Label()
        Me.CELLogo = New DevExpress.XtraEditors.PictureEdit()
        Me.BackGround = New DevExpress.XtraEditors.GroupControl()
        Me.lblExportName = New System.Windows.Forms.Label()
        Me.lblProcessingRequest = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.CELLogo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BackGround, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BackGround.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCpyright
        '
        Me.lblCpyright.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCpyright.AutoSize = True
        Me.lblCpyright.BackColor = System.Drawing.Color.Transparent
        Me.lblCpyright.ForeColor = System.Drawing.Color.White
        Me.lblCpyright.Location = New System.Drawing.Point(6, 122)
        Me.lblCpyright.Name = "lblCpyright"
        Me.lblCpyright.Size = New System.Drawing.Size(215, 13)
        Me.lblCpyright.TabIndex = 111
        Me.lblCpyright.Text = "© 2018 CEL Aerospace Test Equipment Ltd"
        '
        'CELLogo
        '
        Me.CELLogo.EditValue = CType(resources.GetObject("CELLogo.EditValue"), Object)
        Me.CELLogo.Location = New System.Drawing.Point(6, 14)
        Me.CELLogo.Name = "CELLogo"
        Me.CELLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.CELLogo.Properties.Appearance.Options.UseBackColor = True
        Me.CELLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CELLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.CELLogo.Size = New System.Drawing.Size(119, 100)
        Me.CELLogo.TabIndex = 112
        '
        'BackGround
        '
        Me.BackGround.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BackGround.Appearance.BackColor2 = System.Drawing.Color.Black
        Me.BackGround.Appearance.BorderColor = System.Drawing.Color.Blue
        Me.BackGround.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.BackGround.Appearance.Options.UseBackColor = True
        Me.BackGround.Appearance.Options.UseBorderColor = True
        Me.BackGround.Controls.Add(Me.lblExportName)
        Me.BackGround.Controls.Add(Me.lblProcessingRequest)
        Me.BackGround.Controls.Add(Me.ProgressBar1)
        Me.BackGround.Controls.Add(Me.lblCpyright)
        Me.BackGround.Controls.Add(Me.CELLogo)
        Me.BackGround.Location = New System.Drawing.Point(0, 0)
        Me.BackGround.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.BackGround.LookAndFeel.UseDefaultLookAndFeel = False
        Me.BackGround.Name = "BackGround"
        Me.BackGround.Size = New System.Drawing.Size(563, 138)
        Me.BackGround.TabIndex = 115
        '
        'lblExportName
        '
        Me.lblExportName.AutoSize = True
        Me.lblExportName.BackColor = System.Drawing.Color.Transparent
        Me.lblExportName.ForeColor = System.Drawing.Color.White
        Me.lblExportName.Location = New System.Drawing.Point(167, 87)
        Me.lblExportName.Name = "lblExportName"
        Me.lblExportName.Size = New System.Drawing.Size(54, 13)
        Me.lblExportName.TabIndex = 116
        Me.lblExportName.Text = "Exporting:"
        '
        'lblProcessingRequest
        '
        Me.lblProcessingRequest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblProcessingRequest.AutoSize = True
        Me.lblProcessingRequest.BackColor = System.Drawing.Color.Transparent
        Me.lblProcessingRequest.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProcessingRequest.ForeColor = System.Drawing.Color.White
        Me.lblProcessingRequest.Location = New System.Drawing.Point(163, 24)
        Me.lblProcessingRequest.Name = "lblProcessingRequest"
        Me.lblProcessingRequest.Size = New System.Drawing.Size(225, 26)
        Me.lblProcessingRequest.TabIndex = 118
        Me.lblProcessingRequest.Text = "Processing Request..."
        '
        'ProgressBar1
        '
        Me.ProgressBar1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ProgressBar1.Location = New System.Drawing.Point(168, 54)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(382, 30)
        Me.ProgressBar1.TabIndex = 117
        '
        'ProgressBarForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 138)
        Me.ControlBox = False
        Me.Controls.Add(Me.BackGround)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ProgressBarForm"
        Me.Text = "ProgressBar"
        CType(Me.CELLogo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BackGround, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BackGround.ResumeLayout(False)
        Me.BackGround.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCpyright As System.Windows.Forms.Label
    Friend WithEvents CELLogo As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents BackGround As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProcessingRequest As System.Windows.Forms.Label
    Friend WithEvents lblExportName As System.Windows.Forms.Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CELResultListViewFormApprove
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CELResultListViewFormApprove))
        Me.lstViewResult = New System.Windows.Forms.ListView()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.BtnCheck = New System.Windows.Forms.Button()
        Me.BtnUncheck = New System.Windows.Forms.Button()
        Me.BtnApprove = New System.Windows.Forms.Button()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstViewResult
        '
        Me.lstViewResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstViewResult.BackColor = System.Drawing.Color.Black
        Me.lstViewResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstViewResult.ForeColor = System.Drawing.Color.White
        Me.lstViewResult.Location = New System.Drawing.Point(12, 47)
        Me.lstViewResult.Name = "lstViewResult"
        Me.lstViewResult.Size = New System.Drawing.Size(632, 437)
        Me.lstViewResult.TabIndex = 0
        Me.lstViewResult.UseCompatibleStateImageBehavior = False
        '
        'lblMessage
        '
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.White
        Me.lblMessage.Location = New System.Drawing.Point(12, 9)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(581, 35)
        Me.lblMessage.TabIndex = 2
        Me.lblMessage.Text = "lblMessage"
        '
        'btnOk
        '
        Me.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOk.BackColor = System.Drawing.Color.DimGray
        Me.btnOk.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Location = New System.Drawing.Point(499, 490)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(145, 58)
        Me.btnOk.TabIndex = 63
        Me.btnOk.Text = "Close"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'BtnCheck
        '
        Me.BtnCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnCheck.BackColor = System.Drawing.Color.DimGray
        Me.BtnCheck.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCheck.Location = New System.Drawing.Point(16, 490)
        Me.BtnCheck.Name = "BtnCheck"
        Me.BtnCheck.Size = New System.Drawing.Size(100, 26)
        Me.BtnCheck.TabIndex = 64
        Me.BtnCheck.Text = "Check All"
        Me.BtnCheck.UseVisualStyleBackColor = False
        '
        'BtnUncheck
        '
        Me.BtnUncheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnUncheck.BackColor = System.Drawing.Color.DimGray
        Me.BtnUncheck.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUncheck.Location = New System.Drawing.Point(16, 522)
        Me.BtnUncheck.Name = "BtnUncheck"
        Me.BtnUncheck.Size = New System.Drawing.Size(100, 26)
        Me.BtnUncheck.TabIndex = 65
        Me.BtnUncheck.Text = "Uncheck All"
        Me.BtnUncheck.UseVisualStyleBackColor = False
        '
        'BtnApprove
        '
        Me.BtnApprove.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnApprove.BackColor = System.Drawing.Color.DimGray
        Me.BtnApprove.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnApprove.Location = New System.Drawing.Point(167, 490)
        Me.BtnApprove.Name = "BtnApprove"
        Me.BtnApprove.Size = New System.Drawing.Size(117, 46)
        Me.BtnApprove.TabIndex = 66
        Me.BtnApprove.Text = "Approve Selected"
        Me.BtnApprove.UseVisualStyleBackColor = False
        '
        'btnReject
        '
        Me.btnReject.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnReject.BackColor = System.Drawing.Color.DimGray
        Me.btnReject.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReject.Location = New System.Drawing.Point(331, 490)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(117, 46)
        Me.btnReject.TabIndex = 68
        Me.btnReject.Text = "Reject   Selected"
        Me.btnReject.UseVisualStyleBackColor = False
        '
        'CELResultListViewFormApprove
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(656, 558)
        Me.Controls.Add(Me.btnReject)
        Me.Controls.Add(Me.BtnApprove)
        Me.Controls.Add(Me.BtnUncheck)
        Me.Controls.Add(Me.BtnCheck)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.lstViewResult)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CELResultListViewFormApprove"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CELResultListViewFormApprove"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstViewResult As System.Windows.Forms.ListView
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents BtnCheck As System.Windows.Forms.Button
    Friend WithEvents BtnUncheck As System.Windows.Forms.Button
    Friend WithEvents BtnApprove As System.Windows.Forms.Button
    Friend WithEvents btnReject As System.Windows.Forms.Button
End Class

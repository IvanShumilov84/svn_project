<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InputCommentForm
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
Me.OK_Button = New System.Windows.Forms.Button
Me.FormPromptText = New System.Windows.Forms.Label
Me.FormCommentTextInput = New System.Windows.Forms.TextBox
Me.SuspendLayout()
'
'OK_Button
'
Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
Me.OK_Button.Location = New System.Drawing.Point(349, 66)
Me.OK_Button.Name = "OK_Button"
Me.OK_Button.Size = New System.Drawing.Size(67, 23)
Me.OK_Button.TabIndex = 2
Me.OK_Button.Text = "OK"
'
'FormPromptText
'
Me.FormPromptText.AutoSize = True
Me.FormPromptText.Location = New System.Drawing.Point(182, 9)
Me.FormPromptText.Name = "FormPromptText"
Me.FormPromptText.Size = New System.Drawing.Size(54, 13)
Me.FormPromptText.TabIndex = 10
Me.FormPromptText.Text = "Comment:"
'
'FormCommentTextInput
'
Me.FormCommentTextInput.CausesValidation = False
Me.FormCommentTextInput.Location = New System.Drawing.Point(12, 35)
Me.FormCommentTextInput.Name = "FormCommentTextInput"
Me.FormCommentTextInput.Size = New System.Drawing.Size(407, 20)
Me.FormCommentTextInput.TabIndex = 1
Me.FormCommentTextInput.TabStop = False
'
'InputCommentForm
'
Me.AcceptButton = Me.OK_Button
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(428, 101)
Me.Controls.Add(Me.FormCommentTextInput)
Me.Controls.Add(Me.OK_Button)
Me.Controls.Add(Me.FormPromptText)
Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
Me.Location = New System.Drawing.Point(500, 500)
Me.MaximizeBox = False
Me.MinimizeBox = False
Me.Name = "InputCommentForm"
Me.ShowInTaskbar = False
Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
Me.Text = "Log Report"
Me.TopMost = True
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents FormPromptText As System.Windows.Forms.Label
    Friend WithEvents FormCommentTextInput As System.Windows.Forms.TextBox

End Class

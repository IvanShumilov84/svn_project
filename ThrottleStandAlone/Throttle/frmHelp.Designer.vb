<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHelp
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
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Throttle System")
        Dim TreeNode16 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Getting Ready")
        Dim TreeNode17 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("How to Rig")
        Dim TreeNode18 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("How to Store Limits")
        Dim TreeNode19 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Return To IDLE")
        Dim TreeNode20 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Troubleshooting")
        Dim TreeNode21 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Safety")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHelp))
        Me.tabHelp = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lblHelp = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.rtbHelp = New System.Windows.Forms.RichTextBox()
        Me.trvHelp = New System.Windows.Forms.TreeView()
        Me.tabHelp.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabHelp
        '
        Me.tabHelp.Controls.Add(Me.TabPage2)
        Me.tabHelp.Location = New System.Drawing.Point(12, 12)
        Me.tabHelp.Name = "tabHelp"
        Me.tabHelp.SelectedIndex = 0
        Me.tabHelp.Size = New System.Drawing.Size(1040, 622)
        Me.tabHelp.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.SplitContainer1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1032, 596)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Topics"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lblHelp
        '
        Me.lblHelp.AutoSize = True
        Me.lblHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHelp.Location = New System.Drawing.Point(400, 3)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(305, 25)
        Me.lblHelp.TabIndex = 1
        Me.lblHelp.Text = "CEL Throttle Stand Alone Help"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.trvHelp)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rtbHelp)
        Me.SplitContainer1.Size = New System.Drawing.Size(1026, 590)
        Me.SplitContainer1.SplitterDistance = 342
        Me.SplitContainer1.TabIndex = 0
        '
        'rtbHelp
        '
        Me.rtbHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbHelp.Location = New System.Drawing.Point(3, 3)
        Me.rtbHelp.Name = "rtbHelp"
        Me.rtbHelp.Size = New System.Drawing.Size(674, 584)
        Me.rtbHelp.TabIndex = 0
        Me.rtbHelp.Text = ""
        '
        'trvHelp
        '
        Me.trvHelp.Location = New System.Drawing.Point(3, 3)
        Me.trvHelp.Name = "trvHelp"
        TreeNode15.Name = "nodThrottleSystem"
        TreeNode15.Text = "Throttle System"
        TreeNode16.Name = "nodGettingReady"
        TreeNode16.Text = "Getting Ready"
        TreeNode17.Name = "nodHowToRig"
        TreeNode17.Text = "How to Rig"
        TreeNode18.Name = "nodHowToStoreLimits"
        TreeNode18.Text = "How to Store Limits"
        TreeNode19.Name = "nodReturnToIdle"
        TreeNode19.Text = "Return To IDLE"
        TreeNode20.Name = "nodTroubleshooting"
        TreeNode20.Text = "Troubleshooting"
        TreeNode21.Name = "nodSafety"
        TreeNode21.Text = "Safety"
        Me.trvHelp.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode15, TreeNode16, TreeNode17, TreeNode18, TreeNode19, TreeNode20, TreeNode21})
        Me.trvHelp.Size = New System.Drawing.Size(336, 584)
        Me.trvHelp.TabIndex = 0
        '
        'frmHelp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1064, 642)
        Me.Controls.Add(Me.lblHelp)
        Me.Controls.Add(Me.tabHelp)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHelp"
        Me.Text = "Help"
        Me.tabHelp.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tabHelp As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents lblHelp As Label
    Friend WithEvents trvHelp As TreeView
    Friend WithEvents rtbHelp As RichTextBox
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
End Class

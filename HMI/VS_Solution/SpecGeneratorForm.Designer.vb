<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpecGeneratorForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SpecGeneratorForm))
        Me.lblDestination = New System.Windows.Forms.Label
        Me.lblEngine = New System.Windows.Forms.Label
        Me.cboEngineList = New System.Windows.Forms.ComboBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.btnSelectSource = New System.Windows.Forms.Button
        Me.txtSource = New System.Windows.Forms.TextBox
        Me.btnStartProcess = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblDestination
        '
        Me.lblDestination.AccessibleDescription = Nothing
        Me.lblDestination.AccessibleName = Nothing
        resources.ApplyResources(Me.lblDestination, "lblDestination")
        Me.lblDestination.Font = Nothing
        Me.lblDestination.Name = "lblDestination"
        '
        'lblEngine
        '
        Me.lblEngine.AccessibleDescription = Nothing
        Me.lblEngine.AccessibleName = Nothing
        resources.ApplyResources(Me.lblEngine, "lblEngine")
        Me.lblEngine.Font = Nothing
        Me.lblEngine.Name = "lblEngine"
        '
        'cboEngineList
        '
        Me.cboEngineList.AccessibleDescription = Nothing
        Me.cboEngineList.AccessibleName = Nothing
        resources.ApplyResources(Me.cboEngineList, "cboEngineList")
        Me.cboEngineList.BackgroundImage = Nothing
        Me.cboEngineList.Font = Nothing
        Me.cboEngineList.FormattingEnabled = True
        Me.cboEngineList.Name = "cboEngineList"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        resources.ApplyResources(Me.OpenFileDialog1, "OpenFileDialog1")
        '
        'btnSelectSource
        '
        Me.btnSelectSource.AccessibleDescription = Nothing
        Me.btnSelectSource.AccessibleName = Nothing
        resources.ApplyResources(Me.btnSelectSource, "btnSelectSource")
        Me.btnSelectSource.BackgroundImage = Nothing
        Me.btnSelectSource.Font = Nothing
        Me.btnSelectSource.Name = "btnSelectSource"
        Me.btnSelectSource.UseVisualStyleBackColor = True
        '
        'txtSource
        '
        Me.txtSource.AccessibleDescription = Nothing
        Me.txtSource.AccessibleName = Nothing
        resources.ApplyResources(Me.txtSource, "txtSource")
        Me.txtSource.BackgroundImage = Nothing
        Me.txtSource.Font = Nothing
        Me.txtSource.Name = "txtSource"
        '
        'btnStartProcess
        '
        Me.btnStartProcess.AccessibleDescription = Nothing
        Me.btnStartProcess.AccessibleName = Nothing
        resources.ApplyResources(Me.btnStartProcess, "btnStartProcess")
        Me.btnStartProcess.BackgroundImage = Nothing
        Me.btnStartProcess.Font = Nothing
        Me.btnStartProcess.Name = "btnStartProcess"
        Me.btnStartProcess.UseVisualStyleBackColor = True
        '
        'SpecGeneratorForm
        '
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Nothing
        Me.Controls.Add(Me.btnStartProcess)
        Me.Controls.Add(Me.btnSelectSource)
        Me.Controls.Add(Me.txtSource)
        Me.Controls.Add(Me.lblEngine)
        Me.Controls.Add(Me.cboEngineList)
        Me.Controls.Add(Me.lblDestination)
        Me.Font = Nothing
        Me.Icon = Nothing
        Me.Name = "SpecGeneratorForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDestination As System.Windows.Forms.Label
    Friend WithEvents lblEngine As System.Windows.Forms.Label
    Friend WithEvents cboEngineList As System.Windows.Forms.ComboBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnSelectSource As System.Windows.Forms.Button
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents btnStartProcess As System.Windows.Forms.Button
End Class

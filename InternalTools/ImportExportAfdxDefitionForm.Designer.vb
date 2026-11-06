<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportExportAfdxDefitionForm
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
        Me.btnImportDefinition = New System.Windows.Forms.Button()
        Me.btnSelectFile = New System.Windows.Forms.Button()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnExportDefinition = New System.Windows.Forms.Button()
        Me.txtOutputFileName = New System.Windows.Forms.TextBox()
        Me.txtDefaultMTU = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCreateCSV = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnImportDefinition
        '
        Me.btnImportDefinition.BackColor = System.Drawing.Color.DimGray
        Me.btnImportDefinition.Location = New System.Drawing.Point(799, 45)
        Me.btnImportDefinition.Name = "btnImportDefinition"
        Me.btnImportDefinition.Size = New System.Drawing.Size(181, 45)
        Me.btnImportDefinition.TabIndex = 5
        Me.btnImportDefinition.Text = "Import AFDX PW980A"
        Me.btnImportDefinition.UseVisualStyleBackColor = False
        '
        'btnSelectFile
        '
        Me.btnSelectFile.BackColor = System.Drawing.Color.DimGray
        Me.btnSelectFile.Location = New System.Drawing.Point(54, 56)
        Me.btnSelectFile.Name = "btnSelectFile"
        Me.btnSelectFile.Size = New System.Drawing.Size(115, 22)
        Me.btnSelectFile.TabIndex = 7
        Me.btnSelectFile.Text = "Select File"
        Me.btnSelectFile.UseVisualStyleBackColor = False
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(191, 56)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(602, 20)
        Me.txtFileName.TabIndex = 8
        Me.txtFileName.Text = "C:\My Projects\CELDAS 200\InternalTools\ExternalFiles\PW980A\ICD_APU_T35_L4961SP0" & _
    "400926ICD1.csv"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnExportDefinition
        '
        Me.btnExportDefinition.BackColor = System.Drawing.Color.DimGray
        Me.btnExportDefinition.Location = New System.Drawing.Point(799, 96)
        Me.btnExportDefinition.Name = "btnExportDefinition"
        Me.btnExportDefinition.Size = New System.Drawing.Size(181, 45)
        Me.btnExportDefinition.TabIndex = 9
        Me.btnExportDefinition.Text = "Export AFDX PW980A to PowerDNA FORMAT"
        Me.btnExportDefinition.UseVisualStyleBackColor = False
        '
        'txtOutputFileName
        '
        Me.txtOutputFileName.Location = New System.Drawing.Point(191, 109)
        Me.txtOutputFileName.Name = "txtOutputFileName"
        Me.txtOutputFileName.Size = New System.Drawing.Size(602, 20)
        Me.txtOutputFileName.TabIndex = 10
        Me.txtOutputFileName.Text = "C:\Users\francoist\Desktop\Sample664_basictest_v4\afdx_dev1_5_sam1.txt"
        '
        'txtDefaultMTU
        '
        Me.txtDefaultMTU.Location = New System.Drawing.Point(411, 13)
        Me.txtDefaultMTU.Name = "txtDefaultMTU"
        Me.txtDefaultMTU.Size = New System.Drawing.Size(100, 20)
        Me.txtDefaultMTU.TabIndex = 12
        Me.txtDefaultMTU.Text = "1500"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(337, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Default MTU"
        '
        'btnCreateCSV
        '
        Me.btnCreateCSV.BackColor = System.Drawing.Color.DimGray
        Me.btnCreateCSV.Location = New System.Drawing.Point(478, 212)
        Me.btnCreateCSV.Name = "btnCreateCSV"
        Me.btnCreateCSV.Size = New System.Drawing.Size(181, 45)
        Me.btnCreateCSV.TabIndex = 14
        Me.btnCreateCSV.Text = "Create CSV"
        Me.btnCreateCSV.UseVisualStyleBackColor = False
        '
        'ImportExportAfdxDefitionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1028, 332)
        Me.Controls.Add(Me.btnCreateCSV)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDefaultMTU)
        Me.Controls.Add(Me.txtOutputFileName)
        Me.Controls.Add(Me.btnExportDefinition)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.btnSelectFile)
        Me.Controls.Add(Me.btnImportDefinition)
        Me.Name = "ImportExportAfdxDefitionForm"
        Me.Text = "ImportAfdxDefitionForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnImportDefinition As System.Windows.Forms.Button
    Friend WithEvents btnSelectFile As System.Windows.Forms.Button
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnExportDefinition As System.Windows.Forms.Button
    Friend WithEvents txtOutputFileName As System.Windows.Forms.TextBox
    Friend WithEvents txtDefaultMTU As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCreateCSV As System.Windows.Forms.Button
End Class

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
        Me.btnImportAfdxDefinition = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnFTP = New System.Windows.Forms.Button()
        Me.btnCreateFS = New System.Windows.Forms.Button()
        Me.btnUpdateCeldasSo = New System.Windows.Forms.Button()
        Me.btnDatabaseImportUtility = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnImportAfdxDefinition
        '
        Me.btnImportAfdxDefinition.BackColor = System.Drawing.Color.DimGray
        Me.btnImportAfdxDefinition.Location = New System.Drawing.Point(34, 152)
        Me.btnImportAfdxDefinition.Name = "btnImportAfdxDefinition"
        Me.btnImportAfdxDefinition.Size = New System.Drawing.Size(181, 45)
        Me.btnImportAfdxDefinition.TabIndex = 7
        Me.btnImportAfdxDefinition.Text = "Import AFDX Definitions"
        Me.btnImportAfdxDefinition.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DimGray
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(34, 212)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(181, 45)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Duplicate projescts"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnFTP
        '
        Me.btnFTP.BackColor = System.Drawing.Color.DimGray
        Me.btnFTP.Location = New System.Drawing.Point(34, 26)
        Me.btnFTP.Name = "btnFTP"
        Me.btnFTP.Size = New System.Drawing.Size(181, 45)
        Me.btnFTP.TabIndex = 9
        Me.btnFTP.Text = "FTP Utility"
        Me.btnFTP.UseVisualStyleBackColor = False
        '
        'btnCreateFS
        '
        Me.btnCreateFS.BackColor = System.Drawing.Color.DimGray
        Me.btnCreateFS.Enabled = False
        Me.btnCreateFS.Location = New System.Drawing.Point(34, 283)
        Me.btnCreateFS.Name = "btnCreateFS"
        Me.btnCreateFS.Size = New System.Drawing.Size(181, 45)
        Me.btnCreateFS.TabIndex = 10
        Me.btnCreateFS.Text = "Create FS"
        Me.btnCreateFS.UseVisualStyleBackColor = False
        '
        'btnUpdateCeldasSo
        '
        Me.btnUpdateCeldasSo.BackColor = System.Drawing.Color.DimGray
        Me.btnUpdateCeldasSo.Location = New System.Drawing.Point(34, 77)
        Me.btnUpdateCeldasSo.Name = "btnUpdateCeldasSo"
        Me.btnUpdateCeldasSo.Size = New System.Drawing.Size(181, 45)
        Me.btnUpdateCeldasSo.TabIndex = 11
        Me.btnUpdateCeldasSo.Text = "Download  celdas.so"
        Me.btnUpdateCeldasSo.UseVisualStyleBackColor = False
        '
        'btnDatabaseImportUtility
        '
        Me.btnDatabaseImportUtility.BackColor = System.Drawing.Color.DimGray
        Me.btnDatabaseImportUtility.Location = New System.Drawing.Point(34, 348)
        Me.btnDatabaseImportUtility.Name = "btnDatabaseImportUtility"
        Me.btnDatabaseImportUtility.Size = New System.Drawing.Size(181, 45)
        Me.btnDatabaseImportUtility.TabIndex = 12
        Me.btnDatabaseImportUtility.Text = "Database Import Utility"
        Me.btnDatabaseImportUtility.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(69, 408)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(266, 456)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnDatabaseImportUtility)
        Me.Controls.Add(Me.btnUpdateCeldasSo)
        Me.Controls.Add(Me.btnCreateFS)
        Me.Controls.Add(Me.btnFTP)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnImportAfdxDefinition)
        Me.Name = "MainForm"
        Me.Text = "MainForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnImportAfdxDefinition As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnFTP As System.Windows.Forms.Button
    Friend WithEvents btnCreateFS As System.Windows.Forms.Button
    Friend WithEvents btnUpdateCeldasSo As System.Windows.Forms.Button
    Friend WithEvents btnDatabaseImportUtility As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class

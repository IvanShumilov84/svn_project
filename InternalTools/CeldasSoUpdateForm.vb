Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Windows.Forms

Public Class CeldasSoUpdateForm

    Private Sub BtnDownloadAndUpload_Click(sender As Object, e As EventArgs) Handles BtnDownloadAndUpload.Click
        lblStatus.Text = "Downloading celdas.so from: " + txtIPTarget1.Text
        Application.DoEvents()
        Dim ftp As New FtpClient(txtIPTarget1.Text, "Virgo2000", "v2000", "/usr/cel/odkv4.21/CFunctionKit/lib")
        Dim ftp2 As New FtpClient(txtIPTarget2.Text, "Virgo2000", "v2000", "/usr/v2000/bin")
        If File.Exists("c:\temp\celdas.so") Then
            File.Delete("c:\temp\celdas.so")
        End If
        ftp.Download("C:\temp", "celdas.so")
        lblStatus.Text = "Download completed...  Now Uploading to: " + txtIPTarget2.Text
        Application.DoEvents()
        ftp2.Delete("celdas.so")
        ftp2.Upload("C:\temp\celdas.so")
        lblStatus.Text = "Upload completed"

        Me.Dispose()
    End Sub
End Class
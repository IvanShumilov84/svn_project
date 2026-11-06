Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Windows.Forms

Public Class FTP

    Dim ftp As New FtpClient("192.168.1.202", "Virgo2000", "v2000", "/")

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles Me.Load



        txtServerIP.Text = ftp.ServerIP
        txtUsername.Text = ftp.UserId
        txtPassword.Text = ftp.Password
        txtDirectory.Text = ftp.Directory.Replace("/%2f", "")
        Me.Text += ftp.ServerIP

        btnFTPSave.Enabled = False


    End Sub



    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim opFilDlg As New OpenFileDialog()
        If opFilDlg.ShowDialog() = DialogResult.OK Then
            ftp.Upload(opFilDlg.FileName)
        End If
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim fldDlg As New FolderBrowserDialog()
        If txtUpload.Text.Trim().Length > 0 Then
            If fldDlg.ShowDialog() = DialogResult.OK Then
                ftp.Download(fldDlg.SelectedPath, txtUpload.Text.Trim())
            End If
        Else
            MessageBox.Show("Please enter the File name to download")
        End If
    End Sub

    Private Sub btnLstFiles_Click(sender As Object, e As EventArgs) Handles btnLstFiles.Click
        Dim filenames As String() = ftp.GetFileList()
        lstFiles.Items.Clear()
        For Each filename As String In filenames
            lstFiles.Items.Add(filename)
        Next
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Dim fldDlg As New OpenFileDialog()
        If txtUpload.Text.Trim().Length > 0 Then
            ftp.Delete(txtUpload.Text.Trim())
        Else
            MessageBox.Show("Please enter the File name to delete")
        End If
    End Sub


    Private Sub btnFileSize_Click(sender As Object, e As EventArgs) Handles btnFileSize.Click
        Dim size As Long = ftp.GetFileSize(txtUpload.Text.Trim())
        MessageBox.Show(size.ToString() & " bytes")
    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        Rename(txtCurrentFilename.Text.Trim(), txtNewFilename.Text.Trim())
    End Sub

    Private Sub btnewDir_Click(sender As Object, e As EventArgs) Handles btnewDir.Click
        ftp.MakeDir(txtNewDir.Text.Trim())
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        btnFTPSave.Enabled = True
    End Sub

    Private Sub txtServerIP_TextChanged(sender As Object, e As EventArgs) Handles txtServerIP.TextChanged
        btnFTPSave.Enabled = True
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged
        btnFTPSave.Enabled = True
    End Sub
    Private Sub txtDirectory_TextChanged(sender As Object, e As EventArgs) Handles txtDirectory.TextChanged
        btnFTPSave.Enabled = True
    End Sub
    Private Sub btnFTPSave_Click(sender As Object, e As EventArgs) Handles btnFTPSave.Click
        ftp.ServerIP = txtServerIP.Text.Trim()
        ftp.UserId = txtUsername.Text.Trim()
        ftp.Password = txtPassword.Text.Trim()
        ftp.Directory = txtDirectory.Text.Trim()

        btnFTPSave.Enabled = False
    End Sub

    Private Sub btnFileDetailList_Click(sender As Object, e As EventArgs) Handles btnFileDetailList.Click
        Dim filenames As String() = ftp.GetFilesDetailList()
        lstFiles.Items.Clear()
        For Each filename As String In filenames
            lstFiles.Items.Add(filename)
        Next
    End Sub



    Private Sub btnDownloadCeldasSo_Click(sender As Object, e As EventArgs) Handles btnDownloadCeldasSo.Click
        Dim ftp As New FtpClient(txtIPTarget1.Text, "Virgo2000", "v2000", "/usr/cel/odkv4.21/CFunctionKit/lib")
        If File.Exists("c:\temp\celdas.so") Then
            File.Delete("c:\temp\celdas.so")
        End If
        ftp.Download("C:\temp", "celdas.so")
        MessageBox.Show("Completed")
    End Sub
    Private Sub btnUploadCeldasSoOnTarget_Click(sender As Object, e As EventArgs) Handles btnUploadCeldasSoOnTarget.Click
        Dim ftp As New FtpClient(txtIPTarget2.Text, "Virgo2000", "v2000", "/usr/v2000/bin")
        ftp.Delete("celdas.so")
        ftp.Upload("C:\temp\celdas.so")
        MessageBox.Show("Completed")
    End Sub

    Private Sub btnDeleteAppOnTarget_Click(sender As Object, e As EventArgs) Handles btnDeleteAppOnTarget.Click
        Dim ftp As New FtpClient(txtIPTarget2.Text, "Virgo2000", "v2000", "/usr/v2000/app")
        'ftp.Delete("C:\temp", "celdas.so")

        Dim filenames As String() = ftp.GetFileList()
        lstFiles.Items.Clear()
        For Each filename As String In filenames
            lstFiles.Items.Add(filename)
            If filename.StartsWith("I") Then
                ftp.Delete(filename)
            End If
        Next

        MessageBox.Show("Completed")



    End Sub

    Private Sub btnUploadCsvOnTarget_Click(sender As Object, e As EventArgs) Handles btnUploadCsvOnTarget.Click
        Dim ftp As New FtpClient(txtIPTarget1.Text, "Virgo2000", "v2000", "/usr/v2000/app")


        ' make a reference to a directory
        Dim di As New IO.DirectoryInfo("C:\My Projects\CELDAS 200\Workbench")
        Dim files As IO.FileInfo() = di.GetFiles("*.csv")
        Dim file As IO.FileInfo


        'list the names of all files in the specified directory
        For Each file In files
            ftp.Upload(file.FullName)
        Next

    End Sub

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

    End Sub

    Private Sub txtIPTarget1_TextChanged(sender As Object, e As EventArgs) Handles txtIPTarget1.TextChanged

    End Sub

    Private Sub lblStatus_Click(sender As Object, e As EventArgs) Handles lblStatus.Click

    End Sub

    Private Sub txtIPTarget2_TextChanged(sender As Object, e As EventArgs) Handles txtIPTarget2.TextChanged

    End Sub
End Class

Public Class FtpClient

    Dim _ftpServerIP As String
    Dim _ftpUserId As String
    Dim _ftpPassword As String
    Dim _ftpDirectory As String

    Public Property ServerIP As String
        Get
            Return _ftpServerIP
        End Get
        Set(value As String)
            _ftpServerIP = value
        End Set
    End Property
    Public Property UserId
        Get
            Return _ftpUserId
        End Get
        Set(value)
            _ftpUserId = value
        End Set
    End Property
    Public Property Password
        Get
            Return _ftpPassword
        End Get
        Set(value)
            _ftpPassword = value
        End Set
    End Property
    Public Property Directory As String
        Get
            Return _ftpDirectory
        End Get
        Set(value As String)
            _ftpDirectory = "/%2f" + IIf(value.EndsWith("/"), value, value + "/")
        End Set
    End Property

    Public Sub New(ByVal ftpServerIP As String, ByVal ftpUserId As String, ByVal ftpPassword As String, ByVal ftpDirectory As String)
        ServerIP = ftpServerIP
        UserId = ftpUserId
        Password = ftpPassword
        Directory = ftpDirectory
    End Sub
    ''' <summary>
    ''' Method to upload the specified file to the specified FTP Server
    ''' </summary>
    ''' <param name="filename">file full name to be uploaded</param>
    Public Sub Upload(filename As String)


        Dim fileInf As New FileInfo(filename)
        Dim uri As String = ("ftp://" & _ftpServerIP & "/") + fileInf.Name
        Dim reqFTP As FtpWebRequest

        ' Create FtpWebRequest object from the Uri provided
        reqFTP = DirectCast(FtpWebRequest.Create(New Uri(("ftp://" & _ftpServerIP & "/" & _ftpDirectory) + fileInf.Name)), FtpWebRequest)

        ' Provide the WebPermission Credintials
        reqFTP.Credentials = New NetworkCredential(_ftpUserId, _ftpPassword)

        ' By default KeepAlive is true, where the control connection is not closed
        ' after a command is executed.
        reqFTP.KeepAlive = False

        ' Specify the command to be executed.
        reqFTP.Method = WebRequestMethods.Ftp.UploadFile

        ' Specify the data transfer type.
        reqFTP.UseBinary = True

        ' Notify the server about the size of the uploaded file
        reqFTP.ContentLength = fileInf.Length

        ' The buffer size is set to 2kb
        Dim buffLength As Integer = 2048
        Dim buff As Byte() = New Byte(buffLength - 1) {}
        Dim contentLen As Integer

        ' Opens a file stream (System.IO.FileStream) to read the file to be uploaded
        Dim fs As FileStream = fileInf.OpenRead()

        Try
            ' Stream to which the file to be upload is written
            Dim strm As Stream = reqFTP.GetRequestStream()

            ' Read from the file stream 2kb at a time
            contentLen = fs.Read(buff, 0, buffLength)

            ' Till Stream content ends
            While contentLen <> 0
                ' Write Content from the file stream to the FTP Upload Stream
                strm.Write(buff, 0, contentLen)
                contentLen = fs.Read(buff, 0, buffLength)
            End While

            ' Close the file stream and the Request Stream
            strm.Close()
            fs.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Upload Error")
        End Try

    End Sub
    Public Sub Delete(fileName As String)
        Try

            Dim uri As String = "ftp://" & _ftpServerIP & "/" & _ftpDirectory & fileName
            Dim reqFTP As FtpWebRequest
            reqFTP = DirectCast(FtpWebRequest.Create(New Uri("ftp://" & _ftpServerIP & "/" & _ftpDirectory & fileName)), FtpWebRequest)

            reqFTP.Credentials = New NetworkCredential(_ftpUserId, _ftpPassword)
            reqFTP.KeepAlive = False
            reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

            Dim result As String = [String].Empty
            Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
            Dim size As Long = response.ContentLength
            Dim datastream As Stream = response.GetResponseStream()
            Dim sr As New StreamReader(datastream)
            result = sr.ReadToEnd()
            sr.Close()
            datastream.Close()
            response.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "FTP 2.0 Delete")
        End Try
    End Sub
    Public Function GetFilesDetailList() As String()
        Dim downloadFiles As String()
        Try
            Dim result As New StringBuilder()
            Dim ftp As FtpWebRequest
            ftp = DirectCast(FtpWebRequest.Create(New Uri("ftp://" & _ftpServerIP & "/" & _ftpDirectory)), FtpWebRequest)
            ftp.Credentials = New NetworkCredential(_ftpUserId, _ftpPassword)
            ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails
            Dim response As WebResponse = ftp.GetResponse()
            Dim reader As New StreamReader(response.GetResponseStream())
            Dim line As String = reader.ReadLine()
            While line IsNot Nothing
                result.Append(line)
                result.Append(vbLf)
                line = reader.ReadLine()
            End While

            result.Remove(result.ToString().LastIndexOf(vbLf), 1)
            reader.Close()
            response.Close()
            'MessageBox.Show(result.ToString().Split('\n'));
            Return result.ToString().Split(ControlChars.Lf)
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
            downloadFiles = Nothing
            Return downloadFiles
        End Try
    End Function
    Public Function GetFileList() As String()
        Dim downloadFiles As String()
        Dim result As New StringBuilder()
        Dim reqFTP As FtpWebRequest
        Try
            reqFTP = DirectCast(FtpWebRequest.Create(New Uri("ftp://" & _ftpServerIP & _ftpDirectory)), FtpWebRequest)
            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(_ftpUserId, _ftpPassword)
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory
            Dim response As WebResponse = reqFTP.GetResponse()
            Dim reader As New StreamReader(response.GetResponseStream())
            'MessageBox.Show(reader.ReadToEnd());
            Dim line As String = reader.ReadLine()
            While line IsNot Nothing
                result.Append(line)
                result.Append(vbLf)
                line = reader.ReadLine()
            End While
            result.Remove(result.ToString().LastIndexOf(ControlChars.Lf), 1)
            reader.Close()
            response.Close()
            'MessageBox.Show(response.StatusDescription);
            Return result.ToString().Split(ControlChars.Lf)
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
            downloadFiles = Nothing
            Return downloadFiles
        End Try
    End Function
    Public Sub Download(filePath As String, fileName As String)
        Dim reqFTP As FtpWebRequest
        Try
            'filePath = <<The full path where the file is to be created.>>, 
            'fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
            Dim outputStream As New FileStream(filePath & "\" & fileName, FileMode.Create)

            reqFTP = DirectCast(FtpWebRequest.Create(New Uri("ftp://" & _ftpServerIP & "/" & _ftpDirectory & fileName)), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile
            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(_ftpUserId, _ftpPassword)
            Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
            Dim ftpStream As Stream = response.GetResponseStream()
            Dim cl As Long = response.ContentLength
            Dim bufferSize As Integer = 2048
            Dim readCount As Integer
            Dim buffer As Byte() = New Byte(bufferSize - 1) {}

            readCount = ftpStream.Read(buffer, 0, bufferSize)
            While readCount > 0
                outputStream.Write(buffer, 0, readCount)
                readCount = ftpStream.Read(buffer, 0, bufferSize)
            End While

            ftpStream.Close()
            outputStream.Close()
            response.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function GetFileSize(filename As String) As Long
        Dim reqFTP As FtpWebRequest
        Dim fileSize As Long = 0
        Try
            reqFTP = DirectCast(FtpWebRequest.Create(New Uri("ftp://" & _ftpServerIP & "/" & _ftpDirectory & filename)), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.GetFileSize

            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(_ftpUserId, _ftpPassword)
            Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
            Dim ftpStream As Stream = response.GetResponseStream()
            fileSize = response.ContentLength

            ftpStream.Close()
            response.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return fileSize
    End Function
    Public Sub Rename(currentFilename As String, newFilename As String)
        Dim reqFTP As FtpWebRequest
        Try
            reqFTP = DirectCast(FtpWebRequest.Create(New Uri("ftp://" & _ftpServerIP & "/" & _ftpDirectory & currentFilename)), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.Rename
            reqFTP.RenameTo = newFilename
            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(_ftpUserId, _ftpPassword)
            Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
            Dim ftpStream As Stream = response.GetResponseStream()

            ftpStream.Close()
            response.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub MakeDir(dirName As String)
        Dim reqFTP As FtpWebRequest
        Try
            ' dirName = name of the directory to create.
            reqFTP = DirectCast(FtpWebRequest.Create(New Uri("ftp://" & _ftpServerIP & "/" & _ftpDirectory & dirName)), FtpWebRequest)
            reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory
            reqFTP.UseBinary = True
            reqFTP.Credentials = New NetworkCredential(_ftpUserId, _ftpPassword)
            Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
            Dim ftpStream As Stream = response.GetResponseStream()

            ftpStream.Close()
            response.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
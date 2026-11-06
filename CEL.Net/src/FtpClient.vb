'
' Created by SharpDevelop.
' User: logiciel
' Date: 2/8/2006
' Time: 11:40 AM
' 
'*
'* FTP Client library in C#
'* Author: Jaimon Mathew
'* mailto:jaimonmathew@rediffmail.com
'* http://www.csharphelp.com/archives/archive9.html
'* 
'* Addapted for use by Dan Glass 07/03/03
'* 
'* Vb Rewrite by Danny Lepage
'*
Imports System
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Net.Sockets
Imports System.Diagnostics
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Messaging
Imports Microsoft.VisualBasic
Namespace CEL.Net

    Public Class FtpClient

        Public Class FtpException
            Inherits Exception

            Public Sub New(ByVal message As String)
                MyBase.New(message)
            End Sub

            Public Sub New(ByVal message As String, ByVal innerException As Exception)
                MyBase.New(message, innerException)
            End Sub
        End Class
        Private Shared BUFFER_SIZE As Integer = 512
        Private Shared ASCII As Encoding = Encoding.ASCII
        Private _verboseDebugging As Boolean = False
        Private _server As String = "localhost"
        Private _remotePath As String = "."
        Private _username As String = "anonymous"
        Private _password As String = "anonymous@anonymous.net"
        Private _message As String = Nothing
        Private _result As String = Nothing
        Private _port As Integer = 21
        Private _bytes As Integer = 0
        Private _resultCode As Integer = 0
        Private _loggedin As Boolean = False
        Private _binMode As Boolean = False
        Private _buffer As Byte() = New Byte(BUFFER_SIZE) {}
        Private _clientSocket As Socket = Nothing
        Private _timeoutSeconds As Integer = 10

        Public Sub New()
        End Sub

        Public Sub New(ByVal server As String, ByVal username As String, ByVal password As String)
            Me._server = server
            Me._username = username
            Me._password = password
        End Sub

        Public Sub New(ByVal server As String, ByVal username As String, ByVal password As String, ByVal timeoutSeconds As Integer, ByVal port As Integer)
            Me._server = server
            Me._username = username
            Me._password = password
            Me._timeoutSeconds = timeoutSeconds
            Me._port = port
        End Sub

        Public Property VerboseDebugging() As Boolean
            Get
                Return Me._verboseDebugging
            End Get
            Set(value As Boolean)
                Me._verboseDebugging = value
            End Set
        End Property

        Public Property Port() As Integer
            Get
                Return Me._port
            End Get
            Set(value As Integer)
                Me._port = value
            End Set
        End Property

        Public Property Timeout() As Integer
            Get
                Return Me._timeoutSeconds
            End Get
            Set(value As Integer)
                Me._timeoutSeconds = value
            End Set
        End Property

        Public Property Server() As String
            Get
                Return Me._server
            End Get
            Set(value As String)
                Me._server = value
            End Set
        End Property

        Public Property RemotePort() As Integer
            Get
                Return Me._port
            End Get
            Set(value As Integer)
                Me._port = value
            End Set
        End Property

        Public Property RemotePath() As String
            Get
                Return Me._remotePath
            End Get
            Set(value As String)
                Me._remotePath = value
            End Set
        End Property

        Public Property Username() As String
            Get
                Return Me._username
            End Get
            Set(value As String)
                Me._username = value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return Me._password
            End Get
            Set(value As String)
                Me._password = value
            End Set
        End Property

        Public Property BinaryMode() As Boolean
            Get
                Return Me._binMode
            End Get
            Set(value As Boolean)
                If Me._binMode = value Then
                    Return
                End If
                If value Then
                    sendCommand("TYPE I")
                Else
                    sendCommand("TYPE A")
                End If
                If Not (Me._resultCode = 200) Then
                    Debug.Assert(False, "Exception from: CEL.NET.FTPCLient.BinaryMode:", Me._result.Substring(4))
                End If
            End Set
        End Property

        '''
        ''' Login to the remote server.
        '''
        Public Sub Login()
            Try
                Try
                    Me._clientSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    If Me._loggedin Then
                        Me.Close()
                    End If
                    If Me.VerboseDebugging Then
                        Console.WriteLine("Opening connection to " + Me.Server, "FtpClient")
                        Console.WriteLine("")
                    End If

                    Dim ips As IPAddress()
                    ips = Dns.GetHostAddresses(Me.Server)
                    Dim endpoint As New IPEndPoint(ips(0), Me.RemotePort)

                    Dim reattempt As Boolean = True
                    Do While reattempt
                        'This behavior will allow a user to halt the opening process and connect the server and then continue the initialisation process.
                        'This behavior is ONLY triggered on a particular error code (10060). This code is defined as no reply from a request suggesting that there is no server
                        'at the other end to reply to it. Other socket exceptions are possible but not handled specifically.
                        Try
                            Me._clientSocket.Connect(endpoint)
                            reattempt = False
                        Catch ex As System.Net.Sockets.SocketException
                            If ex.ErrorCode = 10060 Then
                                Dim selection As Microsoft.VisualBasic.MsgBoxResult = MsgBox("Impossible to connect to the  server [Possibly QNX]. Make sure it is available before continuing." & vbCr & _
                                                                                             "Press Ok to attempt connecting to the server again or Cancel to skip reconnection.", MsgBoxStyle.OkCancel)
                                If selection = vbOK Then
                                    reattempt = True

                                ElseIf selection = vbCancel Then
                                    reattempt = False
                                    Close()
                                End If
                            End If
                        End Try
                    Loop

                    'Broader catch incase of other socket exception.
                Catch ex As Exception
                    Debug.Assert(False, "Exception from: CEL.NET.FTPCLient.Login:Couldn't connect to remote server:", ex.Message)
                End Try


                Me.readResponse()
                If Me._resultCode <> 220 Then
                    Me.Close()
                    Debug.Assert(False, "Exception from: CEL.NET.FTPCLient.Login", Me._result.Substring(4))
                End If
                If Me.VerboseDebugging Then
                    Console.WriteLine("User " & Me.Username)
                    Console.WriteLine("")
                End If
                Me.sendCommand("USER " + Username)
                If Not (Me._resultCode = 331 OrElse Me._resultCode = 230) Then
                    Me.cleanup()
                    Debug.Assert(False, "Exception from: CEL.NET.FTPCLient.Login", Me._result.Substring(4))
                End If

                If Me._resultCode <> 230 Then
                    If Me.VerboseDebugging Then
                        Console.WriteLine("PASS xxx")
                        Console.WriteLine("")
                    End If
                    Me.sendCommand("PASS " + Password)
                    If Not (Me._resultCode = 230 OrElse Me._resultCode = 202) Then
                        Me.cleanup()
                        Debug.Assert(False, "Exception from: CEL.NET.FTPCLient.Login", Me._result.Substring(4))
                    End If
                End If
                Me._loggedin = True
                Debug.WriteLine("Connected to " + Me.Server, "FtpClient")
                If Me._loggedin = True Then Console.WriteLine("Connected to " + Me.Server, "FtpClient")
                Console.WriteLine("")
                Me.ChangeCreateDir(Me.RemotePath)
            Catch ex As Exception
                Debug.Assert(False, "CEL.NET.FTPClient.Login: ", ex.Message)
            End Try
        End Sub

        '''
        ''' Close the FTP connection.
        '''
        Public Sub Close()
            Try
                Debug.WriteLine("Closing connection to " + Me.Server, "FtpClient")
                If Not (Me._clientSocket Is Nothing) Then
                    Me.sendCommand("QUIT")
                End If
                Me.cleanup()
                Console.WriteLine("Closing...")
            Catch ex As Exception
                Debug.Assert(False, "CEL.NET.FTPClient.Close: ", ex.Message)
            End Try
        End Sub

        Public Function GetFileList() As String()
            Return Me.GetFileList("*.*")
        End Function

        '''
        ''' Return a string array containing the remote directory's file list.
        '''
        Public Function GetFileList(ByVal mask As String) As String()
            If Not Me._loggedin Then
                Me.Login()
            End If
            Dim cSocket As Socket = createDataSocket()
            Me.sendCommand("NLST " + mask)
            If Not (Me._resultCode = 150 OrElse Me._resultCode = 125) Then
                Dim tempString() As String = {Me._result.Substring(4)}
                Return tempString
            End If
            Me._message = ""
            Dim timeout As DateTime = DateTime.Now.AddSeconds(Me._timeoutSeconds)
            While timeout > DateTime.Now
                Dim bytes As Integer = cSocket.Receive(Me._buffer, Me._buffer.Length, 0)
                Me._message += ASCII.GetString(Me._buffer, 0, bytes)
                If bytes < Me._buffer.Length Then
                    Exit While
                End If
            End While
            Dim msg As String() = Me._message.Replace("" & Microsoft.VisualBasic.Chr(13) & "", "").Split(Microsoft.VisualBasic.Chr(10))
            cSocket.Close()
            If Not (Me._message.IndexOf("No such file or directory") = -1) Then
                msg = New String() {}
            End If
            Me.readResponse()
            If Not (Me._resultCode = 226) Then
                msg = New String() {}
            End If
            Return msg
        End Function

        ''' <summary>
        ''' Return the size of a file on the ftp server.
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <returns>seize as long</returns>
        ''' <remarks>Need configured connection (settings) to work</remarks>
        Public Function GetFileSize(ByVal fileName As String) As Long
            If Not Me._loggedin Then
                Me.Login()
            End If
            Me.sendCommand("SIZE " + fileName)
            Dim size As Long = 0
            If Me._resultCode = 213 Then
                size = Long.Parse(Me._result.Substring(4))
            Else
                Debug.Assert(False, "Exception from: CEL.NET.FTPCLient.GetFileSize:", Me._result.Substring(4))
            End If
            If Me.VerboseDebugging Then
                Console.WriteLine("The Size of the file {0} is:{1}", fileName, size)
                Console.WriteLine("")
            End If
            Return size
        End Function

        '''
        ''' Download a file to the Assembly's local directory,
        ''' keeping the same file name.
        '''
        Public Sub Download(ByVal remFileName As String)
            Me.Download(remFileName, "", False)
        End Sub

        '''
        ''' Download a remote file to the Assembly's local directory,
        ''' keeping the same file name, and set the resume flag.
        '''
        '''
        Public Sub Download(ByVal remFileName As String, ByVal [resume] As Boolean)
            Me.Download(remFileName, "", [resume])
        End Sub

        '''
        ''' Download a remote file to a local file name which can include
        ''' a path. The local file name will be created or overwritten,
        ''' but the path must exist.
        '''
        '''
        Public Sub Download(ByVal remFileName As String, ByVal locFileName As String)
            Me.Download(remFileName, locFileName, False)
        End Sub
        '''
        ''' Download a remote file to a local file name which can include
        ''' a path, and set the resume flag. The local file name will be
        ''' created or overwritten, but the path must exist.
        '''
        '''
        '''
        Public Sub Download(ByVal remFileName As String, ByVal locFileName As String, ByVal [resume] As Boolean)
            If Not Me._loggedin Then
                Me.Login()
            End If
            Me.BinaryMode = True
            If Me.VerboseDebugging Then
                Console.WriteLine("Downloading file " + remFileName + " from " + Server + "/" + RemotePath, "FtpClient")
            End If

            If locFileName.Equals("") Then
                locFileName = remFileName
            End If
            Dim output As FileStream = Nothing
            If Not File.Exists(locFileName) Then
                output = File.Create(locFileName)
            Else
                output = New FileStream(locFileName, FileMode.Open)
            End If
            Dim cSocket As Socket = Me.createDataSocket
            Dim offset As Long = 0
            If [resume] Then
                offset = output.Length
                If offset > 0 Then
                    Me.sendCommand("REST " + offset.ToString())
                    If Me._resultCode <> 350 Then
                        offset = 0
                        If Me.VerboseDebugging Then Console.WriteLine("Resuming not supported:" + Me._result.Substring(4), "FtpClient")
                    End If
                End If
                If offset > 0 Then
                    If Me.VerboseDebugging Then
                        Console.WriteLine("seeking to " & offset.ToString())
                    End If
                    Dim npos As Long = output.Seek(offset, SeekOrigin.Begin)
                    Console.WriteLine("new pos=" & npos)
                End If
            End If
            Me.sendCommand("RETR " + remFileName)
            If Not (Me._resultCode = 150 OrElse Me._resultCode = 125) Then
                Debug.Assert(False, "Exception from: CEL.NET.FTPCLient.Download:", Me._result.Substring(4))
            End If
            Dim timeout As DateTime = DateTime.Now.AddSeconds(Me._timeoutSeconds)

            While timeout > DateTime.Now
                Me._bytes = cSocket.Receive(Me._buffer, Me._buffer.Length, 0)
                output.Write(Me._buffer, 0, Me._bytes)
                If Me._bytes <= 0 Then
                    Exit While
                End If
            End While
            output.Close()
            If cSocket.Connected Then
                cSocket.Close()
            End If
            Console.WriteLine("")
            Me.readResponse()
            If Not (Me._resultCode = 226 OrElse Me._resultCode = 250) Then
                Debug.Assert(False, "Error from: CEL.NET.FTPCLient.Download:", Me._result.Substring(4))
            End If
        End Sub
        ''' <summary>
        ''' Return the last time the file was modified on the ftp server.
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <returns>modDate as string or if file doesn't exit: Nothing</returns>
        ''' <remarks>Need configured connection (settings) to work</remarks>
        Public Function GetLastMod(ByVal fileName As String) As String
            Dim modDate As String = ""
            Try
                If Not Me._loggedin Then
                    Me.Login()
                End If
                sendCommand("MDTM " & fileName)

                If Me._resultCode = 213 Then
                    modDate = Int64.Parse(Me._result.Substring(4)).ToString
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Debug.Assert(False, "Error from: CEL.NET.FTPClient.GetLastMod:", Me._result.Substring(4))
            End Try

            Return modDate
        End Function

        Public Function GetWorkingDirectory() As String
            If Not Me._loggedin Then
                Me.Login()
            End If
            sendCommand("PWD")
            Dim dir As String = ""
            If Me._resultCode = 211 Then
                dir = Int64.Parse(Me._result.Substring(4)).ToString
            Else
                Debug.Assert(False, "Exception from: CEL.NET.FTPClient.GetWorkingDirectory:", Me._result.Substring(4))
            End If
            Return dir
        End Function

        Public Sub GetHelp()
            If Not Me._loggedin Then
                Me.Login()
            End If
            sendCommand("HELP ")
            Dim dir As String = ""
            If Me._resultCode = 211 Then

                dir = Int64.Parse(Me._result.Substring(4)).ToString
                Console.WriteLine("{0}", dir)
            Else
                Debug.Assert(False, "Exception from: CEL.NET.FTPClient.GetHelp:", Me._result.Substring(4))
            End If
            Console.WriteLine("{0}", dir)
        End Sub

        '''
        ''' Upload a file.
        '''
        Public Sub Upload(ByVal filePathName As String)
            Me.Upload(filePathName, False)
        End Sub

        '''
        ''' Upload a file and set the resume flag.
        '''
        Public Sub Upload(ByVal filePathName As String, ByVal [resume] As Boolean)

            If Not Me._loggedin Then
                Me.Login()
            End If
            Dim cSocket As Socket = createDataSocket()
            Dim offset As Long = 0
            If [resume] Then
                Try
                    Me.BinaryMode = True
                    offset = GetFileSize(Path.GetFileName(filePathName))
                Catch generatedExceptionVariable0 As Exception
                    offset = 0
                End Try
            End If
            Try
                'cSocket = Me.createDataSocket
                If offset > 0 Then
                    Me.sendCommand("REST " + offset.ToString)
                    If Me._resultCode <> 350 Then
                        Console.WriteLine("Resuming not supported", "FtpClient")
                        offset = 0
                    End If
                End If
                Me.sendCommand("STOR " + Path.GetFileName(filePathName))
                If Not (Me._resultCode = 125 OrElse Me._resultCode = 150) Then
                    Debug.Assert(False, "Exception from: CEL.NET.FTPClient.Upload:", Me._result.Substring(4))
                End If
                Dim input As New FileStream(filePathName, FileMode.Open)
                If offset <> 0 Then
                    If Me.VerboseDebugging Then
                        Console.WriteLine("Resuming at offset " + offset.ToString())
                        Console.WriteLine("")
                    End If
                    input.Seek(offset, SeekOrigin.Begin)
                End If
                Console.WriteLine("Uploading file " + filePathName + " to " + RemotePath, "FtpClient")
                While (InlineAssignHelper(Me._bytes, input.Read(Me._buffer, 0, Me._buffer.Length))) > 0
                    cSocket.Send(Me._buffer, Me._bytes, 0)
                End While

                input.Close()
                Console.WriteLine("")
                If cSocket.Connected Then
                    cSocket.Close()
                End If
                Me.readResponse()
                'Console.WriteLine("readResponse: {0}", Me.readResponse())
                If Not (Me._resultCode = 226 OrElse Me._resultCode = 250) Then
                    Debug.Assert(False, "Exception from: CEL.NET.FTPClient.Upload:", Me._result.Substring(4))
                End If
            Catch ex As Exception
                Console.WriteLine("Error from CEL.NET.FTPClient.Upload: This error was created {0}", ex.Message)
                Debug.Assert(False, "Error: CEL.NET.FTPCLIENT.Upload: Usually crash if the function is missing a parameter or the file you are trying to Upload doesn't exist.")
            End Try

        End Sub

        '''
        ''' Upload a file under a given name and set the resume flag.
        '''
        '''
        Public Sub upload(ByVal fileName As String, ByVal saveName As String, Optional ByVal [resume] As Boolean = False)
            If Not Me._loggedin Then
                Login()
            End If
            Dim cSocket As Socket = createDataSocket()
            Dim offset As Long = 0
            If [resume] Then
                Try
                    Me.BinaryMode = True
                    offset = GetFileSize(fileName)
                Catch generatedExceptionName As Exception
                    offset = 0
                End Try
            End If
            Try
                If offset > 0 Then
                    sendCommand("REST " & offset)
                    If Me._resultCode <> 350 Then
                        Debug.Assert(False, "Exception from: CEL.NET.FTPClient.Upload:", Me._result.Substring(4))
                        offset = 0
                    End If
                End If
                'sendCommand("STOR " & Path.GetFileName(fileName))
                sendCommand("STOR " & saveName)
                If Not (Me._resultCode = 125 OrElse Me._resultCode = 150) Then
                    Debug.Assert(False, "Exception from: CEL.NET.FTPClient.Upload:", Me._result.Substring(4))
                End If
                ' open input stream to read source file
                Dim input As New FileStream(fileName, FileMode.Open)
                If offset <> 0 Then
                    If Me.VerboseDebugging Then
                        Console.WriteLine("seeking to " & offset)
                    End If
                    input.Seek(offset, SeekOrigin.Begin)
                End If
                If Me.VerboseDebugging Then
                    Console.WriteLine(("Uploading file " & fileName & " to ") + RemotePath)
                    Console.WriteLine("")
                End If
                While (InlineAssignHelper(Me._bytes, input.Read(Me._buffer, 0, Me._buffer.Length))) > 0
                    cSocket.Send(Me._buffer, Me._bytes, 0)
                End While
                input.Close()
                If cSocket.Connected Then
                    cSocket.Close()
                End If
                Me.readResponse()
                If Not (Me._resultCode = 226 OrElse Me._resultCode = 250) Then
                    Debug.Assert(False, "Exception from: CEL.NET.FTPClient.Upload:", Me._result.Substring(4))
                End If
            Catch ex As Exception
                Debug.Assert(False, "Error: CEL.NET.FTPCLIENT.Upload: Usually crash if the function is missing a parameter.")
                Console.WriteLine("Error from CEL.NET.FTPClient.Upload: This error was created {0}", ex.Message)
            End Try
        End Sub
        Public Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
            target = value
            Return value
        End Function

        'not tested
        Public Sub UploadDirectory(ByVal path As String, ByVal recurse As Boolean)
            Me.UploadDirectory(path, recurse, "*.*")
        End Sub

        'not tested
        Public Sub UploadDirectory(ByVal path As String, ByVal recurse As Boolean, ByVal mask As String)
            Dim dirs As String() = path.Replace("/", "\").Split("\"c)
            Dim rootDir As String = dirs(dirs.Length - 1)
            If Me.GetFileList(rootDir).Length < 1 Then
                Me.MakeDir(rootDir)
            End If
            Me.ChangeDir(rootDir)
            For Each file As String In Directory.GetFiles(path, mask)
                Me.Upload(file, True)
            Next
            If recurse Then
                For Each dir As String In Directory.GetDirectories(path)
                    Me.UploadDirectory(dir, recurse, mask)
                Next
            End If
            Me.ChangeDir(".")
        End Sub

        '''
        ''' Delete a file from the remote FTP server.
        '''
        Public Sub DeleteFile(ByVal fileName As String)
            If Not Me._loggedin Then
                Me.Login()
            End If
            Me.sendCommand("DELE " + fileName)
            If Me._resultCode <> 250 Then
                Debug.Assert(False, "Exception from: CEL.NET.FTPClient.DeleteFile:", Me._result.Substring(4))
            End If
            If Me.VerboseDebugging Then
                Debug.WriteLine("Deleted file " + fileName, "FtpClient")
            End If
        End Sub

        Public Sub RenameFile(ByVal oldFileName As String, ByVal newFileName As String, ByVal overwrite As Boolean)
            If Not Me._loggedin Then
                Me.Login()
            End If
            Me.sendCommand("RNFR " + oldFileName)
            If Not (Me._resultCode = 350) Then
                Debug.Assert(False, "Exception from: CEL.NET.FTPClient.RenameFile:", Me._result.Substring(4))
            End If
            If Not overwrite AndAlso Me.GetFileList(newFileName).Length > 0 Then
                Console.WriteLine("File already exists")
                Debug.Assert(False, "Exception from: CEL.NET.FTPClient.RenameFile:", Me._result.Substring(4))
            End If
            Me.sendCommand("RNTO " + newFileName)
            If Not (Me._resultCode = 250) Then
                Debug.Assert(False, "Exception from: CEL.NET.FTPClient.RenameFile:", Me._result.Substring(4))
            End If
            Debug.WriteLine("Renamed file " + oldFileName + " to " + newFileName, "FtpClient")
        End Sub

        '''
        ''' Create a directory on the remote FTP server.
        '''
        Public Sub MakeDir(ByVal dirName As String)
            If Not Me._loggedin Then
                Me.Login()
            End If
            Me.sendCommand("MKD " + dirName)
            If Not (Me._resultCode = 250) AndAlso Not (Me._resultCode = 257) Then
                Debug.Assert(False, "Exception from: CEL.NET.FTPClient.MakeDir:", Me._result.Substring(4))
            End If
            If Me.VerboseDebugging Then
                Console.WriteLine("Created directory " + dirName, "FtpClient")
            End If

        End Sub

        '''
        ''' Delete a directory on the remote FTP server.
        '''
        Public Sub RemoveDir(ByVal dirName As String)
            If Not Me._loggedin Then
                Me.Login()
            End If
            Me.sendCommand("RMD " + dirName)
            If Not (Me._resultCode = 250) Then
                Debug.Assert(False, "Exception from: CEL.NET.FTPClient.RemoveDir:", Me._result.Substring(4))
            End If
            Debug.WriteLine("Removed directory " + dirName, "FtpClient")
        End Sub

        ''' <summary>
        ''' Change the Working Ftp Folder to the one given in parameter.
        ''' </summary>
        ''' <param name="dirName"></param>
        ''' <remarks>Attempt to access the asked directory. If UnSuccessful, attemp to create said directry and try again to access it.
        ''' This cycle will happen for each branch of the directory path. ChangeDir will climb the directory tree and create branch as required </remarks>
        Public Sub ChangeCreateDir(ByVal dirName As String)
            If dirName Is Nothing OrElse dirName.Equals(".") OrElse dirName.Length = 0 Then
                Return
            End If
            If Not Me._loggedin Then
                Me.Login()
            End If

            'Attempt to access the ask directory. 
            Me.sendCommand("CWD " + dirName)
            If Me._resultCode <> 250 And dirName <> "./" Then
                Dim splitDirName() As String = dirName.Split("/"c)

                For Each folderName As String In splitDirName
                    If folderName Is Nothing Or folderName = "." Then folderName = "./"
                    Me.sendCommand("CWD " + folderName)
                    'If cannot access folder, attemp to create it.
                    If Me._resultCode <> 250 Then
                        'if folderName is actually something, create the folder since it doesn't exist.
                        If folderName <> "" Then MakeDir(folderName)
                        If Me.VerboseDebugging Then
                            Console.WriteLine("The Directory requested, {0}, did not exist and was created.", folderName)
                        End If
                    End If
                    Me.sendCommand("CWD " + folderName)
                Next folderName
            End If

            Me.sendCommand("PWD")
            If Not (Me._resultCode = 257) Then
                Debug.Assert(False, "Exception from: CEL.NET.FTPClient.ChangeDir:", Me._result.Substring(4))
            End If
            Me.RemotePath = Me._message.Split(""""c)(1)
            If Me.VerboseDebugging Then
                Console.WriteLine("Current directory is " + Me.RemotePath, "FtpClient")
            End If
        End Sub

        Public Sub ChangeDir(ByVal dirName As String)
            If dirName Is Nothing OrElse dirName.Equals(".") OrElse dirName.Length = 0 Then
                Return
            End If
            If Not Me._loggedin Then
                Me.Login()
            End If
            Me.sendCommand("CWD " + dirName)
            If Not (Me._resultCode = 250) Then
                Throw New FtpException(Me._result.Substring(4))
            End If
            Me.sendCommand("PWD")
            If Not (Me._resultCode = 257) Then
                Throw New FtpException(Me._result.Substring(4))
            End If
            Me.remotePath = Me._message.Split(""""c)(1)
            Debug.WriteLine("Current directory is " + Me.remotePath, "FtpClient")
        End Sub

        Private Sub readResponse()
            Me._message = ""
            Me._result = Me.readLine
            Console.WriteLine("(FTP) Response: " + Me._result.ToString())

            If Me._result.Length <= 3 Or String.Compare(Me._result.Substring(0, 3), "   ") = 0 Then
                'The [String.Compare(Me._result.Substring(0, 3), "   ")] check was added to avoid the following issue:
                'When sending the command "QUIT" to the QNX through FTP, 
                'instead of replying "221 Thank you for using the FTP service on 192.168.2.200.", the QNX will sometimes reply the following string (i am not sure why)
                ' "    Total traffic for this session was 179510 bytes in 12 transfers. "
                'This causes an exception during the "Integer.Parse() fuction used to retreive the result_code.
                'Since we don't do a check for the result code 221 after a QUIT, I just set it to be nothing instead. (Shahbaz)
                Me._result = Nothing
            Else
                Me._resultCode = Integer.Parse(Me._result.Substring(0, 3))
            End If

        End Sub

        Private Function readLine() As String
            While True
                Me._bytes = Me._clientSocket.Receive(Me._buffer, Me._buffer.Length, 0)
                Me._message += ASCII.GetString(Me._buffer, 0, Me._bytes)
                If Me._bytes < Me._buffer.Length Then
                    Exit While
                End If
            End While
            Dim msg As String() = Me._message.Split(Microsoft.VisualBasic.Chr(10))
            If Me._message.Length > 2 Then
                Me._message = msg(msg.Length - 2)
            Else
                Me._message = msg(0)
            End If
            If Me._message.Length > 4 AndAlso Not Me._message.Substring(3, 1).Equals(" ") Then
                Return Me.readLine
            End If
            If Me.VerboseDebugging Then
                Dim i As Integer = 0
                While i < msg.Length - 1
                    Debug.Write(msg(i), "FtpClient")
                    System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
                End While
            End If
            Return Me._message
        End Function

        Private Sub sendCommand(ByVal command As String)
            'If Me.VerboseDebugging Then
            '    Console.WriteLine("Sending command :{0}", command)
            '    Console.WriteLine("")
            'End If
            Dim cmdBytes As Byte() = Encoding.ASCII.GetBytes((command + "" & Microsoft.VisualBasic.Chr(13) & "" & Microsoft.VisualBasic.Chr(10) & "").ToCharArray)
            Me._clientSocket.Send(cmdBytes, cmdBytes.Length, 0)
            Console.WriteLine("(FTP) Sent Command: " & command)
            Me.readResponse()
        End Sub

        Private Function createDataSocket() As Socket
            'ftp file transfer REQUIRE "PASV to be send before a file transfer command)
            Me.sendCommand("PASV")
            If Not (Me._resultCode = 227) Then
                Debug.Assert(False, Me._result.Substring(4))
                'Throw New FtpException(Me._result.Substring(4))
            End If
            Dim index1 As Integer = Me._result.IndexOf("("c)
            Dim index2 As Integer = Me._result.IndexOf(")"c)
            Dim ipData As String = Me._result.Substring(index1 + 1, index2 - index1 - 1)
            Dim parts As Integer() = New Integer(5) {}
            Dim len As Integer = ipData.Length
            Dim partCount As Integer = -1
            Dim buf As String = ""
            Dim i As Integer = 0
            While i < len AndAlso partCount <= 6
                Dim ch As Char = Char.Parse(ipData.Substring(i, 1))
                If Char.IsDigit(ch) Then
                    buf += ch
                ElseIf ch <> ","c Then
                    Debug.Assert(False, "Exception from CEL.NET.FTPClient.CreateDataSocket: Malformed PASV result:" + Me._result)
                End If
                If ch = ","c OrElse i + 1 = len Then
                    Try
                        parts(System.Math.Max(System.Threading.Interlocked.Increment(partCount), partCount - 1)) = Integer.Parse(buf)
                        buf = ""
                    Catch ex As Exception
                        Debug.Assert(False, "Exception from CEL.NET.FTPClient.CreateDataSocket: Malformed PASV result (not Supported?):" + Me._result)
                    End Try
                End If
                i += 1
            End While
            Dim ipAddress As String = parts(0).ToString() + "." + parts(1).ToString() + "." + parts(2).ToString() + "." + parts(3).ToString()
            Dim port As Integer = (parts(4) << 8) + parts(5)
            Dim socket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

            Dim ips As IPAddress()
            ips = Dns.GetHostAddresses(ipAddress)
            Dim endPoint As New IPEndPoint(ips(0), port)
            Try
                socket.Connect(endPoint)
            Catch ex As Exception
                If Not socket.Connected Then Console.WriteLine("socket not connect")
                If Not (socket Is Nothing) AndAlso socket.Connected Then
                    socket.Close()
                End If
                Debug.Assert(False, "Error from CEL.NET.FTPClient.CreateDataSocket: ", ex.Message)
            End Try

            Return socket
        End Function

        Private Sub cleanup()
            If Not (Me._clientSocket Is Nothing) Then
                Me._clientSocket.Close()
                Me._clientSocket = Nothing
            End If
            Me._loggedin = False
        End Sub

        Protected Overrides Sub Finalize()
            Me.cleanup()
        End Sub

        Private Delegate Sub LoginCallback()

        Public Function BeginLogin(ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As LoginCallback = AddressOf Login
            Return ftpCallback.BeginInvoke(callback, Nothing)
        End Function

        Private Delegate Sub CloseCallback()

        Public Function BeginClose(ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As CloseCallback = AddressOf Close
            Return ftpCallback.BeginInvoke(callback, Nothing)
        End Function

        Private Delegate Function GetFileListCallback() As String()

        Public Function BeginGetFileList(ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As GetFileListCallback = AddressOf GetFileList
            Return ftpCallback.BeginInvoke(callback, Nothing)
        End Function

        Private Delegate Function GetFileListMaskCallback(ByVal mask As String) As String()

        Public Function BeginGetFileList(ByVal mask As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As GetFileListMaskCallback = AddressOf GetFileList
            Return ftpCallback.BeginInvoke(mask, callback, Nothing)
        End Function

        Private Delegate Function GetFileSizeCallback(ByVal fileName As String) As Int64

        Public Function BeginGetFileSize(ByVal fileName As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As GetFileSizeCallback = AddressOf GetFileSize
            Return ftpCallback.BeginInvoke(fileName, callback, Nothing)
        End Function

        Private Delegate Sub DownloadCallback(ByVal remFileName As String)

        Public Function BeginDownload(ByVal remFileName As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As DownloadCallback = AddressOf Download
            Return ftpCallback.BeginInvoke(remFileName, callback, Nothing)
        End Function

        Private Delegate Sub DownloadFileNameResumeCallback(ByVal remFileName As String, ByVal [resume] As Boolean)

        Public Function BeginDownload(ByVal remFileName As String, ByVal [resume] As Boolean, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As DownloadFileNameResumeCallback = AddressOf Download
            Return ftpCallback.BeginInvoke(remFileName, [resume], callback, Nothing)
        End Function

        Private Delegate Sub DownloadFileNameFileNameCallback(ByVal remFileName As String, ByVal locFileName As String)

        Public Function BeginDownload(ByVal remFileName As String, ByVal locFileName As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As DownloadFileNameFileNameCallback = AddressOf Download
            Return ftpCallback.BeginInvoke(remFileName, locFileName, callback, Nothing)
        End Function

        Private Delegate Sub DownloadFileNameFileNameResumeCallback(ByVal remFileName As String, ByVal locFileName As String, ByVal [resume] As Boolean)

        Public Function BeginDownload(ByVal remFileName As String, ByVal locFileName As String, ByVal [resume] As Boolean, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As DownloadFileNameFileNameResumeCallback = AddressOf Download
            Return ftpCallback.BeginInvoke(remFileName, locFileName, [resume], callback, Nothing)
        End Function

        Private Delegate Sub UploadCallback(ByVal fileName As String)

        Public Function BeginUpload(ByVal fileName As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As UploadCallback = AddressOf Upload
            Return ftpCallback.BeginInvoke(fileName, callback, Nothing)
        End Function

        Private Delegate Sub UploadFileNameResumeCallback(ByVal fileName As String, ByVal [resume] As Boolean)

        Public Function BeginUpload(ByVal fileName As String, ByVal [resume] As Boolean, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As UploadFileNameResumeCallback = AddressOf Upload
            Return ftpCallback.BeginInvoke(fileName, [resume], callback, Nothing)
        End Function

        Private Delegate Sub UploadDirectoryCallback(ByVal path As String, ByVal recurse As Boolean)

        Public Function BeginUploadDirectory(ByVal path As String, ByVal recurse As Boolean, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As UploadDirectoryCallback = AddressOf UploadDirectory
            Return ftpCallback.BeginInvoke(path, recurse, callback, Nothing)
        End Function

        Private Delegate Sub UploadDirectoryPathRecurseMaskCallback(ByVal path As String, ByVal recurse As Boolean, ByVal mask As String)

        Public Function BeginUploadDirectory(ByVal path As String, ByVal recurse As Boolean, ByVal mask As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As UploadDirectoryPathRecurseMaskCallback = AddressOf UploadDirectory
            Return ftpCallback.BeginInvoke(path, recurse, mask, callback, Nothing)
        End Function

        Private Delegate Sub DeleteFileCallback(ByVal fileName As String)

        Public Function BeginDeleteFile(ByVal fileName As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As DeleteFileCallback = AddressOf DeleteFile
            Return ftpCallback.BeginInvoke(fileName, callback, Nothing)
        End Function

        Private Delegate Sub RenameFileCallback(ByVal oldFileName As String, ByVal newFileName As String, ByVal overwrite As Boolean)

        Public Function BeginRenameFile(ByVal oldFileName As String, ByVal newFileName As String, ByVal overwrite As Boolean, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As RenameFileCallback = AddressOf RenameFile
            Return ftpCallback.BeginInvoke(oldFileName, newFileName, overwrite, callback, Nothing)
        End Function

        Private Delegate Sub MakeDirCallback(ByVal dirName As String)

        Public Function BeginMakeDir(ByVal dirName As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As MakeDirCallback = AddressOf MakeDir
            Return ftpCallback.BeginInvoke(dirName, callback, Nothing)
        End Function

        Private Delegate Sub RemoveDirCallback(ByVal dirName As String)

        Public Function BeginRemoveDir(ByVal dirName As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As RemoveDirCallback = AddressOf RemoveDir
            Return ftpCallback.BeginInvoke(dirName, callback, Nothing)
        End Function

        Private Delegate Sub ChangeDirCallback(ByVal dirName As String)

        Public Function BeginChangeDir(ByVal dirName As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As ChangeDirCallback = AddressOf ChangeDir
            Return ftpCallback.BeginInvoke(dirName, callback, Nothing)
        End Function

        Public Function BeginChangeCreateDir(ByVal dirName As String, ByVal callback As System.AsyncCallback) As System.IAsyncResult
            Dim ftpCallback As ChangeDirCallback = AddressOf ChangeCreateDir
            Return ftpCallback.BeginInvoke(dirName, callback, Nothing)
        End Function

    End Class
End Namespace


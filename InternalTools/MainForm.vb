Imports CEL.HMI
Imports EntitySpaces.Interfaces
Imports System.ComponentModel
Imports System.Security.Principal
Imports System.Runtime.InteropServices
Imports System.IO
Imports CEL.Data.BusinessObjects
Imports System.Xml
Imports System.Xml.Linq

Public Class MainForm

    Private CELProject As CELDAS = CELDAS.Instance()



    Public Class ImpersonateUser
        Implements IDisposable

        Private LastContext As WindowsImpersonationContext = Nothing
        Private LastUserHandle As IntPtr = IntPtr.Zero

#Region "User Impersonation api"
        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function LogonUser(lpszUsername As String, lpszDomain As String, lpszPassword As String, dwLogonType As Integer, dwLogonProvider As Integer, ByRef phToken As IntPtr) As Boolean
        End Function

        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function ImpersonateLoggedOnUser(Token As Integer) As Boolean
        End Function

        <DllImport("advapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
        Public Shared Function DuplicateToken(token As IntPtr, impersonationLevel As Integer, ByRef duplication As IntPtr) As Boolean
        End Function

        <DllImport("kernel32.dll")> _
        Public Shared Function CloseHandle(hObject As IntPtr) As Boolean
        End Function

        Public Const LOGON32_PROVIDER_DEFAULT As Integer = 0
        Public Const LOGON32_PROVIDER_WINNT35 As Integer = 1
        Public Const LOGON32_LOGON_INTERACTIVE As Integer = 2
        Public Const LOGON32_LOGON_NETWORK As Integer = 3
        Public Const LOGON32_LOGON_BATCH As Integer = 4
        Public Const LOGON32_LOGON_SERVICE As Integer = 5
        Public Const LOGON32_LOGON_UNLOCK As Integer = 7
        Public Const LOGON32_LOGON_NETWORK_CLEARTEXT As Integer = 8
        ' Win2K or higher
        Public Const LOGON32_LOGON_NEW_CREDENTIALS As Integer = 9
        ' Win2K or higher
#End Region


        Public Sub New(username As String, domainOrComputerName As String, password As String, Optional nm As Integer = LOGON32_LOGON_NETWORK)

            Dim userToken As IntPtr = IntPtr.Zero
            Dim userTokenDuplication As IntPtr = IntPtr.Zero

            Dim loggedOn As Boolean = False

            If domainOrComputerName Is Nothing Then
                domainOrComputerName = Environment.UserDomainName
            End If

            If domainOrComputerName.ToLower() = "nt authority" Then
                loggedOn = LogonUser(username, domainOrComputerName, password, LOGON32_LOGON_SERVICE, LOGON32_PROVIDER_DEFAULT, userToken)
            Else
                loggedOn = LogonUser(username, domainOrComputerName, password, nm, LOGON32_PROVIDER_DEFAULT, userToken)
            End If

            Dim _impersonationContext As WindowsImpersonationContext = Nothing
            If loggedOn Then
                Try
                    ' Create a duplication of the usertoken, this is a solution
                    ' for the known bug that is published under KB article Q319615.
                    If DuplicateToken(userToken, 2, userTokenDuplication) Then
                        ' Create windows identity from the token and impersonate the user.
                        Dim identity As New WindowsIdentity(userTokenDuplication)
                        _impersonationContext = identity.Impersonate()
                    Else
                        ' Token duplication failed!
                        ' Use the default ctor overload
                        ' that will use Mashal.GetLastWin32Error();
                        ' to create the exceptions details.
                        Throw New Win32Exception()
                    End If
                Finally
                    ' Close usertoken handle duplication when created.
                    If Not userTokenDuplication.Equals(IntPtr.Zero) Then
                        ' Closes the handle of the user.
                        CloseHandle(userTokenDuplication)
                        userTokenDuplication = IntPtr.Zero
                    End If

                    ' Close usertoken handle when created.
                    If Not userToken.Equals(IntPtr.Zero) Then
                        ' Closes the handle of the user.
                        CloseHandle(userToken)
                        userToken = IntPtr.Zero
                    End If
                End Try
            Else
                ' Logon failed!
                ' Use the default ctor overload that 
                ' will use Mashal.GetLastWin32Error();
                ' to create the exceptions details.
                Throw New Win32Exception()
            End If

            If LastContext Is Nothing Then
                LastContext = _impersonationContext
            End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            LastContext.Undo()
            LastContext.Dispose()
        End Sub
    End Class


   

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()




        CELProject.DefaultConnectionName = "ACCESSDynamic"
        CELProject.EngineModel = "APS3200"
        CELProject.Database.Password = "CEL9994"


        Dim impersonation = New ImpersonateUser(CELProject.Database.ComputerLoginName, CELProject.Database.ComputerName, CELProject.Project.Number, ImpersonateUser.LOGON32_LOGON_NEW_CREDENTIALS)


        ' Add any initialization after the InitializeComponent() call.
        'Ne pas supprimer.  Pour overrider dynamiquement une connectionstring de entity spaces
        For Each connElement As esConnectionElement In esConfigSettings.ConnectionInfo.Connections

            'connElement.ConnectionString = connElement.ConnectionString.Replace("Password=", "Password=CEL9994" + CELProject.Database.Password) 'CELProject.Database.OleDbConnectionString
            'Solution de contournement pour setter le mot de passe par le code dans la connectionString
            'Dim connectionString As String = connElement.Replace("Password=", "Password=" + CELProject.Database.Password)

            Dim newConn As New esConnectionElement
            newConn.ConnectionString = CELProject.Database.OleDbConnectionString
            newConn.Name = connElement.Name
            newConn.Provider = connElement.Provider
            newConn.ProviderClass = connElement.ProviderClass
            newConn.SqlAccessType = esSqlAccessType.DynamicSQL
            newConn.ProviderMetadataKey = connElement.ProviderMetadataKey
            newConn.CommandTimeout = 20
            esConfigSettings.ConnectionInfo.Connections.Add(newConn)
            esConfigSettings.ConnectionInfo.Default = CELProject.DefaultConnectionName
        Next

        'Initialize database engine
        esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()

    End Sub

    Private Sub btnImportAfdxDefinition_Click(sender As Object, e As EventArgs) Handles btnImportAfdxDefinition.Click
        Dim frmImportAFDX As New ImportExportAfdxDefitionForm(CELProject)
        frmImportAFDX.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' RecursiveDirectoryCopy("C:\My Projects\CELDAS 200", True, True)
    End Sub

    Private Sub RecursiveDirectoryCopy(ByVal sourceDir As String, ByVal fRecursive As Boolean, ByVal overWrite As Boolean)
        Dim sDir As String
        Dim dDirInfo As IO.DirectoryInfo
        Dim sDirInfo As IO.DirectoryInfo
        Dim sFile As String
        Dim sFileInfo As IO.FileInfo
        Dim dFileInfo As IO.FileInfo
        ' Add trailing separators to the supplied paths if they don't exist.
        If Not sourceDir.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) Then
            sourceDir &= System.IO.Path.DirectorySeparatorChar
        End If




        ' Recursive switch to continue drilling down into directory structure.
        If fRecursive Then
            ' Get a list of directories from the current parent.
            For Each sDir In System.IO.Directory.GetDirectories(sourceDir)
                sDirInfo = New System.IO.DirectoryInfo(sDir)
                If Not sDir.EndsWith(".svn") Then
                    ' Since we are in recursive mode, copy the children also
                    RecursiveDirectoryCopy(sDirInfo.FullName, fRecursive, overWrite)
                End If
                sDirInfo = Nothing
                dDirInfo = Nothing
            Next
        End If
        ' Get the files from the current parent.
        For Each sFile In System.IO.Directory.GetFiles(sourceDir)
            sFileInfo = New System.IO.FileInfo(sFile)
            Dim fileExtension As String = Path.GetExtension(sFileInfo.FullName)
            If sFileInfo.FullName.Contains("v2.0.") Then
                'File.Delete(sFileInfo.FullName)
                Continue For
            End If
            If fileExtension = ".csproj" Or fileExtension = ".vbproj" Or fileExtension = ".user" Then
                Dim newFilename As String = Path.GetFileName(sFileInfo.FullName)
                newFilename = newFilename.Replace(".csproj", ".v2.0.csproj")
                newFilename = newFilename.Replace(".vbproj", ".v2.0.vbproj")

                Try


                    sFileInfo.CopyTo(sourceDir + newFilename, overWrite)
                Catch ex As Exception
                    Console.WriteLine("skip " + sourceDir + newFilename)
                End Try
            End If
            'If file exists and is the same length (size). Skip.
            ''If file exists and is of different Length (size) and overwrite = True. Copy
            'If sFileInfo.Length AndAlso overWrite Then
            '    'sFileInfo.CopyTo(sFileInfo.FullName, overWrite)
            '    'If file exists and is of different Length (size) and overwrite = False. Skip
            'ElseIf sFileInfo.Length AndAlso Not overWrite Then
            '    Debug.WriteLine(sFileInfo.FullName & " Not copied.")
            'End If

            sFileInfo = Nothing
            dFileInfo = Nothing
        Next
    End Sub

    Private Sub btnFTP_Click(sender As Object, e As EventArgs) Handles btnFTP.Click

        Dim ftmFTP As New FTP
        ftmFTP.ShowDialog()
    End Sub

    Private Sub btnCreateFS_Click(sender As Object, e As EventArgs) Handles btnCreateFS.Click
        ''Query Database
        'Dim booleanChannelsCollection As HMIBooleanChannelsCollection = New HMIBooleanChannelsCollection()
        'booleanChannelsCollection.es.Connection.Name = CELProject.DefaultConnectionName
        'booleanChannelsCollection.Query.Where(booleanChannelsCollection.Query.CollectionName = "PW980A")
        'booleanChannelsCollection.Query.Where(booleanChannelsCollection.Query.Unavailable = False)
        'booleanChannelsCollection.Query.Where(booleanChannelsCollection.Query.ChannelName.Like("%AFDX_%"))
        'booleanChannelsCollection.Query.Load()




        ''Query Database
        'Dim numericChannelsCollection As HMINumericChannelsCollection = New HMINumericChannelsCollection()
        'numericChannelsCollection.es.Connection.Name = CELProject.DefaultConnectionName
        'numericChannelsCollection.Query.Where(numericChannelsCollection.Query.CollectionName = "PW980A")
        'numericChannelsCollection.Query.Where(numericChannelsCollection.Query.Unavailable = False)
        'numericChannelsCollection.Query.Where(numericChannelsCollection.Query.ChannelName.Like("AFDX_"))
        'numericChannelsCollection.Query.Load()

        'For Each channel As HMIBooleanChannels In booleanChannelsCollection

        '    Dim newChannel As HMINumericChannels = numericChannelsCollection.AddNew()
        '    newChannel.CollectionName = "PW980A"
        '    newChannel.ChannelName = channel.ChannelName + "_FS"
        '    newChannel.Description = channel.ChannelName + "_FS"
        '    newChannel.Source = "AFDX_FS_OK"

        '    numericChannelsCollection.Save()
        'Next


        'Query Database
        Dim numericChannelsCollection As HMINumericChannelsCollection = New HMINumericChannelsCollection()
        numericChannelsCollection.es.Connection.Name = CELProject.DefaultConnectionName
        numericChannelsCollection.Query.Where(numericChannelsCollection.Query.CollectionName = "PW980A")
        numericChannelsCollection.Query.Where(numericChannelsCollection.Query.Unavailable = False)
        numericChannelsCollection.Query.Where(numericChannelsCollection.Query.ChannelName.Like("%AFDX_%"))
        numericChannelsCollection.Query.Load()

        For Each channel As HMINumericChannels In numericChannelsCollection

            If channel.ChannelName.StartsWith("AFDX_") And channel.Description = "" Then

                channel.Description = channel.ChannelName

            End If

        Next
        numericChannelsCollection.Save()
    End Sub


    Private Sub btnUpdateCeldasSo_Click(sender As Object, e As EventArgs) Handles btnUpdateCeldasSo.Click
        Dim frmUpdate As New CeldasSoUpdateForm
        frmUpdate.ShowDialog()
        Me.Dispose()
    End Sub

    Private Sub btnDatabaseImportUtility_Click(sender As Object, e As EventArgs) Handles btnDatabaseImportUtility.Click
        Dim frmDatabaseImportUtility As New DatabaseImporterForm
        frmDatabaseImportUtility.ShowDialog()
    End Sub

    Private Shared _AppConfigFileName As String = ""
    Public Shared Property AppConfigFileName() As String
        Get
            Return _AppConfigFileName
        End Get
        Set(ByVal value As String)
            _AppConfigFileName = value
        End Set
    End Property


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ExportSoftware As New CEL.HMI.HardwareConfiguration

        Dim arguments() As String = System.Environment.GetCommandLineArgs()
        For Each argument As String In arguments
            If argument.Contains("/config=") Then
                _AppConfigFileName = argument.Split("="c)(1)
            End If
        Next


        ExportSoftware.ExportAllCsv(AppConfigFileName) 'Return 1 if succesful
    End Sub
End Class
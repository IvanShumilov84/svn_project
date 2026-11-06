Imports System
Imports System.Security.Principal
Imports System.Runtime.InteropServices
Imports System.ComponentModel

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
    Public ReadOnly Property LoggedOn As Boolean
        Get
            Return _loggedOn
        End Get
    End Property
    Private _loggedOn As Boolean = False
    Public Sub New(username As String, domainOrComputerName As String, password As String, Optional nm As Integer = LOGON32_LOGON_NETWORK)

        Dim userToken As IntPtr = IntPtr.Zero
        Dim userTokenDuplication As IntPtr = IntPtr.Zero
        'Dim loggedOn As Boolean = False

        If domainOrComputerName Is Nothing Or domainOrComputerName = String.Empty Then
            domainOrComputerName = Environment.UserDomainName
        End If

        If domainOrComputerName.ToLower() = "nt authority" Then
            _loggedOn = LogonUser(username, domainOrComputerName, password, LOGON32_LOGON_SERVICE, LOGON32_PROVIDER_DEFAULT, userToken)
            'If testsession is located on a domain computer and the CELDAS200 is launched on a machine not on the domain
        ElseIf domainOrComputerName.tolower.StartsWith("cel01") And Not (Environment.MachineName.ToLower.StartsWith("cel01")) Then
            _loggedOn = False
        Else
            _loggedOn = LogonUser(username, domainOrComputerName, password, nm, LOGON32_PROVIDER_DEFAULT, userToken)
        End If

        Dim _impersonationContext As WindowsImpersonationContext = Nothing
        If _loggedOn Then
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
            'ft desactive  Throw New Win32Exception()
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


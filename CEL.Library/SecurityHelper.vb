Imports System
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports Microsoft.VisualBasic

Public Class SecurityHelper
    'http://www.davidmoore.info/2011/06/20/how-to-check-if-the-current-user-is-an-administrator-even-if-uac-is-on/
    <DllImport("advapi32.dll", SetLastError:=True)> _
    Private Shared Function GetTokenInformation(ByVal tokenHandle As IntPtr, _
                                                    ByVal tokenInformationClass As TokenInformationClass, _
                                                    ByVal tokenInformation As IntPtr, _
                                                    ByVal tokenInformationLength As UInt32, _
                                                    ByRef returnLength As UInt32) As Boolean
    End Function

    '/// <summary>
    '/// Passed to <see cref="GetTokenInformation"/> to specify what
    '/// information about the token to return.
    '/// </summary>
    Public Enum TokenInformationClass
        TokenUser = 1
        TokenGroups
        TokenPrivileges
        TokenOwner
        TokenPrimaryGroup
        TokenDefaultDacl
        TokenSource
        TokenType
        TokenImpersonationLevel
        TokenStatistics
        TokenRestrictedSids
        TokenSessionId
        TokenGroupsAndPrivileges
        TokenSessionReference
        TokenSandBoxInert
        TokenAuditPolicy
        TokenOrigin
        TokenElevationType
        TokenLinkedToken
        TokenElevation
        TokenHasRestrictions
        TokenAccessInformation
        TokenVirtualizationAllowed
        TokenVirtualizationEnabled
        TokenIntegrityLevel
        TokenUiAccess
        TokenMandatoryPolicy
        TokenLogonSid
        MaxTokenInfoClass
    End Enum
    '/// <summary>
    '/// The elevation type for a user token.
    '/// </summary>
    Public Enum TokenElevationType
        TokenElevationTypeDefault = 1
        TokenElevationTypeFull
        TokenElevationTypeLimited
        TokenElevationTypeUnknown
    End Enum
    Public Shared Function HasAdministorRight() As Boolean

        HasAdministorRight = False

        Dim identity = WindowsIdentity.GetCurrent()
        If (IsNothing(identity)) Then
            Throw New InvalidOperationException("Couldn't get the current user identity")
        End If

        Dim principal = New WindowsPrincipal(identity)

        ' Check if this user has the Administrator role. If they do, return immediately.
        ' If UAC is on, and the process is not elevated, then this will actually return false.
        If (principal.IsInRole(WindowsBuiltInRole.Administrator)) Then
            Return True
        End If
        ' If we're not running in Vista onwards, we don't have to worry about checking for UAC.
        If (Environment.OSVersion.Platform <> PlatformID.Win32NT Or Environment.OSVersion.Version.Major < 6) Then

            ' Operating system does not support UAC; skipping elevation check.
            Return False
        End If

    End Function

    Public Shared Function GetUacElevationType() As TokenElevationType  'Return UAC (User Account Control) information
        GetUacElevationType = TokenElevationType.TokenElevationTypeUnknown

        Dim identity = WindowsIdentity.GetCurrent()
        Dim tokenInfLength As Int32 = Marshal.SizeOf(GetType(Int32))
        Dim tokenInformation As IntPtr = Marshal.AllocHGlobal(tokenInfLength)

        Try

            Dim token = identity.Token
            Dim result = GetTokenInformation(token, TokenInformationClass.TokenElevationType, tokenInformation, tokenInfLength, tokenInfLength)

            If (Not (result)) Then
                Dim Exception = Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error())
                Throw New InvalidOperationException("Couldn't get token information", Exception)
            End If

            'Dim elevationType As TokenElevationType = (TokenElevationType)Marshal.ReadInt32(tokenInformation)\
            Dim elevationType As TokenElevationType = Marshal.ReadInt32(tokenInformation)

            Select Case elevationType

                Case TokenElevationType.TokenElevationTypeDefault
                    ' TokenElevationTypeDefault - User is not using a split token, so they cannot elevate.
                    GetUacElevationType = elevationType
                Case TokenElevationType.TokenElevationTypeFull
                    ' TokenElevationTypeFull - User has a split token, and the process is running elevated. Assuming they're an administrator.
                    GetUacElevationType = elevationType
                Case TokenElevationType.TokenElevationTypeLimited
                    ' TokenElevationTypeLimited - User has a split token, but the process is not running elevated. Assuming they're an administrator.
                    GetUacElevationType = elevationType
                Case Else
                    ' Unknown token elevation type.
                    GetUacElevationType = TokenElevationType.TokenElevationTypeUnknown
            End Select
        Catch ex As Exception
            Console.WriteLine(ex.Message)

        Finally

            If (tokenInformation <> IntPtr.Zero) Then
                Marshal.FreeHGlobal(tokenInformation)
            End If
        End Try

    End Function


End Class

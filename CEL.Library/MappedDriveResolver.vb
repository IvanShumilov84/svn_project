Imports System.Management    'System.Management.dll
Imports System.IO
Imports System
Imports System.Windows.Forms

Public Class MappedDriveResolver


    '''/// <summary>Resolves the given path to a full UNC path, or full local drive path.</summary>
    '''/// <param name="pPath"></param>
    '''/// <returns></returns>
    Public Function ResolveToUNC(ByVal pPath As String) As String

        If (pPath.StartsWith("\\")) Then
            Return pPath
        End If

        Dim root As String = ResolveToRootUNC(pPath)

        If (pPath.StartsWith(root)) Then
            Return pPath ' Local drive, no resolving occurred
        Else
            Return pPath.Replace(GetDriveLetter(pPath), root)
        End If

    End Function

    '''/// <summary>Resolves the given path to a root UNC path, or root local drive path.</summary>
    '''/// <param name="pPath"></param>
    '''/// <returns>\\server\share OR C:\</returns>
    Public Function ResolveToRootUNC(ByVal pPath As String) As String
        Dim mo As New ManagementObject()

        If (pPath.StartsWith("\")) Then
            Return Directory.GetDirectoryRoot(pPath)
        End If

        ' Get just the drive letter for WMI call
        Dim driveletter As String = GetDriveLetter(pPath)

        mo.Path = New ManagementPath(String.Format("Win32_LogicalDisk=""{0}""", driveletter))

        ' Get the data we need
        Dim DriveType As UInteger
        Dim NetworkRoot As String = ""
        Try
            DriveType = System.Convert.ToUInt32(mo("DriveType"))
            NetworkRoot = System.Convert.ToString(mo("ProviderName"))
        Catch ex As Exception
            'Exception est levee si les securites UAC (User account security) de windows sont actives.
            MessageBox.Show("Cannot access Network Drive: Disable UAC (User Account Security)")
        End Try
        mo = Nothing

        ' Return the root UNC path if network drive, otherwise return the root path to the local drive
        If (DriveType = 4) Then
            Return NetworkRoot

        Else
            Return driveletter + Path.DirectorySeparatorChar
        End If

    End Function

    '''/// <summary>Checks if the given path is on a network drive.</summary>
    '''/// <param name="pPath"></param>
    '''/// <returns></returns>
    Public Function IsNetworkDrive(ByVal pPath As String) As Boolean
        Dim mo As New ManagementObject()

        If (pPath.StartsWith("\")) Then
            Return True
        End If

        ' Get just the drive letter for WMI call
        Dim driveletter As String = GetDriveLetter(pPath)

        mo.Path = New ManagementPath(String.Format("Win32_LogicalDisk=""{0}""", driveletter)) 'vb

        ' Get the data we need
        Dim DriveType As UInteger
        Try

            DriveType = System.Convert.ToUInt32(mo("DriveType"))

        Catch ex As Exception
            'Exception est levee si les securites UAC (User account security) de windows sont actives.
            MessageBox.Show("Cannot access Network Drive: Disable UAC (User Account Security)")
        End Try
        mo = Nothing

        Return (DriveType = 4)

    End Function

    '''/// <summary>Given a path will extract just the drive letter with volume separator.</summary>
    '''/// <param name="pPath"></param>
    '''/// <returns>C:</returns>
    Public Function GetDriveLetter(ByVal pPath As String) As String
        If (pPath.StartsWith("\")) Then
            Throw New ArgumentException("A UNC path was passed to GetDriveLetter")
        End If
        Return Directory.GetDirectoryRoot(pPath).Replace(Path.DirectorySeparatorChar.ToString(), "")
    End Function



End Class


Imports System
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.Runtime.InteropServices



Public Class ProcessHelper

    <System.Runtime.InteropServices.DllImport("User32.dll")> _
    Private Shared Function SetForegroundWindow(handle As IntPtr) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("User32.dll")> _
    Private Shared Function ShowWindow(handle As IntPtr, nCmdShow As Integer) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("User32.dll")> _
    Private Shared Function IsIconic(handle As IntPtr) As Boolean
    End Function

    Const SW_RESTORE As Integer = 9


    Public Shared Function CheckIfRunning(ByVal processName As String) As Boolean
        Dim p() As Process
        p = Process.GetProcessesByName(processName)
        If p.Length > 0 Then
            Return True ' Process is running
        Else
            Return False ' Process is not running
        End If
    End Function

    Public Shared Function BringToFront(ByVal processName As String) As Boolean

        BringToFront = False

        Try

            If CheckIfRunning(processName) Then

                Dim p() As Process
                p = Process.GetProcessesByName(processName)

                Dim handle As IntPtr = p(0).MainWindowHandle

                If IsIconic(handle) Then
                    ShowWindow(handle, SW_RESTORE)
                End If

                SetForegroundWindow(handle)

                BringToFront = True
            End If
        Catch ex As Exception
        End Try

    End Function

    ''' <summary>
    ''' Determines whether or not the operating system is 64 bit
    ''' </summary>

    Public ReadOnly Property Is64BitOperatingSystem() As Boolean

        Get
            If IntPtr.Size = 8 Then
                Return True '64 Bits
            Else
                Dim Is64 As Boolean = False
                If DoesWin32MethodExist("kernel32.dll", "IsWow64Process") Then
                    IsWow64Process(Process.GetCurrentProcess.Handle, Is64)
                End If

                Return Is64
            End If
        End Get

    End Property
    Public ReadOnly Property Is64BitProcess() As Boolean

        Get
            If IntPtr.Size = 8 Then
                Return True '64 Bits
            ElseIf IntPtr.Size = 4 Then
                Return False '32 bits
            Else
                Return False
                Debug.Assert(False, "Invalid Is64BitProcess")
            End If
        End Get

    End Property
    'API Definitions



    ''' <summary>
    ''' Determines if a process is running on a 64 bit Operating System but in 32 bit emulation mode (WOW64)
    ''' </summary>
    ''' <param name="hProcess">A handle to the process to check</param>
    ''' <param name="Wow64Process">Output parameter. A boolean that will be set to True if the process is running in WOW64 mode</param>
    <System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint:="IsWow64Process")> _
    Public Shared Function IsWow64Process(<System.Runtime.InteropServices.InAttribute()> ByVal hProcess As System.IntPtr, <System.Runtime.InteropServices.OutAttribute()> ByRef Wow64Process As Boolean) As <System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)> Boolean
    End Function

    ''' <summary>
    ''' Determines if a method exists in the specified DLL
    ''' </summary>
    ''' <param name="moduleName">The DLL to look for the method in</param>
    ''' <param name="methodName">The method to look for</param>
    Public Shared Function DoesWin32MethodExist(ByVal moduleName As String, ByVal methodName As String) As Boolean
        Dim moduleHandle As IntPtr = GetModuleHandle(moduleName)
        If (moduleHandle = IntPtr.Zero) Then
            Return False
        End If

        Return (GetProcAddress(moduleHandle, methodName) <> IntPtr.Zero)

    End Function

    ''' <summary>
    ''' Gets a handle to a specified DLL
    ''' </summary>
    ''' <param name="moduleName">The module to return a handle for</param>

    <Runtime.ConstrainedExecution.ReliabilityContract(Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, Runtime.ConstrainedExecution.Cer.MayFail), DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function GetModuleHandle(ByVal moduleName As String) As IntPtr
    End Function

    ''' <summary>
    ''' Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL)
    ''' </summary>
    ''' <param name="hModule">A handle to the DLL to look for the method in</param>
    ''' <param name="methodName">The method to look for</param>

    <DllImport("kernel32.dll", CharSet:=CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)> _
    Public Shared Function GetProcAddress(ByVal hModule As IntPtr, ByVal methodName As String) As IntPtr
    End Function



End Class

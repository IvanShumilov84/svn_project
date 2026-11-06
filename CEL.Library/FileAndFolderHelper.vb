Imports System.IO
Imports System

Public Class FileAndFolderHelper
    Public Shared Function FileAlreadyInUse(ByVal filename As String) As Boolean
        Dim fs As System.IO.FileStream

        Try

            ' if Open() succeeds, then we know the file is not currently in use
            fs = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None)
            fs.Close()
            Return False

        Catch ex As Exception
            Return True

        End Try
    End Function
End Class

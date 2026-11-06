Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports System.Exception
Imports System.Collections.Generic




Public Class DatabaseHelper

#Region " Connection "

    Public Overloads Shared Function OpenConnection(ByVal ConnectionString As String, ByRef Connection As ADODB.Connection, Optional ByVal PopupError As Boolean = True) As Boolean
        OpenConnection = False
        Try
            If Not IsNothing(Connection) Then
                If Connection.State = 1 Then Return True 'Already Open
            End If
            CloseConnection(Connection)
            Connection = New ADODB.Connection()
            Connection.Open(ConnectionString)
            OpenConnection = True
        Catch ex As Exception
            If PopupError Then
                MessageBox.Show("Error: Unable to open database. " & ex.ToString(), "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Try
    End Function
    Public Overloads Shared Function OpenConnection(ByVal ConnectionString As String, ByRef Connection As OleDbConnection, Optional ByVal PopupError As Boolean = True) As Boolean
        OpenConnection = False
        Try
            If Not IsNothing(Connection) Then
                If Connection.State = 1 Then Return True 'Already Open
            End If
            CloseConnection(Connection)
            Connection = New OleDbConnection(ConnectionString)
            Connection.Open()
            OpenConnection = True
        Catch ex As Exception
            If PopupError Then
                MessageBox.Show("Error: Unable to open database. " & ex.ToString(), "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Try
    End Function
    Public Overloads Shared Function CloseConnection(ByRef Connection As ADODB.Connection) As Boolean
        CloseConnection = False
        Try
            If Not Connection Is Nothing Then
                If Connection.State = ADODB.ObjectStateEnum.adStateOpen Then
                    Connection.Close()
                End If
            End If
            CloseConnection = True

        Catch
        Finally
            Connection = Nothing
        End Try
    End Function
    Public Overloads Shared Function CloseConnection(ByRef Connection As OleDbConnection) As Boolean
        CloseConnection = False
        Try
            If Not Connection Is Nothing Then
                If Connection.State = ConnectionState.Open Then
                    Connection.Close()
                End If
            End If
            CloseConnection = True
        Catch
        Finally
            Connection = Nothing
        End Try
    End Function


#End Region

#Region " Recordset "

    Public Shared Function OpenRecordset(ByRef rs As ADODB.Recordset, ByVal strSQL As String, ByVal Connection As ADODB.Connection, Optional ByVal rsReadOnly As Boolean = True, Optional ByVal Cursor As ADODB.CursorTypeEnum = ADODB.CursorTypeEnum.adOpenUnspecified) As Boolean
        CloseRecordset(rs)
        rs = New ADODB.Recordset

        Try
            If rsReadOnly Then
                If Cursor = ADODB.CursorTypeEnum.adOpenUnspecified Then
                    rs.Open(strSQL, Connection, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
                Else
                    rs.Open(strSQL, Connection, Cursor, ADODB.LockTypeEnum.adLockReadOnly)
                End If

            Else
                rs.Open(strSQL, Connection, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            End If
            OpenRecordset = Not rs.EOF
        Catch e As Exception
            OpenRecordset = False
        End Try
    End Function
    Public Shared Function GetValue(ByVal strSQL As String, ByVal Connection As ADODB.Connection) As Object
        Dim rsTemp As New ADODB.Recordset
        Try
            rsTemp.Open(strSQL, Connection, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
            If Not rsTemp.EOF Then
                If Not rsTemp.Fields(0).Value Is DBNull.Value Then
                    GetValue = rsTemp.Fields(0).Value
                Else
                    GetValue = ""
                End If
            Else
                GetValue = ""
            End If
            CloseRecordset(rsTemp)
        Catch e As Exception
            GetValue = ""
        End Try
    End Function
    Public Shared Function IsExist(ByVal strSQL As String, ByVal Connection As ADODB.Connection) As Boolean
        Dim rsTemp As New ADODB.Recordset
        Try
            rsTemp.Open(strSQL, Connection, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)

            If rsTemp.EOF Then
                IsExist = False
            Else
                IsExist = True
            End If
            CloseRecordset(rsTemp)
        Catch e As Exception
            IsExist = False
        End Try
    End Function
    Public Shared Function ExecuteSQL(ByVal strSQL As String, ByVal Connection As ADODB.Connection) As Boolean
        Dim RecordAffected As Object = Nothing
        Try
            Connection.Execute(strSQL, RecordAffected)
            If DirectCast(RecordAffected, Integer) = 0 Then
                ExecuteSQL = False
            Else
                ExecuteSQL = True
            End If
        Catch e As Exception
            ExecuteSQL = False
            MsgBox("ExecuteSQL Error:" & e.Message)
        End Try
    End Function
    Public Shared Function CloseRecordset(ByRef rs As ADODB.Recordset) As Boolean
        CloseRecordset = False
        Try
            If Not rs Is Nothing Then
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then
                    rs.Close()
                End If
            End If
            CloseRecordset = True
        Catch
        Finally
            rs = Nothing
        End Try
    End Function

    'Public Function RecordCount(Optional ByVal strSQL As String = "", Optional ByVal Connection As ADODB.Connection = Nothing, Optional ByVal rs As ADODB.Recordset = Nothing) As Int64
    Public Shared Function FindRecord(ByRef rs As ADODB.Recordset, ByVal FieldName As String, ByVal Value As Object) As Boolean
        rs.MoveFirst()
        Do While Not rs.EOF
            If rs.Fields(FieldName).Value Is Value Then
                Return True
            End If
            rs.MoveNext()
        Loop
        Return False
    End Function
    Public Shared Function RecordCount(ByVal rs As ADODB.Recordset) As Int64
        RecordCount = -1
        rs.MoveFirst()
        Do While Not rs.EOF
            RecordCount += 1
            rs.MoveNext()
        Loop

        If RecordCount <> -1 Then RecordCount += 1

        rs.MoveFirst()
    End Function


#End Region

    Public Function ValidateDatabaseDriver() As Boolean

        Dim processHelper As New ProcessHelper()

        If processHelper.Is64BitProcess() Then

            Dim reader As System.Data.OleDb.OleDbDataReader = OleDbEnumerator.GetRootEnumerator()

            Dim lst As List(Of String) = New List(Of String)

            Do While reader.Read()
                For i As Integer = 0 To reader.FieldCount - 1
                    If reader.GetName(i) = "SOURCES_NAME" Then
                        lst.Add(reader.GetValue(i).ToString())
                    End If
                Next i
                Console.WriteLine("{0} = {1}", reader.GetName(0), reader.GetValue(0))
            Loop
            reader.Close()

            If lst.Contains("Microsoft.ACE.OLEDB.12.0") Or lst.Contains("Microsoft.ACE.OLEDB.15.0") Or lst.Contains("Microsoft.ACE.OLEDB.16.0") Then
                Return True
            Else
                MessageBox.Show("To run properly CELDAS 200 on Windows 64 bits you must install database driver Microsoft.ACE.OLEDB.12.0 or higher", "Invalid database driver", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Else
            Return True
        End If

    End Function
End Class


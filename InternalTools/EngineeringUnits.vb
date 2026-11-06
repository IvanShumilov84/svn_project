Imports System.Data.OleDb
Imports CEL.Library

Public Class UnitsHelper

    Public Shared Function RetrieveEngineeringUnits(connectionString As String) As IList(Of DBUnit)
        Dim units As IList(Of DBUnit) = New List(Of DBUnit)

        Dim SelectString As String
        Dim cn As OleDbConnection = Nothing

        Try
            SelectString = "SELECT ID, DisplayName, Name FROM engineeringunit"


            If DatabaseHelper.OpenConnection(connectionString, cn) Then
                Dim cmd As OleDbCommand = New OleDbCommand(SelectString, cn)
                Dim reader As OleDbDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    units.Add(New DBUnit(reader.Item("ID"), reader.Item("DisplayName").ToString(), reader.Item("Name").ToString()))
                End While

                reader.Close()
            End If

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            DatabaseHelper.CloseConnection(cn)
        End Try

        Return units
    End Function

End Class

Public Class DBUnit

    Private _ID As Integer
    Protected _DisplayName As String
    Protected _Name As String


    Public Property ID As Integer
        Get
            Return _ID
        End Get
        Private Set(value As Integer)
            _ID = value
        End Set
    End Property

    Public Property DisplayName As String
        Get
            Return _DisplayName
        End Get
        Private Set(value As String)
            _DisplayName = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return _Name
        End Get
        Private Set(value As String)
            _Name = value
        End Set
    End Property

    Public Sub New(id As Integer, displayName As String, name As String)
        Me.ID = id
        Me.DisplayName = displayName
        Me.Name = name
    End Sub

End Class
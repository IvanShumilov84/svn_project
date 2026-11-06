Option Infer On 'Used to LINQ features, this option must be on
Imports CEL.Data
Imports CEL.Data.BusinessObjects

Imports EntitySpaces.Interfaces
Imports EntitySpaces.Loader

Imports System.Linq
Imports System.Configuration
Imports EntitySpaces.Core

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Data


Public Class Form3

    Dim engineQuestions As New UUTQuestionCollection

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'DataGridView1.DataSource = test
        Dim connectionName As String = "ACCESSDynamic"

        'Dim enginesList As New BusinessObjects.UutCollection
        'enginesList.es.Connection.Name = connectionName
        'DataGridView1.AutoGenerateColumns = True
        'enginesList.LoadAll()
        'UUTQuestionDataGridView.AutoGenerateColumns = False

        engineQuestions.es.Connection.Name = connectionName
        'engine.Query.Where(engine.Query.Model = "GTCP131-9A")
        engineQuestions.LoadAll()
        'DataGridView1.DataSource = engine(0).GetCollection
        'DataGridView1.DataSource = engine

        Dim table As DataTable = CreateDataTable(engineQuestions, engineQuestions(0))
        Dim piv As New Pivot(table)
        Dim dt = piv.PivotData("QuestionID", "Question", Pivot.AggregateFunction.First, "Model")

        UUTQuestionBindingSource.DataSource = dt
        UUTQuestionDataGridView.DataSource = dt.DefaultView

        'Dim ds As BindingSource = New BindingSource()
        'ds.DataSource = dt
        'UUTQuestionBindingSource.DataSource = dt.DefaultView
        'UUTQuestionDataGridView.DataSource = ds
        'UUTQuestionDataGridView.AutoGenerateColumns = True
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        '' Add any initialization after the InitializeComponent() call.
        esProviderFactory.Factory = _
            New EntitySpaces.LoaderMT.esDataProviderFactory()
    End Sub


    Public Function CreateDataTable(coll As esEntityCollectionBase, entity As esEntity) As DataTable

        Dim table As New DataTable()
        Dim dataColumns As DataColumnCollection = table.Columns
        Dim trueColumns As New List(Of String)()

        ' Stnadard Columns
        For Each esCol As esColumnMetadata In coll.es.Meta.Columns
            ' If entity is null then the collection is empty we need to create
            ' columns however, othewise we create only the columns that were returned
            ' by the query or procedure
            If entity Is Nothing OrElse entity.ContainsColumn(esCol.Name) Then
                dataColumns.Add(New DataColumn(esCol.Name, esCol.Type))
                ' We're going to use this later
                trueColumns.Add(esCol.Name)
            End If
        Next

        ' Extra Columns

        If entity IsNot Nothing Then
            Dim extra As Dictionary(Of String, Object) = entity.GetExtraColumns()
            For Each columnName As String In extra.Keys
                dataColumns.Add(New DataColumn(columnName, extra(columnName).[GetType]()))
            Next
        End If

        For Each ent As esEntity In coll
            Dim row As DataRow = table.NewRow()
            table.Rows.Add(row)

            For Each columnName As String In trueColumns
                If ent.ContainsColumn(columnName) Then
                    Dim value As Object = ent.GetColumn(columnName)
                    row(columnName) = If(value Is Nothing, DBNull.Value, value)
                End If
            Next

            Dim extra As Dictionary(Of String, Object) = ent.GetExtraColumns()
            For Each columnName As String In extra.Keys
                Dim value As Object = extra(columnName)
                row(columnName) = If(value Is Nothing, DBNull.Value, value)
            Next
        Next

        Return table

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UUTQuestionDataGridView.EndEdit()
        'test.Save()
        Try
            engineQuestions.Save()
            engineQuestions.AcceptChanges()
            engineQuestions.LoadAll()
        Catch
            engineQuestions.RejectChanges()
        End Try
    End Sub
End Class


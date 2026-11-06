Imports System.Data

Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Text

Imports CEL.Data.BusinessObjects
Imports CEL.HMI


Public Class LogReportCategoriesForm



    Private m_CELProject As CELDAS
    Dim logReportCategories As New LogReportCategoriesCollection

    'For Drag Drop usage
    Private fromIndex As Integer
    Private dragIndex As Integer
    Private dragRect As Rectangle




    Public Sub New(ByRef celProject As CELDAS)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_CELProject = celProject

        btnSave.Enabled = False
        btnDelete.Enabled = False
        dgvCategories.ReadOnly = True
        btnMoveDown.Enabled = False
        btnMoveUp.Enabled = False
        btnMoveFirst.Enabled = False
        btnMoveLast.Enabled = False

        Select Case m_CELProject.CurrentUser.SecurityLevelEnum
            Case SecurityLevel.eSecurityLevel.Operator

            Case SecurityLevel.eSecurityLevel.Technician, SecurityLevel.eSecurityLevel.TestManager
                btnSave.Enabled = True
                btnDelete.Enabled = True
                dgvCategories.ReadOnly = False
                btnMoveDown.Enabled = True
                btnMoveUp.Enabled = True
                btnMoveFirst.Enabled = True
                btnMoveLast.Enabled = True

        End Select
        dgvCategories.ForeColor = Color.Black
    End Sub



    Private Sub LogReportCategoriesForm_Load(sender As Object, e As EventArgs) Handles Me.Load

        LoadDataGrid()

    End Sub

    Private Sub LoadDataGrid()


        logReportCategories.es.Connection.Name = m_CELProject.DefaultConnectionName
        logReportCategories.Query.Where(logReportCategories.Query.ProjectID = m_CELProject.Project.Number)
        logReportCategories.Query.Where(logReportCategories.Query.EngineModel = m_CELProject.EngineModel)
        logReportCategories.Query.OrderBy(logReportCategories.Query.Order.Ascending)
        logReportCategories.Query.Load()

        For Each category As LogReportCategories In logReportCategories
            dgvCategories.Rows.Add(category.Categories)
        Next

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Save(False, m_CELProject.Project.Number, m_CELProject.EngineModel)

    End Sub
    Private Sub Save(ByVal Duplicate As Boolean, ByVal projectID As Integer, ByVal engineModel As String)
        If btnSave.Enabled Then
            dgvCategories.EndEdit()

            Using noTrans As New esTransactionScope()
                If Duplicate = False Then
                    logReportCategories.MarkAllAsDeleted()
                    logReportCategories.Save()
                End If
                Try
                    Dim i As Integer = 0
                    For Each row As DataGridViewRow In dgvCategories.Rows

                        If Not IsNothing(row.Cells("colCategory").Value) Then
                            Dim newRow As LogReportCategories = logReportCategories.AddNew()
                            i += 1
                            newRow.ProjectID = projectID
                            newRow.EngineModel = engineModel
                            newRow.Categories = row.Cells("colCategory").Value
                            newRow.Order = i
                        End If

                    Next
                    logReportCategories.Save()
                    logReportCategories.AcceptChanges()

                Catch
                    logReportCategories.RejectChanges()
                End Try

                noTrans.Complete()
            End Using
        End If
    End Sub


    Private Sub btnMoveFirst_Click(sender As Object, e As EventArgs) Handles btnMoveFirst.Click
        Dim grid As DataGridView = dgvCategories
        Try
            Dim totalRows As Integer = grid.Rows.Count
            Dim idx As Integer = grid.SelectedCells(0).OwningRow.Index
            If idx = 0 Then
                Return
            End If
            Dim col As Integer = grid.SelectedCells(0).OwningColumn.Index
            Dim rows As DataGridViewRowCollection = grid.Rows
            Dim row As DataGridViewRow = rows(idx)
            rows.Remove(row)

            rows.Insert(0, row)
            grid.ClearSelection()

            grid.Rows(0).Cells(col).Selected = True
        Catch
        End Try
    End Sub

    Private Sub btnMoveUp_Click(sender As Object, e As EventArgs) Handles btnMoveUp.Click
        Dim grid As DataGridView = dgvCategories
        Try
            Dim totalRows As Integer = grid.Rows.Count
            Dim idx As Integer = grid.SelectedCells(0).OwningRow.Index
            If idx = 0 Then
                Return
            End If
            Dim col As Integer = grid.SelectedCells(0).OwningColumn.Index
            Dim rows As DataGridViewRowCollection = grid.Rows
            Dim row As DataGridViewRow = rows(idx)
            rows.Remove(row)

            rows.Insert(idx - 1, row)
            grid.ClearSelection()

            grid.Rows(idx - 1).Cells(col).Selected = True
        Catch
        End Try
    End Sub

    Private Sub btnMoveDown_Click(sender As Object, e As EventArgs) Handles btnMoveDown.Click
        Dim grid As DataGridView = dgvCategories
        Try
            Dim totalRows As Integer = grid.Rows.Count
            Dim idx As Integer = grid.SelectedCells(0).OwningRow.Index
            If idx = totalRows - 2 Then
                Return
            End If
            Dim col As Integer = grid.SelectedCells(0).OwningColumn.Index
            Dim rows As DataGridViewRowCollection = grid.Rows
            Dim row As DataGridViewRow = rows(idx)
            rows.Remove(row)
            rows.Insert(idx + 1, row)
            grid.ClearSelection()
            grid.Rows(idx + 1).Cells(col).Selected = True
        Catch
        End Try
    End Sub

    Private Sub btnMoveLast_Click(sender As Object, e As EventArgs) Handles btnMoveLast.Click
        Dim grid As DataGridView = dgvCategories
        Try
            Dim totalRows As Integer = grid.Rows.Count
            Dim idx As Integer = grid.SelectedCells(0).OwningRow.Index
            If idx = totalRows - 2 Then
                Return
            End If
            Dim col As Integer = grid.SelectedCells(0).OwningColumn.Index
            Dim rows As DataGridViewRowCollection = grid.Rows
            Dim row As DataGridViewRow = rows(idx)
            rows.Remove(row)

            rows.Insert(totalRows - 2, row)
            grid.ClearSelection()

            grid.Rows(totalRows - 2).Cells(col).Selected = True
        Catch
        End Try
    End Sub


    Private Sub dgvCategories_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles dgvCategories.DragDrop

        Dim p As Point = dgvCategories.PointToClient(New Point(e.X, e.Y))
        dragIndex = dgvCategories.HitTest(p.X, p.Y).RowIndex
        If (e.Effect = DragDropEffects.Move) Then
            Dim dragRow As DataGridViewRow = e.Data.GetData(GetType(DataGridViewRow))
            dgvCategories.Rows.RemoveAt(fromIndex)
            dgvCategories.Rows.Insert(dragIndex, dragRow)
        End If
    End Sub

    Private Sub dgvCategories_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles dgvCategories.DragOver

        e.Effect = DragDropEffects.Move
    End Sub


    Private Sub dgvCategories_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvCategories.MouseDown

        fromIndex = dgvCategories.HitTest(e.X, e.Y).RowIndex
        If fromIndex > -1 Then
            Dim dragSize As Size = SystemInformation.DragSize
            dragRect = New Rectangle(New Point(e.X - (dragSize.Width / 2), _
                                               e.Y - (dragSize.Height / 2)), _
                                     dragSize)
        Else
            dragRect = Rectangle.Empty
        End If
    End Sub

    Private Sub dgvCategories_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvCategories.MouseMove

        If (e.Button And MouseButtons.Left) = MouseButtons.Left Then
            If (dragRect <> Rectangle.Empty _
            AndAlso Not dragRect.Contains(e.X, e.Y)) Then
                dgvCategories.DoDragDrop(dgvCategories.Rows(fromIndex), _
                                         DragDropEffects.Move)
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

   

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        For Each row As DataGridViewRow In dgvCategories.SelectedRows
            dgvCategories.Rows.Remove(row)
        Next

    End Sub

   
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Save(True, InputBox("Enter Project Number"), InputBox("Enter Engine Model"))
    End Sub

   
End Class

Imports System.Windows.Forms
Imports CEL.Data.BusinessObjects

Public Class UserEditorForm

    Private ReadOnly m_CELProject As CEL.HMI.CELDAS
    Public Sub New(CELProject As CEL.HMI.CELDAS)
        m_CELProject = CELProject

        ' This call is required by the designer.
        InitializeComponent()


        btnCancel.Enabled = False
        btnSave.Enabled = False
        PasswordDataGridViewTextBoxColumn.Visible = False

        Select Case m_CELProject.CurrentUser.SecurityLevelEnum
            Case SecurityLevel.eSecurityLevel.Operator
                m_UsersCollection.AllowDelete = False
                m_UsersCollection.AllowEdit = False
                m_UsersCollection.AllowNew = False

            Case SecurityLevel.eSecurityLevel.Technician
                m_UsersCollection.AllowDelete = False
                m_UsersCollection.AllowEdit = True
                m_UsersCollection.AllowNew = True
                btnCancel.Enabled = True
                btnSave.Enabled = True

            Case SecurityLevel.eSecurityLevel.TestManager
                m_UsersCollection.AllowDelete = True
                m_UsersCollection.AllowEdit = True
                m_UsersCollection.AllowNew = True
                btnCancel.Enabled = True
                btnSave.Enabled = True
                PasswordDataGridViewTextBoxColumn.Visible = True
            Case Else
                Me.Enabled = False
        End Select

        m_SecurityLevelCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_SecurityLevelCollection.LoadAll()
        m_UsersCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_UsersCollection.Query.Where(m_UsersCollection.Query.ProjectID = m_CELProject.Project.Number)
        m_UsersCollection.Query.Load()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            For Each user As Users In m_UsersCollection
                user.ProjectID = m_CELProject.Project.Number
            Next
            m_UsersCollection.Save()
            m_UsersCollection.AcceptChanges()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        m_UsersCollection.RejectChanges()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub dgvUsers_UserDeletingRow(sender As Object, e As Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgvUsers.UserDeletingRow

        If e.Row.Cells("OperatorIDDataGridViewTextBoxColumn").Value = m_CELProject.Project.Number Then
            System.Windows.Forms.MessageBox.Show("Admin user cannot be deleted", "Admin User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            e.Cancel = True
        Else
            If MessageBox.Show("Are you sure you want to delete this User?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub
 
End Class
Imports EntitySpaces.Interfaces
Imports CEL.HMI
Imports CEL.Data.BusinessObjects
Imports System.Windows.Forms

Public Class LoginForm

    Public PasswordValidated As Boolean = False

    Private ReadOnly m_CELProject As CEL.HMI.CELDAS
    Public Sub New(CELProject As CEL.HMI.CELDAS)

        m_CELProject = CELProject

        ' This call is required by the designer.
        InitializeComponent()

        m_UsersCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_UsersCollection.Query.Where(m_UsersCollection.Query.ProjectID = m_CELProject.Project.Number)
        m_UsersCollection.Query.Load()

    End Sub
    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click

        Dim userColl As UsersCollection = New UsersCollection
        userColl.es.Connection.Name = m_CELProject.DefaultConnectionName
        userColl.Query.Where(userColl.Query.ProjectID = m_CELProject.Project.Number And userColl.Query.OperatorID = cboLogin.SelectedValue)
        If Not cboLogin.SelectedValue Is Nothing Then
            userColl.Query.Load()
        Else
            MessageBox.Show("Invalid UserName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        If userColl.Count = 1 Then
            If txtPassword.Text = userColl.Item(0).Password Then
                m_CELProject.CurrentUser = userColl.Item(0)
                Me.PasswordValidated = True
                Me.Dispose()
            Else
                MessageBox.Show("Invalid password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        'Reload the collection for the form authentification in case of error.
        m_UsersCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_UsersCollection.Query.Where(m_UsersCollection.Query.ProjectID = m_CELProject.Project.Number)
        m_UsersCollection.Query.Load()
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub


End Class
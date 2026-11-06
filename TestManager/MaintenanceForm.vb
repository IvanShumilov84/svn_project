Imports System.Windows.Forms
Imports CEL.Data.BusinessObjects
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Reflection
Imports System.Linq


Public Class MaintenanceForm

    Private ReadOnly m_CELProject As CEL.HMI.CELDAS
    Public Sub New(CELProject As CEL.HMI.CELDAS)
        m_CELProject = CELProject

        ' This call is required by the designer.
        InitializeComponent()

        Dim EngineList As New UutCollection()


        EngineList.Query.Where(EngineList.Query.ProjectID = m_CELProject.Project.Number)
        EngineList.Query.Load()
        cboEngineModel.DataSource = EngineList
        cboEngineModel.DisplayMember = "EngineModel"
        cboEngineModel.ValueMember = "ID"


        m_UutCollection.AllowDelete = False
        m_UutCollection.AllowEdit = False
        m_UutCollection.AllowNew = False

        m_UutChoiceItemsCollection.AllowDelete = False
        m_UutChoiceItemsCollection.AllowEdit = False
        m_UutChoiceItemsCollection.AllowNew = False

        cboSecurityLevel.Enabled = False

        btnSave.Enabled = False
        btnCancel.Enabled = False

        dgvQuestions.ReadOnly = True
        dgvQuestions.AllowUserToAddRows = False
        dgvQuestions.AllowUserToDeleteRows = False

        dgvChoices.ReadOnly = True
        dgvChoices.AllowUserToAddRows = False
        dgvChoices.AllowUserToDeleteRows = False

        dgvChoiceItems.ReadOnly = True
        dgvChoiceItems.AllowUserToAddRows = False
        dgvChoiceItems.AllowUserToDeleteRows = False

        dgvOptions.ReadOnly = True
        dgvOptions.AllowUserToAddRows = False
        dgvOptions.AllowUserToDeleteRows = False

        dgvLimits.ReadOnly = True
        dgvLimits.AllowUserToAddRows = False
        dgvLimits.AllowUserToDeleteRows = False

        dgvCalibrationConstants.ReadOnly = True
        dgvCalibrationConstants.AllowUserToAddRows = False
        dgvCalibrationConstants.AllowUserToDeleteRows = False

        dgvCustomParameters.ReadOnly = True
        dgvCustomParameters.AllowUserToAddRows = False
        dgvCustomParameters.AllowUserToDeleteRows = False

        dgvServiceBulletin.ReadOnly = True
        dgvServiceBulletin.AllowUserToAddRows = False
        dgvServiceBulletin.AllowUserToDeleteRows = False



        Select Case m_CELProject.CurrentUser.SecurityLevelEnum
            Case SecurityLevel.eSecurityLevel.Operator
            Case SecurityLevel.eSecurityLevel.Technician, SecurityLevel.eSecurityLevel.TestManager

                m_UutCollection.AllowDelete = False
                m_UutCollection.AllowEdit = False
                m_UutCollection.AllowNew = False

                m_UutChoiceItemsCollection.AllowDelete = True
                m_UutChoiceItemsCollection.AllowEdit = True
                m_UutChoiceItemsCollection.AllowNew = True

                cboSecurityLevel.Enabled = True

                btnSave.Enabled = True
                btnCancel.Enabled = True

                dgvQuestions.ReadOnly = False
                dgvChoices.ReadOnly = False
                dgvChoiceItems.ReadOnly = False
                dgvChoiceItems.AllowUserToAddRows = True
                dgvChoiceItems.AllowUserToDeleteRows = True
                dgvOptions.ReadOnly = False
                dgvLimits.ReadOnly = False
                dgvCalibrationConstants.ReadOnly = False
                dgvCustomParameters.ReadOnly = False
                dgvServiceBulletin.ReadOnly = False



        End Select
        'QuestionDataGridViewTextBoxColumn.ReadOnly = True
        'QuestionDataGridViewTextBoxColumn.ReadOnly = True
        'DataTypeDataGridViewTextBoxColumn.ReadOnly = True
        'DataTypeDataGridViewTextBoxColumn1.ReadOnly = True
        'OptionIDDataGridViewTextBoxColumn.ReadOnly = True
        'OptionTitleDataGridViewTextBoxColumn.ReadOnly = True
        'ChoiceIDDataGridViewTextBoxColumn.ReadOnly = True
        'ChoiceTitleDataGridViewTextBoxColumn.ReadOnly = True
        'ChoiceStyleDataGridViewTextBoxColumn.ReadOnly = True
        'ParameterNameDataGridViewTextBoxColumn.ReadOnly = True
        m_UutCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_SecurityLevelCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_SecurityLevelCollection.LoadAll()
        m_UutTripTypeCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_UutTripTypeCollection.LoadAll()

        m_UutChoiceCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_UutChoiceCollection.LoadAll()


        ChoiceItemsProjectIDDataGridViewTextBoxColumn.ReadOnly = True
        ChoiceItemsProjectIDDataGridViewTextBoxColumn.Visible = False
        ChoiceItemsEngineModelDataGridViewTextBoxColumn.ReadOnly = True
        ChoiceItemsEngineModelDataGridViewTextBoxColumn.Visible = False

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            m_UutCollection.Save()
            m_UutCollection.AcceptChangesGraph()

            m_UutChoiceItemsCollection.Save()
            m_UutChoiceItemsCollection.AcceptChangesGraph()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        m_UutCollection.RejectChangesGraph()
        m_UutChoiceItemsCollection.RejectChangesGraph()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub cboEngineModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEngineModel.SelectedIndexChanged

        RefreshDataSource()

    End Sub

    Private Sub RefreshDataSource()
        If Not IsNothing(cboEngineModel.SelectedValue) Then

            Dim query As New UutQuery
            Dim choiceQuery As New UUTChoiceQuery
            Dim choiceItemsQuery As New UUTChoiceItemsQuery

            Dim operatorChoiceID? As Integer
            Dim inspectorChoiceID? As Integer

            'UUT Query
            query.Where(query.ProjectID = m_CELProject.Project.Number)
            query.Where(query.Id = cboEngineModel.SelectedValue)
            m_UutCollection.Load(query)

            'Choice Query (This is only used to get the ID associated with operator and inspector fields in the choice table
            choiceQuery.Where(choiceQuery.ProjectID = m_CELProject.Project.Number)
            choiceQuery.Where(choiceQuery.EngineModel = TryCast(cboEngineModel.SelectedItem, Uut).EngineModel)
            m_UutChoiceCollection.Load(choiceQuery)

            For i = 0 To (m_UutChoiceCollection.Count - 1)
                If m_UutChoiceCollection.Item(i).ChoiceTitle = "Operator" Then
                    operatorChoiceID = m_UutChoiceCollection.Item(i).ChoiceID
                ElseIf m_UutChoiceCollection.Item(i).ChoiceTitle = "Inspector" Then
                    inspectorChoiceID = m_UutChoiceCollection.Item(i).ChoiceID
                End If
            Next

            'Choice Items Query (Only fetch the rows containing operators or inspectors)
            choiceItemsQuery.Where(choiceItemsQuery.ProjectID = m_CELProject.Project.Number)
            choiceItemsQuery.Where(choiceItemsQuery.EngineModel = TryCast(cboEngineModel.SelectedItem, Uut).EngineModel)

            If operatorChoiceID.HasValue And inspectorChoiceID.HasValue Then
                choiceItemsQuery.Where(choiceItemsQuery.ChoiceID = operatorChoiceID Or choiceItemsQuery.ChoiceID = inspectorChoiceID)
                m_UutChoiceItemsCollection.Load(choiceItemsQuery)
            ElseIf operatorChoiceID.HasValue Then
                choiceItemsQuery.Where(choiceItemsQuery.ChoiceID = operatorChoiceID)
                m_UutChoiceItemsCollection.Load(choiceItemsQuery)
            ElseIf inspectorChoiceID.HasValue Then
                choiceItemsQuery.Where(choiceItemsQuery.ChoiceID = inspectorChoiceID)
                m_UutChoiceItemsCollection.Load(choiceItemsQuery)
            End If

        End If

    End Sub

    Private Sub dgvChoiceItems_OnNewRow(ByVal sender As Object, ByVal dgvEventChoiceItems As System.Windows.Forms.DataGridViewRowEventArgs) Handles dgvChoiceItems.DefaultValuesNeeded

        With dgvEventChoiceItems.Row
            .Cells("ChoiceItemsProjectIDDataGridViewTextBoxColumn").Value = m_CELProject.Project.Number
            .Cells("ChoiceItemsEngineModelDataGridViewTextBoxColumn").Value = TryCast(cboEngineModel.SelectedItem, Uut).EngineModel
        End With
    End Sub

End Class
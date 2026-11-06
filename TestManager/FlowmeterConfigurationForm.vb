Imports CEL.Data.BusinessObjects
Imports EntitySpaces.DynamicQuery
Imports System.Windows.Forms
Imports System.Data
Imports EntitySpaces.Interfaces


Public Class FlowmeterConfigurationForm


    Private ReadOnly m_CELProject As CEL.HMI.CELDAS
    Public Sub New(CELProject As CEL.HMI.CELDAS)
        m_CELProject = CELProject

        ' This call is required by the designer.
        InitializeComponent()

        btnSave.Enabled = False
        btnCancel.Enabled = False

        m_FlowmeterCollection.AllowDelete = False
        m_FlowmeterCollection.AllowEdit = False
        m_FlowmeterCollection.AllowNew = False

        m_FlowmeterMapCollection.AllowDelete = False
        m_FlowmeterMapCollection.AllowEdit = False
        m_FlowmeterMapCollection.AllowNew = False

        m_FlowmeterMapCollection.AllowDelete = False
        m_FlowmeterMapCollection.AllowEdit = False
        m_FlowmeterMapCollection.AllowNew = False

        cboMappingName.Enabled = False
        cboCollectionName.Enabled = False
        cboFlometerMappingName.Enabled = False
        cboMappedToFlowmeter.Enabled = False
        dgvMapping.Enabled = False
        dgvFlowmeterMaster.Enabled = False
        dgvFlowmeterDetail.ReadOnly = True

        Select Case m_CELProject.CurrentUser.SecurityLevelEnum
            Case SecurityLevel.eSecurityLevel.Operator
            Case SecurityLevel.eSecurityLevel.Technician, SecurityLevel.eSecurityLevel.TestManager
                btnSave.Enabled = True
                btnCancel.Enabled = True

                m_FlowmeterCollection.AllowDelete = True
                m_FlowmeterCollection.AllowEdit = True
                m_FlowmeterCollection.AllowNew = True

                m_FlowmeterMapCollection.AllowDelete = True
                m_FlowmeterMapCollection.AllowEdit = True
                m_FlowmeterMapCollection.AllowNew = True

                m_FlowmeterMapCollection.AllowDelete = True
                m_FlowmeterMapCollection.AllowEdit = True
                m_FlowmeterMapCollection.AllowNew = True

                cboMappingName.Enabled = True
                cboCollectionName.Enabled = False
                cboFlometerMappingName.Enabled = True
                cboMappedToFlowmeter.Enabled = True
                dgvMapping.Enabled = True
                dgvFlowmeterMaster.Enabled = True
                dgvFlowmeterDetail.ReadOnly = False

        End Select

        m_FlowmeterCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_FlowmeterMapCollection.es.Connection.Name = m_CELProject.DefaultConnectionName

        m_FlowmeterMasterCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_FlowMeterInputUnitCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_FlowMeterOutputUnitCollection.es.Connection.Name = m_CELProject.DefaultConnectionName


        m_FlowMeterInputUnitCollection.LoadAll()
        m_FlowMeterOutputUnitCollection.LoadAll()
        m_FlowmeterMapCollection.LoadAll()
        m_FlowmeterMasterCollection.LoadAll()


    End Sub
    Private Sub FlowmeterConfigurationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshDataSource()
    End Sub
    Private Sub RefreshDataSource()

        Dim dynamicQuery As New FlowmeterQuery()

        dynamicQuery.Where(dynamicQuery.ProjectID = m_CELProject.Project.Number)
        dynamicQuery.OrderBy(dynamicQuery.EngineModel.Ascending)
        m_FlowmeterCollection.Load(dynamicQuery)

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click


        For Each Flowmeter As Flowmeter In m_FlowmeterCollection
            Flowmeter.SaveWithPrimaryKeyUpdate(Of Flowmeter)()
        Next
        m_FlowmeterCollection.Save()

        m_FlowmeterMasterCollection.Save()
        m_FlowmeterMapCollection.Save()
        m_FlowmeterMasterCollection.Save()

        m_FlowmeterCollection.AcceptChanges()

        m_FlowmeterMapCollection.AcceptChanges()
        m_FlowmeterMasterCollection.AcceptChangesGraph()
    End Sub

 
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        m_FlowmeterCollection.RejectChangesGraph()
        m_FlowmeterMasterCollection.RejectChangesGraph()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub dgvFlowmeter_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvFlowmeter.DataError
        'MessageBox.Show("Error happened " + e.Context.ToString())

        If (e.Context = DataGridViewDataErrorContexts.Commit) Then
            MessageBox.Show("Commit error")
        End If
        If (e.Context = DataGridViewDataErrorContexts.CurrentCellChange) Then
            MessageBox.Show("Cell change")
        End If
        If (e.Context = DataGridViewDataErrorContexts.Parsing) Then
            MessageBox.Show("parsing error")
        End If
        If (e.Context = DataGridViewDataErrorContexts.LeaveControl) Then
            MessageBox.Show("leave control error")
        End If
        'If ((e.Exception) Is ConstraintException) Then
        '    Dim grd1 As DataGridView = sender
        '    grd1.Rows(e.RowIndex).ErrorText = "an error"
        '    grd1.Rows(e.RowIndex).Cells(e.ColumnIndex).ErrorText = "an error"

        '    e.ThrowException = False
        'End If
    End Sub


End Class
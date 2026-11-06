Option Strict On
Option Explicit On

Public Class LoadCellAssignationForm

	Private ReadOnly m_CELProject As CEL.HMI.CELDAS
	Private m_LoadCellsCollection1 As New CEL.Data.BusinessObjects.LoadCellsCollection
	Private m_LoadCellsCollection2 As New CEL.Data.BusinessObjects.LoadCellsCollection

	Public Sub New(celProject As CEL.HMI.CELDAS)

		' This call is required by the designer.
		InitializeComponent()

        m_CELProject = celProject

        m_LoadCellsCollection1.AllowDelete = False
        m_LoadCellsCollection1.AllowEdit = False
        m_LoadCellsCollection1.AllowNew = False

        m_LoadCellsCollection2.AllowDelete = False
        m_LoadCellsCollection2.AllowEdit = False
        m_LoadCellsCollection2.AllowNew = False
        m_dgvLoadCell1.ReadOnly = True
        m_dgvLoadCell2.ReadOnly = True

        btnCancel.Enabled = False
        btnSave.Enabled = False

        Select Case m_CELProject.CurrentUser.SecurityLevelEnum
            Case CEL.Data.BusinessObjects.SecurityLevel.eSecurityLevel.Operator
            Case CEL.Data.BusinessObjects.SecurityLevel.eSecurityLevel.Technician, CEL.Data.BusinessObjects.SecurityLevel.eSecurityLevel.TestManager
                m_LoadCellsCollection1.AllowDelete = True
                m_LoadCellsCollection1.AllowEdit = True
                m_LoadCellsCollection1.AllowNew = True

                m_LoadCellsCollection2.AllowDelete = True
                m_LoadCellsCollection2.AllowEdit = True
                m_LoadCellsCollection2.AllowNew = True

                m_dgvLoadCell1.ReadOnly = False
                m_dgvLoadCell2.ReadOnly = False

                btnCancel.Enabled = True
                btnSave.Enabled = True
        End Select


        m_LoadCellsCollection1.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_LoadCellsCollection2.es.Connection.Name = m_CELProject.DefaultConnectionName
	End Sub

	Private Sub LoadVinAssignation()

		'Plug 1
		m_LoadCellsCollection1.Query.Where(m_LoadCellsCollection1.Query.Plug = 1)
		m_LoadCellsCollection1.Query.OrderBy(m_LoadCellsCollection1.Query.Number.Ascending)
		m_LoadCellsCollection1.Query.Load()

		LoadCells1CollectionBindingSource.DataSource = m_LoadCellsCollection1

		'Plug 2
		m_LoadCellsCollection2.Query.Where(m_LoadCellsCollection2.Query.Plug = 2)
		m_LoadCellsCollection2.Query.OrderBy(m_LoadCellsCollection2.Query.Number.Ascending)
		m_LoadCellsCollection2.Query.Load()

		LoadCells2CollectionBindingSource.DataSource = m_LoadCellsCollection2

	End Sub

	Private Sub LoadCellAssignationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		LoadVinAssignation()
	End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If System.Windows.Forms.DialogResult.Yes = System.Windows.Forms.MessageBox.Show("Êtes-vous vraiment certain de vouloir enregistrer les changements?", "Save Data", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) Then
            m_LoadCellsCollection1.Save()
            m_LoadCellsCollection2.Save()
        End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        If System.Windows.Forms.DialogResult.Yes = System.Windows.Forms.MessageBox.Show("Êtes-vous vraiment certain de vouloir annuler les changements?", "Save Data", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) Then
            m_LoadCellsCollection1.RejectChanges()
            m_LoadCellsCollection2.RejectChanges()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
End Class
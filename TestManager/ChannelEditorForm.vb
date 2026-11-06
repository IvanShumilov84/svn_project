Imports CEL.Data.BusinessObjects
Imports EntitySpaces.DynamicQuery
Imports System.Windows.Forms
Imports System.Data

Public Class ChannelEditorForm
    Private ReadOnly m_CELProject As CEL.HMI.CELDAS
    Public Sub New(CELProject As CEL.HMI.CELDAS)
        m_CELProject = CELProject

        ' This call is required by the designer.
        InitializeComponent()


        btnSave.Enabled = False
        btnCancel.Enabled = False
       
        m_HmiBooleanChannelsCollection.AllowEdit = False
        m_HmiBooleanChannelsCollection.AllowDelete = False
        m_HmiBooleanChannelsCollection.AllowNew = False

        m_HmiNumericChannelsCollection.AllowEdit = False
        m_HmiNumericChannelsCollection.AllowDelete = False
        m_HmiNumericChannelsCollection.AllowNew = False

        m_HmiTextChannelsCollection.AllowEdit = False
        m_HmiTextChannelsCollection.AllowDelete = False
        m_HmiTextChannelsCollection.AllowNew = False

        m_HmiChannelsMappingCollection.AllowEdit = False
        m_HmiChannelsMappingCollection.AllowDelete = False
        m_HmiChannelsMappingCollection.AllowNew = False

        navBoolean.Buttons.CancelEdit.Visible = False
        navBoolean.Buttons.EndEdit.Visible = False
        navBoolean.Buttons.Remove.Visible = False
        navBoolean.Buttons.Append.Visible = False

        navNumeric.Buttons.CancelEdit.Visible = False
        navNumeric.Buttons.EndEdit.Visible = False
        navNumeric.Buttons.Remove.Visible = False
        navNumeric.Buttons.Append.Visible = False

        navText.Buttons.CancelEdit.Visible = False
        navText.Buttons.EndEdit.Visible = False
        navText.Buttons.Remove.Visible = False
        navText.Buttons.Append.Visible = False

        navMapping.Buttons.CancelEdit.Visible = False
        navMapping.Buttons.EndEdit.Visible = False
        navMapping.Buttons.Remove.Visible = False
        navMapping.Buttons.Append.Visible = False


        txtBooleanChannelDestination.Enabled = False
        txtBooleanChannelName.Enabled = False
        txtBooleanChannelSource.Enabled = False
        txtBooleanDescription.Enabled = False
        txtBooleanExpression.Enabled = False
        txtBooleanTooltip.Enabled = False
        txtBooleanTrueText.Enabled = False
        txtBooleanFalseText.Enabled = False
        cboCollectionNameBoolean.Enabled = False
        cboBooleanInitialValue.Enabled = False
        cboBooleanFinalValue.Enabled = False
        txtBooleanOpcGroup.Enabled = False
        txtBooleanOpcItemId.Enabled = False

        txtNumericChannelDestination.Enabled = False
        txtNumericChannelName.Enabled = False
        txtNumericChannelSource.Enabled = False
        txtNumericDescription.Enabled = False
        txtNumericExpression.Enabled = False
        txtNumericTooltip.Enabled = False
        txtNumericEngUnit.Enabled = False
        txtNumericFormat.Enabled = False
        txtNumericMinimum.Enabled = False
        txtNumericMaximum.Enabled = False
        txtNumericNbDecimal.Enabled = False
        cboCollectionNameNumeric.Enabled = False
        txtNumericHighTriggerValue.Enabled = False
        txtNumericLowTriggerValue.Enabled = False
        chkNumericHighTriggerEnabled.Enabled = False
        chkNumericLowTriggerEnabled.Enabled = False
        chkNumericPersistent.Enabled = False
        txtNumericOpcGroup.Enabled = False
        txtNumericOpcItemId.Enabled = False
        txtNumericInitialValue.Enabled = False
        txtNumericFinalValue.Enabled = False


        txtHighHighChannelSource.Enabled = False
        txtHighHighColor.Enabled = False
        txtHighHighMessage.Enabled = False
        txtHighHighValue.Enabled = False

        txtHighChannelSource.Enabled = False
        txtHighColor.Enabled = False
        txtHighMessage.Enabled = False
        txtHighValue.Enabled = False

        txtLowChannelSource.Enabled = False
        txtLowColor.Enabled = False
        txtLowMessage.Enabled = False
        txtLowValue.Enabled = False

        txtLowLowChannelSource.Enabled = False
        txtLowLowColor.Enabled = False
        txtLowLowMessage.Enabled = False
        txtLowLowValue.Enabled = False

        txtNormalColor.Enabled = False
        txtNormalValue.Enabled = False

        txtTextChannelDestination.Enabled = False
        txtTextChannelName.Enabled = False
        txtTextChannelSource.Enabled = False
        txtTextDescription.Enabled = False
        txtTextTooltip.Enabled = False
        cboTextCollectionName.Enabled = False

        txtTextChannelDestination.Enabled = False
        txtTextChannelName.Enabled = False
        txtTextChannelSource.Enabled = False
        txtTextDescription.Enabled = False
        txtTextTooltip.Enabled = False
        cboTextCollectionName.Enabled = False
        txtTextOpcGroup.Enabled = False
        txtTextOpcItemId.Enabled = False
        txtTextInitialValue.Enabled = False
        txtTextFinalValue.Enabled = False


        txtMappingChannelName.Enabled = False
        txtMappingNote.Enabled = False
        txtMappingFVName.Enabled = False
        cboMappingCollectionName.Enabled = False

           
        Select Case m_CELProject.CurrentUser.SecurityLevelEnum
            Case SecurityLevel.eSecurityLevel.Operator


            Case SecurityLevel.eSecurityLevel.Technician, SecurityLevel.eSecurityLevel.TestManager

                m_HmiBooleanChannelsCollection.AllowEdit = True
                m_HmiBooleanChannelsCollection.AllowDelete = True
                m_HmiBooleanChannelsCollection.AllowNew = True

                m_HmiNumericChannelsCollection.AllowEdit = True
                m_HmiNumericChannelsCollection.AllowDelete = True
                m_HmiNumericChannelsCollection.AllowNew = True

                m_HmiTextChannelsCollection.AllowEdit = True
                m_HmiTextChannelsCollection.AllowDelete = True
                m_HmiTextChannelsCollection.AllowNew = True

                m_HmiChannelsMappingCollection.AllowEdit = True
                m_HmiChannelsMappingCollection.AllowDelete = True
                m_HmiChannelsMappingCollection.AllowNew = True


                navMapping.Buttons.CancelEdit.Visible = True
                navMapping.Buttons.EndEdit.Visible = True
                navMapping.Buttons.Remove.Visible = False
                navMapping.Buttons.Append.Visible = False

                btnSave.Enabled = True
                btnCancel.Enabled = True

            Case Else
                Me.Enabled = False
        End Select



        m_BooleanStateCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_BooleanStateCollection2.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_EngineeringUnitCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_HmiBooleanChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_HmiNumericChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_HmiTextChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        m_HmiChannelsMappingCollection.es.Connection.Name = m_CELProject.DefaultConnectionName

        Try

            m_BooleanStateCollection.LoadAll()
            m_BooleanStateCollection2.LoadAll()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        m_EngineeringUnitCollection.LoadAll()

        Dim EngineList As New UutCollection()


        EngineList.Query.Where(EngineList.Query.ProjectID = m_CELProject.Project.Number)
        EngineList.Query.Load()
        cboEngineModel.DataSource = EngineList
        cboEngineModel.DisplayMember = "EngineModel"
        cboEngineModel.ValueMember = "ID"

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try


            If m_CELProject.CurrentUser.SecurityLevelEnum > SecurityLevel.eSecurityLevel.Operator Then
                m_HmiBooleanChannelsCollection.Save()
                m_HmiNumericChannelsCollection.Save()
                m_HmiTextChannelsCollection.Save()
                m_HmiChannelsMappingCollection.Save()

                m_HmiBooleanChannelsCollection.AcceptChanges()
                m_HmiNumericChannelsCollection.AcceptChanges()
                m_HmiTextChannelsCollection.AcceptChanges()
                m_HmiChannelsMappingCollection.AcceptChanges()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try


    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        m_HmiBooleanChannelsCollection.RejectChangesGraph()
        m_HmiNumericChannelsCollection.RejectChangesGraph()
        m_HmiTextChannelsCollection.RejectChangesGraph()
        m_HmiChannelsMappingCollection.RejectChanges()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
    Private Sub cboEngineModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEngineModel.SelectedIndexChanged

        m_CELProject.EngineModel = cboEngineModel.Text

        RefreshDataSource()

    End Sub
    Private Sub RefreshDataSource()

        'dgvNumericChannels.AutoGenerateColumns = False
        Dim dynamicQuery As New UUTDynamicChannelCollectionQuery()

        dynamicQuery.Where(dynamicQuery.ProjectID = m_CELProject.Project.Number)
        dynamicQuery.Where(dynamicQuery.EngineModel = cboEngineModel.Text)
        dynamicQuery.OrderBy(dynamicQuery.LoadOrder.Ascending)
        m_UutDynamicChannelCollectionCollection.Load(dynamicQuery)


        If m_UutDynamicChannelCollectionCollection.Count > 0 Then

            Dim boolQuery As New HMIBooleanChannelsQuery()
            Dim numQuery As New HMINumericChannelsQuery()
            Dim textQuery As New HMITextChannelsQuery()
            Dim mapQuery As New HMIChannelsMappingQuery()

            boolQuery.es.DefaultConjunction = esConjunction.Or
            numQuery.es.DefaultConjunction = esConjunction.Or
            textQuery.es.DefaultConjunction = esConjunction.Or
            mapQuery.es.DefaultConjunction = esConjunction.Or
         

            'Load All possible mapping for different collection name in a SPECIFIC ENGINE MODEL
            For Each record As UUTDynamicChannelCollection In m_UutDynamicChannelCollectionCollection

                boolQuery.Where(boolQuery.CollectionName = record.CollectionName)
                numQuery.Where(numQuery.CollectionName = record.CollectionName)
                textQuery.Where(textQuery.CollectionName = record.CollectionName)
                mapQuery.Where(mapQuery.CollectionName = record.CollectionName)

            Next
            m_HmiBooleanChannelsCollection.Load(boolQuery)
            m_HmiNumericChannelsCollection.Load(numQuery)
            m_HmiTextChannelsCollection.Load(textQuery)
            m_HmiChannelsMappingCollection.Load(mapQuery)


        End If

    End Sub
    Private Sub ChannelEditorForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshDataSource()
        UpdateLayout()
    End Sub

    Private Sub dgvBooleanChannels_DataError(sender As Object, e As Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvBooleanChannels.DataError
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

   

    Private Sub dgvNumericChannels_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvNumericChannels.DataError
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

    Private Sub dgvTextChannels_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvTextChannels.DataError
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

    Private Sub dgvChannelsMapping_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvChannelsMapping.DataError
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

    Private Sub btnViewThresholdSection_Click(sender As Object, e As EventArgs) Handles btnViewThresholdSection.Click
        If grpNumericThresholdInformation.Visible = True Then
            grpNumericThresholdInformation.Visible = False
            btnViewThresholdSection.Text = "View Threshold Section"
        Else
            grpNumericThresholdInformation.Visible = True
            btnViewThresholdSection.Text = "Hide Threshold Section"
        End If
        UpdateLayout()

    End Sub

  

    Private Sub UpdateLayout()
        If grpNumericThresholdInformation.Visible = True Then
            grpNumericChannelInformation.Top = navNumeric.Top - grpNumericChannelInformation.Height - grpNumericThresholdInformation.Height
        Else
            grpNumericChannelInformation.Top = navNumeric.Top - grpNumericChannelInformation.Height

        End If
        dgvNumericChannels.Height = grpNumericChannelInformation.Top
    End Sub

End Class
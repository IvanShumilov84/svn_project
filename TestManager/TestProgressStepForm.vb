Option Strict On
Option Explicit On

Public Class TestProgressStepForm

    Private Enum RowMoveDirection
        First = 0
        Up = 1
        Down = 2
        Last = 3
    End Enum

    Private ReadOnly m_CELProject As CEL.HMI.CELDAS
    Private m_checkTypeCollection As CEL.Data.BusinessObjects.TestProgressCheckTypeCollection
    Private m_currentEngineModel As String = String.Empty
    Private m_dicEngUnit As New Collections.Generic.Dictionary(Of Integer, String)
    Private m_engineTestStepCollection As New CEL.Data.BusinessObjects.TestProgressTestStepCollection
    Private m_stepTypeCollection As New CEL.Data.BusinessObjects.TestProgressStepTypeCollection

    Public Sub New(celProject As CEL.HMI.CELDAS)

        ' This call is required by the designer.
        InitializeComponent()

        m_CELProject = celProject

        m_dgvNumericChannels.ReadOnly = True
        m_dgvTestProgressStep.ReadOnly = True
        m_dgvTestProgressTestStepCheckListItems.ReadOnly = True

        Select Case m_CELProject.CurrentUser.SecurityLevelEnum
            Case CEL.Data.BusinessObjects.SecurityLevel.eSecurityLevel.Operator
                m_btnSave.Enabled = False
                m_btnMoveUp.Enabled = False
                m_btnMoveDown.Enabled = False
                m_btnMoveFirst.Enabled = False
                m_btnMoveLast.Enabled = False
                m_btnChannelSelect.Enabled = False

            Case CEL.Data.BusinessObjects.SecurityLevel.eSecurityLevel.Technician, CEL.Data.BusinessObjects.SecurityLevel.eSecurityLevel.TestManager
                m_btnSave.Enabled = True
                m_dgvNumericChannels.ReadOnly = False
                m_dgvTestProgressStep.ReadOnly = False
                m_dgvTestProgressTestStepCheckListItems.ReadOnly = False
        End Select

    End Sub

    Private Sub LoadArincUartChannelCollection(projectNumber As Integer, engineModel As String)
        Using arincChannelsCollection As New CEL.Data.BusinessObjects.ARINC429LabelTemplateCollection()
            arincChannelsCollection.Query.es.QuerySource = "DACN1_ARINC429_LABEL"
            arincChannelsCollection.Query.Where(arincChannelsCollection.Query.EngineModel = engineModel)
            arincChannelsCollection.LoadAll()
            For Each channel As CEL.Data.BusinessObjects.ARINC429LabelTemplate In arincChannelsCollection
                m_dgvNumericChannels.Rows.Add(channel.Name, channel.Description, channel.Units)
            Next
        End Using
        Using omnibusboxChannelsCollection As New CEL.Data.BusinessObjects.OmniBusBoxUartTemplateCollection()
            omnibusboxChannelsCollection.Query.es.QuerySource = "DACN1_OmniBusBox_IOMGR_Uart"
            omnibusboxChannelsCollection.Query.Where(omnibusboxChannelsCollection.Query.EngineModel = engineModel)
            omnibusboxChannelsCollection.LoadAll()
            For Each channel As CEL.Data.BusinessObjects.OmniBusBoxUartTemplate In omnibusboxChannelsCollection
                m_dgvNumericChannels.Rows.Add(channel.Name, channel.Parameter, channel.Units)
            Next
        End Using
    End Sub

    Private Sub LoadCheckTypeCollection()
        m_checkTypeCollection = New CEL.Data.BusinessObjects.TestProgressCheckTypeCollection()
        m_checkTypeCollection.LoadAll()
        TestProgressCheckTypeCollectionBindingSource.DataSource = m_checkTypeCollection
    End Sub

    Private Sub LoadEngineeringUnits()
        m_dicEngUnit.Clear()
        Dim engUnitCollection As New CEL.Data.BusinessObjects.EngineeringUnitCollection()
        engUnitCollection.LoadAll()
        For Each engUnit As CEL.Data.BusinessObjects.EngineeringUnit In engUnitCollection
            m_dicEngUnit.Add(engUnit.Id.Value, engUnit.DisplayName)
        Next
    End Sub
    Private Sub LoadEngineSelection()

        Dim uutCollection As New CEL.Data.BusinessObjects.UutCollection()
        uutCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        uutCollection.Query.Where(uutCollection.Query.ProjectID = m_CELProject.Project.Number)
        uutCollection.Query.OrderBy(uutCollection.Query.EngineModel.Ascending)
        uutCollection.Query.Load()

        For Each row As CEL.Data.BusinessObjects.Uut In uutCollection
            m_cboEngine.Items.Add(row.EngineModel)
        Next

    End Sub

    Private Sub LoadHMINumericChannelCollection(projectNumber As Integer, engineModel As String)
        Using dynamicChannelCollection As New CEL.Data.BusinessObjects.UUTDynamicChannelCollectionCollection()
            dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.ProjectID = projectNumber)
            dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.EngineModel = engineModel)
            dynamicChannelCollection.Query.OrderBy(dynamicChannelCollection.Query.LoadOrder.Ascending)
            dynamicChannelCollection.Query.Load()

            If dynamicChannelCollection.Count > 0 Then
                Using hmiNumericChannelCollection As New CEL.Data.BusinessObjects.HMINumericChannelsCollection
                    Dim query As New CEL.Data.BusinessObjects.HMINumericChannelsQuery()
                    query.es.DefaultConjunction = EntitySpaces.DynamicQuery.esConjunction.Or
                    query.OrderBy(query.Description.Ascending)

                    'Load All possible mapping for different collection name in a SPECIFIC ENGINE MODEL
                    For Each record As CEL.Data.BusinessObjects.UUTDynamicChannelCollection In dynamicChannelCollection
                        query.Where(query.CollectionName = record.CollectionName)
                    Next

                    hmiNumericChannelCollection.Load(query)
                    For Each channel As CEL.Data.BusinessObjects.HMINumericChannels In hmiNumericChannelCollection
                        Dim engUnit As String
                        If IsNothing(channel.EngUnitID) Then
                            engUnit = String.Empty
                        Else
                            engUnit = m_dicEngUnit(channel.EngUnitID.Value)
                            If engUnit.Trim = "None" Then
                                engUnit = ""
                            End If
                        End If
                        m_dgvNumericChannels.Rows.Add(channel.ChannelName, channel.Description, engUnit)
                    Next
                End Using
            End If
        End Using
    End Sub

    Private Sub LoadNumericChannelCollection(projectNumber As Integer, engineModel As String)
        m_dgvNumericChannels.SuspendLayout()

        m_dgvNumericChannels.Rows.Clear()
        LoadHMINumericChannelCollection(projectNumber, engineModel)
        LoadArincUartChannelCollection(projectNumber, engineModel)

        m_dgvNumericChannels.ResumeLayout()
    End Sub

    Private Sub LoadStepTypeCollection()
        m_stepTypeCollection = New CEL.Data.BusinessObjects.TestProgressStepTypeCollection()
        m_stepTypeCollection.LoadAll()
        TestProgressStepTypeCollectionBindingSource.DataSource = m_stepTypeCollection
    End Sub

    Private Sub LoadTestStepCollectionByEngineModel(engineModel As String)
        If m_engineTestStepCollection IsNot Nothing Then
            m_engineTestStepCollection.Dispose()
        End If
        m_engineTestStepCollection = New CEL.Data.BusinessObjects.TestProgressTestStepCollection

        m_engineTestStepCollection.Query.Where(m_engineTestStepCollection.Query.EngineModel = engineModel)
        m_engineTestStepCollection.Query.OrderBy("StepOrder", EntitySpaces.DynamicQuery.esOrderByDirection.Ascending)
        m_engineTestStepCollection.Query.Load()

        bindingSource.DataSource = m_engineTestStepCollection
        m_dgvTestProgressStep.DataSource = bindingSource
    End Sub

    Private Sub bindingSource_AddingNew(sender As Object, e As ComponentModel.AddingNewEventArgs) Handles bindingSource.AddingNew
        Dim newTestStep As New CEL.Data.BusinessObjects.TestProgressTestStep()
        newTestStep.EngineModel = m_currentEngineModel
        newTestStep.StepTypeID = m_stepTypeCollection(0).StepTypeID
        e.NewObject = newTestStep
        SetUIState()
    End Sub

    Private Sub bindingSource_CurrentItemChanged(sender As Object, e As EventArgs) Handles bindingSource.CurrentItemChanged
        SetSaveButtonState()
    End Sub

    Private Sub bindingSource_PositionChanged(sender As Object, e As EventArgs) Handles bindingSource.PositionChanged
        SetUIState()
    End Sub

    Private Sub m_btnChannelSelect_Click(sender As Object, e As EventArgs) Handles m_btnChannelSelect.Click
        Dim esCurrentCheckItem As CEL.Data.BusinessObjects.TestProgressCheckListItem = _
            TryCast(TestProgressCheckListItemCollectionByTestStepIDBindingSource.Current, CEL.Data.BusinessObjects.TestProgressCheckListItem)

        If esCurrentCheckItem IsNot Nothing Then
            If m_checkTypeCollection.FindByPrimaryKey(esCurrentCheckItem.CheckTypeID.Value).RequireChannel Then
                If m_dgvNumericChannels.CurrentRow.Cells("ChannelName").Value IsNot Nothing Then
                esCurrentCheckItem.ChannelName = m_dgvNumericChannels.CurrentRow.Cells("ChannelName").Value.ToString()
                End If
                If m_dgvNumericChannels.CurrentRow.Cells("Description").Value IsNot Nothing Then
                esCurrentCheckItem.ParameterName = m_dgvNumericChannels.CurrentRow.Cells("Description").Value.ToString()
                End If
                If m_dgvNumericChannels.CurrentRow.Cells("Units").Value IsNot Nothing Then
                esCurrentCheckItem.Unit = m_dgvNumericChannels.CurrentRow.Cells("Units").Value.ToString()
            End If
        End If
        End If
    End Sub

    Private Sub btnMoveFirst_Click(sender As Object, e As EventArgs) Handles m_btnMoveFirst.Click
        Dim prevCursor As System.Windows.Forms.Cursor = System.Windows.Forms.Cursor.Current
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            MoveBoundedDgvRow(m_dgvTestProgressStep, RowMoveDirection.First)
        Finally
            System.Windows.Forms.Cursor.Current = prevCursor
        End Try
    End Sub

    Private Sub btnMoveUp_Click(sender As Object, e As EventArgs) Handles m_btnMoveUp.Click
        Dim prevCursor As System.Windows.Forms.Cursor = System.Windows.Forms.Cursor.Current
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            MoveBoundedDgvRow(m_dgvTestProgressStep, RowMoveDirection.Up)
        Finally
            System.Windows.Forms.Cursor.Current = prevCursor
        End Try
    End Sub

    Private Sub btnMoveDown_Click(sender As Object, e As EventArgs) Handles m_btnMoveDown.Click
        Dim prevCursor As System.Windows.Forms.Cursor = System.Windows.Forms.Cursor.Current
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            MoveBoundedDgvRow(m_dgvTestProgressStep, RowMoveDirection.Down)
        Finally
            System.Windows.Forms.Cursor.Current = prevCursor
        End Try
    End Sub

    Private Sub btnMoveLast_Click(sender As Object, e As EventArgs) Handles m_btnMoveLast.Click
        Dim prevCursor As System.Windows.Forms.Cursor = System.Windows.Forms.Cursor.Current
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            MoveBoundedDgvRow(m_dgvTestProgressStep, RowMoveDirection.Last)
        Finally
            System.Windows.Forms.Cursor.Current = prevCursor
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles m_btnSave.Click
        If m_engineTestStepCollection IsNot Nothing Then
            Dim prevCursor As System.Windows.Forms.Cursor = System.Windows.Forms.Cursor.Current
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Try
                SaveData()
                SetUIState()
            Finally
                System.Windows.Forms.Cursor.Current = prevCursor
            End Try
        End If
    End Sub

    Private Sub cboEngine_SelectedIndexChanged(sender As Object, e As EventArgs) Handles m_cboEngine.SelectedIndexChanged
        Dim dataNeedsSaving As Boolean = False
        If m_engineTestStepCollection IsNot Nothing AndAlso m_engineTestStepCollection.IsGraphDirty Then
            Dim result = System.Windows.Forms.MessageBox.Show(Me, "Uncommitted changes. Do you want to save changes?", "Test Progress Step Editor", _
            Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Warning, Windows.Forms.MessageBoxDefaultButton.Button1)

            If result = System.Windows.Forms.DialogResult.Yes Then
                dataNeedsSaving = True
            End If
        End If
        Dim prevCursor As System.Windows.Forms.Cursor = System.Windows.Forms.Cursor.Current
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            If dataNeedsSaving Then
                SaveData()
            End If
            m_currentEngineModel = m_cboEngine.Text
            LoadTestStepCollectionByEngineModel(m_currentEngineModel)
            LoadNumericChannelCollection(m_CELProject.Project.Number, m_currentEngineModel)
            SetUIState()
        Finally
            System.Windows.Forms.Cursor.Current = prevCursor
        End Try
    End Sub

    ' This event handler manually raides the CellValueChanged event
    ' by calling the CommitEdit method.
    Private Sub m_dgvTestProgressTestStepCheckListItems_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles m_dgvTestProgressTestStepCheckListItems.CurrentCellDirtyStateChanged
        If m_dgvTestProgressTestStepCheckListItems.IsCurrentCellDirty Then
            If m_dgvTestProgressTestStepCheckListItems.CurrentCell.ColumnIndex = _
                m_dgvTestProgressTestStepCheckListItems.Columns("CheckTypeID").Index Then

                m_dgvTestProgressTestStepCheckListItems.CommitEdit(Windows.Forms.DataGridViewDataErrorContexts.Commit)

            End If
        End If
    End Sub

    Private Sub m_dgvTestProgressTestStepCheckListItems_CellValueChanged(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles m_dgvTestProgressTestStepCheckListItems.CellValueChanged
        If m_dgvTestProgressTestStepCheckListItems.Columns(e.ColumnIndex).Name = "CheckTypeID" Then
            SetChannelSelectState()
        End If
    End Sub

    Private Sub m_dgvTestProgressTestStepCheckListItems_RowValidating(sender As Object, e As Windows.Forms.DataGridViewCellCancelEventArgs)

    End Sub

    Private Sub MoveBoundedDgvRow(dgvGrid As System.Windows.Forms.DataGridView, direction As RowMoveDirection)
        Dim bindingSource As System.Windows.Forms.BindingSource = CType(dgvGrid.DataSource, System.Windows.Forms.BindingSource)
        Dim rowCount As Integer = bindingSource.Count
        Dim currentRowIndex As Integer = bindingSource.Position
        Select Case direction
            Case RowMoveDirection.Down, RowMoveDirection.Last
                If currentRowIndex = rowCount - 1 Then
                    Return
                End If
            Case RowMoveDirection.Up, RowMoveDirection.Up
                If currentRowIndex = 0 Then
                    Return
                End If
        End Select

        Dim destinationRowIndex As Integer = 0

        Dim esCurrentRow As CEL.Data.BusinessObjects.TestProgressTestStep = _
            TryCast(bindingSource.Current, CEL.Data.BusinessObjects.TestProgressTestStep)
        If esCurrentRow IsNot Nothing Then

            Select Case direction
                Case RowMoveDirection.First
                    destinationRowIndex = 0
                Case RowMoveDirection.Up
                    destinationRowIndex = currentRowIndex - 1
                Case RowMoveDirection.Down
                    destinationRowIndex = currentRowIndex + 1
                Case RowMoveDirection.Last
                    destinationRowIndex = rowCount - 1
            End Select

            bindingSource.RemoveAt(currentRowIndex)
            bindingSource.Insert(destinationRowIndex, esCurrentRow)
            esCurrentRow.RowState = EntitySpaces.Interfaces.esDataRowState.Unchanged
            bindingSource.Position = destinationRowIndex
        Else
            System.Windows.Forms.MessageBox.Show("MoveBoundedDgvRow: esCurrentRow Is Nothing")
        End If
    End Sub

    Private Sub SaveData()
        UpdateTestProgressStepOrder()
        Try
        m_engineTestStepCollection.Save()
        Catch e As System.Exception
            System.Windows.Forms.MessageBox.Show(e.Message, "Save Exception", Windows.Forms.MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub SetChannelSelectState()
        Dim enableChannelSelection As Boolean = False

        Dim esCurrentCheckItem As CEL.Data.BusinessObjects.TestProgressCheckListItem = _
            TryCast(TestProgressCheckListItemCollectionByTestStepIDBindingSource.Current, CEL.Data.BusinessObjects.TestProgressCheckListItem)

        If esCurrentCheckItem IsNot Nothing Then
            If m_checkTypeCollection.FindByPrimaryKey(esCurrentCheckItem.CheckTypeID.Value).RequireChannel Then
                enableChannelSelection = True
            End If
        End If

        If enableChannelSelection Then
            If m_CELProject.CurrentUser.SecurityLevelEnum <> CEL.Data.BusinessObjects.SecurityLevel.eSecurityLevel.Operator Then
                m_btnChannelSelect.Enabled = True
                m_btnChannelSelect.BackgroundImage = TestManager.My.Resources.arrow_right
            End If
        Else
            m_btnChannelSelect.Enabled = False
            m_btnChannelSelect.BackgroundImage = TestManager.My.Resources.arrow_right_disabled
        End If
    End Sub

    Private Sub SetSaveButtonState()
        Dim enabled As Boolean = False
        If m_engineTestStepCollection IsNot Nothing Then
            If m_engineTestStepCollection.IsGraphDirty Then
                enabled = True
            End If
        End If
        m_btnSave.Enabled = enabled
    End Sub

    Private Sub SetUIState()
        Dim bindingSource As System.Windows.Forms.BindingSource = CType(m_dgvTestProgressStep.DataSource, System.Windows.Forms.BindingSource)
        If m_currentEngineModel IsNot String.Empty Then
            m_dgvTestProgressStep.Enabled = True
        Else
            m_dgvTestProgressStep.Enabled = False
        End If

        If m_engineTestStepCollection IsNot Nothing Then
            Dim esCurrentTestStep As CEL.Data.BusinessObjects.TestProgressTestStep = _
                TryCast(bindingSource.Current, CEL.Data.BusinessObjects.TestProgressTestStep)

            If (esCurrentTestStep IsNot Nothing AndAlso bindingSource.Position <> m_dgvTestProgressStep.NewRowIndex) Then
                m_dgvTestProgressTestStepCheckListItems.Enabled = True
            Else
                m_dgvTestProgressTestStepCheckListItems.Enabled = False
            End If

            If (bindingSource.Count > 1 AndAlso _
                bindingSource.Position > 0 AndAlso _
                bindingSource.Position <> m_dgvTestProgressStep.NewRowIndex) Then
                If m_CELProject.CurrentUser.SecurityLevelEnum <> CEL.Data.BusinessObjects.SecurityLevel.eSecurityLevel.Operator Then
                    m_btnMoveFirst.Enabled = True
                    m_btnMoveFirst.BackgroundImage = TestManager.My.Resources.arrow_up_3
                    m_btnMoveUp.Enabled = True
                    m_btnMoveUp.BackgroundImage = TestManager.My.Resources.arrow_up
                End If
            Else
                m_btnMoveFirst.Enabled = False
                m_btnMoveFirst.BackgroundImage = TestManager.My.Resources.arrow_up_3_disabled
                m_btnMoveUp.Enabled = False
                m_btnMoveUp.BackgroundImage = TestManager.My.Resources.arrow_up_disabled
            End If
            If (bindingSource.Count > 1 AndAlso _
                bindingSource.Position < (bindingSource.Count - 1)) Then
                If m_CELProject.CurrentUser.SecurityLevelEnum <> CEL.Data.BusinessObjects.SecurityLevel.eSecurityLevel.Operator Then
                    m_btnMoveDown.Enabled = True
                    m_btnMoveDown.BackgroundImage = TestManager.My.Resources.arrow_down
                    m_btnMoveLast.Enabled = True
                    m_btnMoveLast.BackgroundImage = TestManager.My.Resources.arrow_down_3
                End If
            Else
                m_btnMoveDown.Enabled = False
                m_btnMoveDown.BackgroundImage = TestManager.My.Resources.arrow_down_disabled
                m_btnMoveLast.Enabled = False
                m_btnMoveLast.BackgroundImage = TestManager.My.Resources.arrow_down_3_disabled
            End If
        End If
    End Sub

    Private Sub TestProgressCheckListItemCollectionByTestStepIDBindingSource_AddingNew(sender As Object, e As ComponentModel.AddingNewEventArgs) Handles TestProgressCheckListItemCollectionByTestStepIDBindingSource.AddingNew
        Dim newCheckListItem As New CEL.Data.BusinessObjects.TestProgressCheckListItem()
        newCheckListItem.CheckTypeID = m_checkTypeCollection(0).CheckTypeID
        e.NewObject = newCheckListItem
    End Sub

    Private Sub TestProgressCheckListItemCollectionByTestStepIDBindingSource_CurrentItemChanged(sender As Object, e As EventArgs) Handles TestProgressCheckListItemCollectionByTestStepIDBindingSource.CurrentItemChanged
        SetChannelSelectState()
        SetSaveButtonState()
    End Sub

    Private Sub TestProgressStepForm_FormClosed(sender As Object, e As Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If bindingSource IsNot Nothing Then
            bindingSource.DataSource = Nothing
            bindingSource.Dispose()
        End If
        If TestProgressCheckTypeCollectionBindingSource IsNot Nothing Then
            TestProgressCheckTypeCollectionBindingSource.DataSource = Nothing
            TestProgressCheckTypeCollectionBindingSource.Dispose()
        End If
        If TestProgressStepTypeCollectionBindingSource IsNot Nothing Then
            TestProgressStepTypeCollectionBindingSource.DataSource = Nothing
            TestProgressStepTypeCollectionBindingSource.Dispose()
        End If

        If m_engineTestStepCollection IsNot Nothing Then
            m_engineTestStepCollection.Dispose()
        End If
        If m_checkTypeCollection IsNot Nothing Then
            m_checkTypeCollection.Dispose()
        End If
        If m_stepTypeCollection IsNot Nothing Then
            m_stepTypeCollection.Dispose()
        End If

        bindingSource = Nothing
        TestProgressCheckTypeCollectionBindingSource = Nothing
        TestProgressStepTypeCollectionBindingSource = Nothing
        m_checkTypeCollection = Nothing
        m_stepTypeCollection = Nothing
        m_engineTestStepCollection = Nothing
    End Sub

    Private Sub TestProgressStepForm_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If m_engineTestStepCollection IsNot Nothing Then
            If m_engineTestStepCollection.IsGraphDirty Then
                Dim result = System.Windows.Forms.MessageBox.Show(Me, "Uncommitted changes. Do you want to save changes?", "Test Progress Step Editor Closing", _
                Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Warning, Windows.Forms.MessageBoxDefaultButton.Button1)

                If result = System.Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                ElseIf result = System.Windows.Forms.DialogResult.Yes Then
                    Dim prevCursor As System.Windows.Forms.Cursor = System.Windows.Forms.Cursor.Current
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                    Try
                        SaveData()
                    Finally
                        System.Windows.Forms.Cursor.Current = prevCursor
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub TestProgressStepForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim prevCursor As System.Windows.Forms.Cursor = System.Windows.Forms.Cursor.Current
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            LoadEngineeringUnits()
            LoadEngineSelection()
            LoadCheckTypeCollection()
            LoadStepTypeCollection()
            SetUIState()
            SetChannelSelectState()
        Finally
            System.Windows.Forms.Cursor.Current = prevCursor
        End Try
    End Sub

    Private Sub UpdateTestProgressStepOrder()
        Dim index As Integer = 0
        For Each row As CEL.Data.BusinessObjects.TestProgressTestStep In m_engineTestStepCollection
            If row.RowState = EntitySpaces.Interfaces.esDataRowState.Deleted Then
                Continue For
            End If
            index = index + 1
            row.StepOrder = CType(index, Short)
        Next
    End Sub

    Private Sub m_dgvTestProgressTestStepCheckListItems_Validating(sender As Object, e As ComponentModel.CancelEventArgs)
        e.Cancel = m_dgvTestProgressTestStepCheckListItems.IsCurrentRowDirty
    End Sub

    Private m_progressStepDataFormat As System.Windows.Forms.DataFormats.Format = _
        System.Windows.Forms.DataFormats.GetFormat("ProgressStepDataForm")

    Private Sub DeleteBrindingSourceCurrentSelection()
        Me.bindingSource.RemoveCurrent()
    End Sub

    Private Sub CutProgressStepToClipboard()
        Me.CopyProgressStepToClipboard()
        Me.DeleteBrindingSourceCurrentSelection()
    End Sub

    Private Sub CopyProgressStepToClipboard()
        Dim clipboardData As System.Windows.Forms.IDataObject = Me.GetProgressStepDataObject()
        System.Windows.Forms.Clipboard.SetDataObject(clipboardData)
    End Sub

    Private Function GetProgressStepDataObject() As System.Windows.Forms.DataObject
        Dim clipboardData As System.Windows.Forms.DataObject = Me.m_dgvTestProgressStep.GetClipboardContent()

        Dim steps() As CEL.Data.BusinessObjects.TestProgressTestStep

        ReDim steps(Me.m_dgvTestProgressStep.SelectedRows.Count - 1)
        For i As Integer = 0 To Me.m_dgvTestProgressStep.SelectedRows.Count - 1
            steps(i) = CType(Me.m_dgvTestProgressStep.SelectedRows(i).DataBoundItem, CEL.Data.BusinessObjects.TestProgressTestStep)
        Next
        clipboardData.SetData(m_progressStepDataFormat.Name, steps)
        Return clipboardData
    End Function

    Private Sub PasteProgressStepDataObject(dataObject As System.Windows.Forms.IDataObject)
        Dim progressStepDataObject As Object = dataObject.GetData(m_progressStepDataFormat.Name)
        If progressStepDataObject IsNot Nothing Then
            Dim steps() As CEL.Data.BusinessObjects.TestProgressTestStep = CType(progressStepDataObject, CEL.Data.BusinessObjects.TestProgressTestStep())
            For Each progressStep As CEL.Data.BusinessObjects.TestProgressTestStep In steps
                Dim newStep As New CEL.Data.BusinessObjects.TestProgressTestStep
                newStep.EngineModel = progressStep.EngineModel
                newStep.StepName = progressStep.StepName
                newStep.StepTypeID = progressStep.StepTypeID
                newStep.Title = progressStep.Title
                For Each checkItem As CEL.Data.BusinessObjects.TestProgressCheckListItem In progressStep.TestProgressCheckListItemCollectionByTestStepID
                    Dim newCheckItem As CEL.Data.BusinessObjects.TestProgressCheckListItem = newStep.TestProgressCheckListItemCollectionByTestStepID.AddNew()
                    newCheckItem.ChannelName = checkItem.ChannelName
                    newCheckItem.CheckTypeID = checkItem.CheckTypeID
                    newCheckItem.HighLimit = checkItem.HighLimit
                    newCheckItem.LowLimit = checkItem.LowLimit
                    newCheckItem.ParameterName = checkItem.ParameterName
                    newCheckItem.Unit = checkItem.Unit
                Next
                m_engineTestStepCollection.Add(newStep)
            Next
        End If
    End Sub

    Private Sub PasteProgressStepCsv(dataObject As System.Windows.Forms.IDataObject)
        Dim progressStepDataObject As Object = dataObject.GetData(System.Windows.Forms.DataFormats.CommaSeparatedValue)
        Dim csv As String = TryCast(progressStepDataObject, String)

        If csv Is Nothing Then
            Exit Sub
        End If

        Dim lines As String() = csv.Split(CChar(vbNewLine))
        Dim row As Integer = m_dgvTestProgressStep.NewRowIndex
        Dim col As Integer = m_dgvTestProgressStep.CurrentCell.ColumnIndex
        For Each line As String In lines
            If row < m_dgvTestProgressStep.RowCount AndAlso lines.Length > 0 Then
                Try
                    Dim cells As String() = line.Split(","c)
                    For i As Integer = 0 To cells.Length - 1
                        If (col + i) < Me.m_dgvTestProgressStep.ColumnCount Then
                            m_dgvTestProgressStep(col + i, row).Value = _
                                Convert.ChangeType(cells(i), m_dgvTestProgressStep(col + i, row).ValueType)
                        Else
                            Exit For
                        End If
                    Next
                    row = row + 1
                Catch ex As FormatException
                    m_dgvTestProgressStep.CancelEdit()
                End Try
            Else
                Exit For
            End If
        Next
    End Sub

    Private Sub PasteProgressStepFromClipboard()
        If m_dgvTestProgressStep.SelectedRows.Contains(m_dgvTestProgressStep.Rows(m_dgvTestProgressStep.NewRowIndex)) Then
            m_dgvTestProgressStep.Rows(m_dgvTestProgressStep.NewRowIndex).Selected = False
            m_dgvTestProgressStep.CancelEdit()
        End If

        Dim clipboardData As System.Windows.Forms.IDataObject = System.Windows.Forms.Clipboard.GetDataObject()
        If System.Windows.Forms.Clipboard.ContainsData(m_progressStepDataFormat.Name) Then
            Me.PasteProgressStepDataObject(clipboardData)
        ElseIf System.Windows.Forms.Clipboard.ContainsData(System.Windows.Forms.DataFormats.CommaSeparatedValue) Then
            Me.PasteProgressStepCsv(clipboardData)
        End If
    End Sub

    Private Sub m_cmsTestProgressStepCutMenuItem_Click(sender As Object, e As EventArgs) Handles m_cmsTestProgressStepCutMenuItem.Click
        Me.CutProgressStepToClipboard()
    End Sub

    Private Sub m_cmsTestProgressStepCopyMenuItem_Click(sender As Object, e As EventArgs) Handles m_cmsTestProgressStepCopyMenuItem.Click
        Me.CopyProgressStepToClipboard()
    End Sub

    Private Sub m_cmsTestProgressStepPasteMenuItem_Click(sender As Object, e As EventArgs) Handles m_cmsTestProgressStepPasteMenuItem.Click
        Me.PasteProgressStepFromClipboard()
    End Sub
End Class
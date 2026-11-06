Imports System.Data
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Text
Imports System.Linq
Imports CEL.HMI
Imports CEL.Data.BusinessObjects

Public Class LogReportDetailForm

    Private m_CELProject As CELDAS


    Dim logReportCategories As LogReportCategoriesCollection



    Dim dicEngUnit As New Collections.Generic.Dictionary(Of Integer, String)


    Dim dicChannelList As New Collections.Generic.HashSet(Of String)
    Dim dicChannelListRight As New Collections.Generic.HashSet(Of String)
    Dim dicChannelListLeft As New Collections.Generic.HashSet(Of String)
    Dim dicCategories As New Collections.Generic.Dictionary(Of String, Integer)



    'For Drag Drop usage
    'Private fromIndex As Integer
    Private dragIndex As Integer
    'Private dragRect As Rectangle

    Private AllowMultiRowDrag As Boolean = True
    Private lastLeftMouseDownArgs As MouseEventArgs
    Private dragBoxFromMouseDown As Rectangle
    Private rowIndexFromMouseDown As Int32

    Public Sub New(ByRef celProject As CELDAS)

        ' This call is required by the designer.
        InitializeComponent()

        dgvLeft.AutoGenerateColumns = False

        ' Add any initialization after the InitializeComponent() call.
        m_CELProject = celProject


        btnSave.Enabled = False
        dgvLeft.ReadOnly = True
        dgvRight.ReadOnly = True
        btnAddLeft.Enabled = False
        btnAddRight.Enabled = False
        btnMoveDown.Enabled = False
        btnMoveUp.Enabled = False
        btnMoveFirst.Enabled = False
        btnMoveLast.Enabled = False

        btnConfirm.Enabled = False
        cboPartNumber.Enabled = False
        lblPartNumber.Visible = False
        cboPartNumber.Visible = False

        Select Case m_CELProject.CurrentUser.SecurityLevelEnum
            Case SecurityLevel.eSecurityLevel.Operator

            Case SecurityLevel.eSecurityLevel.Technician, SecurityLevel.eSecurityLevel.TestManager
                btnSave.Enabled = True
                dgvLeft.ReadOnly = False
                dgvRight.ReadOnly = False
                btnAddLeft.Enabled = True
                btnAddRight.Enabled = True
                btnMoveDown.Enabled = True
                btnMoveUp.Enabled = True
                btnMoveFirst.Enabled = True
                btnMoveLast.Enabled = True

        End Select

        LoadEngineSelection()

        'If m_CELProject.DebugMode Then
        '    btnDebug.Visible = True
        'End If

    End Sub

    Private Sub LoadEngineSelection()
        Try

            Dim uutCollection As New UutCollection()
            uutCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
            uutCollection.Query.Where(uutCollection.Query.ProjectID = m_CELProject.Project.Number)
            uutCollection.Query.OrderBy(uutCollection.Query.EngineModel.Ascending)
            uutCollection.Query.Load()

            For Each engine As Uut In uutCollection

                If (engine.EngineModel <> "NO MODEL" And engine.Available) Then
                    'Populate the EngineModel List
                    cboEngine.Items.Add(engine.EngineModel)
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("LoadEngineSelection" + ex.ToString())
        End Try

    End Sub

    Private Sub LoadEngineeringUnits()
        dicEngUnit.Clear()
        Dim engUnitCollection As New EngineeringUnitCollection()
        engUnitCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        engUnitCollection.LoadAll()
        For Each engUnit As EngineeringUnit In engUnitCollection
            dicEngUnit.Add(engUnit.Id, engUnit.DisplayName.Trim)
        Next
    End Sub


    Private Sub LoadReportCategories()

        dicCategories.Clear()
        logReportCategories = New LogReportCategoriesCollection()
        logReportCategories.es.Connection.Name = m_CELProject.DefaultConnectionName
        logReportCategories.Query.Select(logReportCategories.Query.Categories)
        logReportCategories.Query.Select(logReportCategories.Query.Order)
        logReportCategories.Query.Where(logReportCategories.Query.ProjectID = m_CELProject.Project.Number)
        logReportCategories.Query.Where(logReportCategories.Query.EngineModel = m_CELProject.EngineModel)
        logReportCategories.Query.OrderBy(logReportCategories.Query.Order.Ascending)
        logReportCategories.Query.Load()


        'Dim cboCategories As DataGridViewComboBoxColumn = dgvRight.Columns("colCategoryRight")
        'colCategoryRight.DataSource = logReportCategories
        colCategoryRight.Items.Clear()
        For Each row As LogReportCategories In logReportCategories
            colCategoryRight.Items.Add(row.Categories)
            dicCategories.Add(row.Categories, row.Order)
        Next

        RefreshSortRightGrid()

    End Sub
    Private Sub RefreshSortLeftGrid()
        dgvLeft.Sort(colChannelName, System.ComponentModel.ListSortDirection.Ascending)
    End Sub
    Private Sub RefreshSortRightGrid()

        Dim i As Integer = 0
        For Each row As DataGridViewRow In dgvRight.Rows
            Dim category As String = row.Cells("colCategoryRight").Value
            Dim categoryViewOrder As Integer = 0
            If Not IsNothing(category) AndAlso category <> String.Empty Then
                If dicCategories.ContainsKey(category) Then
                    categoryViewOrder = dicCategories(category) 'Colonne invisible ViewOrder pour faire le tri
                    row.Cells("colCategoryViewOrder").Value = categoryViewOrder
                    row.Cells("colViewOrderInCategory").Value = i
                    i += 1
                Else
                    row.Cells("colCategoryViewOrder").Value = 0
                    row.Cells("colViewOrderInCategory").Value = 0
                End If

            Else
                row.Cells("colCategoryViewOrder").Value = 0
                row.Cells("colViewOrderInCategory").Value = 0
            End If
        Next

        dgvRight.Sort(New RowComparer(SortOrder.Ascending))

        i = 0
        For Each row As DataGridViewRow In dgvRight.Rows
            Dim category As String = row.Cells("colCategoryRight").Value
            Dim categoryViewOrder As Integer = 0
            If Not IsNothing(category) AndAlso category <> String.Empty Then
                If dicCategories.ContainsKey(category) Then
                    categoryViewOrder = dicCategories(category) 'Colonne invisible ViewOrder pour faire le tri
                    row.Cells("colCategoryViewOrder").Value = categoryViewOrder
                    row.Cells("colViewOrderInCategory").Value = i
                    i += 1
                Else
                    row.Cells("colCategoryViewOrder").Value = 0
                    row.Cells("colViewOrderInCategory").Value = 0
                End If

            Else
                row.Cells("colCategoryViewOrder").Value = 0
                row.Cells("colViewOrderInCategory").Value = 0
            End If
        Next
        RefreshGridAlternateBackcolorRight()
    End Sub
    Private Sub RefreshGridAlternateBackcolorRight()
        If dgvRight.Rows.Count > 0 Then

            Dim categoryID As String = dgvRight.Rows(0).Cells("colCategoryViewOrder").Value
            Dim currentBackcolor As Drawing.Color = Color.White
            For Each row As DataGridViewRow In dgvRight.Rows
                If categoryID <> row.Cells("colCategoryViewOrder").Value Then
                    categoryID = row.Cells("colCategoryViewOrder").Value
                    If currentBackcolor = Color.White Then 'Alternate Colors
                        currentBackcolor = Color.LightGray
                    Else
                        currentBackcolor = Color.White
                    End If
                End If
                row.DefaultCellStyle.BackColor = currentBackcolor
            Next
        End If
    End Sub
    Public Class RowComparer
        Implements System.Collections.IComparer

        Private sortOrderModifier As Integer = 1

        Public Sub New(ByVal sortOrder As SortOrder)
            If sortOrder = sortOrder.Descending Then
                sortOrderModifier = -1
            ElseIf sortOrder = sortOrder.Ascending Then

                sortOrderModifier = 1
            End If
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements Collections.IComparer.Compare
            Dim row1 As DataGridViewRow = CType(x, DataGridViewRow)
            Dim row2 As DataGridViewRow = CType(y, DataGridViewRow)

            Dim result As Integer

            result = row1.Cells("colCategoryViewOrder").Value.CompareTo(row2.Cells("colCategoryViewOrder").Value)

            If (result <> 0) Then
                Return result * sortOrderModifier
            End If

            result = row1.Cells("colViewOrderInCategory").Value.CompareTo(row2.Cells("colViewOrderInCategory").Value)

            If (result <> 0) Then
                Return result * sortOrderModifier
            End If

            Return row1.Cells(0).Value.ToString().CompareTo(row2.Cells(0).Value.ToString()) * sortOrderModifier

        End Function


    End Class

    Private Sub LoadRightGrid()

        dgvRight.Rows.Clear()

        Dim logReportDetailCollection As New LogReportDetailCollection()

        Dim notValidChannelsList As New Collections.Generic.List(Of String)
        Dim channelNotFound As Boolean
        Dim PartNumber As String = "Default"

        notValidChannelsList.Clear()

        If cboPartNumber.SelectedItem IsNot Nothing Then
            PartNumber = cboPartNumber.SelectedItem
        End If

        logReportDetailCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        logReportDetailCollection.Query.Where(logReportDetailCollection.Query.ProjectID = m_CELProject.Project.Number)
        logReportDetailCollection.Query.Where(logReportDetailCollection.Query.EngineModel = m_CELProject.EngineModel)
        logReportDetailCollection.Query.Where(logReportDetailCollection.Query.PwcPn = PartNumber)

        logReportDetailCollection.Query.Load()

        For Each record As LogReportDetail In logReportDetailCollection
            channelNotFound = False

            If Not dicChannelList.Contains(record.ChannelName) Then
                notValidChannelsList.Add(record.ChannelName)
                channelNotFound = True
            End If

            Dim category As String = record.Category
            Dim categoryViewOrder As Integer = 0
            If Not IsNothing(category) AndAlso category <> String.Empty Then
                If dicCategories.ContainsKey(category) Then
                    categoryViewOrder = dicCategories(record.Category) 'Colonne invisible ViewOrder pour faire le tri
                End If
            End If
            Dim displayUnit As String = record.DisplayUnit & ""

            dgvRight.Rows.Add(record.Category, categoryViewOrder, record.ChannelName, record.Description, displayUnit.Trim, record.Format)
            If channelNotFound Then
                dgvRight.Rows(dgvRight.Rows.Count - 1).Cells(2).Style.BackColor = Color.Red
            Else

                RemoveRowLeftGrid(record.ChannelName)
            End If

        Next


        If channelNotFound Then
            Dim notValidWarningForm As New ChannelsNotValidWarningForm(notValidChannelsList)
            notValidWarningForm.StartPosition = FormStartPosition.CenterScreen
            notValidWarningForm.ShowDialog()
        End If

    End Sub
    Private Sub RemoveRowLeftGrid(ByVal ChannelName)
        For Each row As DataGridViewRow In dgvLeft.Rows
            If row.Cells("colChannelName").Value = ChannelName Then
                dgvLeft.Rows.Remove(row)
                dicChannelListLeft.Remove(ChannelName)
            End If
        Next

    End Sub
    Private Sub LoadLeftGrid()

        dgvLeft.Rows.Clear()
        'Query Database
        esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()


        Dim boolChanQuery As HMIBooleanChannelsQuery = New HMIBooleanChannelsQuery()
        Dim numChanQuery As HMINumericChannelsQuery = New HMINumericChannelsQuery()
        Dim textChanQuery As HMITextChannelsQuery = New HMITextChannelsQuery()

        Dim booleanChannelsCollection As New HMIBooleanChannelsCollection()
        booleanChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName

        Dim numericChannelsCollection As New HMINumericChannelsCollection()
        numericChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName

        Dim textChannelsCollection As New HMITextChannelsCollection()
        textChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName

        Dim arincChannelsCollection As New ARINC429LabelTemplateCollection()
        arincChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        arincChannelsCollection.Query.es.QuerySource = "DACN1_ARINC429_LABEL"

        Dim omnibusboxChannelsCollection As New OmniBusBoxUartTemplateCollection()
        omnibusboxChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        omnibusboxChannelsCollection.Query.es.QuerySource = "DACN1_OmniBusBox_IOMGR_Uart"

        Dim canbusChannelsCollection As New Dacn1CanbusLabelCollection()
        canbusChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        canbusChannelsCollection.Query.es.QuerySource = "DACN1_CANBUS_LABEL"

        Dim dynamicChannelCollection As New UUTDynamicChannelCollectionCollection()
        dynamicChannelCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.ProjectID = m_CELProject.Project.Number)
        dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.EngineModel = m_CELProject.EngineModel)
        dynamicChannelCollection.Query.OrderBy(dynamicChannelCollection.Query.LoadOrder.Ascending)
        dynamicChannelCollection.Query.Load()



        If dynamicChannelCollection.Count > 0 Then

            boolChanQuery.SelectAll()
            numChanQuery.SelectAll()
            textChanQuery.SelectAll()



            'Load All possible mapping for different collection name in a SPECIFIC ENGINE MODEL
            For Each record As UUTDynamicChannelCollection In dynamicChannelCollection

                boolChanQuery.es.DefaultConjunction = esConjunction.Or
                boolChanQuery.Where(boolChanQuery.CollectionName = record.CollectionName And boolChanQuery.OPCGroup.IsNull And boolChanQuery.Unavailable = False)

                numChanQuery.es.DefaultConjunction = esConjunction.Or
                numChanQuery.Where(numChanQuery.CollectionName = record.CollectionName And numChanQuery.OPCGroup.IsNull And numChanQuery.Unavailable = False)


                textChanQuery.es.DefaultConjunction = esConjunction.Or
                textChanQuery.Where(textChanQuery.CollectionName = record.CollectionName And textChanQuery.OPCGroup.IsNull And textChanQuery.Unavailable = False)

            Next

            arincChannelsCollection.Query.Where(arincChannelsCollection.Query.EngineModel = m_CELProject.EngineModel)

            If cboPartNumber.SelectedItem Is Nothing Then
                omnibusboxChannelsCollection.Query.Where(omnibusboxChannelsCollection.Query.EngineModel = m_CELProject.EngineModel)
            Else
                omnibusboxChannelsCollection.Query.Where(omnibusboxChannelsCollection.Query.EngineModel = m_CELProject.EngineModel And omnibusboxChannelsCollection.Query.PwcPn = cboPartNumber.SelectedItem.ToString)
            End If

            canbusChannelsCollection.Query.Where(canbusChannelsCollection.Query.EngineModel = m_CELProject.EngineModel)


            Dim sb As New StringBuilder

            'booleanChannelsCollection.Query.Load()
            booleanChannelsCollection.Load(boolChanQuery)
            For Each channel As HMIBooleanChannels In booleanChannelsCollection
                If Not dicChannelListLeft.Contains(channel.ChannelName) Then
                    dicChannelListLeft.Add(channel.ChannelName)
                    dicChannelList.Add(channel.ChannelName)
                    dgvLeft.Rows.Add(channel.ChannelName, channel.Description, "", "", "BOOLEAN")
                Else
                    sb.AppendLine("The channel " + channel.ChannelName + " already exist in the collection")
                End If
            Next

            'numericChannelsCollection.Query.Load()
            numericChannelsCollection.Load(numChanQuery)
            For Each channel As HMINumericChannels In numericChannelsCollection
                If Not dicChannelListLeft.Contains(channel.ChannelName) Then
                    'dgvLeft.Rows.Add(channel.CollectionName, channel.ChannelName, channel.Description, channel.UpToEngineeringUnitByEngUnitID, "NUMERIC")
                    dicChannelListLeft.Add(channel.ChannelName)
                    dicChannelList.Add(channel.ChannelName)
                    Dim engUnit As String
                    If IsNothing(channel.EngUnitID) Then
                        engUnit = String.Empty
                    Else
                        engUnit = dicEngUnit(channel.EngUnitID)
                        If engUnit.Trim = "None" Then
                            engUnit = ""
                        End If
                    End If
                    dgvLeft.Rows.Add(channel.ChannelName, channel.Description, engUnit, channel.Format, "NUMERIC")
                Else
                    sb.AppendLine("The channel " + channel.ChannelName + " already exist in the collection")
                End If
            Next

            textChannelsCollection.Load(textChanQuery)
            For Each channel As HMITextChannels In textChannelsCollection
                If Not dicChannelListLeft.Contains(channel.ChannelName) Then
                    dicChannelListLeft.Add(channel.ChannelName)
                    dicChannelList.Add(channel.ChannelName)
                    dgvLeft.Rows.Add(channel.ChannelName, channel.Description, "", "", "TEXT")
                Else
                    sb.AppendLine("The channel " + channel.ChannelName + " already exist in the collection")
                End If
            Next

            arincChannelsCollection.Query.Load()
            For Each channel As ARINC429LabelTemplate In arincChannelsCollection
                If Not dicChannelListLeft.Contains(channel.Name) Then
                    dicChannelListLeft.Add(channel.Name)
                    dicChannelList.Add(channel.Name)
                    dgvLeft.Rows.Add(channel.Name, channel.Description, channel.Units, channel.DisplayFormat, "ARINC")
                Else
                    sb.AppendLine("The channel " + channel.Name + " already exist in the collection")
                End If
            Next

            omnibusboxChannelsCollection.Query.Load()
            For Each channel As OmniBusBoxUartTemplate In omnibusboxChannelsCollection
                If Not dicChannelListLeft.Contains(channel.Name) Then
                    dicChannelListLeft.Add(channel.Name)
                    dicChannelList.Add(channel.Name)
                    dgvLeft.Rows.Add(channel.Name, channel.Parameter, channel.Units, channel.Format, "UART")
                Else
                    sb.AppendLine("The channel " + channel.Name + " already exist in the collection")
                End If
            Next

            canbusChannelsCollection.Query.Load()
            For Each channel As Dacn1CanbusLabel In canbusChannelsCollection
                If Not dicChannelListLeft.Contains(channel.ChannelName) Then
                    dicChannelListLeft.Add(channel.ChannelName)
                    dicChannelList.Add(channel.ChannelName)
                    Dim engUnit As String
                    If IsNothing(channel.EngUnitID) Then
                        engUnit = String.Empty
                    Else
                        engUnit = dicEngUnit(channel.EngUnitID)
                        If engUnit.Trim = "None" Then
                            engUnit = ""
                        End If
                    End If
                    dgvLeft.Rows.Add(channel.ChannelName, channel.Description, engUnit, channel.DisplayFormat, "CANBUS")
                Else
                    sb.AppendLine("The channel " + channel.ChannelName + " already exist in the collection")
                End If
            Next


            If sb.Length > 0 Then
                MessageBox.Show(sb.ToString, "Invalide Duplicate Channel")
            End If

        End If

    End Sub



    Private Sub btnAddRight_Click(sender As Object, e As EventArgs) Handles btnAddRight.Click

        For Each row In dgvLeft.SelectedRows
            If Not dicChannelListRight.Contains(row.Cells("colChannelName").Value) Then
                dicChannelListRight.Add(row.Cells("colChannelName").Value)
                dicChannelListLeft.Remove(row.Cells("colChannelName").Value)
                Dim index As Integer
                If IsNothing(dgvRight.CurrentRow) Then
                    index = 0
                Else
                    index = dgvRight.CurrentRow.Index
                End If
                Dim categoryDescription As String = ""
                Dim categoryViewOrder As Integer = 0
                Dim viewOrder As Integer = 0
                If (Not IsNothing(dgvRight.CurrentRow) AndAlso dgvRight.CurrentRow.Index >= 0) Then
                    categoryDescription = dgvRight.CurrentRow.Cells("colCategoryRight").Value.ToString
                    categoryViewOrder = dgvRight.CurrentRow.Cells("colCategoryViewOrder").Value.ToString
                    viewOrder = dgvRight.CurrentRow.Cells("colViewOrderInCategory").Value
                End If
                dgvRight.Rows.Insert(index, _
                                     categoryDescription, _
                                     categoryViewOrder, _
                                    row.Cells("colChannelName").Value, _
                                    row.Cells("colDescription").Value, _
                                    row.Cells("colUnit").Value, _
                                    row.Cells("colFormat").Value, _
                                    viewOrder)

                dgvLeft.Rows.Remove(row)

                'dgvRight.ClearSelection()
                'dgvRight.Rows(dgvRight.CurretRow.Index).Cells(0).Selected = True
            Else
                MessageBox.Show("The Item already exist in the Log REPORT")
            End If
        Next
        RefreshSortRightGrid()
    End Sub


    Private Sub btnAddLeft_Click(sender As Object, e As EventArgs) Handles btnAddLeft.Click

        For Each row In dgvRight.SelectedRows
            If dicChannelList.Contains(row.Cells("colChannelNameRight").Value) Or Not (FindDuplicate(row.Cells("colChannelNameRight").Value)) Then 'Les duplicate seront simplement retiré sans ajouter a la liste de gauche
                If Not dicChannelListLeft.Contains(row.Cells("colChannelNameRight").Value) Then
                    dicChannelListLeft.Add(row.Cells("colChannelNameRight").Value)
                    dicChannelListRight.Remove(row.Cells("colChannelNameRight").Value)
                    Dim index As Integer
                    If IsNothing(dgvLeft.CurrentRow) Then
                        index = 0
                    Else
                        index = dgvLeft.CurrentRow.Index
                    End If
                    dgvLeft.Rows.Insert(index, _
                                        row.Cells("colChannelNameRight").Value, _
                                        row.Cells("colDescriptionRight").Value, _
                                        row.Cells("colUnitRight").Value, _
                                        row.Cells("colFormatRight").Value)

                Else
                    MessageBox.Show("The Item already exist in the LEFT list")
                End If
                dgvRight.Rows.Remove(row)
            Else
                dgvRight.Rows.Remove(row) 'Le nom du Channel a probablement changé et n'existe plus alors on le retire simplement
            End If
        Next

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        RefreshSortRightGrid()
        Save()

    End Sub


    Private Sub Save()
        If btnSave.Enabled Then
            dgvRight.EndEdit()

            Dim logReportDetailCollection As New LogReportDetailCollection()
            Dim PartNumber As String = "Default"

            If cboPartNumber.SelectedItem IsNot Nothing Then
                PartNumber = cboPartNumber.SelectedItem
            End If

            logReportDetailCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
            logReportDetailCollection.Query.Where(logReportDetailCollection.Query.ProjectID = m_CELProject.Project.Number)
            logReportDetailCollection.Query.Where(logReportDetailCollection.Query.EngineModel = m_CELProject.EngineModel)
            logReportDetailCollection.Query.Where(logReportDetailCollection.Query.PwcPn = PartNumber)

            logReportDetailCollection.Query.Load()

            If logReportDetailCollection.HasData Then
                logReportDetailCollection.MarkAllAsDeleted()
                logReportDetailCollection.Save()
            End If

            Try
                Dim i As Integer = 0
                For Each row In dgvRight.Rows
                    Dim newRow As LogReportDetail = logReportDetailCollection.AddNew()
                    i += 1
                    newRow.ProjectID = m_CELProject.Project.Number
                    newRow.EngineModel = m_CELProject.EngineModel
                    newRow.PwcPn = PartNumber
                    newRow.ChannelName = row.Cells("colChannelNameRight").Value
                    newRow.DisplayUnit = row.Cells("colUnitRight").Value
                    newRow.Category = row.Cells("colCategoryRight").Value
                    newRow.Description = row.Cells("colDescriptionRight").Value
                    newRow.DisplayUnit = row.Cells("colUnitRight").Value
                    newRow.Format = row.Cells("colFormatRight").Value
                    newRow.OrderInCategory = i


                Next
                logReportDetailCollection.Save()

            Catch ex As Exception
                logReportDetailCollection.RejectChanges()
                MessageBox.Show(ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If btnSave.Enabled Then
            Select Case MessageBox.Show("Save and Exit ?", "Save Data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                Case Windows.Forms.DialogResult.Yes
                    Save()
                    Me.Close()
                Case Windows.Forms.DialogResult.No
                    Me.Close()
                Case Windows.Forms.DialogResult.Cancel

            End Select
        End If
    End Sub

    Private Sub btnMoveFirst_Click(sender As Object, e As EventArgs) Handles btnMoveFirst.Click
        Dim grid As DataGridView = dgvRight
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
            RefreshSortRightGrid()
        Catch
        End Try
    End Sub

    Private Sub btnMoveUp_Click(sender As Object, e As EventArgs) Handles btnMoveUp.Click
        Dim grid As DataGridView = dgvRight
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
            RefreshSortRightGrid()
        Catch
        End Try
    End Sub

    Private Sub btnMoveDown_Click(sender As Object, e As EventArgs) Handles btnMoveDown.Click
        Dim grid As DataGridView = dgvRight
        Try
            Dim totalRows As Integer = grid.Rows.Count
            Dim idx As Integer = grid.SelectedCells(0).OwningRow.Index
            If idx = totalRows - 1 Then
                Return
            End If
            Dim col As Integer = grid.SelectedCells(0).OwningColumn.Index
            Dim rows As DataGridViewRowCollection = grid.Rows
            Dim row As DataGridViewRow = rows(idx)
            rows.Remove(row)
            rows.Insert(idx + 1, row)
            grid.ClearSelection()
            grid.Rows(idx + 1).Cells(col).Selected = True
            RefreshSortRightGrid()
        Catch
        End Try
    End Sub

    Private Sub btnMoveLast_Click(sender As Object, e As EventArgs) Handles btnMoveLast.Click
        Dim grid As DataGridView = dgvRight
        Try
            Dim totalRows As Integer = grid.Rows.Count
            Dim idx As Integer = grid.SelectedCells(0).OwningRow.Index
            If idx = totalRows - 1 Then
                Return
            End If
            Dim col As Integer = grid.SelectedCells(0).OwningColumn.Index
            Dim rows As DataGridViewRowCollection = grid.Rows
            Dim row As DataGridViewRow = rows(idx)
            rows.Remove(row)

            rows.Insert(totalRows - 2, row)
            grid.ClearSelection()

            grid.Rows(totalRows - 2).Cells(col).Selected = True
            RefreshSortRightGrid()
        Catch
        End Try
    End Sub

    Private Sub dgvRight_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRight.CellEndEdit
        If FindDuplicate(dgvRight.CurrentRow.Cells(2).Value.ToString()) Then
            dgvRight.CurrentRow.Cells(2).Style.BackColor = Color.Yellow
            'If MessageBox.Show("This channel " + dgvRight.CurrentRow.Cells(2).Value.ToString() + " is already in the Log Report." + vbCrLf + "Do you want to delete it", "Duplicate Channel", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.Yes Then
            '    dgvRight.Rows.Remove(dgvRight.CurrentRow)
            'End If
        End If
    End Sub
    Private Sub dgvRight_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRight.CellValueChanged
        If e.ColumnIndex = 0 AndAlso e.RowIndex >= 0 Then
            Dim category As String = dgvRight.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            If Not IsNothing(category) AndAlso category <> String.Empty Then
                If dicCategories.ContainsKey(category) Then
                    dgvRight.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = dicCategories(category) 'Colonne invisible ViewOrder pour faire le tri
                    RefreshSortRightGrid()
                End If
            End If
        ElseIf e.ColumnIndex = 2 AndAlso e.RowIndex >= 0 Then 'Validate ChannelName Exists
            If Not dicChannelList.Contains(dgvRight.CurrentRow.Cells(2).Value.ToString()) Then
                ' dgvRight.Rows(dgvRight.Rows.Count - 1).Cells(2).Style.BackColor = Color.Red
                dgvRight.CurrentRow.Cells(2).Style.BackColor = Color.Red
            Else
                dgvRight.CurrentRow.Cells(2).Style.BackColor = Color.White
            End If
            If FindDuplicate(dgvRight.CurrentRow.Cells(2).Value.ToString()) Then
                dgvRight.CurrentRow.Cells(2).Style.BackColor = Color.Yellow

            End If

        End If
    End Sub
    Private Function FindDuplicate(ByVal channelName As String) As Boolean
        Dim currentRowIndex As Integer = dgvRight.CurrentRow.Index
        For rowIndex As Integer = 0 To dgvRight.Rows.Count - 1
            If (dgvRight.Rows(rowIndex).Cells(2).Value <> Nothing AndAlso _
                dgvRight.Rows(rowIndex).Cells(2).Value.Equals(channelName)) AndAlso _
                currentRowIndex <> rowIndex Then
                dgvRight.Rows(rowIndex).Cells(2).Style.BackColor = Color.Yellow
                Return True
                'Add To datagridview

            End If

        Next rowIndex

        Return False

    End Function



    Private Sub dgvRight_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles dgvRight.DragDrop

        Dim p As Point = dgvRight.PointToClient(New Point(e.X, e.Y))
        dragIndex = dgvRight.HitTest(p.X, p.Y).RowIndex
        If (e.Effect = DragDropEffects.Move) Then
            If Me.AllowMultiRowDrag Then
                Dim dragRowCollection As DataGridViewSelectedRowCollection = CType(e.Data.GetData(GetType(DataGridViewSelectedRowCollection)), DataGridViewSelectedRowCollection)
                For Each row As DataGridViewRow In dragRowCollection
                    dgvRight.Rows.Remove(row)
                    dgvRight.Rows.Insert(dragIndex, row)

                Next

            Else
                Dim dragRow As DataGridViewRow = e.Data.GetData(GetType(DataGridViewRow))

                dgvRight.Rows.Remove(dragRow)
                dgvRight.Rows.Insert(dragIndex, dragRow)

            End If
        End If
        dgvRight.ClearSelection()
        'dgvRight.Rows(dragIndex).Cells(0).Selected = True
        RefreshSortRightGrid()

    End Sub
    Private Sub dgvRight_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles dgvRight.DragOver
        e.Effect = DragDropEffects.Move
    End Sub
    Private Sub dgvRight_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvRight.MouseDown
        If (e.Button And Windows.Forms.MouseButtons.Left) = Windows.Forms.MouseButtons.Left Then
            rowIndexFromMouseDown = dgvRight.HitTest(e.X, e.Y).RowIndex
            If rowIndexFromMouseDown <> -1 Then
                Dim dragSize As Size = SystemInformation.DragSize
                dragBoxFromMouseDown = New Rectangle(New Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize)
                lastLeftMouseDownArgs = e 'remember the MouseEventArgs

                If AllowMultiRowDrag Then
                    Exit Sub 'Don't call the base function here to keep the selection
                End If

            Else
                dragBoxFromMouseDown = Rectangle.Empty
                lastLeftMouseDownArgs = Nothing
            End If
        End If
        MyBase.OnMouseDown(e) 'in all other cases call the base function
    End Sub
    Private Sub dgvRight_MouseUp(sender As Object, e As MouseEventArgs) Handles dgvRight.MouseUp
        If lastLeftMouseDownArgs IsNot Nothing AndAlso (e.Button And Windows.Forms.MouseButtons.Left) = Windows.Forms.MouseButtons.Left Then
            'there has been a multiselect drag operation so catch up the MouseDown event
            If AllowMultiRowDrag Then MyBase.OnMouseDown(lastLeftMouseDownArgs)
        End If
        MyBase.OnMouseUp(e) 'now call the base Up-function
    End Sub

    Private Sub dgvRight_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvRight.MouseMove

        If lastLeftMouseDownArgs IsNot Nothing AndAlso (e.Button And Windows.Forms.MouseButtons.Left) = Windows.Forms.MouseButtons.Left Then
            If (dragBoxFromMouseDown <> Rectangle.Empty) AndAlso (Not dragBoxFromMouseDown.Contains(e.X, e.Y)) Then
                'we want to drag one/multiple rows

                Dim row As DataGridViewRow = dgvRight.Rows(rowIndexFromMouseDown)
                row.Selected = True 'always select the row that was under the mouse in the OnMouseDown event.

                If Me.AllowMultiRowDrag Then
                    'multiselect drag, so make all selected rows the drag data
                    Dim dropEffect As DragDropEffects = Me.DoDragDrop(dgvRight.SelectedRows, DragDropEffects.Move)
                Else

                    'only one row to drag
                    Dim dropEffect As DragDropEffects = Me.DoDragDrop(row, DragDropEffects.Move)
                End If
            End If
        End If
        MyBase.OnMouseMove(e) 'let's do the base class the rest
    End Sub
    Private Sub dgvRight_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles dgvRight.DataError

        If e.Exception IsNot Nothing Then
            Try
                'Probablement que la Categorie a ete effacte e
                If e.ColumnIndex = 0 Then
                    dgvRight.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub



    Private Sub btnLogReportCategories_Click_1(sender As Object, e As EventArgs) Handles btnEditCategories.Click
        Dim frmLogReportCategories As New LogReportCategoriesForm(m_CELProject)
        frmLogReportCategories.ShowDialog()

        LoadReportCategories()

    End Sub

    Private Sub cboEngine_TextChanged(sender As Object, e As EventArgs) Handles cboEngine.TextChanged
        Dim partNumberCollection As New DACN1OmniBusBoxIOMGRCollection()
        Dim partNumberList As System.Collections.Generic.List(Of String)

        partNumberCollection.es.Connection.Name = m_CELProject.DefaultConnectionName

        lblPartNumber.Visible = False
        cboPartNumber.DataSource = Nothing
        cboPartNumber.Items.Clear()
        cboPartNumber.Visible = False

        If cboEngine.SelectedItem IsNot Nothing Then
            partNumberCollection.Query.Where(partNumberCollection.Query.EngineModel = cboEngine.SelectedItem.ToString)
            partNumberCollection.Query.Load()
            partNumberList = (From partNumber In partNumberCollection Select partNumber.PwcPn).ToList

            If partNumberList.Count > 0 Then
                lblPartNumber.Visible = True
                cboPartNumber.Visible = True
                cboPartNumber.DataSource = partNumberList
                cboPartNumber.Enabled = True
            End If

            btnConfirm.Enabled = True
        Else
            cboPartNumber.Enabled = False
            btnConfirm.Enabled = False
        End If
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click

        m_CELProject.EngineModel = cboEngine.Text

        dicChannelList.Clear()
        dicChannelListLeft.Clear()
        dicChannelListRight.Clear()

        LoadEngineeringUnits()
        LoadReportCategories()
        LoadLeftGrid()
        LoadRightGrid()

        RefreshSortLeftGrid()
        RefreshSortRightGrid()

    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnDebug.Click



        '    dgvRight.Rows.Clear()

        '    Dim boolChanQuery As HMIBooleanChannelsQuery = New HMIBooleanChannelsQuery()
        '    Dim numChanQuery As HMINumericChannelsQuery = New HMINumericChannelsQuery()
        '    Dim textChanQuery As HMITextChannelsQuery = New HMITextChannelsQuery()
        '    'Dim arincChanQuery As ARINC429LabelTemplateQuery = New ARINC429LabelTemplateQuery()
        '    'Dim omnibusboxChanQuery As OmniBusBoxUartTemplateQuery = New OmniBusBoxUartTemplateQuery()

        '    Dim booleanChannelsCollection As New HMIBooleanChannelsCollection()
        '    booleanChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName

        '    Dim numericChannelsCollection As New HMINumericChannelsCollection()
        '    numericChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName

        '    Dim textChannelsCollection As New HMITextChannelsCollection()
        '    textChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName

        '    Dim arincChannelsCollection As New ARINC429LabelTemplateCollection()
        '    arincChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        '    arincChannelsCollection.Query.es.QuerySource = "DACN1_ARINC429_LABEL"

        '    Dim omnibusboxChannelsCollection As New OmniBusBoxUartTemplateCollection()
        '    omnibusboxChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        '    omnibusboxChannelsCollection.Query.es.QuerySource = "DACN1_OmniBusBox_IOMGR_Uart"

        '    Dim canbusChannelsCollection As New Dacn1CanbusLabelCollection()
        '    canbusChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        '    canbusChannelsCollection.Query.es.QuerySource = "DACN1_CANBUS_LABEL"

        '    Dim dynamicChannelCollection As New UUTDynamicChannelCollectionCollection()
        '    dynamicChannelCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        '    dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.ProjectID = m_CELProject.Project.Number)
        '    dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.EngineModel = m_CELProject.EngineModel)
        '    dynamicChannelCollection.Query.OrderBy(dynamicChannelCollection.Query.LoadOrder.Ascending)
        '    dynamicChannelCollection.Query.Load()



        '    If dynamicChannelCollection.Count > 0 Then

        '        boolChanQuery.SelectAll()
        '        numChanQuery.SelectAll()
        '        textChanQuery.SelectAll()


        '        'Load All possible mapping for different collection name in a SPECIFIC ENGINE MODEL
        '        For Each record As UUTDynamicChannelCollection In dynamicChannelCollection

        '            boolChanQuery.es.DefaultConjunction = esConjunction.Or
        '            boolChanQuery.Where(boolChanQuery.CollectionName = record.CollectionName And boolChanQuery.LogReportOrderInCategorie > 0)

        '            boolChanQuery.OrderBy(boolChanQuery.LogReportCategorie.Ascending)
        '            boolChanQuery.OrderBy(boolChanQuery.LogReportOrderInCategorie.Ascending)

        '            numChanQuery.es.DefaultConjunction = esConjunction.Or
        '            numChanQuery.Where(numChanQuery.CollectionName = record.CollectionName And numChanQuery.LogReportOrderInCategorie > 0)
        '            numChanQuery.OrderBy(numChanQuery.LogReportCategorie.Ascending)
        '            numChanQuery.OrderBy(numChanQuery.LogReportOrderInCategorie.Ascending)


        '            textChanQuery.es.DefaultConjunction = esConjunction.Or
        '            textChanQuery.Where(textChanQuery.CollectionName = record.CollectionName And textChanQuery.LogReportOrderInCategorie > 0)
        '            textChanQuery.OrderBy(textChanQuery.LogReportCategorie.Ascending)
        '            textChanQuery.OrderBy(textChanQuery.LogReportOrderInCategorie.Ascending)

        '        Next



        '        'arincChanQuery.Where(arincChanQuery.EngineModel = _CELProject.EngineModel)
        '        'omnibusboxChanQuery.Where(omnibusboxChanQuery.EngineModel = _CELProject.EngineModel)

        '        arincChannelsCollection.Query.Where(arincChannelsCollection.Query.EngineModel = m_CELProject.EngineModel)
        '        arincChannelsCollection.Query.Where(arincChannelsCollection.Query.LogReportOrderInCategorie > 0)
        '        arincChannelsCollection.Query.OrderBy(arincChannelsCollection.Query.LogReportCategorie.Ascending)
        '        arincChannelsCollection.Query.OrderBy(arincChannelsCollection.Query.LogReportOrderInCategorie.Ascending)

        '        omnibusboxChannelsCollection.Query.Where(omnibusboxChannelsCollection.Query.EngineModel = m_CELProject.EngineModel)
        '        omnibusboxChannelsCollection.Query.Where(omnibusboxChannelsCollection.Query.LogReportOrderInCategorie > 0)
        '        omnibusboxChannelsCollection.Query.OrderBy(omnibusboxChannelsCollection.Query.LogReportCategorie.Ascending)
        '        omnibusboxChannelsCollection.Query.OrderBy(omnibusboxChannelsCollection.Query.LogReportOrderInCategorie.Ascending)

        '        canbusChannelsCollection.Query.Where(canbusChannelsCollection.Query.EngineModel = m_CELProject.EngineModel)
        '        canbusChannelsCollection.Query.Where(canbusChannelsCollection.Query.LogReportOrderInCategorie > 0)
        '        canbusChannelsCollection.Query.OrderBy(canbusChannelsCollection.Query.LogReportCategorie.Ascending)
        '        canbusChannelsCollection.Query.OrderBy(canbusChannelsCollection.Query.LogReportOrderInCategorie.Ascending)

        '        Dim sb As New StringBuilder

        '        'booleanChannelsCollection.Query.Load()
        '        booleanChannelsCollection.Load(boolChanQuery)
        '        For Each channel As HMIBooleanChannels In booleanChannelsCollection
        '            If dicChannelListLeft.Contains(channel.ChannelName) Then
        '                'dicChannelListLeft.Add(channel.ChannelName)
        '                'dicChannelList.Add(channel.ChannelName)
        '                dgvRight.Rows.Add(channel.LogReportCategorie, channel.LogReportOrderInCategorie, channel.ChannelName, channel.Description, "", "", "BOOLEAN")
        '            Else
        '                sb.AppendLine("The channel " + channel.ChannelName + " already exist in the collection")
        '            End If
        '        Next

        '        'numericChannelsCollection.Query.Load()
        '        numericChannelsCollection.Load(numChanQuery)
        '        For Each channel As HMINumericChannels In numericChannelsCollection
        '            If dicChannelListLeft.Contains(channel.ChannelName) Then
        '                'dgvLeft.Rows.Add(channel.CollectionName, channel.ChannelName, channel.Description, channel.UpToEngineeringUnitByEngUnitID, "NUMERIC")
        '                'dicChannelListLeft.Add(channel.ChannelName)
        '                'dicChannelList.Add(channel.ChannelName)
        '                Dim engUnit As String
        '                If IsNothing(channel.EngUnitID) Then
        '                    engUnit = String.Empty
        '                Else
        '                    engUnit = dicEngUnit(channel.EngUnitID)
        '                    If engUnit.Trim = "None" Then
        '                        engUnit = ""
        '                    End If
        '                End If
        '                dgvRight.Rows.Add(channel.LogReportCategorie, channel.LogReportOrderInCategorie, channel.ChannelName, channel.Description, engUnit, channel.Format, "NUMERIC")
        '            Else
        '                sb.AppendLine("The channel " + channel.ChannelName + " already exist in the collection")
        '            End If
        '        Next

        '        'textChannelsCollection.Query.Load()
        '        textChannelsCollection.Load(textChanQuery)
        '        For Each channel As HMITextChannels In textChannelsCollection
        '            If dicChannelListLeft.Contains(channel.ChannelName) Then
        '                'dicChannelListLeft.Add(channel.ChannelName)
        '                'dicChannelList.Add(channel.ChannelName)
        '                dgvRight.Rows.Add(channel.LogReportCategorie, channel.LogReportOrderInCategorie, channel.ChannelName, channel.Description, "", "", "TEXT")
        '            Else
        '                sb.AppendLine("The channel " + channel.ChannelName + " already exist in the collection")
        '            End If
        '        Next

        '        'arincChannelsCollection.Load(arincChanQuery)
        '        arincChannelsCollection.Query.Load()
        '        For Each channel As ARINC429LabelTemplate In arincChannelsCollection
        '            If dicChannelListLeft.Contains(channel.Name) Then
        '                'dicChannelListLeft.Add(channel.Name)
        '                'dicChannelList.Add(channel.Name)
        '                dgvRight.Rows.Add(channel.LogReportCategorie, channel.LogReportOrderInCategorie, channel.Name, channel.Description, channel.Units, channel.DisplayFormat, "ARINC")
        '            Else
        '                sb.AppendLine("The channel " + channel.Name + " already exist in the collection")
        '            End If
        '        Next

        '        'omnibusboxChannelsCollection.Load(omnibusboxChanQuery)
        '        omnibusboxChannelsCollection.Query.Load()
        '        For Each channel As OmniBusBoxUartTemplate In omnibusboxChannelsCollection
        '            If dicChannelListLeft.Contains(channel.Name) Then
        '                'dicChannelListLeft.Add(channel.Name)
        '                'dicChannelList.Add(channel.Name)
        '                dgvRight.Rows.Add(channel.LogReportCategorie, channel.LogReportOrderInCategorie, channel.Name, channel.Parameter, channel.Units, channel.Format, "UART")
        '            Else
        '                sb.AppendLine("The channel " + channel.Name + " already exist in the collection")
        '            End If
        '        Next

        '        canbusChannelsCollection.Query.Load()
        '        For Each channel As Dacn1CanbusLabel In canbusChannelsCollection
        '            If dicChannelListLeft.Contains(channel.ChannelName) Then
        '                'dicChannelListLeft.Add(channel.Name)
        '                'dicChannelList.Add(channel.Name)
        '                Dim engUnit As String
        '                If IsNothing(channel.EngUnitID) Then
        '                    engUnit = String.Empty
        '                Else
        '                    engUnit = dicEngUnit(channel.EngUnitID)
        '                    If engUnit.Trim = "None" Then
        '                        engUnit = ""
        '                    End If
        '                End If
        '                dgvRight.Rows.Add(channel.LogReportCategorie, channel.LogReportOrderInCategorie, channel.ChannelName, channel.Description, engUnit, channel.DisplayFormat, "CANBUS")
        '            Else
        '                sb.AppendLine("The channel " + channel.ChannelName + " already exist in the collection")
        '            End If
        '        Next

        '        If sb.Length > 0 Then
        '            MessageBox.Show(sb.ToString, "Invalide Duplicate Channel")
        '        End If





        '    End If

        '    Save()
        '    ' btnSave.Enabled = False

        'End Sub




End Class


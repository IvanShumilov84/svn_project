Imports EntitySpaces.DynamicQuery

Imports CEL.HMI
Imports CEL.Data.BusinessObjects

Public Class HmiNumericChannelForm

    Private m_CELProject As CELDAS

    Private numericChannelsCollection As New CEL.Data.BusinessObjects.HMINumericChannelsCollection

    Public Sub New(ByRef _CELProject As CELDAS)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_CELProject = _CELProject
    End Sub

    Private Sub HmiNumericChannelForm_Load(sender As Object, e As EventArgs) Handles Me.Load




        LoadEngineSelection()

    End Sub

    Private Sub LoadEngineSelection()

        Dim uutCollection As New UutCollection()
        uutCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        uutCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        uutCollection.Query.Where(uutCollection.Query.ProjectID = m_CELProject.Project.Number)
        uutCollection.Query.OrderBy(uutCollection.Query.EngineModel.Ascending)
        uutCollection.Query.Load()

        For Each row As Uut In uutCollection
            cboEngine.Items.Add(row.EngineModel)
        Next

    End Sub
    Private Sub cboEngine_TextChanged(sender As Object, e As EventArgs) Handles cboEngine.TextChanged

        m_CELProject.EngineModel = cboEngine.Text
        LoadDataGrid()

    End Sub

    Private Sub LoadDataGrid()

        dgvNumericChannels.AutoGenerateColumns = False

        Dim dynamicChannelCollection As New UUTDynamicChannelCollectionCollection()
        dynamicChannelCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
        dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.ProjectID = m_CELProject.Project.Number)
        dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.EngineModel = m_CELProject.EngineModel)
        dynamicChannelCollection.Query.OrderBy(dynamicChannelCollection.Query.LoadOrder.Ascending)
        dynamicChannelCollection.Query.Load()


        If dynamicChannelCollection.Count > 0 Then

            Dim query As New HMINumericChannelsQuery()
            query.es.DefaultConjunction = esConjunction.Or
            numericChannelsCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
            'numericChannelsCollection.es.DefaultConjunction = esConjunction.Or

            'Load All possible mapping for different collection name in a SPECIFIC ENGINE MODEL
            For Each record As UUTDynamicChannelCollection In dynamicChannelCollection

                query.Where(query.CollectionName = record.CollectionName)

            Next

            numericChannelsCollection.Load(query)
            HMINumericChannelsCollectionBindingSource.DataSource = numericChannelsCollection

        End If

    End Sub
End Class
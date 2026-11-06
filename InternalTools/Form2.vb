Imports CEL.Data.BusinessObjects

Imports EntitySpaces.Interfaces
Imports EntitySpaces.Loader

Imports System.Linq
Imports System.Configuration

Public Class Form2
    Dim engine As New CEL.Data.BusinessObjects.UutCollection

    'Private entities As HMI_Boolean_Channels = New HMI_Boolean_Channels
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim test = entities

        'DataGridView1.DataSource = test
        Dim connectionName As String = "ACCESSDynamic"

        'Dim enginesList As New BusinessObjects.UutCollection
        'enginesList.es.Connection.Name = connectionName
        'DataGridView1.AutoGenerateColumns = True
        'enginesList.LoadAll()
        DataGridView1.AutoGenerateColumns = False

        engine.es.Connection.Name = connectionName
        'engine.Query.Where(engine.Query.Model = "GTCP131-9A")
        engine.LoadAll()
        'DataGridView1.DataSource = engine(0).GetCollection
        'DataGridView1.DataSource = engine
        UutBindingSource.DataSource = engine
       
        'test.es.Connection.Name = connectionName
        'DataGridView1.AutoGenerateColumns = True
        ''test.Query.Select(test.Query.Dacn)
        'test.Query.Where(test.Query.Dacnid > 5)
        '' DACN.Query.OrderBy(DACN.Query.Dacn, EntitySpaces.DynamicQuery.esOrderByDirection.Ascending)
        'test.Query.Load()
        '' Bind to our EntitySpaces Collection
        'DataGridView1.DataSource = test



    End Sub
    Public Shared Function GetConnectionString(ByVal strConnection As String) As String

        'Declare a string to hold the connection string  

        Dim sReturn As New String("")

        'Check to see if they provided a connection string name  

        If Not String.IsNullOrEmpty(strConnection) Then

            'Retrieve the connection string fromt he app.config  

            sReturn = ConfigurationManager.ConnectionStrings(strConnection).ConnectionString

        Else

            'Since they didnt provide the name of the connection string  

            'just grab the default on from app.config  

            sReturn = ConfigurationManager.ConnectionStrings("YourConnectionString").ConnectionString()

        End If

        'Return the connection string to the calling method  

        Return sReturn

    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        '' Add any initialization after the InitializeComponent() call.
        esProviderFactory.Factory = _
            New EntitySpaces.LoaderMT.esDataProviderFactory()

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataGridView1.EndEdit()
        'test.Save()
        Try
            engine.Save()
            engine.AcceptChanges()
            engine.LoadAll()
        Catch
            engine.RejectChanges()
        End Try

    End Sub

   
   
End Class
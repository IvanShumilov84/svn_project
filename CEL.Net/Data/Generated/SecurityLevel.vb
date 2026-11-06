
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 05/02/2014 8:02:01 AM
'===============================================================================

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text
Imports System.Linq
Imports System.Data
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery



Namespace BusinessObjects

    ' <summary>
    ' Encapsulates the 'Security Level' table
    ' </summary>

    <System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _
    <Serializable> _
    <DataContract> _
    <KnownType(GetType(SecurityLevel))> _
    <XmlType("SecurityLevel")> _
    Partial Public Class SecurityLevel
        Inherits esSecurityLevel

        <DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _
        Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
            Get
                Return MyBase.Debug
            End Get
        End Property

        Public Overrides Function CreateInstance() As esEntity
            Return New SecurityLevel()
        End Function

#Region "Static Quick Access Methods"
        Public Shared Sub Delete(ByVal securityLevelID As System.Int32)
            Dim obj As New SecurityLevel()
            obj.SecurityLevelID = securityLevelID
            obj.AcceptChanges()
            obj.MarkAsDeleted()
            obj.Save()
        End Sub

        Public Shared Sub Delete(ByVal securityLevelID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
            Dim obj As New SecurityLevel()
            obj.SecurityLevelID = securityLevelID
            obj.AcceptChanges()
            obj.MarkAsDeleted()
            obj.Save(sqlAccessType)
        End Sub
#End Region



    End Class



    <DebuggerDisplay("Count = {Count}")> _
    <Serializable> _
    <CollectionDataContract> _
    <XmlType("SecurityLevelCollection")> _
    Partial Public Class SecurityLevelCollection
        Inherits esSecurityLevelCollection
        Implements IEnumerable(Of SecurityLevel)

        Public Function FindByPrimaryKey(ByVal securityLevelID As System.Int32) As SecurityLevel
            Return MyBase.SingleOrDefault(Function(e) e.SecurityLevelID.HasValue AndAlso e.SecurityLevelID.Value = securityLevelID)
        End Function



#Region "WCF Service Class"

        <DataContract> _
        <KnownType(GetType(SecurityLevel))> _
        Public Class SecurityLevelCollectionWCFPacket
            Inherits esCollectionWCFPacket(Of SecurityLevelCollection)

            Public Shared Widening Operator CType(packet As SecurityLevelCollectionWCFPacket) As SecurityLevelCollection
                Return packet.Collection
            End Operator

            Public Shared Widening Operator CType(collection As SecurityLevelCollection) As SecurityLevelCollectionWCFPacket
                Return New SecurityLevelCollectionWCFPacket() With {.Collection = collection}
            End Operator

        End Class

#End Region



    End Class




    <DebuggerDisplay("Query = {Parse()}")> _
    <Serializable> _
    Partial Public Class SecurityLevelQuery
        Inherits esSecurityLevelQuery

        Public Sub New(ByVal joinAlias As String)
            Me.es.JoinAlias = joinAlias
        End Sub

        Protected Overrides Function GetQueryName() As String
            Return "SecurityLevelQuery"
        End Function

#Region "Explicit Casts"

        Public Shared Narrowing Operator CType(ByVal query As SecurityLevelQuery) As String
            Return SecurityLevelQuery.SerializeHelper.ToXml(query)
        End Operator

        Public Shared Narrowing Operator CType(ByVal query As String) As SecurityLevelQuery
            Return DirectCast(SecurityLevelQuery.SerializeHelper.FromXml(query, GetType(SecurityLevelQuery)), SecurityLevelQuery)
        End Operator

#End Region

    End Class


    <DataContract> _
    <Serializable()> _
    Partial Public MustInherit Class esSecurityLevel
        Inherits esEntity
        Implements INotifyPropertyChanged

        Public Sub New()

        End Sub

#Region "LoadByPrimaryKey"
        Public Overridable Function LoadByPrimaryKey(ByVal securityLevelID As System.Int32) As Boolean

            If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL Then
                Return LoadByPrimaryKeyDynamic(securityLevelID)
            Else
                Return LoadByPrimaryKeyStoredProcedure(securityLevelID)
            End If

        End Function

        Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal securityLevelID As System.Int32) As Boolean

            If sqlAccessType = esSqlAccessType.DynamicSQL Then
                Return LoadByPrimaryKeyDynamic(securityLevelID)
            Else
                Return LoadByPrimaryKeyStoredProcedure(securityLevelID)
            End If

        End Function

        Private Function LoadByPrimaryKeyDynamic(ByVal securityLevelID As System.Int32) As Boolean

            Dim query As New SecurityLevelQuery()
            query.Where(query.SecurityLevelID = securityLevelID)
            Return Me.Load(query)

        End Function

        Private Function LoadByPrimaryKeyStoredProcedure(ByVal securityLevelID As System.Int32) As Boolean

            Dim parms As esParameters = New esParameters()
            parms.Add("SecurityLevelID", securityLevelID)

            Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)

        End Function
#End Region

#Region "Properties"



        ' <summary>
        ' Maps to Security Level.SecurityLevelID
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property SecurityLevelID As Nullable(Of System.Int32)
            Get
                Return MyBase.GetSystemInt32(SecurityLevelMetadata.ColumnNames.SecurityLevelID)
            End Get

            Set(ByVal value As Nullable(Of System.Int32))
                If MyBase.SetSystemInt32(SecurityLevelMetadata.ColumnNames.SecurityLevelID, value) Then
                    OnPropertyChanged(SecurityLevelMetadata.PropertyNames.SecurityLevelID)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Security Level.Description
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property Description As System.String
            Get
                Return MyBase.GetSystemString(SecurityLevelMetadata.ColumnNames.Description)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(SecurityLevelMetadata.ColumnNames.Description, value) Then
                    OnPropertyChanged(SecurityLevelMetadata.PropertyNames.Description)
                End If
            End Set
        End Property

#End Region

#Region ".str() Properties"

        Public Overrides Sub SetProperties(values As IDictionary)

            Dim propertyName As String
            For Each propertyName In values.Keys
                Me.SetProperty(propertyName, values(propertyName))
            Next

        End Sub

        Public Overrides Sub SetProperty(name As String, value As Object)

            Dim col As esColumnMetadata = Me.Meta.Columns.FindByPropertyName(name)
            If Not col Is Nothing Then

                If value Is Nothing OrElse value.GetType().ToString() = "System.String" Then

                    ' Use the strongly typed property
                    Select Case name

                        Case "SecurityLevelID"
                            Me.str().SecurityLevelID = CType(value, String)

                        Case "Description"
                            Me.str().Description = CType(value, String)

                    End Select

                Else

                    Select Case name

                        Case "SecurityLevelID"

                            If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
                                Me.SecurityLevelID = CType(value, Nullable(Of System.Int32))
                                OnPropertyChanged(SecurityLevelMetadata.PropertyNames.SecurityLevelID)
                            End If


                        Case Else

                    End Select
                End If

            ElseIf Me.ContainsColumn(name) Then
                Me.SetColumn(name, value)
            Else
                Throw New Exception("SetProperty Error: '" + name + "' not found")
            End If

        End Sub

        Public Function str() As esStrings

            If _esstrings Is Nothing Then
                _esstrings = New esStrings(Me)
            End If
            Return _esstrings

        End Function

        Public NotInheritable Class esStrings

            Public Sub New(ByVal entity As esSecurityLevel)
                Me.entity = entity
            End Sub


            Public Property SecurityLevelID As System.String
                Get
                    Dim data_ As Nullable(Of System.Int32) = entity.SecurityLevelID

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.SecurityLevelID = Nothing
                    Else
                        entity.SecurityLevelID = Convert.ToInt32(Value)
                    End If
                End Set
            End Property

            Public Property Description As System.String
                Get
                    Dim data_ As System.String = entity.Description

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.Description = Nothing
                    Else
                        entity.Description = Convert.ToString(Value)
                    End If
                End Set
            End Property


            Private entity As esSecurityLevel
        End Class


        <NonSerialized> _
        <IgnoreDataMember> _
              Private _esstrings As esStrings

#End Region

#Region "Housekeeping methods"

        Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return SecurityLevelMetadata.Meta()
            End Get
        End Property

#End Region

#Region "Query Logic"

        Public ReadOnly Property Query() As SecurityLevelQuery
            Get
                If Me.m_query Is Nothing Then
                    Me.m_query = New SecurityLevelQuery()
                    InitQuery(Me.m_query)
                End If

                Return Me.m_query
            End Get
        End Property

        Public Overloads Function Load(ByVal query As SecurityLevelQuery) As Boolean
            Me.m_query = query
            InitQuery(Me.m_query)
            Return Me.Query.Load()
        End Function

        Protected Sub InitQuery(ByVal query As SecurityLevelQuery)
            query.OnLoadDelegate = AddressOf OnQueryLoaded

            If Not query.es2.HasConnection Then
                query.es2.Connection = DirectCast(Me, IEntity).Connection
            End If
        End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As SecurityLevelQuery

    End Class



    <Serializable()> _
    Partial Public MustInherit Class esSecurityLevelCollection
        Inherits esEntityCollection(Of SecurityLevel)

#Region "Housekeeping methods"
        Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return SecurityLevelMetadata.Meta()
            End Get
        End Property

        Protected Overloads Overrides Function GetCollectionName() As String
            Return "SecurityLevelCollection"
        End Function

#End Region

#Region "Query Logic"


        <BrowsableAttribute(False)> _
        Public ReadOnly Property Query() As SecurityLevelQuery
            Get
                If Me.m_query Is Nothing Then
                    Me.m_query = New SecurityLevelQuery()
                    InitQuery(Me.m_query)
                End If

                Return Me.m_query
            End Get
        End Property

        Public Overloads Function Load(ByVal query As SecurityLevelQuery) As Boolean
            Me.m_query = query
            InitQuery(Me.m_query)
            Return Query.Load()
        End Function

        Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
            If Me.m_query Is Nothing Then
                Me.m_query = New SecurityLevelQuery()
                Me.InitQuery(m_query)
            End If
            Return Me.m_query
        End Function

        Protected Sub InitQuery(ByVal query As SecurityLevelQuery)
            query.OnLoadDelegate = AddressOf OnQueryLoaded

            If Not query.es2.HasConnection Then
                query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
            End If
        End Sub

        Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
            Me.InitQuery(DirectCast(query, SecurityLevelQuery))
        End Sub

#End Region

        Private m_query As SecurityLevelQuery
    End Class



    <Serializable> _
    Partial Public MustInherit Class esSecurityLevelQuery
        Inherits esDynamicQuery

        Protected Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return SecurityLevelMetadata.Meta()
            End Get
        End Property

#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
                Case "SecurityLevelID"
                    Return Me.SecurityLevelID
                Case "Description"
                    Return Me.Description
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region

#Region "esQueryItems"


        Public ReadOnly Property SecurityLevelID As esQueryItem
            Get
                Return New esQueryItem(Me, SecurityLevelMetadata.ColumnNames.SecurityLevelID, esSystemType.Int32)
            End Get
        End Property

        Public ReadOnly Property Description As esQueryItem
            Get
                Return New esQueryItem(Me, SecurityLevelMetadata.ColumnNames.Description, esSystemType.String)
            End Get
        End Property

#End Region

    End Class



    Partial Public Class SecurityLevel
        Inherits esSecurityLevel


#Region "UsersCollectionBySecurityLevelID - Zero To Many"

        Public Shared ReadOnly Property Prefetch_UsersCollectionBySecurityLevelID() As esPrefetchMap
            Get
                Dim map As New esPrefetchMap()
                map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.SecurityLevel.UsersCollectionBySecurityLevelID_Delegate)
                map.PropertyName = "UsersCollectionBySecurityLevelID"
                map.MyColumnName = "SecurityLevelID"
                map.ParentColumnName = "SecurityLevelID"
                map.IsMultiPartKey = False
                Return map
            End Get
        End Property

        Private Shared Sub UsersCollectionBySecurityLevelID_Delegate(ByVal data As esPrefetchParameters)

            Dim parent As New SecurityLevelQuery(data.NextAlias())

            Dim mee As UsersQuery = If(data.You IsNot Nothing, TryCast(data.You, UsersQuery), New UsersQuery(data.NextAlias()))

            If data.Root Is Nothing Then
                data.Root = mee
            End If

            data.Root.InnerJoin(parent).On(parent.SecurityLevelID = mee.SecurityLevelID)

            data.You = parent

        End Sub

        ''' <summary>
        ''' Zero to Many
        ''' Foreign Key Name - Security LevelUsers
        ''' </summary>

        <XmlIgnore()> _
        Public Property UsersCollectionBySecurityLevelID As UsersCollection

            Get
                If Me._UsersCollectionBySecurityLevelID Is Nothing Then
                    Me._UsersCollectionBySecurityLevelID = New UsersCollection()
                    Me._UsersCollectionBySecurityLevelID.es.Connection.Name = Me.es.Connection.Name
                    Me.SetPostSave("UsersCollectionBySecurityLevelID", Me._UsersCollectionBySecurityLevelID)

                    If Not Me.SecurityLevelID.Equals(Nothing) Then

                        If Not Me.es.IsLazyLoadDisabled Then

                            Me._UsersCollectionBySecurityLevelID.Query.Where(Me._UsersCollectionBySecurityLevelID.Query.SecurityLevelID = Me.SecurityLevelID)
                            Me._UsersCollectionBySecurityLevelID.Query.Load()
                        End If

                        ' Auto-hookup Foreign Keys
                        Me._UsersCollectionBySecurityLevelID.fks.Add(UsersMetadata.ColumnNames.SecurityLevelID, Me.SecurityLevelID)
                    End If
                End If

                Return Me._UsersCollectionBySecurityLevelID
            End Get

            Set(ByVal value As UsersCollection)
                If Not value Is Nothing Then Throw New Exception("'value' Must be null")

                If Not Me._UsersCollectionBySecurityLevelID Is Nothing Then

                    Me.RemovePostSave("UsersCollectionBySecurityLevelID")
                    Me._UsersCollectionBySecurityLevelID = Nothing


                End If
            End Set

        End Property


        Private _UsersCollectionBySecurityLevelID As UsersCollection
#End Region




        Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
            Dim coll As esEntityCollectionBase = Nothing

            Select Case name

                Case "UsersCollectionBySecurityLevelID"
                    coll = Me.UsersCollectionBySecurityLevelID
                    Exit Select
            End Select

            Return coll
        End Function

        ''' <summary>
        ''' Used internally by the entity's hierarchical properties.
        ''' </summary>
        Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)

            Dim props As New List(Of esPropertyDescriptor)
            props.Add(New esPropertyDescriptor(Me, "UsersCollectionBySecurityLevelID", GetType(UsersCollection), New Users()))
            Return props

        End Function

        ''' <summary>
        ''' Called by ApplyPostSaveKeys 
        ''' </summary>
        ''' <param name="coll">The collection to enumerate over</param>
        ''' <param name="key">"The column name</param>
        ''' <param name="value">The column value</param>
        Private Sub Apply(coll As esEntityCollectionBase, key As String, value As Object)
            For Each obj As esEntity In coll
                If obj.es.IsAdded Then
                    obj.SetProperty(key, value)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Used internally for retrieving AutoIncrementing keys
        ''' during hierarchical PostSave.
        ''' </summary>
        Protected Overrides Sub ApplyPostSaveKeys()

            If Not Me._UsersCollectionBySecurityLevelID Is Nothing Then
                Apply(Me._UsersCollectionBySecurityLevelID, "SecurityLevelID", Me.SecurityLevelID)
            End If

        End Sub
    End Class




    <Serializable> _
    Partial Public Class SecurityLevelMetadata
        Inherits esMetadata
        Implements IMetadata

#Region "Protected Constructor"
        Protected Sub New()
            m_columns = New esColumnMetadataCollection()
            Dim c As esColumnMetadata

            c = New esColumnMetadata(SecurityLevelMetadata.ColumnNames.SecurityLevelID, 0, GetType(System.Int32), esSystemType.Int32)
            c.PropertyName = SecurityLevelMetadata.PropertyNames.SecurityLevelID
            c.IsInPrimaryKey = True
            c.IsAutoIncrement = True
            c.NumericPrecision = 10
            m_columns.Add(c)

            c = New esColumnMetadata(SecurityLevelMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)
            c.PropertyName = SecurityLevelMetadata.PropertyNames.Description
            c.CharacterMaxLength = 50
            c.IsNullable = True
            m_columns.Add(c)

        End Sub
#End Region

        Public Shared Function Meta() As SecurityLevelMetadata
            Return _meta
        End Function

        Public ReadOnly Property DataID() As System.Guid Implements IMetadata.DataID
            Get
                Return MyBase.m_dataID
            End Get
        End Property

        Public ReadOnly Property MultiProviderMode() As Boolean Implements IMetadata.MultiProviderMode
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property Columns() As esColumnMetadataCollection Implements IMetadata.Columns
            Get
                Return MyBase.m_columns
            End Get
        End Property

#Region "ColumnNames"
        Public Class ColumnNames
            Public Const SecurityLevelID As String = "SecurityLevelID"
            Public Const Description As String = "Description"
        End Class
#End Region

#Region "PropertyNames"
        Public Class PropertyNames
            Public Const SecurityLevelID As String = "SecurityLevelID"
            Public Const Description As String = "Description"
        End Class
#End Region

        Public Function GetProviderMetadata(ByVal mapName As String) As esProviderSpecificMetadata _
            Implements IMetadata.GetProviderMetadata

            Dim mapMethod As MapToMeta = mapDelegates(mapName)

            If (Not mapMethod = Nothing) Then
                Return mapMethod(mapName)
            Else
                Return Nothing
            End If

        End Function

#Region "MAP esDefault"

        Private Shared Function RegisterDelegateesDefault() As Integer

            ' This is only executed once per the life of the application
            SyncLock GetType(SecurityLevelMetadata)

                If SecurityLevelMetadata.mapDelegates Is Nothing Then
                    SecurityLevelMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
                End If

                If SecurityLevelMetadata._meta Is Nothing Then
                    SecurityLevelMetadata._meta = New SecurityLevelMetadata()
                End If

                Dim mapMethod As New MapToMeta(AddressOf _meta.esDefault)
                mapDelegates.Add("esDefault", mapMethod)
                mapMethod("esDefault")
                Return 0

            End SyncLock

        End Function

        Private Function esDefault(ByVal mapName As String) As esProviderSpecificMetadata

            If (Not m_providerMetadataMaps.ContainsKey(mapName)) Then

                Dim meta As esProviderSpecificMetadata = New esProviderSpecificMetadata()



                meta.AddTypeMap("SecurityLevelID", New esTypeMap("Long", "System.Int32"))
                meta.AddTypeMap("Description", New esTypeMap("Text", "System.String"))



                meta.Source = "Security Level"
                meta.Destination = "Security Level"

                meta.spInsert = "proc_Security LevelInsert"
                meta.spUpdate = "proc_Security LevelUpdate"
                meta.spDelete = "proc_Security LevelDelete"
                meta.spLoadAll = "proc_Security LevelLoadAll"
                meta.spLoadByPrimaryKey = "proc_Security LevelLoadByPrimaryKey"

                Me.m_providerMetadataMaps.Add("esDefault", meta)

            End If

            Return Me.m_providerMetadataMaps("esDefault")

        End Function

#End Region

        Private Shared _meta As SecurityLevelMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
        Private Shared _esDefault As Integer = RegisterDelegateesDefault()

    End Class

End Namespace

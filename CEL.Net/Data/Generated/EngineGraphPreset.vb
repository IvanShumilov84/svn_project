
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 1/29/2016 11:31:46 AM
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
	' Encapsulates the 'EngineGraphPreset' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(EngineGraphPreset))> _
	<XmlType("EngineGraphPreset")> _	
	Partial Public Class EngineGraphPreset 
		Inherits esEngineGraphPreset
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New EngineGraphPreset()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32)
			Dim obj As New EngineGraphPreset()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New EngineGraphPreset()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("EngineGraphPresetCollection")> _
	Partial Public Class EngineGraphPresetCollection
		Inherits esEngineGraphPresetCollection
		Implements IEnumerable(Of EngineGraphPreset)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32) As EngineGraphPreset
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(EngineGraphPreset))> _
		Public Class EngineGraphPresetCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of EngineGraphPresetCollection)
			
			Public Shared Widening Operator CType(packet As EngineGraphPresetCollectionWCFPacket) As EngineGraphPresetCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As EngineGraphPresetCollection) As EngineGraphPresetCollectionWCFPacket
				Return New EngineGraphPresetCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class EngineGraphPresetQuery 
		Inherits esEngineGraphPresetQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "EngineGraphPresetQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As EngineGraphPresetQuery) As String
			Return EngineGraphPresetQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As EngineGraphPresetQuery
			Return DirectCast(EngineGraphPresetQuery.SerializeHelper.FromXml(query, GetType(EngineGraphPresetQuery)), EngineGraphPresetQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esEngineGraphPreset
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal id As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(id)
			Else
				Return LoadByPrimaryKeyStoredProcedure(id)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal id As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(id)
			Else
				Return LoadByPrimaryKeyStoredProcedure(id)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal id As System.Int32) As Boolean
		
			Dim query As New EngineGraphPresetQuery()
			query.Where(query.Id = id)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal id As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("Id", id)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to EngineGraphPreset.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(EngineGraphPresetMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(EngineGraphPresetMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(EngineGraphPresetMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to EngineGraphPreset.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(EngineGraphPresetMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EngineGraphPresetMetadata.ColumnNames.EngineModel, value) Then
					OnPropertyChanged(EngineGraphPresetMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to EngineGraphPreset.PresetName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PresetName As System.String
			Get
				Return MyBase.GetSystemString(EngineGraphPresetMetadata.ColumnNames.PresetName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EngineGraphPresetMetadata.ColumnNames.PresetName, value) Then
					OnPropertyChanged(EngineGraphPresetMetadata.PropertyNames.PresetName)
				End If
			End Set
		End Property	
		
#End Region	

#Region ".str() Properties"

		Public Overrides Sub SetProperties(values as IDictionary)

		Dim propertyName As String
			For Each propertyName In values.Keys
				Me.SetProperty(propertyName, values(propertyName))
			Next

		End Sub

		Public Overrides Sub SetProperty(name as string, value as object)

			Dim col As esColumnMetadata = Me.Meta.Columns.FindByPropertyName(name)
			If Not col Is Nothing Then

				If value Is Nothing OrElse value.GetType().ToString() = "System.String" Then

					' Use the strongly typed property
					Select Case name
												
						Case "Id"
							Me.str().Id = CType(value, string)
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
						Case "PresetName"
							Me.str().PresetName = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(EngineGraphPresetMetadata.PropertyNames.Id)
							End If
						
					
						Case Else
						
					End Select
				End If

			Else If Me.ContainsColumn(name) Then
				Me.SetColumn(name, value)
			Else
				throw New Exception("SetProperty Error: '" + name + "' not found")
			End If	

		End Sub

		Public Function str() As esStrings
		
			If _esstrings Is Nothing Then
				_esstrings = New esStrings(Me)
			End If
			Return _esstrings
			
		End Function

		NotInheritable Public Class esStrings
		
			Public Sub New(ByVal entity As esEngineGraphPreset)
				Me.entity = entity
			End Sub				
		
	
			Public Property Id As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Id
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Id = Nothing
					Else
						entity.Id = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property EngineModel As System.String 
				Get
					Dim data_ As System.String = entity.EngineModel
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EngineModel = Nothing
					Else
						entity.EngineModel = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property PresetName As System.String 
				Get
					Dim data_ As System.String = entity.PresetName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PresetName = Nothing
					Else
						entity.PresetName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esEngineGraphPreset
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return EngineGraphPresetMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As EngineGraphPresetQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New EngineGraphPresetQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As EngineGraphPresetQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As EngineGraphPresetQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As EngineGraphPresetQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esEngineGraphPresetCollection
		Inherits esEntityCollection(Of EngineGraphPreset)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return EngineGraphPresetMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "EngineGraphPresetCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As EngineGraphPresetQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New EngineGraphPresetQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As EngineGraphPresetQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New EngineGraphPresetQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As EngineGraphPresetQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, EngineGraphPresetQuery))
		End Sub
		
		#End Region
						
		Private m_query As EngineGraphPresetQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esEngineGraphPresetQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return EngineGraphPresetMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "EngineModel" 
					Return Me.EngineModel
				Case "PresetName" 
					Return Me.PresetName
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, EngineGraphPresetMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, EngineGraphPresetMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property PresetName As esQueryItem
			Get
				Return New esQueryItem(Me, EngineGraphPresetMetadata.ColumnNames.PresetName, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class EngineGraphPreset 
		Inherits esEngineGraphPreset
		
	
		#Region "GraphAxisCollectionByGid - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_GraphAxisCollectionByGid() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.EngineGraphPreset.GraphAxisCollectionByGid_Delegate)
				map.PropertyName = "GraphAxisCollectionByGid"
				map.MyColumnName = "GID"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub GraphAxisCollectionByGid_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New EngineGraphPresetQuery(data.NextAlias())
			
			Dim mee As GraphAxisQuery = If(data.You IsNot Nothing, TryCast(data.You, GraphAxisQuery), New GraphAxisQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.Gid)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - EngineGraphPresetGraphAxis
		''' </summary>

		<XmlIgnore()> _ 
		Public Property GraphAxisCollectionByGid As GraphAxisCollection 
		
			Get
				If Me._GraphAxisCollectionByGid Is Nothing Then
					Me._GraphAxisCollectionByGid = New GraphAxisCollection()
					Me._GraphAxisCollectionByGid.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("GraphAxisCollectionByGid", Me._GraphAxisCollectionByGid)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._GraphAxisCollectionByGid.Query.Where(Me._GraphAxisCollectionByGid.Query.Gid = Me.Id)
							Me._GraphAxisCollectionByGid.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._GraphAxisCollectionByGid.fks.Add(GraphAxisMetadata.ColumnNames.Gid, Me.Id)
					End If
				End If

				Return Me._GraphAxisCollectionByGid
			End Get
			
			Set(ByVal value As GraphAxisCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._GraphAxisCollectionByGid Is Nothing Then

					Me.RemovePostSave("GraphAxisCollectionByGid")
					Me._GraphAxisCollectionByGid = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _GraphAxisCollectionByGid As GraphAxisCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "GraphAxisCollectionByGid"
					coll = Me.GraphAxisCollectionByGid
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "GraphAxisCollectionByGid", GetType(GraphAxisCollection), New GraphAxis()))
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
		
			If Not Me._GraphAxisCollectionByGid Is Nothing Then
				Apply(Me._GraphAxisCollectionByGid, "Gid", Me.Id)
			End If
			
		End Sub
	End Class
		



	<Serializable> _
	Partial Public Class EngineGraphPresetMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(EngineGraphPresetMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = EngineGraphPresetMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.IsAutoIncrement = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(EngineGraphPresetMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = EngineGraphPresetMetadata.PropertyNames.EngineModel
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
			c = New esColumnMetadata(EngineGraphPresetMetadata.ColumnNames.PresetName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = EngineGraphPresetMetadata.PropertyNames.PresetName
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As EngineGraphPresetMetadata
			Return _meta
		End Function
		
		Public ReadOnly Property DataID() As System.Guid Implements IMetadata.DataID
			Get
				Return MyBase.m_dataID
			End Get
		End Property

		Public ReadOnly Property MultiProviderMode() As Boolean Implements IMetadata.MultiProviderMode
			Get
				Return false
			End Get
		End Property

		Public ReadOnly Property Columns() As esColumnMetadataCollection Implements IMetadata.Columns
			Get
				Return MyBase.m_columns
			End Get
		End Property

#Region "ColumnNames"
		Public Class ColumnNames
			 Public Const Id As String = "ID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const PresetName As String = "PresetName"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const PresetName As String = "PresetName"
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
			SyncLock GetType(EngineGraphPresetMetadata)
			
				If EngineGraphPresetMetadata.mapDelegates Is Nothing Then
					EngineGraphPresetMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If EngineGraphPresetMetadata._meta Is Nothing Then
					EngineGraphPresetMetadata._meta = New EngineGraphPresetMetadata()
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
				


				meta.AddTypeMap("Id", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EngineModel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("PresetName", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "EngineGraphPreset"
				meta.Destination = "EngineGraphPreset"
				
				meta.spInsert = "proc_EngineGraphPresetInsert"
				meta.spUpdate = "proc_EngineGraphPresetUpdate"
				meta.spDelete = "proc_EngineGraphPresetDelete"
				meta.spLoadAll = "proc_EngineGraphPresetLoadAll"
				meta.spLoadByPrimaryKey = "proc_EngineGraphPresetLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As EngineGraphPresetMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

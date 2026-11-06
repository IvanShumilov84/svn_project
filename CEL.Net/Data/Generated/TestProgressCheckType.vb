
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 27/06/2013 09:49:00
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
	' Encapsulates the 'TestProgressCheckType' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(TestProgressCheckType))> _
	<XmlType("TestProgressCheckType")> _	
	Partial Public Class TestProgressCheckType 
		Inherits esTestProgressCheckType
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New TestProgressCheckType()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal checkTypeID As System.Int32)
			Dim obj As New TestProgressCheckType()
			obj.CheckTypeID = checkTypeID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal checkTypeID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New TestProgressCheckType()
			obj.CheckTypeID = checkTypeID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("TestProgressCheckTypeCollection")> _
	Partial Public Class TestProgressCheckTypeCollection
		Inherits esTestProgressCheckTypeCollection
		Implements IEnumerable(Of TestProgressCheckType)
	
		Public Function FindByPrimaryKey(ByVal checkTypeID As System.Int32) As TestProgressCheckType
			Return MyBase.SingleOrDefault(Function(e) e.CheckTypeID.HasValue AndAlso e.CheckTypeID.Value = checkTypeID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(TestProgressCheckType))> _
		Public Class TestProgressCheckTypeCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of TestProgressCheckTypeCollection)
			
			Public Shared Widening Operator CType(packet As TestProgressCheckTypeCollectionWCFPacket) As TestProgressCheckTypeCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As TestProgressCheckTypeCollection) As TestProgressCheckTypeCollectionWCFPacket
				Return New TestProgressCheckTypeCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class TestProgressCheckTypeQuery 
		Inherits esTestProgressCheckTypeQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "TestProgressCheckTypeQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As TestProgressCheckTypeQuery) As String
			Return TestProgressCheckTypeQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As TestProgressCheckTypeQuery
			Return DirectCast(TestProgressCheckTypeQuery.SerializeHelper.FromXml(query, GetType(TestProgressCheckTypeQuery)), TestProgressCheckTypeQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esTestProgressCheckType
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal checkTypeID As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(checkTypeID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(checkTypeID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal checkTypeID As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(checkTypeID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(checkTypeID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal checkTypeID As System.Int32) As Boolean
		
			Dim query As New TestProgressCheckTypeQuery()
			query.Where(query.CheckTypeID = checkTypeID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal checkTypeID As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("CheckTypeID", checkTypeID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to TestProgressCheckType.CheckTypeID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CheckTypeID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(TestProgressCheckTypeMetadata.ColumnNames.CheckTypeID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(TestProgressCheckTypeMetadata.ColumnNames.CheckTypeID, value) Then
					OnPropertyChanged(TestProgressCheckTypeMetadata.PropertyNames.CheckTypeID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressCheckType.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(TestProgressCheckTypeMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(TestProgressCheckTypeMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(TestProgressCheckTypeMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressCheckType.RequireChannel?
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RequireChannel As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(TestProgressCheckTypeMetadata.ColumnNames.RequireChannel)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(TestProgressCheckTypeMetadata.ColumnNames.RequireChannel, value) Then
					OnPropertyChanged(TestProgressCheckTypeMetadata.PropertyNames.RequireChannel)
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
												
						Case "CheckTypeID"
							Me.str().CheckTypeID = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "RequireChannel"
							Me.str().RequireChannel = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "CheckTypeID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.CheckTypeID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(TestProgressCheckTypeMetadata.PropertyNames.CheckTypeID)
							End If
						
						Case "RequireChannel"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.RequireChannel = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(TestProgressCheckTypeMetadata.PropertyNames.RequireChannel)
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
		
			Public Sub New(ByVal entity As esTestProgressCheckType)
				Me.entity = entity
			End Sub				
		
	
			Public Property CheckTypeID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.CheckTypeID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CheckTypeID = Nothing
					Else
						entity.CheckTypeID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Description As System.String 
				Get
					Dim data_ As System.String = entity.Description
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Description = Nothing
					Else
						entity.Description = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property RequireChannel As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.RequireChannel
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RequireChannel = Nothing
					Else
						entity.RequireChannel = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  

			Private entity As esTestProgressCheckType
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return TestProgressCheckTypeMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As TestProgressCheckTypeQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New TestProgressCheckTypeQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As TestProgressCheckTypeQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As TestProgressCheckTypeQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As TestProgressCheckTypeQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esTestProgressCheckTypeCollection
		Inherits esEntityCollection(Of TestProgressCheckType)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return TestProgressCheckTypeMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "TestProgressCheckTypeCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As TestProgressCheckTypeQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New TestProgressCheckTypeQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As TestProgressCheckTypeQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New TestProgressCheckTypeQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As TestProgressCheckTypeQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, TestProgressCheckTypeQuery))
		End Sub
		
		#End Region
						
		Private m_query As TestProgressCheckTypeQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esTestProgressCheckTypeQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return TestProgressCheckTypeMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "CheckTypeID" 
					Return Me.CheckTypeID
				Case "Description" 
					Return Me.Description
				Case "RequireChannel" 
					Return Me.RequireChannel
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CheckTypeID As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressCheckTypeMetadata.ColumnNames.CheckTypeID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressCheckTypeMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property RequireChannel As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressCheckTypeMetadata.ColumnNames.RequireChannel, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class TestProgressCheckType 
		Inherits esTestProgressCheckType
		
	
		#Region "TestProgressCheckListItemCollectionByCheckTypeID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_TestProgressCheckListItemCollectionByCheckTypeID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.TestProgressCheckType.TestProgressCheckListItemCollectionByCheckTypeID_Delegate)
				map.PropertyName = "TestProgressCheckListItemCollectionByCheckTypeID"
				map.MyColumnName = "CheckTypeID"
				map.ParentColumnName = "CheckTypeID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub TestProgressCheckListItemCollectionByCheckTypeID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New TestProgressCheckTypeQuery(data.NextAlias())
			
			Dim mee As TestProgressCheckListItemQuery = If(data.You IsNot Nothing, TryCast(data.You, TestProgressCheckListItemQuery), New TestProgressCheckListItemQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CheckTypeID = mee.CheckTypeID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - TestProgressCheckTypeTestProgressCheckListItem
		''' </summary>

		<XmlIgnore()> _ 
		Public Property TestProgressCheckListItemCollectionByCheckTypeID As TestProgressCheckListItemCollection 
		
			Get
				If Me._TestProgressCheckListItemCollectionByCheckTypeID Is Nothing Then
					Me._TestProgressCheckListItemCollectionByCheckTypeID = New TestProgressCheckListItemCollection()
					Me._TestProgressCheckListItemCollectionByCheckTypeID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("TestProgressCheckListItemCollectionByCheckTypeID", Me._TestProgressCheckListItemCollectionByCheckTypeID)
				
					If Not Me.CheckTypeID.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._TestProgressCheckListItemCollectionByCheckTypeID.Query.Where(Me._TestProgressCheckListItemCollectionByCheckTypeID.Query.CheckTypeID = Me.CheckTypeID)
							Me._TestProgressCheckListItemCollectionByCheckTypeID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._TestProgressCheckListItemCollectionByCheckTypeID.fks.Add(TestProgressCheckListItemMetadata.ColumnNames.CheckTypeID, Me.CheckTypeID)
					End If
				End If

				Return Me._TestProgressCheckListItemCollectionByCheckTypeID
			End Get
			
			Set(ByVal value As TestProgressCheckListItemCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._TestProgressCheckListItemCollectionByCheckTypeID Is Nothing Then

					Me.RemovePostSave("TestProgressCheckListItemCollectionByCheckTypeID")
					Me._TestProgressCheckListItemCollectionByCheckTypeID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _TestProgressCheckListItemCollectionByCheckTypeID As TestProgressCheckListItemCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "TestProgressCheckListItemCollectionByCheckTypeID"
					coll = Me.TestProgressCheckListItemCollectionByCheckTypeID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "TestProgressCheckListItemCollectionByCheckTypeID", GetType(TestProgressCheckListItemCollection), New TestProgressCheckListItem()))
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
		
			If Not Me._TestProgressCheckListItemCollectionByCheckTypeID Is Nothing Then
				Apply(Me._TestProgressCheckListItemCollectionByCheckTypeID, "CheckTypeID", Me.CheckTypeID)
			End If
			
		End Sub
	End Class
		



	<Serializable> _
	Partial Public Class TestProgressCheckTypeMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(TestProgressCheckTypeMetadata.ColumnNames.CheckTypeID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = TestProgressCheckTypeMetadata.PropertyNames.CheckTypeID
			c.IsInPrimaryKey = True
			c.IsAutoIncrement = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressCheckTypeMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = TestProgressCheckTypeMetadata.PropertyNames.Description
			c.CharacterMaxLength = 25
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressCheckTypeMetadata.ColumnNames.RequireChannel, 2, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = TestProgressCheckTypeMetadata.PropertyNames.RequireChannel
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As TestProgressCheckTypeMetadata
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
			 Public Const CheckTypeID As String = "CheckTypeID"
			 Public Const Description As String = "Description"
			 Public Const RequireChannel As String = "RequireChannel?"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const CheckTypeID As String = "CheckTypeID"
			 Public Const Description As String = "Description"
			 Public Const RequireChannel As String = "RequireChannel"
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
			SyncLock GetType(TestProgressCheckTypeMetadata)
			
				If TestProgressCheckTypeMetadata.mapDelegates Is Nothing Then
					TestProgressCheckTypeMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If TestProgressCheckTypeMetadata._meta Is Nothing Then
					TestProgressCheckTypeMetadata._meta = New TestProgressCheckTypeMetadata()
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
				


				meta.AddTypeMap("CheckTypeID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("RequireChannel", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "TestProgressCheckType"
				meta.Destination = "TestProgressCheckType"
				
				meta.spInsert = "proc_TestProgressCheckTypeInsert"
				meta.spUpdate = "proc_TestProgressCheckTypeUpdate"
				meta.spDelete = "proc_TestProgressCheckTypeDelete"
				meta.spLoadAll = "proc_TestProgressCheckTypeLoadAll"
				meta.spLoadByPrimaryKey = "proc_TestProgressCheckTypeLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As TestProgressCheckTypeMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

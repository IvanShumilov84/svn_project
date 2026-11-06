
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 1/21/2016 2:57:12 PM
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
	' Encapsulates the 'AxisType' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(AxisType))> _
	<XmlType("AxisType")> _	
	Partial Public Class AxisType 
		Inherits esAxisType
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New AxisType()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal typeID As System.Int32)
			Dim obj As New AxisType()
			obj.TypeID = typeID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal typeID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New AxisType()
			obj.TypeID = typeID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("AxisTypeCollection")> _
	Partial Public Class AxisTypeCollection
		Inherits esAxisTypeCollection
		Implements IEnumerable(Of AxisType)
	
		Public Function FindByPrimaryKey(ByVal typeID As System.Int32) As AxisType
			Return MyBase.SingleOrDefault(Function(e) e.TypeID.HasValue AndAlso e.TypeID.Value = typeID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(AxisType))> _
		Public Class AxisTypeCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of AxisTypeCollection)
			
			Public Shared Widening Operator CType(packet As AxisTypeCollectionWCFPacket) As AxisTypeCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As AxisTypeCollection) As AxisTypeCollectionWCFPacket
				Return New AxisTypeCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class AxisTypeQuery 
		Inherits esAxisTypeQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "AxisTypeQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As AxisTypeQuery) As String
			Return AxisTypeQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As AxisTypeQuery
			Return DirectCast(AxisTypeQuery.SerializeHelper.FromXml(query, GetType(AxisTypeQuery)), AxisTypeQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esAxisType
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal typeID As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(typeID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(typeID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal typeID As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(typeID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(typeID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal typeID As System.Int32) As Boolean
		
			Dim query As New AxisTypeQuery()
			query.Where(query.TypeID = typeID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal typeID As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("TypeID", typeID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to AxisType.TypeID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TypeID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(AxisTypeMetadata.ColumnNames.TypeID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(AxisTypeMetadata.ColumnNames.TypeID, value) Then
					OnPropertyChanged(AxisTypeMetadata.PropertyNames.TypeID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to AxisType.Type
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Type As System.String
			Get
				Return MyBase.GetSystemString(AxisTypeMetadata.ColumnNames.Type)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(AxisTypeMetadata.ColumnNames.Type, value) Then
					OnPropertyChanged(AxisTypeMetadata.PropertyNames.Type)
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
												
						Case "TypeID"
							Me.str().TypeID = CType(value, string)
												
						Case "Type"
							Me.str().Type = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "TypeID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.TypeID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(AxisTypeMetadata.PropertyNames.TypeID)
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
		
			Public Sub New(ByVal entity As esAxisType)
				Me.entity = entity
			End Sub				
		
	
			Public Property TypeID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.TypeID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TypeID = Nothing
					Else
						entity.TypeID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Type As System.String 
				Get
					Dim data_ As System.String = entity.Type
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Type = Nothing
					Else
						entity.Type = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esAxisType
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return AxisTypeMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As AxisTypeQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New AxisTypeQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As AxisTypeQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As AxisTypeQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As AxisTypeQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esAxisTypeCollection
		Inherits esEntityCollection(Of AxisType)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return AxisTypeMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "AxisTypeCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As AxisTypeQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New AxisTypeQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As AxisTypeQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New AxisTypeQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As AxisTypeQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, AxisTypeQuery))
		End Sub
		
		#End Region
						
		Private m_query As AxisTypeQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esAxisTypeQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return AxisTypeMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "TypeID" 
					Return Me.TypeID
				Case "Type" 
					Return Me.Type
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property TypeID As esQueryItem
			Get
				Return New esQueryItem(Me, AxisTypeMetadata.ColumnNames.TypeID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Type As esQueryItem
			Get
				Return New esQueryItem(Me, AxisTypeMetadata.ColumnNames.Type, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class AxisType 
		Inherits esAxisType
		
	
		#Region "GraphAxisCollectionByAxisTypeID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_GraphAxisCollectionByAxisTypeID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.AxisType.GraphAxisCollectionByAxisTypeID_Delegate)
				map.PropertyName = "GraphAxisCollectionByAxisTypeID"
				map.MyColumnName = "AxisTypeID"
				map.ParentColumnName = "TypeID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub GraphAxisCollectionByAxisTypeID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New AxisTypeQuery(data.NextAlias())
			
			Dim mee As GraphAxisQuery = If(data.You IsNot Nothing, TryCast(data.You, GraphAxisQuery), New GraphAxisQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.TypeID = mee.AxisTypeID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - AxisTypeGraphAxis
		''' </summary>

		<XmlIgnore()> _ 
		Public Property GraphAxisCollectionByAxisTypeID As GraphAxisCollection 
		
			Get
				If Me._GraphAxisCollectionByAxisTypeID Is Nothing Then
					Me._GraphAxisCollectionByAxisTypeID = New GraphAxisCollection()
					Me._GraphAxisCollectionByAxisTypeID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("GraphAxisCollectionByAxisTypeID", Me._GraphAxisCollectionByAxisTypeID)
				
					If Not Me.TypeID.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._GraphAxisCollectionByAxisTypeID.Query.Where(Me._GraphAxisCollectionByAxisTypeID.Query.AxisTypeID = Me.TypeID)
							Me._GraphAxisCollectionByAxisTypeID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._GraphAxisCollectionByAxisTypeID.fks.Add(GraphAxisMetadata.ColumnNames.AxisTypeID, Me.TypeID)
					End If
				End If

				Return Me._GraphAxisCollectionByAxisTypeID
			End Get
			
			Set(ByVal value As GraphAxisCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._GraphAxisCollectionByAxisTypeID Is Nothing Then

					Me.RemovePostSave("GraphAxisCollectionByAxisTypeID")
					Me._GraphAxisCollectionByAxisTypeID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _GraphAxisCollectionByAxisTypeID As GraphAxisCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "GraphAxisCollectionByAxisTypeID"
					coll = Me.GraphAxisCollectionByAxisTypeID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "GraphAxisCollectionByAxisTypeID", GetType(GraphAxisCollection), New GraphAxis()))
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
		
			If Not Me._GraphAxisCollectionByAxisTypeID Is Nothing Then
				Apply(Me._GraphAxisCollectionByAxisTypeID, "AxisTypeID", Me.TypeID)
			End If
			
		End Sub
	End Class
		



	<Serializable> _
	Partial Public Class AxisTypeMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(AxisTypeMetadata.ColumnNames.TypeID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = AxisTypeMetadata.PropertyNames.TypeID
			c.IsInPrimaryKey = True
			c.IsAutoIncrement = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(AxisTypeMetadata.ColumnNames.Type, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = AxisTypeMetadata.PropertyNames.Type
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As AxisTypeMetadata
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
			 Public Const TypeID As String = "TypeID"
			 Public Const Type As String = "Type"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const TypeID As String = "TypeID"
			 Public Const Type As String = "Type"
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
			SyncLock GetType(AxisTypeMetadata)
			
				If AxisTypeMetadata.mapDelegates Is Nothing Then
					AxisTypeMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If AxisTypeMetadata._meta Is Nothing Then
					AxisTypeMetadata._meta = New AxisTypeMetadata()
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
				


				meta.AddTypeMap("TypeID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Type", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "AxisType"
				meta.Destination = "AxisType"
				
				meta.spInsert = "proc_AxisTypeInsert"
				meta.spUpdate = "proc_AxisTypeUpdate"
				meta.spDelete = "proc_AxisTypeDelete"
				meta.spLoadAll = "proc_AxisTypeLoadAll"
				meta.spLoadByPrimaryKey = "proc_AxisTypeLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As AxisTypeMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

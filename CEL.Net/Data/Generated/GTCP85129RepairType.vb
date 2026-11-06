
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 5/11/2015 8:45:56 AM
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
	' Encapsulates the 'GTCP85_129_RepairType' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(GTCP85129RepairType))> _
	<XmlType("GTCP85129RepairType")> _	
	Partial Public Class GTCP85129RepairType 
		Inherits esGTCP85129RepairType
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New GTCP85129RepairType()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int16)
			Dim obj As New GTCP85129RepairType()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int16, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New GTCP85129RepairType()
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
	<XmlType("GTCP85129RepairTypeCollection")> _
	Partial Public Class GTCP85129RepairTypeCollection
		Inherits esGTCP85129RepairTypeCollection
		Implements IEnumerable(Of GTCP85129RepairType)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int16) As GTCP85129RepairType
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(GTCP85129RepairType))> _
		Public Class GTCP85129RepairTypeCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of GTCP85129RepairTypeCollection)
			
			Public Shared Widening Operator CType(packet As GTCP85129RepairTypeCollectionWCFPacket) As GTCP85129RepairTypeCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As GTCP85129RepairTypeCollection) As GTCP85129RepairTypeCollectionWCFPacket
				Return New GTCP85129RepairTypeCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class GTCP85129RepairTypeQuery 
		Inherits esGTCP85129RepairTypeQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "GTCP85129RepairTypeQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As GTCP85129RepairTypeQuery) As String
			Return GTCP85129RepairTypeQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As GTCP85129RepairTypeQuery
			Return DirectCast(GTCP85129RepairTypeQuery.SerializeHelper.FromXml(query, GetType(GTCP85129RepairTypeQuery)), GTCP85129RepairTypeQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esGTCP85129RepairType
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal id As System.Int16) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(id)
			Else
				Return LoadByPrimaryKeyStoredProcedure(id)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal id As System.Int16) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(id)
			Else
				Return LoadByPrimaryKeyStoredProcedure(id)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal id As System.Int16) As Boolean
		
			Dim query As New GTCP85129RepairTypeQuery()
			query.Where(query.Id = id)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal id As System.Int16) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("Id", id)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to GTCP85_129_RepairType.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(GTCP85129RepairTypeMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(GTCP85129RepairTypeMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(GTCP85129RepairTypeMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GTCP85_129_RepairType.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(GTCP85129RepairTypeMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(GTCP85129RepairTypeMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(GTCP85129RepairTypeMetadata.PropertyNames.Description)
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
												
						Case "Description"
							Me.str().Description = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.Id = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(GTCP85129RepairTypeMetadata.PropertyNames.Id)
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
		
			Public Sub New(ByVal entity As esGTCP85129RepairType)
				Me.entity = entity
			End Sub				
		
	
			Public Property Id As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.Id
					
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
						entity.Id = Convert.ToInt16(Value)
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
		  

			Private entity As esGTCP85129RepairType
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return GTCP85129RepairTypeMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As GTCP85129RepairTypeQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New GTCP85129RepairTypeQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As GTCP85129RepairTypeQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As GTCP85129RepairTypeQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As GTCP85129RepairTypeQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esGTCP85129RepairTypeCollection
		Inherits esEntityCollection(Of GTCP85129RepairType)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return GTCP85129RepairTypeMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "GTCP85129RepairTypeCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As GTCP85129RepairTypeQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New GTCP85129RepairTypeQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As GTCP85129RepairTypeQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New GTCP85129RepairTypeQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As GTCP85129RepairTypeQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, GTCP85129RepairTypeQuery))
		End Sub
		
		#End Region
						
		Private m_query As GTCP85129RepairTypeQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esGTCP85129RepairTypeQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return GTCP85129RepairTypeMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "Description" 
					Return Me.Description
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, GTCP85129RepairTypeMetadata.ColumnNames.Id, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, GTCP85129RepairTypeMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class GTCP85129RepairType 
		Inherits esGTCP85129RepairType
		
	
		#Region "UpToEngineSeriesCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - GTCP85_129_RepairTypeGTCP85_129
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToEngineSeriesCollection As EngineSeriesCollection
		
			Get
				If Me._UpToEngineSeriesCollection Is Nothing Then
					Me._UpToEngineSeriesCollection = New EngineSeriesCollection()
					Me._UpToEngineSeriesCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToEngineSeriesCollection", Me._UpToEngineSeriesCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.Id.Equals(Nothing) Then 
				
						Dim m As New EngineSeriesQuery("m")
						Dim j As New Gtcp85129Query("j")
						m.Select(m)
						m.InnerJoin(j).On(m.EngineSeries = j.EngineSeries)
                        m.Where(j.RepairTypeID = Me.Id)

						Me._UpToEngineSeriesCollection.Load(m)

					End If
				End If

				Return Me._UpToEngineSeriesCollection
			End Get
			
			Set(ByVal value As EngineSeriesCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToEngineSeriesCollection Is Nothing Then

					Me.RemovePostSave("UpToEngineSeriesCollection")
					Me._UpToEngineSeriesCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - GTCP85_129_RepairTypeGTCP85_129
		''' </summary>
		Public Sub AssociateEngineSeriesCollection(entity As EngineSeries)
			If Me._Gtcp85129Collection Is Nothing Then
				Me._Gtcp85129Collection = New Gtcp85129Collection()
				Me._Gtcp85129Collection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("Gtcp85129Collection", Me._Gtcp85129Collection)
			End If
			
			Dim obj As Gtcp85129 = Me._Gtcp85129Collection.AddNew()
			obj.RepairTypeID = Me.Id
			obj.EngineSeries = entity.EngineSeries			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - GTCP85_129_RepairTypeGTCP85_129
		''' </summary>
		Public Sub DissociateEngineSeriesCollection(entity As EngineSeries)
			If Me._Gtcp85129Collection Is Nothing Then
				Me._Gtcp85129Collection = new Gtcp85129Collection()
				Me._Gtcp85129Collection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("Gtcp85129Collection", Me._Gtcp85129Collection)
			End If

			Dim obj As Gtcp85129 = Me._Gtcp85129Collection.AddNew()
			obj.RepairTypeID = Me.Id
            obj.EngineSeries = entity.EngineSeries
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToEngineSeriesCollection As EngineSeriesCollection
		Private _Gtcp85129Collection As Gtcp85129Collection
		#End Region

		#Region "Gtcp85129CollectionByRepairTypeID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_Gtcp85129CollectionByRepairTypeID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.GTCP85129RepairType.Gtcp85129CollectionByRepairTypeID_Delegate)
				map.PropertyName = "Gtcp85129CollectionByRepairTypeID"
				map.MyColumnName = "RepairTypeID"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub Gtcp85129CollectionByRepairTypeID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New GTCP85129RepairTypeQuery(data.NextAlias())
			
			Dim mee As Gtcp85129Query = If(data.You IsNot Nothing, TryCast(data.You, Gtcp85129Query), New Gtcp85129Query(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.RepairTypeID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - GTCP85_129_RepairTypeGTCP85_129
		''' </summary>

		<XmlIgnore()> _ 
		Public Property Gtcp85129CollectionByRepairTypeID As Gtcp85129Collection 
		
			Get
				If Me._Gtcp85129CollectionByRepairTypeID Is Nothing Then
					Me._Gtcp85129CollectionByRepairTypeID = New Gtcp85129Collection()
					Me._Gtcp85129CollectionByRepairTypeID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("Gtcp85129CollectionByRepairTypeID", Me._Gtcp85129CollectionByRepairTypeID)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._Gtcp85129CollectionByRepairTypeID.Query.Where(Me._Gtcp85129CollectionByRepairTypeID.Query.RepairTypeID = Me.Id)
							Me._Gtcp85129CollectionByRepairTypeID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._Gtcp85129CollectionByRepairTypeID.fks.Add(Gtcp85129Metadata.ColumnNames.RepairTypeID, Me.Id)
					End If
				End If

				Return Me._Gtcp85129CollectionByRepairTypeID
			End Get
			
			Set(ByVal value As Gtcp85129Collection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._Gtcp85129CollectionByRepairTypeID Is Nothing Then

					Me.RemovePostSave("Gtcp85129CollectionByRepairTypeID")
					Me._Gtcp85129CollectionByRepairTypeID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _Gtcp85129CollectionByRepairTypeID As Gtcp85129Collection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "Gtcp85129CollectionByRepairTypeID"
					coll = Me.Gtcp85129CollectionByRepairTypeID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "Gtcp85129CollectionByRepairTypeID", GetType(Gtcp85129Collection), New Gtcp85129()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class GTCP85129RepairTypeMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(GTCP85129RepairTypeMetadata.ColumnNames.Id, 0, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = GTCP85129RepairTypeMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(GTCP85129RepairTypeMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = GTCP85129RepairTypeMetadata.PropertyNames.Description
			c.CharacterMaxLength = 75
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As GTCP85129RepairTypeMetadata
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
			 Public Const Description As String = "Description"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
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
			SyncLock GetType(GTCP85129RepairTypeMetadata)
			
				If GTCP85129RepairTypeMetadata.mapDelegates Is Nothing Then
					GTCP85129RepairTypeMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If GTCP85129RepairTypeMetadata._meta Is Nothing Then
					GTCP85129RepairTypeMetadata._meta = New GTCP85129RepairTypeMetadata()
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
				


				meta.AddTypeMap("Id", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "GTCP85_129_RepairType"
				meta.Destination = "GTCP85_129_RepairType"
				
				meta.spInsert = "proc_GTCP85_129_RepairTypeInsert"
				meta.spUpdate = "proc_GTCP85_129_RepairTypeUpdate"
				meta.spDelete = "proc_GTCP85_129_RepairTypeDelete"
				meta.spLoadAll = "proc_GTCP85_129_RepairTypeLoadAll"
				meta.spLoadByPrimaryKey = "proc_GTCP85_129_RepairTypeLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As GTCP85129RepairTypeMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

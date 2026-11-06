
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 4/10/2015 12:29:39 PM
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
	' Encapsulates the 'GTCP85_98_RepairType' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(GTCP8598RepairType))> _
	<XmlType("GTCP8598RepairType")> _	
	Partial Public Class GTCP8598RepairType 
		Inherits esGTCP8598RepairType
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New GTCP8598RepairType()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int16)
			Dim obj As New GTCP8598RepairType()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int16, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New GTCP8598RepairType()
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
	<XmlType("GTCP8598RepairTypeCollection")> _
	Partial Public Class GTCP8598RepairTypeCollection
		Inherits esGTCP8598RepairTypeCollection
		Implements IEnumerable(Of GTCP8598RepairType)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int16) As GTCP8598RepairType
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(GTCP8598RepairType))> _
		Public Class GTCP8598RepairTypeCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of GTCP8598RepairTypeCollection)
			
			Public Shared Widening Operator CType(packet As GTCP8598RepairTypeCollectionWCFPacket) As GTCP8598RepairTypeCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As GTCP8598RepairTypeCollection) As GTCP8598RepairTypeCollectionWCFPacket
				Return New GTCP8598RepairTypeCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class GTCP8598RepairTypeQuery 
		Inherits esGTCP8598RepairTypeQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "GTCP8598RepairTypeQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As GTCP8598RepairTypeQuery) As String
			Return GTCP8598RepairTypeQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As GTCP8598RepairTypeQuery
			Return DirectCast(GTCP8598RepairTypeQuery.SerializeHelper.FromXml(query, GetType(GTCP8598RepairTypeQuery)), GTCP8598RepairTypeQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esGTCP8598RepairType
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
		
			Dim query As New GTCP8598RepairTypeQuery()
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
		' Maps to GTCP85_98_RepairType.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(GTCP8598RepairTypeMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(GTCP8598RepairTypeMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(GTCP8598RepairTypeMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GTCP85_98_RepairType.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(GTCP8598RepairTypeMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(GTCP8598RepairTypeMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(GTCP8598RepairTypeMetadata.PropertyNames.Description)
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
								OnPropertyChanged(GTCP8598RepairTypeMetadata.PropertyNames.Id)
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
		
			Public Sub New(ByVal entity As esGTCP8598RepairType)
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
		  

			Private entity As esGTCP8598RepairType
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return GTCP8598RepairTypeMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As GTCP8598RepairTypeQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New GTCP8598RepairTypeQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As GTCP8598RepairTypeQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As GTCP8598RepairTypeQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As GTCP8598RepairTypeQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esGTCP8598RepairTypeCollection
		Inherits esEntityCollection(Of GTCP8598RepairType)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return GTCP8598RepairTypeMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "GTCP8598RepairTypeCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As GTCP8598RepairTypeQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New GTCP8598RepairTypeQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As GTCP8598RepairTypeQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New GTCP8598RepairTypeQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As GTCP8598RepairTypeQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, GTCP8598RepairTypeQuery))
		End Sub
		
		#End Region
						
		Private m_query As GTCP8598RepairTypeQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esGTCP8598RepairTypeQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return GTCP8598RepairTypeMetadata.Meta()
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
				Return New esQueryItem(Me, GTCP8598RepairTypeMetadata.ColumnNames.Id, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, GTCP8598RepairTypeMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class GTCP8598RepairType 
		Inherits esGTCP8598RepairType
		
	
		#Region "UpToEngineInfoCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - GTCP85_98_RepairTypeGTCP85_98
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToEngineInfoCollection As EngineInfoCollection
		
			Get
				If Me._UpToEngineInfoCollection Is Nothing Then
					Me._UpToEngineInfoCollection = New EngineInfoCollection()
					Me._UpToEngineInfoCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToEngineInfoCollection", Me._UpToEngineInfoCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.Id.Equals(Nothing) Then 
				
						Dim m As New EngineInfoQuery("m")
						Dim j As New Gtcp8598Query("j")
						m.Select(m)
						m.InnerJoin(j).On(m.EngineModel = j.EngineModel)
                        m.Where(j.RepairTypeID = Me.Id)

						Me._UpToEngineInfoCollection.Load(m)

					End If
				End If

				Return Me._UpToEngineInfoCollection
			End Get
			
			Set(ByVal value As EngineInfoCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToEngineInfoCollection Is Nothing Then

					Me.RemovePostSave("UpToEngineInfoCollection")
					Me._UpToEngineInfoCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - GTCP85_98_RepairTypeGTCP85_98
		''' </summary>
		Public Sub AssociateEngineInfoCollection(entity As EngineInfo)
			If Me._Gtcp8598Collection Is Nothing Then
				Me._Gtcp8598Collection = New Gtcp8598Collection()
				Me._Gtcp8598Collection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("Gtcp8598Collection", Me._Gtcp8598Collection)
			End If
			
			Dim obj As Gtcp8598 = Me._Gtcp8598Collection.AddNew()
			obj.RepairTypeID = Me.Id
			obj.EngineModel = entity.EngineModel			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - GTCP85_98_RepairTypeGTCP85_98
		''' </summary>
		Public Sub DissociateEngineInfoCollection(entity As EngineInfo)
			If Me._Gtcp8598Collection Is Nothing Then
				Me._Gtcp8598Collection = new Gtcp8598Collection()
				Me._Gtcp8598Collection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("Gtcp8598Collection", Me._Gtcp8598Collection)
			End If

			Dim obj As Gtcp8598 = Me._Gtcp8598Collection.AddNew()
			obj.RepairTypeID = Me.Id
            obj.EngineModel = entity.EngineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToEngineInfoCollection As EngineInfoCollection
		Private _Gtcp8598Collection As Gtcp8598Collection
		#End Region

		#Region "Gtcp8598CollectionByRepairTypeID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_Gtcp8598CollectionByRepairTypeID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.GTCP8598RepairType.Gtcp8598CollectionByRepairTypeID_Delegate)
				map.PropertyName = "Gtcp8598CollectionByRepairTypeID"
				map.MyColumnName = "RepairTypeID"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub Gtcp8598CollectionByRepairTypeID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New GTCP8598RepairTypeQuery(data.NextAlias())
			
			Dim mee As Gtcp8598Query = If(data.You IsNot Nothing, TryCast(data.You, Gtcp8598Query), New Gtcp8598Query(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.RepairTypeID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - GTCP85_98_RepairTypeGTCP85_98
		''' </summary>

		<XmlIgnore()> _ 
		Public Property Gtcp8598CollectionByRepairTypeID As Gtcp8598Collection 
		
			Get
				If Me._Gtcp8598CollectionByRepairTypeID Is Nothing Then
					Me._Gtcp8598CollectionByRepairTypeID = New Gtcp8598Collection()
					Me._Gtcp8598CollectionByRepairTypeID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("Gtcp8598CollectionByRepairTypeID", Me._Gtcp8598CollectionByRepairTypeID)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._Gtcp8598CollectionByRepairTypeID.Query.Where(Me._Gtcp8598CollectionByRepairTypeID.Query.RepairTypeID = Me.Id)
							Me._Gtcp8598CollectionByRepairTypeID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._Gtcp8598CollectionByRepairTypeID.fks.Add(Gtcp8598Metadata.ColumnNames.RepairTypeID, Me.Id)
					End If
				End If

				Return Me._Gtcp8598CollectionByRepairTypeID
			End Get
			
			Set(ByVal value As Gtcp8598Collection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._Gtcp8598CollectionByRepairTypeID Is Nothing Then

					Me.RemovePostSave("Gtcp8598CollectionByRepairTypeID")
					Me._Gtcp8598CollectionByRepairTypeID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _Gtcp8598CollectionByRepairTypeID As Gtcp8598Collection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "Gtcp8598CollectionByRepairTypeID"
					coll = Me.Gtcp8598CollectionByRepairTypeID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "Gtcp8598CollectionByRepairTypeID", GetType(Gtcp8598Collection), New Gtcp8598()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class GTCP8598RepairTypeMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(GTCP8598RepairTypeMetadata.ColumnNames.Id, 0, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = GTCP8598RepairTypeMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(GTCP8598RepairTypeMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = GTCP8598RepairTypeMetadata.PropertyNames.Description
			c.CharacterMaxLength = 25
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As GTCP8598RepairTypeMetadata
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
			SyncLock GetType(GTCP8598RepairTypeMetadata)
			
				If GTCP8598RepairTypeMetadata.mapDelegates Is Nothing Then
					GTCP8598RepairTypeMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If GTCP8598RepairTypeMetadata._meta Is Nothing Then
					GTCP8598RepairTypeMetadata._meta = New GTCP8598RepairTypeMetadata()
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
				
				
				 
				meta.Source = "GTCP85_98_RepairType"
				meta.Destination = "GTCP85_98_RepairType"
				
				meta.spInsert = "proc_GTCP85_98_RepairTypeInsert"
				meta.spUpdate = "proc_GTCP85_98_RepairTypeUpdate"
				meta.spDelete = "proc_GTCP85_98_RepairTypeDelete"
				meta.spLoadAll = "proc_GTCP85_98_RepairTypeLoadAll"
				meta.spLoadByPrimaryKey = "proc_GTCP85_98_RepairTypeLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As GTCP8598RepairTypeMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

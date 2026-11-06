
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 11/21/2012 2:35:59 PM
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
	' Encapsulates the 'EngineeringUnit' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(EngineeringUnit))> _
	<XmlType("EngineeringUnit")> _	
	Partial Public Class EngineeringUnit 
		Inherits esEngineeringUnit
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New EngineeringUnit()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32)
			Dim obj As New EngineeringUnit()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New EngineeringUnit()
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
	<XmlType("EngineeringUnitCollection")> _
	Partial Public Class EngineeringUnitCollection
		Inherits esEngineeringUnitCollection
		Implements IEnumerable(Of EngineeringUnit)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32) As EngineeringUnit
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(EngineeringUnit))> _
		Public Class EngineeringUnitCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of EngineeringUnitCollection)
			
			Public Shared Widening Operator CType(packet As EngineeringUnitCollectionWCFPacket) As EngineeringUnitCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As EngineeringUnitCollection) As EngineeringUnitCollectionWCFPacket
				Return New EngineeringUnitCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class EngineeringUnitQuery 
		Inherits esEngineeringUnitQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "EngineeringUnitQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As EngineeringUnitQuery) As String
			Return EngineeringUnitQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As EngineeringUnitQuery
			Return DirectCast(EngineeringUnitQuery.SerializeHelper.FromXml(query, GetType(EngineeringUnitQuery)), EngineeringUnitQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esEngineeringUnit
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
		
			Dim query As New EngineeringUnitQuery()
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
		' Maps to EngineeringUnit.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(EngineeringUnitMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(EngineeringUnitMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(EngineeringUnitMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to EngineeringUnit.Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Name As System.String
			Get
				Return MyBase.GetSystemString(EngineeringUnitMetadata.ColumnNames.Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EngineeringUnitMetadata.ColumnNames.Name, value) Then
					OnPropertyChanged(EngineeringUnitMetadata.PropertyNames.Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to EngineeringUnit.DisplayName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DisplayName As System.String
			Get
				Return MyBase.GetSystemString(EngineeringUnitMetadata.ColumnNames.DisplayName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EngineeringUnitMetadata.ColumnNames.DisplayName, value) Then
					OnPropertyChanged(EngineeringUnitMetadata.PropertyNames.DisplayName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to EngineeringUnit.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(EngineeringUnitMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EngineeringUnitMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(EngineeringUnitMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to EngineeringUnit.Category
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Category As System.String
			Get
				Return MyBase.GetSystemString(EngineeringUnitMetadata.ColumnNames.Category)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EngineeringUnitMetadata.ColumnNames.Category, value) Then
					OnPropertyChanged(EngineeringUnitMetadata.PropertyNames.Category)
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
												
						Case "Name"
							Me.str().Name = CType(value, string)
												
						Case "DisplayName"
							Me.str().DisplayName = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "Category"
							Me.str().Category = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(EngineeringUnitMetadata.PropertyNames.Id)
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
		
			Public Sub New(ByVal entity As esEngineeringUnit)
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
		  	
			Public Property Name As System.String 
				Get
					Dim data_ As System.String = entity.Name
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Name = Nothing
					Else
						entity.Name = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DisplayName As System.String 
				Get
					Dim data_ As System.String = entity.DisplayName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DisplayName = Nothing
					Else
						entity.DisplayName = Convert.ToString(Value)
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
		  	
			Public Property Category As System.String 
				Get
					Dim data_ As System.String = entity.Category
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Category = Nothing
					Else
						entity.Category = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esEngineeringUnit
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return EngineeringUnitMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As EngineeringUnitQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New EngineeringUnitQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As EngineeringUnitQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As EngineeringUnitQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As EngineeringUnitQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esEngineeringUnitCollection
		Inherits esEntityCollection(Of EngineeringUnit)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return EngineeringUnitMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "EngineeringUnitCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As EngineeringUnitQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New EngineeringUnitQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As EngineeringUnitQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New EngineeringUnitQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As EngineeringUnitQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, EngineeringUnitQuery))
		End Sub
		
		#End Region
						
		Private m_query As EngineeringUnitQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esEngineeringUnitQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return EngineeringUnitMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "Name" 
					Return Me.Name
				Case "DisplayName" 
					Return Me.DisplayName
				Case "Description" 
					Return Me.Description
				Case "Category" 
					Return Me.Category
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, EngineeringUnitMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Name As esQueryItem
			Get
				Return New esQueryItem(Me, EngineeringUnitMetadata.ColumnNames.Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DisplayName As esQueryItem
			Get
				Return New esQueryItem(Me, EngineeringUnitMetadata.ColumnNames.DisplayName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, EngineeringUnitMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Category As esQueryItem
			Get
				Return New esQueryItem(Me, EngineeringUnitMetadata.ColumnNames.Category, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class EngineeringUnit 
		Inherits esEngineeringUnit
		
	
		#Region "HMINumericChannelsCollectionByEngUnitID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_HMINumericChannelsCollectionByEngUnitID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.EngineeringUnit.HMINumericChannelsCollectionByEngUnitID_Delegate)
				map.PropertyName = "HMINumericChannelsCollectionByEngUnitID"
				map.MyColumnName = "EngUnitID"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub HMINumericChannelsCollectionByEngUnitID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New EngineeringUnitQuery(data.NextAlias())
			
			Dim mee As HMINumericChannelsQuery = If(data.You IsNot Nothing, TryCast(data.You, HMINumericChannelsQuery), New HMINumericChannelsQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.EngUnitID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - EngineeringUnitHMI_Numeric_Channels
		''' </summary>

		<XmlIgnore()> _ 
		Public Property HMINumericChannelsCollectionByEngUnitID As HMINumericChannelsCollection 
		
			Get
				If Me._HMINumericChannelsCollectionByEngUnitID Is Nothing Then
					Me._HMINumericChannelsCollectionByEngUnitID = New HMINumericChannelsCollection()
					Me._HMINumericChannelsCollectionByEngUnitID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("HMINumericChannelsCollectionByEngUnitID", Me._HMINumericChannelsCollectionByEngUnitID)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._HMINumericChannelsCollectionByEngUnitID.Query.Where(Me._HMINumericChannelsCollectionByEngUnitID.Query.EngUnitID = Me.Id)
							Me._HMINumericChannelsCollectionByEngUnitID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._HMINumericChannelsCollectionByEngUnitID.fks.Add(HMINumericChannelsMetadata.ColumnNames.EngUnitID, Me.Id)
					End If
				End If

				Return Me._HMINumericChannelsCollectionByEngUnitID
			End Get
			
			Set(ByVal value As HMINumericChannelsCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._HMINumericChannelsCollectionByEngUnitID Is Nothing Then

					Me.RemovePostSave("HMINumericChannelsCollectionByEngUnitID")
					Me._HMINumericChannelsCollectionByEngUnitID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _HMINumericChannelsCollectionByEngUnitID As HMINumericChannelsCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "HMINumericChannelsCollectionByEngUnitID"
					coll = Me.HMINumericChannelsCollectionByEngUnitID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "HMINumericChannelsCollectionByEngUnitID", GetType(HMINumericChannelsCollection), New HMINumericChannels()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class EngineeringUnitMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(EngineeringUnitMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = EngineeringUnitMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(EngineeringUnitMetadata.ColumnNames.Name, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = EngineeringUnitMetadata.PropertyNames.Name
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(EngineeringUnitMetadata.ColumnNames.DisplayName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = EngineeringUnitMetadata.PropertyNames.DisplayName
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(EngineeringUnitMetadata.ColumnNames.Description, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = EngineeringUnitMetadata.PropertyNames.Description
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(EngineeringUnitMetadata.ColumnNames.Category, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = EngineeringUnitMetadata.PropertyNames.Category
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As EngineeringUnitMetadata
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
			 Public Const Name As String = "Name"
			 Public Const DisplayName As String = "DisplayName"
			 Public Const Description As String = "Description"
			 Public Const Category As String = "Category"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const Name As String = "Name"
			 Public Const DisplayName As String = "DisplayName"
			 Public Const Description As String = "Description"
			 Public Const Category As String = "Category"
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
			SyncLock GetType(EngineeringUnitMetadata)
			
				If EngineeringUnitMetadata.mapDelegates Is Nothing Then
					EngineeringUnitMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If EngineeringUnitMetadata._meta Is Nothing Then
					EngineeringUnitMetadata._meta = New EngineeringUnitMetadata()
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
				meta.AddTypeMap("Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DisplayName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Category", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "EngineeringUnit"
				meta.Destination = "EngineeringUnit"
				
				meta.spInsert = "proc_EngineeringUnitInsert"
				meta.spUpdate = "proc_EngineeringUnitUpdate"
				meta.spDelete = "proc_EngineeringUnitDelete"
				meta.spLoadAll = "proc_EngineeringUnitLoadAll"
				meta.spLoadByPrimaryKey = "proc_EngineeringUnitLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As EngineeringUnitMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

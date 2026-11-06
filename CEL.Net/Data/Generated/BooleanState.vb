
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 11/14/2012 11:37:05 AM
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
	' Encapsulates the 'BooleanState' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(BooleanState))> _
	<XmlType("BooleanState")> _	
	Partial Public Class BooleanState 
		Inherits esBooleanState
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New BooleanState()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32)
			Dim obj As New BooleanState()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New BooleanState()
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
	<XmlType("BooleanStateCollection")> _
	Partial Public Class BooleanStateCollection
		Inherits esBooleanStateCollection
		Implements IEnumerable(Of BooleanState)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32) As BooleanState
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(BooleanState))> _
		Public Class BooleanStateCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of BooleanStateCollection)
			
			Public Shared Widening Operator CType(packet As BooleanStateCollectionWCFPacket) As BooleanStateCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As BooleanStateCollection) As BooleanStateCollectionWCFPacket
				Return New BooleanStateCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class BooleanStateQuery 
		Inherits esBooleanStateQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "BooleanStateQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As BooleanStateQuery) As String
			Return BooleanStateQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As BooleanStateQuery
			Return DirectCast(BooleanStateQuery.SerializeHelper.FromXml(query, GetType(BooleanStateQuery)), BooleanStateQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esBooleanState
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
		
			Dim query As New BooleanStateQuery()
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
		' Maps to BooleanState.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(BooleanStateMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(BooleanStateMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(BooleanStateMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to BooleanState.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(BooleanStateMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(BooleanStateMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(BooleanStateMetadata.PropertyNames.Description)
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
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(BooleanStateMetadata.PropertyNames.Id)
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
		
			Public Sub New(ByVal entity As esBooleanState)
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
		  

			Private entity As esBooleanState
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return BooleanStateMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As BooleanStateQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New BooleanStateQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As BooleanStateQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As BooleanStateQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As BooleanStateQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esBooleanStateCollection
		Inherits esEntityCollection(Of BooleanState)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return BooleanStateMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "BooleanStateCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As BooleanStateQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New BooleanStateQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As BooleanStateQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New BooleanStateQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As BooleanStateQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, BooleanStateQuery))
		End Sub
		
		#End Region
						
		Private m_query As BooleanStateQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esBooleanStateQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return BooleanStateMetadata.Meta()
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
				Return New esQueryItem(Me, BooleanStateMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, BooleanStateMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class BooleanState 
		Inherits esBooleanState
		
	
		#Region "HMIBooleanChannelsCollectionByInitialValue - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_HMIBooleanChannelsCollectionByInitialValue() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.BooleanState.HMIBooleanChannelsCollectionByInitialValue_Delegate)
				map.PropertyName = "HMIBooleanChannelsCollectionByInitialValue"
				map.MyColumnName = "InitialValue"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub HMIBooleanChannelsCollectionByInitialValue_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New BooleanStateQuery(data.NextAlias())
			
			Dim mee As HMIBooleanChannelsQuery = If(data.You IsNot Nothing, TryCast(data.You, HMIBooleanChannelsQuery), New HMIBooleanChannelsQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.InitialValue)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - BooleanStateHMI_Boolean_Channels
		''' </summary>

		<XmlIgnore()> _ 
		Public Property HMIBooleanChannelsCollectionByInitialValue As HMIBooleanChannelsCollection 
		
			Get
				If Me._HMIBooleanChannelsCollectionByInitialValue Is Nothing Then
					Me._HMIBooleanChannelsCollectionByInitialValue = New HMIBooleanChannelsCollection()
					Me._HMIBooleanChannelsCollectionByInitialValue.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("HMIBooleanChannelsCollectionByInitialValue", Me._HMIBooleanChannelsCollectionByInitialValue)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._HMIBooleanChannelsCollectionByInitialValue.Query.Where(Me._HMIBooleanChannelsCollectionByInitialValue.Query.InitialValue = Me.Id)
							Me._HMIBooleanChannelsCollectionByInitialValue.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._HMIBooleanChannelsCollectionByInitialValue.fks.Add(HMIBooleanChannelsMetadata.ColumnNames.InitialValue, Me.Id)
					End If
				End If

				Return Me._HMIBooleanChannelsCollectionByInitialValue
			End Get
			
			Set(ByVal value As HMIBooleanChannelsCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._HMIBooleanChannelsCollectionByInitialValue Is Nothing Then

					Me.RemovePostSave("HMIBooleanChannelsCollectionByInitialValue")
					Me._HMIBooleanChannelsCollectionByInitialValue = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _HMIBooleanChannelsCollectionByInitialValue As HMIBooleanChannelsCollection
		#End Region

		#Region "HMIBooleanChannelsCollectionByFinalValue - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_HMIBooleanChannelsCollectionByFinalValue() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.BooleanState.HMIBooleanChannelsCollectionByFinalValue_Delegate)
				map.PropertyName = "HMIBooleanChannelsCollectionByFinalValue"
				map.MyColumnName = "FinalValue"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub HMIBooleanChannelsCollectionByFinalValue_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New BooleanStateQuery(data.NextAlias())
			
			Dim mee As HMIBooleanChannelsQuery = If(data.You IsNot Nothing, TryCast(data.You, HMIBooleanChannelsQuery), New HMIBooleanChannelsQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.FinalValue)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - BooleanStateHMI_Boolean_Channels1
		''' </summary>

		<XmlIgnore()> _ 
		Public Property HMIBooleanChannelsCollectionByFinalValue As HMIBooleanChannelsCollection 
		
			Get
				If Me._HMIBooleanChannelsCollectionByFinalValue Is Nothing Then
					Me._HMIBooleanChannelsCollectionByFinalValue = New HMIBooleanChannelsCollection()
					Me._HMIBooleanChannelsCollectionByFinalValue.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("HMIBooleanChannelsCollectionByFinalValue", Me._HMIBooleanChannelsCollectionByFinalValue)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._HMIBooleanChannelsCollectionByFinalValue.Query.Where(Me._HMIBooleanChannelsCollectionByFinalValue.Query.FinalValue = Me.Id)
							Me._HMIBooleanChannelsCollectionByFinalValue.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._HMIBooleanChannelsCollectionByFinalValue.fks.Add(HMIBooleanChannelsMetadata.ColumnNames.FinalValue, Me.Id)
					End If
				End If

				Return Me._HMIBooleanChannelsCollectionByFinalValue
			End Get
			
			Set(ByVal value As HMIBooleanChannelsCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._HMIBooleanChannelsCollectionByFinalValue Is Nothing Then

					Me.RemovePostSave("HMIBooleanChannelsCollectionByFinalValue")
					Me._HMIBooleanChannelsCollectionByFinalValue = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _HMIBooleanChannelsCollectionByFinalValue As HMIBooleanChannelsCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "HMIBooleanChannelsCollectionByInitialValue"
					coll = Me.HMIBooleanChannelsCollectionByInitialValue
					Exit Select
				Case "HMIBooleanChannelsCollectionByFinalValue"
					coll = Me.HMIBooleanChannelsCollectionByFinalValue
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "HMIBooleanChannelsCollectionByInitialValue", GetType(HMIBooleanChannelsCollection), New HMIBooleanChannels()))
			props.Add(new esPropertyDescriptor(Me, "HMIBooleanChannelsCollectionByFinalValue", GetType(HMIBooleanChannelsCollection), New HMIBooleanChannels()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class BooleanStateMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(BooleanStateMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = BooleanStateMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(BooleanStateMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = BooleanStateMetadata.PropertyNames.Description
			c.CharacterMaxLength = 10
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As BooleanStateMetadata
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
			SyncLock GetType(BooleanStateMetadata)
			
				If BooleanStateMetadata.mapDelegates Is Nothing Then
					BooleanStateMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If BooleanStateMetadata._meta Is Nothing Then
					BooleanStateMetadata._meta = New BooleanStateMetadata()
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
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "BooleanState"
				meta.Destination = "BooleanState"
				
				meta.spInsert = "proc_BooleanStateInsert"
				meta.spUpdate = "proc_BooleanStateUpdate"
				meta.spDelete = "proc_BooleanStateDelete"
				meta.spLoadAll = "proc_BooleanStateLoadAll"
				meta.spLoadByPrimaryKey = "proc_BooleanStateLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As BooleanStateMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

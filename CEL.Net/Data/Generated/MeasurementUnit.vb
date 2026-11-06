
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 10/25/2019 9:14:18 AM
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
	' Encapsulates the 'MeasurementUnit' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(MeasurementUnit))> _
	<XmlType("MeasurementUnit")> _	
	Partial Public Class MeasurementUnit 
		Inherits esMeasurementUnit
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New MeasurementUnit()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal measurementUnitId As System.Int32)
			Dim obj As New MeasurementUnit()
			obj.MeasurementUnitId = measurementUnitId
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal measurementUnitId As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New MeasurementUnit()
			obj.MeasurementUnitId = measurementUnitId
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("MeasurementUnitCollection")> _
	Partial Public Class MeasurementUnitCollection
		Inherits esMeasurementUnitCollection
		Implements IEnumerable(Of MeasurementUnit)
	
		Public Function FindByPrimaryKey(ByVal measurementUnitId As System.Int32) As MeasurementUnit
			Return MyBase.SingleOrDefault(Function(e) e.MeasurementUnitId.HasValue AndAlso e.MeasurementUnitId.Value = measurementUnitId)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(MeasurementUnit))> _
		Public Class MeasurementUnitCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of MeasurementUnitCollection)
			
			Public Shared Widening Operator CType(packet As MeasurementUnitCollectionWCFPacket) As MeasurementUnitCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As MeasurementUnitCollection) As MeasurementUnitCollectionWCFPacket
				Return New MeasurementUnitCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class MeasurementUnitQuery 
		Inherits esMeasurementUnitQuery

        Public Sub New()
        End Sub

        Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "MeasurementUnitQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As MeasurementUnitQuery) As String
			Return MeasurementUnitQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As MeasurementUnitQuery
			Return DirectCast(MeasurementUnitQuery.SerializeHelper.FromXml(query, GetType(MeasurementUnitQuery)), MeasurementUnitQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esMeasurementUnit
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal measurementUnitId As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(measurementUnitId)
			Else
				Return LoadByPrimaryKeyStoredProcedure(measurementUnitId)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal measurementUnitId As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(measurementUnitId)
			Else
				Return LoadByPrimaryKeyStoredProcedure(measurementUnitId)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal measurementUnitId As System.Int32) As Boolean
		
			Dim query As New MeasurementUnitQuery()
			query.Where(query.MeasurementUnitId = measurementUnitId)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal measurementUnitId As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("MeasurementUnitId", measurementUnitId)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to MeasurementUnit.MeasurementUnitId
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MeasurementUnitId As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(MeasurementUnitMetadata.ColumnNames.MeasurementUnitId)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(MeasurementUnitMetadata.ColumnNames.MeasurementUnitId, value) Then
					OnPropertyChanged(MeasurementUnitMetadata.PropertyNames.MeasurementUnitId)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to MeasurementUnit.MeasurementUnitDescription
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MeasurementUnitDescription As System.String
			Get
				Return MyBase.GetSystemString(MeasurementUnitMetadata.ColumnNames.MeasurementUnitDescription)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(MeasurementUnitMetadata.ColumnNames.MeasurementUnitDescription, value) Then
					OnPropertyChanged(MeasurementUnitMetadata.PropertyNames.MeasurementUnitDescription)
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
												
						Case "MeasurementUnitId"
							Me.str().MeasurementUnitId = CType(value, string)
												
						Case "MeasurementUnitDescription"
							Me.str().MeasurementUnitDescription = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "MeasurementUnitId"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.MeasurementUnitId = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(MeasurementUnitMetadata.PropertyNames.MeasurementUnitId)
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
		
			Public Sub New(ByVal entity As esMeasurementUnit)
				Me.entity = entity
			End Sub				
		
	
			Public Property MeasurementUnitId As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.MeasurementUnitId
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MeasurementUnitId = Nothing
					Else
						entity.MeasurementUnitId = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MeasurementUnitDescription As System.String 
				Get
					Dim data_ As System.String = entity.MeasurementUnitDescription
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MeasurementUnitDescription = Nothing
					Else
						entity.MeasurementUnitDescription = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esMeasurementUnit
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return MeasurementUnitMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As MeasurementUnitQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New MeasurementUnitQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As MeasurementUnitQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As MeasurementUnitQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As MeasurementUnitQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esMeasurementUnitCollection
		Inherits esEntityCollection(Of MeasurementUnit)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return MeasurementUnitMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "MeasurementUnitCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As MeasurementUnitQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New MeasurementUnitQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As MeasurementUnitQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New MeasurementUnitQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As MeasurementUnitQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, MeasurementUnitQuery))
		End Sub
		
		#End Region
						
		Private m_query As MeasurementUnitQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esMeasurementUnitQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return MeasurementUnitMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "MeasurementUnitId" 
					Return Me.MeasurementUnitId
				Case "MeasurementUnitDescription" 
					Return Me.MeasurementUnitDescription
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property MeasurementUnitId As esQueryItem
			Get
				Return New esQueryItem(Me, MeasurementUnitMetadata.ColumnNames.MeasurementUnitId, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MeasurementUnitDescription As esQueryItem
			Get
				Return New esQueryItem(Me, MeasurementUnitMetadata.ColumnNames.MeasurementUnitDescription, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class MeasurementUnit 
		Inherits esMeasurementUnit
		
	
		#Region "UUTHMIWindowCollectionByMeasurementUnitId - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTHMIWindowCollectionByMeasurementUnitId() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.MeasurementUnit.UUTHMIWindowCollectionByMeasurementUnitId_Delegate)
				map.PropertyName = "UUTHMIWindowCollectionByMeasurementUnitId"
				map.MyColumnName = "MeasurementUnitId"
				map.ParentColumnName = "MeasurementUnitId"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub UUTHMIWindowCollectionByMeasurementUnitId_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New MeasurementUnitQuery(data.NextAlias())
			
			Dim mee As UUTHMIWindowQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTHMIWindowQuery), New UUTHMIWindowQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.MeasurementUnitId = mee.MeasurementUnitId)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - MeasurementUnitUUT_HMI_Window
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTHMIWindowCollectionByMeasurementUnitId As UUTHMIWindowCollection 
		
			Get
				If Me._UUTHMIWindowCollectionByMeasurementUnitId Is Nothing Then
					Me._UUTHMIWindowCollectionByMeasurementUnitId = New UUTHMIWindowCollection()
					Me._UUTHMIWindowCollectionByMeasurementUnitId.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTHMIWindowCollectionByMeasurementUnitId", Me._UUTHMIWindowCollectionByMeasurementUnitId)
				
					If Not Me.MeasurementUnitId.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTHMIWindowCollectionByMeasurementUnitId.Query.Where(Me._UUTHMIWindowCollectionByMeasurementUnitId.Query.MeasurementUnitId = Me.MeasurementUnitId)
							Me._UUTHMIWindowCollectionByMeasurementUnitId.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTHMIWindowCollectionByMeasurementUnitId.fks.Add(UUTHMIWindowMetadata.ColumnNames.MeasurementUnitId, Me.MeasurementUnitId)
					End If
				End If

				Return Me._UUTHMIWindowCollectionByMeasurementUnitId
			End Get
			
			Set(ByVal value As UUTHMIWindowCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTHMIWindowCollectionByMeasurementUnitId Is Nothing Then

					Me.RemovePostSave("UUTHMIWindowCollectionByMeasurementUnitId")
					Me._UUTHMIWindowCollectionByMeasurementUnitId = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTHMIWindowCollectionByMeasurementUnitId As UUTHMIWindowCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "UUTHMIWindowCollectionByMeasurementUnitId"
					coll = Me.UUTHMIWindowCollectionByMeasurementUnitId
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "UUTHMIWindowCollectionByMeasurementUnitId", GetType(UUTHMIWindowCollection), New UUTHMIWindow()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class MeasurementUnitMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(MeasurementUnitMetadata.ColumnNames.MeasurementUnitId, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = MeasurementUnitMetadata.PropertyNames.MeasurementUnitId
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(MeasurementUnitMetadata.ColumnNames.MeasurementUnitDescription, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = MeasurementUnitMetadata.PropertyNames.MeasurementUnitDescription
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As MeasurementUnitMetadata
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
			 Public Const MeasurementUnitId As String = "MeasurementUnitId"
			 Public Const MeasurementUnitDescription As String = "MeasurementUnitDescription"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const MeasurementUnitId As String = "MeasurementUnitId"
			 Public Const MeasurementUnitDescription As String = "MeasurementUnitDescription"
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
			SyncLock GetType(MeasurementUnitMetadata)
			
				If MeasurementUnitMetadata.mapDelegates Is Nothing Then
					MeasurementUnitMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If MeasurementUnitMetadata._meta Is Nothing Then
					MeasurementUnitMetadata._meta = New MeasurementUnitMetadata()
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
				


				meta.AddTypeMap("MeasurementUnitId", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MeasurementUnitDescription", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "MeasurementUnit"
				meta.Destination = "MeasurementUnit"
				
				meta.spInsert = "proc_MeasurementUnitInsert"
				meta.spUpdate = "proc_MeasurementUnitUpdate"
				meta.spDelete = "proc_MeasurementUnitDelete"
				meta.spLoadAll = "proc_MeasurementUnitLoadAll"
				meta.spLoadByPrimaryKey = "proc_MeasurementUnitLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As MeasurementUnitMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

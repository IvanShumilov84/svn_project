
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
	' Encapsulates the 'Engine_Series' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(EngineSeries))> _
	<XmlType("EngineSeries")> _	
	Partial Public Class EngineSeries 
		Inherits esEngineSeries
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New EngineSeries()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal engineSeries As System.String)
			Dim obj As New EngineSeries()
			obj.EngineSeries = engineSeries
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal engineSeries As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New EngineSeries()
			obj.EngineSeries = engineSeries
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("EngineSeriesCollection")> _
	Partial Public Class EngineSeriesCollection
		Inherits esEngineSeriesCollection
		Implements IEnumerable(Of EngineSeries)
	
		Public Function FindByPrimaryKey(ByVal engineSeries As System.String) As EngineSeries
			Return MyBase.SingleOrDefault(Function(e) e.EngineSeries = engineSeries)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(EngineSeries))> _
		Public Class EngineSeriesCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of EngineSeriesCollection)
			
			Public Shared Widening Operator CType(packet As EngineSeriesCollectionWCFPacket) As EngineSeriesCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As EngineSeriesCollection) As EngineSeriesCollectionWCFPacket
				Return New EngineSeriesCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class EngineSeriesQuery 
		Inherits esEngineSeriesQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "EngineSeriesQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As EngineSeriesQuery) As String
			Return EngineSeriesQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As EngineSeriesQuery
			Return DirectCast(EngineSeriesQuery.SerializeHelper.FromXml(query, GetType(EngineSeriesQuery)), EngineSeriesQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esEngineSeries
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal engineSeries As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineSeries)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineSeries)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal engineSeries As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineSeries)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineSeries)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal engineSeries As System.String) As Boolean
		
			Dim query As New EngineSeriesQuery()
			query.Where(query.EngineSeries = engineSeries)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal engineSeries As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("EngineSeries", engineSeries)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to Engine_Series.EngineSeries
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineSeries As System.String
			Get
				Return MyBase.GetSystemString(EngineSeriesMetadata.ColumnNames.EngineSeries)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EngineSeriesMetadata.ColumnNames.EngineSeries, value) Then
					OnPropertyChanged(EngineSeriesMetadata.PropertyNames.EngineSeries)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Engine_Series.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(EngineSeriesMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EngineSeriesMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(EngineSeriesMetadata.PropertyNames.Description)
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
												
						Case "EngineSeries"
							Me.str().EngineSeries = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
					
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
		
			Public Sub New(ByVal entity As esEngineSeries)
				Me.entity = entity
			End Sub				
		
	
			Public Property EngineSeries As System.String 
				Get
					Dim data_ As System.String = entity.EngineSeries
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EngineSeries = Nothing
					Else
						entity.EngineSeries = Convert.ToString(Value)
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
		  

			Private entity As esEngineSeries
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return EngineSeriesMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As EngineSeriesQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New EngineSeriesQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As EngineSeriesQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As EngineSeriesQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As EngineSeriesQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esEngineSeriesCollection
		Inherits esEntityCollection(Of EngineSeries)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return EngineSeriesMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "EngineSeriesCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As EngineSeriesQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New EngineSeriesQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As EngineSeriesQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New EngineSeriesQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As EngineSeriesQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, EngineSeriesQuery))
		End Sub
		
		#End Region
						
		Private m_query As EngineSeriesQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esEngineSeriesQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return EngineSeriesMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "EngineSeries" 
					Return Me.EngineSeries
				Case "Description" 
					Return Me.Description
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property EngineSeries As esQueryItem
			Get
				Return New esQueryItem(Me, EngineSeriesMetadata.ColumnNames.EngineSeries, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, EngineSeriesMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class EngineSeries 
		Inherits esEngineSeries
		
	
		#Region "UpToGTCP85129RepairTypeCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - Engine_SeriesGTCP85_129
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToGTCP85129RepairTypeCollection As GTCP85129RepairTypeCollection
		
			Get
				If Me._UpToGTCP85129RepairTypeCollection Is Nothing Then
					Me._UpToGTCP85129RepairTypeCollection = New GTCP85129RepairTypeCollection()
					Me._UpToGTCP85129RepairTypeCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToGTCP85129RepairTypeCollection", Me._UpToGTCP85129RepairTypeCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.EngineSeries.Equals(Nothing) Then 
				
						Dim m As New GTCP85129RepairTypeQuery("m")
						Dim j As New Gtcp85129Query("j")
						m.Select(m)
						m.InnerJoin(j).On(m.Id = j.RepairTypeID)
                        m.Where(j.EngineSeries = Me.EngineSeries)

						Me._UpToGTCP85129RepairTypeCollection.Load(m)

					End If
				End If

				Return Me._UpToGTCP85129RepairTypeCollection
			End Get
			
			Set(ByVal value As GTCP85129RepairTypeCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToGTCP85129RepairTypeCollection Is Nothing Then

					Me.RemovePostSave("UpToGTCP85129RepairTypeCollection")
					Me._UpToGTCP85129RepairTypeCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - Engine_SeriesGTCP85_129
		''' </summary>
		Public Sub AssociateGTCP85129RepairTypeCollection(entity As GTCP85129RepairType)
			If Me._Gtcp85129Collection Is Nothing Then
				Me._Gtcp85129Collection = New Gtcp85129Collection()
				Me._Gtcp85129Collection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("Gtcp85129Collection", Me._Gtcp85129Collection)
			End If
			
			Dim obj As Gtcp85129 = Me._Gtcp85129Collection.AddNew()
			obj.EngineSeries = Me.EngineSeries
			obj.RepairTypeID = entity.Id			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - Engine_SeriesGTCP85_129
		''' </summary>
		Public Sub DissociateGTCP85129RepairTypeCollection(entity As GTCP85129RepairType)
			If Me._Gtcp85129Collection Is Nothing Then
				Me._Gtcp85129Collection = new Gtcp85129Collection()
				Me._Gtcp85129Collection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("Gtcp85129Collection", Me._Gtcp85129Collection)
			End If

			Dim obj As Gtcp85129 = Me._Gtcp85129Collection.AddNew()
			obj.EngineSeries = Me.EngineSeries
            obj.RepairTypeID = entity.Id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToGTCP85129RepairTypeCollection As GTCP85129RepairTypeCollection
		Private _Gtcp85129Collection As Gtcp85129Collection
		#End Region

		#Region "Gtcp85129CollectionByEngineSeries - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_Gtcp85129CollectionByEngineSeries() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.EngineSeries.Gtcp85129CollectionByEngineSeries_Delegate)
				map.PropertyName = "Gtcp85129CollectionByEngineSeries"
				map.MyColumnName = "EngineSeries"
				map.ParentColumnName = "EngineSeries"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub Gtcp85129CollectionByEngineSeries_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New EngineSeriesQuery(data.NextAlias())
			
			Dim mee As Gtcp85129Query = If(data.You IsNot Nothing, TryCast(data.You, Gtcp85129Query), New Gtcp85129Query(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.EngineSeries = mee.EngineSeries)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Engine_SeriesGTCP85_129
		''' </summary>

		<XmlIgnore()> _ 
		Public Property Gtcp85129CollectionByEngineSeries As Gtcp85129Collection 
		
			Get
				If Me._Gtcp85129CollectionByEngineSeries Is Nothing Then
					Me._Gtcp85129CollectionByEngineSeries = New Gtcp85129Collection()
					Me._Gtcp85129CollectionByEngineSeries.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("Gtcp85129CollectionByEngineSeries", Me._Gtcp85129CollectionByEngineSeries)
				
					If Not Me.EngineSeries.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._Gtcp85129CollectionByEngineSeries.Query.Where(Me._Gtcp85129CollectionByEngineSeries.Query.EngineSeries = Me.EngineSeries)
							Me._Gtcp85129CollectionByEngineSeries.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._Gtcp85129CollectionByEngineSeries.fks.Add(Gtcp85129Metadata.ColumnNames.EngineSeries, Me.EngineSeries)
					End If
				End If

				Return Me._Gtcp85129CollectionByEngineSeries
			End Get
			
			Set(ByVal value As Gtcp85129Collection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._Gtcp85129CollectionByEngineSeries Is Nothing Then

					Me.RemovePostSave("Gtcp85129CollectionByEngineSeries")
					Me._Gtcp85129CollectionByEngineSeries = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _Gtcp85129CollectionByEngineSeries As Gtcp85129Collection
		#End Region

		#Region "UpToEngineInfoCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - Engine_SeriesUUT_Engine_Series
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToEngineInfoCollection As EngineInfoCollection
		
			Get
				If Me._UpToEngineInfoCollection Is Nothing Then
					Me._UpToEngineInfoCollection = New EngineInfoCollection()
					Me._UpToEngineInfoCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToEngineInfoCollection", Me._UpToEngineInfoCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.EngineSeries.Equals(Nothing) Then 
				
						Dim m As New EngineInfoQuery("m")
						Dim j As New UUTEngineSeriesQuery("j")
						m.Select(m)
						m.InnerJoin(j).On(m.EngineModel = j.EngineModel)
                        m.Where(j.EngineSeries = Me.EngineSeries)

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
		''' Foreign Key Name - Engine_SeriesUUT_Engine_Series
		''' </summary>
		Public Sub AssociateEngineInfoCollection(entity As EngineInfo)
			If Me._UUTEngineSeriesCollection Is Nothing Then
				Me._UUTEngineSeriesCollection = New UUTEngineSeriesCollection()
				Me._UUTEngineSeriesCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UUTEngineSeriesCollection", Me._UUTEngineSeriesCollection)
			End If
			
			Dim obj As UUTEngineSeries = Me._UUTEngineSeriesCollection.AddNew()
			obj.EngineSeries = Me.EngineSeries
			obj.EngineModel = entity.EngineModel			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - Engine_SeriesUUT_Engine_Series
		''' </summary>
		Public Sub DissociateEngineInfoCollection(entity As EngineInfo)
			If Me._UUTEngineSeriesCollection Is Nothing Then
				Me._UUTEngineSeriesCollection = new UUTEngineSeriesCollection()
				Me._UUTEngineSeriesCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UUTEngineSeriesCollection", Me._UUTEngineSeriesCollection)
			End If

			Dim obj As UUTEngineSeries = Me._UUTEngineSeriesCollection.AddNew()
			obj.EngineSeries = Me.EngineSeries
            obj.EngineModel = entity.EngineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToEngineInfoCollection As EngineInfoCollection
		Private _UUTEngineSeriesCollection As UUTEngineSeriesCollection
		#End Region

		#Region "UUTEngineSeriesCollectionByEngineSeries - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTEngineSeriesCollectionByEngineSeries() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.EngineSeries.UUTEngineSeriesCollectionByEngineSeries_Delegate)
				map.PropertyName = "UUTEngineSeriesCollectionByEngineSeries"
				map.MyColumnName = "EngineSeries"
				map.ParentColumnName = "EngineSeries"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub UUTEngineSeriesCollectionByEngineSeries_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New EngineSeriesQuery(data.NextAlias())
			
			Dim mee As UUTEngineSeriesQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTEngineSeriesQuery), New UUTEngineSeriesQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.EngineSeries = mee.EngineSeries)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Engine_SeriesUUT_Engine_Series
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTEngineSeriesCollectionByEngineSeries As UUTEngineSeriesCollection 
		
			Get
				If Me._UUTEngineSeriesCollectionByEngineSeries Is Nothing Then
					Me._UUTEngineSeriesCollectionByEngineSeries = New UUTEngineSeriesCollection()
					Me._UUTEngineSeriesCollectionByEngineSeries.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTEngineSeriesCollectionByEngineSeries", Me._UUTEngineSeriesCollectionByEngineSeries)
				
					If Not Me.EngineSeries.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTEngineSeriesCollectionByEngineSeries.Query.Where(Me._UUTEngineSeriesCollectionByEngineSeries.Query.EngineSeries = Me.EngineSeries)
							Me._UUTEngineSeriesCollectionByEngineSeries.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTEngineSeriesCollectionByEngineSeries.fks.Add(UUTEngineSeriesMetadata.ColumnNames.EngineSeries, Me.EngineSeries)
					End If
				End If

				Return Me._UUTEngineSeriesCollectionByEngineSeries
			End Get
			
			Set(ByVal value As UUTEngineSeriesCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTEngineSeriesCollectionByEngineSeries Is Nothing Then

					Me.RemovePostSave("UUTEngineSeriesCollectionByEngineSeries")
					Me._UUTEngineSeriesCollectionByEngineSeries = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTEngineSeriesCollectionByEngineSeries As UUTEngineSeriesCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "Gtcp85129CollectionByEngineSeries"
					coll = Me.Gtcp85129CollectionByEngineSeries
					Exit Select
				Case "UUTEngineSeriesCollectionByEngineSeries"
					coll = Me.UUTEngineSeriesCollectionByEngineSeries
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "Gtcp85129CollectionByEngineSeries", GetType(Gtcp85129Collection), New Gtcp85129()))
			props.Add(new esPropertyDescriptor(Me, "UUTEngineSeriesCollectionByEngineSeries", GetType(UUTEngineSeriesCollection), New UUTEngineSeries()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class EngineSeriesMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(EngineSeriesMetadata.ColumnNames.EngineSeries, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = EngineSeriesMetadata.PropertyNames.EngineSeries
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			m_columns.Add(c)
				
			c = New esColumnMetadata(EngineSeriesMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = EngineSeriesMetadata.PropertyNames.Description
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As EngineSeriesMetadata
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
			 Public Const EngineSeries As String = "EngineSeries"
			 Public Const Description As String = "Description"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const EngineSeries As String = "EngineSeries"
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
			SyncLock GetType(EngineSeriesMetadata)
			
				If EngineSeriesMetadata.mapDelegates Is Nothing Then
					EngineSeriesMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If EngineSeriesMetadata._meta Is Nothing Then
					EngineSeriesMetadata._meta = New EngineSeriesMetadata()
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
				


				meta.AddTypeMap("EngineSeries", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "Engine_Series"
				meta.Destination = "Engine_Series"
				
				meta.spInsert = "proc_Engine_SeriesInsert"
				meta.spUpdate = "proc_Engine_SeriesUpdate"
				meta.spDelete = "proc_Engine_SeriesDelete"
				meta.spLoadAll = "proc_Engine_SeriesLoadAll"
				meta.spLoadByPrimaryKey = "proc_Engine_SeriesLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As EngineSeriesMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

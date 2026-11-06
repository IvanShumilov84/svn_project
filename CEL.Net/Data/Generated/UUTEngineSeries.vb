
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
	' Encapsulates the 'UUT_Engine_Series' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTEngineSeries))> _
	<XmlType("UUTEngineSeries")> _	
	Partial Public Class UUTEngineSeries 
		Inherits esUUTEngineSeries
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTEngineSeries()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal engineSeries As System.String, ByVal engineModel As System.String)
			Dim obj As New UUTEngineSeries()
			obj.EngineSeries = engineSeries
			obj.EngineModel = engineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal engineSeries As System.String, ByVal engineModel As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTEngineSeries()
			obj.EngineSeries = engineSeries
			obj.EngineModel = engineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTEngineSeriesCollection")> _
	Partial Public Class UUTEngineSeriesCollection
		Inherits esUUTEngineSeriesCollection
		Implements IEnumerable(Of UUTEngineSeries)
	
		Public Function FindByPrimaryKey(ByVal engineSeries As System.String, ByVal engineModel As System.String) As UUTEngineSeries
			Return MyBase.SingleOrDefault(Function(e) e.EngineSeries = engineSeries And e.EngineModel = engineModel)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTEngineSeries))> _
		Public Class UUTEngineSeriesCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTEngineSeriesCollection)
			
			Public Shared Widening Operator CType(packet As UUTEngineSeriesCollectionWCFPacket) As UUTEngineSeriesCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTEngineSeriesCollection) As UUTEngineSeriesCollectionWCFPacket
				Return New UUTEngineSeriesCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTEngineSeriesQuery 
		Inherits esUUTEngineSeriesQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTEngineSeriesQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTEngineSeriesQuery) As String
			Return UUTEngineSeriesQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTEngineSeriesQuery
			Return DirectCast(UUTEngineSeriesQuery.SerializeHelper.FromXml(query, GetType(UUTEngineSeriesQuery)), UUTEngineSeriesQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTEngineSeries
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal engineSeries As System.String, ByVal engineModel As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineSeries, engineModel)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineSeries, engineModel)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal engineSeries As System.String, ByVal engineModel As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineSeries, engineModel)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineSeries, engineModel)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal engineSeries As System.String, ByVal engineModel As System.String) As Boolean
		
			Dim query As New UUTEngineSeriesQuery()
			query.Where(query.EngineSeries = engineSeries And query.EngineModel = engineModel)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal engineSeries As System.String, ByVal engineModel As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("EngineSeries", engineSeries)
						parms.Add("EngineModel", engineModel)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_Engine_Series.EngineSeries
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineSeries As System.String
			Get
				Return MyBase.GetSystemString(UUTEngineSeriesMetadata.ColumnNames.EngineSeries)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTEngineSeriesMetadata.ColumnNames.EngineSeries, value) Then
					Me._UpToEngineSeriesByEngineSeries = Nothing
					Me.OnPropertyChanged("UpToEngineSeriesByEngineSeries")
					OnPropertyChanged(UUTEngineSeriesMetadata.PropertyNames.EngineSeries)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Engine_Series.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UUTEngineSeriesMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTEngineSeriesMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToEngineInfoByEngineModel = Nothing
					Me.OnPropertyChanged("UpToEngineInfoByEngineModel")
					OnPropertyChanged(UUTEngineSeriesMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Engine_Series.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(UUTEngineSeriesMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTEngineSeriesMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(UUTEngineSeriesMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToEngineInfoByEngineModel As EngineInfo
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToEngineSeriesByEngineSeries As EngineSeries
		
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
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
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
		
			Public Sub New(ByVal entity As esUUTEngineSeries)
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
		  

			Private entity As esUUTEngineSeries
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTEngineSeriesMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTEngineSeriesQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTEngineSeriesQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTEngineSeriesQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTEngineSeriesQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTEngineSeriesQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTEngineSeriesCollection
		Inherits esEntityCollection(Of UUTEngineSeries)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTEngineSeriesMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTEngineSeriesCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTEngineSeriesQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTEngineSeriesQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTEngineSeriesQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTEngineSeriesQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTEngineSeriesQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTEngineSeriesQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTEngineSeriesQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTEngineSeriesQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTEngineSeriesMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "EngineSeries" 
					Return Me.EngineSeries
				Case "EngineModel" 
					Return Me.EngineModel
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
				Return New esQueryItem(Me, UUTEngineSeriesMetadata.ColumnNames.EngineSeries, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UUTEngineSeriesMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, UUTEngineSeriesMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTEngineSeries 
		Inherits esUUTEngineSeries
		
	
		#Region "UpToEngineInfoByEngineModel - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Engine_InfoUUT_Engine_Series
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToEngineInfoByEngineModel As EngineInfo
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToEngineInfoByEngineModel Is Nothing _
						 AndAlso Not EngineModel.Equals(Nothing)  Then
						
					Me._UpToEngineInfoByEngineModel = New EngineInfo()
					Me._UpToEngineInfoByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToEngineInfoByEngineModel", Me._UpToEngineInfoByEngineModel)
					Me._UpToEngineInfoByEngineModel.Query.Where(Me._UpToEngineInfoByEngineModel.Query.EngineModel = Me.EngineModel)
					Me._UpToEngineInfoByEngineModel.Query.Load()
				End If

				Return Me._UpToEngineInfoByEngineModel
			End Get
			
            Set(ByVal value As EngineInfo)
				Me.RemovePreSave("UpToEngineInfoByEngineModel")
				

				If value Is Nothing Then
				
					Me.EngineModel = Nothing
				
					Me._UpToEngineInfoByEngineModel = Nothing
				Else
				
					Me.EngineModel = value.EngineModel
					
					Me._UpToEngineInfoByEngineModel = value
					Me.SetPreSave("UpToEngineInfoByEngineModel", Me._UpToEngineInfoByEngineModel)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToEngineSeriesByEngineSeries - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Engine_SeriesUUT_Engine_Series
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToEngineSeriesByEngineSeries As EngineSeries
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToEngineSeriesByEngineSeries Is Nothing _
						 AndAlso Not EngineSeries.Equals(Nothing)  Then
						
					Me._UpToEngineSeriesByEngineSeries = New EngineSeries()
					Me._UpToEngineSeriesByEngineSeries.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToEngineSeriesByEngineSeries", Me._UpToEngineSeriesByEngineSeries)
					Me._UpToEngineSeriesByEngineSeries.Query.Where(Me._UpToEngineSeriesByEngineSeries.Query.EngineSeries = Me.EngineSeries)
					Me._UpToEngineSeriesByEngineSeries.Query.Load()
				End If

				Return Me._UpToEngineSeriesByEngineSeries
			End Get
			
            Set(ByVal value As EngineSeries)
				Me.RemovePreSave("UpToEngineSeriesByEngineSeries")
				

				If value Is Nothing Then
				
					Me.EngineSeries = Nothing
				
					Me._UpToEngineSeriesByEngineSeries = Nothing
				Else
				
					Me.EngineSeries = value.EngineSeries
					
					Me._UpToEngineSeriesByEngineSeries = value
					Me.SetPreSave("UpToEngineSeriesByEngineSeries", Me._UpToEngineSeriesByEngineSeries)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class UUTEngineSeriesMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTEngineSeriesMetadata.ColumnNames.EngineSeries, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTEngineSeriesMetadata.PropertyNames.EngineSeries
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTEngineSeriesMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTEngineSeriesMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTEngineSeriesMetadata.ColumnNames.Description, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTEngineSeriesMetadata.PropertyNames.Description
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTEngineSeriesMetadata
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
			 Public Const EngineModel As String = "EngineModel"
			 Public Const Description As String = "Description"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const EngineSeries As String = "EngineSeries"
			 Public Const EngineModel As String = "EngineModel"
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
			SyncLock GetType(UUTEngineSeriesMetadata)
			
				If UUTEngineSeriesMetadata.mapDelegates Is Nothing Then
					UUTEngineSeriesMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTEngineSeriesMetadata._meta Is Nothing Then
					UUTEngineSeriesMetadata._meta = New UUTEngineSeriesMetadata()
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
				meta.AddTypeMap("EngineModel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "UUT_Engine_Series"
				meta.Destination = "UUT_Engine_Series"
				
				meta.spInsert = "proc_UUT_Engine_SeriesInsert"
				meta.spUpdate = "proc_UUT_Engine_SeriesUpdate"
				meta.spDelete = "proc_UUT_Engine_SeriesDelete"
				meta.spLoadAll = "proc_UUT_Engine_SeriesLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_Engine_SeriesLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTEngineSeriesMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

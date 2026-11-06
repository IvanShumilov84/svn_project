
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 1/7/2014 12:51:50
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
	' Encapsulates the 'LoadCells' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(LoadCells))> _
	<XmlType("LoadCells")> _	
	Partial Public Class LoadCells 
		Inherits esLoadCells
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New LoadCells()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal project As System.Int32, ByVal source As System.String)
			Dim obj As New LoadCells()
			obj.Project = project
			obj.Source = source
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal project As System.Int32, ByVal source As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New LoadCells()
			obj.Project = project
			obj.Source = source
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("LoadCellsCollection")> _
	Partial Public Class LoadCellsCollection
		Inherits esLoadCellsCollection
		Implements IEnumerable(Of LoadCells)
	
		Public Function FindByPrimaryKey(ByVal project As System.Int32, ByVal source As System.String) As LoadCells
			Return MyBase.SingleOrDefault(Function(e) e.Project.HasValue AndAlso e.Project.Value = project And e.Source = source)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(LoadCells))> _
		Public Class LoadCellsCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of LoadCellsCollection)
			
			Public Shared Widening Operator CType(packet As LoadCellsCollectionWCFPacket) As LoadCellsCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As LoadCellsCollection) As LoadCellsCollectionWCFPacket
				Return New LoadCellsCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class LoadCellsQuery 
		Inherits esLoadCellsQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "LoadCellsQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As LoadCellsQuery) As String
			Return LoadCellsQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As LoadCellsQuery
			Return DirectCast(LoadCellsQuery.SerializeHelper.FromXml(query, GetType(LoadCellsQuery)), LoadCellsQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esLoadCells
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal project As System.Int32, ByVal source As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(project, source)
			Else
				Return LoadByPrimaryKeyStoredProcedure(project, source)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal project As System.Int32, ByVal source As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(project, source)
			Else
				Return LoadByPrimaryKeyStoredProcedure(project, source)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal project As System.Int32, ByVal source As System.String) As Boolean
		
			Dim query As New LoadCellsQuery()
			query.Where(query.Project = project And query.Source = source)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal project As System.Int32, ByVal source As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("Project", project)
						parms.Add("Source", source)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to LoadCells.Project
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Project As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(LoadCellsMetadata.ColumnNames.Project)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(LoadCellsMetadata.ColumnNames.Project, value) Then
					OnPropertyChanged(LoadCellsMetadata.PropertyNames.Project)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to LoadCells.Number
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Number As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(LoadCellsMetadata.ColumnNames.Number)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(LoadCellsMetadata.ColumnNames.Number, value) Then
					OnPropertyChanged(LoadCellsMetadata.PropertyNames.Number)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to LoadCells.Plug
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Plug As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(LoadCellsMetadata.ColumnNames.Plug)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(LoadCellsMetadata.ColumnNames.Plug, value) Then
					OnPropertyChanged(LoadCellsMetadata.PropertyNames.Plug)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to LoadCells.Source
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Source As System.String
			Get
				Return MyBase.GetSystemString(LoadCellsMetadata.ColumnNames.Source)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(LoadCellsMetadata.ColumnNames.Source, value) Then
					OnPropertyChanged(LoadCellsMetadata.PropertyNames.Source)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to LoadCells.SerialNumber
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SerialNumber As System.String
			Get
				Return MyBase.GetSystemString(LoadCellsMetadata.ColumnNames.SerialNumber)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(LoadCellsMetadata.ColumnNames.SerialNumber, value) Then
					OnPropertyChanged(LoadCellsMetadata.PropertyNames.SerialNumber)
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
												
						Case "Project"
							Me.str().Project = CType(value, string)
												
						Case "Number"
							Me.str().Number = CType(value, string)
												
						Case "Plug"
							Me.str().Plug = CType(value, string)
												
						Case "Source"
							Me.str().Source = CType(value, string)
												
						Case "SerialNumber"
							Me.str().SerialNumber = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Project"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Project = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(LoadCellsMetadata.PropertyNames.Project)
							End If
						
						Case "Number"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.Number = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(LoadCellsMetadata.PropertyNames.Number)
							End If
						
						Case "Plug"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.Plug = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(LoadCellsMetadata.PropertyNames.Plug)
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
		
			Public Sub New(ByVal entity As esLoadCells)
				Me.entity = entity
			End Sub				
		
	
			Public Property Project As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Project
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Project = Nothing
					Else
						entity.Project = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Number As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.Number
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Number = Nothing
					Else
						entity.Number = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property Plug As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.Plug
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Plug = Nothing
					Else
						entity.Plug = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property Source As System.String 
				Get
					Dim data_ As System.String = entity.Source
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Source = Nothing
					Else
						entity.Source = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property SerialNumber As System.String 
				Get
					Dim data_ As System.String = entity.SerialNumber
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SerialNumber = Nothing
					Else
						entity.SerialNumber = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esLoadCells
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return LoadCellsMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As LoadCellsQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New LoadCellsQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As LoadCellsQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As LoadCellsQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As LoadCellsQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esLoadCellsCollection
		Inherits esEntityCollection(Of LoadCells)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return LoadCellsMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "LoadCellsCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As LoadCellsQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New LoadCellsQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As LoadCellsQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New LoadCellsQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As LoadCellsQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, LoadCellsQuery))
		End Sub
		
		#End Region
						
		Private m_query As LoadCellsQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esLoadCellsQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return LoadCellsMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Project" 
					Return Me.Project
				Case "Number" 
					Return Me.Number
				Case "Plug" 
					Return Me.Plug
				Case "Source" 
					Return Me.Source
				Case "SerialNumber" 
					Return Me.SerialNumber
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Project As esQueryItem
			Get
				Return New esQueryItem(Me, LoadCellsMetadata.ColumnNames.Project, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Number As esQueryItem
			Get
				Return New esQueryItem(Me, LoadCellsMetadata.ColumnNames.Number, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property Plug As esQueryItem
			Get
				Return New esQueryItem(Me, LoadCellsMetadata.ColumnNames.Plug, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property Source As esQueryItem
			Get
				Return New esQueryItem(Me, LoadCellsMetadata.ColumnNames.Source, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property SerialNumber As esQueryItem
			Get
				Return New esQueryItem(Me, LoadCellsMetadata.ColumnNames.SerialNumber, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class LoadCells 
		Inherits esLoadCells
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class LoadCellsMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(LoadCellsMetadata.ColumnNames.Project, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = LoadCellsMetadata.PropertyNames.Project
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(LoadCellsMetadata.ColumnNames.Number, 1, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = LoadCellsMetadata.PropertyNames.Number
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "1"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(LoadCellsMetadata.ColumnNames.Plug, 2, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = LoadCellsMetadata.PropertyNames.Plug
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(LoadCellsMetadata.ColumnNames.Source, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = LoadCellsMetadata.PropertyNames.Source
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
			c = New esColumnMetadata(LoadCellsMetadata.ColumnNames.SerialNumber, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = LoadCellsMetadata.PropertyNames.SerialNumber
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As LoadCellsMetadata
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
			 Public Const Project As String = "Project"
			 Public Const Number As String = "Number"
			 Public Const Plug As String = "Plug"
			 Public Const Source As String = "Source"
			 Public Const SerialNumber As String = "SerialNumber"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Project As String = "Project"
			 Public Const Number As String = "Number"
			 Public Const Plug As String = "Plug"
			 Public Const Source As String = "Source"
			 Public Const SerialNumber As String = "SerialNumber"
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
			SyncLock GetType(LoadCellsMetadata)
			
				If LoadCellsMetadata.mapDelegates Is Nothing Then
					LoadCellsMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If LoadCellsMetadata._meta Is Nothing Then
					LoadCellsMetadata._meta = New LoadCellsMetadata()
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
				


				meta.AddTypeMap("Project", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Number", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("Plug", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("Source", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("SerialNumber", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "LoadCells"
				meta.Destination = "LoadCells"
				
				meta.spInsert = "proc_LoadCellsInsert"
				meta.spUpdate = "proc_LoadCellsUpdate"
				meta.spDelete = "proc_LoadCellsDelete"
				meta.spLoadAll = "proc_LoadCellsLoadAll"
				meta.spLoadByPrimaryKey = "proc_LoadCellsLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As LoadCellsMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

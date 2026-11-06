
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 5/23/2013 5:32:48 PM
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
	' Encapsulates the 'RetainVariables' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(RetainVariables))> _
	<XmlType("RetainVariables")> _	
	Partial Public Class RetainVariables 
		Inherits esRetainVariables
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New RetainVariables()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal key As System.Int32)
			Dim obj As New RetainVariables()
			obj.ProjectID = projectID
			obj.Key = key
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal key As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New RetainVariables()
			obj.ProjectID = projectID
			obj.Key = key
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("RetainVariablesCollection")> _
	Partial Public Class RetainVariablesCollection
		Inherits esRetainVariablesCollection
		Implements IEnumerable(Of RetainVariables)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal key As System.Int32) As RetainVariables
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.Key.HasValue AndAlso e.Key.Value = key)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(RetainVariables))> _
		Public Class RetainVariablesCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of RetainVariablesCollection)
			
			Public Shared Widening Operator CType(packet As RetainVariablesCollectionWCFPacket) As RetainVariablesCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As RetainVariablesCollection) As RetainVariablesCollectionWCFPacket
				Return New RetainVariablesCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class RetainVariablesQuery 
		Inherits esRetainVariablesQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "RetainVariablesQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As RetainVariablesQuery) As String
			Return RetainVariablesQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As RetainVariablesQuery
			Return DirectCast(RetainVariablesQuery.SerializeHelper.FromXml(query, GetType(RetainVariablesQuery)), RetainVariablesQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esRetainVariables
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal key As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, key)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, key)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal key As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, key)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, key)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal key As System.Int32) As Boolean
		
			Dim query As New RetainVariablesQuery()
			query.Where(query.ProjectID = projectID And query.Key = key)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal key As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("Key", key)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to RetainVariables.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(RetainVariablesMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(RetainVariablesMetadata.ColumnNames.ProjectID, value) Then
					OnPropertyChanged(RetainVariablesMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to RetainVariables.Key
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Key As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(RetainVariablesMetadata.ColumnNames.Key)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(RetainVariablesMetadata.ColumnNames.Key, value) Then
					OnPropertyChanged(RetainVariablesMetadata.PropertyNames.Key)
				End If
			End Set
		End Property	
			
		' <summary>
		' Name of the variable in the workbench
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property VariableName As System.String
			Get
				Return MyBase.GetSystemString(RetainVariablesMetadata.ColumnNames.VariableName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(RetainVariablesMetadata.ColumnNames.VariableName, value) Then
					OnPropertyChanged(RetainVariablesMetadata.PropertyNames.VariableName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Value of the this variable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Value As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(RetainVariablesMetadata.ColumnNames.Value)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(RetainVariablesMetadata.ColumnNames.Value, value) Then
					OnPropertyChanged(RetainVariablesMetadata.PropertyNames.Value)
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
												
						Case "ProjectID"
							Me.str().ProjectID = CType(value, string)
												
						Case "Key"
							Me.str().Key = CType(value, string)
												
						Case "VariableName"
							Me.str().VariableName = CType(value, string)
												
						Case "Value"
							Me.str().Value = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(RetainVariablesMetadata.PropertyNames.ProjectID)
							End If
						
						Case "Key"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Key = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(RetainVariablesMetadata.PropertyNames.Key)
							End If
						
						Case "Value"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.Value = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(RetainVariablesMetadata.PropertyNames.Value)
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
		
			Public Sub New(ByVal entity As esRetainVariables)
				Me.entity = entity
			End Sub				
		
	
			Public Property ProjectID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.ProjectID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ProjectID = Nothing
					Else
						entity.ProjectID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Key As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Key
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Key = Nothing
					Else
						entity.Key = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property VariableName As System.String 
				Get
					Dim data_ As System.String = entity.VariableName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.VariableName = Nothing
					Else
						entity.VariableName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Value As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.Value
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Value = Nothing
					Else
						entity.Value = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  

			Private entity As esRetainVariables
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return RetainVariablesMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As RetainVariablesQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New RetainVariablesQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As RetainVariablesQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As RetainVariablesQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As RetainVariablesQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esRetainVariablesCollection
		Inherits esEntityCollection(Of RetainVariables)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return RetainVariablesMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "RetainVariablesCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As RetainVariablesQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New RetainVariablesQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As RetainVariablesQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New RetainVariablesQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As RetainVariablesQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, RetainVariablesQuery))
		End Sub
		
		#End Region
						
		Private m_query As RetainVariablesQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esRetainVariablesQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return RetainVariablesMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "Key" 
					Return Me.Key
				Case "VariableName" 
					Return Me.VariableName
				Case "Value" 
					Return Me.Value
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, RetainVariablesMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Key As esQueryItem
			Get
				Return New esQueryItem(Me, RetainVariablesMetadata.ColumnNames.Key, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property VariableName As esQueryItem
			Get
				Return New esQueryItem(Me, RetainVariablesMetadata.ColumnNames.VariableName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Value As esQueryItem
			Get
				Return New esQueryItem(Me, RetainVariablesMetadata.ColumnNames.Value, esSystemType.Double)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class RetainVariables 
		Inherits esRetainVariables
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class RetainVariablesMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(RetainVariablesMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = RetainVariablesMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(RetainVariablesMetadata.ColumnNames.Key, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = RetainVariablesMetadata.PropertyNames.Key
			c.IsInPrimaryKey = True
			c.IsAutoIncrement = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(RetainVariablesMetadata.ColumnNames.VariableName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = RetainVariablesMetadata.PropertyNames.VariableName
			c.CharacterMaxLength = 50
			c.Description = "Name of the variable in the workbench"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(RetainVariablesMetadata.ColumnNames.Value, 3, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = RetainVariablesMetadata.PropertyNames.Value
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Value of the this variable"
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As RetainVariablesMetadata
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
			 Public Const ProjectID As String = "ProjectID"
			 Public Const Key As String = "Key"
			 Public Const VariableName As String = "Variable_Name"
			 Public Const Value As String = "Value"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const Key As String = "Key"
			 Public Const VariableName As String = "VariableName"
			 Public Const Value As String = "Value"
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
			SyncLock GetType(RetainVariablesMetadata)
			
				If RetainVariablesMetadata.mapDelegates Is Nothing Then
					RetainVariablesMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If RetainVariablesMetadata._meta Is Nothing Then
					RetainVariablesMetadata._meta = New RetainVariablesMetadata()
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
				


				meta.AddTypeMap("ProjectID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Key", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("VariableName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Value", new esTypeMap("Double", "System.Double"))			
				
				
				 
				meta.Source = "RetainVariables"
				meta.Destination = "RetainVariables"
				
				meta.spInsert = "proc_RetainVariablesInsert"
				meta.spUpdate = "proc_RetainVariablesUpdate"
				meta.spDelete = "proc_RetainVariablesDelete"
				meta.spLoadAll = "proc_RetainVariablesLoadAll"
				meta.spLoadByPrimaryKey = "proc_RetainVariablesLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As RetainVariablesMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

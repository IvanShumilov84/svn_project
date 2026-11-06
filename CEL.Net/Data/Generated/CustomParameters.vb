
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 06/02/2014 8:29:34 AM
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
	' Encapsulates the 'Custom_Parameters' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(CustomParameters))> _
	<XmlType("CustomParameters")> _	
	Partial Public Class CustomParameters 
		Inherits esCustomParameters
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New CustomParameters()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal parameterName As System.String)
			Dim obj As New CustomParameters()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.ParameterName = parameterName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal parameterName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New CustomParameters()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.ParameterName = parameterName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("CustomParametersCollection")> _
	Partial Public Class CustomParametersCollection
		Inherits esCustomParametersCollection
		Implements IEnumerable(Of CustomParameters)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal parameterName As System.String) As CustomParameters
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.ParameterName = parameterName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(CustomParameters))> _
		Public Class CustomParametersCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of CustomParametersCollection)
			
			Public Shared Widening Operator CType(packet As CustomParametersCollectionWCFPacket) As CustomParametersCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As CustomParametersCollection) As CustomParametersCollectionWCFPacket
				Return New CustomParametersCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class CustomParametersQuery 
		Inherits esCustomParametersQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "CustomParametersQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As CustomParametersQuery) As String
			Return CustomParametersQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As CustomParametersQuery
			Return DirectCast(CustomParametersQuery.SerializeHelper.FromXml(query, GetType(CustomParametersQuery)), CustomParametersQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esCustomParameters
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal parameterName As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, parameterName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, parameterName)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal parameterName As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, parameterName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, parameterName)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal parameterName As System.String) As Boolean
		
			Dim query As New CustomParametersQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.ParameterName = parameterName)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal parameterName As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
						parms.Add("ParameterName", parameterName)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to Custom_Parameters.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(CustomParametersMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(CustomParametersMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(CustomParametersMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Custom_Parameters.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(CustomParametersMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(CustomParametersMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(CustomParametersMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Custom_Parameters.ParameterName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ParameterName As System.String
			Get
				Return MyBase.GetSystemString(CustomParametersMetadata.ColumnNames.ParameterName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(CustomParametersMetadata.ColumnNames.ParameterName, value) Then
					OnPropertyChanged(CustomParametersMetadata.PropertyNames.ParameterName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Custom_Parameters.Text
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Text As System.String
			Get
				Return MyBase.GetSystemString(CustomParametersMetadata.ColumnNames.Text)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(CustomParametersMetadata.ColumnNames.Text, value) Then
					OnPropertyChanged(CustomParametersMetadata.PropertyNames.Text)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Custom_Parameters.Available
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Available As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(CustomParametersMetadata.ColumnNames.Available)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(CustomParametersMetadata.ColumnNames.Available, value) Then
					OnPropertyChanged(CustomParametersMetadata.PropertyNames.Available)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToUutByProjectID As Uut
		
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
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
						Case "ParameterName"
							Me.str().ParameterName = CType(value, string)
												
						Case "Text"
							Me.str().Text = CType(value, string)
												
						Case "Available"
							Me.str().Available = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(CustomParametersMetadata.PropertyNames.ProjectID)
							End If
						
						Case "Available"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Available = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(CustomParametersMetadata.PropertyNames.Available)
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
		
			Public Sub New(ByVal entity As esCustomParameters)
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
		  	
			Public Property ParameterName As System.String 
				Get
					Dim data_ As System.String = entity.ParameterName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ParameterName = Nothing
					Else
						entity.ParameterName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Text As System.String 
				Get
					Dim data_ As System.String = entity.Text
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Text = Nothing
					Else
						entity.Text = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Available As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Available
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Available = Nothing
					Else
						entity.Available = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  

			Private entity As esCustomParameters
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return CustomParametersMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As CustomParametersQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New CustomParametersQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As CustomParametersQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As CustomParametersQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As CustomParametersQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esCustomParametersCollection
		Inherits esEntityCollection(Of CustomParameters)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return CustomParametersMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "CustomParametersCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As CustomParametersQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New CustomParametersQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As CustomParametersQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New CustomParametersQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As CustomParametersQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, CustomParametersQuery))
		End Sub
		
		#End Region
						
		Private m_query As CustomParametersQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esCustomParametersQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return CustomParametersMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "ParameterName" 
					Return Me.ParameterName
				Case "Text" 
					Return Me.Text
				Case "Available" 
					Return Me.Available
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, CustomParametersMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, CustomParametersMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ParameterName As esQueryItem
			Get
				Return New esQueryItem(Me, CustomParametersMetadata.ColumnNames.ParameterName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Text As esQueryItem
			Get
				Return New esQueryItem(Me, CustomParametersMetadata.ColumnNames.Text, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Available As esQueryItem
			Get
				Return New esQueryItem(Me, CustomParametersMetadata.ColumnNames.Available, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class CustomParameters 
		Inherits esCustomParameters
		
	
		#Region "UpToUutByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUTCustom_Parameters
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToUutByProjectID As Uut
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToUutByProjectID Is Nothing _
						 AndAlso Not ProjectID.Equals(Nothing)  AndAlso Not EngineModel.Equals(Nothing)  Then
						
					Me._UpToUutByProjectID = New Uut()
					Me._UpToUutByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToUutByProjectID", Me._UpToUutByProjectID)
					Me._UpToUutByProjectID.Query.Where(Me._UpToUutByProjectID.Query.ProjectID = Me.ProjectID)
					Me._UpToUutByProjectID.Query.Where(Me._UpToUutByProjectID.Query.EngineModel = Me.EngineModel)
					Me._UpToUutByProjectID.Query.Load()
				End If

				Return Me._UpToUutByProjectID
			End Get
			
            Set(ByVal value As Uut)
				Me.RemovePreSave("UpToUutByProjectID")
				

				If value Is Nothing Then
				
					Me.ProjectID = Nothing
				
					Me.EngineModel = Nothing
				
					Me._UpToUutByProjectID = Nothing
				Else
				
					Me.ProjectID = value.ProjectID
					
					Me.EngineModel = value.EngineModel
					
					Me._UpToUutByProjectID = value
					Me.SetPreSave("UpToUutByProjectID", Me._UpToUutByProjectID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class CustomParametersMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(CustomParametersMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = CustomParametersMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(CustomParametersMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = CustomParametersMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(CustomParametersMetadata.ColumnNames.ParameterName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = CustomParametersMetadata.PropertyNames.ParameterName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(CustomParametersMetadata.ColumnNames.Text, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = CustomParametersMetadata.PropertyNames.Text
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(CustomParametersMetadata.ColumnNames.Available, 4, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = CustomParametersMetadata.PropertyNames.Available
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As CustomParametersMetadata
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
			 Public Const EngineModel As String = "EngineModel"
			 Public Const ParameterName As String = "ParameterName"
			 Public Const Text As String = "Text"
			 Public Const Available As String = "Available"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const ParameterName As String = "ParameterName"
			 Public Const Text As String = "Text"
			 Public Const Available As String = "Available"
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
			SyncLock GetType(CustomParametersMetadata)
			
				If CustomParametersMetadata.mapDelegates Is Nothing Then
					CustomParametersMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If CustomParametersMetadata._meta Is Nothing Then
					CustomParametersMetadata._meta = New CustomParametersMetadata()
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
				meta.AddTypeMap("EngineModel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ParameterName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Text", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Available", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "Custom_Parameters"
				meta.Destination = "Custom_Parameters"
				
				meta.spInsert = "proc_Custom_ParametersInsert"
				meta.spUpdate = "proc_Custom_ParametersUpdate"
				meta.spDelete = "proc_Custom_ParametersDelete"
				meta.spLoadAll = "proc_Custom_ParametersLoadAll"
				meta.spLoadByPrimaryKey = "proc_Custom_ParametersLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As CustomParametersMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

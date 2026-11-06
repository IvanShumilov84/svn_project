
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 05/02/2014 8:29:26 AM
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
	' Encapsulates the 'Users' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(Users))> _
	<XmlType("Users")> _	
	Partial Public Class Users 
		Inherits esUsers
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New Users()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal operatorID As System.Int32)
			Dim obj As New Users()
			obj.ProjectID = projectID
			obj.OperatorID = operatorID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal operatorID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New Users()
			obj.ProjectID = projectID
			obj.OperatorID = operatorID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UsersCollection")> _
	Partial Public Class UsersCollection
		Inherits esUsersCollection
		Implements IEnumerable(Of Users)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal operatorID As System.Int32) As Users
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.OperatorID.HasValue AndAlso e.OperatorID.Value = operatorID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(Users))> _
		Public Class UsersCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UsersCollection)
			
			Public Shared Widening Operator CType(packet As UsersCollectionWCFPacket) As UsersCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UsersCollection) As UsersCollectionWCFPacket
				Return New UsersCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UsersQuery 
		Inherits esUsersQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UsersQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UsersQuery) As String
			Return UsersQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UsersQuery
			Return DirectCast(UsersQuery.SerializeHelper.FromXml(query, GetType(UsersQuery)), UsersQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUsers
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal operatorID As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, operatorID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, operatorID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal operatorID As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, operatorID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, operatorID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal operatorID As System.Int32) As Boolean
		
			Dim query As New UsersQuery()
			query.Where(query.ProjectID = projectID And query.OperatorID = operatorID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal operatorID As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("OperatorID", operatorID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to Users.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UsersMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UsersMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToProjectInfoByProjectID = Nothing
					Me.OnPropertyChanged("UpToProjectInfoByProjectID")
					OnPropertyChanged(UsersMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Users.OperatorID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OperatorID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UsersMetadata.ColumnNames.OperatorID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UsersMetadata.ColumnNames.OperatorID, value) Then
					OnPropertyChanged(UsersMetadata.PropertyNames.OperatorID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Users.Password
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Password As System.String
			Get
				Return MyBase.GetSystemString(UsersMetadata.ColumnNames.Password)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UsersMetadata.ColumnNames.Password, value) Then
					OnPropertyChanged(UsersMetadata.PropertyNames.Password)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Users.OperatorName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OperatorName As System.String
			Get
				Return MyBase.GetSystemString(UsersMetadata.ColumnNames.OperatorName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UsersMetadata.ColumnNames.OperatorName, value) Then
					OnPropertyChanged(UsersMetadata.PropertyNames.OperatorName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Users.SecurityLevelID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SecurityLevelID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UsersMetadata.ColumnNames.SecurityLevelID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UsersMetadata.ColumnNames.SecurityLevelID, value) Then
					Me._UpToSecurityLevelBySecurityLevelID = Nothing
					Me.OnPropertyChanged("UpToSecurityLevelBySecurityLevelID")
					OnPropertyChanged(UsersMetadata.PropertyNames.SecurityLevelID)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToProjectInfoByProjectID As ProjectInfo
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToSecurityLevelBySecurityLevelID As SecurityLevel
		
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
												
						Case "OperatorID"
							Me.str().OperatorID = CType(value, string)
												
						Case "Password"
							Me.str().Password = CType(value, string)
												
						Case "OperatorName"
							Me.str().OperatorName = CType(value, string)
												
						Case "SecurityLevelID"
							Me.str().SecurityLevelID = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UsersMetadata.PropertyNames.ProjectID)
							End If
						
						Case "OperatorID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.OperatorID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UsersMetadata.PropertyNames.OperatorID)
							End If
						
						Case "SecurityLevelID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SecurityLevelID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UsersMetadata.PropertyNames.SecurityLevelID)
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
		
			Public Sub New(ByVal entity As esUsers)
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
		  	
			Public Property OperatorID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.OperatorID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OperatorID = Nothing
					Else
						entity.OperatorID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Password As System.String 
				Get
					Dim data_ As System.String = entity.Password
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Password = Nothing
					Else
						entity.Password = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property OperatorName As System.String 
				Get
					Dim data_ As System.String = entity.OperatorName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OperatorName = Nothing
					Else
						entity.OperatorName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property SecurityLevelID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SecurityLevelID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SecurityLevelID = Nothing
					Else
						entity.SecurityLevelID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  

			Private entity As esUsers
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UsersMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UsersQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UsersQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UsersQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UsersQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UsersQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUsersCollection
		Inherits esEntityCollection(Of Users)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UsersMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UsersCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UsersQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UsersQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UsersQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UsersQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UsersQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UsersQuery))
		End Sub
		
		#End Region
						
		Private m_query As UsersQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUsersQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UsersMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "OperatorID" 
					Return Me.OperatorID
				Case "Password" 
					Return Me.Password
				Case "OperatorName" 
					Return Me.OperatorName
				Case "SecurityLevelID" 
					Return Me.SecurityLevelID
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, UsersMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property OperatorID As esQueryItem
			Get
				Return New esQueryItem(Me, UsersMetadata.ColumnNames.OperatorID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Password As esQueryItem
			Get
				Return New esQueryItem(Me, UsersMetadata.ColumnNames.Password, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OperatorName As esQueryItem
			Get
				Return New esQueryItem(Me, UsersMetadata.ColumnNames.OperatorName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property SecurityLevelID As esQueryItem
			Get
				Return New esQueryItem(Me, UsersMetadata.ColumnNames.SecurityLevelID, esSystemType.Int32)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class Users 
		Inherits esUsers
		
	
		#Region "UpToProjectInfoByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Project_InfoUsers
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToProjectInfoByProjectID As ProjectInfo
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToProjectInfoByProjectID Is Nothing _
						 AndAlso Not ProjectID.Equals(Nothing)  Then
						
					Me._UpToProjectInfoByProjectID = New ProjectInfo()
					Me._UpToProjectInfoByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToProjectInfoByProjectID", Me._UpToProjectInfoByProjectID)
					Me._UpToProjectInfoByProjectID.Query.Where(Me._UpToProjectInfoByProjectID.Query.ProjectID = Me.ProjectID)
					Me._UpToProjectInfoByProjectID.Query.Load()
				End If

				Return Me._UpToProjectInfoByProjectID
			End Get
			
            Set(ByVal value As ProjectInfo)
				Me.RemovePreSave("UpToProjectInfoByProjectID")
				

				If value Is Nothing Then
				
					Me.ProjectID = Nothing
				
					Me._UpToProjectInfoByProjectID = Nothing
				Else
				
					Me.ProjectID = value.ProjectID
					
					Me._UpToProjectInfoByProjectID = value
					Me.SetPreSave("UpToProjectInfoByProjectID", Me._UpToProjectInfoByProjectID)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToSecurityLevelBySecurityLevelID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Security LevelUsers
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToSecurityLevelBySecurityLevelID As SecurityLevel
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToSecurityLevelBySecurityLevelID Is Nothing _
						 AndAlso Not SecurityLevelID.Equals(Nothing)  Then
						
					Me._UpToSecurityLevelBySecurityLevelID = New SecurityLevel()
					Me._UpToSecurityLevelBySecurityLevelID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToSecurityLevelBySecurityLevelID", Me._UpToSecurityLevelBySecurityLevelID)
					Me._UpToSecurityLevelBySecurityLevelID.Query.Where(Me._UpToSecurityLevelBySecurityLevelID.Query.SecurityLevelID = Me.SecurityLevelID)
					Me._UpToSecurityLevelBySecurityLevelID.Query.Load()
				End If

				Return Me._UpToSecurityLevelBySecurityLevelID
			End Get
			
            Set(ByVal value As SecurityLevel)
				Me.RemovePreSave("UpToSecurityLevelBySecurityLevelID")
				

				If value Is Nothing Then
				
					Me.SecurityLevelID = Nothing
				
					Me._UpToSecurityLevelBySecurityLevelID = Nothing
				Else
				
					Me.SecurityLevelID = value.SecurityLevelID
					
					Me._UpToSecurityLevelBySecurityLevelID = value
					Me.SetPreSave("UpToSecurityLevelBySecurityLevelID", Me._UpToSecurityLevelBySecurityLevelID)
				End If
				
			End Set	

		End Property
		#End Region

		
			
		''' <summary>
		''' Used internally for retrieving AutoIncrementing keys
		''' during hierarchical PreSave.
		''' </summary>
		Protected Overrides Sub ApplyPreSaveKeys()
		
			If Not Me.es.IsDeleted And Not Me._UpToSecurityLevelBySecurityLevelID Is Nothing Then
				Me.SecurityLevelID = Me._UpToSecurityLevelBySecurityLevelID.SecurityLevelID
			End If
			
		End Sub
	End Class
		



	<Serializable> _
	Partial Public Class UsersMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UsersMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UsersMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UsersMetadata.ColumnNames.OperatorID, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UsersMetadata.PropertyNames.OperatorID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UsersMetadata.ColumnNames.Password, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = UsersMetadata.PropertyNames.Password
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UsersMetadata.ColumnNames.OperatorName, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = UsersMetadata.PropertyNames.OperatorName
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UsersMetadata.ColumnNames.SecurityLevelID, 4, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UsersMetadata.PropertyNames.SecurityLevelID
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UsersMetadata
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
			 Public Const OperatorID As String = "OperatorID"
			 Public Const Password As String = "Password"
			 Public Const OperatorName As String = "OperatorName"
			 Public Const SecurityLevelID As String = "SecurityLevelID"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const OperatorID As String = "OperatorID"
			 Public Const Password As String = "Password"
			 Public Const OperatorName As String = "OperatorName"
			 Public Const SecurityLevelID As String = "SecurityLevelID"
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
			SyncLock GetType(UsersMetadata)
			
				If UsersMetadata.mapDelegates Is Nothing Then
					UsersMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UsersMetadata._meta Is Nothing Then
					UsersMetadata._meta = New UsersMetadata()
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
				meta.AddTypeMap("OperatorID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Password", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OperatorName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("SecurityLevelID", new esTypeMap("Long", "System.Int32"))			
				
				
				 
				meta.Source = "Users"
				meta.Destination = "Users"
				
				meta.spInsert = "proc_UsersInsert"
				meta.spUpdate = "proc_UsersUpdate"
				meta.spDelete = "proc_UsersDelete"
				meta.spLoadAll = "proc_UsersLoadAll"
				meta.spLoadByPrimaryKey = "proc_UsersLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UsersMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

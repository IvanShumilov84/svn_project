
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 2015-09-28 11:30:08 AM
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
	' Encapsulates the 'Engine_PwServiceBulletin' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(EnginePwServiceBulletin))> _
	<XmlType("EnginePwServiceBulletin")> _	
	Partial Public Class EnginePwServiceBulletin 
		Inherits esEnginePwServiceBulletin
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New EnginePwServiceBulletin()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal engineModel As System.String, ByVal id As System.Int16)
			Dim obj As New EnginePwServiceBulletin()
			obj.EngineModel = engineModel
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal engineModel As System.String, ByVal id As System.Int16, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New EnginePwServiceBulletin()
			obj.EngineModel = engineModel
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
	<XmlType("EnginePwServiceBulletinCollection")> _
	Partial Public Class EnginePwServiceBulletinCollection
		Inherits esEnginePwServiceBulletinCollection
		Implements IEnumerable(Of EnginePwServiceBulletin)
	
		Public Function FindByPrimaryKey(ByVal engineModel As System.String, ByVal id As System.Int16) As EnginePwServiceBulletin
			Return MyBase.SingleOrDefault(Function(e) e.EngineModel = engineModel And e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(EnginePwServiceBulletin))> _
		Public Class EnginePwServiceBulletinCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of EnginePwServiceBulletinCollection)
			
			Public Shared Widening Operator CType(packet As EnginePwServiceBulletinCollectionWCFPacket) As EnginePwServiceBulletinCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As EnginePwServiceBulletinCollection) As EnginePwServiceBulletinCollectionWCFPacket
				Return New EnginePwServiceBulletinCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class EnginePwServiceBulletinQuery 
		Inherits esEnginePwServiceBulletinQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "EnginePwServiceBulletinQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As EnginePwServiceBulletinQuery) As String
			Return EnginePwServiceBulletinQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As EnginePwServiceBulletinQuery
			Return DirectCast(EnginePwServiceBulletinQuery.SerializeHelper.FromXml(query, GetType(EnginePwServiceBulletinQuery)), EnginePwServiceBulletinQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esEnginePwServiceBulletin
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal engineModel As System.String, ByVal id As System.Int16) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineModel, id)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineModel, id)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal engineModel As System.String, ByVal id As System.Int16) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineModel, id)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineModel, id)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal engineModel As System.String, ByVal id As System.Int16) As Boolean
		
			Dim query As New EnginePwServiceBulletinQuery()
			query.Where(query.EngineModel = engineModel And query.Id = id)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal engineModel As System.String, ByVal id As System.Int16) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("EngineModel", engineModel)
						parms.Add("Id", id)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to Engine_PwServiceBulletin.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(EnginePwServiceBulletinMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EnginePwServiceBulletinMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToEngineInfoByEngineModel = Nothing
					Me.OnPropertyChanged("UpToEngineInfoByEngineModel")
					OnPropertyChanged(EnginePwServiceBulletinMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Engine_PwServiceBulletin.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(EnginePwServiceBulletinMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(EnginePwServiceBulletinMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(EnginePwServiceBulletinMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Engine_PwServiceBulletin.ServiceBulletin1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ServiceBulletin1 As System.String
			Get
				Return MyBase.GetSystemString(EnginePwServiceBulletinMetadata.ColumnNames.ServiceBulletin1)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EnginePwServiceBulletinMetadata.ColumnNames.ServiceBulletin1, value) Then
					OnPropertyChanged(EnginePwServiceBulletinMetadata.PropertyNames.ServiceBulletin1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Engine_PwServiceBulletin.ServiceBulletin2
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ServiceBulletin2 As System.String
			Get
				Return MyBase.GetSystemString(EnginePwServiceBulletinMetadata.ColumnNames.ServiceBulletin2)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EnginePwServiceBulletinMetadata.ColumnNames.ServiceBulletin2, value) Then
					OnPropertyChanged(EnginePwServiceBulletinMetadata.PropertyNames.ServiceBulletin2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Engine_PwServiceBulletin.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(EnginePwServiceBulletinMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EnginePwServiceBulletinMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(EnginePwServiceBulletinMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Engine_PwServiceBulletin.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(EnginePwServiceBulletinMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(EnginePwServiceBulletinMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(EnginePwServiceBulletinMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToEngineInfoByEngineModel As EngineInfo
		
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
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
						Case "Id"
							Me.str().Id = CType(value, string)
												
						Case "ServiceBulletin1"
							Me.str().ServiceBulletin1 = CType(value, string)
												
						Case "ServiceBulletin2"
							Me.str().ServiceBulletin2 = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.Id = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(EnginePwServiceBulletinMetadata.PropertyNames.Id)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(EnginePwServiceBulletinMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esEnginePwServiceBulletin)
				Me.entity = entity
			End Sub				
		
	
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
		  	
			Public Property ServiceBulletin1 As System.String 
				Get
					Dim data_ As System.String = entity.ServiceBulletin1
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ServiceBulletin1 = Nothing
					Else
						entity.ServiceBulletin1 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ServiceBulletin2 As System.String 
				Get
					Dim data_ As System.String = entity.ServiceBulletin2
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ServiceBulletin2 = Nothing
					Else
						entity.ServiceBulletin2 = Convert.ToString(Value)
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
		  	
			Public Property Unavailable As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Unavailable
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Unavailable = Nothing
					Else
						entity.Unavailable = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  

			Private entity As esEnginePwServiceBulletin
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return EnginePwServiceBulletinMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As EnginePwServiceBulletinQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New EnginePwServiceBulletinQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As EnginePwServiceBulletinQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As EnginePwServiceBulletinQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As EnginePwServiceBulletinQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esEnginePwServiceBulletinCollection
		Inherits esEntityCollection(Of EnginePwServiceBulletin)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return EnginePwServiceBulletinMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "EnginePwServiceBulletinCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As EnginePwServiceBulletinQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New EnginePwServiceBulletinQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As EnginePwServiceBulletinQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New EnginePwServiceBulletinQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As EnginePwServiceBulletinQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, EnginePwServiceBulletinQuery))
		End Sub
		
		#End Region
						
		Private m_query As EnginePwServiceBulletinQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esEnginePwServiceBulletinQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return EnginePwServiceBulletinMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "EngineModel" 
					Return Me.EngineModel
				Case "Id" 
					Return Me.Id
				Case "ServiceBulletin1" 
					Return Me.ServiceBulletin1
				Case "ServiceBulletin2" 
					Return Me.ServiceBulletin2
				Case "Description" 
					Return Me.Description
				Case "Unavailable" 
					Return Me.Unavailable
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, EnginePwServiceBulletinMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, EnginePwServiceBulletinMetadata.ColumnNames.Id, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property ServiceBulletin1 As esQueryItem
			Get
				Return New esQueryItem(Me, EnginePwServiceBulletinMetadata.ColumnNames.ServiceBulletin1, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ServiceBulletin2 As esQueryItem
			Get
				Return New esQueryItem(Me, EnginePwServiceBulletinMetadata.ColumnNames.ServiceBulletin2, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, EnginePwServiceBulletinMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, EnginePwServiceBulletinMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class EnginePwServiceBulletin 
		Inherits esEnginePwServiceBulletin
		
	
		#Region "UpToEngineInfoByEngineModel - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Engine_InfoEngine_ServiceBulletin
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

		
		
	End Class
		



	<Serializable> _
	Partial Public Class EnginePwServiceBulletinMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(EnginePwServiceBulletinMetadata.ColumnNames.EngineModel, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = EnginePwServiceBulletinMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			m_columns.Add(c)
				
			c = New esColumnMetadata(EnginePwServiceBulletinMetadata.ColumnNames.Id, 1, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = EnginePwServiceBulletinMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(EnginePwServiceBulletinMetadata.ColumnNames.ServiceBulletin1, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = EnginePwServiceBulletinMetadata.PropertyNames.ServiceBulletin1
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
			c = New esColumnMetadata(EnginePwServiceBulletinMetadata.ColumnNames.ServiceBulletin2, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = EnginePwServiceBulletinMetadata.PropertyNames.ServiceBulletin2
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
			c = New esColumnMetadata(EnginePwServiceBulletinMetadata.ColumnNames.Description, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = EnginePwServiceBulletinMetadata.PropertyNames.Description
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(EnginePwServiceBulletinMetadata.ColumnNames.Unavailable, 5, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = EnginePwServiceBulletinMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As EnginePwServiceBulletinMetadata
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
			 Public Const EngineModel As String = "EngineModel"
			 Public Const Id As String = "ID"
			 Public Const ServiceBulletin1 As String = "ServiceBulletin1"
			 Public Const ServiceBulletin2 As String = "ServiceBulletin2"
			 Public Const Description As String = "Description"
			 Public Const Unavailable As String = "Unavailable"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const EngineModel As String = "EngineModel"
			 Public Const Id As String = "Id"
			 Public Const ServiceBulletin1 As String = "ServiceBulletin1"
			 Public Const ServiceBulletin2 As String = "ServiceBulletin2"
			 Public Const Description As String = "Description"
			 Public Const Unavailable As String = "Unavailable"
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
			SyncLock GetType(EnginePwServiceBulletinMetadata)
			
				If EnginePwServiceBulletinMetadata.mapDelegates Is Nothing Then
					EnginePwServiceBulletinMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If EnginePwServiceBulletinMetadata._meta Is Nothing Then
					EnginePwServiceBulletinMetadata._meta = New EnginePwServiceBulletinMetadata()
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
				


				meta.AddTypeMap("EngineModel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Id", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("ServiceBulletin1", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ServiceBulletin2", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "Engine_PwServiceBulletin"
				meta.Destination = "Engine_PwServiceBulletin"
				
				meta.spInsert = "proc_Engine_PwServiceBulletinInsert"
				meta.spUpdate = "proc_Engine_PwServiceBulletinUpdate"
				meta.spDelete = "proc_Engine_PwServiceBulletinDelete"
				meta.spLoadAll = "proc_Engine_PwServiceBulletinLoadAll"
				meta.spLoadByPrimaryKey = "proc_Engine_PwServiceBulletinLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As EnginePwServiceBulletinMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

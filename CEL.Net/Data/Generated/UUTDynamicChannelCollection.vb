
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 19/11/2013 2:05:24 PM
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
	' Encapsulates the 'UUT_Dynamic_Channel_Collection' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTDynamicChannelCollection))> _
	<XmlType("UUTDynamicChannelCollection")> _	
	Partial Public Class UUTDynamicChannelCollection 
		Inherits esUUTDynamicChannelCollection
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTDynamicChannelCollection()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal collectionName As System.String)
			Dim obj As New UUTDynamicChannelCollection()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.CollectionName = collectionName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal collectionName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTDynamicChannelCollection()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.CollectionName = collectionName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTDynamicChannelCollectionCollection")> _
	Partial Public Class UUTDynamicChannelCollectionCollection
		Inherits esUUTDynamicChannelCollectionCollection
		Implements IEnumerable(Of UUTDynamicChannelCollection)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal collectionName As System.String) As UUTDynamicChannelCollection
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.CollectionName = collectionName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTDynamicChannelCollection))> _
		Public Class UUTDynamicChannelCollectionCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTDynamicChannelCollectionCollection)
			
			Public Shared Widening Operator CType(packet As UUTDynamicChannelCollectionCollectionWCFPacket) As UUTDynamicChannelCollectionCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTDynamicChannelCollectionCollection) As UUTDynamicChannelCollectionCollectionWCFPacket
				Return New UUTDynamicChannelCollectionCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTDynamicChannelCollectionQuery 
		Inherits esUUTDynamicChannelCollectionQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTDynamicChannelCollectionQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTDynamicChannelCollectionQuery) As String
			Return UUTDynamicChannelCollectionQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTDynamicChannelCollectionQuery
			Return DirectCast(UUTDynamicChannelCollectionQuery.SerializeHelper.FromXml(query, GetType(UUTDynamicChannelCollectionQuery)), UUTDynamicChannelCollectionQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTDynamicChannelCollection
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal collectionName As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, collectionName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, collectionName)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal collectionName As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, collectionName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, collectionName)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal collectionName As System.String) As Boolean
		
			Dim query As New UUTDynamicChannelCollectionQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.CollectionName = collectionName)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal collectionName As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
						parms.Add("CollectionName", collectionName)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_Dynamic_Channel_Collection.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTDynamicChannelCollectionMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTDynamicChannelCollectionMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTDynamicChannelCollectionMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Dynamic_Channel_Collection.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UUTDynamicChannelCollectionMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTDynamicChannelCollectionMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTDynamicChannelCollectionMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Dynamic_Channel_Collection.CollectionName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CollectionName As System.String
			Get
				Return MyBase.GetSystemString(UUTDynamicChannelCollectionMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTDynamicChannelCollectionMetadata.ColumnNames.CollectionName, value) Then
					Me._UpToDynamicCollectionByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDynamicCollectionByCollectionName")
					OnPropertyChanged(UUTDynamicChannelCollectionMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Dynamic_Channel_Collection.LoadOrder
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LoadOrder As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTDynamicChannelCollectionMetadata.ColumnNames.LoadOrder)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTDynamicChannelCollectionMetadata.ColumnNames.LoadOrder, value) Then
					OnPropertyChanged(UUTDynamicChannelCollectionMetadata.PropertyNames.LoadOrder)
				End If
			End Set
		End Property	
			
		' <summary>
		' Désigne le nom que portera la collection et doit être unique dans l'explorer
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(UUTDynamicChannelCollectionMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTDynamicChannelCollectionMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(UUTDynamicChannelCollectionMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Dynamic_Channel_Collection.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTDynamicChannelCollectionMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTDynamicChannelCollectionMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(UUTDynamicChannelCollectionMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToDynamicCollectionByCollectionName As DynamicCollection
		
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
												
						Case "CollectionName"
							Me.str().CollectionName = CType(value, string)
												
						Case "LoadOrder"
							Me.str().LoadOrder = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTDynamicChannelCollectionMetadata.PropertyNames.ProjectID)
							End If
						
						Case "LoadOrder"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LoadOrder = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTDynamicChannelCollectionMetadata.PropertyNames.LoadOrder)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTDynamicChannelCollectionMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esUUTDynamicChannelCollection)
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
		  	
			Public Property CollectionName As System.String 
				Get
					Dim data_ As System.String = entity.CollectionName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CollectionName = Nothing
					Else
						entity.CollectionName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property LoadOrder As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.LoadOrder
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LoadOrder = Nothing
					Else
						entity.LoadOrder = Convert.ToInt32(Value)
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
		  

			Private entity As esUUTDynamicChannelCollection
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTDynamicChannelCollectionMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTDynamicChannelCollectionQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTDynamicChannelCollectionQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTDynamicChannelCollectionQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTDynamicChannelCollectionQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTDynamicChannelCollectionQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTDynamicChannelCollectionCollection
		Inherits esEntityCollection(Of UUTDynamicChannelCollection)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTDynamicChannelCollectionMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTDynamicChannelCollectionCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTDynamicChannelCollectionQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTDynamicChannelCollectionQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTDynamicChannelCollectionQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTDynamicChannelCollectionQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTDynamicChannelCollectionQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTDynamicChannelCollectionQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTDynamicChannelCollectionQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTDynamicChannelCollectionQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTDynamicChannelCollectionMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "CollectionName" 
					Return Me.CollectionName
				Case "LoadOrder" 
					Return Me.LoadOrder
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


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTDynamicChannelCollectionMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UUTDynamicChannelCollectionMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, UUTDynamicChannelCollectionMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LoadOrder As esQueryItem
			Get
				Return New esQueryItem(Me, UUTDynamicChannelCollectionMetadata.ColumnNames.LoadOrder, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, UUTDynamicChannelCollectionMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, UUTDynamicChannelCollectionMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTDynamicChannelCollection 
		Inherits esUUTDynamicChannelCollection
		
	
		#Region "UpToDynamicCollectionByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUT_Dynamic_CollectionUUT_Dynamic_Channel_Collection
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToDynamicCollectionByCollectionName As DynamicCollection
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToDynamicCollectionByCollectionName Is Nothing _
						 AndAlso Not CollectionName.Equals(Nothing)  Then
						
					Me._UpToDynamicCollectionByCollectionName = New DynamicCollection()
					Me._UpToDynamicCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToDynamicCollectionByCollectionName", Me._UpToDynamicCollectionByCollectionName)
					Me._UpToDynamicCollectionByCollectionName.Query.Where(Me._UpToDynamicCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
					Me._UpToDynamicCollectionByCollectionName.Query.Load()
				End If

				Return Me._UpToDynamicCollectionByCollectionName
			End Get
			
            Set(ByVal value As DynamicCollection)
				Me.RemovePreSave("UpToDynamicCollectionByCollectionName")
				

				If value Is Nothing Then
				
					Me.CollectionName = Nothing
				
					Me._UpToDynamicCollectionByCollectionName = Nothing
				Else
				
					Me.CollectionName = value.CollectionName
					
					Me._UpToDynamicCollectionByCollectionName = value
					Me.SetPreSave("UpToDynamicCollectionByCollectionName", Me._UpToDynamicCollectionByCollectionName)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToUutByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUTUUT_Dynamic_Channel_Collection
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
	Partial Public Class UUTDynamicChannelCollectionMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTDynamicChannelCollectionMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTDynamicChannelCollectionMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTDynamicChannelCollectionMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTDynamicChannelCollectionMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTDynamicChannelCollectionMetadata.ColumnNames.CollectionName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTDynamicChannelCollectionMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTDynamicChannelCollectionMetadata.ColumnNames.LoadOrder, 3, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTDynamicChannelCollectionMetadata.PropertyNames.LoadOrder
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTDynamicChannelCollectionMetadata.ColumnNames.Description, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTDynamicChannelCollectionMetadata.PropertyNames.Description
			c.CharacterMaxLength = 255
			c.Description = "Désigne le nom que portera la collection et doit être unique dans l'explorer"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTDynamicChannelCollectionMetadata.ColumnNames.Unavailable, 5, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTDynamicChannelCollectionMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTDynamicChannelCollectionMetadata
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
			 Public Const CollectionName As String = "CollectionName"
			 Public Const LoadOrder As String = "LoadOrder"
			 Public Const Description As String = "Description"
			 Public Const Unavailable As String = "Unavailable"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const CollectionName As String = "CollectionName"
			 Public Const LoadOrder As String = "LoadOrder"
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
			SyncLock GetType(UUTDynamicChannelCollectionMetadata)
			
				If UUTDynamicChannelCollectionMetadata.mapDelegates Is Nothing Then
					UUTDynamicChannelCollectionMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTDynamicChannelCollectionMetadata._meta Is Nothing Then
					UUTDynamicChannelCollectionMetadata._meta = New UUTDynamicChannelCollectionMetadata()
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
				meta.AddTypeMap("CollectionName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LoadOrder", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "UUT_Dynamic_Channel_Collection"
				meta.Destination = "UUT_Dynamic_Channel_Collection"
				
				meta.spInsert = "proc_UUT_Dynamic_Channel_CollectionInsert"
				meta.spUpdate = "proc_UUT_Dynamic_Channel_CollectionUpdate"
				meta.spDelete = "proc_UUT_Dynamic_Channel_CollectionDelete"
				meta.spLoadAll = "proc_UUT_Dynamic_Channel_CollectionLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_Dynamic_Channel_CollectionLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTDynamicChannelCollectionMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

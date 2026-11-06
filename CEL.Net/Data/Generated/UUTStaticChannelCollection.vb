
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
	' Encapsulates the 'UUT_Static_Channel_Collection' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTStaticChannelCollection))> _
	<XmlType("UUTStaticChannelCollection")> _	
	Partial Public Class UUTStaticChannelCollection 
		Inherits esUUTStaticChannelCollection
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTStaticChannelCollection()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal fileName As System.String)
			Dim obj As New UUTStaticChannelCollection()
			obj.CollectionName = collectionName
			obj.FileName = fileName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal fileName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTStaticChannelCollection()
			obj.CollectionName = collectionName
			obj.FileName = fileName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTStaticChannelCollectionCollection")> _
	Partial Public Class UUTStaticChannelCollectionCollection
		Inherits esUUTStaticChannelCollectionCollection
		Implements IEnumerable(Of UUTStaticChannelCollection)
	
		Public Function FindByPrimaryKey(ByVal collectionName As System.String, ByVal fileName As System.String) As UUTStaticChannelCollection
			Return MyBase.SingleOrDefault(Function(e) e.CollectionName = collectionName And e.FileName = fileName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTStaticChannelCollection))> _
		Public Class UUTStaticChannelCollectionCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTStaticChannelCollectionCollection)
			
			Public Shared Widening Operator CType(packet As UUTStaticChannelCollectionCollectionWCFPacket) As UUTStaticChannelCollectionCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTStaticChannelCollectionCollection) As UUTStaticChannelCollectionCollectionWCFPacket
				Return New UUTStaticChannelCollectionCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTStaticChannelCollectionQuery 
		Inherits esUUTStaticChannelCollectionQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTStaticChannelCollectionQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTStaticChannelCollectionQuery) As String
			Return UUTStaticChannelCollectionQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTStaticChannelCollectionQuery
			Return DirectCast(UUTStaticChannelCollectionQuery.SerializeHelper.FromXml(query, GetType(UUTStaticChannelCollectionQuery)), UUTStaticChannelCollectionQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTStaticChannelCollection
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal collectionName As System.String, ByVal fileName As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, fileName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, fileName)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal collectionName As System.String, ByVal fileName As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, fileName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, fileName)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal collectionName As System.String, ByVal fileName As System.String) As Boolean
		
			Dim query As New UUTStaticChannelCollectionQuery()
			query.Where(query.CollectionName = collectionName And query.FileName = fileName)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal collectionName As System.String, ByVal fileName As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("CollectionName", collectionName)
						parms.Add("FileName", fileName)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_Static_Channel_Collection.CollectionName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CollectionName As System.String
			Get
				Return MyBase.GetSystemString(UUTStaticChannelCollectionMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTStaticChannelCollectionMetadata.ColumnNames.CollectionName, value) Then
					Me._UpToDynamicCollectionByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDynamicCollectionByCollectionName")
					OnPropertyChanged(UUTStaticChannelCollectionMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Static_Channel_Collection.FileName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FileName As System.String
			Get
				Return MyBase.GetSystemString(UUTStaticChannelCollectionMetadata.ColumnNames.FileName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTStaticChannelCollectionMetadata.ColumnNames.FileName, value) Then
					OnPropertyChanged(UUTStaticChannelCollectionMetadata.PropertyNames.FileName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Désigne le nom que portera la collection et doit être unique dans l'explorer
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(UUTStaticChannelCollectionMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTStaticChannelCollectionMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(UUTStaticChannelCollectionMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToDynamicCollectionByCollectionName As DynamicCollection
		
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
												
						Case "CollectionName"
							Me.str().CollectionName = CType(value, string)
												
						Case "FileName"
							Me.str().FileName = CType(value, string)
												
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
		
			Public Sub New(ByVal entity As esUUTStaticChannelCollection)
				Me.entity = entity
			End Sub				
		
	
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
		  	
			Public Property FileName As System.String 
				Get
					Dim data_ As System.String = entity.FileName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FileName = Nothing
					Else
						entity.FileName = Convert.ToString(Value)
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
		  

			Private entity As esUUTStaticChannelCollection
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTStaticChannelCollectionMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTStaticChannelCollectionQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTStaticChannelCollectionQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTStaticChannelCollectionQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTStaticChannelCollectionQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTStaticChannelCollectionQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTStaticChannelCollectionCollection
		Inherits esEntityCollection(Of UUTStaticChannelCollection)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTStaticChannelCollectionMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTStaticChannelCollectionCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTStaticChannelCollectionQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTStaticChannelCollectionQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTStaticChannelCollectionQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTStaticChannelCollectionQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTStaticChannelCollectionQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTStaticChannelCollectionQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTStaticChannelCollectionQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTStaticChannelCollectionQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTStaticChannelCollectionMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "CollectionName" 
					Return Me.CollectionName
				Case "FileName" 
					Return Me.FileName
				Case "Description" 
					Return Me.Description
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, UUTStaticChannelCollectionMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FileName As esQueryItem
			Get
				Return New esQueryItem(Me, UUTStaticChannelCollectionMetadata.ColumnNames.FileName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, UUTStaticChannelCollectionMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTStaticChannelCollection 
		Inherits esUUTStaticChannelCollection
		
	
		#Region "UpToDynamicCollectionByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Dynamic_CollectionUUT_Static_Channel_Collection
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

		
		
	End Class
		



	<Serializable> _
	Partial Public Class UUTStaticChannelCollectionMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTStaticChannelCollectionMetadata.ColumnNames.CollectionName, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTStaticChannelCollectionMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTStaticChannelCollectionMetadata.ColumnNames.FileName, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTStaticChannelCollectionMetadata.PropertyNames.FileName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTStaticChannelCollectionMetadata.ColumnNames.Description, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTStaticChannelCollectionMetadata.PropertyNames.Description
			c.CharacterMaxLength = 255
			c.Description = "Désigne le nom que portera la collection et doit être unique dans l'explorer"
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTStaticChannelCollectionMetadata
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
			 Public Const CollectionName As String = "CollectionName"
			 Public Const FileName As String = "FileName"
			 Public Const Description As String = "Description"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const CollectionName As String = "CollectionName"
			 Public Const FileName As String = "FileName"
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
			SyncLock GetType(UUTStaticChannelCollectionMetadata)
			
				If UUTStaticChannelCollectionMetadata.mapDelegates Is Nothing Then
					UUTStaticChannelCollectionMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTStaticChannelCollectionMetadata._meta Is Nothing Then
					UUTStaticChannelCollectionMetadata._meta = New UUTStaticChannelCollectionMetadata()
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
				


				meta.AddTypeMap("CollectionName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("FileName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "UUT_Static_Channel_Collection"
				meta.Destination = "UUT_Static_Channel_Collection"
				
				meta.spInsert = "proc_UUT_Static_Channel_CollectionInsert"
				meta.spUpdate = "proc_UUT_Static_Channel_CollectionUpdate"
				meta.spDelete = "proc_UUT_Static_Channel_CollectionDelete"
				meta.spLoadAll = "proc_UUT_Static_Channel_CollectionLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_Static_Channel_CollectionLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTStaticChannelCollectionMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

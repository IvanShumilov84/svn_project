
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 1/21/2016 2:57:15 PM
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
	' Encapsulates the 'GraphParameter' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(GraphParameter))> _
	<XmlType("GraphParameter")> _	
	Partial Public Class GraphParameter 
		Inherits esGraphParameter
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New GraphParameter()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32)
			Dim obj As New GraphParameter()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New GraphParameter()
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
	<XmlType("GraphParameterCollection")> _
	Partial Public Class GraphParameterCollection
		Inherits esGraphParameterCollection
		Implements IEnumerable(Of GraphParameter)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32) As GraphParameter
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(GraphParameter))> _
		Public Class GraphParameterCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of GraphParameterCollection)
			
			Public Shared Widening Operator CType(packet As GraphParameterCollectionWCFPacket) As GraphParameterCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As GraphParameterCollection) As GraphParameterCollectionWCFPacket
				Return New GraphParameterCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class GraphParameterQuery 
		Inherits esGraphParameterQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "GraphParameterQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As GraphParameterQuery) As String
			Return GraphParameterQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As GraphParameterQuery
			Return DirectCast(GraphParameterQuery.SerializeHelper.FromXml(query, GetType(GraphParameterQuery)), GraphParameterQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esGraphParameter
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal id As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(id)
			Else
				Return LoadByPrimaryKeyStoredProcedure(id)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal id As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(id)
			Else
				Return LoadByPrimaryKeyStoredProcedure(id)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal id As System.Int32) As Boolean
		
			Dim query As New GraphParameterQuery()
			query.Where(query.Id = id)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal id As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("Id", id)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to GraphParameter.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(GraphParameterMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(GraphParameterMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(GraphParameterMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphParameter.AxisID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AxisID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(GraphParameterMetadata.ColumnNames.AxisID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(GraphParameterMetadata.ColumnNames.AxisID, value) Then
					Me._UpToGraphAxisByAxisID = Nothing
					Me.OnPropertyChanged("UpToGraphAxisByAxisID")
					OnPropertyChanged(GraphParameterMetadata.PropertyNames.AxisID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphParameter.ChannelName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelName As System.String
			Get
				Return MyBase.GetSystemString(GraphParameterMetadata.ColumnNames.ChannelName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(GraphParameterMetadata.ColumnNames.ChannelName, value) Then
					OnPropertyChanged(GraphParameterMetadata.PropertyNames.ChannelName)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToGraphAxisByAxisID As GraphAxis
		
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
												
						Case "Id"
							Me.str().Id = CType(value, string)
												
						Case "AxisID"
							Me.str().AxisID = CType(value, string)
												
						Case "ChannelName"
							Me.str().ChannelName = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(GraphParameterMetadata.PropertyNames.Id)
							End If
						
						Case "AxisID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.AxisID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(GraphParameterMetadata.PropertyNames.AxisID)
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
		
			Public Sub New(ByVal entity As esGraphParameter)
				Me.entity = entity
			End Sub				
		
	
			Public Property Id As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Id
					
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
						entity.Id = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property AxisID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.AxisID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AxisID = Nothing
					Else
						entity.AxisID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property ChannelName As System.String 
				Get
					Dim data_ As System.String = entity.ChannelName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ChannelName = Nothing
					Else
						entity.ChannelName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esGraphParameter
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return GraphParameterMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As GraphParameterQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New GraphParameterQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As GraphParameterQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As GraphParameterQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As GraphParameterQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esGraphParameterCollection
		Inherits esEntityCollection(Of GraphParameter)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return GraphParameterMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "GraphParameterCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As GraphParameterQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New GraphParameterQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As GraphParameterQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New GraphParameterQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As GraphParameterQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, GraphParameterQuery))
		End Sub
		
		#End Region
						
		Private m_query As GraphParameterQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esGraphParameterQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return GraphParameterMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "AxisID" 
					Return Me.AxisID
				Case "ChannelName" 
					Return Me.ChannelName
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, GraphParameterMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property AxisID As esQueryItem
			Get
				Return New esQueryItem(Me, GraphParameterMetadata.ColumnNames.AxisID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelName As esQueryItem
			Get
				Return New esQueryItem(Me, GraphParameterMetadata.ColumnNames.ChannelName, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class GraphParameter 
		Inherits esGraphParameter
		
	
		#Region "UpToGraphAxisByAxisID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - GraphAxisGraphParameter
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToGraphAxisByAxisID As GraphAxis
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToGraphAxisByAxisID Is Nothing _
						 AndAlso Not AxisID.Equals(Nothing)  Then
						
					Me._UpToGraphAxisByAxisID = New GraphAxis()
					Me._UpToGraphAxisByAxisID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToGraphAxisByAxisID", Me._UpToGraphAxisByAxisID)
					Me._UpToGraphAxisByAxisID.Query.Where(Me._UpToGraphAxisByAxisID.Query.Id = Me.AxisID)
					Me._UpToGraphAxisByAxisID.Query.Load()
				End If

				Return Me._UpToGraphAxisByAxisID
			End Get
			
            Set(ByVal value As GraphAxis)
				Me.RemovePreSave("UpToGraphAxisByAxisID")
				

				If value Is Nothing Then
				
					Me.AxisID = Nothing
				
					Me._UpToGraphAxisByAxisID = Nothing
				Else
				
					Me.AxisID = value.Id
					
					Me._UpToGraphAxisByAxisID = value
					Me.SetPreSave("UpToGraphAxisByAxisID", Me._UpToGraphAxisByAxisID)
				End If
				
			End Set	

		End Property
		#End Region

		
			
		''' <summary>
		''' Used internally for retrieving AutoIncrementing keys
		''' during hierarchical PreSave.
		''' </summary>
		Protected Overrides Sub ApplyPreSaveKeys()
		
			If Not Me.es.IsDeleted And Not Me._UpToGraphAxisByAxisID Is Nothing Then
				Me.AxisID = Me._UpToGraphAxisByAxisID.Id
			End If
			
		End Sub
	End Class
		



	<Serializable> _
	Partial Public Class GraphParameterMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(GraphParameterMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = GraphParameterMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.IsAutoIncrement = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphParameterMetadata.ColumnNames.AxisID, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = GraphParameterMetadata.PropertyNames.AxisID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphParameterMetadata.ColumnNames.ChannelName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = GraphParameterMetadata.PropertyNames.ChannelName
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As GraphParameterMetadata
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
			 Public Const Id As String = "ID"
			 Public Const AxisID As String = "AxisID"
			 Public Const ChannelName As String = "ChannelName"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const AxisID As String = "AxisID"
			 Public Const ChannelName As String = "ChannelName"
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
			SyncLock GetType(GraphParameterMetadata)
			
				If GraphParameterMetadata.mapDelegates Is Nothing Then
					GraphParameterMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If GraphParameterMetadata._meta Is Nothing Then
					GraphParameterMetadata._meta = New GraphParameterMetadata()
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
				


				meta.AddTypeMap("Id", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("AxisID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ChannelName", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "GraphParameter"
				meta.Destination = "GraphParameter"
				
				meta.spInsert = "proc_GraphParameterInsert"
				meta.spUpdate = "proc_GraphParameterUpdate"
				meta.spDelete = "proc_GraphParameterDelete"
				meta.spLoadAll = "proc_GraphParameterLoadAll"
				meta.spLoadByPrimaryKey = "proc_GraphParameterLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As GraphParameterMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

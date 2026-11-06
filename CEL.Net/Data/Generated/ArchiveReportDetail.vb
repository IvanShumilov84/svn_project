
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 21/07/2015 12:42:50 PM
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
	' Encapsulates the 'Archive_Report_Detail' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(ArchiveReportDetail))> _
	<XmlType("ArchiveReportDetail")> _	
	Partial Public Class ArchiveReportDetail 
		Inherits esArchiveReportDetail
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New ArchiveReportDetail()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal channelName As System.String, ByVal pwcPn As System.String)
			Dim obj As New ArchiveReportDetail()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.ChannelName = channelName
			obj.PwcPn = pwcPn
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal channelName As System.String, ByVal pwcPn As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New ArchiveReportDetail()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.ChannelName = channelName
			obj.PwcPn = pwcPn
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("ArchiveReportDetailCollection")> _
	Partial Public Class ArchiveReportDetailCollection
		Inherits esArchiveReportDetailCollection
		Implements IEnumerable(Of ArchiveReportDetail)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal channelName As System.String, ByVal pwcPn As System.String) As ArchiveReportDetail
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.ChannelName = channelName And e.PwcPn = pwcPn)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(ArchiveReportDetail))> _
		Public Class ArchiveReportDetailCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of ArchiveReportDetailCollection)
			
			Public Shared Widening Operator CType(packet As ArchiveReportDetailCollectionWCFPacket) As ArchiveReportDetailCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As ArchiveReportDetailCollection) As ArchiveReportDetailCollectionWCFPacket
				Return New ArchiveReportDetailCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class ArchiveReportDetailQuery 
		Inherits esArchiveReportDetailQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "ArchiveReportDetailQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As ArchiveReportDetailQuery) As String
			Return ArchiveReportDetailQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As ArchiveReportDetailQuery
			Return DirectCast(ArchiveReportDetailQuery.SerializeHelper.FromXml(query, GetType(ArchiveReportDetailQuery)), ArchiveReportDetailQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esArchiveReportDetail
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal channelName As System.String, ByVal pwcPn As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, channelName, pwcPn)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, channelName, pwcPn)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal channelName As System.String, ByVal pwcPn As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, channelName, pwcPn)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, channelName, pwcPn)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal channelName As System.String, ByVal pwcPn As System.String) As Boolean
		
			Dim query As New ArchiveReportDetailQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.ChannelName = channelName And query.PwcPn = pwcPn)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal channelName As System.String, ByVal pwcPn As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
						parms.Add("ChannelName", channelName)
						parms.Add("PwcPn", pwcPn)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to Archive_Report_Detail.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(ArchiveReportDetailMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(ArchiveReportDetailMetadata.ColumnNames.ProjectID, value) Then
					OnPropertyChanged(ArchiveReportDetailMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Archive_Report_Detail.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(ArchiveReportDetailMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ArchiveReportDetailMetadata.ColumnNames.EngineModel, value) Then
					OnPropertyChanged(ArchiveReportDetailMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Archive_Report_Detail.PWC_PN
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PwcPn As System.String
			Get
				Return MyBase.GetSystemString(ArchiveReportDetailMetadata.ColumnNames.PwcPn)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ArchiveReportDetailMetadata.ColumnNames.PwcPn, value) Then
					OnPropertyChanged(ArchiveReportDetailMetadata.PropertyNames.PwcPn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Archive_Report_Detail.ChannelName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelName As System.String
			Get
				Return MyBase.GetSystemString(ArchiveReportDetailMetadata.ColumnNames.ChannelName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ArchiveReportDetailMetadata.ColumnNames.ChannelName, value) Then
					OnPropertyChanged(ArchiveReportDetailMetadata.PropertyNames.ChannelName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Archive_Report_Detail.ViewOrder
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ViewOrder As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(ArchiveReportDetailMetadata.ColumnNames.ViewOrder)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(ArchiveReportDetailMetadata.ColumnNames.ViewOrder, value) Then
					OnPropertyChanged(ArchiveReportDetailMetadata.PropertyNames.ViewOrder)
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
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
						Case "PwcPn"
							Me.str().PwcPn = CType(value, string)
												
						Case "ChannelName"
							Me.str().ChannelName = CType(value, string)
												
						Case "ViewOrder"
							Me.str().ViewOrder = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(ArchiveReportDetailMetadata.PropertyNames.ProjectID)
							End If
						
						Case "ViewOrder"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.ViewOrder = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(ArchiveReportDetailMetadata.PropertyNames.ViewOrder)
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
		
			Public Sub New(ByVal entity As esArchiveReportDetail)
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
		  	
			Public Property PwcPn As System.String 
				Get
					Dim data_ As System.String = entity.PwcPn
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PwcPn = Nothing
					Else
						entity.PwcPn = Convert.ToString(Value)
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
		  	
			Public Property ViewOrder As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.ViewOrder
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ViewOrder = Nothing
					Else
						entity.ViewOrder = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  

			Private entity As esArchiveReportDetail
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return ArchiveReportDetailMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As ArchiveReportDetailQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New ArchiveReportDetailQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As ArchiveReportDetailQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As ArchiveReportDetailQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As ArchiveReportDetailQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esArchiveReportDetailCollection
		Inherits esEntityCollection(Of ArchiveReportDetail)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return ArchiveReportDetailMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "ArchiveReportDetailCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As ArchiveReportDetailQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New ArchiveReportDetailQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As ArchiveReportDetailQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New ArchiveReportDetailQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As ArchiveReportDetailQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, ArchiveReportDetailQuery))
		End Sub
		
		#End Region
						
		Private m_query As ArchiveReportDetailQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esArchiveReportDetailQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return ArchiveReportDetailMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "PwcPn" 
					Return Me.PwcPn
				Case "ChannelName" 
					Return Me.ChannelName
				Case "ViewOrder" 
					Return Me.ViewOrder
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, ArchiveReportDetailMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, ArchiveReportDetailMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property PwcPn As esQueryItem
			Get
				Return New esQueryItem(Me, ArchiveReportDetailMetadata.ColumnNames.PwcPn, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelName As esQueryItem
			Get
				Return New esQueryItem(Me, ArchiveReportDetailMetadata.ColumnNames.ChannelName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ViewOrder As esQueryItem
			Get
				Return New esQueryItem(Me, ArchiveReportDetailMetadata.ColumnNames.ViewOrder, esSystemType.Int16)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class ArchiveReportDetail 
		Inherits esArchiveReportDetail
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class ArchiveReportDetailMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(ArchiveReportDetailMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = ArchiveReportDetailMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ArchiveReportDetailMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = ArchiveReportDetailMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ArchiveReportDetailMetadata.ColumnNames.PwcPn, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = ArchiveReportDetailMetadata.PropertyNames.PwcPn
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			c.HasDefault = True
			c.Default = """Default"""
			m_columns.Add(c)
				
			c = New esColumnMetadata(ArchiveReportDetailMetadata.ColumnNames.ChannelName, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = ArchiveReportDetailMetadata.PropertyNames.ChannelName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 75
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ArchiveReportDetailMetadata.ColumnNames.ViewOrder, 4, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = ArchiveReportDetailMetadata.PropertyNames.ViewOrder
			c.NumericPrecision = 5
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As ArchiveReportDetailMetadata
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
			 Public Const PwcPn As String = "PWC_PN"
			 Public Const ChannelName As String = "ChannelName"
			 Public Const ViewOrder As String = "ViewOrder"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const PwcPn As String = "PwcPn"
			 Public Const ChannelName As String = "ChannelName"
			 Public Const ViewOrder As String = "ViewOrder"
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
			SyncLock GetType(ArchiveReportDetailMetadata)
			
				If ArchiveReportDetailMetadata.mapDelegates Is Nothing Then
					ArchiveReportDetailMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If ArchiveReportDetailMetadata._meta Is Nothing Then
					ArchiveReportDetailMetadata._meta = New ArchiveReportDetailMetadata()
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
				meta.AddTypeMap("PwcPn", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ChannelName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ViewOrder", new esTypeMap("Integer", "System.Int16"))			
				
				
				 
				meta.Source = "Archive_Report_Detail"
				meta.Destination = "Archive_Report_Detail"
				
				meta.spInsert = "proc_Archive_Report_DetailInsert"
				meta.spUpdate = "proc_Archive_Report_DetailUpdate"
				meta.spDelete = "proc_Archive_Report_DetailDelete"
				meta.spLoadAll = "proc_Archive_Report_DetailLoadAll"
				meta.spLoadByPrimaryKey = "proc_Archive_Report_DetailLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As ArchiveReportDetailMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

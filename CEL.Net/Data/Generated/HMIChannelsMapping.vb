
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 19/11/2013 2:04:49 PM
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
	' Encapsulates the 'HMI_Channels_Mapping' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(HMIChannelsMapping))> _
	<XmlType("HMIChannelsMapping")> _	
	Partial Public Class HMIChannelsMapping 
		Inherits esHMIChannelsMapping
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New HMIChannelsMapping()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal channelName As System.String)
			Dim obj As New HMIChannelsMapping()
			obj.ProjectID = projectID
			obj.CollectionName = collectionName
			obj.ChannelName = channelName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal channelName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New HMIChannelsMapping()
			obj.ProjectID = projectID
			obj.CollectionName = collectionName
			obj.ChannelName = channelName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("HMIChannelsMappingCollection")> _
	Partial Public Class HMIChannelsMappingCollection
		Inherits esHMIChannelsMappingCollection
		Implements IEnumerable(Of HMIChannelsMapping)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal channelName As System.String) As HMIChannelsMapping
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.CollectionName = collectionName And e.ChannelName = channelName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(HMIChannelsMapping))> _
		Public Class HMIChannelsMappingCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of HMIChannelsMappingCollection)
			
			Public Shared Widening Operator CType(packet As HMIChannelsMappingCollectionWCFPacket) As HMIChannelsMappingCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As HMIChannelsMappingCollection) As HMIChannelsMappingCollectionWCFPacket
				Return New HMIChannelsMappingCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class HMIChannelsMappingQuery 
		Inherits esHMIChannelsMappingQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "HMIChannelsMappingQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As HMIChannelsMappingQuery) As String
			Return HMIChannelsMappingQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As HMIChannelsMappingQuery
			Return DirectCast(HMIChannelsMappingQuery.SerializeHelper.FromXml(query, GetType(HMIChannelsMappingQuery)), HMIChannelsMappingQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esHMIChannelsMapping
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal channelName As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, collectionName, channelName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, collectionName, channelName)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal channelName As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, collectionName, channelName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, collectionName, channelName)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal channelName As System.String) As Boolean
		
			Dim query As New HMIChannelsMappingQuery()
			query.Where(query.ProjectID = projectID And query.CollectionName = collectionName And query.ChannelName = channelName)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal channelName As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("CollectionName", collectionName)
						parms.Add("ChannelName", channelName)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to HMI_Channels_Mapping.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMIChannelsMappingMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMIChannelsMappingMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToProjectInfoByProjectID = Nothing
					Me.OnPropertyChanged("UpToProjectInfoByProjectID")
					OnPropertyChanged(HMIChannelsMappingMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Channels_Mapping.CollectionName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CollectionName As System.String
			Get
				Return MyBase.GetSystemString(HMIChannelsMappingMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIChannelsMappingMetadata.ColumnNames.CollectionName, value) Then
					Me._UpToDynamicCollectionByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDynamicCollectionByCollectionName")
					OnPropertyChanged(HMIChannelsMappingMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Channels_Mapping.ChannelName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelName As System.String
			Get
				Return MyBase.GetSystemString(HMIChannelsMappingMetadata.ColumnNames.ChannelName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIChannelsMappingMetadata.ColumnNames.ChannelName, value) Then
					OnPropertyChanged(HMIChannelsMappingMetadata.PropertyNames.ChannelName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Channels_Mapping.FV_Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FVName As System.String
			Get
				Return MyBase.GetSystemString(HMIChannelsMappingMetadata.ColumnNames.FVName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIChannelsMappingMetadata.ColumnNames.FVName, value) Then
					OnPropertyChanged(HMIChannelsMappingMetadata.PropertyNames.FVName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Channels_Mapping.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMIChannelsMappingMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMIChannelsMappingMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(HMIChannelsMappingMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Channels_Mapping.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(HMIChannelsMappingMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIChannelsMappingMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(HMIChannelsMappingMetadata.PropertyNames.Note)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Channels_Mapping.TempNote
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TempNote As System.String
			Get
				Return MyBase.GetSystemString(HMIChannelsMappingMetadata.ColumnNames.TempNote)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIChannelsMappingMetadata.ColumnNames.TempNote, value) Then
					OnPropertyChanged(HMIChannelsMappingMetadata.PropertyNames.TempNote)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToDynamicCollectionByCollectionName As DynamicCollection
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToProjectInfoByProjectID As ProjectInfo
		
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
												
						Case "CollectionName"
							Me.str().CollectionName = CType(value, string)
												
						Case "ChannelName"
							Me.str().ChannelName = CType(value, string)
												
						Case "FVName"
							Me.str().FVName = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
												
						Case "Note"
							Me.str().Note = CType(value, string)
												
						Case "TempNote"
							Me.str().TempNote = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMIChannelsMappingMetadata.PropertyNames.ProjectID)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMIChannelsMappingMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esHMIChannelsMapping)
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
		  	
			Public Property FVName As System.String 
				Get
					Dim data_ As System.String = entity.FVName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FVName = Nothing
					Else
						entity.FVName = Convert.ToString(Value)
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
		  	
			Public Property Note As System.String 
				Get
					Dim data_ As System.String = entity.Note
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Note = Nothing
					Else
						entity.Note = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property TempNote As System.String 
				Get
					Dim data_ As System.String = entity.TempNote
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TempNote = Nothing
					Else
						entity.TempNote = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esHMIChannelsMapping
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return HMIChannelsMappingMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As HMIChannelsMappingQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New HMIChannelsMappingQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As HMIChannelsMappingQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As HMIChannelsMappingQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As HMIChannelsMappingQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esHMIChannelsMappingCollection
		Inherits esEntityCollection(Of HMIChannelsMapping)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return HMIChannelsMappingMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "HMIChannelsMappingCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As HMIChannelsMappingQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New HMIChannelsMappingQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As HMIChannelsMappingQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New HMIChannelsMappingQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As HMIChannelsMappingQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, HMIChannelsMappingQuery))
		End Sub
		
		#End Region
						
		Private m_query As HMIChannelsMappingQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esHMIChannelsMappingQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return HMIChannelsMappingMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "CollectionName" 
					Return Me.CollectionName
				Case "ChannelName" 
					Return Me.ChannelName
				Case "FVName" 
					Return Me.FVName
				Case "Unavailable" 
					Return Me.Unavailable
				Case "Note" 
					Return Me.Note
				Case "TempNote" 
					Return Me.TempNote
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, HMIChannelsMappingMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, HMIChannelsMappingMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelName As esQueryItem
			Get
				Return New esQueryItem(Me, HMIChannelsMappingMetadata.ColumnNames.ChannelName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FVName As esQueryItem
			Get
				Return New esQueryItem(Me, HMIChannelsMappingMetadata.ColumnNames.FVName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, HMIChannelsMappingMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, HMIChannelsMappingMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TempNote As esQueryItem
			Get
				Return New esQueryItem(Me, HMIChannelsMappingMetadata.ColumnNames.TempNote, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class HMIChannelsMapping 
		Inherits esHMIChannelsMapping
		
	
		#Region "UpToDynamicCollectionByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Dynamic_CollectionHMI_Channels_Mapping
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

		#Region "UpToProjectInfoByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Project_InfoHMI_Channels_Mapping
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

		
		
	End Class
		



	<Serializable> _
	Partial Public Class HMIChannelsMappingMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(HMIChannelsMappingMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMIChannelsMappingMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIChannelsMappingMetadata.ColumnNames.CollectionName, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIChannelsMappingMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIChannelsMappingMetadata.ColumnNames.ChannelName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIChannelsMappingMetadata.PropertyNames.ChannelName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIChannelsMappingMetadata.ColumnNames.FVName, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIChannelsMappingMetadata.PropertyNames.FVName
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIChannelsMappingMetadata.ColumnNames.Unavailable, 4, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMIChannelsMappingMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIChannelsMappingMetadata.ColumnNames.Note, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIChannelsMappingMetadata.PropertyNames.Note
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIChannelsMappingMetadata.ColumnNames.TempNote, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIChannelsMappingMetadata.PropertyNames.TempNote
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As HMIChannelsMappingMetadata
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
			 Public Const CollectionName As String = "CollectionName"
			 Public Const ChannelName As String = "ChannelName"
			 Public Const FVName As String = "FV_Name"
			 Public Const Unavailable As String = "Unavailable"
			 Public Const Note As String = "Note"
			 Public Const TempNote As String = "TempNote"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const CollectionName As String = "CollectionName"
			 Public Const ChannelName As String = "ChannelName"
			 Public Const FVName As String = "FVName"
			 Public Const Unavailable As String = "Unavailable"
			 Public Const Note As String = "Note"
			 Public Const TempNote As String = "TempNote"
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
			SyncLock GetType(HMIChannelsMappingMetadata)
			
				If HMIChannelsMappingMetadata.mapDelegates Is Nothing Then
					HMIChannelsMappingMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If HMIChannelsMappingMetadata._meta Is Nothing Then
					HMIChannelsMappingMetadata._meta = New HMIChannelsMappingMetadata()
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
				meta.AddTypeMap("CollectionName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ChannelName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("FVName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Note", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("TempNote", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "HMI_Channels_Mapping"
				meta.Destination = "HMI_Channels_Mapping"
				
				meta.spInsert = "proc_HMI_Channels_MappingInsert"
				meta.spUpdate = "proc_HMI_Channels_MappingUpdate"
				meta.spDelete = "proc_HMI_Channels_MappingDelete"
				meta.spLoadAll = "proc_HMI_Channels_MappingLoadAll"
				meta.spLoadByPrimaryKey = "proc_HMI_Channels_MappingLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As HMIChannelsMappingMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

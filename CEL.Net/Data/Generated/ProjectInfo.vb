
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 05/02/2014 8:29:15 AM
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
	' Encapsulates the 'Project_Info' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(ProjectInfo))> _
	<XmlType("ProjectInfo")> _	
	Partial Public Class ProjectInfo 
		Inherits esProjectInfo
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New ProjectInfo()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32)
			Dim obj As New ProjectInfo()
			obj.ProjectID = projectID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New ProjectInfo()
			obj.ProjectID = projectID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("ProjectInfoCollection")> _
	Partial Public Class ProjectInfoCollection
		Inherits esProjectInfoCollection
		Implements IEnumerable(Of ProjectInfo)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32) As ProjectInfo
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(ProjectInfo))> _
		Public Class ProjectInfoCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of ProjectInfoCollection)
			
			Public Shared Widening Operator CType(packet As ProjectInfoCollectionWCFPacket) As ProjectInfoCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As ProjectInfoCollection) As ProjectInfoCollectionWCFPacket
				Return New ProjectInfoCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class ProjectInfoQuery 
		Inherits esProjectInfoQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "ProjectInfoQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As ProjectInfoQuery) As String
			Return ProjectInfoQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As ProjectInfoQuery
			Return DirectCast(ProjectInfoQuery.SerializeHelper.FromXml(query, GetType(ProjectInfoQuery)), ProjectInfoQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esProjectInfo
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32) As Boolean
		
			Dim query As New ProjectInfoQuery()
			query.Where(query.ProjectID = projectID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to Project_Info.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(ProjectInfoMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(ProjectInfoMetadata.ColumnNames.ProjectID, value) Then
					OnPropertyChanged(ProjectInfoMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Project_Info.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(ProjectInfoMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ProjectInfoMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(ProjectInfoMetadata.PropertyNames.Description)
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
												
						Case "Description"
							Me.str().Description = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(ProjectInfoMetadata.PropertyNames.ProjectID)
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
		
			Public Sub New(ByVal entity As esProjectInfo)
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
		  

			Private entity As esProjectInfo
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return ProjectInfoMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As ProjectInfoQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New ProjectInfoQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As ProjectInfoQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As ProjectInfoQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As ProjectInfoQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esProjectInfoCollection
		Inherits esEntityCollection(Of ProjectInfo)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return ProjectInfoMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "ProjectInfoCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As ProjectInfoQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New ProjectInfoQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As ProjectInfoQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New ProjectInfoQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As ProjectInfoQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, ProjectInfoQuery))
		End Sub
		
		#End Region
						
		Private m_query As ProjectInfoQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esProjectInfoQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return ProjectInfoMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "Description" 
					Return Me.Description
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, ProjectInfoMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, ProjectInfoMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class ProjectInfo 
		Inherits esProjectInfo
		
	
		#Region "UpToDynamicCollectionCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - Project_InfoHMI_Channels_Mapping
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToDynamicCollectionCollection As DynamicCollectionCollection
		
			Get
				If Me._UpToDynamicCollectionCollection Is Nothing Then
					Me._UpToDynamicCollectionCollection = New DynamicCollectionCollection()
					Me._UpToDynamicCollectionCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToDynamicCollectionCollection", Me._UpToDynamicCollectionCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.ProjectID.Equals(Nothing) Then 
				
						Dim m As New DynamicCollectionQuery("m")
						Dim j As New HMIChannelsMappingQuery("j")
						m.Select(m)
						m.InnerJoin(j).On(m.CollectionName = j.CollectionName)
                        m.Where(j.ProjectID = Me.ProjectID)

						Me._UpToDynamicCollectionCollection.Load(m)

					End If
				End If

				Return Me._UpToDynamicCollectionCollection
			End Get
			
			Set(ByVal value As DynamicCollectionCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToDynamicCollectionCollection Is Nothing Then

					Me.RemovePostSave("UpToDynamicCollectionCollection")
					Me._UpToDynamicCollectionCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - Project_InfoHMI_Channels_Mapping
		''' </summary>
		Public Sub AssociateDynamicCollectionCollection(entity As DynamicCollection)
			If Me._HMIChannelsMappingCollection Is Nothing Then
				Me._HMIChannelsMappingCollection = New HMIChannelsMappingCollection()
				Me._HMIChannelsMappingCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("HMIChannelsMappingCollection", Me._HMIChannelsMappingCollection)
			End If
			
			Dim obj As HMIChannelsMapping = Me._HMIChannelsMappingCollection.AddNew()
			obj.ProjectID = Me.ProjectID
			obj.CollectionName = entity.CollectionName			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - Project_InfoHMI_Channels_Mapping
		''' </summary>
		Public Sub DissociateDynamicCollectionCollection(entity As DynamicCollection)
			If Me._HMIChannelsMappingCollection Is Nothing Then
				Me._HMIChannelsMappingCollection = new HMIChannelsMappingCollection()
				Me._HMIChannelsMappingCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("HMIChannelsMappingCollection", Me._HMIChannelsMappingCollection)
			End If

			Dim obj As HMIChannelsMapping = Me._HMIChannelsMappingCollection.AddNew()
			obj.ProjectID = Me.ProjectID
            obj.CollectionName = entity.CollectionName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToDynamicCollectionCollection As DynamicCollectionCollection
		Private _HMIChannelsMappingCollection As HMIChannelsMappingCollection
		#End Region

		#Region "HMIChannelsMappingCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_HMIChannelsMappingCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.ProjectInfo.HMIChannelsMappingCollectionByProjectID_Delegate)
				map.PropertyName = "HMIChannelsMappingCollectionByProjectID"
				map.MyColumnName = "ProjectID"
				map.ParentColumnName = "ProjectID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub HMIChannelsMappingCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New ProjectInfoQuery(data.NextAlias())
			
			Dim mee As HMIChannelsMappingQuery = If(data.You IsNot Nothing, TryCast(data.You, HMIChannelsMappingQuery), New HMIChannelsMappingQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Project_InfoHMI_Channels_Mapping
		''' </summary>

		<XmlIgnore()> _ 
		Public Property HMIChannelsMappingCollectionByProjectID As HMIChannelsMappingCollection 
		
			Get
				If Me._HMIChannelsMappingCollectionByProjectID Is Nothing Then
					Me._HMIChannelsMappingCollectionByProjectID = New HMIChannelsMappingCollection()
					Me._HMIChannelsMappingCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("HMIChannelsMappingCollectionByProjectID", Me._HMIChannelsMappingCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._HMIChannelsMappingCollectionByProjectID.Query.Where(Me._HMIChannelsMappingCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._HMIChannelsMappingCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._HMIChannelsMappingCollectionByProjectID.fks.Add(HMIChannelsMappingMetadata.ColumnNames.ProjectID, Me.ProjectID)
					End If
				End If

				Return Me._HMIChannelsMappingCollectionByProjectID
			End Get
			
			Set(ByVal value As HMIChannelsMappingCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._HMIChannelsMappingCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("HMIChannelsMappingCollectionByProjectID")
					Me._HMIChannelsMappingCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _HMIChannelsMappingCollectionByProjectID As HMIChannelsMappingCollection
		#End Region

		#Region "UsersCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UsersCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.ProjectInfo.UsersCollectionByProjectID_Delegate)
				map.PropertyName = "UsersCollectionByProjectID"
				map.MyColumnName = "ProjectID"
				map.ParentColumnName = "ProjectID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub UsersCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New ProjectInfoQuery(data.NextAlias())
			
			Dim mee As UsersQuery = If(data.You IsNot Nothing, TryCast(data.You, UsersQuery), New UsersQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Project_InfoUsers
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UsersCollectionByProjectID As UsersCollection 
		
			Get
				If Me._UsersCollectionByProjectID Is Nothing Then
					Me._UsersCollectionByProjectID = New UsersCollection()
					Me._UsersCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UsersCollectionByProjectID", Me._UsersCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UsersCollectionByProjectID.Query.Where(Me._UsersCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UsersCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UsersCollectionByProjectID.fks.Add(UsersMetadata.ColumnNames.ProjectID, Me.ProjectID)
					End If
				End If

				Return Me._UsersCollectionByProjectID
			End Get
			
			Set(ByVal value As UsersCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UsersCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UsersCollectionByProjectID")
					Me._UsersCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UsersCollectionByProjectID As UsersCollection
		#End Region

		#Region "UpToEngineInfoCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - Project_InfoUUT
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToEngineInfoCollection As EngineInfoCollection
		
			Get
				If Me._UpToEngineInfoCollection Is Nothing Then
					Me._UpToEngineInfoCollection = New EngineInfoCollection()
					Me._UpToEngineInfoCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToEngineInfoCollection", Me._UpToEngineInfoCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.ProjectID.Equals(Nothing) Then 
				
						Dim m As New EngineInfoQuery("m")
						Dim j As New UutQuery("j")
						m.Select(m)
						m.InnerJoin(j).On(m.EngineModel = j.EngineModel)
                        m.Where(j.ProjectID = Me.ProjectID)

						Me._UpToEngineInfoCollection.Load(m)

					End If
				End If

				Return Me._UpToEngineInfoCollection
			End Get
			
			Set(ByVal value As EngineInfoCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToEngineInfoCollection Is Nothing Then

					Me.RemovePostSave("UpToEngineInfoCollection")
					Me._UpToEngineInfoCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - Project_InfoUUT
		''' </summary>
		Public Sub AssociateEngineInfoCollection(entity As EngineInfo)
			If Me._UutCollection Is Nothing Then
				Me._UutCollection = New UutCollection()
				Me._UutCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UutCollection", Me._UutCollection)
			End If
			
			Dim obj As Uut = Me._UutCollection.AddNew()
			obj.ProjectID = Me.ProjectID
			obj.EngineModel = entity.EngineModel			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - Project_InfoUUT
		''' </summary>
		Public Sub DissociateEngineInfoCollection(entity As EngineInfo)
			If Me._UutCollection Is Nothing Then
				Me._UutCollection = new UutCollection()
				Me._UutCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UutCollection", Me._UutCollection)
			End If

			Dim obj As Uut = Me._UutCollection.AddNew()
			obj.ProjectID = Me.ProjectID
            obj.EngineModel = entity.EngineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToEngineInfoCollection As EngineInfoCollection
		Private _UutCollection As UutCollection
		#End Region

		#Region "UutCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UutCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.ProjectInfo.UutCollectionByProjectID_Delegate)
				map.PropertyName = "UutCollectionByProjectID"
				map.MyColumnName = "ProjectID"
				map.ParentColumnName = "ProjectID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub UutCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New ProjectInfoQuery(data.NextAlias())
			
			Dim mee As UutQuery = If(data.You IsNot Nothing, TryCast(data.You, UutQuery), New UutQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Project_InfoUUT
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UutCollectionByProjectID As UutCollection 
		
			Get
				If Me._UutCollectionByProjectID Is Nothing Then
					Me._UutCollectionByProjectID = New UutCollection()
					Me._UutCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UutCollectionByProjectID", Me._UutCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UutCollectionByProjectID.Query.Where(Me._UutCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UutCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UutCollectionByProjectID.fks.Add(UutMetadata.ColumnNames.ProjectID, Me.ProjectID)
					End If
				End If

				Return Me._UutCollectionByProjectID
			End Get
			
			Set(ByVal value As UutCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UutCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UutCollectionByProjectID")
					Me._UutCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UutCollectionByProjectID As UutCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "HMIChannelsMappingCollectionByProjectID"
					coll = Me.HMIChannelsMappingCollectionByProjectID
					Exit Select
				Case "UsersCollectionByProjectID"
					coll = Me.UsersCollectionByProjectID
					Exit Select
				Case "UutCollectionByProjectID"
					coll = Me.UutCollectionByProjectID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "HMIChannelsMappingCollectionByProjectID", GetType(HMIChannelsMappingCollection), New HMIChannelsMapping()))
			props.Add(new esPropertyDescriptor(Me, "UsersCollectionByProjectID", GetType(UsersCollection), New Users()))
			props.Add(new esPropertyDescriptor(Me, "UutCollectionByProjectID", GetType(UutCollection), New Uut()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class ProjectInfoMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(ProjectInfoMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = ProjectInfoMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ProjectInfoMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = ProjectInfoMetadata.PropertyNames.Description
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As ProjectInfoMetadata
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
			 Public Const Description As String = "Description"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
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
			SyncLock GetType(ProjectInfoMetadata)
			
				If ProjectInfoMetadata.mapDelegates Is Nothing Then
					ProjectInfoMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If ProjectInfoMetadata._meta Is Nothing Then
					ProjectInfoMetadata._meta = New ProjectInfoMetadata()
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
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "Project_Info"
				meta.Destination = "Project_Info"
				
				meta.spInsert = "proc_Project_InfoInsert"
				meta.spUpdate = "proc_Project_InfoUpdate"
				meta.spDelete = "proc_Project_InfoDelete"
				meta.spLoadAll = "proc_Project_InfoLoadAll"
				meta.spLoadByPrimaryKey = "proc_Project_InfoLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As ProjectInfoMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace


'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 21/03/2016 11:45:36 PM
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
	' Encapsulates the 'Log_Report_Detail' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(LogReportDetail))> _
	<XmlType("LogReportDetail")> _	
	Partial Public Class LogReportDetail 
		Inherits esLogReportDetail
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New LogReportDetail()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal channelName As System.String, ByVal pwcPn As System.String)
			Dim obj As New LogReportDetail()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.ChannelName = channelName
			obj.PwcPn = pwcPn
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal channelName As System.String, ByVal pwcPn As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New LogReportDetail()
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
	<XmlType("LogReportDetailCollection")> _
	Partial Public Class LogReportDetailCollection
		Inherits esLogReportDetailCollection
		Implements IEnumerable(Of LogReportDetail)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal channelName As System.String, ByVal pwcPn As System.String) As LogReportDetail
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.ChannelName = channelName And e.PwcPn = pwcPn)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(LogReportDetail))> _
		Public Class LogReportDetailCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of LogReportDetailCollection)
			
			Public Shared Widening Operator CType(packet As LogReportDetailCollectionWCFPacket) As LogReportDetailCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As LogReportDetailCollection) As LogReportDetailCollectionWCFPacket
				Return New LogReportDetailCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class LogReportDetailQuery 
		Inherits esLogReportDetailQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "LogReportDetailQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As LogReportDetailQuery) As String
			Return LogReportDetailQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As LogReportDetailQuery
			Return DirectCast(LogReportDetailQuery.SerializeHelper.FromXml(query, GetType(LogReportDetailQuery)), LogReportDetailQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esLogReportDetail
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
		
			Dim query As New LogReportDetailQuery()
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
		' Maps to Log_Report_Detail.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(LogReportDetailMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(LogReportDetailMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(LogReportDetailMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Log_Report_Detail.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(LogReportDetailMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(LogReportDetailMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(LogReportDetailMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Log_Report_Detail.PWC_PN
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PwcPn As System.String
			Get
				Return MyBase.GetSystemString(LogReportDetailMetadata.ColumnNames.PwcPn)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(LogReportDetailMetadata.ColumnNames.PwcPn, value) Then
					OnPropertyChanged(LogReportDetailMetadata.PropertyNames.PwcPn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Log_Report_Detail.ChannelName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelName As System.String
			Get
				Return MyBase.GetSystemString(LogReportDetailMetadata.ColumnNames.ChannelName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(LogReportDetailMetadata.ColumnNames.ChannelName, value) Then
					OnPropertyChanged(LogReportDetailMetadata.PropertyNames.ChannelName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Log_Report_Detail.Category
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Category As System.String
			Get
				Return MyBase.GetSystemString(LogReportDetailMetadata.ColumnNames.Category)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(LogReportDetailMetadata.ColumnNames.Category, value) Then
					OnPropertyChanged(LogReportDetailMetadata.PropertyNames.Category)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Log_Report_Detail.OrderInCategory
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OrderInCategory As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(LogReportDetailMetadata.ColumnNames.OrderInCategory)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(LogReportDetailMetadata.ColumnNames.OrderInCategory, value) Then
					OnPropertyChanged(LogReportDetailMetadata.PropertyNames.OrderInCategory)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Log_Report_Detail.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(LogReportDetailMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(LogReportDetailMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(LogReportDetailMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Log_Report_Detail.DisplayUnit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DisplayUnit As System.String
			Get
				Return MyBase.GetSystemString(LogReportDetailMetadata.ColumnNames.DisplayUnit)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(LogReportDetailMetadata.ColumnNames.DisplayUnit, value) Then
					OnPropertyChanged(LogReportDetailMetadata.PropertyNames.DisplayUnit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Log_Report_Detail.Format
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Format As System.String
			Get
				Return MyBase.GetSystemString(LogReportDetailMetadata.ColumnNames.Format)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(LogReportDetailMetadata.ColumnNames.Format, value) Then
					OnPropertyChanged(LogReportDetailMetadata.PropertyNames.Format)
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
												
						Case "PwcPn"
							Me.str().PwcPn = CType(value, string)
												
						Case "ChannelName"
							Me.str().ChannelName = CType(value, string)
												
						Case "Category"
							Me.str().Category = CType(value, string)
												
						Case "OrderInCategory"
							Me.str().OrderInCategory = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "DisplayUnit"
							Me.str().DisplayUnit = CType(value, string)
												
						Case "Format"
							Me.str().Format = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(LogReportDetailMetadata.PropertyNames.ProjectID)
							End If
						
						Case "OrderInCategory"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.OrderInCategory = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(LogReportDetailMetadata.PropertyNames.OrderInCategory)
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
		
			Public Sub New(ByVal entity As esLogReportDetail)
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
		  	
			Public Property Category As System.String 
				Get
					Dim data_ As System.String = entity.Category
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Category = Nothing
					Else
						entity.Category = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property OrderInCategory As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.OrderInCategory
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OrderInCategory = Nothing
					Else
						entity.OrderInCategory = Convert.ToInt16(Value)
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
		  	
			Public Property DisplayUnit As System.String 
				Get
					Dim data_ As System.String = entity.DisplayUnit
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DisplayUnit = Nothing
					Else
						entity.DisplayUnit = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Format As System.String 
				Get
					Dim data_ As System.String = entity.Format
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Format = Nothing
					Else
						entity.Format = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esLogReportDetail
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return LogReportDetailMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As LogReportDetailQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New LogReportDetailQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As LogReportDetailQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As LogReportDetailQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As LogReportDetailQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esLogReportDetailCollection
		Inherits esEntityCollection(Of LogReportDetail)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return LogReportDetailMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "LogReportDetailCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As LogReportDetailQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New LogReportDetailQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As LogReportDetailQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New LogReportDetailQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As LogReportDetailQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, LogReportDetailQuery))
		End Sub
		
		#End Region
						
		Private m_query As LogReportDetailQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esLogReportDetailQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return LogReportDetailMetadata.Meta()
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
				Case "Category" 
					Return Me.Category
				Case "OrderInCategory" 
					Return Me.OrderInCategory
				Case "Description" 
					Return Me.Description
				Case "DisplayUnit" 
					Return Me.DisplayUnit
				Case "Format" 
					Return Me.Format
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, LogReportDetailMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, LogReportDetailMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property PwcPn As esQueryItem
			Get
				Return New esQueryItem(Me, LogReportDetailMetadata.ColumnNames.PwcPn, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelName As esQueryItem
			Get
				Return New esQueryItem(Me, LogReportDetailMetadata.ColumnNames.ChannelName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Category As esQueryItem
			Get
				Return New esQueryItem(Me, LogReportDetailMetadata.ColumnNames.Category, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OrderInCategory As esQueryItem
			Get
				Return New esQueryItem(Me, LogReportDetailMetadata.ColumnNames.OrderInCategory, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, LogReportDetailMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DisplayUnit As esQueryItem
			Get
				Return New esQueryItem(Me, LogReportDetailMetadata.ColumnNames.DisplayUnit, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Format As esQueryItem
			Get
				Return New esQueryItem(Me, LogReportDetailMetadata.ColumnNames.Format, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class LogReportDetail 
		Inherits esLogReportDetail
		
	
		#Region "LogReportCategoriesCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_LogReportCategoriesCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.LogReportDetail.LogReportCategoriesCollectionByProjectID_Delegate)
				map.PropertyName = "LogReportCategoriesCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel,Categories"
				map.ParentColumnName = "ProjectID,EngineModel,Category"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub LogReportCategoriesCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New LogReportDetailQuery(data.NextAlias())
			
			Dim mee As LogReportCategoriesQuery = If(data.You IsNot Nothing, TryCast(data.You, LogReportCategoriesQuery), New LogReportCategoriesQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel And parent.Category = mee.Categories)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Log_Report_DetailLog_Report_Categories
		''' </summary>

		<XmlIgnore()> _ 
		Public Property LogReportCategoriesCollectionByProjectID As LogReportCategoriesCollection 
		
			Get
				If Me._LogReportCategoriesCollectionByProjectID Is Nothing Then
					Me._LogReportCategoriesCollectionByProjectID = New LogReportCategoriesCollection()
					Me._LogReportCategoriesCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("LogReportCategoriesCollectionByProjectID", Me._LogReportCategoriesCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) AndAlso Not Me.Category.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._LogReportCategoriesCollectionByProjectID.Query.Where(Me._LogReportCategoriesCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._LogReportCategoriesCollectionByProjectID.Query.Where(Me._LogReportCategoriesCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._LogReportCategoriesCollectionByProjectID.Query.Where(Me._LogReportCategoriesCollectionByProjectID.Query.Categories = Me.Category)
							Me._LogReportCategoriesCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._LogReportCategoriesCollectionByProjectID.fks.Add(LogReportCategoriesMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._LogReportCategoriesCollectionByProjectID.fks.Add(LogReportCategoriesMetadata.ColumnNames.EngineModel, Me.EngineModel)
						Me._LogReportCategoriesCollectionByProjectID.fks.Add(LogReportCategoriesMetadata.ColumnNames.Categories, Me.Category)
					End If
				End If

				Return Me._LogReportCategoriesCollectionByProjectID
			End Get
			
			Set(ByVal value As LogReportCategoriesCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._LogReportCategoriesCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("LogReportCategoriesCollectionByProjectID")
					Me._LogReportCategoriesCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _LogReportCategoriesCollectionByProjectID As LogReportCategoriesCollection
		#End Region

		#Region "UpToUutByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUTLog_Report_Detail
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

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "LogReportCategoriesCollectionByProjectID"
					coll = Me.LogReportCategoriesCollectionByProjectID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "LogReportCategoriesCollectionByProjectID", GetType(LogReportCategoriesCollection), New LogReportCategories()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class LogReportDetailMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(LogReportDetailMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = LogReportDetailMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(LogReportDetailMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = LogReportDetailMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(LogReportDetailMetadata.ColumnNames.PwcPn, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = LogReportDetailMetadata.PropertyNames.PwcPn
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			c.HasDefault = True
			c.Default = """Default"""
			m_columns.Add(c)
				
			c = New esColumnMetadata(LogReportDetailMetadata.ColumnNames.ChannelName, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = LogReportDetailMetadata.PropertyNames.ChannelName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(LogReportDetailMetadata.ColumnNames.Category, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = LogReportDetailMetadata.PropertyNames.Category
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(LogReportDetailMetadata.ColumnNames.OrderInCategory, 5, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = LogReportDetailMetadata.PropertyNames.OrderInCategory
			c.NumericPrecision = 5
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(LogReportDetailMetadata.ColumnNames.Description, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = LogReportDetailMetadata.PropertyNames.Description
			c.CharacterMaxLength = 100
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(LogReportDetailMetadata.ColumnNames.DisplayUnit, 7, GetType(System.String), esSystemType.String)	
			c.PropertyName = LogReportDetailMetadata.PropertyNames.DisplayUnit
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(LogReportDetailMetadata.ColumnNames.Format, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = LogReportDetailMetadata.PropertyNames.Format
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As LogReportDetailMetadata
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
			 Public Const Category As String = "Category"
			 Public Const OrderInCategory As String = "OrderInCategory"
			 Public Const Description As String = "Description"
			 Public Const DisplayUnit As String = "DisplayUnit"
			 Public Const Format As String = "Format"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const PwcPn As String = "PwcPn"
			 Public Const ChannelName As String = "ChannelName"
			 Public Const Category As String = "Category"
			 Public Const OrderInCategory As String = "OrderInCategory"
			 Public Const Description As String = "Description"
			 Public Const DisplayUnit As String = "DisplayUnit"
			 Public Const Format As String = "Format"
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
			SyncLock GetType(LogReportDetailMetadata)
			
				If LogReportDetailMetadata.mapDelegates Is Nothing Then
					LogReportDetailMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If LogReportDetailMetadata._meta Is Nothing Then
					LogReportDetailMetadata._meta = New LogReportDetailMetadata()
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
				meta.AddTypeMap("Category", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OrderInCategory", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DisplayUnit", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Format", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "Log_Report_Detail"
				meta.Destination = "Log_Report_Detail"
				
				meta.spInsert = "proc_Log_Report_DetailInsert"
				meta.spUpdate = "proc_Log_Report_DetailUpdate"
				meta.spDelete = "proc_Log_Report_DetailDelete"
				meta.spLoadAll = "proc_Log_Report_DetailLoadAll"
				meta.spLoadByPrimaryKey = "proc_Log_Report_DetailLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As LogReportDetailMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

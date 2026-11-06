
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 27/06/2013 09:48:58
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
	' Encapsulates the 'TestProgressCheckListItem' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(TestProgressCheckListItem))> _
	<XmlType("TestProgressCheckListItem")> _	
	Partial Public Class TestProgressCheckListItem 
		Inherits esTestProgressCheckListItem
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New TestProgressCheckListItem()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32)
			Dim obj As New TestProgressCheckListItem()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New TestProgressCheckListItem()
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
	<XmlType("TestProgressCheckListItemCollection")> _
	Partial Public Class TestProgressCheckListItemCollection
		Inherits esTestProgressCheckListItemCollection
		Implements IEnumerable(Of TestProgressCheckListItem)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32) As TestProgressCheckListItem
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(TestProgressCheckListItem))> _
		Public Class TestProgressCheckListItemCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of TestProgressCheckListItemCollection)
			
			Public Shared Widening Operator CType(packet As TestProgressCheckListItemCollectionWCFPacket) As TestProgressCheckListItemCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As TestProgressCheckListItemCollection) As TestProgressCheckListItemCollectionWCFPacket
				Return New TestProgressCheckListItemCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class TestProgressCheckListItemQuery 
		Inherits esTestProgressCheckListItemQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "TestProgressCheckListItemQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As TestProgressCheckListItemQuery) As String
			Return TestProgressCheckListItemQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As TestProgressCheckListItemQuery
			Return DirectCast(TestProgressCheckListItemQuery.SerializeHelper.FromXml(query, GetType(TestProgressCheckListItemQuery)), TestProgressCheckListItemQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esTestProgressCheckListItem
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
		
			Dim query As New TestProgressCheckListItemQuery()
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
		' Maps to TestProgressCheckListItem.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(TestProgressCheckListItemMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(TestProgressCheckListItemMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressCheckListItem.TestStepID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TestStepID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(TestProgressCheckListItemMetadata.ColumnNames.TestStepID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(TestProgressCheckListItemMetadata.ColumnNames.TestStepID, value) Then
					Me._UpToTestProgressTestStepByTestStepID = Nothing
					Me.OnPropertyChanged("UpToTestProgressTestStepByTestStepID")
					OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.TestStepID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressCheckListItem.ParameterName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ParameterName As System.String
			Get
				Return MyBase.GetSystemString(TestProgressCheckListItemMetadata.ColumnNames.ParameterName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(TestProgressCheckListItemMetadata.ColumnNames.ParameterName, value) Then
					OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.ParameterName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressCheckListItem.LowLimit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLimit As Nullable(Of System.Decimal)
			Get
				Return MyBase.GetSystemDecimal(TestProgressCheckListItemMetadata.ColumnNames.LowLimit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Decimal))
				If MyBase.SetSystemDecimal(TestProgressCheckListItemMetadata.ColumnNames.LowLimit, value) Then
					OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.LowLimit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressCheckListItem.HighLimit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighLimit As Nullable(Of System.Decimal)
			Get
				Return MyBase.GetSystemDecimal(TestProgressCheckListItemMetadata.ColumnNames.HighLimit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Decimal))
				If MyBase.SetSystemDecimal(TestProgressCheckListItemMetadata.ColumnNames.HighLimit, value) Then
					OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.HighLimit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressCheckListItem.Unit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unit As System.String
			Get
				Return MyBase.GetSystemString(TestProgressCheckListItemMetadata.ColumnNames.Unit)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(TestProgressCheckListItemMetadata.ColumnNames.Unit, value) Then
					OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.Unit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressCheckListItem.CheckTypeID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CheckTypeID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(TestProgressCheckListItemMetadata.ColumnNames.CheckTypeID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(TestProgressCheckListItemMetadata.ColumnNames.CheckTypeID, value) Then
					Me._UpToTestProgressCheckTypeByCheckTypeID = Nothing
					Me.OnPropertyChanged("UpToTestProgressCheckTypeByCheckTypeID")
					OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.CheckTypeID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressCheckListItem.ChannelName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelName As System.String
			Get
				Return MyBase.GetSystemString(TestProgressCheckListItemMetadata.ColumnNames.ChannelName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(TestProgressCheckListItemMetadata.ColumnNames.ChannelName, value) Then
					OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.ChannelName)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToTestProgressCheckTypeByCheckTypeID As TestProgressCheckType
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToTestProgressTestStepByTestStepID As TestProgressTestStep
		
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
												
						Case "TestStepID"
							Me.str().TestStepID = CType(value, string)
												
						Case "ParameterName"
							Me.str().ParameterName = CType(value, string)
												
						Case "LowLimit"
							Me.str().LowLimit = CType(value, string)
												
						Case "HighLimit"
							Me.str().HighLimit = CType(value, string)
												
						Case "Unit"
							Me.str().Unit = CType(value, string)
												
						Case "CheckTypeID"
							Me.str().CheckTypeID = CType(value, string)
												
						Case "ChannelName"
							Me.str().ChannelName = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.Id)
							End If
						
						Case "TestStepID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.TestStepID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.TestStepID)
							End If
						
						Case "LowLimit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Decimal" Then
								Me.LowLimit = CType(value, Nullable(Of System.Decimal))
								OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.LowLimit)
							End If
						
						Case "HighLimit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Decimal" Then
								Me.HighLimit = CType(value, Nullable(Of System.Decimal))
								OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.HighLimit)
							End If
						
						Case "CheckTypeID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.CheckTypeID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(TestProgressCheckListItemMetadata.PropertyNames.CheckTypeID)
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
		
			Public Sub New(ByVal entity As esTestProgressCheckListItem)
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
		  	
			Public Property TestStepID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.TestStepID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TestStepID = Nothing
					Else
						entity.TestStepID = Convert.ToInt32(Value)
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
		  	
			Public Property LowLimit As System.String 
				Get
					Dim data_ As Nullable(Of System.Decimal) = entity.LowLimit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLimit = Nothing
					Else
						entity.LowLimit = Convert.ToDecimal(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighLimit As System.String 
				Get
					Dim data_ As Nullable(Of System.Decimal) = entity.HighLimit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighLimit = Nothing
					Else
						entity.HighLimit = Convert.ToDecimal(Value)
					End If
				End Set
			End Property
		  	
			Public Property Unit As System.String 
				Get
					Dim data_ As System.String = entity.Unit
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Unit = Nothing
					Else
						entity.Unit = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CheckTypeID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.CheckTypeID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CheckTypeID = Nothing
					Else
						entity.CheckTypeID = Convert.ToInt32(Value)
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
		  

			Private entity As esTestProgressCheckListItem
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return TestProgressCheckListItemMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As TestProgressCheckListItemQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New TestProgressCheckListItemQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As TestProgressCheckListItemQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As TestProgressCheckListItemQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As TestProgressCheckListItemQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esTestProgressCheckListItemCollection
		Inherits esEntityCollection(Of TestProgressCheckListItem)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return TestProgressCheckListItemMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "TestProgressCheckListItemCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As TestProgressCheckListItemQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New TestProgressCheckListItemQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As TestProgressCheckListItemQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New TestProgressCheckListItemQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As TestProgressCheckListItemQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, TestProgressCheckListItemQuery))
		End Sub
		
		#End Region
						
		Private m_query As TestProgressCheckListItemQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esTestProgressCheckListItemQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return TestProgressCheckListItemMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "TestStepID" 
					Return Me.TestStepID
				Case "ParameterName" 
					Return Me.ParameterName
				Case "LowLimit" 
					Return Me.LowLimit
				Case "HighLimit" 
					Return Me.HighLimit
				Case "Unit" 
					Return Me.Unit
				Case "CheckTypeID" 
					Return Me.CheckTypeID
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
				Return New esQueryItem(Me, TestProgressCheckListItemMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property TestStepID As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressCheckListItemMetadata.ColumnNames.TestStepID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ParameterName As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressCheckListItemMetadata.ColumnNames.ParameterName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LowLimit As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressCheckListItemMetadata.ColumnNames.LowLimit, esSystemType.Decimal)
			End Get
		End Property 
		
		Public ReadOnly Property HighLimit As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressCheckListItemMetadata.ColumnNames.HighLimit, esSystemType.Decimal)
			End Get
		End Property 
		
		Public ReadOnly Property Unit As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressCheckListItemMetadata.ColumnNames.Unit, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CheckTypeID As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressCheckListItemMetadata.ColumnNames.CheckTypeID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelName As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressCheckListItemMetadata.ColumnNames.ChannelName, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class TestProgressCheckListItem 
		Inherits esTestProgressCheckListItem
		
	
		#Region "UpToTestProgressCheckTypeByCheckTypeID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - TestProgressCheckTypeTestProgressCheckListItem
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToTestProgressCheckTypeByCheckTypeID As TestProgressCheckType
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToTestProgressCheckTypeByCheckTypeID Is Nothing _
						 AndAlso Not CheckTypeID.Equals(Nothing)  Then
						
					Me._UpToTestProgressCheckTypeByCheckTypeID = New TestProgressCheckType()
					Me._UpToTestProgressCheckTypeByCheckTypeID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToTestProgressCheckTypeByCheckTypeID", Me._UpToTestProgressCheckTypeByCheckTypeID)
					Me._UpToTestProgressCheckTypeByCheckTypeID.Query.Where(Me._UpToTestProgressCheckTypeByCheckTypeID.Query.CheckTypeID = Me.CheckTypeID)
					Me._UpToTestProgressCheckTypeByCheckTypeID.Query.Load()
				End If

				Return Me._UpToTestProgressCheckTypeByCheckTypeID
			End Get
			
            Set(ByVal value As TestProgressCheckType)
				Me.RemovePreSave("UpToTestProgressCheckTypeByCheckTypeID")
				

				If value Is Nothing Then
				
					Me.CheckTypeID = Nothing
				
					Me._UpToTestProgressCheckTypeByCheckTypeID = Nothing
				Else
				
					Me.CheckTypeID = value.CheckTypeID
					
					Me._UpToTestProgressCheckTypeByCheckTypeID = value
					Me.SetPreSave("UpToTestProgressCheckTypeByCheckTypeID", Me._UpToTestProgressCheckTypeByCheckTypeID)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToTestProgressTestStepByTestStepID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - TestProgressTestStepTestProgressCheckListItem
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToTestProgressTestStepByTestStepID As TestProgressTestStep
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToTestProgressTestStepByTestStepID Is Nothing _
						 AndAlso Not TestStepID.Equals(Nothing)  Then
						
					Me._UpToTestProgressTestStepByTestStepID = New TestProgressTestStep()
					Me._UpToTestProgressTestStepByTestStepID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToTestProgressTestStepByTestStepID", Me._UpToTestProgressTestStepByTestStepID)
					Me._UpToTestProgressTestStepByTestStepID.Query.Where(Me._UpToTestProgressTestStepByTestStepID.Query.TestStepID = Me.TestStepID)
					Me._UpToTestProgressTestStepByTestStepID.Query.Load()
				End If

				Return Me._UpToTestProgressTestStepByTestStepID
			End Get
			
            Set(ByVal value As TestProgressTestStep)
				Me.RemovePreSave("UpToTestProgressTestStepByTestStepID")
				

				If value Is Nothing Then
				
					Me.TestStepID = Nothing
				
					Me._UpToTestProgressTestStepByTestStepID = Nothing
				Else
				
					Me.TestStepID = value.TestStepID
					
					Me._UpToTestProgressTestStepByTestStepID = value
					Me.SetPreSave("UpToTestProgressTestStepByTestStepID", Me._UpToTestProgressTestStepByTestStepID)
				End If
				
			End Set	

		End Property
		#End Region

		
			
		''' <summary>
		''' Used internally for retrieving AutoIncrementing keys
		''' during hierarchical PreSave.
		''' </summary>
		Protected Overrides Sub ApplyPreSaveKeys()
		
			If Not Me.es.IsDeleted And Not Me._UpToTestProgressCheckTypeByCheckTypeID Is Nothing Then
				Me.CheckTypeID = Me._UpToTestProgressCheckTypeByCheckTypeID.CheckTypeID
			End If
			
			If Not Me.es.IsDeleted And Not Me._UpToTestProgressTestStepByTestStepID Is Nothing Then
				Me.TestStepID = Me._UpToTestProgressTestStepByTestStepID.TestStepID
			End If
			
		End Sub
	End Class
		



	<Serializable> _
	Partial Public Class TestProgressCheckListItemMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(TestProgressCheckListItemMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = TestProgressCheckListItemMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.IsAutoIncrement = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressCheckListItemMetadata.ColumnNames.TestStepID, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = TestProgressCheckListItemMetadata.PropertyNames.TestStepID
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressCheckListItemMetadata.ColumnNames.ParameterName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = TestProgressCheckListItemMetadata.PropertyNames.ParameterName
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressCheckListItemMetadata.ColumnNames.LowLimit, 3, GetType(System.Decimal), esSystemType.Decimal)	
			c.PropertyName = TestProgressCheckListItemMetadata.PropertyNames.LowLimit
			c.NumericPrecision = 18
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressCheckListItemMetadata.ColumnNames.HighLimit, 4, GetType(System.Decimal), esSystemType.Decimal)	
			c.PropertyName = TestProgressCheckListItemMetadata.PropertyNames.HighLimit
			c.NumericPrecision = 18
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressCheckListItemMetadata.ColumnNames.Unit, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = TestProgressCheckListItemMetadata.PropertyNames.Unit
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressCheckListItemMetadata.ColumnNames.CheckTypeID, 6, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = TestProgressCheckListItemMetadata.PropertyNames.CheckTypeID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "1"
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressCheckListItemMetadata.ColumnNames.ChannelName, 7, GetType(System.String), esSystemType.String)	
			c.PropertyName = TestProgressCheckListItemMetadata.PropertyNames.ChannelName
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As TestProgressCheckListItemMetadata
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
			 Public Const TestStepID As String = "TestStepID"
			 Public Const ParameterName As String = "ParameterName"
			 Public Const LowLimit As String = "LowLimit"
			 Public Const HighLimit As String = "HighLimit"
			 Public Const Unit As String = "Unit"
			 Public Const CheckTypeID As String = "CheckTypeID"
			 Public Const ChannelName As String = "ChannelName"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const TestStepID As String = "TestStepID"
			 Public Const ParameterName As String = "ParameterName"
			 Public Const LowLimit As String = "LowLimit"
			 Public Const HighLimit As String = "HighLimit"
			 Public Const Unit As String = "Unit"
			 Public Const CheckTypeID As String = "CheckTypeID"
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
			SyncLock GetType(TestProgressCheckListItemMetadata)
			
				If TestProgressCheckListItemMetadata.mapDelegates Is Nothing Then
					TestProgressCheckListItemMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If TestProgressCheckListItemMetadata._meta Is Nothing Then
					TestProgressCheckListItemMetadata._meta = New TestProgressCheckListItemMetadata()
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
				meta.AddTypeMap("TestStepID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ParameterName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LowLimit", new esTypeMap("Decimal", "System.Decimal"))
				meta.AddTypeMap("HighLimit", new esTypeMap("Decimal", "System.Decimal"))
				meta.AddTypeMap("Unit", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CheckTypeID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ChannelName", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "TestProgressCheckListItem"
				meta.Destination = "TestProgressCheckListItem"
				
				meta.spInsert = "proc_TestProgressCheckListItemInsert"
				meta.spUpdate = "proc_TestProgressCheckListItemUpdate"
				meta.spDelete = "proc_TestProgressCheckListItemDelete"
				meta.spLoadAll = "proc_TestProgressCheckListItemLoadAll"
				meta.spLoadByPrimaryKey = "proc_TestProgressCheckListItemLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As TestProgressCheckListItemMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

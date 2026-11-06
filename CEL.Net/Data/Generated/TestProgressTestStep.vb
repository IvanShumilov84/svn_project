
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 27/06/2013 09:49:00
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
	' Encapsulates the 'TestProgressTestStep' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(TestProgressTestStep))> _
	<XmlType("TestProgressTestStep")> _	
	Partial Public Class TestProgressTestStep 
		Inherits esTestProgressTestStep
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New TestProgressTestStep()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal testStepID As System.Int32)
			Dim obj As New TestProgressTestStep()
			obj.TestStepID = testStepID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal testStepID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New TestProgressTestStep()
			obj.TestStepID = testStepID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("TestProgressTestStepCollection")> _
	Partial Public Class TestProgressTestStepCollection
		Inherits esTestProgressTestStepCollection
		Implements IEnumerable(Of TestProgressTestStep)
	
		Public Function FindByPrimaryKey(ByVal testStepID As System.Int32) As TestProgressTestStep
			Return MyBase.SingleOrDefault(Function(e) e.TestStepID.HasValue AndAlso e.TestStepID.Value = testStepID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(TestProgressTestStep))> _
		Public Class TestProgressTestStepCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of TestProgressTestStepCollection)
			
			Public Shared Widening Operator CType(packet As TestProgressTestStepCollectionWCFPacket) As TestProgressTestStepCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As TestProgressTestStepCollection) As TestProgressTestStepCollectionWCFPacket
				Return New TestProgressTestStepCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class TestProgressTestStepQuery 
		Inherits esTestProgressTestStepQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "TestProgressTestStepQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As TestProgressTestStepQuery) As String
			Return TestProgressTestStepQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As TestProgressTestStepQuery
			Return DirectCast(TestProgressTestStepQuery.SerializeHelper.FromXml(query, GetType(TestProgressTestStepQuery)), TestProgressTestStepQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esTestProgressTestStep
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal testStepID As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(testStepID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(testStepID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal testStepID As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(testStepID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(testStepID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal testStepID As System.Int32) As Boolean
		
			Dim query As New TestProgressTestStepQuery()
			query.Where(query.TestStepID = testStepID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal testStepID As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("TestStepID", testStepID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to TestProgressTestStep.TestStepID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TestStepID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(TestProgressTestStepMetadata.ColumnNames.TestStepID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(TestProgressTestStepMetadata.ColumnNames.TestStepID, value) Then
					OnPropertyChanged(TestProgressTestStepMetadata.PropertyNames.TestStepID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressTestStep.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(TestProgressTestStepMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(TestProgressTestStepMetadata.ColumnNames.EngineModel, value) Then
					OnPropertyChanged(TestProgressTestStepMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressTestStep.StepName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StepName As System.String
			Get
				Return MyBase.GetSystemString(TestProgressTestStepMetadata.ColumnNames.StepName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(TestProgressTestStepMetadata.ColumnNames.StepName, value) Then
					OnPropertyChanged(TestProgressTestStepMetadata.PropertyNames.StepName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressTestStep.Title
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Title As System.String
			Get
				Return MyBase.GetSystemString(TestProgressTestStepMetadata.ColumnNames.Title)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(TestProgressTestStepMetadata.ColumnNames.Title, value) Then
					OnPropertyChanged(TestProgressTestStepMetadata.PropertyNames.Title)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressTestStep.StepTypeID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StepTypeID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(TestProgressTestStepMetadata.ColumnNames.StepTypeID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(TestProgressTestStepMetadata.ColumnNames.StepTypeID, value) Then
					Me._UpToTestProgressStepTypeByStepTypeID = Nothing
					Me.OnPropertyChanged("UpToTestProgressStepTypeByStepTypeID")
					OnPropertyChanged(TestProgressTestStepMetadata.PropertyNames.StepTypeID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressTestStep.StepOrder
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StepOrder As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(TestProgressTestStepMetadata.ColumnNames.StepOrder)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(TestProgressTestStepMetadata.ColumnNames.StepOrder, value) Then
					OnPropertyChanged(TestProgressTestStepMetadata.PropertyNames.StepOrder)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToTestProgressStepTypeByStepTypeID As TestProgressStepType
		
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
												
						Case "TestStepID"
							Me.str().TestStepID = CType(value, string)
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
						Case "StepName"
							Me.str().StepName = CType(value, string)
												
						Case "Title"
							Me.str().Title = CType(value, string)
												
						Case "StepTypeID"
							Me.str().StepTypeID = CType(value, string)
												
						Case "StepOrder"
							Me.str().StepOrder = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "TestStepID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.TestStepID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(TestProgressTestStepMetadata.PropertyNames.TestStepID)
							End If
						
						Case "StepTypeID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.StepTypeID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(TestProgressTestStepMetadata.PropertyNames.StepTypeID)
							End If
						
						Case "StepOrder"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.StepOrder = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(TestProgressTestStepMetadata.PropertyNames.StepOrder)
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
		
			Public Sub New(ByVal entity As esTestProgressTestStep)
				Me.entity = entity
			End Sub				
		
	
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
		  	
			Public Property StepName As System.String 
				Get
					Dim data_ As System.String = entity.StepName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.StepName = Nothing
					Else
						entity.StepName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Title As System.String 
				Get
					Dim data_ As System.String = entity.Title
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Title = Nothing
					Else
						entity.Title = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property StepTypeID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.StepTypeID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.StepTypeID = Nothing
					Else
						entity.StepTypeID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property StepOrder As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.StepOrder
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.StepOrder = Nothing
					Else
						entity.StepOrder = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  

			Private entity As esTestProgressTestStep
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return TestProgressTestStepMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As TestProgressTestStepQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New TestProgressTestStepQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As TestProgressTestStepQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As TestProgressTestStepQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As TestProgressTestStepQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esTestProgressTestStepCollection
		Inherits esEntityCollection(Of TestProgressTestStep)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return TestProgressTestStepMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "TestProgressTestStepCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As TestProgressTestStepQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New TestProgressTestStepQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As TestProgressTestStepQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New TestProgressTestStepQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As TestProgressTestStepQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, TestProgressTestStepQuery))
		End Sub
		
		#End Region
						
		Private m_query As TestProgressTestStepQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esTestProgressTestStepQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return TestProgressTestStepMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "TestStepID" 
					Return Me.TestStepID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "StepName" 
					Return Me.StepName
				Case "Title" 
					Return Me.Title
				Case "StepTypeID" 
					Return Me.StepTypeID
				Case "StepOrder" 
					Return Me.StepOrder
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property TestStepID As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressTestStepMetadata.ColumnNames.TestStepID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressTestStepMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property StepName As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressTestStepMetadata.ColumnNames.StepName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Title As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressTestStepMetadata.ColumnNames.Title, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property StepTypeID As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressTestStepMetadata.ColumnNames.StepTypeID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property StepOrder As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressTestStepMetadata.ColumnNames.StepOrder, esSystemType.Int16)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class TestProgressTestStep 
		Inherits esTestProgressTestStep
		
	
		#Region "TestProgressCheckListItemCollectionByTestStepID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_TestProgressCheckListItemCollectionByTestStepID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.TestProgressTestStep.TestProgressCheckListItemCollectionByTestStepID_Delegate)
				map.PropertyName = "TestProgressCheckListItemCollectionByTestStepID"
				map.MyColumnName = "TestStepID"
				map.ParentColumnName = "TestStepID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub TestProgressCheckListItemCollectionByTestStepID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New TestProgressTestStepQuery(data.NextAlias())
			
			Dim mee As TestProgressCheckListItemQuery = If(data.You IsNot Nothing, TryCast(data.You, TestProgressCheckListItemQuery), New TestProgressCheckListItemQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.TestStepID = mee.TestStepID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - TestProgressTestStepTestProgressCheckListItem
		''' </summary>

		<XmlIgnore()> _ 
		Public Property TestProgressCheckListItemCollectionByTestStepID As TestProgressCheckListItemCollection 
		
			Get
				If Me._TestProgressCheckListItemCollectionByTestStepID Is Nothing Then
					Me._TestProgressCheckListItemCollectionByTestStepID = New TestProgressCheckListItemCollection()
					Me._TestProgressCheckListItemCollectionByTestStepID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("TestProgressCheckListItemCollectionByTestStepID", Me._TestProgressCheckListItemCollectionByTestStepID)
				
					If Not Me.TestStepID.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._TestProgressCheckListItemCollectionByTestStepID.Query.Where(Me._TestProgressCheckListItemCollectionByTestStepID.Query.TestStepID = Me.TestStepID)
							Me._TestProgressCheckListItemCollectionByTestStepID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._TestProgressCheckListItemCollectionByTestStepID.fks.Add(TestProgressCheckListItemMetadata.ColumnNames.TestStepID, Me.TestStepID)
					End If
				End If

				Return Me._TestProgressCheckListItemCollectionByTestStepID
			End Get
			
			Set(ByVal value As TestProgressCheckListItemCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._TestProgressCheckListItemCollectionByTestStepID Is Nothing Then

					Me.RemovePostSave("TestProgressCheckListItemCollectionByTestStepID")
					Me._TestProgressCheckListItemCollectionByTestStepID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _TestProgressCheckListItemCollectionByTestStepID As TestProgressCheckListItemCollection
		#End Region

		#Region "UpToTestProgressStepTypeByStepTypeID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - TestProgressStepTypeTestProgressTestStep
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToTestProgressStepTypeByStepTypeID As TestProgressStepType
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToTestProgressStepTypeByStepTypeID Is Nothing _
						 AndAlso Not StepTypeID.Equals(Nothing)  Then
						
					Me._UpToTestProgressStepTypeByStepTypeID = New TestProgressStepType()
					Me._UpToTestProgressStepTypeByStepTypeID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToTestProgressStepTypeByStepTypeID", Me._UpToTestProgressStepTypeByStepTypeID)
					Me._UpToTestProgressStepTypeByStepTypeID.Query.Where(Me._UpToTestProgressStepTypeByStepTypeID.Query.StepTypeID = Me.StepTypeID)
					Me._UpToTestProgressStepTypeByStepTypeID.Query.Load()
				End If

				Return Me._UpToTestProgressStepTypeByStepTypeID
			End Get
			
            Set(ByVal value As TestProgressStepType)
				Me.RemovePreSave("UpToTestProgressStepTypeByStepTypeID")
				

				If value Is Nothing Then
				
					Me.StepTypeID = Nothing
				
					Me._UpToTestProgressStepTypeByStepTypeID = Nothing
				Else
				
					Me.StepTypeID = value.StepTypeID
					
					Me._UpToTestProgressStepTypeByStepTypeID = value
					Me.SetPreSave("UpToTestProgressStepTypeByStepTypeID", Me._UpToTestProgressStepTypeByStepTypeID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "TestProgressCheckListItemCollectionByTestStepID"
					coll = Me.TestProgressCheckListItemCollectionByTestStepID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "TestProgressCheckListItemCollectionByTestStepID", GetType(TestProgressCheckListItemCollection), New TestProgressCheckListItem()))
			Return props
			
		End Function	
		''' <summary>
		''' Used internally for retrieving AutoIncrementing keys
		''' during hierarchical PreSave.
		''' </summary>
		Protected Overrides Sub ApplyPreSaveKeys()
		
			If Not Me.es.IsDeleted And Not Me._UpToTestProgressStepTypeByStepTypeID Is Nothing Then
				Me.StepTypeID = Me._UpToTestProgressStepTypeByStepTypeID.StepTypeID
			End If
			
		End Sub
		
		''' <summary>
		''' Called by ApplyPostSaveKeys 
		''' </summary>
		''' <param name="coll">The collection to enumerate over</param>
		''' <param name="key">"The column name</param>
		''' <param name="value">The column value</param>
		Private Sub Apply(coll As esEntityCollectionBase, key As String, value As Object)
			For Each obj As esEntity In coll
				If obj.es.IsAdded Then
					obj.SetProperty(key, value)
				End If
			Next
		End Sub
		
		''' <summary>
		''' Used internally for retrieving AutoIncrementing keys
		''' during hierarchical PostSave.
		''' </summary>
		Protected Overrides Sub ApplyPostSaveKeys()
		
			If Not Me._TestProgressCheckListItemCollectionByTestStepID Is Nothing Then
				Apply(Me._TestProgressCheckListItemCollectionByTestStepID, "TestStepID", Me.TestStepID)
			End If
			
		End Sub
	End Class
		



	<Serializable> _
	Partial Public Class TestProgressTestStepMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(TestProgressTestStepMetadata.ColumnNames.TestStepID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = TestProgressTestStepMetadata.PropertyNames.TestStepID
			c.IsInPrimaryKey = True
			c.IsAutoIncrement = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressTestStepMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = TestProgressTestStepMetadata.PropertyNames.EngineModel
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressTestStepMetadata.ColumnNames.StepName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = TestProgressTestStepMetadata.PropertyNames.StepName
			c.CharacterMaxLength = 15
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressTestStepMetadata.ColumnNames.Title, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = TestProgressTestStepMetadata.PropertyNames.Title
			c.CharacterMaxLength = 65
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressTestStepMetadata.ColumnNames.StepTypeID, 4, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = TestProgressTestStepMetadata.PropertyNames.StepTypeID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "1"
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressTestStepMetadata.ColumnNames.StepOrder, 5, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = TestProgressTestStepMetadata.PropertyNames.StepOrder
			c.NumericPrecision = 5
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As TestProgressTestStepMetadata
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
			 Public Const TestStepID As String = "TestStepID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const StepName As String = "StepName"
			 Public Const Title As String = "Title"
			 Public Const StepTypeID As String = "StepTypeID"
			 Public Const StepOrder As String = "StepOrder"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const TestStepID As String = "TestStepID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const StepName As String = "StepName"
			 Public Const Title As String = "Title"
			 Public Const StepTypeID As String = "StepTypeID"
			 Public Const StepOrder As String = "StepOrder"
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
			SyncLock GetType(TestProgressTestStepMetadata)
			
				If TestProgressTestStepMetadata.mapDelegates Is Nothing Then
					TestProgressTestStepMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If TestProgressTestStepMetadata._meta Is Nothing Then
					TestProgressTestStepMetadata._meta = New TestProgressTestStepMetadata()
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
				


				meta.AddTypeMap("TestStepID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EngineModel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("StepName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Title", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("StepTypeID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("StepOrder", new esTypeMap("Integer", "System.Int16"))			
				
				
				 
				meta.Source = "TestProgressTestStep"
				meta.Destination = "TestProgressTestStep"
				
				meta.spInsert = "proc_TestProgressTestStepInsert"
				meta.spUpdate = "proc_TestProgressTestStepUpdate"
				meta.spDelete = "proc_TestProgressTestStepDelete"
				meta.spLoadAll = "proc_TestProgressTestStepLoadAll"
				meta.spLoadByPrimaryKey = "proc_TestProgressTestStepLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As TestProgressTestStepMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

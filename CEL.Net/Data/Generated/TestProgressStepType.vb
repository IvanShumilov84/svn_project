
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
	' Encapsulates the 'TestProgressStepType' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(TestProgressStepType))> _
	<XmlType("TestProgressStepType")> _	
	Partial Public Class TestProgressStepType 
		Inherits esTestProgressStepType
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New TestProgressStepType()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal stepTypeID As System.Int32)
			Dim obj As New TestProgressStepType()
			obj.StepTypeID = stepTypeID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal stepTypeID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New TestProgressStepType()
			obj.StepTypeID = stepTypeID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("TestProgressStepTypeCollection")> _
	Partial Public Class TestProgressStepTypeCollection
		Inherits esTestProgressStepTypeCollection
		Implements IEnumerable(Of TestProgressStepType)
	
		Public Function FindByPrimaryKey(ByVal stepTypeID As System.Int32) As TestProgressStepType
			Return MyBase.SingleOrDefault(Function(e) e.StepTypeID.HasValue AndAlso e.StepTypeID.Value = stepTypeID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(TestProgressStepType))> _
		Public Class TestProgressStepTypeCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of TestProgressStepTypeCollection)
			
			Public Shared Widening Operator CType(packet As TestProgressStepTypeCollectionWCFPacket) As TestProgressStepTypeCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As TestProgressStepTypeCollection) As TestProgressStepTypeCollectionWCFPacket
				Return New TestProgressStepTypeCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class TestProgressStepTypeQuery 
		Inherits esTestProgressStepTypeQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "TestProgressStepTypeQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As TestProgressStepTypeQuery) As String
			Return TestProgressStepTypeQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As TestProgressStepTypeQuery
			Return DirectCast(TestProgressStepTypeQuery.SerializeHelper.FromXml(query, GetType(TestProgressStepTypeQuery)), TestProgressStepTypeQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esTestProgressStepType
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal stepTypeID As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(stepTypeID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(stepTypeID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal stepTypeID As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(stepTypeID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(stepTypeID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal stepTypeID As System.Int32) As Boolean
		
			Dim query As New TestProgressStepTypeQuery()
			query.Where(query.StepTypeID = stepTypeID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal stepTypeID As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("StepTypeID", stepTypeID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to TestProgressStepType.StepTypeID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StepTypeID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(TestProgressStepTypeMetadata.ColumnNames.StepTypeID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(TestProgressStepTypeMetadata.ColumnNames.StepTypeID, value) Then
					OnPropertyChanged(TestProgressStepTypeMetadata.PropertyNames.StepTypeID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to TestProgressStepType.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(TestProgressStepTypeMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(TestProgressStepTypeMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(TestProgressStepTypeMetadata.PropertyNames.Description)
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
												
						Case "StepTypeID"
							Me.str().StepTypeID = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "StepTypeID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.StepTypeID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(TestProgressStepTypeMetadata.PropertyNames.StepTypeID)
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
		
			Public Sub New(ByVal entity As esTestProgressStepType)
				Me.entity = entity
			End Sub				
		
	
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
		  

			Private entity As esTestProgressStepType
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return TestProgressStepTypeMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As TestProgressStepTypeQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New TestProgressStepTypeQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As TestProgressStepTypeQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As TestProgressStepTypeQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As TestProgressStepTypeQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esTestProgressStepTypeCollection
		Inherits esEntityCollection(Of TestProgressStepType)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return TestProgressStepTypeMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "TestProgressStepTypeCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As TestProgressStepTypeQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New TestProgressStepTypeQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As TestProgressStepTypeQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New TestProgressStepTypeQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As TestProgressStepTypeQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, TestProgressStepTypeQuery))
		End Sub
		
		#End Region
						
		Private m_query As TestProgressStepTypeQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esTestProgressStepTypeQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return TestProgressStepTypeMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "StepTypeID" 
					Return Me.StepTypeID
				Case "Description" 
					Return Me.Description
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property StepTypeID As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressStepTypeMetadata.ColumnNames.StepTypeID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, TestProgressStepTypeMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class TestProgressStepType 
		Inherits esTestProgressStepType
		
	
		#Region "TestProgressTestStepCollectionByStepTypeID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_TestProgressTestStepCollectionByStepTypeID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.TestProgressStepType.TestProgressTestStepCollectionByStepTypeID_Delegate)
				map.PropertyName = "TestProgressTestStepCollectionByStepTypeID"
				map.MyColumnName = "StepTypeID"
				map.ParentColumnName = "StepTypeID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub TestProgressTestStepCollectionByStepTypeID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New TestProgressStepTypeQuery(data.NextAlias())
			
			Dim mee As TestProgressTestStepQuery = If(data.You IsNot Nothing, TryCast(data.You, TestProgressTestStepQuery), New TestProgressTestStepQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.StepTypeID = mee.StepTypeID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - TestProgressStepTypeTestProgressTestStep
		''' </summary>

		<XmlIgnore()> _ 
		Public Property TestProgressTestStepCollectionByStepTypeID As TestProgressTestStepCollection 
		
			Get
				If Me._TestProgressTestStepCollectionByStepTypeID Is Nothing Then
					Me._TestProgressTestStepCollectionByStepTypeID = New TestProgressTestStepCollection()
					Me._TestProgressTestStepCollectionByStepTypeID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("TestProgressTestStepCollectionByStepTypeID", Me._TestProgressTestStepCollectionByStepTypeID)
				
					If Not Me.StepTypeID.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._TestProgressTestStepCollectionByStepTypeID.Query.Where(Me._TestProgressTestStepCollectionByStepTypeID.Query.StepTypeID = Me.StepTypeID)
							Me._TestProgressTestStepCollectionByStepTypeID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._TestProgressTestStepCollectionByStepTypeID.fks.Add(TestProgressTestStepMetadata.ColumnNames.StepTypeID, Me.StepTypeID)
					End If
				End If

				Return Me._TestProgressTestStepCollectionByStepTypeID
			End Get
			
			Set(ByVal value As TestProgressTestStepCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._TestProgressTestStepCollectionByStepTypeID Is Nothing Then

					Me.RemovePostSave("TestProgressTestStepCollectionByStepTypeID")
					Me._TestProgressTestStepCollectionByStepTypeID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _TestProgressTestStepCollectionByStepTypeID As TestProgressTestStepCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "TestProgressTestStepCollectionByStepTypeID"
					coll = Me.TestProgressTestStepCollectionByStepTypeID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "TestProgressTestStepCollectionByStepTypeID", GetType(TestProgressTestStepCollection), New TestProgressTestStep()))
			Return props
			
		End Function
		
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
		
			If Not Me._TestProgressTestStepCollectionByStepTypeID Is Nothing Then
				Apply(Me._TestProgressTestStepCollectionByStepTypeID, "StepTypeID", Me.StepTypeID)
			End If
			
		End Sub
	End Class
		



	<Serializable> _
	Partial Public Class TestProgressStepTypeMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(TestProgressStepTypeMetadata.ColumnNames.StepTypeID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = TestProgressStepTypeMetadata.PropertyNames.StepTypeID
			c.IsInPrimaryKey = True
			c.IsAutoIncrement = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(TestProgressStepTypeMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = TestProgressStepTypeMetadata.PropertyNames.Description
			c.CharacterMaxLength = 25
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As TestProgressStepTypeMetadata
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
			 Public Const StepTypeID As String = "StepTypeID"
			 Public Const Description As String = "Description"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const StepTypeID As String = "StepTypeID"
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
			SyncLock GetType(TestProgressStepTypeMetadata)
			
				If TestProgressStepTypeMetadata.mapDelegates Is Nothing Then
					TestProgressStepTypeMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If TestProgressStepTypeMetadata._meta Is Nothing Then
					TestProgressStepTypeMetadata._meta = New TestProgressStepTypeMetadata()
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
				


				meta.AddTypeMap("StepTypeID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "TestProgressStepType"
				meta.Destination = "TestProgressStepType"
				
				meta.spInsert = "proc_TestProgressStepTypeInsert"
				meta.spUpdate = "proc_TestProgressStepTypeUpdate"
				meta.spDelete = "proc_TestProgressStepTypeDelete"
				meta.spLoadAll = "proc_TestProgressStepTypeLoadAll"
				meta.spLoadByPrimaryKey = "proc_TestProgressStepTypeLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As TestProgressStepTypeMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

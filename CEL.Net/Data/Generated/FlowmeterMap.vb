
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 5/7/2015 10:46:35 AM
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
	' Encapsulates the 'Flowmeter_Map' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(FlowmeterMap))> _
	<XmlType("FlowmeterMap")> _	
	Partial Public Class FlowmeterMap 
		Inherits esFlowmeterMap
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New FlowmeterMap()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32)
			Dim obj As New FlowmeterMap()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New FlowmeterMap()
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
	<XmlType("FlowmeterMapCollection")> _
	Partial Public Class FlowmeterMapCollection
		Inherits esFlowmeterMapCollection
		Implements IEnumerable(Of FlowmeterMap)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32) As FlowmeterMap
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(FlowmeterMap))> _
		Public Class FlowmeterMapCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of FlowmeterMapCollection)
			
			Public Shared Widening Operator CType(packet As FlowmeterMapCollectionWCFPacket) As FlowmeterMapCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As FlowmeterMapCollection) As FlowmeterMapCollectionWCFPacket
				Return New FlowmeterMapCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class FlowmeterMapQuery 
		Inherits esFlowmeterMapQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "FlowmeterMapQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As FlowmeterMapQuery) As String
			Return FlowmeterMapQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As FlowmeterMapQuery
			Return DirectCast(FlowmeterMapQuery.SerializeHelper.FromXml(query, GetType(FlowmeterMapQuery)), FlowmeterMapQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esFlowmeterMap
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
		
			Dim query As New FlowmeterMapQuery()
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
		' Maps to Flowmeter_Map.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(FlowmeterMapMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(FlowmeterMapMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(FlowmeterMapMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Map.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(FlowmeterMapMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(FlowmeterMapMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(FlowmeterMapMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Map.FlowmeterID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FlowmeterID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(FlowmeterMapMetadata.ColumnNames.FlowmeterID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(FlowmeterMapMetadata.ColumnNames.FlowmeterID, value) Then
					Me._UpToFlowmeterMasterByFlowmeterID = Nothing
					Me.OnPropertyChanged("UpToFlowmeterMasterByFlowmeterID")
					OnPropertyChanged(FlowmeterMapMetadata.PropertyNames.FlowmeterID)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToFlowmeterMasterByFlowmeterID As FlowmeterMaster
		
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
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "FlowmeterID"
							Me.str().FlowmeterID = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(FlowmeterMapMetadata.PropertyNames.Id)
							End If
						
						Case "FlowmeterID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.FlowmeterID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(FlowmeterMapMetadata.PropertyNames.FlowmeterID)
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
		
			Public Sub New(ByVal entity As esFlowmeterMap)
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
		  	
			Public Property FlowmeterID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.FlowmeterID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FlowmeterID = Nothing
					Else
						entity.FlowmeterID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  

			Private entity As esFlowmeterMap
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return FlowmeterMapMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As FlowmeterMapQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New FlowmeterMapQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As FlowmeterMapQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As FlowmeterMapQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As FlowmeterMapQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esFlowmeterMapCollection
		Inherits esEntityCollection(Of FlowmeterMap)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return FlowmeterMapMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "FlowmeterMapCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As FlowmeterMapQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New FlowmeterMapQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As FlowmeterMapQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New FlowmeterMapQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As FlowmeterMapQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, FlowmeterMapQuery))
		End Sub
		
		#End Region
						
		Private m_query As FlowmeterMapQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esFlowmeterMapQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return FlowmeterMapMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "Description" 
					Return Me.Description
				Case "FlowmeterID" 
					Return Me.FlowmeterID
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterMapMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterMapMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FlowmeterID As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterMapMetadata.ColumnNames.FlowmeterID, esSystemType.Int32)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class FlowmeterMap 
		Inherits esFlowmeterMap
		
	
		#Region "UpToUutCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - Flowmeter_MapFlowmeter
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToUutCollection As UutCollection
		
			Get
				If Me._UpToUutCollection Is Nothing Then
					Me._UpToUutCollection = New UutCollection()
					Me._UpToUutCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToUutCollection", Me._UpToUutCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.Id.Equals(Nothing) Then 
				
						Dim m As New UutQuery("m")
						Dim j As New FlowmeterQuery("j")
						m.Select(m)
						m.InnerJoin(j).On(m.ProjectID = j.ProjectID And m.EngineModel = j.EngineModel)
                        m.Where(j.FlowmeterMapID = Me.Id)

						Me._UpToUutCollection.Load(m)

					End If
				End If

				Return Me._UpToUutCollection
			End Get
			
			Set(ByVal value As UutCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToUutCollection Is Nothing Then

					Me.RemovePostSave("UpToUutCollection")
					Me._UpToUutCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - Flowmeter_MapFlowmeter
		''' </summary>
		Public Sub AssociateUutCollection(entity As Uut)
			If Me._FlowmeterCollection Is Nothing Then
				Me._FlowmeterCollection = New FlowmeterCollection()
				Me._FlowmeterCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("FlowmeterCollection", Me._FlowmeterCollection)
			End If
			
			Dim obj As Flowmeter = Me._FlowmeterCollection.AddNew()
			obj.FlowmeterMapID = Me.Id
			obj.ProjectID = entity.ProjectID
			obj.EngineModel = entity.EngineModel			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - Flowmeter_MapFlowmeter
		''' </summary>
		Public Sub DissociateUutCollection(entity As Uut)
			If Me._FlowmeterCollection Is Nothing Then
				Me._FlowmeterCollection = new FlowmeterCollection()
				Me._FlowmeterCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("FlowmeterCollection", Me._FlowmeterCollection)
			End If

			Dim obj As Flowmeter = Me._FlowmeterCollection.AddNew()
			obj.FlowmeterMapID = Me.Id
            obj.ProjectID = entity.ProjectID
            obj.EngineModel = entity.EngineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToUutCollection As UutCollection
		Private _FlowmeterCollection As FlowmeterCollection
		#End Region

		#Region "FlowmeterCollectionByFlowmeterMapID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_FlowmeterCollectionByFlowmeterMapID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.FlowmeterMap.FlowmeterCollectionByFlowmeterMapID_Delegate)
				map.PropertyName = "FlowmeterCollectionByFlowmeterMapID"
				map.MyColumnName = "FlowmeterMapID"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub FlowmeterCollectionByFlowmeterMapID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New FlowmeterMapQuery(data.NextAlias())
			
			Dim mee As FlowmeterQuery = If(data.You IsNot Nothing, TryCast(data.You, FlowmeterQuery), New FlowmeterQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.FlowmeterMapID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Flowmeter_MapFlowmeter
		''' </summary>

		<XmlIgnore()> _ 
		Public Property FlowmeterCollectionByFlowmeterMapID As FlowmeterCollection 
		
			Get
				If Me._FlowmeterCollectionByFlowmeterMapID Is Nothing Then
					Me._FlowmeterCollectionByFlowmeterMapID = New FlowmeterCollection()
					Me._FlowmeterCollectionByFlowmeterMapID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("FlowmeterCollectionByFlowmeterMapID", Me._FlowmeterCollectionByFlowmeterMapID)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._FlowmeterCollectionByFlowmeterMapID.Query.Where(Me._FlowmeterCollectionByFlowmeterMapID.Query.FlowmeterMapID = Me.Id)
							Me._FlowmeterCollectionByFlowmeterMapID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._FlowmeterCollectionByFlowmeterMapID.fks.Add(FlowmeterMetadata.ColumnNames.FlowmeterMapID, Me.Id)
					End If
				End If

				Return Me._FlowmeterCollectionByFlowmeterMapID
			End Get
			
			Set(ByVal value As FlowmeterCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._FlowmeterCollectionByFlowmeterMapID Is Nothing Then

					Me.RemovePostSave("FlowmeterCollectionByFlowmeterMapID")
					Me._FlowmeterCollectionByFlowmeterMapID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _FlowmeterCollectionByFlowmeterMapID As FlowmeterCollection
		#End Region

		#Region "UpToFlowmeterMasterByFlowmeterID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Flowmeter_MasterFlowmeter_Map
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToFlowmeterMasterByFlowmeterID As FlowmeterMaster
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToFlowmeterMasterByFlowmeterID Is Nothing _
						 AndAlso Not FlowmeterID.Equals(Nothing)  Then
						
					Me._UpToFlowmeterMasterByFlowmeterID = New FlowmeterMaster()
					Me._UpToFlowmeterMasterByFlowmeterID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToFlowmeterMasterByFlowmeterID", Me._UpToFlowmeterMasterByFlowmeterID)
					Me._UpToFlowmeterMasterByFlowmeterID.Query.Where(Me._UpToFlowmeterMasterByFlowmeterID.Query.Id = Me.FlowmeterID)
					Me._UpToFlowmeterMasterByFlowmeterID.Query.Load()
				End If

				Return Me._UpToFlowmeterMasterByFlowmeterID
			End Get
			
            Set(ByVal value As FlowmeterMaster)
				Me.RemovePreSave("UpToFlowmeterMasterByFlowmeterID")
				

				If value Is Nothing Then
				
					Me.FlowmeterID = Nothing
				
					Me._UpToFlowmeterMasterByFlowmeterID = Nothing
				Else
				
					Me.FlowmeterID = value.Id
					
					Me._UpToFlowmeterMasterByFlowmeterID = value
					Me.SetPreSave("UpToFlowmeterMasterByFlowmeterID", Me._UpToFlowmeterMasterByFlowmeterID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "FlowmeterCollectionByFlowmeterMapID"
					coll = Me.FlowmeterCollectionByFlowmeterMapID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "FlowmeterCollectionByFlowmeterMapID", GetType(FlowmeterCollection), New Flowmeter()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class FlowmeterMapMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(FlowmeterMapMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = FlowmeterMapMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterMapMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = FlowmeterMapMetadata.PropertyNames.Description
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterMapMetadata.ColumnNames.FlowmeterID, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = FlowmeterMapMetadata.PropertyNames.FlowmeterID
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As FlowmeterMapMetadata
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
			 Public Const Description As String = "Description"
			 Public Const FlowmeterID As String = "FlowmeterID"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const Description As String = "Description"
			 Public Const FlowmeterID As String = "FlowmeterID"
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
			SyncLock GetType(FlowmeterMapMetadata)
			
				If FlowmeterMapMetadata.mapDelegates Is Nothing Then
					FlowmeterMapMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If FlowmeterMapMetadata._meta Is Nothing Then
					FlowmeterMapMetadata._meta = New FlowmeterMapMetadata()
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
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("FlowmeterID", new esTypeMap("Long", "System.Int32"))			
				
				
				 
				meta.Source = "Flowmeter_Map"
				meta.Destination = "Flowmeter_Map"
				
				meta.spInsert = "proc_Flowmeter_MapInsert"
				meta.spUpdate = "proc_Flowmeter_MapUpdate"
				meta.spDelete = "proc_Flowmeter_MapDelete"
				meta.spLoadAll = "proc_Flowmeter_MapLoadAll"
				meta.spLoadByPrimaryKey = "proc_Flowmeter_MapLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As FlowmeterMapMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

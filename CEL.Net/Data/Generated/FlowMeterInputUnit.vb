
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
	' Encapsulates the 'FlowMeter_InputUnit' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(FlowMeterInputUnit))> _
	<XmlType("FlowMeterInputUnit")> _	
	Partial Public Class FlowMeterInputUnit 
		Inherits esFlowMeterInputUnit
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New FlowMeterInputUnit()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Single)
			Dim obj As New FlowMeterInputUnit()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Single, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New FlowMeterInputUnit()
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
	<XmlType("FlowMeterInputUnitCollection")> _
	Partial Public Class FlowMeterInputUnitCollection
		Inherits esFlowMeterInputUnitCollection
		Implements IEnumerable(Of FlowMeterInputUnit)
	
		Public Function FindByPrimaryKey(ByVal id As System.Single) As FlowMeterInputUnit
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(FlowMeterInputUnit))> _
		Public Class FlowMeterInputUnitCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of FlowMeterInputUnitCollection)
			
			Public Shared Widening Operator CType(packet As FlowMeterInputUnitCollectionWCFPacket) As FlowMeterInputUnitCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As FlowMeterInputUnitCollection) As FlowMeterInputUnitCollectionWCFPacket
				Return New FlowMeterInputUnitCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class FlowMeterInputUnitQuery 
		Inherits esFlowMeterInputUnitQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "FlowMeterInputUnitQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As FlowMeterInputUnitQuery) As String
			Return FlowMeterInputUnitQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As FlowMeterInputUnitQuery
			Return DirectCast(FlowMeterInputUnitQuery.SerializeHelper.FromXml(query, GetType(FlowMeterInputUnitQuery)), FlowMeterInputUnitQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esFlowMeterInputUnit
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal id As System.Single) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(id)
			Else
				Return LoadByPrimaryKeyStoredProcedure(id)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal id As System.Single) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(id)
			Else
				Return LoadByPrimaryKeyStoredProcedure(id)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal id As System.Single) As Boolean
		
			Dim query As New FlowMeterInputUnitQuery()
			query.Where(query.Id = id)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal id As System.Single) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("Id", id)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to FlowMeter_InputUnit.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(FlowMeterInputUnitMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(FlowMeterInputUnitMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(FlowMeterInputUnitMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to FlowMeter_InputUnit.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(FlowMeterInputUnitMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(FlowMeterInputUnitMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(FlowMeterInputUnitMetadata.PropertyNames.Description)
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
												
						Case "Id"
							Me.str().Id = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.Id = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(FlowMeterInputUnitMetadata.PropertyNames.Id)
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
		
			Public Sub New(ByVal entity As esFlowMeterInputUnit)
				Me.entity = entity
			End Sub				
		
	
			Public Property Id As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.Id
					
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
						entity.Id = Convert.ToSingle(Value)
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
		  

			Private entity As esFlowMeterInputUnit
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return FlowMeterInputUnitMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As FlowMeterInputUnitQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New FlowMeterInputUnitQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As FlowMeterInputUnitQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As FlowMeterInputUnitQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As FlowMeterInputUnitQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esFlowMeterInputUnitCollection
		Inherits esEntityCollection(Of FlowMeterInputUnit)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return FlowMeterInputUnitMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "FlowMeterInputUnitCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As FlowMeterInputUnitQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New FlowMeterInputUnitQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As FlowMeterInputUnitQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New FlowMeterInputUnitQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As FlowMeterInputUnitQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, FlowMeterInputUnitQuery))
		End Sub
		
		#End Region
						
		Private m_query As FlowMeterInputUnitQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esFlowMeterInputUnitQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return FlowMeterInputUnitMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "Description" 
					Return Me.Description
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, FlowMeterInputUnitMetadata.ColumnNames.Id, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, FlowMeterInputUnitMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class FlowMeterInputUnit 
		Inherits esFlowMeterInputUnit
		
	
		#Region "FlowmeterMasterCollectionByTableInputUnit - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_FlowmeterMasterCollectionByTableInputUnit() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.FlowMeterInputUnit.FlowmeterMasterCollectionByTableInputUnit_Delegate)
				map.PropertyName = "FlowmeterMasterCollectionByTableInputUnit"
				map.MyColumnName = "TableInputUnit"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub FlowmeterMasterCollectionByTableInputUnit_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New FlowMeterInputUnitQuery(data.NextAlias())
			
			Dim mee As FlowmeterMasterQuery = If(data.You IsNot Nothing, TryCast(data.You, FlowmeterMasterQuery), New FlowmeterMasterQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.TableInputUnit)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - FlowMeter_InputUnitFlowmeter_Master
		''' </summary>

		<XmlIgnore()> _ 
		Public Property FlowmeterMasterCollectionByTableInputUnit As FlowmeterMasterCollection 
		
			Get
				If Me._FlowmeterMasterCollectionByTableInputUnit Is Nothing Then
					Me._FlowmeterMasterCollectionByTableInputUnit = New FlowmeterMasterCollection()
					Me._FlowmeterMasterCollectionByTableInputUnit.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("FlowmeterMasterCollectionByTableInputUnit", Me._FlowmeterMasterCollectionByTableInputUnit)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._FlowmeterMasterCollectionByTableInputUnit.Query.Where(Me._FlowmeterMasterCollectionByTableInputUnit.Query.TableInputUnit = Me.Id)
							Me._FlowmeterMasterCollectionByTableInputUnit.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._FlowmeterMasterCollectionByTableInputUnit.fks.Add(FlowmeterMasterMetadata.ColumnNames.TableInputUnit, Me.Id)
					End If
				End If

				Return Me._FlowmeterMasterCollectionByTableInputUnit
			End Get
			
			Set(ByVal value As FlowmeterMasterCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._FlowmeterMasterCollectionByTableInputUnit Is Nothing Then

					Me.RemovePostSave("FlowmeterMasterCollectionByTableInputUnit")
					Me._FlowmeterMasterCollectionByTableInputUnit = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _FlowmeterMasterCollectionByTableInputUnit As FlowmeterMasterCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "FlowmeterMasterCollectionByTableInputUnit"
					coll = Me.FlowmeterMasterCollectionByTableInputUnit
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "FlowmeterMasterCollectionByTableInputUnit", GetType(FlowmeterMasterCollection), New FlowmeterMaster()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class FlowMeterInputUnitMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(FlowMeterInputUnitMetadata.ColumnNames.Id, 0, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = FlowMeterInputUnitMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowMeterInputUnitMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = FlowMeterInputUnitMetadata.PropertyNames.Description
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As FlowMeterInputUnitMetadata
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
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
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
			SyncLock GetType(FlowMeterInputUnitMetadata)
			
				If FlowMeterInputUnitMetadata.mapDelegates Is Nothing Then
					FlowMeterInputUnitMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If FlowMeterInputUnitMetadata._meta Is Nothing Then
					FlowMeterInputUnitMetadata._meta = New FlowMeterInputUnitMetadata()
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
				


				meta.AddTypeMap("Id", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "FlowMeter_InputUnit"
				meta.Destination = "FlowMeter_InputUnit"
				
				meta.spInsert = "proc_FlowMeter_InputUnitInsert"
				meta.spUpdate = "proc_FlowMeter_InputUnitUpdate"
				meta.spDelete = "proc_FlowMeter_InputUnitDelete"
				meta.spLoadAll = "proc_FlowMeter_InputUnitLoadAll"
				meta.spLoadByPrimaryKey = "proc_FlowMeter_InputUnitLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As FlowMeterInputUnitMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace


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
	' Encapsulates the 'Flowmeter_Detail' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(FlowmeterDetail))> _
	<XmlType("FlowmeterDetail")> _	
	Partial Public Class FlowmeterDetail 
		Inherits esFlowmeterDetail
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New FlowmeterDetail()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal flowmeterID As System.Int32, ByVal dataPoint As System.Int32)
			Dim obj As New FlowmeterDetail()
			obj.FlowmeterID = flowmeterID
			obj.DataPoint = dataPoint
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal flowmeterID As System.Int32, ByVal dataPoint As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New FlowmeterDetail()
			obj.FlowmeterID = flowmeterID
			obj.DataPoint = dataPoint
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("FlowmeterDetailCollection")> _
	Partial Public Class FlowmeterDetailCollection
		Inherits esFlowmeterDetailCollection
		Implements IEnumerable(Of FlowmeterDetail)
	
		Public Function FindByPrimaryKey(ByVal flowmeterID As System.Int32, ByVal dataPoint As System.Int32) As FlowmeterDetail
			Return MyBase.SingleOrDefault(Function(e) e.FlowmeterID.HasValue AndAlso e.FlowmeterID.Value = flowmeterID And e.DataPoint.HasValue AndAlso e.DataPoint.Value = dataPoint)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(FlowmeterDetail))> _
		Public Class FlowmeterDetailCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of FlowmeterDetailCollection)
			
			Public Shared Widening Operator CType(packet As FlowmeterDetailCollectionWCFPacket) As FlowmeterDetailCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As FlowmeterDetailCollection) As FlowmeterDetailCollectionWCFPacket
				Return New FlowmeterDetailCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class FlowmeterDetailQuery 
		Inherits esFlowmeterDetailQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "FlowmeterDetailQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As FlowmeterDetailQuery) As String
			Return FlowmeterDetailQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As FlowmeterDetailQuery
			Return DirectCast(FlowmeterDetailQuery.SerializeHelper.FromXml(query, GetType(FlowmeterDetailQuery)), FlowmeterDetailQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esFlowmeterDetail
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal flowmeterID As System.Int32, ByVal dataPoint As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(flowmeterID, dataPoint)
			Else
				Return LoadByPrimaryKeyStoredProcedure(flowmeterID, dataPoint)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal flowmeterID As System.Int32, ByVal dataPoint As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(flowmeterID, dataPoint)
			Else
				Return LoadByPrimaryKeyStoredProcedure(flowmeterID, dataPoint)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal flowmeterID As System.Int32, ByVal dataPoint As System.Int32) As Boolean
		
			Dim query As New FlowmeterDetailQuery()
			query.Where(query.FlowmeterID = flowmeterID And query.DataPoint = dataPoint)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal flowmeterID As System.Int32, ByVal dataPoint As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("FlowmeterID", flowmeterID)
						parms.Add("DataPoint", dataPoint)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to Flowmeter_Detail.FlowmeterID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FlowmeterID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(FlowmeterDetailMetadata.ColumnNames.FlowmeterID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(FlowmeterDetailMetadata.ColumnNames.FlowmeterID, value) Then
					Me._UpToFlowmeterMasterByFlowmeterID = Nothing
					Me.OnPropertyChanged("UpToFlowmeterMasterByFlowmeterID")
					OnPropertyChanged(FlowmeterDetailMetadata.PropertyNames.FlowmeterID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Detail.DataPoint
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DataPoint As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(FlowmeterDetailMetadata.ColumnNames.DataPoint)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(FlowmeterDetailMetadata.ColumnNames.DataPoint, value) Then
					OnPropertyChanged(FlowmeterDetailMetadata.PropertyNames.DataPoint)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Detail.InputValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property InputValue As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(FlowmeterDetailMetadata.ColumnNames.InputValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(FlowmeterDetailMetadata.ColumnNames.InputValue, value) Then
					OnPropertyChanged(FlowmeterDetailMetadata.PropertyNames.InputValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Detail.OutputValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OutputValue As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(FlowmeterDetailMetadata.ColumnNames.OutputValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(FlowmeterDetailMetadata.ColumnNames.OutputValue, value) Then
					OnPropertyChanged(FlowmeterDetailMetadata.PropertyNames.OutputValue)
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
												
						Case "FlowmeterID"
							Me.str().FlowmeterID = CType(value, string)
												
						Case "DataPoint"
							Me.str().DataPoint = CType(value, string)
												
						Case "InputValue"
							Me.str().InputValue = CType(value, string)
												
						Case "OutputValue"
							Me.str().OutputValue = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "FlowmeterID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.FlowmeterID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(FlowmeterDetailMetadata.PropertyNames.FlowmeterID)
							End If
						
						Case "DataPoint"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.DataPoint = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(FlowmeterDetailMetadata.PropertyNames.DataPoint)
							End If
						
						Case "InputValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.InputValue = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(FlowmeterDetailMetadata.PropertyNames.InputValue)
							End If
						
						Case "OutputValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.OutputValue = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(FlowmeterDetailMetadata.PropertyNames.OutputValue)
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
		
			Public Sub New(ByVal entity As esFlowmeterDetail)
				Me.entity = entity
			End Sub				
		
	
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
		  	
			Public Property DataPoint As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.DataPoint
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DataPoint = Nothing
					Else
						entity.DataPoint = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property InputValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.InputValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.InputValue = Nothing
					Else
						entity.InputValue = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property OutputValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.OutputValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OutputValue = Nothing
					Else
						entity.OutputValue = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  

			Private entity As esFlowmeterDetail
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return FlowmeterDetailMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As FlowmeterDetailQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New FlowmeterDetailQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As FlowmeterDetailQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As FlowmeterDetailQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As FlowmeterDetailQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esFlowmeterDetailCollection
		Inherits esEntityCollection(Of FlowmeterDetail)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return FlowmeterDetailMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "FlowmeterDetailCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As FlowmeterDetailQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New FlowmeterDetailQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As FlowmeterDetailQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New FlowmeterDetailQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As FlowmeterDetailQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, FlowmeterDetailQuery))
		End Sub
		
		#End Region
						
		Private m_query As FlowmeterDetailQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esFlowmeterDetailQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return FlowmeterDetailMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "FlowmeterID" 
					Return Me.FlowmeterID
				Case "DataPoint" 
					Return Me.DataPoint
				Case "InputValue" 
					Return Me.InputValue
				Case "OutputValue" 
					Return Me.OutputValue
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property FlowmeterID As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterDetailMetadata.ColumnNames.FlowmeterID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property DataPoint As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterDetailMetadata.ColumnNames.DataPoint, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property InputValue As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterDetailMetadata.ColumnNames.InputValue, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property OutputValue As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterDetailMetadata.ColumnNames.OutputValue, esSystemType.Single)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class FlowmeterDetail 
		Inherits esFlowmeterDetail
		
	
		#Region "UpToFlowmeterMasterByFlowmeterID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Flowmeter_MasterFlowmeter_Detail
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

		
		
	End Class
		



	<Serializable> _
	Partial Public Class FlowmeterDetailMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(FlowmeterDetailMetadata.ColumnNames.FlowmeterID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = FlowmeterDetailMetadata.PropertyNames.FlowmeterID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterDetailMetadata.ColumnNames.DataPoint, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = FlowmeterDetailMetadata.PropertyNames.DataPoint
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterDetailMetadata.ColumnNames.InputValue, 2, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = FlowmeterDetailMetadata.PropertyNames.InputValue
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterDetailMetadata.ColumnNames.OutputValue, 3, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = FlowmeterDetailMetadata.PropertyNames.OutputValue
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As FlowmeterDetailMetadata
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
			 Public Const FlowmeterID As String = "FlowmeterID"
			 Public Const DataPoint As String = "DataPoint"
			 Public Const InputValue As String = "InputValue"
			 Public Const OutputValue As String = "OutputValue"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const FlowmeterID As String = "FlowmeterID"
			 Public Const DataPoint As String = "DataPoint"
			 Public Const InputValue As String = "InputValue"
			 Public Const OutputValue As String = "OutputValue"
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
			SyncLock GetType(FlowmeterDetailMetadata)
			
				If FlowmeterDetailMetadata.mapDelegates Is Nothing Then
					FlowmeterDetailMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If FlowmeterDetailMetadata._meta Is Nothing Then
					FlowmeterDetailMetadata._meta = New FlowmeterDetailMetadata()
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
				


				meta.AddTypeMap("FlowmeterID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("DataPoint", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("InputValue", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("OutputValue", new esTypeMap("Single", "System.Single"))			
				
				
				 
				meta.Source = "Flowmeter_Detail"
				meta.Destination = "Flowmeter_Detail"
				
				meta.spInsert = "proc_Flowmeter_DetailInsert"
				meta.spUpdate = "proc_Flowmeter_DetailUpdate"
				meta.spDelete = "proc_Flowmeter_DetailDelete"
				meta.spLoadAll = "proc_Flowmeter_DetailLoadAll"
				meta.spLoadByPrimaryKey = "proc_Flowmeter_DetailLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As FlowmeterDetailMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

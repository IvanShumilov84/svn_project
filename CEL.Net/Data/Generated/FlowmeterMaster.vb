
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
	' Encapsulates the 'Flowmeter_Master' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(FlowmeterMaster))> _
	<XmlType("FlowmeterMaster")> _	
	Partial Public Class FlowmeterMaster 
		Inherits esFlowmeterMaster
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New FlowmeterMaster()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32)
			Dim obj As New FlowmeterMaster()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New FlowmeterMaster()
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
	<XmlType("FlowmeterMasterCollection")> _
	Partial Public Class FlowmeterMasterCollection
		Inherits esFlowmeterMasterCollection
		Implements IEnumerable(Of FlowmeterMaster)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32) As FlowmeterMaster
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(FlowmeterMaster))> _
		Public Class FlowmeterMasterCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of FlowmeterMasterCollection)
			
			Public Shared Widening Operator CType(packet As FlowmeterMasterCollectionWCFPacket) As FlowmeterMasterCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As FlowmeterMasterCollection) As FlowmeterMasterCollectionWCFPacket
				Return New FlowmeterMasterCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class FlowmeterMasterQuery 
		Inherits esFlowmeterMasterQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "FlowmeterMasterQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As FlowmeterMasterQuery) As String
			Return FlowmeterMasterQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As FlowmeterMasterQuery
			Return DirectCast(FlowmeterMasterQuery.SerializeHelper.FromXml(query, GetType(FlowmeterMasterQuery)), FlowmeterMasterQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esFlowmeterMaster
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
		
			Dim query As New FlowmeterMasterQuery()
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
		' Maps to Flowmeter_Master.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(FlowmeterMasterMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(FlowmeterMasterMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Master.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(FlowmeterMasterMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(FlowmeterMasterMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.Note)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Master.TableInputUnit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TableInputUnit As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(FlowmeterMasterMetadata.ColumnNames.TableInputUnit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(FlowmeterMasterMetadata.ColumnNames.TableInputUnit, value) Then
					Me._UpToFlowMeterInputUnitByTableInputUnit = Nothing
					Me.OnPropertyChanged("UpToFlowMeterInputUnitByTableInputUnit")
					OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.TableInputUnit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Master.TableOutputUnit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TableOutputUnit As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(FlowmeterMasterMetadata.ColumnNames.TableOutputUnit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(FlowmeterMasterMetadata.ColumnNames.TableOutputUnit, value) Then
					Me._UpToFlowMeterOutputUnitByTableOutputUnit = Nothing
					Me.OnPropertyChanged("UpToFlowMeterOutputUnitByTableOutputUnit")
					OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.TableOutputUnit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Master.SerialNumber
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SerialNumber As System.String
			Get
				Return MyBase.GetSystemString(FlowmeterMasterMetadata.ColumnNames.SerialNumber)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(FlowmeterMasterMetadata.ColumnNames.SerialNumber, value) Then
					OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.SerialNumber)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Master.CalibrationDue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationDue As System.String
			Get
				Return MyBase.GetSystemString(FlowmeterMasterMetadata.ColumnNames.CalibrationDue)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(FlowmeterMasterMetadata.ColumnNames.CalibrationDue, value) Then
					OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.CalibrationDue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Flowmeter_Master.SG
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sg As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(FlowmeterMasterMetadata.ColumnNames.Sg)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(FlowmeterMasterMetadata.ColumnNames.Sg, value) Then
					OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.Sg)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToFlowMeterInputUnitByTableInputUnit As FlowMeterInputUnit
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToFlowMeterOutputUnitByTableOutputUnit As FlowMeterOutputUnit
		
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
												
						Case "Note"
							Me.str().Note = CType(value, string)
												
						Case "TableInputUnit"
							Me.str().TableInputUnit = CType(value, string)
												
						Case "TableOutputUnit"
							Me.str().TableOutputUnit = CType(value, string)
												
						Case "SerialNumber"
							Me.str().SerialNumber = CType(value, string)
												
						Case "CalibrationDue"
							Me.str().CalibrationDue = CType(value, string)
												
						Case "Sg"
							Me.str().Sg = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.Id)
							End If
						
						Case "TableInputUnit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.TableInputUnit = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.TableInputUnit)
							End If
						
						Case "TableOutputUnit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.TableOutputUnit = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.TableOutputUnit)
							End If
						
						Case "Sg"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.Sg = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(FlowmeterMasterMetadata.PropertyNames.Sg)
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
		
			Public Sub New(ByVal entity As esFlowmeterMaster)
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
		  	
			Public Property TableInputUnit As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.TableInputUnit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TableInputUnit = Nothing
					Else
						entity.TableInputUnit = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property TableOutputUnit As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.TableOutputUnit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TableOutputUnit = Nothing
					Else
						entity.TableOutputUnit = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property SerialNumber As System.String 
				Get
					Dim data_ As System.String = entity.SerialNumber
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SerialNumber = Nothing
					Else
						entity.SerialNumber = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationDue As System.String 
				Get
					Dim data_ As System.String = entity.CalibrationDue
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationDue = Nothing
					Else
						entity.CalibrationDue = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sg As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.Sg
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sg = Nothing
					Else
						entity.Sg = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  

			Private entity As esFlowmeterMaster
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return FlowmeterMasterMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As FlowmeterMasterQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New FlowmeterMasterQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As FlowmeterMasterQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As FlowmeterMasterQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As FlowmeterMasterQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esFlowmeterMasterCollection
		Inherits esEntityCollection(Of FlowmeterMaster)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return FlowmeterMasterMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "FlowmeterMasterCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As FlowmeterMasterQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New FlowmeterMasterQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As FlowmeterMasterQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New FlowmeterMasterQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As FlowmeterMasterQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, FlowmeterMasterQuery))
		End Sub
		
		#End Region
						
		Private m_query As FlowmeterMasterQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esFlowmeterMasterQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return FlowmeterMasterMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "Note" 
					Return Me.Note
				Case "TableInputUnit" 
					Return Me.TableInputUnit
				Case "TableOutputUnit" 
					Return Me.TableOutputUnit
				Case "SerialNumber" 
					Return Me.SerialNumber
				Case "CalibrationDue" 
					Return Me.CalibrationDue
				Case "Sg" 
					Return Me.Sg
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterMasterMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterMasterMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TableInputUnit As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterMasterMetadata.ColumnNames.TableInputUnit, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property TableOutputUnit As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterMasterMetadata.ColumnNames.TableOutputUnit, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property SerialNumber As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterMasterMetadata.ColumnNames.SerialNumber, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationDue As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterMasterMetadata.ColumnNames.CalibrationDue, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sg As esQueryItem
			Get
				Return New esQueryItem(Me, FlowmeterMasterMetadata.ColumnNames.Sg, esSystemType.Double)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class FlowmeterMaster 
		Inherits esFlowmeterMaster
		
	
		#Region "FlowmeterDetailCollectionByFlowmeterID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_FlowmeterDetailCollectionByFlowmeterID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.FlowmeterMaster.FlowmeterDetailCollectionByFlowmeterID_Delegate)
				map.PropertyName = "FlowmeterDetailCollectionByFlowmeterID"
				map.MyColumnName = "FlowmeterID"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub FlowmeterDetailCollectionByFlowmeterID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New FlowmeterMasterQuery(data.NextAlias())
			
			Dim mee As FlowmeterDetailQuery = If(data.You IsNot Nothing, TryCast(data.You, FlowmeterDetailQuery), New FlowmeterDetailQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.FlowmeterID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Flowmeter_MasterFlowmeter_Detail
		''' </summary>

		<XmlIgnore()> _ 
		Public Property FlowmeterDetailCollectionByFlowmeterID As FlowmeterDetailCollection 
		
			Get
				If Me._FlowmeterDetailCollectionByFlowmeterID Is Nothing Then
					Me._FlowmeterDetailCollectionByFlowmeterID = New FlowmeterDetailCollection()
					Me._FlowmeterDetailCollectionByFlowmeterID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("FlowmeterDetailCollectionByFlowmeterID", Me._FlowmeterDetailCollectionByFlowmeterID)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._FlowmeterDetailCollectionByFlowmeterID.Query.Where(Me._FlowmeterDetailCollectionByFlowmeterID.Query.FlowmeterID = Me.Id)
							Me._FlowmeterDetailCollectionByFlowmeterID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._FlowmeterDetailCollectionByFlowmeterID.fks.Add(FlowmeterDetailMetadata.ColumnNames.FlowmeterID, Me.Id)
					End If
				End If

				Return Me._FlowmeterDetailCollectionByFlowmeterID
			End Get
			
			Set(ByVal value As FlowmeterDetailCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._FlowmeterDetailCollectionByFlowmeterID Is Nothing Then

					Me.RemovePostSave("FlowmeterDetailCollectionByFlowmeterID")
					Me._FlowmeterDetailCollectionByFlowmeterID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _FlowmeterDetailCollectionByFlowmeterID As FlowmeterDetailCollection
		#End Region

		#Region "FlowmeterMapCollectionByFlowmeterID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_FlowmeterMapCollectionByFlowmeterID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.FlowmeterMaster.FlowmeterMapCollectionByFlowmeterID_Delegate)
				map.PropertyName = "FlowmeterMapCollectionByFlowmeterID"
				map.MyColumnName = "FlowmeterID"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub FlowmeterMapCollectionByFlowmeterID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New FlowmeterMasterQuery(data.NextAlias())
			
			Dim mee As FlowmeterMapQuery = If(data.You IsNot Nothing, TryCast(data.You, FlowmeterMapQuery), New FlowmeterMapQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.FlowmeterID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Flowmeter_MasterFlowmeter_Map
		''' </summary>

		<XmlIgnore()> _ 
		Public Property FlowmeterMapCollectionByFlowmeterID As FlowmeterMapCollection 
		
			Get
				If Me._FlowmeterMapCollectionByFlowmeterID Is Nothing Then
					Me._FlowmeterMapCollectionByFlowmeterID = New FlowmeterMapCollection()
					Me._FlowmeterMapCollectionByFlowmeterID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("FlowmeterMapCollectionByFlowmeterID", Me._FlowmeterMapCollectionByFlowmeterID)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._FlowmeterMapCollectionByFlowmeterID.Query.Where(Me._FlowmeterMapCollectionByFlowmeterID.Query.FlowmeterID = Me.Id)
							Me._FlowmeterMapCollectionByFlowmeterID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._FlowmeterMapCollectionByFlowmeterID.fks.Add(FlowmeterMapMetadata.ColumnNames.FlowmeterID, Me.Id)
					End If
				End If

				Return Me._FlowmeterMapCollectionByFlowmeterID
			End Get
			
			Set(ByVal value As FlowmeterMapCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._FlowmeterMapCollectionByFlowmeterID Is Nothing Then

					Me.RemovePostSave("FlowmeterMapCollectionByFlowmeterID")
					Me._FlowmeterMapCollectionByFlowmeterID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _FlowmeterMapCollectionByFlowmeterID As FlowmeterMapCollection
		#End Region

		#Region "UpToFlowMeterInputUnitByTableInputUnit - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - FlowMeter_InputUnitFlowmeter_Master
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToFlowMeterInputUnitByTableInputUnit As FlowMeterInputUnit
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToFlowMeterInputUnitByTableInputUnit Is Nothing _
						 AndAlso Not TableInputUnit.Equals(Nothing)  Then
						
					Me._UpToFlowMeterInputUnitByTableInputUnit = New FlowMeterInputUnit()
					Me._UpToFlowMeterInputUnitByTableInputUnit.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToFlowMeterInputUnitByTableInputUnit", Me._UpToFlowMeterInputUnitByTableInputUnit)
					Me._UpToFlowMeterInputUnitByTableInputUnit.Query.Where(Me._UpToFlowMeterInputUnitByTableInputUnit.Query.Id = Me.TableInputUnit)
					Me._UpToFlowMeterInputUnitByTableInputUnit.Query.Load()
				End If

				Return Me._UpToFlowMeterInputUnitByTableInputUnit
			End Get
			
            Set(ByVal value As FlowMeterInputUnit)
				Me.RemovePreSave("UpToFlowMeterInputUnitByTableInputUnit")
				

				If value Is Nothing Then
				
					Me.TableInputUnit = Nothing
				
					Me._UpToFlowMeterInputUnitByTableInputUnit = Nothing
				Else
				
					Me.TableInputUnit = value.Id
					
					Me._UpToFlowMeterInputUnitByTableInputUnit = value
					Me.SetPreSave("UpToFlowMeterInputUnitByTableInputUnit", Me._UpToFlowMeterInputUnitByTableInputUnit)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToFlowMeterOutputUnitByTableOutputUnit - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - FlowMeter_OutputUnitFlowmeter_Master
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToFlowMeterOutputUnitByTableOutputUnit As FlowMeterOutputUnit
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToFlowMeterOutputUnitByTableOutputUnit Is Nothing _
						 AndAlso Not TableOutputUnit.Equals(Nothing)  Then
						
					Me._UpToFlowMeterOutputUnitByTableOutputUnit = New FlowMeterOutputUnit()
					Me._UpToFlowMeterOutputUnitByTableOutputUnit.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToFlowMeterOutputUnitByTableOutputUnit", Me._UpToFlowMeterOutputUnitByTableOutputUnit)
					Me._UpToFlowMeterOutputUnitByTableOutputUnit.Query.Where(Me._UpToFlowMeterOutputUnitByTableOutputUnit.Query.Id = Me.TableOutputUnit)
					Me._UpToFlowMeterOutputUnitByTableOutputUnit.Query.Load()
				End If

				Return Me._UpToFlowMeterOutputUnitByTableOutputUnit
			End Get
			
            Set(ByVal value As FlowMeterOutputUnit)
				Me.RemovePreSave("UpToFlowMeterOutputUnitByTableOutputUnit")
				

				If value Is Nothing Then
				
					Me.TableOutputUnit = Nothing
				
					Me._UpToFlowMeterOutputUnitByTableOutputUnit = Nothing
				Else
				
					Me.TableOutputUnit = value.Id
					
					Me._UpToFlowMeterOutputUnitByTableOutputUnit = value
					Me.SetPreSave("UpToFlowMeterOutputUnitByTableOutputUnit", Me._UpToFlowMeterOutputUnitByTableOutputUnit)
				End If
				
			End Set	

		End Property
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "FlowmeterDetailCollectionByFlowmeterID"
					coll = Me.FlowmeterDetailCollectionByFlowmeterID
					Exit Select
				Case "FlowmeterMapCollectionByFlowmeterID"
					coll = Me.FlowmeterMapCollectionByFlowmeterID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "FlowmeterDetailCollectionByFlowmeterID", GetType(FlowmeterDetailCollection), New FlowmeterDetail()))
			props.Add(new esPropertyDescriptor(Me, "FlowmeterMapCollectionByFlowmeterID", GetType(FlowmeterMapCollection), New FlowmeterMap()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class FlowmeterMasterMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(FlowmeterMasterMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = FlowmeterMasterMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterMasterMetadata.ColumnNames.Note, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = FlowmeterMasterMetadata.PropertyNames.Note
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterMasterMetadata.ColumnNames.TableInputUnit, 2, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = FlowmeterMasterMetadata.PropertyNames.TableInputUnit
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterMasterMetadata.ColumnNames.TableOutputUnit, 3, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = FlowmeterMasterMetadata.PropertyNames.TableOutputUnit
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterMasterMetadata.ColumnNames.SerialNumber, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = FlowmeterMasterMetadata.PropertyNames.SerialNumber
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterMasterMetadata.ColumnNames.CalibrationDue, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = FlowmeterMasterMetadata.PropertyNames.CalibrationDue
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(FlowmeterMasterMetadata.ColumnNames.Sg, 6, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = FlowmeterMasterMetadata.PropertyNames.Sg
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As FlowmeterMasterMetadata
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
			 Public Const Note As String = "Note"
			 Public Const TableInputUnit As String = "TableInputUnit"
			 Public Const TableOutputUnit As String = "TableOutputUnit"
			 Public Const SerialNumber As String = "SerialNumber"
			 Public Const CalibrationDue As String = "CalibrationDue"
			 Public Const Sg As String = "SG"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const Note As String = "Note"
			 Public Const TableInputUnit As String = "TableInputUnit"
			 Public Const TableOutputUnit As String = "TableOutputUnit"
			 Public Const SerialNumber As String = "SerialNumber"
			 Public Const CalibrationDue As String = "CalibrationDue"
			 Public Const Sg As String = "Sg"
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
			SyncLock GetType(FlowmeterMasterMetadata)
			
				If FlowmeterMasterMetadata.mapDelegates Is Nothing Then
					FlowmeterMasterMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If FlowmeterMasterMetadata._meta Is Nothing Then
					FlowmeterMasterMetadata._meta = New FlowmeterMasterMetadata()
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
				meta.AddTypeMap("Note", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("TableInputUnit", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("TableOutputUnit", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("SerialNumber", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationDue", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sg", new esTypeMap("Double", "System.Double"))			
				
				
				 
				meta.Source = "Flowmeter_Master"
				meta.Destination = "Flowmeter_Master"
				
				meta.spInsert = "proc_Flowmeter_MasterInsert"
				meta.spUpdate = "proc_Flowmeter_MasterUpdate"
				meta.spDelete = "proc_Flowmeter_MasterDelete"
				meta.spLoadAll = "proc_Flowmeter_MasterLoadAll"
				meta.spLoadByPrimaryKey = "proc_Flowmeter_MasterLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As FlowmeterMasterMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

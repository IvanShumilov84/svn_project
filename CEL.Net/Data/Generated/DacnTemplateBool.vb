
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 5/23/2013 3:55:40 PM
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
	' Encapsulates the 'DacnTemplateBool' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(DacnTemplateBool))> _
	<XmlType("DacnTemplateBool")> _	
	Partial Public Class DacnTemplateBool 
		Inherits esDacnTemplateBool
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New DacnTemplateBool()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal name As System.String)
			Dim obj As New DacnTemplateBool()
			obj.ProjectID = projectID
			obj.Name = name
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal name As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New DacnTemplateBool()
			obj.ProjectID = projectID
			obj.Name = name
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("DacnTemplateBoolCollection")> _
	Partial Public Class DacnTemplateBoolCollection
		Inherits esDacnTemplateBoolCollection
		Implements IEnumerable(Of DacnTemplateBool)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal name As System.String) As DacnTemplateBool
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.Name = name)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(DacnTemplateBool))> _
		Public Class DacnTemplateBoolCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of DacnTemplateBoolCollection)
			
			Public Shared Widening Operator CType(packet As DacnTemplateBoolCollectionWCFPacket) As DacnTemplateBoolCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As DacnTemplateBoolCollection) As DacnTemplateBoolCollectionWCFPacket
				Return New DacnTemplateBoolCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class DacnTemplateBoolQuery 
		Inherits esDacnTemplateBoolQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "DacnTemplateBoolQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As DacnTemplateBoolQuery) As String
			Return DacnTemplateBoolQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As DacnTemplateBoolQuery
			Return DirectCast(DacnTemplateBoolQuery.SerializeHelper.FromXml(query, GetType(DacnTemplateBoolQuery)), DacnTemplateBoolQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDacnTemplateBool
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal name As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, name)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, name)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal name As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, name)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, name)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal name As System.String) As Boolean
		
			Dim query As New DacnTemplateBoolQuery()
			query.Where(query.ProjectID = projectID And query.Name = name)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal name As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("Name", name)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to DacnTemplateBool.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateBoolMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateBoolMetadata.ColumnNames.ProjectID, value) Then
					OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateBool.Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Name As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateBoolMetadata.ColumnNames.Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateBoolMetadata.ColumnNames.Name, value) Then
					OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateBool.RDataID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RDataID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateBoolMetadata.ColumnNames.RDataID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateBoolMetadata.ColumnNames.RDataID, value) Then
					OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.RDataID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateBool.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateBoolMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateBoolMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateBool.FieldEquipmentID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FieldEquipmentID As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateBoolMetadata.ColumnNames.FieldEquipmentID)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateBoolMetadata.ColumnNames.FieldEquipmentID, value) Then
					OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.FieldEquipmentID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateBool.DeviceID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DeviceID As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateBoolMetadata.ColumnNames.DeviceID)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateBoolMetadata.ColumnNames.DeviceID, value) Then
					OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.DeviceID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateBool.Channel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Channel As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateBoolMetadata.ColumnNames.Channel)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateBoolMetadata.ColumnNames.Channel, value) Then
					OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.Channel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateBool.Bit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Bit As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateBoolMetadata.ColumnNames.Bit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateBoolMetadata.ColumnNames.Bit, value) Then
					OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.Bit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateBool.OPC_Group
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OPCGroup As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateBoolMetadata.ColumnNames.OPCGroup)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateBoolMetadata.ColumnNames.OPCGroup, value) Then
					OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.OPCGroup)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateBool.OPC_ItemID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OPCItemID As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateBoolMetadata.ColumnNames.OPCItemID)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateBoolMetadata.ColumnNames.OPCItemID, value) Then
					OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.OPCItemID)
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
												
						Case "Name"
							Me.str().Name = CType(value, string)
												
						Case "RDataID"
							Me.str().RDataID = CType(value, string)
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "FieldEquipmentID"
							Me.str().FieldEquipmentID = CType(value, string)
												
						Case "DeviceID"
							Me.str().DeviceID = CType(value, string)
												
						Case "Channel"
							Me.str().Channel = CType(value, string)
												
						Case "Bit"
							Me.str().Bit = CType(value, string)
												
						Case "OPCGroup"
							Me.str().OPCGroup = CType(value, string)
												
						Case "OPCItemID"
							Me.str().OPCItemID = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.ProjectID)
							End If
						
						Case "RDataID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.RDataID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.RDataID)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "Channel"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Channel = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.Channel)
							End If
						
						Case "Bit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Bit = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateBoolMetadata.PropertyNames.Bit)
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
		
			Public Sub New(ByVal entity As esDacnTemplateBool)
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
		  	
			Public Property Name As System.String 
				Get
					Dim data_ As System.String = entity.Name
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Name = Nothing
					Else
						entity.Name = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property RDataID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.RDataID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RDataID = Nothing
					Else
						entity.RDataID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property ExportToCsv As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.ExportToCsv
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ExportToCsv = Nothing
					Else
						entity.ExportToCsv = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property FieldEquipmentID As System.String 
				Get
					Dim data_ As System.String = entity.FieldEquipmentID
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FieldEquipmentID = Nothing
					Else
						entity.FieldEquipmentID = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DeviceID As System.String 
				Get
					Dim data_ As System.String = entity.DeviceID
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DeviceID = Nothing
					Else
						entity.DeviceID = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Channel As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Channel
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Channel = Nothing
					Else
						entity.Channel = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Bit As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Bit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Bit = Nothing
					Else
						entity.Bit = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property OPCGroup As System.String 
				Get
					Dim data_ As System.String = entity.OPCGroup
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OPCGroup = Nothing
					Else
						entity.OPCGroup = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property OPCItemID As System.String 
				Get
					Dim data_ As System.String = entity.OPCItemID
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OPCItemID = Nothing
					Else
						entity.OPCItemID = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esDacnTemplateBool
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DacnTemplateBoolMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As DacnTemplateBoolQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DacnTemplateBoolQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As DacnTemplateBoolQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As DacnTemplateBoolQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As DacnTemplateBoolQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDacnTemplateBoolCollection
		Inherits esEntityCollection(Of DacnTemplateBool)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DacnTemplateBoolMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "DacnTemplateBoolCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As DacnTemplateBoolQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DacnTemplateBoolQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As DacnTemplateBoolQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New DacnTemplateBoolQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As DacnTemplateBoolQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, DacnTemplateBoolQuery))
		End Sub
		
		#End Region
						
		Private m_query As DacnTemplateBoolQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDacnTemplateBoolQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return DacnTemplateBoolMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "Name" 
					Return Me.Name
				Case "RDataID" 
					Return Me.RDataID
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "FieldEquipmentID" 
					Return Me.FieldEquipmentID
				Case "DeviceID" 
					Return Me.DeviceID
				Case "Channel" 
					Return Me.Channel
				Case "Bit" 
					Return Me.Bit
				Case "OPCGroup" 
					Return Me.OPCGroup
				Case "OPCItemID" 
					Return Me.OPCItemID
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateBoolMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Name As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateBoolMetadata.ColumnNames.Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property RDataID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateBoolMetadata.ColumnNames.RDataID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateBoolMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property FieldEquipmentID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateBoolMetadata.ColumnNames.FieldEquipmentID, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DeviceID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateBoolMetadata.ColumnNames.DeviceID, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Channel As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateBoolMetadata.ColumnNames.Channel, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Bit As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateBoolMetadata.ColumnNames.Bit, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property OPCGroup As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateBoolMetadata.ColumnNames.OPCGroup, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OPCItemID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateBoolMetadata.ColumnNames.OPCItemID, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class DacnTemplateBool 
		Inherits esDacnTemplateBool
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class DacnTemplateBoolMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(DacnTemplateBoolMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateBoolMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateBoolMetadata.ColumnNames.Name, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateBoolMetadata.PropertyNames.Name
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO NAME"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateBoolMetadata.ColumnNames.RDataID, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateBoolMetadata.PropertyNames.RDataID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateBoolMetadata.ColumnNames.ExportToCsv, 3, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateBoolMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateBoolMetadata.ColumnNames.FieldEquipmentID, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateBoolMetadata.PropertyNames.FieldEquipmentID
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateBoolMetadata.ColumnNames.DeviceID, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateBoolMetadata.PropertyNames.DeviceID
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateBoolMetadata.ColumnNames.Channel, 6, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateBoolMetadata.PropertyNames.Channel
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateBoolMetadata.ColumnNames.Bit, 7, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateBoolMetadata.PropertyNames.Bit
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateBoolMetadata.ColumnNames.OPCGroup, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateBoolMetadata.PropertyNames.OPCGroup
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateBoolMetadata.ColumnNames.OPCItemID, 9, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateBoolMetadata.PropertyNames.OPCItemID
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As DacnTemplateBoolMetadata
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
			 Public Const Name As String = "Name"
			 Public Const RDataID As String = "RDataID"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const FieldEquipmentID As String = "FieldEquipmentID"
			 Public Const DeviceID As String = "DeviceID"
			 Public Const Channel As String = "Channel"
			 Public Const Bit As String = "Bit"
			 Public Const OPCGroup As String = "OPC_Group"
			 Public Const OPCItemID As String = "OPC_ItemID"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const Name As String = "Name"
			 Public Const RDataID As String = "RDataID"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const FieldEquipmentID As String = "FieldEquipmentID"
			 Public Const DeviceID As String = "DeviceID"
			 Public Const Channel As String = "Channel"
			 Public Const Bit As String = "Bit"
			 Public Const OPCGroup As String = "OPCGroup"
			 Public Const OPCItemID As String = "OPCItemID"
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
			SyncLock GetType(DacnTemplateBoolMetadata)
			
				If DacnTemplateBoolMetadata.mapDelegates Is Nothing Then
					DacnTemplateBoolMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If DacnTemplateBoolMetadata._meta Is Nothing Then
					DacnTemplateBoolMetadata._meta = New DacnTemplateBoolMetadata()
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
				meta.AddTypeMap("Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("RDataID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("FieldEquipmentID", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DeviceID", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Channel", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Bit", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("OPCGroup", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OPCItemID", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "DacnTemplateBool"
				meta.Destination = "DacnTemplateBool"
				
				meta.spInsert = "proc_DacnTemplateBoolInsert"
				meta.spUpdate = "proc_DacnTemplateBoolUpdate"
				meta.spDelete = "proc_DacnTemplateBoolDelete"
				meta.spLoadAll = "proc_DacnTemplateBoolLoadAll"
				meta.spLoadByPrimaryKey = "proc_DacnTemplateBoolLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As DacnTemplateBoolMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

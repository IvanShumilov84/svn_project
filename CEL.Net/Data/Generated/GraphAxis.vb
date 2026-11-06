
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 7/25/2019 1:21:20 PM
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
	' Encapsulates the 'GraphAxis' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(GraphAxis))> _
	<XmlType("GraphAxis")> _	
	Partial Public Class GraphAxis 
		Inherits esGraphAxis
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New GraphAxis()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32)
			Dim obj As New GraphAxis()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New GraphAxis()
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
	<XmlType("GraphAxisCollection")> _
	Partial Public Class GraphAxisCollection
		Inherits esGraphAxisCollection
		Implements IEnumerable(Of GraphAxis)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32) As GraphAxis
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(GraphAxis))> _
		Public Class GraphAxisCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of GraphAxisCollection)
			
			Public Shared Widening Operator CType(packet As GraphAxisCollectionWCFPacket) As GraphAxisCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As GraphAxisCollection) As GraphAxisCollectionWCFPacket
				Return New GraphAxisCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class GraphAxisQuery 
		Inherits esGraphAxisQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "GraphAxisQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As GraphAxisQuery) As String
			Return GraphAxisQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As GraphAxisQuery
			Return DirectCast(GraphAxisQuery.SerializeHelper.FromXml(query, GetType(GraphAxisQuery)), GraphAxisQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esGraphAxis
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
		
			Dim query As New GraphAxisQuery()
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
		' Maps to GraphAxis.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(GraphAxisMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(GraphAxisMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphAxis.GID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Gid As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(GraphAxisMetadata.ColumnNames.Gid)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(GraphAxisMetadata.ColumnNames.Gid, value) Then
					Me._UpToEngineGraphPresetByGid = Nothing
					Me.OnPropertyChanged("UpToEngineGraphPresetByGid")
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.Gid)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphAxis.AxisTitle
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AxisTitle As System.String
			Get
				Return MyBase.GetSystemString(GraphAxisMetadata.ColumnNames.AxisTitle)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(GraphAxisMetadata.ColumnNames.AxisTitle, value) Then
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.AxisTitle)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphAxis.ScaleMin
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ScaleMin As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(GraphAxisMetadata.ColumnNames.ScaleMin)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(GraphAxisMetadata.ColumnNames.ScaleMin, value) Then
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.ScaleMin)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphAxis.ScaleMinAuto
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ScaleMinAuto As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(GraphAxisMetadata.ColumnNames.ScaleMinAuto)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(GraphAxisMetadata.ColumnNames.ScaleMinAuto, value) Then
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.ScaleMinAuto)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphAxis.ScaleMax
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ScaleMax As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(GraphAxisMetadata.ColumnNames.ScaleMax)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(GraphAxisMetadata.ColumnNames.ScaleMax, value) Then
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.ScaleMax)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphAxis.ScaleMaxAuto
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ScaleMaxAuto As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(GraphAxisMetadata.ColumnNames.ScaleMaxAuto)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(GraphAxisMetadata.ColumnNames.ScaleMaxAuto, value) Then
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.ScaleMaxAuto)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphAxis.Unit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unit As System.String
			Get
				Return MyBase.GetSystemString(GraphAxisMetadata.ColumnNames.Unit)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(GraphAxisMetadata.ColumnNames.Unit, value) Then
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.Unit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphAxis.AxisTypeID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AxisTypeID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(GraphAxisMetadata.ColumnNames.AxisTypeID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(GraphAxisMetadata.ColumnNames.AxisTypeID, value) Then
					Me._UpToAxisTypeByAxisTypeID = Nothing
					Me.OnPropertyChanged("UpToAxisTypeByAxisTypeID")
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.AxisTypeID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphAxis.AxisIndex
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AxisIndex As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(GraphAxisMetadata.ColumnNames.AxisIndex)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(GraphAxisMetadata.ColumnNames.AxisIndex, value) Then
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.AxisIndex)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GraphAxis.Color
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Color As System.String
			Get
				Return MyBase.GetSystemString(GraphAxisMetadata.ColumnNames.Color)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(GraphAxisMetadata.ColumnNames.Color, value) Then
					OnPropertyChanged(GraphAxisMetadata.PropertyNames.Color)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToAxisTypeByAxisTypeID As AxisType
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToEngineGraphPresetByGid As EngineGraphPreset
		
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
												
						Case "Gid"
							Me.str().Gid = CType(value, string)
												
						Case "AxisTitle"
							Me.str().AxisTitle = CType(value, string)
												
						Case "ScaleMin"
							Me.str().ScaleMin = CType(value, string)
												
						Case "ScaleMinAuto"
							Me.str().ScaleMinAuto = CType(value, string)
												
						Case "ScaleMax"
							Me.str().ScaleMax = CType(value, string)
												
						Case "ScaleMaxAuto"
							Me.str().ScaleMaxAuto = CType(value, string)
												
						Case "Unit"
							Me.str().Unit = CType(value, string)
												
						Case "AxisTypeID"
							Me.str().AxisTypeID = CType(value, string)
												
						Case "AxisIndex"
							Me.str().AxisIndex = CType(value, string)
												
						Case "Color"
							Me.str().Color = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(GraphAxisMetadata.PropertyNames.Id)
							End If
						
						Case "Gid"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Gid = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(GraphAxisMetadata.PropertyNames.Gid)
							End If
						
						Case "ScaleMin"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ScaleMin = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(GraphAxisMetadata.PropertyNames.ScaleMin)
							End If
						
						Case "ScaleMinAuto"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ScaleMinAuto = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(GraphAxisMetadata.PropertyNames.ScaleMinAuto)
							End If
						
						Case "ScaleMax"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ScaleMax = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(GraphAxisMetadata.PropertyNames.ScaleMax)
							End If
						
						Case "ScaleMaxAuto"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ScaleMaxAuto = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(GraphAxisMetadata.PropertyNames.ScaleMaxAuto)
							End If
						
						Case "AxisTypeID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.AxisTypeID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(GraphAxisMetadata.PropertyNames.AxisTypeID)
							End If
						
						Case "AxisIndex"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.AxisIndex = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(GraphAxisMetadata.PropertyNames.AxisIndex)
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
		
			Public Sub New(ByVal entity As esGraphAxis)
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
		  	
			Public Property Gid As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Gid
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Gid = Nothing
					Else
						entity.Gid = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property AxisTitle As System.String 
				Get
					Dim data_ As System.String = entity.AxisTitle
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AxisTitle = Nothing
					Else
						entity.AxisTitle = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ScaleMin As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.ScaleMin
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ScaleMin = Nothing
					Else
						entity.ScaleMin = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property ScaleMinAuto As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.ScaleMinAuto
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ScaleMinAuto = Nothing
					Else
						entity.ScaleMinAuto = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property ScaleMax As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.ScaleMax
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ScaleMax = Nothing
					Else
						entity.ScaleMax = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property ScaleMaxAuto As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.ScaleMaxAuto
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ScaleMaxAuto = Nothing
					Else
						entity.ScaleMaxAuto = Convert.ToBoolean(Value)
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
		  	
			Public Property AxisTypeID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.AxisTypeID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AxisTypeID = Nothing
					Else
						entity.AxisTypeID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property AxisIndex As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.AxisIndex
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AxisIndex = Nothing
					Else
						entity.AxisIndex = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Color As System.String 
				Get
					Dim data_ As System.String = entity.Color
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Color = Nothing
					Else
						entity.Color = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esGraphAxis
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return GraphAxisMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As GraphAxisQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New GraphAxisQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As GraphAxisQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As GraphAxisQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As GraphAxisQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esGraphAxisCollection
		Inherits esEntityCollection(Of GraphAxis)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return GraphAxisMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "GraphAxisCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As GraphAxisQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New GraphAxisQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As GraphAxisQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New GraphAxisQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As GraphAxisQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, GraphAxisQuery))
		End Sub
		
		#End Region
						
		Private m_query As GraphAxisQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esGraphAxisQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return GraphAxisMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "Gid" 
					Return Me.Gid
				Case "AxisTitle" 
					Return Me.AxisTitle
				Case "ScaleMin" 
					Return Me.ScaleMin
				Case "ScaleMinAuto" 
					Return Me.ScaleMinAuto
				Case "ScaleMax" 
					Return Me.ScaleMax
				Case "ScaleMaxAuto" 
					Return Me.ScaleMaxAuto
				Case "Unit" 
					Return Me.Unit
				Case "AxisTypeID" 
					Return Me.AxisTypeID
				Case "AxisIndex" 
					Return Me.AxisIndex
				Case "Color" 
					Return Me.Color
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Gid As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.Gid, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property AxisTitle As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.AxisTitle, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ScaleMin As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.ScaleMin, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ScaleMinAuto As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.ScaleMinAuto, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property ScaleMax As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.ScaleMax, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ScaleMaxAuto As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.ScaleMaxAuto, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Unit As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.Unit, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property AxisTypeID As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.AxisTypeID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property AxisIndex As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.AxisIndex, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Color As esQueryItem
			Get
				Return New esQueryItem(Me, GraphAxisMetadata.ColumnNames.Color, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class GraphAxis 
		Inherits esGraphAxis
		
	
		#Region "GraphParameterCollectionByAxisID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_GraphParameterCollectionByAxisID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.GraphAxis.GraphParameterCollectionByAxisID_Delegate)
				map.PropertyName = "GraphParameterCollectionByAxisID"
				map.MyColumnName = "AxisID"
				map.ParentColumnName = "ID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub GraphParameterCollectionByAxisID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New GraphAxisQuery(data.NextAlias())
			
			Dim mee As GraphParameterQuery = If(data.You IsNot Nothing, TryCast(data.You, GraphParameterQuery), New GraphParameterQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.Id = mee.AxisID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - GraphAxisGraphParameter
		''' </summary>

		<XmlIgnore()> _ 
		Public Property GraphParameterCollectionByAxisID As GraphParameterCollection 
		
			Get
				If Me._GraphParameterCollectionByAxisID Is Nothing Then
					Me._GraphParameterCollectionByAxisID = New GraphParameterCollection()
					Me._GraphParameterCollectionByAxisID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("GraphParameterCollectionByAxisID", Me._GraphParameterCollectionByAxisID)
				
					If Not Me.Id.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._GraphParameterCollectionByAxisID.Query.Where(Me._GraphParameterCollectionByAxisID.Query.AxisID = Me.Id)
							Me._GraphParameterCollectionByAxisID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._GraphParameterCollectionByAxisID.fks.Add(GraphParameterMetadata.ColumnNames.AxisID, Me.Id)
					End If
				End If

				Return Me._GraphParameterCollectionByAxisID
			End Get
			
			Set(ByVal value As GraphParameterCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._GraphParameterCollectionByAxisID Is Nothing Then

					Me.RemovePostSave("GraphParameterCollectionByAxisID")
					Me._GraphParameterCollectionByAxisID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _GraphParameterCollectionByAxisID As GraphParameterCollection
		#End Region

		#Region "UpToAxisTypeByAxisTypeID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - AxisTypeGraphAxis
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToAxisTypeByAxisTypeID As AxisType
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToAxisTypeByAxisTypeID Is Nothing _
						 AndAlso Not AxisTypeID.Equals(Nothing)  Then
						
					Me._UpToAxisTypeByAxisTypeID = New AxisType()
					Me._UpToAxisTypeByAxisTypeID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToAxisTypeByAxisTypeID", Me._UpToAxisTypeByAxisTypeID)
					Me._UpToAxisTypeByAxisTypeID.Query.Where(Me._UpToAxisTypeByAxisTypeID.Query.TypeID = Me.AxisTypeID)
					Me._UpToAxisTypeByAxisTypeID.Query.Load()
				End If

				Return Me._UpToAxisTypeByAxisTypeID
			End Get
			
            Set(ByVal value As AxisType)
				Me.RemovePreSave("UpToAxisTypeByAxisTypeID")
				

				If value Is Nothing Then
				
					Me.AxisTypeID = Nothing
				
					Me._UpToAxisTypeByAxisTypeID = Nothing
				Else
				
					Me.AxisTypeID = value.TypeID
					
					Me._UpToAxisTypeByAxisTypeID = value
					Me.SetPreSave("UpToAxisTypeByAxisTypeID", Me._UpToAxisTypeByAxisTypeID)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToEngineGraphPresetByGid - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - EngineGraphPresetGraphAxis
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToEngineGraphPresetByGid As EngineGraphPreset
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToEngineGraphPresetByGid Is Nothing _
						 AndAlso Not Gid.Equals(Nothing)  Then
						
					Me._UpToEngineGraphPresetByGid = New EngineGraphPreset()
					Me._UpToEngineGraphPresetByGid.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToEngineGraphPresetByGid", Me._UpToEngineGraphPresetByGid)
					Me._UpToEngineGraphPresetByGid.Query.Where(Me._UpToEngineGraphPresetByGid.Query.Id = Me.Gid)
					Me._UpToEngineGraphPresetByGid.Query.Load()
				End If

				Return Me._UpToEngineGraphPresetByGid
			End Get
			
            Set(ByVal value As EngineGraphPreset)
				Me.RemovePreSave("UpToEngineGraphPresetByGid")
				

				If value Is Nothing Then
				
					Me.Gid = Nothing
				
					Me._UpToEngineGraphPresetByGid = Nothing
				Else
				
					Me.Gid = value.Id
					
					Me._UpToEngineGraphPresetByGid = value
					Me.SetPreSave("UpToEngineGraphPresetByGid", Me._UpToEngineGraphPresetByGid)
				End If
				
			End Set	

		End Property
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "GraphParameterCollectionByAxisID"
					coll = Me.GraphParameterCollectionByAxisID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "GraphParameterCollectionByAxisID", GetType(GraphParameterCollection), New GraphParameter()))
			Return props
			
		End Function	
		''' <summary>
		''' Used internally for retrieving AutoIncrementing keys
		''' during hierarchical PreSave.
		''' </summary>
		Protected Overrides Sub ApplyPreSaveKeys()
		
			If Not Me.es.IsDeleted And Not Me._UpToAxisTypeByAxisTypeID Is Nothing Then
				Me.AxisTypeID = Me._UpToAxisTypeByAxisTypeID.TypeID
			End If
			
			If Not Me.es.IsDeleted And Not Me._UpToEngineGraphPresetByGid Is Nothing Then
				Me.Gid = Me._UpToEngineGraphPresetByGid.Id
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
		
			If Not Me._GraphParameterCollectionByAxisID Is Nothing Then
				Apply(Me._GraphParameterCollectionByAxisID, "AxisID", Me.Id)
			End If
			
		End Sub
	End Class
		



	<Serializable> _
	Partial Public Class GraphAxisMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.IsAutoIncrement = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.Gid, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.Gid
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.AxisTitle, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.AxisTitle
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.ScaleMin, 3, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.ScaleMin
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.ScaleMinAuto, 4, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.ScaleMinAuto
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.ScaleMax, 5, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.ScaleMax
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.ScaleMaxAuto, 6, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.ScaleMaxAuto
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.Unit, 7, GetType(System.String), esSystemType.String)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.Unit
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.AxisTypeID, 8, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.AxisTypeID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.AxisIndex, 9, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.AxisIndex
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(GraphAxisMetadata.ColumnNames.Color, 10, GetType(System.String), esSystemType.String)	
			c.PropertyName = GraphAxisMetadata.PropertyNames.Color
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As GraphAxisMetadata
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
			 Public Const Gid As String = "GID"
			 Public Const AxisTitle As String = "AxisTitle"
			 Public Const ScaleMin As String = "ScaleMin"
			 Public Const ScaleMinAuto As String = "ScaleMinAuto"
			 Public Const ScaleMax As String = "ScaleMax"
			 Public Const ScaleMaxAuto As String = "ScaleMaxAuto"
			 Public Const Unit As String = "Unit"
			 Public Const AxisTypeID As String = "AxisTypeID"
			 Public Const AxisIndex As String = "AxisIndex"
			 Public Const Color As String = "Color"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const Gid As String = "Gid"
			 Public Const AxisTitle As String = "AxisTitle"
			 Public Const ScaleMin As String = "ScaleMin"
			 Public Const ScaleMinAuto As String = "ScaleMinAuto"
			 Public Const ScaleMax As String = "ScaleMax"
			 Public Const ScaleMaxAuto As String = "ScaleMaxAuto"
			 Public Const Unit As String = "Unit"
			 Public Const AxisTypeID As String = "AxisTypeID"
			 Public Const AxisIndex As String = "AxisIndex"
			 Public Const Color As String = "Color"
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
			SyncLock GetType(GraphAxisMetadata)
			
				If GraphAxisMetadata.mapDelegates Is Nothing Then
					GraphAxisMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If GraphAxisMetadata._meta Is Nothing Then
					GraphAxisMetadata._meta = New GraphAxisMetadata()
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
				meta.AddTypeMap("Gid", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("AxisTitle", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ScaleMin", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ScaleMinAuto", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("ScaleMax", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ScaleMaxAuto", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Unit", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("AxisTypeID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("AxisIndex", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Color", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "GraphAxis"
				meta.Destination = "GraphAxis"
				
				meta.spInsert = "proc_GraphAxisInsert"
				meta.spUpdate = "proc_GraphAxisUpdate"
				meta.spDelete = "proc_GraphAxisDelete"
				meta.spLoadAll = "proc_GraphAxisLoadAll"
				meta.spLoadByPrimaryKey = "proc_GraphAxisLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As GraphAxisMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

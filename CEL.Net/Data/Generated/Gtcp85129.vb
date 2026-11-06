
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 5/11/2015 8:45:56 AM
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
	' Encapsulates the 'GTCP85_129' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(Gtcp85129))> _
	<XmlType("Gtcp85129")> _	
	Partial Public Class Gtcp85129 
		Inherits esGtcp85129
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New Gtcp85129()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal engineSeries As System.String, ByVal repairTypeID As System.Int16, ByVal shaftLoad As System.Int16, ByVal tcl As System.Double)
			Dim obj As New Gtcp85129()
			obj.EngineSeries = engineSeries
			obj.RepairTypeID = repairTypeID
			obj.ShaftLoad = shaftLoad
			obj.Tcl = tcl
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal engineSeries As System.String, ByVal repairTypeID As System.Int16, ByVal shaftLoad As System.Int16, ByVal tcl As System.Double, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New Gtcp85129()
			obj.EngineSeries = engineSeries
			obj.RepairTypeID = repairTypeID
			obj.ShaftLoad = shaftLoad
			obj.Tcl = tcl
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("Gtcp85129Collection")> _
	Partial Public Class Gtcp85129Collection
		Inherits esGtcp85129Collection
		Implements IEnumerable(Of Gtcp85129)
	
		Public Function FindByPrimaryKey(ByVal engineSeries As System.String, ByVal repairTypeID As System.Int16, ByVal shaftLoad As System.Int16, ByVal tcl As System.Double) As Gtcp85129
			Return MyBase.SingleOrDefault(Function(e) e.EngineSeries = engineSeries And e.RepairTypeID.HasValue AndAlso e.RepairTypeID.Value = repairTypeID And e.ShaftLoad.HasValue AndAlso e.ShaftLoad.Value = shaftLoad And e.Tcl.HasValue AndAlso e.Tcl.Value = tcl)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(Gtcp85129))> _
		Public Class Gtcp85129CollectionWCFPacket
			Inherits esCollectionWCFPacket(Of Gtcp85129Collection)
			
			Public Shared Widening Operator CType(packet As Gtcp85129CollectionWCFPacket) As Gtcp85129Collection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As Gtcp85129Collection) As Gtcp85129CollectionWCFPacket
				Return New Gtcp85129CollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class Gtcp85129Query 
		Inherits esGtcp85129Query
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "Gtcp85129Query"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As Gtcp85129Query) As String
			Return Gtcp85129Query.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As Gtcp85129Query
			Return DirectCast(Gtcp85129Query.SerializeHelper.FromXml(query, GetType(Gtcp85129Query)), Gtcp85129Query)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esGtcp85129
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal engineSeries As System.String, ByVal repairTypeID As System.Int16, ByVal shaftLoad As System.Int16, ByVal tcl As System.Double) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineSeries, repairTypeID, shaftLoad, tcl)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineSeries, repairTypeID, shaftLoad, tcl)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal engineSeries As System.String, ByVal repairTypeID As System.Int16, ByVal shaftLoad As System.Int16, ByVal tcl As System.Double) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineSeries, repairTypeID, shaftLoad, tcl)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineSeries, repairTypeID, shaftLoad, tcl)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal engineSeries As System.String, ByVal repairTypeID As System.Int16, ByVal shaftLoad As System.Int16, ByVal tcl As System.Double) As Boolean
		
			Dim query As New Gtcp85129Query()
			query.Where(query.EngineSeries = engineSeries And query.RepairTypeID = repairTypeID And query.ShaftLoad = shaftLoad And query.Tcl = tcl)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal engineSeries As System.String, ByVal repairTypeID As System.Int16, ByVal shaftLoad As System.Int16, ByVal tcl As System.Double) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("EngineSeries", engineSeries)
						parms.Add("RepairTypeID", repairTypeID)
						parms.Add("ShaftLoad", shaftLoad)
						parms.Add("Tcl", tcl)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to GTCP85_129.EngineSeries
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineSeries As System.String
			Get
				Return MyBase.GetSystemString(Gtcp85129Metadata.ColumnNames.EngineSeries)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Gtcp85129Metadata.ColumnNames.EngineSeries, value) Then
					Me._UpToEngineSeriesByEngineSeries = Nothing
					Me.OnPropertyChanged("UpToEngineSeriesByEngineSeries")
					OnPropertyChanged(Gtcp85129Metadata.PropertyNames.EngineSeries)
				End If
			End Set
		End Property	
			
		' <summary>
		' Lookup Table on GTCP85_129_RepairType
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RepairTypeID As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(Gtcp85129Metadata.ColumnNames.RepairTypeID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(Gtcp85129Metadata.ColumnNames.RepairTypeID, value) Then
					Me._UpToGTCP85129RepairTypeByRepairTypeID = Nothing
					Me.OnPropertyChanged("UpToGTCP85129RepairTypeByRepairTypeID")
					OnPropertyChanged(Gtcp85129Metadata.PropertyNames.RepairTypeID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GTCP85_129.ShaftLoad
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ShaftLoad As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(Gtcp85129Metadata.ColumnNames.ShaftLoad)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(Gtcp85129Metadata.ColumnNames.ShaftLoad, value) Then
					OnPropertyChanged(Gtcp85129Metadata.PropertyNames.ShaftLoad)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GTCP85_129.Tcl
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Tcl As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(Gtcp85129Metadata.ColumnNames.Tcl)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(Gtcp85129Metadata.ColumnNames.Tcl, value) Then
					OnPropertyChanged(Gtcp85129Metadata.PropertyNames.Tcl)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GTCP85_129.WB_Chng
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property WBChng As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(Gtcp85129Metadata.ColumnNames.WBChng)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(Gtcp85129Metadata.ColumnNames.WBChng, value) Then
					OnPropertyChanged(Gtcp85129Metadata.PropertyNames.WBChng)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GTCP85_129.PB_Chng
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PBChng As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(Gtcp85129Metadata.ColumnNames.PBChng)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(Gtcp85129Metadata.ColumnNames.PBChng, value) Then
					OnPropertyChanged(Gtcp85129Metadata.PropertyNames.PBChng)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to GTCP85_129.TB_Chng
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TBChng As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(Gtcp85129Metadata.ColumnNames.TBChng)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(Gtcp85129Metadata.ColumnNames.TBChng, value) Then
					OnPropertyChanged(Gtcp85129Metadata.PropertyNames.TBChng)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToEngineSeriesByEngineSeries As EngineSeries
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToGTCP85129RepairTypeByRepairTypeID As GTCP85129RepairType
		
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
												
						Case "EngineSeries"
							Me.str().EngineSeries = CType(value, string)
												
						Case "RepairTypeID"
							Me.str().RepairTypeID = CType(value, string)
												
						Case "ShaftLoad"
							Me.str().ShaftLoad = CType(value, string)
												
						Case "Tcl"
							Me.str().Tcl = CType(value, string)
												
						Case "WBChng"
							Me.str().WBChng = CType(value, string)
												
						Case "PBChng"
							Me.str().PBChng = CType(value, string)
												
						Case "TBChng"
							Me.str().TBChng = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "RepairTypeID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.RepairTypeID = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(Gtcp85129Metadata.PropertyNames.RepairTypeID)
							End If
						
						Case "ShaftLoad"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.ShaftLoad = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(Gtcp85129Metadata.PropertyNames.ShaftLoad)
							End If
						
						Case "Tcl"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.Tcl = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(Gtcp85129Metadata.PropertyNames.Tcl)
							End If
						
						Case "WBChng"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.WBChng = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(Gtcp85129Metadata.PropertyNames.WBChng)
							End If
						
						Case "PBChng"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.PBChng = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(Gtcp85129Metadata.PropertyNames.PBChng)
							End If
						
						Case "TBChng"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.TBChng = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(Gtcp85129Metadata.PropertyNames.TBChng)
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
		
			Public Sub New(ByVal entity As esGtcp85129)
				Me.entity = entity
			End Sub				
		
	
			Public Property EngineSeries As System.String 
				Get
					Dim data_ As System.String = entity.EngineSeries
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EngineSeries = Nothing
					Else
						entity.EngineSeries = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property RepairTypeID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.RepairTypeID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RepairTypeID = Nothing
					Else
						entity.RepairTypeID = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property ShaftLoad As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.ShaftLoad
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ShaftLoad = Nothing
					Else
						entity.ShaftLoad = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property Tcl As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.Tcl
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Tcl = Nothing
					Else
						entity.Tcl = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property WBChng As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.WBChng
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.WBChng = Nothing
					Else
						entity.WBChng = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property PBChng As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.PBChng
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PBChng = Nothing
					Else
						entity.PBChng = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property TBChng As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.TBChng
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TBChng = Nothing
					Else
						entity.TBChng = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  

			Private entity As esGtcp85129
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Gtcp85129Metadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As Gtcp85129Query
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Gtcp85129Query()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As Gtcp85129Query) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As Gtcp85129Query)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As Gtcp85129Query

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esGtcp85129Collection
		Inherits esEntityCollection(Of Gtcp85129)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Gtcp85129Metadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "Gtcp85129Collection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As Gtcp85129Query
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Gtcp85129Query()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As Gtcp85129Query) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New Gtcp85129Query()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As Gtcp85129Query)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, Gtcp85129Query))
		End Sub
		
		#End Region
						
		Private m_query As Gtcp85129Query
	End Class



	<Serializable> _
	MustInherit Public Partial Class esGtcp85129Query 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return Gtcp85129Metadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "EngineSeries" 
					Return Me.EngineSeries
				Case "RepairTypeID" 
					Return Me.RepairTypeID
				Case "ShaftLoad" 
					Return Me.ShaftLoad
				Case "Tcl" 
					Return Me.Tcl
				Case "WBChng" 
					Return Me.WBChng
				Case "PBChng" 
					Return Me.PBChng
				Case "TBChng" 
					Return Me.TBChng
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property EngineSeries As esQueryItem
			Get
				Return New esQueryItem(Me, Gtcp85129Metadata.ColumnNames.EngineSeries, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property RepairTypeID As esQueryItem
			Get
				Return New esQueryItem(Me, Gtcp85129Metadata.ColumnNames.RepairTypeID, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property ShaftLoad As esQueryItem
			Get
				Return New esQueryItem(Me, Gtcp85129Metadata.ColumnNames.ShaftLoad, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property Tcl As esQueryItem
			Get
				Return New esQueryItem(Me, Gtcp85129Metadata.ColumnNames.Tcl, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property WBChng As esQueryItem
			Get
				Return New esQueryItem(Me, Gtcp85129Metadata.ColumnNames.WBChng, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property PBChng As esQueryItem
			Get
				Return New esQueryItem(Me, Gtcp85129Metadata.ColumnNames.PBChng, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property TBChng As esQueryItem
			Get
				Return New esQueryItem(Me, Gtcp85129Metadata.ColumnNames.TBChng, esSystemType.Double)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class Gtcp85129 
		Inherits esGtcp85129
		
	
		#Region "UpToEngineSeriesByEngineSeries - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Engine_SeriesGTCP85_129
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToEngineSeriesByEngineSeries As EngineSeries
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToEngineSeriesByEngineSeries Is Nothing _
						 AndAlso Not EngineSeries.Equals(Nothing)  Then
						
					Me._UpToEngineSeriesByEngineSeries = New EngineSeries()
					Me._UpToEngineSeriesByEngineSeries.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToEngineSeriesByEngineSeries", Me._UpToEngineSeriesByEngineSeries)
					Me._UpToEngineSeriesByEngineSeries.Query.Where(Me._UpToEngineSeriesByEngineSeries.Query.EngineSeries = Me.EngineSeries)
					Me._UpToEngineSeriesByEngineSeries.Query.Load()
				End If

				Return Me._UpToEngineSeriesByEngineSeries
			End Get
			
            Set(ByVal value As EngineSeries)
				Me.RemovePreSave("UpToEngineSeriesByEngineSeries")
				

				If value Is Nothing Then
				
					Me.EngineSeries = Nothing
				
					Me._UpToEngineSeriesByEngineSeries = Nothing
				Else
				
					Me.EngineSeries = value.EngineSeries
					
					Me._UpToEngineSeriesByEngineSeries = value
					Me.SetPreSave("UpToEngineSeriesByEngineSeries", Me._UpToEngineSeriesByEngineSeries)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToGTCP85129RepairTypeByRepairTypeID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - GTCP85_129_RepairTypeGTCP85_129
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToGTCP85129RepairTypeByRepairTypeID As GTCP85129RepairType
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToGTCP85129RepairTypeByRepairTypeID Is Nothing _
						 AndAlso Not RepairTypeID.Equals(Nothing)  Then
						
					Me._UpToGTCP85129RepairTypeByRepairTypeID = New GTCP85129RepairType()
					Me._UpToGTCP85129RepairTypeByRepairTypeID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToGTCP85129RepairTypeByRepairTypeID", Me._UpToGTCP85129RepairTypeByRepairTypeID)
					Me._UpToGTCP85129RepairTypeByRepairTypeID.Query.Where(Me._UpToGTCP85129RepairTypeByRepairTypeID.Query.Id = Me.RepairTypeID)
					Me._UpToGTCP85129RepairTypeByRepairTypeID.Query.Load()
				End If

				Return Me._UpToGTCP85129RepairTypeByRepairTypeID
			End Get
			
            Set(ByVal value As GTCP85129RepairType)
				Me.RemovePreSave("UpToGTCP85129RepairTypeByRepairTypeID")
				

				If value Is Nothing Then
				
					Me.RepairTypeID = Nothing
				
					Me._UpToGTCP85129RepairTypeByRepairTypeID = Nothing
				Else
				
					Me.RepairTypeID = value.Id
					
					Me._UpToGTCP85129RepairTypeByRepairTypeID = value
					Me.SetPreSave("UpToGTCP85129RepairTypeByRepairTypeID", Me._UpToGTCP85129RepairTypeByRepairTypeID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class Gtcp85129Metadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(Gtcp85129Metadata.ColumnNames.EngineSeries, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = Gtcp85129Metadata.PropertyNames.EngineSeries
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			m_columns.Add(c)
				
			c = New esColumnMetadata(Gtcp85129Metadata.ColumnNames.RepairTypeID, 1, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = Gtcp85129Metadata.PropertyNames.RepairTypeID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 5
			c.Description = "Lookup Table on GTCP85_129_RepairType"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Gtcp85129Metadata.ColumnNames.ShaftLoad, 2, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = Gtcp85129Metadata.PropertyNames.ShaftLoad
			c.IsInPrimaryKey = True
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Gtcp85129Metadata.ColumnNames.Tcl, 3, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = Gtcp85129Metadata.PropertyNames.Tcl
			c.IsInPrimaryKey = True
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Gtcp85129Metadata.ColumnNames.WBChng, 4, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = Gtcp85129Metadata.PropertyNames.WBChng
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Gtcp85129Metadata.ColumnNames.PBChng, 5, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = Gtcp85129Metadata.PropertyNames.PBChng
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Gtcp85129Metadata.ColumnNames.TBChng, 6, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = Gtcp85129Metadata.PropertyNames.TBChng
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As Gtcp85129Metadata
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
			 Public Const EngineSeries As String = "EngineSeries"
			 Public Const RepairTypeID As String = "RepairTypeID"
			 Public Const ShaftLoad As String = "ShaftLoad"
			 Public Const Tcl As String = "Tcl"
			 Public Const WBChng As String = "WB_Chng"
			 Public Const PBChng As String = "PB_Chng"
			 Public Const TBChng As String = "TB_Chng"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const EngineSeries As String = "EngineSeries"
			 Public Const RepairTypeID As String = "RepairTypeID"
			 Public Const ShaftLoad As String = "ShaftLoad"
			 Public Const Tcl As String = "Tcl"
			 Public Const WBChng As String = "WBChng"
			 Public Const PBChng As String = "PBChng"
			 Public Const TBChng As String = "TBChng"
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
			SyncLock GetType(Gtcp85129Metadata)
			
				If Gtcp85129Metadata.mapDelegates Is Nothing Then
					Gtcp85129Metadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If Gtcp85129Metadata._meta Is Nothing Then
					Gtcp85129Metadata._meta = New Gtcp85129Metadata()
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
				


				meta.AddTypeMap("EngineSeries", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("RepairTypeID", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("ShaftLoad", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("Tcl", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("WBChng", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("PBChng", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("TBChng", new esTypeMap("Double", "System.Double"))			
				
				
				 
				meta.Source = "GTCP85_129"
				meta.Destination = "GTCP85_129"
				
				meta.spInsert = "proc_GTCP85_129Insert"
				meta.spUpdate = "proc_GTCP85_129Update"
				meta.spDelete = "proc_GTCP85_129Delete"
				meta.spLoadAll = "proc_GTCP85_129LoadAll"
				meta.spLoadByPrimaryKey = "proc_GTCP85_129LoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As Gtcp85129Metadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

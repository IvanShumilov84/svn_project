
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 19/11/2013 2:05:24 PM
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
	' Encapsulates the 'UUT_Limit' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTLimit))> _
	<XmlType("UUTLimit")> _	
	Partial Public Class UUTLimit 
		Inherits esUUTLimit
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTLimit()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal limitID As System.Int32)
			Dim obj As New UUTLimit()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.LimitID = limitID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal limitID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTLimit()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.LimitID = limitID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTLimitCollection")> _
	Partial Public Class UUTLimitCollection
		Inherits esUUTLimitCollection
		Implements IEnumerable(Of UUTLimit)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal limitID As System.Int32) As UUTLimit
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.LimitID.HasValue AndAlso e.LimitID.Value = limitID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTLimit))> _
		Public Class UUTLimitCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTLimitCollection)
			
			Public Shared Widening Operator CType(packet As UUTLimitCollectionWCFPacket) As UUTLimitCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTLimitCollection) As UUTLimitCollectionWCFPacket
				Return New UUTLimitCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTLimitQuery 
		Inherits esUUTLimitQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTLimitQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTLimitQuery) As String
			Return UUTLimitQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTLimitQuery
			Return DirectCast(UUTLimitQuery.SerializeHelper.FromXml(query, GetType(UUTLimitQuery)), UUTLimitQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTLimit
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal limitID As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, limitID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, limitID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal limitID As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, limitID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, limitID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal limitID As System.Int32) As Boolean
		
			Dim query As New UUTLimitQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.LimitID = limitID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal limitID As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
						parms.Add("LimitID", limitID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_Limit.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTLimitMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTLimitMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTLimitMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Limit.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UUTLimitMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTLimitMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTLimitMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Limit.LimitID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LimitID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTLimitMetadata.ColumnNames.LimitID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTLimitMetadata.ColumnNames.LimitID, value) Then
					OnPropertyChanged(UUTLimitMetadata.PropertyNames.LimitID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Limit.Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Name As System.String
			Get
				Return MyBase.GetSystemString(UUTLimitMetadata.ColumnNames.Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTLimitMetadata.ColumnNames.Name, value) Then
					OnPropertyChanged(UUTLimitMetadata.PropertyNames.Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Limit.LowLow_Value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLowValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UUTLimitMetadata.ColumnNames.LowLowValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UUTLimitMetadata.ColumnNames.LowLowValue, value) Then
					OnPropertyChanged(UUTLimitMetadata.PropertyNames.LowLowValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Limit.Low_Value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UUTLimitMetadata.ColumnNames.LowValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UUTLimitMetadata.ColumnNames.LowValue, value) Then
					OnPropertyChanged(UUTLimitMetadata.PropertyNames.LowValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Limit.High_Value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UUTLimitMetadata.ColumnNames.HighValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UUTLimitMetadata.ColumnNames.HighValue, value) Then
					OnPropertyChanged(UUTLimitMetadata.PropertyNames.HighValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Limit.HighHigh_Value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighHighValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UUTLimitMetadata.ColumnNames.HighHighValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UUTLimitMetadata.ColumnNames.HighHighValue, value) Then
					OnPropertyChanged(UUTLimitMetadata.PropertyNames.HighHighValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Limit.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTLimitMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTLimitMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(UUTLimitMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToUutByProjectID As Uut
		
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
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
						Case "LimitID"
							Me.str().LimitID = CType(value, string)
												
						Case "Name"
							Me.str().Name = CType(value, string)
												
						Case "LowLowValue"
							Me.str().LowLowValue = CType(value, string)
												
						Case "LowValue"
							Me.str().LowValue = CType(value, string)
												
						Case "HighValue"
							Me.str().HighValue = CType(value, string)
												
						Case "HighHighValue"
							Me.str().HighHighValue = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTLimitMetadata.PropertyNames.ProjectID)
							End If
						
						Case "LimitID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LimitID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTLimitMetadata.PropertyNames.LimitID)
							End If
						
						Case "LowLowValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.LowLowValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UUTLimitMetadata.PropertyNames.LowLowValue)
							End If
						
						Case "LowValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.LowValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UUTLimitMetadata.PropertyNames.LowValue)
							End If
						
						Case "HighValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.HighValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UUTLimitMetadata.PropertyNames.HighValue)
							End If
						
						Case "HighHighValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.HighHighValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UUTLimitMetadata.PropertyNames.HighHighValue)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTLimitMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esUUTLimit)
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
		  	
			Public Property LimitID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.LimitID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LimitID = Nothing
					Else
						entity.LimitID = Convert.ToInt32(Value)
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
		  	
			Public Property LowLowValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.LowLowValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLowValue = Nothing
					Else
						entity.LowLowValue = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.LowValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowValue = Nothing
					Else
						entity.LowValue = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.HighValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighValue = Nothing
					Else
						entity.HighValue = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighHighValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.HighHighValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighHighValue = Nothing
					Else
						entity.HighHighValue = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property Unavailable As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Unavailable
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Unavailable = Nothing
					Else
						entity.Unavailable = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  

			Private entity As esUUTLimit
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTLimitMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTLimitQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTLimitQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTLimitQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTLimitQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTLimitQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTLimitCollection
		Inherits esEntityCollection(Of UUTLimit)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTLimitMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTLimitCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTLimitQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTLimitQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTLimitQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTLimitQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTLimitQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTLimitQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTLimitQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTLimitQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTLimitMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "LimitID" 
					Return Me.LimitID
				Case "Name" 
					Return Me.Name
				Case "LowLowValue" 
					Return Me.LowLowValue
				Case "LowValue" 
					Return Me.LowValue
				Case "HighValue" 
					Return Me.HighValue
				Case "HighHighValue" 
					Return Me.HighHighValue
				Case "Unavailable" 
					Return Me.Unavailable
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTLimitMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UUTLimitMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LimitID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTLimitMetadata.ColumnNames.LimitID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Name As esQueryItem
			Get
				Return New esQueryItem(Me, UUTLimitMetadata.ColumnNames.Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LowLowValue As esQueryItem
			Get
				Return New esQueryItem(Me, UUTLimitMetadata.ColumnNames.LowLowValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property LowValue As esQueryItem
			Get
				Return New esQueryItem(Me, UUTLimitMetadata.ColumnNames.LowValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property HighValue As esQueryItem
			Get
				Return New esQueryItem(Me, UUTLimitMetadata.ColumnNames.HighValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property HighHighValue As esQueryItem
			Get
				Return New esQueryItem(Me, UUTLimitMetadata.ColumnNames.HighHighValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, UUTLimitMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTLimit 
		Inherits esUUTLimit
		
	
		#Region "UpToUutByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUTUUT_Limit
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToUutByProjectID As Uut
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToUutByProjectID Is Nothing _
						 AndAlso Not ProjectID.Equals(Nothing)  AndAlso Not EngineModel.Equals(Nothing)  Then
						
					Me._UpToUutByProjectID = New Uut()
					Me._UpToUutByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToUutByProjectID", Me._UpToUutByProjectID)
					Me._UpToUutByProjectID.Query.Where(Me._UpToUutByProjectID.Query.ProjectID = Me.ProjectID)
					Me._UpToUutByProjectID.Query.Where(Me._UpToUutByProjectID.Query.EngineModel = Me.EngineModel)
					Me._UpToUutByProjectID.Query.Load()
				End If

				Return Me._UpToUutByProjectID
			End Get
			
            Set(ByVal value As Uut)
				Me.RemovePreSave("UpToUutByProjectID")
				

				If value Is Nothing Then
				
					Me.ProjectID = Nothing
				
					Me.EngineModel = Nothing
				
					Me._UpToUutByProjectID = Nothing
				Else
				
					Me.ProjectID = value.ProjectID
					
					Me.EngineModel = value.EngineModel
					
					Me._UpToUutByProjectID = value
					Me.SetPreSave("UpToUutByProjectID", Me._UpToUutByProjectID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class UUTLimitMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTLimitMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTLimitMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTLimitMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTLimitMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTLimitMetadata.ColumnNames.LimitID, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTLimitMetadata.PropertyNames.LimitID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTLimitMetadata.ColumnNames.Name, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTLimitMetadata.PropertyNames.Name
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTLimitMetadata.ColumnNames.LowLowValue, 4, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UUTLimitMetadata.PropertyNames.LowLowValue
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTLimitMetadata.ColumnNames.LowValue, 5, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UUTLimitMetadata.PropertyNames.LowValue
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTLimitMetadata.ColumnNames.HighValue, 6, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UUTLimitMetadata.PropertyNames.HighValue
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTLimitMetadata.ColumnNames.HighHighValue, 7, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UUTLimitMetadata.PropertyNames.HighHighValue
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTLimitMetadata.ColumnNames.Unavailable, 8, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTLimitMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTLimitMetadata
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
			 Public Const EngineModel As String = "EngineModel"
			 Public Const LimitID As String = "LimitID"
			 Public Const Name As String = "Name"
			 Public Const LowLowValue As String = "LowLow_Value"
			 Public Const LowValue As String = "Low_Value"
			 Public Const HighValue As String = "High_Value"
			 Public Const HighHighValue As String = "HighHigh_Value"
			 Public Const Unavailable As String = "Unavailable"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const LimitID As String = "LimitID"
			 Public Const Name As String = "Name"
			 Public Const LowLowValue As String = "LowLowValue"
			 Public Const LowValue As String = "LowValue"
			 Public Const HighValue As String = "HighValue"
			 Public Const HighHighValue As String = "HighHighValue"
			 Public Const Unavailable As String = "Unavailable"
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
			SyncLock GetType(UUTLimitMetadata)
			
				If UUTLimitMetadata.mapDelegates Is Nothing Then
					UUTLimitMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTLimitMetadata._meta Is Nothing Then
					UUTLimitMetadata._meta = New UUTLimitMetadata()
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
				meta.AddTypeMap("EngineModel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LimitID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LowLowValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("LowValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("HighValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("HighHighValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "UUT_Limit"
				meta.Destination = "UUT_Limit"
				
				meta.spInsert = "proc_UUT_LimitInsert"
				meta.spUpdate = "proc_UUT_LimitUpdate"
				meta.spDelete = "proc_UUT_LimitDelete"
				meta.spLoadAll = "proc_UUT_LimitLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_LimitLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTLimitMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace


'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 2015-09-25 3:32:49 PM
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
	' Encapsulates the 'UUT_Trip' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTTrip))> _
	<XmlType("UUTTrip")> _	
	Partial Public Class UUTTrip 
		Inherits esUUTTrip
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTTrip()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal tripID As System.Int32, ByVal tripTypeID As System.Int32)
			Dim obj As New UUTTrip()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.TripID = tripID
			obj.TripTypeID = tripTypeID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal tripID As System.Int32, ByVal tripTypeID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTTrip()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.TripID = tripID
			obj.TripTypeID = tripTypeID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTTripCollection")> _
	Partial Public Class UUTTripCollection
		Inherits esUUTTripCollection
		Implements IEnumerable(Of UUTTrip)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal tripID As System.Int32, ByVal tripTypeID As System.Int32) As UUTTrip
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.TripID.HasValue AndAlso e.TripID.Value = tripID And e.TripTypeID.HasValue AndAlso e.TripTypeID.Value = tripTypeID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTTrip))> _
		Public Class UUTTripCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTTripCollection)
			
			Public Shared Widening Operator CType(packet As UUTTripCollectionWCFPacket) As UUTTripCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTTripCollection) As UUTTripCollectionWCFPacket
				Return New UUTTripCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTTripQuery 
		Inherits esUUTTripQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTTripQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTTripQuery) As String
			Return UUTTripQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTTripQuery
			Return DirectCast(UUTTripQuery.SerializeHelper.FromXml(query, GetType(UUTTripQuery)), UUTTripQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTTrip
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal tripID As System.Int32, ByVal tripTypeID As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, tripID, tripTypeID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, tripID, tripTypeID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal tripID As System.Int32, ByVal tripTypeID As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, tripID, tripTypeID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, tripID, tripTypeID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal tripID As System.Int32, ByVal tripTypeID As System.Int32) As Boolean
		
			Dim query As New UUTTripQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.TripID = tripID And query.TripTypeID = tripTypeID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal tripID As System.Int32, ByVal tripTypeID As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
						parms.Add("TripID", tripID)
						parms.Add("TripTypeID", tripTypeID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_Trip.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTTripMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTTripMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTTripMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Trip.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UUTTripMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTTripMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTTripMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Trip.TripID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TripID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTTripMetadata.ColumnNames.TripID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTTripMetadata.ColumnNames.TripID, value) Then
					OnPropertyChanged(UUTTripMetadata.PropertyNames.TripID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Relation avec table UUT_Trip_Type
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TripTypeID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTTripMetadata.ColumnNames.TripTypeID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTTripMetadata.ColumnNames.TripTypeID, value) Then
					Me._UpToUUTTripTypeByTripTypeID = Nothing
					Me.OnPropertyChanged("UpToUUTTripTypeByTripTypeID")
					OnPropertyChanged(UUTTripMetadata.PropertyNames.TripTypeID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Trip.Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Name As System.String
			Get
				Return MyBase.GetSystemString(UUTTripMetadata.ColumnNames.Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTTripMetadata.ColumnNames.Name, value) Then
					OnPropertyChanged(UUTTripMetadata.PropertyNames.Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Trip.Expression
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Expression As System.String
			Get
				Return MyBase.GetSystemString(UUTTripMetadata.ColumnNames.Expression)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTTripMetadata.ColumnNames.Expression, value) Then
					OnPropertyChanged(UUTTripMetadata.PropertyNames.Expression)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Trip.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(UUTTripMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTTripMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(UUTTripMetadata.PropertyNames.Note)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToUUTTripTypeByTripTypeID As UUTTripType
		
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
												
						Case "TripID"
							Me.str().TripID = CType(value, string)
												
						Case "TripTypeID"
							Me.str().TripTypeID = CType(value, string)
												
						Case "Name"
							Me.str().Name = CType(value, string)
												
						Case "Expression"
							Me.str().Expression = CType(value, string)
												
						Case "Note"
							Me.str().Note = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTTripMetadata.PropertyNames.ProjectID)
							End If
						
						Case "TripID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.TripID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTTripMetadata.PropertyNames.TripID)
							End If
						
						Case "TripTypeID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.TripTypeID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTTripMetadata.PropertyNames.TripTypeID)
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
		
			Public Sub New(ByVal entity As esUUTTrip)
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
		  	
			Public Property TripID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.TripID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TripID = Nothing
					Else
						entity.TripID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property TripTypeID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.TripTypeID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TripTypeID = Nothing
					Else
						entity.TripTypeID = Convert.ToInt32(Value)
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
		  	
			Public Property Expression As System.String 
				Get
					Dim data_ As System.String = entity.Expression
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Expression = Nothing
					Else
						entity.Expression = Convert.ToString(Value)
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
		  

			Private entity As esUUTTrip
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTTripMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTTripQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTTripQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTTripQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTTripQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTTripQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTTripCollection
		Inherits esEntityCollection(Of UUTTrip)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTTripMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTTripCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTTripQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTTripQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTTripQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTTripQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTTripQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTTripQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTTripQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTTripQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTTripMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "TripID" 
					Return Me.TripID
				Case "TripTypeID" 
					Return Me.TripTypeID
				Case "Name" 
					Return Me.Name
				Case "Expression" 
					Return Me.Expression
				Case "Note" 
					Return Me.Note
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTTripMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UUTTripMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TripID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTTripMetadata.ColumnNames.TripID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property TripTypeID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTTripMetadata.ColumnNames.TripTypeID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Name As esQueryItem
			Get
				Return New esQueryItem(Me, UUTTripMetadata.ColumnNames.Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Expression As esQueryItem
			Get
				Return New esQueryItem(Me, UUTTripMetadata.ColumnNames.Expression, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, UUTTripMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTTrip 
		Inherits esUUTTrip
		
	
		#Region "UpToUUTTripTypeByTripTypeID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUT_Trip_TypeUUT_Trip
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToUUTTripTypeByTripTypeID As UUTTripType
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToUUTTripTypeByTripTypeID Is Nothing _
						 AndAlso Not TripTypeID.Equals(Nothing)  Then
						
					Me._UpToUUTTripTypeByTripTypeID = New UUTTripType()
					Me._UpToUUTTripTypeByTripTypeID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToUUTTripTypeByTripTypeID", Me._UpToUUTTripTypeByTripTypeID)
					Me._UpToUUTTripTypeByTripTypeID.Query.Where(Me._UpToUUTTripTypeByTripTypeID.Query.TripTypeID = Me.TripTypeID)
					Me._UpToUUTTripTypeByTripTypeID.Query.Load()
				End If

				Return Me._UpToUUTTripTypeByTripTypeID
			End Get
			
            Set(ByVal value As UUTTripType)
				Me.RemovePreSave("UpToUUTTripTypeByTripTypeID")
				

				If value Is Nothing Then
				
					Me.TripTypeID = Nothing
				
					Me._UpToUUTTripTypeByTripTypeID = Nothing
				Else
				
					Me.TripTypeID = value.TripTypeID
					
					Me._UpToUUTTripTypeByTripTypeID = value
					Me.SetPreSave("UpToUUTTripTypeByTripTypeID", Me._UpToUUTTripTypeByTripTypeID)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToUutByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUTUUT_Trip
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
	Partial Public Class UUTTripMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTTripMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTTripMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTTripMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTTripMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTTripMetadata.ColumnNames.TripID, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTTripMetadata.PropertyNames.TripID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTTripMetadata.ColumnNames.TripTypeID, 3, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTTripMetadata.PropertyNames.TripTypeID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.Description = "Relation avec table UUT_Trip_Type"
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTTripMetadata.ColumnNames.Name, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTTripMetadata.PropertyNames.Name
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTTripMetadata.ColumnNames.Expression, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTTripMetadata.PropertyNames.Expression
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTTripMetadata.ColumnNames.Note, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTTripMetadata.PropertyNames.Note
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTTripMetadata
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
			 Public Const TripID As String = "TripID"
			 Public Const TripTypeID As String = "TripTypeID"
			 Public Const Name As String = "Name"
			 Public Const Expression As String = "Expression"
			 Public Const Note As String = "Note"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const TripID As String = "TripID"
			 Public Const TripTypeID As String = "TripTypeID"
			 Public Const Name As String = "Name"
			 Public Const Expression As String = "Expression"
			 Public Const Note As String = "Note"
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
			SyncLock GetType(UUTTripMetadata)
			
				If UUTTripMetadata.mapDelegates Is Nothing Then
					UUTTripMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTTripMetadata._meta Is Nothing Then
					UUTTripMetadata._meta = New UUTTripMetadata()
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
				meta.AddTypeMap("TripID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("TripTypeID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Expression", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("Note", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "UUT_Trip"
				meta.Destination = "UUT_Trip"
				
				meta.spInsert = "proc_UUT_TripInsert"
				meta.spUpdate = "proc_UUT_TripUpdate"
				meta.spDelete = "proc_UUT_TripDelete"
				meta.spLoadAll = "proc_UUT_TripLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_TripLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTTripMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

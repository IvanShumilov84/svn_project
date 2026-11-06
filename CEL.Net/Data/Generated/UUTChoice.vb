
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
	' Encapsulates the 'UUT_Choice' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTChoice))> _
	<XmlType("UUTChoice")> _	
	Partial Public Class UUTChoice 
		Inherits esUUTChoice
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTChoice()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16)
			Dim obj As New UUTChoice()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.ChoiceID = choiceID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTChoice()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.ChoiceID = choiceID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTChoiceCollection")> _
	Partial Public Class UUTChoiceCollection
		Inherits esUUTChoiceCollection
		Implements IEnumerable(Of UUTChoice)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16) As UUTChoice
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.ChoiceID.HasValue AndAlso e.ChoiceID.Value = choiceID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTChoice))> _
		Public Class UUTChoiceCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTChoiceCollection)
			
			Public Shared Widening Operator CType(packet As UUTChoiceCollectionWCFPacket) As UUTChoiceCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTChoiceCollection) As UUTChoiceCollectionWCFPacket
				Return New UUTChoiceCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTChoiceQuery 
		Inherits esUUTChoiceQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTChoiceQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTChoiceQuery) As String
			Return UUTChoiceQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTChoiceQuery
			Return DirectCast(UUTChoiceQuery.SerializeHelper.FromXml(query, GetType(UUTChoiceQuery)), UUTChoiceQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTChoice
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, choiceID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, choiceID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, choiceID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, choiceID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16) As Boolean
		
			Dim query As New UUTChoiceQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.ChoiceID = choiceID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
						parms.Add("ChoiceID", choiceID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_Choice.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTChoiceMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTChoiceMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UUTChoiceMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTChoiceMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice.ChoiceID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChoiceID As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(UUTChoiceMetadata.ColumnNames.ChoiceID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(UUTChoiceMetadata.ColumnNames.ChoiceID, value) Then
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.ChoiceID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice.ChoiceTitle
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChoiceTitle As System.String
			Get
				Return MyBase.GetSystemString(UUTChoiceMetadata.ColumnNames.ChoiceTitle)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTChoiceMetadata.ColumnNames.ChoiceTitle, value) Then
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.ChoiceTitle)
				End If
			End Set
		End Property	
			
		' <summary>
		' Pour eventuellement avoir l'option que le choix soit dans un display Radio Button ou DropDown list
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChoiceStyle As System.String
			Get
				Return MyBase.GetSystemString(UUTChoiceMetadata.ColumnNames.ChoiceStyle)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTChoiceMetadata.ColumnNames.ChoiceStyle, value) Then
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.ChoiceStyle)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice.DataType
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DataType As System.String
			Get
				Return MyBase.GetSystemString(UUTChoiceMetadata.ColumnNames.DataType)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTChoiceMetadata.ColumnNames.DataType, value) Then
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.DataType)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice.DefaultIndex
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DefaultIndex As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTChoiceMetadata.ColumnNames.DefaultIndex)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTChoiceMetadata.ColumnNames.DefaultIndex, value) Then
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.DefaultIndex)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice.Mandatory
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Mandatory As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTChoiceMetadata.ColumnNames.Mandatory)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTChoiceMetadata.ColumnNames.Mandatory, value) Then
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.Mandatory)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(UUTChoiceMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTChoiceMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.Note)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice.CustomSelectQuery
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CustomSelectQuery As System.String
			Get
				Return MyBase.GetSystemString(UUTChoiceMetadata.ColumnNames.CustomSelectQuery)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTChoiceMetadata.ColumnNames.CustomSelectQuery, value) Then
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.CustomSelectQuery)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice.CustomUpdateQuery
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CustomUpdateQuery As System.String
			Get
				Return MyBase.GetSystemString(UUTChoiceMetadata.ColumnNames.CustomUpdateQuery)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTChoiceMetadata.ColumnNames.CustomUpdateQuery, value) Then
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.CustomUpdateQuery)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTChoiceMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTChoiceMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(UUTChoiceMetadata.PropertyNames.Unavailable)
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
												
						Case "ChoiceID"
							Me.str().ChoiceID = CType(value, string)
												
						Case "ChoiceTitle"
							Me.str().ChoiceTitle = CType(value, string)
												
						Case "ChoiceStyle"
							Me.str().ChoiceStyle = CType(value, string)
												
						Case "DataType"
							Me.str().DataType = CType(value, string)
												
						Case "DefaultIndex"
							Me.str().DefaultIndex = CType(value, string)
												
						Case "Mandatory"
							Me.str().Mandatory = CType(value, string)
												
						Case "Note"
							Me.str().Note = CType(value, string)
												
						Case "CustomSelectQuery"
							Me.str().CustomSelectQuery = CType(value, string)
												
						Case "CustomUpdateQuery"
							Me.str().CustomUpdateQuery = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTChoiceMetadata.PropertyNames.ProjectID)
							End If
						
						Case "ChoiceID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.ChoiceID = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(UUTChoiceMetadata.PropertyNames.ChoiceID)
							End If
						
						Case "DefaultIndex"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.DefaultIndex = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTChoiceMetadata.PropertyNames.DefaultIndex)
							End If
						
						Case "Mandatory"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Mandatory = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTChoiceMetadata.PropertyNames.Mandatory)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTChoiceMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esUUTChoice)
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
		  	
			Public Property ChoiceID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.ChoiceID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ChoiceID = Nothing
					Else
						entity.ChoiceID = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property ChoiceTitle As System.String 
				Get
					Dim data_ As System.String = entity.ChoiceTitle
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ChoiceTitle = Nothing
					Else
						entity.ChoiceTitle = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ChoiceStyle As System.String 
				Get
					Dim data_ As System.String = entity.ChoiceStyle
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ChoiceStyle = Nothing
					Else
						entity.ChoiceStyle = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DataType As System.String 
				Get
					Dim data_ As System.String = entity.DataType
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DataType = Nothing
					Else
						entity.DataType = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DefaultIndex As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.DefaultIndex
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DefaultIndex = Nothing
					Else
						entity.DefaultIndex = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Mandatory As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Mandatory
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Mandatory = Nothing
					Else
						entity.Mandatory = Convert.ToBoolean(Value)
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
		  	
			Public Property CustomSelectQuery As System.String 
				Get
					Dim data_ As System.String = entity.CustomSelectQuery
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CustomSelectQuery = Nothing
					Else
						entity.CustomSelectQuery = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CustomUpdateQuery As System.String 
				Get
					Dim data_ As System.String = entity.CustomUpdateQuery
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CustomUpdateQuery = Nothing
					Else
						entity.CustomUpdateQuery = Convert.ToString(Value)
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
		  

			Private entity As esUUTChoice
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTChoiceMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTChoiceQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTChoiceQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTChoiceQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTChoiceQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTChoiceQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTChoiceCollection
		Inherits esEntityCollection(Of UUTChoice)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTChoiceMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTChoiceCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTChoiceQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTChoiceQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTChoiceQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTChoiceQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTChoiceQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTChoiceQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTChoiceQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTChoiceQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTChoiceMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "ChoiceID" 
					Return Me.ChoiceID
				Case "ChoiceTitle" 
					Return Me.ChoiceTitle
				Case "ChoiceStyle" 
					Return Me.ChoiceStyle
				Case "DataType" 
					Return Me.DataType
				Case "DefaultIndex" 
					Return Me.DefaultIndex
				Case "Mandatory" 
					Return Me.Mandatory
				Case "Note" 
					Return Me.Note
				Case "CustomSelectQuery" 
					Return Me.CustomSelectQuery
				Case "CustomUpdateQuery" 
					Return Me.CustomUpdateQuery
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
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChoiceID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.ChoiceID, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property ChoiceTitle As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.ChoiceTitle, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChoiceStyle As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.ChoiceStyle, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DataType As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.DataType, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DefaultIndex As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.DefaultIndex, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Mandatory As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.Mandatory, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CustomSelectQuery As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.CustomSelectQuery, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CustomUpdateQuery As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.CustomUpdateQuery, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTChoice 
		Inherits esUUTChoice
		
	
		#Region "UUTChoiceItemsCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTChoiceItemsCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.UUTChoice.UUTChoiceItemsCollectionByProjectID_Delegate)
				map.PropertyName = "UUTChoiceItemsCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel,ChoiceID"
				map.ParentColumnName = "ProjectID,EngineModel,ChoiceID"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub UUTChoiceItemsCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UUTChoiceQuery(data.NextAlias())
			
			Dim mee As UUTChoiceItemsQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTChoiceItemsQuery), New UUTChoiceItemsQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel And parent.ChoiceID = mee.ChoiceID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUT_ChoiceUUT_Choice_Items
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTChoiceItemsCollectionByProjectID As UUTChoiceItemsCollection 
		
			Get
				If Me._UUTChoiceItemsCollectionByProjectID Is Nothing Then
					Me._UUTChoiceItemsCollectionByProjectID = New UUTChoiceItemsCollection()
					Me._UUTChoiceItemsCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTChoiceItemsCollectionByProjectID", Me._UUTChoiceItemsCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) AndAlso Not Me.ChoiceID.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTChoiceItemsCollectionByProjectID.Query.Where(Me._UUTChoiceItemsCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UUTChoiceItemsCollectionByProjectID.Query.Where(Me._UUTChoiceItemsCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._UUTChoiceItemsCollectionByProjectID.Query.Where(Me._UUTChoiceItemsCollectionByProjectID.Query.ChoiceID = Me.ChoiceID)
							Me._UUTChoiceItemsCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTChoiceItemsCollectionByProjectID.fks.Add(UUTChoiceItemsMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._UUTChoiceItemsCollectionByProjectID.fks.Add(UUTChoiceItemsMetadata.ColumnNames.EngineModel, Me.EngineModel)
						Me._UUTChoiceItemsCollectionByProjectID.fks.Add(UUTChoiceItemsMetadata.ColumnNames.ChoiceID, Me.ChoiceID)
					End If
				End If

				Return Me._UUTChoiceItemsCollectionByProjectID
			End Get
			
			Set(ByVal value As UUTChoiceItemsCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTChoiceItemsCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UUTChoiceItemsCollectionByProjectID")
					Me._UUTChoiceItemsCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTChoiceItemsCollectionByProjectID As UUTChoiceItemsCollection
		#End Region

		#Region "UpToUutByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUTUUT_Choice
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

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "UUTChoiceItemsCollectionByProjectID"
					coll = Me.UUTChoiceItemsCollectionByProjectID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "UUTChoiceItemsCollectionByProjectID", GetType(UUTChoiceItemsCollection), New UUTChoiceItems()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class UUTChoiceMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.ChoiceID, 2, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.ChoiceID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 5
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.ChoiceTitle, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.ChoiceTitle
			c.CharacterMaxLength = 100
			c.HasDefault = True
			c.Default = """NO CHOICE"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.ChoiceStyle, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.ChoiceStyle
			c.CharacterMaxLength = 255
			c.Description = "Pour eventuellement avoir l'option que le choix soit dans un display Radio Button ou DropDown list"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.DataType, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.DataType
			c.CharacterMaxLength = 10
			c.HasDefault = True
			c.Default = """TEXT"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.DefaultIndex, 6, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.DefaultIndex
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.Mandatory, 7, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.Mandatory
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.Note, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.Note
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.CustomSelectQuery, 9, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.CustomSelectQuery
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.CustomUpdateQuery, 10, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.CustomUpdateQuery
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceMetadata.ColumnNames.Unavailable, 11, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTChoiceMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTChoiceMetadata
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
			 Public Const ChoiceID As String = "ChoiceID"
			 Public Const ChoiceTitle As String = "ChoiceTitle"
			 Public Const ChoiceStyle As String = "ChoiceStyle"
			 Public Const DataType As String = "DataType"
			 Public Const DefaultIndex As String = "DefaultIndex"
			 Public Const Mandatory As String = "Mandatory"
			 Public Const Note As String = "Note"
			 Public Const CustomSelectQuery As String = "CustomSelectQuery"
			 Public Const CustomUpdateQuery As String = "CustomUpdateQuery"
			 Public Const Unavailable As String = "Unavailable"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const ChoiceID As String = "ChoiceID"
			 Public Const ChoiceTitle As String = "ChoiceTitle"
			 Public Const ChoiceStyle As String = "ChoiceStyle"
			 Public Const DataType As String = "DataType"
			 Public Const DefaultIndex As String = "DefaultIndex"
			 Public Const Mandatory As String = "Mandatory"
			 Public Const Note As String = "Note"
			 Public Const CustomSelectQuery As String = "CustomSelectQuery"
			 Public Const CustomUpdateQuery As String = "CustomUpdateQuery"
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
			SyncLock GetType(UUTChoiceMetadata)
			
				If UUTChoiceMetadata.mapDelegates Is Nothing Then
					UUTChoiceMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTChoiceMetadata._meta Is Nothing Then
					UUTChoiceMetadata._meta = New UUTChoiceMetadata()
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
				meta.AddTypeMap("ChoiceID", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("ChoiceTitle", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ChoiceStyle", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DataType", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DefaultIndex", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Mandatory", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Note", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("CustomSelectQuery", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("CustomUpdateQuery", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "UUT_Choice"
				meta.Destination = "UUT_Choice"
				
				meta.spInsert = "proc_UUT_ChoiceInsert"
				meta.spUpdate = "proc_UUT_ChoiceUpdate"
				meta.spDelete = "proc_UUT_ChoiceDelete"
				meta.spLoadAll = "proc_UUT_ChoiceLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_ChoiceLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTChoiceMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

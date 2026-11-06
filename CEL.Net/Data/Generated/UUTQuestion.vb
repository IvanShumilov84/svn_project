
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 5/7/2015 12:40:44 PM
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
	' Encapsulates the 'UUT_Question' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTQuestion))> _
	<XmlType("UUTQuestion")> _	
	Partial Public Class UUTQuestion 
		Inherits esUUTQuestion
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTQuestion()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal questionID As System.Int16)
			Dim obj As New UUTQuestion()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.QuestionID = questionID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal questionID As System.Int16, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTQuestion()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.QuestionID = questionID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTQuestionCollection")> _
	Partial Public Class UUTQuestionCollection
		Inherits esUUTQuestionCollection
		Implements IEnumerable(Of UUTQuestion)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal questionID As System.Int16) As UUTQuestion
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.QuestionID.HasValue AndAlso e.QuestionID.Value = questionID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTQuestion))> _
		Public Class UUTQuestionCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTQuestionCollection)
			
			Public Shared Widening Operator CType(packet As UUTQuestionCollectionWCFPacket) As UUTQuestionCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTQuestionCollection) As UUTQuestionCollectionWCFPacket
				Return New UUTQuestionCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTQuestionQuery 
		Inherits esUUTQuestionQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTQuestionQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTQuestionQuery) As String
			Return UUTQuestionQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTQuestionQuery
			Return DirectCast(UUTQuestionQuery.SerializeHelper.FromXml(query, GetType(UUTQuestionQuery)), UUTQuestionQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTQuestion
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal questionID As System.Int16) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, questionID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, questionID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal questionID As System.Int16) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, questionID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, questionID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal questionID As System.Int16) As Boolean
		
			Dim query As New UUTQuestionQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.QuestionID = questionID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal questionID As System.Int16) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
						parms.Add("QuestionID", questionID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_Question.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTQuestionMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTQuestionMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UUTQuestionMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTQuestionMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.QuestionID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property QuestionID As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(UUTQuestionMetadata.ColumnNames.QuestionID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(UUTQuestionMetadata.ColumnNames.QuestionID, value) Then
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.QuestionID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.Question
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Question As System.String
			Get
				Return MyBase.GetSystemString(UUTQuestionMetadata.ColumnNames.Question)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTQuestionMetadata.ColumnNames.Question, value) Then
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.Question)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.DataType
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DataType As System.String
			Get
				Return MyBase.GetSystemString(UUTQuestionMetadata.ColumnNames.DataType)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTQuestionMetadata.ColumnNames.DataType, value) Then
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.DataType)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.DefaultValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DefaultValue As System.String
			Get
				Return MyBase.GetSystemString(UUTQuestionMetadata.ColumnNames.DefaultValue)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTQuestionMetadata.ColumnNames.DefaultValue, value) Then
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.DefaultValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.HiLimit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HiLimit As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UUTQuestionMetadata.ColumnNames.HiLimit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UUTQuestionMetadata.ColumnNames.HiLimit, value) Then
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.HiLimit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.LoLimit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LoLimit As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UUTQuestionMetadata.ColumnNames.LoLimit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UUTQuestionMetadata.ColumnNames.LoLimit, value) Then
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.LoLimit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.Mandatory
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Mandatory As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTQuestionMetadata.ColumnNames.Mandatory)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTQuestionMetadata.ColumnNames.Mandatory, value) Then
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.Mandatory)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(UUTQuestionMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTQuestionMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.Note)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTQuestionMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTQuestionMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Question.DefaultValueUpdatable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DefaultValueUpdatable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTQuestionMetadata.ColumnNames.DefaultValueUpdatable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTQuestionMetadata.ColumnNames.DefaultValueUpdatable, value) Then
					OnPropertyChanged(UUTQuestionMetadata.PropertyNames.DefaultValueUpdatable)
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
												
						Case "QuestionID"
							Me.str().QuestionID = CType(value, string)
												
						Case "Question"
							Me.str().Question = CType(value, string)
												
						Case "DataType"
							Me.str().DataType = CType(value, string)
												
						Case "DefaultValue"
							Me.str().DefaultValue = CType(value, string)
												
						Case "HiLimit"
							Me.str().HiLimit = CType(value, string)
												
						Case "LoLimit"
							Me.str().LoLimit = CType(value, string)
												
						Case "Mandatory"
							Me.str().Mandatory = CType(value, string)
												
						Case "Note"
							Me.str().Note = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
												
						Case "DefaultValueUpdatable"
							Me.str().DefaultValueUpdatable = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTQuestionMetadata.PropertyNames.ProjectID)
							End If
						
						Case "QuestionID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.QuestionID = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(UUTQuestionMetadata.PropertyNames.QuestionID)
							End If
						
						Case "HiLimit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.HiLimit = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UUTQuestionMetadata.PropertyNames.HiLimit)
							End If
						
						Case "LoLimit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.LoLimit = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UUTQuestionMetadata.PropertyNames.LoLimit)
							End If
						
						Case "Mandatory"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Mandatory = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTQuestionMetadata.PropertyNames.Mandatory)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTQuestionMetadata.PropertyNames.Unavailable)
							End If
						
						Case "DefaultValueUpdatable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.DefaultValueUpdatable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTQuestionMetadata.PropertyNames.DefaultValueUpdatable)
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
		
			Public Sub New(ByVal entity As esUUTQuestion)
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
		  	
			Public Property QuestionID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.QuestionID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.QuestionID = Nothing
					Else
						entity.QuestionID = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property Question As System.String 
				Get
					Dim data_ As System.String = entity.Question
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Question = Nothing
					Else
						entity.Question = Convert.ToString(Value)
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
		  	
			Public Property DefaultValue As System.String 
				Get
					Dim data_ As System.String = entity.DefaultValue
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DefaultValue = Nothing
					Else
						entity.DefaultValue = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property HiLimit As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.HiLimit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HiLimit = Nothing
					Else
						entity.HiLimit = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property LoLimit As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.LoLimit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LoLimit = Nothing
					Else
						entity.LoLimit = Convert.ToDouble(Value)
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
		  	
			Public Property DefaultValueUpdatable As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.DefaultValueUpdatable
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DefaultValueUpdatable = Nothing
					Else
						entity.DefaultValueUpdatable = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  

			Private entity As esUUTQuestion
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTQuestionMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTQuestionQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTQuestionQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTQuestionQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTQuestionQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTQuestionQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTQuestionCollection
		Inherits esEntityCollection(Of UUTQuestion)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTQuestionMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTQuestionCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTQuestionQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTQuestionQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTQuestionQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTQuestionQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTQuestionQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTQuestionQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTQuestionQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTQuestionQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTQuestionMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "QuestionID" 
					Return Me.QuestionID
				Case "Question" 
					Return Me.Question
				Case "DataType" 
					Return Me.DataType
				Case "DefaultValue" 
					Return Me.DefaultValue
				Case "HiLimit" 
					Return Me.HiLimit
				Case "LoLimit" 
					Return Me.LoLimit
				Case "Mandatory" 
					Return Me.Mandatory
				Case "Note" 
					Return Me.Note
				Case "Unavailable" 
					Return Me.Unavailable
				Case "DefaultValueUpdatable" 
					Return Me.DefaultValueUpdatable
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property QuestionID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.QuestionID, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property Question As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.Question, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DataType As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.DataType, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DefaultValue As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.DefaultValue, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property HiLimit As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.HiLimit, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property LoLimit As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.LoLimit, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Mandatory As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.Mandatory, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property DefaultValueUpdatable As esQueryItem
			Get
				Return New esQueryItem(Me, UUTQuestionMetadata.ColumnNames.DefaultValueUpdatable, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTQuestion 
		Inherits esUUTQuestion
		
	
		#Region "UpToUutByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUTUUT_Question
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
	Partial Public Class UUTQuestionMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.QuestionID, 2, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.QuestionID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 5
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.Question, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.Question
			c.CharacterMaxLength = 100
			c.HasDefault = True
			c.Default = """NO QUESTION"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.DataType, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.DataType
			c.CharacterMaxLength = 10
			c.HasDefault = True
			c.Default = """TEXT"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.DefaultValue, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.DefaultValue
			c.CharacterMaxLength = 100
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.HiLimit, 6, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.HiLimit
			c.NumericPrecision = 15
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.LoLimit, 7, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.LoLimit
			c.NumericPrecision = 15
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.Mandatory, 8, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.Mandatory
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.Note, 9, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.Note
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.Unavailable, 10, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTQuestionMetadata.ColumnNames.DefaultValueUpdatable, 11, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTQuestionMetadata.PropertyNames.DefaultValueUpdatable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTQuestionMetadata
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
			 Public Const QuestionID As String = "QuestionID"
			 Public Const Question As String = "Question"
			 Public Const DataType As String = "DataType"
			 Public Const DefaultValue As String = "DefaultValue"
			 Public Const HiLimit As String = "HiLimit"
			 Public Const LoLimit As String = "LoLimit"
			 Public Const Mandatory As String = "Mandatory"
			 Public Const Note As String = "Note"
			 Public Const Unavailable As String = "Unavailable"
			 Public Const DefaultValueUpdatable As String = "DefaultValueUpdatable"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const QuestionID As String = "QuestionID"
			 Public Const Question As String = "Question"
			 Public Const DataType As String = "DataType"
			 Public Const DefaultValue As String = "DefaultValue"
			 Public Const HiLimit As String = "HiLimit"
			 Public Const LoLimit As String = "LoLimit"
			 Public Const Mandatory As String = "Mandatory"
			 Public Const Note As String = "Note"
			 Public Const Unavailable As String = "Unavailable"
			 Public Const DefaultValueUpdatable As String = "DefaultValueUpdatable"
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
			SyncLock GetType(UUTQuestionMetadata)
			
				If UUTQuestionMetadata.mapDelegates Is Nothing Then
					UUTQuestionMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTQuestionMetadata._meta Is Nothing Then
					UUTQuestionMetadata._meta = New UUTQuestionMetadata()
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
				meta.AddTypeMap("QuestionID", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("Question", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DataType", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DefaultValue", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("HiLimit", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("LoLimit", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Mandatory", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Note", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("DefaultValueUpdatable", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "UUT_Question"
				meta.Destination = "UUT_Question"
				
				meta.spInsert = "proc_UUT_QuestionInsert"
				meta.spUpdate = "proc_UUT_QuestionUpdate"
				meta.spDelete = "proc_UUT_QuestionDelete"
				meta.spLoadAll = "proc_UUT_QuestionLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_QuestionLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTQuestionMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

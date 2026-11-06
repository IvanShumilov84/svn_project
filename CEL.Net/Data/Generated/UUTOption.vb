
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
	' Encapsulates the 'UUT_Option' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTOption))> _
	<XmlType("UUTOption")> _	
	Partial Public Class UUTOption 
		Inherits esUUTOption
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTOption()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal optionID As System.Int16)
			Dim obj As New UUTOption()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.OptionID = optionID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal optionID As System.Int16, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTOption()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.OptionID = optionID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTOptionCollection")> _
	Partial Public Class UUTOptionCollection
		Inherits esUUTOptionCollection
		Implements IEnumerable(Of UUTOption)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal optionID As System.Int16) As UUTOption
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.OptionID.HasValue AndAlso e.OptionID.Value = optionID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTOption))> _
		Public Class UUTOptionCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTOptionCollection)
			
			Public Shared Widening Operator CType(packet As UUTOptionCollectionWCFPacket) As UUTOptionCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTOptionCollection) As UUTOptionCollectionWCFPacket
				Return New UUTOptionCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTOptionQuery 
		Inherits esUUTOptionQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTOptionQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTOptionQuery) As String
			Return UUTOptionQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTOptionQuery
			Return DirectCast(UUTOptionQuery.SerializeHelper.FromXml(query, GetType(UUTOptionQuery)), UUTOptionQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTOption
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal optionID As System.Int16) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, optionID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, optionID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal optionID As System.Int16) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, optionID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, optionID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal optionID As System.Int16) As Boolean
		
			Dim query As New UUTOptionQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.OptionID = optionID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal optionID As System.Int16) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
						parms.Add("OptionID", optionID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_Option.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTOptionMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTOptionMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTOptionMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Option.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UUTOptionMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTOptionMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTOptionMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Option.OptionID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OptionID As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(UUTOptionMetadata.ColumnNames.OptionID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(UUTOptionMetadata.ColumnNames.OptionID, value) Then
					OnPropertyChanged(UUTOptionMetadata.PropertyNames.OptionID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Option.OptionTitle
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OptionTitle As System.String
			Get
				Return MyBase.GetSystemString(UUTOptionMetadata.ColumnNames.OptionTitle)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTOptionMetadata.ColumnNames.OptionTitle, value) Then
					OnPropertyChanged(UUTOptionMetadata.PropertyNames.OptionTitle)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Option.DefaultValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DefaultValue As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTOptionMetadata.ColumnNames.DefaultValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTOptionMetadata.ColumnNames.DefaultValue, value) Then
					OnPropertyChanged(UUTOptionMetadata.PropertyNames.DefaultValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Option.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(UUTOptionMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTOptionMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(UUTOptionMetadata.PropertyNames.Note)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Option.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTOptionMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTOptionMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(UUTOptionMetadata.PropertyNames.Unavailable)
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
												
						Case "OptionID"
							Me.str().OptionID = CType(value, string)
												
						Case "OptionTitle"
							Me.str().OptionTitle = CType(value, string)
												
						Case "DefaultValue"
							Me.str().DefaultValue = CType(value, string)
												
						Case "Note"
							Me.str().Note = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTOptionMetadata.PropertyNames.ProjectID)
							End If
						
						Case "OptionID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.OptionID = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(UUTOptionMetadata.PropertyNames.OptionID)
							End If
						
						Case "DefaultValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.DefaultValue = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTOptionMetadata.PropertyNames.DefaultValue)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTOptionMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esUUTOption)
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
		  	
			Public Property OptionID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.OptionID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OptionID = Nothing
					Else
						entity.OptionID = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property OptionTitle As System.String 
				Get
					Dim data_ As System.String = entity.OptionTitle
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OptionTitle = Nothing
					Else
						entity.OptionTitle = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DefaultValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.DefaultValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DefaultValue = Nothing
					Else
						entity.DefaultValue = Convert.ToBoolean(Value)
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
		  

			Private entity As esUUTOption
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTOptionMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTOptionQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTOptionQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTOptionQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTOptionQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTOptionQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTOptionCollection
		Inherits esEntityCollection(Of UUTOption)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTOptionMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTOptionCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTOptionQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTOptionQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTOptionQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTOptionQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTOptionQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTOptionQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTOptionQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTOptionQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTOptionMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "OptionID" 
					Return Me.OptionID
				Case "OptionTitle" 
					Return Me.OptionTitle
				Case "DefaultValue" 
					Return Me.DefaultValue
				Case "Note" 
					Return Me.Note
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
				Return New esQueryItem(Me, UUTOptionMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UUTOptionMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OptionID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTOptionMetadata.ColumnNames.OptionID, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property OptionTitle As esQueryItem
			Get
				Return New esQueryItem(Me, UUTOptionMetadata.ColumnNames.OptionTitle, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DefaultValue As esQueryItem
			Get
				Return New esQueryItem(Me, UUTOptionMetadata.ColumnNames.DefaultValue, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, UUTOptionMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, UUTOptionMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTOption 
		Inherits esUUTOption
		
	
		#Region "UpToUutByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUTUUT_Option
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
	Partial Public Class UUTOptionMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTOptionMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTOptionMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTOptionMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTOptionMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTOptionMetadata.ColumnNames.OptionID, 2, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = UUTOptionMetadata.PropertyNames.OptionID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 5
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTOptionMetadata.ColumnNames.OptionTitle, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTOptionMetadata.PropertyNames.OptionTitle
			c.CharacterMaxLength = 100
			c.HasDefault = True
			c.Default = """NO CHOICE"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTOptionMetadata.ColumnNames.DefaultValue, 4, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTOptionMetadata.PropertyNames.DefaultValue
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTOptionMetadata.ColumnNames.Note, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTOptionMetadata.PropertyNames.Note
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTOptionMetadata.ColumnNames.Unavailable, 6, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTOptionMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTOptionMetadata
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
			 Public Const OptionID As String = "OptionID"
			 Public Const OptionTitle As String = "OptionTitle"
			 Public Const DefaultValue As String = "DefaultValue"
			 Public Const Note As String = "Note"
			 Public Const Unavailable As String = "Unavailable"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const OptionID As String = "OptionID"
			 Public Const OptionTitle As String = "OptionTitle"
			 Public Const DefaultValue As String = "DefaultValue"
			 Public Const Note As String = "Note"
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
			SyncLock GetType(UUTOptionMetadata)
			
				If UUTOptionMetadata.mapDelegates Is Nothing Then
					UUTOptionMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTOptionMetadata._meta Is Nothing Then
					UUTOptionMetadata._meta = New UUTOptionMetadata()
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
				meta.AddTypeMap("OptionID", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("OptionTitle", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DefaultValue", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Note", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "UUT_Option"
				meta.Destination = "UUT_Option"
				
				meta.spInsert = "proc_UUT_OptionInsert"
				meta.spUpdate = "proc_UUT_OptionUpdate"
				meta.spDelete = "proc_UUT_OptionDelete"
				meta.spLoadAll = "proc_UUT_OptionLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_OptionLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTOptionMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

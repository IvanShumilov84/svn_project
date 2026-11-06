
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
	' Encapsulates the 'UUT_Choice_Items' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTChoiceItems))> _
	<XmlType("UUTChoiceItems")> _	
	Partial Public Class UUTChoiceItems 
		Inherits esUUTChoiceItems
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTChoiceItems()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16, ByVal itemID As System.Int32)
			Dim obj As New UUTChoiceItems()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.ChoiceID = choiceID
			obj.ItemID = itemID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16, ByVal itemID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTChoiceItems()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.ChoiceID = choiceID
			obj.ItemID = itemID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTChoiceItemsCollection")> _
	Partial Public Class UUTChoiceItemsCollection
		Inherits esUUTChoiceItemsCollection
		Implements IEnumerable(Of UUTChoiceItems)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16, ByVal itemID As System.Int32) As UUTChoiceItems
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.ChoiceID.HasValue AndAlso e.ChoiceID.Value = choiceID And e.ItemID.HasValue AndAlso e.ItemID.Value = itemID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTChoiceItems))> _
		Public Class UUTChoiceItemsCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTChoiceItemsCollection)
			
			Public Shared Widening Operator CType(packet As UUTChoiceItemsCollectionWCFPacket) As UUTChoiceItemsCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTChoiceItemsCollection) As UUTChoiceItemsCollectionWCFPacket
				Return New UUTChoiceItemsCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTChoiceItemsQuery 
		Inherits esUUTChoiceItemsQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTChoiceItemsQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTChoiceItemsQuery) As String
			Return UUTChoiceItemsQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTChoiceItemsQuery
			Return DirectCast(UUTChoiceItemsQuery.SerializeHelper.FromXml(query, GetType(UUTChoiceItemsQuery)), UUTChoiceItemsQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTChoiceItems
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16, ByVal itemID As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, choiceID, itemID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, choiceID, itemID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16, ByVal itemID As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, choiceID, itemID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, choiceID, itemID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16, ByVal itemID As System.Int32) As Boolean
		
			Dim query As New UUTChoiceItemsQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.ChoiceID = choiceID And query.ItemID = itemID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal choiceID As System.Int16, ByVal itemID As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
						parms.Add("ChoiceID", choiceID)
						parms.Add("ItemID", itemID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_Choice_Items.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTChoiceItemsMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTChoiceItemsMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToUUTChoiceByProjectID = Nothing
					Me.OnPropertyChanged("UpToUUTChoiceByProjectID")
					OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice_Items.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UUTChoiceItemsMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTChoiceItemsMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToUUTChoiceByProjectID = Nothing
					Me.OnPropertyChanged("UpToUUTChoiceByProjectID")
					OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice_Items.ChoiceID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChoiceID As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(UUTChoiceItemsMetadata.ColumnNames.ChoiceID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(UUTChoiceItemsMetadata.ColumnNames.ChoiceID, value) Then
					Me._UpToUUTChoiceByProjectID = Nothing
					Me.OnPropertyChanged("UpToUUTChoiceByProjectID")
					OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.ChoiceID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice_Items.ItemID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ItemID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTChoiceItemsMetadata.ColumnNames.ItemID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTChoiceItemsMetadata.ColumnNames.ItemID, value) Then
					OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.ItemID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice_Items.ItemDescription
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ItemDescription As System.String
			Get
				Return MyBase.GetSystemString(UUTChoiceItemsMetadata.ColumnNames.ItemDescription)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTChoiceItemsMetadata.ColumnNames.ItemDescription, value) Then
					OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.ItemDescription)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice_Items.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(UUTChoiceItemsMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTChoiceItemsMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.Note)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Choice_Items.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTChoiceItemsMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTChoiceItemsMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToUUTChoiceByProjectID As UUTChoice
		
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
												
						Case "ItemID"
							Me.str().ItemID = CType(value, string)
												
						Case "ItemDescription"
							Me.str().ItemDescription = CType(value, string)
												
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
								OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.ProjectID)
							End If
						
						Case "ChoiceID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.ChoiceID = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.ChoiceID)
							End If
						
						Case "ItemID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ItemID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.ItemID)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTChoiceItemsMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esUUTChoiceItems)
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
		  	
			Public Property ItemID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.ItemID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ItemID = Nothing
					Else
						entity.ItemID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property ItemDescription As System.String 
				Get
					Dim data_ As System.String = entity.ItemDescription
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ItemDescription = Nothing
					Else
						entity.ItemDescription = Convert.ToString(Value)
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
		  

			Private entity As esUUTChoiceItems
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTChoiceItemsMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTChoiceItemsQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTChoiceItemsQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTChoiceItemsQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTChoiceItemsQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTChoiceItemsQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTChoiceItemsCollection
		Inherits esEntityCollection(Of UUTChoiceItems)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTChoiceItemsMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTChoiceItemsCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTChoiceItemsQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTChoiceItemsQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTChoiceItemsQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTChoiceItemsQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTChoiceItemsQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTChoiceItemsQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTChoiceItemsQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTChoiceItemsQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTChoiceItemsMetadata.Meta()
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
				Case "ItemID" 
					Return Me.ItemID
				Case "ItemDescription" 
					Return Me.ItemDescription
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
				Return New esQueryItem(Me, UUTChoiceItemsMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceItemsMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChoiceID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceItemsMetadata.ColumnNames.ChoiceID, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property ItemID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceItemsMetadata.ColumnNames.ItemID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ItemDescription As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceItemsMetadata.ColumnNames.ItemDescription, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceItemsMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, UUTChoiceItemsMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTChoiceItems 
		Inherits esUUTChoiceItems
		
	
		#Region "UpToUUTChoiceByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUT_ChoiceUUT_Choice_Items
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToUUTChoiceByProjectID As UUTChoice
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToUUTChoiceByProjectID Is Nothing _
						 AndAlso Not ProjectID.Equals(Nothing)  AndAlso Not EngineModel.Equals(Nothing)  AndAlso Not ChoiceID.Equals(Nothing)  Then
						
					Me._UpToUUTChoiceByProjectID = New UUTChoice()
					Me._UpToUUTChoiceByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToUUTChoiceByProjectID", Me._UpToUUTChoiceByProjectID)
					Me._UpToUUTChoiceByProjectID.Query.Where(Me._UpToUUTChoiceByProjectID.Query.ProjectID = Me.ProjectID)
					Me._UpToUUTChoiceByProjectID.Query.Where(Me._UpToUUTChoiceByProjectID.Query.EngineModel = Me.EngineModel)
					Me._UpToUUTChoiceByProjectID.Query.Where(Me._UpToUUTChoiceByProjectID.Query.ChoiceID = Me.ChoiceID)
					Me._UpToUUTChoiceByProjectID.Query.Load()
				End If

				Return Me._UpToUUTChoiceByProjectID
			End Get
			
            Set(ByVal value As UUTChoice)
				Me.RemovePreSave("UpToUUTChoiceByProjectID")
				

				If value Is Nothing Then
				
					Me.ProjectID = Nothing
				
					Me.EngineModel = Nothing
				
					Me.ChoiceID = Nothing
				
					Me._UpToUUTChoiceByProjectID = Nothing
				Else
				
					Me.ProjectID = value.ProjectID
					
					Me.EngineModel = value.EngineModel
					
					Me.ChoiceID = value.ChoiceID
					
					Me._UpToUUTChoiceByProjectID = value
					Me.SetPreSave("UpToUUTChoiceByProjectID", Me._UpToUUTChoiceByProjectID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class UUTChoiceItemsMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTChoiceItemsMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTChoiceItemsMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceItemsMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTChoiceItemsMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceItemsMetadata.ColumnNames.ChoiceID, 2, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = UUTChoiceItemsMetadata.PropertyNames.ChoiceID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 5
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceItemsMetadata.ColumnNames.ItemID, 3, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTChoiceItemsMetadata.PropertyNames.ItemID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceItemsMetadata.ColumnNames.ItemDescription, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTChoiceItemsMetadata.PropertyNames.ItemDescription
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceItemsMetadata.ColumnNames.Note, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTChoiceItemsMetadata.PropertyNames.Note
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTChoiceItemsMetadata.ColumnNames.Unavailable, 6, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTChoiceItemsMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTChoiceItemsMetadata
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
			 Public Const ItemID As String = "ItemID"
			 Public Const ItemDescription As String = "ItemDescription"
			 Public Const Note As String = "Note"
			 Public Const Unavailable As String = "Unavailable"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const ChoiceID As String = "ChoiceID"
			 Public Const ItemID As String = "ItemID"
			 Public Const ItemDescription As String = "ItemDescription"
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
			SyncLock GetType(UUTChoiceItemsMetadata)
			
				If UUTChoiceItemsMetadata.mapDelegates Is Nothing Then
					UUTChoiceItemsMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTChoiceItemsMetadata._meta Is Nothing Then
					UUTChoiceItemsMetadata._meta = New UUTChoiceItemsMetadata()
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
				meta.AddTypeMap("ItemID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ItemDescription", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Note", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "UUT_Choice_Items"
				meta.Destination = "UUT_Choice_Items"
				
				meta.spInsert = "proc_UUT_Choice_ItemsInsert"
				meta.spUpdate = "proc_UUT_Choice_ItemsUpdate"
				meta.spDelete = "proc_UUT_Choice_ItemsDelete"
				meta.spLoadAll = "proc_UUT_Choice_ItemsLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_Choice_ItemsLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTChoiceItemsMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

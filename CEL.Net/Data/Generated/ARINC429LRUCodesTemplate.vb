
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
	' Encapsulates the 'ARINC429LRUCodesTemplate' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(ARINC429LRUCodesTemplate))> _
	<XmlType("ARINC429LRUCodesTemplate")> _	
	Partial Public Class ARINC429LRUCodesTemplate 
		Inherits esARINC429LRUCodesTemplate
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New ARINC429LRUCodesTemplate()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32)
			Dim obj As New ARINC429LRUCodesTemplate()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New ARINC429LRUCodesTemplate()
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
	<XmlType("ARINC429LRUCodesTemplateCollection")> _
	Partial Public Class ARINC429LRUCodesTemplateCollection
		Inherits esARINC429LRUCodesTemplateCollection
		Implements IEnumerable(Of ARINC429LRUCodesTemplate)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32) As ARINC429LRUCodesTemplate
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(ARINC429LRUCodesTemplate))> _
		Public Class ARINC429LRUCodesTemplateCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of ARINC429LRUCodesTemplateCollection)
			
			Public Shared Widening Operator CType(packet As ARINC429LRUCodesTemplateCollectionWCFPacket) As ARINC429LRUCodesTemplateCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As ARINC429LRUCodesTemplateCollection) As ARINC429LRUCodesTemplateCollectionWCFPacket
				Return New ARINC429LRUCodesTemplateCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class ARINC429LRUCodesTemplateQuery 
		Inherits esARINC429LRUCodesTemplateQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "ARINC429LRUCodesTemplateQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As ARINC429LRUCodesTemplateQuery) As String
			Return ARINC429LRUCodesTemplateQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As ARINC429LRUCodesTemplateQuery
			Return DirectCast(ARINC429LRUCodesTemplateQuery.SerializeHelper.FromXml(query, GetType(ARINC429LRUCodesTemplateQuery)), ARINC429LRUCodesTemplateQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esARINC429LRUCodesTemplate
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
		
			Dim query As New ARINC429LRUCodesTemplateQuery()
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
		' Maps to ARINC429LRUCodesTemplate.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(ARINC429LRUCodesTemplateMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(ARINC429LRUCodesTemplateMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(ARINC429LRUCodesTemplateMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LRUCodesTemplate.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(ARINC429LRUCodesTemplateMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(ARINC429LRUCodesTemplateMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(ARINC429LRUCodesTemplateMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LRUCodesTemplate.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LRUCodesTemplateMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LRUCodesTemplateMetadata.ColumnNames.EngineModel, value) Then
					OnPropertyChanged(ARINC429LRUCodesTemplateMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LRUCodesTemplate.LRUCode
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LRUCode As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(ARINC429LRUCodesTemplateMetadata.ColumnNames.LRUCode)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(ARINC429LRUCodesTemplateMetadata.ColumnNames.LRUCode, value) Then
					OnPropertyChanged(ARINC429LRUCodesTemplateMetadata.PropertyNames.LRUCode)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LRUCodesTemplate.FaultWord
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FaultWord As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LRUCodesTemplateMetadata.ColumnNames.FaultWord)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LRUCodesTemplateMetadata.ColumnNames.FaultWord, value) Then
					OnPropertyChanged(ARINC429LRUCodesTemplateMetadata.PropertyNames.FaultWord)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LRUCodesTemplate.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LRUCodesTemplateMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LRUCodesTemplateMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(ARINC429LRUCodesTemplateMetadata.PropertyNames.Description)
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
												
						Case "Id"
							Me.str().Id = CType(value, string)
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
						Case "LRUCode"
							Me.str().LRUCode = CType(value, string)
												
						Case "FaultWord"
							Me.str().FaultWord = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(ARINC429LRUCodesTemplateMetadata.PropertyNames.Id)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(ARINC429LRUCodesTemplateMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "LRUCode"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.LRUCode = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(ARINC429LRUCodesTemplateMetadata.PropertyNames.LRUCode)
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
		
			Public Sub New(ByVal entity As esARINC429LRUCodesTemplate)
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
		  	
			Public Property LRUCode As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.LRUCode
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LRUCode = Nothing
					Else
						entity.LRUCode = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property FaultWord As System.String 
				Get
					Dim data_ As System.String = entity.FaultWord
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FaultWord = Nothing
					Else
						entity.FaultWord = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Description As System.String 
				Get
					Dim data_ As System.String = entity.Description
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Description = Nothing
					Else
						entity.Description = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esARINC429LRUCodesTemplate
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return ARINC429LRUCodesTemplateMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As ARINC429LRUCodesTemplateQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New ARINC429LRUCodesTemplateQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As ARINC429LRUCodesTemplateQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As ARINC429LRUCodesTemplateQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As ARINC429LRUCodesTemplateQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esARINC429LRUCodesTemplateCollection
		Inherits esEntityCollection(Of ARINC429LRUCodesTemplate)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return ARINC429LRUCodesTemplateMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "ARINC429LRUCodesTemplateCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As ARINC429LRUCodesTemplateQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New ARINC429LRUCodesTemplateQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As ARINC429LRUCodesTemplateQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New ARINC429LRUCodesTemplateQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As ARINC429LRUCodesTemplateQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, ARINC429LRUCodesTemplateQuery))
		End Sub
		
		#End Region
						
		Private m_query As ARINC429LRUCodesTemplateQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esARINC429LRUCodesTemplateQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return ARINC429LRUCodesTemplateMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "EngineModel" 
					Return Me.EngineModel
				Case "LRUCode" 
					Return Me.LRUCode
				Case "FaultWord" 
					Return Me.FaultWord
				Case "Description" 
					Return Me.Description
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LRUCodesTemplateMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LRUCodesTemplateMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LRUCodesTemplateMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LRUCode As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LRUCodesTemplateMetadata.ColumnNames.LRUCode, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property FaultWord As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LRUCodesTemplateMetadata.ColumnNames.FaultWord, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LRUCodesTemplateMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class ARINC429LRUCodesTemplate 
		Inherits esARINC429LRUCodesTemplate
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class ARINC429LRUCodesTemplateMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(ARINC429LRUCodesTemplateMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = ARINC429LRUCodesTemplateMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LRUCodesTemplateMetadata.ColumnNames.ExportToCsv, 1, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = ARINC429LRUCodesTemplateMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LRUCodesTemplateMetadata.ColumnNames.EngineModel, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LRUCodesTemplateMetadata.PropertyNames.EngineModel
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LRUCodesTemplateMetadata.ColumnNames.LRUCode, 3, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = ARINC429LRUCodesTemplateMetadata.PropertyNames.LRUCode
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LRUCodesTemplateMetadata.ColumnNames.FaultWord, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LRUCodesTemplateMetadata.PropertyNames.FaultWord
			c.CharacterMaxLength = 6
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LRUCodesTemplateMetadata.ColumnNames.Description, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LRUCodesTemplateMetadata.PropertyNames.Description
			c.CharacterMaxLength = 75
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As ARINC429LRUCodesTemplateMetadata
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
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const LRUCode As String = "LRUCode"
			 Public Const FaultWord As String = "FaultWord"
			 Public Const Description As String = "Description"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const LRUCode As String = "LRUCode"
			 Public Const FaultWord As String = "FaultWord"
			 Public Const Description As String = "Description"
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
			SyncLock GetType(ARINC429LRUCodesTemplateMetadata)
			
				If ARINC429LRUCodesTemplateMetadata.mapDelegates Is Nothing Then
					ARINC429LRUCodesTemplateMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If ARINC429LRUCodesTemplateMetadata._meta Is Nothing Then
					ARINC429LRUCodesTemplateMetadata._meta = New ARINC429LRUCodesTemplateMetadata()
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
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("EngineModel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LRUCode", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("FaultWord", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "ARINC429LRUCodesTemplate"
				meta.Destination = "ARINC429LRUCodesTemplate"
				
				meta.spInsert = "proc_ARINC429LRUCodesTemplateInsert"
				meta.spUpdate = "proc_ARINC429LRUCodesTemplateUpdate"
				meta.spDelete = "proc_ARINC429LRUCodesTemplateDelete"
				meta.spLoadAll = "proc_ARINC429LRUCodesTemplateLoadAll"
				meta.spLoadByPrimaryKey = "proc_ARINC429LRUCodesTemplateLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As ARINC429LRUCodesTemplateMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

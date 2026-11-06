
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 1/7/2014 9:02:58
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
	' Encapsulates the 'UUT_Generic_Info' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTGenericInfo))> _
	<XmlType("UUTGenericInfo")> _	
	Partial Public Class UUTGenericInfo 
		Inherits esUUTGenericInfo
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTGenericInfo()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal engineModel As System.String, ByVal tableName As System.String, ByVal xValue As System.Double)
			Dim obj As New UUTGenericInfo()
			obj.EngineModel = engineModel
			obj.TableName = tableName
			obj.XValue = xValue
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal engineModel As System.String, ByVal tableName As System.String, ByVal xValue As System.Double, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTGenericInfo()
			obj.EngineModel = engineModel
			obj.TableName = tableName
			obj.XValue = xValue
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTGenericInfoCollection")> _
	Partial Public Class UUTGenericInfoCollection
		Inherits esUUTGenericInfoCollection
		Implements IEnumerable(Of UUTGenericInfo)
	
		Public Function FindByPrimaryKey(ByVal engineModel As System.String, ByVal tableName As System.String, ByVal xValue As System.Double) As UUTGenericInfo
			Return MyBase.SingleOrDefault(Function(e) e.EngineModel = engineModel And e.TableName = tableName And e.XValue.HasValue AndAlso e.XValue.Value = xValue)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTGenericInfo))> _
		Public Class UUTGenericInfoCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTGenericInfoCollection)
			
			Public Shared Widening Operator CType(packet As UUTGenericInfoCollectionWCFPacket) As UUTGenericInfoCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTGenericInfoCollection) As UUTGenericInfoCollectionWCFPacket
				Return New UUTGenericInfoCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTGenericInfoQuery 
		Inherits esUUTGenericInfoQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTGenericInfoQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTGenericInfoQuery) As String
			Return UUTGenericInfoQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTGenericInfoQuery
			Return DirectCast(UUTGenericInfoQuery.SerializeHelper.FromXml(query, GetType(UUTGenericInfoQuery)), UUTGenericInfoQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTGenericInfo
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal engineModel As System.String, ByVal tableName As System.String, ByVal xValue As System.Double) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineModel, tableName, xValue)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineModel, tableName, xValue)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal engineModel As System.String, ByVal tableName As System.String, ByVal xValue As System.Double) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineModel, tableName, xValue)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineModel, tableName, xValue)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal engineModel As System.String, ByVal tableName As System.String, ByVal xValue As System.Double) As Boolean
		
			Dim query As New UUTGenericInfoQuery()
			query.Where(query.EngineModel = engineModel And query.TableName = tableName And query.XValue = xValue)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal engineModel As System.String, ByVal tableName As System.String, ByVal xValue As System.Double) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("EngineModel", engineModel)
						parms.Add("TableName", tableName)
						parms.Add("XValue", xValue)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_Generic_Info.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UUTGenericInfoMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTGenericInfoMetadata.ColumnNames.EngineModel, value) Then
					OnPropertyChanged(UUTGenericInfoMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Generic_Info.TableName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TableName As System.String
			Get
				Return MyBase.GetSystemString(UUTGenericInfoMetadata.ColumnNames.TableName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTGenericInfoMetadata.ColumnNames.TableName, value) Then
					OnPropertyChanged(UUTGenericInfoMetadata.PropertyNames.TableName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Generic_Info.XValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property XValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UUTGenericInfoMetadata.ColumnNames.XValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UUTGenericInfoMetadata.ColumnNames.XValue, value) Then
					OnPropertyChanged(UUTGenericInfoMetadata.PropertyNames.XValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Generic_Info.YValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property YValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UUTGenericInfoMetadata.ColumnNames.YValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UUTGenericInfoMetadata.ColumnNames.YValue, value) Then
					OnPropertyChanged(UUTGenericInfoMetadata.PropertyNames.YValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_Generic_Info.TextValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TextValue As System.String
			Get
				Return MyBase.GetSystemString(UUTGenericInfoMetadata.ColumnNames.TextValue)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTGenericInfoMetadata.ColumnNames.TextValue, value) Then
					OnPropertyChanged(UUTGenericInfoMetadata.PropertyNames.TextValue)
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
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
						Case "TableName"
							Me.str().TableName = CType(value, string)
												
						Case "XValue"
							Me.str().XValue = CType(value, string)
												
						Case "YValue"
							Me.str().YValue = CType(value, string)
												
						Case "TextValue"
							Me.str().TextValue = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "XValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.XValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UUTGenericInfoMetadata.PropertyNames.XValue)
							End If
						
						Case "YValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.YValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UUTGenericInfoMetadata.PropertyNames.YValue)
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
		
			Public Sub New(ByVal entity As esUUTGenericInfo)
				Me.entity = entity
			End Sub				
		
	
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
		  	
			Public Property TableName As System.String 
				Get
					Dim data_ As System.String = entity.TableName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TableName = Nothing
					Else
						entity.TableName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property XValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.XValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.XValue = Nothing
					Else
						entity.XValue = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property YValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.YValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.YValue = Nothing
					Else
						entity.YValue = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property TextValue As System.String 
				Get
					Dim data_ As System.String = entity.TextValue
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TextValue = Nothing
					Else
						entity.TextValue = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esUUTGenericInfo
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTGenericInfoMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTGenericInfoQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTGenericInfoQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTGenericInfoQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTGenericInfoQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTGenericInfoQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTGenericInfoCollection
		Inherits esEntityCollection(Of UUTGenericInfo)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTGenericInfoMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTGenericInfoCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTGenericInfoQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTGenericInfoQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTGenericInfoQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTGenericInfoQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTGenericInfoQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTGenericInfoQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTGenericInfoQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTGenericInfoQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTGenericInfoMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "EngineModel" 
					Return Me.EngineModel
				Case "TableName" 
					Return Me.TableName
				Case "XValue" 
					Return Me.XValue
				Case "YValue" 
					Return Me.YValue
				Case "TextValue" 
					Return Me.TextValue
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UUTGenericInfoMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TableName As esQueryItem
			Get
				Return New esQueryItem(Me, UUTGenericInfoMetadata.ColumnNames.TableName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property XValue As esQueryItem
			Get
				Return New esQueryItem(Me, UUTGenericInfoMetadata.ColumnNames.XValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property YValue As esQueryItem
			Get
				Return New esQueryItem(Me, UUTGenericInfoMetadata.ColumnNames.YValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property TextValue As esQueryItem
			Get
				Return New esQueryItem(Me, UUTGenericInfoMetadata.ColumnNames.TextValue, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTGenericInfo 
		Inherits esUUTGenericInfo
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class UUTGenericInfoMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTGenericInfoMetadata.ColumnNames.EngineModel, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTGenericInfoMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTGenericInfoMetadata.ColumnNames.TableName, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTGenericInfoMetadata.PropertyNames.TableName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTGenericInfoMetadata.ColumnNames.XValue, 2, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UUTGenericInfoMetadata.PropertyNames.XValue
			c.IsInPrimaryKey = True
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTGenericInfoMetadata.ColumnNames.YValue, 3, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UUTGenericInfoMetadata.PropertyNames.YValue
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTGenericInfoMetadata.ColumnNames.TextValue, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTGenericInfoMetadata.PropertyNames.TextValue
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTGenericInfoMetadata
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
			 Public Const EngineModel As String = "EngineModel"
			 Public Const TableName As String = "TableName"
			 Public Const XValue As String = "XValue"
			 Public Const YValue As String = "YValue"
			 Public Const TextValue As String = "TextValue"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const EngineModel As String = "EngineModel"
			 Public Const TableName As String = "TableName"
			 Public Const XValue As String = "XValue"
			 Public Const YValue As String = "YValue"
			 Public Const TextValue As String = "TextValue"
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
			SyncLock GetType(UUTGenericInfoMetadata)
			
				If UUTGenericInfoMetadata.mapDelegates Is Nothing Then
					UUTGenericInfoMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTGenericInfoMetadata._meta Is Nothing Then
					UUTGenericInfoMetadata._meta = New UUTGenericInfoMetadata()
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
				


				meta.AddTypeMap("EngineModel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("TableName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("XValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("YValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("TextValue", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "UUT_Generic_Info"
				meta.Destination = "UUT_Generic_Info"
				
				meta.spInsert = "proc_UUT_Generic_InfoInsert"
				meta.spUpdate = "proc_UUT_Generic_InfoUpdate"
				meta.spDelete = "proc_UUT_Generic_InfoDelete"
				meta.spLoadAll = "proc_UUT_Generic_InfoLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_Generic_InfoLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTGenericInfoMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

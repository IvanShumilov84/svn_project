
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 4/22/2015 12:06:35 PM
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
	' Encapsulates the 'DACN1_OmniBusBox_IOMGR' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(DACN1OmniBusBoxIOMGR))> _
	<XmlType("DACN1OmniBusBoxIOMGR")> _	
	Partial Public Class DACN1OmniBusBoxIOMGR 
		Inherits esDACN1OmniBusBoxIOMGR
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New DACN1OmniBusBoxIOMGR()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal engineModel As System.String, ByVal pwcPn As System.String)
			Dim obj As New DACN1OmniBusBoxIOMGR()
			obj.EngineModel = engineModel
			obj.PwcPn = pwcPn
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal engineModel As System.String, ByVal pwcPn As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New DACN1OmniBusBoxIOMGR()
			obj.EngineModel = engineModel
			obj.PwcPn = pwcPn
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("DACN1OmniBusBoxIOMGRCollection")> _
	Partial Public Class DACN1OmniBusBoxIOMGRCollection
		Inherits esDACN1OmniBusBoxIOMGRCollection
		Implements IEnumerable(Of DACN1OmniBusBoxIOMGR)
	
		Public Function FindByPrimaryKey(ByVal engineModel As System.String, ByVal pwcPn As System.String) As DACN1OmniBusBoxIOMGR
			Return MyBase.SingleOrDefault(Function(e) e.EngineModel = engineModel And e.PwcPn = pwcPn)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(DACN1OmniBusBoxIOMGR))> _
		Public Class DACN1OmniBusBoxIOMGRCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of DACN1OmniBusBoxIOMGRCollection)
			
			Public Shared Widening Operator CType(packet As DACN1OmniBusBoxIOMGRCollectionWCFPacket) As DACN1OmniBusBoxIOMGRCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As DACN1OmniBusBoxIOMGRCollection) As DACN1OmniBusBoxIOMGRCollectionWCFPacket
				Return New DACN1OmniBusBoxIOMGRCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class DACN1OmniBusBoxIOMGRQuery 
		Inherits esDACN1OmniBusBoxIOMGRQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "DACN1OmniBusBoxIOMGRQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As DACN1OmniBusBoxIOMGRQuery) As String
			Return DACN1OmniBusBoxIOMGRQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As DACN1OmniBusBoxIOMGRQuery
			Return DirectCast(DACN1OmniBusBoxIOMGRQuery.SerializeHelper.FromXml(query, GetType(DACN1OmniBusBoxIOMGRQuery)), DACN1OmniBusBoxIOMGRQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDACN1OmniBusBoxIOMGR
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal engineModel As System.String, ByVal pwcPn As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineModel, pwcPn)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineModel, pwcPn)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal engineModel As System.String, ByVal pwcPn As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineModel, pwcPn)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineModel, pwcPn)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal engineModel As System.String, ByVal pwcPn As System.String) As Boolean
		
			Dim query As New DACN1OmniBusBoxIOMGRQuery()
			query.Where(query.EngineModel = engineModel And query.PwcPn = pwcPn)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal engineModel As System.String, ByVal pwcPn As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("EngineModel", engineModel)
						parms.Add("PwcPn", pwcPn)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(DACN1OmniBusBoxIOMGRMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DACN1OmniBusBoxIOMGRMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToEngineInfoByEngineModel = Nothing
					Me.OnPropertyChanged("UpToEngineInfoByEngineModel")
					OnPropertyChanged(DACN1OmniBusBoxIOMGRMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR.PWC_PN
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PwcPn As System.String
			Get
				Return MyBase.GetSystemString(DACN1OmniBusBoxIOMGRMetadata.ColumnNames.PwcPn)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DACN1OmniBusBoxIOMGRMetadata.ColumnNames.PwcPn, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRMetadata.PropertyNames.PwcPn)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToEngineInfoByEngineModel As EngineInfo
		
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
												
						Case "PwcPn"
							Me.str().PwcPn = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
					
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
		
			Public Sub New(ByVal entity As esDACN1OmniBusBoxIOMGR)
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
		  	
			Public Property PwcPn As System.String 
				Get
					Dim data_ As System.String = entity.PwcPn
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PwcPn = Nothing
					Else
						entity.PwcPn = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esDACN1OmniBusBoxIOMGR
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DACN1OmniBusBoxIOMGRMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As DACN1OmniBusBoxIOMGRQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DACN1OmniBusBoxIOMGRQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As DACN1OmniBusBoxIOMGRQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As DACN1OmniBusBoxIOMGRQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As DACN1OmniBusBoxIOMGRQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDACN1OmniBusBoxIOMGRCollection
		Inherits esEntityCollection(Of DACN1OmniBusBoxIOMGR)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DACN1OmniBusBoxIOMGRMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "DACN1OmniBusBoxIOMGRCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As DACN1OmniBusBoxIOMGRQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DACN1OmniBusBoxIOMGRQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As DACN1OmniBusBoxIOMGRQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New DACN1OmniBusBoxIOMGRQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As DACN1OmniBusBoxIOMGRQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, DACN1OmniBusBoxIOMGRQuery))
		End Sub
		
		#End Region
						
		Private m_query As DACN1OmniBusBoxIOMGRQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDACN1OmniBusBoxIOMGRQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return DACN1OmniBusBoxIOMGRMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "EngineModel" 
					Return Me.EngineModel
				Case "PwcPn" 
					Return Me.PwcPn
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property PwcPn As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRMetadata.ColumnNames.PwcPn, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class DACN1OmniBusBoxIOMGR 
		Inherits esDACN1OmniBusBoxIOMGR
		
	
		#Region "DACN1OmniBusBoxIOMGRUartCollectionByEngineModel - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_DACN1OmniBusBoxIOMGRUartCollectionByEngineModel() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.DACN1OmniBusBoxIOMGR.DACN1OmniBusBoxIOMGRUartCollectionByEngineModel_Delegate)
				map.PropertyName = "DACN1OmniBusBoxIOMGRUartCollectionByEngineModel"
				map.MyColumnName = "EngineModel,PWC_PN"
				map.ParentColumnName = "EngineModel,PWC_PN"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub DACN1OmniBusBoxIOMGRUartCollectionByEngineModel_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New DACN1OmniBusBoxIOMGRQuery(data.NextAlias())
			
			Dim mee As DACN1OmniBusBoxIOMGRUartQuery = If(data.You IsNot Nothing, TryCast(data.You, DACN1OmniBusBoxIOMGRUartQuery), New DACN1OmniBusBoxIOMGRUartQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.EngineModel = mee.EngineModel And parent.PwcPn = mee.PwcPn)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - DACN1_OmniBusBox_IOMGRDACN1_OmniBusBox_IOMGR_Uart
		''' </summary>

		<XmlIgnore()> _ 
		Public Property DACN1OmniBusBoxIOMGRUartCollectionByEngineModel As DACN1OmniBusBoxIOMGRUartCollection 
		
			Get
				If Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel Is Nothing Then
					Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel = New DACN1OmniBusBoxIOMGRUartCollection()
					Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("DACN1OmniBusBoxIOMGRUartCollectionByEngineModel", Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel)
				
					If Not Me.EngineModel.Equals(Nothing) AndAlso Not Me.PwcPn.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel.Query.Where(Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel.Query.EngineModel = Me.EngineModel)
							Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel.Query.Where(Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel.Query.PwcPn = Me.PwcPn)
							Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel.fks.Add(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.EngineModel, Me.EngineModel)
						Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel.fks.Add(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.PwcPn, Me.PwcPn)
					End If
				End If

				Return Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel
			End Get
			
			Set(ByVal value As DACN1OmniBusBoxIOMGRUartCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel Is Nothing Then

					Me.RemovePostSave("DACN1OmniBusBoxIOMGRUartCollectionByEngineModel")
					Me._DACN1OmniBusBoxIOMGRUartCollectionByEngineModel = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _DACN1OmniBusBoxIOMGRUartCollectionByEngineModel As DACN1OmniBusBoxIOMGRUartCollection
		#End Region

		#Region "UpToEngineInfoByEngineModel - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Engine_InfoDACN1_OmniBusBox_IOMGR
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToEngineInfoByEngineModel As EngineInfo
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToEngineInfoByEngineModel Is Nothing _
						 AndAlso Not EngineModel.Equals(Nothing)  Then
						
					Me._UpToEngineInfoByEngineModel = New EngineInfo()
					Me._UpToEngineInfoByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToEngineInfoByEngineModel", Me._UpToEngineInfoByEngineModel)
					Me._UpToEngineInfoByEngineModel.Query.Where(Me._UpToEngineInfoByEngineModel.Query.EngineModel = Me.EngineModel)
					Me._UpToEngineInfoByEngineModel.Query.Load()
				End If

				Return Me._UpToEngineInfoByEngineModel
			End Get
			
            Set(ByVal value As EngineInfo)
				Me.RemovePreSave("UpToEngineInfoByEngineModel")
				

				If value Is Nothing Then
				
					Me.EngineModel = Nothing
				
					Me._UpToEngineInfoByEngineModel = Nothing
				Else
				
					Me.EngineModel = value.EngineModel
					
					Me._UpToEngineInfoByEngineModel = value
					Me.SetPreSave("UpToEngineInfoByEngineModel", Me._UpToEngineInfoByEngineModel)
				End If
				
			End Set	

		End Property
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "DACN1OmniBusBoxIOMGRUartCollectionByEngineModel"
					coll = Me.DACN1OmniBusBoxIOMGRUartCollectionByEngineModel
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "DACN1OmniBusBoxIOMGRUartCollectionByEngineModel", GetType(DACN1OmniBusBoxIOMGRUartCollection), New DACN1OmniBusBoxIOMGRUart()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class DACN1OmniBusBoxIOMGRMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRMetadata.ColumnNames.EngineModel, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = DACN1OmniBusBoxIOMGRMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRMetadata.ColumnNames.PwcPn, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = DACN1OmniBusBoxIOMGRMetadata.PropertyNames.PwcPn
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As DACN1OmniBusBoxIOMGRMetadata
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
			 Public Const PwcPn As String = "PWC_PN"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const EngineModel As String = "EngineModel"
			 Public Const PwcPn As String = "PwcPn"
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
			SyncLock GetType(DACN1OmniBusBoxIOMGRMetadata)
			
				If DACN1OmniBusBoxIOMGRMetadata.mapDelegates Is Nothing Then
					DACN1OmniBusBoxIOMGRMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If DACN1OmniBusBoxIOMGRMetadata._meta Is Nothing Then
					DACN1OmniBusBoxIOMGRMetadata._meta = New DACN1OmniBusBoxIOMGRMetadata()
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
				meta.AddTypeMap("PwcPn", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "DACN1_OmniBusBox_IOMGR"
				meta.Destination = "DACN1_OmniBusBox_IOMGR"
				
				meta.spInsert = "proc_DACN1_OmniBusBox_IOMGRInsert"
				meta.spUpdate = "proc_DACN1_OmniBusBox_IOMGRUpdate"
				meta.spDelete = "proc_DACN1_OmniBusBox_IOMGRDelete"
				meta.spLoadAll = "proc_DACN1_OmniBusBox_IOMGRLoadAll"
				meta.spLoadByPrimaryKey = "proc_DACN1_OmniBusBox_IOMGRLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As DACN1OmniBusBoxIOMGRMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

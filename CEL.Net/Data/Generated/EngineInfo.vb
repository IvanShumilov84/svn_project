
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 2015-10-15 9:48:38 AM
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
	' Encapsulates the 'Engine_Info' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(EngineInfo))> _
	<XmlType("EngineInfo")> _	
	Partial Public Class EngineInfo 
		Inherits esEngineInfo
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New EngineInfo()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal engineModel As System.String)
			Dim obj As New EngineInfo()
			obj.EngineModel = engineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal engineModel As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New EngineInfo()
			obj.EngineModel = engineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("EngineInfoCollection")> _
	Partial Public Class EngineInfoCollection
		Inherits esEngineInfoCollection
		Implements IEnumerable(Of EngineInfo)
	
		Public Function FindByPrimaryKey(ByVal engineModel As System.String) As EngineInfo
			Return MyBase.SingleOrDefault(Function(e) e.EngineModel = engineModel)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(EngineInfo))> _
		Public Class EngineInfoCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of EngineInfoCollection)
			
			Public Shared Widening Operator CType(packet As EngineInfoCollectionWCFPacket) As EngineInfoCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As EngineInfoCollection) As EngineInfoCollectionWCFPacket
				Return New EngineInfoCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class EngineInfoQuery 
		Inherits esEngineInfoQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "EngineInfoQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As EngineInfoQuery) As String
			Return EngineInfoQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As EngineInfoQuery
			Return DirectCast(EngineInfoQuery.SerializeHelper.FromXml(query, GetType(EngineInfoQuery)), EngineInfoQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esEngineInfo
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal engineModel As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineModel)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineModel)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal engineModel As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineModel)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineModel)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal engineModel As System.String) As Boolean
		
			Dim query As New EngineInfoQuery()
			query.Where(query.EngineModel = engineModel)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal engineModel As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("EngineModel", engineModel)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to Engine_Info.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(EngineInfoMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EngineInfoMetadata.ColumnNames.EngineModel, value) Then
					OnPropertyChanged(EngineInfoMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Engine_Info.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(EngineInfoMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(EngineInfoMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(EngineInfoMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to Engine_Info.ManufacturerID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ManufacturerID As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(EngineInfoMetadata.ColumnNames.ManufacturerID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(EngineInfoMetadata.ColumnNames.ManufacturerID, value) Then
					Me._UpToEngineManufacturerByManufacturerID = Nothing
					Me.OnPropertyChanged("UpToEngineManufacturerByManufacturerID")
					OnPropertyChanged(EngineInfoMetadata.PropertyNames.ManufacturerID)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToEngineManufacturerByManufacturerID As EngineManufacturer
		
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
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "ManufacturerID"
							Me.str().ManufacturerID = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ManufacturerID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.ManufacturerID = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(EngineInfoMetadata.PropertyNames.ManufacturerID)
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
		
			Public Sub New(ByVal entity As esEngineInfo)
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
		  	
			Public Property ManufacturerID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.ManufacturerID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ManufacturerID = Nothing
					Else
						entity.ManufacturerID = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  

			Private entity As esEngineInfo
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return EngineInfoMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As EngineInfoQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New EngineInfoQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As EngineInfoQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As EngineInfoQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As EngineInfoQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esEngineInfoCollection
		Inherits esEntityCollection(Of EngineInfo)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return EngineInfoMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "EngineInfoCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As EngineInfoQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New EngineInfoQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As EngineInfoQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New EngineInfoQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As EngineInfoQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, EngineInfoQuery))
		End Sub
		
		#End Region
						
		Private m_query As EngineInfoQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esEngineInfoQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return EngineInfoMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "EngineModel" 
					Return Me.EngineModel
				Case "Description" 
					Return Me.Description
				Case "ManufacturerID" 
					Return Me.ManufacturerID
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, EngineInfoMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, EngineInfoMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ManufacturerID As esQueryItem
			Get
				Return New esQueryItem(Me, EngineInfoMetadata.ColumnNames.ManufacturerID, esSystemType.Int16)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class EngineInfo 
		Inherits esEngineInfo
		
	
		#Region "DACN1OmniBusBoxIOMGRCollectionByEngineModel - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_DACN1OmniBusBoxIOMGRCollectionByEngineModel() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.EngineInfo.DACN1OmniBusBoxIOMGRCollectionByEngineModel_Delegate)
				map.PropertyName = "DACN1OmniBusBoxIOMGRCollectionByEngineModel"
				map.MyColumnName = "EngineModel"
				map.ParentColumnName = "EngineModel"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub DACN1OmniBusBoxIOMGRCollectionByEngineModel_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New EngineInfoQuery(data.NextAlias())
			
			Dim mee As DACN1OmniBusBoxIOMGRQuery = If(data.You IsNot Nothing, TryCast(data.You, DACN1OmniBusBoxIOMGRQuery), New DACN1OmniBusBoxIOMGRQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Engine_InfoDACN1_OmniBusBox_IOMGR
		''' </summary>

		<XmlIgnore()> _ 
		Public Property DACN1OmniBusBoxIOMGRCollectionByEngineModel As DACN1OmniBusBoxIOMGRCollection 
		
			Get
				If Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel Is Nothing Then
					Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel = New DACN1OmniBusBoxIOMGRCollection()
					Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("DACN1OmniBusBoxIOMGRCollectionByEngineModel", Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel)
				
					If Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel.Query.Where(Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel.Query.EngineModel = Me.EngineModel)
							Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel.fks.Add(DACN1OmniBusBoxIOMGRMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel
			End Get
			
			Set(ByVal value As DACN1OmniBusBoxIOMGRCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel Is Nothing Then

					Me.RemovePostSave("DACN1OmniBusBoxIOMGRCollectionByEngineModel")
					Me._DACN1OmniBusBoxIOMGRCollectionByEngineModel = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _DACN1OmniBusBoxIOMGRCollectionByEngineModel As DACN1OmniBusBoxIOMGRCollection
		#End Region

		#Region "EnginePwBuildSpecificationCollectionByEngineModel - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_EnginePwBuildSpecificationCollectionByEngineModel() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.EngineInfo.EnginePwBuildSpecificationCollectionByEngineModel_Delegate)
				map.PropertyName = "EnginePwBuildSpecificationCollectionByEngineModel"
				map.MyColumnName = "EngineModel"
				map.ParentColumnName = "EngineModel"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub EnginePwBuildSpecificationCollectionByEngineModel_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New EngineInfoQuery(data.NextAlias())
			
			Dim mee As EnginePwBuildSpecificationQuery = If(data.You IsNot Nothing, TryCast(data.You, EnginePwBuildSpecificationQuery), New EnginePwBuildSpecificationQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Engine_InfoEngine_PwBuildSpecification
		''' </summary>

		<XmlIgnore()> _ 
		Public Property EnginePwBuildSpecificationCollectionByEngineModel As EnginePwBuildSpecificationCollection 
		
			Get
				If Me._EnginePwBuildSpecificationCollectionByEngineModel Is Nothing Then
					Me._EnginePwBuildSpecificationCollectionByEngineModel = New EnginePwBuildSpecificationCollection()
					Me._EnginePwBuildSpecificationCollectionByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("EnginePwBuildSpecificationCollectionByEngineModel", Me._EnginePwBuildSpecificationCollectionByEngineModel)
				
					If Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._EnginePwBuildSpecificationCollectionByEngineModel.Query.Where(Me._EnginePwBuildSpecificationCollectionByEngineModel.Query.EngineModel = Me.EngineModel)
							Me._EnginePwBuildSpecificationCollectionByEngineModel.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._EnginePwBuildSpecificationCollectionByEngineModel.fks.Add(EnginePwBuildSpecificationMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._EnginePwBuildSpecificationCollectionByEngineModel
			End Get
			
			Set(ByVal value As EnginePwBuildSpecificationCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._EnginePwBuildSpecificationCollectionByEngineModel Is Nothing Then

					Me.RemovePostSave("EnginePwBuildSpecificationCollectionByEngineModel")
					Me._EnginePwBuildSpecificationCollectionByEngineModel = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _EnginePwBuildSpecificationCollectionByEngineModel As EnginePwBuildSpecificationCollection
		#End Region

		#Region "EnginePwServiceBulletinCollectionByEngineModel - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_EnginePwServiceBulletinCollectionByEngineModel() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.EngineInfo.EnginePwServiceBulletinCollectionByEngineModel_Delegate)
				map.PropertyName = "EnginePwServiceBulletinCollectionByEngineModel"
				map.MyColumnName = "EngineModel"
				map.ParentColumnName = "EngineModel"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub EnginePwServiceBulletinCollectionByEngineModel_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New EngineInfoQuery(data.NextAlias())
			
			Dim mee As EnginePwServiceBulletinQuery = If(data.You IsNot Nothing, TryCast(data.You, EnginePwServiceBulletinQuery), New EnginePwServiceBulletinQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Engine_InfoEngine_PwServiceBulletin
		''' </summary>

		<XmlIgnore()> _ 
		Public Property EnginePwServiceBulletinCollectionByEngineModel As EnginePwServiceBulletinCollection 
		
			Get
				If Me._EnginePwServiceBulletinCollectionByEngineModel Is Nothing Then
					Me._EnginePwServiceBulletinCollectionByEngineModel = New EnginePwServiceBulletinCollection()
					Me._EnginePwServiceBulletinCollectionByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("EnginePwServiceBulletinCollectionByEngineModel", Me._EnginePwServiceBulletinCollectionByEngineModel)
				
					If Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._EnginePwServiceBulletinCollectionByEngineModel.Query.Where(Me._EnginePwServiceBulletinCollectionByEngineModel.Query.EngineModel = Me.EngineModel)
							Me._EnginePwServiceBulletinCollectionByEngineModel.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._EnginePwServiceBulletinCollectionByEngineModel.fks.Add(EnginePwServiceBulletinMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._EnginePwServiceBulletinCollectionByEngineModel
			End Get
			
			Set(ByVal value As EnginePwServiceBulletinCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._EnginePwServiceBulletinCollectionByEngineModel Is Nothing Then

					Me.RemovePostSave("EnginePwServiceBulletinCollectionByEngineModel")
					Me._EnginePwServiceBulletinCollectionByEngineModel = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _EnginePwServiceBulletinCollectionByEngineModel As EnginePwServiceBulletinCollection
		#End Region

		#Region "UpToGTCP8598RepairTypeCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - Engine_InfoGTCP85_98
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToGTCP8598RepairTypeCollection As GTCP8598RepairTypeCollection
		
			Get
				If Me._UpToGTCP8598RepairTypeCollection Is Nothing Then
					Me._UpToGTCP8598RepairTypeCollection = New GTCP8598RepairTypeCollection()
					Me._UpToGTCP8598RepairTypeCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToGTCP8598RepairTypeCollection", Me._UpToGTCP8598RepairTypeCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.EngineModel.Equals(Nothing) Then 
				
						Dim m As New GTCP8598RepairTypeQuery("m")
						Dim j As New Gtcp8598Query("j")
						m.Select(m)
						m.InnerJoin(j).On(m.Id = j.RepairTypeID)
                        m.Where(j.EngineModel = Me.EngineModel)

						Me._UpToGTCP8598RepairTypeCollection.Load(m)

					End If
				End If

				Return Me._UpToGTCP8598RepairTypeCollection
			End Get
			
			Set(ByVal value As GTCP8598RepairTypeCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToGTCP8598RepairTypeCollection Is Nothing Then

					Me.RemovePostSave("UpToGTCP8598RepairTypeCollection")
					Me._UpToGTCP8598RepairTypeCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - Engine_InfoGTCP85_98
		''' </summary>
		Public Sub AssociateGTCP8598RepairTypeCollection(entity As GTCP8598RepairType)
			If Me._Gtcp8598Collection Is Nothing Then
				Me._Gtcp8598Collection = New Gtcp8598Collection()
				Me._Gtcp8598Collection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("Gtcp8598Collection", Me._Gtcp8598Collection)
			End If
			
			Dim obj As Gtcp8598 = Me._Gtcp8598Collection.AddNew()
			obj.EngineModel = Me.EngineModel
			obj.RepairTypeID = entity.Id			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - Engine_InfoGTCP85_98
		''' </summary>
		Public Sub DissociateGTCP8598RepairTypeCollection(entity As GTCP8598RepairType)
			If Me._Gtcp8598Collection Is Nothing Then
				Me._Gtcp8598Collection = new Gtcp8598Collection()
				Me._Gtcp8598Collection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("Gtcp8598Collection", Me._Gtcp8598Collection)
			End If

			Dim obj As Gtcp8598 = Me._Gtcp8598Collection.AddNew()
			obj.EngineModel = Me.EngineModel
            obj.RepairTypeID = entity.Id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToGTCP8598RepairTypeCollection As GTCP8598RepairTypeCollection
		Private _Gtcp8598Collection As Gtcp8598Collection
		#End Region

		#Region "Gtcp8598CollectionByEngineModel - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_Gtcp8598CollectionByEngineModel() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.EngineInfo.Gtcp8598CollectionByEngineModel_Delegate)
				map.PropertyName = "Gtcp8598CollectionByEngineModel"
				map.MyColumnName = "EngineModel"
				map.ParentColumnName = "EngineModel"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub Gtcp8598CollectionByEngineModel_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New EngineInfoQuery(data.NextAlias())
			
			Dim mee As Gtcp8598Query = If(data.You IsNot Nothing, TryCast(data.You, Gtcp8598Query), New Gtcp8598Query(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Engine_InfoGTCP85_98
		''' </summary>

		<XmlIgnore()> _ 
		Public Property Gtcp8598CollectionByEngineModel As Gtcp8598Collection 
		
			Get
				If Me._Gtcp8598CollectionByEngineModel Is Nothing Then
					Me._Gtcp8598CollectionByEngineModel = New Gtcp8598Collection()
					Me._Gtcp8598CollectionByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("Gtcp8598CollectionByEngineModel", Me._Gtcp8598CollectionByEngineModel)
				
					If Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._Gtcp8598CollectionByEngineModel.Query.Where(Me._Gtcp8598CollectionByEngineModel.Query.EngineModel = Me.EngineModel)
							Me._Gtcp8598CollectionByEngineModel.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._Gtcp8598CollectionByEngineModel.fks.Add(Gtcp8598Metadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._Gtcp8598CollectionByEngineModel
			End Get
			
			Set(ByVal value As Gtcp8598Collection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._Gtcp8598CollectionByEngineModel Is Nothing Then

					Me.RemovePostSave("Gtcp8598CollectionByEngineModel")
					Me._Gtcp8598CollectionByEngineModel = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _Gtcp8598CollectionByEngineModel As Gtcp8598Collection
		#End Region

		#Region "UpToHMIPageMappingCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - Engine_InfoUUT
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToHMIPageMappingCollection As HMIPageMappingCollection
		
			Get
				If Me._UpToHMIPageMappingCollection Is Nothing Then
					Me._UpToHMIPageMappingCollection = New HMIPageMappingCollection()
					Me._UpToHMIPageMappingCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToHMIPageMappingCollection", Me._UpToHMIPageMappingCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.EngineModel.Equals(Nothing) Then 
				
						Dim m As New HMIPageMappingQuery("m")
						Dim j As New UutQuery("j")
						m.Select(m)
						m.InnerJoin(j).On(m.ProjectID = j.ProjectID)
                        m.Where(j.EngineModel = Me.EngineModel)

						Me._UpToHMIPageMappingCollection.Load(m)

					End If
				End If

				Return Me._UpToHMIPageMappingCollection
			End Get
			
			Set(ByVal value As HMIPageMappingCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToHMIPageMappingCollection Is Nothing Then

					Me.RemovePostSave("UpToHMIPageMappingCollection")
					Me._UpToHMIPageMappingCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - Engine_InfoUUT
		''' </summary>
		Public Sub AssociateHMIPageMappingCollection(entity As HMIPageMapping)
			If Me._UutCollection Is Nothing Then
				Me._UutCollection = New UutCollection()
				Me._UutCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UutCollection", Me._UutCollection)
			End If
			
			Dim obj As Uut = Me._UutCollection.AddNew()
			obj.EngineModel = Me.EngineModel
			obj.ProjectID = entity.ProjectID			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - Engine_InfoUUT
		''' </summary>
		Public Sub DissociateHMIPageMappingCollection(entity As HMIPageMapping)
			If Me._UutCollection Is Nothing Then
				Me._UutCollection = new UutCollection()
				Me._UutCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UutCollection", Me._UutCollection)
			End If

			Dim obj As Uut = Me._UutCollection.AddNew()
			obj.EngineModel = Me.EngineModel
            obj.ProjectID = entity.ProjectID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToHMIPageMappingCollection As HMIPageMappingCollection
		Private _UutCollection As UutCollection
		#End Region

		#Region "UutCollectionByEngineModel - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UutCollectionByEngineModel() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.EngineInfo.UutCollectionByEngineModel_Delegate)
				map.PropertyName = "UutCollectionByEngineModel"
				map.MyColumnName = "EngineModel"
				map.ParentColumnName = "EngineModel"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub UutCollectionByEngineModel_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New EngineInfoQuery(data.NextAlias())
			
			Dim mee As UutQuery = If(data.You IsNot Nothing, TryCast(data.You, UutQuery), New UutQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Engine_InfoUUT
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UutCollectionByEngineModel As UutCollection 
		
			Get
				If Me._UutCollectionByEngineModel Is Nothing Then
					Me._UutCollectionByEngineModel = New UutCollection()
					Me._UutCollectionByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UutCollectionByEngineModel", Me._UutCollectionByEngineModel)
				
					If Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UutCollectionByEngineModel.Query.Where(Me._UutCollectionByEngineModel.Query.EngineModel = Me.EngineModel)
							Me._UutCollectionByEngineModel.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UutCollectionByEngineModel.fks.Add(UutMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._UutCollectionByEngineModel
			End Get
			
			Set(ByVal value As UutCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UutCollectionByEngineModel Is Nothing Then

					Me.RemovePostSave("UutCollectionByEngineModel")
					Me._UutCollectionByEngineModel = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UutCollectionByEngineModel As UutCollection
		#End Region

		#Region "UpToEngineSeriesCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - Engine_InfoUUT_Engine_Series
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToEngineSeriesCollection As EngineSeriesCollection
		
			Get
				If Me._UpToEngineSeriesCollection Is Nothing Then
					Me._UpToEngineSeriesCollection = New EngineSeriesCollection()
					Me._UpToEngineSeriesCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToEngineSeriesCollection", Me._UpToEngineSeriesCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.EngineModel.Equals(Nothing) Then 
				
						Dim m As New EngineSeriesQuery("m")
						Dim j As New UUTEngineSeriesQuery("j")
						m.Select(m)
						m.InnerJoin(j).On(m.EngineSeries = j.EngineSeries)
                        m.Where(j.EngineModel = Me.EngineModel)

						Me._UpToEngineSeriesCollection.Load(m)

					End If
				End If

				Return Me._UpToEngineSeriesCollection
			End Get
			
			Set(ByVal value As EngineSeriesCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToEngineSeriesCollection Is Nothing Then

					Me.RemovePostSave("UpToEngineSeriesCollection")
					Me._UpToEngineSeriesCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - Engine_InfoUUT_Engine_Series
		''' </summary>
		Public Sub AssociateEngineSeriesCollection(entity As EngineSeries)
			If Me._UUTEngineSeriesCollection Is Nothing Then
				Me._UUTEngineSeriesCollection = New UUTEngineSeriesCollection()
				Me._UUTEngineSeriesCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UUTEngineSeriesCollection", Me._UUTEngineSeriesCollection)
			End If
			
			Dim obj As UUTEngineSeries = Me._UUTEngineSeriesCollection.AddNew()
			obj.EngineModel = Me.EngineModel
			obj.EngineSeries = entity.EngineSeries			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - Engine_InfoUUT_Engine_Series
		''' </summary>
		Public Sub DissociateEngineSeriesCollection(entity As EngineSeries)
			If Me._UUTEngineSeriesCollection Is Nothing Then
				Me._UUTEngineSeriesCollection = new UUTEngineSeriesCollection()
				Me._UUTEngineSeriesCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UUTEngineSeriesCollection", Me._UUTEngineSeriesCollection)
			End If

			Dim obj As UUTEngineSeries = Me._UUTEngineSeriesCollection.AddNew()
			obj.EngineModel = Me.EngineModel
            obj.EngineSeries = entity.EngineSeries
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToEngineSeriesCollection As EngineSeriesCollection
		Private _UUTEngineSeriesCollection As UUTEngineSeriesCollection
		#End Region

		#Region "UUTEngineSeriesCollectionByEngineModel - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTEngineSeriesCollectionByEngineModel() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.EngineInfo.UUTEngineSeriesCollectionByEngineModel_Delegate)
				map.PropertyName = "UUTEngineSeriesCollectionByEngineModel"
				map.MyColumnName = "EngineModel"
				map.ParentColumnName = "EngineModel"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub UUTEngineSeriesCollectionByEngineModel_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New EngineInfoQuery(data.NextAlias())
			
			Dim mee As UUTEngineSeriesQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTEngineSeriesQuery), New UUTEngineSeriesQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Engine_InfoUUT_Engine_Series
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTEngineSeriesCollectionByEngineModel As UUTEngineSeriesCollection 
		
			Get
				If Me._UUTEngineSeriesCollectionByEngineModel Is Nothing Then
					Me._UUTEngineSeriesCollectionByEngineModel = New UUTEngineSeriesCollection()
					Me._UUTEngineSeriesCollectionByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTEngineSeriesCollectionByEngineModel", Me._UUTEngineSeriesCollectionByEngineModel)
				
					If Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTEngineSeriesCollectionByEngineModel.Query.Where(Me._UUTEngineSeriesCollectionByEngineModel.Query.EngineModel = Me.EngineModel)
							Me._UUTEngineSeriesCollectionByEngineModel.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTEngineSeriesCollectionByEngineModel.fks.Add(UUTEngineSeriesMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._UUTEngineSeriesCollectionByEngineModel
			End Get
			
			Set(ByVal value As UUTEngineSeriesCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTEngineSeriesCollectionByEngineModel Is Nothing Then

					Me.RemovePostSave("UUTEngineSeriesCollectionByEngineModel")
					Me._UUTEngineSeriesCollectionByEngineModel = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTEngineSeriesCollectionByEngineModel As UUTEngineSeriesCollection
		#End Region

		#Region "UpToEngineManufacturerByManufacturerID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Engine_ManufacturerEngine_Info
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToEngineManufacturerByManufacturerID As EngineManufacturer
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToEngineManufacturerByManufacturerID Is Nothing _
						 AndAlso Not ManufacturerID.Equals(Nothing)  Then
						
					Me._UpToEngineManufacturerByManufacturerID = New EngineManufacturer()
					Me._UpToEngineManufacturerByManufacturerID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToEngineManufacturerByManufacturerID", Me._UpToEngineManufacturerByManufacturerID)
					Me._UpToEngineManufacturerByManufacturerID.Query.Where(Me._UpToEngineManufacturerByManufacturerID.Query.Id = Me.ManufacturerID)
					Me._UpToEngineManufacturerByManufacturerID.Query.Load()
				End If

				Return Me._UpToEngineManufacturerByManufacturerID
			End Get
			
            Set(ByVal value As EngineManufacturer)
				Me.RemovePreSave("UpToEngineManufacturerByManufacturerID")
				

				If value Is Nothing Then
				
					Me.ManufacturerID = Nothing
				
					Me._UpToEngineManufacturerByManufacturerID = Nothing
				Else
				
					Me.ManufacturerID = value.Id
					
					Me._UpToEngineManufacturerByManufacturerID = value
					Me.SetPreSave("UpToEngineManufacturerByManufacturerID", Me._UpToEngineManufacturerByManufacturerID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "DACN1OmniBusBoxIOMGRCollectionByEngineModel"
					coll = Me.DACN1OmniBusBoxIOMGRCollectionByEngineModel
					Exit Select
				Case "EnginePwBuildSpecificationCollectionByEngineModel"
					coll = Me.EnginePwBuildSpecificationCollectionByEngineModel
					Exit Select
				Case "EnginePwServiceBulletinCollectionByEngineModel"
					coll = Me.EnginePwServiceBulletinCollectionByEngineModel
					Exit Select
				Case "Gtcp8598CollectionByEngineModel"
					coll = Me.Gtcp8598CollectionByEngineModel
					Exit Select
				Case "UutCollectionByEngineModel"
					coll = Me.UutCollectionByEngineModel
					Exit Select
				Case "UUTEngineSeriesCollectionByEngineModel"
					coll = Me.UUTEngineSeriesCollectionByEngineModel
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "DACN1OmniBusBoxIOMGRCollectionByEngineModel", GetType(DACN1OmniBusBoxIOMGRCollection), New DACN1OmniBusBoxIOMGR()))
			props.Add(new esPropertyDescriptor(Me, "EnginePwBuildSpecificationCollectionByEngineModel", GetType(EnginePwBuildSpecificationCollection), New EnginePwBuildSpecification()))
			props.Add(new esPropertyDescriptor(Me, "EnginePwServiceBulletinCollectionByEngineModel", GetType(EnginePwServiceBulletinCollection), New EnginePwServiceBulletin()))
			props.Add(new esPropertyDescriptor(Me, "Gtcp8598CollectionByEngineModel", GetType(Gtcp8598Collection), New Gtcp8598()))
			props.Add(new esPropertyDescriptor(Me, "UutCollectionByEngineModel", GetType(UutCollection), New Uut()))
			props.Add(new esPropertyDescriptor(Me, "UUTEngineSeriesCollectionByEngineModel", GetType(UUTEngineSeriesCollection), New UUTEngineSeries()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class EngineInfoMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(EngineInfoMetadata.ColumnNames.EngineModel, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = EngineInfoMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			m_columns.Add(c)
				
			c = New esColumnMetadata(EngineInfoMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = EngineInfoMetadata.PropertyNames.Description
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(EngineInfoMetadata.ColumnNames.ManufacturerID, 2, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = EngineInfoMetadata.PropertyNames.ManufacturerID
			c.NumericPrecision = 5
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As EngineInfoMetadata
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
			 Public Const Description As String = "Description"
			 Public Const ManufacturerID As String = "ManufacturerID"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const EngineModel As String = "EngineModel"
			 Public Const Description As String = "Description"
			 Public Const ManufacturerID As String = "ManufacturerID"
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
			SyncLock GetType(EngineInfoMetadata)
			
				If EngineInfoMetadata.mapDelegates Is Nothing Then
					EngineInfoMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If EngineInfoMetadata._meta Is Nothing Then
					EngineInfoMetadata._meta = New EngineInfoMetadata()
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
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ManufacturerID", new esTypeMap("Integer", "System.Int16"))			
				
				
				 
				meta.Source = "Engine_Info"
				meta.Destination = "Engine_Info"
				
				meta.spInsert = "proc_Engine_InfoInsert"
				meta.spUpdate = "proc_Engine_InfoUpdate"
				meta.spDelete = "proc_Engine_InfoDelete"
				meta.spLoadAll = "proc_Engine_InfoLoadAll"
				meta.spLoadByPrimaryKey = "proc_Engine_InfoLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As EngineInfoMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

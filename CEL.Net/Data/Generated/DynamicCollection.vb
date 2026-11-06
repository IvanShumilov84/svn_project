
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 19/11/2013 1:09:25 PM
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
	' Encapsulates the 'Dynamic_Collection' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(DynamicCollection))> _
	<XmlType("DynamicCollection")> _	
	Partial Public Class DynamicCollection 
		Inherits esDynamicCollection
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New DynamicCollection()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal collectionName As System.String)
			Dim obj As New DynamicCollection()
			obj.CollectionName = collectionName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New DynamicCollection()
			obj.CollectionName = collectionName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("DynamicCollectionCollection")> _
	Partial Public Class DynamicCollectionCollection
		Inherits esDynamicCollectionCollection
		Implements IEnumerable(Of DynamicCollection)
	
		Public Function FindByPrimaryKey(ByVal collectionName As System.String) As DynamicCollection
			Return MyBase.SingleOrDefault(Function(e) e.CollectionName = collectionName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(DynamicCollection))> _
		Public Class DynamicCollectionCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of DynamicCollectionCollection)
			
			Public Shared Widening Operator CType(packet As DynamicCollectionCollectionWCFPacket) As DynamicCollectionCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As DynamicCollectionCollection) As DynamicCollectionCollectionWCFPacket
				Return New DynamicCollectionCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class DynamicCollectionQuery 
		Inherits esDynamicCollectionQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "DynamicCollectionQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As DynamicCollectionQuery) As String
			Return DynamicCollectionQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As DynamicCollectionQuery
			Return DirectCast(DynamicCollectionQuery.SerializeHelper.FromXml(query, GetType(DynamicCollectionQuery)), DynamicCollectionQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDynamicCollection
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal collectionName As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal collectionName As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal collectionName As System.String) As Boolean
		
			Dim query As New DynamicCollectionQuery()
			query.Where(query.CollectionName = collectionName)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal collectionName As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("CollectionName", collectionName)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to Dynamic_Collection.CollectionName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CollectionName As System.String
			Get
				Return MyBase.GetSystemString(DynamicCollectionMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DynamicCollectionMetadata.ColumnNames.CollectionName, value) Then
					OnPropertyChanged(DynamicCollectionMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Désigne le nom que portera la collection et doit être unique dans l'explorer
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(DynamicCollectionMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DynamicCollectionMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(DynamicCollectionMetadata.PropertyNames.Description)
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
												
						Case "CollectionName"
							Me.str().CollectionName = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
					
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
		
			Public Sub New(ByVal entity As esDynamicCollection)
				Me.entity = entity
			End Sub				
		
	
			Public Property CollectionName As System.String 
				Get
					Dim data_ As System.String = entity.CollectionName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CollectionName = Nothing
					Else
						entity.CollectionName = Convert.ToString(Value)
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
		  

			Private entity As esDynamicCollection
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DynamicCollectionMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As DynamicCollectionQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DynamicCollectionQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As DynamicCollectionQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As DynamicCollectionQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As DynamicCollectionQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDynamicCollectionCollection
		Inherits esEntityCollection(Of DynamicCollection)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DynamicCollectionMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "DynamicCollectionCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As DynamicCollectionQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DynamicCollectionQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As DynamicCollectionQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New DynamicCollectionQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As DynamicCollectionQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, DynamicCollectionQuery))
		End Sub
		
		#End Region
						
		Private m_query As DynamicCollectionQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDynamicCollectionQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return DynamicCollectionMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "CollectionName" 
					Return Me.CollectionName
				Case "Description" 
					Return Me.Description
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, DynamicCollectionMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, DynamicCollectionMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class DynamicCollection 
		Inherits esDynamicCollection
		
	
		#Region "HMIBooleanChannelsCollectionByCollectionName - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_HMIBooleanChannelsCollectionByCollectionName() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.DynamicCollection.HMIBooleanChannelsCollectionByCollectionName_Delegate)
				map.PropertyName = "HMIBooleanChannelsCollectionByCollectionName"
				map.MyColumnName = "CollectionName"
				map.ParentColumnName = "CollectionName"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub HMIBooleanChannelsCollectionByCollectionName_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New DynamicCollectionQuery(data.NextAlias())
			
			Dim mee As HMIBooleanChannelsQuery = If(data.You IsNot Nothing, TryCast(data.You, HMIBooleanChannelsQuery), New HMIBooleanChannelsQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CollectionName = mee.CollectionName)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Dynamic_CollectionHMI_Boolean_Channels
		''' </summary>

		<XmlIgnore()> _ 
		Public Property HMIBooleanChannelsCollectionByCollectionName As HMIBooleanChannelsCollection 
		
			Get
				If Me._HMIBooleanChannelsCollectionByCollectionName Is Nothing Then
					Me._HMIBooleanChannelsCollectionByCollectionName = New HMIBooleanChannelsCollection()
					Me._HMIBooleanChannelsCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("HMIBooleanChannelsCollectionByCollectionName", Me._HMIBooleanChannelsCollectionByCollectionName)
				
					If Not Me.CollectionName.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._HMIBooleanChannelsCollectionByCollectionName.Query.Where(Me._HMIBooleanChannelsCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
							Me._HMIBooleanChannelsCollectionByCollectionName.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._HMIBooleanChannelsCollectionByCollectionName.fks.Add(HMIBooleanChannelsMetadata.ColumnNames.CollectionName, Me.CollectionName)
					End If
				End If

				Return Me._HMIBooleanChannelsCollectionByCollectionName
			End Get
			
			Set(ByVal value As HMIBooleanChannelsCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._HMIBooleanChannelsCollectionByCollectionName Is Nothing Then

					Me.RemovePostSave("HMIBooleanChannelsCollectionByCollectionName")
					Me._HMIBooleanChannelsCollectionByCollectionName = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _HMIBooleanChannelsCollectionByCollectionName As HMIBooleanChannelsCollection
		#End Region

		#Region "HMINumericChannelsCollectionByCollectionName - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_HMINumericChannelsCollectionByCollectionName() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.DynamicCollection.HMINumericChannelsCollectionByCollectionName_Delegate)
				map.PropertyName = "HMINumericChannelsCollectionByCollectionName"
				map.MyColumnName = "CollectionName"
				map.ParentColumnName = "CollectionName"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub HMINumericChannelsCollectionByCollectionName_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New DynamicCollectionQuery(data.NextAlias())
			
			Dim mee As HMINumericChannelsQuery = If(data.You IsNot Nothing, TryCast(data.You, HMINumericChannelsQuery), New HMINumericChannelsQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CollectionName = mee.CollectionName)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Dynamic_CollectionHMI_Numeric_Channels
		''' </summary>

		<XmlIgnore()> _ 
		Public Property HMINumericChannelsCollectionByCollectionName As HMINumericChannelsCollection 
		
			Get
				If Me._HMINumericChannelsCollectionByCollectionName Is Nothing Then
					Me._HMINumericChannelsCollectionByCollectionName = New HMINumericChannelsCollection()
					Me._HMINumericChannelsCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("HMINumericChannelsCollectionByCollectionName", Me._HMINumericChannelsCollectionByCollectionName)
				
					If Not Me.CollectionName.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._HMINumericChannelsCollectionByCollectionName.Query.Where(Me._HMINumericChannelsCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
							Me._HMINumericChannelsCollectionByCollectionName.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._HMINumericChannelsCollectionByCollectionName.fks.Add(HMINumericChannelsMetadata.ColumnNames.CollectionName, Me.CollectionName)
					End If
				End If

				Return Me._HMINumericChannelsCollectionByCollectionName
			End Get
			
			Set(ByVal value As HMINumericChannelsCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._HMINumericChannelsCollectionByCollectionName Is Nothing Then

					Me.RemovePostSave("HMINumericChannelsCollectionByCollectionName")
					Me._HMINumericChannelsCollectionByCollectionName = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _HMINumericChannelsCollectionByCollectionName As HMINumericChannelsCollection
		#End Region

		#Region "HMITextChannelsCollectionByCollectionName - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_HMITextChannelsCollectionByCollectionName() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.DynamicCollection.HMITextChannelsCollectionByCollectionName_Delegate)
				map.PropertyName = "HMITextChannelsCollectionByCollectionName"
				map.MyColumnName = "CollectionName"
				map.ParentColumnName = "CollectionName"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub HMITextChannelsCollectionByCollectionName_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New DynamicCollectionQuery(data.NextAlias())
			
			Dim mee As HMITextChannelsQuery = If(data.You IsNot Nothing, TryCast(data.You, HMITextChannelsQuery), New HMITextChannelsQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CollectionName = mee.CollectionName)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Dynamic_CollectionHMI_Text_Channels
		''' </summary>

		<XmlIgnore()> _ 
		Public Property HMITextChannelsCollectionByCollectionName As HMITextChannelsCollection 
		
			Get
				If Me._HMITextChannelsCollectionByCollectionName Is Nothing Then
					Me._HMITextChannelsCollectionByCollectionName = New HMITextChannelsCollection()
					Me._HMITextChannelsCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("HMITextChannelsCollectionByCollectionName", Me._HMITextChannelsCollectionByCollectionName)
				
					If Not Me.CollectionName.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._HMITextChannelsCollectionByCollectionName.Query.Where(Me._HMITextChannelsCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
							Me._HMITextChannelsCollectionByCollectionName.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._HMITextChannelsCollectionByCollectionName.fks.Add(HMITextChannelsMetadata.ColumnNames.CollectionName, Me.CollectionName)
					End If
				End If

				Return Me._HMITextChannelsCollectionByCollectionName
			End Get
			
			Set(ByVal value As HMITextChannelsCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._HMITextChannelsCollectionByCollectionName Is Nothing Then

					Me.RemovePostSave("HMITextChannelsCollectionByCollectionName")
					Me._HMITextChannelsCollectionByCollectionName = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _HMITextChannelsCollectionByCollectionName As HMITextChannelsCollection
		#End Region

		#Region "UUTStaticChannelCollectionCollectionByCollectionName - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTStaticChannelCollectionCollectionByCollectionName() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.DynamicCollection.UUTStaticChannelCollectionCollectionByCollectionName_Delegate)
				map.PropertyName = "UUTStaticChannelCollectionCollectionByCollectionName"
				map.MyColumnName = "CollectionName"
				map.ParentColumnName = "CollectionName"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub UUTStaticChannelCollectionCollectionByCollectionName_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New DynamicCollectionQuery(data.NextAlias())
			
			Dim mee As UUTStaticChannelCollectionQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTStaticChannelCollectionQuery), New UUTStaticChannelCollectionQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CollectionName = mee.CollectionName)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - Dynamic_CollectionUUT_Static_Channel_Collection
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTStaticChannelCollectionCollectionByCollectionName As UUTStaticChannelCollectionCollection 
		
			Get
				If Me._UUTStaticChannelCollectionCollectionByCollectionName Is Nothing Then
					Me._UUTStaticChannelCollectionCollectionByCollectionName = New UUTStaticChannelCollectionCollection()
					Me._UUTStaticChannelCollectionCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTStaticChannelCollectionCollectionByCollectionName", Me._UUTStaticChannelCollectionCollectionByCollectionName)
				
					If Not Me.CollectionName.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTStaticChannelCollectionCollectionByCollectionName.Query.Where(Me._UUTStaticChannelCollectionCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
							Me._UUTStaticChannelCollectionCollectionByCollectionName.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTStaticChannelCollectionCollectionByCollectionName.fks.Add(UUTStaticChannelCollectionMetadata.ColumnNames.CollectionName, Me.CollectionName)
					End If
				End If

				Return Me._UUTStaticChannelCollectionCollectionByCollectionName
			End Get
			
			Set(ByVal value As UUTStaticChannelCollectionCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTStaticChannelCollectionCollectionByCollectionName Is Nothing Then

					Me.RemovePostSave("UUTStaticChannelCollectionCollectionByCollectionName")
					Me._UUTStaticChannelCollectionCollectionByCollectionName = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTStaticChannelCollectionCollectionByCollectionName As UUTStaticChannelCollectionCollection
		#End Region

		#Region "Dacn1AfdxVirtualLinkCollectionByCollectionName - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_Dacn1AfdxVirtualLinkCollectionByCollectionName() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.DynamicCollection.Dacn1AfdxVirtualLinkCollectionByCollectionName_Delegate)
				map.PropertyName = "Dacn1AfdxVirtualLinkCollectionByCollectionName"
				map.MyColumnName = "CollectionName"
				map.ParentColumnName = "CollectionName"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub Dacn1AfdxVirtualLinkCollectionByCollectionName_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New DynamicCollectionQuery(data.NextAlias())
			
			Dim mee As Dacn1AfdxVirtualLinkQuery = If(data.You IsNot Nothing, TryCast(data.You, Dacn1AfdxVirtualLinkQuery), New Dacn1AfdxVirtualLinkQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CollectionName = mee.CollectionName)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUT_Dynamic_CollectionDACN1_AFDX_VIRTUAL_LINK
		''' </summary>

		<XmlIgnore()> _ 
		Public Property Dacn1AfdxVirtualLinkCollectionByCollectionName As Dacn1AfdxVirtualLinkCollection 
		
			Get
				If Me._Dacn1AfdxVirtualLinkCollectionByCollectionName Is Nothing Then
					Me._Dacn1AfdxVirtualLinkCollectionByCollectionName = New Dacn1AfdxVirtualLinkCollection()
					Me._Dacn1AfdxVirtualLinkCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("Dacn1AfdxVirtualLinkCollectionByCollectionName", Me._Dacn1AfdxVirtualLinkCollectionByCollectionName)
				
					If Not Me.CollectionName.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._Dacn1AfdxVirtualLinkCollectionByCollectionName.Query.Where(Me._Dacn1AfdxVirtualLinkCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
							Me._Dacn1AfdxVirtualLinkCollectionByCollectionName.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._Dacn1AfdxVirtualLinkCollectionByCollectionName.fks.Add(Dacn1AfdxVirtualLinkMetadata.ColumnNames.CollectionName, Me.CollectionName)
					End If
				End If

				Return Me._Dacn1AfdxVirtualLinkCollectionByCollectionName
			End Get
			
			Set(ByVal value As Dacn1AfdxVirtualLinkCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._Dacn1AfdxVirtualLinkCollectionByCollectionName Is Nothing Then

					Me.RemovePostSave("Dacn1AfdxVirtualLinkCollectionByCollectionName")
					Me._Dacn1AfdxVirtualLinkCollectionByCollectionName = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _Dacn1AfdxVirtualLinkCollectionByCollectionName As Dacn1AfdxVirtualLinkCollection
		#End Region

		#Region "UUTDynamicChannelCollectionCollectionByCollectionName - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTDynamicChannelCollectionCollectionByCollectionName() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.DynamicCollection.UUTDynamicChannelCollectionCollectionByCollectionName_Delegate)
				map.PropertyName = "UUTDynamicChannelCollectionCollectionByCollectionName"
				map.MyColumnName = "CollectionName"
				map.ParentColumnName = "CollectionName"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub UUTDynamicChannelCollectionCollectionByCollectionName_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New DynamicCollectionQuery(data.NextAlias())
			
			Dim mee As UUTDynamicChannelCollectionQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTDynamicChannelCollectionQuery), New UUTDynamicChannelCollectionQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CollectionName = mee.CollectionName)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUT_Dynamic_CollectionUUT_Dynamic_Channel_Collection
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTDynamicChannelCollectionCollectionByCollectionName As UUTDynamicChannelCollectionCollection 
		
			Get
				If Me._UUTDynamicChannelCollectionCollectionByCollectionName Is Nothing Then
					Me._UUTDynamicChannelCollectionCollectionByCollectionName = New UUTDynamicChannelCollectionCollection()
					Me._UUTDynamicChannelCollectionCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTDynamicChannelCollectionCollectionByCollectionName", Me._UUTDynamicChannelCollectionCollectionByCollectionName)
				
					If Not Me.CollectionName.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTDynamicChannelCollectionCollectionByCollectionName.Query.Where(Me._UUTDynamicChannelCollectionCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
							Me._UUTDynamicChannelCollectionCollectionByCollectionName.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTDynamicChannelCollectionCollectionByCollectionName.fks.Add(UUTDynamicChannelCollectionMetadata.ColumnNames.CollectionName, Me.CollectionName)
					End If
				End If

				Return Me._UUTDynamicChannelCollectionCollectionByCollectionName
			End Get
			
			Set(ByVal value As UUTDynamicChannelCollectionCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTDynamicChannelCollectionCollectionByCollectionName Is Nothing Then

					Me.RemovePostSave("UUTDynamicChannelCollectionCollectionByCollectionName")
					Me._UUTDynamicChannelCollectionCollectionByCollectionName = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTDynamicChannelCollectionCollectionByCollectionName As UUTDynamicChannelCollectionCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "HMIBooleanChannelsCollectionByCollectionName"
					coll = Me.HMIBooleanChannelsCollectionByCollectionName
					Exit Select
				Case "HMINumericChannelsCollectionByCollectionName"
					coll = Me.HMINumericChannelsCollectionByCollectionName
					Exit Select
				Case "HMITextChannelsCollectionByCollectionName"
					coll = Me.HMITextChannelsCollectionByCollectionName
					Exit Select
				Case "UUTStaticChannelCollectionCollectionByCollectionName"
					coll = Me.UUTStaticChannelCollectionCollectionByCollectionName
					Exit Select
				Case "Dacn1AfdxVirtualLinkCollectionByCollectionName"
					coll = Me.Dacn1AfdxVirtualLinkCollectionByCollectionName
					Exit Select
				Case "UUTDynamicChannelCollectionCollectionByCollectionName"
					coll = Me.UUTDynamicChannelCollectionCollectionByCollectionName
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "HMIBooleanChannelsCollectionByCollectionName", GetType(HMIBooleanChannelsCollection), New HMIBooleanChannels()))
			props.Add(new esPropertyDescriptor(Me, "HMINumericChannelsCollectionByCollectionName", GetType(HMINumericChannelsCollection), New HMINumericChannels()))
			props.Add(new esPropertyDescriptor(Me, "HMITextChannelsCollectionByCollectionName", GetType(HMITextChannelsCollection), New HMITextChannels()))
			props.Add(new esPropertyDescriptor(Me, "UUTStaticChannelCollectionCollectionByCollectionName", GetType(UUTStaticChannelCollectionCollection), New UUTStaticChannelCollection()))
			props.Add(new esPropertyDescriptor(Me, "Dacn1AfdxVirtualLinkCollectionByCollectionName", GetType(Dacn1AfdxVirtualLinkCollection), New Dacn1AfdxVirtualLink()))
			props.Add(new esPropertyDescriptor(Me, "UUTDynamicChannelCollectionCollectionByCollectionName", GetType(UUTDynamicChannelCollectionCollection), New UUTDynamicChannelCollection()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class DynamicCollectionMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(DynamicCollectionMetadata.ColumnNames.CollectionName, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = DynamicCollectionMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DynamicCollectionMetadata.ColumnNames.Description, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = DynamicCollectionMetadata.PropertyNames.Description
			c.CharacterMaxLength = 255
			c.Description = "Désigne le nom que portera la collection et doit être unique dans l'explorer"
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As DynamicCollectionMetadata
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
			 Public Const CollectionName As String = "CollectionName"
			 Public Const Description As String = "Description"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const CollectionName As String = "CollectionName"
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
			SyncLock GetType(DynamicCollectionMetadata)
			
				If DynamicCollectionMetadata.mapDelegates Is Nothing Then
					DynamicCollectionMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If DynamicCollectionMetadata._meta Is Nothing Then
					DynamicCollectionMetadata._meta = New DynamicCollectionMetadata()
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
				


				meta.AddTypeMap("CollectionName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "Dynamic_Collection"
				meta.Destination = "Dynamic_Collection"
				
				meta.spInsert = "proc_Dynamic_CollectionInsert"
				meta.spUpdate = "proc_Dynamic_CollectionUpdate"
				meta.spDelete = "proc_Dynamic_CollectionDelete"
				meta.spLoadAll = "proc_Dynamic_CollectionLoadAll"
				meta.spLoadByPrimaryKey = "proc_Dynamic_CollectionLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As DynamicCollectionMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

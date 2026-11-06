
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 5/7/2015 11:03:08 AM
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
	' Encapsulates the 'HMI_Text_Channels' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(HMITextChannels))> _
	<XmlType("HMITextChannels")> _	
	Partial Public Class HMITextChannels 
		Inherits esHMITextChannels
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New HMITextChannels()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal channelName As System.String)
			Dim obj As New HMITextChannels()
			obj.CollectionName = collectionName
			obj.ChannelName = channelName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal channelName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New HMITextChannels()
			obj.CollectionName = collectionName
			obj.ChannelName = channelName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("HMITextChannelsCollection")> _
	Partial Public Class HMITextChannelsCollection
		Inherits esHMITextChannelsCollection
		Implements IEnumerable(Of HMITextChannels)
	
		Public Function FindByPrimaryKey(ByVal collectionName As System.String, ByVal channelName As System.String) As HMITextChannels
			Return MyBase.SingleOrDefault(Function(e) e.CollectionName = collectionName And e.ChannelName = channelName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(HMITextChannels))> _
		Public Class HMITextChannelsCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of HMITextChannelsCollection)
			
			Public Shared Widening Operator CType(packet As HMITextChannelsCollectionWCFPacket) As HMITextChannelsCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As HMITextChannelsCollection) As HMITextChannelsCollectionWCFPacket
				Return New HMITextChannelsCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class HMITextChannelsQuery 
		Inherits esHMITextChannelsQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "HMITextChannelsQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As HMITextChannelsQuery) As String
			Return HMITextChannelsQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As HMITextChannelsQuery
			Return DirectCast(HMITextChannelsQuery.SerializeHelper.FromXml(query, GetType(HMITextChannelsQuery)), HMITextChannelsQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esHMITextChannels
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal collectionName As System.String, ByVal channelName As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, channelName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, channelName)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal collectionName As System.String, ByVal channelName As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, channelName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, channelName)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal collectionName As System.String, ByVal channelName As System.String) As Boolean
		
			Dim query As New HMITextChannelsQuery()
			query.Where(query.CollectionName = collectionName And query.ChannelName = channelName)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal collectionName As System.String, ByVal channelName As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("CollectionName", collectionName)
						parms.Add("ChannelName", channelName)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to HMI_Text_Channels.CollectionName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CollectionName As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.CollectionName, value) Then
					Me._UpToDynamicCollectionByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDynamicCollectionByCollectionName")
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Text_Channels.ChannelName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelName As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.ChannelName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.ChannelName, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.ChannelName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Text_Channels.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Text_Channels.Source
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Source As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.Source)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.Source, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.Source)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Text_Channels.Destination
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Destination As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.Destination)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.Destination, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.Destination)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Text_Channels.OPC_Group
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OPCGroup As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.OPCGroup)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.OPCGroup, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.OPCGroup)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Text_Channels.OPC_ItemID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OPCItemID As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.OPCItemID)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.OPCItemID, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.OPCItemID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Text_Channels.Tooltip
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Tooltip As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.Tooltip)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.Tooltip, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.Tooltip)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Text_Channels.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.Note)
				End If
			End Set
		End Property	
			
		' <summary>
		' Intitial value when opening the sequence
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property InitialValue As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.InitialValue)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.InitialValue, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.InitialValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Final value when closing the sequence
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FinalValue As System.String
			Get
				Return MyBase.GetSystemString(HMITextChannelsMetadata.ColumnNames.FinalValue)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMITextChannelsMetadata.ColumnNames.FinalValue, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.FinalValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Text_Channels.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMITextChannelsMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMITextChannelsMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToDynamicCollectionByCollectionName As DynamicCollection
		
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
												
						Case "ChannelName"
							Me.str().ChannelName = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "Source"
							Me.str().Source = CType(value, string)
												
						Case "Destination"
							Me.str().Destination = CType(value, string)
												
						Case "OPCGroup"
							Me.str().OPCGroup = CType(value, string)
												
						Case "OPCItemID"
							Me.str().OPCItemID = CType(value, string)
												
						Case "Tooltip"
							Me.str().Tooltip = CType(value, string)
												
						Case "Note"
							Me.str().Note = CType(value, string)
												
						Case "InitialValue"
							Me.str().InitialValue = CType(value, string)
												
						Case "FinalValue"
							Me.str().FinalValue = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMITextChannelsMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esHMITextChannels)
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
		  	
			Public Property ChannelName As System.String 
				Get
					Dim data_ As System.String = entity.ChannelName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ChannelName = Nothing
					Else
						entity.ChannelName = Convert.ToString(Value)
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
		  	
			Public Property Source As System.String 
				Get
					Dim data_ As System.String = entity.Source
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Source = Nothing
					Else
						entity.Source = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Destination As System.String 
				Get
					Dim data_ As System.String = entity.Destination
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Destination = Nothing
					Else
						entity.Destination = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property OPCGroup As System.String 
				Get
					Dim data_ As System.String = entity.OPCGroup
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OPCGroup = Nothing
					Else
						entity.OPCGroup = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property OPCItemID As System.String 
				Get
					Dim data_ As System.String = entity.OPCItemID
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OPCItemID = Nothing
					Else
						entity.OPCItemID = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Tooltip As System.String 
				Get
					Dim data_ As System.String = entity.Tooltip
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Tooltip = Nothing
					Else
						entity.Tooltip = Convert.ToString(Value)
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
		  	
			Public Property InitialValue As System.String 
				Get
					Dim data_ As System.String = entity.InitialValue
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.InitialValue = Nothing
					Else
						entity.InitialValue = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property FinalValue As System.String 
				Get
					Dim data_ As System.String = entity.FinalValue
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FinalValue = Nothing
					Else
						entity.FinalValue = Convert.ToString(Value)
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
		  

			Private entity As esHMITextChannels
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return HMITextChannelsMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As HMITextChannelsQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New HMITextChannelsQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As HMITextChannelsQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As HMITextChannelsQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As HMITextChannelsQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esHMITextChannelsCollection
		Inherits esEntityCollection(Of HMITextChannels)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return HMITextChannelsMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "HMITextChannelsCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As HMITextChannelsQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New HMITextChannelsQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As HMITextChannelsQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New HMITextChannelsQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As HMITextChannelsQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, HMITextChannelsQuery))
		End Sub
		
		#End Region
						
		Private m_query As HMITextChannelsQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esHMITextChannelsQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return HMITextChannelsMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "CollectionName" 
					Return Me.CollectionName
				Case "ChannelName" 
					Return Me.ChannelName
				Case "Description" 
					Return Me.Description
				Case "Source" 
					Return Me.Source
				Case "Destination" 
					Return Me.Destination
				Case "OPCGroup" 
					Return Me.OPCGroup
				Case "OPCItemID" 
					Return Me.OPCItemID
				Case "Tooltip" 
					Return Me.Tooltip
				Case "Note" 
					Return Me.Note
				Case "InitialValue" 
					Return Me.InitialValue
				Case "FinalValue" 
					Return Me.FinalValue
				Case "Unavailable" 
					Return Me.Unavailable
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelName As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.ChannelName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Source As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.Source, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Destination As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.Destination, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OPCGroup As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.OPCGroup, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OPCItemID As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.OPCItemID, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Tooltip As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.Tooltip, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property InitialValue As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.InitialValue, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FinalValue As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.FinalValue, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, HMITextChannelsMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class HMITextChannels 
		Inherits esHMITextChannels
		
	
		#Region "UpToDynamicCollectionByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Dynamic_CollectionHMI_Text_Channels
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToDynamicCollectionByCollectionName As DynamicCollection
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToDynamicCollectionByCollectionName Is Nothing _
						 AndAlso Not CollectionName.Equals(Nothing)  Then
						
					Me._UpToDynamicCollectionByCollectionName = New DynamicCollection()
					Me._UpToDynamicCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToDynamicCollectionByCollectionName", Me._UpToDynamicCollectionByCollectionName)
					Me._UpToDynamicCollectionByCollectionName.Query.Where(Me._UpToDynamicCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
					Me._UpToDynamicCollectionByCollectionName.Query.Load()
				End If

				Return Me._UpToDynamicCollectionByCollectionName
			End Get
			
            Set(ByVal value As DynamicCollection)
				Me.RemovePreSave("UpToDynamicCollectionByCollectionName")
				

				If value Is Nothing Then
				
					Me.CollectionName = Nothing
				
					Me._UpToDynamicCollectionByCollectionName = Nothing
				Else
				
					Me.CollectionName = value.CollectionName
					
					Me._UpToDynamicCollectionByCollectionName = value
					Me.SetPreSave("UpToDynamicCollectionByCollectionName", Me._UpToDynamicCollectionByCollectionName)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class HMITextChannelsMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.CollectionName, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.ChannelName, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.ChannelName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.Description, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.Description
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.Source, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.Source
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.Destination, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.Destination
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.OPCGroup, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.OPCGroup
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.OPCItemID, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.OPCItemID
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.Tooltip, 7, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.Tooltip
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.Note, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.Note
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.InitialValue, 9, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.InitialValue
			c.CharacterMaxLength = 255
			c.Description = "Intitial value when opening the sequence"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.FinalValue, 10, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.FinalValue
			c.CharacterMaxLength = 255
			c.Description = "Final value when closing the sequence"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMITextChannelsMetadata.ColumnNames.Unavailable, 11, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMITextChannelsMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As HMITextChannelsMetadata
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
			 Public Const ChannelName As String = "ChannelName"
			 Public Const Description As String = "Description"
			 Public Const Source As String = "Source"
			 Public Const Destination As String = "Destination"
			 Public Const OPCGroup As String = "OPC_Group"
			 Public Const OPCItemID As String = "OPC_ItemID"
			 Public Const Tooltip As String = "Tooltip"
			 Public Const Note As String = "Note"
			 Public Const InitialValue As String = "InitialValue"
			 Public Const FinalValue As String = "FinalValue"
			 Public Const Unavailable As String = "Unavailable"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const CollectionName As String = "CollectionName"
			 Public Const ChannelName As String = "ChannelName"
			 Public Const Description As String = "Description"
			 Public Const Source As String = "Source"
			 Public Const Destination As String = "Destination"
			 Public Const OPCGroup As String = "OPCGroup"
			 Public Const OPCItemID As String = "OPCItemID"
			 Public Const Tooltip As String = "Tooltip"
			 Public Const Note As String = "Note"
			 Public Const InitialValue As String = "InitialValue"
			 Public Const FinalValue As String = "FinalValue"
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
			SyncLock GetType(HMITextChannelsMetadata)
			
				If HMITextChannelsMetadata.mapDelegates Is Nothing Then
					HMITextChannelsMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If HMITextChannelsMetadata._meta Is Nothing Then
					HMITextChannelsMetadata._meta = New HMITextChannelsMetadata()
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
				meta.AddTypeMap("ChannelName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Source", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Destination", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OPCGroup", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OPCItemID", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Tooltip", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Note", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("InitialValue", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("FinalValue", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "HMI_Text_Channels"
				meta.Destination = "HMI_Text_Channels"
				
				meta.spInsert = "proc_HMI_Text_ChannelsInsert"
				meta.spUpdate = "proc_HMI_Text_ChannelsUpdate"
				meta.spDelete = "proc_HMI_Text_ChannelsDelete"
				meta.spLoadAll = "proc_HMI_Text_ChannelsLoadAll"
				meta.spLoadByPrimaryKey = "proc_HMI_Text_ChannelsLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As HMITextChannelsMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

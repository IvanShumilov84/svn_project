
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 7/17/2019 11:40:59 AM
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
	' Encapsulates the 'HMI_Boolean_Channels' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(HMIBooleanChannels))> _
	<XmlType("HMIBooleanChannels")> _	
	Partial Public Class HMIBooleanChannels 
		Inherits esHMIBooleanChannels
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New HMIBooleanChannels()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal channelName As System.String)
			Dim obj As New HMIBooleanChannels()
			obj.CollectionName = collectionName
			obj.ChannelName = channelName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal channelName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New HMIBooleanChannels()
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
	<XmlType("HMIBooleanChannelsCollection")> _
	Partial Public Class HMIBooleanChannelsCollection
		Inherits esHMIBooleanChannelsCollection
		Implements IEnumerable(Of HMIBooleanChannels)
	
		Public Function FindByPrimaryKey(ByVal collectionName As System.String, ByVal channelName As System.String) As HMIBooleanChannels
			Return MyBase.SingleOrDefault(Function(e) e.CollectionName = collectionName And e.ChannelName = channelName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(HMIBooleanChannels))> _
		Public Class HMIBooleanChannelsCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of HMIBooleanChannelsCollection)
			
			Public Shared Widening Operator CType(packet As HMIBooleanChannelsCollectionWCFPacket) As HMIBooleanChannelsCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As HMIBooleanChannelsCollection) As HMIBooleanChannelsCollectionWCFPacket
				Return New HMIBooleanChannelsCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class HMIBooleanChannelsQuery 
		Inherits esHMIBooleanChannelsQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "HMIBooleanChannelsQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As HMIBooleanChannelsQuery) As String
			Return HMIBooleanChannelsQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As HMIBooleanChannelsQuery
			Return DirectCast(HMIBooleanChannelsQuery.SerializeHelper.FromXml(query, GetType(HMIBooleanChannelsQuery)), HMIBooleanChannelsQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esHMIBooleanChannels
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
		
			Dim query As New HMIBooleanChannelsQuery()
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
		' Maps to HMI_Boolean_Channels.CollectionName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CollectionName As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.CollectionName, value) Then
					Me._UpToDynamicCollectionByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDynamicCollectionByCollectionName")
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.ChannelName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelName As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.ChannelName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.ChannelName, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.ChannelName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.Source
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Source As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Source)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Source, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.Source)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.Destination
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Destination As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Destination)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Destination, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.Destination)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.ResetLatch
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ResetLatch As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.ResetLatch)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.ResetLatch, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.ResetLatch)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.TempNote
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TempNote As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.TempNote)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.TempNote, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.TempNote)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.Expression
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Expression As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Expression)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Expression, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.Expression)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.OPC_Group
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OPCGroup As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.OPCGroup)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.OPCGroup, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.OPCGroup)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.OPC_ItemID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OPCItemID As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.OPCItemID)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.OPCItemID, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.OPCItemID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.Tooltip
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Tooltip As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Tooltip)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Tooltip, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.Tooltip)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.Note)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.FalseText
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FalseText As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.FalseText)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.FalseText, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.FalseText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.TrueText
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TrueText As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.TrueText)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.TrueText, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.TrueText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.FalseMessageText
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FalseMessageText As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.FalseMessageText)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.FalseMessageText, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.FalseMessageText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.TrueMessageText
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TrueMessageText As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.TrueMessageText)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.TrueMessageText, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.TrueMessageText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.SoundMessage
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SoundMessage As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMIBooleanChannelsMetadata.ColumnNames.SoundMessage)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMIBooleanChannelsMetadata.ColumnNames.SoundMessage, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.SoundMessage)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.FormulaVisibleForAdminOnly
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FormulaVisibleForAdminOnly As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMIBooleanChannelsMetadata.ColumnNames.FormulaVisibleForAdminOnly)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMIBooleanChannelsMetadata.ColumnNames.FormulaVisibleForAdminOnly, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.FormulaVisibleForAdminOnly)
				End If
			End Set
		End Property	
			
		' <summary>
		' Intitial value when opening the sequence
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property InitialValue As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMIBooleanChannelsMetadata.ColumnNames.InitialValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMIBooleanChannelsMetadata.ColumnNames.InitialValue, value) Then
					Me._UpToBooleanStateByInitialValue = Nothing
					Me.OnPropertyChanged("UpToBooleanStateByInitialValue")
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.InitialValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Final value when closing the sequence
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FinalValue As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMIBooleanChannelsMetadata.ColumnNames.FinalValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMIBooleanChannelsMetadata.ColumnNames.FinalValue, value) Then
					Me._UpToBooleanStateByFinalValue = Nothing
					Me.OnPropertyChanged("UpToBooleanStateByFinalValue")
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.FinalValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMIBooleanChannelsMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMIBooleanChannelsMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.FalseMessageTextColor
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FalseMessageTextColor As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.FalseMessageTextColor)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.FalseMessageTextColor, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.FalseMessageTextColor)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Boolean_Channels.TrueMessageTextColor
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TrueMessageTextColor As System.String
			Get
				Return MyBase.GetSystemString(HMIBooleanChannelsMetadata.ColumnNames.TrueMessageTextColor)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIBooleanChannelsMetadata.ColumnNames.TrueMessageTextColor, value) Then
					OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.TrueMessageTextColor)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToBooleanStateByInitialValue As BooleanState
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToBooleanStateByFinalValue As BooleanState
		
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
												
						Case "ResetLatch"
							Me.str().ResetLatch = CType(value, string)
												
						Case "TempNote"
							Me.str().TempNote = CType(value, string)
												
						Case "Expression"
							Me.str().Expression = CType(value, string)
												
						Case "OPCGroup"
							Me.str().OPCGroup = CType(value, string)
												
						Case "OPCItemID"
							Me.str().OPCItemID = CType(value, string)
												
						Case "Tooltip"
							Me.str().Tooltip = CType(value, string)
												
						Case "Note"
							Me.str().Note = CType(value, string)
												
						Case "FalseText"
							Me.str().FalseText = CType(value, string)
												
						Case "TrueText"
							Me.str().TrueText = CType(value, string)
												
						Case "FalseMessageText"
							Me.str().FalseMessageText = CType(value, string)
												
						Case "TrueMessageText"
							Me.str().TrueMessageText = CType(value, string)
												
						Case "SoundMessage"
							Me.str().SoundMessage = CType(value, string)
												
						Case "FormulaVisibleForAdminOnly"
							Me.str().FormulaVisibleForAdminOnly = CType(value, string)
												
						Case "InitialValue"
							Me.str().InitialValue = CType(value, string)
												
						Case "FinalValue"
							Me.str().FinalValue = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
												
						Case "FalseMessageTextColor"
							Me.str().FalseMessageTextColor = CType(value, string)
												
						Case "TrueMessageTextColor"
							Me.str().TrueMessageTextColor = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "SoundMessage"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.SoundMessage = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.SoundMessage)
							End If
						
						Case "FormulaVisibleForAdminOnly"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.FormulaVisibleForAdminOnly = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.FormulaVisibleForAdminOnly)
							End If
						
						Case "InitialValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.InitialValue = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.InitialValue)
							End If
						
						Case "FinalValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.FinalValue = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.FinalValue)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMIBooleanChannelsMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esHMIBooleanChannels)
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
		  	
			Public Property ResetLatch As System.String 
				Get
					Dim data_ As System.String = entity.ResetLatch
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ResetLatch = Nothing
					Else
						entity.ResetLatch = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property TempNote As System.String 
				Get
					Dim data_ As System.String = entity.TempNote
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TempNote = Nothing
					Else
						entity.TempNote = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Expression As System.String 
				Get
					Dim data_ As System.String = entity.Expression
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Expression = Nothing
					Else
						entity.Expression = Convert.ToString(Value)
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
		  	
			Public Property FalseText As System.String 
				Get
					Dim data_ As System.String = entity.FalseText
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FalseText = Nothing
					Else
						entity.FalseText = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property TrueText As System.String 
				Get
					Dim data_ As System.String = entity.TrueText
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TrueText = Nothing
					Else
						entity.TrueText = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property FalseMessageText As System.String 
				Get
					Dim data_ As System.String = entity.FalseMessageText
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FalseMessageText = Nothing
					Else
						entity.FalseMessageText = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property TrueMessageText As System.String 
				Get
					Dim data_ As System.String = entity.TrueMessageText
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TrueMessageText = Nothing
					Else
						entity.TrueMessageText = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property SoundMessage As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.SoundMessage
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SoundMessage = Nothing
					Else
						entity.SoundMessage = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property FormulaVisibleForAdminOnly As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.FormulaVisibleForAdminOnly
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FormulaVisibleForAdminOnly = Nothing
					Else
						entity.FormulaVisibleForAdminOnly = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property InitialValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.InitialValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.InitialValue = Nothing
					Else
						entity.InitialValue = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property FinalValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.FinalValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FinalValue = Nothing
					Else
						entity.FinalValue = Convert.ToInt32(Value)
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
		  	
			Public Property FalseMessageTextColor As System.String 
				Get
					Dim data_ As System.String = entity.FalseMessageTextColor
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FalseMessageTextColor = Nothing
					Else
						entity.FalseMessageTextColor = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property TrueMessageTextColor As System.String 
				Get
					Dim data_ As System.String = entity.TrueMessageTextColor
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TrueMessageTextColor = Nothing
					Else
						entity.TrueMessageTextColor = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esHMIBooleanChannels
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return HMIBooleanChannelsMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As HMIBooleanChannelsQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New HMIBooleanChannelsQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As HMIBooleanChannelsQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As HMIBooleanChannelsQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As HMIBooleanChannelsQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esHMIBooleanChannelsCollection
		Inherits esEntityCollection(Of HMIBooleanChannels)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return HMIBooleanChannelsMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "HMIBooleanChannelsCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As HMIBooleanChannelsQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New HMIBooleanChannelsQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As HMIBooleanChannelsQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New HMIBooleanChannelsQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As HMIBooleanChannelsQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, HMIBooleanChannelsQuery))
		End Sub
		
		#End Region
						
		Private m_query As HMIBooleanChannelsQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esHMIBooleanChannelsQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return HMIBooleanChannelsMetadata.Meta()
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
				Case "ResetLatch" 
					Return Me.ResetLatch
				Case "TempNote" 
					Return Me.TempNote
				Case "Expression" 
					Return Me.Expression
				Case "OPCGroup" 
					Return Me.OPCGroup
				Case "OPCItemID" 
					Return Me.OPCItemID
				Case "Tooltip" 
					Return Me.Tooltip
				Case "Note" 
					Return Me.Note
				Case "FalseText" 
					Return Me.FalseText
				Case "TrueText" 
					Return Me.TrueText
				Case "FalseMessageText" 
					Return Me.FalseMessageText
				Case "TrueMessageText" 
					Return Me.TrueMessageText
				Case "SoundMessage" 
					Return Me.SoundMessage
				Case "FormulaVisibleForAdminOnly" 
					Return Me.FormulaVisibleForAdminOnly
				Case "InitialValue" 
					Return Me.InitialValue
				Case "FinalValue" 
					Return Me.FinalValue
				Case "Unavailable" 
					Return Me.Unavailable
				Case "FalseMessageTextColor" 
					Return Me.FalseMessageTextColor
				Case "TrueMessageTextColor" 
					Return Me.TrueMessageTextColor
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelName As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.ChannelName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Source As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.Source, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Destination As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.Destination, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ResetLatch As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.ResetLatch, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TempNote As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.TempNote, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Expression As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.Expression, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OPCGroup As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.OPCGroup, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OPCItemID As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.OPCItemID, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Tooltip As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.Tooltip, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FalseText As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.FalseText, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TrueText As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.TrueText, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FalseMessageText As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.FalseMessageText, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TrueMessageText As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.TrueMessageText, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property SoundMessage As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.SoundMessage, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property FormulaVisibleForAdminOnly As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.FormulaVisibleForAdminOnly, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property InitialValue As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.InitialValue, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property FinalValue As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.FinalValue, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property FalseMessageTextColor As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.FalseMessageTextColor, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TrueMessageTextColor As esQueryItem
			Get
				Return New esQueryItem(Me, HMIBooleanChannelsMetadata.ColumnNames.TrueMessageTextColor, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class HMIBooleanChannels 
		Inherits esHMIBooleanChannels
		
	
		#Region "UpToBooleanStateByInitialValue - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - BooleanStateHMI_Boolean_Channels
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToBooleanStateByInitialValue As BooleanState
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToBooleanStateByInitialValue Is Nothing _
						 AndAlso Not InitialValue.Equals(Nothing)  Then
						
					Me._UpToBooleanStateByInitialValue = New BooleanState()
					Me._UpToBooleanStateByInitialValue.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToBooleanStateByInitialValue", Me._UpToBooleanStateByInitialValue)
					Me._UpToBooleanStateByInitialValue.Query.Where(Me._UpToBooleanStateByInitialValue.Query.Id = Me.InitialValue)
					Me._UpToBooleanStateByInitialValue.Query.Load()
				End If

				Return Me._UpToBooleanStateByInitialValue
			End Get
			
            Set(ByVal value As BooleanState)
				Me.RemovePreSave("UpToBooleanStateByInitialValue")
				

				If value Is Nothing Then
				
					Me.InitialValue = Nothing
				
					Me._UpToBooleanStateByInitialValue = Nothing
				Else
				
					Me.InitialValue = value.Id
					
					Me._UpToBooleanStateByInitialValue = value
					Me.SetPreSave("UpToBooleanStateByInitialValue", Me._UpToBooleanStateByInitialValue)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToBooleanStateByFinalValue - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - BooleanStateHMI_Boolean_Channels1
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToBooleanStateByFinalValue As BooleanState
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToBooleanStateByFinalValue Is Nothing _
						 AndAlso Not FinalValue.Equals(Nothing)  Then
						
					Me._UpToBooleanStateByFinalValue = New BooleanState()
					Me._UpToBooleanStateByFinalValue.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToBooleanStateByFinalValue", Me._UpToBooleanStateByFinalValue)
					Me._UpToBooleanStateByFinalValue.Query.Where(Me._UpToBooleanStateByFinalValue.Query.Id = Me.FinalValue)
					Me._UpToBooleanStateByFinalValue.Query.Load()
				End If

				Return Me._UpToBooleanStateByFinalValue
			End Get
			
            Set(ByVal value As BooleanState)
				Me.RemovePreSave("UpToBooleanStateByFinalValue")
				

				If value Is Nothing Then
				
					Me.FinalValue = Nothing
				
					Me._UpToBooleanStateByFinalValue = Nothing
				Else
				
					Me.FinalValue = value.Id
					
					Me._UpToBooleanStateByFinalValue = value
					Me.SetPreSave("UpToBooleanStateByFinalValue", Me._UpToBooleanStateByFinalValue)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToDynamicCollectionByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Dynamic_CollectionHMI_Boolean_Channels
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
	Partial Public Class HMIBooleanChannelsMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.CollectionName, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.ChannelName, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.ChannelName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 75
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.Description, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.Description
			c.CharacterMaxLength = 75
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.Source, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.Source
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.Destination, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.Destination
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.ResetLatch, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.ResetLatch
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.TempNote, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.TempNote
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.Expression, 7, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.Expression
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.OPCGroup, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.OPCGroup
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.OPCItemID, 9, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.OPCItemID
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.Tooltip, 10, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.Tooltip
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.Note, 11, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.Note
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.FalseText, 12, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.FalseText
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.TrueText, 13, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.TrueText
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.FalseMessageText, 14, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.FalseMessageText
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.TrueMessageText, 15, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.TrueMessageText
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.SoundMessage, 16, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.SoundMessage
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.FormulaVisibleForAdminOnly, 17, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.FormulaVisibleForAdminOnly
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.InitialValue, 18, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.InitialValue
			c.NumericPrecision = 10
			c.Description = "Intitial value when opening the sequence"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.FinalValue, 19, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.FinalValue
			c.NumericPrecision = 10
			c.Description = "Final value when closing the sequence"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.Unavailable, 20, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.FalseMessageTextColor, 21, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.FalseMessageTextColor
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIBooleanChannelsMetadata.ColumnNames.TrueMessageTextColor, 22, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIBooleanChannelsMetadata.PropertyNames.TrueMessageTextColor
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As HMIBooleanChannelsMetadata
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
			 Public Const ResetLatch As String = "ResetLatch"
			 Public Const TempNote As String = "TempNote"
			 Public Const Expression As String = "Expression"
			 Public Const OPCGroup As String = "OPC_Group"
			 Public Const OPCItemID As String = "OPC_ItemID"
			 Public Const Tooltip As String = "Tooltip"
			 Public Const Note As String = "Note"
			 Public Const FalseText As String = "FalseText"
			 Public Const TrueText As String = "TrueText"
			 Public Const FalseMessageText As String = "FalseMessageText"
			 Public Const TrueMessageText As String = "TrueMessageText"
			 Public Const SoundMessage As String = "SoundMessage"
			 Public Const FormulaVisibleForAdminOnly As String = "FormulaVisibleForAdminOnly"
			 Public Const InitialValue As String = "InitialValue"
			 Public Const FinalValue As String = "FinalValue"
			 Public Const Unavailable As String = "Unavailable"
			 Public Const FalseMessageTextColor As String = "FalseMessageTextColor"
			 Public Const TrueMessageTextColor As String = "TrueMessageTextColor"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const CollectionName As String = "CollectionName"
			 Public Const ChannelName As String = "ChannelName"
			 Public Const Description As String = "Description"
			 Public Const Source As String = "Source"
			 Public Const Destination As String = "Destination"
			 Public Const ResetLatch As String = "ResetLatch"
			 Public Const TempNote As String = "TempNote"
			 Public Const Expression As String = "Expression"
			 Public Const OPCGroup As String = "OPCGroup"
			 Public Const OPCItemID As String = "OPCItemID"
			 Public Const Tooltip As String = "Tooltip"
			 Public Const Note As String = "Note"
			 Public Const FalseText As String = "FalseText"
			 Public Const TrueText As String = "TrueText"
			 Public Const FalseMessageText As String = "FalseMessageText"
			 Public Const TrueMessageText As String = "TrueMessageText"
			 Public Const SoundMessage As String = "SoundMessage"
			 Public Const FormulaVisibleForAdminOnly As String = "FormulaVisibleForAdminOnly"
			 Public Const InitialValue As String = "InitialValue"
			 Public Const FinalValue As String = "FinalValue"
			 Public Const Unavailable As String = "Unavailable"
			 Public Const FalseMessageTextColor As String = "FalseMessageTextColor"
			 Public Const TrueMessageTextColor As String = "TrueMessageTextColor"
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
			SyncLock GetType(HMIBooleanChannelsMetadata)
			
				If HMIBooleanChannelsMetadata.mapDelegates Is Nothing Then
					HMIBooleanChannelsMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If HMIBooleanChannelsMetadata._meta Is Nothing Then
					HMIBooleanChannelsMetadata._meta = New HMIBooleanChannelsMetadata()
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
				meta.AddTypeMap("ResetLatch", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("TempNote", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Expression", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("OPCGroup", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OPCItemID", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Tooltip", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Note", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("FalseText", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("TrueText", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("FalseMessageText", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("TrueMessageText", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("SoundMessage", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("FormulaVisibleForAdminOnly", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("InitialValue", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("FinalValue", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("FalseMessageTextColor", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("TrueMessageTextColor", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "HMI_Boolean_Channels"
				meta.Destination = "HMI_Boolean_Channels"
				
				meta.spInsert = "proc_HMI_Boolean_ChannelsInsert"
				meta.spUpdate = "proc_HMI_Boolean_ChannelsUpdate"
				meta.spDelete = "proc_HMI_Boolean_ChannelsDelete"
				meta.spLoadAll = "proc_HMI_Boolean_ChannelsLoadAll"
				meta.spLoadByPrimaryKey = "proc_HMI_Boolean_ChannelsLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As HMIBooleanChannelsMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

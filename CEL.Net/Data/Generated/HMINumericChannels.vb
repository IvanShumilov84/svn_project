
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
	' Encapsulates the 'HMI_Numeric_Channels' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(HMINumericChannels))> _
	<XmlType("HMINumericChannels")> _	
	Partial Public Class HMINumericChannels 
		Inherits esHMINumericChannels
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New HMINumericChannels()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal channelName As System.String)
			Dim obj As New HMINumericChannels()
			obj.CollectionName = collectionName
			obj.ChannelName = channelName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal channelName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New HMINumericChannels()
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
	<XmlType("HMINumericChannelsCollection")> _
	Partial Public Class HMINumericChannelsCollection
		Inherits esHMINumericChannelsCollection
		Implements IEnumerable(Of HMINumericChannels)
	
		Public Function FindByPrimaryKey(ByVal collectionName As System.String, ByVal channelName As System.String) As HMINumericChannels
			Return MyBase.SingleOrDefault(Function(e) e.CollectionName = collectionName And e.ChannelName = channelName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(HMINumericChannels))> _
		Public Class HMINumericChannelsCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of HMINumericChannelsCollection)
			
			Public Shared Widening Operator CType(packet As HMINumericChannelsCollectionWCFPacket) As HMINumericChannelsCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As HMINumericChannelsCollection) As HMINumericChannelsCollectionWCFPacket
				Return New HMINumericChannelsCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class HMINumericChannelsQuery 
		Inherits esHMINumericChannelsQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "HMINumericChannelsQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As HMINumericChannelsQuery) As String
			Return HMINumericChannelsQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As HMINumericChannelsQuery
			Return DirectCast(HMINumericChannelsQuery.SerializeHelper.FromXml(query, GetType(HMINumericChannelsQuery)), HMINumericChannelsQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esHMINumericChannels
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
		
			Dim query As New HMINumericChannelsQuery()
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
		' Maps to HMI_Numeric_Channels.CollectionName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CollectionName As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.CollectionName, value) Then
					Me._UpToDynamicCollectionByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDynamicCollectionByCollectionName")
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.ChannelName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelName As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ChannelName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ChannelName, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ChannelName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.TempNote
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TempNote As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.TempNote)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.TempNote, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.TempNote)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Source
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Source As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.Source)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.Source, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Source)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Destination
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Destination As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.Destination)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.Destination, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Destination)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Expression
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Expression As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.Expression)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.Expression, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Expression)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.OPC_Group
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OPCGroup As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.OPCGroup)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.OPCGroup, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.OPCGroup)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.OPC_ItemID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OPCItemID As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.OPCItemID)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.OPCItemID, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.OPCItemID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Tooltip
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Tooltip As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.Tooltip)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.Tooltip, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Tooltip)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Note)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.DecimalDigits
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DecimalDigits As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMINumericChannelsMetadata.ColumnNames.DecimalDigits)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMINumericChannelsMetadata.ColumnNames.DecimalDigits, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.DecimalDigits)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Format
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Format As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.Format)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.Format, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Format)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Minimum
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Minimum As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMINumericChannelsMetadata.ColumnNames.Minimum)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMINumericChannelsMetadata.ColumnNames.Minimum, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Minimum)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Maximum
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Maximum As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMINumericChannelsMetadata.ColumnNames.Maximum)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMINumericChannelsMetadata.ColumnNames.Maximum, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Maximum)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.EngUnitID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngUnitID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMINumericChannelsMetadata.ColumnNames.EngUnitID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMINumericChannelsMetadata.ColumnNames.EngUnitID, value) Then
					Me._UpToEngineeringUnitByEngUnitID = Nothing
					Me.OnPropertyChanged("UpToEngineeringUnitByEngUnitID")
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.EngUnitID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Mean_Enabled
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MeanEnabled As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.MeanEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.MeanEnabled, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.MeanEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Mean_Period
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MeanPeriod As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMINumericChannelsMetadata.ColumnNames.MeanPeriod)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMINumericChannelsMetadata.ColumnNames.MeanPeriod, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.MeanPeriod)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_HighHigh
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdHighHigh As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHigh)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHigh, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdHighHigh)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_HighHighSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdHighHighSource As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighSource)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighSource, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdHighHighSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_HighHighColor
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdHighHighColor As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighColor)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighColor, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdHighHighColor)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_HighHighMessageText
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdHighHighMessageText As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighMessageText)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighMessageText, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdHighHighMessageText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_High
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdHigh As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(HMINumericChannelsMetadata.ColumnNames.ThresholdHigh)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(HMINumericChannelsMetadata.ColumnNames.ThresholdHigh, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdHigh)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_HighSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdHighSource As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighSource)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighSource, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdHighSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_HighColor
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdHighColor As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighColor)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighColor, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdHighColor)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_HighMessageText
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdHighMessageText As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighMessageText)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdHighMessageText, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdHighMessageText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_LowLow
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdLowLow As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLow)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLow, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdLowLow)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_LowLowSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdLowLowSource As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowSource)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowSource, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdLowLowSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_LowLowColor
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdLowLowColor As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowColor)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowColor, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdLowLowColor)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_LowLowMessageText
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdLowLowMessageText As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowMessageText)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowMessageText, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdLowLowMessageText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_Low
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdLow As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(HMINumericChannelsMetadata.ColumnNames.ThresholdLow)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(HMINumericChannelsMetadata.ColumnNames.ThresholdLow, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdLow)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_LowSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdLowSource As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowSource)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowSource, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdLowSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_LowColor
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdLowColor As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowColor)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowColor, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdLowColor)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_LowMessageText
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdLowMessageText As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowMessageText)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdLowMessageText, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdLowMessageText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_Normal
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdNormal As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(HMINumericChannelsMetadata.ColumnNames.ThresholdNormal)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(HMINumericChannelsMetadata.ColumnNames.ThresholdNormal, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdNormal)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Threshold_NormalColor
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ThresholdNormalColor As System.String
			Get
				Return MyBase.GetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdNormalColor)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMINumericChannelsMetadata.ColumnNames.ThresholdNormalColor, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdNormalColor)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.High_Trigger
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighTrigger As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMINumericChannelsMetadata.ColumnNames.HighTrigger)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMINumericChannelsMetadata.ColumnNames.HighTrigger, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.HighTrigger)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.High_Trigger_Enabled
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighTriggerEnabled As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.HighTriggerEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.HighTriggerEnabled, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.HighTriggerEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Low_Trigger
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowTrigger As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMINumericChannelsMetadata.ColumnNames.LowTrigger)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMINumericChannelsMetadata.ColumnNames.LowTrigger, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.LowTrigger)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Low_Trigger_Enabled
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowTriggerEnabled As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.LowTriggerEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.LowTriggerEnabled, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.LowTriggerEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.SpeechMessageText
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SpeechMessageText As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.SpeechMessageText)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.SpeechMessageText, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.SpeechMessageText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.FormulaVisibleForAdminOnly
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FormulaVisibleForAdminOnly As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.FormulaVisibleForAdminOnly)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.FormulaVisibleForAdminOnly, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.FormulaVisibleForAdminOnly)
				End If
			End Set
		End Property	
			
		' <summary>
		' Initial value when opening the sequence
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property InitialValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(HMINumericChannelsMetadata.ColumnNames.InitialValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(HMINumericChannelsMetadata.ColumnNames.InitialValue, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.InitialValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Final value when closing the sequence
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FinalValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(HMINumericChannelsMetadata.ColumnNames.FinalValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(HMINumericChannelsMetadata.ColumnNames.FinalValue, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.FinalValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Numeric_Channels.Persistent
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Persistent As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.Persistent)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMINumericChannelsMetadata.ColumnNames.Persistent, value) Then
					OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Persistent)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToDynamicCollectionByCollectionName As DynamicCollection
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToEngineeringUnitByEngUnitID As EngineeringUnit
		
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
												
						Case "TempNote"
							Me.str().TempNote = CType(value, string)
												
						Case "Source"
							Me.str().Source = CType(value, string)
												
						Case "Destination"
							Me.str().Destination = CType(value, string)
												
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
												
						Case "DecimalDigits"
							Me.str().DecimalDigits = CType(value, string)
												
						Case "Format"
							Me.str().Format = CType(value, string)
												
						Case "Minimum"
							Me.str().Minimum = CType(value, string)
												
						Case "Maximum"
							Me.str().Maximum = CType(value, string)
												
						Case "EngUnitID"
							Me.str().EngUnitID = CType(value, string)
												
						Case "MeanEnabled"
							Me.str().MeanEnabled = CType(value, string)
												
						Case "MeanPeriod"
							Me.str().MeanPeriod = CType(value, string)
												
						Case "ThresholdHighHigh"
							Me.str().ThresholdHighHigh = CType(value, string)
												
						Case "ThresholdHighHighSource"
							Me.str().ThresholdHighHighSource = CType(value, string)
												
						Case "ThresholdHighHighColor"
							Me.str().ThresholdHighHighColor = CType(value, string)
												
						Case "ThresholdHighHighMessageText"
							Me.str().ThresholdHighHighMessageText = CType(value, string)
												
						Case "ThresholdHigh"
							Me.str().ThresholdHigh = CType(value, string)
												
						Case "ThresholdHighSource"
							Me.str().ThresholdHighSource = CType(value, string)
												
						Case "ThresholdHighColor"
							Me.str().ThresholdHighColor = CType(value, string)
												
						Case "ThresholdHighMessageText"
							Me.str().ThresholdHighMessageText = CType(value, string)
												
						Case "ThresholdLowLow"
							Me.str().ThresholdLowLow = CType(value, string)
												
						Case "ThresholdLowLowSource"
							Me.str().ThresholdLowLowSource = CType(value, string)
												
						Case "ThresholdLowLowColor"
							Me.str().ThresholdLowLowColor = CType(value, string)
												
						Case "ThresholdLowLowMessageText"
							Me.str().ThresholdLowLowMessageText = CType(value, string)
												
						Case "ThresholdLow"
							Me.str().ThresholdLow = CType(value, string)
												
						Case "ThresholdLowSource"
							Me.str().ThresholdLowSource = CType(value, string)
												
						Case "ThresholdLowColor"
							Me.str().ThresholdLowColor = CType(value, string)
												
						Case "ThresholdLowMessageText"
							Me.str().ThresholdLowMessageText = CType(value, string)
												
						Case "ThresholdNormal"
							Me.str().ThresholdNormal = CType(value, string)
												
						Case "ThresholdNormalColor"
							Me.str().ThresholdNormalColor = CType(value, string)
												
						Case "HighTrigger"
							Me.str().HighTrigger = CType(value, string)
												
						Case "HighTriggerEnabled"
							Me.str().HighTriggerEnabled = CType(value, string)
												
						Case "LowTrigger"
							Me.str().LowTrigger = CType(value, string)
												
						Case "LowTriggerEnabled"
							Me.str().LowTriggerEnabled = CType(value, string)
												
						Case "SpeechMessageText"
							Me.str().SpeechMessageText = CType(value, string)
												
						Case "FormulaVisibleForAdminOnly"
							Me.str().FormulaVisibleForAdminOnly = CType(value, string)
												
						Case "InitialValue"
							Me.str().InitialValue = CType(value, string)
												
						Case "FinalValue"
							Me.str().FinalValue = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
												
						Case "Persistent"
							Me.str().Persistent = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "DecimalDigits"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.DecimalDigits = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.DecimalDigits)
							End If
						
						Case "Minimum"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Minimum = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Minimum)
							End If
						
						Case "Maximum"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Maximum = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Maximum)
							End If
						
						Case "EngUnitID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.EngUnitID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.EngUnitID)
							End If
						
						Case "MeanEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.MeanEnabled = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.MeanEnabled)
							End If
						
						Case "MeanPeriod"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.MeanPeriod = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.MeanPeriod)
							End If
						
						Case "ThresholdHighHigh"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ThresholdHighHigh = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdHighHigh)
							End If
						
						Case "ThresholdHigh"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ThresholdHigh = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdHigh)
							End If
						
						Case "ThresholdLowLow"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ThresholdLowLow = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdLowLow)
							End If
						
						Case "ThresholdLow"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ThresholdLow = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdLow)
							End If
						
						Case "ThresholdNormal"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ThresholdNormal = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.ThresholdNormal)
							End If
						
						Case "HighTrigger"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.HighTrigger = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.HighTrigger)
							End If
						
						Case "HighTriggerEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.HighTriggerEnabled = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.HighTriggerEnabled)
							End If
						
						Case "LowTrigger"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LowTrigger = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.LowTrigger)
							End If
						
						Case "LowTriggerEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.LowTriggerEnabled = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.LowTriggerEnabled)
							End If
						
						Case "SpeechMessageText"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.SpeechMessageText = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.SpeechMessageText)
							End If
						
						Case "FormulaVisibleForAdminOnly"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.FormulaVisibleForAdminOnly = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.FormulaVisibleForAdminOnly)
							End If
						
						Case "InitialValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.InitialValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.InitialValue)
							End If
						
						Case "FinalValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.FinalValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.FinalValue)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Unavailable)
							End If
						
						Case "Persistent"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Persistent = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMINumericChannelsMetadata.PropertyNames.Persistent)
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
		
			Public Sub New(ByVal entity As esHMINumericChannels)
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
		  	
			Public Property DecimalDigits As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.DecimalDigits
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DecimalDigits = Nothing
					Else
						entity.DecimalDigits = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Format As System.String 
				Get
					Dim data_ As System.String = entity.Format
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Format = Nothing
					Else
						entity.Format = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Minimum As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Minimum
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Minimum = Nothing
					Else
						entity.Minimum = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Maximum As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Maximum
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Maximum = Nothing
					Else
						entity.Maximum = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property EngUnitID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.EngUnitID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EngUnitID = Nothing
					Else
						entity.EngUnitID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MeanEnabled As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.MeanEnabled
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MeanEnabled = Nothing
					Else
						entity.MeanEnabled = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property MeanPeriod As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.MeanPeriod
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MeanPeriod = Nothing
					Else
						entity.MeanPeriod = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdHighHigh As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ThresholdHighHigh
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdHighHigh = Nothing
					Else
						entity.ThresholdHighHigh = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdHighHighSource As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdHighHighSource
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdHighHighSource = Nothing
					Else
						entity.ThresholdHighHighSource = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdHighHighColor As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdHighHighColor
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdHighHighColor = Nothing
					Else
						entity.ThresholdHighHighColor = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdHighHighMessageText As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdHighHighMessageText
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdHighHighMessageText = Nothing
					Else
						entity.ThresholdHighHighMessageText = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdHigh As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ThresholdHigh
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdHigh = Nothing
					Else
						entity.ThresholdHigh = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdHighSource As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdHighSource
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdHighSource = Nothing
					Else
						entity.ThresholdHighSource = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdHighColor As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdHighColor
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdHighColor = Nothing
					Else
						entity.ThresholdHighColor = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdHighMessageText As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdHighMessageText
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdHighMessageText = Nothing
					Else
						entity.ThresholdHighMessageText = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdLowLow As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ThresholdLowLow
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdLowLow = Nothing
					Else
						entity.ThresholdLowLow = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdLowLowSource As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdLowLowSource
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdLowLowSource = Nothing
					Else
						entity.ThresholdLowLowSource = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdLowLowColor As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdLowLowColor
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdLowLowColor = Nothing
					Else
						entity.ThresholdLowLowColor = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdLowLowMessageText As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdLowLowMessageText
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdLowLowMessageText = Nothing
					Else
						entity.ThresholdLowLowMessageText = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdLow As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ThresholdLow
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdLow = Nothing
					Else
						entity.ThresholdLow = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdLowSource As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdLowSource
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdLowSource = Nothing
					Else
						entity.ThresholdLowSource = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdLowColor As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdLowColor
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdLowColor = Nothing
					Else
						entity.ThresholdLowColor = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdLowMessageText As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdLowMessageText
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdLowMessageText = Nothing
					Else
						entity.ThresholdLowMessageText = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdNormal As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ThresholdNormal
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdNormal = Nothing
					Else
						entity.ThresholdNormal = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property ThresholdNormalColor As System.String 
				Get
					Dim data_ As System.String = entity.ThresholdNormalColor
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ThresholdNormalColor = Nothing
					Else
						entity.ThresholdNormalColor = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighTrigger As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.HighTrigger
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighTrigger = Nothing
					Else
						entity.HighTrigger = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighTriggerEnabled As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.HighTriggerEnabled
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighTriggerEnabled = Nothing
					Else
						entity.HighTriggerEnabled = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowTrigger As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.LowTrigger
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowTrigger = Nothing
					Else
						entity.LowTrigger = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowTriggerEnabled As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.LowTriggerEnabled
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowTriggerEnabled = Nothing
					Else
						entity.LowTriggerEnabled = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property SpeechMessageText As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.SpeechMessageText
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SpeechMessageText = Nothing
					Else
						entity.SpeechMessageText = Convert.ToBoolean(Value)
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
					Dim data_ As Nullable(Of System.Double) = entity.InitialValue
					
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
						entity.InitialValue = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property FinalValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.FinalValue
					
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
						entity.FinalValue = Convert.ToDouble(Value)
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
		  	
			Public Property Persistent As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Persistent
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Persistent = Nothing
					Else
						entity.Persistent = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  

			Private entity As esHMINumericChannels
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return HMINumericChannelsMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As HMINumericChannelsQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New HMINumericChannelsQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As HMINumericChannelsQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As HMINumericChannelsQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As HMINumericChannelsQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esHMINumericChannelsCollection
		Inherits esEntityCollection(Of HMINumericChannels)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return HMINumericChannelsMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "HMINumericChannelsCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As HMINumericChannelsQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New HMINumericChannelsQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As HMINumericChannelsQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New HMINumericChannelsQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As HMINumericChannelsQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, HMINumericChannelsQuery))
		End Sub
		
		#End Region
						
		Private m_query As HMINumericChannelsQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esHMINumericChannelsQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return HMINumericChannelsMetadata.Meta()
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
				Case "TempNote" 
					Return Me.TempNote
				Case "Source" 
					Return Me.Source
				Case "Destination" 
					Return Me.Destination
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
				Case "DecimalDigits" 
					Return Me.DecimalDigits
				Case "Format" 
					Return Me.Format
				Case "Minimum" 
					Return Me.Minimum
				Case "Maximum" 
					Return Me.Maximum
				Case "EngUnitID" 
					Return Me.EngUnitID
				Case "MeanEnabled" 
					Return Me.MeanEnabled
				Case "MeanPeriod" 
					Return Me.MeanPeriod
				Case "ThresholdHighHigh" 
					Return Me.ThresholdHighHigh
				Case "ThresholdHighHighSource" 
					Return Me.ThresholdHighHighSource
				Case "ThresholdHighHighColor" 
					Return Me.ThresholdHighHighColor
				Case "ThresholdHighHighMessageText" 
					Return Me.ThresholdHighHighMessageText
				Case "ThresholdHigh" 
					Return Me.ThresholdHigh
				Case "ThresholdHighSource" 
					Return Me.ThresholdHighSource
				Case "ThresholdHighColor" 
					Return Me.ThresholdHighColor
				Case "ThresholdHighMessageText" 
					Return Me.ThresholdHighMessageText
				Case "ThresholdLowLow" 
					Return Me.ThresholdLowLow
				Case "ThresholdLowLowSource" 
					Return Me.ThresholdLowLowSource
				Case "ThresholdLowLowColor" 
					Return Me.ThresholdLowLowColor
				Case "ThresholdLowLowMessageText" 
					Return Me.ThresholdLowLowMessageText
				Case "ThresholdLow" 
					Return Me.ThresholdLow
				Case "ThresholdLowSource" 
					Return Me.ThresholdLowSource
				Case "ThresholdLowColor" 
					Return Me.ThresholdLowColor
				Case "ThresholdLowMessageText" 
					Return Me.ThresholdLowMessageText
				Case "ThresholdNormal" 
					Return Me.ThresholdNormal
				Case "ThresholdNormalColor" 
					Return Me.ThresholdNormalColor
				Case "HighTrigger" 
					Return Me.HighTrigger
				Case "HighTriggerEnabled" 
					Return Me.HighTriggerEnabled
				Case "LowTrigger" 
					Return Me.LowTrigger
				Case "LowTriggerEnabled" 
					Return Me.LowTriggerEnabled
				Case "SpeechMessageText" 
					Return Me.SpeechMessageText
				Case "FormulaVisibleForAdminOnly" 
					Return Me.FormulaVisibleForAdminOnly
				Case "InitialValue" 
					Return Me.InitialValue
				Case "FinalValue" 
					Return Me.FinalValue
				Case "Unavailable" 
					Return Me.Unavailable
				Case "Persistent" 
					Return Me.Persistent
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelName As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ChannelName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TempNote As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.TempNote, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Source As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Source, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Destination As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Destination, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Expression As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Expression, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OPCGroup As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.OPCGroup, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OPCItemID As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.OPCItemID, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Tooltip As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Tooltip, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DecimalDigits As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.DecimalDigits, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Format As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Format, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Minimum As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Minimum, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Maximum As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Maximum, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngUnitID As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.EngUnitID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MeanEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.MeanEnabled, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property MeanPeriod As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.MeanPeriod, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdHighHigh As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdHighHigh, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdHighHighSource As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighSource, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdHighHighColor As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighColor, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdHighHighMessageText As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighMessageText, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdHigh As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdHigh, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdHighSource As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdHighSource, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdHighColor As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdHighColor, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdHighMessageText As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdHighMessageText, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdLowLow As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdLowLow, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdLowLowSource As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowSource, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdLowLowColor As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowColor, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdLowLowMessageText As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowMessageText, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdLow As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdLow, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdLowSource As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdLowSource, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdLowColor As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdLowColor, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdLowMessageText As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdLowMessageText, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdNormal As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdNormal, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property ThresholdNormalColor As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.ThresholdNormalColor, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property HighTrigger As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.HighTrigger, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property HighTriggerEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.HighTriggerEnabled, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property LowTrigger As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.LowTrigger, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property LowTriggerEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.LowTriggerEnabled, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property SpeechMessageText As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.SpeechMessageText, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property FormulaVisibleForAdminOnly As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.FormulaVisibleForAdminOnly, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property InitialValue As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.InitialValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property FinalValue As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.FinalValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Persistent As esQueryItem
			Get
				Return New esQueryItem(Me, HMINumericChannelsMetadata.ColumnNames.Persistent, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class HMINumericChannels 
		Inherits esHMINumericChannels
		
	
		#Region "UpToDynamicCollectionByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Dynamic_CollectionHMI_Numeric_Channels
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

		#Region "UpToEngineeringUnitByEngUnitID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - EngineeringUnitHMI_Numeric_Channels
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToEngineeringUnitByEngUnitID As EngineeringUnit
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToEngineeringUnitByEngUnitID Is Nothing _
						 AndAlso Not EngUnitID.Equals(Nothing)  Then
						
					Me._UpToEngineeringUnitByEngUnitID = New EngineeringUnit()
					Me._UpToEngineeringUnitByEngUnitID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToEngineeringUnitByEngUnitID", Me._UpToEngineeringUnitByEngUnitID)
					Me._UpToEngineeringUnitByEngUnitID.Query.Where(Me._UpToEngineeringUnitByEngUnitID.Query.Id = Me.EngUnitID)
					Me._UpToEngineeringUnitByEngUnitID.Query.Load()
				End If

				Return Me._UpToEngineeringUnitByEngUnitID
			End Get
			
            Set(ByVal value As EngineeringUnit)
				Me.RemovePreSave("UpToEngineeringUnitByEngUnitID")
				

				If value Is Nothing Then
				
					Me.EngUnitID = Nothing
				
					Me._UpToEngineeringUnitByEngUnitID = Nothing
				Else
				
					Me.EngUnitID = value.Id
					
					Me._UpToEngineeringUnitByEngUnitID = value
					Me.SetPreSave("UpToEngineeringUnitByEngUnitID", Me._UpToEngineeringUnitByEngUnitID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class HMINumericChannelsMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.CollectionName, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ChannelName, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ChannelName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 100
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Description, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Description
			c.CharacterMaxLength = 75
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.TempNote, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.TempNote
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Source, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Source
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Destination, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Destination
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Expression, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Expression
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.OPCGroup, 7, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.OPCGroup
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.OPCItemID, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.OPCItemID
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Tooltip, 9, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Tooltip
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Note, 10, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Note
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.DecimalDigits, 11, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.DecimalDigits
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Format, 12, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Format
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Minimum, 13, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Minimum
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Maximum, 14, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Maximum
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.EngUnitID, 15, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.EngUnitID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.MeanEnabled, 16, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.MeanEnabled
			c.CharacterMaxLength = 2
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.MeanPeriod, 17, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.MeanPeriod
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHigh, 18, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdHighHigh
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighSource, 19, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdHighHighSource
			c.CharacterMaxLength = 75
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighColor, 20, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdHighHighColor
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdHighHighMessageText, 21, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdHighHighMessageText
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdHigh, 22, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdHigh
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdHighSource, 23, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdHighSource
			c.CharacterMaxLength = 75
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdHighColor, 24, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdHighColor
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdHighMessageText, 25, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdHighMessageText
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLow, 26, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdLowLow
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowSource, 27, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdLowLowSource
			c.CharacterMaxLength = 75
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowColor, 28, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdLowLowColor
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdLowLowMessageText, 29, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdLowLowMessageText
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdLow, 30, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdLow
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdLowSource, 31, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdLowSource
			c.CharacterMaxLength = 75
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdLowColor, 32, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdLowColor
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdLowMessageText, 33, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdLowMessageText
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdNormal, 34, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdNormal
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.ThresholdNormalColor, 35, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.ThresholdNormalColor
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.HighTrigger, 36, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.HighTrigger
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.HighTriggerEnabled, 37, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.HighTriggerEnabled
			c.CharacterMaxLength = 2
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.LowTrigger, 38, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.LowTrigger
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.LowTriggerEnabled, 39, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.LowTriggerEnabled
			c.CharacterMaxLength = 2
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.SpeechMessageText, 40, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.SpeechMessageText
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.FormulaVisibleForAdminOnly, 41, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.FormulaVisibleForAdminOnly
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.InitialValue, 42, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.InitialValue
			c.NumericPrecision = 15
			c.Description = "Initial value when opening the sequence"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.FinalValue, 43, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.FinalValue
			c.NumericPrecision = 15
			c.Description = "Final value when closing the sequence"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Unavailable, 44, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMINumericChannelsMetadata.ColumnNames.Persistent, 45, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMINumericChannelsMetadata.PropertyNames.Persistent
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As HMINumericChannelsMetadata
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
			 Public Const TempNote As String = "TempNote"
			 Public Const Source As String = "Source"
			 Public Const Destination As String = "Destination"
			 Public Const Expression As String = "Expression"
			 Public Const OPCGroup As String = "OPC_Group"
			 Public Const OPCItemID As String = "OPC_ItemID"
			 Public Const Tooltip As String = "Tooltip"
			 Public Const Note As String = "Note"
			 Public Const DecimalDigits As String = "DecimalDigits"
			 Public Const Format As String = "Format"
			 Public Const Minimum As String = "Minimum"
			 Public Const Maximum As String = "Maximum"
			 Public Const EngUnitID As String = "EngUnitID"
			 Public Const MeanEnabled As String = "Mean_Enabled"
			 Public Const MeanPeriod As String = "Mean_Period"
			 Public Const ThresholdHighHigh As String = "Threshold_HighHigh"
			 Public Const ThresholdHighHighSource As String = "Threshold_HighHighSource"
			 Public Const ThresholdHighHighColor As String = "Threshold_HighHighColor"
			 Public Const ThresholdHighHighMessageText As String = "Threshold_HighHighMessageText"
			 Public Const ThresholdHigh As String = "Threshold_High"
			 Public Const ThresholdHighSource As String = "Threshold_HighSource"
			 Public Const ThresholdHighColor As String = "Threshold_HighColor"
			 Public Const ThresholdHighMessageText As String = "Threshold_HighMessageText"
			 Public Const ThresholdLowLow As String = "Threshold_LowLow"
			 Public Const ThresholdLowLowSource As String = "Threshold_LowLowSource"
			 Public Const ThresholdLowLowColor As String = "Threshold_LowLowColor"
			 Public Const ThresholdLowLowMessageText As String = "Threshold_LowLowMessageText"
			 Public Const ThresholdLow As String = "Threshold_Low"
			 Public Const ThresholdLowSource As String = "Threshold_LowSource"
			 Public Const ThresholdLowColor As String = "Threshold_LowColor"
			 Public Const ThresholdLowMessageText As String = "Threshold_LowMessageText"
			 Public Const ThresholdNormal As String = "Threshold_Normal"
			 Public Const ThresholdNormalColor As String = "Threshold_NormalColor"
			 Public Const HighTrigger As String = "High_Trigger"
			 Public Const HighTriggerEnabled As String = "High_Trigger_Enabled"
			 Public Const LowTrigger As String = "Low_Trigger"
			 Public Const LowTriggerEnabled As String = "Low_Trigger_Enabled"
			 Public Const SpeechMessageText As String = "SpeechMessageText"
			 Public Const FormulaVisibleForAdminOnly As String = "FormulaVisibleForAdminOnly"
			 Public Const InitialValue As String = "InitialValue"
			 Public Const FinalValue As String = "FinalValue"
			 Public Const Unavailable As String = "Unavailable"
			 Public Const Persistent As String = "Persistent"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const CollectionName As String = "CollectionName"
			 Public Const ChannelName As String = "ChannelName"
			 Public Const Description As String = "Description"
			 Public Const TempNote As String = "TempNote"
			 Public Const Source As String = "Source"
			 Public Const Destination As String = "Destination"
			 Public Const Expression As String = "Expression"
			 Public Const OPCGroup As String = "OPCGroup"
			 Public Const OPCItemID As String = "OPCItemID"
			 Public Const Tooltip As String = "Tooltip"
			 Public Const Note As String = "Note"
			 Public Const DecimalDigits As String = "DecimalDigits"
			 Public Const Format As String = "Format"
			 Public Const Minimum As String = "Minimum"
			 Public Const Maximum As String = "Maximum"
			 Public Const EngUnitID As String = "EngUnitID"
			 Public Const MeanEnabled As String = "MeanEnabled"
			 Public Const MeanPeriod As String = "MeanPeriod"
			 Public Const ThresholdHighHigh As String = "ThresholdHighHigh"
			 Public Const ThresholdHighHighSource As String = "ThresholdHighHighSource"
			 Public Const ThresholdHighHighColor As String = "ThresholdHighHighColor"
			 Public Const ThresholdHighHighMessageText As String = "ThresholdHighHighMessageText"
			 Public Const ThresholdHigh As String = "ThresholdHigh"
			 Public Const ThresholdHighSource As String = "ThresholdHighSource"
			 Public Const ThresholdHighColor As String = "ThresholdHighColor"
			 Public Const ThresholdHighMessageText As String = "ThresholdHighMessageText"
			 Public Const ThresholdLowLow As String = "ThresholdLowLow"
			 Public Const ThresholdLowLowSource As String = "ThresholdLowLowSource"
			 Public Const ThresholdLowLowColor As String = "ThresholdLowLowColor"
			 Public Const ThresholdLowLowMessageText As String = "ThresholdLowLowMessageText"
			 Public Const ThresholdLow As String = "ThresholdLow"
			 Public Const ThresholdLowSource As String = "ThresholdLowSource"
			 Public Const ThresholdLowColor As String = "ThresholdLowColor"
			 Public Const ThresholdLowMessageText As String = "ThresholdLowMessageText"
			 Public Const ThresholdNormal As String = "ThresholdNormal"
			 Public Const ThresholdNormalColor As String = "ThresholdNormalColor"
			 Public Const HighTrigger As String = "HighTrigger"
			 Public Const HighTriggerEnabled As String = "HighTriggerEnabled"
			 Public Const LowTrigger As String = "LowTrigger"
			 Public Const LowTriggerEnabled As String = "LowTriggerEnabled"
			 Public Const SpeechMessageText As String = "SpeechMessageText"
			 Public Const FormulaVisibleForAdminOnly As String = "FormulaVisibleForAdminOnly"
			 Public Const InitialValue As String = "InitialValue"
			 Public Const FinalValue As String = "FinalValue"
			 Public Const Unavailable As String = "Unavailable"
			 Public Const Persistent As String = "Persistent"
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
			SyncLock GetType(HMINumericChannelsMetadata)
			
				If HMINumericChannelsMetadata.mapDelegates Is Nothing Then
					HMINumericChannelsMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If HMINumericChannelsMetadata._meta Is Nothing Then
					HMINumericChannelsMetadata._meta = New HMINumericChannelsMetadata()
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
				meta.AddTypeMap("TempNote", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Source", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Destination", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Expression", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("OPCGroup", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OPCItemID", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Tooltip", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Note", new esTypeMap("Memo", "System.String"))
				meta.AddTypeMap("DecimalDigits", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Format", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Minimum", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Maximum", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EngUnitID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MeanEnabled", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("MeanPeriod", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ThresholdHighHigh", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("ThresholdHighHighSource", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdHighHighColor", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdHighHighMessageText", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdHigh", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("ThresholdHighSource", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdHighColor", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdHighMessageText", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdLowLow", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("ThresholdLowLowSource", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdLowLowColor", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdLowLowMessageText", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdLow", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("ThresholdLowSource", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdLowColor", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdLowMessageText", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ThresholdNormal", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("ThresholdNormalColor", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("HighTrigger", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("HighTriggerEnabled", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("LowTrigger", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("LowTriggerEnabled", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("SpeechMessageText", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("FormulaVisibleForAdminOnly", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("InitialValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("FinalValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Persistent", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "HMI_Numeric_Channels"
				meta.Destination = "HMI_Numeric_Channels"
				
				meta.spInsert = "proc_HMI_Numeric_ChannelsInsert"
				meta.spUpdate = "proc_HMI_Numeric_ChannelsUpdate"
				meta.spDelete = "proc_HMI_Numeric_ChannelsDelete"
				meta.spLoadAll = "proc_HMI_Numeric_ChannelsLoadAll"
				meta.spLoadByPrimaryKey = "proc_HMI_Numeric_ChannelsLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As HMINumericChannelsMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

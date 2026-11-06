
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 12/14/2018 10:45:37 AM
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
	' Encapsulates the 'DacnTemplateReal' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(DacnTemplateReal))> _
	<XmlType("DacnTemplateReal")> _	
	Partial Public Class DacnTemplateReal 
		Inherits esDacnTemplateReal
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New DacnTemplateReal()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal name As System.String)
			Dim obj As New DacnTemplateReal()
			obj.ProjectID = projectID
			obj.Name = name
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal name As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New DacnTemplateReal()
			obj.ProjectID = projectID
			obj.Name = name
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("DacnTemplateRealCollection")> _
	Partial Public Class DacnTemplateRealCollection
		Inherits esDacnTemplateRealCollection
		Implements IEnumerable(Of DacnTemplateReal)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal name As System.String) As DacnTemplateReal
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.Name = name)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(DacnTemplateReal))> _
		Public Class DacnTemplateRealCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of DacnTemplateRealCollection)
			
			Public Shared Widening Operator CType(packet As DacnTemplateRealCollectionWCFPacket) As DacnTemplateRealCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As DacnTemplateRealCollection) As DacnTemplateRealCollectionWCFPacket
				Return New DacnTemplateRealCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class DacnTemplateRealQuery 
		Inherits esDacnTemplateRealQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "DacnTemplateRealQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As DacnTemplateRealQuery) As String
			Return DacnTemplateRealQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As DacnTemplateRealQuery
			Return DirectCast(DacnTemplateRealQuery.SerializeHelper.FromXml(query, GetType(DacnTemplateRealQuery)), DacnTemplateRealQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDacnTemplateReal
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal name As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, name)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, name)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal name As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, name)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, name)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal name As System.String) As Boolean
		
			Dim query As New DacnTemplateRealQuery()
			query.Where(query.ProjectID = projectID And query.Name = name)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal name As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("Name", name)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to DacnTemplateReal.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToProjectInfoByProjectID = Nothing
					Me.OnPropertyChanged("UpToProjectInfoByProjectID")
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Name As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Name, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.RdataID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RdataID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.RdataID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.RdataID, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RdataID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.FieldEquipmentID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FieldEquipmentID As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.FieldEquipmentID)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.FieldEquipmentID, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.FieldEquipmentID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.DeviceID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DeviceID As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.DeviceID)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.DeviceID, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DeviceID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.In_Channel_Type
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property InChannelType As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.InChannelType)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.InChannelType, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.InChannelType)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Channel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Channel As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.Channel)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.Channel, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Channel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Base_Address
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property BaseAddress As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.BaseAddress)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.BaseAddress, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.BaseAddress)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Physical_Channel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PhysicalChannel As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.PhysicalChannel)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.PhysicalChannel, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.PhysicalChannel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.MinDCValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MinDCValue As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.MinDCValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.MinDCValue, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.MinDCValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.MaxDCValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MaxDCValue As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.MaxDCValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.MaxDCValue, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.MaxDCValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.TerminalConfiguration
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TerminalConfiguration As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.TerminalConfiguration)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.TerminalConfiguration, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TerminalConfiguration)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.AlarmOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AlarmOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.AlarmOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.AlarmOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.AlarmOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.StatisticsOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StatisticsOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.StatisticsOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.StatisticsOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.StatisticsOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.TextConversionOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TextConversionOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.TextConversionOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.TextConversionOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TextConversionOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.DataAcquisitionOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DataAcquisitionOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.DataAcquisitionOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.DataAcquisitionOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DataAcquisitionOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.DataProcessingOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DataProcessingOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.DataProcessingOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.DataProcessingOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DataProcessingOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Period in msec
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PeriodInMsec As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(DacnTemplateRealMetadata.ColumnNames.PeriodInMsec)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(DacnTemplateRealMetadata.ColumnNames.PeriodInMsec, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.PeriodInMsec)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.TreatOnOption
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TreatOnOption As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.TreatOnOption)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.TreatOnOption, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TreatOnOption)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.DefaultValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DefaultValue As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.DefaultValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.DefaultValue, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DefaultValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.UpperLimit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UpperLimit As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.UpperLimit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.UpperLimit, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.UpperLimit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowerLimit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowerLimit As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowerLimit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowerLimit, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowerLimit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.DeltaVPlus
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DeltaVPlus As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.DeltaVPlus)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.DeltaVPlus, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DeltaVPlus)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.DeltaVMinus
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DeltaVMinus As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.DeltaVMinus)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.DeltaVMinus, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DeltaVMinus)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.RocPlus
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RocPlus As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.RocPlus)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.RocPlus, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RocPlus)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.RocMinus
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RocMinus As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.RocMinus)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.RocMinus, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RocMinus)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.EventOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EventOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.EventOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.EventOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.EventOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighHighLimit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighHighLimit As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighHighLimit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighHighLimit, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighLimit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighLimit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighLimit As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighLimit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighLimit, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighLimit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowLimit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLimit As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowLimit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowLimit, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLimit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowLowLimit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLowLimit As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowLowLimit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowLowLimit, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowLimit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighHighAlarm
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighHighAlarm As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighHighAlarm)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighHighAlarm, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighAlarm)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighAlarm
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighAlarm As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighAlarm)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighAlarm, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighAlarm)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowAlarm
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowAlarm As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowAlarm)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowAlarm, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowAlarm)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowLowAlarm
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLowAlarm As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowLowAlarm)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowLowAlarm, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowAlarm)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighHighBand
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighHighBand As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighHighBand)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighHighBand, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighBand)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighBand
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighBand As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighBand)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.HighBand, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighBand)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowBand
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowBand As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowBand)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowBand, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowBand)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowLowBand
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLowBand As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowLowBand)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.LowLowBand, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowBand)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighHighAlarmOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighHighAlarmOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.HighHighAlarmOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.HighHighAlarmOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighAlarmOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighAlarmOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighAlarmOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.HighAlarmOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.HighAlarmOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighAlarmOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowAlarmOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowAlarmOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.LowAlarmOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.LowAlarmOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowAlarmOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowLowAlarmOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLowAlarmOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.LowLowAlarmOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.LowLowAlarmOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowAlarmOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighHighAlarmSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighHighAlarmSource As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.HighHighAlarmSource)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.HighHighAlarmSource, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighAlarmSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighAlarmSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighAlarmSource As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.HighAlarmSource)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.HighAlarmSource, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighAlarmSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowAlarmSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowAlarmSource As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.LowAlarmSource)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.LowAlarmSource, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowAlarmSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowLowAlarmSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLowAlarmSource As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.LowLowAlarmSource)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.LowLowAlarmSource, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowAlarmSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.TextPvFormat
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TextPvFormat As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.TextPvFormat)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.TextPvFormat, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TextPvFormat)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HMIAliasName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HMIAliasName As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.HMIAliasName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.HMIAliasName, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HMIAliasName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.FILEAliasName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FILEAliasName As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.FILEAliasName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.FILEAliasName, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.FILEAliasName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.EngUnit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngUnit As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.EngUnit)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.EngUnit, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.EngUnit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.EngUnitID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngUnitID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.EngUnitID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.EngUnitID, value) Then
					Me._UpToEngineeringUnitByEngUnitID = Nothing
					Me.OnPropertyChanged("UpToEngineeringUnitByEngUnitID")
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.EngUnitID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.MinMaxOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MinMaxOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.MinMaxOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.MinMaxOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.MinMaxOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.DeltaVOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DeltaVOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.DeltaVOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.DeltaVOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DeltaVOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.DeltaTOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DeltaTOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.DeltaTOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.DeltaTOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DeltaTOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.RocOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RocOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.RocOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.RocOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RocOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.SumVOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SumVOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.SumVOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.SumVOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.SumVOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.SumTOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SumTOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.SumTOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.SumTOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.SumTOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.AvgOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AvgOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.AvgOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.AvgOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.AvgOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.AvgTypeID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AvgTypeID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.AvgTypeID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.AvgTypeID, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.AvgTypeID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.AvgPeriod
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AvgPeriod As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.AvgPeriod)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.AvgPeriod, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.AvgPeriod)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.AvgNbSamples
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AvgNbSamples As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.AvgNbSamples)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.AvgNbSamples, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.AvgNbSamples)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.QueueOptionID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property QueueOptionID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.QueueOptionID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.QueueOptionID, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.QueueOptionID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.ArchOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ArchOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.ArchOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.ArchOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ArchOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.ArchRate
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ArchRate As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.ArchRate)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.ArchRate, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ArchRate)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.TrigCondID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TrigCondID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.TrigCondID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.TrigCondID, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TrigCondID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Trig Level
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TrigLevel As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.TrigLevel)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.TrigLevel, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TrigLevel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Trig DeadBand
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TrigDeadBand As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DacnTemplateRealMetadata.ColumnNames.TrigDeadBand)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DacnTemplateRealMetadata.ColumnNames.TrigDeadBand, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TrigDeadBand)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.TimeOut
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TimeOut As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.TimeOut)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.TimeOut, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TimeOut)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.QueueOption
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property QueueOption As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.QueueOption)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.QueueOption, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.QueueOption)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.QueueSize
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property QueueSize As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.QueueSize)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.QueueSize, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.QueueSize)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.PreTrigRecord
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PreTrigRecord As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.PreTrigRecord)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.PreTrigRecord, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.PreTrigRecord)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.MaxRecord
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MaxRecord As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(DacnTemplateRealMetadata.ColumnNames.MaxRecord)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(DacnTemplateRealMetadata.ColumnNames.MaxRecord, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.MaxRecord)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.DacqPreTrig
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DacqPreTrig As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(DacnTemplateRealMetadata.ColumnNames.DacqPreTrig)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(DacnTemplateRealMetadata.ColumnNames.DacqPreTrig, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DacqPreTrig)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.ProcType
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProcType As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.ProcType)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.ProcType, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ProcType)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.PolyDegree
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PolyDegree As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.PolyDegree)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.PolyDegree, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.PolyDegree)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.NumOfArg
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NumOfArg As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.NumOfArg)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.NumOfArg, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NumOfArg)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.NumOfValidArg
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NumOfValidArg As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.NumOfValidArg)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.NumOfValidArg, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NumOfValidArg)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Coeff1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Coeff1 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff1)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff1, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Coeff2
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Coeff2 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff2)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff2, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Coeff3
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Coeff3 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff3)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff3, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff3)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Coeff4
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Coeff4 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff4)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff4, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff4)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Coeff5
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Coeff5 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff5)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff5, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff5)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Coeff6
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Coeff6 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff6)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.Coeff6, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff6)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.NotYetApproved_Coeff1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NotYetApprovedCoeff1 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff1)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff1, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.NotYetApproved_Coeff2
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NotYetApprovedCoeff2 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff2)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff2, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.NotYetApproved_Coeff3
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NotYetApprovedCoeff3 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff3)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff3, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff3)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.NotYetApproved_Coeff4
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NotYetApprovedCoeff4 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff4)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff4, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff4)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.NotYetApproved_Coeff5
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NotYetApprovedCoeff5 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff5)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff5, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff5)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.NotYetApproved_Coeff6
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NotYetApprovedCoeff6 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff6)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff6, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff6)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.WaitingForApproval
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property WaitingForApproval As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.WaitingForApproval)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.WaitingForApproval, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.WaitingForApproval)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg1 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg1)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg1, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg2
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg2 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg2)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg2, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg3
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg3 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg3)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg3, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg3)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg4
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg4 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg4)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg4, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg4)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg5
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg5 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg5)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg5, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg5)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg6
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg6 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg6)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg6, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg6)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg7
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg7 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg7)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg7, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg7)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg8
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg8 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg8)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg8, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg8)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg9
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg9 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg9)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg9, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg9)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg10
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg10 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg10)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg10, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg10)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg11
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg11 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg11)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg11, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg11)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg12
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg12 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg12)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg12, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg12)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg13
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg13 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg13)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg13, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg13)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg14
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg14 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg14)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg14, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg14)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg15
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg15 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg15)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg15, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg15)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg16
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg16 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg16)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg16, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg16)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg17
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg17 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg17)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg17, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg17)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg18
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg18 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg18)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg18, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg18)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg19
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg19 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg19)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg19, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg19)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Arg20
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Arg20 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg20)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Arg20, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Arg20)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighHighLimitOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighHighLimitOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.HighHighLimitOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.HighHighLimitOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighLimitOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighLimitOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighLimitOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.HighLimitOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.HighLimitOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighLimitOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowLimitOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLimitOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.LowLimitOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.LowLimitOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLimitOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowLowLimitOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLowLimitOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.LowLowLimitOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.LowLowLimitOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowLimitOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighHighAlarmName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighHighAlarmName As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.HighHighAlarmName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.HighHighAlarmName, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighAlarmName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.HighAlarmName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HighAlarmName As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.HighAlarmName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.HighAlarmName, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighAlarmName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowAlarmName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowAlarmName As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.LowAlarmName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.LowAlarmName, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowAlarmName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LowLowAlarmName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LowLowAlarmName As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.LowLowAlarmName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.LowLowAlarmName, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowAlarmName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.ArgSrc
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ArgSrc As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.ArgSrc)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.ArgSrc, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ArgSrc)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.ValidArgOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ValidArgOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.ValidArgOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DacnTemplateRealMetadata.ColumnNames.ValidArgOn, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ValidArgOn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Measured value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ReadV1 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV1)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV1, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Measured value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ReadV2 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV2)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV2, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Measured value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ReadV3 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV3)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV3, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV3)
				End If
			End Set
		End Property	
			
		' <summary>
		' Measured value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ReadV4 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV4)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV4, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV4)
				End If
			End Set
		End Property	
			
		' <summary>
		' Measured value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ReadV5 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV5)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV5, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV5)
				End If
			End Set
		End Property	
			
		' <summary>
		' Measured value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ReadV6 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV6)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.ReadV6, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV6)
				End If
			End Set
		End Property	
			
		' <summary>
		' Simulated value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RealV1 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV1)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV1, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Simulated value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RealV2 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV2)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV2, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Simulated value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RealV3 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV3)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV3, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV3)
				End If
			End Set
		End Property	
			
		' <summary>
		' Simulated value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RealV4 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV4)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV4, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV4)
				End If
			End Set
		End Property	
			
		' <summary>
		' Simulated value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RealV5 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV5)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV5, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV5)
				End If
			End Set
		End Property	
			
		' <summary>
		' Simulated value
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RealV6 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV6)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.RealV6, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV6)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Smoothing_Period
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SmoothingPeriod As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.SmoothingPeriod)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.SmoothingPeriod, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.SmoothingPeriod)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.margin_accuracy
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MarginAccuracy As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.MarginAccuracy)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.MarginAccuracy, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.MarginAccuracy)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.OEM_Tolerance
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OEMTolerance As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(DacnTemplateRealMetadata.ColumnNames.OEMTolerance)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(DacnTemplateRealMetadata.ColumnNames.OEMTolerance, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.OEMTolerance)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.nb_p_allowed
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NbPAllowed As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnTemplateRealMetadata.ColumnNames.NbPAllowed)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnTemplateRealMetadata.ColumnNames.NbPAllowed, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NbPAllowed)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Calibration_measured
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationMeasured As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationMeasured)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationMeasured, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.CalibrationMeasured)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Channel_type
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelType As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.ChannelType)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.ChannelType, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ChannelType)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Channel_range
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelRange As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.ChannelRange)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.ChannelRange, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ChannelRange)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Transducer_Range
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TransducerRange As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.TransducerRange)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.TransducerRange, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TransducerRange)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Calibration_range
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationRange As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationRange)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationRange, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.CalibrationRange)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Make1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Make1 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Make1)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Make1, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Make1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Model1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Model1 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Model1)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Model1, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Model1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Serial_Number1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SerialNumber1 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.SerialNumber1)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.SerialNumber1, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.SerialNumber1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Calibration_Due_Date1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationDueDate1 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate1)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate1, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.CalibrationDueDate1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Make2
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Make2 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Make2)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Make2, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Make2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Model2
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Model2 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Model2)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Model2, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Model2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Serial_Number2
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SerialNumber2 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.SerialNumber2)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.SerialNumber2, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.SerialNumber2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Calibration_Due_Date2
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationDueDate2 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate2)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate2, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.CalibrationDueDate2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Make3
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Make3 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Make3)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Make3, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Make3)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Model3
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Model3 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Model3)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Model3, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Model3)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Serial_Number3
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SerialNumber3 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.SerialNumber3)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.SerialNumber3, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.SerialNumber3)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Calibration_Due_Date3
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationDueDate3 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate3)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate3, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.CalibrationDueDate3)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Make4
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Make4 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Make4)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Make4, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Make4)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Model4
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Model4 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Model4)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Model4, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Model4)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Serial_Number4
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SerialNumber4 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.SerialNumber4)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.SerialNumber4, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.SerialNumber4)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Calibration_Due_Date4
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationDueDate4 As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate4)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate4, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.CalibrationDueDate4)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Comments
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Comments As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Comments)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Comments, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Comments)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Calibrated_by
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibratedBy As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibratedBy)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibratedBy, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.CalibratedBy)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Assistant
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Assistant As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Assistant)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Assistant, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Assistant)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Approval
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Approval As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.Approval)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.Approval, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Approval)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.LastCalibDate
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LastCalibDate As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.LastCalibDate)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.LastCalibDate, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LastCalibDate)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.NewCalibDate
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NewCalibDate As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.NewCalibDate)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.NewCalibDate, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NewCalibDate)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.OPC_Group
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OPCGroup As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.OPCGroup)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.OPCGroup, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.OPCGroup)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.OPC_ItemID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OPCItemID As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.OPCItemID)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.OPCItemID, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.OPCItemID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.TC_Transducer_SN
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TCTransducerSN As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.TCTransducerSN)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.TCTransducerSN, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TCTransducerSN)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Calibration_Temp
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationTemp As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationTemp)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationTemp, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.CalibrationTemp)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DacnTemplateReal.Calibration_Humidity
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationHumidity As System.String
			Get
				Return MyBase.GetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationHumidity)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnTemplateRealMetadata.ColumnNames.CalibrationHumidity, value) Then
					OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.CalibrationHumidity)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToEngineeringUnitByEngUnitID As EngineeringUnit
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToProjectInfoByProjectID As ProjectInfo
		
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
												
						Case "Name"
							Me.str().Name = CType(value, string)
												
						Case "RdataID"
							Me.str().RdataID = CType(value, string)
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "FieldEquipmentID"
							Me.str().FieldEquipmentID = CType(value, string)
												
						Case "DeviceID"
							Me.str().DeviceID = CType(value, string)
												
						Case "InChannelType"
							Me.str().InChannelType = CType(value, string)
												
						Case "Channel"
							Me.str().Channel = CType(value, string)
												
						Case "BaseAddress"
							Me.str().BaseAddress = CType(value, string)
												
						Case "PhysicalChannel"
							Me.str().PhysicalChannel = CType(value, string)
												
						Case "MinDCValue"
							Me.str().MinDCValue = CType(value, string)
												
						Case "MaxDCValue"
							Me.str().MaxDCValue = CType(value, string)
												
						Case "TerminalConfiguration"
							Me.str().TerminalConfiguration = CType(value, string)
												
						Case "AlarmOn"
							Me.str().AlarmOn = CType(value, string)
												
						Case "StatisticsOn"
							Me.str().StatisticsOn = CType(value, string)
												
						Case "TextConversionOn"
							Me.str().TextConversionOn = CType(value, string)
												
						Case "DataAcquisitionOn"
							Me.str().DataAcquisitionOn = CType(value, string)
												
						Case "DataProcessingOn"
							Me.str().DataProcessingOn = CType(value, string)
												
						Case "PeriodInMsec"
							Me.str().PeriodInMsec = CType(value, string)
												
						Case "TreatOnOption"
							Me.str().TreatOnOption = CType(value, string)
												
						Case "DefaultValue"
							Me.str().DefaultValue = CType(value, string)
												
						Case "UpperLimit"
							Me.str().UpperLimit = CType(value, string)
												
						Case "LowerLimit"
							Me.str().LowerLimit = CType(value, string)
												
						Case "DeltaVPlus"
							Me.str().DeltaVPlus = CType(value, string)
												
						Case "DeltaVMinus"
							Me.str().DeltaVMinus = CType(value, string)
												
						Case "RocPlus"
							Me.str().RocPlus = CType(value, string)
												
						Case "RocMinus"
							Me.str().RocMinus = CType(value, string)
												
						Case "EventOn"
							Me.str().EventOn = CType(value, string)
												
						Case "HighHighLimit"
							Me.str().HighHighLimit = CType(value, string)
												
						Case "HighLimit"
							Me.str().HighLimit = CType(value, string)
												
						Case "LowLimit"
							Me.str().LowLimit = CType(value, string)
												
						Case "LowLowLimit"
							Me.str().LowLowLimit = CType(value, string)
												
						Case "HighHighAlarm"
							Me.str().HighHighAlarm = CType(value, string)
												
						Case "HighAlarm"
							Me.str().HighAlarm = CType(value, string)
												
						Case "LowAlarm"
							Me.str().LowAlarm = CType(value, string)
												
						Case "LowLowAlarm"
							Me.str().LowLowAlarm = CType(value, string)
												
						Case "HighHighBand"
							Me.str().HighHighBand = CType(value, string)
												
						Case "HighBand"
							Me.str().HighBand = CType(value, string)
												
						Case "LowBand"
							Me.str().LowBand = CType(value, string)
												
						Case "LowLowBand"
							Me.str().LowLowBand = CType(value, string)
												
						Case "HighHighAlarmOn"
							Me.str().HighHighAlarmOn = CType(value, string)
												
						Case "HighAlarmOn"
							Me.str().HighAlarmOn = CType(value, string)
												
						Case "LowAlarmOn"
							Me.str().LowAlarmOn = CType(value, string)
												
						Case "LowLowAlarmOn"
							Me.str().LowLowAlarmOn = CType(value, string)
												
						Case "HighHighAlarmSource"
							Me.str().HighHighAlarmSource = CType(value, string)
												
						Case "HighAlarmSource"
							Me.str().HighAlarmSource = CType(value, string)
												
						Case "LowAlarmSource"
							Me.str().LowAlarmSource = CType(value, string)
												
						Case "LowLowAlarmSource"
							Me.str().LowLowAlarmSource = CType(value, string)
												
						Case "TextPvFormat"
							Me.str().TextPvFormat = CType(value, string)
												
						Case "HMIAliasName"
							Me.str().HMIAliasName = CType(value, string)
												
						Case "FILEAliasName"
							Me.str().FILEAliasName = CType(value, string)
												
						Case "EngUnit"
							Me.str().EngUnit = CType(value, string)
												
						Case "EngUnitID"
							Me.str().EngUnitID = CType(value, string)
												
						Case "MinMaxOn"
							Me.str().MinMaxOn = CType(value, string)
												
						Case "DeltaVOn"
							Me.str().DeltaVOn = CType(value, string)
												
						Case "DeltaTOn"
							Me.str().DeltaTOn = CType(value, string)
												
						Case "RocOn"
							Me.str().RocOn = CType(value, string)
												
						Case "SumVOn"
							Me.str().SumVOn = CType(value, string)
												
						Case "SumTOn"
							Me.str().SumTOn = CType(value, string)
												
						Case "AvgOn"
							Me.str().AvgOn = CType(value, string)
												
						Case "AvgTypeID"
							Me.str().AvgTypeID = CType(value, string)
												
						Case "AvgPeriod"
							Me.str().AvgPeriod = CType(value, string)
												
						Case "AvgNbSamples"
							Me.str().AvgNbSamples = CType(value, string)
												
						Case "QueueOptionID"
							Me.str().QueueOptionID = CType(value, string)
												
						Case "ArchOn"
							Me.str().ArchOn = CType(value, string)
												
						Case "ArchRate"
							Me.str().ArchRate = CType(value, string)
												
						Case "TrigCondID"
							Me.str().TrigCondID = CType(value, string)
												
						Case "TrigLevel"
							Me.str().TrigLevel = CType(value, string)
												
						Case "TrigDeadBand"
							Me.str().TrigDeadBand = CType(value, string)
												
						Case "TimeOut"
							Me.str().TimeOut = CType(value, string)
												
						Case "QueueOption"
							Me.str().QueueOption = CType(value, string)
												
						Case "QueueSize"
							Me.str().QueueSize = CType(value, string)
												
						Case "PreTrigRecord"
							Me.str().PreTrigRecord = CType(value, string)
												
						Case "MaxRecord"
							Me.str().MaxRecord = CType(value, string)
												
						Case "DacqPreTrig"
							Me.str().DacqPreTrig = CType(value, string)
												
						Case "ProcType"
							Me.str().ProcType = CType(value, string)
												
						Case "PolyDegree"
							Me.str().PolyDegree = CType(value, string)
												
						Case "NumOfArg"
							Me.str().NumOfArg = CType(value, string)
												
						Case "NumOfValidArg"
							Me.str().NumOfValidArg = CType(value, string)
												
						Case "Coeff1"
							Me.str().Coeff1 = CType(value, string)
												
						Case "Coeff2"
							Me.str().Coeff2 = CType(value, string)
												
						Case "Coeff3"
							Me.str().Coeff3 = CType(value, string)
												
						Case "Coeff4"
							Me.str().Coeff4 = CType(value, string)
												
						Case "Coeff5"
							Me.str().Coeff5 = CType(value, string)
												
						Case "Coeff6"
							Me.str().Coeff6 = CType(value, string)
												
						Case "NotYetApprovedCoeff1"
							Me.str().NotYetApprovedCoeff1 = CType(value, string)
												
						Case "NotYetApprovedCoeff2"
							Me.str().NotYetApprovedCoeff2 = CType(value, string)
												
						Case "NotYetApprovedCoeff3"
							Me.str().NotYetApprovedCoeff3 = CType(value, string)
												
						Case "NotYetApprovedCoeff4"
							Me.str().NotYetApprovedCoeff4 = CType(value, string)
												
						Case "NotYetApprovedCoeff5"
							Me.str().NotYetApprovedCoeff5 = CType(value, string)
												
						Case "NotYetApprovedCoeff6"
							Me.str().NotYetApprovedCoeff6 = CType(value, string)
												
						Case "WaitingForApproval"
							Me.str().WaitingForApproval = CType(value, string)
												
						Case "Arg1"
							Me.str().Arg1 = CType(value, string)
												
						Case "Arg2"
							Me.str().Arg2 = CType(value, string)
												
						Case "Arg3"
							Me.str().Arg3 = CType(value, string)
												
						Case "Arg4"
							Me.str().Arg4 = CType(value, string)
												
						Case "Arg5"
							Me.str().Arg5 = CType(value, string)
												
						Case "Arg6"
							Me.str().Arg6 = CType(value, string)
												
						Case "Arg7"
							Me.str().Arg7 = CType(value, string)
												
						Case "Arg8"
							Me.str().Arg8 = CType(value, string)
												
						Case "Arg9"
							Me.str().Arg9 = CType(value, string)
												
						Case "Arg10"
							Me.str().Arg10 = CType(value, string)
												
						Case "Arg11"
							Me.str().Arg11 = CType(value, string)
												
						Case "Arg12"
							Me.str().Arg12 = CType(value, string)
												
						Case "Arg13"
							Me.str().Arg13 = CType(value, string)
												
						Case "Arg14"
							Me.str().Arg14 = CType(value, string)
												
						Case "Arg15"
							Me.str().Arg15 = CType(value, string)
												
						Case "Arg16"
							Me.str().Arg16 = CType(value, string)
												
						Case "Arg17"
							Me.str().Arg17 = CType(value, string)
												
						Case "Arg18"
							Me.str().Arg18 = CType(value, string)
												
						Case "Arg19"
							Me.str().Arg19 = CType(value, string)
												
						Case "Arg20"
							Me.str().Arg20 = CType(value, string)
												
						Case "HighHighLimitOn"
							Me.str().HighHighLimitOn = CType(value, string)
												
						Case "HighLimitOn"
							Me.str().HighLimitOn = CType(value, string)
												
						Case "LowLimitOn"
							Me.str().LowLimitOn = CType(value, string)
												
						Case "LowLowLimitOn"
							Me.str().LowLowLimitOn = CType(value, string)
												
						Case "HighHighAlarmName"
							Me.str().HighHighAlarmName = CType(value, string)
												
						Case "HighAlarmName"
							Me.str().HighAlarmName = CType(value, string)
												
						Case "LowAlarmName"
							Me.str().LowAlarmName = CType(value, string)
												
						Case "LowLowAlarmName"
							Me.str().LowLowAlarmName = CType(value, string)
												
						Case "ArgSrc"
							Me.str().ArgSrc = CType(value, string)
												
						Case "ValidArgOn"
							Me.str().ValidArgOn = CType(value, string)
												
						Case "ReadV1"
							Me.str().ReadV1 = CType(value, string)
												
						Case "ReadV2"
							Me.str().ReadV2 = CType(value, string)
												
						Case "ReadV3"
							Me.str().ReadV3 = CType(value, string)
												
						Case "ReadV4"
							Me.str().ReadV4 = CType(value, string)
												
						Case "ReadV5"
							Me.str().ReadV5 = CType(value, string)
												
						Case "ReadV6"
							Me.str().ReadV6 = CType(value, string)
												
						Case "RealV1"
							Me.str().RealV1 = CType(value, string)
												
						Case "RealV2"
							Me.str().RealV2 = CType(value, string)
												
						Case "RealV3"
							Me.str().RealV3 = CType(value, string)
												
						Case "RealV4"
							Me.str().RealV4 = CType(value, string)
												
						Case "RealV5"
							Me.str().RealV5 = CType(value, string)
												
						Case "RealV6"
							Me.str().RealV6 = CType(value, string)
												
						Case "SmoothingPeriod"
							Me.str().SmoothingPeriod = CType(value, string)
												
						Case "MarginAccuracy"
							Me.str().MarginAccuracy = CType(value, string)
												
						Case "OEMTolerance"
							Me.str().OEMTolerance = CType(value, string)
												
						Case "NbPAllowed"
							Me.str().NbPAllowed = CType(value, string)
												
						Case "CalibrationMeasured"
							Me.str().CalibrationMeasured = CType(value, string)
												
						Case "ChannelType"
							Me.str().ChannelType = CType(value, string)
												
						Case "ChannelRange"
							Me.str().ChannelRange = CType(value, string)
												
						Case "TransducerRange"
							Me.str().TransducerRange = CType(value, string)
												
						Case "CalibrationRange"
							Me.str().CalibrationRange = CType(value, string)
												
						Case "Make1"
							Me.str().Make1 = CType(value, string)
												
						Case "Model1"
							Me.str().Model1 = CType(value, string)
												
						Case "SerialNumber1"
							Me.str().SerialNumber1 = CType(value, string)
												
						Case "CalibrationDueDate1"
							Me.str().CalibrationDueDate1 = CType(value, string)
												
						Case "Make2"
							Me.str().Make2 = CType(value, string)
												
						Case "Model2"
							Me.str().Model2 = CType(value, string)
												
						Case "SerialNumber2"
							Me.str().SerialNumber2 = CType(value, string)
												
						Case "CalibrationDueDate2"
							Me.str().CalibrationDueDate2 = CType(value, string)
												
						Case "Make3"
							Me.str().Make3 = CType(value, string)
												
						Case "Model3"
							Me.str().Model3 = CType(value, string)
												
						Case "SerialNumber3"
							Me.str().SerialNumber3 = CType(value, string)
												
						Case "CalibrationDueDate3"
							Me.str().CalibrationDueDate3 = CType(value, string)
												
						Case "Make4"
							Me.str().Make4 = CType(value, string)
												
						Case "Model4"
							Me.str().Model4 = CType(value, string)
												
						Case "SerialNumber4"
							Me.str().SerialNumber4 = CType(value, string)
												
						Case "CalibrationDueDate4"
							Me.str().CalibrationDueDate4 = CType(value, string)
												
						Case "Comments"
							Me.str().Comments = CType(value, string)
												
						Case "CalibratedBy"
							Me.str().CalibratedBy = CType(value, string)
												
						Case "Assistant"
							Me.str().Assistant = CType(value, string)
												
						Case "Approval"
							Me.str().Approval = CType(value, string)
												
						Case "LastCalibDate"
							Me.str().LastCalibDate = CType(value, string)
												
						Case "NewCalibDate"
							Me.str().NewCalibDate = CType(value, string)
												
						Case "OPCGroup"
							Me.str().OPCGroup = CType(value, string)
												
						Case "OPCItemID"
							Me.str().OPCItemID = CType(value, string)
												
						Case "TCTransducerSN"
							Me.str().TCTransducerSN = CType(value, string)
												
						Case "CalibrationTemp"
							Me.str().CalibrationTemp = CType(value, string)
												
						Case "CalibrationHumidity"
							Me.str().CalibrationHumidity = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ProjectID)
							End If
						
						Case "RdataID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.RdataID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RdataID)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "InChannelType"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.InChannelType = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.InChannelType)
							End If
						
						Case "Channel"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Channel = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Channel)
							End If
						
						Case "BaseAddress"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.BaseAddress = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.BaseAddress)
							End If
						
						Case "PhysicalChannel"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.PhysicalChannel = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.PhysicalChannel)
							End If
						
						Case "MinDCValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.MinDCValue = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.MinDCValue)
							End If
						
						Case "MaxDCValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.MaxDCValue = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.MaxDCValue)
							End If
						
						Case "TerminalConfiguration"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.TerminalConfiguration = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TerminalConfiguration)
							End If
						
						Case "AlarmOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.AlarmOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.AlarmOn)
							End If
						
						Case "StatisticsOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.StatisticsOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.StatisticsOn)
							End If
						
						Case "TextConversionOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.TextConversionOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TextConversionOn)
							End If
						
						Case "DataAcquisitionOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.DataAcquisitionOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DataAcquisitionOn)
							End If
						
						Case "DataProcessingOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.DataProcessingOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DataProcessingOn)
							End If
						
						Case "PeriodInMsec"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.PeriodInMsec = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.PeriodInMsec)
							End If
						
						Case "TreatOnOption"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.TreatOnOption = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TreatOnOption)
							End If
						
						Case "DefaultValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.DefaultValue = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DefaultValue)
							End If
						
						Case "UpperLimit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.UpperLimit = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.UpperLimit)
							End If
						
						Case "LowerLimit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.LowerLimit = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowerLimit)
							End If
						
						Case "DeltaVPlus"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.DeltaVPlus = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DeltaVPlus)
							End If
						
						Case "DeltaVMinus"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.DeltaVMinus = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DeltaVMinus)
							End If
						
						Case "RocPlus"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.RocPlus = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RocPlus)
							End If
						
						Case "RocMinus"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.RocMinus = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RocMinus)
							End If
						
						Case "EventOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.EventOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.EventOn)
							End If
						
						Case "HighHighLimit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.HighHighLimit = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighLimit)
							End If
						
						Case "HighLimit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.HighLimit = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighLimit)
							End If
						
						Case "LowLimit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.LowLimit = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLimit)
							End If
						
						Case "LowLowLimit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.LowLowLimit = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowLimit)
							End If
						
						Case "HighHighAlarm"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.HighHighAlarm = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighAlarm)
							End If
						
						Case "HighAlarm"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.HighAlarm = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighAlarm)
							End If
						
						Case "LowAlarm"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.LowAlarm = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowAlarm)
							End If
						
						Case "LowLowAlarm"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.LowLowAlarm = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowAlarm)
							End If
						
						Case "HighHighBand"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.HighHighBand = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighBand)
							End If
						
						Case "HighBand"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.HighBand = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighBand)
							End If
						
						Case "LowBand"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.LowBand = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowBand)
							End If
						
						Case "LowLowBand"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.LowLowBand = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowBand)
							End If
						
						Case "HighHighAlarmOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.HighHighAlarmOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighAlarmOn)
							End If
						
						Case "HighAlarmOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.HighAlarmOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighAlarmOn)
							End If
						
						Case "LowAlarmOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.LowAlarmOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowAlarmOn)
							End If
						
						Case "LowLowAlarmOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.LowLowAlarmOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowAlarmOn)
							End If
						
						Case "HighHighAlarmSource"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.HighHighAlarmSource = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighAlarmSource)
							End If
						
						Case "HighAlarmSource"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.HighAlarmSource = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighAlarmSource)
							End If
						
						Case "LowAlarmSource"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LowAlarmSource = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowAlarmSource)
							End If
						
						Case "LowLowAlarmSource"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LowLowAlarmSource = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowAlarmSource)
							End If
						
						Case "EngUnitID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.EngUnitID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.EngUnitID)
							End If
						
						Case "MinMaxOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.MinMaxOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.MinMaxOn)
							End If
						
						Case "DeltaVOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.DeltaVOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DeltaVOn)
							End If
						
						Case "DeltaTOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.DeltaTOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DeltaTOn)
							End If
						
						Case "RocOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.RocOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RocOn)
							End If
						
						Case "SumVOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.SumVOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.SumVOn)
							End If
						
						Case "SumTOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.SumTOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.SumTOn)
							End If
						
						Case "AvgOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.AvgOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.AvgOn)
							End If
						
						Case "AvgTypeID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.AvgTypeID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.AvgTypeID)
							End If
						
						Case "AvgPeriod"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.AvgPeriod = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.AvgPeriod)
							End If
						
						Case "AvgNbSamples"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.AvgNbSamples = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.AvgNbSamples)
							End If
						
						Case "QueueOptionID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.QueueOptionID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.QueueOptionID)
							End If
						
						Case "ArchOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ArchOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ArchOn)
							End If
						
						Case "ArchRate"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ArchRate = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ArchRate)
							End If
						
						Case "TrigCondID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.TrigCondID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TrigCondID)
							End If
						
						Case "TrigLevel"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.TrigLevel = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TrigLevel)
							End If
						
						Case "TrigDeadBand"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.TrigDeadBand = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TrigDeadBand)
							End If
						
						Case "TimeOut"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.TimeOut = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.TimeOut)
							End If
						
						Case "QueueOption"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.QueueOption = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.QueueOption)
							End If
						
						Case "QueueSize"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.QueueSize = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.QueueSize)
							End If
						
						Case "PreTrigRecord"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.PreTrigRecord = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.PreTrigRecord)
							End If
						
						Case "MaxRecord"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.MaxRecord = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.MaxRecord)
							End If
						
						Case "DacqPreTrig"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.DacqPreTrig = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.DacqPreTrig)
							End If
						
						Case "ProcType"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProcType = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ProcType)
							End If
						
						Case "PolyDegree"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.PolyDegree = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.PolyDegree)
							End If
						
						Case "NumOfArg"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.NumOfArg = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NumOfArg)
							End If
						
						Case "NumOfValidArg"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.NumOfValidArg = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NumOfValidArg)
							End If
						
						Case "Coeff1"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.Coeff1 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff1)
							End If
						
						Case "Coeff2"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.Coeff2 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff2)
							End If
						
						Case "Coeff3"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.Coeff3 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff3)
							End If
						
						Case "Coeff4"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.Coeff4 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff4)
							End If
						
						Case "Coeff5"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.Coeff5 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff5)
							End If
						
						Case "Coeff6"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.Coeff6 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.Coeff6)
							End If
						
						Case "NotYetApprovedCoeff1"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.NotYetApprovedCoeff1 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff1)
							End If
						
						Case "NotYetApprovedCoeff2"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.NotYetApprovedCoeff2 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff2)
							End If
						
						Case "NotYetApprovedCoeff3"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.NotYetApprovedCoeff3 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff3)
							End If
						
						Case "NotYetApprovedCoeff4"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.NotYetApprovedCoeff4 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff4)
							End If
						
						Case "NotYetApprovedCoeff5"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.NotYetApprovedCoeff5 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff5)
							End If
						
						Case "NotYetApprovedCoeff6"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.NotYetApprovedCoeff6 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff6)
							End If
						
						Case "WaitingForApproval"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.WaitingForApproval = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.WaitingForApproval)
							End If
						
						Case "HighHighLimitOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.HighHighLimitOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighHighLimitOn)
							End If
						
						Case "HighLimitOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.HighLimitOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.HighLimitOn)
							End If
						
						Case "LowLimitOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.LowLimitOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLimitOn)
							End If
						
						Case "LowLowLimitOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.LowLowLimitOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.LowLowLimitOn)
							End If
						
						Case "ArgSrc"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ArgSrc = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ArgSrc)
							End If
						
						Case "ValidArgOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ValidArgOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ValidArgOn)
							End If
						
						Case "ReadV1"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ReadV1 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV1)
							End If
						
						Case "ReadV2"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ReadV2 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV2)
							End If
						
						Case "ReadV3"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ReadV3 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV3)
							End If
						
						Case "ReadV4"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ReadV4 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV4)
							End If
						
						Case "ReadV5"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ReadV5 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV5)
							End If
						
						Case "ReadV6"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.ReadV6 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.ReadV6)
							End If
						
						Case "RealV1"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.RealV1 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV1)
							End If
						
						Case "RealV2"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.RealV2 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV2)
							End If
						
						Case "RealV3"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.RealV3 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV3)
							End If
						
						Case "RealV4"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.RealV4 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV4)
							End If
						
						Case "RealV5"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.RealV5 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV5)
							End If
						
						Case "RealV6"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.RealV6 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.RealV6)
							End If
						
						Case "SmoothingPeriod"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SmoothingPeriod = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.SmoothingPeriod)
							End If
						
						Case "MarginAccuracy"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.MarginAccuracy = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.MarginAccuracy)
							End If
						
						Case "OEMTolerance"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.OEMTolerance = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.OEMTolerance)
							End If
						
						Case "NbPAllowed"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.NbPAllowed = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnTemplateRealMetadata.PropertyNames.NbPAllowed)
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
		
			Public Sub New(ByVal entity As esDacnTemplateReal)
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
		  	
			Public Property Name As System.String 
				Get
					Dim data_ As System.String = entity.Name
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Name = Nothing
					Else
						entity.Name = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property RdataID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.RdataID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RdataID = Nothing
					Else
						entity.RdataID = Convert.ToInt32(Value)
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
		  	
			Public Property FieldEquipmentID As System.String 
				Get
					Dim data_ As System.String = entity.FieldEquipmentID
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FieldEquipmentID = Nothing
					Else
						entity.FieldEquipmentID = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DeviceID As System.String 
				Get
					Dim data_ As System.String = entity.DeviceID
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DeviceID = Nothing
					Else
						entity.DeviceID = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property InChannelType As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.InChannelType
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.InChannelType = Nothing
					Else
						entity.InChannelType = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Channel As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Channel
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Channel = Nothing
					Else
						entity.Channel = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property BaseAddress As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.BaseAddress
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.BaseAddress = Nothing
					Else
						entity.BaseAddress = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property PhysicalChannel As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.PhysicalChannel
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PhysicalChannel = Nothing
					Else
						entity.PhysicalChannel = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MinDCValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.MinDCValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MinDCValue = Nothing
					Else
						entity.MinDCValue = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property MaxDCValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.MaxDCValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MaxDCValue = Nothing
					Else
						entity.MaxDCValue = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property TerminalConfiguration As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.TerminalConfiguration
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TerminalConfiguration = Nothing
					Else
						entity.TerminalConfiguration = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property AlarmOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.AlarmOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AlarmOn = Nothing
					Else
						entity.AlarmOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property StatisticsOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.StatisticsOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.StatisticsOn = Nothing
					Else
						entity.StatisticsOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property TextConversionOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.TextConversionOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TextConversionOn = Nothing
					Else
						entity.TextConversionOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property DataAcquisitionOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.DataAcquisitionOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DataAcquisitionOn = Nothing
					Else
						entity.DataAcquisitionOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property DataProcessingOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.DataProcessingOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DataProcessingOn = Nothing
					Else
						entity.DataProcessingOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property PeriodInMsec As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.PeriodInMsec
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PeriodInMsec = Nothing
					Else
						entity.PeriodInMsec = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property TreatOnOption As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.TreatOnOption
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TreatOnOption = Nothing
					Else
						entity.TreatOnOption = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property DefaultValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.DefaultValue
					
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
						entity.DefaultValue = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property UpperLimit As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.UpperLimit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.UpperLimit = Nothing
					Else
						entity.UpperLimit = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowerLimit As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.LowerLimit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowerLimit = Nothing
					Else
						entity.LowerLimit = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property DeltaVPlus As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.DeltaVPlus
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DeltaVPlus = Nothing
					Else
						entity.DeltaVPlus = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property DeltaVMinus As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.DeltaVMinus
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DeltaVMinus = Nothing
					Else
						entity.DeltaVMinus = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property RocPlus As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.RocPlus
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RocPlus = Nothing
					Else
						entity.RocPlus = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property RocMinus As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.RocMinus
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RocMinus = Nothing
					Else
						entity.RocMinus = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property EventOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.EventOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EventOn = Nothing
					Else
						entity.EventOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighHighLimit As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.HighHighLimit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighHighLimit = Nothing
					Else
						entity.HighHighLimit = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighLimit As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.HighLimit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighLimit = Nothing
					Else
						entity.HighLimit = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowLimit As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.LowLimit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLimit = Nothing
					Else
						entity.LowLimit = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowLowLimit As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.LowLowLimit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLowLimit = Nothing
					Else
						entity.LowLowLimit = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighHighAlarm As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.HighHighAlarm
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighHighAlarm = Nothing
					Else
						entity.HighHighAlarm = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighAlarm As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.HighAlarm
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighAlarm = Nothing
					Else
						entity.HighAlarm = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowAlarm As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.LowAlarm
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowAlarm = Nothing
					Else
						entity.LowAlarm = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowLowAlarm As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.LowLowAlarm
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLowAlarm = Nothing
					Else
						entity.LowLowAlarm = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighHighBand As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.HighHighBand
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighHighBand = Nothing
					Else
						entity.HighHighBand = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighBand As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.HighBand
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighBand = Nothing
					Else
						entity.HighBand = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowBand As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.LowBand
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowBand = Nothing
					Else
						entity.LowBand = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowLowBand As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.LowLowBand
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLowBand = Nothing
					Else
						entity.LowLowBand = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighHighAlarmOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.HighHighAlarmOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighHighAlarmOn = Nothing
					Else
						entity.HighHighAlarmOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighAlarmOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.HighAlarmOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighAlarmOn = Nothing
					Else
						entity.HighAlarmOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowAlarmOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.LowAlarmOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowAlarmOn = Nothing
					Else
						entity.LowAlarmOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowLowAlarmOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.LowLowAlarmOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLowAlarmOn = Nothing
					Else
						entity.LowLowAlarmOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighHighAlarmSource As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.HighHighAlarmSource
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighHighAlarmSource = Nothing
					Else
						entity.HighHighAlarmSource = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighAlarmSource As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.HighAlarmSource
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighAlarmSource = Nothing
					Else
						entity.HighAlarmSource = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowAlarmSource As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.LowAlarmSource
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowAlarmSource = Nothing
					Else
						entity.LowAlarmSource = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowLowAlarmSource As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.LowLowAlarmSource
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLowAlarmSource = Nothing
					Else
						entity.LowLowAlarmSource = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property TextPvFormat As System.String 
				Get
					Dim data_ As System.String = entity.TextPvFormat
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TextPvFormat = Nothing
					Else
						entity.TextPvFormat = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property HMIAliasName As System.String 
				Get
					Dim data_ As System.String = entity.HMIAliasName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HMIAliasName = Nothing
					Else
						entity.HMIAliasName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property FILEAliasName As System.String 
				Get
					Dim data_ As System.String = entity.FILEAliasName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FILEAliasName = Nothing
					Else
						entity.FILEAliasName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property EngUnit As System.String 
				Get
					Dim data_ As System.String = entity.EngUnit
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EngUnit = Nothing
					Else
						entity.EngUnit = Convert.ToString(Value)
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
		  	
			Public Property MinMaxOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.MinMaxOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MinMaxOn = Nothing
					Else
						entity.MinMaxOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property DeltaVOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.DeltaVOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DeltaVOn = Nothing
					Else
						entity.DeltaVOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property DeltaTOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.DeltaTOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DeltaTOn = Nothing
					Else
						entity.DeltaTOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property RocOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.RocOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RocOn = Nothing
					Else
						entity.RocOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property SumVOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.SumVOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SumVOn = Nothing
					Else
						entity.SumVOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property SumTOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.SumTOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SumTOn = Nothing
					Else
						entity.SumTOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property AvgOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.AvgOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AvgOn = Nothing
					Else
						entity.AvgOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property AvgTypeID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.AvgTypeID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AvgTypeID = Nothing
					Else
						entity.AvgTypeID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property AvgPeriod As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.AvgPeriod
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AvgPeriod = Nothing
					Else
						entity.AvgPeriod = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property AvgNbSamples As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.AvgNbSamples
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AvgNbSamples = Nothing
					Else
						entity.AvgNbSamples = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property QueueOptionID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.QueueOptionID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.QueueOptionID = Nothing
					Else
						entity.QueueOptionID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property ArchOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.ArchOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ArchOn = Nothing
					Else
						entity.ArchOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property ArchRate As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.ArchRate
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ArchRate = Nothing
					Else
						entity.ArchRate = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property TrigCondID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.TrigCondID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TrigCondID = Nothing
					Else
						entity.TrigCondID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property TrigLevel As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.TrigLevel
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TrigLevel = Nothing
					Else
						entity.TrigLevel = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property TrigDeadBand As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.TrigDeadBand
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TrigDeadBand = Nothing
					Else
						entity.TrigDeadBand = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property TimeOut As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.TimeOut
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TimeOut = Nothing
					Else
						entity.TimeOut = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property QueueOption As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.QueueOption
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.QueueOption = Nothing
					Else
						entity.QueueOption = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property QueueSize As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.QueueSize
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.QueueSize = Nothing
					Else
						entity.QueueSize = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property PreTrigRecord As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.PreTrigRecord
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PreTrigRecord = Nothing
					Else
						entity.PreTrigRecord = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MaxRecord As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.MaxRecord
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MaxRecord = Nothing
					Else
						entity.MaxRecord = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property DacqPreTrig As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.DacqPreTrig
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DacqPreTrig = Nothing
					Else
						entity.DacqPreTrig = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property ProcType As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.ProcType
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ProcType = Nothing
					Else
						entity.ProcType = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property PolyDegree As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.PolyDegree
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PolyDegree = Nothing
					Else
						entity.PolyDegree = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property NumOfArg As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.NumOfArg
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NumOfArg = Nothing
					Else
						entity.NumOfArg = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property NumOfValidArg As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.NumOfValidArg
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NumOfValidArg = Nothing
					Else
						entity.NumOfValidArg = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Coeff1 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.Coeff1
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Coeff1 = Nothing
					Else
						entity.Coeff1 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property Coeff2 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.Coeff2
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Coeff2 = Nothing
					Else
						entity.Coeff2 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property Coeff3 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.Coeff3
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Coeff3 = Nothing
					Else
						entity.Coeff3 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property Coeff4 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.Coeff4
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Coeff4 = Nothing
					Else
						entity.Coeff4 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property Coeff5 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.Coeff5
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Coeff5 = Nothing
					Else
						entity.Coeff5 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property Coeff6 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.Coeff6
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Coeff6 = Nothing
					Else
						entity.Coeff6 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property NotYetApprovedCoeff1 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.NotYetApprovedCoeff1
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NotYetApprovedCoeff1 = Nothing
					Else
						entity.NotYetApprovedCoeff1 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property NotYetApprovedCoeff2 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.NotYetApprovedCoeff2
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NotYetApprovedCoeff2 = Nothing
					Else
						entity.NotYetApprovedCoeff2 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property NotYetApprovedCoeff3 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.NotYetApprovedCoeff3
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NotYetApprovedCoeff3 = Nothing
					Else
						entity.NotYetApprovedCoeff3 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property NotYetApprovedCoeff4 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.NotYetApprovedCoeff4
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NotYetApprovedCoeff4 = Nothing
					Else
						entity.NotYetApprovedCoeff4 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property NotYetApprovedCoeff5 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.NotYetApprovedCoeff5
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NotYetApprovedCoeff5 = Nothing
					Else
						entity.NotYetApprovedCoeff5 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property NotYetApprovedCoeff6 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.NotYetApprovedCoeff6
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NotYetApprovedCoeff6 = Nothing
					Else
						entity.NotYetApprovedCoeff6 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property WaitingForApproval As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.WaitingForApproval
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.WaitingForApproval = Nothing
					Else
						entity.WaitingForApproval = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg1 As System.String 
				Get
					Dim data_ As System.String = entity.Arg1
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg1 = Nothing
					Else
						entity.Arg1 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg2 As System.String 
				Get
					Dim data_ As System.String = entity.Arg2
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg2 = Nothing
					Else
						entity.Arg2 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg3 As System.String 
				Get
					Dim data_ As System.String = entity.Arg3
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg3 = Nothing
					Else
						entity.Arg3 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg4 As System.String 
				Get
					Dim data_ As System.String = entity.Arg4
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg4 = Nothing
					Else
						entity.Arg4 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg5 As System.String 
				Get
					Dim data_ As System.String = entity.Arg5
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg5 = Nothing
					Else
						entity.Arg5 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg6 As System.String 
				Get
					Dim data_ As System.String = entity.Arg6
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg6 = Nothing
					Else
						entity.Arg6 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg7 As System.String 
				Get
					Dim data_ As System.String = entity.Arg7
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg7 = Nothing
					Else
						entity.Arg7 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg8 As System.String 
				Get
					Dim data_ As System.String = entity.Arg8
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg8 = Nothing
					Else
						entity.Arg8 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg9 As System.String 
				Get
					Dim data_ As System.String = entity.Arg9
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg9 = Nothing
					Else
						entity.Arg9 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg10 As System.String 
				Get
					Dim data_ As System.String = entity.Arg10
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg10 = Nothing
					Else
						entity.Arg10 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg11 As System.String 
				Get
					Dim data_ As System.String = entity.Arg11
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg11 = Nothing
					Else
						entity.Arg11 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg12 As System.String 
				Get
					Dim data_ As System.String = entity.Arg12
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg12 = Nothing
					Else
						entity.Arg12 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg13 As System.String 
				Get
					Dim data_ As System.String = entity.Arg13
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg13 = Nothing
					Else
						entity.Arg13 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg14 As System.String 
				Get
					Dim data_ As System.String = entity.Arg14
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg14 = Nothing
					Else
						entity.Arg14 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg15 As System.String 
				Get
					Dim data_ As System.String = entity.Arg15
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg15 = Nothing
					Else
						entity.Arg15 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg16 As System.String 
				Get
					Dim data_ As System.String = entity.Arg16
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg16 = Nothing
					Else
						entity.Arg16 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg17 As System.String 
				Get
					Dim data_ As System.String = entity.Arg17
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg17 = Nothing
					Else
						entity.Arg17 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg18 As System.String 
				Get
					Dim data_ As System.String = entity.Arg18
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg18 = Nothing
					Else
						entity.Arg18 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg19 As System.String 
				Get
					Dim data_ As System.String = entity.Arg19
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg19 = Nothing
					Else
						entity.Arg19 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Arg20 As System.String 
				Get
					Dim data_ As System.String = entity.Arg20
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Arg20 = Nothing
					Else
						entity.Arg20 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighHighLimitOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.HighHighLimitOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighHighLimitOn = Nothing
					Else
						entity.HighHighLimitOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighLimitOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.HighLimitOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighLimitOn = Nothing
					Else
						entity.HighLimitOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowLimitOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.LowLimitOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLimitOn = Nothing
					Else
						entity.LowLimitOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowLowLimitOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.LowLowLimitOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLowLimitOn = Nothing
					Else
						entity.LowLowLimitOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighHighAlarmName As System.String 
				Get
					Dim data_ As System.String = entity.HighHighAlarmName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighHighAlarmName = Nothing
					Else
						entity.HighHighAlarmName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property HighAlarmName As System.String 
				Get
					Dim data_ As System.String = entity.HighAlarmName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HighAlarmName = Nothing
					Else
						entity.HighAlarmName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowAlarmName As System.String 
				Get
					Dim data_ As System.String = entity.LowAlarmName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowAlarmName = Nothing
					Else
						entity.LowAlarmName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property LowLowAlarmName As System.String 
				Get
					Dim data_ As System.String = entity.LowLowAlarmName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LowLowAlarmName = Nothing
					Else
						entity.LowLowAlarmName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ArgSrc As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.ArgSrc
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ArgSrc = Nothing
					Else
						entity.ArgSrc = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property ValidArgOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.ValidArgOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ValidArgOn = Nothing
					Else
						entity.ValidArgOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property ReadV1 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ReadV1
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ReadV1 = Nothing
					Else
						entity.ReadV1 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property ReadV2 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ReadV2
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ReadV2 = Nothing
					Else
						entity.ReadV2 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property ReadV3 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ReadV3
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ReadV3 = Nothing
					Else
						entity.ReadV3 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property ReadV4 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ReadV4
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ReadV4 = Nothing
					Else
						entity.ReadV4 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property ReadV5 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ReadV5
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ReadV5 = Nothing
					Else
						entity.ReadV5 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property ReadV6 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.ReadV6
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ReadV6 = Nothing
					Else
						entity.ReadV6 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property RealV1 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.RealV1
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RealV1 = Nothing
					Else
						entity.RealV1 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property RealV2 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.RealV2
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RealV2 = Nothing
					Else
						entity.RealV2 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property RealV3 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.RealV3
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RealV3 = Nothing
					Else
						entity.RealV3 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property RealV4 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.RealV4
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RealV4 = Nothing
					Else
						entity.RealV4 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property RealV5 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.RealV5
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RealV5 = Nothing
					Else
						entity.RealV5 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property RealV6 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.RealV6
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RealV6 = Nothing
					Else
						entity.RealV6 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property SmoothingPeriod As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SmoothingPeriod
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SmoothingPeriod = Nothing
					Else
						entity.SmoothingPeriod = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MarginAccuracy As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.MarginAccuracy
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MarginAccuracy = Nothing
					Else
						entity.MarginAccuracy = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property OEMTolerance As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.OEMTolerance
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OEMTolerance = Nothing
					Else
						entity.OEMTolerance = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property NbPAllowed As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.NbPAllowed
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NbPAllowed = Nothing
					Else
						entity.NbPAllowed = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationMeasured As System.String 
				Get
					Dim data_ As System.String = entity.CalibrationMeasured
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationMeasured = Nothing
					Else
						entity.CalibrationMeasured = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ChannelType As System.String 
				Get
					Dim data_ As System.String = entity.ChannelType
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ChannelType = Nothing
					Else
						entity.ChannelType = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ChannelRange As System.String 
				Get
					Dim data_ As System.String = entity.ChannelRange
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ChannelRange = Nothing
					Else
						entity.ChannelRange = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property TransducerRange As System.String 
				Get
					Dim data_ As System.String = entity.TransducerRange
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TransducerRange = Nothing
					Else
						entity.TransducerRange = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationRange As System.String 
				Get
					Dim data_ As System.String = entity.CalibrationRange
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationRange = Nothing
					Else
						entity.CalibrationRange = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Make1 As System.String 
				Get
					Dim data_ As System.String = entity.Make1
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Make1 = Nothing
					Else
						entity.Make1 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Model1 As System.String 
				Get
					Dim data_ As System.String = entity.Model1
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Model1 = Nothing
					Else
						entity.Model1 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property SerialNumber1 As System.String 
				Get
					Dim data_ As System.String = entity.SerialNumber1
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SerialNumber1 = Nothing
					Else
						entity.SerialNumber1 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationDueDate1 As System.String 
				Get
					Dim data_ As System.String = entity.CalibrationDueDate1
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationDueDate1 = Nothing
					Else
						entity.CalibrationDueDate1 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Make2 As System.String 
				Get
					Dim data_ As System.String = entity.Make2
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Make2 = Nothing
					Else
						entity.Make2 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Model2 As System.String 
				Get
					Dim data_ As System.String = entity.Model2
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Model2 = Nothing
					Else
						entity.Model2 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property SerialNumber2 As System.String 
				Get
					Dim data_ As System.String = entity.SerialNumber2
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SerialNumber2 = Nothing
					Else
						entity.SerialNumber2 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationDueDate2 As System.String 
				Get
					Dim data_ As System.String = entity.CalibrationDueDate2
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationDueDate2 = Nothing
					Else
						entity.CalibrationDueDate2 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Make3 As System.String 
				Get
					Dim data_ As System.String = entity.Make3
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Make3 = Nothing
					Else
						entity.Make3 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Model3 As System.String 
				Get
					Dim data_ As System.String = entity.Model3
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Model3 = Nothing
					Else
						entity.Model3 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property SerialNumber3 As System.String 
				Get
					Dim data_ As System.String = entity.SerialNumber3
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SerialNumber3 = Nothing
					Else
						entity.SerialNumber3 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationDueDate3 As System.String 
				Get
					Dim data_ As System.String = entity.CalibrationDueDate3
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationDueDate3 = Nothing
					Else
						entity.CalibrationDueDate3 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Make4 As System.String 
				Get
					Dim data_ As System.String = entity.Make4
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Make4 = Nothing
					Else
						entity.Make4 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Model4 As System.String 
				Get
					Dim data_ As System.String = entity.Model4
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Model4 = Nothing
					Else
						entity.Model4 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property SerialNumber4 As System.String 
				Get
					Dim data_ As System.String = entity.SerialNumber4
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SerialNumber4 = Nothing
					Else
						entity.SerialNumber4 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationDueDate4 As System.String 
				Get
					Dim data_ As System.String = entity.CalibrationDueDate4
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationDueDate4 = Nothing
					Else
						entity.CalibrationDueDate4 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Comments As System.String 
				Get
					Dim data_ As System.String = entity.Comments
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Comments = Nothing
					Else
						entity.Comments = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibratedBy As System.String 
				Get
					Dim data_ As System.String = entity.CalibratedBy
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibratedBy = Nothing
					Else
						entity.CalibratedBy = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Assistant As System.String 
				Get
					Dim data_ As System.String = entity.Assistant
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Assistant = Nothing
					Else
						entity.Assistant = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Approval As System.String 
				Get
					Dim data_ As System.String = entity.Approval
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Approval = Nothing
					Else
						entity.Approval = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property LastCalibDate As System.String 
				Get
					Dim data_ As System.String = entity.LastCalibDate
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LastCalibDate = Nothing
					Else
						entity.LastCalibDate = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property NewCalibDate As System.String 
				Get
					Dim data_ As System.String = entity.NewCalibDate
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NewCalibDate = Nothing
					Else
						entity.NewCalibDate = Convert.ToString(Value)
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
		  	
			Public Property TCTransducerSN As System.String 
				Get
					Dim data_ As System.String = entity.TCTransducerSN
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TCTransducerSN = Nothing
					Else
						entity.TCTransducerSN = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationTemp As System.String 
				Get
					Dim data_ As System.String = entity.CalibrationTemp
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationTemp = Nothing
					Else
						entity.CalibrationTemp = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationHumidity As System.String 
				Get
					Dim data_ As System.String = entity.CalibrationHumidity
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationHumidity = Nothing
					Else
						entity.CalibrationHumidity = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esDacnTemplateReal
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DacnTemplateRealMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As DacnTemplateRealQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DacnTemplateRealQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As DacnTemplateRealQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As DacnTemplateRealQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As DacnTemplateRealQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDacnTemplateRealCollection
		Inherits esEntityCollection(Of DacnTemplateReal)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DacnTemplateRealMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "DacnTemplateRealCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As DacnTemplateRealQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DacnTemplateRealQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As DacnTemplateRealQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New DacnTemplateRealQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As DacnTemplateRealQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, DacnTemplateRealQuery))
		End Sub
		
		#End Region
						
		Private m_query As DacnTemplateRealQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDacnTemplateRealQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return DacnTemplateRealMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "Name" 
					Return Me.Name
				Case "RdataID" 
					Return Me.RdataID
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "FieldEquipmentID" 
					Return Me.FieldEquipmentID
				Case "DeviceID" 
					Return Me.DeviceID
				Case "InChannelType" 
					Return Me.InChannelType
				Case "Channel" 
					Return Me.Channel
				Case "BaseAddress" 
					Return Me.BaseAddress
				Case "PhysicalChannel" 
					Return Me.PhysicalChannel
				Case "MinDCValue" 
					Return Me.MinDCValue
				Case "MaxDCValue" 
					Return Me.MaxDCValue
				Case "TerminalConfiguration" 
					Return Me.TerminalConfiguration
				Case "AlarmOn" 
					Return Me.AlarmOn
				Case "StatisticsOn" 
					Return Me.StatisticsOn
				Case "TextConversionOn" 
					Return Me.TextConversionOn
				Case "DataAcquisitionOn" 
					Return Me.DataAcquisitionOn
				Case "DataProcessingOn" 
					Return Me.DataProcessingOn
				Case "PeriodInMsec" 
					Return Me.PeriodInMsec
				Case "TreatOnOption" 
					Return Me.TreatOnOption
				Case "DefaultValue" 
					Return Me.DefaultValue
				Case "UpperLimit" 
					Return Me.UpperLimit
				Case "LowerLimit" 
					Return Me.LowerLimit
				Case "DeltaVPlus" 
					Return Me.DeltaVPlus
				Case "DeltaVMinus" 
					Return Me.DeltaVMinus
				Case "RocPlus" 
					Return Me.RocPlus
				Case "RocMinus" 
					Return Me.RocMinus
				Case "EventOn" 
					Return Me.EventOn
				Case "HighHighLimit" 
					Return Me.HighHighLimit
				Case "HighLimit" 
					Return Me.HighLimit
				Case "LowLimit" 
					Return Me.LowLimit
				Case "LowLowLimit" 
					Return Me.LowLowLimit
				Case "HighHighAlarm" 
					Return Me.HighHighAlarm
				Case "HighAlarm" 
					Return Me.HighAlarm
				Case "LowAlarm" 
					Return Me.LowAlarm
				Case "LowLowAlarm" 
					Return Me.LowLowAlarm
				Case "HighHighBand" 
					Return Me.HighHighBand
				Case "HighBand" 
					Return Me.HighBand
				Case "LowBand" 
					Return Me.LowBand
				Case "LowLowBand" 
					Return Me.LowLowBand
				Case "HighHighAlarmOn" 
					Return Me.HighHighAlarmOn
				Case "HighAlarmOn" 
					Return Me.HighAlarmOn
				Case "LowAlarmOn" 
					Return Me.LowAlarmOn
				Case "LowLowAlarmOn" 
					Return Me.LowLowAlarmOn
				Case "HighHighAlarmSource" 
					Return Me.HighHighAlarmSource
				Case "HighAlarmSource" 
					Return Me.HighAlarmSource
				Case "LowAlarmSource" 
					Return Me.LowAlarmSource
				Case "LowLowAlarmSource" 
					Return Me.LowLowAlarmSource
				Case "TextPvFormat" 
					Return Me.TextPvFormat
				Case "HMIAliasName" 
					Return Me.HMIAliasName
				Case "FILEAliasName" 
					Return Me.FILEAliasName
				Case "EngUnit" 
					Return Me.EngUnit
				Case "EngUnitID" 
					Return Me.EngUnitID
				Case "MinMaxOn" 
					Return Me.MinMaxOn
				Case "DeltaVOn" 
					Return Me.DeltaVOn
				Case "DeltaTOn" 
					Return Me.DeltaTOn
				Case "RocOn" 
					Return Me.RocOn
				Case "SumVOn" 
					Return Me.SumVOn
				Case "SumTOn" 
					Return Me.SumTOn
				Case "AvgOn" 
					Return Me.AvgOn
				Case "AvgTypeID" 
					Return Me.AvgTypeID
				Case "AvgPeriod" 
					Return Me.AvgPeriod
				Case "AvgNbSamples" 
					Return Me.AvgNbSamples
				Case "QueueOptionID" 
					Return Me.QueueOptionID
				Case "ArchOn" 
					Return Me.ArchOn
				Case "ArchRate" 
					Return Me.ArchRate
				Case "TrigCondID" 
					Return Me.TrigCondID
				Case "TrigLevel" 
					Return Me.TrigLevel
				Case "TrigDeadBand" 
					Return Me.TrigDeadBand
				Case "TimeOut" 
					Return Me.TimeOut
				Case "QueueOption" 
					Return Me.QueueOption
				Case "QueueSize" 
					Return Me.QueueSize
				Case "PreTrigRecord" 
					Return Me.PreTrigRecord
				Case "MaxRecord" 
					Return Me.MaxRecord
				Case "DacqPreTrig" 
					Return Me.DacqPreTrig
				Case "ProcType" 
					Return Me.ProcType
				Case "PolyDegree" 
					Return Me.PolyDegree
				Case "NumOfArg" 
					Return Me.NumOfArg
				Case "NumOfValidArg" 
					Return Me.NumOfValidArg
				Case "Coeff1" 
					Return Me.Coeff1
				Case "Coeff2" 
					Return Me.Coeff2
				Case "Coeff3" 
					Return Me.Coeff3
				Case "Coeff4" 
					Return Me.Coeff4
				Case "Coeff5" 
					Return Me.Coeff5
				Case "Coeff6" 
					Return Me.Coeff6
				Case "NotYetApprovedCoeff1" 
					Return Me.NotYetApprovedCoeff1
				Case "NotYetApprovedCoeff2" 
					Return Me.NotYetApprovedCoeff2
				Case "NotYetApprovedCoeff3" 
					Return Me.NotYetApprovedCoeff3
				Case "NotYetApprovedCoeff4" 
					Return Me.NotYetApprovedCoeff4
				Case "NotYetApprovedCoeff5" 
					Return Me.NotYetApprovedCoeff5
				Case "NotYetApprovedCoeff6" 
					Return Me.NotYetApprovedCoeff6
				Case "WaitingForApproval" 
					Return Me.WaitingForApproval
				Case "Arg1" 
					Return Me.Arg1
				Case "Arg2" 
					Return Me.Arg2
				Case "Arg3" 
					Return Me.Arg3
				Case "Arg4" 
					Return Me.Arg4
				Case "Arg5" 
					Return Me.Arg5
				Case "Arg6" 
					Return Me.Arg6
				Case "Arg7" 
					Return Me.Arg7
				Case "Arg8" 
					Return Me.Arg8
				Case "Arg9" 
					Return Me.Arg9
				Case "Arg10" 
					Return Me.Arg10
				Case "Arg11" 
					Return Me.Arg11
				Case "Arg12" 
					Return Me.Arg12
				Case "Arg13" 
					Return Me.Arg13
				Case "Arg14" 
					Return Me.Arg14
				Case "Arg15" 
					Return Me.Arg15
				Case "Arg16" 
					Return Me.Arg16
				Case "Arg17" 
					Return Me.Arg17
				Case "Arg18" 
					Return Me.Arg18
				Case "Arg19" 
					Return Me.Arg19
				Case "Arg20" 
					Return Me.Arg20
				Case "HighHighLimitOn" 
					Return Me.HighHighLimitOn
				Case "HighLimitOn" 
					Return Me.HighLimitOn
				Case "LowLimitOn" 
					Return Me.LowLimitOn
				Case "LowLowLimitOn" 
					Return Me.LowLowLimitOn
				Case "HighHighAlarmName" 
					Return Me.HighHighAlarmName
				Case "HighAlarmName" 
					Return Me.HighAlarmName
				Case "LowAlarmName" 
					Return Me.LowAlarmName
				Case "LowLowAlarmName" 
					Return Me.LowLowAlarmName
				Case "ArgSrc" 
					Return Me.ArgSrc
				Case "ValidArgOn" 
					Return Me.ValidArgOn
				Case "ReadV1" 
					Return Me.ReadV1
				Case "ReadV2" 
					Return Me.ReadV2
				Case "ReadV3" 
					Return Me.ReadV3
				Case "ReadV4" 
					Return Me.ReadV4
				Case "ReadV5" 
					Return Me.ReadV5
				Case "ReadV6" 
					Return Me.ReadV6
				Case "RealV1" 
					Return Me.RealV1
				Case "RealV2" 
					Return Me.RealV2
				Case "RealV3" 
					Return Me.RealV3
				Case "RealV4" 
					Return Me.RealV4
				Case "RealV5" 
					Return Me.RealV5
				Case "RealV6" 
					Return Me.RealV6
				Case "SmoothingPeriod" 
					Return Me.SmoothingPeriod
				Case "MarginAccuracy" 
					Return Me.MarginAccuracy
				Case "OEMTolerance" 
					Return Me.OEMTolerance
				Case "NbPAllowed" 
					Return Me.NbPAllowed
				Case "CalibrationMeasured" 
					Return Me.CalibrationMeasured
				Case "ChannelType" 
					Return Me.ChannelType
				Case "ChannelRange" 
					Return Me.ChannelRange
				Case "TransducerRange" 
					Return Me.TransducerRange
				Case "CalibrationRange" 
					Return Me.CalibrationRange
				Case "Make1" 
					Return Me.Make1
				Case "Model1" 
					Return Me.Model1
				Case "SerialNumber1" 
					Return Me.SerialNumber1
				Case "CalibrationDueDate1" 
					Return Me.CalibrationDueDate1
				Case "Make2" 
					Return Me.Make2
				Case "Model2" 
					Return Me.Model2
				Case "SerialNumber2" 
					Return Me.SerialNumber2
				Case "CalibrationDueDate2" 
					Return Me.CalibrationDueDate2
				Case "Make3" 
					Return Me.Make3
				Case "Model3" 
					Return Me.Model3
				Case "SerialNumber3" 
					Return Me.SerialNumber3
				Case "CalibrationDueDate3" 
					Return Me.CalibrationDueDate3
				Case "Make4" 
					Return Me.Make4
				Case "Model4" 
					Return Me.Model4
				Case "SerialNumber4" 
					Return Me.SerialNumber4
				Case "CalibrationDueDate4" 
					Return Me.CalibrationDueDate4
				Case "Comments" 
					Return Me.Comments
				Case "CalibratedBy" 
					Return Me.CalibratedBy
				Case "Assistant" 
					Return Me.Assistant
				Case "Approval" 
					Return Me.Approval
				Case "LastCalibDate" 
					Return Me.LastCalibDate
				Case "NewCalibDate" 
					Return Me.NewCalibDate
				Case "OPCGroup" 
					Return Me.OPCGroup
				Case "OPCItemID" 
					Return Me.OPCItemID
				Case "TCTransducerSN" 
					Return Me.TCTransducerSN
				Case "CalibrationTemp" 
					Return Me.CalibrationTemp
				Case "CalibrationHumidity" 
					Return Me.CalibrationHumidity
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Name As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property RdataID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.RdataID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property FieldEquipmentID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.FieldEquipmentID, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DeviceID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.DeviceID, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property InChannelType As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.InChannelType, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Channel As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Channel, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property BaseAddress As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.BaseAddress, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property PhysicalChannel As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.PhysicalChannel, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MinDCValue As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.MinDCValue, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property MaxDCValue As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.MaxDCValue, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property TerminalConfiguration As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.TerminalConfiguration, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property AlarmOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.AlarmOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property StatisticsOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.StatisticsOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property TextConversionOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.TextConversionOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property DataAcquisitionOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.DataAcquisitionOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property DataProcessingOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.DataProcessingOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property PeriodInMsec As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.PeriodInMsec, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property TreatOnOption As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.TreatOnOption, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property DefaultValue As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.DefaultValue, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property UpperLimit As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.UpperLimit, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property LowerLimit As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowerLimit, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property DeltaVPlus As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.DeltaVPlus, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property DeltaVMinus As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.DeltaVMinus, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property RocPlus As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.RocPlus, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property RocMinus As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.RocMinus, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property EventOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.EventOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property HighHighLimit As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighHighLimit, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property HighLimit As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighLimit, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property LowLimit As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowLimit, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property LowLowLimit As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowLowLimit, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property HighHighAlarm As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighHighAlarm, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property HighAlarm As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighAlarm, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property LowAlarm As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowAlarm, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property LowLowAlarm As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowLowAlarm, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property HighHighBand As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighHighBand, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property HighBand As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighBand, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property LowBand As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowBand, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property LowLowBand As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowLowBand, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property HighHighAlarmOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighHighAlarmOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property HighAlarmOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighAlarmOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property LowAlarmOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowAlarmOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property LowLowAlarmOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowLowAlarmOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property HighHighAlarmSource As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighHighAlarmSource, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property HighAlarmSource As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighAlarmSource, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property LowAlarmSource As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowAlarmSource, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property LowLowAlarmSource As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowLowAlarmSource, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property TextPvFormat As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.TextPvFormat, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property HMIAliasName As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HMIAliasName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FILEAliasName As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.FILEAliasName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property EngUnit As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.EngUnit, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property EngUnitID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.EngUnitID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MinMaxOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.MinMaxOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property DeltaVOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.DeltaVOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property DeltaTOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.DeltaTOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property RocOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.RocOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property SumVOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.SumVOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property SumTOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.SumTOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property AvgOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.AvgOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property AvgTypeID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.AvgTypeID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property AvgPeriod As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.AvgPeriod, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property AvgNbSamples As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.AvgNbSamples, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property QueueOptionID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.QueueOptionID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ArchOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ArchOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property ArchRate As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ArchRate, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property TrigCondID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.TrigCondID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property TrigLevel As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.TrigLevel, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property TrigDeadBand As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.TrigDeadBand, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property TimeOut As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.TimeOut, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property QueueOption As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.QueueOption, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property QueueSize As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.QueueSize, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property PreTrigRecord As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.PreTrigRecord, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MaxRecord As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.MaxRecord, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property DacqPreTrig As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.DacqPreTrig, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property ProcType As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ProcType, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property PolyDegree As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.PolyDegree, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property NumOfArg As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.NumOfArg, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property NumOfValidArg As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.NumOfValidArg, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Coeff1 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Coeff1, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Coeff2 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Coeff2, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Coeff3 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Coeff3, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Coeff4 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Coeff4, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Coeff5 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Coeff5, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Coeff6 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Coeff6, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property NotYetApprovedCoeff1 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff1, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property NotYetApprovedCoeff2 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff2, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property NotYetApprovedCoeff3 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff3, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property NotYetApprovedCoeff4 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff4, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property NotYetApprovedCoeff5 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff5, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property NotYetApprovedCoeff6 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff6, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property WaitingForApproval As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.WaitingForApproval, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Arg1 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg1, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg2 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg2, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg3 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg3, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg4 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg4, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg5 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg5, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg6 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg6, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg7 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg7, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg8 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg8, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg9 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg9, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg10 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg10, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg11 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg11, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg12 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg12, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg13 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg13, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg14 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg14, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg15 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg15, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg16 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg16, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg17 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg17, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg18 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg18, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg19 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg19, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Arg20 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Arg20, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property HighHighLimitOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighHighLimitOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property HighLimitOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighLimitOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property LowLimitOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowLimitOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property LowLowLimitOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowLowLimitOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property HighHighAlarmName As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighHighAlarmName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property HighAlarmName As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.HighAlarmName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LowAlarmName As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowAlarmName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LowLowAlarmName As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LowLowAlarmName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ArgSrc As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ArgSrc, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ValidArgOn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ValidArgOn, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property ReadV1 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ReadV1, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property ReadV2 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ReadV2, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property ReadV3 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ReadV3, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property ReadV4 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ReadV4, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property ReadV5 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ReadV5, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property ReadV6 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ReadV6, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property RealV1 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.RealV1, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property RealV2 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.RealV2, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property RealV3 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.RealV3, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property RealV4 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.RealV4, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property RealV5 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.RealV5, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property RealV6 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.RealV6, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property SmoothingPeriod As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.SmoothingPeriod, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MarginAccuracy As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.MarginAccuracy, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property OEMTolerance As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.OEMTolerance, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property NbPAllowed As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.NbPAllowed, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationMeasured As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.CalibrationMeasured, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelType As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ChannelType, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelRange As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.ChannelRange, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TransducerRange As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.TransducerRange, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationRange As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.CalibrationRange, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Make1 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Make1, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Model1 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Model1, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property SerialNumber1 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.SerialNumber1, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationDueDate1 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate1, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Make2 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Make2, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Model2 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Model2, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property SerialNumber2 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.SerialNumber2, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationDueDate2 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate2, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Make3 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Make3, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Model3 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Model3, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property SerialNumber3 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.SerialNumber3, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationDueDate3 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate3, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Make4 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Make4, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Model4 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Model4, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property SerialNumber4 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.SerialNumber4, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationDueDate4 As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate4, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Comments As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Comments, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibratedBy As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.CalibratedBy, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Assistant As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Assistant, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Approval As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.Approval, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LastCalibDate As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.LastCalibDate, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property NewCalibDate As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.NewCalibDate, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OPCGroup As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.OPCGroup, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OPCItemID As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.OPCItemID, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TCTransducerSN As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.TCTransducerSN, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationTemp As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.CalibrationTemp, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationHumidity As esQueryItem
			Get
				Return New esQueryItem(Me, DacnTemplateRealMetadata.ColumnNames.CalibrationHumidity, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class DacnTemplateReal 
		Inherits esDacnTemplateReal
		
	
		#Region "UpToEngineeringUnitByEngUnitID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - EngineeringUnitDacnTemplateReal
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

		#Region "UpToProjectInfoByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Project_InfoDacnTemplateReal
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToProjectInfoByProjectID As ProjectInfo
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToProjectInfoByProjectID Is Nothing _
						 AndAlso Not ProjectID.Equals(Nothing)  Then
						
					Me._UpToProjectInfoByProjectID = New ProjectInfo()
					Me._UpToProjectInfoByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToProjectInfoByProjectID", Me._UpToProjectInfoByProjectID)
					Me._UpToProjectInfoByProjectID.Query.Where(Me._UpToProjectInfoByProjectID.Query.ProjectID = Me.ProjectID)
					Me._UpToProjectInfoByProjectID.Query.Load()
				End If

				Return Me._UpToProjectInfoByProjectID
			End Get
			
            Set(ByVal value As ProjectInfo)
				Me.RemovePreSave("UpToProjectInfoByProjectID")
				

				If value Is Nothing Then
				
					Me.ProjectID = Nothing
				
					Me._UpToProjectInfoByProjectID = Nothing
				Else
				
					Me.ProjectID = value.ProjectID
					
					Me._UpToProjectInfoByProjectID = value
					Me.SetPreSave("UpToProjectInfoByProjectID", Me._UpToProjectInfoByProjectID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class DacnTemplateRealMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Name, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Name
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO NAME"""
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.RdataID, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.RdataID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ExportToCsv, 3, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.FieldEquipmentID, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.FieldEquipmentID
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.DeviceID, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.DeviceID
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.InChannelType, 6, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.InChannelType
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Channel, 7, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Channel
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.BaseAddress, 8, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.BaseAddress
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.PhysicalChannel, 9, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.PhysicalChannel
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.MinDCValue, 10, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.MinDCValue
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.MaxDCValue, 11, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.MaxDCValue
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "10"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.TerminalConfiguration, 12, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.TerminalConfiguration
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "10106"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.AlarmOn, 13, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.AlarmOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.StatisticsOn, 14, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.StatisticsOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "Yes"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.TextConversionOn, 15, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.TextConversionOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.DataAcquisitionOn, 16, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.DataAcquisitionOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "Yes"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.DataProcessingOn, 17, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.DataProcessingOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "Yes"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.PeriodInMsec, 18, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.PeriodInMsec
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "100"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.TreatOnOption, 19, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.TreatOnOption
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "1"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.DefaultValue, 20, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.DefaultValue
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.UpperLimit, 21, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.UpperLimit
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowerLimit, 22, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowerLimit
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.DeltaVPlus, 23, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.DeltaVPlus
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.DeltaVMinus, 24, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.DeltaVMinus
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.RocPlus, 25, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.RocPlus
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.RocMinus, 26, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.RocMinus
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.EventOn, 27, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.EventOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighHighLimit, 28, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighHighLimit
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighLimit, 29, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighLimit
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowLimit, 30, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowLimit
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowLowLimit, 31, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowLowLimit
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighHighAlarm, 32, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighHighAlarm
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighAlarm, 33, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighAlarm
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowAlarm, 34, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowAlarm
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowLowAlarm, 35, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowLowAlarm
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighHighBand, 36, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighHighBand
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighBand, 37, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighBand
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowBand, 38, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowBand
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowLowBand, 39, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowLowBand
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighHighAlarmOn, 40, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighHighAlarmOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighAlarmOn, 41, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighAlarmOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowAlarmOn, 42, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowAlarmOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowLowAlarmOn, 43, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowLowAlarmOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighHighAlarmSource, 44, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighHighAlarmSource
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighAlarmSource, 45, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighAlarmSource
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowAlarmSource, 46, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowAlarmSource
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowLowAlarmSource, 47, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowLowAlarmSource
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.TextPvFormat, 48, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.TextPvFormat
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """5.2"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HMIAliasName, 49, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HMIAliasName
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO NAME"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.FILEAliasName, 50, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.FILEAliasName
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO NAME"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.EngUnit, 51, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.EngUnit
			c.CharacterMaxLength = 30
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.EngUnitID, 52, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.EngUnitID
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.MinMaxOn, 53, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.MinMaxOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.DeltaVOn, 54, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.DeltaVOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.DeltaTOn, 55, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.DeltaTOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.RocOn, 56, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.RocOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.SumVOn, 57, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.SumVOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.SumTOn, 58, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.SumTOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.AvgOn, 59, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.AvgOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "Yes"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.AvgTypeID, 60, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.AvgTypeID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "2"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.AvgPeriod, 61, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.AvgPeriod
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "3"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.AvgNbSamples, 62, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.AvgNbSamples
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.QueueOptionID, 63, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.QueueOptionID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ArchOn, 64, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ArchOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ArchRate, 65, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ArchRate
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.TrigCondID, 66, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.TrigCondID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.TrigLevel, 67, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.TrigLevel
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.TrigDeadBand, 68, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.TrigDeadBand
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.TimeOut, 69, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.TimeOut
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.QueueOption, 70, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.QueueOption
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.QueueSize, 71, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.QueueSize
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.PreTrigRecord, 72, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.PreTrigRecord
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.MaxRecord, 73, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.MaxRecord
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.DacqPreTrig, 74, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.DacqPreTrig
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ProcType, 75, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ProcType
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "2"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.PolyDegree, 76, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.PolyDegree
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "1"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.NumOfArg, 77, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.NumOfArg
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.NumOfValidArg, 78, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.NumOfValidArg
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Coeff1, 79, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Coeff1
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Coeff2, 80, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Coeff2
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "1"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Coeff3, 81, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Coeff3
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Coeff4, 82, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Coeff4
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Coeff5, 83, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Coeff5
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Coeff6, 84, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Coeff6
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff1, 85, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff1
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff2, 86, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff2
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "1"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff3, 87, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff3
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff4, 88, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff4
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff5, 89, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff5
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.NotYetApprovedCoeff6, 90, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.NotYetApprovedCoeff6
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.WaitingForApproval, 91, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.WaitingForApproval
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg1, 92, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg1
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg2, 93, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg2
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg3, 94, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg3
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg4, 95, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg4
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg5, 96, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg5
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg6, 97, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg6
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg7, 98, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg7
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg8, 99, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg8
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg9, 100, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg9
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg10, 101, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg10
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg11, 102, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg11
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg12, 103, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg12
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg13, 104, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg13
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg14, 105, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg14
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg15, 106, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg15
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg16, 107, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg16
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg17, 108, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg17
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg18, 109, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg18
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg19, 110, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg19
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Arg20, 111, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Arg20
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighHighLimitOn, 112, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighHighLimitOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighLimitOn, 113, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighLimitOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowLimitOn, 114, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowLimitOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowLowLimitOn, 115, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowLowLimitOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighHighAlarmName, 116, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighHighAlarmName
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.HighAlarmName, 117, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.HighAlarmName
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowAlarmName, 118, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowAlarmName
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LowLowAlarmName, 119, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LowLowAlarmName
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ArgSrc, 120, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ArgSrc
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ValidArgOn, 121, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ValidArgOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "No"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ReadV1, 122, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ReadV1
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Measured value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ReadV2, 123, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ReadV2
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Measured value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ReadV3, 124, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ReadV3
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Measured value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ReadV4, 125, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ReadV4
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Measured value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ReadV5, 126, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ReadV5
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Measured value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ReadV6, 127, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ReadV6
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Measured value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.RealV1, 128, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.RealV1
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Simulated value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.RealV2, 129, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.RealV2
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Simulated value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.RealV3, 130, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.RealV3
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Simulated value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.RealV4, 131, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.RealV4
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Simulated value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.RealV5, 132, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.RealV5
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Simulated value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.RealV6, 133, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.RealV6
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Simulated value"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.SmoothingPeriod, 134, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.SmoothingPeriod
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "800"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.MarginAccuracy, 135, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.MarginAccuracy
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.OEMTolerance, 136, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.OEMTolerance
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.NbPAllowed, 137, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.NbPAllowed
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "6"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.CalibrationMeasured, 138, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.CalibrationMeasured
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """none"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ChannelType, 139, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ChannelType
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.ChannelRange, 140, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.ChannelRange
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.TransducerRange, 141, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.TransducerRange
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.CalibrationRange, 142, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.CalibrationRange
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Make1, 143, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Make1
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Model1, 144, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Model1
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.SerialNumber1, 145, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.SerialNumber1
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate1, 146, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.CalibrationDueDate1
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Make2, 147, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Make2
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Model2, 148, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Model2
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.SerialNumber2, 149, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.SerialNumber2
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate2, 150, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.CalibrationDueDate2
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Make3, 151, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Make3
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Model3, 152, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Model3
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.SerialNumber3, 153, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.SerialNumber3
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate3, 154, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.CalibrationDueDate3
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Make4, 155, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Make4
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Model4, 156, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Model4
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.SerialNumber4, 157, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.SerialNumber4
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.CalibrationDueDate4, 158, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.CalibrationDueDate4
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Comments, 159, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Comments
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.CalibratedBy, 160, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.CalibratedBy
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Assistant, 161, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Assistant
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.Approval, 162, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.Approval
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.LastCalibDate, 163, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.LastCalibDate
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.NewCalibDate, 164, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.NewCalibDate
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.OPCGroup, 165, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.OPCGroup
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.OPCItemID, 166, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.OPCItemID
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.TCTransducerSN, 167, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.TCTransducerSN
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.CalibrationTemp, 168, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.CalibrationTemp
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnTemplateRealMetadata.ColumnNames.CalibrationHumidity, 169, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnTemplateRealMetadata.PropertyNames.CalibrationHumidity
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As DacnTemplateRealMetadata
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
			 Public Const Name As String = "Name"
			 Public Const RdataID As String = "RdataID"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const FieldEquipmentID As String = "FieldEquipmentID"
			 Public Const DeviceID As String = "DeviceID"
			 Public Const InChannelType As String = "In_Channel_Type"
			 Public Const Channel As String = "Channel"
			 Public Const BaseAddress As String = "Base_Address"
			 Public Const PhysicalChannel As String = "Physical_Channel"
			 Public Const MinDCValue As String = "MinDCValue"
			 Public Const MaxDCValue As String = "MaxDCValue"
			 Public Const TerminalConfiguration As String = "TerminalConfiguration"
			 Public Const AlarmOn As String = "AlarmOn"
			 Public Const StatisticsOn As String = "StatisticsOn"
			 Public Const TextConversionOn As String = "TextConversionOn"
			 Public Const DataAcquisitionOn As String = "DataAcquisitionOn"
			 Public Const DataProcessingOn As String = "DataProcessingOn"
			 Public Const PeriodInMsec As String = "Period in msec"
			 Public Const TreatOnOption As String = "TreatOnOption"
			 Public Const DefaultValue As String = "DefaultValue"
			 Public Const UpperLimit As String = "UpperLimit"
			 Public Const LowerLimit As String = "LowerLimit"
			 Public Const DeltaVPlus As String = "DeltaVPlus"
			 Public Const DeltaVMinus As String = "DeltaVMinus"
			 Public Const RocPlus As String = "RocPlus"
			 Public Const RocMinus As String = "RocMinus"
			 Public Const EventOn As String = "EventOn"
			 Public Const HighHighLimit As String = "HighHighLimit"
			 Public Const HighLimit As String = "HighLimit"
			 Public Const LowLimit As String = "LowLimit"
			 Public Const LowLowLimit As String = "LowLowLimit"
			 Public Const HighHighAlarm As String = "HighHighAlarm"
			 Public Const HighAlarm As String = "HighAlarm"
			 Public Const LowAlarm As String = "LowAlarm"
			 Public Const LowLowAlarm As String = "LowLowAlarm"
			 Public Const HighHighBand As String = "HighHighBand"
			 Public Const HighBand As String = "HighBand"
			 Public Const LowBand As String = "LowBand"
			 Public Const LowLowBand As String = "LowLowBand"
			 Public Const HighHighAlarmOn As String = "HighHighAlarmOn"
			 Public Const HighAlarmOn As String = "HighAlarmOn"
			 Public Const LowAlarmOn As String = "LowAlarmOn"
			 Public Const LowLowAlarmOn As String = "LowLowAlarmOn"
			 Public Const HighHighAlarmSource As String = "HighHighAlarmSource"
			 Public Const HighAlarmSource As String = "HighAlarmSource"
			 Public Const LowAlarmSource As String = "LowAlarmSource"
			 Public Const LowLowAlarmSource As String = "LowLowAlarmSource"
			 Public Const TextPvFormat As String = "TextPvFormat"
			 Public Const HMIAliasName As String = "HMIAliasName"
			 Public Const FILEAliasName As String = "FILEAliasName"
			 Public Const EngUnit As String = "EngUnit"
			 Public Const EngUnitID As String = "EngUnitID"
			 Public Const MinMaxOn As String = "MinMaxOn"
			 Public Const DeltaVOn As String = "DeltaVOn"
			 Public Const DeltaTOn As String = "DeltaTOn"
			 Public Const RocOn As String = "RocOn"
			 Public Const SumVOn As String = "SumVOn"
			 Public Const SumTOn As String = "SumTOn"
			 Public Const AvgOn As String = "AvgOn"
			 Public Const AvgTypeID As String = "AvgTypeID"
			 Public Const AvgPeriod As String = "AvgPeriod"
			 Public Const AvgNbSamples As String = "AvgNbSamples"
			 Public Const QueueOptionID As String = "QueueOptionID"
			 Public Const ArchOn As String = "ArchOn"
			 Public Const ArchRate As String = "ArchRate"
			 Public Const TrigCondID As String = "TrigCondID"
			 Public Const TrigLevel As String = "Trig Level"
			 Public Const TrigDeadBand As String = "Trig DeadBand"
			 Public Const TimeOut As String = "TimeOut"
			 Public Const QueueOption As String = "QueueOption"
			 Public Const QueueSize As String = "QueueSize"
			 Public Const PreTrigRecord As String = "PreTrigRecord"
			 Public Const MaxRecord As String = "MaxRecord"
			 Public Const DacqPreTrig As String = "DacqPreTrig"
			 Public Const ProcType As String = "ProcType"
			 Public Const PolyDegree As String = "PolyDegree"
			 Public Const NumOfArg As String = "NumOfArg"
			 Public Const NumOfValidArg As String = "NumOfValidArg"
			 Public Const Coeff1 As String = "Coeff1"
			 Public Const Coeff2 As String = "Coeff2"
			 Public Const Coeff3 As String = "Coeff3"
			 Public Const Coeff4 As String = "Coeff4"
			 Public Const Coeff5 As String = "Coeff5"
			 Public Const Coeff6 As String = "Coeff6"
			 Public Const NotYetApprovedCoeff1 As String = "NotYetApproved_Coeff1"
			 Public Const NotYetApprovedCoeff2 As String = "NotYetApproved_Coeff2"
			 Public Const NotYetApprovedCoeff3 As String = "NotYetApproved_Coeff3"
			 Public Const NotYetApprovedCoeff4 As String = "NotYetApproved_Coeff4"
			 Public Const NotYetApprovedCoeff5 As String = "NotYetApproved_Coeff5"
			 Public Const NotYetApprovedCoeff6 As String = "NotYetApproved_Coeff6"
			 Public Const WaitingForApproval As String = "WaitingForApproval"
			 Public Const Arg1 As String = "Arg1"
			 Public Const Arg2 As String = "Arg2"
			 Public Const Arg3 As String = "Arg3"
			 Public Const Arg4 As String = "Arg4"
			 Public Const Arg5 As String = "Arg5"
			 Public Const Arg6 As String = "Arg6"
			 Public Const Arg7 As String = "Arg7"
			 Public Const Arg8 As String = "Arg8"
			 Public Const Arg9 As String = "Arg9"
			 Public Const Arg10 As String = "Arg10"
			 Public Const Arg11 As String = "Arg11"
			 Public Const Arg12 As String = "Arg12"
			 Public Const Arg13 As String = "Arg13"
			 Public Const Arg14 As String = "Arg14"
			 Public Const Arg15 As String = "Arg15"
			 Public Const Arg16 As String = "Arg16"
			 Public Const Arg17 As String = "Arg17"
			 Public Const Arg18 As String = "Arg18"
			 Public Const Arg19 As String = "Arg19"
			 Public Const Arg20 As String = "Arg20"
			 Public Const HighHighLimitOn As String = "HighHighLimitOn"
			 Public Const HighLimitOn As String = "HighLimitOn"
			 Public Const LowLimitOn As String = "LowLimitOn"
			 Public Const LowLowLimitOn As String = "LowLowLimitOn"
			 Public Const HighHighAlarmName As String = "HighHighAlarmName"
			 Public Const HighAlarmName As String = "HighAlarmName"
			 Public Const LowAlarmName As String = "LowAlarmName"
			 Public Const LowLowAlarmName As String = "LowLowAlarmName"
			 Public Const ArgSrc As String = "ArgSrc"
			 Public Const ValidArgOn As String = "ValidArgOn"
			 Public Const ReadV1 As String = "read_v1"
			 Public Const ReadV2 As String = "read_v2"
			 Public Const ReadV3 As String = "read_v3"
			 Public Const ReadV4 As String = "read_v4"
			 Public Const ReadV5 As String = "read_v5"
			 Public Const ReadV6 As String = "read_v6"
			 Public Const RealV1 As String = "real_v1"
			 Public Const RealV2 As String = "real_v2"
			 Public Const RealV3 As String = "real_v3"
			 Public Const RealV4 As String = "real_v4"
			 Public Const RealV5 As String = "real_v5"
			 Public Const RealV6 As String = "real_v6"
			 Public Const SmoothingPeriod As String = "Smoothing_Period"
			 Public Const MarginAccuracy As String = "margin_accuracy"
			 Public Const OEMTolerance As String = "OEM_Tolerance"
			 Public Const NbPAllowed As String = "nb_p_allowed"
			 Public Const CalibrationMeasured As String = "Calibration_measured"
			 Public Const ChannelType As String = "Channel_type"
			 Public Const ChannelRange As String = "Channel_range"
			 Public Const TransducerRange As String = "Transducer_Range"
			 Public Const CalibrationRange As String = "Calibration_range"
			 Public Const Make1 As String = "Make1"
			 Public Const Model1 As String = "Model1"
			 Public Const SerialNumber1 As String = "Serial_Number1"
			 Public Const CalibrationDueDate1 As String = "Calibration_Due_Date1"
			 Public Const Make2 As String = "Make2"
			 Public Const Model2 As String = "Model2"
			 Public Const SerialNumber2 As String = "Serial_Number2"
			 Public Const CalibrationDueDate2 As String = "Calibration_Due_Date2"
			 Public Const Make3 As String = "Make3"
			 Public Const Model3 As String = "Model3"
			 Public Const SerialNumber3 As String = "Serial_Number3"
			 Public Const CalibrationDueDate3 As String = "Calibration_Due_Date3"
			 Public Const Make4 As String = "Make4"
			 Public Const Model4 As String = "Model4"
			 Public Const SerialNumber4 As String = "Serial_Number4"
			 Public Const CalibrationDueDate4 As String = "Calibration_Due_Date4"
			 Public Const Comments As String = "Comments"
			 Public Const CalibratedBy As String = "Calibrated_by"
			 Public Const Assistant As String = "Assistant"
			 Public Const Approval As String = "Approval"
			 Public Const LastCalibDate As String = "LastCalibDate"
			 Public Const NewCalibDate As String = "NewCalibDate"
			 Public Const OPCGroup As String = "OPC_Group"
			 Public Const OPCItemID As String = "OPC_ItemID"
			 Public Const TCTransducerSN As String = "TC_Transducer_SN"
			 Public Const CalibrationTemp As String = "Calibration_Temp"
			 Public Const CalibrationHumidity As String = "Calibration_Humidity"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const Name As String = "Name"
			 Public Const RdataID As String = "RdataID"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const FieldEquipmentID As String = "FieldEquipmentID"
			 Public Const DeviceID As String = "DeviceID"
			 Public Const InChannelType As String = "InChannelType"
			 Public Const Channel As String = "Channel"
			 Public Const BaseAddress As String = "BaseAddress"
			 Public Const PhysicalChannel As String = "PhysicalChannel"
			 Public Const MinDCValue As String = "MinDCValue"
			 Public Const MaxDCValue As String = "MaxDCValue"
			 Public Const TerminalConfiguration As String = "TerminalConfiguration"
			 Public Const AlarmOn As String = "AlarmOn"
			 Public Const StatisticsOn As String = "StatisticsOn"
			 Public Const TextConversionOn As String = "TextConversionOn"
			 Public Const DataAcquisitionOn As String = "DataAcquisitionOn"
			 Public Const DataProcessingOn As String = "DataProcessingOn"
			 Public Const PeriodInMsec As String = "PeriodInMsec"
			 Public Const TreatOnOption As String = "TreatOnOption"
			 Public Const DefaultValue As String = "DefaultValue"
			 Public Const UpperLimit As String = "UpperLimit"
			 Public Const LowerLimit As String = "LowerLimit"
			 Public Const DeltaVPlus As String = "DeltaVPlus"
			 Public Const DeltaVMinus As String = "DeltaVMinus"
			 Public Const RocPlus As String = "RocPlus"
			 Public Const RocMinus As String = "RocMinus"
			 Public Const EventOn As String = "EventOn"
			 Public Const HighHighLimit As String = "HighHighLimit"
			 Public Const HighLimit As String = "HighLimit"
			 Public Const LowLimit As String = "LowLimit"
			 Public Const LowLowLimit As String = "LowLowLimit"
			 Public Const HighHighAlarm As String = "HighHighAlarm"
			 Public Const HighAlarm As String = "HighAlarm"
			 Public Const LowAlarm As String = "LowAlarm"
			 Public Const LowLowAlarm As String = "LowLowAlarm"
			 Public Const HighHighBand As String = "HighHighBand"
			 Public Const HighBand As String = "HighBand"
			 Public Const LowBand As String = "LowBand"
			 Public Const LowLowBand As String = "LowLowBand"
			 Public Const HighHighAlarmOn As String = "HighHighAlarmOn"
			 Public Const HighAlarmOn As String = "HighAlarmOn"
			 Public Const LowAlarmOn As String = "LowAlarmOn"
			 Public Const LowLowAlarmOn As String = "LowLowAlarmOn"
			 Public Const HighHighAlarmSource As String = "HighHighAlarmSource"
			 Public Const HighAlarmSource As String = "HighAlarmSource"
			 Public Const LowAlarmSource As String = "LowAlarmSource"
			 Public Const LowLowAlarmSource As String = "LowLowAlarmSource"
			 Public Const TextPvFormat As String = "TextPvFormat"
			 Public Const HMIAliasName As String = "HMIAliasName"
			 Public Const FILEAliasName As String = "FILEAliasName"
			 Public Const EngUnit As String = "EngUnit"
			 Public Const EngUnitID As String = "EngUnitID"
			 Public Const MinMaxOn As String = "MinMaxOn"
			 Public Const DeltaVOn As String = "DeltaVOn"
			 Public Const DeltaTOn As String = "DeltaTOn"
			 Public Const RocOn As String = "RocOn"
			 Public Const SumVOn As String = "SumVOn"
			 Public Const SumTOn As String = "SumTOn"
			 Public Const AvgOn As String = "AvgOn"
			 Public Const AvgTypeID As String = "AvgTypeID"
			 Public Const AvgPeriod As String = "AvgPeriod"
			 Public Const AvgNbSamples As String = "AvgNbSamples"
			 Public Const QueueOptionID As String = "QueueOptionID"
			 Public Const ArchOn As String = "ArchOn"
			 Public Const ArchRate As String = "ArchRate"
			 Public Const TrigCondID As String = "TrigCondID"
			 Public Const TrigLevel As String = "TrigLevel"
			 Public Const TrigDeadBand As String = "TrigDeadBand"
			 Public Const TimeOut As String = "TimeOut"
			 Public Const QueueOption As String = "QueueOption"
			 Public Const QueueSize As String = "QueueSize"
			 Public Const PreTrigRecord As String = "PreTrigRecord"
			 Public Const MaxRecord As String = "MaxRecord"
			 Public Const DacqPreTrig As String = "DacqPreTrig"
			 Public Const ProcType As String = "ProcType"
			 Public Const PolyDegree As String = "PolyDegree"
			 Public Const NumOfArg As String = "NumOfArg"
			 Public Const NumOfValidArg As String = "NumOfValidArg"
			 Public Const Coeff1 As String = "Coeff1"
			 Public Const Coeff2 As String = "Coeff2"
			 Public Const Coeff3 As String = "Coeff3"
			 Public Const Coeff4 As String = "Coeff4"
			 Public Const Coeff5 As String = "Coeff5"
			 Public Const Coeff6 As String = "Coeff6"
			 Public Const NotYetApprovedCoeff1 As String = "NotYetApprovedCoeff1"
			 Public Const NotYetApprovedCoeff2 As String = "NotYetApprovedCoeff2"
			 Public Const NotYetApprovedCoeff3 As String = "NotYetApprovedCoeff3"
			 Public Const NotYetApprovedCoeff4 As String = "NotYetApprovedCoeff4"
			 Public Const NotYetApprovedCoeff5 As String = "NotYetApprovedCoeff5"
			 Public Const NotYetApprovedCoeff6 As String = "NotYetApprovedCoeff6"
			 Public Const WaitingForApproval As String = "WaitingForApproval"
			 Public Const Arg1 As String = "Arg1"
			 Public Const Arg2 As String = "Arg2"
			 Public Const Arg3 As String = "Arg3"
			 Public Const Arg4 As String = "Arg4"
			 Public Const Arg5 As String = "Arg5"
			 Public Const Arg6 As String = "Arg6"
			 Public Const Arg7 As String = "Arg7"
			 Public Const Arg8 As String = "Arg8"
			 Public Const Arg9 As String = "Arg9"
			 Public Const Arg10 As String = "Arg10"
			 Public Const Arg11 As String = "Arg11"
			 Public Const Arg12 As String = "Arg12"
			 Public Const Arg13 As String = "Arg13"
			 Public Const Arg14 As String = "Arg14"
			 Public Const Arg15 As String = "Arg15"
			 Public Const Arg16 As String = "Arg16"
			 Public Const Arg17 As String = "Arg17"
			 Public Const Arg18 As String = "Arg18"
			 Public Const Arg19 As String = "Arg19"
			 Public Const Arg20 As String = "Arg20"
			 Public Const HighHighLimitOn As String = "HighHighLimitOn"
			 Public Const HighLimitOn As String = "HighLimitOn"
			 Public Const LowLimitOn As String = "LowLimitOn"
			 Public Const LowLowLimitOn As String = "LowLowLimitOn"
			 Public Const HighHighAlarmName As String = "HighHighAlarmName"
			 Public Const HighAlarmName As String = "HighAlarmName"
			 Public Const LowAlarmName As String = "LowAlarmName"
			 Public Const LowLowAlarmName As String = "LowLowAlarmName"
			 Public Const ArgSrc As String = "ArgSrc"
			 Public Const ValidArgOn As String = "ValidArgOn"
			 Public Const ReadV1 As String = "ReadV1"
			 Public Const ReadV2 As String = "ReadV2"
			 Public Const ReadV3 As String = "ReadV3"
			 Public Const ReadV4 As String = "ReadV4"
			 Public Const ReadV5 As String = "ReadV5"
			 Public Const ReadV6 As String = "ReadV6"
			 Public Const RealV1 As String = "RealV1"
			 Public Const RealV2 As String = "RealV2"
			 Public Const RealV3 As String = "RealV3"
			 Public Const RealV4 As String = "RealV4"
			 Public Const RealV5 As String = "RealV5"
			 Public Const RealV6 As String = "RealV6"
			 Public Const SmoothingPeriod As String = "SmoothingPeriod"
			 Public Const MarginAccuracy As String = "MarginAccuracy"
			 Public Const OEMTolerance As String = "OEMTolerance"
			 Public Const NbPAllowed As String = "NbPAllowed"
			 Public Const CalibrationMeasured As String = "CalibrationMeasured"
			 Public Const ChannelType As String = "ChannelType"
			 Public Const ChannelRange As String = "ChannelRange"
			 Public Const TransducerRange As String = "TransducerRange"
			 Public Const CalibrationRange As String = "CalibrationRange"
			 Public Const Make1 As String = "Make1"
			 Public Const Model1 As String = "Model1"
			 Public Const SerialNumber1 As String = "SerialNumber1"
			 Public Const CalibrationDueDate1 As String = "CalibrationDueDate1"
			 Public Const Make2 As String = "Make2"
			 Public Const Model2 As String = "Model2"
			 Public Const SerialNumber2 As String = "SerialNumber2"
			 Public Const CalibrationDueDate2 As String = "CalibrationDueDate2"
			 Public Const Make3 As String = "Make3"
			 Public Const Model3 As String = "Model3"
			 Public Const SerialNumber3 As String = "SerialNumber3"
			 Public Const CalibrationDueDate3 As String = "CalibrationDueDate3"
			 Public Const Make4 As String = "Make4"
			 Public Const Model4 As String = "Model4"
			 Public Const SerialNumber4 As String = "SerialNumber4"
			 Public Const CalibrationDueDate4 As String = "CalibrationDueDate4"
			 Public Const Comments As String = "Comments"
			 Public Const CalibratedBy As String = "CalibratedBy"
			 Public Const Assistant As String = "Assistant"
			 Public Const Approval As String = "Approval"
			 Public Const LastCalibDate As String = "LastCalibDate"
			 Public Const NewCalibDate As String = "NewCalibDate"
			 Public Const OPCGroup As String = "OPCGroup"
			 Public Const OPCItemID As String = "OPCItemID"
			 Public Const TCTransducerSN As String = "TCTransducerSN"
			 Public Const CalibrationTemp As String = "CalibrationTemp"
			 Public Const CalibrationHumidity As String = "CalibrationHumidity"
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
			SyncLock GetType(DacnTemplateRealMetadata)
			
				If DacnTemplateRealMetadata.mapDelegates Is Nothing Then
					DacnTemplateRealMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If DacnTemplateRealMetadata._meta Is Nothing Then
					DacnTemplateRealMetadata._meta = New DacnTemplateRealMetadata()
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
				meta.AddTypeMap("Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("RdataID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("FieldEquipmentID", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DeviceID", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("InChannelType", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Channel", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("BaseAddress", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("PhysicalChannel", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MinDCValue", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("MaxDCValue", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("TerminalConfiguration", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("AlarmOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("StatisticsOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("TextConversionOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("DataAcquisitionOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("DataProcessingOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("PeriodInMsec", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("TreatOnOption", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("DefaultValue", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("UpperLimit", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("LowerLimit", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("DeltaVPlus", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("DeltaVMinus", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("RocPlus", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("RocMinus", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("EventOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("HighHighLimit", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("HighLimit", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("LowLimit", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("LowLowLimit", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("HighHighAlarm", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("HighAlarm", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("LowAlarm", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("LowLowAlarm", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("HighHighBand", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("HighBand", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("LowBand", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("LowLowBand", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("HighHighAlarmOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("HighAlarmOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("LowAlarmOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("LowLowAlarmOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("HighHighAlarmSource", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("HighAlarmSource", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("LowAlarmSource", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("LowLowAlarmSource", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("TextPvFormat", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("HMIAliasName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("FILEAliasName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("EngUnit", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("EngUnitID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MinMaxOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("DeltaVOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("DeltaTOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("RocOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("SumVOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("SumTOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("AvgOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("AvgTypeID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("AvgPeriod", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("AvgNbSamples", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("QueueOptionID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ArchOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("ArchRate", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("TrigCondID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("TrigLevel", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("TrigDeadBand", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("TimeOut", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("QueueOption", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("QueueSize", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("PreTrigRecord", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MaxRecord", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("DacqPreTrig", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("ProcType", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("PolyDegree", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("NumOfArg", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("NumOfValidArg", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Coeff1", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Coeff2", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Coeff3", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Coeff4", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Coeff5", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Coeff6", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("NotYetApprovedCoeff1", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("NotYetApprovedCoeff2", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("NotYetApprovedCoeff3", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("NotYetApprovedCoeff4", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("NotYetApprovedCoeff5", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("NotYetApprovedCoeff6", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("WaitingForApproval", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Arg1", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg2", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg3", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg4", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg5", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg6", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg7", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg8", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg9", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg10", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg11", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg12", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg13", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg14", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg15", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg16", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg17", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg18", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg19", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Arg20", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("HighHighLimitOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("HighLimitOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("LowLimitOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("LowLowLimitOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("HighHighAlarmName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("HighAlarmName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LowAlarmName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LowLowAlarmName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ArgSrc", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ValidArgOn", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("ReadV1", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("ReadV2", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("ReadV3", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("ReadV4", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("ReadV5", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("ReadV6", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("RealV1", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("RealV2", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("RealV3", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("RealV4", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("RealV5", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("RealV6", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("SmoothingPeriod", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MarginAccuracy", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("OEMTolerance", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("NbPAllowed", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("CalibrationMeasured", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ChannelType", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ChannelRange", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("TransducerRange", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationRange", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Make1", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Model1", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("SerialNumber1", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationDueDate1", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Make2", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Model2", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("SerialNumber2", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationDueDate2", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Make3", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Model3", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("SerialNumber3", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationDueDate3", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Make4", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Model4", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("SerialNumber4", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationDueDate4", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Comments", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibratedBy", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Assistant", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Approval", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LastCalibDate", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("NewCalibDate", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OPCGroup", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OPCItemID", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("TCTransducerSN", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationTemp", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationHumidity", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "DacnTemplateReal"
				meta.Destination = "DacnTemplateReal"
				
				meta.spInsert = "proc_DacnTemplateRealInsert"
				meta.spUpdate = "proc_DacnTemplateRealUpdate"
				meta.spDelete = "proc_DacnTemplateRealDelete"
				meta.spLoadAll = "proc_DacnTemplateRealLoadAll"
				meta.spLoadByPrimaryKey = "proc_DacnTemplateRealLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As DacnTemplateRealMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

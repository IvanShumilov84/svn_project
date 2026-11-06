
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 23/01/2014 2:08:00 PM
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
	' Encapsulates the 'DACN1_CANBUS_LABEL' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(Dacn1CanbusLabel))> _
	<XmlType("Dacn1CanbusLabel")> _	
	Partial Public Class Dacn1CanbusLabel 
		Inherits esDacn1CanbusLabel
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New Dacn1CanbusLabel()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32)
			Dim obj As New Dacn1CanbusLabel()
			obj.Id = id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New Dacn1CanbusLabel()
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
	<XmlType("Dacn1CanbusLabelCollection")> _
	Partial Public Class Dacn1CanbusLabelCollection
		Inherits esDacn1CanbusLabelCollection
		Implements IEnumerable(Of Dacn1CanbusLabel)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32) As Dacn1CanbusLabel
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(Dacn1CanbusLabel))> _
		Public Class Dacn1CanbusLabelCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of Dacn1CanbusLabelCollection)
			
			Public Shared Widening Operator CType(packet As Dacn1CanbusLabelCollectionWCFPacket) As Dacn1CanbusLabelCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As Dacn1CanbusLabelCollection) As Dacn1CanbusLabelCollectionWCFPacket
				Return New Dacn1CanbusLabelCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class Dacn1CanbusLabelQuery 
		Inherits esDacn1CanbusLabelQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "Dacn1CanbusLabelQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As Dacn1CanbusLabelQuery) As String
			Return Dacn1CanbusLabelQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As Dacn1CanbusLabelQuery
			Return DirectCast(Dacn1CanbusLabelQuery.SerializeHelper.FromXml(query, GetType(Dacn1CanbusLabelQuery)), Dacn1CanbusLabelQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDacn1CanbusLabel
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
		
			Dim query As New Dacn1CanbusLabelQuery()
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
		' Maps to DACN1_CANBUS_LABEL.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1CanbusLabelMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1CanbusLabelMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.Engine_Model
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.EngineModel, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.ChannelName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelName As System.String
			Get
				Return MyBase.GetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.ChannelName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.ChannelName, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.ChannelName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' CANBus Channel A/B
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CANBusChannel As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1CanbusLabelMetadata.ColumnNames.CANBusChannel)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1CanbusLabelMetadata.ColumnNames.CANBusChannel, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.CANBusChannel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.TX/RX
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TxRx As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1CanbusLabelMetadata.ColumnNames.TxRx)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1CanbusLabelMetadata.ColumnNames.TxRx, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.TxRx)
				End If
			End Set
		End Property	
			
		' <summary>
		' Hexadecimal CANBus ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CANBusID As System.String
			Get
				Return MyBase.GetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.CANBusID)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.CANBusID, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.CANBusID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.Word
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Word As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(Dacn1CanbusLabelMetadata.ColumnNames.Word)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(Dacn1CanbusLabelMetadata.ColumnNames.Word, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Word)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.MSB
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Msb As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(Dacn1CanbusLabelMetadata.ColumnNames.Msb)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(Dacn1CanbusLabelMetadata.ColumnNames.Msb, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Msb)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.LSB
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Lsb As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(Dacn1CanbusLabelMetadata.ColumnNames.Lsb)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(Dacn1CanbusLabelMetadata.ColumnNames.Lsb, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Lsb)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.LSBResolution
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LSBResolution As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(Dacn1CanbusLabelMetadata.ColumnNames.LSBResolution)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(Dacn1CanbusLabelMetadata.ColumnNames.LSBResolution, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.LSBResolution)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.EngUnitID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngUnitID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1CanbusLabelMetadata.ColumnNames.EngUnitID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1CanbusLabelMetadata.ColumnNames.EngUnitID, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.EngUnitID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.DisplayFormat
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DisplayFormat As System.String
			Get
				Return MyBase.GetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.DisplayFormat)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.DisplayFormat, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.DisplayFormat)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.DecimalDigits
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DecimalDigits As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1CanbusLabelMetadata.ColumnNames.DecimalDigits)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1CanbusLabelMetadata.ColumnNames.DecimalDigits, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.DecimalDigits)
				End If
			End Set
		End Property	
			
		' <summary>
		' Default Channel (Tx only)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DefaultTxChannel As System.String
			Get
				Return MyBase.GetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.DefaultTxChannel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.DefaultTxChannel, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.DefaultTxChannel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Default value (Tx only)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DefaultTxValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(Dacn1CanbusLabelMetadata.ColumnNames.DefaultTxValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(Dacn1CanbusLabelMetadata.ColumnNames.DefaultTxValue, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.DefaultTxValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Signed/Unsigned Integer (Rx only)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SignedInteger As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1CanbusLabelMetadata.ColumnNames.SignedInteger)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1CanbusLabelMetadata.ColumnNames.SignedInteger, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.SignedInteger)
				End If
			End Set
		End Property	
			
		' <summary>
		' Is it a floating point (Tx only)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property IEEE754Type As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1CanbusLabelMetadata.ColumnNames.IEEE754Type)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1CanbusLabelMetadata.ColumnNames.IEEE754Type, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.IEEE754Type)
				End If
			End Set
		End Property	
			
		' <summary>
		' Minimum Transmit Interval (mSec)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MinInterval As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(Dacn1CanbusLabelMetadata.ColumnNames.MinInterval)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(Dacn1CanbusLabelMetadata.ColumnNames.MinInterval, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.MinInterval)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maximum Transmit Interval (mSec)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MaxInterval As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(Dacn1CanbusLabelMetadata.ColumnNames.MaxInterval)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(Dacn1CanbusLabelMetadata.ColumnNames.MaxInterval, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.MaxInterval)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.DisplayOnCELDAS
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DisplayOnCELDAS As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1CanbusLabelMetadata.ColumnNames.DisplayOnCELDAS)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1CanbusLabelMetadata.ColumnNames.DisplayOnCELDAS, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.DisplayOnCELDAS)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.Logger_Enabled
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LoggerEnabled As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1CanbusLabelMetadata.ColumnNames.LoggerEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1CanbusLabelMetadata.ColumnNames.LoggerEnabled, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.LoggerEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.Logger_Index
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LoggerIndex As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1CanbusLabelMetadata.ColumnNames.LoggerIndex)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1CanbusLabelMetadata.ColumnNames.LoggerIndex, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.LoggerIndex)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.Log_Report_Categorie
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LogReportCategorie As System.String
			Get
				Return MyBase.GetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.LogReportCategorie)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1CanbusLabelMetadata.ColumnNames.LogReportCategorie, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.LogReportCategorie)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.Log_Report_Order_In_Categorie
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LogReportOrderInCategorie As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1CanbusLabelMetadata.ColumnNames.LogReportOrderInCategorie)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1CanbusLabelMetadata.ColumnNames.LogReportOrderInCategorie, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.LogReportOrderInCategorie)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_CANBUS_LABEL.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1CanbusLabelMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1CanbusLabelMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Unavailable)
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
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
						Case "ChannelName"
							Me.str().ChannelName = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "CANBusChannel"
							Me.str().CANBusChannel = CType(value, string)
												
						Case "TxRx"
							Me.str().TxRx = CType(value, string)
												
						Case "CANBusID"
							Me.str().CANBusID = CType(value, string)
												
						Case "Word"
							Me.str().Word = CType(value, string)
												
						Case "Msb"
							Me.str().Msb = CType(value, string)
												
						Case "Lsb"
							Me.str().Lsb = CType(value, string)
												
						Case "LSBResolution"
							Me.str().LSBResolution = CType(value, string)
												
						Case "EngUnitID"
							Me.str().EngUnitID = CType(value, string)
												
						Case "DisplayFormat"
							Me.str().DisplayFormat = CType(value, string)
												
						Case "DecimalDigits"
							Me.str().DecimalDigits = CType(value, string)
												
						Case "DefaultTxChannel"
							Me.str().DefaultTxChannel = CType(value, string)
												
						Case "DefaultTxValue"
							Me.str().DefaultTxValue = CType(value, string)
												
						Case "SignedInteger"
							Me.str().SignedInteger = CType(value, string)
												
						Case "IEEE754Type"
							Me.str().IEEE754Type = CType(value, string)
												
						Case "MinInterval"
							Me.str().MinInterval = CType(value, string)
												
						Case "MaxInterval"
							Me.str().MaxInterval = CType(value, string)
												
						Case "DisplayOnCELDAS"
							Me.str().DisplayOnCELDAS = CType(value, string)
												
						Case "LoggerEnabled"
							Me.str().LoggerEnabled = CType(value, string)
												
						Case "LoggerIndex"
							Me.str().LoggerIndex = CType(value, string)
												
						Case "LogReportCategorie"
							Me.str().LogReportCategorie = CType(value, string)
												
						Case "LogReportOrderInCategorie"
							Me.str().LogReportOrderInCategorie = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Id)
							End If
						
						Case "CANBusChannel"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.CANBusChannel = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.CANBusChannel)
							End If
						
						Case "TxRx"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.TxRx = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.TxRx)
							End If
						
						Case "Word"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.Word = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Word)
							End If
						
						Case "Msb"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.Msb = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Msb)
							End If
						
						Case "Lsb"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.Lsb = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Lsb)
							End If
						
						Case "LSBResolution"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.LSBResolution = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.LSBResolution)
							End If
						
						Case "EngUnitID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.EngUnitID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.EngUnitID)
							End If
						
						Case "DecimalDigits"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.DecimalDigits = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.DecimalDigits)
							End If
						
						Case "DefaultTxValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.DefaultTxValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.DefaultTxValue)
							End If
						
						Case "SignedInteger"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.SignedInteger = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.SignedInteger)
							End If
						
						Case "IEEE754Type"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.IEEE754Type = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.IEEE754Type)
							End If
						
						Case "MinInterval"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.MinInterval = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.MinInterval)
							End If
						
						Case "MaxInterval"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.MaxInterval = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.MaxInterval)
							End If
						
						Case "DisplayOnCELDAS"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.DisplayOnCELDAS = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.DisplayOnCELDAS)
							End If
						
						Case "LoggerEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.LoggerEnabled = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.LoggerEnabled)
							End If
						
						Case "LoggerIndex"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LoggerIndex = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.LoggerIndex)
							End If
						
						Case "LogReportOrderInCategorie"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LogReportOrderInCategorie = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.LogReportOrderInCategorie)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1CanbusLabelMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esDacn1CanbusLabel)
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
		  	
			Public Property CANBusChannel As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.CANBusChannel
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CANBusChannel = Nothing
					Else
						entity.CANBusChannel = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property TxRx As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.TxRx
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TxRx = Nothing
					Else
						entity.TxRx = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property CANBusID As System.String 
				Get
					Dim data_ As System.String = entity.CANBusID
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CANBusID = Nothing
					Else
						entity.CANBusID = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Word As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.Word
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Word = Nothing
					Else
						entity.Word = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property Msb As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.Msb
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Msb = Nothing
					Else
						entity.Msb = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property Lsb As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.Lsb
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Lsb = Nothing
					Else
						entity.Lsb = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property LSBResolution As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.LSBResolution
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LSBResolution = Nothing
					Else
						entity.LSBResolution = Convert.ToDouble(Value)
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
		  	
			Public Property DisplayFormat As System.String 
				Get
					Dim data_ As System.String = entity.DisplayFormat
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DisplayFormat = Nothing
					Else
						entity.DisplayFormat = Convert.ToString(Value)
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
		  	
			Public Property DefaultTxChannel As System.String 
				Get
					Dim data_ As System.String = entity.DefaultTxChannel
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DefaultTxChannel = Nothing
					Else
						entity.DefaultTxChannel = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DefaultTxValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.DefaultTxValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DefaultTxValue = Nothing
					Else
						entity.DefaultTxValue = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property SignedInteger As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.SignedInteger
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SignedInteger = Nothing
					Else
						entity.SignedInteger = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property IEEE754Type As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.IEEE754Type
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.IEEE754Type = Nothing
					Else
						entity.IEEE754Type = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property MinInterval As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.MinInterval
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MinInterval = Nothing
					Else
						entity.MinInterval = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property MaxInterval As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.MaxInterval
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MaxInterval = Nothing
					Else
						entity.MaxInterval = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property DisplayOnCELDAS As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.DisplayOnCELDAS
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DisplayOnCELDAS = Nothing
					Else
						entity.DisplayOnCELDAS = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property LoggerEnabled As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.LoggerEnabled
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LoggerEnabled = Nothing
					Else
						entity.LoggerEnabled = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property LoggerIndex As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.LoggerIndex
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LoggerIndex = Nothing
					Else
						entity.LoggerIndex = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property LogReportCategorie As System.String 
				Get
					Dim data_ As System.String = entity.LogReportCategorie
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LogReportCategorie = Nothing
					Else
						entity.LogReportCategorie = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property LogReportOrderInCategorie As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.LogReportOrderInCategorie
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LogReportOrderInCategorie = Nothing
					Else
						entity.LogReportOrderInCategorie = Convert.ToInt32(Value)
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
		  

			Private entity As esDacn1CanbusLabel
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1CanbusLabelMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As Dacn1CanbusLabelQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1CanbusLabelQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As Dacn1CanbusLabelQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As Dacn1CanbusLabelQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As Dacn1CanbusLabelQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDacn1CanbusLabelCollection
		Inherits esEntityCollection(Of Dacn1CanbusLabel)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1CanbusLabelMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "Dacn1CanbusLabelCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As Dacn1CanbusLabelQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1CanbusLabelQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As Dacn1CanbusLabelQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New Dacn1CanbusLabelQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As Dacn1CanbusLabelQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, Dacn1CanbusLabelQuery))
		End Sub
		
		#End Region
						
		Private m_query As Dacn1CanbusLabelQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDacn1CanbusLabelQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return Dacn1CanbusLabelMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "EngineModel" 
					Return Me.EngineModel
				Case "ChannelName" 
					Return Me.ChannelName
				Case "Description" 
					Return Me.Description
				Case "CANBusChannel" 
					Return Me.CANBusChannel
				Case "TxRx" 
					Return Me.TxRx
				Case "CANBusID" 
					Return Me.CANBusID
				Case "Word" 
					Return Me.Word
				Case "Msb" 
					Return Me.Msb
				Case "Lsb" 
					Return Me.Lsb
				Case "LSBResolution" 
					Return Me.LSBResolution
				Case "EngUnitID" 
					Return Me.EngUnitID
				Case "DisplayFormat" 
					Return Me.DisplayFormat
				Case "DecimalDigits" 
					Return Me.DecimalDigits
				Case "DefaultTxChannel" 
					Return Me.DefaultTxChannel
				Case "DefaultTxValue" 
					Return Me.DefaultTxValue
				Case "SignedInteger" 
					Return Me.SignedInteger
				Case "IEEE754Type" 
					Return Me.IEEE754Type
				Case "MinInterval" 
					Return Me.MinInterval
				Case "MaxInterval" 
					Return Me.MaxInterval
				Case "DisplayOnCELDAS" 
					Return Me.DisplayOnCELDAS
				Case "LoggerEnabled" 
					Return Me.LoggerEnabled
				Case "LoggerIndex" 
					Return Me.LoggerIndex
				Case "LogReportCategorie" 
					Return Me.LogReportCategorie
				Case "LogReportOrderInCategorie" 
					Return Me.LogReportOrderInCategorie
				Case "Unavailable" 
					Return Me.Unavailable
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.ChannelName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CANBusChannel As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.CANBusChannel, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property TxRx As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.TxRx, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property CANBusID As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.CANBusID, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Word As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.Word, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property Msb As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.Msb, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property Lsb As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.Lsb, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property LSBResolution As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.LSBResolution, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property EngUnitID As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.EngUnitID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property DisplayFormat As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.DisplayFormat, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DecimalDigits As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.DecimalDigits, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property DefaultTxChannel As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.DefaultTxChannel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DefaultTxValue As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.DefaultTxValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property SignedInteger As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.SignedInteger, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property IEEE754Type As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.IEEE754Type, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property MinInterval As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.MinInterval, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property MaxInterval As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.MaxInterval, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property DisplayOnCELDAS As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.DisplayOnCELDAS, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property LoggerEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.LoggerEnabled, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property LoggerIndex As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.LoggerIndex, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property LogReportCategorie As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.LogReportCategorie, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LogReportOrderInCategorie As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.LogReportOrderInCategorie, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1CanbusLabelMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class Dacn1CanbusLabel 
		Inherits esDacn1CanbusLabel
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class Dacn1CanbusLabelMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.EngineModel
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.ChannelName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.ChannelName
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.Description, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.Description
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.CANBusChannel, 4, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.CANBusChannel
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "CANBus Channel A/B"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.TxRx, 5, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.TxRx
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.CANBusID, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.CANBusID
			c.CharacterMaxLength = 255
			c.Description = "Hexadecimal CANBus ID"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.Word, 7, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.Word
			c.NumericPrecision = 5
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.Msb, 8, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.Msb
			c.NumericPrecision = 5
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.Lsb, 9, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.Lsb
			c.NumericPrecision = 5
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.LSBResolution, 10, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.LSBResolution
			c.NumericPrecision = 15
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.EngUnitID, 11, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.EngUnitID
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.DisplayFormat, 12, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.DisplayFormat
			c.CharacterMaxLength = 3
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.DecimalDigits, 13, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.DecimalDigits
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.DefaultTxChannel, 14, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.DefaultTxChannel
			c.CharacterMaxLength = 255
			c.Description = "Default Channel (Tx only)"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.DefaultTxValue, 15, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.DefaultTxValue
			c.NumericPrecision = 15
			c.Description = "Default value (Tx only)"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.SignedInteger, 16, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.SignedInteger
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Signed/Unsigned Integer (Rx only)"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.IEEE754Type, 17, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.IEEE754Type
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Is it a floating point (Tx only)"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.MinInterval, 18, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.MinInterval
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Minimum Transmit Interval (mSec)"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.MaxInterval, 19, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.MaxInterval
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Maximum Transmit Interval (mSec)"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.DisplayOnCELDAS, 20, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.DisplayOnCELDAS
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "Yes"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.LoggerEnabled, 21, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.LoggerEnabled
			c.CharacterMaxLength = 2
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.LoggerIndex, 22, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.LoggerIndex
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.LogReportCategorie, 23, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.LogReportCategorie
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.LogReportOrderInCategorie, 24, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.LogReportOrderInCategorie
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1CanbusLabelMetadata.ColumnNames.Unavailable, 25, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1CanbusLabelMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As Dacn1CanbusLabelMetadata
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
			 Public Const EngineModel As String = "Engine_Model"
			 Public Const ChannelName As String = "ChannelName"
			 Public Const Description As String = "Description"
			 Public Const CANBusChannel As String = "CANBus_Channel"
			 Public Const TxRx As String = "TX/RX"
			 Public Const CANBusID As String = "CANBusID"
			 Public Const Word As String = "Word"
			 Public Const Msb As String = "MSB"
			 Public Const Lsb As String = "LSB"
			 Public Const LSBResolution As String = "LSBResolution"
			 Public Const EngUnitID As String = "EngUnitID"
			 Public Const DisplayFormat As String = "DisplayFormat"
			 Public Const DecimalDigits As String = "DecimalDigits"
			 Public Const DefaultTxChannel As String = "DefaultTxChannel"
			 Public Const DefaultTxValue As String = "DefaultTxValue"
			 Public Const SignedInteger As String = "SignedInteger"
			 Public Const IEEE754Type As String = "IEEE754Type"
			 Public Const MinInterval As String = "Min_Interval"
			 Public Const MaxInterval As String = "Max_Interval"
			 Public Const DisplayOnCELDAS As String = "DisplayOnCELDAS"
			 Public Const LoggerEnabled As String = "Logger_Enabled"
			 Public Const LoggerIndex As String = "Logger_Index"
			 Public Const LogReportCategorie As String = "Log_Report_Categorie"
			 Public Const LogReportOrderInCategorie As String = "Log_Report_Order_In_Categorie"
			 Public Const Unavailable As String = "Unavailable"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const ChannelName As String = "ChannelName"
			 Public Const Description As String = "Description"
			 Public Const CANBusChannel As String = "CANBusChannel"
			 Public Const TxRx As String = "TxRx"
			 Public Const CANBusID As String = "CANBusID"
			 Public Const Word As String = "Word"
			 Public Const Msb As String = "Msb"
			 Public Const Lsb As String = "Lsb"
			 Public Const LSBResolution As String = "LSBResolution"
			 Public Const EngUnitID As String = "EngUnitID"
			 Public Const DisplayFormat As String = "DisplayFormat"
			 Public Const DecimalDigits As String = "DecimalDigits"
			 Public Const DefaultTxChannel As String = "DefaultTxChannel"
			 Public Const DefaultTxValue As String = "DefaultTxValue"
			 Public Const SignedInteger As String = "SignedInteger"
			 Public Const IEEE754Type As String = "IEEE754Type"
			 Public Const MinInterval As String = "MinInterval"
			 Public Const MaxInterval As String = "MaxInterval"
			 Public Const DisplayOnCELDAS As String = "DisplayOnCELDAS"
			 Public Const LoggerEnabled As String = "LoggerEnabled"
			 Public Const LoggerIndex As String = "LoggerIndex"
			 Public Const LogReportCategorie As String = "LogReportCategorie"
			 Public Const LogReportOrderInCategorie As String = "LogReportOrderInCategorie"
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
			SyncLock GetType(Dacn1CanbusLabelMetadata)
			
				If Dacn1CanbusLabelMetadata.mapDelegates Is Nothing Then
					Dacn1CanbusLabelMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If Dacn1CanbusLabelMetadata._meta Is Nothing Then
					Dacn1CanbusLabelMetadata._meta = New Dacn1CanbusLabelMetadata()
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
				meta.AddTypeMap("EngineModel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ChannelName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CANBusChannel", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("TxRx", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("CANBusID", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Word", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("Msb", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("Lsb", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("LSBResolution", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("EngUnitID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("DisplayFormat", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DecimalDigits", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("DefaultTxChannel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DefaultTxValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("SignedInteger", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("IEEE754Type", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("MinInterval", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("MaxInterval", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("DisplayOnCELDAS", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("LoggerEnabled", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("LoggerIndex", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("LogReportCategorie", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LogReportOrderInCategorie", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "DACN1_CANBUS_LABEL"
				meta.Destination = "DACN1_CANBUS_LABEL"
				
				meta.spInsert = "proc_DACN1_CANBUS_LABELInsert"
				meta.spUpdate = "proc_DACN1_CANBUS_LABELUpdate"
				meta.spDelete = "proc_DACN1_CANBUS_LABELDelete"
				meta.spLoadAll = "proc_DACN1_CANBUS_LABELLoadAll"
				meta.spLoadByPrimaryKey = "proc_DACN1_CANBUS_LABELLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As Dacn1CanbusLabelMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

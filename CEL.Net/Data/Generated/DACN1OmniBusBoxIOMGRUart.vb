
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 4/22/2015 12:20:21 PM
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
	' Encapsulates the 'DACN1_OmniBusBox_IOMGR_Uart' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(DACN1OmniBusBoxIOMGRUart))> _
	<XmlType("DACN1OmniBusBoxIOMGRUart")> _	
	Partial Public Class DACN1OmniBusBoxIOMGRUart 
		Inherits esDACN1OmniBusBoxIOMGRUart
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New DACN1OmniBusBoxIOMGRUart()
		End Function
		
		#Region "Static Quick Access Methods"
		
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("DACN1OmniBusBoxIOMGRUartCollection")> _
	Partial Public Class DACN1OmniBusBoxIOMGRUartCollection
		Inherits esDACN1OmniBusBoxIOMGRUartCollection
		Implements IEnumerable(Of DACN1OmniBusBoxIOMGRUart)
	


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(DACN1OmniBusBoxIOMGRUart))> _
		Public Class DACN1OmniBusBoxIOMGRUartCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of DACN1OmniBusBoxIOMGRUartCollection)
			
			Public Shared Widening Operator CType(packet As DACN1OmniBusBoxIOMGRUartCollectionWCFPacket) As DACN1OmniBusBoxIOMGRUartCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As DACN1OmniBusBoxIOMGRUartCollection) As DACN1OmniBusBoxIOMGRUartCollectionWCFPacket
				Return New DACN1OmniBusBoxIOMGRUartCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class DACN1OmniBusBoxIOMGRUartQuery 
		Inherits esDACN1OmniBusBoxIOMGRUartQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "DACN1OmniBusBoxIOMGRUartQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As DACN1OmniBusBoxIOMGRUartQuery) As String
			Return DACN1OmniBusBoxIOMGRUartQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As DACN1OmniBusBoxIOMGRUartQuery
			Return DirectCast(DACN1OmniBusBoxIOMGRUartQuery.SerializeHelper.FromXml(query, GetType(DACN1OmniBusBoxIOMGRUartQuery)), DACN1OmniBusBoxIOMGRUartQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDACN1OmniBusBoxIOMGRUart
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey() As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic()
			Else
				Return LoadByPrimaryKeyStoredProcedure()
			End If
			
		End Function
	
        Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType) As Boolean

            If sqlAccessType = esSqlAccessType.DynamicSQL Then
                Return LoadByPrimaryKeyDynamic()
            Else
                Return LoadByPrimaryKeyStoredProcedure()
            End If

        End Function
	
		Private Function LoadByPrimaryKeyDynamic() As Boolean
		
			Dim query As New DACN1OmniBusBoxIOMGRUartQuery()
			query.Where()
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure() As Boolean
		
			Dim parms As esParameters = New esParameters()

			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToDACN1OmniBusBoxIOMGRByEngineModel = Nothing
					Me.OnPropertyChanged("UpToDACN1OmniBusBoxIOMGRByEngineModel")
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.PWC_PN
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PwcPn As System.String
			Get
				Return MyBase.GetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.PwcPn)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.PwcPn, value) Then
					Me._UpToDACN1OmniBusBoxIOMGRByEngineModel = Nothing
					Me.OnPropertyChanged("UpToDACN1OmniBusBoxIOMGRByEngineModel")
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.PwcPn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Name As System.String
			Get
				Return MyBase.GetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Name, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Source
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Source As System.String
			Get
				Return MyBase.GetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Source)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Source, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Source)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Arinc_Channel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ArincChannel As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ArincChannel)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ArincChannel, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ArincChannel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Parameter_No
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ParameterNo As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ParameterNo)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ParameterNo, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ParameterNo)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Parameter
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Parameter As System.String
			Get
				Return MyBase.GetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Parameter)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Parameter, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Parameter)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Address
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Address As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Address)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Address, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Address)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Data_Type
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DataType As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.DataType)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.DataType, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.DataType)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Units
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Units As System.String
			Get
				Return MyBase.GetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Units)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Units, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Units)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Format
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Format As System.String
			Get
				Return MyBase.GetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Format)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Format, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Format)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Card_Number
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CardNumber As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.CardNumber)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.CardNumber, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.CardNumber)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Core_Number
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CoreNumber As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.CoreNumber)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.CoreNumber, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.CoreNumber)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Channel_Number
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelNumber As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ChannelNumber)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ChannelNumber, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ChannelNumber)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Slope
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Slope As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Slope)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Slope, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Slope)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Offset
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Offset As Nullable(Of System.Single)
			Get
				Return MyBase.GetSystemSingle(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Offset)
			End Get
			
			Set(ByVal value As Nullable(Of System.Single))
				If MyBase.SetSystemSingle(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Offset, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Offset)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Logger_Enabled
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LoggerEnabled As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LoggerEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LoggerEnabled, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LoggerEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Logger_Index
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LoggerIndex As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LoggerIndex)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LoggerIndex, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LoggerIndex)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Log_Report_Categorie
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LogReportCategorie As System.String
			Get
				Return MyBase.GetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LogReportCategorie)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LogReportCategorie, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LogReportCategorie)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_OmniBusBox_IOMGR_Uart.Log_Report_Order_In_Categorie
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LogReportOrderInCategorie As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LogReportOrderInCategorie)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LogReportOrderInCategorie, value) Then
					OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LogReportOrderInCategorie)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToDACN1OmniBusBoxIOMGRByEngineModel As DACN1OmniBusBoxIOMGR
		
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
												
						Case "PwcPn"
							Me.str().PwcPn = CType(value, string)
												
						Case "Name"
							Me.str().Name = CType(value, string)
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "Source"
							Me.str().Source = CType(value, string)
												
						Case "ArincChannel"
							Me.str().ArincChannel = CType(value, string)
												
						Case "ParameterNo"
							Me.str().ParameterNo = CType(value, string)
												
						Case "Parameter"
							Me.str().Parameter = CType(value, string)
												
						Case "Address"
							Me.str().Address = CType(value, string)
												
						Case "DataType"
							Me.str().DataType = CType(value, string)
												
						Case "Units"
							Me.str().Units = CType(value, string)
												
						Case "Format"
							Me.str().Format = CType(value, string)
												
						Case "CardNumber"
							Me.str().CardNumber = CType(value, string)
												
						Case "CoreNumber"
							Me.str().CoreNumber = CType(value, string)
												
						Case "ChannelNumber"
							Me.str().ChannelNumber = CType(value, string)
												
						Case "Slope"
							Me.str().Slope = CType(value, string)
												
						Case "Offset"
							Me.str().Offset = CType(value, string)
												
						Case "LoggerEnabled"
							Me.str().LoggerEnabled = CType(value, string)
												
						Case "LoggerIndex"
							Me.str().LoggerIndex = CType(value, string)
												
						Case "LogReportCategorie"
							Me.str().LogReportCategorie = CType(value, string)
												
						Case "LogReportOrderInCategorie"
							Me.str().LogReportOrderInCategorie = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Id)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "ArincChannel"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.ArincChannel = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ArincChannel)
							End If
						
						Case "ParameterNo"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.ParameterNo = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ParameterNo)
							End If
						
						Case "Address"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Address = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Address)
							End If
						
						Case "DataType"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.DataType = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.DataType)
							End If
						
						Case "CardNumber"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.CardNumber = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.CardNumber)
							End If
						
						Case "CoreNumber"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.CoreNumber = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.CoreNumber)
							End If
						
						Case "ChannelNumber"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.ChannelNumber = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ChannelNumber)
							End If
						
						Case "Slope"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.Slope = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Slope)
							End If
						
						Case "Offset"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
								Me.Offset = CType(value, Nullable(Of System.Single))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Offset)
							End If
						
						Case "LoggerEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.LoggerEnabled = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LoggerEnabled)
							End If
						
						Case "LoggerIndex"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LoggerIndex = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LoggerIndex)
							End If
						
						Case "LogReportOrderInCategorie"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LogReportOrderInCategorie = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LogReportOrderInCategorie)
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
		
			Public Sub New(ByVal entity As esDACN1OmniBusBoxIOMGRUart)
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
		  	
			Public Property ArincChannel As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.ArincChannel
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ArincChannel = Nothing
					Else
						entity.ArincChannel = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property ParameterNo As System.String 
				Get
					Dim data_ As Nullable(Of System.Int16) = entity.ParameterNo
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ParameterNo = Nothing
					Else
						entity.ParameterNo = Convert.ToInt16(Value)
					End If
				End Set
			End Property
		  	
			Public Property Parameter As System.String 
				Get
					Dim data_ As System.String = entity.Parameter
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Parameter = Nothing
					Else
						entity.Parameter = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Address As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Address
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Address = Nothing
					Else
						entity.Address = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property DataType As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.DataType
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DataType = Nothing
					Else
						entity.DataType = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property Units As System.String 
				Get
					Dim data_ As System.String = entity.Units
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Units = Nothing
					Else
						entity.Units = Convert.ToString(Value)
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
		  	
			Public Property CardNumber As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.CardNumber
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CardNumber = Nothing
					Else
						entity.CardNumber = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property CoreNumber As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.CoreNumber
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CoreNumber = Nothing
					Else
						entity.CoreNumber = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property ChannelNumber As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.ChannelNumber
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ChannelNumber = Nothing
					Else
						entity.ChannelNumber = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property Slope As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.Slope
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Slope = Nothing
					Else
						entity.Slope = Convert.ToSingle(Value)
					End If
				End Set
			End Property
		  	
			Public Property Offset As System.String 
				Get
					Dim data_ As Nullable(Of System.Single) = entity.Offset
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Offset = Nothing
					Else
						entity.Offset = Convert.ToSingle(Value)
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
		  

			Private entity As esDACN1OmniBusBoxIOMGRUart
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DACN1OmniBusBoxIOMGRUartMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As DACN1OmniBusBoxIOMGRUartQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DACN1OmniBusBoxIOMGRUartQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As DACN1OmniBusBoxIOMGRUartQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As DACN1OmniBusBoxIOMGRUartQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As DACN1OmniBusBoxIOMGRUartQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDACN1OmniBusBoxIOMGRUartCollection
		Inherits esEntityCollection(Of DACN1OmniBusBoxIOMGRUart)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DACN1OmniBusBoxIOMGRUartMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "DACN1OmniBusBoxIOMGRUartCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As DACN1OmniBusBoxIOMGRUartQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DACN1OmniBusBoxIOMGRUartQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As DACN1OmniBusBoxIOMGRUartQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New DACN1OmniBusBoxIOMGRUartQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As DACN1OmniBusBoxIOMGRUartQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, DACN1OmniBusBoxIOMGRUartQuery))
		End Sub
		
		#End Region
						
		Private m_query As DACN1OmniBusBoxIOMGRUartQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDACN1OmniBusBoxIOMGRUartQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return DACN1OmniBusBoxIOMGRUartMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "EngineModel" 
					Return Me.EngineModel
				Case "PwcPn" 
					Return Me.PwcPn
				Case "Name" 
					Return Me.Name
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "Source" 
					Return Me.Source
				Case "ArincChannel" 
					Return Me.ArincChannel
				Case "ParameterNo" 
					Return Me.ParameterNo
				Case "Parameter" 
					Return Me.Parameter
				Case "Address" 
					Return Me.Address
				Case "DataType" 
					Return Me.DataType
				Case "Units" 
					Return Me.Units
				Case "Format" 
					Return Me.Format
				Case "CardNumber" 
					Return Me.CardNumber
				Case "CoreNumber" 
					Return Me.CoreNumber
				Case "ChannelNumber" 
					Return Me.ChannelNumber
				Case "Slope" 
					Return Me.Slope
				Case "Offset" 
					Return Me.Offset
				Case "LoggerEnabled" 
					Return Me.LoggerEnabled
				Case "LoggerIndex" 
					Return Me.LoggerIndex
				Case "LogReportCategorie" 
					Return Me.LogReportCategorie
				Case "LogReportOrderInCategorie" 
					Return Me.LogReportOrderInCategorie
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property PwcPn As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.PwcPn, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Name As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Source As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Source, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ArincChannel As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ArincChannel, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property ParameterNo As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ParameterNo, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property Parameter As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Parameter, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Address As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Address, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property DataType As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.DataType, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property Units As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Units, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Format As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Format, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CardNumber As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.CardNumber, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property CoreNumber As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.CoreNumber, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelNumber As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ChannelNumber, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property Slope As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Slope, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property Offset As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Offset, esSystemType.Single)
			End Get
		End Property 
		
		Public ReadOnly Property LoggerEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LoggerEnabled, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property LoggerIndex As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LoggerIndex, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property LogReportCategorie As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LogReportCategorie, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LogReportOrderInCategorie As esQueryItem
			Get
				Return New esQueryItem(Me, DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LogReportOrderInCategorie, esSystemType.Int32)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class DACN1OmniBusBoxIOMGRUart 
		Inherits esDACN1OmniBusBoxIOMGRUart
		
	
		#Region "UpToDACN1OmniBusBoxIOMGRByEngineModel - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - DACN1_OmniBusBox_IOMGRDACN1_OmniBusBox_IOMGR_Uart
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToDACN1OmniBusBoxIOMGRByEngineModel As DACN1OmniBusBoxIOMGR
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToDACN1OmniBusBoxIOMGRByEngineModel Is Nothing _
						 AndAlso Not EngineModel.Equals(Nothing)  AndAlso Not PwcPn.Equals(Nothing)  Then
						
					Me._UpToDACN1OmniBusBoxIOMGRByEngineModel = New DACN1OmniBusBoxIOMGR()
					Me._UpToDACN1OmniBusBoxIOMGRByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToDACN1OmniBusBoxIOMGRByEngineModel", Me._UpToDACN1OmniBusBoxIOMGRByEngineModel)
					Me._UpToDACN1OmniBusBoxIOMGRByEngineModel.Query.Where(Me._UpToDACN1OmniBusBoxIOMGRByEngineModel.Query.EngineModel = Me.EngineModel)
					Me._UpToDACN1OmniBusBoxIOMGRByEngineModel.Query.Where(Me._UpToDACN1OmniBusBoxIOMGRByEngineModel.Query.PwcPn = Me.PwcPn)
					Me._UpToDACN1OmniBusBoxIOMGRByEngineModel.Query.Load()
				End If

				Return Me._UpToDACN1OmniBusBoxIOMGRByEngineModel
			End Get
			
            Set(ByVal value As DACN1OmniBusBoxIOMGR)
				Me.RemovePreSave("UpToDACN1OmniBusBoxIOMGRByEngineModel")
				

				If value Is Nothing Then
				
					Me.EngineModel = Nothing
				
					Me.PwcPn = Nothing
				
					Me._UpToDACN1OmniBusBoxIOMGRByEngineModel = Nothing
				Else
				
					Me.EngineModel = value.EngineModel
					
					Me.PwcPn = value.PwcPn
					
					Me._UpToDACN1OmniBusBoxIOMGRByEngineModel = value
					Me.SetPreSave("UpToDACN1OmniBusBoxIOMGRByEngineModel", Me._UpToDACN1OmniBusBoxIOMGRByEngineModel)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class DACN1OmniBusBoxIOMGRUartMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Id
			c.IsAutoIncrement = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.EngineModel
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.PwcPn, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.PwcPn
			c.CharacterMaxLength = 255
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Name, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Name
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ExportToCsv, 4, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Source, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Source
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ArincChannel, 6, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ArincChannel
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ParameterNo, 7, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ParameterNo
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Parameter, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Parameter
			c.CharacterMaxLength = 25
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Address, 9, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Address
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.DataType, 10, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.DataType
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Units, 11, GetType(System.String), esSystemType.String)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Units
			c.CharacterMaxLength = 25
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Format, 12, GetType(System.String), esSystemType.String)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Format
			c.CharacterMaxLength = 5
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.CardNumber, 13, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.CardNumber
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.CoreNumber, 14, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.CoreNumber
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.ChannelNumber, 15, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.ChannelNumber
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Slope, 16, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Slope
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.Offset, 17, GetType(System.Single), esSystemType.Single)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.Offset
			c.NumericPrecision = 7
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LoggerEnabled, 18, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LoggerEnabled
			c.CharacterMaxLength = 2
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LoggerIndex, 19, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LoggerIndex
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LogReportCategorie, 20, GetType(System.String), esSystemType.String)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LogReportCategorie
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DACN1OmniBusBoxIOMGRUartMetadata.ColumnNames.LogReportOrderInCategorie, 21, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DACN1OmniBusBoxIOMGRUartMetadata.PropertyNames.LogReportOrderInCategorie
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As DACN1OmniBusBoxIOMGRUartMetadata
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
			 Public Const EngineModel As String = "EngineModel"
			 Public Const PwcPn As String = "PWC_PN"
			 Public Const Name As String = "Name"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const Source As String = "Source"
			 Public Const ArincChannel As String = "Arinc_Channel"
			 Public Const ParameterNo As String = "Parameter_No"
			 Public Const Parameter As String = "Parameter"
			 Public Const Address As String = "Address"
			 Public Const DataType As String = "Data_Type"
			 Public Const Units As String = "Units"
			 Public Const Format As String = "Format"
			 Public Const CardNumber As String = "Card_Number"
			 Public Const CoreNumber As String = "Core_Number"
			 Public Const ChannelNumber As String = "Channel_Number"
			 Public Const Slope As String = "Slope"
			 Public Const Offset As String = "Offset"
			 Public Const LoggerEnabled As String = "Logger_Enabled"
			 Public Const LoggerIndex As String = "Logger_Index"
			 Public Const LogReportCategorie As String = "Log_Report_Categorie"
			 Public Const LogReportOrderInCategorie As String = "Log_Report_Order_In_Categorie"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const PwcPn As String = "PwcPn"
			 Public Const Name As String = "Name"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const Source As String = "Source"
			 Public Const ArincChannel As String = "ArincChannel"
			 Public Const ParameterNo As String = "ParameterNo"
			 Public Const Parameter As String = "Parameter"
			 Public Const Address As String = "Address"
			 Public Const DataType As String = "DataType"
			 Public Const Units As String = "Units"
			 Public Const Format As String = "Format"
			 Public Const CardNumber As String = "CardNumber"
			 Public Const CoreNumber As String = "CoreNumber"
			 Public Const ChannelNumber As String = "ChannelNumber"
			 Public Const Slope As String = "Slope"
			 Public Const Offset As String = "Offset"
			 Public Const LoggerEnabled As String = "LoggerEnabled"
			 Public Const LoggerIndex As String = "LoggerIndex"
			 Public Const LogReportCategorie As String = "LogReportCategorie"
			 Public Const LogReportOrderInCategorie As String = "LogReportOrderInCategorie"
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
			SyncLock GetType(DACN1OmniBusBoxIOMGRUartMetadata)
			
				If DACN1OmniBusBoxIOMGRUartMetadata.mapDelegates Is Nothing Then
					DACN1OmniBusBoxIOMGRUartMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If DACN1OmniBusBoxIOMGRUartMetadata._meta Is Nothing Then
					DACN1OmniBusBoxIOMGRUartMetadata._meta = New DACN1OmniBusBoxIOMGRUartMetadata()
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
				meta.AddTypeMap("PwcPn", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Source", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ArincChannel", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("ParameterNo", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("Parameter", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Address", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("DataType", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("Units", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Format", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CardNumber", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("CoreNumber", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("ChannelNumber", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("Slope", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("Offset", new esTypeMap("Single", "System.Single"))
				meta.AddTypeMap("LoggerEnabled", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("LoggerIndex", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("LogReportCategorie", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LogReportOrderInCategorie", new esTypeMap("Long", "System.Int32"))			
				
				
				 
				meta.Source = "DACN1_OmniBusBox_IOMGR_Uart"
				meta.Destination = "DACN1_OmniBusBox_IOMGR_Uart"
				
				meta.spInsert = "proc_DACN1_OmniBusBox_IOMGR_UartInsert"
				meta.spUpdate = "proc_DACN1_OmniBusBox_IOMGR_UartUpdate"
				meta.spDelete = "proc_DACN1_OmniBusBox_IOMGR_UartDelete"
				meta.spLoadAll = "proc_DACN1_OmniBusBox_IOMGR_UartLoadAll"
				meta.spLoadByPrimaryKey = "proc_DACN1_OmniBusBox_IOMGR_UartLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As DACN1OmniBusBoxIOMGRUartMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

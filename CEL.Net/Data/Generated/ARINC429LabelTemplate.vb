
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 5/23/2013 3:55:40 PM
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
	' Encapsulates the 'ARINC429LabelTemplate' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(ARINC429LabelTemplate))> _
	<XmlType("ARINC429LabelTemplate")> _	
	Partial Public Class ARINC429LabelTemplate 
		Inherits esARINC429LabelTemplate
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New ARINC429LabelTemplate()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal engineModel As System.String, ByVal name As System.String)
			Dim obj As New ARINC429LabelTemplate()
			obj.EngineModel = engineModel
			obj.Name = name
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal engineModel As System.String, ByVal name As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New ARINC429LabelTemplate()
			obj.EngineModel = engineModel
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
	<XmlType("ARINC429LabelTemplateCollection")> _
	Partial Public Class ARINC429LabelTemplateCollection
		Inherits esARINC429LabelTemplateCollection
		Implements IEnumerable(Of ARINC429LabelTemplate)
	
		Public Function FindByPrimaryKey(ByVal engineModel As System.String, ByVal name As System.String) As ARINC429LabelTemplate
			Return MyBase.SingleOrDefault(Function(e) e.EngineModel = engineModel And e.Name = name)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(ARINC429LabelTemplate))> _
		Public Class ARINC429LabelTemplateCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of ARINC429LabelTemplateCollection)
			
			Public Shared Widening Operator CType(packet As ARINC429LabelTemplateCollectionWCFPacket) As ARINC429LabelTemplateCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As ARINC429LabelTemplateCollection) As ARINC429LabelTemplateCollectionWCFPacket
				Return New ARINC429LabelTemplateCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class ARINC429LabelTemplateQuery 
		Inherits esARINC429LabelTemplateQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "ARINC429LabelTemplateQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As ARINC429LabelTemplateQuery) As String
			Return ARINC429LabelTemplateQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As ARINC429LabelTemplateQuery
			Return DirectCast(ARINC429LabelTemplateQuery.SerializeHelper.FromXml(query, GetType(ARINC429LabelTemplateQuery)), ARINC429LabelTemplateQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esARINC429LabelTemplate
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal engineModel As System.String, ByVal name As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineModel, name)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineModel, name)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal engineModel As System.String, ByVal name As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(engineModel, name)
			Else
				Return LoadByPrimaryKeyStoredProcedure(engineModel, name)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal engineModel As System.String, ByVal name As System.String) As Boolean
		
			Dim query As New ARINC429LabelTemplateQuery()
			query.Where(query.EngineModel = engineModel And query.Name = name)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal engineModel As System.String, ByVal name As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("EngineModel", engineModel)
						parms.Add("Name", name)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to ARINC429LabelTemplate.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(ARINC429LabelTemplateMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(ARINC429LabelTemplateMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.EngineModel, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Name As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.Name, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(ARINC429LabelTemplateMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(ARINC429LabelTemplateMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.Parameter
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Parameter As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.Parameter)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.Parameter, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Parameter)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' Arinc Channel A/B
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ArincChannel As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.ArincChannel)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.ArincChannel, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.ArincChannel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.Units
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Units As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.Units)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.Units, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Units)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.DisplayFormat
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DisplayFormat As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.DisplayFormat)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.DisplayFormat, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.DisplayFormat)
				End If
			End Set
		End Property	
			
		' <summary>
		' Omnibus Box Number
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CardNumber As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.CardNumber)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.CardNumber, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.CardNumber)
				End If
			End Set
		End Property	
			
		' <summary>
		' Card Number in OmnibusBox
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CoreNumber As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.CoreNumber)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.CoreNumber, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.CoreNumber)
				End If
			End Set
		End Property	
			
		' <summary>
		' 0-1-2-3 = RX A429; 4-5-6-7 = UART TX/RX; 8-9-10-11 = TX A429
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ChannelNumber As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.ChannelNumber)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.ChannelNumber, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.ChannelNumber)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.TX/RX
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TxRx As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.TxRx)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.TxRx, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.TxRx)
				End If
			End Set
		End Property	
			
		' <summary>
		' Octal Arinc Label
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Label As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.Label)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.Label, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Label)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.SDI
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sdi As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.Sdi)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.Sdi, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Sdi)
				End If
			End Set
		End Property	
			
		' <summary>
		' Status Matrix (SM)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StatusMatrix As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.StatusMatrix)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.StatusMatrix, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.StatusMatrix)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.DefaultValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DefaultValue As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.DefaultValue)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.DefaultValue, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.DefaultValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.Format
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Format As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.Format)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.Format, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Format)
				End If
			End Set
		End Property	
			
		' <summary>
		' Scaling Factor
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ScalingFactor As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(ARINC429LabelTemplateMetadata.ColumnNames.ScalingFactor)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(ARINC429LabelTemplateMetadata.ColumnNames.ScalingFactor, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.ScalingFactor)
				End If
			End Set
		End Property	
			
		' <summary>
		' Number Of Significant (Bits for BNR, Digits for BCD)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NbSigBitsOrDigits As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.NbSigBitsOrDigits)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429LabelTemplateMetadata.ColumnNames.NbSigBitsOrDigits, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.NbSigBitsOrDigits)
				End If
			End Set
		End Property	
			
		' <summary>
		' Minimum Transmit Interval (mSec)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MinInterval As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(ARINC429LabelTemplateMetadata.ColumnNames.MinInterval)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(ARINC429LabelTemplateMetadata.ColumnNames.MinInterval, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.MinInterval)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maximum Transmit Interval (mSec)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MaxInterval As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(ARINC429LabelTemplateMetadata.ColumnNames.MaxInterval)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(ARINC429LabelTemplateMetadata.ColumnNames.MaxInterval, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.MaxInterval)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.Logger_Enabled
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LoggerEnabled As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(ARINC429LabelTemplateMetadata.ColumnNames.LoggerEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(ARINC429LabelTemplateMetadata.ColumnNames.LoggerEnabled, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.LoggerEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.Logger_Index
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LoggerIndex As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(ARINC429LabelTemplateMetadata.ColumnNames.LoggerIndex)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(ARINC429LabelTemplateMetadata.ColumnNames.LoggerIndex, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.LoggerIndex)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.Log_Report_Categorie
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LogReportCategorie As System.String
			Get
				Return MyBase.GetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.LogReportCategorie)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429LabelTemplateMetadata.ColumnNames.LogReportCategorie, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.LogReportCategorie)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429LabelTemplate.Log_Report_Order_In_Categorie
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LogReportOrderInCategorie As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(ARINC429LabelTemplateMetadata.ColumnNames.LogReportOrderInCategorie)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(ARINC429LabelTemplateMetadata.ColumnNames.LogReportOrderInCategorie, value) Then
					OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.LogReportOrderInCategorie)
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
												
						Case "Name"
							Me.str().Name = CType(value, string)
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "Parameter"
							Me.str().Parameter = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "ArincChannel"
							Me.str().ArincChannel = CType(value, string)
												
						Case "Units"
							Me.str().Units = CType(value, string)
												
						Case "DisplayFormat"
							Me.str().DisplayFormat = CType(value, string)
												
						Case "CardNumber"
							Me.str().CardNumber = CType(value, string)
												
						Case "CoreNumber"
							Me.str().CoreNumber = CType(value, string)
												
						Case "ChannelNumber"
							Me.str().ChannelNumber = CType(value, string)
												
						Case "TxRx"
							Me.str().TxRx = CType(value, string)
												
						Case "Label"
							Me.str().Label = CType(value, string)
												
						Case "Sdi"
							Me.str().Sdi = CType(value, string)
												
						Case "StatusMatrix"
							Me.str().StatusMatrix = CType(value, string)
												
						Case "DefaultValue"
							Me.str().DefaultValue = CType(value, string)
												
						Case "Format"
							Me.str().Format = CType(value, string)
												
						Case "ScalingFactor"
							Me.str().ScalingFactor = CType(value, string)
												
						Case "NbSigBitsOrDigits"
							Me.str().NbSigBitsOrDigits = CType(value, string)
												
						Case "MinInterval"
							Me.str().MinInterval = CType(value, string)
												
						Case "MaxInterval"
							Me.str().MaxInterval = CType(value, string)
												
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
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Id)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "ArincChannel"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.ArincChannel = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.ArincChannel)
							End If
						
						Case "CardNumber"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.CardNumber = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.CardNumber)
							End If
						
						Case "CoreNumber"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.CoreNumber = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.CoreNumber)
							End If
						
						Case "ChannelNumber"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.ChannelNumber = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.ChannelNumber)
							End If
						
						Case "TxRx"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.TxRx = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.TxRx)
							End If
						
						Case "Sdi"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.Sdi = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Sdi)
							End If
						
						Case "StatusMatrix"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.StatusMatrix = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.StatusMatrix)
							End If
						
						Case "Format"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.Format = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.Format)
							End If
						
						Case "ScalingFactor"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ScalingFactor = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.ScalingFactor)
							End If
						
						Case "NbSigBitsOrDigits"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.NbSigBitsOrDigits = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.NbSigBitsOrDigits)
							End If
						
						Case "MinInterval"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.MinInterval = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.MinInterval)
							End If
						
						Case "MaxInterval"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.MaxInterval = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.MaxInterval)
							End If
						
						Case "LoggerEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.LoggerEnabled = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.LoggerEnabled)
							End If
						
						Case "LoggerIndex"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LoggerIndex = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.LoggerIndex)
							End If
						
						Case "LogReportOrderInCategorie"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LogReportOrderInCategorie = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(ARINC429LabelTemplateMetadata.PropertyNames.LogReportOrderInCategorie)
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
		
			Public Sub New(ByVal entity As esARINC429LabelTemplate)
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
		  	
			Public Property Label As System.String 
				Get
					Dim data_ As System.String = entity.Label
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Label = Nothing
					Else
						entity.Label = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sdi As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.Sdi
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sdi = Nothing
					Else
						entity.Sdi = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property StatusMatrix As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.StatusMatrix
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.StatusMatrix = Nothing
					Else
						entity.StatusMatrix = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property DefaultValue As System.String 
				Get
					Dim data_ As System.String = entity.DefaultValue
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DefaultValue = Nothing
					Else
						entity.DefaultValue = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Format As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.Format
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Format = Nothing
					Else
						entity.Format = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property ScalingFactor As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.ScalingFactor
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ScalingFactor = Nothing
					Else
						entity.ScalingFactor = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property NbSigBitsOrDigits As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.NbSigBitsOrDigits
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NbSigBitsOrDigits = Nothing
					Else
						entity.NbSigBitsOrDigits = Convert.ToByte(Value)
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
		  

			Private entity As esARINC429LabelTemplate
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return ARINC429LabelTemplateMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As ARINC429LabelTemplateQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New ARINC429LabelTemplateQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As ARINC429LabelTemplateQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As ARINC429LabelTemplateQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As ARINC429LabelTemplateQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esARINC429LabelTemplateCollection
		Inherits esEntityCollection(Of ARINC429LabelTemplate)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return ARINC429LabelTemplateMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "ARINC429LabelTemplateCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As ARINC429LabelTemplateQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New ARINC429LabelTemplateQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As ARINC429LabelTemplateQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New ARINC429LabelTemplateQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As ARINC429LabelTemplateQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, ARINC429LabelTemplateQuery))
		End Sub
		
		#End Region
						
		Private m_query As ARINC429LabelTemplateQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esARINC429LabelTemplateQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return ARINC429LabelTemplateMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "EngineModel" 
					Return Me.EngineModel
				Case "Name" 
					Return Me.Name
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "Parameter" 
					Return Me.Parameter
				Case "Description" 
					Return Me.Description
				Case "ArincChannel" 
					Return Me.ArincChannel
				Case "Units" 
					Return Me.Units
				Case "DisplayFormat" 
					Return Me.DisplayFormat
				Case "CardNumber" 
					Return Me.CardNumber
				Case "CoreNumber" 
					Return Me.CoreNumber
				Case "ChannelNumber" 
					Return Me.ChannelNumber
				Case "TxRx" 
					Return Me.TxRx
				Case "Label" 
					Return Me.Label
				Case "Sdi" 
					Return Me.Sdi
				Case "StatusMatrix" 
					Return Me.StatusMatrix
				Case "DefaultValue" 
					Return Me.DefaultValue
				Case "Format" 
					Return Me.Format
				Case "ScalingFactor" 
					Return Me.ScalingFactor
				Case "NbSigBitsOrDigits" 
					Return Me.NbSigBitsOrDigits
				Case "MinInterval" 
					Return Me.MinInterval
				Case "MaxInterval" 
					Return Me.MaxInterval
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
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Name As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Parameter As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.Parameter, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ArincChannel As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.ArincChannel, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property Units As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.Units, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DisplayFormat As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.DisplayFormat, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CardNumber As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.CardNumber, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property CoreNumber As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.CoreNumber, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property ChannelNumber As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.ChannelNumber, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property TxRx As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.TxRx, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property Label As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.Label, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sdi As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.Sdi, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property StatusMatrix As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.StatusMatrix, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property DefaultValue As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.DefaultValue, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Format As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.Format, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property ScalingFactor As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.ScalingFactor, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property NbSigBitsOrDigits As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.NbSigBitsOrDigits, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property MinInterval As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.MinInterval, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property MaxInterval As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.MaxInterval, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property LoggerEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.LoggerEnabled, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property LoggerIndex As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.LoggerIndex, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property LogReportCategorie As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.LogReportCategorie, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LogReportOrderInCategorie As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429LabelTemplateMetadata.ColumnNames.LogReportOrderInCategorie, esSystemType.Int32)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class ARINC429LabelTemplate 
		Inherits esARINC429LabelTemplate
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class ARINC429LabelTemplateMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.Id
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.Name, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.Name
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.ExportToCsv, 3, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.Parameter, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.Parameter
			c.CharacterMaxLength = 25
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.Description, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.Description
			c.CharacterMaxLength = 25
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.ArincChannel, 6, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.ArincChannel
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Arinc Channel A/B"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.Units, 7, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.Units
			c.CharacterMaxLength = 25
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.DisplayFormat, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.DisplayFormat
			c.CharacterMaxLength = 3
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.CardNumber, 9, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.CardNumber
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Omnibus Box Number"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.CoreNumber, 10, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.CoreNumber
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Card Number in OmnibusBox"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.ChannelNumber, 11, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.ChannelNumber
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "0-1-2-3 = RX A429; 4-5-6-7 = UART TX/RX; 8-9-10-11 = TX A429"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.TxRx, 12, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.TxRx
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.Label, 13, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.Label
			c.CharacterMaxLength = 4
			c.Description = "Octal Arinc Label"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.Sdi, 14, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.Sdi
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.StatusMatrix, 15, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.StatusMatrix
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "4"
			c.Description = "Status Matrix (SM)"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.DefaultValue, 16, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.DefaultValue
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.Format, 17, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.Format
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.ScalingFactor, 18, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.ScalingFactor
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Scaling Factor"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.NbSigBitsOrDigits, 19, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.NbSigBitsOrDigits
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Number Of Significant (Bits for BNR, Digits for BCD)"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.MinInterval, 20, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.MinInterval
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Minimum Transmit Interval (mSec)"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.MaxInterval, 21, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.MaxInterval
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Maximum Transmit Interval (mSec)"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.LoggerEnabled, 22, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.LoggerEnabled
			c.CharacterMaxLength = 2
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.LoggerIndex, 23, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.LoggerIndex
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.LogReportCategorie, 24, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.LogReportCategorie
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429LabelTemplateMetadata.ColumnNames.LogReportOrderInCategorie, 25, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = ARINC429LabelTemplateMetadata.PropertyNames.LogReportOrderInCategorie
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As ARINC429LabelTemplateMetadata
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
			 Public Const Name As String = "Name"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const Parameter As String = "Parameter"
			 Public Const Description As String = "Description"
			 Public Const ArincChannel As String = "Arinc_Channel"
			 Public Const Units As String = "Units"
			 Public Const DisplayFormat As String = "DisplayFormat"
			 Public Const CardNumber As String = "Card_Number"
			 Public Const CoreNumber As String = "Core_Number"
			 Public Const ChannelNumber As String = "Channel_Number"
			 Public Const TxRx As String = "TX/RX"
			 Public Const Label As String = "Label"
			 Public Const Sdi As String = "SDI"
			 Public Const StatusMatrix As String = "StatusMatrix"
			 Public Const DefaultValue As String = "DefaultValue"
			 Public Const Format As String = "Format"
			 Public Const ScalingFactor As String = "Scaling_Factor"
			 Public Const NbSigBitsOrDigits As String = "Nb_Sig_Bits_or_Digits"
			 Public Const MinInterval As String = "Min_Interval"
			 Public Const MaxInterval As String = "Max_Interval"
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
			 Public Const Name As String = "Name"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const Parameter As String = "Parameter"
			 Public Const Description As String = "Description"
			 Public Const ArincChannel As String = "ArincChannel"
			 Public Const Units As String = "Units"
			 Public Const DisplayFormat As String = "DisplayFormat"
			 Public Const CardNumber As String = "CardNumber"
			 Public Const CoreNumber As String = "CoreNumber"
			 Public Const ChannelNumber As String = "ChannelNumber"
			 Public Const TxRx As String = "TxRx"
			 Public Const Label As String = "Label"
			 Public Const Sdi As String = "Sdi"
			 Public Const StatusMatrix As String = "StatusMatrix"
			 Public Const DefaultValue As String = "DefaultValue"
			 Public Const Format As String = "Format"
			 Public Const ScalingFactor As String = "ScalingFactor"
			 Public Const NbSigBitsOrDigits As String = "NbSigBitsOrDigits"
			 Public Const MinInterval As String = "MinInterval"
			 Public Const MaxInterval As String = "MaxInterval"
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
			SyncLock GetType(ARINC429LabelTemplateMetadata)
			
				If ARINC429LabelTemplateMetadata.mapDelegates Is Nothing Then
					ARINC429LabelTemplateMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If ARINC429LabelTemplateMetadata._meta Is Nothing Then
					ARINC429LabelTemplateMetadata._meta = New ARINC429LabelTemplateMetadata()
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
				meta.AddTypeMap("Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Parameter", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ArincChannel", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("Units", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DisplayFormat", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CardNumber", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("CoreNumber", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("ChannelNumber", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("TxRx", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("Label", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sdi", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("StatusMatrix", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("DefaultValue", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Format", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("ScalingFactor", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("NbSigBitsOrDigits", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("MinInterval", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("MaxInterval", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("LoggerEnabled", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("LoggerIndex", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("LogReportCategorie", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LogReportOrderInCategorie", new esTypeMap("Long", "System.Int32"))			
				
				
				 
				meta.Source = "ARINC429LabelTemplate"
				meta.Destination = "ARINC429LabelTemplate"
				
				meta.spInsert = "proc_ARINC429LabelTemplateInsert"
				meta.spUpdate = "proc_ARINC429LabelTemplateUpdate"
				meta.spDelete = "proc_ARINC429LabelTemplateDelete"
				meta.spLoadAll = "proc_ARINC429LabelTemplateLoadAll"
				meta.spLoadByPrimaryKey = "proc_ARINC429LabelTemplateLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As ARINC429LabelTemplateMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

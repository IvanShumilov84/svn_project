
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 31/01/2014 1:53:58 PM
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
	' Encapsulates the 'DACN1_AFDX_SIGNAL' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(Dacn1AfdxSignal))> _
	<XmlType("Dacn1AfdxSignal")> _	
	Partial Public Class Dacn1AfdxSignal 
		Inherits esDacn1AfdxSignal
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New Dacn1AfdxSignal()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String)
			Dim obj As New Dacn1AfdxSignal()
			obj.CollectionName = collectionName
			obj.Vlid = vlid
			obj.UdpPortSource = udpPortSource
			obj.UdpPortDestination = udpPortDestination
			obj.FdsName = fdsName
			obj.ParameterName = parameterName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New Dacn1AfdxSignal()
			obj.CollectionName = collectionName
			obj.Vlid = vlid
			obj.UdpPortSource = udpPortSource
			obj.UdpPortDestination = udpPortDestination
			obj.FdsName = fdsName
			obj.ParameterName = parameterName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("Dacn1AfdxSignalCollection")> _
	Partial Public Class Dacn1AfdxSignalCollection
		Inherits esDacn1AfdxSignalCollection
		Implements IEnumerable(Of Dacn1AfdxSignal)
	
		Public Function FindByPrimaryKey(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String) As Dacn1AfdxSignal
			Return MyBase.SingleOrDefault(Function(e) e.CollectionName = collectionName And e.Vlid.HasValue AndAlso e.Vlid.Value = vlid And e.UdpPortSource.HasValue AndAlso e.UdpPortSource.Value = udpPortSource And e.UdpPortDestination.HasValue AndAlso e.UdpPortDestination.Value = udpPortDestination And e.FdsName = fdsName And e.ParameterName = parameterName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(Dacn1AfdxSignal))> _
		Public Class Dacn1AfdxSignalCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of Dacn1AfdxSignalCollection)
			
			Public Shared Widening Operator CType(packet As Dacn1AfdxSignalCollectionWCFPacket) As Dacn1AfdxSignalCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As Dacn1AfdxSignalCollection) As Dacn1AfdxSignalCollectionWCFPacket
				Return New Dacn1AfdxSignalCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class Dacn1AfdxSignalQuery 
		Inherits esDacn1AfdxSignalQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "Dacn1AfdxSignalQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As Dacn1AfdxSignalQuery) As String
			Return Dacn1AfdxSignalQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As Dacn1AfdxSignalQuery
			Return DirectCast(Dacn1AfdxSignalQuery.SerializeHelper.FromXml(query, GetType(Dacn1AfdxSignalQuery)), Dacn1AfdxSignalQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDacn1AfdxSignal
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, vlid, udpPortSource, udpPortDestination, fdsName, parameterName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, vlid, udpPortSource, udpPortDestination, fdsName, parameterName)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, vlid, udpPortSource, udpPortDestination, fdsName, parameterName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, vlid, udpPortSource, udpPortDestination, fdsName, parameterName)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String) As Boolean
		
			Dim query As New Dacn1AfdxSignalQuery()
			query.Where(query.CollectionName = collectionName And query.Vlid = vlid And query.UdpPortSource = udpPortSource And query.UdpPortDestination = udpPortDestination And query.FdsName = fdsName And query.ParameterName = parameterName)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("CollectionName", collectionName)
						parms.Add("Vlid", vlid)
						parms.Add("UdpPortSource", udpPortSource)
						parms.Add("UdpPortDestination", udpPortDestination)
						parms.Add("FdsName", fdsName)
						parms.Add("ParameterName", parameterName)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CollectionName As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxSignalMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxSignalMetadata.ColumnNames.CollectionName, value) Then
					Me._UpToDacn1AfdxMessageByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxMessageByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.VLID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Vlid As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.Vlid)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.Vlid, value) Then
					Me._UpToDacn1AfdxMessageByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxMessageByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.Vlid)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.UdpPortSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UdpPortSource As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.UdpPortSource)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.UdpPortSource, value) Then
					Me._UpToDacn1AfdxMessageByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxMessageByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.UdpPortSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.UdpPortDestination
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UdpPortDestination As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.UdpPortDestination)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.UdpPortDestination, value) Then
					Me._UpToDacn1AfdxMessageByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxMessageByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.UdpPortDestination)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.FdsName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FdsName As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxSignalMetadata.ColumnNames.FdsName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxSignalMetadata.ColumnNames.FdsName, value) Then
					Me._UpToDacn1AfdxMessageByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxMessageByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.FdsName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.ParameterName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ParameterName As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxSignalMetadata.ColumnNames.ParameterName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxSignalMetadata.ColumnNames.ParameterName, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.ParameterName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1AfdxSignalMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1AfdxSignalMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Tx/Rx CE CHAMPS EST DUPLICATA De celui de FDS
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TxRx As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxSignalMetadata.ColumnNames.TxRx)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxSignalMetadata.ColumnNames.TxRx, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.TxRx)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.SignalNumberOfBit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SignalNumberOfBit As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.SignalNumberOfBit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.SignalNumberOfBit, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.SignalNumberOfBit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.SignalAddress
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SignalAddress As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.SignalAddress)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.SignalAddress, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.SignalAddress)
				End If
			End Set
		End Property	
			
		' <summary>
		' Bit Position dans le FDS a l'adresse FdsAddress voir table DACN1_AFDX_MESSAGE
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StartBit As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.StartBit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.StartBit, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.StartBit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Bit Position dans le FDS a l'adresse FdsAddress voir table DACN1_AFDX_MESSAGE
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StopBit As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.StopBit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.StopBit, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.StopBit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.LSBValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LSBValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(Dacn1AfdxSignalMetadata.ColumnNames.LSBValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(Dacn1AfdxSignalMetadata.ColumnNames.LSBValue, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.LSBValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' ParameterType or DataType is the same
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DataType As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.DataType)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.DataType, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.DataType)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.CodingType
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CodingType As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.CodingType)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.CodingType, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.CodingType)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.EngUnitID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngUnitID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.EngUnitID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.EngUnitID, value) Then
					Me._UpToEngineeringUnitByEngUnitID = Nothing
					Me.OnPropertyChanged("UpToEngineeringUnitByEngUnitID")
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.EngUnitID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.MinValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MinValue As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.MinValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.MinValue, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.MinValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.MaxValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MaxValue As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.MaxValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.MaxValue, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.MaxValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.Accuracy
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Accuracy As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.Accuracy)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.Accuracy, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.Accuracy)
				End If
			End Set
		End Property	
			
		' <summary>
		' String Format
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Format As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxSignalMetadata.ColumnNames.Format)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxSignalMetadata.ColumnNames.Format, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.Format)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.DisplayFormat
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DisplayFormat As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxSignalMetadata.ColumnNames.DisplayFormat)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxSignalMetadata.ColumnNames.DisplayFormat, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.DisplayFormat)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.DecimalDigits
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DecimalDigits As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.DecimalDigits)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.DecimalDigits, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.DecimalDigits)
				End If
			End Set
		End Property	
			
		' <summary>
		' String Length, Opagque Length
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Length As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.Length)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalMetadata.ColumnNames.Length, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.Length)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL.HideInGrid
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HideInGrid As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1AfdxSignalMetadata.ColumnNames.HideInGrid)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1AfdxSignalMetadata.ColumnNames.HideInGrid, value) Then
					OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.HideInGrid)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToDacn1AfdxMessageByCollectionName As Dacn1AfdxMessage
		
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
												
						Case "Vlid"
							Me.str().Vlid = CType(value, string)
												
						Case "UdpPortSource"
							Me.str().UdpPortSource = CType(value, string)
												
						Case "UdpPortDestination"
							Me.str().UdpPortDestination = CType(value, string)
												
						Case "FdsName"
							Me.str().FdsName = CType(value, string)
												
						Case "ParameterName"
							Me.str().ParameterName = CType(value, string)
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "TxRx"
							Me.str().TxRx = CType(value, string)
												
						Case "SignalNumberOfBit"
							Me.str().SignalNumberOfBit = CType(value, string)
												
						Case "SignalAddress"
							Me.str().SignalAddress = CType(value, string)
												
						Case "StartBit"
							Me.str().StartBit = CType(value, string)
												
						Case "StopBit"
							Me.str().StopBit = CType(value, string)
												
						Case "LSBValue"
							Me.str().LSBValue = CType(value, string)
												
						Case "DataType"
							Me.str().DataType = CType(value, string)
												
						Case "CodingType"
							Me.str().CodingType = CType(value, string)
												
						Case "EngUnitID"
							Me.str().EngUnitID = CType(value, string)
												
						Case "MinValue"
							Me.str().MinValue = CType(value, string)
												
						Case "MaxValue"
							Me.str().MaxValue = CType(value, string)
												
						Case "Accuracy"
							Me.str().Accuracy = CType(value, string)
												
						Case "Format"
							Me.str().Format = CType(value, string)
												
						Case "DisplayFormat"
							Me.str().DisplayFormat = CType(value, string)
												
						Case "DecimalDigits"
							Me.str().DecimalDigits = CType(value, string)
												
						Case "Length"
							Me.str().Length = CType(value, string)
												
						Case "HideInGrid"
							Me.str().HideInGrid = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Vlid"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Vlid = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.Vlid)
							End If
						
						Case "UdpPortSource"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.UdpPortSource = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.UdpPortSource)
							End If
						
						Case "UdpPortDestination"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.UdpPortDestination = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.UdpPortDestination)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "TxRx"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.TxRx = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.TxRx)
							End If
						
						Case "SignalNumberOfBit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SignalNumberOfBit = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.SignalNumberOfBit)
							End If
						
						Case "SignalAddress"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SignalAddress = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.SignalAddress)
							End If
						
						Case "StartBit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.StartBit = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.StartBit)
							End If
						
						Case "StopBit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.StopBit = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.StopBit)
							End If
						
						Case "LSBValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.LSBValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.LSBValue)
							End If
						
						Case "DataType"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.DataType = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.DataType)
							End If
						
						Case "CodingType"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.CodingType = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.CodingType)
							End If
						
						Case "EngUnitID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.EngUnitID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.EngUnitID)
							End If
						
						Case "MinValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.MinValue = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.MinValue)
							End If
						
						Case "MaxValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.MaxValue = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.MaxValue)
							End If
						
						Case "Accuracy"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Accuracy = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.Accuracy)
							End If
						
						Case "DecimalDigits"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.DecimalDigits = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.DecimalDigits)
							End If
						
						Case "Length"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Length = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.Length)
							End If
						
						Case "HideInGrid"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.HideInGrid = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1AfdxSignalMetadata.PropertyNames.HideInGrid)
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
		
			Public Sub New(ByVal entity As esDacn1AfdxSignal)
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
		  	
			Public Property Vlid As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Vlid
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Vlid = Nothing
					Else
						entity.Vlid = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property UdpPortSource As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.UdpPortSource
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.UdpPortSource = Nothing
					Else
						entity.UdpPortSource = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property UdpPortDestination As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.UdpPortDestination
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.UdpPortDestination = Nothing
					Else
						entity.UdpPortDestination = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property FdsName As System.String 
				Get
					Dim data_ As System.String = entity.FdsName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FdsName = Nothing
					Else
						entity.FdsName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ParameterName As System.String 
				Get
					Dim data_ As System.String = entity.ParameterName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ParameterName = Nothing
					Else
						entity.ParameterName = Convert.ToString(Value)
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
		  	
			Public Property SignalNumberOfBit As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SignalNumberOfBit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SignalNumberOfBit = Nothing
					Else
						entity.SignalNumberOfBit = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property SignalAddress As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SignalAddress
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SignalAddress = Nothing
					Else
						entity.SignalAddress = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property StartBit As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.StartBit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.StartBit = Nothing
					Else
						entity.StartBit = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property StopBit As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.StopBit
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.StopBit = Nothing
					Else
						entity.StopBit = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property LSBValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.LSBValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LSBValue = Nothing
					Else
						entity.LSBValue = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property DataType As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.DataType
					
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
						entity.DataType = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property CodingType As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.CodingType
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CodingType = Nothing
					Else
						entity.CodingType = Convert.ToInt32(Value)
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
		  	
			Public Property MinValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.MinValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MinValue = Nothing
					Else
						entity.MinValue = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MaxValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.MaxValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MaxValue = Nothing
					Else
						entity.MaxValue = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Accuracy As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Accuracy
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Accuracy = Nothing
					Else
						entity.Accuracy = Convert.ToInt32(Value)
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
		  	
			Public Property Length As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Length
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Length = Nothing
					Else
						entity.Length = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property HideInGrid As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.HideInGrid
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HideInGrid = Nothing
					Else
						entity.HideInGrid = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  

			Private entity As esDacn1AfdxSignal
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1AfdxSignalMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As Dacn1AfdxSignalQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1AfdxSignalQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As Dacn1AfdxSignalQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As Dacn1AfdxSignalQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As Dacn1AfdxSignalQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDacn1AfdxSignalCollection
		Inherits esEntityCollection(Of Dacn1AfdxSignal)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1AfdxSignalMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "Dacn1AfdxSignalCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As Dacn1AfdxSignalQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1AfdxSignalQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As Dacn1AfdxSignalQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New Dacn1AfdxSignalQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As Dacn1AfdxSignalQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, Dacn1AfdxSignalQuery))
		End Sub
		
		#End Region
						
		Private m_query As Dacn1AfdxSignalQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDacn1AfdxSignalQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return Dacn1AfdxSignalMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "CollectionName" 
					Return Me.CollectionName
				Case "Vlid" 
					Return Me.Vlid
				Case "UdpPortSource" 
					Return Me.UdpPortSource
				Case "UdpPortDestination" 
					Return Me.UdpPortDestination
				Case "FdsName" 
					Return Me.FdsName
				Case "ParameterName" 
					Return Me.ParameterName
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "TxRx" 
					Return Me.TxRx
				Case "SignalNumberOfBit" 
					Return Me.SignalNumberOfBit
				Case "SignalAddress" 
					Return Me.SignalAddress
				Case "StartBit" 
					Return Me.StartBit
				Case "StopBit" 
					Return Me.StopBit
				Case "LSBValue" 
					Return Me.LSBValue
				Case "DataType" 
					Return Me.DataType
				Case "CodingType" 
					Return Me.CodingType
				Case "EngUnitID" 
					Return Me.EngUnitID
				Case "MinValue" 
					Return Me.MinValue
				Case "MaxValue" 
					Return Me.MaxValue
				Case "Accuracy" 
					Return Me.Accuracy
				Case "Format" 
					Return Me.Format
				Case "DisplayFormat" 
					Return Me.DisplayFormat
				Case "DecimalDigits" 
					Return Me.DecimalDigits
				Case "Length" 
					Return Me.Length
				Case "HideInGrid" 
					Return Me.HideInGrid
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Vlid As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.Vlid, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property UdpPortSource As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.UdpPortSource, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property UdpPortDestination As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.UdpPortDestination, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property FdsName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.FdsName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ParameterName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.ParameterName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property TxRx As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.TxRx, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property SignalNumberOfBit As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.SignalNumberOfBit, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property SignalAddress As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.SignalAddress, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property StartBit As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.StartBit, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property StopBit As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.StopBit, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property LSBValue As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.LSBValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property DataType As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.DataType, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property CodingType As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.CodingType, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngUnitID As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.EngUnitID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MinValue As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.MinValue, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MaxValue As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.MaxValue, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Accuracy As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.Accuracy, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Format As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.Format, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DisplayFormat As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.DisplayFormat, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DecimalDigits As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.DecimalDigits, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Length As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.Length, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property HideInGrid As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalMetadata.ColumnNames.HideInGrid, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class Dacn1AfdxSignal 
		Inherits esDacn1AfdxSignal
		
	
		#Region "Dacn1AfdxSignalEnumerationCollectionByCollectionName - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_Dacn1AfdxSignalEnumerationCollectionByCollectionName() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Dacn1AfdxSignal.Dacn1AfdxSignalEnumerationCollectionByCollectionName_Delegate)
				map.PropertyName = "Dacn1AfdxSignalEnumerationCollectionByCollectionName"
				map.MyColumnName = "CollectionName,VLID,UdpPortSource,UdpPortDestination,FdsName,ParameterName"
				map.ParentColumnName = "CollectionName,VLID,UdpPortSource,UdpPortDestination,FdsName,ParameterName"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub Dacn1AfdxSignalEnumerationCollectionByCollectionName_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New Dacn1AfdxSignalQuery(data.NextAlias())
			
			Dim mee As Dacn1AfdxSignalEnumerationQuery = If(data.You IsNot Nothing, TryCast(data.You, Dacn1AfdxSignalEnumerationQuery), New Dacn1AfdxSignalEnumerationQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CollectionName = mee.CollectionName And parent.Vlid = mee.Vlid And parent.UdpPortSource = mee.UdpPortSource And parent.UdpPortDestination = mee.UdpPortDestination And parent.FdsName = mee.FdsName And parent.ParameterName = mee.ParameterName)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - DACN1_AFDX_SIGNALDACN1_AFDX_SIGNAL_ENUMERATION
		''' </summary>

		<XmlIgnore()> _ 
		Public Property Dacn1AfdxSignalEnumerationCollectionByCollectionName As Dacn1AfdxSignalEnumerationCollection 
		
			Get
				If Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName Is Nothing Then
					Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName = New Dacn1AfdxSignalEnumerationCollection()
					Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("Dacn1AfdxSignalEnumerationCollectionByCollectionName", Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName)
				
					If Not Me.CollectionName.Equals(Nothing) AndAlso Not Me.Vlid.Equals(Nothing) AndAlso Not Me.UdpPortSource.Equals(Nothing) AndAlso Not Me.UdpPortDestination.Equals(Nothing) AndAlso Not Me.FdsName.Equals(Nothing) AndAlso Not Me.ParameterName.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
							Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.Vlid = Me.Vlid)
							Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.UdpPortSource = Me.UdpPortSource)
							Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.UdpPortDestination = Me.UdpPortDestination)
							Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.FdsName = Me.FdsName)
							Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.ParameterName = Me.ParameterName)
							Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.fks.Add(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.CollectionName, Me.CollectionName)
						Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.fks.Add(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.Vlid, Me.Vlid)
						Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.fks.Add(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.UdpPortSource, Me.UdpPortSource)
						Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.fks.Add(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.UdpPortDestination, Me.UdpPortDestination)
						Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.fks.Add(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.FdsName, Me.FdsName)
						Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName.fks.Add(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.ParameterName, Me.ParameterName)
					End If
				End If

				Return Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName
			End Get
			
			Set(ByVal value As Dacn1AfdxSignalEnumerationCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName Is Nothing Then

					Me.RemovePostSave("Dacn1AfdxSignalEnumerationCollectionByCollectionName")
					Me._Dacn1AfdxSignalEnumerationCollectionByCollectionName = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _Dacn1AfdxSignalEnumerationCollectionByCollectionName As Dacn1AfdxSignalEnumerationCollection
		#End Region

		#Region "UpToDacn1AfdxMessageByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - DACN1_AFDX_MESSAGEDACN1_AFDX_DISCRETE
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToDacn1AfdxMessageByCollectionName As Dacn1AfdxMessage
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToDacn1AfdxMessageByCollectionName Is Nothing _
						 AndAlso Not CollectionName.Equals(Nothing)  AndAlso Not Vlid.Equals(Nothing)  AndAlso Not UdpPortSource.Equals(Nothing)  AndAlso Not UdpPortDestination.Equals(Nothing)  AndAlso Not FdsName.Equals(Nothing)  Then
						
					Me._UpToDacn1AfdxMessageByCollectionName = New Dacn1AfdxMessage()
					Me._UpToDacn1AfdxMessageByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToDacn1AfdxMessageByCollectionName", Me._UpToDacn1AfdxMessageByCollectionName)
					Me._UpToDacn1AfdxMessageByCollectionName.Query.Where(Me._UpToDacn1AfdxMessageByCollectionName.Query.CollectionName = Me.CollectionName)
					Me._UpToDacn1AfdxMessageByCollectionName.Query.Where(Me._UpToDacn1AfdxMessageByCollectionName.Query.Vlid = Me.Vlid)
					Me._UpToDacn1AfdxMessageByCollectionName.Query.Where(Me._UpToDacn1AfdxMessageByCollectionName.Query.UdpPortSource = Me.UdpPortSource)
					Me._UpToDacn1AfdxMessageByCollectionName.Query.Where(Me._UpToDacn1AfdxMessageByCollectionName.Query.UdpPortDestination = Me.UdpPortDestination)
					Me._UpToDacn1AfdxMessageByCollectionName.Query.Where(Me._UpToDacn1AfdxMessageByCollectionName.Query.FdsName = Me.FdsName)
					Me._UpToDacn1AfdxMessageByCollectionName.Query.Load()
				End If

				Return Me._UpToDacn1AfdxMessageByCollectionName
			End Get
			
            Set(ByVal value As Dacn1AfdxMessage)
				Me.RemovePreSave("UpToDacn1AfdxMessageByCollectionName")
				

				If value Is Nothing Then
				
					Me.CollectionName = Nothing
				
					Me.Vlid = Nothing
				
					Me.UdpPortSource = Nothing
				
					Me.UdpPortDestination = Nothing
				
					Me.FdsName = Nothing
				
					Me._UpToDacn1AfdxMessageByCollectionName = Nothing
				Else
				
					Me.CollectionName = value.CollectionName
					
					Me.Vlid = value.Vlid
					
					Me.UdpPortSource = value.UdpPortSource
					
					Me.UdpPortDestination = value.UdpPortDestination
					
					Me.FdsName = value.FdsName
					
					Me._UpToDacn1AfdxMessageByCollectionName = value
					Me.SetPreSave("UpToDacn1AfdxMessageByCollectionName", Me._UpToDacn1AfdxMessageByCollectionName)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToEngineeringUnitByEngUnitID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - EngineeringUnitDACN1_AFDX_SIGNAL
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

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "Dacn1AfdxSignalEnumerationCollectionByCollectionName"
					coll = Me.Dacn1AfdxSignalEnumerationCollectionByCollectionName
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "Dacn1AfdxSignalEnumerationCollectionByCollectionName", GetType(Dacn1AfdxSignalEnumerationCollection), New Dacn1AfdxSignalEnumeration()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class Dacn1AfdxSignalMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.CollectionName, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			c.Description = "EngineModel"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.Vlid, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.Vlid
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.UdpPortSource, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.UdpPortSource
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.UdpPortDestination, 3, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.UdpPortDestination
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.FdsName, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.FdsName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.ParameterName, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.ParameterName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.ExportToCsv, 6, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.TxRx, 7, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.TxRx
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Tx/Rx CE CHAMPS EST DUPLICATA De celui de FDS"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.SignalNumberOfBit, 8, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.SignalNumberOfBit
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.SignalAddress, 9, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.SignalAddress
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.StartBit, 10, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.StartBit
			c.NumericPrecision = 10
			c.Description = "Bit Position dans le FDS a l'adresse FdsAddress voir table DACN1_AFDX_MESSAGE"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.StopBit, 11, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.StopBit
			c.NumericPrecision = 10
			c.Description = "Bit Position dans le FDS a l'adresse FdsAddress voir table DACN1_AFDX_MESSAGE"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.LSBValue, 12, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.LSBValue
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "1"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.DataType, 13, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.DataType
			c.NumericPrecision = 10
			c.Description = "ParameterType or DataType is the same"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.CodingType, 14, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.CodingType
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.EngUnitID, 15, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.EngUnitID
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.MinValue, 16, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.MinValue
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.MaxValue, 17, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.MaxValue
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.Accuracy, 18, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.Accuracy
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.Format, 19, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.Format
			c.CharacterMaxLength = 255
			c.Description = "String Format"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.DisplayFormat, 20, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.DisplayFormat
			c.CharacterMaxLength = 3
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.DecimalDigits, 21, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.DecimalDigits
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.Length, 22, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.Length
			c.NumericPrecision = 10
			c.Description = "String Length, Opagque Length"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalMetadata.ColumnNames.HideInGrid, 23, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1AfdxSignalMetadata.PropertyNames.HideInGrid
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As Dacn1AfdxSignalMetadata
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
			 Public Const Vlid As String = "VLID"
			 Public Const UdpPortSource As String = "UdpPortSource"
			 Public Const UdpPortDestination As String = "UdpPortDestination"
			 Public Const FdsName As String = "FdsName"
			 Public Const ParameterName As String = "ParameterName"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const TxRx As String = "TxRx"
			 Public Const SignalNumberOfBit As String = "SignalNumberOfBit"
			 Public Const SignalAddress As String = "SignalAddress"
			 Public Const StartBit As String = "StartBit"
			 Public Const StopBit As String = "StopBit"
			 Public Const LSBValue As String = "LSBValue"
			 Public Const DataType As String = "DataType"
			 Public Const CodingType As String = "CodingType"
			 Public Const EngUnitID As String = "EngUnitID"
			 Public Const MinValue As String = "MinValue"
			 Public Const MaxValue As String = "MaxValue"
			 Public Const Accuracy As String = "Accuracy"
			 Public Const Format As String = "Format"
			 Public Const DisplayFormat As String = "DisplayFormat"
			 Public Const DecimalDigits As String = "DecimalDigits"
			 Public Const Length As String = "Length"
			 Public Const HideInGrid As String = "HideInGrid"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const CollectionName As String = "CollectionName"
			 Public Const Vlid As String = "Vlid"
			 Public Const UdpPortSource As String = "UdpPortSource"
			 Public Const UdpPortDestination As String = "UdpPortDestination"
			 Public Const FdsName As String = "FdsName"
			 Public Const ParameterName As String = "ParameterName"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const TxRx As String = "TxRx"
			 Public Const SignalNumberOfBit As String = "SignalNumberOfBit"
			 Public Const SignalAddress As String = "SignalAddress"
			 Public Const StartBit As String = "StartBit"
			 Public Const StopBit As String = "StopBit"
			 Public Const LSBValue As String = "LSBValue"
			 Public Const DataType As String = "DataType"
			 Public Const CodingType As String = "CodingType"
			 Public Const EngUnitID As String = "EngUnitID"
			 Public Const MinValue As String = "MinValue"
			 Public Const MaxValue As String = "MaxValue"
			 Public Const Accuracy As String = "Accuracy"
			 Public Const Format As String = "Format"
			 Public Const DisplayFormat As String = "DisplayFormat"
			 Public Const DecimalDigits As String = "DecimalDigits"
			 Public Const Length As String = "Length"
			 Public Const HideInGrid As String = "HideInGrid"
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
			SyncLock GetType(Dacn1AfdxSignalMetadata)
			
				If Dacn1AfdxSignalMetadata.mapDelegates Is Nothing Then
					Dacn1AfdxSignalMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If Dacn1AfdxSignalMetadata._meta Is Nothing Then
					Dacn1AfdxSignalMetadata._meta = New Dacn1AfdxSignalMetadata()
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
				meta.AddTypeMap("Vlid", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("UdpPortSource", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("UdpPortDestination", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("FdsName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ParameterName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("TxRx", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("SignalNumberOfBit", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("SignalAddress", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("StartBit", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("StopBit", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("LSBValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("DataType", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("CodingType", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EngUnitID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MinValue", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MaxValue", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Accuracy", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Format", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DisplayFormat", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DecimalDigits", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Length", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("HideInGrid", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "DACN1_AFDX_SIGNAL"
				meta.Destination = "DACN1_AFDX_SIGNAL"
				
				meta.spInsert = "proc_DACN1_AFDX_SIGNALInsert"
				meta.spUpdate = "proc_DACN1_AFDX_SIGNALUpdate"
				meta.spDelete = "proc_DACN1_AFDX_SIGNALDelete"
				meta.spLoadAll = "proc_DACN1_AFDX_SIGNALLoadAll"
				meta.spLoadByPrimaryKey = "proc_DACN1_AFDX_SIGNALLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As Dacn1AfdxSignalMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

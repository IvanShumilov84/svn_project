
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 31/01/2014 10:52:25 AM
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
	' Encapsulates the 'DACN1_AFDX_PORT' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(Dacn1AfdxPort))> _
	<XmlType("Dacn1AfdxPort")> _	
	Partial Public Class Dacn1AfdxPort 
		Inherits esDacn1AfdxPort
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New Dacn1AfdxPort()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32)
			Dim obj As New Dacn1AfdxPort()
			obj.CollectionName = collectionName
			obj.Vlid = vlid
			obj.UdpPortSource = udpPortSource
			obj.UdpPortDestination = udpPortDestination
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New Dacn1AfdxPort()
			obj.CollectionName = collectionName
			obj.Vlid = vlid
			obj.UdpPortSource = udpPortSource
			obj.UdpPortDestination = udpPortDestination
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("Dacn1AfdxPortCollection")> _
	Partial Public Class Dacn1AfdxPortCollection
		Inherits esDacn1AfdxPortCollection
		Implements IEnumerable(Of Dacn1AfdxPort)
	
		Public Function FindByPrimaryKey(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32) As Dacn1AfdxPort
			Return MyBase.SingleOrDefault(Function(e) e.CollectionName = collectionName And e.Vlid.HasValue AndAlso e.Vlid.Value = vlid And e.UdpPortSource.HasValue AndAlso e.UdpPortSource.Value = udpPortSource And e.UdpPortDestination.HasValue AndAlso e.UdpPortDestination.Value = udpPortDestination)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(Dacn1AfdxPort))> _
		Public Class Dacn1AfdxPortCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of Dacn1AfdxPortCollection)
			
			Public Shared Widening Operator CType(packet As Dacn1AfdxPortCollectionWCFPacket) As Dacn1AfdxPortCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As Dacn1AfdxPortCollection) As Dacn1AfdxPortCollectionWCFPacket
				Return New Dacn1AfdxPortCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class Dacn1AfdxPortQuery 
		Inherits esDacn1AfdxPortQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "Dacn1AfdxPortQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As Dacn1AfdxPortQuery) As String
			Return Dacn1AfdxPortQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As Dacn1AfdxPortQuery
			Return DirectCast(Dacn1AfdxPortQuery.SerializeHelper.FromXml(query, GetType(Dacn1AfdxPortQuery)), Dacn1AfdxPortQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDacn1AfdxPort
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, vlid, udpPortSource, udpPortDestination)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, vlid, udpPortSource, udpPortDestination)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, vlid, udpPortSource, udpPortDestination)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, vlid, udpPortSource, udpPortDestination)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32) As Boolean
		
			Dim query As New Dacn1AfdxPortQuery()
			query.Where(query.CollectionName = collectionName And query.Vlid = vlid And query.UdpPortSource = udpPortSource And query.UdpPortDestination = udpPortDestination)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("CollectionName", collectionName)
						parms.Add("Vlid", vlid)
						parms.Add("UdpPortSource", udpPortSource)
						parms.Add("UdpPortDestination", udpPortDestination)
			
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
				Return MyBase.GetSystemString(Dacn1AfdxPortMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxPortMetadata.ColumnNames.CollectionName, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.VLID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Vlid As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.Vlid)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.Vlid, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.Vlid)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.UdpPortSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UdpPortSource As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.UdpPortSource)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.UdpPortSource, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.UdpPortSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.UdpPortDestination
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UdpPortDestination As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.UdpPortDestination)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.UdpPortDestination, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.UdpPortDestination)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Name As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxPortMetadata.ColumnNames.Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxPortMetadata.ColumnNames.Name, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.TxRx
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TxRx As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxPortMetadata.ColumnNames.TxRx)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxPortMetadata.ColumnNames.TxRx, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.TxRx)
				End If
			End Set
		End Property	
			
		' <summary>
		' Flags:VL_MULTI; VL_FRAG_EN; VL_ICMP_EN; VL_EDE_EN
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PortMode As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxPortMetadata.ColumnNames.PortMode)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxPortMetadata.ColumnNames.PortMode, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PortMode)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.PortID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PortID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.PortID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.PortID, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PortID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1AfdxPortMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1AfdxPortMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.Period
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Period As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.Period)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.Period, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.Period)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.PeriodSkew
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PeriodSkew As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.PeriodSkew)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.PeriodSkew, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PeriodSkew)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.EndSystemSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EndSystemSource As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.EndSystemSource)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.EndSystemSource, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.EndSystemSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.PartitionSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PartitionSource As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.PartitionSource)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.PartitionSource, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PartitionSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.EndSystemDestination
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EndSystemDestination As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.EndSystemDestination)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.EndSystemDestination, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.EndSystemDestination)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.PartitionDestination
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PartitionDestination As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.PartitionDestination)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.PartitionDestination, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PartitionDestination)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.SubVLID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SubVLID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.SubVLID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.SubVLID, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.SubVLID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Or PortType = Sampling, Queuing, SAP
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PortType As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.PortType)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.PortType, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PortType)
				End If
			End Set
		End Property	
			
		' <summary>
		' Longueur du message AFDX (Payload)
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MessageLength As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.MessageLength)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.MessageLength, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.MessageLength)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.Depth
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Depth As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.Depth)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.Depth, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.Depth)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_PORT.BufferSize
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property BufferSize As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.BufferSize)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxPortMetadata.ColumnNames.BufferSize, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.BufferSize)
				End If
			End Set
		End Property	
			
		' <summary>
		' 0 = Inactif et 1 =Permet d'activer le tracing de librairie libceldas pour les trame AFDX et afficher sur la console QNX
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TracingEnabled As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxPortMetadata.ColumnNames.TracingEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxPortMetadata.ColumnNames.TracingEnabled, value) Then
					OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.TracingEnabled)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToDacn1AfdxVirtualLinkByCollectionName As Dacn1AfdxVirtualLink
		
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
												
						Case "Name"
							Me.str().Name = CType(value, string)
												
						Case "TxRx"
							Me.str().TxRx = CType(value, string)
												
						Case "PortMode"
							Me.str().PortMode = CType(value, string)
												
						Case "PortID"
							Me.str().PortID = CType(value, string)
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "Period"
							Me.str().Period = CType(value, string)
												
						Case "PeriodSkew"
							Me.str().PeriodSkew = CType(value, string)
												
						Case "EndSystemSource"
							Me.str().EndSystemSource = CType(value, string)
												
						Case "PartitionSource"
							Me.str().PartitionSource = CType(value, string)
												
						Case "EndSystemDestination"
							Me.str().EndSystemDestination = CType(value, string)
												
						Case "PartitionDestination"
							Me.str().PartitionDestination = CType(value, string)
												
						Case "SubVLID"
							Me.str().SubVLID = CType(value, string)
												
						Case "PortType"
							Me.str().PortType = CType(value, string)
												
						Case "MessageLength"
							Me.str().MessageLength = CType(value, string)
												
						Case "Depth"
							Me.str().Depth = CType(value, string)
												
						Case "BufferSize"
							Me.str().BufferSize = CType(value, string)
												
						Case "TracingEnabled"
							Me.str().TracingEnabled = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Vlid"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Vlid = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.Vlid)
							End If
						
						Case "UdpPortSource"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.UdpPortSource = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.UdpPortSource)
							End If
						
						Case "UdpPortDestination"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.UdpPortDestination = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.UdpPortDestination)
							End If
						
						Case "TxRx"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.TxRx = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.TxRx)
							End If
						
						Case "PortID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.PortID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PortID)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "Period"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Period = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.Period)
							End If
						
						Case "PeriodSkew"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.PeriodSkew = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PeriodSkew)
							End If
						
						Case "EndSystemSource"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.EndSystemSource = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.EndSystemSource)
							End If
						
						Case "PartitionSource"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.PartitionSource = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PartitionSource)
							End If
						
						Case "EndSystemDestination"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.EndSystemDestination = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.EndSystemDestination)
							End If
						
						Case "PartitionDestination"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.PartitionDestination = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PartitionDestination)
							End If
						
						Case "SubVLID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SubVLID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.SubVLID)
							End If
						
						Case "PortType"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.PortType = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.PortType)
							End If
						
						Case "MessageLength"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.MessageLength = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.MessageLength)
							End If
						
						Case "Depth"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Depth = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.Depth)
							End If
						
						Case "BufferSize"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.BufferSize = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.BufferSize)
							End If
						
						Case "TracingEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.TracingEnabled = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxPortMetadata.PropertyNames.TracingEnabled)
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
		
			Public Sub New(ByVal entity As esDacn1AfdxPort)
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
		  	
			Public Property PortMode As System.String 
				Get
					Dim data_ As System.String = entity.PortMode
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PortMode = Nothing
					Else
						entity.PortMode = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property PortID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.PortID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PortID = Nothing
					Else
						entity.PortID = Convert.ToInt32(Value)
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
		  	
			Public Property Period As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Period
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Period = Nothing
					Else
						entity.Period = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property PeriodSkew As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.PeriodSkew
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PeriodSkew = Nothing
					Else
						entity.PeriodSkew = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property EndSystemSource As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.EndSystemSource
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EndSystemSource = Nothing
					Else
						entity.EndSystemSource = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property PartitionSource As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.PartitionSource
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PartitionSource = Nothing
					Else
						entity.PartitionSource = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property EndSystemDestination As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.EndSystemDestination
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EndSystemDestination = Nothing
					Else
						entity.EndSystemDestination = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property PartitionDestination As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.PartitionDestination
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PartitionDestination = Nothing
					Else
						entity.PartitionDestination = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property SubVLID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SubVLID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SubVLID = Nothing
					Else
						entity.SubVLID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property PortType As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.PortType
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PortType = Nothing
					Else
						entity.PortType = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MessageLength As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.MessageLength
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MessageLength = Nothing
					Else
						entity.MessageLength = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Depth As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Depth
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Depth = Nothing
					Else
						entity.Depth = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property BufferSize As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.BufferSize
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.BufferSize = Nothing
					Else
						entity.BufferSize = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property TracingEnabled As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.TracingEnabled
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TracingEnabled = Nothing
					Else
						entity.TracingEnabled = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  

			Private entity As esDacn1AfdxPort
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1AfdxPortMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As Dacn1AfdxPortQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1AfdxPortQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As Dacn1AfdxPortQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As Dacn1AfdxPortQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As Dacn1AfdxPortQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDacn1AfdxPortCollection
		Inherits esEntityCollection(Of Dacn1AfdxPort)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1AfdxPortMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "Dacn1AfdxPortCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As Dacn1AfdxPortQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1AfdxPortQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As Dacn1AfdxPortQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New Dacn1AfdxPortQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As Dacn1AfdxPortQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, Dacn1AfdxPortQuery))
		End Sub
		
		#End Region
						
		Private m_query As Dacn1AfdxPortQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDacn1AfdxPortQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return Dacn1AfdxPortMetadata.Meta()
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
				Case "Name" 
					Return Me.Name
				Case "TxRx" 
					Return Me.TxRx
				Case "PortMode" 
					Return Me.PortMode
				Case "PortID" 
					Return Me.PortID
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "Period" 
					Return Me.Period
				Case "PeriodSkew" 
					Return Me.PeriodSkew
				Case "EndSystemSource" 
					Return Me.EndSystemSource
				Case "PartitionSource" 
					Return Me.PartitionSource
				Case "EndSystemDestination" 
					Return Me.EndSystemDestination
				Case "PartitionDestination" 
					Return Me.PartitionDestination
				Case "SubVLID" 
					Return Me.SubVLID
				Case "PortType" 
					Return Me.PortType
				Case "MessageLength" 
					Return Me.MessageLength
				Case "Depth" 
					Return Me.Depth
				Case "BufferSize" 
					Return Me.BufferSize
				Case "TracingEnabled" 
					Return Me.TracingEnabled
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Vlid As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.Vlid, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property UdpPortSource As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.UdpPortSource, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property UdpPortDestination As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.UdpPortDestination, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Name As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TxRx As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.TxRx, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property PortMode As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.PortMode, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property PortID As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.PortID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Period As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.Period, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property PeriodSkew As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.PeriodSkew, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EndSystemSource As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.EndSystemSource, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property PartitionSource As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.PartitionSource, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EndSystemDestination As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.EndSystemDestination, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property PartitionDestination As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.PartitionDestination, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property SubVLID As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.SubVLID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property PortType As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.PortType, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MessageLength As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.MessageLength, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Depth As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.Depth, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property BufferSize As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.BufferSize, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property TracingEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.TracingEnabled, esSystemType.Byte)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class Dacn1AfdxPort 
		Inherits esDacn1AfdxPort
		
	
		#Region "Dacn1AfdxMessageCollectionByCollectionName - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_Dacn1AfdxMessageCollectionByCollectionName() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Dacn1AfdxPort.Dacn1AfdxMessageCollectionByCollectionName_Delegate)
				map.PropertyName = "Dacn1AfdxMessageCollectionByCollectionName"
				map.MyColumnName = "CollectionName,VLID,UdpPortSource,UdpPortDestination"
				map.ParentColumnName = "CollectionName,VLID,UdpPortSource,UdpPortDestination"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub Dacn1AfdxMessageCollectionByCollectionName_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New Dacn1AfdxPortQuery(data.NextAlias())
			
			Dim mee As Dacn1AfdxMessageQuery = If(data.You IsNot Nothing, TryCast(data.You, Dacn1AfdxMessageQuery), New Dacn1AfdxMessageQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CollectionName = mee.CollectionName And parent.Vlid = mee.Vlid And parent.UdpPortSource = mee.UdpPortSource And parent.UdpPortDestination = mee.UdpPortDestination)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - DACN1_AFDX_PORTDACN1_AFDX_MESSAGE
		''' </summary>

		<XmlIgnore()> _ 
		Public Property Dacn1AfdxMessageCollectionByCollectionName As Dacn1AfdxMessageCollection 
		
			Get
				If Me._Dacn1AfdxMessageCollectionByCollectionName Is Nothing Then
					Me._Dacn1AfdxMessageCollectionByCollectionName = New Dacn1AfdxMessageCollection()
					Me._Dacn1AfdxMessageCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("Dacn1AfdxMessageCollectionByCollectionName", Me._Dacn1AfdxMessageCollectionByCollectionName)
				
					If Not Me.CollectionName.Equals(Nothing) AndAlso Not Me.Vlid.Equals(Nothing) AndAlso Not Me.UdpPortSource.Equals(Nothing) AndAlso Not Me.UdpPortDestination.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._Dacn1AfdxMessageCollectionByCollectionName.Query.Where(Me._Dacn1AfdxMessageCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
							Me._Dacn1AfdxMessageCollectionByCollectionName.Query.Where(Me._Dacn1AfdxMessageCollectionByCollectionName.Query.Vlid = Me.Vlid)
							Me._Dacn1AfdxMessageCollectionByCollectionName.Query.Where(Me._Dacn1AfdxMessageCollectionByCollectionName.Query.UdpPortSource = Me.UdpPortSource)
							Me._Dacn1AfdxMessageCollectionByCollectionName.Query.Where(Me._Dacn1AfdxMessageCollectionByCollectionName.Query.UdpPortDestination = Me.UdpPortDestination)
							Me._Dacn1AfdxMessageCollectionByCollectionName.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._Dacn1AfdxMessageCollectionByCollectionName.fks.Add(Dacn1AfdxMessageMetadata.ColumnNames.CollectionName, Me.CollectionName)
						Me._Dacn1AfdxMessageCollectionByCollectionName.fks.Add(Dacn1AfdxMessageMetadata.ColumnNames.Vlid, Me.Vlid)
						Me._Dacn1AfdxMessageCollectionByCollectionName.fks.Add(Dacn1AfdxMessageMetadata.ColumnNames.UdpPortSource, Me.UdpPortSource)
						Me._Dacn1AfdxMessageCollectionByCollectionName.fks.Add(Dacn1AfdxMessageMetadata.ColumnNames.UdpPortDestination, Me.UdpPortDestination)
					End If
				End If

				Return Me._Dacn1AfdxMessageCollectionByCollectionName
			End Get
			
			Set(ByVal value As Dacn1AfdxMessageCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._Dacn1AfdxMessageCollectionByCollectionName Is Nothing Then

					Me.RemovePostSave("Dacn1AfdxMessageCollectionByCollectionName")
					Me._Dacn1AfdxMessageCollectionByCollectionName = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _Dacn1AfdxMessageCollectionByCollectionName As Dacn1AfdxMessageCollection
		#End Region

		#Region "UpToDacn1AfdxVirtualLinkByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - {5D9741FF-5531-46BE-9E93-DB7B87963973}
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToDacn1AfdxVirtualLinkByCollectionName As Dacn1AfdxVirtualLink
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToDacn1AfdxVirtualLinkByCollectionName Is Nothing _
						 AndAlso Not CollectionName.Equals(Nothing)  AndAlso Not Vlid.Equals(Nothing)  Then
						
					Me._UpToDacn1AfdxVirtualLinkByCollectionName = New Dacn1AfdxVirtualLink()
					Me._UpToDacn1AfdxVirtualLinkByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToDacn1AfdxVirtualLinkByCollectionName", Me._UpToDacn1AfdxVirtualLinkByCollectionName)
					Me._UpToDacn1AfdxVirtualLinkByCollectionName.Query.Where(Me._UpToDacn1AfdxVirtualLinkByCollectionName.Query.CollectionName = Me.CollectionName)
					Me._UpToDacn1AfdxVirtualLinkByCollectionName.Query.Where(Me._UpToDacn1AfdxVirtualLinkByCollectionName.Query.Vlid = Me.Vlid)
					Me._UpToDacn1AfdxVirtualLinkByCollectionName.Query.Load()
				End If

				Return Me._UpToDacn1AfdxVirtualLinkByCollectionName
			End Get
			
            Set(ByVal value As Dacn1AfdxVirtualLink)
				Me.RemovePreSave("UpToDacn1AfdxVirtualLinkByCollectionName")
				

				If value Is Nothing Then
				
					Me.CollectionName = Nothing
				
					Me.Vlid = Nothing
				
					Me._UpToDacn1AfdxVirtualLinkByCollectionName = Nothing
				Else
				
					Me.CollectionName = value.CollectionName
					
					Me.Vlid = value.Vlid
					
					Me._UpToDacn1AfdxVirtualLinkByCollectionName = value
					Me.SetPreSave("UpToDacn1AfdxVirtualLinkByCollectionName", Me._UpToDacn1AfdxVirtualLinkByCollectionName)
				End If
				
			End Set	

		End Property
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "Dacn1AfdxMessageCollectionByCollectionName"
					coll = Me.Dacn1AfdxMessageCollectionByCollectionName
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "Dacn1AfdxMessageCollectionByCollectionName", GetType(Dacn1AfdxMessageCollection), New Dacn1AfdxMessage()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class Dacn1AfdxPortMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.CollectionName, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			c.Description = "EngineModel"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.Vlid, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.Vlid
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.UdpPortSource, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.UdpPortSource
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.UdpPortDestination, 3, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.UdpPortDestination
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.Name, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.Name
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.TxRx, 5, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.TxRx
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.PortMode, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.PortMode
			c.CharacterMaxLength = 25
			c.Description = "Flags:VL_MULTI; VL_FRAG_EN; VL_ICMP_EN; VL_EDE_EN"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.PortID, 7, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.PortID
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.ExportToCsv, 8, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.Period, 9, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.Period
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.PeriodSkew, 10, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.PeriodSkew
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.EndSystemSource, 11, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.EndSystemSource
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.PartitionSource, 12, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.PartitionSource
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.EndSystemDestination, 13, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.EndSystemDestination
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.PartitionDestination, 14, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.PartitionDestination
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.SubVLID, 15, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.SubVLID
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.PortType, 16, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.PortType
			c.NumericPrecision = 10
			c.Description = "Or PortType = Sampling, Queuing, SAP"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.MessageLength, 17, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.MessageLength
			c.NumericPrecision = 10
			c.Description = "Longueur du message AFDX (Payload)"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.Depth, 18, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.Depth
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.BufferSize, 19, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.BufferSize
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxPortMetadata.ColumnNames.TracingEnabled, 20, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxPortMetadata.PropertyNames.TracingEnabled
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "0 = Inactif et 1 =Permet d'activer le tracing de librairie libceldas pour les trame AFDX et afficher sur la console QNX"
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As Dacn1AfdxPortMetadata
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
			 Public Const Name As String = "Name"
			 Public Const TxRx As String = "TxRx"
			 Public Const PortMode As String = "PortMode"
			 Public Const PortID As String = "PortID"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const Period As String = "Period"
			 Public Const PeriodSkew As String = "PeriodSkew"
			 Public Const EndSystemSource As String = "EndSystemSource"
			 Public Const PartitionSource As String = "PartitionSource"
			 Public Const EndSystemDestination As String = "EndSystemDestination"
			 Public Const PartitionDestination As String = "PartitionDestination"
			 Public Const SubVLID As String = "SubVLID"
			 Public Const PortType As String = "PortType"
			 Public Const MessageLength As String = "MessageLength"
			 Public Const Depth As String = "Depth"
			 Public Const BufferSize As String = "BufferSize"
			 Public Const TracingEnabled As String = "TracingEnabled"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const CollectionName As String = "CollectionName"
			 Public Const Vlid As String = "Vlid"
			 Public Const UdpPortSource As String = "UdpPortSource"
			 Public Const UdpPortDestination As String = "UdpPortDestination"
			 Public Const Name As String = "Name"
			 Public Const TxRx As String = "TxRx"
			 Public Const PortMode As String = "PortMode"
			 Public Const PortID As String = "PortID"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const Period As String = "Period"
			 Public Const PeriodSkew As String = "PeriodSkew"
			 Public Const EndSystemSource As String = "EndSystemSource"
			 Public Const PartitionSource As String = "PartitionSource"
			 Public Const EndSystemDestination As String = "EndSystemDestination"
			 Public Const PartitionDestination As String = "PartitionDestination"
			 Public Const SubVLID As String = "SubVLID"
			 Public Const PortType As String = "PortType"
			 Public Const MessageLength As String = "MessageLength"
			 Public Const Depth As String = "Depth"
			 Public Const BufferSize As String = "BufferSize"
			 Public Const TracingEnabled As String = "TracingEnabled"
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
			SyncLock GetType(Dacn1AfdxPortMetadata)
			
				If Dacn1AfdxPortMetadata.mapDelegates Is Nothing Then
					Dacn1AfdxPortMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If Dacn1AfdxPortMetadata._meta Is Nothing Then
					Dacn1AfdxPortMetadata._meta = New Dacn1AfdxPortMetadata()
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
				meta.AddTypeMap("Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("TxRx", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("PortMode", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("PortID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Period", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("PeriodSkew", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EndSystemSource", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("PartitionSource", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EndSystemDestination", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("PartitionDestination", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("SubVLID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("PortType", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MessageLength", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Depth", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("BufferSize", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("TracingEnabled", new esTypeMap("Byte", "System.Byte"))			
				
				
				 
				meta.Source = "DACN1_AFDX_PORT"
				meta.Destination = "DACN1_AFDX_PORT"
				
				meta.spInsert = "proc_DACN1_AFDX_PORTInsert"
				meta.spUpdate = "proc_DACN1_AFDX_PORTUpdate"
				meta.spDelete = "proc_DACN1_AFDX_PORTDelete"
				meta.spLoadAll = "proc_DACN1_AFDX_PORTLoadAll"
				meta.spLoadByPrimaryKey = "proc_DACN1_AFDX_PORTLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As Dacn1AfdxPortMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

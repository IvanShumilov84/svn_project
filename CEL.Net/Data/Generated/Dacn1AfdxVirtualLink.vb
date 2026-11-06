
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
	' Encapsulates the 'DACN1_AFDX_VIRTUAL_LINK' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(Dacn1AfdxVirtualLink))> _
	<XmlType("Dacn1AfdxVirtualLink")> _	
	Partial Public Class Dacn1AfdxVirtualLink 
		Inherits esDacn1AfdxVirtualLink
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New Dacn1AfdxVirtualLink()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal vlid As System.Int32)
			Dim obj As New Dacn1AfdxVirtualLink()
			obj.CollectionName = collectionName
			obj.Vlid = vlid
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New Dacn1AfdxVirtualLink()
			obj.CollectionName = collectionName
			obj.Vlid = vlid
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("Dacn1AfdxVirtualLinkCollection")> _
	Partial Public Class Dacn1AfdxVirtualLinkCollection
		Inherits esDacn1AfdxVirtualLinkCollection
		Implements IEnumerable(Of Dacn1AfdxVirtualLink)
	
		Public Function FindByPrimaryKey(ByVal collectionName As System.String, ByVal vlid As System.Int32) As Dacn1AfdxVirtualLink
			Return MyBase.SingleOrDefault(Function(e) e.CollectionName = collectionName And e.Vlid.HasValue AndAlso e.Vlid.Value = vlid)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(Dacn1AfdxVirtualLink))> _
		Public Class Dacn1AfdxVirtualLinkCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of Dacn1AfdxVirtualLinkCollection)
			
			Public Shared Widening Operator CType(packet As Dacn1AfdxVirtualLinkCollectionWCFPacket) As Dacn1AfdxVirtualLinkCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As Dacn1AfdxVirtualLinkCollection) As Dacn1AfdxVirtualLinkCollectionWCFPacket
				Return New Dacn1AfdxVirtualLinkCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class Dacn1AfdxVirtualLinkQuery 
		Inherits esDacn1AfdxVirtualLinkQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "Dacn1AfdxVirtualLinkQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As Dacn1AfdxVirtualLinkQuery) As String
			Return Dacn1AfdxVirtualLinkQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As Dacn1AfdxVirtualLinkQuery
			Return DirectCast(Dacn1AfdxVirtualLinkQuery.SerializeHelper.FromXml(query, GetType(Dacn1AfdxVirtualLinkQuery)), Dacn1AfdxVirtualLinkQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDacn1AfdxVirtualLink
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal collectionName As System.String, ByVal vlid As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, vlid)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, vlid)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal collectionName As System.String, ByVal vlid As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, vlid)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, vlid)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal collectionName As System.String, ByVal vlid As System.Int32) As Boolean
		
			Dim query As New Dacn1AfdxVirtualLinkQuery()
			query.Where(query.CollectionName = collectionName And query.Vlid = vlid)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal collectionName As System.String, ByVal vlid As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("CollectionName", collectionName)
						parms.Add("Vlid", vlid)
			
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
				Return MyBase.GetSystemString(Dacn1AfdxVirtualLinkMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxVirtualLinkMetadata.ColumnNames.CollectionName, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.VLID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Vlid As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Vlid)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Vlid, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.Vlid)
				End If
			End Set
		End Property	
			
		' <summary>
		' Arinc Channel A/B
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ArincChannel As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.ArincChannel)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.ArincChannel, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.ArincChannel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.IntegrityCheck
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property IntegrityCheck As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.IntegrityCheck)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.IntegrityCheck, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.IntegrityCheck)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.RedondancyEnabled
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property RedondancyEnabled As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.RedondancyEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.RedondancyEnabled, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.RedondancyEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' Flags for Ch A or B or A and B;Redondancy Enabled and IntegrityCheck
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property VLMode As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxVirtualLinkMetadata.ColumnNames.VLMode)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxVirtualLinkMetadata.ColumnNames.VLMode, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.VLMode)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.VLName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property VLName As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxVirtualLinkMetadata.ColumnNames.VLName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxVirtualLinkMetadata.ColumnNames.VLName, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.VLName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.NumberOfSubVL
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property NumberOfSubVL As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.NumberOfSubVL)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.NumberOfSubVL, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.NumberOfSubVL)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.SubVLID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SubVLID As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.SubVLID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.SubVLID, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.SubVLID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1AfdxVirtualLinkMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1AfdxVirtualLinkMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.Description)
				End If
			End Set
		End Property	
			
		' <summary>
		' flag in port mode:  Unicast / Multicast
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property BroadcastType As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.BroadcastType)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.BroadcastType, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.BroadcastType)
				End If
			End Set
		End Property	
			
		' <summary>
		' flag in port mode: Tx/Rx
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TxRx As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.TxRx)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.TxRx, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.TxRx)
				End If
			End Set
		End Property	
			
		' <summary>
		' flag in port mode:  VL Enable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property VirtualLinkEnabled As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.VirtualLinkEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.VirtualLinkEnabled, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.VirtualLinkEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' flag in port mode:  Fragmentation Enabled
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FragmentationEnabled As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.FragmentationEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.FragmentationEnabled, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.FragmentationEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' flag in port mode:  ICMP Protocol
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ICMPEnabled As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.ICMPEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.ICMPEnabled, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.ICMPEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.BAG
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Bag As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Bag)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Bag, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.Bag)
				End If
			End Set
		End Property	
			
		' <summary>
		' Max Frame Size or Maximum Transmit Unit
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Mtu As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Mtu)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Mtu, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.Mtu)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.MaxJitter
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MaxJitter As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.MaxJitter)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.MaxJitter, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.MaxJitter)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.SkewMax
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SkewMax As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.SkewMax)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.SkewMax, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.SkewMax)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.SkewDelay
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SkewDelay As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.SkewDelay)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.SkewDelay, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.SkewDelay)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.PrevSnRange
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PrevSnRange As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.PrevSnRange)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.PrevSnRange, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.PrevSnRange)
				End If
			End Set
		End Property	
			
		' <summary>
		' flag in port mode:  EDE Protocol
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EDEEnabled As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDEEnabled)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDEEnabled, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDEEnabled)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.EDESid
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EDESid As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDESid)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDESid, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDESid)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.EDESkewMax
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EDESkewMax As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDESkewMax)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDESkewMax, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDESkewMax)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.EDEAgeMax
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EDEAgeMax As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDEAgeMax)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDEAgeMax, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDEAgeMax)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_VIRTUAL_LINK.EDELagMax
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EDELagMax As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDELagMax)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDELagMax, value) Then
					OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDELagMax)
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
												
						Case "Vlid"
							Me.str().Vlid = CType(value, string)
												
						Case "ArincChannel"
							Me.str().ArincChannel = CType(value, string)
												
						Case "IntegrityCheck"
							Me.str().IntegrityCheck = CType(value, string)
												
						Case "RedondancyEnabled"
							Me.str().RedondancyEnabled = CType(value, string)
												
						Case "VLMode"
							Me.str().VLMode = CType(value, string)
												
						Case "VLName"
							Me.str().VLName = CType(value, string)
												
						Case "NumberOfSubVL"
							Me.str().NumberOfSubVL = CType(value, string)
												
						Case "SubVLID"
							Me.str().SubVLID = CType(value, string)
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
												
						Case "BroadcastType"
							Me.str().BroadcastType = CType(value, string)
												
						Case "TxRx"
							Me.str().TxRx = CType(value, string)
												
						Case "VirtualLinkEnabled"
							Me.str().VirtualLinkEnabled = CType(value, string)
												
						Case "FragmentationEnabled"
							Me.str().FragmentationEnabled = CType(value, string)
												
						Case "ICMPEnabled"
							Me.str().ICMPEnabled = CType(value, string)
												
						Case "Bag"
							Me.str().Bag = CType(value, string)
												
						Case "Mtu"
							Me.str().Mtu = CType(value, string)
												
						Case "MaxJitter"
							Me.str().MaxJitter = CType(value, string)
												
						Case "SkewMax"
							Me.str().SkewMax = CType(value, string)
												
						Case "SkewDelay"
							Me.str().SkewDelay = CType(value, string)
												
						Case "PrevSnRange"
							Me.str().PrevSnRange = CType(value, string)
												
						Case "EDEEnabled"
							Me.str().EDEEnabled = CType(value, string)
												
						Case "EDESid"
							Me.str().EDESid = CType(value, string)
												
						Case "EDESkewMax"
							Me.str().EDESkewMax = CType(value, string)
												
						Case "EDEAgeMax"
							Me.str().EDEAgeMax = CType(value, string)
												
						Case "EDELagMax"
							Me.str().EDELagMax = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Vlid"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Vlid = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.Vlid)
							End If
						
						Case "ArincChannel"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.ArincChannel = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.ArincChannel)
							End If
						
						Case "IntegrityCheck"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.IntegrityCheck = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.IntegrityCheck)
							End If
						
						Case "RedondancyEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.RedondancyEnabled = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.RedondancyEnabled)
							End If
						
						Case "NumberOfSubVL"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.NumberOfSubVL = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.NumberOfSubVL)
							End If
						
						Case "SubVLID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.SubVLID = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.SubVLID)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "BroadcastType"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.BroadcastType = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.BroadcastType)
							End If
						
						Case "TxRx"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.TxRx = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.TxRx)
							End If
						
						Case "VirtualLinkEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.VirtualLinkEnabled = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.VirtualLinkEnabled)
							End If
						
						Case "FragmentationEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.FragmentationEnabled = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.FragmentationEnabled)
							End If
						
						Case "ICMPEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.ICMPEnabled = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.ICMPEnabled)
							End If
						
						Case "Bag"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Bag = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.Bag)
							End If
						
						Case "Mtu"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Mtu = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.Mtu)
							End If
						
						Case "MaxJitter"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.MaxJitter = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.MaxJitter)
							End If
						
						Case "SkewMax"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SkewMax = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.SkewMax)
							End If
						
						Case "SkewDelay"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SkewDelay = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.SkewDelay)
							End If
						
						Case "PrevSnRange"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.PrevSnRange = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.PrevSnRange)
							End If
						
						Case "EDEEnabled"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.EDEEnabled = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDEEnabled)
							End If
						
						Case "EDESid"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.EDESid = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDESid)
							End If
						
						Case "EDESkewMax"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.EDESkewMax = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDESkewMax)
							End If
						
						Case "EDEAgeMax"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.EDEAgeMax = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDEAgeMax)
							End If
						
						Case "EDELagMax"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.EDELagMax = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDELagMax)
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
		
			Public Sub New(ByVal entity As esDacn1AfdxVirtualLink)
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
		  	
			Public Property IntegrityCheck As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.IntegrityCheck
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.IntegrityCheck = Nothing
					Else
						entity.IntegrityCheck = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property RedondancyEnabled As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.RedondancyEnabled
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.RedondancyEnabled = Nothing
					Else
						entity.RedondancyEnabled = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property VLMode As System.String 
				Get
					Dim data_ As System.String = entity.VLMode
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.VLMode = Nothing
					Else
						entity.VLMode = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property VLName As System.String 
				Get
					Dim data_ As System.String = entity.VLName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.VLName = Nothing
					Else
						entity.VLName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property NumberOfSubVL As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.NumberOfSubVL
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.NumberOfSubVL = Nothing
					Else
						entity.NumberOfSubVL = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property SubVLID As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.SubVLID
					
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
						entity.SubVLID = Convert.ToByte(Value)
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
		  	
			Public Property BroadcastType As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.BroadcastType
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.BroadcastType = Nothing
					Else
						entity.BroadcastType = Convert.ToByte(Value)
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
		  	
			Public Property VirtualLinkEnabled As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.VirtualLinkEnabled
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.VirtualLinkEnabled = Nothing
					Else
						entity.VirtualLinkEnabled = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property FragmentationEnabled As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.FragmentationEnabled
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FragmentationEnabled = Nothing
					Else
						entity.FragmentationEnabled = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property ICMPEnabled As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.ICMPEnabled
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ICMPEnabled = Nothing
					Else
						entity.ICMPEnabled = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property Bag As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Bag
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Bag = Nothing
					Else
						entity.Bag = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Mtu As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.Mtu
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Mtu = Nothing
					Else
						entity.Mtu = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MaxJitter As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.MaxJitter
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MaxJitter = Nothing
					Else
						entity.MaxJitter = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property SkewMax As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SkewMax
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SkewMax = Nothing
					Else
						entity.SkewMax = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property SkewDelay As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SkewDelay
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SkewDelay = Nothing
					Else
						entity.SkewDelay = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property PrevSnRange As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.PrevSnRange
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.PrevSnRange = Nothing
					Else
						entity.PrevSnRange = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property EDEEnabled As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.EDEEnabled
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EDEEnabled = Nothing
					Else
						entity.EDEEnabled = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property EDESid As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.EDESid
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EDESid = Nothing
					Else
						entity.EDESid = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property EDESkewMax As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.EDESkewMax
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EDESkewMax = Nothing
					Else
						entity.EDESkewMax = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property EDEAgeMax As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.EDEAgeMax
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EDEAgeMax = Nothing
					Else
						entity.EDEAgeMax = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property EDELagMax As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.EDELagMax
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EDELagMax = Nothing
					Else
						entity.EDELagMax = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  

			Private entity As esDacn1AfdxVirtualLink
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1AfdxVirtualLinkMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As Dacn1AfdxVirtualLinkQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1AfdxVirtualLinkQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As Dacn1AfdxVirtualLinkQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As Dacn1AfdxVirtualLinkQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As Dacn1AfdxVirtualLinkQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDacn1AfdxVirtualLinkCollection
		Inherits esEntityCollection(Of Dacn1AfdxVirtualLink)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1AfdxVirtualLinkMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "Dacn1AfdxVirtualLinkCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As Dacn1AfdxVirtualLinkQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1AfdxVirtualLinkQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As Dacn1AfdxVirtualLinkQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New Dacn1AfdxVirtualLinkQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As Dacn1AfdxVirtualLinkQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, Dacn1AfdxVirtualLinkQuery))
		End Sub
		
		#End Region
						
		Private m_query As Dacn1AfdxVirtualLinkQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDacn1AfdxVirtualLinkQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return Dacn1AfdxVirtualLinkMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "CollectionName" 
					Return Me.CollectionName
				Case "Vlid" 
					Return Me.Vlid
				Case "ArincChannel" 
					Return Me.ArincChannel
				Case "IntegrityCheck" 
					Return Me.IntegrityCheck
				Case "RedondancyEnabled" 
					Return Me.RedondancyEnabled
				Case "VLMode" 
					Return Me.VLMode
				Case "VLName" 
					Return Me.VLName
				Case "NumberOfSubVL" 
					Return Me.NumberOfSubVL
				Case "SubVLID" 
					Return Me.SubVLID
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "Description" 
					Return Me.Description
				Case "BroadcastType" 
					Return Me.BroadcastType
				Case "TxRx" 
					Return Me.TxRx
				Case "VirtualLinkEnabled" 
					Return Me.VirtualLinkEnabled
				Case "FragmentationEnabled" 
					Return Me.FragmentationEnabled
				Case "ICMPEnabled" 
					Return Me.ICMPEnabled
				Case "Bag" 
					Return Me.Bag
				Case "Mtu" 
					Return Me.Mtu
				Case "MaxJitter" 
					Return Me.MaxJitter
				Case "SkewMax" 
					Return Me.SkewMax
				Case "SkewDelay" 
					Return Me.SkewDelay
				Case "PrevSnRange" 
					Return Me.PrevSnRange
				Case "EDEEnabled" 
					Return Me.EDEEnabled
				Case "EDESid" 
					Return Me.EDESid
				Case "EDESkewMax" 
					Return Me.EDESkewMax
				Case "EDEAgeMax" 
					Return Me.EDEAgeMax
				Case "EDELagMax" 
					Return Me.EDELagMax
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Vlid As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.Vlid, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ArincChannel As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.ArincChannel, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property IntegrityCheck As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.IntegrityCheck, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property RedondancyEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.RedondancyEnabled, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property VLMode As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.VLMode, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property VLName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.VLName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property NumberOfSubVL As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.NumberOfSubVL, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property SubVLID As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.SubVLID, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property BroadcastType As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.BroadcastType, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property TxRx As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.TxRx, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property VirtualLinkEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.VirtualLinkEnabled, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property FragmentationEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.FragmentationEnabled, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property ICMPEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.ICMPEnabled, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property Bag As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.Bag, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Mtu As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.Mtu, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MaxJitter As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.MaxJitter, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property SkewMax As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.SkewMax, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property SkewDelay As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.SkewDelay, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property PrevSnRange As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.PrevSnRange, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EDEEnabled As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDEEnabled, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property EDESid As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDESid, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EDESkewMax As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDESkewMax, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EDEAgeMax As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDEAgeMax, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EDELagMax As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDELagMax, esSystemType.Int32)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class Dacn1AfdxVirtualLink 
		Inherits esDacn1AfdxVirtualLink
		
	
		#Region "Dacn1AfdxPortCollectionByCollectionName - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_Dacn1AfdxPortCollectionByCollectionName() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Dacn1AfdxVirtualLink.Dacn1AfdxPortCollectionByCollectionName_Delegate)
				map.PropertyName = "Dacn1AfdxPortCollectionByCollectionName"
				map.MyColumnName = "CollectionName,VLID"
				map.ParentColumnName = "CollectionName,VLID"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub Dacn1AfdxPortCollectionByCollectionName_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New Dacn1AfdxVirtualLinkQuery(data.NextAlias())
			
			Dim mee As Dacn1AfdxPortQuery = If(data.You IsNot Nothing, TryCast(data.You, Dacn1AfdxPortQuery), New Dacn1AfdxPortQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CollectionName = mee.CollectionName And parent.Vlid = mee.Vlid)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - {5D9741FF-5531-46BE-9E93-DB7B87963973}
		''' </summary>

		<XmlIgnore()> _ 
		Public Property Dacn1AfdxPortCollectionByCollectionName As Dacn1AfdxPortCollection 
		
			Get
				If Me._Dacn1AfdxPortCollectionByCollectionName Is Nothing Then
					Me._Dacn1AfdxPortCollectionByCollectionName = New Dacn1AfdxPortCollection()
					Me._Dacn1AfdxPortCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("Dacn1AfdxPortCollectionByCollectionName", Me._Dacn1AfdxPortCollectionByCollectionName)
				
					If Not Me.CollectionName.Equals(Nothing) AndAlso Not Me.Vlid.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._Dacn1AfdxPortCollectionByCollectionName.Query.Where(Me._Dacn1AfdxPortCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
							Me._Dacn1AfdxPortCollectionByCollectionName.Query.Where(Me._Dacn1AfdxPortCollectionByCollectionName.Query.Vlid = Me.Vlid)
							Me._Dacn1AfdxPortCollectionByCollectionName.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._Dacn1AfdxPortCollectionByCollectionName.fks.Add(Dacn1AfdxPortMetadata.ColumnNames.CollectionName, Me.CollectionName)
						Me._Dacn1AfdxPortCollectionByCollectionName.fks.Add(Dacn1AfdxPortMetadata.ColumnNames.Vlid, Me.Vlid)
					End If
				End If

				Return Me._Dacn1AfdxPortCollectionByCollectionName
			End Get
			
			Set(ByVal value As Dacn1AfdxPortCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._Dacn1AfdxPortCollectionByCollectionName Is Nothing Then

					Me.RemovePostSave("Dacn1AfdxPortCollectionByCollectionName")
					Me._Dacn1AfdxPortCollectionByCollectionName = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _Dacn1AfdxPortCollectionByCollectionName As Dacn1AfdxPortCollection
		#End Region

		#Region "UpToDynamicCollectionByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - {ED97B366-C831-4170-A8BC-9CFAEBE1EC61}
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

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "Dacn1AfdxPortCollectionByCollectionName"
					coll = Me.Dacn1AfdxPortCollectionByCollectionName
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "Dacn1AfdxPortCollectionByCollectionName", GetType(Dacn1AfdxPortCollection), New Dacn1AfdxPort()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class Dacn1AfdxVirtualLinkMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.CollectionName, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			c.Description = "EngineModel"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Vlid, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.Vlid
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.ArincChannel, 2, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.ArincChannel
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Arinc Channel A/B"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.IntegrityCheck, 3, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.IntegrityCheck
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.RedondancyEnabled, 4, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.RedondancyEnabled
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.VLMode, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.VLMode
			c.CharacterMaxLength = 25
			c.Description = "Flags for Ch A or B or A and B;Redondancy Enabled and IntegrityCheck"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.VLName, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.VLName
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.NumberOfSubVL, 7, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.NumberOfSubVL
			c.NumericPrecision = 3
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.SubVLID, 8, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.SubVLID
			c.NumericPrecision = 3
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.ExportToCsv, 9, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Description, 10, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.Description
			c.CharacterMaxLength = 25
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.BroadcastType, 11, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.BroadcastType
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "flag in port mode:  Unicast / Multicast"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.TxRx, 12, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.TxRx
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "flag in port mode: Tx/Rx"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.VirtualLinkEnabled, 13, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.VirtualLinkEnabled
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "flag in port mode:  VL Enable"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.FragmentationEnabled, 14, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.FragmentationEnabled
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "flag in port mode:  Fragmentation Enabled"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.ICMPEnabled, 15, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.ICMPEnabled
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "flag in port mode:  ICMP Protocol"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Bag, 16, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.Bag
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.Mtu, 17, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.Mtu
			c.NumericPrecision = 10
			c.Description = "Max Frame Size or Maximum Transmit Unit"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.MaxJitter, 18, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.MaxJitter
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.SkewMax, 19, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.SkewMax
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.SkewDelay, 20, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.SkewDelay
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.PrevSnRange, 21, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.PrevSnRange
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDEEnabled, 22, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDEEnabled
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "flag in port mode:  EDE Protocol"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDESid, 23, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDESid
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDESkewMax, 24, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDESkewMax
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDEAgeMax, 25, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDEAgeMax
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxVirtualLinkMetadata.ColumnNames.EDELagMax, 26, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxVirtualLinkMetadata.PropertyNames.EDELagMax
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As Dacn1AfdxVirtualLinkMetadata
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
			 Public Const ArincChannel As String = "ArincChannel"
			 Public Const IntegrityCheck As String = "IntegrityCheck"
			 Public Const RedondancyEnabled As String = "RedondancyEnabled"
			 Public Const VLMode As String = "VLMode"
			 Public Const VLName As String = "VLName"
			 Public Const NumberOfSubVL As String = "NumberOfSubVL"
			 Public Const SubVLID As String = "SubVLID"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const Description As String = "Description"
			 Public Const BroadcastType As String = "BroadcastType"
			 Public Const TxRx As String = "TxRx"
			 Public Const VirtualLinkEnabled As String = "VirtualLinkEnabled"
			 Public Const FragmentationEnabled As String = "FragmentationEnabled"
			 Public Const ICMPEnabled As String = "ICMPEnabled"
			 Public Const Bag As String = "BAG"
			 Public Const Mtu As String = "MTU"
			 Public Const MaxJitter As String = "MaxJitter"
			 Public Const SkewMax As String = "SkewMax"
			 Public Const SkewDelay As String = "SkewDelay"
			 Public Const PrevSnRange As String = "PrevSnRange"
			 Public Const EDEEnabled As String = "EDEEnabled"
			 Public Const EDESid As String = "EDESid"
			 Public Const EDESkewMax As String = "EDESkewMax"
			 Public Const EDEAgeMax As String = "EDEAgeMax"
			 Public Const EDELagMax As String = "EDELagMax"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const CollectionName As String = "CollectionName"
			 Public Const Vlid As String = "Vlid"
			 Public Const ArincChannel As String = "ArincChannel"
			 Public Const IntegrityCheck As String = "IntegrityCheck"
			 Public Const RedondancyEnabled As String = "RedondancyEnabled"
			 Public Const VLMode As String = "VLMode"
			 Public Const VLName As String = "VLName"
			 Public Const NumberOfSubVL As String = "NumberOfSubVL"
			 Public Const SubVLID As String = "SubVLID"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const Description As String = "Description"
			 Public Const BroadcastType As String = "BroadcastType"
			 Public Const TxRx As String = "TxRx"
			 Public Const VirtualLinkEnabled As String = "VirtualLinkEnabled"
			 Public Const FragmentationEnabled As String = "FragmentationEnabled"
			 Public Const ICMPEnabled As String = "ICMPEnabled"
			 Public Const Bag As String = "Bag"
			 Public Const Mtu As String = "Mtu"
			 Public Const MaxJitter As String = "MaxJitter"
			 Public Const SkewMax As String = "SkewMax"
			 Public Const SkewDelay As String = "SkewDelay"
			 Public Const PrevSnRange As String = "PrevSnRange"
			 Public Const EDEEnabled As String = "EDEEnabled"
			 Public Const EDESid As String = "EDESid"
			 Public Const EDESkewMax As String = "EDESkewMax"
			 Public Const EDEAgeMax As String = "EDEAgeMax"
			 Public Const EDELagMax As String = "EDELagMax"
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
			SyncLock GetType(Dacn1AfdxVirtualLinkMetadata)
			
				If Dacn1AfdxVirtualLinkMetadata.mapDelegates Is Nothing Then
					Dacn1AfdxVirtualLinkMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If Dacn1AfdxVirtualLinkMetadata._meta Is Nothing Then
					Dacn1AfdxVirtualLinkMetadata._meta = New Dacn1AfdxVirtualLinkMetadata()
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
				meta.AddTypeMap("ArincChannel", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("IntegrityCheck", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("RedondancyEnabled", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("VLMode", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("VLName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("NumberOfSubVL", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("SubVLID", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("BroadcastType", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("TxRx", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("VirtualLinkEnabled", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("FragmentationEnabled", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("ICMPEnabled", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("Bag", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Mtu", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MaxJitter", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("SkewMax", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("SkewDelay", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("PrevSnRange", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EDEEnabled", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("EDESid", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EDESkewMax", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EDEAgeMax", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EDELagMax", new esTypeMap("Long", "System.Int32"))			
				
				
				 
				meta.Source = "DACN1_AFDX_VIRTUAL_LINK"
				meta.Destination = "DACN1_AFDX_VIRTUAL_LINK"
				
				meta.spInsert = "proc_DACN1_AFDX_VIRTUAL_LINKInsert"
				meta.spUpdate = "proc_DACN1_AFDX_VIRTUAL_LINKUpdate"
				meta.spDelete = "proc_DACN1_AFDX_VIRTUAL_LINKDelete"
				meta.spLoadAll = "proc_DACN1_AFDX_VIRTUAL_LINKLoadAll"
				meta.spLoadByPrimaryKey = "proc_DACN1_AFDX_VIRTUAL_LINKLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As Dacn1AfdxVirtualLinkMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

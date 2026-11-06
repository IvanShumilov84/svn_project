
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 31/01/2014 10:52:24 AM
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
	' Encapsulates the 'DACN1_AFDX_MESSAGE' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(Dacn1AfdxMessage))> _
	<XmlType("Dacn1AfdxMessage")> _	
	Partial Public Class Dacn1AfdxMessage 
		Inherits esDacn1AfdxMessage
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New Dacn1AfdxMessage()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String)
			Dim obj As New Dacn1AfdxMessage()
			obj.CollectionName = collectionName
			obj.Vlid = vlid
			obj.UdpPortSource = udpPortSource
			obj.UdpPortDestination = udpPortDestination
			obj.FdsName = fdsName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New Dacn1AfdxMessage()
			obj.CollectionName = collectionName
			obj.Vlid = vlid
			obj.UdpPortSource = udpPortSource
			obj.UdpPortDestination = udpPortDestination
			obj.FdsName = fdsName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("Dacn1AfdxMessageCollection")> _
	Partial Public Class Dacn1AfdxMessageCollection
		Inherits esDacn1AfdxMessageCollection
		Implements IEnumerable(Of Dacn1AfdxMessage)
	
		Public Function FindByPrimaryKey(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String) As Dacn1AfdxMessage
			Return MyBase.SingleOrDefault(Function(e) e.CollectionName = collectionName And e.Vlid.HasValue AndAlso e.Vlid.Value = vlid And e.UdpPortSource.HasValue AndAlso e.UdpPortSource.Value = udpPortSource And e.UdpPortDestination.HasValue AndAlso e.UdpPortDestination.Value = udpPortDestination And e.FdsName = fdsName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(Dacn1AfdxMessage))> _
		Public Class Dacn1AfdxMessageCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of Dacn1AfdxMessageCollection)
			
			Public Shared Widening Operator CType(packet As Dacn1AfdxMessageCollectionWCFPacket) As Dacn1AfdxMessageCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As Dacn1AfdxMessageCollection) As Dacn1AfdxMessageCollectionWCFPacket
				Return New Dacn1AfdxMessageCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class Dacn1AfdxMessageQuery 
		Inherits esDacn1AfdxMessageQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "Dacn1AfdxMessageQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As Dacn1AfdxMessageQuery) As String
			Return Dacn1AfdxMessageQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As Dacn1AfdxMessageQuery
			Return DirectCast(Dacn1AfdxMessageQuery.SerializeHelper.FromXml(query, GetType(Dacn1AfdxMessageQuery)), Dacn1AfdxMessageQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDacn1AfdxMessage
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, vlid, udpPortSource, udpPortDestination, fdsName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, vlid, udpPortSource, udpPortDestination, fdsName)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, vlid, udpPortSource, udpPortDestination, fdsName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, vlid, udpPortSource, udpPortDestination, fdsName)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String) As Boolean
		
			Dim query As New Dacn1AfdxMessageQuery()
			query.Where(query.CollectionName = collectionName And query.Vlid = vlid And query.UdpPortSource = udpPortSource And query.UdpPortDestination = udpPortDestination And query.FdsName = fdsName)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("CollectionName", collectionName)
						parms.Add("Vlid", vlid)
						parms.Add("UdpPortSource", udpPortSource)
						parms.Add("UdpPortDestination", udpPortDestination)
						parms.Add("FdsName", fdsName)
			
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
				Return MyBase.GetSystemString(Dacn1AfdxMessageMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxMessageMetadata.ColumnNames.CollectionName, value) Then
					Me._UpToDacn1AfdxPortByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxPortByCollectionName")
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.VLID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Vlid As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.Vlid)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.Vlid, value) Then
					Me._UpToDacn1AfdxPortByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxPortByCollectionName")
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.Vlid)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.UdpPortSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UdpPortSource As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.UdpPortSource)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.UdpPortSource, value) Then
					Me._UpToDacn1AfdxPortByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxPortByCollectionName")
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.UdpPortSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.UdpPortDestination
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UdpPortDestination As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.UdpPortDestination)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.UdpPortDestination, value) Then
					Me._UpToDacn1AfdxPortByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxPortByCollectionName")
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.UdpPortDestination)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.FdsName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FdsName As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxMessageMetadata.ColumnNames.FdsName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxMessageMetadata.ColumnNames.FdsName, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.FdsName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1AfdxMessageMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1AfdxMessageMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' flag in port mode: Tx/Rx
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TxRx As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(Dacn1AfdxMessageMetadata.ColumnNames.TxRx)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(Dacn1AfdxMessageMetadata.ColumnNames.TxRx, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.TxRx)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.FdsDescription
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FdsDescription As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxMessageMetadata.ColumnNames.FdsDescription)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxMessageMetadata.ColumnNames.FdsDescription, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.FdsDescription)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.FdsAddress
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FdsAddress As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.FdsAddress)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.FdsAddress, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.FdsAddress)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.FdsLength
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FdsLength As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.FdsLength)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.FdsLength, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.FdsLength)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.SignalNumberOfBit_OLD
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SignalNumberOfBitOLD As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.SignalNumberOfBitOLD)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.SignalNumberOfBitOLD, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.SignalNumberOfBitOLD)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.SignalAddress_OLD
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SignalAddressOLD As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.SignalAddressOLD)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.SignalAddressOLD, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.SignalAddressOLD)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.FsName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FsName As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxMessageMetadata.ColumnNames.FsName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxMessageMetadata.ColumnNames.FsName, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.FsName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.FsDescription
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FsDescription As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxMessageMetadata.ColumnNames.FsDescription)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxMessageMetadata.ColumnNames.FsDescription, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.FsDescription)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_MESSAGE.FsAddress
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FsAddress As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.FsAddress)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxMessageMetadata.ColumnNames.FsAddress, value) Then
					OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.FsAddress)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToDacn1AfdxPortByCollectionName As Dacn1AfdxPort
		
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
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "TxRx"
							Me.str().TxRx = CType(value, string)
												
						Case "FdsDescription"
							Me.str().FdsDescription = CType(value, string)
												
						Case "FdsAddress"
							Me.str().FdsAddress = CType(value, string)
												
						Case "FdsLength"
							Me.str().FdsLength = CType(value, string)
												
						Case "SignalNumberOfBitOLD"
							Me.str().SignalNumberOfBitOLD = CType(value, string)
												
						Case "SignalAddressOLD"
							Me.str().SignalAddressOLD = CType(value, string)
												
						Case "FsName"
							Me.str().FsName = CType(value, string)
												
						Case "FsDescription"
							Me.str().FsDescription = CType(value, string)
												
						Case "FsAddress"
							Me.str().FsAddress = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Vlid"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Vlid = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.Vlid)
							End If
						
						Case "UdpPortSource"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.UdpPortSource = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.UdpPortSource)
							End If
						
						Case "UdpPortDestination"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.UdpPortDestination = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.UdpPortDestination)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "TxRx"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.TxRx = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.TxRx)
							End If
						
						Case "FdsAddress"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.FdsAddress = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.FdsAddress)
							End If
						
						Case "FdsLength"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.FdsLength = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.FdsLength)
							End If
						
						Case "SignalNumberOfBitOLD"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SignalNumberOfBitOLD = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.SignalNumberOfBitOLD)
							End If
						
						Case "SignalAddressOLD"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SignalAddressOLD = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.SignalAddressOLD)
							End If
						
						Case "FsAddress"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.FsAddress = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxMessageMetadata.PropertyNames.FsAddress)
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
		
			Public Sub New(ByVal entity As esDacn1AfdxMessage)
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
		  	
			Public Property FdsDescription As System.String 
				Get
					Dim data_ As System.String = entity.FdsDescription
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FdsDescription = Nothing
					Else
						entity.FdsDescription = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property FdsAddress As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.FdsAddress
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FdsAddress = Nothing
					Else
						entity.FdsAddress = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property FdsLength As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.FdsLength
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FdsLength = Nothing
					Else
						entity.FdsLength = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property SignalNumberOfBitOLD As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SignalNumberOfBitOLD
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SignalNumberOfBitOLD = Nothing
					Else
						entity.SignalNumberOfBitOLD = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property SignalAddressOLD As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SignalAddressOLD
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SignalAddressOLD = Nothing
					Else
						entity.SignalAddressOLD = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property FsName As System.String 
				Get
					Dim data_ As System.String = entity.FsName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FsName = Nothing
					Else
						entity.FsName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property FsDescription As System.String 
				Get
					Dim data_ As System.String = entity.FsDescription
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FsDescription = Nothing
					Else
						entity.FsDescription = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property FsAddress As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.FsAddress
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FsAddress = Nothing
					Else
						entity.FsAddress = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  

			Private entity As esDacn1AfdxMessage
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1AfdxMessageMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As Dacn1AfdxMessageQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1AfdxMessageQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As Dacn1AfdxMessageQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As Dacn1AfdxMessageQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As Dacn1AfdxMessageQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDacn1AfdxMessageCollection
		Inherits esEntityCollection(Of Dacn1AfdxMessage)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1AfdxMessageMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "Dacn1AfdxMessageCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As Dacn1AfdxMessageQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1AfdxMessageQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As Dacn1AfdxMessageQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New Dacn1AfdxMessageQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As Dacn1AfdxMessageQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, Dacn1AfdxMessageQuery))
		End Sub
		
		#End Region
						
		Private m_query As Dacn1AfdxMessageQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDacn1AfdxMessageQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return Dacn1AfdxMessageMetadata.Meta()
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
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "TxRx" 
					Return Me.TxRx
				Case "FdsDescription" 
					Return Me.FdsDescription
				Case "FdsAddress" 
					Return Me.FdsAddress
				Case "FdsLength" 
					Return Me.FdsLength
				Case "SignalNumberOfBitOLD" 
					Return Me.SignalNumberOfBitOLD
				Case "SignalAddressOLD" 
					Return Me.SignalAddressOLD
				Case "FsName" 
					Return Me.FsName
				Case "FsDescription" 
					Return Me.FsDescription
				Case "FsAddress" 
					Return Me.FsAddress
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Vlid As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.Vlid, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property UdpPortSource As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.UdpPortSource, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property UdpPortDestination As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.UdpPortDestination, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property FdsName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.FdsName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property TxRx As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.TxRx, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property FdsDescription As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.FdsDescription, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FdsAddress As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.FdsAddress, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property FdsLength As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.FdsLength, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property SignalNumberOfBitOLD As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.SignalNumberOfBitOLD, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property SignalAddressOLD As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.SignalAddressOLD, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property FsName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.FsName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FsDescription As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.FsDescription, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FsAddress As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxMessageMetadata.ColumnNames.FsAddress, esSystemType.Int32)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class Dacn1AfdxMessage 
		Inherits esDacn1AfdxMessage
		
	
		#Region "Dacn1AfdxSignalCollectionByCollectionName - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_Dacn1AfdxSignalCollectionByCollectionName() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Dacn1AfdxMessage.Dacn1AfdxSignalCollectionByCollectionName_Delegate)
				map.PropertyName = "Dacn1AfdxSignalCollectionByCollectionName"
				map.MyColumnName = "CollectionName,VLID,UdpPortSource,UdpPortDestination,FdsName"
				map.ParentColumnName = "CollectionName,VLID,UdpPortSource,UdpPortDestination,FdsName"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub Dacn1AfdxSignalCollectionByCollectionName_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New Dacn1AfdxMessageQuery(data.NextAlias())
			
			Dim mee As Dacn1AfdxSignalQuery = If(data.You IsNot Nothing, TryCast(data.You, Dacn1AfdxSignalQuery), New Dacn1AfdxSignalQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.CollectionName = mee.CollectionName And parent.Vlid = mee.Vlid And parent.UdpPortSource = mee.UdpPortSource And parent.UdpPortDestination = mee.UdpPortDestination And parent.FdsName = mee.FdsName)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - DACN1_AFDX_MESSAGEDACN1_AFDX_DISCRETE
		''' </summary>

		<XmlIgnore()> _ 
		Public Property Dacn1AfdxSignalCollectionByCollectionName As Dacn1AfdxSignalCollection 
		
			Get
				If Me._Dacn1AfdxSignalCollectionByCollectionName Is Nothing Then
					Me._Dacn1AfdxSignalCollectionByCollectionName = New Dacn1AfdxSignalCollection()
					Me._Dacn1AfdxSignalCollectionByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("Dacn1AfdxSignalCollectionByCollectionName", Me._Dacn1AfdxSignalCollectionByCollectionName)
				
					If Not Me.CollectionName.Equals(Nothing) AndAlso Not Me.Vlid.Equals(Nothing) AndAlso Not Me.UdpPortSource.Equals(Nothing) AndAlso Not Me.UdpPortDestination.Equals(Nothing) AndAlso Not Me.FdsName.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._Dacn1AfdxSignalCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalCollectionByCollectionName.Query.CollectionName = Me.CollectionName)
							Me._Dacn1AfdxSignalCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalCollectionByCollectionName.Query.Vlid = Me.Vlid)
							Me._Dacn1AfdxSignalCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalCollectionByCollectionName.Query.UdpPortSource = Me.UdpPortSource)
							Me._Dacn1AfdxSignalCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalCollectionByCollectionName.Query.UdpPortDestination = Me.UdpPortDestination)
							Me._Dacn1AfdxSignalCollectionByCollectionName.Query.Where(Me._Dacn1AfdxSignalCollectionByCollectionName.Query.FdsName = Me.FdsName)
							Me._Dacn1AfdxSignalCollectionByCollectionName.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._Dacn1AfdxSignalCollectionByCollectionName.fks.Add(Dacn1AfdxSignalMetadata.ColumnNames.CollectionName, Me.CollectionName)
						Me._Dacn1AfdxSignalCollectionByCollectionName.fks.Add(Dacn1AfdxSignalMetadata.ColumnNames.Vlid, Me.Vlid)
						Me._Dacn1AfdxSignalCollectionByCollectionName.fks.Add(Dacn1AfdxSignalMetadata.ColumnNames.UdpPortSource, Me.UdpPortSource)
						Me._Dacn1AfdxSignalCollectionByCollectionName.fks.Add(Dacn1AfdxSignalMetadata.ColumnNames.UdpPortDestination, Me.UdpPortDestination)
						Me._Dacn1AfdxSignalCollectionByCollectionName.fks.Add(Dacn1AfdxSignalMetadata.ColumnNames.FdsName, Me.FdsName)
					End If
				End If

				Return Me._Dacn1AfdxSignalCollectionByCollectionName
			End Get
			
			Set(ByVal value As Dacn1AfdxSignalCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._Dacn1AfdxSignalCollectionByCollectionName Is Nothing Then

					Me.RemovePostSave("Dacn1AfdxSignalCollectionByCollectionName")
					Me._Dacn1AfdxSignalCollectionByCollectionName = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _Dacn1AfdxSignalCollectionByCollectionName As Dacn1AfdxSignalCollection
		#End Region

		#Region "UpToDacn1AfdxPortByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - DACN1_AFDX_PORTDACN1_AFDX_MESSAGE
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToDacn1AfdxPortByCollectionName As Dacn1AfdxPort
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToDacn1AfdxPortByCollectionName Is Nothing _
						 AndAlso Not CollectionName.Equals(Nothing)  AndAlso Not Vlid.Equals(Nothing)  AndAlso Not UdpPortSource.Equals(Nothing)  AndAlso Not UdpPortDestination.Equals(Nothing)  Then
						
					Me._UpToDacn1AfdxPortByCollectionName = New Dacn1AfdxPort()
					Me._UpToDacn1AfdxPortByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToDacn1AfdxPortByCollectionName", Me._UpToDacn1AfdxPortByCollectionName)
					Me._UpToDacn1AfdxPortByCollectionName.Query.Where(Me._UpToDacn1AfdxPortByCollectionName.Query.CollectionName = Me.CollectionName)
					Me._UpToDacn1AfdxPortByCollectionName.Query.Where(Me._UpToDacn1AfdxPortByCollectionName.Query.Vlid = Me.Vlid)
					Me._UpToDacn1AfdxPortByCollectionName.Query.Where(Me._UpToDacn1AfdxPortByCollectionName.Query.UdpPortSource = Me.UdpPortSource)
					Me._UpToDacn1AfdxPortByCollectionName.Query.Where(Me._UpToDacn1AfdxPortByCollectionName.Query.UdpPortDestination = Me.UdpPortDestination)
					Me._UpToDacn1AfdxPortByCollectionName.Query.Load()
				End If

				Return Me._UpToDacn1AfdxPortByCollectionName
			End Get
			
            Set(ByVal value As Dacn1AfdxPort)
				Me.RemovePreSave("UpToDacn1AfdxPortByCollectionName")
				

				If value Is Nothing Then
				
					Me.CollectionName = Nothing
				
					Me.Vlid = Nothing
				
					Me.UdpPortSource = Nothing
				
					Me.UdpPortDestination = Nothing
				
					Me._UpToDacn1AfdxPortByCollectionName = Nothing
				Else
				
					Me.CollectionName = value.CollectionName
					
					Me.Vlid = value.Vlid
					
					Me.UdpPortSource = value.UdpPortSource
					
					Me.UdpPortDestination = value.UdpPortDestination
					
					Me._UpToDacn1AfdxPortByCollectionName = value
					Me.SetPreSave("UpToDacn1AfdxPortByCollectionName", Me._UpToDacn1AfdxPortByCollectionName)
				End If
				
			End Set	

		End Property
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "Dacn1AfdxSignalCollectionByCollectionName"
					coll = Me.Dacn1AfdxSignalCollectionByCollectionName
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "Dacn1AfdxSignalCollectionByCollectionName", GetType(Dacn1AfdxSignalCollection), New Dacn1AfdxSignal()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class Dacn1AfdxMessageMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.CollectionName, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			c.Description = "EngineModel"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.Vlid, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.Vlid
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.UdpPortSource, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.UdpPortSource
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.UdpPortDestination, 3, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.UdpPortDestination
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.FdsName, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.FdsName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.ExportToCsv, 5, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.TxRx, 6, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.TxRx
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "flag in port mode: Tx/Rx"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.FdsDescription, 7, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.FdsDescription
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.FdsAddress, 8, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.FdsAddress
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.FdsLength, 9, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.FdsLength
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.SignalNumberOfBitOLD, 10, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.SignalNumberOfBitOLD
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.SignalAddressOLD, 11, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.SignalAddressOLD
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.FsName, 12, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.FsName
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.FsDescription, 13, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.FsDescription
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxMessageMetadata.ColumnNames.FsAddress, 14, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxMessageMetadata.PropertyNames.FsAddress
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As Dacn1AfdxMessageMetadata
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
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const TxRx As String = "TxRx"
			 Public Const FdsDescription As String = "FdsDescription"
			 Public Const FdsAddress As String = "FdsAddress"
			 Public Const FdsLength As String = "FdsLength"
			 Public Const SignalNumberOfBitOLD As String = "SignalNumberOfBit_OLD"
			 Public Const SignalAddressOLD As String = "SignalAddress_OLD"
			 Public Const FsName As String = "FsName"
			 Public Const FsDescription As String = "FsDescription"
			 Public Const FsAddress As String = "FsAddress"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const CollectionName As String = "CollectionName"
			 Public Const Vlid As String = "Vlid"
			 Public Const UdpPortSource As String = "UdpPortSource"
			 Public Const UdpPortDestination As String = "UdpPortDestination"
			 Public Const FdsName As String = "FdsName"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const TxRx As String = "TxRx"
			 Public Const FdsDescription As String = "FdsDescription"
			 Public Const FdsAddress As String = "FdsAddress"
			 Public Const FdsLength As String = "FdsLength"
			 Public Const SignalNumberOfBitOLD As String = "SignalNumberOfBitOLD"
			 Public Const SignalAddressOLD As String = "SignalAddressOLD"
			 Public Const FsName As String = "FsName"
			 Public Const FsDescription As String = "FsDescription"
			 Public Const FsAddress As String = "FsAddress"
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
			SyncLock GetType(Dacn1AfdxMessageMetadata)
			
				If Dacn1AfdxMessageMetadata.mapDelegates Is Nothing Then
					Dacn1AfdxMessageMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If Dacn1AfdxMessageMetadata._meta Is Nothing Then
					Dacn1AfdxMessageMetadata._meta = New Dacn1AfdxMessageMetadata()
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
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("TxRx", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("FdsDescription", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("FdsAddress", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("FdsLength", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("SignalNumberOfBitOLD", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("SignalAddressOLD", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("FsName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("FsDescription", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("FsAddress", new esTypeMap("Long", "System.Int32"))			
				
				
				 
				meta.Source = "DACN1_AFDX_MESSAGE"
				meta.Destination = "DACN1_AFDX_MESSAGE"
				
				meta.spInsert = "proc_DACN1_AFDX_MESSAGEInsert"
				meta.spUpdate = "proc_DACN1_AFDX_MESSAGEUpdate"
				meta.spDelete = "proc_DACN1_AFDX_MESSAGEDelete"
				meta.spLoadAll = "proc_DACN1_AFDX_MESSAGELoadAll"
				meta.spLoadByPrimaryKey = "proc_DACN1_AFDX_MESSAGELoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As Dacn1AfdxMessageMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace


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
	' Encapsulates the 'DACN1_AFDX_SIGNAL_ENUMERATION' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(Dacn1AfdxSignalEnumeration))> _
	<XmlType("Dacn1AfdxSignalEnumeration")> _	
	Partial Public Class Dacn1AfdxSignalEnumeration 
		Inherits esDacn1AfdxSignalEnumeration
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New Dacn1AfdxSignalEnumeration()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String, ByVal associatedStateValue As System.Int32)
			Dim obj As New Dacn1AfdxSignalEnumeration()
			obj.CollectionName = collectionName
			obj.Vlid = vlid
			obj.UdpPortSource = udpPortSource
			obj.UdpPortDestination = udpPortDestination
			obj.FdsName = fdsName
			obj.ParameterName = parameterName
			obj.AssociatedStateValue = associatedStateValue
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String, ByVal associatedStateValue As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New Dacn1AfdxSignalEnumeration()
			obj.CollectionName = collectionName
			obj.Vlid = vlid
			obj.UdpPortSource = udpPortSource
			obj.UdpPortDestination = udpPortDestination
			obj.FdsName = fdsName
			obj.ParameterName = parameterName
			obj.AssociatedStateValue = associatedStateValue
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("Dacn1AfdxSignalEnumerationCollection")> _
	Partial Public Class Dacn1AfdxSignalEnumerationCollection
		Inherits esDacn1AfdxSignalEnumerationCollection
		Implements IEnumerable(Of Dacn1AfdxSignalEnumeration)
	
		Public Function FindByPrimaryKey(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String, ByVal associatedStateValue As System.Int32) As Dacn1AfdxSignalEnumeration
			Return MyBase.SingleOrDefault(Function(e) e.CollectionName = collectionName And e.Vlid.HasValue AndAlso e.Vlid.Value = vlid And e.UdpPortSource.HasValue AndAlso e.UdpPortSource.Value = udpPortSource And e.UdpPortDestination.HasValue AndAlso e.UdpPortDestination.Value = udpPortDestination And e.FdsName = fdsName And e.ParameterName = parameterName And e.AssociatedStateValue.HasValue AndAlso e.AssociatedStateValue.Value = associatedStateValue)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(Dacn1AfdxSignalEnumeration))> _
		Public Class Dacn1AfdxSignalEnumerationCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of Dacn1AfdxSignalEnumerationCollection)
			
			Public Shared Widening Operator CType(packet As Dacn1AfdxSignalEnumerationCollectionWCFPacket) As Dacn1AfdxSignalEnumerationCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As Dacn1AfdxSignalEnumerationCollection) As Dacn1AfdxSignalEnumerationCollectionWCFPacket
				Return New Dacn1AfdxSignalEnumerationCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class Dacn1AfdxSignalEnumerationQuery 
		Inherits esDacn1AfdxSignalEnumerationQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "Dacn1AfdxSignalEnumerationQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As Dacn1AfdxSignalEnumerationQuery) As String
			Return Dacn1AfdxSignalEnumerationQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As Dacn1AfdxSignalEnumerationQuery
			Return DirectCast(Dacn1AfdxSignalEnumerationQuery.SerializeHelper.FromXml(query, GetType(Dacn1AfdxSignalEnumerationQuery)), Dacn1AfdxSignalEnumerationQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDacn1AfdxSignalEnumeration
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String, ByVal associatedStateValue As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, vlid, udpPortSource, udpPortDestination, fdsName, parameterName, associatedStateValue)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, vlid, udpPortSource, udpPortDestination, fdsName, parameterName, associatedStateValue)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String, ByVal associatedStateValue As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(collectionName, vlid, udpPortSource, udpPortDestination, fdsName, parameterName, associatedStateValue)
			Else
				Return LoadByPrimaryKeyStoredProcedure(collectionName, vlid, udpPortSource, udpPortDestination, fdsName, parameterName, associatedStateValue)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String, ByVal associatedStateValue As System.Int32) As Boolean
		
			Dim query As New Dacn1AfdxSignalEnumerationQuery()
			query.Where(query.CollectionName = collectionName And query.Vlid = vlid And query.UdpPortSource = udpPortSource And query.UdpPortDestination = udpPortDestination And query.FdsName = fdsName And query.ParameterName = parameterName And query.AssociatedStateValue = associatedStateValue)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal collectionName As System.String, ByVal vlid As System.Int32, ByVal udpPortSource As System.Int32, ByVal udpPortDestination As System.Int32, ByVal fdsName As System.String, ByVal parameterName As System.String, ByVal associatedStateValue As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("CollectionName", collectionName)
						parms.Add("Vlid", vlid)
						parms.Add("UdpPortSource", udpPortSource)
						parms.Add("UdpPortDestination", udpPortDestination)
						parms.Add("FdsName", fdsName)
						parms.Add("ParameterName", parameterName)
						parms.Add("AssociatedStateValue", associatedStateValue)
			
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
				Return MyBase.GetSystemString(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.CollectionName, value) Then
					Me._UpToDacn1AfdxSignalByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxSignalByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL_ENUMERATION.VLID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Vlid As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.Vlid)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.Vlid, value) Then
					Me._UpToDacn1AfdxSignalByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxSignalByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.Vlid)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL_ENUMERATION.UdpPortSource
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UdpPortSource As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.UdpPortSource)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.UdpPortSource, value) Then
					Me._UpToDacn1AfdxSignalByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxSignalByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.UdpPortSource)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL_ENUMERATION.UdpPortDestination
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UdpPortDestination As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.UdpPortDestination)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.UdpPortDestination, value) Then
					Me._UpToDacn1AfdxSignalByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxSignalByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.UdpPortDestination)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL_ENUMERATION.FdsName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FdsName As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.FdsName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.FdsName, value) Then
					Me._UpToDacn1AfdxSignalByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxSignalByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.FdsName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL_ENUMERATION.ParameterName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ParameterName As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.ParameterName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.ParameterName, value) Then
					Me._UpToDacn1AfdxSignalByCollectionName = Nothing
					Me.OnPropertyChanged("UpToDacn1AfdxSignalByCollectionName")
					OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.ParameterName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL_ENUMERATION.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL_ENUMERATION.AssociatedStateValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AssociatedStateValue As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.AssociatedStateValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.AssociatedStateValue, value) Then
					OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.AssociatedStateValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL_ENUMERATION.AssociatedStateDefinition
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property AssociatedStateDefinition As System.String
			Get
				Return MyBase.GetSystemString(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.AssociatedStateDefinition)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.AssociatedStateDefinition, value) Then
					OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.AssociatedStateDefinition)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN1_AFDX_SIGNAL_ENUMERATION.DisplayOn
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DisplayOn As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.DisplayOn)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.DisplayOn, value) Then
					OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.DisplayOn)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToDacn1AfdxSignalByCollectionName As Dacn1AfdxSignal
		
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
												
						Case "AssociatedStateValue"
							Me.str().AssociatedStateValue = CType(value, string)
												
						Case "AssociatedStateDefinition"
							Me.str().AssociatedStateDefinition = CType(value, string)
												
						Case "DisplayOn"
							Me.str().DisplayOn = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Vlid"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Vlid = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.Vlid)
							End If
						
						Case "UdpPortSource"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.UdpPortSource = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.UdpPortSource)
							End If
						
						Case "UdpPortDestination"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.UdpPortDestination = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.UdpPortDestination)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "AssociatedStateValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.AssociatedStateValue = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.AssociatedStateValue)
							End If
						
						Case "DisplayOn"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.DisplayOn = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(Dacn1AfdxSignalEnumerationMetadata.PropertyNames.DisplayOn)
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
		
			Public Sub New(ByVal entity As esDacn1AfdxSignalEnumeration)
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
		  	
			Public Property AssociatedStateValue As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.AssociatedStateValue
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AssociatedStateValue = Nothing
					Else
						entity.AssociatedStateValue = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property AssociatedStateDefinition As System.String 
				Get
					Dim data_ As System.String = entity.AssociatedStateDefinition
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.AssociatedStateDefinition = Nothing
					Else
						entity.AssociatedStateDefinition = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DisplayOn As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.DisplayOn
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DisplayOn = Nothing
					Else
						entity.DisplayOn = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  

			Private entity As esDacn1AfdxSignalEnumeration
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1AfdxSignalEnumerationMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As Dacn1AfdxSignalEnumerationQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1AfdxSignalEnumerationQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As Dacn1AfdxSignalEnumerationQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As Dacn1AfdxSignalEnumerationQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As Dacn1AfdxSignalEnumerationQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDacn1AfdxSignalEnumerationCollection
		Inherits esEntityCollection(Of Dacn1AfdxSignalEnumeration)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return Dacn1AfdxSignalEnumerationMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "Dacn1AfdxSignalEnumerationCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As Dacn1AfdxSignalEnumerationQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New Dacn1AfdxSignalEnumerationQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As Dacn1AfdxSignalEnumerationQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New Dacn1AfdxSignalEnumerationQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As Dacn1AfdxSignalEnumerationQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, Dacn1AfdxSignalEnumerationQuery))
		End Sub
		
		#End Region
						
		Private m_query As Dacn1AfdxSignalEnumerationQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDacn1AfdxSignalEnumerationQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return Dacn1AfdxSignalEnumerationMetadata.Meta()
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
				Case "AssociatedStateValue" 
					Return Me.AssociatedStateValue
				Case "AssociatedStateDefinition" 
					Return Me.AssociatedStateDefinition
				Case "DisplayOn" 
					Return Me.DisplayOn
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalEnumerationMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Vlid As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalEnumerationMetadata.ColumnNames.Vlid, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property UdpPortSource As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalEnumerationMetadata.ColumnNames.UdpPortSource, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property UdpPortDestination As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalEnumerationMetadata.ColumnNames.UdpPortDestination, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property FdsName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalEnumerationMetadata.ColumnNames.FdsName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ParameterName As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalEnumerationMetadata.ColumnNames.ParameterName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalEnumerationMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property AssociatedStateValue As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalEnumerationMetadata.ColumnNames.AssociatedStateValue, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property AssociatedStateDefinition As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalEnumerationMetadata.ColumnNames.AssociatedStateDefinition, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DisplayOn As esQueryItem
			Get
				Return New esQueryItem(Me, Dacn1AfdxSignalEnumerationMetadata.ColumnNames.DisplayOn, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class Dacn1AfdxSignalEnumeration 
		Inherits esDacn1AfdxSignalEnumeration
		
	
		#Region "UpToDacn1AfdxSignalByCollectionName - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - DACN1_AFDX_SIGNALDACN1_AFDX_SIGNAL_ENUMERATION
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToDacn1AfdxSignalByCollectionName As Dacn1AfdxSignal
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToDacn1AfdxSignalByCollectionName Is Nothing _
						 AndAlso Not CollectionName.Equals(Nothing)  AndAlso Not Vlid.Equals(Nothing)  AndAlso Not UdpPortSource.Equals(Nothing)  AndAlso Not UdpPortDestination.Equals(Nothing)  AndAlso Not FdsName.Equals(Nothing)  AndAlso Not ParameterName.Equals(Nothing)  Then
						
					Me._UpToDacn1AfdxSignalByCollectionName = New Dacn1AfdxSignal()
					Me._UpToDacn1AfdxSignalByCollectionName.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToDacn1AfdxSignalByCollectionName", Me._UpToDacn1AfdxSignalByCollectionName)
					Me._UpToDacn1AfdxSignalByCollectionName.Query.Where(Me._UpToDacn1AfdxSignalByCollectionName.Query.CollectionName = Me.CollectionName)
					Me._UpToDacn1AfdxSignalByCollectionName.Query.Where(Me._UpToDacn1AfdxSignalByCollectionName.Query.Vlid = Me.Vlid)
					Me._UpToDacn1AfdxSignalByCollectionName.Query.Where(Me._UpToDacn1AfdxSignalByCollectionName.Query.UdpPortSource = Me.UdpPortSource)
					Me._UpToDacn1AfdxSignalByCollectionName.Query.Where(Me._UpToDacn1AfdxSignalByCollectionName.Query.UdpPortDestination = Me.UdpPortDestination)
					Me._UpToDacn1AfdxSignalByCollectionName.Query.Where(Me._UpToDacn1AfdxSignalByCollectionName.Query.FdsName = Me.FdsName)
					Me._UpToDacn1AfdxSignalByCollectionName.Query.Where(Me._UpToDacn1AfdxSignalByCollectionName.Query.ParameterName = Me.ParameterName)
					Me._UpToDacn1AfdxSignalByCollectionName.Query.Load()
				End If

				Return Me._UpToDacn1AfdxSignalByCollectionName
			End Get
			
            Set(ByVal value As Dacn1AfdxSignal)
				Me.RemovePreSave("UpToDacn1AfdxSignalByCollectionName")
				

				If value Is Nothing Then
				
					Me.CollectionName = Nothing
				
					Me.Vlid = Nothing
				
					Me.UdpPortSource = Nothing
				
					Me.UdpPortDestination = Nothing
				
					Me.FdsName = Nothing
				
					Me.ParameterName = Nothing
				
					Me._UpToDacn1AfdxSignalByCollectionName = Nothing
				Else
				
					Me.CollectionName = value.CollectionName
					
					Me.Vlid = value.Vlid
					
					Me.UdpPortSource = value.UdpPortSource
					
					Me.UdpPortDestination = value.UdpPortDestination
					
					Me.FdsName = value.FdsName
					
					Me.ParameterName = value.ParameterName
					
					Me._UpToDacn1AfdxSignalByCollectionName = value
					Me.SetPreSave("UpToDacn1AfdxSignalByCollectionName", Me._UpToDacn1AfdxSignalByCollectionName)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class Dacn1AfdxSignalEnumerationMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.CollectionName, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxSignalEnumerationMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			c.Description = "EngineModel"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.Vlid, 1, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalEnumerationMetadata.PropertyNames.Vlid
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.UdpPortSource, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalEnumerationMetadata.PropertyNames.UdpPortSource
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.UdpPortDestination, 3, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalEnumerationMetadata.PropertyNames.UdpPortDestination
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.FdsName, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxSignalEnumerationMetadata.PropertyNames.FdsName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.ParameterName, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxSignalEnumerationMetadata.PropertyNames.ParameterName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.ExportToCsv, 6, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1AfdxSignalEnumerationMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.AssociatedStateValue, 7, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = Dacn1AfdxSignalEnumerationMetadata.PropertyNames.AssociatedStateValue
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.AssociatedStateDefinition, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = Dacn1AfdxSignalEnumerationMetadata.PropertyNames.AssociatedStateDefinition
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(Dacn1AfdxSignalEnumerationMetadata.ColumnNames.DisplayOn, 9, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = Dacn1AfdxSignalEnumerationMetadata.PropertyNames.DisplayOn
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As Dacn1AfdxSignalEnumerationMetadata
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
			 Public Const AssociatedStateValue As String = "AssociatedStateValue"
			 Public Const AssociatedStateDefinition As String = "AssociatedStateDefinition"
			 Public Const DisplayOn As String = "DisplayOn"
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
			 Public Const AssociatedStateValue As String = "AssociatedStateValue"
			 Public Const AssociatedStateDefinition As String = "AssociatedStateDefinition"
			 Public Const DisplayOn As String = "DisplayOn"
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
			SyncLock GetType(Dacn1AfdxSignalEnumerationMetadata)
			
				If Dacn1AfdxSignalEnumerationMetadata.mapDelegates Is Nothing Then
					Dacn1AfdxSignalEnumerationMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If Dacn1AfdxSignalEnumerationMetadata._meta Is Nothing Then
					Dacn1AfdxSignalEnumerationMetadata._meta = New Dacn1AfdxSignalEnumerationMetadata()
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
				meta.AddTypeMap("AssociatedStateValue", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("AssociatedStateDefinition", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DisplayOn", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "DACN1_AFDX_SIGNAL_ENUMERATION"
				meta.Destination = "DACN1_AFDX_SIGNAL_ENUMERATION"
				
				meta.spInsert = "proc_DACN1_AFDX_SIGNAL_ENUMERATIONInsert"
				meta.spUpdate = "proc_DACN1_AFDX_SIGNAL_ENUMERATIONUpdate"
				meta.spDelete = "proc_DACN1_AFDX_SIGNAL_ENUMERATIONDelete"
				meta.spLoadAll = "proc_DACN1_AFDX_SIGNAL_ENUMERATIONLoadAll"
				meta.spLoadByPrimaryKey = "proc_DACN1_AFDX_SIGNAL_ENUMERATIONLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As Dacn1AfdxSignalEnumerationMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

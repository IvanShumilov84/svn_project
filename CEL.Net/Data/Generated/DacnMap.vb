
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 11/30/2012 11:48:07 AM
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
	' Encapsulates the 'DACN_MAP' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(DacnMap))> _
	<XmlType("DacnMap")> _	
	Partial Public Class DacnMap 
		Inherits esDacnMap
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New DacnMap()
		End Function
		
		#Region "Static Quick Access Methods"
		
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("DacnMapCollection")> _
	Partial Public Class DacnMapCollection
		Inherits esDacnMapCollection
		Implements IEnumerable(Of DacnMap)
	


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(DacnMap))> _
		Public Class DacnMapCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of DacnMapCollection)
			
			Public Shared Widening Operator CType(packet As DacnMapCollectionWCFPacket) As DacnMapCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As DacnMapCollection) As DacnMapCollectionWCFPacket
				Return New DacnMapCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class DacnMapQuery 
		Inherits esDacnMapQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "DacnMapQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As DacnMapQuery) As String
			Return DacnMapQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As DacnMapQuery
			Return DirectCast(DacnMapQuery.SerializeHelper.FromXml(query, GetType(DacnMapQuery)), DacnMapQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esDacnMap
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
		
			Dim query As New DacnMapQuery()
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
		' Maps to DACN_MAP.DACN
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Dacn As System.String
			Get
				Return MyBase.GetSystemString(DacnMapMetadata.ColumnNames.Dacn)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnMapMetadata.ColumnNames.Dacn, value) Then
					OnPropertyChanged(DacnMapMetadata.PropertyNames.Dacn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN_MAP.DACL
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Dacl As System.String
			Get
				Return MyBase.GetSystemString(DacnMapMetadata.ColumnNames.Dacl)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnMapMetadata.ColumnNames.Dacl, value) Then
					OnPropertyChanged(DacnMapMetadata.PropertyNames.Dacl)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN_MAP.DACL Real
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DACLReal As System.String
			Get
				Return MyBase.GetSystemString(DacnMapMetadata.ColumnNames.DACLReal)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnMapMetadata.ColumnNames.DACLReal, value) Then
					OnPropertyChanged(DacnMapMetadata.PropertyNames.DACLReal)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN_MAP.DACL Bool
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DACLBool As System.String
			Get
				Return MyBase.GetSystemString(DacnMapMetadata.ColumnNames.DACLBool)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnMapMetadata.ColumnNames.DACLBool, value) Then
					OnPropertyChanged(DacnMapMetadata.PropertyNames.DACLBool)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN_MAP.DACL MultiState
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DACLMultiState As System.String
			Get
				Return MyBase.GetSystemString(DacnMapMetadata.ColumnNames.DACLMultiState)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnMapMetadata.ColumnNames.DACLMultiState, value) Then
					OnPropertyChanged(DacnMapMetadata.PropertyNames.DACLMultiState)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN_MAP.DACL Text
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DACLText As System.String
			Get
				Return MyBase.GetSystemString(DacnMapMetadata.ColumnNames.DACLText)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnMapMetadata.ColumnNames.DACLText, value) Then
					OnPropertyChanged(DacnMapMetadata.PropertyNames.DACLText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN_MAP.DACL TMS
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property DaclTms As System.String
			Get
				Return MyBase.GetSystemString(DacnMapMetadata.ColumnNames.DaclTms)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(DacnMapMetadata.ColumnNames.DaclTms, value) Then
					OnPropertyChanged(DacnMapMetadata.PropertyNames.DaclTms)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN_MAP.PreTrigRecord
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PreTrigRecord As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(DacnMapMetadata.ColumnNames.PreTrigRecord)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(DacnMapMetadata.ColumnNames.PreTrigRecord, value) Then
					OnPropertyChanged(DacnMapMetadata.PropertyNames.PreTrigRecord)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to DACN_MAP.MaxRecord
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MaxRecord As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(DacnMapMetadata.ColumnNames.MaxRecord)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(DacnMapMetadata.ColumnNames.MaxRecord, value) Then
					OnPropertyChanged(DacnMapMetadata.PropertyNames.MaxRecord)
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
												
						Case "Dacn"
							Me.str().Dacn = CType(value, string)
												
						Case "Dacl"
							Me.str().Dacl = CType(value, string)
												
						Case "DACLReal"
							Me.str().DACLReal = CType(value, string)
												
						Case "DACLBool"
							Me.str().DACLBool = CType(value, string)
												
						Case "DACLMultiState"
							Me.str().DACLMultiState = CType(value, string)
												
						Case "DACLText"
							Me.str().DACLText = CType(value, string)
												
						Case "DaclTms"
							Me.str().DaclTms = CType(value, string)
												
						Case "PreTrigRecord"
							Me.str().PreTrigRecord = CType(value, string)
												
						Case "MaxRecord"
							Me.str().MaxRecord = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "PreTrigRecord"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.PreTrigRecord = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(DacnMapMetadata.PropertyNames.PreTrigRecord)
							End If
						
						Case "MaxRecord"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.MaxRecord = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(DacnMapMetadata.PropertyNames.MaxRecord)
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
		
			Public Sub New(ByVal entity As esDacnMap)
				Me.entity = entity
			End Sub				
		
	
			Public Property Dacn As System.String 
				Get
					Dim data_ As System.String = entity.Dacn
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Dacn = Nothing
					Else
						entity.Dacn = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Dacl As System.String 
				Get
					Dim data_ As System.String = entity.Dacl
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Dacl = Nothing
					Else
						entity.Dacl = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DACLReal As System.String 
				Get
					Dim data_ As System.String = entity.DACLReal
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DACLReal = Nothing
					Else
						entity.DACLReal = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DACLBool As System.String 
				Get
					Dim data_ As System.String = entity.DACLBool
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DACLBool = Nothing
					Else
						entity.DACLBool = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DACLMultiState As System.String 
				Get
					Dim data_ As System.String = entity.DACLMultiState
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DACLMultiState = Nothing
					Else
						entity.DACLMultiState = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DACLText As System.String 
				Get
					Dim data_ As System.String = entity.DACLText
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DACLText = Nothing
					Else
						entity.DACLText = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property DaclTms As System.String 
				Get
					Dim data_ As System.String = entity.DaclTms
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.DaclTms = Nothing
					Else
						entity.DaclTms = Convert.ToString(Value)
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
		  

			Private entity As esDacnMap
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DacnMapMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As DacnMapQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DacnMapQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As DacnMapQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As DacnMapQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As DacnMapQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esDacnMapCollection
		Inherits esEntityCollection(Of DacnMap)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return DacnMapMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "DacnMapCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As DacnMapQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New DacnMapQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As DacnMapQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New DacnMapQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As DacnMapQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, DacnMapQuery))
		End Sub
		
		#End Region
						
		Private m_query As DacnMapQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esDacnMapQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return DacnMapMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Dacn" 
					Return Me.Dacn
				Case "Dacl" 
					Return Me.Dacl
				Case "DACLReal" 
					Return Me.DACLReal
				Case "DACLBool" 
					Return Me.DACLBool
				Case "DACLMultiState" 
					Return Me.DACLMultiState
				Case "DACLText" 
					Return Me.DACLText
				Case "DaclTms" 
					Return Me.DaclTms
				Case "PreTrigRecord" 
					Return Me.PreTrigRecord
				Case "MaxRecord" 
					Return Me.MaxRecord
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Dacn As esQueryItem
			Get
				Return New esQueryItem(Me, DacnMapMetadata.ColumnNames.Dacn, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Dacl As esQueryItem
			Get
				Return New esQueryItem(Me, DacnMapMetadata.ColumnNames.Dacl, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DACLReal As esQueryItem
			Get
				Return New esQueryItem(Me, DacnMapMetadata.ColumnNames.DACLReal, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DACLBool As esQueryItem
			Get
				Return New esQueryItem(Me, DacnMapMetadata.ColumnNames.DACLBool, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DACLMultiState As esQueryItem
			Get
				Return New esQueryItem(Me, DacnMapMetadata.ColumnNames.DACLMultiState, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DACLText As esQueryItem
			Get
				Return New esQueryItem(Me, DacnMapMetadata.ColumnNames.DACLText, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property DaclTms As esQueryItem
			Get
				Return New esQueryItem(Me, DacnMapMetadata.ColumnNames.DaclTms, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property PreTrigRecord As esQueryItem
			Get
				Return New esQueryItem(Me, DacnMapMetadata.ColumnNames.PreTrigRecord, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MaxRecord As esQueryItem
			Get
				Return New esQueryItem(Me, DacnMapMetadata.ColumnNames.MaxRecord, esSystemType.Int16)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class DacnMap 
		Inherits esDacnMap
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class DacnMapMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(DacnMapMetadata.ColumnNames.Dacn, 0, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnMapMetadata.PropertyNames.Dacn
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO DACN"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnMapMetadata.ColumnNames.Dacl, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnMapMetadata.PropertyNames.Dacl
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO DACL"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnMapMetadata.ColumnNames.DACLReal, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnMapMetadata.PropertyNames.DACLReal
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO TABLE"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnMapMetadata.ColumnNames.DACLBool, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnMapMetadata.PropertyNames.DACLBool
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO TABLE"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnMapMetadata.ColumnNames.DACLMultiState, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnMapMetadata.PropertyNames.DACLMultiState
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO TABLE"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnMapMetadata.ColumnNames.DACLText, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnMapMetadata.PropertyNames.DACLText
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO TABLE"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnMapMetadata.ColumnNames.DaclTms, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = DacnMapMetadata.PropertyNames.DaclTms
			c.CharacterMaxLength = 50
			c.HasDefault = True
			c.Default = """NO TABLE"""
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnMapMetadata.ColumnNames.PreTrigRecord, 7, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = DacnMapMetadata.PropertyNames.PreTrigRecord
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(DacnMapMetadata.ColumnNames.MaxRecord, 8, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = DacnMapMetadata.PropertyNames.MaxRecord
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As DacnMapMetadata
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
			 Public Const Dacn As String = "DACN"
			 Public Const Dacl As String = "DACL"
			 Public Const DACLReal As String = "DACL Real"
			 Public Const DACLBool As String = "DACL Bool"
			 Public Const DACLMultiState As String = "DACL MultiState"
			 Public Const DACLText As String = "DACL Text"
			 Public Const DaclTms As String = "DACL TMS"
			 Public Const PreTrigRecord As String = "PreTrigRecord"
			 Public Const MaxRecord As String = "MaxRecord"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Dacn As String = "Dacn"
			 Public Const Dacl As String = "Dacl"
			 Public Const DACLReal As String = "DACLReal"
			 Public Const DACLBool As String = "DACLBool"
			 Public Const DACLMultiState As String = "DACLMultiState"
			 Public Const DACLText As String = "DACLText"
			 Public Const DaclTms As String = "DaclTms"
			 Public Const PreTrigRecord As String = "PreTrigRecord"
			 Public Const MaxRecord As String = "MaxRecord"
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
			SyncLock GetType(DacnMapMetadata)
			
				If DacnMapMetadata.mapDelegates Is Nothing Then
					DacnMapMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If DacnMapMetadata._meta Is Nothing Then
					DacnMapMetadata._meta = New DacnMapMetadata()
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
				


				meta.AddTypeMap("Dacn", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Dacl", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DACLReal", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DACLBool", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DACLMultiState", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DACLText", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DaclTms", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("PreTrigRecord", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MaxRecord", new esTypeMap("Integer", "System.Int16"))			
				
				
				 
				meta.Source = "DACN_MAP"
				meta.Destination = "DACN_MAP"
				
				meta.spInsert = "proc_DACN_MAPInsert"
				meta.spUpdate = "proc_DACN_MAPUpdate"
				meta.spDelete = "proc_DACN_MAPDelete"
				meta.spLoadAll = "proc_DACN_MAPLoadAll"
				meta.spLoadByPrimaryKey = "proc_DACN_MAPLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As DacnMapMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

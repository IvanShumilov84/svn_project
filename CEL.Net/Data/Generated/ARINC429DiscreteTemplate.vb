
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 9/24/2013 16:55:46
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
	' Encapsulates the 'ARINC429DiscreteTemplate' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(ARINC429DiscreteTemplate))> _
	<XmlType("ARINC429DiscreteTemplate")> _	
	Partial Public Class ARINC429DiscreteTemplate 
		Inherits esARINC429DiscreteTemplate
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New ARINC429DiscreteTemplate()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32, ByVal engineModel As System.String)
			Dim obj As New ARINC429DiscreteTemplate()
			obj.Id = id
			obj.EngineModel = engineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal engineModel As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New ARINC429DiscreteTemplate()
			obj.Id = id
			obj.EngineModel = engineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("ARINC429DiscreteTemplateCollection")> _
	Partial Public Class ARINC429DiscreteTemplateCollection
		Inherits esARINC429DiscreteTemplateCollection
		Implements IEnumerable(Of ARINC429DiscreteTemplate)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32, ByVal engineModel As System.String) As ARINC429DiscreteTemplate
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id And e.EngineModel = engineModel)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(ARINC429DiscreteTemplate))> _
		Public Class ARINC429DiscreteTemplateCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of ARINC429DiscreteTemplateCollection)
			
			Public Shared Widening Operator CType(packet As ARINC429DiscreteTemplateCollectionWCFPacket) As ARINC429DiscreteTemplateCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As ARINC429DiscreteTemplateCollection) As ARINC429DiscreteTemplateCollectionWCFPacket
				Return New ARINC429DiscreteTemplateCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class ARINC429DiscreteTemplateQuery 
		Inherits esARINC429DiscreteTemplateQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "ARINC429DiscreteTemplateQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As ARINC429DiscreteTemplateQuery) As String
			Return ARINC429DiscreteTemplateQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As ARINC429DiscreteTemplateQuery
			Return DirectCast(ARINC429DiscreteTemplateQuery.SerializeHelper.FromXml(query, GetType(ARINC429DiscreteTemplateQuery)), ARINC429DiscreteTemplateQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esARINC429DiscreteTemplate
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal id As System.Int32, ByVal engineModel As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(id, engineModel)
			Else
				Return LoadByPrimaryKeyStoredProcedure(id, engineModel)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal id As System.Int32, ByVal engineModel As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(id, engineModel)
			Else
				Return LoadByPrimaryKeyStoredProcedure(id, engineModel)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal id As System.Int32, ByVal engineModel As System.String) As Boolean
		
			Dim query As New ARINC429DiscreteTemplateQuery()
			query.Where(query.Id = id And query.EngineModel = engineModel)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal id As System.Int32, ByVal engineModel As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("Id", id)
						parms.Add("EngineModel", engineModel)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to ARINC429DiscreteTemplate.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(ARINC429DiscreteTemplateMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(ARINC429DiscreteTemplateMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429DiscreteTemplate.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(ARINC429DiscreteTemplateMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429DiscreteTemplateMetadata.ColumnNames.EngineModel, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Octal Arinc Label
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Label As System.String
			Get
				Return MyBase.GetSystemString(ARINC429DiscreteTemplateMetadata.ColumnNames.Label)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429DiscreteTemplateMetadata.ColumnNames.Label, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.Label)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429DiscreteTemplate.SDI
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sdi As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(ARINC429DiscreteTemplateMetadata.ColumnNames.Sdi)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(ARINC429DiscreteTemplateMetadata.ColumnNames.Sdi, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.Sdi)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429DiscreteTemplate.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(ARINC429DiscreteTemplateMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(ARINC429DiscreteTemplateMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429DiscreteTemplate.Parameter_No
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ParameterNo As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(ARINC429DiscreteTemplateMetadata.ColumnNames.ParameterNo)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(ARINC429DiscreteTemplateMetadata.ColumnNames.ParameterNo, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.ParameterNo)
				End If
			End Set
		End Property	
			
		' <summary>
		' Relative to the datafield of the ARINC word. So ARINC Bit 11 is DataField bit 1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StartBit As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429DiscreteTemplateMetadata.ColumnNames.StartBit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429DiscreteTemplateMetadata.ColumnNames.StartBit, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.StartBit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Relative to the datafield of the ARINC word. So ARINC Bit 11 is DataField bit 1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StopBit As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(ARINC429DiscreteTemplateMetadata.ColumnNames.StopBit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(ARINC429DiscreteTemplateMetadata.ColumnNames.StopBit, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.StopBit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429DiscreteTemplate.LSBValue
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LSBValue As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(ARINC429DiscreteTemplateMetadata.ColumnNames.LSBValue)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(ARINC429DiscreteTemplateMetadata.ColumnNames.LSBValue, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.LSBValue)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429DiscreteTemplate.Code
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Code As System.String
			Get
				Return MyBase.GetSystemString(ARINC429DiscreteTemplateMetadata.ColumnNames.Code)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429DiscreteTemplateMetadata.ColumnNames.Code, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.Code)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to ARINC429DiscreteTemplate.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(ARINC429DiscreteTemplateMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(ARINC429DiscreteTemplateMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.Description)
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
												
						Case "Label"
							Me.str().Label = CType(value, string)
												
						Case "Sdi"
							Me.str().Sdi = CType(value, string)
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "ParameterNo"
							Me.str().ParameterNo = CType(value, string)
												
						Case "StartBit"
							Me.str().StartBit = CType(value, string)
												
						Case "StopBit"
							Me.str().StopBit = CType(value, string)
												
						Case "LSBValue"
							Me.str().LSBValue = CType(value, string)
												
						Case "Code"
							Me.str().Code = CType(value, string)
												
						Case "Description"
							Me.str().Description = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.Id)
							End If
						
						Case "Sdi"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Sdi = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.Sdi)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "ParameterNo"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.ParameterNo = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.ParameterNo)
							End If
						
						Case "StartBit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.StartBit = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.StartBit)
							End If
						
						Case "StopBit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.StopBit = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.StopBit)
							End If
						
						Case "LSBValue"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.LSBValue = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(ARINC429DiscreteTemplateMetadata.PropertyNames.LSBValue)
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
		
			Public Sub New(ByVal entity As esARINC429DiscreteTemplate)
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
					Dim data_ As Nullable(Of System.Int32) = entity.Sdi
					
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
						entity.Sdi = Convert.ToInt32(Value)
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
		  	
			Public Property StartBit As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.StartBit
					
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
						entity.StartBit = Convert.ToByte(Value)
					End If
				End Set
			End Property
		  	
			Public Property StopBit As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.StopBit
					
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
						entity.StopBit = Convert.ToByte(Value)
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
		  	
			Public Property Code As System.String 
				Get
					Dim data_ As System.String = entity.Code
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Code = Nothing
					Else
						entity.Code = Convert.ToString(Value)
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
		  

			Private entity As esARINC429DiscreteTemplate
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return ARINC429DiscreteTemplateMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As ARINC429DiscreteTemplateQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New ARINC429DiscreteTemplateQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As ARINC429DiscreteTemplateQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As ARINC429DiscreteTemplateQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As ARINC429DiscreteTemplateQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esARINC429DiscreteTemplateCollection
		Inherits esEntityCollection(Of ARINC429DiscreteTemplate)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return ARINC429DiscreteTemplateMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "ARINC429DiscreteTemplateCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As ARINC429DiscreteTemplateQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New ARINC429DiscreteTemplateQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As ARINC429DiscreteTemplateQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New ARINC429DiscreteTemplateQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As ARINC429DiscreteTemplateQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, ARINC429DiscreteTemplateQuery))
		End Sub
		
		#End Region
						
		Private m_query As ARINC429DiscreteTemplateQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esARINC429DiscreteTemplateQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return ARINC429DiscreteTemplateMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "Id" 
					Return Me.Id
				Case "EngineModel" 
					Return Me.EngineModel
				Case "Label" 
					Return Me.Label
				Case "Sdi" 
					Return Me.Sdi
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "ParameterNo" 
					Return Me.ParameterNo
				Case "StartBit" 
					Return Me.StartBit
				Case "StopBit" 
					Return Me.StopBit
				Case "LSBValue" 
					Return Me.LSBValue
				Case "Code" 
					Return Me.Code
				Case "Description" 
					Return Me.Description
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Label As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.Label, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sdi As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.Sdi, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property ParameterNo As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.ParameterNo, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property StartBit As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.StartBit, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property StopBit As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.StopBit, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property LSBValue As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.LSBValue, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Code As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.Code, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, ARINC429DiscreteTemplateMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class ARINC429DiscreteTemplate 
		Inherits esARINC429DiscreteTemplate
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class ARINC429DiscreteTemplateMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.Label, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.Label
			c.CharacterMaxLength = 4
			c.Description = "Octal Arinc Label"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.Sdi, 3, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.Sdi
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "15"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.ExportToCsv, 4, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.ParameterNo, 5, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.ParameterNo
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.StartBit, 6, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.StartBit
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Relative to the datafield of the ARINC word. So ARINC Bit 11 is DataField bit 1"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.StopBit, 7, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.StopBit
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Relative to the datafield of the ARINC word. So ARINC Bit 11 is DataField bit 1"
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.LSBValue, 8, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.LSBValue
			c.NumericPrecision = 15
			c.HasDefault = True
			c.Default = "1"
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.Code, 9, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.Code
			c.CharacterMaxLength = 25
			m_columns.Add(c)
				
			c = New esColumnMetadata(ARINC429DiscreteTemplateMetadata.ColumnNames.Description, 10, GetType(System.String), esSystemType.String)	
			c.PropertyName = ARINC429DiscreteTemplateMetadata.PropertyNames.Description
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As ARINC429DiscreteTemplateMetadata
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
			 Public Const Label As String = "Label"
			 Public Const Sdi As String = "SDI"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const ParameterNo As String = "Parameter_No"
			 Public Const StartBit As String = "StartBit"
			 Public Const StopBit As String = "StopBit"
			 Public Const LSBValue As String = "LSBValue"
			 Public Const Code As String = "Code"
			 Public Const Description As String = "Description"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const Label As String = "Label"
			 Public Const Sdi As String = "Sdi"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const ParameterNo As String = "ParameterNo"
			 Public Const StartBit As String = "StartBit"
			 Public Const StopBit As String = "StopBit"
			 Public Const LSBValue As String = "LSBValue"
			 Public Const Code As String = "Code"
			 Public Const Description As String = "Description"
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
			SyncLock GetType(ARINC429DiscreteTemplateMetadata)
			
				If ARINC429DiscreteTemplateMetadata.mapDelegates Is Nothing Then
					ARINC429DiscreteTemplateMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If ARINC429DiscreteTemplateMetadata._meta Is Nothing Then
					ARINC429DiscreteTemplateMetadata._meta = New ARINC429DiscreteTemplateMetadata()
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
				meta.AddTypeMap("Label", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sdi", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("ParameterNo", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("StartBit", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("StopBit", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("LSBValue", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Code", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "ARINC429DiscreteTemplate"
				meta.Destination = "ARINC429DiscreteTemplate"
				
				meta.spInsert = "proc_ARINC429DiscreteTemplateInsert"
				meta.spUpdate = "proc_ARINC429DiscreteTemplateUpdate"
				meta.spDelete = "proc_ARINC429DiscreteTemplateDelete"
				meta.spLoadAll = "proc_ARINC429DiscreteTemplateLoadAll"
				meta.spLoadByPrimaryKey = "proc_ARINC429DiscreteTemplateLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As ARINC429DiscreteTemplateMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

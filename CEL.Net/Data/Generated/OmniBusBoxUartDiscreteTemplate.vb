
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 17/04/2015 11:55:39 AM
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
	' Encapsulates the 'OmniBusBoxUartDiscreteTemplate' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(OmniBusBoxUartDiscreteTemplate))> _
	<XmlType("OmniBusBoxUartDiscreteTemplate")> _	
	Partial Public Class OmniBusBoxUartDiscreteTemplate 
		Inherits esOmniBusBoxUartDiscreteTemplate
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New OmniBusBoxUartDiscreteTemplate()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal id As System.Int32, ByVal engineModel As System.String)
			Dim obj As New OmniBusBoxUartDiscreteTemplate()
			obj.Id = id
			obj.EngineModel = engineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal id As System.Int32, ByVal engineModel As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New OmniBusBoxUartDiscreteTemplate()
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
	<XmlType("OmniBusBoxUartDiscreteTemplateCollection")> _
	Partial Public Class OmniBusBoxUartDiscreteTemplateCollection
		Inherits esOmniBusBoxUartDiscreteTemplateCollection
		Implements IEnumerable(Of OmniBusBoxUartDiscreteTemplate)
	
		Public Function FindByPrimaryKey(ByVal id As System.Int32, ByVal engineModel As System.String) As OmniBusBoxUartDiscreteTemplate
			Return MyBase.SingleOrDefault(Function(e) e.Id.HasValue AndAlso e.Id.Value = id And e.EngineModel = engineModel)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(OmniBusBoxUartDiscreteTemplate))> _
		Public Class OmniBusBoxUartDiscreteTemplateCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of OmniBusBoxUartDiscreteTemplateCollection)
			
			Public Shared Widening Operator CType(packet As OmniBusBoxUartDiscreteTemplateCollectionWCFPacket) As OmniBusBoxUartDiscreteTemplateCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As OmniBusBoxUartDiscreteTemplateCollection) As OmniBusBoxUartDiscreteTemplateCollectionWCFPacket
				Return New OmniBusBoxUartDiscreteTemplateCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class OmniBusBoxUartDiscreteTemplateQuery 
		Inherits esOmniBusBoxUartDiscreteTemplateQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "OmniBusBoxUartDiscreteTemplateQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As OmniBusBoxUartDiscreteTemplateQuery) As String
			Return OmniBusBoxUartDiscreteTemplateQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As OmniBusBoxUartDiscreteTemplateQuery
			Return DirectCast(OmniBusBoxUartDiscreteTemplateQuery.SerializeHelper.FromXml(query, GetType(OmniBusBoxUartDiscreteTemplateQuery)), OmniBusBoxUartDiscreteTemplateQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esOmniBusBoxUartDiscreteTemplate
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
		
			Dim query As New OmniBusBoxUartDiscreteTemplateQuery()
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
		' Maps to OmniBusBoxUartDiscreteTemplate.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to OmniBusBoxUartDiscreteTemplate.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.EngineModel, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to OmniBusBoxUartDiscreteTemplate.PWC_PN
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property PwcPn As System.String
			Get
				Return MyBase.GetSystemString(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.PwcPn)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.PwcPn, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.PwcPn)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to OmniBusBoxUartDiscreteTemplate.ExportToCsv
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.ExportToCsv)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.ExportToCsv, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.ExportToCsv)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to OmniBusBoxUartDiscreteTemplate.Source
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Source As System.String
			Get
				Return MyBase.GetSystemString(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Source)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Source, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.Source)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to OmniBusBoxUartDiscreteTemplate.Parameter_No
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ParameterNo As Nullable(Of System.Int16)
			Get
				Return MyBase.GetSystemInt16(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.ParameterNo)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int16))
				If MyBase.SetSystemInt16(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.ParameterNo, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.ParameterNo)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to OmniBusBoxUartDiscreteTemplate.Uart_Channel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UartChannel As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.UartChannel)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.UartChannel, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.UartChannel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Uart word. 16 bits
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StartBit As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.StartBit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.StartBit, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.StartBit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Uart word. 16 bits
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property StopBit As Nullable(Of System.Byte)
			Get
				Return MyBase.GetSystemByte(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.StopBit)
			End Get
			
			Set(ByVal value As Nullable(Of System.Byte))
				If MyBase.SetSystemByte(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.StopBit, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.StopBit)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to OmniBusBoxUartDiscreteTemplate.Code
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Code As System.String
			Get
				Return MyBase.GetSystemString(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Code)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Code, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.Code)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to OmniBusBoxUartDiscreteTemplate.Description
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Description As System.String
			Get
				Return MyBase.GetSystemString(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Description)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Description, value) Then
					OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.Description)
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
												
						Case "PwcPn"
							Me.str().PwcPn = CType(value, string)
												
						Case "ExportToCsv"
							Me.str().ExportToCsv = CType(value, string)
												
						Case "Source"
							Me.str().Source = CType(value, string)
												
						Case "ParameterNo"
							Me.str().ParameterNo = CType(value, string)
												
						Case "UartChannel"
							Me.str().UartChannel = CType(value, string)
												
						Case "StartBit"
							Me.str().StartBit = CType(value, string)
												
						Case "StopBit"
							Me.str().StopBit = CType(value, string)
												
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
								OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.Id)
							End If
						
						Case "ExportToCsv"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.ExportToCsv)
							End If
						
						Case "ParameterNo"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
								Me.ParameterNo = CType(value, Nullable(Of System.Int16))
								OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.ParameterNo)
							End If
						
						Case "UartChannel"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.UartChannel = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.UartChannel)
							End If
						
						Case "StartBit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.StartBit = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.StartBit)
							End If
						
						Case "StopBit"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
								Me.StopBit = CType(value, Nullable(Of System.Byte))
								OnPropertyChanged(OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.StopBit)
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
		
			Public Sub New(ByVal entity As esOmniBusBoxUartDiscreteTemplate)
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
		  	
			Public Property UartChannel As System.String 
				Get
					Dim data_ As Nullable(Of System.Byte) = entity.UartChannel
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.UartChannel = Nothing
					Else
						entity.UartChannel = Convert.ToByte(Value)
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
		  

			Private entity As esOmniBusBoxUartDiscreteTemplate
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return OmniBusBoxUartDiscreteTemplateMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As OmniBusBoxUartDiscreteTemplateQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New OmniBusBoxUartDiscreteTemplateQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As OmniBusBoxUartDiscreteTemplateQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As OmniBusBoxUartDiscreteTemplateQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As OmniBusBoxUartDiscreteTemplateQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esOmniBusBoxUartDiscreteTemplateCollection
		Inherits esEntityCollection(Of OmniBusBoxUartDiscreteTemplate)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return OmniBusBoxUartDiscreteTemplateMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "OmniBusBoxUartDiscreteTemplateCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As OmniBusBoxUartDiscreteTemplateQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New OmniBusBoxUartDiscreteTemplateQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As OmniBusBoxUartDiscreteTemplateQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New OmniBusBoxUartDiscreteTemplateQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As OmniBusBoxUartDiscreteTemplateQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, OmniBusBoxUartDiscreteTemplateQuery))
		End Sub
		
		#End Region
						
		Private m_query As OmniBusBoxUartDiscreteTemplateQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esOmniBusBoxUartDiscreteTemplateQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return OmniBusBoxUartDiscreteTemplateMetadata.Meta()
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
				Case "ExportToCsv" 
					Return Me.ExportToCsv
				Case "Source" 
					Return Me.Source
				Case "ParameterNo" 
					Return Me.ParameterNo
				Case "UartChannel" 
					Return Me.UartChannel
				Case "StartBit" 
					Return Me.StartBit
				Case "StopBit" 
					Return Me.StopBit
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
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property PwcPn As esQueryItem
			Get
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.PwcPn, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ExportToCsv As esQueryItem
			Get
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Source As esQueryItem
			Get
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Source, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ParameterNo As esQueryItem
			Get
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.ParameterNo, esSystemType.Int16)
			End Get
		End Property 
		
		Public ReadOnly Property UartChannel As esQueryItem
			Get
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.UartChannel, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property StartBit As esQueryItem
			Get
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.StartBit, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property StopBit As esQueryItem
			Get
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.StopBit, esSystemType.Byte)
			End Get
		End Property 
		
		Public ReadOnly Property Code As esQueryItem
			Get
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Code, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Description As esQueryItem
			Get
				Return New esQueryItem(Me, OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Description, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class OmniBusBoxUartDiscreteTemplate 
		Inherits esOmniBusBoxUartDiscreteTemplate
		
	
		
		
	End Class
		



	<Serializable> _
	Partial Public Class OmniBusBoxUartDiscreteTemplateMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.Id
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			c.HasDefault = True
			c.Default = """NO MODEL"""
			m_columns.Add(c)
				
			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.PwcPn, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.PwcPn
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.ExportToCsv, 3, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.ExportToCsv
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Source, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.Source
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.ParameterNo, 5, GetType(System.Int16), esSystemType.Int16)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.ParameterNo
			c.NumericPrecision = 5
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.UartChannel, 6, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.UartChannel
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.StartBit, 7, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.StartBit
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Uart word. 16 bits"
			m_columns.Add(c)
				
			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.StopBit, 8, GetType(System.Byte), esSystemType.Byte)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.StopBit
			c.NumericPrecision = 3
			c.HasDefault = True
			c.Default = "0"
			c.Description = "Uart word. 16 bits"
			m_columns.Add(c)
				
			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Code, 9, GetType(System.String), esSystemType.String)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.Code
			c.CharacterMaxLength = 25
			m_columns.Add(c)
				
			c = New esColumnMetadata(OmniBusBoxUartDiscreteTemplateMetadata.ColumnNames.Description, 10, GetType(System.String), esSystemType.String)	
			c.PropertyName = OmniBusBoxUartDiscreteTemplateMetadata.PropertyNames.Description
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As OmniBusBoxUartDiscreteTemplateMetadata
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
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const Source As String = "Source"
			 Public Const ParameterNo As String = "Parameter_No"
			 Public Const UartChannel As String = "Uart_Channel"
			 Public Const StartBit As String = "StartBit"
			 Public Const StopBit As String = "StopBit"
			 Public Const Code As String = "Code"
			 Public Const Description As String = "Description"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const Id As String = "Id"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const PwcPn As String = "PwcPn"
			 Public Const ExportToCsv As String = "ExportToCsv"
			 Public Const Source As String = "Source"
			 Public Const ParameterNo As String = "ParameterNo"
			 Public Const UartChannel As String = "UartChannel"
			 Public Const StartBit As String = "StartBit"
			 Public Const StopBit As String = "StopBit"
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
			SyncLock GetType(OmniBusBoxUartDiscreteTemplateMetadata)
			
				If OmniBusBoxUartDiscreteTemplateMetadata.mapDelegates Is Nothing Then
					OmniBusBoxUartDiscreteTemplateMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If OmniBusBoxUartDiscreteTemplateMetadata._meta Is Nothing Then
					OmniBusBoxUartDiscreteTemplateMetadata._meta = New OmniBusBoxUartDiscreteTemplateMetadata()
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
				meta.AddTypeMap("ExportToCsv", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Source", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ParameterNo", new esTypeMap("Integer", "System.Int16"))
				meta.AddTypeMap("UartChannel", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("StartBit", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("StopBit", new esTypeMap("Byte", "System.Byte"))
				meta.AddTypeMap("Code", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Description", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "OmniBusBoxUartDiscreteTemplate"
				meta.Destination = "OmniBusBoxUartDiscreteTemplate"
				
				meta.spInsert = "proc_OmniBusBoxUartDiscreteTemplateInsert"
				meta.spUpdate = "proc_OmniBusBoxUartDiscreteTemplateUpdate"
				meta.spDelete = "proc_OmniBusBoxUartDiscreteTemplateDelete"
				meta.spLoadAll = "proc_OmniBusBoxUartDiscreteTemplateLoadAll"
				meta.spLoadByPrimaryKey = "proc_OmniBusBoxUartDiscreteTemplateLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As OmniBusBoxUartDiscreteTemplateMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

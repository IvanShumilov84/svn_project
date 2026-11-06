
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 16/03/2015 8:29:24 AM
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
	' Encapsulates the 'HMI_Page_Mapping' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(HMIPageMapping))> _
	<XmlType("HMIPageMapping")> _	
	Partial Public Class HMIPageMapping 
		Inherits esHMIPageMapping
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New HMIPageMapping()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal controlName As System.String)
			Dim obj As New HMIPageMapping()
			obj.ProjectID = projectID
			obj.CollectionName = collectionName
			obj.ControlName = controlName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal controlName As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New HMIPageMapping()
			obj.ProjectID = projectID
			obj.CollectionName = collectionName
			obj.ControlName = controlName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("HMIPageMappingCollection")> _
	Partial Public Class HMIPageMappingCollection
		Inherits esHMIPageMappingCollection
		Implements IEnumerable(Of HMIPageMapping)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal controlName As System.String) As HMIPageMapping
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.CollectionName = collectionName And e.ControlName = controlName)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(HMIPageMapping))> _
		Public Class HMIPageMappingCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of HMIPageMappingCollection)
			
			Public Shared Widening Operator CType(packet As HMIPageMappingCollectionWCFPacket) As HMIPageMappingCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As HMIPageMappingCollection) As HMIPageMappingCollectionWCFPacket
				Return New HMIPageMappingCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class HMIPageMappingQuery 
		Inherits esHMIPageMappingQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "HMIPageMappingQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As HMIPageMappingQuery) As String
			Return HMIPageMappingQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As HMIPageMappingQuery
			Return DirectCast(HMIPageMappingQuery.SerializeHelper.FromXml(query, GetType(HMIPageMappingQuery)), HMIPageMappingQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esHMIPageMapping
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal controlName As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, collectionName, controlName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, collectionName, controlName)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal controlName As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, collectionName, controlName)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, collectionName, controlName)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal controlName As System.String) As Boolean
		
			Dim query As New HMIPageMappingQuery()
			query.Where(query.ProjectID = projectID And query.CollectionName = collectionName And query.ControlName = controlName)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal collectionName As System.String, ByVal controlName As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("CollectionName", collectionName)
						parms.Add("ControlName", controlName)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to HMI_Page_Mapping.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMIPageMappingMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMIPageMappingMetadata.ColumnNames.ProjectID, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.CollectionName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CollectionName As System.String
			Get
				Return MyBase.GetSystemString(HMIPageMappingMetadata.ColumnNames.CollectionName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIPageMappingMetadata.ColumnNames.CollectionName, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.CollectionName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.ControlName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ControlName As System.String
			Get
				Return MyBase.GetSystemString(HMIPageMappingMetadata.ColumnNames.ControlName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIPageMappingMetadata.ColumnNames.ControlName, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.ControlName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.Source
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Source As System.String
			Get
				Return MyBase.GetSystemString(HMIPageMappingMetadata.ColumnNames.Source)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIPageMappingMetadata.ColumnNames.Source, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.Source)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.TitleText
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TitleText As System.String
			Get
				Return MyBase.GetSystemString(HMIPageMappingMetadata.ColumnNames.TitleText)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIPageMappingMetadata.ColumnNames.TitleText, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.TitleText)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.FileName
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property FileName As System.String
			Get
				Return MyBase.GetSystemString(HMIPageMappingMetadata.ColumnNames.FileName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIPageMappingMetadata.ColumnNames.FileName, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.FileName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.Enable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Enable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMIPageMappingMetadata.ColumnNames.Enable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMIPageMappingMetadata.ColumnNames.Enable, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.Enable)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.Visible
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Visible As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMIPageMappingMetadata.ColumnNames.Visible)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMIPageMappingMetadata.ColumnNames.Visible, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.Visible)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.TempNote
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property TempNote As System.String
			Get
				Return MyBase.GetSystemString(HMIPageMappingMetadata.ColumnNames.TempNote)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIPageMappingMetadata.ColumnNames.TempNote, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.TempNote)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.OffColor
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OffColor As System.String
			Get
				Return MyBase.GetSystemString(HMIPageMappingMetadata.ColumnNames.OffColor)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIPageMappingMetadata.ColumnNames.OffColor, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.OffColor)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.OnColor
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property OnColor As System.String
			Get
				Return MyBase.GetSystemString(HMIPageMappingMetadata.ColumnNames.OnColor)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIPageMappingMetadata.ColumnNames.OnColor, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.OnColor)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.LocationX
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LocationX As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMIPageMappingMetadata.ColumnNames.LocationX)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMIPageMappingMetadata.ColumnNames.LocationX, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.LocationX)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.LocationY
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property LocationY As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMIPageMappingMetadata.ColumnNames.LocationY)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMIPageMappingMetadata.ColumnNames.LocationY, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.LocationY)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.SizeX
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SizeX As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMIPageMappingMetadata.ColumnNames.SizeX)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMIPageMappingMetadata.ColumnNames.SizeX, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.SizeX)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.SizeY
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SizeY As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMIPageMappingMetadata.ColumnNames.SizeY)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMIPageMappingMetadata.ColumnNames.SizeY, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.SizeY)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.Font
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Font As System.String
			Get
				Return MyBase.GetSystemString(HMIPageMappingMetadata.ColumnNames.Font)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIPageMappingMetadata.ColumnNames.Font, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.Font)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.UnitsPercent
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property UnitsPercent As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMIPageMappingMetadata.ColumnNames.UnitsPercent)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMIPageMappingMetadata.ColumnNames.UnitsPercent, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.UnitsPercent)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.ValuePercent
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ValuePercent As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(HMIPageMappingMetadata.ColumnNames.ValuePercent)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(HMIPageMappingMetadata.ColumnNames.ValuePercent, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.ValuePercent)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(HMIPageMappingMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(HMIPageMappingMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to HMI_Page_Mapping.Note
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Note As System.String
			Get
				Return MyBase.GetSystemString(HMIPageMappingMetadata.ColumnNames.Note)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(HMIPageMappingMetadata.ColumnNames.Note, value) Then
					OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.Note)
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
												
						Case "ProjectID"
							Me.str().ProjectID = CType(value, string)
												
						Case "CollectionName"
							Me.str().CollectionName = CType(value, string)
												
						Case "ControlName"
							Me.str().ControlName = CType(value, string)
												
						Case "Source"
							Me.str().Source = CType(value, string)
												
						Case "TitleText"
							Me.str().TitleText = CType(value, string)
												
						Case "FileName"
							Me.str().FileName = CType(value, string)
												
						Case "Enable"
							Me.str().Enable = CType(value, string)
												
						Case "Visible"
							Me.str().Visible = CType(value, string)
												
						Case "TempNote"
							Me.str().TempNote = CType(value, string)
												
						Case "OffColor"
							Me.str().OffColor = CType(value, string)
												
						Case "OnColor"
							Me.str().OnColor = CType(value, string)
												
						Case "LocationX"
							Me.str().LocationX = CType(value, string)
												
						Case "LocationY"
							Me.str().LocationY = CType(value, string)
												
						Case "SizeX"
							Me.str().SizeX = CType(value, string)
												
						Case "SizeY"
							Me.str().SizeY = CType(value, string)
												
						Case "Font"
							Me.str().Font = CType(value, string)
												
						Case "UnitsPercent"
							Me.str().UnitsPercent = CType(value, string)
												
						Case "ValuePercent"
							Me.str().ValuePercent = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
												
						Case "Note"
							Me.str().Note = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.ProjectID)
							End If
						
						Case "Enable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Enable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.Enable)
							End If
						
						Case "Visible"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Visible = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.Visible)
							End If
						
						Case "LocationX"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LocationX = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.LocationX)
							End If
						
						Case "LocationY"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.LocationY = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.LocationY)
							End If
						
						Case "SizeX"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SizeX = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.SizeX)
							End If
						
						Case "SizeY"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SizeY = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.SizeY)
							End If
						
						Case "UnitsPercent"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.UnitsPercent = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.UnitsPercent)
							End If
						
						Case "ValuePercent"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ValuePercent = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.ValuePercent)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(HMIPageMappingMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esHMIPageMapping)
				Me.entity = entity
			End Sub				
		
	
			Public Property ProjectID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.ProjectID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ProjectID = Nothing
					Else
						entity.ProjectID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
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
		  	
			Public Property ControlName As System.String 
				Get
					Dim data_ As System.String = entity.ControlName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ControlName = Nothing
					Else
						entity.ControlName = Convert.ToString(Value)
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
		  	
			Public Property TitleText As System.String 
				Get
					Dim data_ As System.String = entity.TitleText
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TitleText = Nothing
					Else
						entity.TitleText = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property FileName As System.String 
				Get
					Dim data_ As System.String = entity.FileName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.FileName = Nothing
					Else
						entity.FileName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Enable As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Enable
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Enable = Nothing
					Else
						entity.Enable = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property Visible As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Visible
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Visible = Nothing
					Else
						entity.Visible = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property TempNote As System.String 
				Get
					Dim data_ As System.String = entity.TempNote
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.TempNote = Nothing
					Else
						entity.TempNote = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property OffColor As System.String 
				Get
					Dim data_ As System.String = entity.OffColor
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OffColor = Nothing
					Else
						entity.OffColor = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property OnColor As System.String 
				Get
					Dim data_ As System.String = entity.OnColor
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.OnColor = Nothing
					Else
						entity.OnColor = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property LocationX As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.LocationX
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LocationX = Nothing
					Else
						entity.LocationX = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property LocationY As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.LocationY
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.LocationY = Nothing
					Else
						entity.LocationY = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property SizeX As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SizeX
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SizeX = Nothing
					Else
						entity.SizeX = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property SizeY As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SizeY
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SizeY = Nothing
					Else
						entity.SizeY = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Font As System.String 
				Get
					Dim data_ As System.String = entity.Font
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Font = Nothing
					Else
						entity.Font = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property UnitsPercent As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.UnitsPercent
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.UnitsPercent = Nothing
					Else
						entity.UnitsPercent = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property ValuePercent As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.ValuePercent
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ValuePercent = Nothing
					Else
						entity.ValuePercent = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Unavailable As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Unavailable
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Unavailable = Nothing
					Else
						entity.Unavailable = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property Note As System.String 
				Get
					Dim data_ As System.String = entity.Note
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Note = Nothing
					Else
						entity.Note = Convert.ToString(Value)
					End If
				End Set
			End Property
		  

			Private entity As esHMIPageMapping
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return HMIPageMappingMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As HMIPageMappingQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New HMIPageMappingQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As HMIPageMappingQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As HMIPageMappingQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As HMIPageMappingQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esHMIPageMappingCollection
		Inherits esEntityCollection(Of HMIPageMapping)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return HMIPageMappingMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "HMIPageMappingCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As HMIPageMappingQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New HMIPageMappingQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As HMIPageMappingQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New HMIPageMappingQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As HMIPageMappingQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, HMIPageMappingQuery))
		End Sub
		
		#End Region
						
		Private m_query As HMIPageMappingQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esHMIPageMappingQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return HMIPageMappingMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "CollectionName" 
					Return Me.CollectionName
				Case "ControlName" 
					Return Me.ControlName
				Case "Source" 
					Return Me.Source
				Case "TitleText" 
					Return Me.TitleText
				Case "FileName" 
					Return Me.FileName
				Case "Enable" 
					Return Me.Enable
				Case "Visible" 
					Return Me.Visible
				Case "TempNote" 
					Return Me.TempNote
				Case "OffColor" 
					Return Me.OffColor
				Case "OnColor" 
					Return Me.OnColor
				Case "LocationX" 
					Return Me.LocationX
				Case "LocationY" 
					Return Me.LocationY
				Case "SizeX" 
					Return Me.SizeX
				Case "SizeY" 
					Return Me.SizeY
				Case "Font" 
					Return Me.Font
				Case "UnitsPercent" 
					Return Me.UnitsPercent
				Case "ValuePercent" 
					Return Me.ValuePercent
				Case "Unavailable" 
					Return Me.Unavailable
				Case "Note" 
					Return Me.Note
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property CollectionName As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.CollectionName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ControlName As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.ControlName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Source As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.Source, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property TitleText As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.TitleText, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property FileName As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.FileName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Enable As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.Enable, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Visible As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.Visible, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property TempNote As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.TempNote, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OffColor As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.OffColor, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property OnColor As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.OnColor, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property LocationX As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.LocationX, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property LocationY As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.LocationY, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property SizeX As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.SizeX, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property SizeY As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.SizeY, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Font As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.Font, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property UnitsPercent As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.UnitsPercent, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property ValuePercent As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.ValuePercent, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Note As esQueryItem
			Get
				Return New esQueryItem(Me, HMIPageMappingMetadata.ColumnNames.Note, esSystemType.String)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class HMIPageMapping 
		Inherits esHMIPageMapping
		
	
		#Region "UutCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UutCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.HMIPageMapping.UutCollectionByProjectID_Delegate)
				map.PropertyName = "UutCollectionByProjectID"
				map.MyColumnName = "ProjectID"
				map.ParentColumnName = "ProjectID"
				map.IsMultiPartKey = false
				Return map
			End Get
		End Property

		Private Shared Sub UutCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New HMIPageMappingQuery(data.NextAlias())
			
			Dim mee As UutQuery = If(data.You IsNot Nothing, TryCast(data.You, UutQuery), New UutQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - HMI_Page_MappingUUT
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UutCollectionByProjectID As UutCollection 
		
			Get
				If Me._UutCollectionByProjectID Is Nothing Then
					Me._UutCollectionByProjectID = New UutCollection()
					Me._UutCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UutCollectionByProjectID", Me._UutCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UutCollectionByProjectID.Query.Where(Me._UutCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UutCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UutCollectionByProjectID.fks.Add(UutMetadata.ColumnNames.ProjectID, Me.ProjectID)
					End If
				End If

				Return Me._UutCollectionByProjectID
			End Get
			
			Set(ByVal value As UutCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UutCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UutCollectionByProjectID")
					Me._UutCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UutCollectionByProjectID As UutCollection
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "UutCollectionByProjectID"
					coll = Me.UutCollectionByProjectID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "UutCollectionByProjectID", GetType(UutCollection), New Uut()))
			Return props
			
		End Function
	End Class
		



	<Serializable> _
	Partial Public Class HMIPageMappingMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.CollectionName, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.CollectionName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.ControlName, 2, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.ControlName
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 100
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.Source, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.Source
			c.CharacterMaxLength = 75
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.TitleText, 4, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.TitleText
			c.CharacterMaxLength = 100
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.FileName, 5, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.FileName
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.Enable, 6, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.Enable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.Visible, 7, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.Visible
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "1"
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.TempNote, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.TempNote
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.OffColor, 9, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.OffColor
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.OnColor, 10, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.OnColor
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.LocationX, 11, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.LocationX
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.LocationY, 12, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.LocationY
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.SizeX, 13, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.SizeX
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.SizeY, 14, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.SizeY
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.Font, 15, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.Font
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.UnitsPercent, 16, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.UnitsPercent
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.ValuePercent, 17, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.ValuePercent
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.Unavailable, 18, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(HMIPageMappingMetadata.ColumnNames.Note, 19, GetType(System.String), esSystemType.String)	
			c.PropertyName = HMIPageMappingMetadata.PropertyNames.Note
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As HMIPageMappingMetadata
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
			 Public Const ProjectID As String = "ProjectID"
			 Public Const CollectionName As String = "CollectionName"
			 Public Const ControlName As String = "ControlName"
			 Public Const Source As String = "Source"
			 Public Const TitleText As String = "TitleText"
			 Public Const FileName As String = "FileName"
			 Public Const Enable As String = "Enable"
			 Public Const Visible As String = "Visible"
			 Public Const TempNote As String = "TempNote"
			 Public Const OffColor As String = "OffColor"
			 Public Const OnColor As String = "OnColor"
			 Public Const LocationX As String = "LocationX"
			 Public Const LocationY As String = "LocationY"
			 Public Const SizeX As String = "SizeX"
			 Public Const SizeY As String = "SizeY"
			 Public Const Font As String = "Font"
			 Public Const UnitsPercent As String = "UnitsPercent"
			 Public Const ValuePercent As String = "ValuePercent"
			 Public Const Unavailable As String = "Unavailable"
			 Public Const Note As String = "Note"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const CollectionName As String = "CollectionName"
			 Public Const ControlName As String = "ControlName"
			 Public Const Source As String = "Source"
			 Public Const TitleText As String = "TitleText"
			 Public Const FileName As String = "FileName"
			 Public Const Enable As String = "Enable"
			 Public Const Visible As String = "Visible"
			 Public Const TempNote As String = "TempNote"
			 Public Const OffColor As String = "OffColor"
			 Public Const OnColor As String = "OnColor"
			 Public Const LocationX As String = "LocationX"
			 Public Const LocationY As String = "LocationY"
			 Public Const SizeX As String = "SizeX"
			 Public Const SizeY As String = "SizeY"
			 Public Const Font As String = "Font"
			 Public Const UnitsPercent As String = "UnitsPercent"
			 Public Const ValuePercent As String = "ValuePercent"
			 Public Const Unavailable As String = "Unavailable"
			 Public Const Note As String = "Note"
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
			SyncLock GetType(HMIPageMappingMetadata)
			
				If HMIPageMappingMetadata.mapDelegates Is Nothing Then
					HMIPageMappingMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If HMIPageMappingMetadata._meta Is Nothing Then
					HMIPageMappingMetadata._meta = New HMIPageMappingMetadata()
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
				


				meta.AddTypeMap("ProjectID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("CollectionName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ControlName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Source", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("TitleText", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("FileName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Enable", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Visible", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("TempNote", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OffColor", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("OnColor", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("LocationX", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("LocationY", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("SizeX", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("SizeY", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Font", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("UnitsPercent", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("ValuePercent", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Note", new esTypeMap("Memo", "System.String"))			
				
				
				 
				meta.Source = "HMI_Page_Mapping"
				meta.Destination = "HMI_Page_Mapping"
				
				meta.spInsert = "proc_HMI_Page_MappingInsert"
				meta.spUpdate = "proc_HMI_Page_MappingUpdate"
				meta.spDelete = "proc_HMI_Page_MappingDelete"
				meta.spLoadAll = "proc_HMI_Page_MappingLoadAll"
				meta.spLoadByPrimaryKey = "proc_HMI_Page_MappingLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As HMIPageMappingMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

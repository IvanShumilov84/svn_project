
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 10/25/2019 9:14:47 AM
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
	' Encapsulates the 'UUT_HMI_Window' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(UUTHMIWindow))> _
	<XmlType("UUTHMIWindow")> _	
	Partial Public Class UUTHMIWindow 
		Inherits esUUTHMIWindow
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New UUTHMIWindow()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal windowID As System.Int32)
			Dim obj As New UUTHMIWindow()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.WindowID = windowID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal windowID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New UUTHMIWindow()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.WindowID = windowID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save(sqlAccessType)
		End Sub
		#End Region		
	
		
			
	End Class


 
	<DebuggerDisplay("Count = {Count}")> _ 
	<Serializable> _
	<CollectionDataContract> _
	<XmlType("UUTHMIWindowCollection")> _
	Partial Public Class UUTHMIWindowCollection
		Inherits esUUTHMIWindowCollection
		Implements IEnumerable(Of UUTHMIWindow)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal windowID As System.Int32) As UUTHMIWindow
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.WindowID.HasValue AndAlso e.WindowID.Value = windowID)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(UUTHMIWindow))> _
		Public Class UUTHMIWindowCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UUTHMIWindowCollection)
			
			Public Shared Widening Operator CType(packet As UUTHMIWindowCollectionWCFPacket) As UUTHMIWindowCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UUTHMIWindowCollection) As UUTHMIWindowCollectionWCFPacket
				Return New UUTHMIWindowCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UUTHMIWindowQuery 
		Inherits esUUTHMIWindowQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UUTHMIWindowQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UUTHMIWindowQuery) As String
			Return UUTHMIWindowQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UUTHMIWindowQuery
			Return DirectCast(UUTHMIWindowQuery.SerializeHelper.FromXml(query, GetType(UUTHMIWindowQuery)), UUTHMIWindowQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUUTHMIWindow
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal windowID As System.Int32) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, windowID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, windowID)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal windowID As System.Int32) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel, windowID)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, windowID)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal windowID As System.Int32) As Boolean
		
			Dim query As New UUTHMIWindowQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.WindowID = windowID)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal windowID As System.Int32) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
						parms.Add("WindowID", windowID)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT_HMI_Window.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTHMIWindowMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTHMIWindowMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UUTHMIWindowMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTHMIWindowMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToUutByProjectID = Nothing
					Me.OnPropertyChanged("UpToUutByProjectID")
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.WindowID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property WindowID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTHMIWindowMetadata.ColumnNames.WindowID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTHMIWindowMetadata.ColumnNames.WindowID, value) Then
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.WindowID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.MainScreen
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MainScreen As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTHMIWindowMetadata.ColumnNames.MainScreen)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTHMIWindowMetadata.ColumnNames.MainScreen, value) Then
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.MainScreen)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.Master
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Master As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTHMIWindowMetadata.ColumnNames.Master)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTHMIWindowMetadata.ColumnNames.Master, value) Then
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.Master)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.Slave
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Slave As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTHMIWindowMetadata.ColumnNames.Slave)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTHMIWindowMetadata.ColumnNames.Slave, value) Then
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.Slave)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.HMI_Window_Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property HMIWindowName As System.String
			Get
				Return MyBase.GetSystemString(UUTHMIWindowMetadata.ColumnNames.HMIWindowName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTHMIWindowMetadata.ColumnNames.HMIWindowName, value) Then
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.HMIWindowName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.Parent_Window_Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ParentWindowName As System.String
			Get
				Return MyBase.GetSystemString(UUTHMIWindowMetadata.ColumnNames.ParentWindowName)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UUTHMIWindowMetadata.ColumnNames.ParentWindowName, value) Then
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.ParentWindowName)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.Window_BorderStyle
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property WindowBorderStyle As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTHMIWindowMetadata.ColumnNames.WindowBorderStyle)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTHMIWindowMetadata.ColumnNames.WindowBorderStyle, value) Then
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.WindowBorderStyle)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.Window_Width
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property WindowWidth As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTHMIWindowMetadata.ColumnNames.WindowWidth)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTHMIWindowMetadata.ColumnNames.WindowWidth, value) Then
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.WindowWidth)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.Window_Height
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property WindowHeight As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTHMIWindowMetadata.ColumnNames.WindowHeight)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTHMIWindowMetadata.ColumnNames.WindowHeight, value) Then
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.WindowHeight)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.MeasurementUnitId
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MeasurementUnitId As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UUTHMIWindowMetadata.ColumnNames.MeasurementUnitId)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UUTHMIWindowMetadata.ColumnNames.MeasurementUnitId, value) Then
					Me._UpToMeasurementUnitByMeasurementUnitId = Nothing
					Me.OnPropertyChanged("UpToMeasurementUnitByMeasurementUnitId")
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.MeasurementUnitId)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT_HMI_Window.Unavailable
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Unavailable As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UUTHMIWindowMetadata.ColumnNames.Unavailable)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UUTHMIWindowMetadata.ColumnNames.Unavailable, value) Then
					OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.Unavailable)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToMeasurementUnitByMeasurementUnitId As MeasurementUnit
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToUutByProjectID As Uut
		
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
												
						Case "EngineModel"
							Me.str().EngineModel = CType(value, string)
												
						Case "WindowID"
							Me.str().WindowID = CType(value, string)
												
						Case "MainScreen"
							Me.str().MainScreen = CType(value, string)
												
						Case "Master"
							Me.str().Master = CType(value, string)
												
						Case "Slave"
							Me.str().Slave = CType(value, string)
												
						Case "HMIWindowName"
							Me.str().HMIWindowName = CType(value, string)
												
						Case "ParentWindowName"
							Me.str().ParentWindowName = CType(value, string)
												
						Case "WindowBorderStyle"
							Me.str().WindowBorderStyle = CType(value, string)
												
						Case "WindowWidth"
							Me.str().WindowWidth = CType(value, string)
												
						Case "WindowHeight"
							Me.str().WindowHeight = CType(value, string)
												
						Case "MeasurementUnitId"
							Me.str().MeasurementUnitId = CType(value, string)
												
						Case "Unavailable"
							Me.str().Unavailable = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.ProjectID)
							End If
						
						Case "WindowID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.WindowID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.WindowID)
							End If
						
						Case "MainScreen"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.MainScreen = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.MainScreen)
							End If
						
						Case "Master"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Master = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.Master)
							End If
						
						Case "Slave"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Slave = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.Slave)
							End If
						
						Case "WindowBorderStyle"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.WindowBorderStyle = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.WindowBorderStyle)
							End If
						
						Case "WindowWidth"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.WindowWidth = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.WindowWidth)
							End If
						
						Case "WindowHeight"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.WindowHeight = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.WindowHeight)
							End If
						
						Case "MeasurementUnitId"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.MeasurementUnitId = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.MeasurementUnitId)
							End If
						
						Case "Unavailable"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Unavailable = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UUTHMIWindowMetadata.PropertyNames.Unavailable)
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
		
			Public Sub New(ByVal entity As esUUTHMIWindow)
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
		  	
			Public Property WindowID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.WindowID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.WindowID = Nothing
					Else
						entity.WindowID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MainScreen As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.MainScreen
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MainScreen = Nothing
					Else
						entity.MainScreen = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property Master As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Master
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Master = Nothing
					Else
						entity.Master = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property Slave As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Slave
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Slave = Nothing
					Else
						entity.Slave = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property HMIWindowName As System.String 
				Get
					Dim data_ As System.String = entity.HMIWindowName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.HMIWindowName = Nothing
					Else
						entity.HMIWindowName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property ParentWindowName As System.String 
				Get
					Dim data_ As System.String = entity.ParentWindowName
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.ParentWindowName = Nothing
					Else
						entity.ParentWindowName = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property WindowBorderStyle As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.WindowBorderStyle
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.WindowBorderStyle = Nothing
					Else
						entity.WindowBorderStyle = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property WindowWidth As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.WindowWidth
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.WindowWidth = Nothing
					Else
						entity.WindowWidth = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property WindowHeight As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.WindowHeight
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.WindowHeight = Nothing
					Else
						entity.WindowHeight = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MeasurementUnitId As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.MeasurementUnitId
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MeasurementUnitId = Nothing
					Else
						entity.MeasurementUnitId = Convert.ToInt32(Value)
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
		  

			Private entity As esUUTHMIWindow
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTHMIWindowMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UUTHMIWindowQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTHMIWindowQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UUTHMIWindowQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UUTHMIWindowQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UUTHMIWindowQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUUTHMIWindowCollection
		Inherits esEntityCollection(Of UUTHMIWindow)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UUTHMIWindowMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UUTHMIWindowCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UUTHMIWindowQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UUTHMIWindowQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UUTHMIWindowQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UUTHMIWindowQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UUTHMIWindowQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UUTHMIWindowQuery))
		End Sub
		
		#End Region
						
		Private m_query As UUTHMIWindowQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUUTHMIWindowQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UUTHMIWindowMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "WindowID" 
					Return Me.WindowID
				Case "MainScreen" 
					Return Me.MainScreen
				Case "Master" 
					Return Me.Master
				Case "Slave" 
					Return Me.Slave
				Case "HMIWindowName" 
					Return Me.HMIWindowName
				Case "ParentWindowName" 
					Return Me.ParentWindowName
				Case "WindowBorderStyle" 
					Return Me.WindowBorderStyle
				Case "WindowWidth" 
					Return Me.WindowWidth
				Case "WindowHeight" 
					Return Me.WindowHeight
				Case "MeasurementUnitId" 
					Return Me.MeasurementUnitId
				Case "Unavailable" 
					Return Me.Unavailable
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property WindowID As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.WindowID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MainScreen As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.MainScreen, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Master As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.Master, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property Slave As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.Slave, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property HMIWindowName As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.HMIWindowName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property ParentWindowName As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.ParentWindowName, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property WindowBorderStyle As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.WindowBorderStyle, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property WindowWidth As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.WindowWidth, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property WindowHeight As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.WindowHeight, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MeasurementUnitId As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.MeasurementUnitId, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Unavailable As esQueryItem
			Get
				Return New esQueryItem(Me, UUTHMIWindowMetadata.ColumnNames.Unavailable, esSystemType.Boolean)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class UUTHMIWindow 
		Inherits esUUTHMIWindow
		
	
		#Region "UpToMeasurementUnitByMeasurementUnitId - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - MeasurementUnitUUT_HMI_Window
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToMeasurementUnitByMeasurementUnitId As MeasurementUnit
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToMeasurementUnitByMeasurementUnitId Is Nothing _
						 AndAlso Not MeasurementUnitId.Equals(Nothing)  Then
						
					Me._UpToMeasurementUnitByMeasurementUnitId = New MeasurementUnit()
					Me._UpToMeasurementUnitByMeasurementUnitId.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToMeasurementUnitByMeasurementUnitId", Me._UpToMeasurementUnitByMeasurementUnitId)
					Me._UpToMeasurementUnitByMeasurementUnitId.Query.Where(Me._UpToMeasurementUnitByMeasurementUnitId.Query.MeasurementUnitId = Me.MeasurementUnitId)
					Me._UpToMeasurementUnitByMeasurementUnitId.Query.Load()
				End If

				Return Me._UpToMeasurementUnitByMeasurementUnitId
			End Get
			
            Set(ByVal value As MeasurementUnit)
				Me.RemovePreSave("UpToMeasurementUnitByMeasurementUnitId")
				

				If value Is Nothing Then
				
					Me.MeasurementUnitId = Nothing
				
					Me._UpToMeasurementUnitByMeasurementUnitId = Nothing
				Else
				
					Me.MeasurementUnitId = value.MeasurementUnitId
					
					Me._UpToMeasurementUnitByMeasurementUnitId = value
					Me.SetPreSave("UpToMeasurementUnitByMeasurementUnitId", Me._UpToMeasurementUnitByMeasurementUnitId)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToUutByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - UUTUUT_HMI_Window
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToUutByProjectID As Uut
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToUutByProjectID Is Nothing _
						 AndAlso Not ProjectID.Equals(Nothing)  AndAlso Not EngineModel.Equals(Nothing)  Then
						
					Me._UpToUutByProjectID = New Uut()
					Me._UpToUutByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToUutByProjectID", Me._UpToUutByProjectID)
					Me._UpToUutByProjectID.Query.Where(Me._UpToUutByProjectID.Query.ProjectID = Me.ProjectID)
					Me._UpToUutByProjectID.Query.Where(Me._UpToUutByProjectID.Query.EngineModel = Me.EngineModel)
					Me._UpToUutByProjectID.Query.Load()
				End If

				Return Me._UpToUutByProjectID
			End Get
			
            Set(ByVal value As Uut)
				Me.RemovePreSave("UpToUutByProjectID")
				

				If value Is Nothing Then
				
					Me.ProjectID = Nothing
				
					Me.EngineModel = Nothing
				
					Me._UpToUutByProjectID = Nothing
				Else
				
					Me.ProjectID = value.ProjectID
					
					Me.EngineModel = value.EngineModel
					
					Me._UpToUutByProjectID = value
					Me.SetPreSave("UpToUutByProjectID", Me._UpToUutByProjectID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
	End Class
		



	<Serializable> _
	Partial Public Class UUTHMIWindowMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 50
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.WindowID, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.WindowID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.MainScreen, 3, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.MainScreen
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.Master, 4, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.Master
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.Slave, 5, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.Slave
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.HMIWindowName, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.HMIWindowName
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.ParentWindowName, 7, GetType(System.String), esSystemType.String)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.ParentWindowName
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.WindowBorderStyle, 8, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.WindowBorderStyle
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.WindowWidth, 9, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.WindowWidth
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.WindowHeight, 10, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.WindowHeight
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.MeasurementUnitId, 11, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.MeasurementUnitId
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UUTHMIWindowMetadata.ColumnNames.Unavailable, 12, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UUTHMIWindowMetadata.PropertyNames.Unavailable
			c.CharacterMaxLength = 2
			c.HasDefault = True
			c.Default = "0"
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UUTHMIWindowMetadata
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
			 Public Const EngineModel As String = "EngineModel"
			 Public Const WindowID As String = "WindowID"
			 Public Const MainScreen As String = "MainScreen"
			 Public Const Master As String = "Master"
			 Public Const Slave As String = "Slave"
			 Public Const HMIWindowName As String = "HMI_Window_Name"
			 Public Const ParentWindowName As String = "Parent_Window_Name"
			 Public Const WindowBorderStyle As String = "Window_BorderStyle"
			 Public Const WindowWidth As String = "Window_Width"
			 Public Const WindowHeight As String = "Window_Height"
			 Public Const MeasurementUnitId As String = "MeasurementUnitId"
			 Public Const Unavailable As String = "Unavailable"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const WindowID As String = "WindowID"
			 Public Const MainScreen As String = "MainScreen"
			 Public Const Master As String = "Master"
			 Public Const Slave As String = "Slave"
			 Public Const HMIWindowName As String = "HMIWindowName"
			 Public Const ParentWindowName As String = "ParentWindowName"
			 Public Const WindowBorderStyle As String = "WindowBorderStyle"
			 Public Const WindowWidth As String = "WindowWidth"
			 Public Const WindowHeight As String = "WindowHeight"
			 Public Const MeasurementUnitId As String = "MeasurementUnitId"
			 Public Const Unavailable As String = "Unavailable"
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
			SyncLock GetType(UUTHMIWindowMetadata)
			
				If UUTHMIWindowMetadata.mapDelegates Is Nothing Then
					UUTHMIWindowMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UUTHMIWindowMetadata._meta Is Nothing Then
					UUTHMIWindowMetadata._meta = New UUTHMIWindowMetadata()
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
				meta.AddTypeMap("EngineModel", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("WindowID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MainScreen", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Master", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("Slave", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("HMIWindowName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("ParentWindowName", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("WindowBorderStyle", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("WindowWidth", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("WindowHeight", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MeasurementUnitId", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Unavailable", new esTypeMap("Yes/No", "System.Boolean"))			
				
				
				 
				meta.Source = "UUT_HMI_Window"
				meta.Destination = "UUT_HMI_Window"
				
				meta.spInsert = "proc_UUT_HMI_WindowInsert"
				meta.spUpdate = "proc_UUT_HMI_WindowUpdate"
				meta.spDelete = "proc_UUT_HMI_WindowDelete"
				meta.spLoadAll = "proc_UUT_HMI_WindowLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUT_HMI_WindowLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UUTHMIWindowMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

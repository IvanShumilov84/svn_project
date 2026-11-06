
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 1/21/2016 3:04:03 PM
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
	' Encapsulates the 'UUT' table
	' </summary>

	<System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _ 
	<Serializable> _
	<DataContract> _
	<KnownType(GetType(Uut))> _
	<XmlType("Uut")> _	
	Partial Public Class Uut 
		Inherits esUut
		
		<DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _		
		Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
			Get
				Return MyBase.Debug
			End Get
		End Property
		
		Public Overrides Function CreateInstance() as esEntity
			Return New Uut()
		End Function
		
		#Region "Static Quick Access Methods"
		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String)
			Dim obj As New Uut()
			obj.ProjectID = projectID
			obj.EngineModel = engineModel
			obj.AcceptChanges()
			obj.MarkAsDeleted()
			obj.Save()
		End Sub

		Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal sqlAccessType As esSqlAccessType)
			Dim obj As New Uut()
			obj.ProjectID = projectID
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
	<XmlType("UutCollection")> _
	Partial Public Class UutCollection
		Inherits esUutCollection
		Implements IEnumerable(Of Uut)
	
		Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String) As Uut
			Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel)
		End Function


				
		#Region "WCF Service Class"

		<DataContract> _
		<KnownType(GetType(Uut))> _
		Public Class UutCollectionWCFPacket
			Inherits esCollectionWCFPacket(Of UutCollection)
			
			Public Shared Widening Operator CType(packet As UutCollectionWCFPacket) As UutCollection
				Return packet.Collection
			End Operator

			Public Shared Widening Operator CType(collection As UutCollection) As UutCollectionWCFPacket
				Return New UutCollectionWCFPacket()  With {.Collection = collection}
			End Operator
			
		End Class

		#End Region
		
			
		
	End Class




	<DebuggerDisplay("Query = {Parse()}")> _ 
	<Serializable> _ 
	Partial Public Class UutQuery 
		Inherits esUutQuery
		
		Public Sub New(ByVal joinAlias As String)
			Me.es.JoinAlias = joinAlias
		End Sub	
		
		Protected Overrides Function GetQueryName() As String
			Return "UutQuery"
		End Function	
		
		#Region "Explicit Casts"

		Public Shared Narrowing Operator CType(ByVal query As UutQuery) As String
			Return UutQuery.SerializeHelper.ToXml(query)
		End Operator

		Public Shared Narrowing Operator CType(ByVal query As String) As UutQuery
			Return DirectCast(UutQuery.SerializeHelper.FromXml(query, GetType(UutQuery)), UutQuery)
		End Operator

		#End Region
			
	End Class

	
	<DataContract> _
	<Serializable()> _
	MustInherit Public Partial Class esUut
		Inherits esEntity
		Implements INotifyPropertyChanged
	
		Public Sub New()
		
		End Sub
		
#Region "LoadByPrimaryKey"		
		Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String) As Boolean
		
			If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel)
			End If
			
		End Function
	
		Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String) As Boolean
		
			If sqlAccessType = esSqlAccessType.DynamicSQL
				Return LoadByPrimaryKeyDynamic(projectID, engineModel)
			Else
				Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel)
			End If
			
		End Function
	
		Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String) As Boolean
		
			Dim query As New UutQuery()
			query.Where(query.ProjectID = projectID And query.EngineModel = engineModel)
			Return Me.Load(query)
			
		End Function
	
		Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String) As Boolean
		
			Dim parms As esParameters = New esParameters()
			parms.Add("ProjectID", projectID)
						parms.Add("EngineModel", engineModel)
			
			Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)
			
		End Function
#End Region
		
#Region "Properties"
		
		
			
		' <summary>
		' Maps to UUT.ProjectID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property ProjectID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UutMetadata.ColumnNames.ProjectID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UutMetadata.ColumnNames.ProjectID, value) Then
					Me._UpToProjectInfoByProjectID = Nothing
					Me.OnPropertyChanged("UpToProjectInfoByProjectID")
					OnPropertyChanged(UutMetadata.PropertyNames.ProjectID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.EngineModel
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineModel As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.EngineModel)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.EngineModel, value) Then
					Me._UpToEngineInfoByEngineModel = Nothing
					Me.OnPropertyChanged("UpToEngineInfoByEngineModel")
					OnPropertyChanged(UutMetadata.PropertyNames.EngineModel)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.ID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Id As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UutMetadata.ColumnNames.Id)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UutMetadata.ColumnNames.Id, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Id)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.EngineFamily
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property EngineFamily As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.EngineFamily)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.EngineFamily, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.EngineFamily)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.Available
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Available As Nullable(Of System.Boolean)
			Get
				Return MyBase.GetSystemBoolean(UutMetadata.ColumnNames.Available)
			End Get
			
			Set(ByVal value As Nullable(Of System.Boolean))
				If MyBase.SetSystemBoolean(UutMetadata.ColumnNames.Available, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Available)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SecurityLevelID
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SecurityLevelID As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UutMetadata.ColumnNames.SecurityLevelID)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UutMetadata.ColumnNames.SecurityLevelID, value) Then
					Me._UpToSecurityLevelBySecurityLevelID = Nothing
					Me.OnPropertyChanged("UpToSecurityLevelBySecurityLevelID")
					OnPropertyChanged(UutMetadata.PropertyNames.SecurityLevelID)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.Constant1_Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Constant1Name As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Constant1Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Constant1Name, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Constant1Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.Calibration_constant_1
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationConstant1 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UutMetadata.ColumnNames.CalibrationConstant1)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UutMetadata.ColumnNames.CalibrationConstant1, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.CalibrationConstant1)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.Constant2_Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Constant2Name As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Constant2Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Constant2Name, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Constant2Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.Calibration_constant_2
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationConstant2 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UutMetadata.ColumnNames.CalibrationConstant2)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UutMetadata.ColumnNames.CalibrationConstant2, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.CalibrationConstant2)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.Constant3_Name
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Constant3Name As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Constant3Name)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Constant3Name, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Constant3Name)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.Calibration_Constant_3
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CalibrationConstant3 As Nullable(Of System.Double)
			Get
				Return MyBase.GetSystemDouble(UutMetadata.ColumnNames.CalibrationConstant3)
			End Get
			
			Set(ByVal value As Nullable(Of System.Double))
				If MyBase.SetSystemDouble(UutMetadata.ColumnNames.CalibrationConstant3, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.CalibrationConstant3)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.Smoothing_Period
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property SmoothingPeriod As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UutMetadata.ColumnNames.SmoothingPeriod)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UutMetadata.ColumnNames.SmoothingPeriod, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.SmoothingPeriod)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB11
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb11 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb11)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb11, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb11)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB12
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb12 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb12)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb12, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb12)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB21
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb21 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb21)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb21, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb21)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB22
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb22 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb22)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb22, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb22)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB31
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb31 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb31)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb31, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb31)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB32
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb32 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb32)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb32, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb32)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB41
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb41 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb41)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb41, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb41)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB42
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb42 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb42)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb42, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb42)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB51
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb51 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb51)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb51, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb51)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB52
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb52 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb52)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb52, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb52)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB61
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb61 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb61)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb61, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb61)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB62
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb62 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb62)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb62, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb62)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB71
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb71 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb71)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb71, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb71)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB72
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb72 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb72)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb72, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb72)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB81
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb81 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb81)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb81, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb81)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB82
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb82 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb82)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb82, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb82)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB91
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb91 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb91)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb91, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb91)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB92
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb92 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb92)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb92, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb92)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB101
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb101 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb101)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb101, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb101)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.SB102
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property Sb102 As System.String
			Get
				Return MyBase.GetSystemString(UutMetadata.ColumnNames.Sb102)
			End Get
			
			Set(ByVal value As System.String)
				If MyBase.SetSystemString(UutMetadata.ColumnNames.Sb102, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.Sb102)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.CCAS_PickUp1_Sensitivity
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CCASPickUp1Sensitivity As Nullable(Of System.Decimal)
			Get
				Return MyBase.GetSystemDecimal(UutMetadata.ColumnNames.CCASPickUp1Sensitivity)
			End Get
			
			Set(ByVal value As Nullable(Of System.Decimal))
				If MyBase.SetSystemDecimal(UutMetadata.ColumnNames.CCASPickUp1Sensitivity, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.CCASPickUp1Sensitivity)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.CCAS_PickUp2_Sensitivity
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property CCASPickUp2Sensitivity As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UutMetadata.ColumnNames.CCASPickUp2Sensitivity)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UutMetadata.ColumnNames.CCASPickUp2Sensitivity, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.CCASPickUp2Sensitivity)
				End If
			End Set
		End Property	
			
		' <summary>
		' Maps to UUT.MinSlopeRig
		' </summary>
		<DataMember(EmitDefaultValue:=False)> _
		Public Overridable Property MinSlopeRig As Nullable(Of System.Int32)
			Get
				Return MyBase.GetSystemInt32(UutMetadata.ColumnNames.MinSlopeRig)
			End Get
			
			Set(ByVal value As Nullable(Of System.Int32))
				If MyBase.SetSystemInt32(UutMetadata.ColumnNames.MinSlopeRig, value) Then
					OnPropertyChanged(UutMetadata.PropertyNames.MinSlopeRig)
				End If
			End Set
		End Property	
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToEngineInfoByEngineModel As EngineInfo
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToHMIPageMappingByProjectID As HMIPageMapping
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToProjectInfoByProjectID As ProjectInfo
		
		<CLSCompliant(false)> _
		Dim Friend Protected _UpToSecurityLevelBySecurityLevelID As SecurityLevel
		
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
												
						Case "Id"
							Me.str().Id = CType(value, string)
												
						Case "EngineFamily"
							Me.str().EngineFamily = CType(value, string)
												
						Case "Available"
							Me.str().Available = CType(value, string)
												
						Case "SecurityLevelID"
							Me.str().SecurityLevelID = CType(value, string)
												
						Case "Constant1Name"
							Me.str().Constant1Name = CType(value, string)
												
						Case "CalibrationConstant1"
							Me.str().CalibrationConstant1 = CType(value, string)
												
						Case "Constant2Name"
							Me.str().Constant2Name = CType(value, string)
												
						Case "CalibrationConstant2"
							Me.str().CalibrationConstant2 = CType(value, string)
												
						Case "Constant3Name"
							Me.str().Constant3Name = CType(value, string)
												
						Case "CalibrationConstant3"
							Me.str().CalibrationConstant3 = CType(value, string)
												
						Case "SmoothingPeriod"
							Me.str().SmoothingPeriod = CType(value, string)
												
						Case "Sb11"
							Me.str().Sb11 = CType(value, string)
												
						Case "Sb12"
							Me.str().Sb12 = CType(value, string)
												
						Case "Sb21"
							Me.str().Sb21 = CType(value, string)
												
						Case "Sb22"
							Me.str().Sb22 = CType(value, string)
												
						Case "Sb31"
							Me.str().Sb31 = CType(value, string)
												
						Case "Sb32"
							Me.str().Sb32 = CType(value, string)
												
						Case "Sb41"
							Me.str().Sb41 = CType(value, string)
												
						Case "Sb42"
							Me.str().Sb42 = CType(value, string)
												
						Case "Sb51"
							Me.str().Sb51 = CType(value, string)
												
						Case "Sb52"
							Me.str().Sb52 = CType(value, string)
												
						Case "Sb61"
							Me.str().Sb61 = CType(value, string)
												
						Case "Sb62"
							Me.str().Sb62 = CType(value, string)
												
						Case "Sb71"
							Me.str().Sb71 = CType(value, string)
												
						Case "Sb72"
							Me.str().Sb72 = CType(value, string)
												
						Case "Sb81"
							Me.str().Sb81 = CType(value, string)
												
						Case "Sb82"
							Me.str().Sb82 = CType(value, string)
												
						Case "Sb91"
							Me.str().Sb91 = CType(value, string)
												
						Case "Sb92"
							Me.str().Sb92 = CType(value, string)
												
						Case "Sb101"
							Me.str().Sb101 = CType(value, string)
												
						Case "Sb102"
							Me.str().Sb102 = CType(value, string)
												
						Case "CCASPickUp1Sensitivity"
							Me.str().CCASPickUp1Sensitivity = CType(value, string)
												
						Case "CCASPickUp2Sensitivity"
							Me.str().CCASPickUp2Sensitivity = CType(value, string)
												
						Case "MinSlopeRig"
							Me.str().MinSlopeRig = CType(value, string)
					
					End Select
					
				Else
				
					Select Case name
						
						Case "ProjectID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.ProjectID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UutMetadata.PropertyNames.ProjectID)
							End If
						
						Case "Id"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.Id = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UutMetadata.PropertyNames.Id)
							End If
						
						Case "Available"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
								Me.Available = CType(value, Nullable(Of System.Boolean))
								OnPropertyChanged(UutMetadata.PropertyNames.Available)
							End If
						
						Case "SecurityLevelID"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SecurityLevelID = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UutMetadata.PropertyNames.SecurityLevelID)
							End If
						
						Case "CalibrationConstant1"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.CalibrationConstant1 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UutMetadata.PropertyNames.CalibrationConstant1)
							End If
						
						Case "CalibrationConstant2"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.CalibrationConstant2 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UutMetadata.PropertyNames.CalibrationConstant2)
							End If
						
						Case "CalibrationConstant3"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Double" Then
								Me.CalibrationConstant3 = CType(value, Nullable(Of System.Double))
								OnPropertyChanged(UutMetadata.PropertyNames.CalibrationConstant3)
							End If
						
						Case "SmoothingPeriod"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.SmoothingPeriod = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UutMetadata.PropertyNames.SmoothingPeriod)
							End If
						
						Case "CCASPickUp1Sensitivity"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Decimal" Then
								Me.CCASPickUp1Sensitivity = CType(value, Nullable(Of System.Decimal))
								OnPropertyChanged(UutMetadata.PropertyNames.CCASPickUp1Sensitivity)
							End If
						
						Case "CCASPickUp2Sensitivity"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.CCASPickUp2Sensitivity = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UutMetadata.PropertyNames.CCASPickUp2Sensitivity)
							End If
						
						Case "MinSlopeRig"
						
							If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
								Me.MinSlopeRig = CType(value, Nullable(Of System.Int32))
								OnPropertyChanged(UutMetadata.PropertyNames.MinSlopeRig)
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
		
			Public Sub New(ByVal entity As esUut)
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
		  	
			Public Property EngineFamily As System.String 
				Get
					Dim data_ As System.String = entity.EngineFamily
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.EngineFamily = Nothing
					Else
						entity.EngineFamily = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Available As System.String 
				Get
					Dim data_ As Nullable(Of System.Boolean) = entity.Available
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Available = Nothing
					Else
						entity.Available = Convert.ToBoolean(Value)
					End If
				End Set
			End Property
		  	
			Public Property SecurityLevelID As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SecurityLevelID
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SecurityLevelID = Nothing
					Else
						entity.SecurityLevelID = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Constant1Name As System.String 
				Get
					Dim data_ As System.String = entity.Constant1Name
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Constant1Name = Nothing
					Else
						entity.Constant1Name = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationConstant1 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.CalibrationConstant1
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationConstant1 = Nothing
					Else
						entity.CalibrationConstant1 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property Constant2Name As System.String 
				Get
					Dim data_ As System.String = entity.Constant2Name
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Constant2Name = Nothing
					Else
						entity.Constant2Name = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationConstant2 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.CalibrationConstant2
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationConstant2 = Nothing
					Else
						entity.CalibrationConstant2 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property Constant3Name As System.String 
				Get
					Dim data_ As System.String = entity.Constant3Name
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Constant3Name = Nothing
					Else
						entity.Constant3Name = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CalibrationConstant3 As System.String 
				Get
					Dim data_ As Nullable(Of System.Double) = entity.CalibrationConstant3
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CalibrationConstant3 = Nothing
					Else
						entity.CalibrationConstant3 = Convert.ToDouble(Value)
					End If
				End Set
			End Property
		  	
			Public Property SmoothingPeriod As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.SmoothingPeriod
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.SmoothingPeriod = Nothing
					Else
						entity.SmoothingPeriod = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb11 As System.String 
				Get
					Dim data_ As System.String = entity.Sb11
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb11 = Nothing
					Else
						entity.Sb11 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb12 As System.String 
				Get
					Dim data_ As System.String = entity.Sb12
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb12 = Nothing
					Else
						entity.Sb12 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb21 As System.String 
				Get
					Dim data_ As System.String = entity.Sb21
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb21 = Nothing
					Else
						entity.Sb21 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb22 As System.String 
				Get
					Dim data_ As System.String = entity.Sb22
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb22 = Nothing
					Else
						entity.Sb22 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb31 As System.String 
				Get
					Dim data_ As System.String = entity.Sb31
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb31 = Nothing
					Else
						entity.Sb31 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb32 As System.String 
				Get
					Dim data_ As System.String = entity.Sb32
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb32 = Nothing
					Else
						entity.Sb32 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb41 As System.String 
				Get
					Dim data_ As System.String = entity.Sb41
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb41 = Nothing
					Else
						entity.Sb41 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb42 As System.String 
				Get
					Dim data_ As System.String = entity.Sb42
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb42 = Nothing
					Else
						entity.Sb42 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb51 As System.String 
				Get
					Dim data_ As System.String = entity.Sb51
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb51 = Nothing
					Else
						entity.Sb51 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb52 As System.String 
				Get
					Dim data_ As System.String = entity.Sb52
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb52 = Nothing
					Else
						entity.Sb52 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb61 As System.String 
				Get
					Dim data_ As System.String = entity.Sb61
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb61 = Nothing
					Else
						entity.Sb61 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb62 As System.String 
				Get
					Dim data_ As System.String = entity.Sb62
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb62 = Nothing
					Else
						entity.Sb62 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb71 As System.String 
				Get
					Dim data_ As System.String = entity.Sb71
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb71 = Nothing
					Else
						entity.Sb71 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb72 As System.String 
				Get
					Dim data_ As System.String = entity.Sb72
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb72 = Nothing
					Else
						entity.Sb72 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb81 As System.String 
				Get
					Dim data_ As System.String = entity.Sb81
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb81 = Nothing
					Else
						entity.Sb81 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb82 As System.String 
				Get
					Dim data_ As System.String = entity.Sb82
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb82 = Nothing
					Else
						entity.Sb82 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb91 As System.String 
				Get
					Dim data_ As System.String = entity.Sb91
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb91 = Nothing
					Else
						entity.Sb91 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb92 As System.String 
				Get
					Dim data_ As System.String = entity.Sb92
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb92 = Nothing
					Else
						entity.Sb92 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb101 As System.String 
				Get
					Dim data_ As System.String = entity.Sb101
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb101 = Nothing
					Else
						entity.Sb101 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property Sb102 As System.String 
				Get
					Dim data_ As System.String = entity.Sb102
					
					if data_ Is Nothing Then
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.Sb102 = Nothing
					Else
						entity.Sb102 = Convert.ToString(Value)
					End If
				End Set
			End Property
		  	
			Public Property CCASPickUp1Sensitivity As System.String 
				Get
					Dim data_ As Nullable(Of System.Decimal) = entity.CCASPickUp1Sensitivity
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CCASPickUp1Sensitivity = Nothing
					Else
						entity.CCASPickUp1Sensitivity = Convert.ToDecimal(Value)
					End If
				End Set
			End Property
		  	
			Public Property CCASPickUp2Sensitivity As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.CCASPickUp2Sensitivity
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.CCASPickUp2Sensitivity = Nothing
					Else
						entity.CCASPickUp2Sensitivity = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  	
			Public Property MinSlopeRig As System.String 
				Get
					Dim data_ As Nullable(Of System.Int32) = entity.MinSlopeRig
					
					If Not data_.HasValue Then
					
						Return String.Empty
					Else
						Return Convert.ToString(data_)
					End If
				End Get

				Set(ByVal Value as System.String)
					If String.IsNullOrEmpty(value) Then
						entity.MinSlopeRig = Nothing
					Else
						entity.MinSlopeRig = Convert.ToInt32(Value)
					End If
				End Set
			End Property
		  

			Private entity As esUut
		End Class
		

        <NonSerialized> _
        <IgnoreDataMember> _		
		Private _esstrings As esStrings
		
#End Region

#Region "Housekeeping methods"

		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UutMetadata.Meta()
			End Get
		End Property

#End Region

#Region "Query Logic"

		Public ReadOnly Property Query() As UutQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UutQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property

		Public Overloads Function Load(ByVal query As UutQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Me.Query.Load()
		End Function

		Protected Sub InitQuery(ByVal query As UutQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntity).Connection
			End If
		End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As UutQuery

	End Class



	<Serializable()> _
	MustInherit Public Partial Class esUutCollection
		Inherits esEntityCollection(Of Uut)
		
		#Region "Housekeeping methods"
		Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
			Get
				Return UutMetadata.Meta()
			End Get
		End Property
		
		Protected Overloads Overrides Function GetCollectionName() As String
			Return "UutCollection"
		End Function
		
		#End Region
		
		#Region "Query Logic"
		

		<BrowsableAttribute(False)> _ 
		Public ReadOnly Property Query() As UutQuery
			Get
				If Me.m_query Is Nothing Then
					Me.m_query = New UutQuery()
					InitQuery(Me.m_query)
				End If
				
				Return Me.m_query
			End Get
		End Property
		
		Public Overloads Function Load(ByVal query As UutQuery) As Boolean
			Me.m_query = query
			InitQuery(Me.m_query)
			Return Query.Load()
		End Function
		
		Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
			If Me.m_query Is Nothing Then
				Me.m_query = New UutQuery()
				Me.InitQuery(m_query)
			End If
			Return Me.m_query
		End Function
		
		Protected Sub InitQuery(ByVal query As UutQuery)
			query.OnLoadDelegate = AddressOf OnQueryLoaded
			
			If Not query.es2.HasConnection Then
				query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
			End If
		End Sub
		
		Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
			Me.InitQuery(DirectCast(query, UutQuery))
		End Sub
		
		#End Region
						
		Private m_query As UutQuery
	End Class



	<Serializable> _
	MustInherit Public Partial Class esUutQuery 
		Inherits esDynamicQuery 
	
		Protected ReadOnly Overrides Property Meta() As IMetadata
			Get
				Return UutMetadata.Meta()
			End Get
		End Property
		
#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
				Case "ProjectID" 
					Return Me.ProjectID
				Case "EngineModel" 
					Return Me.EngineModel
				Case "Id" 
					Return Me.Id
				Case "EngineFamily" 
					Return Me.EngineFamily
				Case "Available" 
					Return Me.Available
				Case "SecurityLevelID" 
					Return Me.SecurityLevelID
				Case "Constant1Name" 
					Return Me.Constant1Name
				Case "CalibrationConstant1" 
					Return Me.CalibrationConstant1
				Case "Constant2Name" 
					Return Me.Constant2Name
				Case "CalibrationConstant2" 
					Return Me.CalibrationConstant2
				Case "Constant3Name" 
					Return Me.Constant3Name
				Case "CalibrationConstant3" 
					Return Me.CalibrationConstant3
				Case "SmoothingPeriod" 
					Return Me.SmoothingPeriod
				Case "Sb11" 
					Return Me.Sb11
				Case "Sb12" 
					Return Me.Sb12
				Case "Sb21" 
					Return Me.Sb21
				Case "Sb22" 
					Return Me.Sb22
				Case "Sb31" 
					Return Me.Sb31
				Case "Sb32" 
					Return Me.Sb32
				Case "Sb41" 
					Return Me.Sb41
				Case "Sb42" 
					Return Me.Sb42
				Case "Sb51" 
					Return Me.Sb51
				Case "Sb52" 
					Return Me.Sb52
				Case "Sb61" 
					Return Me.Sb61
				Case "Sb62" 
					Return Me.Sb62
				Case "Sb71" 
					Return Me.Sb71
				Case "Sb72" 
					Return Me.Sb72
				Case "Sb81" 
					Return Me.Sb81
				Case "Sb82" 
					Return Me.Sb82
				Case "Sb91" 
					Return Me.Sb91
				Case "Sb92" 
					Return Me.Sb92
				Case "Sb101" 
					Return Me.Sb101
				Case "Sb102" 
					Return Me.Sb102
				Case "CCASPickUp1Sensitivity" 
					Return Me.CCASPickUp1Sensitivity
				Case "CCASPickUp2Sensitivity" 
					Return Me.CCASPickUp2Sensitivity
				Case "MinSlopeRig" 
					Return Me.MinSlopeRig
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region		
		
#Region "esQueryItems"


		Public ReadOnly Property ProjectID As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.ProjectID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineModel As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.EngineModel, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Id As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Id, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property EngineFamily As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.EngineFamily, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Available As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Available, esSystemType.Boolean)
			End Get
		End Property 
		
		Public ReadOnly Property SecurityLevelID As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.SecurityLevelID, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Constant1Name As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Constant1Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationConstant1 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.CalibrationConstant1, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Constant2Name As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Constant2Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationConstant2 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.CalibrationConstant2, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property Constant3Name As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Constant3Name, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CalibrationConstant3 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.CalibrationConstant3, esSystemType.Double)
			End Get
		End Property 
		
		Public ReadOnly Property SmoothingPeriod As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.SmoothingPeriod, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property Sb11 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb11, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb12 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb12, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb21 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb21, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb22 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb22, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb31 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb31, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb32 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb32, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb41 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb41, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb42 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb42, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb51 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb51, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb52 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb52, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb61 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb61, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb62 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb62, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb71 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb71, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb72 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb72, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb81 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb81, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb82 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb82, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb91 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb91, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb92 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb92, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb101 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb101, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property Sb102 As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.Sb102, esSystemType.String)
			End Get
		End Property 
		
		Public ReadOnly Property CCASPickUp1Sensitivity As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.CCASPickUp1Sensitivity, esSystemType.Decimal)
			End Get
		End Property 
		
		Public ReadOnly Property CCASPickUp2Sensitivity As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.CCASPickUp2Sensitivity, esSystemType.Int32)
			End Get
		End Property 
		
		Public ReadOnly Property MinSlopeRig As esQueryItem
			Get
				Return New esQueryItem(Me, UutMetadata.ColumnNames.MinSlopeRig, esSystemType.Int32)
			End Get
		End Property 
		
#End Region	
		
	End Class


	
	Partial Public Class Uut 
		Inherits esUut
		
	
		#Region "CustomParametersCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_CustomParametersCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Uut.CustomParametersCollectionByProjectID_Delegate)
				map.PropertyName = "CustomParametersCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel"
				map.ParentColumnName = "ProjectID,EngineModel"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub CustomParametersCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UutQuery(data.NextAlias())
			
			Dim mee As CustomParametersQuery = If(data.You IsNot Nothing, TryCast(data.You, CustomParametersQuery), New CustomParametersQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUTCustom_Parameters
		''' </summary>

		<XmlIgnore()> _ 
		Public Property CustomParametersCollectionByProjectID As CustomParametersCollection 
		
			Get
				If Me._CustomParametersCollectionByProjectID Is Nothing Then
					Me._CustomParametersCollectionByProjectID = New CustomParametersCollection()
					Me._CustomParametersCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("CustomParametersCollectionByProjectID", Me._CustomParametersCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._CustomParametersCollectionByProjectID.Query.Where(Me._CustomParametersCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._CustomParametersCollectionByProjectID.Query.Where(Me._CustomParametersCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._CustomParametersCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._CustomParametersCollectionByProjectID.fks.Add(CustomParametersMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._CustomParametersCollectionByProjectID.fks.Add(CustomParametersMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._CustomParametersCollectionByProjectID
			End Get
			
			Set(ByVal value As CustomParametersCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._CustomParametersCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("CustomParametersCollectionByProjectID")
					Me._CustomParametersCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _CustomParametersCollectionByProjectID As CustomParametersCollection
		#End Region

		#Region "UpToFlowmeterMapCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - UUTFlowmeter
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToFlowmeterMapCollection As FlowmeterMapCollection
		
			Get
				If Me._UpToFlowmeterMapCollection Is Nothing Then
					Me._UpToFlowmeterMapCollection = New FlowmeterMapCollection()
					Me._UpToFlowmeterMapCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToFlowmeterMapCollection", Me._UpToFlowmeterMapCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.ProjectID.Equals(Nothing) Then 
				
						Dim m As New FlowmeterMapQuery("m")
						Dim j As New FlowmeterQuery("j")
						m.Select(m)
						m.InnerJoin(j).On(m.Id = j.FlowmeterMapID)
                        m.Where(j.ProjectID = Me.ProjectID)
                        m.Where(j.EngineModel = Me.EngineModel)

						Me._UpToFlowmeterMapCollection.Load(m)

					End If
				End If

				Return Me._UpToFlowmeterMapCollection
			End Get
			
			Set(ByVal value As FlowmeterMapCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToFlowmeterMapCollection Is Nothing Then

					Me.RemovePostSave("UpToFlowmeterMapCollection")
					Me._UpToFlowmeterMapCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - UUTFlowmeter
		''' </summary>
		Public Sub AssociateFlowmeterMapCollection(entity As FlowmeterMap)
			If Me._FlowmeterCollection Is Nothing Then
				Me._FlowmeterCollection = New FlowmeterCollection()
				Me._FlowmeterCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("FlowmeterCollection", Me._FlowmeterCollection)
			End If
			
			Dim obj As Flowmeter = Me._FlowmeterCollection.AddNew()
			obj.ProjectID = Me.ProjectID
			obj.EngineModel = Me.EngineModel
			obj.FlowmeterMapID = entity.Id			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - UUTFlowmeter
		''' </summary>
		Public Sub DissociateFlowmeterMapCollection(entity As FlowmeterMap)
			If Me._FlowmeterCollection Is Nothing Then
				Me._FlowmeterCollection = new FlowmeterCollection()
				Me._FlowmeterCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("FlowmeterCollection", Me._FlowmeterCollection)
			End If

			Dim obj As Flowmeter = Me._FlowmeterCollection.AddNew()
			obj.ProjectID = Me.ProjectID
			obj.EngineModel = Me.EngineModel
            obj.FlowmeterMapID = entity.Id
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToFlowmeterMapCollection As FlowmeterMapCollection
		Private _FlowmeterCollection As FlowmeterCollection
		#End Region

		#Region "FlowmeterCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_FlowmeterCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Uut.FlowmeterCollectionByProjectID_Delegate)
				map.PropertyName = "FlowmeterCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel"
				map.ParentColumnName = "ProjectID,EngineModel"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub FlowmeterCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UutQuery(data.NextAlias())
			
			Dim mee As FlowmeterQuery = If(data.You IsNot Nothing, TryCast(data.You, FlowmeterQuery), New FlowmeterQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUTFlowmeter
		''' </summary>

		<XmlIgnore()> _ 
		Public Property FlowmeterCollectionByProjectID As FlowmeterCollection 
		
			Get
				If Me._FlowmeterCollectionByProjectID Is Nothing Then
					Me._FlowmeterCollectionByProjectID = New FlowmeterCollection()
					Me._FlowmeterCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("FlowmeterCollectionByProjectID", Me._FlowmeterCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._FlowmeterCollectionByProjectID.Query.Where(Me._FlowmeterCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._FlowmeterCollectionByProjectID.Query.Where(Me._FlowmeterCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._FlowmeterCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._FlowmeterCollectionByProjectID.fks.Add(FlowmeterMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._FlowmeterCollectionByProjectID.fks.Add(FlowmeterMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._FlowmeterCollectionByProjectID
			End Get
			
			Set(ByVal value As FlowmeterCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._FlowmeterCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("FlowmeterCollectionByProjectID")
					Me._FlowmeterCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _FlowmeterCollectionByProjectID As FlowmeterCollection
		#End Region

		#Region "LogReportDetailCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_LogReportDetailCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Uut.LogReportDetailCollectionByProjectID_Delegate)
				map.PropertyName = "LogReportDetailCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel"
				map.ParentColumnName = "ProjectID,EngineModel"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub LogReportDetailCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UutQuery(data.NextAlias())
			
			Dim mee As LogReportDetailQuery = If(data.You IsNot Nothing, TryCast(data.You, LogReportDetailQuery), New LogReportDetailQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUTLog_Report_Detail
		''' </summary>

		<XmlIgnore()> _ 
		Public Property LogReportDetailCollectionByProjectID As LogReportDetailCollection 
		
			Get
				If Me._LogReportDetailCollectionByProjectID Is Nothing Then
					Me._LogReportDetailCollectionByProjectID = New LogReportDetailCollection()
					Me._LogReportDetailCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("LogReportDetailCollectionByProjectID", Me._LogReportDetailCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._LogReportDetailCollectionByProjectID.Query.Where(Me._LogReportDetailCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._LogReportDetailCollectionByProjectID.Query.Where(Me._LogReportDetailCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._LogReportDetailCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._LogReportDetailCollectionByProjectID.fks.Add(LogReportDetailMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._LogReportDetailCollectionByProjectID.fks.Add(LogReportDetailMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._LogReportDetailCollectionByProjectID
			End Get
			
			Set(ByVal value As LogReportDetailCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._LogReportDetailCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("LogReportDetailCollectionByProjectID")
					Me._LogReportDetailCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _LogReportDetailCollectionByProjectID As LogReportDetailCollection
		#End Region

		#Region "UUTChoiceCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTChoiceCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Uut.UUTChoiceCollectionByProjectID_Delegate)
				map.PropertyName = "UUTChoiceCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel"
				map.ParentColumnName = "ProjectID,EngineModel"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub UUTChoiceCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UutQuery(data.NextAlias())
			
			Dim mee As UUTChoiceQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTChoiceQuery), New UUTChoiceQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUTUUT_Choice
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTChoiceCollectionByProjectID As UUTChoiceCollection 
		
			Get
				If Me._UUTChoiceCollectionByProjectID Is Nothing Then
					Me._UUTChoiceCollectionByProjectID = New UUTChoiceCollection()
					Me._UUTChoiceCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTChoiceCollectionByProjectID", Me._UUTChoiceCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTChoiceCollectionByProjectID.Query.Where(Me._UUTChoiceCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UUTChoiceCollectionByProjectID.Query.Where(Me._UUTChoiceCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._UUTChoiceCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTChoiceCollectionByProjectID.fks.Add(UUTChoiceMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._UUTChoiceCollectionByProjectID.fks.Add(UUTChoiceMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._UUTChoiceCollectionByProjectID
			End Get
			
			Set(ByVal value As UUTChoiceCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTChoiceCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UUTChoiceCollectionByProjectID")
					Me._UUTChoiceCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTChoiceCollectionByProjectID As UUTChoiceCollection
		#End Region

		#Region "UpToDynamicCollectionCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - UUTUUT_Dynamic_Channel_Collection
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToDynamicCollectionCollection As DynamicCollectionCollection
		
			Get
				If Me._UpToDynamicCollectionCollection Is Nothing Then
					Me._UpToDynamicCollectionCollection = New DynamicCollectionCollection()
					Me._UpToDynamicCollectionCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToDynamicCollectionCollection", Me._UpToDynamicCollectionCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.ProjectID.Equals(Nothing) Then 
				
						Dim m As New DynamicCollectionQuery("m")
						Dim j As New UUTDynamicChannelCollectionQuery("j")
						m.Select(m)
						m.InnerJoin(j).On(m.CollectionName = j.CollectionName)
                        m.Where(j.ProjectID = Me.ProjectID)
                        m.Where(j.EngineModel = Me.EngineModel)

						Me._UpToDynamicCollectionCollection.Load(m)

					End If
				End If

				Return Me._UpToDynamicCollectionCollection
			End Get
			
			Set(ByVal value As DynamicCollectionCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToDynamicCollectionCollection Is Nothing Then

					Me.RemovePostSave("UpToDynamicCollectionCollection")
					Me._UpToDynamicCollectionCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - UUTUUT_Dynamic_Channel_Collection
		''' </summary>
		Public Sub AssociateDynamicCollectionCollection(entity As DynamicCollection)
			If Me._UUTDynamicChannelCollectionCollection Is Nothing Then
				Me._UUTDynamicChannelCollectionCollection = New UUTDynamicChannelCollectionCollection()
				Me._UUTDynamicChannelCollectionCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UUTDynamicChannelCollectionCollection", Me._UUTDynamicChannelCollectionCollection)
			End If
			
			Dim obj As UUTDynamicChannelCollection = Me._UUTDynamicChannelCollectionCollection.AddNew()
			obj.ProjectID = Me.ProjectID
			obj.EngineModel = Me.EngineModel
			obj.CollectionName = entity.CollectionName			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - UUTUUT_Dynamic_Channel_Collection
		''' </summary>
		Public Sub DissociateDynamicCollectionCollection(entity As DynamicCollection)
			If Me._UUTDynamicChannelCollectionCollection Is Nothing Then
				Me._UUTDynamicChannelCollectionCollection = new UUTDynamicChannelCollectionCollection()
				Me._UUTDynamicChannelCollectionCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UUTDynamicChannelCollectionCollection", Me._UUTDynamicChannelCollectionCollection)
			End If

			Dim obj As UUTDynamicChannelCollection = Me._UUTDynamicChannelCollectionCollection.AddNew()
			obj.ProjectID = Me.ProjectID
			obj.EngineModel = Me.EngineModel
            obj.CollectionName = entity.CollectionName
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToDynamicCollectionCollection As DynamicCollectionCollection
		Private _UUTDynamicChannelCollectionCollection As UUTDynamicChannelCollectionCollection
		#End Region

		#Region "UUTDynamicChannelCollectionCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTDynamicChannelCollectionCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Uut.UUTDynamicChannelCollectionCollectionByProjectID_Delegate)
				map.PropertyName = "UUTDynamicChannelCollectionCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel"
				map.ParentColumnName = "ProjectID,EngineModel"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub UUTDynamicChannelCollectionCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UutQuery(data.NextAlias())
			
			Dim mee As UUTDynamicChannelCollectionQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTDynamicChannelCollectionQuery), New UUTDynamicChannelCollectionQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUTUUT_Dynamic_Channel_Collection
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTDynamicChannelCollectionCollectionByProjectID As UUTDynamicChannelCollectionCollection 
		
			Get
				If Me._UUTDynamicChannelCollectionCollectionByProjectID Is Nothing Then
					Me._UUTDynamicChannelCollectionCollectionByProjectID = New UUTDynamicChannelCollectionCollection()
					Me._UUTDynamicChannelCollectionCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTDynamicChannelCollectionCollectionByProjectID", Me._UUTDynamicChannelCollectionCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTDynamicChannelCollectionCollectionByProjectID.Query.Where(Me._UUTDynamicChannelCollectionCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UUTDynamicChannelCollectionCollectionByProjectID.Query.Where(Me._UUTDynamicChannelCollectionCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._UUTDynamicChannelCollectionCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTDynamicChannelCollectionCollectionByProjectID.fks.Add(UUTDynamicChannelCollectionMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._UUTDynamicChannelCollectionCollectionByProjectID.fks.Add(UUTDynamicChannelCollectionMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._UUTDynamicChannelCollectionCollectionByProjectID
			End Get
			
			Set(ByVal value As UUTDynamicChannelCollectionCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTDynamicChannelCollectionCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UUTDynamicChannelCollectionCollectionByProjectID")
					Me._UUTDynamicChannelCollectionCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTDynamicChannelCollectionCollectionByProjectID As UUTDynamicChannelCollectionCollection
		#End Region

		#Region "UUTHMIWindowCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTHMIWindowCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Uut.UUTHMIWindowCollectionByProjectID_Delegate)
				map.PropertyName = "UUTHMIWindowCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel"
				map.ParentColumnName = "ProjectID,EngineModel"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub UUTHMIWindowCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UutQuery(data.NextAlias())
			
			Dim mee As UUTHMIWindowQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTHMIWindowQuery), New UUTHMIWindowQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUTUUT_HMI_Window
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTHMIWindowCollectionByProjectID As UUTHMIWindowCollection 
		
			Get
				If Me._UUTHMIWindowCollectionByProjectID Is Nothing Then
					Me._UUTHMIWindowCollectionByProjectID = New UUTHMIWindowCollection()
					Me._UUTHMIWindowCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTHMIWindowCollectionByProjectID", Me._UUTHMIWindowCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTHMIWindowCollectionByProjectID.Query.Where(Me._UUTHMIWindowCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UUTHMIWindowCollectionByProjectID.Query.Where(Me._UUTHMIWindowCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._UUTHMIWindowCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTHMIWindowCollectionByProjectID.fks.Add(UUTHMIWindowMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._UUTHMIWindowCollectionByProjectID.fks.Add(UUTHMIWindowMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._UUTHMIWindowCollectionByProjectID
			End Get
			
			Set(ByVal value As UUTHMIWindowCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTHMIWindowCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UUTHMIWindowCollectionByProjectID")
					Me._UUTHMIWindowCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTHMIWindowCollectionByProjectID As UUTHMIWindowCollection
		#End Region

		#Region "UUTLimitCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTLimitCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Uut.UUTLimitCollectionByProjectID_Delegate)
				map.PropertyName = "UUTLimitCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel"
				map.ParentColumnName = "ProjectID,EngineModel"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub UUTLimitCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UutQuery(data.NextAlias())
			
			Dim mee As UUTLimitQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTLimitQuery), New UUTLimitQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUTUUT_Limit
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTLimitCollectionByProjectID As UUTLimitCollection 
		
			Get
				If Me._UUTLimitCollectionByProjectID Is Nothing Then
					Me._UUTLimitCollectionByProjectID = New UUTLimitCollection()
					Me._UUTLimitCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTLimitCollectionByProjectID", Me._UUTLimitCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTLimitCollectionByProjectID.Query.Where(Me._UUTLimitCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UUTLimitCollectionByProjectID.Query.Where(Me._UUTLimitCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._UUTLimitCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTLimitCollectionByProjectID.fks.Add(UUTLimitMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._UUTLimitCollectionByProjectID.fks.Add(UUTLimitMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._UUTLimitCollectionByProjectID
			End Get
			
			Set(ByVal value As UUTLimitCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTLimitCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UUTLimitCollectionByProjectID")
					Me._UUTLimitCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTLimitCollectionByProjectID As UUTLimitCollection
		#End Region

		#Region "UUTOptionCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTOptionCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Uut.UUTOptionCollectionByProjectID_Delegate)
				map.PropertyName = "UUTOptionCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel"
				map.ParentColumnName = "ProjectID,EngineModel"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub UUTOptionCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UutQuery(data.NextAlias())
			
			Dim mee As UUTOptionQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTOptionQuery), New UUTOptionQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUTUUT_Option
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTOptionCollectionByProjectID As UUTOptionCollection 
		
			Get
				If Me._UUTOptionCollectionByProjectID Is Nothing Then
					Me._UUTOptionCollectionByProjectID = New UUTOptionCollection()
					Me._UUTOptionCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTOptionCollectionByProjectID", Me._UUTOptionCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTOptionCollectionByProjectID.Query.Where(Me._UUTOptionCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UUTOptionCollectionByProjectID.Query.Where(Me._UUTOptionCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._UUTOptionCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTOptionCollectionByProjectID.fks.Add(UUTOptionMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._UUTOptionCollectionByProjectID.fks.Add(UUTOptionMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._UUTOptionCollectionByProjectID
			End Get
			
			Set(ByVal value As UUTOptionCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTOptionCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UUTOptionCollectionByProjectID")
					Me._UUTOptionCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTOptionCollectionByProjectID As UUTOptionCollection
		#End Region

		#Region "UUTQuestionCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTQuestionCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Uut.UUTQuestionCollectionByProjectID_Delegate)
				map.PropertyName = "UUTQuestionCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel"
				map.ParentColumnName = "ProjectID,EngineModel"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub UUTQuestionCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UutQuery(data.NextAlias())
			
			Dim mee As UUTQuestionQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTQuestionQuery), New UUTQuestionQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUTUUT_Question
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTQuestionCollectionByProjectID As UUTQuestionCollection 
		
			Get
				If Me._UUTQuestionCollectionByProjectID Is Nothing Then
					Me._UUTQuestionCollectionByProjectID = New UUTQuestionCollection()
					Me._UUTQuestionCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTQuestionCollectionByProjectID", Me._UUTQuestionCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTQuestionCollectionByProjectID.Query.Where(Me._UUTQuestionCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UUTQuestionCollectionByProjectID.Query.Where(Me._UUTQuestionCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._UUTQuestionCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTQuestionCollectionByProjectID.fks.Add(UUTQuestionMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._UUTQuestionCollectionByProjectID.fks.Add(UUTQuestionMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._UUTQuestionCollectionByProjectID
			End Get
			
			Set(ByVal value As UUTQuestionCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTQuestionCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UUTQuestionCollectionByProjectID")
					Me._UUTQuestionCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTQuestionCollectionByProjectID As UUTQuestionCollection
		#End Region

		#Region "UpToUUTTripTypeCollection - Many To Many"
		''' <summary>
		''' Many to Many
		''' Foreign Key Name - UUTUUT_Trip
		''' </summary>

		<XmlIgnore()> _
		Public Property UpToUUTTripTypeCollection As UUTTripTypeCollection
		
			Get
				If Me._UpToUUTTripTypeCollection Is Nothing Then
					Me._UpToUUTTripTypeCollection = New UUTTripTypeCollection()
					Me._UpToUUTTripTypeCollection.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UpToUUTTripTypeCollection", Me._UpToUUTTripTypeCollection)
					If Not Me.es.IsLazyLoadDisabled And Not Me.ProjectID.Equals(Nothing) Then 
				
						Dim m As New UUTTripTypeQuery("m")
						Dim j As New UUTTripQuery("j")
						m.Select(m)
						m.InnerJoin(j).On(m.TripTypeID = j.TripTypeID)
                        m.Where(j.ProjectID = Me.ProjectID)
                        m.Where(j.EngineModel = Me.EngineModel)

						Me._UpToUUTTripTypeCollection.Load(m)

					End If
				End If

				Return Me._UpToUUTTripTypeCollection
			End Get
			
			Set(ByVal value As UUTTripTypeCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UpToUUTTripTypeCollection Is Nothing Then

					Me.RemovePostSave("UpToUUTTripTypeCollection")
					Me._UpToUUTTripTypeCollection = Nothing
					

				End If
			End Set	
			
		End Property

		''' <summary>
		''' Many to Many Associate
		''' Foreign Key Name - UUTUUT_Trip
		''' </summary>
		Public Sub AssociateUUTTripTypeCollection(entity As UUTTripType)
			If Me._UUTTripCollection Is Nothing Then
				Me._UUTTripCollection = New UUTTripCollection()
				Me._UUTTripCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UUTTripCollection", Me._UUTTripCollection)
			End If
			
			Dim obj As UUTTrip = Me._UUTTripCollection.AddNew()
			obj.ProjectID = Me.ProjectID
			obj.EngineModel = Me.EngineModel
			obj.TripTypeID = entity.TripTypeID			
			
		End Sub

		''' <summary>
		''' Many to Many Dissociate
		''' Foreign Key Name - UUTUUT_Trip
		''' </summary>
		Public Sub DissociateUUTTripTypeCollection(entity As UUTTripType)
			If Me._UUTTripCollection Is Nothing Then
				Me._UUTTripCollection = new UUTTripCollection()
				Me._UUTTripCollection.es.Connection.Name = Me.es.Connection.Name
				Me.SetPostSave("UUTTripCollection", Me._UUTTripCollection)
			End If

			Dim obj As UUTTrip = Me._UUTTripCollection.AddNew()
			obj.ProjectID = Me.ProjectID
			obj.EngineModel = Me.EngineModel
            obj.TripTypeID = entity.TripTypeID
			obj.AcceptChanges()
			obj.MarkAsDeleted()
		End Sub

		Private _UpToUUTTripTypeCollection As UUTTripTypeCollection
		Private _UUTTripCollection As UUTTripCollection
		#End Region

		#Region "UUTTripCollectionByProjectID - Zero To Many"
		
		Public Shared ReadOnly Property Prefetch_UUTTripCollectionByProjectID() As esPrefetchMap
			Get
				Dim map As New esPrefetchMap()
				map.PrefetchDelegate = New esPrefetchDelegate(AddressOf BusinessObjects.Uut.UUTTripCollectionByProjectID_Delegate)
				map.PropertyName = "UUTTripCollectionByProjectID"
				map.MyColumnName = "ProjectID,EngineModel"
				map.ParentColumnName = "ProjectID,EngineModel"
				map.IsMultiPartKey = true
				Return map
			End Get
		End Property

		Private Shared Sub UUTTripCollectionByProjectID_Delegate(ByVal data As esPrefetchParameters)
		
			Dim parent As New UutQuery(data.NextAlias())
			
			Dim mee As UUTTripQuery = If(data.You IsNot Nothing, TryCast(data.You, UUTTripQuery), New UUTTripQuery(data.NextAlias()))

			If data.Root Is Nothing Then
				data.Root = mee
			End If
			
			data.Root.InnerJoin(parent).On(parent.ProjectID = mee.ProjectID And parent.EngineModel = mee.EngineModel)

			data.You = parent
			
		End Sub		

		''' <summary>
		''' Zero to Many
		''' Foreign Key Name - UUTUUT_Trip
		''' </summary>

		<XmlIgnore()> _ 
		Public Property UUTTripCollectionByProjectID As UUTTripCollection 
		
			Get
				If Me._UUTTripCollectionByProjectID Is Nothing Then
					Me._UUTTripCollectionByProjectID = New UUTTripCollection()
					Me._UUTTripCollectionByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPostSave("UUTTripCollectionByProjectID", Me._UUTTripCollectionByProjectID)
				
					If Not Me.ProjectID.Equals(Nothing) AndAlso Not Me.EngineModel.Equals(Nothing) Then
					
						If Not Me.es.IsLazyLoadDisabled Then
						
							Me._UUTTripCollectionByProjectID.Query.Where(Me._UUTTripCollectionByProjectID.Query.ProjectID = Me.ProjectID)
							Me._UUTTripCollectionByProjectID.Query.Where(Me._UUTTripCollectionByProjectID.Query.EngineModel = Me.EngineModel)
							Me._UUTTripCollectionByProjectID.Query.Load()
						End If

						' Auto-hookup Foreign Keys
						Me._UUTTripCollectionByProjectID.fks.Add(UUTTripMetadata.ColumnNames.ProjectID, Me.ProjectID)
						Me._UUTTripCollectionByProjectID.fks.Add(UUTTripMetadata.ColumnNames.EngineModel, Me.EngineModel)
					End If
				End If

				Return Me._UUTTripCollectionByProjectID
			End Get
			
			Set(ByVal value As UUTTripCollection)
				If Not value Is Nothing Then Throw New Exception("'value' Must be null")

				If Not Me._UUTTripCollectionByProjectID Is Nothing Then

					Me.RemovePostSave("UUTTripCollectionByProjectID")
					Me._UUTTripCollectionByProjectID = Nothing
					

				End If
			End Set				
			
		End Property
		

		private _UUTTripCollectionByProjectID As UUTTripCollection
		#End Region

		#Region "UpToEngineInfoByEngineModel - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Engine_InfoUUT
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToEngineInfoByEngineModel As EngineInfo
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToEngineInfoByEngineModel Is Nothing _
						 AndAlso Not EngineModel.Equals(Nothing)  Then
						
					Me._UpToEngineInfoByEngineModel = New EngineInfo()
					Me._UpToEngineInfoByEngineModel.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToEngineInfoByEngineModel", Me._UpToEngineInfoByEngineModel)
					Me._UpToEngineInfoByEngineModel.Query.Where(Me._UpToEngineInfoByEngineModel.Query.EngineModel = Me.EngineModel)
					Me._UpToEngineInfoByEngineModel.Query.Load()
				End If

				Return Me._UpToEngineInfoByEngineModel
			End Get
			
            Set(ByVal value As EngineInfo)
				Me.RemovePreSave("UpToEngineInfoByEngineModel")
				

				If value Is Nothing Then
				
					Me.EngineModel = Nothing
				
					Me._UpToEngineInfoByEngineModel = Nothing
				Else
				
					Me.EngineModel = value.EngineModel
					
					Me._UpToEngineInfoByEngineModel = value
					Me.SetPreSave("UpToEngineInfoByEngineModel", Me._UpToEngineInfoByEngineModel)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToHMIPageMappingByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - HMI_Page_MappingUUT
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToHMIPageMappingByProjectID As HMIPageMapping
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToHMIPageMappingByProjectID Is Nothing _
						 AndAlso Not ProjectID.Equals(Nothing)  Then
						
					Me._UpToHMIPageMappingByProjectID = New HMIPageMapping()
					Me._UpToHMIPageMappingByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToHMIPageMappingByProjectID", Me._UpToHMIPageMappingByProjectID)
					Me._UpToHMIPageMappingByProjectID.Query.Where(Me._UpToHMIPageMappingByProjectID.Query.ProjectID = Me.ProjectID)
					Me._UpToHMIPageMappingByProjectID.Query.Load()
				End If

				Return Me._UpToHMIPageMappingByProjectID
			End Get
			
            Set(ByVal value As HMIPageMapping)
				Me.RemovePreSave("UpToHMIPageMappingByProjectID")
				

				If value Is Nothing Then
				
					Me.ProjectID = Nothing
				
					Me._UpToHMIPageMappingByProjectID = Nothing
				Else
				
					Me.ProjectID = value.ProjectID
					
					Me._UpToHMIPageMappingByProjectID = value
					Me.SetPreSave("UpToHMIPageMappingByProjectID", Me._UpToHMIPageMappingByProjectID)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToProjectInfoByProjectID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Project_InfoUUT
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToProjectInfoByProjectID As ProjectInfo
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToProjectInfoByProjectID Is Nothing _
						 AndAlso Not ProjectID.Equals(Nothing)  Then
						
					Me._UpToProjectInfoByProjectID = New ProjectInfo()
					Me._UpToProjectInfoByProjectID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToProjectInfoByProjectID", Me._UpToProjectInfoByProjectID)
					Me._UpToProjectInfoByProjectID.Query.Where(Me._UpToProjectInfoByProjectID.Query.ProjectID = Me.ProjectID)
					Me._UpToProjectInfoByProjectID.Query.Load()
				End If

				Return Me._UpToProjectInfoByProjectID
			End Get
			
            Set(ByVal value As ProjectInfo)
				Me.RemovePreSave("UpToProjectInfoByProjectID")
				

				If value Is Nothing Then
				
					Me.ProjectID = Nothing
				
					Me._UpToProjectInfoByProjectID = Nothing
				Else
				
					Me.ProjectID = value.ProjectID
					
					Me._UpToProjectInfoByProjectID = value
					Me.SetPreSave("UpToProjectInfoByProjectID", Me._UpToProjectInfoByProjectID)
				End If
				
			End Set	

		End Property
		#End Region

		#Region "UpToSecurityLevelBySecurityLevelID - Many To One"
		''' <summary>
		''' Many to One
		''' Foreign Key Name - Security LevelUUT
		''' </summary>

		<XmlIgnore()> _		
		Public Property UpToSecurityLevelBySecurityLevelID As SecurityLevel
		
			Get
				If Me.es.IsLazyLoadDisabled Then return Nothing
				
				If Me._UpToSecurityLevelBySecurityLevelID Is Nothing _
						 AndAlso Not SecurityLevelID.Equals(Nothing)  Then
						
					Me._UpToSecurityLevelBySecurityLevelID = New SecurityLevel()
					Me._UpToSecurityLevelBySecurityLevelID.es.Connection.Name = Me.es.Connection.Name
					Me.SetPreSave("UpToSecurityLevelBySecurityLevelID", Me._UpToSecurityLevelBySecurityLevelID)
					Me._UpToSecurityLevelBySecurityLevelID.Query.Where(Me._UpToSecurityLevelBySecurityLevelID.Query.SecurityLevelID = Me.SecurityLevelID)
					Me._UpToSecurityLevelBySecurityLevelID.Query.Load()
				End If

				Return Me._UpToSecurityLevelBySecurityLevelID
			End Get
			
            Set(ByVal value As SecurityLevel)
				Me.RemovePreSave("UpToSecurityLevelBySecurityLevelID")
				

				If value Is Nothing Then
				
					Me.SecurityLevelID = Nothing
				
					Me._UpToSecurityLevelBySecurityLevelID = Nothing
				Else
				
					Me.SecurityLevelID = value.SecurityLevelID
					
					Me._UpToSecurityLevelBySecurityLevelID = value
					Me.SetPreSave("UpToSecurityLevelBySecurityLevelID", Me._UpToSecurityLevelBySecurityLevelID)
				End If
				
			End Set	

		End Property
		#End Region

		
		
		
		Protected Overrides Function CreateCollectionForPrefetch(name As String) As esEntityCollectionBase
			Dim coll As esEntityCollectionBase = Nothing

			Select Case name
			
				Case "CustomParametersCollectionByProjectID"
					coll = Me.CustomParametersCollectionByProjectID
					Exit Select
				Case "FlowmeterCollectionByProjectID"
					coll = Me.FlowmeterCollectionByProjectID
					Exit Select
				Case "LogReportDetailCollectionByProjectID"
					coll = Me.LogReportDetailCollectionByProjectID
					Exit Select
				Case "UUTChoiceCollectionByProjectID"
					coll = Me.UUTChoiceCollectionByProjectID
					Exit Select
				Case "UUTDynamicChannelCollectionCollectionByProjectID"
					coll = Me.UUTDynamicChannelCollectionCollectionByProjectID
					Exit Select
				Case "UUTHMIWindowCollectionByProjectID"
					coll = Me.UUTHMIWindowCollectionByProjectID
					Exit Select
				Case "UUTLimitCollectionByProjectID"
					coll = Me.UUTLimitCollectionByProjectID
					Exit Select
				Case "UUTOptionCollectionByProjectID"
					coll = Me.UUTOptionCollectionByProjectID
					Exit Select
				Case "UUTQuestionCollectionByProjectID"
					coll = Me.UUTQuestionCollectionByProjectID
					Exit Select
				Case "UUTTripCollectionByProjectID"
					coll = Me.UUTTripCollectionByProjectID
					Exit Select	
			End Select

			Return coll
		End Function
					
		''' <summary>
		''' Used internally by the entity's hierarchical properties.
		''' </summary>
		Protected Overrides Function GetHierarchicalProperties() As List(Of esPropertyDescriptor)
		
			Dim props As New List(Of esPropertyDescriptor)
			props.Add(new esPropertyDescriptor(Me, "CustomParametersCollectionByProjectID", GetType(CustomParametersCollection), New CustomParameters()))
			props.Add(new esPropertyDescriptor(Me, "FlowmeterCollectionByProjectID", GetType(FlowmeterCollection), New Flowmeter()))
			props.Add(new esPropertyDescriptor(Me, "LogReportDetailCollectionByProjectID", GetType(LogReportDetailCollection), New LogReportDetail()))
			props.Add(new esPropertyDescriptor(Me, "UUTChoiceCollectionByProjectID", GetType(UUTChoiceCollection), New UUTChoice()))
			props.Add(new esPropertyDescriptor(Me, "UUTDynamicChannelCollectionCollectionByProjectID", GetType(UUTDynamicChannelCollectionCollection), New UUTDynamicChannelCollection()))
			props.Add(new esPropertyDescriptor(Me, "UUTHMIWindowCollectionByProjectID", GetType(UUTHMIWindowCollection), New UUTHMIWindow()))
			props.Add(new esPropertyDescriptor(Me, "UUTLimitCollectionByProjectID", GetType(UUTLimitCollection), New UUTLimit()))
			props.Add(new esPropertyDescriptor(Me, "UUTOptionCollectionByProjectID", GetType(UUTOptionCollection), New UUTOption()))
			props.Add(new esPropertyDescriptor(Me, "UUTQuestionCollectionByProjectID", GetType(UUTQuestionCollection), New UUTQuestion()))
			props.Add(new esPropertyDescriptor(Me, "UUTTripCollectionByProjectID", GetType(UUTTripCollection), New UUTTrip()))
			Return props
			
		End Function	
		''' <summary>
		''' Used internally for retrieving AutoIncrementing keys
		''' during hierarchical PreSave.
		''' </summary>
		Protected Overrides Sub ApplyPreSaveKeys()
		
			If Not Me.es.IsDeleted And Not Me._UpToSecurityLevelBySecurityLevelID Is Nothing Then
				Me.SecurityLevelID = Me._UpToSecurityLevelBySecurityLevelID.SecurityLevelID
			End If
			
		End Sub
	End Class
		



	<Serializable> _
	Partial Public Class UutMetadata 
		Inherits esMetadata
		Implements IMetadata
		
#Region "Protected Constructor"
		Protected Sub New()
			m_columns = New esColumnMetadataCollection()
			Dim c as esColumnMetadata

			c = New esColumnMetadata(UutMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UutMetadata.PropertyNames.ProjectID
			c.IsInPrimaryKey = True
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.EngineModel
			c.IsInPrimaryKey = True
			c.CharacterMaxLength = 32
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Id, 2, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UutMetadata.PropertyNames.Id
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.EngineFamily, 3, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.EngineFamily
			c.CharacterMaxLength = 255
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Available, 4, GetType(System.Boolean), esSystemType.Boolean)	
			c.PropertyName = UutMetadata.PropertyNames.Available
			c.CharacterMaxLength = 2
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.SecurityLevelID, 5, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UutMetadata.PropertyNames.SecurityLevelID
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Constant1Name, 6, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Constant1Name
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.CalibrationConstant1, 7, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UutMetadata.PropertyNames.CalibrationConstant1
			c.NumericPrecision = 15
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Constant2Name, 8, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Constant2Name
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.CalibrationConstant2, 9, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UutMetadata.PropertyNames.CalibrationConstant2
			c.NumericPrecision = 15
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Constant3Name, 10, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Constant3Name
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.CalibrationConstant3, 11, GetType(System.Double), esSystemType.Double)	
			c.PropertyName = UutMetadata.PropertyNames.CalibrationConstant3
			c.NumericPrecision = 15
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.SmoothingPeriod, 12, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UutMetadata.PropertyNames.SmoothingPeriod
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb11, 13, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb11
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb12, 14, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb12
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb21, 15, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb21
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb22, 16, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb22
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb31, 17, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb31
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb32, 18, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb32
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb41, 19, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb41
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb42, 20, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb42
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb51, 21, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb51
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb52, 22, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb52
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb61, 23, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb61
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb62, 24, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb62
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb71, 25, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb71
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb72, 26, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb72
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb81, 27, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb81
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb82, 28, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb82
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb91, 29, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb91
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb92, 30, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb92
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb101, 31, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb101
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.Sb102, 32, GetType(System.String), esSystemType.String)	
			c.PropertyName = UutMetadata.PropertyNames.Sb102
			c.CharacterMaxLength = 50
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.CCASPickUp1Sensitivity, 33, GetType(System.Decimal), esSystemType.Decimal)	
			c.PropertyName = UutMetadata.PropertyNames.CCASPickUp1Sensitivity
			c.NumericPrecision = 18
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.CCASPickUp2Sensitivity, 34, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UutMetadata.PropertyNames.CCASPickUp2Sensitivity
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
			c = New esColumnMetadata(UutMetadata.ColumnNames.MinSlopeRig, 35, GetType(System.Int32), esSystemType.Int32)	
			c.PropertyName = UutMetadata.PropertyNames.MinSlopeRig
			c.NumericPrecision = 10
			c.IsNullable = True
			m_columns.Add(c)
				
		End Sub
#End Region	
	
		Shared Public Function Meta() As UutMetadata
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
			 Public Const Id As String = "ID"
			 Public Const EngineFamily As String = "EngineFamily"
			 Public Const Available As String = "Available"
			 Public Const SecurityLevelID As String = "SecurityLevelID"
			 Public Const Constant1Name As String = "Constant1_Name"
			 Public Const CalibrationConstant1 As String = "Calibration_constant_1"
			 Public Const Constant2Name As String = "Constant2_Name"
			 Public Const CalibrationConstant2 As String = "Calibration_constant_2"
			 Public Const Constant3Name As String = "Constant3_Name"
			 Public Const CalibrationConstant3 As String = "Calibration_Constant_3"
			 Public Const SmoothingPeriod As String = "Smoothing_Period"
			 Public Const Sb11 As String = "SB11"
			 Public Const Sb12 As String = "SB12"
			 Public Const Sb21 As String = "SB21"
			 Public Const Sb22 As String = "SB22"
			 Public Const Sb31 As String = "SB31"
			 Public Const Sb32 As String = "SB32"
			 Public Const Sb41 As String = "SB41"
			 Public Const Sb42 As String = "SB42"
			 Public Const Sb51 As String = "SB51"
			 Public Const Sb52 As String = "SB52"
			 Public Const Sb61 As String = "SB61"
			 Public Const Sb62 As String = "SB62"
			 Public Const Sb71 As String = "SB71"
			 Public Const Sb72 As String = "SB72"
			 Public Const Sb81 As String = "SB81"
			 Public Const Sb82 As String = "SB82"
			 Public Const Sb91 As String = "SB91"
			 Public Const Sb92 As String = "SB92"
			 Public Const Sb101 As String = "SB101"
			 Public Const Sb102 As String = "SB102"
			 Public Const CCASPickUp1Sensitivity As String = "CCAS_PickUp1_Sensitivity"
			 Public Const CCASPickUp2Sensitivity As String = "CCAS_PickUp2_Sensitivity"
			 Public Const MinSlopeRig As String = "MinSlopeRig"
		End Class
#End Region	
		
#Region "PropertyNames"
		Public Class  PropertyNames
			 Public Const ProjectID As String = "ProjectID"
			 Public Const EngineModel As String = "EngineModel"
			 Public Const Id As String = "Id"
			 Public Const EngineFamily As String = "EngineFamily"
			 Public Const Available As String = "Available"
			 Public Const SecurityLevelID As String = "SecurityLevelID"
			 Public Const Constant1Name As String = "Constant1Name"
			 Public Const CalibrationConstant1 As String = "CalibrationConstant1"
			 Public Const Constant2Name As String = "Constant2Name"
			 Public Const CalibrationConstant2 As String = "CalibrationConstant2"
			 Public Const Constant3Name As String = "Constant3Name"
			 Public Const CalibrationConstant3 As String = "CalibrationConstant3"
			 Public Const SmoothingPeriod As String = "SmoothingPeriod"
			 Public Const Sb11 As String = "Sb11"
			 Public Const Sb12 As String = "Sb12"
			 Public Const Sb21 As String = "Sb21"
			 Public Const Sb22 As String = "Sb22"
			 Public Const Sb31 As String = "Sb31"
			 Public Const Sb32 As String = "Sb32"
			 Public Const Sb41 As String = "Sb41"
			 Public Const Sb42 As String = "Sb42"
			 Public Const Sb51 As String = "Sb51"
			 Public Const Sb52 As String = "Sb52"
			 Public Const Sb61 As String = "Sb61"
			 Public Const Sb62 As String = "Sb62"
			 Public Const Sb71 As String = "Sb71"
			 Public Const Sb72 As String = "Sb72"
			 Public Const Sb81 As String = "Sb81"
			 Public Const Sb82 As String = "Sb82"
			 Public Const Sb91 As String = "Sb91"
			 Public Const Sb92 As String = "Sb92"
			 Public Const Sb101 As String = "Sb101"
			 Public Const Sb102 As String = "Sb102"
			 Public Const CCASPickUp1Sensitivity As String = "CCASPickUp1Sensitivity"
			 Public Const CCASPickUp2Sensitivity As String = "CCASPickUp2Sensitivity"
			 Public Const MinSlopeRig As String = "MinSlopeRig"
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
			SyncLock GetType(UutMetadata)
			
				If UutMetadata.mapDelegates Is Nothing Then
					UutMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
				End If			

				If UutMetadata._meta Is Nothing Then
					UutMetadata._meta = New UutMetadata()
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
				meta.AddTypeMap("Id", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("EngineFamily", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Available", new esTypeMap("Yes/No", "System.Boolean"))
				meta.AddTypeMap("SecurityLevelID", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Constant1Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationConstant1", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Constant2Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationConstant2", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("Constant3Name", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CalibrationConstant3", new esTypeMap("Double", "System.Double"))
				meta.AddTypeMap("SmoothingPeriod", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("Sb11", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb12", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb21", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb22", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb31", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb32", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb41", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb42", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb51", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb52", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb61", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb62", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb71", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb72", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb81", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb82", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb91", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb92", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb101", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("Sb102", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("CCASPickUp1Sensitivity", new esTypeMap("Decimal", "System.Decimal"))
				meta.AddTypeMap("CCASPickUp2Sensitivity", new esTypeMap("Long", "System.Int32"))
				meta.AddTypeMap("MinSlopeRig", new esTypeMap("Long", "System.Int32"))			
				
				
				 
				meta.Source = "UUT"
				meta.Destination = "UUT"
				
				meta.spInsert = "proc_UUTInsert"
				meta.spUpdate = "proc_UUTUpdate"
				meta.spDelete = "proc_UUTDelete"
				meta.spLoadAll = "proc_UUTLoadAll"
				meta.spLoadByPrimaryKey = "proc_UUTLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As UutMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

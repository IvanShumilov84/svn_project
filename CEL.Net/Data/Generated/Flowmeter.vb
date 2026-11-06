
'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 18/01/2018 9:46:40 AM
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
    ' Encapsulates the 'Flowmeter' table
    ' </summary>

    <System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _
    <Serializable> _
    <DataContract> _
    <KnownType(GetType(Flowmeter))> _
    <XmlType("Flowmeter")> _
    Partial Public Class Flowmeter
        Inherits esFlowmeter

        <DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _
        Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
            Get
                Return MyBase.Debug
            End Get
        End Property

        Public Overrides Function CreateInstance() As esEntity
            Return New Flowmeter()
        End Function

#Region "Static Quick Access Methods"
        Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal flowmeterMapID As System.Int32)
            Dim obj As New Flowmeter()
            obj.ProjectID = projectID
            obj.EngineModel = engineModel
            obj.FlowmeterMapID = flowmeterMapID
            obj.AcceptChanges()
            obj.MarkAsDeleted()
            obj.Save()
        End Sub

        Public Shared Sub Delete(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal flowmeterMapID As System.Int32, ByVal sqlAccessType As esSqlAccessType)
            Dim obj As New Flowmeter()
            obj.ProjectID = projectID
            obj.EngineModel = engineModel
            obj.FlowmeterMapID = flowmeterMapID
            obj.AcceptChanges()
            obj.MarkAsDeleted()
            obj.Save(sqlAccessType)
        End Sub
#End Region



    End Class



    <DebuggerDisplay("Count = {Count}")> _
    <Serializable> _
    <CollectionDataContract> _
    <XmlType("FlowmeterCollection")> _
    Partial Public Class FlowmeterCollection
        Inherits esFlowmeterCollection
        Implements IEnumerable(Of Flowmeter)

        Public Function FindByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal flowmeterMapID As System.Int32) As Flowmeter
            Return MyBase.SingleOrDefault(Function(e) e.ProjectID.HasValue AndAlso e.ProjectID.Value = projectID And e.EngineModel = engineModel And e.FlowmeterMapID.HasValue AndAlso e.FlowmeterMapID.Value = flowmeterMapID)
        End Function



#Region "WCF Service Class"

        <DataContract> _
        <KnownType(GetType(Flowmeter))> _
        Public Class FlowmeterCollectionWCFPacket
            Inherits esCollectionWCFPacket(Of FlowmeterCollection)

            Public Shared Widening Operator CType(packet As FlowmeterCollectionWCFPacket) As FlowmeterCollection
                Return packet.Collection
            End Operator

            Public Shared Widening Operator CType(collection As FlowmeterCollection) As FlowmeterCollectionWCFPacket
                Return New FlowmeterCollectionWCFPacket() With {.Collection = collection}
            End Operator

        End Class

#End Region



    End Class




    <DebuggerDisplay("Query = {Parse()}")> _
    <Serializable> _
    Partial Public Class FlowmeterQuery
        Inherits esFlowmeterQuery

        Public Sub New(ByVal joinAlias As String)
            Me.es.JoinAlias = joinAlias
        End Sub

        Protected Overrides Function GetQueryName() As String
            Return "FlowmeterQuery"
        End Function

#Region "Explicit Casts"

        Public Shared Narrowing Operator CType(ByVal query As FlowmeterQuery) As String
            Return FlowmeterQuery.SerializeHelper.ToXml(query)
        End Operator

        Public Shared Narrowing Operator CType(ByVal query As String) As FlowmeterQuery
            Return DirectCast(FlowmeterQuery.SerializeHelper.FromXml(query, GetType(FlowmeterQuery)), FlowmeterQuery)
        End Operator

#End Region

    End Class


    <DataContract> _
    <Serializable()> _
    Partial Public MustInherit Class esFlowmeter
        Inherits esEntity
        Implements INotifyPropertyChanged

        Public Sub New()

        End Sub

#Region "LoadByPrimaryKey"
        Public Overridable Function LoadByPrimaryKey(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal flowmeterMapID As System.Int32) As Boolean

            If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL Then
                Return LoadByPrimaryKeyDynamic(projectID, engineModel, flowmeterMapID)
            Else
                Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, flowmeterMapID)
            End If

        End Function

        Public Overridable Function LoadByPrimaryKey(ByVal sqlAccessType As esSqlAccessType, ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal flowmeterMapID As System.Int32) As Boolean

            If sqlAccessType = esSqlAccessType.DynamicSQL Then
                Return LoadByPrimaryKeyDynamic(projectID, engineModel, flowmeterMapID)
            Else
                Return LoadByPrimaryKeyStoredProcedure(projectID, engineModel, flowmeterMapID)
            End If

        End Function

        Private Function LoadByPrimaryKeyDynamic(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal flowmeterMapID As System.Int32) As Boolean

            Dim query As New FlowmeterQuery()
            query.Where(query.ProjectID = projectID And query.EngineModel = engineModel And query.FlowmeterMapID = flowmeterMapID)
            Return Me.Load(query)

        End Function

        Private Function LoadByPrimaryKeyStoredProcedure(ByVal projectID As System.Int32, ByVal engineModel As System.String, ByVal flowmeterMapID As System.Int32) As Boolean

            Dim parms As esParameters = New esParameters()
            parms.Add("ProjectID", projectID)
            parms.Add("EngineModel", engineModel)
            parms.Add("FlowmeterMapID", flowmeterMapID)

            Return MyBase.Load(esQueryType.StoredProcedure, Me.es.spLoadByPrimaryKey, parms)

        End Function
#End Region

#Region "Properties"



        ' <summary>
        ' Maps to Flowmeter.ProjectID
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property ProjectID As Nullable(Of System.Int32)
            Get
                Return MyBase.GetSystemInt32(FlowmeterMetadata.ColumnNames.ProjectID)
            End Get

            Set(ByVal value As Nullable(Of System.Int32))
                If MyBase.SetSystemInt32(FlowmeterMetadata.ColumnNames.ProjectID, value) Then
                    Me._UpToUutByProjectID = Nothing
                    Me.OnPropertyChanged("UpToUutByProjectID")
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.ProjectID)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.EngineModel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property EngineModel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.EngineModel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.EngineModel, value) Then
                    Me._UpToUutByProjectID = Nothing
                    Me.OnPropertyChanged("UpToUutByProjectID")
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.EngineModel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.FlowmeterMapID
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property FlowmeterMapID As Nullable(Of System.Int32)
            Get
                Return MyBase.GetSystemInt32(FlowmeterMetadata.ColumnNames.FlowmeterMapID)
            End Get

            Set(ByVal value As Nullable(Of System.Int32))
                If MyBase.SetSystemInt32(FlowmeterMetadata.ColumnNames.FlowmeterMapID, value) Then
                    Me._UpToFlowmeterMapByFlowmeterMapID = Nothing
                    Me.OnPropertyChanged("UpToFlowmeterMapByFlowmeterMapID")
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.FlowmeterMapID)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.RawChannel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property RawChannel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.RawChannel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.RawChannel, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.RawChannel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.CalChannel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property CalChannel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.CalChannel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.CalChannel, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.CalChannel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.TotChannel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property TotChannel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.TotChannel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.TotChannel, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.TotChannel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.CustomOutputUnit
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property CustomOutputUnit As Nullable(Of System.Int32)
            Get
                Return MyBase.GetSystemInt32(FlowmeterMetadata.ColumnNames.CustomOutputUnit)
            End Get

            Set(ByVal value As Nullable(Of System.Int32))
                If MyBase.SetSystemInt32(FlowmeterMetadata.ColumnNames.CustomOutputUnit, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.CustomOutputUnit)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.FuelSlopeChannel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property FuelSlopeChannel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.FuelSlopeChannel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.FuelSlopeChannel, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.FuelSlopeChannel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.FuelTemperatureChannel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property FuelTemperatureChannel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.FuelTemperatureChannel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.FuelTemperatureChannel, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.FuelTemperatureChannel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.FuelSampleTemperatureChannel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property FuelSampleTemperatureChannel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.FuelSampleTemperatureChannel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.FuelSampleTemperatureChannel, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.FuelSampleTemperatureChannel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.FuelSampleSGChannel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property FuelSampleSGChannel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.FuelSampleSGChannel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.FuelSampleSGChannel, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.FuelSampleSGChannel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.WaterDensityAt60FChannel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property WaterDensityAt60FChannel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.WaterDensityAt60FChannel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.WaterDensityAt60FChannel, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.WaterDensityAt60FChannel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.PPHChannel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property PPHChannel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.PPHChannel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.PPHChannel, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.PPHChannel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to Flowmeter.TotalWeightChannel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property TotalWeightChannel As System.String
            Get
                Return MyBase.GetSystemString(FlowmeterMetadata.ColumnNames.TotalWeightChannel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(FlowmeterMetadata.ColumnNames.TotalWeightChannel, value) Then
                    OnPropertyChanged(FlowmeterMetadata.PropertyNames.TotalWeightChannel)
                End If
            End Set
        End Property

        <CLSCompliant(False)> _
        Protected Friend _UpToFlowmeterMapByFlowmeterMapID As FlowmeterMap

        <CLSCompliant(False)> _
        Protected Friend _UpToUutByProjectID As Uut

#End Region

#Region ".str() Properties"

        Public Overrides Sub SetProperties(values As IDictionary)

            Dim propertyName As String
            For Each propertyName In values.Keys
                Me.SetProperty(propertyName, values(propertyName))
            Next

        End Sub

        Public Overrides Sub SetProperty(name As String, value As Object)

            Dim col As esColumnMetadata = Me.Meta.Columns.FindByPropertyName(name)
            If Not col Is Nothing Then

                If value Is Nothing OrElse value.GetType().ToString() = "System.String" Then

                    ' Use the strongly typed property
                    Select Case name

                        Case "ProjectID"
                            Me.str().ProjectID = CType(value, String)

                        Case "EngineModel"
                            Me.str().EngineModel = CType(value, String)

                        Case "FlowmeterMapID"
                            Me.str().FlowmeterMapID = CType(value, String)

                        Case "RawChannel"
                            Me.str().RawChannel = CType(value, String)

                        Case "CalChannel"
                            Me.str().CalChannel = CType(value, String)

                        Case "TotChannel"
                            Me.str().TotChannel = CType(value, String)

                        Case "CustomOutputUnit"
                            Me.str().CustomOutputUnit = CType(value, String)

                        Case "FuelSlopeChannel"
                            Me.str().FuelSlopeChannel = CType(value, String)

                        Case "FuelTemperatureChannel"
                            Me.str().FuelTemperatureChannel = CType(value, String)

                        Case "FuelSampleTemperatureChannel"
                            Me.str().FuelSampleTemperatureChannel = CType(value, String)

                        Case "FuelSampleSGChannel"
                            Me.str().FuelSampleSGChannel = CType(value, String)

                        Case "WaterDensityAt60FChannel"
                            Me.str().WaterDensityAt60FChannel = CType(value, String)

                        Case "PPHChannel"
                            Me.str().PPHChannel = CType(value, String)

                        Case "TotalWeightChannel"
                            Me.str().TotalWeightChannel = CType(value, String)

                    End Select

                Else

                    Select Case name

                        Case "ProjectID"

                            If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
                                Me.ProjectID = CType(value, Nullable(Of System.Int32))
                                OnPropertyChanged(FlowmeterMetadata.PropertyNames.ProjectID)
                            End If

                        Case "FlowmeterMapID"

                            If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
                                Me.FlowmeterMapID = CType(value, Nullable(Of System.Int32))
                                OnPropertyChanged(FlowmeterMetadata.PropertyNames.FlowmeterMapID)
                            End If

                        Case "CustomOutputUnit"

                            If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
                                Me.CustomOutputUnit = CType(value, Nullable(Of System.Int32))
                                OnPropertyChanged(FlowmeterMetadata.PropertyNames.CustomOutputUnit)
                            End If


                        Case Else

                    End Select
                End If

            ElseIf Me.ContainsColumn(name) Then
                Me.SetColumn(name, value)
            Else
                Throw New Exception("SetProperty Error: '" + name + "' not found")
            End If

        End Sub

        Public Function str() As esStrings

            If _esstrings Is Nothing Then
                _esstrings = New esStrings(Me)
            End If
            Return _esstrings

        End Function

        Public NotInheritable Class esStrings

            Public Sub New(ByVal entity As esFlowmeter)
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

                Set(ByVal Value As System.String)
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

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.EngineModel = Nothing
                    Else
                        entity.EngineModel = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property FlowmeterMapID As System.String
                Get
                    Dim data_ As Nullable(Of System.Int32) = entity.FlowmeterMapID

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.FlowmeterMapID = Nothing
                    Else
                        entity.FlowmeterMapID = Convert.ToInt32(Value)
                    End If
                End Set
            End Property

            Public Property RawChannel As System.String
                Get
                    Dim data_ As System.String = entity.RawChannel

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.RawChannel = Nothing
                    Else
                        entity.RawChannel = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property CalChannel As System.String
                Get
                    Dim data_ As System.String = entity.CalChannel

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.CalChannel = Nothing
                    Else
                        entity.CalChannel = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property TotChannel As System.String
                Get
                    Dim data_ As System.String = entity.TotChannel

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.TotChannel = Nothing
                    Else
                        entity.TotChannel = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property CustomOutputUnit As System.String
                Get
                    Dim data_ As Nullable(Of System.Int32) = entity.CustomOutputUnit

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.CustomOutputUnit = Nothing
                    Else
                        entity.CustomOutputUnit = Convert.ToInt32(Value)
                    End If
                End Set
            End Property

            Public Property FuelSlopeChannel As System.String
                Get
                    Dim data_ As System.String = entity.FuelSlopeChannel

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.FuelSlopeChannel = Nothing
                    Else
                        entity.FuelSlopeChannel = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property FuelTemperatureChannel As System.String
                Get
                    Dim data_ As System.String = entity.FuelTemperatureChannel

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.FuelTemperatureChannel = Nothing
                    Else
                        entity.FuelTemperatureChannel = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property FuelSampleTemperatureChannel As System.String
                Get
                    Dim data_ As System.String = entity.FuelSampleTemperatureChannel

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.FuelSampleTemperatureChannel = Nothing
                    Else
                        entity.FuelSampleTemperatureChannel = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property FuelSampleSGChannel As System.String
                Get
                    Dim data_ As System.String = entity.FuelSampleSGChannel

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.FuelSampleSGChannel = Nothing
                    Else
                        entity.FuelSampleSGChannel = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property WaterDensityAt60FChannel As System.String
                Get
                    Dim data_ As System.String = entity.WaterDensityAt60FChannel

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.WaterDensityAt60FChannel = Nothing
                    Else
                        entity.WaterDensityAt60FChannel = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property PPHChannel As System.String
                Get
                    Dim data_ As System.String = entity.PPHChannel

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.PPHChannel = Nothing
                    Else
                        entity.PPHChannel = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property TotalWeightChannel As System.String
                Get
                    Dim data_ As System.String = entity.TotalWeightChannel

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.TotalWeightChannel = Nothing
                    Else
                        entity.TotalWeightChannel = Convert.ToString(Value)
                    End If
                End Set
            End Property


            Private entity As esFlowmeter
        End Class


        <NonSerialized> _
        <IgnoreDataMember> _
              Private _esstrings As esStrings

#End Region

#Region "Housekeeping methods"

        Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return FlowmeterMetadata.Meta()
            End Get
        End Property

#End Region

#Region "Query Logic"

        Public ReadOnly Property Query() As FlowmeterQuery
            Get
                If Me.m_query Is Nothing Then
                    Me.m_query = New FlowmeterQuery()
                    InitQuery(Me.m_query)
                End If

                Return Me.m_query
            End Get
        End Property

        Public Overloads Function Load(ByVal query As FlowmeterQuery) As Boolean
            Me.m_query = query
            InitQuery(Me.m_query)
            Return Me.Query.Load()
        End Function

        Protected Sub InitQuery(ByVal query As FlowmeterQuery)
            query.OnLoadDelegate = AddressOf OnQueryLoaded

            If Not query.es2.HasConnection Then
                query.es2.Connection = DirectCast(Me, IEntity).Connection
            End If
        End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As FlowmeterQuery

    End Class



    <Serializable()> _
    Partial Public MustInherit Class esFlowmeterCollection
        Inherits esEntityCollection(Of Flowmeter)

#Region "Housekeeping methods"
        Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return FlowmeterMetadata.Meta()
            End Get
        End Property

        Protected Overloads Overrides Function GetCollectionName() As String
            Return "FlowmeterCollection"
        End Function

#End Region

#Region "Query Logic"


        <BrowsableAttribute(False)> _
        Public ReadOnly Property Query() As FlowmeterQuery
            Get
                If Me.m_query Is Nothing Then
                    Me.m_query = New FlowmeterQuery()
                    InitQuery(Me.m_query)
                End If

                Return Me.m_query
            End Get
        End Property

        Public Overloads Function Load(ByVal query As FlowmeterQuery) As Boolean
            Me.m_query = query
            InitQuery(Me.m_query)
            Return Query.Load()
        End Function

        Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
            If Me.m_query Is Nothing Then
                Me.m_query = New FlowmeterQuery()
                Me.InitQuery(m_query)
            End If
            Return Me.m_query
        End Function

        Protected Sub InitQuery(ByVal query As FlowmeterQuery)
            query.OnLoadDelegate = AddressOf OnQueryLoaded

            If Not query.es2.HasConnection Then
                query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
            End If
        End Sub

        Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
            Me.InitQuery(DirectCast(query, FlowmeterQuery))
        End Sub

#End Region

        Private m_query As FlowmeterQuery
    End Class



    <Serializable> _
    Partial Public MustInherit Class esFlowmeterQuery
        Inherits esDynamicQuery

        Protected Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return FlowmeterMetadata.Meta()
            End Get
        End Property

#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
                Case "ProjectID"
                    Return Me.ProjectID
                Case "EngineModel"
                    Return Me.EngineModel
                Case "FlowmeterMapID"
                    Return Me.FlowmeterMapID
                Case "RawChannel"
                    Return Me.RawChannel
                Case "CalChannel"
                    Return Me.CalChannel
                Case "TotChannel"
                    Return Me.TotChannel
                Case "CustomOutputUnit"
                    Return Me.CustomOutputUnit
                Case "FuelSlopeChannel"
                    Return Me.FuelSlopeChannel
                Case "FuelTemperatureChannel"
                    Return Me.FuelTemperatureChannel
                Case "FuelSampleTemperatureChannel"
                    Return Me.FuelSampleTemperatureChannel
                Case "FuelSampleSGChannel"
                    Return Me.FuelSampleSGChannel
                Case "WaterDensityAt60FChannel"
                    Return Me.WaterDensityAt60FChannel
                Case "PPHChannel"
                    Return Me.PPHChannel
                Case "TotalWeightChannel"
                    Return Me.TotalWeightChannel
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region

#Region "esQueryItems"


        Public ReadOnly Property ProjectID As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.ProjectID, esSystemType.Int32)
            End Get
        End Property

        Public ReadOnly Property EngineModel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.EngineModel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property FlowmeterMapID As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.FlowmeterMapID, esSystemType.Int32)
            End Get
        End Property

        Public ReadOnly Property RawChannel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.RawChannel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property CalChannel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.CalChannel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property TotChannel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.TotChannel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property CustomOutputUnit As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.CustomOutputUnit, esSystemType.Int32)
            End Get
        End Property

        Public ReadOnly Property FuelSlopeChannel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.FuelSlopeChannel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property FuelTemperatureChannel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.FuelTemperatureChannel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property FuelSampleTemperatureChannel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.FuelSampleTemperatureChannel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property FuelSampleSGChannel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.FuelSampleSGChannel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property WaterDensityAt60FChannel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.WaterDensityAt60FChannel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property PPHChannel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.PPHChannel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property TotalWeightChannel As esQueryItem
            Get
                Return New esQueryItem(Me, FlowmeterMetadata.ColumnNames.TotalWeightChannel, esSystemType.String)
            End Get
        End Property

#End Region

    End Class



    Partial Public Class Flowmeter
        Inherits esFlowmeter


#Region "UpToFlowmeterMapByFlowmeterMapID - Many To One"
        ''' <summary>
        ''' Many to One
        ''' Foreign Key Name - Flowmeter_MapFlowmeter
        ''' </summary>

        <XmlIgnore()> _
        Public Property UpToFlowmeterMapByFlowmeterMapID As FlowmeterMap

            Get
                If Me.es.IsLazyLoadDisabled Then Return Nothing

                If Me._UpToFlowmeterMapByFlowmeterMapID Is Nothing _
                         AndAlso Not FlowmeterMapID.Equals(Nothing) Then

                    Me._UpToFlowmeterMapByFlowmeterMapID = New FlowmeterMap()
                    Me._UpToFlowmeterMapByFlowmeterMapID.es.Connection.Name = Me.es.Connection.Name
                    Me.SetPreSave("UpToFlowmeterMapByFlowmeterMapID", Me._UpToFlowmeterMapByFlowmeterMapID)
                    Me._UpToFlowmeterMapByFlowmeterMapID.Query.Where(Me._UpToFlowmeterMapByFlowmeterMapID.Query.Id = Me.FlowmeterMapID)
                    Me._UpToFlowmeterMapByFlowmeterMapID.Query.Load()
                End If

                Return Me._UpToFlowmeterMapByFlowmeterMapID
            End Get

            Set(ByVal value As FlowmeterMap)
                Me.RemovePreSave("UpToFlowmeterMapByFlowmeterMapID")


                If value Is Nothing Then

                    Me.FlowmeterMapID = Nothing

                    Me._UpToFlowmeterMapByFlowmeterMapID = Nothing
                Else

                    Me.FlowmeterMapID = value.Id

                    Me._UpToFlowmeterMapByFlowmeterMapID = value
                    Me.SetPreSave("UpToFlowmeterMapByFlowmeterMapID", Me._UpToFlowmeterMapByFlowmeterMapID)
                End If

            End Set

        End Property
#End Region

#Region "UpToUutByProjectID - Many To One"
        ''' <summary>
        ''' Many to One
        ''' Foreign Key Name - UUTFlowmeter
        ''' </summary>

        <XmlIgnore()> _
        Public Property UpToUutByProjectID As Uut

            Get
                If Me.es.IsLazyLoadDisabled Then Return Nothing

                If Me._UpToUutByProjectID Is Nothing _
                         AndAlso Not ProjectID.Equals(Nothing) AndAlso Not EngineModel.Equals(Nothing) Then

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
    Partial Public Class FlowmeterMetadata
        Inherits esMetadata
        Implements IMetadata

#Region "Protected Constructor"
        Protected Sub New()
            m_columns = New esColumnMetadataCollection()
            Dim c As esColumnMetadata

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.ProjectID, 0, GetType(System.Int32), esSystemType.Int32)
            c.PropertyName = FlowmeterMetadata.PropertyNames.ProjectID
            c.IsInPrimaryKey = True
            c.NumericPrecision = 10
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.EngineModel
            c.IsInPrimaryKey = True
            c.CharacterMaxLength = 32
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.FlowmeterMapID, 2, GetType(System.Int32), esSystemType.Int32)
            c.PropertyName = FlowmeterMetadata.PropertyNames.FlowmeterMapID
            c.IsInPrimaryKey = True
            c.NumericPrecision = 10
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.RawChannel, 3, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.RawChannel
            c.CharacterMaxLength = 50
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.CalChannel, 4, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.CalChannel
            c.CharacterMaxLength = 50
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.TotChannel, 5, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.TotChannel
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.CustomOutputUnit, 6, GetType(System.Int32), esSystemType.Int32)
            c.PropertyName = FlowmeterMetadata.PropertyNames.CustomOutputUnit
            c.NumericPrecision = 10
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.FuelSlopeChannel, 7, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.FuelSlopeChannel
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.FuelTemperatureChannel, 8, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.FuelTemperatureChannel
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.FuelSampleTemperatureChannel, 9, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.FuelSampleTemperatureChannel
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.FuelSampleSGChannel, 10, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.FuelSampleSGChannel
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.WaterDensityAt60FChannel, 11, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.WaterDensityAt60FChannel
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.PPHChannel, 12, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.PPHChannel
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(FlowmeterMetadata.ColumnNames.TotalWeightChannel, 13, GetType(System.String), esSystemType.String)
            c.PropertyName = FlowmeterMetadata.PropertyNames.TotalWeightChannel
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

        End Sub
#End Region

        Public Shared Function Meta() As FlowmeterMetadata
            Return _meta
        End Function

        Public ReadOnly Property DataID() As System.Guid Implements IMetadata.DataID
            Get
                Return MyBase.m_dataID
            End Get
        End Property

        Public ReadOnly Property MultiProviderMode() As Boolean Implements IMetadata.MultiProviderMode
            Get
                Return False
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
            Public Const FlowmeterMapID As String = "FlowmeterMapID"
            Public Const RawChannel As String = "RawChannel"
            Public Const CalChannel As String = "CalChannel"
            Public Const TotChannel As String = "TotChannel"
            Public Const CustomOutputUnit As String = "CustomOutputUnit"
            Public Const FuelSlopeChannel As String = "FuelSlopeChannel"
            Public Const FuelTemperatureChannel As String = "FuelTemperatureChannel"
            Public Const FuelSampleTemperatureChannel As String = "FuelSampleTemperatureChannel"
            Public Const FuelSampleSGChannel As String = "FuelSampleSGChannel"
            Public Const WaterDensityAt60FChannel As String = "WaterDensityAt60FChannel"
            Public Const PPHChannel As String = "PPHChannel"
            Public Const TotalWeightChannel As String = "TotalWeightChannel"
        End Class
#End Region

#Region "PropertyNames"
        Public Class PropertyNames
            Public Const ProjectID As String = "ProjectID"
            Public Const EngineModel As String = "EngineModel"
            Public Const FlowmeterMapID As String = "FlowmeterMapID"
            Public Const RawChannel As String = "RawChannel"
            Public Const CalChannel As String = "CalChannel"
            Public Const TotChannel As String = "TotChannel"
            Public Const CustomOutputUnit As String = "CustomOutputUnit"
            Public Const FuelSlopeChannel As String = "FuelSlopeChannel"
            Public Const FuelTemperatureChannel As String = "FuelTemperatureChannel"
            Public Const FuelSampleTemperatureChannel As String = "FuelSampleTemperatureChannel"
            Public Const FuelSampleSGChannel As String = "FuelSampleSGChannel"
            Public Const WaterDensityAt60FChannel As String = "WaterDensityAt60FChannel"
            Public Const PPHChannel As String = "PPHChannel"
            Public Const TotalWeightChannel As String = "TotalWeightChannel"
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
            SyncLock GetType(FlowmeterMetadata)

                If FlowmeterMetadata.mapDelegates Is Nothing Then
                    FlowmeterMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
                End If

                If FlowmeterMetadata._meta Is Nothing Then
                    FlowmeterMetadata._meta = New FlowmeterMetadata()
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



                meta.AddTypeMap("ProjectID", New esTypeMap("Long", "System.Int32"))
                meta.AddTypeMap("EngineModel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("FlowmeterMapID", New esTypeMap("Long", "System.Int32"))
                meta.AddTypeMap("RawChannel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("CalChannel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("TotChannel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("CustomOutputUnit", New esTypeMap("Long", "System.Int32"))
                meta.AddTypeMap("FuelSlopeChannel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("FuelTemperatureChannel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("FuelSampleTemperatureChannel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("FuelSampleSGChannel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("WaterDensityAt60FChannel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("PPHChannel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("TotalWeightChannel", New esTypeMap("Text", "System.String"))



                meta.Source = "Flowmeter"
                meta.Destination = "Flowmeter"

                meta.spInsert = "proc_FlowmeterInsert"
                meta.spUpdate = "proc_FlowmeterUpdate"
                meta.spDelete = "proc_FlowmeterDelete"
                meta.spLoadAll = "proc_FlowmeterLoadAll"
                meta.spLoadByPrimaryKey = "proc_FlowmeterLoadByPrimaryKey"

                Me.m_providerMetadataMaps.Add("esDefault", meta)

            End If

            Return Me.m_providerMetadataMaps("esDefault")

        End Function

#End Region

        Private Shared _meta As FlowmeterMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
        Private Shared _esDefault As Integer = RegisterDelegateesDefault()

    End Class

End Namespace


'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 16/04/2015 11:37:17 AM
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
    ' Encapsulates the 'OmniBusBoxUartTemplate' table
    ' </summary>

    <System.Diagnostics.DebuggerDisplay("Data = {Debug}")> _
    <Serializable> _
    <DataContract> _
    <KnownType(GetType(OmniBusBoxUartTemplate))> _
    <XmlType("OmniBusBoxUartTemplate")> _
    Partial Public Class OmniBusBoxUartTemplate
        Inherits esOmniBusBoxUartTemplate

        <DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)> _
        Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
            Get
                Return MyBase.Debug
            End Get
        End Property

        Public Overrides Function CreateInstance() As esEntity
            Return New OmniBusBoxUartTemplate()
        End Function

#Region "Static Quick Access Methods"

#End Region



    End Class



    <DebuggerDisplay("Count = {Count}")> _
    <Serializable> _
    <CollectionDataContract> _
    <XmlType("OmniBusBoxUartTemplateCollection")> _
    Partial Public Class OmniBusBoxUartTemplateCollection
        Inherits esOmniBusBoxUartTemplateCollection
        Implements IEnumerable(Of OmniBusBoxUartTemplate)




#Region "WCF Service Class"

        <DataContract> _
        <KnownType(GetType(OmniBusBoxUartTemplate))> _
        Public Class OmniBusBoxUartTemplateCollectionWCFPacket
            Inherits esCollectionWCFPacket(Of OmniBusBoxUartTemplateCollection)

            Public Shared Widening Operator CType(packet As OmniBusBoxUartTemplateCollectionWCFPacket) As OmniBusBoxUartTemplateCollection
                Return packet.Collection
            End Operator

            Public Shared Widening Operator CType(collection As OmniBusBoxUartTemplateCollection) As OmniBusBoxUartTemplateCollectionWCFPacket
                Return New OmniBusBoxUartTemplateCollectionWCFPacket() With {.Collection = collection}
            End Operator

        End Class

#End Region



    End Class




    <DebuggerDisplay("Query = {Parse()}")> _
    <Serializable> _
    Partial Public Class OmniBusBoxUartTemplateQuery
        Inherits esOmniBusBoxUartTemplateQuery

        Public Sub New(ByVal joinAlias As String)
            Me.es.JoinAlias = joinAlias
        End Sub

        Protected Overrides Function GetQueryName() As String
            Return "OmniBusBoxUartTemplateQuery"
        End Function

#Region "Explicit Casts"

        Public Shared Narrowing Operator CType(ByVal query As OmniBusBoxUartTemplateQuery) As String
            Return OmniBusBoxUartTemplateQuery.SerializeHelper.ToXml(query)
        End Operator

        Public Shared Narrowing Operator CType(ByVal query As String) As OmniBusBoxUartTemplateQuery
            Return DirectCast(OmniBusBoxUartTemplateQuery.SerializeHelper.FromXml(query, GetType(OmniBusBoxUartTemplateQuery)), OmniBusBoxUartTemplateQuery)
        End Operator

#End Region

    End Class


    <DataContract> _
    <Serializable()> _
    Partial Public MustInherit Class esOmniBusBoxUartTemplate
        Inherits esEntity
        Implements INotifyPropertyChanged

        Public Sub New()

        End Sub

#Region "LoadByPrimaryKey"
        Public Overridable Function LoadByPrimaryKey() As Boolean

            If Me.es.Connection.SqlAccessType = esSqlAccessType.DynamicSQL Then
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

            Dim query As New OmniBusBoxUartTemplateQuery()
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
        ' Maps to OmniBusBoxUartTemplate.ID
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property Id As Nullable(Of System.Int32)
            Get
                Return MyBase.GetSystemInt32(OmniBusBoxUartTemplateMetadata.ColumnNames.Id)
            End Get

            Set(ByVal value As Nullable(Of System.Int32))
                If MyBase.SetSystemInt32(OmniBusBoxUartTemplateMetadata.ColumnNames.Id, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Id)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.EngineModel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property EngineModel As System.String
            Get
                Return MyBase.GetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.EngineModel)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.EngineModel, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.EngineModel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.PWC_PN
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property PwcPn As System.String
            Get
                Return MyBase.GetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.PwcPn)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.PwcPn, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.PwcPn)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Name
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property Name As System.String
            Get
                Return MyBase.GetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.Name)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.Name, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Name)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.ExportToCsv
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property ExportToCsv As Nullable(Of System.Boolean)
            Get
                Return MyBase.GetSystemBoolean(OmniBusBoxUartTemplateMetadata.ColumnNames.ExportToCsv)
            End Get

            Set(ByVal value As Nullable(Of System.Boolean))
                If MyBase.SetSystemBoolean(OmniBusBoxUartTemplateMetadata.ColumnNames.ExportToCsv, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.ExportToCsv)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Source
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property Source As System.String
            Get
                Return MyBase.GetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.Source)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.Source, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Source)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Arinc_Channel
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property ArincChannel As Nullable(Of System.Byte)
            Get
                Return MyBase.GetSystemByte(OmniBusBoxUartTemplateMetadata.ColumnNames.ArincChannel)
            End Get

            Set(ByVal value As Nullable(Of System.Byte))
                If MyBase.SetSystemByte(OmniBusBoxUartTemplateMetadata.ColumnNames.ArincChannel, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.ArincChannel)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Parameter_No
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property ParameterNo As Nullable(Of System.Int16)
            Get
                Return MyBase.GetSystemInt16(OmniBusBoxUartTemplateMetadata.ColumnNames.ParameterNo)
            End Get

            Set(ByVal value As Nullable(Of System.Int16))
                If MyBase.SetSystemInt16(OmniBusBoxUartTemplateMetadata.ColumnNames.ParameterNo, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.ParameterNo)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Parameter
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property Parameter As System.String
            Get
                Return MyBase.GetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.Parameter)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.Parameter, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Parameter)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Address
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property Address As Nullable(Of System.Int32)
            Get
                Return MyBase.GetSystemInt32(OmniBusBoxUartTemplateMetadata.ColumnNames.Address)
            End Get

            Set(ByVal value As Nullable(Of System.Int32))
                If MyBase.SetSystemInt32(OmniBusBoxUartTemplateMetadata.ColumnNames.Address, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Address)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Data_Type
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property DataType As Nullable(Of System.Byte)
            Get
                Return MyBase.GetSystemByte(OmniBusBoxUartTemplateMetadata.ColumnNames.DataType)
            End Get

            Set(ByVal value As Nullable(Of System.Byte))
                If MyBase.SetSystemByte(OmniBusBoxUartTemplateMetadata.ColumnNames.DataType, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.DataType)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Units
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property Units As System.String
            Get
                Return MyBase.GetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.Units)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.Units, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Units)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Format
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property Format As System.String
            Get
                Return MyBase.GetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.Format)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.Format, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Format)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Card_Number
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property CardNumber As Nullable(Of System.Byte)
            Get
                Return MyBase.GetSystemByte(OmniBusBoxUartTemplateMetadata.ColumnNames.CardNumber)
            End Get

            Set(ByVal value As Nullable(Of System.Byte))
                If MyBase.SetSystemByte(OmniBusBoxUartTemplateMetadata.ColumnNames.CardNumber, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.CardNumber)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Core_Number
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property CoreNumber As Nullable(Of System.Byte)
            Get
                Return MyBase.GetSystemByte(OmniBusBoxUartTemplateMetadata.ColumnNames.CoreNumber)
            End Get

            Set(ByVal value As Nullable(Of System.Byte))
                If MyBase.SetSystemByte(OmniBusBoxUartTemplateMetadata.ColumnNames.CoreNumber, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.CoreNumber)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Channel_Number
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property ChannelNumber As Nullable(Of System.Byte)
            Get
                Return MyBase.GetSystemByte(OmniBusBoxUartTemplateMetadata.ColumnNames.ChannelNumber)
            End Get

            Set(ByVal value As Nullable(Of System.Byte))
                If MyBase.SetSystemByte(OmniBusBoxUartTemplateMetadata.ColumnNames.ChannelNumber, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.ChannelNumber)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Slope
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property Slope As Nullable(Of System.Single)
            Get
                Return MyBase.GetSystemSingle(OmniBusBoxUartTemplateMetadata.ColumnNames.Slope)
            End Get

            Set(ByVal value As Nullable(Of System.Single))
                If MyBase.SetSystemSingle(OmniBusBoxUartTemplateMetadata.ColumnNames.Slope, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Slope)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Offset
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property Offset As Nullable(Of System.Single)
            Get
                Return MyBase.GetSystemSingle(OmniBusBoxUartTemplateMetadata.ColumnNames.Offset)
            End Get

            Set(ByVal value As Nullable(Of System.Single))
                If MyBase.SetSystemSingle(OmniBusBoxUartTemplateMetadata.ColumnNames.Offset, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Offset)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Logger_Enabled
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property LoggerEnabled As Nullable(Of System.Boolean)
            Get
                Return MyBase.GetSystemBoolean(OmniBusBoxUartTemplateMetadata.ColumnNames.LoggerEnabled)
            End Get

            Set(ByVal value As Nullable(Of System.Boolean))
                If MyBase.SetSystemBoolean(OmniBusBoxUartTemplateMetadata.ColumnNames.LoggerEnabled, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.LoggerEnabled)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Logger_Index
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property LoggerIndex As Nullable(Of System.Int32)
            Get
                Return MyBase.GetSystemInt32(OmniBusBoxUartTemplateMetadata.ColumnNames.LoggerIndex)
            End Get

            Set(ByVal value As Nullable(Of System.Int32))
                If MyBase.SetSystemInt32(OmniBusBoxUartTemplateMetadata.ColumnNames.LoggerIndex, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.LoggerIndex)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Log_Report_Categorie
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property LogReportCategorie As System.String
            Get
                Return MyBase.GetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.LogReportCategorie)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(OmniBusBoxUartTemplateMetadata.ColumnNames.LogReportCategorie, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.LogReportCategorie)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to OmniBusBoxUartTemplate.Log_Report_Order_In_Categorie
        ' </summary>
        <DataMember(EmitDefaultValue:=False)> _
        Public Overridable Property LogReportOrderInCategorie As Nullable(Of System.Int32)
            Get
                Return MyBase.GetSystemInt32(OmniBusBoxUartTemplateMetadata.ColumnNames.LogReportOrderInCategorie)
            End Get

            Set(ByVal value As Nullable(Of System.Int32))
                If MyBase.SetSystemInt32(OmniBusBoxUartTemplateMetadata.ColumnNames.LogReportOrderInCategorie, value) Then
                    OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.LogReportOrderInCategorie)
                End If
            End Set
        End Property

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

                        Case "Id"
                            Me.str().Id = CType(value, String)

                        Case "EngineModel"
                            Me.str().EngineModel = CType(value, String)

                        Case "PwcPn"
                            Me.str().PwcPn = CType(value, String)

                        Case "Name"
                            Me.str().Name = CType(value, String)

                        Case "ExportToCsv"
                            Me.str().ExportToCsv = CType(value, String)

                        Case "Source"
                            Me.str().Source = CType(value, String)

                        Case "ArincChannel"
                            Me.str().ArincChannel = CType(value, String)

                        Case "ParameterNo"
                            Me.str().ParameterNo = CType(value, String)

                        Case "Parameter"
                            Me.str().Parameter = CType(value, String)

                        Case "Address"
                            Me.str().Address = CType(value, String)

                        Case "DataType"
                            Me.str().DataType = CType(value, String)

                        Case "Units"
                            Me.str().Units = CType(value, String)

                        Case "Format"
                            Me.str().Format = CType(value, String)

                        Case "CardNumber"
                            Me.str().CardNumber = CType(value, String)

                        Case "CoreNumber"
                            Me.str().CoreNumber = CType(value, String)

                        Case "ChannelNumber"
                            Me.str().ChannelNumber = CType(value, String)

                        Case "Slope"
                            Me.str().Slope = CType(value, String)

                        Case "Offset"
                            Me.str().Offset = CType(value, String)

                        Case "LoggerEnabled"
                            Me.str().LoggerEnabled = CType(value, String)

                        Case "LoggerIndex"
                            Me.str().LoggerIndex = CType(value, String)

                        Case "LogReportCategorie"
                            Me.str().LogReportCategorie = CType(value, String)

                        Case "LogReportOrderInCategorie"
                            Me.str().LogReportOrderInCategorie = CType(value, String)

                    End Select

                Else

                    Select Case name

                        Case "Id"

                            If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
                                Me.Id = CType(value, Nullable(Of System.Int32))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Id)
                            End If

                        Case "ExportToCsv"

                            If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
                                Me.ExportToCsv = CType(value, Nullable(Of System.Boolean))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.ExportToCsv)
                            End If

                        Case "ArincChannel"

                            If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
                                Me.ArincChannel = CType(value, Nullable(Of System.Byte))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.ArincChannel)
                            End If

                        Case "ParameterNo"

                            If value Is Nothing Or value.GetType().ToString() = "System.Int16" Then
                                Me.ParameterNo = CType(value, Nullable(Of System.Int16))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.ParameterNo)
                            End If

                        Case "Address"

                            If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
                                Me.Address = CType(value, Nullable(Of System.Int32))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Address)
                            End If

                        Case "DataType"

                            If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
                                Me.DataType = CType(value, Nullable(Of System.Byte))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.DataType)
                            End If

                        Case "CardNumber"

                            If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
                                Me.CardNumber = CType(value, Nullable(Of System.Byte))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.CardNumber)
                            End If

                        Case "CoreNumber"

                            If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
                                Me.CoreNumber = CType(value, Nullable(Of System.Byte))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.CoreNumber)
                            End If

                        Case "ChannelNumber"

                            If value Is Nothing Or value.GetType().ToString() = "System.Byte" Then
                                Me.ChannelNumber = CType(value, Nullable(Of System.Byte))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.ChannelNumber)
                            End If

                        Case "Slope"

                            If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
                                Me.Slope = CType(value, Nullable(Of System.Single))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Slope)
                            End If

                        Case "Offset"

                            If value Is Nothing Or value.GetType().ToString() = "System.Single" Then
                                Me.Offset = CType(value, Nullable(Of System.Single))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.Offset)
                            End If

                        Case "LoggerEnabled"

                            If value Is Nothing Or value.GetType().ToString() = "System.Boolean" Then
                                Me.LoggerEnabled = CType(value, Nullable(Of System.Boolean))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.LoggerEnabled)
                            End If

                        Case "LoggerIndex"

                            If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
                                Me.LoggerIndex = CType(value, Nullable(Of System.Int32))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.LoggerIndex)
                            End If

                        Case "LogReportOrderInCategorie"

                            If value Is Nothing Or value.GetType().ToString() = "System.Int32" Then
                                Me.LogReportOrderInCategorie = CType(value, Nullable(Of System.Int32))
                                OnPropertyChanged(OmniBusBoxUartTemplateMetadata.PropertyNames.LogReportOrderInCategorie)
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

            Public Sub New(ByVal entity As esOmniBusBoxUartTemplate)
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

                Set(ByVal Value As System.String)
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

            Public Property PwcPn As System.String
                Get
                    Dim data_ As System.String = entity.PwcPn

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.PwcPn = Nothing
                    Else
                        entity.PwcPn = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property Name As System.String
                Get
                    Dim data_ As System.String = entity.Name

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.Name = Nothing
                    Else
                        entity.Name = Convert.ToString(Value)
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

                Set(ByVal Value As System.String)
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

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.Source = Nothing
                    Else
                        entity.Source = Convert.ToString(Value)
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

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.ArincChannel = Nothing
                    Else
                        entity.ArincChannel = Convert.ToByte(Value)
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

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.ParameterNo = Nothing
                    Else
                        entity.ParameterNo = Convert.ToInt16(Value)
                    End If
                End Set
            End Property

            Public Property Parameter As System.String
                Get
                    Dim data_ As System.String = entity.Parameter

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.Parameter = Nothing
                    Else
                        entity.Parameter = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property Address As System.String
                Get
                    Dim data_ As Nullable(Of System.Int32) = entity.Address

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.Address = Nothing
                    Else
                        entity.Address = Convert.ToInt32(Value)
                    End If
                End Set
            End Property

            Public Property DataType As System.String
                Get
                    Dim data_ As Nullable(Of System.Byte) = entity.DataType

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.DataType = Nothing
                    Else
                        entity.DataType = Convert.ToByte(Value)
                    End If
                End Set
            End Property

            Public Property Units As System.String
                Get
                    Dim data_ As System.String = entity.Units

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.Units = Nothing
                    Else
                        entity.Units = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property Format As System.String
                Get
                    Dim data_ As System.String = entity.Format

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.Format = Nothing
                    Else
                        entity.Format = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property CardNumber As System.String
                Get
                    Dim data_ As Nullable(Of System.Byte) = entity.CardNumber

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.CardNumber = Nothing
                    Else
                        entity.CardNumber = Convert.ToByte(Value)
                    End If
                End Set
            End Property

            Public Property CoreNumber As System.String
                Get
                    Dim data_ As Nullable(Of System.Byte) = entity.CoreNumber

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.CoreNumber = Nothing
                    Else
                        entity.CoreNumber = Convert.ToByte(Value)
                    End If
                End Set
            End Property

            Public Property ChannelNumber As System.String
                Get
                    Dim data_ As Nullable(Of System.Byte) = entity.ChannelNumber

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.ChannelNumber = Nothing
                    Else
                        entity.ChannelNumber = Convert.ToByte(Value)
                    End If
                End Set
            End Property

            Public Property Slope As System.String
                Get
                    Dim data_ As Nullable(Of System.Single) = entity.Slope

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.Slope = Nothing
                    Else
                        entity.Slope = Convert.ToSingle(Value)
                    End If
                End Set
            End Property

            Public Property Offset As System.String
                Get
                    Dim data_ As Nullable(Of System.Single) = entity.Offset

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.Offset = Nothing
                    Else
                        entity.Offset = Convert.ToSingle(Value)
                    End If
                End Set
            End Property

            Public Property LoggerEnabled As System.String
                Get
                    Dim data_ As Nullable(Of System.Boolean) = entity.LoggerEnabled

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.LoggerEnabled = Nothing
                    Else
                        entity.LoggerEnabled = Convert.ToBoolean(Value)
                    End If
                End Set
            End Property

            Public Property LoggerIndex As System.String
                Get
                    Dim data_ As Nullable(Of System.Int32) = entity.LoggerIndex

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.LoggerIndex = Nothing
                    Else
                        entity.LoggerIndex = Convert.ToInt32(Value)
                    End If
                End Set
            End Property

            Public Property LogReportCategorie As System.String
                Get
                    Dim data_ As System.String = entity.LogReportCategorie

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.LogReportCategorie = Nothing
                    Else
                        entity.LogReportCategorie = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property LogReportOrderInCategorie As System.String
                Get
                    Dim data_ As Nullable(Of System.Int32) = entity.LogReportOrderInCategorie

                    If Not data_.HasValue Then

                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(value) Then
                        entity.LogReportOrderInCategorie = Nothing
                    Else
                        entity.LogReportOrderInCategorie = Convert.ToInt32(Value)
                    End If
                End Set
            End Property


            Private entity As esOmniBusBoxUartTemplate
        End Class


        <NonSerialized> _
        <IgnoreDataMember> _
              Private _esstrings As esStrings

#End Region

#Region "Housekeeping methods"

        Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return OmniBusBoxUartTemplateMetadata.Meta()
            End Get
        End Property

#End Region

#Region "Query Logic"

        Public ReadOnly Property Query() As OmniBusBoxUartTemplateQuery
            Get
                If Me.m_query Is Nothing Then
                    Me.m_query = New OmniBusBoxUartTemplateQuery()
                    InitQuery(Me.m_query)
                End If

                Return Me.m_query
            End Get
        End Property

        Public Overloads Function Load(ByVal query As OmniBusBoxUartTemplateQuery) As Boolean
            Me.m_query = query
            InitQuery(Me.m_query)
            Return Me.Query.Load()
        End Function

        Protected Sub InitQuery(ByVal query As OmniBusBoxUartTemplateQuery)
            query.OnLoadDelegate = AddressOf OnQueryLoaded

            If Not query.es2.HasConnection Then
                query.es2.Connection = DirectCast(Me, IEntity).Connection
            End If
        End Sub

#End Region

        <IgnoreDataMember> _
        Private m_query As OmniBusBoxUartTemplateQuery

    End Class



    <Serializable()> _
    Partial Public MustInherit Class esOmniBusBoxUartTemplateCollection
        Inherits esEntityCollection(Of OmniBusBoxUartTemplate)

#Region "Housekeeping methods"
        Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return OmniBusBoxUartTemplateMetadata.Meta()
            End Get
        End Property

        Protected Overloads Overrides Function GetCollectionName() As String
            Return "OmniBusBoxUartTemplateCollection"
        End Function

#End Region

#Region "Query Logic"


        <BrowsableAttribute(False)> _
        Public ReadOnly Property Query() As OmniBusBoxUartTemplateQuery
            Get
                If Me.m_query Is Nothing Then
                    Me.m_query = New OmniBusBoxUartTemplateQuery()
                    InitQuery(Me.m_query)
                End If

                Return Me.m_query
            End Get
        End Property

        Public Overloads Function Load(ByVal query As OmniBusBoxUartTemplateQuery) As Boolean
            Me.m_query = query
            InitQuery(Me.m_query)
            Return Query.Load()
        End Function

        Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
            If Me.m_query Is Nothing Then
                Me.m_query = New OmniBusBoxUartTemplateQuery()
                Me.InitQuery(m_query)
            End If
            Return Me.m_query
        End Function

        Protected Sub InitQuery(ByVal query As OmniBusBoxUartTemplateQuery)
            query.OnLoadDelegate = AddressOf OnQueryLoaded

            If Not query.es2.HasConnection Then
                query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
            End If
        End Sub

        Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
            Me.InitQuery(DirectCast(query, OmniBusBoxUartTemplateQuery))
        End Sub

#End Region

        Private m_query As OmniBusBoxUartTemplateQuery
    End Class



    <Serializable> _
    Partial Public MustInherit Class esOmniBusBoxUartTemplateQuery
        Inherits esDynamicQuery

        Protected Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return OmniBusBoxUartTemplateMetadata.Meta()
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
                Case "Name"
                    Return Me.Name
                Case "ExportToCsv"
                    Return Me.ExportToCsv
                Case "Source"
                    Return Me.Source
                Case "ArincChannel"
                    Return Me.ArincChannel
                Case "ParameterNo"
                    Return Me.ParameterNo
                Case "Parameter"
                    Return Me.Parameter
                Case "Address"
                    Return Me.Address
                Case "DataType"
                    Return Me.DataType
                Case "Units"
                    Return Me.Units
                Case "Format"
                    Return Me.Format
                Case "CardNumber"
                    Return Me.CardNumber
                Case "CoreNumber"
                    Return Me.CoreNumber
                Case "ChannelNumber"
                    Return Me.ChannelNumber
                Case "Slope"
                    Return Me.Slope
                Case "Offset"
                    Return Me.Offset
                Case "LoggerEnabled"
                    Return Me.LoggerEnabled
                Case "LoggerIndex"
                    Return Me.LoggerIndex
                Case "LogReportCategorie"
                    Return Me.LogReportCategorie
                Case "LogReportOrderInCategorie"
                    Return Me.LogReportOrderInCategorie
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region

#Region "esQueryItems"


        Public ReadOnly Property Id As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.Id, esSystemType.Int32)
            End Get
        End Property

        Public ReadOnly Property EngineModel As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.EngineModel, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property PwcPn As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.PwcPn, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property Name As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.Name, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property ExportToCsv As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.ExportToCsv, esSystemType.Boolean)
            End Get
        End Property

        Public ReadOnly Property Source As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.Source, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property ArincChannel As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.ArincChannel, esSystemType.Byte)
            End Get
        End Property

        Public ReadOnly Property ParameterNo As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.ParameterNo, esSystemType.Int16)
            End Get
        End Property

        Public ReadOnly Property Parameter As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.Parameter, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property Address As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.Address, esSystemType.Int32)
            End Get
        End Property

        Public ReadOnly Property DataType As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.DataType, esSystemType.Byte)
            End Get
        End Property

        Public ReadOnly Property Units As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.Units, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property Format As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.Format, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property CardNumber As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.CardNumber, esSystemType.Byte)
            End Get
        End Property

        Public ReadOnly Property CoreNumber As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.CoreNumber, esSystemType.Byte)
            End Get
        End Property

        Public ReadOnly Property ChannelNumber As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.ChannelNumber, esSystemType.Byte)
            End Get
        End Property

        Public ReadOnly Property Slope As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.Slope, esSystemType.Single)
            End Get
        End Property

        Public ReadOnly Property Offset As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.Offset, esSystemType.Single)
            End Get
        End Property

        Public ReadOnly Property LoggerEnabled As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.LoggerEnabled, esSystemType.Boolean)
            End Get
        End Property

        Public ReadOnly Property LoggerIndex As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.LoggerIndex, esSystemType.Int32)
            End Get
        End Property

        Public ReadOnly Property LogReportCategorie As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.LogReportCategorie, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property LogReportOrderInCategorie As esQueryItem
            Get
                Return New esQueryItem(Me, OmniBusBoxUartTemplateMetadata.ColumnNames.LogReportOrderInCategorie, esSystemType.Int32)
            End Get
        End Property

#End Region

    End Class



    Partial Public Class OmniBusBoxUartTemplate
        Inherits esOmniBusBoxUartTemplate




    End Class




    <Serializable> _
    Partial Public Class OmniBusBoxUartTemplateMetadata
        Inherits esMetadata
        Implements IMetadata

#Region "Protected Constructor"
        Protected Sub New()
            m_columns = New esColumnMetadataCollection()
            Dim c As esColumnMetadata

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.Id, 0, GetType(System.Int32), esSystemType.Int32)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.Id
            c.NumericPrecision = 10
            c.HasDefault = True
            c.Default = "0"
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.EngineModel, 1, GetType(System.String), esSystemType.String)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.EngineModel
            c.CharacterMaxLength = 32
            c.HasDefault = True
            c.Default = """NO MODEL"""
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.PwcPn, 2, GetType(System.String), esSystemType.String)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.PwcPn
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.Name, 3, GetType(System.String), esSystemType.String)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.Name
            c.CharacterMaxLength = 50
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.ExportToCsv, 4, GetType(System.Boolean), esSystemType.Boolean)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.ExportToCsv
            c.CharacterMaxLength = 2
            c.HasDefault = True
            c.Default = "0"
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.Source, 5, GetType(System.String), esSystemType.String)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.Source
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.ArincChannel, 6, GetType(System.Byte), esSystemType.Byte)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.ArincChannel
            c.NumericPrecision = 3
            c.HasDefault = True
            c.Default = "0"
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.ParameterNo, 7, GetType(System.Int16), esSystemType.Int16)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.ParameterNo
            c.NumericPrecision = 5
            c.HasDefault = True
            c.Default = "0"
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.Parameter, 8, GetType(System.String), esSystemType.String)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.Parameter
            c.CharacterMaxLength = 25
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.Address, 9, GetType(System.Int32), esSystemType.Int32)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.Address
            c.NumericPrecision = 10
            c.HasDefault = True
            c.Default = "0"
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.DataType, 10, GetType(System.Byte), esSystemType.Byte)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.DataType
            c.NumericPrecision = 3
            c.HasDefault = True
            c.Default = "0"
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.Units, 11, GetType(System.String), esSystemType.String)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.Units
            c.CharacterMaxLength = 25
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.Format, 12, GetType(System.String), esSystemType.String)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.Format
            c.CharacterMaxLength = 5
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.CardNumber, 13, GetType(System.Byte), esSystemType.Byte)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.CardNumber
            c.NumericPrecision = 3
            c.HasDefault = True
            c.Default = "0"
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.CoreNumber, 14, GetType(System.Byte), esSystemType.Byte)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.CoreNumber
            c.NumericPrecision = 3
            c.HasDefault = True
            c.Default = "0"
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.ChannelNumber, 15, GetType(System.Byte), esSystemType.Byte)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.ChannelNumber
            c.NumericPrecision = 3
            c.HasDefault = True
            c.Default = "0"
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.Slope, 16, GetType(System.Single), esSystemType.Single)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.Slope
            c.NumericPrecision = 7
            c.HasDefault = True
            c.Default = "0"
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.Offset, 17, GetType(System.Single), esSystemType.Single)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.Offset
            c.NumericPrecision = 7
            c.HasDefault = True
            c.Default = "0"
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.LoggerEnabled, 18, GetType(System.Boolean), esSystemType.Boolean)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.LoggerEnabled
            c.CharacterMaxLength = 2
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.LoggerIndex, 19, GetType(System.Int32), esSystemType.Int32)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.LoggerIndex
            c.NumericPrecision = 10
            c.HasDefault = True
            c.Default = "0"
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.LogReportCategorie, 20, GetType(System.String), esSystemType.String)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.LogReportCategorie
            c.CharacterMaxLength = 50
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(OmniBusBoxUartTemplateMetadata.ColumnNames.LogReportOrderInCategorie, 21, GetType(System.Int32), esSystemType.Int32)
            c.PropertyName = OmniBusBoxUartTemplateMetadata.PropertyNames.LogReportOrderInCategorie
            c.NumericPrecision = 10
            c.HasDefault = True
            c.Default = "0"
            c.IsNullable = True
            m_columns.Add(c)

        End Sub
#End Region

        Public Shared Function Meta() As OmniBusBoxUartTemplateMetadata
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
            Public Const Id As String = "ID"
            Public Const EngineModel As String = "EngineModel"
            Public Const PwcPn As String = "PWC_PN"
            Public Const Name As String = "Name"
            Public Const ExportToCsv As String = "ExportToCsv"
            Public Const Source As String = "Source"
            Public Const ArincChannel As String = "Arinc_Channel"
            Public Const ParameterNo As String = "Parameter_No"
            Public Const Parameter As String = "Parameter"
            Public Const Address As String = "Address"
            Public Const DataType As String = "Data_Type"
            Public Const Units As String = "Units"
            Public Const Format As String = "Format"
            Public Const CardNumber As String = "Card_Number"
            Public Const CoreNumber As String = "Core_Number"
            Public Const ChannelNumber As String = "Channel_Number"
            Public Const Slope As String = "Slope"
            Public Const Offset As String = "Offset"
            Public Const LoggerEnabled As String = "Logger_Enabled"
            Public Const LoggerIndex As String = "Logger_Index"
            Public Const LogReportCategorie As String = "Log_Report_Categorie"
            Public Const LogReportOrderInCategorie As String = "Log_Report_Order_In_Categorie"
        End Class
#End Region

#Region "PropertyNames"
        Public Class PropertyNames
            Public Const Id As String = "Id"
            Public Const EngineModel As String = "EngineModel"
            Public Const PwcPn As String = "PwcPn"
            Public Const Name As String = "Name"
            Public Const ExportToCsv As String = "ExportToCsv"
            Public Const Source As String = "Source"
            Public Const ArincChannel As String = "ArincChannel"
            Public Const ParameterNo As String = "ParameterNo"
            Public Const Parameter As String = "Parameter"
            Public Const Address As String = "Address"
            Public Const DataType As String = "DataType"
            Public Const Units As String = "Units"
            Public Const Format As String = "Format"
            Public Const CardNumber As String = "CardNumber"
            Public Const CoreNumber As String = "CoreNumber"
            Public Const ChannelNumber As String = "ChannelNumber"
            Public Const Slope As String = "Slope"
            Public Const Offset As String = "Offset"
            Public Const LoggerEnabled As String = "LoggerEnabled"
            Public Const LoggerIndex As String = "LoggerIndex"
            Public Const LogReportCategorie As String = "LogReportCategorie"
            Public Const LogReportOrderInCategorie As String = "LogReportOrderInCategorie"
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
            SyncLock GetType(OmniBusBoxUartTemplateMetadata)

                If OmniBusBoxUartTemplateMetadata.mapDelegates Is Nothing Then
                    OmniBusBoxUartTemplateMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
                End If

                If OmniBusBoxUartTemplateMetadata._meta Is Nothing Then
                    OmniBusBoxUartTemplateMetadata._meta = New OmniBusBoxUartTemplateMetadata()
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



                meta.AddTypeMap("Id", New esTypeMap("Long", "System.Int32"))
                meta.AddTypeMap("EngineModel", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("PwcPn", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("Name", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("ExportToCsv", New esTypeMap("Yes/No", "System.Boolean"))
                meta.AddTypeMap("Source", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("ArincChannel", New esTypeMap("Byte", "System.Byte"))
                meta.AddTypeMap("ParameterNo", New esTypeMap("Integer", "System.Int16"))
                meta.AddTypeMap("Parameter", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("Address", New esTypeMap("Long", "System.Int32"))
                meta.AddTypeMap("DataType", New esTypeMap("Byte", "System.Byte"))
                meta.AddTypeMap("Units", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("Format", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("CardNumber", New esTypeMap("Byte", "System.Byte"))
                meta.AddTypeMap("CoreNumber", New esTypeMap("Byte", "System.Byte"))
                meta.AddTypeMap("ChannelNumber", New esTypeMap("Byte", "System.Byte"))
                meta.AddTypeMap("Slope", New esTypeMap("Single", "System.Single"))
                meta.AddTypeMap("Offset", New esTypeMap("Single", "System.Single"))
                meta.AddTypeMap("LoggerEnabled", New esTypeMap("Yes/No", "System.Boolean"))
                meta.AddTypeMap("LoggerIndex", New esTypeMap("Long", "System.Int32"))
                meta.AddTypeMap("LogReportCategorie", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("LogReportOrderInCategorie", New esTypeMap("Long", "System.Int32"))



                meta.Source = "OmniBusBoxUartTemplate"
                meta.Destination = "OmniBusBoxUartTemplate"

                meta.spInsert = "proc_OmniBusBoxUartTemplateInsert"
                meta.spUpdate = "proc_OmniBusBoxUartTemplateUpdate"
                meta.spDelete = "proc_OmniBusBoxUartTemplateDelete"
                meta.spLoadAll = "proc_OmniBusBoxUartTemplateLoadAll"
                meta.spLoadByPrimaryKey = "proc_OmniBusBoxUartTemplateLoadByPrimaryKey"

                Me.m_providerMetadataMaps.Add("esDefault", meta)

            End If

            Return Me.m_providerMetadataMaps("esDefault")

        End Function

#End Region

        Private Shared _meta As OmniBusBoxUartTemplateMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
        Private Shared _esDefault As Integer = RegisterDelegateesDefault()

    End Class

End Namespace


'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 10/21/2019 10:06:35 AM
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
    ' Encapsulates the 'EngineeringUnitConversion' table
    ' </summary>

    <System.Diagnostics.DebuggerDisplay("Data = {Debug}")>
    <Serializable>
    <DataContract>
    <KnownType(GetType(EngineeringUnitConversion))>
    <XmlType("EngineeringUnitConversion")>
    Partial Public Class EngineeringUnitConversion
        Inherits esEngineeringUnitConversion

        <DebuggerBrowsable(DebuggerBrowsableState.RootHidden Or DebuggerBrowsableState.Never)>
        Protected Overrides ReadOnly Property Debug() As esEntityDebuggerView()
            Get
                Return MyBase.Debug
            End Get
        End Property

        Public Overrides Function CreateInstance() As esEntity
            Return New EngineeringUnitConversion()
        End Function

#Region "Static Quick Access Methods"

#End Region



    End Class



    <DebuggerDisplay("Count = {Count}")>
    <Serializable>
    <CollectionDataContract>
    <XmlType("EngineeringUnitConversionCollection")>
    Partial Public Class EngineeringUnitConversionCollection
        Inherits esEngineeringUnitConversionCollection
        Implements IEnumerable(Of EngineeringUnitConversion)




#Region "WCF Service Class"

        <DataContract>
        <KnownType(GetType(EngineeringUnitConversion))>
        Public Class EngineeringUnitConversionCollectionWCFPacket
            Inherits esCollectionWCFPacket(Of EngineeringUnitConversionCollection)

            Public Shared Widening Operator CType(packet As EngineeringUnitConversionCollectionWCFPacket) As EngineeringUnitConversionCollection
                Return packet.Collection
            End Operator

            Public Shared Widening Operator CType(collection As EngineeringUnitConversionCollection) As EngineeringUnitConversionCollectionWCFPacket
                Return New EngineeringUnitConversionCollectionWCFPacket() With {.Collection = collection}
            End Operator

        End Class

#End Region



    End Class




    <DebuggerDisplay("Query = {Parse()}")>
    <Serializable>
    Partial Public Class EngineeringUnitConversionQuery
        Inherits esEngineeringUnitConversionQuery

        Public Sub New()
        End Sub

        Public Sub New(ByVal joinAlias As String)
            Me.es.JoinAlias = joinAlias
        End Sub

        Protected Overrides Function GetQueryName() As String
            Return "EngineeringUnitConversionQuery"
        End Function

#Region "Explicit Casts"

        Public Shared Narrowing Operator CType(ByVal query As EngineeringUnitConversionQuery) As String
            Return EngineeringUnitConversionQuery.SerializeHelper.ToXml(query)
        End Operator

        Public Shared Narrowing Operator CType(ByVal query As String) As EngineeringUnitConversionQuery
            Return DirectCast(EngineeringUnitConversionQuery.SerializeHelper.FromXml(query, GetType(EngineeringUnitConversionQuery)), EngineeringUnitConversionQuery)
        End Operator

#End Region

    End Class


    <DataContract>
    <Serializable()>
    Partial Public MustInherit Class esEngineeringUnitConversion
        Inherits esEntity
        Implements INotifyPropertyChanged

        Public Sub New()

        End Sub


#Region "Properties"



        ' <summary>
        ' Maps to EngineeringUnitConversion.UnitImperial
        ' </summary>
        <DataMember(EmitDefaultValue:=False)>
        Public Overridable Property UnitImperial As System.String
            Get
                Return MyBase.GetSystemString(EngineeringUnitConversionMetadata.ColumnNames.UnitImperial)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(EngineeringUnitConversionMetadata.ColumnNames.UnitImperial, value) Then
                    OnPropertyChanged(EngineeringUnitConversionMetadata.PropertyNames.UnitImperial)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to EngineeringUnitConversion.DisplayUnitImperial
        ' </summary>
        <DataMember(EmitDefaultValue:=False)>
        Public Overridable Property DisplayUnitImperial As System.String
            Get
                Return MyBase.GetSystemString(EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitImperial)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitImperial, value) Then
                    OnPropertyChanged(EngineeringUnitConversionMetadata.PropertyNames.DisplayUnitImperial)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to EngineeringUnitConversion.UnitMetric
        ' </summary>
        <DataMember(EmitDefaultValue:=False)>
        Public Overridable Property UnitMetric As System.String
            Get
                Return MyBase.GetSystemString(EngineeringUnitConversionMetadata.ColumnNames.UnitMetric)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(EngineeringUnitConversionMetadata.ColumnNames.UnitMetric, value) Then
                    OnPropertyChanged(EngineeringUnitConversionMetadata.PropertyNames.UnitMetric)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to EngineeringUnitConversion.DisplayUnitMetric
        ' </summary>
        <DataMember(EmitDefaultValue:=False)>
        Public Overridable Property DisplayUnitMetric As System.String
            Get
                Return MyBase.GetSystemString(EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitMetric)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitMetric, value) Then
                    OnPropertyChanged(EngineeringUnitConversionMetadata.PropertyNames.DisplayUnitMetric)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to EngineeringUnitConversion.UnitSI
        ' </summary>
        <DataMember(EmitDefaultValue:=False)>
        Public Overridable Property UnitSI As System.String
            Get
                Return MyBase.GetSystemString(EngineeringUnitConversionMetadata.ColumnNames.UnitSI)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(EngineeringUnitConversionMetadata.ColumnNames.UnitSI, value) Then
                    OnPropertyChanged(EngineeringUnitConversionMetadata.PropertyNames.UnitSI)
                End If
            End Set
        End Property

        ' <summary>
        ' Maps to EngineeringUnitConversion.DisplayUnitSI
        ' </summary>
        <DataMember(EmitDefaultValue:=False)>
        Public Overridable Property DisplayUnitSI As System.String
            Get
                Return MyBase.GetSystemString(EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitSI)
            End Get

            Set(ByVal value As System.String)
                If MyBase.SetSystemString(EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitSI, value) Then
                    OnPropertyChanged(EngineeringUnitConversionMetadata.PropertyNames.DisplayUnitSI)
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

                        Case "UnitImperial"
                            Me.str().UnitImperial = CType(value, String)

                        Case "DisplayUnitImperial"
                            Me.str().DisplayUnitImperial = CType(value, String)

                        Case "UnitMetric"
                            Me.str().UnitMetric = CType(value, String)

                        Case "DisplayUnitMetric"
                            Me.str().DisplayUnitMetric = CType(value, String)

                        Case "UnitSI"
                            Me.str().UnitSI = CType(value, String)

                        Case "DisplayUnitSI"
                            Me.str().DisplayUnitSI = CType(value, String)

                    End Select

                Else

                    Select Case name


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

            Public Sub New(ByVal entity As esEngineeringUnitConversion)
                Me.entity = entity
            End Sub


            Public Property UnitImperial As System.String
                Get
                    Dim data_ As System.String = entity.UnitImperial

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(Value) Then
                        entity.UnitImperial = Nothing
                    Else
                        entity.UnitImperial = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property DisplayUnitImperial As System.String
                Get
                    Dim data_ As System.String = entity.DisplayUnitImperial

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(Value) Then
                        entity.DisplayUnitImperial = Nothing
                    Else
                        entity.DisplayUnitImperial = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property UnitMetric As System.String
                Get
                    Dim data_ As System.String = entity.UnitMetric

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(Value) Then
                        entity.UnitMetric = Nothing
                    Else
                        entity.UnitMetric = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property DisplayUnitMetric As System.String
                Get
                    Dim data_ As System.String = entity.DisplayUnitMetric

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(Value) Then
                        entity.DisplayUnitMetric = Nothing
                    Else
                        entity.DisplayUnitMetric = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property UnitSI As System.String
                Get
                    Dim data_ As System.String = entity.UnitSI

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(Value) Then
                        entity.UnitSI = Nothing
                    Else
                        entity.UnitSI = Convert.ToString(Value)
                    End If
                End Set
            End Property

            Public Property DisplayUnitSI As System.String
                Get
                    Dim data_ As System.String = entity.DisplayUnitSI

                    If data_ Is Nothing Then
                        Return String.Empty
                    Else
                        Return Convert.ToString(data_)
                    End If
                End Get

                Set(ByVal Value As System.String)
                    If String.IsNullOrEmpty(Value) Then
                        entity.DisplayUnitSI = Nothing
                    Else
                        entity.DisplayUnitSI = Convert.ToString(Value)
                    End If
                End Set
            End Property


            Private entity As esEngineeringUnitConversion
        End Class


        <NonSerialized>
        <IgnoreDataMember>
        Private _esstrings As esStrings

#End Region

#Region "Housekeeping methods"

        Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return EngineeringUnitConversionMetadata.Meta()
            End Get
        End Property

#End Region

#Region "Query Logic"

        Public ReadOnly Property Query() As EngineeringUnitConversionQuery
            Get
                If Me.m_query Is Nothing Then
                    Me.m_query = New EngineeringUnitConversionQuery()
                    InitQuery(Me.m_query)
                End If

                Return Me.m_query
            End Get
        End Property

        Public Overloads Function Load(ByVal query As EngineeringUnitConversionQuery) As Boolean
            Me.m_query = query
            InitQuery(Me.m_query)
            Return Me.Query.Load()
        End Function

        Protected Sub InitQuery(ByVal query As EngineeringUnitConversionQuery)
            query.OnLoadDelegate = AddressOf OnQueryLoaded

            If Not query.es2.HasConnection Then
                query.es2.Connection = DirectCast(Me, IEntity).Connection
            End If
        End Sub

#End Region

        <IgnoreDataMember>
        Private m_query As EngineeringUnitConversionQuery

    End Class



    <Serializable()>
    Partial Public MustInherit Class esEngineeringUnitConversionCollection
        Inherits esEntityCollection(Of EngineeringUnitConversion)

#Region "Housekeeping methods"
        Protected Overloads Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return EngineeringUnitConversionMetadata.Meta()
            End Get
        End Property

        Protected Overloads Overrides Function GetCollectionName() As String
            Return "EngineeringUnitConversionCollection"
        End Function

#End Region

#Region "Query Logic"


        <BrowsableAttribute(False)>
        Public ReadOnly Property Query() As EngineeringUnitConversionQuery
            Get
                If Me.m_query Is Nothing Then
                    Me.m_query = New EngineeringUnitConversionQuery()
                    InitQuery(Me.m_query)
                End If

                Return Me.m_query
            End Get
        End Property

        Public Overloads Function Load(ByVal query As EngineeringUnitConversionQuery) As Boolean
            Me.m_query = query
            InitQuery(Me.m_query)
            Return query.Load()
        End Function

        Protected Overloads Overrides Function GetDynamicQuery() As esDynamicQuery
            If Me.m_query Is Nothing Then
                Me.m_query = New EngineeringUnitConversionQuery()
                Me.InitQuery(m_query)
            End If
            Return Me.m_query
        End Function

        Protected Sub InitQuery(ByVal query As EngineeringUnitConversionQuery)
            query.OnLoadDelegate = AddressOf OnQueryLoaded

            If Not query.es2.HasConnection Then
                query.es2.Connection = DirectCast(Me, IEntityCollection).Connection
            End If
        End Sub

        Protected Overloads Overrides Sub HookupQuery(ByVal query As esDynamicQuery)
            Me.InitQuery(DirectCast(query, EngineeringUnitConversionQuery))
        End Sub

#End Region

        Private m_query As EngineeringUnitConversionQuery
    End Class



    <Serializable>
    Partial Public MustInherit Class esEngineeringUnitConversionQuery
        Inherits esDynamicQuery

        Protected Overrides ReadOnly Property Meta() As IMetadata
            Get
                Return EngineeringUnitConversionMetadata.Meta()
            End Get
        End Property

#Region "QueryItemFromName"

        Protected Overrides Function QueryItemFromName(ByVal name As String) As esQueryItem
            Select Case name
                Case "UnitImperial"
                    Return Me.UnitImperial
                Case "DisplayUnitImperial"
                    Return Me.DisplayUnitImperial
                Case "UnitMetric"
                    Return Me.UnitMetric
                Case "DisplayUnitMetric"
                    Return Me.DisplayUnitMetric
                Case "UnitSI"
                    Return Me.UnitSI
                Case "DisplayUnitSI"
                    Return Me.DisplayUnitSI
                Case Else
                    Return Nothing
            End Select
        End Function

#End Region

#Region "esQueryItems"


        Public ReadOnly Property UnitImperial As esQueryItem
            Get
                Return New esQueryItem(Me, EngineeringUnitConversionMetadata.ColumnNames.UnitImperial, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property DisplayUnitImperial As esQueryItem
            Get
                Return New esQueryItem(Me, EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitImperial, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property UnitMetric As esQueryItem
            Get
                Return New esQueryItem(Me, EngineeringUnitConversionMetadata.ColumnNames.UnitMetric, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property DisplayUnitMetric As esQueryItem
            Get
                Return New esQueryItem(Me, EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitMetric, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property UnitSI As esQueryItem
            Get
                Return New esQueryItem(Me, EngineeringUnitConversionMetadata.ColumnNames.UnitSI, esSystemType.String)
            End Get
        End Property

        Public ReadOnly Property DisplayUnitSI As esQueryItem
            Get
                Return New esQueryItem(Me, EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitSI, esSystemType.String)
            End Get
        End Property

#End Region

    End Class



    Partial Public Class EngineeringUnitConversion
        Inherits esEngineeringUnitConversion




    End Class




    <Serializable>
    Partial Public Class EngineeringUnitConversionMetadata
        Inherits esMetadata
        Implements IMetadata

#Region "Protected Constructor"
        Protected Sub New()
            m_columns = New esColumnMetadataCollection()
            Dim c As esColumnMetadata

            c = New esColumnMetadata(EngineeringUnitConversionMetadata.ColumnNames.UnitImperial, 0, GetType(System.String), esSystemType.String)
            c.PropertyName = EngineeringUnitConversionMetadata.PropertyNames.UnitImperial
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitImperial, 1, GetType(System.String), esSystemType.String)
            c.PropertyName = EngineeringUnitConversionMetadata.PropertyNames.DisplayUnitImperial
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(EngineeringUnitConversionMetadata.ColumnNames.UnitMetric, 2, GetType(System.String), esSystemType.String)
            c.PropertyName = EngineeringUnitConversionMetadata.PropertyNames.UnitMetric
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitMetric, 3, GetType(System.String), esSystemType.String)
            c.PropertyName = EngineeringUnitConversionMetadata.PropertyNames.DisplayUnitMetric
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(EngineeringUnitConversionMetadata.ColumnNames.UnitSI, 4, GetType(System.String), esSystemType.String)
            c.PropertyName = EngineeringUnitConversionMetadata.PropertyNames.UnitSI
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

            c = New esColumnMetadata(EngineeringUnitConversionMetadata.ColumnNames.DisplayUnitSI, 5, GetType(System.String), esSystemType.String)
            c.PropertyName = EngineeringUnitConversionMetadata.PropertyNames.DisplayUnitSI
            c.CharacterMaxLength = 255
            c.IsNullable = True
            m_columns.Add(c)

        End Sub
#End Region

        Public Shared Function Meta() As EngineeringUnitConversionMetadata
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
            Public Const UnitImperial As String = "UnitImperial"
            Public Const DisplayUnitImperial As String = "DisplayUnitImperial"
            Public Const UnitMetric As String = "UnitMetric"
            Public Const DisplayUnitMetric As String = "DisplayUnitMetric"
            Public Const UnitSI As String = "UnitSI"
            Public Const DisplayUnitSI As String = "DisplayUnitSI"
        End Class
#End Region

#Region "PropertyNames"
        Public Class PropertyNames
            Public Const UnitImperial As String = "UnitImperial"
            Public Const DisplayUnitImperial As String = "DisplayUnitImperial"
            Public Const UnitMetric As String = "UnitMetric"
            Public Const DisplayUnitMetric As String = "DisplayUnitMetric"
            Public Const UnitSI As String = "UnitSI"
            Public Const DisplayUnitSI As String = "DisplayUnitSI"
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
            SyncLock GetType(EngineeringUnitConversionMetadata)

                If EngineeringUnitConversionMetadata.mapDelegates Is Nothing Then
                    EngineeringUnitConversionMetadata.mapDelegates = New Dictionary(Of String, MapToMeta)
                End If

                If EngineeringUnitConversionMetadata._meta Is Nothing Then
                    EngineeringUnitConversionMetadata._meta = New EngineeringUnitConversionMetadata()
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



                meta.AddTypeMap("UnitImperial", New esTypeMap("Text", "System.String"))
                meta.AddTypeMap("DisplayUnitImperial", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("UnitMetric", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DisplayUnitMetric", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("UnitSI", new esTypeMap("Text", "System.String"))
				meta.AddTypeMap("DisplayUnitSI", new esTypeMap("Text", "System.String"))			
				
				
				 
				meta.Source = "EngineeringUnitConversion"
				meta.Destination = "EngineeringUnitConversion"
				
				meta.spInsert = "proc_EngineeringUnitConversionInsert"
				meta.spUpdate = "proc_EngineeringUnitConversionUpdate"
				meta.spDelete = "proc_EngineeringUnitConversionDelete"
				meta.spLoadAll = "proc_EngineeringUnitConversionLoadAll"
				meta.spLoadByPrimaryKey = "proc_EngineeringUnitConversionLoadByPrimaryKey"
				
				Me.m_providerMetadataMaps.Add("esDefault", meta)

			End If

			Return Me.m_providerMetadataMaps("esDefault")

		End Function
		
#End Region	
		
        Private Shared _meta As EngineeringUnitConversionMetadata
        Protected Shared mapDelegates As Dictionary(Of String, MapToMeta)
		Private Shared _esDefault As Integer = RegisterDelegateesDefault()	
		
	End Class
	
End Namespace

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.IO
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Reflection
Imports System.Data
Imports System.Data.OleDb
Imports System.ComponentModel
Imports System.Runtime.InteropServices
'Imports System.Linq
Imports System.Text
Imports System.Configuration

Imports KineticaRT
'Imports KineticaRT.Studio.Application
Imports KineticaRT.Sequence
Imports KineticaRT.Values
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Xml.Linq
Imports CEL.Library

'Imports Microsoft.Office.Interop

'ft Reference to System.Configuration is Required


Namespace CEL.HMI

#Region "cRIOConfiguration Auxiliary Classes"
    'config for the cRIO
    Public Class cRIOConfigurationSection
        Inherits ConfigurationSection

        ' Set the ConsoleElement. 
        <ConfigurationProperty("cRIOConfig")> _
        Public Property cRIOConfig() As cRIOInformationElement
            Get
                Return (DirectCast(Me("cRIOConfig"), cRIOInformationElement))
            End Get
            Set(ByVal value As cRIOInformationElement)
                Me("cRIOConfig") = value
            End Set
        End Property
    End Class

    'class specific for the cRIOConfigurationSection
    Public Class cRIOInformationElement
        Inherits System.Configuration.ConfigurationElement

        <ConfigurationProperty("ipAddress", defaultValue:="192.168.2.201", IsRequired:=False)> _
        Public Property IpAdress() As String
            Get
                Return DirectCast(Me("ipAddress"), String)
            End Get
            Set(ByVal value As String)
                Me("ipAddress") = value
            End Set
        End Property
        <ConfigurationProperty("port", defaultValue:="55555", IsRequired:=False)> _
        Public Property port() As String
            Get
                Return DirectCast(Me("port"), String)
            End Get
            Set(ByVal value As String)
                Me("port") = value
            End Set
        End Property
    End Class
#End Region

#Region "QnxConfiguration Auxiliary Classes"
    'config for the qnx ftp upload
    Public Class QnxConfigurationSection
        Inherits ConfigurationSection

        ' Set the ConsoleElement. 
        <ConfigurationProperty("qnxConfig")> _
        Public Property QnxConfig() As QnxInformationElement
            Get
                Return (DirectCast(Me("qnxConfig"), QnxInformationElement))
            End Get
            Set(ByVal value As QnxInformationElement)
                Me("qnxConfig") = value
            End Set
        End Property


    End Class

    'class specific for the qnxConfigurationSection
    Public Class QnxInformationElement
        Inherits System.Configuration.ConfigurationElement

        <ConfigurationProperty("ipAddress", defaultValue:="192.168.2.200", IsRequired:=True)> _
        Public Property IpAdress() As String
            Get
                Return DirectCast(Me("ipAddress"), String)
            End Get
            Set(ByVal value As String)
                Me("ipAddress") = value
            End Set
        End Property
        <ConfigurationProperty("uploadPath", defaultValue:="./app/app", IsRequired:=True)> _
        Public Property UploadPath() As String
            Get
                Return DirectCast(Me("uploadPath"), String)
            End Get
            Set(ByVal value As String)
                Me("uploadPath") = value
            End Set
        End Property
        <ConfigurationProperty("userName", defaultValue:="Virgo2000", IsRequired:=True)> _
        Public Property UserName() As String
            Get
                Return DirectCast(Me("userName"), String)
            End Get
            Set(ByVal value As String)
                Me("userName") = value
            End Set
        End Property
        <ConfigurationProperty("password", defaultValue:="v2000", IsRequired:=True)> _
        Public Property Password() As String
            Get
                Return DirectCast(Me("password"), String)
            End Get
            Set(ByVal value As String)
                Me("password") = value
            End Set
        End Property
    End Class


#End Region

#Region "UnitronConfiguration Auxiliary Classes"
    'config for the Unitron Modbus TCP/IP
    Public Class UnitronConfigurationSection
        Inherits ConfigurationSection

        ' Set the ConsoleElement. 
        <ConfigurationProperty("UnitronConfig")> _
        Public Property UnitronConfig() As UnitronInformationElement
            Get
                Return (DirectCast(Me("UnitronConfig"), UnitronInformationElement))
            End Get
            Set(ByVal value As UnitronInformationElement)
                Me("UnitronConfig") = value
            End Set
        End Property


    End Class

    'class specific for the UnitronConfigurationSection
    Public Class UnitronInformationElement
        Inherits System.Configuration.ConfigurationElement

        <ConfigurationProperty("ipAddress", defaultValue:="192.168.2.250", IsRequired:=False)> _
        Public Property IpAdress() As String
            Get
                Return DirectCast(Me("ipAddress"), String)
            End Get
            Set(ByVal value As String)
                Me("ipAddress") = value
            End Set
        End Property
        <ConfigurationProperty("ipPort", defaultValue:="502", IsRequired:=False)> _
        Public Property ipPort() As String
            Get
                Return DirectCast(Me("ipPort"), String)
            End Get
            Set(ByVal value As String)
                Me("ipPort") = value
            End Set
        End Property
        <ConfigurationProperty("DefaultCurrentLimit", DefaultValue:="1200", IsRequired:=False)> _
        Public Property DefaultCurrentLimit() As Integer
            Get
                Return DirectCast(Me("DefaultCurrentLimit"), Integer)
            End Get
            Set(ByVal value As Integer)
                Me("DefaultCurrentLimit") = value
            End Set
        End Property
        <ConfigurationProperty("PW100OilPriming", DefaultValue:="300", IsRequired:=False)> _
        Public Property PW100OilPriming() As Integer
            Get
                Return DirectCast(Me("PW100OilPriming"), Integer)
            End Get
            Set(ByVal value As Integer)
                Me("PW100OilPriming") = value
            End Set
        End Property
        <ConfigurationProperty("PW100Motoring", DefaultValue:="600", IsRequired:=False)> _
        Public Property PW100Motoring() As Integer
            Get
                Return DirectCast(Me("PW100Motoring"), Integer)
            End Get
            Set(ByVal value As Integer)
                Me("PW100Motoring") = value
            End Set
        End Property
        <ConfigurationProperty("PW100Cranking", DefaultValue:="1150", IsRequired:=False)> _
        Public Property PW100Cranking() As Integer
            Get
                Return DirectCast(Me("PW100Cranking"), Integer)
            End Get
            Set(ByVal value As Integer)
                Me("PW100Cranking") = value
            End Set
        End Property
    End Class


#End Region

#Region "HobartConfiguration Auxiliary Classes"
    'config for the Hobart Modbus TCP/IP
    Public Class HobartConfigurationSection
        Inherits ConfigurationSection

        ' Set the ConsoleElement. 
        <ConfigurationProperty("HobartConfig")> _
        Public Property HobartConfig() As HobartInformationElement
            Get
                Return (DirectCast(Me("HobartConfig"), HobartInformationElement))
            End Get
            Set(ByVal value As HobartInformationElement)
                Me("HobartConfig") = value
            End Set
        End Property


    End Class

    'class specific for the HobartConfigurationSection
    Public Class HobartInformationElement
        Inherits System.Configuration.ConfigurationElement

        <ConfigurationProperty("ipAddress", defaultValue:="192.168.2.250", IsRequired:=False)> _
        Public Property IpAdress() As String
            Get
                Return DirectCast(Me("ipAddress"), String)
            End Get
            Set(ByVal value As String)
                Me("ipAddress") = value
            End Set
        End Property
        <ConfigurationProperty("ipPort", defaultValue:="502", IsRequired:=False)> _
        Public Property ipPort() As String
            Get
                Return DirectCast(Me("ipPort"), String)
            End Get
            Set(ByVal value As String)
                Me("ipPort") = value
            End Set
        End Property
        <ConfigurationProperty("DefaultCurrentLimit", DefaultValue:="1200", IsRequired:=False)> _
        Public Property DefaultCurrentLimit() As Integer
            Get
                Return DirectCast(Me("DefaultCurrentLimit"), Integer)
            End Get
            Set(ByVal value As Integer)
                Me("DefaultCurrentLimit") = value
            End Set
        End Property

    End Class


#End Region

#Region "CsvExport Auxiliary Classes"

    'class specifique to the loadConfig
    'Load the main section. Contain the type of delimited and global FilePath
    Public Class ExportConfigurationSection
        Inherits ConfigurationSection

        ' Set the ConsoleElement. 
        <ConfigurationProperty("exportInformation")> _
        Public Property ExportConfig() As ExportInformationElement
            Get
                Return (DirectCast(Me("exportInformation"), ExportInformationElement))
            End Get
            Set(ByVal value As ExportInformationElement)
                Me("exportInformation") = value
            End Set
        End Property


    End Class

    'Contain the config for a particular table.
    Public Class ExportFormatConfigurationSection
        Inherits ConfigurationSection
        ' Create a configuration section.
        Public Sub New()
        End Sub

        <ConfigurationProperty("", IsRequired:=False, IsKey:=False, IsDefaultCollection:=True)> _
        Public Property Items() As ExportFormatInformationItemCollection
            Get
                If MyBase.Item("items") Is Nothing Then
                    Return DirectCast(MyBase.Item(""), ExportFormatInformationItemCollection)
                Else
                    Return DirectCast(MyBase.Item("items"), ExportFormatInformationItemCollection)
                End If

            End Get
            Set(value As ExportFormatInformationItemCollection)
                MyBase.Item("items") = value
            End Set
        End Property
    End Class

    'Contain the config of all the tables
    <ConfigurationCollection(GetType(ExportFormatInformationItem), CollectionType:=ConfigurationElementCollectionType.BasicMapAlternate)> _
    Public Class ExportFormatInformationItemCollection
        Inherits ConfigurationElementCollection

        Friend Const ItemPropertyName As String = "exportFormatInformation"

        Public Overrides ReadOnly Property CollectionType() As ConfigurationElementCollectionType
            Get
                Return ConfigurationElementCollectionType.BasicMapAlternate
            End Get
        End Property

        Protected Overrides ReadOnly Property ElementName() As String
            Get
                Return ItemPropertyName
            End Get
        End Property

        Protected Overrides Function IsElementName(elementName As String) As Boolean
            Return (elementName = ItemPropertyName)
        End Function

        Protected Overrides Function GetElementKey(element As ConfigurationElement) As Object
            Return DirectCast(element, ExportFormatInformationItem).TableName
        End Function

        Protected Overrides Function CreateNewElement() As ConfigurationElement
            Return New ExportFormatInformationItem()
        End Function

        Public Overrides Function IsReadOnly() As Boolean
            Return False
        End Function
        Public Function ContainsKey(key As String) As Boolean
            Dim result As Boolean = False
            Dim keys As Object() = BaseGetAllKeys()
            For Each obj As Object In keys
                If DirectCast(obj, String) = key Then
                    result = True
                    Exit For
                End If
            Next
            Return result
        End Function
        Default Public Shadows Property Item(index As Integer) As ExportFormatInformationItem 'ft ConfigurationElement
            Get
                Return DirectCast(BaseGet(index), ExportFormatInformationItem) 'ConfigurationElement
            End Get
            Set(value As ExportFormatInformationItem) 'ConfigurationElement
                If BaseGet(index) IsNot Nothing Then
                    BaseRemoveAt(index)
                End If
                BaseAdd(index, value)
            End Set
        End Property
        Default Public Shadows ReadOnly Property Item(tableName As String) As ExportFormatInformationItem 'ft ConfigurationElement
            Get
                Return DirectCast(BaseGet(tableName), ExportFormatInformationItem) 'ConfigurationElement
            End Get
        End Property
        Public Sub Add(ByVal item As ExportFormatInformationItem)
            BaseAdd(item)
        End Sub
    End Class

    'class specific for the exportConfigurationSection
    Public Class ExportInformationElement
        Inherits System.Configuration.ConfigurationElement

        Public Enum eExportType
            CSVDelimited = 0
            ' XMLDelimited = 1
        End Enum

        <ConfigurationProperty("exportType", defaultValue:="CSVDelimited", IsRequired:=True)> _
        Public Property ExportType() As eExportType
            Get
                Return DirectCast(Me("exportType"), eExportType)
            End Get
            Set(ByVal value As eExportType)
                Me("exportType") = value
            End Set
        End Property
        <ConfigurationProperty("exportPathName", defaultValue:="C:\My Projects\CELDAS 200\Workbench\Csv\", IsRequired:=True)> _
        Public Property ExportPathName() As String
            Get
                Return DirectCast(Me("exportPathName"), String)
            End Get
            Set(ByVal value As String)
                Me("exportPathName") = value
            End Set
        End Property


    End Class

    'class specific for the exportFormatConfigurationSEction
    Public Class ExportFormatInformationItem
        Inherits System.Configuration.ConfigurationElement

        <ConfigurationProperty("fileName", defaultValue:="Unknown", IsRequired:=True)> _
        Public Property FileName() As String
            Get
                Return DirectCast(Me("fileName"), String)
            End Get
            Set(ByVal value As String)
                Me("fileName") = value
            End Set
        End Property
        <ConfigurationProperty("tableName", defaultValue:="Unknown", IsRequired:=True)> _
        Public Property TableName() As String
            Get
                Return DirectCast(Me("tableName"), String)
            End Get
            Set(ByVal value As String)
                Me("tableName") = value
            End Set
        End Property
        <ConfigurationProperty("columnNameHeader", defaultValue:="False", IsRequired:=True)> _
        Public Property ColumnNameHeader() As String
            Get
                Return DirectCast(Me("columnNameHeader"), String)
            End Get
            Set(ByVal value As String)
                Me("columnNameHeader") = value
            End Set
        End Property
        <ConfigurationProperty("decimalSymbol", defaultValue:=".", IsRequired:=True)> _
        Public Property DecimalSymbol() As String
            Get
                Return DirectCast(Me("decimalSymbol"), String)
            End Get
            Set(ByVal value As String)
                Me("decimalSymbol") = value
            End Set
        End Property
        <ConfigurationProperty("numberDigits", defaultValue:="10", IsRequired:=True)> _
        Public Property NumberDigits() As String
            Get
                Return DirectCast(Me("numberDigits"), String)
            End Get
            Set(ByVal value As String)
                Me("numberDigits") = value
            End Set
        End Property
        <ConfigurationProperty("textDelimiter", defaultValue:="none", IsRequired:=True)> _
        Public Property TextDelimiter() As String
            Get
                Return DirectCast(Me("textDelimiter"), String)
            End Get
            Set(ByVal value As String)
                Me("textDelimiter") = value
            End Set
        End Property
    End Class



#End Region

#Region "Project Auxiliary Classes"

    Public Class ProjectInformationElement
        Inherits System.Configuration.ConfigurationElement

        ' Create the element.
        Public Sub New()
        End Sub

        ' Create the element.
        Public Sub New(ByVal projectName As String, ByVal projectNumber As String)

            Me.Name = projectName
            Me.Number = CType(projectNumber, Integer)
        End Sub

        <ConfigurationProperty("Rootpath", DefaultValue:="C:\My Projects\CELDAS 200", IsRequired:=True)> _
        Public Property Rootpath() As String
            Get
                Return DirectCast(Me("Rootpath"), String)
            End Get
            Set(ByVal value As String)
                Me("Rootpath") = value
            End Set
        End Property

        <ConfigurationProperty("name", DefaultValue:="GMF Indonesia", IsRequired:=True)>
        Public Property Name() As String
            Get
                Return DirectCast(Me("name"), String)
            End Get
            Set(ByVal value As String)
                Me("name") = value
            End Set
        End Property

        <ConfigurationProperty("description", DefaultValue:="no description", IsRequired:=True)>
        Public Property Description() As String
            Get
                Return DirectCast(Me("description"), String)
            End Get
            Set(ByVal value As String)
                Me("description") = value
            End Set
        End Property

        <ConfigurationProperty("number", DefaultValue:="10726", IsRequired:=True)>
        Public Property Number() As Integer
            Get
                Return DirectCast(Me("number"), Integer)
            End Get
            Set(ByVal value As Integer)
                Me("number") = value
            End Set
        End Property

        <ConfigurationProperty("referenceNumber", DefaultValue:="0", IsRequired:=True)>
        Public Property ReferenceNumber() As Integer
            Get
                Return DirectCast(Me("referenceNumber"), Integer)
            End Get
            Set(ByVal value As Integer)
                Me("referenceNumber") = value
            End Set
        End Property

        'Public ReadOnly Property Number() As String 'Test de Read ProjectNumber a partir du fichier de config du WorkBench plutot que celui du HMI
        '    Get
        '        Return ReadOneIntegerConfigItem("//ConfigurationSettings/FACILITY/PROJECT_NUMBER", 0).ToString
        '    End Get
        '    'Set(ByVal value As String)
        '    '    Me("number") = value
        '    'End Set

        'End Property

        <ConfigurationProperty("WelcomeScreenSkinFullFileName", DefaultValue:="", IsRequired:=True)> _
        Public Property WelcomeScreenSkinFullFileName() As String
            Get
                Return DirectCast(Me("WelcomeScreenSkinFullFileName"), String)
            End Get
            Set(ByVal value As String)
                Me("WelcomeScreenSkinFullFileName") = value
            End Set
        End Property

        <ConfigurationProperty("workbenchConfigurationFullFileName", DefaultValue:="", IsRequired:=True)> _
        Public Property WorkbenchConfigurationFullFileName() As String
            Get
                Return DirectCast(Me("workbenchConfigurationFullFileName"), String)
            End Get
            Set(ByVal value As String)
                Me("workbenchConfigurationFileName") = value
            End Set
        End Property

        'EXE Application Path for CEC Vibration System Configuration
        <ConfigurationProperty("cecVibrationSystemExeFile", DefaultValue:="", IsRequired:=False)> _
        Public Property CECVibrationSystemExeFile() As String
            Get
                Return DirectCast(Me("cecVibrationSystemExeFile"), String)
            End Get
            Set(ByVal value As String)
                Me("cecVibrationSystemExeFile") = value
            End Set
        End Property

        'EXE Application Path for Pwc Server Dll Processing System Configuration
        <ConfigurationProperty("PwcServerDllProcessingSystemExeFile", DefaultValue:="", IsRequired:=False)> _
        Public Property PwcServerDllProcessingSystemExeFile() As String
            Get
                Return DirectCast(Me("PwcServerDllProcessingSystemExeFile"), String)
            End Get
            Set(ByVal value As String)
                Me("PwcServerDllProcessingSystemExeFile") = value
            End Set
        End Property

        'Password Cell Protection for Performance Reports
        <ConfigurationProperty("PerformanceReportsCellProtection", DefaultValue:="CEL9994", IsRequired:=False)> _
        Public Property PerformanceReportsCellProtection() As String
            Get
                Return DirectCast(Me("PerformanceReportsCellProtection"), String)
            End Get
            Set(ByVal value As String)
                Me("PerformanceReportsCellProtection") = value
            End Set
        End Property

        Public Function ReadOneIntegerConfigItem(ByVal xPath As String, ByVal defaultValue As Integer) As Integer
            Try
                Dim params As String() = xPath.Split("/".ToCharArray)
                Dim i As Integer = params.GetUpperBound(0)

                Dim nodePath As String = "/"
                For j As Integer = 0 To i - 3
                    nodePath = nodePath + "/" + params(j + 2)
                Next j

                Dim xml As XmlDocument = New XmlDocument
                xml.Load(WorkbenchConfigurationFullFileName)
               
                Dim node As XmlNode = xml.SelectSingleNode(nodePath)

                For Each node2 As Object In node.ChildNodes
                    If node2.NodeType = XmlNodeType.Element Then
                        Console.WriteLine(node2.ToString)
                        Dim elm As XmlElement = node2

                        If (elm.Attributes("key").Value = params(i)) Then
                            'Console.WriteLine(elm.Attributes("value").Value)
                            'Console.WriteLine(elm.Attributes("key").Value)
                            Return CType(elm.Attributes("value").Value, Integer)
                        End If
                    End If
                Next

                Return defaultValue
                'Dim params As String() = xPath.Split("/".ToCharArray)
                'Dim i As Integer = params.GetUpperBound(0)
                'If i = 4 Then

                '    Dim xml As XmlDocument = New XmlDocument
                '    xml.Load(WorkbenchConfigurationFullFileName)
                '    Dim doc As XDocument = XDocument.Parse(xml.InnerXml)

                '    For Each row As Object In doc.Root.Element(params(3)).Elements("add") 'Engine ex. APS3200
                '        If row.Attribute("key").Value = params(4) Then ' key T_EGT_NUMBER_PROBE
                '            'Console.WriteLine(row.Attribute("value").Value)  'return value
                '            Return CType(row.Attribute("value").Value, Integer)
                '        End If
                '    Next
                '    Return defaultValue
                'ElseIf i = 5 Then
                '    Dim xml As XmlDocument = New XmlDocument
                '    xml.Load(WorkbenchConfigurationFullFileName)
                '    Dim doc As XDocument = XDocument.Parse(xml.InnerXml)

                '    Dim node As XmlNode = xml.SelectSingleNode("//" + params(2) + "/" + params(3) + "/" + params(4))

                '    Dim element As XmlNode = node.SelectSingleNode(params(5))

                '    For Each row As XmlNode In node 'Engine ex. APS3200
                '        If row.NodeType = XmlNodeType.Attribute Then
                '            If row.Name = params(5) Then ' key T_EGT_NUMBER_PROBE
                '                'Console.WriteLine(row.Attribute("value").Value)  'return value
                '                Return CType(row.Value, Integer)
                '            End If
                '        End If

                '    Next
                '    Return defaultValue

                'Else
                'MessageBox.Show("ReadWorkBenchConfigFile: Invalid xml Path ...  use something like //ConfigurationSettings/APS3200/T_EGT_NUMBER_PROBE")
                'End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("ReadWorkBenchConfigFile: " + ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function

        ''' <summary>
        ''' Identify the configuration value for the ressources parameted to the function.
        ''' </summary>
        ''' <param name="xPath"></param>
        ''' <param name="defaultValue"></param>
        ''' <returns>Return True if disable. False if enabled.</returns>
        ''' <remarks></remarks>
        Public Function ReadOneBooleanConfigItem(ByVal xPath As String, ByVal defaultValue As Boolean) As Boolean
            Try
                Dim params As String() = xPath.Split("/".ToCharArray)
                Dim i As Integer = params.GetUpperBound(0)
                If i = 4 Then

                    Dim xml As XmlDocument = New XmlDocument
                    xml.Load(WorkbenchConfigurationFullFileName)
                    Dim doc As XDocument = XDocument.Parse(xml.InnerXml)

                    For Each row As Object In doc.Root.Element(params(3)).Elements("add") 'add Tag (RESSOURCE)
                        If (row.Attribute("key").Value.ToString Like params(4)) Then ' key tag (NIDAQ_DISABLED)

                            If (row.Attribute("value").Value.ToString Like "false") Then
                                Return False
                            Else
                                Return True 'Default to DISABLED
                            End If

                        End If
                    Next
                    Return defaultValue
                Else
                    clsTopMostMessageBox.Show("ReadWorkBenchConfigFile: Invalid xml Path ...  use something like //ConfigurationSettings/APS3200/T_EGT_NUMBER_PROBE", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Debug.Assert(False)
            Catch ex As Exception
                clsTopMostMessageBox.Show("ReadWorkBenchConfigFile: " + ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function


    End Class


    ' Define a custom section that is used by the application
    ' to create custom configuration sections at the specified 
    ' level in the configuration hierarchy.
    ' This enables the the user that has the proper access 
    ' rights, to make changes to the configuration files.
    Public Class ProjectConfigurationSection
        Inherits ConfigurationSection
        ' Create a configuration section.
        Public Sub New()
        End Sub

        ' Set or get the ConsoleElement. 
        <ConfigurationProperty("projectInformation")> _
        Public Property ProjectElement() As ProjectInformationElement
            Get
                Return (DirectCast(Me("projectInformation"), ProjectInformationElement))
            End Get
            Set(ByVal value As ProjectInformationElement)
                Me("projectInformation") = value
            End Set
        End Property
    End Class
#End Region

#Region "Engine Auxiliary Classes"

    Public Class EngineConfigurationSection
        Inherits ConfigurationSection

        ' Create a configuration section.
        Public Sub New()
        End Sub

        <ConfigurationProperty("", IsRequired:=False, IsKey:=False, IsDefaultCollection:=True)> _
        Public Property Items() As EngineInformationItemCollection
            Get
                If MyBase.Item("items") Is Nothing Then
                    Return DirectCast(MyBase.Item(""), EngineInformationItemCollection)
                Else
                    Return DirectCast(MyBase.Item("items"), EngineInformationItemCollection)
                End If

            End Get
            Set(value As EngineInformationItemCollection)
                MyBase.Item("items") = value
            End Set
        End Property
    End Class

    <ConfigurationCollection(GetType(EngineInformationItem), CollectionType:=ConfigurationElementCollectionType.BasicMapAlternate)> _
    Public Class EngineInformationItemCollection
        Inherits ConfigurationElementCollection

        Friend Const ItemPropertyName As String = "engineInformation"

        Public Overrides ReadOnly Property CollectionType() As ConfigurationElementCollectionType
            Get
                Return ConfigurationElementCollectionType.BasicMapAlternate
            End Get
        End Property

        Protected Overrides ReadOnly Property ElementName() As String
            Get
                Return ItemPropertyName
            End Get
        End Property

        Protected Overrides Function IsElementName(elementName As String) As Boolean
            Return (elementName = ItemPropertyName)
        End Function

        Protected Overrides Function GetElementKey(element As ConfigurationElement) As Object
            Return DirectCast(element, EngineInformationItem).EngineModel
        End Function

        Protected Overrides Function CreateNewElement() As ConfigurationElement
            Return New EngineInformationItem()
        End Function

        Public Overrides Function IsReadOnly() As Boolean
            Return False
        End Function
        Public Function ContainsKey(key As String) As Boolean 'ft
            Dim result As Boolean = False
            Dim keys As Object() = BaseGetAllKeys()
            For Each obj As Object In keys
                If DirectCast(obj, String) = key Then
                    result = True
                    Exit For
                End If
            Next
            Return result
        End Function
        Default Public Shadows Property Item(index As Integer) As EngineInformationItem 'ft ConfigurationElement
            Get
                Return DirectCast(BaseGet(index), EngineInformationItem) 'ConfigurationElement
            End Get
            Set(value As EngineInformationItem) 'ConfigurationElement
                If BaseGet(index) IsNot Nothing Then
                    BaseRemoveAt(index)
                End If
                BaseAdd(index, value)
            End Set
        End Property
        Default Public Shadows ReadOnly Property Item(EngineModel As String) As EngineInformationItem 'ft ConfigurationElement
            Get
                Return DirectCast(BaseGet(EngineModel), EngineInformationItem) 'ConfigurationElement
            End Get
        End Property
        Public Sub Add(ByVal item As EngineInformationItem)
            BaseAdd(item)
        End Sub
    End Class

    Public Class EngineInformationItem
        Inherits System.Configuration.ConfigurationElement

        Public Enum eLength
            Inch
            Centimeter
        End Enum

        Public Enum eTapArrangement
            Corner
            DandD_2
            Flange
        End Enum

        Public Enum eArincConfiguration
            None
            Omnibusbox
            HoneywellComputer
        End Enum
        Public Enum eDressKit 'Used for APS3200 and GTCP331-9A
            NotApplicable
            Pratt
            HamiltonSundstrand
            Honeywell
        End Enum

        <ConfigurationProperty("PLALeverConfig", DefaultValue:="", IsRequired:=False)> _
        Public Property PLALeverConfig() As String
            Get
                Return DirectCast(Me("PLALeverConfig"), String)
            End Get
            Set(ByVal value As String)
                Me("PLALeverConfig") = value
            End Set
        End Property
        <ConfigurationProperty("PLAMinRange", DefaultValue:="", IsRequired:=False)> _
        Public Property PLAMinRange() As String
            Get
                Return DirectCast(Me("PLAMinRange"), String)
            End Get
            Set(ByVal value As String)
                Me("PLAMinRange") = value
            End Set
        End Property
        <ConfigurationProperty("PLAMaxRange", DefaultValue:="", IsRequired:=False)> _
        Public Property PLAMaxRange() As String
            Get
                Return DirectCast(Me("PLAMaxRange"), String)
            End Get
            Set(ByVal value As String)
                Me("PLAMaxRange") = value
            End Set
        End Property
        <ConfigurationProperty("CLPLeverConfig", DefaultValue:="", IsRequired:=False)> _
        Public Property CLPLeverConfig() As String
            Get
                Return DirectCast(Me("CLPLeverConfig"), String)
            End Get
            Set(ByVal value As String)
                Me("CLPLeverConfig") = value
            End Set
        End Property
        <ConfigurationProperty("CLPMinRange", DefaultValue:="", IsRequired:=False)> _
        Public Property CLPMinRange() As String
            Get
                Return DirectCast(Me("CLPMinRange"), String)
            End Get
            Set(ByVal value As String)
                Me("CLPMinRange") = value
            End Set
        End Property
        <ConfigurationProperty("CLPMaxRange", DefaultValue:="", IsRequired:=False)> _
        Public Property CLPMaxRange() As String
            Get
                Return DirectCast(Me("CLPMaxRange"), String)
            End Get
            Set(ByVal value As String)
                Me("CLPMaxRange") = value
            End Set
        End Property
        <ConfigurationProperty("LeverExcitation", DefaultValue:="", IsRequired:=False)> _
        Public Property LeverExcitation() As String
            Get
                Return DirectCast(Me("LeverExcitation"), String)
            End Get
            Set(ByVal value As String)
                Me("LeverExcitation") = value
            End Set
        End Property
        <ConfigurationProperty("engineModel", DefaultValue:="", IsRequired:=True)> _
        Public Property EngineModel() As String
            Get
                Return DirectCast(Me("engineModel"), String)
            End Get
            Set(ByVal value As String)
                Me("engineModel") = value
            End Set
        End Property
        <ConfigurationProperty("ductDiameter", DefaultValue:="7.98", IsRequired:=False)> _
        Public Property DuctDiameter() As Double
            Get
                Return DirectCast(Me("ductDiameter"), Double)
            End Get
            Set(ByVal value As Double)
                Me("ductDiameter") = value
            End Set
        End Property
        <ConfigurationProperty("ductDiameterUnit", DefaultValue:="Inch", IsRequired:=False)> _
        Public Property DuctDiameterUnit() As eLength
            Get
                Return DirectCast(Me("ductDiameterUnit"), eLength)
            End Get
            Set(ByVal value As eLength)
                Me("ductDiameterUnit") = value
            End Set
        End Property
        <ConfigurationProperty("orificeDiameter", DefaultValue:="3.245", IsRequired:=False)> _
        Public Property OrificeDiameter() As Double
            Get
                Return DirectCast(Me("orificeDiameter"), Double)
            End Get
            Set(ByVal value As Double)
                Me("orificeDiameter") = value
            End Set
        End Property
        <ConfigurationProperty("orificeDiameterUnit", DefaultValue:="Inch", IsRequired:=False)> _
        Public Property OrificeDiameterUnit() As eLength
            Get
                Return DirectCast(Me("orificeDiameterUnit"), eLength)
            End Get
            Set(ByVal value As eLength)
                Me("orificeDiameterUnit") = value
            End Set
        End Property
        <ConfigurationProperty("orificeThickness", DefaultValue:="0.125", IsRequired:=False)> _
        Public Property OrificeThinkness() As Double
            Get
                Return DirectCast(Me("orificeThickness"), Double)
            End Get
            Set(value As Double)
                Me("orificeThinkness") = value
            End Set
        End Property
        <ConfigurationProperty("orificeThicknessUnit", DefaultValue:="Inch", IsRequired:=False)> _
        Public Property OrificeThinknessUnit() As eLength
            Get
                Return DirectCast(Me("orificeThicknessUnit"), eLength)
            End Get
            Set(value As eLength)
                Me("orificeThinknessUnit") = value
            End Set
        End Property
        <ConfigurationProperty("tapArrangement", DefaultValue:="DandD_2", IsRequired:=False)>
        Public Property TapArrangement() As eTapArrangement
            Get
                Return DirectCast(Me("tapArrangement"), eTapArrangement)
            End Get
            Set(ByVal value As eTapArrangement)
                Me("tapArrangement") = value
            End Set
        End Property

        <ConfigurationProperty("useMeasuredBleedFlowValue", DefaultValue:="false", IsRequired:=False)>
        Public Property UseMeasuredBleedFlowValue() As Boolean
            Get
                Return DirectCast(Me("useMeasuredBleedFlowValue"), Boolean)
            End Get
            Set(ByVal value As Boolean)
                Me("useMeasuredBleedFlowValue") = value
            End Set
        End Property



        'Not required for all engine
        <ConfigurationProperty("performanceReportTemplateFile", DefaultValue:="", IsRequired:=False)> _
        Public Property PerformanceReportTemplateFile() As String
            Get
                Return DirectCast(Me("performanceReportTemplateFile"), String)
            End Get
            Set(ByVal value As String)
                Me("performanceReportTemplateFile") = value
            End Set
        End Property

        'Not required for all engine
        <ConfigurationProperty("performanceReportType", DefaultValue:="Repair", IsRequired:=False)> _
        Public Property PerformanceReportType() As String
            Get
                Return DirectCast(Me("performanceReportType"), String)
            End Get
            Set(ByVal value As String)
                Me("performanceReportType") = value
            End Set
        End Property
        'Not required for all engine
        <ConfigurationProperty("performanceReportAlternateTemplateFile", DefaultValue:="", IsRequired:=False)> _
        Public Property PerformanceReportAlternateTemplateFile() As String
            Get
                Return DirectCast(Me("performanceReportAlternateTemplateFile"), String)
            End Get
            Set(ByVal value As String)
                Me("performanceReportAlternateTemplateFile") = value
            End Set
        End Property

        'Not required for all engine
        <ConfigurationProperty("performanceAndLogAveragingPeriod", DefaultValue:="5", IsRequired:=False)> _
        Public Property PerformanceAndLogAveragingPeriod() As Integer
            Get
                Return DirectCast(Me("performanceAndLogAveragingPeriod"), Integer)
            End Get
            Set(ByVal value As Integer)
                Me("performanceAndLogAveragingPeriod") = value
            End Set
        End Property

        'Hamilton Sundstrand SmartTerminal Utility'  FOR APS3200 there is more than one version of SmartTerminal ECB V5 or ECB V6
        <ConfigurationProperty("smartTerminalUtilityExeFile1", DefaultValue:="", IsRequired:=False)> _
        Public Property SmartTerminalUtilityExeFile1() As String
            Get
                Return DirectCast(Me("smartTerminalUtilityExeFile1"), String)
            End Get
            Set(ByVal value As String)
                Me("smartTerminalUtilityExeFile1") = value
            End Set
        End Property
        <ConfigurationProperty("smartTerminalUtilityExeFile2", DefaultValue:="", IsRequired:=False)> _
        Public Property SmartTerminalUtilityExeFile2() As String
            Get
                Return DirectCast(Me("smartTerminalUtilityExeFile2"), String)
            End Get
            Set(ByVal value As String)
                Me("smartTerminalUtilityExeFile2") = value
            End Set
        End Property
        'Pratt Dresskit
        <ConfigurationProperty("dressKit", DefaultValue:="NotApplicable", IsRequired:=False)> _
        Public Property Dresskit() As eDressKit
            Get
                Return DirectCast(Me("dressKit"), eDressKit)
            End Get
            Set(ByVal value As eDressKit)
                Me("dressKit") = value
            End Set
        End Property
        'CEC Vibration system engine configuration file
        <ConfigurationProperty("lauchCECVibrationSystemApplicationWhenOpeningSession", DefaultValue:="false", IsRequired:=False)> _
        Public Property LauchCECVibrationSystemApplicationWhenOpeningSession() As Boolean
            Get
                Return DirectCast(Me("lauchCECVibrationSystemApplicationWhenOpeningSession"), Boolean)
            End Get
            Set(ByVal value As Boolean)
                Me("lauchCECVibrationSystemApplicationWhenOpeningSession") = value
            End Set
        End Property
        'CEC Vibration system engine configuration file
        <ConfigurationProperty("cecVibrationSystemDefaultEngineConfigFile", DefaultValue:="", IsRequired:=False)> _
        Public Property CECVibrationSystemDefaultEngineConfigFile() As String
            Get
                Return DirectCast(Me("cecVibrationSystemDefaultEngineConfigFile"), String)
            End Get
            Set(ByVal value As String)
                Me("cecVibrationSystemDefaultEngineConfigFile") = value
            End Set
        End Property

        <ConfigurationProperty("WinDMMUtilityExeFile", DefaultValue:="C:\Honeywell\WinDMM\windmm.exe", IsRequired:=False)> _
        Public Property WinDMMUtilityExeFile() As String
            Get
                Return DirectCast(Me("WinDMMUtilityExeFile"), String)
            End Get
            Set(ByVal value As String)
                Me("WinDMMUtilityExeFile") = value
            End Set
        End Property

        'Enables PW&C Server for DLL Processing
        <ConfigurationProperty("PwcServerDllProcessingEnabled", DefaultValue:="False", IsRequired:=False)> _
        Public Property PwcServerDllProcessingEnabled() As Boolean
            Get
                Return DirectCast(Me("PwcServerDllProcessingEnabled"), Boolean)
            End Get
            Set(ByVal value As Boolean)
                Me("PwcServerDllProcessingEnabled") = value
            End Set
        End Property

        'Enables PW&C Server Console Visible for DLL Processing
        <ConfigurationProperty("PwcServerDllProcessingVisible", DefaultValue:="False", IsRequired:=False)> _
        Public Property PwcServerDllProcessingVisible() As Boolean
            Get
                Return DirectCast(Me("PwcServerDllProcessingVisible"), Boolean)
            End Get
            Set(ByVal value As Boolean)
                Me("PwcServerDllProcessingVisible") = value
            End Set
        End Property

        'EXE Application Argument for Pwc Server Dll Processing System Configuration
        <ConfigurationProperty("PwcServerDllProcessingSystemExeFileArgument", DefaultValue:="", IsRequired:=False)> _
        Public Property PwcServerDllProcessingSystemExeFileArgument() As String
            Get
                Return DirectCast(Me("PwcServerDllProcessingSystemExeFileArgument"), String)
            End Get
            Set(ByVal value As String)
                Me("PwcServerDllProcessingSystemExeFileArgument") = value
            End Set
        End Property

        <ConfigurationProperty("arincConfiguration", DefaultValue:="Omnibusbox", IsRequired:=True)> _
        Public Property ArincConfiguration() As eArincConfiguration
            Get
                Return DirectCast(Me("arincConfiguration"), eArincConfiguration)
            End Get
            Set(ByVal value As eArincConfiguration)
                Me("arincConfiguration") = value
            End Set
        End Property
        <ConfigurationProperty("customOpeningMessage", DefaultValue:="", IsRequired:=False)> _
        Public Property CustomOpeningMessage() As String
            Get
                Return DirectCast(Me("customOpeningMessage"), String)
            End Get
            Set(ByVal value As String)
                Me("customOpeningMessage") = value
            End Set
        End Property


    End Class

#End Region

#Region "Report Auxiliary Classes"

    Public Class ReportInformationElement
        Inherits System.Configuration.ConfigurationElement

        ' Create the element.
        Public Sub New()
        End Sub

        ' Create the element.
        Public Sub New(ByVal calibrationReportOutputDirectory As String, _
                       ByVal calibrationReportTemplateFile As String, _
                       ByVal checkReportOutputDirectory As String, _
                       ByVal checkReportTemplateFile As String, _
                       ByVal logReportTemplateFile As String, _
                       ByVal logReportTemplateCustomerLogo As String, _
                       ByVal testProgressFullFileName As String)
            Me.CalibrationReportOutputDirectory = calibrationReportOutputDirectory
            Me.CalibrationReportTemplateFile = calibrationReportTemplateFile
            Me.CheckReportOutputDirectory = checkReportOutputDirectory
            Me.CheckReportTemplateFile = checkReportTemplateFile
            Me.LogReportTemplateFile = logReportTemplateFile
            Me.LogReportTemplateCustomerLogo = logReportTemplateCustomerLogo
            Me.TestProgressFullFileName = testProgressFullFileName
        End Sub


        <ConfigurationProperty("calibrationReportOutputDirectory", DefaultValue:="C:\Calibration Reports\", IsRequired:=True)> _
        Public Property CalibrationReportOutputDirectory() As String
            Get
                Return DirectCast(Me("calibrationReportOutputDirectory"), String)
            End Get
            Set(ByVal value As String)
                Me("calibrationReportOutputDirectory") = value
            End Set
        End Property
        <ConfigurationProperty("calibrationReportTemplateFile", DefaultValue:="Report_Calibration.xlsx", IsRequired:=True)> _
        Public Property CalibrationReportTemplateFile() As String
            Get
                Return DirectCast(Me("calibrationReportTemplateFile"), String)
            End Get
            Set(ByVal value As String)
                Me("calibrationReportTemplateFile") = value
            End Set
        End Property
        <ConfigurationProperty("checkReportOutputDirectory", DefaultValue:="C:\Check Reports\", IsRequired:=True)> _
        Public Property CheckReportOutputDirectory() As String
            Get
                Return DirectCast(Me("checkReportOutputDirectory"), String)
            End Get
            Set(ByVal value As String)
                Me("checkReportOutputDirectory") = value
            End Set
        End Property
        <ConfigurationProperty("checkReportTemplateFile", DefaultValue:="Report_Check.xlsx", IsRequired:=True)> _
        Public Property CheckReportTemplateFile() As String
            Get
                Return DirectCast(Me("checkReportTemplateFile"), String)
            End Get
            Set(ByVal value As String)
                Me("checkReportTemplateFile") = value
            End Set
        End Property
        <ConfigurationProperty("logReportTemplateFile", DefaultValue:="Log_Report.xlsx", IsRequired:=True)> _
        Public Property LogReportTemplateFile() As String
            Get
                Return DirectCast(Me("logReportTemplateFile"), String)
            End Get
            Set(ByVal value As String)
                Me("logReportTemplateFile") = value
            End Set
        End Property
        <ConfigurationProperty("logReportTemplateCustomerLogo", DefaultValue:="C:\My Projects\CELDAS 200\HMI\Images\Customer Logo.JPG", IsRequired:=True)> _
        Public Property LogReportTemplateCustomerLogo() As String
            Get
                Return DirectCast(Me("logReportTemplateCustomerLogo"), String)
            End Get
            Set(ByVal value As String)
                Me("logReportTemplateCustomerLogo") = value
            End Set
        End Property
        <ConfigurationProperty("testProgressFullFileName", DefaultValue:="C:\My Projects\CELDAS 200\HMI\TestProgressSheet.xsl", IsRequired:=False)> _
        Public Property TestProgressFullFileName() As String
            Get
                Return DirectCast(Me("testProgressFullFileName"), String)
            End Get
            Set(ByVal value As String)
                Me("testProgressFullFileName") = value
            End Set
        End Property

    End Class


    ' Define a custom section that is used by the application
    ' to create custom configuration sections at the specified 
    ' level in the configuration hierarchy.
    ' This enables the the user that has the proper access 
    ' rights, to make changes to the configuration files.
    Public Class ReportConfigurationSection
        Inherits ConfigurationSection
        ' Create a configuration section.
        Public Sub New()
        End Sub

        ' Set or get the ConsoleElement. 
        <ConfigurationProperty("reportInformation")> _
        Public Property ReportElement() As ReportInformationElement
            Get
                Return (DirectCast(Me("reportInformation"), ReportInformationElement))
            End Get
            Set(ByVal value As ReportInformationElement)
                Me("reportInformation") = value
            End Set
        End Property
    End Class
#End Region

#Region "Database Auxiliary Classes"
    Public Class DatabaseConfigElement
        Inherits System.Configuration.ConfigurationElement

        ' Create the element.
        Public Sub New()
        End Sub

        ' Create the element.
        Public Sub New(ByVal path As String, ByVal databaseFilename As String, ByVal workgroupFilename As String, ByVal computerName As String, ByVal computerLoginName As String)
            Me.Path = path
            Me.DatabaseFileName = databaseFilename
            Me.WorkGroupFileName = workgroupFilename
            Me.ComputerName = computerName
            Me.ComputerLoginName = computerLoginName

        End Sub
        ' Get or set the ComputerName 
        <ConfigurationProperty("computerName", DefaultValue:="CELDAS200-12917", IsRequired:=True)> _
        Public Property ComputerName() As String
            Get
                Return DirectCast(Me("computerName"), String)
            End Get
            Set(ByVal Value As String)
                Me("computerName") = Value
            End Set
        End Property
        <ConfigurationProperty("computerLoginName", DefaultValue:="CELDAS200", IsRequired:=True)> _
        Public Property ComputerLoginName() As String
            Get
                Return DirectCast(Me("computerLoginName"), String)
            End Get
            Set(ByVal Value As String)
                Me("computerLoginName") = Value
            End Set
        End Property
        ' Get or set the path 
        <ConfigurationProperty("path", DefaultValue:="C:\My Projects\CELDAS 200\Workbench\", IsRequired:=True)> _
        Public Property Path() As String
            Get
                Return DirectCast(Me("path"), String)
            End Get
            Set(ByVal Value As String)
                Me("path") = Value
            End Set
        End Property
        ' Get or set the filename
        <ConfigurationProperty("databaseFilename", DefaultValue:="TestSession.mdb", IsRequired:=True)> _
        Public Property DatabaseFileName() As String
            Get
                Return DirectCast(Me("databaseFilename"), String)
            End Get
            Set(ByVal Value As String)
                Me("databaseFilename") = Value
            End Set
        End Property
        Public ReadOnly Property FullFilePath() As String
            Get
                If Path.Substring(Path.Length - 1) = "\" Then
                    Return String.Format("{0}{1}", Path, DatabaseFileName)
                Else
                    Return String.Format("{0}\{1}", Path, DatabaseFileName)
                End If
            End Get
        End Property
        ' Get or set the filename
        <ConfigurationProperty("workGroupFilename", DefaultValue:="Secured.mdw", IsRequired:=True)> _
        Public Property WorkGroupFileName() As String
            Get
                Return DirectCast(Me("workGroupFilename"), String)
            End Get
            Set(ByVal Value As String)
                Me("workGroupFilename") = Value
            End Set
        End Property
        Public ReadOnly Property FullFileWorkgroup() As String
            Get
                If Path.Substring(Path.Length - 1) = "\" Then
                    Return String.Format("{0}{1}", Path, WorkGroupFileName)
                Else
                    Return String.Format("{0}\{1}", Path, WorkGroupFileName)
                End If
            End Get
        End Property
        <ConfigurationProperty("userId", DefaultValue:="Secured.mdw", IsRequired:=True)> _
        Public Property UserId() As String
            Get
                Return DirectCast(Me("userId"), String)
            End Get
            Set(ByVal Value As String)
                Me("userId") = Value
            End Set
        End Property
        Private _Password As String
        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(ByVal Value As String)
                _Password = Value
            End Set
        End Property
        Public ReadOnly Property OleDbConnectionString() As String
            Get
                Dim processHelper As New ProcessHelper()

                If processHelper.Is64BitProcess() Then
                    Return String.Format("Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0};Jet OLEDB:System database={1};User ID={2}; Password={3}", FullFilePath, FullFileWorkgroup, UserId, Password)
                Else
                    Return String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Jet OLEDB:System database={1};User ID={2}; Password={3}", FullFilePath, FullFileWorkgroup, UserId, Password)
                End If


            End Get
        End Property
        Private _connection As OleDbConnection
        Public ReadOnly Property Connection As OleDbConnection
            Get
                DatabaseHelper.OpenConnection(OleDbConnectionString, _connection)
                Return _connection
            End Get
        End Property

    End Class


    ' Define a custom section that is used by the application
    ' to create custom configuration sections at the specified 
    ' level in the configuration hierarchy.
    ' This enables the the user that has the proper access 
    ' rights, to make changes to the configuration files.
    Public Class DatabaseConfigurationSection
        Inherits ConfigurationSection
        ' Create a configuration section.
        Public Sub New()
        End Sub

        ' Set or get the ConsoleElement. 
        <ConfigurationProperty("databaseInformation")> _
        Public Property DatabaseElement() As DatabaseConfigElement
            Get
                Return (DirectCast(Me("databaseInformation"), DatabaseConfigElement))
            End Get
            Set(ByVal value As DatabaseConfigElement)
                Me("databaseInformation") = value
            End Set
        End Property
    End Class
#End Region

#Region "Test Manager Configuration Auxiliary Classes"
    Public Class TestManagerConfigurationSection
        Inherits ConfigurationSection

        <ConfigurationProperty("Enable")> _
        Public Property Enable() As TestManagerElement
            Get
                Return (DirectCast(Me("Enable"), TestManagerElement))
            End Get
            Set(ByVal value As TestManagerElement)
                Me("Enable") = value
            End Set
        End Property
    End Class

    Public Class TestManagerElement
        Inherits System.Configuration.ConfigurationElement

        <ConfigurationProperty("FlowmeterConfiguration", defaultValue:="True", IsRequired:=False)> _
        Public Property FlowmeterConfiguration() As String
            Get
                Return DirectCast(Me("FlowmeterConfiguration"), String)
            End Get
            Set(ByVal value As String)
                Me("FlowmeterConfiguration") = value
            End Set
        End Property

        <ConfigurationProperty("LoadCellAssignationEditor", defaultValue:="True", IsRequired:=False)> _
        Public Property LoadCellAssignationEditor() As String
            Get
                Return DirectCast(Me("LoadCellAssignationEditor"), String)
            End Get
            Set(ByVal value As String)
                Me("LoadCellAssignationEditor") = value
            End Set
        End Property

        <ConfigurationProperty("LogReportLayout", defaultValue:="True", IsRequired:=False)> _
        Public Property LogReportLayout() As String
            Get
                Return DirectCast(Me("LogReportLayout"), String)
            End Get
            Set(ByVal value As String)
                Me("LogReportLayout") = value
            End Set
        End Property

        <ConfigurationProperty("HmiNumericChannels", defaultValue:="True", IsRequired:=False)> _
        Public Property HmiNumericChannels() As String
            Get
                Return DirectCast(Me("HmiNumericChannels"), String)
            End Get
            Set(ByVal value As String)
                Me("HmiNumericChannels") = value
            End Set
        End Property

        <ConfigurationProperty("ArchiveReportLayout", defaultValue:="True", IsRequired:=False)> _
        Public Property ArchiveReportLayout() As String
            Get
                Return DirectCast(Me("ArchiveReportLayout"), String)
            End Get
            Set(ByVal value As String)
                Me("ArchiveReportLayout") = value
            End Set
        End Property

        <ConfigurationProperty("TestProgressStepEditor", defaultValue:="True", IsRequired:=False)> _
        Public Property TestProgressStepEditor() As String
            Get
                Return DirectCast(Me("TestProgressStepEditor"), String)
            End Get
            Set(ByVal value As String)
                Me("TestProgressStepEditor") = value
            End Set
        End Property

        <ConfigurationProperty("UserEditor", defaultValue:="True", IsRequired:=False)> _
        Public Property UserEditor() As String
            Get
                Return DirectCast(Me("UserEditor"), String)
            End Get
            Set(ByVal value As String)
                Me("UserEditor") = value
            End Set
        End Property

        <ConfigurationProperty("Maintenance", defaultValue:="True", IsRequired:=False)> _
        Public Property Maintenance() As String
            Get
                Return DirectCast(Me("Maintenance"), String)
            End Get
            Set(ByVal value As String)
                Me("Maintenance") = value
            End Set
        End Property

        <ConfigurationProperty("ChannelEditor", defaultValue:="True", IsRequired:=False)> _
        Public Property ChannelEditor() As String
            Get
                Return DirectCast(Me("ChannelEditor"), String)
            End Get
            Set(ByVal value As String)
                Me("ChannelEditor") = value
            End Set
        End Property

        <ConfigurationProperty("ConfigurationFileExport", defaultValue:="True", IsRequired:=False)> _
        Public Property ConfigurationFileExport() As String
            Get
                Return DirectCast(Me("ConfigurationFileExport"), String)
            End Get
            Set(ByVal value As String)
                Me("ConfigurationFileExport") = value
            End Set
        End Property

        <ConfigurationProperty("CSVFileExport", defaultValue:="True", IsRequired:=False)> _
        Public Property CSVFileExport() As String
            Get
                Return DirectCast(Me("CSVFileExport"), String)
            End Get
            Set(ByVal value As String)
                Me("CSVFileExport") = value
            End Set
        End Property

        <ConfigurationProperty("CSVFileUpload", defaultValue:="True", IsRequired:=False)> _
        Public Property CSVFileUpload() As String
            Get
                Return DirectCast(Me("CSVFileUpload"), String)
            End Get
            Set(ByVal value As String)
                Me("CSVFileUpload") = value
            End Set
        End Property

        <ConfigurationProperty("HardwareConfiguration", defaultValue:="True", IsRequired:=False)> _
        Public Property HardwareConfiguration() As String
            Get
                Return DirectCast(Me("HardwareConfiguration"), String)
            End Get
            Set(ByVal value As String)
                Me("HardwareConfiguration") = value
            End Set
        End Property

    End Class
#End Region

#Region "Auxiliary Classes"

    ' Define a custom configuration element to be 
    ' contained by the ConsoleSection. This element 
    ' stores background and foreground colors that
    ' the application applies to the console window.
    Public Class ConsoleConfigElement
        Inherits System.Configuration.ConfigurationElement
        ' Create the element.
        Public Sub New()
        End Sub

        ' Create the element.
        Public Sub New(ByVal fColor As ConsoleColor, ByVal bColor As ConsoleColor)
            ForegroundColor = fColor
            BackgroundColor = bColor
        End Sub

        ' Get or set the console background color.
        <ConfigurationProperty("background", DefaultValue:=ConsoleColor.Black, IsRequired:=False)> _
        Public Property BackgroundColor() As ConsoleColor
            Get
                Return DirectCast(Me("background"), ConsoleColor)
            End Get
            Set(ByVal value As ConsoleColor)
                Me("background") = value
            End Set
        End Property

        ' Get or set the console foreground color.
        <ConfigurationProperty("foreground", DefaultValue:=ConsoleColor.White, IsRequired:=False)> _
        Public Property ForegroundColor() As ConsoleColor
            Get
                Return DirectCast(Me("foreground"),  _
                  ConsoleColor)
            End Get
            Set(ByVal value As ConsoleColor)
                Me("foreground") = value
            End Set
        End Property
    End Class

    ' Define a custom section that is used by the application
    ' to create custom configuration sections at the specified 
    ' level in the configuration hierarchy.
    ' This enables the the user that has the proper access 
    ' rights, to make changes to the configuration files.
    Public Class ConsoleSection
        Inherits ConfigurationSection
        ' Create a configuration section.
        Public Sub New()
        End Sub

        ' Set or get the ConsoleElement. 
        <ConfigurationProperty("consoleElement")> _
        Public Property ConsoleElement() As ConsoleConfigElement
            Get
                Return (DirectCast(Me("consoleElement"), ConsoleConfigElement))
            End Get
            Set(ByVal value As ConsoleConfigElement)
                Me("consoleElement") = value
            End Set
        End Property
    End Class
#End Region

#Region "ConfigurationManager Interaction Class"

    ' The following code uses the ConfigurationManager class to 
    ' perform the following tasks:
    ' 1) Get the application roaming configuration file.
    ' 2) Get the application configuration file.
    ' 3) Access a specified configuration file through mapping.
    ' 4) Access the machine configuration file through mapping. 
    ' 5) Read a specified configuration section.
    ' 6) Read the connectionStrings section.
    ' 7) Read or write the appSettings section.
    Public Class UsingConfigurationManager
        ' Get the roaming configuration file associated 
        ' with the application.
        ' Note.This function uses the OpenExeConfiguration(
        ' ConfigurationUserLevel userLevel) method to 
        ' get the configuration file.
        Public Shared Sub GetRoamingConfiguration()
            ' Define the custom section to add to the
            ' configuration file.
            Dim sectionName As String = "consoleSection"
            Dim cSection As ConsoleSection = Nothing

            ' Get the roaming configuration that applies to the current user.
            Dim roamingConfig As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming)

            ' Map the roaming configuration file. This
            ' enables the application to access 
            ' the configuration file using the
            ' System.Configuration.Configuration class
            Dim configFileMap As New ExeConfigurationFileMap()
            configFileMap.ExeConfigFilename = roamingConfig.FilePath

            ' Get the mapped configuration file.
            Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None)

            Try
                cSection = DirectCast(config.GetSection(sectionName), ConsoleSection)

                If cSection Is Nothing Then
                    ' Create a custom configuration section.
                    cSection = New ConsoleSection()

                    ' Define where in the configuration file 
                    ' hierarchy the associated 
                    ' configuration section can be declared.
                    cSection.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser

                    ' Allow the configuration section to be 
                    ' overridden by other configuration files.
                    cSection.SectionInformation.AllowOverride = True

                    ' Force the section to be saved.
                    cSection.SectionInformation.ForceSave = True

                    ' Store console settings for roaming users.
                    cSection.ConsoleElement.BackgroundColor = ConsoleColor.Blue
                    cSection.ConsoleElement.ForegroundColor = ConsoleColor.Yellow

                    ' Add configuration information to 
                    ' the configuration file.
                    config.Sections.Add(sectionName, cSection)
                    config.Save(ConfigurationSaveMode.Modified)
                    ' Force a reload of the changed section. This 
                    ' makes the new values available for reading.
                    ConfigurationManager.RefreshSection(sectionName)
                End If
            Catch e As ConfigurationErrorsException
                Console.WriteLine("[Exception error: {0}]", e.ToString())
            End Try

            ' Set console properties using values
            ' stored in the configuration file.
            Console.BackgroundColor = cSection.ConsoleElement.BackgroundColor
            Console.ForegroundColor = cSection.ConsoleElement.ForegroundColor
            ' Apply the changes.
            'Console.Clear()

            ' Display feedback.
            Console.WriteLine()
            Console.WriteLine("Performed the following tasks:")
            Console.WriteLine("Created roaming configuration file {0}", config.FilePath)
            Console.WriteLine("Created custom ConsoleSection.")
            Console.WriteLine("Stored background (blue) and foreground (yellow) colors.")
            Console.WriteLine("Added the section to the configuration file.")
            Console.WriteLine("Read stored colors from the configuration file.")
            Console.WriteLine("Applied the colors to the console window.")
            Console.WriteLine()

        End Sub



        Public Shared databaseInfoSection As New DatabaseConfigurationSection
        Public Shared reportInfoSection As New ReportConfigurationSection
        Public Shared engineInfoSection As New EngineConfigurationSection
        Public Shared engineInfoCollection As EngineInformationItemCollection
        Public Shared projectInfoSection As New ProjectConfigurationSection

        Public Shared ExportConfigData As New ExportConfigurationSection
        Public Shared ExportFormatSection As New ExportFormatConfigurationSection
        Public Shared ExportFormatCollection As New ExportFormatInformationItemCollection
        Public Shared QnxConfigData As New QnxConfigurationSection
        Public Shared UnitronConfigData As New UnitronConfigurationSection
        Public Shared HobartConfigData As New HobartConfigurationSection
        Public Shared cRIOConfigData As New cRIOConfigurationSection
        Public Shared TestManagerConfigData As New TestManagerConfigurationSection

        ' Get the application configuration file.
        ' Note. This function uses the OpenExeConfiguration(string)method to get the application configuration file. 
        Public Shared Sub GetAppConfiguration(ByVal ConfigFileName As String)
            ' Get the application path needed to obtain the application configuration file.
            Dim applicationName As String = Environment.GetCommandLineArgs()(0) 'ft + ".exe"
            Dim config As System.Configuration.Configuration = Nothing


            'ConfigFileName = "12958/config"
            'ft Load un fichier de config specifique a un projet exemple Prj12917.exe.config lorsque le no de projet est passe en argument
            If ConfigFileName <> "" Then
                Try
                    Dim configFullFileName As String = System.IO.Path.Combine(Environment.CurrentDirectory, ConfigFileName)

                    If Not File.Exists(configFullFileName) Then
                        clsTopMostMessageBox.Show("Loading configuration error for config file " + ConfigFileName, "Configuration File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    ' To map the selected configuration file.
                    Dim configFileMap As New ExeConfigurationFileMap()
                    configFileMap.ExeConfigFilename = configFullFileName
                    config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else

                Dim exePath As String = System.IO.Path.Combine(Environment.CurrentDirectory, applicationName)
                ' Get the configuration file. The file name has this format appname.exe.config.
                config = ConfigurationManager.OpenExeConfiguration(exePath)

            End If




            Try
                ' Create a custom configuration section
                ' having the same name used in the 
                ' roaming configuration file.
                ' This is possible because the configuration 
                ' section can be overridden by other 
                ' configuration files. 
                Dim sectionName As String = "consoleSection"
                'Dim customSection As New ConsoleSection()

                'If config.Sections(sectionName) Is Nothing Then
                '    ' Store console settings.
                '    customSection.ConsoleElement.BackgroundColor = ConsoleColor.Black
                '    customSection.ConsoleElement.ForegroundColor = ConsoleColor.White

                '    ' Add configuration information to the
                '    ' configuration file.
                '    config.Sections.Add(sectionName, customSection)
                '    config.Save(ConfigurationSaveMode.Modified)
                '    ' Force a reload of the changed section.
                '    ' This makes the new values available for reading.
                '    ConfigurationManager.RefreshSection(sectionName)
                'End If
                '' Set console properties using values
                '' stored in the configuration file.
                'customSection = DirectCast(config.GetSection(sectionName), ConsoleSection)
                'Console.BackgroundColor = customSection.ConsoleElement.BackgroundColor
                'Console.ForegroundColor = customSection.ConsoleElement.ForegroundColor
                '' Apply the changes.
                ''Console.Clear()


                Try
                    Console.WriteLine("Loading configuration: Project Information Section")
                    sectionName = "projectConfigurationSection"
                    If config.Sections(sectionName) Is Nothing Then
                        clsTopMostMessageBox.Show("ConfigurationSettings.GetAppConfiguration: Unable to load config file ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        projectInfoSection.ProjectElement.Name = "CEL Master Project"
                        projectInfoSection.ProjectElement.Number = -1
                        config.Sections.Add(sectionName, projectInfoSection)
                        config.Save(ConfigurationSaveMode.Modified)
                        ' Force a reload of the changed section. This makes the new values available for reading.
                        ConfigurationManager.RefreshSection(sectionName)
                    End If
                    ' Load properties using values stored in the configuration file.
                    projectInfoSection = DirectCast(config.GetSection(sectionName), ProjectConfigurationSection)
                Catch ex As System.Configuration.ConfigurationErrorsException
                    clsTopMostMessageBox.Show(ex.BareMessage + vbLf + vbLf + "Make sure it's present in the *.exe.config file in CustomPrj/Config/  under the projectInformation section", "Loading configuration error for ProjectInformationSection", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for ProjectInformationSection", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try




                Try
                    Console.WriteLine("Loading configuration: Database Information Section")
                    sectionName = "databaseConfigurationSection"
                    If config.Sections(sectionName) Is Nothing Then
                        databaseInfoSection.DatabaseElement.Path = "C:\My Projects\CELDAS 200\Workbench\"
                        databaseInfoSection.DatabaseElement.DatabaseFileName = "TestSession.mdb"
                        databaseInfoSection.DatabaseElement.WorkGroupFileName = "Secured.mdw"
                        databaseInfoSection.DatabaseElement.UserId = "Administrator"
                        config.Sections.Add(sectionName, databaseInfoSection)
                        config.Save(ConfigurationSaveMode.Modified)
                        ' Force a reload of the changed section. This makes the new values available for reading.
                        ConfigurationManager.RefreshSection(sectionName)
                        databaseInfoSection.DatabaseElement.Password = "CEL9994"
                    End If
                    ' Load properties using values stored in the configuration file.
                    databaseInfoSection = DirectCast(config.GetSection(sectionName), DatabaseConfigurationSection)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for DatabaseInformationSection", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try




                Try
                    Console.WriteLine("Loading configuration: Report Information Section")
                    sectionName = "reportConfigurationSection"
                    If config.Sections(sectionName) Is Nothing Then
                        reportInfoSection.ReportElement.CalibrationReportOutputDirectory = "C:\Calibration Reports\"
                        reportInfoSection.ReportElement.CalibrationReportTemplateFile = "Report_Calibration.xlsx"
                        reportInfoSection.ReportElement.CheckReportOutputDirectory = "C:\Check Reports\"
                        reportInfoSection.ReportElement.CheckReportTemplateFile = "Report_Check.xlsx"
                        config.Sections.Add(sectionName, reportInfoSection)
                        config.Save(ConfigurationSaveMode.Modified)
                        ' Force a reload of the changed section. This makes the new values available for reading.
                        ConfigurationManager.RefreshSection(sectionName)
                    End If
                    ' Load properties using values stored in the configuration file.
                    reportInfoSection = DirectCast(config.GetSection(sectionName), ReportConfigurationSection)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for ReportConfigurationSection", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try




                Try
                    Console.WriteLine("Loading configuration: Engines Information Section")
                    'sectionName = "engineInformationElements"
                    sectionName = "engineConfigurationSection"
                    If config.Sections(sectionName) Is Nothing Then
                        Dim engineTemplateInfo As New EngineInformationItem
                        engineTemplateInfo.EngineModel = "SampleEngineModel"
                        engineTemplateInfo.DuctDiameter = 7.98
                        engineTemplateInfo.DuctDiameterUnit = EngineInformationItem.eLength.Inch
                        engineTemplateInfo.OrificeDiameter = 3.245
                        engineTemplateInfo.PerformanceAndLogAveragingPeriod = 5
                        engineTemplateInfo.OrificeDiameterUnit = EngineInformationItem.eLength.Inch
                        engineTemplateInfo.ArincConfiguration = EngineInformationItem.eArincConfiguration.Omnibusbox
                        engineInfoSection.Items.Add(engineTemplateInfo)
                        config.Sections.Add(sectionName, engineInfoSection)
                        config.Save(ConfigurationSaveMode.Modified)
                        ' Force a reload of the changed section. This makes the new values available for reading.
                        ConfigurationManager.RefreshSection(sectionName)

                    End If
                    Dim section As EngineConfigurationSection = DirectCast(config.GetSection(sectionName), EngineConfigurationSection)
                    engineInfoCollection = section.Items
                    'Dim engineInfo As EngineInformationItem
                    'engineInfo = engineInfoCollection.Item("GTCP131-9B")
                    'Dim test As String = engineInfo.EngineModel
                    'Load properties using values stored in the configuration file.
                    engineInfoSection = DirectCast(config.GetSection(sectionName), EngineConfigurationSection)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for EngineConfigurationSection", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try



                'config format export
                Try
                    Console.WriteLine("Loading Configuration: Export Configuration Section")
                    sectionName = "exportConfigurationSection"
                    If config.Sections(sectionName) Is Nothing Then
                        Console.WriteLine("Failed to load the Export Configuration Section")
                        Dim exportconfigtemplate As New ExportInformationElement
                        exportconfigtemplate.ExportType = 0
                        exportconfigtemplate.ExportPathName = "C:\My Projects\12958\CustomPrj\Csv\"
                        config.Sections.Add(sectionName, ExportConfigData)
                        config.Save(ConfigurationSaveMode.Modified)
                        ' Force a reload of the changed section. This makes the new values available for reading.
                        ConfigurationManager.RefreshSection(sectionName)
                    End If

                    'Load properties using values stored in the configuration file.
                    ExportConfigData = DirectCast(config.GetSection(sectionName), ExportConfigurationSection)

                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                Try
                    Console.WriteLine("Loading Configuration: Export Format Configuration Section")
                    sectionName = "exportFormatConfigurationSection"
                    If config.Sections(sectionName) Is Nothing Then
                        Console.WriteLine("Failed to load the export format Configuration Section")
                        Dim formatConfigTemplate As New ExportFormatInformationItem
                        formatConfigTemplate.FileName = "Warning_Config_Error_-_Place-Holder_DACN1_BOOL_SERIAL_CSV.csv"
                        formatConfigTemplate.TableName = "DACN1_SERIAL_IOMGR_BOOL"
                        formatConfigTemplate.ColumnNameHeader = "False"
                        formatConfigTemplate.DecimalSymbol = "."
                        formatConfigTemplate.NumberDigits = "10"
                        formatConfigTemplate.TextDelimiter = "none"
                        ExportFormatSection.Items.Add(formatConfigTemplate)
                        config.Sections.Add(sectionName, ExportFormatSection)
                        config.Save(ConfigurationSaveMode.Modified)
                        ' Force a reload of the changed section. This makes the new values available for reading.
                        ConfigurationManager.RefreshSection(sectionName)
                    End If
                    Dim section As ExportFormatConfigurationSection = DirectCast(config.GetSection(sectionName), ExportFormatConfigurationSection)
                    ExportFormatCollection = section.Items
                    ExportFormatSection = DirectCast(config.GetSection(sectionName), ExportFormatConfigurationSection)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try



                'Qnx Config
                Try
                    Console.WriteLine("Loading Configuration: QnxConfig Section")
                    sectionName = "qnxConfigurationSection"
                    If config.Sections(sectionName) Is Nothing Then
                        Console.WriteLine("Failed to load the QNX Configuration Section")
                        Dim qnxConfigTemplate As New QnxInformationElement
                        qnxConfigTemplate.IpAdress = "192.168.2.200"
                        qnxConfigTemplate.UploadPath = "./app"
                        qnxConfigTemplate.UserName = "Virgo2000"
                        qnxConfigTemplate.Password = "v2000"
                        config.Sections.Add(sectionName, QnxConfigData)
                        config.Save(ConfigurationSaveMode.Modified)
                        ' Force a reload of the changed section. This makes the new values available for reading.
                        ConfigurationManager.RefreshSection(sectionName)
                    End If
                    QnxConfigData = DirectCast(config.GetSection(sectionName), QnxConfigurationSection)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'Unitron Config
                Try
                    Console.WriteLine("Loading Configuration: UnitronConfig Section")
                    sectionName = "UnitronConfigurationSection"
                    If config.Sections(sectionName) Is Nothing Then
                        Console.WriteLine("Failed to load the Unitron Configuration Section")
                        Dim UnitronConfigTemplate As New UnitronInformationElement
                        UnitronConfigTemplate.IpAdress = "192.168.2.250"
                        UnitronConfigTemplate.DefaultCurrentLimit = 1200
                        UnitronConfigTemplate.PW100OilPriming = 300
                        UnitronConfigTemplate.PW100Motoring = 600
                        UnitronConfigTemplate.PW100Cranking = 1150
                        config.Sections.Add(sectionName, UnitronConfigData)
                        config.Save(ConfigurationSaveMode.Modified)
                        ' Force a reload of the changed section. This makes the new values available for reading.
                        ConfigurationManager.RefreshSection(sectionName)
                    End If
                    UnitronConfigData = DirectCast(config.GetSection(sectionName), UnitronConfigurationSection)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'Hobart Config
                Try
                    Console.WriteLine("Loading Configuration: HobartConfig Section")
                    sectionName = "HobartConfigurationSection"
                    If config.Sections(sectionName) Is Nothing Then
                        Console.WriteLine("Failed to load the Hobart Configuration Section")
                        Dim HobartConfigTemplate As New HobartInformationElement
                        HobartConfigTemplate.IpAdress = "192.168.2.250"
                        HobartConfigTemplate.DefaultCurrentLimit = 1200
                        config.Sections.Add(sectionName, HobartConfigData)
                        config.Save(ConfigurationSaveMode.Modified)
                        ' Force a reload of the changed section. This makes the new values available for reading.
                        ConfigurationManager.RefreshSection(sectionName)
                    End If
                    HobartConfigData = DirectCast(config.GetSection(sectionName), HobartConfigurationSection)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'Test Manager Configuration
                Try
                    Console.WriteLine("Loading Configuration: Test Manager Configuration Section")
                    sectionName = "testmanagerConfigurationSection"
                    If config.Sections(sectionName) Is Nothing Then
                        Console.WriteLine("Failed to load the Test Manager Configuration Section")
                        Dim testmanagerConfigTemplate As New TestManagerElement
                        testmanagerConfigTemplate.FlowmeterConfiguration = "True"
                        config.Sections.Add(sectionName, TestManagerConfigData)
                        config.Save(ConfigurationSaveMode.Modified)
                        ' Force a reload of the changed section. This makes the new values available for reading.
                        ConfigurationManager.RefreshSection(sectionName)
                    End If
                    TestManagerConfigData = DirectCast(config.GetSection(sectionName), TestManagerConfigurationSection)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Catch e As ConfigurationErrorsException
                Console.WriteLine("[Error exception: {0}]", e.ToString())
            End Try
        End Sub


        'These will hold the Configuration for exporting.

        'get the configuration export file.
        'Access and load the configfile.
        Public Shared Sub LoadExportsConfigFile(ByVal ConfigFileName As String)

            ' Get the application path needed to obtain the application configuration file.
            Dim applicationName As String = Environment.GetCommandLineArgs()(0) 'ft + ".exe"
            Dim config As System.Configuration.Configuration = Nothing
            Dim sectionName As String

            If ConfigFileName <> "" Then
                Try
                    Dim configFullFileName As String = System.IO.Path.Combine(Environment.CurrentDirectory, ConfigFileName)
                    'configFullFileName = "C:\My Projects\12958\CustomPrj\Config\Prj12958_HMI.exe.config" ''force config location atm. To be modified

                    If Not File.Exists(configFullFileName) Then
                        clsTopMostMessageBox.Show("Loading configuration error for config file " + ConfigFileName, "Configuration File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    'Retrieve the config File
                    Dim configFileMap As New ExeConfigurationFileMap()
                    configFileMap.ExeConfigFilename = configFullFileName
                    config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None)
                Catch ex As Exception
                    clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                Dim exePath As String = System.IO.Path.Combine(Environment.CurrentDirectory, applicationName)
                config = ConfigurationManager.OpenExeConfiguration(exePath)
            End If

            'export Configuration Config
            Try
                Console.WriteLine("Loading Configuration: Export Configuration Section")
                sectionName = "exportConfigurationSection"
                If config.Sections(sectionName) Is Nothing Then
                    config.Sections.Add(sectionName, ExportConfigData)
                    config.Save(ConfigurationSaveMode.Modified)
                    ' Force a reload of the changed section. This makes the new values available for reading.
                    ConfigurationManager.RefreshSection(sectionName)
                End If

                'Load properties using values stored in the configuration file.
                ExportConfigData = DirectCast(config.GetSection(sectionName), ExportConfigurationSection)

            Catch ex As Exception
                clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'export Format Config
            Try
                sectionName = "exportFormatConfigurationSection"
                Dim section As ExportFormatConfigurationSection = DirectCast(config.GetSection(sectionName), ExportFormatConfigurationSection)
                ExportFormatCollection = section.Items
                ExportFormatSection = DirectCast(config.GetSection(sectionName), ExportFormatConfigurationSection)

            Catch ex As Exception
                clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'Qnx Config
            Try
                sectionName = "qnxConfigurationSection"
                QnxConfigData = DirectCast(config.GetSection(sectionName), QnxConfigurationSection)
            Catch ex As Exception
                clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'Test Manager Config
            Try
                sectionName = "testmanagerConfigurationSection"
                TestManagerConfigData = DirectCast(config.GetSection(sectionName), TestManagerConfigurationSection)
            Catch ex As Exception
                clsTopMostMessageBox.Show(ex.ToString(), "Loading configuration error for config file " + ConfigFileName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub


        ' Get the AppSettings section.        
        ' This function uses the AppSettings property to read the appSettings configuration section.
        Public Shared Sub ReadAppSettings()
            Try
                ' Get the AppSettings section.
                Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings

                ' Get the AppSettings section elements.
                Console.WriteLine("Read application settings:")

                If appSettings.Count = 0 Then
                    Console.WriteLine("[ReadAppSettings: {0}]", "AppSettings is empty Use GetSection command first.")
                End If
                Dim i As Integer = 0
                While i < appSettings.Count
                    Console.WriteLine("#{0} Key: {1} Value: {2}", i, appSettings.GetKey(i), appSettings(i))
                    System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
                End While
            Catch e As ConfigurationErrorsException
                Console.WriteLine("[ReadAppSettings: {0}]", e.ToString())
            End Try
        End Sub

        ' Get the ConnectionStrings section.        
        ' This function uses the ConnectionStrings 
        ' property to read the connectionStrings
        ' configuration section.
        Public Shared Sub ReadConnectionStrings()

            ' Get the ConnectionStrings collection.
            Dim connections _
            As ConnectionStringSettingsCollection = ConfigurationManager.ConnectionStrings

            If connections.Count <> 0 Then

                Console.WriteLine()
                Console.WriteLine("Performed the following tasks:")
                Console.WriteLine("Read connection strings:")
                Console.WriteLine()

                ' Get the collection elements.
                For Each connection _
                As ConnectionStringSettings In connections
                    Dim name As String = connection.Name
                    Dim provider As String = connection.ProviderName
                    Dim connectionString As String = _
                    connection.ConnectionString

                    Console.WriteLine("Name:               {0}", name)
                    Console.WriteLine("Connection string:  {0}", connectionString)
                    Console.WriteLine("Provider:            {0}", provider)
                Next
            Else
                Console.WriteLine()
                Console.WriteLine("No connection string is defined.")
                Console.WriteLine()
            End If
        End Sub


        ' Create the AppSettings section.
        ' The function uses the GetSection(string)method 
        ' to access the configuration section. 
        ' It also adds a new element to the section 
        ' collection.
        Public Shared Sub CreateAppSettings()
            ' Get the application configuration file.
            Dim config As System.Configuration.Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

            Dim sectionName As String = "appSettings"

            ' Add an entry to appSettings.
            Dim appStgCnt As Integer = _
            ConfigurationManager.AppSettings.Count
            Dim newKey As String = "NewKey" + appStgCnt.ToString()

            Dim newValue As String = System.DateTime.Now.ToLongDateString() + " " + System.DateTime.Now.ToLongTimeString()

            config.AppSettings.Settings.Add(newKey, newValue)

            ' Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified)

            ' Force a reload of the changed section. This 
            ' makes the new values available for reading.
            ConfigurationManager.RefreshSection(sectionName)

            ' Get the AppSettings section.
            Dim appSettingSection As AppSettingsSection = DirectCast(config.GetSection(sectionName), AppSettingsSection)

            ' Display raw xml.
            Console.WriteLine()
            Console.WriteLine("Performed the following tasks:")
            Console.WriteLine("Created AppSettings section.")
            Console.WriteLine("Added new element to the section.")
            Console.WriteLine("Read the following settings:")
            Console.WriteLine()

            ' Get the AppSettings XML.
            Console.WriteLine(appSettingSection.SectionInformation.GetRawXml())
        End Sub

        ' Access a configuration file using mapping.
        ' This function uses the OpenMappedExeConfiguration 
        ' method to access the roaming and the application
        ' configuration files.   
        ' It then uses the the custom ConsoleSection from
        ' each file to console window colors.
        Public Shared Sub MapExeConfiguration()
            ' Cotains the selected configuration.
            Dim config As System.Configuration.Configuration

            ' Get the roaming configuration file.
            Dim roamingConfig As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming)

            ' Get the application configuration file.
            Dim appConfig As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

            ' To map the selected configuration file.
            Dim configFileMap As New ExeConfigurationFileMap()

            ' Map roaming configuration. This allows
            ' access to the roaming configuration file.
            configFileMap.ExeConfigFilename = roamingConfig.FilePath

            ' Get the mapped configuration file
            config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None)

            Dim sectionName As String = "consoleSection"

            Dim customSection As ConsoleSection = DirectCast(config.GetSection(sectionName), ConsoleSection)

            ' Set console properties using the configuration values contained in the mapped configuration file.
            Console.BackgroundColor = customSection.ConsoleElement.BackgroundColor
            Console.ForegroundColor = customSection.ConsoleElement.ForegroundColor
            'Console.Clear()

            Console.WriteLine()
            Console.WriteLine("Performed following tasks:")
            Console.WriteLine("Mapped roaming configuration file: {0}", config.FilePath)
            Console.WriteLine("Set console colors from stored configuration values.")
            Console.WriteLine()
            Console.Write("Press Enter to switch to default colors.....")
            Console.ReadLine()

            ' Map roaming configuration. This allows
            ' access to the application configuration file.
            configFileMap.ExeConfigFilename = appConfig.FilePath

            ' Get the mapped configuration file
            config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None)

            customSection = DirectCast(config.GetSection(sectionName), ConsoleSection)

            If customSection Is Nothing Then
                customSection = New ConsoleSection()

                ' Store console settings.
                customSection.ConsoleElement.BackgroundColor = ConsoleColor.Black
                customSection.ConsoleElement.ForegroundColor = ConsoleColor.White

                ' Add configuration information to the configuration file.
                config.Sections.Add(sectionName, customSection)
                config.Save(ConfigurationSaveMode.Modified)
                ' Force a reload of the changed section.
                ' This makes the new values available for reading.
                ConfigurationManager.RefreshSection(sectionName)
            End If
            ' Set console properties using the configuration values contained in the mapped configuration file.
            Console.BackgroundColor = customSection.ConsoleElement.BackgroundColor
            Console.ForegroundColor = customSection.ConsoleElement.ForegroundColor
            'Console.Clear()

            Console.WriteLine("Mapped application configuration file: {0}", config.FilePath)
            Console.WriteLine("Set console colors from stored configuration values.")
            Console.WriteLine()
        End Sub

        ' Access the machine configuration file using mapping.
        ' The function uses the OpenMappedMachineConfiguration 
        ' method to access the machine configuration. 
        Public Shared Sub MapMachineConfiguration()
            ' Get the machine.config3 file.
            Dim machineConfig As Configuration = ConfigurationManager.OpenMachineConfiguration()
            ' Get the machine.config file path.
            Dim configFile As New ConfigurationFileMap(machineConfig.FilePath)

            ' Map the application configuration file to the machine 
            ' configuration file.
            Dim config As Configuration = _
            ConfigurationManager.OpenMappedMachineConfiguration(configFile)

            ' Get the AppSettings section.
            Dim appSettingSection As AppSettingsSection = DirectCast(config.GetSection("appSettings"), AppSettingsSection)
            appSettingSection.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToRoamingUser

            ' Display the configuration file sections.
            Dim sections As ConfigurationSectionCollection = config.Sections

            Console.WriteLine()
            Console.WriteLine("Using OpenMappedMachineConfiguration.")
            Console.WriteLine("Sections in machine.config:")

            ' Get the sections in the machine.config.
            For Each section As ConfigurationSection In sections
                Dim name As String = section.SectionInformation.Name
                Console.WriteLine("Name: {0}", name)
            Next

        End Sub



    End Class

#End Region



End Namespace
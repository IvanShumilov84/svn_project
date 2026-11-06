Option Strict On
Option Explicit On

Imports System
Imports System.Collections.ObjectModel

Namespace CEL.TestProgress

    Public Class TestProgressSheet
        Implements System.Xml.Serialization.IXmlSerializable

        Private m_engineModel As String
        Private m_engineSN As String
        Private m_workOrder As String
        Private m_date As Date
        Private m_header As New TestProgressSheetHeader()
        Private m_entries As New TestProgressSheetEntryCollection

        Public Sub New()
            m_engineModel = String.Empty
            m_engineSN = String.Empty
            m_workOrder = String.Empty
            m_date = System.DateTime.Now
        End Sub

        Public Sub New(engineModel As String, engineSN As String, workOrder As String)
            m_engineModel = engineModel
            m_engineSN = engineSN
            m_workOrder = workOrder
            m_date = System.DateTime.Now
        End Sub

        Public ReadOnly Property DateTime As System.DateTime
            Get
                Return m_date
            End Get
        End Property

        Public ReadOnly Property EngineModel As String
            Get
                Return m_engineModel
            End Get
        End Property

        Public ReadOnly Property EngineSN As String
            Get
                Return m_engineSN
            End Get
        End Property

        Public ReadOnly Property Entries As TestProgressSheetEntryCollection
            Get
                Return m_entries
            End Get
        End Property

        Public ReadOnly Property Header As TestProgressSheetHeader
            Get
                Return m_header
            End Get
        End Property

        Public ReadOnly Property WorkOrder As String
            Get
                Return m_workOrder
            End Get
        End Property

        Public Function GetSchema() As Xml.Schema.XmlSchema Implements Xml.Serialization.IXmlSerializable.GetSchema
            Return Nothing
        End Function

        Private Function ReadDateTime(reader As System.Xml.XmlReader) As System.DateTime
            Dim year, month, day, hour, minute As Integer

            reader.MoveToContent()
            year = Integer.Parse(reader.ReadElementString("Year"))
            reader.MoveToContent()
            month = Integer.Parse(reader.ReadElementString("Month"))
            reader.MoveToContent()
            day = Integer.Parse(reader.ReadElementString("Day"))
            reader.MoveToContent()
            hour = Integer.Parse(reader.ReadElementString("Hour"))
            reader.MoveToContent()
            minute = Integer.Parse(reader.ReadElementString("Minute"))
            reader.MoveToContent()

            Return New System.DateTime(year, month, day, hour, minute, 0)
        End Function

        Public Sub ReadXml(reader As System.Xml.XmlReader) Implements System.Xml.Serialization.IXmlSerializable.ReadXml
            reader.MoveToContent()
            reader.ReadStartElement("TestProgressSheet")
            While (Not reader.EOF)
                reader.MoveToContent()
                Select Case reader.NodeType
                    Case System.Xml.XmlNodeType.Element
                        Select Case reader.Name
                            Case "EngineModel"
                                Me.m_engineModel = reader.ReadElementString("EngineModel")
                            Case "EngineSN"
                                Me.m_engineSN = reader.ReadElementString("EngineSN")
                            Case "WorkOrder"
                                Me.m_workOrder = reader.ReadElementString("WorkOrder")
                            Case "DateTime"
                                Dim isEmptyElement As Boolean = reader.IsEmptyElement
                                reader.ReadStartElement("DateTime")
                                If Not isEmptyElement Then
                                    m_date = Me.ReadDateTime(reader)
                                    reader.ReadEndElement()
                                End If
                            Case "Entries"
                                Dim isEmptyElement As Boolean = reader.IsEmptyElement
                                reader.ReadStartElement("Entries")
                                If Not isEmptyElement Then
                                    While reader.MoveToContent() = Xml.XmlNodeType.Element _
                                  AndAlso reader.Name = "TestProgressSheetEntry"
                                        Dim sheetEntry As New TestProgressSheetEntry()
                                        sheetEntry.ReadXml(reader)
                                        Entries.Add(sheetEntry)
                                    End While
                                    reader.ReadEndElement()
                                End If
                            Case "Header"
                                Dim isEmptyElement As Boolean = reader.IsEmptyElement
                                reader.ReadStartElement("Header")
                                If Not isEmptyElement Then
                                    m_header.ReadXml(reader)
                                    reader.ReadEndElement()
                                End If
                            Case Else
                                reader.Skip()
                        End Select
                    Case System.Xml.XmlNodeType.EndElement
                        Select Case reader.Name
                            Case "TestProgressSheet"
                                reader.ReadEndElement()
                                Exit While
                            Case Else
                                reader.Skip()
                        End Select
                    Case Else
                        reader.Skip()
                End Select
            End While
        End Sub

        Public Sub WriteXml(writer As System.Xml.XmlWriter) Implements System.Xml.Serialization.IXmlSerializable.WriteXml
            writer.WriteElementString("EngineModel", EngineModel)
            writer.WriteElementString("EngineSN", EngineSN)
            writer.WriteElementString("WorkOrder", WorkOrder)

            writer.WriteStartElement("DateTime")
            writer.WriteElementString("Year", DateTime.Year.ToString())
            writer.WriteElementString("Month", DateTime.Month.ToString("00"))
            writer.WriteElementString("Day", DateTime.Day.ToString("00"))
            writer.WriteElementString("Hour", DateTime.Hour.ToString())
            writer.WriteElementString("Minute", DateTime.Minute.ToString("00"))
            writer.WriteEndElement()

            writer.WriteStartElement("Header")
            Header.WriteXml(writer)
            writer.WriteEndElement()

            writer.WriteStartElement("Entries")
            For Each entry As TestProgressSheetEntry In Entries
                writer.WriteStartElement("TestProgressSheetEntry")
                entry.WriteXml(writer)
                writer.WriteEndElement()
            Next
            writer.WriteEndElement()
        End Sub
    End Class
End Namespace

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.ObjectModel

Namespace CEL.TestProgress

    Public Class TestProgressSheetEntry
        Implements System.Xml.Serialization.IXmlSerializable

        Private m_result As String
        Private m_dateTime As String
        Private ReadOnly m_checkItemResults As New ObservableCollection(Of String)

        Public Sub New()
            m_dateTime = String.Empty
            m_result = String.Empty
        End Sub

        Public Sub New(dateTime As String, testStepResult As String)
            m_dateTime = dateTime
            m_result = testStepResult
        End Sub

        Public Sub New(dateTime As String, testStepResult As String, testStepCheckItemResults() As String)
            m_dateTime = dateTime
            m_result = testStepResult
            If testStepCheckItemResults IsNot Nothing Then
                m_checkItemResults = New ObservableCollection(Of String)(testStepCheckItemResults)
            End If
        End Sub

        Public ReadOnly Property DateTime As String
            Get
                Return m_dateTime
            End Get
        End Property

        Public Property Result As String
            Get
                Return m_result
            End Get
            Set(value As String)
                m_result = value
            End Set
        End Property

        Public ReadOnly Property CheckItemResults As ObservableCollection(Of String)
            Get
                Return m_checkItemResults
            End Get
        End Property

        Public Function GetSchema() As System.Xml.Schema.XmlSchema Implements System.Xml.Serialization.IXmlSerializable.GetSchema
            Return Nothing
        End Function

        Public Sub ReadXml(reader As System.Xml.XmlReader) Implements System.Xml.Serialization.IXmlSerializable.ReadXml
            reader.MoveToContent()
            reader.ReadStartElement("TestProgressSheetEntry")
            While (Not reader.EOF)
                reader.MoveToContent()
                Select Case reader.NodeType
                    Case System.Xml.XmlNodeType.Element
                        Select Case reader.Name
                            Case "DateTime"
                                Me.m_dateTime = reader.ReadElementString("DateTime")
                            Case "Result"
                                Me.m_result = reader.ReadElementString("Result")
                            Case "CheckItemResults"
                                Dim isEmptyElement As Boolean = reader.IsEmptyElement
                                reader.ReadStartElement("CheckItemResults")
                                If Not isEmptyElement Then
                                    While reader.MoveToContent() = Xml.XmlNodeType.Element _
                                  AndAlso reader.Name = "CheckItem"
                                        CheckItemResults.Add(reader.ReadElementString("CheckItem"))
                                    End While
                                    reader.ReadEndElement()
                                End If
                            Case Else
                                reader.Skip()
                        End Select
                    Case System.Xml.XmlNodeType.EndElement
                        Select Case reader.Name
                            Case "TestProgressSheetEntry"
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
            writer.WriteElementString("DateTime", DateTime)
            writer.WriteElementString("Result", Result)
            writer.WriteStartElement("CheckItemResults")
            For Each item As String In CheckItemResults
                writer.WriteElementString("CheckItem", item)
            Next
            writer.WriteEndElement()
        End Sub
    End Class
End Namespace
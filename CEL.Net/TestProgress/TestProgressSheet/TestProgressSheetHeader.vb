Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic

Namespace CEL.TestProgress

    Public Class TestProgressSheetHeader
        Implements System.Xml.Serialization.IXmlSerializable

        Private m_keyValues As New Dictionary(Of String, String)()

        Public Sub New()
        End Sub

        Default Public Property Items(key As String) As String
            Get
                Return m_keyValues(key)
            End Get
            Set(value As String)
                m_keyValues(key) = value
            End Set
        End Property

        Public Function GetSchema() As Xml.Schema.XmlSchema Implements Xml.Serialization.IXmlSerializable.GetSchema
            Return Nothing
        End Function

        Public Sub ReadXml(reader As Xml.XmlReader) Implements Xml.Serialization.IXmlSerializable.ReadXml
            While Not reader.EOF
                reader.MoveToContent()
                Select Case reader.NodeType
                    Case System.Xml.XmlNodeType.Element
                        Select Case reader.Name
                            Case "HeaderEntry"
                                Dim isEmptyElement As Boolean = reader.IsEmptyElement

                                If reader.HasAttributes Then
                                    ReadHeaderEntryAttributes(reader)
                                End If
                                reader.ReadStartElement("HeaderEntry")
                                If Not isEmptyElement Then
                                    reader.MoveToContent()
                                    reader.ReadEndElement()
                                End If
                            Case Else
                                reader.Skip()
                        End Select
                    Case System.Xml.XmlNodeType.EndElement
                        Select Case reader.Name
                            Case "Header"
                                Exit While
                            Case Else
                                reader.Skip()
                        End Select
                    Case Else
                        reader.Skip()
                End Select
            End While
        End Sub

        Private Sub ReadHeaderEntryAttributes(ByVal reader As Xml.XmlReader)
            Dim curKey As String = String.Empty
            Dim curValue As String = String.Empty
            Dim i As Integer
            For i = 0 To reader.AttributeCount - 1
                reader.MoveToAttribute(i)
                Select Case reader.Name
                    Case "Key"
                        curKey = reader.Value
                    Case "Value"
                        curValue = reader.Value
                End Select
            Next
            reader.MoveToElement()
            If curKey <> String.Empty Then
                m_keyValues(curKey) = curValue
            End If
        End Sub

        Public Sub WriteXml(writer As Xml.XmlWriter) Implements Xml.Serialization.IXmlSerializable.WriteXml
            Dim iter As Dictionary(Of String, String).Enumerator = m_keyValues.GetEnumerator()

            For Each entry As KeyValuePair(Of String, String) In m_keyValues
                writer.WriteStartElement("HeaderEntry")
                writer.WriteAttributeString("Key", entry.Key)
                writer.WriteAttributeString("Value", entry.Value)
                writer.WriteEndElement()
            Next
        End Sub
    End Class
End Namespace

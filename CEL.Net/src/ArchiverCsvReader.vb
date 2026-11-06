'
' Created by SharpDevelop.
' User: Logiciel
' Date: 2/1/2006
' Time: 1:29 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Imports System
Imports System.Collections.Specialized
Imports System.IO

Namespace CEL.Archiver.Csv

	Public Class CsvReader
		Inherits CEL.IO.Csv.CsvReader

		#Region "Constants"

		Public Shadows Const DefaultDelimiter As Char = ";"C

		#End Region
		
		#Region "Fields"

		Private _engineBuild As String
		Private _engineNumber As String
		Private _engineType As String
		Private _fieldHeaders As HybridDictionary
		Private _testBedNumber As String
		Private _testDate As DateTime
		Private _title As String

		#End Region
		
		#Region "Constructors"

		Public Sub New(reader As TextReader, Optional bufferSize As Integer = CsvReader.DefaultBufferSize)
			Me.New(reader, CEL.Archiver.Csv.CsvReader.DefaultDelimiter, _
					CEL.IO.Csv.CsvReader.DefaultQuote, _
					CEL.IO.Csv.CsvReader.DefaultEscape, _
					CEL.IO.Csv.CsvReader.DefaultComment, _
					bufferSize)
		End Sub
		
		Public Sub New(reader As TextReader, delimiter As Char, quote As Char, escape As Char, comment As Char, Optional bufferSize As Integer = CsvReader.DefaultBufferSize)
			MyBase.New(reader, delimiter, quote, escape, comment, bufferSize)
			
			Me.ReadTitle()
			Me.ReadEngineType()
			Me.ReadTestBedNumber()
			Me.ReadEngineNumber()
			Me.ReadEngineBuild()
			Me.ReadTestDate()
			Me.ReadHeader()
		End Sub

		#End Region

		#Region "Properties"
		
		Public ReadOnly Property EngineBuild As String
			Get
				Return Me._engineBuild
			End Get
		End Property
		
		Public ReadOnly Property EngineNumber As String
			Get
				Return Me._engineNumber
			End Get
		End Property

		Public ReadOnly Property EngineType As String
			Get
				Return Me._engineType
			End Get
		End Property
		
		Public Overloads Default ReadOnly Property Field(fieldName As String) As String
			Get
                Return (Me.Field(Me._fieldHeaders(fieldName).ToString()))
			End Get
		End Property

		Public ReadOnly Property FieldHeaders As HybridDictionary
			Get
				Return Me._fieldHeaders
			End Get
		End Property

		Public ReadOnly Property TestBedNumber As String
			Get
				Return Me._testBedNumber
			End Get
		End Property

		Public ReadOnly Property TestData As DateTime
			Get
				Return Me._testDate
			End Get
		End Property

		Public ReadOnly Property Title As String
			Get
				Return Me._title
			End Get
		End Property

		#End Region
		
		#Region "Methods"

		Private Sub ReadEngineBuild()
			If (Me.ReadNextRecord()) Then
				If (Me(0) = "Engine build:") Then
					Me._engineBuild = Me(1)
				End If
			End If
		End Sub

		Private Sub ReadEngineNumber()
			If (Me.ReadNextRecord()) Then
				If (Me(0) = "Engine number:") Then
					Me._engineNumber = Me(1)
				End If
			End If
		End Sub

		Private Sub ReadEngineType()
			If (Me.ReadNextRecord()) Then
				If (Me(0) = "Engine Type:") Then
					Me._engineType = Me(1)
				End If
			End If
		End Sub

		Private Sub ReadHeader()
			If (Me.ReadNextRecord()) Then
				Dim index As Integer
				
				Me._fieldHeaders = New HybridDictionary()
				
				For index = 0 To (Me.FieldCount - 1)
					Me._fieldHeaders.Add(Me(index), index)
				Next
			End If
		End Sub

		Private Sub ReadTestBedNumber()
			If (Me.ReadNextRecord()) Then
				If (Me(0) = "Test bed number:") Then
					Me._testBedNumber = Me(1)
				End If
			End If
		End Sub
		
		Private Sub ReadTestDate()
			If (Me.ReadNextRecord()) Then
				If (Me(0) = "Test date:") Then
					Dim dateString As String = Me(1)
					Dim dateFields As String()
					
					dateFields = dateString.Split("/"C)
					
                    Me._testDate = New DateTime(Convert.ToInt32(dateFields(0)), Convert.ToInt32(dateFields(1)), Convert.ToInt32(dateFields(2)))
				End If
			End If
		End Sub

		Private Sub ReadTitle()

			If (Me.ReadNextRecord()) Then
				If (Me(0) = "Title:") Then
					Me._title = Me(1)
				End If
			End If
		End Sub
		
		#End Region		
	End Class
End Namespace

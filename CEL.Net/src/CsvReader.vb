'
' Created by SharpDevelop.
' User: logiciel
' Date: 2/2/2006
' Time: 1:37 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
' Inspired from Sébastien Lorion CsvReader.cs

Imports System
Imports System.IO

Namespace CEL.IO.Csv

	Public Class CsvReader

		#Region "Constants"

        Private Const CrChar As Char = CChar(Microsoft.VisualBasic.vbCr)
		Public Const DefaultBufferSize As Integer = 4096
		Public Const DefaultComment As Char = "#"C
		Public Const DefaultDelimiter As Char = ","C
		Public Const DefaultEscape As Char = """"C
		Public Const DefaultQuote As Char = """"C
		Private Const LatinChar As Char = Microsoft.VisualBasic.ChrW(&H00ff)
        Private Const LfChar As Char = CChar(Microsoft.VisualBasic.vbLf)
        Private Const TabChar As Char = CChar(Microsoft.VisualBasic.vbTab)
		
		#End Region
		
		#Region "Fields"

		#Region "Settings"
		Private _bufferSize As Integer
		Private _delimiter As Char
		Private _comment As Char
		Private _escape As Char
		Private _quote As Char
		#End Region

		#Region "State"
		Private _buffer() As Char
		Private _bufferLength As Integer
		Private _currentFieldIndex As Integer
		Private _currentRecordIndex As Integer
		Private _eol As Boolean
		Private _eof As Boolean
		Private _fields() As String
		Private _nextFieldStart As Integer
		Private _reader As TextReader

		#End Region
		
		#End Region
		
		#Region "Constructors"
		
		Public Sub New(reader As TextReader, Optional bufferSize As Integer = CsvReader.DefaultBufferSize)
			
			Me.New(reader, CsvReader.DefaultDelimiter, CsvReader.DefaultQuote, CsvReader.DefaultEscape, CsvReader.DefaultComment, bufferSize)
			
		End Sub
		
		Public Sub New(reader As TextReader, delimiter As Char, quote As Char, escape As Char, comment As Char, Optional bufferSize As Integer = CsvReader.DefaultBufferSize)
			
			If (reader Is Nothing) Then
				Throw New ArgumentNullException("reader")
			End If
			
			If (bufferSize <= 0) Then
				Throw New ArgumentOutOfRangeException("bufferSize", bufferSize, "Buffer size must be 1 or more.")
			End If
			
			Me._reader	= reader
			Me._delimiter	= delimiter
			Me._quote	= quote
			Me._escape	= escape
			Me._comment	= comment
			Me._bufferSize	= bufferSize
			
			ReDim Me._buffer(Me._bufferSize - 1)
			ReDim Me._fields(15)
		End Sub
		
		#End Region
		
		#Region "Properties"

		#Region "Setting"

		Public ReadOnly Property Comment As Char
			Get
				Return Me._comment
			End Get
		End Property
		
		Public ReadOnly Property Delimiter As Char
			Get
				Return Me._delimiter
			End Get
		End Property
		
		Public ReadOnly Property Escape As Char
			Get
				Return Me._escape
			End Get
		End Property
		
		Public ReadOnly Property Quote As Char
			Get
				Return Me._quote
			End Get
		End Property

		#End Region
		
		#Region "State"

		Public ReadOnly Property CurrentRecordIndex As Integer
			Get
				Return Me._currentRecordIndex
			End Get
		End Property

		Public ReadOnly Property FieldCount As Integer
			Get
				Return Me._currentFieldIndex
			End Get
		End Property

		Public Default ReadOnly Property Field(fieldIndex As Integer) As String
			Get
				If ((fieldIndex >= 0) And fieldIndex < Me.FieldCount) Then
					
					Return Me._fields(fieldIndex)
				Else
					Throw New ArgumentOutOfRangeException("fieldIndex", fieldIndex, "Field Index must be between 0 and Field Count.")
				End If
			End Get
		End Property

		#End Region

		#End Region
		
		#Region "Methods"

		#Region "GetCurrentRawData"

		Private Function GetCurrentRawData() As String
			
			If (Not (Me._buffer Is Nothing) And Me._bufferLength > 0) Then
				Return New String(Me._buffer, 0, Me._bufferLength)
			Else
				Return String.Empty
			End If
		End Function
		
		#End Region

		#Region "IsWhiteSpace"
		
		Private Shared Function IsWhiteSpace(c As Char) As Boolean
			' See char.IsLatin1(char c) in Reflector
			
			If (c <= LatinChar) Then
				Return (c = " "C OrElse c = TabChar)
			Else
				Return (Char.GetUnicodeCategory(c) = System.Globalization.UnicodeCategory.SpaceSeparator)
			End If
		End Function
		
		#End Region

		#Region "ParseNewLine"
		
		Private Function ParseNewLine(ByRef pos As Integer) As Boolean

			Dim c As Char
			
			If (pos >= Me._bufferLength) Then
				If (Not Me.ReadBuffer()) Then
					Return False
				Else
					pos = 0
				End If
			End If
			
			If (pos < Me._bufferLength) Then
				
				c = Me._buffer(pos)
				
				' Treat "\r" as new line only if it is not the delimiter
				If (c = CrChar And Not(Me._delimiter = CrChar)) Then
				
					pos+= 1

					' Skip following "\n" if there is one
					
					If (pos < Me._bufferLength) Then
						
						If (Me._buffer(pos) = LfChar) Then
							pos+= 1
						End If
					Else
						If (Me.ReadBuffer()) Then
							If (Me._buffer(0) = LfChar) Then
								pos = 1
							Else
								pos = 0
							End If
						End If
					End If
					
					Return True
					
				Else If (c = LfChar) Then
					
					pos+= 1
					
					Return True
				End If
			End If
			
			Return False
		End Function
		
		#End Region

		#Region "ReadBuffer"

		Private Function ReadBuffer() As Boolean
			
			Me._bufferLength = Me._reader.Read(Me._buffer, 0, Me._bufferSize)
			
			If (Me._bufferLength > 0) Then
				Return True
			Else
				Me._eof = True
				Me._buffer = Nothing

				Return False
			End If
		End Function

		#End Region

		#Region "ReadField"

		Private Function ReadField() As String

			Dim value As String = String.Empty
			
			SkipWhiteSpaces(Me._nextFieldStart)

			If (Me._nextFieldStart >= Me._bufferLength) Then
				If (Me.ReadBuffer()) Then
					
					Me._nextFieldStart = 0
				End If
			End If

			If (Me._nextFieldStart < Me._bufferLength) Then

				Dim c As Char = Me._buffer(Me._nextFieldStart)

				If (c = Me._quote) Then
					
					' Quoted Field, skipping over quote
					value = Me.ReadQuotedField(Me._nextFieldStart + 1)
				Else
					value = Me.ReadUnquotedField(Me._nextFieldStart)
				End If
			End If
			
			Return value
		End Function

		Private Function ReadQuotedField(pos As Integer) As String

			Dim c As Char
			Dim escaped As Boolean
			Dim start As Integer = pos
			Dim value As String = String.Empty

			While (True)
				
				If (pos >= Me._bufferLength) Then
					If (start < pos) Then
						value+= New String(Me._buffer, start, pos - start)
					End If

					pos = 0
					start = 0
					Me._nextFieldStart = 0
					
					If (Not Me.ReadBuffer()) Then
						Exit While
					End If
				End If

				c = Me._buffer(pos)

				If (escaped) Then
					
					escaped = False
					start = pos
					
				Else If (c = Me._escape AndAlso (   (Me._escape <> Me._quote) OrElse _
							            (((pos + 1) < Me._bufferLength AndAlso Me._buffer(pos+1) = Me._quote) OrElse _
							             ((pos + 1) = Me._bufferLength AndAlso Me._reader.Peek() = Microsoft.VisualBasic.AscW(Me._quote))))) Then
							             
					value+= New string(Me._buffer, start, pos - start)
					escaped = True
					
				Else If (c = Me._quote) Then
				
					Exit While					
				End If
				
				pos+= 1
			End While

			If (start < pos) Then
				value+= New string(Me._buffer, start, pos - start)
			End If

			If (Not Me._eof) Then
				' Skip quote
				Me._nextFieldStart = pos + 1

				' Skip whitespaces between quote and delimiter
				Me.SkipWhiteSpaces(Me._nextFieldStart)

				If (Me._nextFieldStart >= Me._bufferLength) Then
					Me.ReadBuffer()
				End If
				
				If (Me._nextFieldStart < Me._bufferLength) Then
					
					c = Me._buffer(Me._nextFieldStart)
					
					If (c = Me._delimiter) Then

						' Skip delimiter						
						Me._nextFieldStart+= 1
						
					Else If (c = CrChar Or c = LfChar) Then
					
						Me._eol = True
					Else
						' Garbage characters between quote and Field/Record delimiter
                        Throw New Exception("The CSV appear to be corrupt near record " + Me._currentRecordIndex.ToString + _
                            ", position " + Me._nextFieldStart.ToString + "." + "Current raw data : " + Me.GetCurrentRawData().ToString)
					End If
				End If
			End If

			Return value			
		End Function
		
		Private Function ReadUnQuotedField(pos As Integer) As String

			Dim c As Char
			Dim start As Integer = pos
			Dim value As String = String.Empty
			
			While(True)
			
				If (pos >= Me._bufferLength) Then

					If (start < pos) Then
						value+= New string(Me._buffer, start, pos - start)
					End If		

					pos = 0
					start = 0
					Me._nextFieldStart = 0

					If (Not Me.ReadBuffer()) Then
						Exit While
					End If
				End If
				
				c = Me._buffer(pos)
				
				If (c = Me._delimiter) Then
					
					Me._nextFieldStart = pos + 1
					
					Exit While
				Else If (c = CrChar Or c = LfChar) Then
				
					Me._nextFieldStart = pos
					Me._eol = True
					
					Exit While
				Else
					pos+= 1
				End If
			End While

			If (start < pos) Then
				
				value+= New string(Me._buffer, start, pos - start)
			End If

			Return value
		End Function

		#End Region

		#Region "ReadNextRecord"

		Public Function ReadNextRecord() As Boolean

			Dim value As String

			Me._currentFieldIndex = 0
			Me._eol = False

			If (Me._eof) Then
				Return False
			End If

			If (SkipBlankAndCommentedLines(Me._nextFieldStart)) Then

				Me._currentRecordIndex+= 1
					
				While (Not Me._eol And Not Me._eof)

					value = Me.ReadField()
					If (value Is Nothing) Then
						value = String.Empty
					End If

					If (Me._fields.GetUpperBound(0) < Me._currentFieldIndex) Then
						ReDim Preserve Me._fields(Me._currentFieldIndex+16)
					End If

					Me._fields(Me._currentFieldIndex) = value
					Me._currentFieldIndex+= 1
					
					If (Me._eol) Then
						Me.ParseNewLine(Me._nextFieldStart)
					End If
				End While
			Else
				Return False
			End If
			Return True
		End Function
		
		#End Region

		#Region "SkipBlankAndCommentedLines"

		Private Function SkipBlankAndCommentedLines(ByRef pos As Integer) As Boolean

			If (pos < Me._bufferLength) Then
				DoSkipBlankAndCommentedLines(pos)
			End If
			
			While (pos >= Me._bufferLength AndAlso (Not Me._eof))
				If (Me.ReadBuffer()) Then
					pos = 0
					
					DoSkipBlankAndCommentedLines(pos)
				Else
					Return False
				End If
			End While
			
			Return (Not Me._eof)
		End Function

		Private Sub DoSkipBlankAndCommentedLines(ByRef pos As Integer)
			
			While (pos < Me._bufferLength)
				
				If (Me._buffer(pos) = Me._comment) Then
					
					' Comment Line.
					' 
					pos+= 1

					While (True)
						If (pos >= Me._bufferLength) Then
							If (Me.ReadBuffer()) Then
								pos = 0
							Else
								Exit While
							End If
						Else If (Me.ParseNewLine(pos)) Then
						
							Exit While
						Else
							pos+= 1
						End If
					End While
				Else If (Me.ParseNewLine(pos))
				
					' Continue
				Else
					Exit While
				End If
			End While
		End Sub

		#End Region

		#Region "SkipWhiteSpaces"

		Private Function SkipWhiteSpaces(ByRef pos As Integer) As Boolean
			
			While (True)
				
                While (pos < Me._bufferLength AndAlso CsvReader.IsWhiteSpace(Me._buffer(pos)))
                    pos += 1
                End While
				
				If (pos < Me._bufferLength) Then
					Exit While
				Else
					pos = 0
					
					If (Not Me.ReadBuffer()) Then
						Return False
					End If
				End If
			End While
			
			Return True
		End Function

		#End Region

		#End Region
		
	End Class

End Namespace

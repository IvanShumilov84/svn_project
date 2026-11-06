Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Printing
Imports System.IO


Namespace CEL.Forms
	Public Class DisplayFile
		Inherits Form

		Public Sub New(fileName As String)
			MyBase.New()
			Dim WindowWidth, WindowHeight as integer
			
			Me.fileName = fileName
			
			WindowWidth = 800
            WindowHeight = 810

			richTextBox1 = New RichTextBox()
			richTextBox1.Dock = DockStyle.Fill
			richTextBox1.Font = New Font(New FontFamily("Lucida Console"), 10)
			richTextBox1.WordWrap = False
			richTextBox1.LoadFile(fileName, RichTextBoxStreamType.PlainText)
			richTextBox1.ReadOnly = True
			
			Dim PrintButton As Button = New Button()
			PrintButton.Text = "PRINT"
			PrintButton.Left = WindowWidth - 100
			PrintButton.Top = 0

			Controls.Add(PrintButton)
			Controls.Add(richTextBox1)
			
			ShowInTaskbar = False
			Size = New Size(WindowWidth,WindowHeight)

			AddHandler PrintButton.Click, AddressOf PrintButton_Click
		End Sub

		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
			MyBase.Dispose(disposing)
		End Sub
		
		Sub PrintButton_Click(sender As Object, e As EventArgs)
			Try
				streamToPrint = New StringReader(richTextBox1.Text)
				Try
					'printFont = New Font("Arial", 10)
					printFont = richTextBox1.Font
					Dim pd As New PrintDocument()
					Dim margins As New Margins(50,50,100,100)
					pd.DefaultPageSettings.Margins = margins
					AddHandler pd.PrintPage, AddressOf pd_PrintPage
					' Print the document.
					pd.Print()
				Finally
					streamToPrint.Close()
				End Try
			Catch ex As Exception
				MessageBox.Show(ex.Message)
			End Try
		End Sub   
    	
		' The PrintPage event is raised for each page to be printed.
		Private Sub pd_PrintPage(sender As Object, ev As PrintPageEventArgs)
			Dim linesPerPage As Single = 0
			Dim yPos As Single = 0
			Dim count As Integer = 0
			Dim leftMargin As Single = ev.MarginBounds.Left
			Dim topMargin As Single = ev.MarginBounds.Top
			Dim line As String = Nothing
        
			' Calculate the number of lines per page.
			linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics)
        
			' Iterate over the file, printing each line.
			While count < linesPerPage
				line = streamToPrint.ReadLine()
				If line Is Nothing Then
					Exit While
				End If
				yPos = topMargin + count * printFont.GetHeight(ev.Graphics)
				ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
				count += 1
			End While
        
			' If more lines exist, print another page.
			If Not (line Is Nothing) Then
				ev.HasMorePages = True
			Else
				ev.HasMorePages = False
			End If
		End Sub    

		Private fileName As String     	    
		Private richTextBox1 As RichTextBox
		Private printFont As Font
		Private streamToPrint As StringReader
	End Class
End Namespace

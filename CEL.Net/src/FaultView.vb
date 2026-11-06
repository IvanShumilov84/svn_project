'
' Created by SharpDevelop.
' User: logiciel
' Date: 1/5/2006
' Time: 4:32 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Imports System
Imports System.Collections
Imports System.Drawing
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace CEL.Controls
	
	Public Class FaultView
		Inherits System.Windows.Forms.UserControl
		Private aCMCChannelCollection As CEL.ACMCChannelCollection
'		Private tableStyle As System.Windows.Forms.DataGridTableStyle
'		Private codesStyle As CEL.Controls.DataGridTextBoxColumnEx
'		Private dataGridEx1 As CEL.Controls.DataGridEx
'		Private label1 As System.Windows.Forms.Label
		Private textBox1 As System.Windows.Forms.TextBox
		
		Public Sub New()
			MyBase.New
'			Me.codesStyle = New CEL.Controls.DataGridTextBoxColumnEx
'			Me.tableStyle = New System.Windows.Forms.DataGridTableStyle
			' Must be called for initialization
			Me.InitializeComponent
'			Me.codesStyle = New CEL.Controls.DataGridTextBoxColumnEx
'			Me.tableStyle = New System.Windows.Forms.DataGridTableStyle
'			Me.codesStyle.MappingName = "Codes"
'			Me.tableStyle.ColumnHeadersVisible = False
'			Me.tableStyle.MappingName = "ACMCChannelCollection"
'			Me.tableStyle.ReadOnly = True
'			Me.tableStyle.GridColumnStyles.Add(Me.codesStyle)
'			Me.dataGridEx1.TableStyles.Add(Me.tableStyle)
			Me.UpdateColumnSize()
			
'			AddHandler Me.dataGridEx1.CurrentCellChanged, AddressOf Me.OnDataGridCurrentCellChangedHdlr
		End Sub

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
		Public ReadOnly Shadows Property Controls As ControlCollection
			Get
				Return MyBase.Controls
			End Get
		End Property

		Public Sub SetDataBinding(ByVal value As ACMCChannelCollection)
			If (Not value Is Me.aCMCChannelCollection) Then
'				Me.dataGridEx1.SetDataBinding(value, "")
				If (Not Me.aCMCChannelCollection Is Nothing) Then
					RemoveHandler Me.aCMCChannelCollection.ListChanged, AddressOf Me.OnACMCChannelCollectionListChangedHdlr
				End If
				Me.aCMCChannelCollection = value
				If (Not value Is Nothing) Then
					AddHandler Me.aCMCChannelCollection.ListChanged, AddressOf Me.OnACMCChannelCollectionListChangedHdlr
				End If
				
				Me.UpdateFaultListTextBox()
			End If
		End Sub
		
		Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
			MyBase.SuspendLayout()
'			Me.tableStyle.AlternatingBackColor = Me.BackColor
'			Me.tableStyle.BackColor = Me.BackColor
			Me.textBox1.BackColor = Me.BackColor
			'Me.dataGridEx1.BackgroundColor = Me.BackColor
			MyBase.ResumeLayout
			MyBase.OnBackColorChanged(e)
		End Sub

		Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
'			Me.tableStyle.ForeColor = Me.ForeColor
			Me.textBox1.ForeColor = Me.ForeColor
			MyBase.OnForeColorChanged(e)
		End Sub

		#Region " Windows Forms Designer generated code "
		' This method is required for Windows Forms designer support.
		' Do not change the method contents inside the source code editor. The Forms designer might
		' not be able to load this method if it was changed manually.
		Private Sub InitializeComponent()
			Me.textBox1 = New System.Windows.Forms.TextBox
'			Me.label1 = New System.Windows.Forms.Label
'			Me.dataGridEx1 = New CEL.Controls.DataGridEx
'			CType(Me.dataGridEx1,System.ComponentModel.ISupportInitialize).BeginInit
			Me.SuspendLayout
			'
			'textBox1
			'
			Me.textBox1.Cursor = System.Windows.Forms.Cursors.Default
			Me.textBox1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.textBox1.Location = New System.Drawing.Point(0, 304)
			Me.textBox1.Multiline = true
			Me.textBox1.Name = "textBox1"
			Me.textBox1.ReadOnly = true
			Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
'			Me.textBox1.Size = New System.Drawing.Size(440, 160)
			Me.textBox1.TabIndex = 0
            Me.textBox1.Text = "" ' No Text by Default
			Me.textBox1.WordWrap = false
			'
			'label1
			'
'			Me.label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
'						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
'			Me.label1.Location = New System.Drawing.Point(0, 280)
'			Me.label1.Name = "label1"
'			Me.label1.Size = New System.Drawing.Size(440, 23)
'			Me.label1.TabIndex = 2
'			Me.label1.Text = "Fault(s) Description"
'			Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			'
			'dataGridEx1
			'
'			Me.dataGridEx1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
'						Or System.Windows.Forms.AnchorStyles.Left)  _
'						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
'			Me.dataGridEx1.BackgroundColor = System.Drawing.Color.Transparent
'			Me.dataGridEx1.BorderStyle = System.Windows.Forms.BorderStyle.None
'			Me.dataGridEx1.CaptionVisible = false
'			Me.dataGridEx1.ColumnHeadersVisible = false
'			Me.dataGridEx1.HeaderForeColor = System.Drawing.SystemColors.ControlText
'			Me.dataGridEx1.Location = New System.Drawing.Point(0, 0)
'			Me.dataGridEx1.Name = "dataGridEx1"
'			Me.dataGridEx1.Size = New System.Drawing.Size(440, 277)
'			Me.dataGridEx1.TabIndex = 0
			'
			'FaultView
			'
'			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.textBox1)
'			Me.Controls.Add(Me.dataGridEx1)
			Me.Name = "FaultView"
			Me.Size = New System.Drawing.Size(440, 464)
'			CType(Me.dataGridEx1,System.ComponentModel.ISupportInitialize).EndInit
			Me.ResumeLayout(false)
		End Sub
		#End Region

		Private Sub OnACMCChannelCollectionListChangedHdlr(sender As Object, e As ListChangedEventArgs)
			Me.UpdateFaultListTextBox()
		End Sub
		
		Private Sub OnDataGridCurrentCellChangedHdlr(sender As Object, e As EventArgs)
			Me.UpdateFaultListTextBox()
		End Sub

		Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
			MyBase.OnSizeChanged(e)
			Me.UpdateColumnSize
		End Sub

		Private Sub UpdateColumnSize()
'			Dim columnWidth = Me.dataGridEx1.ClientSize.Width
'			If (Me.dataGridEx1.RowHeadersVisible) Then
'				columnWidth = Math.Max(columnWidth - Me.dataGridEx1.RowHeaderWidth, 5)
'			End If
			MyBase.SuspendLayout()
'			Me.codesStyle.Width = columnWidth
			MyBase.ResumeLayout()
		End Sub
		
		Private Sub UpdateFaultListTextBox()
			Dim i As Integer
			Dim curRow As Integer
			Dim faultsMessages As String()
			Dim faultsDesc As New System.Collections.Specialized.StringCollection

			For curRow = 0 To Me.aCMCChannelCollection.Count() - 1
				faultsMessages = Me.aCMCChannelCollection(curRow).ActiveFaultDescriptions
				For i = 0 To faultsMessages.GetUpperBound(0)
					faultsDesc.Add(faultsMessages(i))
				Next i
			Next curRow
			
			Dim result(faultsDesc.Count()-1) As String
            faultsDesc.CopyTo(result, 0)


            
            UpdateTextBoxResult(result)
            'Me.textBox1.Lines = result


        End Sub

        'Use Delegate for Cross Thread Operation
        Private Delegate Sub UpdateTextBoxResultDelegate(ByRef result() As String)
        Private Sub UpdateTextBoxResult(ByRef result() As String)

            If (Me.textBox1.InvokeRequired) Then
                Me.textBox1.BeginInvoke(New UpdateTextBoxResultDelegate(AddressOf Me.UpdateTextBoxResult), New Object() {result})
            Else
                textBox1.Lines = result
            End If
        End Sub
	End Class
End Namespace

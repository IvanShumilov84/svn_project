'
' Created by SharpDevelop.
' User: Logiciel
' Date: 2005-07-22
' Time: 15:58
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Imports System
Imports System.Drawing
Imports System.Windows.Forms

Namespace CEL.Perf
	
	Public Class PerfPWCalCondListForm
		Inherits System.Windows.Forms.Form
		Private dataGrid1 As System.Windows.Forms.DataGrid
		Private button1 As System.Windows.Forms.Button
		Private button2 As System.Windows.Forms.Button
		
		Public Sub New(condListItems() As PerfPWCalConditionListItem)
			MyBase.New
			'
			' The Me.InitializeComponent call is required for Windows Forms designer support.
			'
			Me.InitializeComponent
			
			dataGrid1.SetDataBinding(condListItems,"")
			CreateTableStyle
		End Sub
		
		#Region " Windows Forms Designer generated code "
		' This method is required for Windows Forms designer support.
		' Do not change the method contents inside the source code editor. The Forms designer might
		' not be able to load this method if it was changed manually.
		Private Sub InitializeComponent()
			Me.dataGrid1 = New System.Windows.Forms.DataGrid
			Me.button1 = New System.Windows.Forms.Button
			Me.button2 = New System.Windows.Forms.Button
			CType(Me.dataGrid1,System.ComponentModel.ISupportInitialize).BeginInit
			Me.SuspendLayout
			'
			'dataGrid1
			'
			Me.dataGrid1.DataMember = ""
			Me.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
			Me.dataGrid1.Location = New System.Drawing.Point(0, 0)
			Me.dataGrid1.Name = "dataGrid1"
			Me.dataGrid1.Size = New System.Drawing.Size(600, 304)
			Me.dataGrid1.Anchor = (AnchorStyles.Left Or AnchorStyles.Top Or _
				AnchorStyles.Right)
			Me.dataGrid1.TabIndex = 0
			Me.button1.DialogResult = DialogResult.OK
			Me.button1.Location = New Point(10,301)
			Me.button1.Text = "OK"
			Me.button2.DialogResult = DialogResult.Cancel
			Me.button2.Location = New Point(582, 301)
			Me.button2.Text = "Cancel"
			'
			'CELPerfPWCalCondList
			'
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(592, 342)
			Me.AcceptButton = button1
			Me.CancelButton = button2
			Me.Controls.Add(Me.dataGrid1)
			Me.Controls.Add(Me.button1)
			Me.Controls.Add(Me.button2)
			Me.Name = "CELPerfPWCalCondList"
			Me.Text = "CELPerfPWCalCondList"
			AddHandler Load, AddressOf Me.CELPerfPWCalCondListLoad
			CType(Me.dataGrid1,System.ComponentModel.ISupportInitialize).EndInit
			Me.ResumeLayout(false)
		End Sub
		#End Region
		
		Private Sub CELPerfPWCalCondListLoad(sender As System.Object, e As System.EventArgs)
			
			
		End Sub
		
		Private Sub CreateTableStyle
			Dim calCondListTable As New DataGridTableStyle
						
			calCondListTable.MappingName = "PerfPWCalConditionListItem[]"
			
			' Creates four column styles.
			Dim condNoColumn As New DataGridTextBoxColumn()
			condNoColumn.MappingName = "ConditionNumber"
			condNoColumn.HeaderText = "ConditionNo"
			condNoColumn.ReadOnly = TRUE

			Dim thrustColumn As New DataGridTextBoxColumn()
			thrustColumn.MappingName = "Thrust"
			thrustColumn.HeaderText = "Thrust"
			thrustColumn.ReadOnly = TRUE

			Dim t45Column As New DataGridTextBoxColumn()
			t45Column.MappingName = "T45"
			t45Column.HeaderText = "T45"
			t45Column.ReadOnly = TRUE

			Dim activeColumn As New DataGridBoolColumn()
			activeColumn.MappingName = "Active"
			activeColumn.HeaderText = "Active"

			' Adds the column styles to the grid table style.
			calCondListTable.GridColumnStyles.Add(condNoColumn)
			calCondListTable.GridColumnStyles.Add(thrustColumn)
			calCondListTable.GridColumnStyles.Add(t45Column)
			calCondListTable.GridColumnStyles.Add(activeColumn)

			' Add the table style to the collection, but clear the 
			' collection first.
			DataGrid1.TableStyles.Clear()
			DataGrid1.TableStyles.Add(calCondListTable)

		End Sub
	End Class
End Namespace

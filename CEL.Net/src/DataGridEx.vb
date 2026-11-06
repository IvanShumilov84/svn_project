'
' Created by SharpDevelop.
' User: logiciel
' Date: 12/21/2005
' Time: 10:32 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Imports System.ComponentModel
Imports System.Windows.Forms

Namespace CEL.Controls

	Public Class DataGridEx
		Inherits DataGrid

		Public Sub New()
			'Enable Double Buffering to reduce flickering
			Me.SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.DoubleBuffer, True)
		End Sub
		
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Shadows Property DataMember As String
			Get
				Return MyBase.DataMember
			End Get
			Set(ByVal value As String)
				MyBase.DataSource = value
			End Set
		End Property

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Shadows Property DataSource As Object
			Get
				Return MyBase.DataSource
			End Get
			Set(ByVal value As Object)
				MyBase.DataSource = value
			End Set
		End Property

		Public ReadOnly Property HorizScrollBarHeight As Integer
			Get
				Return Me.horizScrollBar.Height
			End Get
		End Property
		
		Public ReadOnly Property HorizScrollBarVisible As Boolean
			Get
				Return Me.horizScrollBar.Visible
			End Get
		End Property
		
		Public ReadOnly Property VertScrollBarVisible As Boolean
			Get
				Return Me.VertScrollBar.Visible
			End Get
		End Property

		Public ReadOnly Property VertScrollBarWidth As Integer
			Get
				Return Me.vertScrollBar.Width
			End Get
		End Property
		
	End Class

	Public Class DataGridTextBoxColumnEx
		Inherits DataGridTextBoxColumn
		
		Private _backColor As System.Drawing.Color = System.Drawing.Color.Empty
		Private _borderColor As System.Drawing.Color = System.Drawing.Color.Empty
		Private _borderStyle As System.Windows.Forms.BorderStyle = System.Windows.Forms.BorderStyle.None
		Private _borderWidth As Single = 1.0
		Private _foreColor As System.Drawing.Color = System.Drawing.Color.Empty
		
		Public Property BackColor As System.Drawing.Color
			Get
				Return Me._backColor
			End Get
			Set (ByVal value As System.Drawing.Color)
				If (Not Me._backColor.Equals(value)) Then
					Me._backColor = value
					Me.textBox.BackColor = Me._backColor
					Me.Invalidate
				End If
			End Set
		End Property
		
		Public Property BorderColor As System.Drawing.Color
			Get
				Return Me._borderColor
			End Get
			Set (ByVal value As System.Drawing.Color)
				If (Not Me._borderColor.Equals(value)) Then
					Me._borderColor = value
					Me.Invalidate
				End If
			End Set
		End Property
		
		Public Property BorderStyle As System.Windows.Forms.BorderStyle
			Get
				Return Me._borderStyle
			End Get
			Set (ByVal value As System.Windows.Forms.BorderStyle)
				If (Not Me._borderStyle.Equals(value)) Then
					Me._borderStyle = value
					Me.Invalidate
				End If
			End Set
		End Property

		Public Property BorderWidth As Single
			Get
				Return Me._borderWidth
			End Get
			Set (ByVal value As Single)
				If (Not Me._borderWidth.Equals(value)) Then
					Me._borderWidth = value
					Me.Invalidate
				End If
			End Set
		End Property
		
		Public Property ForeColor As System.Drawing.Color
			Get
				Return Me._foreColor
			End Get
			Set (ByVal value As System.Drawing.Color)
				Me._foreColor = value
				Me.TextBox.ForeColor = value
				Me.Invalidate
			End Set
		End Property

		Overloads Protected Overrides Sub Edit( _
			ByVal source As CurrencyManager, _
			ByVal rowNum As Integer, _
			ByVal bounds As System.Drawing.Rectangle, _
			ByVal isReadOnly As Boolean, _
			ByVal instantText As String, _
			ByVal cellIsVisible As Boolean _
		)
			If (Not ((isReadOnly OrElse Me.ReadOnly) OrElse Me.DataGridTableStyle.ReadOnly)) Then
				MyBase.Edit(source, rowNum, bounds, isReadOnly, instantText, cellIsVisible)
			End If
		End Sub
	
		Overloads Protected Overrides Sub Paint( _
			ByVal g As System.Drawing.Graphics, _
			ByVal bounds As System.Drawing.Rectangle, _
			ByVal source As CurrencyManager, _
			ByVal rowNum As Integer, _
			ByVal backBrush As System.Drawing.Brush, _
			ByVal foreBrush As System.Drawing.Brush, _
			ByVal alignToRight As Boolean _
		)
			Dim tmpBackBrush, tmpForeBrush As System.Drawing.Brush
			If (Not Me._backColor.Equals(System.Drawing.Color.Empty)) Then
				tmpBackBrush = New system.Drawing.SolidBrush(Me._backColor)
			Else
				tmpBackBrush = backBrush
			End If
			If (Not Me._foreColor.Equals(System.Drawing.Color.Empty)) Then
				tmpForeBrush = New system.Drawing.SolidBrush(Me._foreColor)
			Else
				tmpForeBrush = foreBrush
			End If
			
			MyBase.Paint(g, bounds, source, rowNum, tmpBackBrush, tmpForeBrush, alignToRight)
			
			If (Not Me._backColor.Equals(System.Drawing.Color.Empty)) Then
				tmpBackBrush.Dispose()
			End If
			If (Not Me._foreColor.Equals(System.Drawing.Color.Empty)) Then
				tmpForeBrush.Dispose()
			End If
			
			Select (Me._borderStyle)
			Case System.Windows.Forms.BorderStyle.FixedSingle
				Dim tmpPen As New System.Drawing.Pen(Me._borderColor, Me._borderWidth)
				Dim tmpRect As New System.Drawing.Rectangle(bounds.Location, bounds.Size)
				
				tmpRect.Width = (bounds.Width - 1)
				tmpRect.Height = (bounds.Height - 1)
				g.DrawRectangle(tmpPen, tmpRect)
				tmpPen.Dispose()
			End Select
		End Sub
	End Class
End Namespace

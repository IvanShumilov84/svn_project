'
' Created by SharpDevelop.
' User: logiciel
' Date: 9/6/2005
' Time: 9:38 AM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports KineticaRT.Controls
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.Serialization
Imports System.Globalization
Imports System.Reflection
Imports Microsoft.VisualBasic

Namespace CEL.Controls

	<TypeConverter(GetType(ObjectTypeConverter))> _
	Public Class Point
        Inherits Global.KineticaRT.Controls.SubObjectBase
		
		Public Property X As Double
			Get
				Return myX
			End Get
			Set (ByVal newX As Double)
				If (newX <> myX) Then
					Me.myX = newX
					Me.OnPropertyChanged("X")
				End If
			End Set
		End Property
		
		Public Property Y As Double
			Get
				Return myY
			End Get
			Set (ByVal newY As Double)
				If (newY <> myY) Then
					Me.myY = newY
					Me.OnPropertyChanged("Y")
				End If
			End Set
		End Property

		Public Sub New()
			myX = 0.0
			myY = 0.0
		End Sub
		
		Public Sub New(ByVal newPoint As System.Drawing.Point)
			myX = newPoint.X
			myY = newPoint.Y
		End Sub
		
		Public Sub New(ByVal x As Double, ByVal y As Double)
			myX = x
			myY = y
		End Sub
				
		Private myX As Double
		Private myY As Double
	End Class

	<TypeConverter(GetType(PointCollection.PointCollConverter)), DefaultMember("Item")> _
	Public Class PointCollection
        Inherits Global.KineticaRT.Controls.SubObjectCollection
		'Inherits CollectionBase
		
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Property Item(ByVal Index As Integer) As Point
			Get
				Return CType(MyBase.List.Item(Index), Point)
			End Get
			Set (ByVal value As Point)
				MyBase.List.Item(index) = value
			End Set
		End Property

		Public Sub New
			
		End Sub

		Public Sub New(ByVal pointArray() As Point)
			AddRange(pointArray)
		End Sub
		
		Public Sub New(ByVal pointArray() As System.Drawing.Point)
			AddRange(pointArray)
		End Sub
		
		Public Sub AddRange(ByVal pointArray() As Point)
			Dim i As Integer
			
			For i = 0 To pointArray.Length - 1
				MyBase.List.Add(pointArray(i))
			Next
		End Sub
		
		Public Sub AddRange(ByVal pointArray() As System.Drawing.Point)
			Dim i As Integer
			
			For i = 0 To pointArray.Length - 1
				MyBase.List.Add(New Point(pointArray(i)))
			Next
		End Sub
		
		'Nested Types
		Friend Class PointCollConverter
            Inherits Global.KineticaRT.Controls.ObjectTypeConverter
			'Inherits TypeConverter
			
			Public Sub New()
			End Sub

			Public Overloads Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
				If (destinationType Is GetType(InstanceDescriptor)) Then
					Return True
				End If
				Return MyBase.CanConvertTo(context, destinationType)
			End Function
			
			Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
				If (Not destinationType Is GetType(InstanceDescriptor)) Then
					Return MyBase.ConvertTo(context, culture, value, destinationType)
				End If
				Dim collection1 As PointCollection = CType(value, PointCollection)
				Dim pointArray1 As Point() = New Point(collection1.Count() -1) {}
				Dim typeArray1 As Type() = New Type() {GetType(Point())}
				Dim info1 As ConstructorInfo = GetType(PointCollection).GetConstructor(typeArray1)
				Dim num1 As Integer
				
				For num1 = 0 To collection1.Count - 1
					pointArray1(num1) = collection1.Item(num1)
				Next num1
				Dim objArray1 As Object() = New Object() { pointArray1 }
				Return New InstanceDescriptor(info1, objArray1)
			End Function
		End Class
	End Class

	<TypeConverter(GetType(ObjectTypeConverter))> _
	Public Class XYTramLine
        Inherits Global.KineticaRT.Controls.SubObjectBase
		
		<DefaultValue(True), Category("TramLine"), Description("Gets or Sets whether the Tram line is enabled")> _
		Public Property Enabled As Boolean
			Get
				Return Me.myEnabled
			End Get
			Set (ByVal value As Boolean)
				If (Me.myEnabled <> value) Then
					Me.myEnabled = value
					Me.OnPropertyChanged("Enabled")
				End If
			End Set
		End Property
		
		<Category("TramLine"), Description("Gets or Sets the color of the Tram line")> _
		Public Property LineColor As Color
			Get
				Return Me.myLineColor
			End Get
			Set (ByVal value As Color)
				If (Not Me.myLineColor.Equals(value)) Then
					Me.myLineColor = value
					Me.OnPropertyChanged("LineColor")
				End If
			End Set
		End Property

		<Description("Gets or Sets the collection of points"), Category("TramLine"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
		Public Property Points As PointCollection
			Get
				Return Me.myPoints
			End Get
			Set (ByVal value As PointCollection)
				If (Not Me.myPoints Is Nothing) Then
					RemoveHandler Me.myPoints.PropertyChanged, Me.propertyHdlr
				End If
				Me.myPoints = value
				If (Not Me.myPoints Is Nothing) Then
					AddHandler Me.myPoints.PropertyChanged, Me.propertyHdlr
				End If
				Me.OnPropertyChanged("Points")
			End Set
		End Property
		
		<DefaultValue(1), Category("TramLine"), Description("Gets or Sets the width of the Tram line in pixels")> _
		Public Property Width As Integer
			Get
				Return Me.myWidth
			End Get
			Set (ByVal value As Integer)
				If (Me.myWidth <> value) Then
					Me.myWidth = value
					Me.OnPropertyChanged("width")
				End If
			End Set
		End Property
		
		<Category("TramLine"), Description("Gets or sets the Maximum X value displayed on the Graph"), DefaultValue(CType(100, Double))> _
		Public Property XMaximum As Double
			Get
				Return Me.myXMaximum
			End Get
			Set(ByVal value As Double)
				If (Me.myXMaximum <> value) Then
					Me.myXMaximum = value
					Me.OnPropertyChanged("XMaximum")
				End If
			End Set
		End Property

		<Category("TramLine"), Description("Gets or sets the Minimum X value displayed on the Graph"), DefaultValue(CType(100, Double))> _
		Public Property XMinimum As Double
			Get
				Return Me.myXMinimum
			End Get
			Set(ByVal value As Double)
				If (Me.myXMinimum <> value) Then
					Me.myXMinimum = value
					Me.OnPropertyChanged("XMinimum")
				End If
			End Set
		End Property
		
		<DefaultValue(3), Description("Gets or sets the source of the Minimum and Maximum X values displayed"), Category("TramLine")> _
		Public Property XRangeSource As XRangeSources
			Get
				Return Me.myXRangeSource
			End Get
			Set(ByVal value As XRangeSources)
				If (Me.myXRangeSource <> value) Then
					Me.myXRangeSource = value
					Me.OnPropertyChanged("XRangeSource")
				End If
			End Set
		End Property
		
		<Category("TramLine"), Description("Gets or sets the Maximum Y value displayed on the Graph"), DefaultValue(CType(100, Double))> _
		Public Property YMaximum As Double
			Get
				Return Me.myYMaximum
			End Get
			Set(ByVal value As Double)
				If (Me.myYMaximum <> value) Then
					Me.myYMaximum = value
					Me.OnPropertyChanged("YMaximum")
				End If
			End Set
		End Property

		<Category("TramLine"), Description("Gets or sets the Minimum Y value displayed on the Graph"), DefaultValue(CType(100, Double))> _
		Public Property YMinimum As Double
			Get
				Return Me.myYMinimum
			End Get
			Set(ByVal value As Double)
				If (Me.myYMinimum <> value) Then
					Me.myYMinimum = value
					Me.OnPropertyChanged("YMinimum")
				End If
			End Set
		End Property
		
		<DefaultValue(3), Description("Gets or sets the source of the Minimum and Maximum Y values displayed"), Category("TramLine")> _
		Public Property YRangeSource As YRangeSources
			Get
				Return Me.myYRangeSource
			End Get
			Set(ByVal value As YRangeSources)
				If (Me.myYRangeSource <> value) Then
					Me.myYRangeSource = value
					Me.OnPropertyChanged("YRangeSource")
				End If
			End Set
		End Property
		
		Public Sub New()
			Me.Initialise(Color.Red)
		End Sub
		
		Public Sub New(ByVal lineColor As Color)
			Me.Initialise(lineColor)
		End Sub
		
		Public Function Draw(ByVal scales() As Scale, paperRect As Rectangle, ByVal g As Graphics, ByVal backColor As Color) As Boolean
			If Not Me.enabled Then
				Return False
			End If
			If (Me.myPoints.Count > 1) Then
			Try
				Dim index As Integer
				Dim num1, num2, num3, num4, num5, num6 As Double
				Dim myRawPoints As System.Drawing.Point() = New System.Drawing.Point(myPoints.Count()-1) {}
				Me.GetRanges(Scales, num3, num4, num5, num6)
				
				For index = 0 To (Me.myPoints.Count() - 1)
					num1 = ((CType(myPoints.Item(index).x, Double) - num3) / num4)
					num2 = ((CType(myPoints.Item(index).y, Double) - num5) / num6)
					myRawPoints(index).x = (paperRect.Left + CType((num1 * (paperRect.Width - 1)), Integer))
					myRawPoints(index).y = (paperRect.Top + CType((0.99999 + ((1 - num2) * (paperRect.Height - 1))), Integer))
					'MsgBox("index: " + index.ToString() + " x: " + myRawPoints(index).x.ToString() + " y: " + myRawPoints(index).y.ToString())
				Next
				Dim pen1 As New Pen(lineColor, CType(width, Single))
				'g.DrawLine(pen1,0,0,100,100)
				g.DrawLines(pen1, myRawPoints)
				pen1.Dispose()
				Return True
			Catch e As Exception
				MsgBox(e.ToString())
			End Try
			End If
			Return False
		End Function
		
		Private Sub GetRanges(ByVal scales() As Scale, ByRef XMin As Double, ByRef XRange As Double, ByRef YMin As Double, ByRef YRange As Double)
			Select Case Me.xRangeSource
				Case XRangeSources.TopScale
					XMin = Scales(2).Minimum
					XRange = (Scales(2).Maximum - XMin)
					Exit Select
				Case XRangeSources.BottomScale
					XMin = Scales(3).Minimum
					XRange = (Scales(3).Maximum - XMin)
					Exit Select
				Case Else
					XMin = Me.xMinimum
					XRange = (Me.xMaximum - Me.xMinimum)
					Exit Select
			End Select
			Select Case Me.yRangeSource
				Case YRangeSources.LeftScale
					YMin = Scales(0).Minimum
					YRange = (Scales(0).Maximum - YMin)
					Return
				Case YRangeSources.RightScale
					YMin = Scales(1).Minimum
					YRange = (Scales(1).Maximum - YMin)
					Return
			End Select
			YMin = Me.yMinimum
			YRange = (Me.yMaximum - Me.yMinimum)
		End Sub
			
		Private Sub Initialise(ByVal lineColor As Color)
			Me.myEnabled = True
			Me.myLineColor = lineColor
			Me.myWidth = 1
			Me.myXMinimum = 0
			Me.myXMaximum = 100
			Me.myXRangeSource = XRangeSources.BottomScale
			Me.myYMinimum = 0
			Me.myYMaximum = 100
			Me.myYRangeSource = YRangeSources.LeftScale
			Me.myPoints = New PointCollection()
			Me.propertyHdlr = New PropertyChangedEventHandler(AddressOf Me.OnChildPropertyChanged)
			AddHandler Me.myPoints.PropertyChanged, Me.propertyHdlr
		End Sub
		
		Private Sub OnChildPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
			Me.OnPropertyChanged(e.PropertyName)
		End Sub
		
		Private myEnabled As Boolean
		Private myLineColor As Color
		Private myPoints As PointCollection
		Private myWidth As Integer
		Private myXRangeSource As XRangeSources
		Private myXMaximum As Double
		Private myXMinimum As Double
		Private myYRangeSource As YRangeSources
		Private myYMaximum As Double
		Private myYMinimum As Double
		Private propertyHdlr As PropertyChangedEventHandler
	End Class

	<TypeConverter(GetType(XYTramLineCollection.XYTramLineCollConverter)), DefaultMember("Item")> _
	Public Class XYTramLineCollection
        Inherits Global.KineticaRT.Controls.SubObjectCollection

		Public Sub New()
			Me.AddTramLine(Color.Red)
		End Sub
		
		Public Sub New(ByVal tramLines() As XYTramLine)
			Me.AddRange(tramLines)
		End Sub
		
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public Property Item(ByVal Index As Integer) As XYTramLine
			Get
				Return CType(MyBase.List.Item(Index), XYTramLine)
			End Get
			Set (ByVal value As XYTramLine)
				If (Not value Is MyBase.List.Item(index)) Then
					MyBase.List.Item(index) = value
				End If
			End Set
		End Property			

		Public Sub AddRange(ByVal tramLines() As XYTramLine)
			Dim i As Integer
			
			For i = 0 To tramLines.Length - 1
				MyBase.List.Add(tramLines(i))
			Next i
		End Sub
		
		Public Sub AddTramLine(ByVal lineColor As Color)
			MyBase.List.Add(New XYTramLine(lineColor))
		End Sub
		
		Protected Friend Function Draw(ByVal scales As Scale(), paperRect As Rectangle, g As Graphics, backColor As Color) As Boolean
			Dim flag As Boolean = False
			Dim tramLine1 As XYTramLine
			
			For Each tramLine1 In MyBase.List
				If tramLine1.Draw(scales, paperRect, g, backColor) Then
					flag = True
				End If
			Next
			Return flag
		End Function
		
		'Nested Types
		Friend Class XYTramLineCollConverter
            Inherits Global.KineticaRT.Controls.ObjectTypeConverter
			
			Public Sub New()
			End Sub
			
			Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
				If (Not destinationType Is GetType(InstanceDescriptor)) Then
					Return MyBase.ConvertTo(context, culture, value, destinationType)
				End If
				Dim collection1 As XYTramLineCollection = CType(value, XYTramLineCollection)
				Dim tramLineArray1 As XYTramLine() = New XYTramLine(collection1.Count() -1) {}
				Dim typeArray1 As Type() = New Type() {GetType(XYTramLine())}
				Dim info1 As ConstructorInfo = GetType(XYTramLineCollection).GetConstructor(typeArray1)
				Dim num1 As Integer
				
				For num1 = 0 To collection1.Count - 1
					tramLineArray1(num1) = collection1.Item(num1)
				Next num1
				Dim objArray1 As Object() = New Object() { tramLineArray1 }
				Return New InstanceDescriptor(info1, objArray1)
			End Function
		End Class
	End Class

	<ToolboxBitmap(GetType(XYGraph)), DefaultProperty("Traces")> _	
	Public Class XYGraph
        Inherits Global.KineticaRT.Controls.XYGraph

		'<Description("Gets Or Sets the style of the Control's Border"), Category("Control"), DefaultValue(1)> _
		'Public Shadows Property BorderStyle As BorderStyles
		'	Get
		'		Dim parentControl As KineticaRT.Controls.Control
				
		'		parentControl = CType(Me, KineticaRT.Controls.Control)
		'		Return parentControl.borderStyle
		'	End Get
		'	Set(ByVal value As BorderStyles)
		'		MyBase.BorderStyle = value
		'	End Set
		'End Property		
		
		<Description("Gets or Sets the collection of CELXYTramLine objects"), Category("CELXYGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
		Public Property TramLines As XYTramLineCollection
			Get
				Return myTramLines
			End Get
			Set (ByVal value As XYTramLineCollection)
				If (Not Me.myTramLines Is Nothing) Then
					RemoveHandler Me.myTramLines.PropertyChanged, Me.propertyHdlr
				End If
				Me.myTramLines = value
				If (Not Me.myTramLines Is Nothing) Then
					AddHandler Me.myTramLines.PropertyChanged, Me.propertyHdlr
				End If
				Me.OnPropertyChanged("TramLines")
			End Set
		End Property

		Public Sub New()
			MyBase.New
			' Must be called for initialization
			Me.InitializeComponent
			Me.myTramLines = New XYTramLineCollection()
			Me.propertyHdlr = New PropertyChangedEventHandler(AddressOf Me.OnChildPropertyChanged)
			AddHandler Me.myTramLines.PropertyChanged, Me.propertyHdlr
		End Sub
		
		'Public Overrides Sub Connect()
		'	If (Not Me.traces Is Nothing) Then
		'		Me.traces.Connect
		'	End If
		'	MyBase.Connect
		'End Sub
		
		Public Overrides Sub Repaint(fullRedraw As Boolean, g As Graphics)
			Dim myScale(3) As Scale
			Dim paperRect As Rectangle
			
			MyBase.Repaint(fullRedraw, g)
			
			myScale(0) = Me.LeftScale
			myScale(1) = Me.RightScale
			myScale(2) = Me.TopScale
			myScale(3) = Me.BottomScale
			paperRect = Me.DisplayRectangle
			paperRect.X = (paperRect.X + Me.ScaleWidth)
			paperRect.Width = (paperRect.Width - (2 * Me.ScaleWidth))
			paperRect.Y = (paperRect.Y + Me.ScaleWidth)
			paperRect.Height = (paperRect.Height - (2 * Me.ScaleWidth))
			
			Me.myTramLines.Draw(myScale, paperRect, g, Me.BackColor)
		End Sub
		
		Private myTramLines As XYTramLineCollection
		Private propertyHdlr As PropertyChangedEventHandler

		#Region " Windows Forms Designer generated code "
		' This method is required for Windows Forms designer support.
		' Do not change the method contents inside the source code editor. The Forms designer might
		' not be able to load this method if it was changed manually.
		Private Sub InitializeComponent()
			'
			'UserControl1
			'
			Me.Name = "XYGraph"
		End Sub
		#End Region
		
	End Class
End Namespace

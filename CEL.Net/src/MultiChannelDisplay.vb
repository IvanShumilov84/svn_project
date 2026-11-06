'
' Created by SharpDevelop.
' User: logiciel
' Date: 12/22/2005
' Time: 1:22 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Imports KineticaRT.Studio.Controls
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace CEL.Controls

	Public Class MultiChannelDisplay
		Inherits System.Windows.Forms.UserControl
		
		Public Sub New()
			Me.InitializeComponent()
			Me.UpdateColumnSize()
        End Sub

        Private Sub multiChannelDisplay_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles _dataGrid.MouseDown
            Dim arincChannel As CEL.ARINCChannel
            Dim arincChannelCollection As CEL.ARINCChannelCollection = Me._dataGrid.DataSource

            If ((arincChannelCollection IsNot Nothing) AndAlso e.Button = Windows.Forms.MouseButtons.Left) Then
                Dim hitTestInfo As DataGrid.HitTestInfo = Me._dataGrid.HitTest(New System.Drawing.Point(e.X, e.Y))

                If (hitTestInfo IsNot Nothing AndAlso hitTestInfo.Row > -1) Then
                    Dim o As Object
                    o = hitTestInfo.Type

                    arincChannel = arincChannelCollection(hitTestInfo.Row())
                    DoDragDrop(arincChannel.Name, DragDropEffects.Copy Or DragDropEffects.Move)
                End If
            End If
        End Sub

        <Category("Control")> _
        Public Property BorderColor As System.Drawing.Color
            Get
                Return Me._valueStyle.BorderColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                Me._valueStyle.BorderColor = value
            End Set
        End Property

        <Category("Control"), DefaultValue(System.Windows.Forms.BorderStyle.None)> _
        Public Overloads Property BorderStyle() As System.Windows.Forms.BorderStyle
            Get
                Return Me._valueStyle.BorderStyle
            End Get
            Set(ByVal value As System.Windows.Forms.BorderStyle)
                Me._valueStyle.BorderStyle = value
            End Set
        End Property

        <Category("Control"), DefaultValue(1.0)> _
        Public Property BorderWidth As Single
            Get
                Return Me._valueStyle.BorderWidth
            End Get
            Set(ByVal value As Single)
                Me._valueStyle.Width = Convert.ToInt32(value)
            End Set
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
        Public Shadows ReadOnly Property Controls As ControlCollection
            Get
                Return MyBase.Controls
            End Get
        End Property

        <Category("Display"), DefaultValue(LayoutsDVU.DescriptionValueUnits)> _
        Public Property SectionLayout As LayoutsDVU
            Get
                Return Me._sectionLayout
            End Get
            Set(ByVal value As LayoutsDVU)
                If (value <> Me._sectionLayout) Then
                    Me._sectionLayout = value
                    Me.UpdateLayout()
                End If
            End Set
        End Property

        <Category("Display"), DefaultValue(15)> _
        Public Property UnitsPercent As Integer
            Get
                Return Me._unitsPercent
            End Get
            Set(ByVal value As Integer)
                If (Me._unitsPercent <> value) Then
                    Me._unitsPercent = value
                    Me.UpdateColumnSize()
                End If
            End Set
        End Property

        <Category("Display")> _
        Public Property ValueBackColor As System.Drawing.Color
            Get
                Return Me._valueStyle.BackColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                Me._valueStyle.BackColor = value
            End Set
        End Property

        <Category("Display")> _
        Public Property ValueColor As System.Drawing.Color
            Get
                Return Me._valueStyle.ForeColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                Me._valueStyle.ForeColor = value
            End Set
        End Property

        <Category("Display"), DefaultValue(25)> _
        Public Property ValuePercent As Integer
            Get
                Return Me._valuePercent
            End Get
            Set(ByVal value As Integer)
                If (value <> Me._valuePercent) Then
                    Me._valuePercent = value
                    Me.UpdateColumnSize()
                End If
            End Set
        End Property

        Public Sub SetDataBinding(ByVal value As ARINCChannelCollection)
            Me._dataGrid.SetDataBinding(value, "")
        End Sub

        Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
            MyBase.SuspendLayout()
            Me._tableStyle.AlternatingBackColor = Me.BackColor
            Me._tableStyle.BackColor = Me.BackColor
            Me._dataGrid.BackgroundColor = Me.BackColor
            Me._dataGrid.GridLineColor = Me.BackColor
            Me._tableStyle.GridLineColor = Me.BackColor
            MyBase.ResumeLayout()
            MyBase.OnBackColorChanged(e)
        End Sub

        Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
            Me._dataGrid.Font = Me.Font
            MyBase.OnFontChanged(e)
        End Sub

        Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
            Me._tableStyle.ForeColor = Me.ForeColor
            MyBase.OnForeColorChanged(e)
        End Sub

        Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
            MyBase.OnSizeChanged(e)
            Me.UpdateColumnSize()
        End Sub

        Private Sub InitializeComponent()
            Me._dataGrid = New DataGridEx
            Me._tableStyle = New DataGridTableStyle
            Me._descrStyle = New DataGridTextBoxColumnEx
            Me._valueStyle = New DataGridTextBoxColumnEx
            Me._unitsStyle = New DataGridTextBoxColumnEx
            MyBase.SuspendLayout()
            Me._tableStyle.AllowSorting = False
            Me._tableStyle.AlternatingBackColor = Me.BackColor
            Me._tableStyle.BackColor = Me.BackColor
            Me._dataGrid.BackgroundColor = Me.BackColor
            Me._dataGrid.BorderStyle = BorderStyle.None
            Me._dataGrid.CaptionVisible = False
            Me._dataGrid.ColumnHeadersVisible = False
            Me._tableStyle.ColumnHeadersVisible = False
            Me._dataGrid.Font = Me.Font
            Me._dataGrid.Dock = DockStyle.Fill
            Me._dataGrid.FlatMode = True
            Me._tableStyle.ForeColor = Me.ForeColor
            Me._dataGrid.GridLineColor = Me.BackColor
            Me._tableStyle.GridLineColor = Me.BackColor
            Me._tableStyle.GridLineStyle = DataGridLineStyle.Solid
            Me.Name = "MultiChannelDisplay"
            Me._tableStyle.ReadOnly = True
            Me._tableStyle.RowHeadersVisible = False
            Me._dataGrid.TabIndex = 0
            Me._tableStyle.MappingName = "ARINCChannelCollection"
            Me._descrStyle.MappingName = "Description"
            Me._valueStyle.Alignment = HorizontalAlignment.Center
            Me._valueStyle.ForeColor = System.Drawing.Color.White
            Me._valueStyle.MappingName = "Value"
            Me._unitsStyle.Alignment = HorizontalAlignment.Center
            Me._unitsStyle.MappingName = "Units"
            Me._tableStyle.GridColumnStyles.Add(Me._descrStyle)
            Me._tableStyle.GridColumnStyles.Add(Me._valueStyle)
            Me._tableStyle.GridColumnStyles.Add(Me._unitsStyle)
            Me._dataGrid.TableStyles.Clear()
            Me._dataGrid.TableStyles.Add(Me._tableStyle)
            MyBase.Controls.Add(Me._dataGrid)
            MyBase.ResumeLayout()
        End Sub

        Private Sub UpdateColumnSize()
            Dim clientSizeWidth As Integer = Math.Max(Me._dataGrid.ClientSize.Width - Me._dataGrid.VertScrollBarWidth, 5)

            Dim valueWidth As Integer = CType(((0.01 * Me._valuePercent) * clientSizeWidth), Integer)
            Dim unitsWidth As Integer = CType(((0.01 * Me._unitsPercent) * clientSizeWidth), Integer)
            Dim descrWidth As Integer = Math.Max((clientSizeWidth - valueWidth - unitsWidth), 5)

            MyBase.SuspendLayout()
            Me._descrStyle.Width = descrWidth
            Me._valueStyle.Width = valueWidth
            Me._unitsStyle.Width = unitsWidth
            MyBase.ResumeLayout()
        End Sub

        Private Sub UpdateLayout()
            MyBase.SuspendLayout()
            Me._tableStyle.GridColumnStyles.Clear()
            Select Case Me._sectionLayout
                Case LayoutsDVU.DescriptionUnitsValue
                    Me._tableStyle.GridColumnStyles.Add(Me._descrStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._unitsStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._valueStyle)
                Case LayoutsDVU.DescriptionValueUnits
                    Me._tableStyle.GridColumnStyles.Add(Me._descrStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._valueStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._unitsStyle)
                Case LayoutsDVU.UnitsDescriptionValue
                    Me._tableStyle.GridColumnStyles.Add(Me._unitsStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._descrStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._valueStyle)
                Case LayoutsDVU.UnitsValueDescription
                    Me._tableStyle.GridColumnStyles.Add(Me._unitsStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._valueStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._descrStyle)
                Case LayoutsDVU.ValueDescriptionUnits
                    Me._tableStyle.GridColumnStyles.Add(Me._valueStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._descrStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._unitsStyle)
                Case LayoutsDVU.ValueUnitsDescription
                    Me._tableStyle.GridColumnStyles.Add(Me._valueStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._unitsStyle)
                    Me._tableStyle.GridColumnStyles.Add(Me._descrStyle)
            End Select
            MyBase.ResumeLayout()
        End Sub

        Private WithEvents _dataGrid As DataGridEx
        Private _descrStyle As DataGridTextBoxColumnEx
        Private _sectionLayout As LayoutsDVU = LayoutsDVU.DescriptionValueUnits
        Private _tableStyle As DataGridTableStyle
        Private _valuePercent As Integer = 25
        Private _valueStyle As DataGridTextBoxColumnEx
        Private _unitsPercent As Integer = 15
        Private _unitsStyle As DataGridTextBoxColumnEx

    End Class
	
End Namespace

'
' Created by SharpDevelop.
' User: logiciel
' Date: 12/21/2005
' Time: 10:38 AM
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports CEL.Data.BusinessObjects

'ft 2012 
Imports KineticaRT.Channels
Imports KineticaRT.Values



Namespace CEL
    Public Class ARINCChannel

        'Private _krtChannel As INumericChannel
        Private _krtNumericChannel As INumericChannel
        Private _krtTextChannel As ITextChannel
        Private _krtBooleanChannel As IBooleanChannel
        Private _parent As ARINCChannelCollection

        Public Sub New(ByVal channel As INumericChannel)
            Me._krtNumericChannel = channel
        End Sub
        Public Sub New(ByVal channel As ITextChannel)
            Me._krtTextChannel = channel
        End Sub
        Public Sub New(ByVal channel As IBooleanChannel)
            Me._krtBooleanChannel = channel
        End Sub

        Public ReadOnly Property Name As String
            Get
                If _krtNumericChannel IsNot Nothing Then
                    Return Me._krtNumericChannel.Name
                ElseIf _krtTextChannel IsNot Nothing Then
                    Return Me._krtTextChannel.Name
                ElseIf _krtBooleanChannel IsNot Nothing Then
                    Return Me._krtBooleanChannel.Name
                Else
                    Return Nothing
                End If
            End Get
        End Property


        Public ReadOnly Property Description As String
            Get
                If _krtNumericChannel IsNot Nothing Then
                    Return Me._krtNumericChannel.Description
                ElseIf _krtTextChannel IsNot Nothing Then
                    Return Me._krtTextChannel.Description
                ElseIf _krtBooleanChannel IsNot Nothing Then
                    Return Me._krtBooleanChannel.Description
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property Value As String
            Get
                If _krtNumericChannel IsNot Nothing Then
                    Return Me._krtNumericChannel.Value.ToString(_krtNumericChannel.Format, Nothing)
                ElseIf _krtTextChannel IsNot Nothing Then
                    Return Me._krtTextChannel.Value.ToString()
                ElseIf _krtBooleanChannel IsNot Nothing Then
                    Return Me._krtBooleanChannel.Value.ToString()
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property Units As String
            Get
                If _krtNumericChannel IsNot Nothing Then
                    Return Me._krtNumericChannel.Units
                ElseIf _krtTextChannel IsNot Nothing Then
                    Return String.Empty
                ElseIf _krtBooleanChannel IsNot Nothing Then
                    Return String.Empty
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Protected Friend Property Parent As ARINCChannelCollection
            Get
                Return Me._parent
            End Get
            Set(ByVal value As ARINCChannelCollection)
                If Not (Me._parent Is value) Then
                    If Not (Me._parent Is Nothing) Then
                        If _krtNumericChannel IsNot Nothing Then
                            RemoveHandler Me._krtNumericChannel.PropertyChanged, AddressOf Me.OnPropertyChanged
                            RemoveHandler Me._krtNumericChannel.ValueChanged, AddressOf Me.OnValueChanged
                        ElseIf _krtTextChannel IsNot Nothing Then
                            RemoveHandler Me._krtTextChannel.PropertyChanged, AddressOf Me.OnPropertyChanged
                            RemoveHandler Me._krtTextChannel.ValueChanged, AddressOf Me.OnValueChanged
                        ElseIf _krtBooleanChannel IsNot Nothing Then
                            RemoveHandler Me._krtBooleanChannel.PropertyChanged, AddressOf Me.OnPropertyChanged
                            RemoveHandler Me._krtBooleanChannel.ValueChanged, AddressOf Me.OnValueChanged
                        End If
                    End If

                    If Not (value Is Nothing) Then
                        If _krtNumericChannel IsNot Nothing Then
                            AddHandler Me._krtNumericChannel.PropertyChanged, AddressOf Me.OnPropertyChanged
                            AddHandler Me._krtNumericChannel.ValueChanged, AddressOf Me.OnValueChanged
                        ElseIf _krtTextChannel IsNot Nothing Then
                            AddHandler Me._krtTextChannel.PropertyChanged, AddressOf Me.OnPropertyChanged
                            AddHandler Me._krtTextChannel.ValueChanged, AddressOf Me.OnValueChanged
                        ElseIf _krtBooleanChannel IsNot Nothing Then
                            AddHandler Me._krtBooleanChannel.PropertyChanged, AddressOf Me.OnPropertyChanged
                            AddHandler Me._krtBooleanChannel.ValueChanged, AddressOf Me.OnValueChanged
                        End If
                    End If
                        Me._parent = value
                    End If
            End Set
        End Property

        Private Sub OnPropertyChanged(sender As Object, ByVal e As PropertyChangedEventArgs)
            If Not (Me.Parent Is Nothing) Then
                If (e.PropertyName = "Description" OrElse e.PropertyName = "Units") Then
                    Me.Parent.ARINCChannelChanged(Me)
                End If
            End If
        End Sub

        Private Sub OnValueChanged(ByVal sender As IChannel, ByVal value As Value)
            If Not (Me.Parent Is Nothing) Then
                Me.Parent.ARINCChannelChanged(Me)
            End If
        End Sub

    End Class

    Public Class ARINCChannelCollection
        Inherits ComponentCollectionBase
        Implements IBindingList

        Private _resetEvent As New ListChangedEventArgs(ListChangedType.Reset, -1)
        Private _site As ISite = Nothing

        Default Public Property Item(ByVal index As Integer) As ARINCChannel
            Get
                Return CType(List(index), ARINCChannel)
            End Get
            Set(ByVal value As ARINCChannel)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As ARINCChannel) As Integer
            Return List.Add(value)
        End Function

        Protected Overridable Sub OnListChanged(ByVal ev As ListChangedEventArgs)
            RaiseEvent ListChanged(Me, ev)
        End Sub

        Protected Overrides Sub OnClear()
            Dim c As ARINCChannel
            For Each c In List
                c.Parent = Nothing
            Next
        End Sub

        Protected Overrides Sub OnClearComplete()
            OnListChanged(Me._resetEvent)
        End Sub

        Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
            Dim c As ARINCChannel = CType(value, ARINCChannel)
            c.Parent = Me
            OnListChanged(New ListChangedEventArgs(ListChangedType.ItemAdded, index))
        End Sub

        Protected Overrides Sub OnRemoveComplete(ByVal index As Integer, ByVal value As Object)
            Dim c As ARINCChannel = CType(value, ARINCChannel)
            c.Parent = Nothing
            OnListChanged(New ListChangedEventArgs(ListChangedType.ItemDeleted, index))
        End Sub

        Protected Overrides Sub OnSetComplete(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If (oldValue IsNot newValue) Then

                Dim oldChannel As ARINCChannel = CType(oldValue, ARINCChannel)
                Dim newChannel As ARINCChannel = CType(newValue, ARINCChannel)

                oldChannel.Parent = Nothing
                newChannel.Parent = Me

                OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, index))
            End If
        End Sub

        Friend Sub ARINCChannelChanged(ByVal channel As ARINCChannel)
            Dim index As Integer = List.IndexOf(channel)
            OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, index))
        End Sub

        ' Implements IBindingList

        ReadOnly Property AllowEdit() As Boolean Implements IBindingList.AllowEdit
            Get
                Return False
            End Get
        End Property

        ReadOnly Property AllowNew() As Boolean Implements IBindingList.AllowNew
            Get
                Return False
            End Get
        End Property

        ReadOnly Property AllowRemove() As Boolean Implements IBindingList.AllowRemove
            Get
                Return False
            End Get
        End Property

        ReadOnly Property SupportsChangeNotification() As Boolean Implements IBindingList.SupportsChangeNotification
            Get
                Return True
            End Get
        End Property

        ReadOnly Property SupportsSearching() As Boolean Implements IBindingList.SupportsSearching
            Get
                Return False
            End Get
        End Property

        ReadOnly Property SupportsSorting() As Boolean Implements IBindingList.SupportsSorting
            Get
                Return False
            End Get
        End Property

        ' Events.

        Public Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged

        ' Unsupported properties

        ReadOnly Property IsSorted() As Boolean Implements IBindingList.IsSorted
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        ReadOnly Property SortDirection() As ListSortDirection Implements IBindingList.SortDirection
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        ReadOnly Property SortProperty() As PropertyDescriptor Implements IBindingList.SortProperty
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        ' Unsupported methods

        Function AddNew() As Object Implements IBindingList.AddNew
            Throw New NotSupportedException()
        End Function

        Sub AddIndex(ByVal prop As PropertyDescriptor) Implements IBindingList.AddIndex
            Throw New NotSupportedException()
        End Sub

        Sub ApplySort(ByVal prop As PropertyDescriptor, ByVal direction As ListSortDirection) Implements IBindingList.ApplySort
            Throw New NotSupportedException()
        End Sub

        Function Find(ByVal prop As PropertyDescriptor, ByVal key As Object) As Integer Implements IBindingList.Find
            Throw New NotSupportedException()
        End Function

        Sub RemoveIndex(ByVal prop As PropertyDescriptor) Implements IBindingList.RemoveIndex
            Throw New NotSupportedException()
        End Sub

        Sub RemoveSort() Implements IBindingList.RemoveSort
            Throw New NotSupportedException()
        End Sub

    End Class

    Public Class ACMCChannel
        Private _discreteList As BitFieldCollection
        Private _krtChannel As IChannel
        Private _parent As ACMCChannelCollection
        Public ChannelName As String
        Public Sub New(ByVal channel As IChannel)
            Me._discreteList = New BitFieldCollection()
            Me._krtChannel = channel
            Me.ChannelName = channel.Name
        End Sub

        Public ReadOnly Property Codes As String
            Get
                Dim discrete As BitField
                Dim nbCodes As Integer = 0
                Dim result As String = ""

                For Each discrete In Me._discreteList
                    If (nbCodes > 0) Then
                        result += " "
                    End If
                    If discrete.Value <> 0 Then
                        result += discrete.Code.ToString
                    Else
                        result += "--"
                    End If
                    nbCodes += 1
                Next

                Return result
            End Get
        End Property

        Public ReadOnly Property ActiveFaultDescriptions As String()
            Get
                Dim discrete As BitField
                Dim faultDescs As New System.Collections.Specialized.StringCollection

                For Each discrete In Me._discreteList
                    If discrete.Value <> 0 Then
                        faultDescs.Add(discrete.Description)
                    End If
                Next

                Dim result(faultDescs.Count() - 1) As String
                faultDescs.CopyTo(result, 0)
                Return result
            End Get
        End Property

        Protected Friend Property Parent As ACMCChannelCollection
            Get
                Return Me._parent
            End Get
            Set(ByVal value As ACMCChannelCollection)
                If Not (Me._parent Is value) Then
                    If Not (Me._parent Is Nothing) Then
                        RemoveHandler Me._krtChannel.ValueChanged, AddressOf Me.OnKRTValueChangedHdlr
                    End If

                    If Not (value Is Nothing) Then
                        AddHandler Me._krtChannel.ValueChanged, AddressOf Me.OnKRTValueChangedHdlr
                    End If
                    Me._parent = value
                End If
            End Set
        End Property

        Public Sub AddBitField(startBit As Integer, length As Integer, code As ARINC429DiscreteTemplate.eDiscreteCode, description As String)
            Dim value As Long
            Dim newBitField As New BitField(CByte(startBit), CByte(length), code, description)

            Me._discreteList.Add(newBitField)
            Try
                value = CLng(Me._krtChannel.Value.ToDouble()) 'ft TODO: Ici newValue.ToLong() non implémenté... doit Rajouter une classe/object Values.Long  (du meme genre que Values.Double)
            Catch e As Exception
                value = 0
            End Try
            newBitField.Value = newBitField.GetFieldValue(value)
            If (Not Me._parent Is Nothing) Then
                Me._parent.ACMCChannelChanged(Me)
            End If
        End Sub
        Public Sub AddBitField(startBit As Integer?, length As Integer?, code As Dacn1Afdx.eCodingType, description As String)
            Dim value As Long
            Dim newBitField As New BitField(CByte(startBit), CByte(length), code, description)

            Me._discreteList.Add(newBitField)
            Try
                value = CLng(Me._krtChannel.Value.ToDouble()) 'ft TODO: Ici newValue.ToLong() non implémenté... doit Rajouter une classe/object Values.Long  (du meme genre que Values.Double)
            Catch e As Exception
                value = 0
            End Try
            newBitField.Value = newBitField.GetFieldValue(value)
            If (Not Me._parent Is Nothing) Then
                Me._parent.ACMCChannelChanged(Me)
            End If
        End Sub
        Public Sub AddBitField(startBit As Integer?, length As Integer?, code As OmniBusBoxUartDiscreteTemplate.eDiscreteCode, description As String)
            Dim value As Long
            Dim newBitField As New BitField(CByte(startBit), CByte(length), code, description)

            Me._discreteList.Add(newBitField)
            Try
                value = CLng(Me._krtChannel.Value.ToDouble()) 'ft TODO: Ici newValue.ToLong() non implémenté... doit Rajouter une classe/object Values.Long  (du meme genre que Values.Double)
            Catch e As Exception
                value = 0
            End Try
            newBitField.Value = newBitField.GetFieldValue(value)
            If (Not Me._parent Is Nothing) Then
                Me._parent.ACMCChannelChanged(Me)
            End If
        End Sub

        Private Sub OnKRTValueChangedHdlr(ByVal sender As IChannel, ByVal newValue As Value)
            Dim value As Long
            Dim discrete As BitField

            Try
                If Not newValue.ToString() = "Empty" And Not newValue.ToString() = "Not Connected" Then 'If Value is Not Connected or Empty it will throw an exception
                    value = CLng(newValue.ToDouble()) 'ft TODO: Ici newValue.ToLong() non implémenté... doit Rajouter une classe/object Values.Long  (du meme genre que Values.Double)
                Else
                    value = 0
                End If
            Catch e As Exception
                value = 0
            End Try

            For Each discrete In Me._discreteList
                discrete.Value = discrete.GetFieldValue(value)
            Next

            If (Not Me._parent Is Nothing) Then
                Me._parent.ACMCChannelChanged(Me)
            End If
        End Sub

        Private Class BitField
            Public Sub New(startBit As Byte, length As Byte, code As ARINC429DiscreteTemplate.eDiscreteCode, description As String)
                Dim count As Byte
                Dim baseMask As Long = (1 << (startBit - 1))
                Me._shift = CByte(startBit - 1)
                Me._code = code
                Me._description = description
                Me._mask = baseMask

                For count = 1 To CByte(length - 1)
                    Me._mask = ((Me._mask << 1) Or baseMask)
                Next
            End Sub

            Public ReadOnly Property Code As ARINC429DiscreteTemplate.eDiscreteCode
                Get
                    Return Me._code
                End Get
            End Property

            Public ReadOnly Property Description As String
                Get
                    Return Me._description
                End Get
            End Property

            Public Property Value As Long
                Get
                    Return Me._value
                End Get
                Set(newVal As Long)
                    If (newVal <> Me._value) Then
                        Me._value = newVal
                    End If
                End Set
            End Property

            Protected Friend Function GetFieldValue(word As Long) As Long
                Return ((word And Me._mask) >> Me._shift)
            End Function

            Private _mask As Long
            Private _shift As Byte
            Private _code As ARINC429DiscreteTemplate.eDiscreteCode
            Private _description As String
            Private _value As Long
        End Class

        Private Class BitFieldCollection
            Inherits System.Collections.CollectionBase

            Public Property Item(ByVal index As Integer) As BitField
                Get
                    Return CType(MyBase.List.Item(index), BitField)
                End Get
                Set(ByVal value As BitField)
                    If (Not value Is MyBase.List.Item(index)) Then
                        MyBase.List.Item(index) = value
                    End If
                End Set
            End Property

            Public Sub Add(ByVal value As BitField)
                List.Add(value)
            End Sub
        End Class
    End Class

    Public Class ACMCChannelCollection
        Inherits ComponentCollectionBase
        Implements IBindingList

        Private _resetEvent As New ListChangedEventArgs(ListChangedType.Reset, -1)
        Private _site As ISite = Nothing

        Default Public Property Item(ByVal index As Integer) As ACMCChannel
            Get
                Return CType(List(index), ACMCChannel)
            End Get
            Set(ByVal value As ACMCChannel)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As ACMCChannel) As Integer
            Return List.Add(value)
        End Function

        Protected Overridable Sub OnListChanged(ByVal ev As ListChangedEventArgs)
            RaiseEvent ListChanged(Me, ev)
        End Sub

        Protected Overrides Sub OnClear()
            Dim c As ACMCChannel
            For Each c In List
                c.Parent = Nothing
            Next
        End Sub

        Protected Overrides Sub OnClearComplete()
            OnListChanged(Me._resetEvent)
        End Sub

        Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
            Dim c As ACMCChannel = CType(value, ACMCChannel)
            c.Parent = Me
            OnListChanged(New ListChangedEventArgs(ListChangedType.ItemAdded, index))
        End Sub

        Protected Overrides Sub OnRemoveComplete(ByVal index As Integer, ByVal value As Object)
            Dim c As ACMCChannel = CType(value, ACMCChannel)
            c.Parent = Nothing
            OnListChanged(New ListChangedEventArgs(ListChangedType.ItemDeleted, index))
        End Sub

        Protected Overrides Sub OnSetComplete(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If (oldValue IsNot newValue) Then

                Dim oldChannel As ACMCChannel = CType(oldValue, ACMCChannel)
                Dim newChannel As ACMCChannel = CType(newValue, ACMCChannel)

                oldChannel.Parent = Nothing
                newChannel.Parent = Me

                OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, index))
            End If
        End Sub

        Friend Sub ACMCChannelChanged(ByVal channel As ACMCChannel)
            Dim index As Integer = List.IndexOf(channel)
            OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, index))
        End Sub

        ' Implements IBindingList

        ReadOnly Property AllowEdit() As Boolean Implements IBindingList.AllowEdit
            Get
                Return False
            End Get
        End Property

        ReadOnly Property AllowNew() As Boolean Implements IBindingList.AllowNew
            Get
                Return False
            End Get
        End Property

        ReadOnly Property AllowRemove() As Boolean Implements IBindingList.AllowRemove
            Get
                Return False
            End Get
        End Property

        ReadOnly Property SupportsChangeNotification() As Boolean Implements IBindingList.SupportsChangeNotification
            Get
                Return True
            End Get
        End Property

        ReadOnly Property SupportsSearching() As Boolean Implements IBindingList.SupportsSearching
            Get
                Return False
            End Get
        End Property

        ReadOnly Property SupportsSorting() As Boolean Implements IBindingList.SupportsSorting
            Get
                Return False
            End Get
        End Property

        ' Events.

        Public Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged

        ' Unsupported properties

        ReadOnly Property IsSorted() As Boolean Implements IBindingList.IsSorted
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        ReadOnly Property SortDirection() As ListSortDirection Implements IBindingList.SortDirection
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        ReadOnly Property SortProperty() As PropertyDescriptor Implements IBindingList.SortProperty
            Get
                Throw New NotSupportedException()
            End Get
        End Property

        ' Unsupported methods

        Function AddNew() As Object Implements IBindingList.AddNew
            Throw New NotSupportedException()
        End Function

        Sub AddIndex(ByVal prop As PropertyDescriptor) Implements IBindingList.AddIndex
            Throw New NotSupportedException()
        End Sub

        Sub ApplySort(ByVal prop As PropertyDescriptor, ByVal direction As ListSortDirection) Implements IBindingList.ApplySort
            Throw New NotSupportedException()
        End Sub

        Function Find(ByVal prop As PropertyDescriptor, ByVal key As Object) As Integer Implements IBindingList.Find
            Throw New NotSupportedException()
        End Function

        Sub RemoveIndex(ByVal prop As PropertyDescriptor) Implements IBindingList.RemoveIndex
            Throw New NotSupportedException()
        End Sub

        Sub RemoveSort() Implements IBindingList.RemoveSort
            Throw New NotSupportedException()
        End Sub

    End Class
End Namespace


'
' Created by SharpDevelop.
' User: logiciel
' Date: 12/21/2005
' Time: 2:35 PM
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'

Imports System
Imports System.Collections
Imports System.ComponentModel

Namespace CEL
	MustInherit Public Class ComponentCollectionBase
		Inherits Component
		Implements IList, ICollection, IEnumerable

		Protected Sub New()
		End Sub

		Public Sub Clear() Implements IList.Clear
			Me.OnClear
			Me.InnerList.Clear
			Me.OnClearComplete
		End Sub
		
		Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return Me.InnerList.GetEnumerator
		End Function
		
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
			If (Not Me._disposed) Then
				Try
					If (disposing) Then
						Me.List.Clear()
					End If
					Me._disposed = True
				Finally
					MyBase.Dispose(disposing)
				End Try
			End If
		End Sub
		
		Protected Overridable Sub OnClear()
		End Sub

		Protected Overridable Sub OnClearComplete()
		End Sub

		Protected Overridable Sub OnInsert(ByVal index As Integer, ByVal value As Object)
		End Sub

		Protected Overridable Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
		End Sub

		Protected Overridable Sub OnRemove(ByVal index As Integer, ByVal value As Object)
		End Sub

		Protected Overridable Sub OnRemoveComplete(ByVal Index As Integer, ByVal value As Object)
		End Sub
		
		Protected Overridable Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
		End Sub
		
		Protected Overridable Sub OnSetComplete(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
		End Sub
		
		Protected Overridable Sub OnValidate(ByVal value As Object)
			If (value Is Nothing) Then
				Throw New ArgumentNullException("value")
			End If
		End Sub

		Public Sub RemoveAt(ByVal index As Integer) Implements IList.RemoveAt
			If ((index < 0) OrElse (index >= Me.InnerList.Count)) Then
				Throw New ArgumentOutOfRangeException("index")
			End If
			Dim obj1 As Object = Me.InnerList.Item(index)
			Me.OnValidate(obj1)
			Me.OnRemove(index, obj1)
			Me.InnerList.RemoveAt(index)
			Me.OnRemoveComplete(index, obj1)
		End Sub

		Private Sub CopyTo(ByVal dstArray As Array, ByVal index As Integer) Implements ICollection.CopyTo
			Me.InnerList.CopyTo(dstArray, index)
		End Sub

		Private Function Add(ByVal value As Object) As Integer Implements IList.Add
			Me.OnValidate(value)
			Me.OnInsert(Me.InnerList.Count, value)
			Dim num1 As Integer = Me.InnerList.Add(value)
			Try
				Me.OnInsertComplete(num1, value)
			Catch exception1 As Exception
				Me.InnerList.RemoveAt(num1)
				Throw
			End Try
			Return num1
		End Function

		Private Function Contains(ByVal value As Object) As Boolean Implements IList.Contains
			Return Me.InnerList.Contains(value)
		End Function

		Private Function IndexOf(ByVal value As Object) As Integer Implements IList.IndexOf
			Return Me.InnerList.IndexOf(value)
		End Function

		Private Sub Insert(ByVal index As Integer, ByVal value As Object) Implements IList.Insert
			If ((index < 0) OrElse (index > Me.InnerList.Count)) Then
				Throw New ArgumentOutOfRangeException("index")
			End If
			Me.OnValidate(value)
			Me.OnInsert(index, value)
			Me.InnerList.Insert(index, value)
			Try
				Me.OnInsertComplete(index, value)
			Catch exception1 As Exception
				Me.InnerList.RemoveAt(index)
				Throw
			End Try
		End Sub

		Private Sub Remove(ByVal value As Object) Implements IList.Remove
			Me.OnValidate(value)
			Dim num1 As Integer = Me.InnerList.IndexOf(value)
			If (num1 < 0) Then
				Throw New ArgumentException("Arg_RemoveArgNotFound")
			End If
			Me.OnRemove(num1, value)
			Me.InnerList.RemoveAt(num1)
			Me.OnRemoveComplete(num1, value)
		End Sub

		Public ReadOnly Property Count As Integer Implements ICollection.Count
			Get
				If (Not Me._list Is Nothing) Then
					Return Me._list.Count
				End If
				Return 0
			End Get
		End Property
		
		Protected ReadOnly Property InnerList As ArrayList
			Get
				If (Me._list Is Nothing) Then
					Me._list = New ArrayList()
				End If
				Return Me._list
			End Get
		End Property

		Protected ReadOnly Property List As IList
			Get
				Return Me
			End Get
		End Property

		Private ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
			Get
				Return Me.InnerList.IsSynchronized
			End Get
		End Property

		Private ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
			Get
				Return Me.InnerList.SyncRoot
			End Get
		End Property

		Private ReadOnly Property IsFixedSize As Boolean Implements IList.IsFixedSize
			Get
				Return Me.InnerList.IsFixedSize
			End Get
		End Property

		Private ReadOnly Property IsReadOnly As Boolean Implements IList.IsReadOnly
			Get
				Return Me.InnerList.IsReadOnly
			End Get
		End Property

		Private Property Item(index As Integer) As Object Implements IList.Item
			Get
				If ((index < 0) OrElse (index >= Me.InnerList.Count)) Then
					Throw New ArgumentOutOfRangeException("index")
				End If
				Return Me.InnerList.Item(index)
			End Get
			Set(ByVal value As Object)
				If ((index < 0) OrElse (index >= Me.InnerList.Count)) Then
					Throw New ArgumentOutOfRangeException("index")
				End If
				Me.OnValidate(value)
				Dim obj1 As Object = Me.InnerList.Item(index)
				Me.OnSet(index, obj1, value)
				Me.InnerList.Item(index) = value
				Try
					Me.OnSetComplete(index, obj1, value)
				Catch exception1 As Exception
					Me.InnerList.Item(index) = obj1
					Throw
				End Try
			End Set
		End Property

		Private _disposed As Boolean = False
		Private _list As ArrayList
	End Class
End Namespace

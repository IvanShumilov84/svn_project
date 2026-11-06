Imports System.Data
Imports System.Windows.Forms
Imports System.Collections

Public Class CELResultListViewForm

    Public Sub New(ByVal windowTitle As String, ByVal message As String, ByVal dataset As DataSet, ByVal columnsWidth() As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        InitializeListView()

        lblMessage.Text = message
        Me.Text = windowTitle
        FillListViewWithDataset(dataset)
        SetColumnsSize(columnsWidth)
    End Sub

    Private Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        Me.Close()
    End Sub

    Private Sub FillListViewWithDataset(ByVal ds As DataSet)
        For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
            lstViewResult.Columns.Add(ds.Tables(0).Columns(i).ToString())
        Next
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim listRow As New ListViewItem
            listRow.Text = ds.Tables(0).Rows(i)(0).ToString()
            For j As Integer = 1 To ds.Tables(0).Columns.Count - 1
                listRow.SubItems.Add(ds.Tables(0).Rows(i)(j).ToString())
            Next
            lstViewResult.Items.Add(listRow)
        Next

    End Sub
    Private Sub SetColumnsSize(ByVal columnsWidth() As Integer)
        For i As Integer = 0 To columnsWidth.GetLength(0) - 1
            lstViewResult.Columns(i).Width = columnsWidth(i)
        Next
    End Sub

    Private _lvwItemComparer As ListViewItemComparer
    'Initialize ListView
    Private Sub InitializeListView()

        ' Set the view to show details.
        lstViewResult.View = View.Details

        ' Allow the user to edit item text.
        lstViewResult.LabelEdit = True

        ' Allow the user to rearrange columns.
        lstViewResult.AllowColumnReorder = True

        ' Select the item and subitems when selection is made.
        lstViewResult.FullRowSelect = True

        ' Display grid lines.
        lstViewResult.GridLines = True

        'Sort the items in the list in ascending order.
        lstViewResult.Sorting = SortOrder.Ascending

        ' Attach Subitems to the ListView
        ''lstViewResult.Columns.Add("Title", 300, HorizontalAlignment.Left)
        ''lstViewResult.Columns.Add("ID", 70, HorizontalAlignment.Left)
        ''lstViewResult.Columns.Add("Price", 70, HorizontalAlignment.Left)
        ''lstViewResult.Columns.Add("Publi-Date", 100, HorizontalAlignment.Left)

        ' The ListViewItemSorter property allows you to specify the
        ' object that performs the sorting of items in the ListView.
        ' You can use the ListViewItemSorter property in combination
        ' with the Sort method to perform custom sorting.
        _lvwItemComparer = New ListViewItemComparer()
        Me.lstViewResult.ListViewItemSorter = _lvwItemComparer
    End Sub
    ' Perform Sorting on Column Headers
    Private Sub lstViewResult_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs)

        ' Determine if clicked column is already the column that is being sorted.
        If e.Column = _lvwItemComparer.SortColumn Then
            ' Reverse the current sort direction for this column.
            If _lvwItemComparer.Order = SortOrder.Ascending Then
                _lvwItemComparer.Order = SortOrder.Descending
            Else
                _lvwItemComparer.Order = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending.
            _lvwItemComparer.SortColumn = e.Column
            _lvwItemComparer.Order = SortOrder.Ascending
        End If

        ' Perform the sort with these new sort options.
        Me.lstViewResult.Sort()
    End Sub


    ' This class is an implementation of the 'IComparer' interface.
    Public Class ListViewItemComparer
        Implements IComparer
        ' Specifies the column to be sorted
        Private ColumnToSort As Integer

        ' Specifies the order in which to sort (i.e. 'Ascending').
        Private OrderOfSort As SortOrder

        ' Case insensitive comparer object
        Private ObjectCompare As CaseInsensitiveComparer

        ' Class constructor, initializes various elements
        Public Sub New()
            ' Initialize the column to '0'
            ColumnToSort = 0

            ' Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None

            ' Initialize the CaseInsensitiveComparer object
            ObjectCompare = New CaseInsensitiveComparer()
        End Sub

        ' This method is inherited from the IComparer interface.
        ' It compares the two objects passed using a case
        ' insensitive comparison.
        '
        ' x: First object to be compared
        ' y: Second object to be compared
        '
        ' The result of the comparison. "0" if equal,
        ' negative if 'x' is less than 'y' and
        ' positive if 'x' is greater than 'y'
        Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
            Dim compareResult As Integer
            Dim listviewX As ListViewItem, listviewY As ListViewItem

            ' Cast the objects to be compared to ListViewItem objects
            listviewX = DirectCast(x, ListViewItem)
            listviewY = DirectCast(y, ListViewItem)

            ' Case insensitive Compare
            compareResult = ObjectCompare.Compare(listviewX.SubItems(ColumnToSort).Text, listviewY.SubItems(ColumnToSort).Text)

            ' Calculate correct return value based on object comparison
            If OrderOfSort = SortOrder.Ascending Then
                ' Ascending sort is selected, return normal result of compare operation
                Return compareResult
            ElseIf OrderOfSort = SortOrder.Descending Then
                ' Descending sort is selected, return negative result of compare operation
                Return (-compareResult)
            Else
                ' Return '0' to indicate they are equal
                Return 0
            End If
        End Function

        ' Gets or sets the number of the column to which to
        ' apply the sorting operation (Defaults to '0').
        Public Property SortColumn() As Integer
            Get
                Return ColumnToSort
            End Get
            Set(value As Integer)
                ColumnToSort = value
            End Set
        End Property

        ' Gets or sets the order of sorting to apply
        ' (for example, 'Ascending' or 'Descending').
        Public Property Order() As SortOrder
            Get
                Return OrderOfSort
            End Get
            Set(value As SortOrder)
                OrderOfSort = value
            End Set
        End Property
    End Class
    ''Simple String Sorting
    'Public Function Compare(x As Object, y As Object) As Integer
    '    Dim compareResult As Integer
    '    Dim listviewX As ListViewItem, listviewY As ListViewItem

    '    ' Cast the objects to be compared to ListViewItem objects
    '    listviewX = DirectCast(x, ListViewItem)
    '    listviewY = DirectCast(y, ListViewItem)

    '    ' Simple String Compare
    '    compareResult = String.Compare(listviewX.SubItems(ColumnToSort).Text, listviewY.SubItems(ColumnToSort).Text)

    '    ' Calculate correct return value based on object comparison
    '    If OrderOfSort = SortOrder.Ascending Then
    '        ' Ascending sort is selected, return normal result of compare operation
    '        Return compareResult
    '    ElseIf OrderOfSort = SortOrder.Descending Then
    '        ' Descending sort is selected, return negative result of compare operation
    '        Return (-compareResult)
    '    Else
    '        ' Return '0' to indicate they are equal
    '        Return 0
    '    End If
    'End Function
    ''Sorting Dates
    'Public Function Compare(x As Object, y As Object) As Integer
    '    Dim compareResult As Integer
    '    Dim listviewX As ListViewItem, listviewY As ListViewItem

    '    ' Cast the objects to be compared to ListViewItem objects
    '    listviewX = DirectCast(x, ListViewItem)
    '    listviewY = DirectCast(y, ListViewItem)

    '    ' Determine whether the type being compared is a date type.
    '    Try
    '        ' Parse the two objects passed as a parameter as a DateTime.
    '        Dim firstDate As System.DateTime = DateTime.Parse(listviewX.SubItems(ColumnToSort).Text)
    '        Dim secondDate As System.DateTime = DateTime.Parse(listviewY.SubItems(ColumnToSort).Text)

    '        ' Compare the two dates.
    '        compareResult = DateTime.Compare(firstDate, secondDate)

    '        ' If neither compared object has a valid date format,
    '        ' perform a Case Insensitive Sort
    '    Catch
    '        ' Case Insensitive Compare
    '        compareResult = ObjectCompare.Compare(listviewX.SubItems(ColumnToSort).Text, listviewY.SubItems(ColumnToSort).Text)
    '    End Try

    '    ' Calculate correct return value based on object comparison
    '    If OrderOfSort = SortOrder.Ascending Then
    '        ' Ascending sort is selected, return normal result of compare operation
    '        Return compareResult
    '    ElseIf OrderOfSort = SortOrder.Descending Then
    '        ' Descending sort is selected, return negative result of compare operation
    '        Return (-compareResult)
    '    Else
    '        ' Return '0' to indicate they are equal
    '        Return 0
    '    End If
    'End Function

  
End Class
Imports CEL.STM
Imports System.Text
Imports NUnit.Framework

<TestFixture()> Public Class STMConvertNUnitTests

    <Test()> Public Sub ByteListFlattenFalse()
        Dim bytes As List(Of Byte) = New List(Of Byte)

        Assert.NotNull(bytes)
        Convert.Flatten(False, bytes)

        Dim length As Integer = bytes.Count
        Assert.AreEqual(1, length, 0.0, "Boolean not flattened correctly: Byte List is too long")

        Assert.AreEqual(0, bytes(0), 0.0, "Boolean not flattened correctly: Wrong value")
    End Sub

    <Test()> Public Sub ByteListFlattenTrue()
        Dim bytes As List(Of Byte) = New List(Of Byte)

        Assert.NotNull(bytes)
        Convert.Flatten(True, bytes)

        Dim length As Integer = bytes.Count
        Assert.AreEqual(1, length, 0.0, "Boolean not flattened correctly: Byte List is too long")

        Assert.AreEqual(1, bytes(0), 0.0, "Boolean not flattened correctly: Wrong value")
    End Sub

    <Test()> Public Sub ByteSegmentListFlattenFalse()
        Dim bytes As Generic.List(Of ArraySegment(Of Byte)) = New List(Of ArraySegment(Of Byte))

        Assert.NotNull(bytes)
        Convert.Flatten(False, bytes)

        Dim length As Integer = bytes.Count
        Assert.AreEqual(1, length, 0.0, "Boolean not flattened correctly: Segment of Bytes List is too long")

        length = bytes(0).Array.Length
        Assert.AreEqual(1, length, 0.0, "Boolean not flattened correctly: Byte Segment is too long")

        Assert.AreEqual(0, bytes(0)(0), 0.0, "Boolean not flattened correctly: Wrong value")
    End Sub

    <Test()> Public Sub ByteSegmentListFlattenTrue()
        Dim bytes As Generic.List(Of ArraySegment(Of Byte)) = New List(Of ArraySegment(Of Byte))

        Assert.NotNull(bytes)
        Convert.Flatten(True, bytes)

        Dim length As Integer = bytes.Count
        Assert.AreEqual(1, length, 0.0, "Boolean not flattened correctly: Segment of Bytes List is too long")

        length = bytes(0).Array.Length
        Assert.AreEqual(1, length, 0.0, "Boolean not flattened correctly: Byte Segment is too long")

        Assert.AreEqual(1, bytes(0)(0), 0.0, "Boolean not flattened correctly: Wrong value")
    End Sub
End Class
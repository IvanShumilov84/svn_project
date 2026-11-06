'
' STMConvert.vb
'
' LabVIEW STM communication implementation for .Net.
' Based on a Reference Design from the NI System Engineering Group.
' http://www.ni.com/example/31208/en/
' Ported from C++ to Vb.Net by Danny Lepage 
' lepage dot danny at cel dash aerospace dot ca
'
' Copyright (c) 2014 CEL Aerospace Test Equipment LTD.
' Copyright (c) 2014 Dallas Airmotive Inc.
'
' TODO
' Change all functions to pass position ByRef so caller doesn't need to understand
' data representation to property parse the stream.

Imports System.Runtime.InteropServices

Namespace STM


    ''' <summary>
    ''' Utility class to perform flattening to/unflattening from stream.
    ''' Equivalent of LabVIEW FlattenToString and UnflattenFromString.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Convert

        ''' <summary>
        ''' Flatten a boolean value to "string".
        ''' </summary>
        ''' <param name="val"><see cref="System.Boolean">Boolean</see> value to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val As Boolean, data As System.Collections.Generic.List(Of Byte))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            data.AddRange(bytes)
        End Sub


        Public Shared Sub Flatten(val As Boolean, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            data.Add(New System.ArraySegment(Of Byte)(bytes))
        End Sub


        ''' <summary>
        ''' Flatten a double array to "string".
        ''' </summary>
        ''' <param name="val">Array of <see cref="System.Double">Double</see> values to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val() As Double, data As System.Collections.Generic.List(Of Byte))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        Public Shared Sub Flatten(val() As Double, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        ''' <summary>
        ''' Flatten a double to "string".
        ''' </summary>
        ''' <param name="val"><see cref="System.Double">Double</see> value to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val As Double, data As System.Collections.Generic.List(Of Byte))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.AddRange(bytes)
        End Sub


        Public Shared Sub Flatten(val As Double, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.Add(New System.ArraySegment(Of Byte)(bytes))
        End Sub


        ''' <summary>
        ''' Flatten an array of Int32 to "string".
        ''' </summary>
        ''' <param name="val">Array of <see cref="System.Int32">Int32</see> values to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val() As Int32, data As System.Collections.Generic.List(Of Byte))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        Public Shared Sub Flatten(val() As Int32, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        ''' <summary>
        ''' Flatten a Int32 to "string".
        ''' </summary>
        ''' <param name="val"><see cref="System.Int32">Int32</see> value to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val As Int32, data As System.Collections.Generic.List(Of Byte))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.AddRange(bytes)
        End Sub


        ''' <summary>
        ''' Flatten a Int32 to "string". 
        ''' </summary>
        ''' <param name="val"><see cref="System.Int32">Int32</see> value to flatten.</param>
        ''' <param name="data">A buffer set of <see cref="System.ArraySegment(Of T)">ArraySegment(Of T)</see>s of type <see cref="System.Byte">Byte</see> where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val As Int32, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.Add(New System.ArraySegment(Of Byte)(bytes))
        End Sub


        ''' <summary>
        ''' Flatten an array of Single to "string".
        ''' </summary>
        ''' <param name="val">Array of <see cref="System.Single">Single</see> values to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val() As Single, data As System.Collections.Generic.List(Of Byte))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        Public Shared Sub Flatten(val() As Single, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        ''' <summary>
        ''' Flatten a Single value to "string".
        ''' </summary>
        ''' <param name="val"><see cref="System.Single">Single</see> value to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val As Single, data As System.Collections.Generic.List(Of Byte))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.AddRange(bytes)
        End Sub


        Public Shared Sub Flatten(val As Single, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.Add(New System.ArraySegment(Of Byte)(bytes))
        End Sub


        ''' <summary>
        ''' Flatten an Array of Int16 to "string".
        ''' </summary>
        ''' <param name="val">Array of <see cref="System.Int16">Int16</see> values to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val() As Int16, data As System.Collections.Generic.List(Of Byte))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        Public Shared Sub Flatten(val() As Int16, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        ''' <summary>
        ''' Flatten an Int16 to "string".
        ''' </summary>
        ''' <param name="val"><see cref="System.Single">Int16</see> value to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val As Int16, data As System.Collections.Generic.List(Of Byte))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.AddRange(bytes)
        End Sub


        Public Shared Sub Flatten(val As Int16, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.Add(New System.ArraySegment(Of Byte)(bytes))
        End Sub


        ''' <summary>
        ''' Flatten an array of UInt32 to "string".
        ''' </summary>
        ''' <param name="val">Array of <see cref="System.UInt32">UInt32</see> values to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val() As UInt32, data As System.Collections.Generic.List(Of Byte))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        Public Shared Sub Flatten(val() As UInt32, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        ''' <summary>
        ''' Flatten an UInt32 to "string".
        ''' </summary>
        ''' <param name="val"><see cref="System.UInt32">UInt32</see> value to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val As UInt32, data As System.Collections.Generic.List(Of Byte))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.AddRange(bytes)
        End Sub


        Public Shared Sub Flatten(val As UInt32, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.Add(New System.ArraySegment(Of Byte)(bytes))
        End Sub


        ''' <summary>
        ''' Flatten an array of UInt16 to "string".
        ''' </summary>
        ''' <param name="val">Array of <see cref="System.UInt16">UInt16</see> values to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val() As UInt16, data As System.Collections.Generic.List(Of Byte))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        Public Shared Sub Flatten(val() As UInt16, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub


        ''' <summary>
        ''' Flatten an UInt16 to "string".
        ''' </summary>
        ''' <param name="val"><see cref="System.UInt16">UInt16</see> value to flatten.</param>
        ''' <param name="data">Destination <see cref="System.Byte">byte</see> buffer where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val As UInt16, data As System.Collections.Generic.List(Of Byte))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.AddRange(bytes)
        End Sub


        ''' <summary>
        ''' Flatten an UInt16 to "string".
        ''' </summary>
        ''' <param name="val"><see cref="System.UInt16">UInt16</see> value to flatten.</param>
        ''' <param name="data">A buffer set of <see cref="System.ArraySegment(Of T)">ArraySegment(Of T)</see>s of type <see cref="System.Byte">Byte</see> where the flatten value will be appended.</param>
        ''' <remarks></remarks>
        Public Shared Sub Flatten(val As UInt16, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim bytes() As Byte = BitConverter.GetBytes(val)

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            data.Add(New System.ArraySegment(Of Byte)(bytes))
        End Sub

        Public Shared Sub Flatten(val As String(), data As System.Collections.Generic.List(Of Byte))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub

        Public Shared Sub Flatten(val As String(), data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim arraySize = val.GetLength(0)

            Flatten(arraySize, data)

            For index As Integer = 0 To arraySize - 1
                Flatten(val(index), data)
            Next
        End Sub

        Public Shared Sub Flatten(val As String, data As System.Collections.Generic.List(Of Byte))
            Dim bytes() As Byte = System.Text.Encoding.ASCII.GetBytes(val)

            Dim arraySize = bytes.GetLength(0)

            Flatten(arraySize, data)
            data.AddRange(bytes)
        End Sub

        Public Shared Sub Flatten(val As String, data As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))
            Dim bytes() As Byte = System.Text.Encoding.ASCII.GetBytes(val)

            Dim arraySize = bytes.GetLength(0)

            Flatten(arraySize, data)
            data.Add(New System.ArraySegment(Of Byte)(bytes))
        End Sub

        ''' <summary>
        ''' Unflatten a boolean from a "string".
        ''' </summary>
        ''' <param name="data"></param>
        ''' <param name="position"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function BoolUnflatten(data As System.Collections.Generic.List(Of Byte), position As Integer) As Boolean
            Dim bytes() As Byte = data.GetRange(position, Marshal.SizeOf(Byte.MaxValue.GetType)).ToArray

            Return BitConverter.ToBoolean(bytes, 0)
        End Function

        Public Shared Function ByteUnflatten(data As System.Collections.Generic.List(Of Byte), position As Integer) As Byte
            Return data(position)
        End Function

        Public Shared Function Dbl2dArrUnflatten(data As System.Collections.Generic.List(Of Byte), ByRef position As Integer) As Double(,)
            Dim val(,) As Double
            Dim xSize As UInteger = UIntUnflatten(data, position)
            position += Marshal.SizeOf(UInteger.MaxValue.GetType)
            Dim ySize As UInteger = UIntUnflatten(data, position)
            position += Marshal.SizeOf(UInteger.MaxValue.GetType)

            ReDim val(CInt(xSize - 1), CInt(ySize - 1))

            For x As Integer = 0 To CInt(xSize - 1)
                For y As Integer = 0 To CInt(ySize - 1)
                    val(x, y) = DblUnflatten(data, position)
                    position += Marshal.SizeOf(Double.MaxValue.GetType)
                Next
            Next

            Return val
        End Function

        Public Shared Function DblArrUnflatten(data As System.Collections.Generic.List(Of Byte), ByRef position As Integer) As Double()
            Dim val() As Double
            Dim arraySize As Integer = IntUnflatten(data, position)
            position += Marshal.SizeOf(Integer.MaxValue.GetType)

            ReDim val(arraySize - 1)

            For index As Integer = 0 To arraySize - 1
                val(index) = DblUnflatten(data, position)
                position += Marshal.SizeOf(Double.MaxValue.GetType)
            Next

            Return val
        End Function

        Public Shared Function DblUnflatten(data As System.Collections.Generic.List(Of Byte), position As Integer) As Double
            Dim bytes() As Byte = data.GetRange(position, Marshal.SizeOf(Double.MaxValue.GetType)).ToArray

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            Return BitConverter.ToDouble(bytes, 0)
        End Function

        Public Shared Function IntUnflatten(data As System.Collections.Generic.List(Of Byte), position As Integer) As Int32
            Dim bytes() As Byte = data.GetRange(position, Marshal.SizeOf(Int32.MaxValue.GetType)).ToArray

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            Return BitConverter.ToInt32(bytes, 0)
        End Function

        Public Shared Function SglUnflatten(data As System.Collections.Generic.List(Of Byte), position As Integer) As Single
            Dim bytes() As Byte = data.GetRange(position, Marshal.SizeOf(Single.MaxValue.GetType)).ToArray

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            Return BitConverter.ToSingle(bytes, 0)
        End Function

        Public Shared Function ShortUnflatten(data As System.Collections.Generic.List(Of Byte), position As Integer) As Int16
            Dim bytes() As Byte = data.GetRange(position, Marshal.SizeOf(Int16.MaxValue.GetType)).ToArray

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            Return BitConverter.ToInt16(bytes, 0)
        End Function

        Public Shared Function StringArrUnflatten(data As System.Collections.Generic.List(Of Byte), position As Integer) As String()
            Dim arraySize As Integer = IntUnflatten(data, position)
            Dim val() As String

            position += Marshal.SizeOf(Integer.MaxValue.GetType)

            ReDim val(arraySize - 1)
            For index As Integer = 0 To arraySize - 1
                val(index) = StringUnflatten(data, position)
                position += val(index).Length + Marshal.SizeOf(Integer.MaxValue.GetType)
            Next

            Return val
        End Function

        Public Shared Function StringUnflatten(data As System.Collections.Generic.List(Of Byte), position As Integer) As String
            Dim strLength As Integer = IntUnflatten(data, position)

            position += Marshal.SizeOf(Integer.MaxValue.GetType)

            Return System.Text.Encoding.ASCII.GetString(data.GetRange(position, strLength).ToArray())
        End Function

        Public Shared Function UIntArrUnflatten(data As System.Collections.Generic.List(Of Byte), ByRef position As Integer) As UInt32()
            Dim val() As UInt32
            Dim arraySize As Integer = IntUnflatten(data, position)
            position += Marshal.SizeOf(Integer.MaxValue.GetType)

            ReDim val(arraySize - 1)

            For index As Integer = 0 To arraySize - 1
                val(index) = UIntUnflatten(data, position)
                position += Marshal.SizeOf(UInteger.MaxValue.GetType)
            Next

            Return val
        End Function

        Public Shared Function UIntUnflatten(data As System.Collections.Generic.List(Of Byte), position As Integer) As UInt32
            Dim bytes() As Byte = data.GetRange(position, Marshal.SizeOf(UInt32.MaxValue.GetType)).ToArray

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            Return BitConverter.ToUInt32(bytes, 0)
        End Function

        Public Shared Function UShortUnflatten(data As System.Collections.Generic.List(Of Byte), position As Integer) As UInt16
            Dim bytes() As Byte = data.GetRange(position, 2).ToArray

            If BitConverter.IsLittleEndian Then
                Array.Reverse(bytes)
            End If

            Return BitConverter.ToUInt16(bytes, 0)
        End Function

    End Class
End Namespace


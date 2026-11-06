Imports System
Imports System.Collections.Generic
Imports System.Linq

Module Module1

    Public NotInheritable Class LinqExtenions
        Private Sub New()
        End Sub
        '<System.Runtime.CompilerServices.Extension> _
        Public Shared Function Pivot(Of TSource, TFirstKey, TSecondKey, TValue)(source As IEnumerable(Of TSource), firstKeySelector As Func(Of TSource, TFirstKey), secondKeySelector As Func(Of TSource, TSecondKey), aggregate As Func(Of IEnumerable(Of TSource), TValue)) As Dictionary(Of TFirstKey, Dictionary(Of TSecondKey, TValue))
            Dim retVal = New Dictionary(Of TFirstKey, Dictionary(Of TSecondKey, TValue))()
            Dim l = source.ToLookup(firstKeySelector)
            For Each item As Object In l
                Dim dict = New Dictionary(Of TSecondKey, TValue)()
                retVal.Add(item.Key, dict)
                Dim subdict = item.ToLookup(secondKeySelector)
                For Each subitem As Object In subdict
                    dict.Add(subitem.Key, aggregate(subitem))
                Next
            Next
            Return retVal
        End Function
    End Class

End Module

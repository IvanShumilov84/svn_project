Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Reflection
Imports System.ComponentModel

Module CommonModule
    Public Function GetComponents(ByVal frm As Form) As List(Of Component) 'Get All components on a windows forms
        Dim components As New List(Of Component)
        Dim fieldInfos() As FieldInfo = frm.GetType.GetFields _
            (BindingFlags.NonPublic Or BindingFlags.Instance Or _
             BindingFlags.DeclaredOnly)
        For Each f As FieldInfo In fieldInfos
            If f.FieldType.BaseType Is GetType(Component) Then
                Dim c As Component = TryCast(f.GetValue(frm), Component)
                If c IsNot Nothing Then components.Add(c)
            End If
        Next
        Return components
    End Function
End Module

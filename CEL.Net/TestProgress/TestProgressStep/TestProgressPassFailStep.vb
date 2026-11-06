Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic

Namespace CEL.TestProgress

    Public Class TestProgressPassFailStep
        Inherits CEL.TestProgress.TestBaseStep
        Implements CEL.TestProgress.IPassFail

        Public Sub New(strTitle As String)
            MyBase.New(strTitle)
        End Sub

        Public Sub New(title As String, checkList As CEL.TestProgress.ICheckListItem())
            MyBase.New(title, checkList)
        End Sub

        Public Function Fail(testProgressSheet As TestProgressSheet) As TestProgressSheetEntry Implements CEL.TestProgress.IPassFail.Fail
            Dim testStepResultString As String

            If CheckListFailed() Then
                testStepResultString = Title + " : Out Of Tolerances"
            Else
                testStepResultString = Title + " : Failed"
            End If

            Return New TestProgressSheetEntry(String.Empty, testStepResultString, GetCheckListResults())
        End Function

        Public Function Pass(testProgressSheet As TestProgressSheet) As TestProgressSheetEntry Implements CEL.TestProgress.IPassFail.Pass
            Dim testStepResultString As String

            If CheckListFailed() Then
                testStepResultString = Title + " : Out Of Tolerances"
                System.Diagnostics.Trace.WriteLine("TestProgressPassFailStep.Pass() : CheckListFailed for Title")
                For Each item As String In GetCheckListResults()
                    System.Diagnostics.Trace.WriteLine("\t" + item)
                Next
            Else
                If CheckList.Count > 0 Then
                    testStepResultString = Title
                Else
                    testStepResultString = Title + " : Passed"
                End If
            End If

            Return New TestProgressSheetEntry(String.Empty, testStepResultString, GetCheckListResults())
        End Function
    End Class

End Namespace


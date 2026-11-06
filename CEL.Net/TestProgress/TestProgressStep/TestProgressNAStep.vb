Option Strict On
Option Explicit On

Namespace CEL.TestProgress

    Public Class TestProgressNAStep
        Inherits CEL.TestProgress.TestBaseStep
        Implements CEL.TestProgress.IExecutable

        Public Sub New(title As String)
            MyBase.New(title)
        End Sub

        Public Sub New(title As String, checkList As CEL.TestProgress.ICheckListItem())
            MyBase.New(title, checkList)
        End Sub

        Public Function Execute(testProgressSheet As TestProgressSheet) As TestProgressSheetEntry Implements CEL.TestProgress.IExecutable.Execute
            Dim testStepResultString As String

            If CheckListFailed() Then
                testStepResultString = Title + " : Out Of Tolerances"
            Else
                testStepResultString = Title
            End If

            Return New TestProgressSheetEntry(String.Empty, testStepResultString, GetCheckListResults())
        End Function
    End Class

End Namespace


Option Strict On
Option Explicit On

Namespace CEL.TestProgress

    Public Interface ICheckListItem
        Inherits System.ComponentModel.INotifyPropertyChanged
        Property Active As Boolean
        Property Value As String
        ReadOnly Property Parameter As String
        ReadOnly Property Unit As String
        ReadOnly Property Limit As String
        Function Failed() As Boolean
        Function Passed() As Boolean
    End Interface

    Public Interface ITestStep
        Property Active As Boolean
        ReadOnly Property CheckList As System.ComponentModel.BindingList(Of ICheckListItem)
        ReadOnly Property Title As String
    End Interface

    Public Interface IPassFail
        Function Pass(testProgressSheet As TestProgressSheet) As TestProgressSheetEntry
        Function Fail(testProgressSheet As TestProgressSheet) As TestProgressSheetEntry
    End Interface

    Public Interface IExecutable
        Function Execute(testProgressSheet As TestProgressSheet) As TestProgressSheetEntry
    End Interface

    Public Interface ITestProgressStepListFactory
        Function CreateEngineTestProgressStepList(EngineModel As String) As System.Collections.Generic.ICollection(Of ITestStep)
    End Interface

End Namespace


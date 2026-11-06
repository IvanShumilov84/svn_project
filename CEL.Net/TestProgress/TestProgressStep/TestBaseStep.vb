Option Strict On
Option Explicit On

Namespace CEL.TestProgress

    Public MustInherit Class TestBaseStep
        Implements ITestStep

        Public Sub New(strTitle As String)
            m_strTitle = strTitle
        End Sub

        Public Sub New(strTitle As String, checkList As CEL.TestProgress.ICheckListItem())
            Me.New(strTitle)

            m_CheckList = New System.ComponentModel.BindingList(Of ICheckListItem)(checkList)
        End Sub

        Public Property Active As Boolean Implements CEL.TestProgress.ITestStep.Active
            Get
                Return m_active
            End Get
            Set(value As Boolean)
                If m_active <> value Then
                    m_active = value
                    For Each checkItem As ICheckListItem In CheckList
                        checkItem.Active = m_active
                    Next
                End If
            End Set
        End Property

        Public ReadOnly Property CheckList As System.ComponentModel.BindingList(Of ICheckListItem) Implements CEL.TestProgress.ITestStep.CheckList
            Get
                Return m_CheckList
            End Get
        End Property

        Public ReadOnly Property Title As String Implements CEL.TestProgress.ITestStep.Title
            Get
                Return m_strTitle
            End Get
        End Property

        Protected ReadOnly Property CheckListFailed As Boolean
            Get
                Dim results As Boolean = False
                For Each checkListItem As ICheckListItem In CheckList
                    If checkListItem.Failed Then
                        results = True
                        Exit For
                    End If
                Next
                Return results
            End Get
        End Property

        Protected ReadOnly Property CheckListPassed As Boolean
            Get
                Return Not CheckListFailed
            End Get
        End Property

        Protected Function GetCheckListResults() As String()
            Dim checkListResult() As String
            ReDim checkListResult(CheckList.Count - 1)

            Dim index As Integer = 0
            For Each checkListItem As ICheckListItem In Me.CheckList
                checkListResult(index) = checkListItem.ToString()
                index += 1
            Next
            Return checkListResult
        End Function

        Private m_active As Boolean = False
        Private ReadOnly m_strTitle As String
        Private ReadOnly m_CheckList As System.ComponentModel.BindingList(Of ICheckListItem)
    End Class

End Namespace


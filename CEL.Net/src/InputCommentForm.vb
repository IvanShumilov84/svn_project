Imports System.Windows.Forms

Public Class InputCommentForm

Public Overloads Function ShowDialog( _
    ByVal TitleText As String, _
    ByVal PromptText As String, _
    ByVal DefaultText As String, _
    ByRef EnteredText As String) As System.Windows.Forms.DialogResult

    Me.Text = TitleText
    Me.FormPromptText.Text = PromptText
    Me.FormCommentTextInput.Text = DefaultText

    Me.FormCommentTextInput.Select()

    Me.ShowDialog()

    EnteredText = Me.m_CommentText

    Return Me.DialogResult

  End Function

Protected m_CommentText As String = ""

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        m_CommentText = Me.FormCommentTextInput.Text
        Me.Close()
    End Sub

    Private Sub InputCommentForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.FormCommentTextInput.Select()
    End Sub

    Private Sub InputComment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.FormCommentTextInput.Select()
    End Sub
End Class

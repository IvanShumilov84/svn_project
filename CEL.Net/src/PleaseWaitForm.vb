Public Class PleaseWaitForm

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
    Public Sub New(message As String)
        MyBase.New()
        InitializeComponent()
        Me.Label1.Text = message
    End Sub
End Class
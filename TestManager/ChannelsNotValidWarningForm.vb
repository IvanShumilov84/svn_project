Public Class ChannelsNotValidWarningForm


    Public Sub New(notValidChannels As System.Collections.Generic.List(Of String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.ListBox1.DataSource = notValidChannels

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        Me.Finalize()
    End Sub

End Class
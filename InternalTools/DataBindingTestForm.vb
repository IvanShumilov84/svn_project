Public Class DataBindingTestForm
    Private boolChannel As New KineticaRT.Channels.BooleanChannel
    Private numChannel As New KineticaRT.Channels.NumericChannel
    Private celNumLinkChannel As New CEL.KineticaRT.Channels.NumericThresholdLink
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        boolChannel.Connect()
        numChannel.Connect()
        celNumLinkChannel.Connect()

        ' Add any initialization after the InitializeComponent() call.
        TextBox1.DataBindings.Add(New Binding("Text", boolChannel, "ValueObject", False, DataSourceUpdateMode.OnPropertyChanged))
        TextBox2.DataBindings.Add(New Binding("Text", numChannel, "ValueObject", False, DataSourceUpdateMode.OnPropertyChanged))
        TextBox4.DataBindings.Add(New Binding("Text", celNumLinkChannel, "ValueObject", False, DataSourceUpdateMode.OnPropertyChanged))
        TextBox5.DataBindings.Add(New Binding("Text", celNumLinkChannel, "ValueObject", False, DataSourceUpdateMode.OnPropertyChanged))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox3.Text = boolChannel.ValueObject
        TextBox4.Text = numChannel.ValueObject
        boolChannel.ValueObject = True
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        boolChannel.ValueObject = CheckBox1.Checked
        celNumLinkChannel.ValueObject = numChannel.ValueObject
        'TextBox5.Text = celNumLinkChannel.ValueObject
    End Sub


End Class

Imports System.Windows.Controls
Imports System.Windows.Forms

Public Class Form4

    Dim wpfTextBox As System.Windows.Controls.TextBox

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles Me.Load



        'wpfTextBox = New System.Windows.Controls.TextBox()
        'wpfTextBox.Name = "myTextBox"
        'wpfTextBox.Text = "Allo"

        'AddHandler wpfTextBox.TextChanged, AddressOf textbox_TextChanged


        'ElementHost1.Child = wpfTextBox

    End Sub

    Public Sub textbox_TextChanged(ByVal sender As Object, ByVal e As TextChangedEventArgs)

        MessageBox.Show("Text Changed!", ":-D")

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'wpfTextBox.Text = "test"

    End Sub
End Class
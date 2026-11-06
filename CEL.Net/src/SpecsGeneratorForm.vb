Public Class SpecsGeneratorForm

    Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw.DoWork



    End Sub


    Private Sub btnGenerate_Click(sender As Object, e As System.EventArgs) Handles btnGenerate.Click
        If Not bw.IsBusy Then
            bw.RunWorkerAsync()
        End If
    End Sub


End Class
Public Class ThreadHelper

    Public Shared Sub StopOrKillThread(ByRef WorkerThread As System.Threading.Thread, ByRef ExitThreadSignal As Boolean)

        'already stop, do nothing
        If WorkerThread Is Nothing Then
            Return
        End If

        ExitThreadSignal = True

        'Kill the Event Archiver Thread if still alive
        Dim NumberIteration As Integer

        For NumberIteration = 0 To 2000
            System.Threading.Thread.Sleep(25) 'give a chance to the other thread to complete 2000*25ms = 10 seconds

            'already stop, do nothing
            If WorkerThread Is Nothing Then
                Return
            End If

            If Not WorkerThread.IsAlive Then
                Exit For
            End If
        Next

        If Not (WorkerThread Is Nothing) Then
            If Not (WorkerThread.Join(500)) Then
                If WorkerThread.IsAlive Then
                    WorkerThread.Abort()
                    WorkerThread.Join()
                End If
            End If
            WorkerThread = Nothing
        End If

    End Sub

End Class

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Diagnostics
Imports System.ComponentModel
Imports TestManager
Imports System.Threading
Imports System.Net

Public Enum FunctionalityName
    Export
    Upload
    RebootQNX
    RebootCRIO
    RetrieveELog
    RetrieveCRIOLog
End Enum

Public Class ProgressBarForm

    'Init Progress bar form and start the export function.
    Public Sub New(CELProject As CEL.HMI.CELDAS, ByVal InterfaceChoice As FunctionalityName)
        'You can create your own objet to pass information to the background worker.
        Dim workerInfo As BackGroundWorkerInformation
        workerInfo.InterfaceChoice = InterfaceChoice
        workerInfo.Cfgdata = CELProject

        InitializeComponent()
        'init progressbar parameters
        ProgressBar1.Maximum = CELProject.LoadFormatCollection.Count
        ProgressBar1.Minimum = 0
        ProgressBar1.Step = 1
        ProgressBar1.MarqueeAnimationSpeed = 0
        'init backgroundworker parameters
        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True
        'Init progressbar in middle of screen
        Me.StartPosition = FormStartPosition.CenterParent
        'start background work
        bw.RunWorkerAsync(workerInfo)
    End Sub

    Public WithEvents bw As New BackgroundWorker

    ''Start the work on an other thread
    Private Sub bgw_doWork(sender As Object, e As DoWorkEventArgs) Handles bw.DoWork
        Dim doSelectedWork As WorkChoiceForProgressBar = New WorkChoice
        Dim bgWorker As BackGroundWorkerInformation = e.Argument
        'Take the interface selection
        Select Case bgWorker.InterfaceChoice
            Case FunctionalityName.Export
                doSelectedWork.DoWorkForExporter(bw)
            Case FunctionalityName.Upload
                doSelectedWork.DoWorkForUploader(bgWorker.Cfgdata, bw)
            Case FunctionalityName.RebootQNX
                doSelectedWork.DoWorkForRebootQNX(bw)
            Case FunctionalityName.RebootCRIO
                doSelectedWork.DoWorkForRebootCRIO(bw)
            Case FunctionalityName.RetrieveELog
                doSelectedWork.DoWorkForRetrieveELog(bw)
            Case FunctionalityName.RetrieveCRIOLog
                doSelectedWork.DoWorkForRetrieveCRIOLog(bw)
            Case Else
                Debug.Assert(False, "ProgressBarForm.bgw_doWork: No case for this particular request " + e.Argument)
                Console.WriteLine("ProgressBarForm.bgw_doWork: No case for this particular request ", e.Argument)
        End Select
    End Sub
    Private Sub bgw_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bw.ProgressChanged
        lblExportName.Text = "Processing: " & e.UserState
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub Bgw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted
        'Close form when operation is completed
        Me.Close()
    End Sub

    Public Interface WorkChoiceForProgressBar
        Function DoWorkForExporter(ByRef bw As BackgroundWorker) As Boolean
        Function DoWorkForUploader(ByRef configData As CEL.HMI.CELDAS, ByRef bw As BackgroundWorker) As Boolean
        Function DoWorkForRebootQNX(ByRef bw As BackgroundWorker) As Boolean
        Function DoWorkForRebootCRIO(ByRef bw As BackgroundWorker) As Boolean
        Function DoWorkForRetrieveELog(ByRef bw As BackgroundWorker) As Boolean
        Function DoWorkForRetrieveCRIOLog(ByRef bw As BackgroundWorker) As Boolean
    End Interface

    Private Class WorkChoice
        Implements WorkChoiceForProgressBar

        Private Const QNX_IP As String = "192.168.2.200"
        Private Const CRIO_IP As String = "192.168.2.220"

        Private Function DoWorkForExporter(ByRef theBackGroundWorker As BackgroundWorker) As Boolean Implements WorkChoiceForProgressBar.DoWorkForExporter
            DoWorkForExporter = False
            Try
                'e.Argument decideds the interface and the work to do on the second thread
                Dim exportSoftware As New CEL.HMI.HardwareConfiguration
                If exportSoftware.ExportAllCsv(Nothing, theBackGroundWorker) = True Then
                Else
                    Debug.Assert(False, "Export CSV(s) ERROR: is the database available? Corrected path?")
                End If

                theBackGroundWorker.CancelAsync()
            Catch ex As Exception
                Debug.Assert(False, "ERROR from: TestManager.ProgressBarForm.DoWorkForExporter ERROR: ", ex.ToString)
            End Try
            Return True
        End Function

        Private Function DoWorkForUploader(ByRef configData As CEL.HMI.CELDAS, ByRef theBackGroundWorker As BackgroundWorker) As Boolean Implements WorkChoiceForProgressBar.DoWorkForUploader
            DoWorkForUploader = False
            Try
                Dim exportSoftware As New CEL.HMI.HardwareConfiguration
                If exportSoftware.FtpAllCsvUpload(configData.ExportPathName, configData.QNX.UploadPath,
                                                         configData.QNX.IpAdress, configData.QNX.UserName, configData.QNX.Password, theBackGroundWorker) Then
                End If
                theBackGroundWorker.CancelAsync()

            Catch ex As Exception
                Debug.Assert(False, "ERROR from: TestManager.ProgressBarForm.DoWorkForUploader ERROR: ", ex.ToString)
            End Try
            Return True
        End Function

        Private Function DoWorkForRebootQNX(ByRef theBackGroundWorker As BackgroundWorker) As Boolean Implements WorkChoiceForProgressBar.DoWorkForRebootQNX
            DoWorkForRebootQNX = False
            Try
                Dim TelNetClient As New MinimalisticTelnet.TelnetConnection(QNX_IP, 23)

                TelNetClient.Login("root", "", 3000)
                TelNetClient.WriteLine("shutdown")
                theBackGroundWorker.CancelAsync()

            Catch ex As Exception
                MsgBox("ERROR from: TestManager.ProgressBarForm.DoWorkForRebootQNX : Cannot Connect to QNX")
            End Try
            Return True
        End Function

        Public Function DoWorkForRebootCRIO(ByRef backgroundWorker As BackgroundWorker) As Boolean Implements WorkChoiceForProgressBar.DoWorkForRebootCRIO
            Try
                Dim telNetClient As New MinimalisticTelnet.TelnetConnection(CRIO_IP, 22)

                telNetClient.Login("admin", "", 3000)
                telNetClient.WriteLine("reboot")
                backgroundWorker.CancelAsync()

            Catch ex As Exception
                MsgBox("ERROR from: TestManager.ProgressBarForm.DoWorkForRebootCRIO : Cannot Connect to cRIO")
            End Try
            Return True
        End Function

        Public Function DoWorkForRetrieveELog(ByRef backgroundWorker As BackgroundWorker) As Boolean Implements WorkChoiceForProgressBar.DoWorkForRetrieveELog
            Try
                Dim telNetClient As New MinimalisticTelnet.TelnetConnection(QNX_IP, 23)
                telNetClient.Login("root", "", 3000)
                telNetClient.WriteLine("cd /usr/v2000")
                telNetClient.WriteLine("./bin/ExtractLog e.log e.txt")
                telNetClient.WriteLine("exit")

            Catch ex As Exception
                MsgBox("ERROR from: TestManager.ProgressBarForm.DoWorkForRetrieveELog : Cannot convert e.log to e.txt")
            End Try

            Thread.Sleep(2000)

            DownloadFile(QNX_IP, "Virgo2000", "v2000", "e.txt", "/", "C:\Users\Celdas200\Desktop\Logs\")

            backgroundWorker.CancelAsync()

            Return True
        End Function

        Public Function DoWorkForRetrieveCRIOLog(ByRef backgroundWorker As BackgroundWorker) As Boolean Implements WorkChoiceForProgressBar.DoWorkForRetrieveCRIOLog

            'This should work, but I haven't tested as I didn't have acess to a cRIO

            DownloadFile(CRIO_IP, "admin", "", "errlog.txt", "/var/local/natinst/log/", "C:\Users\Celdas200\Desktop\Logs\")

            backgroundWorker.CancelAsync()

            Return True
        End Function

        Private Sub DownloadFile(server As String, user As String, password As String, fileName As String, sourcePath As String, destinationPath As String)

            Dim request = New WebClient()
            request.Credentials = New NetworkCredential(user, password)

            Try
                request.DownloadFile("ftp://" & server & sourcePath & fileName, destinationPath & DateTime.Now.ToString("yyyy-MM-dd_hh:mm").Replace(":", "H") & "_" & fileName)
            Catch ex As Exception

                MsgBox("Cannot download " & fileName & " from " & server)
            End Try

        End Sub

    End Class

    Public Structure BackGroundWorkerInformation

        Public InterfaceChoice As FunctionalityName
        Public Cfgdata As CEL.HMI.CELDAS

    End Structure
End Class

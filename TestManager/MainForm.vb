Imports EntitySpaces.Interfaces
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports System.Diagnostics
Imports System.IO
Imports System.Xml

Imports CEL.HMI
Imports CEL.Library

Public Class MainForm

    Private m_CELProject As CELDAS = CELDAS.Instance()

    <System.STAThread()> _
    Public Shared Sub Main()
        System.Windows.Forms.Application.EnableVisualStyles()
        System.Windows.Forms.Application.Run(New MainForm)
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()



        m_CELProject.DefaultConnectionName = "ACCESSDynamic"
        m_CELProject.EngineModel = "APS3200"
        m_CELProject.PWCEECPartNumber = "Default"
        m_CELProject.Database.Password = "CEL9994"

        ' Add any initialization after the InitializeComponent() call.
        'Ne pas supprimer.  Pour overrider dynamiquement une connectionstring de entity spaces
        For Each connElement As esConnectionElement In esConfigSettings.ConnectionInfo.Connections

            'connElement.ConnectionString = connElement.ConnectionString.Replace("Password=", "Password=CEL9994" + CELProject.Database.Password) 'CELProject.Database.OleDbConnectionString
            'Solution de contournement pour setter le mot de passe par le code dans la connectionString
            'Dim connectionString As String = connElement.Replace("Password=", "Password=" + CELProject.Database.Password)

            Dim newConn As New esConnectionElement
            newConn.ConnectionString = m_CELProject.Database.OleDbConnectionString
            newConn.Name = connElement.Name
            newConn.Provider = connElement.Provider
            newConn.ProviderClass = connElement.ProviderClass
            newConn.SqlAccessType = esSqlAccessType.DynamicSQL
            newConn.ProviderMetadataKey = connElement.ProviderMetadataKey
            newConn.CommandTimeout = 20
            esConfigSettings.ConnectionInfo.Connections.Add(newConn)
            esConfigSettings.ConnectionInfo.Default = m_CELProject.DefaultConnectionName
        Next



        'Initialize database engine
        esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()


        Dim Impersonation As ImpersonateUser


        Impersonation = New ImpersonateUser(m_CELProject.Database.ComputerLoginName, m_CELProject.Database.ComputerName, m_CELProject.Project.Number, ImpersonateUser.LOGON32_LOGON_NEW_CREDENTIALS)

        'User Account Control (UAC)
        'A problem arises when you use any of the above code on a machine that has UAC enabled, and the process is not elevated.
        'While the user may be an administrator, when the process is not elevated yet, the user has a split token – which doesn’t have the administrator privileges.
        'A way around this is to use the GetTokenInformation API call to inspect the token to see if it’s a split token. In most cases this will mean that UAC is on and
        ' the current user is an administrator. This is not 100% reliable (see References) but it’s probably the best we can do for now.

        'If Impersonation.LoggedOn AndAlso Not SecurityHelper.HasAdministorRight() Then
        '    MessageBox.Show("To RUN this application properly you must have Administrator RIGHTS", "Administrator Right Required", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        '    Application.Exit()
        'End If
        If SecurityHelper.GetUacElevationType() <> SecurityHelper.TokenElevationType.TokenElevationTypeDefault And m_CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then
            'A partir d'un PC remote le UAC ne permet pas d'ouvrir de fichier a partir d'un drive mappe.  
            'BUG connu aussi de la fonction File.Exists et Directory.Exists qui retourne toujours False
            'Si l'UAC est configure a Disabled (Never Notify) alors on est capable d'ouvrir les fichier via drive mappe, mais la fonction File.Exists et Directory.Exists retournera toujours False
            'TokenElevationTypeDefault  NeverNotify 
            'TokenElevationTypeFull     Never me only when programs try to make changes to my computer
            'TokenElevationTypeFull     Never me only when programs try to make changes to my computer
            'TokenElevationTypeFull     Always Notify

            MessageBox.Show("To RUN this application properly you have to set UAC (User Account Control Setting) to ""Never Notify"" ", "Administrator Right Required", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Application.Exit()
            Return
        End If
#If DEBUG Then
        lblStatusBar.Text = "Connected to " + m_CELProject.DefaultConnectionName + ": " + m_CELProject.Database.FullFilePath
#Else
#End If

    End Sub

    Private Sub btnLogReportLayout_Click(sender As Object, e As EventArgs) Handles btnLogReportLayout.Click

        Dim frmLogReportDetail As New LogReportDetailForm(m_CELProject)
        frmLogReportDetail.ShowDialog()

    End Sub

    Private Sub btnHmiNumericChannels_Click(sender As Object, e As EventArgs) Handles btnHmiNumericChannels.Click

        Dim frmHmiNumericChannels As New HmiNumericChannelForm(m_CELProject)
        frmHmiNumericChannels.ShowDialog()

    End Sub

    Private Sub btnArchiveReportLayout_Click(sender As Object, e As EventArgs) Handles btnArchiveReportLayout.Click

        Dim frmArchiveReportDetail As New ArchiveReportDetailForm(m_CELProject)
        frmArchiveReportDetail.ShowDialog()

    End Sub

    Private Sub BtnExitApplication_Click(sender As Object, e As EventArgs) Handles BtnExitApplication.Click
        Me.Dispose()
    End Sub

    Private Sub btnTestProgressStepEditor_Click(sender As Object, e As EventArgs) Handles btnTestProgressStepEditor.Click
        Dim frmTestProgressStepEditor As New TestProgressStepForm(m_CELProject)
        frmTestProgressStepEditor.ShowDialog()
    End Sub

    Private Sub btnLoadCellAssignationEditor_Click(sender As Object, e As EventArgs) Handles btnLoadCellAssignationEditor.Click
        Dim frmLoadCellAssignationEditor As New LoadCellAssignationForm(m_CELProject)
        frmLoadCellAssignationEditor.ShowDialog()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Dispose()
    End Sub

    Private Sub btnUserEditor_Click(sender As Object, e As EventArgs) Handles btnUserEditor.Click
        Dim frmUserEditor As New UserEditorForm(m_CELProject)
        frmUserEditor.ShowDialog()
    End Sub

    Private Sub btnUUTForm_Click(sender As Object, e As EventArgs) Handles btnUUTForm.Click
        Dim frmMaintenance As New MaintenanceForm(m_CELProject)
        frmMaintenance.ShowDialog()
    End Sub

    Private Sub btnChannelEditor_Click(sender As Object, e As EventArgs) Handles btnChannelEditor.Click
        Dim frmChannelEditor As New ChannelEditorForm(m_CELProject)
        frmChannelEditor.ShowDialog()
    End Sub
    Private Sub btnFlowmeterConfiguration_Click(sender As Object, e As EventArgs) Handles btnFlowmeterConfiguration.Click
        Dim frmFlowmeter As New FlowmeterConfigurationForm(m_CELProject)
        frmFlowmeter.ShowDialog()
    End Sub


    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try


            Dim frmLogon As New LoginForm(m_CELProject)
            frmLogon.ShowDialog()
            If frmLogon.PasswordValidated Then
                'Set visiblity of button
                btnFlowmeterConfiguration.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.FlowmeterConfiguration
                btnLoadCellAssignationEditor.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.LoadCellAssignationEditor
                btnLogReportLayout.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.LogReportLayout
                btnHmiNumericChannels.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.HmiNumericChannels
                btnArchiveReportLayout.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.ArchiveReportLayout
                btnTestProgressStepEditor.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.TestProgressStepEditor
                btnUserEditor.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.UserEditor
                btnUUTForm.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.Maintenance
                btnChannelEditor.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.ChannelEditor
                btnConfigurationFileExport.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.ConfigurationFileExport
                btnCSVFileExport.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.CSVFileExport
                btnCSVFileUpload.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.CSVFileUpload
                btnHardwareConfiguration.Visible = UsingConfigurationManager.TestManagerConfigData.Enable.HardwareConfiguration
            Else
                Me.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show("MainForm_Load: " + ex.ToString())
        End Try
    End Sub


    Private Sub btnConfigurationFileExport_Click(sender As Object, e As EventArgs) Handles btnConfigurationFileExport.Click
        'gather config pathname
        Try
            'Resummon the sub that gather commandline argument
            CELDAS.ProcessCmdLineArguments()

            Dim exportSoft As New CEL.HMI.HardwareConfiguration

            Dim ftp As CEL.Net.FtpClient
            ftp = exportSoft.InitFTPConnection(False, m_CELProject.QNX.IpAdress, m_CELProject.QNX.UserName, m_CELProject.QNX.Password, True)

            exportSoft.ConstantCreationTimeFtpUpload(m_CELProject.Project.WorkbenchConfigurationFullFileName, "celdas.cfg", ftp)
        Catch ex As Exception
            Debug.Assert(False, "Error: Mainform.btnConfigurationFileExport_click: Probleme uploading the config file.")
        End Try

        MsgBox("Configuration file from : " & vbCr & "[" & m_CELProject.Project.WorkbenchConfigurationFullFileName & "]," & vbCr & " as been exported to the ./ of the QNX Server")
        Console.WriteLine("Config file specified in the command line have been uploaded to the Qnx upload folder specified in the configuration file.")
    End Sub

    ''' <summary>
    ''' ONLY Export the CSV in the folder path configured in the config. file.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This button require CELDAS lib to be available.</remarks>
    Private Sub btnCSVFileExport_Click(sender As Object, e As EventArgs) Handles btnCSVFileExport.Click
        Try
            CELDAS.ProcessCmdLineArguments()

            ''Offload the process to an other thread to avoid freezing the ui.
            Dim LoadForm As New ProgressBarForm(m_CELProject, FunctionalityName.Export)
            LoadForm.ShowDialog()
            MsgBox("Export Completed")

        Catch ex As Exception
            Debug.Assert(False, "ERROR from: TestManager.MainForm.btnCSVFileExport_Click: {0} Error", ex.ToString)
        End Try
        Console.WriteLine("The Export of the csv(s) files have been completed in the configured directory from the config. file.")
    End Sub

    ''' <summary>
    ''' ONLY Upload the CSVs file from the Folder to the Folder, both, configured in the config. file.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This requires CSV to have already been made. If not, system may crash.</remarks>
    Private Sub btnCSVFileUpload_Click(sender As Object, e As EventArgs) Handles btnCSVFileUpload.Click
        Try
            CELDAS.ProcessCmdLineArguments()

            ''Offload the process to an other thread to avoid freezing the ui.
            Dim loadForm As New ProgressBarForm(m_CELProject, FunctionalityName.Upload)
            loadForm.ShowDialog()
            MsgBox("Upload Completed")

        Catch ex As Exception
            Console.WriteLine("ERROR from:TestManager.MainForm.btnCSVFileUpload_Click: Message:{0} -Do CSV(s) exist? Is the server available? Corrected path?", ex.ToString)
            Debug.Assert(False)
        End Try
        Console.WriteLine("The Upload of the Csv(s) files have been completed in the configured directory from the config. file.")
    End Sub

    Private Sub mnuRebootQNX_Click(sender As Object, e As EventArgs) Handles RebootQNXToolStripMenuItem.Click
        Try

            Dim LoadForm As New ProgressBarForm(m_CELProject, FunctionalityName.RebootQNX)
            LoadForm.ShowDialog()
            MsgBox("Reboot request in progress ; please wait at least a minute for the QNX to be operational.")

        Catch ex As Exception
            MsgBox("Could not connect to QNX and request reboot")
        End Try
    End Sub

    Private Sub mnuRetrieveELOG_Click(sender As Object, e As EventArgs) Handles RetrieveELogToolStripMenuItem.Click
        Try

            Dim LoadForm As New ProgressBarForm(m_CELProject, FunctionalityName.RetrieveELog)
            LoadForm.ShowDialog()

        Catch ex As Exception
            MsgBox("Could not connect to QNX")
        End Try
    End Sub

    Private Sub mnuRebootCRIO_Click(sender As Object, e As EventArgs) Handles RebootCRIOToolStripMenuItem.Click
        Try

            Dim LoadForm As New ProgressBarForm(m_CELProject, FunctionalityName.RebootCRIO)
            LoadForm.ShowDialog()
            MsgBox("Reboot request in progress ; please wait at least a minute for the cRIO to be operational.")

        Catch ex As Exception
            MsgBox("Could not connect to cRIO and request reboot")
        End Try
    End Sub

    Private Sub mnuRetrieveCRIOLog_Click(sender As Object, e As EventArgs) Handles RetrieveCRIOLogToolStripMenuItem.Click
        Try

            Dim LoadForm As New ProgressBarForm(m_CELProject, FunctionalityName.RetrieveCRIOLog)
            LoadForm.ShowDialog()

        Catch ex As Exception
            MsgBox("Could not connect to cRIO")
        End Try
    End Sub
End Class


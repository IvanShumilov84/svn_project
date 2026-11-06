Imports KineticaRT
Imports KineticaRT.Sequence
Imports KineticaRT.Servers.OpcClient
Imports KineticaRT.Logic.Message
Imports KineticaRT.Values
Imports KineticaRT.Channels
Imports KineticaRT.Studio.Controls
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.OleDb
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Security
Imports System.Security.Permissions
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports System.Xml.Linq


Imports CEL.HMI
Imports CEL.Library
Imports CEL.Data.BusinessObjects

'EntitySpaces
Imports EntitySpaces.Interfaces
Imports EntitySpaces.Loader
Imports System.Configuration
Imports EntitySpaces.Core
Imports System.Text.RegularExpressions
Imports System.Security.Principal

Imports System.Runtime.InteropServices 'For DllImport
Imports EntitySpaces.DynamicQuery



Imports CEL.Data.BusinessObjects.ARINC429LabelTemplate
Imports System.ComponentModel
Imports System.Globalization
Imports System.Net

Namespace StudioDemonstration

    Public Class OpenTest_Sequence
        Inherits CEL.KineticaRT.Sequence.OpenTest

        Const recorderName As String = "ARCHIVE"
        Const eventRecorderName As String = "EVENT_ARCHIVE"

        Private CELProject As CELDAS = CELDAS.Instance()
        Private HMIChannelCreation As New HMIChannelCreation(CELProject)
        Private SystemCheck As SystemCheck

        Dim Impersonation As ImpersonateUser
        Dim databaseHelper As DatabaseHelper

        Dim MeasurementUnitSelected As MeasurementUnit


        Public Overloads Overrides Sub Connect()

            Try
                If Me.Connected = False Then

                    'MyBase.Connect()
                    If Not CELProject.FastLoading Then
                        databaseHelper = New DatabaseHelper()
                        databaseHelper.ValidateDatabaseDriver()
                    End If

                    Impersonation = New ImpersonateUser(CELProject.Database.ComputerLoginName, CELProject.Database.ComputerName, CELProject.Project.Number, ImpersonateUser.LOGON32_LOGON_NEW_CREDENTIALS)

                    'User Account Control (UAC)
                    'A problem arises when you use any of the above code on a machine that has UAC enabled, and the process is not elevated.
                    'While the user may be an administrator, when the process is not elevated yet, the user has a split token – which doesn’t have the administrator privileges.
                    'A way around this is to use the GetTokenInformation API call to inspect the token to see if it’s a split token. In most cases this will mean that UAC is on and
                    ' the current user is an administrator. This is not 100% reliable (see References) but it’s probably the best we can do for now.

                    If Not CELProject.FastLoading Then
                        If Impersonation.LoggedOn AndAlso Not SecurityHelper.HasAdministorRight() Then
                            '              MessageBox.Show("To RUN this application properly you must have Administrator RIGHTS", "Administrator Right Required", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                            'Application.Exit()
                        End If
                        If SecurityHelper.GetUacElevationType() <> SecurityHelper.TokenElevationType.TokenElevationTypeDefault And CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then
                            'A partir d'un PC remote le UAC ne permet pas d'ouvrir de fichier a partir d'un drive mappe.  
                            'BUG connu aussi de la fonction File.Exists et Directory.Exists qui retourne toujours False
                            'Si l'UAC est configure a Disabled (Never Notify) alors on est capable d'ouvrir les fichier via drive mappe, mais la fonction File.Exists et Directory.Exists retournera toujours False
                            'TokenElevationTypeDefault  NeverNotify 
                            'TokenElevationTypeFull     Never me only when programs try to make changes to my computer
                            'TokenElevationTypeFull     Never me only when programs try to make changes to my computer
                            'TokenElevationTypeFull     Always Notify

                            '             clsTopMostMessageBox.Show("To RUN this application properly you have to set UAC (User Account Control Setting) to ""Never Notify"" ", "Administrator Right Required", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                            '            Application.Exit()
                            '           Return
                        End If
                    End If
                    '' Show how to use OpenExeConfiguration using the configuration user level.
                    'UsingConfigurationManager.GetRoamingConfiguration()
                    '' Show how to use GetSection.
                    'UsingConfigurationManager.CreateAppSettings()
                    '' Show how to use AppSettings.
                    'UsingConfigurationManager.ReadAppSettings()
                    '' Show how to use OpenExeConfiguration using the configuration file path.
                    'UsingConfigurationManager.GetAppConfiguration()
                    '' Show how to use ConnectionStrings.
                    'UsingConfigurationManager.ReadConnectionStrings()
                    '' Show how to use OpenMappedExeConfiguration.
                    'UsingConfigurationManager.MapExeConfiguration()
                    '' Show how to use OpenMappedMachineConfiguration.
                    'UsingConfigurationManager.MapMachineConfiguration()


                    CELProject.SetCallBackFunctions(AddressOf Me.GetChannel, AddressOf Me.GetObject, AddressOf Me.InitializeChannel, AddressOf Me.InitializeObject)
                    CELProject.LogPerfSynch.SetCallBackFunctions(AddressOf Me.GetChannel, AddressOf Me.GetObject)
                    'Setup Project Information

                    CELProject.TechnologyName = "CELDAS™ 200"


                    CELProject.ChannelSelectorFormModuleAvailable = False 'This Module is not available for 10726 Indonesia

                    CELProject.Database.Password = "CEL9994" 'Toujours requis dans le code!!!!!

                    'Test multi-user
                    'If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    '    CELProject.Database.UserId = "Administrator"
                    '    CELProject.Database.Password = "CEL9994"
                    'Else
                    '    CELProject.Database.UserId = "Administrator2"
                    '    CELProject.Database.Password = "CEL9994"
                    'End If


                    'Dim process As System.Diagnostics.Process
                    'Dim processStartInfo As System.Diagnostics.ProcessStartInfo
                    'processStartInfo = New System.Diagnostics.ProcessStartInfo
                    'processStartInfo.FileName = CELProject.Database.ExportCsvBatchFile
                    'If System.Environment.OSVersion.Version.Major >= 6 Then ' Windows Vista or higher
                    '    processStartInfo.Verb = "runas"
                    'Else
                    '    ' No need to prompt to run as admin
                    'End If
                    'processStartInfo.Arguments = ""
                    'processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                    'processStartInfo.UseShellExecute = True
                    'process = System.Diagnostics.Process.Start(processStartInfo)
                    'process.WaitForExit(60000)
                    'Dim appError As String
                    'Dim exitCode As Integer
                    'If process.HasExited Then
                    '    exitCode = process.ExitCode
                    '    If exitCode = 0 Then
                    '    Else
                    '        appError = process.StandardError.ReadToEnd()
                    '        MessageBox.Show(appError)
                    '    End If
                    'Else

                    'End If
                    'Shell(CELProject.Database.ExportCsvBatchFile, AppWinStyle.Hide)



                    'Il faut verifier que le repertoire de la base de donnees existe, ceci est moins utile pour des systemes qui n'ont qu'un HMI car la base de donnees est directement sur le PC.
                    'Par contre dans une configuration ou on a 2 HMI (Critical et Performance) il est toujours possible que le PC slave n'ait pas de communication avec le PC Master.
                    'Si on ne verifie pas que le repertoire exite et qu'on soit sur le PC qui est slave, alors on a un beau message d'exception qui n'est pas tres clair.
                    'Pour eviter ce probleme on ajoute ici un message explicite pour l'utilisateur.

                    Try

                        If Not CELProject.FastLoading Then
                            'Mapped network drives are user specific, so if the app is running under a different identity than the user that created the mapped drive letter (z:) it won't work.
                            Dim objReader As New System.IO.StreamReader(CELProject.Database.FullFilePath)
                            Dim buf(2) As Char
                            Dim count As Integer = objReader.ReadBlock(buf, 0, 3)
                            If count = 0 Then
                                DatabaseNotPresent = True
                            Else
                                DatabaseNotPresent = False
                            End If

                            'New Issue 12917 2014-05-18: Close Required or Test Session is Read Only
                            objReader.Close()
                        Else
                            DatabaseNotPresent = False
                        End If
                    Catch ex As Exception
                        'Le UAC (User account control) empeche l'acces aux drive mappe, meme si l'application roule en administrateur
                        'Ou sinon la base de donnees est deja ouverte dans Access... impossible de la lire
                        'System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()


                        If Impersonation.LoggedOn = False Then

                            Dim frmLogin As New CEL.KineticaRT.Controls.AuthentificationForm
                            frmLogin.LoginName = Environment.UserName

                            Dim NeedNetworkPassword As Boolean = True

                            While NeedNetworkPassword

                                Dim PopUpResult As System.Windows.Forms.DialogResult = frmLogin.ShowDialog

                                If PopUpResult = DialogResult.Cancel Then

                                    'user does not want to enter the required password
                                    DatabaseNotPresent = False
                                    Environment.Exit(0)
                                    Return

                                ElseIf PopUpResult = DialogResult.OK Then
                                    Dim LoginName As String = Nothing
                                    Dim Domain As String = Nothing

                                    'check if the LoginName contains the domain name
                                    If frmLogin.LoginName.Contains("/") Or frmLogin.LoginName.Contains("\") Then
                                        Dim SplitResult As String() = frmLogin.LoginName.Split(New Char() {"/"c, "\"c})
                                        Domain = SplitResult(0)
                                        LoginName = SplitResult(1)
                                        Impersonation = New ImpersonateUser(LoginName, Domain, frmLogin.Password, ImpersonateUser.LOGON32_LOGON_NEW_CREDENTIALS)
                                    Else
                                        Impersonation = New ImpersonateUser(frmLogin.LoginName, "", frmLogin.Password, ImpersonateUser.LOGON32_LOGON_INTERACTIVE)
                                    End If


                                    If Not Impersonation.LoggedOn Then
                                        clsTopMostMessageBox.Show("Invalid password or login name", "Authentification Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Else
                                        NeedNetworkPassword = False
                                    End If
                                End If
                            End While
                        End If

                    End Try

                    If Not CELProject.FastLoading Then
                        Dim mdr As New MappedDriveResolver()
                        If mdr.IsNetworkDrive(CELProject.Database.Path) Then
                            Dim path As String = mdr.ResolveToRootUNC(CELProject.Database.Path)
                            If (Directory.Exists(path)) Then
                                ' If (New DirectoryInfo(path).Exists) Then
                                DatabaseNotPresent = False
                            Else
                                DatabaseNotPresent = True
                                clsTopMostMessageBox.Show("OpenTest_Sequence: Connect: The database path does not exist " & path & vbCrLf & vbCrLf &
                                                 "It is recommended to close the Welcome Screen, fix the problem and restart the application",
                                                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Application.Exit()
                                Return
                            End If
                        Else
                            'BUG: File.Exists et Directory.Exists ne marchent pas retourne toujours False sur des drive mappes.
                            If (File.Exists(CELProject.Database.FullFilePath)) Then
                                DatabaseNotPresent = False
                            Else
                                DatabaseNotPresent = True
                                clsTopMostMessageBox.Show("OpenTest_Sequence: Connect: The database path does not exist " & CELProject.Database.Path.ToString() & vbCrLf & vbCrLf &
                                                 "Usually this error is caused by the T:\ being not mapped" & vbCrLf & vbCrLf &
                                                 "It is recommended to close the Welcome Screen, fix the problem and restart the application",
                                                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Application.Exit()
                                Return
                            End If
                        End If
                    Else
                        DatabaseNotPresent = False
                    End If

                    'Ne pas supprimer.  Pour overrider dynamiquement une connectionstring de entity spaces
                    For Each connElement As esConnectionElement In esConfigSettings.ConnectionInfo.Connections

                        'connElement.ConnectionString = connElement.ConnectionString.Replace("Password=", "Password=CEL9994" + CELProject.Database.Password) 'CELProject.Database.OleDbConnectionString
                        'Solution de contournement pour setter le mot de passe par le code dans la connectionString
                        'Dim connectionString As String = connElement.Replace("Password=", "Password=" + CELProject.Database.Password)

                        Dim newConn As New esConnectionElement
                        newConn.ConnectionString = CELProject.Database.OleDbConnectionString
                        newConn.Name = connElement.Name
                        newConn.Provider = connElement.Provider
                        newConn.ProviderClass = connElement.ProviderClass
                        newConn.SqlAccessType = esSqlAccessType.DynamicSQL
                        newConn.ProviderMetadataKey = connElement.ProviderMetadataKey
                        newConn.CommandTimeout = 20
                        esConfigSettings.ConnectionInfo.Connections.Add(newConn)
                        esConfigSettings.ConnectionInfo.Default = CELProject.DefaultConnectionName
                    Next


                    'Initialize database engine
                    esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()




                    InputOutputRegEx = BuildInputOutputRegExString()


                    CELProject.HMIProject = GetObject("Project")

                    If CELProject.HMIProject Is Nothing Then
                        clsTopMostMessageBox.Show("OpenTestSequence.OnTestSessionOpened: CELProject.HMIProject Is Nothing", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    coreCollectionName = "CORE"
                    Dim coreChannelCollection As KineticaRT.ChannelCollection = CELProject.CreateCollection(coreCollectionName)

                    If Not IsNothing(coreChannelCollection) Then

                        HMIChannelCreation.CreateChannelsEngineBoolean(coreCollectionName, coreChannelCollection, EngineMappingList, BoolInitChannel, BoolInitChannelValue, BoolInitIndex)
                        HMIChannelCreation.CreateChannelsEngineNumeric(coreCollectionName, coreChannelCollection, Nothing, numericCollectionName, EngineMappingList, InputOutputRegEx, Me.GetRegexOptions())
                        HMIChannelCreation.CreateChannelsEngineText(coreCollectionName, coreChannelCollection)

                    End If
                    ConnectCollection(coreCollectionName)

                    coreArchiveEventArchiveCollectionName = "CORE_ARCHIVE"
                    Dim coreArchiveEventArchiveChannelCollection As KineticaRT.ChannelCollection = CELProject.CreateCollection(coreArchiveEventArchiveCollectionName)
                    If Not IsNothing(coreArchiveEventArchiveChannelCollection) Then
                        Initialize_Archive(coreArchiveEventArchiveChannelCollection)
                    End If
                    ConnectCollection(coreArchiveEventArchiveCollectionName)

                    CELProject.Init_Default_Channels()

                    'Verify the connection to the channels
                    If Init_Channels() = False Then
                        clsTopMostMessageBox.Show("OpenTest_Sequence: Connect() - missing channels", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    CELProject.EngineWindowCollection.Item(0).EngineWindow.BeginInvoke(New MethodInvoker(AddressOf Me.ConfigWelcomeScreen))

                    'TODO: les handlers ici pas pour le slave?

                    'Connect our event handlers 
                    AddHandler Cancel_MeasurementUnit.ValueChanged, AddressOf OnCancel_MeasurementUnit
                    AddHandler Cancel_Password.ValueChanged, AddressOf OnCancel_Password
                    AddHandler Cancel_UserInputs.ValueChanged, AddressOf OnCancel_UserInputs
                    If CELProject.ChannelSelectorFormModuleAvailable Then
                        AddHandler frmChannelSelector.Closing, AddressOf OnWindowClosing
                    End If
                    AddHandler _cboEngineModel.SelectedIndexChanged, AddressOf OnEngineModel_List_SelectedIndexChanged
                    AddHandler _cboMeasurementUnit.SelectedIndexChanged, AddressOf OnMeasurementUnit_List_SelectedIndexChanged
                    AddHandler OK_MeasurementUnit.ValueChanged, AddressOf OnOK_MeasurementUnit
                    AddHandler OK_Password.ValueChanged, AddressOf OnOK_Password
                    AddHandler OK_UserInputs.ValueChanged, AddressOf OnOK_UserInputs
                    AddHandler Open_Test.ValueChanged, AddressOf OnOpen_Test
                    AddHandler _frmWelcomeScreen.Closing, AddressOf OnWindowClosing
                    AddHandler DAS_CALIBRATION_BTN.ValueChanged, AddressOf OnDAS_CALIBRATION_BTN
                    AddHandler SYSTEM_CHECK_BTN.ValueChanged, AddressOf OnSYSTEM_CHECK_BTN



                    If Not CELProject.FastLoading Then
                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            Validate_CsvAndHarwareConfiguration()
                        End If
                    End If
                    MyBase.Connect()
                    Me.Connected = True

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Connect: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Private Function BuildInputOutputRegExString() As String

            Dim regularExpression As String = ""

            regularExpression += "((P|T|VIN|VIB|F|VOUT|IIN|IOUT)\d+)" 'Sans les unites est aussi necessaire dans le traitement

            Return regularExpression

        End Function

        Private Function GetRegexOptions() As System.Text.RegularExpressions.RegexOptions

            Dim DotAll As Boolean = False
            Dim MultiLine As Boolean = False
            Dim IgnoreCase As Boolean = False
            Dim ECMAScript As Boolean = False

            ' "Dot all" and "ECMAScript" are mutually exclusive options.
            ' If we include them both, then the Regex() constructor or the 
            ' Regex.Match() method will raise an exception


            'System.Diagnostics.Trace.Assert(!(checkDotAll.Checked && checkECMAScript.Checked), 
            '       "DotAll and ECMAScript options are mutually exclusive")


            ' Construct a RegexOptions object
            ' If the options are predetermined, you don't need to use a RegexOptions object
            ' You can simply pass something like RegexOptions.Multiline | RegexOptions.Ignorecase
            ' directly to the Regex() constructor or the Regex.Match() method
            Dim options As RegexOptions = New RegexOptions()
            ' If true, the dot matches any character, including a newline
            ' If false, the dot matches any character, except a newline
            If (DotAll) Then
                options = options Or RegexOptions.Singleline
            End If
            ' If true, the caret ^ matches after a newline, and the dollar $ matches 
            ' before a newline, as well as at the start and end of the subject string
            ' If false, the caret only matches at the start of the string
            ' and the dollar only at the end of the string
            If (MultiLine) Then
                options = options Or RegexOptions.Multiline
            End If

            ' If true, literal characters and character classes are matched case insensitively
            If (IgnoreCase) Then
                options = options Or RegexOptions.IgnoreCase
            End If
            ' If true, \w, \d and \s match ASCII characters only,
            ' and \10 is backreference 1 followed by a literal 0 rather than octal escape 10.
            If (ECMAScript) Then
                options = options Or RegexOptions.ECMAScript
            End If

            Return options
        End Function


        Public Overloads Overrides Sub Disconnect()

            Try
                If Me.Connected = True Then

                    'TODO: handler pas pour le slave?

                    'Remove Default Handlers
                    CELProject.Dispose_Default_Channels()

                    'Connect our event handlers
                    RemoveHandler Cancel_MeasurementUnit.ValueChanged, AddressOf OnCancel_MeasurementUnit
                    RemoveHandler Cancel_Password.ValueChanged, AddressOf OnCancel_Password
                    RemoveHandler Cancel_UserInputs.ValueChanged, AddressOf OnCancel_UserInputs
                    If CELProject.ChannelSelectorFormModuleAvailable Then
                        RemoveHandler frmChannelSelector.Closing, AddressOf OnWindowClosing
                    End If
                    RemoveHandler _cboEngineModel.SelectedIndexChanged, AddressOf OnEngineModel_List_SelectedIndexChanged
                    RemoveHandler _cboMeasurementUnit.SelectedIndexChanged, AddressOf OnMeasurementUnit_List_SelectedIndexChanged
                    RemoveHandler OK_MeasurementUnit.ValueChanged, AddressOf OnOK_MeasurementUnit
                    RemoveHandler OK_Password.ValueChanged, AddressOf OnOK_Password
                    RemoveHandler OK_UserInputs.ValueChanged, AddressOf OnOK_UserInputs
                    RemoveHandler Open_Test.ValueChanged, AddressOf OnOpen_Test
                    RemoveHandler _frmWelcomeScreen.Closing, AddressOf OnWindowClosing
                    RemoveHandler DAS_CALIBRATION_BTN.ValueChanged, AddressOf OnDAS_CALIBRATION_BTN
                    RemoveHandler SYSTEM_CHECK_BTN.ValueChanged, AddressOf OnSYSTEM_CHECK_BTN


                    MyBase.Disconnect()

                    Me.Connected = False
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Disconnect: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub ResetOutputsOnCLose()
            'Permet de réinitialiser les valeurs des channel voir champ FinalValue des tables HMI_Boolean et Numeric
            Try

                Dim dynamicChannelCollection As New UUTDynamicChannelCollectionCollection()
                dynamicChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.EngineModel = CELProject.EngineModel)
                dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.Unavailable = False)
                dynamicChannelCollection.Query.OrderBy(dynamicChannelCollection.Query.LoadOrder.Ascending)
                dynamicChannelCollection.Query.Load()

                Dim boolColl As New HMIBooleanChannelsCollection()
                boolColl.es.Connection.Name = CELProject.DefaultConnectionName
                Dim numColl As New HMINumericChannelsCollection()
                numColl.es.Connection.Name = CELProject.DefaultConnectionName
                Dim textColl As New HMITextChannelsCollection()
                textColl.es.Connection.Name = CELProject.DefaultConnectionName

                Dim boolQuery As New HMIBooleanChannelsQuery()
                boolQuery.es.DefaultConjunction = esConjunction.Or
                Dim numQuery As New HMINumericChannelsQuery()
                numQuery.es.DefaultConjunction = esConjunction.Or
                Dim textQuery As New HMITextChannelsQuery()
                textQuery.es.DefaultConjunction = esConjunction.Or

                If dynamicChannelCollection.Count > 0 Then

                    'Load All possible mapping for different collection name in a SPECIFIC ENGINE MODEL
                    For Each record As UUTDynamicChannelCollection In dynamicChannelCollection

                        boolQuery.Where(boolQuery.CollectionName = record.CollectionName And boolQuery.Unavailable = False)
                        numQuery.Where(numQuery.CollectionName = record.CollectionName And numQuery.Unavailable = False)
                        textQuery.Where(textQuery.CollectionName = record.CollectionName And textQuery.Unavailable = False)

                    Next



                    boolColl.Load(boolQuery)
                    numColl.Load(numQuery)
                    textColl.Load(textQuery)
                End If
                If boolColl.Count > 0 Then
                    For Each booleanChannel As HMIBooleanChannels In boolColl


                        If booleanChannel.FinalValue = eBooleanState.True Then
                            Dim channel As Object = Me.GetChannel(booleanChannel.ChannelName)
                            If Not channel Is Nothing Then

                                If Not channel.GetType() Is GetType(KineticaRT.Logic.Channels.BooleanExpression) And Not channel.GetType() Is GetType(CEL.KineticaRT.Servers.Logger.BooleanExpression) Then
                                    channel.Value = Values.True
                                Else
                                    clsTopMostMessageBox.Show("OpenTest_Sequence.ResetOutputsOnCLose: Cannot set value on Expression Channel " & booleanChannel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            Else
                                clsTopMostMessageBox.Show("OpenTest_Sequence.ResetOutputsOnCLose:  Channel " & booleanChannel.ChannelName & " not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            Continue For
                        ElseIf booleanChannel.FinalValue = eBooleanState.False Then
                            Dim channel As Object = Me.GetChannel(booleanChannel.ChannelName)

                            If Not channel Is Nothing Then

                                If Not channel.GetType() Is GetType(KineticaRT.Logic.Channels.BooleanExpression) And Not channel.GetType() Is GetType(CEL.KineticaRT.Servers.Logger.BooleanExpression) Then
                                    channel.Value = Values.False
                                Else
                                    clsTopMostMessageBox.Show("OpenTest_Sequence.ResetOutputsOnCLose: Cannot set value on Expression Channel " & booleanChannel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            Else
                                clsTopMostMessageBox.Show("OpenTest_Sequence.ResetOutputsOnCLose:  Channel " & booleanChannel.ChannelName & " not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            Continue For
                        End If

                    Next
                End If

                If numColl.Count > 0 Then
                    For Each numericChannel As HMINumericChannels In numColl

                        If Not IsNothing(numericChannel.FinalValue) AndAlso IsNumeric(numericChannel.FinalValue) Then
                            Dim channel As Object = Me.GetChannel(numericChannel.ChannelName)
                            If Not channel Is Nothing Then

                                If Not channel.GetType() Is GetType(KineticaRT.Logic.Channels.NumericExpression) And Not channel.GetType() Is GetType(CEL.KineticaRT.Servers.Logger.NumericThresholdExpression) Then
                                    channel.Value = New KineticaRT.Values.Double(numericChannel.FinalValue)
                                Else
                                    clsTopMostMessageBox.Show("OpenTest_Sequence.ResetOutputsOnCLose: Cannot set value on Expression Channel " & numericChannel.ChannelName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            Else
                                clsTopMostMessageBox.Show("OpenTest_Sequence.ResetOutputsOnCLose:  Channel " & numericChannel.ChannelName & " not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            Continue For
                        End If
                    Next
                End If
                If textColl.Count > 0 Then
                    For Each textChannel As HMITextChannels In textColl

                        If Not IsNothing(textChannel.FinalValue) AndAlso textChannel.FinalValue & "" <> "" Then
                            Dim channel As Object = Me.GetChannel(textChannel.ChannelName)
                            If Not channel Is Nothing Then
                                channel.Value = New KineticaRT.Values.Text(textChannel.FinalValue)
                            Else
                                clsTopMostMessageBox.Show("OpenTest_Sequence.ResetOutputsOnCLose:  Channel " & textChannel.ChannelName & " not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            Continue For
                        End If
                    Next
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.ResetOutputsOnCLose:  " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Public Function Close_HMI(ByVal engine_model As String) As Boolean
            'Dim channelsInfo As KineticaRT.Studio.Application.Project.ChannelsInfo
            Close_HMI = False

            Try

                Using transaction As New esTransactionScope()

                    ''Permet de remettre des channels dans un état spécifique
                    ''Pour le moment on ne le fait que pour le Critical. Normalement il ne devrait pas y avoir de commande vers les DOUT dans le performance.
                    ''TODO: Eventuellement on devrait pouvoir avoir des DOUT sur n'importe quel HMI.
                    'If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    '    ResetOutputsOnCLose()  transféré dans l'événement OnCLoseTestBtn
                    'End If

                    If CELProject.ChannelSelectorFormModuleAvailable Then
                        frmChannelSelector.Visible = False
                    End If

                    For Each window As CELDAS.sEngineWindowInfo In CELProject.EngineWindowCollection
                        If window.WindowName = "Welcome_Screen" Then
                            'window.EngineWindow.Visible = True
                            window.EngineWindow.Show()
                        Else
                            'window.EngineWindow.Visible = False
                            window.EngineWindow.Hide()
                            Application.DoEvents()
                        End If
                    Next

                    CELProject.LogPerfSynch.Clear() 'Clears Handlers sur les Channel Moteurs

                    RemoveCollection(engineCollectionName)
                    If CELProject.EngineModel <> "SYSTEM CHECK" AndAlso CELProject.EngineModel <> "DAS CALIBRATION" Then
                        RemoveCollection(messagesCollectionName)
                    End If
                    RemoveCollection(arincUartCollectionName)
                    If CELProject.EngineModel IsNot Nothing Then
                        LoadUnloadStaticCollections(CELProject.EngineModel, True)
                    End If
                    RemoveCollection(logPerfCollectionName)
                    RemoveCollection(numericCollectionName)
                    RemoveCollection(booleanCollectionName)
                    RemoveCollection(canBusCollectionName)

                    If CELProject.DebugMode = True And CELProject.EngineModel <> "SYSTEM CHECK" AndAlso CELProject.EngineModel IsNot Nothing Then
                        LoadUnloadStaticCollections("SYSTEM CHECK", True)
                        RemoveCollection("SYSTEM_CHECK")
                    End If

                    transaction.Complete()
                    Close_HMI = True
                End Using

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Close_HMI: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Private Sub ConfigWelcomeScreen()

            Try
                Dim lblX64Edition As Label = Nothing
                Dim lblProjectNumber As System.Windows.Forms.Label = Nothing ' System.Windows.Forms.Label = Nothing
                Dim lblDatabaseConnection As System.Windows.Forms.Label = Nothing
                Dim backgroundImage As System.Drawing.Image = Nothing

                MyBase.InitializeObject(lblProjectNumber, "Welcome_Screen.ProjectNumberTxt")
                MyBase.InitializeObject(lblDatabaseConnection, "Welcome_Screen.lblDatabaseConnection")
                MyBase.InitializeObject(lblX64Edition, "Welcome_Screen.lblX64Edition")

                lblProjectNumber.Text = "Project " & CELProject.Project.Number.ToString()
                backgroundImage = Image.FromFile(CELProject.Project.WelcomeScreenSkinFullFileName.ToString())

                Dim processHelper As New ProcessHelper()

                If processHelper.Is64BitProcess() Then
                    lblX64Edition.Text = "x64 Edition"
                Else
                    lblX64Edition.Text = "x86 Edition"
                End If


                If Impersonation.LoggedOn Then
                    lblDatabaseConnection.Text = "Connected to " + CELProject.Database.FullFilePath
                Else
                    lblDatabaseConnection.Text = "Unable to connect to " + CELProject.Database.FullFilePath
                End If
                If CELProject.DebugMode Then
                    lblDatabaseConnection.Text += vbCrLf + "Debug Mode is ON"
                    DEBUG_MODE.ValueObject = True
                Else
                    DEBUG_MODE.ValueObject = False
                End If
                If CELProject.DataAcquisitionMode = CELDAS.eDataAcquisitionMode.SIMULATION Then
                    lblDatabaseConnection.Text += vbCrLf + "Simulation Mode is ON"
                End If



                Dim pnlWelcomeScreeen As KineticaRT.Studio.Window = Nothing


                For Each ctl As System.Windows.Forms.Control In _frmWelcomeScreen.Controls
                    If TypeOf ctl Is KineticaRT.Controls.Control Then
                        If TypeOf ctl Is KineticaRT.Controls.Tab Then
                            Dim tabWelcomeScreen As KineticaRT.Controls.Tab = ctl
                            'For Each ctl As System.Windows.Forms.Control In tabWelcomeScreen.TA
                            For Each ctl2 As System.Windows.Forms.Control In tabWelcomeScreen.Controls
                                If TypeOf ctl2 Is KineticaRT.Controls.PanelBase Then
                                    Dim pnl As KineticaRT.Controls.PanelBase = ctl2
                                    pnl.BackgroundImage = backgroundImage
                                    Exit For
                                    'For Each ctl3 As System.Windows.Forms.Control In pnl.Controls
                                    '    If TypeOf ctl3 Is KineticaRT.Controls.Label Then
                                    '        'ctl3.Invalidate()
                                    '    End If
                                    'Next

                                End If
                            Next
                        End If
                    End If
                Next


                UpdateCeldasStatusFrame()

            Catch ex As Exception
                clsTopMostMessageBox.Show("ConfigWelcomeScreen: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Structure CeldasStatusControl
            Public isDisabled As Boolean
            Public handle As KineticaRT.Studio.Controls.Display
        End Structure

        Public Function MoveCeldasStatusControl(ByVal position As Integer, ByRef posX As Integer, ByRef posY As Integer) As Boolean
            If (position < 0) Then
                'Invalid Position
                Return False
            Else
                posX = 8 + (308 * (position Mod 4))
                posY = 8 + (32 * CInt(Math.Floor(position / 4)))
                Return True
            End If
        End Function

        Public Function MoveCeldasStatusFrame(ByVal height As Integer, ByRef posY As Integer) As Boolean

            posY = 975 - height 'Adjust form position (bottom as a fixed point at 934px)
            Return True

        End Function

        Public Function ResizeCeldasStatusFrame(ByVal nbElements As Integer, ByRef width As Integer, ByRef height As Integer) As Boolean

            If nbElements > 0 Then
                height = 28 + 8 + (32 * CInt(Math.Ceiling(nbElements / 4))) + 8 'Resize form
                Return True
            End If

            Return False

        End Function

        Private Sub UpdateCeldasStatusFrame()

            Dim resourceCounter As Integer = 0
            Dim resourcePosition As Integer = 0
            Dim resourceQtyVisible As Integer = 0
            Try
                Dim resourceList() As CeldasStatusControl
                Dim frmCeldasStatus As KineticaRT.Controls.Frame = CELProject.SearchControl(_frmWelcomeScreen, "fraCeldasStatus")

                'Nidaq
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/NIDAQ_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspNIDAQ_Status")
                resourceCounter += 1

                'Serial
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/SERIAL_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspSerial_Status")
                resourceCounter += 1

                'OmniBusBox
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/OMNIBUSBOX_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspOmnibusbox_Status")
                resourceCounter += 1

                'PowerDNA
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/POWERDNA_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspPowerDNA_Status")
                resourceCounter += 1

                'UUT Controller
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/UUT_CONTROLLER_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspUUT_Controller_Status")
                resourceCounter += 1

                'UUT Cruncher
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/UUT_CRUNCHER_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspUUT_Cruncher_Status")
                resourceCounter += 1

                'Arinc
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/SERIAL_ARINC_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspSerial_Arinc_Status")
                resourceCounter += 1

                'Scanivalve Temperature
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/SCANNIVALVE_TEMPERATURE_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspScanivalve_Temp_Status")
                resourceCounter += 1

                'Scanivalve Pressure
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/SCANNIVALVE_PRESSURE_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspScanivalve_Pressure_Status")
                resourceCounter += 1

                'Smart Motor
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/SM1720_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspSM1720_Status")
                resourceCounter += 1

                'Gali
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/GALIL_CONTROLLER_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspGlail_Controller_Status")
                resourceCounter += 1

                'Texcel V100
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/TEXCEL_V100_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspTexcel_V100_Status")
                resourceCounter += 1

                'Texcel VX100
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/TEXCEL_VX100_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspTexcel_VX100_Status")
                resourceCounter += 1

                'Load Bank Controller
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/LOAD_BANK_CONTROLLER_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspLoad_Bank_Controller_Status")
                resourceCounter += 1

                'Facility Controller
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/FACILITY_CONTROLLER_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspFacility_Controller_Status")
                resourceCounter += 1

                'AFDX
                ReDim Preserve resourceList(resourceCounter)
                resourceList(resourceCounter).isDisabled = CELProject.Project.ReadOneBooleanConfigItem("//ConfigurationSettings/RESSOURCE/AFDX_DISABLED", True)
                resourceList(resourceCounter).handle = CELProject.SearchControl(_frmWelcomeScreen, "dspAFDX_Status")
                resourceCounter += 1

                'Manipulate Control
                For i As Integer = 0 To (resourceCounter - 1)
                    If resourceList(i).isDisabled Then
                        If Not resourceList(i).handle Is Nothing Then resourceList(i).handle.Visible = False
                    Else
                        If Not resourceList(i).handle Is Nothing Then
                            resourceList(i).handle.Visible = True
                            MoveCeldasStatusControl(resourcePosition, resourceList(i).handle.Left, resourceList(i).handle.Top) 'Set Position by reference
                            resourcePosition += 1 'Position of visible resources (First position is at 0).
                            resourceQtyVisible += 1 'Number of visible resources.
                        End If
                    End If
                Next

                'Manipulate Form
                If Not resourceQtyVisible = 0 Then
                    ResizeCeldasStatusFrame(resourceQtyVisible, frmCeldasStatus.Width, frmCeldasStatus.Height)
                    MoveCeldasStatusFrame(frmCeldasStatus.Height, frmCeldasStatus.Top)
                End If

            Catch ex As Exception
                Debug.Assert(False, "Error from: HMI.OpenTest_Sequence.UpdateCeldasStatusFrame: Problem reformating the size of the ressource pannel.", ex.Message)
            End Try
        End Sub


#Region " Validation Functions "

        Private Function Validate_ConfigurationFile() As Boolean

            If CELProject.EngineModel <> "DAS CALIBRATION" And CELProject.EngineModel <> "SYSTEM CHECK" Then
                If IsNothing(CELProject.EngineConfiguration) Then
                    clsTopMostMessageBox.Show("The EngineConfiguration section for the selected engine is not defined in your XML Configuration file (.exe.config) ", "Configuration file error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If
                Return True
            Else
                Return True
            End If
        End Function

        Private Function Validate_CsvAndHarwareConfiguration() As Boolean
            Validate_CsvAndHarwareConfiguration = False
            Validate_Qnx_CSV_File()
            Validate_ConfigurationFileIntegrity()
            Validate_DACN1_Serial_IOMGR_Real()
            Validate_DACN1_PowerDNA_IOMGR_Real()
            Validate_CSV_CalibrationExpireDate()
            Return True
        End Function

        Private Function Validate_DACN1_PowerDNA_IOMGR_Real() As Boolean
            Validate_DACN1_PowerDNA_IOMGR_Real = False

            Try

                Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
                dacnRealChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                dacnRealChannelCollection.Query.es.QuerySource = "DACN1_PowerDNA_IOMGR_Real_1" 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
                dacnRealChannelCollection.Query.Load()

                For Each channel As DacnTemplateReal In dacnRealChannelCollection
                    If channel.EngUnit = "" Or channel.EngUnitID = 10 Then
                        clsTopMostMessageBox.Show("DACN1_PowerDNA_IOMGR_Real_1 configuration error on " + channel.Name + ".  EngUnit or EngUnitID is missing ", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Next

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Validate_DACN1_PowerDNA_IOMGR_Real: " + ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function
        ''' <summary>
        ''' Validate the integrity of the CSV files on the Qnx server by comparing time of creation and size.
        ''' </summary>
        ''' <remarks>Require both file to be in their expected location for the sub to verify integrity. Will pop a msgbox if not possible.</remarks>
        Private Function Validate_Qnx_CSV_File()
            Validate_Qnx_CSV_File = False
            Try
                Dim validationSoftware As New CEL.HMI.HardwareConfiguration
                If validationSoftware.CsvConfigIntegrityVerification(CELProject.QNX.UploadPath, CELProject.QNX.IpAdress, CELProject.QNX.UserName, CELProject.QNX.Password) Then
                    Return True
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Validate_Qnx_CSV_File: " + ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function
        ''' <summary>
        ''' Validate the integrity of the celdas.cfg File on the Qnx server by comparing time of creation and size.
        ''' </summary>
        ''' <remarks>Require both file to be in their expected location for the sub to verify integrity. Will pop a msgbox if not possible.</remarks>
        Private Function Validate_ConfigurationFileIntegrity()
            Validate_ConfigurationFileIntegrity = False
            Try
                Dim validationSoftware As New CEL.HMI.HardwareConfiguration
                If validationSoftware.ConfigFileIntegrityVerification("./", CELProject.QNX.IpAdress, CELProject.QNX.UserName, CELProject.QNX.Password) Then
                    Return True
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Validate_ConfigurationFileIntegrity: " + ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Private Function Validate_DACN1_Serial_IOMGR_Real() As Boolean
            Validate_DACN1_Serial_IOMGR_Real = False

            Try

                Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
                dacnRealChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                dacnRealChannelCollection.Query.es.QuerySource = "DACN1_Serial_IOMGR_Real" 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
                dacnRealChannelCollection.Query.Load()

                For Each channel As DacnTemplateReal In dacnRealChannelCollection
                    'Modele de Puck 4 Ports dont l'interrogation se fait un port à la fois
                    If channel.DeviceID = "SCM9B-3172" Or
                       channel.DeviceID = "SCM9B-3182" Or
                       channel.DeviceID = "SCM9B-3262" Or
                       channel.DeviceID = "SCM9B-5122" Or
                       channel.DeviceID = "SCM9B-5132" Or
                       channel.DeviceID = "SCM9B-5142" Then
                        If channel.PhysicalChannel <> 1 Then
                            clsTopMostMessageBox.Show("DACN1_Serial_IOMGR_Real configuration error on " + channel.Name + ".  The physicalChannel should be 1. " +
                                             "If this is the 2nd, 3rd et 4th bit of a puck physicalChannel should all be set to 1 and " +
                                             "all the Base_Adress should be something 5, 6, 7, 8 (5 for port 1, 6 for port 2 and so on)", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    ElseIf channel.DeviceID = "SCM9B-5322" Then 'Puck de temperature qui se lit en block de 4
                        If channel.PhysicalChannel < 1 Or channel.PhysicalChannel > 4 Then
                            clsTopMostMessageBox.Show("DACN1_Serial_IOMGR_Real configuration error on " + channel.Name + ".  The physicalChannel should be between 1 and 4. ", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    End If
                Next
                Return True
            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Validate_DACN_Serial: " + ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function

        Private Function Validate_CSV_CalibrationExpireDate()
            Validate_CSV_CalibrationExpireDate = False

            Dim tableName As String = ""
            Dim channelName As String = ""
            Try
                Dim ds As New DataSet
                Dim dt As New DataTable
                Dim dr As DataRow
                Dim idColumn As DataColumn
                Dim nameColumn As DataColumn
                Dim descriptionColumn As DataColumn
                Dim dateColumn As DataColumn

                Dim i As Integer

                Dim provider As CultureInfo = CultureInfo.InvariantCulture
                Dim nbrTable As Integer = CELProject.CSV_NumericTables.GetUpperBound(0)
                Dim channelCounter As Integer = 0

                dt = New DataTable()
                idColumn = New DataColumn("ID", Type.GetType("System.Int32"))
                nameColumn = New DataColumn("Name", Type.GetType("System.String"))
                descriptionColumn = New DataColumn("Description", Type.GetType("System.String"))
                dateColumn = New DataColumn("Date", Type.GetType("System.String"))

                dt.Columns.Add(idColumn)
                dt.Columns.Add(nameColumn)
                dt.Columns.Add(descriptionColumn)
                dt.Columns.Add(dateColumn)

                Dim k As Integer = 0
                For i = 0 To nbrTable

                    'Query Database Real
                    Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
                    dacnRealChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                    dacnRealChannelCollection.Query.es.QuerySource = CELProject.CSV_NumericTables(i) 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                    dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
                    dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
                    dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.DataProcessingOn = True)
                    For j As Integer = 0 To CELProject.NO_CALIBRATION_CHANNEL.GetUpperBound(0)
                        dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.Name.NotLike(CELProject.NO_CALIBRATION_CHANNEL(j)))
                    Next
                    dacnRealChannelCollection.Query.Load()

                    For Each channel As DacnTemplateReal In dacnRealChannelCollection
                        channelName = channel.Name
                        If channel.LastCalibDate = "" Then

                            channelCounter += 1

                            dr = dt.NewRow()
                            k += 1
                            dr("ID") = k
                            dr("Name") = channel.Name
                            If CELProject.Project.Number = 12917 Then
                                dr("Description") = "Date de calibration vide"
                            Else
                                dr("Description") = "Calibration date is empty"
                            End If
                            dr("Date") = channel.LastCalibDate
                            dt.Rows.Add(dr)

                        Else
                            Dim LastCalibDate As System.DateTime
                            Dim diff As Integer
                            Dim noPreviousCalibration As Boolean

                            If channel.LastCalibDate <> String.Empty Then
                                LastCalibDate = System.DateTime.ParseExact(channel.LastCalibDate, "yyyy/MM/dd", provider)
                                diff = DateDiff(DateInterval.Day, LastCalibDate, Now)
                            Else
                                noPreviousCalibration = True
                            End If

                            If diff > 365 Or noPreviousCalibration Then
                                channelCounter += 1

                                dr = dt.NewRow()
                                k += 1
                                dr("ID") = k
                                dr("Name") = channel.Name
                                If CELProject.Project.Number = 12917 Then
                                    dr("Description") = "Date de calibration expirée"
                                Else
                                    dr("Description") = "Calibration date is expired"
                                End If

                                If noPreviousCalibration Then
                                    dr("Date") = String.Empty
                                Else
                                    dr("Date") = channel.LastCalibDate
                                End If
                                dt.Rows.Add(dr)
                            End If
                        End If
                    Next
                Next i

                If channelCounter > 0 Then

                    ds.Tables.Add(dt)
                    Dim columnsWith(0 To 3) As Integer
                    columnsWith(0) = 75
                    columnsWith(1) = 125
                    columnsWith(2) = 250
                    columnsWith(3) = 100
                    If CELProject.Project.Number = 12917 Then
                        Dim frmResults As New CELResultListViewForm("Validation des paramètres de Calibration", "La calibration de certains paramètres sont soit expirés ou invalides.", ds, columnsWith)
                        frmResults.Show()
                    Else
                        Dim frmResults As New CELResultListViewForm("Calibration", "The calibration of some parameters are expired.", ds, columnsWith)
                        frmResults.Show()
                    End If

                End If
                Return True
            Catch ex As System.Data.OleDb.OleDbException
                Debug.Assert(False, "Error: OpenTest_Sequence.Validate_CSV_CalibrationExpireDate: Could not properly connect to database:", ex.Message)
            Catch ex As Exception
                Debug.Assert(False, "OpenTest_Sequence.Validate_CSV_CalibrationExpireDate: The table name is:" & tableName & " and the channel name is: " & channelName, ex.Message)
                'MessageBox.Show("OpenTest_Sequence.Validate_CSV_CalibrationExpireDate: This table, " & channelName & "is expired. Error message: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function

        Private Sub Validate_Collection(ByRef ChannelCollection As KineticaRT.ChannelCollection)
            For Each keyval As Object In ChannelCollection
                If keyval.Value.Value = Values.NotConnected Then
                    Console.WriteLine("OpentTest.ValidateCollection: Channel " + keyval.Key.ToString() + " Is Not Connected")
                    'Au moment de faire un connect sur une collection ou sur un channel, le server répond et génère un ValueChanged pour updater sa valeur mais
                    'ca ne se fait pas instantanément, alors il se peut que si on Call Validate_Collection que la Value soit Not Connected.

                End If
            Next
        End Sub

        Private Sub Validate_HmiMappingFieldIntegrity()
            Try

                For Each item As KeyValuePair(Of String, String) In EngineMappingList

                    Dim dynamicChannelCollection As New UUTDynamicChannelCollectionCollection()
                    dynamicChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                    dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.ProjectID = CELProject.Project.Number)
                    dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.EngineModel = CELProject.EngineModel)
                    dynamicChannelCollection.Query.OrderBy(dynamicChannelCollection.Query.LoadOrder.Ascending)
                    dynamicChannelCollection.Query.Load()

                    Dim boolColl As New HMIBooleanChannelsCollection()
                    boolColl.es.Connection.Name = CELProject.DefaultConnectionName
                    Dim numColl As New HMINumericChannelsCollection()
                    numColl.es.Connection.Name = CELProject.DefaultConnectionName


                    Dim boolQuery As New HMIBooleanChannelsQuery()
                    boolQuery.es.DefaultConjunction = esConjunction.Or
                    Dim numQuery As New HMINumericChannelsQuery()
                    numQuery.es.DefaultConjunction = esConjunction.Or

                    If dynamicChannelCollection.Count > 0 Then

                        'Load All possible mapping for different collection name in a SPECIFIC ENGINE MODEL
                        For Each record As UUTDynamicChannelCollection In dynamicChannelCollection

                            boolQuery.Where(boolQuery.CollectionName = record.CollectionName And boolQuery.Unavailable = False And (boolQuery.Source = item.Key Or boolQuery.Destination = item.Key))
                            numQuery.Where(numQuery.CollectionName = record.CollectionName And numQuery.Unavailable = False And (numQuery.Source = item.Key Or numQuery.Destination = item.Key))

                        Next

                    End If

                    boolColl.Load(boolQuery)
                    numColl.Load(numQuery)

                    If boolColl.Count = 0 And numColl.Count = 0 Then
                        Dim msg As String = "The mapping field '" + item.Key + "'  is not used in Tables  HMI Boolean or HMI Numeric "
                        If CELProject.NoPopupError Then
                            Console.WriteLine(msg)
                        Else
                            clsTopMostMessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                        End If

                    End If

                Next
            Catch ex As Exception
                Dim msg As String = "OpenTest_Sequence.ValidateHmiMappingFieldIntegrity: " + ex.ToString()
                If CELProject.NoPopupError Then
                    Console.WriteLine(msg)
                Else
                    clsTopMostMessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try
        End Sub

        Function Validate_UserPasswordAndSecurityAccess() As Long

            Dim passwordMatch As Boolean = False

            Try

                Dim engineSelected As New Uut

                If CELProject.EngineModel = "DAS CALIBRATION" Or CELProject.EngineModel = "SYSTEM CHECK" Then
                    Dim UUTfromTestSession As New Uut

                    UUTfromTestSession.es.Connection.Name = CELProject.DefaultConnectionName
                    UUTfromTestSession.Query.Where(UUTfromTestSession.Query.ProjectID = CELProject.Project.Number)
                    UUTfromTestSession.Query.Where(UUTfromTestSession.Query.EngineModel = CELProject.EngineModel)
                    UUTfromTestSession.Query.Load()

                    engineSelected = UUTfromTestSession

                ElseIf _cboEngineModel.SelectedItem Is Nothing Then 'This should never happen, just in case
                    clsTopMostMessageBox.Show("Error: The selection of the engine is Nothing, Security level has been forced to TestManager", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    engineSelected.SecurityLevelID = 3

                Else
                    engineSelected = _cboEngineModel.SelectedItem
                End If

                Dim usersCollection As UsersCollection = New UsersCollection()
                usersCollection.Query.Where(usersCollection.Query.ProjectID = CELProject.Project.Number)
                usersCollection.Query.Where(usersCollection.Query.Password = _txtPassword.Text)
                usersCollection.Query.Load()

                If usersCollection.Count = 0 Then
                    clsTopMostMessageBox.Show("Error: Wrong Password", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else

                    CELProject.CurrentUser = usersCollection.Item(0)

                    'If engine.Available = False Then
                    '    MessageBox.Show("Error: Engine not available", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Else
                    If (CELProject.CurrentUser.SecurityLevelID < engineSelected.SecurityLevelID) Then
                        clsTopMostMessageBox.Show("Error: Permission Denied", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else

                        CELProject.Test_Mgr_User_ID = GetChannel("Test_Mgr_User_ID")
                        CELProject.Test_Mgr_User_ID.Value = New KineticaRT.Values.Text(CELProject.CurrentUser.OperatorID)
                        CELProject.Test_Mgr_User_Name = GetChannel("Test_Mgr_User_Name")
                        CELProject.Test_Mgr_User_Name.Value = New KineticaRT.Values.Text(CELProject.CurrentUser.OperatorName)

                        Return True
                    End If
                End If
                'End If
                'End If
            Catch ex As Exception
                Dim msg As String = "OpenTest_Sequence.Get_Open_Test_Results: " & ex.ToString()
                If CELProject.NoPopupError Then
                    Console.WriteLine(msg)
                Else
                    clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try
            Return False
        End Function

#End Region

        Private Function CreateChannelsDefaultCANBus(ByRef channelCollection As KineticaRT.ChannelCollection) As Boolean

            Const CANBusRX As UInt32 = 0
            Const CANBusTX As UInt32 = 1

            CreateChannelsDefaultCANBus = False

            Try

                Dim dacnCANBusChannelCollection As Dacn1CanbusLabelCollection = New Dacn1CanbusLabelCollection()

                dacnCANBusChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                dacnCANBusChannelCollection.Query.Where(dacnCANBusChannelCollection.Query.EngineModel = CELProject.EngineModel)
                dacnCANBusChannelCollection.Query.OrderBy(dacnCANBusChannelCollection.Query.ChannelName.Ascending)
                dacnCANBusChannelCollection.Query.Load()

                Dim NumericLink_Channel As CEL.KineticaRT.Servers.Logger.NumericThresholdLink

                For Each channel As Dacn1CanbusLabel In dacnCANBusChannelCollection

                    'Create Numeric Link Channels
                    NumericLink_Channel = New CEL.KineticaRT.Servers.Logger.NumericThresholdLink

                    NumericLink_Channel.Description = channel.Description

                    NumericLink_Channel.Destination = ""

                    If Not (channel.DefaultTxChannel Is Nothing) And channel.TxRx = CANBusTX Then
                        NumericLink_Channel.Source = channel.DefaultTxChannel
                    Else
                        NumericLink_Channel.Source = ""
                    End If

                    NumericLink_Channel.Enabled = channel.LoggerEnabled

                    If channel.LoggerEnabled = False Or channel.LoggerIndex Is Nothing Then
                        NumericLink_Channel.Index = 0
                    Else
                        NumericLink_Channel.Index = channel.LoggerIndex
                    End If

                    NumericLink_Channel.Logger = recorderName
                    NumericLink_Channel.EventLogger = eventRecorderName
                    NumericLink_Channel.DecimalDigits = channel.DecimalDigits
                    NumericLink_Channel.Format = channel.DisplayFormat

                    If Not (channel.EngUnitID Is Nothing) Then
                        NumericLink_Channel.Units = CELProject.GetUnitDisplayNameById(channel.EngUnitID)
                    Else
                        NumericLink_Channel.Units = ""
                    End If

                    NumericLink_Channel.Name = channel.ChannelName

                    NumericLink_Channel.HighColor = System.Drawing.Color.White
                    NumericLink_Channel.HighHighColor = System.Drawing.Color.White
                    NumericLink_Channel.LowColor = System.Drawing.Color.White
                    NumericLink_Channel.LowLowColor = System.Drawing.Color.White

                    channelCollection.Add(NumericLink_Channel)

                Next

                Dim CurCANBusKTChannel As IChannel
                For Each channel As Dacn1CanbusLabel In dacnCANBusChannelCollection
                    CurCANBusKTChannel = GetChannel(channel.ChannelName.ToString())

                    If Not (CurCANBusKTChannel Is Nothing) Then
                        Select Case channel.TxRx
                            Case CANBusRX
                                CurCANBusKTChannel.Value = Values.NaN
                            Case CANBusTX
                                If Not (channel.DefaultTxValue.ToString() Is Nothing) Then
                                    CurCANBusKTChannel.Value = New KineticaRT.Values.Double(channel.DefaultTxValue)
                                Else
                                    CurCANBusKTChannel.Value = Values.NaN
                                End If
                        End Select
                    End If
                Next

                CreateChannelsDefaultCANBus = True

            Catch ex As Exception
                Dim msg As String = "OpenTest_Sequence.CreateChannels: " & ex.ToString()
                If CELProject.NoPopupError Then
                    Console.WriteLine(msg)
                Else
                    clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try

        End Function

        Private Function CreateChannelsDefaultNumeric(ByRef channelCollection As KineticaRT.ChannelCollection) As Boolean

            CreateChannelsDefaultNumeric = False

            Dim i As Integer
            Dim nbrTable As Integer
            Dim opcBooleanChannel As KineticaRT.Servers.OpcClient.BooleanItem
            Dim opcNumericChannel As KineticaRT.Servers.OpcClient.Item

            Try

                nbrTable = CELProject.CSV_NumericTables.GetUpperBound(0)

                For i = 0 To nbrTable

                    'Query Database Real
                    'esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                    Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
                    dacnRealChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                    dacnRealChannelCollection.Query.es.QuerySource = CELProject.CSV_NumericTables(i) 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                    dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
                    dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
                    dacnRealChannelCollection.Query.Load()

                    For Each channel As DacnTemplateReal In dacnRealChannelCollection

                        opcNumericChannel = HMIChannelCreation.CreateChannelNumericOpcItem(channel)

                        channelCollection.Add(opcNumericChannel)
                        Try
                            'ChannelList.Add(channel.Name, channel.UpToEngineeringUnitByEngUnitID.Name)
                            CELProject.ChannelList.Add(channel.Name, CELProject.GetUnitNameById(channel.EngUnitID))
                        Catch ex As Exception
                            Dim msg As String = "OpenTest_Sequence.CreateChannelsDefaultNumeric: Duplicate channel " + channel.Name
                            If CELProject.NoPopupError Then
                                Console.WriteLine(msg)
                            Else
                                clsTopMostMessageBox.Show(msg, "ChannelList.Add", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                        End Try

                        'Add Channel MEAN
                        Dim channelMean = HMIChannelCreation.CreateChannelNumericExpressionMean(channel)

                        channelCollection.Add(channelMean)

                        'We added a new functionnality for the Power DNA temperature, An OPC tag for Out Of Service
                        If (opcNumericChannel.ItemID.Contains(".t[") = True) Then

                            opcBooleanChannel = New KineticaRT.Servers.OpcClient.BooleanItem

                            opcBooleanChannel.Description = opcNumericChannel.Name + "_OutOfService"
                            opcBooleanChannel.Name = opcNumericChannel.Name + "_OutOfService"
                            opcBooleanChannel.Group = opcNumericChannel.Group
                            opcBooleanChannel.ItemID = opcNumericChannel.ItemID.Replace(".mean_value", "")
                            opcBooleanChannel.ItemID = opcBooleanChannel.ItemID.Replace("[", "_out_of_service[")

                            channelCollection.Add(opcBooleanChannel)

                        End If
                    Next
                Next

                CreateChannelsDefaultNumeric = True

            Catch ex As Exception
                Dim msg As String = "OpenTest_Sequence: CreateChannels: " & ex.ToString()
                If CELProject.NoPopupError Then
                    Console.WriteLine(msg)
                Else
                    clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try

        End Function

        Private Function CreateChannelsDefaultBoolean(ByRef channelCollection As KineticaRT.ChannelCollection) As Boolean

            CreateChannelsDefaultBoolean = False

            Dim i As Integer
            Dim nbrTable As Integer
            Dim opcBooleanChannel As KineticaRT.Servers.OpcClient.BooleanItem

            Try

                nbrTable = CELProject.CSV_BooleanTables.GetUpperBound(0)

                For i = 0 To nbrTable

                    'Query Database Bool
                    Dim dacnBoolChannelCollection As DacnTemplateBoolCollection = New DacnTemplateBoolCollection()
                    dacnBoolChannelCollection.Query.es.QuerySource = CELProject.CSV_BooleanTables(i) 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
                    dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.ExportToCsv = True)
                    dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.ProjectID = CELProject.Project.Number)
                    '**************************************************************************************
                    '**************************************************************************************
                    '**************************************************************************************
                    '**************************************************************************************
                    '**************************************************************************************
                    'TODO fix temporaire pour eviter des prob de comm entre un slave et un master
                    'Pour etre certain a 100% que les DOUTS ne puissent etre percu par le Performance
                    '**************************************************************************************
                    '**************************************************************************************
                    '**************************************************************************************
                    '**************************************************************************************
                    '**************************************************************************************
                    If CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then
                        dacnBoolChannelCollection.Query.Where(dacnBoolChannelCollection.Query.Name.Like("DIN%"))
                    End If
                    '**************************************************************************************
                    '**************************************************************************************
                    '**************************************************************************************
                    '**************************************************************************************
                    '**************************************************************************************

                    dacnBoolChannelCollection.Query.Load()

                    For Each channel As DacnTemplateBool In dacnBoolChannelCollection
                        'Create Boolean Channels
                        opcBooleanChannel = HMIChannelCreation.CreateChannelBooleanOpcItem(channel)

                        channelCollection.Add(opcBooleanChannel)

                    Next

                Next

                CreateChannelsDefaultBoolean = True

            Catch ex As Exception
                Dim msg As String = "OpenTest_Sequence: CreateChannels: " & ex.ToString()
                If CELProject.NoPopupError Then
                    Console.WriteLine(msg)
                Else
                    clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try

        End Function

        Private Function RemoveCollection(ByVal collectionName As String) As Boolean

            RemoveCollection = False

            Try

                Dim channelsInfo As KineticaRT.Studio.Application.Project.ChannelsInfo
                channelsInfo = CELProject.HMIProject.GetChannelsInfo(collectionName)
                If Not (channelsInfo Is Nothing) Then
                    CELProject.HMIProject.RemoveChannels(channelsInfo)
                    RemoveCollection = True
                End If

            Catch ex As Exception

            End Try

        End Function

        Private Function ConnectCollection(ByVal collectionName As String) As Boolean

            ConnectCollection = False

            Try

                Dim channelCollection As KineticaRT.ChannelCollection

                channelCollection = GetObject(collectionName)
                If Not IsNothing(channelCollection) Then

                    channelCollection.Connect()

                    ConnectCollection = True

                End If

            Catch ex As Exception

            End Try

        End Function

        Private Sub UpdateHMIUserControls()

            Try

                'Query Database
                Dim dynamicChannelCollection As UUTDynamicChannelCollectionCollection = New UUTDynamicChannelCollectionCollection()
                dynamicChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.ProjectID = CELProject.Project.Number)
                dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.EngineModel = CELProject.EngineModel)
                dynamicChannelCollection.Query.OrderBy(dynamicChannelCollection.Query.LoadOrder.Ascending)
                dynamicChannelCollection.Query.Load()

                'Load All possible mapping for different collection name in a SPECIFIC ENGINE MODEL
                For Each record As UUTDynamicChannelCollection In dynamicChannelCollection

                    Dim pageMappingCollection As HMIPageMappingCollection = New HMIPageMappingCollection()
                    pageMappingCollection.es.Connection.Name = CELProject.DefaultConnectionName
                    pageMappingCollection.Query.Where(pageMappingCollection.Query.ProjectID = CELProject.Project.Number)
                    pageMappingCollection.Query.Where(pageMappingCollection.Query.CollectionName = record.CollectionName)
                    pageMappingCollection.Query.Load()

                    For Each controlMapping As HMIPageMapping In pageMappingCollection

                        Dim userControl As Object = Nothing

                        MyBase.InitializeObject(userControl, controlMapping.ControlName)

                        If Not (userControl Is Nothing) Then
                            Select Case userControl.GetType().ToString()
                                Case "KineticaRT.Studio.Controls.Dial"
                                    userControl.Source = controlMapping.Source
                                    userControl.Enabled = controlMapping.Enable
                                    userControl.Visible = controlMapping.Visible
                                Case "KineticaRT.Controls.Button"
                                    userControl.Source = controlMapping.Source
                                    userControl.Destination = controlMapping.Source
                                    userControl.InputEnabled = controlMapping.Enable
                                    userControl.Visible = controlMapping.Visible

                                Case "CEL.KineticaRT.Controls.Button",
                                    "KineticaRT.Controls.Display",
                                    "KineticaRT.Controls.PictureButton",
                                    "KineticaRT.Studio.Controls.Button",
                                    "KineticaRT.Studio.Controls.Display",
                                    "KineticaRT.Studio.Controls.Linear"
                                    userControl.Source = controlMapping.Source
                                    userControl.InputEnabled = controlMapping.Enable
                                    userControl.Visible = controlMapping.Visible

                                Case "KineticaRT.Studio.Controls.LampIndicator",
                                     "KineticaRT.Controls.Indicator"
                                    userControl.Source = controlMapping.Source
                                    userControl.Visible = controlMapping.Visible

                                    If controlMapping.OffColor <> Nothing Then
                                        userControl.OffColor = System.Drawing.ColorTranslator.FromHtml(controlMapping.OffColor)
                                    End If

                                    If controlMapping.OnColor <> Nothing Then
                                        userControl.OnColor = System.Drawing.ColorTranslator.FromHtml(controlMapping.OnColor)
                                    End If

                                    If controlMapping.SizeX IsNot Nothing Then
                                        userControl.Width = controlMapping.SizeX
                                    End If

                                    If controlMapping.SizeY IsNot Nothing Then
                                        userControl.Height = controlMapping.SizeY
                                    End If

                                Case "KineticaRT.Controls.Frame"
                                    userControl.TitleText = controlMapping.TitleText
                                    userControl.Enabled = controlMapping.Enable
                                    userControl.Visible = controlMapping.Visible

                                    If controlMapping.SizeX IsNot Nothing Then
                                        userControl.Width = controlMapping.SizeX
                                    End If

                                    If controlMapping.SizeY IsNot Nothing Then
                                        userControl.Height = controlMapping.SizeY
                                    End If

                                    If controlMapping.LocationX IsNot Nothing Then
                                        userControl.Left = controlMapping.LocationX
                                    End If

                                    If controlMapping.LocationY IsNot Nothing Then
                                        userControl.Top = controlMapping.LocationY
                                    End If

                                Case "System.Windows.Forms.Label"
                                    userControl.Text = controlMapping.TitleText
                                    userControl.Enabled = controlMapping.Enable
                                    userControl.Visible = controlMapping.Visible

                                Case "KineticaRT.Controls.Picture"
                                    userControl.FileName = controlMapping.FileName
                                    userControl.Enabled = controlMapping.Enable
                                    userControl.Visible = controlMapping.Visible

                                Case "CEL.Controls.MultiChannelDisplay"

                                    If controlMapping.SizeX IsNot Nothing Then
                                        userControl.Width = controlMapping.SizeX
                                    End If

                                    If controlMapping.SizeY IsNot Nothing Then
                                        userControl.Height = controlMapping.SizeY
                                    End If

                                    If controlMapping.LocationX IsNot Nothing Then
                                        userControl.Left = controlMapping.LocationX
                                    End If

                                    If controlMapping.LocationY IsNot Nothing Then
                                        userControl.Top = controlMapping.LocationY
                                    End If

                                Case Else
                                    Debug.Assert(False, "OpenTest_Sequece.UpdateHMIUserControls:  Object type not supported:" + userControl.GetType().ToString())
                            End Select

                            If Not IsNothing(controlMapping.LocationX) AndAlso Not IsNothing(controlMapping.LocationY) Then
                                userControl.location = New System.Drawing.Point(controlMapping.LocationX, controlMapping.LocationY)
                            End If

                        Else
                            Dim msg As String = "OpenTest_Sequence.UpdateHMIUserControls: Object not found in HMI page " & controlMapping.ControlName
                            If CELProject.NoPopupError Then
                                Console.WriteLine(msg)
                            Else
                                clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                        End If

                    Next

                Next

            Catch ex As Exception
                Dim msg As String = "OpenTest_Sequence.UpdateHMIUserControls: " & ex.ToString()
                If CELProject.NoPopupError Then
                    Console.WriteLine(msg)
                Else
                    clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            End Try
        End Sub

        Private Function Create_Engine_Channels(ByVal collectionName As String, ByRef engineChannelCollection As KineticaRT.ChannelCollection, ByRef numericChannelCollection As KineticaRT.ChannelCollection)

            Create_Engine_Channels = False

            Try

                If Not IsNothing(engineChannelCollection) Then


                    HMIChannelCreation.CreateChannelsEngineBoolean(collectionName, engineChannelCollection, EngineMappingList, BoolInitChannel, BoolInitChannelValue, BoolInitIndex)
                    HMIChannelCreation.CreateChannelsEngineNumeric(collectionName, engineChannelCollection, numericChannelCollection, numericCollectionName, EngineMappingList, InputOutputRegEx, Me.GetRegexOptions())
                    HMIChannelCreation.CreateChannelsEngineText(collectionName, engineChannelCollection)

                    Create_Engine_Channels = True

                Else
                    Debug.Assert(False)
                End If
            Catch ex As Exception
                Dim msg As String = "OpenTest_Sequence.Create_Engine_Channels: " & "collection name: " & collectionName & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString()
                If CELProject.NoPopupError Then
                    Console.WriteLine(msg)
                Else
                    clsTopMostMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Try
        End Function

        Private Sub LoadEngineArchiveLayout(ByVal engineModel As String)

            CELProject.DicArchiveChannelList.Clear()

            Dim archiveReportDetailCollection As New ArchiveReportDetailCollection()
            archiveReportDetailCollection.es.Connection.Name = CELProject.DefaultConnectionName
            archiveReportDetailCollection.Query.Where(archiveReportDetailCollection.Query.ProjectID = CELProject.Project.Number)
            archiveReportDetailCollection.Query.Where(archiveReportDetailCollection.Query.EngineModel = engineModel)
            archiveReportDetailCollection.Query.OrderBy(archiveReportDetailCollection.Query.ViewOrder.Ascending)
            archiveReportDetailCollection.Query.Where(archiveReportDetailCollection.Query.PwcPn = CELProject.PWCEECPartNumber)
            archiveReportDetailCollection.Query.Load()

            For Each record As ArchiveReportDetail In archiveReportDetailCollection

                CELProject.DicArchiveChannelList.Add(record.ChannelName, record.ViewOrder)

            Next
        End Sub

        Private Sub LoadArincChannelsForChannelList(ByVal engineModel As String)

            esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
            Dim ArincLabelCollection As ARINC429LabelTemplateCollection = New ARINC429LabelTemplateCollection()
            ArincLabelCollection.es.Connection.Name = CELProject.DefaultConnectionName
            ArincLabelCollection.Query.es.QuerySource = "DACN1_ARINC429_LABEL"
            ArincLabelCollection.Query.Where(ArincLabelCollection.Query.EngineModel = engineModel)
            ArincLabelCollection.Query.OrderBy(ArincLabelCollection.Query.EngineModel.Ascending)
            ArincLabelCollection.Query.Load()

            For Each labelInfo As ARINC429LabelTemplate In ArincLabelCollection

                CELProject.ChannelList.Add(labelInfo.Name, CELProject.GetUnitNameByDisplayName(labelInfo.Units))

            Next
        End Sub

        Private Sub LoadUartChannelsForChannelList(ByVal engineModel As String)

            esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
            Dim UartCollection As OmniBusBoxUartTemplateCollection = New OmniBusBoxUartTemplateCollection()
            UartCollection.es.Connection.Name = CELProject.DefaultConnectionName
            UartCollection.Query.es.QuerySource = "DACN1_OmniBusBox_IOMGR_Uart"
            UartCollection.Query.Where(UartCollection.Query.EngineModel = engineModel)
            UartCollection.Query.Where(UartCollection.Query.PwcPn = CELProject.PWCEECPartNumber)
            UartCollection.Query.OrderBy(UartCollection.Query.EngineModel.Ascending)
            UartCollection.Query.Load()

            For Each labelInfo As OmniBusBoxUartTemplate In UartCollection

                CELProject.ChannelList.Add(labelInfo.Name, CELProject.GetUnitNameByDisplayName(labelInfo.Units))

            Next
        End Sub

        Private Function LoadUnloadStaticCollections(ByVal collectionName As String, Optional ByVal Unload As Boolean = False) As Boolean

            LoadUnloadStaticCollections = False

            Dim ChannelCollection As KineticaRT.ChannelCollection
            Dim Return_Value As New ArrayList()
            Dim ChannelsInfo As KineticaRT.Studio.Application.Project.ChannelsInfo

            Try

                'Query Database
                'esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                Dim uutSaticChannelCollection As UUTStaticChannelCollectionCollection = New UUTStaticChannelCollectionCollection()
                uutSaticChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                uutSaticChannelCollection.Query.Where(uutSaticChannelCollection.Query.CollectionName = collectionName)
                uutSaticChannelCollection.Query.Load()

                For Each record As UUTStaticChannelCollection In uutSaticChannelCollection

                    If Unload Then

                        ChannelsInfo = CELProject.HMIProject.GetChannelsInfo(record.Description)
                        If Not (ChannelsInfo Is Nothing) Then
                            CELProject.HMIProject.RemoveChannels(ChannelsInfo)
                        End If

                    Else
                        CELProject.HMIProject.AddChannelCollection(record.FileName, record.Description, True) 'TEST
                        ChannelCollection = GetObject(record.Description)

                        ChannelCollection.Connect()

                        Return_Value.Add(Not ChannelCollection Is Nothing)
                    End If
                Next

                LoadUnloadStaticCollections = Not Return_Value.Contains(False)

            Catch ex As Exception
                clsTopMostMessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function

        Private Function Create_Engine_Folder(ByVal engine_model As String, ByVal SerialNumber As String, ByVal WO As String) As Boolean

            Create_Engine_Folder = False

            Try

                If engine_model <> "DAS_CALIBRATION" And engine_model <> "SYSTEM_CHECK" Then
                    'TODO: Il faudrait faire la config de ce répertoire à l'externe du code de facon dynamique
                    Dim folderName As String = "C:\CELDAS DATA\" & engine_model & "\" & SerialNumber & "\" & WO & "\Log"

                    If Directory.Exists(folderName) = False Then
                        Directory.CreateDirectory(folderName)
                    End If

                    folderName = "C:\CELDAS DATA\" & engine_model & "\" & SerialNumber & "\" & WO & "\Archive"

                    If Directory.Exists(folderName) = False Then
                        Directory.CreateDirectory(folderName)
                    End If

                    folderName = "C:\CELDAS DATA\" & engine_model & "\" & SerialNumber & "\" & WO & "\Perf"

                    If Directory.Exists(folderName) = False Then
                        Directory.CreateDirectory(folderName)

                    End If
                End If
                Create_Engine_Folder = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Create_Engine_Folder: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function



        Private Function Load_User_Inputs(ByRef engine As Uut, ByVal restoringSession As Boolean) As Boolean

            Load_User_Inputs = False

            Try

                CELProject.EngineFamily = engine.EngineFamily & ""

                If Not UserInputInfo Is Nothing Then
                    UserInputInfo.Dispose()
                End If
                UserInputInfo = New UserInputInformation(engine, restoringSession)
                If UserInputInfo.Initialize() Then

                    'Constants
                    If (IsNothing(engine.CalibrationConstant1)) Then
                        CELProject.Test_Mgr_UUT_Calib_Constant(0).Value = Values.Zero
                        'Test_Mgr_UUT_Calib_Constant(0).Value =Values.Notconnected
                    Else
                        CELProject.Test_Mgr_UUT_Calib_Constant(0).Value = New KineticaRT.Values.Double(engine.CalibrationConstant1)
                        'Test_Mgr_UUT_Calib_Constant(0).Value =Values.Notconnected
                    End If

                    If (IsNothing(engine.CalibrationConstant2)) Then
                        CELProject.Test_Mgr_UUT_Calib_Constant(1).Value = Values.Zero
                    Else
                        CELProject.Test_Mgr_UUT_Calib_Constant(1).Value = New KineticaRT.Values.Double(engine.CalibrationConstant2)
                    End If

#If 0 Then
        'TODO: Ici c'est spécifique a un moteur et devrait etre deplace dans la sequence du moteur
        'Minimun slope for rigging
        MinSlopeRig = engine.MinSlopeRig").ToString()
        Galil_MinSlope.Value = New KineticaRT.Values.Double(CDbl(MinSlopeRig))
#End If

                Else
                    clsTopMostMessageBox.Show("OpenTest_Sequence.Load_User_Inputs: Cannot Initialize UserInputInfo", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                Load_User_Inputs = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Get_User_Inputs: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End Function

        Private Function Init_Channels() As Boolean
            Dim Return_Value As New ArrayList()

            Try
                Return_Value.Clear()

                'Initialise Channels			

                'TODO: ces channels ne sont pas necessaire pour le slave ?
                Return_Value.Add(MyBase.InitializeChannel(Cancel_MeasurementUnit, "Cancel_MeasurementUnit"))
                Return_Value.Add(MyBase.InitializeChannel(Cancel_Password, "Cancel_Password"))
                Return_Value.Add(MyBase.InitializeChannel(Cancel_UserInputs, "Cancel_UserInputs"))
                Return_Value.Add(MyBase.InitializeChannel(CLOSE_TEST_BTN_OPC, "CLOSE_TEST_BTN_OPC"))
                Return_Value.Add(MyBase.InitializeChannel(DEBUG_MODE, "DEBUG_MODE"))
                Return_Value.Add(MyBase.InitializeChannel(OK_MeasurementUnit, "OK_MeasurementUnit"))
                Return_Value.Add(MyBase.InitializeChannel(OK_Password, "OK_Password"))
                Return_Value.Add(MyBase.InitializeChannel(OK_UserInputs, "OK_UserInputs"))
                Return_Value.Add(MyBase.InitializeChannel(Open_Test, "Open_Test"))
                Return_Value.Add(MyBase.InitializeChannel(DAS_CALIBRATION_BTN, "DAS_CALIBRATION_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(SYSTEM_CHECK_BTN, "SYSTEM_CHECK_BTN"))

                Return_Value.Add(MyBase.InitializeChannel(ENGINE_MODEL, "ENGINE_MODEL"))
                Return_Value.Add(MyBase.InitializeChannel(ENGINE_FAMILY, "ENGINE_FAMILY"))

                Return_Value.Add(MyBase.InitializeObject(_cboEngineModel, "Test_Password.EngineModel_List"))
                Return_Value.Add(MyBase.InitializeObject(_cboMeasurementUnit, "MeasurementUnitSelector.MeasurementUnit_List"))
                Return_Value.Add(MyBase.InitializeObject(_txtPassword, "Test_Password.Password_TextBox"))
                Return_Value.Add(MyBase.InitializeObject(_frmWelcomeScreen, "Welcome_Screen"))

                Return_Value.Add(MyBase.InitializeObject(_btnOpenTest, "Welcome_Screen.btnOpenTest"))
                Return_Value.Add(MyBase.InitializeObject(_btnDasCalibration, "Welcome_Screen.btnDasCalibration"))
                Return_Value.Add(MyBase.InitializeObject(_btnSystemCheck, "Welcome_Screen.btnSystemCheck"))


                CELProject.InitializeAndConnectDefaultEngineWindows()

                For Each window As CELDAS.sEngineWindowInfo In CELProject.EngineWindowCollection
                    If window.WindowName = "Welcome_Screen" Then
                        window.EngineWindow.Show()
                    Else
                        window.EngineWindow.Hide()
                    End If
                Next

                _btnOpenTest.Visible = False
                _btnDasCalibration.Visible = False
                _btnSystemCheck.Visible = False

                If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    _btnOpenTest.Visible = True
                    _btnDasCalibration.Visible = True
                    _btnSystemCheck.Visible = True
                End If

                If Return_Value.Contains(False) Then
                    Return False
                Else
                    Return True
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence: Init_Channels: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Function

        Private Function Initialize_PasswordWindow() As Boolean

            Initialize_PasswordWindow = False

            Try
                _txtPassword.Text = ""

                'Query Database
                Dim engineCollection As UutCollection = New UutCollection()
                engineCollection.es.Connection.Name = CELProject.DefaultConnectionName
                engineCollection.Query.Where(engineCollection.Query.ProjectID = CELProject.Project.Number)
                engineCollection.Query.Where(engineCollection.Query.Available = True)
                engineCollection.Query.OrderBy(engineCollection.Query.Id.Ascending)
                engineCollection.Query.Load()

                _cboEngineModel.DisplayMember = "EngineModel"
                _cboEngineModel.ValueMember = "Id"
                _cboEngineModel.DataSource = engineCollection

                'cboEngineModel.Items.Clear()
                'For Each engine As Uut In engineCollection

                '    If (engine.EngineModel <> "NO MODEL" And engine.Available) Then
                '        'Populate the EngineModel List
                '        cboEngineModel.Items.Add(engine.EngineModel)
                '    End If
                'Next

                Initialize_PasswordWindow = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Initialize_PasswordWindow: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function

        Private Function Open_HMI(ByVal engineModel As String) As Boolean

            Open_HMI = False

            Try
                CELProject.InitializeAndConnectEngineWindows() 'ft Cette ligne est necessaire pcq sinon la fenetre SystemCheck ne marche pas, voir onglet Nidaq et autres


                'Query Database
                Dim windowCollection As UUTHMIWindowCollection = New UUTHMIWindowCollection()
                windowCollection.es.Connection.Name = CELProject.DefaultConnectionName
                windowCollection.Query.Where(windowCollection.Query.ProjectID = CELProject.Project.Number)
                windowCollection.Query.Where(windowCollection.Query.EngineModel = engineModel)
                windowCollection.Query.Where(windowCollection.Query.MeasurementUnitId = CELProject.MeasurementUnitInUse)
                windowCollection.Query.OrderBy(windowCollection.Query.WindowID.Ascending)
                windowCollection.Query.Load()

                For Each hmiWindow As UUTHMIWindow In windowCollection

                    For Each window As CELDAS.sEngineWindowInfo In CELProject.EngineWindowCollection
                        CELProject.SkinControls(window.EngineWindow)
                        If hmiWindow.HMIWindowName = window.WindowName And hmiWindow.MainScreen = True AndAlso ((CELProject.HierarchyType = CELDAS.eHierarchyType.Master And window.Master) Or (CELProject.HierarchyType = CELDAS.eHierarchyType.Slave And window.Slave)) Then

                            window.EngineWindow.WindowState = FormWindowState.Normal
                            window.EngineWindow.Visible = True
                            window.EngineWindow.Show()

                            If Not IsNothing(hmiWindow.WindowHeight) AndAlso hmiWindow.WindowHeight <> 0 Then
                                window.EngineWindow.Height = hmiWindow.WindowHeight
                            End If
                            If Not IsNothing(hmiWindow.WindowWidth) AndAlso hmiWindow.WindowWidth <> 0 Then
                                window.EngineWindow.Width = hmiWindow.WindowWidth
                            End If

                            Exit For

                        ElseIf hmiWindow.HMIWindowName = window.WindowName AndAlso hmiWindow.Slave = False AndAlso CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then
                            window.EngineWindow.Disconnect()
                            Exit For
                        ElseIf hmiWindow.HMIWindowName = window.WindowName AndAlso hmiWindow.Master = False AndAlso CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            window.EngineWindow.Disconnect()
                            Exit For
                        End If
                    Next
                Next

                Open_HMI = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Open_HMI: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function

        Function Read_Limits() As Boolean

            Dim i As Integer
            Read_Limits = False

            Try

                'Query Database
                Dim tripCollection As UUTTripCollection = New UUTTripCollection()
                tripCollection.es.Connection.Name = CELProject.DefaultConnectionName
                tripCollection.Query.Where(tripCollection.Query.ProjectID = CELProject.Project.Number)
                tripCollection.Query.Where(tripCollection.Query.EngineModel = CELProject.EngineModelFormatted)
                tripCollection.Query.OrderBy(tripCollection.Query.TripID.Ascending)
                tripCollection.Query.Load()

                'Initialization
                For i = 0 To CELProject.Test_Mgr_UUT_TripPoints_F.GetUpperBound(0)
                    CELProject.Test_Mgr_UUT_TripPoints_F(i).Value = New KineticaRT.Values.Double(0.0)
                Next
                For i = 0 To CELProject.Test_Mgr_UUT_TripPoints_I.GetUpperBound(0)
                    CELProject.Test_Mgr_UUT_TripPoints_I(i).Value = New KineticaRT.Values.Double(0.0)
                Next
                For i = 0 To CELProject.Test_Mgr_UUT_TripPoints_N.GetUpperBound(0)
                    CELProject.Test_Mgr_UUT_TripPoints_N(i).Value = New KineticaRT.Values.Double(0.0)
                Next
                For i = 0 To CELProject.Test_Mgr_UUT_TripPoints_Q.GetUpperBound(0)
                    CELProject.Test_Mgr_UUT_TripPoints_Q(i).Value = New KineticaRT.Values.Double(0.0)
                Next
                For i = 0 To CELProject.Test_Mgr_UUT_TripPoints_T.GetUpperBound(0)
                    CELProject.Test_Mgr_UUT_TripPoints_T(i).Value = New KineticaRT.Values.Double(0.0)
                Next
                For i = 0 To CELProject.Test_Mgr_UUT_TripPoints_V.GetUpperBound(0)
                    CELProject.Test_Mgr_UUT_TripPoints_V(i).Value = New KineticaRT.Values.Double(0.0)
                Next
                For i = 0 To CELProject.Test_Mgr_UUT_TripPoints_Vib.GetUpperBound(0)
                    CELProject.Test_Mgr_UUT_TripPoints_Vib(i).Value = New KineticaRT.Values.Double(0.0)
                Next

                Dim engineChannelCollection As KineticaRT.ChannelCollection = GetObject(engineCollectionName)

                For Each trip As UUTTrip In tripCollection

                    'Dim tripType As UUTTrip.eTripType = trip.TypeID

                    'TODO: mettre autant de trip que nécessaire génériquement
                    Debug.Assert(trip.TripID < 8 And trip.TripID > 0) 'Actuellement harcodé a 7 max de 1 a 7

                    'Select Case tripType
                    Select Case trip.Type
                        Case UUTTrip.eTripType.Frequency

                            'CELProject.Test_Mgr_UUT_TripPoints_F(trip.TripID - 1).Value = New KineticaRT.Values.Text(trip.Value)
                            CELProject.Test_Mgr_UUT_TripPoints_F(trip.TripID - 1).Description = trip.Name

                            Dim channel As New HMINumericChannels
                            channel.ChannelName = "TRIP_F" + trip.TripID.ToString + "_EXPRESSION"
                            channel.Expression = trip.Expression
                            channel.DecimalDigits = 2
                            channel.Format = "F2"
                            channel.EngUnitID = 0
                            channel.Minimum = 0
                            channel.Maximum = 100

                            Dim newChannel = HMIChannelCreation.CreateChannelNumericThresholdExpression(channel)
                            engineChannelCollection.Add(newChannel)

                            Dim channel2 As New HMINumericChannels
                            channel2.ChannelName = "TRIP_F" + trip.TripID.ToString + "_LINK"
                            channel2.Source = channel.ChannelName
                            channel2.Destination = CELProject.Test_Mgr_UUT_TripPoints_F(trip.TripID - 1).Name
                            channel2.DecimalDigits = 2
                            channel2.Format = "F2"
                            channel2.EngUnitID = 0
                            channel2.Minimum = 0
                            channel2.Maximum = 100

                            Dim newChannel2 = HMIChannelCreation.CreateChannelNumericThresholdLink(channel2)
                            engineChannelCollection.Add(newChannel2)


                        Case UUTTrip.eTripType.Speed
                            'CELProject.Test_Mgr_UUT_TripPoints_N(trip.TripID - 1).Value = New KineticaRT.Values.Text(trip.Value)
                            CELProject.Test_Mgr_UUT_TripPoints_N(trip.TripID - 1).Description = trip.Name

                            Dim channel As New HMINumericChannels
                            channel.ChannelName = "TRIP_N" + trip.TripID.ToString + "_EXPRESSION"
                            channel.Expression = trip.Expression
                            channel.DecimalDigits = 2
                            channel.Format = "F2"
                            channel.EngUnitID = 0
                            channel.Minimum = 0
                            channel.Maximum = 100

                            Dim newChannel = HMIChannelCreation.CreateChannelNumericThresholdExpression(channel)
                            engineChannelCollection.Add(newChannel)

                            Dim channel2 As New HMINumericChannels
                            channel2.ChannelName = "TRIP_N" + trip.TripID.ToString + "_LINK"
                            channel2.Source = channel.ChannelName
                            channel2.Destination = CELProject.Test_Mgr_UUT_TripPoints_N(trip.TripID - 1).Name
                            channel2.DecimalDigits = 2
                            channel2.Format = "F2"
                            channel2.EngUnitID = 0
                            channel2.Minimum = 0
                            channel2.Maximum = 100

                            Dim newChannel2 = HMIChannelCreation.CreateChannelNumericThresholdLink(channel2)
                            engineChannelCollection.Add(newChannel2)

                        Case UUTTrip.eTripType.Current

                            'CELProject.Test_Mgr_UUT_TripPoints_I(trip.TripID - 1).Value = New KineticaRT.Values.Text(trip.Value)
                            CELProject.Test_Mgr_UUT_TripPoints_I(trip.TripID - 1).Description = trip.Name

                            Dim channel As New HMINumericChannels
                            channel.ChannelName = "TRIP_I" + trip.TripID.ToString + "_EXPRESSION"
                            channel.Expression = trip.Expression
                            channel.DecimalDigits = 2
                            channel.Format = "F2"
                            channel.EngUnitID = 0
                            channel.Minimum = 0
                            channel.Maximum = 100

                            Dim newChannel = HMIChannelCreation.CreateChannelNumericThresholdExpression(channel)
                            engineChannelCollection.Add(newChannel)

                            Dim channel2 As New HMINumericChannels
                            channel2.ChannelName = "TRIP_I" + trip.TripID.ToString + "_LINK"
                            channel2.Source = channel.ChannelName
                            channel2.Destination = CELProject.Test_Mgr_UUT_TripPoints_I(trip.TripID - 1).Name
                            channel2.DecimalDigits = 2
                            channel2.Format = "F2"
                            channel2.EngUnitID = 0
                            channel2.Minimum = 0
                            channel2.Maximum = 100

                            Dim newChannel2 = HMIChannelCreation.CreateChannelNumericThresholdLink(channel2)
                            engineChannelCollection.Add(newChannel2)

                        Case UUTTrip.eTripType.Temperature
                            'CELProject.Test_Mgr_UUT_TripPoints_T(trip.TripID - 1).Value = New KineticaRT.Values.Text(trip.Value)
                            CELProject.Test_Mgr_UUT_TripPoints_T(trip.TripID - 1).Description = trip.Name

                            Dim channel As New HMINumericChannels
                            channel.ChannelName = "TRIP_T" + trip.TripID.ToString + "_EXPRESSION"
                            channel.Expression = trip.Expression
                            channel.DecimalDigits = 2
                            channel.Format = "F2"
                            channel.EngUnitID = 0
                            channel.Minimum = 0
                            channel.Maximum = 100

                            Dim newChannel = HMIChannelCreation.CreateChannelNumericThresholdExpression(channel)
                            engineChannelCollection.Add(newChannel)

                            Dim channel2 As New HMINumericChannels
                            channel2.ChannelName = "TRIP_T" + trip.TripID.ToString + "_LINK"
                            channel2.Source = channel.ChannelName
                            channel2.Destination = CELProject.Test_Mgr_UUT_TripPoints_T(trip.TripID - 1).Name
                            channel2.DecimalDigits = 2
                            channel2.Format = "F2"
                            channel2.EngUnitID = 0
                            channel2.Minimum = 0
                            channel2.Maximum = 100

                            Dim newChannel2 = HMIChannelCreation.CreateChannelNumericThresholdLink(channel2)
                            engineChannelCollection.Add(newChannel2)

                        Case UUTTrip.eTripType.Torque

                            'CELProject.Test_Mgr_UUT_TripPoints_Q(trip.TripID - 1).Value = New KineticaRT.Values.Text(trip.Value)
                            CELProject.Test_Mgr_UUT_TripPoints_Q(trip.TripID - 1).Description = trip.Name

                            Dim channel As New HMINumericChannels
                            channel.ChannelName = "TRIP_Q" + trip.TripID.ToString + "_EXPRESSION"
                            channel.Expression = trip.Expression
                            channel.DecimalDigits = 2
                            channel.Format = "F2"
                            channel.EngUnitID = 0
                            channel.Minimum = 0
                            channel.Maximum = 100

                            Dim newChannel = HMIChannelCreation.CreateChannelNumericThresholdExpression(channel)
                            engineChannelCollection.Add(newChannel)

                            Dim channel2 As New HMINumericChannels
                            channel2.ChannelName = "TRIP_Q" + trip.TripID.ToString + "_LINK"
                            channel2.Source = channel.ChannelName
                            channel2.Destination = CELProject.Test_Mgr_UUT_TripPoints_Q(trip.TripID - 1).Name
                            channel2.DecimalDigits = 2
                            channel2.Format = "F2"
                            channel2.EngUnitID = 0
                            channel2.Minimum = 0
                            channel2.Maximum = 100

                            Dim newChannel2 = HMIChannelCreation.CreateChannelNumericThresholdLink(channel2)
                            engineChannelCollection.Add(newChannel2)

                        Case UUTTrip.eTripType.Vibration
                            'CELProject.Test_Mgr_UUT_TripPoints_Vib(trip.TripID - 1).Value = New KineticaRT.Values.Text(trip.Value)
                            CELProject.Test_Mgr_UUT_TripPoints_Vib(trip.TripID - 1).Description = trip.Name

                            Dim channel As New HMINumericChannels
                            channel.ChannelName = "TRIP_VIB" + trip.TripID.ToString + "_EXPRESSION"
                            channel.Expression = trip.Expression
                            channel.DecimalDigits = 2
                            channel.Format = "F2"
                            channel.EngUnitID = 0
                            channel.Minimum = 0
                            channel.Maximum = 100

                            Dim newChannel = HMIChannelCreation.CreateChannelNumericThresholdExpression(channel)
                            engineChannelCollection.Add(newChannel)

                            Dim channel2 As New HMINumericChannels
                            channel2.ChannelName = "TRIP_VIB" + trip.TripID.ToString + "_LINK"
                            channel2.Source = channel.ChannelName
                            channel2.Destination = CELProject.Test_Mgr_UUT_TripPoints_Vib(trip.TripID - 1).Name
                            channel2.DecimalDigits = 2
                            channel2.Format = "F2"
                            channel2.EngUnitID = 0
                            channel2.Minimum = 0
                            channel2.Maximum = 100

                            Dim newChannel2 = HMIChannelCreation.CreateChannelNumericThresholdLink(channel2)
                            engineChannelCollection.Add(newChannel2)

                        Case UUTTrip.eTripType.Pressure

                            'CELProject.Test_Mgr_UUT_TripPoints_P(trip.TripID - 1).Value = New KineticaRT.Values.Text(trip.Value)
                            CELProject.Test_Mgr_UUT_TripPoints_P(trip.TripID - 1).Description = trip.Name

                            Dim channel As New HMINumericChannels
                            channel.ChannelName = "TRIP_P" + trip.TripID.ToString + "_EXPRESSION"
                            channel.Expression = trip.Expression
                            channel.DecimalDigits = 2
                            channel.Format = "F2"
                            channel.EngUnitID = 0
                            channel.Minimum = 0
                            channel.Maximum = 100

                            Dim newChannel = HMIChannelCreation.CreateChannelNumericThresholdExpression(channel)
                            engineChannelCollection.Add(newChannel)

                            Dim channel2 As New HMINumericChannels
                            channel2.ChannelName = "TRIP_P" + trip.TripID.ToString + "_LINK"
                            channel2.Source = channel.ChannelName
                            channel2.Destination = CELProject.Test_Mgr_UUT_TripPoints_P(trip.TripID - 1).Name
                            channel2.DecimalDigits = 2
                            channel2.Format = "F2"
                            channel2.EngUnitID = 0
                            channel2.Minimum = 0
                            channel2.Maximum = 100

                            Dim newChannel2 = HMIChannelCreation.CreateChannelNumericThresholdLink(channel2)
                            engineChannelCollection.Add(newChannel2)

                        Case UUTTrip.eTripType.Voltage

                            'CELProject.Test_Mgr_UUT_TripPoints_V(trip.TripID - 1).Value = New KineticaRT.Values.Text(trip.Value)
                            CELProject.Test_Mgr_UUT_TripPoints_V(trip.TripID - 1).Description = trip.Name

                            Dim channel As New HMINumericChannels
                            channel.ChannelName = "TRIP_V" + trip.TripID.ToString + "_EXPRESSION"
                            channel.Expression = trip.Expression
                            channel.DecimalDigits = 2
                            channel.Format = "F2"
                            channel.EngUnitID = 0
                            channel.Minimum = 0
                            channel.Maximum = 100

                            Dim newChannel = HMIChannelCreation.CreateChannelNumericThresholdExpression(channel)
                            engineChannelCollection.Add(newChannel)

                            Dim channel2 As New HMINumericChannels
                            channel2.ChannelName = "TRIP_V" + trip.TripID.ToString + "_LINK"
                            channel2.Source = channel.ChannelName
                            channel2.Destination = CELProject.Test_Mgr_UUT_TripPoints_V(trip.TripID - 1).Name
                            channel2.DecimalDigits = 2
                            channel2.Format = "F2"
                            channel2.EngUnitID = 0
                            channel2.Minimum = 0
                            channel2.Maximum = 100

                            Dim newChannel2 = HMIChannelCreation.CreateChannelNumericThresholdLink(channel2)
                            engineChannelCollection.Add(newChannel2)

                        Case Else
                            Debug.Assert(False)
                    End Select



                Next
                Read_Limits = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Read_Limits: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


        End Function

        Function Start_DACS() As Boolean
            Start_DACS = False
            Try
                If CELProject.Test_Mgr_Status.Value.ToString() = CELDAS.eTest_Mgr_Status.Idling Then
                    If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                        CELProject.Test_Mgr_Cmd.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Cmd.Open)
                        Start_DACS = True
                    End If
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.Start_DACS: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Private Sub OnCancel_MeasurementUnit(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If (CELProject.IsTrue(newVal)) Then
                    CELProject.ActivateForm("MeasurementUnitSelector", "Welcome_Screen", False, False)
                    Cancel_MeasurementUnit.Value = Values.False
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnCancel_MeasurementUnit: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnCancel_Password(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If (CELProject.IsTrue(newVal)) Then
                    CELProject.ActivateForm("Test_Password", "Welcome_Screen", False, False)
                    Cancel_Password.Value = Values.False
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnCancel_Password: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnCancel_UserInputs(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If (CELProject.IsTrue(newVal)) Then
                    CELProject.ActivateForm("UserInputs", "Welcome_Screen", False, False)

                    Me.UserInputInformationLoaded = False
                    Open_Test.Value = Values.False
                    Cancel_UserInputs.Value = Values.False
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnCancel_UserInputs: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnCLOSE_TEST_BTN(ByVal sender As IChannel, ByVal newVal As Value)

            If (CELProject.IsTrue(newVal)) Then

                If CELProject.EngineModel <> "DAS CALIBRATION" Then 'Le closing pour DAS CALIBRATION se fait dans sa sequence
                    If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

                        CLOSE_TEST_BTN_OPC.Value = KineticaRT.Values.Values.True
                        'Permet de remettre des channels dans un état spécifique
                        'Pour le moment on ne le fait que pour le Critical. Normalement il ne devrait pas y avoir de commande vers les DOUT dans le performance.
                        'TODO: Eventuellement on devrait pouvoir avoir des DOUT sur n'importe quel HMI.
                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            ResetOutputsOnCLose() 'Cette fonction doit être exécutée avant que le QNX soit a IDLING pour avoir le temps de processer les changement d etats
                        End If

                        'Master Sending Command to Close DAS, Slave will be calling Close_HMI in OnTestSessionClosing
                        CELProject.Test_Mgr_Cmd.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Cmd.Close)
                        sender.Value = Values.False

                    End If
                End If
            End If
        End Sub

        Private Sub OnCLOSE_TEST_BTN_OPC()
            Try
                ENGINE_FAMILY.Value = New KineticaRT.Values.Text("N/A")
                ENGINE_MODEL.Value = New KineticaRT.Values.Text("N/A")
            Catch ex As Exception
                MessageBox.Show("OnCLOSE_TEST_BTN_OPC: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnMeasurementUnit_List_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Try
                MeasurementUnitSelected = _cboMeasurementUnit.SelectedItem
            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnEngineModel_List_SelectedIndexChanged: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnEngineModel_List_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Try

                CELProject.EngineModel = _cboEngineModel.Text
                'EngineModel = cboEngineModel.Text 'TODO: Retirer la variable privee EngineModel de la sequence
                CELProject.EngineModel = _cboEngineModel.Text

                'Query Database
                Dim uutInfo As Uut = _cboEngineModel.SelectedItem

                'Dim uutInfo As Uut = New Uut()
                'uutInfo.es.Connection.Name = CELProject.DefaultConnectionName
                'uutInfo.Query.Where(uutInfo.Query.ProjectID = CELProject.Project.Number)
                'uutInfo.Query.Where(uutInfo.Query.EngineModel = CELProject.EngineModel)
                'uutInfo.Query.Load()

                If uutInfo.EngineFamily & "" <> "" Then 'Use EngineFamily architecture for the DAS
                    CELProject.EngineFamily = uutInfo.EngineFamily
                Else
                    CELProject.EngineFamily = ""
                End If


            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnEngineModel_List_SelectedIndexChanged: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub ShowUserInputs(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (CELProject.IsTrue(newVal)) Then
                    If CELProject.EngineModel <> String.Empty Then
                        CELProject.ActivateForm("MeasurementUnitSelector", "Welcome_Screen", False, False)

                        Me.UserInputInformationLoaded = False
                        If Load_User_Inputs(_cboEngineModel.SelectedItem, False) Then
                            If CELProject.EngineModel = "DAS CALIBRATION" Or CELProject.EngineModel = "SYSTEM CHECK" Then 'Choice_Title(0) = "" And QuestionTitleList(0) = "" And OptionList(0) = "" Then  'ft pourquoi cette ligne de code????
                                'En DAS CALIBRATION on a pas besoin du user input
                                OK_UserInputs.Value = Values.True
                            Else
                                CELProject.ActivateForm("UserInputs", "Welcome_Screen", True, False)
                            End If
                            Me.UserInputInformationLoaded = True
                            'FIX BUG lorsque en restore une session ex. et qu on ouvre le HMI qui est deja RUNNING, les Description des Channels ne sont pas loadé correctement
                            'Ex.  TN_UUT_Answer1.Description n'pas loade correctement pcq on a pas passe par le OK password, Alors la generation du HEADER du LOG REPORT a les bonne
                            'reponses mais pas les label descritptif correcte.

                        Else
                            clsTopMostMessageBox.Show("OpenTest_Seuqence.OnOK_Password: Load_User_Inputs error", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End If

                    OK_UserInputs.Value = Values.False
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnOK_MeasurementUnit: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub OnOK_MeasurementUnit(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                MeasurementUnitSelected = _cboMeasurementUnit.SelectedItem

                'Re-Initialize OK Measurement Unite Selector Button Value
                OK_MeasurementUnit.Value = Values.False

                'ShowUserInputs(sender, newVal)
                Try
                    If (CELProject.IsTrue(newVal)) Then
                        If (Validate_UserPasswordAndSecurityAccess()) Then
                            If CELProject.EngineModel <> String.Empty Then
                                CELProject.ActivateForm("MeasurementUnitSelector", "Welcome_Screen", False, False)

                                Me.UserInputInformationLoaded = False
                                If Load_User_Inputs(_cboEngineModel.SelectedItem, False) Then
                                    If CELProject.EngineModel = "DAS CALIBRATION" Or CELProject.EngineModel = "SYSTEM CHECK" Then 'Choice_Title(0) = "" And QuestionTitleList(0) = "" And OptionList(0) = "" Then  'ft pourquoi cette ligne de code????
                                        'En DAS CALIBRATION on a pas besoin du user input
                                        OK_UserInputs.Value = Values.True
                                    Else
                                        CELProject.ActivateForm("UserInputs", "Welcome_Screen", True, False)
                                    End If
                                    Me.UserInputInformationLoaded = True
                                    'FIX BUG lorsque en restore une session ex. et qu on ouvre le HMI qui est deja RUNNING, les Description des Channels ne sont pas loadé correctement
                                    'Ex.  TN_UUT_Answer1.Description n'pas loade correctement pcq on a pas passe par le OK password, Alors la generation du HEADER du LOG REPORT a les bonne
                                    'reponses mais pas les label descritptif correcte.

                                Else
                                    clsTopMostMessageBox.Show("OpenTest_Seuqence.OnOK_Password: Load_User_Inputs error", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            End If
                        End If

                        OK_UserInputs.Value = Values.False
                    End If

                Catch ex As Exception
                    clsTopMostMessageBox.Show("OpenTest_Sequence.OnOK_MeasurementUnit: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try


            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnOK_MeasurementUnit: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnOK_Password(ByVal sender As IChannel, ByVal newVal As Value)

            Dim measurementUnitCollection As New MeasurementUnitCollection()

            Try
                If (CELProject.IsTrue(newVal)) Then
                    If (Validate_UserPasswordAndSecurityAccess()) Then
                        If CELProject.EngineModel <> "" Then
                            CELProject.ActivateForm("Test_Password", "Welcome_Screen", False, False)

                            ' For the selected project and engine, determine what are the Measurement Units screen defined in UUT_HMI_WINDOW. This will be determined which
                            ' Measurement Unit CELDAS can propose to the user for the selected engine
                            Dim windowCollection As UUTHMIWindowCollection = New UUTHMIWindowCollection()

                            windowCollection.es.Connection.Name = CELProject.DefaultConnectionName
                            windowCollection.Query.Where(windowCollection.Query.ProjectID = CELProject.Project.Number)
                            windowCollection.Query.Where(windowCollection.Query.EngineModel = CELProject.EngineModel)
                            windowCollection.Query.Where(windowCollection.Query.Unavailable = False)
                            windowCollection.Query.Where(windowCollection.Query.MeasurementUnitId <> CELDAS.eMeasurementUnit.Imperial)
                            windowCollection.Query.Load()

                            ' Add the imperial unit in the list of possible Measurement Unit (Always there)
                            Dim measurementUnit As MeasurementUnit = New MeasurementUnit()
                            measurementUnit.es.Connection.Name = CELProject.DefaultConnectionName
                            measurementUnit.Query.Where(measurementUnit.Query.MeasurementUnitId = CELDAS.eMeasurementUnit.Imperial)
                            measurementUnit.Query.Load()

                            measurementUnitCollection.Add(measurementUnit)

                            ' Add the other measurement units
                            If windowCollection.Count <> 0 Then

                                For Each currentUutWindow As UUTHMIWindow In windowCollection
                                    Dim measurementUnitInCollection As MeasurementUnit = New MeasurementUnit()
                                    measurementUnitInCollection.es.Connection.Name = CELProject.DefaultConnectionName
                                    measurementUnitInCollection.Query.Where(measurementUnitInCollection.Query.MeasurementUnitId = currentUutWindow.MeasurementUnitId)
                                    measurementUnitInCollection.Query.Load()

                                    Dim measurementUnitFound As Boolean
                                    measurementUnitFound = False
                                    For Each currentMeasurementUnitInCollection As MeasurementUnit In measurementUnitCollection
                                        If currentMeasurementUnitInCollection.MeasurementUnitId = measurementUnitInCollection.MeasurementUnitId Then
                                            measurementUnitFound = True
                                        End If
                                    Next

                                    If Not measurementUnitFound Then
                                        measurementUnitCollection.Add(measurementUnitInCollection)
                                    End If
                                Next
                            End If

                            If measurementUnitCollection.Count > 1 Then
                                ' We need to ask the user in which measurement unit the test will operate

                                _cboMeasurementUnit.DisplayMember = "MeasurementUnitDescription"
                                _cboMeasurementUnit.ValueMember = "MeasurementUnitId"
                                _cboMeasurementUnit.DataSource = measurementUnitCollection

                                CELProject.ActivateForm("MeasurementUnitSelector", "Welcome_Screen", True, False)
                            Else
                                ' Only Imperial unit are defined so no need to ask for measurement unit to use
                                'MeasurementUnitSelected = New MeasurementUnit()
                                'MeasurementUnitSelected.es.Connection.Name = CELProject.DefaultConnectionName
                                'MeasurementUnitSelected.Query.Where(MeasurementUnitSelected.Query.MeasurementUnitId = 0)
                                'MeasurementUnitSelected.Query.Load()

                                ShowUserInputs(sender, newVal)
                            End If
                        Else
                            clsTopMostMessageBox.Show("OpenTest_Sequence.OnOK_Password: The Test_Mgr_UUT_Model is empty", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End If

                    'Re-Initialize OK Password Button Value
                    OK_Password.Value = Values.False

                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnOK_Password: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub OnOK_UserInputs(ByVal sender As IChannel, ByVal newVal As Value)

            Try

                If (CELProject.IsTrue(newVal)) Then

                    If Validate_ConfigurationFile() Then

                        If UserInputInfo.ValidateAnswer() Then

                            If UserInputInfo.ExecuteChoicesCustomUpdateQuery() Then

                                UserInputInfo.UpdateDefaultValueUpdatable()

                                CELProject.ActivateForm("UserInputs", "Welcome_Screen", False, False)

                                If MeasurementUnitSelected Is Nothing Then
                                    CELProject.MeasurementUnitInUse = CELDAS.eMeasurementUnit.Imperial
                                Else
                                    CELProject.MeasurementUnitInUse = MeasurementUnitSelected.MeasurementUnitId
                                End If


                                If CELProject.MeasurementUnitInUse <> CELDAS.eMeasurementUnit.Imperial Then
                                    CELProject.MeasurementUnitNameInUse = "_" + MeasurementUnitSelected.MeasurementUnitDescription
                                Else
                                    CELProject.MeasurementUnitNameInUse = String.Empty
                                End If

                                If CELProject.EngineModel <> "DAS CALIBRATION" And CELProject.EngineModel <> "SYSTEM CHECK" Then

                                    'Display the orifice to use to the operator
                                    Dim engineCustomOpeningMessage As String = String.Empty

                                    If Not IsNothing(CELProject.EngineConfiguration) Then
                                        engineCustomOpeningMessage = CELProject.EngineConfiguration.CustomOpeningMessage
                                    End If
                                    CELProject.DisplayOrifice(engineCustomOpeningMessage)

                                    Debug.Assert(CELProject.EngineModel <> "", "OpenTestSequence.OnOK_UserInputs.CELProject.EngineModel")
                                    Debug.Assert(CELProject.Test_Mgr_UUT_Answer(0).Value.ToString() <> "", "OpenTestSequence.OnOK_UserInputs.CELProject.Test_Mgr_UUT_Answer(0).Value.ToString")
                                    Debug.Assert(CELProject.Test_Mgr_UUT_Answer(1).Value.ToString() <> "", "OpenTestSequence.OnOK_UserInputs.CELProject.Test_Mgr_UUT_Answer(1).Value.ToString")

                                End If

                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************

                                'TODO DANS LE BUT DE TESTER LE WORKBENCH SANS LE HARDWARE ON PEUT METTRE LE MODE EN SIMULATION
                                'TOUT EN NE DÉSACTIVANT PAS LES RESSOURCES DU WORKBENCH

                                'ATTENTION ATTENTION CETTE VARIABLE DOIT À LA LIVRAISON DU SYSTÈME ÊTRE EN RUN_FOR_TRUE 


                                'CELProject.Test_Mgr_Mode.Value = New KineticaRT.Values.Double(eTest_Mgr_Mode.SIMULATION)
                                CELProject.Test_Mgr_Mode.Value = New KineticaRT.Values.Double(CELProject.DataAcquisitionMode)

                                'MessageBox.Show("Critical.OpenTest.OnOK_UserInputs: DEVELOPMENT MODE: Les ressources workbench du système sont actuellement " & vbCrLf & _
                                '    " en mode SIMULATION (0) i.e. les ressources sont a running mais n'interroge pas le hardare.  " & vbCrLf & _
                                '    "Mettre ces lignes en commentaires dans le code lorsque en SHAKEDOWN et Activer le RUN_FOR_TRUE (1)." )

                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************
                                '**************************************************************************************

                                _frmWelcomeScreen.Cursor = Cursors.WaitCursor

                                'UART CONFIGURATION IS EXPORTED TO QNX AFTER USER SELECTION
                                '
                                'For example, PW206A, PW206B, PW206C are using a UART parameter list that changes depending of services bulletins
                                'A generic list of parameters (RTD) cannot be achieved as some parameters are present in a given USI and not in ther other.
                                'This information was given by Francis Boyte from P&WC

                                'First thing is to validate if any service bulletins have been entered in the user inputs
                                'Questions/Choices are scanned for the title "PWC EEC P/N" and the user selection is passed to CELDAS instance for use in the HardwareConfiguration class

                                'For the master, the information required for the csv generation is done during the user inputs
                                'However, for the slave, this is done during the "Opened" event. We also retrieve the information is the session of the test was alted.
                                If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

                                    'PWC EEC P/N is retrieved based on the questions/choices for a specific engine  
                                    CELProject.PWCEECPartNumber = UserInputInfo.FindPWCEECPartNumber(CELProject.EngineModel)

                                    'Omnibusbox UART CSV file is generated based on UART parameter list specific to the PWC EEC P/N
                                    Dim exportSoftware As New CEL.HMI.HardwareConfiguration
                                    exportSoftware.ExportCsv("DACN1_OmniBusBox_IOMGR_Uart")

                                    exportSoftware.FtpFileUpload(CELProject.ExportPathName + "DACN1_UART_OMNIBUSBOX_CSV.csv", CELProject.QNX.UploadPath, "DACN1_UART_OMNIBUSBOX_CSV.csv", CELProject.QNX.IpAdress, CELProject.QNX.UserName, CELProject.QNX.Password)
                                End If

                                Start_DACS()
                            End If



                            CELProject.CleanUpKineticaRtFile()
                        End If

                    End If

                End If

                OK_MeasurementUnit.Value = Values.False
                OK_UserInputs.Value = Values.False
                Open_Test.Value = Values.False

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence: OnOK_UserInputs: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnDAS_CALIBRATION_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try

                If (CELProject.IsTrue(newVal)) Then

                    If (CELProject.IsTrue(newVal)) Then

                        Select Case CELProject.Test_Mgr_Status.Value.ToString()

                            Case CELDAS.eTest_Mgr_Status.Idling
                                CELProject.EngineFamily = ""
                                Dim engine As String = "DAS CALIBRATION"
                                CELProject.UserDefaultEngine = engine
                                _cboEngineModel.Text = engine
                                CELProject.EngineModel = engine
                                _txtPassword.Text = CELProject.UserDefaultPassword

                                CELProject.Test_Mgr_Mode.Value = New KineticaRT.Values.Double(CELProject.DataAcquisitionMode)

                                If CELProject.UserDefaultPassword <> "" Then
                                    If (Validate_UserPasswordAndSecurityAccess()) Then

                                        Start_DACS()
                                    Else
                                        Open_Test.Value = Values.True
                                    End If
                                Else
                                    Open_Test.Value = Values.True
                                End If
                            Case CELDAS.eTest_Mgr_Status.Running, CELDAS.eTest_Mgr_Status.Closing, CELDAS.eTest_Mgr_Status.Closed, CELDAS.eTest_Mgr_Status.Loading, CELDAS.eTest_Mgr_Status.Loaded
                                clsTopMostMessageBox.Show("Test Session is running!, Please Close Test Session prior to Open it", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                            Case Else
                                clsTopMostMessageBox.Show("CELDAS not connected or not ready.", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        End Select

                        DAS_CALIBRATION_BTN.Value = Values.False
                    End If

                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub OnSYSTEM_CHECK_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try

                If (CELProject.IsTrue(newVal)) Then

                    Select Case CELProject.Test_Mgr_Status.Value.ToString()

                        Case CELDAS.eTest_Mgr_Status.Idling
                            CELProject.EngineFamily = ""
                            Dim engine As String = "SYSTEM CHECK"
                            CELProject.UserDefaultEngine = engine
                            _cboEngineModel.Text = engine
                            CELProject.EngineModel = engine
                            _txtPassword.Text = CELProject.UserDefaultPassword
                            CELProject.Test_Mgr_Mode.Value = New KineticaRT.Values.Double(CELProject.DataAcquisitionMode)

                            If CELProject.UserDefaultPassword <> "" Then
                                If (Validate_UserPasswordAndSecurityAccess()) Then
                                    Thread.Sleep(2000)
                                    Start_DACS()
                                Else
                                    Open_Test.Value = Values.True
                                End If
                            Else
                                Open_Test.Value = Values.True
                            End If

                        Case CELDAS.eTest_Mgr_Status.Running, CELDAS.eTest_Mgr_Status.Closing, CELDAS.eTest_Mgr_Status.Closed, CELDAS.eTest_Mgr_Status.Loading, CELDAS.eTest_Mgr_Status.Loaded
                            clsTopMostMessageBox.Show("Test Session is running!, Please Close Test Session prior to Open it", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Case Else
                            clsTopMostMessageBox.Show("CELDAS not connected or not ready.", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End Select


                    SYSTEM_CHECK_BTN.Value = Values.False
                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Sub OnOpen_Test(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (CELProject.IsTrue(newVal)) Then

                    Select Case CELProject.Test_Mgr_Status.Value.ToString()

                        Case CELDAS.eTest_Mgr_Status.Idling
                            Initialize_PasswordWindow()
                            CELProject.ActivateForm("Test_Password", "Welcome_Screen", True, False)


                            'CEL Internal developer tool...
                            If CELProject.UserDefaultPassword & "" <> "" Then
                                _txtPassword.Text = CELProject.UserDefaultPassword
                            End If

                            'CEL Internal developer tool...
                            If CELProject.UserDefaultEngine & "" <> "" Then
                                _cboEngineModel.Text = CELProject.UserDefaultEngine
                                CELProject.EngineModel = _cboEngineModel.Text
                                CELProject.EngineModel = CELProject.UserDefaultEngine
                            End If

                        Case CELDAS.eTest_Mgr_Status.Running, CELDAS.eTest_Mgr_Status.Closing, CELDAS.eTest_Mgr_Status.Closed, CELDAS.eTest_Mgr_Status.Loading, CELDAS.eTest_Mgr_Status.Loaded
                            clsTopMostMessageBox.Show("Test Session is running!, Please Close Test Session prior to Open it", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Case Else
                            clsTopMostMessageBox.Show("CELDAS not connected or not ready.", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End Select

                    Open_Test.Value = Values.False

                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnOpen_Test: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionClosing(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionClosing(newStatus)

                If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    CLOSE_TEST_BTN_OPC.Value = KineticaRT.Values.Values.False
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionClosing: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionPostClosing(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)

            Try

                MyBase.OnTestSessionPostClosing(newStatus)

                If Not CELProject.CLOSE_TEST_BTN Is Nothing Then
                    RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN
                End If

                Close_HMI(CELProject.EngineModel)

                If Not CELProject.ArincCtrl Is Nothing Then
                    CELProject.ArincCtrl.Dispose()
                End If
                If Not AfdxCtrl Is Nothing Then
                    AfdxCtrl.Dispose()
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionPostClosing: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionIdling(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionIdling(newStatus)

                'Reset Variables
                If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

                    CELProject.EngineModel = ""
                    CLOSE_TEST_BTN_OPC.Value = KineticaRT.Values.Values.False

                End If

                'Change Windows Cursor
                _frmWelcomeScreen.Cursor = Cursors.Arrow

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence: OnTestSessionIdling: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionPreOpening(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            MyBase.OnTestSessionPreOpening(newStatus)

            'TODO Commented OnTestSessionPreOpening Code as it is very specific : need to be reworked in case we have a master/slave
            'If CELProject.EngineModel <> "DAS CALIBRATION" And CELProject.EngineModel <> "SYSTEM CHECK" Then

            '    Dim Engine_Run_Frame As KineticaRT.Controls.Control = Nothing
            '    Dim MessageLogger As KineticaRT.Logic.Message.Display = Nothing

            '    If CELProject.EngineFamily <> "" Then
            '        MyBase.InitializeObject(Engine_Run_Frame, CELProject.EngineFamily.Replace("-", "_") + "_Screen." + CELProject.EngineFamily.Replace("-", "_") + "_EngineRun")
            '    Else
            '        MyBase.InitializeObject(Engine_Run_Frame, CELProject.EngineModel.Replace("-", "_") + "_Screen." + CELProject.EngineModel() + "_EngineRun")
            '    End If

            '    If Engine_Run_Frame Is Nothing Then
            '        MessageBox.Show("Initialize_Object - Engine_Run_Frame Is Nothing")
            '    Else
            '        MessageLogger = Engine_Run_Frame.FindObject("MessageLogger")

            '        If MessageLogger Is Nothing Then
            '            MessageBox.Show("Initialize_Object - MessageLogger Is Nothing")
            '        Else
            '            MessageLogger.Clear()
            '        End If
            '    End If

            'End If

        End Sub

        Protected Overrides Sub OnTestSessionOpened(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)

            'TODO: ************************************************************************************************************
            'On va elever le systemcheck du slave en attendant de fixer les problèmes de synchronisation entre le master/slave.
            'Il faudra le remettre losque le problème sera résolu.

            Try
                Dim CANBusChannelCollection As KineticaRT.ChannelCollection = Nothing
                Dim numericChannelCollection As KineticaRT.ChannelCollection = Nothing
                Dim booleanChannelCollection As KineticaRT.ChannelCollection = Nothing
                Dim messagesChannelCollection As KineticaRT.ChannelCollection = Nothing
                Dim engineChannelCollection As KineticaRT.ChannelCollection = Nothing
                Dim arincUartChannelCollection As KineticaRT.ChannelCollection = Nothing
                Dim systemCheckChannelCollection As KineticaRT.ChannelCollection = Nothing

                'Using rootTrans = New esTransactionScope()
                'On desactive les transactions pour eviter qu.entityspaces lock les tables access causant des exceptions lorsque 2 HMI roule en meme temps
                'ce qui donnait les erreurs sur le slave:
                'Cannot open the Microsoft Jet engine workgroup information file.
                'The database has been placed in a state by an unknown user that prevents it form being opened or locked
                'System.Data.OleDbException (0x8004005) Could not use ''; file already in use

                MyBase.OnTestSessionOpened(newStatus)

                If Me.DatabaseNotPresent Then
                    Debug.Assert(False, "Cannot find database")
                    Exit Sub  'On a determine plus tot dans le code qu'on a un probleme. Un message d'erreur a ete produit, inutile de generer d'autres probleme qui decoule de cette erreur.
                End If

                If CELProject.EngineModel <> "DAS CALIBRATION" And CELProject.EngineModel <> "SYSTEM CHECK" Then

                    Debug.Assert(CELProject.EngineModel <> "Empty", "OpenTestSequence.OnTestSessionOpened.CELProject.EngineModel")
                    Debug.Assert(CELProject.EngineModel <> "", "OpenTestSequence.OnTestSessionOpened.CELProject.EngineModel")
                    Debug.Assert(CELProject.Test_Mgr_UUT_Answer(0).Value.ToString() <> "", "OpenTestSequence.OnTestSessionOpened.CELProject.Test_Mgr_UUT_Answer(0).Value.ToString")
                    Debug.Assert(CELProject.Test_Mgr_UUT_Answer(1).Value.ToString() <> "", "OpenTestSequence.OnTestSessionOpened.CELProject.Test_Mgr_UUT_Answer(1).Value.ToString")

                End If

                'Si la session est running, il faut resetté la variable pcq c'est dans l'événement de la combo box que normalement le EngineModel et EngineFamiliy est setté
                CELProject.EngineModel = CELProject.EngineModel

                If CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then 'Bug Fix le slave ne recuperait pas le bon etat si en simulation ou pas
                    CELProject.DataAcquisitionMode = CELProject.Test_Mgr_Mode.Value.ToDouble
                End If


                Dim engine As Uut = Nothing
                'If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                '    engine = _cboEngineModel.SelectedItem
                'Else 'Slave


                'End If

                If Not UserInputInformationLoaded Then

                    ''Get the Options - Questions - Choices associated with the selected Engine Model
                    ''Query Database
                    Dim engineCollection As UutCollection = New UutCollection()
                    engineCollection.es.Connection.Name = CELProject.DefaultConnectionName
                    engineCollection.Query.Where(engineCollection.Query.ProjectID = CELProject.Project.Number)
                    engineCollection.Query.Where(engineCollection.Query.EngineModel = CELProject.EngineModel)
                    engineCollection.Query.Load()

                    If engineCollection.Count = 1 Then

                        engine = engineCollection.Item(0)

                    End If

                    'Restoring a Session, So Reload the correct User Input Information from Database
                    Me.UserInputInformationLoaded = True
                    Load_User_Inputs(engine, True)
                    CELProject.PWCEECPartNumber = UserInputInfo.FindPWCEECPartNumber(CELProject.EngineModel)
                End If

                'We must do the function here for the slave as it is already done in the User Inputs Validation for the Master for the CSV generation.
                If CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then
                    'Load_User_Inputs(engine, True)
                    CELProject.PWCEECPartNumber = UserInputInfo.FindPWCEECPartNumber(CELProject.EngineModel)
                End If

                '******************************************************
                'CELProject.InitializeAndConnectEngineWindows() 'Maintenant on initialise et connecte uniquement les fenetres nécessaires pour le moteur
                '******************************************************
                Create_Engine_Folder(CELProject.EngineModelFormatted, CELProject.Test_Mgr_UUT_Answer(0).Value.ToString(), CELProject.Test_Mgr_UUT_Answer(1).Value.ToString())

                CELProject.ChannelList.Clear()
                EngineMappingList.Clear()

                AfdxCtrl = New AFDX()
                CELProject.ArincCtrl = New ARINC()

                'Using noTrans As New esTransactionScope(esTransactionScopeOption.Suppress)
                Using noTrans As New esTransactionScope()

                    'Query Database
                    Dim uutInfo As Uut = New Uut()
                    uutInfo.es.Connection.Name = CELProject.DefaultConnectionName
                    uutInfo.Query.Where(uutInfo.Query.ProjectID = CELProject.Project.Number)
                    uutInfo.Query.Where(uutInfo.Query.EngineModel = CELProject.EngineModel)
                    uutInfo.Query.Load()

                    'Si la session est running, il faut resetté la variable pcq c'est dans l'événement de la combo box que normalement le EngineModel et EngineFamily est setté
                    If uutInfo.EngineFamily & "" <> "" Then 'Use EngineFamily architecture for the DAS
                        CELProject.EngineFamily = uutInfo.EngineFamily
                    Else
                        CELProject.EngineFamily = ""
                    End If


                    numericCollectionName = "Numeric"
                    booleanCollectionName = "Boolean"
                    openTestCollectionName = "OpenTest"
                    archiveCollectionName = "Archive"
                    engineCollectionName = ""
                    messagesCollectionName = ""
                    logPerfCollectionName = ""
                    arincUartCollectionName = ""
                    arincCollectionName = "ARINC"
                    canBusCollectionName = "CANBus"
                    engineLimitsCollectionName = "EngineLimits"
                    If (CELProject.EngineModel = "SYSTEM CHECK" Or CELProject.DebugMode = True) And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                        systemCheckCollectionName = "SYSTEM_CHECK"
                    End If


                    If uutInfo.EngineFamily & "" <> "" Then 'Use EngineFamily architecture for the DAS
                        'CELProject.EngineFamily = uutInfo.EngineFamily
                        messagesCollectionName = uutInfo.EngineFamily + "_Messages"
                        engineCollectionName = uutInfo.EngineFamily
                        logPerfCollectionName = uutInfo.EngineFamily + "_Log_Perf"
                        arincUartCollectionName = uutInfo.EngineFamily + "_Arinc_Uart"
                    Else 'Use Engine Only architecture for the DAS
                        'CELProject.EngineFamily = ""
                        messagesCollectionName = CELProject.FormatEngineModel(uutInfo.EngineModel) + "_Messages"
                        engineCollectionName = CELProject.FormatEngineModel(uutInfo.EngineModel)
                        logPerfCollectionName = CELProject.FormatEngineModel(uutInfo.EngineModel) + "_Log_Perf"
                        arincUartCollectionName = CELProject.FormatEngineModel(uutInfo.EngineModel) + "_Arinc_Uart"
                    End If

                    'Create Collections
                    CANBusChannelCollection = CELProject.CreateCollection(canBusCollectionName)
                    numericChannelCollection = CELProject.CreateCollection(numericCollectionName)
                    booleanChannelCollection = CELProject.CreateCollection(booleanCollectionName)
                    If CELProject.EngineModel <> "SYSTEM CHECK" AndAlso CELProject.EngineModel <> "DAS CALIBRATION" Then
                        messagesChannelCollection = CELProject.CreateCollection(messagesCollectionName)
                    End If
                    engineChannelCollection = CELProject.CreateCollection(engineCollectionName)
                    arincUartChannelCollection = CELProject.CreateCollection(arincUartCollectionName)
                    If (CELProject.EngineModel = "SYSTEM CHECK" Or CELProject.DebugMode = True) And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                        systemCheckChannelCollection = CELProject.CreateCollection(systemCheckCollectionName)
                    End If


                    'Initialisation de LogPerfSynch
                    CELProject.LogPerfSynch.ChannelCollection = CELProject.CreateCollection(logPerfCollectionName)
                    CELProject.LogPerfSynch.SetInputOutputRegExString(InputOutputRegEx)

                    Debug.Assert(Not IsNothing(CELProject.LogPerfSynch.ChannelCollection))

                    Select Case False

                        Case CreateChannelsDefaultCANBus(CANBusChannelCollection)
                            clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionOpened: Create_Channels(CANBusChannelCollection)", "", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Case CreateChannelsDefaultNumeric(numericChannelCollection)
                            clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionOpened: Create_Channels(NUMERIC)", "", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Case CreateChannelsDefaultBoolean(booleanChannelCollection)
                            clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionOpened: OPEN SEQUENCE: Create_Channels(BOOLEAN)", "", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Case LoadUnloadStaticCollections(CELProject.EngineModel) 'TODO: Dans les prochains moteur essayer d'eliminer autant que possible les collections statiques
                            clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionOpened: Load_Unload_Static_Collections()", "", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End Select

                    'Par contre, si on est sur le slave, en calibration, on ne charge pas la collection
                    If CELProject.EngineModel = "DAS CALIBRATION" And CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then
                        LoadUnloadStaticCollections(CELProject.EngineModel, True)
                    End If

                    If (CELProject.EngineModel = "SYSTEM CHECK" Or (CELProject.DebugMode = True And CELProject.EngineModel <> "SYSTEM CHECK")) And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                        LoadUnloadStaticCollections("SYSTEM CHECK")
                        systemCheckChannelCollection = CELProject.CreateCollection("SYSTEM_CHECK")
                        Create_Engine_Channels("SYSTEM CHECK", systemCheckChannelCollection, numericChannelCollection)
                        ConnectCollection("SYSTEM_CHECK")
                    End If

                    'Query Database
                    Dim dynamicChannelCollection As UUTDynamicChannelCollectionCollection = New UUTDynamicChannelCollectionCollection()
                    dynamicChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                    dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.ProjectID = CELProject.Project.Number)
                    dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.EngineModel = CELProject.EngineModel)
                    dynamicChannelCollection.Query.Where(dynamicChannelCollection.Query.Unavailable = False)
                    dynamicChannelCollection.Query.OrderBy(dynamicChannelCollection.Query.LoadOrder.Ascending)
                    dynamicChannelCollection.Query.Load()

                    'Load All possible mapping for different collection name in a SPECIFIC ENGINE MODEL
                    For Each record As UUTDynamicChannelCollection In dynamicChannelCollection

                        Dim engineChannelsMappingCollection As HMIChannelsMappingCollection = New HMIChannelsMappingCollection()
                        engineChannelsMappingCollection.es.Connection.Name = CELProject.DefaultConnectionName
                        engineChannelsMappingCollection.Query.Where(engineChannelsMappingCollection.Query.ProjectID = CELProject.Project.Number)
                        engineChannelsMappingCollection.Query.Where(engineChannelsMappingCollection.Query.CollectionName = record.CollectionName)
                        engineChannelsMappingCollection.Query.Where(engineChannelsMappingCollection.Query.Unavailable = False)
                        engineChannelsMappingCollection.Query.Load()

                        For Each engineMapRecord As HMIChannelsMapping In engineChannelsMappingCollection
                            Try
                                EngineMappingList.Add(engineMapRecord.ChannelName, engineMapRecord.FVName)
                            Catch ex As Exception
                                clsTopMostMessageBox.Show("The channel " + engineMapRecord.ChannelName + " already exist in the collection EngineMappingList", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
                            Try
                                CELProject.ChannelList.Add(engineMapRecord.ChannelName, engineMapRecord.FVName)
                            Catch ex As Exception
                                clsTopMostMessageBox.Show("The channel " + engineMapRecord.ChannelName + " already exist in the collection ChannelList", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try

                        Next
                    Next

                    Validate_HmiMappingFieldIntegrity()

                    LoadEngineArchiveLayout(CELProject.EngineModel)

                    LoadArincChannelsForChannelList(CELProject.EngineModel)

                    LoadUartChannelsForChannelList(CELProject.EngineModel)

                    Dim folderName As String = "C:\CELDAS DATA\" + CELProject.EngineModel + "\" + CELProject.Test_Mgr_UUT_Answer(0).Value.ToString() + "\" + CELProject.Test_Mgr_UUT_Answer(1).Value.ToString() + "\log\Messages.Log"

                    For Each record As UUTDynamicChannelCollection In dynamicChannelCollection

                        If CELProject.EngineModel <> "DAS CALIBRATION" AndAlso CELProject.EngineModel <> "SYSTEM CHECK" _
                            AndAlso CELProject.ArincCtrl.Initialize(record.CollectionName, arincUartChannelCollection, CELProject.DicArchiveChannelList, CELProject.EngineConfiguration.ArincConfiguration) = False Then 'Le SearchCriteria Model de la table ARINC_429... fonctionne par modele de moteur
                            clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionOpened: ArincCtrl.Initialize()", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        'Le SearchCriteria Model de la table ARINC_429... fonctionne par modele de moteur
                        If AfdxCtrl.Initialize(record.CollectionName, arincUartChannelCollection, CELProject.DicArchiveChannelList) = False Then
                            clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionOpened: AfdxCtrl.Initialize(()", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        If Create_Engine_Channels(record.CollectionName, engineChannelCollection, numericChannelCollection) = False Then
                            clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionOpened: Create_Engine_Channels()", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        If CELProject.EngineModel <> "DAS CALIBRATION" AndAlso CELProject.EngineModel <> "SYSTEM CHECK" AndAlso
                            HMIChannelCreation.CreateChannelsMessagesThreshold(record.CollectionName, messagesChannelCollection, folderName) = False Then
                            clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionOpened: Create_Messages_Threshold_Channels()", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        If CELProject.EngineModel <> "DAS CALIBRATION" AndAlso CELProject.EngineModel <> "SYSTEM CHECK" AndAlso
                            HMIChannelCreation.CreateChannelMessageEvent(record.CollectionName, messagesChannelCollection) = False Then
                            clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionOpened: Create_Messages_Event_Threshold_Channels()", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    Next

                    noTrans.Complete()

                End Using

                HMIChannelCreation.CreateChanneNumericExpressionMeanForOpcNumericItem(arincCollectionName, numericChannelCollection) 'Required for LogPerfSynch
                HMIChannelCreation.CreateChanneNumericExpressionMeanForOpcNumericItem(arincUartCollectionName, numericChannelCollection) 'Required for LogPerfSynch
                HMIChannelCreation.CreateChanneNumericExpressionMeanForOpcNumericItem(engineLimitsCollectionName, numericChannelCollection) 'Required for LogPerfSynch
                HMIChannelCreation.CreateChanneNumericExpressionMeanForOpcNumericItem(canBusCollectionName, numericChannelCollection) 'Required for LogPerfSynch

                If (CELProject.EngineModel = "SYSTEM CHECK" Or CELProject.DebugMode = True) And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    'Verify the connection to the channels
                    If Init_Channels() = False Then
                        clsTopMostMessageBox.Show("SystemCheck_Sequence.OnTestSessionInitialized.Init_Channels:  missing channels", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    SystemCheck = New SystemCheck(recorderName, eventRecorderName, BoolInitChannel, BoolInitChannelValue, BoolInitIndex, systemCheckChannelCollection)
                    SystemCheck.CreateChannelsForSerialBoolPanel()
                    SystemCheck.CreateChannelsForSerialRealPanel()
                    SystemCheck.CreateChannelsForNidaqBoolPanel()
                    SystemCheck.CreateChannelsForNidaqRealPanel()
                    SystemCheck.CreateChannelsForPowerDnaPanel()
                    SystemCheck.CreateChannelsForScanivalvePanel()

                End If

                AfdxCtrl.InitializeGrid(CELProject.EngineModel)

                'Read TestSession Limits
                Read_Limits()

                'CELProject.RefreshExplorer(coreArchiveEventArchiveCollectionName)
                CELProject.RefreshExplorer(canBusCollectionName)
                CELProject.RefreshExplorer(arincUartCollectionName)
                CELProject.RefreshExplorer(arincCollectionName)
                CELProject.RefreshExplorer(engineCollectionName)
                CELProject.RefreshExplorer(CELProject.EngineModelFormatted.Replace("-", "_") + "_Channels")
                If CELProject.EngineModel <> "SYSTEM CHECK" AndAlso CELProject.EngineModel <> "DAS CALIBRATION" Then
                    CELProject.RefreshExplorer(messagesCollectionName)
                End If
                'CELProject.RefreshExplorer(coreCollectionName) 'Certains channel OPC de cette collection avait un Not Connected... 
                CELProject.RefreshExplorer(booleanCollectionName)
                CELProject.RefreshExplorer(numericCollectionName)
                If (CELProject.EngineModel = "SYSTEM CHECK" Or CELProject.DebugMode = True) And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    CELProject.RefreshExplorer(systemCheckCollectionName)
                End If

                'CELProject.LogPerfSynch.AddCollection(coreArchiveEventArchiveCollectionName)
                CELProject.LogPerfSynch.AddCollection(engineCollectionName, True)
                ' pas besoin CELProject.LogPerfSynch.AddCollection(messagesCollectionName, True)
                CELProject.LogPerfSynch.AddCollection(arincUartCollectionName)

                'NB: The "Numeric" COLLECTION must be added after that Engine Collection was created, 
                'because New Channels are created on demand while processing engine formulas and then new channels are added to Numeric Collection

                CELProject.LogPerfSynch.AddCollection(numericCollectionName)
                CELProject.LogPerfSynch.AddCollection(booleanCollectionName)
                CELProject.LogPerfSynch.AddCollection(coreCollectionName)
                CELProject.LogPerfSynch.AddCollection(engineLimitsCollectionName)
                CELProject.LogPerfSynch.AddCollection(openTestCollectionName)
                CELProject.LogPerfSynch.AddCollection(arincCollectionName) 'TODO ARINC ...
                CELProject.LogPerfSynch.AddCollection(canBusCollectionName)
                CELProject.LogPerfSynch.AddCollection(CELProject.EngineModelFormatted.Replace("-", "_") + "_Channels")
                'ft pas necessaire If (CELProject.EngineModel = "SYSTEM CHECK" Or CELProject.DebugMode = True) And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                '    CELProject.LogPerfSynch.AddCollection(systemCheckCollectionName)
                'End If

                ConnectCollection(numericCollectionName) 'Le connect numeric doit etre refait pcq lors du Call de Create_Engine_Channels, il se peut qu<on ajoute d<autres channels avec diferentes unites dans la collection Numeric
                '**************************************************************************************
                '**************************************************************************************
                '**************************************************************************************
                '**************************************************************************************
                '**************************************************************************************
                'See code for in CreateChannelsDefaultBoolean specific to the Performance computer that cannot affect the DOUTS on QNX
                ConnectCollection(booleanCollectionName)
                'ConnectCollection(archiveCollectionName)
                ConnectCollection(arincUartCollectionName)
                ConnectCollection(CELProject.EngineModelFormatted + "_Channels")
                ConnectCollection(logPerfCollectionName)

                'ConnectCollection(coreCollectionName)

                ConnectCollection(canBusCollectionName)
                ConnectCollection(arincUartCollectionName)
                ConnectCollection(engineCollectionName)
                If CELProject.EngineModel <> "SYSTEM CHECK" AndAlso CELProject.EngineModel <> "DAS CALIBRATION" Then
                    ConnectCollection(messagesCollectionName) 'Pour ces moteurs on ne cree pas les repertoires, alors le path de fichier de message log n'extiste pas et ca lance une exception
                End If

                If (CELProject.EngineModel = "SYSTEM CHECK" Or CELProject.DebugMode = True) And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    ConnectCollection(systemCheckCollectionName)
                End If


                CELProject.RefreshExplorer(logPerfCollectionName)

                'CELProject.HMIProject.Connect() 'ft desactivé est-ce necessaire?...ca marcher sans
                '******************************************************
                For Each windowInfo As KineticaRT.Studio.Application.Project.WindowInfo In CELProject.HMIProject.WindowsInfo
                    If windowInfo.Window.Name <> "Welcome_Screen" And windowInfo.Window.Name <> "Test_Password" And windowInfo.Window.Name <> "UserInputs" And windowInfo.Window.Name <> "MeasurementUnitSelector" Then
                        windowInfo.Window.Disconnect()  'Disconnect toutes les windows pour eviter l'interference entre chaque ecran moteur START_BTN est sur tous les ecran, mais si le type AutoReturn...Noraml est differrent d'un ecran a l'autre ca corromp la logique
                    End If
                Next


                'CELProject.InitializeAndConnectEngineWindows() 'TODO: Cette ligne deplace dans Open_HMI, pcq les call Initialize_SC_Serial_Bool()    Initialize_SC_Serial_Real() ... se font dans le OnTestSessionInitialized
                'TODO ramener les load Initialize_SC_Serial_Bool() Initialize_SC_Serial_Real() ... etc  de seuqence SystemCheck dans l'OpenTest
                '******************************************************

                CELProject.EngineWindowCollection.Item(0).EngineWindow.BeginInvoke(New MethodInvoker(AddressOf Me.UpdateHMIUserControls))

                'We just create the channel and the initial value was not taken in consideration
                'We recorded which one needed to be initialized
                'now this is the time to initialize the boolean value
                Thread.Sleep(4000)
                If BoolInitIndex <> 0 Then

                    For NumberIteration = 0 To BoolInitIndex - 1
                        Dim tempChannel As IChannel

                        tempChannel = GetChannel(BoolInitChannel(NumberIteration).Name)
                        tempChannel.Value = BoolInitChannelValue(NumberIteration)
                    Next

                    BoolInitIndex = 0
                End If

                If CELProject.EngineModel = "Empty" Or CELProject.EngineModel = "" Then
                    clsTopMostMessageBox.Show("OpenTestSequence.OnTestSessionOpened.CELProject.Test_Mgr_UUT_Model is Empty", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    CELProject.Test_Mgr_Cmd.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Cmd.Close)
                ElseIf CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    CELProject.Test_Mgr_Cmd.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Cmd.Load)
                End If
                ENGINE_FAMILY.Value = New KineticaRT.Values.Text(CELProject.EngineFamily)
                ENGINE_MODEL.Value = New KineticaRT.Values.Text(CELProject.EngineModel)

                'rootTrans.Complete()
                'End Using
            Catch ex As Exception

                clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionOpened: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try
        End Sub

        Protected Overrides Sub OnTestSessionLoaded(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionLoaded(newStatus)


                If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    CELProject.Test_Mgr_Cmd.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Cmd.Initialize)
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionLoaded: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionInitialized(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionInitialized(newStatus)

                If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    CELProject.Test_Mgr_Cmd.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Cmd.Run)
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionInitialized: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionRunning(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try

                MyBase.OnTestSessionRunning(newStatus)

                If Me.DatabaseNotPresent Then
                    Debug.Assert(False)
                    Exit Sub  'On a determine plus tot dans le code qu'on a un probleme. Un message d'erreur a ete produit, inutile de generer d'autres probleme qui decoule de cette erreur.
                End If

                Me.Open_HMI(CELProject.EngineModel)

                AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN

                'Change Windows Cursor
                _frmWelcomeScreen.Cursor = Cursors.Arrow

                If CELProject.DebugMode = True And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                    'TODO: Enlever la codition pour hierarchy type quand les problemes de synchronisations seront reglés.
                    CELProject.ActivateForm("SystemCheck_Screen", Nothing, True, False)
                End If

            Catch ex As Exception

                clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionRunning: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try
        End Sub

        Private Sub OnWindowClosing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
            Try
                If (sender Is _frmWelcomeScreen) Then
                    Try
                        If (CELProject.Test_Mgr_Status.Value.ToString() = CELDAS.eTest_Mgr_Status.Running) Then
                            Dim msg, title As String
                            Dim style As MsgBoxStyle

                            msg = "If the engine is running, control of the engine will be lost." + Chr(13) +
                             "Shutdown the engine and close test." + Chr(13) +
                             "Do you want to exit now ?"
                            style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo Or MsgBoxStyle.Question
                            title = "Exit CELDAS200 Confirmation"
                            If (MsgBox(msg, style, title) <> MsgBoxResult.Yes) Then
                                e.Cancel = True
                            Else
                                e.Cancel = False
                            End If
                        End If

                        If e.Cancel = False Then
                            'Thread.Sleep(1000)
                            If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then 'le slave ne doit pas envoyer le close, pcq le master va perdre le controle moteur
                                CELProject.Test_Mgr_Cmd.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Cmd.Close)
                            End If

                            'Change Windows Cursor
                            Cursor.Current = Cursors.WaitCursor
                            Thread.Sleep(4000)
                        End If
                    Catch exception As Exception
                        '
                        ' Test_Mgr_Status is NotConnected
                        ' Should be OK to Close
                        '
                        e.Cancel = False
                    End Try
                ElseIf (sender Is frmChannelSelector) Then
                    e.Cancel = True
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnWindowClosing: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Private Sub Initialize_Archive(ByRef archiveChannelCollection As KineticaRT.ChannelCollection)
            Dim ARCHIVE As New KineticaRT.Servers.Logging.CsvFile
            Dim EVENT_ARCHIVE As New KineticaRT.Servers.Logging.CsvFile
            Dim archiveFulFileName As String = String.Empty
            Dim eventArchiveFullFileName As String = String.Empty

            ARCHIVE.Name = "ARCHIVE"
            EVENT_ARCHIVE.Name = "EVENT_ARCHIVE"

            HMIChannelCreation.CreateChannelsEngineBoolean(coreArchiveEventArchiveCollectionName, archiveChannelCollection, EngineMappingList, BoolInitChannel, BoolInitChannelValue, BoolInitIndex)
            HMIChannelCreation.CreateChannelsEngineNumeric(coreArchiveEventArchiveCollectionName, archiveChannelCollection, Nothing, numericCollectionName, EngineMappingList, InputOutputRegEx, Me.GetRegexOptions())
            HMIChannelCreation.CreateChannelsEngineText(coreArchiveEventArchiveCollectionName, archiveChannelCollection)

        End Sub


        Private Function Create_Archive_Channels() As Boolean

            Dim Boolean_Channel As KineticaRT.Logic.Channels.BooleanVariable
            Dim ChannelCollection As KineticaRT.ChannelCollection
            Dim ChannelsInfo As KineticaRT.Studio.Application.Project.ChannelsInfo
            Dim CsvFile_Channel As New CEL.KineticaRT.Servers.Logger.CsvFile
            Dim Numeric_Channel As KineticaRT.Logic.Channels.NumericVariable
            Dim Text_Channel As KineticaRT.Logic.Channels.TextVariable

            Try
                ChannelsInfo = CELProject.HMIProject.GetChannelsInfo("Archive")
                If Not (ChannelsInfo Is Nothing) Then
                    CELProject.HMIProject.RemoveChannels(ChannelsInfo)
                End If

                CELProject.HMIProject.AddChannelCollection("", "Archive", True)
                ChannelCollection = GetObject("Archive")

                If ChannelCollection Is Nothing Then
                    Create_Archive_Channels = False
                    Exit Function
                End If

                CsvFile_Channel.MaxFileSize = 0
                CsvFile_Channel.Pause = "ARCHIVE_PAUSE"
                CsvFile_Channel.Play = "ARCHIVE_PLAY"
                CsvFile_Channel.PlaySpeed = "ARCHIVE_PLAYSPEED"
                CsvFile_Channel.Record = "ARCHIVE_RECORD"
                CsvFile_Channel.RecordSpeed = "ARCHIVE_RECORDSPEED"
                CsvFile_Channel.Step = "ARCHIVE_STEP"
                CsvFile_Channel.Stop = "ARCHIVE_STOP"
                CsvFile_Channel.Time = "ARCHIVE_TIME"
                CsvFile_Channel.Name = "ARCHIVE"
                ChannelCollection.Add(CsvFile_Channel)

                Boolean_Channel = New KineticaRT.Logic.Channels.BooleanVariable
                Boolean_Channel.FalseText = ""
                Boolean_Channel.TrueText = "Pause"
                Boolean_Channel.Description = "Pause Archive File"
                Boolean_Channel.ToolTip = ""
                Boolean_Channel.Name = "ARCHIVE_PAUSE"
                ChannelCollection.Add(Boolean_Channel)

                Boolean_Channel = New KineticaRT.Logic.Channels.BooleanVariable
                Boolean_Channel.FalseText = ""
                Boolean_Channel.TrueText = "Play"
                Boolean_Channel.Description = "Play Archive File"
                Boolean_Channel.ToolTip = ""
                Boolean_Channel.Name = "ARCHIVE_PLAY"
                ChannelCollection.Add(Boolean_Channel)

                Boolean_Channel = New KineticaRT.Logic.Channels.BooleanVariable
                Boolean_Channel.FalseText = ""
                Boolean_Channel.TrueText = "Play"
                Boolean_Channel.Description = "Play Archive File"
                Boolean_Channel.ToolTip = ""
                Boolean_Channel.Name = "PLAY_BUTTON"
                ChannelCollection.Add(Boolean_Channel)

                Numeric_Channel = New KineticaRT.Logic.Channels.NumericVariable
                Numeric_Channel.Description = "Play Speed"
                Numeric_Channel.ToolTip = ""
                Numeric_Channel.DecimalDigits = 0
                Numeric_Channel.Format = "F1"
                Numeric_Channel.Maximum = 10
                Numeric_Channel.Minimum = 0.1
                Numeric_Channel.Units = " Hz"
                Numeric_Channel.Name = "ARCHIVE_PLAYSPEED"
                Numeric_Channel.Value = New KineticaRT.Values.Double(1)
                ChannelCollection.Add(Numeric_Channel)

                Boolean_Channel = New KineticaRT.Logic.Channels.BooleanVariable
                Boolean_Channel.FalseText = ""
                Boolean_Channel.TrueText = "Record"
                Boolean_Channel.Description = "Record Archive File"
                Boolean_Channel.ToolTip = ""
                Boolean_Channel.Name = "ARCHIVE_RECORD"
                ChannelCollection.Add(Boolean_Channel)

                Numeric_Channel = New KineticaRT.Logic.Channels.NumericVariable
                Numeric_Channel.Description = "Record Speed"
                Numeric_Channel.ToolTip = ""
                Numeric_Channel.DecimalDigits = 0
                Numeric_Channel.Format = "F0"
                Numeric_Channel.Maximum = 10
                Numeric_Channel.Minimum = 0
                Numeric_Channel.Units = " Hz"
                Numeric_Channel.Name = "ARCHIVE_RECORDSPEED"
                Numeric_Channel.Value = New KineticaRT.Values.Double(1)
                ChannelCollection.Add(Numeric_Channel)

                Boolean_Channel = New KineticaRT.Logic.Channels.BooleanVariable
                Boolean_Channel.FalseText = ""
                Boolean_Channel.TrueText = "Step"
                Boolean_Channel.Description = "Step Archive File"
                Boolean_Channel.ToolTip = ""
                Boolean_Channel.Name = "ARCHIVE_STEP"
                ChannelCollection.Add(Boolean_Channel)

                Boolean_Channel = New KineticaRT.Logic.Channels.BooleanVariable
                Boolean_Channel.FalseText = ""
                Boolean_Channel.TrueText = "Stop"
                Boolean_Channel.Description = "Stop Archive File"
                Boolean_Channel.ToolTip = ""
                Boolean_Channel.Name = "ARCHIVE_STOP"
                ChannelCollection.Add(Boolean_Channel)

                Text_Channel = New KineticaRT.Logic.Channels.TextVariable
                Text_Channel.Description = "Archive File Time"
                Text_Channel.ToolTip = ""
                Text_Channel.Name = "ARCHIVE_TIME"
                ChannelCollection.Add(Text_Channel)

                'Refresh Explorer			
                ChannelsInfo = CELProject.HMIProject.GetChannelsInfo("Archive")
                CELProject.HMIProject.UpdateChannels(ChannelsInfo)

                Create_Archive_Channels = True

            Catch ex As Exception
                MessageBox.Show("OpenTest_Sequence: Create_Archive_Channels: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        '******************
        '   Collections   *
        '******************

        Private numericCollectionName As String
        Private booleanCollectionName As String
        Private openTestCollectionName As String
        Private archiveCollectionName As String
        Private engineCollectionName As String
        Private messagesCollectionName As String
        Private logPerfCollectionName As String
        Private arincUartCollectionName As String
        Private arincCollectionName As String
        Private canBusCollectionName As String
        Private coreCollectionName As String
        Private engineLimitsCollectionName As String
        Private coreArchiveEventArchiveCollectionName As String
        Private systemCheckCollectionName As String
        '************
        '* Channels *
        '************

        Private Cancel_MeasurementUnit As IBooleanChannel
        Private Cancel_Password As IBooleanChannel
        Private Cancel_UserInputs As IBooleanChannel
        Private CLOSE_TEST_BTN_OPC As IBooleanChannel
        Private DAS_CALIBRATION_BTN As IBooleanChannel
        Private OK_MeasurementUnit As IBooleanChannel
        Private OK_Password As IBooleanChannel
        Private OK_UserInputs As IBooleanChannel
        Private Open_Test As IBooleanChannel
        Private SYSTEM_CHECK_BTN As IBooleanChannel
        Private DEBUG_MODE As IBooleanChannel
        Private ProjectNumber As INumericChannel
        Private Test_Mgr_UUT_Model As IChannel
        Private Test_Mgr_UUT_Family As IChannel
        Private ENGINE_MODEL As ITextChannel
        Private ENGINE_FAMILY As ITextChannel

        '*********************
        '* Forms and Control *
        '*********************
        Private _btnOpenTest As System.Windows.Forms.Control
        Private _btnDasCalibration As System.Windows.Forms.Control
        Private _btnSystemCheck As System.Windows.Forms.Control


        Private frmChannelSelector As KineticaRT.Studio.Controls.ChannelSelectorForm

        Private _cboEngineModel As System.Windows.Forms.ComboBox
        Private _cboMeasurementUnit As System.Windows.Forms.ComboBox
        Private _txtPassword As System.Windows.Forms.TextBox
        Private _frmWelcomeScreen As System.Windows.Forms.Form

        '********
        '* Misc *
        '********
        Private UserInputInformationLoaded As Boolean = False

        Private DatabaseNotPresent As Boolean = True

        Private UserInputInfo As UserInputInformation

        Private BoolInitChannel(200) As Object
        Private BoolInitChannelValue(200) As Value
        Private BoolInitIndex As Integer = 0
        Private NumberIteration As Integer

        Private AfdxCtrl As AFDX
        'Private ArincCtrl As ARINC
        Private EngineMappingList As New Dictionary(Of String, String)

        'Private ChannelList As New Dictionary(Of String, String)
        'Private DicEngUnitName As New Collections.Generic.Dictionary(Of Integer, String)
        'Private DicEngUnitDisplayName As New Collections.Generic.Dictionary(Of Integer, String)
        'Private DicArchiveChannelList As New Collections.Generic.Dictionary(Of String, Integer) 'used to keep channels that will be recorded and their recorder Index

        Private InputOutputRegEx As String

        Public Sub New()

        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace
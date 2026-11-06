Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms
Imports KineticaRT
Imports KineticaRT.Channels
Imports KineticaRT.Sequence
Imports KineticaRT.Values
Imports Microsoft.VisualBasic

Imports CEL.Library
Imports CEL.HMI
Imports CEL.KineticaRT.Sequence

Namespace StudioDemonstration

    '<System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Unrestricted:=True)> _
    Public Class Archive_Sequence
        Inherits CEL.KineticaRT.Sequence.Sequence

        Private Const ARCHIVE_PLAYSPEED_DEFAULT_VALUE As Double = 1.0
        Private Const ARCHIVE_RECORDSPEED_DEFAULT_VALUE As Double = 5.0

        Private CELProject As CELDAS = CELDAS.Instance()

        Public Overloads Overrides Sub Connect()
            Try
                If Me.Connected = False Then
                    MyBase.Connect()

                    'Verify the connection to the channels
                    If Init_Channels() Then

                        AddHandler ARCHIVE_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnArchive_Button
                        AddHandler ARCHIVE_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnClose_Archive_Button

                    Else
                        MessageBox.Show("Archive_Sequence.Connect: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & "Missing channels", "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    Me.Connected = True
                End If

            Catch Ex As Exception
                MessageBox.Show("Archive_Sequence.Connect: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Overloads Overrides Sub Disconnect()
            Try
                ExitThread = True
                If Me.Connected Then
                    RemoveHandler ARCHIVE_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnArchive_Button
                    RemoveHandler ARCHIVE_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnClose_Archive_Button

                    MyBase.Disconnect()
                    Me.Connected = False
                End If

            Catch Ex As Exception
                MessageBox.Show("Archive_Sequence.Disconnect: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString())
            End Try
        End Sub
        Private Sub AddHandlers()

            AddHandler OPEN_ARCHIVE_FILE_BTN.ValueChanged, AddressOf OnArchive
            AddHandler ARCHIVE_RECORD_BTN.ValueChanged, AddressOf OnArchive
            AddHandler ARCHIVE_STOP_BTN.ValueChanged, AddressOf OnArchive
            AddHandler EVENT_ARCHIVE.PropertyChanged, AddressOf OnEventArchivePropertyChanged
            AddHandler ENGINE_AT_REST.ValueChanged, AddressOf OnEngineStateChanged
            AddHandler EVENT_ARCHIVE_PRETRIG.ValueChanged, AddressOf OnEventArchiveTrigValuesChanged
            AddHandler EVENT_ARCHIVE_POSTTRIG.ValueChanged, AddressOf OnEventArchiveTrigValuesChanged


        End Sub
        Private Sub RemoveHandlers()

            If Not IsNothing(OPEN_ARCHIVE_FILE_BTN) Then
                RemoveHandler OPEN_ARCHIVE_FILE_BTN.ValueChanged, AddressOf OnArchive
            End If
            If Not IsNothing(ARCHIVE_RECORD_BTN) Then
                RemoveHandler ARCHIVE_RECORD_BTN.ValueChanged, AddressOf OnArchive
            End If
            If Not IsNothing(ARCHIVE_STOP_BTN) Then
                RemoveHandler ARCHIVE_STOP_BTN.ValueChanged, AddressOf OnArchive
            End If
            If Not IsNothing(EVENT_ARCHIVE) Then
                RemoveHandler EVENT_ARCHIVE.PropertyChanged, AddressOf OnEventArchivePropertyChanged
            End If
            If Not IsNothing(ENGINE_AT_REST) Then
                RemoveHandler ENGINE_AT_REST.ValueChanged, AddressOf OnEngineStateChanged
            End If
            If Not IsNothing(EVENT_ARCHIVE_PRETRIG) Then
                RemoveHandler EVENT_ARCHIVE_PRETRIG.ValueChanged, AddressOf OnEventArchiveTrigValuesChanged
            End If
            If Not IsNothing(EVENT_ARCHIVE_POSTTRIG) Then
                AddHandler EVENT_ARCHIVE_POSTTRIG.ValueChanged, AddressOf OnEventArchiveTrigValuesChanged
            End If


        End Sub
        Protected Overrides Sub OnEngineWorkingThread()
            'Do Nothing
        End Sub

        Private Function Init_Channels() As Boolean
            Try

                Dim Return_Value As New ArrayList()

                Return_Value.Clear()

                'Initialize Archiving channels
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE, "ARCHIVE"))
                Return_Value.Add(MyBase.InitializeChannel(EVENT_ARCHIVE, "EVENT_ARCHIVE"))

                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_PLAYSPEED, "ARCHIVE_PLAYSPEED"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_RECORDSPEED, "ARCHIVE_RECORDSPEED"))

                Return_Value.Add(MyBase.InitializeChannel(EVENT_ARCHIVE_PRETRIG, "EVENT_ARCHIVE_PRETRIG"))
                Return_Value.Add(MyBase.InitializeChannel(EVENT_ARCHIVE_POSTTRIG, "EVENT_ARCHIVE_POSTTRIG"))

                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_PLAY, "ARCHIVE_PLAY"))
                Return_Value.Add(MyBase.InitializeChannel(OPEN_ARCHIVE_FILE_BTN, "OPEN_ARCHIVE_FILE_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(OPEN_ARCHIVE_FILE_BTN_ENB, "OPEN_ARCHIVE_FILE_BTN_ENB"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_STOP_BTN, "ARCHIVE_STOP_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_RECORD_BTN, "ARCHIVE_RECORD_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_STEP_BTN, "ARCHIVE_STEP_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_PAUSE_BTN, "ARCHIVE_PAUSE_BTN"))

                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_RECORDING_ON_MASTER_OPC, "ARCHIVE_RECORDING_ON_MASTER_OPC"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_RECORDING_ON_SLAVE_OPC, "ARCHIVE_RECORDING_ON_SLAVE_OPC"))

                'Return_Value.Add(MyBase.InitializeChannel(EVENT_ARCHIVE_STOP_BTN, "EVENT_ARCHIVE_STOP_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(EVENT_ARCHIVE_RECORD, "EVENT_ARCHIVE_RECORD"))
                'Return_Value.Add(MyBase.InitializeChannel(PLAY_BTN, "PLAY_BTN"))

                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_OPEN_WINDOW_BTN, "ARCHIVE_OPEN_WINDOW_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_CLOSE_WINDOW_BTN, "ARCHIVE_CLOSE_WINDOW_BTN"))

                If Return_Value.Contains(False) Then
                    Return False
                Else
                    Return True
                End If

            Catch Ex As Exception
                MessageBox.Show("Archive_Sequence.Init_Channels: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString())
                Return False
            End Try
        End Function

        Private Sub Init_Channels_Value()
            Try
                If CELProject.EngineModel <> "DAS CALIBRATION" AndAlso CELProject.EngineModel <> "SYSTEM CHECK" Then
                    OPEN_ARCHIVE_FILE_BTN.Value = Values.False
                    OPEN_ARCHIVE_FILE_BTN_ENB.Value = Values.True
                End If

            Catch Ex As Exception
                MessageBox.Show("Archive_Sequence.Init_Engine_Channels_Value: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnEngineStateChanged(ByVal sender As KineticaRT.Channels.IChannel, ByVal newVal As KineticaRT.Values.Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If ENGINE_AT_REST.Value = Values.True Then
                OPEN_ARCHIVE_FILE_BTN_ENB.Value = Values.True
            Else
                OPEN_ARCHIVE_FILE_BTN_ENB.Value = Values.False
            End If
        End Sub

        '*******************
        '* OnArchive()     *
        '*******************
        Private Sub OnArchive(ByVal sender As KineticaRT.Channels.IChannel, ByVal newVal As KineticaRT.Values.Value)
            Dim ChannelCollection As KineticaRT.ChannelCollection
            Dim Destination As String
            Dim FileName As String
            Dim myOpenFileDialog As OpenFileDialog
            Dim Source As String

            Try
                If sender.Name = "OPEN_ARCHIVE_FILE_BTN" Then
                    If (CELProject.IsTrue(newVal)) Then
                        myOpenFileDialog = New OpenFileDialog()
                        'TODO: Ici il faut rendre generique via une configuration
                        myOpenFileDialog.InitialDirectory = CELProject.ArchiveDestinationPath
                        myOpenFileDialog.Filter = "csv files (*.csv)|*.csv"
                        myOpenFileDialog.FilterIndex = 1
                        myOpenFileDialog.RestoreDirectory = True
                        myOpenFileDialog.Multiselect = False

                        If myOpenFileDialog.ShowDialog() = DialogResult.OK Then
                            FileName = myOpenFileDialog.FileName()

                            Try
                                'File.Delete(Application.StartupPath + "\PrjExt\Archive.csv")
                                'File.Copy(FileName, Application.StartupPath + "\PrjExt\Archive.csv")

                                If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                                    File.Delete(CELProject.Project.Rootpath + "\HMI\PrjExt\CRIT_Archive.csv")
                                    File.Copy(FileName, CELProject.Project.Rootpath + "\HMI\PrjExt\CRIT_Archive.csv")
                                Else
                                    File.Delete(CELProject.Project.Rootpath + "\HMI\PrjExt\PERF_Archive.csv")
                                    File.Copy(FileName, CELProject.Project.Rootpath + "\HMI\PrjExt\PERF_Archive.csv")
                                End If

                                PlayBack_Archive_Flag = True
                                Record_Archive_Flag = False

                                EVENT_ARCHIVE_RECORD.Value = Values.False
                                ARCHIVE_PLAY.Value = Values.True

                            Catch Ex As Exception
                                MessageBox.Show("OnArchive.Play.Copy: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Finally
                            End Try
                        Else

                            OPEN_ARCHIVE_FILE_BTN.Value = Values.False
                        End If

                        myOpenFileDialog.Dispose()
                    End If
                ElseIf sender.Name = "ARCHIVE_RECORD_BTN" Then
                    ARCHIVE.LatchedTrigger = True 'Allow to make a regular Manual Record Without Pre and Post Buffer
                    PlayBack_Archive_Flag = False

                    Record_Archive_Flag = True
                    If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                        ARCHIVE_RECORDING_ON_MASTER_OPC.Value = Values.True
                    ElseIf CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then
                        ARCHIVE_RECORDING_ON_SLAVE_OPC.Value = Values.True
                    End If

                ElseIf sender.Name = "ARCHIVE_STOP_BTN" Then
                    If (CELProject.IsTrue(newVal) And Record_Archive_Flag = True) Then

                        PlayBack_Archive_Flag = False

                        Record_Archive_Flag = False
                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            ARCHIVE_RECORDING_ON_MASTER_OPC.Value = Values.False
                        ElseIf CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then
                            ARCHIVE_RECORDING_ON_SLAVE_OPC.Value = Values.False
                        End If

                        'Source = Application.StartupPath + "\PrjExt\Archive.csv"
                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            Source = CELProject.Project.Rootpath + "\HMI\PrjExt\CRIT_Archive.csv"

                            'TODO: Ici il faut rendre générique via une configuration
                            'NB, Ici on utilise plus les channel kineticart pour formatter le path, PCQ si L'opc est trop rapide les channel n'ont pas le temps de se reconnecter
                            'et les variables Test_Mgr_uut_model... sont vides et ca crash.
                            Destination = CELProject.ArchiveDestinationPath + "CRIT_Archive " + Format(Now, "yyyy-MM-dd HH'h'mm'm'ss's'") + ".csv"
                        Else
                            Source = CELProject.Project.Rootpath + "\HMI\PrjExt\PERF_Archive.csv"

                            'TODO: Ici il faut rendre générique via une configuration
                            'NB, Ici on utilise plus les channel kineticart pour formatter le path, PCQ si L'opc est trop rapide les channel n'ont pas le temps de se reconnecter
                            'et les variables Test_Mgr_uut_model... sont vides et ca crash.
                            Destination = CELProject.ArchiveDestinationPath + "PERF_Archive " + Format(Now, "yyyy-MM-dd HH'h'mm'm'ss's'") + ".csv"
                        End If

                        Try
                            'TODO: Si ca plante ici c'est problablement la proriété FileName du CHANNEL ARCHIVE et CHANNEL EVENT_ARCHIVE dans l'explorer qui pointe pas au bon path!!!
                            'Faudrait trouver une facon d'aller l'initialiser!!
                            File.Copy(Source, Destination, True)
                        Catch Ex As Exception
                            MessageBox.Show("OnArchive.Stop.Copy: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End Try

                    ElseIf (CELProject.IsTrue(newVal) And PlayBack_Archive_Flag = True) Then

                        OPEN_ARCHIVE_FILE_BTN.Value = Values.False


                        ChannelCollection = GetObject(CELProject.EngineModel)

                        'ft j'Ai retiré les ChannelCollection.Disconnect/Connect ici-bas pcq ca cause un bug a la fin d'un playback,
                        'parce que les output comme les DOUT peuvent toggler d'un etat a l'autre
                        'If Not (ChannelCollection Is Nothing) Then
                        '    ChannelCollection.Disconnect()
                        '    ChannelCollection.Connect()
                        'End If

                        'ChannelCollection = GetObject(CELProject.EngineModel.Replace("-", "_") + "_Channels")
                        'If Not (ChannelCollection Is Nothing) Then
                        '    ChannelCollection.Disconnect()
                        '    ChannelCollection.Connect()
                        'End If

                        'Warning these flags must stay after ChannelCollection.Disconnect() and  ChannelCollection.Connect()
                        'Because of the EventArchiverThread that will start recording once the flag is ressetted
                        PlayBack_Archive_Flag = False
                        Record_Archive_Flag = False

                    End If
                End If
            Catch Ex As Exception
                MessageBox.Show("Archive_Sequence.OnArchive: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnArchive_Button(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (CELProject.IsTrue(newVal)) Then

                    CELProject.ActivateForm("Archiving", CELProject.DefaultParentWindow, True, False)

                End If

                ARCHIVE_OPEN_WINDOW_BTN.Value = Values.False
            Catch Ex As Exception
                MessageBox.Show("Archive_Sequence.OnArchive_Button: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnClose_Archive_Button(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (CELProject.IsTrue(newVal)) Then

                    CELProject.ActivateForm("Archiving", CELProject.DefaultParentWindow, False, False)

                End If

                ARCHIVE_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                MessageBox.Show("Archive_Sequence.OnClose_Archive_Button: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        'Logger Événementiel
        Private Sub EventArchiveThread()
            Dim Source As String
            Dim Destination As String

            Try
                Do While Not ExitThread
                    Thread.Sleep(25)

                    Select Case CELProject.Test_Mgr_Status.Value.ToString()
                        Case CELDAS.eTest_Mgr_Status.Running, CELDAS.eTest_Mgr_Status.ErrorOnRunning
                            If (PlayBack_Archive_Flag = True Or OPEN_ARCHIVE_FILE_BTN.Value.Equals(Values.True)) Then
                                If EVENT_ARCHIVE.Stop = False Then 'Check if already stopped
                                    EVENT_ARCHIVE.Stop = True 'If playback is running then stop recording
                                End If
                            ElseIf EVENT_ARCHIVE.Record = False And EventArchiveNeedSave = False Then 'Wait the file to be copied first, then restart Recording (See OnEventArchivePropertyChanged for file copy)
                                'TODO cette ligne a réactiver ou a changer? cause des events des channels en loop infinie?????
                                EVENT_ARCHIVE.Record = True
                            ElseIf EVENT_ARCHIVE.Record = True And EVENT_ARCHIVE.Triggered = True Then 'Wait the file to be copied first, then restart Recording (See OnEventArchivePropertyChanged for file copy)
                                EVENT_ARCHIVE_RECORD.Value = Values.True 'Update for flashing led Event Recording
                            Else
                                EVENT_ARCHIVE_RECORD.Value = Values.False 'Update for flashing led Event Recording
                            End If

                        Case CELDAS.eTest_Mgr_Status.Closing, CELDAS.eTest_Mgr_Status.Closed, CELDAS.eTest_Mgr_Status.Idling
                            If EVENT_ARCHIVE.Stop = False Then
                                EVENT_ARCHIVE.Triggered = False     'Stop Recording
                                EVENT_ARCHIVE.Stop = True           'Stop recording
                            End If
                    End Select

                    If EVENT_ARCHIVE.Value.ToString() = "Stopped" And EventArchiveNeedSave = True Then
                        'Source = "C:\My Projects\KineticaRT\Prj" + CELProject.ProjectNumber + "_HMI\PrjExt\EventArchive.csv"
                        'Source = Application.StartupPath + "\PrjExt\EventArchive.csv"

                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            Source = CELProject.Project.Rootpath + "\HMI\PrjExt\CRIT_EventArchive.csv"
                            Destination = CELProject.ArchiveDestinationPath + "CRIT_EventArchive " + Format(Now, "yyyy-MM-dd HH'h'mm'm'ss's'") + ".csv"
                        Else
                            Source = CELProject.Project.Rootpath + "\HMI\PrjExt\PERF_EventArchive.csv"
                            Destination = CELProject.ArchiveDestinationPath + "PERF_EventArchive " + Format(Now, "yyyy-MM-dd HH'h'mm'm'ss's'") + ".csv"
                        End If


                        Dim InUse As Integer = 50
                        Dim LineCount As Integer = 0
                        Do
                            Try
                                Dim sr As StreamReader = File.OpenText(Source)
                                'loop until the end
                                While Not (sr.ReadLine() Is Nothing)
                                    LineCount += 1
                                    If LineCount > 1 Then Exit While
                                End While
                                sr.Close()
                                InUse = 0
                            Catch ex As Exception
                                Thread.Sleep(50) 'Wait 50 ms before trying again
                                InUse -= 1
                                LineCount = 0
                            End Try
                        Loop While InUse > 0

                        Try
                            If LineCount > 1 Then 'Keep file with more than 1 line only
                                File.Copy(Source, Destination, True)
                            End If
                        Catch Ex As Exception
                            MessageBox.Show("Archive_Sequence.EventArchiveThread.Copy: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End Try
                        EventArchiveNeedSave = False
                    End If
                Loop
            Catch ex As Exception
                MessageBox.Show("Archive_Sequence.EventArchiveThread: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub


        Private Sub OnEventArchiveTrigValuesChanged(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If sender.Name = "EVENT_ARCHIVE_POSTTRIG" Then
                    EVENT_ARCHIVE.PostTriggerPeriod = CInt(EVENT_ARCHIVE_POSTTRIG.Value.ToDouble)
                ElseIf sender.Name = "EVENT_ARCHIVE_PRETRIG" Then
                    EVENT_ARCHIVE.PreTriggerPeriod = CInt(EVENT_ARCHIVE_PRETRIG.Value.ToDouble)
                End If
            Catch ex As Exception
                MessageBox.Show("Archive_Sequence: OnEventArchiveTrigValuesChanged: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub



        Private Sub OnEventArchivePropertyChanged(ByVal Sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)

            'Triggered  = True when  a Trigger limit has been exceeded
            If e.PropertyName = "Triggered" Then
                If EVENT_ARCHIVE.Triggered = True Then
                    EventArchiveNeedSave = True
                ElseIf EventArchiveNeedSave = True And EVENT_ARCHIVE.Stop = False Then
                    EVENT_ARCHIVE.Stop = True
                    'Do not add code after EVENT_ARCHIVE.STOP = True ==> The current thread will be aborted
                End If
            End If
        End Sub


        Protected Overrides Sub OnTestSessionClosing(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                If CELProject.EngineModel <> "SYSTEM CHECK" AndAlso CELProject.EngineModel <> "DAS CALIBRATION" Then
                    'For All Engine
                    ExitThread = True
                    Thread.Sleep(25) 'Wait to be sure that the thread execution stops.

                    'Retire cette condition, pcq ceci empechait de continuer la fonction et de retirer les HANDLERS
                    'If LastStatus <> CELDAS.eTest_Mgr_Status.Initialized Then  
                    '    Exit Sub
                    'End If

                    'For All Engine except for DAS CALIBRATION

                    Init_Channels_Value() 'Reinit variables before we quit

                    'Stops the Event Archiver (always on)
                    'Warning this must be done before we remove handlers!
                    EVENT_ARCHIVE.Stop = True
                    EVENT_ARCHIVE.Triggered = False

                    'Remove Handlers before Add Handlers, to remove duplicate handlers

                    RemoveHandlers()

                    'Kill the Event Archiver Thread if still alive
                    ThreadHelper.StopOrKillThread(EventArchiverThread, ExitThread)

                    'LastStatus = eTest_Mgr_Status.Closed
                End If
                MyBase.OnTestSessionClosing(newStatus)

            Catch ex As Exception
                MessageBox.Show("OnTestSessionClosing: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub



        Protected Overrides Sub OnTestSessionIdling(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try

                Try
                    If CELProject.EngineModel <> "SYSTEM CHECK" AndAlso CELProject.EngineModel <> "DAS CALIBRATION" Then
                        'LastStatus = eTest_Mgr_Status.Idling

                        Init_Channels_Value() 'Reinit variables before we quit

                        'Stops the Event Archiver (always on)
                        'Warning this must be done before we remove handlers!
                        EVENT_ARCHIVE.Stop = True
                        EVENT_ARCHIVE.Triggered = False

                        RemoveHandlers()
                    End If
                Catch Ex As Exception
                Finally
                End Try

                MyBase.OnTestSessionIdling(newStatus)

            Catch ex As Exception
                MessageBox.Show("OnTestSessionIdling: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub



        '******************************
        '* OnTestSessionInitialized() *
        '******************************
        Protected Overrides Sub OnTestSessionInitialized(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try

                'LastStatus = eTest_Mgr_Status.Initialized
                If CELProject.EngineModel <> "SYSTEM CHECK" AndAlso CELProject.EngineModel <> "DAS CALIBRATION" Then
                    MyBase.InitializeChannel(ENGINE_AT_REST, "ENGINE_AT_REST")

                    'MyBase.InitializeChannel(PLAY_BTN_ENB, "PLAY_BTN_ENB")
                End If


                MyBase.OnTestSessionInitialized(newStatus)

            Catch ex As Exception
                MessageBox.Show("Archive_Sequence.OnTestSessionInitialized: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionOpened(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionOpened(newStatus)

            Catch ex As Exception

                MessageBox.Show("Archive_Sequence.OnTestSessionOpened: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try
        End Sub

        Protected Overrides Sub OnTestSessionRunning(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                'If LastStatus = 0 Then ' Si logiciel plante, en reouvrant le soft, le status sera a 0 donc sort
                '    Exit Sub
                'End If
                If CELProject.EngineModel <> "SYSTEM CHECK" AndAlso CELProject.EngineModel <> "DAS CALIBRATION" Then
                    If Init_Channels() Then
                        Init_Channels_Value()

                        'TODO: Ici il faut rendre générique via une configuration
                        CELProject.ArchiveDestinationPath = "C:\CELDAS DATA\" + CELProject.EngineModel + "\" + CELProject.Test_Mgr_UUT_Answer(0).Value.ToString() + "\" + _
                            CELProject.Test_Mgr_UUT_Answer(1).Value.ToString() + "\Archive\"

                        AddHandlers()

                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            ARCHIVE.FileName = """" + CELProject.Project.Rootpath + "\HMI\PrjExt\CRIT_Archive.csv" + """"
                            EVENT_ARCHIVE.FileName = """" + CELProject.Project.Rootpath + "\HMI\PrjExt\CRIT_EventArchive.csv" + """"
                        Else
                            ARCHIVE.FileName = """" + CELProject.Project.Rootpath + "\HMI\PrjExt\PERF_Archive.csv" + """"
                            EVENT_ARCHIVE.FileName = """" + CELProject.Project.Rootpath + "\HMI\PrjExt\PERF_EventArchive.csv" + """"
                        End If

                        EVENT_ARCHIVE.Disconnect()
                        EVENT_ARCHIVE.Connect()

                        'This Thread contols the Event Archiver
                        ExitThread = False
                        EventArchiverThread = New Thread(AddressOf EventArchiveThread)
                        EventArchiverThread.Priority = ThreadPriority.Normal
                        EventArchiverThread.Name = "EventArchiverThread"
                        EventArchiverThread.Start()

                        EVENT_ARCHIVE_RECORD.Value = Values.False
                        EVENT_ARCHIVE.Stop = True
                        EVENT_ARCHIVE.Triggered = False

                        'Archive Record is Set to True Automatically on the Start Button of the Engine : See Test Session
                        'ARCHIVE_RECORD.Value = Values.True

                        ARCHIVE_PLAYSPEED.Value = New KineticaRT.Values.Double(ARCHIVE_PLAYSPEED_DEFAULT_VALUE)
                        ARCHIVE_RECORDSPEED.Value = New KineticaRT.Values.Double(ARCHIVE_RECORDSPEED_DEFAULT_VALUE)

                        EVENT_ARCHIVE_PRETRIG.Value = New KineticaRT.Values.Double(300)
                        EVENT_ARCHIVE_POSTTRIG.Value = New KineticaRT.Values.Double(60)
                        EVENT_ARCHIVE.MaxFileSize = 350 '350 Mb pour la taille maximum d'un fichier

                    End If
                End If
                MyBase.OnTestSessionRunning(newStatus)

            Catch ex As Exception
                MessageBox.Show("Archive_Sequence.OnTestSessionRunning: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub


        '********************************************************************
        '* Private Members **************************************************
        '********************************************************************

        '************
        '* Channels *
        '************
        Private EVENT_ARCHIVE As KineticaRT.Servers.Logging.CsvFile
        Private ARCHIVE As KineticaRT.Servers.Logging.CsvFile

        Private ARCHIVE_OPEN_WINDOW_BTN As KineticaRT.Channels.BooleanChannel
        Private ARCHIVE_CLOSE_WINDOW_BTN As IBooleanChannel

        Private ARCHIVE_STOP_BTN As KineticaRT.Channels.BooleanChannel
        Private ARCHIVE_PLAY As KineticaRT.Channels.BooleanChannel
        Private OPEN_ARCHIVE_FILE_BTN_ENB As KineticaRT.Channels.BooleanChannel
        Private ARCHIVE_RECORD_BTN As KineticaRT.Channels.BooleanChannel
        Private ARCHIVE_PAUSE_BTN As KineticaRT.Channels.BooleanChannel
        Private ARCHIVE_STEP_BTN As KineticaRT.Channels.BooleanChannel
        'Private EVENT_ARCHIVE_STOP_BTN As KineticaRT.Channels.BooleanChannel
        Private ENGINE_AT_REST As KineticaRT.Channels.BooleanChannel
        Private OPEN_ARCHIVE_FILE_BTN As KineticaRT.Channels.BooleanChannel

        Private ARCHIVE_RECORDING_ON_MASTER_OPC As KineticaRT.Channels.BooleanChannel
        Private ARCHIVE_RECORDING_ON_SLAVE_OPC As KineticaRT.Channels.BooleanChannel

        Private DateTime As New KineticaRT.Values.DateTime(Now)

        Private EVENT_ARCHIVE_RECORD As KineticaRT.Channels.BooleanChannel
        'Private PLAY_BTN As KineticaRT.Channels.BooleanChannel
        'Private PLAY_BTN_ENB As KineticaRT.Channels.BooleanChannel

        Private ARCHIVE_PLAYSPEED As KineticaRT.Channels.NumericChannel
        Private ARCHIVE_RECORDSPEED As KineticaRT.Channels.NumericChannel
        Private EVENT_ARCHIVE_PRETRIG As KineticaRT.Channels.NumericChannel
        Private EVENT_ARCHIVE_POSTTRIG As KineticaRT.Channels.NumericChannel

        '********
        '* Misc *
        '********
        Private ExitThread As Boolean = False
        Private LastStatus As CELDAS.eTest_Mgr_Status

        Private EventArchiveNeedSave As Boolean = False
        Private Record_Archive_Flag As Boolean = False
        Private PlayBack_Archive_Flag As Boolean = False

        Private EventArchiveEngineModel As String = ""
        Private EngineFolder As String = String.Empty
        Private EngineModel As String = ""

        '**********
        '* Thread *
        '**********
        Private EventArchiverThread As Thread

    End Class
End Namespace

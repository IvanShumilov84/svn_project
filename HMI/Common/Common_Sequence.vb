Imports System
Imports System.Collections
Imports System.Collections.Generic
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
Imports Microsoft.Office.Interop 'Excel
Imports System.Data
Imports System.Data.OleDb
Imports Microsoft.VisualBasic
Imports CEL.HMI
Imports CEL.Library

Namespace StudioDemonstration

    Public Class Common_Sequence
        Inherits StandardSequence

        'Private CELProject As CELDAS = CELDAS.Instance()

        Protected Overrides Sub OnEngineWorkingThread()

            If CELProject.IsFalse(CELProject.ARCHIVE_PLAY.Value) Then

                Select Case CELProject.Project.Number

                    Case 11813
                        OperatingTimerCtrl.Timer()
                        StarterLongCurrentTimerCtrl.Timer()
                        StarterOverCurrentTimerCtrl.Timer()

                    Case 11825
                        OperatingTimerCtrl.Timer()
                        StarterLongCurrentTimerCtrl.Timer()
                        StarterOverCurrentTimerCtrl.Timer()

                        UPSTimerCtrl.Timer()

                        OverCurrentLB1PhaseA.Timer()
                        OverCurrentLB1PhaseB.Timer()
                        OverCurrentLB1PhaseC.Timer()
                        StopWatchTimerCtrl.Timer()

                    Case 11847
                        'StarterLongCurrentTimerCtrl.Timer()
                        'StarterOverCurrentTimerCtrl.Timer()

                        'OverCurrentLB1PhaseA.Timer()
                        'OverCurrentLB1PhaseB.Timer()
                        'OverCurrentLB1PhaseC.Timer()
                        StopWatchTimerCtrl.Timer()

                    Case 11894
                        'Do Nothing

                    Case 12958
                        OperatingTimerCtrl.Timer()
                        StarterLongCurrentTimerCtrl.Timer()
                        StarterOverCurrentTimerCtrl.Timer()

                        BuzzerTimerCtrl.Timer()
                        UPSTimerCtrl.Timer()

                        OverCurrentLB1PhaseA.Timer()
                        OverCurrentLB1PhaseB.Timer()
                        OverCurrentLB1PhaseC.Timer()

                        OverCurrentLB2PhaseA.Timer()
                        OverCurrentLB2PhaseB.Timer()
                        OverCurrentLB2PhaseC.Timer()

                    Case 12917
                        OperatingTimerCtrl.Timer()
                        StarterLongCurrentTimerCtrl.Timer()
                        StarterOverCurrentTimerCtrl.Timer()

                        'Persistent Values Save Check
                        CommonPersistentValuesCtrl.SavePersistentValues(False)

                    Case 13015
                        OperatingTimerCtrl.Timer()

                        OverCurrentLB1PhaseA.Timer()
                        OverCurrentLB1PhaseB.Timer()
                        OverCurrentLB1PhaseC.Timer()
                        StopWatchTimerCtrl.Timer()

                    Case 13031
                        OperatingTimerCtrl.Timer()
                        StarterLongCurrentTimerCtrl.Timer()
                        StarterOverCurrentTimerCtrl.Timer()

                        UPSTimerCtrl.Timer()

                        OverCurrentLB1PhaseA.Timer()
                        OverCurrentLB1PhaseB.Timer()
                        OverCurrentLB1PhaseC.Timer()

                    Case 14075
                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            FacilityCtrl.TestCellArming()
                        End If

                    Case 14085
                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            UPSTimerCtrl.Timer()
                            DynoScavengePumpsTimerCtrl.Timer()
                            FlywheelScavengePumpsTimerCtrl.Timer()
                            FacilityCtrl.TestCellArming()
                            'FacilityFireTimerCtrl.Timer()
                        End If

                    Case 14086
                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            UPSTimerCtrl.Timer()
                            FacilityCtrl.TestCellArming()
                            FacilityFireTimerCtrl.Timer()
                            StopWatchTimerCtrl.Timer()

                        End If

                    Case 14094
                        OperatingTimerCtrl.Timer()

                        StarterLongCurrentTimerCtrl.Timer()
                        StarterOverCurrentTimerCtrl.Timer()

                        OverCurrentLB1PhaseA.Timer()
                        OverCurrentLB1PhaseB.Timer()
                        OverCurrentLB1PhaseC.Timer()
                        StopWatchTimerCtrl.Timer()

                    Case 14105
                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            UPSTimerCtrl.Timer()
                            E_STOP5SecDelayTimerCtrl.Timer()
                            E_STOP12SecDelayTimerCtrl.Timer()
                            DynoScavengePumpsTimerCtrl.Timer()
                            FlywheelScavengePumpsTimerCtrl.Timer()
                            FacilityCtrl.TestCellArming()
                            FacilityFireTimerCtrl.Timer()
                        End If

                    Case 15127

                        OperatingTimerCtrl.Timer()
                        StarterLongCurrentTimerCtrl.Timer()
                        StarterOverCurrentTimerCtrl.Timer()

                        UPSTimerCtrl.Timer()

                        OverCurrentLB1PhaseA.Timer()
                        OverCurrentLB1PhaseB.Timer()
                        OverCurrentLB1PhaseC.Timer()

                    Case 15208
                        OperatingTimerCtrl.Timer()
                        StopWatchTimerCtrl.Timer()

                    Case 15234
                        'Do Nothing

                    Case 17631
                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            FacilityCtrl.TestCellArming()
                        End If

                    Case 17865
                        OperatingTimerCtrl.Timer()
                        CeldasTimeRunningTimerCtrl.Timer()

                        OverCurrentLB1PhaseA.Timer()
                        OverCurrentLB1PhaseB.Timer()
                        OverCurrentLB1PhaseC.Timer()
                        OverVoltageLB1PhaseVAB.Timer()
                        OverVoltageLB1PhaseVBC.Timer()
                        OverVoltageLB1PhaseVCA.Timer()
                        StopWatchTimerCtrl.Timer()
                        StopWatch2TimerCtrl.Timer()
                        UPSTimerCtrl.Timer()

                    Case 18957
                        If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                            UPSTimerCtrl.Timer()
                            E_STOP5SecDelayTimerCtrl.Timer()
                            E_STOP12SecDelayTimerCtrl.Timer()
                            If CELProject.EngineFamily = "T700" Then
                                DynoScavengePumpsTimerCtrl.Timer()
                                FlywheelScavengePumpsTimerCtrl.Timer()
                            End If
                            FacilityCtrl.TestCellArming()
                            'FacilityFireTimerCtrl.Timer()
                        End If

                    Case Else
                        Debug.Assert(False, "Common_WorkingThread: Code Missign for this project")

                End Select

            End If

        End Sub

        Protected Overrides Sub OnTestSessionClosing(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionClosing(newStatus)

                If CELProject.EngineModel <> "DAS CALIBRATION" And CELProject.EngineModel <> "SYSTEM CHECK" Then

                    MyBase.StopWorkingThread()

                    'common to all project, anyway protected against null pointer

                    If Not (BuzzerTimerCtrl Is Nothing) Then
                        BuzzerTimerCtrl.Dispose()
                    End If

                    If Not (CeldasTimeRunningTimerCtrl Is Nothing) Then
                        CeldasTimeRunningTimerCtrl.Dispose()
                    End If

                    If Not (UnitronCtrl Is Nothing) Then
                        UnitronCtrl.Dispose()
                    End If

                    If Not (HobartCtrl Is Nothing) Then
                        HobartCtrl.Dispose()
                    End If

                    If Not (CommonPersistentValuesCtrl Is Nothing) Then
                        CommonPersistentValuesCtrl.Dispose()
                    End If

                    If Not (DynoInletCtrl Is Nothing) Then
                        DynoInletCtrl.Dispose()
                    End If

                    If Not (DynoOutletCtrl Is Nothing) Then
                        DynoOutletCtrl.Dispose()
                    End If

                    If Not (FuelRegulatorCtrl Is Nothing) Then
                        FuelRegulatorCtrl.Dispose()
                    End If

                    If Not (LargeLoadAirCtrl Is Nothing) Then
                        LargeLoadAirCtrl.Dispose()
                    End If

                    If Not (LoadAir8InchCtrl Is Nothing) Then
                        LoadAir8InchCtrl.Dispose()
                    End If

                    If Not (LoadAir8InchFastClosingCtrl Is Nothing) Then
                        LoadAir8InchFastClosingCtrl.Dispose()
                    End If

                    If Not (LoadAir3InchCtrl Is Nothing) Then
                        LoadAir3InchCtrl.Dispose()
                    End If

                    If Not (LoadAir3InchFastClosingCtrl Is Nothing) Then
                        LoadAir3InchFastClosingCtrl.Dispose()
                    End If

                    If Not (LoadAirCtrl Is Nothing) Then
                        LoadAirCtrl.Dispose()
                    End If

                    If Not (FacilityFireTimerCtrl Is Nothing) Then
                        FacilityFireTimerCtrl.Dispose()
                    End If

                    If Not (OperatingTimerCtrl Is Nothing) Then
                        OperatingTimerCtrl.Dispose()
                    End If

                    If Not (OverCurrentLB1PhaseA Is Nothing) Then
                        OverCurrentLB1PhaseA.Dispose()
                    End If

                    If Not (OverCurrentLB1PhaseB Is Nothing) Then
                        OverCurrentLB1PhaseB.Dispose()
                    End If

                    If Not (OverCurrentLB1PhaseC Is Nothing) Then
                        OverCurrentLB1PhaseC.Dispose()
                    End If

                    If Not (OverCurrentLB2PhaseA Is Nothing) Then
                        OverCurrentLB2PhaseA.Dispose()
                    End If

                    If Not (OverCurrentLB2PhaseB Is Nothing) Then
                        OverCurrentLB2PhaseB.Dispose()
                    End If

                    If Not (OverCurrentLB2PhaseC Is Nothing) Then
                        OverCurrentLB2PhaseC.Dispose()
                    End If

                    If Not (OverVoltageLB1PhaseVAB Is Nothing) Then
                        OverVoltageLB1PhaseVAB.Dispose()
                    End If

                    If Not (SmallLoadAirCtrl Is Nothing) Then
                        SmallLoadAirCtrl.Dispose()
                    End If

                    If Not (StarterLongCurrentTimerCtrl Is Nothing) Then
                        StarterLongCurrentTimerCtrl.Dispose()
                    End If

                    If Not (StarterOverCurrentTimerCtrl Is Nothing) Then
                        StarterOverCurrentTimerCtrl.Dispose()
                    End If

                    If Not (StartTimerDCDfmeaCtrl Is Nothing) Then
                        StartTimerDCDfmeaCtrl.Dispose()
                    End If

                    If Not (UPSTimerCtrl Is Nothing) Then
                        UPSTimerCtrl.Dispose()
                    End If

                    If Not (StopWatchTimerCtrl Is Nothing) Then
                        StopWatchTimerCtrl.Dispose()
                    End If

                    If Not (StopWatch2TimerCtrl Is Nothing) Then
                        StopWatch2TimerCtrl.Dispose()
                    End If

                    If Not (FacilityCtrl Is Nothing) Then
                        FacilityCtrl.Dispose()
                    End If

                    'Close the PW&C DLL Server
                    If CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then
                        If CELProject.EngineModel <> "DAS CALIBRATION" And CELProject.EngineModel <> "SYSTEM CHECK" Then
                            ClosePwcServerDllProcessing()
                        End If
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show("Common_Sequence.OnTestSessionClosing: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionOpened(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionOpened(newStatus)


                If CELProject.HierarchyType = CELDAS.eHierarchyType.Slave Then
                    If CELProject.EngineModel <> "DAS CALIBRATION" And CELProject.EngineModel <> "SYSTEM CHECK" Then
                        LaunchPwcServerDllProcessing() 'TODO ft a deplacer dans l'open test
                    End If
                End If

            Catch ex As Exception

                MessageBox.Show("Common_Sequence.OnTestSessionOpened: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try
        End Sub

        Private Sub LauchCECApplication()

            If CELProject.EngineConfiguration.LauchCECVibrationSystemApplicationWhenOpeningSession Then

                Dim programfile As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                If Environment.Is64BitOperatingSystem Then
                    programfile = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                End If
                Dim appFile = CELProject.Project.CECVibrationSystemExeFile

                If CELProject.Project.CECVibrationSystemExeFile.Contains("x86") Then
                    appFile = appFile.Replace("C:\Program Files (x86)", programfile)
                Else
                    appFile = appFile.Replace("C:\Program Files", programfile)
                End If

                If File.Exists(appFile) Then
                    If Not ProcessHelper.CheckIfRunning("CCATS") Then
                        System.Diagnostics.Process.Start(appFile)
                    Else
                        ProcessHelper.BringToFront("CCATS")
                    End If
                Else
                    Debug.Assert(False, "The file " + CELProject.Project.CECVibrationSystemExeFile + " does not exist ")
                End If

            End If

        End Sub


        Private Sub LaunchPwcServerDllProcessing() 'TODO ft a deplacer dans l'open test

            If CELProject.EngineConfiguration.PwcServerDllProcessingEnabled Then
                If File.Exists(CELProject.Project.PwcServerDllProcessingSystemExeFile) Then

                    If ProcessHelper.CheckIfRunning("PwcServer") Then

                        Dim Prc() As Process = Process.GetProcesses()

                        For Each Process As Process In Prc
                            If Process.ProcessName = "PwcServer" Then
                                Process.Kill()
                            ElseIf Process.ProcessName = "NIApplicationWebServer" Then 'Ce service entre en conflit avec le port 8080
                                Process.Kill()
                            End If
                        Next
                    End If

                    Dim PwcServerStartInfo As New ProcessStartInfo
                    Dim PwcServerExecuting As New Process

                    With PwcServerStartInfo
                        .Arguments = CELProject.EngineConfiguration.PwcServerDllProcessingSystemExeFileArgument
                        .FileName = CELProject.Project.PwcServerDllProcessingSystemExeFile
                        .Verb = "runas"
                        If Not CELProject.EngineConfiguration.PwcServerDllProcessingVisible Then
                            If CELProject.DebugMode = False Then
                                .WindowStyle = ProcessWindowStyle.Hidden
                            End If
                        End If
                    End With

                    PwcServerExecuting = Process.Start(PwcServerStartInfo)

                Else
                    Debug.Assert(False, "The file " + CELProject.Project.PwcServerDllProcessingSystemExeFile + " does not exist ")
                End If

            End If

        End Sub


        Private Sub ClosePwcServerDllProcessing()
            If CELProject.EngineConfiguration Is Nothing Then
                'Currently, Thread is going through test_session_closing twice. 
                'Once on close test and once more when the DAS is closed with the red X on the top right corner.
                'On the second close, CELProjet.Engine configuration is "Nothing" so this function causes an exception.
                'SH
                Exit Sub
            End If

            'If there is an error on opening, the configuration wont be retrieved : no need to worry has the process is launched later : will monitor if this fixes the problem.
            If Not (CELProject.EngineConfiguration.PwcServerDllProcessingEnabled.ToString Is Nothing) Then

                If CELProject.EngineConfiguration.PwcServerDllProcessingEnabled.ToString Then

                    Dim Prc() As Process = Process.GetProcesses()

                    For Each Process As Process In Prc
                        If Process.ProcessName = "PwcServer" Then
                            Process.Kill()
                        End If
                    Next

                End If
            End If


        End Sub

        Protected Overrides Sub OnTestSessionRunning(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionRunning(newStatus)

                If CELProject.EngineModel <> "DAS CALIBRATION" And CELProject.EngineModel <> "SYSTEM CHECK" Then

                    LauchCECApplication()
                    Init_Engine_Channels()
                    Init_Engine_Channels_Value()
                    AddHandlers()

                    Select Case CELProject.Project.Number

                        Case 11813
                            'Configuring Load Air Valve
                            LoadAirCtrl = New UpDownDemandController("LOAD_AIR_VALVE_DEMAND", "LOAD_AIR_VALVE_DEMAND_INCREMENT", "LOAD_AIR_VALVE_UP_DEMAND_BTN", "LOAD_AIR_VALVE_DOWN_DEMAND_BTN", Nothing)
                            LoadAirCtrl.Maximum = 100
                            LoadAirCtrl.Increment = 0.5
                            LoadAirCtrl.Minimum = 0
                            LoadAirCtrl.ResetValue = 0

                            OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", Nothing, 0, "NONE", True)

                            StarterLongCurrentTimerCtrl = New TimerController(Me, "STARTER_LONGCURRENT_START_CONDITION", "STARTER_LONGCURRENT_STOP_CONDITION", "STARTER_LONGCURRENT_CONDITION_RESET", "STARTER_LONGCURRENT_TIME_SEC", Nothing, 60, "STARTER_LONGCURRENT_MAX_VALUE_REACHED")
                            StarterOverCurrentTimerCtrl = New TimerController(Me, "STARTER_OVERCURRENT_START_CONDITION", "STARTER_OVERCURRENT_STOP_CONDITION", "STARTER_OVERCURRENT_CONDITION_RESET", "STARTER_OVERCURRENT_TIME_SEC", Nothing, 10, "STARTER_OVERCURRENT_MAX_VALUE_REACHED")

                        Case 11825 'Ethiopie
                            'Configuring large (8 inch) Load Air Valve Reset after fast closing
                            LoadAir3InchFastClosingCtrl = New PreviousValueReturnController("LOAD_AIR_VALVE_3INCH_DEMAND", "RAPID_CLOSING_BTN")

                            'Configuring Small (3 INCH) Load Air Valve
                            LoadAir3InchCtrl = New UpDownDemandController("LOAD_AIR_VALVE_3INCH_DEMAND", "LOAD_AIR_VALVE_3INCH_DEMAND_INCREMENT", "LOAD_AIR_VALVE_3INCH_UP_DEMAND_BTN", "LOAD_AIR_VALVE_3INCH_DOWN_DEMAND_BTN", Nothing)
                            LoadAir3InchCtrl.Maximum = 100
                            LoadAir3InchCtrl.Increment = 0.5
                            LoadAir3InchCtrl.Minimum = 0
                            LoadAir3InchCtrl.ResetValue = 0

                            OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", "OPERATING_TIME", 0, "NONE", True)

                            'Configuring large (8 inch) Load Air Valve Reset after fast closing
                            LoadAir8InchFastClosingCtrl = New PreviousValueReturnController("LOAD_AIR_VALVE_8INCH_DEMAND", "RAPID_CLOSING_BTN")

                            'Configuring large (8 inch) Load Air Valve
                            LoadAir8InchCtrl = New UpDownDemandController("LOAD_AIR_VALVE_8INCH_DEMAND", "LOAD_AIR_VALVE_8INCH_DEMAND_INCREMENT", "LOAD_AIR_VALVE_8INCH_UP_DEMAND_BTN", "LOAD_AIR_VALVE_8INCH_DOWN_DEMAND_BTN", Nothing)
                            LoadAir8InchCtrl.Maximum = 100
                            LoadAir8InchCtrl.Increment = 0.5
                            LoadAir8InchCtrl.Minimum = 0
                            LoadAir8InchCtrl.ResetValue = 0

                            StarterLongCurrentTimerCtrl = New TimerController(Me, "STARTER_LONGCURRENT_START_CONDITION", "STARTER_LONGCURRENT_STOP_CONDITION", "STARTER_LONGCURRENT_CONDITION_RESET", "STARTER_LONGCURRENT_TIME_SEC", Nothing, 60, "STARTER_LONGCURRENT_MAX_VALUE_REACHED")
                            StarterOverCurrentTimerCtrl = New TimerController(Me, "STARTER_OVERCURRENT_START_CONDITION", "STARTER_OVERCURRENT_STOP_CONDITION", "STARTER_OVERCURRENT_CONDITION_RESET", "STARTER_OVERCURRENT_TIME_SEC", Nothing, 10, "STARTER_OVERCURRENT_MAX_VALUE_REACHED")


                            UPSTimerCtrl = New TimerController(Me, "UPS_ON_BATTERY_START_CONDITION", Nothing, "UPS_ON_BATTERY_RESET_CONDITION", Nothing, Nothing, 60, "UPS_ON_BATTERY_MAX_TIME_REACHED")

                            OverCurrentLB1PhaseA = New TimerController(Me, "LB1_IA_OVERCURRENT", Nothing, "LB1_IA_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IA_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseB = New TimerController(Me, "LB1_IB_OVERCURRENT", Nothing, "LB1_IB_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IB_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseC = New TimerController(Me, "LB1_IC_OVERCURRENT", Nothing, "LB1_IC_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IC_OVERCURRENT_REACHED")

                            'Configuring Fuel Pressure Regulator Valve
                            FuelRegulatorCtrl = New UpDownDemandController("FUEL_PRESSURE_DEMAND", "FUEL_PRESSURE_DEMAND_INCREMENT", "FUEL_PRESSURE_UP_DEMAND_BTN", "FUEL_PRESSURE_DOWN_DEMAND_BTN", Nothing)
                            FuelRegulatorCtrl.Maximum = 100
                            FuelRegulatorCtrl.Increment = 1
                            FuelRegulatorCtrl.Minimum = 0
                            FuelRegulatorCtrl.ResetValue = 0

                            'Stopwatch Timer
                            StopWatchTimerCtrl = New TimerController(Me, "STOPWATCH_TIME_START_CONDITION", "STOPWATCH_TIME_STOP_CONDITION", "STOPWATCH_TIME_RESET_CONDITION", "STOPWATCH_TIME_SEC", "STOPWATCH_TIME", Nothing, Nothing, False, Nothing)

                        Case 11847
                            'OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", Nothing, 0, "NONE", True)

                            ''Configuring large (8 inch) Load Air Valve Reset after fast closing
                            'LoadAir8InchFastClosingCtrl = New PreviousValueReturnController("LOAD_AIR_VALVE_DEMAND", "RAPID_CLOSING_BTN")

                            ''Configuring large (8 inch) Load Air Valve
                            'LoadAir8InchCtrl = New UpDownDemandController("LOAD_AIR_VALVE_DEMAND", "LOAD_AIR_VALVE_DEMAND_INCREMENT", "LOAD_AIR_VALVE_UP_DEMAND_BTN", "LOAD_AIR_VALVE_DOWN_DEMAND_BTN", Nothing)
                            'LoadAir8InchCtrl.Maximum = 100
                            'LoadAir8InchCtrl.Increment = 0.5
                            'LoadAir8InchCtrl.Minimum = 0
                            'LoadAir8InchCtrl.ResetValue = 0

                            'Stopwatch
                            StopWatchTimerCtrl = New TimerController(Me, "STOPWATCH_START_CONDITION", "STOPWATCH_STOP_CONDITION", "STOPWATCH_RESET_CONDITION", "STOPWATCH_TIME_SEC", "STOPWATCH_TIME", 0, Nothing, False)

                        Case 12917
                            Dim folderName As String = "C:\Common"

                            If Directory.Exists(folderName) = False Then
                                Directory.CreateDirectory(folderName)
                            End If

                            Dim PersistentValuesChannels = {"FUEL_PRESSURE_DEMAND", "DYNO_WATER_INLET_VALVE_DEMAND"}
                            Dim PersistentValuesFileName = "C:\Common\PersistentValues.xml"

                            CommonPersistentValuesCtrl = New PersistentValuesController(Me, PersistentValuesChannels, 1, PersistentValuesFileName)

                            'Configuring Dyno Water Intlet Valve
                            DynoInletCtrl = New UpDownDemandController("DYNO_WATER_INLET_VALVE_DEMAND", "DYNO_WATER_INLET_VALVE_DEMAND_INCREMENT", "DYNO_WATER_INLET_VALVE_UP_DEMAND_BTN", "DYNO_WATER_INLET_VALVE_DOWN_DEMAND_BTN", Nothing)
                            DynoInletCtrl.Maximum = 100
                            DynoInletCtrl.Increment = 0.5
                            DynoInletCtrl.Minimum = 0
                            DynoInletCtrl.ResetValue = 0

                            'Configuring Dyno Water Outlet Valve
                            Select Case CELProject.EngineModel
                                Case "PW901A", "PW980A"
                                    DynoOutletCtrl = New UpDownDemandController("DYNO_WATER_OUTLET_VALVE_DEMAND", "DYNO_WATER_OUTLET_VALVE_DEMAND_INCREMENT", "DYNO_WATER_OUTLET_VALVE_UP_DEMAND_BTN", "DYNO_WATER_OUTLET_VALVE_DOWN_DEMAND_BTN", "DYNO_WATER_OUTLET_VALVE_RESET_PW900")
                                Case Else
                                    DynoOutletCtrl = New UpDownDemandController("DYNO_WATER_OUTLET_VALVE_DEMAND", "DYNO_WATER_OUTLET_VALVE_DEMAND_INCREMENT", "DYNO_WATER_OUTLET_VALVE_UP_DEMAND_BTN", "DYNO_WATER_OUTLET_VALVE_DOWN_DEMAND_BTN", "DYNO_WATER_OUTLET_VALVE_RESET")
                            End Select
                            DynoOutletCtrl.Maximum = 100
                            DynoOutletCtrl.Increment = 0.5
                            DynoOutletCtrl.Minimum = 0
                            DynoOutletCtrl.ResetValue = 100

                            'Configuring Fuel Pressure Regulator Valve
                            FuelRegulatorCtrl = New UpDownDemandController("FUEL_PRESSURE_DEMAND", "FUEL_PRESSURE_DEMAND_INCREMENT", "FUEL_PRESSURE_UP_DEMAND_BTN", "FUEL_PRESSURE_DOWN_DEMAND_BTN", Nothing)
                            FuelRegulatorCtrl.Maximum = 100
                            FuelRegulatorCtrl.Increment = 1
                            FuelRegulatorCtrl.Minimum = 0
                            FuelRegulatorCtrl.ResetValue = 0

                            'Configuring Load Air Valve
                            LoadAirCtrl = New UpDownDemandController("LOAD_AIR_VALVE_DEMAND", "LOAD_AIR_VALVE_DEMAND_INCREMENT", "LOAD_AIR_VALVE_UP_DEMAND_BTN", "LOAD_AIR_VALVE_DOWN_DEMAND_BTN", Nothing)
                            LoadAirCtrl.Maximum = 100
                            LoadAirCtrl.Increment = 0.5
                            LoadAirCtrl.Minimum = 0
                            LoadAirCtrl.ResetValue = 0

                            OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", Nothing, 0, "NONE", True)

                            'Configuring Load Air Valve
                            SmallLoadAirCtrl = New UpDownDemandController("LOAD_AIR_SMALL_VALVE_DEMAND", "LOAD_AIR_SMALL_VALVE_DEMAND_INCREMENT", "LOAD_AIR_SMALL_VALVE_UP_DEMAND_BTN", "LOAD_AIR_SMALL_VALVE_DOWN_DEMAND_BTN", Nothing)
                            SmallLoadAirCtrl.Maximum = 100
                            SmallLoadAirCtrl.Increment = 0.5
                            SmallLoadAirCtrl.Minimum = 0
                            SmallLoadAirCtrl.ResetValue = 0

                            StarterLongCurrentTimerCtrl = New TimerController(Me, "STARTER_LONGCURRENT_START_CONDITION", "STARTER_LONGCURRENT_STOP_CONDITION", "STARTER_LONGCURRENT_CONDITION_RESET", "STARTER_LONGCURRENT_TIME_SEC", Nothing, 30, "STARTER_LONGCURRENT_MAX_VALUE_REACHED")
                            StarterOverCurrentTimerCtrl = New TimerController(Me, "STARTER_OVERCURRENT_START_CONDITION", "STARTER_OVERCURRENT_STOP_CONDITION", "STARTER_OVERCURRENT_CONDITION_RESET", "STARTER_OVERCURRENT_TIME_SEC", Nothing, 10, "STARTER_OVERCURRENT_MAX_VALUE_REACHED")

                        Case 12958 'EPCOR
                            'Configuring Load Air Valve
                            LoadAirCtrl = New UpDownDemandController("LOAD_AIR_VALVE_DEMAND", "LOAD_AIR_VALVE_DEMAND_INCREMENT", "LOAD_AIR_VALVE_UP_DEMAND_BTN", "LOAD_AIR_VALVE_DOWN_DEMAND_BTN", Nothing)
                            LoadAirCtrl.Maximum = 100
                            LoadAirCtrl.Increment = 0.5
                            LoadAirCtrl.Minimum = 0
                            LoadAirCtrl.ResetValue = 0

                            OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", Nothing, 0, "NONE", True)

                            'Configuring Load Air Valve
                            LargeLoadAirCtrl = New UpDownDemandController("LOAD_AIR_LARGE_VALVE_DEMAND", "LOAD_AIR_LARGE_VALVE_DEMAND_INCREMENT", "LOAD_AIR_LARGE_VALVE_UP_DEMAND_BTN", "LOAD_AIR_LARGE_VALVE_DOWN_DEMAND_BTN", Nothing)
                            LargeLoadAirCtrl.Maximum = 100
                            LargeLoadAirCtrl.Increment = 0.5
                            LargeLoadAirCtrl.Minimum = 0
                            LargeLoadAirCtrl.ResetValue = 0

                            StarterLongCurrentTimerCtrl = New TimerController(Me, "STARTER_LONGCURRENT_START_CONDITION", "STARTER_LONGCURRENT_STOP_CONDITION", "STARTER_LONGCURRENT_CONDITION_RESET", "STARTER_LONGCURRENT_TIME_SEC", Nothing, 60, "STARTER_LONGCURRENT_MAX_VALUE_REACHED")
                            StarterOverCurrentTimerCtrl = New TimerController(Me, "STARTER_OVERCURRENT_START_CONDITION", "STARTER_OVERCURRENT_STOP_CONDITION", "STARTER_OVERCURRENT_CONDITION_RESET", "STARTER_OVERCURRENT_TIME_SEC", Nothing, 10, "STARTER_OVERCURRENT_MAX_VALUE_REACHED")


                            BuzzerTimerCtrl = New TimerController(Me, "BUZZER_BTN", Nothing, "BUZZER_TIMER_RESET_LOGIC", Nothing, 15, "BUZZER_TIMER_VALUE_REACH", Nothing, "Engine is now ready to START")

                            UPSTimerCtrl = New TimerController(Me, "UPS_ON_BATTERY_START_CONDITION", Nothing, "UPS_ON_BATTERY_RESET_CONDITION", Nothing, 60, "UPS_ON_BATTERY_MAX_TIME_REACHED")

                            OverCurrentLB1PhaseA = New TimerController(Me, "LB1_IA_OVERCURRENT", Nothing, "LB1_IA_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IA_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseB = New TimerController(Me, "LB1_IB_OVERCURRENT", Nothing, "LB1_IB_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IB_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseC = New TimerController(Me, "LB1_IC_OVERCURRENT", Nothing, "LB1_IC_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IC_OVERCURRENT_REACHED")

                            OverCurrentLB2PhaseA = New TimerController(Me, "LB2_IA_OVERCURRENT", Nothing, "LB2_IA_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB2_IA_OVERCURRENT_REACHED")
                            OverCurrentLB2PhaseB = New TimerController(Me, "LB2_IB_OVERCURRENT", Nothing, "LB2_IB_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB2_IB_OVERCURRENT_REACHED")
                            OverCurrentLB2PhaseC = New TimerController(Me, "LB2_IC_OVERCURRENT", Nothing, "LB2_IC_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB2_IC_OVERCURRENT_REACHED")

                        Case 13015 'Turquie THY
                            OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", Nothing, 0, "NONE", True)

                            'Configuring large (8 inch) Load Air Valve Reset after fast closing
                            LoadAir8InchFastClosingCtrl = New PreviousValueReturnController("LOAD_AIR_VALVE_DEMAND", "RAPID_CLOSING_BTN")

                            'Configuring large (8 inch) Load Air Valve
                            LoadAir8InchCtrl = New UpDownDemandController("LOAD_AIR_VALVE_DEMAND", "LOAD_AIR_VALVE_DEMAND_INCREMENT", "LOAD_AIR_VALVE_UP_DEMAND_BTN", "LOAD_AIR_VALVE_DOWN_DEMAND_BTN", Nothing)
                            LoadAir8InchCtrl.Maximum = 100
                            LoadAir8InchCtrl.Increment = 0.5
                            LoadAir8InchCtrl.Minimum = 0
                            LoadAir8InchCtrl.ResetValue = 0

                            HobartCtrl = New HobartController(Me, "STARTER_RECTIFIER_READY_28_VDC_IND", "STARTER_RECTIFIER_MODBUS_STATE", "STARTER_RECTIFIER_MODBUS_FAULT",
                                                                  "STARTER_RECTIFIER_VOLTAGE_VDC", "STARTER_RECTIFIER_CURRENT_AMP", "STARTER_RECTIFIER_POWER_KW", "STARTER_RECTIFIER_CURRENT_LIMIT_AMP", "STARTER_RECTIFIER_RESET_BTN",
                                                                  "STARTER_CONTACTOR1_REQUEST", "STARTER_CONTACTOR2_REQUEST", "STARTER_CONTACTOR1_CMD", "STARTER_CONTACTOR2_CMD",
                                                                  "STARTER_CONTACTOR1_DC_ON_IND", "STARTER_CONTACTOR2_DC_ON_IND", "STARTER_CONTACTOR1_MODBUS_STATE", "STARTER_CONTACTOR2_MODBUS_STATE")

                            OverCurrentLB1PhaseA = New TimerController(Me, "LB1_IA_OVERCURRENT", Nothing, "LB1_IA_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IA_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseB = New TimerController(Me, "LB1_IB_OVERCURRENT", Nothing, "LB1_IB_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IB_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseC = New TimerController(Me, "LB1_IC_OVERCURRENT", Nothing, "LB1_IC_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IC_OVERCURRENT_REACHED")

                            'Stopwatch
                            StopWatchTimerCtrl = New TimerController(Me, "STOPWATCH_START_CONDITION", "STOPWATCH_STOP_CONDITION", "STOPWATCH_RESET_CONDITION", "STOPWATCH_TIME_SEC", "STOPWATCH_TIME", 0, Nothing, False)

                        Case 13031
                            DisplayCustomMessage()

                            'Configuring Load Air Valve
                            LoadAirCtrl = New UpDownDemandController("LOAD_AIR_VALVE_DEMAND", "LOAD_AIR_VALVE_DEMAND_INCREMENT", "LOAD_AIR_VALVE_UP_DEMAND_BTN", "LOAD_AIR_VALVE_DOWN_DEMAND_BTN", Nothing)
                            LoadAirCtrl.Maximum = 100
                            LoadAirCtrl.Increment = 0.5
                            LoadAirCtrl.Minimum = 0
                            LoadAirCtrl.ResetValue = 0

                            OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", Nothing, 0, "NONE", True)

                            StarterLongCurrentTimerCtrl = New TimerController(Me, "STARTER_LONGCURRENT_START_CONDITION", "STARTER_LONGCURRENT_STOP_CONDITION", "STARTER_LONGCURRENT_CONDITION_RESET", "STARTER_LONGCURRENT_TIME_SEC", Nothing, 60, "STARTER_LONGCURRENT_MAX_VALUE_REACHED")
                            StarterOverCurrentTimerCtrl = New TimerController(Me, "STARTER_OVERCURRENT_START_CONDITION", "STARTER_OVERCURRENT_STOP_CONDITION", "STARTER_OVERCURRENT_CONDITION_RESET", "STARTER_OVERCURRENT_TIME_SEC", Nothing, 10, "STARTER_OVERCURRENT_MAX_VALUE_REACHED")

                            UPSTimerCtrl = New TimerController(Me, "UPS_ON_BATTERY_START_CONDITION", Nothing, "UPS_ON_BATTERY_RESET_CONDITION", Nothing, Nothing, 60, "UPS_ON_BATTERY_MAX_TIME_REACHED")

                            OverCurrentLB1PhaseA = New TimerController(Me, "LB1_IA_OVERCURRENT", Nothing, "LB1_IA_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IA_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseB = New TimerController(Me, "LB1_IB_OVERCURRENT", Nothing, "LB1_IB_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IB_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseC = New TimerController(Me, "LB1_IC_OVERCURRENT", Nothing, "LB1_IC_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IC_OVERCURRENT_REACHED")

                        Case 14075
                            If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

                                'If a Fire occurs, we wait 5 seconds before resetting TC Arming : this is for the louver to extract fumes as much as possible outside the test cell
                                'and maximize argon concentration
                                'DISABLED AT THE MOMENT AS THE CUSTOMER'S FIRE BEHAVIOR IS UNCLEAR.

                                'FacilityFireTimerCtrl = New TimerController(Me, "FACILITY_FIRE_TIME_START_CONDITION_LATCH", "FACILITY_FIRE_TIME_STOP_CONDITION_LATCH", "FACILITY_FIRE_TIME_CONDITION_RESET", "FACILITY_FIRE_TIME_SEC", Nothing, 5, "FACILITY_FIRE_TIME_MAX_VALUE_REACHED")

                                UnitronCtrl = New UnitronController(Me, "STARTER_RECTIFIER_BTN", "STARTER_RECTIFIER_STATUS_IND", "STARTER_RECTIFIER_OVERLOAD_IND", "CRANK_BTN", "STARTER_RECTIFIER_CONTACTOR_FEEDBACK_IND", "STARTER_VOLTAGE_SOURCE_OPC", "STARTER_CURRENT_SOURCE_OPC", Nothing, Nothing)

                                Dim FacilityItems As New List(Of CEL.HMI.FacilityController.FacilityItemsList)

                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_FAN_BTN", "COOLING_TOWER_FAN_STATUS_IND", 3.0, "COOLING_TOWER_FAN_FAULT_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_PUMP_BTN", "COOLING_TOWER_PUMP_STATUS_IND", 3.0, "COOLING_TOWER_PUMP_FAULT_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("OIL_COOLING_WATER_SUPPLY_BTN", "OIL_COOLING_WATER_SUPPLY_STATUS_IND", 3.0, "OIL_COOLING_WATER_SUPPLY_FAULT_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("HYDRAULIC_PWR_PACK_SUPPLY_BTN", "HYDRAULIC_PWR_PACK_SUPPLY_STATUS_IND", 3.0, "HYDRAULIC_PWR_PACK_SUPPLY_FAULT_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_WATER_SUPPLY_PUMP_BTN", "DYNO_WATER_SUPPLY_PUMP_STATUS_IND", 0.0, "DYNO_WATER_SUPPLY_PUMP_FAULT_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("STARTER_RECTIFIER_BTN", "STARTER_RECTIFIER_STATUS_IND", 4.0, "STARTER_RECTIFIER_OVERLOAD_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("LOUVER_INTAKE_BTN", "LOUVER_INTAKE_AUX_IND", 10.0, Nothing))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("LOUVER_EXHAUST_BTN", "LOUVER_EXHAUST_AUX_IND", 20.0, Nothing))

                                FacilityCtrl = New FacilityController(Me, "TC_ARMING_BTN", FacilityItems, False, False, "The dynamometer must be error free with the inlet valve at or below 2% to activate the dynamometer water supply pump")

                            End If

                        Case 14085
                            If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then
                                ' OIL_BACK_PRESSURE_REGULATOR_DEMAND need to be persistent for this project
                                Dim PersistentValuesChannels = {"OIL_BACK_PRESSURE_REGULATOR_DEMAND"}
                                Dim PersistentValuesFileName = "C:\CELDAS DATA\Common\PersistentValues.xml"
                                CommonPersistentValuesCtrl = New PersistentValuesController(Me, PersistentValuesChannels, 1, PersistentValuesFileName)

                                'TIMER DISABLED FOR THE MOMENT
                                'If a Fire occurs, we wait 5 seconds before resetting TC Arming : this is for the louver to extract fumes as much as possible outside the test cell
                                'and maximize argon concentration
                                'FacilityFireTimerCtrl = New TimerController(Me, "FACILITY_FIRE_TIME_START_CONDITION_LATCH", "FACILITY_FIRE_TIME_STOP_CONDITION_LATCH", "FACILITY_FIRE_TIME_CONDITION_RESET", "FACILITY_FIRE_TIME_SEC", Nothing, 5, "FACILITY_FIRE_TIME_MAX_VALUE_REACHED")

                                'If the UPS system is activated, after 5 seconds, a signal is will activate the PMS Switch from "FLY" to "IDLE"
                                'A "Return to IDLE" functionnality also needs to be implemented in case the operator goes back to "FLY" without having moved the lever, resulting in a slam accel.
                                UPSTimerCtrl = New TimerController(Me, "UPS_ON_BATTERY_START_CONDITION", Nothing, "UPS_ON_BATTERY_RESET_CONDITION", Nothing, Nothing, 5, "UPS_ON_BATTERY_MAX_TIME_REACHED")

                                'Dyno and Flywheel Scavenge Pumps are deactivated after 5 seconds we have obtained the associated low pressure switch signal
                                DynoScavengePumpsTimerCtrl = New TimerController(Me, "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_START_CONDITION", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_STOP_CONDITION", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_CONDITION_RESET", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_TIME_SEC", Nothing, 30, "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_MAX_VALUE_REACHED")
                                FlywheelScavengePumpsTimerCtrl = New TimerController(Me, "FLYWHEEL_POWER_PACK_SCAV_PUMPS_START_CONDITION", "FLYWHEEL_POWER_PACK_SCAV_PUMPS_STOP_CONDITION", "FLYWHEEL_POWER_PACK_SCAV_PUMPS_CONDITION_RESET", "FLYWHEEL_POWER_PACK_SCAV_PUMPS_TIME_SEC", Nothing, 30, "FLYWHEEL_POWER_PACK_SCAV_PUMPS_MAX_VALUE_REACHED")

                                'Scavenge Pumps Feedbacks are monitored but not activated by the Test Cell arming (this is done by putting 0 as feedback delay)
                                'Scavenge Pumps are activated when the power pack is activated ; Scavenge Pumps are deactivated only when the low pressure switch signal confirms
                                'that the hydraulic fluid has been recycled. If the Scavenge pumps are deactivated too early, an overflow of hydraulic fluid will occur on the test bed.

                                Dim FacilityItems As New List(Of CEL.HMI.FacilityController.FacilityItemsList)

                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_FAN_BTN", "COOLING_TOWER_FAN_AUX_IND", 3.0, "COOLING_TOWER_FAN_OVERLOAD_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_PUMP_BTN", "COOLING_TOWER_PUMP_AUX_IND", 3.0, "COOLING_TOWER_PUMP_OVERLOAD_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("CHILLER_AND_PUMP_CONTROL_BTN", "CHILLER_AND_PUMP_CONTROL_AUX_IND", 3.0, Nothing))

                                'HS150 Drivetrain : PT6C, PW210A
                                'F359 Drivetrain : PW200, PW210S

                                If CELProject.EngineFamily = "PT6C" Or CELProject.EngineModel = "PW210A" Then

                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_HYDRAULIC_POWER_PACK_SUPPLY_HS150_BTN", "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_HS150_AUX_IND", 3.0, "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_HS150_OVERLOAD_IND"))
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMP1_HS150_BTN", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMP1_HS150_AUX_IND", 0.0, "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMP1_HS150_OVERLOAD_IND"))
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMP2_HS150_BTN", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMP2_HS150_AUX_IND", 0.0, "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMP2_HS150_OVERLOAD_IND"))

                                ElseIf CELProject.EngineFamily = "PW200" Or CELProject.EngineModel = "PW210S" Then

                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_HYDRAULIC_POWER_PACK_SUPPLY_F359_BTN", "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_F359_AUX_IND", 3.0, "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_F359_OVERLOAD_IND"))
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_OIL_MIST_AIR_PRESSURE_SWITCH_BTN", "DYNO_OIL_MIST_AIR_PRESSURE_SWITCH_AUX_IND", 1.0, Nothing))
                                Else
                                    Debug.Assert(False, "Common_Sequence.OnTestSessionRunning: Code Missing for this project")
                                End If

                                If CELProject.EngineFamily = "PT6C" Or CELProject.EngineModel = "PW210A" Then

                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("TQMETER_LUBE_SYSTEM_HEATER_BTN", "TQMETER_LUBE_SYSTEM_HEATER_AUX_IND", 3.0, "TQMETER_LUBE_SYSTEM_HEATER_OVERLOAD_IND"))
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("TQMETER_LUBE_SYSTEM_AIR_SUPPLY_BTN", "TQMETER_LUBE_SYSTEM_AIR_SUPPLY_AUX_IND", 3.0, "TQMETER_LUBE_SYSTEM_AIR_SUPPLY_OVERLOAD_IND"))
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("TQMETER_LUBE_SYSTEM_PUMP_BTN", "TQMETER_LUBE_SYSTEM_PUMP_AUX_IND", 3.0, "TQMETER_LUBE_SYSTEM_PUMP_OVERLOAD_IND"))


                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_BTN", "FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_AUX_IND", 3.0, "FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_OVERLOAD_IND"))
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("FLYWHEEL_POWER_PACK_SCAV_PUMP1_BTN", "FLYWHEEL_POWER_PACK_SCAV_PUMP1_AUX_IND", 0.0, "FLYWHEEL_POWER_PACK_SCAV_PUMP1_OVERLOAD_IND"))
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("FLYWHEEL_POWER_PACK_SCAV_PUMP2_BTN", "FLYWHEEL_POWER_PACK_SCAV_PUMP2_AUX_IND", 0.0, "FLYWHEEL_POWER_PACK_SCAV_PUMP2_OVERLOAD_IND"))
                                End If

                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("LOUVER_INTAKE_BTN", "LOUVER_INTAKE_AUX_IND", 80.0, Nothing))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("LOUVER_EXHAUST_BTN", "LOUVER_EXHAUST_AUX_IND", 85.0, Nothing))

                                If CELProject.EngineFamily = "PW200" Then
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("OIL_COOLING_AIR_ENABLE_BTN", "OIL_COOLING_AIR_ENABLE_AUX_IND", 1.0, Nothing))
                                End If

                                'Dyno Water Supply Cold Circuit Feedback set to zero to be monitored but not included in ARMING : Can only be activated when Inlet Valve of Dyno is Zero.
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_WATER_SUPPLY_PUMP_COLD_CIRCUIT_BTN", "DYNO_WATER_SUPPLY_PUMP_COLD_CIRCUIT_AUX_IND", 0.0, "DYNO_WATER_SUPPLY_PUMP_COLD_CIRCUIT_OVERLOAD_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_WATER_SUPPLY_PUMP_HOT_CIRCUIT_BTN", "DYNO_WATER_SUPPLY_PUMP_HOT_CIRCUIT_AUX_IND", 3.0, "DYNO_WATER_SUPPLY_PUMP_HOT_CIRCUIT_OVERLOAD_IND"))

                                'Fuel Tank has two Pumps : we determine randomly which we will be using for the TC Arming.
                                'Dim RandomClass As New Random()
                                'Dim FuelPumpRandomSelection As Integer = RandomClass.Next(1, 3)

                                'If FuelPumpRandomSelection = 1 Then
                                '    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("FUEL_SUPPLY_PUMP1_BTN", "FUEL_SUPPLY_PUMP1_AUX_IND", 3.0, Nothing))
                                'ElseIf FuelPumpRandomSelection = 2 Then
                                '    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("FUEL_SUPPLY_PUMP2_BTN", "FUEL_SUPPLY_PUMP2_AUX_IND", 3.0, Nothing))
                                'Else
                                '    Debug.Assert(False, "Common_Sequence.OnTestSessionRunning: .NET Framework Unhandled Random Class Number Generation : No Fuel Pump Managed by TC Arming")
                                'End If

                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("STARTER_RECTIFIER_BTN", "STARTER_RECTIFIER_CONTACTOR_READY_IND", 3.0, "STARTER_RECTIFIER_OVERLOAD_IND"))

                                If CELProject.EngineFamily = "PT6C" Or CELProject.EngineModel = "PW210A" Then

                                    FacilityCtrl = New FacilityController(Me, "TC_ARMING_BTN", FacilityItems, False, False, "")

                                ElseIf CELProject.EngineFamily = "PW200" Or CELProject.EngineModel = "PW210S" Then

                                    FacilityCtrl = New FacilityController(Me, "TC_ARMING_BTN", FacilityItems, False, False, "Make sure you wait 5 minutes for Dyno Oil Mist")
                                Else
                                    Debug.Assert(False, "Common_Sequence.OnTestSessionRunning: Code Missing for this project")
                                End If
                            End If

                        Case 14086

                            If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

                                'If a Fire occurs, we wait 5 seconds before resetting TC Arming : this is for the louver to extract fumes as much as possible outside the test cell
                                'and maximize argon concentration
                                FacilityFireTimerCtrl = New TimerController(Me, "FACILITY_FIRE_TIME_START_CONDITION_LATCH", "FACILITY_FIRE_TIME_STOP_CONDITION_LATCH", "FACILITY_FIRE_TIME_CONDITION_RESET", "FACILITY_FIRE_TIME_SEC", Nothing, 5, "FACILITY_FIRE_TIME_MAX_VALUE_REACHED")

                                'If the UPS system is activated, after 5 seconds, a signal is will be sent to the throttle controller (cRIO) to "return to idle"
                                UPSTimerCtrl = New TimerController(Me, "UPS_ON_BATTERY_START_CONDITION", Nothing, "UPS_ON_BATTERY_RESET_CONDITION", Nothing, Nothing, 5, "UPS_ON_BATTERY_MAX_TIME_REACHED")

                                'Stopwatch
                                StopWatchTimerCtrl = New TimerController(Me, "STOPWATCH_START_CONDITION", "STOPWATCH_STOP_CONDITION", "STOPWATCH_RESET_CONDITION", "STOPWATCH_TIME_SEC", "STOPWATCH_TIME", 0, Nothing, False)

                                Dim FacilityItems As New List(Of CEL.HMI.FacilityController.FacilityItemsList)

                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_FAN_BTN", "COOLING_TOWER_FAN_STATUS_IND", 3.0, "COOLING_TOWER_FAN_FAULT_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_PUMP_BTN", "COOLING_TOWER_PUMP_STATUS_IND", 3.0, "COOLING_TOWER_PUMP_FAULT_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_WATER_SUPPLY_PUMP_BTN", "DYNO_WATER_SUPPLY_PUMP_STATUS_IND", 3.0, "DYNO_WATER_SUPPLY_PUMP_FAULT_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("LOUVER_INTAKE_BTN", "LOUVER_INTAKE_AUX_IND", 90.0, Nothing))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("LOUVER_EXHAUST_BTN", "LOUVER_EXHAUST_AUX_IND", 90.0, Nothing))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("SHOP_AIR_TO_OIL_SKID_BTN", "SHOP_AIR_PRESSURE_SWITCH_IND", 4.0, Nothing))
                                If CELProject.EngineFamily = "PT6A" Then
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("SHOP_AIR_TO_AIR_CYLINDER_BTN", "SHOP_AIR_PRESSURE_SWITCH_IND", 4.0, Nothing))
                                End If
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_HYDRAULIC_POWER_PACK_SUPPLY_F47_BTN", "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_F47_STATUS_IND", 3.0, "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_F47_FAULT_IND"))

                                FacilityCtrl = New FacilityController(Me, "TC_ARMING_BTN", FacilityItems, False, False, "")

                            End If

                        Case 14094 'Sichuan
                            OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", Nothing, 0, "NONE", True)

                            'Configuring large (8 inch) Load Air Valve Reset after fast closing
                            LoadAir8InchFastClosingCtrl = New PreviousValueReturnController("LOAD_AIR_VALVE_DEMAND", "RAPID_CLOSING_BTN")

                            'Configuring large (8 inch) Load Air Valve
                            LoadAir8InchCtrl = New UpDownDemandController("LOAD_AIR_VALVE_DEMAND", "LOAD_AIR_VALVE_DEMAND_INCREMENT", "LOAD_AIR_VALVE_UP_DEMAND_BTN", "LOAD_AIR_VALVE_DOWN_DEMAND_BTN", Nothing)
                            LoadAir8InchCtrl.Maximum = 100
                            LoadAir8InchCtrl.Increment = 0.5
                            LoadAir8InchCtrl.Minimum = 0
                            LoadAir8InchCtrl.ResetValue = 0

                            HobartCtrl = New HobartController(Me, "STARTER_RECTIFIER_READY_28_VDC_IND", "STARTER_RECTIFIER_MODBUS_STATE", "STARTER_RECTIFIER_MODBUS_FAULT",
                                                                  "STARTER_RECTIFIER_VOLTAGE_VDC", "STARTER_RECTIFIER_CURRENT_AMP", "STARTER_RECTIFIER_POWER_KW", "STARTER_RECTIFIER_CURRENT_LIMIT_AMP", "STARTER_RECTIFIER_RESET_BTN",
                                                                  "STARTER_CONTACTOR1_REQUEST", Nothing, "STARTER_CONTACTOR1_CMD", Nothing,
                                                                  "STARTER_CONTACTOR1_DC_ON_IND", Nothing, "STARTER_CONTACTOR1_MODBUS_STATE", Nothing)

                            StarterLongCurrentTimerCtrl = New TimerController(Me, "STARTER_LONGCURRENT_START_CONDITION", "STARTER_LONGCURRENT_STOP_CONDITION", "STARTER_LONGCURRENT_CONDITION_RESET", "STARTER_LONGCURRENT_TIME_SEC", Nothing, 60, "STARTER_LONGCURRENT_MAX_VALUE_REACHED")
                            StarterOverCurrentTimerCtrl = New TimerController(Me, "STARTER_OVERCURRENT_START_CONDITION", "STARTER_OVERCURRENT_STOP_CONDITION", "STARTER_OVERCURRENT_CONDITION_RESET", "STARTER_OVERCURRENT_TIME_SEC", Nothing, 10, "STARTER_OVERCURRENT_MAX_VALUE_REACHED")

                            OverCurrentLB1PhaseA = New TimerController(Me, "LB1_IA_OVERCURRENT", Nothing, "LB1_IA_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IA_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseB = New TimerController(Me, "LB1_IB_OVERCURRENT", Nothing, "LB1_IB_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IB_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseC = New TimerController(Me, "LB1_IC_OVERCURRENT", Nothing, "LB1_IC_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IC_OVERCURRENT_REACHED")

                            'Stopwatch
                            StopWatchTimerCtrl = New TimerController(Me, "STOPWATCH_START_CONDITION", "STOPWATCH_STOP_CONDITION", "STOPWATCH_RESET_CONDITION", "STOPWATCH_TIME_SEC", "STOPWATCH_TIME", 0, Nothing, False)

                        Case 14105

                            If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

                                'TIMER DISABLED FOR THE MOMENT
                                'If a Fire occurs, we wait 5 seconds before resetting TC Arming : this is for the louver to extract fumes as much as possible outside the test cell
                                'and maximize argon concentration
                                FacilityFireTimerCtrl = New TimerController(Me, "FACILITY_FIRE_TIME_START_CONDITION_LATCH", "FACILITY_FIRE_TIME_STOP_CONDITION_LATCH", "FACILITY_FIRE_TIME_CONDITION_RESET", "FACILITY_FIRE_TIME_SEC", Nothing, 5, "FACILITY_FIRE_TIME_MAX_VALUE_REACHED")

                                'A "Return to IDLE" functionnality also needs to be implemented in case the operator goes back to "FLY" without having moved the lever, resulting in a slam accel.
                                UPSTimerCtrl = New TimerController(Me, "UPS_ON_BATTERY_START_CONDITION", Nothing, "UPS_ON_BATTERY_RESET_CONDITION", Nothing, Nothing, 300, "UPS_ON_BATTERY_MAX_TIME_REACHED")

                                E_STOP5SecDelayTimerCtrl = New TimerController(Me, "ESTOP_DELAY_START_CONDITION", Nothing, "ESTOP_DELAY_RESET_CONDITION", Nothing, Nothing, 5, "ESTOP_5_SEC_DELAY_MAX_TIME_REACHED")
                                E_STOP12SecDelayTimerCtrl = New TimerController(Me, "ESTOP_DELAY_START_CONDITION", Nothing, "ESTOP_DELAY_RESET_CONDITION", Nothing, Nothing, 12, "ESTOP_12_SEC_DELAY_MAX_TIME_REACHED")

                                'Dyno and Flywheel Scavenge Pumps are deactivated after 60 seconds we have obtained the associated low pressure switch signal
                                DynoScavengePumpsTimerCtrl = New TimerController(Me, "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_START_CONDITION", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_STOP_CONDITION", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_CONDITION_RESET", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_TIME_SEC", Nothing, 60, "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_MAX_VALUE_REACHED")
                                FlywheelScavengePumpsTimerCtrl = New TimerController(Me, "FLYWHEEL_POWER_PACK_SCAV_PUMPS_START_CONDITION", "FLYWHEEL_POWER_PACK_SCAV_PUMPS_STOP_CONDITION", "FLYWHEEL_POWER_PACK_SCAV_PUMPS_CONDITION_RESET", "FLYWHEEL_POWER_PACK_SCAV_PUMPS_TIME_SEC", Nothing, 60, "FLYWHEEL_POWER_PACK_SCAV_PUMPS_MAX_VALUE_REACHED")

                                'Scavenge Pumps Feedbacks are monitored but not activated by the Test Cell arming (this is done by putting 0 as feedback delay)
                                'Scavenge Pumps are activated when the power pack is activated ; Scavenge Pumps are deactivated only when the low pressure switch signal confirms
                                'that the hydraulic fluid has been recycled. If the Scavenge pumps are deactivated too early, an overflow of hydraulic fluid will occur on the test bed.

                                Dim FacilityItems As New List(Of CEL.HMI.FacilityController.FacilityItemsList)


                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("LOUVER_INTAKE_BTN", "LOUVER_INTAKE_REVERSE_IND", 10.0, Nothing))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("LOUVER_EXHAUST_BTN", "LOUVER_EXHAUST_IND", 15.0, Nothing))

                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_FAN_BTN", "COOLING_TOWER_FAN_STATUS", 3.0, "COOLING_TOWER_FAN_OVERLOAD"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_PUMP_BTN", "COOLING_TOWER_PUMP_STATUS", 3.0, "COOLING_TOWER_PUMP_OVERLOAD"))

                                '                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_AIR_SUPPLY_BTN", "SHOP_AIR_PRESSURE_SWITCH_IND", 3.0, Nothing))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_HYDRAULIC_POWER_PACK_SUPPLY_BTN", "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_STATUS", 3.0, "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_OVERLOAD"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_BTN", "FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_STATUS", 3.0, "FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_OVERLOAD"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_WATER_SUPPLY_PUMP_BTN", "DYNO_WATER_SUPPLY_PUMP_DRIVE_STATUS", 6.0, "DYNO_WATER_SUPPLY_PUMP_DRIVE_OVERLOAD"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("INTAKE_HEATER_FAN_BTN", "HEATER_FAN_RUNNING_IND", 3.0, "INTAKE_HEATER_FAULT"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("INTAKE_HEATER_BTN", "INTAKE_HEATER_STATUS", 3.0, "INTAKE_HEATER_FAULT"))

                                FacilityCtrl = New FacilityController(Me, "TC_ARMING_BTN", FacilityItems, False, False, "")

                            End If

                        Case 15127 'HNA

                            OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", Nothing, 0, "NONE", True)
                            StarterLongCurrentTimerCtrl = New TimerController(Me, "STARTER_LONGCURRENT_START_CONDITION", "STARTER_LONGCURRENT_STOP_CONDITION", "STARTER_LONGCURRENT_CONDITION_RESET", "STARTER_LONGCURRENT_TIME_SEC", Nothing, 60, "STARTER_LONGCURRENT_MAX_VALUE_REACHED")
                            StarterOverCurrentTimerCtrl = New TimerController(Me, "STARTER_OVERCURRENT_START_CONDITION", "STARTER_OVERCURRENT_STOP_CONDITION", "STARTER_OVERCURRENT_CONDITION_RESET", "STARTER_OVERCURRENT_TIME_SEC", Nothing, 10, "STARTER_OVERCURRENT_MAX_VALUE_REACHED")

                            UPSTimerCtrl = New TimerController(Me, "UPS_ON_BATTERY_START_CONDITION", Nothing, "UPS_ON_BATTERY_RESET_CONDITION", Nothing, Nothing, 60, "UPS_ON_BATTERY_MAX_TIME_REACHED")

                            OverCurrentLB1PhaseA = New TimerController(Me, "LB1_IA_OVERCURRENT", Nothing, "LB1_IA_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IA_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseB = New TimerController(Me, "LB1_IB_OVERCURRENT", Nothing, "LB1_IB_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IB_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseC = New TimerController(Me, "LB1_IC_OVERCURRENT", Nothing, "LB1_IC_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IC_OVERCURRENT_REACHED")

                        Case 15208 'Intermountain
                            OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", Nothing, 0, "NONE", True)
                            UnitronCtrl = New UnitronController(Me, "STARTER_RECTIFIER_BTN", "STARTER_RECTIFIER_STATUS_IND", "STARTER_RECTIFIER_OVERLOAD_IND", "CRANK_BTN", "STARTER_RECTIFIER_CONTACTOR_FEEDBACK_IND", "STARTER_VOLTAGE_SOURCE_OPC", "STARTER_CURRENT_SOURCE_OPC", Nothing, Nothing)
                            StopWatchTimerCtrl = New TimerController(Me, "STOPWATCH_START_CONDITION", "STOPWATCH_STOP_CONDITION", "STOPWATCH_RESET_CONDITION", "STOPWATCH_TIME_SEC", "STOPWATCH_TIME", 0, Nothing, False)

                        Case 15234 'DAO
                            If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

                                'If a Fire occurs, we wait 5 seconds before resetting TC Arming : this is for the louver to extract fumes as much as possible outside the test cell
                                'and maximize argon concentration
                                'DISABLED AT THE MOMENT AS THE CUSTOMER'S FIRE BEHAVIOR IS UNCLEAR.

                                'FacilityFireTimerCtrl = New TimerController(Me, "FACILITY_FIRE_TIME_START_CONDITION_LATCH", "FACILITY_FIRE_TIME_STOP_CONDITION_LATCH", "FACILITY_FIRE_TIME_CONDITION_RESET", "FACILITY_FIRE_TIME_SEC", Nothing, 5, "FACILITY_FIRE_TIME_MAX_VALUE_REACHED")

                            End If

                        Case 17631 'CTS
                            If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

                                'If a Fire occurs, we wait 5 seconds before resetting TC Arming : this is for the louver to extract fumes as much as possible outside the test cell
                                'and maximize argon concentration
                                ' Alain                    FacilityFireTimerCtrl = New TimerController(Me, "FACILITY_FIRE_TIME_START_CONDITION_LATCH", "FACILITY_FIRE_TIME_STOP_CONDITION_LATCH", "FACILITY_FIRE_TIME_CONDITION_RESET", "FACILITY_FIRE_TIME_SEC", Nothing, 5, "FACILITY_FIRE_TIME_MAX_VALUE_REACHED")

                                'If the UPS system is activated, after 5 seconds, a signal is will be sent to the throttle controller (cRIO) to "return to idle"
                                ' ALain               UPSTimerCtrl = New TimerController(Me, "UPS_ON_BATTERY_START_CONDITION", Nothing, "UPS_ON_BATTERY_RESET_CONDITION", Nothing, Nothing, 5, "UPS_ON_BATTERY_MAX_TIME_REACHED")

                                Dim FacilityItems As New List(Of CEL.HMI.FacilityController.FacilityItemsList)

                                If CELProject.EngineFamily = "PT6A" Then
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("SHAFT_GUARD_LOCKING_SWITCH_BTN", "SHAFT_GUARD_ENGAGED_IND", 3.0, Nothing))
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("AIR_CYLINDER_SOLENOID_BTN", "DUMMY_TRUE_IND", 4.0, Nothing))
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("AIR_FEATHER_SOLENOID_BTN", "DUMMY_TRUE_IND", 4.0, Nothing))
                                End If
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_FAN_BTN", "COOLING_TOWER_FAN_AUX_IND", 3.0, "COOLING_TOWER_FAN_OVERLOAD_IND"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_SUPPLY_PUMP_BTN", "COOLING_TOWER_SUPPLY_PUMP_AUX", 3.0, "COOLING_TOWER_SUPPLY_PUMP_OVERLOAD"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("INTAKE_LOUVER_BTN", "INTAKE_LOUVER_IND", 30.0, Nothing))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("EXHAUST_HAT_BTN", "EXHAUST_HAT_OPEN_IND", 30.0, Nothing))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_AIR_SUPPLY_BTN", "DUMMY_TRUE_IND", 4.0, Nothing))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_POWER_PACK_BTN", "DYNO_POWER_PACK_AUX", 4.0, "DYNO_POWER_PACK_OVERLOAD"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_WATER_SUPPLY_PUMP_BTN", "DYNO_WATER_SUPPLY_PUMP_AUX", 4.0, "DYNO_WATER_SUPPLY_PUMP_OVERLOAD"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("FUEL_SUPPLY_PUMP_BTN", "FUEL_SUPPLY_PUMP_AUX", 4.0, "FUEL_SUPPLY_PUMP_OVERLOAD"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("STARTER_MAIN_CONTACTOR_BTN", "STARTER_MAIN_CONTACTOR_ON_IND", 4.0, Nothing))

                                FacilityCtrl = New FacilityController(Me, "TC_ARMING_BTN", FacilityItems, False, False, "")

                            End If

                        Case 17865 'S7
                            OperatingTimerCtrl = New TimerController(Me, "OPERATING_TIME_START_CONDITION", "OPERATING_TIME_STOP_CONDITION", "OPERATING_TIME_CONDITION_RESET", "OPERATING_TIME_CURRENT_SEC", "OPERATING_TIME_HH_MM_SS", 0, "NONE", True)

                            UPSTimerCtrl = New TimerController(Me, "UPS_ON_BATTERY_START_CONDITION", Nothing, "UPS_ON_BATTERY_RESET_CONDITION", Nothing, Nothing, 300, "UPS_ON_BATTERY_MAX_TIME_REACHED")

                            'Cette variable est uitilise pour fermer pendant 5 secondes la valve du shop air lors d'un demarrage de l'applicaqtion pour un moteur
                            CeldasTimeRunningTimerCtrl = New TimerController(Me, "CELDAS_TIME_RUNNING_START_CONDITION", "CELDAS_TIME_RUNNING_STOP_CONDITION", "CELDAS_TIME_RUNNING_RESET_CONDITION", "CELDAS_TIME_RUNNING_SEC", Nothing, 0, "NONE", False)
                            CELDAS_TIME_RUNNING_START_CONDITION.Value = Values.True

                            'Configuring large (8 inch) Load Air Valve Reset after fast closing
                            LoadAir8InchFastClosingCtrl = New PreviousValueReturnController("LOAD_AIR_VALVE_DEMAND", "RAPID_CLOSING_BTN")

                            'Configuring large (8 inch) Load Air Valve
                            LoadAir8InchCtrl = New UpDownDemandController("LOAD_AIR_VALVE_DEMAND", "LOAD_AIR_VALVE_DEMAND_INCREMENT", "LOAD_AIR_VALVE_UP_DEMAND_BTN", "LOAD_AIR_VALVE_DOWN_DEMAND_BTN", Nothing)
                            LoadAir8InchCtrl.Maximum = 100
                            LoadAir8InchCtrl.Increment = 0.5
                            LoadAir8InchCtrl.Minimum = 0
                            LoadAir8InchCtrl.ResetValue = 0

                            HobartCtrl = New HobartController(Me, "STARTER_RECTIFIER_READY_28_VDC_IND", "STARTER_RECTIFIER_MODBUS_STATE", "STARTER_RECTIFIER_MODBUS_FAULT",
                                                                  "STARTER_RECTIFIER_VOLTAGE_VDC", "STARTER_RECTIFIER_CURRENT_AMP", "STARTER_RECTIFIER_POWER_KW", "STARTER_RECTIFIER_CURRENT_LIMIT_AMP", "STARTER_RECTIFIER_RESET_BTN",
                                                                  "STARTER_CONTACTOR1_REQUEST", Nothing,
                                                                  "STARTER_CONTACTOR1_CMD", Nothing,
                                                                  "STARTER_CONTACTOR1_DC_ON_IND", Nothing,
                                                                  "STARTER_CONTACTOR1_MODBUS_STATE", Nothing)

                            OverCurrentLB1PhaseA = New TimerController(Me, "LB1_IA_OVERCURRENT", Nothing, "LB1_IA_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IA_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseB = New TimerController(Me, "LB1_IB_OVERCURRENT", Nothing, "LB1_IB_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IB_OVERCURRENT_REACHED")
                            OverCurrentLB1PhaseC = New TimerController(Me, "LB1_IC_OVERCURRENT", Nothing, "LB1_IC_OVERCURRENT_RESET", Nothing, Nothing, 1, "LB1_IC_OVERCURRENT_REACHED")

                            OverVoltageLB1PhaseVAB = New TimerController(Me, "LB1_VAB_OVERVOLTAGE", Nothing, "LB1_VAB_OVERVOLTAGE_RESET", Nothing, Nothing, 1, "LB1_VAB_OVERVOLTAGE_REACHED")
                            OverVoltageLB1PhaseVBC = New TimerController(Me, "LB1_VBC_OVERVOLTAGE", Nothing, "LB1_VBC_OVERVOLTAGE_RESET", Nothing, Nothing, 1, "LB1_VBC_OVERVOLTAGE_REACHED")
                            OverVoltageLB1PhaseVCA = New TimerController(Me, "LB1_VCA_OVERVOLTAGE", Nothing, "LB1_VCA_OVERVOLTAGE_RESET", Nothing, Nothing, 1, "LB1_VCA_OVERVOLTAGE_REACHED")

                            StarterLongCurrentTimerCtrl = New TimerController(Me, "STARTER_LONGCURRENT_START_CONDITION", "STARTER_LONGCURRENT_STOP_CONDITION", "STARTER_LONGCURRENT_CONDITION_RESET", "STARTER_LONGCURRENT_TIME_SEC", Nothing, 60, "STARTER_LONGCURRENT_MAX_VALUE_REACHED")
                            StarterOverCurrentTimerCtrl = New TimerController(Me, "STARTER_OVERCURRENT_START_CONDITION", "STARTER_OVERCURRENT_STOP_CONDITION", "STARTER_OVERCURRENT_CONDITION_RESET", "STARTER_OVERCURRENT_TIME_SEC", Nothing, 10, "STARTER_OVERCURRENT_MAX_VALUE_REACHED")

                            'Stopwatch
                            StopWatchTimerCtrl = New TimerController(Me, "STOPWATCH_START_CONDITION", "STOPWATCH_STOP_CONDITION", "STOPWATCH_RESET_CONDITION", "STOPWATCH_TIME_SEC", "STOPWATCH_TIME", 0, Nothing, False)
                            StopWatch2TimerCtrl = New TimerController(Me, "STOPWATCH_2_START_CONDITION", "STOPWATCH_2_STOP_CONDITION", "STOPWATCH_2_RESET_CONDITION", "STOPWATCH_2_TIME_SEC", "STOPWATCH_2_TIME", 0, Nothing, False)

                        Case 18957
                            If CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

                                'TIMER DISABLED FOR THE MOMENT
                                'If a Fire occurs, we wait 5 seconds before resetting TC Arming : this is for the louver to extract fumes as much as possible outside the test cell
                                'and maximize argon concentration
                                'FacilityFireTimerCtrl = New TimerController(Me, "FACILITY_FIRE_TIME_START_CONDITION_LATCH", "FACILITY_FIRE_TIME_STOP_CONDITION_LATCH", "FACILITY_FIRE_TIME_CONDITION_RESET", "FACILITY_FIRE_TIME_SEC", Nothing, 5, "FACILITY_FIRE_TIME_MAX_VALUE_REACHED")

                                'A "Return to IDLE" functionnality also needs to be implemented in case the operator goes back to "FLY" without having moved the lever, resulting in a slam accel.
                                UPSTimerCtrl = New TimerController(Me, "UPS_ON_BATTERY_START_CONDITION", Nothing, "UPS_ON_BATTERY_RESET_CONDITION", Nothing, Nothing, 300, "UPS_ON_BATTERY_MAX_TIME_REACHED")

                                E_STOP5SecDelayTimerCtrl = New TimerController(Me, "ESTOP_DELAY_START_CONDITION", Nothing, "ESTOP_DELAY_RESET_CONDITION", Nothing, Nothing, 5, "ESTOP_5_SEC_DELAY_MAX_TIME_REACHED")
                                E_STOP12SecDelayTimerCtrl = New TimerController(Me, "ESTOP_DELAY_START_CONDITION", Nothing, "ESTOP_DELAY_RESET_CONDITION", Nothing, Nothing, 12, "ESTOP_12_SEC_DELAY_MAX_TIME_REACHED")

                                'Dyno and Flywheel Scavenge Pumps are deactivated after 60 seconds we have obtained the associated low pressure switch signal
                                If CELProject.EngineFamily = "T700" Then
                                    DynoScavengePumpsTimerCtrl = New TimerController(Me, "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_START_CONDITION", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_STOP_CONDITION", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_CONDITION_RESET", "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_TIME_SEC", Nothing, 60, "DYNO_HYDRAULIC_POWER_PACK_SCAV_PUMPS_MAX_VALUE_REACHED")
                                    FlywheelScavengePumpsTimerCtrl = New TimerController(Me, "FLYWHEEL_POWER_PACK_SCAV_PUMPS_START_CONDITION", "FLYWHEEL_POWER_PACK_SCAV_PUMPS_STOP_CONDITION", "FLYWHEEL_POWER_PACK_SCAV_PUMPS_CONDITION_RESET", "FLYWHEEL_POWER_PACK_SCAV_PUMPS_TIME_SEC", Nothing, 60, "FLYWHEEL_POWER_PACK_SCAV_PUMPS_MAX_VALUE_REACHED")
                                End If

                                'Scavenge Pumps Feedbacks are monitored but not activated by the Test Cell arming (this is done by putting 0 as feedback delay)
                                'Scavenge Pumps are activated when the power pack is activated ; Scavenge Pumps are deactivated only when the low pressure switch signal confirms
                                'that the hydraulic fluid has been recycled. If the Scavenge pumps are deactivated too early, an overflow of hydraulic fluid will occur on the test bed.

                                Dim FacilityItems As New List(Of CEL.HMI.FacilityController.FacilityItemsList)


                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("EXHAUST_HAT_BTN", "EXHAUST_HAT_OPENED_IND", 15.0, Nothing))

                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_FAN_BTN", "COOLING_TOWER_FAN_1_STATUS", 3.0, "COOLING_TOWER_FAN_1_OVERLOAD"))
                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("COOLING_TOWER_PUMP_BTN", "COOLING_TOWER_PUMP_STATUS", 3.0, "COOLING_TOWER_PUMP_OVERLOAD"))

                                If CELProject.EngineFamily = "T700" Then
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("SHAFT_GUARD_COMMAND_POWER_TO_LOCK_CMD", "SHAFT_GUARD_ENGAGED_IND", 5.0, Nothing))
                                End If

                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_HYDRAULIC_POWER_PACK_SUPPLY_BTN", "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_STATUS", 3.0, "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_OVERLOAD"))

                                If CELProject.EngineFamily = "T700" Then
                                    FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_BTN", "FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_STATUS", 3.0, "FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_OVERLOAD"))
                                End If

                                FacilityItems.Add(New CEL.HMI.FacilityController.FacilityItemsList("DYNO_WATER_SUPPLY_PUMP_BTN", "DYNO_WATER_SUPPLY_PUMP_DRIVE_STATUS", 6.0, "DYNO_WATER_SUPPLY_PUMP_DRIVE_OVERLOAD"))

                                FacilityCtrl = New FacilityController(Me, "TC_ARMING_BTN", FacilityItems, False, False, "")

                            End If


                        Case Else
                            Debug.Assert(False, "Common_Sequence.OnTestSessionRunning: Code Missing for this project")

                    End Select
                    MyBase.StartWorkingThread()

                End If

            Catch ex As Exception
                MessageBox.Show("Common_Sequence.OnTestSessionRunning: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub AddHandlers()
            AddRemoveHandler(True)
        End Sub
        Protected Overrides Sub RemoveHandlers()
            AddRemoveHandler(False)
        End Sub
        Private Function AddRemoveHandler(Optional ByVal Add As Boolean = False) As Boolean

            AddRemoveHandler = False

            Try
                If Add Then
                    AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN

                    Select Case CELProject.Project.Number
                        Case 12917
                            AddHandler TARE_BTN.ValueChanged, AddressOf OnTARE_BTN
                        Case 14085
                            If CELProject.EngineFamily = "PT6C" Or CELProject.EngineModel = "PW210A" Then
                                AddHandler DYNO_HYDRAULIC_POWER_PACK_SUPPLY_HS150_BTN.ValueChanged, AddressOf OnDynoFlywheelPowerPackDeactivated
                                AddHandler FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_BTN.ValueChanged, AddressOf OnDynoFlywheelPowerPackDeactivated
                            End If
                            AddHandler FACILITY_WATER_LEVEL_H_IND.ValueChanged, AddressOf OnFacilityWaterLevel

                    End Select

                Else
                    RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN

                    Select Case CELProject.Project.Number
                        Case 12917
                            RemoveHandler TARE_BTN.ValueChanged, AddressOf OnTARE_BTN
                        Case 14085
                            If CELProject.EngineFamily = "PT6C" Or CELProject.EngineModel = "PW210A" Then
                                RemoveHandler DYNO_HYDRAULIC_POWER_PACK_SUPPLY_HS150_BTN.ValueChanged, AddressOf OnDynoFlywheelPowerPackDeactivated
                                RemoveHandler FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_BTN.ValueChanged, AddressOf OnDynoFlywheelPowerPackDeactivated
                            End If
                            RemoveHandler FACILITY_WATER_LEVEL_H_IND.ValueChanged, AddressOf OnFacilityWaterLevel

                    End Select

                End If
                AddRemoveHandler = True
            Catch ex As Exception
                MessageBox.Show("Common_Sequence.AddRemoveHandlers: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End Function

        Protected Overrides Sub Init_Engine_Controls()

        End Sub
        Protected Overrides Function Init_Engine_Channels() As Boolean

            Dim returnValue As New ArrayList()

            returnValue.Clear()

            Try

                Select Case CELProject.Project.Number

                    Case 11813
                        'Do Nothing

                    Case 11825 'Ethiopie
                        'Do Nothing

                    Case 11847
                        'Do Nothing

                    Case 11894
                        'Do Nothing

                    Case 12917
                        returnValue.Add(MyBase.InitializeChannel(TARE_BTN, "TARE_BTN"))
                        returnValue.Add(MyBase.InitializeChannel(LOAD_CELL_1_KG_MEAN, "LOAD_CELL_1_KG_MEAN"))
                        returnValue.Add(MyBase.InitializeChannel(LOAD_CELL_2_KG_MEAN, "LOAD_CELL_2_KG_MEAN"))
                        returnValue.Add(MyBase.InitializeChannel(LOAD_CELL_1_TARE_OFFSET_KG, "LOAD_CELL_1_TARE_OFFSET_KG"))
                        returnValue.Add(MyBase.InitializeChannel(LOAD_CELL_2_TARE_OFFSET_KG, "LOAD_CELL_2_TARE_OFFSET_KG"))

                    Case 12958 'Epcor
                        'Do Nothing

                    Case 13015 'THY
                        'Do Nothing

                    Case 13031
                        returnValue.Add(MyBase.InitializeChannel(APU_TESTED_ACCORDING_TO, "APU_TESTED_ACCORDING_TO"))
                        'Do Nothing

                    Case 14075
                        'Do Nothing

                    Case 14085
                        returnValue.Add(MyBase.InitializeChannel(DYNO_HYDRAULIC_POWER_PACK_SUPPLY_HS150_BTN, "DYNO_HYDRAULIC_POWER_PACK_SUPPLY_HS150_BTN"))
                        returnValue.Add(MyBase.InitializeChannel(FACILITY_WATER_LEVEL_H_IND, "FACILITY_WATER_LEVEL_H_IND"))
                        returnValue.Add(MyBase.InitializeChannel(FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_BTN, "FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_BTN"))

                    Case 14086
                        returnValue.Add(MyBase.InitializeChannel(STOPWATCH_TIME_SEC, "STOPWATCH_TIME_SEC"))
                        returnValue.Add(MyBase.InitializeChannel(STOPWATCH_TIME, "STOPWATCH_TIME"))

                    Case 14094
                        'Do Nothing

                    Case 14105
                        'Do Nothing

                    Case 15127
                        'Do Nothing

                    Case 15208
                        'Do Nothing

                    Case 15234
                        'Do Nothing

                    Case 17631
                        'Do Nothing

                    Case 17865
                        returnValue.Add(MyBase.InitializeChannel(CELDAS_TIME_RUNNING_START_CONDITION, "CELDAS_TIME_RUNNING_START_CONDITION"))
                       'Do Nothing

                    Case 18957
                        'Do Nothing

                    Case Else
                        Debug.Assert(False, "Common_Sequence.OnTestSessionRunning: Code Missing for this project")
                End Select

            Catch ex As Exception
                MessageBox.Show("Common_Sequence.Init_Engine_Channels: " & ex.ToString())
                returnValue.Add(False)
            Finally

            End Try

            If returnValue.Contains(False) Then
                Return False
            End If
            Return True

        End Function
        Protected Overrides Sub Init_Engine_Channels_Value()

        End Sub

        Public Function DisplayCustomMessage() As Boolean

            Try

                Dim Custom_Parameters As New System.Collections.Specialized.OrderedDictionary()

                If CELProject.EngineModel <> "DAS CALIBRATION" And CELProject.EngineModel <> "SYSTEM CHECK" Then
                    CELProject.LoadSave_Custom_Parameters(Custom_Parameters, False) 'Load some parameters value from database
                End If

                Select Case CELProject.Project.Number
                    Case 13031

                        Dim Message As String = Nothing

                        If Not (APU_TESTED_ACCORDING_TO Is Nothing) Then
                            APU_TESTED_ACCORDING_TO.Value = New KineticaRT.Values.Text(Custom_Parameters("APUTestedAccordingTo").ToString())
                        Else
                            Return False
                        End If

                        Message = CELProject.EngineModel + " tested acording to: " + vbCr + APU_TESTED_ACCORDING_TO.Value.ToString()
                        clsTopMostMessageBox.Show(Message, "CELDAS200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    Case Else
                        Return False
                End Select

                Return True

            Catch ex As Exception
                MessageBox.Show("Common_Sequence.DisplayCustomMessage: " & ex.ToString())
            End Try

        End Function

        Private Sub OnCLOSE_TEST_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(newVal) Then
                    If Not (CommonPersistentValuesCtrl Is Nothing) Then
                        'Persistent Values Force Save
                        CommonPersistentValuesCtrl.SavePersistentValues(True)
                    End If

                    RemoveHandlers()
                End If
            Catch ex As Exception
                MessageBox.Show("Common_Sequence.OnCLOSE_TEST_BTN: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub OnTARE_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            If (CELProject.IsTrue(newVal)) Then

                Try
                    LOAD_CELL_1_TARE_OFFSET_KG.Value = New KineticaRT.Values.Double(-LOAD_CELL_1_KG_MEAN.Value.ToDouble)
                    LOAD_CELL_2_TARE_OFFSET_KG.Value = New KineticaRT.Values.Double(-LOAD_CELL_2_KG_MEAN.Value.ToDouble)

                Catch ex As System.Exception
                    MessageBox.Show("Common_Sequence.OnTARE_BTN: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                TARE_BTN.Value = Values.False

            End If

        End Sub

        '*****************************************************
        '* OnDYNO_HYDRAULIC_POWER_PACK_SUPPLY_HS150_BTN()    *
        '*****************************************************
        Private Sub OnDynoFlywheelPowerPackDeactivated(ByVal sender As KineticaRT.Channels.IChannel, ByVal newVal As KineticaRT.Values.Value)
            If (CELProject.IsFalse(newVal)) Then

                Try
                    CELDAS.NotifyIcon.ShowBalloonTip(30000, CELProject.TechnologyName, "Dynamometers and Flywheel Scavenge Pumps will keep working 3 minutes after the pressure switch is deactivated", ToolTipIcon.Info)
                Catch ex As System.Exception
                    MessageBox.Show("Common_Sequence.OnDYNO_HYDRAULIC_POWER_PACK_SUPPLY_HS150_BTN: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End If

        End Sub

        '*****************************
        '* OnFacilityWaterLevel()    *
        '*****************************
        Private Sub OnFacilityWaterLevel(ByVal sender As KineticaRT.Channels.IChannel, ByVal newVal As KineticaRT.Values.Value)
            Try
                If (CELProject.IsTrue(newVal)) Then
                    CELDAS.NotifyIcon.ShowBalloonTip(30000, CELProject.TechnologyName, "Hi Water Level Stabilized : Make sure the draining pump is off", ToolTipIcon.Info)
                Else
                    CELDAS.NotifyIcon.ShowBalloonTip(30000, CELProject.TechnologyName, "Hi Water Level Reached : Make sure the draining pump is activated", ToolTipIcon.Info)
                End If

            Catch ex As System.Exception
                MessageBox.Show("Common_Sequence.OnFacilityWaterLevel: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        ''********************************************************************
        ''* Private Members **************************************************
        ''********************************************************************
        Private STOPWATCH_TIME_SEC As INumericChannel
        Private STOPWATCH_2_TIME_SEC As INumericChannel
        Private STOPWATCH_TIME As ITextChannel
        Private STOPWATCH_2_TIME As ITextChannel
        Private APU_TESTED_ACCORDING_TO As KineticaRT.Channels.TextChannel
        Private CELDAS_TIME_RUNNING_START_CONDITION As IBooleanChannel
        Private TARE_BTN As KineticaRT.Channels.IBooleanChannel
        Private DYNO_HYDRAULIC_POWER_PACK_SUPPLY_HS150_BTN As KineticaRT.Channels.IBooleanChannel
        Private FACILITY_WATER_LEVEL_H_IND As KineticaRT.Channels.IBooleanChannel
        Private FLYWHEEL_HYDRAULIC_POWER_PACK_SUPPLY_BTN As KineticaRT.Channels.IBooleanChannel
        Private FUEL_USED_TOTAL_LITERS As INumericChannel
        Private FUEL_USED_TOTAL_KG As INumericChannel
        Private FUEL_USED_INITIAL_KG As INumericChannel
        Private FUEL_USED_INITIAL_LITERS As INumericChannel
        Private LOAD_CELL_1_KG_MEAN As KineticaRT.Channels.INumericChannel
        Private LOAD_CELL_2_KG_MEAN As KineticaRT.Channels.INumericChannel
        Private LOAD_CELL_1_TARE_OFFSET_KG As KineticaRT.Channels.INumericChannel
        Private LOAD_CELL_2_TARE_OFFSET_KG As KineticaRT.Channels.INumericChannel

        '********
        '* Misc *
        '********
        Private BuzzerTimerCtrl As TimerController = Nothing
        Private CeldasTimeRunningTimerCtrl As TimerController = Nothing
        Private UPSTimerCtrl As TimerController = Nothing
        Private E_STOP5SecDelayTimerCtrl As TimerController = Nothing
        Private E_STOP12SecDelayTimerCtrl As TimerController = Nothing
        Private StopWatchTimerCtrl As TimerController = Nothing
        Private StopWatch2TimerCtrl As TimerController = Nothing
        Private OverCurrentLB1PhaseA As TimerController = Nothing
        Private OverCurrentLB1PhaseB As TimerController = Nothing
        Private OverCurrentLB1PhaseC As TimerController = Nothing
        Private OverCurrentLB2PhaseA As TimerController = Nothing
        Private OverCurrentLB2PhaseB As TimerController = Nothing
        Private OverCurrentLB2PhaseC As TimerController = Nothing
        Private OverVoltageLB1PhaseVAB As TimerController = Nothing
        Private OverVoltageLB1PhaseVBC As TimerController = Nothing
        Private OverVoltageLB1PhaseVCA As TimerController = Nothing

        '**********
        '* Thread *
        '**********
        Private CommonPersistentValuesCtrl As PersistentValuesController = Nothing
        Private DynoInletCtrl As UpDownDemandController = Nothing
        Private DynoOutletCtrl As UpDownDemandController = Nothing
        Private DynoScavengePumpsTimerCtrl As TimerController = Nothing
        Private FacilityFireTimerCtrl As TimerController = Nothing
        Private FlywheelScavengePumpsTimerCtrl As TimerController = Nothing
        Private FuelRegulatorCtrl As UpDownDemandController = Nothing
        Private LoadAirCtrl As UpDownDemandController = Nothing
        Private OperatingTimerCtrl As TimerController = Nothing
        Private SmallLoadAirCtrl As UpDownDemandController = Nothing
        Private LargeLoadAirCtrl As UpDownDemandController = Nothing
        Private LoadAir3InchCtrl As UpDownDemandController = Nothing
        Private LoadAir3InchFastClosingCtrl As PreviousValueReturnController = Nothing
        Private LoadAir8InchCtrl As UpDownDemandController = Nothing
        Private LoadAir8InchFastClosingCtrl As PreviousValueReturnController = Nothing
        Private StarterLongCurrentTimerCtrl As TimerController = Nothing
        Private StarterOverCurrentTimerCtrl As TimerController = Nothing
        Private FacilityCtrl As FacilityController = Nothing
        Private UnitronCtrl As UnitronController = Nothing
        Private HobartCtrl As HobartController = Nothing
        Private StartTimerDCDfmeaCtrl As TimerController = Nothing

    End Class

End Namespace

Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms
Imports KineticaRT
Imports KineticaRT.Sequence
Imports KineticaRT.Values
Imports KineticaRT.Channels
Imports KineticaRT.Controls.Frame
Imports Microsoft.Office.Interop 'Excel
Imports System.Data
Imports System.Data.OleDb
Imports Microsoft.VisualBasic
Imports EntitySpaces.Interfaces

Imports CEL.Library
Imports CEL.HMI
Imports CEL.Data.BusinessObjects

Namespace StudioDemonstration

	Public Class RE220_RJ_Sequence
        Inherits ApuSequence

#Region "Constants"

        Private Const MaxNumberOfTBleedProbe As Integer = 4
        Private Const MaxNumberOfTEgtProbe As Integer = 15
        Private Const MaxNumberOfTInletProbe As Integer = 15

#End Region

        Private Enum eTestStepRE220
            NO_LOAD = 0         ' RTL
            ECS_LOAD = 1        ' ECS
            MES_LOAD = 2        ' MES
            FINAL_DATA = 3
        End Enum

        Protected Overrides Sub Compute(ByVal CalulateForLogAndPerformanceReports As Boolean)

            Try
                If CalulateForLogAndPerformanceReports Then

                    If EngineConfiguration.UseMeasuredBleedFlowValue Then
                        LOG_PERF_WB_CALC.Value = LOG_PERF_WB_MEASURED.Value
                    Else
                        LOG_PERF_WB_CALC.Value = New KineticaRT.Values.Double(Compute_BleedAirFlow(LOG_PERF_T_ORIFICE_DEGF.Value.ToDouble(),
                                                                                                     LOG_PERF_P_ORIFICE_PSIA.Value.ToDouble(),
                                                                                                     LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToDouble))
                    End If

                    Compute_RE220_Perf(LOG_PERF_T_INLET_AVG_DEGF,
                                       LOG_PERF_DELWB,
                                       LOG_PERF_DELPB,
                                       LOG_PERF_DELTB,
                                       LOG_PERF_DELWF,
                                       LOG_PERF_DELEGT,
                                       LOG_PERF_EGTREQ_DEGF)

                    FlowmeterCtrl.ComputeFlow(True)
                Else

                    If EngineConfiguration.UseMeasuredBleedFlowValue Then
                        WB_CALC.Value = WB_MEASURED.Value
                    Else
                        WB_CALC.Value = New KineticaRT.Values.Double(Compute_BleedAirFlow(T_ORIFICE_DEGF.Value.ToDouble(),
                                                                                          P_ORIFICE_PSIA.Value.ToDouble(),
                                                                                          P_ORIFICE_DELTA_PSID.Value.ToDouble))
                    End If

                    Compute_RE220_Perf(T_INLET_AVG_DEGF,
                                       DELWB,
                                       DELPB,
                                       DELTB,
                                       DELWF,
                                       DELEGT,
                                       EGTREQ_DEGF)
                End If

            Catch ex As Exception
                MessageBox.Show("Compute_RE220" + ex.ToString(), "Error")
            End Try

        End Sub

        Private Sub Compute_RE220_Perf(ByRef tinlet_degf_avg As IChannel,
                                            ByRef del_wb As IChannel,
                                            ByRef del_pb As IChannel,
                                            ByRef del_tb As IChannel,
                                            ByRef del_wf As IChannel,
                                            ByRef del_egt As IChannel,
                                            ByRef egt_req As IChannel)

            Try

                Select Case CELProject.PERF_TEST_STEP_NB.Value.ToDouble()

                    Case eTestStepRE220.NO_LOAD, eTestStepRE220.FINAL_DATA
                        del_wb.Value = Values.NaN
                        del_pb.Value = Values.NaN
                        del_tb.Value = Values.NaN
                        del_wf.Value = Values.NaN
                        del_egt.Value = Values.NaN
                        egt_req.Value = Values.NaN

                    Case eTestStepRE220.ECS_LOAD
                        'ECS
                        del_wb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), RE220_ECS, 1)
                        del_pb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), RE220_ECS, 2)
                        del_tb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), RE220_ECS, 3)
                        del_wf.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), RE220_ECS, 4)
                        del_egt.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), RE220_ECS, 5)
                        egt_req.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), RE220_EGT, 1)

                    Case eTestStepRE220.MES_LOAD
                        'MES
                        del_pb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), RE220_MES, 1)
                        del_tb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), RE220_MES, 2)
                        del_egt.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), RE220_MES, 3)

                End Select

            Catch Ex As Exception
                MessageBox.Show("Compute_RE220_Perf: " & Ex.ToString())
            End Try

        End Sub

        Private Sub ValidateFMInitialValue()
            Try

                Select Case CELProject.Project.Number

                    Case 17865
                        FM_INITIAL_VOLUME_LITERS.Value = Values.Zero    'TODO: MB: Flowmeter or MassFlow?

                    Case Else
                        Debug.Assert(False, "ValidateFMInitialValue: Code Missign for this project")

                End Select

            Catch Ex As Exception
                MessageBox.Show("ValidateFMInitialValue: " & Ex.ToString())
            End Try

        End Sub

        Private Sub EmulateInletFlapOpeningClosing()

            Static FlapPosition As Integer = 0
            Try

                If CELProject.IsTrue(INLET_FLAP_OPEN_CMD.Value) Then
                    If (FlapPosition < 3000) Then
                        FlapPosition = FlapPosition + 100
                    Else
                        FlapPosition = 3000
                    End If

                ElseIf CELProject.IsTrue(INLET_FLAP_CLOSE_CMD.Value) Then
                    If (FlapPosition > 0) Then
                        FlapPosition = FlapPosition - 100
                    Else
                        FlapPosition = 0
                    End If
                End If


                If FlapPosition = 0 Then ' (* 0 is close position*)
                    If CELProject.IsFalse(INLET_FLAP_CLOSED_IND.Value) Then
                        INLET_FLAP_CLOSED_IND.Value = Values.True
                    End If
                Else
                    If CELProject.IsTrue(INLET_FLAP_CLOSED_IND.Value) Then
                        INLET_FLAP_CLOSED_IND.Value = Values.False
                    End If
                End If

                If FlapPosition = 3000 Then '(* 3000 is open position*)
                    If CELProject.IsFalse(INLET_FLAP_OPENED_IND.Value) Then
                        INLET_FLAP_OPENED_IND.Value = Values.True
                    End If
                Else
                    If CELProject.IsTrue(INLET_FLAP_OPENED_IND.Value) Then
                        INLET_FLAP_OPENED_IND.Value = Values.False
                    End If
                End If

                If (FlapPosition <> 0) And (FlapPosition <> 3000) Then
                    If CELProject.IsFalse(INLET_FLAP_MOVING_IND.Value) Then
                        INLET_FLAP_MOVING_IND.Value = Values.True
                    End If
                Else
                    If CELProject.IsTrue(INLET_FLAP_MOVING_IND.Value) Then
                        INLET_FLAP_MOVING_IND.Value = Values.False
                    End If

                End If

            Catch ex As Exception

            End Try

        End Sub


        Private Function CreateS717865PerformanceFile(ByVal AppendOption As Boolean, ByVal TestStepNumber As Integer, ByVal ProtectPassword As String, ByVal UnProtectPassword As String, ByRef excelWorkbook As Excel.Workbook) As Boolean

            CreateS717865PerformanceFile = CreateStandardCELPerformanceFile(AppendOption, TestStepNumber, ProtectPassword, UnProtectPassword, excelWorkbook)

        End Function

        Private Function CreateStandardCELPerformanceFile(ByVal AppendOption As Boolean, ByVal TestStepNumber As Integer, ByVal ProtectPassword As String, ByVal UnProtectPassword As String, ByRef excelWorkbook As Excel.Workbook) As Boolean

            Dim currentSheet As String
            Dim excelSheets As Excel.Sheets
            Dim excelWorksheet As Excel.Worksheet
            Dim currentRow As Integer = 2
            Dim currentCol As Integer = 2


            CreateStandardCELPerformanceFile = True

            ' The following gets the Worksheets collection
            excelSheets = excelWorkbook.Worksheets

            Try
                excelWorkbook.Worksheets(1).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(2).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(3).Unprotect(UnProtectPassword)

                If AppendOption = False Then

                    currentSheet = "RAW DATA"
                    excelWorksheet = excelSheets.Item(currentSheet)

                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(0).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(1).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(2).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(3).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(4).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(5).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(6).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(7).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(8).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(9).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Answer(10).Value.ToString()


                    currentRow += 2
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Choice(0).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Choice(3).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Choice(4).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Choice(5).Value.ToString()

                    currentRow += 1
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = CELProject.Test_Mgr_UUT_Choice(6).Value.ToString()

                    currentRow += 31
                    excelWorksheet.Cells(currentRow, currentCol).NumberFormat = "@"
                    excelWorksheet.Cells(currentRow, currentCol).Value = Format(Now, "d MMMM yyyy")

                End If

                Select Case TestStepNumber
                    Case eTestStepRE220.NO_LOAD
                        currentCol = 5

                    Case eTestStepRE220.ECS_LOAD
                        currentCol = 6

                    Case eTestStepRE220.MES_LOAD
                        currentCol = 7

                    Case eTestStepRE220.FINAL_DATA
                        currentCol = 2
                End Select

                currentSheet = "RAW DATA"
                currentRow = 2
                excelWorksheet = excelSheets.Item(currentSheet)

                If TestStepNumber <> eTestStepRE220.FINAL_DATA Then

                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_T_INLET_AVG_DEGF.Value.ToString(LOG_PERF_T_INLET_AVG_DEGF.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_P_INLET_PSIA.Value.ToString(LOG_PERF_P_INLET_PSIA.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_P_GEARBOX_PSIG.Value.ToString(LOG_PERF_P_GEARBOX_PSIG.Format, Nothing)      ' GEARBOX PRESSURE in PSIG not inH2O

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_T_ETC1_DEGF.Value.ToString(LOG_PERF_T_ETC1_DEGF.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_T_ETC2_DEGF.Value.ToString(LOG_PERF_T_ETC2_DEGF.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_T_ETC_DELTA_DEGF.Value.ToString(LOG_PERF_T_ETC_DELTA_DEGF.Format, Nothing)

                    ' EGT Temperatures
                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_T_EGT_AVG_DEGF.Value.ToString(LOG_PERF_T_EGT_AVG_DEGF.Format, Nothing)

                    If (TestStepNumber = eTestStepRE220.ECS_LOAD) Then
                        currentRow = currentRow + 1
                        excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_EGTCOR1.Value.ToString(LOG_PERF_EGTCOR1.Format, Nothing)
                    Else
                        currentRow = currentRow + 1
                        excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_EGTCR1.Value.ToString(LOG_PERF_EGTCR1.Format, Nothing)
                    End If

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)


                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing)

                    ' Bleed Discharge Corrected Flow (ACTUAL + Corrected* and Corrected**)
                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_WBDC.Value.ToString(LOG_PERF_WBDC.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_WBDCR1.Value.ToString(LOG_PERF_WBDCR1.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_DWBDCOR.Value.ToString(LOG_PERF_DWBDCOR.Format, Nothing)

                    ' Bleed Airflow
                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_WBCOR1.Value.ToString(LOG_PERF_WBCOR1.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)


                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_PBCOR1.Value.ToString(LOG_PERF_PBCOR1.Format, Nothing)


                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_TBCOR1.Value.ToString(LOG_PERF_TBCOR1.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_WFCOR1.Value.ToString(LOG_PERF_WFCOR1.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_SHP.Value.ToString(LOG_PERF_SHP.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_SHPCOR.Value.ToString(LOG_PERF_SHPCOR.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = Format(Now, "d MMMM yyyy HH:mm")
                Else
                    currentRow = 24
                    excelWorksheet.Cells(currentRow, currentCol).Value = APU_DRY_WEIGHT.Value.ToString(APU_DRY_WEIGHT.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = NUMBER_OF_STARTS.Value.ToString(NUMBER_OF_STARTS.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = OPERATING_TIME_CURRENT_HOURS.Value.ToString(OPERATING_TIME_CURRENT_HOURS.Format, Nothing)

                    currentRow = currentRow + 1
                    excelWorksheet.Cells(currentRow, currentCol).Value = STARTING_TIME_SEC.Value.ToString(STARTING_TIME_SEC.Format, Nothing)

                    currentRow = 32
                    excelWorksheet.Cells(currentRow, currentCol).Value = Format(Now, "d MMMM yyyy HH:mm")

                    excelWorksheet.Visible = Excel.XlSheetVisibility.xlSheetHidden


                    currentSheet = "SUMMARY"
                    excelWorksheet = excelSheets.Item(currentSheet)

                    If CELProject.IsTrue(PERF_TEST_RESULT_ACCEPTED.Value) Then
                        excelWorksheet.OLEObjects("TestResultAccepted").object.value = True
                    Else
                        excelWorksheet.OLEObjects("TestResultAccepted").object.value = False
                    End If

                    If CELProject.IsTrue(PERF_TEST_RESULT_REJECTED.Value) Then
                        excelWorksheet.OLEObjects("TestResultRejected").object.value = True
                    Else
                        excelWorksheet.OLEObjects("TestResultRejected").object.value = False
                    End If

                End If


            Catch ex As Exception
                System.Diagnostics.Trace.WriteLine("CreatePerformanceFile: " + ex.ToString())
                MessageBox.Show(ex.ToString)
                CreateStandardCELPerformanceFile = False

            Finally
                'Lock Sheets
                excelWorkbook.Worksheets(1).Protect(ProtectPassword)
                excelWorkbook.Worksheets(2).Protect(ProtectPassword)
                excelWorkbook.Worksheets(3).Protect(ProtectPassword)

            End Try

        End Function


        Private Sub Init_Arrays()

            Dim i As Integer
            Dim rs As New ADODB.Recordset
            Dim SelectString As String

            Try

                'Read Table 106A RE220
                SelectString = "SELECT * FROM RE220_EGT ORDER BY [T1]"
                If DatabaseHelper.OpenRecordset(rs, SelectString, cnMain, True) Then
                    ReDim RE220_EGT(DatabaseHelper.RecordCount(rs) - 1, 1)
                    For i = 0 To RE220_EGT.GetLength(0) - 1
                        RE220_EGT(i, 0) = rs.Fields("T1").Value
                        RE220_EGT(i, 1) = rs.Fields("EGT").Value
                        rs.MoveNext()
                    Next i
                    DatabaseHelper.CloseRecordset(rs)
                End If

                'Read Table 106B RE220
                SelectString = "SELECT * FROM RE220_ECS ORDER BY [T1]"
                If DatabaseHelper.OpenRecordset(rs, SelectString, cnMain, True) Then
                    ReDim RE220_ECS(DatabaseHelper.RecordCount(rs) - 1, 5)
                    For i = 0 To RE220_ECS.GetLength(0) - 1
                        RE220_ECS(i, 0) = rs.Fields("T1").Value
                        RE220_ECS(i, 1) = rs.Fields("DWB").Value
                        RE220_ECS(i, 2) = rs.Fields("DPB").Value
                        RE220_ECS(i, 3) = rs.Fields("DTB").Value
                        RE220_ECS(i, 4) = rs.Fields("DWF").Value
                        RE220_ECS(i, 5) = rs.Fields("DEGT").Value
                        rs.MoveNext()
                    Next i
                    DatabaseHelper.CloseRecordset(rs)
                End If

                'Read Table 106C RE220
                SelectString = "SELECT * FROM RE220_MES ORDER BY [T1]"
                If DatabaseHelper.OpenRecordset(rs, SelectString, cnMain, True) Then
                    ReDim RE220_MES(DatabaseHelper.RecordCount(rs) - 1, 3)
                    For i = 0 To RE220_MES.GetLength(0) - 1
                        RE220_MES(i, 0) = rs.Fields("T1").Value
                        RE220_MES(i, 1) = rs.Fields("DPB").Value
                        RE220_MES(i, 2) = rs.Fields("DTB").Value
                        RE220_MES(i, 3) = rs.Fields("DEGT").Value
                        rs.MoveNext()
                    Next i
                    DatabaseHelper.CloseRecordset(rs)
                End If

            Catch Ex As Exception
                MessageBox.Show("InitArrays_RE220: " & Ex.ToString())
            End Try
        End Sub

        Protected Overrides Sub Init_Engine_Channels_Value()

            Try

                'Read the mass flow meter value
                FM_INITIAL_VOLUME_LITERS.Value = New KineticaRT.Values.Double(FM_TOTAL_VOLUME_LITERS.Value.ToDouble())

                ' Set the Test Step to first step (Ready To Load)
                CELProject.PERF_TEST_STEP_NB.Value = Values.Zero

                ' Close the Test Cell Valve (CMM 49-24-50 3.C.2.d)
                Select Case CELProject.Project.Number
                    Case 17865
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsS7.LHV)
                        LOAD_AIR_VALVE_DEMAND.Value = New KineticaRT.Values.Double(100.0)

                    Case Else
                        Debug.Assert(False, "Compute_Time: Code Missign for this project")
                End Select

            Catch Ex As Exception
                MessageBox.Show("Init_Engine_Channels_Value: " & Ex.ToString())
            End Try
        End Sub

        Protected Overrides Sub Init_Engine_Controls()

            Try

                ' Set the Test Step to first step (Ready To Load)
                cmbTestStepList.SelectedIndex = 0

            Catch ex As Exception
                MessageBox.Show("Init_Engine_Controls: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Protected Overrides Function CreateCustomHeaderLogFile(ByRef ExcelWorksheet As Excel.Worksheet, ByRef RowIndex As Integer, ByRef ColumnIndex As Integer) As Boolean

            Try
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = "Date Time:"
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Font.Bold = True
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight

                RowIndex += 1
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = "Cond#:"
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Font.Bold = True
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight

                RowIndex += 1
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = "Comment:"
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Font.Bold = True
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
                ExcelWorksheet.Rows(RowIndex).RowHeight = 65.25
                ExcelWorksheet.Rows(RowIndex).VerticalAlignment = Excel.XlVAlign.xlVAlignTop
                ExcelWorksheet.Rows(RowIndex).WrapText = True

                CreateCustomHeaderLogFile = True

            Catch ex As Exception
                MessageBox.Show("CreateCustomHeaderLogFile: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CreateCustomHeaderLogFile = False
            End Try

            Return CreateCustomHeaderLogFile

        End Function

        Protected Overrides Function UpdateCustomHeaderLogFile(ByVal AppendOption As Boolean, ByVal IsPerfEntry As Boolean, ByRef ExcelWorksheet As Excel.Worksheet, ByRef RowIndex As Integer, ByRef ColumnIndex As Integer) As Boolean

            Try
                Dim Log_Condition As Integer

                If AppendOption = False Then
                    Log_Condition = 1
                Else
                    Log_Condition = ExcelWorksheet.Cells(RowIndex + 1, ExcelWorksheet.Columns.Count).End(Excel.XlDirection.xlToLeft).Column - 1
                    Log_Condition = Log_Condition + 1
                End If

                If ((Log_Condition > 1) And ((Log_Condition Mod 5) = 1)) Then

                    'Repeat The Test Spec For this new sets of condition

                    With ExcelWorksheet
                        .Range(.Cells(2, 2), .Cells(RowIndex - 2, 6)).Copy(.Cells(2, Log_Condition + 1))
                    End With
                End If

                ColumnIndex = 1 + Log_Condition

                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = Format(Now, "yyyy/MM/dd HH:mm:ss")
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
                RowIndex += 1
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = Log_Condition.ToString()
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
                RowIndex += 1
                If IsPerfEntry = False Then
                    Dim CommentText As String = ""
                    Dim InputObject As New InputCommentForm
                    InputObject.ShowDialog("Log Report", "Comment:", "", CommentText)

                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = CommentText
                Else
                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = "STEP: " & cmbTestStepList.SelectedItem
                End If

                ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
                RowIndex += 2
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = "Values"
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Font.Bold = True
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

                UpdateCustomHeaderLogFile = True
            Catch ex As Exception
                MessageBox.Show("UpdateCustomHeaderLogFile: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                UpdateCustomHeaderLogFile = False
            End Try

            Return UpdateCustomHeaderLogFile

        End Function

        Protected Overrides Sub OnEngineWorkingThread()

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Return
            End If

            Compute(False)

            EmulateInletFlapOpeningClosing()

            'Make sure FM_INITIAL_VOLUME_LITERS is different than 0
            ValidateFMInitialValue()

            'Persistent Values Save Check
            PersistentValuesCtrl.SavePersistentValues()

            StartTimerCtrl.Timer()


        End Sub

        Private Function Compute_BleedAirFlow(ByVal TOrifice_DEGF As Double, _
                                                ByVal POrifice_PSIA As Double, _
                                                ByVal POrificeDelta_PSID As Double) As Double

            Dim BleedAirFLow As Double

            Try

                If CELProject.IsFalse(BLEED_BTN.Value) Then
                    Return 0.0
                End If

                BleedAirFLow = CELProject.GetBleedAirFlow(EngineConfiguration.DuctDiameter,
                                                          EngineConfiguration.DuctDiameterUnit,
                                                          EngineConfiguration.OrificeDiameter,
                                                          EngineConfiguration.OrificeDiameterUnit,
                                                          TOrifice_DEGF,
                                                          CELDAS.eTemperature.Fahrenheit,
                                                          POrifice_PSIA,
                                                          CELDAS.ePressure.PSI,
                                                          POrificeDelta_PSID,
                                                          CELDAS.ePressure.PSI,
                                                          EngineConfiguration.OrificeThinkness,
                                                          EngineConfiguration.OrificeThinknessUnit,
                                                          EngineConfiguration.TapArrangement,
                                                          CELDAS.eFlowMass.LbByMin)

                Return BleedAirFLow

            Catch Ex As Exception
                MessageBox.Show("RE220_Sequence.Compute_BleedAirFlow: " & Ex.ToString)
                Return Double.NaN

            End Try

        End Function

        Private Sub OnAcceleratingTime_Calc(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsTrue(newVal) And WF_AVG_PPH.Value > 5.0 Then
                    NUMBER_OF_STARTS.Value = New KineticaRT.Values.Double(NUMBER_OF_STARTS.Value.ToDouble + 1)
                End If


            Catch ex As Exception
                MessageBox.Show("OnAcceleratingTime_Calc: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Private Sub OnCapture(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                If sender.Name = "Log_Perf_CaptureNoLoad" Then

                ElseIf sender.Name = "Log_Perf_CaptureECS" Then
                    Log_Perf_ECS_WBCOR1.Value = WBCOR1.Value
                    Log_Perf_ECS_PBCOR.Value = PBCOR.Value
                    Log_Perf_ECS_SHPCOR.Value = SHPCOR.Value

                ElseIf sender.Name = "Log_Perf_CaptureMES" Then
                    Log_Perf_MES_TBCOR.Value = TBCOR.Value
                    Log_Perf_MES_PBCOR.Value = PBCOR.Value
                    Log_Perf_MES_SHPCOR.Value = SHPCOR.Value
                    Log_Perf_MES_EGTCOR.Value = EGTCOR.Value
                End If

            Catch ex As Exception
                MessageBox.Show("OnCapture: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Protected Overrides Sub OnCLOSE_TEST_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                MyBase.StopWorkingThread()

                'Persistent Values Force Save
                PersistentValuesCtrl.SavePersistentValues(True)
                RemoveHandlers()
                Dispose_Engine_Controllers()

                CELProject.CLOSE_TEST_BTN.Value = Values.False


            Catch ex As Exception
                MessageBox.Show("OnClose_Test: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnHONEYWELL_COMPUTER_SELECTION_BTN(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(newVal) Then
                CELProject.ArincCtrl.UpdateLayout(EngineInformationItem.eArincConfiguration.HoneywellComputer)
            Else
                CELProject.ArincCtrl.UpdateLayout(EngineInformationItem.eArincConfiguration.Omnibusbox)
            End If

        End Sub

        Protected Overrides Sub OnLOG_TAKE_POINT_BTN(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                'Update the Log/Perf channel set
                CELProject.LogPerfSynch.UpdateValues()

                'Synchronize data for Log and Performance
                Compute(True)

                CELProject.DoLogging(AddressOf UpdateCustomHeaderLogFile, False, CELProject.EngineModel, True)
                CELProject.LOG_TAKE_POINT_BTN.Value = Values.False


            Catch Ex As Exception
                MessageBox.Show("OnLOG_TAKE_POINT_BTN: " & Ex.ToString())
            End Try
        End Sub

        Protected Overrides Sub OnNEW_TEST_BTN(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Return
            End If

            If (CELProject.IsFalse(newVal)) Then
                Return
            End If


            If DialogResult.Yes = MessageBox.Show("Do you want to reset the fuel used, the operating time, the number of starts and other persistent values?", "CELDAS200", MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
				NUMBER_OF_STARTS.Value = Values.Zero
				OPERATING_TIME_CURRENT_SEC.Value = Values.Zero
				FUEL_USED_INITIAL_LITERS.Value = Values.Zero
				WF_TOT1.Value = Values.Zero
				WF_TOT2.Value = Values.Zero
				'Log_Perf_NbApuFaults.Value = Values.Zero

				VIB_GEARBOX_MAX_IPS_PEAK_PERSISTENT.Value = Values.Zero
				VIB_TURBINE_MAX_IPS_PEAK_PERSISTENT.Value = Values.Zero
			End If

        End Sub


        Protected Overrides Sub OnPERF_CLOSE_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                'CELProject.ActivateForm("RE220_Performance_Screen", CELProject.EngineModel.Replace("-", "_") + "_Screen", False, False)
                CELProject.ActivateForm(PerformanceScreenName, MainScreenName, False, False)

                CELProject.PERF_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                MessageBox.Show("OnPERF_CLOSE_WINDOW_BTN: " & Ex.ToString())
            End Try
        End Sub


        Protected Overrides Sub OnPERF_OPEN_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                'CELProject.ActivateForm("RE220_Performance_Screen", CELProject.EngineModel.Replace("-", "_") + "_Screen", True, False)
                CELProject.ActivateForm(PerformanceScreenName, MainScreenName, True, False)
                CELProject.PERF_OPEN_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                MessageBox.Show("OnPERF_OPEN_WINDOW_BTN: " & Ex.ToString())
            End Try
        End Sub

        Private Function GetPerformanceReportTemplateFileName() As String

            Dim PerformanceReportTemplateFileName As String = EngineConfiguration.PerformanceReportTemplateFile
            Dim SuffixToAdd As String = CELProject.MeasurementUnitNameInUse

            Dim ReportTemplateFileName = System.IO.Path.GetFileNameWithoutExtension(PerformanceReportTemplateFileName)
            Dim ReportTemplatePath = System.IO.Path.GetDirectoryName(PerformanceReportTemplateFileName)

            ReportTemplateFileName = System.IO.Path.Combine(ReportTemplatePath, ReportTemplateFileName + SuffixToAdd + System.IO.Path.GetExtension(PerformanceReportTemplateFileName))
            Return ReportTemplateFileName

        End Function

        Protected Overrides Sub OnPERF_TAKE_POINT_BTN(ByVal sender As IChannel, ByVal newVal As Value)

            Dim SearchPath As String
            Dim excelWorkbook As Excel.Workbook = Nothing

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                'Update the Log/Perf channel set
                CELProject.LogPerfSynch.UpdateValues()

                'Synchronize data for Log and Performance
                Compute(True)

                SearchPath = CELProject.EngineSession.PerfFolder


                Dim NewFileCreated As Boolean
                Dim PerfTemplateFullFileName As String = GetPerformanceReportTemplateFileName()

                Select Case CELProject.Project.Number

                    Case 17865 ' S7
                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateS717865PerformanceFile)

                    Case Else

                        Debug.Assert(False, "OnPerf_TAKE_POINT_BTN, Code Missing for this project")
                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateStandardCELPerformanceFile)

                End Select

                'CELProject.DoLogging(AddressOf UpdateCustomHeaderLogFile, True, CELProject.EngineModel, NewFileCreated)
                CELProject.PERF_TAKE_POINT_BTN.Value = Values.False

            Catch ex As Exception
                MessageBox.Show("OnPERF_TAKE_POINT_BTN: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally

            End Try
        End Sub


        Private Sub OnPERFTestResultAcceptedCheckBox(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                PERF_TEST_RESULT_REJECTED.Value = Values.False

            Catch Ex As Exception
                MessageBox.Show("OnPERFTestResultAcceptedCheckBox: " & Ex.ToString())
            End Try
        End Sub


        Private Sub OnPERFTestResultRejectedCheckBox(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                PERF_TEST_RESULT_ACCEPTED.Value = Values.False


            Catch Ex As Exception
                MessageBox.Show("OnPERFTestResultRejectedCheckBox: " & Ex.ToString())
            End Try
        End Sub


        Private Sub OnLOAD_AIR_VALVES_OPEN(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                Select Case CELProject.Project.Number

                    Case 17865
                        LOAD_AIR_VALVE_DEMAND.Value = New Global.KineticaRT.Values.Double(100)

                End Select

            Catch ex As Exception
                MessageBox.Show("OnLOAD_AIR_VALVES_CLOSE: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Protected Overrides Sub OnTestStep_List_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                CELProject.PERF_TEST_STEP_NB.Value = New KineticaRT.Values.Integer(cmbTestStepList.SelectedIndex)

            Catch ex As Exception
                MessageBox.Show("OnTestStep_List_SelectedIndexChanged: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End Sub

        Protected Overrides Sub OnTestSessionClosing(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)

            Try
                MyBase.OnTestSessionClosing(newStatus)

                If CELProject.EngineFamily <> "RE220" Then
                    Return
                End If

                If Not (FlowmeterCtrl Is Nothing) Then
                    FlowmeterCtrl.Dispose()
                End If

                'Close the connection to the database
                DatabaseHelper.CloseConnection(cnMain)

            Catch ex As Exception
                MessageBox.Show("OnTestSessionClosing: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionInitialized(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionInitialized(newStatus)

                If CELProject.EngineFamily <> "RE220" Then
                    Return
                End If

                Select Case CELProject.Project.Number

                    Case 17865
                        UserInputsS7.MapUserInputsParameters(CELProject)

                    Case Else
                        Debug.Assert(False, "OnTestSessionInitialized, Code Missign for this project")

                End Select

                EngineNameAsPrefix = CELProject.EngineModel.Replace("-", "_")
                BaseScreenName = EngineNameAsPrefix + "_Screen"
                PerformanceScreenName = EngineNameAsPrefix + "_Performance_Screen" + CELProject.MeasurementUnitNameInUse
                MainScreenName = BaseScreenName + CELProject.MeasurementUnitNameInUse

                LoadBankFrameControlName = MainScreenName + ".fraControlSystem.fraLoadBankControl"
                TestStepFrameControlName = MainScreenName + ".fraControl"
                ControlSystemFrameName = MainScreenName + ".fraControlSystem"


                CELProject.EngineSession.WorkingFolder = "C:\CELDAS DATA\" + CELProject.EngineModel + "\" + CELProject.Test_Mgr_UUT_Answer(0).Value.ToString() + "\" + CELProject.Test_Mgr_UUT_Answer(1).Value.ToString()
                CELProject.CreateLogFile(AddressOf CreateCustomHeaderLogFile, CELProject.EngineModel, CELProject.EngineModel)

                'Set The Connection (Global scope)
                If Not DatabaseHelper.OpenConnection(CELProject.Database.OleDbConnectionString, cnMain, True) Then
                    'Close Test if unable to open connection to database
                    CELProject.Test_Mgr_Cmd.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Cmd.Close)
                End If

            Catch ex As Exception
                MessageBox.Show("OnTestSessionInitialized: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionOpened(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionOpened(newStatus)

            Catch ex As Exception

                MessageBox.Show("RE220_Sequence.OnTestSessionOpened: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try
        End Sub

        Protected Overrides Sub OnTestSessionRunning(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)

            Try
                MyBase.OnTestSessionRunning(newStatus)

                If CELProject.EngineFamily <> "RE220" Then
                    Return
                End If

                CELProject.LoadSave_Custom_Parameters(Custom_Parameters, False)

                Init_Engine_Channels()
                Init_Arrays()
                Init_Engine_Channels_Value()

                CELProject.Perf_Nb_Sheet_To_Print = 4

                Init_Engine_Controllers()

                CELProject.EngineWindowCollection.Item(0).EngineWindow.BeginInvoke(New MethodInvoker(AddressOf Me.Init_Engine_Controls)) 'Must be after Init_Engine Channels

                AddHandlers()

                MyBase.StartWorkingThread()

            Catch ex As Exception
                MessageBox.Show("OnTestSessionRunning: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub AddHandlers()

            AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN
            AddHandler Log_Perf_CaptureECS.ValueChanged, AddressOf OnCapture
            AddHandler Log_Perf_CaptureMES.ValueChanged, AddressOf OnCapture
            AddHandler Log_Perf_CaptureNoLoad.ValueChanged, AddressOf OnCapture

            'AddHandler HONEYWELL_COMPUTER_SELECTION_BTN.ValueChanged, AddressOf OnHONEYWELL_COMPUTER_SELECTION_BTN
            AddHandler CELProject.LOG_TAKE_POINT_BTN.ValueChanged, AddressOf OnLOG_TAKE_POINT_BTN
            AddHandler PERF_NEW_TEST_OPC.ValueChanged, AddressOf OnNEW_TEST_BTN
            AddHandler CELProject.PERF_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnPERF_CLOSE_WINDOW_BTN
            AddHandler CELProject.PERF_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnPERF_OPEN_WINDOW_BTN
            AddHandler CELProject.PERF_TAKE_POINT_BTN.ValueChanged, AddressOf OnPERF_TAKE_POINT_BTN
            AddHandler PERF_TEST_RESULT_ACCEPTED.ValueChanged, AddressOf OnPERFTestResultAcceptedCheckBox
            AddHandler PERF_TEST_RESULT_REJECTED.ValueChanged, AddressOf OnPERFTestResultRejectedCheckBox
            AddHandler READY_TO_LOAD_IND.ValueChanged, AddressOf OnAcceleratingTime_Calc
            AddHandler cmbTestStepList.SelectedIndexChanged, AddressOf OnTestStep_List_SelectedIndexChanged

        End Sub
        Protected Overrides Sub RemoveHandlers()

            If Not (READY_TO_LOAD_IND Is Nothing) Then
                RemoveHandler READY_TO_LOAD_IND.ValueChanged, AddressOf OnAcceleratingTime_Calc
            End If

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN
            End If

            If Not (Log_Perf_CaptureECS Is Nothing) Then
                RemoveHandler Log_Perf_CaptureECS.ValueChanged, AddressOf OnCapture
            End If

            If Not (Log_Perf_CaptureMES Is Nothing) Then
                RemoveHandler Log_Perf_CaptureMES.ValueChanged, AddressOf OnCapture
            End If

            If Not (Log_Perf_CaptureNoLoad Is Nothing) Then
                RemoveHandler Log_Perf_CaptureNoLoad.ValueChanged, AddressOf OnCapture
            End If

            'If Not (HONEYWELL_COMPUTER_SELECTION_BTN Is Nothing) Then
            '    RemoveHandler HONEYWELL_COMPUTER_SELECTION_BTN.ValueChanged, AddressOf OnHONEYWELL_COMPUTER_SELECTION_BTN
            'End If

            If Not (CELProject.LOG_TAKE_POINT_BTN Is Nothing) Then
                RemoveHandler CELProject.LOG_TAKE_POINT_BTN.ValueChanged, AddressOf OnLOG_TAKE_POINT_BTN
            End If

            If Not (PERF_NEW_TEST_OPC Is Nothing) Then
                RemoveHandler PERF_NEW_TEST_OPC.ValueChanged, AddressOf OnNEW_TEST_BTN
            End If

            If Not (CELProject.PERF_CLOSE_WINDOW_BTN Is Nothing) Then
                RemoveHandler CELProject.PERF_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnPERF_CLOSE_WINDOW_BTN
            End If

            If Not (CELProject.PERF_OPEN_WINDOW_BTN Is Nothing) Then
                RemoveHandler CELProject.PERF_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnPERF_OPEN_WINDOW_BTN
            End If

            If Not (CELProject.PERF_TAKE_POINT_BTN Is Nothing) Then
                RemoveHandler CELProject.PERF_TAKE_POINT_BTN.ValueChanged, AddressOf OnPERF_TAKE_POINT_BTN
            End If

            If Not (PERF_TEST_RESULT_ACCEPTED Is Nothing) Then
                RemoveHandler PERF_TEST_RESULT_ACCEPTED.ValueChanged, AddressOf OnPERFTestResultAcceptedCheckBox
            End If

            If Not (PERF_TEST_RESULT_REJECTED Is Nothing) Then
                RemoveHandler PERF_TEST_RESULT_REJECTED.ValueChanged, AddressOf OnPERFTestResultRejectedCheckBox
            End If

            If Not (cmbTestStepList Is Nothing) Then
                RemoveHandler cmbTestStepList.SelectedIndexChanged, AddressOf OnTestStep_List_SelectedIndexChanged
            End If

        End Sub

        Protected Overrides Sub Init_Engine_Controllers()

            Dim PersistentValuesChannels() As String = {"NUMBER_OF_STARTS",
                                                           "OPERATING_TIME_CURRENT_SEC",
                                                           "FUEL_USED_INITIAL_LITERS",
                                                           "FUEL_USED_TOTAL_LITERS",
                                                           "VIB_GEARBOX_MAX_IPS_PEAK_PERSISTENT",
                                                           "VIB_TURBINE_MAX_IPS_PEAK_PERSISTENT"}

            Dim EngineSN As String = Nothing
            Dim WorkOrder As String = Nothing

            Select Case CELProject.Project.Number

                Case 17865
                    EngineSN = UserInputsS7.SerialNumber
                    WorkOrder = UserInputsS7.WorkOrder

                Case Else
                    Debug.Assert(False, "OnTestSessionRunning: Code Missing for this project")

            End Select

            Dim PersistentValuesFileName As String = "C:\CELDAS DATA\" & CELProject.EngineModel & "\" & EngineSN & "\" & WorkOrder & "\Log\PersistentValues.xml"
            PersistentValuesCtrl = New PersistentValuesController(Me, PersistentValuesChannels, 1, PersistentValuesFileName)

            Select Case CELProject.Project.Number

                Case 17865
                    If Double.IsNaN(FUEL_USED_TOTAL_LITERS.Value.ToDouble()) Then
                        FUEL_USED_INITIAL_LITERS.Value = Values.Zero
                    Else
                        FUEL_USED_INITIAL_LITERS.Value = New KineticaRT.Values.Double(FUEL_USED_TOTAL_LITERS.Value.ToDouble())
                    End If

                Case Else
                    Debug.Assert(False, "OnTestSessionRunning: Code Missing for this project")

            End Select

            ' Default value if not overridden by the project
            Dim RealNumberOfTBleedProbe As Integer = MaxNumberOfTBleedProbe
            Dim RealNumberOfTEgtProbe As Integer = MaxNumberOfTEgtProbe
            Dim RealNumberOfTInletProbe As Integer = MaxNumberOfTInletProbe

            Select Case CELProject.Project.Number

                Case 17865
                    Dim ConfigPrefix As String = "//ConfigurationSettings/" & CELProject.EngineFamily & "/"
                    RealNumberOfTInletProbe = CELProject.Project.ReadOneIntegerConfigItem(ConfigPrefix & "T_INLET_NUMBER_PROBE", 0) ' 15
                    RealNumberOfTEgtProbe = CELProject.Project.ReadOneIntegerConfigItem(ConfigPrefix & "T_EGT_NUMBER_PROBE", 0)     ' 12
                    RealNumberOfTBleedProbe = CELProject.Project.ReadOneIntegerConfigItem(ConfigPrefix & "T_BLEED_NUMBER_PROBE", 0) '  4

                    Debug.Assert(RealNumberOfTInletProbe <> 0 And RealNumberOfTEgtProbe <> 0 And RealNumberOfTBleedProbe <> 0)

                Case Else
                    Debug.Assert(False, "OnTestSessionRunning: Code Missign for this project")

            End Select

            'UpdateTBleedChannels()
            DeletableTBleedChCtrl = New DeletableChannelController(Me, RealNumberOfTBleedProbe, MaxNumberOfTBleedProbe, MaxNumberOfTBleedProbe, "T_BLEED", "_DEGF", "_DELETE_BTN", "T_BLEED_AVG_DEGF", "T_BLEED_MIN_DEGF", "T_BLEED_MAX_DEGF", "T_BLEED_SPREAD_DEGF")
            DeletableTEgtChCtrl = New DeletableChannelController(Me, RealNumberOfTEgtProbe, MaxNumberOfTEgtProbe, MaxNumberOfTEgtProbe, "T_EGT", "_DEGF", "_DELETE_BTN", "T_EGT_AVG_DEGF", "T_EGT_MIN_DEGF", "T_EGT_MAX_DEGF", "T_EGT_SPREAD_DEGF")
            DeletableTInletChCtrl = New DeletableChannelController(Me, RealNumberOfTInletProbe, MaxNumberOfTInletProbe, MaxNumberOfTInletProbe, "T_INLET", "_DEGF", "_DELETE_BTN", "T_INLET_AVG_DEGF", "T_INLET_MIN_DEGF", "T_INLET_MAX_DEGF", "T_INLET_SPREAD_DEGF")

            FlowmeterCtrl = New FlowmeterController(Me)

            StarterMaxCurrentRecordCtrl = New MinMaxRecordController(Me, MinMaxRecordController.eRecordType.Maximum, "STARTER_RECTIFIER_CURRENT_AMP", "STARTER_RECTIFIER_CURRENT_AMP_MAX", "START_BTN")
            StarterMaxVoltageRecordCtrl = New MinMaxRecordController(Me, MinMaxRecordController.eRecordType.Maximum, "STARTER_RECTIFIER_VOLTAGE_VDC", "STARTER_RECTIFIER_VOLTAGE_VDC_MAX", "START_BTN")

            StartTimerCtrl = New TimerController(Me, "STARTING_TIME_START_CONDITION_LATCH", "STARTING_TIME_STOP_CONDITION_LATCH", "STARTING_TIME_CONDITION_RESET", "STARTING_TIME_SEC", Nothing, 0, "NONE")

            ' Configure the Oil Cooling variable speed fan
            OilCoolingCtrl = New UpDownDemandController("OIL_COOLING_FAN_DEMAND", Nothing, "OIL_COOLING_UP_DEMAND", "OIL_COOLING_DOWN_DEMAND", Nothing)
            OilCoolingCtrl.Maximum = 100
            OilCoolingCtrl.Increment = 5
            OilCoolingCtrl.Minimum = 0
            OilCoolingCtrl.ResetValue = 0

            'Configuring Load Bank Control
            Dim myLBData As LoadBankController.LB_DataInfo

            myLBData.FanEnableChannelName = "LB1_FAN_ENABLE_CMD"
            myLBData.FanEnableBtnChannelName = "LB1_FAN_ENABLE_BTN"
            myLBData.EnableChannelName = "LB_LOAD_BTN"
            myLBData.DemandChannelName = "LB1_DEMAND"
            myLBData.UpDemandChannelName = "LB1_UP_DEMAND_BTN"
            myLBData.DownDemandChannelName = "LB1_DOWN_DEMAND_BTN"
            myLBData.DemandChannelIncrementName = "LB1_DEMAND_INCREMENT"
            myLBData.FrameName = LoadBankFrameControlName
            myLBData.DisplayName = "dspLB_DEMAND"
            myLBData.ThreadSpeedMsec = 250 'msec
            myLBData.Maximum = 100
            myLBData.DelestageKwpersec = 10 '10 kW/sec delestage
            myLBData.FanCoolDownSeconds = 300
            myLBData.Memorize = False
            myLBData.Increment = 1
            myLBData.InitialDemandChannelName = Nothing
            myLBData.BtnLatchSeconds = 0
            myLBData.FanEnableChannelPulseName = Nothing
            myLBData.FanStopChannelPulseName = Nothing
            myLBData.PulseEnableInitialType = False
            myLBData.PulseStopInitialType = False

            Select Case CELProject.Project.Number

                Case 17865
                    myLBData.FieldFlashingTimeMsec = 0  'no field flashing
                    myLBData.FieldFlashingSignalName = Nothing
                    myLBData.FieldFlashingTriggerSignalName = Nothing

                Case Else
                    myLBData.FieldFlashingTimeMsec = 0  'no field flashing
                    myLBData.FieldFlashingSignalName = Nothing
                    myLBData.FieldFlashingTriggerSignalName = Nothing
                    Debug.Assert(False, "OnTestSessionRunning: Code Missign for this project.")

            End Select

            LoadBankCtrl = New LoadBankController(myLBData)

            'Configuring Load Bank Stage Values
            Select Case CELProject.Project.Number

                Case 17865
                    LoadBankCtrl.AddStage(1, "LB1_1")
                    LoadBankCtrl.AddStage(1, "LB1_2")
                    LoadBankCtrl.AddStage(2, "LB1_3")
                    LoadBankCtrl.AddStage(4, "LB1_4")
                    LoadBankCtrl.AddStage(8, "LB1_5")
                    LoadBankCtrl.AddStage(16, "LB1_6")
                    LoadBankCtrl.AddStage(32, "LB1_7")
                    LoadBankCtrl.AddStage(32, "LB1_8")
                    LoadBankCtrl.AddStage(64, "LB1_9")

                Case Else
                    Debug.Assert(False, "OnTestSessionRunning: Load bank not configured for this project")

            End Select

            LoadBankCtrl.AddCurrentPhases("LB1_CURRENT_IA", "LB1_CURRENT_IB", "LB1_CURRENT_IC", 20, 300)
            LoadBankCtrl.AddVoltagePhases("LB1_VOLTAGE_VAB", "LB1_VOLTAGE_VBC", "LB1_VOLTAGE_VCA", 20, 150)
            LoadBankCtrl.StartWorkerThread(250)

        End Sub

        Protected Overrides Sub Dispose_Engine_Controllers()

            If Not (DeletableTBleedChCtrl Is Nothing) Then
                DeletableTBleedChCtrl.Dispose()
            End If

            If Not (DeletableTEgtChCtrl Is Nothing) Then
                DeletableTEgtChCtrl.Dispose()
            End If

            If Not (DeletableTInletChCtrl Is Nothing) Then
                DeletableTInletChCtrl.Dispose()
            End If

            If Not (LoadBankCtrl Is Nothing) Then
                LoadBankCtrl.Dispose()
            End If

            If Not (PersistentValuesCtrl Is Nothing) Then
                PersistentValuesCtrl.Dispose()
            End If

            If Not (StarterMaxCurrentRecordCtrl Is Nothing) Then
                StarterMaxCurrentRecordCtrl.Dispose()
            End If

            If Not (StarterMaxVoltageRecordCtrl Is Nothing) Then
                StarterMaxVoltageRecordCtrl.Dispose()
            End If

            If Not (StartTimerCtrl Is Nothing) Then
                StartTimerCtrl.Dispose()
            End If

            If Not (FlowmeterCtrl Is Nothing) Then
                FlowmeterCtrl.Dispose()
            End If

        End Sub

        Protected Overrides Sub SetTestStepList(value As Integer)
        End Sub

        Protected Overrides Function Init_Engine_Channels() As Boolean

            Dim Return_Value As New ArrayList()

            Return_Value.Clear()

            Try
                'Initialise Common Engine Channels

                Return_Value.Add(MyBase.InitializeChannel(BLEED_BTN, "BLEED_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(FLHV, "FLHV"))
                Return_Value.Add(MyBase.InitializeChannel(FUEL_USED_INITIAL_LITERS, "FUEL_USED_INITIAL_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(FUEL_USED_TOTAL_LITERS, "FUEL_USED_TOTAL_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(FM_INITIAL_VOLUME_LITERS, "FM_INITIAL_VOLUME_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(FM_TOTAL_VOLUME_LITERS, "FM_TOTAL_VOLUME_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_CLOSE_CMD, "INLET_FLAP_CLOSE_CMD"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_CLOSED_IND, "INLET_FLAP_CLOSED_IND"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_OPEN_CMD, "INLET_FLAP_OPEN_CMD"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_OPENED_IND, "INLET_FLAP_OPENED_IND"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_MOVING_IND, "INLET_FLAP_MOVING_IND"))
                Return_Value.Add(MyBase.InitializeChannel(LOAD_AIR_VALVE_DEMAND, "LOAD_AIR_VALVE_DEMAND"))
                'Return_Value.Add(MyBase.InitializeChannel(HONEYWELL_COMPUTER_SELECTION_BTN, "HONEYWELL_COMPUTER_SELECTION_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(NUMBER_OF_STARTS, "NUMBER_OF_STARTS"))
                Return_Value.Add(MyBase.InitializeChannel(OPERATING_TIME_CURRENT_SEC, "OPERATING_TIME_CURRENT_SEC"))
                Return_Value.Add(MyBase.InitializeChannel(OPERATING_TIME_CURRENT_HOURS, "OPERATING_TIME_CURRENT_HOURS"))
                Return_Value.Add(MyBase.InitializeChannel(T_INLET_AVG_DEGF, "T_INLET_AVG_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(READY_TO_LOAD_IND, "READY_TO_LOAD_IND"))
                Return_Value.Add(MyBase.InitializeChannel(STARTING_TIME_SEC, "STARTING_TIME_SEC"))
                Return_Value.Add(MyBase.InitializeChannel(APU_DRY_WEIGHT, "APU_DRY_WEIGHT"))


                Return_Value.Add(MyBase.InitializeChannel(DELEGT, "DELEGT"))
                Return_Value.Add(MyBase.InitializeChannel(DELPB, "DELPB"))
                Return_Value.Add(MyBase.InitializeChannel(DELTB, "DELTB"))
                Return_Value.Add(MyBase.InitializeChannel(DELWB, "DELWB"))
                Return_Value.Add(MyBase.InitializeChannel(DELWF, "DELWF"))
                Return_Value.Add(MyBase.InitializeChannel(EGTREQ_DEGF, "EGTREQ_DEGF"))

                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureNoLoad, "Log_Perf_CaptureNoLoad"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureECS, "Log_Perf_CaptureECS"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureMES, "Log_Perf_CaptureMES"))

                Return_Value.Add(MyBase.InitializeChannel(P_ORIFICE_PSIA, "P_ORIFICE_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(P_ORIFICE_DELTA_PSID, "P_ORIFICE_DELTA_PSID"))
                Return_Value.Add(MyBase.InitializeChannel(PBCOR_MEAN, "PBCOR_MEAN"))

                Return_Value.Add(MyBase.InitializeChannel(PERF_TEST_RESULT_ACCEPTED, "PERF_TEST_RESULT_ACCEPTED"))
                Return_Value.Add(MyBase.InitializeChannel(PERF_TEST_RESULT_REJECTED, "PERF_TEST_RESULT_REJECTED"))
                Return_Value.Add(MyBase.InitializeChannel(PERF_NEW_TEST_OPC, "PERF_NEW_TEST_OPC"))

                Return_Value.Add(MyBase.InitializeChannel(T_ORIFICE_DEGF, "T_ORIFICE_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(WB_CALC, "WB_CALC"))
                Return_Value.Add(MyBase.InitializeChannel(WB_MEASURED, "WB_MEASURED"))
                Return_Value.Add(MyBase.InitializeChannel(WF_AVG_PPH, "WF_AVG_PPH"))
                Return_Value.Add(MyBase.InitializeChannel(WF_TOT1, "WF_TOT1"))
                Return_Value.Add(MyBase.InitializeChannel(WF_TOT2, "WF_TOT2"))

                Return_Value.Add(MyBase.InitializeChannel(VIB_GEARBOX_MAX_IPS_PEAK_PERSISTENT, "VIB_GEARBOX_MAX_IPS_PEAK_PERSISTENT"))
                Return_Value.Add(MyBase.InitializeChannel(VIB_TURBINE_MAX_IPS_PEAK_PERSISTENT, "VIB_TURBINE_MAX_IPS_PEAK_PERSISTENT"))

                Return_Value.Add(MyBase.InitializeChannel(EGTCOR1, "EGTCOR1"))
                Return_Value.Add(MyBase.InitializeChannel(EGTCOR, "EGTCOR"))
                Return_Value.Add(MyBase.InitializeChannel(EGTCR1, "EGTCR1"))
                Return_Value.Add(MyBase.InitializeChannel(PBCOR1, "PBCOR1"))
                Return_Value.Add(MyBase.InitializeChannel(PBCOR, "PBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(TBCOR1, "TBCOR1"))
                Return_Value.Add(MyBase.InitializeChannel(TBCOR, "TBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(WBCOR1, "WBCOR1"))
                Return_Value.Add(MyBase.InitializeChannel(WBCOR, "WBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(WBDCR1, "WBDCR1"))
                Return_Value.Add(MyBase.InitializeChannel(DWBDCOR, "DWBDCOR"))
                Return_Value.Add(MyBase.InitializeChannel(WFCOR1, "WFCOR1"))
                Return_Value.Add(MyBase.InitializeChannel(WFCOR, "WFCOR"))
                Return_Value.Add(MyBase.InitializeChannel(WBDC, "WBDC"))
                Return_Value.Add(MyBase.InitializeChannel(SHPCOR, "SHPCOR"))



                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_BARO_PSIA, "LOG_PERF_P_BARO_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_INLET_AVG_DEGF, "LOG_PERF_T_INLET_AVG_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_INLET_PSIA, "LOG_PERF_P_INLET_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_OIL_PSIG, "LOG_PERF_P_OIL_PSIG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_OIL_DEGF, "LOG_PERF_T_OIL_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_GEARBOX_INH2O, "LOG_PERF_P_GEARBOX_INH2O"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_GEARBOX_PSIG, "LOG_PERF_P_GEARBOX_PSIG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ETC1_DEGF, "LOG_PERF_T_ETC1_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ETC2_DEGF, "LOG_PERF_T_ETC2_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ETC_DELTA_DEGF, "LOG_PERF_T_ETC_DELTA_DEGF"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGTREQ_DEGF, "LOG_PERF_EGTREQ_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_EGT_AVG_DEGF, "LOG_PERF_T_EGT_AVG_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGTCOR1, "LOG_PERF_EGTCOR1"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGTCR1, "LOG_PERF_EGTCR1"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGTCOR, "LOG_PERF_EGTCOR"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_EXHAUST_PSIA, "LOG_PERF_P_EXHAUST_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_ORIFICE_PSIA, "LOG_PERF_P_ORIFICE_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ORIFICE_DEGF, "LOG_PERF_T_ORIFICE_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_ORIFICE_DELTA_PSID, "LOG_PERF_P_ORIFICE_DELTA_PSID"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBDC, "LOG_PERF_WBDC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBDCR1, "LOG_PERF_WBDCR1"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DWBDCOR, "LOG_PERF_DWBDCOR"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB, "LOG_PERF_WB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCOR1, "LOG_PERF_WBCOR1"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCOR, "LOG_PERF_WBCOR"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_CALC, "LOG_PERF_WB_CALC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_MEASURED, "LOG_PERF_WB_MEASURED"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_BLEED1_5_PSIA, "LOG_PERF_P_BLEED1_5_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PBCOR, "LOG_PERF_PBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PBCOR1, "LOG_PERF_PBCOR1"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_BLEED_AVG_DEGF, "LOG_PERF_T_BLEED_AVG_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_TBCOR1, "LOG_PERF_TBCOR1"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_TBCOR, "LOG_PERF_TBCOR"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_AVG_PPH, "LOG_PERF_WF_AVG_PPH"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WFCOR1, "LOG_PERF_WFCOR1"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WFCOR, "LOG_PERF_WFCOR"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_SHP, "LOG_PERF_SHP"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_SHPCOR, "LOG_PERF_SHPCOR"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_NG, "LOG_PERF_NG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_GEARBOX, "LOG_PERF_VIB_GEARBOX"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_TURBINE, "LOG_PERF_VIB_TURBINE"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELEGT, "LOG_PERF_DELEGT"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELWF, "LOG_PERF_DELWF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELTB, "LOG_PERF_DELTB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELPB, "LOG_PERF_DELPB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELWB, "LOG_PERF_DELWB"))

                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_ECS_WBCOR1, "Log_Perf_ECS_WBCOR1"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_ECS_PBCOR, "Log_Perf_ECS_PBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_ECS_SHPCOR, "Log_Perf_ECS_SHPCOR"))

                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_MES_TBCOR, "Log_Perf_MES_TBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_MES_PBCOR, "Log_Perf_MES_PBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_MES_SHPCOR, "Log_Perf_MES_SHPCOR"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_MES_EGTCOR, "Log_Perf_MES_EGTCOR"))

                Return_Value.Add(MyBase.InitializeObject(Test_Step_Frame, TestStepFrameControlName))
                Return_Value.Add(MyBase.InitializeObject(Ctrl_System_Frame, ControlSystemFrameName))


                cmbTestStepList = Test_Step_Frame.FindObject("TestStep_List")

                If cmbTestStepList Is Nothing Then
                    MessageBox.Show("MyBase.InitializeObject - TestStep_List Is Nothing")
                    Return_Value.Add(False)
                Else
                    Return_Value.Add(True)
                End If

            Catch Ex As Exception
                MessageBox.Show("Init_Engine_Channels: " & Ex.ToString())
                Return_Value.Add(False)
            Finally

            End Try

            If Return_Value.Contains(False) Then
                Init_Engine_Channels = False
            Else
                Init_Engine_Channels = True
            End If

        End Function


        '********************************************************************
        '* Private Members **************************************************
        '********************************************************************

        Private EngineNameAsPrefix As String
        Private PerformanceScreenName As String
        Private BaseScreenName As String
        Private MainScreenName As String
        Private LoadBankFrameControlName As String
        Private TestStepFrameControlName As String
        Private ControlSystemFrameName As String

        '************
        '* Channels *
        '************

        Private BLEED_BTN As IBooleanChannel
        Private DELEGT As INumericChannel
        Private DELPB As INumericChannel
        Private DELTB As INumericChannel
        Private DELWB As INumericChannel
        Private DELWF As INumericChannel
        Private EGTREQ_DEGF As INumericChannel
        Private FLHV As INumericChannel
        Private FM_INITIAL_VOLUME_LITERS As INumericChannel
        Private FM_TOTAL_VOLUME_LITERS As INumericChannel
        Private FUEL_USED_INITIAL_LITERS As INumericChannel
        Private FUEL_USED_TOTAL_LITERS As INumericChannel
        Private APU_DRY_WEIGHT As INumericChannel
        'Private HONEYWELL_COMPUTER_SELECTION_BTN As IBooleanChannel

        Private INLET_FLAP_CLOSE_CMD As IBooleanChannel
        Private INLET_FLAP_OPEN_CMD As IBooleanChannel
        Private INLET_FLAP_CLOSED_IND As IBooleanChannel
        Private INLET_FLAP_MOVING_IND As IBooleanChannel
        Private INLET_FLAP_OPENED_IND As IBooleanChannel

        Private LOAD_AIR_VALVE_DEMAND As INumericChannel

        Private NUMBER_OF_STARTS As INumericChannel
        Private OPERATING_TIME_CURRENT_SEC As INumericChannel
        Private OPERATING_TIME_CURRENT_HOURS As INumericChannel

        Private P_ORIFICE_DELTA_PSID As INumericChannel
        Private P_ORIFICE_PSIA As INumericChannel

        Private PBCOR_MEAN As INumericChannel

        Private PERF_TEST_RESULT_ACCEPTED As IBooleanChannel
        Private PERF_TEST_RESULT_REJECTED As IBooleanChannel
        Private PERF_NEW_TEST_OPC As IBooleanChannel

        Private READY_TO_LOAD_IND As IBooleanChannel

        Private STARTING_TIME_SEC As INumericChannel
        Private T_ORIFICE_DEGF As INumericChannel
        Private T_INLET_AVG_DEGF As INumericChannel

        Private VIB_GEARBOX_MAX_IPS_PEAK_PERSISTENT As INumericChannel
        Private VIB_TURBINE_MAX_IPS_PEAK_PERSISTENT As INumericChannel

        Private WB_CALC As INumericChannel
        Private WB_MEASURED As INumericChannel
        Private WF_AVG_PPH As INumericChannel
        Private WF_TOT1 As INumericChannel
        Private WF_TOT2 As INumericChannel

        Private EGTCOR1 As INumericChannel
        Private EGTCOR As INumericChannel
        Private EGTCR1 As INumericChannel
        Private PBCOR1 As INumericChannel
        Private PBCOR As INumericChannel
        Private TBCOR1 As INumericChannel
        Private TBCOR As INumericChannel
        Private WBCOR1 As INumericChannel
        Private WBCOR As INumericChannel
        Private WBDCR1 As INumericChannel
        Private DWBDCOR As INumericChannel
        Private WFCOR1 As INumericChannel
        Private WFCOR As INumericChannel
        Private WBDC As INumericChannel
        Private SHPCOR As INumericChannel


        ' ******************
        ' Log Performance
        ' ******************

        Private LOG_PERF_OPERATING_TIME_CURRENT_HOURS As INumericChannel
        Private LOG_PERF_OPERATING_TIME_CURRENT_MINUTES As INumericChannel

        Private LOG_PERF_P_BARO_PSIA As INumericChannel
        Private LOG_PERF_T_INLET_AVG_DEGF As INumericChannel
        Private LOG_PERF_P_INLET_PSIA As INumericChannel
        Private LOG_PERF_P_OIL_PSIG As INumericChannel
        Private LOG_PERF_T_OIL_DEGF As INumericChannel
        Private LOG_PERF_P_GEARBOX_INH2O As INumericChannel
        Private LOG_PERF_P_GEARBOX_PSIG As INumericChannel
        Private LOG_PERF_T_ETC1_DEGF As INumericChannel
        Private LOG_PERF_T_ETC2_DEGF As INumericChannel
        Private LOG_PERF_T_ETC_DELTA_DEGF As INumericChannel

        Private LOG_PERF_EGTREQ_DEGF As INumericChannel
        Private LOG_PERF_T_EGT_AVG_DEGF As INumericChannel
        Private LOG_PERF_EGTCOR1 As INumericChannel
        Private LOG_PERF_EGTCR1 As INumericChannel
        Private LOG_PERF_EGTCOR As INumericChannel

        Private LOG_PERF_P_EXHAUST_PSIA As INumericChannel
        Private LOG_PERF_P_ORIFICE_PSIA As INumericChannel
        Private LOG_PERF_T_ORIFICE_DEGF As INumericChannel
        Private LOG_PERF_P_ORIFICE_DELTA_PSID As INumericChannel

        Private LOG_PERF_WB As INumericChannel
        Private LOG_PERF_WBDC As INumericChannel
        Private LOG_PERF_WBDCR1 As INumericChannel
        Private LOG_PERF_DWBDCOR As INumericChannel

        Private LOG_PERF_WBCOR1 As INumericChannel
        Private LOG_PERF_WBCOR As INumericChannel

        Private LOG_PERF_WB_CALC As INumericChannel
        Private LOG_PERF_WB_MEASURED As INumericChannel

        Private LOG_PERF_P_BLEED1_5_PSIA As INumericChannel
        Private LOG_PERF_PBCOR As INumericChannel
        Private LOG_PERF_PBCOR1 As INumericChannel

        Private LOG_PERF_T_BLEED_AVG_DEGF As INumericChannel
        Private LOG_PERF_TBCOR1 As INumericChannel
        Private LOG_PERF_TBCOR As INumericChannel

        Private LOG_PERF_WF_AVG_PPH As INumericChannel
        Private LOG_PERF_WFCOR1 As INumericChannel
        Private LOG_PERF_WFCOR As INumericChannel

        Private LOG_PERF_SHP As INumericChannel
        Private LOG_PERF_SHPCOR As INumericChannel

        Private LOG_PERF_NG As INumericChannel
        Private LOG_PERF_VIB_GEARBOX As INumericChannel
        Private LOG_PERF_VIB_TURBINE As INumericChannel

        Private LOG_PERF_DELEGT As INumericChannel
        Private LOG_PERF_DELWF As INumericChannel
        Private LOG_PERF_DELTB As INumericChannel
        Private LOG_PERF_DELPB As INumericChannel
        Private LOG_PERF_DELWB As INumericChannel


        '*****************************
        '* Log & Performance reports *
        '*****************************

        Private Log_Perf_CaptureECS As IBooleanChannel              ' Button for capturing performance information for No Load
        Private Log_Perf_CaptureMES As IBooleanChannel              ' Button for capturing performance information for ECS
        Private Log_Perf_CaptureNoLoad As IBooleanChannel           ' Button for capturing performance information for MES

        Private Log_Perf_ECS_WBCOR1 As INumericChannel
        Private Log_Perf_ECS_PBCOR As INumericChannel
        Private Log_Perf_ECS_SHPCOR As INumericChannel

        Private Log_Perf_MES_TBCOR As INumericChannel
        Private Log_Perf_MES_PBCOR As INumericChannel
        Private Log_Perf_MES_SHPCOR As INumericChannel
        Private Log_Perf_MES_EGTCOR As INumericChannel

        Private Log_Perf_NbApuFaults As INumericChannel


        '*********************
        '* Forms and Control *
        '*********************
        Private Ctrl_System_Frame As KineticaRT.Controls.Control
        Private Test_Step_Frame As KineticaRT.Controls.Control
        Private cmbTestStepList As System.Windows.Forms.ComboBox


        '********
        '* Misc *
        '********
        Private cnMain As ADODB.Connection
        Private Custom_Parameters As New System.Collections.Specialized.OrderedDictionary()


        '**********
        '* Thread *
        '**********
        Private PersistentValuesCtrl As PersistentValuesController = Nothing

        'Arrays from access table in test session
        Private RE220_ECS(,) As Double                                  'Used to keep a database table in memory for fast access for RE220
        Private RE220_MES(,) As Double                                  'Used to keep a database table in memory for fast access for RE220
        Private RE220_EGT(,) As Double                                  'Used to keep a database table in memory for fast access for RE220

        Private StarterMaxCurrentRecordCtrl As MinMaxRecordController = Nothing
        Private StarterMaxVoltageRecordCtrl As MinMaxRecordController = Nothing

        Private DeletableTBleedChCtrl As DeletableChannelController = Nothing
        Private DeletableTEgtChCtrl As DeletableChannelController = Nothing
        Private DeletableTInletChCtrl As DeletableChannelController = Nothing
        Private FlowmeterCtrl As FlowmeterController = Nothing

        Private LoadBankCtrl As LoadBankController = Nothing
        Private StartTimerCtrl As TimerController = Nothing
        Private OilCoolingCtrl As UpDownDemandController = Nothing

        Private UserInputsS7 As New UserInputS7_RE220()

    End Class


    Public Class UserInputS7_RE220

        'Question
        Public SerialNumber As String = String.Empty
        Public WorkOrder As String = String.Empty
        Public PartNumber As String = String.Empty
        Public LHV As String = String.Empty
        Public FuelSG As String = String.Empty
        Public TFuelDegF As String = String.Empty
        Public OilType As String = String.Empty
        Public FuelType As String = String.Empty
        Public CustomerName As String = String.Empty
        Public TurbineVibPickupSerialNumber As String = String.Empty
        Public GearboxVibPickupSerialNumber As String = String.Empty

        'Choice
        Public ChoiceTestType As String = String.Empty
        Public ChoiceOperator As String = String.Empty
        Public ChoiceInspector As String = String.Empty
        Public ChoiceRepairType As String = String.Empty


        ' Allow friend access to the empty constructor.
        Friend Sub New()
        End Sub

        Public Sub MapUserInputsParameters(ByRef CELProject As CELDAS)

            'Question
            SerialNumber = CELProject.Test_Mgr_UUT_Answer(0).Value.ToString()
            WorkOrder = CELProject.Test_Mgr_UUT_Answer(1).Value.ToString()
            PartNumber = CELProject.Test_Mgr_UUT_Answer(2).Value.ToString()
            LHV = CELProject.Test_Mgr_UUT_Answer(3).Value.ToString()
            FuelSG = CELProject.Test_Mgr_UUT_Answer(4).Value.ToString()
            TFuelDegF = CELProject.Test_Mgr_UUT_Answer(5).Value.ToString()
            OilType = CELProject.Test_Mgr_UUT_Answer(6).Value.ToString()
            FuelType = CELProject.Test_Mgr_UUT_Answer(7).Value.ToString()
            CustomerName = CELProject.Test_Mgr_UUT_Answer(8).Value.ToString()
            TurbineVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(9).Value.ToString()
            GearboxVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(10).Value.ToString()

            'Choice
            ChoiceTestType = CELProject.Test_Mgr_UUT_Choice(0).Value.ToString()
            ChoiceRepairType = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()
            ChoiceOperator = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()
            ChoiceInspector = CELProject.Test_Mgr_UUT_Choice(3).Value.ToString()
        End Sub

    End Class

End Namespace

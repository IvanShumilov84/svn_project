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

	Public Class GTCP131_9A_Sequence
        Inherits ApuSequence

        Private Const MaxNumberOfTBleedProbe As Integer = 4
		Private Const MaxNumberOfTEgtProbe As Integer = 12
		Private Const MaxNumberOfTInletProbe As Integer = 12


		Private Enum eTestStepGTCP131_9A
			TCOND1 = 0	' RTL
			TCOND2 = 1	' GEN LOAD
			IGV_OFF = 2	' IGV Offset
			TCOND3 = 3	' 2 PCKS-700 ECS HIGH
			TCOND4 = 4	' MES
			TCOND5 = 5	' FLOW SENSOR TEST STEP 1
			TCOND6 = 6	' FLOW SENSOR TEST STEP 2
			TCOND7 = 7	' MINIMUM SURGE MARGIN TEST
			FINAL_DATA = 8
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

                    Compute_GTCP131_9A_Perf(LOG_PERF_T_INLET_AVG_DEGF, _
                                            LOG_PERF_DELWB, _
                                            LOG_PERF_DELPB, _
                                            LOG_PERF_DELTB, _
                                            LOG_PERF_DELEGT, _
                                            LOG_PERF_DELWF, _
                                            LOG_PERF_EGTREQ_DEGF, _
                                            LOG_PERF_PBREQ_PSIA, _
                                            LOG_PERF_WBREQ_PPM, _
                                            LOG_PERF_WFREQ_PPH, _
                                            LOG_PERF_WBCDNA_LO_LIM, _
                                            LOG_PERF_WBCDNA_HI_LIM)

                    FlowmeterCtrl.ComputeFlow(True)

                    'IMPORTANT: Toutes les variables flottantes telles que WB_CALC, DELPB, LB_DEMAND dont la source et la destination sont vides doivent
                    'manuellement être updaté pour être inscrite dans le LOG ou le PERF report.
                Else

                    If EngineConfiguration.UseMeasuredBleedFlowValue Then
                        WB_CALC.Value = WB_MEASURED.Value
                    Else
                        WB_CALC.Value = New KineticaRT.Values.Double(Compute_BleedAirFlow(T_ORIFICE_DEGF.Value.ToDouble(),
                                                                                          P_ORIFICE_PSIA.Value.ToDouble(),
                                                                                          P_ORIFICE_DELTA_PSID.Value.ToDouble))
                    End If

                    Compute_GTCP131_9A_Perf(T_INLET_AVG_DEGF, _
                                          DELWB, _
                                          DELPB, _
                                          DELTB, _
                                          DELEGT, _
                                          DELWF, _
                                          EGTREQ_DEGF, _
                                          PBREQ_PSIA, _
                                          WBREQ_PPM, _
                                          WFREQ_PPH, _
                                          WBCDNA_LO_LIM, _
                                          WBCDNA_HI_LIM)

                End If

            Catch ex As Exception
                MessageBox.Show("Compute_GTCP131_9A" + ex.ToString(), "Error")
            End Try

        End Sub

	    Private Sub Compute_GTCP131_9A_Perf(ByRef tinlet_degf_avg As IChannel, _
                                            ByRef del_wb As IChannel, _
                                            ByRef del_pb As IChannel, _
                                            ByRef del_tb As IChannel, _
                                            ByRef del_egt As IChannel, _
                                            ByRef del_wf As IChannel, _
                                            ByRef egt_req As IChannel, _
                                            ByRef pb_req As IChannel, _
                                            ByRef wb_req As IChannel, _
                                            ByRef wf_req As IChannel, _
                                            ByRef wbcdna_lo_lim As IChannel, _
                                            ByRef wbcdna_hi_lim As IChannel)

            Try

                Select Case CELProject.PERF_TEST_STEP_NB.Value.ToDouble()

                    Case eTestStepGTCP131_9A.TCOND1, eTestStepGTCP131_9A.TCOND2, eTestStepGTCP131_9A.FINAL_DATA
                        del_wb.Value = Values.NaN
                        del_pb.Value = Values.NaN
                        del_tb.Value = Values.NaN
                        del_egt.Value = Values.NaN
                        del_wf.Value = Values.NaN
                        egt_req.Value = Values.NaN
                        pb_req.Value = Values.NaN
                        wb_req.Value = Values.NaN
                        wf_req.Value = Values.NaN

                    Case eTestStepGTCP131_9A.IGV_OFF, eTestStepGTCP131_9A.TCOND3, eTestStepGTCP131_9A.TCOND5, eTestStepGTCP131_9A.TCOND6, eTestStepGTCP131_9A.TCOND7
                        'ECS
                        del_wb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_ECS, 1)
                        del_pb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_ECS, 2)
                        del_tb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_ECS, 3)
                        del_egt.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_ECS, 4)
                        del_wf.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_ECS, 5)

                        If HeavyRepairType = True Then 'Table 1311 (page 1330)
                            egt_req.Value = New KineticaRT.Values.Double(1130.0)
                            pb_req.Value = New KineticaRT.Values.Double(51.2)
                            wb_req.Value = New KineticaRT.Values.Double(152.2)
                            wf_req.Value = Values.NaN
                        Else
                            egt_req.Value = New KineticaRT.Values.Double(1150.0)
                            pb_req.Value = New KineticaRT.Values.Double(50.2)
                            wb_req.Value = New KineticaRT.Values.Double(148.9)
                            wf_req.Value = Values.NaN
                        End If

                    Case eTestStepGTCP131_9A.TCOND4
                        'MES
                        del_wb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_MES, 1)
                        del_pb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_MES, 2)
                        del_tb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_MES, 3)
                        del_egt.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_MES, 4)
                        del_wf.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_MES, 5)

                        If HeavyRepairType = True Then 'Table 1312 (page 1331)
                            egt_req.Value = New KineticaRT.Values.Double(1125.0)
                            pb_req.Value = New KineticaRT.Values.Double(51.1)
                            wb_req.Value = Values.NaN
                            wf_req.Value = New KineticaRT.Values.Double(269.0)
                        Else
                            egt_req.Value = New KineticaRT.Values.Double(1145.0)
                            pb_req.Value = New KineticaRT.Values.Double(50.1)
                            wb_req.Value = Values.NaN
                            wf_req.Value = New KineticaRT.Values.Double(269.0)
                        End If

                End Select

                Select Case CELProject.PERF_TEST_STEP_NB.Value.ToDouble()

                    Case eTestStepGTCP131_9A.TCOND1, eTestStepGTCP131_9A.TCOND2, eTestStepGTCP131_9A.FINAL_DATA
                        wbcdna_lo_lim.Value = New KineticaRT.Values.Double(System.Double.NegativeInfinity)
                        wbcdna_hi_lim.Value = New KineticaRT.Values.Double(System.Double.PositiveInfinity)

                    Case eTestStepGTCP131_9A.IGV_OFF, eTestStepGTCP131_9A.TCOND3
                        ' Table 1305 (p1309)
                        wbcdna_lo_lim.Value = New KineticaRT.Values.Double(56.1)
                        wbcdna_hi_lim.Value = New KineticaRT.Values.Double(58.1)

                    Case eTestStepGTCP131_9A.TCOND4
                        ' Table 1305 (p1309)
                        wbcdna_lo_lim.Value = New KineticaRT.Values.Double(52.0)
                        wbcdna_hi_lim.Value = New KineticaRT.Values.Double(54.0)

                    Case eTestStepGTCP131_9A.TCOND5, eTestStepGTCP131_9A.TCOND6
                        wbcdna_lo_lim.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_WBCDNA_LIMITS, 1)
                        wbcdna_hi_lim.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9A_WBCDNA_LIMITS, 2)

                    Case eTestStepGTCP131_9A.TCOND7
                        ' TODO Ici on devrait peut-être ajouter une interpolation comme dans les conditions 5 et 6, 
                        ' mais en utilisant la figure 1304 (p1334), tel que décrit dans le manuel à la page 1335 (8.G.(3)).
                        ' En attendant une décision, je vais maintenir les limites à l'infini.
                        wbcdna_lo_lim.Value = New KineticaRT.Values.Double(System.Double.NegativeInfinity)
                        wbcdna_hi_lim.Value = New KineticaRT.Values.Double(System.Double.PositiveInfinity)


                End Select

            Catch Ex As Exception
                MessageBox.Show("Compute_GTCP131_9A_Perf: " & Ex.ToString())
            End Try

        End Sub

       Private Sub ValidateFMInitialValue()
            Try

                Select Case CELProject.Project.Number

                    Case 12917
                        'Read the mass flow meter value, the value cannot be 0
                        If FM_INITIAL_VOLUME_LITERS.Value.ToDouble() = 0.0 Then
                            FM_INITIAL_VOLUME_LITERS.Value = New KineticaRT.Values.Double(FM_TOTAL_VOLUME_LITERS.Value.ToDouble())
                        End If

                    Case 11813, 12958, 13015
                        FM_INITIAL_VOLUME_LITERS.Value = Values.Zero

                    Case 17865
                        FM_INITIAL_VOLUME_LITERS.Value = Values.Zero

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

                If CELProject.IsTrue(INLET_FLAP_OPEN_REQUEST_IND.Value) Then
                    If (FlapPosition < 3000) Then
                        FlapPosition = FlapPosition + 100
                    Else
                        FlapPosition = 3000
                    End If

                ElseIf CELProject.IsTrue(INLET_FLAP_CLOSE_REQUEST_IND.Value) Then
                    If (FlapPosition > 0) Then
                        FlapPosition = FlapPosition - 100
                    Else
                        FlapPosition = 0
                    End If
                End If


                If FlapPosition = 0 Then ' (* 0 is close position*)
                    If CELProject.IsFalse(INLET_FLAP_CLOSE_CMD.Value) Then
                        INLET_FLAP_CLOSE_CMD.Value = Values.True
                    End If
                Else
                    If CELProject.IsTrue(INLET_FLAP_CLOSE_CMD.Value) Then
                        INLET_FLAP_CLOSE_CMD.Value = Values.False
                    End If
                End If

                If FlapPosition = 3000 Then '(* 3000 is open position*)
                    If CELProject.IsFalse(INLET_FLAP_OPEN_CMD.Value) Then
                        INLET_FLAP_OPEN_CMD.Value = Values.True
                    End If
                Else
                    If CELProject.IsTrue(INLET_FLAP_OPEN_CMD.Value) Then
                        INLET_FLAP_OPEN_CMD.Value = Values.False
                    End If
                End If

                If (FlapPosition <> 0) And (FlapPosition <> 3000) Then
                    If CELProject.IsFalse(INLET_FLAP_MOVE_CMD.Value) Then
                        INLET_FLAP_MOVE_CMD.Value = Values.True
                    End If
                Else
                    If CELProject.IsTrue(INLET_FLAP_MOVE_CMD.Value) Then
                        INLET_FLAP_MOVE_CMD.Value = Values.False
                    End If

                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Sub RevimaWriteCommonDataForSheet2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("DETAILS 1")

                excelWorksheet.Cells(19, Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing)
                excelWorksheet.Cells(20, Column).Value = LOG_PERF_P_INLET_PSIA.Value.ToString(LOG_PERF_P_INLET_PSIA.Format, Nothing)
                excelWorksheet.Cells(21, Column).Value = LOG_PERF_T_INLET_AVG_DEGF.Value.ToString(LOG_PERF_T_INLET_AVG_DEGF.Format, Nothing)
                excelWorksheet.Cells(22, Column).Value = LOG_PERF_LCIT_DEGF.Value.ToString(LOG_PERF_LCIT_DEGF.Format, Nothing)
                excelWorksheet.Cells(23, Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing)
                excelWorksheet.Cells(24, Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing)
                excelWorksheet.Cells(25, Column).Value = LOG_PERF_T_FUEL_DEGF.Value.ToString(LOG_PERF_T_FUEL_DEGF.Format, Nothing)
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_PSIG.Format, Nothing)
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing)
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing)
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_VIB_COOL_FAN.Value.ToString(LOG_PERF_VIB_COOL_FAN.Format, Nothing)
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing)
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing)
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_PSIA.Format, Nothing)
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_T_LCDT_DEGF.Value.ToString(LOG_PERF_T_LCDT_DEGF.Format, Nothing)
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_T_ETC1_DEGF.Value.ToString(LOG_PERF_T_ETC1_DEGF.Format, Nothing)
                excelWorksheet.Cells(35, Column).Value = LOG_PERF_T_ETC2_DEGF.Value.ToString(LOG_PERF_T_ETC2_DEGF.Format, Nothing)
                excelWorksheet.Cells(36, Column).Value = LOG_PERF_T_EGT_AVG_DEGF.Value.ToString(LOG_PERF_T_EGT_AVG_DEGF.Format, Nothing)
                excelWorksheet.Cells(37, Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing)
                excelWorksheet.Cells(45, Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing)

            Catch Ex As Exception
                MessageBox.Show("RevimaWriteCommonDataForSheet2: " & Ex.ToString())
            End Try
        End Sub

        Private Sub RevimaWriteCommon2DataForSheet2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("DETAILS 1")

                excelWorksheet.Cells(38, Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)
                excelWorksheet.Cells(39, Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)
                excelWorksheet.Cells(40, Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing)
                excelWorksheet.Cells(41, Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)
                excelWorksheet.Cells(42, Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                excelWorksheet.Cells(43, Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)
                excelWorksheet.Cells(44, Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)
                excelWorksheet.Cells(46, Column).Value = LOG_PERF_LB1_POWER_TOT.Value.ToString(LOG_PERF_LB1_POWER_TOT.Format, Nothing)

                excelWorksheet = excelSheets.Item("DETAILS 2")

                excelWorksheet.Cells(19, Column).Value = LOG_PERF_SHP_SL.Value.ToString(LOG_PERF_SHP_SL.Format, Nothing)
                excelWorksheet.Cells(21, Column).Value = LOG_PERF_DELTA_P_SL.Value.ToString(LOG_PERF_DELTA_P_SL.Format, Nothing)
                excelWorksheet.Cells(23, Column).Value = LOG_PERF_PB_SL_PSIA.Value.ToString(LOG_PERF_PB_SL_PSIA.Format, Nothing)
                excelWorksheet.Cells(25, Column).Value = LOG_PERF_DELPB.Value.ToString(LOG_PERF_DELPB.Format, Nothing)
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_PB_INST_PSIA.Value.ToString(LOG_PERF_PB_INST_PSIA.Format, Nothing)
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_WB_SL.Value.ToString(LOG_PERF_WB_SL.Format, Nothing)
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_DELWB.Value.ToString(LOG_PERF_DELWB.Format, Nothing)
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_WB_INST.Value.ToString(LOG_PERF_WB_INST.Format, Nothing)
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                excelWorksheet.Cells(35, Column).Value = LOG_PERF_DELTB.Value.ToString(LOG_PERF_DELTB.Format, Nothing)
                excelWorksheet.Cells(36, Column).Value = LOG_PERF_TBCOR_EXC_PB.Value.ToString(LOG_PERF_TBCOR_EXC_PB.Format, Nothing)
                excelWorksheet.Cells(38, Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                excelWorksheet.Cells(40, Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)
                excelWorksheet.Cells(41, Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing)
                excelWorksheet.Cells(43, Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)
                excelWorksheet.Cells(44, Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)
                excelWorksheet.Cells(46, Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                excelWorksheet.Cells(48, Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)
                excelWorksheet.Cells(50, Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)
                excelWorksheet.Cells(51, Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)
                excelWorksheet.Cells(53, Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)
                excelWorksheet.Cells(54, Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing)
                excelWorksheet.Cells(56, Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

            Catch Ex As Exception
                MessageBox.Show("RevimaWriteCommon2DataForSheet2: " & Ex.ToString())
            End Try
        End Sub

        Private Function CreateRevima12917PerformanceFile(ByVal AppendOption As Boolean, ByVal TestStepNumber As Integer, ByVal ProtectPassword As String, ByVal UnProtectPassword As String, ByRef excelWorkbook As Excel.Workbook) As Boolean
            'Declaration
            Dim excelSheets As Excel.Sheets
            Dim excelWorksheet As Excel.Worksheet

            CreateRevima12917PerformanceFile = True

            ' The following gets the Worksheets collection
            excelSheets = excelWorkbook.Worksheets

            Try
                excelWorkbook.Worksheets(1).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(2).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(3).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(4).Unprotect(UnProtectPassword)

                If AppendOption = False Then

                    excelWorksheet = excelSheets.Item("SUMMARY")

                    'APU Type, cellule E6 : text directly in the excell sheet

                    'Engine S/N
                    excelWorksheet.Cells(5, 28).NumberFormat = "@"
                    excelWorksheet.Cells(5, 28).Value = UserInputsRevima.EngineSerialNumber

                    'Work Order
                    excelWorksheet.Cells(6, 28).NumberFormat = "@"
                    excelWorksheet.Cells(6, 28).Value = UserInputsRevima.WorkOrder

                    'ECB S/N
                    excelWorksheet.Cells(7, 28).NumberFormat = "@"
                    excelWorksheet.Cells(7, 28).Value = UserInputsRevima.ECBSerialNumber

                    'Exhaust Duct S/N
                    excelWorksheet.Cells(8, 28).NumberFormat = "@"
                    excelWorksheet.Cells(8, 28).Value = UserInputsRevima.ExhaustDuctSerialNumber

                    'Bellmouth S/N
                    excelWorksheet.Cells(9, 28).NumberFormat = "@"
                    excelWorksheet.Cells(9, 28).Value = UserInputsRevima.BellmouthSerialNumber

                    'Bleed Duct S/N
                    excelWorksheet.Cells(10, 28).NumberFormat = "@"
                    excelWorksheet.Cells(10, 28).Value = UserInputsRevima.BleedDuctSerialNumber

                    'Starter S/N
                    excelWorksheet.Cells(11, 28).NumberFormat = "@"
                    excelWorksheet.Cells(11, 28).Value = UserInputsRevima.StarterSerialNumber

                    'Turbine Vib S/N
                    excelWorksheet.Cells(12, 28).NumberFormat = "@"
                    excelWorksheet.Cells(12, 28).Value = UserInputsRevima.TurbineVibPickupSerialNumber

                    'Gearbox Vib S/N
                    excelWorksheet.Cells(5, 96).NumberFormat = "@"
                    excelWorksheet.Cells(5, 96).Value = UserInputsRevima.GearboxVibPickupSerialNumber

                    'Cooling Fan Vib S/N
                    excelWorksheet.Cells(6, 96).NumberFormat = "@"
                    excelWorksheet.Cells(6, 96).Value = UserInputsRevima.FanVibPickupSerialNumber

                    'Oil Type
                    excelWorksheet.Cells(7, 96).NumberFormat = "@"
                    excelWorksheet.Cells(7, 96).Value = UserInputsRevima.OilType

                    'TSN
                    excelWorksheet.Cells(8, 96).NumberFormat = "@"
                    excelWorksheet.Cells(8, 96).Value = UserInputsRevima.TimeSinceNewOrOverhaul

                    'CSN
                    excelWorksheet.Cells(9, 96).NumberFormat = "@"
                    excelWorksheet.Cells(9, 96).Value = UserInputsRevima.CyclesSinceNewOrOverhaul

                    'Reason
                    excelWorksheet.Cells(10, 96).NumberFormat = "@"
                    excelWorksheet.Cells(10, 96).Value = UserInputsRevima.Reason

                    'Test Type
                    excelWorksheet.Cells(11, 96).NumberFormat = "@"
                    excelWorksheet.Cells(11, 96).Value = UserInputsRevima.TestType

                    'Repair Type
                    excelWorksheet.Cells(12, 96).NumberFormat = "@"
                    excelWorksheet.Cells(12, 96).Value = UserInputsRevima.RepairType

                    'Date
                    excelWorksheet.Cells(13, 96).NumberFormat = "@"
                    excelWorksheet.Cells(13, 96).Value = Format(Now, "d MMMM yyyy")

                    excelWorksheet = excelSheets.Item("ACCEPTANCE")

                    'Document Number
                    excelWorksheet.Cells(41, 5).NumberFormat = "@"
                    excelWorksheet.Cells(41, 5).Value = excelWorksheet.Cells(41, 5).Value.ToString() + Custom_Parameters("APUTestedAccordingTo").ToString()
                End If

                Select Case TestStepNumber

                    Case eTestStepGTCP131_9A.TCOND1

                        RevimaWriteCommonDataForSheet2(excelSheets, 109) 'RTL

                    Case eTestStepGTCP131_9A.TCOND2

                        RevimaWriteCommonDataForSheet2(excelSheets, 123) 'SHAFT

                        excelWorksheet = excelSheets.Item("DETAILS 1")
                        excelWorksheet.Cells(46, 123).Value = LOG_PERF_LB1_POWER_TOT.Value.ToString(LOG_PERF_LB1_POWER_TOT.Format, Nothing)

                        excelWorksheet = excelSheets.Item("DETAILS 2")
                        excelWorksheet.Cells(19, 123).Value = LOG_PERF_SHP_SL.Value.ToString(LOG_PERF_SHP_SL.Format, Nothing)

                    Case eTestStepGTCP131_9A.IGV_OFF

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' ECS OFFSET WORKSHEET section
                        excelWorksheet.Cells(33, 116).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)
                        excelWorksheet.Cells(34, 116).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)
                        excelWorksheet.Cells(35, 116).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)
                        excelWorksheet.Cells(36, 116).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)
                        excelWorksheet.Cells(37, 116).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND3

                        RevimaWriteCommonDataForSheet2(excelSheets, 81) 'ECS
                        RevimaWriteCommon2DataForSheet2(excelSheets, 81)    'ECS

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        'PBCOR, WBCOR and EGTCOR limits
                        If (UserInputsRevima.RepairType.ToUpper() = "HEAVY") Then
                            excelWorksheet.Cells(20, 53).NumberFormat = "@"
                            excelWorksheet.Cells(20, 53).Value = "51.2 MINIMUM"
                            excelWorksheet.Cells(22, 53).NumberFormat = "@"
                            excelWorksheet.Cells(22, 53).Value = "152.2 MINIMUM"
                            excelWorksheet.Cells(24, 53).NumberFormat = "@"
                            excelWorksheet.Cells(24, 53).Value = "1125 MAXIMUM"
                        Else
                            excelWorksheet.Cells(20, 53).NumberFormat = "@"
                            excelWorksheet.Cells(20, 53).Value = "50.2 MINIMUM"
                            excelWorksheet.Cells(22, 53).NumberFormat = "@"
                            excelWorksheet.Cells(22, 53).Value = "148.9 MINIMUM"
                            excelWorksheet.Cells(24, 53).NumberFormat = "@"
                            excelWorksheet.Cells(24, 53).Value = "1150 MAXIMUM"
                        End If

                        excelWorksheet.Cells(20, 74).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(22, 74).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(24, 74).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(26, 74).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND4

                        RevimaWriteCommonDataForSheet2(excelSheets, 95) 'MES
                        RevimaWriteCommon2DataForSheet2(excelSheets, 95) 'MES

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        'PBCOR and EGTCOR limits
                        If (UserInputsRevima.RepairType.ToUpper() = "HEAVY") Then
                            excelWorksheet.Cells(20, 95).NumberFormat = "@"
                            excelWorksheet.Cells(20, 95).Value = "51.1 MINIMUM"
                            excelWorksheet.Cells(24, 95).NumberFormat = "@"
                            excelWorksheet.Cells(24, 95).Value = "1120 MAXIMUM"
                        Else
                            excelWorksheet.Cells(20, 95).NumberFormat = "@"
                            excelWorksheet.Cells(20, 95).Value = "50.1 MINIMUM"
                            excelWorksheet.Cells(24, 95).NumberFormat = "@"
                            excelWorksheet.Cells(24, 95).Value = "1145 MAXIMUM"
                        End If

                        excelWorksheet.Cells(20, 116).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(22, 116).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(24, 116).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(26, 116).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND5

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        excelWorksheet.Cells(41, 95).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(42, 95).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(41, 116).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND6

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        excelWorksheet.Cells(43, 95).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(44, 95).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(43, 116).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                        excelWorksheet.Cells(45, 116).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND7

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                            excelWorksheet.Cells(49, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(49, 116).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9A.FINAL_DATA

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section

                        If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                            excelWorksheet.Cells(48, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(48, 116).Value = "FAILED"
                        End If

                        excelWorksheet.Cells(50, 116).Value = STARTING_TIME_SEC.Value.ToString(STARTING_TIME_SEC.Format, Nothing)
                        excelWorksheet.Cells(51, 116).Value = NUMBER_OF_STARTS.Value.ToString()
                        excelWorksheet.Cells(52, 116).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)
                        excelWorksheet.Cells(53, 116).Value = Log_Perf_NbApuFaults.Value.ToString()
                End Select

                'TestProgress_RecordPerfStep(TestStepNumber)

                'Test Result
                excelWorksheet = excelSheets.Item("ACCEPTANCE")

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

            Catch ex As Exception
                System.Diagnostics.Trace.WriteLine("CreatePerformanceFile: " + ex.ToString())
                MessageBox.Show(ex.ToString)
                CreateRevima12917PerformanceFile = False
            Finally
                'Lock Sheets
                excelWorkbook.Worksheets(1).Protect(ProtectPassword)
                excelWorkbook.Worksheets(2).Protect(ProtectPassword)
                excelWorkbook.Worksheets(3).Protect(ProtectPassword)
                excelWorkbook.Worksheets(4).Protect(ProtectPassword)

            End Try
            'End Select
        End Function

        Private Sub ChinaWriteCommonDataForSheet2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("DETAILS 1")

                excelWorksheet.Cells(20, Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing)
                excelWorksheet.Cells(21, Column).Value = LOG_PERF_P_INLET_PSIA.Value.ToString(LOG_PERF_P_INLET_PSIA.Format, Nothing)
                excelWorksheet.Cells(22, Column).Value = LOG_PERF_T_INLET_AVG_DEGF.Value.ToString(LOG_PERF_T_INLET_AVG_DEGF.Format, Nothing)
                excelWorksheet.Cells(23, Column).Value = LOG_PERF_LCIT_DEGF.Value.ToString(LOG_PERF_LCIT_DEGF.Format, Nothing)
                excelWorksheet.Cells(24, Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing)
                excelWorksheet.Cells(25, Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing)
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_T_FUEL_DEGF.Value.ToString(LOG_PERF_T_FUEL_DEGF.Format, Nothing)
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_PSIG.Format, Nothing)
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing)
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing)
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_VIB_COOL_FAN.Value.ToString(LOG_PERF_VIB_COOL_FAN.Format, Nothing)
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing)
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing)
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_ARINC_SCV_POS.Value.ToString(LOG_PERF_ARINC_SCV_POS.Format, Nothing)
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_PSIA.Format, Nothing)
                excelWorksheet.Cells(35, Column).Value = LOG_PERF_T_LCDT_DEGF.Value.ToString(LOG_PERF_T_LCDT_DEGF.Format, Nothing)
                excelWorksheet.Cells(36, Column).Value = LOG_PERF_T_ETC1_DEGF.Value.ToString(LOG_PERF_T_ETC1_DEGF.Format, Nothing)
                excelWorksheet.Cells(37, Column).Value = LOG_PERF_T_ETC2_DEGF.Value.ToString(LOG_PERF_T_ETC2_DEGF.Format, Nothing)
                excelWorksheet.Cells(38, Column).Value = LOG_PERF_T_EGT_AVG_DEGF.Value.ToString(LOG_PERF_T_EGT_AVG_DEGF.Format, Nothing)
                excelWorksheet.Cells(39, Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing)
                excelWorksheet.Cells(47, Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing)

            Catch Ex As Exception
                MessageBox.Show("ChinaWriteCommonDataForSheet2: " & Ex.ToString())
            End Try
        End Sub

      Private Sub ChinaWriteCommon2DataForSheet2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("DETAILS 1")

                excelWorksheet.Cells(40, Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)
                excelWorksheet.Cells(41, Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)
                excelWorksheet.Cells(42, Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing)
                excelWorksheet.Cells(43, Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)
                excelWorksheet.Cells(44, Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                excelWorksheet.Cells(45, Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)
                excelWorksheet.Cells(46, Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)
                excelWorksheet.Cells(48, Column).Value = LOG_PERF_LB1_POWER_TOT.Value.ToString(LOG_PERF_LB1_POWER_TOT.Format, Nothing)

                excelWorksheet = excelSheets.Item("DETAILS 2")

                excelWorksheet.Cells(20, Column).Value = LOG_PERF_SHP_SL.Value.ToString(LOG_PERF_SHP_SL.Format, Nothing)
                excelWorksheet.Cells(22, Column).Value = LOG_PERF_DELTA_P_SL.Value.ToString(LOG_PERF_DELTA_P_SL.Format, Nothing)
                excelWorksheet.Cells(24, Column).Value = LOG_PERF_PB_SL_PSIA.Value.ToString(LOG_PERF_PB_SL_PSIA.Format, Nothing)
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_DELPB.Value.ToString(LOG_PERF_DELPB.Format, Nothing)
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_PB_INST_PSIA.Value.ToString(LOG_PERF_PB_INST_PSIA.Format, Nothing)
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_WB_SL.Value.ToString(LOG_PERF_WB_SL.Format, Nothing)
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_DELWB.Value.ToString(LOG_PERF_DELWB.Format, Nothing)
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_WB_INST.Value.ToString(LOG_PERF_WB_INST.Format, Nothing)
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                excelWorksheet.Cells(36, Column).Value = LOG_PERF_DELTB.Value.ToString(LOG_PERF_DELTB.Format, Nothing)
                excelWorksheet.Cells(37, Column).Value = LOG_PERF_TBCOR_EXC_PB.Value.ToString(LOG_PERF_TBCOR_EXC_PB.Format, Nothing)
                excelWorksheet.Cells(39, Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                excelWorksheet.Cells(41, Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)
                excelWorksheet.Cells(42, Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing)
                excelWorksheet.Cells(44, Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)
                excelWorksheet.Cells(45, Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)
                excelWorksheet.Cells(47, Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                excelWorksheet.Cells(49, Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)
                excelWorksheet.Cells(51, Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)
                excelWorksheet.Cells(52, Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)
                excelWorksheet.Cells(54, Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)
                excelWorksheet.Cells(55, Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing)
                excelWorksheet.Cells(57, Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

            Catch Ex As Exception
                MessageBox.Show("ChinaWriteCommon2DataForSheet2: " & Ex.ToString())
            End Try
        End Sub

        Private Function CreateChina11813PerformanceFile(ByVal AppendOption As Boolean, ByVal TestStepNumber As Integer, ByVal ProtectPassword As String, ByVal UnProtectPassword As String, ByRef excelWorkbook As Excel.Workbook) As Boolean
            'Declaration
            Dim excelSheets As Excel.Sheets
            Dim excelWorksheet As Excel.Worksheet

            CreateChina11813PerformanceFile = True

            ' The following gets the Worksheets collection
            excelSheets = excelWorkbook.Worksheets

            Try
                excelWorkbook.Worksheets(1).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(2).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(3).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(4).Unprotect(UnProtectPassword)

                If AppendOption = False Then

                    excelWorksheet = excelSheets.Item("SUMMARY")

                    'APU Type, cellule E6 : text directly in the excell sheet

                    'Engine S/N
                    excelWorksheet.Cells(5, 28).NumberFormat = "@"
                    excelWorksheet.Cells(5, 28).Value = UserInputsChine.EngineSerialNumber

                    'Work Order
                    excelWorksheet.Cells(6, 28).NumberFormat = "@"
                    excelWorksheet.Cells(6, 28).Value = UserInputsChine.WorkOrder

                    'ECB S/N
                    excelWorksheet.Cells(7, 28).NumberFormat = "@"
                    excelWorksheet.Cells(7, 28).Value = UserInputsChine.ECBSerialNumber

                    'Exhaust Duct S/N
                    excelWorksheet.Cells(8, 28).NumberFormat = "@"
                    excelWorksheet.Cells(8, 28).Value = UserInputsChine.ExhaustDuctSerialNumber

                    'Bellmouth S/N
                    excelWorksheet.Cells(9, 28).NumberFormat = "@"
                    excelWorksheet.Cells(9, 28).Value = UserInputsChine.BellmouthSerialNumber

                    'Bleed Duct S/N
                    excelWorksheet.Cells(10, 28).NumberFormat = "@"
                    excelWorksheet.Cells(10, 28).Value = UserInputsChine.BleedDuctSerialNumber

                    'Starter S/N
                    excelWorksheet.Cells(11, 28).NumberFormat = "@"
                    excelWorksheet.Cells(11, 28).Value = UserInputsChine.StarterSerialNumber

                    'Sample T Fuel
                    excelWorksheet.Cells(12, 28).NumberFormat = "@"
                    excelWorksheet.Cells(12, 28).Value = UserInputsChine.SampleTFuel

                    'Sample SG
                    excelWorksheet.Cells(13, 28).NumberFormat = "@"
                    excelWorksheet.Cells(13, 28).Value = UserInputsChine.SampleSG

                    'Turbine Vib S/N
                    excelWorksheet.Cells(14, 28).NumberFormat = "@"
                    excelWorksheet.Cells(14, 28).Value = UserInputsChine.TurbineVibPickupSerialNumber

                    'Gearbox Vib S/N
                    excelWorksheet.Cells(5, 96).NumberFormat = "@"
                    excelWorksheet.Cells(5, 96).Value = UserInputsChine.GearboxVibPickupSerialNumber

                    'Cooling Fan Vib S/N
                    excelWorksheet.Cells(6, 96).NumberFormat = "@"
                    excelWorksheet.Cells(6, 96).Value = UserInputsChine.FanVibPickupSerialNumber

                    'Oil Type
                    excelWorksheet.Cells(7, 96).NumberFormat = "@"
                    excelWorksheet.Cells(7, 96).Value = UserInputsChine.OilType

                    'TSN
                    excelWorksheet.Cells(8, 96).NumberFormat = "@"
                    excelWorksheet.Cells(8, 96).Value = UserInputsChine.TimeSinceNewOrOverhaul

                    'CSN
                    excelWorksheet.Cells(9, 96).NumberFormat = "@"
                    excelWorksheet.Cells(9, 96).Value = UserInputsChine.CyclesSinceNewOrOverhaul

                    'LHV
                    excelWorksheet.Cells(10, 96).NumberFormat = "@"
                    excelWorksheet.Cells(10, 96).Value = UserInputsChine.LHV

                    'Test Type
                    excelWorksheet.Cells(11, 96).NumberFormat = "@"
                    excelWorksheet.Cells(11, 96).Value = UserInputsChine.TestType

                    'Repair Type
                    excelWorksheet.Cells(12, 96).NumberFormat = "@"
                    excelWorksheet.Cells(12, 96).Value = UserInputsChine.RepairType

                    'Reason
                    excelWorksheet.Cells(13, 96).NumberFormat = "@"
                    excelWorksheet.Cells(13, 96).Value = UserInputsChine.Reason

                    'Date
                    excelWorksheet.Cells(14, 96).NumberFormat = "@"
                    excelWorksheet.Cells(14, 96).Value = Format(Now, "d MMMM yyyy")

                    excelWorksheet = excelSheets.Item("ACCEPTANCE")

                    'Document Number
                    excelWorksheet.Cells(42, 5).NumberFormat = "@"
                    excelWorksheet.Cells(42, 5).Value = excelWorksheet.Cells(42, 5).Value.ToString() + Custom_Parameters("APUTestedAccordingTo").ToString()
                End If

                Select Case TestStepNumber

                    Case eTestStepGTCP131_9A.TCOND1

                        ChinaWriteCommonDataForSheet2(excelSheets, 109) 'RTL

                    Case eTestStepGTCP131_9A.TCOND2

                        ChinaWriteCommonDataForSheet2(excelSheets, 123) 'SHAFT

                        excelWorksheet = excelSheets.Item("DETAILS 1")
                        excelWorksheet.Cells(48, 123).Value = LOG_PERF_LB1_POWER_TOT.Value.ToString(LOG_PERF_LB1_POWER_TOT.Format, Nothing)

                        excelWorksheet = excelSheets.Item("DETAILS 2")
                        excelWorksheet.Cells(20, 123).Value = LOG_PERF_SHP_SL.Value.ToString(LOG_PERF_SHP_SL.Format, Nothing)

                    Case eTestStepGTCP131_9A.IGV_OFF

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' ECS OFFSET WORKSHEET section
                        excelWorksheet.Cells(34, 116).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)
                        excelWorksheet.Cells(35, 116).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)
                        excelWorksheet.Cells(36, 116).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)
                        excelWorksheet.Cells(37, 116).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)
                        excelWorksheet.Cells(38, 116).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND3

                        ChinaWriteCommonDataForSheet2(excelSheets, 81) 'ECS
                        ChinaWriteCommon2DataForSheet2(excelSheets, 81)    'ECS

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        'PBCOR, WBCOR and EGTCOR limits
                        If (UserInputsChine.RepairType.ToUpper() = "HEAVY") Then
                            excelWorksheet.Cells(21, 53).NumberFormat = "@"
                            excelWorksheet.Cells(21, 53).Value = "51.2 MINIMUM"
                            excelWorksheet.Cells(23, 53).NumberFormat = "@"
                            excelWorksheet.Cells(23, 53).Value = "152.2 MINIMUM"
                            excelWorksheet.Cells(25, 53).NumberFormat = "@"
                            excelWorksheet.Cells(25, 53).Value = "1125 MAXIMUM"
                        Else
                            excelWorksheet.Cells(21, 53).NumberFormat = "@"
                            excelWorksheet.Cells(21, 53).Value = "50.2 MINIMUM"
                            excelWorksheet.Cells(23, 53).NumberFormat = "@"
                            excelWorksheet.Cells(23, 53).Value = "148.9 MINIMUM"
                            excelWorksheet.Cells(25, 53).NumberFormat = "@"
                            excelWorksheet.Cells(25, 53).Value = "1150 MAXIMUM"
                        End If

                        excelWorksheet.Cells(21, 74).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(23, 74).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(25, 74).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(27, 74).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND4

                        ChinaWriteCommonDataForSheet2(excelSheets, 95) 'MES
                        ChinaWriteCommon2DataForSheet2(excelSheets, 95) 'MES

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        'PBCOR and EGTCOR limits
                        If (UserInputsChine.RepairType.ToUpper() = "HEAVY") Then
                            excelWorksheet.Cells(21, 95).NumberFormat = "@"
                            excelWorksheet.Cells(21, 95).Value = "51.1 MINIMUM"
                            excelWorksheet.Cells(25, 95).NumberFormat = "@"
                            excelWorksheet.Cells(25, 95).Value = "1120 MAXIMUM"
                        Else
                            excelWorksheet.Cells(21, 95).NumberFormat = "@"
                            excelWorksheet.Cells(21, 95).Value = "50.1 MINIMUM"
                            excelWorksheet.Cells(25, 95).NumberFormat = "@"
                            excelWorksheet.Cells(25, 95).Value = "1145 MAXIMUM"
                        End If

                        excelWorksheet.Cells(21, 116).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(23, 116).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(25, 116).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(27, 116).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND5

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        excelWorksheet.Cells(42, 95).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(43, 95).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(42, 116).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND6

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        excelWorksheet.Cells(44, 95).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(45, 95).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(44, 116).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                        excelWorksheet.Cells(46, 116).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND7

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                            excelWorksheet.Cells(50, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(50, 116).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9A.FINAL_DATA

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section

                        If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                            excelWorksheet.Cells(49, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(49, 116).Value = "FAILED"
                        End If

                        excelWorksheet.Cells(51, 116).Value = STARTING_TIME_SEC.Value.ToString(STARTING_TIME_SEC.Format, Nothing)
                        excelWorksheet.Cells(52, 116).Value = NUMBER_OF_STARTS.Value.ToString()
                        excelWorksheet.Cells(53, 116).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)
                        excelWorksheet.Cells(54, 116).Value = Log_Perf_NbApuFaults.Value.ToString()
                End Select

                'Test Result
                excelWorksheet = excelSheets.Item("ACCEPTANCE")

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

            Catch ex As Exception
                System.Diagnostics.Trace.WriteLine("CreatePerformanceFile: " + ex.ToString())
                MessageBox.Show(ex.ToString)
                CreateChina11813PerformanceFile = False
            Finally
                'Lock Sheets
                excelWorkbook.Worksheets(1).Protect(ProtectPassword)
                excelWorkbook.Worksheets(2).Protect(ProtectPassword)
                excelWorkbook.Worksheets(3).Protect(ProtectPassword)
                excelWorkbook.Worksheets(4).Protect(ProtectPassword)

            End Try
            'End Select
        End Function


        Private Sub EPCORWritePerfSheet2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ShowBleed As Boolean, ByVal ShowPower As Boolean)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("Sheet2")

                excelWorksheet.Cells(14, Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing) 'PBAR
                excelWorksheet.Cells(15, Column).Value = LOG_PERF_P_INLET_PSIA.Value.ToString(LOG_PERF_P_INLET_PSIA.Format, Nothing) 'PINLET
                excelWorksheet.Cells(16, Column).Value = LOG_PERF_T_INLET_AVG_DEGF.Value.ToString(LOG_PERF_T_INLET_AVG_DEGF.Format, Nothing)    'T1
                excelWorksheet.Cells(17, Column).Value = LOG_PERF_LCIT_DEGF.Value.ToString(LOG_PERF_LCIT_DEGF.Format, Nothing) 'TENIVA
                excelWorksheet.Cells(18, Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing)    'POIL
                excelWorksheet.Cells(19, Column).Value = LOG_PERF_P_GEARBOX_INH2O.Value.ToString(LOG_PERF_P_GEARBOX_INH2O.Format, Nothing) 'PSGBX
                excelWorksheet.Cells(20, Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing)    'TOS
                excelWorksheet.Cells(21, Column).Value = LOG_PERF_T_FUEL_DEGF.Value.ToString(LOG_PERF_T_FUEL_DEGF.Format, Nothing) 'TFUEL
                excelWorksheet.Cells(22, Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_PSIG.Format, Nothing) 'PFUEL
                excelWorksheet.Cells(23, Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing) 'VIBGBA
                excelWorksheet.Cells(24, Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing) 'VIBTHA
                excelWorksheet.Cells(25, Column).Value = LOG_PERF_VIB_COOL_FAN.Value.ToString(LOG_PERF_VIB_COOL_FAN.Format, Nothing) 'VIBCFA
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing)    'XNL
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing) 'PIGV
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_ARINC_SCV_POS.Value.ToString(LOG_PERF_ARINC_SCV_POS.Format, Nothing) 'PIGV
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_PSIA.Format, Nothing) 'PCDFD
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_T_LCDT_DEGF.Value.ToString(LOG_PERF_T_LCDT_DEGF.Format, Nothing) 'TCDFD
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_T_ETC1_DEGF.Value.ToString(LOG_PERF_T_ETC1_DEGF.Format, Nothing) 'TTDEA
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_T_ETC2_DEGF.Value.ToString(LOG_PERF_T_ETC2_DEGF.Format, Nothing) 'TTDEB
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_T_EGT_AVG_DEGF.Value.ToString(LOG_PERF_T_EGT_AVG_DEGF.Format, Nothing)    'EGT
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing)    'PS9

                If ShowBleed Then
                    excelWorksheet.Cells(35, Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)      'PBORFA
                    excelWorksheet.Cells(36, Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)      'TBORFA
                    excelWorksheet.Cells(37, Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing)    'PDBORA
                    excelWorksheet.Cells(38, Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)                    'WB
                    excelWorksheet.Cells(39, Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)            'WBCDNA
                    excelWorksheet.Cells(40, Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)    'PB
                    excelWorksheet.Cells(41, Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)     'TB
                End If

                ' Always show fuel flow
                excelWorksheet.Cells(42, Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing)    'WF

                If ShowPower Then
                    excelWorksheet.Cells(43, Column).Value = LOG_PERF_LB1_POWER_TOT.Value.ToString(LOG_PERF_LB1_POWER_TOT.Format, Nothing)  'PWGEN
                End If

            Catch Ex As Exception
                MessageBox.Show("EPCORWritePerfSheet2: " & Ex.ToString())
            End Try
        End Sub


        Private Sub FillAllDatas(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ColumnName As String)

            Try

                Select Case CELProject.MeasurementUnitInUse
                    Case CELDAS.eMeasurementUnit.Imperial
                        FillAllDatasImperial(excelSheets, Column, ColumnName)

                    Case CELDAS.eMeasurementUnit.Metric
                        FillAllDatasMetric(excelSheets, Column, ColumnName)

                End Select

            Catch ex As Exception

                MessageBox.Show("FillAllDatas: " & ex.ToString())

            End Try

        End Sub

        Private Sub FillAllDatasImperial(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ColumnName As String)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("Sheet5")
                Dim startColum As Integer = 4

                'Header
                excelWorksheet.Cells(2, startColum + Column).Value = ColumnName

                excelWorksheet.Cells(3, startColum).Value = "DateTime"
                excelWorksheet.Cells(3, startColum + Column).NumberFormat = "@"
                excelWorksheet.Cells(3, startColum + Column).Name = ColumnName & "DateTime"
                excelWorksheet.Cells(3, startColum + Column).Value = Format(Now, "yyyy-MM-dd HH:mm:ss")


                excelWorksheet.Cells(4, startColum).Value = "LOG_PERF_P_BARO_PSIA"
                excelWorksheet.Cells(4, startColum + Column).Name = ColumnName & "LOG_PERF_P_BARO_PSIA"
                excelWorksheet.Cells(4, startColum + Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing) 'PBAR

                excelWorksheet.Cells(5, startColum).Value = "LOG_PERF_P_INLET_PSIA"
                excelWorksheet.Cells(5, startColum + Column).Name = ColumnName & "LOG_PERF_P_INLET_PSIA"
                excelWorksheet.Cells(5, startColum + Column).Value = LOG_PERF_P_INLET_PSIA.Value.ToString(LOG_PERF_P_INLET_PSIA.Format, Nothing) 'PINLET

                excelWorksheet.Cells(6, startColum).Value = "LOG_PERF_T_INLET_AVG_DEGF"
                excelWorksheet.Cells(6, startColum + Column).Name = ColumnName & "LOG_PERF_T_INLET_AVG_DEGF"
                excelWorksheet.Cells(6, startColum + Column).Value = LOG_PERF_T_INLET_AVG_DEGF.Value.ToString(LOG_PERF_T_INLET_AVG_DEGF.Format, Nothing)    'T1

                excelWorksheet.Cells(7, startColum).Value = "LOG_PERF_LCIT_DEGF"
                excelWorksheet.Cells(7, startColum + Column).Name = ColumnName & "LOG_PERF_LCIT_DEGF"
                excelWorksheet.Cells(7, startColum + Column).Value = LOG_PERF_LCIT_DEGF.Value.ToString(LOG_PERF_LCIT_DEGF.Format, Nothing) 'TENIVA

                excelWorksheet.Cells(8, startColum).Value = "LOG_PERF_P_OIL_PSIG"
                excelWorksheet.Cells(8, startColum + Column).Name = ColumnName & "LOG_PERF_P_OIL_PSIG"
                excelWorksheet.Cells(8, startColum + Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing)    'POIL

                excelWorksheet.Cells(9, startColum).Value = "LOG_PERF_P_GEARBOX_INH2O"
                excelWorksheet.Cells(9, startColum + Column).Name = ColumnName & "LOG_PERF_P_GEARBOX_INH2O"
                excelWorksheet.Cells(9, startColum + Column).Value = LOG_PERF_P_GEARBOX_INH2O.Value.ToString(LOG_PERF_P_GEARBOX_INH2O.Format, Nothing) 'PSGBX

                excelWorksheet.Cells(10, startColum).Value = "LOG_PERF_T_OIL_DEGF"
                excelWorksheet.Cells(10, startColum + Column).Name = ColumnName & "LOG_PERF_T_OIL_DEGF"
                excelWorksheet.Cells(10, startColum + Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing)    'TOS

                excelWorksheet.Cells(11, startColum).Value = "LOG_PERF_T_FUEL_DEGF"
                excelWorksheet.Cells(11, startColum + Column).Name = ColumnName & "LOG_PERF_T_FUEL_DEGF"
                excelWorksheet.Cells(11, startColum + Column).Value = LOG_PERF_T_FUEL_DEGF.Value.ToString(LOG_PERF_T_FUEL_DEGF.Format, Nothing) 'TFUEL

                excelWorksheet.Cells(12, startColum).Value = "LOG_PERF_P_FUEL_PSIG"
                excelWorksheet.Cells(12, startColum + Column).Name = ColumnName & "LOG_PERF_P_FUEL_PSIG"
                excelWorksheet.Cells(12, startColum + Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_PSIG.Format, Nothing) 'PFUEL

                excelWorksheet.Cells(13, startColum).Value = "LOG_PERF_VIB_GEARBOX"
                excelWorksheet.Cells(13, startColum + Column).Name = ColumnName & "LOG_PERF_VIB_GEARBOX"
                excelWorksheet.Cells(13, startColum + Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing) 'VIBGBA

                excelWorksheet.Cells(14, startColum).Value = "LOG_PERF_VIB_TURBINE"
                excelWorksheet.Cells(14, startColum + Column).Name = ColumnName & "LOG_PERF_VIB_TURBINE"
                excelWorksheet.Cells(14, startColum + Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing) 'VIBTHA

                excelWorksheet.Cells(15, startColum).Value = "LOG_PERF_VIB_COOL_FAN"
                excelWorksheet.Cells(15, startColum + Column).Name = ColumnName & "LOG_PERF_VIB_COOL_FAN"
                excelWorksheet.Cells(15, startColum + Column).Value = LOG_PERF_VIB_COOL_FAN.Value.ToString(LOG_PERF_VIB_COOL_FAN.Format, Nothing) 'VIBCFA

                excelWorksheet.Cells(16, startColum).Value = "LOG_PERF_NG"
                excelWorksheet.Cells(16, startColum + Column).Name = ColumnName & "LOG_PERF_NG"
                excelWorksheet.Cells(16, startColum + Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing)    'XNL

                excelWorksheet.Cells(17, startColum).Value = "LOG_PERF_ARINC_IGV_POS"
                excelWorksheet.Cells(17, startColum + Column).Name = ColumnName & "LOG_PERF_ARINC_IGV_POS"
                excelWorksheet.Cells(17, startColum + Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing) 'PIGV

                excelWorksheet.Cells(18, startColum).Value = "LOG_PERF_ARINC_SCV_POS"
                excelWorksheet.Cells(18, startColum + Column).Name = ColumnName & "LOG_PERF_ARINC_SCV_POS"
                excelWorksheet.Cells(18, startColum + Column).Value = LOG_PERF_ARINC_SCV_POS.Value.ToString(LOG_PERF_ARINC_SCV_POS.Format, Nothing) 'PIGV

                excelWorksheet.Cells(19, startColum).Value = "LOG_PERF_P_COMP_DISCH_PSIA"
                excelWorksheet.Cells(19, startColum + Column).Name = ColumnName & "LOG_PERF_P_COMP_DISCH_PSIA"
                excelWorksheet.Cells(19, startColum + Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_PSIA.Format, Nothing) 'PCDFD

                excelWorksheet.Cells(20, startColum).Value = "LOG_PERF_T_LCDT_DEGF"
                excelWorksheet.Cells(20, startColum + Column).Name = ColumnName & "LOG_PERF_T_LCDT_DEGF"
                excelWorksheet.Cells(20, startColum + Column).Value = LOG_PERF_T_LCDT_DEGF.Value.ToString(LOG_PERF_T_LCDT_DEGF.Format, Nothing) 'TCDFD

                excelWorksheet.Cells(21, startColum).Value = "LOG_PERF_T_ETC1_DEGF"
                excelWorksheet.Cells(21, startColum + Column).Name = ColumnName & "LOG_PERF_T_ETC1_DEGF"
                excelWorksheet.Cells(21, startColum + Column).Value = LOG_PERF_T_ETC1_DEGF.Value.ToString(LOG_PERF_T_ETC1_DEGF.Format, Nothing) 'TTDEA

                excelWorksheet.Cells(22, startColum).Value = "LOG_PERF_T_ETC2_DEGF"
                excelWorksheet.Cells(22, startColum + Column).Name = ColumnName & "LOG_PERF_T_ETC2_DEGF"
                excelWorksheet.Cells(22, startColum + Column).Value = LOG_PERF_T_ETC2_DEGF.Value.ToString(LOG_PERF_T_ETC2_DEGF.Format, Nothing) 'TTDEB

                excelWorksheet.Cells(23, startColum).Value = "LOG_PERF_T_EGT_AVG_DEGF"
                excelWorksheet.Cells(23, startColum + Column).Name = ColumnName & "LOG_PERF_T_EGT_AVG_DEGF"
                excelWorksheet.Cells(23, startColum + Column).Value = LOG_PERF_T_EGT_AVG_DEGF.Value.ToString(LOG_PERF_T_EGT_AVG_DEGF.Format, Nothing)    'EGT

                excelWorksheet.Cells(24, startColum).Value = "LOG_PERF_P_EXHAUST_PSIA"
                excelWorksheet.Cells(24, startColum + Column).Name = ColumnName & "LOG_PERF_P_EXHAUST_PSIA"
                excelWorksheet.Cells(24, startColum + Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing)    'PS9

                excelWorksheet.Cells(25, startColum).Value = "LOG_PERF_P_ORIFICE_PSIA"
                excelWorksheet.Cells(25, startColum + Column).Name = ColumnName & "LOG_PERF_P_ORIFICE_PSIA"
                excelWorksheet.Cells(25, startColum + Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)      'PBORFA

                excelWorksheet.Cells(26, startColum).Value = "LOG_PERF_T_ORIFICE_DEGF"
                excelWorksheet.Cells(26, startColum + Column).Name = ColumnName & "LOG_PERF_T_ORIFICE_DEGF"
                excelWorksheet.Cells(26, startColum + Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)      'TBORFA

                excelWorksheet.Cells(27, startColum).Value = "LOG_PERF_P_ORIFICE_DELTA_PSID"
                excelWorksheet.Cells(27, startColum + Column).Name = ColumnName & "LOG_PERF_P_ORIFICE_DELTA_PSID"
                excelWorksheet.Cells(27, startColum + Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing)    'PDBORA

                excelWorksheet.Cells(28, startColum).Value = "LOG_PERF_WB"
                excelWorksheet.Cells(28, startColum + Column).Name = ColumnName & "LOG_PERF_WB"
                excelWorksheet.Cells(28, startColum + Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)                    'WB

                excelWorksheet.Cells(29, startColum).Value = "LOG_PERF_WBCDNA"
                excelWorksheet.Cells(29, startColum + Column).Name = ColumnName & "LOG_PERF_WBCDNA"
                excelWorksheet.Cells(29, startColum + Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)            'WBCDNA

                excelWorksheet.Cells(30, startColum).Value = "LOG_PERF_P_BLEED1_5_PSIA"
                excelWorksheet.Cells(30, startColum + Column).Name = ColumnName & "LOG_PERF_P_BLEED1_5_PSIA"
                excelWorksheet.Cells(30, startColum + Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)    'PB

                excelWorksheet.Cells(31, startColum).Value = "LOG_PERF_T_BLEED_AVG_DEGF"
                excelWorksheet.Cells(31, startColum + Column).Name = ColumnName & "LOG_PERF_T_BLEED_AVG_DEGF"
                excelWorksheet.Cells(31, startColum + Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)     'TB

                excelWorksheet.Cells(32, startColum).Value = "LOG_PERF_T_BLEED_AVG_DEGF"
                excelWorksheet.Cells(32, startColum + Column).Name = ColumnName & "LOG_PERF_T_BLEED_AVG_DEGF"
                excelWorksheet.Cells(32, startColum + Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)     'TB

                excelWorksheet.Cells(33, startColum).Value = "LOG_PERF_WF_AVG_PPH"
                excelWorksheet.Cells(33, startColum + Column).Name = ColumnName & "LOG_PERF_WF_AVG_PPH"
                excelWorksheet.Cells(33, startColum + Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing)    'WF

                excelWorksheet.Cells(34, startColum).Value = "LOG_PERF_LB1_POWER_TOT"
                excelWorksheet.Cells(34, startColum + Column).Name = ColumnName & "LOG_PERF_LB1_POWER_TOT"
                excelWorksheet.Cells(34, startColum + Column).Value = LOG_PERF_LB1_POWER_TOT.Value.ToString(LOG_PERF_LB1_POWER_TOT.Format, Nothing)  'PWGEN



                excelWorksheet.Cells(35, startColum).Value = "LOG_PERF_SHP_SL"
                excelWorksheet.Cells(35, startColum + Column).Name = ColumnName & "LOG_PERF_SHP_SL"
                excelWorksheet.Cells(35, startColum + Column).Value = LOG_PERF_SHP_SL.Value.ToString(LOG_PERF_SHP_SL.Format, Nothing)    'SHPSL

                excelWorksheet.Cells(36, startColum).Value = "LOG_PERF_DELTA_P_SL"
                excelWorksheet.Cells(36, startColum + Column).Name = ColumnName & "LOG_PERF_DELTA_P_SL"
                excelWorksheet.Cells(36, startColum + Column).Value = LOG_PERF_DELTA_P_SL.Value.ToString(LOG_PERF_DELTA_P_SL.Format, Nothing)    'SL Delta P

                excelWorksheet.Cells(37, startColum).Value = "LOG_PERF_PB_SL_PSIA"
                excelWorksheet.Cells(37, startColum + Column).Name = ColumnName & "LOG_PERF_PB_SL_PSIA"
                excelWorksheet.Cells(37, startColum + Column).Value = LOG_PERF_PB_SL_PSIA.Value.ToString(LOG_PERF_PB_SL_PSIA.Format, Nothing)    'SL Bleed P

                excelWorksheet.Cells(38, startColum).Value = "LOG_PERF_DELPB"
                excelWorksheet.Cells(38, startColum + Column).Name = ColumnName & "LOG_PERF_DELPB"
                excelWorksheet.Cells(38, startColum + Column).Value = LOG_PERF_DELPB.Value.ToString(LOG_PERF_DELPB.Format, Nothing)      'DELPB

                excelWorksheet.Cells(39, startColum).Value = "LOG_PERF_PB_INST_PSIA"
                excelWorksheet.Cells(39, startColum + Column).Name = ColumnName & "LOG_PERF_PB_INST_PSIA"
                excelWorksheet.Cells(39, startColum + Column).Value = LOG_PERF_PB_INST_PSIA.Value.ToString(LOG_PERF_PB_INST_PSIA.Format, Nothing)    'Installation effect on Bleed P

                excelWorksheet.Cells(40, startColum).Value = "LOG_PERF_PBCOR"
                excelWorksheet.Cells(40, startColum + Column).Name = ColumnName & "LOG_PERF_PBCOR"
                excelWorksheet.Cells(40, startColum + Column).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)      'PBCOR

                excelWorksheet.Cells(41, startColum).Value = "LOG_PERF_WB_SL"
                excelWorksheet.Cells(41, startColum + Column).Name = ColumnName & "LOG_PERF_WB_SL"
                excelWorksheet.Cells(41, startColum + Column).Value = LOG_PERF_WB_SL.Value.ToString(LOG_PERF_WB_SL.Format, Nothing)      'SL Bleed AF

                excelWorksheet.Cells(42, startColum).Value = "LOG_PERF_DELWB"
                excelWorksheet.Cells(42, startColum + Column).Name = ColumnName & "LOG_PERF_DELWB"
                excelWorksheet.Cells(42, startColum + Column).Value = LOG_PERF_DELWB.Value.ToString(LOG_PERF_DELWB.Format, Nothing)      'DELWB

                excelWorksheet.Cells(43, startColum).Value = "LOG_PERF_WB_INST"
                excelWorksheet.Cells(43, startColum + Column).Name = ColumnName & "LOG_PERF_WB_INST"
                excelWorksheet.Cells(43, startColum + Column).Value = LOG_PERF_WB_INST.Value.ToString(LOG_PERF_WB_INST.Format, Nothing)  'Installation effect on WB

                excelWorksheet.Cells(44, startColum).Value = "LOG_PERF_WBCOR"
                excelWorksheet.Cells(44, startColum + Column).Name = ColumnName & "LOG_PERF_WBCOR"
                excelWorksheet.Cells(44, startColum + Column).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)      'WFCOR

                excelWorksheet.Cells(45, startColum).Value = "LOG_PERF_DELTB"
                excelWorksheet.Cells(45, startColum + Column).Name = ColumnName & "LOG_PERF_DELTB"
                excelWorksheet.Cells(45, startColum + Column).Value = LOG_PERF_DELTB.Value.ToString(LOG_PERF_DELTB.Format, Nothing)      'DELTB

                excelWorksheet.Cells(46, startColum).Value = "LOG_PERF_TBCOR_EXC_PB"
                excelWorksheet.Cells(46, startColum + Column).Name = ColumnName & "LOG_PERF_TBCOR_EXC_PB"
                excelWorksheet.Cells(46, startColum + Column).Value = LOG_PERF_TBCOR_EXC_PB.Value.ToString(LOG_PERF_TBCOR_EXC_PB.Format, Nothing)    'Excess pressure correction on TBCOR

                excelWorksheet.Cells(47, startColum).Value = "LOG_PERF_TBCOR"
                excelWorksheet.Cells(47, startColum + Column).Name = ColumnName & "LOG_PERF_TBCOR"
                excelWorksheet.Cells(47, startColum + Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)      'TBCOR

                excelWorksheet.Cells(48, startColum).Value = "LOG_PERF_DELEGT"
                excelWorksheet.Cells(48, startColum + Column).Name = ColumnName & "LOG_PERF_DELEGT"
                excelWorksheet.Cells(48, startColum + Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)    'DELEGT

                excelWorksheet.Cells(49, startColum).Value = "LOG_PERF_EGT_DP_CORR"
                excelWorksheet.Cells(49, startColum + Column).Name = ColumnName & "LOG_PERF_EGT_DP_CORR"
                excelWorksheet.Cells(49, startColum + Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing) 'Delta P Correction on EGT

                excelWorksheet.Cells(50, startColum).Value = "LOG_PERF_EGT_INST"
                excelWorksheet.Cells(50, startColum + Column).Name = ColumnName & "LOG_PERF_EGT_INST"
                excelWorksheet.Cells(50, startColum + Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)    'Installation effect on EGT

                excelWorksheet.Cells(51, startColum).Value = "LOG_PERF_EGT_EXC_PB"
                excelWorksheet.Cells(51, startColum + Column).Name = ColumnName & "LOG_PERF_EGT_EXC_PB"
                excelWorksheet.Cells(51, startColum + Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)    'Excess bleed P correction on EGT

                excelWorksheet.Cells(52, startColum).Value = "LOG_PERF_EGTCOR"
                excelWorksheet.Cells(52, startColum + Column).Name = ColumnName & "LOG_PERF_EGTCOR"
                excelWorksheet.Cells(52, startColum + Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)    'EGTCOR

                excelWorksheet.Cells(53, startColum).Value = "LOG_PERF_WF_SL"
                excelWorksheet.Cells(53, startColum + Column).Name = ColumnName & "LOG_PERF_WF_SL"
                excelWorksheet.Cells(53, startColum + Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)      'SL Fuel FLow

                excelWorksheet.Cells(54, startColum).Value = "LOG_PERF_DELWF"
                excelWorksheet.Cells(54, startColum + Column).Name = ColumnName & "LOG_PERF_DELWF"
                excelWorksheet.Cells(54, startColum + Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)      'DELWF

                excelWorksheet.Cells(55, startColum).Value = "LOG_PERF_WF_DP_CORR"
                excelWorksheet.Cells(55, startColum + Column).Name = ColumnName & "LOG_PERF_WF_DP_CORR"
                excelWorksheet.Cells(55, startColum + Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)    'Delta P Correction on WF

                excelWorksheet.Cells(56, startColum).Value = "LOG_PERF_WF_INST"
                excelWorksheet.Cells(56, startColum + Column).Name = ColumnName & "LOG_PERF_WF_INST"
                excelWorksheet.Cells(56, startColum + Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)  'Installation effect on WF

                excelWorksheet.Cells(57, startColum).Value = "LOG_PERF_WF_EXC_PB"
                excelWorksheet.Cells(57, startColum + Column).Name = ColumnName & "LOG_PERF_WF_EXC_PB"
                excelWorksheet.Cells(57, startColum + Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing) 'Excess bleed P correction on WF

                excelWorksheet.Cells(58, startColum).Value = "LOG_PERF_WFCOR"
                excelWorksheet.Cells(58, startColum + Column).Name = ColumnName & "LOG_PERF_WFCOR"
                excelWorksheet.Cells(58, startColum + Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)      'WFCOR

                ' ECS OFFSET WORKSHEET section
                excelWorksheet.Cells(59, startColum).Value = "Log_Perf_InitPBCOR"
                excelWorksheet.Cells(59, startColum + Column).Name = ColumnName & "Log_Perf_InitPBCOR"
                excelWorksheet.Cells(59, startColum + Column).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)

                excelWorksheet.Cells(60, startColum).Value = "Log_Perf_FinalPBCOR"
                excelWorksheet.Cells(60, startColum + Column).Name = ColumnName & "Log_Perf_FinalPBCOR"
                excelWorksheet.Cells(60, startColum + Column).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)

                excelWorksheet.Cells(61, startColum).Value = "Log_Perf_InitIGV"
                excelWorksheet.Cells(61, startColum + Column).Name = ColumnName & "Log_Perf_InitIGV"
                excelWorksheet.Cells(61, startColum + Column).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)

                excelWorksheet.Cells(62, startColum).Value = "Log_Perf_FinalIGV"
                excelWorksheet.Cells(62, startColum + Column).Name = ColumnName & "Log_Perf_FinalIGV"
                excelWorksheet.Cells(62, startColum + Column).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)

                excelWorksheet.Cells(63, startColum).Value = "LOG_PERF_ECS_OFFSET"
                excelWorksheet.Cells(63, startColum + Column).Name = ColumnName & "LOG_PERF_ECS_OFFSET"
                excelWorksheet.Cells(63, startColum + Column).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                ' Performance Summary, Data point, Actual
                excelWorksheet.Cells(64, startColum).Value = "LOG_PERF_PBCOR"
                excelWorksheet.Cells(64, startColum + Column).Name = ColumnName & "LOG_PERF_PBCOR"
                excelWorksheet.Cells(64, startColum + Column).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)

                excelWorksheet.Cells(65, startColum).Value = "LOG_PERF_WBCOR"
                excelWorksheet.Cells(65, startColum + Column).Name = ColumnName & "LOG_PERF_WBCOR"
                excelWorksheet.Cells(65, startColum + Column).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)

                excelWorksheet.Cells(66, startColum).Value = "LOG_PERF_EGTCOR"
                excelWorksheet.Cells(66, startColum + Column).Name = ColumnName & "LOG_PERF_EGTCOR"
                excelWorksheet.Cells(66, startColum + Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)

                excelWorksheet.Cells(67, startColum).Value = "LOG_PERF_WFCOR"
                excelWorksheet.Cells(67, startColum + Column).Name = ColumnName & "LOG_PERF_WFCOR"
                excelWorksheet.Cells(67, startColum + Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)



                excelWorksheet.Cells(68, startColum).Value = "LOG_PERF_WBCDNA_LO_LIM"
                excelWorksheet.Cells(68, startColum + Column).Name = ColumnName & "LOG_PERF_WBCDNA_LO_LIM"
                excelWorksheet.Cells(68, startColum + Column).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)

                excelWorksheet.Cells(69, startColum).Value = "LOG_PERF_WBCDNA_HI_LIM"
                excelWorksheet.Cells(69, startColum + Column).Name = ColumnName & "LOG_PERF_WBCDNA_HI_LIM"
                excelWorksheet.Cells(69, startColum + Column).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)

                excelWorksheet.Cells(70, startColum).Value = "LOG_PERF_WBCDNA"
                excelWorksheet.Cells(70, startColum + Column).Name = ColumnName & "LOG_PERF_WBCDNA"
                excelWorksheet.Cells(70, startColum + Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)


                excelWorksheet.Cells(71, startColum).Value = "LOG_PERF_FLOW_SENSOR_ACC"
                excelWorksheet.Cells(71, startColum + Column).Name = ColumnName & "LOG_PERF_FLOW_SENSOR_ACC"
                excelWorksheet.Cells(71, startColum + Column).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)




                excelWorksheet.Cells(72, startColum).Value = "Log_Perf_SCVStability"
                excelWorksheet.Cells(72, startColum + Column).Name = ColumnName & "Log_Perf_SCVStability"
                If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                    excelWorksheet.Cells(72, startColum + Column).Value = "PASSED"
                Else
                    excelWorksheet.Cells(72, startColum + Column).Value = "FAILED"
                End If

                excelWorksheet.Cells(73, startColum).Value = "STARTING_TIME_SEC"
                excelWorksheet.Cells(73, startColum + Column).Name = ColumnName & "STARTING_TIME_SEC"
                excelWorksheet.Cells(73, startColum + Column).Value = STARTING_TIME_SEC.Value.ToString(STARTING_TIME_SEC.Format, Nothing)

                excelWorksheet.Cells(74, startColum).Value = "NUMBER_OF_STARTS"
                excelWorksheet.Cells(74, startColum + Column).Name = ColumnName & "NUMBER_OF_STARTS"
                excelWorksheet.Cells(74, startColum + Column).Value = NUMBER_OF_STARTS.Value.ToString()

                excelWorksheet.Cells(75, startColum).Value = "LOG_PERF_OPERATING_TIME_CURRENT_HOURS"
                excelWorksheet.Cells(75, startColum + Column).Name = ColumnName & "LOG_PERF_OPERATING_TIME_CURRENT_HOURS"
                excelWorksheet.Cells(75, startColum + Column).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)

                excelWorksheet.Cells(76, startColum).Value = "Log_Perf_NbApuFaults"
                excelWorksheet.Cells(76, startColum + Column).Name = ColumnName & "Log_Perf_NbApuFaults"
                excelWorksheet.Cells(76, startColum + Column).Value = Log_Perf_NbApuFaults.Value.ToString()



                excelWorksheet.Cells(77, startColum).Value = "PBCOR_LIMIT_MIN"
                excelWorksheet.Cells(77, startColum + Column).Name = ColumnName & "PBCOR_LIMIT_MIN"

                excelWorksheet.Cells(78, startColum).Value = "WBCOR_LIMIT_MIN"
                excelWorksheet.Cells(78, startColum + Column).Name = ColumnName & "WBCOR_LIMIT_MIN"

                excelWorksheet.Cells(79, startColum).Value = "BLEED_PRESSURE_LIMIT_MAX"
                excelWorksheet.Cells(79, startColum + Column).Name = ColumnName & "BLEED_PRESSURE_LIMIT_MAX"


                excelWorksheet.Cells(80, startColum).Value = "WFCOR_REF"
                excelWorksheet.Cells(80, startColum + Column).Name = ColumnName & "WFCOR_REF"



                excelWorksheet.Cells(81, startColum).Value = "Log_Perf_MinSurgeMargin"
                excelWorksheet.Cells(81, startColum + Column).Name = ColumnName & "Log_Perf_MinSurgeMargin"
                If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                    excelWorksheet.Cells(81, startColum + Column).Value = "PASSED"
                Else
                    excelWorksheet.Cells(81, startColum + Column).Value = "FAILED"
                End If


            Catch Ex As Exception
                MessageBox.Show("FillAllDatas: " & Ex.ToString())
            End Try

        End Sub

        Private Sub FillAllDatasMetric(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ColumnName As String)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("Sheet5")
                Dim startColum As Integer = 4

                'Header
                excelWorksheet.Cells(2, startColum + Column).Value = ColumnName

                excelWorksheet.Cells(3, startColum).Value = "DateTime"
                excelWorksheet.Cells(3, startColum + Column).NumberFormat = "@"
                excelWorksheet.Cells(3, startColum + Column).Name = ColumnName & "DateTime"
                excelWorksheet.Cells(3, startColum + Column).Value = Format(Now, "yyyy-MM-dd HH:mm:ss")


                excelWorksheet.Cells(4, startColum).Value = "LOG_PERF_P_BARO_PSIA"
                excelWorksheet.Cells(4, startColum + Column).Name = ColumnName & "LOG_PERF_P_BARO_PSIA"
                excelWorksheet.Cells(4, startColum + Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_BARA.Format, Nothing) 'PBAR

                excelWorksheet.Cells(5, startColum).Value = "LOG_PERF_P_INLET_PSIA"
                excelWorksheet.Cells(5, startColum + Column).Name = ColumnName & "LOG_PERF_P_INLET_PSIA"
                excelWorksheet.Cells(5, startColum + Column).Value = LOG_PERF_P_INLET_PSIA.Value.ToString(LOG_PERF_P_INLET_BARA.Format, Nothing) 'PINLET

                excelWorksheet.Cells(6, startColum).Value = "LOG_PERF_T_INLET_AVG_DEGF"
                excelWorksheet.Cells(6, startColum + Column).Name = ColumnName & "LOG_PERF_T_INLET_AVG_DEGF"
                excelWorksheet.Cells(6, startColum + Column).Value = LOG_PERF_T_INLET_AVG_DEGF.Value.ToString(LOG_PERF_T_INLET_AVG_DEGC.Format, Nothing)    'T1

                excelWorksheet.Cells(7, startColum).Value = "LOG_PERF_LCIT_DEGF"
                excelWorksheet.Cells(7, startColum + Column).Name = ColumnName & "LOG_PERF_LCIT_DEGF"
                excelWorksheet.Cells(7, startColum + Column).Value = LOG_PERF_LCIT_DEGF.Value.ToString(LOG_PERF_LCIT_DEGC.Format, Nothing) 'TENIVA

                excelWorksheet.Cells(8, startColum).Value = "LOG_PERF_P_OIL_PSIG"
                excelWorksheet.Cells(8, startColum + Column).Name = ColumnName & "LOG_PERF_P_OIL_PSIG"
                excelWorksheet.Cells(8, startColum + Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_KPAG.Format, Nothing)    'POIL

                excelWorksheet.Cells(9, startColum).Value = "LOG_PERF_P_GEARBOX_INH2O"
                excelWorksheet.Cells(9, startColum + Column).Name = ColumnName & "LOG_PERF_P_GEARBOX_INH2O"
                excelWorksheet.Cells(9, startColum + Column).Value = LOG_PERF_P_GEARBOX_INH2O.Value.ToString(LOG_PERF_P_GEARBOX_MMH2O.Format, Nothing) 'PSGBX

                excelWorksheet.Cells(10, startColum).Value = "LOG_PERF_T_OIL_DEGF"
                excelWorksheet.Cells(10, startColum + Column).Name = ColumnName & "LOG_PERF_T_OIL_DEGF"
                excelWorksheet.Cells(10, startColum + Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGC.Format, Nothing)    'TOS

                excelWorksheet.Cells(11, startColum).Value = "LOG_PERF_T_FUEL_DEGF"
                excelWorksheet.Cells(11, startColum + Column).Name = ColumnName & "LOG_PERF_T_FUEL_DEGF"
                excelWorksheet.Cells(11, startColum + Column).Value = LOG_PERF_T_FUEL_DEGF.Value.ToString(LOG_PERF_T_FUEL_DEGC.Format, Nothing) 'TFUEL

                excelWorksheet.Cells(12, startColum).Value = "LOG_PERF_P_FUEL_PSIG"
                excelWorksheet.Cells(12, startColum + Column).Name = ColumnName & "LOG_PERF_P_FUEL_PSIG"
                excelWorksheet.Cells(12, startColum + Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_KPAG.Format, Nothing) 'PFUEL

                excelWorksheet.Cells(13, startColum).Value = "LOG_PERF_VIB_GEARBOX"
                excelWorksheet.Cells(13, startColum + Column).Name = ColumnName & "LOG_PERF_VIB_GEARBOX"
                excelWorksheet.Cells(13, startColum + Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX_MMPS_PEAK.Format, Nothing) 'VIBGBA

                excelWorksheet.Cells(14, startColum).Value = "LOG_PERF_VIB_TURBINE"
                excelWorksheet.Cells(14, startColum + Column).Name = ColumnName & "LOG_PERF_VIB_TURBINE"
                excelWorksheet.Cells(14, startColum + Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE_MMPS_PEAK.Format, Nothing) 'VIBTHA

                excelWorksheet.Cells(15, startColum).Value = "LOG_PERF_VIB_COOL_FAN"
                excelWorksheet.Cells(15, startColum + Column).Name = ColumnName & "LOG_PERF_VIB_COOL_FAN"
                excelWorksheet.Cells(15, startColum + Column).Value = LOG_PERF_VIB_COOL_FAN.Value.ToString(LOG_PERF_VIB_COOL_FAN_MMPS_PEAK.Format, Nothing) 'VIBCFA

                excelWorksheet.Cells(16, startColum).Value = "LOG_PERF_NG"
                excelWorksheet.Cells(16, startColum + Column).Name = ColumnName & "LOG_PERF_NG"
                excelWorksheet.Cells(16, startColum + Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing)    'XNL

                excelWorksheet.Cells(17, startColum).Value = "LOG_PERF_ARINC_IGV_POS"
                excelWorksheet.Cells(17, startColum + Column).Name = ColumnName & "LOG_PERF_ARINC_IGV_POS"
                excelWorksheet.Cells(17, startColum + Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing) 'PIGV

                excelWorksheet.Cells(18, startColum).Value = "LOG_PERF_ARINC_SCV_POS"
                excelWorksheet.Cells(18, startColum + Column).Name = ColumnName & "LOG_PERF_ARINC_SCV_POS"
                excelWorksheet.Cells(18, startColum + Column).Value = LOG_PERF_ARINC_SCV_POS.Value.ToString(LOG_PERF_ARINC_SCV_POS.Format, Nothing) 'PIGV

                excelWorksheet.Cells(19, startColum).Value = "LOG_PERF_P_COMP_DISCH_PSIA"
                excelWorksheet.Cells(19, startColum + Column).Name = ColumnName & "LOG_PERF_P_COMP_DISCH_PSIA"
                excelWorksheet.Cells(19, startColum + Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_BARA.Format, Nothing) 'PCDFD

                excelWorksheet.Cells(20, startColum).Value = "LOG_PERF_T_LCDT_DEGF"
                excelWorksheet.Cells(20, startColum + Column).Name = ColumnName & "LOG_PERF_T_LCDT_DEGF"
                excelWorksheet.Cells(20, startColum + Column).Value = LOG_PERF_T_LCDT_DEGF.Value.ToString(LOG_PERF_T_LCDT_DEGC.Format, Nothing) 'TCDFD

                excelWorksheet.Cells(21, startColum).Value = "LOG_PERF_T_ETC1_DEGF"
                excelWorksheet.Cells(21, startColum + Column).Name = ColumnName & "LOG_PERF_T_ETC1_DEGF"
                excelWorksheet.Cells(21, startColum + Column).Value = LOG_PERF_T_ETC1_DEGF.Value.ToString(LOG_PERF_T_ETC1_DEGC.Format, Nothing) 'TTDEA

                excelWorksheet.Cells(22, startColum).Value = "LOG_PERF_T_ETC2_DEGF"
                excelWorksheet.Cells(22, startColum + Column).Name = ColumnName & "LOG_PERF_T_ETC2_DEGF"
                excelWorksheet.Cells(22, startColum + Column).Value = LOG_PERF_T_ETC2_DEGF.Value.ToString(LOG_PERF_T_ETC2_DEGC.Format, Nothing) 'TTDEB

                excelWorksheet.Cells(23, startColum).Value = "LOG_PERF_T_EGT_AVG_DEGF"
                excelWorksheet.Cells(23, startColum + Column).Name = ColumnName & "LOG_PERF_T_EGT_AVG_DEGF"
                excelWorksheet.Cells(23, startColum + Column).Value = LOG_PERF_T_EGT_AVG_DEGF.Value.ToString(LOG_PERF_T_EGT_AVG_DEGC.Format, Nothing)    'EGT

                excelWorksheet.Cells(24, startColum).Value = "LOG_PERF_P_EXHAUST_PSIA"
                excelWorksheet.Cells(24, startColum + Column).Name = ColumnName & "LOG_PERF_P_EXHAUST_PSIA"
                excelWorksheet.Cells(24, startColum + Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_BARA.Format, Nothing)    'PS9

                excelWorksheet.Cells(25, startColum).Value = "LOG_PERF_P_ORIFICE_PSIA"
                excelWorksheet.Cells(25, startColum + Column).Name = ColumnName & "LOG_PERF_P_ORIFICE_PSIA"
                excelWorksheet.Cells(25, startColum + Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_BARA.Format, Nothing)      'PBORFA

                excelWorksheet.Cells(26, startColum).Value = "LOG_PERF_T_ORIFICE_DEGF"
                excelWorksheet.Cells(26, startColum + Column).Name = ColumnName & "LOG_PERF_T_ORIFICE_DEGF"
                excelWorksheet.Cells(26, startColum + Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGC.Format, Nothing)      'TBORFA

                excelWorksheet.Cells(27, startColum).Value = "LOG_PERF_P_ORIFICE_DELTA_PSID"
                excelWorksheet.Cells(27, startColum + Column).Name = ColumnName & "LOG_PERF_P_ORIFICE_DELTA_PSID"
                excelWorksheet.Cells(27, startColum + Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_KPAD.Format, Nothing)    'PDBORA

                excelWorksheet.Cells(28, startColum).Value = "LOG_PERF_WB"
                excelWorksheet.Cells(28, startColum + Column).Name = ColumnName & "LOG_PERF_WB"
                excelWorksheet.Cells(28, startColum + Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB_KGPM.Format, Nothing)                    'WB

                excelWorksheet.Cells(29, startColum).Value = "LOG_PERF_WBCDNA"
                excelWorksheet.Cells(29, startColum + Column).Name = ColumnName & "LOG_PERF_WBCDNA"
                excelWorksheet.Cells(29, startColum + Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA_KGPM.Format, Nothing)            'WBCDNA

                excelWorksheet.Cells(30, startColum).Value = "LOG_PERF_P_BLEED1_5_PSIA"
                excelWorksheet.Cells(30, startColum + Column).Name = ColumnName & "LOG_PERF_P_BLEED1_5_PSIA"
                excelWorksheet.Cells(30, startColum + Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_BARA.Format, Nothing)    'PB

                excelWorksheet.Cells(31, startColum).Value = "LOG_PERF_T_BLEED_AVG_DEGF"
                excelWorksheet.Cells(31, startColum + Column).Name = ColumnName & "LOG_PERF_T_BLEED_AVG_DEGF"
                excelWorksheet.Cells(31, startColum + Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGC.Format, Nothing)     'TB

                excelWorksheet.Cells(32, startColum).Value = "LOG_PERF_T_BLEED_AVG_DEGF"
                excelWorksheet.Cells(32, startColum + Column).Name = ColumnName & "LOG_PERF_T_BLEED_AVG_DEGF"
                excelWorksheet.Cells(32, startColum + Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGC.Format, Nothing)     'TB

                excelWorksheet.Cells(33, startColum).Value = "LOG_PERF_WF_AVG_PPH"
                excelWorksheet.Cells(33, startColum + Column).Name = ColumnName & "LOG_PERF_WF_AVG_PPH"
                excelWorksheet.Cells(33, startColum + Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_KGH.Format, Nothing)    'WF

                excelWorksheet.Cells(34, startColum).Value = "LOG_PERF_LB1_POWER_TOT"
                excelWorksheet.Cells(34, startColum + Column).Name = ColumnName & "LOG_PERF_LB1_POWER_TOT"
                excelWorksheet.Cells(34, startColum + Column).Value = LOG_PERF_LB1_POWER_TOT.Value.ToString(LOG_PERF_LB1_POWER_TOT.Format, Nothing)  'PWGEN



                excelWorksheet.Cells(35, startColum).Value = "LOG_PERF_SHP_SL"
                excelWorksheet.Cells(35, startColum + Column).Name = ColumnName & "LOG_PERF_SHP_SL"
                excelWorksheet.Cells(35, startColum + Column).Value = LOG_PERF_SHP_SL.Value.ToString(LOG_PERF_SHP_SL.Format, Nothing)    'SHPSL

                excelWorksheet.Cells(36, startColum).Value = "LOG_PERF_DELTA_P_SL"
                excelWorksheet.Cells(36, startColum + Column).Name = ColumnName & "LOG_PERF_DELTA_P_SL"
                excelWorksheet.Cells(36, startColum + Column).Value = LOG_PERF_DELTA_P_SL.Value.ToString(LOG_PERF_DELTA_P_SL_BARA.Format, Nothing)    'SL Delta P

                excelWorksheet.Cells(37, startColum).Value = "LOG_PERF_PB_SL_PSIA"
                excelWorksheet.Cells(37, startColum + Column).Name = ColumnName & "LOG_PERF_PB_SL_PSIA"
                excelWorksheet.Cells(37, startColum + Column).Value = LOG_PERF_PB_SL_PSIA.Value.ToString(LOG_PERF_PB_SL_BARA.Format, Nothing)    'SL Bleed P

                excelWorksheet.Cells(38, startColum).Value = "LOG_PERF_DELPB"
                excelWorksheet.Cells(38, startColum + Column).Name = ColumnName & "LOG_PERF_DELPB"
                excelWorksheet.Cells(38, startColum + Column).Value = LOG_PERF_DELPB.Value.ToString(LOG_PERF_DELPB.Format, Nothing)      'DELPB

                excelWorksheet.Cells(39, startColum).Value = "LOG_PERF_PB_INST_PSIA"
                excelWorksheet.Cells(39, startColum + Column).Name = ColumnName & "LOG_PERF_PB_INST_PSIA"
                excelWorksheet.Cells(39, startColum + Column).Value = LOG_PERF_PB_INST_PSIA.Value.ToString(LOG_PERF_PB_INST_PSIA.Format, Nothing)    'Installation effect on Bleed P

                excelWorksheet.Cells(40, startColum).Value = "LOG_PERF_PBCOR"
                excelWorksheet.Cells(40, startColum + Column).Name = ColumnName & "LOG_PERF_PBCOR"
                excelWorksheet.Cells(40, startColum + Column).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR_BARA.Format, Nothing)      'PBCOR

                excelWorksheet.Cells(41, startColum).Value = "LOG_PERF_WB_SL"
                excelWorksheet.Cells(41, startColum + Column).Name = ColumnName & "LOG_PERF_WB_SL"
                excelWorksheet.Cells(41, startColum + Column).Value = LOG_PERF_WB_SL.Value.ToString(LOG_PERF_WB_SL_KGPM.Format, Nothing)      'SL Bleed AF

                excelWorksheet.Cells(42, startColum).Value = "LOG_PERF_DELWB"
                excelWorksheet.Cells(42, startColum + Column).Name = ColumnName & "LOG_PERF_DELWB"
                excelWorksheet.Cells(42, startColum + Column).Value = LOG_PERF_DELWB.Value.ToString(LOG_PERF_DELWB.Format, Nothing)      'DELWB

                excelWorksheet.Cells(43, startColum).Value = "LOG_PERF_WB_INST"
                excelWorksheet.Cells(43, startColum + Column).Name = ColumnName & "LOG_PERF_WB_INST"
                excelWorksheet.Cells(43, startColum + Column).Value = LOG_PERF_WB_INST.Value.ToString(LOG_PERF_WB_INST.Format, Nothing)  'Installation effect on WB

                excelWorksheet.Cells(44, startColum).Value = "LOG_PERF_WBCOR"
                excelWorksheet.Cells(44, startColum + Column).Name = ColumnName & "LOG_PERF_WBCOR"
                excelWorksheet.Cells(44, startColum + Column).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)      'WFCOR

                excelWorksheet.Cells(45, startColum).Value = "LOG_PERF_DELTB"
                excelWorksheet.Cells(45, startColum + Column).Name = ColumnName & "LOG_PERF_DELTB"
                excelWorksheet.Cells(45, startColum + Column).Value = LOG_PERF_DELTB.Value.ToString(LOG_PERF_DELTB.Format, Nothing)      'DELTB

                excelWorksheet.Cells(46, startColum).Value = "LOG_PERF_TBCOR_EXC_PB"
                excelWorksheet.Cells(46, startColum + Column).Name = ColumnName & "LOG_PERF_TBCOR_EXC_PB"
                excelWorksheet.Cells(46, startColum + Column).Value = LOG_PERF_TBCOR_EXC_PB.Value.ToString(LOG_PERF_TBCOR_EXC_PB.Format, Nothing)    'Excess pressure correction on TBCOR

                excelWorksheet.Cells(47, startColum).Value = "LOG_PERF_TBCOR"
                excelWorksheet.Cells(47, startColum + Column).Name = ColumnName & "LOG_PERF_TBCOR"
                excelWorksheet.Cells(47, startColum + Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)      'TBCOR

                excelWorksheet.Cells(48, startColum).Value = "LOG_PERF_DELEGT"
                excelWorksheet.Cells(48, startColum + Column).Name = ColumnName & "LOG_PERF_DELEGT"
                excelWorksheet.Cells(48, startColum + Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)    'DELEGT

                excelWorksheet.Cells(49, startColum).Value = "LOG_PERF_EGT_DP_CORR"
                excelWorksheet.Cells(49, startColum + Column).Name = ColumnName & "LOG_PERF_EGT_DP_CORR"
                excelWorksheet.Cells(49, startColum + Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing) 'Delta P Correction on EGT

                excelWorksheet.Cells(50, startColum).Value = "LOG_PERF_EGT_INST"
                excelWorksheet.Cells(50, startColum + Column).Name = ColumnName & "LOG_PERF_EGT_INST"
                excelWorksheet.Cells(50, startColum + Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)    'Installation effect on EGT

                excelWorksheet.Cells(51, startColum).Value = "LOG_PERF_EGT_EXC_PB"
                excelWorksheet.Cells(51, startColum + Column).Name = ColumnName & "LOG_PERF_EGT_EXC_PB"
                excelWorksheet.Cells(51, startColum + Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)    'Excess bleed P correction on EGT

                excelWorksheet.Cells(52, startColum).Value = "LOG_PERF_EGTCOR"
                excelWorksheet.Cells(52, startColum + Column).Name = ColumnName & "LOG_PERF_EGTCOR"
                excelWorksheet.Cells(52, startColum + Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)    'EGTCOR

                excelWorksheet.Cells(53, startColum).Value = "LOG_PERF_WF_SL"
                excelWorksheet.Cells(53, startColum + Column).Name = ColumnName & "LOG_PERF_WF_SL"
                excelWorksheet.Cells(53, startColum + Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)      'SL Fuel FLow

                excelWorksheet.Cells(54, startColum).Value = "LOG_PERF_DELWF"
                excelWorksheet.Cells(54, startColum + Column).Name = ColumnName & "LOG_PERF_DELWF"
                excelWorksheet.Cells(54, startColum + Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)      'DELWF

                excelWorksheet.Cells(55, startColum).Value = "LOG_PERF_WF_DP_CORR"
                excelWorksheet.Cells(55, startColum + Column).Name = ColumnName & "LOG_PERF_WF_DP_CORR"
                excelWorksheet.Cells(55, startColum + Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)    'Delta P Correction on WF

                excelWorksheet.Cells(56, startColum).Value = "LOG_PERF_WF_INST"
                excelWorksheet.Cells(56, startColum + Column).Name = ColumnName & "LOG_PERF_WF_INST"
                excelWorksheet.Cells(56, startColum + Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)  'Installation effect on WF

                excelWorksheet.Cells(57, startColum).Value = "LOG_PERF_WF_EXC_PB"
                excelWorksheet.Cells(57, startColum + Column).Name = ColumnName & "LOG_PERF_WF_EXC_PB"
                excelWorksheet.Cells(57, startColum + Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing) 'Excess bleed P correction on WF

                excelWorksheet.Cells(58, startColum).Value = "LOG_PERF_WFCOR"
                excelWorksheet.Cells(58, startColum + Column).Name = ColumnName & "LOG_PERF_WFCOR"
                excelWorksheet.Cells(58, startColum + Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)      'WFCOR

                ' ECS OFFSET WORKSHEET section
                excelWorksheet.Cells(59, startColum).Value = "Log_Perf_InitPBCOR"
                excelWorksheet.Cells(59, startColum + Column).Name = ColumnName & "Log_Perf_InitPBCOR"
                excelWorksheet.Cells(59, startColum + Column).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)

                excelWorksheet.Cells(60, startColum).Value = "Log_Perf_FinalPBCOR"
                excelWorksheet.Cells(60, startColum + Column).Name = ColumnName & "Log_Perf_FinalPBCOR"
                excelWorksheet.Cells(60, startColum + Column).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)

                excelWorksheet.Cells(61, startColum).Value = "Log_Perf_InitIGV"
                excelWorksheet.Cells(61, startColum + Column).Name = ColumnName & "Log_Perf_InitIGV"
                excelWorksheet.Cells(61, startColum + Column).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)

                excelWorksheet.Cells(62, startColum).Value = "Log_Perf_FinalIGV"
                excelWorksheet.Cells(62, startColum + Column).Name = ColumnName & "Log_Perf_FinalIGV"
                excelWorksheet.Cells(62, startColum + Column).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)

                excelWorksheet.Cells(63, startColum).Value = "LOG_PERF_ECS_OFFSET"
                excelWorksheet.Cells(63, startColum + Column).Name = ColumnName & "LOG_PERF_ECS_OFFSET"
                excelWorksheet.Cells(63, startColum + Column).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                ' Performance Summary, Data point, Actual
                excelWorksheet.Cells(64, startColum).Value = "LOG_PERF_PBCOR"
                excelWorksheet.Cells(64, startColum + Column).Name = ColumnName & "LOG_PERF_PBCOR"
                excelWorksheet.Cells(64, startColum + Column).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)

                excelWorksheet.Cells(65, startColum).Value = "LOG_PERF_WBCOR"
                excelWorksheet.Cells(65, startColum + Column).Name = ColumnName & "LOG_PERF_WBCOR"
                excelWorksheet.Cells(65, startColum + Column).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)

                excelWorksheet.Cells(66, startColum).Value = "LOG_PERF_EGTCOR"
                excelWorksheet.Cells(66, startColum + Column).Name = ColumnName & "LOG_PERF_EGTCOR"
                excelWorksheet.Cells(66, startColum + Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)

                excelWorksheet.Cells(67, startColum).Value = "LOG_PERF_WFCOR"
                excelWorksheet.Cells(67, startColum + Column).Name = ColumnName & "LOG_PERF_WFCOR"
                excelWorksheet.Cells(67, startColum + Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)



                excelWorksheet.Cells(68, startColum).Value = "LOG_PERF_WBCDNA_LO_LIM"
                excelWorksheet.Cells(68, startColum + Column).Name = ColumnName & "LOG_PERF_WBCDNA_LO_LIM"
                excelWorksheet.Cells(68, startColum + Column).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)

                excelWorksheet.Cells(69, startColum).Value = "LOG_PERF_WBCDNA_HI_LIM"
                excelWorksheet.Cells(69, startColum + Column).Name = ColumnName & "LOG_PERF_WBCDNA_HI_LIM"
                excelWorksheet.Cells(69, startColum + Column).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)

                excelWorksheet.Cells(70, startColum).Value = "LOG_PERF_WBCDNA"
                excelWorksheet.Cells(70, startColum + Column).Name = ColumnName & "LOG_PERF_WBCDNA"
                excelWorksheet.Cells(70, startColum + Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)


                excelWorksheet.Cells(71, startColum).Value = "LOG_PERF_FLOW_SENSOR_ACC"
                excelWorksheet.Cells(71, startColum + Column).Name = ColumnName & "LOG_PERF_FLOW_SENSOR_ACC"
                excelWorksheet.Cells(71, startColum + Column).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)




                excelWorksheet.Cells(72, startColum).Value = "Log_Perf_SCVStability"
                excelWorksheet.Cells(72, startColum + Column).Name = ColumnName & "Log_Perf_SCVStability"
                If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                    excelWorksheet.Cells(72, startColum + Column).Value = "PASSED"
                Else
                    excelWorksheet.Cells(72, startColum + Column).Value = "FAILED"
                End If

                excelWorksheet.Cells(73, startColum).Value = "STARTING_TIME_SEC"
                excelWorksheet.Cells(73, startColum + Column).Name = ColumnName & "STARTING_TIME_SEC"
                excelWorksheet.Cells(73, startColum + Column).Value = STARTING_TIME_SEC.Value.ToString(STARTING_TIME_SEC.Format, Nothing)

                excelWorksheet.Cells(74, startColum).Value = "NUMBER_OF_STARTS"
                excelWorksheet.Cells(74, startColum + Column).Name = ColumnName & "NUMBER_OF_STARTS"
                excelWorksheet.Cells(74, startColum + Column).Value = NUMBER_OF_STARTS.Value.ToString()

                excelWorksheet.Cells(75, startColum).Value = "LOG_PERF_OPERATING_TIME_CURRENT_HOURS"
                excelWorksheet.Cells(75, startColum + Column).Name = ColumnName & "LOG_PERF_OPERATING_TIME_CURRENT_HOURS"
                excelWorksheet.Cells(75, startColum + Column).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)

                excelWorksheet.Cells(76, startColum).Value = "Log_Perf_NbApuFaults"
                excelWorksheet.Cells(76, startColum + Column).Name = ColumnName & "Log_Perf_NbApuFaults"
                excelWorksheet.Cells(76, startColum + Column).Value = Log_Perf_NbApuFaults.Value.ToString()



                excelWorksheet.Cells(77, startColum).Value = "PBCOR_LIMIT_MIN"
                excelWorksheet.Cells(77, startColum + Column).Name = ColumnName & "PBCOR_LIMIT_MIN"

                excelWorksheet.Cells(78, startColum).Value = "WBCOR_LIMIT_MIN"
                excelWorksheet.Cells(78, startColum + Column).Name = ColumnName & "WBCOR_LIMIT_MIN"

                excelWorksheet.Cells(79, startColum).Value = "BLEED_PRESSURE_LIMIT_MAX"
                excelWorksheet.Cells(79, startColum + Column).Name = ColumnName & "BLEED_PRESSURE_LIMIT_MAX"


                excelWorksheet.Cells(80, startColum).Value = "WFCOR_REF"
                excelWorksheet.Cells(80, startColum + Column).Name = ColumnName & "WFCOR_REF"



                excelWorksheet.Cells(81, startColum).Value = "Log_Perf_MinSurgeMargin"
                excelWorksheet.Cells(81, startColum + Column).Name = ColumnName & "Log_Perf_MinSurgeMargin"
                If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                    excelWorksheet.Cells(81, startColum + Column).Value = "PASSED"
                Else
                    excelWorksheet.Cells(81, startColum + Column).Value = "FAILED"
                End If


            Catch Ex As Exception
                MessageBox.Show("FillAllDatas: " & Ex.ToString())
            End Try

        End Sub

        Private Sub THYWritePerfSheet2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ShowBleed As Boolean, ByVal ShowPower As Boolean)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("Sheet2")

                excelWorksheet.Cells(16, Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing) 'PBAR
                excelWorksheet.Cells(17, Column).Value = LOG_PERF_P_INLET_PSIA.Value.ToString(LOG_PERF_P_INLET_PSIA.Format, Nothing) 'PINLET
                excelWorksheet.Cells(18, Column).Value = LOG_PERF_T_INLET_AVG_DEGF.Value.ToString(LOG_PERF_T_INLET_AVG_DEGF.Format, Nothing)    'T1
                excelWorksheet.Cells(19, Column).Value = LOG_PERF_LCIT_DEGF.Value.ToString(LOG_PERF_LCIT_DEGF.Format, Nothing) 'TENIVA
                excelWorksheet.Cells(20, Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing)    'POIL
                excelWorksheet.Cells(21, Column).Value = LOG_PERF_P_GEARBOX_INH2O.Value.ToString(LOG_PERF_P_GEARBOX_INH2O.Format, Nothing) 'PSGBX
                excelWorksheet.Cells(22, Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing)    'TOS
                excelWorksheet.Cells(23, Column).Value = LOG_PERF_T_FUEL_DEGF.Value.ToString(LOG_PERF_T_FUEL_DEGF.Format, Nothing) 'TFUEL
                excelWorksheet.Cells(24, Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_PSIG.Format, Nothing) 'PFUEL
                excelWorksheet.Cells(25, Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing) 'VIBGBA
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing) 'VIBTHA
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_VIB_COOL_FAN.Value.ToString(LOG_PERF_VIB_COOL_FAN.Format, Nothing) 'VIBCFA
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing)    'XNL
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing) 'PIGV
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_ARINC_SCV_POS.Value.ToString(LOG_PERF_ARINC_SCV_POS.Format, Nothing) 'PIGV
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_PSIA.Format, Nothing) 'PCDFD
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_T_LCDT_DEGF.Value.ToString(LOG_PERF_T_LCDT_DEGF.Format, Nothing) 'TCDFD
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_T_ETC1_DEGF.Value.ToString(LOG_PERF_T_ETC1_DEGF.Format, Nothing) 'TTDEA
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_T_ETC2_DEGF.Value.ToString(LOG_PERF_T_ETC2_DEGF.Format, Nothing) 'TTDEB
                excelWorksheet.Cells(35, Column).Value = LOG_PERF_T_EGT_AVG_DEGF.Value.ToString(LOG_PERF_T_EGT_AVG_DEGF.Format, Nothing)    'EGT
                excelWorksheet.Cells(36, Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing)    'PS9

                If ShowBleed Then
                    excelWorksheet.Cells(37, Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)      'PBORFA
                    excelWorksheet.Cells(38, Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)      'TBORFA
                    excelWorksheet.Cells(39, Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing)    'PDBORA
                    excelWorksheet.Cells(40, Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)                    'WB
                    excelWorksheet.Cells(41, Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)            'WBCDNA
                    excelWorksheet.Cells(42, Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)    'PB
                    excelWorksheet.Cells(43, Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)     'TB
                End If

                ' Always show fuel flow
                excelWorksheet.Cells(44, Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing)    'WF

                If ShowPower Then
                    excelWorksheet.Cells(45, Column).Value = LOG_PERF_LB1_POWER_TOT.Value.ToString(LOG_PERF_LB1_POWER_TOT.Format, Nothing)  'PWGEN
                End If

            Catch Ex As Exception
                MessageBox.Show("THYWritePerfSheet2: " & Ex.ToString())
            End Try

        End Sub


        Private Sub EPCORWritePerfSheet3(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ShowShaftLoadOnly As Boolean)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("Sheet3")

                excelWorksheet.Cells(15, Column).Value = LOG_PERF_SHP_SL.Value.ToString(LOG_PERF_SHP_SL.Format, Nothing)    'SHPSL

                If Not ShowShaftLoadOnly Then

                    excelWorksheet.Cells(16, Column).Value = LOG_PERF_DELTA_P_SL.Value.ToString(LOG_PERF_DELTA_P_SL.Format, Nothing)    'SL Delta P
                    excelWorksheet.Cells(17, Column).Value = LOG_PERF_PB_SL_PSIA.Value.ToString(LOG_PERF_PB_SL_PSIA.Format, Nothing)    'SL Bleed P
                    excelWorksheet.Cells(18, Column).Value = LOG_PERF_DELPB.Value.ToString(LOG_PERF_DELPB.Format, Nothing)      'DELPB
                    excelWorksheet.Cells(19, Column).Value = LOG_PERF_PB_INST_PSIA.Value.ToString(LOG_PERF_PB_INST_PSIA.Format, Nothing)    'Installation effect on Bleed P
                    excelWorksheet.Cells(20, Column).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)      'PBCOR
                    excelWorksheet.Cells(21, Column).Value = LOG_PERF_WB_SL.Value.ToString(LOG_PERF_WB_SL.Format, Nothing)      'SL Bleed AF
                    excelWorksheet.Cells(22, Column).Value = LOG_PERF_DELWB.Value.ToString(LOG_PERF_DELWB.Format, Nothing)      'DELWB
                    excelWorksheet.Cells(23, Column).Value = LOG_PERF_WB_INST.Value.ToString(LOG_PERF_WB_INST.Format, Nothing)  'Installation effect on WB
                    excelWorksheet.Cells(24, Column).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)      'WFCOR
                    excelWorksheet.Cells(25, Column).Value = LOG_PERF_DELTB.Value.ToString(LOG_PERF_DELTB.Format, Nothing)      'DELTB
                    excelWorksheet.Cells(26, Column).Value = LOG_PERF_TBCOR_EXC_PB.Value.ToString(LOG_PERF_TBCOR_EXC_PB.Format, Nothing)    'Excess pressure correction on TBCOR
                    excelWorksheet.Cells(27, Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)      'TBCOR
                    excelWorksheet.Cells(28, Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)    'DELEGT
                    excelWorksheet.Cells(29, Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing) 'Delta P Correction on EGT
                    excelWorksheet.Cells(30, Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)    'Installation effect on EGT
                    excelWorksheet.Cells(31, Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)    'Excess bleed P correction on EGT
                    excelWorksheet.Cells(32, Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)    'EGTCOR
                    excelWorksheet.Cells(33, Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)      'SL Fuel FLow
                    excelWorksheet.Cells(34, Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)      'DELWF
                    excelWorksheet.Cells(35, Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)    'Delta P Correction on WF
                    excelWorksheet.Cells(36, Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)  'Installation effect on WF
                    excelWorksheet.Cells(37, Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing) 'Excess bleed P correction on WF
                    excelWorksheet.Cells(38, Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)      'WFCOR

                End If

            Catch Ex As Exception
                MessageBox.Show("EPCORWritePerfSheet3: " & Ex.ToString())
            End Try
        End Sub


        Private Sub THYWritePerfSheet3(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ShowShaftLoadOnly As Boolean)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("Sheet3")

                excelWorksheet.Cells(17, Column).Value = LOG_PERF_SHP_SL.Value.ToString(LOG_PERF_SHP_SL.Format, Nothing)    'SHPSL

                If Not ShowShaftLoadOnly Then

                    excelWorksheet.Cells(18, Column).Value = LOG_PERF_DELTA_P_SL.Value.ToString(LOG_PERF_DELTA_P_SL.Format, Nothing)    'SL Delta P
                    excelWorksheet.Cells(19, Column).Value = LOG_PERF_PB_SL_PSIA.Value.ToString(LOG_PERF_PB_SL_PSIA.Format, Nothing)    'SL Bleed P
                    excelWorksheet.Cells(20, Column).Value = LOG_PERF_DELPB.Value.ToString(LOG_PERF_DELPB.Format, Nothing)      'DELPB
                    excelWorksheet.Cells(21, Column).Value = LOG_PERF_PB_INST_PSIA.Value.ToString(LOG_PERF_PB_INST_PSIA.Format, Nothing)    'Installation effect on Bleed P
                    excelWorksheet.Cells(22, Column).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)      'PBCOR
                    excelWorksheet.Cells(23, Column).Value = LOG_PERF_WB_SL.Value.ToString(LOG_PERF_WB_SL.Format, Nothing)      'SL Bleed AF
                    excelWorksheet.Cells(24, Column).Value = LOG_PERF_DELWB.Value.ToString(LOG_PERF_DELWB.Format, Nothing)      'DELWB
                    excelWorksheet.Cells(25, Column).Value = LOG_PERF_WB_INST.Value.ToString(LOG_PERF_WB_INST.Format, Nothing)  'Installation effect on WB
                    excelWorksheet.Cells(26, Column).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)      'WFCOR
                    excelWorksheet.Cells(27, Column).Value = LOG_PERF_DELTB.Value.ToString(LOG_PERF_DELTB.Format, Nothing)      'DELTB
                    excelWorksheet.Cells(28, Column).Value = LOG_PERF_TBCOR_EXC_PB.Value.ToString(LOG_PERF_TBCOR_EXC_PB.Format, Nothing)    'Excess pressure correction on TBCOR
                    excelWorksheet.Cells(29, Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)      'TBCOR
                    excelWorksheet.Cells(30, Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)    'DELEGT
                    excelWorksheet.Cells(31, Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing) 'Delta P Correction on EGT
                    excelWorksheet.Cells(32, Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)    'Installation effect on EGT
                    excelWorksheet.Cells(33, Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)    'Excess bleed P correction on EGT
                    excelWorksheet.Cells(34, Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)    'EGTCOR
                    excelWorksheet.Cells(35, Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)      'SL Fuel FLow
                    excelWorksheet.Cells(36, Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)      'DELWF
                    excelWorksheet.Cells(37, Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)    'Delta P Correction on WF
                    excelWorksheet.Cells(38, Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)  'Installation effect on WF
                    excelWorksheet.Cells(39, Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing) 'Excess bleed P correction on WF
                    excelWorksheet.Cells(40, Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)      'WFCOR

                End If

            Catch Ex As Exception
                MessageBox.Show("THYWritePerfSheet3: " & Ex.ToString())
            End Try

        End Sub


        Private Function CreateEPCOR12958PerformanceFile(ByVal AppendOption As Boolean, ByVal TestStepNumber As Integer, ByVal ProtectPassword As String, ByVal UnProtectPassword As String, ByRef excelWorkbook As Excel.Workbook) As Boolean
            'Declaration
            Dim excelSheets As Excel.Sheets
            Dim excelWorksheet As Excel.Worksheet

            CreateEPCOR12958PerformanceFile = True

            ' The following gets the Worksheets collection
            excelSheets = excelWorkbook.Worksheets

            Try
                excelWorkbook.Worksheets(1).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(2).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(3).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(4).Unprotect(UnProtectPassword)

                If AppendOption = False Then

                    excelWorksheet = excelSheets.Item("Sheet1")
                    'Customer
                    excelWorksheet.Cells(5, 3).NumberFormat = "@"
                    excelWorksheet.Cells(5, 3).Value = UserInputsEPCOR.CustomerName

                    'Work Order
                    excelWorksheet.Cells(6, 3).NumberFormat = "@"
                    excelWorksheet.Cells(6, 3).Value = UserInputsEPCOR.WorkOrder

                    'Engine P/N
                    excelWorksheet.Cells(7, 3).NumberFormat = "@"
                    excelWorksheet.Cells(7, 3).Value = UserInputsEPCOR.PartNumber

                    'Engine S/N
                    excelWorksheet.Cells(8, 3).NumberFormat = "@"
                    excelWorksheet.Cells(8, 3).Value = UserInputsEPCOR.SerialNumber

                    'Repair Type
                    excelWorksheet.Cells(9, 3).NumberFormat = "@"
                    excelWorksheet.Cells(9, 3).Value = UserInputsEPCOR.ChoiceRepairType

                    'Date
                    excelWorksheet.Cells(5, 8).NumberFormat = "@"
                    excelWorksheet.Cells(5, 8).Value = Format(Now, "d MMMM yyyy")

                    'Fuel SG
                    excelWorksheet.Cells(6, 8).NumberFormat = "@"
                    excelWorksheet.Cells(6, 8).Value = UserInputsEPCOR.FuelSG

                    'T Fuel
                    excelWorksheet.Cells(7, 8).NumberFormat = "@"
                    excelWorksheet.Cells(7, 8).Value = UserInputsEPCOR.TFuelDegF

                    'Fuel Type
                    excelWorksheet.Cells(8, 8).NumberFormat = "@"
                    excelWorksheet.Cells(8, 8).Value = UserInputsEPCOR.FuelType

                    'Oil Type
                    excelWorksheet.Cells(9, 8).NumberFormat = "@"
                    excelWorksheet.Cells(9, 8).Value = UserInputsEPCOR.OilType

                    'Document Number
                    excelWorksheet = excelSheets.Item("Sheet4")
                    excelWorksheet.Cells(47, 1).NumberFormat = "@"
                    excelWorksheet.Cells(47, 1).Value = excelWorksheet.Cells(47, 1).Value.ToString() + Custom_Parameters("APUTestedAccordingTo").ToString()

                End If

                Select Case TestStepNumber

                    Case eTestStepGTCP131_9A.TCOND1 'RTL

                        EPCORWritePerfSheet2(excelSheets, 9, False, False)

                    Case eTestStepGTCP131_9A.TCOND2 'GEN LOAD

                        EPCORWritePerfSheet2(excelSheets, 10, False, True)
                        EPCORWritePerfSheet3(excelSheets, 8, True)

                    Case eTestStepGTCP131_9A.IGV_OFF 'IGV Offset
                        excelWorksheet = excelSheets.Item("Sheet1")

                        ' ECS OFFSET WORKSHEET section
                        excelWorksheet.Cells(24, 8).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)
                        excelWorksheet.Cells(25, 8).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)
                        excelWorksheet.Cells(26, 8).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)
                        excelWorksheet.Cells(27, 8).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)
                        excelWorksheet.Cells(28, 8).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND3 ' 2 PCKS-700 ECS HIGH

                        excelWorksheet = excelSheets.Item("Sheet1")

                        ' Performance Summary, Data point 0003, Limit.
                        '  Note: Migrating limits from excel sheet to vb code (upgrade from 07553 to 12958)
                        '        Also, updating values for manual revision 8 (Table 1311 (p1330)).
                        If HeavyRepairType Then
                            excelWorksheet.Cells(15, 5).NumberFormat = "@"
                            excelWorksheet.Cells(15, 5).Value = "51.2 (3,53) MINIMUM"   'PBCOR (Taken from Figure 1307 (p1355) because table 1311 contains a unit conversion error)
                            excelWorksheet.Cells(16, 5).NumberFormat = "@"
                            excelWorksheet.Cells(16, 5).Value = "152.2 (69,0) MINIMUM"  'WBCOR
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "1130 (610) MAXIMUM"    'EGTCOR
                        Else
                            excelWorksheet.Cells(15, 5).NumberFormat = "@"
                            excelWorksheet.Cells(15, 5).Value = "50.2 (3,46) MINIMUM"   'PBCOR
                            excelWorksheet.Cells(16, 5).NumberFormat = "@"
                            excelWorksheet.Cells(16, 5).Value = "148.9 (67,5) MINIMUM"  'WBCOR
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "1150 (621) MAXIMUM"    'EGTCOR
                        End If

                        ' Performance Summary, Data point 0003, Actual
                        excelWorksheet.Cells(15, 6).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(16, 6).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(17, 6).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(18, 6).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                        ' ECS HIGH
                        EPCORWritePerfSheet2(excelSheets, 7, True, True)
                        EPCORWritePerfSheet3(excelSheets, 5, False)

                    Case eTestStepGTCP131_9A.TCOND4 ' MES

                        excelWorksheet = excelSheets.Item("Sheet1")

                        ' Performance Summary, Data point 0004, Limit.
                        '  Note: Migrating limits from excel sheet to vb code (upgrade from 07553 to 12958)
                        '        Also, updating values for manual revision 8 (Table 1312, page 1331).
                        If HeavyRepairType Then
                            excelWorksheet.Cells(15, 7).NumberFormat = "@"
                            excelWorksheet.Cells(15, 7).Value = "51.1 (3,52) MINIMUM"   'PBCOR
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "1125 (607) MAXIMUM"    'EGTCOR
                        Else
                            excelWorksheet.Cells(15, 7).NumberFormat = "@"
                            excelWorksheet.Cells(15, 7).Value = "50.1 (3,45) MINIMUM"   'PBCOR
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "1145 (618) MAXIMUM"    'EGTCOR
                        End If
                        excelWorksheet.Cells(18, 7).NumberFormat = "@"
                        excelWorksheet.Cells(18, 7).Value = "269.0 (122,0) REFERENCE"   'WFCOR (figure 1307, page 1355)

                        ' Performance Summary, Data point 0004, Actual
                        excelWorksheet.Cells(15, 8).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(16, 8).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(17, 8).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(18, 8).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                        ' MES
                        EPCORWritePerfSheet2(excelSheets, 8, True, True)
                        EPCORWritePerfSheet3(excelSheets, 6, False)

                    Case eTestStepGTCP131_9A.TCOND5 ' FLOW SENSOR TEST STEP 1
                        excelWorksheet = excelSheets.Item("Sheet1")

                        excelWorksheet.Cells(32, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(33, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(32, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND6 ' FLOW SENSOR TEST STEP 2
                        excelWorksheet = excelSheets.Item("Sheet1")

                        excelWorksheet.Cells(34, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(35, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(34, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                        excelWorksheet.Cells(36, 8).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND7 ' MINIMUM SURGE MARGIN TEST
                        excelWorksheet = excelSheets.Item("Sheet1")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                            excelWorksheet.Cells(40, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(40, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9A.FINAL_DATA
                        excelWorksheet = excelSheets.Item("Sheet1")

                        ' PERFORMED TEST DETAILS section

                        If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                            excelWorksheet.Cells(39, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(39, 8).Value = "FAILED"
                        End If

                        excelWorksheet.Cells(41, 8).Value = STARTING_TIME_SEC.Value.ToString(STARTING_TIME_SEC.Format, Nothing)
                        excelWorksheet.Cells(42, 8).Value = NUMBER_OF_STARTS.Value.ToString()
                        excelWorksheet.Cells(43, 8).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)
                        excelWorksheet.Cells(44, 8).Value = Log_Perf_NbApuFaults.Value.ToString()

                End Select

                'TestProgress_RecordPerfStep(TestStepNumber)

                'Write the manual Entry
                excelWorksheet = excelSheets.Item("Sheet4")

                'Test Result
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

                excelWorksheet.Cells(53, 3).Value = UserInputsEPCOR.ChoiceOperator
                excelWorksheet.Cells(54, 3).Value = UserInputsEPCOR.ChoiceInspector


            Catch ex As Exception
                System.Diagnostics.Trace.WriteLine("CreateEPCOR12958PerformanceFile: " + ex.ToString())
                MessageBox.Show(ex.ToString)
                CreateEPCOR12958PerformanceFile = False
            Finally
                'Lock Sheets
                excelWorkbook.Worksheets(1).Protect(ProtectPassword)
                excelWorkbook.Worksheets(2).Protect(ProtectPassword)
                excelWorkbook.Worksheets(3).Protect(ProtectPassword)
                excelWorkbook.Worksheets(4).Protect(ProtectPassword)

            End Try

        End Function


        Private Function CreateTHY13015PerformanceFile(ByVal AppendOption As Boolean, ByVal TestStepNumber As Integer, ByVal ProtectPassword As String, ByVal UnProtectPassword As String, ByRef excelWorkbook As Excel.Workbook) As Boolean

            'Declaration
            Dim excelSheets As Excel.Sheets
            Dim excelWorksheet As Excel.Worksheet
            Dim SelectString As String = String.Empty
            Dim Cmd As OleDbCommand
            Dim Reader As OleDbDataReader
            Dim TestCellName As String = "Unknown"


            CreateTHY13015PerformanceFile = True

            ' The following gets the Worksheets collection
            excelSheets = excelWorkbook.Worksheets

            Try
                excelWorkbook.Worksheets(1).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(2).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(3).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(4).Unprotect(UnProtectPassword)

                If AppendOption = False Then

                    excelWorksheet = excelSheets.Item("Sheet1")

                    ' Serial Number
                    excelWorksheet.Cells(5, 3).NumberFormat = "@"
                    excelWorksheet.Cells(5, 3).Value = UserInputsTHY.SerialNumber

                    ' Work Order
                    excelWorksheet.Cells(6, 3).NumberFormat = "@"
                    excelWorksheet.Cells(6, 3).Value = UserInputsTHY.WorkOrder

                    ' Engine P/N
                    excelWorksheet.Cells(7, 3).NumberFormat = "@"
                    excelWorksheet.Cells(7, 3).Value = UserInputsTHY.PartNumber

                    'T Oil Type
                    excelWorksheet.Cells(8, 3).NumberFormat = "@"
                    excelWorksheet.Cells(8, 3).Value = UserInputsTHY.OilType

                    'Fuel Temp
                    excelWorksheet.Cells(5, 8).NumberFormat = "@"
                    excelWorksheet.Cells(5, 8).Value = UserInputsTHY.FuelType

                    'Oil Type
                    excelWorksheet.Cells(6, 8).NumberFormat = "@"
                    excelWorksheet.Cells(6, 8).Value = UserInputsTHY.ChoiceTestType

                    'Oil Type
                    excelWorksheet.Cells(7, 8).NumberFormat = "@"
                    excelWorksheet.Cells(7, 8).Value = UserInputsTHY.ChoiceRepairType

                    'Date
                    excelWorksheet.Cells(8, 8).NumberFormat = "@"
                    excelWorksheet.Cells(8, 8).Value = Format(Now, "d MMMM yyyy")

                    'Document Number
                    excelWorksheet = excelSheets.Item("Sheet4")
                    excelWorksheet.Cells(49, 1).NumberFormat = "@"
                    excelWorksheet.Cells(49, 1).Value = excelWorksheet.Cells(49, 1).Value.ToString() + Custom_Parameters("APUTestedAccordingTo").ToString()

                    ' Retrieve the test cell number from the test session
                    SelectString = "SELECT * FROM TestCellIdentification"

                    Try
                        Cmd = New OleDbCommand(SelectString, CELProject.Database.Connection)
                        Reader = Cmd.ExecuteReader()
                        Reader.Read()
                        TestCellName = Reader.Item("TestCelNumber").ToString()
                        Reader.Close()

                    Catch ex As Exception
                        If Not Reader.IsClosed Then
                            Reader.Close()
                        End If
                    End Try

                    ' Add the test cell identification in the right footer
                    excelWorksheet = excelSheets.Item("Sheet1")
                    excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    excelWorksheet = excelSheets.Item("Sheet2")
                    excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    excelWorksheet = excelSheets.Item("Sheet3")
                    excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    excelWorksheet = excelSheets.Item("Sheet4")
                    excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName


                End If

                Select Case TestStepNumber

                    Case eTestStepGTCP131_9A.TCOND1 'RTL

                        THYWritePerfSheet2(excelSheets, 9, False, False)

                    Case eTestStepGTCP131_9A.TCOND2 'GEN LOAD

                        THYWritePerfSheet2(excelSheets, 10, False, True)
                        THYWritePerfSheet3(excelSheets, 8, True)

                    Case eTestStepGTCP131_9A.IGV_OFF 'IGV Offset
                        excelWorksheet = excelSheets.Item("Sheet1")

                        ' ECS OFFSET WORKSHEET section
                        excelWorksheet.Cells(26, 8).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)
                        excelWorksheet.Cells(27, 8).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)
                        excelWorksheet.Cells(28, 8).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)
                        excelWorksheet.Cells(29, 8).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)
                        excelWorksheet.Cells(30, 8).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND3 ' 2 PCKS-700 ECS HIGH

                        excelWorksheet = excelSheets.Item("Sheet1")

                        ' Performance Summary, Data point 0003, Limit.
                        '  Note: Migrating limits from excel sheet to vb code (upgrade from 07553 to 12958)
                        '        Also, updating values for manual revision 8 (Table 1311 (p1330)).
                        If HeavyRepairType Then
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "51.2 (3,53) MINIMUM"   'PBCOR (Taken from Figure 1307 (p1355) because table 1311 contains a unit conversion error)
                            excelWorksheet.Cells(18, 5).NumberFormat = "@"
                            excelWorksheet.Cells(18, 5).Value = "152.2 (69,0) MINIMUM"  'WBCOR
                            excelWorksheet.Cells(19, 5).NumberFormat = "@"
                            excelWorksheet.Cells(19, 5).Value = "1130 (610) MAXIMUM"    'EGTCOR
                        Else
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "50.2 (3,46) MINIMUM"   'PBCOR
                            excelWorksheet.Cells(18, 5).NumberFormat = "@"
                            excelWorksheet.Cells(18, 5).Value = "148.9 (67,5) MINIMUM"  'WBCOR
                            excelWorksheet.Cells(19, 5).NumberFormat = "@"
                            excelWorksheet.Cells(19, 5).Value = "1150 (621) MAXIMUM"    'EGTCOR
                        End If

                        ' Performance Summary, Data point 0003, Actual
                        excelWorksheet.Cells(17, 6).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(18, 6).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(19, 6).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(20, 6).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                        ' ECS HIGH
                        THYWritePerfSheet2(excelSheets, 7, True, True)
                        THYWritePerfSheet3(excelSheets, 5, False)

                    Case eTestStepGTCP131_9A.TCOND4 ' MES

                        excelWorksheet = excelSheets.Item("Sheet1")

                        ' Performance Summary, Data point 0004, Limit.
                        '  Note: Migrating limits from excel sheet to vb code (upgrade from 07553 to 12958)
                        '        Also, updating values for manual revision 8 (Table 1312, page 1331).
                        If HeavyRepairType Then
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "51.1 (3,52) MINIMUM"   'PBCOR
                            excelWorksheet.Cells(19, 7).NumberFormat = "@"
                            excelWorksheet.Cells(19, 7).Value = "1125 (607) MAXIMUM"    'EGTCOR
                        Else
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "50.1 (3,45) MINIMUM"   'PBCOR
                            excelWorksheet.Cells(19, 7).NumberFormat = "@"
                            excelWorksheet.Cells(19, 7).Value = "1145 (618) MAXIMUM"    'EGTCOR
                        End If

                        excelWorksheet.Cells(20, 7).NumberFormat = "@"
                        excelWorksheet.Cells(20, 7).Value = "269.0 (122,0) REFERENCE"   'WFCOR (figure 1307, page 1355)

                        ' Performance Summary, Data point 0004, Actual
                        excelWorksheet.Cells(17, 8).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(18, 8).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(19, 8).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(20, 8).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                        ' MES
                        THYWritePerfSheet2(excelSheets, 8, True, True)
                        THYWritePerfSheet3(excelSheets, 6, False)

                    Case eTestStepGTCP131_9A.TCOND5 ' FLOW SENSOR TEST STEP 1
                        excelWorksheet = excelSheets.Item("Sheet1")

                        excelWorksheet.Cells(34, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(35, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(34, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND6 ' FLOW SENSOR TEST STEP 2
                        excelWorksheet = excelSheets.Item("Sheet1")

                        excelWorksheet.Cells(36, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(37, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(36, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                        excelWorksheet.Cells(38, 8).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)

                    Case eTestStepGTCP131_9A.TCOND7 ' MINIMUM SURGE MARGIN TEST
                        excelWorksheet = excelSheets.Item("Sheet1")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                            excelWorksheet.Cells(42, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(42, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9A.FINAL_DATA
                        excelWorksheet = excelSheets.Item("Sheet1")

                        ' PERFORMED TEST DETAILS section

                        If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                            excelWorksheet.Cells(41, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(41, 8).Value = "FAILED"
                        End If

                        excelWorksheet.Cells(43, 8).Value = STARTING_TIME_SEC.Value.ToString(STARTING_TIME_SEC.Format, Nothing)
                        excelWorksheet.Cells(44, 8).Value = NUMBER_OF_STARTS.Value.ToString()
                        excelWorksheet.Cells(45, 8).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)
                        excelWorksheet.Cells(46, 8).Value = Log_Perf_NbApuFaults.Value.ToString()

                End Select

                'TestProgress_RecordPerfStep(TestStepNumber)

                'Write the manual Entry
                excelWorksheet = excelSheets.Item("Sheet4")

                'Test Result
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

                'excelWorksheet.CheckBoxes("chkTestResultAccepted ").value = CELProject.IsTrue(CELProject.PERF_TEST_RESULT_ACCEPTED.Value)
                'excelWorksheet.CheckBoxes("chkTestResultRejected ").value = CELProject.IsTrue(CELProject.PERF_TEST_RESULT_REJECTED.Value)

                excelWorksheet.Cells(55, 3).Value = UserInputsTHY.ChoiceOperator
                excelWorksheet.Cells(56, 3).Value = UserInputsTHY.ChoiceInspector


            Catch ex As Exception
                System.Diagnostics.Trace.WriteLine("CreateTHY13015PerformanceFile: " + ex.ToString())
                MessageBox.Show(ex.ToString)
                CreateTHY13015PerformanceFile = False

            Finally
                'Lock Sheets
                excelWorkbook.Worksheets(1).Protect(ProtectPassword)
                excelWorkbook.Worksheets(2).Protect(ProtectPassword)
                excelWorkbook.Worksheets(3).Protect(ProtectPassword)
                excelWorkbook.Worksheets(4).Protect(ProtectPassword)

            End Try

        End Function

        Private Function CreateStandardCELPerformanceFile(ByVal AppendOption As Boolean, ByVal TestStepNumber As Integer, ByVal ProtectPassword As String, ByVal UnProtectPassword As String, ByRef excelWorkbook As Excel.Workbook) As Boolean

            'Declaration
            Dim excelSheets As Excel.Sheets
            Dim excelWorksheet As Excel.Worksheet
            Dim SelectString As String = String.Empty
            Dim Cmd As OleDbCommand
            Dim Reader As OleDbDataReader
            Dim TestCellName As String = "Unknown"

            CreateStandardCELPerformanceFile = True

            ' The following gets the Worksheets collection
            excelSheets = excelWorkbook.Worksheets

            Try
                excelWorkbook.Worksheets(1).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(2).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(3).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(4).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(5).Unprotect(UnProtectPassword)

                If AppendOption = False Then


                    excelWorksheet = excelSheets.Item("Sheet5")

                    'Date
                    excelWorksheet.Cells(1, 1).Value = "DATE"
                    excelWorksheet.Cells(1, 2).NumberFormat = "@"
                    excelWorksheet.Cells(1, 2).Name = "DATE"
                    excelWorksheet.Cells(1, 2).Value = Format(Now, "d MMMM yyyy")


                    'Document Number
                    excelWorksheet.Cells(2, 1).Value = "TestedAccordingTo"
                    excelWorksheet.Cells(2, 2).NumberFormat = "@"
                    excelWorksheet.Cells(2, 2).Name = "TestedAccordingTo"
                    excelWorksheet.Cells(2, 2).Value = Custom_Parameters("APUTestedAccordingTo").ToString()


                    Dim theType As Type = GetType(UserInputS7_GTCP131_9A)
                    Dim properties As FieldInfo() = theType.GetFields()

                    Dim cpt As Integer = 0

                    For Each prop As FieldInfo In properties

                        excelWorksheet.Cells(cpt + 3, 1).Value = prop.Name
                        excelWorksheet.Cells(cpt + 3, 2).Name = prop.Name
                        excelWorksheet.Cells(cpt + 3, 2).NumberFormat = "@"
                        excelWorksheet.Cells(cpt + 3, 2).Value = prop.GetValue(UserInputsS7)

                        cpt += 1
                    Next

                    ' Retrieve the test cell number from the test session
                    'SelectString = "SELECT * FROM TestCellIdentification"

                    'Try
                    '    Cmd = New OleDbCommand(SelectString, CELProject.Database.Connection)
                    '    Reader = Cmd.ExecuteReader()
                    '    Reader.Read()
                    '    TestCellName = Reader.Item("TestCelNumber").ToString()
                    '    Reader.Close()

                    'Catch ex As Exception
                    '    If Not Reader.IsClosed Then
                    '        Reader.Close()
                    '    End If
                    'End Try

                    '' Add the test cell identification in the right footer
                    'excelWorksheet = excelSheets.Item("Sheet1")
                    'excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    'excelWorksheet = excelSheets.Item("Sheet2")
                    'excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    'excelWorksheet = excelSheets.Item("Sheet3")
                    'excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    'excelWorksheet = excelSheets.Item("Sheet4")
                    'excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                End If

                Dim ColumnName As String

                Select Case TestStepNumber

                    Case eTestStepGTCP131_9A.TCOND1 'RTL
                        ColumnName = eTestStepGTCP131_9A.TCOND1.ToString

                        FillAllDatas(excelSheets, 1, ColumnName)

                    Case eTestStepGTCP131_9A.TCOND2 'GEN LOAD
                        ColumnName = eTestStepGTCP131_9A.TCOND2.ToString

                        FillAllDatas(excelSheets, 2, ColumnName)

                    Case eTestStepGTCP131_9A.IGV_OFF 'IGV Offset
                        ColumnName = eTestStepGTCP131_9A.IGV_OFF.ToString

                        FillAllDatas(excelSheets, 3, ColumnName)

                    Case eTestStepGTCP131_9A.TCOND3 ' 2 PCKS-700 ECS HIGH
                        ColumnName = eTestStepGTCP131_9A.TCOND3.ToString

                        FillAllDatas(excelSheets, 4, ColumnName)

                        ' Performance Summary, Data point 0003, Limit.
                        '  Note: Migrating limits from excel sheet to vb code (upgrade from 07553 to 12958)
                        '        Also, updating values for manual revision 8 (Table 1311 (p1330)).

                        excelWorksheet = excelSheets.Item("Sheet5")

                        If HeavyRepairType Then
                            excelWorksheet.Cells(77, 8).NumberFormat = "@"
                            excelWorksheet.Cells(77, 8).Value = "51.2 (3,53) MINIMUM"   'PBCOR (Taken from Figure 1307 (p1355) because table 1311 contains a unit conversion error)
                            excelWorksheet.Cells(78, 8).NumberFormat = "@"
                            excelWorksheet.Cells(78, 8).Value = "152.2 (69,0) MINIMUM"  'WBCOR
                            excelWorksheet.Cells(79, 8).NumberFormat = "@"
                            excelWorksheet.Cells(79, 8).Value = "1130 (610) MAXIMUM"    'EGTCOR
                        Else
                            excelWorksheet.Cells(77, 8).NumberFormat = "@"
                            excelWorksheet.Cells(77, 8).Value = "50.2 (3,46) MINIMUM"   'PBCOR
                            excelWorksheet.Cells(78, 8).NumberFormat = "@"
                            excelWorksheet.Cells(78, 8).Value = "148.9 (67,5) MINIMUM"  'WBCOR
                            excelWorksheet.Cells(79, 8).NumberFormat = "@"
                            excelWorksheet.Cells(79, 8).Value = "1150 (621) MAXIMUM"    'EGTCOR
                        End If

                    Case eTestStepGTCP131_9A.TCOND4 ' MES
                        ColumnName = eTestStepGTCP131_9A.TCOND4.ToString

                        FillAllDatas(excelSheets, 5, ColumnName)

                        excelWorksheet = excelSheets.Item("Sheet5")

                        ' Performance Summary, Data point 0004, Limit.
                        '  Note: Migrating limits from excel sheet to vb code (upgrade from 07553 to 12958)
                        '        Also, updating values for manual revision 8 (Table 1312, page 1331).
                        If HeavyRepairType Then
                            excelWorksheet.Cells(77, 9).NumberFormat = "@"
                            excelWorksheet.Cells(77, 9).Value = "51.1 (3,52) MINIMUM"   'PBCOR
                            excelWorksheet.Cells(79, 9).NumberFormat = "@"
                            excelWorksheet.Cells(79, 9).Value = "1125 (607) MAXIMUM"    'EGTCOR
                        Else
                            excelWorksheet.Cells(77, 9).NumberFormat = "@"
                            excelWorksheet.Cells(77, 9).Value = "50.1 (3,45) MINIMUM"   'PBCOR
                            excelWorksheet.Cells(79, 9).NumberFormat = "@"
                            excelWorksheet.Cells(79, 9).Value = "1145 (618) MAXIMUM"    'EGTCOR
                        End If

                        excelWorksheet.Cells(80, 9).NumberFormat = "@"
                        excelWorksheet.Cells(80, 9).Value = "269.0 (122,0) REFERENCE"   'WFCOR (figure 1307, page 1355)

                    Case eTestStepGTCP131_9A.TCOND5 ' FLOW SENSOR TEST STEP 1
                        ColumnName = eTestStepGTCP131_9A.TCOND5.ToString

                        FillAllDatas(excelSheets, 6, ColumnName)

                    Case eTestStepGTCP131_9A.TCOND6 ' FLOW SENSOR TEST STEP 2
                        ColumnName = eTestStepGTCP131_9A.TCOND6.ToString

                        FillAllDatas(excelSheets, 7, ColumnName)

                    Case eTestStepGTCP131_9A.TCOND7 ' MINIMUM SURGE MARGIN TEST
                        ColumnName = eTestStepGTCP131_9A.TCOND7.ToString

                        FillAllDatas(excelSheets, 8, ColumnName)

                    Case eTestStepGTCP131_9A.FINAL_DATA
                        ColumnName = eTestStepGTCP131_9A.FINAL_DATA.ToString

                        FillAllDatas(excelSheets, 9, ColumnName)

                End Select

                'Write the manual Entry
                excelWorksheet = excelSheets.Item("Sheet4")

                'Test Result
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

                ' Hide the worksheet with the raw data
                excelWorksheet = excelSheets.Item("Sheet5")
                excelWorksheet.Visible = Excel.XlSheetVisibility.xlSheetHidden


            Catch ex As Exception
                System.Diagnostics.Trace.WriteLine("CreateStandardCELPerformanceFile: " + ex.ToString())
                MessageBox.Show(ex.ToString)
                CreateStandardCELPerformanceFile = False

            Finally
                'Lock Sheets
                excelWorkbook.Worksheets(1).Protect(ProtectPassword)
                excelWorkbook.Worksheets(2).Protect(ProtectPassword)
                excelWorkbook.Worksheets(3).Protect(ProtectPassword)
                excelWorkbook.Worksheets(4).Protect(ProtectPassword)
                excelWorkbook.Worksheets(5).Protect(ProtectPassword)

            End Try

        End Function

        Private Sub Init_Arrays()

            Dim i As Integer
            Dim rs As New ADODB.Recordset
            Dim SelectString As String

            Try
                'TODO: Amelioration, l'information de la table GTCP131_9A_MES,ECS etc. devrait être retirés d'Access et mis
                'carrement a l'initialisation de cette classe via Array statique aussi pour permettre de tracker les changements via SVN

                'Read Table 1312A GTCP131-9A
                SelectString = "SELECT * FROM GTCP131_9A_MES ORDER BY [TAMB]"
                If DatabaseHelper.OpenRecordset(rs, SelectString, cnMain, True) Then
                    ReDim GTCP131_9A_MES(DatabaseHelper.RecordCount(rs) - 1, 5)
                    For i = 0 To GTCP131_9A_MES.GetLength(0) - 1
                        GTCP131_9A_MES(i, 0) = rs.Fields("TAMB").Value
                        GTCP131_9A_MES(i, 1) = rs.Fields("DWB").Value
                        GTCP131_9A_MES(i, 2) = rs.Fields("DPB").Value
                        GTCP131_9A_MES(i, 3) = rs.Fields("DTB").Value
                        GTCP131_9A_MES(i, 4) = rs.Fields("DEGT").Value
                        GTCP131_9A_MES(i, 5) = rs.Fields("DWF").Value
                        rs.MoveNext()
                    Next i
                    DatabaseHelper.CloseRecordset(rs)
                End If

                'Read Table 1313A GTCP131-9A
                SelectString = "SELECT * FROM GTCP131_9A_ECS ORDER BY [TAMB]"
                If DatabaseHelper.OpenRecordset(rs, SelectString, cnMain, True) Then
                    ReDim GTCP131_9A_ECS(DatabaseHelper.RecordCount(rs) - 1, 5)
                    For i = 0 To GTCP131_9A_ECS.GetLength(0) - 1
                        GTCP131_9A_ECS(i, 0) = rs.Fields("TAMB").Value
                        GTCP131_9A_ECS(i, 1) = rs.Fields("DWB").Value
                        GTCP131_9A_ECS(i, 2) = rs.Fields("DPB").Value
                        GTCP131_9A_ECS(i, 3) = rs.Fields("DTB").Value
                        GTCP131_9A_ECS(i, 4) = rs.Fields("DEGT").Value
                        GTCP131_9A_ECS(i, 5) = rs.Fields("DWF").Value
                        rs.MoveNext()
                    Next i
                    DatabaseHelper.CloseRecordset(rs)
                End If

                'Read Figure 1303 GTCP131-9A
                SelectString = "SELECT * FROM GTCP131_9A_WBCDNA_LIMITS ORDER BY [T_INLET_AVG]"
                If DatabaseHelper.OpenRecordset(rs, SelectString, cnMain, True) Then
                    ReDim GTCP131_9A_WBCDNA_LIMITS(DatabaseHelper.RecordCount(rs) - 1, 2)
                    For i = 0 To GTCP131_9A_WBCDNA_LIMITS.GetLength(0) - 1
                        GTCP131_9A_WBCDNA_LIMITS(i, 0) = rs.Fields("T_INLET_AVG").Value
                        GTCP131_9A_WBCDNA_LIMITS(i, 1) = rs.Fields("LOW_LIMIT").Value
                        GTCP131_9A_WBCDNA_LIMITS(i, 2) = rs.Fields("HIGH_LIMIT").Value
                        rs.MoveNext()
                    Next i
                    DatabaseHelper.CloseRecordset(rs)
                End If

            Catch Ex As Exception
                MessageBox.Show("InitArrays_GTCP131_9A: " & Ex.ToString())
            End Try
        End Sub


        Protected Overrides Sub Init_Engine_Channels_Value()

            Try

                'Read the mass flow meter value
                FM_INITIAL_VOLUME_LITERS.Value = New KineticaRT.Values.Double(FM_TOTAL_VOLUME_LITERS.Value.ToDouble())

                CELProject.PERF_TEST_STEP_NB.Value = Values.Zero

                Select Case CELProject.Project.Number
                    Case 12917
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsRevima.LHV)
                        LOAD_AIR_VALVE_DEMAND.Value = New KineticaRT.Values.Double(15.0)
                    Case 11813
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsChine.LHV)
                        LOAD_AIR_VALVE_DEMAND.Value = New KineticaRT.Values.Double(15.0)

                    Case 12958
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsEPCOR.LHV)
                        LOAD_AIR_VALVE_DEMAND.Value = New KineticaRT.Values.Double(15.0)

                    Case 13015
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsTHY.LHV)
                        LOAD_AIR_VALVE_DEMAND.Value = New KineticaRT.Values.Double(100.0)

                    Case 17865
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsS7.LHV)
                        LOAD_AIR_VALVE_DEMAND.Value = New KineticaRT.Values.Double(100.0)

                    Case Else
                        Debug.Assert(False, "Compute_Time: Code Missign for this project")
                End Select

                DRESS_KIT.Value = New KineticaRT.Values.Text(EngineConfiguration.Dresskit.ToString)

            Catch Ex As Exception
                MessageBox.Show("Init_Engine_Channels_Value: " & Ex.ToString())
            End Try
        End Sub


        Protected Overrides Sub Init_Engine_Controls()

            Try

                TestStep_List.SelectedIndex = 0

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

                Return True

            Catch ex As Exception
                MessageBox.Show("CreateCustomHeaderLogFile: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
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
                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = "STEP: " & TestStep_List.SelectedItem
                End If

                ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
                RowIndex += 2
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = "Values"
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).Font.Bold = True
                ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

            Catch ex As Exception
                MessageBox.Show("UpdateCustomHeaderLogFile: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
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

        Private Sub OnENGINE_START(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If (CELProject.IsFalse(newVal)) Then
                    Return
                End If

                ' Perform the action
                ARCHIVE_RECORD_BTN.Value = Values.True

            Catch ex As System.Exception
                MessageBox.Show("OnENGINE_START: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub OnENGINE_STOP(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsFalse(CELProject.ARCHIVE_PLAY.Value) Then
                    If (CELProject.IsTrue(newVal)) Then
                        ARCHIVE_STOP_BTN.Value = Values.True
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("OnENGINE_STOP: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Function Compute_BleedAirFlow(ByVal TOrifice_DEGF As Double,
                                                ByVal POrifice_PSIA As Double,
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
                MessageBox.Show("GTCP131_9A_Sequence.Compute_BleedAirFlow: " & Ex.ToString)
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

                If sender.Name = "Log_Perf_CaptureFinal" Then
                    Log_Perf_FinalIGV.Value = ARINC_IGV_POS.Value
                    Log_Perf_FinalPBCOR.Value = PBCOR_MEAN.Value

                ElseIf sender.Name = "Log_Perf_CaptureInitial" Then
                    Log_Perf_InitIGV.Value = ARINC_IGV_POS.Value
                    Log_Perf_InitPBCOR.Value = PBCOR_MEAN.Value
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
                Log_Perf_MinSurgeMargin.Value = Values.False
                Log_Perf_SCVStability.Value = Values.False
                Log_Perf_InitPBCOR.Value = Values.Zero
                Log_Perf_InitIGV.Value = Values.Zero
                Log_Perf_FinalPBCOR.Value = Values.Zero
                Log_Perf_FinalIGV.Value = Values.Zero
                Log_Perf_NbApuFaults.Value = Values.Zero

                VIB_GEARBOX_MAX_IPS_PEAK_PERSISTENT.Value = Values.Zero
                VIB_TURBINE_MAX_IPS_PEAK_PERSISTENT.Value = Values.Zero
                VIB_COOLING_FAN_MAX_IPS_PEAK_PERSISTENT.Value = Values.Zero
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

                    Case 11813 ' China Southern

                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateChina11813PerformanceFile)

                    Case 12917 ' Révima

                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateRevima12917PerformanceFile)

                    Case 12958 ' EPCOR update (from 05306, 07553)

                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateEPCOR12958PerformanceFile)

                    Case 13015 ' THY

                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateTHY13015PerformanceFile)

                        'Case 17865 ' S7

                        '    NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateStandardCELPerformanceFile)

                    Case Else

                        'Debug.Assert(False, "OnPerf_TAKE_POINT_BTN, Code Missign for this project")
                        ' By default, we use the standard CEL performance report that can be customized by changing the Excel file only and not the report code
                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateStandardCELPerformanceFile)

                End Select

                CELProject.DoLogging(AddressOf UpdateCustomHeaderLogFile, True, CELProject.EngineModel, NewFileCreated)
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


        Private Sub OnWinDMMButton(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Return
            End If

            If (CELProject.IsFalse(newVal)) Then
                Return
            End If

            Try
                Dim proc As New System.Diagnostics.Process()
                Dim VarInfo As New System.Diagnostics.ProcessStartInfo()

                VarInfo.FileName = EngineConfiguration.WinDMMUtilityExeFile
                VarInfo.WorkingDirectory = "c:\Honeywell\WinDMM"
                VarInfo.UseShellExecute = False
                proc.StartInfo = VarInfo

                If File.Exists(EngineConfiguration.WinDMMUtilityExeFile) Then
                    proc.Start()
                Else
                    Debug.Assert(False, "The file " + EngineConfiguration.WinDMMUtilityExeFile + " does not exist")
                End If

            Catch ex As System.Exception
                MessageBox.Show("WinDMM Button: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            WINDMM_BTN.Value = Values.False

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

                    Case 13015, 17865
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

                CELProject.PERF_TEST_STEP_NB.Value = New KineticaRT.Values.Integer(TestStep_List.SelectedIndex)

            Catch ex As Exception
                MessageBox.Show("OnTestStep_List_SelectedIndexChanged: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

        End Sub

        Protected Overrides Sub OnTestSessionClosing(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)

            Try
                MyBase.OnTestSessionClosing(newStatus)

                If CELProject.EngineModel <> "GTCP131-9A" Then
                    Return
                End If

                If Not (FlowmeterCtrl Is Nothing) Then
                    FlowmeterCtrl.Dispose()
                End If

                'Close the connection to the database
                DatabaseHelper.CloseConnection(cnMain)

                'If TestProgressMgr IsNot Nothing Then
                '    TestProgress_Save()
                '    TestProgressMgr.Dispose()
                '    TestProgressMgr = Nothing
                'End If


            Catch ex As Exception
                MessageBox.Show("OnTestSessionClosing: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionInitialized(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionInitialized(newStatus)

                If CELProject.EngineModel <> "GTCP131-9A" Then
                    Return
                End If

                Select Case CELProject.Project.Number

                    Case 12917
                        UserInputsRevima.MapUserInputsParameters(CELProject)
                        If UserInputsRevima.RepairType.ToUpper() = "HEAVY" Then
                            HeavyRepairType = True
                        Else
                            HeavyRepairType = False
                        End If

                    Case 11813
                        UserInputsChine.MapUserInputsParameters(CELProject)
                        If UserInputsChine.RepairType.ToUpper() = "HEAVY" Then
                            HeavyRepairType = True
                        Else
                            HeavyRepairType = False
                        End If

                    Case 12958
                        UserInputsEPCOR.MapUserInputsParameters(CELProject)
                        If UserInputsEPCOR.ChoiceRepairType.ToUpper() = "HEAVY" Then
                            HeavyRepairType = True
                        Else
                            HeavyRepairType = False
                        End If

                    Case 13015
                        UserInputsTHY.MapUserInputsParameters(CELProject)
                        If UserInputsTHY.ChoiceRepairType.ToUpper() = "HEAVY" Then
                            HeavyRepairType = True
                        Else
                            HeavyRepairType = False
                        End If

                    Case 17865
                        UserInputsS7.MapUserInputsParameters(CELProject)
                        If UserInputsS7.ChoiceRepairType.ToUpper() = "HEAVY" Then
                            HeavyRepairType = True
                        Else
                            HeavyRepairType = False
                        End If

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


                ' Tables 1311 (p1330) and 1312 (p1331)
                If HeavyRepairType Then
                    MinPBCOR_ECS = 51.2
                    MinPBCOR_MES = 51.1
                    MaxEGTCOR_ECS = 1130.0
                    MaxEGTCOR_MES = 1125.0
                    MinWBCOR_ECS = 152.2
                Else
                    MinPBCOR_ECS = 50.2
                    MinPBCOR_MES = 50.1
                    MaxEGTCOR_ECS = 1150.0
                    MaxEGTCOR_MES = 1145.0
                    MinWBCOR_ECS = 148.9
                End If

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

                MessageBox.Show("GTCP131_9A_Sequence.OnTestSessionOpened: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try
        End Sub

        Protected Overrides Sub OnTestSessionRunning(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)

            Try
                MyBase.OnTestSessionRunning(newStatus)

                If CELProject.EngineModel <> "GTCP131-9A" Then
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

                'Test Progress Initialization (first in Révima)
                Select Case CELProject.Project.Number
                    Case 12917, 12958, 13015, 17865
                        'TestProgress_Initialize()
                    Case 11813
                        ' Do nothing
                    Case Else
                        Debug.Assert(False, "OnTestSessionRunning: Test progress not configured for this project")
                End Select


            Catch ex As Exception
                MessageBox.Show("OnTestSessionRunning: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub AddHandlers()

            AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN
            AddHandler Log_Perf_CaptureFinal.ValueChanged, AddressOf OnCapture
            AddHandler Log_Perf_CaptureInitial.ValueChanged, AddressOf OnCapture
            AddHandler HONEYWELL_COMPUTER_SELECTION_BTN.ValueChanged, AddressOf OnHONEYWELL_COMPUTER_SELECTION_BTN
            AddHandler CELProject.LOG_TAKE_POINT_BTN.ValueChanged, AddressOf OnLOG_TAKE_POINT_BTN
            AddHandler PERF_NEW_TEST_OPC.ValueChanged, AddressOf OnNEW_TEST_BTN
            AddHandler CELProject.PERF_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnPERF_CLOSE_WINDOW_BTN
            AddHandler CELProject.PERF_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnPERF_OPEN_WINDOW_BTN
            AddHandler CELProject.PERF_TAKE_POINT_BTN.ValueChanged, AddressOf OnPERF_TAKE_POINT_BTN
            AddHandler PERF_TEST_RESULT_ACCEPTED.ValueChanged, AddressOf OnPERFTestResultAcceptedCheckBox
            AddHandler PERF_TEST_RESULT_REJECTED.ValueChanged, AddressOf OnPERFTestResultRejectedCheckBox
            AddHandler READY_TO_LOAD_IND.ValueChanged, AddressOf OnAcceleratingTime_Calc
            AddHandler TestStep_List.SelectedIndexChanged, AddressOf OnTestStep_List_SelectedIndexChanged

            AddHandler WINDMM_BTN.ValueChanged, AddressOf OnWinDMMButton
            AddHandler START_BTN.ValueChanged, AddressOf OnENGINE_START
            AddHandler ENGINE_AT_REST_IND.ValueChanged, AddressOf OnENGINE_STOP

            AddHandler QUICK_LOAD_AIR_VALVE_OPEN_BTN.ValueChanged, AddressOf OnLOAD_AIR_VALVES_OPEN

        End Sub
        Protected Overrides Sub RemoveHandlers()

            If Not (READY_TO_LOAD_IND Is Nothing) Then
                RemoveHandler READY_TO_LOAD_IND.ValueChanged, AddressOf OnAcceleratingTime_Calc
            End If

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN
            End If

            If Not (Log_Perf_CaptureFinal Is Nothing) Then
                RemoveHandler Log_Perf_CaptureFinal.ValueChanged, AddressOf OnCapture
            End If

            If Not (Log_Perf_CaptureInitial Is Nothing) Then
                RemoveHandler Log_Perf_CaptureInitial.ValueChanged, AddressOf OnCapture
            End If
            If Not (HONEYWELL_COMPUTER_SELECTION_BTN Is Nothing) Then
                RemoveHandler HONEYWELL_COMPUTER_SELECTION_BTN.ValueChanged, AddressOf OnHONEYWELL_COMPUTER_SELECTION_BTN
            End If

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

            If Not (WINDMM_BTN Is Nothing) Then
                RemoveHandler WINDMM_BTN.ValueChanged, AddressOf OnWinDMMButton
            End If

            If Not (TestStep_List Is Nothing) Then
                RemoveHandler TestStep_List.SelectedIndexChanged, AddressOf OnTestStep_List_SelectedIndexChanged
            End If

            If Not (QUICK_LOAD_AIR_VALVE_OPEN_BTN Is Nothing) Then
                RemoveHandler QUICK_LOAD_AIR_VALVE_OPEN_BTN.ValueChanged, AddressOf OnLOAD_AIR_VALVES_OPEN
            End If

            If Not (START_BTN Is Nothing) Then
                RemoveHandler START_BTN.ValueChanged, AddressOf OnENGINE_START
            End If

            If Not (ENGINE_AT_REST_IND Is Nothing) Then
                RemoveHandler ENGINE_AT_REST_IND.ValueChanged, AddressOf OnENGINE_STOP
            End If

        End Sub

        Protected Overrides Sub Init_Engine_Controllers()

            Dim PersistentValuesChannels() As String = {"NUMBER_OF_STARTS",
                                                           "OPERATING_TIME_CURRENT_SEC",
                                                           "FUEL_USED_INITIAL_LITERS",
                                                           "FUEL_USED_TOTAL_LITERS",
                                                           "PERF_TEST_RESULT_ACCEPTED",
                                                           "PERF_TEST_RESULT_REJECTED",
                                                           "Log_Perf_MinSurgeMargin",
                                                           "Log_Perf_SCVStability",
                                                           "Log_Perf_InitPBCOR",
                                                           "Log_Perf_InitIGV",
                                                           "Log_Perf_FinalPBCOR",
                                                           "Log_Perf_FinalIGV",
                                                           "Log_Perf_NbApuFaults",
                                                           "VIB_GEARBOX_MAX_IPS_PEAK_PERSISTENT",
                                                           "VIB_TURBINE_MAX_IPS_PEAK_PERSISTENT",
                                                           "VIB_COOLING_FAN_MAX_IPS_PEAK_PERSISTENT"}

            Dim EngineSN As String = Nothing
            Dim WorkOrder As String = Nothing

            Select Case CELProject.Project.Number

                Case 12917
                    EngineSN = UserInputsRevima.EngineSerialNumber
                    WorkOrder = UserInputsRevima.WorkOrder

                Case 11813
                    EngineSN = UserInputsChine.EngineSerialNumber
                    WorkOrder = UserInputsChine.WorkOrder

                Case 12958
                    EngineSN = UserInputsEPCOR.SerialNumber
                    WorkOrder = UserInputsEPCOR.WorkOrder

                Case 13015
                    EngineSN = UserInputsTHY.SerialNumber
                    WorkOrder = UserInputsTHY.WorkOrder

                Case 17865
                    EngineSN = UserInputsS7.SerialNumber
                    WorkOrder = UserInputsS7.WorkOrder

                Case Else
                    Debug.Assert(False, "OnTestSessionRunning: Code Missing for this project")

            End Select

            Dim PersistentValuesFileName As String = "C:\CELDAS DATA\" & CELProject.EngineModel & "\" & EngineSN & "\" & WorkOrder & "\Log\PersistentValues.xml"
            PersistentValuesCtrl = New PersistentValuesController(Me, PersistentValuesChannels, 1, PersistentValuesFileName)

            Select Case CELProject.Project.Number

                Case 12917
                    Dim FuelUsedTotal As Double = FUEL_USED_TOTAL_LITERS.Value.ToDouble()
                    FM_INITIAL_VOLUME_LITERS.Value = New KineticaRT.Values.Double(FM_TOTAL_VOLUME_LITERS.Value.ToDouble())
                    FUEL_USED_INITIAL_LITERS.Value = New KineticaRT.Values.Double(FuelUsedTotal)

                Case 11813, 12958, 13015, 17865
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

                Case 12917
                    RealNumberOfTInletProbe = 4
                    RealNumberOfTBleedProbe = 1
                    RealNumberOfTEgtProbe = 6

                Case 11813
                    RealNumberOfTInletProbe = 10
                    RealNumberOfTBleedProbe = 1
                    RealNumberOfTEgtProbe = 6

                Case 12958
                    Dim ConfigPrefix As String = "//ConfigurationSettings/" & CELProject.EngineModel & "/"
                    RealNumberOfTInletProbe = CELProject.Project.ReadOneIntegerConfigItem(ConfigPrefix & "T_INLET_NUMBER_PROBE", 0) ' 10
                    RealNumberOfTEgtProbe = CELProject.Project.ReadOneIntegerConfigItem(ConfigPrefix & "T_EGT_NUMBER_PROBE", 0)     ' 12
                    RealNumberOfTBleedProbe = CELProject.Project.ReadOneIntegerConfigItem(ConfigPrefix & "T_BLEED_NUMBER_PROBE", 0) '  4

                    Debug.Assert(RealNumberOfTInletProbe <> 0 And RealNumberOfTEgtProbe <> 0 And RealNumberOfTBleedProbe <> 0)

                Case 13015
                    Dim ConfigPrefix As String = "//ConfigurationSettings/" & CELProject.EngineModel & "/"
                    RealNumberOfTInletProbe = CELProject.Project.ReadOneIntegerConfigItem(ConfigPrefix & "T_INLET_NUMBER_PROBE", 0) ' 10
                    RealNumberOfTEgtProbe = CELProject.Project.ReadOneIntegerConfigItem(ConfigPrefix & "T_EGT_NUMBER_PROBE", 0)     ' 12
                    RealNumberOfTBleedProbe = CELProject.Project.ReadOneIntegerConfigItem(ConfigPrefix & "T_BLEED_NUMBER_PROBE", 0) '  4

                    Debug.Assert(RealNumberOfTInletProbe <> 0 And RealNumberOfTEgtProbe <> 0 And RealNumberOfTBleedProbe <> 0)

                Case 17865
                    Dim ConfigPrefix As String = "//ConfigurationSettings/" & CELProject.EngineModel & "/"
                    RealNumberOfTInletProbe = CELProject.Project.ReadOneIntegerConfigItem(ConfigPrefix & "T_INLET_NUMBER_PROBE", 0) ' 10
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

            'Configuring ECS Control
            ECSCtrl = New ECSController(Me, 0, 100, 36, "ECS_BTN", "ECS_DEMAND", "ECS_UP_DEMAND", "ECS_DOWN_DEMAND", ECS_Display)
            ECSCtrl.Memorize = False
            ECSCtrl.Enable = False
            ECSCtrl.UseIGVPositionAsFirstValue = False

            VariSpeedCtrl = New ECSController(Me, 0, 5, 2.5, "VARISPEED_BTN", "VARISPEED_DEMAND", "VARISPEED_UP_DEMAND", "VARISPEED_DOWN_DEMAND", Varispeed_Display, 0.2)
            VariSpeedCtrl.Memorize = True
            VariSpeedCtrl.Enable = False
            VariSpeedCtrl.UseIGVPositionAsFirstValue = False

            StarterMaxCurrentRecordCtrl = New MinMaxRecordController(Me, MinMaxRecordController.eRecordType.Maximum, "STARTER_RECTIFIER_CURRENT_AMP", "STARTER_RECTIFIER_CURRENT_AMP_MAX", "START_BTN")
            StarterMaxVoltageRecordCtrl = New MinMaxRecordController(Me, MinMaxRecordController.eRecordType.Maximum, "STARTER_RECTIFIER_VOLTAGE_VDC", "STARTER_RECTIFIER_VOLTAGE_VDC_MAX", "START_BTN")

            StartTimerCtrl = New TimerController(Me, "STARTING_TIME_START_CONDITION_LATCH", "STARTING_TIME_STOP_CONDITION_LATCH", "STARTING_TIME_CONDITION_RESET", "STARTING_TIME_SEC", Nothing, 0, "NONE")

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

                Case 12917
                    myLBData.FieldFlashingTimeMsec = 1000
                    myLBData.FieldFlashingSignalName = "FIELD_FLASHING"
                    myLBData.FieldFlashingTriggerSignalName = "LB_GCU_BTN"

                Case 11813, 12958, 13015, 17865
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

                Case 11813
                    LoadBankCtrl.AddStage(1, "LB1_1")
                    LoadBankCtrl.AddStage(2, "LB1_2")
                    LoadBankCtrl.AddStage(2, "LB1_3")
                    LoadBankCtrl.AddStage(5, "LB1_4")
                    LoadBankCtrl.AddStage(10, "LB1_5")
                    LoadBankCtrl.AddStage(20, "LB1_6")
                    LoadBankCtrl.AddStage(40, "LB1_7")
                    LoadBankCtrl.AddStage(40, "LB1_8")
                    LoadBankCtrl.AddStage(100, "LB1_9")
                    LoadBankCtrl.AddStage(100, "LB1_10")
                    LoadBankCtrl.AddStage(100, "LB1_11")

                Case 12958 'maximum allowed is 100 kW, define only required stage
                    LoadBankCtrl.AddStage(1, "LB1_1")
                    LoadBankCtrl.AddStage(1, "LB1_2")
                    LoadBankCtrl.AddStage(2, "LB1_3")
                    LoadBankCtrl.AddStage(4, "LB1_4")
                    LoadBankCtrl.AddStage(8, "LB1_5")
                    LoadBankCtrl.AddStage(15, "LB1_6")
                    LoadBankCtrl.AddStage(25, "LB1_7")
                    LoadBankCtrl.AddStage(50, "LB1_8")

                Case 13015
                    LoadBankCtrl.AddStage(1, "LB1_1")
                    LoadBankCtrl.AddStage(1, "LB1_2")
                    LoadBankCtrl.AddStage(2, "LB1_3")
                    LoadBankCtrl.AddStage(4, "LB1_4")
                    LoadBankCtrl.AddStage(8, "LB1_5")
                    LoadBankCtrl.AddStage(14, "LB1_6")
                    LoadBankCtrl.AddStage(23, "LB1_7")
                    LoadBankCtrl.AddStage(47, "LB1_8")

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

            If Not (ECSCtrl Is Nothing) Then
                ECSCtrl.Dispose()
            End If

            If Not (VariSpeedCtrl Is Nothing) Then
                VariSpeedCtrl.Dispose()
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

                Return_Value.Add(MyBase.InitializeChannel(START_BTN, "START_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(ENGINE_AT_REST_IND, "ENGINE_AT_REST_IND"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_STOP_BTN, "ARCHIVE_STOP_BTN"))

                Return_Value.Add(MyBase.InitializeChannel(BLEED_BTN, "BLEED_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_RECORD_BTN, "ARCHIVE_RECORD_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(DRESS_KIT, "DRESS_KIT"))
                Return_Value.Add(MyBase.InitializeChannel(FLHV, "FLHV"))
                Return_Value.Add(MyBase.InitializeChannel(FUEL_USED_INITIAL_LITERS, "FUEL_USED_INITIAL_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(FUEL_USED_TOTAL_LITERS, "FUEL_USED_TOTAL_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(FM_INITIAL_VOLUME_LITERS, "FM_INITIAL_VOLUME_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(FM_TOTAL_VOLUME_LITERS, "FM_TOTAL_VOLUME_LITERS"))

                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_CLOSE_CMD, "INLET_FLAP_CLOSE_CMD"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_OPEN_CMD, "INLET_FLAP_OPEN_CMD"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_MOVE_CMD, "INLET_FLAP_MOVE_CMD"))

                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_OPEN_REQUEST_IND, "INLET_FLAP_OPEN_REQUEST_IND"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_CLOSE_REQUEST_IND, "INLET_FLAP_CLOSE_REQUEST_IND"))

                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_CLOSED_IND, "INLET_FLAP_CLOSED_IND"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_MOVEMENT_IND, "INLET_FLAP_MOVEMENT_IND"))
                'Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_OPENED_IND, "INLET_FLAP_OPENED_IND"))


                Return_Value.Add(MyBase.InitializeChannel(LOAD_AIR_VALVE_DEMAND, "LOAD_AIR_VALVE_DEMAND"))
                Return_Value.Add(MyBase.InitializeChannel(HONEYWELL_COMPUTER_SELECTION_BTN, "HONEYWELL_COMPUTER_SELECTION_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_LB1_POWER_TOT, "LOG_PERF_LB1_POWER_TOT"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_BLEED_AVG_DEGF, "LOG_PERF_T_BLEED_AVG_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_EGT_AVG_DEGF, "LOG_PERF_T_EGT_AVG_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_INLET_AVG_DEGF, "LOG_PERF_T_INLET_AVG_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(NUMBER_OF_STARTS, "NUMBER_OF_STARTS"))
                Return_Value.Add(MyBase.InitializeChannel(OPERATING_TIME_CURRENT_SEC, "OPERATING_TIME_CURRENT_SEC"))
                Return_Value.Add(MyBase.InitializeChannel(T_INLET_AVG_DEGF, "T_INLET_AVG_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(READY_TO_LOAD_IND, "READY_TO_LOAD_IND"))
                Return_Value.Add(MyBase.InitializeChannel(STARTING_TIME_SEC, "STARTING_TIME_SEC"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_BARO_PSIA, "LOG_PERF_P_BARO_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(DELEGT, "DELEGT"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELEGT, "LOG_PERF_DELEGT"))
                Return_Value.Add(MyBase.InitializeChannel(DELPB, "DELPB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELPB, "LOG_PERF_DELPB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELTA_P_SL, "LOG_PERF_DELTA_P_SL"))
                Return_Value.Add(MyBase.InitializeChannel(DELTB, "DELTB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELTB, "LOG_PERF_DELTB"))
                Return_Value.Add(MyBase.InitializeChannel(DELWB, "DELWB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELWB, "LOG_PERF_DELWB"))
                Return_Value.Add(MyBase.InitializeChannel(DELWF, "DELWF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELWF, "LOG_PERF_DELWF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_ECS_OFFSET, "LOG_PERF_ECS_OFFSET"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGT_DP_CORR, "LOG_PERF_EGT_DP_CORR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGT_EXC_PB, "LOG_PERF_EGT_EXC_PB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGT_INST, "LOG_PERF_EGT_INST"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGTCOR, "LOG_PERF_EGTCOR"))
                Return_Value.Add(MyBase.InitializeChannel(EGTREQ_DEGF, "EGTREQ_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGTREQ_DEGF, "LOG_PERF_EGTREQ_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_FLOW_SENSOR_ACC, "LOG_PERF_FLOW_SENSOR_ACC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_OPERATING_TIME_CURRENT_HOURS, "LOG_PERF_OPERATING_TIME_CURRENT_HOURS"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_OPERATING_TIME_CURRENT_MINUTES, "LOG_PERF_OPERATING_TIME_CURRENT_MINUTES"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_LCIT_DEGF, "LOG_PERF_LCIT_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureFinal, "Log_Perf_CaptureFinal"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureInitial, "Log_Perf_CaptureInitial"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_FinalIGV, "Log_Perf_FinalIGV"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_FinalPBCOR, "Log_Perf_FinalPBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_FinalPBCOR_BARA, "Log_Perf_FinalPBCOR_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_InitIGV, "Log_Perf_InitIGV"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_InitPBCOR, "Log_Perf_InitPBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_InitPBCOR_BARA, "Log_Perf_InitPBCOR_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_MinSurgeMargin, "Log_Perf_MinSurgeMargin"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_NbApuFaults, "Log_Perf_NbApuFaults"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_SCVStability, "Log_Perf_SCVStability"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_NG, "LOG_PERF_NG"))
                Return_Value.Add(MyBase.InitializeChannel(ARINC_IGV_POS, "ARINC_IGV_POS"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_ARINC_IGV_POS, "LOG_PERF_ARINC_IGV_POS"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_ARINC_SCV_POS, "LOG_PERF_ARINC_SCV_POS"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_BLEED1_5_PSIA, "LOG_PERF_P_BLEED1_5_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_COMP_DISCH_PSIA, "LOG_PERF_P_COMP_DISCH_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_EXHAUST_PSIA, "LOG_PERF_P_EXHAUST_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_FUEL_PSIG, "LOG_PERF_P_FUEL_PSIG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_GEARBOX_INH2O, "LOG_PERF_P_GEARBOX_INH2O"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_INLET_PSIA, "LOG_PERF_P_INLET_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_OIL_PSIG, "LOG_PERF_P_OIL_PSIG"))
                Return_Value.Add(MyBase.InitializeChannel(P_ORIFICE_PSIA, "P_ORIFICE_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(P_ORIFICE_DELTA_PSID, "P_ORIFICE_DELTA_PSID"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_ORIFICE_DELTA_PSID, "LOG_PERF_P_ORIFICE_DELTA_PSID"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_ORIFICE_PSIA, "LOG_PERF_P_ORIFICE_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PB_INST_PSIA, "LOG_PERF_PB_INST_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PB_SL_PSIA, "LOG_PERF_PB_SL_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(PBCOR_MEAN, "PBCOR_MEAN"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PBCOR, "LOG_PERF_PBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(PBREQ_PSIA, "PBREQ_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PBREQ_PSIA, "LOG_PERF_PBREQ_PSIA"))

                Return_Value.Add(MyBase.InitializeChannel(PERF_TEST_RESULT_ACCEPTED, "PERF_TEST_RESULT_ACCEPTED"))
                Return_Value.Add(MyBase.InitializeChannel(PERF_TEST_RESULT_REJECTED, "PERF_TEST_RESULT_REJECTED"))
                Return_Value.Add(MyBase.InitializeChannel(PERF_NEW_TEST_OPC, "PERF_NEW_TEST_OPC"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_SHP_SL, "LOG_PERF_SHP_SL"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ETC1_DEGF, "LOG_PERF_T_ETC1_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ETC2_DEGF, "LOG_PERF_T_ETC2_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_FUEL_DEGF, "LOG_PERF_T_FUEL_DEGF"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_LCDT_DEGF, "LOG_PERF_T_LCDT_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_OIL_DEGF, "LOG_PERF_T_OIL_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(T_ORIFICE_DEGF, "T_ORIFICE_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ORIFICE_DEGF, "LOG_PERF_T_ORIFICE_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_TBCOR_EXC_PB, "LOG_PERF_TBCOR_EXC_PB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_TBCOR, "LOG_PERF_TBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_GEARBOX, "LOG_PERF_VIB_GEARBOX"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_TURBINE, "LOG_PERF_VIB_TURBINE"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_COOL_FAN, "LOG_PERF_VIB_COOL_FAN"))
                Return_Value.Add(MyBase.InitializeChannel(WB_CALC, "WB_CALC"))
                Return_Value.Add(MyBase.InitializeChannel(WB_MEASURED, "WB_MEASURED"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_CALC, "LOG_PERF_WB_CALC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_MEASURED, "LOG_PERF_WB_MEASURED"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_INST, "LOG_PERF_WB_INST"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB, "LOG_PERF_WB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_SL, "LOG_PERF_WB_SL"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCDNA, "LOG_PERF_WBCDNA"))
                Return_Value.Add(MyBase.InitializeChannel(WBCDNA_HI_LIM, "WBCDNA_HI_LIM"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCDNA_HI_LIM, "LOG_PERF_WBCDNA_HI_LIM"))
                Return_Value.Add(MyBase.InitializeChannel(WBCDNA_LO_LIM, "WBCDNA_LO_LIM"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCDNA_LO_LIM, "LOG_PERF_WBCDNA_LO_LIM"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCOR, "LOG_PERF_WBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(WBREQ_PPM, "WBREQ_PPM"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBREQ_PPM, "LOG_PERF_WBREQ_PPM"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_AVG_PPH, "LOG_PERF_WF_AVG_PPH"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_DP_CORR, "LOG_PERF_WF_DP_CORR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_EXC_PB, "LOG_PERF_WF_EXC_PB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_INST, "LOG_PERF_WF_INST"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_SL, "LOG_PERF_WF_SL"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WFCOR, "LOG_PERF_WFCOR"))
                Return_Value.Add(MyBase.InitializeChannel(WFREQ_PPH, "WFREQ_PPH"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WFREQ_PPH, "LOG_PERF_WFREQ_PPH"))
                Return_Value.Add(MyBase.InitializeChannel(WINDMM_BTN, "WINDMM_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(WF_AVG_PPH, "WF_AVG_PPH"))
                Return_Value.Add(MyBase.InitializeChannel(WF_TOT1, "WF_TOT1"))
                Return_Value.Add(MyBase.InitializeChannel(WF_TOT2, "WF_TOT2"))

                Return_Value.Add(MyBase.InitializeChannel(VIB_GEARBOX_MAX_IPS_PEAK_PERSISTENT, "VIB_GEARBOX_MAX_IPS_PEAK_PERSISTENT"))
                Return_Value.Add(MyBase.InitializeChannel(VIB_TURBINE_MAX_IPS_PEAK_PERSISTENT, "VIB_TURBINE_MAX_IPS_PEAK_PERSISTENT"))
                Return_Value.Add(MyBase.InitializeChannel(VIB_COOLING_FAN_MAX_IPS_PEAK_PERSISTENT, "VIB_COOLING_FAN_MAX_IPS_PEAK_PERSISTENT"))

                Return_Value.Add(MyBase.InitializeChannel(QUICK_LOAD_AIR_VALVE_OPEN_BTN, "QUICK_LOAD_AIR_VALVE_OPEN_BTN"))

                Return_Value.Add(MyBase.InitializeObject(Test_Step_Frame, TestStepFrameControlName))
                Return_Value.Add(MyBase.InitializeObject(Ctrl_System_Frame, ControlSystemFrameName))


                '****************************
                ' Channels used in metric
                '****************************

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_BARO_BARA, "LOG_PERF_P_BARO_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_INLET_BARA, "LOG_PERF_P_INLET_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_INLET_AVG_DEGC, "LOG_PERF_T_INLET_AVG_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_LCIT_DEGC, "LOG_PERF_LCIT_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_OIL_KPAG, "LOG_PERF_P_OIL_KPAG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_GEARBOX_MMH2O, "LOG_PERF_P_GEARBOX_MMH2O"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_OIL_DEGC, "LOG_PERF_T_OIL_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_FUEL_DEGC, "LOG_PERF_T_FUEL_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_FUEL_KPAG, "LOG_PERF_P_FUEL_KPAG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_GEARBOX_MMPS_PEAK, "LOG_PERF_VIB_GEARBOX_MMPS_PEAK"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_TURBINE_MMPS_PEAK, "LOG_PERF_VIB_TURBINE_MMPS_PEAK"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_COOL_FAN_MMPS_PEAK, "LOG_PERF_VIB_COOL_FAN_MMPS_PEAK"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_COMP_DISCH_BARA, "LOG_PERF_P_COMP_DISCH_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_LCDT_DEGC, "LOG_PERF_T_LCDT_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ETC1_DEGC, "LOG_PERF_T_ETC1_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ETC2_DEGC, "LOG_PERF_T_ETC2_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_EGT_AVG_DEGC, "LOG_PERF_T_EGT_AVG_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_EXHAUST_BARA, "LOG_PERF_P_EXHAUST_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_ORIFICE_BARA, "LOG_PERF_P_ORIFICE_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ORIFICE_DEGC, "LOG_PERF_T_ORIFICE_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_ORIFICE_DELTA_KPAD, "LOG_PERF_P_ORIFICE_DELTA_KPAD"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_KGPM, "LOG_PERF_WB_KGPM"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCDNA_KGPM, "LOG_PERF_WBCDNA_KGPM"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_BLEED1_5_BARA, "LOG_PERF_P_BLEED1_5_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_BLEED_AVG_DEGC, "LOG_PERF_T_BLEED_AVG_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_BLEED_AVG_DEGC, "LOG_PERF_T_BLEED_AVG_DEGC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_AVG_KGH, "LOG_PERF_WF_AVG_KGH"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELTA_P_SL_BARA, "LOG_PERF_DELTA_P_SL_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PB_SL_BARA, "LOG_PERF_PB_SL_BARA"))

                'Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PB_INST_BARA, "LOG_PERF_PB_INST_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PBCOR_BARA, "LOG_PERF_PBCOR_BARA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_SL_KGPM, "LOG_PERF_WB_SL_KGPM"))

                '****************************


                ECS_Frame = Ctrl_System_Frame.FindObject("fraECS")
                ECS_Display = ECS_Frame.FindObject("dspECS_DEMAND")

                If ECS_Display Is Nothing Then
                    MessageBox.Show("MyBase.InitializeObject - ECS_Display Is Nothing")
                    Return_Value.Add(False)
                Else
                    Return_Value.Add(True)
                End If

                TestStep_List = Test_Step_Frame.FindObject("TestStep_List")

                If TestStep_List Is Nothing Then
                    MessageBox.Show("MyBase.InitializeObject - TestStep_List Is Nothing")
                    Return_Value.Add(False)
                Else
                    Return_Value.Add(True)
                End If

                Varispeed_Frame = Ctrl_System_Frame.FindObject("fraVarispeed")
                Varispeed_Display = Varispeed_Frame.FindObject("dspVARISPEED_DEMAND")

                If Varispeed_Display Is Nothing Then
                    MessageBox.Show("MyBase.InitializeObject - Varispeed_Display Is Nothing")
                    Return_Value.Add(False)
                Else
                    Return_Value.Add(True)
                End If

                'End Select

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

        '************
        '* Channels *
        '************

        Private START_BTN As IBooleanChannel
        Private EngineNameAsPrefix As String
        Private ARCHIVE_RECORD_BTN As IBooleanChannel
        Private PerformanceScreenName As String
        Private BaseScreenName As String
        Private MainScreenName As String
        Private LoadBankFrameControlName As String
        Private TestStepFrameControlName As String
        Private ControlSystemFrameName As String
        Private ENGINE_AT_REST_IND As IBooleanChannel
        Private ARCHIVE_STOP_BTN As KineticaRT.Channels.IBooleanChannel

        Private BLEED_BTN As IBooleanChannel
        Private DRESS_KIT As ITextChannel
        Private FLHV As INumericChannel
        Private FUEL_USED_INITIAL_LITERS As INumericChannel
        Private FUEL_USED_TOTAL_LITERS As INumericChannel
        Private FM_INITIAL_VOLUME_LITERS As INumericChannel
        Private FM_TOTAL_VOLUME_LITERS As INumericChannel

        Private INLET_FLAP_CLOSE_CMD As IBooleanChannel
        Private INLET_FLAP_OPEN_CMD As IBooleanChannel
        Private INLET_FLAP_MOVE_CMD As IBooleanChannel
        Private INLET_FLAP_OPEN_REQUEST_IND As IBooleanChannel 'ECB Feedback
        Private INLET_FLAP_CLOSE_REQUEST_IND As IBooleanChannel 'ECB Feedback

        Private INLET_FLAP_CLOSED_IND As IBooleanChannel
        Private INLET_FLAP_MOVEMENT_IND As IBooleanChannel
        Private INLET_FLAP_OPENED_IND As IBooleanChannel

        Private LOAD_AIR_VALVE_DEMAND As INumericChannel
        Private NUMBER_OF_STARTS As INumericChannel
        Private OPERATING_TIME_CURRENT_SEC As INumericChannel
        Private READY_TO_LOAD_IND As IBooleanChannel
        Private T_INLET_AVG_DEGF As INumericChannel

        Private LOG_PERF_LB1_POWER_TOT As INumericChannel
        Private LOG_PERF_OPERATING_TIME_CURRENT_HOURS As INumericChannel
        Private LOG_PERF_OPERATING_TIME_CURRENT_MINUTES As INumericChannel
        Private LOG_PERF_T_BLEED_AVG_DEGF As INumericChannel
        Private LOG_PERF_T_EGT_AVG_DEGF As INumericChannel
        Private LOG_PERF_T_ETC1_DEGF As INumericChannel
        Private LOG_PERF_T_ETC2_DEGF As INumericChannel
        Private LOG_PERF_T_INLET_AVG_DEGF As INumericChannel

        Private ARINC_IGV_POS As INumericChannel
        Private LOG_PERF_ARINC_IGV_POS As INumericChannel
        Private LOG_PERF_ARINC_SCV_POS As INumericChannel
        Private LOG_PERF_P_BARO_PSIA As INumericChannel
        Private DELEGT As INumericChannel
        Private LOG_PERF_DELEGT As INumericChannel
        Private DELPB As INumericChannel
        Private LOG_PERF_DELPB As INumericChannel
        Private LOG_PERF_DELTA_P_SL As INumericChannel
        Private DELTB As INumericChannel
        Private LOG_PERF_DELTB As INumericChannel
        Private DELWB As INumericChannel
        Private LOG_PERF_DELWB As INumericChannel
        Private DELWF As INumericChannel
        Private LOG_PERF_DELWF As INumericChannel
        Private LOG_PERF_ECS_OFFSET As INumericChannel
        Private LOG_PERF_EGT_DP_CORR As INumericChannel
        Private LOG_PERF_EGT_EXC_PB As INumericChannel
        Private LOG_PERF_EGT_INST As INumericChannel
        Private LOG_PERF_EGTCOR As INumericChannel
        Private EGTREQ_DEGF As INumericChannel
        Private LOG_PERF_EGTREQ_DEGF As INumericChannel
        Private LOG_PERF_FLOW_SENSOR_ACC As INumericChannel
        Private HONEYWELL_COMPUTER_SELECTION_BTN As IBooleanChannel
        Private LOG_PERF_LCIT_DEGF As INumericChannel
        Private LOG_PERF_NG As INumericChannel
        Private LOG_PERF_P_BLEED1_5_PSIA As INumericChannel
        Private LOG_PERF_P_COMP_DISCH_PSIA As INumericChannel
        Private LOG_PERF_P_EXHAUST_PSIA As INumericChannel
        Private LOG_PERF_P_FUEL_PSIG As INumericChannel
        Private LOG_PERF_P_GEARBOX_INH2O As INumericChannel
        Private LOG_PERF_P_INLET_PSIA As INumericChannel
        Private LOG_PERF_P_OIL_PSIG As INumericChannel
        Private P_ORIFICE_PSIA As INumericChannel
        Private LOG_PERF_P_ORIFICE_PSIA As INumericChannel
        Private P_ORIFICE_DELTA_PSID As INumericChannel
        Private LOG_PERF_P_ORIFICE_DELTA_PSID As INumericChannel

        '*************************************
        ' Channels in metric notation
        '*************************************

        Private LOG_PERF_P_BARO_BARA As INumericChannel
        Private LOG_PERF_P_INLET_BARA As INumericChannel
        Private LOG_PERF_T_INLET_AVG_DEGC As INumericChannel
        Private LOG_PERF_LCIT_DEGC As INumericChannel
        Private LOG_PERF_P_OIL_KPAG As INumericChannel
        Private LOG_PERF_P_GEARBOX_MMH2O As INumericChannel
        Private LOG_PERF_T_OIL_DEGC As INumericChannel
        Private LOG_PERF_T_FUEL_DEGC As INumericChannel
        Private LOG_PERF_P_FUEL_KPAG As INumericChannel
        Private LOG_PERF_VIB_GEARBOX_MMPS_PEAK As INumericChannel
        Private LOG_PERF_VIB_TURBINE_MMPS_PEAK As INumericChannel
        Private LOG_PERF_VIB_COOL_FAN_MMPS_PEAK As INumericChannel

        Private LOG_PERF_P_COMP_DISCH_BARA As INumericChannel
        Private LOG_PERF_T_LCDT_DEGC As INumericChannel
        Private LOG_PERF_T_ETC1_DEGC As INumericChannel
        Private LOG_PERF_T_ETC2_DEGC As INumericChannel
        Private LOG_PERF_T_EGT_AVG_DEGC As INumericChannel
        Private LOG_PERF_P_EXHAUST_BARA As INumericChannel
        Private LOG_PERF_P_ORIFICE_BARA As INumericChannel
        Private LOG_PERF_T_ORIFICE_DEGC As INumericChannel
        Private LOG_PERF_P_ORIFICE_DELTA_KPAD As INumericChannel
        Private LOG_PERF_WB_KGPM As INumericChannel
        Private LOG_PERF_WBCDNA_KGPM As INumericChannel
        Private LOG_PERF_P_BLEED1_5_BARA As INumericChannel
        Private LOG_PERF_T_BLEED_AVG_DEGC As INumericChannel
        Private LOG_PERF_WF_AVG_KGH As INumericChannel

        Private LOG_PERF_DELTA_P_SL_BARA As INumericChannel
        Private LOG_PERF_PB_SL_BARA As INumericChannel

        Private LOG_PERF_PB_INST_BARA As INumericChannel
        Private LOG_PERF_PBCOR_BARA As INumericChannel
        Private LOG_PERF_WB_SL_KGPM As INumericChannel


        '******************

        Private PERF_TEST_RESULT_ACCEPTED As IBooleanChannel
        Private PERF_TEST_RESULT_REJECTED As IBooleanChannel
        Private PERF_NEW_TEST_OPC As IBooleanChannel


        Private LOG_PERF_PB_INST_PSIA As INumericChannel
        Private LOG_PERF_PB_SL_PSIA As INumericChannel
        Private PBCOR_MEAN As INumericChannel
        Private LOG_PERF_PBCOR As INumericChannel
        Private PBREQ_PSIA As INumericChannel
        Private LOG_PERF_PBREQ_PSIA As INumericChannel

        Private LOG_PERF_SHP_SL As INumericChannel
        Private LOG_PERF_T_FUEL_DEGF As INumericChannel
        Private LOG_PERF_T_LCDT_DEGF As INumericChannel
        Private LOG_PERF_T_OIL_DEGF As INumericChannel
        Private T_ORIFICE_DEGF As INumericChannel
        Private LOG_PERF_T_ORIFICE_DEGF As INumericChannel
        Private LOG_PERF_TBCOR_EXC_PB As INumericChannel
        Private LOG_PERF_TBCOR As INumericChannel
        Private LOG_PERF_VIB_COOL_FAN As INumericChannel
        Private LOG_PERF_VIB_GEARBOX As INumericChannel
        Private LOG_PERF_VIB_TURBINE As INumericChannel
        Private WB_CALC As INumericChannel
        Private WB_MEASURED As INumericChannel
        Private LOG_PERF_WB_CALC As INumericChannel
        Private LOG_PERF_WB_MEASURED As INumericChannel
        Private LOG_PERF_WB As INumericChannel
        Private LOG_PERF_WB_SL As INumericChannel
        Private LOG_PERF_WB_INST As INumericChannel
        Private LOG_PERF_WBCDNA As INumericChannel
        Private WBCDNA_HI_LIM As INumericChannel
        Private LOG_PERF_WBCDNA_HI_LIM As INumericChannel
        Private WBCDNA_LO_LIM As INumericChannel
        Private LOG_PERF_WBCDNA_LO_LIM As INumericChannel
        Private LOG_PERF_WBCOR As INumericChannel
        Private WBREQ_PPM As INumericChannel
        Private LOG_PERF_WBREQ_PPM As INumericChannel
        Private LOG_PERF_WF_AVG_PPH As INumericChannel
        Private LOG_PERF_WF_DP_CORR As INumericChannel
        Private LOG_PERF_WF_EXC_PB As INumericChannel
        Private LOG_PERF_WF_INST As INumericChannel
        Private LOG_PERF_WF_SL As INumericChannel
        Private LOG_PERF_WFCOR As INumericChannel
        Private WFREQ_PPH As INumericChannel
        Private LOG_PERF_WFREQ_PPH As INumericChannel
        Private WINDMM_BTN As IBooleanChannel
        Private WF_AVG_PPH As INumericChannel
        Private WF_TOT1 As INumericChannel
        Private WF_TOT2 As INumericChannel

        Private VIB_GEARBOX_MAX_IPS_PEAK_PERSISTENT As INumericChannel
        Private VIB_TURBINE_MAX_IPS_PEAK_PERSISTENT As INumericChannel
        Private VIB_COOLING_FAN_MAX_IPS_PEAK_PERSISTENT As INumericChannel

        Private QUICK_LOAD_AIR_VALVE_OPEN_BTN As KineticaRT.Channels.IBooleanChannel

        '*****************************
        '* Log & Performance reports *
        '*****************************

        Private Log_Perf_CaptureFinal As IBooleanChannel
        Private Log_Perf_CaptureInitial As IBooleanChannel
        Private Log_Perf_FinalIGV As INumericChannel
        Private Log_Perf_FinalPBCOR As INumericChannel
        Private Log_Perf_InitIGV As INumericChannel
        Private Log_Perf_InitPBCOR As INumericChannel
        Private Log_Perf_FinalPBCOR_BARA As INumericChannel
        Private Log_Perf_InitPBCOR_BARA As INumericChannel
        Private Log_Perf_MinSurgeMargin As IBooleanChannel
        Private Log_Perf_NbApuFaults As INumericChannel
        Private Log_Perf_SCVStability As IBooleanChannel
        Private STARTING_TIME_SEC As INumericChannel

        '*********************
        '* Forms and Control *
        '*********************
        Private ECS_Display As KineticaRT.Studio.Controls.Display
        Private Varispeed_Display As KineticaRT.Studio.Controls.Display
        'Frame
        Private ECS_Frame As KineticaRT.Controls.Control
        Private Ctrl_System_Frame As KineticaRT.Controls.Control
        Private Test_Step_Frame As KineticaRT.Controls.Control
        Private TestStep_List As System.Windows.Forms.ComboBox
        Private Varispeed_Frame As KineticaRT.Controls.Control

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
        Private GTCP131_9A_ECS(,) As Double 'Used to keep a database table in memory for fast access for GTCP131-9A
        Private GTCP131_9A_MES(,) As Double 'Used to keep a database table in memory for fast access for GTCP131-9A
        Private GTCP131_9A_WBCDNA_LIMITS(,) As Double 'Used to keep a database table in memory for fast access for GTCP131-9A

        Private StarterMaxCurrentRecordCtrl As MinMaxRecordController = Nothing
        Private StarterMaxVoltageRecordCtrl As MinMaxRecordController = Nothing

        Private DeletableTBleedChCtrl As DeletableChannelController = Nothing
        Private DeletableTEgtChCtrl As DeletableChannelController = Nothing
        Private DeletableTInletChCtrl As DeletableChannelController = Nothing
        Private FlowmeterCtrl As FlowmeterController = Nothing

        Private LoadBankCtrl As LoadBankController = Nothing
        Private ECSCtrl As ECSController = Nothing
        Private StartTimerCtrl As TimerController = Nothing
        Private VariSpeedCtrl As ECSController = Nothing

        'Private TestProgressMgr As CEL.TestProgress.TestProgressMgr = Nothing
        Private HeavyRepairType As Boolean = False
        Private MinPBCOR_ECS As Double = System.Double.NaN
        Private MinPBCOR_MES As Double = System.Double.NaN
        Private MaxEGTCOR_ECS As Double = System.Double.NaN
        Private MaxEGTCOR_MES As Double = System.Double.NaN
        Private MinWBCOR_ECS As Double = System.Double.NaN

        Private UserInputsRevima As New UserInputRevimaGTCP131_9A()
        Private UserInputsChine As New UserInputChine11813_GTCP131_9A()
        Private UserInputsEPCOR As New UserInputEPCOR12958_GTCP131_9A()
        Private UserInputsTHY As New UserInputTHY13015_GTCP131_9A()
        Private UserInputsS7 As New UserInputS7_GTCP131_9A()

    End Class

    Public Class UserInputRevimaGTCP131_9A

        'Question
        Public EngineSerialNumber As String = ""
        Public WorkOrder As String = ""
        Public ECBSerialNumber As String = ""
        Public ExhaustDuctSerialNumber As String = ""
        Public BellmouthSerialNumber As String = ""
        Public BleedDuctSerialNumber As String = ""
        Public StarterSerialNumber As String = ""
        Public TurbineVibPickupSerialNumber As String = ""
        Public GearboxVibPickupSerialNumber As String = ""
        Public FanVibPickupSerialNumber As String = ""
        Public OilType As String = ""
        Public LHV As String = ""
        Public TimeSinceNewOrOverhaul As String = "" 'TSN/TSO
        Public CyclesSinceNewOrOverhaul As String = "" 'CSN

        'Choice
        Public Reason As String = ""
        Public TestType As String = ""
        Public RepairType As String = ""

        ' Allow friend access to the empty constructor.
        Friend Sub New()

        End Sub
        Public Sub MapUserInputsParameters(ByRef CELProject As CELDAS)
            'Question
            EngineSerialNumber = CELProject.Test_Mgr_UUT_Answer(0).Value.ToString()
            WorkOrder = CELProject.Test_Mgr_UUT_Answer(1).Value.ToString()
            ECBSerialNumber = CELProject.Test_Mgr_UUT_Answer(2).Value.ToString()
            ExhaustDuctSerialNumber = CELProject.Test_Mgr_UUT_Answer(3).Value.ToString()
            BellmouthSerialNumber = CELProject.Test_Mgr_UUT_Answer(4).Value.ToString()
            BleedDuctSerialNumber = CELProject.Test_Mgr_UUT_Answer(5).Value.ToString()
            StarterSerialNumber = CELProject.Test_Mgr_UUT_Answer(6).Value.ToString()
            TurbineVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(9).Value.ToString()
            GearboxVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(10).Value.ToString()
            FanVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(11).Value.ToString()
            OilType = CELProject.Test_Mgr_UUT_Answer(12).Value.ToString()
            LHV = CELProject.Test_Mgr_UUT_Answer(13).Value.ToString()
            TimeSinceNewOrOverhaul = CELProject.Test_Mgr_UUT_Answer(15).Value.ToString()
            CyclesSinceNewOrOverhaul = CELProject.Test_Mgr_UUT_Answer(16).Value.ToString()
            'Choice
            Reason = CELProject.Test_Mgr_UUT_Choice(0).Value.ToString()
            TestType = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()
            RepairType = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()
        End Sub
    End Class

    Public Class UserInputChine11813_GTCP131_9A

        'Question
        Public EngineSerialNumber As String = ""
        Public WorkOrder As String = ""
        Public ECBSerialNumber As String = ""
        Public ExhaustDuctSerialNumber As String = ""
        Public BellmouthSerialNumber As String = ""
        Public BleedDuctSerialNumber As String = ""
        Public StarterSerialNumber As String = ""
        Public SampleTFuel As String = ""
        Public SampleSG As String = ""
        Public TurbineVibPickupSerialNumber As String = ""
        Public GearboxVibPickupSerialNumber As String = ""
        Public FanVibPickupSerialNumber As String = ""
        Public OilType As String = ""
        Public TimeSinceNewOrOverhaul As String = "" 'TSN/TSO
        Public CyclesSinceNewOrOverhaul As String = "" 'CSN
        Public LHV As String = ""

        'Choice
        Public TestType As String = ""
        Public RepairType As String = ""
        Public Reason As String = ""

        ' Allow friend access to the empty constructor.
        Friend Sub New()

        End Sub
        Public Sub MapUserInputsParameters(ByRef CELProject As CELDAS)

            'Question
            EngineSerialNumber = CELProject.Test_Mgr_UUT_Answer(0).Value.ToString()
            WorkOrder = CELProject.Test_Mgr_UUT_Answer(1).Value.ToString()
            ECBSerialNumber = CELProject.Test_Mgr_UUT_Answer(2).Value.ToString()
            ExhaustDuctSerialNumber = CELProject.Test_Mgr_UUT_Answer(3).Value.ToString()
            BellmouthSerialNumber = CELProject.Test_Mgr_UUT_Answer(4).Value.ToString()
            BleedDuctSerialNumber = CELProject.Test_Mgr_UUT_Answer(5).Value.ToString()
            StarterSerialNumber = CELProject.Test_Mgr_UUT_Answer(6).Value.ToString()
            SampleTFuel = CELProject.Test_Mgr_UUT_Answer(7).Value.ToString()
            SampleSG = CELProject.Test_Mgr_UUT_Answer(8).Value.ToString()
            TurbineVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(9).Value.ToString()
            GearboxVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(10).Value.ToString()
            FanVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(11).Value.ToString()
            OilType = CELProject.Test_Mgr_UUT_Answer(12).Value.ToString()
            TimeSinceNewOrOverhaul = CELProject.Test_Mgr_UUT_Answer(15).Value.ToString()
            CyclesSinceNewOrOverhaul = CELProject.Test_Mgr_UUT_Answer(16).Value.ToString()
            LHV = CELProject.Test_Mgr_UUT_Answer(17).Value.ToString()

            'Choice
            TestType = CELProject.Test_Mgr_UUT_Choice(0).Value.ToString()
            RepairType = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()
            Reason = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()

        End Sub
    End Class

    Public Class UserInputEPCOR12958_GTCP131_9A

        'Question
        Public SerialNumber As String = ""
        Public WorkOrder As String = ""
        Public PartNumber As String = ""
        Public LHV As String = ""
        Public FuelSG As String = ""
        Public TFuelDegF As String = ""
        Public OilType As String = ""
        Public FuelType As String = ""
        Public CustomerName As String = ""

        'Choice
        Public ChoiceTestType As String = ""
        Public ChoiceOperator As String = ""
        Public ChoiceInspector As String = ""
        Public ChoiceRepairType As String = ""

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

            'Choice
            ChoiceTestType = CELProject.Test_Mgr_UUT_Choice(0).Value.ToString()
            ChoiceRepairType = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()
            ChoiceOperator = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()
            ChoiceInspector = CELProject.Test_Mgr_UUT_Choice(3).Value.ToString()
        End Sub
    End Class

    Public Class UserInputTHY13015_GTCP131_9A

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
        Public CoolingFanVibPickupSerialNumber As String = String.Empty

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
            'CustomerName = CELProject.Test_Mgr_UUT_Answer(8).Value.ToString()
            TurbineVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(9).Value.ToString()
            GearboxVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(10).Value.ToString()
            CoolingFanVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(11).Value.ToString()

            'Choice
            ChoiceTestType = CELProject.Test_Mgr_UUT_Choice(0).Value.ToString()
            ChoiceRepairType = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()
            ChoiceOperator = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()
            ChoiceInspector = CELProject.Test_Mgr_UUT_Choice(3).Value.ToString()
        End Sub
    End Class

    Public Class UserInputS7_GTCP131_9A

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
        Public CoolingFanVibPickupSerialNumber As String = String.Empty

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
            TurbineVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(9).Value.ToString()
            GearboxVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(10).Value.ToString()
            CoolingFanVibPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(11).Value.ToString()

            'Choice
            ChoiceTestType = CELProject.Test_Mgr_UUT_Choice(0).Value.ToString()
            ChoiceOperator = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()
            ChoiceInspector = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()
            ChoiceRepairType = CELProject.Test_Mgr_UUT_Choice(3).Value.ToString()
        End Sub

    End Class

End Namespace

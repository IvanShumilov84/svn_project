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

	Public Class GTCP131_9B_Sequence
        Inherits ApuSequence

		Private Const MaxNumberOfTBleedProbe As Integer = 4
		Private Const MaxNumberOfTEgtProbe As Integer = 12
		Private Const MaxNumberOfTInletProbe As Integer = 12

		Private Enum eTestStepGTCP131_9B
			TCOND1 = 0	' RTL
			IGV_OFF = 1	' IGV Offset
			TCOND2 = 2	' ECS
			TCOND3 = 3	' MES
			TCOND4 = 4	' Flow sensor test 1
			TCOND5 = 5	' Flow sensor test 2
			TCOND6 = 6	' Minimum surge margin test
			TCOND7 = 7	' Load cycle test 1
			TCOND8 = 8	' Load cycle test 2
			TCOND9 = 9	' Load cycle test 3
			FINAL_DATA = 10
		End Enum

        Protected Overrides Sub Compute(ByVal CalculateForLogAndPerformanceReports As Boolean)

            Try
                If CalculateForLogAndPerformanceReports Then

                    If EngineConfiguration.UseMeasuredBleedFlowValue Then
                        LOG_PERF_WB_CALC.Value = LOG_PERF_WB_MEASURED.Value
                    Else
                        LOG_PERF_WB_CALC.Value = New KineticaRT.Values.Double(Compute_BleedAirFlow(LOG_PERF_T_ORIFICE_DEGF.Value.ToDouble(),
                                                                                                   LOG_PERF_P_ORIFICE_PSIA.Value.ToDouble(),
                                                                                                   LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToDouble))
                    End If

                    Compute_GTCP131_9B_Perf(LOG_PERF_T_INLET_DEGF_AVG, _
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

                    Compute_GTCP131_9B_Perf(T_INLET_DEGF_AVG, _
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
                MessageBox.Show("Compute_GTCP131_9B" + ex.ToString(), "Error")
            End Try

        End Sub

	    Private Sub Compute_GTCP131_9B_Perf(ByRef tinlet_degf_avg As IChannel, _
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

                    Case eTestStepGTCP131_9B.TCOND1, eTestStepGTCP131_9B.TCOND7, eTestStepGTCP131_9B.FINAL_DATA
                        del_wb.Value = Values.NaN
                        del_pb.Value = Values.NaN
                        del_tb.Value = Values.NaN
                        del_egt.Value = Values.NaN
                        del_wf.Value = Values.NaN

                        egt_req.Value = Values.NaN
                        pb_req.Value = Values.NaN
                        wb_req.Value = Values.NaN
                        wf_req.Value = Values.NaN

                    Case eTestStepGTCP131_9B.IGV_OFF, eTestStepGTCP131_9B.TCOND2, eTestStepGTCP131_9B.TCOND4, eTestStepGTCP131_9B.TCOND5, eTestStepGTCP131_9B.TCOND6
                        'ECS
                        del_wb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_ECS, 1)
                        del_pb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_ECS, 2)
                        del_tb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_ECS, 3)
                        del_egt.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_ECS, 4)
                        del_wf.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_ECS, 5)

                        If HeavyRepairType = True Then 'Table 1311 (page 1328)
                            egt_req.Value = New KineticaRT.Values.Double(1090.0)
                            pb_req.Value = New KineticaRT.Values.Double(51.2)
                            wb_req.Value = New KineticaRT.Values.Double(155.0)
                            wf_req.Value = Values.NaN
                        Else
                            egt_req.Value = New KineticaRT.Values.Double(1115.0)
                            pb_req.Value = New KineticaRT.Values.Double(51.2)
                            wb_req.Value = New KineticaRT.Values.Double(155.0)
                            wf_req.Value = Values.NaN
                        End If

                    Case eTestStepGTCP131_9B.TCOND3, eTestStepGTCP131_9B.TCOND8, eTestStepGTCP131_9B.TCOND9
                        'MES
                        del_wb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_MES, 1)
                        del_pb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_MES, 2)
                        del_tb.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_MES, 3)
                        del_egt.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_MES, 4)
                        del_wf.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_MES, 5)

                        If HeavyRepairType = True Then 'Table 1312 (page 1329)
                            egt_req.Value = New KineticaRT.Values.Double(1080.0)
                            pb_req.Value = New KineticaRT.Values.Double(54.5)
                            wb_req.Value = Values.NaN
                            wf_req.Value = Values.NaN
                        Else
                            egt_req.Value = New KineticaRT.Values.Double(1105.0)
                            pb_req.Value = New KineticaRT.Values.Double(53.7)
                            wb_req.Value = Values.NaN
                            wf_req.Value = Values.NaN
                        End If

                End Select

                Select Case CELProject.PERF_TEST_STEP_NB.Value.ToDouble()

                    Case eTestStepGTCP131_9B.TCOND1, eTestStepGTCP131_9B.TCOND7, eTestStepGTCP131_9B.FINAL_DATA
                        wbcdna_lo_lim.Value = New KineticaRT.Values.Double(System.Double.NegativeInfinity)
                        wbcdna_hi_lim.Value = New KineticaRT.Values.Double(System.Double.PositiveInfinity)

                    Case eTestStepGTCP131_9B.IGV_OFF, eTestStepGTCP131_9B.TCOND2
                        ' Table 1305 (p1309)
                        wbcdna_lo_lim.Value = New KineticaRT.Values.Double(57.2)
                        wbcdna_hi_lim.Value = New KineticaRT.Values.Double(59.2)

                    Case eTestStepGTCP131_9B.TCOND3, eTestStepGTCP131_9B.TCOND8, eTestStepGTCP131_9B.TCOND9
                        ' Table 1305 (p1309)
                        wbcdna_lo_lim.Value = New KineticaRT.Values.Double(49.0)
                        wbcdna_hi_lim.Value = New KineticaRT.Values.Double(51.0)

                    Case eTestStepGTCP131_9B.TCOND4, eTestStepGTCP131_9B.TCOND5
                        wbcdna_lo_lim.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_WBCDNA_LIMITS, 1)
                        wbcdna_hi_lim.Value = CELProject.Interpolate_1D(tinlet_degf_avg.Value.ToDouble(), GTCP131_9B_WBCDNA_LIMITS, 2)

                    Case eTestStepGTCP131_9B.TCOND6
                        'From figure 1304
                        'X1 =0, X2 = 130, Y1 = 42.2, Y2 = 42.55
                        wbcdna_lo_lim.Value = New KineticaRT.Values.Double((0.0027 * tinlet_degf_avg.Value.ToDouble()) + 42.2 - 1)
                        wbcdna_hi_lim.Value = New KineticaRT.Values.Double((0.0027 * tinlet_degf_avg.Value.ToDouble()) + 42.2 + 1)

                End Select

            Catch Ex As Exception
                MessageBox.Show("Compute_GTCP131_9B_Perf: " & Ex.ToString())
            End Try

        End Sub

		Private Sub ValidateFMInitialValue()
			Try

				Select Case CELProject.Project.Number

					Case 11813
                        FM_INITIAL_VOLUME_LITERS.Value = Values.Zero
                    Case 12958
                        FM_INITIAL_VOLUME_LITERS.Value = Values.Zero
                    Case 11825
                        FM_INITIAL_VOLUME_LITERS.Value = Values.Zero
                    Case 13015
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
            Const defaultCounter As Integer = 3000
            Try

                If CELProject.IsTrue(INLET_FLAP_OPEN_REQUEST_IND.Value) Then
                    If (FlapPosition < defaultCounter) Then
                        FlapPosition = FlapPosition + 100
                    Else
                        FlapPosition = defaultCounter
                    End If

                ElseIf CELProject.IsTrue(INLET_FLAP_CLOSE_REQUEST_IND.Value) Then
                    If (FlapPosition > 0) Then
                        FlapPosition = FlapPosition - 100
                    Else
                        FlapPosition = 0
                    End If
                End If

                If FlapPosition < defaultCounter Then
                    If CELProject.IsFalse(INLET_DOOR_NOT_FULLY_OPENED_IND.Value) Then
                        INLET_DOOR_NOT_FULLY_OPENED_IND.Value = Values.True   ' (* 0 is close position*) 
                    End If
                Else
                    If CELProject.IsTrue(INLET_DOOR_NOT_FULLY_OPENED_IND.Value) Then
                        INLET_DOOR_NOT_FULLY_OPENED_IND.Value = Values.False
                    End If
                End If

                If FlapPosition = defaultCounter Then
                    If CELProject.IsFalse(INLET_DOOR_FULLY_OPENED_IND.Value) Then
                        INLET_DOOR_FULLY_OPENED_IND.Value = Values.True  '(* 3000 is open position*)
                    End If
                Else
                    If CELProject.IsTrue(INLET_DOOR_FULLY_OPENED_IND.Value) Then
                        INLET_DOOR_FULLY_OPENED_IND.Value = Values.False
                    End If
                End If

            Catch ex As Exception
                Console.WriteLine("EmulateInletFlapOpeningClosing: " + ex.Message)
            End Try

        End Sub

        Private Sub EmulateAPU_Fuel_SOV()
            Static APU_Fuel_SOVPosition As Integer = 0
            Try

                If CELProject.IsTrue(APU_FUEL_VALVE_DOOR_OPEN_CMD.Value) Then
                    If (APU_Fuel_SOVPosition < 1000) Then
                        APU_Fuel_SOVPosition = APU_Fuel_SOVPosition + 100
                    Else
                        APU_Fuel_SOVPosition = 1000
                    End If

                ElseIf CELProject.IsTrue(APU_FUEL_VALVE_DOOR_CLOSE_CMD.Value) Then
                    If (APU_Fuel_SOVPosition > 0) Then
                        APU_Fuel_SOVPosition = APU_Fuel_SOVPosition - 100
                    Else
                        APU_Fuel_SOVPosition = 0
                    End If
                End If

                If APU_Fuel_SOVPosition = 0 Then
                    If CELProject.IsFalse(APU_FUEL_VALVE_DOOR_CLOSED_IND.Value) Then
                        APU_FUEL_VALVE_DOOR_CLOSED_IND.Value = Values.True   ' (* 0 is close position*) 
                    End If
                Else
                    If CELProject.IsTrue(APU_FUEL_VALVE_DOOR_CLOSED_IND.Value) Then
                        APU_FUEL_VALVE_DOOR_CLOSED_IND.Value = Values.False   ' (* 0 is close position*) 
                    End If

                End If

                If APU_Fuel_SOVPosition = 1000 Then
                    If CELProject.IsFalse(APU_FUEL_VALVE_DOOR_OPENED_IND.Value) Then
                        APU_FUEL_VALVE_DOOR_OPENED_IND.Value = Values.True '(* 1000 is open position*)
                    End If
                Else
                    If CELProject.IsTrue(APU_FUEL_VALVE_DOOR_OPENED_IND.Value) Then
                        APU_FUEL_VALVE_DOOR_OPENED_IND.Value = Values.False '(* 1000 is open position*)
                    End If

                End If


            Catch ex As Exception

            End Try

        End Sub

        Private Sub EPCORWritePerfSheet3(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("CALCULATIONS")

                excelWorksheet.Cells(15, Column).Value = LOG_PERF_GEN_SL.Value.ToString(LOG_PERF_GEN_SL.Format, Nothing)    'GENSL
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
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)      'TBCOR
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)    'DELEGT
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing) 'Delta P Correction on EGT
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)    'Installation effect on EGT
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)    'Excess bleed P correction on EGT
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)    'EGTCOR
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)      'SL Fuel FLow
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)      'DELWF
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)    'Delta P Correction on WF
                excelWorksheet.Cells(35, Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)  'Installation effect on WF
                excelWorksheet.Cells(36, Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing) 'Excess bleed P correction on WF
                excelWorksheet.Cells(37, Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)      'WFCOR

            Catch Ex As Exception
                MessageBox.Show("EPCORWritePerfSheet3: " & Ex.ToString())
            End Try
        End Sub

        Private Sub EPCORWritePerfSheet2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ShowEverything As Boolean)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("DETAILS")

                excelWorksheet.Cells(14, Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing) 'PBAR
                excelWorksheet.Cells(15, Column).Value = LOG_PERF_T_INLET_DEGF_AVG.Value.ToString(LOG_PERF_T_INLET_DEGF_AVG.Format, Nothing) 'T1
                excelWorksheet.Cells(16, Column).Value = LOG_PERF_ARINC_T2_DEGF.Value.ToString(LOG_PERF_ARINC_T2_DEGF.Format, Nothing) 'TENIVA
                excelWorksheet.Cells(17, Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing) 'POIL
                excelWorksheet.Cells(18, Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing) 'TOIL
                excelWorksheet.Cells(19, Column).Value = LOG_PERF_P_GEARBOX_INH2O.Value.ToString(LOG_PERF_P_GEARBOX_INH2O.Format, Nothing) 'PSGBX
                excelWorksheet.Cells(20, Column).Value = LOG_PERF_T_FUEL_DEGF.Value.ToString(LOG_PERF_T_FUEL_DEGF.Format, Nothing) 'TFUEL
                excelWorksheet.Cells(21, Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_PSIG.Format, Nothing) 'PFUEL
                excelWorksheet.Cells(22, Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing) 'VIGGBA
                excelWorksheet.Cells(23, Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing) 'VIBTHA
                excelWorksheet.Cells(24, Column).Value = LOG_PERF_VIB_FAN.Value.ToString(LOG_PERF_VIB_FAN.Format, Nothing) 'VIBTHB
                excelWorksheet.Cells(25, Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing) 'XNL
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing) 'PIGV
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_PSIA.Format, Nothing) 'PCDFD
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_T_TCD_DEGF.Value.ToString(LOG_PERF_T_TCD_DEGF.Format, Nothing) 'TCDFD
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_T_EGT_P1_DEGF.Value.ToString(LOG_PERF_T_EGT_P1_DEGF.Format, Nothing) 'TTDEA
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_T_EGT_P2_DEGF.Value.ToString(LOG_PERF_T_EGT_P2_DEGF.Format, Nothing) 'TTDEB
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_T_EGT_DEGF_AVG.Value.ToString(LOG_PERF_T_EGT_DEGF_AVG.Format, Nothing) 'EGT
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing) 'PS9
                excelWorksheet.Cells(40, Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing) 'WF

                If (ShowEverything) Then
                    excelWorksheet.Cells(33, Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)      'PBORFA
                    excelWorksheet.Cells(34, Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)      'TBORFA
                    excelWorksheet.Cells(35, Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing)    'PDBORA
                    excelWorksheet.Cells(36, Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)                    'WB
                    excelWorksheet.Cells(37, Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)            'WBCDNA
                    excelWorksheet.Cells(38, Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)    'PB
                    excelWorksheet.Cells(39, Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)     'TB
                    excelWorksheet.Cells(41, Column).Value = LOG_PERF_GENERATOR_POWER_KW.Value.ToString(LOG_PERF_GENERATOR_POWER_KW.Format, Nothing) 'PWGEN
                End If

            Catch Ex As Exception
                MessageBox.Show("EPCORWritePerfSheet2: " & Ex.ToString())
            End Try
        End Sub

        Private Sub EthiopieWritePerfSheet3(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("CALCULATIONS")

                excelWorksheet.Cells(17, Column).Value = LOG_PERF_GEN_SL.Value.ToString(LOG_PERF_GEN_SL.Format, Nothing)    'GENSL
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
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)      'TBCOR
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)    'DELEGT
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing) 'Delta P Correction on EGT
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)    'Installation effect on EGT
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)    'Excess bleed P correction on EGT
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)    'EGTCOR
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)      'SL Fuel FLow
                excelWorksheet.Cells(35, Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)      'DELWF
                excelWorksheet.Cells(36, Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)    'Delta P Correction on WF
                excelWorksheet.Cells(37, Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)  'Installation effect on WF
                excelWorksheet.Cells(38, Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing) 'Excess bleed P correction on WF
                excelWorksheet.Cells(39, Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)      'WFCOR

            Catch Ex As Exception
                MessageBox.Show("EthiopieWritePerfSheet3: " & Ex.ToString())
            End Try
        End Sub

        Private Sub FillAllDatas(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ColumnName As String)
            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("RAW DATA")
                Dim startColumn As Integer = 4

                'Header
                excelWorksheet.Cells(2, startColumn + Column).Value = ColumnName

                excelWorksheet.Cells(3, startColumn).Value = "DateTime"
                excelWorksheet.Cells(3, startColumn + Column).NumberFormat = "@"
                excelWorksheet.Cells(3, startColumn + Column).Name = ColumnName & "DateTime"
                excelWorksheet.Cells(3, startColumn + Column).Value = Format(Now, "yyyy-MM-dd HH:mm:ss")



                excelWorksheet.Cells(4, startColumn).Value = "LOG_PERF_T_INLET_DEGF_AVG"
                excelWorksheet.Cells(4, startColumn + Column).Name = ColumnName & "LOG_PERF_T_INLET_DEGF_AVG"
                excelWorksheet.Cells(4, startColumn + Column).Value = LOG_PERF_T_INLET_DEGF_AVG.Value.ToString(LOG_PERF_T_INLET_DEGF_AVG.Format, Nothing)

                excelWorksheet.Cells(5, startColumn).Value = "LOG_PERF_ARINC_T2_DEGF"
                excelWorksheet.Cells(5, startColumn + Column).Name = ColumnName & "LOG_PERF_ARINC_T2_DEGF"
                excelWorksheet.Cells(5, startColumn + Column).Value = LOG_PERF_ARINC_T2_DEGF.Value.ToString(LOG_PERF_ARINC_T2_DEGF.Format, Nothing)

                excelWorksheet.Cells(6, startColumn).Value = "LOG_PERF_P_OIL_PSIG"
                excelWorksheet.Cells(6, startColumn + Column).Name = ColumnName & "LOG_PERF_P_OIL_PSIG"
                excelWorksheet.Cells(6, startColumn + Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing)

                excelWorksheet.Cells(7, startColumn).Value = "LOG_PERF_T_OIL_DEGF"
                excelWorksheet.Cells(7, startColumn + Column).Name = ColumnName & "LOG_PERF_T_OIL_DEGF"
                excelWorksheet.Cells(7, startColumn + Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing)

                excelWorksheet.Cells(8, startColumn).Value = "LOG_PERF_P_GEARBOX_INH2O"
                excelWorksheet.Cells(8, startColumn + Column).Name = ColumnName & "LOG_PERF_P_GEARBOX_INH2O"
                excelWorksheet.Cells(8, startColumn + Column).Value = LOG_PERF_P_GEARBOX_INH2O.Value.ToString(LOG_PERF_P_GEARBOX_INH2O.Format, Nothing)

                excelWorksheet.Cells(9, startColumn).Value = "LOG_PERF_T_FUEL_AVG_DEGF"
                excelWorksheet.Cells(9, startColumn + Column).Name = ColumnName & "LOG_PERF_T_FUEL_AVG_DEGF"
                excelWorksheet.Cells(9, startColumn + Column).Value = LOG_PERF_T_FUEL_AVG_DEGF.Value.ToString(LOG_PERF_T_FUEL_AVG_DEGF.Format, Nothing)

                excelWorksheet.Cells(10, startColumn).Value = "LOG_PERF_P_FUEL_PSIG"
                excelWorksheet.Cells(10, startColumn + Column).Name = ColumnName & "LOG_PERF_P_FUEL_PSIG"
                excelWorksheet.Cells(10, startColumn + Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_PSIG.Format, Nothing)

                excelWorksheet.Cells(11, startColumn).Value = "LOG_PERF_VIB_GEARBOX"
                excelWorksheet.Cells(11, startColumn + Column).Name = ColumnName & "LOG_PERF_VIB_GEARBOX"
                excelWorksheet.Cells(11, startColumn + Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing)

                excelWorksheet.Cells(12, startColumn).Value = "LOG_PERF_VIB_TURBINE"
                excelWorksheet.Cells(12, startColumn + Column).Name = ColumnName & "LOG_PERF_VIB_TURBINE"
                excelWorksheet.Cells(12, startColumn + Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing)

                excelWorksheet.Cells(13, startColumn).Value = "LOG_PERF_VIB_FAN"
                excelWorksheet.Cells(13, startColumn + Column).Name = ColumnName & "LOG_PERF_VIB_FAN"
                excelWorksheet.Cells(13, startColumn + Column).Value = LOG_PERF_VIB_FAN.Value.ToString(LOG_PERF_VIB_FAN.Format, Nothing)

                excelWorksheet.Cells(14, startColumn).Value = "LOG_PERF_NG"
                excelWorksheet.Cells(14, startColumn + Column).Name = ColumnName & "LOG_PERF_NG"
                excelWorksheet.Cells(14, startColumn + Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing)

                excelWorksheet.Cells(15, startColumn).Value = "LOG_PERF_ARINC_IGV_POS"
                excelWorksheet.Cells(15, startColumn + Column).Name = ColumnName & "LOG_PERF_ARINC_IGV_POS"
                excelWorksheet.Cells(15, startColumn + Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing)

                excelWorksheet.Cells(16, startColumn).Value = "LOG_PERF_P_COMP_DISCH_PSIA"
                excelWorksheet.Cells(16, startColumn + Column).Name = ColumnName & "LOG_PERF_P_COMP_DISCH_PSIA"
                excelWorksheet.Cells(16, startColumn + Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_PSIA.Format, Nothing)

                excelWorksheet.Cells(17, startColumn).Value = "LOG_PERF_T_TCD_DEGF"
                excelWorksheet.Cells(17, startColumn + Column).Name = ColumnName & "LOG_PERF_T_TCD_DEGF"
                excelWorksheet.Cells(17, startColumn + Column).Value = LOG_PERF_T_TCD_DEGF.Value.ToString(LOG_PERF_T_TCD_DEGF.Format, Nothing)

                excelWorksheet.Cells(18, startColumn).Value = "LOG_PERF_T_EGT_P1_DEGF"
                excelWorksheet.Cells(18, startColumn + Column).Name = ColumnName & "LOG_PERF_T_EGT_P1_DEGF"
                excelWorksheet.Cells(18, startColumn + Column).Value = LOG_PERF_T_EGT_P1_DEGF.Value.ToString(LOG_PERF_T_EGT_P1_DEGF.Format, Nothing)

                excelWorksheet.Cells(19, startColumn).Value = "LOG_PERF_T_EGT_P2_DEGF"
                excelWorksheet.Cells(19, startColumn + Column).Name = ColumnName & "LOG_PERF_T_EGT_P2_DEGF"
                excelWorksheet.Cells(19, startColumn + Column).Value = LOG_PERF_T_EGT_P2_DEGF.Value.ToString(LOG_PERF_T_EGT_P2_DEGF.Format, Nothing)

                excelWorksheet.Cells(20, startColumn).Value = "LOG_PERF_T_EGT_DEGF_AVG"
                excelWorksheet.Cells(20, startColumn + Column).Name = ColumnName & "LOG_PERF_T_EGT_DEGF_AVG"
                excelWorksheet.Cells(20, startColumn + Column).Value = LOG_PERF_T_EGT_DEGF_AVG.Value.ToString(LOG_PERF_T_EGT_DEGF_AVG.Format, Nothing)

                excelWorksheet.Cells(21, startColumn).Value = "LOG_PERF_P_EXHAUST_PSIA"
                excelWorksheet.Cells(21, startColumn + Column).Name = ColumnName & "LOG_PERF_P_EXHAUST_PSIA"
                excelWorksheet.Cells(21, startColumn + Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing)

                excelWorksheet.Cells(22, startColumn).Value = "LOG_PERF_WF_AVG_PPH"
                excelWorksheet.Cells(22, startColumn + Column).Name = ColumnName & "LOG_PERF_WF_AVG_PPH"
                excelWorksheet.Cells(22, startColumn + Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing)



                excelWorksheet.Cells(23, startColumn).Value = "LOG_PERF_P_ORIFICE_PSIA"
                excelWorksheet.Cells(23, startColumn + Column).Name = ColumnName & "LOG_PERF_P_ORIFICE_PSIA"
                excelWorksheet.Cells(23, startColumn + Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)

                excelWorksheet.Cells(24, startColumn).Value = "LOG_PERF_T_ORIFICE_DEGF"
                excelWorksheet.Cells(24, startColumn + Column).Name = ColumnName & "LOG_PERF_T_ORIFICE_DEGF"
                excelWorksheet.Cells(24, startColumn + Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)

                excelWorksheet.Cells(25, startColumn).Value = "LOG_PERF_P_ORIFICE_DELTA_PSID"
                excelWorksheet.Cells(25, startColumn + Column).Name = ColumnName & "LOG_PERF_P_ORIFICE_DELTA_PSID"
                excelWorksheet.Cells(25, startColumn + Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing)

                excelWorksheet.Cells(26, startColumn).Value = "LOG_PERF_WB"
                excelWorksheet.Cells(26, startColumn + Column).Name = ColumnName & "LOG_PERF_WB"
                excelWorksheet.Cells(26, startColumn + Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)

                excelWorksheet.Cells(27, startColumn).Value = "LOG_PERF_WBCDNA"
                excelWorksheet.Cells(27, startColumn + Column).Name = ColumnName & "LOG_PERF_WBCDNA"
                excelWorksheet.Cells(27, startColumn + Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)

                excelWorksheet.Cells(28, startColumn).Value = "LOG_PERF_P_BLEED1_5_PSIA"
                excelWorksheet.Cells(28, startColumn + Column).Name = ColumnName & "LOG_PERF_P_BLEED1_5_PSIA"
                excelWorksheet.Cells(28, startColumn + Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)

                excelWorksheet.Cells(29, startColumn).Value = "LOG_PERF_T_BLEED_AVG_DEGF"
                excelWorksheet.Cells(29, startColumn + Column).Name = ColumnName & "LOG_PERF_T_BLEED_AVG_DEGF"
                excelWorksheet.Cells(29, startColumn + Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)

                excelWorksheet.Cells(30, startColumn).Value = "LOG_PERF_GENERATOR_POWER_KW"
                excelWorksheet.Cells(30, startColumn + Column).Name = ColumnName & "LOG_PERF_GENERATOR_POWER_KW"
                excelWorksheet.Cells(30, startColumn + Column).Value = LOG_PERF_GENERATOR_POWER_KW.Value.ToString(LOG_PERF_GENERATOR_POWER_KW.Format, Nothing)



                excelWorksheet.Cells(31, startColumn).Value = "LOG_PERF_GEN_SL"
                excelWorksheet.Cells(31, startColumn + Column).Name = ColumnName & "LOG_PERF_GEN_SL"
                excelWorksheet.Cells(31, startColumn + Column).Value = LOG_PERF_GEN_SL.Value.ToString(LOG_PERF_GEN_SL.Format, Nothing)

                excelWorksheet.Cells(32, startColumn).Value = "LOG_PERF_DELTA_P_SL"
                excelWorksheet.Cells(32, startColumn + Column).Name = ColumnName & "LOG_PERF_DELTA_P_SL"
                excelWorksheet.Cells(32, startColumn + Column).Value = LOG_PERF_DELTA_P_SL.Value.ToString(LOG_PERF_DELTA_P_SL.Format, Nothing)

                excelWorksheet.Cells(33, startColumn).Value = "LOG_PERF_PB_SL_PSIA"
                excelWorksheet.Cells(33, startColumn + Column).Name = ColumnName & "LOG_PERF_PB_SL_PSIA"
                excelWorksheet.Cells(33, startColumn + Column).Value = LOG_PERF_PB_SL_PSIA.Value.ToString(LOG_PERF_PB_SL_PSIA.Format, Nothing)

                excelWorksheet.Cells(34, startColumn).Value = "LOG_PERF_DELPB"
                excelWorksheet.Cells(34, startColumn + Column).Name = ColumnName & "LOG_PERF_DELPB"
                excelWorksheet.Cells(34, startColumn + Column).Value = LOG_PERF_DELPB.Value.ToString(LOG_PERF_DELPB.Format, Nothing)

                excelWorksheet.Cells(35, startColumn).Value = "LOG_PERF_PB_INST_PSIA"
                excelWorksheet.Cells(35, startColumn + Column).Name = ColumnName & "LOG_PERF_PB_INST_PSIA"
                excelWorksheet.Cells(35, startColumn + Column).Value = LOG_PERF_PB_INST_PSIA.Value.ToString(LOG_PERF_PB_INST_PSIA.Format, Nothing)

                excelWorksheet.Cells(36, startColumn).Value = "LOG_PERF_PBCOR"
                excelWorksheet.Cells(36, startColumn + Column).Name = ColumnName & "LOG_PERF_PBCOR"
                excelWorksheet.Cells(36, startColumn + Column).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)

                excelWorksheet.Cells(37, startColumn).Value = "LOG_PERF_WB_SL"
                excelWorksheet.Cells(37, startColumn + Column).Name = ColumnName & "LOG_PERF_WB_SL"
                excelWorksheet.Cells(37, startColumn + Column).Value = LOG_PERF_WB_SL.Value.ToString(LOG_PERF_WB_SL.Format, Nothing)

                excelWorksheet.Cells(38, startColumn).Value = "LOG_PERF_DELWB"
                excelWorksheet.Cells(38, startColumn + Column).Name = ColumnName & "LOG_PERF_DELWB"
                excelWorksheet.Cells(38, startColumn + Column).Value = LOG_PERF_DELWB.Value.ToString(LOG_PERF_DELWB.Format, Nothing)

                excelWorksheet.Cells(39, startColumn).Value = "LOG_PERF_WB_INST"
                excelWorksheet.Cells(39, startColumn + Column).Name = ColumnName & "LOG_PERF_WB_INST"
                excelWorksheet.Cells(39, startColumn + Column).Value = LOG_PERF_WB_INST.Value.ToString(LOG_PERF_WB_INST.Format, Nothing)

                excelWorksheet.Cells(40, startColumn).Value = "LOG_PERF_WBCOR"
                excelWorksheet.Cells(40, startColumn + Column).Name = ColumnName & "LOG_PERF_WBCOR"
                excelWorksheet.Cells(40, startColumn + Column).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)

                excelWorksheet.Cells(41, startColumn).Value = "LOG_PERF_DELTB"
                excelWorksheet.Cells(41, startColumn + Column).Name = ColumnName & "LOG_PERF_DELTB"
                excelWorksheet.Cells(41, startColumn + Column).Value = LOG_PERF_DELTB.Value.ToString(LOG_PERF_DELTB.Format, Nothing)

                excelWorksheet.Cells(42, startColumn).Value = "LOG_PERF_TBCOR"
                excelWorksheet.Cells(42, startColumn + Column).Name = ColumnName & "LOG_PERF_TBCOR"
                excelWorksheet.Cells(42, startColumn + Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)

                excelWorksheet.Cells(43, startColumn).Value = "LOG_PERF_DELEGT"
                excelWorksheet.Cells(43, startColumn + Column).Name = ColumnName & "LOG_PERF_DELEGT"
                excelWorksheet.Cells(43, startColumn + Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)

                excelWorksheet.Cells(44, startColumn).Value = "LOG_PERF_EGT_DP_CORR"
                excelWorksheet.Cells(44, startColumn + Column).Name = ColumnName & "LOG_PERF_EGT_DP_CORR"
                excelWorksheet.Cells(44, startColumn + Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing)

                excelWorksheet.Cells(45, startColumn).Value = "LOG_PERF_EGT_INST"
                excelWorksheet.Cells(45, startColumn + Column).Name = ColumnName & "LOG_PERF_EGT_INST"
                excelWorksheet.Cells(45, startColumn + Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)

                excelWorksheet.Cells(46, startColumn).Value = "LOG_PERF_EGT_EXC_PB"
                excelWorksheet.Cells(46, startColumn + Column).Name = ColumnName & "LOG_PERF_EGT_EXC_PB"
                excelWorksheet.Cells(46, startColumn + Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)

                excelWorksheet.Cells(47, startColumn).Value = "LOG_PERF_EGTCOR"
                excelWorksheet.Cells(47, startColumn + Column).Name = ColumnName & "LOG_PERF_EGTCOR"
                excelWorksheet.Cells(47, startColumn + Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)

                excelWorksheet.Cells(48, startColumn).Value = "LOG_PERF_WF_SL"
                excelWorksheet.Cells(48, startColumn + Column).Name = ColumnName & "LOG_PERF_WF_SL"
                excelWorksheet.Cells(48, startColumn + Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)

                excelWorksheet.Cells(49, startColumn).Value = "LOG_PERF_DELWF"
                excelWorksheet.Cells(49, startColumn + Column).Name = ColumnName & "LOG_PERF_DELWF"
                excelWorksheet.Cells(49, startColumn + Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)

                excelWorksheet.Cells(50, startColumn).Value = "LOG_PERF_WF_DP_CORR"
                excelWorksheet.Cells(50, startColumn + Column).Name = ColumnName & "LOG_PERF_WF_DP_CORR"
                excelWorksheet.Cells(50, startColumn + Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)

                excelWorksheet.Cells(51, startColumn).Value = "LOG_PERF_WF_INST"
                excelWorksheet.Cells(51, startColumn + Column).Name = ColumnName & "LOG_PERF_WF_INST"
                excelWorksheet.Cells(51, startColumn + Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)

                excelWorksheet.Cells(52, startColumn).Value = "LOG_PERF_WF_EXC_PB"
                excelWorksheet.Cells(52, startColumn + Column).Name = ColumnName & "LOG_PERF_WF_EXC_PB"
                excelWorksheet.Cells(52, startColumn + Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing)

                excelWorksheet.Cells(53, startColumn).Value = "LOG_PERF_WFCOR"
                excelWorksheet.Cells(53, startColumn + Column).Name = ColumnName & "LOG_PERF_WFCOR"
                excelWorksheet.Cells(53, startColumn + Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)



                'excelWorksheet.Cells(54, startColumn).Value = "Log_Perf_InitPBCOR"
                'excelWorksheet.Cells(54, startColumn + Column).Name = ColumnName & "Log_Perf_InitPBCOR"
                'excelWorksheet.Cells(54, startColumn + Column).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)

                'excelWorksheet.Cells(55, startColumn).Value = "Log_Perf_FinalPBCOR"
                'excelWorksheet.Cells(55, startColumn + Column).Name = ColumnName & "Log_Perf_FinalPBCOR"
                'excelWorksheet.Cells(55, startColumn + Column).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)

                'excelWorksheet.Cells(56, startColumn).Value = "Log_Perf_InitIGV"
                'excelWorksheet.Cells(56, startColumn + Column).Name = ColumnName & "Log_Perf_InitIGV"
                'excelWorksheet.Cells(56, startColumn + Column).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)

                'excelWorksheet.Cells(57, startColumn).Value = "Log_Perf_FinalIGV"
                'excelWorksheet.Cells(57, startColumn + Column).Name = ColumnName & "Log_Perf_FinalIGV"
                'excelWorksheet.Cells(57, startColumn + Column).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)

                'excelWorksheet.Cells(58, startColumn).Value = "LOG_PERF_ECS_OFFSET"
                'excelWorksheet.Cells(58, startColumn + Column).Name = ColumnName & "LOG_PERF_ECS_OFFSET"
                'excelWorksheet.Cells(58, startColumn + Column).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)



                excelWorksheet.Cells(59, startColumn).Value = "LOG_PERF_WBCDNA_LO_LIM"
                excelWorksheet.Cells(59, startColumn + Column).Name = ColumnName & "LOG_PERF_WBCDNA_LO_LIM"
                excelWorksheet.Cells(59, startColumn + Column).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)

                excelWorksheet.Cells(60, startColumn).Value = "LOG_PERF_WBCDNA_HI_LIM"
                excelWorksheet.Cells(60, startColumn + Column).Name = ColumnName & "LOG_PERF_WBCDNA_HI_LIM"
                excelWorksheet.Cells(60, startColumn + Column).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)

                excelWorksheet.Cells(61, startColumn).Value = "LOG_PERF_FLOW_SENSOR_ACC"
                excelWorksheet.Cells(61, startColumn + Column).Name = ColumnName & "LOG_PERF_FLOW_SENSOR_ACC"
                excelWorksheet.Cells(61, startColumn + Column).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)

                excelWorksheet.Cells(62, startColumn).Value = "Log_Perf_SCVStability"
                excelWorksheet.Cells(62, startColumn + Column).Name = ColumnName & "Log_Perf_SCVStability"
                If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                    excelWorksheet.Cells(62, startColumn + Column).Value = "PASSED"
                Else
                    excelWorksheet.Cells(62, startColumn + Column).Value = "FAILED"
                End If

                excelWorksheet.Cells(63, startColumn).Value = "Log_Perf_MinSurgeMargin"
                excelWorksheet.Cells(63, startColumn + Column).Name = ColumnName & "Log_Perf_MinSurgeMargin"
                If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                    excelWorksheet.Cells(63, startColumn + Column).Value = "PASSED"
                Else
                    excelWorksheet.Cells(63, startColumn + Column).Value = "FAILED"
                End If

                'excelWorksheet.Cells(64, startColumn).Value = "Log_Perf_LoadCycle96kW"
                'excelWorksheet.Cells(64, startColumn + Column).Name = ColumnName & "Log_Perf_LoadCycle96kW"
                'If CELProject.IsTrue(Log_Perf_LoadCycle96kW.Value) Then
                '    excelWorksheet.Cells(64, 8).Value = "PASSED"
                'Else
                '    excelWorksheet.Cells(64, 8).Value = "FAILED"
                'End If



                'excelWorksheet.Cells(73, startColumn).Value = "NUMBER_OF_STARTS"
                'excelWorksheet.Cells(73, startColumn + Column).Name = ColumnName & "NUMBER_OF_STARTS"
                'excelWorksheet.Cells(73, startColumn + Column).Value = NUMBER_OF_STARTS.Value.ToString()

                excelWorksheet.Cells(73, startColumn).Value = "LOG_PERF_OPERATING_TIME_CURRENT_HOURS"
                excelWorksheet.Cells(73, startColumn + Column).Name = ColumnName & "LOG_PERF_OPERATING_TIME_CURRENT_HOURS"
                excelWorksheet.Cells(73, startColumn + Column).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)

                excelWorksheet.Cells(74, startColumn).Value = "LOG_PERF_P_BARO_PSIA"
                excelWorksheet.Cells(74, startColumn + Column).Name = ColumnName & "LOG_PERF_P_BARO_PSIA"
                excelWorksheet.Cells(74, startColumn + Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing)



            Catch ex As Exception
                MessageBox.Show("FillAllDatas: " & ex.ToString())
            End Try
        End Sub

        Private Sub EthiopieWritePerfSheet2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ShowEverything As Boolean)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("DETAILS")

                excelWorksheet.Cells(16, Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing) 'PBAR
                excelWorksheet.Cells(17, Column).Value = LOG_PERF_T_INLET_DEGF_AVG.Value.ToString(LOG_PERF_T_INLET_DEGF_AVG.Format, Nothing) 'T1
                excelWorksheet.Cells(18, Column).Value = LOG_PERF_ARINC_T2_DEGF.Value.ToString(LOG_PERF_ARINC_T2_DEGF.Format, Nothing) 'TENIVA
                excelWorksheet.Cells(19, Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing) 'POIL
                excelWorksheet.Cells(20, Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing) 'TOIL
                excelWorksheet.Cells(21, Column).Value = LOG_PERF_P_GEARBOX_INH2O.Value.ToString(LOG_PERF_P_GEARBOX_INH2O.Format, Nothing) 'PSGBX
                excelWorksheet.Cells(22, Column).Value = LOG_PERF_T_FUEL_AVG_DEGF.Value.ToString(LOG_PERF_T_FUEL_AVG_DEGF.Format, Nothing) 'TFUEL
                excelWorksheet.Cells(23, Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_PSIG.Format, Nothing) 'PFUEL
                excelWorksheet.Cells(24, Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing) 'VIGGBA
                excelWorksheet.Cells(25, Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing) 'VIBTHA
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_VIB_FAN.Value.ToString(LOG_PERF_VIB_FAN.Format, Nothing) 'VIBTHB
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing) 'XNL
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing) 'PIGV
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_PSIA.Format, Nothing) 'PCDFD
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_T_TCD_DEGF.Value.ToString(LOG_PERF_T_TCD_DEGF.Format, Nothing) 'TCDFD
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_T_EGT_P1_DEGF.Value.ToString(LOG_PERF_T_EGT_P1_DEGF.Format, Nothing) 'TTDEA
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_T_EGT_P2_DEGF.Value.ToString(LOG_PERF_T_EGT_P2_DEGF.Format, Nothing) 'TTDEB
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_T_EGT_DEGF_AVG.Value.ToString(LOG_PERF_T_EGT_DEGF_AVG.Format, Nothing) 'EGT
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing) 'PS9
                excelWorksheet.Cells(42, Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing) 'WF

                If (ShowEverything) Then
                    excelWorksheet.Cells(35, Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)      'PBORFA
                    excelWorksheet.Cells(36, Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)      'TBORFA
                    excelWorksheet.Cells(37, Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing)    'PDBORA
                    excelWorksheet.Cells(38, Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)                    'WB
                    excelWorksheet.Cells(39, Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)            'WBCDNA
                    excelWorksheet.Cells(40, Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)    'PB
                    excelWorksheet.Cells(41, Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)     'TB
                    excelWorksheet.Cells(43, Column).Value = LOG_PERF_GENERATOR_POWER_KW.Value.ToString(LOG_PERF_GENERATOR_POWER_KW.Format, Nothing) 'PWGEN
                End If

            Catch Ex As Exception
                MessageBox.Show("EthiopieWritePerfSheet2: " & Ex.ToString())
            End Try
        End Sub

        Private Sub THY13015WritePerfSheet3(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("CALCULATIONS")

                excelWorksheet.Cells(17, Column).Value = LOG_PERF_GEN_SL.Value.ToString(LOG_PERF_GEN_SL.Format, Nothing)    'GENSL
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
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)      'TBCOR
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)    'DELEGT
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing) 'Delta P Correction on EGT
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)    'Installation effect on EGT
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)    'Excess bleed P correction on EGT
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)    'EGTCOR
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)      'SL Fuel FLow
                excelWorksheet.Cells(35, Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)      'DELWF
                excelWorksheet.Cells(36, Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)    'Delta P Correction on WF
                excelWorksheet.Cells(37, Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)  'Installation effect on WF
                excelWorksheet.Cells(38, Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing) 'Excess bleed P correction on WF
                excelWorksheet.Cells(39, Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)      'WFCOR

            Catch Ex As Exception
                MessageBox.Show("EthiopieWritePerfSheet3: " & Ex.ToString())
            End Try

        End Sub

        Private Sub THY13015WritePerfSheet2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer, ByVal ShowEverything As Boolean)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("DETAILS")

                excelWorksheet.Cells(16, Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing) 'PBAR
                'excelWorksheet.Cells(16, Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing) + String.Format(" ({0})", LOG_PERF_P_BARO_BARA.Value.ToString(LOG_PERF_P_BARO_BARA.Format, Nothing))

                excelWorksheet.Cells(17, Column).Value = LOG_PERF_T_INLET_DEGF_AVG.Value.ToString(LOG_PERF_T_INLET_DEGF_AVG.Format, Nothing) 'T1
                excelWorksheet.Cells(18, Column).Value = LOG_PERF_ARINC_T2_DEGF.Value.ToString(LOG_PERF_ARINC_T2_DEGF.Format, Nothing) 'TENIVA
                excelWorksheet.Cells(19, Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing) 'POIL
                excelWorksheet.Cells(20, Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing) 'TOIL
                excelWorksheet.Cells(21, Column).Value = LOG_PERF_P_GEARBOX_INH2O.Value.ToString(LOG_PERF_P_GEARBOX_INH2O.Format, Nothing) 'PSGBX
                excelWorksheet.Cells(22, Column).Value = LOG_PERF_T_FUEL_AVG_DEGF.Value.ToString(LOG_PERF_T_FUEL_AVG_DEGF.Format, Nothing) 'TFUEL
                excelWorksheet.Cells(23, Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_PSIG.Format, Nothing) 'PFUEL
                excelWorksheet.Cells(24, Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing) 'VIGGBA
                excelWorksheet.Cells(25, Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing) 'VIBTHA
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_VIB_FAN.Value.ToString(LOG_PERF_VIB_FAN.Format, Nothing) 'VIBTHB
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing) 'XNL
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing) 'PIGV
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_PSIA.Format, Nothing) 'PCDFD
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_T_TCD_DEGF.Value.ToString(LOG_PERF_T_TCD_DEGF.Format, Nothing) 'TCDFD
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_T_EGT_P1_DEGF.Value.ToString(LOG_PERF_T_EGT_P1_DEGF.Format, Nothing) 'TTDEA
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_T_EGT_P2_DEGF.Value.ToString(LOG_PERF_T_EGT_P2_DEGF.Format, Nothing) 'TTDEB
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_T_EGT_DEGF_AVG.Value.ToString(LOG_PERF_T_EGT_DEGF_AVG.Format, Nothing) 'EGT
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing) 'PS9
                excelWorksheet.Cells(42, Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing) 'WF

                If (ShowEverything) Then
                    excelWorksheet.Cells(35, Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)      'PBORFA
                    excelWorksheet.Cells(36, Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)      'TBORFA
                    excelWorksheet.Cells(37, Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing)    'PDBORA
                    excelWorksheet.Cells(38, Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)                    'WB
                    excelWorksheet.Cells(39, Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)            'WBCDNA
                    excelWorksheet.Cells(40, Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)    'PB
                    excelWorksheet.Cells(41, Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)     'TB
                    excelWorksheet.Cells(43, Column).Value = LOG_PERF_GENERATOR_POWER_KW.Value.ToString(LOG_PERF_GENERATOR_POWER_KW.Format, Nothing) 'PWGEN
                End If

            Catch Ex As Exception
                MessageBox.Show("EthiopieWritePerfSheet2: " & Ex.ToString())
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

                excelWorksheet = excelSheets.Item("SUMMARY")

                If AppendOption = False Then

                    'APU Type: text directly in the Excel sheet

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
                    excelWorksheet = excelSheets.Item("ACCEPTANCE")
                    excelWorksheet.Cells(47, 1).NumberFormat = "@"
                    excelWorksheet.Cells(47, 1).Value = excelWorksheet.Cells(47, 1).Value.ToString() + Custom_Parameters("APUTestedAccordingTo").ToString()

                End If

                Select Case TestStepNumber

                    Case eTestStepGTCP131_9B.TCOND1 'Ready To Load

                        EPCORWritePerfSheet2(excelSheets, 9, False) 'RTL

                    Case eTestStepGTCP131_9B.IGV_OFF

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' ECS OFFSET WORKSHEET section
                        excelWorksheet.Cells(24, 8).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)
                        excelWorksheet.Cells(25, 8).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)
                        excelWorksheet.Cells(26, 8).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)
                        excelWorksheet.Cells(27, 8).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)
                        excelWorksheet.Cells(28, 8).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND2

                        ' ECS HIGH
                        EPCORWritePerfSheet2(excelSheets, 7, True)
                        EPCORWritePerfSheet3(excelSheets, 5)

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        If (HeavyRepairType) Then
                            excelWorksheet.Cells(15, 5).NumberFormat = "@"
                            excelWorksheet.Cells(15, 5).Value = "51.2 (3,53) MINIMUM"
                            excelWorksheet.Cells(16, 5).NumberFormat = "@"
                            excelWorksheet.Cells(16, 5).Value = "155 (70,31) MINIMUM"
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "445 (229) MAXIMUM"
                            excelWorksheet.Cells(18, 5).NumberFormat = "@"
                            excelWorksheet.Cells(18, 5).Value = "1090 (588) MAXiMUM"
                        Else
                            excelWorksheet.Cells(15, 5).NumberFormat = "@"
                            excelWorksheet.Cells(15, 5).Value = "51.2 (3,53) MINIMUM"
                            excelWorksheet.Cells(16, 5).NumberFormat = "@"
                            excelWorksheet.Cells(16, 5).Value = "155 (70,31) MINIMUM"
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "445 (229) MAXIMUM"
                            excelWorksheet.Cells(18, 5).NumberFormat = "@"
                            excelWorksheet.Cells(18, 5).Value = "1115 (601) MAXIMUM"
                        End If

                        ' Performance Summary, Data point 0002, Actual
                        excelWorksheet.Cells(15, 6).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(16, 6).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(17, 6).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                        excelWorksheet.Cells(18, 6).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(19, 6).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND3

                        ' MES
                        EPCORWritePerfSheet2(excelSheets, 8, True)
                        EPCORWritePerfSheet3(excelSheets, 6)

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        If (HeavyRepairType) Then
                            excelWorksheet.Cells(15, 7).NumberFormat = "@"
                            excelWorksheet.Cells(15, 7).Value = "54.5 (3,76) MINIMUM"
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "445 (229) MAXIMUM"
                            excelWorksheet.Cells(18, 7).NumberFormat = "@"
                            excelWorksheet.Cells(18, 7).Value = "1080 (582) MAXIMUM"
                        Else
                            excelWorksheet.Cells(15, 7).NumberFormat = "@"
                            excelWorksheet.Cells(15, 7).Value = "53.7 (3,70) MINIMUM"
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "445 (229) MAXIMUM"
                            excelWorksheet.Cells(18, 75).NumberFormat = "@"
                            excelWorksheet.Cells(18, 7).Value = "1105 (596) MAXIMUM"
                        End If

                        ' Performance Summary, Data point 0003, Actual
                        excelWorksheet.Cells(15, 8).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(16, 8).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(17, 8).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                        excelWorksheet.Cells(18, 8).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(19, 8).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND4

                        ' WBCDNA Flow sensor test
                        excelWorksheet = excelSheets.Item("SUMMARY")
                        excelWorksheet.Cells(32, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(33, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(32, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND5

                        ' WBCDNA flow sensor test and WC flow sensor accuracy
                        excelWorksheet = excelSheets.Item("SUMMARY")
                        excelWorksheet.Cells(34, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(35, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(34, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                        excelWorksheet.Cells(36, 8).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND6

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                            excelWorksheet.Cells(39, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(39, 8).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                            excelWorksheet.Cells(40, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(40, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND7

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycle96kW.Value) Then
                            excelWorksheet.Cells(43, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(43, 8).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(47, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(47, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND8

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycleMES.Value) Then
                            excelWorksheet.Cells(44, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(44, 8).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(47, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(47, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND9

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycleMES96kW.Value) Then
                            excelWorksheet.Cells(45, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(45, 8).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(BLEED_BTN.Value) Then

                            If UserInputsEPCOR.ChoiceRepairType = "Heavy" Then
                                excelWorksheet.Cells(46, 7).Value = "MAX 1135 F"
                            Else
                                excelWorksheet.Cells(46, 7).Value = "MAX 1160 F"
                            End If

                            excelWorksheet.Cells(46, 8).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing) & " F"
                        End If


                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(47, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(47, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.FINAL_DATA

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        excelWorksheet.Cells(41, 8).Value = Log_Perf_ACStartTime.Value.ToString(Log_Perf_ACStartTime.Format, Nothing)
                        excelWorksheet.Cells(42, 8).Value = Log_Perf_DCStartTime.Value.ToString(Log_Perf_DCStartTime.Format, Nothing)
                        excelWorksheet.Cells(48, 8).Value = NUMBER_OF_STARTS.Value.ToString()
                        excelWorksheet.Cells(49, 8).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)
                        excelWorksheet.Cells(50, 8).Value = Log_Perf_NbApuFaults.Value.ToString()

                End Select

                'TestProgress_RecordPerfStep(TestStepNumber)

                'Write the manual Entry
                excelWorksheet = excelSheets.Item("ACCEPTANCE")

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
                System.Diagnostics.Trace.WriteLine("GTCP131_9B_Sequence.CreateEPCOR12958PerformanceFile: " + ex.ToString())
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

        Private Function CreateEthiopie11825PerformanceFile(ByVal AppendOption As Boolean, ByVal TestStepNumber As Integer, ByVal ProtectPassword As String, ByVal UnProtectPassword As String, ByRef excelWorkbook As Excel.Workbook) As Boolean
            'Declaration
            Dim excelSheets As Excel.Sheets
            Dim excelWorksheet As Excel.Worksheet

            CreateEthiopie11825PerformanceFile = True

            ' The following gets the Worksheets collection
            excelSheets = excelWorkbook.Worksheets

            Try
                excelWorkbook.Worksheets(1).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(2).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(3).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(4).Unprotect(UnProtectPassword)

                excelWorksheet = excelSheets.Item("SUMMARY")

                If AppendOption = False Then

                    'APU Type: text directly in the Excel sheet

                    'Eng S/N
                    excelWorksheet.Cells(5, 3).NumberFormat = "@"
                    excelWorksheet.Cells(5, 3).Value = UserInputsEthiopie.SerialNumber

                    'Work Order
                    excelWorksheet.Cells(6, 3).NumberFormat = "@"
                    excelWorksheet.Cells(6, 3).Value = UserInputsEthiopie.WorkOrder

                    'ECB S/N
                    excelWorksheet.Cells(7, 3).NumberFormat = "@"
                    excelWorksheet.Cells(7, 3).Value = UserInputsEthiopie.PartNumber

                    'Engine Vibration Pickup Serial Number
                    excelWorksheet.Cells(8, 3).NumberFormat = "@"
                    excelWorksheet.Cells(8, 3).Value = UserInputsEthiopie.EngineVibrationPickupSerialNumber

                    'Gearbox Vibration Pickup Serial Number
                    excelWorksheet.Cells(9, 3).NumberFormat = "@"
                    excelWorksheet.Cells(9, 3).Value = UserInputsEthiopie.GearboxVibrationPickupSerialNumber

                    'Cooling Fan Vibration Pickup Serial Number
                    excelWorksheet.Cells(10, 3).NumberFormat = "@"
                    excelWorksheet.Cells(10, 3).Value = UserInputsEthiopie.CoolingFanVibrationPickupSerialNumber

                    'Fuel SG
                    excelWorksheet.Cells(11, 3).NumberFormat = "@"
                    excelWorksheet.Cells(11, 3).Value = UserInputsEthiopie.FuelSG

                    'T Fuel
                    excelWorksheet.Cells(5, 8).NumberFormat = "@"
                    excelWorksheet.Cells(5, 8).Value = UserInputsEthiopie.TFuelDegF

                    'Fuel Type
                    excelWorksheet.Cells(6, 8).NumberFormat = "@"
                    excelWorksheet.Cells(6, 8).Value = UserInputsEthiopie.FuelType

                    'Oil Type
                    excelWorksheet.Cells(7, 8).NumberFormat = "@"
                    excelWorksheet.Cells(7, 8).Value = UserInputsEthiopie.OilType

                    'LHV 
                    excelWorksheet.Cells(8, 8).NumberFormat = "@"
                    excelWorksheet.Cells(8, 8).Value = UserInputsEthiopie.LHV

                    'Test Type
                    excelWorksheet.Cells(9, 8).NumberFormat = "@"
                    excelWorksheet.Cells(9, 8).Value = UserInputsEthiopie.ChoiceTestType

                    'Repair Type
                    excelWorksheet.Cells(10, 8).NumberFormat = "@"
                    excelWorksheet.Cells(10, 8).Value = UserInputsEthiopie.ChoiceRepairType

                    'Test Date
                    excelWorksheet.Cells(11, 8).NumberFormat = "@"
                    excelWorksheet.Cells(11, 8).Value = Format(Now, "d MMMM yyyy")

                    'Document Number
                    excelWorksheet = excelSheets.Item("ACCEPTANCE")
                    excelWorksheet.Cells(47, 1).NumberFormat = "@"
                    excelWorksheet.Cells(47, 1).Value = excelWorksheet.Cells(47, 1).Value.ToString() + Custom_Parameters("APUTestedAccordingTo").ToString()

                End If

                Select Case TestStepNumber

                    Case eTestStepGTCP131_9B.TCOND1 'Ready To Load

                        EthiopieWritePerfSheet2(excelSheets, 9, False) 'RTL

                    Case eTestStepGTCP131_9B.IGV_OFF

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' ECS OFFSET WORKSHEET section
                        excelWorksheet.Cells(26, 8).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)
                        excelWorksheet.Cells(27, 8).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)
                        excelWorksheet.Cells(28, 8).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)
                        excelWorksheet.Cells(29, 8).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)
                        excelWorksheet.Cells(30, 8).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND2

                        ' ECS HIGH
                        EthiopieWritePerfSheet2(excelSheets, 7, True)
                        EthiopieWritePerfSheet3(excelSheets, 5)

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        If (HeavyRepairType) Then
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "51.2 MINIMUM"
                            excelWorksheet.Cells(18, 5).NumberFormat = "@"
                            excelWorksheet.Cells(18, 5).Value = "155 MINIMUM"
                            excelWorksheet.Cells(19, 5).NumberFormat = "@"
                            excelWorksheet.Cells(19, 5).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 5).NumberFormat = "@"
                            excelWorksheet.Cells(20, 5).Value = "1090 MAXiMUM"
                        Else
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "51.2 MINIMUM"
                            excelWorksheet.Cells(18, 5).NumberFormat = "@"
                            excelWorksheet.Cells(18, 5).Value = "155 MINIMUM"
                            excelWorksheet.Cells(19, 5).NumberFormat = "@"
                            excelWorksheet.Cells(19, 5).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 5).NumberFormat = "@"
                            excelWorksheet.Cells(20, 5).Value = "1115 MAXIMUM"
                        End If

                        ' Performance Summary, Data point 0002, Actual
                        excelWorksheet.Cells(17, 6).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(18, 6).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(19, 6).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                        excelWorksheet.Cells(20, 6).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(21, 6).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND3

                        ' MES
                        EthiopieWritePerfSheet2(excelSheets, 8, True)
                        EthiopieWritePerfSheet3(excelSheets, 6)

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        If (HeavyRepairType) Then
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "54.5 MINIMUM"
                            excelWorksheet.Cells(19, 7).NumberFormat = "@"
                            excelWorksheet.Cells(19, 7).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 7).NumberFormat = "@"
                            excelWorksheet.Cells(20, 7).Value = "1080 MAXIMUM"
                        Else
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "53.7 MINIMUM"
                            excelWorksheet.Cells(19, 7).NumberFormat = "@"
                            excelWorksheet.Cells(19, 7).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 75).NumberFormat = "@"
                            excelWorksheet.Cells(20, 7).Value = "1105 MAXIMUM"
                        End If

                        ' Performance Summary, Data point 0003, Actual
                        excelWorksheet.Cells(17, 8).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(18, 8).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(19, 8).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                        excelWorksheet.Cells(20, 8).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(21, 8).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND4

                        ' WBCDNA Flow sensor test
                        excelWorksheet = excelSheets.Item("SUMMARY")
                        excelWorksheet.Cells(34, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(35, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(34, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND5

                        ' WBCDNA flow sensor test and WC flow sensor accuracy
                        excelWorksheet = excelSheets.Item("SUMMARY")
                        excelWorksheet.Cells(36, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(37, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(36, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                        excelWorksheet.Cells(38, 8).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND6

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                            excelWorksheet.Cells(41, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(41, 8).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                            excelWorksheet.Cells(42, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(42, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND7

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycle96kW.Value) Then
                            excelWorksheet.Cells(45, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(45, 8).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(49, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(49, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND8

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycleMES.Value) Then
                            excelWorksheet.Cells(46, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(46, 8).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(49, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(49, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND9

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycleMES96kW.Value) Then
                            excelWorksheet.Cells(47, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(47, 8).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(BLEED_BTN.Value) Then

                            If UserInputsEthiopie.ChoiceRepairType = "Heavy" Then
                                excelWorksheet.Cells(48, 7).Value = "MAX 1135 F"
                            Else
                                excelWorksheet.Cells(48, 7).Value = "MAX 1160 F"
                            End If

                            excelWorksheet.Cells(48, 8).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing) & " F"
                        End If

                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(49, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(49, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.FINAL_DATA

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        excelWorksheet.Cells(43, 8).Value = Log_Perf_ACStartTime.Value.ToString(Log_Perf_ACStartTime.Format, Nothing)
                        excelWorksheet.Cells(44, 8).Value = Log_Perf_DCStartTime.Value.ToString(Log_Perf_DCStartTime.Format, Nothing)
                        excelWorksheet.Cells(49, 8).Value = NUMBER_OF_STARTS.Value.ToString()
                        excelWorksheet.Cells(50, 8).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)
                        excelWorksheet.Cells(51, 8).Value = Log_Perf_NbApuFaults.Value.ToString()

                End Select

                'TestProgress_RecordPerfStep(TestStepNumber)
                'TestProgress_Save()

                'Write the manual Entry
                excelWorksheet = excelSheets.Item("ACCEPTANCE")

                'Test Result
                excelWorksheet.CheckBoxes("chkTestResultAccepted ").value = CELProject.IsTrue(CELProject.PERF_TEST_RESULT_ACCEPTED.Value)
                excelWorksheet.CheckBoxes("chkTestResultRejected ").value = CELProject.IsTrue(CELProject.PERF_TEST_RESULT_REJECTED.Value)


                excelWorksheet.Cells(53, 3).Value = UserInputsEthiopie.ChoiceOperator
                excelWorksheet.Cells(54, 3).Value = UserInputsEthiopie.ChoiceInspector

            Catch ex As Exception
                System.Diagnostics.Trace.WriteLine("GTCP131_9B_Sequence.CreateEthiopie11825PerformanceFile: " + ex.ToString())
                MessageBox.Show(ex.ToString)
                CreateEthiopie11825PerformanceFile = False
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

                excelWorksheet = excelSheets.Item("SUMMARY")

                If AppendOption = False Then

                    'APU Type: text directly in the Excel sheet

                    'Eng S/N
                    excelWorksheet.Cells(5, 3).NumberFormat = "@"
                    excelWorksheet.Cells(5, 3).Value = UserInputsTHY.SerialNumber

                    'Work Order
                    excelWorksheet.Cells(6, 3).NumberFormat = "@"
                    excelWorksheet.Cells(6, 3).Value = UserInputsTHY.WorkOrder

                    'ECB S/N
                    excelWorksheet.Cells(7, 3).NumberFormat = "@"
                    excelWorksheet.Cells(7, 3).Value = UserInputsTHY.PartNumber

                    'Oil Type
                    excelWorksheet.Cells(8, 3).NumberFormat = "@"
                    excelWorksheet.Cells(8, 3).Value = UserInputsTHY.OilType

                    'Fuel Type
                    excelWorksheet.Cells(5, 8).NumberFormat = "@"
                    excelWorksheet.Cells(5, 8).Value = UserInputsTHY.FuelType

                    'Test Type
                    excelWorksheet.Cells(6, 8).NumberFormat = "@"
                    excelWorksheet.Cells(6, 8).Value = UserInputsTHY.ChoiceTestType

                    'Repair Type
                    excelWorksheet.Cells(7, 8).NumberFormat = "@"
                    excelWorksheet.Cells(7, 8).Value = UserInputsTHY.ChoiceRepairType

                    'Test Date
                    excelWorksheet.Cells(8, 8).NumberFormat = "@"
                    excelWorksheet.Cells(8, 8).Value = Format(Now, "d MMMM yyyy")

                    'Document Number
                    excelWorksheet = excelSheets.Item("ACCEPTANCE")
                    excelWorksheet.Cells(47, 1).NumberFormat = "@"
                    excelWorksheet.Cells(47, 1).Value = excelWorksheet.Cells(47, 1).Value.ToString() + Custom_Parameters("APUTestedAccordingTo").ToString()

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
                    excelWorksheet = excelSheets.Item("SUMMARY")
                    excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    excelWorksheet = excelSheets.Item("DETAILS")
                    excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    excelWorksheet = excelSheets.Item("CALCULATIONS")
                    excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    excelWorksheet = excelSheets.Item("ACCEPTANCE")
                    excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                End If

                Select Case TestStepNumber

                    Case eTestStepGTCP131_9B.TCOND1 'Ready To Load

                        THY13015WritePerfSheet2(excelSheets, 9, False) 'RTL

                    Case eTestStepGTCP131_9B.IGV_OFF

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' ECS OFFSET WORKSHEET section
                        excelWorksheet.Cells(26, 8).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)
                        excelWorksheet.Cells(27, 8).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)
                        excelWorksheet.Cells(28, 8).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)
                        excelWorksheet.Cells(29, 8).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)
                        excelWorksheet.Cells(30, 8).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND2

                        ' ECS HIGH
                        THY13015WritePerfSheet2(excelSheets, 7, True)
                        THY13015WritePerfSheet3(excelSheets, 5)

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        If (HeavyRepairType) Then
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "51.2 MINIMUM"
                            excelWorksheet.Cells(18, 5).NumberFormat = "@"
                            excelWorksheet.Cells(18, 5).Value = "155 MINIMUM"
                            excelWorksheet.Cells(19, 5).NumberFormat = "@"
                            excelWorksheet.Cells(19, 5).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 5).NumberFormat = "@"
                            excelWorksheet.Cells(20, 5).Value = "1090 MAXiMUM"
                        Else
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "51.2 MINIMUM"
                            excelWorksheet.Cells(18, 5).NumberFormat = "@"
                            excelWorksheet.Cells(18, 5).Value = "155 MINIMUM"
                            excelWorksheet.Cells(19, 5).NumberFormat = "@"
                            excelWorksheet.Cells(19, 5).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 5).NumberFormat = "@"
                            excelWorksheet.Cells(20, 5).Value = "1115 MAXIMUM"
                        End If

                        ' Performance Summary, Data point 0002, Actual
                        excelWorksheet.Cells(17, 6).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(18, 6).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(19, 6).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                        excelWorksheet.Cells(20, 6).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(21, 6).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND3

                        ' MES
                        THY13015WritePerfSheet2(excelSheets, 8, True)
                        THY13015WritePerfSheet3(excelSheets, 6)

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        If (HeavyRepairType) Then
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "54.5 MINIMUM"
                            excelWorksheet.Cells(19, 7).NumberFormat = "@"
                            excelWorksheet.Cells(19, 7).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 7).NumberFormat = "@"
                            excelWorksheet.Cells(20, 7).Value = "1080 MAXIMUM"
                        Else
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "53.7 MINIMUM"
                            excelWorksheet.Cells(19, 7).NumberFormat = "@"
                            excelWorksheet.Cells(19, 7).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 75).NumberFormat = "@"
                            excelWorksheet.Cells(20, 7).Value = "1105 MAXIMUM"
                        End If

                        ' Performance Summary, Data point 0003, Actual
                        excelWorksheet.Cells(17, 8).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(18, 8).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(19, 8).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                        excelWorksheet.Cells(20, 8).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(21, 8).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND4

                        ' WBCDNA Flow sensor test
                        excelWorksheet = excelSheets.Item("SUMMARY")
                        excelWorksheet.Cells(34, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(35, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(34, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND5

                        ' WBCDNA flow sensor test and WC flow sensor accuracy
                        excelWorksheet = excelSheets.Item("SUMMARY")
                        excelWorksheet.Cells(36, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(37, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(36, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                        excelWorksheet.Cells(38, 8).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND6

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                            excelWorksheet.Cells(41, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(41, 8).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                            excelWorksheet.Cells(42, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(42, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND7

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycle96kW.Value) Then
                            excelWorksheet.Cells(45, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(45, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND8

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycleMES.Value) Then
                            excelWorksheet.Cells(46, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(46, 8).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND9

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycleMES96kW.Value) Then
                            excelWorksheet.Cells(47, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(47, 8).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(BLEED_BTN.Value) Then

                            If UserInputsEPCOR.ChoiceRepairType = "Heavy" Then
                                excelWorksheet.Cells(48, 7).Value = "MAX 1135 F"
                            Else
                                excelWorksheet.Cells(48, 7).Value = "MAX 1160 F"
                            End If

                            excelWorksheet.Cells(48, 8).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing) & " F"
                        End If

                    Case eTestStepGTCP131_9B.FINAL_DATA

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        excelWorksheet.Cells(43, 8).Value = Log_Perf_ACStartTime.Value.ToString(Log_Perf_ACStartTime.Format, Nothing)
                        excelWorksheet.Cells(44, 8).Value = Log_Perf_DCStartTime.Value.ToString(Log_Perf_DCStartTime.Format, Nothing)
                        excelWorksheet.Cells(50, 8).Value = NUMBER_OF_STARTS.Value.ToString()
                        excelWorksheet.Cells(51, 8).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)
                        excelWorksheet.Cells(52, 8).Value = Log_Perf_NbApuFaults.Value.ToString()

                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(49, 8).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(49, 8).Value = "FAILED"
                        End If

                End Select

                'TestProgress_RecordPerfStep(TestStepNumber)
                'TestProgress_Save()

                'Write the manual Entry
                excelWorksheet = excelSheets.Item("ACCEPTANCE")

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


                excelWorksheet.Cells(53, 3).Value = UserInputsTHY.ChoiceOperator
                excelWorksheet.Cells(54, 3).Value = UserInputsTHY.ChoiceInspector

            Catch ex As Exception
                System.Diagnostics.Trace.WriteLine("GTCP131_9B_Sequence.CreateTHY13015PerformanceFile: " + ex.ToString())
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

        Private Sub ChinaWriteCommonDataForDetails1(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("DETAILS 1")

                excelWorksheet.Cells(20, Column).Value = LOG_PERF_GEN_SL.Value.ToString(LOG_PERF_GEN_SL.Format, Nothing)    'GENSL
                excelWorksheet.Cells(22, Column).Value = LOG_PERF_DELTA_P_SL.Value.ToString(LOG_PERF_DELTA_P_SL.Format, Nothing)
                excelWorksheet.Cells(24, Column).Value = LOG_PERF_PB_SL_PSIA.Value.ToString(LOG_PERF_PB_SL_PSIA.Format, Nothing)
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_DELPB.Value.ToString(LOG_PERF_DELPB.Format, Nothing)      'DELPB
                excelWorksheet.Cells(27, Column).Value = LOG_PERF_PB_INST_PSIA.Value.ToString(LOG_PERF_PB_INST_PSIA.Format, Nothing)
                excelWorksheet.Cells(28, Column).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)      'PBCOR
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_WB_SL.Value.ToString(LOG_PERF_WB_SL.Format, Nothing)
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_DELWB.Value.ToString(LOG_PERF_DELWB.Format, Nothing)      'DELWB
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_WB_INST.Value.ToString(LOG_PERF_WB_INST.Format, Nothing)
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)      'WFCOR
                excelWorksheet.Cells(36, Column).Value = LOG_PERF_DELTB.Value.ToString(LOG_PERF_DELTB.Format, Nothing)      'DELTB
                excelWorksheet.Cells(37, Column).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)      'TBCOR
                excelWorksheet.Cells(39, Column).Value = LOG_PERF_DELEGT.Value.ToString(LOG_PERF_DELEGT.Format, Nothing)    'DELEGT
                excelWorksheet.Cells(40, Column).Value = LOG_PERF_EGT_DP_CORR.Value.ToString(LOG_PERF_EGT_DP_CORR.Format, Nothing)
                excelWorksheet.Cells(42, Column).Value = LOG_PERF_EGT_INST.Value.ToString(LOG_PERF_EGT_INST.Format, Nothing)
                excelWorksheet.Cells(43, Column).Value = LOG_PERF_EGT_EXC_PB.Value.ToString(LOG_PERF_EGT_EXC_PB.Format, Nothing)
                excelWorksheet.Cells(45, Column).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)    'EGTCOR
                excelWorksheet.Cells(47, Column).Value = LOG_PERF_WF_SL.Value.ToString(LOG_PERF_WF_SL.Format, Nothing)
                excelWorksheet.Cells(49, Column).Value = LOG_PERF_DELWF.Value.ToString(LOG_PERF_DELWF.Format, Nothing)      'DELWF
                excelWorksheet.Cells(50, Column).Value = LOG_PERF_WF_DP_CORR.Value.ToString(LOG_PERF_WF_DP_CORR.Format, Nothing)
                excelWorksheet.Cells(52, Column).Value = LOG_PERF_WF_INST.Value.ToString(LOG_PERF_WF_INST.Format, Nothing)
                excelWorksheet.Cells(53, Column).Value = LOG_PERF_WF_EXC_PB.Value.ToString(LOG_PERF_WF_EXC_PB.Format, Nothing)
                excelWorksheet.Cells(55, Column).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)      'WFCOR

            Catch Ex As Exception
                MessageBox.Show("ChinaWriteCommonDataForDetails1: " & Ex.ToString())
            End Try
        End Sub

        Private Sub ChinaWriteCommonDataForDetails2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("DETAILS 2")

                excelWorksheet.Cells(20, Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing) 'PBAR
                excelWorksheet.Cells(21, Column).Value = LOG_PERF_P_BARO_PSIA.Value.ToString(LOG_PERF_P_BARO_PSIA.Format, Nothing) 'PCELL
                excelWorksheet.Cells(22, Column).Value = LOG_PERF_T_INLET_DEGF_AVG.Value.ToString(LOG_PERF_T_INLET_DEGF_AVG.Format, Nothing) 'T1
                excelWorksheet.Cells(23, Column).Value = LOG_PERF_ARINC_T2_DEGF.Value.ToString(LOG_PERF_ARINC_T2_DEGF.Format, Nothing) 'TENIVA
                excelWorksheet.Cells(24, Column).Value = LOG_PERF_P_OIL_PSIG.Value.ToString(LOG_PERF_P_OIL_PSIG.Format, Nothing) 'POIL
                excelWorksheet.Cells(25, Column).Value = LOG_PERF_T_OIL_DEGF.Value.ToString(LOG_PERF_T_OIL_DEGF.Format, Nothing) 'TOIL
                excelWorksheet.Cells(26, Column).Value = LOG_PERF_P_GEARBOX_INH2O.Value.ToString(LOG_PERF_P_GEARBOX_INH2O.Format, Nothing) 'PSGBX

                Dim Oil_F As Double = ARINC_OIL_T.Value.ToDouble * 9.0 / 5.0 + 32.0 'convert from De C to DegF
                excelWorksheet.Cells(27, Column).Value = Oil_F.ToString(ARINC_OIL_T.Format, Nothing) 'TOS

                excelWorksheet.Cells(28, Column).Value = LOG_PERF_T_FUEL_DEGF.Value.ToString(LOG_PERF_T_FUEL_DEGF.Format, Nothing) 'TFUEL
                excelWorksheet.Cells(29, Column).Value = LOG_PERF_P_FUEL_PSIG.Value.ToString(LOG_PERF_P_FUEL_PSIG.Format, Nothing) 'PFUEL
                excelWorksheet.Cells(30, Column).Value = LOG_PERF_VIB_GEARBOX.Value.ToString(LOG_PERF_VIB_GEARBOX.Format, Nothing) 'VIGGBA
                excelWorksheet.Cells(31, Column).Value = LOG_PERF_VIB_TURBINE.Value.ToString(LOG_PERF_VIB_TURBINE.Format, Nothing) 'VIBTHA
                excelWorksheet.Cells(32, Column).Value = LOG_PERF_VIB_FAN.Value.ToString(LOG_PERF_VIB_FAN.Format, Nothing) 'VIBTHB
                excelWorksheet.Cells(33, Column).Value = LOG_PERF_NG.Value.ToString(LOG_PERF_NG.Format, Nothing) 'XNL
                excelWorksheet.Cells(34, Column).Value = LOG_PERF_ARINC_IGV_POS.Value.ToString(LOG_PERF_ARINC_IGV_POS.Format, Nothing) 'PIGV
                excelWorksheet.Cells(35, Column).Value = LOG_PERF_P_COMP_DISCH_PSIA.Value.ToString(LOG_PERF_P_COMP_DISCH_PSIA.Format, Nothing) 'PCDFD
                excelWorksheet.Cells(36, Column).Value = LOG_PERF_T_TCD_DEGF.Value.ToString(LOG_PERF_T_TCD_DEGF.Format, Nothing) 'TCDFD
                excelWorksheet.Cells(37, Column).Value = LOG_PERF_T_EGT_P1_DEGF.Value.ToString(LOG_PERF_T_EGT_P1_DEGF.Format, Nothing) 'TTDEA
                excelWorksheet.Cells(38, Column).Value = LOG_PERF_T_EGT_P2_DEGF.Value.ToString(LOG_PERF_T_EGT_P2_DEGF.Format, Nothing) 'TTDEB
                excelWorksheet.Cells(39, Column).Value = LOG_PERF_T_EGT_DEGF_AVG.Value.ToString(LOG_PERF_T_EGT_DEGF_AVG.Format, Nothing) 'EGT
                excelWorksheet.Cells(40, Column).Value = LOG_PERF_P_EXHAUST_PSIA.Value.ToString(LOG_PERF_P_EXHAUST_PSIA.Format, Nothing) 'PS9
                excelWorksheet.Cells(48, Column).Value = LOG_PERF_WF_AVG_PPH.Value.ToString(LOG_PERF_WF_AVG_PPH.Format, Nothing) 'WF

            Catch Ex As Exception
                MessageBox.Show("ChinaWriteCommonDataForDetails2: " & Ex.ToString())
            End Try
        End Sub

        Private Sub ChinaWriteCommon2DataForDetails2(ByVal excelSheets As Excel.Sheets, ByVal Column As Integer)

            Try
                Dim excelWorksheet As Excel.Worksheet = excelSheets.Item("DETAILS 2")

                excelWorksheet.Cells(41, Column).Value = LOG_PERF_P_ORIFICE_PSIA.Value.ToString(LOG_PERF_P_ORIFICE_PSIA.Format, Nothing)      'PBORFA
                excelWorksheet.Cells(42, Column).Value = LOG_PERF_T_ORIFICE_DEGF.Value.ToString(LOG_PERF_T_ORIFICE_DEGF.Format, Nothing)      'TBORFA
                excelWorksheet.Cells(43, Column).Value = LOG_PERF_P_ORIFICE_DELTA_PSID.Value.ToString(LOG_PERF_P_ORIFICE_DELTA_PSID.Format, Nothing) 'PDBORA
                excelWorksheet.Cells(44, Column).Value = LOG_PERF_WB.Value.ToString(LOG_PERF_WB.Format, Nothing)                    'WB
                excelWorksheet.Cells(45, Column).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)            'WBCDNA
                excelWorksheet.Cells(46, Column).Value = LOG_PERF_P_BLEED1_5_PSIA.Value.ToString(LOG_PERF_P_BLEED1_5_PSIA.Format, Nothing)    'PB
                excelWorksheet.Cells(47, Column).Value = LOG_PERF_T_BLEED_AVG_DEGF.Value.ToString(LOG_PERF_T_BLEED_AVG_DEGF.Format, Nothing)  'TB
                excelWorksheet.Cells(49, Column).Value = LOG_PERF_GENERATOR_POWER_KW.Value.ToString(LOG_PERF_GENERATOR_POWER_KW.Format, Nothing) 'PWGEN

            Catch Ex As Exception
                MessageBox.Show("ChinaWriteCommon2DataForDetails2: " & Ex.ToString())
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

                    'APU Type: text directly in the excell sheet

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

                    Case eTestStepGTCP131_9B.TCOND1 'Ready To Load

                        ChinaWriteCommonDataForDetails2(excelSheets, 119) 'RTL

                    Case eTestStepGTCP131_9B.IGV_OFF

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' ECS OFFSET WORKSHEET section
                        excelWorksheet.Cells(36, 116).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)
                        excelWorksheet.Cells(37, 116).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)
                        excelWorksheet.Cells(38, 116).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)
                        excelWorksheet.Cells(39, 116).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)
                        excelWorksheet.Cells(40, 116).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND2

                        ChinaWriteCommonDataForDetails1(excelSheets, 81) 'ECS
                        ChinaWriteCommonDataForDetails2(excelSheets, 81) 'ECS
                        ChinaWriteCommon2DataForDetails2(excelSheets, 81) 'ECS

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        If (UserInputsChine.RepairType.ToUpper() = "HEAVY") Then
                            excelWorksheet.Cells(21, 53).NumberFormat = "@"
                            excelWorksheet.Cells(21, 53).Value = "51.2 MIN"
                            excelWorksheet.Cells(23, 53).NumberFormat = "@"
                            excelWorksheet.Cells(23, 53).Value = "155 MIN"
                            excelWorksheet.Cells(25, 53).NumberFormat = "@"
                            excelWorksheet.Cells(25, 53).Value = "445 MAX"
                            excelWorksheet.Cells(27, 53).NumberFormat = "@"
                            excelWorksheet.Cells(27, 53).Value = "1090 MAX."
                        Else
                            excelWorksheet.Cells(21, 53).NumberFormat = "@"
                            excelWorksheet.Cells(21, 53).Value = "51.2 MIN"
                            excelWorksheet.Cells(23, 53).NumberFormat = "@"
                            excelWorksheet.Cells(23, 53).Value = "155 MIN"
                            excelWorksheet.Cells(25, 53).NumberFormat = "@"
                            excelWorksheet.Cells(25, 53).Value = "445 MAX"
                            excelWorksheet.Cells(27, 53).NumberFormat = "@"
                            excelWorksheet.Cells(27, 53).Value = "1115 MAX"
                        End If

                        excelWorksheet.Cells(21, 74).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(23, 74).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(25, 74).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                        excelWorksheet.Cells(27, 74).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(29, 74).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND3

                        ChinaWriteCommonDataForDetails1(excelSheets, 100) 'MES
                        ChinaWriteCommonDataForDetails2(excelSheets, 100) 'MES
                        ChinaWriteCommon2DataForDetails2(excelSheets, 100) 'MES

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        If (UserInputsChine.RepairType.ToUpper() = "HEAVY") Then
                            excelWorksheet.Cells(21, 95).NumberFormat = "@"
                            excelWorksheet.Cells(21, 95).Value = "54.5 MIN."
                            excelWorksheet.Cells(25, 95).NumberFormat = "@"
                            excelWorksheet.Cells(25, 95).Value = "445 MAX"
                            excelWorksheet.Cells(27, 95).NumberFormat = "@"
                            excelWorksheet.Cells(27, 95).Value = "1080 MAX."
                        Else
                            excelWorksheet.Cells(21, 95).NumberFormat = "@"
                            excelWorksheet.Cells(21, 95).Value = "53.7 MIN."
                            excelWorksheet.Cells(25, 95).NumberFormat = "@"
                            excelWorksheet.Cells(25, 95).Value = "445 MAX"
                            excelWorksheet.Cells(27, 95).NumberFormat = "@"
                            excelWorksheet.Cells(27, 95).Value = "1105 MAX."
                        End If

                        excelWorksheet.Cells(21, 116).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        excelWorksheet.Cells(23, 116).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        excelWorksheet.Cells(25, 116).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                        excelWorksheet.Cells(27, 116).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        excelWorksheet.Cells(29, 116).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND4

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        excelWorksheet.Cells(44, 95).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(45, 95).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(44, 116).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND5
                        excelWorksheet = excelSheets.Item("SUMMARY")

                        excelWorksheet.Cells(46, 95).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        excelWorksheet.Cells(47, 95).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        excelWorksheet.Cells(46, 116).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                        excelWorksheet.Cells(48, 116).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND6

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                            excelWorksheet.Cells(51, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(51, 116).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                            excelWorksheet.Cells(52, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(52, 116).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND7

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycle96kW.Value) Then
                            excelWorksheet.Cells(55, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(55, 116).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(59, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(59, 116).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND8

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycleMES.Value) Then
                            excelWorksheet.Cells(56, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(56, 116).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(59, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(59, 116).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND9

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        If CELProject.IsTrue(Log_Perf_LoadCycleMES96kW.Value) Then
                            excelWorksheet.Cells(57, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(57, 116).Value = "FAILED"
                        End If

                        If CELProject.IsTrue(BLEED_BTN.Value) Then

                            If UserInputsChine.RepairType.ToUpper() = "HEAVY" Then
                                excelWorksheet.Cells(58, 95).Value = "MAX 1135 °F"
                            Else
                                excelWorksheet.Cells(58, 95).Value = "MAX 1160 °F"
                            End If

                            excelWorksheet.Cells(58, 116).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        End If


                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(59, 116).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(59, 116).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.FINAL_DATA

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        ' PERFORMED TEST DETAILS section
                        excelWorksheet = excelSheets.Item("SUMMARY")

                        excelWorksheet.Cells(53, 116).Value = Log_Perf_ACStartTime.Value.ToString(Log_Perf_ACStartTime.Format, Nothing)
                        excelWorksheet.Cells(54, 116).Value = Log_Perf_DCStartTime.Value.ToString(Log_Perf_DCStartTime.Format, Nothing)

                        excelWorksheet.Cells(60, 116).Value = NUMBER_OF_STARTS.Value.ToString()
                        excelWorksheet.Cells(61, 116).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)
                        excelWorksheet.Cells(62, 116).Value = Log_Perf_NbApuFaults.Value.ToString()

                End Select

                'Write the manual Entry
                excelWorksheet = excelSheets.Item("ACCEPTANCE")

                'Test Result
                If CELProject.IsTrue(CELProject.PERF_TEST_RESULT_ACCEPTED.Value) Then
                    excelWorksheet.CheckBoxes("chkTestResultAccepted").Value = True
                Else
                    excelWorksheet.CheckBoxes("chkTestResultAccepted").Value = False
                End If

                If CELProject.IsTrue(CELProject.PERF_TEST_RESULT_REJECTED.Value) Then
                    excelWorksheet.CheckBoxes("chkTestResultRejected").Value = True
                Else
                    excelWorksheet.CheckBoxes("chkTestResultRejected").Value = False
                End If

            Catch ex As Exception
                System.Diagnostics.Trace.WriteLine("GTCP131_9B_Sequence.CreatePerformanceFile: " + ex.ToString())
                MessageBox.Show(ex.ToString)
                CreateChina11813PerformanceFile = False
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
            Dim currentSheet As String
            Dim excelSheets As Excel.Sheets
            Dim excelWorksheet As Excel.Worksheet
            Dim TestCellName As String = "Unknown"
            Dim SelectString As String = String.Empty
            Dim Cmd As OleDbCommand
            Dim Reader As OleDbDataReader

            CreateStandardCELPerformanceFile = True

            ' The following gets the Worksheets collection
            excelSheets = excelWorkbook.Worksheets

            Try
                Dim test As Integer = excelWorkbook.Worksheets.Count

                excelWorkbook.Worksheets(1).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(2).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(3).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(4).Unprotect(UnProtectPassword)
                excelWorkbook.Worksheets(5).Unprotect(UnProtectPassword)

                If AppendOption = False Then

                    excelWorksheet = excelSheets.Item("RAW DATA")

                    'Eng S/N
                    excelWorksheet.Cells(5, 1).Value = "EngineConfiguration S/N"
                    excelWorksheet.Cells(5, 2).NumberFormat = "@"
                    excelWorksheet.Cells(5, 2).Value = UserInputsS7.SerialNumber

                    'Work Order
                    excelWorksheet.Cells(6, 1).Value = "Work order"
                    excelWorksheet.Cells(6, 2).NumberFormat = "@"
                    excelWorksheet.Cells(6, 2).Value = UserInputsS7.WorkOrder

                    'ECB S/N
                    excelWorksheet.Cells(7, 1).Value = "ECB S/N"
                    excelWorksheet.Cells(7, 2).NumberFormat = "@"
                    excelWorksheet.Cells(7, 2).Value = UserInputsS7.PartNumber

                    'LHV
                    excelWorksheet.Cells(8, 1).Value = "LHV"
                    excelWorksheet.Cells(8, 2).NumberFormat = "@"
                    excelWorksheet.Cells(8, 2).Value = UserInputsS7.LHV

                    'Sample fuel SG
                    excelWorksheet.Cells(9, 1).Value = "Fuel SG"
                    excelWorksheet.Cells(9, 2).NumberFormat = "@"
                    excelWorksheet.Cells(9, 2).Value = UserInputsS7.FuelSG

                    'Sample TFuel
                    excelWorksheet.Cells(10, 1).Value = "Fuel Temp"
                    excelWorksheet.Cells(10, 2).NumberFormat = "@"
                    excelWorksheet.Cells(10, 2).Value = UserInputsS7.TFuelDegF

                    'Oil Type
                    excelWorksheet.Cells(11, 1).Value = "Oil Type"
                    excelWorksheet.Cells(11, 2).NumberFormat = "@"
                    excelWorksheet.Cells(11, 2).Value = UserInputsS7.OilType

                    'Fuel Type
                    excelWorksheet.Cells(12, 1).Value = "Fuel Type"
                    excelWorksheet.Cells(12, 2).NumberFormat = "@"
                    excelWorksheet.Cells(12, 2).Value = UserInputsS7.FuelType

                    'Test Type
                    excelWorksheet.Cells(13, 1).Value = "Test Type"
                    excelWorksheet.Cells(13, 2).NumberFormat = "@"
                    excelWorksheet.Cells(13, 2).Value = UserInputsS7.ChoiceTestType

                    'Operator
                    excelWorksheet.Cells(14, 1).Value = "Operator"
                    excelWorksheet.Cells(14, 2).NumberFormat = "@"
                    excelWorksheet.Cells(14, 2).Value = UserInputsS7.ChoiceOperator

                    'Inspector
                    excelWorksheet.Cells(15, 1).Value = "Inspector"
                    excelWorksheet.Cells(15, 2).NumberFormat = "@"
                    excelWorksheet.Cells(15, 2).Value = UserInputsS7.ChoiceInspector

                    'Repair Type
                    excelWorksheet.Cells(16, 1).Value = "Repair Type"
                    excelWorksheet.Cells(16, 2).NumberFormat = "@"
                    excelWorksheet.Cells(16, 2).Value = UserInputsS7.ChoiceRepairType

                    'Test Date
                    excelWorksheet.Cells(17, 1).Value = "Test Date"
                    excelWorksheet.Cells(17, 2).NumberFormat = "@"
                    excelWorksheet.Cells(17, 2).Value = Format(Now, "d MMMM yyyy")

                    'Document Number
                    excelWorksheet = excelSheets.Item("ACCEPTANCE")
                    excelWorksheet.Cells(47, 1).NumberFormat = "@"
                    excelWorksheet.Cells(47, 1).Value = excelWorksheet.Cells(47, 1).Value.ToString() + Custom_Parameters("APUTestedAccordingTo").ToString()

                    'excelWorksheet = excelSheets.Item("RAW DATA")

                    ''Date
                    'excelWorksheet.Cells(1, 1).Value = "DATE"
                    'excelWorksheet.Cells(1, 2).NumberFormat = "@"
                    'excelWorksheet.Cells(1, 2).Name = "DATE"
                    'excelWorksheet.Cells(1, 2).Value = Format(Now, "d MMMM yyyy")

                    ''Document Number
                    'excelWorksheet.Cells(2, 1).Value = "TestedAccordingTo"
                    'excelWorksheet.Cells(2, 2).NumberFormat = "@"
                    'excelWorksheet.Cells(2, 2).Name = "TestedAccordingTo"
                    'excelWorksheet.Cells(2, 2).Value = Custom_Parameters("APUTestedAccordingTo").ToString()

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
                    'excelWorksheet = excelSheets.Item("SUMMARY")
                    'excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    'excelWorksheet = excelSheets.Item("DETAILS")
                    'excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    'excelWorksheet = excelSheets.Item("CALCULATIONS")
                    'excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                    'excelWorksheet = excelSheets.Item("ACCEPTANCE")
                    'excelWorksheet.PageSetup.RightFooter = excelWorksheet.PageSetup.RightFooter + TestCellName

                End If

                ' PERFORMED TEST DETAILS section
                excelWorksheet = excelSheets.Item("RAW DATA")

                'excelWorksheet.Cells(19, 1).Value = "AC START TIME"
                'excelWorksheet.Cells(19, 2).Value = Log_Perf_ACStartTime.Value.ToString(Log_Perf_ACStartTime.Format, Nothing)
                'excelWorksheet.Cells(20, 1).Value = "DC START TIME"
                'excelWorksheet.Cells(20, 2).Value = Log_Perf_DCStartTime.Value.ToString(Log_Perf_DCStartTime.Format, Nothing)
                'excelWorksheet.Cells(21, 1).Value = "NUMBER OF STARTS"
                'excelWorksheet.Cells(21, 2).Value = NUMBER_OF_STARTS.Value.ToString()
                'excelWorksheet.Cells(22, 1).Value = "NB APU FAULTS"
                'excelWorksheet.Cells(22, 2).Value = Log_Perf_NbApuFaults.Value.ToString()

                Dim ColumnName As String

                Select Case TestStepNumber

                    Case eTestStepGTCP131_9B.TCOND1

                        ColumnName = eTestStepGTCP131_9B.TCOND1.ToString

                        FillAllDatas(excelSheets, 1, ColumnName)

                    Case eTestStepGTCP131_9B.TCOND2

                        ColumnName = eTestStepGTCP131_9B.TCOND2.ToString

                        FillAllDatas(excelSheets, 2, ColumnName)

                        excelWorksheet = excelSheets.Item("SUMMARY")

                        If (HeavyRepairType) Then
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "51.2 MINIMUM"
                            excelWorksheet.Cells(18, 5).NumberFormat = "@"
                            excelWorksheet.Cells(18, 5).Value = "155 MINIMUM"
                            excelWorksheet.Cells(19, 5).NumberFormat = "@"
                            excelWorksheet.Cells(19, 5).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 5).NumberFormat = "@"
                            excelWorksheet.Cells(20, 5).Value = "1090 MAXiMUM"
                        Else
                            excelWorksheet.Cells(17, 5).NumberFormat = "@"
                            excelWorksheet.Cells(17, 5).Value = "51.2 MINIMUM"
                            excelWorksheet.Cells(18, 5).NumberFormat = "@"
                            excelWorksheet.Cells(18, 5).Value = "155 MINIMUM"
                            excelWorksheet.Cells(19, 5).NumberFormat = "@"
                            excelWorksheet.Cells(19, 5).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 5).NumberFormat = "@"
                            excelWorksheet.Cells(20, 5).Value = "1115 MAXIMUM"
                        End If

                        ' Performance Summary, Data point 0002, Actual
                        'excelWorksheet.Cells(17, 6).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        'excelWorksheet.Cells(18, 6).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        'excelWorksheet.Cells(19, 6).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                        'excelWorksheet.Cells(20, 6).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        'excelWorksheet.Cells(21, 6).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND3

                        ColumnName = eTestStepGTCP131_9B.TCOND3.ToString
                        FillAllDatas(excelSheets, 3, ColumnName)

                        ' PERFORMANCE SUMMARY section COND3
                        ' PBCOR MINIMUM AND EGTCOR MAXIMUM
                        excelWorksheet = excelSheets.Item("SUMMARY")

                        If (HeavyRepairType) Then
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "54.5 MINIMUM"
                            excelWorksheet.Cells(19, 7).NumberFormat = "@"
                            excelWorksheet.Cells(19, 7).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 7).NumberFormat = "@"
                            excelWorksheet.Cells(20, 7).Value = "1080 MAXIMUM"
                        Else
                            excelWorksheet.Cells(17, 7).NumberFormat = "@"
                            excelWorksheet.Cells(17, 7).Value = "53.7 MINIMUM"
                            excelWorksheet.Cells(19, 7).NumberFormat = "@"
                            excelWorksheet.Cells(19, 7).Value = "445 MAXIMUM"
                            excelWorksheet.Cells(20, 75).NumberFormat = "@"
                            excelWorksheet.Cells(20, 7).Value = "1105 MAXIMUM"
                        End If

                        ' MES + 65 KW COND3
                        'excelWorksheet.Cells(17, 8).Value = LOG_PERF_PBCOR.Value.ToString(LOG_PERF_PBCOR.Format, Nothing)
                        'excelWorksheet.Cells(18, 8).Value = LOG_PERF_WBCOR.Value.ToString(LOG_PERF_WBCOR.Format, Nothing)
                        'excelWorksheet.Cells(19, 8).Value = LOG_PERF_TBCOR.Value.ToString(LOG_PERF_TBCOR.Format, Nothing)
                        'excelWorksheet.Cells(20, 8).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing)
                        'excelWorksheet.Cells(21, 8).Value = LOG_PERF_WFCOR.Value.ToString(LOG_PERF_WFCOR.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND4

                        ColumnName = eTestStepGTCP131_9B.TCOND4.ToString
                        FillAllDatas(excelSheets, 4, ColumnName)

                        ' WBCDNA Flow sensor test
                        'excelWorksheet = excelSheets.Item("SUMMARY")
                        'excelWorksheet.Cells(34, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        'excelWorksheet.Cells(35, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        'excelWorksheet.Cells(34, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND5

                        ColumnName = eTestStepGTCP131_9B.TCOND5.ToString
                        FillAllDatas(excelSheets, 5, ColumnName)

                        ' WBCDNA flow sensor test and WC flow sensor accuracy
                        'excelWorksheet = excelSheets.Item("SUMMARY")
                        'excelWorksheet.Cells(36, 7).Value = LOG_PERF_WBCDNA_LO_LIM.Value.ToString(LOG_PERF_WBCDNA_LO_LIM.Format, Nothing)
                        'excelWorksheet.Cells(37, 7).Value = LOG_PERF_WBCDNA_HI_LIM.Value.ToString(LOG_PERF_WBCDNA_HI_LIM.Format, Nothing)
                        'excelWorksheet.Cells(36, 8).Value = LOG_PERF_WBCDNA.Value.ToString(LOG_PERF_WBCDNA.Format, Nothing)
                        'excelWorksheet.Cells(38, 8).Value = LOG_PERF_FLOW_SENSOR_ACC.Value.ToString(LOG_PERF_FLOW_SENSOR_ACC.Format, Nothing)

                    Case eTestStepGTCP131_9B.TCOND6

                        ColumnName = eTestStepGTCP131_9B.TCOND6.ToString
                        FillAllDatas(excelSheets, 6, ColumnName)

                        ' PERFORMED TEST DETAILS section
                        excelWorksheet = excelSheets.Item("RAW DATA")

                        excelWorksheet.Cells(62, 4).Value = "Log_Perf_SCVStability"
                        If CELProject.IsTrue(Log_Perf_SCVStability.Value) Then
                            excelWorksheet.Cells(62, 10).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(62, 10).Value = "FAILED"
                        End If

                        excelWorksheet.Cells(63, 4).Value = "Log_Perf_MinSurgeMargin"
                        If CELProject.IsTrue(Log_Perf_MinSurgeMargin.Value) Then
                            excelWorksheet.Cells(63, 10).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(63, 10).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND7

                        ColumnName = eTestStepGTCP131_9B.TCOND7.ToString
                        FillAllDatas(excelSheets, 7, ColumnName)

                        excelWorksheet = excelSheets.Item("RAW DATA")

                        ' PERFORMED TEST DETAILS section
                        excelWorksheet.Cells(64, 4).Value = "Log_Perf_LoadCycle96kW"
                        If CELProject.IsTrue(Log_Perf_LoadCycle96kW.Value) Then
                            excelWorksheet.Cells(64, 11).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(64, 11).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND8

                        ColumnName = eTestStepGTCP131_9B.TCOND8.ToString
                        FillAllDatas(excelSheets, 8, ColumnName)

                        excelWorksheet = excelSheets.Item("RAW DATA")

                        ' PERFORMED TEST DETAILS section
                        excelWorksheet.Cells(65, 4).Value = "Log_Perf_LoadCycleMES"
                        If CELProject.IsTrue(Log_Perf_LoadCycleMES.Value) Then
                            excelWorksheet.Cells(65, 12).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(65, 12).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.TCOND9

                        ColumnName = eTestStepGTCP131_9B.TCOND9.ToString
                        FillAllDatas(excelSheets, 9, ColumnName)

                        excelWorksheet = excelSheets.Item("RAW DATA")

                        ' PERFORMED TEST DETAILS section
                        excelWorksheet.Cells(66, 4).Value = "Log_Perf_LoadCycleMES96kW"
                        If CELProject.IsTrue(Log_Perf_LoadCycleMES96kW.Value) Then
                            excelWorksheet.Cells(66, 13).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(66, 13).Value = "FAILED"
                        End If

                        excelWorksheet = excelSheets.Item("SUMMARY")
                        If CELProject.IsTrue(BLEED_BTN.Value) Then
                            If UserInputsEPCOR.ChoiceRepairType = "Heavy" Then
                                excelWorksheet.Cells(48, 7).Value = "MAX 1135 F"
                            Else
                                excelWorksheet.Cells(48, 7).Value = "MAX 1160 F"
                            End If

                            excelWorksheet.Cells(48, 8).Value = LOG_PERF_EGTCOR.Value.ToString(LOG_PERF_EGTCOR.Format, Nothing) & " F"
                        End If

                    Case eTestStepGTCP131_9B.FINAL_DATA

                        ColumnName = eTestStepGTCP131_9B.FINAL_DATA.ToString
                        FillAllDatas(excelSheets, 10, ColumnName)

                        excelWorksheet = excelSheets.Item("RAW DATA")

                        ' PERFORMED TEST DETAILS section
                        excelWorksheet.Cells(67, 4).Value = "Log_Perf_ACStartTime"
                        excelWorksheet.Cells(67, 14).Value = Log_Perf_ACStartTime.Value.ToString(Log_Perf_ACStartTime.Format, Nothing)
                        excelWorksheet.Cells(68, 4).Value = "Log_Perf_DCStartTime"
                        excelWorksheet.Cells(68, 14).Value = Log_Perf_DCStartTime.Value.ToString(Log_Perf_DCStartTime.Format, Nothing)
                        excelWorksheet.Cells(69, 4).Value = "NUMBER_OF_STARTS"
                        excelWorksheet.Cells(69, 14).Value = NUMBER_OF_STARTS.Value.ToString()
                        excelWorksheet.Cells(70, 4).Value = "LOG_PERF_OPERATING_TIME_CURRENT_HOURS"
                        excelWorksheet.Cells(70, 14).Value = LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Value.ToString(LOG_PERF_OPERATING_TIME_CURRENT_HOURS.Format, Nothing)
                        excelWorksheet.Cells(71, 4).Value = "Log_Perf_NbApuFaults"
                        excelWorksheet.Cells(71, 14).Value = Log_Perf_NbApuFaults.Value.ToString()

                        excelWorksheet.Cells(72, 4).Value = "Log_Perf_LoadCycleRTL"
                        If CELProject.IsTrue(Log_Perf_LoadCycleRTL.Value) Then
                            excelWorksheet.Cells(72, 14).Value = "PASSED"
                        Else
                            excelWorksheet.Cells(72, 14).Value = "FAILED"
                        End If

                    Case eTestStepGTCP131_9B.IGV_OFF

                        ColumnName = eTestStepGTCP131_9B.IGV_OFF.ToString
                        FillAllDatas(excelSheets, 11, ColumnName)

                        excelWorksheet = excelSheets.Item("RAW DATA")

                        ' ECS OFFSET WORKSHEET section
                        excelWorksheet.Cells(54, 4).Value = "Log_Perf_InitPBCOR"
                        excelWorksheet.Cells(54, 15).Value = Log_Perf_InitPBCOR.Value.ToString(Log_Perf_InitPBCOR.Format, Nothing)
                        excelWorksheet.Cells(55, 4).Value = "Log_Perf_FinalPBCOR"
                        excelWorksheet.Cells(55, 15).Value = Log_Perf_FinalPBCOR.Value.ToString(Log_Perf_FinalPBCOR.Format, Nothing)
                        excelWorksheet.Cells(56, 4).Value = "Log_Perf_InitIGV"
                        excelWorksheet.Cells(56, 15).Value = Log_Perf_InitIGV.Value.ToString(Log_Perf_InitIGV.Format, Nothing)
                        excelWorksheet.Cells(57, 4).Value = "Log_Perf_FinalIGV"
                        excelWorksheet.Cells(57, 15).Value = Log_Perf_FinalIGV.Value.ToString(Log_Perf_FinalIGV.Format, Nothing)
                        excelWorksheet.Cells(58, 4).Value = "LOG_PERF_ECS_OFFSET"
                        excelWorksheet.Cells(58, 15).Value = LOG_PERF_ECS_OFFSET.Value.ToString(LOG_PERF_ECS_OFFSET.Format, Nothing)

                End Select

                'Write the manual Entry
                excelWorksheet = excelSheets.Item("ACCEPTANCE")

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

                excelWorksheet.Cells(53, 3).Value = UserInputsTHY.ChoiceOperator
                excelWorksheet.Cells(54, 3).Value = UserInputsTHY.ChoiceInspector

                excelWorksheet = excelSheets.Item("RAW DATA")
                excelWorksheet.Visible = Excel.XlSheetVisibility.xlSheetHidden

            Catch ex As Exception
                System.Diagnostics.Trace.WriteLine("GTCP131_9B_Sequence.CreatePerformanceFile: " + ex.ToString())
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

            Dim rs As New ADODB.Recordset
            Dim i As Integer
            Dim SelectString As String

            Try

                'Read Table 1312 GTCP131-9B (page 1344)
                SelectString = "SELECT * FROM GTCP131_9B_MES ORDER BY [TAMB]"
                If DatabaseHelper.OpenRecordset(rs, SelectString, cnMain, True) Then
                    ReDim GTCP131_9B_MES(DatabaseHelper.RecordCount(rs) - 1, 5)
                    For i = 0 To GTCP131_9B_MES.GetLength(0) - 1
                        GTCP131_9B_MES(i, 0) = rs.Fields("TAMB").Value
                        GTCP131_9B_MES(i, 1) = rs.Fields("WB_DELTA").Value
                        GTCP131_9B_MES(i, 2) = rs.Fields("PB_DELTA").Value
                        GTCP131_9B_MES(i, 3) = rs.Fields("TB_DELTA").Value
                        GTCP131_9B_MES(i, 4) = rs.Fields("EGT_DELTA").Value
                        GTCP131_9B_MES(i, 5) = rs.Fields("WF_DELTA").Value
                        rs.MoveNext()
                    Next i
                    DatabaseHelper.CloseRecordset(rs)
                End If

                'Read Table 1313 GTCP131-9B
                SelectString = "SELECT * FROM GTCP131_9B_ECS ORDER BY [TAMB]"
                If DatabaseHelper.OpenRecordset(rs, SelectString, cnMain, True) Then
                    ReDim GTCP131_9B_ECS(DatabaseHelper.RecordCount(rs) - 1, 5)
                    For i = 0 To GTCP131_9B_ECS.GetLength(0) - 1
                        GTCP131_9B_ECS(i, 0) = rs.Fields("TAMB").Value
                        GTCP131_9B_ECS(i, 1) = rs.Fields("WB_DELTA").Value
                        GTCP131_9B_ECS(i, 2) = rs.Fields("PB_DELTA").Value
                        GTCP131_9B_ECS(i, 3) = rs.Fields("TB_DELTA").Value
                        GTCP131_9B_ECS(i, 4) = rs.Fields("EGT_DELTA").Value
                        GTCP131_9B_ECS(i, 5) = rs.Fields("WF_DELTA").Value
                        rs.MoveNext()
                    Next i
                    DatabaseHelper.CloseRecordset(rs)
                End If

                'Read Figure 1303 GTCP131-9B
                SelectString = "SELECT * FROM GTCP131_9B_WBCDNA_LIMITS ORDER BY [T_INLET_AVG]"
                If DatabaseHelper.OpenRecordset(rs, SelectString, cnMain, True) Then
                    ReDim GTCP131_9B_WBCDNA_LIMITS(DatabaseHelper.RecordCount(rs) - 1, 2)
                    For i = 0 To GTCP131_9B_WBCDNA_LIMITS.GetLength(0) - 1
                        GTCP131_9B_WBCDNA_LIMITS(i, 0) = rs.Fields("T_INLET_AVG").Value
                        GTCP131_9B_WBCDNA_LIMITS(i, 1) = rs.Fields("LOW_LIMIT").Value
                        GTCP131_9B_WBCDNA_LIMITS(i, 2) = rs.Fields("HIGH_LIMIT").Value
                        rs.MoveNext()
                    Next i
                    DatabaseHelper.CloseRecordset(rs)
                End If

                'Read the Arinc Fault Code and equivalent Text
                Dim FaultCodeCollection As New Dacn1Arinc429FaultCodeCollection()

                FaultCodeCollection.Query.Where(FaultCodeCollection.Query.CollectionName = CELProject.EngineModel)
                FaultCodeCollection.Query.Load()

                i = 0
                ReDim ArincFaultDefinition(FaultCodeCollection.Count - 1)

                For Each Fault As Dacn1Arinc429FaultCode In FaultCodeCollection


                    ArincFaultDefinition(i).FaultCode = Fault.FaultCode
                    ArincFaultDefinition(i).FaultText = Fault.FaultText

                    i = i + 1

                Next

            Catch Ex As Exception
                MessageBox.Show("InitArrays_GTCP131_9B: " & Ex.ToString())
            End Try
        End Sub

        Protected Overrides Sub Init_Engine_Channels_Value()

            Try

                'Read the mass flow meter value
                FM_INITIAL_VOLUME_LITERS.Value = New KineticaRT.Values.Double(FM_TOTAL_VOLUME_LITERS.Value.ToDouble())

                CELProject.PERF_TEST_STEP_NB.Value = Values.Zero

                Select Case CELProject.Project.Number
                    Case 11813
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsChine.LHV)

                    Case 12958
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsEPCOR.LHV)
                        LOAD_AIR_VALVE_DEMAND.Value = New KineticaRT.Values.Double(15.0)

                    Case 11825
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsEthiopie.LHV)
                        LOAD_AIR_VALVE_3INCH_DEMAND.Value = New KineticaRT.Values.Double(15.0)
                        LOAD_AIR_VALVE_8INCH_DEMAND.Value = New KineticaRT.Values.Double(15.0)

                    Case 13015
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsTHY.LHV)
                        LOAD_AIR_VALVE_DEMAND.Value = New KineticaRT.Values.Double(100.0)

                    Case 17865
                        FLHV.Value = New KineticaRT.Values.Double(UserInputsS7.LHV)
                        LOAD_AIR_VALVE_DEMAND.Value = New KineticaRT.Values.Double(100.0)

                    Case Else
                        Debug.Assert(False, "Init_Engine_Channels_Value: Code Missign for this project")

                End Select

            Catch ex As Exception
                MessageBox.Show("Init_Engine_Channels_Value: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                Select Case CELProject.Project.Number
                    '* FOR Ethiopia project, customer required a dedicated space to sign the log report. 

                    Case 11825
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = "Operator Signature:"
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Font.Bold = True
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
                        RowIndex += 1
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = "Inspector Signature:"
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Font.Bold = True
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
                        RowIndex += 2

                End Select

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

                Select Case CELProject.Project.Number

                    Case 11825 '* FOR Ethiopia project, customer required a dedicated space to sign the log report. It is an offset requiring 3 spaces. 

                        RowIndex = RowIndex + 3

                End Select

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

            EmulateAPU_Fuel_SOV()

            'Make sure FM_INITIAL_VOLUME_LITERS is different than 0
            ValidateFMInitialValue()

            'Persistent Values Save Check
            PersistentValuesCtrl.SavePersistentValues()

            LightOffTimerCtrl.Timer()
            StartTimerCtrl.Timer()
            StartTimerACCtrl.Timer()
            StartTimerDCCtrl.Timer()

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

            Catch ex As Exception
                MessageBox.Show("GTCP131_9B_Sequence.Compute_BleedAirFlow: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Double.NaN
            End Try

        End Function

        Private Sub OnAcceleratingTime_Calc(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                If WF_AVG_PPH.Value > 5.0 Then
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

                If sender.Name = "Log_Perf_CaptureACStartTime" Then
                    Log_Perf_ACStartTime.Value = STARTING_AC_TIME_SEC.Value

                ElseIf sender.Name = "Log_Perf_CaptureDCStartTime" Then
                    Log_Perf_DCStartTime.Value = STARTING_DC_TIME_SEC.Value

                ElseIf sender.Name = "Log_Perf_CaptureFIGV" Then
                    Log_Perf_FinalIGV.Value = ARINC_IGV_POS.Value

                ElseIf sender.Name = "Log_Perf_CaptureFPBCOR" Then
                    Log_Perf_FinalPBCOR.Value = PBCOR_MEAN.Value

                ElseIf sender.Name = "Log_Perf_CaptureIIGV" Then
                    Log_Perf_InitIGV.Value = ARINC_IGV_POS.Value

                ElseIf sender.Name = "Log_Perf_CaptureIPBCOR" Then
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

        Private Sub OnFaultCode(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                Dim i As Integer
                Dim j As Integer
                Dim FaultString = Nothing
                Dim FaultFound(5) As Boolean  ' we use item 1 to 5 and don't use item 0

                For j = 1 To A429IN_Fault.GetLength(0) - 1 ' we use item 1 to 5 and don't use item 0
                    FaultFound(j) = False
                Next j


                For i = 0 To ArincFaultDefinition.GetLength(0) - 1

                    For j = 1 To A429IN_Fault.GetLength(0) - 1 ' we use item 1 to 5 and don't use item 0

                        If A429IN_Fault(j).Value.ToDouble() = ArincFaultDefinition(i).FaultCode Then     'Find the text equivalent to the fault code
                            FAULT_TEXT(j).Value = New KineticaRT.Values.Text(ArincFaultDefinition(i).FaultText)
                            FaultFound(j) = True
                        End If

                    Next j
                Next i

                For j = 1 To FaultFound.GetLength(0) - 1 ' we use item 1 to 5 and don't use item 0

                    If FaultFound(j) = False Then
                        FAULT_TEXT(j).Value = New KineticaRT.Values.Text("Fault Code Not Defined in test session")
                    End If
                Next j

            Catch Ex As Exception
                MessageBox.Show("OnFaultCode: " & Ex.ToString())
            End Try
        End Sub

        Private Sub OnUSING_HONEYWELL_COMPUTER(ByVal sender As IChannel, ByVal newVal As Value)
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

                CELProject.DoLogging(AddressOf UpdateCustomHeaderLogFile, False, "GTCP131-9B", True)
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
                Log_Perf_MinSurgeMargin.Value = Values.Zero
                Log_Perf_SCVStability.Value = Values.Zero
                Log_Perf_LoadCycle96kW.Value = Values.Zero
                Log_Perf_LoadCycleMES.Value = Values.Zero
                Log_Perf_LoadCycleMES96kW.Value = Values.Zero
                Log_Perf_LoadCycleRTL.Value = Values.Zero
                Log_Perf_InitPBCOR.Value = Values.Zero
                Log_Perf_InitIGV.Value = Values.Zero
                Log_Perf_FinalPBCOR.Value = Values.Zero
                Log_Perf_FinalIGV.Value = Values.Zero
                Log_Perf_NbApuFaults.Value = Values.Zero
                Log_Perf_ACStartTime.Value = Values.Zero
                Log_Perf_DCStartTime.Value = Values.Zero
            End If
        End Sub

        Protected Overrides Sub OnPERF_CLOSE_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)

            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then    ' We are playing the archive so don't perform any action
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

                    Case 12958 ' EPCOR update (from 05306, 07553)
                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateEPCOR12958PerformanceFile)

                    Case 11825 ' Ethiopie update (from 12958)
                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateEthiopie11825PerformanceFile)

                    Case 13015 ' Turkey THY
                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateTHY13015PerformanceFile)

                    Case 17865 ' S7
                        NewFileCreated = CELProject.ProcessPerformanceReport(SearchPath, PerfTemplateFullFileName, AddressOf CreateStandardCELPerformanceFile)

                    Case Else
                        Debug.Assert(False, "OnPerf_TAKE_POINT_BTN, Code Missign for this project")
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
                ' Determine if you perform the action or not (no action when in archive playback)
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                ' Perform the Test Result accepted action

                CELProject.PERF_TEST_RESULT_REJECTED.Value = Values.False

            Catch Ex As Exception
                MessageBox.Show("OnPERFTestResultAcceptedCheckBox: " & Ex.ToString())
            End Try
        End Sub

        Private Sub OnPERFTestResultRejectedCheckBox(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                ' Determine if you perform the action or not (no action when in archive playback)
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If CELProject.IsFalse(newVal) Then
                    Return
                End If

                ' Perform the action
                CELProject.PERF_TEST_RESULT_ACCEPTED.Value = Values.False

            Catch Ex As Exception
                MessageBox.Show("OnPERFTestResultRejectedCheckBox: " & Ex.ToString())
            End Try
        End Sub

        Private Sub OnWinDMMButton(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Return
            End If

            Dim proc As New System.Diagnostics.Process()
            Dim VarInfo As New System.Diagnostics.ProcessStartInfo()

            VarInfo.FileName = EngineConfiguration.WinDMMUtilityExeFile
            VarInfo.WorkingDirectory = "c:\Honeywell\WinDMM"
            VarInfo.UseShellExecute = False
            proc.StartInfo = VarInfo


            If (CELProject.IsTrue(newVal)) Then

                Try

                    If File.Exists(EngineConfiguration.WinDMMUtilityExeFile) Then
                        proc.Start()
                    Else
                        Debug.Assert(False, "The file " + EngineConfiguration.WinDMMUtilityExeFile + " does not exist")
                    End If

                Catch ex As System.Exception
                    MessageBox.Show("WinDMM Button: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                WINDMM_BTN.Value = Values.False

            End If

        End Sub

        Private Sub OnTCV_CLOSE_MES_OFF_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If (CELProject.IsFalse(newVal)) Then
                    Return
                End If

                MES_BTN.Value = Values.False
                LOAD_AIR_VALVE_DEMAND.Value = New Global.KineticaRT.Values.Double(0)

            Catch ex As System.Exception
                MessageBox.Show("OnTCV_CLOSE_MES_OFF_BTN: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

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
                T_EGT_AVG_CURRENT_DEGF.Value = T_EGT_DEGF_AVG.Value
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

        Private Sub OnLOAD_AIR_VALVES_CLOSE(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If (CELProject.IsTrue(newVal)) Then
                    LOAD_AIR_VALVE_3INCH_DEMAND.Value = Values.Zero
                    LOAD_AIR_VALVE_8INCH_DEMAND.Value = Values.Zero
                End If

            Catch ex As Exception
                MessageBox.Show("OnLOAD_AIR_VALVES_CLOSE: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnLOAD_AIR_VALVES_OPEN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If (CELProject.IsTrue(newVal)) Then
                    LOAD_AIR_VALVE_3INCH_DEMAND.Value = New Global.KineticaRT.Values.Double(100)
                    LOAD_AIR_VALVE_8INCH_DEMAND.Value = New Global.KineticaRT.Values.Double(100)
                End If

            Catch ex As Exception
                MessageBox.Show("OnLOAD_AIR_VALVES_CLOSE: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnQUICK_LOAD_AIR_VALVE_OPEN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Return
                End If

                If (CELProject.IsFalse(newVal)) Then
                    Return
                End If

                Select Case CELProject.Project.Number

                    Case 13015
                        LOAD_AIR_VALVE_DEMAND.Value = New Global.KineticaRT.Values.Double(100)

                    Case 17865
                        LOAD_AIR_VALVE_DEMAND.Value = New Global.KineticaRT.Values.Double(100)

                End Select

            Catch ex As Exception
                MessageBox.Show("OnQUICK_LOAD_AIR_VALVE_OPEN: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

                If CELProject.EngineModel <> "GTCP131-9B" Then
                    Return
                End If

                If Not (FlowmeterCtrl Is Nothing) Then
                    FlowmeterCtrl.Dispose()
                End If

                'S'assure de bien killer le thread de flowmeter
                'Close the connection to the database
                DatabaseHelper.CloseConnection(cnMain)

                'If TestProgressMgr IsNot Nothing Then
                '    TestProgress_Save()
                '    TestProgressMgr.Dispose()
                '    TestProgressMgr = Nothing
                'End If


            Catch ex As Exception
                MessageBox.Show("OnTestSessionClosing:" & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub OnTestSessionInitialized(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Try
                MyBase.OnTestSessionInitialized(newStatus)

                If CELProject.EngineModel <> "GTCP131-9B" Then
                    Return
                End If


                Select Case CELProject.Project.Number

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

                    Case 11825
                        UserInputsEthiopie.MapUserInputsParameters(CELProject)
                        If UserInputsEthiopie.ChoiceRepairType.ToUpper() = "HEAVY" Then
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

                MessageBox.Show("GTCP131_9B_Sequence.OnTestSessionOpened: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try
        End Sub

        Protected Overrides Sub OnTestSessionRunning(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)

            Try
                MyBase.OnTestSessionRunning(newStatus)

                If CELProject.EngineModel <> "GTCP131-9B" Then
                    Return
                End If

                CELProject.LoadSave_Custom_Parameters(Custom_Parameters)

                Init_Engine_Channels()
                Init_Arrays()
                CELProject.EngineWindowCollection.Item(0).EngineWindow.BeginInvoke(New MethodInvoker(AddressOf Me.Init_Engine_Controls))
                Init_Engine_Channels_Value()

                CELProject.Perf_Nb_Sheet_To_Print = 4

                Init_Engine_Controllers()

                CELProject.EngineWindowCollection.Item(0).EngineWindow.BeginInvoke(New MethodInvoker(AddressOf Me.Init_Engine_Controls))

                AddHandlers()

                MyBase.StartWorkingThread()

                'Test Progress Initialization (first in EPCOR)
                Select Case CELProject.Project.Number
                    Case 12958
                        'TestProgress_Initialize()
                    Case 11825
                        'TestProgress_Initialize()
                    Case 13015
                        'TestProgress_Initialize()
                    Case 11813
                        ' Do nothing
                    Case 17865
                        'TestProgress_Initialize()
                    Case Else
                        Debug.Assert(False, "OnTestSessionRunning: Test progress not configured for this project")
                End Select

            Catch ex As Exception
                MessageBox.Show("OnTestSessionRunning:" & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Protected Overrides Sub AddHandlers()

            'Add Handlers
            'Initialise Common Engine Channels
            For i As Integer = 1 To A429IN_Fault.GetLength(0) - 1  ' we use item 1 to 5 and don't use item 0
                AddHandler A429IN_Fault(i).ValueChanged, AddressOf OnFaultCode
            Next i

            AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN
            AddHandler Log_Perf_CaptureACStartTime.ValueChanged, AddressOf OnCapture
            AddHandler Log_Perf_CaptureDCStartTime.ValueChanged, AddressOf OnCapture
            AddHandler USING_HONEYWELL_COMPUTER.ValueChanged, AddressOf OnUSING_HONEYWELL_COMPUTER
            AddHandler Log_Perf_CaptureFIGV.ValueChanged, AddressOf OnCapture
            AddHandler Log_Perf_CaptureFPBCOR.ValueChanged, AddressOf OnCapture
            AddHandler Log_Perf_CaptureIIGV.ValueChanged, AddressOf OnCapture
            AddHandler Log_Perf_CaptureIPBCOR.ValueChanged, AddressOf OnCapture
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
            AddHandler TCV_CLOSE_MES_OFF_BTN.ValueChanged, AddressOf OnTCV_CLOSE_MES_OFF_BTN
            AddHandler ENGINE_AT_REST_IND.ValueChanged, AddressOf OnENGINE_STOP

            AddHandler LOAD_AIR_VALVES_CLOSE_BTN.ValueChanged, AddressOf OnLOAD_AIR_VALVES_CLOSE
            AddHandler LOAD_AIR_VALVES_OPEN_BTN.ValueChanged, AddressOf OnLOAD_AIR_VALVES_OPEN

            AddHandler QUICK_LOAD_AIR_VALVE_OPEN_BTN.ValueChanged, AddressOf OnQUICK_LOAD_AIR_VALVE_OPEN

        End Sub
        Protected Overrides Sub RemoveHandlers()

            For i As Integer = 1 To A429IN_Fault.GetLength(0) - 1  ' we use item 1 to 5 and don't use item 0
                If Not (A429IN_Fault(i) Is Nothing) Then
                    RemoveHandler A429IN_Fault(i).ValueChanged, AddressOf OnFaultCode
                End If
            Next i

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN
            End If

            If Not (USING_HONEYWELL_COMPUTER Is Nothing) Then
                RemoveHandler USING_HONEYWELL_COMPUTER.ValueChanged, AddressOf OnUSING_HONEYWELL_COMPUTER
            End If
            If Not (PERF_NEW_TEST_OPC Is Nothing) Then
                RemoveHandler PERF_NEW_TEST_OPC.ValueChanged, AddressOf OnNEW_TEST_BTN
            End If

            If Not (CELProject.LOG_TAKE_POINT_BTN Is Nothing) Then
                RemoveHandler CELProject.LOG_TAKE_POINT_BTN.ValueChanged, AddressOf OnLOG_TAKE_POINT_BTN
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

            If Not (READY_TO_LOAD_IND Is Nothing) Then
                RemoveHandler READY_TO_LOAD_IND.ValueChanged, AddressOf OnAcceleratingTime_Calc
            End If

            If Not (CELProject.PERF_TEST_RESULT_ACCEPTED Is Nothing) Then
                RemoveHandler CELProject.PERF_TEST_RESULT_ACCEPTED.ValueChanged, AddressOf OnPERFTestResultAcceptedCheckBox
            End If

            If Not (CELProject.PERF_TEST_RESULT_REJECTED Is Nothing) Then
                RemoveHandler CELProject.PERF_TEST_RESULT_REJECTED.ValueChanged, AddressOf OnPERFTestResultRejectedCheckBox
            End If

            If Not (WINDMM_BTN Is Nothing) Then
                RemoveHandler WINDMM_BTN.ValueChanged, AddressOf OnWinDMMButton
            End If

            If Not (START_BTN Is Nothing) Then
                RemoveHandler START_BTN.ValueChanged, AddressOf OnENGINE_START
            End If

            If Not (TCV_CLOSE_MES_OFF_BTN Is Nothing) Then
                RemoveHandler TCV_CLOSE_MES_OFF_BTN.ValueChanged, AddressOf OnTCV_CLOSE_MES_OFF_BTN
            End If

            If Not (ENGINE_AT_REST_IND Is Nothing) Then
                RemoveHandler ENGINE_AT_REST_IND.ValueChanged, AddressOf OnENGINE_STOP
            End If

            If Not (TestStep_List Is Nothing) Then
                RemoveHandler TestStep_List.SelectedIndexChanged, AddressOf OnTestStep_List_SelectedIndexChanged
            End If

            If Not (Log_Perf_CaptureACStartTime Is Nothing) Then
                RemoveHandler Log_Perf_CaptureACStartTime.ValueChanged, AddressOf OnCapture
            End If

            If Not (Log_Perf_CaptureDCStartTime Is Nothing) Then
                RemoveHandler Log_Perf_CaptureDCStartTime.ValueChanged, AddressOf OnCapture
            End If

            If Not (Log_Perf_CaptureFIGV Is Nothing) Then
                RemoveHandler Log_Perf_CaptureFIGV.ValueChanged, AddressOf OnCapture
            End If

            If Not (Log_Perf_CaptureFPBCOR Is Nothing) Then
                RemoveHandler Log_Perf_CaptureFPBCOR.ValueChanged, AddressOf OnCapture
            End If

            If Not (Log_Perf_CaptureIIGV Is Nothing) Then
                RemoveHandler Log_Perf_CaptureIIGV.ValueChanged, AddressOf OnCapture
            End If

            If Not (Log_Perf_CaptureIPBCOR Is Nothing) Then
                RemoveHandler Log_Perf_CaptureIPBCOR.ValueChanged, AddressOf OnCapture
            End If

            If Not (LOAD_AIR_VALVES_CLOSE_BTN Is Nothing) Then
                RemoveHandler LOAD_AIR_VALVES_CLOSE_BTN.ValueChanged, AddressOf OnLOAD_AIR_VALVES_CLOSE
            End If

            If Not (LOAD_AIR_VALVES_OPEN_BTN Is Nothing) Then
                RemoveHandler LOAD_AIR_VALVES_OPEN_BTN.ValueChanged, AddressOf OnLOAD_AIR_VALVES_OPEN
            End If

            If Not (QUICK_LOAD_AIR_VALVE_OPEN_BTN Is Nothing) Then
                RemoveHandler QUICK_LOAD_AIR_VALVE_OPEN_BTN.ValueChanged, AddressOf OnQUICK_LOAD_AIR_VALVE_OPEN
            End If

        End Sub

        Protected Overrides Sub Init_Engine_Controllers()

            Dim PersistentValuesChannels() As String = {"NUMBER_OF_STARTS",
                                                              "OPERATING_TIME_CURRENT_SEC",
                                                              "FUEL_USED_INITIAL_LITERS",
                                                              "FUEL_USED_TOTAL_LITERS",
                                                              "PERF_TEST_RESULT_ACCEPTED",
                                                              "PERF_TEST_RESULT_REJECTED",
                                                              "Log_Perf_SCVStability",
                                                              "Log_Perf_MinSurgeMargin",
                                                              "Log_Perf_LoadCycle96kW",
                                                              "Log_Perf_LoadCycleMES",
                                                              "Log_Perf_LoadCycleMES96kW",
                                                              "Log_Perf_LoadCycleRTL",
                                                              "Log_Perf_InitPBCOR",
                                                              "Log_Perf_FinalPBCOR",
                                                              "Log_Perf_InitIGV",
                                                              "Log_Perf_FinalIGV",
                                                              "Log_Perf_NbApuFaults"}

            Dim EngineSN As String = Nothing
            Dim WorkOrder As String = Nothing

            Select Case CELProject.Project.Number

                Case 11813
                    EngineSN = UserInputsChine.EngineSerialNumber
                    WorkOrder = UserInputsChine.WorkOrder

                Case 12958
                    EngineSN = UserInputsEPCOR.SerialNumber
                    WorkOrder = UserInputsEPCOR.WorkOrder

                Case 11825
                    EngineSN = UserInputsEthiopie.SerialNumber
                    WorkOrder = UserInputsEthiopie.WorkOrder

                Case 13015
                    EngineSN = UserInputsTHY.SerialNumber
                    WorkOrder = UserInputsTHY.WorkOrder

                Case 17865
                    EngineSN = UserInputsS7.SerialNumber
                    WorkOrder = UserInputsS7.WorkOrder

                Case Else
                    Debug.Assert(False, "OnTestSessionRunning: Code Missign for this project")

            End Select

            Dim PersistentValuesFileName As String = "C:\CELDAS DATA\" & CELProject.EngineModel & "\" & EngineSN & "\" & WorkOrder & "\Log\PersistentValues.xml"
            PersistentValuesCtrl = New PersistentValuesController(Me, PersistentValuesChannels, 1, PersistentValuesFileName)

            If Double.IsNaN(FUEL_USED_TOTAL_LITERS.Value.ToDouble()) Then
                FUEL_USED_INITIAL_LITERS.Value = Values.Zero
            Else
                FUEL_USED_INITIAL_LITERS.Value = New KineticaRT.Values.Double(FUEL_USED_TOTAL_LITERS.Value.ToDouble())
            End If

            ' Default value if not overridden by the project
            Dim RealNumberOfTBleedProbe As Integer = MaxNumberOfTBleedProbe
            Dim RealNumberOfTEgtProbe As Integer = MaxNumberOfTEgtProbe
            Dim RealNumberOfTInletProbe As Integer = MaxNumberOfTInletProbe

            DeletableTBleedChCtrl = New DeletableChannelController(Me, RealNumberOfTBleedProbe, MaxNumberOfTBleedProbe, MaxNumberOfTBleedProbe, "T_BLEED", "_DEGF", "_DELETE_BTN", "T_BLEED_AVG_DEGF", "T_BLEED_MIN_DEGF", "T_BLEED_MAX_DEGF", "T_BLEED_SPREAD_DEGF")
            DeletableTEgtChCtrl = New DeletableChannelController(Me, RealNumberOfTEgtProbe, MaxNumberOfTEgtProbe, MaxNumberOfTEgtProbe, "T_EGT", "_DEGF", "_DELETE_BTN", "T_EGT_DEGF_AVG", "T_EGT_DEGF_MIN", "T_EGT_DEGF_MAX", "T_EGT_DEGF_SPREAD")
            DeletableTInletChCtrl = New DeletableChannelController(Me, RealNumberOfTInletProbe, MaxNumberOfTInletProbe, MaxNumberOfTInletProbe, "T_INLET", "_DEGF", "_DELETE_BTN", "T_INLET_DEGF_AVG", "T_INLET_DEGF_MIN", "T_INLET_DEGF_MAX", "T_INLET_DEGF_SPREAD")

            'Configuring ECS Control
            ECSCtrl = New ECSController(Me, 0, 100, 0, "ECS_BTN", "ECS_DEMAND", "ECS_UP_DEMAND", "ECS_DOWN_DEMAND", ECS_Display)
            ECSCtrl.Memorize = False
            ECSCtrl.Enable = False
            ECSCtrl.UseIGVPositionAsFirstValue = True
            ECSCtrl.IGVpositionChannel = ARINC_IGV_POS

            FlowmeterCtrl = New FlowmeterController(Me)

            StarterDCMaxCurrentRecordCtrl = New MinMaxRecordController(Me, MinMaxRecordController.eRecordType.Maximum, "STARTER_RECTIFIER_CURRENT_AMP", "STARTER_RECTIFIER_CURRENT_AMP_MAX", "START_BTN")
            StarterDCMaxVoltageRecordCtrl = New MinMaxRecordController(Me, MinMaxRecordController.eRecordType.Maximum, "STARTER_RECTIFIER_VOLTAGE_VDC", "STARTER_RECTIFIER_VOLTAGE_VDC_MAX", "START_BTN")
            StarterACMaxCurrentRecordCtrl = New MinMaxRecordController(Me, MinMaxRecordController.eRecordType.Maximum, "STARTER_CURRENT_AC400HZ", "STARTER_CURRENT_AC400HZ_MAX", "START_BTN")
            StarterACMaxVoltageRecordCtrl = New MinMaxRecordController(Me, MinMaxRecordController.eRecordType.Maximum, "STARTER_VOLTAGE_AC400HZ", "STARTER_VOLTAGE_AC400HZ_MAX", "START_BTN")

            LightOffTimerCtrl = New TimerController(Me, "LIGHT_OFF_TIME_START_CONDITION_LATCH", "LIGHT_OFF_TIME_STOP_CONDITION_LATCH", "LIGHT_OFF_TIME_CONDITION_RESET", "LIGHT_OFF_TIME_SEC", Nothing, 0, "NONE")
            StartTimerCtrl = New TimerController(Me, "STARTING_TIME_START_CONDITION_LATCH", "STARTING_TIME_STOP_CONDITION_LATCH", "STARTING_TIME_CONDITION_RESET", "STARTING_TIME_SEC", Nothing, 0, "NONE")
            StartTimerACCtrl = New TimerController(Me, "STARTING_TIME_START_AC_CONDITION_LATCH", "STARTING_TIME_STOP_CONDITION_LATCH", "STARTING_TIME_CONDITION_RESET", "STARTING_AC_TIME_SEC", Nothing, 0, "NONE")
            StartTimerDCCtrl = New TimerController(Me, "STARTING_TIME_START_DC_CONDITION_LATCH", "STARTING_TIME_STOP_CONDITION_LATCH", "STARTING_TIME_CONDITION_RESET", "STARTING_DC_TIME_SEC", Nothing, 0, "NONE")

            'Configuring Load Bank Control
            Dim myLBData As LoadBankController.LB_DataInfo = Nothing

            myLBData.FanEnableChannelName = "LB1_FAN_ENABLE_CMD"
            myLBData.FanEnableBtnChannelName = "LB1_FAN_ENABLE_BTN"
            myLBData.EnableChannelName = "LB_LOAD_BTN"
            myLBData.DemandChannelName = "LB1_DEMAND"
            myLBData.UpDemandChannelName = "LB1_UP_DEMAND_BTN"
            myLBData.DownDemandChannelName = "LB1_DOWN_DEMAND_BTN"
            myLBData.DemandChannelIncrementName = "LB1_DEMAND_INCREMENT"
            myLBData.InitialDemandChannelName = Nothing
            myLBData.FrameName = LoadBankFrameControlName
            myLBData.DisplayName = "dspLB_DEMAND"
            myLBData.ThreadSpeedMsec = 250 'msec
            myLBData.Maximum = 110
            myLBData.DelestageKwpersec = 10 '10 kW/sec delestage
            myLBData.FanCoolDownSeconds = 300
            myLBData.Memorize = False
            myLBData.Increment = 1
            myLBData.FieldFlashingTimeMsec = 0
            myLBData.FieldFlashingSignalName = Nothing
            myLBData.FieldFlashingTriggerSignalName = Nothing
            myLBData.BtnLatchSeconds = 0
            myLBData.FanEnableChannelPulseName = Nothing
            myLBData.FanStopChannelPulseName = Nothing
            myLBData.PulseEnableInitialType = False
            myLBData.PulseStopInitialType = False


            Select Case CELProject.Project.Number

                Case 11825
                    myLBData.FanEnableChannelPulseName = "LB1_FAN_START_CMD"
                    myLBData.FanStopChannelPulseName = "LB1_FAN_STOP_CMD"
                    myLBData.Maximum = 105
                    myLBData.BtnLatchSeconds = 3
                    myLBData.FanCoolDownSeconds = 0   'no cooldown for this project

                Case 13015
                    'nothing specific for this project

                Case 17865
                    'nothing specific for this project

                Case Else
                    Debug.Assert(False, "OnTestSessionRunning: Load bank not configured for this project")

            End Select


            LoadBankCtrl = New LoadBankController(myLBData)

            'Configuring Load Bank Stage Values
            Select Case CELProject.Project.Number

                Case 11813
                    LoadBankCtrl.AddStage(1, "LB1_1")
                    LoadBankCtrl.AddStage(2, "LB1_2")
                    LoadBankCtrl.AddStage(4, "LB1_3")
                    LoadBankCtrl.AddStage(8, "LB1_4")
                    LoadBankCtrl.AddStage(9, "LB1_5") 'Theorical value is 10
                    LoadBankCtrl.AddStage(9, "LB1_6") 'Theorical value is 10
                    LoadBankCtrl.AddStage(22, "LB1_7") 'Theorical value is 25
                    LoadBankCtrl.AddStage(44, "LB1_8") 'Theorical value is 50
                    LoadBankCtrl.AddStage(100, "LB1_9")

                Case 12958 'maximum allowed is 110 kW, define only required stage
                    LoadBankCtrl.AddStage(1, "LB1_1")
                    LoadBankCtrl.AddStage(1, "LB1_2")
                    LoadBankCtrl.AddStage(2, "LB1_3")
                    LoadBankCtrl.AddStage(4, "LB1_4")
                    LoadBankCtrl.AddStage(8, "LB1_5")
                    LoadBankCtrl.AddStage(15, "LB1_6")
                    LoadBankCtrl.AddStage(25, "LB1_7")
                    LoadBankCtrl.AddStage(50, "LB1_8")
                    LoadBankCtrl.AddStage(100, "LB1_9")

                Case 11825
                    LoadBankCtrl.AddStage(1, "LB1_1")
                    LoadBankCtrl.AddStage(2, "LB1_2")
                    LoadBankCtrl.AddStage(2, "LB1_3")
                    LoadBankCtrl.AddStage(5, "LB1_4")
                    LoadBankCtrl.AddStage(10, "LB1_5")
                    LoadBankCtrl.AddStage(10, "LB1_6")
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
                    LoadBankCtrl.AddStage(47, "LB1_9")

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

            If Not (DeletableTEgtChCtrl Is Nothing) Then
                DeletableTEgtChCtrl.Dispose()
            End If

            If Not (DeletableTInletChCtrl Is Nothing) Then
                DeletableTInletChCtrl.Dispose()
            End If

            If Not (DeletableTBleedChCtrl Is Nothing) Then
                DeletableTBleedChCtrl.Dispose()
            End If

            If Not (LoadBankCtrl Is Nothing) Then
                LoadBankCtrl.Dispose()
            End If

            If Not (PersistentValuesCtrl Is Nothing) Then
                PersistentValuesCtrl.Dispose()
            End If

            If Not (StarterDCMaxCurrentRecordCtrl Is Nothing) Then
                StarterDCMaxCurrentRecordCtrl.Dispose()
            End If

            If Not (StarterDCMaxVoltageRecordCtrl Is Nothing) Then
                StarterDCMaxVoltageRecordCtrl.Dispose()
            End If

            If Not (StarterACMaxVoltageRecordCtrl Is Nothing) Then
                StarterACMaxVoltageRecordCtrl.Dispose()
            End If

            If Not (StarterACMaxCurrentRecordCtrl Is Nothing) Then
                StarterACMaxCurrentRecordCtrl.Dispose()
            End If

            If Not (LightOffTimerCtrl Is Nothing) Then
                LightOffTimerCtrl.Dispose()
            End If

            If Not (StartTimerCtrl Is Nothing) Then
                StartTimerCtrl.Dispose()
            End If

            If Not (StartTimerACCtrl Is Nothing) Then
                StartTimerACCtrl.Dispose()
            End If

            If Not (StartTimerDCCtrl Is Nothing) Then
                StartTimerDCCtrl.Dispose()
            End If

            If Not (ECSCtrl Is Nothing) Then
                ECSCtrl.Dispose()
            End If

            If Not (FlowmeterCtrl Is Nothing) Then
                FlowmeterCtrl.Dispose()
            End If

        End Sub
        Protected Overrides Sub SetTestStepList(value As Integer)
        End Sub

        Protected Overrides Function Init_Engine_Channels() As Boolean

            Dim Return_Value As New ArrayList()
            Dim i As Integer

            Return_Value.Clear()

            Try

                'Channels required to start and atop the archive automatically.
                Return_Value.Add(MyBase.InitializeChannel(START_BTN, "START_BTN"))

                'Channels Required to quickly close/open load air valves
                Return_Value.Add(MyBase.InitializeChannel(LOAD_AIR_VALVES_CLOSE_BTN, "LOAD_AIR_VALVES_CLOSE_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(LOAD_AIR_VALVES_OPEN_BTN, "LOAD_AIR_VALVES_OPEN_BTN"))

                'Initialise Common Engine Channels
                For i = 1 To A429IN_Fault.GetLength(0) - 1 ' we use item 1 to 5 and don't use item 0
                    Return_Value.Add(MyBase.InitializeChannel(A429IN_Fault(i), "A429IN_Fault_" + i.ToString()))
                    Return_Value.Add(MyBase.InitializeChannel(FAULT_TEXT(i), "FAULT_" + i.ToString() + "_TEXT"))
                Next i


                Return_Value.Add(MyBase.InitializeChannel(APU_FUEL_VALVE_DOOR_CLOSE_CMD, "APU_FUEL_VALVE_DOOR_CLOSE_CMD"))
                Return_Value.Add(MyBase.InitializeChannel(APU_FUEL_VALVE_DOOR_CLOSED_IND, "APU_FUEL_VALVE_DOOR_CLOSED_IND"))
                Return_Value.Add(MyBase.InitializeChannel(APU_FUEL_VALVE_DOOR_OPEN_CMD, "APU_FUEL_VALVE_DOOR_OPEN_CMD"))
                Return_Value.Add(MyBase.InitializeChannel(APU_FUEL_VALVE_DOOR_OPENED_IND, "APU_FUEL_VALVE_DOOR_OPENED_IND"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_RECORD_BTN, "ARCHIVE_RECORD_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(ARINC_IGV_POS, "ARINC_IGV_POS"))
                Return_Value.Add(MyBase.InitializeChannel(ARINC_OIL_T, "ARINC_OIL_T"))
                Return_Value.Add(MyBase.InitializeChannel(BLEED_BTN, "BLEED_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(DELEGT, "DELEGT"))
                Return_Value.Add(MyBase.InitializeChannel(DELPB, "DELPB"))
                Return_Value.Add(MyBase.InitializeChannel(DELTB, "DELTB"))
                Return_Value.Add(MyBase.InitializeChannel(DELWB, "DELWB"))
                Return_Value.Add(MyBase.InitializeChannel(DELWF, "DELWF"))
                Return_Value.Add(MyBase.InitializeChannel(EGTREQ_DEGF, "EGTREQ_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(FLHV, "FLHV"))
                Return_Value.Add(MyBase.InitializeChannel(FUEL_USED_INITIAL_LITERS, "FUEL_USED_INITIAL_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(FUEL_USED_TOTAL_LITERS, "FUEL_USED_TOTAL_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(FM_INITIAL_VOLUME_LITERS, "FM_INITIAL_VOLUME_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(FM_TOTAL_VOLUME_LITERS, "FM_TOTAL_VOLUME_LITERS"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_DOOR_FULLY_OPENED_IND, "INLET_DOOR_FULLY_OPENED_IND"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_DOOR_NOT_FULLY_OPENED_IND, "INLET_DOOR_NOT_FULLY_OPENED_IND"))
                'Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_CLOSE_CMD, "INLET_FLAP_CLOSE_CMD"))
                'Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_OPEN_CMD, "INLET_FLAP_OPEN_CMD"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_CLOSE_REQUEST_IND, "INLET_FLAP_CLOSE_REQUEST_IND"))
                Return_Value.Add(MyBase.InitializeChannel(INLET_FLAP_OPEN_REQUEST_IND, "INLET_FLAP_OPEN_REQUEST_IND"))
                Return_Value.Add(MyBase.InitializeChannel(USING_HONEYWELL_COMPUTER, "USING_HONEYWELL_COMPUTER"))
                Return_Value.Add(MyBase.InitializeChannel(ENGINE_AT_REST_IND, "ENGINE_AT_REST_IND"))
                Return_Value.Add(MyBase.InitializeChannel(ARCHIVE_STOP_BTN, "ARCHIVE_STOP_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(STARTER_AC_BTN, "STARTER_AC_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(STARTER_AC_OUTPUT_STEP, "STARTER_AC_OUTPUT_STEP"))
                Return_Value.Add(MyBase.InitializeChannel(MES_BTN, "MES_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(TCV_CLOSE_MES_OFF_BTN, "TCV_CLOSE_MES_OFF_BTN"))

                Select Case CELProject.Project.Number
                    Case 11813
                        Return_Value.Add(MyBase.InitializeChannel(LOAD_AIR_VALVE_DEMAND, "LOAD_AIR_VALVE_DEMAND"))
                    Case 12958
                        Return_Value.Add(MyBase.InitializeChannel(LOAD_AIR_VALVE_DEMAND, "LOAD_AIR_VALVE_DEMAND"))
                    Case 11825
                        Return_Value.Add(MyBase.InitializeChannel(LOAD_AIR_VALVE_3INCH_DEMAND, "LOAD_AIR_VALVE_3INCH_DEMAND"))
                        Return_Value.Add(MyBase.InitializeChannel(LOAD_AIR_VALVE_8INCH_DEMAND, "LOAD_AIR_VALVE_8INCH_DEMAND"))
                    Case 13015
                        Return_Value.Add(MyBase.InitializeChannel(LOAD_AIR_VALVE_DEMAND, "LOAD_AIR_VALVE_DEMAND"))
                        Return_Value.Add(MyBase.InitializeChannel(QUICK_LOAD_AIR_VALVE_OPEN_BTN, "QUICK_LOAD_AIR_VALVE_OPEN_BTN"))
                    Case 17865
                        Return_Value.Add(MyBase.InitializeChannel(LOAD_AIR_VALVE_DEMAND, "LOAD_AIR_VALVE_DEMAND"))
                        Return_Value.Add(MyBase.InitializeChannel(QUICK_LOAD_AIR_VALVE_OPEN_BTN, "QUICK_LOAD_AIR_VALVE_OPEN_BTN"))
                    Case Else
                        Debug.Assert(False, "Init_Engine_Channels_Value: Code Missign for this project")
                End Select

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_ARINC_IGV_POS, "LOG_PERF_ARINC_IGV_POS"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_ARINC_T2_DEGF, "LOG_PERF_ARINC_T2_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELEGT, "LOG_PERF_DELEGT"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELPB, "LOG_PERF_DELPB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELTA_P_SL, "LOG_PERF_DELTA_P_SL"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELTB, "LOG_PERF_DELTB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELWB, "LOG_PERF_DELWB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_DELWF, "LOG_PERF_DELWF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_ECS_OFFSET, "LOG_PERF_ECS_OFFSET"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGT_DP_CORR, "LOG_PERF_EGT_DP_CORR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGT_EXC_PB, "LOG_PERF_EGT_EXC_PB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGT_INST, "LOG_PERF_EGT_INST"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGTCOR, "LOG_PERF_EGTCOR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_EGTREQ_DEGF, "LOG_PERF_EGTREQ_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_FLOW_SENSOR_ACC, "LOG_PERF_FLOW_SENSOR_ACC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_GEN_SL, "LOG_PERF_GEN_SL"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_GENERATOR_POWER_KW, "LOG_PERF_GENERATOR_POWER_KW"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_NG, "LOG_PERF_NG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_OPERATING_TIME_CURRENT_HOURS, "LOG_PERF_OPERATING_TIME_CURRENT_HOURS"))

                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_BARO_PSIA, "LOG_PERF_P_BARO_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_BARO_BARA, "LOG_PERF_P_BARO_BARA"))    ' TEST


                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_BLEED1_5_PSIA, "LOG_PERF_P_BLEED1_5_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_COMP_DISCH_PSIA, "LOG_PERF_P_COMP_DISCH_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_EXHAUST_PSIA, "LOG_PERF_P_EXHAUST_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_FUEL_PSIG, "LOG_PERF_P_FUEL_PSIG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_GEARBOX_INH2O, "LOG_PERF_P_GEARBOX_INH2O"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_OIL_PSIG, "LOG_PERF_P_OIL_PSIG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_ORIFICE_DELTA_PSID, "LOG_PERF_P_ORIFICE_DELTA_PSID"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_P_ORIFICE_PSIA, "LOG_PERF_P_ORIFICE_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PB_INST_PSIA, "LOG_PERF_PB_INST_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PB_SL_PSIA, "LOG_PERF_PB_SL_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PBCOR, "LOG_PERF_PBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_PBREQ_PSIA, "LOG_PERF_PBREQ_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_BLEED_AVG_DEGF, "LOG_PERF_T_BLEED_AVG_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_EGT_DEGF_AVG, "LOG_PERF_T_EGT_DEGF_AVG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_EGT_P1_DEGF, "LOG_PERF_T_EGT_P1_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_EGT_P2_DEGF, "LOG_PERF_T_EGT_P2_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_FUEL_DEGF, "LOG_PERF_T_FUEL_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_FUEL_AVG_DEGF, "LOG_PERF_T_FUEL_AVG_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_INLET_DEGF_AVG, "LOG_PERF_T_INLET_DEGF_AVG"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_OIL_DEGF, "LOG_PERF_T_OIL_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_ORIFICE_DEGF, "LOG_PERF_T_ORIFICE_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_TBCOR, "LOG_PERF_TBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_T_TCD_DEGF, "LOG_PERF_T_TCD_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_GEARBOX, "LOG_PERF_VIB_GEARBOX"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_TURBINE, "LOG_PERF_VIB_TURBINE"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_VIB_FAN, "LOG_PERF_VIB_FAN"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB, "LOG_PERF_WB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_CALC, "LOG_PERF_WB_CALC"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_MEASURED, "LOG_PERF_WB_MEASURED"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_SL, "LOG_PERF_WB_SL"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WB_INST, "LOG_PERF_WB_INST"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCDNA, "LOG_PERF_WBCDNA"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCDNA_HI_LIM, "LOG_PERF_WBCDNA_HI_LIM"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCDNA_LO_LIM, "LOG_PERF_WBCDNA_LO_LIM"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBCOR, "LOG_PERF_WBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_AVG_PPH, "LOG_PERF_WF_AVG_PPH"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_DP_CORR, "LOG_PERF_WF_DP_CORR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_EXC_PB, "LOG_PERF_WF_EXC_PB"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_INST, "LOG_PERF_WF_INST"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WF_SL, "LOG_PERF_WF_SL"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WFCOR, "LOG_PERF_WFCOR"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WBREQ_PPM, "LOG_PERF_WBREQ_PPM"))
                Return_Value.Add(MyBase.InitializeChannel(LOG_PERF_WFREQ_PPH, "LOG_PERF_WFREQ_PPH"))

                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_ACStartTime, "Log_Perf_ACStartTime"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureACStartTime, "Log_Perf_CaptureACStartTime"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureDCStartTime, "Log_Perf_CaptureDCStartTime"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureFIGV, "Log_Perf_CaptureFIGV"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureFPBCOR, "Log_Perf_CaptureFPBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureIIGV, "Log_Perf_CaptureIIGV"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_CaptureIPBCOR, "Log_Perf_CaptureIPBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_DCStartTime, "Log_Perf_DCStartTime"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_FinalIGV, "Log_Perf_FinalIGV"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_FinalPBCOR, "Log_Perf_FinalPBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_InitIGV, "Log_Perf_InitIGV"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_InitPBCOR, "Log_Perf_InitPBCOR"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_LoadCycle96kW, "Log_Perf_LoadCycle96kW"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_LoadCycleMES, "Log_Perf_LoadCycleMES"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_LoadCycleMES96kW, "Log_Perf_LoadCycleMES96kW"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_LoadCycleRTL, "Log_Perf_LoadCycleRTL"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_MinSurgeMargin, "Log_Perf_MinSurgeMargin"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_NbApuFaults, "Log_Perf_NbApuFaults"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_OperatingTime, "Log_Perf_OperatingTime"))
                Return_Value.Add(MyBase.InitializeChannel(Log_Perf_SCVStability, "Log_Perf_SCVStability"))
                Return_Value.Add(MyBase.InitializeChannel(NG_PCT, "NG_PCT"))
                Return_Value.Add(MyBase.InitializeChannel(NUMBER_OF_STARTS, "NUMBER_OF_STARTS"))
                Return_Value.Add(MyBase.InitializeChannel(OPERATING_TIME_CURRENT_SEC, "OPERATING_TIME_CURRENT_SEC"))
                Return_Value.Add(MyBase.InitializeChannel(P_ORIFICE_PSIA, "P_ORIFICE_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(P_ORIFICE_DELTA_PSID, "P_ORIFICE_DELTA_PSID"))
                Return_Value.Add(MyBase.InitializeChannel(PBCOR_MEAN, "PBCOR_MEAN"))
                Return_Value.Add(MyBase.InitializeChannel(PBREQ_PSIA, "PBREQ_PSIA"))
                Return_Value.Add(MyBase.InitializeChannel(PERF_TEST_RESULT_ACCEPTED, "PERF_TEST_RESULT_ACCEPTED"))
                Return_Value.Add(MyBase.InitializeChannel(PERF_TEST_RESULT_REJECTED, "PERF_TEST_RESULT_REJECTED"))
                Return_Value.Add(MyBase.InitializeChannel(PERF_NEW_TEST_OPC, "PERF_NEW_TEST_OPC"))
                Return_Value.Add(MyBase.InitializeChannel(READY_TO_LOAD_IND, "READY_TO_LOAD_IND"))
                Return_Value.Add(MyBase.InitializeChannel(STARTING_AC_TIME_SEC, "STARTING_AC_TIME_SEC"))
                Return_Value.Add(MyBase.InitializeChannel(STARTING_DC_TIME_SEC, "STARTING_DC_TIME_SEC"))
                Return_Value.Add(MyBase.InitializeChannel(T_INLET_DEGF_AVG, "T_INLET_DEGF_AVG"))
                Return_Value.Add(MyBase.InitializeChannel(T_ORIFICE_DEGF, "T_ORIFICE_DEGF"))
                Return_Value.Add(MyBase.InitializeChannel(T2_POT_ADJ, "T2_POT_ADJ", False)) 'This channel is optionnal for some facilities
                Return_Value.Add(MyBase.InitializeChannel(WB_CALC, "WB_CALC"))
                Return_Value.Add(MyBase.InitializeChannel(WB_MEASURED, "WB_MEASURED"))
                Return_Value.Add(MyBase.InitializeChannel(WBCDNA_HI_LIM, "WBCDNA_HI_LIM"))
                Return_Value.Add(MyBase.InitializeChannel(WBCDNA_LO_LIM, "WBCDNA_LO_LIM"))
                Return_Value.Add(MyBase.InitializeChannel(WBREQ_PPM, "WBREQ_PPM"))
                Return_Value.Add(MyBase.InitializeChannel(WFREQ_PPH, "WFREQ_PPH"))
                Return_Value.Add(MyBase.InitializeChannel(WINDMM_BTN, "WINDMM_BTN"))
                Return_Value.Add(MyBase.InitializeChannel(WF_AVG_PPH, "WF_AVG_PPH"))
                Return_Value.Add(MyBase.InitializeChannel(WF_TOT1, "WF_TOT1"))
                Return_Value.Add(MyBase.InitializeChannel(WF_TOT2, "WF_TOT2"))

                Return_Value.Add(MyBase.InitializeChannel(T_EGT_DEGF_AVG, "T_EGT_DEGF_AVG"))
                Return_Value.Add(MyBase.InitializeChannel(T_EGT_AVG_CURRENT_DEGF, "T_EGT_AVG_CURRENT_DEGF"))

                Return_Value.Add(MyBase.InitializeObject(Test_Step_Frame, TestStepFrameControlName))
                Return_Value.Add(MyBase.InitializeObject(Ctrl_System_Frame, ControlSystemFrameName))


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



            Catch Ex As Exception
                MessageBox.Show("GTCP131_9B_Sequence.Init_Engine_Channels: " & Ex.ToString())
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
        Private START_BTN As IBooleanChannel
        Private ARCHIVE_RECORD_BTN As IBooleanChannel
        Private ARCHIVE_STOP_BTN As KineticaRT.Channels.IBooleanChannel

        Private A429IN_Fault(5) As INumericChannel  ' Array of Fault Channel, we use item 1 to 5 and don't use item 0
        Private FAULT_TEXT(5) As ITextChannel ' Array of Fault Channel Text, we use item 1 to 5 and don't use item 0

        Private T_EGT_DEGF_AVG As INumericChannel
        Private T_EGT_AVG_CURRENT_DEGF As INumericChannel

        Private ARINC_OIL_T As INumericChannel
        Private APU_FUEL_VALVE_DOOR_CLOSE_CMD As IBooleanChannel
        Private APU_FUEL_VALVE_DOOR_CLOSED_IND As IBooleanChannel
        Private APU_FUEL_VALVE_DOOR_OPEN_CMD As IBooleanChannel
        Private APU_FUEL_VALVE_DOOR_OPENED_IND As IBooleanChannel
        Private ARINC_IGV_POS As INumericChannel
        Private BLEED_BTN As IBooleanChannel
        Private DELEGT As INumericChannel
        Private DELPB As INumericChannel
        Private DELTB As INumericChannel
        Private DELWB As INumericChannel
        Private DELWF As INumericChannel
        Private EGTREQ_DEGF As INumericChannel
        Private FLHV As INumericChannel
        Private FUEL_USED_INITIAL_LITERS As INumericChannel
        Private FUEL_USED_TOTAL_LITERS As INumericChannel
        Private FM_INITIAL_VOLUME_LITERS As INumericChannel
        Private FM_TOTAL_VOLUME_LITERS As INumericChannel

        Private LOAD_AIR_VALVES_CLOSE_BTN As KineticaRT.Channels.IBooleanChannel
        Private LOAD_AIR_VALVES_OPEN_BTN As KineticaRT.Channels.IBooleanChannel

        Private QUICK_LOAD_AIR_VALVE_OPEN_BTN As KineticaRT.Channels.IBooleanChannel

        Private ENGINE_AT_REST_IND As IBooleanChannel

        Private INLET_DOOR_FULLY_OPENED_IND As IBooleanChannel
        Private INLET_DOOR_NOT_FULLY_OPENED_IND As IBooleanChannel
        'Private INLET_FLAP_CLOSE_CMD As IBooleanChannel
        'Private INLET_FLAP_OPEN_CMD As IBooleanChannel

        Private INLET_FLAP_CLOSE_REQUEST_IND As IBooleanChannel
        Private INLET_FLAP_OPEN_REQUEST_IND As IBooleanChannel

        Private STARTER_AC_BTN As IBooleanChannel
        Private STARTER_AC_OUTPUT_STEP As IBooleanChannel
        Private MES_BTN As IBooleanChannel
        Private TCV_CLOSE_MES_OFF_BTN As IBooleanChannel

        Private USING_HONEYWELL_COMPUTER As IBooleanChannel
        Private LOAD_AIR_VALVE_DEMAND As INumericChannel
        Private LOAD_AIR_VALVE_3INCH_DEMAND As INumericChannel
        Private LOAD_AIR_VALVE_8INCH_DEMAND As INumericChannel
        Private LOG_PERF_ARINC_IGV_POS As INumericChannel
        Private LOG_PERF_ARINC_T2_DEGF As INumericChannel
        Private LOG_PERF_DELEGT As INumericChannel
        Private LOG_PERF_DELPB As INumericChannel
        Private LOG_PERF_DELTA_P_SL As INumericChannel
        Private LOG_PERF_DELTB As INumericChannel
        Private LOG_PERF_DELWB As INumericChannel
        Private LOG_PERF_DELWF As INumericChannel
        Private LOG_PERF_ECS_OFFSET As INumericChannel
        Private LOG_PERF_EGT_DP_CORR As INumericChannel
        Private LOG_PERF_EGT_EXC_PB As INumericChannel
        Private LOG_PERF_EGT_INST As INumericChannel
        Private LOG_PERF_EGTCOR As INumericChannel
        Private LOG_PERF_EGTREQ_DEGF As INumericChannel
        Private LOG_PERF_FLOW_SENSOR_ACC As INumericChannel
        Private LOG_PERF_GEN_SL As INumericChannel
        Private LOG_PERF_GENERATOR_POWER_KW As INumericChannel
        Private LOG_PERF_NG As INumericChannel
        Private LOG_PERF_OPERATING_TIME_CURRENT_HOURS As INumericChannel

        Private LOG_PERF_P_BARO_PSIA As INumericChannel
        Private LOG_PERF_P_BARO_BARA As INumericChannel         ' TEST


        Private LOG_PERF_P_BLEED1_5_PSIA As INumericChannel
        Private LOG_PERF_P_COMP_DISCH_PSIA As INumericChannel
        Private LOG_PERF_P_EXHAUST_PSIA As INumericChannel
        Private LOG_PERF_P_FUEL_PSIG As INumericChannel
        Private LOG_PERF_P_GEARBOX_INH2O As INumericChannel
        Private LOG_PERF_P_OIL_PSIG As INumericChannel
        Private LOG_PERF_P_ORIFICE_DELTA_PSID As INumericChannel
        Private LOG_PERF_P_ORIFICE_PSIA As INumericChannel
        Private LOG_PERF_PB_INST_PSIA As INumericChannel
        Private LOG_PERF_PB_SL_PSIA As INumericChannel
        Private LOG_PERF_PBCOR As INumericChannel
        Private LOG_PERF_PBREQ_PSIA As INumericChannel
        Private LOG_PERF_T_BLEED_AVG_DEGF As INumericChannel
        Private LOG_PERF_T_EGT_DEGF_AVG As INumericChannel
        Private LOG_PERF_T_EGT_P1_DEGF As INumericChannel
        Private LOG_PERF_T_EGT_P2_DEGF As INumericChannel
        Private LOG_PERF_T_FUEL_DEGF As INumericChannel
        Private LOG_PERF_T_FUEL_AVG_DEGF As INumericChannel
        Private LOG_PERF_T_INLET_DEGF_AVG As INumericChannel
        Private LOG_PERF_T_OIL_DEGF As INumericChannel
        Private LOG_PERF_T_ORIFICE_DEGF As INumericChannel
        Private LOG_PERF_TBCOR As INumericChannel
        Private LOG_PERF_T_TCD_DEGF As INumericChannel
        Private LOG_PERF_VIB_FAN As INumericChannel
        Private LOG_PERF_VIB_GEARBOX As INumericChannel
        Private LOG_PERF_VIB_TURBINE As INumericChannel
        Private LOG_PERF_WB_CALC As INumericChannel
        Private LOG_PERF_WB_MEASURED As INumericChannel
        Private LOG_PERF_WB As INumericChannel
        Private LOG_PERF_WB_SL As INumericChannel
        Private LOG_PERF_WB_INST As INumericChannel
        Private LOG_PERF_WBCDNA As INumericChannel
        Private LOG_PERF_WBCDNA_HI_LIM As INumericChannel
        Private LOG_PERF_WBCDNA_LO_LIM As INumericChannel
        Private LOG_PERF_WBCOR As INumericChannel
        Private LOG_PERF_WBREQ_PPM As INumericChannel
        Private LOG_PERF_WF_AVG_PPH As INumericChannel
        Private LOG_PERF_WF_DP_CORR As INumericChannel
        Private LOG_PERF_WF_EXC_PB As INumericChannel
        Private LOG_PERF_WF_INST As INumericChannel
        Private LOG_PERF_WF_SL As INumericChannel
        Private LOG_PERF_WFCOR As INumericChannel
        Private LOG_PERF_WFREQ_PPH As INumericChannel



        Private NUMBER_OF_STARTS As INumericChannel
        Private OPERATING_TIME_CURRENT_SEC As INumericChannel
        Private P_ORIFICE_DELTA_PSID As INumericChannel
        Private P_ORIFICE_PSIA As INumericChannel
        Private PBCOR_MEAN As INumericChannel
        Private PBREQ_PSIA As INumericChannel
        Private PERF_TEST_RESULT_ACCEPTED As IBooleanChannel
        Private PERF_TEST_RESULT_REJECTED As IBooleanChannel
        Private PERF_NEW_TEST_OPC As IBooleanChannel
        Private READY_TO_LOAD_IND As IBooleanChannel
        Private STARTING_AC_TIME_SEC As INumericChannel
        Private STARTING_DC_TIME_SEC As INumericChannel
        Private T_INLET_DEGF_AVG As INumericChannel
        Private T_ORIFICE_DEGF As INumericChannel
        Private T2_POT_ADJ As IBooleanChannel
        Private WB_CALC As INumericChannel
        Private WB_MEASURED As INumericChannel
        Private WBCDNA_HI_LIM As INumericChannel
        Private WBCDNA_LO_LIM As INumericChannel
        Private WBREQ_PPM As INumericChannel
        Private WFREQ_PPH As INumericChannel
        Private WINDMM_BTN As IBooleanChannel
        Private WF_AVG_PPH As INumericChannel
        Private WF_TOT1 As INumericChannel
        Private WF_TOT2 As INumericChannel


        '*****************************
        '* Log & Performance reports *
        '*****************************

        Private Log_Perf_ACStartTime As INumericChannel
        Private Log_Perf_CaptureACStartTime As IBooleanChannel
        Private Log_Perf_CaptureDCStartTime As IBooleanChannel
        Private Log_Perf_CaptureFIGV As IBooleanChannel
        Private Log_Perf_CaptureFPBCOR As IBooleanChannel
        Private Log_Perf_CaptureIIGV As IBooleanChannel
        Private Log_Perf_CaptureIPBCOR As IBooleanChannel
        Private Log_Perf_DCStartTime As INumericChannel
        Private Log_Perf_FinalIGV As INumericChannel
        Private Log_Perf_FinalPBCOR As INumericChannel
        Private Log_Perf_InitIGV As INumericChannel
        Private Log_Perf_InitPBCOR As INumericChannel
        Private Log_Perf_LoadCycle96kW As IBooleanChannel
        Private Log_Perf_LoadCycleMES As IBooleanChannel
        Private Log_Perf_LoadCycleMES96kW As IBooleanChannel
        Private Log_Perf_LoadCycleRTL As IBooleanChannel
        Private Log_Perf_MinSurgeMargin As IBooleanChannel
        Private Log_Perf_NbApuFaults As INumericChannel
        Private Log_Perf_OperatingTime As ITextChannel
        Private Log_Perf_SCVStability As IBooleanChannel

        Private NG_PCT As INumericChannel

        '*********************
        '* Forms and Control *
        '*********************
        'Frame
        Private Test_Step_Frame As KineticaRT.Controls.Control
        Private TestStep_List As System.Windows.Forms.ComboBox

        '********
        '* Misc *
        '********
        Private cnMain As ADODB.Connection
        Private Custom_Parameters As New System.Collections.Specialized.OrderedDictionary()

        Private LastStart As Boolean = False
        Private OperatingTime_Start As System.DateTime
        Private OperatingTime As System.TimeSpan 'Used to calculate operating time

        Private ECS_Display As KineticaRT.Studio.Controls.Display

        Private ECS_Frame As KineticaRT.Controls.Control
        Private Ctrl_System_Frame As KineticaRT.Controls.Control
        Private Master_Panel_Frame As KineticaRT.Controls.Control
   
        Private PersistentValuesCtrl As PersistentValuesController = Nothing
 
        'Arrays from access table in test session
        Private GTCP131_9B_ECS(,) As Double 'Used to keep a database table in memory for fast access for GTCP131-9B
        Private GTCP131_9B_MES(,) As Double 'Used to keep a database table in memory for fast access for GTCP131-9B
        Private GTCP131_9B_WBCDNA_LIMITS(,) As Double 'Used to keep a database table in memory for fast access for GTCP131-9B

        Private DeletableTBleedChCtrl As DeletableChannelController = Nothing
        Private DeletableTEgtChCtrl As DeletableChannelController = Nothing
        Private DeletableTInletChCtrl As DeletableChannelController = Nothing
        Private FlowmeterCtrl As FlowmeterController = Nothing

        Private StarterDCMaxCurrentRecordCtrl As MinMaxRecordController = Nothing
        Private StarterDCMaxVoltageRecordCtrl As MinMaxRecordController = Nothing
        Private StarterACMaxVoltageRecordCtrl As MinMaxRecordController = Nothing
        Private StarterACMaxCurrentRecordCtrl As MinMaxRecordController = Nothing

        Private LoadBankCtrl As LoadBankController = Nothing
        Private ECSCtrl As ECSController = Nothing
        Private LightOffTimerCtrl As TimerController = Nothing
        Private StartTimerCtrl As TimerController = Nothing
        Private StartTimerACCtrl As TimerController = Nothing
        Private StartTimerDCCtrl As TimerController = Nothing

        'Private TestProgressMgr As CEL.TestProgress.TestProgressMgr = Nothing
        Private HeavyRepairType As Boolean = False

        Private UserInputsChine As New UserInputChine11813_GTCP131_9B()
        Private UserInputsEPCOR As New UserInputEPCOR12958_GTCP131_9B()
        Private UserInputsEthiopie As New UserInputEthiopie11825_GTCP131_9B()
        Private UserInputsTHY As New UserInputTHY13015_GTCP131_9B()
        Private UserInputsS7 As New UserInputS7_17865_GTCP131_9B()

        Public Structure FaultInfo
            Public FaultCode As Integer
            Public FaultText As String
        End Structure

        Private ArincFaultDefinition() As FaultInfo  ' array of fault from the test session

    End Class



	Public Class UserInputChine11813_GTCP131_9B

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

	Public Class UserInputEPCOR12958_GTCP131_9B

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
			ChoiceOperator = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()
			ChoiceInspector = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()
			ChoiceRepairType = CELProject.Test_Mgr_UUT_Choice(3).Value.ToString()
		End Sub
	End Class

    Public Class UserInputEthiopie11825_GTCP131_9B

        'Question
        Public SerialNumber As String = ""
        Public WorkOrder As String = ""
        Public PartNumber As String = ""
        Public LHV As String = ""
        Public FuelSG As String = ""
        Public TFuelDegF As String = ""
        Public OilType As String = ""
        Public FuelType As String = ""
        Public EngineVibrationPickupSerialNumber As String = ""
        Public GearboxVibrationPickupSerialNumber As String = ""
        Public CoolingFanVibrationPickupSerialNumber As String = ""

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
            EngineVibrationPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(9).Value.ToString()
            GearboxVibrationPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(10).Value.ToString()
            CoolingFanVibrationPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(11).Value.ToString()

            'Choice
            ChoiceTestType = CELProject.Test_Mgr_UUT_Choice(0).Value.ToString()
            ChoiceRepairType = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()
            ChoiceOperator = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()
            ChoiceInspector = CELProject.Test_Mgr_UUT_Choice(3).Value.ToString()

        End Sub
    End Class


    Public Class UserInputTHY13015_GTCP131_9B

        'Question
        Public SerialNumber As String = ""
        Public WorkOrder As String = ""
        Public PartNumber As String = ""
        Public LHV As String = ""
        Public FuelSG As String = ""
        Public TFuelDegF As String = ""
        Public OilType As String = ""
        Public FuelType As String = ""
        Public EngineVibrationPickupSerialNumber As String = ""
        Public GearboxVibrationPickupSerialNumber As String = ""
        Public CoolingFanVibrationPickupSerialNumber As String = ""

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
            EngineVibrationPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(9).Value.ToString()
            GearboxVibrationPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(10).Value.ToString()
            CoolingFanVibrationPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(11).Value.ToString()

            'Choice
            ChoiceTestType = CELProject.Test_Mgr_UUT_Choice(0).Value.ToString()
            ChoiceRepairType = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()
            ChoiceOperator = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()
            ChoiceInspector = CELProject.Test_Mgr_UUT_Choice(3).Value.ToString()

        End Sub
    End Class

    Public Class UserInputS7_17865_GTCP131_9B

        'Question
        Public SerialNumber As String = String.Empty
        Public WorkOrder As String = String.Empty
        Public PartNumber As String = String.Empty
        Public LHV As String = String.Empty
        Public FuelSG As String = String.Empty
        Public TFuelDegF As String = String.Empty
        Public OilType As String = String.Empty
        Public FuelType As String = String.Empty
        Public EngineVibrationPickupSerialNumber As String = String.Empty
        Public GearboxVibrationPickupSerialNumber As String = String.Empty
        Public CoolingFanVibrationPickupSerialNumber As String = String.Empty

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
            EngineVibrationPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(9).Value.ToString()
            GearboxVibrationPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(10).Value.ToString()
            CoolingFanVibrationPickupSerialNumber = CELProject.Test_Mgr_UUT_Answer(11).Value.ToString()

            'Choice
            ChoiceTestType = CELProject.Test_Mgr_UUT_Choice(0).Value.ToString()
            ChoiceOperator = CELProject.Test_Mgr_UUT_Choice(1).Value.ToString()
            ChoiceInspector = CELProject.Test_Mgr_UUT_Choice(2).Value.ToString()
            ChoiceRepairType = CELProject.Test_Mgr_UUT_Choice(3).Value.ToString()
        End Sub

    End Class

End Namespace

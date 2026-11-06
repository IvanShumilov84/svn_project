Imports System
Imports System.Drawing
Imports System.Collections
Imports System.Threading
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.IO
Imports System.Text.RegularExpressions
Imports KineticaRT
Imports KineticaRT.Channels
Imports KineticaRT.Sequence
Imports KineticaRT.Values
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.OleDb
Imports Microsoft.VisualBasic

Imports CEL.HMI
Imports CEL.Library
Imports CEL.Data.BusinessObjects

'EntitySpaces
Imports EntitySpaces.Interfaces
Imports EntitySpaces.Loader
Imports EntitySpaces.DynamicQuery
Imports System.Globalization


Namespace StudioDemonstration

	'<System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Unrestricted:=True)> _
	Public Class Calibration_Sequence
        Inherits CEL.KineticaRT.Sequence.Sequence

        Private CELProject As CELDAS = CELDAS.Instance()

		Protected Overrides Sub OnEngineWorkingThread()
			'Do Nothing
		End Sub

		Private Sub Cal_Start(ByVal Index As Integer, ByVal Prefix As String, ByVal Branch As String)
			Dim i As Integer

			'ft Force a trigger un Value Changed du Sub_Status passera de 0 a 71 afin de regler bug de curseur qui reste a Sablier dans le cas ou
			'on change trop vite de FV avec les fleche ou la souris
			Test_Mgr_Sub_Status.Value = New KineticaRT.Values.Double(CELDAS.eTest_Mgr_Sub_Status.Sub_Idling)

			If Test_Mgr_Sub_Status.ToString() = 0 Or NewSelectedIO Then
				NewSelectedIO = False

				Cal_Degree.Value = New KineticaRT.Values.Double(1)
				Cal_C(0).Value = New KineticaRT.Values.Double(Offset.ToString())
				Cal_C(1).Value = New KineticaRT.Values.Double(Slope.ToString())
				For i = 2 To 5
					Cal_C(2).Value = New KineticaRT.Values.Double(0)
				Next
				Cal_Branch.Value = New KineticaRT.Values.Text(Branch)
				Cal_Prefix.Value = New KineticaRT.Values.Text(Prefix)
				Cal_Index.Value = New KineticaRT.Values.Text(Index)

				FVName_List.ValueMember = "Branch_List"
				If (FVName_List.SelectedValue = "DACN1_SCU_IOMGR_REAL" And Prefix = "F") Then
					Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Sub_Cmd.Req_Channel_Speed)
				Else
					If (FVName_List.SelectedValue = "DACN1_SCU_IOMGR_REAL" And Prefix = "CCAS_VIB") Then
						Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Sub_Cmd.CCAS_Setup)
					Else
						Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Sub_Cmd.Xfer_Coeff)
					End If
				End If
			End If
		End Sub

		Private Function Init_Channels() As Boolean
			Dim i As Integer
			Dim Return_Value As New ArrayList()

			Return_Value.Clear()

			'Initialise Channels
			Return_Value.Add(MyBase.InitializeChannel(Accuracy, "Accuracy"))
			Return_Value.Add(MyBase.InitializeChannel(Cal_Branch, "Cal_Branch"))
			For i = 0 To Cal_C.GetUpperBound(0) - 1
				Return_Value.Add(MyBase.InitializeChannel(Cal_C(i), "Cal_C" + (i + 1).ToString()))
			Next
			Return_Value.Add(MyBase.InitializeChannel(Cal_Degree, "Cal_Degree"))
			Return_Value.Add(MyBase.InitializeChannel(Cal_Index, "Cal_Index"))
			Return_Value.Add(MyBase.InitializeChannel(Cal_Prefix, "Cal_Prefix"))
			Return_Value.Add(MyBase.InitializeChannel(Calib_Resize_Panel, "Calib_Resize_Panel"))
			Return_Value.Add(MyBase.InitializeChannel(Calib_UserInputs_Cancel, "Calib_UserInputs_Cancel"))
			Return_Value.Add(MyBase.InitializeChannel(Calib_UserInputs_OK, "Calib_UserInputs_OK"))
			For i = 0 To Calibration_Due_Date.GetUpperBound(0) - 1
				Return_Value.Add(MyBase.InitializeChannel(Calibration_Due_Date(i), "Calibration_Due_Date" + (i + 1).ToString()))
			Next
			Return_Value.Add(MyBase.InitializeChannel(Calibration_Range, "Calibration_Range"))
			Return_Value.Add(MyBase.InitializeChannel(Calibration_Value, "Calibration_Value"))
			Return_Value.Add(MyBase.InitializeChannel(Calibration_Temp, "Calibration_Temp"))
			Return_Value.Add(MyBase.InitializeChannel(Calibration_Humidity, "Calibration_Humidity"))
			Return_Value.Add(MyBase.InitializeChannel(TC_Transducer_SN, "TC_Transducer_SN"))
			Return_Value.Add(MyBase.InitializeChannel(CCAS_Requested_Vib_Channel, "CCAS_Requested_Vib_Channel"))
			Return_Value.Add(MyBase.InitializeChannel(Channel_Range, "Channel_Range"))
			Return_Value.Add(MyBase.InitializeChannel(Channel_Type, "Channel_Type"))
			Return_Value.Add(MyBase.InitializeChannel(Check_Report, "Check_Report"))
			For i = 0 To Clear_Button.GetUpperBound(0) - 1
				Return_Value.Add(MyBase.InitializeChannel(Clear_Button(i), "P" + (i + 1).ToString() + "_Clear_Button"))
			Next
			Return_Value.Add(MyBase.InitializeChannel(Comments, "Comments"))
			Return_Value.Add(MyBase.InitializeChannel(CSV_Filename, "CSV_Filename")) 'Multiple StrongBack support see Prj08618
			For i = 0 To Enter_Button.GetUpperBound(0) - 1
				Return_Value.Add(MyBase.InitializeChannel(Enter_Button(i), "P" + (i + 1).ToString() + "_Enter_Button"))
			Next
			Return_Value.Add(MyBase.InitializeChannel(Generic_Name, "Generic_Name"))
			Return_Value.Add(MyBase.InitializeChannel(Last_Calib_Date, "Last_Calib_Date"))
			Return_Value.Add(MyBase.InitializeChannel(Margin_Accuracy, "Margin_Accuracy"))
			Return_Value.Add(MyBase.InitializeChannel(OEM_Tolerance_Text, "OEM_Tolerance_Text"))
			Return_Value.Add(MyBase.InitializeChannel(OEM_Tolerance, "OEM_Tolerance"))
			For i = 0 To Measured.GetUpperBound(0) - 1
				Return_Value.Add(MyBase.InitializeChannel(Measured(i), "P" + (i + 1).ToString() + "_Measured"))
				Return_Value.Add(MyBase.InitializeChannel(Measured_Button(i), "P" + (i + 1).ToString() + "_Measured_Button"))
			Next
			Return_Value.Add(MyBase.InitializeChannel(New_Calib_Report, "New_Calib_Report"))
			Return_Value.Add(MyBase.InitializeChannel(Offset, "Offset"))
			Return_Value.Add(MyBase.InitializeChannel(Save_Data, "Save_Data"))
			Return_Value.Add(MyBase.InitializeChannel(SCU_Speed_Channel, "SCU_Speed_Channel"))
			For i = 0 To Simulated.GetUpperBound(0) - 1
				Return_Value.Add(MyBase.InitializeChannel(Simulated(i), "P" + (i + 1).ToString() + "_Simulated"))
			Next
			Return_Value.Add(MyBase.InitializeChannel(Slope, "Slope"))
			Return_Value.Add(MyBase.InitializeChannel(Test_Mgr_Calib_Calcul, "Test_Mgr_Calib_Calcul"))
			Return_Value.Add(MyBase.InitializeChannel(Test_Mgr_Calib_Flag, "Test_Mgr_Calib_Flag"))
			Return_Value.Add(MyBase.InitializeChannel(Test_Mgr_PUsed, "Test_Mgr_PUsed"))
			Return_Value.Add(MyBase.InitializeChannel(Test_Mgr_Sub_Command, "Test_Mgr_Sub_Command"))
			Return_Value.Add(MyBase.InitializeChannel(Test_Mgr_Sub_Status, "Test_Mgr_Sub_Status"))
			Return_Value.Add(MyBase.InitializeChannel(Transducer_Range, "Transducer_Range"))
			Return_Value.Add(MyBase.InitializeChannel(Units_Measured, "Units_Measured"))
			Return_Value.Add(MyBase.InitializeChannel(Units_Simulated, "Units_Simulated"))
            Return_Value.Add(MyBase.InitializeChannel(Save_Calib_Info, "Save_Calib_Info"))

            'Initialise Objects			
            Return_Value.Add(MyBase.InitializeObject(Calibration_Input_Data, "Calibration_Input_Data"))
			Return_Value.Add(MyBase.InitializeObject(Calibration, "Calibration"))
			Return_Value.Add(MyBase.InitializeObject(Calibration_Information_Panel, "Calibration.Calibration_Information_Panel"))

			Return_Value.Add(MyBase.InitializeObject(SAVE_CALIB_INFO_BTN, "Calibration.SAVE_CALIB_INFO_BTN"))
			Return_Value.Add(MyBase.InitializeObject(PressEnter_Label, "Calibration.PressEnter_Label"))

			Frame1 = Calibration_Information_Panel.FindObject("Frame1")
			If Frame1 Is Nothing Then
				MessageBox.Show("Initialize_Object - Frame1 Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If
			LAST_CALIB_DISPLAY = Frame1.FindObject("Display10")
			If LAST_CALIB_DISPLAY Is Nothing Then
				MessageBox.Show("Initialize_Object - LAST_CALIB_DISPLAY Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If
			ACCURACY_DISPLAY = Frame1.FindObject("Display1")
			If ACCURACY_DISPLAY Is Nothing Then
				MessageBox.Show("Initialize_Object - ACCURACY_DISPLAY Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If
			SIMULATED_VALUE_DISPLAY = Frame1.FindObject("Display3")
			If SIMULATED_VALUE_DISPLAY Is Nothing Then
				MessageBox.Show("Initialize_Object - SIMULATED_VALUE_DISPLAY Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If
			TRANSDUCER_RANGE_DISPLAY = Frame1.FindObject("Display5")
			If TRANSDUCER_RANGE_DISPLAY Is Nothing Then
				MessageBox.Show("Initialize_Object - TRANSDUCER_RANGE_DISPLAY Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If
			COMMENTS_DISPLAY = Frame1.FindObject("Display6")
			If COMMENTS_DISPLAY Is Nothing Then
				MessageBox.Show("Initialize_Object - COMMENTS_DISPLAY Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If
			SimulatedUnits_List = Frame1.FindObject("SimulatedUnits_List")
			If SimulatedUnits_List Is Nothing Then
				MessageBox.Show("Initialize_Object - SimulatedUnits_List Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If

			Frame2 = Calibration_Information_Panel.FindObject("Frame2")
			If Frame2 Is Nothing Then
				MessageBox.Show("Initialize_Object - Frame2 Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If
			Slope_Txt = Frame2.FindObject("Slope_Txt")
			If Slope_Txt Is Nothing Then
				MessageBox.Show("Initialize_Object - Slope_Txt Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If
			Offset_Txt = Frame2.FindObject("Offset_Txt")
			If Offset_Txt Is Nothing Then
				MessageBox.Show("Initialize_Object - Offset_Txt Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If

			Frame3 = Calibration_Information_Panel.FindObject("Frame3")
			If Frame3 Is Nothing Then
				MessageBox.Show("Initialize_Object - Frame3 Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If
			FVName_List = Frame3.FindObject("FVName_List")
			If FVName_List Is Nothing Then
				MessageBox.Show("Initialize_Object - FVName_List Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If


			Return_Value.Add(MyBase.InitializeObject(Calibration_Panel, "Calibration.Calibration_Panel"))
			For i = 0 To Point_Frame.GetUpperBound(0) - 1
				Point_Frame(i) = Calibration_Panel.FindObject("P" + (i + 1).ToString() + "_Frame")
				If Point_Frame(i) Is Nothing Then
					MessageBox.Show("Initialize_Object - P" + (i + 1).ToString() + "_Frame Is Nothing")
					Return_Value.Add(False)
				Else
					Return_Value.Add(True)
				End If
			Next

			For i = 0 To Measured_Txt.GetUpperBound(0) - 1
				Measured_Txt(i) = Point_Frame(i).FindObject("P" + (i + 1).ToString() + "_Measured_Txt")
				If Measured_Txt(i) Is Nothing Then
					MessageBox.Show("Initialize_Object - P" + (i + 1).ToString() + "_Measured_Txt Is Nothing")
					Return_Value.Add(False)
				Else
					Return_Value.Add(True)
				End If
			Next

			For i = 0 To Simulated_Txt.GetUpperBound(0) - 1
				Simulated_Txt(i) = Point_Frame(i).FindObject("P" + (i + 1).ToString() + "_Simulated_Txt")
				If Simulated_Txt(i) Is Nothing Then
					MessageBox.Show("Initialize_Object - P" + (i + 1).ToString() + "_Simualted_Txt Is Nothing")
					Return_Value.Add(False)
				Else
					Return_Value.Add(True)
				End If
			Next

			For i = 0 To Button1.GetUpperBound(0) - 1
				Button1(i) = Point_Frame(i).FindObject("Button1")
				If Button1(i) Is Nothing Then
					MessageBox.Show("Initialize_Object - Button1 Is Nothing")
					Return_Value.Add(False)
				Else
					Return_Value.Add(True)
				End If
			Next

			For i = 0 To Button2.GetUpperBound(0) - 1
				Button2(i) = Point_Frame(i).FindObject("Button2")
				If Button2(i) Is Nothing Then
					MessageBox.Show("Initialize_Object - Button2 Is Nothing")
					Return_Value.Add(False)
				Else
					Return_Value.Add(True)
				End If
			Next

			For i = 0 To Button3.GetUpperBound(0) - 1
				Button3(i) = Point_Frame(i).FindObject("Button3")
				If Button3(i) Is Nothing Then
					MessageBox.Show("Initialize_Object - Button3 Is Nothing")
					Return_Value.Add(False)
				Else
					Return_Value.Add(True)
				End If
			Next

			Return_Value.Add(MyBase.InitializeObject(Approval_List, "Calibration_Input_Data.Approval_List"))
			Return_Value.Add(MyBase.InitializeObject(Assistant_List, "Calibration_Input_Data.Assistant_List"))
			For i = 0 To Make_List.GetUpperBound(0) - 1
				Return_Value.Add(MyBase.InitializeObject(Make_List(i), "Calibration_Input_Data.Make_List" + (i + 1).ToString()))
			Next
			For i = 0 To Model_List.GetUpperBound(0) - 1
				Return_Value.Add(MyBase.InitializeObject(Model_List(i), "Calibration_Input_Data.Model_List" + (i + 1).ToString()))
			Next
			For i = 0 To Serial_Number_List.GetUpperBound(0) - 1
				Return_Value.Add(MyBase.InitializeObject(Serial_Number_List(i), "Calibration_Input_Data.Serial_Number_List" + (i + 1).ToString()))
			Next
			Return_Value.Add(MyBase.InitializeObject(CalibratedBy_List, "Calibration_Input_Data.CalibratedBy_List"))
            Return_Value.Add(MyBase.InitializeObject(InstrumentRange_Display, "Calibration_Input_Data.dspInstrumentRange"))

            PARAMETERS_TXT_DISPLAY = Frame3.FindObject("PARAMETERS_TXT_DISPLAY")
			If PARAMETERS_TXT_DISPLAY Is Nothing Then
				MessageBox.Show("Initialize_Object - PARAMETERS_TXT_DISPLAY Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If

			MEASURED_VALUE_DISPLAY = Frame3.FindObject("MEASURED_VALUE_DISPLAY")
			If MEASURED_VALUE_DISPLAY Is Nothing Then
				MessageBox.Show("Initialize_Object - MEASURED_VALUE_DISPLAY Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If

			MEASURED_TXT_DISPLAY = Frame3.FindObject("MEASURED_TXT_DISPLAY")
			If MEASURED_TXT_DISPLAY Is Nothing Then
				MessageBox.Show("Initialize_Object - MEASURED_TXT_DISPLAY Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If

			CALIBRATION_BTN = Frame3.FindObject("CALIBRATION_BTN")
			If CALIBRATION_BTN Is Nothing Then
				MessageBox.Show("Initialize_Object - CALIBRATION_BTN Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If

			Return_Value.Add(MyBase.InitializeObject(ControlsFrame, "Calibration.ControlsFrame"))

			'CHECK_REPORT_BTN = ControlsFrame.FindObject("CHECK_REPORT_BTN")
			'If CHECK_REPORT_BTN Is Nothing Then
			'    MessageBox.Show("Initialize_Object - CHECK_REPORT_BTN Is Nothing")
			'    Return_Value.Add(False)
			'Else
			'    Return_Value.Add(True)
			'End If

			NEW_REPORT_BTN = ControlsFrame.FindObject("NEW_REPORT_BTN")
			If NEW_REPORT_BTN Is Nothing Then
				MessageBox.Show("Initialize_Object - NEW_REPORT_BTN Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If

			SAVE_DATA_BTN = ControlsFrame.FindObject("SAVE_DATA_BTN")
			If SAVE_DATA_BTN Is Nothing Then
				MessageBox.Show("Initialize_Object - SAVE_DATA_BTN Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If

			CALIB_RESIZE_BTN = ControlsFrame.FindObject("CALIB_RESIZE_BTN")
			If CALIB_RESIZE_BTN Is Nothing Then
				MessageBox.Show("Initialize_Object - CALIB_RESIZE_BTN Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If

			btnClose = ControlsFrame.FindObject("btnClose")
			If btnClose Is Nothing Then
				MessageBox.Show("Initialize_Object - CLOSE_TEST_BTN Is Nothing")
				Return_Value.Add(False)
			Else
				Return_Value.Add(True)
			End If

			Return_Value.Add(MyBase.InitializeObject(DAS_CALIB_DISPLAY, "Calibration.DAS_CALIB_DISPLAY"))
			Return_Value.Add(MyBase.InitializeObject(GodMode_Label, "Calibration.GodMode_Label"))
			Return_Value.Add(MyBase.InitializeObject(Picture2, "Calibration.Picture2"))

			If Return_Value.Contains(False) Then
				Return False
			Else
				Return True
			End If
		End Function

		Private Sub Init_Channels_Value()
			Dim i As Integer

			Calib_Resize_Panel.Value = Values.False
			Calib_UserInputs_Cancel.Value = Values.False
			Calib_UserInputs_OK.Value = Values.False
			Calibration_Input_Data.Visible = False
			CalibrationFlag = False
			CheckFlag = False
			Check_Report.Value = Values.False
			ItemSelected = False
			New_Calib_Report.Value = Values.False
			NewSelectedIO = False
			For i = 0 To Clear_Button.GetUpperBound(0) - 1
				Clear_Button(i).Value = Values.False
				Enter_Button(i).Value = Values.False
				Measured_Button(i).Value = Values.False
			Next
			PUsed(1) = 0
			PUsed(2) = 0
			PUsed(3) = 0
			PUsed(4) = 0
			PUsed(5) = 0
			PUsed(6) = 0
			Save_Data.Value = Values.False
			Test_Mgr_Calib_Flag.Value = Values.False
		End Sub

		Private Sub Initialize_Calib_InputData()

			Dim Found As Boolean
			Dim i, j As Integer
			Dim Make_Counter As Integer
			Dim MakeList(200) As String
			Dim Model_Counter As Integer
			Dim ModelList(200) As String
			Dim PUsed_Tot As Integer
			Dim Serial_Number_Counter As Integer
			Dim Serial_NumberList(200) As String
			Dim CalibratedByCounter As Integer
			Dim CalibratedByList(200) As String
			Dim AssistantCounter As Integer
			Dim AssistantList(200) As String
			Dim ApprovalCounter As Integer
			Dim ApprovalList(200) As String

			PUsed_Tot = PUsed(0) + PUsed(1) + PUsed(2) + PUsed(3) + PUsed(4) + PUsed(5)
			If PUsed_Tot < 2 Then
				MessageBox.Show("Minimum of two points is required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Else
				Test_Mgr_PUsed.Value = New KineticaRT.Values.Double(PUsed_Tot)
				Test_Mgr_Calib_Calcul.Value = New KineticaRT.Values.Double(444)



				'Get the information about calibration for the selected FV
				FVName_List.ValueMember = "Branch_List"
				'Query Database Real
				Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
				dacnRealChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
				dacnRealChannelCollection.Query.es.QuerySource = FVName_List.SelectedValue 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
				dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
				dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
				dacnRealChannelCollection.Query.Load()
				'SelectString = "SELECT * FROM " + FVName_List.SelectedValue

				'Initialization of the Lists
				For i = 0 To 200
					MakeList(i) = ""
					ModelList(i) = ""
					Serial_NumberList(i) = ""
					CalibratedByList(i) = ""
					AssistantList(i) = ""
					ApprovalList(i) = ""
				Next
				For i = 0 To Make_List.GetUpperBound(0) - 1
					Make_List(i).Items.Clear()
					Model_List(i).Items.Clear()
					Serial_Number_List(i).Items.Clear()
				Next
				CalibratedBy_List.Items.Clear()
				Assistant_List.Items.Clear()
				Approval_List.Items.Clear()

				'Initialization of counter
				Make_Counter = 0
				Model_Counter = 0
				Serial_Number_Counter = 0
				CalibratedByCounter = 0
				AssistantCounter = 0
				ApprovalCounter = 0

				'Fill up the Drop Down List
				For Each channel As DacnTemplateReal In dacnRealChannelCollection


					'CONSTRUCTION OF MAKELIST
					'MAKE1
					If Not (channel.Make1 = "") Then
						Found = False
						For j = 0 To Make_Counter
							If MakeList(j) = channel.Make1 Then
								Found = True
								j = Make_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							MakeList(Make_Counter) = channel.Make1
							For i = 0 To Make_List.GetUpperBound(0) - 1
								Make_List(i).Items.Add(MakeList(Make_Counter))
							Next
							Make_Counter = Make_Counter + 1
						End If
					End If
					'MAKE2
					If Not (channel.Make2 = "") Then
						Found = False
						For j = 0 To Make_Counter
							If MakeList(j) = channel.Make2 Then
								Found = True
								j = Make_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							MakeList(Make_Counter) = channel.Make2
							For i = 0 To Make_List.GetUpperBound(0) - 1
								Make_List(i).Items.Add(MakeList(Make_Counter))
							Next
							Make_Counter = Make_Counter + 1
						End If
					End If
					'MAKE3
					If Not (channel.Make3 = "") Then
						Found = False
						For j = 0 To Make_Counter
							If MakeList(j) = channel.Make3 Then
								Found = True
								j = Make_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							MakeList(Make_Counter) = channel.Make3
							For i = 0 To Make_List.GetUpperBound(0) - 1
								Make_List(i).Items.Add(MakeList(Make_Counter))
							Next
							Make_Counter = Make_Counter + 1
						End If
					End If
					'MAKE4
					If Not (channel.Make4 = "") Then
						Found = False
						For j = 0 To Make_Counter
							If MakeList(j) = channel.Make4 Then
								Found = True
								j = Make_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							MakeList(Make_Counter) = channel.Make4
							For i = 0 To Make_List.GetUpperBound(0) - 1
								Make_List(i).Items.Add(MakeList(Make_Counter))
							Next
							Make_Counter = Make_Counter + 1
						End If
					End If

					'CONSTRUCTION OF MODELLIST
					'MODEL1
					If Not (channel.Model1 = "") Then
						Found = False
						For j = 0 To Model_Counter
							If ModelList(j) = channel.Model1 Then
								Found = True
								j = Model_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							ModelList(Model_Counter) = channel.Model1
							For i = 0 To Model_List.GetUpperBound(0) - 1
								Model_List(i).Items.Add(ModelList(Model_Counter))
							Next
							Model_Counter = Model_Counter + 1
						End If
					End If
					'MODEL2
					If Not (channel.Model2 = "") Then
						Found = False
						For j = 0 To Model_Counter
							If ModelList(j) = channel.Model2 Then
								Found = True
								j = Model_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							ModelList(Model_Counter) = channel.Model2
							For i = 0 To Model_List.GetUpperBound(0) - 1
								Model_List(i).Items.Add(ModelList(Model_Counter))
							Next
							Model_Counter = Model_Counter + 1
						End If
					End If
					'MODEL3
					If Not (channel.Model3 = "") Then
						Found = False
						For j = 0 To Model_Counter
							If ModelList(j) = channel.Model3 Then
								Found = True
								j = Model_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							ModelList(Model_Counter) = channel.Model3
							For i = 0 To Model_List.GetUpperBound(0) - 1
								Model_List(i).Items.Add(ModelList(Model_Counter))
							Next
							Model_Counter = Model_Counter + 1
						End If
					End If
					'MODEL4
					If Not (channel.Model4 = "") Then
						Found = False
						For j = 0 To Model_Counter
							If ModelList(j) = channel.Model4 Then
								Found = True
								j = Model_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							ModelList(Model_Counter) = channel.Model4
							For i = 0 To Model_List.GetUpperBound(0) - 1
								Model_List(i).Items.Add(ModelList(Model_Counter))
							Next
							Model_Counter = Model_Counter + 1
						End If
					End If

					'CONSTRUCTION OF SERIAL_NUMBERLIST
					'SERIAL NUMBER 1
					If Not (channel.SerialNumber1 = "") Then
						Found = False
						For j = 0 To Serial_Number_Counter
							If Serial_NumberList(j) = channel.SerialNumber1 Then
								Found = True
								j = Serial_Number_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							Serial_NumberList(Serial_Number_Counter) = channel.SerialNumber1
							For i = 0 To Serial_Number_List.GetUpperBound(0) - 1
								Serial_Number_List(i).Items.Add(Serial_NumberList(Serial_Number_Counter))
							Next
							Serial_Number_Counter = Serial_Number_Counter + 1
						End If
					End If
					'SERIAL NUMBER 2
					If Not (channel.SerialNumber2 = "") Then
						Found = False
						For j = 0 To Serial_Number_Counter
							If Serial_NumberList(j) = channel.SerialNumber2 Then
								Found = True
								j = Serial_Number_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							Serial_NumberList(Serial_Number_Counter) = channel.SerialNumber2
							For i = 0 To Serial_Number_List.GetUpperBound(0) - 1
								Serial_Number_List(i).Items.Add(Serial_NumberList(Serial_Number_Counter))
							Next
							Serial_Number_Counter = Serial_Number_Counter + 1
						End If
					End If

					'SERIAL NUMBER 3
					If Not (channel.SerialNumber3 = "") Then
						Found = False
						For j = 0 To Serial_Number_Counter
							If Serial_NumberList(j) = channel.SerialNumber3 Then
								Found = True
								j = Serial_Number_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							Serial_NumberList(Serial_Number_Counter) = channel.SerialNumber3
							For i = 0 To Serial_Number_List.GetUpperBound(0) - 1
								Serial_Number_List(i).Items.Add(Serial_NumberList(Serial_Number_Counter))
							Next
							Serial_Number_Counter = Serial_Number_Counter + 1
						End If
					End If

					'SERIAL NUMBER 4
					If Not (channel.SerialNumber4 = "") Then
						Found = False
						For j = 0 To Serial_Number_Counter
							If Serial_NumberList(j) = channel.SerialNumber4 Then
								Found = True
								j = Serial_Number_Counter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							Serial_NumberList(Serial_Number_Counter) = channel.SerialNumber4
							For i = 0 To Serial_Number_List.GetUpperBound(0) - 1
								Serial_Number_List(i).Items.Add(Serial_NumberList(Serial_Number_Counter))
							Next
							Serial_Number_Counter = Serial_Number_Counter + 1
						End If
					End If

					'CONSTRUCTION OF CALIBRATED BY LIST
					If Not (channel.CalibratedBy = "") Then
						Found = False
						For j = 0 To CalibratedByCounter
							If CalibratedByList(j) = channel.CalibratedBy Then
								Found = True
								j = CalibratedByCounter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							CalibratedByList(CalibratedByCounter) = channel.CalibratedBy
							CalibratedBy_List.Items.Add(CalibratedByList(CalibratedByCounter))
							CalibratedByCounter = CalibratedByCounter + 1
						End If
					End If

					'CONSTRUCTION OF Assistant IST
					If Not (channel.Assistant = "") Then
						Found = False
						For j = 0 To AssistantCounter
							If AssistantList(j) = channel.Assistant Then
								Found = True
								j = AssistantCounter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							AssistantList(AssistantCounter) = channel.Assistant
							Assistant_List.Items.Add(AssistantList(AssistantCounter))
							AssistantCounter = AssistantCounter + 1
						End If
					End If

					'CONSTRUCTION OF Approval List
					If Not (channel.Approval = "") Then
						Found = False
						For j = 0 To ApprovalCounter
							If ApprovalList(j) = channel.Approval Then
								Found = True
								j = ApprovalCounter
							End If
						Next
						'Put the value in the list if is not
						If Found = False Then
							ApprovalList(ApprovalCounter) = channel.Approval
							Approval_List.Items.Add(ApprovalList(ApprovalCounter))
							ApprovalCounter = ApprovalCounter + 1
						End If
					End If
				Next

				'Get the information about calibration for the selected FV
				'FVName_List.ValueMember = "Branch_List"
				'SelectString = "SELECT * FROM " + FVName_List.SelectedValue
				For Each channel As DacnTemplateReal In dacnRealChannelCollection

					FVName_List.ValueMember = "FV_Name"
					If (channel.Name = FVName_List.SelectedValue) Then
						Margin_Accuracy.Value = New KineticaRT.Values.Text(channel.MarginAccuracy)

						If (IsNothing(channel.OEMTolerance)) Then
							OEM_Tolerance_Text.Value = New KineticaRT.Values.Text("=NA()")
						Else
							If (channel.OEMTolerance.Value = 0.0) Then
								OEM_Tolerance_Text.Value = New KineticaRT.Values.Text("=NA()")
							Else
								OEM_Tolerance_Text.Value = New KineticaRT.Values.Text(channel.OEMTolerance)
							End If
						End If
						Channel_Type.Value = New KineticaRT.Values.Text(channel.ChannelType)
						Channel_Range.Value = New KineticaRT.Values.Text(channel.ChannelRange)
						Transducer_Range.Value = New KineticaRT.Values.Text(channel.TransducerRange)
						Calibration_Range.Value = New KineticaRT.Values.Text(channel.CalibrationRange)

						TC_Transducer_SN.Value = New KineticaRT.Values.Text(channel.TCTransducerSN)

						Make_List(0).Text = channel.Make1
						Model_List(0).Text = channel.Model1
						Serial_Number_List(0).Text = channel.SerialNumber1
						Calibration_Due_Date(0).Value = New KineticaRT.Values.Text(channel.CalibrationDueDate1)

						Make_List(1).Text = channel.Make2
						Model_List(1).Text = channel.Model2
						Serial_Number_List(1).Text = channel.SerialNumber2
						Calibration_Due_Date(1).Value = New KineticaRT.Values.Text(channel.CalibrationDueDate2)

						Make_List(2).Text = channel.Make3
						Model_List(2).Text = channel.Model3
						Serial_Number_List(2).Text = channel.SerialNumber3
						Calibration_Due_Date(2).Value = New KineticaRT.Values.Text(channel.CalibrationDueDate3)

						Make_List(3).Text = channel.Make4
						Model_List(3).Text = channel.Model4
						Serial_Number_List(3).Text = channel.SerialNumber4
						Calibration_Due_Date(3).Value = New KineticaRT.Values.Text(channel.CalibrationDueDate4)

						Comments.Value = New KineticaRT.Values.Text(channel.Comments)
						Generic_Name.Value = New KineticaRT.Values.Text(channel.HMIAliasName)
						CalibratedBy_List.Text = channel.CalibratedBy
						Assistant_List.Text = channel.Assistant
						Approval_List.Text = channel.Approval
					End If
				Next

				'To detect changed value (God mode)
				initial_InstrumentRange = Channel_Range.Value.ToString

				'Open the User Inputs Data
				Calibration_Input_Data.Visible = True
				Calibration_Input_Data.TopMost = True
				Calibration_Input_Data.Refresh()
			End If
		End Sub


		Private Sub Initialize_Calib_Window()
			Dim i As Integer

			'Calibration window Initialization			
			CalibrationFlag = False
			Accuracy.Value = Values.EmptyString
			OEM_Tolerance.Value = KineticaRT.Values.Values.EmptyString
			Calibration_Value.Value = New KineticaRT.Values.Double(0)

			TC_Transducer_SN.Value = KineticaRT.Values.Values.EmptyString

			Comments.Value = Values.EmptyString
			Generic_Name.Value = Values.EmptyString
			Last_Calib_Date.Value = Values.EmptyString
			Offset.Value = New KineticaRT.Values.Double(0)
			Offset_Txt.ValueColor = System.Drawing.Color.Blue

			For i = 0 To 5
				Point_Frame(i).BackColor = System.Drawing.Color.Black
				Measured(i).Value = New KineticaRT.Values.Double(0)
				Measured_Txt(i).ValueColor = System.Drawing.Color.Blue
				Simulated(i).Value = New KineticaRT.Values.Double(0)
				Simulated_Txt(i).InputEnabled = False
				Simulated_Txt(i).ValueColor = System.Drawing.Color.Blue
			Next

			Slope.Value = New KineticaRT.Values.Double(0)
			Slope_Txt.ValueColor = System.Drawing.Color.Blue
			Test_Mgr_Calib_Flag.Value = Values.False
			Transducer_Range.Value = Values.EmptyString
			Units_Measured.Value = Values.EmptyString
			Units_Simulated.Value = Values.EmptyString
		End Sub


		'FT j<ai esaye de la convertir a EntitySpaces mais c'est beaucoup trop complexe.
		Private Sub Initialize_Calibration()
			Dim calibrationFVName As New ArrayList()
			Dim command As OleDbCommand
			'Dim ConnectionString 		As String 
			Dim dacn1Table As New ArrayList()
			Dim i As Integer
			Dim j As Integer
			Dim reader As OleDbDataReader
			Dim selectString As String
			Dim table As String
			'Dim Table2 As String  'Multiple StrongBack support see Prj08618
			Dim connection As OleDbConnection



			Initialize_Calib_Window()

			connection = New OleDbConnection(CELProject.Database.OleDbConnectionString)

			'Open the connection.
			Try
				connection.Open()
			Catch exception As System.Exception
				System.Diagnostics.Trace.WriteLine(exception.Message) 'Probably not found the file.
			End Try

			'Get All the table
			'SelectString = "SELECT [DACL Real] FROM DACN_MAP"
			selectString = "SELECT [DACL Real] FROM MAP WHERE [DACL Real] <> 'NO TABLE' " 'Multiple StrongBack support see Prj08618
			command = New OleDbCommand(selectString, connection)
			reader = command.ExecuteReader()

			While (reader.Read())
				'Populate the Calibration_FVName Array
				dacn1Table.Add(reader.Item("DACL Real").ToString())
			End While
			reader.Close()

			calibrationFVName.Clear()


			'Multiple StrongBack support see Prj08618
			i = 0
			For Each table In dacn1Table
				If (CSV_Filename.Value.ToString() = "/usr/v2000/app/DACN1_Real_PowerDNA_CSV_1.csv") And (table = "DACN1_PowerDNA_IOMGR_Real") Then
					table = "DACN1_PowerDNA_IOMGR_Real_1"
					Strongback_Number = 1
				End If

				If (CSV_Filename.Value.ToString() = "/usr/v2000/app/DACN1_Real_PowerDNA_CSV_2.csv") And (table = "DACN1_PowerDNA_IOMGR_Real") Then
					table = "DACN1_PowerDNA_IOMGR_Real_2"
					Strongback_Number = 2
				End If

				If table = "DACN1_PowerDNA_IOMGR_Real" Then 'ft fix en simulation la var CSV_Filename est vide alors affecte la valeur de la table par defaut
					table = "DACN1_PowerDNA_IOMGR_Real_1"
					Strongback_Number = 1
				End If

				If i = 0 Then
					selectString = "SELECT Name,HMIAliasName, '" + table + "' As BranchList FROM " + table
					selectString += " WHERE DataProcessingOn = True And ExportToCsv = True And ProjectID = " + CELProject.Project.Number.ToString + " "
					selectString += " AND Name Not like" + Chr(34) + "NO NAME " + Chr(34)
					For j = 0 To CELProject.NO_CALIBRATION_CHANNEL.GetUpperBound(0)
						selectString += " AND Name Not like" + Chr(34) + CELProject.NO_CALIBRATION_CHANNEL(j) + Chr(34) + " "
					Next

				Else
					selectString += "UNION "
					selectString += "Select  Name,HMIAliasName, '" + table + "' As BranchList FROM " + table
					selectString += " WHERE DataProcessingOn = True And ExportToCsv = True And ProjectID = " + CELProject.Project.Number.ToString + " "

					For j = 0 To CELProject.NO_CALIBRATION_CHANNEL.GetUpperBound(0)
						selectString += " AND Name Not like " + Chr(34) + CELProject.NO_CALIBRATION_CHANNEL(j) + Chr(34) + " "
					Next

				End If

				i = i + 1
			Next table

			If i > 0 Then
				selectString += "ORDER BY HMIAliasName"
			End If

			'*****************************************************************************************************************************************
			command = New OleDbCommand(selectString, connection)
			reader = command.ExecuteReader()

			While (reader.Read())
				'Populate the Calibration_FVName Array
				calibrationFVName.Add(New Calib_FVName(reader.Item("Name").ToString(), reader.Item("HMIAliasName").ToString(), reader.Item("BranchList").ToString().ToUpper()))
			End While
			reader.Close()


			'Close the connection to the TestSession Database
			connection.Close()

			'Try to order (will use the Public Overloads CompareTo function defined below)
			calibrationFVName.Sort()

			'Associate the array list to the FV Drop Down List
			FVName_List.DataSource = calibrationFVName
			FVName_List.DisplayMember = "Generic_Name"
			FVName_List.ValueMember = "FV_Name"
		End Sub

		'*******************************
		'* Initialize_SimulatedUnits() *
		'*******************************
		Private Sub Initialize_SimulatedUnits()
			Dim engineeringUnitsCollection As EngineeringUnitCollection = New EngineeringUnitCollection()
			engineeringUnitsCollection.es.Connection.Name = CELProject.DefaultConnectionName
			engineeringUnitsCollection.Query.Where(engineeringUnitsCollection.Query.Id >= 0)        ' Might not be the best way to select entire collection
			engineeringUnitsCollection.Query.Load()

			SimulatedUnits_List.DisplayMember = "DisplayName"
			SimulatedUnits_List.ValueMember = "DisplayName"
			SimulatedUnits_List.DataSource = engineeringUnitsCollection
			SimulatedUnits_List.DropDownStyle = ComboBoxStyle.DropDownList
		End Sub

		Private Sub Read_TestSession()

			'Query Database Real
			Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
			dacnRealChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
			FVName_List.ValueMember = "Branch_List"
			dacnRealChannelCollection.Query.es.QuerySource = FVName_List.SelectedValue 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
			dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
			dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
			FVName_List.ValueMember = "FV_Name"
			dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.Name = FVName_List.SelectedValue)
			dacnRealChannelCollection.Query.Load()


			If dacnRealChannelCollection.Count = 1 Then

				ValidateCsvRecord(dacnRealChannelCollection(0))

				Last_Calib_Date.Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).LastCalibDate)
				Accuracy.Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).MarginAccuracy)
				If (IsNothing(dacnRealChannelCollection(0).OEMTolerance) Or dacnRealChannelCollection(0).OEMTolerance = 0.0) Then
					OEM_Tolerance.Value = New KineticaRT.Values.Text("=NA()")
				Else
					OEM_Tolerance.Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).OEMTolerance)
				End If
				Units_Measured.Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).CalibrationMeasured)
				Units_Simulated.Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).UpToEngineeringUnitByEngUnitID.DisplayName)

				SimulatedUnits_List.SelectedIndex = SimulatedUnits_List.FindStringExact(Units_Simulated.Value.ToString)

				Comments.Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).Comments)
				Generic_Name.Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).HMIAliasName)
				Transducer_Range.Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).TransducerRange)
				Offset.Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).Coeff1)
				Slope.Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).Coeff2)
				Measured(0).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).ReadV1)
				Simulated(0).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).RealV1)
				Measured(1).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).ReadV2)
				Simulated(1).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).RealV2)
				Measured(2).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).ReadV3)
				Simulated(2).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).RealV3)
				Measured(3).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).ReadV4)
				Simulated(3).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).RealV4)
				Measured(4).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).ReadV5)
				Simulated(4).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).RealV5)
				Measured(5).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).ReadV6)
				Simulated(5).Value = New KineticaRT.Values.Text(dacnRealChannelCollection(0).RealV6)



			Else
				Select Case CELProject.Project.Number
					Case 12917
						MessageBox.Show("Calibration : Lecture du TestSession: Aucune information disponible pour le paramètre" + FVName_List.SelectedValue)
					Case Else
						MessageBox.Show("Calibration : ReadTestSession: record information not found for parameter " + FVName_List.SelectedValue)
				End Select
			End If

		End Sub
		Private Function ValidateCsvRecord(ByRef csvParameter As DacnTemplateReal) As Boolean
			ValidateCsvRecord = False

			If csvParameter.MarginAccuracy Is Nothing OrElse csvParameter.MarginAccuracy.Value = 0 Then
				Select Case CELProject.Project.Number
					Case 12917
						MessageBox.Show("Ce paramètre a une précision invalide", "Erreur Configuration CSV", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
					Case Else
						MessageBox.Show("This parameter has Invalid Margin Accuracy", "CSV Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
				End Select
			ElseIf csvParameter.EngUnit.Trim & "" = "" OrElse csvParameter.EngUnitID = 0 OrElse csvParameter.EngUnitID = 10 Then
				Select Case CELProject.Project.Number
					Case 12917
						MessageBox.Show("Ce paramètre a une unité invalide", "Erreur Configuration CSV", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
					Case Else
						MessageBox.Show("This parameter has Invalid EngUnit or EngUnitID", "CSV Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
				End Select
			ElseIf csvParameter.CalibrationMeasured.Trim & "" = "" Then
				Select Case CELProject.Project.Number
					Case 12917
						MessageBox.Show("Ce paramètre a une valeur de CalibrationMeasured invalide", "Erreur Configuration CSV", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
					Case Else
						MessageBox.Show("This parameter has Invalid CalibrationMeasured", "CSV Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
				End Select
			End If
		End Function

		Function Save_TestSession() As Boolean

			Save_TestSession = False

			'Query Database Real
			Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
			dacnRealChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
			FVName_List.ValueMember = "Branch_List"
			dacnRealChannelCollection.Query.es.QuerySource = FVName_List.SelectedValue 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
			dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
			dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
			FVName_List.ValueMember = "FV_Name"
			dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.Name = FVName_List.SelectedValue)
			dacnRealChannelCollection.Query.Load()

			If dacnRealChannelCollection.Count = 1 Then

				dacnRealChannelCollection(0).ChannelType = Channel_Type.Value.ToString()
				dacnRealChannelCollection(0).ChannelRange = Channel_Range.Value.ToString()
				dacnRealChannelCollection(0).CalibrationRange = Calibration_Range.Value.ToString()

				If Calibration_Temp.Value.ToString().ToUpper().IndexOf(" C") = -1 And
				   Calibration_Temp.Value.ToString().ToUpper().IndexOf(" F") = -1 And
				   Calibration_Temp.Value.ToString().ToUpper().IndexOf(" R") = -1 And
				   Calibration_Temp.Value.ToString().IndexOf("Enter") = -1 Then
					Calibration_Temp.Value = New KineticaRT.Values.Text(Calibration_Temp.Value.ToString() + " C")
				End If

				dacnRealChannelCollection(0).CalibrationTemp = Calibration_Temp.Value.ToString()

				If Calibration_Humidity.Value.ToString().IndexOf("%") = -1 And Calibration_Humidity.Value.ToString().IndexOf("Enter") = -1 Then
					Calibration_Humidity.Value = New KineticaRT.Values.Text(Calibration_Humidity.Value.ToString() + " %")
				End If

				dacnRealChannelCollection(0).CalibrationHumidity = Calibration_Humidity.Value.ToString()

				dacnRealChannelCollection(0).TCTransducerSN = TC_Transducer_SN.Value.ToString()

				dacnRealChannelCollection(0).Make1 = Make_List(0).Text
				dacnRealChannelCollection(0).Model1 = Model_List(0).Text
				dacnRealChannelCollection(0).SerialNumber1 = Serial_Number_List(0).Text
				dacnRealChannelCollection(0).CalibrationDueDate1 = Calibration_Due_Date(0).Value.ToString()

				dacnRealChannelCollection(0).Make2 = Make_List(1).Text
				dacnRealChannelCollection(0).Model2 = Model_List(1).Text
				dacnRealChannelCollection(0).SerialNumber2 = Serial_Number_List(1).Text
				dacnRealChannelCollection(0).CalibrationDueDate2 = Calibration_Due_Date(1).Value.ToString()

				dacnRealChannelCollection(0).Make3 = Make_List(2).Text
				dacnRealChannelCollection(0).Model3 = Model_List(2).Text
				dacnRealChannelCollection(0).SerialNumber3 = Serial_Number_List(2).Text
				dacnRealChannelCollection(0).CalibrationDueDate3 = Calibration_Due_Date(2).Value.ToString()

				dacnRealChannelCollection(0).Make4 = Make_List(3).Text
				dacnRealChannelCollection(0).Model4 = Model_List(3).Text
				dacnRealChannelCollection(0).SerialNumber4 = Serial_Number_List(3).Text
				dacnRealChannelCollection(0).CalibrationDueDate4 = Calibration_Due_Date(3).Value.ToString()

				If Len(Comments.Value.ToString()) < 255 Then
					dacnRealChannelCollection(0).Comments = Comments.Value.ToString()
				Else
					Select Case CELProject.Project.Number
						Case 12917
							System.Windows.Forms.MessageBox.Show("La quantité de caractères pour le commentaire est excédée")
						Case Else
							System.Windows.Forms.MessageBox.Show("Lenght of Comments field is too long")
					End Select
					Save_TestSession = False
				End If
				dacnRealChannelCollection(0).ChannelRange = Channel_Range.Value.ToString
				dacnRealChannelCollection(0).CalibratedBy = CalibratedBy_List.Text
				dacnRealChannelCollection(0).Assistant = Assistant_List.Text
				dacnRealChannelCollection(0).Approval = Approval_List.Text

				Try
					dacnRealChannelCollection.Save()
					Save_TestSession = True
				Catch ex As Exception
					Select Case CELProject.Project.Number
						Case 12917
							MessageBox.Show("Le sauvegarde ne peut être effectuée dans le TestSession pour le paramètre " + dacnRealChannelCollection(0).Name)
						Case Else
							MessageBox.Show("Cannot save record information in TestSession for parameter " + dacnRealChannelCollection(0).Name)
					End Select
				End Try

			Else
				Select Case CELProject.Project.Number
					Case 12917
						MessageBox.Show("Calibration : Sauvegarde du TestSession: Aucune information disponible pour le paramètre " + FVName_List.SelectedValue)
					Case Else
						MessageBox.Show("Calibration : SaveTestSession: record information not found for parameter " + FVName_List.SelectedValue)
				End Select
			End If

		End Function

		Private Sub Set_CCAS_Channel(ByVal Index As Integer)
			If Not (CCAS_Requested_Vib_Channel Is Nothing) Then
				CCAS_Requested_Vib_Channel.Value = New KineticaRT.Values.Double(Index)
			End If
		End Sub


		Private Sub SetSpeedChannel(ByVal Index As Integer)
			If Not (SCU_Speed_Channel Is Nothing) Then
				If Index = 1 Or Index = 6 Or Index = 11 Then
					SCU_Speed_Channel.Value = New KineticaRT.Values.Double(1)
				ElseIf Index = 2 Or Index = 7 Or Index = 12 Then
					SCU_Speed_Channel.Value = New KineticaRT.Values.Double(2)
				ElseIf Index = 3 Or Index = 8 Or Index = 13 Then
					SCU_Speed_Channel.Value = New KineticaRT.Values.Double(3)
				ElseIf Index = 4 Or Index = 9 Or Index = 14 Then
					SCU_Speed_Channel.Value = New KineticaRT.Values.Double(4)
				ElseIf Index = 5 Or Index = 10 Or Index = 15 Then
					SCU_Speed_Channel.Value = New KineticaRT.Values.Double(5)
				End If
			End If
		End Sub


		Private Sub FVName_List_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
			Try
				Dim i As Integer
				Dim Index As Integer
				Dim Prefix As String
				Dim StopIndex As Integer
				Dim Temp As String

				NewSelectedIO = True
				ItemSelected = True

				For i = 0 To 5
					PUsed(i) = 0
				Next


				'Get the prefix and the index of the selected IO
				FVName_List.ValueMember = "FV_Name"
				For i = 1 To Len(FVName_List.SelectedValue)
					Temp = Right(FVName_List.SelectedValue, i)
					If IsNumeric(Temp) = False Then
						StopIndex = i - 1
						i = Len(FVName_List.SelectedValue)
					End If
				Next

				Index = Right(FVName_List.SelectedValue, StopIndex)
				Prefix = Left(FVName_List.SelectedValue, Len(FVName_List.SelectedValue) - StopIndex)

				FVName_List.ValueMember = "Branch_List"
				If ((FVName_List.SelectedValue = "DACN1_SCU_IOMGR_REAL" And Prefix = "F") Or (FVName_List.SelectedValue = "DACN1_SCU_IOMGR_REAL" And Prefix = "VF")) Then
					SetSpeedChannel(Index)
				End If

				If (FVName_List.SelectedValue = "DACN1_SCU_IOMGR_REAL" And Prefix = "CCAS_VIB") Then
					Set_CCAS_Channel(Index)
				End If

				'Change Windows Cursor
				Calibration.Cursor = Cursors.WaitCursor

				Initialize_Calib_Window()

				Read_TestSession()
				FVName_List.ValueMember = "Branch_List"
				Cal_Start(Index, Prefix, FVName_List.SelectedValue)

				' Updated initial values to track changed field
				initial_LastCalibDate = Last_Calib_Date.Value.ToString
				initial_Accuracy = Accuracy.Value.ToDouble
				initial_UnitsSimulated = SimulatedUnits_List.Items.Item(SimulatedUnits_List.FindString(Units_Simulated.Value.ToString))
				initial_TransducerRange = Transducer_Range.Value.ToString
				initial_Comments = Comments.Value.ToString

			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: FVName_List_SelectedIndexChanged: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub


        '**********************
        '* OnCalibCloseTest() *
        '**********************
        Private Sub OnCLOSE_TEST_BTN(ByVal sender As KineticaRT.Channels.IChannel, ByVal newVal As KineticaRT.Values.Value)
			Try
				Dim confirmationQuestion, confirmationCaption, saveQuestion, saveCaption As String

				' add CheckForModifiedCalibInfo

				Select Case CELProject.Project.Number
					Case 12917
						confirmationQuestion = "Êtes-vous vraiment certain de vouloir fermer? "
						confirmationCaption = "Quitter la Calibration"
						saveQuestion = "Voulez-vous conserver vos modifications?"
						saveCaption = "Modifications non-sauvegardées"
					Case Else
						confirmationQuestion = "Do you really want to close?"
						confirmationCaption = "Close Calibration"
						saveQuestion = "Do you want to keep your modifications?"
						saveCaption = "Unsaved modifications"
				End Select

				If (CELProject.IsTrue(newVal)) Then
					' Ask to save changes before leaving
					If DialogResult.Yes = MessageBox.Show(confirmationQuestion, confirmationCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
						If CheckForModifiedCalibInfo() Then
							If DialogResult.Yes = MessageBox.Show(saveQuestion, saveCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
								If Not SaveCalibrationInformation() Then
									Return
								End If
							End If
						End If
						Calibration.Cursor = Cursors.WaitCursor

						CELDAS.ExcelApplication.Visible = False
						Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Double(0)

						Test_Mgr_Calib_Flag.Value = Values.False

						NewSelectedIO = False
						ItemSelected = False

						'Close the User Inputs Data
						Calibration_Input_Data.Visible = False
						Calibration_Input_Data.TopMost = False

						'Master Sending Command to Close DAS, Slave will be calling Close_HMI in OnTestSessionClosing
						CELProject.Test_Mgr_Cmd.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Cmd.Close)
						Try
							'access the function/information required
							Dim exportSoftware As New CEL.HMI.HardwareConfiguration
							'create the CsvFile into configured server
							If exportSoftware.ExportAllCsv() = True Then

								'upload the csv Files to the configured path from the qnx config in the config file.
								exportSoftware.FtpAllCsvUpload(CELProject.ExportPathName, _
														  CELProject.QNX.UploadPath, CELProject.QNX.IpAdress, CELProject.QNX.UserName, CELProject.QNX.Password)
							Else
								Debug.Assert(False, "Error From : CEL.HMI.Calibration_sequence: Export CSV ERROR")
							End If

							'\/\/\/\/\/ old method to create and export CSV file to qnx \/\/\/\/\/\/
							'Dim ExportStartInfo As New ProcessStartInfo
							'Dim ExportExecuting As New Process
							'With ExportStartInfo
							'.FileName = CELProject.Database.ExportCsvBatchFile
							'.WindowStyle = ProcessWindowStyle.Hidden
							'End With
							'ExportExecuting = Process.Start(ExportStartInfo)
							'/\/\/\/\/\ old method to create and export CSV file to qnx /\/\/\/\/\/\

						Catch ex As Exception
							MessageBox.Show("Calibration_Sequence: OnCalibCloseTest: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Calibration.Cursor = Cursors.Arrow
							Debug.Assert(False)
						End Try
					End If

                    Calibration.Cursor = Cursors.Arrow
                    sender.Value = KineticaRT.Values.Values.False

				End If

			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnCalibCloseTest: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub

		Private Sub OnCalibResizePanel(ByVal sender As IChannel, ByVal newVal As Value)
			Try
				If (CELProject.IsTrue(newVal)) Then
					'Shinked Size
					'Calibration Information Panel
					Calibration_Information_Panel.Location = New Point(40, 48)
					Calibration_Information_Panel.Size = New Point(460, 334)
					Frame3.Size = New Point(446, 70)
					FVName_List.Location = New Point(8, 26)
					PARAMETERS_TXT_DISPLAY.Location = New Point(24, 4)
					MEASURED_VALUE_DISPLAY.Location = New Point(304, 30)
					MEASURED_TXT_DISPLAY.Location = New Point(296, 4)
					CALIBRATION_BTN.Location = New Point(152, 24)
					'Calibration Controls Buttons
					ControlsFrame.Location = New Point(104, 384)
					ControlsFrame.Size = New Point(342, 150)
					'CHECK_REPORT_BTN.Location = New Point(4, 4)
					'CHECK_REPORT_BTN.Size = New Point(318, 30)
					NEW_REPORT_BTN.Location = New Point(4, 4)
					NEW_REPORT_BTN.Size = New Point(318, 30)
					SAVE_DATA_BTN.Location = New Point(4, 38)
					SAVE_DATA_BTN.Size = New Point(318, 30)
					'Calib Resize BTN
					CALIB_RESIZE_BTN.Location = New Point(4, 72)
					CALIB_RESIZE_BTN.Size = New Point(318, 30)
					btnClose.Location = New Point(4, 106)
					btnClose.Size = New Point(318, 30)
					'Calibration Panel
					Calibration_Panel.Location = New Point(520, 48)
					Calibration_Panel.Size = New Point(454, 436)
					'Press enter Label
					PressEnter_Label.Location = New Point(112, 584)
					'Calibration Panel Point 1
					Point_Frame(0).Location = New Point(4, 4)
					Point_Frame(0).Size = New Point(438, 62)
					Button2(0).Location = New Point(288, 4)
					Button2(0).Size = New Point(138, 22)
					Button3(0).Location = New Point(288, 32)
					Button3(0).Size = New Point(138, 22)
					Measured_Txt(0).Location = New Point(4, 4)
					Measured_Txt(0).Size = New Point(270, 22)
					Simulated_Txt(0).Location = New Point(4, 32)
					Simulated_Txt(0).Size = New Point(270, 22)
					Button1(0).Location = New Point(4, 4)
					Button1(0).Size = New Point(162, 22)
					'Calibration Panel Point 2
					Point_Frame(1).Location = New Point(4, 70)
					Point_Frame(1).Size = New Point(438, 62)
					Button2(1).Location = New Point(288, 4)
					Button2(1).Size = New Point(138, 22)
					Button3(1).Location = New Point(288, 32)
					Button3(1).Size = New Point(138, 22)
					Measured_Txt(1).Location = New Point(4, 4)
					Measured_Txt(1).Size = New Point(270, 22)
					Simulated_Txt(1).Location = New Point(4, 32)
					Simulated_Txt(1).Size = New Point(270, 22)
					Button1(1).Location = New Point(4, 4)
					Button1(1).Size = New Point(162, 22)
					'Calibration Panel Point 3
					Point_Frame(2).Location = New Point(4, 136)
					Point_Frame(2).Size = New Point(438, 62)
					Button2(2).Location = New Point(288, 4)
					Button2(2).Size = New Point(138, 22)
					Button3(2).Location = New Point(288, 32)
					Button3(2).Size = New Point(138, 22)
					Measured_Txt(2).Location = New Point(4, 4)
					Measured_Txt(2).Size = New Point(270, 22)
					Simulated_Txt(2).Location = New Point(4, 32)
					Simulated_Txt(2).Size = New Point(270, 22)
					Button1(2).Location = New Point(4, 4)
					Button1(2).Size = New Point(162, 22)
					'Calibration Panel Point 4
					Point_Frame(3).Location = New Point(4, 202)
					Point_Frame(3).Size = New Point(438, 62)
					Button2(3).Location = New Point(288, 4)
					Button2(3).Size = New Point(138, 22)
					Button3(3).Location = New Point(288, 32)
					Button3(3).Size = New Point(138, 22)
					Measured_Txt(3).Location = New Point(4, 4)
					Measured_Txt(3).Size = New Point(270, 22)
					Simulated_Txt(3).Location = New Point(4, 32)
					Simulated_Txt(3).Size = New Point(270, 22)
					Button1(3).Location = New Point(4, 4)
					Button1(3).Size = New Point(162, 22)
					'Calibration Panel Point 5
					Point_Frame(4).Location = New Point(4, 268)
					Point_Frame(4).Size = New Point(438, 62)
					Button2(4).Location = New Point(288, 4)
					Button2(4).Size = New Point(138, 22)
					Button3(4).Location = New Point(288, 32)
					Button3(4).Size = New Point(138, 22)
					Measured_Txt(4).Location = New Point(4, 4)
					Measured_Txt(4).Size = New Point(270, 22)
					Simulated_Txt(4).Location = New Point(4, 32)
					Simulated_Txt(4).Size = New Point(270, 22)
					Button1(4).Location = New Point(4, 4)
					Button1(4).Size = New Point(162, 22)
					'Calibration Panel Point 6
					Point_Frame(5).Location = New Point(4, 334)
					Point_Frame(5).Size = New Point(438, 62)
					Button2(5).Location = New Point(288, 4)
					Button2(5).Size = New Point(138, 22)
					Button3(5).Location = New Point(288, 32)
					Button3(5).Size = New Point(138, 22)
					Measured_Txt(5).Location = New Point(4, 4)
					Measured_Txt(5).Size = New Point(270, 22)
					Simulated_Txt(5).Location = New Point(4, 32)
					Simulated_Txt(5).Size = New Point(270, 22)
					Button1(5).Location = New Point(4, 4)
					Button1(5).Size = New Point(162, 22)
					'DAS CALIBRATION Window Display
					DAS_CALIB_DISPLAY.Location = New Point(306, 4)
					Picture2.Location = New Point(328, 8)
					'God mode label
					GodMode_Label.Location = New Point(728, 11)
				ElseIf (CELProject.IsFalse(newVal)) Then
					'Normal Size
					'Calibration Information Panel
					Calibration_Information_Panel.Location = New Point(160, 110)
					Calibration_Information_Panel.Size = New Point(460, 365)
					Frame3.Size = New Point(446, 105)
					FVName_List.Location = New Point(43, 26)
					PARAMETERS_TXT_DISPLAY.Location = New Point(56, 4)
					MEASURED_VALUE_DISPLAY.Location = New Point(272, 30)
					MEASURED_TXT_DISPLAY.Location = New Point(264, 4)
					CALIBRATION_BTN.Location = New Point(262, 58)
					'Calibration Controls Buttons
					ControlsFrame.Location = New Point(224, 488)
					ControlsFrame.Size = New Point(342, 214)
					'CHECK_REPORT_BTN.Location = New Point(4, 4)
					'CHECK_REPORT_BTN.Size = New Point(318, 45)
					NEW_REPORT_BTN.Location = New Point(4, 4)
					NEW_REPORT_BTN.Size = New Point(318, 45)
					SAVE_DATA_BTN.Location = New Point(4, 52)
					SAVE_DATA_BTN.Size = New Point(318, 45)
					'Calib Resize BTN
					CALIB_RESIZE_BTN.Location = New Point(4, 100)
					CALIB_RESIZE_BTN.Size = New Point(318, 45)
					btnClose.Location = New Point(4, 148)
					btnClose.Size = New Point(318, 45)
					'Calibration Panel
					Calibration_Panel.Location = New Point(632, 110)
					Calibration_Panel.Size = New Point(454, 592)
					'Press enter Label
					PressEnter_Label.Location = New Point(232, 784)
					'Calibration Panel Point 1
					Point_Frame(0).Location = New Point(4, 4)
					Point_Frame(0).Size = New Point(438, 88)
					Button2(0).Location = New Point(288, 4)
					Button2(0).Size = New Point(138, 34)
					Button3(0).Location = New Point(288, 42)
					Button3(0).Size = New Point(138, 34)
					Measured_Txt(0).Location = New Point(4, 17)
					Measured_Txt(0).Size = New Point(270, 22)
					Simulated_Txt(0).Location = New Point(4, 43)
					Simulated_Txt(0).Size = New Point(270, 22)
					Button1(0).Location = New Point(4, 16)
					Button1(0).Size = New Point(162, 24)
					'Calibration Panel Point 2
					Point_Frame(1).Location = New Point(4, 96)
					Point_Frame(1).Size = New Point(438, 88)
					Button2(1).Location = New Point(288, 4)
					Button2(1).Size = New Point(138, 34)
					Button3(1).Location = New Point(288, 42)
					Button3(1).Size = New Point(138, 34)
					Measured_Txt(1).Location = New Point(4, 17)
					Measured_Txt(1).Size = New Point(270, 22)
					Simulated_Txt(1).Location = New Point(4, 43)
					Simulated_Txt(1).Size = New Point(270, 22)
					Button1(1).Location = New Point(4, 16)
					Button1(1).Size = New Point(162, 24)
					'Calibration Panel Point 3
					Point_Frame(2).Location = New Point(4, 188)
					Point_Frame(2).Size = New Point(438, 88)
					Button2(2).Location = New Point(288, 4)
					Button2(2).Size = New Point(138, 34)
					Button3(2).Location = New Point(288, 42)
					Button3(2).Size = New Point(138, 34)
					Measured_Txt(2).Location = New Point(4, 17)
					Measured_Txt(2).Size = New Point(270, 22)
					Simulated_Txt(2).Location = New Point(4, 43)
					Simulated_Txt(2).Size = New Point(270, 22)
					Button1(2).Location = New Point(4, 16)
					Button1(2).Size = New Point(162, 24)
					'Calibration Panel Point 4
					Point_Frame(3).Location = New Point(4, 280)
					Point_Frame(3).Size = New Point(438, 88)
					Button2(3).Location = New Point(288, 4)
					Button2(3).Size = New Point(138, 34)
					Button3(3).Location = New Point(288, 42)
					Button3(3).Size = New Point(138, 34)
					Measured_Txt(3).Location = New Point(4, 17)
					Measured_Txt(3).Size = New Point(270, 22)
					Simulated_Txt(3).Location = New Point(4, 43)
					Simulated_Txt(3).Size = New Point(270, 22)
					Button1(3).Location = New Point(4, 16)
					Button1(3).Size = New Point(162, 24)
					'Calibration Panel Point 5
					Point_Frame(4).Location = New Point(4, 372)
					Point_Frame(4).Size = New Point(438, 88)
					Button2(4).Location = New Point(288, 4)
					Button2(4).Size = New Point(138, 34)
					Button3(4).Location = New Point(288, 42)
					Button3(4).Size = New Point(138, 34)
					Measured_Txt(4).Location = New Point(4, 17)
					Measured_Txt(4).Size = New Point(270, 22)
					Simulated_Txt(4).Location = New Point(4, 43)
					Simulated_Txt(4).Size = New Point(270, 22)
					Button1(4).Location = New Point(4, 16)
					Button1(4).Size = New Point(162, 24)
					'Calibration Panel Point 6
					Point_Frame(5).Location = New Point(4, 464)
					Point_Frame(5).Size = New Point(438, 88)
					Button2(5).Location = New Point(288, 4)
					Button2(5).Size = New Point(138, 34)
					Button3(5).Location = New Point(288, 42)
					Button3(5).Size = New Point(138, 34)
					Measured_Txt(5).Location = New Point(4, 17)
					Measured_Txt(5).Size = New Point(270, 22)
					Simulated_Txt(5).Location = New Point(4, 43)
					Simulated_Txt(5).Size = New Point(270, 22)
					Button1(5).Location = New Point(4, 16)
					Button1(5).Size = New Point(162, 24)
					'DAS CALIBRATION Window Display
					DAS_CALIB_DISPLAY.Location = New Point(440, 4)
					Picture2.Location = New Point(456, 8)
					'God mode label
					GodMode_Label.Location = New Point(556, 56)
				End If
			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnCalibCloseTest: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub

		Private Sub OnCalib_UserInputs_Cancel(ByVal sender As IChannel, ByVal newVal As Value)
			Try
				If (CELProject.IsTrue(newVal)) Then

					'Close the User Inputs Data
					Calibration_Input_Data.Visible = False
					Calibration_Input_Data.TopMost = False

					Calib_UserInputs_Cancel.Value = Values.False
				End If
			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnCalib_UserInputs_Cancel: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub


		Private Sub OnCalib_UserInputs_OK(ByVal sender As IChannel, ByVal newVal As Value)
			Dim columnIndex As Integer
			Dim currentSheet As String
			Dim excelSheets As Excel.Sheets
			Dim excelWorkbook As Excel.Workbook = Nothing
			Dim excelWorksheet As Excel.Worksheet
			Dim rowIndex As Integer
			Dim workbookPath As String
			Dim testCellText As String
			Dim selectString As String
			Dim connection As OleDbConnection
			Dim command As OleDbCommand
			Dim reader As OleDbDataReader
			Dim TestCellName As String = "Unknown"


			'The report has to be customize for the specific test cell.
			'The database has a table called TestCellIdentification that contain a data information
			'that has to be put in the footer

			' Check for modified Instrument Range (God mode)
			If (CELProject.IsTrue(newVal)) Then
				If initial_InstrumentRange <> Channel_Range.Value.ToString Then
					Dim saveQuestion As String = "Do you want to keep your modifications?"
					Dim saveCaption As String = "Unsaved Instrument Range"
					If DialogResult.No = MessageBox.Show(saveQuestion, saveCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
						' Put back initial value
						Channel_Range.Value = New KineticaRT.Values.Text(initial_InstrumentRange)
					Else
						CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Instrument range value saved", ToolTipIcon.Info)
					End If
				End If
			End If
			
			'Connect to the TestSession Database
			connection = New OleDbConnection(CELProject.Database.OleDbConnectionString)

			'Open the connection.
			Try
				connection.Open()
			Catch exception As System.Exception
				System.Diagnostics.Trace.WriteLine(exception.Message) 'Probably not found the file.
			End Try

			'select the table containing the data to extract
			selectString = "SELECT * FROM TestCellIdentification"

			command = New OleDbCommand(selectString, connection)
			reader = command.ExecuteReader()
			reader.Read()
			TestCellName = reader.Item("TestCelNumber").ToString()

			reader.Close()
			connection.Close()

			Try

				If (CELProject.IsTrue(newVal)) Then
					If Save_TestSession() = True Then
						'Close the User Inputs Data
						Calibration_Input_Data.Visible = False
						Calibration_Input_Data.TopMost = False

						'excelApp = New Excel.ApplicationClass()

						' The following code opens an existing workbook
						'New Calib Report
						'If ((Check_Report_Flag = False) And (Calib_Report_Flag = True)) Then
						workbookPath = System.IO.Path.GetFullPath(CELProject.Report.CalibrationReportTemplateFile)
						CalibrationFlag = True
						CheckFlag = False

						Dim TemplateDateBefore As Date = File.GetLastWriteTime(workbookPath)


						Try
							excelWorkbook = CELDAS.ExcelApplication.Workbooks.Open(workbookPath, 0, False, 5, "", "", False, Excel.XlPlatform.xlWindows, "", True, False, 0, True, False, False)
						Catch exception As System.Exception
							System.Diagnostics.Trace.WriteLine("Opening Excel: " + exception.Message)
							MessageBox.Show(exception.Message)
						End Try
						CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Generating report...", ToolTipIcon.Info)

						'excelApp.Visible = True  ' Makes Excel visible to the user.
						'CELDAS.ExcelApplication.Visible = True

						If (Not excelWorkbook Is Nothing) Then
							' The following gets the Worksheets collection
							excelSheets = excelWorkbook.Worksheets

							currentSheet = "Report"
							excelWorksheet = excelSheets.Item(currentSheet)

							excelWorkbook.Sheets(1).UnProtect(CELProject.Database.Password)

							'the footer is different depending of the test cell
							'TestCellText = "&8CELDAS™ 200" & vbLf & "T/C ID " & ValueFromDB
							If CELProject.Project.Number = 11894 Then
								testCellText = "&8CELDAS™ 200" & vbLf & CELProject.Project.Number & vbLf
							Else
								testCellText = "&8CELDAS™ 200" & vbLf & CELProject.Project.Number & vbLf & "Test Cell: " + TestCellName
							End If

							Try
								excelWorksheet.PageSetup.RightFooter = testCellText
							Catch
								'excelWorksheet.PageSetup plante lorsque le PC n'a pas d'imprimante d'intalle
							End Try

							'Title
							excelWorksheet.Cells(1, 4) = "" & CELProject.Project.Number & " CALIBRATION REPORT -- " & Generic_Name.Value.ToString()


							'------------------------------------------------------------------------------------

							'Customer
							excelWorksheet.Cells(3, 3) = CELProject.Project.Name

							'Equipement P/N
							excelWorksheet.Cells(4, 3) = CELProject.Project.Number

							'Description
							excelWorksheet.Cells(5, 3) = CELProject.Project.Description

							'ReferenceNumber
							excelWorksheet.Cells(6, 3) = CELProject.Project.ReferenceNumber

							'Channel Name
							FVName_List.ValueMember = "FV_Name"
							excelWorksheet.Cells(3, 6) = FVName_List.SelectedValue
							'Generic_Name.Value.ToString() contient la meme information?

							'Channel Type
							excelWorksheet.Cells(4, 6) = Channel_Type.Value.ToString()

							'Units
							excelWorksheet.Cells(5, 6) = Units_Simulated.Value.ToString()

							'Transducer Range
							excelWorksheet.Cells(5, 9) = Transducer_Range.Value.ToString()

							'Calibrated Range
							excelWorksheet.Cells(6, 9) = Calibration_Range.Value.ToString()

							rowIndex = 4
							columnIndex = 9

							Select Case CELProject.Project.Number
								Case 11813
								Case 15140
								Case 14105
								Case 18957
									FVName_List.ValueMember = "Branch_List"
									'Since we do not have strongback in this project we do not want to display it
									rowIndex += 1
									excelWorksheet.Cells(rowIndex, columnIndex) = " "
									columnIndex -= 1
									excelWorksheet.Cells(rowIndex, columnIndex) = " "

								Case Else
									'Strongback Support
									FVName_List.ValueMember = "Branch_List"
									If ((FVName_List.SelectedValue = "DACN1_POWERDNA_IOMGR_REAL_1") Or (FVName_List.SelectedValue = "DACN1_POWERDNA_IOMGR_REAL_2")) Then

										rowIndex = 6
										columnIndex = 6
										excelWorksheet.Cells(rowIndex, columnIndex) = Strongback_Number
										columnIndex -= 1
										excelWorksheet.Cells(rowIndex, columnIndex) = "Strongback:"
									Else
										rowIndex = 6
										columnIndex = 6
										excelWorksheet.Cells(rowIndex, columnIndex) = " "
										columnIndex -= 1
										excelWorksheet.Cells(rowIndex, columnIndex) = " "
									End If
							End Select

							'OEM Tolerance
							excelWorksheet.Cells(3, 9) = OEM_Tolerance_Text.Value.ToString()

							'Calibration Tolerance
							excelWorksheet.Cells(4, 9) = Margin_Accuracy.Value.ToString()

							'Date / Time
							'excelWorksheet.Cells(rowIndex, columnIndex) = Format(Now(), "MMMM dd,yyyy HH:mm:ss")


							Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
							dacnRealChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
							FVName_List.ValueMember = "Branch_List"
							dacnRealChannelCollection.Query.es.QuerySource = FVName_List.SelectedValue 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
							dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
							dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
							FVName_List.ValueMember = "FV_Name"
							dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.Name = FVName_List.SelectedValue)
							dacnRealChannelCollection.Query.Load()


                            Dim dateTimeFormat As String = "MMM dd,yyyy"
                            If dacnRealChannelCollection(0).LastCalibDate <> String.Empty Then
                                excelWorksheet.Cells(7, 6) = Format(System.Convert.ToDateTime(dacnRealChannelCollection(0).LastCalibDate), dateTimeFormat)
                            Else
                                excelWorksheet.Cells(7, 6) = String.Empty
                            End If

                            excelWorksheet.Cells(8, 6) = Format(Now(), dateTimeFormat)


                            ' Transducer S/N
                            excelWorksheet.Cells(7, 3) = TC_Transducer_SN.Value.ToString()

							' Calibration Humidity
							If Calibration_Humidity.Value.ToString().IndexOf("%") = -1 And Calibration_Humidity.Value.ToString().IndexOf("Enter") = -1 Then
								Calibration_Humidity.Value = New KineticaRT.Values.Text(Calibration_Humidity.Value.ToString() + " %")
							End If

							excelWorksheet.Cells(8, 9) = Calibration_Humidity.Value.ToString()

							excelWorksheet.Range("F6:F6").HorizontalAlignment = Excel.Constants.xlLeft

							' Calibration Temperature
							If Calibration_Temp.Value.ToString().ToUpper().IndexOf(" C") = -1 And
							   Calibration_Temp.Value.ToString().ToUpper().IndexOf(" F") = -1 And
							   Calibration_Temp.Value.ToString().ToUpper().IndexOf(" R") = -1 And
							   Calibration_Temp.Value.ToString().IndexOf("Enter") = -1 Then
								Calibration_Temp.Value = New KineticaRT.Values.Text(Calibration_Temp.Value.ToString() + " C")
							End If

							excelWorksheet.Cells(7, 9) = Calibration_Temp.Value.ToString()

							'Calibrator value (EngUnit) x-axis
							excelWorksheet.Cells(23, 10) = "(" & Me.Units_Simulated.Value.ToString() & ")"

							'Calibrated Deviation (EngUnit) y-axis
							excelWorksheet.Cells(9, 1) = "(" & Me.Units_Simulated.Value.ToString() & "(d))"

							'Calibration Value
							'New Calib Report
							'If ((Check_Report_Flag = False) And (Calib_Report_Flag = True)) Then
							'C1
							excelWorksheet.Cells(25, 3) = Cal_C(0).ToString()
							'C2
							excelWorksheet.Cells(25, 8) = Cal_C(1).ToString()
							'C3
							excelWorksheet.Cells(26, 3) = Offset.ToString()
							'C4	
							excelWorksheet.Cells(26, 8) = Slope.ToString()

							excelWorksheet.Cells(29, 2) = "value (" & Me.Units_Simulated.Value.ToString() & ")"

							'Raw value (UnitMeasured) ex. in VDC
							excelWorksheet.Cells(29, 3) = "value (" & Me.Units_Measured.Value.ToString() & ")"

							'Previous Calibrated Value (EngUnit)
							excelWorksheet.Cells(29, 4) = "value (" & Me.Units_Simulated.Value.ToString() & ")"

							'Calibrated value (EngUnit)
							excelWorksheet.Cells(29, 6) = "value (" & Me.Units_Simulated.Value.ToString() & ")"


							'Points
							'Point 1
							If PUsed(0) = 1 Then
								'Calibrator Value
								excelWorksheet.Cells(30, 2) = Simulated(0).ToString()
								'Raw Value
								excelWorksheet.Cells(30, 3) = Measured(0).ToString()
							Else
								'Calibrator Value
								excelWorksheet.Cells(30, 2) = ""
								'Raw Value
								excelWorksheet.Cells(30, 3) = ""
							End If
							'Point 2
							If PUsed(1) = 1 Then
								'Calibrator Value
								excelWorksheet.Cells(31, 2) = Simulated(1).ToString()
								'Raw Value
								excelWorksheet.Cells(31, 3) = Measured(1).ToString()
							Else
								'Calibrator Value
								excelWorksheet.Cells(31, 2) = ""
								'Raw Value
								excelWorksheet.Cells(31, 3) = ""
							End If
							'Point 3
							If PUsed(2) = 1 Then
								'Calibrator Value
								excelWorksheet.Cells(32, 2) = Simulated(2).ToString()
								'Raw Value
								excelWorksheet.Cells(32, 3) = Measured(2).ToString()
							Else
								'Calibrator Value
								excelWorksheet.Cells(32, 2) = ""
								'Raw Value
								excelWorksheet.Cells(32, 3) = ""
							End If
							'Point 4
							If PUsed(3) = 1 Then
								'Calibrator Value
								excelWorksheet.Cells(33, 2) = Simulated(3).ToString()
								'Raw Value
								excelWorksheet.Cells(33, 3) = Measured(3).ToString()
							Else
								'Calibrator Value
								excelWorksheet.Cells(33, 2) = ""
								'Raw Value
								excelWorksheet.Cells(33, 3) = ""
							End If
							'Point 5
							If PUsed(4) = 1 Then
								'Calibrator Value
								excelWorksheet.Cells(34, 2) = Simulated(4).ToString()
								'Raw Value
								excelWorksheet.Cells(34, 3) = Measured(4).ToString()
							Else
								'Calibrator Value
								excelWorksheet.Cells(34, 2) = ""
								'Raw Value
								excelWorksheet.Cells(34, 3) = ""
							End If
							'Point 6					
							If PUsed(5) = 1 Then
								'Calibrator Value
								excelWorksheet.Cells(35, 2) = Simulated(5).ToString()
								'Raw Value
								excelWorksheet.Cells(35, 3) = Measured(5).ToString()
							Else
								'Calibrator Value
								excelWorksheet.Cells(35, 2) = ""
								'Raw Value
								excelWorksheet.Cells(35, 3) = ""
							End If


							'Calibration Equipment
							'Equipment 1
							excelWorksheet.Cells(41, 1) = Make_List(0).Text
							excelWorksheet.Cells(41, 3) = Model_List(0).Text
							excelWorksheet.Cells(41, 6) = Serial_Number_List(0).Text
							excelWorksheet.Cells(41, 8) = Calibration_Due_Date(0).Value.ToString()

							'Equipment 2
							excelWorksheet.Cells(42, 1) = Make_List(1).Text
							excelWorksheet.Cells(42, 3) = Model_List(1).Text
							excelWorksheet.Cells(42, 6) = Serial_Number_List(1).Text
							excelWorksheet.Cells(42, 8) = Calibration_Due_Date(1).Value.ToString()

							'Equipment 3
							excelWorksheet.Cells(43, 1) = Make_List(2).Text
							excelWorksheet.Cells(43, 3) = Model_List(2).Text
							excelWorksheet.Cells(43, 6) = Serial_Number_List(2).Text
							excelWorksheet.Cells(43, 8) = Calibration_Due_Date(2).Value.ToString()

							'Equipment 4
							excelWorksheet.Cells(44, 1) = Make_List(3).Text
							excelWorksheet.Cells(44, 3) = Model_List(3).Text
							excelWorksheet.Cells(44, 6) = Serial_Number_List(3).Text
							excelWorksheet.Cells(44, 8) = Calibration_Due_Date(3).Value.ToString()


							'Footer
							'Comments
							If Not IsNothing(Comments) Then
								excelWorksheet.Cells(45, 2) = Comments.Value.ToString()
							End If
							'Calibrated By
							excelWorksheet.Cells(49, 2) = CalibratedBy_List.Text
							'Assistant
							excelWorksheet.Cells(51, 2) = Assistant_List.Text
							'Approval
							excelWorksheet.Cells(53, 2) = Approval_List.Text

							excelWorkbook.Sheets(1).Protect(CELProject.Database.Password)

							'Save the report
							excelWorkbook.Save()

							' test
							CELDAS.ExcelApplication.Visible = True

							Dim TemplateDateAfter As Date = File.GetLastWriteTime(workbookPath)

							If Date.Compare(TemplateDateBefore, TemplateDateAfter) = 0 Then
								MessageBox.Show("Unexpected error, cannot write file " & workbookPath & vbCrLf & "Usually it happens because the file is already opened, make sure that the file is not opened and then save it by clicking on the save icon from Excel ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
							End If

						End If
					End If
				End If

			Catch ex As Exception

				If (Not excelWorkbook Is Nothing) Then
					excelWorkbook.Close()
				End If

				If ex.HResult = &H800AC472 Then
					MessageBox.Show("OnCalib_UserInputs_OK: " & vbCr & _
									"Excel COM Service is currently busy. Please try again." & vbCr, _
									"Error", _
									MessageBoxButtons.OK, _
									MessageBoxIcon.Exclamation)
				Else
					MessageBox.Show("OnCalib_UserInputs_OK: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				End If

			Finally
				Calib_UserInputs_OK.Value = Values.False
			End Try

		End Sub


		'Private Sub OnCheck_Report(ByVal sender As IChannel, ByVal newVal As Value)
		'    If (CELProject.IsTrue(newVal)) Then
		'        Check_Report_Flag = True
		'        Calib_Report_Flag = False
		'        Initialize_Calib_InputData()
		'    End If

		'    Check_Report.Value = Values.False
		'End Sub


		Private Sub OnNew_Calib_Report(ByVal sender As IChannel, ByVal newVal As Value)
			Try
				If (CELProject.IsTrue(newVal)) Then
					'Check_Report_Flag = False
					'Calib_Report_Flag = True
					Initialize_Calib_InputData()
				End If

				New_Calib_Report.Value = Values.False
			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnNew_Calib_Report: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub


		Private Sub OnClear_Button(ByVal sender As IChannel, ByVal newVal As Value)
			Try
				Dim index As Integer

				index = System.Convert.ToInt16(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(sender.Name, 2), 1))

				If (CELProject.IsTrue(newVal)) Then
					If (Test_Mgr_Calib_Flag.Value.ToString() = Values.True.ToString()) Then
						Point_Frame(index - 1).BackColor = System.Drawing.Color.Black
						Simulated_Txt(index - 1).InputEnabled = True

						PUsed(index - 1) = 0
					End If
				End If

				Clear_Button(index - 1).Value = Values.False
			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnClear_Button: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub


		Private Sub OnEnter_Button(ByVal sender As IChannel, ByVal newVal As Value)
			Try
				Dim index As Integer

				index = System.Convert.ToInt16(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(sender.Name, 2), 1))

				If (CELProject.IsTrue(newVal)) Then
					If (Test_Mgr_Calib_Flag.Value.ToString() = Values.True.ToString() And PUsed(index - 1) = 0) Then
						Point_Frame(index - 1).BackColor = System.Drawing.Color.Blue
						Simulated_Txt(index - 1).InputEnabled = False
						Measured_Txt(index - 1).ValueColor = System.Drawing.Color.Black
						Simulated_Txt(index - 1).ValueColor = System.Drawing.Color.Black

						PUsed(index - 1) = 1
					End If

					Enter_Button(index - 1).Value = Values.False

				End If

			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnEnter_Button: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub


		Private Sub OnMeasured_Button(ByVal sender As IChannel, ByVal newVal As Value)
			Try
				Dim index As Integer

				index = System.Convert.ToInt16(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(sender.Name, 2), 1))

				If (CELProject.IsTrue(newVal)) Then
					If (Test_Mgr_Calib_Flag.Value.ToString() = Values.True.ToString() And PUsed(index - 1) = 0) Then
						Measured_Txt(index - 1).ValueColor = System.Drawing.Color.Black
						Measured(index - 1).Value = New KineticaRT.Values.Text(Calibration_Value.ToString())
					End If
				End If

				Measured_Button(index - 1).Value = Values.False
			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnMeasured_Button: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub


		Private Sub OnSimulated(ByVal sender As IChannel, ByVal newVal As Value)
			Try
				Dim index As Integer

				index = System.Convert.ToInt16(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(sender.Name, 2), 1))

				If (Test_Mgr_Calib_Flag.Value.ToString() = Values.True.ToString()) Then
					Simulated_Txt(index - 1).ValueColor = System.Drawing.Color.Black
				End If
			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnSimulated: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub


		Private Sub OnSave_Data(ByVal sender As IChannel, ByVal newVal As Value)

			Try


				Dim confirmationQuestion As String
				Select Case CELProject.Project.Number
					Case 12917
						confirmationQuestion = "Voulez-vous vraiment écraser l’ancienne calibration?"
					Case Else
						confirmationQuestion = "Do you want to save new calibration information?"
				End Select


				Dim i As Integer
				Dim pointUsedTot As Integer

				If (CELProject.IsTrue(newVal)) Then
					If (CalibrationFlag = True Or CheckFlag = True) Then
						If DialogResult.Yes = MessageBox.Show(confirmationQuestion, "Calibration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then

							'Query Database Real
							Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
							dacnRealChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
							FVName_List.ValueMember = "Branch_List"
							dacnRealChannelCollection.Query.es.QuerySource = FVName_List.SelectedValue 'Force l'entite a se replugger sur une autre table que celle par defaut si elle ont la meme structure
							dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
							dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ExportToCsv = True)
							FVName_List.ValueMember = "FV_Name"
							dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.Name = FVName_List.SelectedValue)
							dacnRealChannelCollection.Query.Load()

							If Not dacnRealChannelCollection.Count = 1 Then
								Select Case CELProject.Project.Number
									Case 12917
										MessageBox.Show("Calibration : Lecture du Test Session : Aucune information disponible pour le paramètre " + FVName_List.SelectedValue)
									Case Else
										MessageBox.Show("Calibration : Read TestSession: record information not found for parameter " + FVName_List.SelectedValue)
								End Select

							Else

								dacnRealChannelCollection(0).ReadV1 = 0
								dacnRealChannelCollection(0).RealV1 = 0
								dacnRealChannelCollection(0).ReadV2 = 0
								dacnRealChannelCollection(0).RealV2 = 0
								dacnRealChannelCollection(0).ReadV3 = 0
								dacnRealChannelCollection(0).RealV3 = 0
								dacnRealChannelCollection(0).ReadV4 = 0
								dacnRealChannelCollection(0).RealV4 = 0
								dacnRealChannelCollection(0).ReadV5 = 0
								dacnRealChannelCollection(0).RealV5 = 0
								dacnRealChannelCollection(0).ReadV6 = 0
								dacnRealChannelCollection(0).RealV6 = 0

								If PUsed(0) = 1 Then
									dacnRealChannelCollection(0).ReadV1 = Measured(0).ToString()
									dacnRealChannelCollection(0).RealV1 = Simulated(0).ToString()
								End If

								If PUsed(1) = 1 Then
									dacnRealChannelCollection(0).ReadV2 = Measured(1).ToString()
									dacnRealChannelCollection(0).RealV2 = Simulated(1).ToString()
								End If

								If PUsed(2) = 1 Then
									dacnRealChannelCollection(0).ReadV3 = Measured(2).ToString()
									dacnRealChannelCollection(0).RealV3 = Simulated(2).ToString()
								End If
								If PUsed(3) = 1 Then
									dacnRealChannelCollection(0).ReadV4 = Measured(3).ToString()
									dacnRealChannelCollection(0).RealV4 = Simulated(3).ToString()
								End If
								If PUsed(4) = 1 Then
									dacnRealChannelCollection(0).ReadV5 = Measured(4).ToString()
									dacnRealChannelCollection(0).RealV5 = Simulated(4).ToString()
								End If
								If PUsed(5) = 1 Then
									dacnRealChannelCollection(0).ReadV6 = Measured(5).ToString()
									dacnRealChannelCollection(0).RealV6 = Simulated(5).ToString()
								End If

								dacnRealChannelCollection(0).NewCalibDate = Now().ToString("yyyy\/MM\/dd")

							End If


							pointUsedTot = 0
							For i = 0 To 5
								pointUsedTot += PUsed(i)
							Next

							If pointUsedTot >= 2 Then

								dacnRealChannelCollection.Save()

								If CalibrationFlag = True Then

									dacnRealChannelCollection(0).PolyDegree = Cal_Degree.ToString()
									dacnRealChannelCollection(0).NotYetApprovedCoeff1 = Cal_C(0).ToString()
									dacnRealChannelCollection(0).NotYetApprovedCoeff2 = Cal_C(1).ToString()
									dacnRealChannelCollection(0).NotYetApprovedCoeff3 = Cal_C(2).ToString()
									dacnRealChannelCollection(0).NotYetApprovedCoeff4 = Cal_C(3).ToString()
									dacnRealChannelCollection(0).NotYetApprovedCoeff5 = Cal_C(4).ToString()
									dacnRealChannelCollection(0).NotYetApprovedCoeff6 = Cal_C(5).ToString()
									dacnRealChannelCollection(0).WaitingForApproval = True

									dacnRealChannelCollection.Save()

									'Copy Flag
									If Directory.Exists(CELProject.Report.CalibrationReportOutputDirectory) = False Then
										'Create the Directory
										Directory.CreateDirectory(CELProject.Report.CalibrationReportOutputDirectory)
									End If

									If ((FVName_List.SelectedValue = "DACN1_POWERDNA_IOMGR_REAL_1") Or (FVName_List.SelectedValue = "DACN1_POWERDNA_IOMGR_REAL_2")) Then
										FVName_List.ValueMember = "FV_Name"

										Select Case CELProject.Project.Number
											Case 11813
												'Since we do not have strongback in this project we do not want to display it
												File.Copy(CELProject.Report.CalibrationReportTemplateFile, CELProject.Report.CalibrationReportOutputDirectory + "\Report Calibration " + FVName_List.SelectedValue + " " + Format(Now, "yyyy MM dd HH,mm,ss") + ".xlsx")
											Case Else
												File.Copy(CELProject.Report.CalibrationReportTemplateFile, CELProject.Report.CalibrationReportOutputDirectory + "\Report Calibration " + FVName_List.SelectedValue + " SB_" + Strongback_Number.ToString() + " " + Format(Now, "yyyy MM dd HH,mm,ss") + ".xlsx")
										End Select

									Else
										FVName_List.ValueMember = "FV_Name"
										File.Copy(CELProject.Report.CalibrationReportTemplateFile, CELProject.Report.CalibrationReportOutputDirectory + "\Report Calibration " + FVName_List.SelectedValue + " " + Format(Now, "yyyy MM dd HH,mm,ss") + ".xlsx")
									End If

								ElseIf CheckFlag = True Then
									If Directory.Exists(CELProject.Report.CheckReportOutputDirectory) = False Then
										'Create the Directory
										Directory.CreateDirectory(CELProject.Report.CheckReportOutputDirectory)
									End If

									FVName_List.ValueMember = "Branch_List"
									If ((FVName_List.SelectedValue = "DACN1_POWERDNA_IOMGR_REAL_1") Or (FVName_List.SelectedValue = "DACN1_POWERDNA_IOMGR_REAL_2")) Then
										FVName_List.ValueMember = "FV_Name"

										Select Case CELProject.Project.Number
											Case 11813
												'Since we do not have strongback in this project we do not want to display it
												File.Copy(CELProject.Report.CheckReportTemplateFile, CELProject.Report.CheckReportOutputDirectory + "\Report Check " + FVName_List.SelectedValue + " " + Format(Now, "yyyy MM dd HH,mm,ss") + ".xlsx")
											Case Else
												File.Copy(CELProject.Report.CheckReportTemplateFile, CELProject.Report.CheckReportOutputDirectory + "\Report Check " + FVName_List.SelectedValue + " SB_" + Strongback_Number.ToString() + " " + Format(Now, "yyyy MM dd HH,mm,ss") + ".xlsx")
										End Select

									Else
										FVName_List.ValueMember = "FV_Name"
										File.Copy(CELProject.Report.CheckReportTemplateFile, CELProject.Report.CheckReportOutputDirectory + "\Report Check " + FVName_List.SelectedValue + " " + Format(Now, "yyyy MM dd HH,mm,ss") + ".xlsx")
									End If
								End If


								'Initialisation of PUsed Array
								PUsed(0) = 0
								PUsed(1) = 0
								PUsed(2) = 0
								PUsed(3) = 0
								PUsed(4) = 0
								PUsed(5) = 0


								CalibrationFlag = False
								CheckFlag = False

								'Update QNX
								Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Sub_Cmd.Xfer_Coeff)

								Initialize_Calib_Window()

								Read_TestSession()

								Test_Mgr_Calib_Flag.Value = Values.False
							Else
								Select Case CELProject.Project.Number
									Case 12917
										MessageBox.Show("Erreur: Un minimum de deux points est requis", "Calibration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
									Case Else
										MessageBox.Show("Error: Minimum of two points is required", "Calibration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								End Select

							End If
						End If
					Else
						Select Case CELProject.Project.Number
							Case 12917
								MessageBox.Show("Erreur: La calibration doit être effectuée en premier lieu", "Calibration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Case Else
								MessageBox.Show("Error: Calibration must be done first", "Calibration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						End Select
					End If
					Save_Data.Value = Values.False
				End If


			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnSave_Data: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub
		Private Sub Validate_Approved_Parameters()
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
				Dim selectBox As DataColumn

				Dim provider As CultureInfo = CultureInfo.InvariantCulture
				Dim nbrTable As Integer = CELProject.CSV_NumericTables.GetUpperBound(0)
				Dim channelCounter As Integer = 0

				dt = New DataTable()
				idColumn = New DataColumn("ID", Type.GetType("System.Int32"))
				nameColumn = New DataColumn("Name", Type.GetType("System.String"))
				descriptionColumn = New DataColumn("Description", Type.GetType("System.String"))
				dateColumn = New DataColumn("Date", Type.GetType("System.String"))
				selectBox = New DataColumn("OK", Type.GetType("System.String"))

				dt.Columns.Add(selectBox)
				dt.Columns.Add(idColumn)
				dt.Columns.Add(nameColumn)
				dt.Columns.Add(descriptionColumn)
				dt.Columns.Add(dateColumn)

				Dim k As Integer = 0
				Dim i As Integer
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
						If channel.WaitingForApproval = True Then
							channelCounter += 1

							dr = dt.NewRow()
							k += 1
							dr("OK") = ""
							dr("ID") = k
							dr("Name") = channel.Name
							dr("Description") = "Parameter is waiting for approval"
							If channel.NewCalibDate = "" Then
								dr("Date") = "N/A"
							Else
								dr("Date") = channel.NewCalibDate.ToString
							End If


							dt.Rows.Add(dr)
						End If
					Next
				Next i
				If channelCounter > 0 Then

					ds.Tables.Add(dt)
					Dim columnsWith(0 To 4) As Integer
					columnsWith(0) = 40
					columnsWith(1) = 70
					columnsWith(2) = 100
					columnsWith(3) = 250
					columnsWith(4) = 160
					If CELProject.Project.Number = 12917 Then
						Dim frmResults As New CELResultListViewFormApprove("Approbation des paramètres de Calibration", "La calibration de certains paramètres doivent être approuvés.", ds, columnsWith)
						frmResults.Show()
					Else
						Dim frmResults As New CELResultListViewFormApprove("Approval", "The calibration of some parameters need to be approved.", ds, columnsWith)
						frmResults.Show()
					End If
				End If
			Catch ex As System.Data.OleDb.OleDbException
                Debug.Assert(False, "Error: OpenTest_Sequence.Validate_Approved_Parameters: Could not properly connect to database:", ex.Message)
            Catch ex As Exception
                Debug.Assert(False, "OpenTest_Sequence.Validate_Approved_Parameters: The table name is:" & tableName & " and the channel name is: " & channelName, ex.Message)
                MessageBox.Show("OpenTest_Sequence.Validate_Approved_Parameters: This table, " & channelName & "is expired. Error message: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

		End Sub

		Private Sub OnTest_Mgr_Calib_Flag(ByVal sender As IChannel, ByVal newVal As Value)
			Try
				If ItemSelected = True Then
					If (Test_Mgr_Calib_Flag.Value.ToString() = Values.True.ToString()) Then
						Cal_Degree.Value = New KineticaRT.Values.Double(1)
						Cal_C(0).Value = New KineticaRT.Values.Double(0)
						Cal_C(1).Value = New KineticaRT.Values.Double(1)
						Cal_C(2).Value = New KineticaRT.Values.Double(0)
						Cal_C(3).Value = New KineticaRT.Values.Double(0)
						Cal_C(4).Value = New KineticaRT.Values.Double(0)
						Cal_C(5).Value = New KineticaRT.Values.Double(0)

						SetSimulatedTxtInputEnabled(True)

						Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Sub_Cmd.Xfer_Coeff)
					Else
						Cal_Degree.Value = New KineticaRT.Values.Double(1)
						Cal_C(0).Value = New KineticaRT.Values.Double(Offset.ToString())
						Cal_C(1).Value = New KineticaRT.Values.Double(Slope.ToString())
						Cal_C(2).Value = New KineticaRT.Values.Double(0)
						Cal_C(3).Value = New KineticaRT.Values.Double(0)
						Cal_C(4).Value = New KineticaRT.Values.Double(0)
						Cal_C(5).Value = New KineticaRT.Values.Double(0)

						SetSimulatedTxtInputEnabled(False)

						Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Sub_Cmd.Xfer_Coeff)
					End If
				Else
					Test_Mgr_Calib_Flag.Value = Values.False
				End If
			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnTest_Mgr_Calib_Flag: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub
		'Use Delegate for Cross Thread Operation Not Valid for control "Calibration"
		Private Delegate Sub SetSimulatedTxtInputEnabledDelegate(ByVal value As Boolean)
		Private Sub SetSimulatedTxtInputEnabled(ByVal value As Boolean)

			If (Me.Simulated_Txt(0).InvokeRequired) Then
				Me.Simulated_Txt(0).BeginInvoke(New SetSimulatedTxtInputEnabledDelegate(AddressOf Me.SetSimulatedTxtInputEnabled), value)
			Else
				Simulated_Txt(0).InputEnabled = value
				Simulated_Txt(1).InputEnabled = value
				Simulated_Txt(2).InputEnabled = value
				Simulated_Txt(3).InputEnabled = value
				Simulated_Txt(4).InputEnabled = value
				Simulated_Txt(5).InputEnabled = value
			End If
		End Sub

        '***********************
        '* OnSave_Calib_Info() *
        '***********************
        Private Sub OnSave_Calib_Info(ByVal sender As KineticaRT.Channels.IChannel, ByVal newVal As KineticaRT.Values.Value)
            If CELProject.IsTrue(newVal) Then
                SaveCalibrationInformation()
            End If
            If sender.Equals(Save_Calib_Info) Then
                sender.Value = KineticaRT.Values.Values.False
            End If
        End Sub

        '*************************
        '* onClick_FVName_List() *
        '*************************
        Private Sub onClick_FVName_List()

            ' Ask if user wants to save changes if changes were done, before changing FVName
            If CheckForModifiedCalibInfo() Then

                Dim saveQuestion, saveCaption As String
                Select Case CELProject.Project.Number
                    Case 12917
                        saveQuestion = "Voulez-vous conserver vos modifications?"
                        saveCaption = "Modifications non-sauvegardées"
                    Case Else
                        saveQuestion = "Do you want to keep your modifications?"
                        saveCaption = "Unsaved modifications"
                End Select

                ' Save calibration info on Yes, reset values on No, do nothing on cancel
                Dim result As DialogResult = MessageBox.Show(saveQuestion, saveCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                Select Case result
                    Case DialogResult.Yes
                        SaveCalibrationInformation()
                    Case DialogResult.No
                        Last_Calib_Date.Value = New KineticaRT.Values.Text(initial_LastCalibDate)
                        Accuracy.Value = New KineticaRT.Values.Double(initial_Accuracy)
                        SimulatedUnits_List.SelectedItem = initial_UnitsSimulated
                        Transducer_Range.Value = New KineticaRT.Values.Text(initial_TransducerRange)
                        Comments.Value = New KineticaRT.Values.Text(initial_Comments)
                End Select
            End If
        End Sub

        '*******************************
        '* CheckForModifiedCalibInfo() *
        '*******************************
        Private Function CheckForModifiedCalibInfo() As Boolean
            If initial_LastCalibDate <> Last_Calib_Date.Value.ToString Then
                Return True
            ElseIf initial_Accuracy <> Accuracy.Value.ToDouble Then
                Return True
            ElseIf Not initial_UnitsSimulated.Equals(SimulatedUnits_List.SelectedItem) Then
                Return True
            ElseIf initial_TransducerRange <> Transducer_Range.Value.ToString Then
                Return True
            ElseIf initial_Comments <> Comments.Value.ToString Then
                Return True
            Else
                Return False
            End If
        End Function

        '********************************
        '* SaveCalibrationInformation() *
        '********************************
        Private Function SaveCalibrationInformation() As Boolean
			Try
                'esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                Dim dacnRealChannelCollection As DacnTemplateRealCollection = New DacnTemplateRealCollection()
                dacnRealChannelCollection.es.Connection.Name = CELProject.DefaultConnectionName
                FVName_List.ValueMember = "Branch_List"
                dacnRealChannelCollection.Query.es.QuerySource = FVName_List.SelectedValue
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.ProjectID = CELProject.Project.Number)
                FVName_List.ValueMember = "FV_Name"
                dacnRealChannelCollection.Query.Where(dacnRealChannelCollection.Query.Name = FVName_List.SelectedValue)
                dacnRealChannelCollection.Query.Load()
                FVName_List.DisplayMember = "Generic_Name"

                If dacnRealChannelCollection.Count = 1 Then
                    ' Check date format
                    If System.DateTime.TryParseExact(Last_Calib_Date.Value.ToString, "yyyy/mm/dd", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, Nothing) Then
                        dacnRealChannelCollection(0).LastCalibDate = Last_Calib_Date.Value.ToString
                    Else
                        MessageBox.Show("Please enter date in the YYYY/MM/DD format", "Invalid date", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        LAST_CALIB_DISPLAY.Focus()
                        Return False
                    End If

                    ' Save calibration data
                    dacnRealChannelCollection(0).MarginAccuracy = Accuracy.Value.ToDouble
                    dacnRealChannelCollection(0).UpToEngineeringUnitByEngUnitID = SimulatedUnits_List.SelectedItem
                    dacnRealChannelCollection(0).TransducerRange = Transducer_Range.Value.ToString
                    dacnRealChannelCollection(0).Comments = Comments.Value.ToString
                    dacnRealChannelCollection.Save()

                    ' Update initial values
                    initial_LastCalibDate = Last_Calib_Date.Value.ToString
                    initial_Accuracy = Accuracy.Value.ToDouble
                    initial_UnitsSimulated = SimulatedUnits_List.SelectedItem
                    initial_TransducerRange = Transducer_Range.Value.ToString
                    initial_Comments = Comments.Value.ToString

                    CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Calibration information saved", ToolTipIcon.Info)
                    Return True
                Else
                    CELDAS.NotifyIcon.ShowBalloonTip(3000, CELProject.TechnologyName, "Failed to load", ToolTipIcon.Error)
                    Return False
                End If
            Catch ex As Exception
                MessageBox.Show("Calibration_Sequence: SaveCalibrationInformation: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End Try
        End Function

        '**************************
        '* OnTestSessionRunning() *
        '**************************
        Protected Overrides Sub OnTestSessionRunning(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)

            Dim i As Integer
            Try
                Validate_Approved_Parameters()

                MyBase.OnTestSessionRunning(newStatus)

                If CELProject.EngineModel = "DAS CALIBRATION" And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

					'Verify the connection to the channels
					If Init_Channels() = False Then
						MessageBox.Show(Me.GetType.FullName.ToString() + ".OnTestSessionRunning: missing channels")
						Return
					End If

					'Initialize Channels Value
					Init_Channels_Value()

					'Initialize Simulated Units ComboBox (easier to do before the FVName_List.SelectedIndexChanged handler)
					Initialize_SimulatedUnits()
					SimulatedUnits_List.Visible = False

					' Connect our event handler(s). 
					AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN
					AddHandler Calib_Resize_Panel.ValueChanged, AddressOf OnCalibResizePanel
					AddHandler Calib_UserInputs_Cancel.ValueChanged, AddressOf OnCalib_UserInputs_Cancel
					AddHandler Calib_UserInputs_OK.ValueChanged, AddressOf OnCalib_UserInputs_OK
					'AddHandler Check_Report.ValueChanged, AddressOf OnCheck_Report
					AddHandler FVName_List.SelectedIndexChanged, AddressOf FVName_List_SelectedIndexChanged
					AddHandler New_Calib_Report.ValueChanged, AddressOf OnNew_Calib_Report
					For i = 0 To Clear_Button.GetUpperBound(0) - 1
						AddHandler Clear_Button(i).ValueChanged, AddressOf OnClear_Button
						AddHandler Enter_Button(i).ValueChanged, AddressOf OnEnter_Button
						AddHandler Measured_Button(i).ValueChanged, AddressOf OnMeasured_Button
						AddHandler Simulated(i).ValueChanged, AddressOf OnSimulated
					Next
					AddHandler Save_Data.ValueChanged, AddressOf OnSave_Data
					AddHandler Test_Mgr_Calib_Flag.ValueChanged, AddressOf OnTest_Mgr_Calib_Flag
					AddHandler Test_Mgr_Sub_Status.ValueChanged, AddressOf OnTestMgrSubStatus

                    Initialize_Calibration()

                    initial_LastCalibDate = Last_Calib_Date.Value.ToString
                    initial_Accuracy = Accuracy.Value.ToDouble
                    initial_UnitsSimulated = SimulatedUnits_List.Items.Item(SimulatedUnits_List.FindString(Units_Simulated.Value.ToString))
                    initial_TransducerRange = Transducer_Range.Value.ToString
                    initial_Comments = Comments.Value.ToString

                    ' Validation of password for poweruser mode
                    If CELProject.CurrentUser.SecurityLevelID >= 4 Then
                        AddHandler Save_Calib_Info.ValueChanged, AddressOf OnSave_Calib_Info
                        AddHandler FVName_List.Click, AddressOf onClick_FVName_List
                        GodMode_Label.Visible = True
                        SAVE_CALIB_INFO_BTN.Visible = True
                        PressEnter_Label.Visible = True
                        SimulatedUnits_List.Visible = True
                        SimulatedUnits_List.BringToFront()
                        LAST_CALIB_DISPLAY.InputEnabled = True
                        ACCURACY_DISPLAY.InputEnabled = True
                        TRANSDUCER_RANGE_DISPLAY.InputEnabled = True
                        COMMENTS_DISPLAY.InputEnabled = True
                        InstrumentRange_Display.InputEnabled = True
                        InstrumentRange_Display.BackColor = Color.White
                        InstrumentRange_Display.ForeColor = Color.Black
                        InstrumentRange_Display.BorderType = Controls.BorderType.Indented
                        InstrumentRange_Display.BorderColor = Color.Blue
                    Else
                        GodMode_Label.Visible = False
                        SAVE_CALIB_INFO_BTN.Visible = False
                        PressEnter_Label.Visible = False
                        SimulatedUnits_List.Visible = False
                        SIMULATED_VALUE_DISPLAY.BringToFront()
                        LAST_CALIB_DISPLAY.InputEnabled = False
                        ACCURACY_DISPLAY.InputEnabled = False
                        TRANSDUCER_RANGE_DISPLAY.InputEnabled = False
                        COMMENTS_DISPLAY.InputEnabled = False
                        InstrumentRange_Display.InputEnabled = False
                        InstrumentRange_Display.BackColor = Color.Black
                        InstrumentRange_Display.ForeColor = Color.White
                        InstrumentRange_Display.BorderType = Controls.BorderType.None
                    End If

                    'Change Windows Cursor
                    Calibration.Cursor = Cursors.Arrow

					' Fait juste une fois lors de l'ouverture du systeme. Par la suite on garde la valeur entree par l'usager
					Calibration_Temp.Value = New KineticaRT.Values.Text("Enter a value and unit")
					Calibration_Humidity.Value = New KineticaRT.Values.Text("Enter a value and unit")

				End If

			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnTestSessionRunning: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub

        Protected Overrides Sub OnTestSessionClosing(ByVal newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
            Dim i As Integer
            Try

                If CELProject.EngineModel = "DAS CALIBRATION" And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

                    RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf OnCLOSE_TEST_BTN
                    RemoveHandler Calib_Resize_Panel.ValueChanged, AddressOf OnCalibResizePanel
                    RemoveHandler Calib_UserInputs_Cancel.ValueChanged, AddressOf OnCalib_UserInputs_Cancel
                    RemoveHandler Calib_UserInputs_OK.ValueChanged, AddressOf OnCalib_UserInputs_OK
                    'RemoveHandler Check_Report.ValueChanged, AddressOf OnCheck_Report
                    RemoveHandler FVName_List.SelectedIndexChanged, AddressOf FVName_List_SelectedIndexChanged
                    RemoveHandler New_Calib_Report.ValueChanged, AddressOf OnNew_Calib_Report
                    For i = 0 To Clear_Button.GetUpperBound(0) - 1
                        RemoveHandler Clear_Button(i).ValueChanged, AddressOf OnClear_Button
                        RemoveHandler Enter_Button(i).ValueChanged, AddressOf OnEnter_Button
                        RemoveHandler Measured_Button(i).ValueChanged, AddressOf OnMeasured_Button
                        RemoveHandler Simulated(i).ValueChanged, AddressOf OnSimulated
                    Next
                    RemoveHandler Save_Data.ValueChanged, AddressOf OnSave_Data
                    RemoveHandler Test_Mgr_Calib_Flag.ValueChanged, AddressOf OnTest_Mgr_Calib_Flag
                    RemoveHandler Test_Mgr_Sub_Status.ValueChanged, AddressOf OnTestMgrSubStatus

                    RemoveHandler Save_Calib_Info.ValueChanged, AddressOf OnSave_Calib_Info

                    Dim _onClick_FvName_List As EventHandler = AddressOf onClick_FVName_List
                    RemoveHandler FVName_List.Click, _onClick_FvName_List

                    GodMode_Label.Visible = False
                    SAVE_CALIB_INFO_BTN.Visible = False
                    PressEnter_Label.Visible = False
                    SimulatedUnits_List.Visible = False
                    SIMULATED_VALUE_DISPLAY.BringToFront()
                    LAST_CALIB_DISPLAY.InputEnabled = False
                    ACCURACY_DISPLAY.InputEnabled = False
                    TRANSDUCER_RANGE_DISPLAY.InputEnabled = False
                    COMMENTS_DISPLAY.InputEnabled = False
                    InstrumentRange_Display.InputEnabled = False
                    InstrumentRange_Display.BackColor = Color.Black
                    InstrumentRange_Display.ForeColor = Color.White
                    InstrumentRange_Display.BorderType = Controls.BorderType.None
                End If
            Catch ex As Exception

            End Try

        End Sub


        Protected Overrides Sub OnTestSessionOpened(newStatus As CEL.KineticaRT.Sequence.eTestMgrStatus)
			Try
				MyBase.OnTestSessionOpened(newStatus)

			Catch ex As Exception

				MessageBox.Show("Calibration_Sequence.OnTestSessionOpened: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

			End Try
		End Sub

		Private Sub OnTestMgrSubStatus(ByVal sender As IChannel, ByVal newVal As Value)
			Try

				If CELProject.EngineModel = "DAS CALIBRATION" And CELProject.HierarchyType = CELDAS.eHierarchyType.Master Then

					Select Case newVal.ToString()
						Case CELDAS.eTest_Mgr_Sub_Status.Coeff_Xfered
							Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Sub_Cmd.Calib)
							'BUG BUG Certaines fois le curseur reste avec le sablier simplement pcq la commande envoye 
							'au das d aller en mode calibration n,est pas recu. Alors le HMI reste en hold jusqu a ce que
							'L evenement se produise  (Case eTest_Mgr_Sub_Status.Calibrating) mais ne se produit jamais

						Case CELDAS.eTest_Mgr_Sub_Status.Calibrating
							'Change Windows Cursor
							SetCalibrationCursor(Cursors.Arrow)
						Case CELDAS.eTest_Mgr_Sub_Status.Speed_Channel_Requested
							Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Sub_Cmd.Speed_Channel_Set)
						Case CELDAS.eTest_Mgr_Sub_Status.Speed_Channel_Forced
							Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Sub_Cmd.Xfer_Coeff)
						Case CELDAS.eTest_Mgr_Sub_Status.CCAS_Setup_Done
							Test_Mgr_Sub_Command.Value = New KineticaRT.Values.Text(CELDAS.eTest_Mgr_Sub_Cmd.Xfer_Coeff)
						Case CELDAS.eTest_Mgr_Sub_Status.Coeff_Xfer_Failed
							Select Case CELProject.Project.Number
								Case 12917
									MessageBox.Show("Erreur d'opération: Incapable de transférer les coefficients du canal en cours." + vbCrLf + "SVP valider que les fichiers CSV sur la station QNX sont à jour", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
								Case Else
									MessageBox.Show("Operation error: Unable to transfer current channel's coefficient." + vbCrLf + "Please validate that CSV files on QNX station are up to date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
							End Select
					End Select

				End If

			Catch ex As Exception
				MessageBox.Show("Calibration_Sequence: OnTestMgrSubStatus: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End Try
		End Sub

		'Use Delegate for Cross Thread Operation Not Valid for control "Calibration"
		Private Delegate Sub SetCalibrationCursorDelegate(ByVal cursor As Cursor)
		Private Sub SetCalibrationCursor(ByVal cursor As Cursor)

			If (Me.Calibration.InvokeRequired) Then
				Me.Calibration.BeginInvoke(New SetCalibrationCursorDelegate(AddressOf Me.SetCalibrationCursor), cursor)
			Else
				Me.Calibration.Cursor = cursor
			End If
		End Sub


		'********************************************************************
		'* Private Members **************************************************
		'********************************************************************

		'************
		'* Channels *
		'************
		Private Accuracy As KineticaRT.Channels.NumericChannel
		Private Cal_Branch As KineticaRT.Servers.OpcClient.TextItem
		Private Cal_C(6) As KineticaRT.Servers.OpcClient.Item
		Private Cal_Degree As KineticaRT.Servers.OpcClient.Item
		Private Cal_Index As KineticaRT.Servers.OpcClient.Item
		Private Cal_Prefix As KineticaRT.Servers.OpcClient.TextItem

		Private Calib_Resize_Panel As KineticaRT.Logic.Channels.BooleanVariable
		Private Calib_UserInputs_Cancel As KineticaRT.Logic.Channels.BooleanVariable
		Private Calib_UserInputs_OK As KineticaRT.Logic.Channels.BooleanVariable
		Private Calibration_Due_Date(4) As ITextChannel
		Private Calibration_Range As ITextChannel
		Private Calibration_Temp As KineticaRT.Channels.TextChannel
		Private Calibration_Humidity As KineticaRT.Channels.TextChannel
		Private TC_Transducer_SN As KineticaRT.Channels.TextChannel
		Private Calibration_Value As KineticaRT.Servers.OpcClient.Item
		Private CCAS_Requested_Vib_Channel As KineticaRT.Servers.OpcClient.Item
		Private Channel_Range As ITextChannel
		Private Channel_Type As ITextChannel
		Private Check_Report As KineticaRT.Logic.Channels.BooleanVariable
		Private Clear_Button(6) As IBooleanChannel
		Private Comments As ITextChannel
		Private CSV_Filename As KineticaRT.Servers.OpcClient.TextItem
		Private Enter_Button(6) As IBooleanChannel
		Private Generic_Name As ITextChannel
		Private Last_Calib_Date As ITextChannel
		Private Margin_Accuracy As ITextChannel
		Private OEM_Tolerance As KineticaRT.Channels.NumericChannel
		Private OEM_Tolerance_Text As ITextChannel
		Private Measured(6) As KineticaRT.Servers.OpcClient.Item
		Private Measured_Button(6) As IBooleanChannel
		Private New_Calib_Report As KineticaRT.Logic.Channels.BooleanVariable
		Private Offset As KineticaRT.Channels.NumericChannel
		Private Save_Data As KineticaRT.Logic.Channels.BooleanVariable
		Private SCU_Speed_Channel As KineticaRT.Servers.OpcClient.Item
		Private Simulated(6) As KineticaRT.Servers.OpcClient.Item
		Private Slope As KineticaRT.Channels.NumericChannel
		Private Test_Mgr_Calib_Calcul As KineticaRT.Servers.OpcClient.Item
		Private Test_Mgr_Calib_Flag As KineticaRT.Servers.OpcClient.BooleanItem
		Private Test_Mgr_PUsed As KineticaRT.Servers.OpcClient.Item
		Private Test_Mgr_Sub_Command As KineticaRT.Servers.OpcClient.Item
		Private Test_Mgr_Sub_Status As KineticaRT.Servers.OpcClient.Item
		Private Transducer_Range As ITextChannel
		Private Units_Measured As ITextChannel
        Private Units_Simulated As ITextChannel
        Private Save_Calib_Info As KineticaRT.Channels.BooleanChannel


        '*********************
        '* Forms and Control *
        '*********************
        Private Calibration As System.Windows.Forms.Form
		Private Calibration_Information_Panel As KineticaRT.Controls.Control
		Private Calibration_Input_Data As System.Windows.Forms.Form
		Private Calibration_Panel As KineticaRT.Controls.Control
		Private Approval_List As System.Windows.Forms.ComboBox
		Private Frame1 As KineticaRT.Controls.Control
		Private Frame2 As KineticaRT.Controls.Control
		Private Frame3 As KineticaRT.Controls.Control
		Private FVName_List As System.Windows.Forms.ComboBox
		Private Make_List(4) As System.Windows.Forms.ComboBox
		Private Model_List(4) As System.Windows.Forms.ComboBox
		Private Offset_Txt As KineticaRT.Studio.Controls.Display
		Private Point_Frame(6) As KineticaRT.Controls.Control
		Private Measured_Txt(6) As KineticaRT.Studio.Controls.Display
		Private Simulated_Txt(6) As KineticaRT.Studio.Controls.Display
		Private Serial_Number_List(4) As System.Windows.Forms.ComboBox
		Private Slope_Txt As KineticaRT.Studio.Controls.Display
		Private CalibratedBy_List As System.Windows.Forms.ComboBox
		Private Assistant_List As System.Windows.Forms.ComboBox

        ' Used for God mode
        Private LAST_CALIB_DISPLAY As KineticaRT.Studio.Controls.Display
        Private ACCURACY_DISPLAY As KineticaRT.Studio.Controls.Display
        Private SIMULATED_VALUE_DISPLAY As KineticaRT.Studio.Controls.Display
        Private TRANSDUCER_RANGE_DISPLAY As KineticaRT.Studio.Controls.Display
        Private COMMENTS_DISPLAY As KineticaRT.Studio.Controls.Display
        Private SAVE_CALIB_INFO_BTN As KineticaRT.Studio.Controls.Button
        Private SimulatedUnits_List As System.Windows.Forms.ComboBox
        Private PressEnter_Label As System.Windows.Forms.Label
        Private GodMode_Label As System.Windows.Forms.Label
        Private InstrumentRange_Display As KineticaRT.Controls.Display

        Private PARAMETERS_TXT_DISPLAY As KineticaRT.Controls.Display
		Private MEASURED_VALUE_DISPLAY As KineticaRT.Controls.Display
		Private MEASURED_TXT_DISPLAY As KineticaRT.Controls.Display
		Private CALIBRATION_BTN As KineticaRT.Studio.Controls.Button
		Private ControlsFrame As KineticaRT.Controls.Control
		'Private CHECK_REPORT_BTN As KineticaRT.Studio.Controls.Button
		Private NEW_REPORT_BTN As KineticaRT.Studio.Controls.Button
		Private SAVE_DATA_BTN As KineticaRT.Studio.Controls.Button
		Private btnClose As KineticaRT.Studio.Controls.Button
		Private CALIB_RESIZE_BTN As KineticaRT.Studio.Controls.Button
		Private Button1(6) As KineticaRT.Studio.Controls.Button '1st Measured Value
		Private Button2(6) As KineticaRT.Studio.Controls.Button 'Enter
		Private Button3(6) As KineticaRT.Studio.Controls.Button 'Clear
		Private DAS_CALIB_DISPLAY As KineticaRT.Controls.Display
		Private Picture2 As KineticaRT.Controls.Picture

		'********
		'* Misc *
		'********	
		'Private Calib_Report_Flag As Boolean
		Private CalibrationFlag As Boolean
		'Private Check_Report_Flag As Boolean
		Private CheckFlag As Boolean
		Private ItemSelected As Boolean
		Private NewSelectedIO As Boolean
		Private PUsed(6) As Integer
		Private Strongback_Number As String

        ' God mode variables, used to store initial values
        Private initial_LastCalibDate As String
        Private initial_Accuracy As Double
        Private initial_UnitsSimulated As EngineeringUnit
        Private initial_TransducerRange As String
        Private initial_Comments As String
        Private initial_InstrumentRange As String

		Public Sub New()

		End Sub
	End Class

	<Serializable()> _
	Public Class Calib_FVName
		Implements IComparable

		Private myFV_Name As String
		Private myGeneric_Name As String
		Private myBranch_List As String

		Public Overloads Function CompareTo(ByVal obj As Object) As Integer _
			Implements IComparable.CompareTo

			If TypeOf obj Is Calib_FVName Then
				Dim temp As Calib_FVName = CType(obj, Calib_FVName)
				Dim r As New Regex("(?<type>[a-zA-Z_]+)(?<seq>\d+)")
				Dim m As Match
				Dim t As Match
				Dim ret As Integer
				Dim seq1, seq2 As Integer

				' From Regex expression get the type and sequence number for FVName

				m = r.Match(Me.myFV_Name)
				t = r.Match(temp.myFV_Name)


				If (m.Success) And (t.Success) Then
					ret = m.Groups("type").Value.CompareTo(t.Groups("type").Value)
					' If type equal compare the sequence number if not return the CompareTo result
					' CompareTo return < 0 if less then, 0 if equal or > 0 if more then
					If ret = 0 Then
						seq1 = Integer.Parse(m.Groups("seq").Value)
						seq2 = Integer.Parse(t.Groups("seq").Value)
						ret = seq1.CompareTo(seq2)
					End If
				Else
					ret = Me.myFV_Name.CompareTo(temp.myFV_Name)
				End If

				Return ret
			End If

			Throw New ArgumentException("object is not a Calib_FVName")
		End Function

		Public Sub New(ByVal strFV_Name As String, ByVal strGeneric_Name As String, ByVal strBranch_List As String)
			MyBase.New()
			Me.myFV_Name = strFV_Name
			Me.myGeneric_Name = strGeneric_Name
			Me.myBranch_List = strBranch_List
		End Sub

		Public ReadOnly Property FV_Name() As String
			Get
				Return myFV_Name
			End Get
		End Property

		Public ReadOnly Property Generic_Name() As String
			Get
				Return myGeneric_Name
			End Get
		End Property

		Public ReadOnly Property Branch_List() As String
			Get
				Return myBranch_List
			End Get
		End Property

		Public Overrides Function ToString() As String
			Return Me.FV_Name & " - " & Me.Generic_Name
		End Function
	End Class
End Namespace

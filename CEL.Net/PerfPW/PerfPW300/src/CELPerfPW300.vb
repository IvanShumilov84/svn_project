Imports System
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports CEL.Perf

Namespace CEL.Perf

	'****************************************************************************
	'* Public Class EngineModelPW ***********************************************
	'****************************************************************************

	Public NotInheritable Class PerfPW300
        Inherits Global.CEL.Perf.PerfPW

		'********************************************************************
		'* Private Declare **************************************************
		'********************************************************************

		Private Declare Ansi Sub PWC83023CALOFF Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			()

		Private Declare Ansi Sub PWC83023CALON Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			()

		Private Declare Ansi Sub PWC83023CONDITIONOFF Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer, ByRef iConditionNo As Integer)

		Private Declare Ansi Sub PWC83023CONDITIONON Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer, ByRef iConditionNo As Integer)

		Private Declare Ansi Sub PWC83023ERRORSTRING Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer, <[In],Out> ByVal Str1 As StringBuilder, ByVal Str1Size As Integer, _
			 <[In],Out> ByVal Str2 As StringBuilder, ByVal Str2Size As Integer)

		Private Declare Ansi Sub PWC83023FACALCS Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer, ByRef iSave As Integer, ByRef iOutput As Integer)

		Private Declare Ansi Sub PWC83023GETALLFARESULTS Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(<[In],Out> ByVal Headers As StringBuilder, ByVal HeadersStrSize As Integer, _
			<[In],Out> ByVal PlotVals As StringBuilder, ByVal plotValsStrSize As Integer, _			
			ByRef n_Vals As Integer)

		Private Declare Ansi Sub PWC83023GETALLRESULTS Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(<[In],Out> ByVal Headers As StringBuilder, ByVal HeadersStrSize As Integer, _
			<[In],Out> ByVal PlotVals() As single, ByRef n_Vals As Integer)

		Private Declare Ansi Sub PWC83023GETBSLIST Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer, <[In],Out> ByVal BSList As StringBuilder, ByVal BSListStrSize As Integer, _
			 ByRef NumBS As Integer)

		Private Declare Ansi Sub PWC83023GETBUILD Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iBuild As Integer)

		Private Declare Ansi Sub PWC83023GETBUILDSPEC Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iBuildSpec As Integer)

		Private Declare Ansi Sub PWC83023GETCAL Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iCalibration As Integer)
			
		Private Declare Ansi Sub PWC83023GETCALLIST Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(<[In],Out> ByVal calibrationList() As Integer, ByRef numberCalibration As Integer)

		Private Declare Ansi Sub PWC83023GETCALSTAT Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(<[In],Out> ByVal cStatus As StringBuilder, ByVal cStatusSize As Integer)

		Private Declare Ansi Sub PWC83023GETCONDITION Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iCondition As Integer)

		Private Declare Ansi Sub PWC83023GETCONDITIONLIST Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef calNumber As Integer, <[In],Out> ByVal conditionNumbers() As Integer, _
			 <[In],Out> ByVal descriptions As StringBuilder, ByVal descriptionSize As Integer, _
			 <[In],Out> ByVal statuses As StringBuilder, ByVal statusSize As Integer, _
			ByRef numberCondition As Integer)
			
		Private Declare Ansi Sub PWC83023GETENGINE Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(<[In],Out> ByVal cEngine As StringBuilder, ByVal cEngineSize As Integer)

		Private Declare Ansi Sub PWC83023GETENGINEMODEL Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(<[In],Out> ByVal cEngineModel As StringBuilder, ByVal cEngineModelSize As Integer)
		
		Private Declare Ansi Sub PWC83023GETFARRESULT Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByVal cLabel As String, ByVal cLabelLen As Integer, ByRef rResult As Single)

		Private Declare Ansi Sub PWC83023GETRESULT Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByVal cLabel As String, ByVal cLabelLen As Integer, ByRef rResult As Single)

		Private Declare Ansi Sub PWC83023GETSB Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer, _
			ByVal cBuildSpec As String, ByVal cBuildSpecLen As Integer, _
			ByRef NumSB As Integer, _
			<[In],Out> ByVal SBList As StringBuilder, ByVal SBListStrSize As Integer, _
			<[In],Out> ByVal EngineUp As StringBuilder, ByVal EngineUpSize As Integer)
			
		Private Declare Ansi Sub PWC83023GETUNITDATA Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer)

		Private Declare Ansi Sub PWC83023LOADBUILDSPEC Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer, ByVal cBuildSpec As String, ByVal cBuildSpecLen As Integer, _
			 ByVal SB1 As String, ByVal SB1Len As Integer, _
			 ByVal SB2 As String, ByVal SB2Len As Integer)

		Private Declare Ansi Sub PWC83023LOADCONDITIONSTAT Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer)

		Private Declare Ansi Function PWC83023PUTVALUE Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByVal cLabel As String, ByVal cLabelLen As Integer, _
			 ByVal cValue As String, ByVal cValueLen As Integer, _
			 ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

		Private Declare Ansi Function PWC83023PUTUNITVALUE Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByVal cLabel As String, ByVal cLabelLen As Integer, ByVal cUnit As String, ByVal cUnitLen As Integer) As Boolean

		Private Declare Ansi Sub PWC83023PROCESS Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer, ByRef iSave As Integer, ByRef iOutput As Integer)

		Private Declare Ansi Sub PWC83023SAVECONDITIONSTAT Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer)

		Private Declare Ansi Sub PWC83023SAVEUNITDATA Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iError As Integer)

		Private Declare Ansi Sub PWC83023SETBUILD Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iBuild As Integer)

		Private Declare Ansi Sub PWC83023SETCAL Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iCalibration As Integer)
			
		Private Declare Ansi Sub PWC83023SETCONDITION Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByRef iCondition As Integer)

		Private Declare Ansi Sub PWC83023SETCONFIGURATIONPATH Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByVal cPath As String, ByVal cPathLen As Integer)

		Private Declare Ansi Sub PWC83023SETDATAPATH Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByVal cPath As String, ByVal cPathLen As Integer)

		Private Declare Ansi Sub PWC83023SETDATETIME Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			()

		Private Declare Ansi Sub PWC83023SETENGINE Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByVal cEngine As String, ByVal cEngineLen As Integer)

		Private Declare Ansi Sub PWC83023SETSTATUS Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83023\exe\Pwc83023.dll" _
			(ByVal cStatus As String, ByVal cStatusLen As Integer)

		'********************************************************************
		'* Public Properties ************************************************
		'********************************************************************

		Public Shared ReadOnly Property Instance() As CEL.Perf.Perf
			Get
				If (NOT isInitialized) Then
					theInstance = New PerfPW300
					isInitialized = TRUE
				End If

				Return theInstance
			End Get
		End Property

		'********************************************************************
		'* Public Members ***************************************************
		'********************************************************************

		Public Overrides Sub SetEngineSN(serialNumber As String)
			Dim buildNumber As Integer = 1

			PWC83023SETENGINE(serialNumber, serialNumber.Length())
			PWC83023SETBUILD(buildNumber)
		End Sub

		'********************************************************************
		'* Protected Members ************************************************
		'********************************************************************

		Protected Overrides Sub PWC_FACalcs(ByRef ErrorNo As Integer, Save As Boolean, Output As Boolean)
			Dim iOutput As Integer = 0
			Dim iSave As Integer = 0

			If Save Then
				iSave = 1
			End If
			If Output Then
				iOutput = 1
			End If

			PWC83023SETDATETIME()
			PWC83023FACALCS(ErrorNo, iSave, iOutput)
		End Sub

		Protected Overrides Sub PWC_GetAllFAResults(ByRef Headers() As String, ByRef PlotVals() As String)
			Dim i as integer
			Dim myHeadersFor As New StringBuilder(100*10)		'100 Strings of 10 Chars each
			Dim myPlotValsFor as New StringBuilder(100*10)		'100 Strings of 10 Chars each
			Dim myHeaders As String
			Dim myPlotVals as string
			Dim n_Vals As Integer
			
			PWC83023GETALLFARESULTS(myHeadersFor, 10, MyPlotValsFor, 10, n_Vals)
			
			ReDim Headers(n_Vals - 1)
			ReDim PlotVals(n_Vals - 1)
						
			myHeaders = myHeadersFor.ToString()
			myPlotVals = myPlotValsfor.ToString()
			For i=0 To (n_Vals - 1)
				Headers(i) = myHeaders.Substring(i*10,10)
				PlotVals(i) = myPlotVals.Substring(i*10,10)
			Next i
		End Sub

		Protected Overrides Sub PWC_GetAllResults(ByRef Headers() As String, ByRef PlotVals() As single)
			Dim i as integer
			Dim myHeadersFor As New StringBuilder(100*10)		'100 Strings of 10 Chars each
			Dim myHeaders As String
			Dim n_Vals As Integer
			
			Redim PlotVals(100)
		 
			PWC83023GETALLRESULTS(myHeadersFor, 10, PlotVals, n_Vals)
			
			ReDim Preserve PlotVals(n_Vals - 1)
			ReDim Headers(n_Vals - 1)
			
			myHeaders = myHeadersFor.ToString()
			For i=0 To (n_Vals - 1)
				Headers(i) = myHeaders.Substring(i*10,10)
			Next i
		End Sub

		Protected Overrides Sub PWC_GetBSList(ByRef ErrorNo As Integer, ByRef BSList() As String)
			Dim i, NumBS As Integer
			Dim myBsListFor As New StringBuilder(100*26)	'100 Strings Of 26 Chars each
			Dim myBsList As String

			PWC83023GETBSLIST(ErrorNo, myBSListFor, 26, NumBs)
			If (ErrorNo <> 0) Then
				'
				'Throw Exception ?!?!?!
				'
			Else
				ReDim BSList(NumBS-1)
				myBSList = myBsListFor.ToString()
				For i = 0 To (NumBS-1)
					BSList(i) = myBSList.SubString(i*26, 26)
					'BSList(i) = New CEL.Perf.BuildSpecDescriptor(myBSList.SubString(i*26,4).Trim(), _
					'			 myBSList.SubString(i*26+12,14).Trim() )
				Next i
			End If
		End Sub

		Protected Overrides Sub PWC_GetBuild(ByRef buildNumber As Integer)
			PWC83023GETBUILD(buildNumber)
		End Sub

		Protected Overrides Sub PWC_GetBuildSpec(ByRef buildSpecNumber As Integer)
			PWC83023GETBUILDSPEC(buildSpecNumber)
		End Sub

		Protected Overrides Sub PWC_GetCalibrationList(ByRef calibrationList() As Integer)
			Dim numberCalibration As Integer
			ReDim calibrationList(80)
			
			PWC83023GETCALLIST(calibrationList, numberCalibration)
			ReDim Preserve calibrationList(numberCalibration-1)
		End Sub

		Protected Overrides Sub PWC_GetCalibrationNumber(ByRef calibrationNumber As Integer)
			PWC83023GETCAL(calibrationNumber)
		End Sub
		
		Protected Overrides Sub PWC_GetCalibrationStatus(ByRef calibrationStatus As String)
			Dim myCalibrationStatus As New StringBuilder("", 3)

			PWC83023GETCALSTAT(myCalibrationStatus, myCalibrationStatus.Capacity())
			calibrationStatus = myCalibrationStatus.ToString().SubString(0,3)		
		End Sub

		Protected Overrides Sub PWC_GetConditionList(calNo As Integer, _
			ByRef conditionNumbers() As Integer, ByRef descriptions() As String, ByRef statuses() As String)
			ReDim conditionNumbers(200)
			Dim descriptionsFor As New StringBuilder(200*32)	'200 Strings of 32 chars each
			Dim statusesFor As New StringBuilder(200*3)			'200 Strings of 3 chars each
			Dim numConditions, i As Integer
			
			PWC83023GETCONDITIONLIST(calNo, conditionNumbers, descriptionsFor, 32, _
				statusesFor, 3, numConditions)
			ReDim Preserve conditionNumbers(numConditions-1)
			ReDim descriptions(numConditions-1)
			Dim descriptionsTmp As String = descriptionsFor.ToString()
			ReDim statuses(numConditions-1)
			Dim statusesTmp As String = statusesFor.ToString()
			
			If (numConditions > 0) Then
				For i = 0 To numConditions-1
					If (descriptionsTmp.Length >= (i*32+32)) Then
						descriptions(i) = descriptionsTmp.SubString(i*32, 32)
					Else
						descriptions(i) = ""
					End If
					If (statusesTmp.Length >= (i*3+3)) Then
						statuses(i) = statusesTmp.Substring(i*3, 3)
					Else
						statuses(i) = ""
					End If
				Next i
			End If
		End Sub

		Protected Overrides Sub PWC_GetConditionNumber(ByRef conditionNumber As Integer)
			PWC83023GETCONDITION(conditionNumber)
		End Sub

		Protected Overrides Sub PWC_ConditionOff(ByRef ErrorNo As Integer, ConditionNo As Integer)
			PWC83023CONDITIONOFF(ErrorNo, ConditionNo)
		End Sub

		Protected Overrides Sub PWC_ConditionOn(ByRef ErrorNo As Integer, ConditionNo As Integer)
			PWC83023CONDITIONON(ErrorNo, ConditionNo)
		End Sub

		Protected Overrides Sub PWC_GetEngine(ByRef engineSerialNumber As String)
			Dim engineSNFor As New StringBuilder(25)

			PWC83023GETENGINE(engineSNFor, engineSNFor.Capacity())
			engineSerialNumber = engineSNFor.ToString().Substring(0,25).Trim()
		End Sub

		Protected Overrides Sub PWC_GetEngineModel(ByRef EngineModel As String)
			Dim EngineModelFor As New StringBuilder(14)

			PWC83023GETENGINEMODEL(EngineModelFor, EngineModelFor.Capacity())
			EngineModel = EngineModelFor.ToString().SubString(0,14).Trim()
		End Sub

		Protected Overrides Sub PWC_GetErrorStrings(ErrorNo As Integer, ByRef Str1 As String, ByRef Str2 As String)
			Dim Str1For As New StringBuilder(80)
			Dim Str2For As New StringBuilder(80)

			PWC83023ERRORSTRING(ErrorNo, Str1For, Str1For.Capacity(), Str2For, Str2For.Capacity())
			Str1 = Str1For.ToString().SubString(0,80).Trim()
			Str2 = Str2For.ToString().SubString(0,80).Trim()
		End Sub

		Protected Overrides Sub PWC_GetFArResult(label As String, ByRef value As Single)
			PWC83023GETFARRESULT(label, label.length(), value)
		End Sub

		Protected Overrides Sub PWC_GetResult(label As String, ByRef value As String)
			Dim valueFor As Single

			PWC83023GETRESULT(label, label.length(), valueFor)
			value = valueFor.ToString()
		End Sub

		Protected Overrides Sub PWC_GetSB(ByRef errorNo	As Integer, buildSpec As String, ByRef sbList As String(), ByRef engineUp As String)
			Dim i,numSB As Integer
			Dim myEngineUpFor As New StringBuilder(2*6)
			Dim mySBListFor As New StringBuilder(2*6)	'2 strings of 6 characters each
			Dim mySBList As String
			
			PWC83023GETSB(errorNo, buildSpec, buildSpec.Length, numSB, mySBListFor, 6, myEngineUpFor, 6)
			If (errorNo = 0) Then
				ReDim sbList(numSB-1)
				
				engineUp = myEngineUpFor.ToString().Trim()
				mySBList = mySBListFor.ToString()

				For i = 0 To (numSB-1)
					sbList(i) = mySBList.Substring(i*6,6)
				Next
			End If
		End Sub
		
		Protected Overrides Sub PWC_GetUnitData(ByRef ErrorNo As Integer)
			PWC83023GETUNITDATA(ErrorNo)
		End Sub

		Protected Overrides Sub PWC_LoadBuildSpec(ByRef ErrorNo As Integer, buildSpec As String, SB1 As String, SB2 As String)
			PWC83023LOADBUILDSPEC(ErrorNo, buildSpec, buildSpec.Length(), SB1, SB1.Length(), SB2, SB2.Length())
		End Sub

		Protected Overrides Sub PWC_LoadConditionStat(ByRef ErrorNo As Integer)
			PWC83023LOADCONDITIONSTAT(ErrorNo)
		End Sub

		Protected Overrides Sub PWC_Process(ByRef ErrorNo As Integer, Save As Boolean, Output As Boolean)
			Dim iOutput As Integer = 0
			Dim iSave As Integer = 0

			If Save Then
				iSave = 1
			End If
			If Output Then
				iOutput = 1
			End If

			PWC83023SETDATETIME()
			PWC83023PROCESS(ErrorNo, iSave, iOutput)
		End Sub

		Protected Overrides Function PWC_PutUnitValue(parameter As String, unit As String) As Boolean
			Return PWC83023PUTUNITVALUE(parameter, parameter.Length(), unit, unit.Length())
		End Function

		Protected Overrides Function PWC_PutValue(Label As String, Value As String, Index As String) As Boolean
			Return PWC83023PUTVALUE(Label, Label.Length(), Value, Value.Length(), Index, Index.Length())
		End Function

		Protected Overrides Sub PWC_SaveConditionStat(ByRef ErrorNo As Integer)
			PWC83023SAVECONDITIONSTAT(ErrorNo)
		End Sub

		Protected Overrides Sub PWC_SaveUnitData(ByRef ErrorNo As Integer)
			PWC83023SAVEUNITDATA(ErrorNo)
		End Sub
		
		Protected Overrides Sub PWC_SetCalibrationNumber(calibrationNumber As Integer)
			PWC83023SETCAL(calibrationNumber)
		End Sub

		Protected Overrides Sub PWC_SetCalibrationOff()
			PWC83023CALOFF()
		End Sub

		Protected Overrides Sub PWC_SetCalibrationOn()
			PWC83023CALON()
		End Sub

		Protected Overrides Sub PWC_SetConditionNumber(conditionNumber As Integer)
			PWC83023SETCONDITION(conditionNumber)
		End Sub

		Protected Overrides Sub PWC_SetConfigurationPath(Path As String)
			PWC83023SETCONFIGURATIONPATH(Path, Path.Length())
		End Sub

		Protected Overrides Sub PWC_SetDataPath(Path As String)
			PWC83023SETDATAPATH(Path, Path.Length())
		End Sub

		Protected Overrides Sub PWC_SetStatus(status As String)
			Dim myStatus As String = status.Substring(0,1).ToUpper()

			PWC83023SETSTATUS(myStatus, myStatus.Length())
		End Sub

		'********************************************************************
		'* Private Mambers **************************************************
		'********************************************************************

		Private Sub New()
			ConfigurationPath = "C:\Program Files\Pratt & Whitney Canada\Pwc83023\Configuration\"
'			SetConfigurationPath("..\..\KineticaRT\Prj04252_HMI_Critical\Configuration\")
			BuildSupportedEngineModelList()
		End Sub

		Private Sub BuildSupportedEngineModelList
			Dim i,j As Integer
			ReDim supportedEngineModels(pw300SupportedEngineModels.GetUpperBound(0))
            Dim BSList() As String = Nothing
            Dim SBList() As String = Nothing
            Dim EngineUp As String = Nothing
						
			GetBSList(BSList)
			
			For i = 0 To BSList.GetUpperBound(0)
				
				GetSB(BSList(i), SBList, EngineUp)
				
				For j = 0 To supportedEngineModels.GetUpperBound(0)
					If (BSList(i).IndexOf(pw300SupportedEngineModels(j,1)) <> -1) Then
						If supportedEngineModels(j) Is Nothing Then
							supportedEngineModels(j) = _
								New EngineModelPW(pw300SupportedEngineModels(j, 0), _
										  pw300SupportedEngineModels(j, 1), _
										  BSList(i), _
										  SBList, _
										  Me)
						Else
							supportedEngineModels(j).AddBuildSpec(BSList(i), SBList)
						End If	
					End If
				Next j
			Next i
		End Sub

		Private Shared pw300SupportedEngineModels As String(,) = { {"PW305A ANC",   "PW305A"}, _
									   {"PW305A SMITH", "PW305A"}, _
									   {"PW305B SMITH", "PW305B"}, _
									   {"PW306A ANC",   "PW306A"}, _
									   {"PW306B ANC",   "PW306B"}, _
									   {"PW306C",       "PW306C"}, _
									   {"PW308A ANC",   "PW308A"}, _
									   {"PW308C ANC",   "PW308C"} }
		Private Shared theInstance As PerfPW300
		Private Shared isInitialized As Boolean = FALSE
	End Class
End Namespace

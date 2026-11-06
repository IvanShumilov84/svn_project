Imports System
Imports System.Text
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports CEL.Perf

Namespace CEL.Perf

	'****************************************************************************
	'* Public Class EngineModelPW ***********************************************
	'****************************************************************************

	Public NotInheritable Class PerfPW500
        Inherits Global.CEL.Perf.PerfPW

		'********************************************************************
		'* Private Declare **************************************************
		'********************************************************************

		Private Declare Ansi Sub PWC83024CALOFF Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			()

		Private Declare Ansi Sub PWC83024CALON Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			()

		Private Declare Ansi Sub PWC83024CONDITIONOFF Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer, ByRef iConditionNo As Integer)

		Private Declare Ansi Sub PWC83024CONDITIONON Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer, ByRef iConditionNo As Integer)

		Private Declare Ansi Sub PWC83024ERRORSTRING Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer, <[In],Out> ByVal Str1 As StringBuilder, ByVal Str1Size As Integer, _
			 <[In],Out> ByVal Str2 As StringBuilder, ByVal Str2Size As Integer)

		Private Declare Ansi Sub PWC83024FACALCS Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer, ByRef iSave As Integer, ByRef iOutput As Integer)

		Private Declare Ansi Sub PWC83024GETALLFARESULTS Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(<[In],Out> ByVal Headers As StringBuilder, ByVal HeadersStrSize As Integer, _
			<[In],Out> ByVal PlotVals As StringBuilder, ByVal PlotValsStrSize As Integer, _
			ByRef n_Vals As Integer)

		Private Declare Ansi Sub PWC83024GETALLRESULTS Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(<[In],Out> ByVal Headers As StringBuilder, ByVal HeadersStrSize As Integer, _
			<[In],Out> ByVal PlotVals() As single, ByRef n_Vals As Integer)

		Private Declare Ansi Sub PWC83024GETBSLIST Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer, <[In],Out> ByVal BSList As StringBuilder, ByVal BSListStrSize As Integer, _
			 ByRef NumBS As Integer)

		Private Declare Ansi Sub PWC83024GETBUILD Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iBuild As Integer)

		Private Declare Ansi Sub PWC83024GETBUILDSPEC Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iBuildSpec As Integer)

		Private Declare Ansi Sub PWC83024GETCAL Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iCalibration As Integer)

		Private Declare Ansi Sub PWC83024GETCALLIST Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(<[In],Out> ByVal calibrationList() As Integer, ByRef numberCalibration As Integer)

		Private Declare Ansi Sub PWC83024GETCALSTAT Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(<[In],Out> ByVal cStatus As StringBuilder, ByVal cStatusSize As Integer)

		Private Declare Ansi Sub PWC83024GETCONDITION Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iCondition As Integer)

		Private Declare Ansi Sub PWC83024GETCONDITIONLIST Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef calNumber As Integer, <[In],Out> ByVal conditionNumbers() As Integer, _
			 <[In],Out> ByVal descriptions As StringBuilder, ByVal descriptionSize As Integer, _
			 <[In],Out> ByVal statuses As StringBuilder, ByVal statusSize As Integer, _
			ByRef numberCondition As Integer)
			
		Private Declare Ansi Sub PWC83024GETENGINE Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(<[In],Out> ByVal cEngine As StringBuilder, ByVal cEngineSize As Integer)

		Private Declare Ansi Sub PWC83024GETENGINEMODEL Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(<[In],Out> ByVal cEngineModel As StringBuilder, ByVal cEngineModelSize As Integer)

		Private Declare Ansi Sub PWC83024GETFARRESULT Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByVal cLabel As String, ByVal cLabelLen As Integer, ByRef rResult As Single)

		Private Declare Ansi Sub PWC83024GETRESULT Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByVal cLabel As String, ByVal cLabelLen As Integer, ByRef rResult As Single)

		Private Declare Ansi Sub PWC83024GETSB Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer, _
			ByVal cBuildSpec As String, ByVal cBuildSpecLen As Integer, _
			ByRef NumSB As Integer, _
			<[In],Out> ByVal SBList As StringBuilder, ByVal SBListStrSize As Integer, _
			<[In],Out> ByVal EngineUp As StringBuilder, ByVal EngineUpSize As Integer)
			
		Private Declare Ansi Sub PWC83024GETUNITDATA Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer)

		Private Declare Ansi Sub PWC83024LOADBUILDSPEC Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer, ByVal cBuildSpec As String, ByVal cBuildSpecLen As Integer, _
			 ByVal SB1 As String, ByVal SB1Len As Integer, _
			 ByVal SB2 As String, ByVal SB2Len As Integer)

		Private Declare Ansi Sub PWC83024LOADCONDITIONSTAT Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer)

		Private Declare Ansi Function PWC83024PUTVALUE Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByVal cLabel As String, ByVal cLabelLen As Integer, _
			 ByVal cValue As String, ByVal cValueLen As Integer, _
			 ByVal cIndex As String, ByVal cIndexLen As Integer) As Boolean

		Private Declare Ansi Function PWC83024PUTUNITVALUE Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByVal cLabel As String, ByVal cLabelLen As Integer, ByVal cUnit As String, ByVal cUnitLen As Integer) As Boolean

		Private Declare Ansi Sub PWC83024PROCESS Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer, ByRef iSave As Integer, ByRef iOutput As Integer)

		Private Declare Ansi Sub PWC83024SAVECONDITIONSTAT Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer)

		Private Declare Ansi Sub PWC83024SAVEUNITDATA Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iError As Integer)

		Private Declare Ansi Sub PWC83024SETBUILD Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iBuild As Integer)

		Private Declare Ansi Sub PWC83024SETCAL Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iCalibration As Integer)

		Private Declare Ansi Sub PWC83024SETCONDITION Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByRef iCondition As Integer)

		Private Declare Ansi Sub PWC83024SETCONFIGURATIONPATH Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByVal cPath As String, ByVal cPathLen As Integer)

		Private Declare Ansi Sub PWC83024SETDATAPATH Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByVal cPath As String, ByVal cPathLen As Integer)

		Private Declare Ansi Sub PWC83024SETDATETIME Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			()

		Private Declare Ansi Sub PWC83024SETENGINE Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByVal cEngine As String, ByVal cEngineLen As Integer)

		Private Declare Ansi Sub PWC83024SETSTATUS Lib "C:\Program Files\Pratt & Whitney Canada\Pwc83024\exe\Pwc83024.dll" _
			(ByVal cStatus As String, ByVal cStatusLen As Integer)

		'********************************************************************
		'* Public Properties ************************************************
		'********************************************************************

		Public Shared ReadOnly Property Instance() As CEL.Perf.Perf
			Get
				If (NOT isInitialized) Then
					theInstance = New PerfPW500
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

			PWC83024SETENGINE(serialNumber, serialNumber.Length())
			PWC83024SETBUILD(buildNumber)
		End Sub

		'********************************************************************
		'* Protected Members ************************************************
		'********************************************************************

		Protected Overrides Sub PWC_ConditionOff(ByRef ErrorNo As Integer, ConditionNo As Integer)
			PWC83024CONDITIONOFF(ErrorNo, ConditionNo)
		End Sub

		Protected Overrides Sub PWC_ConditionOn(ByRef ErrorNo As Integer, ConditionNo As Integer)
			PWC83024CONDITIONON(ErrorNo, ConditionNo)
		End Sub

		Protected Overrides Sub PWC_FACalcs(ByRef ErrorNo As Integer, Save As Boolean, Output As Boolean)
			Dim iOutput As Integer = 0
			Dim iSave As Integer = 0

			If Save Then
				iSave = 1
			End If
			If Output Then
				iOutput = 1
			End If

			PWC83024SETDATETIME()
			PWC83024FACALCS(ErrorNo, iSave, iOutput)
		End Sub

		Protected Overrides Sub PWC_GetAllFAResults(ByRef Headers() As String, ByRef PlotVals() As String)
			Dim i as integer
			Dim myHeadersFor As New StringBuilder(100*10)		'100 Strings of 10 Chars each
			Dim myPlotValsFor as New StringBuilder(100*10)		'100 Strings of 10 Chars each
			Dim myHeaders As String
			Dim myPlotVals as string
			Dim n_Vals As Integer
			
			PWC83024GETALLFARESULTS(myHeadersFor, 10, MyPlotValsFor, 10, n_Vals)
			
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
		 
			PWC83024GETALLRESULTS(myHeadersFor, 10, PlotVals, n_Vals)
			
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

			PWC83024GETBSLIST(ErrorNo, myBSListFor, 26, NumBs)
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
			PWC83024GETBUILD(buildNumber)
		End Sub

		Protected Overrides Sub PWC_GetBuildSpec(ByRef buildSpecNumber As Integer)
			PWC83024GETBUILDSPEC(buildSpecNumber)
		End Sub

		Protected Overrides Sub PWC_GetCalibrationList(ByRef calibrationList() As Integer)
			Dim numberCalibration As Integer
			ReDim calibrationList(80)
			
			PWC83024GETCALLIST(calibrationList, numberCalibration)
			ReDim Preserve calibrationList(numberCalibration-1)
		End Sub

		Protected Overrides Sub PWC_GetCalibrationNumber(ByRef calibrationNumber As Integer)
			PWC83024GETCAL(calibrationNumber)
		End Sub		
		
		Protected Overrides Sub PWC_GetCalibrationStatus(ByRef calibrationStatus As String)
			Dim myCalibrationStatus As New StringBuilder("", 3)

			PWC83024GETCALSTAT(myCalibrationStatus, myCalibrationStatus.Capacity())
			calibrationStatus = myCalibrationStatus.ToString().SubString(0,3)		
		End Sub

		Protected Overrides Sub PWC_GetConditionList(calNo As Integer, _
			ByRef conditionNumbers() As Integer, ByRef descriptions() As String, ByRef statuses() As String)
			ReDim conditionNumbers(200)
			Dim descriptionsFor As New StringBuilder(200*32)	'200 Strings of 32 chars each
			Dim statusesFor As New StringBuilder(200*3)			'200 Strings of 3 chars each
			Dim numConditions, i As Integer
			
			PWC83024GETCONDITIONLIST(calNo, conditionNumbers, descriptionsFor, 32, _
				statusesFor, 3, numConditions)
			ReDim Preserve conditionNumbers(numConditions-1)
			ReDim descriptions(numConditions-1)
			Dim descriptionsTmp As String = descriptionsFor.ToString()
			ReDim statuses(numConditions-1)
			Dim statusesTmp As String = statusesFor.ToString()
			
			For i = 0 To numConditions-1
				descriptions(i) = descriptionsTmp.SubString(i*32, 32)
				statuses(i) = statusesTmp.Substring(i*3, 3)
			Next i
		End Sub

		Protected Overrides Sub PWC_GetConditionNumber(ByRef conditionNumber As Integer)
			PWC83024GETCONDITION(conditionNumber)
		End Sub

		Protected Overrides Sub PWC_GetEngine(ByRef engineSerialNumber As String)
			Dim engineSNFor As New StringBuilder(25)

			PWC83024GETENGINE(engineSNFor, engineSNFor.Capacity())
			engineSerialNumber = engineSNFor.ToString().Substring(0,25).Trim()
		End Sub

		Protected Overrides Sub PWC_GetEngineModel(ByRef EngineModel As String)
			Dim EngineModelFor As New StringBuilder(14)

			PWC83024GETENGINEMODEL(EngineModelFor, EngineModelFor.Capacity())
			EngineModel = EngineModelFor.ToString().SubString(0,14).Trim()
		End Sub

		Protected Overrides Sub PWC_GetErrorStrings(ErrorNo As Integer, ByRef Str1 As String, ByRef Str2 As String)
			Dim Str1For As New StringBuilder(80)
			Dim Str2For As New StringBuilder(80)

			PWC83024ERRORSTRING(ErrorNo, Str1For, Str1For.Capacity(), Str2For, Str2For.Capacity())
			Str1 = Str1For.ToString().SubString(0,80).Trim()
			Str2 = Str2For.ToString().SubString(0,80).Trim()
		End Sub

		Protected Overrides Sub PWC_GetFArResult(label As String, ByRef value As Single)
			PWC83024GETFARRESULT(label, label.length(), value)
		End Sub

		Protected Overrides Sub PWC_GetResult(label As String, ByRef value As String)
			Dim valueFor As Single

			PWC83024GETRESULT(label, label.length(), valueFor)
			value = valueFor.ToString()
		End Sub

		Protected Overrides Sub PWC_GetSB(ByRef errorNo	As Integer, buildSpec As String, ByRef sbList As String(), ByRef engineUp As String)
			Dim i,numSB As Integer
			Dim myEngineUpFor As New StringBuilder(6)
			Dim mySBListFor As New StringBuilder(2*6)	'2 strings of 6 characters each
			Dim mySBList As String
			
			PWC83024GETSB(errorNo, buildSpec, buildSpec.Length, numSB, mySBListFor, 6, myEngineUpFor, 6)
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
			PWC83024GETUNITDATA(ErrorNo)
		End Sub

		Protected Overrides Sub PWC_LoadBuildSpec(ByRef ErrorNo As Integer, buildSpec As String, SB1 As String, SB2 As String)
			PWC83024LOADBUILDSPEC(ErrorNo, buildSpec, buildSpec.Length(), SB1, SB1.Length(), SB2, SB2.Length())
		End Sub

		Protected Overrides Sub PWC_LoadConditionStat(ByRef ErrorNo As Integer)
			PWC83024LOADCONDITIONSTAT(ErrorNo)
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

			PWC83024SETDATETIME()
			PWC83024PROCESS(ErrorNo, iSave, iOutput)
		End Sub

		Protected Overrides Function PWC_PutUnitValue(parameter As String, unit As String) As Boolean
			Return PWC83024PUTUNITVALUE(parameter, parameter.Length(), unit, unit.Length())
		End Function

		Protected Overrides Function PWC_PutValue(Label As String, Value As String, Index As String) As Boolean
			Return PWC83024PUTVALUE(Label, Label.Length(), Value, Value.Length(), Index, Index.Length())
		End Function

		Protected Overrides Sub PWC_SaveConditionStat(ByRef ErrorNo As Integer)
			PWC83024SAVECONDITIONSTAT(ErrorNo)
		End Sub

		Protected Overrides Sub PWC_SaveUnitData(ByRef ErrorNo As Integer)
			PWC83024SAVEUNITDATA(ErrorNo)
		End Sub

		Protected Overrides Sub PWC_SetCalibrationNumber(calibrationNumber As Integer)
			PWC83024SETCAL(calibrationNumber)
		End Sub

		Protected Overrides Sub PWC_SetCalibrationOff()
			PWC83024CALOFF()
		End Sub

		Protected Overrides Sub PWC_SetCalibrationOn()
			PWC83024CALON()
		End Sub

		Protected Overrides Sub PWC_SetConditionNumber(conditionNumber As Integer)
			PWC83024SETCONDITION(conditionNumber)
		End Sub

		Protected Overrides Sub PWC_SetConfigurationPath(Path As String)
			PWC83024SETCONFIGURATIONPATH(Path, Path.Length())
		End Sub

		Protected Overrides Sub PWC_SetDataPath(Path As String)
			PWC83024SETDATAPATH(Path, Path.Length())
		End Sub

		Protected Overrides Sub PWC_SetStatus(status As String)
			Dim myStatus As String = status.Substring(0,1).ToUpper()

			PWC83024SETSTATUS(myStatus, myStatus.Length())
		End Sub

		'********************************************************************
		'* Private Mambers **************************************************
		'********************************************************************

		Private Sub New()
'			ConfigurationPath = ".\Configuration\"
			ConfigurationPath = "C:\Program Files\Pratt & Whitney Canada\Pwc83024\Configuration\"
			BuildSupportedEngineModelList()
		End Sub

		Private Sub BuildSupportedEngineModelList
			Dim i,j As Integer
			ReDim supportedEngineModels(pw500SupportedEngineModels.GetUpperBound(0))
            Dim BSList() As String = Nothing
            Dim SBList() As String = Nothing
            Dim EngineUp As String = Nothing

			GetBSList(BSList)

			For i = 0 To BSList.GetUpperBound(0)
				
				GetSB(BSList(i), SBList, EngineUp)
				
				For j = 0 To supportedEngineModels.GetUpperBound(0)
					If (BSList(i).IndexOf(pw500SupportedEngineModels(j,1)) <> -1) Then
						If supportedEngineModels(j) Is Nothing Then
							supportedEngineModels(j) = _
								New EngineModelPW(pw500SupportedEngineModels(j, 0), _
									  pw500SupportedEngineModels(j, 1), _
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

		Private Shared pw500SupportedEngineModels As String(,) = { {"PW530A",	"PW530A"}, _
									   {"PW535A",	"PW535A"}, _
									   {"PW545A",	"PW545A"}, _
									   {"PW545B",	"PW545B"} }
		Private Shared theInstance As PerfPW500
		Private Shared isInitialized As Boolean = FALSE
	End Class
End Namespace

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Drawing
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Namespace CEL.Perf

	'****************************************************************************
	'* Public Class EngineModelPW ***********************************************
	'****************************************************************************

	Public Class EngineModelPW
		Inherits EngineModel

		Protected Friend Class BuildSpec
			
			Public Sub New (newBuildSpec As String, newSBList As String())
				pwBuildSpec = newBuildSpec
				pwSBList = newSBList			
			End Sub

			Protected Friend pwBuildSpec As String
			Protected Friend pwSBList As String()
		End Class

		Public Sub New (newCommonName As String, newDesc As String, newBuildSpec As String, _
				newSBList As String(), newPerf As PerfPW)
			MyBase.New(newCommonName)
			
			pwBuildSpec(0) = New BuildSpec(newBuildSpec, newSBList)
			pwEngineDescription = newDesc
			itsPerf = newPerf
		End Sub

		Public Overrides Function GetPerf As Perf
			return itsPerf
		End Function

		Public Overrides Sub SetAsCurrentModelOnTest()
			itsPerf.EngineModel = Me
		End Sub

		Public Sub AddBuildSpec(newBuildSpec As String, newSBList As String())
			ReDim Preserve pwBuildSpec(pwBuildSpec.GetUpperBound(0)+1)
			
			pwBuildSpec(pwBuildSpec.GetUpperBound(0)) = New BuildSpec(newBuildSpec, newSBList)
		End Sub
	
		Protected Friend pwEngineDescription As String
		Protected Friend pwBuildSpec(0) As BuildSpec
		Protected itsPerf As PerfPW
	End Class

	'****************************************************************************
	'* Public Class PerfPW ******************************************************
	'****************************************************************************

	Public MustInherit Class PerfPW
		Inherits Perf

		'********************************************************************
		'* Public Properties ************************************************
		'********************************************************************

		Public Overrides Property Calibration As Boolean
			Get
                Dim calibrationStatus As String = Nothing

				PWC_GetCalibrationStatus(calibrationStatus)
				If (CalibrationStatus = "OFF") Then
					Return FALSE
				Else
					Return TRUE
				End If
			End Get
			Set (ByVal Value As Boolean)
				If (Value = TRUE)
					PWC_SetCalibrationOn()
				Else
					PWC_SetCalibrationOff()
				End If
			End Set
		End Property

		Public Overrides Property CalibrationNumber As Integer
			Get
				Dim calNumber As Integer
				
				PWC_GetCalibrationNumber(calNumber)
				Return calNumber
			End Get
			Set (ByVal calNumber As Integer)
				PWC_SetCalibrationNumber(calNumber)
			End Set
		End Property

		Public Overrides Property ConditionNumber As Integer
			Get
				Dim conNumber As Integer

				PWC_GetConditionNumber(conNumber)
				Return conNumber
			End Get
			Set (ByVal conNumber As Integer)
				PWC_SetConditionNumber(conNumber)
			End Set
		End Property

		Public Overrides Property ConfigurationPath As String
			Get
				Return itsConfigurationPath
			End Get
			Set (ByVal Path As String)
				'
				'The Fortran Library seems to have Problem with path beginning with "<DriverLetter>:"
				'So Fix the Path before calling the library
				'
				itsConfigurationPath = FixPath(Path)

				PWC_SetConfigurationPath(itsConfigurationPath)
				GetUnitData()
			End Set
		End Property

		Public Overrides Property DataPath As String
			Get
				Return itsDataPath
			End Get
			Set (ByVal Path As String)
				'
				'The Fortran Library seems to have Problem with path beginning with "<DriverLetter>:"
				'So Fix the Path before calling the library
				'
				'Dim FixedPath = FixPath(Path)

				'PWC_SetDataPath(FixedPath)
				itsDataPath = FixPath(Path)
				PWC_SetDataPath(itsDataPath)
			End Set
		End Property
		
		Public Property EngineModel As EngineModelPW
			Get
				return itsEngineModel
			End Get
			Set (ByVal newEngineModel As EngineModelPW)
				SetEngineModel(newEngineModel)
				itsEngineModel = newEngineModel

				'Dim theEngineModel As String
				'PWC_GetEngineModel(theEngineModel)
				'Microsoft.VisualBasic.MsgBox("Engine returned: " + theEngineModel)
			End Set
		End Property

		'********************************************************************
		'* Public Members ***************************************************
		'********************************************************************

		Public Sub ConditionOff(condNo As Integer)
			Dim errorNo As Integer
			
			PWC_ConditionOff(errorNo, condNo)
		End Sub

		Public Sub ConditionOn(condNo As Integer)
			Dim errorNo As Integer
			
			PWC_ConditionOn(errorNo, condNo)
		End Sub

		Public Overrides Sub DoFinalAcceptanceCalcs()
			Dim ErrorNo, curStep As Integer
			'Dim calCondList() As PerfPWCalConditionListItem
			'Dim calCondListForm As PerfPWCalCondListForm
            Dim calibrationList() As Integer = Nothing

			For curStep = 1 To 4
				Select Case curStep
				Case 1
					PWC_LoadConditionStat(ErrorNo)
				Case 2
					Dim bFound As Boolean = False
					PWC_GetCalibrationList(calibrationList)
					
					For Each cal As Integer In calibrationList
						If (cal = Me.CalibrationNumber) Then
							ErrorNo = 0
							bFound = True
							Exit For
						End If
					Next
					
					If Not bFound Then
						Throw New PerfPWException(33, "Final Acceptance","No Active Condition record found")
					End If
				Case 3
					If (Me.NumberOfActiveConditions(Me.CalibrationNumber) > 0) Then
						ErrorNo = 0
					Else
						Throw New PerfPWException(33, "Final Acceptance","No Active Condition record found")
					End If
				Case 4
					'Dim result As DialogResult
					
					'calCondList = Me.GetCalibrationConditions(Me.CalibrationNumber)
					'calCondListForm = New PerfPWCalCondListForm(calCondList)
					'result = calCondListForm.ShowDialog()
					'calCondListForm.Dispose()
					'IF (result = DialogResult.OK) Then
						PWC_FACalcs(ErrorNo, True, True)
					'End If
				End Select

				Select Case ErrorNo
				Case 0
					'No error, Nothing to do
				Case Else
                    Dim Str1 As String = Nothing
                    Dim Str2 As String = Nothing

					PWC_GetErrorStrings(ErrorNo, Str1, Str2)
					Throw New PerfPWException(ErrorNo, Str1, Str2)
				End Select
			Next curStep
		End Sub

		Public Overrides Sub GetAllFAResults(ByRef Headers As String(), ByRef PlotVals() as String)
			PWC_GetAllFAResults(Headers, PlotVals)
		End Sub
		
		Public Overrides Sub GetAllResults(ByRef Headers As String(), ByRef PlotVals() as single)
			PWC_GetAllResults(Headers, PlotVals)
		End Sub
		
		Public Overrides Sub GetBuildSpec(ByRef buildSpecNumber As Integer)
			PWC_GetBuildSpec(buildSpecNumber)
		End Sub
		
		Public Sub GetBSList(ByRef BSList As String())
			Dim ErrorNo As Integer

			PWC_GetBSList(ErrorNo, BSList)
			Select Case ErrorNo
			Case 0
				'No Error, Nothing to do.
			Case Else
                Dim Str1 As String = Nothing
                Dim Str2 As String = Nothing

				PWC_GetErrorStrings(ErrorNo, Str1, Str2)
				Throw New PerfPWException(ErrorNo, Str1, Str2)
			End Select
		End Sub 

		Public Overrides Function GetConditionOutputFileName() As String
            Dim engineSerialNumber As String = Nothing
			Dim engineBuildNumber As Integer
			Dim fileName As String

			PWC_GetEngine(engineSerialNumber)
			PWC_GetBuild(engineBuildNumber)
			fileName = "OUT_" + engineSerialNumber + "_" + Format(engineBuildNumber, "0000") + "_" + _
				Format(ConditionNumber, "0000") + ".DAT"
			Return fileName
		End Function

		Public Overrides Sub GetFArResult(label As String, ByRef value As Single)
			PWC_GetFArResult(label, value)
		End Sub

		Public Overrides Function GetFinalAcceptanceOutputFileName() As String
            Dim engineSerialNumber As String = Nothing
			Dim engineBuildNumber As Integer
			Dim fileName As String

			PWC_GetEngine(engineSerialNumber)
			PWC_GetBuild(engineBuildNumber)
			fileName = "OUTFA_" + engineSerialNumber + "_" + Format(engineBuildNumber, "0000") + ".DAT"
			Return fileName
		End Function

		Public Overrides Function GetNextCalibrationNumber() As Integer
            Dim calibrationList() As Integer = Nothing
			Dim currentCalibrationNumber As Integer = 0
			Dim errorNo As Integer
			
			PWC_LoadConditionStat(errorNo)
			If (errorNo = 0) Then
				
				PWC_GetCalibrationList(calibrationList)
				If (calibrationList.GetUpperBound(0) > 0) Then
					Array.Sort(calibrationList)
					currentCalibrationNumber = calibrationList(calibrationList.GetUpperBound(0))
				End If
			End If
			Return (currentCalibrationNumber+1)
		End Function
		
		Public Overrides Function GetNextConditionNumber() As Integer
			Dim conditionNumber As Integer = 0
			Dim conditionFilenames As String()
            Dim engineSerialNumber As String = Nothing
			Dim engineBuildNumber As Integer

			PWC_GetEngine(engineSerialNumber)
			PWC_GetBuild(engineBuildNumber)		
			conditionFilenames = Directory.GetFiles(Me.DataPath, "P" + _
				engineSerialNumber + "_" + Format(engineBuildNumber, "0000") + "_*.DAT")
			If (conditionFilenames.GetLength(0) > 0) Then
				'
				'Defines a regular expression that match Pssssss_bbbb_cccc.DAT
				'to extract the condition number.
				'Where:
				'  - ssssss: Engine Serial Number
				'  - bbbb: Engine Build Number
				'  - cccc: Condition Number
				'
				'Dim r As New Regex("P" + engineSerialNumber + "_" + _
				'	Format(engineBuildNumber, "0000") + "_(?<conNumber>\d+)\.DAT$")
				Dim r As New Regex("_(?<conNumber>\d+)\.DAT$")
				Dim m As Match
	
				Array.Sort(conditionFilenames)
				m = r.Match(conditionFilenames(conditionFilenames.GetUpperBound(0)))
				If (m.Success) Then
					conditionNumber = integer.Parse(m.Groups("conNumber").Value)
				End If
			End If
			Return (conditionNumber + 1)
		End Function
			
		Public Overrides Sub GetResult(label As String, ByRef value As String)
			PWC_GetResult(label, value)
		End Sub
		
		Public Sub GetSB(buildSpec As String, ByRef sbList As String(), ByRef engineUp As String)
			Dim ErrorNo As Integer
			
			PWC_GetSB(ErrorNo, buildSpec, sbList, engineUp)
			Select Case ErrorNo
			Case 0
				'No Error, Nothing to do.
			Case Else
                Dim Str1 As String = Nothing
                Dim Str2 As String = Nothing

				PWC_GetErrorStrings(ErrorNo, Str1, Str2)				
				Throw New PerfPWException(ErrorNo, Str1, Str2)
			End Select
		End Sub
		
		Public Overrides Function GetSupportedEngineModels() As EngineModel()
			return supportedEngineModels
		End Function

		Public Sub GetUnitData()
			Dim ErrorNo As Integer

			PWC_GetUnitData(ErrorNo)
			Select Case ErrorNo
			Case 0
				'No Error, Nothing to do.
			Case Else
                Dim Str1 As String = Nothing
                Dim Str2 As String = Nothing

				PWC_GetErrorStrings(ErrorNo, Str1, Str2)				
				Throw New PerfPWException(ErrorNo, Str1, Str2)
			End Select
		End Sub 

		Public Overrides Sub ProcessCondition()
			Dim ErrorNo As Integer

			PWC_Process(ErrorNo, TRUE, TRUE)
			Select Case ErrorNo
			Case 0
				'No error, nothing to do
			Case Else
                Dim Str1 As String = Nothing
                Dim Str2 As String = Nothing

				If ((ErrorNo = 22) Or (ErrorNo = 24)) Then
					If (Me.Calibration) Then
						'MsgBox("ErrorNo: " + ErrorNo.ToString())
						'
						'Calculation error
						'Make sure condition is not "On" for current Calibration
						'
						Dim dummyErrorNo As Integer
						Dim calCondList() As PerfPWCalConditionListItem
						Dim curCondition As PerfPWCalConditionListItem
						
						PWC_LoadConditionStat(dummyErrorNo)
						If (dummyErrorNo <> 31 And dummyErrorNo <> 33) Then
							calCondList = Me.GetCalibrationConditions(Me.CalibrationNumber)
						
							For Each curCondition In calCondList
								If (Me.ConditionNumber = curCondition.ConditionNumber) Then
									PWC_ConditionOff(dummyErrorNo, Me.ConditionNumber)
									PWC_SaveConditionStat(dummyErrorNo)
									Exit For
								End If
							Next
						End If
					End If
				End If
				PWC_GetErrorStrings(ErrorNo, Str1, Str2)
				Throw New PerfPWException(ErrorNo, Str1, Str2)
			End Select
		End Sub


		Public Overrides Sub PutValue(Label As String, Value As String, Index As String)
			If NOT (PWC_PutValue(Label, Value, Index)) Then
				Throw New PerfPWException(-999,"Failed to Put Value", label + "(" + _
					Index + "): " + Value)
			End If
					
		End Sub

		Public Overrides Sub SaveParameterUnitData()
			Dim ErrorNo As Integer

			PWC_SaveUnitData(ErrorNo)
			Select Case ErrorNo
			Case 0
				'No Error, Nothing to do.
			Case Else
                Dim Str1 As String = Nothing
                Dim Str2 As String = Nothing

				PWC_GetErrorStrings(ErrorNo, Str1, Str2)				
				Throw New PerfPWException(ErrorNo, Str1, Str2)
			End Select			
		End Sub

		Public Overrides Sub SetEngineStatus(engineStatus As String)
			PWC_SetStatus(engineStatus)
		End Sub

		Public Overrides Sub SetParameterUnitValue(parameter As String, unit As String)
			If NOT (PWC_PutUnitValue(parameter, unit)) Then
				Throw New PerfPWException(-999,"Failed to set Parameter Unit", parameter + _
					" (" + unit + ")")
			End If
		End Sub
		
		'********************************************************************
		'* Protected Members ************************************************
		'********************************************************************

		Protected Sub SetEngineModel(engineModel As EngineModelPW)
			Dim done As Boolean
			Dim buildSpecForm As PerfPWBuildSpecChooser
            Dim serviceBulletinForm As PerfPWServiceBulletinChooser = Nothing
			Dim buildSpec As EngineModelPW.BuildSpec
			Dim SB1 As String = ""
			Dim SB2 As String = ""
			Dim ErrorNo As Integer

			If (engineModel.pwBuildSpec.GetUpperBound(0) > 0) Then
				buildSpecForm = New PerfPWBuildSpecChooser(engineModel.pwBuildSpec)
				buildSpecForm.TopMost = True

				buildSpecForm.ShowDialog()
				
				buildSpec = buildSpecForm.BuildSpecSelection
			Else
				buildSpec = engineModel.pwBuildSpec(0)
			End If

			If (buildSpec.pwSBList.GetUpperBound(0) > -1) Then
				serviceBulletinForm = New PerfPWServiceBulletinChooser(buildSpec.pwSBList)
				serviceBulletinForm.TopMost = True
			End If

			While (Not done)
				If ( Not serviceBulletinForm Is Nothing) Then
					serviceBulletinForm.ShowDialog()
					
					SB1 = serviceBulletinForm.SBSelection(0)
					If (serviceBulletinForm.SBSelection.GetUpperBound(0) > 0) Then
						SB2 = serviceBulletinForm.SBSelection(1)
					End If
				Else
					done = True
				End If
				
				PWC_LoadBuildSpec(ErrorNo, buildSpec.pwBuildSpec, SB1, SB2)
				
				Select Case ErrorNo
				Case 0
					'No Error, Nothing to do
					done = True
				Case Else
                Dim Str1 As String = Nothing
                Dim Str2 As String = Nothing

					PWC_GetErrorStrings(ErrorNo, Str1, Str2)
					If ((Not serviceBulletinForm Is Nothing) And _
						((ErrorNo = 9) Or (ErrorNo = 27))) Then
						Microsoft.VisualBasic.MsgBox(Str2, MsgBoxStyle.OKOnly Or MsgBoxStyle.Exclamation, Str1)
					Else
						Throw New PerfPWException(ErrorNo, Str1, Str2)
					End If
				End Select
			End While
			
			If ( Not serviceBulletinForm Is Nothing) Then
				serviceBulletinForm.Dispose()
			End If
		End Sub

		Protected MustOverride Sub PWC_ConditionOff(ByRef ErrorNo As Integer, ConditionNo As Integer)
		Protected MustOverride Sub PWC_ConditionOn(ByRef ErrorNo As Integer, ConditionNo As Integer)
		Protected MustOverride Sub PWC_FACalcs(ByRef ErrorNo As Integer, Save As Boolean, Output As Boolean)
		Protected MustOverride Sub PWC_GetAllFAResults(ByRef Headers() As String, ByRef PlotVals() As String)
		Protected MustOverride Sub PWC_GetAllResults(ByRef Headers() As String, ByRef PlotVals() As single)
		Protected MustOverride Sub PWC_GetBSList(ByRef ErrorNo As Integer, ByRef BSList() As String)
		Protected MustOverride Sub PWC_GetBuild(ByRef buildNumber As Integer)
		Protected MustOverride Sub PWC_GetBuildSpec(ByRef buildSpecNumber As Integer)
		Protected MustOverride Sub PWC_GetCalibrationList(ByRef calibrationList() As Integer)
		Protected MustOverride Sub PWC_GetCalibrationNumber(ByRef calibrationNumber As Integer)
		Protected MustOverride Sub PWC_GetCalibrationStatus(ByRef calibrationStatus As String)
		Protected Mustoverride Sub PWC_GetConditionList(calNo As Integer, _
			ByRef conditionNumbers() As Integer, ByRef descriptions() As String, ByRef statuses() As String)
		Protected MustOverride Sub PWC_GetConditionNumber(ByRef conditionNumber As Integer)
		Protected MustOverride Sub PWC_GetEngine(ByRef engineSerialNumber As String)
		Protected MustOverride Sub PWC_GetEngineModel(ByRef EngineModel As String)
		Protected MustOverride Sub PWC_GetErrorStrings(ErrorNo As Integer, ByRef Str1 As String, ByRef Str2 As String)
		Protected MustOverride Sub PWC_GetFArResult(label As String, ByRef value As Single)
		Protected MustOverride Sub PWC_GetResult(label As String, ByRef value As String)
		Protected MustOverride Sub PWC_GetSB(ByRef ErrorNo As Integer, buildSpec As String, ByRef sbList As String(), ByRef engineUp As String)
		Protected MustOVerride Sub PWC_GetUnitData(ByRef ErrorNo As Integer)
		Protected MustOverride Sub PWC_LoadBuildSpec(ByRef ErrorNo As Integer, buildSpec As String, SB1 As String, SB2 As String)
		Protected MustOverride Sub PWC_LoadConditionStat(ByRef ErrorNo As Integer)
		Protected MustOverride Sub PWC_Process(ByRef ErrorNo As Integer, Save As Boolean, Output As Boolean)
		Protected MustOverride Function PWC_PutUnitValue(Label As String, Unit As String) As Boolean
		Protected MustOverride Function PWC_PutValue(Label As String, Value As String, Index As String) As Boolean
		Protected MustOverride Sub PWC_SaveConditionStat(ByRef ErrorNo As Integer)
		Protected MustOverride Sub PWC_SaveUnitData(ByRef ErrorNo As Integer)
		Protected MustOverride Sub PWC_SetCalibrationNumber(calibrationNumber As Integer)
		Protected MustOverride Sub PWC_SetCalibrationOff()
		Protected MustOverride Sub PWC_SetCalibrationOn()
		Protected MustOverride Sub PWC_SetConditionNumber(conditionNumber As Integer)
		Protected MustOverride Sub PWC_SetConfigurationPath(Path As String)
		Protected MustOverride Sub PWC_SetDataPath(Path As String)
		Protected MustOverride Sub PWC_SetStatus(engineStatus As String)

		'********************************************************************
		'* Private Members **************************************************
		'********************************************************************

		Private Function FixPath(Path As String) As String
			Dim DriveLetterSpecPosition As Integer = Path.LastIndexOf(":")
			Dim FixedPath As String

			If (DriveLetterSpecPosition <> -1) Then
				FixedPath = Path.SubString(DriveLetterSpecPosition+1)
			Else
				FixedPath = Path
			End If
			FixPath = FixedPath
		End Function

		Private Function GetCalibrationConditions(calNo As Integer) As PerfPWCalConditionListItem()
			Dim calCondListItems() As PerfPWCalConditionListItem
            Dim condNumbers() As Integer = Nothing
            Dim descs() As String = Nothing
            Dim statuses() As String = Nothing
			Dim i As Integer

			PWC_GetConditionList(calNo, condNumbers, descs, statuses)

			ReDim calCondListItems(condNumbers.GetUpperBound(0))
			For i = 0 To condNumbers.GetUpperBound(0)
				calCondListItems(i) = _
					New PerfPWCalConditionListItem( Me, condNumbers(i),	_
						Single.Parse(descs(i).SubString(4, 14)), _
						Single.Parse(descs(i).SubString(18, 14)), _
						statuses(i).Trim() = "ON")
			Next i

			Return calCondListItems
		End Function

		Private Function NumberOfActiveConditions(calNo As Integer) As Integer
            Dim condNumbers() As Integer = Nothing
            Dim descs() As String = Nothing
            Dim statuses() As String = Nothing
			Dim i As Integer, numActiveCondition As Integer = 0
			
			PWC_GetConditionList(calNo, condNumbers, descs, statuses)
			
			For i = 0 To statuses.GetUpperBound(0)
				If (statuses(i).Trim() = "ON") Then
					numActiveCondition+= 1
				End If
			Next i
			Return numActiveCondition
		End Function

		Protected supportedEngineModels() As EngineModelPW
		Private itsConfigurationPath As String
		Private itsDataPath As String
		Private itsEngineModel As EngineModelPW
	End Class

	'****************************************************************************
	'* Public Class PerfPWException *********************************************
	'****************************************************************************

	Public Class PerfPWException
		Inherits System.ApplicationException

		'********************************************************************
		'* Public Constructors **********************************************
		'********************************************************************

		Public Sub New(ErrorNo As Integer, ErrorCategory As String, ErrorDescription As String)
			MyBase.New(ErrorCategory + VbCr + VbLf + ErrorDescription)
			itsErrorCategory = ErrorCategory
			itsErrorDescription = ErrorDescription
			itsErrorNo = ErrorNo
		End Sub

		'********************************************************************
		'* Public Properties ************************************************
		'********************************************************************

		Public ReadOnly Property ErrorCategory As String
			Get
				return itsErrorCategory
			End Get
		End Property

		Public ReadOnly Property ErrorDescription As String
			Get
				return itsErrorDescription
			End Get
		End Property

		Public ReadOnly Property ErrorNo As Integer
			Get
				return itsErrorNo
			End Get
		End Property

		'********************************************************************
		'* Private Members **************************************************
		'********************************************************************

		Private itsErrorCategory As String
		Private itsErrorDescription As String
		Private itsErrorNo As Integer
	End Class

	'****************************************************************************
	'* Private Class PerfPWConditionListItem *************************************
	'****************************************************************************

	Public Class PerfPWCalConditionListItem

		'********************************************************************
		'* Public Constructors **********************************************
		'********************************************************************

		Public Sub New(newPerf As PerfPW, newConditionNumber As Integer, _
			newThrust As Single, newT45 As Single, newActiveStatus As Boolean)
			
			Me.myPerf = newPerf
			Me.myConditionNumber = newConditionNumber
			Me.myThrust = newThrust
			Me.myT45 = newT45
			Me.myActiveStatus = newActiveStatus
		End Sub

		'********************************************************************
		'* Public Properties ************************************************
		'********************************************************************

		Public ReadOnly Property ConditionNumber As Integer
			Get
				Return myConditionNumber
			End Get
		End Property
		
		Public ReadOnly Property Thrust As Single
			Get
				Return myThrust
			End Get
		End Property
		
		Public ReadOnly Property T45 As Single
			Get
				Return myT45
			End Get
		End Property
		
		Public Property Active As Boolean
			Get
				Return myActiveStatus
			End Get
			Set (ByVal Value As Boolean)
				If (Value) Then
					Me.myPerf.ConditionOn(Me.ConditionNumber)
				Else
					Me.myPerf.ConditionOff(Me.ConditionNumber)
				End If
			End Set
		End Property
		
		Private myConditionNumber As Integer
		Private myT45 As Single
		Private myThrust As Single
        Private myActiveStatus As Boolean
		Private myPerf As PerfPW
	End Class
	

	Class PerfPWServiceBulletinChooser
		Inherits Form

		'********************************************************************
		'* Public Constructors **********************************************
		'********************************************************************

		Public Sub New(SBList As String())
			Dim i As Integer
			Dim nextTopPosition As Integer
			ReDim itsSBSelection(SBList.GetLength(0)-1)
			ReDim groupBoxArray(SBList.GetLength(0)-1)		'A GroupBox per Service Bulletin

			OKButton = New Button()

			For i = 0 To itsSBSelection.GetUpperBound(0)
				itsSBSelection(i) = ""
			Next
										
			Me.Width = 600
										
			instructionLabel = New Label()
			instructionLabel.TextAlign = ContentAlignment.MiddleCenter
			instructionLabel.Text = "PLEASE INDICATE IF YOU ARE PRE OR POST" + VbCrLf + _
				"THE FOLLOWING SERVICE BULLETINS"
			instructionLabel.Top = 15
			instructionLabel.Size = New Size(instructionLabel.PreferredWidth, instructionLabel.PreferredHeight * 2)
			instructionLabel.Anchor = AnchorStyles.Top
			Me.Controls.Add(instructionLabel)
			
			nextTopPosition = (instructionLabel.Bottom + 15)
			
			For i = 0 To SBList.GetUpperBound(0)
				groupBoxArray(i) = Me.BuildSBGroupBox(SBList(i))
				groupBoxArray(i).Top = nextTopPosition
				
				Me.Controls.Add(groupBoxArray(i))
				
				nextTopPosition = (groupBoxArray(i).Bottom + 15)
			Next

			OKButton.Text = "OK"
			OKButton.Top = nextTopPosition
			Me.ClientSize = New Size(groupBoxArray(0).Right + 15, OKButton.Bottom + 15)
            OKButton.Left = Convert.ToInt32(((Me.ClientSize.Width - OKButton.Width) / 2))
			OKButton.DialogResult = DialogResult.OK
			OKButton.Enabled = False
			
			Me.Controls.Add(OKButton)
			
			Me.Text = "Service Bulletin Selection"
            instructionLabel.Left = Convert.ToInt32(((Me.ClientSize.Width - instructionLabel.Width) / 2))
			Me.FormBorderStyle = FormBorderStyle.FixedDialog
			Me.ControlBox = False
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.AcceptButton = OKButton
			
		End Sub

		Public ReadOnly Property SBSelection As String()
			Get
				Return itsSBSelection
			End Get
		End Property

		Private Function BuildSBGroupBox(SB As String) As GroupBox
			Dim index, nextLeft As Integer
			Dim SBGroupBox As GroupBox
            Dim SBRadioButton As RadioButton = Nothing
			
			SBGroupBox = New GroupBox()
			SBGroupBox.Name = SB
			SBGroupBox.Text = "Service Bulletin: " + SB.Trim()

			nextLeft = 15

			For index = 0 To 2
				SBRadioButton = New RadioButton()
				SBRadioButton.Left = nextLeft
				SBRadioButton.Width = radioButtonWidths(index)
				SBRadioButton.Top = 15
				SBRadioButton.Name = radioButtonNames(index)
				SBRadioButton.Text = radioButtonText(index)

				SBGroupBox.Controls.Add(SBRadioButton)
				AddHandler SBRadioButton.CheckedChanged, AddressOf Me.OnCheckedChanged
				nextLeft = (SBRadioButton.Right + 15)
			Next
			SBRadioButton.Checked = TRUE
			SBGroupBox.Size = New Size(nextLeft, SBRadioButton.Bottom + 15)
			SBGroupBox.Left = 15
			
			Return SBGroupBox
		End Function
		
		Private Sub OnCheckedChanged(sender As Object, e As EventArgs)
			Dim enableButton As Boolean = True
			Dim index As Integer
			Dim theRadioButton As RadioButton
			Dim SBGroupBox As GroupBox
			
            theRadioButton = CType(sender, RadioButton)
			
			If (theRadioButton.Checked) Then
                SBGroupBox = CType(theRadioButton.Parent, GroupBox)
				
				For index = 0 To groupBoxArray.GetUpperBound(0)
					If (Not groupBoxArray(index) Is Nothing) Then
						If (SBGroupBox Is groupBoxArray(index)) Then
							If (theRadioButton.Name = "Unselected") Then
								itsSBSelection(index) = ""
							Else
								itsSBSelection(index) = (theRadioButton.Name + SBGroupBox.Name)
							End If
						End If
					End If
				Next
			End If
			
			For index = 0 To itsSBSelection.GetUpperBound(0)
				If (itsSBSelection(index) = "") Then
					enableButton = False
				End If
			Next
			OKButton.Enabled = enableButton
		End Sub
		
		Private instructionLabel As Label
		Private itsSBSelection As String()
		Private groupBoxArray As GroupBox()
		Private OKButton As Button
		Private Shared radioButtonNames As String() = {"PRE", "PST", "Unselected"}
		Private Shared radioButtonText As String() = {"PRE", "POST", "Unselected"}
		Private Shared radioButtonWidths As Integer() = {75, 75, 80}
	End Class

	Class PerfPWBuildSpecChooser
		Inherits Form

		'********************************************************************
		'* Public Constructors **********************************************
		'********************************************************************

		Public Sub New(buildSpec As EngineModelPW.BuildSpec())
			Dim nextTopPosition As Integer

			itsBuildSpec = buildSpec

			OKButton = New Button()
									
			Me.Width = 600

			groupBox = New GroupBox
			groupBox.Left = 15
			groupBox.Text = "Build Spec/Model Selection"
			Me.Controls.Add(groupBox)
										
			instructionLabel = New Label()
			instructionLabel.TextAlign = ContentAlignment.MiddleCenter
			instructionLabel.Text = "Select Build Spec/Model from list"
			instructionLabel.Top = 15
			instructionLabel.Left = 5
			instructionLabel.Size = New Size(instructionLabel.PreferredWidth, instructionLabel.PreferredHeight * 2)
			instructionLabel.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
			groupBox.Controls.Add(instructionLabel)
			
			nextTopPosition = (instructionLabel.Bottom + 15)

			listBox = New ListBox()
			listBox.Top = nextTopPosition
			listBox.Left = 5
			listBox.Width = groupBox.ClientSize.Width - (listBox.Left * 2)
			listBox.Height = groupBox.Height - listBox.Top - listBox.Left
			For Each bs As EngineModelPW.BuildSpec In buildSpec
				listBox.Items.Add(bs.pwBuildSpec)
			Next
			AddHandler listBox.SelectedIndexChanged, AddressOf Me.OnSelectedIndexChanged
			groupBox.Controls.Add(listBox)

			nextTopPosition = (groupBox.Bottom + 15)

			OKButton.Text = "OK"
			OKButton.Top = nextTopPosition
			Me.ClientSize = New Size(groupBox.Right + 15, OKButton.Bottom + 15)
            OKButton.Left = Convert.ToInt32(((Me.ClientSize.Width - OKButton.Width) / 2))
			OKButton.DialogResult = DialogResult.OK
			OKButton.Enabled = False
			
			Me.Controls.Add(OKButton)
			
			Me.Text = "Model Selection"
            instructionLabel.Left = Convert.ToInt32(((groupBox.ClientSize.Width - instructionLabel.Width) / 2))
			Me.FormBorderStyle = FormBorderStyle.FixedDialog
			Me.ControlBox = False
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.AcceptButton = OKButton
			
		End Sub

		Public ReadOnly Property BuildSpecSelection As EngineModelPW.BuildSpec
			Get
				Return itsBuildSpecSelection
			End Get
		End Property

		Private Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
			If listBox.SelectedIndex = -1 Then
				OKButton.Enabled = False
				itsBuildSpecSelection = Nothing
			Else
				OKButton.Enabled = True
				itsBuildSpecSelection = itsBuildSpec(listBox.SelectedIndex)
			End If
		End Sub
	
		Private instructionLabel As Label
		Private itsBuildSpec		  As EngineModelPW.BuildSpec()
		Private itsBuildSpecSelection As EngineModelPW.BuildSpec
		Private groupBox As GroupBox
		Private listBox As ListBox
		Private OKButton As Button
	End Class

End Namespace

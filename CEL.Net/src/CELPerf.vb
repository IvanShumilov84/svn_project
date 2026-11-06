Namespace CEL.Perf
	Public Class BuildSpecDescriptor
		Public Sub New (itsEngineModel As String, itsBuildSpec As String)
			BuildSpec = itsBuildSpec
			EngineModel = itsEngineModel
		End Sub

		Public BuildSpec As String
		Public EngineModel As String
	End Class

	Public Class ConditionDescriptor
		Public Sub New (itsConditionNo As Integer, itsConditionIsOn As Boolean, itsParameterValues() As ParameterValue)
			ConditionNo = itsConditionNo
			ConditionIsOn = itsConditionIsOn
			ParameterValues = itsParameterValues
		End Sub

		Public ConditionNo As Integer
		Public ConditionIsOn As Boolean
		Public ParameterValues() As ParameterValue
	End Class

	Public Class CalibrationSet
		Public Sub New (itsCalibrationNo As Integer)
			CalibrationNo = itsCalibrationNo
		End Sub

		Public CalibrationNo As Integer
		Public Conditions() As ConditionDescriptor
	End Class

	Public Class ParameterValue
		Public Sub New (itsHeader As String, itsValue As Single)
			Header = itsHeader
			Value = itsValue
		End Sub

		Public Sub New (itsHeader As String, itsValue As String)
			Header = itsValue
			Single.Parse(itsValue)
		End Sub

		Public Header As String
		Public Value As Single
	End Class

	'****************************************************************************
	'* Public Class EngineModel *************************************************
	'****************************************************************************

	Public MustInherit Class EngineModel

		'********************************************************************
		'* Public Members ***************************************************
		'********************************************************************

		Public Overrides Function ToString() As String
            Return String.Copy(commonName)
		End Function

		Public MustOverride Function GetPerf() As Perf
		Public MustOverride Sub SetAsCurrentModelOnTest()

		'********************************************************************
		'* Protected Members ************************************************
		'********************************************************************

		Protected Sub New(newCommonName As String)
			commonName = newCommonName
		End Sub

		Protected commonName As String
	End Class

	'****************************************************************************
	'* Public Class Perf ********************************************************
	'****************************************************************************

	Public MustInherit Class Perf

		'********************************************************************
		'* Public Members ***************************************************
		'********************************************************************

		Public MustOverride Property Calibration As Boolean
		Public MustOverride Property CalibrationNumber As Integer
		Public MustOverride Property ConditionNumber As Integer
		Public MustOverride Property ConfigurationPath As String
		Public MustOverride Property DataPath As String
		Public MustOverride Sub DoFinalAcceptanceCalcs()
		Public MustOverride Sub GetAllFAResults(ByRef Headers As String(), ByRef PlotVals() As String)
		Public MustOverride Sub GetAllResults(ByRef Headers As String(), ByRef PlotVals() as single)
		Public MustOverride Sub GetBuildSpec(ByRef buildSpecNumber As Integer)
		Public MustOverride Function GetConditionOutputFileName() As String
		Public MustOverride Sub GetFArResult(label As String, ByRef value As Single)
		Public MustOverride Function GetFinalAcceptanceOutputFileName() As String
		Public MustOverride Function GetNextCalibrationNumber() As Integer
		Public MustOverride Function GetNextConditionNumber() As Integer
		Public MustOverride Sub GetResult(label As String, ByRef value As String)
		Public MustOverride Function GetSupportedEngineModels() As EngineModel()
		Public MustOverride Sub ProcessCondition()
		Public MustOverride Sub PutValue(Label As String, Value As String, Index As String)
		Public MustOverride Sub SaveParameterUnitData()
		Public MustOverride Sub SetEngineSN(serialNumber As String)
		Public MustOverride Sub SetEngineStatus(engineStatus As String)
		Public MustOverride Sub SetParameterUnitValue(parameter As String, unit As String)
		Public Shared Sub SetEngineModelOnTest(engineModel As EngineModel)
			engineModel.SetAsCurrentModelOnTest()
		End Sub
	End Class

End Namespace
	

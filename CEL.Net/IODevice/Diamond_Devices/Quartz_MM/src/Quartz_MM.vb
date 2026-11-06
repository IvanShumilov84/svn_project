'****************************************************************************************************************
'*	Name :			Quartz_MM.vb														
'*	Description :	DiamondIODevice.vb
'*
'*	Dependacy : 	
'*
'*									HYSTORY
'*
'*	Rev			Date			Name			Description
'*		        (YYYY/MM/DD)
'*
'*	1.0	         2005/11/24		N.Sangiovanni	First Release
'*
'*****************************************************************************************************************

Imports Microsoft.VisualBasic
Imports System
Imports System.Threading 
Imports System.Windows.Forms


Namespace CEL.DiamondIODevice

	'****************************************************************************
	'* Public Class Quartz_MM ***************************************************
	'****************************************************************************
	Public NotInheritable Class Quartz_MM
		Inherits DiamondIODevice

		'**********************
		'* Public Properties  *
		'**********************
		Public ReadOnly Property CounterChannels(ByVal index as Byte) As DiamondCounterChannel
			Get
				Return Me.myCounterChannels(index)
			End Get
		End Property

		Public ReadOnly Property FrequencyChannels(ByVal index as Byte) As DiamondFrequencyChannel
			Get
				Return Me.myFrequencyChannels(index)
			End Get
		End Property
		
		Public ReadOnly Property DIOChannels(ByVal index As Byte) As DiamondDIOChannel
			Get
				return myDIOChannels(index)
			End Get
		End Property


	
		'**********************
		'* Public Members 	  *
		'**********************
		Public Sub New(baseAddress As Short, intLevel As Byte, Output_Ctrl() As Byte, Gating_Ctrl() As Byte, Count_Source() As Byte)
			Dim my_dsccb 			As New DSCCB()
			Dim i	 				As Integer
			Dim ConfigurationByte 	As Byte = 0

			'Initialise Diamond Library
			MyBase.Initialise()
												
			'Initialise Quartz-MM card
			my_dsccb.boardType 		= DSC_BoardType.DSC_QMM
			my_dsccb.int_level 		= intLevel
			my_dsccb.base_address 	= baseAddress
			
			MyBase.Board_Initialisation(my_dsccb)
			
			For i=0 To 4
				myCounterChannels(i) 	= New DiamondCounterChannel()
				myFrequencyChannels(i) 	= New DiamondFrequencyChannel()
				myOldCounter(i)			= Convert.ToUInt16(65535)
				myOldTime(i)			= 0
			Next
			
			For i=0 To 15
				'0 to 7  = Input
				'8 to 15 = Output
				myDIOChannels(i) = New DiamondDIOChannel()
				If i < 8 Then
					Me.DIOChannels(i).DIOInput = True
				Else
					Me.DIOChannels(i).DIOInput = False
				End If
			Next

			'Set Master Mode Register
			Me.Set_MMR()
			
			'Set Counter Mode Register
			Me.Set_CMR(Output_Ctrl, Gating_Ctrl, Count_Source)
			
			'Load Counter Load Register
			Me.Set_LoadRegister()
		End Sub

	
		Public Sub Start_Scan()
			Dim CounterAutoEvent 		As New AutoResetEvent(False)
			Dim CounterTimerDelegate 	As TimerCallback 	= AddressOf OnCounterTimer
			Dim FrequencyAutoEvent 		As New AutoResetEvent(False)
			Dim FrequencyTimerDelegate 	As TimerCallback 	= AddressOf OnFrequencyTimer

			'Load and arm counter
			Me.LoadArm_Counter()
			
			aCounterTimer 	= New System.Threading.Timer(CounterTimerDelegate,CounterAutoEvent,0,1)
			aFrequencyTimer = New System.Threading.Timer(FrequencyTimerDelegate,FrequencyAutoEvent,0,500)
		End Sub

		
		Public Sub Stop_Scan()			
			aCounterTimer.Dispose()
			aFrequencyTimer.Dispose()
		End Sub
		
		
		'**********************
		'* Private Members 	  *
		'**********************
		Private Sub Counter_Read()
			Dim errorParams 	As ERRPARAMS
			Dim i				As Integer
			Dim myDate			As DateTime = DateTime.Now
			

			For i=1 To 5
				If (DSCUDVB.dscQMMSingleCounterControl(Me.dscb, i, DSC_QMM_Action.QMM_ACTION_SAVE) <> DSC_ErrorCodes.DE_NONE) Then
					DSCUDVB.dscGetLastError(errorParams)					
					Throw New DiamondException("dscQMMSingleCounterControl",errorParams.ErrCode, errorParams.ErrString)
				End If
				
				If (DSCUDVB.dscQMMReadHoldRegister(Me.dscb, i, myCounterChannels(i-1).CounterValue) <> DSC_ErrorCodes.DE_NONE) Then
					DSCUDVB.dscGetLastError(errorParams)					
					Throw New DiamondException("dscQMMSingleCounterControl",errorParams.ErrCode, errorParams.ErrString)
				Else
					myCounterChannels(i-1).NbrTicks = myDate.Ticks
				End If
			Next
		End Sub

		Protected Sub DIO_Scan()
			Dim errorParams As ERRPARAMS
			Dim i			As Integer
			Dim SetByte 	As Byte
			
						
			'0 to 7  = Input
			'8 to 15 = Output
			'INPUT
			If (DSCUDVB.dscDIOInputByte(Me.dscb, 0, SetByte) <> DSC_ErrorCodes.DE_NONE) Then
				DSCUDVB.dscGetLastError(errorParams)
				Throw New DiamondException("dscDIOInputByte", errorParams.ErrCode, errorParams.ErrString)
			Else				
				For i=0 To 7
					myDIOChannels(i).DIOValue = Convert.ToBoolean(SetByte And Math.Pow(2,i))							
				Next						
			End If
						
			'OUTPUT 
			SetByte = 0
			For i=8 To 15				
				SetByte += Convert.ToInt16(myDIOChannels(i).DIOValue) * Math.Pow(2,i-8)
			Next				
						
			If (DSCUDVB.dscDIOOutputByte(Me.dscb, 0, SetByte) <> DSC_ErrorCodes.DE_NONE) Then
				DSCUDVB.dscGetLastError(errorParams)
				Throw New DiamondException("dscDIOOutputByte", errorParams.ErrCode, errorParams.ErrString)
			End If		
		End Sub


		'Private Sub Frequency_Read()
		'	Dim errorParams 	As ERRPARAMS
		'	Dim i				As Integer
		'	Dim Period 			As Single

		'	For i=1 To 5
		'		If (DSCUDVB.dscQMMMeasurePeriod(Me.dscb, 0, period) <> DSC_ErrorCodes.DE_NONE) Then
		'			DSCUDVB.dscGetLastError(errorParams)
		'			If errorParams.ErrCode = 11 Then
		'				If (DSCUDVB.dscQMMMeasurePeriod(Me.dscb, 1, period) <> DSC_ErrorCodes.DE_NONE) Then
		'					DSCUDVB.dscGetLastError(errorParams)
		'					If errorParams.ErrCode = 11 Then
		'						myFrequencyChannels(i-1).FrequencyValue = 0
		'					ElseIf errorParams.ErrCode <> 20 Then
		'						Throw New DiamondException("dscQMMMeasurePeriod",errorParams.ErrCode, errorParams.ErrString)
		'					End If
		'				Else
		'					myFrequencyChannels(i-1).FrequencyValue = 1/period
		'				End If
		'			ElseIf errorParams.ErrCode <> 20 Then
		'				Throw New DiamondException("dscQMMMeasurePeriod",errorParams.ErrCode, errorParams.ErrString)
		'			End If
		'		Else					
		'			myFrequencyChannels(i-1).FrequencyValue = 1/period
		'		End If
		'	Next
		'End Sub

		Private Sub Frequency_Read()
			Dim i 			As Integer
			Dim TimeDiff 	As Double = 0
			Dim CounterDiff As Double = 0

			Try
				For i=0 To 4				
					CounterDiff = Convert.ToDouble(myOldCounter(i)) - Convert.ToDouble(myCounterChannels(i).CounterValue)
					If CounterDiff < 0 Then
						CounterDiff += 655535
					End If
					TimeDiff = Convert.ToDouble((myCounterChannels(i).NbrTicks - myOldTime(i))) * 100.0 * Math.Pow(10, -9)
				
					myFrequencyChannels(i).FrequencyValue = CounterDiff / TimeDiff
				
					myOldCounter(i) = myCounterChannels(i).CounterValue
					myOldTime(i) = myCounterChannels(i).NbrTicks
				Next
			Catch e As exception
				MessageBox.Show(e.ToString())
			End Try
		End Sub


		Private Sub LoadArm_Counter()
			Dim errorParams 	As ERRPARAMS
			Dim i				As Integer
						
			For i=1 To 5
				If (DSCUDVB.dscQMMSingleCounterControl(Me.dscb, i, DSC_QMM_Action.QMM_ACTION_LOAD_AND_ARM) <> DSC_ErrorCodes.DE_NONE) Then
					DSCUDVB.dscGetLastError(errorParams)					
					Throw New DiamondException("dscQMMSingleCounterControl",errorParams.ErrCode, errorParams.ErrString)
				End If
			Next			
		End Sub


		Private Sub OnCounterTimer(stateInfo As Object)
			Dim Message As String
			
			Try
				Me.Counter_Read()	
				Me.DIO_Scan()
			Catch e As CEL.DiamondIODevice.DiamondException
				Message = e.FunctionName + " (" + e.ErrorCode.ToString() + ")" + VbCrLf + e.ErrorDescription
				MessageBox.Show(Message, "ERROR")
			End Try
		End Sub


		Private Sub OnFrequencyTimer(stateInfo As Object)
			Dim Message As String
			
			Try
				Me.Frequency_Read()
			Catch e As CEL.DiamondIODevice.DiamondException
				Message = e.FunctionName + " (" + e.ErrorCode.ToString() + ")" + VbCrLf + e.ErrorDescription
				MessageBox.Show(Message, "ERROR")
			End Try
		End Sub


		Private Sub Set_CMR(Output_Ctrl() As Byte, Gating_Ctrl() As Byte, Count_Source() As Byte)
			Dim dscqmm_cmr	 	As DSCQMM_CMR
			Dim errorParams 	As ERRPARAMS
			Dim i				As Integer


			For i=1 To 5
				dscqmm_cmr.counter = i
				dscqmm_cmr.count_direction = 0
				dscqmm_cmr.cycle = 1
				dscqmm_cmr.special_gate = 0
				dscqmm_cmr.reload_source = 0
				dscqmm_cmr.output_control = Output_Ctrl(i-1)
				dscqmm_cmr.gating_control = Gating_Ctrl(i-1)
				dscqmm_cmr.count_type = 0				
				dscqmm_cmr.active_source_edge = 0
				dscqmm_cmr.count_source = Count_Source(i-1)
				
				
				If (DSCUDVB.dscQMMSetCMR(Me.dscb, dscqmm_cmr) <> DSC_ErrorCodes.DE_NONE) Then
					DSCUDVB.dscGetLastError(errorParams)					
					Throw New DiamondException("dscQMMSetCMR",errorParams.ErrCode, errorParams.ErrString)
				End If
			Next
		End Sub

		Private Sub Set_LoadRegister()
			Dim errorParams 	As ERRPARAMS
			Dim i				As Integer
			
									
			For i=1 To 5
				If (DSCUDVB.dscQMMSetLoadRegister(Me.dscb, i,Convert.ToUInt16(65535)) <> DSC_ErrorCodes.DE_NONE) Then
					DSCUDVB.dscGetLastError(errorParams)					
					Throw New DiamondException("dscQMMSetLoadRegister",errorParams.ErrCode, errorParams.ErrString)
				End If
			Next
		End Sub
		
		Private Sub Set_MMR()
			Dim dscqmm_mmr	 	As DSCQMM_MMR
			Dim errorParams 	As ERRPARAMS

			dscqmm_mmr.counter_group 	= DSC_Counter_Group.QMM_COUNTER_GROUP_1 
			dscqmm_mmr.compare1_enable	= 0
			dscqmm_mmr.compare2_enable	= 0
			dscqmm_mmr.fout_divider		= 1
			dscqmm_mmr.fout_source		= DSC_QMM_Source.QMM_SOURCE_F3_40KHZ
			dscqmm_mmr.tod_mode			= DSC_QMM_TOD.QMM_TOD_DISABLED 
			
			If (DSCUDVB.dscQMMSetMMR(Me.dscb, dscqmm_mmr) <> DSC_ErrorCodes.DE_NONE) Then
				DSCUDVB.dscGetLastError(errorParams)					
				Throw New DiamondException("dscQMMSetMMR",errorParams.ErrCode, errorParams.ErrString)
			End If			
		End Sub
		
		Private aCounterTimer 		 	As System.Threading.Timer
		Private aFrequencyTimer 	 	As System.Threading.Timer
		Private myCounterChannels(4) 	As DiamondCounterChannel
		Private myFrequencyChannels(4) 	As DiamondFrequencyChannel
		Private myDIOChannels(16) 		As DiamondDIOChannel
		Private myOldCounter(4)			As UInt16 
		Private myOldTime(4)			As Long 
	End Class
	
	
	
	'****************************************************************************
	'* Public Class DiamondCounterChannel ********************************************
	'****************************************************************************
	Public Class DiamondCounterChannel

		'**********************
		'* Public Properties  *
		'**********************
		Public Property CounterValue As UInt16
			Get
				Return Me.myValue
			End Get
		
			Set(ByVal Value As UInt16)
				If (Convert.ToDouble(Me.myValue) <> Convert.ToDouble(Value)) Then
					Me.myValue = Value
					
					Me.OnValueChanged(Value)
				End If
			End Set
		End Property


		Public Property NbrTicks As Long
			Get
				Return Me.myNbrTicks
			End Get
		
			Set(ByVal Value As Long)
				Me.myNbrTicks = Value
			End Set
		End Property

		'**********************
		'* Public Members     *
		'**********************
		Public Sub New()
			MyBase.New()
		End Sub

		Public Event ValueChanged As CounterValueChangedEventHandler
						
		'**********************
		'* Protected Members  *
		'**********************	
		Protected Sub OnValueChanged(newVal As UInt16)
			RaiseEvent ValueChanged(Me, Convert.ToDouble(65535)-Convert.ToDouble(newVal))
		End Sub

		Private myValue As UInt16 
		Private myNbrTicks As Long
	End Class



	'****************************************************************************
	'* Public Class DiamondFrequencyChannel ********************************************
	'****************************************************************************
	Public Class DiamondFrequencyChannel

		'**********************
		'* Public Properties  *
		'**********************
		Public Property FrequencyValue As Single
			Get
				Return Me.myValue
			End Get
		
			Set(ByVal Value As Single)
				'If (Me.myValue <> Value AND Convert.ToDouble(Value) <> 40000) Then
				If (Convert.ToDouble(Value) > 58 And Convert.ToDouble(Value) < 62) Then
					Me.myValue = 0.0001
					
					Me.OnValueChanged(0.0001)
				elseIf Convert.ToDouble(Value) < 20000 Then
					Me.myValue = Value
					
					Me.OnValueChanged(Value)
				End If
			End Set
		End Property


		'**********************
		'* Public Members     *
		'**********************
		Public Sub New()
			MyBase.New()
		End Sub

		Public Event ValueChanged As FrequencyValueChangedEventHandler
						
		'**********************
		'* Protected Members  *
		'**********************	
		Protected Sub OnValueChanged(newVal As Single)
			RaiseEvent ValueChanged(Me, Convert.ToDouble(newVal))
		End Sub

		Private myValue As Single = 1
	End Class


	Public Delegate Sub CounterValueChangedEventHandler(sender As Object, newVal As Double)
	Public Delegate Sub FrequencyValueChangedEventHandler(sender As Object, newVal As Double)
End Namespace

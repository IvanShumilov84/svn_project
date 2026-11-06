'****************************************************************************************************************
'*	Name :			DiamondIODevice.vb														
'*	Description :	
'*
'*	Dependacy : 	
'*
'*									HYSTORY
'*
'*	Rev			Date			Name			Description
'*		        (YYYY/MM/DD)
'*
'*	1.0	         2005/10/12		N.Sangiovanni	First Release
'*
'*****************************************************************************************************************

Imports KineticaRT.Channels
Imports System
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Windows.Forms



Namespace CEL.DiamondIODevice

	'****************************************************************************
	'* Public Class DiamondADChannel ********************************************
	'****************************************************************************
	Public Class DiamondADChannel

		'**********************
		'* Public Properties  *
		'**********************
		Public Property ADValue As Double
			Get
				Return Me.myValue
			End Get
		
			Set(ByVal Value As Double)
				If (Me.myValue <> Value) Then
					Me.myValue = Value
					
					Me.OnValueChanged(Value)
					Me.OnMeanValueChanged(Value)
				End If
			End Set
		End Property


		Public Property MeanPeriod As Integer
			Get
				Return Me.myMeanPeriod
			End Get
		
			Set
				Me.myMeanPeriod = MeanPeriod
			End Set
		End Property


		Public ReadOnly Property MeanValue As Integer
			Get
                Return Convert.ToInt32(Me.myMeanValue)
			End Get
		End Property


		'**********************
		'* Public Members     *
		'**********************
		Public Sub New()
			MyBase.New()
			
			Dim i As Integer
			
			For i=0 To 100
				myMean_Tmp(i) = 0
			Next
		End Sub


		Public Event ValueChanged As ValueChangedEventHandler
		Public Event MeanValueChanged As MeanValueChangedEventHandler
		
						
		'**********************
		'* Protected Members  *
		'**********************	
		Protected Overridable Sub OnMeanValueChanged(newVal As Double)
			Dim i 		As Integer
			Dim MeanTot As Double = 0
			
			For i=(myMeanPeriod-1) To 1 Step -1
				myMean_Tmp(i) = myMean_Tmp(i-1)
			Next
			myMean_Tmp(0) = newVal
			
			For i=0 To (myMeanPeriod - 1) Step 1
				MeanTot += myMean_Tmp(i)
			Next
			myMeanValue = MeanTot / myMeanPeriod
		
			RaiseEvent MeanValueChanged(Me, myMeanValue)
		End Sub


		Protected Overridable Sub OnValueChanged(newVal As Double)
			RaiseEvent ValueChanged(Me, newVal)
		End Sub


		Private myMeanPeriod 	As Integer 	= 1
		Private myMean_Tmp(100)	As Double
		Private myMeanValue 	As Double 	= Double.NaN
		Private myValue 		As Double 	= Double.NaN
	End Class



	'****************************************************************************
	'* Public Class DiamondDIOChannel *******************************************
	'****************************************************************************
	Public Class DiamondDIOChannel
		
		'**********************
		'* Public Properties  *
		'**********************
		Public Property DIOInput As Boolean
			Get
				Return Me.myDIOInput
			End Get
		
			Set(ByVal Value As Boolean)
				Me.myDIOInput = Value
			End Set
		End Property


		Public Property DIOValue As Boolean
			Get
				Return Me.myDIOValue
			End Get
		
			Set(ByVal Value As Boolean)
				If Me.myDIOInput = False Then
					Me.myDIOValue = Value				
				ElseIf ((Me.myDIOValue.ToString() <> Value.ToString()) And (Me.myDIOInput)) Then
					Me.myDIOValue = Value					
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

		Public Event ValueChanged As DIOValueChangedEventHandler
		
						
		'**********************
		'* Protected Members  *
		'**********************	
		Protected Overridable Sub OnValueChanged(newVal As Boolean)
			RaiseEvent ValueChanged(Me, NewVal)	
		End Sub
		


		Private myDIOInput 	As Boolean = True		
		Private myDIOValue 	As Boolean = False
	End Class



	'****************************************************************************
	'* Public Class DiamondException *********************************************
	'****************************************************************************
	Public Class DiamondException
		Inherits System.ApplicationException


		'**********************
		'* Public Properties  *
		'**********************
		Public ReadOnly Property ErrorCode As Byte
			Get
				Return myErrorCode								
			End Get
		End Property
		
		Public ReadOnly Property ErrorDescription As String
			Get
				Return myErrorDescription
			End Get
		End Property

		Public ReadOnly Property FunctionName As String
			Get
				return myFunctionName
			End Get
		End Property


		'**********************
		'* Public Members     *
		'**********************
		Public Sub New(FunctionName As String, ErrorCode As Byte, ErrorDescription As String)
			MyBase.New(FunctionName + " error: " + ErrorDescription + " (" + ErrorCode.ToString() + ")")			
			
			myFunctionName 		= FunctionName
			myErrorCode 		= ErrorCode
			myErrorDescription 	= ErrorDescription			
		End Sub

		
		Private myErrorCode 		As Byte		
		Private myErrorDescription 	As String	
		Private myFunctionName 		As String		
	End Class



	'****************************************************************************
	'* Public Class DiamondIODevice *********************************************
	'****************************************************************************
	Public MustInherit Class DiamondIODevice

		'**********************
		'* Protected Members  *
		'**********************
        Protected Sub Board_Initialisation(dsccb As sDSCCB)
            Dim errorParams As ERRPARAMS = Nothing

            'Board Initialisation
            If (DSCUDVB.dscInitBoard(dsccb.boardtype, dsccb, Me.dscb) <> DSC_ErrorCodes.DE_NONE) Then
                DSCUDVB.dscGetLastError(errorParams)
                Throw New DiamondException("dscInitBoard", errorParams.ErrCode, errorParams.ErrString)
            End If
        End Sub
		
		Overrides Protected Sub Finalize()
			DSCUDVB.dscFree()
		End Sub
		
		Protected Sub Initialise()
            Dim ErrorParams As ERRPARAMS = Nothing
			
			If Not DiamondLibInitialized Then			
				'Diamond Library Initialisation				
				If (DSCUDVB.dscInit(DSC_VERSION) <> DSC_ErrorCodes.DE_NONE)	Then
					DSCUDVB.dscGetLastError(ErrorParams)
					Throw New DiamondException("dscInit", errorParams.ErrCode, errorParams.ErrString)
				Else
                    'Me.DiamondLibInitialized = True
                    DiamondIODevice.DiamondLibInitialized = True
				End If
			End If
		End Sub
		
		Protected Sub New()
			MyBase.New()
		End Sub
			
	
		Private Shared 	DiamondLibInitialized 	As Boolean = False
		Private Const 	DSC_VERSION 			As Integer = 1424
		
		Protected dscb							As Short
	End Class



	'****************************************************************************
	'* Public Class DiamondIODevice_ADDIO ***************************************
	'****************************************************************************
	Public MustInherit Class DiamondIODevice_ADDIO
		Inherits DiamondIODevice
		
		'**********************
		'* Public Properties  *
		'**********************
		Public ReadOnly Property ADChannels(ByVal index As Byte) As DiamondADChannel
			Get
				return myADChannels(index)
			End Get
		End Property


		Public ReadOnly Property DIOChannels(ByVal index As Byte) As DiamondDIOChannel
			Get
				return myDIOChannels(index)
			End Get
		End Property


		Public Property DAOutput_Voltages(ByVal index As Byte) As double
			Get
				return myDAOutput_Voltages(index)
			End Get
	
			Set
				myDAOutput_Voltages(index) = DAOutput_Voltages(index)
			End Set
		End Property


		'**********************
		'* Protected Members  *
		'**********************		
		Protected Sub AD_AutoCAL()
			Dim curStep 		As Integer		
            Dim dscadcalparams As DSCADCALPARAMS = Nothing
			Dim ErrorCode 		As Byte		
            Dim errorParams As ERRPARAMS = Nothing
            Dim FunctionName As String = Nothing
				
			dscadcalparams.adrange 		= 4		'Range 0-10 Vdc
			dscadcalparams.boot_adrange = 0		'Boot Range +/-10 Vdc
		
			For curStep = 1 To 3 
				Select Case curStep
					Case 1
						'AutoCAL Algorithm
						FunctionName 	= "dscADAutoCal"
						ErrorCode 		= DSCUDVB.dscADAutoCal(Me.dscb, dscadcalparams)
					Case 2
						'AutoCAL Verification
						FunctionName 	= "dscADCalVerify"
						ErrorCode 		= DSCUDVB.dscADCalVerify(Me.dscb, dscadcalparams)
					Case 3
						'Tolerance 2 LSB = 306 micro V					
						If ((abs(dscadcalparams.ad_offset) > 2) Or (abs(dscadcalparams.ad_gain) > 2)) Then
							ErrorCode = 99
						Else
							ErrorCode = DSC_ErrorCodes.DE_NONE
						End If
				End Select
			
				Select Case ErrorCode
					Case DSC_ErrorCodes.DE_NONE
						'No error - Nothing to do
					Case 99
						Throw New DiamondException("Hercules_AD_AutoCAL", 99, "Values for offset and gain don't met specified tolerance")
					Case Else
						DSCUDVB.dscGetLastError(errorParams)											
						Throw New DiamondException("dscInitBoard",errorParams.ErrCode, errorParams.ErrString)
				End Select
			Next curStep
		End Sub	


		Protected Sub AD_Read()
            Dim dscadscan As sDSCADSCAN
            Dim errorParams As ERRPARAMS = Nothing
			Dim i 			As integer
			Dim sampleptr 	As IntPtr
			Dim samples() 	As Short			
			Dim voltage 	As Double
			
			
			dscadscan.low_channel 	= 0
			dscadscan.high_channel 	= 31
			dscadscan.gain 			= myADSettings.gain

			sampleptr = Marshal.AllocHGlobal(2 * (dscadscan.high_channel - dscadscan.low_channel + 1))
			

			If (DSCUDVB.dscADScan(Me.dscb, dscadscan, sampleptr) <> DSC_ErrorCodes.DE_NONE) Then
				DSCUDVB.dscGetLastError(errorParams)
				Throw New DiamondException("dscADScan", errorParams.ErrCode, errorParams.ErrString)
			Else
				samples = New Short(dscadscan.high_channel - dscadscan.low_channel) {}
        		Marshal.Copy(sampleptr, samples, 0, (dscadscan.high_channel - dscadscan.low_channel + 1))
        		Marshal.FreeHGlobal(sampleptr)				

				For i=dscadscan.low_channel To dscadscan.high_channel
					If (DSCUDVB.dscADCodeToVoltage(Me.dscb, myADSettings, samples(i), voltage) <> DSC_ErrorCodes.DE_NONE) Then
						DSCUDVB.dscGetLastError(errorParams)
						Throw New DiamondException("dscADCodeToVoltage", errorParams.ErrCode, errorParams.ErrString)
					Else
						Me.myADChannels(i).ADValue = voltage
					End If
				Next
			End If
		End Sub


		Protected Sub AD_Settings()
            Dim errorParams As ERRPARAMS = Nothing
		
			Me.myADSettings.range 				= DSC_Range.RANGE_10
			Me.myADSettings.polarity 			= DSC_Polarity.UNIPOLAR
			Me.myADSettings.gain 				= DSC_Gain.GAIN_1
            Me.myADSettings.load_cal = CByte(True)
			Me.myADSettings.addiff 				= DSC_ADDIFF.SINGLE_ENDED
			Me.myADSettings.current_channel 	= 0
			Me.myADSettings.scan_interval 		= DSC_ScanInterval.SCAN_INTERVAL_20
		
			If (DSCUDVB.dscADSetSettings(Me.dscb, Me.myADSettings) <> DSC_ErrorCodes.DE_NONE) Then
				DSCUDVB.dscGetLastError(errorParams)
				Throw New DiamondException("dscADSetSettings", errorParams.ErrCode, errorParams.ErrString)
			End If
		End Sub


		Protected Sub DA_Scan(output_voltages() as double)
            Dim dscdacs As DSCDACS = New DSCDACS
            Dim errorParams As ERRPARAMS = Nothing
			Dim i 				As Integer
            Dim Output_Codes(4) As Byte
			Dim Output_Codesptr As IntPtr
			
			
			For i=0 To 4
				dscdacs.channel_enable(i) = True
                Output_Codes(i) = CByte(output_voltages(i) / 10 * 4096)
			Next
			
			Marshal.Copy(Output_Codes,0,Output_Codesptr,4)
			dscdacs.output_codes = Output_Codesptr
			
			If (DSCUDVB.dscDAConvertScan(Me.dscb, dscdacs) <> DSC_ErrorCodes.DE_NONE) Then
				DSCUDVB.dscGetLastError(errorParams)
				Throw New DiamondException("dscDAConvertScan", errorParams.ErrCode, errorParams.ErrString)
			End If
		End Sub


		Protected Sub DA_Settings()
			Dim dscdasettings 	As DSCDASETTINGS
            Dim errorParams As ERRPARAMS = Nothing
			
			dscdasettings.polarity = DSC_Polarity.UNIPOLAR
            dscdasettings.load_cal = CByte(True)
			
            If (DSCUDVB.dscDASetSettings(CByte(Me.dscb), dscdasettings) <> DSC_ErrorCodes.DE_NONE) Then
                DSCUDVB.dscGetLastError(errorParams)
                Throw New DiamondException("dscDASetSettings", errorParams.ErrCode, errorParams.errstring)
            End If
		End Sub

		
		Protected Sub DIO_Configuration(ConfigurationByte As Byte)
			Dim config() 	As Byte
			Dim configptr 	As IntPtr
            Dim errorParams As ERRPARAMS = Nothing
			Dim i,j			As Integer
			
			
			config 	= New Byte(0) {ConfigurationByte}
			configptr = Marshal.AllocHGlobal(1)
			Marshal.Copy(config,0,configptr,1)
								
			If (DSCUDVB.dscDIOSetConfig(Me.dscb, configptr) <> DSC_ErrorCodes.DE_NONE) Then
				DSCUDVB.dscGetLastError(errorParams)
				Throw New DiamondException("dscDIOSetConfig", errorParams.ErrCode, errorParams.ErrString)
			End If	
			
            For i = 0 To (Convert.ToInt32(Me.NbrDIO / 8) - 1)
                For j = 0 To 7
                    'INPUT
                    If Me.DIODirections(i) = 0 Then
                        Me.DIOChannels(CByte(i * 8 + j)).DIOInput = True
                    ElseIf Me.DIODirections(i) = 1 Then
                        Me.DIOChannels(CByte(i * 8 + j)).DIOInput = False
                    End If
                Next
            Next
		End Sub


		Protected Sub DIO_Scan()
            Dim errorParams As ERRPARAMS = New ERRPARAMS
			Dim i,j			As Integer
			Dim SetByte 	As Byte
			
						
			'0=Input
			'1=Output
            For i = 0 To (Convert.ToInt32(Me.NbrDIO / 8) - 1)
                'INPUT
                If Me.DIODirections(i) = 0 Then
                    If (DSCUDVB.dscDIOInputByte(Me.dscb, CByte(i), SetByte) <> DSC_ErrorCodes.DE_NONE) Then
                        DSCUDVB.dscGetLastError(errorParams)
                        Throw New DiamondException("dscDIOInputByte", errorParams.ErrCode, errorParams.errstring)
                    Else
                        For j = 0 To 7
                            myDIOChannels(i * 8 + j).DIOValue = Convert.ToBoolean(SetByte And CType(Pow(2, j), Long))
                        Next
                    End If
                    'OUTPUT 
                ElseIf Me.DIODirections(i) = 1 Then
                    SetByte = 0
                    For j = 0 To 7 Step 1
                        SetByte += CByte(Convert.ToInt16(myDIOChannels(i * 8 + j).DIOValue) * Pow(2, j))
                    Next j

                    If (DSCUDVB.dscDIOOutputByte(Me.dscb, CByte(i), SetByte) <> DSC_ErrorCodes.DE_NONE) Then
                        DSCUDVB.dscGetLastError(errorParams)
                        Throw New DiamondException("dscDIOOutputByte", errorParams.ErrCode, errorParams.errstring)
                    End If
                End If
            Next
			
		End Sub

		
		Protected Sub New()
			MyBase.New()
			
			Dim i,j 	As Integer
						
			For i=0 To 31
				myADChannels(i) = New DiamondADChannel()
			Next
			
			'For i=0 To (NbrDio/8-1)
			For i=0 To 4
				For j=0 To 7
					myDIOChannels((i*8)+j) = New DiamondDIOChannel()
				Next 
			Next
		End Sub

			
		Private myADChannels(31) 	As DiamondADChannel		
        Private myADSettings As sDSCADSETTINGS
		Private myDAOutput_Voltages As Double() = {0,0,0,0}
		Private myDIOChannels(48) 	As DiamondDIOChannel		
		
		
		Protected DIODirections(6)	As Integer
		Protected NbrDIO			As Integer = 0
	End Class



	'****************************************************************************
	'* Public Class IODeviceHandlerWrapper **************************************
	'****************************************************************************
	Public Class IODeviceHandlerWrapper
		
		'**********************
		'* Public Members     *
		'**********************
		'Constructor Numeric Channel
		Public Sub New(KineticaRTNumericChannel As KineticaRT.Channels.NumericChannel)
			myKineticaRTNumericChannel = KineticaRTNumericChannel
		End Sub
		
		'Constructor Boolean Channel (DIN)
		Public Sub New(KineticaRTBooleanChannel As KineticaRT.Channels.BooleanChannel)
			myKineticaRTBooleanChannel = KineticaRTBooleanChannel
		End Sub

		'Constructor Boolean Channel (DOUT)
		Public Sub New(DIOChannel As DiamondDIOChannel)
			myDIOChannel = DIOChannel
		End Sub



		'Handler Numeric Channel
		Public Sub OnValueChanged(sender As Object, newVal As Double)
			myKineticaRTNumericChannel.Value = New KineticaRT.Values.Double(newVal)
		End Sub

		'Handler Boolean Channel (DIN)
		Public Sub OnValueChanged(sender As Object, newVal As Boolean)
			If newVal Then		
				myKineticaRTBooleanChannel.Value = KineticaRT.Values.Values.True				
			Else
				myKineticaRTBooleanChannel.Value = KineticaRT.Values.Values.False
			End If					
		End Sub
		
		'Handler Boolean Channel (DOUT)
		Public Sub OnValueChanged(sender As KineticaRT.Channels.IChannel, newVal As KineticaRT.Values.Value)
			myDIOChannel.DIOValue = Convert.ToBoolean(newVal.ToString())
		End Sub




		'**********************
		'* Private Members    *
		'**********************
		Private myKineticaRTNumericChannel 	As KineticaRT.Channels.NumericChannel
		Private myKineticaRTBooleanChannel 	As KineticaRT.Channels.BooleanChannel
		Private myDIOChannel				As DiamondDIOChannel		
	End Class

	
	Public Delegate Sub DIOValueChangedEventHandler	(sender As Object, newVal As Boolean)
	Public Delegate Sub MeanValueChangedEventHandler(sender As Object, newVal As Double)
	Public Delegate Sub ValueChangedEventHandler	(sender As Object, newVal As Double)
		

	Public Enum DSC_ADDIFF As Byte
		SINGLE_ENDED	= 0
		DIFFERENTIAL	= 1
	End Enum

	Public Enum DSC_BoardType As Byte
		DSC_DMM16 		= 0
		DSC_RMM 		= 1
		DSC_TMM 		= 2
		DSC_OPMM 		= 3
		DSC_DMM 		= 4
		DSC_SMM 		= 5
		DSC_GMM			= 6
		DSC_QMM			= 7
		DSC_ZMM			= 8
		DSC_PMM			= 9
		DSC_OMM			= 10
		DSC_RMM416		= 11
		DSC_DMM32		= 12
		DSC_DMM32AT		= 12
		DSC_EMMDIO		= 13
		DSC_RMM1612		= 14
		DSC_DMMAT		= 15
		DSC_DMM16AT		= 16
		DSC_IR104		= 17
		DSC_EMM8		= 18
		DSC_PROM		= 19
		DSC_HERCEBX 	= 20
		DSC_CPT			= 21
		DSC_DMM48		= 22
		DSC_DMM48AT		= 22
		DSC_OMMDIO		= 23
		DSC_DIO82C55	= 24
		DSC_MRC			= 24
		DSC_EMMOPTO		= 24
		DSC_TEST		= 126
		DSC_RAW			= 127
		DSC_DRVR		= 255
	End Enum


	Public Enum DSC_Counter_Group As Byte
	'Group 1 means chip 1 and counters 1-5 
 	'Group 2 means chip 2 and counters 6-10
		QMM_COUNTER_GROUP_1			= 1
		QMM_COUNTER_GROUP_2			= 2
	End Enum
	
	
	Public Enum DSC_ErrorCodes As Byte
		DE_NONE 					= 0
		DE_HW_FAILURE 				= 1
		DE_SW_FAILURE 				= 2
		DE_HW_NOT_SUPPORTED 		= 3
		DE_SW_NOT_SUPPORTED 		= 4
		DE_INVALID_PARM 			= 5
		DE_ALTERNATE_IN_PROGRESS 	= 6
		DE_NONE_IN_PROGRESS 		= 7
		DE_BUFFER_ROLLOVER 			= 8
		DE_INVALID_ERROR_CODE 		= 9
		DE_ALREADY_PAUSED 			= 10
		DE_OVERFLOW 				= 11
		DE_INVALID_FUNC 			= 12
		DE_DSCUDH_INVALID 			= 13
		DE_INVALID_BOARD 			= 14
		DE_BOARD_LIMIT_REACHED 		= 15
		DE_BOARD_BUSY 				= 16
		DE_INVALID_WINDRVR_HANDLE 	= 17
		DE_INVALID_WINDRVR_VERSION 	= 18
		DE_BAD_WINDRVR_BOARD_INIT 	= 19
		DE_OPERATION_TIMED_OUT 		= 20
		DE_INVALID_WINDRVR_KP 		= 21
		DE_THREAD_FAILURE 			= 22
	End Enum


	Public Enum DSC_Gain As Byte
		GAIN_1			= 0
		GAIN_2			= 1
		GAIN_4			= 2
		GAIN_8			= 3
		GAIN_10			= 1
		GAIN_100		= 2
		GAIN_1000		= 3
	End Enum

	Public Enum DSC_Polarity As Byte
		BIPOLAR			= 0
		UNIPOLAR		= 1
	End Enum

	Public Enum DSC_QMM_Action As Byte
		QMM_ACTION_NONE				= 0
		QMM_ACTION_ARM				= 1
		QMM_ACTION_LOAD				= 2
		QMM_ACTION_LOAD_AND_ARM		= 3
		QMM_ACTION_DISARM_AND_SAVE	= 4
		QMM_ACTION_SAVE				= 5
		QMM_ACTION_DISARM			= 6
	End Enum

	Public Enum DSC_QMM_Gating As Byte
		QMM_NO_GATING					= 0
		QMM_ACTIVE_HIGH_TC_NM1			= 1
		QMM_ACTIVE_HIGH_LEVEL_GATE_NP1	= 2
		QMM_ACTIVE_HIGH_LEVEL_GATE_NM1	= 3
		QMM_ACTIVE_HIGH_LEVEL_GATE_N	= 4
		QMM_ACTIVE_LOW_LEVEL_GATE_N		= 5
		QMM_ACTIVE_HIGH_EDGE_GATE_N		= 6
		QMM_ACTIVE_LOW_EDGE_GATE_N		= 7
	End Enum
	
	Public Enum DSC_QMM_Interval As Byte
		QMM_INTERVAL_1MS_1KHZ			= 0
		QMM_INTERVAL_10MS_100HZ			= 1
		QMM_INTERVAL_100MS_10HZ			= 2
		QMM_INTERVAL_1S_1HZ				= 3
		QMM_INTERVAL_10S_01HZ			= 4
	End Enum
	
	Public Enum DSC_QMM_Output_Ctrl As Byte
		QMM_INACTIVE_OUTPUT_LOW			= 0
		QMM_ACTIVE_HIGH_PULSE_ON_TC		= 1
		QMM_TOGGLE_ON_TC				= 2
		QMM_INACTIVE_OUTPUT_HIGH		= 4
		QMM_ACTIVE_LOW_PULSE_ON_TC		= 5
	End Enum
	
	Public Enum DSC_QMM_Source As Byte
		QMM_SOURCE_E1_TC_NM1	= 0
		QMM_SOURCE_SRC1			= 1
		QMM_SOURCE_SRC2			= 2
		QMM_SOURCE_SRC3			= 3
		QMM_SOURCE_SRC4			= 4
		QMM_SOURCE_SRC5			= 5
		QMM_SOURCE_GATE1		= 6
		QMM_SOURCE_GATE2		= 7
		QMM_SOURCE_GATE3		= 8
		QMM_SOURCE_GATE4		= 9
		QMM_SOURCE_GATE5		= 10
		QMM_SOURCE_F1_4MHZ		= 11
		QMM_SOURCE_F2_400KHZ	= 12
		QMM_SOURCE_F3_40KHZ		= 13
		QMM_SOURCE_F4_4KHZ		= 14
		QMM_SOURCE_F5_400HZ		= 15
	End Enum

	Public Enum DSC_QMM_TOD As Byte
		QMM_TOD_DISABLED		= 0
		QMM_TOD_DIVIDE_BY_5		= 1
		QMM_TOD_DIVIDE_BY_6		= 2
		QMM_TOD_DIVIDE_BY_10	= 3
	End Enum
	
	Public Enum DSC_Range As Byte
		RANGE_5			= 0
		RANGE_10		= 1
	End Enum

	Public Enum DSC_ScanInterval As Byte
		SCAN_INTERVAL_5	= 3
		SCAN_INTERVAL_9	= 4
		SCAN_INTERVAL_10= 2
		SCAN_INTERVAL_15= 1
		SCAN_INTERVAL_20= 0
	End Enum
End Namespace

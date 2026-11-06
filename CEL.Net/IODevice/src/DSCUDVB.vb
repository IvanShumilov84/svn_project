Imports System
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

'users should implement this function or use for their own user function
Public Delegate Sub DSCUserInterruptFunction(ByVal k As IntPtr)

'DSCCB 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> Public Structure sDSCCB
    Public boardtype As Byte     '/* Contains the boardtype constant */
    Public boardnum As Short     '/* The handle to the board */

    Public base_address As Short  '/* Base address of the board */

    Public int_level As Byte

    Public RMM_external_trigger As Integer
    Public RMM_external_trigger_c3 As Integer

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> Public EMM_IOAddr As Short()     'must be size of 8
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> Public EMM_Interrupt As Short()      'must be a size of 8


    'PROM specific 
    Public clkfrq0 As Byte        '/* 0 = 10Mhz, 1 = 1MHz */
    Public clkfrq1 As Byte          '/* 0 = 10Mhz, 1 = 100KHz */
    Public clksel1 As Byte          '/* 0 = internal oscillator, frequency set by ckfrq1.  1 = external clock input CLK1 */

    Public address_space As Short

    'Obsolete  Kept for backwards compatability only  
    Public clock_freq As Integer
    Public dma_level As Byte
    Public int_level1 As Byte
    Public int_level2 As Byte
    Public int_level3 As Byte
    Public fpga As Short
    Public SMM_AD_resolution As Byte
    Public RMM_mode_16bit As Integer
    Public RMM_DAC_0_3_external_enable As Integer
    Public RMM_DAC_4_7_external_enable As Integer
End Structure


'DSCADSETTINGS
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> Public Structure sDSCADSETTINGS
    Public current_channel As Byte     '/* INPUT: Which channel to read AD samples from */ 
    Public gain As Byte            '/* INPUT: 0 = gain of 1, 1 = gain of 2, 2 = gain of 4, 3 = gain of 8 */
    Public range As Byte           '/* INPUT: 0 = 5V range, 1 = 10V range */
    Public polarity As Byte        '/* INPUT: 0 = bipolar, 1 = unipolar */
    Public load_cal As Byte        '/* INPUT: 0 = do not load calibration values, 1 = load */
    Public scan_interval As Byte   '/* For DMM32, Hercules, DMM48 only. Use constants SCAN_INTERVAL_X */
    Public addiff As Byte       '/* For HERCULES only, 0 = SINGLE_ENDED or 1 = DIFFERENTIAL */
End Structure


'DSCADSCAN
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> Public Structure sDSCADSCAN
    Public low_channel As Byte         '/* INPUT: Low channel of the scan */
    Public high_channel As Byte        '/* INPUT: High channel of the scan */
    Public sample_values As IntPtr     'each index is size of 2 bytes. Convert to short
    Public gain As Byte           '/* Obsolete */
End Structure


'ERRPARAMS
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure ERRPARAMS
    Public ErrCode As Byte    '/* OUTPUT: The numerical value of the last error */
    <MarshalAs(UnmanagedType.LPStr)> _
    Public errstring As String '/* OUTPUT: A string description of the last error */
End Structure


'DSCAIOINT
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure sDSCAIOINT
    Public num_conversions As Integer      '/* INPUT: total # of conversions to write to sample_values, unless cycling. */
    Public conversion_rate As Single       '/* INPUT: interrupts per second  */
    Public conversion_rate_final As Single '/* OUTPUT: the actual conversion rate programmed into the clock  */
    Public cycle As Integer                '/* INPUT: when done with num_conversions, restart at the beginning of the buffer?  */
    Public internal_clock As Integer       '/* INPUT: use the internal clock (82c54) ? */
    Public low_channel As Byte           '/* INPUT: lowest channel number to perform A/D conversion on  */
    Public high_channel As Byte          '/* INPUT: highest channel number to perform A/D conversion on  */
    Public external_gate_enable As Integer '/* INPUT: A/D dmm, dmm-16 only  */
    Public internal_clock_gate As Integer  '/* INPUT: Set internal gating on or off (refer to manual) */
    Public sample_values As IntPtr         'each index is 2 bytes, conver to short
    Public fifo_enab As Integer            '/* INPUT: Fifo operation flag for int sample/scan modes. */
    Public fifo_depth As Short              '/* INPUT: Fifo depth (if fifo mode used) */
    Public dump_threshold As Integer       '/* INPUT: Threshold at which to dump the sample buffer */
    Public channel_align As Integer        '/* INPUT: if true, the interrupt will automatically realign the channels each cycle */
    Public clksource As Byte    '/* INPUT (PROM only): 0 for 100KHz, 1 for 10MHz internal clock source.*/
End Structure



'DSCAUTOCAL, DSCADCALPARAMS 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCAUTOCAL
    Public adrange As Byte
    Public boot_adrange As Byte
    Public ad_offset, ad_gain As Single
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
    Public target_values As Double()        'an array with length of 8
    Public use_eeprom As Integer
End Structure

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCADCALPARAMS
    Public adrange As Byte
    Public boot_adrange As Byte
    Public ad_offset, ad_gain As Single
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
    Public target_values As Double()          'an array with length of 8
    Public use_eeprom As Integer
End Structure


'DSCDACS 
'#define DACS_MAX_CHANNELS 16
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCDACS
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)> _
    Public channel_enable() As Boolean
    Public output_codes As IntPtr    'each index is a size of 4 bytes, convert to DWORD or Integer
End Structure


'DSCDASETTINGS
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCDASETTINGS
    Public polarity As Byte      '/* INPUT: 0 = BIPOLAR, 1 = UNIPOLAR */
    Public load_cal As Byte    '/* INPUT: 0 = FALSE, 1= TRUE. Loads calibration from eeprom */
    Public range As Single     '/* INPUT: Absolute value of maximum voltage */
End Structure


'DAPARAMS 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DAPARAMS
    Public channel As Byte
    Public output_code As Integer
End Structure


'DSCDACALPARAMS 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCDACALPARAMS
    Public fixed As Integer
    Public polarity As Integer
    Public darange As Double
    Public offset As Single
    Public gain As Single
End Structure


'DSCS
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCS
    Public op_type As Integer
    Public transfers As Integer
    Public total_transfers As Integer
    Public da_transfers As Integer
    Public da_total_transfers As Integer
    Public overflows As Integer

    'The following are for backwards compatibility, not used anymore
    Public paused As Byte
    Public output As Byte
    Public base_address As Short
    Public int_level As Byte
    Public analog As Integer

    'For DMMAT specific use: 
    Public ch0pol As Integer
    Public ch0prog As Integer
    Public ch0ext As Integer
    'cho0pol=true: bipolar, ch0prog=true: programmable, ch0ext=true: external; */
    Public ch1pol As Integer
    Public ch1prog As Integer
    Public ch1ext As Integer
    Public ref As Single
End Structure

'DSCUSERINTFUNCTION
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCUSERINTFUNCTION
    <MarshalAs(UnmanagedType.FunctionPtr)> _
    Public func As DSCUserInterruptFunction
    Public int_mode As Byte
    Public int_type As Integer
End Structure


'DSCUSERINT
'These input values are board specific, please refer to the user's manual 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCUSERINT
    Public intsource As Byte
    Public counter As Byte
    Public clksource As Byte
    Public rate As Single
    Public int_type As Integer
    <MarshalAs(UnmanagedType.FunctionPtr)> _
    Public func As DSCUserInterruptFunction
End Structure


'DSCCS 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCCS
    Public value As Integer
    Public status As Byte
End Structure


'DSCCR 
'Used only in dscCounterRead, otherwise mostly for internal driver use 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCCR
    Public control_code As Byte
    Public counter_number As Byte
    Public counter_data As Integer
    Public counter0 As DSCCS
    Public counter1 As DSCCS
    Public counter2 As DSCCS
End Structure


'DSCDIOINT 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCDIOINT
    Public num_transfers As Short
    Public port As Byte
    Public size As Byte
    Public cycle As Integer
    Public internal_clock As Integer
    Public mode As Byte
    Public source As Byte
    Public digital_values As IntPtr    'each index is 1 byte, convert to BYTE
    Public dump_threshold As Integer
End Structure


'DSCEMMDIO 
'this structure is for use with the EMMDIO function dscEMMDIO[G/S]etState 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCEMMDIO
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=6)> _
    Public DIOpins As Byte()             'Length of 6
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=6)> _
    Public lock_port As Integer()        'length of 6
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
    Public edge_polarity As Byte()      ' array length of 4
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
    Public edge_detect As Byte()        ' array length of 4
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
    Public edge_detect_int As Byte()    ' array length of 4
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)> _
    Public edge_detect_clear As Byte()  ' array length of 3
    Public use_DIOpins As Integer
    Public use_lock_port As Integer
    Public use_edge_polarity As Integer
    Public use_edge_detect As Integer
    Public interrupt_status As Byte
End Structure


'DSCOPTOSTATE 
'this structure is used for the dscOptoGetState/SetState
' max opto is currently 8 here.

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCOPTOSTATE
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
    Public edge_polarity() As Byte          'array length is 8 throughout
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
    Public edge_detect_enab() As Byte
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
    Public edge_status() As Byte
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
    Public oint_state() As Byte
    Public dmm48at_oint_state As Byte
End Structure



'DSCEMMDIORESETINT 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCEMMDIORESETINT
    Public use_lock_port As Integer
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=6)> _
    Public lock_port() As Integer           'array length is 6
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)> _
    Public edge_detect_clear() As Byte      'array length is 3
End Structure


'DSCWATCHDOG
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCWATCHDOG
    Public wd1 As Short
    Public wd2 As Byte
    Public options As Byte
End Structure


'DSCPWM	
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCPWM
    Public output_freq As Double
    Public duty_cycle As Single
    Public polarity As Byte
    Public pwm_circuit As Byte
    Public output_enab As Integer
End Structure


'DSCQMM_MMR
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCQMM_MMR
    Public counter_group As Byte
    Public fout_divider As Byte
    Public fout_source As Byte
    Public compare1_enable As Byte
    Public compare2_enable As Byte
    Public tod_mode As Byte
End Structure


'DSCQMM_CMR
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCQMM_CMR
    Public counter As Byte
    Public gating_control As Byte
    Public active_source_edge As Byte
    Public count_source As Byte
    Public special_gate As Byte
    Public reload_source As Byte
    Public cycle As Byte
    Public count_type As Byte
    Public count_direction As Byte
    Public output_control As Byte
End Structure


'DSCQMM_MCC multiple counter control 
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCQMM_MCC
    Public group1_action As Byte
    Public group1_counter_select As Byte
    Public group2_action As Byte
    Public group2_counter_select As Byte
End Structure


'DSCQMM_SCF special counter functions
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCQMM_SCF
    Public counter As Byte
    Public action As Byte
    Public alarm_value As Short
End Structure


'DSCQMM_PWM pulse width modulation control */
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
Public Structure DSCQMM_PWM
    Public init As Byte
    Public counter As Byte
    Public output_freq As Single
    Public duty_cycle As Single

    Public input_freq As Integer
    Public load_reg As Short
    Public hold_reg As Short
    Public hit_extreme As Byte
End Structure


'DSCUDVB class
Public Class DSCUDVB


    'Error handling Functions
    Public Declare Function dscGetLastError Lib "dscud.dll" (ByRef errparams As ERRPARAMS) As Byte
    Public Declare Function dscGetErrorString Lib "dscud.dll" (ByVal error_code As Byte) As String

    'Initialization Functions
    Public Declare Function dscInit Lib "dscud.dll" (ByVal version As Integer) As Byte
    Public Declare Function dscFree Lib "dscud.dll" () As Byte
    Public Declare Ansi Function dscInitBoard Lib "dscud.dll" (ByVal boardtype As Byte, ByRef dsccb As sDSCCB, ByRef dscb As Short) As Byte
    Public Declare Function dscFreeBoard Lib "dscud.dll" (ByVal board As Short) As Byte

    'A/D functions
    Public Declare Function dscADSetSettings Lib "dscud.dll" (ByVal board As Short, ByRef settings As sDSCADSETTINGS) As Byte
    Public Declare Function dscADSetChannel Lib "dscud.dll" (ByVal board As Short, ByVal low_channel As Byte, ByVal high_channel As Byte) As Byte
    Public Declare Function dscADSample Lib "dscud.dll" (ByVal board As Short, ByRef sample As Short) As Byte
    Public Declare Function dscADSampleInt Lib "dscud.dll" (ByVal board As Short, ByRef dscaioint As sDSCAIOINT) As Byte
    Public Declare Function dscADSampleAvg Lib "dscud.dll" (ByVal board As Short, ByRef average As Double, ByVal count As Integer) As Byte
    Public Declare Function dscADScan Lib "dscud.dll" (ByVal board As Short, ByRef dscadscanVar As sDSCADSCAN, ByVal sample_values As IntPtr) As Byte
    Public Declare Function dscADScanInt Lib "dscud.dll" (ByVal board As Short, ByRef dscaioint As sDSCAIOINT) As Byte
    Public Declare Function dscADScanAvg Lib "dscud.dll" (ByVal board As Short, ByRef dscadscan As sDSCADSCAN, ByVal average_values As IntPtr, ByVal count As Integer) As Byte

    'D/A functions
    Public Declare Function dscDASetPolarity Lib "dscud.dll" (ByVal board As Short, ByVal polarity As Byte) As Byte
    Public Declare Function dscDASetSettings Lib "dscud.dll" (ByVal board As Byte, ByRef dasettings As DSCDASETTINGS) As Byte
    Public Declare Function dscDAConvert Lib "dscud.dll" (ByVal board As Short, ByVal channel As Byte, ByVal output_code As Integer) As Byte
    Public Declare Function dscDAConvertScan Lib "dscud.dll" (ByVal board As Short, ByRef dscdacs As DSCDACS) As Byte
    Public Declare Function dscDAConvertScanInt Lib "dscud.dll" (ByVal board As Short, ByRef dscaioint As sDSCAIOINT) As Byte

    'DIO Functions 
    Public Declare Function dscDIOSetConfig Lib "dscud.dll" (ByVal board As Short, ByVal config_bytes As IntPtr) As Byte
    Public Declare Function dscDIOInputByte Lib "dscud.dll" (ByVal board As Short, ByVal port As Byte, ByRef digital_value As Byte) As Byte
    Public Declare Function dscDIOOutputByte Lib "dscud.dll" (ByVal board As Short, ByVal port As Byte, ByVal digital_value As Byte) As Byte
    Public Declare Function dscDIOInputBit Lib "dscud.dll" (ByVal board As Short, ByVal port As Byte, ByVal bit As Byte, ByRef digital_value As Byte) As Byte
    Public Declare Function dscDIOOutputBit Lib "dscud.dll" (ByVal board As Short, ByVal port As Byte, ByVal bit As Byte, ByVal digital_value As Byte) As Byte
    Public Declare Function dscDIOSetBit Lib "dscud.dll" (ByVal board As Short, ByVal port As Byte, ByVal bit As Byte) As Byte
    Public Declare Function dscDIOClearBit Lib "dscud.dll" (ByVal board As Short, ByVal port As Byte, ByVal bit As Byte) As Byte
    Public Declare Function dscDIOInputInt Lib "dscud.dll" (ByVal board As Short, ByRef dscdioint As DSCDIOINT) As Byte
    Public Declare Function dscDIOOutputInt Lib "dscud.dll" (ByVal board As Short, ByRef dscdioint As DSCDIOINT) As Byte
    Public Declare Function dscDMM32DIOOutputByte Lib "dscud.dll" (ByVal board As Short, ByVal port As Byte, ByVal digital_value As Byte) As Byte
    Public Declare Function dscDMM32DIOInputByte Lib "dscud.dll" (ByVal board As Short, ByVal port As Byte, ByRef digital_value As Byte) As Byte

    'Opto Functions	
    Public Declare Function dscOptoInputByte Lib "dscud.dll" (ByVal board As Short, ByVal port As Byte, ByRef optoValue As Byte) As Byte
    Public Declare Function dscOptoInputBit Lib "dscud.dll" (ByVal board As Short, ByVal port As Byte, ByVal bit As Byte, ByRef optoValue As Byte) As Byte
    Public Declare Function dscOptoGetPolarity Lib "dscud.dll" (ByVal board As Short, ByRef polarity As Byte) As Byte

    'opto Edge State
    Public Declare Function dscOptoGetState Lib "dscud.dll" (ByVal board As Short, ByRef state As DSCOPTOSTATE) As Byte
    Public Declare Function dscOptoSetState Lib "dscud.dll" (ByVal board As Short, ByRef state As DSCOPTOSTATE) As Byte

    'Counter Functions 
    Public Declare Function dscCounterSetRateSingle Lib "dscud.dll" (ByVal board As Short, ByVal hertz As Single, ByVal ctr As Integer) As Byte
    Public Declare Function dscCounterSetRate Lib "dscud.dll" (ByVal board As Short, ByVal hertz As Single) As Byte
    Public Declare Function dscCounterDirectSet Lib "dscud.dll" (ByVal board As Short, ByVal code As Byte, ByVal data As Integer, ByVal ctr_number As Byte) As Byte
    Public Declare Function dscCounterRead Lib "dscud.dll" (ByVal board As Short, ByRef dsccr As DSCCR) As Byte

    'Calibration Functions 
    Public Declare Function dscSetCalMux Lib "dscud.dll" (ByVal board As Short, ByVal TrueForOn As Integer) As Byte
    Public Declare Function dscADAutoCal Lib "dscud.dll" (ByVal board As Short, ByRef params As DSCADCALPARAMS) As Byte
    Public Declare Function dscDAAutoCal Lib "dscud.dll" (ByVal board As Short, ByRef params As DSCDACALPARAMS) As Byte
    Public Declare Function dscADCalVerify Lib "dscud.dll" (ByVal board As Short, ByRef params As DSCADCALPARAMS) As Byte
    Public Declare Function dscDACalVerify Lib "dscud.dll" (ByVal board As Short, ByRef params As DSCDACALPARAMS) As Byte
    Public Declare Function dscGetReferenceVoltages Lib "dscud.dll" (ByVal board As Short, ByVal refs As IntPtr) As Byte
    Public Declare Function dscSetReferenceVoltages Lib "dscud.dll" (ByVal board As Short, ByVal refs As IntPtr) As Byte
    Public Declare Function dscDAGetOffsets Lib "dscud.dll" (ByVal board As Short, ByVal offsets As IntPtr, ByVal count As Integer) As Byte
    Public Declare Function dscDASetOffsets Lib "dscud.dll" (ByVal board As Short, ByVal offsets As IntPtr, ByVal count As Integer) As Byte

    'Relay Functions	
    Public Declare Function dscSetRelay Lib "dscud.dll" (ByVal board As Short, ByVal relay As Byte, ByVal value As Byte) As Byte 'relays start from 0. For ir104, please use dscIR104 functions
    Public Declare Function dscGetRelay Lib "dscud.dll" (ByVal board As Short, ByVal relay As Byte, ByRef value As Byte) As Byte 'relays start from 0. For ir104, please use dscIR104 functions
    Public Declare Function dscSetRelayMulti Lib "dscud.dll" (ByVal board As Short, ByVal relayGroup As Byte, ByVal value As Byte) As Byte 'each relayGroup groups 8 relays together. 
    'ex. relayGroup 0 refers to relays 0-7
    'relayGroup 1 referes to relays 8-15, etc.
    'for boards with relays that aren't multiples 
    'of 8, such as IR104, relays 16-20 are part of
    'relayGroup 2. The same applies to other boards.
    'Parameter value is the value to set for each relay. 
    'Bit0 of value is value set for relay 0, 
    'bit1 of value is value set for relay 1, etc 
    Public Declare Function dscGetRelayMulti Lib "dscud.dll" (ByVal board As Short, ByVal relayGroup As Byte, ByRef value As Byte) As Byte 'each relayGroup groups 8 relays together. 
    'ex. relayGroup 0 refers to relays 0-7
    'relayGroup 1 referes to relays 8-15, etc.
    'for boards with relays that aren't multiples 
    'of 8, such as IR104, relays 16-20 are part of
    'relayGroup 2. The same applies to other boards.
    'Parameter value is the value to set for each relay. 
    'Bit0 of value is value set for relay 0, 
    'bit1 of value is value set for relay 1, etc */

    'WATCHDOG Functions 
    Public Declare Function dscWatchdogEnable Lib "dscud.dll" (ByVal board As Short, ByVal timer16 As Short, ByVal timer8 As Byte, ByVal options As Integer) As Byte
    Public Declare Function dscWatchdogDisable Lib "dscud.dll" (ByVal board As Short) As Byte
    Public Declare Function dscWatchdogTrigger Lib "dscud.dll" (ByVal board As Short) As Byte

    'PULSE WIDTH MOD Functions
    Public Declare Function dscPWMLoad Lib "dscud.dll" (ByVal board As Short, ByVal pwm_circuit As Byte, ByVal counter As Byte, ByVal value As Integer) As Byte
    Public Declare Function dscPWMConfig Lib "dscud.dll" (ByVal board As Short, ByVal config_byte As IntPtr) As Byte
    Public Declare Function dscPWMClear Lib "dscud.dll" (ByVal board As Short, ByVal pwm_circuit As Byte) As Byte
    Public Declare Function dscPWMFunction Lib "dscud.dll" (ByVal board As Short, ByRef dscpwm As DSCPWM) As Byte

    'Register Functions 
    Public Declare Function dscRegisterRead Lib "dscud.dll" (ByVal board As Short, ByVal address As Short, ByRef data As Byte) As Byte
    Public Declare Function dscRegisterWrite Lib "dscud.dll" (ByVal board As Short, ByVal address As Short, ByVal data As Byte) As Byte
    Public Declare Function dscInp Lib "dscud.dll" (ByVal address As Integer, ByRef value As Byte) As Byte
    Public Declare Function dscInpw Lib "dscud.dll" (ByVal address As Integer, ByRef value As Short) As Byte
    Public Declare Function dscInpl Lib "dscud.dll" (ByVal address As Integer, ByRef value As Integer) As Byte
    Public Declare Function dscInpws Lib "dscud.dll" (ByVal address As Integer, ByRef buffer As Short, ByVal n As Short) As Byte
    Public Declare Function dscOutp Lib "dscud.dll" (ByVal address As Integer, ByVal value As Byte) As Byte
    Public Declare Function dscOutpw Lib "dscud.dll" (ByVal address As Integer, ByVal value As Short) As Byte
    Public Declare Function dscOutpl Lib "dscud.dll" (ByVal address As Integer, ByVal value As Integer) As Byte
    Public Declare Function dscOutpws Lib "dscud.dll" (ByVal address As Integer, ByRef buffer As Short, ByVal n As Short) As Byte

    'Misc. Functions 
    Public Declare Function dscSleep Lib "dscud.dll" (ByVal ms As Integer) As Byte
    Public Declare Function dscGetTime Lib "dscud.dll" (ByRef ms As Integer) As Byte
    Public Declare Function dscGetEEPROM Lib "dscud.dll" (ByVal board As Short, ByVal address As Integer, ByRef data As Byte) As Byte
    Public Declare Function dscSetEEPROM Lib "dscud.dll" (ByVal board As Short, ByVal address As Integer, ByVal data As Byte) As Byte
    Public Declare Function dscSetTrimDac Lib "dscud.dll" (ByVal board As Short, ByVal trimDac As Integer, ByVal value As Byte) As Byte
    Public Declare Function dscGetStatus Lib "dscud.dll" (ByVal board As Short, ByRef status As DSCS) As Byte
    Public Declare Function dscCancelOp Lib "dscud.dll" (ByVal board As Short) As Byte
    Public Declare Function dscCancelOpType Lib "dscud.dll" (ByVal board As Short, ByVal int_type As Integer) As Byte
    Public Declare Function dscPauseOp Lib "dscud.dll" (ByVal board As Short) As Byte
    Public Declare Function dscResumeOp Lib "dscud.dll" (ByVal board As Short) As Byte
    Public Declare Function dscSetUserInterruptFunction Lib "dscud.dll" (ByVal board As Short, ByRef dscuserintfunc As DSCUSERINTFUNCTION) As Byte
    Public Declare Function dscSetUserInterruptFunctionType Lib "dscud.dll" (ByVal board As Short, ByRef dscuserintfunc As DSCUSERINTFUNCTION, ByVal int_type As Integer) As Byte
    Public Declare Function dscUserIntVB Lib "dscud.dll" (ByVal board As Short, ByRef dscuserint As dscUserInt) As Byte
    Public Declare Function dscClearUserInterruptFunction Lib "dscud.dll" (ByVal board As Short) As Byte
    Public Declare Function dscClearUserInterruptFunctionType Lib "dscud.dll" (ByVal board As Short, ByVal int_type As Integer) As Byte
    Public Declare Function dscGetBoardMacro Lib "dscud.dll" (ByVal boardtype As String, ByRef macro As Byte) As Byte
    Public Declare Function dscGetFPGARev Lib "dscud.dll" (ByVal board As Short, ByRef fpga As Short) As Byte
    Public Declare Function dscSetSystemPriority Lib "dscud.dll" (ByVal priority As Integer) As Byte


    'Unit Conversion Functions 
    Public Declare Function dscADCodeToVoltage Lib "dscud.dll" (ByVal board As Short, ByVal dasettings As sDSCADSETTINGS, ByVal adcode As Short, ByRef voltage As Double) As Byte
    Public Declare Function dscVoltageToADCode Lib "dscud.dll" (ByVal board As Short, ByVal dasettings As sDSCADSETTINGS, ByVal voltage As Double, ByRef adcode As Short) As Byte
    Public Declare Function dscDACodeToVoltage Lib "dscud.dll" (ByVal board As Short, ByVal dasettings As DSCDASETTINGS, ByVal dacode As Integer, ByRef voltage As Double) As Byte
    Public Declare Function dscVoltageToDACode Lib "dscud.dll" (ByVal board As Short, ByVal dasettings As DSCDASETTINGS, ByVal voltage As Double, ByRef dacode As Integer) As Byte

    'IR104
    Public Declare Function dscIR104ClearRelay Lib "dscud.dll" (ByVal board As Short, ByVal relay As Byte) As Byte '/*relay range 1-20*/
    Public Declare Function dscIR104SetRelay Lib "dscud.dll" (ByVal board As Short, ByVal relay As Byte) As Byte '/*relay range 1-20*/
    Public Declare Function dscIR104RelayInput Lib "dscud.dll" (ByVal board As Short, ByVal relay As Byte, ByRef value As Byte) As Byte '/*relay range 1-20*/
    Public Declare Function dscIR104OptoInput Lib "dscud.dll" (ByVal board As Short, ByVal opto As Byte, ByRef value As Byte) As Byte '/*opto range 1-20*/

    'EMMDIO Functions 
    Public Declare Function dscEMMDIOGetState Lib "dscud.dll" (ByVal board As Short, ByRef state As DSCEMMDIO) As Byte
    Public Declare Function dscEMMDIOSetState Lib "dscud.dll" (ByVal board As Short, ByRef state As DSCEMMDIO) As Byte
    Public Declare Function dscEMMDIOResetInt Lib "dscud.dll" (ByVal board As Short, ByRef edge As dscEMMDIOResetInt) As Byte

    'QMM Function Prototypes 
    Public Declare Function dscQMMReset Lib "dscud.dll" (ByVal board As Short) As Byte

    Public Declare Function dscQMMSetMMR Lib "dscud.dll" (ByVal board As Short, ByRef dscqmmmmr As DSCQMM_MMR) As Byte
    Public Declare Function dscQMMSetCMR Lib "dscud.dll" (ByVal board As Short, ByRef dscqmmcmr As DSCQMM_CMR) As Byte
    Public Declare Function dscQMMCounterControl Lib "dscud.dll" (ByVal board As Short, ByRef dscqmmmcc As DSCQMM_MCC, ByRef status As Byte) As Byte
    Public Declare Function dscQMMSingleCounterControl Lib "dscud.dll" (ByVal board As Short, ByVal counter As Byte, ByVal action As Byte) As Byte
    Public Declare Function dscQMMSetLoadRegister Lib "dscud.dll" (ByVal board As Short, ByVal counter As Byte, ByVal value As UInt16) As Byte
    Public Declare Function dscQMMSetHoldRegister Lib "dscud.dll" (ByVal board As Short, ByVal counter As Byte, ByVal value As UInt16) As Byte
    Public Declare Function dscQMMReadHoldRegister Lib "dscud.dll" (ByVal board As Short, ByVal counter As Byte, ByRef value As UInt16) As Byte


    Public Declare Function dscQMMSpecialCounterFunction Lib "dscud.dll" (ByVal board As Short, ByRef dscqmmscf As DSCQMM_SCF) As Byte
    Public Declare Function dscQMMMeasureFrequency Lib "dscud.dll" (ByVal board As Short, ByVal interval As Byte, ByVal source As Byte, ByRef freq As Single) As Byte
    Public Declare Function dscQMMMeasurePeriod Lib "dscud.dll" (ByVal board As Short, ByVal frequency As Byte, ByRef period As Single) As Byte

    Public Declare Function dscQMMPulseWidthModulation Lib "dscud.dll" (ByVal board As Short, ByRef dscqmmpwm As DSCQMM_PWM) As Byte

End Class

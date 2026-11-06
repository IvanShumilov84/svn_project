'****************************************************************************************************************
'*	Name :			Hercules.vb														
'*	Description :	DiamondIODevice.vb
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

Imports Microsoft.VisualBasic
Imports System
Imports System.Math
Imports System.Threading 
Imports System.Windows.Forms

Namespace CEL.DiamondIODevice

	'****************************************************************************
	'* Public Class Hercules ****************************************************
	'****************************************************************************
	
	Public NotInheritable Class Hercules
		Inherits DiamondIODevice_ADDIO
	
		'**********************
		'* Public Members 	  *
		'**********************
		Public Sub New(baseAddress As Short, intLevel As Byte, DIODirections() as integer)
			Dim ConfigurationByte 	As Byte = 0
			Dim i	 				As Integer
			Dim my_dsccb 			As New DSCCB()
												
			my_dsccb.boardType 		= DSC_BoardType.DSC_HERCEBX
			my_dsccb.int_level 		= intLevel
			my_dsccb.base_address 	= baseAddress

			'Initialise Diamond Library
			MyBase.Initialise()
			
			'Initialise Board
			MyBase.Board_Initialisation(my_dsccb)

			'Hercules AD AutoCAL
			MyBase.AD_AutoCAL()

			'Hercules DI/O Configuration			
			'0=Input
			'1=Output
			For i=0 To 4
				ConfigurationByte += (DIODirections.GetValue(i) * Pow(2, Convert.ToDouble(i)))				
				MyBase.DIODirections(i) = DIODirections(i)				
			Next			
			MyBase.NbrDIO = 40
			MyBase.DIO_Configuration(ConfigurationByte)
			
			'Hercules DA Settings
			MyBase.DA_Settings()
		
			'Hercules AD Settings			
			MyBase.AD_Settings()					
		End Sub

	
		Public Sub Start_Scan()
			Dim AutoEvent 				As New AutoResetEvent(False)
			Dim TimerDelegate 			As TimerCallback 	= AddressOf OnTimer
			
			aTimer			= New System.Threading.Timer(TimerDelegate,AutoEvent,0,1)
		End Sub

		
		Public Sub Stop_Scan()
			aTimer.Dispose()
		End Sub
		
		
		'**********************
		'* Private Members    *
		'**********************				
		Private Sub OnTimer(stateInfo As Object)			
			Dim Message As String
			
			Try
				MyBase.AD_Read()
				MyBase.DIO_Scan()
			Catch e1 As CEL.DiamondIODevice.DiamondException
				Message = e1.FunctionName + " (" + e1.ErrorCode.ToString() + ")" + VbCrLf + e1.ErrorDescription
				MessageBox.Show(Message, "ERROR")
			End Try
		End Sub
		
		Private aTimer 		 	As System.Threading.Timer
	End Class
End Namespace

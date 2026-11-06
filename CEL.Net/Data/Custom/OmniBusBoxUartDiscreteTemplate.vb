'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 3/13/2013 9:56:07 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class OmniBusBoxUartDiscreteTemplate 
		Inherits esOmniBusBoxUartDiscreteTemplate

		Public Sub New()

        End Sub

        Public Enum eDiscreteCode
            Fault
            Status
            BNR 'Packed BNR
            BCD 'Packed BCD
        End Enum

        Public Property DiscreteCode As eDiscreteCode
            Get
                If MyBase.Code Is Nothing Then
                    Return String.Empty
                Else
                    Select Case MyBase.Code.ToUpper
                        Case "F"
                            Return eDiscreteCode.Fault
                        Case "S"
                            Return eDiscreteCode.Status
                        Case "BNR"
                            Return eDiscreteCode.BNR
                        Case "BCD"
                            Return eDiscreteCode.BCD
                        Case Else
                            Throw New Exception()
                    End Select
                End If
            End Get
            Set(value As eDiscreteCode)
                Select Case value
                    Case eDiscreteCode.Fault
                        MyBase.Code = "F"
                    Case eDiscreteCode.Status
                        MyBase.Code = "S"
                    Case eDiscreteCode.BCD
                        MyBase.Code = "BCD"
                    Case eDiscreteCode.BNR
                        MyBase.Code = "BNR"
                    Case Else
                        Throw New Exception()
                End Select
                MyBase.Code = value
            End Set
        End Property

        'Public Property UartChannel As String


	End Class

End Namespace

'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 3/12/2013 11:03:46 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class ARINC429DiscreteTemplate 
		Inherits esARINC429DiscreteTemplate

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
        <Obsolete("Obselete property, You MUST use DiscreteCode", True)> _
        Public Overrides Property Code As String
            Get
                Return MyBase.Code
            End Get
            Set(value As String)
                MyBase.Code = value
            End Set
        End Property
        Public Shadows Property StartBit As Byte
            Get
                Return MyBase.StartBit
            End Get
            Set(value As Byte)
                MyBase.StartBit = value
            End Set
        End Property
        Public Shadows Property StopBit As Byte
            Get
                Return MyBase.StopBit
            End Get
            Set(value As Byte)
                MyBase.StopBit = value
            End Set
        End Property
        Public Shadows Property LSBValue As Double
            Get
                Return MyBase.LSBValue
            End Get
            Set(value As Double)
                MyBase.LSBValue = value
            End Set
        End Property
    End Class

End Namespace

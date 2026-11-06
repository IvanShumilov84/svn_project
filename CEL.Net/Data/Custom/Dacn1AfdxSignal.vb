'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 08/01/2014 10:59:20 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery
Imports CEL.Data.BusinessObjects.Dacn1Afdx

Namespace BusinessObjects

	Partial Public Class Dacn1AfdxSignal 
		Inherits esDacn1AfdxSignal

		Public Sub New()

        End Sub
        Public Overloads Property TxRxEnum As eTransmissionMode

            Get
                Return MyBase.TxRx
            End Get
            Set(value As eTransmissionMode)
                MyBase.TxRx = value
            End Set

        End Property
        <Obsolete("Obselete property, You MUST use TxRxEnum", True)> _
        Public Overrides Property TxRx As Byte?
            Get
                Return MyBase.TxRx
            End Get
            Set(value As Byte?)
                MyBase.TxRx = value
            End Set
        End Property


        Public Property DataTypeEnum As eDataType
            Get
                Return MyBase.DataType
            End Get
            Set(value As eDataType)
                MyBase.DataType = value
            End Set
        End Property
        <Obsolete("Obselete property, You MUST use DataTypeEnum", True)> _
        Public Overrides Property DataType As Integer?
            Get
                Return MyBase.DataType
            End Get
            Set(value As Integer?)
                MyBase.DataType = value
            End Set
        End Property


        Public Property CodingTypeEnum As eCodingType
            Get
                If IsNothing(MyBase.CodingType) Then
                    Return eCodingType.NotSpecified
                End If
                Return MyBase.CodingType
            End Get
            Set(value As eCodingType)
                MyBase.CodingType = value
            End Set
        End Property
        <Obsolete("Obselete property, You MUST use CodingTypeEnum", True)> _
        Public Overrides Property CodingType As Integer?
            Get
                Return MyBase.CodingType
            End Get
            Set(value As Integer?)
                MyBase.CodingType = value
            End Set
        End Property
        'Public Shadows Property StartBit As Byte
        '    Get
        '        Return MyBase.StartBit
        '    End Get
        '    Set(value As Byte)
        '        MyBase.StartBit = value
        '    End Set
        'End Property
        'Public Shadows Property StopBit As Byte
        '    Get
        '        Return MyBase.StopBit
        '    End Get
        '    Set(value As Byte)
        '        MyBase.StopBit = value
        '    End Set
        'End Property
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

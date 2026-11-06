'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 08/11/2013 2:28:09 PM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Imports CEL.Data.BusinessObjects.Dacn1Afdx

Namespace BusinessObjects

	Partial Public Class Dacn1AfdxMessage 
		Inherits esDacn1AfdxMessage

		Public Sub New()

        End Sub

      
        Public Overloads Property TxRxEnum As eTransmissionMode

            Get
                Return MyBase.TxRx
                'Select Case MyBase.TxRx
                '    Case 0
                '        Return eArincTxRx.Rx
                '    Case 1
                '        Return eArincTxRx.Tx
                '    Case Else
                '        Throw New Exception
                'End Select
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




      
        'Public Overloads Property CodingTypeEnum As eCodingType
        '    Get
        '        Return MyBase.CodingType
        '    End Get
        '    Set(value As eCodingType)
        '        MyBase.CodingType = value
        '    End Set
        'End Property
        '<Obsolete("Obselete property, You MUST use CodingTypeEnum", True)> _
        'Public Overloads Property CodingType
        '    Get
        '        Return MyBase.CodingType
        '    End Get
        '    Set(value)
        '        MyBase.CodingType = value
        '    End Set
        'End Property


      
        'Public Overloads Property DataTypeEnum As eDataType
        '    Get
        '        Return MyBase.DataType
        '    End Get
        '    Set(value As eDataType)
        '        MyBase.DataType = value
        '    End Set
        'End Property
        '<Obsolete("Obselete property, You MUST use DataTypeEnum", True)> _
        'Public Overrides Property DataType As Integer?
        '    Get
        '        Return MyBase.DataType
        '    End Get
        '    Set(value As Integer?)
        '        MyBase.DataType = value
        '    End Set
        'End Property

    End Class

End Namespace

'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 06/11/2013 8:07:50 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery
Imports CEL.Data.BusinessObjects.Dacn1Afdx

Namespace BusinessObjects

    Partial Public Class Dacn1AfdxPort
        Inherits esDacn1AfdxPort

        Public Sub New()

        End Sub



        '<Obsolete("Obselete property, You MUST use TransmissionChannelAB", True)> _
        'Public Overrides Property TransmissionChannel As Byte?
        '    Get
        '        Return MyBase.TransmissionChannel
        '    End Get
        '    Set(value As Byte?)
        '        MyBase.TransmissionChannel = value
        '    End Set
        'End Property
       
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


        Public Overloads Property PortTypeEnum As ePortType

            Get
                Return MyBase.PortType
                'Select Case MyBase.TxRx
                '    Case 0
                '        Return eArincTxRx.Rx
                '    Case 1
                '        Return eArincTxRx.Tx
                '    Case Else
                '        Throw New Exception
                'End Select
            End Get
            Set(value As ePortType)
                MyBase.PortType = value
            End Set
        End Property
        <Obsolete("Obselete property, You MUST use PortTypeEnum", True)> _
        Public Overrides Property PortType As Integer?
            Get
                Return MyBase.PortType
            End Get
            Set(value As Integer?)
                MyBase.PortType = value
            End Set
        End Property

    End Class

End Namespace

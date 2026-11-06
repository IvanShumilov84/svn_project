'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 06/11/2013 1:44:52 PM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery
Imports CEL.Data.BusinessObjects.Dacn1Afdx

Namespace BusinessObjects

    Partial Public Class Dacn1AfdxVirtualLink
        Inherits esDacn1AfdxVirtualLink

        Public Sub New()

        End Sub

        Public Enum eArincChannel
            ChA = 0
            ChB = 1
            ChAB = 2
        End Enum
        Public Overloads Property ArincChannelEnum As eArincChannel 'Comme ArincChannel fait partie de la cle primaire de la table je ne peux pas la mettre obselete
            'Public Overloads Property ArincChannelEnum As eArincChannel
            Get
                Return MyBase.ArincChannel
                'Select Case MyBase.TransmissionChannel
                '    Case 0
                '        Return eArincChannel.CHA
                '    Case 1
                '        Return eArincChannel.CHB
                '    Case 2
                '        Return eArincChannel.CHAB
                '    Case Else
                '        Throw New Exception
                'End Select
            End Get
            Set(value As eArincChannel)
                MyBase.ArincChannel = value
            End Set
        End Property

        Public Enum eBroadcastType
            Unicast = 0
            Multicast = 1
        End Enum
        Public Overloads Property BroadcastTypeEnum As eBroadcastType

            Get
                Return MyBase.BroadcastType
                'Select Case MyBase.BroadcastType
                '    Case 0
                '        Return eBroadCast.Unicast
                '    Case 1
                '        Return eBroadCast.Multicast
                '    Case Else
                '        Throw New Exception
                'End Select
            End Get
            Set(value As eBroadcastType)
                MyBase.BroadcastType = value
            End Set

        End Property
        <Obsolete("Obselete property, You MUST use BroadcastTypeEnum", True)> _
        Public Overrides Property BroadcastType As Byte?
            Get
                Return MyBase.BroadcastType
            End Get
            Set(value As Byte?)
                MyBase.BroadcastType = value
            End Set
        End Property

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

        Public Overrides Property IntegrityCheck As Byte?
            Get
                If IsNothing(MyBase.IntegrityCheck) Then Return 0

                Return MyBase.IntegrityCheck
            End Get
            Set(value As Byte?)
                MyBase.IntegrityCheck = value
            End Set
        End Property

        Public Overrides Property RedondancyEnabled As Byte?
            Get
                If IsNothing(MyBase.RedondancyEnabled) Then Return 0
                Return MyBase.RedondancyEnabled
            End Get
            Set(value As Byte?)
                MyBase.RedondancyEnabled = value
            End Set
        End Property


    End Class

End Namespace

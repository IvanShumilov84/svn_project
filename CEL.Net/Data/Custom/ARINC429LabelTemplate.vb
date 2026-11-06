'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 3/12/2013 11:03:22 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core

Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class ARINC429LabelTemplate 
		Inherits esARINC429LabelTemplate

		Public Sub New()

        End Sub



        Public Overrides Property LoggerEnabled As Boolean?
            Get
                If MyBase.LoggerEnabled Is Nothing Then
                    Return False
                Else
                    Return MyBase.LoggerEnabled
                End If

            End Get
            Set(value As Boolean?)
                MyBase.LoggerEnabled = value
            End Set
        End Property


        Public Enum eArincChannel
            CHA = 0
            CHB = 1
        End Enum
        Public Property ArincChannelAB As eArincChannel
            Get
                Select Case MyBase.ArincChannel
                    Case 0
                        Return eArincChannel.CHA
                    Case 1
                        Return eArincChannel.CHB
                    Case Else
                        Throw New Exception
                End Select
            End Get
            Set(value As eArincChannel)
                MyBase.ArincChannel = value
            End Set
        End Property

        <Obsolete("Obselete property, You MUST use Channel", True)> _
        Public Overrides Property ArincChannel As Byte?
            Get
                Return MyBase.ArincChannel
            End Get
            Set(value As Byte?)
                MyBase.ArincChannel = value
            End Set
        End Property




        Public Enum eArincTxRx
            Rx = 0
            Tx = 1
        End Enum
        Public Overloads Property ArincRxTx As eArincTxRx

            Get
                Select Case MyBase.TxRx
                    Case 0
                        Return eArincTxRx.Rx
                    Case 1
                        Return eArincTxRx.Tx
                    Case Else
                        Throw New Exception
                End Select
            End Get
            Set(value As eArincTxRx)
                MyBase.TxRx = value
            End Set

        End Property
        <Obsolete("Obselete property, You MUST use DiscreteCode", True)> _
        Public Overrides Property TxRx As Byte?
            Get
                Return MyBase.TxRx
            End Get
            Set(value As Byte?)
                MyBase.TxRx = value
            End Set
        End Property



    End Class


End Namespace

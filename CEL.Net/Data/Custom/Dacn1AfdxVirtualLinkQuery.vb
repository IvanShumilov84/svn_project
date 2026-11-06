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
Imports CEL.Data.BusinessObjects.Dacn1AfdxVirtualLink

Namespace BusinessObjects

    Partial Public Class Dacn1AfdxVirtualLinkQuery
        Inherits esDacn1AfdxVirtualLinkQuery

        Public Sub New()

        End Sub


        Public ReadOnly Property ArincChannelEnum As esQueryItem
            Get
                Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.ArincChannel, esSystemType.Byte)
            End Get
        End Property
        Public ReadOnly Property BroadcastTypeEnum As esQueryItem
            Get
                Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.BroadcastType, esSystemType.Byte)
            End Get
        End Property
        Public ReadOnly Property TxRxEnum As esQueryItem
            Get
                Return New esQueryItem(Me, Dacn1AfdxVirtualLinkMetadata.ColumnNames.TxRx, esSystemType.Byte)
            End Get
        End Property
    End Class

End Namespace


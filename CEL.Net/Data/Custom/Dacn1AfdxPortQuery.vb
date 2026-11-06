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

Namespace BusinessObjects

	Partial Public Class Dacn1AfdxPortQuery
		Inherits esDacn1AfdxPortQuery

		Public Sub New()

        End Sub
        Public ReadOnly Property TxRxEnum As esQueryItem
            Get
                Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.TxRx, esSystemType.Byte)
            End Get
        End Property
        Public ReadOnly Property PortTypeEnum As esQueryItem
            Get
                Return New esQueryItem(Me, Dacn1AfdxPortMetadata.ColumnNames.PortType, esSystemType.Byte)
            End Get
        End Property
	End Class

End Namespace


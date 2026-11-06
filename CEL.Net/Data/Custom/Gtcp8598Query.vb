'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 4/10/2015 10:39:47 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class Gtcp8598Query
		Inherits esGtcp8598Query

		Public Sub New()

        End Sub

        Public ReadOnly Property RepairTypeEnum As esQueryItem
            Get
                Return New esQueryItem(Me, Gtcp8598Metadata.ColumnNames.RepairTypeID, esSystemType.Int32)
            End Get
        End Property

    End Class

End Namespace


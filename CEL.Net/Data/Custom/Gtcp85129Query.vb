'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 5/11/2015 8:23:54 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class Gtcp85129Query
		Inherits esGtcp85129Query

		Public Sub New()

		End Sub

        Public ReadOnly Property RepairTypeEnum As esQueryItem
            Get
                Return New esQueryItem(Me, Gtcp85129Metadata.ColumnNames.RepairTypeID, esSystemType.Int32)
            End Get
        End Property

	End Class

End Namespace


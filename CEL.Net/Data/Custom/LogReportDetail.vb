'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 5/28/2013 7:54:02 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class LogReportDetail 
		Inherits esLogReportDetail

		Public Sub New()

		End Sub


        Public Overrides Property Category As String
            Get
                If MyBase.Category Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.Category.Trim
                End If
            End Get
            Set(value As String)
                MyBase.Category = value
            End Set
        End Property
	End Class

End Namespace

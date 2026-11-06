'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 11/14/2012 10:58:25 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class UUTChoice 
		Inherits esUUTChoice

		Public Sub New()

        End Sub
        Public Overrides Property DefaultIndex As Integer?
            Get
                If MyBase.DefaultIndex Is Nothing Then
                    Return -1
                Else
                    Return MyBase.DefaultIndex
                End If

            End Get
            Set(value As Integer?)
                MyBase.DefaultIndex = value
            End Set
        End Property

	End Class

End Namespace

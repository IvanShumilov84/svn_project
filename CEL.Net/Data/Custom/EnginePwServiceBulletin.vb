'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 2015-09-28 11:30:17 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class EnginePwServiceBulletin 
		Inherits esEnginePwServiceBulletin

		Public Sub New()

        End Sub

        Public Overrides Property ServiceBulletin1 As String
            Get
                If IsNothing(MyBase.ServiceBulletin1) Then
                    Return String.Empty
                Else
                    Return MyBase.ServiceBulletin1
                End If
            End Get
            Set(value As String)
                MyBase.ServiceBulletin1 = value
            End Set
        End Property
        Public Overrides Property ServiceBulletin2 As String
            Get
                If IsNothing(MyBase.ServiceBulletin2) Then
                    Return String.Empty
                Else
                    Return MyBase.ServiceBulletin2
                End If
            End Get
            Set(value As String)
                MyBase.ServiceBulletin2 = value
            End Set
        End Property

	End Class

End Namespace

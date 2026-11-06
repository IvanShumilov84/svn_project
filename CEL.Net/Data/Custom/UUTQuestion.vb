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

	Partial Public Class UUTQuestion 
		Inherits esUUTQuestion

		Public Sub New()

		End Sub

        Public Overrides Property HiLimit As Double?
            Get
                If MyBase.HiLimit Is Nothing Then
                    Return 9999
                Else
                    Return MyBase.HiLimit
                End If
            End Get
            Set(value As Double?)
                MyBase.HiLimit = value
            End Set
        End Property
        Public Overrides Property LoLimit As Double?
            Get
                If MyBase.LoLimit Is Nothing Then
                    Return -9999
                Else
                    Return MyBase.LoLimit
                End If

            End Get
            Set(value As Double?)
                MyBase.LoLimit = value
            End Set
        End Property
        Public Overrides Property DefaultValue As String
            Get
                If MyBase.DefaultValue Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.DefaultValue
                End If
            End Get
            Set(value As String)
                MyBase.DefaultValue = value
            End Set
        End Property
    End Class

End Namespace

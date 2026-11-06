'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 11/19/2012 11:37:52 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class HMITextChannels 
		Inherits esHMITextChannels

		Public Sub New()

		End Sub

        Public Overrides Property CollectionName As String
            Get
                If MyBase.CollectionName Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.CollectionName.Trim
                End If
            End Get
            Set(value As String)
                MyBase.CollectionName = value
            End Set
        End Property
        Public Overrides Property ChannelName As String
            Get
                If MyBase.ChannelName Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ChannelName.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ChannelName = value
            End Set
        End Property
        Public Overrides Property Description As String
            Get
                If MyBase.Description Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.Description
                End If
            End Get
            Set(value As String)
                MyBase.Description = value
            End Set
        End Property
        Public Overrides Property Source As String
            Get
                If MyBase.Source Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.Source.Trim
                End If
            End Get
            Set(value As String)
                MyBase.Source = value
            End Set
        End Property
        Public Overrides Property Destination As String
            Get
                If MyBase.Destination Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.Destination.Trim
                End If
            End Get
            Set(value As String)
                MyBase.Destination = value
            End Set
        End Property
        Public Overrides Property OPCGroup As String
            Get
                If MyBase.OPCGroup Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.OPCGroup.Trim
                End If
            End Get
            Set(value As String)
                MyBase.OPCGroup = value
            End Set
        End Property
        Public Overrides Property OPCItemID As String
            Get
                If MyBase.OPCItemID Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.OPCItemID.Trim
                End If
            End Get
            Set(value As String)
                MyBase.OPCItemID = value
            End Set
        End Property
        Public Overrides Property Tooltip As String
            Get
                If MyBase.Tooltip Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.Tooltip ' pas de trim
                End If
            End Get
            Set(value As String)
                MyBase.Tooltip = value
            End Set
        End Property
        Public Overrides Property Note As String
            Get
                If MyBase.Note Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.Note ' pas de trim
                End If
            End Get
            Set(value As String)
                MyBase.Note = value
            End Set
        End Property
      

	End Class

End Namespace

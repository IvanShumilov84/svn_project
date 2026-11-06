'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 11/14/2012 11:35:26 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

    Public Enum eBooleanState 'Correspond aux valeurs dans la Table BooleanState
        NoState = 0
        [True] = 1
        [False] = 2
    End Enum
	Partial Public Class HMIBooleanChannels 
		Inherits esHMIBooleanChannels

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
        Public Overrides Property Expression As String
            Get
                If MyBase.Expression Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.Expression
                End If
            End Get
            Set(value As String)
                MyBase.Expression = value
            End Set
        End Property
        Public Overrides Property ResetLatch As String
            Get
                If MyBase.ResetLatch Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ResetLatch.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ResetLatch = value
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
        Public Overrides Property TrueText As String
            Get
                If MyBase.TrueText Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.TrueText ' pas de trim
                End If
            End Get
            Set(value As String)
                MyBase.TrueText = value
            End Set
        End Property
        Public Overrides Property FalseText As String
            Get
                If MyBase.FalseText Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.FalseText ' pas de trim
                End If
            End Get
            Set(value As String)
                MyBase.FalseText = value
            End Set
        End Property
        Public Overrides Property FalseMessageText As String
            Get
                If MyBase.FalseMessageText Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.FalseMessageText ' pas de trim
                End If
            End Get
            Set(value As String)
                MyBase.FalseMessageText = value
            End Set
        End Property
        Public Overrides Property TrueMessageText As String
            Get
                If MyBase.TrueMessageText Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.TrueMessageText ' pas de trim
                End If
            End Get
            Set(value As String)
                MyBase.TrueMessageText = value
            End Set
        End Property
        Public Shadows Property InitialValue As eBooleanState
            Get
                If MyBase.InitialValue Is Nothing Then
                    Return eBooleanState.NoState
                Else
                    Return MyBase.InitialValue
                End If
            End Get
            Set(value As eBooleanState)
                MyBase.InitialValue = value
            End Set
        End Property
        Public Shadows Property FinalValue As eBooleanState
            Get
                If MyBase.FinalValue Is Nothing Then
                    Return eBooleanState.NoState
                Else
                    Return MyBase.FinalValue
                End If
            End Get
            Set(value As eBooleanState)
                MyBase.FinalValue = value
            End Set
        End Property

    End Class

End Namespace

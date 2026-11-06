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

	Partial Public Class HMINumericChannels 
		Inherits esHMINumericChannels

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
        Public Overrides Property Format As String
            Get
                If MyBase.Format Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.Format.Trim
                End If
            End Get
            Set(value As String)
                MyBase.Format = value
            End Set
        End Property
        Public Overrides Property MeanEnabled As Boolean?
            Get
                If MyBase.MeanEnabled Is Nothing Then
                    Return False
                Else
                    Return MyBase.MeanEnabled
                End If
            End Get
            Set(value As Boolean?)
                MyBase.MeanEnabled = value
            End Set
        End Property
        Public Overrides Property ThresholdHighHighMessageText As String
            Get
                If MyBase.ThresholdHighHighMessageText Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdHighHighMessageText.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdHighHighMessageText = value
            End Set
        End Property
        Public Overrides Property ThresholdHighHighSource As String
            Get
                If MyBase.ThresholdHighHighSource Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdHighHighSource.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdHighHighSource = value
            End Set
        End Property
        Public Overrides Property ThresholdHighHighColor As String
            Get
                If MyBase.ThresholdHighHighColor Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdHighHighColor.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdHighHighColor = value
            End Set
        End Property
        Public Overrides Property ThresholdHighMessageText As String
            Get
                If MyBase.ThresholdHighMessageText Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdHighMessageText.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdHighMessageText = value
            End Set
        End Property
        Public Overrides Property ThresholdHighSource As String
            Get
                If MyBase.ThresholdHighSource Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdHighSource.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdHighHighSource = value
            End Set
        End Property
        Public Overrides Property ThresholdHighColor As String
            Get
                If MyBase.ThresholdHighColor Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdHighColor.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdHighColor = value
            End Set
        End Property
        Public Overrides Property ThresholdLowLowMessageText As String
            Get
                If MyBase.ThresholdLowLowMessageText Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdLowLowMessageText.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdLowLowMessageText = value
            End Set
        End Property
        Public Overrides Property ThresholdLowLowSource As String
            Get
                If MyBase.ThresholdLowLowSource Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdLowLowSource.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdLowLowSource = value
            End Set
        End Property
        Public Overrides Property ThresholdLowLowColor As String
            Get
                If MyBase.ThresholdLowLowColor Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdLowLowColor.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdLowLowColor = value
            End Set
        End Property
        Public Overrides Property ThresholdLowMessageText As String
            Get
                If MyBase.ThresholdLowMessageText Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdLowMessageText.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdLowMessageText = value
            End Set
        End Property
        Public Overrides Property ThresholdLowSource As String
            Get
                If MyBase.ThresholdLowSource Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdLowSource.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdLowLowSource = value
            End Set
        End Property
        Public Overrides Property ThresholdLowColor As String
            Get
                If MyBase.ThresholdLowColor Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdLowColor.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdLowColor = value
            End Set
        End Property
        Public Overrides Property ThresholdNormalColor As String
            Get
                If MyBase.ThresholdNormalColor Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.ThresholdNormalColor.Trim
                End If
            End Get
            Set(value As String)
                MyBase.ThresholdNormalColor = value
            End Set
        End Property
        Public Overrides Property HighTriggerEnabled As Boolean?
            Get
                If MyBase.HighTriggerEnabled Is Nothing Then
                    Return False
                Else
                    Return MyBase.HighTriggerEnabled
                End If
            End Get
            Set(value As Boolean?)
                MyBase.HighTriggerEnabled = value
            End Set
        End Property
        Public Overrides Property LowTriggerEnabled As Boolean?
            Get
                If MyBase.LowTriggerEnabled Is Nothing Then
                    Return False
                Else
                    Return MyBase.LowTriggerEnabled
                End If
            End Get
            Set(value As Boolean?)
                MyBase.LowTriggerEnabled = value
            End Set
        End Property
       

    End Class

End Namespace

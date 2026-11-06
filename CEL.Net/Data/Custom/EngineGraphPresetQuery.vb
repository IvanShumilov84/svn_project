'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 1/21/2016 2:53:19 PM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

    <System.ComponentModel.Browsable(False)>
       Partial Public Class EngineGraphPresetQuery
        Inherits esEngineGraphPresetQuery

        Public Sub New()

        End Sub

        <System.ComponentModel.Browsable(False)>
        Protected Overrides ReadOnly Property Meta As IMetadata
            Get
                Return MyBase.Meta
            End Get
        End Property
    End Class

End Namespace


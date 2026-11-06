'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 04/02/2014 4:43:16 PM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class SecurityLevel 
		Inherits esSecurityLevel

		Public Sub New()

        End Sub

        '**************************************************************************
        'Security Level     Default Password    Right on the TestSession Database
        '**************************************************************************
        'Operator           Operator            View
        'Technician         Technician          View or Add
        'Test Manager       Test Manager        View or Add or Modify or Delete Data

        Public Enum eSecurityLevel
            [Operator] = 1
            Technician = 2
            TestManager = 3
            'Eventuellement Ajouter Administrator = 9
        End Enum

        Public Overloads Property SecurityLevelEnum As eSecurityLevel
            Get
                Return MyBase.SecurityLevelID
            End Get
            Set(value As eSecurityLevel)
                MyBase.SecurityLevelID = value
            End Set
        End Property
        'Cle primaire utilisé un peu partout dans les autres tables... difficile de forcer a remplacer par l'enum
        '<Obsolete("Obselete property, You MUST use TxRxEnum", True)> _
        'Public Overrides Property SecurityLevelID As Integer?
        '    Get
        '        Return MyBase.SecurityLevelID
        '    End Get
        '    Set(value As Integer?)
        '        MyBase.SecurityLevelID = value
        '    End Set
        'End Property

	End Class

End Namespace

'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 4/10/2015 10:39:47 AM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class Gtcp8598 
		Inherits esGtcp8598

		Public Sub New()

        End Sub

        Public Enum eRepairType
            NotApplicable = 0
            RepairedEngine = 1
            OverhauledEngine = 2
        End Enum
      


        Public Overloads Property RepairTypeEnum As eRepairType
            Get
                Return MyBase.RepairTypeID
            End Get
            Set(value As eRepairType)
                MyBase.RepairTypeID = value
            End Set
        End Property

        'Je ne peux pas la mettre obsolete pcq ce champ fait parti d'une clé primaire <Obsolete("Obselete property, You MUST use RepairTypeEnum", True)> _
        Public Overrides Property RepairTypeID As Short?
            Get
                Return MyBase.RepairTypeID
            End Get
            Set(value As Short?)
                MyBase.RepairTypeID = value
            End Set
        End Property

    End Class

End Namespace

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
Imports CEL.Data.BusinessObjects.SecurityLevel

Namespace BusinessObjects

	Partial Public Class Uut 
		Inherits esUut

		Public Sub New()

        End Sub



        Public Overloads Property SecurityLevelEnum As eSecurityLevel
            Get
                If IsNothing(MyBase.SecurityLevelID) Then
                    MyBase.SecurityLevelID = 1 'Operator
                End If

                Return MyBase.SecurityLevelID
            End Get
            Set(value As eSecurityLevel)
                MyBase.SecurityLevelID = value
            End Set
        End Property

	End Class

End Namespace

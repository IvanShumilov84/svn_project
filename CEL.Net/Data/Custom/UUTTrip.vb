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

	Partial Public Class UUTTrip 
		Inherits esUUTTrip

        Public Enum eTripType 'Doit matcher avec les valeurs de la table UUT_Trip_Type
            Speed = 1
            Temperature = 2
            Torque = 3
            Vibration = 4
            Frequency = 5
            Pressure = 6
            Voltage = 7
            Current = 8
        End Enum

		Public Sub New()

        End Sub

        Public Property Type As eTripType
            Get
                Return CType(MyBase.TripTypeID, eTripType)
            End Get
            Set(value As eTripType)
                MyBase.TripTypeID = value
            End Set
        End Property



	End Class

End Namespace

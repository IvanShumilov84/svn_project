'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 11/28/2012 1:55:09 PM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class DacnTemplateReal 
		Inherits esDacnTemplateReal

		Public Sub New()

        End Sub

        Public Overrides Property LastCalibDate As System.String
            Get
                If MyBase.LastCalibDate Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.LastCalibDate.Trim
                End If
            End Get
            Set(value As System.String)
                MyBase.LastCalibDate = value
            End Set
        End Property
        Public Overrides Property CalibrationMeasured As System.String
            Get
                If MyBase.CalibrationMeasured Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.CalibrationMeasured.Trim
                End If
            End Get
            Set(value As System.String)
                MyBase.CalibrationMeasured = value
            End Set
        End Property
        Public Overrides Property EngUnit As System.String
            Get
                If MyBase.EngUnit Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.EngUnit
                End If
            End Get
            Set(value As System.String)
                MyBase.EngUnit = value
            End Set
        End Property
        Public Overrides Property Comments As System.String
            Get
                If MyBase.Comments Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.Comments
                End If
            End Get
            Set(value As System.String)
                MyBase.Comments = value
            End Set
        End Property
        Public Overrides Property HMIAliasName As System.String
            Get
                If MyBase.HMIAliasName Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.HMIAliasName
                End If
            End Get
            Set(value As System.String)
                MyBase.HMIAliasName = value
            End Set
        End Property
        Public Overrides Property TransducerRange As System.String
            Get
                If MyBase.TransducerRange Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.TransducerRange
                End If
            End Get
            Set(value As System.String)
                MyBase.TransducerRange = value
            End Set
        End Property

        Public Overrides Property CalibrationDueDate1 As String
            Get
                If MyBase.CalibrationDueDate1 Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.CalibrationDueDate1
                End If
                Return MyBase.CalibrationDueDate1
            End Get
            Set(value As String)
                MyBase.CalibrationDueDate1 = value
            End Set
        End Property
        Public Overrides Property CalibrationDueDate2 As String
            Get
                If MyBase.CalibrationDueDate2 Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.CalibrationDueDate2
                End If
                Return MyBase.CalibrationDueDate2
            End Get
            Set(value As String)
                MyBase.CalibrationDueDate2 = value
            End Set
        End Property
        Public Overrides Property CalibrationDueDate3 As String
            Get
                If MyBase.CalibrationDueDate3 Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.CalibrationDueDate3
                End If
                Return MyBase.CalibrationDueDate3
            End Get
            Set(value As String)
                MyBase.CalibrationDueDate3 = value
            End Set
        End Property
        Public Overrides Property CalibrationDueDate4 As String
            Get
                If MyBase.CalibrationDueDate4 Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.CalibrationDueDate4
                End If
                Return MyBase.CalibrationDueDate4
            End Get
            Set(value As String)
                MyBase.CalibrationDueDate4 = value
            End Set
        End Property

        Public Overrides Property CalibrationRange As String
            Get
                If MyBase.CalibrationRange Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.CalibrationRange
                End If
                Return MyBase.CalibrationRange
            End Get
            Set(value As String)
                MyBase.CalibrationRange = value
            End Set
        End Property

        Public Overrides Property CalibrationHumidity As String
            Get
                If MyBase.CalibrationHumidity Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.CalibrationHumidity
                End If
                Return MyBase.CalibrationHumidity
            End Get
            Set(value As String)
                MyBase.CalibrationHumidity = value
            End Set
        End Property

        Public Overrides Property CalibrationTemp As String
            Get
                If MyBase.CalibrationTemp Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.CalibrationTemp
                End If
                Return MyBase.CalibrationTemp
            End Get
            Set(value As String)
                MyBase.CalibrationTemp = value
            End Set
        End Property

        Public Overrides Property TCTransducerSN As String
            Get
                If MyBase.TCTransducerSN Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.TCTransducerSN
                End If
                Return MyBase.TCTransducerSN
            End Get
            Set(value As String)
                MyBase.TCTransducerSN = value
            End Set
        End Property

        Public Overloads Property InChannelType As Integer?
            Get
                If MyBase.InChannelType Is Nothing Then
                    Return 0
                Else
                    Return MyBase.InChannelType
                End If
                Return MyBase.InChannelType
            End Get
            Set(value As Integer?)
                MyBase.InChannelType = value
            End Set
        End Property
        ' \/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/        \/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
        '                                 New propterties has overload. added by phil
        ' /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\        /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
        Public Overloads Property statisticsOnInteger As Integer?
            Get
                Return CInt(MyBase.StatisticsOn) * -1
            End Get
            Set(value As Integer?)
                MyBase.StatisticsOn = value
            End Set
        End Property

        Public Overloads Property AlarmOnInteger As Integer?
            Get
                Return CInt(MyBase.AlarmOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.AlarmOn = value
            End Set
        End Property

        Public Overloads Property TextConversionOnInteger As Integer?
            Get
                Return CInt(MyBase.TextConversionOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.TextConversionOn = value
            End Set
        End Property
        Public Overloads Property DataAcquisitionOnInteger As Integer?
            Get
                Return CInt(MyBase.DataAcquisitionOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.DataAcquisitionOn = value
            End Set
        End Property
        Public Overloads Property DataProcessingOnInteger As Integer?
            Get
                Return CInt(MyBase.DataProcessingOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.DataProcessingOn = value
            End Set
        End Property


        Public Overloads Property EventOnInteger As Integer?
            Get
                Return CInt(MyBase.EventOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.EventOn = value
            End Set
        End Property
        Public Overloads Property HighHighLimitOnInteger As Integer?
            Get
                Return CInt(MyBase.HighHighLimitOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.HighHighLimitOn = value
            End Set
        End Property
        Public Overloads Property HighLimitOnInteger As Integer?
            Get
                Return CInt(MyBase.HighLimitOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.HighLimitOn = value
            End Set
        End Property
        Public Overloads Property LowLimitOnInteger As Integer?
            Get
                Return CInt(MyBase.LowLimitOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.LowLimitOn = value
            End Set
        End Property
        Public Overloads Property LowLowLimitOnInteger As Integer?
            Get
                Return CInt(MyBase.LowLowLimitOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.LowLowLimitOn = value
            End Set
        End Property
        Public Overloads Property HighHighAlarmOnInteger As Integer?
            Get
                Return CInt(MyBase.HighHighAlarmOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.HighHighAlarmOn = value
            End Set
        End Property
        Public Overloads Property HighAlarmOnInteger As Integer?
            Get
                Return CInt(MyBase.HighAlarmOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.HighAlarmOn = value
            End Set
        End Property
        Public Overloads Property LowAlarmOnInteger As Integer?
            Get
                Return CInt(MyBase.LowAlarmOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.LowAlarmOn = value
            End Set
        End Property
        Public Overloads Property LowLowAlarmOnInteger As Integer?
            Get
                Return CInt(MyBase.LowLowAlarmOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.LowLowAlarmOn = value
            End Set
        End Property

        Public Overloads Property MinMaxOnInteger As Integer?
            Get
                Return CInt(MyBase.MinMaxOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.MinMaxOn = value
            End Set
        End Property
        Public Overloads Property DeltaVOnInteger As Integer?
            Get
                Return CInt(MyBase.DeltaVOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.DeltaVOn = value
            End Set
        End Property
        Public Overloads Property SumVOnInteger As Integer?
            Get
                Return CInt(MyBase.SumVOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.SumVOn = value
            End Set
        End Property
        Public Overloads Property DeltaTOnInteger As Integer?
            Get
                Return CInt(MyBase.DeltaTOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.DeltaTOn = value
            End Set
        End Property
        Public Overloads Property SumTOnInteger As Integer?
            Get
                Return CInt(MyBase.SumTOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.SumTOn = value
            End Set
        End Property
        Public Overloads Property RocOnInteger As Integer?
            Get
                Return CInt(MyBase.RocOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.RocOn = value
            End Set
        End Property
        Public Overloads Property AvgOnInteger As Integer?
            Get
                Return CInt(MyBase.AvgOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.AvgOn = value
            End Set
        End Property

        Public Overloads Property ValidArgOnInteger As Integer?
            Get
                Return CInt(MyBase.ValidArgOn) * -1
            End Get

            Set(value As Integer?)
                MyBase.ValidArgOn = value
            End Set
        End Property


    End Class
End Namespace

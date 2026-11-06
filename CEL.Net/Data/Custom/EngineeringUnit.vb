'===============================================================================
'                   EntitySpaces Studio by EntitySpaces, LLC
'            Persistence Layer and Business Objects for Microsoft .NET
'            EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
'                         http://www.entityspaces.net
'===============================================================================
' EntitySpaces Version : 2012.1.0930.0
' EntitySpaces Driver  : Access
' Date Generated       : 11/21/2012 2:27:38 PM
'===============================================================================

Imports System

Imports EntitySpaces.Core
Imports EntitySpaces.Interfaces
Imports EntitySpaces.DynamicQuery

Namespace BusinessObjects

	Partial Public Class EngineeringUnit 
		Inherits esEngineeringUnit

		Public Sub New()

        End Sub

        Public Enum eEngineeringUnit

            None = 0
            Empty = 10
            PSIG = 1
            PSIA = 2
            PSID = 8
            INH2OG = 11
            INH2OA = 13
            INHGG = 14
            INHGA = 16
            BAR = 17
            MBAR = 19
            INHGD = 32
            INH2OD = 33
            MBARA = 34
            KPAG = 35
            KPAA = 37
            BARA = 39
            BARD = 40
            BARG = 41
            KPAD = 42
            DEGC = 50
            DEGF = 55
            DEGFD = 57
            DEGCD = 58
            DEGK = 61
            DEGR = 62
            PCT = 150
            LBSFT = 200
            BTU_PER_POUND = 202
            NM = 203
            RPM = 250
            MPS_SQ_RMS = 300
            CMPS_RMS = 301
            G_RMS = 302
            MILS = 303
            IPS_P = 308
            IPS_PP = 309
            IPS = 310
            IPS_RMS = 317
            MMPS_RMS = 318
            MMPS = 319
            MMPS_P = 320
            MMPS_PP = 321
            G_P = 322
            G_PP = 323
            mils_P = 324
            mils_PP = 325
            HZ = 400
            KHZ = 401
            HOUR = 500
            MIN = 501
            SEC = 502
            mSec = 503
            MONTH = 507
            DEGS = 600
            Ohms = 700
            QT = 750
            LITER = 752
            GAL = 753
            VDC = 800
            mVDC = 801
            mA = 802
            W = 803
            mW = 803
            AMP = 805
            KW = 807
            VAC = 808
            HP = 809
            SHP = 816
            KVA = 817
            LBS = 850
            KG = 853
            PPH = 900
            PPM = 902
            PPS = 907
            KGPH = 909
            KGPS = 910
            SFC = 1000
            Time = 1001
            STARTS = 1002
            DELTA = 1003
            CYCLES = 1004
            HEX = 1010

        End Enum



        Public Overrides Property DisplayName As String
            Get
                If MyBase.DisplayName Is Nothing Then
                    Return String.Empty
                Else
                    Return MyBase.DisplayName
                End If
            End Get
            Set(value As String)
                MyBase.DisplayName = value
            End Set
        End Property


    End Class

End Namespace

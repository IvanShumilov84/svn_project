Imports System
Imports System.Collections.Generic
Imports System.Xml




#Region "UnitConverter"


Public Class ExpressionUnitConversion

#Region "Constants"
    Private Const BAR_TO_INHG As String = "29.52999"
    Private Const MBAR_TO_PSI As String = "0.01450377377"
    Private Const MBAR_TO_KPA As String = MBAR_TO_PSI + "×" + PSI_TO_KPA
    Private Const HOUR_TO_MINUTES As String = "60.0"
    Private Const HOUR_TO_SECONDS As String = "3600.0"
    Private Const MINUTE_TO_SECONDS As String = "60.0"
    Private Const KG_TO_LBS As String = "2.204622622"
    Private Const KW_TO_HP As String = "1.341022"
    Private Const USGALLON_TO_LITERS As String = "3.785411784"
    Private Const PPH_TO_KGPH As String = "1.0 / " + KG_TO_LBS
    Private Const PPM_TO_PPS As String = "1.0 / 60.0"
    Private Const PPH_TO_PPS As String = "1.0 / 3600.0"
    Private Const PSI_TO_INH2O As String = "27.680672603"
    Private Const INH2O_TO_MMH2O As String = "25.40000"
    Private Const PSI_TO_INHG As String = "2.03602046"
    Private Const PSI_TO_MBAR As String = "68.9475729"
    Private Const PSI_TO_BAR As String = "0.0689475729"
    Private Const PSI_TO_KPA As String = "6.89475729"
    Private Const KPA_TO_PSI As String = "1.0 / " + PSI_TO_KPA
    Private Const KPA_TO_BAR As String = "1.0 / 100.0"
    Private Const KPA_TO_MBAR As String = "10.0"
    Private Const INCH_TO_MM As String = "25.4"
    Private Const INCH_TO_CM As String = "2.54"
    Private Const GTOMS2 As String = "9.80665"
    Private Const RMS_TO_PEEK As String = "1.41421"
    Private Const PEEK_TO_RMS As String = "1.0 / " + RMS_TO_PEEK

    Private Const KPA_TO_INH2O As String = "4.01865"

#End Region

#Region "Fields"


    Private _ConversionTable As New Dictionary(Of Pair(Of String, String), IExpressionUnitConverter)
    Private _AbsolutePressureList As New HashSet(Of String)

#End Region

#Region "Constructors"
    Public Sub New()


        _AbsolutePressureList.Add("PSIA")
        _AbsolutePressureList.Add("KPAA")
        _AbsolutePressureList.Add("INH2OA")
        _AbsolutePressureList.Add("INHGA")
        _AbsolutePressureList.Add("BARA")
        _AbsolutePressureList.Add("MBARA")

        ' TODO:    On doit statuer et documenter une fois pour toute le facteur de conversion a utiliser pour INH2O puisque celui-ci de façon général dépend de la 
        '          température Standard utilisé dans l'industrie Aerospatial ?
        Dim conversionPair As Pair(Of String, String)


        _ConversionTable.Add(New Pair(Of String, String)("VDC", "PCT"), New ExpressionMxBConverter("10.0", "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("MVDC", "VDC"), New ExpressionMxBConverter("0.001", "0.0"))


        'Power  *******************************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("KW", "W"), New ExpressionMxBConverter("1000", "0")) 'unit test done convert/invert
        _ConversionTable.Add(New Pair(Of String, String)("KW", "HP"), New ExpressionMxBConverter(KW_TO_HP, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("KW", "SHP"), New ExpressionMxBConverter(KW_TO_HP, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("KW", "KVA"), New ExpressionMxBConverter("1.0", "0"))

        'Frequency/Speed  *********************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("KHZ", "HZ"), New ExpressionMxBConverter("1000", "0.0")) 'unit test done convert/invert
        _ConversionTable.Add(New Pair(Of String, String)("RPM", "HZ"), New ExpressionMxBConverter("1.0/60", "0.0")) 'unit test done convert/invert

        'Flow  ********************************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("LPH", "LPM"), New ExpressionMxBConverter("1.0/" + HOUR_TO_MINUTES, "0")) 'unit test done
        _ConversionTable.Add(New Pair(Of String, String)("LPM", "LPH"), New ExpressionMxBConverter(HOUR_TO_MINUTES, "0")) 'unit test done
        _ConversionTable.Add(New Pair(Of String, String)("PPH", "KGPH"), New ExpressionMxBConverter(PPH_TO_KGPH, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("USGPH", "LPH"), New ExpressionMxBConverter(USGALLON_TO_LITERS, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("USGPH", "LPM"), New ExpressionMxBConverter(USGALLON_TO_LITERS + "/" + HOUR_TO_MINUTES, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("USGPH", "USGPM"), New ExpressionMxBConverter("1.0/" + HOUR_TO_MINUTES, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("USGPM", "LPH"), New ExpressionMxBConverter(USGALLON_TO_LITERS + "×" + HOUR_TO_MINUTES, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("USGPM", "LPM"), New ExpressionMxBConverter(USGALLON_TO_LITERS, "0"))

        _ConversionTable.Add(New Pair(Of String, String)("PPM", "PPS"), New ExpressionMxBConverter(PPM_TO_PPS, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("PPM", "KGPS"), New ExpressionMxBConverter(PPM_TO_PPS + "/" + KG_TO_LBS, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("PPH", "KGPS"), New ExpressionMxBConverter(PPH_TO_PPS + "/" + KG_TO_LBS, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("PPM", "KGPM"), New ExpressionMxBConverter("1.0/" + KG_TO_LBS, "0"))


        'Volume  ********************************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("GAL", "LITER"), New ExpressionMxBConverter(USGALLON_TO_LITERS, "0"))

        'Vibration  **************************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("IPS_RMS", "MMPS_RMS"), New ExpressionMxBConverter(INCH_TO_MM, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("IPS_RMS", "CMPS_RMS"), New ExpressionMxBConverter(INCH_TO_CM, "0"))

        _ConversionTable.Add(New Pair(Of String, String)("IPS_P", "MMPS_P"), New ExpressionMxBConverter(INCH_TO_MM, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("IPS_P", "CMPS_P"), New ExpressionMxBConverter(INCH_TO_CM, "0"))

        _ConversionTable.Add(New Pair(Of String, String)("IPS_PP", "MMPS_PP"), New ExpressionMxBConverter(INCH_TO_MM, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("IPS_PP", "CMPS_PP"), New ExpressionMxBConverter(INCH_TO_CM, "0"))

        _ConversionTable.Add(New Pair(Of String, String)("G_RMS", "MPS_SQ_RMS"), New ExpressionMxBConverter(GTOMS2, "0"))

        _ConversionTable.Add(New Pair(Of String, String)("IPS_RMS", "IPS_P"), New ExpressionMxBConverter(RMS_TO_PEEK, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("IPS_P", "IPS_RMS"), New ExpressionMxBConverter(PEEK_TO_RMS, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("in/sec RMS", "in/sec Pk"), New ExpressionMxBConverter(PEEK_TO_RMS, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("in/sec Pk", "in/sec RMS"), New ExpressionMxBConverter(RMS_TO_PEEK, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("in/sec RMS", "in/s Pk"), New ExpressionMxBConverter(PEEK_TO_RMS, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("in/sec Pk", "in/s RMS"), New ExpressionMxBConverter(RMS_TO_PEEK, "0"))

        'Temperature  ************************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("DEGC", "DEGF"), New ExpressionMxBConverter("1.8", "32.0"))
        _ConversionTable.Add(New Pair(Of String, String)("DEGC", "DEGR"), New ExpressionMxBConverter("1.8", "491.67"))
        _ConversionTable.Add(New Pair(Of String, String)("DEGF", "DEGR"), New ExpressionMxBConverter("1.0", "459.67"))
        _ConversionTable.Add(New Pair(Of String, String)("DEGCD", "DEGFD"), New ExpressionMxBConverter("1.8", "0.0"))

        'Time  *******************************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("HOUR", "MIN"), New ExpressionMxBConverter(HOUR_TO_MINUTES, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("MIN", "SEC"), New ExpressionMxBConverter(MINUTE_TO_SECONDS, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("HOUR", "SEC"), New ExpressionMxBConverter(HOUR_TO_SECONDS, "0"))

        'PSI PSI  ****************************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("PSID", "PSIG"), New ExpressionMxBConverter("1.0", "0.0"))
        conversionPair = New Pair(Of String, String)("PSID", "PSIA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0", "0"))

        'MBAR VS KPA *************************************************************************************************************************
        conversionPair = New Pair(Of String, String)("MBAR", "KPAA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, MBAR_TO_KPA, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("MBARA", "KPAA"), New ExpressionMxBConverter(MBAR_TO_KPA, "0"))

        'MBAR VS PSI *************************************************************************************************************************
        conversionPair = New Pair(Of String, String)("MBAR", "PSIA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0/" + PSI_TO_MBAR, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("MBARA", "PSIA"), New ExpressionMxBConverter("1.0/" + PSI_TO_MBAR, "0"))

        'BAR VS PSI *************************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("BARD", "PSID"), New ExpressionMxBConverter("1.0/" + PSI_TO_BAR, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("BAR", "PSID"), New ExpressionMxBConverter("1.0/" + PSI_TO_BAR, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("BARD", "PSIG"), New ExpressionMxBConverter("1.0/" + PSI_TO_BAR, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("BAR", "PSIG"), New ExpressionMxBConverter("1.0/" + PSI_TO_BAR, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("BARG", "PSIG"), New ExpressionMxBConverter("1.0/" + PSI_TO_BAR, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("BARA", "PSIA"), New ExpressionMxBConverter("1.0/" + PSI_TO_BAR, "0"))
        conversionPair = New Pair(Of String, String)("BARG", "PSIA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0/" + PSI_TO_BAR, "0"))
        conversionPair = New Pair(Of String, String)("BAR", "PSIA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0/" + PSI_TO_BAR, "0"))
        conversionPair = New Pair(Of String, String)("BARA", "PSIG")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0/" + PSI_TO_BAR, "0"))
        conversionPair = New Pair(Of String, String)("PSIG", "BARA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, PSI_TO_BAR, "0"))

        'KPA VS BAR *************************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("KPA", "BAR"), New ExpressionMxBConverter(KPA_TO_BAR, "0")) 'A tester
        _ConversionTable.Add(New Pair(Of String, String)("KPA", "BARG"), New ExpressionMxBConverter(KPA_TO_BAR, "0")) 'A tester
        _ConversionTable.Add(New Pair(Of String, String)("KPAG", "BARG"), New ExpressionMxBConverter(KPA_TO_BAR, "0")) 'A tester
        _ConversionTable.Add(New Pair(Of String, String)("KPAG", "BAR"), New ExpressionMxBConverter(KPA_TO_BAR, "0")) 'A tester
        _ConversionTable.Add(New Pair(Of String, String)("KPAA", "BARA"), New ExpressionMxBConverter(KPA_TO_BAR, "0")) 'A tester
        conversionPair = New Pair(Of String, String)("KPA", "BARA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, KPA_TO_BAR, "0")) 'A tester
        conversionPair = New Pair(Of String, String)("KPAG", "BARA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, KPA_TO_BAR, "0")) 'A tester


        'KPA VS MBAR ***********************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("KPA", "MBAR"), New ExpressionMxBConverter(KPA_TO_MBAR, "0")) 'A tester
        _ConversionTable.Add(New Pair(Of String, String)("KPAG", "MBAR"), New ExpressionMxBConverter(KPA_TO_MBAR, "0")) 'A tester
        _ConversionTable.Add(New Pair(Of String, String)("KPAA", "MBARA"), New ExpressionMxBConverter(KPA_TO_MBAR, "0")) 'A tester
        conversionPair = New Pair(Of String, String)("KPA", "MBARA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, KPA_TO_MBAR, "0")) 'A tester
        conversionPair = New Pair(Of String, String)("KPAG", "MBARA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, KPA_TO_MBAR, "0")) 'A tester


        'KPA VS PSI ***********************************************************************************************************************
        conversionPair = New Pair(Of String, String)("KPAG", "PSIA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, KPA_TO_PSI, "0")) 'A tester
        _ConversionTable.Add(New Pair(Of String, String)("PSIG", "KPAG"), New ExpressionMxBConverter(PSI_TO_KPA, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSI", "KPA"), New ExpressionMxBConverter(PSI_TO_KPA, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSIG", "KPA"), New ExpressionMxBConverter(PSI_TO_KPA, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSIA", "KPAA"), New ExpressionMxBConverter(PSI_TO_KPA, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSI", "KPAG"), New ExpressionMxBConverter(PSI_TO_KPA, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSID", "KPAG"), New ExpressionMxBConverter(PSI_TO_KPA, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSID", "KPA"), New ExpressionMxBConverter(PSI_TO_KPA, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSID", "KPAD"), New ExpressionMxBConverter(PSI_TO_KPA, "0.0"))
        conversionPair = New Pair(Of String, String)("PSIG", "KPAA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, PSI_TO_KPA, "0.0"))
        conversionPair = New Pair(Of String, String)("PSI", "KPAA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, PSI_TO_KPA, "0.0"))

        'KPA VS INH2O  ***********************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("KPA", "INH2OG"), New ExpressionMxBConverter(KPA_TO_INH2O, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("KPAA", "INH2OG"), New ExpressionMxBConverter(KPA_TO_INH2O, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("KPAG", "INH2OG"), New ExpressionMxBConverter(KPA_TO_INH2O, "0.0"))

        'PSI VS KPa  ***********************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("KPAA", "PSIA"), New ExpressionMxBConverter("1.0/" + PSI_TO_KPA, "0.0"))
        conversionPair = New Pair(Of String, String)("KPAG", "KPAA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0", "0"))


        'PSI VS INHG  ***********************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("PSIA", "INHGA"), New ExpressionMxBConverter(PSI_TO_INHG, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("BARA", "INHGA"), New ExpressionMxBConverter(BAR_TO_INHG, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("BARG", "INHGG"), New ExpressionMxBConverter(BAR_TO_INHG, "0"))

        conversionPair = New Pair(Of String, String)("PSID", "INHGA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, PSI_TO_INHG, "0.0"))
        conversionPair = New Pair(Of String, String)("PSIG", "INHGA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, PSI_TO_INHG, "0.0"))
        conversionPair = New Pair(Of String, String)("PSIG", "PSIA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0", "0"))
        conversionPair = New Pair(Of String, String)("INHG", "PSIA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0/" + PSI_TO_INHG, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSID", "INHG"), New ExpressionMxBConverter(PSI_TO_INHG, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSIG", "INHG"), New ExpressionMxBConverter(PSI_TO_INHG, "0"))
        conversionPair = New Pair(Of String, String)("INHGG", "PSIA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0/" + PSI_TO_INHG, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSID", "INHGG"), New ExpressionMxBConverter(PSI_TO_INHG, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSIG", "INHGG"), New ExpressionMxBConverter(PSI_TO_INHG, "0.0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSID", "INHGD"), New ExpressionMxBConverter(PSI_TO_INHG, "0.0"))


        'INH2O VS PSI  ***********************************************************************************************************************
        conversionPair = New Pair(Of String, String)("INH2O", "PSIA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0/" + PSI_TO_INH2O, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSI", "INH2O"), New ExpressionMxBConverter(PSI_TO_INH2O, "0")) 'Unit Test done 2 way convert/invert
        _ConversionTable.Add(New Pair(Of String, String)("PSI", "INH2OG"), New ExpressionMxBConverter(PSI_TO_INH2O, "0")) 'Unit Test done 2 way convert/invert
        _ConversionTable.Add(New Pair(Of String, String)("PSID", "INH2O"), New ExpressionMxBConverter(PSI_TO_INH2O, "0")) 'Unit Test done 2 way convert/invert
        _ConversionTable.Add(New Pair(Of String, String)("PSID", "INH2OG"), New ExpressionMxBConverter(PSI_TO_INH2O, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("PSIG", "INH2O"), New ExpressionMxBConverter(PSI_TO_INH2O, "0")) 'Unit Test done 2 way convert/invert
        _ConversionTable.Add(New Pair(Of String, String)("PSIG", "INH2OG"), New ExpressionMxBConverter(PSI_TO_INH2O, "0")) 'Unit Test done 2 way convert/invert
        _ConversionTable.Add(New Pair(Of String, String)("PSID", "INH2OD"), New ExpressionMxBConverter(PSI_TO_INH2O, "0"))

        conversionPair = New Pair(Of String, String)("INH2OG", "PSIA")
        _ConversionTable.Add(conversionPair, New ExpressionGageVsAbsolutePressureConverter(conversionPair, AddressOf Me.GetConversionInfo, AddressOf ProcessUnitConversion, "1.0/" + PSI_TO_INH2O, "0"))


        'MMH2O VS INH2O  ***********************************************************************************************************************
        _ConversionTable.Add(New Pair(Of String, String)("INH2O", "MMH2O"), New ExpressionMxBConverter(INH2O_TO_MMH2O, "0"))
        _ConversionTable.Add(New Pair(Of String, String)("INH2OG", "MMH2O"), New ExpressionMxBConverter(INH2O_TO_MMH2O, "0"))

    End Sub

    Private _BaroChannelName As String = "P_BARO_PSIA"
    Private _BaroUnit As String = "PSIA"
    'Private _FromUnit As String
    'Private _ToUnit As String
    Public Sub SetBaroChannel(ByVal baroChannelName As String, ByVal baroUnit As String)

        _BaroChannelName = baroChannelName
        _BaroUnit = baroUnit

    End Sub
    Private Sub GetBaroChannel(ByRef baroChannelName As String, ByRef baroUnit As String)

        baroChannelName = _BaroChannelName
        baroUnit = _BaroUnit

    End Sub
    'Private Sub GetConversionInfo(ByRef baroChannelName As String, ByRef baroUnit As String, ByRef fromUnit As String, ByRef toUnit As String, ByRef absPressureList As HashSet(Of String))
    Private Sub GetConversionInfo(ByRef baroChannelName As String, ByRef baroUnit As String, ByRef absPressureList As HashSet(Of String))

        baroChannelName = _BaroChannelName
        baroUnit = _BaroUnit
        'fromUnit = _FromUnit
        'toUnit = _ToUnit
        absPressureList = _AbsolutePressureList

    End Sub

    Public Function ProcessUnitConversion(ByVal channelName As String, ByVal fromUnit As String, ByVal toUnit As String, ByRef outputExpression As String) As Boolean
        Dim bError As Boolean = False
        Dim cF As IExpressionUnitConverter

        outputExpression = String.Format("({0})", channelName) 'Valeur par si aucune conversion c'est le channel lui-meme
        fromUnit = fromUnit.ToUpper
        '_FromUnit = fromUnit.ToUpper
        toUnit = toUnit.ToUpper
        '_ToUnit = toUnit.ToUpper

        If fromUnit <> toUnit Then
            If _ConversionTable.ContainsKey(New Pair(Of String, String)(fromUnit, toUnit)) Then
                cF = _ConversionTable(New Pair(Of String, String)(fromUnit, toUnit))
                outputExpression = cF.Convert(channelName)
            ElseIf _ConversionTable.ContainsKey(New Pair(Of String, String)(toUnit, fromUnit)) Then
                cF = _ConversionTable(New Pair(Of String, String)(toUnit, fromUnit))
                outputExpression = cF.Inverse(channelName)
            Else
                'Can't perform conversion from " & fromUnits & " to " & toUnits)
                bError = True
            End If
        End If

        Return bError
    End Function

#End Region



End Class



#Region "IUnitConverter"
''' <summary>
''' Interface definition for conversion between units
''' and its corresponding inverse conversion
''' </summary>
''' <remarks></remarks>
Public Interface IExpressionUnitConverter
    ''' <summary>
    ''' Converts the specified value from a unit to another unit.
    ''' </summary>
    ''' <param name="Value">The value to convert from.</param>
    ''' <returns>The converted value</returns>
    Function Convert(ByVal Value As String) As String
    ''' <summary>
    ''' Perform the Inverses conversion i.e 
    ''' If y = converter.Convert(x) Then
    '''     x = converter.Inverse(y)
    ''' End If
    ''' </summary>
    ''' <param name="Value">The value</param>
    ''' <returns>The inverse of dValue</returns>
    Function Inverse(ByVal Value As String) As String
End Interface
#End Region

#Region "MxVConverter"
''' <summary>
''' Implements the simple 'y = m(x) + b' conversion and its inverse 'x = (y - b) / m'
''' </summary>
Public Class ExpressionMxBConverter
    Implements IExpressionUnitConverter
    Private ReadOnly Slope As String = "1.0"
    Private ReadOnly Offset As String = "0.0"

    Public Sub New(ByVal slope As String, ByVal offset As String)
        Me.Slope = slope
        Me.Offset = offset
    End Sub

    Public Function Convert(ByVal value As String) As String Implements IExpressionUnitConverter.Convert
        If Slope = "1.0" And Offset = "0" Then
            Return String.Format("({0})", value)
        ElseIf Offset = "0" Or Offset = "0.0" Then
            Return String.Format("(({0})×({1}))", value, Slope)
        End If
        Return String.Format("((({0})×({1}))+({2}))", value, Slope, Offset)
    End Function
    Public Function Inverse(ByVal value As String) As String Implements IExpressionUnitConverter.Inverse
        If Slope = "1.0" And Offset = "0" Then
            Return String.Format("({0})", value)
        ElseIf Offset = "0" Or Offset = "0.0" Then
            Return String.Format("(({0})/({1}))", value, Slope)
        End If
        Return String.Format("((({0})-({1}))/({2}))", value, Offset, Slope)
    End Function
End Class
#End Region

#Region "GageToAbsolutePressureConverter"
Public Class ExpressionGageVsAbsolutePressureConverter
    Implements IExpressionUnitConverter
    Private ReadOnly MxBConverter As ExpressionMxBConverter
    Private ReadOnly itsConversionInfoCallBack As GetConversionInfoDelegate
    Private absolutePressureList As HashSet(Of String)
    Private ReadOnly ProcessUnitConversionCallBack As ProcessUnitConversionDelegate
    Private Y As String
    Private X As String


    Public Sub New(ByVal conversionPair As Pair(Of String, String), ByVal getConversionInfoCallBack As GetConversionInfoDelegate, ByVal getProcessUnitConversionCallBack As ProcessUnitConversionDelegate, ByVal slope As String, ByVal offset As String)
        Me.itsConversionInfoCallBack = getConversionInfoCallBack
        Me.MxBConverter = New ExpressionMxBConverter(slope, offset)
        Me.ProcessUnitConversionCallBack = getProcessUnitConversionCallBack
        Me.X = conversionPair.First
        Me.Y = conversionPair.Second
    End Sub


    Public Function Convert(ByVal value As String) As String Implements IExpressionUnitConverter.Convert
        Dim baro As String = ""
        Dim baroUnit As String = ""
        Dim newExpression As String = ""

        Me.itsConversionInfoCallBack.Invoke(baro, baroUnit, absolutePressureList)

        If IsAbsolutePressure(Y) Then 'Gage To Absolute conversion
            If IsSamePressureType(baroUnit, X) Then
                Return String.Format("{0}", MxBConverter.Convert(String.Format("{0}+{1}", value, baro)))
            ElseIf IsSamePressureType(baroUnit, Y) Then
                Return String.Format("({0}+{1})", MxBConverter.Convert(value), baro)
            Else
                ProcessUnitConversionCallBack(baro, baroUnit, Y, newExpression)
                Return String.Format("({0}+{1})", MxBConverter.Convert(value), newExpression)
            End If
        Else 'Absolute To Gage conversion
            If IsSamePressureType(baroUnit, X) Then
                Return String.Format("{0}", MxBConverter.Convert(String.Format("{0}-{1}", value, baro)))
            ElseIf IsSamePressureType(baroUnit, Y) Then
                Return String.Format("({0}-{1})", MxBConverter.Convert(value), baro)
            Else
                ProcessUnitConversionCallBack(baro, baroUnit, X, newExpression)
                Return String.Format("{0}", MxBConverter.Convert(String.Format("{0}-{1}", value, newExpression)))
            End If
        End If

    End Function
    Public Function Inverse(ByVal value As String) As String Implements IExpressionUnitConverter.Inverse
        Dim baro As String = ""
        Dim baroUnit As String = ""
        Dim newExpression As String = ""

        Me.itsConversionInfoCallBack.Invoke(baro, baroUnit, absolutePressureList)

        If IsAbsolutePressure(X) Then 'Gage To Absolute conversion
            If IsSamePressureType(baroUnit, Y) Then
                Return String.Format("{0}", MxBConverter.Inverse(String.Format("{0}+{1}", value, baro)))
            ElseIf IsSamePressureType(baroUnit, X) Then
                Return String.Format("({0}+{1})", MxBConverter.Inverse(value), baro)
            Else
                ProcessUnitConversionCallBack(baro, baroUnit, X, newExpression)
                Return String.Format("({0}+{1})", MxBConverter.Inverse(value), newExpression)
            End If
        Else 'Absolute To Gage conversion
            If IsSamePressureType(baroUnit, Y) Then
                Return String.Format("{0}", MxBConverter.Inverse(String.Format("{0}-{1}", value, baro)))
            ElseIf IsSamePressureType(baroUnit, X) Then
                Return String.Format("({0}-{1})", MxBConverter.Inverse(value), baro)
            Else
                ProcessUnitConversionCallBack(baro, baroUnit, Y, newExpression)
                Return String.Format("{0}", MxBConverter.Inverse(String.Format("{0}-{1}", value, newExpression)))
            End If
        End If

    End Function
    'Public Function Convert(ByVal value As String) As String Implements IExpressionUnitConverter.Convert
    '    Dim baro As String = ""
    '    Dim baroUnit As String = ""
    '    Dim newExpression As String = ""

    '    Me.itsConversionInfoCallBack.Invoke(baro, baroUnit, absolutePressureList)

    '    If IsSamePressureType(baroUnit, X) Then
    '        Return String.Format("{0}", MxBConverter.Convert(String.Format("{0}+{1}", value, baro)))
    '    ElseIf IsSamePressureType(baroUnit, Y) Then
    '        Return String.Format("({0}+{1})", MxBConverter.Convert(value), baro)
    '    Else
    '        'ProcessUnitConversionCallBack(baro, baroUnit, Y, newExpression)
    '        'Return String.Format("({0}+{1})", MxBConverter.Convert(value), newExpression)
    '        If IsAbsolutePressure(Y) Then
    '            ProcessUnitConversionCallBack(baro, baroUnit, Y, newExpression)
    '            Return String.Format("({0}+{1})", MxBConverter.Convert(value), newExpression)
    '        ElseIf IsAbsolutePressure(X) Then
    '            ProcessUnitConversionCallBack(baro, baroUnit, X, newExpression)
    '            Return String.Format("{0}", MxBConverter.Convert(String.Format("{0}+{1}", value, newExpression)))
    '        Else
    '            Throw New Exception("Invalid Conversion Convert")
    '        End If
    '    End If

    'End Function
    'Public Function Inverse(ByVal value As String) As String Implements IExpressionUnitConverter.Inverse
    '    Dim baro As String = ""
    '    Dim baroUnit As String = ""
    '    Dim newExpression As String = ""

    '    Me.itsConversionInfoCallBack.Invoke(baro, baroUnit, absolutePressureList)

    '    If IsSamePressureType(baroUnit, Y) Then
    '        Return String.Format("{0}", MxBConverter.Inverse(String.Format("{0}-{1}", value, baro)))
    '    ElseIf IsSamePressureType(baroUnit, X) Then
    '        Return String.Format("({0}-{1})", MxBConverter.Inverse(value), baro)
    '    Else
    '        If IsAbsolutePressure(X) Then
    '            ProcessUnitConversionCallBack(baro, baroUnit, X, newExpression)
    '            Return String.Format("({0}-{1})", MxBConverter.Inverse(value), newExpression)
    '        ElseIf IsAbsolutePressure(Y) Then
    '            ProcessUnitConversionCallBack(baro, baroUnit, Y, newExpression)
    '            Return String.Format("{0}", MxBConverter.Inverse(String.Format("{0}-{1}", value, newExpression)))
    '        Else
    '            Throw New Exception("Invalid Conversion Inverse")
    '        End If
    '    End If

    'End Function

    Private Function IsAbsolutePressure(ByVal unit As String) As Boolean
        Return absolutePressureList.Contains(unit)
    End Function
    Private Function IsSamePressureType(ByVal first As String, ByVal second As String) As Boolean
        If first.StartsWith("PSI") And second.StartsWith("PSI") Then Return True
        If first.StartsWith("BAR") And second.StartsWith("BAR") Then Return True
        If first.StartsWith("MBAR") And second.StartsWith("MBAR") Then Return True
        If first.StartsWith("KPA") And second.StartsWith("KPA") Then Return True
        If first.StartsWith("INHG") And second.StartsWith("INHG") Then Return True
        If first.StartsWith("INH2O") And second.StartsWith("INH2O") Then Return True

        Return False

    End Function
End Class

#End Region





#End Region

#Region "Pair"
Public Structure Pair(Of T, U)
    Implements IEquatable(Of Pair(Of T, U))
    Private ReadOnly itsFirst As T
    Private ReadOnly itsSecond As U

    Public Sub New(ByVal first As T, ByVal second As U)
        Me.itsFirst = first
        Me.itsSecond = second
    End Sub

    Public ReadOnly Property First() As T
        Get
            Return itsFirst
        End Get
    End Property

    Public ReadOnly Property Second() As U
        Get
            Return itsSecond
        End Get
    End Property

    Public Overrides Function GetHashCode() As Integer
        Return First.GetHashCode() Or Second.GetHashCode()
    End Function

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim bEquals As Boolean = False
        If obj Is Nothing OrElse Not Object.ReferenceEquals(Me.GetType(), obj.GetType()) Then
            bEquals = False
        Else
            bEquals = Me.Equals(DirectCast(obj, Pair(Of T, U)))
        End If
        Return bEquals
    End Function

    Public Overloads Function Equals(ByVal other As Pair(Of T, U)) As Boolean _
    Implements IEquatable(Of Pair(Of T, U)).Equals
        Dim bEquals As Boolean

        bEquals = other.itsFirst.Equals(itsFirst) And other.itsSecond.Equals(itsSecond)

        Return bEquals
    End Function
End Structure
#End Region


#Region "Delegates"
Public Delegate Sub GetParameterValueDelegate(ByRef baroValue As String, ByRef baroUnit As String)
' Public Delegate Sub GetConversionInfoDelegate(ByRef baroName As String, ByRef baroUnit As String, ByRef fromunit As String, ByRef toUnit As String, ByRef absPressureList As HashSet(Of String))
Public Delegate Sub GetConversionInfoDelegate(ByRef baroName As String, ByRef baroUnit As String, ByRef absPressureList As HashSet(Of String))

Public Delegate Function ProcessUnitConversionDelegate(ByVal channelName As String, ByVal fromUnit As String, ByVal toUnit As String, ByRef outputExpression As String) As Boolean
#End Region


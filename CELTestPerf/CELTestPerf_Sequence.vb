Option Strict On

Imports Microsoft.VisualBasic
Imports KineticaRT
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Threading
Imports System.Xml
Imports KineticaRT.Studio

Imports KineticaRT.Studio.Application


Namespace CELTestPerf

    Public Class CELTestPerf_Sequence
        Inherits KineticaRT.Sequence.Sequence

#Region "Constants"
        Private Const BAR_TO_INHG As Double = 29.52999
        Private Const BAR_TO_INH20 As Double = 401.859807188
        Private Const BAR_TO_PSI As Double = 14.503773773
        Private Const INH20_TO_BAR As Double = 0.00248843
        Private Const INH20_TO_INHG As Double = 0.073690866
        Private Const CHANNELMAPCOLLECTIONNAME As String = "ChannelMap"
        Private Const DELTA_SG_PER_DEGF As Double = -0.0004
        Private Const GTOMS2 As Double = 9.80665
        Private Const HOURTOMINUTES As Double = 60.0
        Private Const KGTOLBS As Double = 2.204622622
        Private Const PPHTOKGPH As Double = 1.0 / KGTOLBS
        Private Const PSITOBAR As Double = 0.0689475729
        Private Const PSITOINH2O As Double = 27.680672603
        Private Const PSITOINHG As Double = 2.03602046
        Private Const PSITOMBAR As Double = 68.9475729
        Private Const USGALLONTOLITERS As Double = 3.785411784
        Private Const PERFDATADIRECTORY As String = "C:\My Projects\CELDAS 200\CELTestPerf\Data"
        Private Const DATABASEFILENAME As String = "TestSession.mdb"
        Private Const DATABASESECURENAME As String = "Secured.mdw"
        Private Const DATABASEUSER As String = "Administrator"
        Private Const DATABASEPSWD As String = "CEL9994"
#End Region 'Constants

#Region "Delegates"
        Public Delegate Function GetParameterValueDelegate(ByRef value As Double, ByRef units As String) As Boolean
        Public Delegate Function SetParameterValueDelegate(ByVal value As Double) As Boolean
#End Region

#Region "Internal Classes"

#Region "UnitConverter"

#Region "IUnitConverter"
        ''' <summary>
        ''' Interface definition for conversion between units
        ''' and its corresponding inverse conversion
        ''' </summary>
        Public Interface IUnitConverter
            ''' <summary>
            ''' Converts the specified value from a unit to another unit.
            ''' </summary>
            ''' <param name="dValue">The value to convert from.</param>
            ''' <returns>The converted value</returns>
            Function Convert(ByVal dValue As Double) As Double
            ''' <summary>
            ''' Perform the Inverses conversion i.e 
            ''' If y = converter.Convert(x) Then
            '''     x = converter.Inverse(y)
            ''' End If
            ''' </summary>
            ''' <param name="dValue">The value</param>
            ''' <returns>The inverse of dValue</returns>
            Function Inverse(ByVal dValue As Double) As Double
        End Interface
#End Region

        Public Class UnitConverterMgr
            Private Shared ReadOnly m_theInstance As New UnitConverterMgr
            Private itsConversionTable As Dictionary(Of Pair(Of String, String), IUnitConverter) = _
               New Dictionary(Of Pair(Of String, String), IUnitConverter)
            Private itsBaroChannel As KineticaRT.Channels.INumericChannel

            Private Sub New()
                Dim conversionPair As Pair(Of String, String)
                itsConversionTable.Add(New Pair(Of String, String)("BARA", "PSIA"), New MxBConverter(1.0 / PSITOBAR, 0))
                itsConversionTable.Add(New Pair(Of String, String)("BARA", "INHGA"), New MxBConverter(1.0 / BAR_TO_INHG, 0))
                itsConversionTable.Add(New Pair(Of String, String)("BARG", "INH20G"), New MxBConverter(1.0 / BAR_TO_INH20, 0))
                itsConversionTable.Add(New Pair(Of String, String)("BARD", "INH20D"), New MxBConverter(1.0 / BAR_TO_INH20, 0))

                conversionPair = New Pair(Of String, String)("PSIG", "BARA")
                itsConversionTable.Add(conversionPair, New GageToAbsolutePressureConverter(conversionPair, AddressOf Me.GetBaroValue, PSITOBAR, 0))
                itsConversionTable.Add(New Pair(Of String, String)("BARD", "PSID"), New MxBConverter(1.0 / PSITOBAR, 0))

                conversionPair = New Pair(Of String, String)("BARG", "PSIA")
                itsConversionTable.Add(conversionPair, New GageToAbsolutePressureConverter(conversionPair, AddressOf Me.GetBaroValue, 1.0 / PSITOBAR, 0))

                conversionPair = New Pair(Of String, String)("INH20G", "INHGA")
                itsConversionTable.Add(conversionPair, New GageToAbsolutePressureConverter(conversionPair, AddressOf Me.GetBaroValue, INH20_TO_INHG, 0))
                itsConversionTable.Add(New Pair(Of String, String)("BARG", "PSIG"), New MxBConverter(1.0 / PSITOBAR, 0))
                itsConversionTable.Add(New Pair(Of String, String)("DEGC", "DEGF"), New MxBConverter(9.0 / 5.0, 32.0))
                itsConversionTable.Add(New Pair(Of String, String)("DEGC", "DEGR"), New MxBConverter(9.0 / 5.0, 491.67))
                itsConversionTable.Add(New Pair(Of String, String)("DEGF", "DEGR"), New MxBConverter(1.0, 459.67))
                itsConversionTable.Add(New Pair(Of String, String)("G_RMS", "MPS_SQ_RMS"), New MxBConverter(GTOMS2, 0))
                '
                ' TODO
                ' On doit statuer et documenter une fois pour toute le facteur de conversion a
                ' utiliser pour INH2O puisque celui-ci de façon général dépend de la température
                ' Standard utilisé dans l'industrie Aerospatial ?
                '
                conversionPair = New Pair(Of String, String)("INH2O", "PSIA")
                itsConversionTable.Add(conversionPair, New GageToAbsolutePressureConverter(conversionPair, AddressOf Me.GetBaroValue, 1.0 / PSITOINH2O, 0))

                conversionPair = New Pair(Of String, String)("INH20G", "BARA")
                itsConversionTable.Add(conversionPair, New GageToAbsolutePressureConverter(conversionPair, AddressOf Me.GetBaroValue, INH20_TO_BAR, 0))

                conversionPair = New Pair(Of String, String)("INHG", "PSIA")
                itsConversionTable.Add(conversionPair, New GageToAbsolutePressureConverter(conversionPair, AddressOf Me.GetBaroValue, 1.0 / PSITOINHG, 0))
                itsConversionTable.Add(New Pair(Of String, String)("INHGA", "PSIA"), New MxBConverter(1.0 / PSITOINHG, 0))
                itsConversionTable.Add(New Pair(Of String, String)("KW", "W"), New MxBConverter(1000, 0))
                itsConversionTable.Add(New Pair(Of String, String)("LPH", "LPM"), New MxBConverter(1.0 / HOURTOMINUTES, 0))
                itsConversionTable.Add(New Pair(Of String, String)("PPH", "KGPH"), New MxBConverter(PPHTOKGPH, 0))

                conversionPair = New Pair(Of String, String)("MBAR", "PSIA")
                itsConversionTable.Add(conversionPair, New GageToAbsolutePressureConverter(conversionPair, AddressOf Me.GetBaroValue, 1.0 / PSITOMBAR, 0))
                itsConversionTable.Add(New Pair(Of String, String)("MBARA", "PSIA"), New MxBConverter(1.0 / PSITOMBAR, 0))
                itsConversionTable.Add(New Pair(Of String, String)("PSID", "INH2O"), New MxBConverter(PSITOINH2O, 0))
                itsConversionTable.Add(New Pair(Of String, String)("PSID", "INHG"), New MxBConverter(PSITOINHG, 0))
                itsConversionTable.Add(New Pair(Of String, String)("PSIG", "INH2O"), New MxBConverter(PSITOINH2O, 0))
                itsConversionTable.Add(New Pair(Of String, String)("PSIG", "INHG"), New MxBConverter(PSITOINHG, 0))

                conversionPair = New Pair(Of String, String)("PSIG", "PSIA")
                itsConversionTable.Add(conversionPair, New GageToAbsolutePressureConverter(conversionPair, AddressOf Me.GetBaroValue, 1, 0))
                itsConversionTable.Add(New Pair(Of String, String)("RPM", "HZ"), New MxBConverter(1.0, 0.0))
                itsConversionTable.Add(New Pair(Of String, String)("USGPH", "LPH"), New MxBConverter(USGALLONTOLITERS, 0))
                itsConversionTable.Add(New Pair(Of String, String)("USGPH", "LPM"), New MxBConverter(USGALLONTOLITERS / HOURTOMINUTES, 0))
                itsConversionTable.Add(New Pair(Of String, String)("USGPH", "USGPM"), New MxBConverter(1.0 / HOURTOMINUTES, 0))
                itsConversionTable.Add(New Pair(Of String, String)("USGPM", "LPH"), New MxBConverter(USGALLONTOLITERS * HOURTOMINUTES, 0))
                itsConversionTable.Add(New Pair(Of String, String)("USGPM", "LPM"), New MxBConverter(USGALLONTOLITERS, 0))
            End Sub
            Private Function GetBaroValue(ByRef baro As Double, ByRef units As String) As Boolean
                Dim bError As Boolean = False
                Dim tmpBaro As Double = Double.NaN

                If Me.itsBaroChannel IsNot Nothing Then
                    baro = itsBaroChannel.Value.ToDouble()
                    units = itsBaroChannel.Units
                End If

                Return bError
            End Function

            Public Shared ReadOnly Property Instance As UnitConverterMgr
                Get
                    Return m_theInstance
                End Get
            End Property

            Public Sub SetBaroChannel(baroChannel As KineticaRT.Channels.INumericChannel)
                itsBaroChannel = baroChannel
            End Sub

            Public Function ProcessUnitConversion(ByVal value As Double, ByVal sFromUnits As String, ByRef toValue As Double, ByVal sToUnits As String) As Boolean
                Dim bError As Boolean = False
                Dim cF As IUnitConverter

                toValue = value
                If sFromUnits <> sToUnits Then
                    If itsConversionTable.ContainsKey(New Pair(Of String, String)(sFromUnits, sToUnits)) Then
                        cF = itsConversionTable(New Pair(Of String, String)(sFromUnits, sToUnits))
                        toValue = cF.Convert(value)
                    ElseIf itsConversionTable.ContainsKey(New Pair(Of String, String)(sToUnits, sFromUnits)) Then
                        cF = itsConversionTable(New Pair(Of String, String)(sToUnits, sFromUnits))
                        toValue = cF.Inverse(value)
                    Else
                        MsgBox("Can't perform conversion from " & sFromUnits & " to " & sToUnits)
                        bError = True
                    End If
                End If

                Return bError
            End Function
        End Class

#Region "MxVConverter"
        ''' <summary>
        ''' Implements the simple 'y = m(x) + b' conversion and its inverse 'x = (y - b) / m'
        ''' </summary>
        Private Class MxBConverter
            Implements IUnitConverter
            Private ReadOnly itsSlope As Double = 1.0
            Private ReadOnly itsOffset As Double = 0.0

            Public Sub New(ByVal slope As Double, ByVal offset As Double)
                Me.itsSlope = slope
                Me.itsOffset = offset
            End Sub

            Public Function Convert(ByVal dValue As Double) As Double Implements IUnitConverter.Convert
                Return (dValue * itsSlope + itsOffset)
            End Function

            Public Function Inverse(ByVal dValue As Double) As Double Implements IUnitConverter.Inverse
                Return (dValue - itsOffset) / itsSlope
            End Function
        End Class
#End Region

#Region "GageToAbsolutePressureConverter"
        Private Class GageToAbsolutePressureConverter
            Implements IUnitConverter
            Private ReadOnly itsMxBConverter As MxBConverter
            Private ReadOnly itsGetBaroValueHandler As GetParameterValueDelegate
            Private ReadOnly itsFromUnit As String
            Private ReadOnly itsToUnit As String

            Public Sub New(ByVal conversionPair As Pair(Of String, String), ByVal getBaroValueHandler As GetParameterValueDelegate, ByVal slope As Double, ByVal offset As Double)
                Me.itsGetBaroValueHandler = getBaroValueHandler
                Me.itsMxBConverter = New MxBConverter(slope, offset)
                Me.itsFromUnit = conversionPair.First
                Me.itsToUnit = conversionPair.Second
            End Sub

            Public Function Convert(ByVal dValue As Double) As Double Implements IUnitConverter.Convert
                Dim baro As Double
                Dim baroUnit As String = Nothing

                Me.itsGetBaroValueHandler.Invoke(baro, baroUnit)
                Return itsMxBConverter.Convert(dValue) + Me.GetBaro(itsToUnit)
            End Function

            Public Function Inverse(ByVal dValue As Double) As Double Implements IUnitConverter.Inverse
                Dim baro As Double
                Dim baroUnit As String = Nothing

                Me.itsGetBaroValueHandler.Invoke(baro, baroUnit)
                Return itsMxBConverter.Inverse(dValue - Me.GetBaro(itsToUnit))
            End Function

            Private Function GetBaro(ByVal requiredUnit As String) As Double
                Dim baroOriginalValue As Double
                Dim baroOriginalUnit As String = String.Empty
                Dim baro As Double = Double.NaN

                Me.itsGetBaroValueHandler(baroOriginalValue, baroOriginalUnit)
                UnitConverterMgr.Instance.ProcessUnitConversion(baroOriginalValue, baroOriginalUnit, baro, requiredUnit)
                Return baro
            End Function
        End Class
#End Region

#End Region

#Region "Pair"
        Private Structure Pair(Of T, U)
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

#Region "KineticaRTParameterValueDelegator"
        Class KineticaRTParameterValueDelegator
            Private itsChannel As KineticaRT.Channels.INumericChannel

            Public Sub New(ByVal channel As KineticaRT.Channels.INumericChannel)
                itsChannel = channel
            End Sub

            Public Function GetValue(ByRef value As Double, ByRef units As String) As Boolean
                Dim bError As Boolean = False
                value = Double.NaN
                If itsChannel IsNot Nothing Then
                    value = itsChannel.Value.ToDouble()
                    units = itsChannel.Units
                    System.Diagnostics.Trace.WriteLine(itsChannel.Name & " value is: " & value.ToString & " " & units)
                Else
                    bError = True
                End If
                Return bError
            End Function

            Public Function SetValue(ByVal value As Double) As Boolean
                If itsChannel IsNot Nothing Then
                    itsChannel.Value = New KineticaRT.Values.Double(value)
                    System.Diagnostics.Trace.WriteLine(itsChannel.Name & " set value to: " & value.ToString)
                End If
            End Function
        End Class
#End Region

#Region "Turbine Flow Meter Processing"

#Region "TurbineFlowMeter"

        Structure TurbineFlowMeter
            Private ReadOnly itsItems(,) As Double
            Private ReadOnly itsFromUnit As String
            Private ReadOnly itsToUnit As String
            Public Sub New(ByVal fromUnit As String, ByVal toUnit As String, ByRef items(,) As Double)
                itsFromUnit = fromUnit
                itsToUnit = toUnit
                itsItems = items
            End Sub
            ReadOnly Property Items() As Double(,)
                Get
                    Return itsItems
                End Get
            End Property
            ReadOnly Property FromUnits() As String
                Get
                    Return itsFromUnit
                End Get
            End Property
            ReadOnly Property ToUnits() As String
                Get
                    Return itsToUnit
                End Get
            End Property

        End Structure

#End Region

#Region "TurbineFlowMeterProcessor"


        Class TurbineFlowMeterProcessor

            Private itsCharacterizer As LinearCharacterizer
            Private itsCharacterizerInputUnits As String
            Private itsGetSGSampleValue As GetParameterValueDelegate
            Private itsGetTFuelValue As GetParameterValueDelegate
            Private itsGetTSGSampleValue As GetParameterValueDelegate
            Private itsSetOutputValue As SetParameterValueDelegate
            Private itsUnits As String
            Private itsUnitConversionHandler As UnitConverter

            Public Delegate Function UnitConverter(ByVal fromValue As Double, ByVal fromUnit As String, _
                ByRef toValue As Double, ByVal toUnit As String) As Boolean

            Public Sub New(ByVal units As String, _
                    ByVal unitConversionDelegate As UnitConverter, _
                    ByVal getSGSampleValueDelegate As GetParameterValueDelegate, _
                    ByVal getTSGSampleValueDelegate As GetParameterValueDelegate, _
                    ByVal getTFuelValueDelegate As GetParameterValueDelegate, _
                    ByVal characterizer As LinearCharacterizer, _
                    ByVal characterizerInputUnits As String, _
                    ByVal setOutputValueDelegate As SetParameterValueDelegate)
                itsGetSGSampleValue = getSGSampleValueDelegate
                itsGetTSGSampleValue = getTSGSampleValueDelegate
                itsGetTFuelValue = getTFuelValueDelegate
                itsCharacterizer = characterizer
                itsCharacterizerInputUnits = characterizerInputUnits
                itsSetOutputValue = setOutputValueDelegate
                itsUnitConversionHandler = unitConversionDelegate
            End Sub

            Private Function GetUnits(ByVal parameter As XmlNode, ByRef units As String) As Boolean
                Dim bError As Boolean = False
                Dim tmpNode As XmlNode

                tmpNode = parameter("Units")
                If tmpNode IsNot Nothing Then
                    units = tmpNode.InnerText
                Else
                    MsgBox("Missing <Units> for node " & parameter.InnerXml)
                    bError = True
                End If

                Return bError
            End Function

            Private Function GetDensity(ByRef densityKgpL As Double) As Boolean
                Dim bError As Boolean = False
                Dim SGSample As Double
                Dim TSGSampleDegF As Double
                Dim TFuelDegF As Double

                densityKgpL = 1.0

                bError = GetSGSample(SGSample)
                If Not bError Then
                    bError = GetTSGSample(TSGSampleDegF)
                End If
                If Not bError Then
                    bError = GetTFuel(TFuelDegF)
                End If
                If Not bError Then
                    densityKgpL = SGSample + ((TFuelDegF - TSGSampleDegF) * DELTA_SG_PER_DEGF)
                End If

                Return bError
            End Function

            Private Function GetSGSample(ByRef SGSample As Double) As Boolean
                Dim bError As Boolean = False
                Dim dummy As String = String.Empty

                SGSample = Double.NaN

                If Me.itsGetSGSampleValue IsNot Nothing Then
                    bError = Me.itsGetSGSampleValue.Invoke(SGSample, dummy)
                End If

                Return bError
            End Function

            Private Function GetTFuel(ByRef TFuelDegF As Double) As Boolean
                Dim bError As Boolean = False
                Dim units As String = String.Empty
                Dim value As Double
                TFuelDegF = Double.NaN

                If Me.itsGetTFuelValue IsNot Nothing Then
                    bError = Me.itsGetTFuelValue.Invoke(value, units)
                End If
                If Not bError Then
                    bError = Me.itsUnitConversionHandler.Invoke(value, units, TFuelDegF, "DEGF")
                End If
                Return bError
            End Function
            Private Function GetTSGSample(ByRef TSGSampleDegF As Double) As Boolean
                Dim bError As Boolean = False
                Dim units As String = String.Empty
                Dim value As Double

                TSGSampleDegF = Double.NaN

                If Me.itsGetTSGSampleValue IsNot Nothing Then
                    bError = Me.itsGetTSGSampleValue.Invoke(value, units)
                End If
                If Not bError Then
                    bError = Me.itsUnitConversionHandler(value, units, TSGSampleDegF, "DEGF")
                End If
                Return bError
            End Function

            Private Function GetValue(ByVal parameter As XmlNode, ByRef value As Double) As Boolean
                Dim bError As Boolean = False
                Dim tmpNode As XmlNode

                tmpNode = parameter("Value")
                If tmpNode IsNot Nothing Then
                    If Not Double.TryParse(tmpNode.InnerText, value) Then
                        MsgBox("Error parsing value for node " & parameter.InnerXml)
                        bError = True
                    End If
                Else
                    MsgBox("Missing <Value> for node " & parameter.InnerXml)
                    bError = True
                End If

                Return bError
            End Function

            ''' <summary>
            ''' Gets the volumetric flow in lph from the Mass Flow in Kgph.
            ''' </summary>
            ''' <param name="massFlow">The mass flow in kilogram per hour.</param>
            ''' <param name="volumetricFlow">The volumetric flow in liter per hour.</param>
            ''' <returns></returns>
            Private Function GetVolumetricFlow(ByVal massFlowKgph As Double, ByRef volumetricFlowLph As Double) As Boolean
                Dim bError As Boolean = False
                Dim densityKgpL As Double

                bError = GetDensity(densityKgpL)
                If Not bError Then
                    volumetricFlowLph = massFlowKgph / densityKgpL
                End If
                Return bError
            End Function

            Public Function Process(ByVal parameter As XmlNode) As Boolean
                Dim bError As Boolean = False
                Dim massFlow, massFlowKgph, volumetricFlowLph As Double
                Dim fromUnits As String = String.Empty

                bError = GetValue(parameter, massFlow)
                If Not bError Then
                    bError = GetUnits(parameter, fromUnits)
                End If
                If Not bError Then
                    bError = Me.itsUnitConversionHandler.Invoke(massFlow, fromUnits, massFlowKgph, "KGPH")
                End If
                If Not bError Then
                    bError = GetVolumetricFlow(massFlowKgph, volumetricFlowLph)
                End If
                If Not bError Then
                    Dim value As Double
                    Dim volumetricFlow As Double
                    bError = Me.itsUnitConversionHandler.Invoke(volumetricFlowLph, "LPH", _
                        volumetricFlow, Me.itsCharacterizerInputUnits)
                    If Not bError Then
                        value = Me.itsCharacterizer.Interpolate(volumetricFlow)
                        Me.itsSetOutputValue(value)
                    End If
                End If
            End Function
        End Class

#End Region

#Region "Characterizer"

        Public Interface ICharacterizer
            Function Interpolate(ByVal input As Double) As Double
        End Interface

        Public Class LinearCharacterizer
            Implements ICharacterizer

            Private itsTable(,) As Double
            Private firstIndex, lastIndex, stepIndex As Integer

            Public Sub New(ByVal theTable(,) As Double)
                itsTable = theTable
                If itsTable.GetUpperBound(0) > 0 Then
                    If itsTable(0, 0) <= itsTable(itsTable.GetUpperBound(0), 0) Then
                        firstIndex = 0
                        lastIndex = itsTable.GetUpperBound(0)
                        stepIndex = 1
                    Else
                        firstIndex = itsTable.GetUpperBound(0)
                        lastIndex = 0
                        stepIndex = -1
                    End If
                End If
            End Sub

            Private Function DoInterpolation(ByVal input As Double) As Double
                Dim index As Integer
                Dim value As Double

                If FindInterval(input, index) Then
                    Dim m, b As Double

                    m = (itsTable(index, 1) - itsTable(index - stepIndex, 1)) / _
                        (itsTable(index, 0) - itsTable(index - stepIndex, 0))
                    b = itsTable(index - 1, 1)
                    value = (input - itsTable(index - stepIndex, 0)) * m + b
                End If
                Return value
            End Function

            Public Function Interpolate(ByVal input As Double) As Double Implements ICharacterizer.Interpolate
                Dim value As Double = Double.NaN

                If itsTable.GetUpperBound(0) > 0 Then
                    If WithinBounds(input) Then
                        value = Me.DoInterpolation(input)
                    End If
                End If
                Return value
            End Function

            Private Function FindInterval(ByVal input As Double, ByRef index As Integer) As Boolean
                Dim bFound As Boolean = False

                For index = firstIndex + stepIndex To lastIndex Step stepIndex
                    If input >= itsTable(index - stepIndex, 0) And input <= itsTable(index, 0) Then
                        bFound = True
                        Exit For
                    End If
                Next
                Return bFound
            End Function

            Private Function WithinBounds(ByVal input As Double) As Boolean
                Dim result As Boolean = False

                If input >= itsTable(firstIndex, 0) AndAlso input <= itsTable(lastIndex, 0) Then
                    result = True
                End If

                Return result
            End Function
        End Class

#End Region

#End Region

#End Region

#Region "Fields"
        Private itsConditionsCB As System.Windows.Forms.ComboBox
        Private itsConditionsXmlNodes As System.Collections.Generic.List(Of XmlNode) = New System.Collections.Generic.List(Of XmlNode)
        Private itsDataFileCB As System.Windows.Forms.ComboBox
        Private itsPerformanceXmlDoc As XmlDocument
        Private itsEngineModelCh As KineticaRT.Channels.IChannel
        Private itsEngineSNCh As KineticaRT.Channels.IChannel
        Private itsKineticaRTProject As KineticaRT.Studio.Application.Project
        Private itsPerformanceXmlNode As XmlNode
        Private itsTurbineFlowMeter(0) As TurbineFlowMeter
        Private itsTurbineFlowMeterProcessors As Dictionary(Of String, TurbineFlowMeterProcessor) = _
            New Dictionary(Of String, TurbineFlowMeterProcessor)
#End Region

#Region "Constructors"
        Public Sub New()
        End Sub
#End Region

#Region "Methods"

#Region "IConnectable"
        Public Overrides Sub Connect()
            Dim bError As Boolean = False

            itsKineticaRTProject = TryCast(GetObject("Project"), KineticaRT.Studio.Application.Project)
            If (itsKineticaRTProject Is Nothing) Then
                MsgBox("KineticaRT project is nothing")
                bError = True
            End If
            If Not bError Then
                itsDataFileCB = TryCast(GetObject("CELTestPerf.3"), System.Windows.Forms.ComboBox)
                If (itsDataFileCB Is Nothing) Then
                    MsgBox("dataFileCB is nothing")
                    bError = True
                End If
            End If
            If Not bError Then
                itsConditionsCB = TryCast(GetObject("CELTestPerf.4"), System.Windows.Forms.ComboBox)
                If (itsConditionsCB Is Nothing) Then
                    MsgBox("conditionsCB is nothing")
                    bError = True
                End If
            End If
            If Not bError Then
                itsEngineModelCh = GetChannel("EngineModel")
                If (itsEngineModelCh Is Nothing) Then
                    MsgBox("EngineModel channel is nothing")
                    bError = True
                Else
                    itsEngineModelCh.Value = KineticaRT.Values.Values.EmptyString
                End If
            End If
            If Not bError Then
                itsEngineSNCh = GetChannel("EngineSN")
                If (itsEngineSNCh Is Nothing) Then
                    MsgBox("EngineSN channel is nothing")
                    bError = True
                Else
                    itsEngineSNCh.Value = KineticaRT.Values.Values.EmptyString
                End If
            End If

            If Not bError Then
                FillDataFiles()
            End If

            If Not bError Then
                bError = ReadTurbineFlowMeters()
            End If
            If Not bError AndAlso (itsDataFileCB IsNot Nothing) AndAlso (itsConditionsCB IsNot Nothing) Then
                AddHandler itsDataFileCB.SelectedIndexChanged, AddressOf Me.OnDataFileCBIndexChanged
                AddHandler itsConditionsCB.SelectedIndexChanged, AddressOf Me.OnConditionsCBIndexChanged
            End If
            Dim mainWindow As KineticaRT.Studio.Window
            mainWindow = TryCast(GetObject("CELTestPerf"), KineticaRT.Studio.Window)
            mainWindow.Connect()
            mainWindow.Show()
        End Sub

        Public Overrides Sub Disconnect()
            If itsDataFileCB IsNot Nothing Then
                RemoveHandler itsDataFileCB.SelectedIndexChanged, AddressOf Me.OnDataFileCBIndexChanged
                itsDataFileCB.Items.Clear()
                itsDataFileCB = Nothing
            End If
            If itsConditionsCB IsNot Nothing Then
                RemoveHandler itsConditionsCB.SelectedIndexChanged, AddressOf Me.OnConditionsCBIndexChanged
                itsConditionsCB.Items.Clear()
                itsConditionsCB = Nothing
            End If
        End Sub
#End Region

        Private Sub FillDataFiles()
            itsDataFileCB.Items.Clear()

            If Not Directory.Exists(PERFDATADIRECTORY) Then
                MsgBox("No such directory " & PERFDATADIRECTORY)
            Else
                Dim di As New DirectoryInfo(PERFDATADIRECTORY)
                Dim fi As FileInfo() = di.GetFiles()
                For Each fiTemp As FileInfo In fi
                    itsDataFileCB.Items.Add(fiTemp.Name)
                Next
                For Each diTemp As DirectoryInfo In di.GetDirectories

                    'itsDataFileCB.Items.Add(diTemp.Name)
                    For Each fiTemp As FileInfo In diTemp.GetFiles
                        If fiTemp.Extension.ToLower = ".xml" Then
                            itsDataFileCB.Items.Add(diTemp.Name + "\" + fiTemp.Name)
                        End If
                    Next
                Next

            End If
        End Sub

        Private Function CreateLinkChannel(ByRef channel As XmlNode) As Channels.IChannel
            Dim theKineticaRTChannel As CEL.KineticaRT.Channels.NumericThresholdLink = _
                New CEL.KineticaRT.Channels.NumericThresholdLink

            For Each tmpNode As XmlNode In channel.ChildNodes
                Select Case tmpNode.Name
                    Case "FVName"
                        theKineticaRTChannel.Source = tmpNode.InnerText
                        theKineticaRTChannel.Destination = tmpNode.InnerText
                    Case "Parameter"
                        theKineticaRTChannel.Name = tmpNode.InnerText
                    Case "Name"
                        theKineticaRTChannel.Description = tmpNode.InnerText
                        If tmpNode.InnerText.ToUpper().Contains("BARO") Then
                            UnitConverterMgr.Instance.SetBaroChannel(theKineticaRTChannel)
                        End If
                    Case "Units"
                        theKineticaRTChannel.Units = tmpNode.InnerText
                End Select
            Next

            Return theKineticaRTChannel
        End Function

        Private Function CreateOpcChannel(ByRef channel As XmlNode) As Channels.IChannel
            Dim theKineticaRTChannel As Servers.OpcClient.Item = New Servers.OpcClient.Item

            theKineticaRTChannel.Group = "Group_Fast"
            For Each tmpNode As XmlNode In channel.ChildNodes
                Select Case tmpNode.Name
                    Case "FVName"
                        theKineticaRTChannel.Name = tmpNode.InnerText
                    Case "Name"
                        theKineticaRTChannel.Description = tmpNode.InnerText
#If 0 Then
                        If tmpNode.InnerText.ToUpper().Contains("BARO") Then
                            itsBaroChannel = theKineticaRTChannel
                        End If
#End If
                    Case "Units"
                        theKineticaRTChannel.Units = tmpNode.InnerText
                    Case "ItemID"
                        theKineticaRTChannel.ItemID = tmpNode.InnerText
                    Case "Slope"
                        Dim scale As Double
                        If Double.TryParse(tmpNode.InnerText, scale) Then
                            theKineticaRTChannel.Scale = scale
                        Else
                            MsgBox("Error parsing Slope for Channel " & channel("Parameter").InnerXml)
                        End If
                End Select
            Next

            Return theKineticaRTChannel
        End Function

        Private Function CreateOrReplaceChannelCollection(ByVal theCollectionName As String) As KineticaRT.ChannelCollection
            Dim theChannelsInfo As KineticaRT.Studio.Application.Project.ChannelsInfo
            Dim theChannelCollection As KineticaRT.ChannelCollection

            theChannelsInfo = itsKineticaRTProject.GetChannelsInfo(theCollectionName)
            If theChannelsInfo IsNot Nothing Then
                itsKineticaRTProject.RemoveChannels(theChannelsInfo)
            End If

            itsKineticaRTProject.AddChannelCollection("", theCollectionName, True)
            theChannelCollection = TryCast(GetObject(theCollectionName), KineticaRT.ChannelCollection)

            Return theChannelCollection
        End Function

        Private Function IsFuelFlowName(ByVal name As String) As Boolean
            Dim bResult As Boolean = False

            If name.StartsWith("WF") OrElse name.StartsWith("FUEL_FLOW") Then
                bResult = True
            End If
            Return bResult
        End Function

        Private Function ProcessChannelCreation(ByVal theChannelNode As XmlNode, ByVal theChannelCollection As KineticaRT.ChannelCollection) As Boolean
            Dim bError As Boolean = False
            Dim theOpcChannel As KineticaRT.Channels.IChannel
            Dim theLinkChannel As KineticaRT.Channels.IChannel

            theOpcChannel = CreateOpcChannel(theChannelNode)
            theLinkChannel = CreateLinkChannel(theChannelNode)
            If theOpcChannel Is Nothing OrElse theLinkChannel Is Nothing Then
                MsgBox("Failed to create channel " & theChannelNode.InnerXml & ". Skipping.")
            Else
                theChannelCollection.Add(TryCast(theOpcChannel, KineticaRT.Object))
                theChannelCollection.Add(TryCast(theLinkChannel, KineticaRT.Object))
            End If

            Return bError
        End Function

        Private Function ProcessChannelMap() As Boolean
            Dim bError As Boolean = False
            Dim theChannelMapNode As XmlNode = itsPerformanceXmlNode(CHANNELMAPCOLLECTIONNAME)
            Dim theChannelCollection As KineticaRT.ChannelCollection = Nothing

            UnitConverterMgr.Instance.SetBaroChannel(Nothing)
            itsTurbineFlowMeterProcessors.Clear()

            If theChannelMapNode Is Nothing Then
                MsgBox("Can't find 'ChannelMap' section, Invalid Perf Data xml file. Aborting")
                bError = True
            End If

            If Not bError Then
                theChannelCollection = CreateOrReplaceChannelCollection(CHANNELMAPCOLLECTIONNAME)
                If theChannelCollection Is Nothing Then
                    MsgBox("Could not create " & CHANNELMAPCOLLECTIONNAME & " channel collection. Arborting")
                    bError = True
                End If
            End If

            If Not bError Then
                For Each theNode As XmlNode In theChannelMapNode.ChildNodes
                    ProcessChannelMapNode(theNode, theChannelCollection)
                Next
                For Each theNode As XmlNode In theChannelMapNode.ChildNodes
                    ProcessTurbineFlowMeterNode(theNode, theChannelCollection)
                Next
                Dim theChannelsInfo As KineticaRT.Studio.Application.Project.ChannelsInfo

                theChannelsInfo = itsKineticaRTProject.GetChannelsInfo(CHANNELMAPCOLLECTIONNAME)
                If theChannelsInfo IsNot Nothing Then
                    itsKineticaRTProject.UpdateChannels(theChannelsInfo)
                    theChannelCollection.Connect()
                Else
                    MsgBox("Failed to update " & CHANNELMAPCOLLECTIONNAME & " channels info. Aborting")
                    bError = True
                End If
            End If

            Return bError
        End Function

        Private Sub ProcessChannelMapNode(ByVal theNode As XmlNode, ByVal theChannelCollection As KineticaRT.ChannelCollection)
            If "Channel" = theNode.Name Then
                ProcessChannelCreation(theNode, theChannelCollection)
            End If
        End Sub

        Private Function ProcessCondition(ByVal condition As XmlNode) As Boolean
            Dim bError As Boolean = False
            Dim kineticaRTChannel As KineticaRT.Channels.INumericChannel
            Dim parameterNameNode As XmlNode

            '
            ' Process depend parameters (TFuel, etc) before processing turbine flow meter
            '
            For Each parameter As XmlNode In condition("Parameters").ChildNodes
                If parameter.NodeType = XmlNodeType.Comment Then
                    Continue For
                End If
                parameterNameNode = parameter("Name")
                If parameterNameNode Is Nothing Then
                    MsgBox("Missing parameter name for node " & parameter.InnerXml)
                Else
                    If Not Me.IsFuelFlowName(parameterNameNode.InnerText) Then
                        kineticaRTChannel = TryCast(GetChannel(parameterNameNode.InnerText), KineticaRT.Channels.INumericChannel)
                        If kineticaRTChannel Is Nothing Then
                            MsgBox("KineticaRT channel " & parameterNameNode.InnerText & " doesn't exist" & vbCrLf & _
                            parameter.InnerXml)
                        Else
                            bError = ProcessConditionParameter(parameter, kineticaRTChannel)
                        End If
                    End If
                End If
            Next
            'Thread.Sleep(250)
            For Each parameter As XmlNode In condition("Parameters").ChildNodes
                If parameter.NodeType = XmlNodeType.Comment Then
                    Continue For
                End If
                parameterNameNode = parameter("Name")
                If parameterNameNode Is Nothing Then
                    MsgBox("Missing parameter name for node " & parameter.InnerXml)
                Else
                    If Me.IsFuelFlowName(parameterNameNode.InnerText) Then
                        Dim turbineFlowMeterProcessor As TurbineFlowMeterProcessor
                        If Me.itsTurbineFlowMeterProcessors.ContainsKey(parameterNameNode.InnerText) Then
                            turbineFlowMeterProcessor = Me.itsTurbineFlowMeterProcessors.Item(parameterNameNode.InnerText)
                            turbineFlowMeterProcessor.Process(parameter)
                        End If
                    End If
                End If
            Next

            Return bError
        End Function

        Private Function ProcessConditionParameter(ByVal parameter As XmlNode, ByVal channel As KineticaRT.Channels.INumericChannel) As Boolean
            Dim bError As Boolean = False
            Dim value As Double
            Dim bValue As Boolean
            Dim tmpNode As XmlNode

            tmpNode = parameter("Value")
            If tmpNode IsNot Nothing Then
                If Double.TryParse(tmpNode.InnerText, value) Then
                    tmpNode = parameter("Units")
                    If tmpNode IsNot Nothing Then
                        Dim sFromUnit, sToUnit As String

                        sFromUnit = tmpNode.InnerText.ToUpper
                        sToUnit = channel.Units.ToUpper
                        If sFromUnit <> sToUnit Then
                            UnitConverterMgr.Instance.ProcessUnitConversion(value, sFromUnit, value, sToUnit)
                        End If
                    End If
                    System.Diagnostics.Trace.WriteLine("Setting parameter: " & channel.Name & " to value: " & value.ToString())
                    channel.Value = New KineticaRT.Values.Double(value)
                ElseIf Boolean.TryParse(tmpNode.InnerText, bValue) Then
                    If bValue Then
                        channel.Value = KineticaRT.Values.Values.True
                    Else
                        channel.Value = KineticaRT.Values.Values.False
                    End If
                Else
                    MsgBox("Error parsing value for node " & parameter.InnerXml)
                    bError = True
                End If
            End If
            If Not bError Then
            End If

            Return bError
        End Function

        Private Function ProcessTurbineFlowMeterProcessorCreation(ByVal theNode As XmlNode, ByVal theChannelCollection As KineticaRT.ChannelCollection) As Boolean
            Dim bError As Boolean = False
            Dim SGChannel, TSGChannel, TFuelChannel, InputChannel As KineticaRT.Channels.INumericChannel
            Dim SGDelegator As KineticaRTParameterValueDelegator = Nothing
            Dim TSGDelegator As KineticaRTParameterValueDelegator = Nothing
            Dim TFuelDelegator As KineticaRTParameterValueDelegator = Nothing
            Dim InputDelegator As KineticaRTParameterValueDelegator = Nothing
            Dim name As String = Nothing
            Dim units As String = Nothing
            Dim tmpNode As XmlNode
            Dim flowMeterId As Integer = 0

            tmpNode = theNode("SGSampleChannel")
            If tmpNode IsNot Nothing Then
                SGChannel = TryCast(GetChannel(tmpNode.InnerText), KineticaRT.Channels.INumericChannel)
                SGDelegator = New KineticaRTParameterValueDelegator(SGChannel)
            End If
            tmpNode = theNode("TSGSampleChannel")
            If tmpNode IsNot Nothing Then
                TSGChannel = TryCast(GetChannel(tmpNode.InnerText), KineticaRT.Channels.INumericChannel)
                TSGDelegator = New KineticaRTParameterValueDelegator(TSGChannel)
            End If
            tmpNode = theNode("TFuelChannel")
            If tmpNode IsNot Nothing Then
                TFuelChannel = TryCast(GetChannel(tmpNode.InnerText), KineticaRT.Channels.INumericChannel)
                TFuelDelegator = New KineticaRTParameterValueDelegator(TFuelChannel)
            End If
            tmpNode = theNode("Input")
            If tmpNode IsNot Nothing Then
                InputChannel = TryCast(GetChannel(tmpNode.InnerText), KineticaRT.Channels.INumericChannel)
                InputDelegator = New KineticaRTParameterValueDelegator(InputChannel)
            End If
            tmpNode = theNode("Parameter")
            If tmpNode IsNot Nothing Then
                name = tmpNode.InnerText
                If Not Integer.TryParse(name.Remove(0, 3), flowMeterId) Then
                    flowMeterId = 0
                Else
                    flowMeterId -= 1
                End If
            End If
            tmpNode = theNode("Units")
            If tmpNode IsNot Nothing Then
                units = tmpNode.InnerText
            End If

            itsTurbineFlowMeterProcessors.Add(name, _
                New TurbineFlowMeterProcessor( _
                    units, _
                    AddressOf UnitConverterMgr.Instance.ProcessUnitConversion, _
                    AddressOf SGDelegator.GetValue, _
                    AddressOf TSGDelegator.GetValue, _
                    AddressOf TFuelDelegator.GetValue, _
                    New LinearCharacterizer(itsTurbineFlowMeter(flowMeterId).Items), _
                    itsTurbineFlowMeter(flowMeterId).FromUnits, _
                    AddressOf InputDelegator.SetValue))

            Return bError
        End Function

        Private Sub ProcessTurbineFlowMeterNode(ByVal theNode As XmlNode, ByVal theChannelCollection As KineticaRT.ChannelCollection)
            If "TurbineFlowMeter" = theNode.Name Then
                ProcessTurbineFlowMeterProcessorCreation(theNode, theChannelCollection)
            End If
        End Sub

        Private Function ReadTurbineFlowMeters() As Boolean
            Dim bError As Boolean = False
            Dim testSessionPath As String

            testSessionPath = System.Configuration.ConfigurationSettings.AppSettings("TestSessionPath")
            If testSessionPath IsNot Nothing Then
                Dim Path As String = System.IO.Path.GetDirectoryName(testSessionPath)
                Dim FullFilePath As String = Path & System.IO.Path.DirectorySeparatorChar & DATABASEFILENAME
                Dim FullFileWorkgroup As String = Path & System.IO.Path.DirectorySeparatorChar & DATABASESECURENAME

                Dim connectString As String
                If IntPtr.Size = 8 Then
                    connectString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0};Jet OLEDB:System database={1};User ID={2}; Password={3}", FullFilePath, FullFileWorkgroup, DATABASEUSER, DATABASEPSWD)
                Else
                    connectString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Jet OLEDB:System database={1};User ID={2}; Password={3}", FullFilePath, FullFileWorkgroup, DATABASEUSER, DATABASEPSWD)
                End If
                Dim theConnection As New OleDbConnection(connectString)

                theConnection.Open()
                Try
                    Dim oldSchema As Boolean = False
                    Dim newSchema As Boolean = False
                    Dim schemaTable As DataTable = _
                        theConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})

                    For i As Integer = 0 To schemaTable.Rows.Count - 1
                        If "Flowmeter_Master" = schemaTable.Rows(i)!TABLE_NAME.ToString Then
                            newSchema = True
                        ElseIf "Flowmeter_Information" = schemaTable.Rows(i)!TABLE_NAME.ToString Then
                            oldSchema = True
                        End If
                    Next
                    If newSchema Then
                        ReadTurbineFlowMetersFromNewTable(theConnection)
                    ElseIf oldSchema Then
                        ReadTurbineFlowMetersFromOldTable(theConnection)
                    Else
                        MsgBox("Unknown Turbine Flow Meter table schema, aborting.", MsgBoxStyle.Critical)
                        bError = True
                    End If
                Finally
                    theConnection.Close()
                End Try
            End If

                Return bError
        End Function

        Private Function ReadTurbineFlowMetersFromNewTable(ByRef theConnection As OleDbConnection) As Boolean
            Dim bError As Boolean = False
            Dim getMaxID As String = "SELECT MAX(ID) FROM Flowmeter_Master"

            Dim command As New OleDbCommand(getMaxID, theConnection)
            Dim count As Integer = CInt(command.ExecuteScalar()) - 1
            ReDim Me.itsTurbineFlowMeter(count)

            command.Dispose()
            Dim queryMaster As String = _
                "SELECT Flowmeter_Master.ID As ID, Flowmeter_InputUnit.Description As InputUnit, Flowmeter_OutputUnit.Description As OutputUnit " & _
                "FROM Flowmeter_Master, Flowmeter_InputUnit, Flowmeter_OutputUnit " & _
                "WHERE Flowmeter_Master.TableInputUnit = Flowmeter_InputUnit.ID AND " & _
                      "Flowmeter_Master.TableOutputUnit = Flowmeter_OutputUnit.ID"
            Dim queryDetail As String = _
                "SELECT * FROM Flowmeter_Detail WHERE FlowmeterID=?"
            Dim getMaxDetailID As String = "SELECT MAX(DataPoint) FROM Flowmeter_Detail where FlowmeterID=?"
            command = New OleDbCommand(queryMaster, theConnection)
            Dim getDetailCommand As OleDbCommand = New OleDbCommand(queryDetail, theConnection)
            Dim reader As OleDbDataReader = command.ExecuteReader()
            While reader.Read()
                Dim id As Integer = CType(reader.Item("ID"), Integer)
                Dim getMaxIDDetailCommand As OleDbCommand = New OleDbCommand(getMaxDetailID, theConnection)
                getMaxIDDetailCommand.Parameters.Clear()
                getMaxIDDetailCommand.Parameters.Add("FlowmeterID", OleDbType.Integer).Value = id
                Try
                    count = DirectCast(getMaxIDDetailCommand.ExecuteScalar(), Integer)
                    Dim values(count - 1, 1) As Double
                    getDetailCommand.Parameters.Clear()
                    getDetailCommand.Parameters.Add("FlowmeterID", OleDbType.Integer).Value = id
                    Dim detailReader As OleDbDataReader = getDetailCommand.ExecuteReader()
                    While detailReader.Read()
                        values(CInt(detailReader.Item("DataPoint")) - 1, 0) = CDbl(detailReader.Item("OutputValue"))
                        values(CInt(detailReader.Item("DataPoint")) - 1, 1) = CDbl(detailReader.Item("InputValue"))
                    End While
                    detailReader.Close()

                    itsTurbineFlowMeter(CInt(reader.Item("ID")) - 1) = _
                        New TurbineFlowMeter(CStr(reader.Item("OutputUnit")), CStr(reader.Item("InputUnit")), values)

                Catch ex As Exception

                End Try

            End While

            Return bError
        End Function

        Private Function ReadTurbineFlowMetersFromOldTable(ByRef theConnection As OleDbConnection) As Boolean
            Dim bError As Boolean = False
            Dim getMaxID As String = "SELECT MAX(ID) FROM Flowmeter_Information"

            Dim command As New OleDbCommand(getMaxID, theConnection)
            Dim count As Integer = CInt(command.ExecuteScalar()) - 1
            ReDim Me.itsTurbineFlowMeter(count)

            command.Dispose()
            Dim queryInfo As String = _
                "SELECT Flowmeter_Information.ID As ID " & _
                "FROM Flowmeter_Information"
            Dim queryDetail As String = _
                "SELECT * FROM Flowmeter WHERE Flowmeter_ID=?"
            Dim getMaxDetailID As String = "SELECT MAX([Data Point]) FROM Flowmeter where Flowmeter_ID=?"
            command = New OleDbCommand(queryInfo, theConnection)
            Dim getDetailCommand As OleDbCommand = New OleDbCommand(queryDetail, theConnection)
            Dim reader As OleDbDataReader = command.ExecuteReader()
            Try
                While reader.Read()
                    Dim id As Integer = CType(reader.Item("ID"), Integer)
                    Dim getMaxIDDetailCommand As OleDbCommand = New OleDbCommand(getMaxDetailID, theConnection)
                    getMaxIDDetailCommand.Parameters.Clear()
                    getMaxIDDetailCommand.Parameters.Add("Flowmeter_ID", OleDbType.Integer).Value = id
                    count = CInt(getMaxIDDetailCommand.ExecuteScalar())
                    Dim values(count - 1, 1) As Double
                    getDetailCommand.Parameters.Clear()
                    getDetailCommand.Parameters.Add("Flowmeter_ID", OleDbType.Integer).Value = id
                    Dim detailReader As OleDbDataReader = getDetailCommand.ExecuteReader()
                    Try
                        While detailReader.Read()
                            values(CInt(detailReader.Item("Data Point")) - 1, 0) = CDbl(detailReader.Item("USGPM"))
                            values(CInt(detailReader.Item("Data Point")) - 1, 1) = CDbl(detailReader.Item("Frequency"))
                        End While
                    Finally
                        detailReader.Close()
                    End Try
                    itsTurbineFlowMeter(CInt(reader.Item("ID")) - 1) = _
                        New TurbineFlowMeter("USGPM", "HZ", values)
                End While
            Finally
                reader.Close()
            End Try
            Return bError
        End Function
        Private Function ResetChannels() As Boolean
            Dim bError As Boolean = False
            Dim theChannelCollection As KineticaRT.ChannelCollection = _
                TryCast(GetObject(CHANNELMAPCOLLECTIONNAME), KineticaRT.ChannelCollection)

            If theChannelCollection IsNot Nothing Then
                For Each entry As System.Collections.DictionaryEntry In theChannelCollection
                    Dim channel As KineticaRT.Servers.OpcClient.Item = _
                        TryCast(entry.Value, KineticaRT.Servers.OpcClient.Item)
                    '
                    ' Don't reset value comming from User Input Screen on the HMI such as SG and TSG
                    '
                    If channel IsNot Nothing AndAlso Not channel.ItemID.ToUpper().Contains("ANSWER") Then
                        channel.Value = KineticaRT.Values.Values.Zero
                    End If
                Next
            End If
        End Function

#Region "ComboBoxes Handlers"
        Private Sub OnDataFileCBIndexChanged(ByVal sender As Object, ByVal eventArgs As System.EventArgs)
            Dim bError As Boolean = False
            Dim xmlTextReader As New XmlTextReader(PERFDATADIRECTORY & Path.DirectorySeparatorChar & _
                itsDataFileCB.Text)

            itsPerformanceXmlDoc = New XmlDocument()

            itsConditionsCB.SelectedIndex = -1
            itsConditionsCB.Items.Clear()
            itsConditionsXmlNodes.Clear()

            itsPerformanceXmlDoc.Load(xmlTextReader)
            xmlTextReader.Close()

            itsPerformanceXmlNode = itsPerformanceXmlDoc.Item("Performance")
            If itsPerformanceXmlNode Is Nothing Then
                MsgBox("Can't find 'Performance' root element, Invalid Perf Data xml file. Aborting")
                bError = True
            End If
            If Not bError Then
                itsConditionsCB.BeginUpdate()
                Try
                    '
                    ' Add Special Clear/Reset Condition
                    '
                    itsConditionsCB.Items.Add("0 - CLEAR / RESET DATA")
                    itsConditionsXmlNodes.Add(Nothing)
                    For Each condition As XmlNode In itsPerformanceXmlNode("Conditions").ChildNodes
                        itsConditionsCB.Items.Add(condition("NO").InnerText & " - " & condition("Description").InnerText)
                        itsConditionsXmlNodes.Add(condition)
                    Next
                Finally
                    itsConditionsCB.EndUpdate()
                End Try
            End If
            Dim engineNode As XmlNode = itsPerformanceXmlNode("Engine")
            If engineNode Is Nothing Then
                MsgBox("Can't find Engine section, skipping")
            Else
                itsEngineModelCh.Value = New KineticaRT.Values.Text(engineNode("Model").InnerText)
                itsEngineSNCh.Value = New KineticaRT.Values.Text(engineNode("SN").InnerText)
            End If

            ProcessChannelMap()
        End Sub

        Private Sub OnConditionsCBIndexChanged(ByVal sender As Object, ByVal eventArgs As System.EventArgs)
            If itsConditionsCB.SelectedIndex = 0 Then
                ResetChannels()
            ElseIf itsConditionsCB.SelectedIndex > 0 Then
                ProcessCondition(itsConditionsXmlNodes(itsConditionsCB.SelectedIndex))
            End If
        End Sub
#End Region

#End Region

    End Class
End Namespace


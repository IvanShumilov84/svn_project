Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms
Imports KineticaRT
Imports KineticaRT.Channels
Imports KineticaRT.Sequence
Imports KineticaRT.Values
Imports System.Data.OleDb
Imports Microsoft.VisualBasic


Imports CEL.KineticaRT.Sequence
Imports CEL.Library
Imports CEL.HMI
Imports CEL.EngUnit
Imports CEL.Data.BusinessObjects

Namespace CEL.HMI

    Public Class FlowmeterController
        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()
        Private _Sequence As CEL.KineticaRT.Sequence.Sequence

        Public Sub New(ByRef Sequence As CEL.KineticaRT.Sequence.Sequence)
            _Sequence = Sequence
            Initialize()
        End Sub

        Private Function Read_Flowmeters() As Boolean
            Read_Flowmeters = False
            Try

                'Query Database 
                Dim flowmeterMasterCollection As FlowmeterMasterCollection = New FlowmeterMasterCollection()
                flowmeterMasterCollection.es.Connection.Name = CELProject.DefaultConnectionName
                flowmeterMasterCollection.Query.Select(flowmeterMasterCollection.Query.Id.Max)
                flowmeterMasterCollection.Query.Load()
                'strSQL = "SELECT * FROM Flowmeter_Master" ' Flowmeter_Master
                Dim idMax As Integer? = flowmeterMasterCollection.Item(0).Id
                ReDim FlowMeter(CInt(idMax))

                flowmeterMasterCollection = New FlowmeterMasterCollection()
                flowmeterMasterCollection.es.Connection.Name = CELProject.DefaultConnectionName
                flowmeterMasterCollection.Query.Where(flowmeterMasterCollection.Query.Id <> 0) '0 = NO FLOWMETER
                flowmeterMasterCollection.Query.Load()

                For Each recordMaster As FlowmeterMaster In flowmeterMasterCollection

                    'Fill the header and detail information of the flow meter
                    FlowMeter(System.Convert.ToInt32(recordMaster.Id)).ID = System.Convert.ToInt16(recordMaster.Id)
                    FlowMeter(System.Convert.ToInt32(recordMaster.Id)).SerialNumber = recordMaster.SerialNumber.ToString
                    FlowMeter(System.Convert.ToInt32(recordMaster.Id)).SpecificGravity = System.Convert.ToDouble(recordMaster.Sg)
                    FlowMeter(System.Convert.ToInt32(recordMaster.Id)).CalibrationDue = recordMaster.CalibrationDue.ToString
                    FlowMeter(System.Convert.ToInt32(recordMaster.Id)).TableInputUnit = CType(recordMaster.TableOutputUnit, eFlowMeter_InputUnit)
                    FlowMeter(System.Convert.ToInt32(recordMaster.Id)).TableOutputUnit = CType(recordMaster.TableOutputUnit, eFlowMeter_OutputUnit)

                    'strSQL = "SELECT * FROM Flowmeter_Detail WHERE FlowmeterID = " & rsFlowMeterMaster.Fields("ID").Value.ToString

                    Dim j As Integer = 0
                    ReDim FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail(recordMaster.FlowmeterDetailCollectionByFlowmeterID.Count)

                    For Each recordDetail As FlowmeterDetail In recordMaster.FlowmeterDetailCollectionByFlowmeterID
                        'Fill the detail of the current flow meter
                        j = System.Convert.ToInt16(recordDetail.DataPoint)

                        FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail(j).ID = System.Convert.ToInt16(recordMaster.Id)
                        FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail(j).DataPoint = System.Convert.ToInt16(recordDetail.DataPoint)
                        FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail(j).InputValue = System.Convert.ToDouble(recordDetail.InputValue)
                        FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail(j).OutputValue = System.Convert.ToDouble(recordDetail.OutputValue)

                    Next

                    ' Check if TestSession Data are good
                    For j = FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail.GetUpperBound(0) To 1 Step -1
                        If FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail(j).InputValue < FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail(j - 1).InputValue Or FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail(j).OutputValue < FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail(j - 1).OutputValue Then
                            clsTopMostMessageBox.Show("Flowmeter_Sequence.cFlowMeter.Flowmeter_Characterizer: TestSession data are not in order (DataPoint :" + FlowMeter(System.Convert.ToInt32(recordMaster.Id)).Detail(j).DataPoint.ToString + ").", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Next

                    If recordMaster.FlowmeterDetailCollectionByFlowmeterID.Count = 0 Then
                        clsTopMostMessageBox.Show(Me.GetType.Name + MethodBase.GetCurrentMethod().ToString() + " Flowmeter has no records ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Next

                Read_Flowmeters = True


            Catch Ex As Exception
                clsTopMostMessageBox.Show("Flowmeter_Sequence.Read_Flowmeters: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
            End Try
        End Function

        Private Sub Flowmeter_WorkingThread()

            Try

                Do While Not ExitThread

                    Thread.Sleep(100)

                    ComputeFlow(False)
                    If CELProject.Test_Mgr_Status.Value.ToDouble() = eTestMgrStatus.Closing Then
                        ExitThread = True
                    End If
                Loop

            Catch Ex As Exception
                clsTopMostMessageBox.Show("Flowmeter_Sequence.WorkingThread: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub
        Public Sub ComputeFlow(ByVal CalulateForLogAndPerformanceReports As Boolean)

            If CELProject.IsFalse(CELProject.ARCHIVE_PLAY.Value) Then

                If CalulateForLogAndPerformanceReports = False Then

                    For Each fmeter As sFlowmeter_PerEngine In FlowmeterPerEngine
                        fmeter.CalChannel.Value = New Global.KineticaRT.Values.Double(fmeter.Flowmeter.GetFlow(fmeter.RawChannel.Value.ToDouble(), fmeter.CustomOutputUnit))
                        fmeter.PPHChannel.Value = New Global.KineticaRT.Values.Double(fmeter.Flowmeter.CalculatePPH(fmeter.RawChannel.Value.ToDouble(),
                                                                                                                    fmeter.FuelSlopeChannel.Value.ToDouble(),
                                                                                                                    fmeter.FuelTemperatureChannel.Value.ToDouble(),
                                                                                                                    fmeter.FuelSampleTemperatureChannel.Value.ToDouble(),
                                                                                                                    fmeter.FuelSampleSGChannel.Value.ToDouble(),
                                                                                                                    fmeter.WaterDensityAt60FChannel.Value.ToDouble(), fmeter.CalChannel.Value.ToDouble()))
                        fmeter.TotalWeightChannel.Value = New Global.KineticaRT.Values.Double(fmeter.Flowmeter.GetTotalFlowWeight(fmeter.TotalWeightChannel.Value.ToDouble(), fmeter.PPHChannel.Value.ToDouble()))
                        fmeter.TotChannel.Value = New Global.KineticaRT.Values.Double(fmeter.Flowmeter.GetTotalFlow(fmeter.TotChannel.Value.ToDouble(), fmeter.CalChannel.Value.ToDouble(), fmeter.CustomOutputUnit))
                    Next

                Else

                    For Each fmeter As sFlowmeter_PerEngine In FlowmeterPerEngine
                        fmeter.CalChannelLogPerf.Value = New Global.KineticaRT.Values.Double(fmeter.Flowmeter.GetFlow(fmeter.RawChannelLogPerf.Value.ToDouble(), fmeter.CustomOutputUnit))
                        fmeter.TotChannelLogPerf.Value = New Global.KineticaRT.Values.Double(fmeter.Flowmeter.GetTotalFlow(fmeter.TotChannelLogPerf.Value.ToDouble(), fmeter.CalChannelLogPerf.Value.ToDouble(), fmeter.CustomOutputUnit))
                    Next

                End If

            End If

        End Sub

        Private Sub Initialize()

            Try

                ExitThread = False

                Read_Flowmeters()

                If Not FlowmeterPerEngine Is Nothing Then
                    FlowmeterPerEngine.Clear()
                Else
                    FlowmeterPerEngine = New List(Of sFlowmeter_PerEngine)
                End If

                'Load en mémoire les flowmeters associés au moteur selon la table Flowmeter_Engine

                'Dim strSQL As String = "SELECT Flowmeter.EngineModel,Flowmeter.RawChannel,Flowmeter.CalChannel,Flowmeter.TotChannel,Flowmeter.FlowmeterMapID,Flowmeter.CustomOutputUnit, Flowmeter_Map.FlowmeterID FROM Flowmeter INNER JOIN Flowmeter_Map ON Flowmeter_Map.ID = Flowmeter.FlowmeterMapID WHERE Flowmeter.EngineModel='" + CELProject.EngineModel + "' AND Flowmeter.ProjectID =" & CELProject.Project.Number
                'Query Database 
                Dim flowmeterCollection As FlowmeterCollection = New FlowmeterCollection()
                flowmeterCollection.es.Connection.Name = CELProject.DefaultConnectionName
                flowmeterCollection.Query.Where(flowmeterCollection.Query.ProjectID = CELProject.Project.Number)
                flowmeterCollection.Query.Where(flowmeterCollection.Query.EngineModel = CELProject.EngineModel)
                flowmeterCollection.Query.Where(flowmeterCollection.Query.FlowmeterMapID <> 0)  'FlowmeterMapID = 0 = NO FLOWMETER de configure
                flowmeterCollection.Query.Load()

                For Each flometerPerEngine As Flowmeter In flowmeterCollection
                    Dim fmeter As New sFlowmeter_PerEngine

                    _Sequence.InitializeChannel(fmeter.RawChannel, flometerPerEngine.RawChannel)
                    _Sequence.InitializeChannel(fmeter.RawChannelLogPerf, "LOG_PERF_" + flometerPerEngine.RawChannel)

                    _Sequence.InitializeChannel(fmeter.CalChannel, flometerPerEngine.CalChannel)
                    _Sequence.InitializeChannel(fmeter.CalChannelLogPerf, "LOG_PERF_" + flometerPerEngine.CalChannel)

                    _Sequence.InitializeChannel(fmeter.TotChannel, flometerPerEngine.TotChannel)
                    _Sequence.InitializeChannel(fmeter.TotChannelLogPerf, "LOG_PERF_" + flometerPerEngine.TotChannel)

                    _Sequence.InitializeChannel(fmeter.FuelSampleSGChannel, flometerPerEngine.FuelSampleSGChannel)
                    _Sequence.InitializeChannel(fmeter.FuelSampleTemperatureChannel, flometerPerEngine.FuelSampleTemperatureChannel)
                    _Sequence.InitializeChannel(fmeter.FuelSlopeChannel, flometerPerEngine.FuelSlopeChannel)
                    _Sequence.InitializeChannel(fmeter.FuelTemperatureChannel, flometerPerEngine.FuelTemperatureChannel)
                    _Sequence.InitializeChannel(fmeter.PPHChannel, flometerPerEngine.PPHChannel)
                    _Sequence.InitializeChannel(fmeter.TotalWeightChannel, flometerPerEngine.TotalWeightChannel)
                    _Sequence.InitializeChannel(fmeter.WaterDensityAt60FChannel, flometerPerEngine.WaterDensityAt60FChannel)

                    fmeter.Flowmeter = New cFlowMeter(FlowMeter(System.Convert.ToInt32(flometerPerEngine.UpToFlowmeterMapByFlowmeterMapID.FlowmeterID)))
                    fmeter.CustomOutputUnit = CType(flometerPerEngine.CustomOutputUnit, eFlowMeter_OutputUnit)

                    If (fmeter.RawChannel Is Nothing Or fmeter.CalChannel Is Nothing) Then
                        Throw New Exception("Flowmeter_Sequence.OnTestSessionRunning: Invalid RawChannel of CalChannel")
                    End If

                    FlowmeterPerEngine.Add(fmeter)

                Next

                'Create Thread for flowmeters calculation
                Flowmeter_WorkerThread = New Thread(AddressOf Flowmeter_WorkingThread)
                Flowmeter_WorkerThread.Priority = ThreadPriority.Normal
                Flowmeter_WorkerThread.Name = "Flowmeter_WorkerThread"
                Flowmeter_WorkerThread.Start()

                'End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("Flowmeter_Sequence.OnTestSessionRunning: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private FlowMeter() As sFlowMeter_Master
        Private FlowmeterPerEngine As System.Collections.IList

        Private Flowmeter_WorkerThread As Thread
        Private ExitThread As Boolean = False


#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose

            Try
                ThreadHelper.StopOrKillThread(Flowmeter_WorkerThread, Me.ExitThread)

                If Not FlowmeterPerEngine Is Nothing Then
                    'Reset Each FTotalizer 
                    For Each fmeter As sFlowmeter_PerEngine In FlowmeterPerEngine
                        fmeter.TotChannel.Value = Values.Zero
                        fmeter.TotalWeightChannel.Value = Values.Zero
                    Next
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence.OnTestSessionPostClosing: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

#Region " Public Enum "

    Public Structure sFlowMeter_Master
        Dim ID As Short
        'Dim Description As String
        Dim SerialNumber As String
        Dim SpecificGravity As Double
        Dim CalibrationDue As String
        Dim TableInputUnit As eFlowMeter_InputUnit
        Dim TableOutputUnit As eFlowMeter_OutputUnit
        Dim Detail() As sFlowMeter_Detail
    End Structure
    Public Structure sFlowMeter_Detail
        Dim ID As Short
        Dim DataPoint As Short
        Dim InputValue As Double
        Dim OutputValue As Double
    End Structure
    Public Structure sFlowmeter_PerEngine
        Public RawChannel As IChannel
        Public RawChannelLogPerf As IChannel
        Public CalChannel As IChannel
        Public CalChannelLogPerf As IChannel
        Public TotChannel As IChannel
        Public TotChannelLogPerf As IChannel
        Public Flowmeter As cFlowMeter
        Public CustomOutputUnit As eFlowMeter_OutputUnit
        Public FuelSlopeChannel As IChannel
        Public FuelTemperatureChannel As IChannel
        Public FuelSampleTemperatureChannel As IChannel
        Public FuelSampleSGChannel As IChannel
        Public WaterDensityAt60FChannel As IChannel
        Public PPHChannel As IChannel
        Public TotalWeightChannel As IChannel
    End Structure
    'Public Enum eFlowMeter 'Custom par projet voir table Access Flowmeter_Master
    '    OilGearBoxFlow = 0
    '    OilPowerSectionFlow = 1
    '    StarboardFlow = 2
    '    PortFlow = 3
    'End Enum
    Public Enum eFlowMeter_InputUnit 'Match exactement les valeurs de la Table Flowmeter_InputUnit
        Frequency = 1
        Voltage = 2
    End Enum
    Public Enum eFlowMeter_OutputUnit 'Match exactement les valeurs de la Table Flowmeter_OutputUnit
        USGPM = 1
        USGPH = 2
        LPM = 3
        LPH = 4
    End Enum

#End Region

#Region " Public Class FlowMeter "

    Public Class cFlowMeter

        Private _FlowMeterData As sFlowMeter_Master

        Private _TotalFuel As Double
        Private _TotalFuelWeight As Double
        Private _FuelFlow_Start As System.DateTime = Now
        Private _FuelFlowWeight_Start As System.DateTime = Now
        Private _ts_FuelFlow_time As System.TimeSpan 'Used to calculate the acceleration time

        Public Sub New(ByVal FlowMeterData As sFlowMeter_Master)
            _FlowMeterData = FlowMeterData
        End Sub
        Public Property ID() As Short
            Get
                Return _FlowMeterData.ID
            End Get
            Set(ByVal value As Short)
                _FlowMeterData.ID = value
            End Set
        End Property
        'Public Property Description() As String
        '    Get
        '        Return _FlowMeterData.Description
        '    End Get
        '    Set(ByVal value As String)
        '        _FlowMeterData.Description = value
        '    End Set
        'End Property
        Public Property SerialNumber() As String
            Get
                Return _FlowMeterData.SerialNumber
            End Get
            Set(ByVal value As String)
                _FlowMeterData.SerialNumber = value
            End Set
        End Property
        Public Property TableInputUnit() As eFlowMeter_InputUnit
            Get
                Return _FlowMeterData.TableInputUnit
            End Get
            Set(ByVal value As eFlowMeter_InputUnit)
                _FlowMeterData.TableInputUnit = value
            End Set
        End Property
        Public Property TableOutputUnit() As eFlowMeter_OutputUnit
            Get
                Return _FlowMeterData.TableOutputUnit
            End Get
            Set(ByVal value As eFlowMeter_OutputUnit)
                _FlowMeterData.TableOutputUnit = value
            End Set
        End Property
        Public Property Detail(ByVal Index As Integer) As sFlowMeter_Detail
            Get
                Return _FlowMeterData.Detail(Index)
            End Get
            Set(ByVal value As sFlowMeter_Detail)
                _FlowMeterData.Detail(Index) = value
            End Set
        End Property
        'Public Property Reset() As Boolean
        '    Get
        '        'Do Nothing
        '    End Get
        '    Set(ByVal value As Boolean)
        '        If value = True Then
        '            _TotalFuel = 0.0
        '        End If
        '    End Set
        'End Property
        Public Function CalculatePPH(ByVal flowHz As Double, ByVal fuelSlope As Double, ByVal fuelTemperature As Double,
                                     ByVal fuelSampleTemperature As Double, ByVal fuelSampleSG As Double,
                                     ByVal waterDensityAt60F As Double, ByVal calibratedFlow As Double) As Double
            '   If(WF1_HZ < 10.0, 0.0, ((FUEL_SLOPE × (T_FUEL_DEGF - SAMPLE_T_FUEL_DEGF)) + SAMPLE_SG_FUEL) × WATER_DENSITY_AT_60F × 60 × WF_CAL1)

            If flowHz < 10.0 Then
                Return 0.0
            End If

            Return ((fuelSlope * (fuelTemperature - fuelSampleTemperature)) + fuelSampleSG) * waterDensityAt60F * 60.0 * calibratedFlow
        End Function
        Public Function GetFlow(ByVal Value As Double, ByVal ConvertTableOutputTo As eFlowMeter_OutputUnit) As Double
            'Trap an NaN value
            If Double.IsNaN(Value) Or Value = 0 Then Return 0

            Dim flow As Double = 0.0

            'Du to calibration the Value which is input frequency have a little bit of noise around 0 Hz
            'This is why we ignore the frequency under 1 Hz. The first point of the flowmeter is around 19Hz!!
            If Value < 1 AndAlso _FlowMeterData.TableInputUnit = eFlowMeter_InputUnit.Frequency Then
                flow = 0
            Else
                flow = Flowmeter_Characterizer(Value)
            End If

            Select Case _FlowMeterData.TableOutputUnit
                Case eFlowMeter_OutputUnit.LPM
                    Select Case ConvertTableOutputTo
                        Case eFlowMeter_OutputUnit.LPM
                            Return flow
                        Case eFlowMeter_OutputUnit.LPH
                            Return flow * UnitConversion.Minutes_Per_Hour
                        Case eFlowMeter_OutputUnit.USGPM
                            Return flow / UnitConversion.Liters_Per_UsGallon
                        Case eFlowMeter_OutputUnit.USGPH
                            Return flow * UnitConversion.Minutes_Per_Hour / UnitConversion.Liters_Per_UsGallon
                    End Select
                Case eFlowMeter_OutputUnit.LPH
                    Select Case ConvertTableOutputTo
                        Case eFlowMeter_OutputUnit.LPM
                            Return flow / UnitConversion.Minutes_Per_Hour
                        Case eFlowMeter_OutputUnit.LPH
                            Return flow
                        Case eFlowMeter_OutputUnit.USGPM
                            Return flow / UnitConversion.Minutes_Per_Hour / UnitConversion.Liters_Per_UsGallon
                        Case eFlowMeter_OutputUnit.USGPH
                            Return flow / UnitConversion.Liters_Per_UsGallon
                    End Select
                Case eFlowMeter_OutputUnit.USGPM
                    Select Case ConvertTableOutputTo
                        Case eFlowMeter_OutputUnit.LPM
                            Return flow * UnitConversion.Liters_Per_UsGallon
                        Case eFlowMeter_OutputUnit.LPH
                            Return flow * UnitConversion.Liters_Per_UsGallon * UnitConversion.Minutes_Per_Hour
                        Case eFlowMeter_OutputUnit.USGPM
                            Return flow
                        Case eFlowMeter_OutputUnit.USGPH
                            Return flow * UnitConversion.Minutes_Per_Hour
                    End Select
                Case eFlowMeter_OutputUnit.USGPH
                    Select Case ConvertTableOutputTo
                        Case eFlowMeter_OutputUnit.LPM
                            Return flow * UnitConversion.Liters_Per_UsGallon / UnitConversion.Minutes_Per_Hour
                        Case eFlowMeter_OutputUnit.LPH
                            Return flow * UnitConversion.Liters_Per_UsGallon
                        Case eFlowMeter_OutputUnit.USGPM
                            Return flow / UnitConversion.Minutes_Per_Hour
                        Case eFlowMeter_OutputUnit.USGPH
                            Return flow
                    End Select
            End Select

        End Function

        Public Function GetTotalFlow(ByVal CurrentTotalFlow As Double, ByVal Value As Double, ByVal ConvertTableOutputTo As eFlowMeter_OutputUnit) As Double

            'Trap an NaN value
            If Double.IsNaN(Value) Then Return 0

            _ts_FuelFlow_time = Now.Subtract(_FuelFlow_Start)

            Dim time_elapsed_msec As Double = _ts_FuelFlow_time.Milliseconds

            _FuelFlow_Start = Now

            Dim unitTime As Double = 0.0

            Select Case ConvertTableOutputTo
                Case eFlowMeter_OutputUnit.LPM
                    'Converting msec to minutes : dividing by 60000
                    unitTime = time_elapsed_msec / UnitConversion.mSeconds_Per_Minute
                Case eFlowMeter_OutputUnit.LPH
                    'Converting msec to hours : dividing by 3600000
                    unitTime = time_elapsed_msec / UnitConversion.mSeconds_Per_Hour
                Case eFlowMeter_OutputUnit.USGPM
                    unitTime = time_elapsed_msec / UnitConversion.mSeconds_Per_Minute
                Case eFlowMeter_OutputUnit.USGPH
                    unitTime = time_elapsed_msec / UnitConversion.mSeconds_Per_Hour
            End Select

            _TotalFuel = CurrentTotalFlow + Math.Max(0.0, Value * unitTime)
            Return _TotalFuel

        End Function

        Public Function GetTotalFlowWeight(ByVal CurrentTotalFlowWeight As Double, ByVal flowPPH As Double) As Double

            'Trap an NaN value
            If Double.IsNaN(flowPPH) Then Return 0

            _ts_FuelFlow_time = Now.Subtract(_FuelFlowWeight_Start)

            Dim time_elapsed_msec As Double = _ts_FuelFlow_time.Milliseconds

            _FuelFlowWeight_Start = Now

            Dim unitTime As Double = time_elapsed_msec / UnitConversion.mSeconds_Per_Hour

            _TotalFuelWeight = CurrentTotalFlowWeight + Math.Max(0.0, flowPPH * unitTime)
            Return _TotalFuelWeight

        End Function
        '*****************************
        '* Flowmeter_Characterizer() *
        '*****************************
        Private Function Flowmeter_Characterizer(ByVal Value As Double) As Double
            Try
                Dim Flow As Double
                Dim i, j As Integer
                Dim Offset As Double
                Dim Slope As Double
                Dim Divider As Double

                Dim arrayMaxIndex As Integer = _FlowMeterData.Detail.GetUpperBound(0)

                If Value <= _FlowMeterData.Detail(0).InputValue Then 'Extrapollation <

                    'To avoid division by 0
                    Divider = _FlowMeterData.Detail(1).InputValue - _FlowMeterData.Detail(0).InputValue

                    If Divider = 0 Then
                        Flow = 0
                    Else
                        Slope = (_FlowMeterData.Detail(1).OutputValue - _FlowMeterData.Detail(0).OutputValue) / Divider
                        Offset = _FlowMeterData.Detail(0).OutputValue - Slope * _FlowMeterData.Detail(0).InputValue
                        Flow = Slope * Value + Offset
                    End If

                ElseIf Value >= _FlowMeterData.Detail(arrayMaxIndex).InputValue Then 'Extrapollation >

                    For i = arrayMaxIndex To 0 Step -1
                        If _FlowMeterData.Detail(i).InputValue <> _FlowMeterData.Detail(arrayMaxIndex).InputValue Then
                            Exit For
                        End If
                    Next

                    Divider = _FlowMeterData.Detail(arrayMaxIndex).InputValue - _FlowMeterData.Detail(i).InputValue

                    If Divider = 0 Then
                        Flow = 0
                    Else
                        Slope = (_FlowMeterData.Detail(arrayMaxIndex).OutputValue - _FlowMeterData.Detail(i).OutputValue) / Divider
                        Offset = _FlowMeterData.Detail(arrayMaxIndex).OutputValue - Slope * _FlowMeterData.Detail(arrayMaxIndex).InputValue
                        Flow = Slope * Value + Offset
                    End If


                Else 'Interpollation
                    For i = 0 To (arrayMaxIndex - 1)
                        If ((Value >= _FlowMeterData.Detail(i).InputValue) And (Value <= _FlowMeterData.Detail(i + 1).InputValue)) Then
                            'Exit For
                            j = i
                            i = 70
                        End If
                    Next

                    Divider = _FlowMeterData.Detail(j + 1).InputValue - _FlowMeterData.Detail(j).InputValue

                    If Divider = 0 Then
                        Flow = 0
                    Else
                        Slope = (_FlowMeterData.Detail(j + 1).OutputValue - _FlowMeterData.Detail(j).OutputValue) / Divider
                        Offset = _FlowMeterData.Detail(j).OutputValue - Slope * _FlowMeterData.Detail(j).InputValue
                        Flow = Slope * Value + Offset
                    End If
                End If

                If Flow < 0.0 Then
                    Flowmeter_Characterizer = 0
                Else
                    Flowmeter_Characterizer = Flow
                End If

            Catch Ex As Exception
                clsTopMostMessageBox.Show("Flowmeter_Sequence.cFlowMeter.Flowmeter_Characterizer: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

    End Class

#End Region

End Namespace



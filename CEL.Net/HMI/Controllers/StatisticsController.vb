Imports System
Imports System.Windows.Forms

Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.HMI

    Public Class StatisticsController 'Used for test cell statistics

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _Sequence As CEL.KineticaRT.Sequence.Sequence
        Private _FuelCostChannel As INumericChannel
        Private _LaborCostChannel As INumericChannel
        Private _FacilityCostChannel As INumericChannel
        Private _ElapsedTimeChannel As INumericChannel
        Private _ElapsedTimeUnitsChannel As String
        Private _FuelConsumptionChannel As INumericChannel
        Private _FuelConsumptionUnitsChannel As String
        Private _FuelCostResultChannel As INumericChannel
        Private _LaborCostResultChannel As INumericChannel
        Private _FacilityCostResultChannel As INumericChannel
        Private _TotalCostResultChannel As INumericChannel

        Public Sub New(ByRef Sequence As CEL.KineticaRT.Sequence.Sequence, _
                       Optional ByVal FuelCostChannelName As String = "FUEL_COST_$_PER_LBS", _
                       Optional ByVal LaborCostChannelName As String = "LABOR_COST_$_PER_HOUR", _
                       Optional ByVal FacilityCostChannelName As String = "FACILITY_COST_$_PER_HOUR", _
                       Optional ByVal ElapsedTimeChannelName As String = "ELAPSED_TIME", _
                       Optional ByVal ElapsedTimeUnitsChannelName As String = "HOURS", _
                       Optional ByVal FuelConsumptionChannelName As String = "TOTALIZER_VALUE", _
                       Optional ByVal FuelConsumptionUnitsChannelName As String = "LBS", _
                       Optional ByVal FuelCostResultChannelName As String = "FUEL_COST_$", _
                       Optional ByVal LaborCostResultChannelName As String = "LABOR_COST_$", _
                       Optional ByVal FacilityCostResultChannelName As String = "FACILITY_COST_$", _
                       Optional ByVal TotalCostResultChannelName As String = "TOTAL_COST_$")

            _Sequence = Sequence

            Dim TempChannel As IChannel = Nothing

            _Sequence.InitializeChannel(TempChannel, FuelCostChannelName)
            _FuelCostChannel = CType(TempChannel, INumericChannel)

            _Sequence.InitializeChannel(TempChannel, LaborCostChannelName)
            _LaborCostChannel = CType(TempChannel, INumericChannel)

            _Sequence.InitializeChannel(TempChannel, FacilityCostChannelName)
            _FacilityCostChannel = CType(TempChannel, INumericChannel)

            _Sequence.InitializeChannel(TempChannel, ElapsedTimeChannelName)
            _ElapsedTimeChannel = CType(TempChannel, INumericChannel)

            _ElapsedTimeUnitsChannel = ElapsedTimeUnitsChannelName

            _Sequence.InitializeChannel(TempChannel, FuelConsumptionChannelName)
            _FuelConsumptionChannel = CType(TempChannel, INumericChannel)

            _FuelConsumptionUnitsChannel = FuelConsumptionUnitsChannelName

            _Sequence.InitializeChannel(TempChannel, FuelCostResultChannelName)
            _FuelCostResultChannel = CType(TempChannel, INumericChannel)

            _Sequence.InitializeChannel(TempChannel, LaborCostResultChannelName)
            _LaborCostResultChannel = CType(TempChannel, INumericChannel)

            _Sequence.InitializeChannel(TempChannel, FacilityCostResultChannelName)
            _FacilityCostResultChannel = CType(TempChannel, INumericChannel)

            _Sequence.InitializeChannel(TempChannel, TotalCostResultChannelName)
            _TotalCostResultChannel = CType(TempChannel, INumericChannel)

        End Sub

        Public Sub CalculateStatistics()
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                'If CELProject.IsTrue(newVal) Then

                'Fuel Cost Result
                'Fuel Cost $/hour multiplied by elapsed Time
                If _FuelCostChannel.Value = Values.Zero Or _ElapsedTimeChannel.Value = Values.Zero Then
                    _FuelCostResultChannel.Value = Values.Zero
                Else
                    _FuelCostResultChannel.Value = New Global.KineticaRT.Values.Double(_FuelCostChannel.Value.ToDouble * _FuelConsumptionChannel.Value.ToDouble)
                End If

                'Labor Cost Result
                'Labor Cost $/hour multiplied by elapsed time
                If _LaborCostChannel.Value = Values.Zero Or _ElapsedTimeChannel.Value = Values.Zero Then
                    _LaborCostResultChannel.Value = Values.Zero
                Else
                    _LaborCostResultChannel.Value = New Global.KineticaRT.Values.Double(_LaborCostChannel.Value.ToDouble * _ElapsedTimeChannel.Value.ToDouble)
                End If

                'Facility Cost Result
                'Facility Cost $/hour multiplied by elapsed time
                If _FacilityCostChannel.Value = Values.Zero Or _ElapsedTimeChannel.Value = Values.Zero Then
                    _FacilityCostResultChannel.Value = Values.Zero
                Else
                    _FacilityCostResultChannel.Value = New Global.KineticaRT.Values.Double(_FacilityCostChannel.Value.ToDouble * _ElapsedTimeChannel.Value.ToDouble)
                End If

                'Total Cost Result
                'Fuel Cost Result + Labor Cost Result + Facility Cost Result
                _TotalCostResultChannel.Value = New Global.KineticaRT.Values.Double(_FuelCostResultChannel.Value.ToDouble + _LaborCostResultChannel.Value.ToDouble + _FacilityCostResultChannel.Value.ToDouble)

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnCalculateStatistics: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called
                    'RemoveHandlersStatsController()
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class


End Namespace
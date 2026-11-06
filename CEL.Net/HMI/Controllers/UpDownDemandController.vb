Imports System
Imports System.Windows.Forms

Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.HMI



    Public Class UpDownDemandController
        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _DemandChannel As IChannel = Nothing
        Private _DemandChannelIncrement As IChannel = Nothing
        Private _UpChannel As IBooleanChannel = Nothing
        Private _DownChannel As IBooleanChannel = Nothing
        Private _ResetChannel As IBooleanChannel = Nothing

        Private _Increment As Double = 0.5
        Public Property Increment() As Double
            Get
                Return _Increment
            End Get
            Set(ByVal value As Double)
                _Increment = value

                If Not (_DemandChannelIncrement Is Nothing) Then
                    If (_Increment <> _DemandChannelIncrement.Value.ToDouble()) Then
                        _DemandChannelIncrement.Value = New Global.KineticaRT.Values.Double(_Increment)
                    End If
                End If

            End Set
        End Property
        Private _Maximum As Double = 100
        Public Property Maximum() As Double
            Get
                Return _Maximum
            End Get
            Set(ByVal value As Double)
                _Maximum = value
            End Set
        End Property
        Private _Minimum As Double = 0
        Public Property Minimum() As Double
            Get
                Return _Minimum
            End Get
            Set(ByVal value As Double)
                _Minimum = value
            End Set
        End Property
        Private _ResetValue As Double = 0
        Public Property ResetValue() As Double
            Get
                Return _ResetValue
            End Get
            Set(ByVal value As Double)
                _ResetValue = value
            End Set
        End Property
        Public Sub New(ByVal DemandChannelName As String, _
                        ByVal DemandChannelIncrementName As String, _
                        ByVal UpChannelName As String, _
                        ByVal DownChannelName As String, _
                        ByVal ResetChannelName As String)

            Dim TempChannel As IChannel = Nothing

            If Not (DemandChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_DemandChannel, DemandChannelName)
            End If

            If Not (DemandChannelIncrementName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_DemandChannelIncrement, DemandChannelIncrementName)
            End If

            If Not (UpChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, UpChannelName)
                _UpChannel = CType(TempChannel, IBooleanChannel)
            End If

            If Not (DownChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, DownChannelName)
                _DownChannel = CType(TempChannel, IBooleanChannel)
            End If

            If Not (ResetChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(TempChannel, ResetChannelName)
                _ResetChannel = CType(TempChannel, IBooleanChannel)
            End If

            Me.AddHandlers()

            'Sylvain: J'ai enlever ce code car j'ai besoin que la sequence prenne la valeur initiale specifie dans le test session.
            'If Not (_DemandChannel Is Nothing) Then
            '_DemandChannel.Value = KineticaRT.Values.Values.Zero
            'End If

            If Not (_DemandChannelIncrement Is Nothing) Then
                _DemandChannelIncrement.Value = Values.Zero
            End If

            If Not (_UpChannel Is Nothing) Then
                _UpChannel.Value = Values.False
            End If

            If Not (_DownChannel Is Nothing) Then
                _DownChannel.Value = Values.False
            End If



        End Sub
        Private Sub AddHandlers()

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf Me.OnClose_Test
            End If

            If Not (_DemandChannel Is Nothing) Then
                AddHandler _DemandChannel.ValueChanged, AddressOf OnDemandValidate
            End If

            If Not (_DemandChannelIncrement Is Nothing) Then
                AddHandler _DemandChannelIncrement.ValueChanged, AddressOf OnDemandIncrementValidate
            End If

            If Not (_UpChannel Is Nothing) Then
                AddHandler _UpChannel.ValueChanged, AddressOf OnDemandCtrl
            End If

            If Not (_DownChannel Is Nothing) Then
                AddHandler _DownChannel.ValueChanged, AddressOf OnDemandCtrl
            End If

            If Not (_ResetChannel Is Nothing) Then
                AddHandler _ResetChannel.ValueChanged, AddressOf OnDemandReset
            End If

        End Sub
        Private Sub RemoveHandlers()

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf Me.OnClose_Test
            End If

            If Not (_DemandChannel Is Nothing) Then
                RemoveHandler _DemandChannel.ValueChanged, AddressOf OnDemandValidate
            End If

            If Not (_DemandChannelIncrement Is Nothing) Then
                RemoveHandler _DemandChannelIncrement.ValueChanged, AddressOf OnDemandIncrementValidate
            End If

            If Not (_UpChannel Is Nothing) Then
                RemoveHandler _UpChannel.ValueChanged, AddressOf OnDemandCtrl
            End If

            If Not (_DownChannel Is Nothing) Then
                RemoveHandler _DownChannel.ValueChanged, AddressOf OnDemandCtrl
            End If

            If Not (_ResetChannel Is Nothing) Then
                RemoveHandler _ResetChannel.ValueChanged, AddressOf OnDemandReset
            End If

        End Sub
        Private Sub OnDemandCtrl(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If CELProject.IsTrue(newVal) Then
                    If sender.Name = _UpChannel.Name.ToString Then
                        If _DemandChannel.Value.ToDouble() < 100 Then
                            _DemandChannel.Value = New Global.KineticaRT.Values.Double(_DemandChannel.Value.ToDouble() + Increment)
                        End If
                    ElseIf sender.Name = _DownChannel.Name.ToString Then
                        If _DemandChannel.Value.ToDouble() > 0 Then
                            _DemandChannel.Value = New Global.KineticaRT.Values.Double(_DemandChannel.Value.ToDouble() - Increment)
                        End If
                    End If
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnDemandCtrl: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
        '**********************
        '* OnDemandValidate() *
        '**********************
        Private Sub OnDemandValidate(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            'If the user delete de value in the input value and then hit enter we will have an exception, so assume miminum value in this case
            Try
                Dim TempValue As Integer = CInt(_DemandChannel.Value.ToDouble())
            Catch
                _DemandChannel.Value = New Global.KineticaRT.Values.Double(Minimum)
            End Try

            Try
                If _DemandChannel.Value.ToDouble() > Maximum Then
                    _DemandChannel.Value = New Global.KineticaRT.Values.Double(Maximum)
                ElseIf _DemandChannel.Value.ToDouble() < Minimum Then
                    _DemandChannel.Value = New Global.KineticaRT.Values.Double(Minimum)
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnDemandValidate: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
        '*******************************
        '* OnDemandIncrementValidate() *
        '*******************************
        Private Sub OnDemandIncrementValidate(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            'If the user delete de value in the input value and then hit enter we will have an exception, so assume miminum value in this case
            Try
                Dim TempValue As Integer = CInt(_DemandChannelIncrement.Value.ToDouble())
            Catch
                _DemandChannelIncrement.Value = New Global.KineticaRT.Values.Double(0.1)
            End Try

            Try
                If _DemandChannelIncrement.Value.ToDouble() > Maximum Then
                    _DemandChannelIncrement.Value = New Global.KineticaRT.Values.Double(Maximum)
                ElseIf _DemandChannelIncrement.Value.ToDouble() < 0.1 Then
                    _DemandChannelIncrement.Value = New Global.KineticaRT.Values.Double(0.1)
                End If

                Increment = _DemandChannelIncrement.Value.ToDouble()

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnDemandIncrementValidate: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
        '*******************
        '* OnDemandReset() *
        '*******************
        Private Sub OnDemandReset(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If
                If CELProject.IsTrue(newVal) Then
                    If _ResetValue < Minimum Then
                        _DemandChannel.Value = New Global.KineticaRT.Values.Double(Minimum)
                    ElseIf _ResetValue > Maximum Then
                        _DemandChannel.Value = New Global.KineticaRT.Values.Double(Maximum)
                    Else
                        _DemandChannel.Value = New Global.KineticaRT.Values.Double(_ResetValue)
                    End If

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnDemandReset: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


        End Sub


        '********************
        '* OnCloseTest()    *
        '********************
        Private Sub OnClose_Test(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(newVal) Then
                    RemoveHandlers()

                    'If Not (_DemandChannel Is Nothing) Then
                    '    _DemandChannel.Value = New Global.KineticaRT.Values.Double(Minimum) 'KineticaRT.Values.Values.Zero
                    'End If

                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("OnClose_Test: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called
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
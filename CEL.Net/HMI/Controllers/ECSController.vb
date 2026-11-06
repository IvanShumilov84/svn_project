Imports System
Imports KineticaRT.Channels
Imports KineticaRT.Values

Namespace CEL.HMI


    Public Class ECSController

        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()
        Private _Sequence As CEL.KineticaRT.Sequence.Sequence
        Private _ECSEnableChannel As IBooleanChannel
        Private _ECSDemandChannel As IChannel
        Private _ECSUpDemandChannel As IBooleanChannel
        Private _ECSDownDemandChannel As IBooleanChannel
        Private _ECSDisplay As Global.KineticaRT.Studio.Controls.Display
        Private _ECS_Max As Double
        Private _ECS_Min As Double
        Private _ECS_DefaultDemand As Double
        Private _ECS_Increment As Double
        Private _DemandChannelIncrement As IChannel = Nothing

        Private _ECSDemandMemorized As Double 'Used to keep the ECS demand when disabled

        Private _Enable As Boolean
        Private _Memorize As Boolean
        Private _UseIGVPositionAsFirstValue As Boolean
        Private _IGVpositionChannel As IChannel


        Public Property Enable() As Boolean
            Get
                Return _Enable
            End Get
            Set(ByVal value As Boolean)
                _Enable = value
                If _Enable = True Then
                    _ECSEnableChannel.Value = Values.True
                ElseIf _Enable = False Then
                    _ECSEnableChannel.Value = Values.False
                End If
            End Set
        End Property

        Public Property IGVpositionChannel() As IChannel
            Get
                Return _IGVpositionChannel
            End Get
            Set(ByVal value As IChannel)
                _IGVpositionChannel = value
            End Set
        End Property

        Public Property UseIGVPositionAsFirstValue() As Boolean
            Get
                Return _UseIGVPositionAsFirstValue
            End Get
            Set(ByVal value As Boolean)
                _UseIGVPositionAsFirstValue = value
            End Set
        End Property

        Public Property Memorize() As Boolean
            Get
                Return _Memorize
            End Get
            Set(ByVal value As Boolean)
                _Memorize = value
            End Set
        End Property


        Public Sub New(ByRef Sequence As CEL.KineticaRT.Sequence.Sequence, _
                                Optional ByVal ECS_Min As Double = 0, _
                                Optional ByVal ECS_Max As Double = 10, _
                                Optional ByVal ECS_DefaultDemand As Double = 0, _
                                Optional ByVal ECSEnableChannelName As String = "ECS_ENABLE", _
                                Optional ByVal ECSDemandChannelName As String = "ECS_DEMAND", _
                                Optional ByVal ECSUpDemandChannelName As String = "ECS_UP_DEMAND", _
                                Optional ByVal ECSDownDemandChannelName As String = "ECS_DOWN_DEMAND", _
                                Optional ByRef ECSDisplay As Global.KineticaRT.Studio.Controls.Display = Nothing, _
                                Optional ByVal ECS_Increment As Double = 1.0, _
                                Optional ByVal DemandChannelIncrementName As String = Nothing _
                                )


            _Sequence = Sequence

            _ECS_Max = ECS_Max
            _ECS_Min = ECS_Min
            _ECS_DefaultDemand = ECS_DefaultDemand

            _ECSDisplay = ECSDisplay

            _ECS_Increment = ECS_Increment

            Dim TempChannel As IChannel = Nothing

            _Sequence.InitializeChannel(TempChannel, ECSEnableChannelName)
            _ECSEnableChannel = CType(TempChannel, IBooleanChannel)

            _Sequence.InitializeChannel(_ECSDemandChannel, ECSDemandChannelName)

            _Sequence.InitializeChannel(TempChannel, ECSUpDemandChannelName)
            _ECSUpDemandChannel = CType(TempChannel, IBooleanChannel)

            _Sequence.InitializeChannel(TempChannel, ECSDownDemandChannelName)
            _ECSDownDemandChannel = CType(TempChannel, IBooleanChannel)

            If Not (_ECSDemandChannel Is Nothing) Then
                _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(ECS_DefaultDemand)
            End If
            If Not (DemandChannelIncrementName Is Nothing) Then
                _Sequence.InitializeChannel(_DemandChannelIncrement, DemandChannelIncrementName)
            End If

            _ECSDemandMemorized = ECS_DefaultDemand

            Me.AddHandlersECS()

            SetECSVisibleEnabled(False)

        End Sub

        Private Sub AddHandlersECS()
            AddHandler _ECSEnableChannel.ValueChanged, AddressOf OnECSEnable
            AddHandler _ECSDemandChannel.ValueChanged, AddressOf OnECSDemandValidate
            AddHandler _ECSDownDemandChannel.ValueChanged, AddressOf OnECSDemandCtrl
            AddHandler _ECSUpDemandChannel.ValueChanged, AddressOf OnECSDemandCtrl
        End Sub

        Private Sub RemoveHandlersECS()
            If Not (_ECSEnableChannel Is Nothing) Then
                RemoveHandler _ECSEnableChannel.ValueChanged, AddressOf OnECSEnable
            End If
            If Not (_ECSDemandChannel Is Nothing) Then
                RemoveHandler _ECSDemandChannel.ValueChanged, AddressOf OnECSDemandValidate
            End If
            If Not (_ECSDownDemandChannel Is Nothing) Then
                RemoveHandler _ECSDownDemandChannel.ValueChanged, AddressOf OnECSDemandCtrl
            End If
            If Not (_ECSUpDemandChannel Is Nothing) Then
                RemoveHandler _ECSUpDemandChannel.ValueChanged, AddressOf OnECSDemandCtrl
            End If
        End Sub


        '***********************
        '* OnECSDemandCtrl() *
        '***********************
        Private Sub OnECSDemandCtrl(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If CELProject.IsTrue(newVal) Then
                If sender.Name = _ECSUpDemandChannel.Name.ToString Then
                    If _ECSDemandChannel.Value.ToDouble() < _ECS_Max Then
                        If Not _DemandChannelIncrement Is Nothing Then
                            _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(_ECSDemandChannel.Value.ToDouble() + _DemandChannelIncrement.Value.ToDouble())
                        Else
                            _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(_ECSDemandChannel.Value.ToDouble() + _ECS_Increment)
                        End If
                    End If
                ElseIf sender.Name = _ECSDownDemandChannel.Name.ToString Then
                    If _ECSDemandChannel.Value.ToDouble() > _ECS_Min Then
                        If Not _DemandChannelIncrement Is Nothing Then
                            _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(_ECSDemandChannel.Value.ToDouble() - _DemandChannelIncrement.Value.ToDouble())
                        Else
                            _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(_ECSDemandChannel.Value.ToDouble() - _ECS_Increment)
                        End If

                    End If
                End If
            End If
        End Sub


        '***************************
        '* OnECSDemandValidate() *
        '***************************
        Private Sub OnECSDemandValidate(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            'If the user delete de value in the input value and then hit enter we will have an exception, so assume miminum value in this case
            Try
                Dim TempValue As Integer = CInt(_ECSDemandChannel.Value.ToDouble())
            Catch
                _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(_ECS_DefaultDemand)
            End Try

            If _ECSDemandChannel.Value.ToDouble() > _ECS_Max Then
                _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(_ECS_Max)
            ElseIf _ECSDemandChannel.Value.ToDouble() < _ECS_Min Then
                _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(_ECS_Min)
            End If

        End Sub

        '******************
        '* OnECSEnable() *
        '******************
        Private Sub OnECSEnable(ByVal sender As IChannel, ByVal newVal As Value)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If CELProject.IsTrue(newVal) Then
                If Me.Memorize = True Then
                    _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(_ECSDemandMemorized)
                ElseIf Me._UseIGVPositionAsFirstValue = True Then
                    _ECSDemandChannel.Value = Me._IGVpositionChannel.Value
                ElseIf Me.Memorize = False Then
                    _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(_ECS_DefaultDemand)
                End If
                SetECSVisibleEnabled(True)
            Else
                If Me.Memorize = True Then
                    _ECSDemandMemorized = _ECSDemandChannel.Value.ToDouble()
                End If
                _ECSDemandChannel.Value = New Global.KineticaRT.Values.Double(_ECS_DefaultDemand)
                SetECSVisibleEnabled(False)
            End If
        End Sub
        'Use Delegate for Cross Thread Operation
        Private Delegate Sub SetECSVisibleEnabledDelegate(ByVal value As Boolean)
        Private Sub SetECSVisibleEnabled(ByVal value As Boolean)

            If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                Exit Sub 'Sort de cette fonction si l'archive est en playback
            End If

            If (Me._ECSDisplay.InvokeRequired) Then
                Me._ECSDisplay.BeginInvoke(New SetECSVisibleEnabledDelegate(AddressOf Me.SetECSVisibleEnabled), value)
            Else
                _ECSDisplay.Enabled = value
            End If
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free managed resources when explicitly called
                    RemoveHandlersECS()
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
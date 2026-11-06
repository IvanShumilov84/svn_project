Imports System
Imports System.Windows.Forms
Imports System.Threading

Imports KineticaRT.Channels
Imports KineticaRT.Values


Namespace CEL.HMI

    Public Class PreviousValueReturnController
        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _DemandChannel As IChannel = Nothing
        Private _CloseValveChannel As IChannel = Nothing

        Private _MemorizeValue As Double = 0.0

        Public Sub New(ByVal DemandChannelName As String, _
                        ByVal CloseValveChannelName As String)

            If Not (DemandChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_DemandChannel, DemandChannelName)
            End If

            If Not (CloseValveChannelName Is Nothing) Then
                CELProject.InitializeChannelCallBack(_CloseValveChannel, CloseValveChannelName)
            End If

            Me.AddHandlers()

        End Sub

        Private Sub AddHandlers()

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                AddHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf Me.OnClose_Test
            End If

            If Not (_CloseValveChannel Is Nothing) Then
                AddHandler _CloseValveChannel.ValueChanged, AddressOf OnBtnValveChange
            End If

        End Sub

        Private Sub RemoveHandlers()

            If Not (CELProject.CLOSE_TEST_BTN Is Nothing) Then
                RemoveHandler CELProject.CLOSE_TEST_BTN.ValueChanged, AddressOf Me.OnClose_Test
            End If

            If Not (_CloseValveChannel Is Nothing) Then
                RemoveHandler _CloseValveChannel.ValueChanged, AddressOf OnBtnValveChange
            End If

        End Sub

        Private Sub OnBtnValveChange(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If CELProject.IsTrue(newVal) Then

                    _MemorizeValue = _DemandChannel.Value.ToDouble()

                    _DemandChannel.Value = Values.Zero

                Else

                    _DemandChannel.Value = New Global.KineticaRT.Values.Double(_MemorizeValue)

                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("OnBtnValveChange: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '********************
        '* OnCloseTest()    *
        '********************
        Private Sub OnClose_Test(ByVal sender As IChannel, ByVal newVal As Value)
            Try

                If CELProject.IsTrue(CELProject.ARCHIVE_PLAY.Value) Then
                    Exit Sub 'Sort de cette fonction si l'archive est en playback
                End If

                If CELProject.IsTrue(newVal) Then
                    RemoveHandlers()

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
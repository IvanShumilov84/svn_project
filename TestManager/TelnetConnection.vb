
' minimalistic telnet implementation
' conceived by Tom Janssens on 2007/06/06  for codeproject
'
' Modified by PPB for QNX connection
'
' 1. Commented Password Management as root reach prompt automatically
' http://www.corebvba.be



Imports System.Collections.Generic
Imports System.Text
Imports System.Net.Sockets

Namespace MinimalisticTelnet
    Enum Verbs
        WILL = 251
        WONT = 252
        [DO] = 253
        DONT = 254
        IAC = 255
    End Enum

    Enum Options
        SGA = 3
    End Enum

    Class TelnetConnection
        Private tcpSocket As TcpClient

        Private TimeOutMs As Integer = 100

        Public Sub New(Hostname As String, Port As Integer)

            tcpSocket = New TcpClient(Hostname, Port)
        End Sub

        Public Function Login(Username As String, Password As String, LoginTimeOutMs As Integer) As String
            Dim oldTimeOutMs As Integer = TimeOutMs
            TimeOutMs = LoginTimeOutMs
            Dim s As String = Read()
            If Not s.TrimEnd().EndsWith(":") Then
                Throw New Exception("Failed to connect : no login prompt")
            End If
            WriteLine(Username)

            's += Read()
            'If Not s.TrimEnd().EndsWith(":") Then
            '    Throw New Exception("Failed to connect : no password prompt")
            'End If
            'WriteLine(Password)

            s += Read()
            TimeOutMs = oldTimeOutMs
            Return s
        End Function

        Public Sub WriteLine(cmd As String)
            Write(cmd & Convert.ToString(vbLf))
        End Sub

        Public Sub Write(cmd As String)
            If Not tcpSocket.Connected Then
                Return
            End If
            Dim buf As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(cmd.Replace(vbNullChar & "xFF", vbNullChar & "xFF" & vbNullChar & "xFF"))
            tcpSocket.GetStream().Write(buf, 0, buf.Length)
        End Sub

        Public Function Read() As String
            If Not tcpSocket.Connected Then
                Return Nothing
            End If
            Dim sb As New StringBuilder()
            Do
                ParseTelnet(sb)
                System.Threading.Thread.Sleep(TimeOutMs)
            Loop While tcpSocket.Available > 0
            Return sb.ToString()
        End Function

        Public ReadOnly Property IsConnected() As Boolean
            Get
                Return tcpSocket.Connected
            End Get
        End Property

        Private Sub ParseTelnet(sb As StringBuilder)
            While tcpSocket.Available > 0
                Dim input As Integer = tcpSocket.GetStream().ReadByte()
                Select Case input
                    Case -1
                        Exit Select
                    Case CInt(Verbs.IAC)
                        ' interpret as command
                        Dim inputverb As Integer = tcpSocket.GetStream().ReadByte()
                        If inputverb = -1 Then
                            Exit Select
                        End If
                        Select Case inputverb
                            Case CInt(Verbs.IAC)
                                'literal IAC = 255 escaped, so append char 255 to string
                                sb.Append(inputverb)
                                Exit Select
                            Case CInt(Verbs.[DO]), CInt(Verbs.DONT), CInt(Verbs.WILL), CInt(Verbs.WONT)
                                ' reply to all commands with "WONT", unless it is SGA (suppres go ahead)
                                Dim inputoption As Integer = tcpSocket.GetStream().ReadByte()
                                If inputoption = -1 Then
                                    Exit Select
                                End If
                                tcpSocket.GetStream().WriteByte(CByte(Verbs.IAC))
                                If inputoption = CInt(Options.SGA) Then
                                    tcpSocket.GetStream().WriteByte(If(inputverb = CInt(Verbs.[DO]), CByte(Verbs.WILL), CByte(Verbs.[DO])))
                                Else
                                    tcpSocket.GetStream().WriteByte(If(inputverb = CInt(Verbs.[DO]), CByte(Verbs.WONT), CByte(Verbs.DONT)))
                                End If
                                tcpSocket.GetStream().WriteByte(CByte(inputoption))
                                Exit Select
                            Case Else
                                Exit Select
                        End Select
                        Exit Select
                    Case Else
                        sb.Append(Microsoft.VisualBasic.ChrW(input))
                        Exit Select
                End Select
            End While
        End Sub
    End Class
End Namespace

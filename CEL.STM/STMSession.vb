'
' STMSession.vb
'
' LabVIEW STM communication implementation for .Net.
' Based on a Reference Design from the NI System Engineering Group.
' http://www.ni.com/example/31208/en/
' Ported from C++ to Vb.Net by Danny Lepage 
' lepage dot danny at cel dash aerospace dot ca
'
' Copyright (c) 2014 CEL Aerospace Test Equipment LTD.
' Copyright (c) 2014 Dallas Airmotive Inc.
'

Namespace STM


    ''' <summary>
    ''' Represents the new data received.
    ''' </summary>
    Public Class NewDataReceivedEventArgs
        Inherits System.EventArgs


        ''' <summary>
        ''' Initializes a new instance of the NewDataRecievedEventArgs class using the specified meta name and data.
        ''' </summary>
        ''' <param name="metaName">The name of the message.</param>
        ''' <param name="data">The data associated with the message.</param>
        ''' <remarks></remarks>
        Public Sub New(ByRef metaName As String, ByRef data As System.Collections.Generic.IReadOnlyList(Of Byte))
            m_metaName = metaName
            m_data = data
        End Sub


        ''' <summary>
        ''' Gets the meta name associated with the received message.
        ''' </summary>
        ''' <value>A string value that describes the name of the message that caused the event.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property MetaName As String
            Get
                Return m_metaName
            End Get
        End Property


        ''' <summary>
        ''' Gets the data of associated with the received message.
        ''' </summary>
        ''' <value>The list of bytes that represents the data of the received message.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Data As System.Collections.Generic.IReadOnlyCollection(Of Byte)
            Get
                Return m_data
            End Get
        End Property


        Private m_metaName As String
        Private m_data As System.Collections.Generic.IReadOnlyList(Of Byte)
    End Class


    ''' <summary>
    ''' Represents the the error event raised.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ErrorEventArgs
        Inherits System.EventArgs


        ''' <summary>
        ''' Initialise a new instance of the ErrorEventArgs class with the specified error code.
        ''' </summary>
        ''' <param name="errorCode">The error code associated with the event.</param>
        ''' <remarks></remarks>
        Public Sub New(ByRef errorCode As Integer)
            m_errorCode = errorCode
        End Sub


        ''' <summary>
        ''' Get the error code associated with this event.
        ''' </summary>
        ''' <value>The error code of the event.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ErrorCode As Integer
            Get
                Return m_errorCode
            End Get
        End Property

        Private m_errorCode As Integer
    End Class


    ''' <summary>
    ''' Represents a STM communication session with a peer.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Session
        Implements System.IDisposable


        ''' <summary>
        ''' Occurs when the session has been successfully established.
        ''' </summary>
        ''' <remarks></remarks>
        Public Event Connected As EventHandler


        ''' <summary>
        ''' Occurs when the session has been terminated.
        ''' </summary>
        ''' <remarks></remarks>
        Public Event Closed As EventHandler


        ''' <summary>
        ''' Occurs when there is a communication error.
        ''' </summary>
        ''' <remarks></remarks>
        Public Event [Error] As EventHandler(Of ErrorEventArgs)


        ''' <summary>
        ''' Occurs when the Meta Data has been received.
        ''' </summary>
        ''' <remarks></remarks>
        Public Event MetaDataReceived As EventHandler


        ''' <summary>
        ''' Occurs when new data is received.
        ''' </summary>
        ''' <remarks></remarks>
        Public Event NewDataReceived As EventHandler(Of NewDataReceivedEventArgs)


        ''' <summary>
        ''' Return the array of meta data associated to this session.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property MetaData As String()
            Get
                Return m_metaData
            End Get
        End Property

        ''' <summary>
        ''' Establishes an STM connection to a remote host. The host is specified by an IP address and a port number.
        ''' </summary>
        ''' <param name="host">The name of the remote host.</param>
        ''' <param name="port">The port number of the remote host.</param>
        ''' <remarks></remarks>
        Public Sub Connect(host As String, port As UInt16)
            m_socket = New System.Net.Sockets.Socket( _
                System.Net.Sockets.AddressFamily.InterNetwork, _
                System.Net.Sockets.SocketType.Stream, _
                System.Net.Sockets.ProtocolType.Tcp)
            m_recvBuf.Clear()
            m_metaDefined = False
            m_socket.BeginConnect(host, port, New System.AsyncCallback(AddressOf ConnectCallback), Me)
        End Sub


        ''' <summary>
        ''' Disconnect the STM connection to a remote host.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Disconnect()
            If Not IsNothing(m_socket) Then
                If m_socket.Connected Then
                    m_socket.Shutdown(Net.Sockets.SocketShutdown.Both)
                End If
            End If
        End Sub

        Public ReadOnly Property IsConnected As Boolean
            Get
                If Not IsNothing(m_socket) Then
                    Return m_socket.Connected
                Else
                    Return False
                End If
            End Get
        End Property

        ''' <summary>
        ''' Release all resources used by the current instance of the <see cref="STM.Session">Session</see> class.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Dispose() Implements IDisposable.Dispose

            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub


        ''' <summary>
        ''' Send a message to the remote host.
        ''' </summary>
        ''' <param name="msgMeta">The message name to be sent.</param>
        ''' <param name="msgData">An array of type <see cref="System.Byte">Byte</see> that contains the message data.</param>
        ''' <remarks></remarks>
        Public Sub SendMsg(msgMeta As String, msgData() As Byte)
            Dim theList As System.Collections.Generic.List(Of System.ArraySegment(Of Byte)) = _
                New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))
            theList.Add(New System.ArraySegment(Of Byte)(msgData))

            SendMsg(msgMeta, theList)
        End Sub


        ''' <summary>
        ''' Send a message to the remote host.
        ''' </summary>
        ''' <param name="msgMeta">The message name to be sent</param>
        ''' <param name="msgData">A list of <see cref="System.ArraySegment(Of T)">ArraySegment(Of T)</see>s of type <see cref="System.Byte">Byte</see> that contains the message data.</param>
        ''' <remarks></remarks>
        Public Sub SendMsg(msgMeta As String, msgData As System.Collections.Generic.IList(Of System.ArraySegment(Of Byte)))

            If m_metaDefined = True Then
                Dim id As UShort = 0
                Dim buffer As System.Collections.Generic.List(Of System.ArraySegment(Of Byte)) = _
                    New System.Collections.Generic.List(Of System.ArraySegment(Of Byte))

                If GetIDFromName(msgMeta, id) Then
                    Dim totalMsgCount As Integer = 0
                    Dim socketError As System.Net.Sockets.SocketError

                    For Each segment As System.ArraySegment(Of Byte) In msgData
                        totalMsgCount += segment.Count
                    Next
                    STM.Convert.Flatten(totalMsgCount + 2, buffer)

                    STM.Convert.Flatten(id, buffer) 'Append meta ID
                    buffer.AddRange(msgData) 'Append msgData

                    Try
                        m_socket.BeginSend(buffer, Net.Sockets.SocketFlags.None, socketError, New System.AsyncCallback(AddressOf SendCallback), Me)
                    Catch e As ObjectDisposedException
                        'The socket has been closed
                        OnClosed(New System.EventArgs())
                    End Try
                Else
                    Throw New System.ArgumentException("Unknow meta data name '" + msgMeta + "'")
                End If
            End If
        End Sub


        ''' <summary>
        ''' Releases the unmanaged resources used by the <see cref="STM.Session">Session</see>, and optionally disposes of the managed resources.
        ''' </summary>
        ''' <param name="disposing">true to release both managed and unmanaged resources;false to releases only unmanaged resources.</param>
        ''' <remarks></remarks>
        Protected Overridable Sub Dispose(disposing As Boolean)
            If m_disposed Then
                Return
            End If

            If disposing Then
                If m_socket IsNot Nothing Then
                    m_socket.Close()
                    m_socket.Dispose()
                End If
            End If
            m_disposed = True
        End Sub


        ''' <summary>
        ''' Raises the <see cref="STM.Session.Closed">Closed</see> event.
        ''' </summary>
        ''' <param name="e">An <see cref="System.EventArgs">EventArgs</see> that contains the event data.</param>
        ''' <remarks></remarks>
        Protected Overridable Sub OnClosed(e As System.EventArgs)
            RaiseEvent Closed(Me, e)
        End Sub


        ''' <summary>
        ''' Raises the <see cref="STM.Session.Connected">Connected</see> event.
        ''' </summary>
        ''' <param name="e">An <see cref="System.EventArgs">EventArgs</see> that contains the event data.</param>
        ''' <remarks></remarks>
        Protected Overridable Sub OnConnected(e As System.EventArgs)
            RaiseEvent Connected(Me, e)
        End Sub


        ''' <summary>
        ''' Raises the <see cref="STM.Session.[Error]">Error</see> event.
        ''' </summary>
        ''' <param name="e">An <see cref="System.EventArgs">EventArgs</see> that contains the event data.</param>
        ''' <remarks></remarks>
        Protected Overridable Sub OnError(e As ErrorEventArgs)
            RaiseEvent Error(Me, e)
        End Sub


        ''' <summary>
        ''' Raises the <see cref="STM.Session.MetaDataReceived">MetaDataReceived</see> event.
        ''' </summary>
        ''' <param name="e">An <see cref="System.EventArgs">EventArgs</see> that contains the event data.</param>
        ''' <remarks></remarks>
        Protected Overridable Sub OnMetaDataReceived(e As System.EventArgs)
            RaiseEvent MetaDataReceived(Me, e)
        End Sub


        ''' <summary>
        ''' Raises the <see cref="STM.Session.NewDataReceived">NewDataReceived</see> event.
        ''' </summary>
        ''' <param name="e">A <see cref="STM.NewDataReceivedEventArgs">NewDataReceivedEventArgs</see> that contains the event data.</param>
        ''' <remarks></remarks>
        Protected Overridable Sub OnNewDataReceived(e As NewDataReceivedEventArgs)
            RaiseEvent NewDataReceived(Me, e)
        End Sub


        ''' <summary>
        ''' The <see cref="System.AsyncCallback">AsyncCallback</see> delegate called by <see cref="System.Net.Sockets.Socket.BeginConnect">Socket.BeginConnect</see>.
        ''' </summary>
        ''' <param name="ar">An <see cref="System.IASyncResult">IAsyncResult</see> that references the asynchronous connection.</param>
        ''' <remarks></remarks>
        Private Shared Sub ConnectCallback(ByVal ar As System.IAsyncResult)
            Dim session As STM.Session = CType(ar.AsyncState, STM.Session)

            Try
                session.m_socket.EndConnect(ar)
                session.OnConnected(New System.EventArgs)
                session.m_readBufSize = session.m_socket.ReceiveBufferSize
                ReDim session.m_readBuf(session.m_readBufSize - 1)
                session.m_socket.BeginReceive(session.m_readBuf, 0, session.m_readBufSize, 0, _
                                              New AsyncCallback(AddressOf ReceiveCallback), session)
            Catch e As System.Net.Sockets.SocketException
                session.OnError(New ErrorEventArgs(e.ErrorCode))
            End Try
        End Sub


        ''' <summary>
        ''' The <see cref="System.AsyncCallback">AsyncCallback</see> delegate called by <see cref="System.Net.Sockets.Socket.BeginReceive">Socket.BeginReceive</see>.
        ''' </summary>
        ''' <param name="ar">An <see cref="System.IASyncResult">IAsyncResult</see> that references the asynchronous connection.</param>
        ''' <remarks></remarks>
        Private Shared Sub ReceiveCallback(ByVal ar As System.IAsyncResult)
            Dim socketError As System.Net.Sockets.SocketError
            Dim session As STM.Session = CType(ar.AsyncState, STM.Session)

            Dim bytesRead As Integer = session.m_socket.EndReceive(ar, socketError)

            If (bytesRead > 0) Then
                session.m_recvBuf.AddRange(New System.ArraySegment(Of Byte)(session.m_readBuf, 0, bytesRead))
                If session.ProcessMsg() Then
                    If session.m_socket.Connected Then
                        Try
                            session.m_socket.BeginReceive(session.m_readBuf, 0, session.m_readBufSize, 0,
                                      New AsyncCallback(AddressOf ReceiveCallback), session)
                        Catch ex As Exception
                            session.m_socket.Close()
                            session.OnClosed(New System.EventArgs())
                        End Try
                    Else
                        session.m_socket.Close()
                        session.OnClosed(New System.EventArgs())
                    End If
                Else
                    session.m_socket.Close()
                    session.OnClosed(New System.EventArgs())
                End If
            ElseIf bytesRead = 0 Then
                'Other end closed the connection
                session.m_socket.Close()
                session.OnClosed(New EventArgs())
            End If
        End Sub


        ''' <summary>
        ''' The <see cref="System.AsyncCallback">AsyncCallback</see> delegate called by <see cref="System.Net.Sockets.Socket.BeginSend">Socket.BeginSend</see>.
        ''' </summary>
        ''' <param name="ar">An <see cref="System.IASyncResult">IAsyncResult</see> that references the asynchronous connection.</param>
        ''' <remarks></remarks>
        Private Shared Sub SendCallback(ByVal ar As System.IAsyncResult)
            Dim errorCode As Net.Sockets.SocketError
            Dim session As STM.Session = CType(ar.AsyncState, STM.Session)

            Try
                If Not (session Is Nothing) Then
                    Dim bytesSent As Integer = session.m_socket.EndSend(ar, errorCode)
                End If
            Catch
                'Other end closed the connection
                session.m_socket.Close()
                session.OnClosed(New EventArgs())
            End Try

        End Sub


        ''' <summary>
        ''' Get the Meta ID from the Meta Name.
        ''' </summary>
        ''' <param name="name">The Meta Name of the meta id to retrieve.</param>
        ''' <param name="id">The meta id retrieve if successful.</param>
        ''' <returns>True on success. False if the Meta Name does not exist for this session.</returns>
        ''' <remarks></remarks>
        Private Function GetIDFromName(name As String, ByRef id As UShort) As Boolean
            Dim found As Boolean = False
            For id = 0 To CUShort(m_metaData.GetUpperBound(0))
                If m_metaData(id) = name Then
                    found = True
                    Exit For
                End If
            Next
            Return found
        End Function


        ''' <summary>
        ''' Decode the meta data contained in the byte buffer.
        ''' </summary>
        ''' <param name="data">The byte buffer containing meta data to be decoded.</param>
        ''' <param name="position">The position in the byte buffer where decoding should start.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function DecodeMeta(data As System.Collections.Generic.List(Of Byte), position As Integer) As Boolean
            Dim MetaElementLen As Integer = 0

            m_numMeta = STM.Convert.IntUnflatten(data, position)    ' How many elements are in the Meta Data
            ReDim m_metaData(m_numMeta - 1)             ' Allocated memory for the Meta Data

            position += 4

            For index As Integer = 0 To m_numMeta - 1
                MetaElementLen = STM.Convert.IntUnflatten(data, position)
                position += 4
                m_metaData(index) = System.Text.Encoding.ASCII.GetString(data.GetRange(position, MetaElementLen).ToArray())
                position += MetaElementLen
            Next

            m_metaDefined = True
            Return True
        End Function


        ''' <summary>
        ''' Process any remaining complete messages.
        ''' </summary>
        ''' <remarks></remarks>
        Private Function ProcessMsg() As Boolean
            Dim result As Boolean = True
            Dim dataSize, metaindex As Integer
            Try
                While (m_recvBuf.Count() >= 4)

                    dataSize = STM.Convert.IntUnflatten(m_recvBuf, 0)

                    If (m_recvBuf.Count >= dataSize + 4) Then
                        If (Not m_metaDefined) Then
                            If (DecodeMeta(m_recvBuf, 4)) Then
                                m_recvBuf.RemoveRange(0, dataSize + 4)
                            End If
                            OnMetaDataReceived(New System.EventArgs)
                        Else
                            metaindex = STM.Convert.UShortUnflatten(m_recvBuf, 4)
                            If metaindex <= m_metaData.GetUpperBound(0) AndAlso metaindex >= 0 Then
                                m_msgMeta = m_metaData(metaindex)
                                m_msgData = m_recvBuf.GetRange(6, dataSize - 2)
                                m_recvBuf.RemoveRange(0, dataSize + 4)
                                OnNewDataReceived(New NewDataReceivedEventArgs(m_msgMeta, m_msgData))
                            Else
                                result = False
                                System.Diagnostics.Trace.WriteLine(String.Format("STMSession.ProcessMsg(): Invalid metaindex ({0})", metaindex))
                                Exit While
                            End If
                        End If
                    Else
                        Exit While
                    End If
                End While
            Catch ex As Exception
                result = False
                System.Diagnostics.Trace.WriteLine(String.Format("STMSession.ProcessMsg(): Exception '{0}'", ex.Message))
            End Try
            Return result
        End Function


        Private m_disposed As Boolean = False
        Private m_readBufSize As Integer
        Private m_metaData() As String
        Private m_metaDefined As Boolean = False
        Private m_msgData As System.Collections.Generic.List(Of Byte) = New System.Collections.Generic.List(Of Byte)
        Private m_msgMeta As String
        Private m_numMeta As Integer
        Private m_readBuf() As Byte
        Private m_recvBuf As System.Collections.Generic.List(Of Byte) = New System.Collections.Generic.List(Of Byte)
        Private m_socket As System.Net.Sockets.Socket
    End Class
End Namespace


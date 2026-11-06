'
' Created by SharpDevelop.
' User: Logiciel
' Date: 2005-07-17
' Time: 21:45
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System
Imports System.Runtime.Serialization
Imports System.Security.Permissions

Class StreamClient
	Inherits KineticaRT.Object
	
	Public Property IP As String
		Get
			Return Me.serverIP
		End Get
		Set (ByVal value As String)
			If (Not Me.serverIP Is value) Then
				Me.serverIP = value
				Me.OnPropertyChanged("IP")
			End If
		End Set
	End Property
	
	Public Property Port As String
		Get
			Return Me.serverPort
		End Get
		Set (ByVal value As String)
			If (Not Me.serverPort Is value) Then
				Me.serverPort = value
				Me.OnPropertyChanged("Port")
			End If
		End Set
	End Property
	
	Public Overrides Sub Connect()
		MyBase.Connect()
		SyncLock Me
            If Me.Connected Then
                Return
            End If
            Me.Connected = True
		End SyncLock
	End Sub

	Public Overrides Sub Disconnect()
		SyncLock Me
            If Me.Connected Then
                Me.Connected = False
            End If
		End SyncLock
		MyBase.Disconnect()
	End Sub

	Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
		MyBase.New(info, context)
		If (info Is Nothing) Then
			Throw New ArgumentNullException("info")
		End If
		Try 
			Me.serverIP = info.GetString("ServerIP")
			Me.serverPort = info.GetString("ServerPort")
		Catch exception1 As  SerializationException
		End Try
	End Sub

	<SecurityPermission(SecurityAction.Demand, SerializationFormatter:=True)> _
	Public Overrides Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext)
		If (info Is Nothing) Then
			Throw New ArgumentNullException("info")
		End If
		MyBase.GetObjectData(info, context)
		info.AddValue("ServerIP", Me.serverIP)
		info.AddValue("ServerPort", Me.serverPort)
	End Sub


	Private serverIP As String
	Private serverPort As String
End Class

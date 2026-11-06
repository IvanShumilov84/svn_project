imports System.IO

Namespace CEL
	Public Module TestSession

		Sub CreateEngineFolders(EngineModel As String, SerialNumber As String, BuildNumber As String, WorkOrder As String)
			Dim FolderName As String

			FolderName = GetLogFolderName(EngineModel, SerialNumber, BuildNumber, WorkOrder)
			If (Directory.Exists(FolderName) = FALSE) Then
				Directory.CreateDirectory(FolderName)
			End If
			FolderName = GetArchiveFolderName(EngineModel, SerialNumber, BuildNumber, WorkOrder)
			If (Directory.Exists(FolderName) = FALSE) Then
				Directory.CreateDirectory(FolderName)
			End If
			FolderName = GetPerfFolderName(EngineModel, SerialNumber, BuildNumber, WorkOrder)
			If (Directory.Exists(FolderName) = FALSE) Then
				Directory.CreateDirectory(FolderName)
			End If	
		End Sub

		Function GetBaseFolderName(EngineModel As String, SerialNumber As String, BuildNumber As String, WorkOrder As String) As String
			Dim BaseFolderName As String
			
			BaseFolderName = "C:\" + EngineModel
			If (SerialNumber <> "") Then
				BaseFolderName+= "\SN_" + SerialNumber
				If (BuildNumber <> "") Then
					BaseFolderName+= "\" + BuildNumber
				End If

				If (WorkOrder <> "") Then
					BaseFolderName+= "\WO_" + WorkOrder
				End If
			End If
			GetBaseFolderName = BaseFolderName
		End Function

		Function GetArchiveFolderName(EngineModel As String, SerialNumber As String, BuildNumber As String, WorkOrder As String) As String
			Dim BaseFolderName As String = GetBaseFolderName(EngineModel, SerialNumber, BuildNumber, WorkOrder)

			GetArchiveFolderName = BaseFolderName + "\Archive\"
		End Function

		Function GetLogFolderName(EngineModel As String, SerialNumber As String, BuildNumber As String, WorkOrder As String) As String
			Dim BaseFolderName As String = GetBaseFolderName(EngineModel, SerialNumber, BuildNumber, WorkOrder)

			GetLogFolderName = BaseFolderName + "\Log\"
		End Function

		Function GetPerfFolderName(EngineModel As String, SerialNumber As String, BuildNumber As String, WorkOrder As String) As String
			Dim BaseFolderName As String = GetBaseFolderName(EngineModel, SerialNumber, BuildNumber, WorkOrder)

			GetPerfFolderName = BaseFolderName + "\Perf\"
		End Function

		Public Enum TestMgrCmd
			OOPEN		= 1
			LLOAD		= 2
			IINITIALIZE	= 3
			RRUN		= 4
			CCLOSE		= 5
		End Enum

		Public Enum TestMgrStatus
			IDLING		= -999
			OPENING		= 1
			OPENED		= 2
			LOADING		= 3
			LOADED		= 4
			INITIALIZING	= 5
			INITIALIZED	= 6
			RUNNING		= 7
			CLOSING		= 8
			CLOSED		= 9
		End Enum

	End Module
End Namespace
	
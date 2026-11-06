Imports System
Imports System.Collections
Imports System.IO
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Reflection
Imports System.Collections.Generic
Imports KineticaRT
Imports KineticaRT.Channels
'Imports KineticaRT.Studio.Application
Imports KineticaRT.Sequence
Imports KineticaRT.Values
Imports System.Data
Imports System.Data.OleDb
Imports Microsoft.VisualBasic
Imports Microsoft.Office.Interop
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Imports CEL.EngUnit
Imports CEL.Library
Imports CEL.Data.BusinessObjects

Imports EntitySpaces.Interfaces
Imports EntitySpaces.Loader
Imports KineticaRT.Studio.Controls
Imports CEL.HMI.ExportInformationElement


'Imports EntitySpaces.Profiler

Namespace CEL.HMI

    ' <System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Unrestricted:=True)> _
    Public NotInheritable Class CELDAS
        '
        ' Shared members are 'eagerly initialized', that is,
        ' immediately when class is loaded for the first time.
        ' .Net guarantees thread safety for static initialization.
        '
        Private Shared ReadOnly _Instance As New CELDAS()
        Private Shared _LogPerfSynch As CEL.LogPerfSynch
        Private Shared _databaseConnection As OleDbConnection

        Public ArincCtrl As HMI.ARINC


        Public ChannelList As New Dictionary(Of String, String)
        'Public DicEngUnitName As New Collections.Generic.Dictionary(Of Integer, String)
        'Public DicEngUnitDisplayName As New Collections.Generic.Dictionary(Of Integer, String)

        Public DicArchiveChannelList As New Collections.Generic.Dictionary(Of String, Integer) 'used to keep channels that will be recorded and their recorder Index
        Dim effectiveUnitToUse As String
        Dim channelToUse As String
        Dim unitInImperial As String
        Dim targetDisplayUnit As String

        Public Enum eMeasurementUnit
            Imperial
            Metric
            SI
        End Enum


        Public Shared ReadOnly Property Instance() As CELDAS
            Get
                Return _Instance
            End Get
        End Property


        Private _measurementUnitInUse As eMeasurementUnit = eMeasurementUnit.Imperial    ' By default if no specific measurement unit is used, Imperial measurement units are used

        Public Property MeasurementUnitInUse() As eMeasurementUnit
            Get
                Return _measurementUnitInUse
            End Get
            Set(ByVal value As eMeasurementUnit)
                _measurementUnitInUse = value
            End Set
        End Property

        Private _measurementUnitNameInUse As String = String.Empty

        Public Property MeasurementUnitNameInUse() As String
            Get
                Return _measurementUnitNameInUse
            End Get
            Set(ByVal value As String)
                _measurementUnitNameInUse = value
            End Set
        End Property



        Private Sub New()
            ProcessCmdLineArguments()

            'Load up application configuration file
            UsingConfigurationManager.GetAppConfiguration(AppConfigFileName)

            _EngineSession = New EngineSessionInformation()



            ''Setup database connection
            If _DefaultConnectionName = "" Then
                _DefaultConnectionName = esConfigSettings.ConnectionInfo.Default '"ACCESSDynamic"
            End If


#If EXCEL_CULTUREINFO_ISSUE Then
            Dim OldCultureInfo As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
            'Change the Culture Info  BUG with Thread Excel Application
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
#End If
            '_ExcelApplication = New Microsoft.Office.Interop.Excel.Application
            '_ExcelApplication.DisplayAlerts = False
            '_ExcelApplication.Visible = False
            '_ExcelApplication.Caption = System.Guid.NewGuid.ToString.ToUpper

            NotifyIcon = New System.Windows.Forms.NotifyIcon
            AddHandler NotifyIcon.BalloonTipClicked, AddressOf OnNotifyIconBalloonTipClicked

            Dim strNotifyIconPath As String = _
                Application.StartupPath & Path.DirectorySeparatorChar & "Images" & Path.DirectorySeparatorChar & "NotifyIcon.ico"
            If Directory.Exists(Path.GetDirectoryName(strNotifyIconPath)) Then
                If File.Exists(strNotifyIconPath) Then
                    _Icon = New System.Drawing.Icon(strNotifyIconPath)
                    NotifyIcon.Icon = _Icon
                    NotifyIcon.Visible = True
                End If
            End If
            If _LogPerfSynch Is Nothing Then
                _LogPerfSynch = CEL.LogPerfSynch.Instance
            End If
        End Sub
        Public Shared Sub OnNotifyIconBalloonTipClicked() Handles NotifyIcon.Click

            Try

                If Not IsNothing(NotifyIcon.Tag) Then
                    If NotifyIcon.Tag.ToString.ToLower.EndsWith("xlsx") Then
                        Dim filename As String = NotifyIcon.Tag.ToString
                        If File.Exists(filename) Then 'Open EXCEL
                            'Process.Start("excel.exe", filename)
                            Dim excelProcess As Process = New Process()

                            excelProcess.StartInfo.FileName = "excel.exe"
                            excelProcess.StartInfo.Arguments = """" + filename + """"
                            excelProcess.Start()

                        End If
                    Else
                        Dim directoryPath As String = NotifyIcon.Tag.ToString
                        If Directory.Exists(directoryPath) Then 'Open EXPLORER
                            Process.Start("explorer.exe", directoryPath)
                        End If
                    End If

                End If
                NotifyIcon.Tag = Nothing
            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnNotifyIconBalloonTipClicked" + ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub
        Public Shared ReadOnly Property ExcelApplication As Microsoft.Office.Interop.Excel.Application
            Get
                If IsNothing(_ExcelApplication) Then
                    _ExcelApplication = New Microsoft.Office.Interop.Excel.Application()
                    _ExcelApplication.DisplayAlerts = False
                    _ExcelApplication.Visible = False
                    _ExcelApplication.Caption = System.Guid.NewGuid.ToString.ToUpper
                End If

                Return _ExcelApplication
            End Get
        End Property


        Public AppExecutablePath As String = System.Windows.Forms.Application.ExecutablePath
        Public AppStartupPath As String = System.Windows.Forms.Application.StartupPath

        Public Shared WithEvents NotifyIcon As System.Windows.Forms.NotifyIcon
        Private Shared _Icon As System.Drawing.Icon

        Public Enum eFlowMass
            LbByMin
            KgByMin
            KgBySec
        End Enum

        Public Enum eLength
            Inch
            Centimeter
        End Enum

        Public Enum ePressure
            PSI
            Kilopascal
            InchesOfMercury
            InchesH2O
        End Enum

        Public Enum eTapArrangement
            Corner
            DandD_2
            Flange
        End Enum

        Public Enum eTemperature
            Celsius
            Fahrenheit
        End Enum

        Public Enum eTest_Mgr_Cmd
            Open = 1
            Load = 2
            Initialize = 3
            Run = 4
            Close = 5
            Idle = -99
        End Enum

        Public Enum eTest_Mgr_Status
            Opening = 1
            Opened = 2
            Loading = 3
            Loaded = 4
            Initializing = 5
            Initialized = 6
            Running = 7
            Closing = 8
            Closed = 9
            Idling = -999
            ErrorOnRunning = -4
        End Enum

        Public Enum eTest_Mgr_Sub_Cmd
            Sub_Idle = 40
            Xfer_Coeff = 41
            Calib = 42
            Req_Xfer_Coeff = 43
            Req_Channel_Speed = 44
            Speed_Channel_Set = 45
            CCAS_Setup = 46
        End Enum

        Public Enum eTest_Mgr_Sub_Status
            Sub_Idling = 0
            Coeff_Xfered = 71
            Calibrating = 72
            Speed_Channel_Forced = 73
            Speed_Channel_Requested = 74
            CCAS_Setup_Done = 75
            Coeff_Xfer_Failed = 76
        End Enum

        'Sequence Delegate / CallBack
        Public Delegate Function GetChannelFPtr(ByVal ChannelName As String) As IChannel
        Public GetChannelCallBack As GetChannelFPtr

        Public Delegate Function GetObjectFPtr(ByVal ObjectName As String) As Object 'As System.Windows.Forms.Control 'Object
        Public GetObjectCallBack As GetObjectFPtr

        Public Delegate Function InitializeChannelFPtr(ByRef Channel As IChannel, ByVal ChannelName As String) As Boolean
        'Public Delegate Function InitializeChannelFPtr(ByRef Channel As Object, ByVal ChannelName As String) As Boolean
        Public InitializeChannelCallBack As InitializeChannelFPtr

        Public Delegate Function InitializeObjectFPtr(ByRef Channel As Object, ByVal ChannelName As String) As Boolean
        Public InitializeObjectCallBack As InitializeObjectFPtr


        Public Sub SetCallBackFunctions(ByVal GetChannelCallBackPtr As GetChannelFPtr, _
                                        ByVal GetObjectCallBackPtr As GetObjectFPtr, _
                                        ByVal InitializeChannelCallBackPtr As InitializeChannelFPtr, _
                                        ByVal InitializeObjectCallBackPtr As InitializeObjectFPtr)
            GetChannelCallBack = GetChannelCallBackPtr
            GetObjectCallBack = GetObjectCallBackPtr
            InitializeChannelCallBack = InitializeChannelCallBackPtr
            InitializeObjectCallBack = InitializeObjectCallBackPtr
        End Sub

        Public Function CreateCollection(ByVal collectionName As String) As Global.KineticaRT.ChannelCollection

            CreateCollection = Nothing

            Dim channelsInfo As Global.KineticaRT.Studio.Application.Project.ChannelsInfo

            'Add Collection to Project
            Dim MyPath As String = CurDir()
            File.Delete(MyPath + "\" + collectionName + ".ksc")

            channelsInfo = Me.HMIProject.GetChannelsInfo(collectionName)
            If Not (channelsInfo Is Nothing) Then
                Me.HMIProject.RemoveChannels(channelsInfo)
            End If

            Me.HMIProject.AddChannelCollection("", collectionName, True)
            'CreateCollection = GetObject(collectionName) 'Cette ligne plante...
            CreateCollection = CType(Me.GetObjectCallBack.Invoke(collectionName), Global.KineticaRT.ChannelCollection)

        End Function

        Public Function RefreshExplorer(ByVal collectionName As String) As Boolean

            Dim channelsInfo As Global.KineticaRT.Studio.Application.Project.ChannelsInfo

            'Refresh Explorer			
            channelsInfo = Me.HMIProject.GetChannelsInfo(collectionName)
            If Not IsNothing(channelsInfo) Then
                Me.HMIProject.UpdateChannels(channelsInfo)
            End If

        End Function

#Region " Logging and Performance "


        Private Function GetSqlQuery() As String

            Dim i As Integer = 0
            Dim selectString As String = ""
            selectString += " (SELECT  EngineModel, ChannelName As Name, Description, Format, DisplayUnit As Units, Category As Log_Report_Categorie, OrderInCategory As Log_Report_Order_In_Categorie , '0' As Mean_Enabled"
            selectString += " FROM Log_Report_Detail"
            If PWCEECPartNumber IsNot Nothing Then
                selectString += " WHERE (ProjectID=" + Project.Number.ToString() + " AND EngineModel='" + Me.EngineModel + "' AND PWC_PN ='" + PWCEECPartNumber + " ' AND Category<>'') "
            Else
                selectString += " WHERE (ProjectID=" + Project.Number.ToString() + " AND EngineModel='" + Me.EngineModel + " ' AND Category<>'') "
            End If

            selectString += " ORDER BY OrderInCategory)"
            Return selectString

        End Function


        '*******************
        '* CreateLogFile() *
        '*******************
        Public Delegate Function CreateCustomHeaderLogFilePtr(ByRef ExcelWorksheet As Excel.Worksheet, ByRef CurrentRowIndex As Integer, ByRef CurrentColumnIndex As Integer) As Boolean
        Private RowIndex_Start_Log As Integer 'TODO: ici était globale a la séquence, mais ici créé localement temporairement
        Public Sub CreateLogFile(ByVal CreateCustomHeaderCallBack As CreateCustomHeaderLogFilePtr, ByVal UUT_Model As String, Optional ByVal LogTitle As String = Nothing)

            Dim Categorie_Name As String
            Dim Cmd As OleDbCommand
            Dim ColumnIndex As Integer
            Dim ExcelSheets As Excel.Sheets
            Dim ExcelWorkbook As Excel.Workbook
            Dim ExcelWorksheet As Excel.Worksheet
            Dim ExcelFirstWorkSheet As Excel.Worksheet
            Dim Filename As String
            Dim Reader As OleDbDataReader
            Dim RowIndex As Integer
            Dim SelectString As String
            Dim TestCellName As String = "Unknown"
            Dim Var1 As IInfoChannel

            Dim chanName As String

            Try

                'Create Log Report
                ExcelWorkbook = ExcelApplication.Workbooks.Add

                'The following gets the Worksheets collection
                ExcelSheets = ExcelWorkbook.Worksheets
                'Need to Find dynamically the name of the sheet : in the french version of Excel, there is no "Sheet1"...
                ExcelFirstWorkSheet = CType(ExcelWorkbook.Sheets(1), Microsoft.Office.Interop.Excel.Worksheet)
                Dim CurrentSheetName As String = ExcelFirstWorkSheet.Name
                ExcelWorksheet = CType(ExcelSheets.Item(CurrentSheetName), Microsoft.Office.Interop.Excel.Worksheet)

                SelectString = "SELECT * FROM Log_Report WHERE [EngineModel] = " + Chr(39) + UUT_Model + Chr(39) + " AND ProjectID = " & Me.Project.Number & " AND NOT([Position] = 0) ORDER BY [Position]"

                Cmd = New OleDbCommand(SelectString, Me.Database.Connection)
                Reader = Cmd.ExecuteReader()

                ColumnIndex = 1
                RowIndex = 2

                While (Reader.Read())
                    Var1 = CType(Me.GetChannelCallBack.Invoke(Reader.Item("Header").ToString()), IInfoChannel)
                    If Var1 Is Nothing Then
                        clsTopMostMessageBox.Show("ERROR: Log Variable creation is Nothing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    'TODO: Sylvain: J'ai disable les warnings Late bounnd resolution; runtime errors could occur dans le projet en ignorant les warning BC42017 => trouver une meilleure solution

                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = Var1.Description + ":"
                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Font.Bold = True
                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).WrapText = True
                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Borders(Excel.XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlHairline
                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Borders(Excel.XlBordersIndex.xlEdgeRight).Weight = Excel.XlBorderWeight.xlHairline
                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Borders(Excel.XlBordersIndex.xlEdgeTop).Weight = Excel.XlBorderWeight.xlHairline
                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Borders(Excel.XlBordersIndex.xlEdgeLeft).Weight = Excel.XlBorderWeight.xlHairline

                    ColumnIndex += 2
                    If ColumnIndex Mod 7 = 0 Then
                        RowIndex += 1
                        ColumnIndex = 1
                    End If
                End While
                Reader.Close()

                ColumnIndex = 1
                RowIndex += 3

                RowIndex_Start_Log = RowIndex

                CreateCustomHeaderCallBack.Invoke(ExcelWorksheet, RowIndex, ColumnIndex)

                SelectString = GetSqlQuery()

                Cmd = New OleDbCommand(SelectString, Me.Database.Connection)
                Reader = Cmd.ExecuteReader()

                Categorie_Name = ""
                ColumnIndex = 1
                RowIndex += 3

                While (Reader.Read())
                    chanName = Reader.Item("Name").ToString().ToUpper()
                    targetDisplayUnit = String.Empty

                    If Reader.Item("Log_Report_Categorie").ToString() <> Categorie_Name Then
                        Categorie_Name = Reader.Item("Log_Report_Categorie").ToString()

                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = Categorie_Name
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Font.Bold = True
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Interior.Pattern = Excel.XlPattern.xlPatternGray16

                        RowIndex += 1
                    End If

                    If Reader.Item("Units").ToString() & "" = "" Then
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = Reader.Item("Description").ToString()
                    Else
                        effectiveUnitToUse = LookupForActualUnitInLogReport(Reader.Item("Units").ToString(), MeasurementUnitInUse, unitInImperial, targetDisplayUnit)
                        'ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = Reader.Item("Description").ToString() + " (" + Reader.Item("Units").ToString().Trim() + ")"
                        If targetDisplayUnit = String.Empty Then
                            ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = Reader.Item("Description").ToString() + " (" + Reader.Item("Units").ToString().Trim() + ")"
                        Else
                            ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = Reader.Item("Description").ToString() + " (" + targetDisplayUnit + ")"
                        End If
                    End If

                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Borders(Excel.XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlHairline
                    ExcelWorksheet.Cells(RowIndex, ColumnIndex).Borders(Excel.XlBordersIndex.xlEdgeRight).Weight = Excel.XlBorderWeight.xlHairline
                    RowIndex += 1
                End While
                Reader.Close()

                'Add some Style to the report
                ExcelWorksheet.Cells.ColumnWidth = 14.71
                ExcelWorksheet.Columns(1).ColumnWidth = 24.86

                ' For THY 13015, the test cell name is included in the footer of log report
                If Project.Number = 13015 Then
                    SelectString = "SELECT * FROM TestCellIdentification"

                    Try
                        Cmd = New OleDbCommand(SelectString, Me.Database.Connection)
                        Reader = Cmd.ExecuteReader()
                        Reader.Read()
                        TestCellName = Reader.Item("TestCelNumber").ToString()
                        Reader.Close()

                    Catch ex As Exception
                        If Not Reader.IsClosed Then
                            Reader.Close()
                        End If
                    End Try

                End If

                Try

                    ExcelWorksheet.PageSetup.Zoom = 85
                    ExcelWorksheet.PageSetup.PrintTitleColumns = "$A:$A"

                    If LogTitle Is Nothing Then
                        ExcelWorksheet.PageSetup.CenterHeader = "&""Arial,Bold""&20&U" & UUT_Model & Chr(10) & "Log File"
                    Else
                        ExcelWorksheet.PageSetup.CenterHeader = "&""Arial,Bold""&20&U" & LogTitle & Chr(10) & "Log File"
                    End If

                    With ExcelWorksheet.PageSetup
                        If File.Exists(Report.LogReportTemplateCustomerLogo) Then
                            .LeftHeaderPicture.Filename = Report.LogReportTemplateCustomerLogo 'ICI l'image doit etre un full path file et non avec un path relatif  sinon ca lance une exception
                        Else
                            clsTopMostMessageBox.Show("CreateLogFile:  The file " + Report.LogReportTemplateCustomerLogo + " does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        .LeftHeaderPicture.Height = 40
                        .LeftHeaderPicture.Width = 200
                    End With

                    ExcelWorksheet.PageSetup.LeftHeader = "&G"
                    ExcelWorksheet.PageSetup.LeftFooter = "&8DAS manufactured by CEL Aerospace LTD, Canada (1) 450-442-9994" + Chr(10) + "www.cel-aerospace.ca"
                    ExcelWorksheet.PageSetup.CenterFooter = "&P of &N"

                    If Project.Number = 12917 Then
                        ExcelWorksheet.PageSetup.RightFooter = "CELDAS™ 200 Version " + Me.Project.Number.ToString() + vbCr + "BC21" 'BC21 Requested by Revima
                    Else
                        If Project.Number = 13015 Then
                            ExcelWorksheet.PageSetup.RightFooter = "CELDAS™ 200 Version " + Me.Project.Number.ToString() + Chr(13) + "Test Cell: " + TestCellName
                        Else
                            ExcelWorksheet.PageSetup.RightFooter = "CELDAS™ 200 Version " + Me.Project.Number.ToString()
                        End If
                    End If


                    ExcelWorksheet.PageSetup.TopMargin = ExcelApplication.InchesToPoints(1.25)

                Catch ex As Exception

                    clsTopMostMessageBox.Show("CELDAS.CreateLogFileError: This problem occurs when there are no printer drivers installed on your computer. " + ex.Message, "Excel Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                End Try

                'Filename = Me.Defaultpath + "PrjExt\Log_Report.xlsx"
                Filename = Report.LogReportTemplateFile

                File.Delete(Filename)

                ExcelWorkbook.SaveAs(Filename) 'ICI l'image doit etre un full path file et non avec un path relatif  sinon ca lance une exception
                ExcelWorkbook.Close()

            Catch ex As Exception
                'TODO Ici on a une exception entre master et slave parce les 2 applications sur un meme poste essaye tous les 2 de creer le log file
                clsTopMostMessageBox.Show("CELDAS.CreateLogFile: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
            End Try
        End Sub
        '***************
        '* DoLogging() *
        '***************
        Public Sub DoLogging(ByVal UpdateCustomHeaderCallBack As UpdateCustomHeaderLogFilePtr, ByVal isPerfEntry As Boolean, ByVal UUT_Model As String, Optional ByVal CreateNewFile As Boolean = False, Optional ByVal BackupPath As String = "")
            'Variable Declaration
            Dim appendOption As Boolean
            Dim dirs As String()
            Dim ExcelFirstWorkSheet As Excel.Worksheet
            Dim excelSheets As Excel.Sheets
            Dim excelWorkbook As Excel.Workbook = Nothing
            Dim excelWorksheet As Excel.Worksheet
            Dim exist As Boolean
            Dim filename As String
            Dim backupFilename As String = Nothing
            Dim newFilename As String
            Dim searchPath As String
            Dim workbookPath As String
            Dim pleaseWait As New PleaseWaitForm("Please wait while saving log data")

            Try

                searchPath = Me.EngineSession.LogFolder
                dirs = Directory.GetFiles(searchPath, "Log Report*")

                Array.Sort(dirs)


                If dirs.Length > 0 Then
                    filename = dirs(dirs.GetUpperBound(0))
                Else
                    filename = ""
                End If

                Dim fileInUse As Boolean = False

                exist = File.Exists(filename)

                If exist = False Then
                    New_Log_Required = True
                Else
                    fileInUse = FileAndFolderHelper.FileAlreadyInUse(filename)
                End If

                If Not fileInUse Then
                    If Not New_Log_Required Then
                        filename = dirs(dirs.GetUpperBound(0))

                        'The following code opens an existing workbook
                        workbookPath = System.IO.Path.GetFullPath(filename)

                        Try
                            excelWorkbook = ExcelApplication.Workbooks.Open(workbookPath, 0, False, 5, "", "", False, _
                                                                                Excel.XlPlatform.xlWindows, "", True, False, 0, True, False, False)
                        Catch exception As System.Exception
                            System.Diagnostics.Trace.WriteLine("Opening Excel: " + exception.Message)
                            clsTopMostMessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try

                        If Not (excelWorkbook Is Nothing) Then
                            'The following gets the Worksheets collection
                            excelSheets = excelWorkbook.Worksheets

                            'Need to Find dynamically the name of the sheet : in the french version of Excel, there is no "Sheet1"...
                            ExcelFirstWorkSheet = CType(excelWorkbook.Sheets(1), Microsoft.Office.Interop.Excel.Worksheet)
                            Dim CurrentSheetName As String = ExcelFirstWorkSheet.Name
                            excelWorksheet = CType(excelSheets.Item(CurrentSheetName), Microsoft.Office.Interop.Excel.Worksheet)

                            appendOption = True
                            'Unlock Sheets
                            excelWorksheet.Unprotect(Me.Project.PerformanceReportsCellProtection)

                            pleaseWait.Show()
                            Me.UpdateLogFile(UpdateCustomHeaderCallBack, appendOption, excelWorksheet, isPerfEntry, UUT_Model)
                            pleaseWait.Hide()

                            excelWorksheet.Protect(Me.Project.PerformanceReportsCellProtection)

                            excelWorkbook.Save()

                            If BackupPath <> "" Then  'Optional for HS 
                                If Not My.Computer.FileSystem.DirectoryExists(BackupPath) Then
                                    My.Computer.FileSystem.CreateDirectory(BackupPath)
                                End If

                                excelWorkbook.SaveAs(BackupPath + backupFilename)
                            End If

                            excelWorkbook.Close()

                        End If
                    Else
                        filename = Me.Report.LogReportTemplateFile 'Me.CriticalHmiPath + "PrjExt\Log_Report.xlsx"

                        'The following code opens an existing workbook
                        workbookPath = System.IO.Path.GetFullPath(filename)

                        Try
                            excelWorkbook = ExcelApplication.Workbooks.Open(workbookPath, 0, False, 5, "", "", False, _
                                                                                Excel.XlPlatform.xlWindows, "", True, False, 0, True, False, False)
                        Catch exception As System.Exception
                            System.Diagnostics.Trace.WriteLine("Opening Excel: " + exception.Message)
                            clsTopMostMessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try

                        If Not (excelWorkbook Is Nothing) Then
                            'The following gets the Worksheets collection
                            excelSheets = excelWorkbook.Worksheets

                            'Need to Find dynamically the name of the sheet : in the french version of Excel, there is no "Sheet1"...
                            ExcelFirstWorkSheet = CType(excelWorkbook.Sheets(1), Microsoft.Office.Interop.Excel.Worksheet)
                            Dim CurrentSheetName As String = ExcelFirstWorkSheet.Name
                            excelWorksheet = CType(excelSheets.Item(CurrentSheetName), Microsoft.Office.Interop.Excel.Worksheet)

                            appendOption = False

                            excelWorksheet.Unprotect(Me.Project.PerformanceReportsCellProtection)

                            pleaseWait.Show()
                            Me.UpdateLogFile(UpdateCustomHeaderCallBack, appendOption, excelWorksheet, isPerfEntry, UUT_Model)
                            pleaseWait.Hide()

                            excelWorksheet.Protect(Me.Project.PerformanceReportsCellProtection)

                            newFilename = "Log Report " + Format(Now, "yyyy-MM-dd HH'h'mm'm'ss's'")
                            filename = Me.EngineSession.LogFolder + newFilename + ".xlsx"

                            excelWorkbook.SaveAs(filename)

                            If BackupPath <> "" Then 'Optional for HS 
                                If Not My.Computer.FileSystem.DirectoryExists(BackupPath) Then
                                    My.Computer.FileSystem.CreateDirectory(BackupPath)
                                End If
                                excelWorkbook.SaveAs(BackupPath + newFilename + ".xlsx")
                            End If

                            excelWorkbook.Close()


                        End If

                        New_Log_Required = False

                    End If

                    If Not isPerfEntry Then
                        NotifyIcon.Tag = filename 'Path.GetDirectoryName(filename)
                        NotifyIcon.ShowBalloonTip(3000, TechnologyName, "New log file successfully generated" + vbCrLf + "Click here to open file.", ToolTipIcon.Info)
                    End If

                Else
                    NotifyIcon.ShowBalloonTip(0, TechnologyName, "DataPoint cannot be recorded: The log file is already in use.  Please close Microsoft Excel and try again.", ToolTipIcon.Error)
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.DoLogging: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
            End Try

        End Sub
        '*******************
        '* UpdateLogFile() *
        '*******************
        Public Delegate Function UpdateCustomHeaderLogFilePtr(ByVal AppendOption As Boolean, ByVal IsPerfEntry As Boolean, ByRef ExcelWorksheet As Excel.Worksheet, ByRef CurrentRowIndex As Integer, ByRef CurrentColumnIndex As Integer) As Boolean
        Public Function UpdateLogFile(ByVal UpdateCustomHeaderCallBack As UpdateCustomHeaderLogFilePtr, ByVal AppendOption As Boolean, ByRef ExcelWorksheet As Excel.Worksheet, ByVal IsPerfEntry As Boolean, ByVal UUT_Model As String) As Boolean
            Dim Categorie_Name As String
            Dim Cmd As OleDbCommand
            Dim ColumnIndex As Integer
            Dim MaxPosition As Integer
            Dim Reader As OleDbDataReader
            Dim RowIndex As Integer
            Dim SelectString As String
            Dim Var1 As IChannel
            Dim Var2 As IChannel

            UpdateLogFile = False

            Try


                If AppendOption = False Then

                    SelectString = "SELECT * FROM Log_Report WHERE [EngineModel] = " + Chr(39) + UUT_Model + Chr(39) + " AND ProjectID = " & Me.Project.Number & " AND NOT([Position] = 0) ORDER BY [Position]"
                    Cmd = New OleDbCommand(SelectString, Me.Database.Connection)
                    Reader = Cmd.ExecuteReader()

                    ColumnIndex = 1
                    RowIndex = 2

                    While (Reader.Read())
                        Var1 = Me.GetChannelCallBack(Reader.Item("Header").ToString())
                        If Var1 Is Nothing Then
                            clsTopMostMessageBox.Show("ERROR: Log Variable creation is Nothing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        ExcelWorksheet.Cells(RowIndex, ColumnIndex + 1).NumberFormat = "@"
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex + 1).Value = Var1.Value.ToString()
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex + 1).Font.Bold = False
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex + 1).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex + 1).WrapText = True
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex + 1).Borders(Excel.XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlHairline
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex + 1).Borders(Excel.XlBordersIndex.xlEdgeRight).Weight = Excel.XlBorderWeight.xlHairline
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex + 1).Borders(Excel.XlBordersIndex.xlEdgeTop).Weight = Excel.XlBorderWeight.xlHairline
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex + 1).Borders(Excel.XlBordersIndex.xlEdgeLeft).Weight = Excel.XlBorderWeight.xlHairline

                        ColumnIndex += 2
                        If ColumnIndex Mod 7 = 0 Then
                            RowIndex += 1
                            ColumnIndex = 1
                        End If
                    End While
                    Reader.Close()
                End If

                SelectString = "SELECT Max([Position]) AS [Maximum] FROM [Log_Report] WHERE EngineModel = '" & UUT_Model & "' AND ProjectID = " & Me.Project.Number

                Cmd = New OleDbCommand(SelectString, Me.Database.Connection)

                Reader = Cmd.ExecuteReader()

                While (Reader.Read())
                    MaxPosition = CInt(Math.Round(CDbl(Reader.Item("Maximum").ToString()) / 3))
                End While
                Reader.Close()

                'RowIndex 	= MaxPosition + 2 + 2
                RowIndex = RowIndex_Start_Log


                UpdateCustomHeaderCallBack.Invoke(AppendOption, IsPerfEntry, ExcelWorksheet, RowIndex, ColumnIndex)


                SelectString = GetSqlQuery()

                Cmd = New OleDbCommand(SelectString, Me.Database.Connection)
                Reader = Cmd.ExecuteReader()

                Categorie_Name = ""
                RowIndex += 1
                'TODO ft Utiliser Entityspaces
                Dim nbOfErrors As Integer = 0
                While (Reader.Read())

                    effectiveUnitToUse = LookupForActualUnitInLogReport(Reader.Item("Units").ToString(), MeasurementUnitInUse, unitInImperial, targetDisplayUnit)

                    ' If effectiveUnitToUse Is empty, this means the unit specific as no coresponding unit to be converted into
                    ' We will then use the value of the channel directly as specified in the log_report_detail

                    If (effectiveUnitToUse <> String.Empty) Then

                        ' If the unit that was entered in the table log_report_detail And the engineering unit to use are the
                        ' same, no conversion is required (most likely because it's imperial measurement unit) the

                        If (effectiveUnitToUse = Reader.Item("Units").ToString()) Then
                            channelToUse = Reader.Item("Name").ToString()
                        Else
                            ' Perform the enginneering unit translation from the unit specified in log_report_detail into
                            ' the engineering unit specified by the user when starting the test. We will likely have to use
                            ' a different channel name from the one in the record.ChannelName

                            If Reader.Item("Name").ToString().Contains(unitInImperial) Then
                                ' The channel name Is defined with the display unit in the channel name. Replace the suffix portion of the channel name with 

                                channelToUse = Reader.Item("Name").ToString().Replace(unitInImperial.ToUpper(), effectiveUnitToUse.ToUpper())
                            Else
                                ' The record.ChannelName suffix doesn't match the record.DisplayUnit. Append the substitution
                                ' engineering unit to the channel name to get the channel name to use

                                channelToUse = Reader.Item("Name").ToString() + "_" + effectiveUnitToUse.ToUpper()
                            End If
                        End If
                    Else
                        channelToUse = Reader.Item("Name").ToString()
                    End If


                    If Reader.Item("Log_Report_Categorie").ToString() <> Categorie_Name Then
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Interior.Pattern = Excel.XlPattern.xlPatternGray16
                        Categorie_Name = Reader.Item("Log_Report_Categorie").ToString()
                        RowIndex += 1
                    End If

                    Var2 = Me.GetChannelCallBack("LOG_PERF_" + channelToUse)
                    If Var2 Is Nothing Then
                        nbOfErrors += 1
                        clsTopMostMessageBox.Show("CELDAS.UpdateLogFile: Channel not found:  LOG_PERF_" + channelToUse + " (Log Variable creation is Nothing) the logReport Must be Outdated and should reindexed", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If nbOfErrors > 10 Then
                            If clsTopMostMessageBox.Show("CELDAS.UpdateLogFile: The number of errors is too high.  Do you want to exit function?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
                                Exit While
                            End If
                            nbOfErrors = 0
                        End If
                    Else

                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).NumberFormat = "@"
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight

                        If TypeOf Var2.Value Is Global.KineticaRT.Values.Boolean Then
                            ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = Var2.Value.ToString()
                        Else
                            ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value = Var2.Value.ToString(Reader.Item("Format").ToString(), Nothing)
                        End If
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Borders(Excel.XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlHairline
                        ExcelWorksheet.Cells(RowIndex, ColumnIndex).Borders(Excel.XlBordersIndex.xlEdgeRight).Weight = Excel.XlBorderWeight.xlHairline
                    End If
                    RowIndex += 1
                End While
                Reader.Close()


                UpdateLogFile = True


            Catch ex As Exception
                clsTopMostMessageBox.Show("UpdateLogFile: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Private Function LookupForActualUnitInLogReport(ByVal displayUnit As String, ByVal engineeringUnitToDisplay As eMeasurementUnit, ByRef unitInImperial As String, ByRef targetDisplayUnit As String) As String

            esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
            Dim unitsConversionCollection As EngineeringUnitConversionCollection = New EngineeringUnitConversionCollection()
            unitsConversionCollection.es.Connection.Name = DefaultConnectionName
            unitsConversionCollection.Query.Where(unitsConversionCollection.Query.DisplayUnitImperial = displayUnit)
            unitsConversionCollection.Query.Load()

            If unitsConversionCollection.HasData Then

                unitInImperial = unitsConversionCollection.Item(0).UnitImperial
                ' When channel represents a differential unit, the unit usually contains a "D" at the end but in the channel name, the "D" is omitted.
                If (unitInImperial(unitInImperial.Length - 1) = "D" And unitsConversionCollection.Item(0).DisplayUnitImperial.Contains("(d)")) Or (unitInImperial.ToUpper() = "INH2OG") Then
                    unitInImperial = unitInImperial.Substring(0, unitInImperial.Length - 1)
                End If

                Select Case engineeringUnitToDisplay
                    Case eMeasurementUnit.Imperial
                        targetDisplayUnit = unitsConversionCollection.Item(0).DisplayUnitImperial
                        Return unitsConversionCollection.Item(0).UnitImperial

                    Case eMeasurementUnit.Metric
                        targetDisplayUnit = unitsConversionCollection.Item(0).DisplayUnitMetric
                        Return unitsConversionCollection.Item(0).UnitMetric

                    Case eMeasurementUnit.SI
                        targetDisplayUnit = unitsConversionCollection.Item(0).DisplayUnitSI
                        Return unitsConversionCollection.Item(0).UnitSI

                    Case Else
                        targetDisplayUnit = unitsConversionCollection.Item(0).DisplayUnitImperial
                        Return unitsConversionCollection.Item(0).UnitImperial
                End Select
            Else
                targetDisplayUnit = String.Empty
                Return String.Empty

            End If

        End Function


        Public Delegate Function UpdatePerformanceReportFPtr(ByVal AppendOption As Boolean, ByVal TestStepNumber As Integer, ByVal ProtectPassword As String, ByVal UnProtectPassword As String, ByRef ExcelWorkbook As Excel.Workbook) As Boolean
        Public Function ProcessPerformanceReport(ByVal SearchPath As String, ByVal TemplateFullFileName As String, ByVal UpdatePerformanceReportCallBack As UpdatePerformanceReportFPtr, Optional ByVal BackupPath As String = "") As Boolean
            'Variable Declaration
            Dim filename As String = ""
            Dim backupFilename As String
            Dim newFilename As String = ""
            Dim dirs As String()
            Dim exist As Boolean
            Dim workbookPath As String
            Dim excelWorkbook As Excel.Workbook = Nothing
            Dim appendOption As Boolean
            Dim newFileCreated As Boolean = False
            Dim ProtectPassword As String = ""
            Dim UnProtectPassword As String = ""

            Dim pleaseWait As New PleaseWaitForm("Please wait while saving performance report...")
            Try

                dirs = Directory.GetFiles(SearchPath, "*.xlsx")
                Array.Sort(dirs)

                'Special case for the PW980A, the perf directory contains other xlsx file that are not the performance file that we want.
                If dirs.Length > 0 Then
                    For i As Integer = dirs.GetUpperBound(0) To 0 Step -1
                        If Not (dirs(i).Contains("PW980A Q13")) Then
                            filename = dirs(i)
                            Exit For
                        End If
                    Next
                End If


                Dim fileInUse As Boolean = False

                'Check if the file exist, if not create one
                exist = File.Exists(filename)

                If exist = False Then
                    New_Perf_Required = True
                Else
                    fileInUse = FileAndFolderHelper.FileAlreadyInUse(filename)
                End If

                If Not fileInUse Then
                    Dim message As String = ""
                    If Not New_Perf_Required Then

                        '****************Open and Append to the the log Report the Last Performance Report Object****************'

                        ' The following code opens an existing workbook
                        workbookPath = System.IO.Path.GetFullPath(filename)

                        backupFilename = System.IO.Path.GetFileName(filename)

                        Try
                            excelWorkbook = ExcelApplication.Workbooks.Open(workbookPath, 0, False, 5, "", "", False, Excel.XlPlatform.xlWindows, "", True, False, 0, True, False, False)
                        Catch exception As System.Exception
                            System.Diagnostics.Trace.WriteLine("Opening Excel: " + exception.Message)
                            clsTopMostMessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try

                        If Not (excelWorkbook Is Nothing) Then

                            '****************Open the Last Full Log Report Object****************'
                            appendOption = True
                            ProtectPassword = Me.Project.PerformanceReportsCellProtection
                            UnProtectPassword = Me.Project.PerformanceReportsCellProtection

                            pleaseWait.Show()
                            If UpdatePerformanceReportCallBack.Invoke(appendOption, CType(Me.PERF_TEST_STEP_NB.Value.ToDouble, Integer), ProtectPassword, UnProtectPassword, excelWorkbook) = False Then
                                clsTopMostMessageBox.Show("Performance Report has generated an error.  The report will not be saved!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                excelWorkbook.Close()
                                Exit Function
                            End If
                            pleaseWait.Hide()

                            excelWorkbook.Save()

                            If BackupPath <> "" Then  'Optional for HS 
                                If Not My.Computer.FileSystem.DirectoryExists(BackupPath) Then
                                    My.Computer.FileSystem.CreateDirectory(BackupPath)
                                End If

                                excelWorkbook.SaveAs(BackupPath + backupFilename)
                            End If

                        End If
                        message = "Performance file successfully updated."
                    Else
                        workbookPath = TemplateFullFileName
                        Try
                            excelWorkbook = ExcelApplication.Workbooks.Open(workbookPath, 0, False, 5, "", "", False, Excel.XlPlatform.xlWindows, "", True, False, 0, True, False, False)
                        Catch exception As System.Exception
                            System.Diagnostics.Trace.WriteLine("Opening Excel: " + exception.Message)
                            clsTopMostMessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try

                        appendOption = False 'Creating New One
                        ProtectPassword = Me.Project.PerformanceReportsCellProtection
                        UnProtectPassword = Me.Database.Password

                        pleaseWait.Show()
                        If UpdatePerformanceReportCallBack.Invoke(appendOption, CType(Me.PERF_TEST_STEP_NB.Value.ToDouble, Integer), ProtectPassword, UnProtectPassword, excelWorkbook) = False Then
                            clsTopMostMessageBox.Show("Performance Report has generated an error.  The report will not be saved!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            'Create the new File
                            newFilename = Me.Test_Mgr_UUT_Model.Value.ToString() + " Performance" + Format(Now, " yyyy MM dd HH,mm,ss")
                            filename = SearchPath + newFilename + ".xlsx"
                            excelWorkbook.SaveAs(filename)

                            'NewFileCreated = True
                        End If

                        pleaseWait.Hide()

                        If BackupPath <> "" Then 'Optional for HS 
                            If Not My.Computer.FileSystem.DirectoryExists(BackupPath) Then
                                My.Computer.FileSystem.CreateDirectory(BackupPath)
                            End If
                            excelWorkbook.SaveAs(BackupPath + newFilename + ".xlsx")
                        End If





                        New_Perf_Required = False
                        message = "New performance file successfully generated."

                    End If
                    excelWorkbook.Close()
                    NotifyIcon.Tag = filename 'Path.GetDirectoryName(filename)

                    NotifyIcon.ShowBalloonTip(3000, TechnologyName, message + vbCrLf + "Click here to open file.", ToolTipIcon.Info)

                    ProcessPerformanceReport = True

                Else

                    NotifyIcon.ShowBalloonTip(0, TechnologyName, "DataPoint cannot be recorded: The report file is already in use.  Please close Microsoft Excel and try again.", ToolTipIcon.Error)

                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.ProcessPerformanceReport: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally

            End Try
        End Function

#End Region

        Private Sub OnViewCamera(ByVal sender As IChannel, ByVal newVal As Value)
            If IsTrue(newVal) Then

                'Start the camera by opening an Internet Explorer page
                Try
                    System.Diagnostics.Process.Start("IExplore.exe", "http://192.168.2.240")
                Catch exception As System.Exception
                    clsTopMostMessageBox.Show("Camera Start Button: " & exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End If

            VIEW_CAMERA_BTN.Value = Values.False
        End Sub

        Private Sub OnNewTest(ByVal sender As IChannel, ByVal newVal As Value)
            If (IsTrue(newVal)) Then

                If Project.Number = 12917 Then
                    If DialogResult.Yes = clsTopMostMessageBox.Show("Êtes-vous vraiment certain de vouloir faire un nouvel essai?", "Engine Perf/Log Rapports", MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
                        New_Log_Required = True
                        New_Perf_Required = True
                        New_Trim_Required = True

                        PERF_NEW_TEST_OPC.Value = Values.True
                    End If
                Else
                    If DialogResult.Yes = clsTopMostMessageBox.Show("Do you want to create new reports?", "Engine Perf/Log Reports", MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
                        New_Log_Required = True
                        New_Perf_Required = True
                        New_Trim_Required = True

                        PERF_NEW_TEST_OPC.Value = Values.True
                    End If
                End If

                NEW_TEST_BTN.Value = Values.False

            End If

        End Sub
        Private Sub OnTestMgr_STATUS(ByVal sender As IChannel, ByVal newVal As Value)

            If newVal.ToDouble > -999 And newVal.ToDouble < 0 Then
                clsTopMostMessageBox.Show("TestMgrStatus: " + sender.Name + " returning error " + newVal.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End Sub

        Private Sub OnViewReport(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                Dim xlWorkbook As Excel.Workbook = Nothing
                Dim Filename As String
                Dim File_Dialog As New OpenFileDialog()
                Dim WorkbookPath As String

                File_Dialog.Reset()
                File_Dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
                File_Dialog.FilterIndex = 1

                Select Case "True"
                    Case LOG_VIEW_REPORT_BTN.Value.ToString()
                        File_Dialog.Title = "View Log Report"
                        File_Dialog.InitialDirectory = Me.EngineSession.LogFolder 'EngineFolder + "\Log\"
                    Case PERF_VIEW_REPORT_BTN.Value.ToString()
                        File_Dialog.Title = "View Performance Report"
                        File_Dialog.InitialDirectory = Me.EngineSession.PerfFolder 'EngineFolder + "\Perf\"
                    Case LOG_PRINT_REPORT_BTN.Value.ToString()
                        File_Dialog.Title = "Print Log Report"
                        File_Dialog.InitialDirectory = Me.EngineSession.LogFolder 'EngineFolder + "\Log\"
                    Case PERF_PRINT_REPORT_BTN.Value.ToString()
                        File_Dialog.Title = "Print Performance Report"
                        File_Dialog.InitialDirectory = Me.EngineSession.PerfFolder 'EngineFolder + "\Perf\"

                    Case Else : Exit Sub
                End Select

                If File_Dialog.ShowDialog() = DialogResult.OK Then
                    Filename = File_Dialog.FileName
                Else
                    Filename = ""
                End If

                If Filename <> "" Then
                    If File.Exists(Filename) Then

                        If System.IO.Path.GetExtension(Filename).StartsWith(".xl") Then

                            'Don't prompt user to save
                            ExcelApplication.DisplayAlerts = False
                            ExcelApplication.Visible = False

                            ' The following code opens an existing workbook    
                            WorkbookPath = System.IO.Path.GetFullPath(Filename)
                            Try
                                xlWorkbook = ExcelApplication.Workbooks.Open(WorkbookPath, 0, True, 5, "", "", False, Excel.XlPlatform.xlWindows, "", False, False, 0, True, False, False)
                            Catch exception As System.Exception
                                System.Diagnostics.Trace.WriteLine("Opening Excel: " + exception.Message)
                                clsTopMostMessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try

                            Select Case "True"
                                Case PERF_VIEW_REPORT_BTN.Value.ToString(), LOG_VIEW_REPORT_BTN.Value.ToString()
                                    ExcelApplication.Visible = True  ' Makes Excel visible to the user.	
                                Case PERF_PRINT_REPORT_BTN.Value.ToString()
                                    PrintReport(xlWorkbook, Perf_Nb_Sheet_To_Print)
                                Case LOG_PRINT_REPORT_BTN.Value.ToString()
                                    PrintReport(xlWorkbook, 1)
                            End Select

                            'Note: we need the following line because without it we cannot open an excel after printing
                            ExcelApplication.Visible = True  ' Makes Excel visible to the user.
                        Else
                            ' Open using notepad
                            Process.Start("notepad.exe", Filename)
                        End If
                    End If
                End If

                Select Case "True"
                    Case LOG_VIEW_REPORT_BTN.Value.ToString()
                        LOG_VIEW_REPORT_BTN.Value = Values.False
                    Case PERF_VIEW_REPORT_BTN.Value.ToString()
                        PERF_VIEW_REPORT_BTN.Value = Values.False
                    Case LOG_PRINT_REPORT_BTN.Value.ToString()
                        LOG_PRINT_REPORT_BTN.Value = Values.False
                    Case PERF_PRINT_REPORT_BTN.Value.ToString()
                        PERF_PRINT_REPORT_BTN.Value = Values.False
                    Case Else : Exit Sub
                End Select

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnViewReport: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
        Private Sub PrintReport(ByVal xlWorkbook As Excel.Workbook, Optional ByVal NumberOfSheet As Integer = -1)
            Try
                If NumberOfSheet = -1 Then
                    xlWorkbook.PrintOut()
                Else
                    Dim i As Integer
                    For i = 1 To NumberOfSheet
                        xlWorkbook.Sheets(i).PrintOut()
                    Next
                End If

            Catch exception As System.Exception
                clsTopMostMessageBox.Show(exception.Message, "CELDAS.PrintReport", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Function FormSelectFile(ByVal Dirs As String(), Optional ByVal SelString As String = "View") As String
            Try
                ' Create an instance of the ListBox.
                Dim listBox1 As New System.Windows.Forms.ListBox()
                Dim FileSelected As String = ""
                Dim Result As DialogResult
                'Dim SearchPath		As String
                'Dim WorkbookPath 	As String
                Dim Form1 As New Form()
                Dim ButtonSel As New System.Windows.Forms.Button()
                Dim ButtonCancel As New System.Windows.Forms.Button()

                'Set the text of ButtonSel to View
                ButtonSel.Text = SelString
                ButtonSel.Location = New System.Drawing.Point(350, 425)
                ButtonSel.DialogResult = DialogResult.OK

                ButtonCancel.Text = "Cancel"
                'set the position based on the location of ButtonSel
                ButtonCancel.Location = New System.Drawing.Point(ButtonSel.Left - ButtonCancel.Width - 10, ButtonSel.Top)
                ButtonCancel.DialogResult = DialogResult.Cancel

                'Set the accept button of the form to ButtonSel
                Form1.AcceptButton = ButtonSel
                Form1.CancelButton = ButtonCancel

                Form1.Text = "Select File"
                Form1.Size = New System.Drawing.Size(700, 500)
                Form1.StartPosition = FormStartPosition.CenterParent
                Form1.MaximizeBox = False
                Form1.MinimizeBox = False
                Form1.FormBorderStyle = FormBorderStyle.FixedDialog
                Form1.SizeGripStyle = SizeGripStyle.Hide


                'Set the size and location of the ListBox.
                listBox1.Size = New System.Drawing.Size(650, 400)
                listBox1.Location = New System.Drawing.Point(25, 25)
                'Add the ListBox to the form.
                Form1.Controls.Add(listBox1)
                'Add the button to the form.
                Form1.Controls.Add(ButtonSel)
                Form1.Controls.Add(ButtonCancel)

                If Dirs.GetUpperBound(0) = -1 Then
                    Result = clsTopMostMessageBox.Show("No File found", "Log Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    FileSelected = ""
                Else

                    ' Shutdown the painting of the ListBox as items are added.
                    listBox1.BeginUpdate()
                    ' Loop through and add 50 items to the ListBox.
                    Dim x As Integer
                    For x = 0 To Dirs.GetUpperBound(0)
                        listBox1.Items.Add(Dirs(x))
                    Next x
                    ' Allow the ListBox to repaint and display the new items.
                    listBox1.EndUpdate()

                    If Form1.ShowDialog() = DialogResult.OK Then
                        'MessageBox.Show(listBox1.SelectedIndices(0).ToString())
                        If listBox1.SelectedItem IsNot Nothing Then
                            FileSelected = listBox1.SelectedItem.ToString()
                        Else
                            FileSelected = ""
                        End If

                        Form1.Dispose()
                    Else
                        FileSelected = ""
                        Form1.Dispose()
                    End If

                End If
                FormSelectFile = FileSelected

            Catch Ex As Exception
                FormSelectFile = ""
                clsTopMostMessageBox.Show("Performance.Engine_Sequence.FormSelectFile: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & Ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Public EngineWindowCollection As New List(Of sEngineWindowInfo)
        Public Structure sEngineWindowInfo
            Dim WindowName As String
            Dim OwnerName As String
            Dim EngineWindow As Global.KineticaRT.Studio.Window 'System.Windows.Forms.Form
            Dim MainScreen As Boolean 'Default screen visible at initialization
            Dim Master As Boolean 'Indicate if the screen is shown when the app is configured as Master
            Dim Slave As Boolean 'Indicate if the screen is shown when the app is configured as Slave
            Dim DefaultWindow As Boolean
        End Structure

        Private _engUnitCollection As EngineeringUnitCollection 'TODO a deplacer dans le business layer
        Public Function GetUnitNameByDisplayName(ByVal DisplayName As String) As String

            If IsNothing(_engUnitCollection) Then
                _engUnitCollection = New EngineeringUnitCollection()
                _engUnitCollection.LoadAll()
            End If

            For Each engUnit As EngineeringUnit In _engUnitCollection
                If engUnit.DisplayName = DisplayName Then
                    Return engUnit.Name
                End If
            Next
            Return "None"
        End Function
        Public Function GetUnitNameById(ByVal Id As Integer) As String 'TODO a deplacer dans le business layer

            If IsNothing(_engUnitCollection) Then
                _engUnitCollection = New EngineeringUnitCollection()
                _engUnitCollection.LoadAll()
            End If

            For Each engUnit As EngineeringUnit In _engUnitCollection
                If engUnit.Id = Id Then
                    Return engUnit.Name
                End If
            Next
            Return "None"
        End Function
        Public Function GetUnitDisplayNameById(ByVal Id As Integer) As String 'TODO a deplacer dans le business layer

            If IsNothing(_engUnitCollection) Then
                _engUnitCollection = New EngineeringUnitCollection()
                _engUnitCollection.LoadAll()
            End If

            For Each engUnit As EngineeringUnit In _engUnitCollection
                If engUnit.Id = Id Then
                    Return engUnit.DisplayName
                End If
            Next
            Return "None"
        End Function

        Public Sub InitializeAndConnectDefaultEngineWindows()

            Try

                EngineWindowCollection.Clear()

                'Ajout automatique du Welcome_Screen
                Dim windowInfo As New sEngineWindowInfo()

                windowInfo.WindowName = "Welcome_Screen"
                windowInfo.Master = True
                windowInfo.Slave = True
                windowInfo.DefaultWindow = True
                Me.InitializeObjectCallBack.Invoke(windowInfo.EngineWindow, windowInfo.WindowName)
                windowInfo.EngineWindow.Connect()
                windowInfo.EngineWindow.Width = 3840 '3824
                windowInfo.EngineWindow.Height = 1048 '1040
                windowInfo.EngineWindow.FormBorderStyle = FormBorderStyle.FixedSingle
                Me.EngineWindowCollection.Add(windowInfo)

                windowInfo.WindowName = "UserInputs"
                windowInfo.Master = True
                windowInfo.DefaultWindow = True
                Me.InitializeObjectCallBack.Invoke(windowInfo.EngineWindow, windowInfo.WindowName)
                windowInfo.EngineWindow.Connect()
                windowInfo.EngineWindow.FormBorderStyle = FormBorderStyle.FixedSingle
                Me.EngineWindowCollection.Add(windowInfo)

                windowInfo.WindowName = "Test_Password"
                windowInfo.Master = True
                windowInfo.DefaultWindow = True
                Me.InitializeObjectCallBack.Invoke(windowInfo.EngineWindow, windowInfo.WindowName)
                windowInfo.EngineWindow.Connect()
                windowInfo.EngineWindow.FormBorderStyle = FormBorderStyle.FixedSingle
                Me.EngineWindowCollection.Add(windowInfo)

                windowInfo.WindowName = "MeasurementUnitSelector"
                windowInfo.Master = True
                windowInfo.DefaultWindow = True
                Me.InitializeObjectCallBack.Invoke(windowInfo.EngineWindow, windowInfo.WindowName)
                windowInfo.EngineWindow.Connect()
                windowInfo.EngineWindow.FormBorderStyle = FormBorderStyle.FixedSingle
                Me.EngineWindowCollection.Add(windowInfo)


                If Me.DebugMode = True And Me.EngineModel <> "SYSTEM CHECK" Then
                    windowInfo.WindowName = "SystemCheck_Screen"
                    windowInfo.Master = True
                    windowInfo.DefaultWindow = False 'False = ce n'est pas un default windows pcq on peut le loader dans les choix moteurs
                    Me.InitializeObjectCallBack.Invoke(windowInfo.EngineWindow, windowInfo.WindowName)
                    windowInfo.EngineWindow.Connect()
                    windowInfo.EngineWindow.FormBorderStyle = FormBorderStyle.FixedSingle
                    Me.EngineWindowCollection.Add(windowInfo)
                End If

            Catch ex As Exception
                MsgBox("Error InitializeEngineWindows" + ex.Message)
            End Try


        End Sub
        Private _DefaultParentWindow As String = "" 'Cette fenetre est la fenetre par defaut sur lequel le focus doit revenir lors de la fermeture des autres fenetres ex. archive 
        Public Property DefaultParentWindow As String
            Get
                Return _DefaultParentWindow
            End Get
            Set(value As String)
                _DefaultParentWindow = value
            End Set
        End Property
        Public Sub InitializeAndConnectEngineWindows()
            Try

                For Each window As sEngineWindowInfo In Me.EngineWindowCollection
                    If window.WindowName = "SystemCheck_Screen" And Me.DebugMode = True And Me.EngineModel <> "SYSTEM CHECK" Then
                        window.EngineWindow.Connect() 'Deconnecte les autres fenetres pour eviter que des boutons ayant le meme nom entre interference.  Ex.  Un START_BTN a autoreturn avec un START_BTN d'un autre moteur avec type = Normal scrappe la logique du bouton.
                    ElseIf Not window.DefaultWindow Then
                        window.EngineWindow.Disconnect() 'Deconnecte les autres fenetres pour eviter que des boutons ayant le meme nom entre interference.  Ex.  Un START_BTN a autoreturn avec un START_BTN d'un autre moteur avec type = Normal scrappe la logique du bouton.
                    Else
                        window.EngineWindow.Connect()
                    End If
                Next

                Dim windowInfo As New sEngineWindowInfo()

                'Query Database
                esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                Dim hmiWindow As New UUTHMIWindowQuery("wdo")
                Dim engine As New UutQuery("eng")

                'strSQL = "SELECT Distinct(UUT_HMI_Window.HMI_Window_Name) As Window_Name, UUT_HMI_Window.MainScreen, UUT_HMI_Window.Master, UUT_HMI_Window.Slave FROM UUT INNER JOIN UUT_HMI_Window ON UUT.Model = UUT_HMI_Window.Model WHERE (UUT.Available)=True"

                Dim windowCollection As UUTHMIWindowCollection = New UUTHMIWindowCollection()
                windowCollection.es.Connection.Name = DefaultConnectionName
                windowCollection.Query.Where(windowCollection.Query.ProjectID = Me.Project.Number)
                windowCollection.Query.Where(windowCollection.Query.EngineModel = Me.EngineModel)
                windowCollection.Query.Where(windowCollection.Query.MeasurementUnitId = Me.MeasurementUnitInUse)
                windowCollection.Query.Where(windowCollection.Query.Unavailable = False)
                'windowCollection.Load(hmiWindow)
                windowCollection.Query.Load()

                For Each window As UUTHMIWindow In windowCollection

                    windowInfo = New sEngineWindowInfo()

                    windowInfo.WindowName = window.HMIWindowName
                    windowInfo.MainScreen = window.MainScreen.Value
                    windowInfo.Master = window.Master.Value
                    windowInfo.Slave = window.Slave.Value
                    Me.InitializeObjectCallBack.Invoke(windowInfo.EngineWindow, windowInfo.WindowName)

                    If Not IsNothing(windowInfo.EngineWindow) Then
                        windowInfo.EngineWindow.Connect()
                        windowInfo.EngineWindow.FormBorderStyle = FormBorderStyle.FixedSingle
                        If Not EngineWindowCollection.Contains(windowInfo) Then
                            EngineWindowCollection.Add(windowInfo)
                        End If
                    End If

                    If (Me.HierarchyType = eHierarchyType.Master And window.Master) Or (Me.HierarchyType = eHierarchyType.Slave And window.Slave) Then
                        If window.MainScreen = True And window.ParentWindowName = "" Then
                            Me.DefaultParentWindow = window.HMIWindowName
                        End If
                    End If
                Next
            Catch ex As Exception
                MsgBox("Error InitializeEngineWindows" + ex.Message)
            End Try
        End Sub

        Public Sub ActivateForm(ByVal FormName As String, ByVal OwnerName As String, ByVal Activate As Boolean, ByVal ConnectProject As Boolean)

            Try
                If (Activate = True) Then
                    Dim windowFound As Boolean = False
                    For Each window As CELDAS.sEngineWindowInfo In Me.EngineWindowCollection
                        If window.WindowName = FormName Then
                            If (Me.HierarchyType = eHierarchyType.Master And window.Master) Or (Me.HierarchyType = eHierarchyType.Slave And window.Slave) Then
                                If OwnerName <> "" Then
                                    'window.EngineWindow.Refresh()' //ft desactive parce ca trig un repaint du welcome screen non necessaire
                                    window.EngineWindow.BringToFront()

                                    For Each obj As CELDAS.sEngineWindowInfo In Me.EngineWindowCollection
                                        If obj.WindowName = OwnerName Then
                                            window.EngineWindow.Owner = obj.EngineWindow

                                            Exit For
                                        End If
                                    Next
                                End If

                                window.EngineWindow.Visible = True
                                window.EngineWindow.Show()

                                'window.EngineWindow.WindowState = FormWindowState.Normal
                                windowFound = True

                                If ConnectProject Then
                                    'The following line needs to be added because the child window does not
                                    'seems to connect properly to the APS2300 channel, so the displayed element
                                    'APS2300.XXX shows as not connected
                                    Dim HMIProject As Global.KineticaRT.Studio.Application.Project
                                    HMIProject = CType(Me.GetObjectCallBack.Invoke("Project"), Global.KineticaRT.Studio.Application.Project)
                                    HMIProject.Connect()
                                End If

                                Exit For

                            End If
                        End If
                    Next
                    If Me.EngineWindowCollection.Count = 0 Then 'TODO: logique pas bonne
                        clsTopMostMessageBox.Show("ERROR: " & FormName & " Window could not be found.", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                    If Not windowFound Then
                        clsTopMostMessageBox.Show("CELDAS.ActivateForm: The specified window was not found in the collection. Validate that the window is associated with this engine in table UUT_HMI_WINDOW. " + FormName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else 'Deactivate
                    For Each window As CELDAS.sEngineWindowInfo In Me.EngineWindowCollection
                        If window.WindowName = FormName Then
                            window.EngineWindow.Hide()
                            'window.EngineWindow.Refresh()' //ft desactive parce ca trig un repaint du welcome screen non necessaire
                        ElseIf window.WindowName = OwnerName Then
                            'window.EngineWindow.Refresh() //ft desactive parce ca trig un repaint du welcome screen non necessaire
                            window.EngineWindow.BringToFront()
                        End If

                        'If (i - 1) = WINDOW_NAME.GetUpperBound(0) Then 'TODO: logique pas bonne
                        '   clsTopMostMessageBox.Show("ERROR: " & FormName & " Window could not be found.", "CELDAS™ 200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'End If
                    Next
                End If

            Catch

            End Try

        End Sub

        Public Shared Sub ProcessCmdLineArguments()
            Dim arguments() As String = System.Environment.GetCommandLineArgs()

            _DataAcquisitionMode = eDataAcquisitionMode.RUN_FOR_TRUE
            _HierarchyType = eHierarchyType.Master
            If arguments.GetUpperBound(0) > 0 Then
                For Each argument As String In arguments
                    If argument.ToLower().Contains("/slave") Then
                        _HierarchyType = eHierarchyType.Slave
                    ElseIf argument.ToLower().Contains("/simulation") Then
                        _DataAcquisitionMode = eDataAcquisitionMode.SIMULATION
                    End If
                    If argument.Contains("/password=") Then
                        _UserDefaultPassword = argument.Split("="c)(1)
                    End If
                    If argument.Contains("/engine=") Then
                        _UserDefaultEngine = argument.Split("="c)(1)
                    End If
                    If argument.Contains("/connection=") Then
                        _DefaultConnectionName = argument.Split("="c)(1)
                    End If
                    If argument.Contains("/config=") Then
                        _AppConfigFileName = argument.Split("="c)(1)
                    End If
                    If argument.Contains("/debug") Then
                        _DebugMode = True
                    End If
                    If argument.Contains("/nopopup") Then
                        _NoPopupError = True
                    End If
                    If argument.Contains("/fast") Or argument.Contains("/fastloading") Then
                        _FastLoading = True
                    End If

                Next

            End If
        End Sub

        'Test_Mgr_Mode est liaison avec User_Interface->TM_In_Test_Mgr ==> Test_Mgr->In_Test_Mgr ==> et qui entre dans chacune des ressources
        'Ex.  Out_SCU_IOMgr.mode = 0 permet de laisser la ressource a Enabled mais qui n'ouvre pas le hardware (mode DEBUG pour le développement)
        'et lorsque Out_SCU_IOMgr.mode est 1 alors la ressource essayera de communiquer avec le hardware (mode NORMAL)
        Public Enum eDataAcquisitionMode
            SIMULATION = 0
            RUN_FOR_TRUE = 1
        End Enum
        Public Enum eHierarchyType
            Master = 0
            Slave = 1
            Independant = 2
        End Enum


        'CEL Internal developper tool
        Private Shared _UserDefaultPassword As String
        Public Property UserDefaultPassword() As String
            Get
                Return _UserDefaultPassword
            End Get
            Set(ByVal value As String)
                _UserDefaultPassword = value
            End Set
        End Property
        'CEL Internal developper tool
        Private Shared _DebugMode As Boolean
        Public Property DebugMode() As Boolean
            Get
                Return _DebugMode
            End Get
            Set(ByVal value As Boolean)
                _DebugMode = value
            End Set
        End Property
        'CEL Internal developper tool
        Private Shared _NoPopupError As Boolean
        Public Property NoPopupError() As Boolean
            Get
                Return _NoPopupError
            End Get
            Set(ByVal value As Boolean)
                _NoPopupError = value
            End Set
        End Property
        'CEL Internal developper tool
        Private Shared _UserDefaultEngine As String
        Public Property UserDefaultEngine() As String
            Get
                Return _UserDefaultEngine
            End Get
            Set(ByVal value As String)
                _UserDefaultEngine = value
            End Set
        End Property
        'CEL Internal developper tool
        Private Shared _DefaultConnectionName As String
        Public Property DefaultConnectionName() As String
            Get
                Return _DefaultConnectionName
            End Get
            Set(ByVal value As String)
                _DefaultConnectionName = value
            End Set
        End Property
        Private Shared _AppConfigFileName As String = ""
        Public Shared Property AppConfigFileName() As String
            Get
                Return _AppConfigFileName
            End Get
            Set(ByVal value As String)
                _AppConfigFileName = value
            End Set
        End Property
        Private _EngineFamility As String = String.Empty
        Public Property EngineFamily() As String
            Get
                If Not IsNothing(Test_Mgr_UUT_Family) Then
                    Return Test_Mgr_UUT_Family.Value.ToString
                Else
                    Return _EngineFamility
                End If
            End Get
            Set(value As String)
                _EngineFamility = value
                If Not IsNothing(Test_Mgr_UUT_Family) Then 'Le testmanager utilise cette proriété mais n'itialise pas le Test_Mgr_UUT_Model donc ca crashait
                    Test_Mgr_UUT_Family.ValueObject = value
                End If
            End Set
        End Property
        Private _EngineModel As String = String.Empty
        Public Property EngineModel() As String
            Get
                If Not IsNothing(Test_Mgr_UUT_Model) Then
                    Return Test_Mgr_UUT_Model.Value.ToString
                Else
                    Return _EngineModel
                End If
            End Get
            Set(value As String)
                _EngineModel = value
                If Not IsNothing(Test_Mgr_UUT_Model) Then  'Le testmanager utilise cette proriété mais n'itialise pas le Test_Mgr_UUT_Model donc ca crashait
                    Test_Mgr_UUT_Model.ValueObject = value
                End If
            End Set
        End Property

        Private Shared _FastLoading As Boolean
        Public Property FastLoading() As Boolean
            Get
                Return _FastLoading
            End Get
            Set(value As Boolean)
                _FastLoading = value
            End Set
        End Property

        Public ReadOnly Property EngineConfiguration() As EngineInformationItem
            Get
                'Par defaut on retourne la config propre au moteur, sinon on retourne cette de la famille
                Dim engineConfig As EngineInformationItem = UsingConfigurationManager.engineInfoCollection.Item(EngineModel)
                If IsNothing(engineConfig) Then
                    engineConfig = UsingConfigurationManager.engineInfoCollection.Item(EngineFamily)
                    If Not IsNothing(engineConfig) Then
                        Console.WriteLine("Engine configuration not found in app.config for engine model " + Me.EngineModel + " The configuration for family will be used by default " + Me.EngineFamily)
                    End If
                    Return engineConfig
                Else
                    Return engineConfig
                End If
            End Get
        End Property
        Private _PWCEECPartNumber As String
        Public Property PWCEECPartNumber As String
            Get
                Return _PWCEECPartNumber
            End Get
            Set(ByVal value As String)
                _PWCEECPartNumber = value
            End Set
        End Property

        Public ReadOnly Property EngineModelFormatted() As String
            Get
                Return EngineModel.Replace(" ", "_")
            End Get
        End Property
        Public Function FormatEngineModel(ByVal engineModel As String) As String
            Return engineModel.Replace(" ", "_")
        End Function





        Private Shared _HierarchyType As eHierarchyType
        Public Property HierarchyType() As eHierarchyType
            Get
                Return _HierarchyType
            End Get
            Set(ByVal value As eHierarchyType)
                _HierarchyType = value
            End Set
        End Property
        Private Shared _DataAcquisitionMode As eDataAcquisitionMode
        Public Property DataAcquisitionMode() As eDataAcquisitionMode
            Get
                Return _DataAcquisitionMode
            End Get
            Set(ByVal value As eDataAcquisitionMode)
                _DataAcquisitionMode = value
            End Set
        End Property

        Public ReadOnly Property LogPerfSynch() As CEL.LogPerfSynch
            Get
                Return _LogPerfSynch
            End Get
        End Property

        Private _CELTechnologie As String
        Public Property TechnologyName() As String
            Get
                Return _CELTechnologie
            End Get
            Set(ByVal value As String)
                _CELTechnologie = value
            End Set
        End Property
        Private _ChannelSelectorFormModuleAvailable As Boolean
        Public Property ChannelSelectorFormModuleAvailable() As Boolean
            Get
                Return _ChannelSelectorFormModuleAvailable
            End Get
            Set(ByVal value As Boolean)
                _ChannelSelectorFormModuleAvailable = value
            End Set

        End Property
        Public _WorkBenchProjectExtensionPath As String
        Public Property WorkBenchProjectExtensionPath() As String
            Get
                Return _WorkBenchProjectExtensionPath
            End Get
            Set(ByVal value As String)
                _WorkBenchProjectExtensionPath = value
            End Set
        End Property

        Private _ArchiveDestinationPath As String 'ft ici ca doit etre fait dans la config par moteur
        Public Property ArchiveDestinationPath() As String
            Get
                Return _ArchiveDestinationPath
            End Get
            Set(ByVal value As String)
                _ArchiveDestinationPath = value
            End Set
        End Property
        Private Shared _CurrentUser As Users
        Public Property CurrentUser As Users
            Get
                Return _CurrentUser
            End Get
            Set(value As Users)
                _CurrentUser = value
            End Set
        End Property
        Private Shared _EngineSession As EngineSessionInformation
        Public ReadOnly Property EngineSession() As EngineSessionInformation
            Get
                Return _EngineSession
            End Get
        End Property
        Public ReadOnly Property cRIO() As cRIOInformationElement
            Get
                Return UsingConfigurationManager.cRIOConfigData.cRIOConfig
            End Get
        End Property
        Public ReadOnly Property QNX() As QnxInformationElement
            Get
                Return UsingConfigurationManager.QnxConfigData.QnxConfig
            End Get
        End Property
        Public ReadOnly Property Unitron() As UnitronInformationElement
            Get
                Return UsingConfigurationManager.UnitronConfigData.UnitronConfig
            End Get
        End Property
        Public ReadOnly Property Hobart() As HobartInformationElement
            Get
                Return UsingConfigurationManager.HobartConfigData.HobartConfig
            End Get
        End Property
        Public ReadOnly Property ExportFormatInformationSection As ExportFormatConfigurationSection
            Get
                Return UsingConfigurationManager.ExportFormatSection
            End Get
        End Property
        Public ReadOnly Property ExportType() As eExportType
            Get
                Return UsingConfigurationManager.ExportConfigData.ExportConfig.ExportType
            End Get
        End Property
        Public ReadOnly Property ExportPathName() As String
            Get
                Return UsingConfigurationManager.ExportConfigData.ExportConfig.ExportPathName
            End Get
        End Property
        Public Shared ReadOnly Property LoadCsvExportConfig(ByVal item As String) As ExportFormatInformationItem
            Get
                Return UsingConfigurationManager.ExportFormatCollection.Item(item)
            End Get
        End Property

        Public ReadOnly Property LoadFormatCollection() As ExportFormatInformationItemCollection
            Get
                Return UsingConfigurationManager.ExportFormatCollection
            End Get
        End Property
        Public ReadOnly Property Database() As DatabaseConfigElement
            Get
                Return UsingConfigurationManager.databaseInfoSection.DatabaseElement
            End Get
        End Property

        Public ReadOnly Property Report() As ReportInformationElement
            Get
                Return UsingConfigurationManager.reportInfoSection.ReportElement
            End Get
        End Property
        Public ReadOnly Property Project() As ProjectInformationElement
            Get
                Return UsingConfigurationManager.projectInfoSection.ProjectElement
            End Get
        End Property
        Public NO_CALIBRATION_CHANNEL() As String = {"VOUT%", "IOUT%"}
        Public CSV_BooleanTables() As String = {"DACN1_NIDAQ_IOMGR_BOOL", _
                                                "DACN1_SERIAL_IOMGR_BOOL"} '{"DACN1_Distributed_IOMGR_Bool"}
        Public CSV_NumericTables() As String = {"DACN1_NIDAQ_IOMGR_REAL", _
                                                "DACN1_POWERDNA_IOMGR_REAL_1", _
                                                "DACN1_SERIAL_IOMGR_REAL", _
                                                "DACN1_SCANIVALVE_IOMGR_REAL"} '{"DACN1_Distributed_IOMGR_Real", "DACN1_SCU_IOMGR_Real"}
        ' "DACN1_POWERDNA_IOMGR_REAL_2", _  DOT NOT ADD POWERDNA_REAL_2 when in STRONGBACK CONFIGURATION!!!!

        Public Function Interpolate_1D(ByVal input_value As Double, ByVal table(,) As Double, ByVal column As Integer) As Global.KineticaRT.Values.Double
            Dim index As Integer = 0
            Dim slope As Double
            Try
                If Not Double.IsNaN(input_value) Then
                    If input_value >= table(0, 0) And input_value <= table(table.GetUpperBound(0), 0) Then
                        While (input_value > table(index + 1, 0) And index < table.GetUpperBound(0) - 1)
                            index += 1
                        End While
                        slope = (table(index + 1, column) - table(index, column)) / (table(index + 1, 0) - table(index, 0))
                        Return New Global.KineticaRT.Values.Double(slope * (input_value - table(index, 0)) + table(index, column))
                    Else 'Outside the table
                        Return Me.KineticaRT_NAN
                    End If
                Else
                    Return Me.KineticaRT_NAN
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("Interpolate_1D: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Me.KineticaRT_NAN
            End Try

        End Function

        Public Function Interpolation_1D(ByVal input_value As Double, ByVal table(,) As Double, ByVal column As Integer, Optional ByVal Transpose As Boolean = False) As Double
            Dim index As Integer = 0
            Dim slope As Double
            Try
                If Transpose Then
                    If input_value >= table(0, 0) And input_value <= table(0, table.GetUpperBound(1)) Then
                        While (input_value > table(0, index + 1) And index < table.GetUpperBound(1) - 1)
                            index += 1
                        End While
                        slope = (table(column, index + 1) - table(column, index)) / (table(0, index + 1) - table(0, index))
                        Return slope * (input_value - table(0, index)) + table(column, index)
                    Else 'Outside the table
                        Return Double.NaN
                    End If
                Else
                    If input_value >= table(0, 0) And input_value <= table(table.GetUpperBound(0), 0) Then
                        While (input_value > table(index + 1, 0) And index < table.GetUpperBound(0) - 1)
                            index += 1
                        End While
                        slope = (table(index + 1, column) - table(index, column)) / (table(index + 1, 0) - table(index, 0))
                        Return slope * (input_value - table(index, 0)) + table(index, column)
                    Else 'Outside the table
                        Return Double.NaN
                    End If
                End If
            Catch ex As Exception
                clsTopMostMessageBox.Show("Interpolation_1D: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Double.NaN
            End Try

        End Function

        Public Function LeastSquaresLine(ByVal X() As Double, ByVal Y() As Double) As Double()
            Dim SumX, SumXSquared, SumY, SumXY As Double
            For I As Integer = 0 To X.Length - 1
                SumX += X(I)
                SumXSquared += X(I) * X(I)
                SumY += Y(I)
                SumXY += X(I) * Y(I)
            Next
            Dim Slope, Intercept, Determinant As Double
            Determinant = X.Length * SumXSquared - SumX * SumX
            Slope = (X.Length * SumXY - SumY * SumX) / Determinant
            Intercept = (SumY * SumXSquared - SumX * SumXY) / Determinant
            Return New Double() {Slope, Intercept}

        End Function

        Public Function IsPairNumber(ByVal Number As Integer) As Boolean
            Return ((Number Mod 2) = 0)
        End Function

        Public Function IsTrue(ByVal newVal As Value) As Boolean
            If (newVal.ToString.ToUpper() = "TRUE") Or (newVal.ToString.ToUpper() = "1") Then
                Return True
            End If
            Return False
        End Function
        Public Function IsFalse(ByVal newVal As Value) As Boolean
            If (newVal.ToString.ToUpper() = "FALSE") Or (newVal.ToString.ToUpper() = "0") Then
                Return True
            End If
            Return False
        End Function
        Public Function ValidateConfiguration() As Boolean
            Return ValidateMSAccessConfiguration()
        End Function
        Private Function ValidateMSAccessConfiguration() As Boolean
            Return File.Exists(Database.Path + "schema.ini")
        End Function


        '************************************************************************************************************************
        '* Input:	DuctDiameter 			(Must be converted to inches) (const)
        '*			OrificeDiameter 		(Must be converted to inches) (const)
        '*			OrificeInletPressure 	(Must be converted to inHgA inches Mercure (Input value Must be Absolute)
        '*			OrificeInletTemp. 		(Must be converted to deg F)
        '*			OrificeDeltaP			(Must be converted to inH20 inches H20)
        '*          OrificeThickness        (Must be converted to inches) (const)
        '*			TapArrangement			(Corner Tap; D and D/2 Tap; Flange Tap)
        '* Output: 	WB_inlbmin 				(Must be converted to lb/min)
        '* Description: This function refresh/calculate the new orifice flow
        '*				each times that the value change for either one of these 
        '*				variables: Orifice Inlet Pressure, Orifice Inlet Temp, Orifice Delta P
        '*				This function is based on the standard ASME MFC-3Ma-2007
        '''************************************************************************************************************************
        Public Function GetBleedAirFlow(ByVal DuctDiameter As Double, _
                  ByVal DuctDiameterUnitType As eLength, _
                  ByVal OrificeDiameter As Double, _
                  ByVal OrificeDiameterUnitType As eLength, _
                  ByVal OrificeInletTemperature As Double, _
                  ByVal OrificeInletTemperatureUnitType As eTemperature, _
                  ByVal OrificeInletPressure As Double, _
                  ByVal OrificeInletPressureUnitType As ePressure, _
                  ByVal OrificeDeltaP As Double, _
                  ByVal OrificeDeltaPUnitType As ePressure, _
                  ByVal OrificeThickness As Double, _
                  ByVal OrificeThicknessUnitType As eLength, _
                  ByVal TapArrangement As eTapArrangement, _
                  ByVal OutputValueUnitType As eFlowMass) As Double

            Dim WB_inlbmin As Double   'Output of this function

            'Orifice and Duct geometry
            Dim Beta As Double
            If DuctDiameter = 0.0 Then
                Beta = 0.0
            Else
                Beta = OrificeDiameter / DuctDiameter
            End If
            Dim B1P3 As Double = Math.Pow(Beta, 1.3)
            Dim B2 As Double = Math.Pow(Beta, 2.0)
            Dim B3P5 As Double = Math.Pow(Beta, 3.5)
            Dim B4 As Double = Math.Pow(Beta, 4.0)
            Dim B8 As Double = Math.Pow(Beta, 8.0)

            Select Case DuctDiameterUnitType 'Formula uses DuctDiameter in inches
                Case eLength.Inch 'Default
                    'Do nothing
                Case eLength.Centimeter
                    DuctDiameter = DuctDiameter * UnitConversion.Inches_Per_Centimeter
                Case Else
                    clsTopMostMessageBox.Show("GetBleedAirFlow: Invalid DuctDiameterUnitType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Throw new Exception
            End Select

            Select Case OrificeDiameterUnitType 'Formula uses OrificeDiameter in inches
                Case eLength.Inch 'Default
                    'Do nothing
                Case eLength.Centimeter
                    OrificeDiameter = OrificeDiameter * UnitConversion.Inches_Per_Centimeter
                Case Else
                    clsTopMostMessageBox.Show("GetBleedAirFlow: Invalid OrificeDiameterUnitType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Throw new Exception
            End Select

            Select Case OrificeThicknessUnitType 'Formula uses OrificeThickness in inches
                Case eLength.Inch 'Default
                    'Do nothing
                Case eLength.Centimeter
                    OrificeThickness = OrificeThickness * UnitConversion.Inches_Per_Centimeter
                Case Else
                    clsTopMostMessageBox.Show("GetBleedAirFlow: Invalid OrificeThicknessUnitType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Throw new Exception
            End Select

            Dim L1 As Double
            Dim L2 As Double
            Select Case TapArrangement
                Case eTapArrangement.Corner
                    L1 = 0.0
                    L2 = 0.0
                Case eTapArrangement.DandD_2
                    L1 = 1.0
                    L2 = (0.5 * DuctDiameter - OrificeThickness) / DuctDiameter
                Case eTapArrangement.Flange
                    L1 = 1 / DuctDiameter
                    L2 = 1 / DuctDiameter
                Case Else
                    clsTopMostMessageBox.Show("GetBleedAirFlow: Invalid TapArrangement", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Select
            Dim M2 As Double = 2 * L2 / (1 - Beta)

            'Pressures and Temperature inputs
            Select Case OrificeInletTemperatureUnitType 'Formula uses OrificeInletTemperature in degree F
                Case eTemperature.Fahrenheit 'Default
                    'Do nothing
                Case eTemperature.Celsius
                    OrificeInletTemperature = OrificeInletTemperature * 1.8 + 32.0
                Case Else
                    clsTopMostMessageBox.Show("GetBleedAirFlow: Invalid OrificeInletTemperatureUnitType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Throw new Exception
            End Select

            Select Case OrificeInletPressureUnitType 'Formula uses OrificeInletPressure PSIA
                Case ePressure.PSI 'Default
                    'Do nothing
                Case ePressure.Kilopascal
                    OrificeInletPressure = OrificeInletPressure * UnitConversion.Psi_Per_kPa
                Case ePressure.InchesOfMercury
                    OrificeInletPressure = OrificeInletPressure * UnitConversion.Psi_Per_inHg
                Case Else
                    clsTopMostMessageBox.Show("GetBleedAirFlow: Invalid OrificeInletPressureUnitType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Throw new Exception
            End Select

            Select Case OrificeDeltaPUnitType 'Formula uses OrificeDeltaP in H20
                Case ePressure.InchesH2O 'Default
                    'Do nothing
                Case ePressure.InchesOfMercury
                    OrificeDeltaP = OrificeDeltaP * UnitConversion.inH20_Per_inHg
                Case ePressure.Kilopascal
                    OrificeDeltaP = OrificeDeltaP * UnitConversion.inH2O_Per_kPa
                Case ePressure.PSI
                    OrificeDeltaP = OrificeDeltaP * UnitConversion.inH2O_Per_Psi
                Case Else
                    clsTopMostMessageBox.Show("GetBleedAirFlow: Invalid OrificeDeltaPUnitType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Throw new Exception
            End Select

            'Air properties
            Dim GasConstant As Double = 53.342
            Dim SHR As Double = 1.396
            Dim rho_air As Double = OrificeInletPressure * 144 / (GasConstant * (OrificeInletTemperature + 459.67))
            Dim c As Double = Math.Pow(SHR * (GasConstant * 32.174) * (OrificeInletTemperature + 459.67), 0.5)
            Dim mu_0 As Double = 0.000000362
            Dim T_0 As Double = 518.7
            Dim SuthConst As Double = 198.72

            'Calculations for Reynolds Number
            Dim ExpansionFactor As Double = 1 - (0.351 + 0.256 * B4 + 0.93 * B8) * (1 - Math.Pow(((OrificeInletPressure - (OrificeDeltaP * 0.03612629)) / OrificeInletPressure), 1 / SHR))
            Dim mu As Double = mu_0 * (T_0 + SuthConst) / (OrificeInletTemperature + 459.67 + SuthConst) * Math.Pow((OrificeInletTemperature + 459.67) / T_0, 1.5)

            'Initial Condition
            Dim WB_0 As Double = 0.0997019 * ExpansionFactor * Math.Pow(OrificeDiameter, 2.0) * Math.Pow((OrificeDeltaP * rho_air) / (1 - B4), 0.5)
            Dim RD_0 As Double = 4.0 * WB_0 / (Math.PI * mu * 32.174 * DuctDiameter / 12.0)
            Dim A_0 As Double = Math.Pow(19000 * Beta / RD_0, 0.8)

            Dim DC(7) As Double
            DC(0) = 0.5961
            DC(1) = 0.0261 * B2
            DC(2) = -0.216 * B8
            DC(3) = 0.000521 * Math.Pow(1000000.0 * Beta / RD_0, 0.7)
            DC(4) = (0.0188 + 0.0063 * A_0) * B3P5 * Math.Pow(1000000.0 / RD_0, 0.3)
            DC(5) = (0.043 + 0.08 * Math.Pow(Math.E, -10 * L1) - 0.123 * Math.Pow(Math.E, -7 * L1)) * (1 - 0.11 * A_0) * B4 / (1 - B4)
            DC(6) = -0.031 * (M2 - 0.8 * Math.Pow(M2, 1.1)) * B1P3

            Dim DischCoeff_0 As Double = DC(0) + DC(1) + DC(2) + DC(3) + DC(4) + DC(5) + DC(6)

            'Iterations
            Dim WB_error As Double = 1
            Dim Iteration_nb As Double = 0
            Dim WB As Double
            Dim RD As Double
            Dim A As Double
            Dim DischCoeff As Double

            'While the calculation has converged, or maximum 5 iterations
            While WB_error > 0.000001 And Iteration_nb < 6
                WB = 0.0997019 * DischCoeff_0 * ExpansionFactor * Math.Pow(OrificeDiameter, 2.0) * Math.Pow((OrificeDeltaP * rho_air) / (1 - B4), 0.5)
                RD = 4.0 * WB / (Math.PI * mu * 32.174 * DuctDiameter / 12.0)
                A = Math.Pow(19000 * Beta / RD, 0.8)
                DC(3) = 0.000521 * Math.Pow(1000000.0 * Beta / RD, 0.7)
                DC(4) = (0.0188 + 0.0063 * A) * B3P5 * Math.Pow(1000000.0 / RD, 0.3)
                DC(5) = (0.043 + 0.08 * Math.Pow(Math.E, -10 * L1) - 0.123 * Math.Pow(Math.E, -7 * L1)) * (1 - 0.11 * A) * B4 / (1 - B4)
                DischCoeff = DC(0) + DC(1) + DC(2) + DC(3) + DC(4) + DC(5) + DC(6)

                WB_error = Math.Abs(WB - WB_0) / WB
                WB_0 = WB
                RD_0 = RD
                A_0 = A
                DischCoeff_0 = DischCoeff
                Iteration_nb += 1
            End While

            'If the calculation has not converged, the value is invalid
            If WB_error > 0.000001 Then
                Return Double.NaN
            End If

            WB_inlbmin = WB * UnitConversion.Minutes_Per_Hour 'Output in lb/min

            Select Case OutputValueUnitType
                Case eFlowMass.LbByMin
                    Return WB_inlbmin
                Case eFlowMass.KgByMin
                    Return WB_inlbmin * UnitConversion.kg_Per_Pound
                Case eFlowMass.KgBySec
                    Return WB_inlbmin * UnitConversion.kg_Per_Pound / UnitConversion.Seconds_Per_Minute
                Case Else
                    clsTopMostMessageBox.Show("GetBleedAirFlow: Invalid OutputValueUnitType", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'Throw new Exception
                    Return 0.0
            End Select

        End Function

        Function SearchControl(Parent As System.Windows.Forms.Control, Name As String) As Object
            Try
                'Si on a le bon contrôle, le retourner.
                If (Parent.Name = Name) Then
                    Return Parent
                End If
                '...pour chaques composantes du contrôle...
                For Each Ctrl As System.Windows.Forms.Control In Parent.Controls
                    Dim Found As New Object
                    '...chercher récursivement
                    Found = SearchControl(Ctrl, Name)
                    If Not IsNothing(Found) Then
                        Return Found
                    End If
                Next
            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.SearchControl: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

            'Si rien n'a été trouvé, retourner Nothing.

            Return Nothing
        End Function

        Function SkinControls(Container As System.Windows.Forms.Control) As Object
            Try

                '...pour chaques composantes du contrôle...
                For Each Ctrl As System.Windows.Forms.Control In Container.Controls

                    'Dim themeColor As Drawing.Color = Drawing.Color.DimGray
                    'Dim themeColorLight As Drawing.Color = Drawing.Color.LightGray
                    'Dim themeColorDark As Drawing.Color = Drawing.Color.Black

                    Dim themeColor As Drawing.Color = Drawing.Color.Blue
                    Dim themeColorLight As Drawing.Color = Drawing.Color.Blue
                    Dim themeColorDark As Drawing.Color = Drawing.Color.Navy

                    Dim gradientMode As Drawing.Drawing2D.LinearGradientMode = Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                    '...chercher récursivement
                    'SkinControls(Ctrl)
                    'If TypeOf Ctrl Is Global.KineticaRT.Studio.Controls.Display Then
                    '    Dim control As Global.KineticaRT.Studio.Controls.Display = Ctrl
                    '    control.DefaultGradientColor = themeColor
                    '    control.GradientEnabled = True
                    '    control.LinearGradientMode = gradientMode
                    '    'control.Font = New System.Drawing.Font("Trebuchet", 9)
                    'ElseIf TypeOf Ctrl Is Global.KineticaRT.Controls.Button Then
                    '    Dim control As Global.KineticaRT.Controls.Button = Ctrl

                    '    control.GradientEnabled = True
                    '    control.LinearGradientMode = gradientMode
                    '    control.BorderColor = themeColorDark
                    '    control.GradientLeftColor = themeColorLight
                    '    control.GradientRightColor = themeColorDark

                    'ElseIf TypeOf Ctrl Is CEL.KineticaRT.Controls.Button Then
                    '    Dim control As CEL.KineticaRT.Controls.Button = Ctrl

                    '    control.GradientEnabled = True
                    '    control.LinearGradientMode = gradientMode
                    '    control.ButtonGradientLeftColor = themeColorLight
                    '    control.ButtonGradientRightColor = themeColorDark
                    '    control.ButtonGradientAlternateLeftColor = themeColorDark
                    '    control.ButtonGradientAlternateRightColor = themeColorLight
                    '    control.ButtonColor = themeColorDark
                    '    control.ButtonAlternateColor = themeColorLight
                    'ElseIf TypeOf Ctrl Is Global.KineticaRT.Studio.Controls.Button Then
                    '    Dim control As Global.KineticaRT.Studio.Controls.Button = Ctrl

                    '    control.GradientEnabled = True
                    '    control.LinearGradientMode = gradientMode
                    '    control.ButtonGradientLeftColor = themeColorLight
                    '    control.ButtonGradientRightColor = themeColorDark
                    '    control.ButtonGradientAlternateLeftColor = themeColorDark
                    '    control.ButtonGradientAlternateRightColor = themeColorLight
                    '    control.ButtonColor = themeColorDark
                    '    control.ButtonAlternateColor = themeColorLight

                    'ElseIf TypeOf Ctrl Is Global.KineticaRT.Controls.Display Then
                    '    Dim control As Global.KineticaRT.Controls.Display = Ctrl
                    '    control.DefaultGradientColor = themeColor
                    '    control.GradientEnabled = True
                    '    control.LinearGradientMode = gradientMode
                    '    control.BorderColor = themeColor
                    '    'control.BorderType = Global.KineticaRT.Controls.BorderType.Rounded

                    'ElseIf TypeOf Ctrl Is Global.KineticaRT.Studio.Controls.Linear Then
                    '    Dim control As Global.KineticaRT.Studio.Controls.Linear = Ctrl
                    '    control.DefaultGradientColor = themeColor
                    '    control.GradientEnabled = True
                    '    control.LinearGradientMode = gradientMode
                    '    control.BorderColor = themeColor
                    '    'control.BorderType = Global.KineticaRT.Controls.BorderType.Rounded

                    'ElseIf TypeOf Ctrl Is Global.KineticaRT.Studio.Controls.Dial Then
                    '    Dim control As Global.KineticaRT.Studio.Controls.Dial = Ctrl
                    '    control.DefaultGradientColor = themeColor
                    '    control.GradientEnabled = True
                    '    control.LinearGradientMode = gradientMode
                    '    control.BorderColor = themeColor
                    '    control.BorderType = Global.KineticaRT.Controls.BorderType.Rounded

                    'ElseIf TypeOf Ctrl Is Global.KineticaRT.Studio.Controls.LampIndicator Then
                    '    Dim control As Global.KineticaRT.Studio.Controls.LampIndicator = Ctrl
                    '    control.DefaultGradientColor = themeColor
                    '    control.GradientEnabled = True
                    '    control.LinearGradientMode = gradientMode


                    'ElseIf TypeOf Ctrl Is Global.KineticaRT.Controls.Indicator Then
                    '    Dim control As Global.KineticaRT.Controls.Indicator = Ctrl
                    '    control.DefaultGradientColor = themeColor
                    '    control.GradientEnabled = True
                    '    control.LinearGradientMode = gradientMode

                    'ElseIf TypeOf Ctrl Is Global.KineticaRT.Controls.Frame Then
                    '    Dim control As Global.KineticaRT.Controls.Frame = Ctrl
                    '    control.BorderColor = themeColor
                    '    control.TitleRightColor = themeColor
                    '    control.TitleUnderlineColor = Drawing.Color.Red
                    '    'control.BorderType = Global.KineticaRT.Controls.BorderType.Rounded

                    'ElseIf TypeOf Ctrl Is Global.KineticaRT.Controls.Tab Then
                    '    Dim control As Global.KineticaRT.Controls.Tab = Ctrl
                    '    control.BorderColor = themeColor
                    '    'control.BorderType = Global.KineticaRT.Controls.BorderType.Rounded

                    'ElseIf TypeOf Ctrl Is Global.KineticaRT.Logic.Message.Display Then
                    '    Dim control As Global.KineticaRT.Logic.Message.Display = Ctrl
                    '    control.BackColor = Drawing.Color.Silver
                    '    control.BorderColor = themeColor

                    'ElseIf TypeOf Ctrl Is System.Windows.Forms.ComboBox Then
                    '    Dim control As System.Windows.Forms.ComboBox = Ctrl
                    '    control.BackColor = Drawing.Color.Silver
                    'ElseIf TypeOf Ctrl Is System.Windows.Forms.TextBox Then
                    '    Dim control As System.Windows.Forms.TextBox = Ctrl
                    '    control.BackColor = Drawing.Color.Silver
                    'End If

                Next
            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.SearchControl: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try

            'Si rien n'a été trouvé, retourner Nothing.
            Return Nothing
        End Function

        Public Function LoadSave_Retain_Variables(ByRef Retain_Variables As System.Collections.Specialized.StringDictionary, Optional ByVal Save As Boolean = False) As Boolean

            LoadSave_Retain_Variables = False

            Try
                'Query Database
                esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                Dim retainVariablesCollection As RetainVariablesCollection = New RetainVariablesCollection()
                retainVariablesCollection.es.Connection.Name = DefaultConnectionName
                retainVariablesCollection.Query.Where(retainVariablesCollection.Query.ProjectID = Me.Project.Number)
                retainVariablesCollection.Query.Load()

                If Save Then
                    For Each retainVariable As RetainVariables In retainVariablesCollection
                        retainVariable.Value = Retain_Variables.Item(retainVariable.VariableName)
                    Next
                    retainVariablesCollection.Save()

                Else 'Load parameters in memory
                    Retain_Variables.Clear()
                    For Each retainVariable As RetainVariables In retainVariablesCollection
                        Retain_Variables.Add(retainVariable.VariableName, retainVariable.Value.ToString())
                    Next
                End If
            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.LoadSave_Retain_Variables: " & Me.GetType().Name.ToString() & " " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
            End Try

        End Function
        Public Function LoadSave_Custom_Parameters(ByRef Custom_Parameters As System.Collections.Specialized.OrderedDictionary, Optional ByVal Save As Boolean = False) As Boolean

            LoadSave_Custom_Parameters = False

            Try
                'Query Database
                esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
                Dim customParameterCollection As CustomParametersCollection = New CustomParametersCollection()
                customParameterCollection.es.Connection.Name = DefaultConnectionName
                customParameterCollection.Query.Where(customParameterCollection.Query.ProjectID = Me.Project.Number)
                customParameterCollection.Query.Where(customParameterCollection.Query.EngineModel = Test_Mgr_UUT_Model.Value.ToString())
                customParameterCollection.Query.Load()

                If Save Then
                    For Each customParameter As CustomParameters In customParameterCollection
                        customParameter.Text = Custom_Parameters.Item(customParameter.ParameterName)
                    Next
                    customParameterCollection.Save()
                Else 'Load parameters in memory
                    Custom_Parameters.Clear()
                    For Each customParameter As CustomParameters In customParameterCollection
                        Custom_Parameters.Add(customParameter.ParameterName, customParameter.Text)
                    Next
                End If

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.LoadSave_Custom_Parameters: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
            End Try

        End Function

        Public Function CleanUpKineticaRtFile() As Boolean
            CleanUpKineticaRtFile = False
            Try
                Dim folder As New DirectoryInfo("C:\Documents and Settings\" + Me.GetWindowsUserName() + "\Local Settings\Temp\KineticaRT\FrameBase\")
                Dim fileInFolder As FileInfo() = folder.GetFiles()
                Dim currentFile As FileInfo

                'list the names of all files in the specified directory
                For Each currentFile In fileInFolder
                    If (Now.Subtract(currentFile.CreationTime()).TotalDays > 15 And currentFile.IsReadOnly()) Then
                        'currentFile.IsReadOnly() = False
                        currentFile.Delete()
                    End If
                Next

                CleanUpKineticaRtFile = True
            Catch ex As Exception

            End Try
        End Function

        Public Sub DisplayOrifice(Optional customMessage As String = "")

            If Test_Mgr_UUT_Model.Value.ToString() <> "DAS CALIBRATION" And Test_Mgr_UUT_Model.Value.ToString() <> "SYSTEM CHECK" Then

                Dim cTemp As String = Nothing



                Dim orificeDiameter As Double = EngineConfiguration.OrificeDiameter

                If Project.Number = 10726 Then

                    cTemp = "Make sure that the " + Format(orificeDiameter, "0.000") + " inch orifice is installed."

                    If Test_Mgr_UUT_Family.Value.ToString() = "GTCP85-129" Or Test_Mgr_UUT_Family.Value.ToString() = "TSCP700" Then
                        cTemp = cTemp + vbCrLf + vbCrLf + "Make sure to load the GTCP85 And TSCP700 file with the CCATS program"

                        clsTopMostMessageBox.Show(cTemp, "CELDAS200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    ElseIf Test_Mgr_UUT_Model.Value.ToString() = "GTCP131-9A" Or Test_Mgr_UUT_Model.Value.ToString() = "GTCP131-9B" Then
                        cTemp = cTemp + vbCrLf + vbCrLf + "Make sure to load the GTCP131-9A-9B file with the CCATS program"
                        cTemp = cTemp + vbCrLf + vbCrLf + "Also make sure that the Vibration pickup are correctly connected"
                        cTemp = cTemp + vbCrLf + "     Serial Number 31665 to the Gearbox"
                        cTemp = cTemp + vbCrLf + "     Serial Number 31666 to the Turbine"
                        cTemp = cTemp + vbCrLf + "     Serial Number 31667 to the Cool fan"

                        clsTopMostMessageBox.Show(cTemp, "CELDAS200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    ElseIf Test_Mgr_UUT_Calib_Constant(1).Value.ToDouble > 0 Then

                        clsTopMostMessageBox.Show(cTemp, "CELDAS200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End If

                ElseIf Project.Number = 11813 Then

                    cTemp = "Make sure to load the " + Test_Mgr_UUT_Model.Value.ToString() + " file with the CCATS program."

                    If Test_Mgr_UUT_Calib_Constant(1).Value.ToDouble <> 0.0 Then

                        cTemp = cTemp + vbCrLf + vbCrLf + "Make sure that the " + Format(Test_Mgr_UUT_Calib_Constant(1).Value.ToDouble, "0.000") + " inch orifice is installed."

                    End If

                    clsTopMostMessageBox.Show(cTemp, "CELDAS200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


                ElseIf Project.Number = 12917 Then

                    If Test_Mgr_UUT_Calib_Constant(1).Value.ToDouble = 0.0 Then

                        cTemp = "Veuillez vous assurer de démarrer le programme de vibration CEC avec le bon fichier de configuration moteur."

                    Else
                        cTemp = "Veuillez vous assurer que l'orifice de " + Format(Test_Mgr_UUT_Calib_Constant(1).Value.ToDouble, "0.000") + " pouces est installé."

                        cTemp = cTemp + vbCrLf + vbCrLf + "Veuillez vous assurer de démarrer le programme de vibration CEC avec le bon fichier de configuration moteur."

                    End If

                    clsTopMostMessageBox.Show(cTemp, "CELDAS200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Else

                    cTemp = "Make sure to load the " + Test_Mgr_UUT_Model.Value.ToString() + " file with the CCATS program." + vbCrLf + vbCrLf + "Make sure that the vibration pick-ups are installed on the engine."

                    If customMessage <> "" Then
                        cTemp += vbCrLf + vbCrLf + customMessage
                    End If
                    clsTopMostMessageBox.Show(cTemp, "CELDAS200", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If



            End If

        End Sub


        Declare Function EndTask Lib "user32.dll" (ByVal hWnd As IntPtr) As Integer
        Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
        Declare Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
        Declare Function SetLastError Lib "kernel32.dll" (ByVal dwErrCode As Integer) As IntPtr
        Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, ByRef nSize As Integer) As Integer

        Public Sub EnsureProcessKilled(ByVal MainWindowHandle As IntPtr, ByVal Caption As String)
            Try
                SetLastError(0)
                ' for Excel versions <10, this won't be set yet
                If IntPtr.Equals(MainWindowHandle, IntPtr.Zero) Then MainWindowHandle = FindWindow(Nothing, Caption)
                If IntPtr.Equals(MainWindowHandle, IntPtr.Zero) Then Exit Sub ' at this point, presume the window has been closed.

                Dim iRes, iProcID As Integer
                iRes = GetWindowThreadProcessId(MainWindowHandle, iProcID)
                If iProcID = 0 Then ' can’t get Process ID
                    If EndTask(MainWindowHandle) <> 0 Then Exit Sub ' success
                    'Throw New ApplicationException("Failed to close.")
                End If
                Dim proc As System.Diagnostics.Process
                proc = System.Diagnostics.Process.GetProcessById(iProcID)
                proc.CloseMainWindow()
                proc.Refresh()
                If proc.HasExited Then Exit Sub
                proc.Kill()
            Catch ex As Exception
                'Do nothing
            End Try
        End Sub



        Public Function GetWindowsUserName() As String
            Dim iReturn As Integer
            Dim userName As String
            userName = New String(CChar(" "), 50)
            iReturn = GetUserName(userName, 50)
            GetWindowsUserName = userName.Substring(0, userName.IndexOf(Chr(0)))
        End Function

        '********************************************************************
        '* Shared Channels FOR ALL SEQUENCES 
        '********************************************************************
        Public Shared _ExcelApplication As Microsoft.Office.Interop.Excel.Application

        Public ARCHIVE_PLAY As IChannel
        Public CLOSE_TEST_BTN As IChannel
        Public DIAGNOSTIC_APU_CLOSE_WINDOW_BTN As IChannel
        Public DIAGNOSTIC_APU_OPEN_WINDOW_BTN As IChannel
        Public DIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN As IChannel
        Public DIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN As IChannel
        Public DYNO_INLET_VALVE_CLOSE_WINDOW_BTN As IChannel
        Public DYNO_INLET_VALVE_OPEN_WINDOW_BTN As IChannel
        Public FCS_CLOSE_WINDOW_BTN As IChannel
        Public FCS_OPEN_WINDOW_BTN As IChannel
        Public FUEL_PRESSURE_REGULATOR_CLOSE_WINDOW_BTN As IChannel
        Public FUEL_PRESSURE_REGULATOR_OPEN_WINDOW_BTN As IChannel
        Public HMIProject As Global.KineticaRT.Studio.Application.Project
        Public KineticaRT_NAN As Global.KineticaRT.Values.Double = New Global.KineticaRT.Values.Double(Double.NaN)

        Public LOG_PRINT_REPORT_BTN As IChannel
        Public LOG_TAKE_POINT_BTN As IChannel
        Public LOG_VIEW_REPORT_BTN As IChannel
        Public NEW_TEST_BTN As IChannel
        Public PERF_CLOSE_WINDOW_BTN As IChannel
        Public PERF_OPEN_WINDOW_BTN As IChannel
        Public PERF_PRINT_REPORT_BTN As IChannel
        Public PERF_TAKE_POINT_BTN As IChannel
        Public PERF_TEST_STEP_NB As Global.KineticaRT.Channels.INumericChannel
        Public PERF_TEST_RESULT_ACCEPTED As Global.KineticaRT.Channels.IBooleanChannel
        Public PERF_TEST_RESULT_REJECTED As Global.KineticaRT.Channels.IBooleanChannel
        Public PERF_VIEW_REPORT_BTN As IChannel
        Public REPAIR_TYPE As IChannel
        Public RVDT_CLOSE_WINDOW_BTN As IChannel
        Public RVDT_OPEN_WINDOW_BTN As IChannel
        Public STATS_CLOSE_WINDOW_BTN As IChannel
        Public STATS_OPEN_WINDOW_BTN As IChannel
        Public SYSTEM_MONITORING_CLOSE_WINDOW_BTN As IChannel
        Public SYSTEM_MONITORING_OPEN_WINDOW_BTN As IChannel
        Public THROTTLE_FCU_CLOSE_WINDOW_BTN As IChannel
        Public THROTTLE_FCU_OPEN_WINDOW_BTN As IChannel
        Public THROTTLE_GOV_CLOSE_WINDOW_BTN As IChannel
        Public THROTTLE_GOV_OPEN_WINDOW_BTN As IChannel
        Public THROTTLE_MOR_CLOSE_WINDOW_BTN As IChannel
        Public THROTTLE_MOR_OPEN_WINDOW_BTN As IChannel


        Public VIEW_CAMERA_BTN As IChannel


        Private Test_Mgr_UUT_Model As IChannel
        Private Test_Mgr_UUT_Family As IChannel
        Public Test_Mgr_Cmd As IChannel
        Public Test_Mgr_Mode As IChannel
        Public Test_Mgr_Status As IChannel
        Public Test_Mgr_User_ID As IChannel
        Public Test_Mgr_User_Name As IChannel
        Public Test_Mgr_User_Security_Lvl As IChannel

        Public Const MAXQUESTIONS As Integer = 18
        Public Const MAXCHOICES As Integer = 8
        Public Const MAXOPTIONS As Integer = 16
        Public Const MAXSERVICEBULLETIN As Integer = 2
        Public Test_Mgr_UUT_Answer(MAXQUESTIONS - 1) As IInfoChannel '18 Elements
        Public Test_Mgr_UUT_Choice(MAXCHOICES - 1) As IInfoChannel '8 Elements
        Public Test_Mgr_UUT_Option(MAXOPTIONS - 1) As IInfoChannel '16 Elements
        Public Test_Mgr_UUT_PwBuildSpecification As IInfoChannel
        Public Test_Mgr_UUT_PwServiceBulletin(MAXSERVICEBULLETIN - 1) As IInfoChannel '2 Elements

        Public Test_Mgr_UUT_Calib_Constant(1) As IChannel '2 Elements
        Public Test_Mgr_UUT_TripPoints_F(3) As IInfoChannel '4 Elements
        Public Test_Mgr_UUT_TripPoints_I(3) As IInfoChannel '4 Elements
        Public Test_Mgr_UUT_TripPoints_N(3) As IInfoChannel '4 Elements
        Public Test_Mgr_UUT_TripPoints_Q(3) As IInfoChannel '4 Elements
        Public Test_Mgr_UUT_TripPoints_T(3) As IInfoChannel '4 Elements
        Public Test_Mgr_UUT_TripPoints_V(3) As IInfoChannel '4 Elements
        Public Test_Mgr_UUT_TripPoints_Vib(3) As IInfoChannel '4 Elements
        Public Test_Mgr_UUT_TripPoints_P(3) As IInfoChannel '4 Elements


        Public Test_Mgr_In_AFDX_IOMmgr_Status As IChannel
        Public Test_Mgr_In_Facility_Controller_Status As IChannel
        Public Test_Mgr_In_Galil_Controller_Status As IChannel
        Public Test_Mgr_In_Load_Bank_Controller_Status As IChannel
        Public Test_Mgr_In_NIDAQ_Status As IChannel
        Public Test_Mgr_In_Omnibusbox_Status As IChannel
        Public Test_Mgr_In_PowerDNA_Status As IChannel
        Public Test_Mgr_In_Scanivalve_Pressure_Status As IChannel
        Public Test_Mgr_In_Scanivalve_Temp_Status As IChannel
        Public Test_Mgr_In_Serial_Arinc_Status As IChannel
        Public Test_Mgr_In_Serial_Status As IChannel
        Public Test_Mgr_In_SM1720_Status As IChannel
        Public Test_Mgr_In_Texcel_V100_Status As IChannel
        Public Test_Mgr_In_Texcel_VX100_Status As IChannel
        Public Test_Mgr_In_UUT_Controller_Status As IChannel
        Public Test_Mgr_In_UUT_Cruncher_Status As IChannel




        'Used by New Test Button

        Public Perf_Nb_Sheet_To_Print As Integer = 2
        Public PERF_NEW_TEST_OPC As IChannel
        Public New_Log_Required As Boolean = False
        Public New_Perf_Required As Boolean = False
        Public New_Trim_Required As Boolean = False


        Public Function Init_Default_Channels() As Boolean
            Dim i As Integer
            Dim Return_Value As New ArrayList()
            Dim TempChannel As IChannel = Nothing
            'Debug.Assert(Not _Sequence Is Nothing)

            Try

                Return_Value.Clear()
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(ARCHIVE_PLAY, "ARCHIVE_PLAY"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(CLOSE_TEST_BTN, "CLOSE_TEST_BTN"))

                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_Cmd, "Test_Mgr_Cmd"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_Mode, "Test_Mgr_Mode"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_Status, "Test_Mgr_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_User_ID, "Test_Mgr_User_ID"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_User_Name, "Test_Mgr_User_Name"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_User_Security_Lvl, "Test_Mgr_User_Security_Lvl"))
                For i = 0 To Test_Mgr_UUT_Answer.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_Answer" + (i + 1).ToString()))
                    Test_Mgr_UUT_Answer(i) = CType(TempChannel, IInfoChannel)
                Next
                For i = 0 To Test_Mgr_UUT_Calib_Constant.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_UUT_Calib_Constant(i), "Test_Mgr_UUT_Calib_Constant" + (i + 1).ToString()))
                Next
                For i = 0 To Test_Mgr_UUT_Choice.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_Choice" + (i + 1).ToString()))
                    Test_Mgr_UUT_Choice(i) = CType(TempChannel, IInfoChannel)
                Next
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_UUT_Model, "Test_Mgr_UUT_Model"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_UUT_Family, "Test_Mgr_UUT_Family"))

                For i = 0 To Test_Mgr_UUT_Option.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_Option" + (i + 1).ToString()))
                    Test_Mgr_UUT_Option(i) = CType(TempChannel, IInfoChannel)
                Next
                For i = 0 To Test_Mgr_UUT_TripPoints_F.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_TripPoints_F" + (i + 1).ToString()))
                    Test_Mgr_UUT_TripPoints_F(i) = CType(TempChannel, IInfoChannel)
                Next
                For i = 0 To Test_Mgr_UUT_TripPoints_I.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_TripPoints_I" + (i + 1).ToString()))
                    Test_Mgr_UUT_TripPoints_I(i) = CType(TempChannel, IInfoChannel)
                Next
                For i = 0 To Test_Mgr_UUT_TripPoints_N.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_TripPoints_N" + (i + 1).ToString()))
                    Test_Mgr_UUT_TripPoints_N(i) = CType(TempChannel, IInfoChannel)
                Next
                For i = 0 To Test_Mgr_UUT_TripPoints_Q.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_TripPoints_Q" + (i + 1).ToString()))
                    Test_Mgr_UUT_TripPoints_Q(i) = CType(TempChannel, IInfoChannel)
                Next
                For i = 0 To Test_Mgr_UUT_TripPoints_T.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_TripPoints_T" + (i + 1).ToString()))
                    Test_Mgr_UUT_TripPoints_T(i) = CType(TempChannel, IInfoChannel)
                Next
                For i = 0 To Test_Mgr_UUT_TripPoints_V.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_TripPoints_V" + (i + 1).ToString()))
                    Test_Mgr_UUT_TripPoints_V(i) = CType(TempChannel, IInfoChannel)
                Next
                For i = 0 To Test_Mgr_UUT_TripPoints_Vib.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_TripPoints_VIB" + (i + 1).ToString()))
                    Test_Mgr_UUT_TripPoints_Vib(i) = CType(TempChannel, IInfoChannel)
                Next
                For i = 0 To Test_Mgr_UUT_TripPoints_P.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_TripPoints_P" + (i + 1).ToString()))
                    Test_Mgr_UUT_TripPoints_P(i) = CType(TempChannel, IInfoChannel)
                Next

                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_PwBuildSpecification"))
                Test_Mgr_UUT_PwBuildSpecification = CType(TempChannel, IInfoChannel)

                For i = 0 To Test_Mgr_UUT_PwServiceBulletin.GetUpperBound(0)
                    Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "Test_Mgr_UUT_PwServiceBulletin" + (i + 1).ToString()))
                    Test_Mgr_UUT_PwServiceBulletin(i) = CType(TempChannel, IInfoChannel)
                Next

                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(DIAGNOSTIC_APU_CLOSE_WINDOW_BTN, "DIAGNOSTIC_APU_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(DIAGNOSTIC_APU_OPEN_WINDOW_BTN, "DIAGNOSTIC_APU_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(DIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN, "DIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(DIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN, "DIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(DYNO_INLET_VALVE_CLOSE_WINDOW_BTN, "DYNO_INLET_VALVE_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(DYNO_INLET_VALVE_OPEN_WINDOW_BTN, "DYNO_INLET_VALVE_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(FCS_CLOSE_WINDOW_BTN, "FCS_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(FCS_OPEN_WINDOW_BTN, "FCS_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(FUEL_PRESSURE_REGULATOR_CLOSE_WINDOW_BTN, "FUEL_PRESSURE_REGULATOR_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(FUEL_PRESSURE_REGULATOR_OPEN_WINDOW_BTN, "FUEL_PRESSURE_REGULATOR_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(LOG_PRINT_REPORT_BTN, "LOG_PRINT_REPORT_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(LOG_TAKE_POINT_BTN, "LOG_TAKE_POINT_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(LOG_VIEW_REPORT_BTN, "LOG_VIEW_REPORT_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(NEW_TEST_BTN, "NEW_TEST_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_Status, "Test_Mgr_Status"))

                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_AFDX_IOMmgr_Status, "Test_Mgr_In_AFDX_IOMgr_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_Facility_Controller_Status, "Test_Mgr_In_Facility_Controller_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_Galil_Controller_Status, "Test_Mgr_In_Galil_Controller_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_Load_Bank_Controller_Status, "Test_Mgr_In_Load_Bank_Controller_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_NIDAQ_Status, "Test_Mgr_In_NIDAQ_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_Omnibusbox_Status, "Test_Mgr_In_Omnibusbox_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_PowerDNA_Status, "Test_Mgr_In_PowerDNA_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_Scanivalve_Pressure_Status, "Test_Mgr_In_Scanivalve_Pressure_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_Scanivalve_Temp_Status, "Test_Mgr_In_Scanivalve_Temp_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_Serial_Arinc_Status, "Test_Mgr_In_Serial_Arinc_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_Serial_Status, "Test_Mgr_In_Serial_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_SM1720_Status, "Test_Mgr_In_SM1720_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_Texcel_V100_Status, "Test_Mgr_In_Texcel_V100_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_Texcel_VX100_Status, "Test_Mgr_In_Texcel_VX100_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_UUT_Controller_Status, "Test_Mgr_In_UUT_Controller_Status"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(Test_Mgr_In_UUT_Cruncher_Status, "Test_Mgr_In_UUT_Cruncher_Status"))


                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(PERF_CLOSE_WINDOW_BTN, "PERF_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(PERF_OPEN_WINDOW_BTN, "PERF_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(PERF_PRINT_REPORT_BTN, "PERF_PRINT_REPORT_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(PERF_TAKE_POINT_BTN, "PERF_TAKE_POINT_BTN"))

                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "PERF_TEST_STEP_NB"))
                PERF_TEST_STEP_NB = CType(TempChannel, INumericChannel)
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "PERF_TEST_RESULT_ACCEPTED"))
                PERF_TEST_RESULT_ACCEPTED = CType(TempChannel, IBooleanChannel)
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(TempChannel, "PERF_TEST_RESULT_REJECTED"))
                PERF_TEST_RESULT_REJECTED = CType(TempChannel, IBooleanChannel)

                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(PERF_VIEW_REPORT_BTN, "PERF_VIEW_REPORT_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(REPAIR_TYPE, "REPAIR_TYPE"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(RVDT_CLOSE_WINDOW_BTN, "RVDT_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(RVDT_OPEN_WINDOW_BTN, "RVDT_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(STATS_CLOSE_WINDOW_BTN, "STATS_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(STATS_OPEN_WINDOW_BTN, "STATS_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(SYSTEM_MONITORING_CLOSE_WINDOW_BTN, "SYSTEM_MONITORING_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(SYSTEM_MONITORING_OPEN_WINDOW_BTN, "SYSTEM_MONITORING_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(THROTTLE_FCU_CLOSE_WINDOW_BTN, "THROTTLE_FCU_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(THROTTLE_FCU_OPEN_WINDOW_BTN, "THROTTLE_FCU_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(THROTTLE_GOV_CLOSE_WINDOW_BTN, "THROTTLE_GOV_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(THROTTLE_GOV_OPEN_WINDOW_BTN, "THROTTLE_GOV_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(THROTTLE_MOR_CLOSE_WINDOW_BTN, "THROTTLE_MOR_CLOSE_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(THROTTLE_MOR_OPEN_WINDOW_BTN, "THROTTLE_MOR_OPEN_WINDOW_BTN"))
                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(VIEW_CAMERA_BTN, "VIEW_CAMERA_BTN"))

                Return_Value.Add(Me.InitializeChannelCallBack.Invoke(PERF_NEW_TEST_OPC, "PERF_NEW_TEST_OPC"))

                'TODO: Faire des delete handler pour les handlers suivant
                AddHandler DIAGNOSTIC_APU_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnDIAGNOSTIC_APU_CLOSE_WINDOW_BTN
                AddHandler DIAGNOSTIC_APU_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnDIAGNOSTIC_APU_OPEN_WINDOW_BTN
                AddHandler DIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnDIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN
                AddHandler DIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnDIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN
                AddHandler DYNO_INLET_VALVE_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnDyno_Inlet_Valve_Button
                AddHandler DYNO_INLET_VALVE_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnCloseDyno_Inlet_Valve_Button
                AddHandler FCS_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnFCS_Button
                AddHandler FCS_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnClose_FCS_Button
                AddHandler FUEL_PRESSURE_REGULATOR_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnFuel_Pressure_Regulator_Button
                AddHandler FUEL_PRESSURE_REGULATOR_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnCloseFuel_Pressure_Regulator_Button
                AddHandler STATS_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnStats_Button
                AddHandler STATS_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnClose_Stats_Button
                AddHandler LOG_PRINT_REPORT_BTN.ValueChanged, AddressOf OnViewReport
                AddHandler LOG_VIEW_REPORT_BTN.ValueChanged, AddressOf OnViewReport
                AddHandler NEW_TEST_BTN.ValueChanged, AddressOf OnNewTest

                AddHandler PERF_PRINT_REPORT_BTN.ValueChanged, AddressOf OnViewReport
                AddHandler PERF_TEST_RESULT_ACCEPTED.ValueChanged, AddressOf OnPERFTestResultAcceptedCheckBox
                AddHandler PERF_TEST_RESULT_REJECTED.ValueChanged, AddressOf OnPERFTestResultRejectedCheckBox
                AddHandler PERF_VIEW_REPORT_BTN.ValueChanged, AddressOf OnViewReport
                AddHandler RVDT_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnRVDT_Button
                AddHandler RVDT_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnClose_RVDT_Button
                AddHandler SYSTEM_MONITORING_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnSYSTEM_MONITORING_OPEN_WINDOW_BTN
                AddHandler SYSTEM_MONITORING_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnSYSTEM_MONITORING_CLOSE_WINDOW_BTN
                AddHandler THROTTLE_FCU_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_FCU_OPEN_WINDOW
                AddHandler THROTTLE_FCU_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_FCU_CLOSE_WINDOW
                AddHandler THROTTLE_GOV_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_GOV_OPEN_WINDOW
                AddHandler THROTTLE_GOV_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_GOV_CLOSE_WINDOW
                AddHandler THROTTLE_MOR_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_MOR_OPEN_WINDOW
                AddHandler THROTTLE_MOR_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_MOR_CLOSE_WINDOW

                AddHandler Test_Mgr_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_AFDX_IOMmgr_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_Facility_Controller_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_Galil_Controller_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_Load_Bank_Controller_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_NIDAQ_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_Omnibusbox_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_PowerDNA_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_Scanivalve_Pressure_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_Scanivalve_Temp_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_Serial_Arinc_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_Serial_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_SM1720_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_Texcel_V100_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_Texcel_VX100_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_UUT_Controller_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                AddHandler Test_Mgr_In_UUT_Cruncher_Status.ValueChanged, AddressOf OnTestMgr_STATUS

                AddHandler VIEW_CAMERA_BTN.ValueChanged, AddressOf OnViewCamera


                If Return_Value.Contains(False) Then
                    Return False
                Else
                    Return True
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.Init_Channels: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function

        Private Sub OnPERFTestResultAcceptedCheckBox(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If Me.IsTrue(newVal) Then
                    Me.PERF_TEST_RESULT_REJECTED.Value = Values.False
                End If

            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnPERFTestResultAcceptedCheckBox: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnPERFTestResultRejectedCheckBox(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If Me.IsTrue(newVal) Then
                    Me.PERF_TEST_RESULT_ACCEPTED.Value = Values.False
                End If

            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnPERFTestResultRejectedCheckBox: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnDyno_Inlet_Valve_Button(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If (Me.IsTrue(newVal)) Then
                    If Me.Test_Mgr_UUT_Family.Value.ToString() <> "" Then
                        Me.ActivateForm("DynoInletValve_Screen", Me.Test_Mgr_UUT_Family.Value.ToString().Replace("-", "_") & "_Screen", True, False)
                    Else
                        Me.ActivateForm("DynoInletValve_Screen", Me.Test_Mgr_UUT_Model.Value.ToString().Replace("-", "_") & "_Screen", True, False)
                    End If
                End If
                DYNO_INLET_VALVE_OPEN_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnDyno_Inlet_Valve_Button: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Private Sub OnCloseDyno_Inlet_Valve_Button(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If (Me.IsTrue(newVal)) Then
                    If Me.Test_Mgr_UUT_Family.Value.ToString() <> "" Then
                        Me.ActivateForm("DynoInletValve_Screen", Me.Test_Mgr_UUT_Family.Value.ToString().Replace("-", "_") + "_Screen", False, False)
                    Else
                        Me.ActivateForm("DynoInletValve_Screen", Me.Test_Mgr_UUT_Model.Value.ToString().Replace("-", "_") + "_Screen", False, False)
                    End If
                End If
                DYNO_INLET_VALVE_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnCloseDyno_Inlet_Valve_Button: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnDIAGNOSTIC_APU_CLOSE_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If Me.IsTrue(newVal) Then
                    Me.ActivateForm("Diagnostic_Screen", Me.DefaultParentWindow, False, False)
                    'Reset Button
                    Me.DIAGNOSTIC_APU_CLOSE_WINDOW_BTN.Value = Values.False
                End If
            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnDIAGNOSTIC_APU_CLOSE_WINDOW_BTN: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnDIAGNOSTIC_APU_OPEN_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If Me.IsTrue(newVal) Then
                    Me.ActivateForm("Diagnostic_Screen", Me.DefaultParentWindow, True, False)
                    'Reset Button
                    Me.DIAGNOSTIC_APU_OPEN_WINDOW_BTN.Value = Values.False
                End If
            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnDIAGNOSTIC_APU_OPEN_WINDOW_BTN: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnDIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If Me.IsTrue(newVal) Then
                    Me.ActivateForm("Diagnostic_Screen_Engine", Me.DefaultParentWindow, False, False)
                    'Reset Button
                    Me.DIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN.Value = Values.False
                End If
            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnDIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnDIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If Me.IsTrue(newVal) Then
                    Me.ActivateForm("Diagnostic_Screen_Engine", Me.DefaultParentWindow, True, False)
                    'Reset Button
                    Me.DIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN.Value = Values.False
                End If
            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnDIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnFCS_Button(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("FCS_Screen_" & Me.Project.Number, Me.DefaultParentWindow, True, False)

                End If

                FCS_OPEN_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnFCS_Button: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnClose_FCS_Button(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("FCS_Screen_" & Me.Project.Number, Me.DefaultParentWindow, False, False)

                End If

                FCS_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnClose_FCS_Button: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnRVDT_Button(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("RVDT_Resolver_Screen", Me.DefaultParentWindow, True, False)

                End If

                RVDT_OPEN_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnRVDT_Button: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnClose_RVDT_Button(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("RVDT_Resolver_Screen", Me.DefaultParentWindow, False, False)

                End If

                RVDT_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnClose_RVDT_Button: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnTHROTTLE_FCU_OPEN_WINDOW(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("FCU_Rigging_Screen", Me.DefaultParentWindow, True, False)

                End If

                THROTTLE_FCU_OPEN_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnTHROTTLE_FCU_OPEN_WINDOW: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnTHROTTLE_FCU_CLOSE_WINDOW(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("FCU_Rigging_Screen", Me.DefaultParentWindow, False, False)

                End If

                THROTTLE_FCU_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnTHROTTLE_FCU_CLOSE_WINDOW: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnTHROTTLE_GOV_OPEN_WINDOW(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("GOV_Rigging_Screen", Me.DefaultParentWindow, True, False)

                End If

                THROTTLE_GOV_OPEN_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnTHROTTLE_GOV_OPEN_WINDOW: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnTHROTTLE_GOV_CLOSE_WINDOW(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("GOV_Rigging_Screen", Me.DefaultParentWindow, False, False)

                End If

                THROTTLE_GOV_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnTHROTTLE_GOV_CLOSE_WINDOW: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnTHROTTLE_MOR_OPEN_WINDOW(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("MOR_Rigging_Screen", Me.DefaultParentWindow, True, False)

                End If

                THROTTLE_MOR_OPEN_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnTHROTTLE_MOR_OPEN_WINDOW: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnTHROTTLE_MOR_CLOSE_WINDOW(ByVal sender As IChannel, ByVal newVal As Value)

            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("MOR_Rigging_Screen", Me.DefaultParentWindow, False, False)

                End If

                THROTTLE_MOR_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnTHROTTLE_MOR_CLOSE_WINDOW: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnFuel_Pressure_Regulator_Button(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("FuelPressureRegulator_Screen", Me.DefaultParentWindow, True, False)

                End If

                FUEL_PRESSURE_REGULATOR_OPEN_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnFuel_Pressure_Regulator_Button: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnCloseFuel_Pressure_Regulator_Button(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("FuelPressureRegulator_Screen", Me.DefaultParentWindow, False, False)

                End If

                FUEL_PRESSURE_REGULATOR_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnCloseFuel_Pressure_Regulator_Button: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnStats_Button(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("Statistics", Me.DefaultParentWindow, True, False)

                End If

                STATS_OPEN_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnStats_Button: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnClose_Stats_Button(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If (Me.IsTrue(newVal)) Then

                    Me.ActivateForm("Statistics", Me.DefaultParentWindow, False, False)

                End If

                STATS_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OnClose_Stats_Button: " & Ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub OnSYSTEM_MONITORING_CLOSE_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If Me.IsTrue(newVal) Then

                    Me.ActivateForm("System_Monitoring_Screen", Me.DefaultParentWindow, False, False)


                End If

                SYSTEM_MONITORING_CLOSE_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnSYSTEM_MONITORING_CLOSE_WINDOW_BTN: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub OnSYSTEM_MONITORING_OPEN_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
            Try
                If Me.IsTrue(newVal) Then

                    Me.ActivateForm("System_Monitoring_Screen", Me.DefaultParentWindow, True, False)

                End If

                SYSTEM_MONITORING_OPEN_WINDOW_BTN.Value = Values.False

            Catch Ex As Exception
                clsTopMostMessageBox.Show("OnSYSTEM_MONITORING_OPEN_WINDOW_BTN: " & Ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Function Dispose_Default_Channels() As Boolean

            Dim Return_Value As New ArrayList()

            Try

                Return_Value.Clear()

                If Not (DIAGNOSTIC_APU_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler DIAGNOSTIC_APU_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnDIAGNOSTIC_APU_CLOSE_WINDOW_BTN
                End If
                If Not (DIAGNOSTIC_APU_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler DIAGNOSTIC_APU_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnDIAGNOSTIC_APU_OPEN_WINDOW_BTN
                End If
                If Not (DIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler DIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnDIAGNOSTIC_ENGINE_CLOSE_WINDOW_BTN
                End If
                If Not (DIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler DIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnDIAGNOSTIC_ENGINE_OPEN_WINDOW_BTN
                End If
                If Not (DYNO_INLET_VALVE_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler DYNO_INLET_VALVE_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnCloseDyno_Inlet_Valve_Button
                End If
                If Not (DYNO_INLET_VALVE_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler DYNO_INLET_VALVE_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnDyno_Inlet_Valve_Button
                End If
                If Not (FCS_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler FCS_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnFCS_Button
                End If
                If Not (FCS_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler FCS_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnClose_FCS_Button
                End If
                If Not (FUEL_PRESSURE_REGULATOR_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler FUEL_PRESSURE_REGULATOR_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnCloseFuel_Pressure_Regulator_Button
                End If
                If Not (FUEL_PRESSURE_REGULATOR_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler FUEL_PRESSURE_REGULATOR_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnFuel_Pressure_Regulator_Button
                End If
                If Not (LOG_PRINT_REPORT_BTN Is Nothing) Then
                    RemoveHandler LOG_PRINT_REPORT_BTN.ValueChanged, AddressOf OnViewReport
                End If
                If Not (LOG_VIEW_REPORT_BTN Is Nothing) Then
                    RemoveHandler LOG_VIEW_REPORT_BTN.ValueChanged, AddressOf OnViewReport
                End If
                If Not (NEW_TEST_BTN Is Nothing) Then
                    RemoveHandler NEW_TEST_BTN.ValueChanged, AddressOf OnNewTest
                End If
                If Not (PERF_PRINT_REPORT_BTN Is Nothing) Then
                    RemoveHandler PERF_PRINT_REPORT_BTN.ValueChanged, AddressOf OnViewReport
                End If
                If Not (PERF_TEST_RESULT_ACCEPTED Is Nothing) Then
                    RemoveHandler PERF_TEST_RESULT_ACCEPTED.ValueChanged, AddressOf OnPERFTestResultAcceptedCheckBox
                End If
                If Not (PERF_TEST_RESULT_REJECTED Is Nothing) Then
                    RemoveHandler PERF_TEST_RESULT_REJECTED.ValueChanged, AddressOf OnPERFTestResultRejectedCheckBox
                End If
                If Not (PERF_VIEW_REPORT_BTN Is Nothing) Then
                    RemoveHandler PERF_VIEW_REPORT_BTN.ValueChanged, AddressOf OnViewReport
                End If
                If Not (RVDT_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler RVDT_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnRVDT_Button
                End If
                If Not (RVDT_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler RVDT_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnClose_RVDT_Button
                End If
                If Not (STATS_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler STATS_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnStats_Button
                End If
                If Not (STATS_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler STATS_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnClose_Stats_Button
                End If
                If Not (SYSTEM_MONITORING_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler SYSTEM_MONITORING_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnSYSTEM_MONITORING_OPEN_WINDOW_BTN
                End If
                If Not (SYSTEM_MONITORING_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler SYSTEM_MONITORING_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnSYSTEM_MONITORING_CLOSE_WINDOW_BTN
                End If
                If Not (THROTTLE_FCU_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler THROTTLE_FCU_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_FCU_OPEN_WINDOW
                End If
                If Not (THROTTLE_FCU_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler THROTTLE_FCU_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_FCU_CLOSE_WINDOW
                End If
                If Not (THROTTLE_GOV_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler THROTTLE_GOV_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_GOV_OPEN_WINDOW
                End If
                If Not (THROTTLE_GOV_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler THROTTLE_GOV_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_GOV_CLOSE_WINDOW
                End If
                If Not (THROTTLE_MOR_OPEN_WINDOW_BTN Is Nothing) Then
                    RemoveHandler THROTTLE_MOR_OPEN_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_MOR_OPEN_WINDOW
                End If
                If Not (THROTTLE_MOR_CLOSE_WINDOW_BTN Is Nothing) Then
                    RemoveHandler THROTTLE_MOR_CLOSE_WINDOW_BTN.ValueChanged, AddressOf OnTHROTTLE_MOR_CLOSE_WINDOW
                End If
                If Not (Test_Mgr_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_AFDX_IOMmgr_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_AFDX_IOMmgr_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_Facility_Controller_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_Facility_Controller_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_Galil_Controller_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_Galil_Controller_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_Load_Bank_Controller_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_Load_Bank_Controller_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_NIDAQ_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_NIDAQ_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_Omnibusbox_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_Omnibusbox_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_PowerDNA_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_PowerDNA_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_Scanivalve_Pressure_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_Scanivalve_Pressure_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_Scanivalve_Temp_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_Scanivalve_Temp_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_Serial_Arinc_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_Serial_Arinc_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_Serial_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_Serial_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_SM1720_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_SM1720_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_Texcel_V100_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_Texcel_V100_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_Texcel_VX100_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_Texcel_VX100_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_UUT_Controller_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_UUT_Controller_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (Test_Mgr_In_UUT_Cruncher_Status Is Nothing) Then
                    RemoveHandler Test_Mgr_In_UUT_Cruncher_Status.ValueChanged, AddressOf OnTestMgr_STATUS
                End If
                If Not (VIEW_CAMERA_BTN Is Nothing) Then
                    RemoveHandler VIEW_CAMERA_BTN.ValueChanged, AddressOf OnViewCamera
                End If

                If Return_Value.Contains(False) Then
                    Return False
                Else
                    Return True
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.Dispose_Init_Channels: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function

        Protected Overrides Sub Finalize()
            If _ExcelApplication IsNot Nothing Then

			    _ExcelApplication.Quit()

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(_ExcelApplication)
                _ExcelApplication = Nothing

                GC.Collect()
                GC.WaitForPendingFinalizers()

            End If

            GC.Collect()
            GC.WaitForPendingFinalizers()
            MyBase.Finalize()
        End Sub
    End Class








    Public MustInherit Class ApuSequence
        Inherits EngineSequence
        Implements IExcelLogging
        Implements IPerformanceLogScreen

        Protected MustOverride Sub Compute(ByVal CalulateForLogAndPerformanceReports As Boolean)


        Protected MustOverride Sub OnCLOSE_TEST_BTN(ByVal sender As IChannel, ByVal newVal As Value)
        Protected MustOverride Sub OnNEW_TEST_BTN(ByVal sender As IChannel, ByVal newVal As Value)

        Protected MustOverride Sub SetTestStepList(ByVal value As Integer)
        Protected MustOverride Sub OnTestStep_List_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Protected MustOverride Sub Init_Engine_Controllers()
        Protected MustOverride Sub Dispose_Engine_Controllers()

        Protected MustOverride Sub OnLOG_TAKE_POINT_BTN(ByVal sender As IChannel, ByVal newVal As Value) Implements IExcelLogging.OnLOG_TAKE_POINT_BTN
        Protected MustOverride Function CreateCustomHeaderLogFile(ByRef ExcelWorksheet As Excel.Worksheet, ByRef RowIndex As Integer, ByRef ColumnIndex As Integer) As Boolean Implements IExcelLogging.CreateCustomHeaderLogFile
        Protected MustOverride Function UpdateCustomHeaderLogFile(ByVal AppendOption As Boolean, ByVal IsPerfEntry As Boolean, ByRef ExcelWorksheet As Excel.Worksheet, ByRef RowIndex As Integer, ByRef ColumnIndex As Integer) As Boolean Implements IExcelLogging.UpdateCustomHeaderLogFile

        Protected MustOverride Sub OnPERF_CLOSE_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value) Implements IPerformanceLogScreen.OnPERF_CLOSE_WINDOW_BTN
        Protected MustOverride Sub OnPERF_OPEN_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value) Implements IPerformanceLogScreen.OnPERF_OPEN_WINDOW_BTN
        Protected MustOverride Sub OnPERF_TAKE_POINT_BTN(ByVal sender As IChannel, ByVal newVal As Value) Implements IPerformanceLogScreen.OnPERF_TAKE_POINT_BTN

    End Class
    Public MustInherit Class PropellerDynoSequence
        Inherits EngineSequence

        Implements IExcelLogging

        Protected MustOverride Sub OnCLOSE_TEST_BTN(ByVal sender As IChannel, ByVal newVal As Value)
        Protected MustOverride Sub OnNEW_TEST_BTN(ByVal sender As IChannel, ByVal newVal As Value)

        Protected MustOverride Sub Init_Engine_Controllers()
        Protected MustOverride Sub Dispose_Engine_Controllers()

        'IExcelLogging
        Protected MustOverride Sub OnLOG_TAKE_POINT_BTN(ByVal sender As IChannel, ByVal newVal As Value) Implements IExcelLogging.OnLOG_TAKE_POINT_BTN
        Protected MustOverride Function CreateCustomHeaderLogFile(ByRef ExcelWorksheet As Excel.Worksheet, ByRef RowIndex As Integer, ByRef ColumnIndex As Integer) As Boolean Implements IExcelLogging.CreateCustomHeaderLogFile
        Protected MustOverride Function UpdateCustomHeaderLogFile(ByVal AppendOption As Boolean, ByVal IsPerfEntry As Boolean, ByRef ExcelWorksheet As Excel.Worksheet, ByRef RowIndex As Integer, ByRef ColumnIndex As Integer) As Boolean Implements IExcelLogging.UpdateCustomHeaderLogFile

        Protected MustOverride Sub OnDYNO_DEMAND_VALIDATE(ByVal sender As IChannel, ByVal newVal As Value)
        Protected MustOverride Sub OnDYNO_INLET_VALIDATE(ByVal sender As IChannel, ByVal newVal As Value)

    End Class
    Public Interface IExcelLogging
        Sub OnLOG_TAKE_POINT_BTN(ByVal sender As IChannel, ByVal newVal As Value)
        Function CreateCustomHeaderLogFile(ByRef ExcelWorksheet As Excel.Worksheet, ByRef RowIndex As Integer, ByRef ColumnIndex As Integer) As Boolean
        Function UpdateCustomHeaderLogFile(ByVal AppendOption As Boolean, ByVal IsPerfEntry As Boolean, ByRef ExcelWorksheet As Excel.Worksheet, ByRef RowIndex As Integer, ByRef ColumnIndex As Integer) As Boolean
    End Interface
    Public Interface IExcelPerformanceReport

    End Interface
    Public Interface IPerformanceLogScreen
        Sub OnPERF_CLOSE_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
        Sub OnPERF_OPEN_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
        Sub OnPERF_TAKE_POINT_BTN(ByVal sender As IChannel, ByVal newVal As Value)
    End Interface
    Public Interface IAccelCheckScreen
        Sub OnACCEL_CHECK_OPEN_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
        Sub OnACCEL_CHECK_CLOSE_WINDOW_BTN(ByVal sender As IChannel, ByVal newVal As Value)
    End Interface
    Public Interface IBleedAirFlowCalculation
        Function Compute_BleedAirFlow(ByVal TOrifice As Double, _
                                            ByVal POrifice As Double, _
                                            ByVal POrificeDelta As Double) As Double
    End Interface
    Public Interface ICorrectionTableCalculation 'For Honeywell APU

    End Interface
    Public Interface ISmartTerminal
        Sub OnSMART_TERMINAL_BTN(ByVal sender As IChannel, ByVal newVal As Value)
    End Interface

    Public MustInherit Class EngineSequence
        Inherits StandardSequence

    End Class

    Public MustInherit Class StandardSequence
        Inherits CEL.KineticaRT.Sequence.Sequence

        Protected CELProject As CELDAS = CELDAS.Instance()
        Protected ReadOnly Property EngineConfiguration() As EngineInformationItem
            Get
                'Par defaut on retourne la config propre au moteur, sinon on retourne cette de la famille
                Dim engineConfig As EngineInformationItem = UsingConfigurationManager.engineInfoCollection.Item(CELProject.EngineModel)
                If IsNothing(engineConfig) Then
                    engineConfig = UsingConfigurationManager.engineInfoCollection.Item(CELProject.EngineFamily)
                    If Not IsNothing(engineConfig) Then
                        Console.WriteLine("Engine configuration not found in app.config for engine model " + CELProject.EngineModel + " The configuration for family will be used by default " + CELProject.EngineFamily)
                    End If
                    Return engineConfig
                Else
                    Return engineConfig
                End If
            End Get
        End Property

        Protected MustOverride Sub AddHandlers()
        Protected MustOverride Sub RemoveHandlers()

        Protected MustOverride Sub Init_Engine_Controls()
        Protected MustOverride Function Init_Engine_Channels() As Boolean
        Protected MustOverride Sub Init_Engine_Channels_Value()

    End Class






    Public Class EngineSessionInformation

        Private _WorkingFolder As String
        Public Property WorkingFolder() As String
            Get
                If _WorkingFolder.Substring(_WorkingFolder.Length - 1) = "\" Then
                    Return String.Format("{0}", _WorkingFolder)
                Else
                    Return String.Format("{0}\", _WorkingFolder)
                End If
            End Get
            Set(ByVal value As String)
                _WorkingFolder = value
            End Set
        End Property
        Public ReadOnly Property LogFolder() As String
            Get
                Return String.Format("{0}{1}\", WorkingFolder, "Log")
            End Get
        End Property
        Private _PerfFolder As String
        Public ReadOnly Property PerfFolder() As String
            Get
                Return String.Format("{0}{1}\", WorkingFolder, "Perf")
            End Get
        End Property
        Public ReadOnly Property ArchiveFolder() As String
            Get
                Return String.Format("{0}{1}\", WorkingFolder, "Archive")
            End Get
        End Property

    End Class



    'TODO: Cette classe est une BusinessLogic qui devrait être déplacée dans les Classes Customs de EntitySpaces
    Public Class UserInputInformation
        Implements IDisposable

        Private CELProject As CELDAS = CELDAS.Instance()

        Private _engine As Uut
        Private _restoringSession As Boolean
        'Questions
        Private _UutQuestionCollection As UUTQuestionCollection
        Private _txtAnswers(CELDAS.MAXQUESTIONS - 1) As System.Windows.Forms.Control
        'Choices
        Private _UutChoiceCollection As UUTChoiceCollection
        Private _lblChoice(CELDAS.MAXCHOICES - 1) As System.Windows.Forms.Control '8 Elements
        Private _cboChoice(CELDAS.MAXCHOICES - 1) As System.Windows.Forms.ComboBox '8 Elements


        Private _cboPwBuildSpecification As System.Windows.Forms.ComboBox
        Private _lblPwBuilSpecification As System.Windows.Forms.Control
        Private _cboPwServiceBulletin(CELDAS.MAXSERVICEBULLETIN - 1) As System.Windows.Forms.ComboBox
        Private _lblPwServiceBulletin(CELDAS.MAXSERVICEBULLETIN - 1) As System.Windows.Forms.Control

        'Otions
        Private _UutOptionCollection As UUTOptionCollection
        Private _chkOptions(CELDAS.MAXOPTIONS - 1) As System.Windows.Forms.Control
        Private _grpOptionsBox As System.Windows.Forms.Control
        Private _lblOptions As System.Windows.Forms.Control
        Private OK_UserInputs As IChannel
        Private _PWCEECPartNumber As String = "Default"

        Public Property PWCEECPartNumber As String
            Get
                If _PWCEECPartNumber = "" Then
                    _PWCEECPartNumber = "Default"
                End If
                Return _PWCEECPartNumber
            End Get
            Set(ByVal value As String)
                _PWCEECPartNumber = value
            End Set
        End Property

        Public Sub New(ByRef engine As Uut, ByVal restoringSession As Boolean)
            _engine = engine
            _restoringSession = restoringSession

            'We need the data also when restoring session to update description on generic channels like Test_Mgr_UUT_Answers...choice and other, this description is used in log_report generation
            _UutChoiceCollection = engine.UUTChoiceCollectionByProjectID
            _UutQuestionCollection = engine.UUTQuestionCollectionByProjectID
            _UutOptionCollection = engine.UUTOptionCollectionByProjectID


        End Sub

        Public Function Initialize() As Boolean
            Initialize = False

            Try
                If _engine.EngineModel <> "DAS CALIBRATION" And _engine.EngineModel <> "SYSTEM CHECK" Then

                    If InitializeObjects() Then
                        If InitializeQuestions() Then 'Get_User_Inputs_Questions()
                            If InitializeChoices() Then
                                If InitializeOptions() Then
                                    If InitializeBuildSpecificationCombo() Then
                                        If InitializeServiceBulletinCombo() Then
                                            Initialize = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else
                    Initialize = True
                End If

            Catch ex As Exception
                clsTopMostMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function
        Private Function InitializeObjects() As Boolean

            Try
                InitializeObjects = False

                CELProject.InitializeChannelCallBack.Invoke(OK_UserInputs, "OK_UserInputs")

                Dim i As Integer
                For i = 0 To _txtAnswers.GetUpperBound(0)
                    CELProject.InitializeObjectCallBack(_txtAnswers(i), "UserInputs.Answer" + (i + 1).ToString())
                Next
                For i = 0 To _lblChoice.GetUpperBound(0)
                    CELProject.InitializeObjectCallBack(_lblChoice(i), "UserInputs.Choice_Label" + (i + 1).ToString())
                    CELProject.InitializeObjectCallBack(_cboChoice(i), "UserInputs.Choice_List" + (i + 1).ToString())
                Next
                For i = 0 To _chkOptions.GetUpperBound(0)
                    CELProject.InitializeObjectCallBack(_chkOptions(i), "UserInputs.Option" + (i + 1).ToString())
                Next
                CELProject.InitializeObjectCallBack(_grpOptionsBox, "UserInputs.Options_Box")
                CELProject.InitializeObjectCallBack(_lblOptions, "UserInputs.1")

                CELProject.InitializeObjectCallBack(_lblPwBuilSpecification, "UserInputs.lblPwBuildSpecification")
                CELProject.InitializeObjectCallBack(_cboPwBuildSpecification, "UserInputs.cboPwBuildSpecification")
                For i = 0 To 1
                    CELProject.InitializeObjectCallBack(_cboPwServiceBulletin(i), "UserInputs.cboPwServiceBulletin" + (i + 1).ToString())
                    CELProject.InitializeObjectCallBack(_lblPwServiceBulletin(i), "UserInputs.lblPwServiceBulletin" + (i + 1).ToString())
                Next


                InitializeObjects = True

            Catch ex As Exception
                clsTopMostMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                InitializeObjects = False
            End Try


        End Function
        Private Function InitializeQuestions() As Boolean
            InitializeQuestions = False
            Try

                If Not _restoringSession Then
                    For i As Integer = 0 To _txtAnswers.GetUpperBound(0)
                        _txtAnswers(i).Visible = False
                    Next

                    For Each question As UUTQuestion In _engine.UUTQuestionCollectionByProjectID

                        Dim index As Integer = CType(question.QuestionID.Value, Integer) 'Attention Commence a 1

                        _txtAnswers(index - 1).Visible = question.Question <> ""
                        CELProject.Test_Mgr_UUT_Answer(index - 1).Description = question.Question
                        CELProject.Test_Mgr_UUT_Answer(index - 1).Value = New Global.KineticaRT.Values.Text(question.DefaultValue)

                    Next
                Else
                    For Each question As UUTQuestion In _engine.UUTQuestionCollectionByProjectID

                        Dim index As Integer = CType(question.QuestionID.Value, Integer) 'Attention Commence a 1

                        _txtAnswers(index - 1).Visible = question.Question <> ""
                        CELProject.Test_Mgr_UUT_Answer(index - 1).Description = question.Question
                    Next

                End If


                InitializeQuestions = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.OpenTest_Sequence.InitializeInputQuestions: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Function

        Public Function UpdateDefaultValueUpdatable() As Boolean

            UpdateDefaultValueUpdatable = False

            Dim UserAnswer As String = String.Empty

            Try

                For Each question As UUTQuestion In _UutQuestionCollection

                    Dim index As Integer = CType(question.QuestionID, Integer)  'Attention Commence a 1

                    UserAnswer = CELProject.Test_Mgr_UUT_Answer(index - 1).Value.ToString()

                    If question.DefaultValueUpdatable = True Then

                        question.DefaultValue = UserAnswer

                    End If

                Next

                _UutQuestionCollection.Save()

            Catch ex As Exception

            End Try

        End Function

        Public Function ExecuteChoicesCustomUpdateQuery() As Boolean

            ExecuteChoicesCustomUpdateQuery = False

            Try

                For Each choice As UUTChoice In _UutChoiceCollection

                    Dim index As Integer = CType(choice.ChoiceID.Value, Integer)  'Attention Commence a 1

                    If choice.DataType = "QUERY" AndAlso choice.CustomUpdateQuery <> "" Then

                        Dim updateQuery As String = choice.CustomUpdateQuery.Replace("VALUEFROMCOMBO", _cboChoice(index - 1).Text)
                        Dim rs As New ADODB.Recordset
                        Dim cn As New ADODB.Connection
                        Dim recordAffected As Object = 0
                        Try

                            If DatabaseHelper.OpenConnection(CELProject.Database.OleDbConnectionString, cn) Then

                                cn.Execute(updateQuery, recordAffected)
                                If CType(recordAffected, Integer) = 0 Then
                                    clsTopMostMessageBox.Show("CELDAS.UserInputInformation.ExecuteChoicesCustomUpdateQuery: " & vbCrLf & vbCrLf & " No record affected for Custom update query. " & vbCrLf & vbCrLf & updateQuery, "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            End If

                        Catch ex As Exception
                            clsTopMostMessageBox.Show("CELDAS.UserInputInformation.ExecuteChoicesCustomUpdateQuery: the following query generated an exception" & vbCrLf & vbCrLf & updateQuery & vbCrLf & vbCrLf & "Custom update query error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End If
                Next

                ExecuteChoicesCustomUpdateQuery = True

            Catch ex As Exception

            End Try

        End Function

        Private Function InitializeChoices() As Boolean

            InitializeChoices = False

            Try
                If Not _restoringSession Then

                    For i As Integer = 0 To _cboChoice.GetUpperBound(0)

                        CELProject.Test_Mgr_UUT_Choice(i).Description = ""
                        CELProject.Test_Mgr_UUT_Choice(i).Value = Values.EmptyString

                        _lblChoice(i).Text = ""
                        _cboChoice(i).DataSource = Nothing
                        SetControlsVisible(False, i)
                        AddHandler _cboChoice(i).SelectedIndexChanged, AddressOf cboChoice_SelectedIndexChanged
                        AddHandler _cboChoice(i).LostFocus, AddressOf cboChoice_SelectedIndexChanged
                    Next

                    For Each choice As UUTChoice In _UutChoiceCollection

                        Dim index As Integer = CType(choice.ChoiceID.Value, Integer)  'Attention Commence a 1

                        SetControlsVisible(choice.ChoiceTitle <> "", index - 1)

                        _lblChoice(index - 1).Text = choice.ChoiceTitle
                        CELProject.Test_Mgr_UUT_Choice(index - 1).Description = choice.ChoiceTitle

                        If choice.DataType <> "QUERY" Then

                            _cboChoice(index - 1).DisplayMember = "ItemDescription"
                            _cboChoice(index - 1).ValueMember = "ItemID"
                            _cboChoice(index - 1).DataSource = choice.UUTChoiceItemsCollectionByProjectID

                        Else 'OPEN MANUAL QUERY INSTEAD OF RELATED TABLE UUT_CHOICE_ITEM

                            Dim selectString As String = choice.CustomSelectQuery
                            Dim rs As New ADODB.Recordset
                            Dim cn As New ADODB.Connection

                            Try

                                If DatabaseHelper.OpenConnection(CELProject.Database.OleDbConnectionString, cn) Then

                                    If DatabaseHelper.OpenRecordset(rs, selectString, cn, False) Then
                                        Dim i As Integer = 1
                                        Do While Not rs.EOF

                                            Dim choiceItem As New UUTChoiceItems
                                            choiceItem.ItemID = i.ToString
                                            choiceItem.ItemDescription = rs.Fields(0).Value.ToString
                                            _cboChoice(index - 1).Items.Add(choiceItem)
                                            i += 1
                                            rs.MoveNext()
                                        Loop

                                        rs.Close()
                                    End If

                                End If

                            Catch ex As Exception
                                clsTopMostMessageBox.Show("CELDAS.UserInputInformation.InitializeInputChoices: " & vbCrLf & vbCrLf & "Custom query error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try

                        End If

                        If choice.DefaultIndex <> -1 Then
                            _cboChoice(index - 1).SelectedIndex = choice.DefaultIndex - 1
                        End If

                    Next
                Else
                    For i As Integer = 0 To _cboChoice.GetUpperBound(0)

                        CELProject.Test_Mgr_UUT_Choice(i).Description = ""

                    Next

                    For Each choice As UUTChoice In _UutChoiceCollection

                        Dim index As Integer = CType(choice.ChoiceID.Value, Integer)  'Attention Commence a 1

                        CELProject.Test_Mgr_UUT_Choice(index - 1).Description = choice.ChoiceTitle

                    Next
                End If

                InitializeChoices = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.UserInputInformation.InitializeInputChoices: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Return InitializeChoices

        End Function

        Private Function InitializeOptions() As Boolean
            InitializeOptions = False
            Try
                If _restoringSession Then
                    Return True
                End If

                For i As Integer = 0 To _chkOptions.GetUpperBound(0) - 1
                    _chkOptions(i).Visible = False
                    _grpOptionsBox.Visible = False
                    _lblOptions.Visible = False
                Next

                For Each uutOption As UUTOption In _UutOptionCollection

                    Dim index As Integer = CType(uutOption.OptionID.Value, Integer) 'Attention Commence a 1

                    _chkOptions(index - 1).Visible = uutOption.OptionTitle <> ""
                    CELProject.Test_Mgr_UUT_Option(index - 1).Description = uutOption.OptionTitle
                    CELProject.Test_Mgr_UUT_Option(index - 1).Value = New Global.KineticaRT.Values.Text(uutOption.DefaultValue.Value.ToString())


                    _grpOptionsBox.Visible = True
                    _lblOptions.Visible = True

                Next

                InitializeOptions = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.UserInputInformation.InitializeOptions: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Function

        Private Sub cboChoice_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim Index As Integer

            Try
                Dim cboSender As System.Windows.Forms.ComboBox = CType(sender, System.Windows.Forms.ComboBox)
                Index = CInt(Right(cboSender.Name, 1))

                CELProject.Test_Mgr_UUT_Choice(Index - 1).Value = New Global.KineticaRT.Values.Text(cboSender.Text)

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.UserInputInformation.cboChoice_SelectedIndexChanged: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
        Private Sub cboPwBuildSpecification_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

            Try
                Dim cboSender As System.Windows.Forms.ComboBox = CType(sender, System.Windows.Forms.ComboBox)

                CELProject.Test_Mgr_UUT_PwBuildSpecification.Value = New Global.KineticaRT.Values.Text(cboSender.Text)

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.UserInputInformation.cboPwBuildSpecification_SelectedIndexChanged: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
        Private Sub cboServiceBulletin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

            Try
                Dim cboSender As System.Windows.Forms.ComboBox = CType(sender, System.Windows.Forms.ComboBox)

                Select Case cboSender.Name
                    Case "cboPwServiceBulletin1"
                        CELProject.Test_Mgr_UUT_PwServiceBulletin(0).Value = New Global.KineticaRT.Values.Text(cboSender.Text)
                    Case "cboPwServiceBulletin2"
                        CELProject.Test_Mgr_UUT_PwServiceBulletin(1).Value = New Global.KineticaRT.Values.Text(cboSender.Text)
                End Select

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.UserInputInformation.cboServiceBulletin_SelectedIndexChanged: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Function InitializeBuildSpecificationCombo() As Boolean
            InitializeBuildSpecificationCombo = False

            Try
                If _restoringSession Then
                    Return True
                End If

                CELProject.Test_Mgr_UUT_PwBuildSpecification.Description = ""
                CELProject.Test_Mgr_UUT_PwBuildSpecification.Value = Values.EmptyString

                Dim engineBuildSpecCollection As EnginePwBuildSpecificationCollection = New EnginePwBuildSpecificationCollection()
                engineBuildSpecCollection.es.Connection.Name = CELProject.DefaultConnectionName
                engineBuildSpecCollection.Query.Where(engineBuildSpecCollection.Query.EngineModel = _engine.EngineModel)
                engineBuildSpecCollection.Query.Where(engineBuildSpecCollection.Query.Unavailable = False)
                engineBuildSpecCollection.Query.OrderBy(engineBuildSpecCollection.Query.Id.Ascending)
                engineBuildSpecCollection.Query.Load()

                SetControlVisible(engineBuildSpecCollection.Count > 0, _lblPwBuilSpecification)
                SetControlVisible(engineBuildSpecCollection.Count > 0, CType(_cboPwBuildSpecification, System.Windows.Forms.Control))

                _cboPwBuildSpecification.ValueMember = "ID"
                _cboPwBuildSpecification.DisplayMember = "BuildSpecification"
                _cboPwBuildSpecification.DataSource = engineBuildSpecCollection

                _cboPwBuildSpecification.SelectedIndex = -1

                AddHandler _cboPwBuildSpecification.SelectedIndexChanged, AddressOf cboPwBuildSpecification_SelectedIndexChanged
                AddHandler _cboPwBuildSpecification.LostFocus, AddressOf cboPwBuildSpecification_SelectedIndexChanged

                InitializeBuildSpecificationCombo = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.UserInputInformation.InitializeInputChoices: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Return InitializeBuildSpecificationCombo
        End Function
        Private Function InitializeServiceBulletinCombo() As Boolean
            InitializeServiceBulletinCombo = False

            Try

                If _restoringSession Then
                    Return True
                End If

                For i As Integer = 0 To _cboPwServiceBulletin.GetUpperBound(0)
                    CELProject.Test_Mgr_UUT_PwServiceBulletin(i).Description = ""
                    CELProject.Test_Mgr_UUT_PwServiceBulletin(i).Value = Values.EmptyString
                Next i

                Dim engineServiceBulletinCollection As EnginePwServiceBulletinCollection = New EnginePwServiceBulletinCollection()
                engineServiceBulletinCollection.es.Connection.Name = CELProject.DefaultConnectionName
                engineServiceBulletinCollection.Query.Where(engineServiceBulletinCollection.Query.EngineModel = _engine.EngineModel)
                engineServiceBulletinCollection.Query.Where(engineServiceBulletinCollection.Query.Unavailable = False)
                engineServiceBulletinCollection.Query.OrderBy(engineServiceBulletinCollection.Query.Id.Ascending)
                engineServiceBulletinCollection.Query.Load()

                For i As Integer = 0 To _cboPwServiceBulletin.GetUpperBound(0)
                    SetControlVisible(engineServiceBulletinCollection.Count > 0, _lblPwServiceBulletin(i))
                    SetControlVisible(engineServiceBulletinCollection.Count > 0, CType(_cboPwServiceBulletin(i), System.Windows.Forms.Control))
                Next i

                For i As Integer = 0 To _cboPwServiceBulletin.GetUpperBound(0) 'les handler doivent être ajouté après les databinding pour que les 2 combo soit synchrone
                    AddHandler _cboPwServiceBulletin(i).SelectedIndexChanged, AddressOf cboServiceBulletin_SelectedIndexChanged
                    AddHandler _cboPwServiceBulletin(i).LostFocus, AddressOf cboServiceBulletin_SelectedIndexChanged
                Next

                'Cas spécial on a 2 combo dont la 1e affiche le service bulletin 1 et l'autre affiche le service bulletin 2 (les SB sont en groupes de 2
                For i As Integer = 0 To _cboPwServiceBulletin.GetUpperBound(0)
                    _cboPwServiceBulletin(i).ValueMember = "ID"
                    _cboPwServiceBulletin(i).DisplayMember = "ServiceBulletin" + (i + 1).ToString
                    _cboPwServiceBulletin(i).DataSource = engineServiceBulletinCollection
                Next

                InitializeServiceBulletinCombo = True

            Catch ex As Exception
                clsTopMostMessageBox.Show("CELDAS.UserInputInformation.InitializeInputChoices: " & vbCrLf & vbCrLf & "Error message: " & vbCrLf & ex.ToString(), "UNEXPECTED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Return InitializeServiceBulletinCombo
        End Function


        Private Delegate Sub SetControlsVisibleDelegate(ByVal value As Boolean, ByVal index As Integer)
        Private Sub SetControlsVisible(ByVal value As Boolean, ByVal index As Integer)
            If (_cboChoice(0).InvokeRequired) Then
                _cboChoice(0).BeginInvoke(New SetControlsVisibleDelegate(AddressOf Me.SetControlsVisible), value, index)
            Else
                _cboChoice(index).Visible = value
                _lblChoice(index).Visible = value
            End If
        End Sub
        Private Delegate Sub SetControlVisibleDelegate(ByVal value As Boolean, ByRef control As System.Windows.Forms.Control)
        Private Sub SetControlVisible(ByVal value As Boolean, ByRef control As System.Windows.Forms.Control)
            If (control.InvokeRequired) Then
                control.BeginInvoke(New SetControlVisibleDelegate(AddressOf Me.SetControlVisible), value, control)
            Else
                control.Visible = value
            End If
        End Sub

        Function ValidateAnswer() As Boolean

            Dim Find_Character1 As Integer
            Dim Find_Character2 As Integer
            Dim Find_Character3 As Integer
            Dim Find_Character4 As Integer
            Dim Find_Character5 As Integer
            Dim Find_Character6 As Integer
            Dim Find_Character7 As Integer
            Dim Find_Character8 As Integer
            Dim Find_Character9 As Integer

            Dim Value As Double
            Dim UserQuestionAnswer As String = String.Empty
            Dim UserChoiceAnswer As String = String.Empty

            Dim Return_Value As New ArrayList()

            'TODO: Amelioration: Ici c'est de la logique de businnes logic qu'on devrait integrer nos objets du domaine

            Try
                'Validate Question
                'For Each Question As UserQuestionInformation In QuestionList

                For Each Question As UUTQuestion In _UutQuestionCollection
                    UserQuestionAnswer = CELProject.Test_Mgr_UUT_Answer(Question.QuestionID - 1).Value.ToString()

                    If Question.Question <> "" Then
                        If UserQuestionAnswer <> String.Empty Then
                            If Question.DataType = "VALUE" Then
                                If Not IsNumeric(UserQuestionAnswer) Then
                                    clsTopMostMessageBox.Show("Error: " & Question.Question & " has a wrong value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    OK_UserInputs.Value = Values.False
                                    Return_Value.Add(False)
                                Else
                                    Value = CDbl(UserQuestionAnswer)

                                    If Value > Question.HiLimit Then
                                        clsTopMostMessageBox.Show(Question.Question & " higher than " & Question.HiLimit, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        OK_UserInputs.Value = Values.False
                                        Return_Value.Add(False)
                                    ElseIf Value < Question.LoLimit Then
                                        clsTopMostMessageBox.Show(Question.Question & " lower than " & Question.LoLimit, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        OK_UserInputs.Value = Values.False
                                        Return_Value.Add(False)
                                    Else
                                        'Answers(Index) = Answer
                                        Return_Value.Add(True)
                                    End If
                                End If
                                'If the answer is TEXT type
                            Else
                                If CELProject.EngineFamily = "PT6T" And (Question.QuestionID = 1) And ((UserQuestionAnswer.Split("-").Length - 1) <> 1) Then
                                    clsTopMostMessageBox.Show("PT6T engine : Serial numbers of engines must entered with the following format : PORT-STBD", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    OK_UserInputs.Value = Values.False
                                    Return_Value.Add(False)
                                End If
                                'Answers(Index) = Answer
                                Return_Value.Add(True)
                            End If
                            'If the user don't have answer the question, check if the answer is required
                        Else
                            If Question.Mandatory = True Then
                                clsTopMostMessageBox.Show(Question.Question & " has an empty field", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                OK_UserInputs.Value = Values.False
                                Return_Value.Add(False)
                            End If
                        End If
                    Else
                        Return_Value.Add(True)
                    End If

                    If ((Question.QuestionID = 1) Or (Question.QuestionID = 2)) Then 'Lors de la construction des path pour sauvegarder les rapports on valide certains varactères spéciaux
                        If Len(UserQuestionAnswer) <> 0 Then
                            Find_Character1 = UserQuestionAnswer.IndexOf("\", 1, Len(UserQuestionAnswer) - 1)
                            Find_Character2 = UserQuestionAnswer.IndexOf("/", 1, Len(UserQuestionAnswer) - 1)
                            Find_Character3 = UserQuestionAnswer.IndexOf(":", 1, Len(UserQuestionAnswer) - 1)
                            Find_Character4 = UserQuestionAnswer.IndexOf("*", 1, Len(UserQuestionAnswer) - 1)
                            Find_Character5 = UserQuestionAnswer.IndexOf("?", 1, Len(UserQuestionAnswer) - 1)
                            Find_Character6 = UserQuestionAnswer.IndexOf(Chr(34), 1, Len(UserQuestionAnswer) - 1)
                            Find_Character7 = UserQuestionAnswer.IndexOf("<", 1, Len(UserQuestionAnswer) - 1)
                            Find_Character8 = UserQuestionAnswer.IndexOf(">", 1, Len(UserQuestionAnswer) - 1)
                            Find_Character9 = UserQuestionAnswer.IndexOf("|", 1, Len(UserQuestionAnswer) - 1)

                            If ((Find_Character1 <> -1) Or (Find_Character2 <> -1) Or (Find_Character3 <> -1) Or (Find_Character4 <> -1) Or (Find_Character5 <> -1) Or (Find_Character6 <> -1) Or (Find_Character7 <> -1) Or (Find_Character8 <> -1) Or (Find_Character9 <> -1)) Then
                                If Question.QuestionID = 1 Then
                                    clsTopMostMessageBox.Show("Serial Number cannot contain any of the following characters: \/:*?" + Chr(34) + "<>|", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                ElseIf Question.QuestionID = 2 Then
                                    clsTopMostMessageBox.Show("Work Order cannot contain any of the following characters: \/:*?" + Chr(34) + "<>|", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                OK_UserInputs.Value = Values.False
                                Return_Value.Add(False)
                            End If
                        End If
                    End If
                Next

                'Validate Choice
                'For Each Choice As UserChoiceInformation In ChoiceList
                For Each Choice As UUTChoice In _UutChoiceCollection
                    UserChoiceAnswer = CELProject.Test_Mgr_UUT_Choice(Choice.ChoiceID - 1).Value.ToString()

                    If Choice.Mandatory And CELProject.Test_Mgr_UUT_Choice(Choice.ChoiceID - 1).Value.ToString() = "" Then
                        clsTopMostMessageBox.Show(Choice.ChoiceTitle & " has an empty field", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        OK_UserInputs.Value = Values.False
                        Return_Value.Add(False)
                    End If
                Next

                'Validate options


                'Validates PwServiceBulletin
                If Not IsNothing(_cboPwServiceBulletin(0)) AndAlso _cboPwServiceBulletin(0).Visible = True Then
                    If _cboPwServiceBulletin(0).SelectedIndex = -1 Then
                        clsTopMostMessageBox.Show("Service Bulletin has an empty field", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return_Value.Add(False)
                    End If
                End If
                'Validates PwBuildSpecification
                If Not IsNothing(_cboPwBuildSpecification) AndAlso _cboPwBuildSpecification.Visible = True Then
                    If _cboPwBuildSpecification.SelectedIndex = -1 Then
                        clsTopMostMessageBox.Show("Build Specification has an empty field", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return_Value.Add(False)
                    End If
                End If


            Catch ex As Exception
                clsTopMostMessageBox.Show("OpenTest_Sequence: Validate_Answer: " & ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return_Value.Add(False)
            End Try

            ValidateAnswer = Not Return_Value.Contains(False)

        End Function

        Public Function FindPWCEECPartNumber(ByRef engineModel As String) As String

            Dim PWCEEPartNumber As String = "Default"

            'SelectString = "SELECT * FROM UUT_Question WHERE Model = '" & engineModel & "' ORDER BY QuestionID"
            esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
            Dim uutQuestionCollection As UUTQuestionCollection = New UUTQuestionCollection()
            uutQuestionCollection.es.Connection.Name = CELProject.DefaultConnectionName
            uutQuestionCollection.Query.Where(uutQuestionCollection.Query.ProjectID = CELProject.Project.Number)
            uutQuestionCollection.Query.Where(uutQuestionCollection.Query.EngineModel = engineModel)
            uutQuestionCollection.Query.Where(uutQuestionCollection.Query.Unavailable = False)
            uutQuestionCollection.Query.OrderBy(uutQuestionCollection.Query.QuestionID.Ascending)
            uutQuestionCollection.Query.Load()

            For Each question As UUTQuestion In uutQuestionCollection

                Dim index As Integer = CType(question.QuestionID.Value, Integer) 'Attention Commence a 1

                If CELProject.Test_Mgr_UUT_Answer(index - 1).Description.ToString = "PWC EEC P/N" Then
                    PWCEEPartNumber = CELProject.Test_Mgr_UUT_Answer(index - 1).Value.ToString
                End If

            Next

            esProviderFactory.Factory = New EntitySpaces.LoaderMT.esDataProviderFactory()
            Dim uutChoiceCollection As UUTChoiceCollection = New UUTChoiceCollection()
            uutChoiceCollection.es.Connection.Name = CELProject.DefaultConnectionName
            uutChoiceCollection.Query.Where(uutChoiceCollection.Query.ProjectID = CELProject.Project.Number)
            uutChoiceCollection.Query.Where(uutChoiceCollection.Query.EngineModel = engineModel)
            uutChoiceCollection.Query.Where(uutChoiceCollection.Query.Unavailable = False)
            uutChoiceCollection.Query.OrderBy(uutChoiceCollection.Query.ChoiceID.Ascending)
            uutChoiceCollection.Query.Load()

            For Each choice As UUTChoice In uutChoiceCollection

                Dim index As Integer = CType(choice.ChoiceID.Value, Integer)  'Attention Commence a 1
                If CELProject.Test_Mgr_UUT_Choice(index - 1).Description.ToString = "PWC EEC P/N" Then
                    PWCEEPartNumber = CELProject.Test_Mgr_UUT_Choice(index - 1).Value.ToString
                End If

            Next

            Return PWCEEPartNumber

        End Function


        Private disposedValue As Boolean = False        ' To detect redundant calls
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then

                    For i As Integer = 0 To _cboChoice.GetUpperBound(0) - 1
                        If Not _cboChoice(i) Is Nothing Then
                            _cboChoice(i).Visible = False
                            _lblChoice(i).Visible = False

                            RemoveHandler _cboChoice(i).SelectedIndexChanged, AddressOf cboChoice_SelectedIndexChanged
                            RemoveHandler _cboChoice(i).LostFocus, AddressOf cboChoice_SelectedIndexChanged
                        End If
                    Next
                    If Not (_cboPwBuildSpecification Is Nothing) Then
                        RemoveHandler _cboPwBuildSpecification.SelectedIndexChanged, AddressOf cboPwBuildSpecification_SelectedIndexChanged
                        RemoveHandler _cboPwBuildSpecification.LostFocus, AddressOf cboPwBuildSpecification_SelectedIndexChanged
                    End If

                    For i As Integer = 0 To _cboPwServiceBulletin.GetUpperBound(0)
                        If Not (_cboPwServiceBulletin(i) Is Nothing) Then

                            RemoveHandler _cboPwServiceBulletin(i).SelectedIndexChanged, AddressOf cboServiceBulletin_SelectedIndexChanged
                            RemoveHandler _cboPwServiceBulletin(i).LostFocus, AddressOf cboServiceBulletin_SelectedIndexChanged
                        End If
                    Next
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


Public Class clsTopMostMessageBox
    Public Shared Function Show(ByVal message As String, ByVal title As String, ByVal buttons As MessageBoxButtons, ByVal icons As MessageBoxIcon) As DialogResult
        ' Create a host form that is a TopMost window which will be the 
        ' parent of the clsTopMostMessageBox.
        Dim topmostForm As Form = New Form()
        topmostForm.Text = "CEL POPUP WINDOW"
        topmostForm.Name = "CEL POPUP WINDOW"
        ' new form should not be visible so position it off the visible screen and make it as small as possible
        topmostForm.Size = New System.Drawing.Size(1, 1)
        topmostForm.StartPosition = FormStartPosition.Manual
        Dim rect As System.Drawing.Rectangle = SystemInformation.VirtualScreen
        topmostForm.Location = New System.Drawing.Point(rect.Bottom + 10, rect.Right + 10)
        topmostForm.Show()
        topmostForm.ShowInTaskbar = False
        ' Make this form the active form and make it TopMost
        topmostForm.Focus()
        topmostForm.BringToFront()
        topmostForm.TopMost = True
        ' Finally show the MessageBox with the form just created as its owner
        Dim result As DialogResult = MessageBox.Show(topmostForm, message, title, buttons, icons)
        'clean it up all the way
        topmostForm.Dispose()
        Return result
    End Function
End Class
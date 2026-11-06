Option Strict Off

Imports System
Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Diagnostics
Imports System.Collections
Imports System.Collections.Generic

Imports System.ComponentModel
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Imports CEL.Net.FtpClient
Imports CEL.HMI.ExportInformationElement
Imports CEL.Data.BusinessObjects

Namespace CEL.HMI


    ''' <summary>
    ''' Contain the functions and subs used for exporting the Csv onto the Qnx Server.
    ''' </summary>
    ''' <remarks>Note that this library is highly dependante on proper configuration file and said file should be loaded before using most of the function.</remarks>
    Public Class HardwareConfiguration
        Private Shared Property CELProject As Object
        Private Shared m_CELProject As CELDAS = CELDAS.Instance()

        ''' <summary>
        ''' Ensure the Config file (celdas.cfg) is save size and same creating time. 
        ''' </summary>
        ''' <param name="UploadPath"></param>
        ''' <param name="serverIP"></param>
        ''' <param name="username"></param>
        ''' <param name="password"></param>
        ''' <returns>Return True if succesful; False otherwise.</returns>
        ''' <remarks>Compare the Time of creation and file size.</remarks>
        Public Function ConfigFileIntegrityVerification(ByVal UploadPath As String, ByVal serverIP As String, ByVal username As String, ByVal password As String) As Boolean
            Try
                'Creation de connection ftp
                Dim ftp As CEL.Net.FtpClient = InitFTPConnection(False, serverIP, username, password, True)
                Dim ftpFileList() As String = ftp.GetFileList()
                Dim fileposition As Boolean = File.Exists(m_CELProject.Project.WorkbenchConfigurationFullFileName)
                Dim ftpFilePositionExist As Boolean = False

                ''Return if the exact FileName exist within the directory.
                ''If a file is not found, the return value is False, True if found
                For Each strFtpFileName As String In ftpFileList
                    If strFtpFileName.CompareTo("celdas.cfg") = 0 Then ftpFilePositionExist = True
                Next strFtpFileName

                'If file is not uploaded and doesn't exist in the export folder.
                If ftpFilePositionExist = False Then
                    If fileposition = False Then
                        MsgBox("The *.cfg file is missing from the Qnx AND the expected export folder! Please Verify.")
                        Return False
                    End If
                    'If FIle is missing from QnX, just upload it.
                    If fileposition = True Then
                        MsgBox("The *.cfg was missing and was reuploaded from the config folder.")
                        ConstantCreationTimeFtpUpload(m_CELProject.Project.WorkbenchConfigurationFullFileName, "celdas.cfg", ftp)
                    End If

                End If
                'If config file is present on the FTP.
                If ftpFilePositionExist <> False Then
                    'if export file is missing, don't do Discrepancy check. (Won't do anything).
                    If fileposition = False Then
                        MsgBox("The *.cfg file is on the Qnx Server but is missing from the export folder! Integrity cannot be established.")
                    End If
                    'Check integrity if everything required is present.
                    If fileposition = True Then
                        If ConfigFileDiscrepancyDetector(m_CELProject.Project.WorkbenchConfigurationFullFileName, "celdas.cfg", ftp) Then
                            Dim result As Integer = clsTopMostMessageBox.Show("There is a Discrepancy with, " & "CELDAS.cfg" & ". Would you like to UPLOAD it again?", "CELDAS.cfg", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If result = DialogResult.Yes Then
                                ConstantCreationTimeFtpUpload(m_CELProject.Project.WorkbenchConfigurationFullFileName, "celdas.cfg", ftp)
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Assert(False, "Error From: CEL.HMI.HardWareConfiguration.ConfigFileIntegrityVerification: ", ex.Message)
                Return False
            End Try
            Console.WriteLine("Config file on Qnx is up to date.")
            Return True
        End Function

        ''' <summary>
        ''' Verify the integrity of the csv files on the ftp server. These files are compared by their creation date and their size to the original files on the windows cpu
        ''' </summary>
        ''' <param name="UploadPath"></param>
        ''' <param name="ServerIP"></param>
        ''' <param name="Username"></param>
        ''' <param name="Password"></param>
        ''' <returns>Return True if successful; False if error or exception</returns>
        ''' <remarks></remarks>
        Public Function CsvConfigIntegrityVerification(ByVal UploadPath As String, ByVal serverIP As String, ByVal username As String, ByVal password As String) As Boolean
            Try
                'Init ftp connection
                Dim ftp As CEL.Net.FtpClient = InitFTPConnection(False, serverIP, username, password, True)
                ftp.ChangeCreateDir(UploadPath)
                Dim ftpFileList() As String = ftp.GetFileList()

                'Decide what interfaces to select.
                Dim exportEngine As IExportEngine = ExportEngineSelection(m_CELProject.ExportType)

                'check if directory exist, if not, create it.
                CheckFolderExistance(m_CELProject.ExportPathName)

                Dim csvArray As New List(Of String)
                'Retrieve the name of the files in the export directory.
                Dim directoryName As DirectoryInfo = New DirectoryInfo(m_CELProject.ExportPathName)
                Dim fileSystemInformation() As FileSystemInfo = directoryName.GetFileSystemInfos("*.csv")

                For Each strfilename As FileInfo In fileSystemInformation
                    csvArray.Add(strfilename.Name)
                Next strfilename

                'For each tableName that need to be exported
                For Each csvCfg As ExportFormatInformationItem In m_CELProject.LoadFormatCollection
                    Dim filePositionExist As Boolean = False
                    Dim ftpFilePositionExist As Boolean = False
                    'The Omnibusbox UART CSV file is now generated dynamically
                    If csvCfg.FileName <> "DACN1_UART_OMNIBUSBOX_CSV.csv" Then
                        ''Return if the exact FileName exist within the directory.
                        ''If a file is not found, the return value is False, True if found
                        For Each strFileName As String In csvArray
                            If strFileName.CompareTo(csvCfg.FileName) = 0 Then filePositionExist = True
                        Next strFileName
                        For Each strFtpFileName As String In ftpFileList
                            If strFtpFileName.CompareTo(csvCfg.FileName) = 0 Then ftpFilePositionExist = True
                        Next strFtpFileName

                        ''Check if the exact filename is in the export folder. If not reexport
                        If filePositionExist = False Then
                            ExportCsv(csvCfg.TableName)
                            Console.WriteLine("This file, {0} was recreated", csvCfg.FileName)
                        End If

                        ''Check if the exact filename is in the upload folder. If not reupload 
                        If ftpFilePositionExist = False Then
                            ConstantCreationTimeFtpUpload(m_CELProject.ExportPathName & csvCfg.FileName, csvCfg.FileName, ftp)
                            Console.WriteLine("This file, {0}, did not exist on the ftp upload directory and was reuploaded.", csvCfg.FileName)
                        End If

                        ''Compare size and time creation. If ftpFile
                        If ftpFilePositionExist And filePositionExist Then
                            If ConfigFileDiscrepancyDetector(m_CELProject.ExportPathName & csvCfg.FileName, csvCfg.FileName, ftp) Then
                                Dim result As Integer = clsTopMostMessageBox.Show("There is a Discrepancy with, " & csvCfg.FileName.ToString & ". Would you like to EXPORT AND UPLOAD it again?", csvCfg.FileName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                                If result = DialogResult.Yes Then
                                    ExportCsv(csvCfg.TableName)
                                    ConstantCreationTimeFtpUpload(m_CELProject.ExportPathName & csvCfg.FileName, csvCfg.FileName, ftp)
                                    Console.WriteLine("A discrepancy between the uploaded file creation time and the exported file creation time" & _
                                                      "was detected in, {0}, the file was recreated and re-uploaded", csvCfg.FileName)

                                ElseIf result = DialogResult.No Then
                                    Dim result2 As Integer = clsTopMostMessageBox.Show("There is a Discrepancy with, " & csvCfg.FileName.ToString & ". Would you like to JUST UPLOAD it again?", csvCfg.FileName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                    If result2 = DialogResult.Yes And result = DialogResult.No Then
                                        ConstantCreationTimeFtpUpload(m_CELProject.ExportPathName & csvCfg.FileName, csvCfg.FileName, ftp)
                                        Console.WriteLine("A discrepancy between the uploaded file creation time and the exported file creation time" & _
                                                          "was detected in, {0}, the file was re-uploaded", csvCfg.FileName)
                                    End If
                                End If
                            End If
                        End If
                        Console.WriteLine("{0} Seem to have be up to date.", csvCfg.FileName)
                        Console.WriteLine("")
                    End If
                Next csvCfg

            Catch ex As Exception
                Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.CsvUploadIntegrityChecker: There was an error: Verify configuration?", ex.Message)
                Return False
            End Try

            Console.WriteLine("All CSV Config files should be up to date.")
            Return True
        End Function

        ''' <summary>
        ''' Connect to a server and return the handle to use other ftp command
        ''' </summary>
        ''' <param name="ipConnection"></param>
        ''' <param name="username"></param>
        ''' <param name="password"></param>
        ''' <returns>an ftp connection (CEL.NET.FTPClient)</returns>
        ''' <remarks>Remote path by default is the root directory and the remote port by default is 21.</remarks>
        Public Function InitFTPConnection(ByVal verboseDebug As Boolean, ByVal ipConnection As String, ByVal username As String, ByVal password As String, ByVal binaryMode As Boolean, _
                                                Optional ByVal remotePort As Integer = Nothing, Optional ByVal remotePath As String = Nothing) As CEL.Net.FtpClient
            Dim ftp As New CEL.Net.FtpClient
            Try
                ftp.VerboseDebugging = verboseDebug
                ftp.Server = ipConnection
                ftp.Username = username
                ftp.Password = password
                If Not remotePath Is Nothing Then
                    ftp.RemotePath = remotePath
                Else
                    ftp.RemotePath = "./"
                End If
                If remotePort <> Nothing Then
                    ftp.RemotePort = remotePort
                Else
                    ftp.RemotePort = 21
                End If

                ftp.Login()
                ftp.BinaryMode = binaryMode

            Catch ex As Exception
                Debug.Assert(False, "ERROR: CEL.HMI.HarwareConfiguration.InitFTPConnection: Problem Connecting to ftp.", ex.Message)
            End Try
            Return ftp
        End Function

        ''' <summary>
        ''' Export all the CSVs by successively calling Export csv and feeding it the tableName found in the config file.
        ''' </summary>
        ''' <param name="configFilePathName"></param>
        ''' <returns>Return True if successful</returns>
        ''' <remarks></remarks>
        ''' 
        Public Function ExportAllCsv(Optional ByVal configFilePathName As String = Nothing, Optional ByRef bgWorker As BackgroundWorker = Nothing) As Boolean
            Try
                If Nothing <> configFilePathName Then
                    UsingConfigurationManager.GetAppConfiguration(configFilePathName)
                End If
                Dim i As Integer = 0
                'cycle through the config file, exporting each tableName.
                For Each element As ExportFormatInformationItem In m_CELProject.LoadFormatCollection
                    'The Omnibusbox UART CSV file is now generated dynamically
                    If bgWorker IsNot Nothing Then
                        bgWorker.ReportProgress(i, element.TableName)
                        i += 1
                    End If
                    ExportCsv(element.TableName)
                Next
            Catch ex As Exception
                Debug.Assert(False, "Error From: CEL.HMI.HardWareConfiguration.ExportAllCsv", ex.Message)
                Return False
            End Try
            Console.WriteLine("Export Complete!")
            Return True
        End Function

        ''' <summary>
        ''' Export one CSV as long as the provided information are correct
        ''' </summary>
        ''' <param name="tablename"></param>
        ''' <param name="exportEngine"></param>
        ''' <returns>return True if succcesful; False if otherwise.</returns>
        ''' <remarks></remarks>
        Public Function ExportCsv(ByVal tablename As String) As Boolean
            Try
                Console.WriteLine("Exporting:{0}", tablename)
                'Decide what interface to select.
                Dim exportEngine As IExportEngine = ExportEngineSelection(m_CELProject.ExportType)

                Select Case tablename
                    Case "DACN1_NIDAQ_IOMGR_Bool"
                        exportEngine.ExportNidaqBoolean(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_Serial_IOMGR_Bool"
                        exportEngine.ExportSerialBoolean(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_NIDAQ_IOMGR_Real"
                        exportEngine.ExportNidaqReal(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_Serial_IOMGR_Real"
                        exportEngine.ExportSerialReal(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_ARINC429_LABEL"
                        exportEngine.ExportArinc429Label(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_PowerDNA_IOMGR_Real_1"
                        exportEngine.ExportPowerDNA1(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_PowerDNA_IOMGR_Real_2"
                        exportEngine.ExportPowerDNA2(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_OmniBusBox_IOMGR_Uart"
                        exportEngine.ExportOmniBusBox(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_SCANIVALVE_IOMGR_REAL"
                        exportEngine.ExportScaniValveReal(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_AFDX_PORT"
                        exportEngine.ExportAfdxPort(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_AFDX_VIRTUAL_LINK"
                        exportEngine.ExportAfdxVirtualLink(m_CELProject.ExportPathName(), tablename)
                    Case "DACN1_AFDX_SIGNAL"
                        exportEngine.ExportAfdxSignal(m_CELProject.ExportPathName(), tablename)
                    Case Else
                        Debug.Assert(False, "HardwareConfiguration.ExportAllCsv: Invalid table name " + tablename)
                        Console.WriteLine("This tableName has no associated export code: {0}", tablename)
                        Return False
                End Select
            Catch ex As Exception
                Debug.Assert(False, "Error: CEL.NET.HardwareConfiguration.ExportCSV: Configuration error? Non Existant tablename Code? Does Db Exist?")
            End Try
            Return True
        End Function

        'interface for the csv export when csv delimited
        Public Interface IExportEngine
            Function ExportNidaqBoolean(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportNidaqReal(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportSerialBoolean(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportSerialReal(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportArinc429Label(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportAfdxPort(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportAfdxSignal(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportAfdxVirtualLink(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportPowerDNA1(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportPowerDNA2(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportScaniValveReal(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
            Function ExportOmniBusBox(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean
        End Interface

        'function in the interface when CSVDelimited
        Public Class CsvExportEngine
            Implements IExportEngine

            Public Function ExportAfdxPort(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportAfdxPort
                Dim csvDelimited As String = ","
                Try
                    Dim tableCollection As New CEL.Data.BusinessObjects.Dacn1AfdxPortCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.LoadAll()

                    Dim tableVirtualLinkCollection As New CEL.Data.BusinessObjects.Dacn1AfdxVirtualLinkCollection
                    tableVirtualLinkCollection.Query.es.QuerySource = "DACN1_AFDX_VIRTUAL_LINK"
                    tableVirtualLinkCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableVirtualLinkCollection.Query.Where(tableVirtualLinkCollection.Query.ExportToCsv = True)
                    tableVirtualLinkCollection.LoadAll()

                    Dim tableMessageCollection As New CEL.Data.BusinessObjects.Dacn1AfdxMessageCollection
                    tableMessageCollection.Query.es.QuerySource = "DACN1_AFDX_MESSAGE"
                    tableMessageCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableMessageCollection.Query.Where(tableMessageCollection.Query.ExportToCsv = True)
                    tableMessageCollection.LoadAll()

                    Dim tableSignalCollection As New CEL.Data.BusinessObjects.Dacn1AfdxSignalCollection
                    tableSignalCollection.Query.es.QuerySource = "DACN1_AFDX_SIGNAL"
                    tableSignalCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableSignalCollection.Query.Where(tableSignalCollection.Query.ExportToCsv = True)
                    tableSignalCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)
                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        writer = New StreamWriter((pathName + exportFormatConfig.FileName))
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.Create)
                        writer = New StreamWriter(fs, encoding)
                    End If
                    ''Nous mettons les collections en ordre selon le choix selectionné. The ordered collection are in tas|tp|tam|order Object.
                    Dim tableAdfxSignal As Object = From tableSignal In CType(tableSignalCollection, IEnumerable)
                                                Order By tableSignal.CollectionName, tableSignal.Vlid, tableSignal.UdpPortSource, tableSignal.UdpPortDestination, tableSignal.StartBit

                    Dim tableAdfxPort As Object = From tablePort In CType(tableCollection, IEnumerable)
                                                Order By tablePort.CollectionName, tablePort.Vlid, tablePort.UdpPortSource, tablePort.UdpPortDestination

                    Dim tableAdfxMessage As Object = From tableMessage In CType(tableMessageCollection, IEnumerable)
                                                Order By tableMessage.CollectionName, tableMessage.Vlid, tableMessage.UdpPortSource, tableMessage.UdpPortDestination, tableMessage.FdsName

                    Dim tableAdfxVirtualLink As Object = From tableVirtualLink In CType(tableVirtualLinkCollection, IEnumerable)
                                            Order By tableVirtualLink.collectionName, tableVirtualLink.Vlid

                    'Merging la table ADFX VirtualLink et la table des ADFX Port
                    Dim joinedVirtualLinkPort As Object = From tableVirtualLink In CType(tableAdfxVirtualLink, IEnumerable)
                                                 Join tablePort In CType(tableAdfxPort, IEnumerable) _
                                                 On tableVirtualLink.collectionName Equals tablePort.collectionName _
                                                 And tableVirtualLink.Vlid Equals tablePort.Vlid
                                                 Where tableVirtualLink.ExportToCsv = True And tablePort.ExportToCSV = True
                                                 Select tableVirtualLink.CollectionName, tableVirtualLink.vlid, tablePort.PortID, tablePort.UdpPortSource, tablePort.UdpPortDestination, _
                                                 tablePort.name, tablePort.TxRxenum, tablePort.portMode, tablePort.period, tablePort.periodskew, tablePort.endsystemSource, _
                                                 tablePort.partitionSource, tablePort.EndSystemDestination, tablePort.PartitionDestination, _
                                                 tablePort.subVLID, tablePort.PortTypeEnum, tablePort.messageLength, tablePort.depth, tablePort.Buffersize, tablePort.tracingEnabled

                    'Merging la table ADFX Message et la table precedemment mergé
                    Dim joinedTableVirtualLinkPortMessage As Object = From joinedTable In CType(joinedVirtualLinkPort, IEnumerable)
                                                  Join tableMessage In CType(tableAdfxMessage, IEnumerable) _
                                                  On joinedTable.CollectionName Equals tableMessage.CollectionName _
                                                  And joinedTable.Vlid Equals tableMessage.Vlid _
                                                  And joinedTable.UdpPortSource Equals tableMessage.UdpPortSource _
                                                  And joinedTable.UdpPortDestination Equals tableMessage.UdpPortDestination
                                                  Where tableMessage.ExportToCSV = True
                                                  Select joinedTable.CollectionName, joinedTable.Vlid, joinedTable.udpPortsource, joinedTable.UdpPortDestination, joinedTable.PortID, _
                                                  joinedTable.name, joinedTable.TxRxenum, joinedTable.portMode, joinedTable.period, joinedTable.periodskew, joinedTable.endsystemSource, _
                                                  joinedTable.partitionSource, joinedTable.EndSystemDestination, joinedTable.PartitionDestination, _
                                                  joinedTable.subVLID, joinedTable.PortTypeEnum, joinedTable.messageLength, joinedTable.depth, joinedTable.Buffersize, joinedTable.tracingEnabled, _
                                                  tableMessage.FdsName

                    'Merging la table ADFX SIGNAL et la table precedemment mergé
                    Dim joinedAllTab As Object = From tableSignal In CType(tableAdfxSignal, IEnumerable)
                                                          Join joinedVirtualLinkMessagePort In CType(joinedTableVirtualLinkPortMessage, IEnumerable) _
                                                          On tableSignal.collectionName Equals joinedVirtualLinkMessagePort.CollectionName _
                                                          And tableSignal.Vlid Equals joinedVirtualLinkMessagePort.Vlid _
                                                          And tableSignal.UdpPortSource Equals joinedVirtualLinkMessagePort.UdpPortSource _
                                                          And tableSignal.UdpPortDestination Equals joinedVirtualLinkMessagePort.UdpPortDestination _
                                                          And tableSignal.FdsName Equals joinedVirtualLinkMessagePort.FdsName
                                                          Where tableSignal.ExportToCsv = True
                                                          Select joinedVirtualLinkMessagePort.Vlid, joinedVirtualLinkMessagePort.UdpPortDestination, joinedVirtualLinkMessagePort.UdpPortSource, _
                                                          joinedVirtualLinkMessagePort.Name, joinedVirtualLinkMessagePort.Txrxenum, joinedVirtualLinkMessagePort.PortMode, _
                                                          joinedVirtualLinkMessagePort.portID, joinedVirtualLinkMessagePort.period, joinedVirtualLinkMessagePort.periodSkew, _
                                                          joinedVirtualLinkMessagePort.endsystemsource, joinedVirtualLinkMessagePort.PartitionSource, joinedVirtualLinkMessagePort.endsystemdestination, _
                                                          joinedVirtualLinkMessagePort.partitiondestination, joinedVirtualLinkMessagePort.SubVLID, joinedVirtualLinkMessagePort.PortTypeEnum, _
                                                          joinedVirtualLinkMessagePort.messageLength, joinedVirtualLinkMessagePort.Depth, joinedVirtualLinkMessagePort.BufferSize, _
                                                          joinedVirtualLinkMessagePort.TracingEnabled, joinedVirtualLinkMessagePort.collectionName

                    ''sorting and selection of what we want
                    Dim finalColl As Object = From coll In CType(joinedAllTab, IEnumerable)
                        Order By coll.CollectionName, coll.Vlid, coll.UdpPortDestination, coll.UdpPortSource, coll.PortID
                    Select coll.CollectionName, coll.VLID, coll.UdpPortDestination, coll.UdpPortSource, coll.Name, coll.TxRxEnum, coll.PortMode, coll.PortID, coll.Period, coll.PeriodSkew, _
                    coll.EndSystemSource, coll.PartitionSource, coll.EndSystemDestination, coll.PartitionDestination, coll.SubVLID, coll.PortTypeEnum, coll.MessageLength, coll.Depth, _
                    coll.BufferSize, coll.TracingEnabled
                    Distinct
                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}", _
                                                              csvDelimited, _
                                                              "CollectionName", "Vlid", "UdpPortDestination", "UdpPortSource", "Name", "TxRxEnum", "PortMode", "PortID", "Period", "PeriodSkew", _
                                                              "EndSystemSource", "PartitionSource", "EndSystemDestination", "PartitionDestination", "SubVLID", "PortTypeEnum", "MessageLenght", "Depth", _
                                                              "BufferSize", "TracingEnabled")
                        writer.WriteLine(strTemp)
                    End If

                    ''put into string so we can put it into the CSV
                    Dim record As Object
                    For Each record In finalColl
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}", _
                                                              csvDelimited, _
                                                              FormatRecord(record.CollectionName, exportFormatConfig), _
                                                              FormatRecord(record.Vlid, exportFormatConfig), _
                                                              FormatRecord(record.UdpPortDestination, exportFormatConfig), _
                                                              FormatRecord(record.UdpPortSource, exportFormatConfig), _
                                                              FormatRecord(record.Name, exportFormatConfig), _
                                                              FormatRecord(record.TxRxEnum, exportFormatConfig), _
                                                              FormatRecord(record.PortMode, exportFormatConfig), _
                                                              FormatRecord(record.PortID, exportFormatConfig), _
                                                              FormatRecord(record.Period, exportFormatConfig), _
                                                              FormatRecord(record.PeriodSkew, exportFormatConfig), _
                                                              FormatRecord(record.EndSystemSource, exportFormatConfig), _
                                                              FormatRecord(record.PartitionSource, exportFormatConfig), _
                                                              FormatRecord(record.EndSystemDestination, exportFormatConfig), _
                                                              FormatRecord(record.PartitionDestination, exportFormatConfig), _
                                                              FormatRecord(record.SubVLID, exportFormatConfig), _
                                                              FormatRecord(record.PortTypeEnum, exportFormatConfig), _
                                                              FormatRecord(record.MessageLength, exportFormatConfig), _
                                                              FormatRecord(record.Depth, exportFormatConfig), _
                                                              FormatRecord(record.BufferSize, exportFormatConfig), _
                                                              FormatRecord(record.TracingEnabled, exportFormatConfig))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportAfdxPort : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportAFDXPort : Could not export properly")
                    Return False
                End Try


                Return True
            End Function

            Public Function ExportAfdxSignal(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportAfdxSignal
                Dim csvDelimited As String = ","
                Try
                    Dim tableSignalCollection As New CEL.Data.BusinessObjects.Dacn1AfdxSignalCollection
                    tableSignalCollection.Query.es.QuerySource = tableName
                    tableSignalCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableSignalCollection.Query.Where(tableSignalCollection.Query.ExportToCsv = True)
                    tableSignalCollection.LoadAll()

                    Dim tableMessageCollection As New CEL.Data.BusinessObjects.Dacn1AfdxMessageCollection
                    tableMessageCollection.Query.es.QuerySource = "DACN1_AFDX_MESSAGE"
                    tableMessageCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableMessageCollection.Query.Where(tableMessageCollection.Query.ExportToCsv = True)
                    tableMessageCollection.LoadAll()

                    Dim tablePortCollection As New CEL.Data.BusinessObjects.Dacn1AfdxPortCollection
                    tablePortCollection.Query.es.QuerySource = "DACN1_AFDX_PORT"
                    tablePortCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tablePortCollection.Query.Where(tablePortCollection.Query.ExportToCsv = True)
                    tablePortCollection.LoadAll()

                    Dim tableVirtualLinkCollection As New CEL.Data.BusinessObjects.Dacn1AfdxVirtualLinkCollection
                    tableVirtualLinkCollection.Query.es.QuerySource = "DACN1_AFDX_VIRTUAL_LINK"
                    tableVirtualLinkCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableVirtualLinkCollection.Query.Where(tableVirtualLinkCollection.Query.ExportToCsv = True)
                    tableVirtualLinkCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)
                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        writer = New StreamWriter((pathName + exportFormatConfig.FileName))
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If

                    ''Nous mettons les collections en ordre selon le choix selectionné. The ordered collection are in tas|tp|tam|order Object.
                    Dim tableAdfxSignal As Object = From tableSignal In CType(tableSignalCollection, IEnumerable)
                                                Order By tableSignal.CollectionName, tableSignal.Vlid, tableSignal.UdpPortSource, tableSignal.UdpPortDestination, tableSignal.StartBit

                    Dim tableAdfxPort As Object = From tablePort In CType(tablePortCollection, IEnumerable)
                                                Order By tablePort.CollectionName, tablePort.Vlid, tablePort.UdpPortSource, tablePort.UdpPortDestination

                    Dim tableAdfxMessage As Object = From tableMessage In CType(tableMessageCollection, IEnumerable)
                                                Order By tableMessage.CollectionName, tableMessage.Vlid, tableMessage.UdpPortSource, tableMessage.UdpPortDestination, tableMessage.FdsName

                    Dim tableAdfxVirtualLink As Object = From tableVirtualLink In CType(tableVirtualLinkCollection, IEnumerable)
                                            Order By tableVirtualLink.collectionName, tableVirtualLink.Vlid

                    'Merging la table ADFX VirtualLink et la table des ADFX Port
                    Dim joinedTableVirtualLinkPort As Object = From tableVirtualLink In CType(tableAdfxVirtualLink, IEnumerable)
                                                 Join tablePort In CType(tableAdfxPort, IEnumerable) _
                                                 On tableVirtualLink.collectionName Equals tablePort.collectionName _
                                                 And tableVirtualLink.Vlid Equals tablePort.Vlid
                                                 Where tableVirtualLink.ExportToCsv = True And tablePort.ExportToCSV = True
                                                 Select tableVirtualLink.CollectionName, tableVirtualLink.vlid, tablePort.PortID, tablePort.UdpPortSource, tablePort.UdpPortDestination

                    'Merging la table ADFX Message et la table precedemment mergé
                    Dim joinedTableVirtualLinkPortMessage As Object = From joinedTable In CType(joinedTableVirtualLinkPort, IEnumerable)
                                                  Join tableMessage In CType(tableAdfxMessage, IEnumerable) _
                                                  On joinedTable.CollectionName Equals tableMessage.CollectionName _
                                                  And joinedTable.Vlid Equals tableMessage.Vlid _
                                                  And joinedTable.UdpPortSource Equals tableMessage.UdpPortSource _
                                                  And joinedTable.UdpPortDestination Equals tableMessage.UdpPortDestination
                                                  Where tableMessage.ExportToCSV = True
                                                  Select joinedTable.CollectionName, joinedTable.Vlid, joinedTable.udpPortsource, joinedTable.UdpPortDestination, joinedTable.PortID, _
                                                  tableMessage.FdsName, tableMessage.FdsAddress, tableMessage.FsAddress, tableMessage.FdsLength

                    'Merging la table ADFX SIGNAL et la table precedemment mergé
                    Dim joinedAllTab As Object = From tableSignal In CType(tableAdfxSignal, IEnumerable)
                                                          Join joinedTableVirtualLinkMessagePort In CType(joinedTableVirtualLinkPortMessage, IEnumerable) _
                                                          On tableSignal.collectionName Equals joinedTableVirtualLinkMessagePort.CollectionName _
                                                          And tableSignal.Vlid Equals joinedTableVirtualLinkMessagePort.Vlid _
                                                          And tableSignal.UdpPortSource Equals joinedTableVirtualLinkMessagePort.UdpPortSource _
                                                          And tableSignal.UdpPortDestination Equals joinedTableVirtualLinkMessagePort.UdpPortDestination _
                                                          And tableSignal.FdsName Equals joinedTableVirtualLinkMessagePort.FdsName
                                                          Where tableSignal.ExportToCsv = True
                                                          Select tableSignal.CollectionName, tableSignal.Vlid, tableSignal.UdpPortSource, tableSignal.UdpPortDestination, tableSignal.FdsName, _
                                                          joinedTableVirtualLinkMessagePort.FdsAddress, joinedTableVirtualLinkMessagePort.FdsLength, tableSignal.SignalAddress, _
                                                          joinedTableVirtualLinkMessagePort.FsAddress, tableSignal.ParameterName, tableSignal.TxRxenum, joinedTableVirtualLinkMessagePort.PortID, _
                                                          tableSignal.StartBit, tableSignal.StopBit, tableSignal.LSBValue, tableSignal.DataTypeEnum, tableSignal.CodingTypeEnum, tableSignal.MinValue, _
                                                          tableSignal.MaxValue, tableSignal.length

                    ''dernier classsement avant la sauvegarde des données
                    Dim collectionFinal As Object = From coll In CType(joinedAllTab, IEnumerable)
                              Order By coll.CollectionName, coll.Vlid, coll.UdpPortSource, coll.UdpPortDestination, coll.SignalAddress, coll.StartBit


                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}", _
                                                              csvDelimited, "CollectionName", "Vlid", "UdpPortSource", "UdpPortDestination", "FdsName", "FdsAddress", "FdsLength", "FsAddress", _
                                                              "SignalAddress", "ParameterName", "TxRxEnum", "PortID", "StartBit", "StopBit", "LSBValue", "DataTypeEnum", "CodingTypeEnum", _
                                                              "MinValue", "MaxValue", "Length")
                    End If

                    ''Create the string that concatonate the data to be written to the CSV file
                    Dim record As Object
                    For Each record In collectionFinal
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}", _
                                                                csvDelimited, _
                                                                FormatRecord(record.CollectionName, exportFormatConfig), _
                                                                FormatRecord(record.Vlid, exportFormatConfig), _
                                                                FormatRecord(record.UdpPortSource, exportFormatConfig), _
                                                                FormatRecord(record.UdpPortDestination, exportFormatConfig), _
                                                                FormatRecord(record.FdsName, exportFormatConfig), _
                                                                FormatRecord(record.FdsAddress, exportFormatConfig), _
                                                                FormatRecord(record.FdsLength, exportFormatConfig), _
                                                                FormatRecord(record.FsAddress, exportFormatConfig), _
                                                                FormatRecord(record.SignalAddress, exportFormatConfig), _
                                                                FormatRecord(record.ParameterName, exportFormatConfig), _
                                                                FormatRecord(record.TxRxEnum, exportFormatConfig), _
                                                                FormatRecord(record.PortID, exportFormatConfig), _
                                                                FormatRecord(record.StartBit, exportFormatConfig), _
                                                                FormatRecord(record.StopBit, exportFormatConfig), _
                                                                FormatRecord(record.LSBValue, exportFormatConfig, 10), _
                                                                FormatRecord(record.DataTypeEnum, exportFormatConfig), _
                                                                FormatRecord(record.CodingTypeEnum, exportFormatConfig), _
                                                                FormatRecord(record.MinValue, exportFormatConfig), _
                                                                FormatRecord(record.MaxValue, exportFormatConfig), _
                                                                FormatRecord(record.Length, exportFormatConfig))
                        ''write to file the created string
                        writer.WriteLine(strTemp)
                    Next

                    ''flush the string that write the data and close the file
                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportAfdxSignal : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportAFDXSignal : Could not export properly")
                    Return False
                End Try

                Return True
            End Function

            Public Function ExportAfdxVirtualLink(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportAfdxVirtualLink
                Dim csvDelimited As String = ","
                Try
                    Dim tableCollection As New CEL.Data.BusinessObjects.Dacn1AfdxVirtualLinkCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.LoadAll()

                    Dim tableSignalCollection As New CEL.Data.BusinessObjects.Dacn1AfdxSignalCollection
                    tableSignalCollection.Query.es.QuerySource = "DACN1_AFDX_SIGNAL"
                    tableSignalCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableSignalCollection.Query.Where(tableSignalCollection.Query.ExportToCsv = True)
                    tableSignalCollection.LoadAll()

                    Dim tableMessageCollection As New CEL.Data.BusinessObjects.Dacn1AfdxMessageCollection
                    tableMessageCollection.Query.es.QuerySource = "DACN1_AFDX_MESSAGE"
                    tableMessageCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableMessageCollection.Query.Where(tableMessageCollection.Query.ExportToCsv = True)
                    tableMessageCollection.LoadAll()

                    Dim tablePortCollection As New CEL.Data.BusinessObjects.Dacn1AfdxPortCollection
                    tablePortCollection.Query.es.QuerySource = "DACN1_AFDX_PORT"
                    tablePortCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tablePortCollection.Query.Where(tablePortCollection.Query.ExportToCsv = True)
                    tablePortCollection.LoadAll()

                    ''Nous mettons les collections en ordre selon le choix selectionné. The ordered collection are in tas|tp|tam|order Object.
                    Dim tableAdfxSignal As Object = From tableSignal In CType(tableSignalCollection, IEnumerable)
                                                Order By tableSignal.CollectionName, tableSignal.Vlid, tableSignal.UdpPortSource, tableSignal.UdpPortDestination, tableSignal.StartBit

                    Dim tableAdfxPort As Object = From tablePort In CType(tablePortCollection, IEnumerable)
                                                Order By tablePort.CollectionName, tablePort.Vlid, tablePort.UdpPortSource, tablePort.UdpPortDestination

                    Dim tableAdfxMessage As Object = From tableMessage In CType(tableMessageCollection, IEnumerable)
                                                Order By tableMessage.CollectionName, tableMessage.Vlid, tableMessage.UdpPortSource, tableMessage.UdpPortDestination, tableMessage.FdsName

                    Dim tableAdfxVirtualLink As Object = From tableVirtualLink In CType(tableCollection, IEnumerable)
                                            Order By tableVirtualLink.collectionName, tableVirtualLink.Vlid

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)
                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        writer = New StreamWriter((pathName + exportFormatConfig.FileName))
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If

                    'Merging la table ADFX VirtualLink et la table des ADFX Port
                    Dim joinedTableVirtualLinkPort As Object = From tableVirtualLink In CType(tableAdfxVirtualLink, IEnumerable)
                             Join tablePort In CType(tableAdfxPort, IEnumerable) _
                             On tableVirtualLink.collectionName Equals tablePort.collectionName _
                             And tableVirtualLink.Vlid Equals tablePort.Vlid
                             Where tableVirtualLink.ExportToCsv = True And tablePort.ExportToCSV = True
                             Select tableVirtualLink.CollectionName, tableVirtualLink.vlid, tablePort.UdpPortSource, tablePort.UdpPortDestination, tablePort.portID, _
                                    tableVirtualLink.VLMode, tableVirtualLink.NumberofSubVL, tableVirtualLink.SubVlid, tableVirtualLink.TxRxEnum, tableVirtualLink.bag, _
                                    tableVirtualLink.MTU, tableVirtualLink.MaxJitter, tableVirtualLink.SkewMax, tableVirtualLink.SkewDelay, tableVirtualLink.prevSnRange, _
                                    tableVirtualLink.eDEEnabled, tableVirtualLink.eDESid, tableVirtualLink.eDESkewMax, tableVirtualLink.eDEAgeMax, tableVirtualLink.EDELagMax

                    'Merging la table ADFX Message et la table precedemment mergé
                    Dim joinedTableVirtualLinkPortMessage As Object = From joinedTableVlP In CType(joinedTableVirtualLinkPort, IEnumerable)
                                                  Join tableMessage In CType(tableAdfxMessage, IEnumerable) _
                                                  On joinedTableVlP.CollectionName Equals tableMessage.CollectionName _
                                                  And joinedTableVlP.Vlid Equals tableMessage.Vlid _
                                                  And joinedTableVlP.UdpPortSource Equals tableMessage.UdpPortSource _
                                                  And joinedTableVlP.UdpPortDestination Equals tableMessage.UdpPortDestination
                                                  Where tableMessage.ExportToCSV = True
                                                  Select joinedTableVlP.CollectionName, joinedTableVlP.Vlid, joinedTableVlP.udpPortsource, joinedTableVlP.UdpPortDestination, _
                                                         tableMessage.FdsName, joinedTableVlP.portID, joinedTableVlP.VLMode, joinedTableVlP.NumberofSubVL, joinedTableVlP.SubVlid, _
                                                         joinedTableVlP.TxRxEnum, joinedTableVlP.bag, joinedTableVlP.MTU, joinedTableVlP.MaxJitter, joinedTableVlP.SkewMax, joinedTableVlP.SkewDelay, _
                                                         joinedTableVlP.prevSnRange, joinedTableVlP.eDEEnabled, joinedTableVlP.eDESid, joinedTableVlP.eDESkewMax, joinedTableVlP.eDEAgeMax, _
                                                         joinedTableVlP.EDELagMax

                    'Merging la table ADFX SIGNAL et la table precedemment mergé
                    Dim joinedAllTab As Object = From tableSignal In CType(tableAdfxSignal, IEnumerable)
                                                          Join tablePortMessageVirtualLink In CType(joinedTableVirtualLinkPortMessage, IEnumerable) _
                                                          On tableSignal.collectionName Equals tablePortMessageVirtualLink.CollectionName _
                                                          And tableSignal.Vlid Equals tablePortMessageVirtualLink.Vlid _
                                                          And tableSignal.UdpPortSource Equals tablePortMessageVirtualLink.UdpPortSource _
                                                          And tableSignal.UdpPortDestination Equals tablePortMessageVirtualLink.UdpPortDestination _
                                                          And tableSignal.FdsName Equals tablePortMessageVirtualLink.FdsName
                                                          Where tableSignal.ExportToCsv = True
                                                          Select tablePortMessageVirtualLink.collectionName, tablePortMessageVirtualLink.Vlid, tablePortMessageVirtualLink.UdpPortDestination, _
                                                                 tablePortMessageVirtualLink.UdpPortSource, tablePortMessageVirtualLink.PortID, _
                                                                 tablePortMessageVirtualLink.VLMode, tablePortMessageVirtualLink.NumberofSubVL, tablePortMessageVirtualLink.SubVlid, _
                                                                 tablePortMessageVirtualLink.TxRxEnum, tablePortMessageVirtualLink.bag, tablePortMessageVirtualLink.MTU, _
                                                                 tablePortMessageVirtualLink.MaxJitter, tablePortMessageVirtualLink.SkewMax, tablePortMessageVirtualLink.SkewDelay, _
                                                                 tablePortMessageVirtualLink.prevSnRange, tablePortMessageVirtualLink.eDEEnabled, tablePortMessageVirtualLink.eDESid, _
                                                                 tablePortMessageVirtualLink.eDESkewMax, tablePortMessageVirtualLink.eDEAgeMax, tablePortMessageVirtualLink.EDELagMax
                    'sorting et selection Distinct
                    Dim collectionFinal As Object = From coll In CType(joinedAllTab, IEnumerable)
                                     Order By coll.CollectionName, coll.Vlid, coll.UdpPortDestination, coll.UdpPortSource, coll.PortID
                                     Select coll.CollectionName, coll.VLID, coll.VLMode, coll.NumberOfSubVL, coll.SubVLID, coll.TxRxEnum, coll.BAG, coll.MTU, coll.MaxJitter, coll.SkewMax, _
                                            coll.SkewDelay, coll.PrevSnRange, coll.EDEEnabled, coll.EDESid, coll.EDESkewMax, coll.EDEAgeMax, coll.EDELagMax
                                     Distinct

                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}", _
                                                              csvDelimited, _
                                                              "CollectionName", "Vlid", "VLMode", "NumberOfSubVL", "SubVLID", "TxRxEnum", "Bag", "Mtu", "MaxJitter", "SkewMax", _
                                                              "SkewDelay", "PrevSnRange", "EDEEnabled", "EDESid", "EDESkewMax", "EDEAgeMax", "EDELagMax")
                        writer.WriteLine(strTemp)
                    End If

                    'String Created to save data in the CSV
                    Dim record As Object
                    For Each record In collectionFinal
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}", _
                                                                                        csvDelimited, _
                                                                                        FormatRecord(record.CollectionName, exportFormatConfig), _
                                                                                        FormatRecord(record.Vlid, exportFormatConfig), _
                                                                                        FormatRecord(record.VLMode, exportFormatConfig), _
                                                                                        FormatRecord(record.NumberOfSubVL, exportFormatConfig), _
                                                                                        FormatRecord(record.SubVLID, exportFormatConfig), _
                                                                                        FormatRecord(record.TxRxEnum, exportFormatConfig), _
                                                                                        FormatRecord(record.Bag, exportFormatConfig), _
                                                                                        FormatRecord(record.Mtu, exportFormatConfig), _
                                                                                        FormatRecord(record.MaxJitter, exportFormatConfig), _
                                                                                        FormatRecord(record.SkewMax, exportFormatConfig), _
                                                                                        FormatRecord(record.SkewDelay, exportFormatConfig), _
                                                                                        FormatRecord(record.PrevSnRange, exportFormatConfig), _
                                                                                        FormatRecord(record.EDEEnabled, exportFormatConfig), _
                                                                                        FormatRecord(record.EDESid, exportFormatConfig), _
                                                                                        FormatRecord(record.EDESkewMax, exportFormatConfig), _
                                                                                        FormatRecord(record.EDEAgeMax, exportFormatConfig), _
                                                                                        FormatRecord(record.EDELagMax, exportFormatConfig))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportAFDXVirtualLink : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportAFDXVirtualLink : Could not export properly")
                    Return False
                End Try

                Return True
            End Function

            Public Function ExportArinc429Label(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportArinc429Label
                Dim csvDelimited As String = ","
                Try
                    Dim tableCollection As New CEL.Data.BusinessObjects.ARINC429LabelTemplateCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)

                    'Way to force extended ascii encoding within the streamwriter this is necessary for ° and other character. Since ascii is 7bit and extended ascii is 8 bit,
                    'in regular unicode or utf encoding, the extended ascii character will give something weird. IE: "°C" will give "?C" since the extended ascii character 
                    'is not recognized
                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If encoding Is Nothing Then
                        Dim isoEncoding As Integer = 28591
                        Dim specialEncoding As System.Text.Encoding = _
                            System.Text.Encoding.GetEncoding(isoEncoding)
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, specialEncoding)
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If

                    ''sorting and selection of what we want
                    Dim finalColl As Object = From fin In CType(tableCollection, IEnumerable)
                                                Where fin.ExportToCSV = True
                                                Order By fin.EngineModel, fin.txrx, fin.description, fin.ChannelNumber

                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}", _
                                                              csvDelimited, _
                                                              "EngineModel", "Parameter", "Description", "ArincChannelAB", "Units", "Units", "CardNumber", "CoreNumber", "ChannelNumber", "ArincRxTx", _
                                                              "Label", "sdi", "StatusMatrix", "DefaultValue", "Format", "ScalingFactor", "NbSigBitsOrDigits", "MinInterval", "Maxinterval")
                        writer.WriteLine(strTemp)
                    End If

                    ''Mise en string du datas avec quelque formatage
                    For Each record As ARINC429LabelTemplate In finalColl
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}", _
                                                                                            csvDelimited, _
                                                                                            FormatRecord(record.EngineModel, exportFormatConfig), _
                                                                                            FormatRecord(record.Parameter, exportFormatConfig), _
                                                                                            FormatRecord(record.Description, exportFormatConfig), _
                                                                                            FormatRecord(record.ArincChannelAB, exportFormatConfig), _
                                                                                            FormatRecord(record.Units, exportFormatConfig), _
                                                                                            FormatRecord(record.CardNumber, exportFormatConfig), _
                                                                                            FormatRecord(record.CoreNumber, exportFormatConfig), _
                                                                                            FormatRecord(record.ChannelNumber, exportFormatConfig), _
                                                                                            FormatRecord(record.ArincRxTx, exportFormatConfig), _
                                                                                           """" + FormatRecord(record.Label, exportFormatConfig) + """", _
                                                                                            FormatRecord(record.Sdi, exportFormatConfig), _
                                                                                            FormatRecord(record.StatusMatrix, exportFormatConfig), _
                                                                                           FormatRecord(record.DefaultValue, exportFormatConfig), _
                                                                                            FormatRecord(record.Format, exportFormatConfig), _
                                                                                            FormatRecord(record.ScalingFactor, exportFormatConfig), _
                                                                                            FormatRecord(record.NbSigBitsOrDigits, exportFormatConfig), _
                                                                                            FormatRecord(record.MinInterval, exportFormatConfig), _
                                                                                            FormatRecord(record.MaxInterval, exportFormatConfig))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportArinc429Label : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportArinc429Label : Could not export properly")
                    Return False
                End Try

                Return True
            End Function

            Public Function ExportNidaqBoolean(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportNidaqBoolean
                Dim csvDelimited As String = ","
                Try
                    Dim tableCollection As New CEL.Data.BusinessObjects.DacnTemplateBoolCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)

                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        writer = New StreamWriter((pathName + exportFormatConfig.FileName))
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If

                    'Include column name if requested in the cfg file
                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}", _
                                                              csvDelimited, _
                                                              "RdataID", _
                                                              "Name", _
                                                              "FieldEquipementID", _
                                                              "DeviceID", _
                                                              "Channel", _
                                                              "bit")

                        writer.WriteLine(strTemp)
                    End If

                    Dim finalColl As Object = From fin In CType(tableCollection, IEnumerable)
                                    Where fin.projectid = m_CELProject.Project.Number.ToString And fin.ExportToCSV = True
                                    Order By fin.Name

                    ''put into string so we can put it into the CSV
                    For Each record As DacnTemplateBool In finalColl
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}", _
                                                            csvDelimited, _
                                                            FormatRecord(record.RDataID, exportFormatConfig), _
                                                            FormatRecord(record.Name, exportFormatConfig), _
                                                            FormatRecord(record.FieldEquipmentID, exportFormatConfig), _
                                                            FormatRecord(record.DeviceID, exportFormatConfig), _
                                                            FormatRecord(record.Channel, exportFormatConfig), _
                                                            FormatRecord(record.Bit, exportFormatConfig))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)

                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportNidaqBoolean : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportNidaqBoolean : Could not export properly", ex.Message)
                    Return False
                End Try

                Return True
            End Function

            Public Function ExportNidaqReal(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportNidaqReal
                Dim csvDelimited As String = ","
                Try
                    Dim tableCollection As New CEL.Data.BusinessObjects.DacnTemplateRealCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)

                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        writer = New StreamWriter((pathName + exportFormatConfig.FileName))
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If


                    'Include column name if requested in the cfg file
                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}" + _
                                                              "{19}{0}{20}{0}{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}" + _
                                                              "{36}{0}{37}{0}{38}{0}{39}{0}{40}{0}{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}{51}{0}{52}{0}" + _
                                                              "{53}{0}{54}{0}{55}{0}{56}{0}{57}{0}{58}{0}{59}{0}{60}{0}{61}{0}{62}{0}{63}{0}{64}{0}{65}{0}{66}{0}{67}{0}{68}{0}{69}{0}" + _
                                                              "{70}{0}{71}{0}{72}{0}{73}{0}{74}{0}{75}{0}{76}{0}{77}{0}{78}{0}{79}{0}{80}{0}{81}{0}{82}{0}{83}{0}{84}{0}{85}{0}{86}{0}" + _
                                                              "{87}{0}{88}{0}{89}{0}{90}{0}{91}{0}{92}{0}{93}{0}{94}{0}{95}{0}{96}{0}{97}{0}{98}{0}{99}{0}{100}{0}{101}{0}{102}{0}{103}{0}{104}{0}{105}{0}{106}", _
                                                              csvDelimited, _
                                                              "RdataID", "Name", "FieldEquipmentID", "DeviceID", "Channel", "MinDCValue", "MaxDCValue", "TerminalConfiguration", "AlarmOn", "TextConversionOn", _
                                                              "StatisticsOn", "DataAcquisitionOn", "DataProcessingOn", "PeriodInMsec", "TreatOnOption", "DefaultValue", "UpperLimit", "LowerLimit", _
                                                              "DeltaVPlus", "DeltaVMinus", "RocPlus", "RocMinus", "EventOn", "HighHighLimitOn", "HighLimitOn", "LowLimitOn", "LowLowLimitOn", "HighHighAlarmOn", _
                                                              "HighAlarmOn", "LowAlarmOn", "LowLowAlarmOn", "HighHighLimit", "HighLimit", "LowLimit", "LowLowLimit", "HighHighAlarm", "HighAlarm", "LowAlarm", _
                                                              "LowLowAlarm", "HighHighBand", "HighBand", "LowBand", "LowLowBand", "HighHighAlarmSource", "HighAlarmSource", "LowAlarmSource", "LowLowAlarmSource", _
                                                              "HighHighAlarmName", "HighAlarmName", "LowAlarmName", "LowLowAlarmName", "TextPvFormat", "HMIAliasName", "FILEAliasName", "EngUnit", "MinMaxOn", _
                                                              "DeltaVOn", "SumVOn", "DeltaTOn", "SumTOn", "RocOn", "AvgOn", "AvgTypeID", "AvgPeriod", "AvgNbSamples", "TrigCondID", "TrigLevel", "TrigDeadBand", _
                                                              "TimeOut", "QueueOption", "QueueSize", "PreTrigRecord", "MaxRecord", "ArgSrc", "NumOfArg", "ValidArgOn", "NumOfValidArg", "Coeff1", "Coeff2", _
                                                              "Coeff3", "Coeff4", "Coeff5", "Coeff6", "Arg1", "Arg2", "Arg3", "Arg4", "Arg5", "Arg6", "Arg7", "Arg8", "Arg9", "Arg10", "Arg11", "Arg12", "Arg13", _
                                                              "Arg14", "Arg15", "Arg16", "Arg17", "Arg18", "Arg19", "Arg20", "PolyDegree", "ProcType")
                        writer.WriteLine(strTemp)          ''sorting and selection of what we want            
                    End If

                    ''sorting and selection of what we want
                    Dim finalColl As Object = From fin In CType(tableCollection, IEnumerable)
                                    Where fin.projectid = m_CELProject.Project.Number.ToString And fin.ExportToCSV = True
                                    Order By fin.RdataID

                    ''put into string so we can put it into the CSV
                    For Each record As DacnTemplateReal In finalColl

                        'EngUnit need to be something. In case of nothing or "", the string is set to none to avoid errors during csv write
                        If (record.EngUnit Is Nothing) Or (record.EngUnit = "") Then record.EngUnit = "none"

                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}" + _
                                                              "{19}{0}{20}{0}{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}" + _
                                                              "{36}{0}{37}{0}{38}{0}{39}{0}{40}{0}{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}{51}{0}{52}{0}" + _
                                                              "{53}{0}{54}{0}{55}{0}{56}{0}{57}{0}{58}{0}{59}{0}{60}{0}{61}{0}{62}{0}{63}{0}{64}{0}{65}{0}{66}{0}{67}{0}{68}{0}{69}{0}" + _
                                                              "{70}{0}{71}{0}{72}{0}{73}{0}{74}{0}{75}{0}{76}{0}{77}{0}{78}{0}{79}{0}{80}{0}{81}{0}{82}{0}{83}{0}{84}{0}{85}{0}{86}{0}" + _
                                                              "{87}{0}{88}{0}{89}{0}{90}{0}{91}{0}{92}{0}{93}{0}{94}{0}{95}{0}{96}{0}{97}{0}{98}{0}{99}{0}{100}{0}{101}{0}{102}{0}{103}{0}{104}{0}{105}{0}{106}", _
                                                                                csvDelimited, _
                                                                                FormatRecord(record.RdataID, exportFormatConfig), _
                                                                                FormatRecord(record.Name, exportFormatConfig), _
                                                                                FormatRecord(record.FieldEquipmentID, exportFormatConfig), _
                                                                                FormatRecord(record.DeviceID, exportFormatConfig), _
                                                                                FormatRecord(record.Channel, exportFormatConfig), _
                                                                                FormatRecord(record.MinDCValue, exportFormatConfig, 5), _
                                                                                FormatRecord(record.MaxDCValue, exportFormatConfig, 5), _
                                                                                FormatRecord(record.TerminalConfiguration, exportFormatConfig), _
                                                                                record.AlarmOnInteger, _
                                                                                record.TextConversionOnInteger, _
                                                                                record.statisticsOnInteger, _
                                                                                record.DataAcquisitionOnInteger, _
                                                                                record.DataProcessingOnInteger, _
                                                                                FormatRecord(record.PeriodInMsec, exportFormatConfig), _
                                                                                FormatRecord(record.TreatOnOption, exportFormatConfig), _
                                                                                FormatRecord(record.DefaultValue, exportFormatConfig, 7), _
                                                                                FormatRecord(record.DefaultValue, exportFormatConfig, 7), _
                                                                                FormatRecord(record.UpperLimit, exportFormatConfig, 7), _
                                                                                FormatRecord(record.LowerLimit, exportFormatConfig, 7), _
                                                                                FormatRecord(record.DeltaVPlus, exportFormatConfig, 7), _
                                                                                FormatRecord(record.DeltaVMinus, exportFormatConfig, 7), _
                                                                                FormatRecord(record.RocPlus, exportFormatConfig, 7), _
                                                                                FormatRecord(record.RocMinus, exportFormatConfig, 7), _
                                                                                record.EventOnInteger, _
                                                                                record.HighHighLimitOnInteger, _
                                                                                record.HighLimitOnInteger, _
                                                                                record.LowLimitOnInteger, _
                                                                                record.LowLowLimitOnInteger, _
                                                                                record.HighHighAlarmOnInteger, _
                                                                                record.HighAlarmOnInteger, _
                                                                                record.LowAlarmOnInteger, _
                                                                                record.LowLowAlarmOnInteger, _
                                                                                FormatRecord(record.HighHighLimit, exportFormatConfig, 7), _
                                                                                FormatRecord(record.HighLimit, exportFormatConfig, 7), _
                                                                                FormatRecord(record.LowLimit, exportFormatConfig, 7), _
                                                                                FormatRecord(record.LowLowLimit, exportFormatConfig, 7), _
                                                                                FormatRecord(record.HighHighAlarm, exportFormatConfig, 7), _
                                                                                FormatRecord(record.HighAlarm, exportFormatConfig, 7), _
                                                                                FormatRecord(record.LowAlarm, exportFormatConfig, 7), _
                                                                                FormatRecord(record.LowLowAlarm, exportFormatConfig, 7), _
                                                                                FormatRecord(record.HighHighBand, exportFormatConfig, 7), _
                                                                                FormatRecord(record.HighBand, exportFormatConfig, 7), _
                                                                                FormatRecord(record.LowBand, exportFormatConfig, 7), _
                                                                                FormatRecord(record.LowLowBand, exportFormatConfig, 7), _
                                                                                FormatRecord(record.HighHighAlarmSource, exportFormatConfig), _
                                                                                FormatRecord(record.HighAlarmSource, exportFormatConfig), _
                                                                                FormatRecord(record.LowAlarmSource, exportFormatConfig), _
                                                                                FormatRecord(record.LowLowAlarmSource, exportFormatConfig), _
                                                                                FormatRecord(record.HighHighAlarmName, exportFormatConfig), _
                                                                                FormatRecord(record.HighAlarmName, exportFormatConfig), _
                                                                                FormatRecord(record.LowAlarmName, exportFormatConfig), _
                                                                                FormatRecord(record.LowLowAlarmName, exportFormatConfig), _
                                                                                FormatRecord(record.TextPvFormat, exportFormatConfig), _
                                                                                FormatRecord(record.HMIAliasName, exportFormatConfig), _
                                                                                FormatRecord(record.FILEAliasName, exportFormatConfig), _
                                                                                FormatRecord(record.EngUnit, exportFormatConfig), _
                                                                                record.MinMaxOnInteger, _
                                                                                record.DeltaVOnInteger, _
                                                                                record.SumVOnInteger, _
                                                                                record.DeltaTOnInteger, _
                                                                                record.SumTOnInteger, _
                                                                                record.RocOnInteger, _
                                                                                record.AvgOnInteger, _
                                                                                FormatRecord(record.AvgTypeID, exportFormatConfig), _
                                                                                FormatRecord(record.AvgPeriod, exportFormatConfig), _
                                                                                FormatRecord(record.AvgNbSamples, exportFormatConfig), _
                                                                                FormatRecord(record.TrigCondID, exportFormatConfig), _
                                                                                FormatRecord(record.TrigLevel, exportFormatConfig, 7), _
                                                                                FormatRecord(record.TrigDeadBand, exportFormatConfig, 7), _
                                                                                FormatRecord(record.TimeOut, exportFormatConfig), _
                                                                                FormatRecord(record.QueueOption, exportFormatConfig), _
                                                                                FormatRecord(record.QueueSize, exportFormatConfig), _
                                                                                FormatRecord(record.PreTrigRecord, exportFormatConfig), _
                                                                                FormatRecord(record.MaxRecord, exportFormatConfig), _
                                                                                FormatRecord(record.ArgSrc, exportFormatConfig), _
                                                                                FormatRecord(record.NumOfArg, exportFormatConfig), _
                                                                                record.ValidArgOnInteger, _
                                                                                FormatRecord(record.NumOfValidArg, exportFormatConfig), _
                                                                                FormatRecord(record.Coeff1, exportFormatConfig, 7), _
                                                                                FormatRecord(record.Coeff2, exportFormatConfig, 6), _
                                                                                FormatRecord(record.Coeff3, exportFormatConfig, 7), _
                                                                                FormatRecord(record.Coeff4, exportFormatConfig, 7), _
                                                                                FormatRecord(record.Coeff5, exportFormatConfig, 7), _
                                                                                FormatRecord(record.Coeff6, exportFormatConfig, 7), _
                                                                                FormatRecord(record.Arg1, exportFormatConfig), _
                                                                                FormatRecord(record.Arg2, exportFormatConfig), _
                                                                                FormatRecord(record.Arg3, exportFormatConfig), _
                                                                                FormatRecord(record.Arg4, exportFormatConfig), _
                                                                                FormatRecord(record.Arg5, exportFormatConfig), _
                                                                                FormatRecord(record.Arg6, exportFormatConfig), _
                                                                                FormatRecord(record.Arg7, exportFormatConfig), _
                                                                                FormatRecord(record.Arg8, exportFormatConfig), _
                                                                                FormatRecord(record.Arg9, exportFormatConfig), _
                                                                                FormatRecord(record.Arg10, exportFormatConfig), _
                                                                                FormatRecord(record.Arg11, exportFormatConfig), _
                                                                                FormatRecord(record.Arg12, exportFormatConfig), _
                                                                                FormatRecord(record.Arg13, exportFormatConfig), _
                                                                                FormatRecord(record.Arg14, exportFormatConfig), _
                                                                                FormatRecord(record.Arg15, exportFormatConfig), _
                                                                                FormatRecord(record.Arg16, exportFormatConfig), _
                                                                                FormatRecord(record.Arg17, exportFormatConfig), _
                                                                                FormatRecord(record.Arg18, exportFormatConfig), _
                                                                                FormatRecord(record.Arg19, exportFormatConfig), _
                                                                                FormatRecord(record.Arg20, exportFormatConfig), _
                                                                                FormatRecord(record.PolyDegree, exportFormatConfig), _
                                                                                FormatRecord(record.ProcType, exportFormatConfig))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportNidaqReal : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportNidaqReal : Could not export properly")
                    Return False
                End Try

                Return True
            End Function

            Public Function ExportOmniBusBox(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportOmniBusBox
                Dim csvDelimited As String = ","
                Try
                    Dim tableCollection As New CEL.Data.BusinessObjects.OmniBusBoxUartTemplateCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.Query.Where(tableCollection.Query.EngineModel = m_CELProject.EngineModel)
                    tableCollection.Query.Where(tableCollection.Query.PwcPn = m_CELProject.PWCEECPartNumber)
                    tableCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)

                    'Way to force extended ascii encoding within the streamwriter this is necessary for ° and other character. Since ascii is 7bit and extended ascii is 8 bit,
                    'in regular unicode or utf encoding, the extended ascii character will give something weird. IE: "°C" will give "?C" since the extended ascii character 
                    'is not recognized
                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        Dim isoEncoding As Integer = 28591
                        Dim specialEncoding As System.Text.Encoding = _
                            System.Text.Encoding.GetEncoding(isoEncoding)
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, specialEncoding)
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If

                    ''sorting and selection of what we want
                    Dim finalColl As Object = From fin In CType(tableCollection, IEnumerable)
                                              Where fin.ExportToCSV = True
                                              Order By fin.EngineModel, fin.ArincChannel, fin.ParameterNo

                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}", _
                                          csvDelimited, _
                                          "EngineModel", "ArincChannel", "Parameter", "Address", "DataType", "Units", "CardNumber", "CoreNumber", "ChannelNumber", "Slope", "Offset")
                        writer.WriteLine(strTemp)
                    End If

                    ''mise en string des datas
                    For Each record As OmniBusBoxUartTemplate In finalColl
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}", _
                                                                    csvDelimited, _
                                                                    FormatRecord(record.EngineModel, exportFormatConfig), _
                                                                    FormatRecord(record.ArincChannel, exportFormatConfig), _
                                                                    FormatRecord(record.Parameter, exportFormatConfig), _
                                                                    FormatRecord(record.Address, exportFormatConfig), _
                                                                    FormatRecord(record.DataType, exportFormatConfig), _
                                                                    FormatRecord(record.Units, exportFormatConfig), _
                                                                    FormatRecord(record.CardNumber, exportFormatConfig), _
                                                                    FormatRecord(record.CoreNumber, exportFormatConfig), _
                                                                    FormatRecord(record.ChannelNumber, exportFormatConfig), _
                                                                    FormatRecord(record.Slope, exportFormatConfig, 9), _
                                                                    FormatRecord(record.Offset, exportFormatConfig, 7))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportOmniBusBox : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportOmniBusBox : Could not export properly")
                    Return False
                End Try

                Return True
            End Function

            Public Function ExportPowerDNA1(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportPowerDNA1
                Dim csvDelimited As String = ","
                Try
                    Dim tableCollection As New CEL.Data.BusinessObjects.DacnTemplateRealCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)

                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        writer = New StreamWriter((pathName + exportFormatConfig.FileName))
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If

                    ''sorting and selection of what we want
                    Dim finalColl As Object = From fin In CType(tableCollection, IEnumerable)
                                                Where fin.projectid = m_CELProject.Project.Number.ToString And fin.ExportToCSV = True
                                                Order By fin.FieldEquipmentID, fin.BaseAddress, fin.PhysicalChannel

                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}" + _
                                                              "{20}{0}{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}{36}{0}" + _
                                                              "{37}{0}{38}{0}{39}{0}{40}{0}{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}{51}{0}{52}{0}{53}{0}" + _
                                                              "{54}{0}{55}{0}{56}{0}{57}{0}{58}{0}{59}{0}{60}{0}{61}{0}{62}{0}{63}{0}{64}{0}{65}{0}{66}{0}{67}{0}{68}{0}{69}{0}{70}{0}" + _
                                                              "{71}{0}{72}{0}{73}{0}{74}{0}{75}{0}{76}{0}{77}{0}{78}{0}{79}{0}{80}{0}{81}{0}{82}{0}{83}{0}{84}{0}{85}{0}{86}{0}{87}{0}" + _
                                                              "{88}{0}{89}{0}{90}{0}{91}{0}{92}{0}{93}{0}{94}{0}{95}{0}{96}{0}{97}{0}{98}{0}{99}{0}{100}{0}{101}{0}{102}{0}{103}{0}{104}" + _
                                                              "{0}{105}{0}{106}{0}{107}{0}{108}", _
                                                                csvDelimited, "RdataID", "Name", "FieldEquipmentID", "DeviceID", "InChannelType", "BaseAddress", "PhysicalChannel", "MinDCValue", _
                                                                "MaxDCValue", "TerminalConfiguration", "AlarmOn", "TextConversionOn", "StatisticsOn", "DataAcquisitionOn", "DataProcessingOn", _
                                                                "PeriodInMsec", "TreatOnOption", "DefaultValue", "DefaultValue", "UpperLimit", "LowerLimit", "DeltaVPlus", "DeltaVMinus", "RocPlus", _
                                                                "RocMinus", "EventOn", "HighHighLimitOn", "HighLimitOn", "LowLimitOn", "LowLowLimitOn", "HighHighAlarmOn", "HighAlarmOn", "LowAlarmOn", _
                                                                "LowLowAlarmOn", "HighHighLimit", "HighLimit", "LowLimit", "LowLowLimit", "HighHighAlarm", "HighAlarm", "LowAlarm", "LowLowAlarm",
                                                                "HighHighBand", "HighBand", "LowBand", "LowLowBand", "HighHighAlarmSource", "HighAlarmSource", "LowLowAlarmName", "TextPvFormat", _
                                                                "HMIAliasName", "FILEAliasName", "EngUnit", "MinMaxOn", "DeltaVOn", "SumVOn", "DeltaTOn", "SumTOn", "RocOn", "AvgOn", "AvgTypeID", _
                                                                "AvgPeriod", "AvgNbSamples", "TrigCondID", "TrigLevel", "TrigDeadBand", "TimeOut", "QueueOption", "QueueSize", "PreTrigRecord", _
                                                                "MaxRecord", "ArgSrc", "NumOfArg", "ValidArgOn", "NumOfValidArg", "Coeff1", "Coeff2", "Coeff3", "Coeff4", "Coeff5", "Coeff6", _
                                                                "Arg1", "Arg2", "Arg3", "Arg4", "Arg5", "Arg6", "Arg7", "Arg8", "Arg9", "Arg10", "Arg11", "Arg12", "Arg13", "Arg14", "Arg15", _
                                                                "Arg16", "Arg17", "Arg18", "Arg19", "Arg20", "PolyDegree", "ProcType")
                        writer.WriteLine(strTemp)
                    End If

                    ''put into string so we can put it into the CSV
                    For Each record As DacnTemplateReal In finalColl

                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}" + _
                                                              "{20}{0}{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}{36}{0}" + _
                                                              "{37}{0}{38}{0}{39}{0}{40}{0}{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}{51}{0}{52}{0}{53}{0}" + _
                                                              "{54}{0}{55}{0}{56}{0}{57}{0}{58}{0}{59}{0}{60}{0}{61}{0}{62}{0}{63}{0}{64}{0}{65}{0}{66}{0}{67}{0}{68}{0}{69}{0}{70}{0}" + _
                                                              "{71}{0}{72}{0}{73}{0}{74}{0}{75}{0}{76}{0}{77}{0}{78}{0}{79}{0}{80}{0}{81}{0}{82}{0}{83}{0}{84}{0}{85}{0}{86}{0}{87}{0}" + _
                                                              "{88}{0}{89}{0}{90}{0}{91}{0}{92}{0}{93}{0}{94}{0}{95}{0}{96}{0}{97}{0}{98}{0}{99}{0}{100}{0}{101}{0}{102}{0}{103}{0}{104}" + _
                                                              "{0}{105}{0}{106}{0}{107}{0}{108}", _
                                                                                            csvDelimited, _
                                                                                            FormatRecord(record.RdataID, exportFormatConfig), _
                                                                                            FormatRecord(record.Name, exportFormatConfig), _
                                                                                            FormatRecord(record.FieldEquipmentID, exportFormatConfig), _
                                                                                            FormatRecord(record.DeviceID, exportFormatConfig), _
                                                                                            FormatRecord(record.InChannelType, exportFormatConfig), _
                                                                                            FormatRecord(record.BaseAddress, exportFormatConfig), _
                                                                                            FormatRecord(record.PhysicalChannel, exportFormatConfig), _
                                                                                            FormatRecord(record.MinDCValue, exportFormatConfig, 5), _
                                                                                            FormatRecord(record.MaxDCValue, exportFormatConfig, 5), _
                                                                                            FormatRecord(record.TerminalConfiguration, exportFormatConfig), _
                                                                                            record.AlarmOnInteger, _
                                                                                            record.TextConversionOnInteger, _
                                                                                            record.statisticsOnInteger, _
                                                                                            record.DataAcquisitionOnInteger, _
                                                                                            record.DataProcessingOnInteger, _
                                                                                            FormatRecord(record.PeriodInMsec, exportFormatConfig), _
                                                                                            FormatRecord(record.TreatOnOption, exportFormatConfig), _
                                                                                            FormatRecord(record.DefaultValue, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.DefaultValue, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.UpperLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowerLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.DeltaVPlus, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.DeltaVMinus, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.RocPlus, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.RocMinus, exportFormatConfig, 7), _
                                                                                            record.EventOnInteger, _
                                                                                            record.HighHighLimitOnInteger, _
                                                                                            record.HighLimitOnInteger, _
                                                                                            record.LowLimitOnInteger, _
                                                                                            record.LowLowLimitOnInteger, _
                                                                                            record.HighHighAlarmOnInteger, _
                                                                                            record.HighAlarmOnInteger, _
                                                                                            record.LowAlarmOnInteger, _
                                                                                            record.LowLowAlarmOnInteger, _
                                                                                            FormatRecord(record.HighHighLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowLowLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighHighAlarm, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighAlarm, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowAlarm, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowLowAlarm, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighHighBand, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighBand, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowBand, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowLowBand, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighHighAlarmSource, exportFormatConfig), _
                                                                                            FormatRecord(record.HighAlarmSource, exportFormatConfig), _
                                                                                            FormatRecord(record.LowAlarmSource, exportFormatConfig), _
                                                                                            FormatRecord(record.LowLowAlarmSource, exportFormatConfig), _
                                                                                            FormatRecord(record.HighHighAlarmName, exportFormatConfig), _
                                                                                            FormatRecord(record.HighAlarmName, exportFormatConfig), _
                                                                                            FormatRecord(record.LowAlarmName, exportFormatConfig), _
                                                                                            FormatRecord(record.LowLowAlarmName, exportFormatConfig), _
                                                                                            FormatRecord(record.TextPvFormat, exportFormatConfig), _
                                                                                            FormatRecord(record.HMIAliasName, exportFormatConfig), _
                                                                                            FormatRecord(record.FILEAliasName, exportFormatConfig), _
                                                                                            FormatRecord(record.EngUnit, exportFormatConfig), _
                                                                                            record.MinMaxOnInteger, _
                                                                                            record.DeltaVOnInteger, _
                                                                                            record.SumVOnInteger, _
                                                                                            record.DeltaTOnInteger, _
                                                                                            record.SumTOnInteger, _
                                                                                            record.RocOnInteger, _
                                                                                            record.AvgOnInteger, _
                                                                                            FormatRecord(record.AvgTypeID, exportFormatConfig), _
                                                                                            FormatRecord(record.AvgPeriod, exportFormatConfig), _
                                                                                            FormatRecord(record.AvgNbSamples, exportFormatConfig), _
                                                                                            FormatRecord(record.TrigCondID, exportFormatConfig), _
                                                                                            FormatRecord(record.TrigLevel, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.TrigDeadBand, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.TimeOut, exportFormatConfig), _
                                                                                            FormatRecord(record.QueueOption, exportFormatConfig), _
                                                                                            FormatRecord(record.QueueSize, exportFormatConfig), _
                                                                                            FormatRecord(record.PreTrigRecord, exportFormatConfig), _
                                                                                            FormatRecord(record.MaxRecord, exportFormatConfig), _
                                                                                            FormatRecord(record.ArgSrc, exportFormatConfig), _
                                                                                            FormatRecord(record.NumOfArg, exportFormatConfig), _
                                                                                            record.ValidArgOnInteger, _
                                                                                            FormatRecord(record.NumOfValidArg, exportFormatConfig), _
                                                                                            FormatRecord(record.Coeff1, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.Coeff2, exportFormatConfig, 6), _
                                                                                            FormatRecord(record.Coeff3, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.Coeff4, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.Coeff5, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.Coeff6, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.Arg1, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg2, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg3, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg4, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg5, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg6, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg7, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg8, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg9, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg10, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg11, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg12, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg13, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg14, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg15, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg16, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg17, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg18, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg19, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg20, exportFormatConfig), _
                                                                                            FormatRecord(record.PolyDegree, exportFormatConfig), _
                                                                                            FormatRecord(record.ProcType, exportFormatConfig))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportPowerDna1 : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportPowerDna1 : Could not export properly")
                    Return False
                End Try

                Return True
            End Function

            Public Function ExportPowerDNA2(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportPowerDNA2
                Dim csvDelimited As String = ","
                Try
                    Dim tableCollection As New CEL.Data.BusinessObjects.DacnTemplateRealCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)
                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        writer = New StreamWriter((pathName + exportFormatConfig.FileName))
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If

                    ''sorting and selection of what we want
                    Dim finalColl As Object = From fin In CType(tableCollection, IEnumerable)
                                                Where fin.projectid = m_CELProject.Project.Number.ToString And fin.ExportToCSV = True
                                                Order By fin.FieldEquipmentID, fin.BaseAddress, fin.PhysicalChannel

                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}" + _
                                                              "{20}{0}{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}{36}{0}" + _
                                                              "{37}{0}{38}{0}{39}{0}{40}{0}{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}{51}{0}{52}{0}{53}{0}" + _
                                                              "{54}{0}{55}{0}{56}{0}{57}{0}{58}{0}{59}{0}{60}{0}{61}{0}{62}{0}{63}{0}{64}{0}{65}{0}{66}{0}{67}{0}{68}{0}{69}{0}{70}{0}" + _
                                                              "{71}{0}{72}{0}{73}{0}{74}{0}{75}{0}{76}{0}{77}{0}{78}{0}{79}{0}{80}{0}{81}{0}{82}{0}{83}{0}{84}{0}{85}{0}{86}{0}{87}{0}" + _
                                                              "{88}{0}{89}{0}{90}{0}{91}{0}{92}{0}{93}{0}{94}{0}{95}{0}{96}{0}{97}{0}{98}{0}{99}{0}{100}{0}{101}{0}{102}{0}{103}{0}{104}" + _
                                                              "{0}{105}{0}{106}{0}{107}{0}{108}", _
                                                                csvDelimited, "RdataID", "Name", "FieldEquipmentID", "DeviceID", "InChannelType", "BaseAddress", "PhysicalChannel", "MinDCValue", _
                                                                "MaxDCValue", "TerminalConfiguration", "AlarmOn", "TextConversionOn", "StatisticsOn", "DataAcquisitionOn", "DataProcessingOn", _
                                                                "PeriodInMsec", "TreatOnOption", "DefaultValue", "DefaultValue", "UpperLimit", "LowerLimit", "DeltaVPlus", "DeltaVMinus", "RocPlus", _
                                                                "RocMinus", "EventOn", "HighHighLimitOn", "HighLimitOn", "LowLimitOn", "LowLowLimitOn", "HighHighAlarmOn", "HighAlarmOn", "LowAlarmOn", _
                                                                "LowLowAlarmOn", "HighHighLimit", "HighLimit", "LowLimit", "LowLowLimit", "HighHighAlarm", "HighAlarm", "LowAlarm", "LowLowAlarm",
                                                                "HighHighBand", "HighBand", "LowBand", "LowLowBand", "HighHighAlarmSource", "HighAlarmSource", "LowLowAlarmName", "TextPvFormat", _
                                                                "HMIAliasName", "FILEAliasName", "EngUnit", "MinMaxOn", "DeltaVOn", "SumVOn", "DeltaTOn", "SumTOn", "RocOn", "AvgOn", "AvgTypeID", _
                                                                "AvgPeriod", "AvgNbSamples", "TrigCondID", "TrigLevel", "TrigDeadBand", "TimeOut", "QueueOption", "QueueSize", "PreTrigRecord", _
                                                                "MaxRecord", "ArgSrc", "NumOfArg", "ValidArgOn", "NumOfValidArg", "Coeff1", "Coeff2", "Coeff3", "Coeff4", "Coeff5", "Coeff6", _
                                                                "Arg1", "Arg2", "Arg3", "Arg4", "Arg5", "Arg6", "Arg7", "Arg8", "Arg9", "Arg10", "Arg11", "Arg12", "Arg13", "Arg14", "Arg15", _
                                                                "Arg16", "Arg17", "Arg18", "Arg19", "Arg20", "PolyDegree", "ProcType")
                        writer.WriteLine(strTemp)
                    End If

                    ''put into string so we can put it into the CSV
                    For Each record As DacnTemplateReal In finalColl
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}" + _
                                                              "{20}{0}{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}{36}{0}" + _
                                                              "{37}{0}{38}{0}{39}{0}{40}{0}{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}{51}{0}{52}{0}{53}{0}" + _
                                                              "{54}{0}{55}{0}{56}{0}{57}{0}{58}{0}{59}{0}{60}{0}{61}{0}{62}{0}{63}{0}{64}{0}{65}{0}{66}{0}{67}{0}{68}{0}{69}{0}{70}{0}" + _
                                                              "{71}{0}{72}{0}{73}{0}{74}{0}{75}{0}{76}{0}{77}{0}{78}{0}{79}{0}{80}{0}{81}{0}{82}{0}{83}{0}{84}{0}{85}{0}{86}{0}{87}{0}" + _
                                                              "{88}{0}{89}{0}{90}{0}{91}{0}{92}{0}{93}{0}{94}{0}{95}{0}{96}{0}{97}{0}{98}{0}{99}{0}{100}{0}{101}{0}{102}{0}{103}{0}{104}" + _
                                                              "{0}{105}{0}{106}{0}{107}{0}{108}", _
                                                                                          csvDelimited, _
                                                                                            FormatRecord(record.RdataID, exportFormatConfig), _
                                                                                            FormatRecord(record.Name, exportFormatConfig), _
                                                                                            FormatRecord(record.FieldEquipmentID, exportFormatConfig), _
                                                                                            FormatRecord(record.DeviceID, exportFormatConfig), _
                                                                                            FormatRecord(record.InChannelType, exportFormatConfig), _
                                                                                            FormatRecord(record.BaseAddress, exportFormatConfig), _
                                                                                            FormatRecord(record.PhysicalChannel, exportFormatConfig), _
                                                                                            FormatRecord(record.MinDCValue, exportFormatConfig, 5), _
                                                                                            FormatRecord(record.MaxDCValue, exportFormatConfig, 5), _
                                                                                            FormatRecord(record.TerminalConfiguration, exportFormatConfig), _
                                                                                            record.AlarmOnInteger, _
                                                                                            record.TextConversionOnInteger, _
                                                                                            record.statisticsOnInteger, _
                                                                                            record.DataAcquisitionOnInteger, _
                                                                                            record.DataProcessingOnInteger, _
                                                                                            FormatRecord(record.PeriodInMsec, exportFormatConfig), _
                                                                                            FormatRecord(record.TreatOnOption, exportFormatConfig), _
                                                                                            FormatRecord(record.DefaultValue, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.DefaultValue, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.UpperLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowerLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.DeltaVPlus, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.DeltaVMinus, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.RocPlus, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.RocMinus, exportFormatConfig, 7), _
                                                                                            record.EventOnInteger, _
                                                                                            record.HighHighLimitOnInteger, _
                                                                                            record.HighLimitOnInteger, _
                                                                                            record.LowLimitOnInteger, _
                                                                                            record.LowLowLimitOnInteger, _
                                                                                            record.HighHighAlarmOnInteger, _
                                                                                            record.HighAlarmOnInteger, _
                                                                                            record.LowAlarmOnInteger, _
                                                                                            record.LowLowAlarmOnInteger, _
                                                                                            FormatRecord(record.HighHighLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowLowLimit, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighHighAlarm, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighAlarm, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowAlarm, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowLowAlarm, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighHighBand, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighBand, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowBand, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.LowLowBand, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.HighHighAlarmSource, exportFormatConfig), _
                                                                                            FormatRecord(record.HighAlarmSource, exportFormatConfig), _
                                                                                            FormatRecord(record.LowAlarmSource, exportFormatConfig), _
                                                                                            FormatRecord(record.LowLowAlarmSource, exportFormatConfig), _
                                                                                            FormatRecord(record.HighHighAlarmName, exportFormatConfig), _
                                                                                            FormatRecord(record.HighAlarmName, exportFormatConfig), _
                                                                                            FormatRecord(record.LowAlarmName, exportFormatConfig), _
                                                                                            FormatRecord(record.LowLowAlarmName, exportFormatConfig), _
                                                                                            FormatRecord(record.TextPvFormat, exportFormatConfig), _
                                                                                            FormatRecord(record.HMIAliasName, exportFormatConfig), _
                                                                                            FormatRecord(record.FILEAliasName, exportFormatConfig), _
                                                                                            FormatRecord(record.EngUnit, exportFormatConfig), _
                                                                                            record.MinMaxOnInteger, _
                                                                                            record.DeltaVOnInteger, _
                                                                                            record.SumVOnInteger, _
                                                                                            record.DeltaTOnInteger, _
                                                                                            record.SumTOnInteger, _
                                                                                            record.RocOnInteger, _
                                                                                            record.AvgOnInteger, _
                                                                                            FormatRecord(record.AvgTypeID, exportFormatConfig), _
                                                                                            FormatRecord(record.AvgPeriod, exportFormatConfig), _
                                                                                            FormatRecord(record.AvgNbSamples, exportFormatConfig), _
                                                                                            FormatRecord(record.TrigCondID, exportFormatConfig), _
                                                                                            FormatRecord(record.TrigLevel, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.TrigDeadBand, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.TimeOut, exportFormatConfig), _
                                                                                            FormatRecord(record.QueueOption, exportFormatConfig), _
                                                                                            FormatRecord(record.QueueSize, exportFormatConfig), _
                                                                                            FormatRecord(record.PreTrigRecord, exportFormatConfig), _
                                                                                            FormatRecord(record.MaxRecord, exportFormatConfig), _
                                                                                            FormatRecord(record.ArgSrc, exportFormatConfig), _
                                                                                            FormatRecord(record.NumOfArg, exportFormatConfig), _
                                                                                            record.ValidArgOnInteger, _
                                                                                            FormatRecord(record.NumOfValidArg, exportFormatConfig), _
                                                                                            FormatRecord(record.Coeff1, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.Coeff2, exportFormatConfig, 6), _
                                                                                            FormatRecord(record.Coeff3, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.Coeff4, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.Coeff5, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.Coeff6, exportFormatConfig, 7), _
                                                                                            FormatRecord(record.Arg1, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg2, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg3, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg4, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg5, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg6, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg7, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg8, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg9, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg10, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg11, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg12, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg13, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg14, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg15, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg16, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg17, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg18, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg19, exportFormatConfig), _
                                                                                            FormatRecord(record.Arg20, exportFormatConfig), _
                                                                                            FormatRecord(record.PolyDegree, exportFormatConfig), _
                                                                                            FormatRecord(record.ProcType, exportFormatConfig))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportPowerDNA2 : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportPowerDNA2 : Could not export properly")
                    Return False
                End Try

                Return True
            End Function

            Public Function ExportScaniValveReal(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportScaniValveReal
                Dim csvDelimited As String = ","
                Try
                    Dim tableCollection As New CEL.Data.BusinessObjects.DacnTemplateRealCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)
                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        writer = New StreamWriter((pathName + exportFormatConfig.FileName))
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If

                    ''sorting and selection of what we want
                    Dim finalColl As Object = From fin In CType(tableCollection, IEnumerable)
                                                Where fin.projectid = m_CELProject.Project.Number.ToString And fin.ExportToCSV = True
                                                Order By fin.RdataID

                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}" + _
                                                              "{20}{0}{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}{36}{0}{37}{0}" + _
                                                              "{38}{0}{39}{0}{40}{0}{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}{51}{0}{52}{0}{53}{0}{54}{0}{55}{0}" + _
                                                              "{56}{0}{57}{0}{58}{0}{59}{0}{60}{0}{61}{0}{62}{0}{63}{0}{64}{0}{65}{0}{66}{0}{67}{0}{68}{0}{69}{0}{70}{0}{71}{0}{72}{0}{73}{0}" + _
                                                              "{74}{0}{75}{0}{76}{0}{77}{0}{78}{0}{79}{0}{80}{0}{81}{0}{82}{0}{83}{0}{84}{0}{85}{0}{86}{0}{87}{0}{88}{0}{89}{0}{90}{0}{91}{0}" + _
                                                              "{92}{0}{93}{0}{94}{0}{95}{0}{96}{0}{97}{0}{98}{0}{99}{0}{100}{0}{101}{0}{102}{0}{103}", _
                                                                            csvDelimited, "RdataID", "Name", "FieldEquipmentID", "DeviceID", "Channel", "AlarmOn", "TextConversionOn", "StatisticsOn", _
                                                                            "DataAcquisitionOn", "DataProcessingOn", "PeriodInMsec", "TreatOnOption", "DefaultValue", "DefaultValue", "UpperLimit", _
                                                                            "LowerLimit", "DeltaVPlus", "DeltaVMinus", "RocPlus", "RocMinus", "EventOn", "HighHighLimitOn", "HighLimitOn", _
                                                                            "LowLimitOn", "LowLowLimitOn", "HighHighAlarmOn", "HighAlarmOn", "LowAlarmOn", "LowLowAlarmOn", "HighHighLimit", "HighLimit", _
                                                                            "LowLimit", "LowLowLimit", "HighHighAlarm", "HighAlarm", "LowAlarm", "LowLowAlarm", "HighHighBand", "HighBand", _
                                                                            "LowBand", "LowLowBand", "HighHighAlarmSource", "HighAlarmSource", "LowAlarmSource", "LowLowAlarmSource", "HighHighAlarmName", _
                                                                            "HighAlarmName", "LowAlarmName", "LowLowAlarmName", "TextPvFormat", "HMIAliasName", "FILEAliasName", "EngUnit", "MinMaxOn", _
                                                                            "DeltaVOn", "SumVOn", "DeltaTOn", "SumTOn", "RocOn", "AvgOn", "AvgTypeID", "AvgPeriod", "AvgNbSamples", "TrigCondID", _
                                                                            "TrigLevel", "TrigDeadBand", "TimeOut", "QueueOption", "QueueSize", "PreTrigRecord", "MaxRecord", "ArgSrc", "NumOfArg", _
                                                                            "ValidArgOn", "NumOfValidArg", "Coeff1", "Coeff2", "Coeff3", "Coeff4", "Coeff5", "Coeff6", "Arg1", "Arg2", _
                                                                            "Arg3", "Arg4", "Arg5", "Arg6", "Arg7", "Arg8", "Arg9", "Arg10", "Arg11", "Arg12", "Arg13", "Arg14", _
                                                                            "Arg15", "Arg15", "Arg16", "Arg17", "Arg18", "Arg19", "Arg20", "PolyDegree", "ProcType")
                        writer.WriteLine(strTemp)
                    End If

                    ''put into string so we can put it into the CSV
                    For Each record As DacnTemplateReal In finalColl

                        'EngUnit need to be something. In case of nothing or "", the string is set to none to avoid errors during csv write
                        If (record.EngUnit Is Nothing) Or (record.EngUnit = "") Then record.EngUnit = "none"

                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}" + _
                                                              "{20}{0}{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}{36}{0}{37}{0}" + _
                                                              "{38}{0}{39}{0}{40}{0}{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}{51}{0}{52}{0}{53}{0}{54}{0}{55}{0}" + _
                                                              "{56}{0}{57}{0}{58}{0}{59}{0}{60}{0}{61}{0}{62}{0}{63}{0}{64}{0}{65}{0}{66}{0}{67}{0}{68}{0}{69}{0}{70}{0}{71}{0}{72}{0}{73}{0}" + _
                                                              "{74}{0}{75}{0}{76}{0}{77}{0}{78}{0}{79}{0}{80}{0}{81}{0}{82}{0}{83}{0}{84}{0}{85}{0}{86}{0}{87}{0}{88}{0}{89}{0}{90}{0}{91}{0}" + _
                                                              "{92}{0}{93}{0}{94}{0}{95}{0}{96}{0}{97}{0}{98}{0}{99}{0}{100}{0}{101}{0}{102}{0}{103}", _
                                                                              csvDelimited, _
                                                                              FormatRecord(record.RdataID, exportFormatConfig), _
                                                                              FormatRecord(record.Name, exportFormatConfig), _
                                                                              FormatRecord(record.FieldEquipmentID, exportFormatConfig), _
                                                                              FormatRecord(record.DeviceID, exportFormatConfig), _
                                                                              FormatRecord(record.Channel, exportFormatConfig), _
                                                                              record.AlarmOnInteger, _
                                                                              record.TextConversionOnInteger, _
                                                                              record.statisticsOnInteger, _
                                                                              record.DataAcquisitionOnInteger, _
                                                                              record.DataProcessingOnInteger, _
                                                                              FormatRecord(record.PeriodInMsec, exportFormatConfig), _
                                                                              FormatRecord(record.TreatOnOption, exportFormatConfig), _
                                                                              FormatRecord(record.DefaultValue, exportFormatConfig, 7), _
                                                                              FormatRecord(record.DefaultValue, exportFormatConfig, 7), _
                                                                              FormatRecord(record.UpperLimit, exportFormatConfig, 7), _
                                                                              FormatRecord(record.LowerLimit, exportFormatConfig, 7), _
                                                                              FormatRecord(record.DeltaVPlus, exportFormatConfig, 7), _
                                                                              FormatRecord(record.DeltaVMinus, exportFormatConfig, 7), _
                                                                              FormatRecord(record.RocPlus, exportFormatConfig, 7), _
                                                                              FormatRecord(record.RocMinus, exportFormatConfig, 7), _
                                                                              record.EventOnInteger, _
                                                                              record.HighHighLimitOnInteger, _
                                                                              record.HighLimitOnInteger, _
                                                                              record.LowLimitOnInteger, _
                                                                              record.LowLowLimitOnInteger, _
                                                                              record.HighHighAlarmOnInteger, _
                                                                              record.HighAlarmOnInteger, _
                                                                              record.LowAlarmOnInteger, _
                                                                              record.LowLowAlarmOnInteger, _
                                                                              FormatRecord(record.HighHighLimit, exportFormatConfig), _
                                                                              FormatRecord(record.HighLimit, exportFormatConfig, 7), _
                                                                              FormatRecord(record.LowLimit, exportFormatConfig, 7), _
                                                                              FormatRecord(record.LowLowLimit, exportFormatConfig, 7), _
                                                                              FormatRecord(record.HighHighAlarm, exportFormatConfig, 7), _
                                                                              FormatRecord(record.HighAlarm, exportFormatConfig, 7), _
                                                                              FormatRecord(record.LowAlarm, exportFormatConfig, 7), _
                                                                              FormatRecord(record.LowLowAlarm, exportFormatConfig, 7), _
                                                                              FormatRecord(record.HighHighBand, exportFormatConfig, 7), _
                                                                              FormatRecord(record.HighBand, exportFormatConfig, 7), _
                                                                              FormatRecord(record.LowBand, exportFormatConfig, 7), _
                                                                              FormatRecord(record.LowLowBand, exportFormatConfig, 7), _
                                                                              FormatRecord(record.HighHighAlarmSource, exportFormatConfig), _
                                                                              FormatRecord(record.HighAlarmSource, exportFormatConfig), _
                                                                              FormatRecord(record.LowAlarmSource, exportFormatConfig), _
                                                                              FormatRecord(record.LowLowAlarmSource, exportFormatConfig), _
                                                                              FormatRecord(record.HighHighAlarmName, exportFormatConfig), _
                                                                              FormatRecord(record.HighAlarmName, exportFormatConfig), _
                                                                              FormatRecord(record.LowAlarmName, exportFormatConfig), _
                                                                              FormatRecord(record.LowLowAlarmName, exportFormatConfig), _
                                                                              FormatRecord(record.TextPvFormat, exportFormatConfig), _
                                                                              FormatRecord(record.HMIAliasName, exportFormatConfig), _
                                                                              FormatRecord(record.FILEAliasName, exportFormatConfig), _
                                                                              FormatRecord(record.EngUnit, exportFormatConfig), _
                                                                              record.MinMaxOnInteger, _
                                                                              record.DeltaVOnInteger, _
                                                                              record.SumVOnInteger, _
                                                                              record.DeltaTOnInteger, _
                                                                              record.SumTOnInteger, _
                                                                              record.RocOnInteger, _
                                                                              record.AvgOnInteger, _
                                                                              FormatRecord(record.AvgTypeID, exportFormatConfig), _
                                                                              FormatRecord(record.AvgPeriod, exportFormatConfig), _
                                                                              FormatRecord(record.AvgNbSamples, exportFormatConfig), _
                                                                              FormatRecord(record.TrigCondID, exportFormatConfig), _
                                                                              FormatRecord(record.TrigLevel, exportFormatConfig, 7), _
                                                                              FormatRecord(record.TrigDeadBand, exportFormatConfig, 7), _
                                                                              FormatRecord(record.TimeOut, exportFormatConfig), _
                                                                              FormatRecord(record.QueueOption, exportFormatConfig), _
                                                                              FormatRecord(record.QueueSize, exportFormatConfig), _
                                                                              FormatRecord(record.PreTrigRecord, exportFormatConfig), _
                                                                              FormatRecord(record.MaxRecord, exportFormatConfig), _
                                                                              FormatRecord(record.ArgSrc, exportFormatConfig), _
                                                                              FormatRecord(record.NumOfArg, exportFormatConfig), _
                                                                              record.ValidArgOnInteger, _
                                                                              FormatRecord(record.NumOfValidArg, exportFormatConfig), _
                                                                              FormatRecord(record.Coeff1, exportFormatConfig, 7), _
                                                                              FormatRecord(record.Coeff2, exportFormatConfig, 6), _
                                                                              FormatRecord(record.Coeff3, exportFormatConfig, 7), _
                                                                              FormatRecord(record.Coeff4, exportFormatConfig, 7), _
                                                                              FormatRecord(record.Coeff5, exportFormatConfig, 7), _
                                                                              FormatRecord(record.Coeff6, exportFormatConfig, 7), _
                                                                              FormatRecord(record.Arg1, exportFormatConfig), _
                                                                              FormatRecord(record.Arg2, exportFormatConfig), _
                                                                              FormatRecord(record.Arg3, exportFormatConfig), _
                                                                              FormatRecord(record.Arg4, exportFormatConfig), _
                                                                              FormatRecord(record.Arg5, exportFormatConfig), _
                                                                              FormatRecord(record.Arg6, exportFormatConfig), _
                                                                              FormatRecord(record.Arg7, exportFormatConfig), _
                                                                              FormatRecord(record.Arg8, exportFormatConfig), _
                                                                              FormatRecord(record.Arg9, exportFormatConfig), _
                                                                              FormatRecord(record.Arg10, exportFormatConfig), _
                                                                              FormatRecord(record.Arg11, exportFormatConfig), _
                                                                              FormatRecord(record.Arg12, exportFormatConfig), _
                                                                              FormatRecord(record.Arg13, exportFormatConfig), _
                                                                              FormatRecord(record.Arg14, exportFormatConfig), _
                                                                              FormatRecord(record.Arg15, exportFormatConfig), _
                                                                              FormatRecord(record.Arg16, exportFormatConfig), _
                                                                              FormatRecord(record.Arg17, exportFormatConfig), _
                                                                              FormatRecord(record.Arg18, exportFormatConfig), _
                                                                              FormatRecord(record.Arg19, exportFormatConfig), _
                                                                              FormatRecord(record.Arg20, exportFormatConfig), _
                                                                              FormatRecord(record.PolyDegree, exportFormatConfig), _
                                                                              FormatRecord(record.ProcType, exportFormatConfig))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportScaniValveReal : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportScaniValveReal : Could not export properly")
                    Return False
                End Try

                Return True
            End Function

            Public Function ExportSerialBoolean(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportSerialBoolean
                Dim csvDelimited As String = ","
                Try

                    Dim tableCollection As New CEL.Data.BusinessObjects.DacnTemplateBoolCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)
                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        writer = New StreamWriter((pathName + exportFormatConfig.FileName))
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If

                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}", _
                                                              csvDelimited, _
                                                              "RdataID", "Name", "FieldEquipementID", "DeviceID", "Channel", "bit")
                        writer.WriteLine(strTemp)
                    End If

                    'sorting and selection of what we want
                    Dim finalColl As Object = From fin In CType(tableCollection, IEnumerable)
                                    Where fin.projectid = m_CELProject.Project.Number.ToString And fin.ExportToCSV = True
                                    Order By fin.RdataID

                    ''put into string to be saved into file
                    For Each record As DacnTemplateBool In finalColl
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}", _
                                                              csvDelimited, _
                                                              FormatRecord(record.RDataID, exportFormatConfig), _
                                                              FormatRecord(record.Name, exportFormatConfig), _
                                                              FormatRecord(record.FieldEquipmentID, exportFormatConfig), _
                                                              FormatRecord(record.DeviceID, exportFormatConfig), _
                                                              FormatRecord(record.Channel, exportFormatConfig), _
                                                              FormatRecord(record.Bit, exportFormatConfig))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportSerialBoolean : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportSerialBoolean : Could not export properly")
                    Return False
                End Try

                Return True
            End Function

            Public Function ExportSerialReal(ByVal pathName As String, ByVal tableName As String, Optional ByVal encoding As System.Text.Encoding = Nothing) As Boolean Implements IExportEngine.ExportSerialReal
                Dim csvDelimited As String = ","
                Try
                    Dim tableCollection As New CEL.Data.BusinessObjects.DacnTemplateRealCollection
                    tableCollection.Query.es.QuerySource = tableName
                    tableCollection.es.Connection.Name = m_CELProject.DefaultConnectionName
                    tableCollection.Query.Where(tableCollection.Query.ExportToCsv = True)
                    tableCollection.LoadAll()

                    Dim exportFormatConfig As ExportFormatInformationItem = CELDAS.LoadCsvExportConfig(tableName)
                    'check if directory exist, if not, create it.
                    CheckFolderExistance(pathName)
                    'Create StreamWriter to specified location in the config file.
                    CheckFileExistance(pathName, exportFormatConfig.FileName)
                    Dim writer As StreamWriter
                    Dim fs As FileStream
                    If IsNothing(encoding) Then
                        writer = New StreamWriter((pathName + exportFormatConfig.FileName))
                    Else
                        fs = New FileStream((pathName + exportFormatConfig.FileName), FileMode.CreateNew)
                        writer = New StreamWriter(fs, encoding)
                    End If

                    ''sorting and selection of what we want
                    Dim finalColl As Object = From coll In CType(tableCollection, IEnumerable)
                                    Where coll.projectid = m_CELProject.Project.Number.ToString And coll.ExportToCSV = True
                                    Order By coll.RdataID

                    If exportFormatConfig.ColumnNameHeader = True Then
                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}{0}" + _
                                                              "{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}{36}{0}{37}{0}{38}{0}{39}{0}" + _
                                                              "{40}{0}{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}{51}{0}{52}{0}{53}{0}{54}{0}{55}{0}{56}{0}{57}{0}{58}{0}" + _
                                                              "{59}{0}{60}{0}{61}{0}{62}{0}{63}{0}{64}{0}{65}{0}{66}{0}{67}{0}{68}{0}{69}{0}{70}{0}{71}{0}{72}{0}{73}{0}{74}{0}{75}{0}{76}{0}{77}{0}" + _
                                                              "{78}{0}{79}{0}{80}{0}{81}{0}{82}{0}{83}{0}{84}{0}{85}{0}{86}{0}{87}{0}{88}{0}{89}{0}{90}{0}{91}{0}{92}{0}{93}{0}{94}{0}{95}{0}{96}{0}" + _
                                                              "{97}{0}{98}{0}{99}{0}{100}{0}{101}{0}{102}{0}{103}{0}{104}", _
                                                              csvDelimited, "RdataID", "Name", "FieldEquipmentID", "DeviceID", "BaseAddress", "PhysicalChannel", _
                                                              "AlarmOn", "TextConversionOn", "StatisticsOn", "DataAcquisitionOn", "DataProcessingOn", "PeriodInMsec", "TreatOnOption", "DefaultValue", _
                                                              "DefaultValue", "UpperLimit", "LowerLimit", "DeltaVPlus", "DeltaVMinus", "RocPlus", "RocMinus", "EventOn", "HighHighLimitOn", "HighLimitOn", _
                                                              "LowLimitOn", "LowLowLimitOn", "HighHighAlarmOn", "HighAlarmOn", "LowAlarmOn", "LowLowAlarmOn", "HighHighLimit", "HighLimit", "LowLimit", _
                                                              "LowLowLimit", "HighHighAlarm", "HighAlarm", "LowAlarm", "LowLowAlarm", "HighHighBand", "HighBand", "LowBand", "LowLowBand", "HighHighAlarmSource", _
                                                              "HighAlarmSource", "LowAlarmSource", "LowLowAlarmSource", "HighHighAlarmName", "HighAlarmName", "LowAlarmName", "LowLowAlarmName", "TextPv", _
                                                              "HMIAliasName", "FILEAliasName", "EngUnit", "MinMaxOn", "DeltaVOn", "SumVOn", "DeltaTOn", "SumTOn", "RocOn", "AvgOn", "AvgTypeID", "AvgPeriod", _
                                                              "AvgNbSamples", "TrigCondID", "TrigLevel", "TrigDeadBand", "TimeOut", "QueueOption", "QueueSize", "PreTrig", "MaxRecord", "ArgSrc", "NumOfArg", _
                                                              "ValidArgOn", "NumOfValidArg", "Coeff1", "Coeff2", "Coeff3", "Coeff4", "Coeff5", "Coeff6", "Arg1", "Arg2", "Arg3", "Arg4", "Arg5", "Arg6", "Arg7", _
                                                              "Arg8", "Arg9", "Arg10", "Arg11", "Arg12", "Arg13", "Arg14", "Arg15", "Arg16", "Arg17", "Arg18", "Arg19", "Arg20", "PolyDegree", "ProcType")
                        writer.WriteLine(strTemp)
                    End If

                    For Each record As DacnTemplateReal In finalColl

                        'EngUnit need to be something. In case of nothing or "", the string is set to none to avoid errors during csv write
                        If (record.EngUnit Is Nothing) Or (record.EngUnit = "") Then record.EngUnit = "none"

                        Dim strTemp As String = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}{0}" + _
                                                              "{21}{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}{0}{32}{0}{33}{0}{34}{0}{35}{0}{36}{0}{37}{0}{38}{0}{39}{0}" + _
                                                              "{40}{0}{41}{0}{42}{0}{43}{0}{44}{0}{45}{0}{46}{0}{47}{0}{48}{0}{49}{0}{50}{0}{51}{0}{52}{0}{53}{0}{54}{0}{55}{0}{56}{0}{57}{0}{58}{0}" + _
                                                              "{59}{0}{60}{0}{61}{0}{62}{0}{63}{0}{64}{0}{65}{0}{66}{0}{67}{0}{68}{0}{69}{0}{70}{0}{71}{0}{72}{0}{73}{0}{74}{0}{75}{0}{76}{0}{77}{0}" + _
                                                              "{78}{0}{79}{0}{80}{0}{81}{0}{82}{0}{83}{0}{84}{0}{85}{0}{86}{0}{87}{0}{88}{0}{89}{0}{90}{0}{91}{0}{92}{0}{93}{0}{94}{0}{95}{0}{96}{0}" + _
                                                              "{97}{0}{98}{0}{99}{0}{100}{0}{101}{0}{102}{0}{103}{0}{104}", _
                                                                                        csvDelimited, _
                                                                                        FormatRecord(record.RdataID, exportFormatConfig), _
                                                                                        FormatRecord(record.Name, exportFormatConfig), _
                                                                                        FormatRecord(record.FieldEquipmentID, exportFormatConfig), _
                                                                                        FormatRecord(record.DeviceID, exportFormatConfig), _
                                                                                        FormatRecord(record.BaseAddress, exportFormatConfig), _
                                                                                        FormatRecord(record.PhysicalChannel, exportFormatConfig), _
                                                                                        record.AlarmOnInteger, _
                                                                                        record.TextConversionOnInteger, _
                                                                                        record.statisticsOnInteger, _
                                                                                        record.DataAcquisitionOnInteger, _
                                                                                        record.DataProcessingOnInteger, _
                                                                                        FormatRecord(record.PeriodInMsec, exportFormatConfig), _
                                                                                        FormatRecord(record.TreatOnOption, exportFormatConfig), _
                                                                                        FormatRecord(record.DefaultValue, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.DefaultValue, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.UpperLimit, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.LowerLimit, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.DeltaVPlus, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.DeltaVMinus, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.RocPlus, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.RocMinus, exportFormatConfig, 7), _
                                                                                        record.EventOnInteger, _
                                                                                        record.HighHighLimitOnInteger, _
                                                                                        record.HighLimitOnInteger, _
                                                                                        record.LowLimitOnInteger, _
                                                                                        record.LowLowLimitOnInteger, _
                                                                                        record.HighHighAlarmOnInteger, _
                                                                                        record.HighAlarmOnInteger, _
                                                                                        record.LowAlarmOnInteger, _
                                                                                        record.LowLowAlarmOnInteger, _
                                                                                        FormatRecord(record.HighHighLimit, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.HighLimit, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.LowLimit, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.LowLowLimit, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.HighHighAlarm, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.HighAlarm, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.LowAlarm, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.LowLowAlarm, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.HighHighBand, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.HighBand, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.LowBand, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.LowLowBand, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.HighHighAlarmSource, exportFormatConfig), _
                                                                                        FormatRecord(record.HighAlarmSource, exportFormatConfig), _
                                                                                        FormatRecord(record.LowAlarmSource, exportFormatConfig), _
                                                                                        FormatRecord(record.LowLowAlarmSource, exportFormatConfig), _
                                                                                        FormatRecord(record.HighHighAlarmName, exportFormatConfig), _
                                                                                        FormatRecord(record.HighAlarmName, exportFormatConfig), _
                                                                                        FormatRecord(record.LowAlarmName, exportFormatConfig), _
                                                                                        FormatRecord(record.LowLowAlarmName, exportFormatConfig), _
                                                                                        FormatRecord(record.TextPvFormat, exportFormatConfig), _
                                                                                        FormatRecord(record.HMIAliasName, exportFormatConfig), _
                                                                                        FormatRecord(record.FILEAliasName, exportFormatConfig), _
                                                                                        FormatRecord(record.EngUnit, exportFormatConfig), _
                                                                                        record.MinMaxOnInteger, _
                                                                                        record.DeltaVOnInteger, _
                                                                                        record.SumVOnInteger, _
                                                                                        record.DeltaTOnInteger, _
                                                                                        record.SumTOnInteger, _
                                                                                        record.RocOnInteger, _
                                                                                        record.AvgOnInteger, _
                                                                                        FormatRecord(record.AvgTypeID, exportFormatConfig), _
                                                                                        FormatRecord(record.AvgPeriod, exportFormatConfig), _
                                                                                        FormatRecord(record.AvgNbSamples, exportFormatConfig), _
                                                                                        FormatRecord(record.TrigCondID, exportFormatConfig), _
                                                                                        FormatRecord(record.TrigLevel, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.TrigDeadBand, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.TimeOut, exportFormatConfig), _
                                                                                        FormatRecord(record.QueueOption, exportFormatConfig), _
                                                                                        FormatRecord(record.QueueSize, exportFormatConfig), _
                                                                                        FormatRecord(record.PreTrigRecord, exportFormatConfig), _
                                                                                        FormatRecord(record.MaxRecord, exportFormatConfig), _
                                                                                        FormatRecord(record.ArgSrc, exportFormatConfig), _
                                                                                        FormatRecord(record.NumOfArg, exportFormatConfig), _
                                                                                        record.ValidArgOnInteger, _
                                                                                        FormatRecord(record.NumOfValidArg, exportFormatConfig), _
                                                                                        FormatRecord(record.Coeff1, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.Coeff2, exportFormatConfig, 6), _
                                                                                        FormatRecord(record.Coeff3, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.Coeff4, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.Coeff5, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.Coeff6, exportFormatConfig, 7), _
                                                                                        FormatRecord(record.Arg1, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg2, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg3, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg4, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg5, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg6, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg7, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg8, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg9, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg10, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg11, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg12, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg13, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg14, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg15, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg16, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg17, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg18, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg19, exportFormatConfig), _
                                                                                        FormatRecord(record.Arg20, exportFormatConfig), _
                                                                                        FormatRecord(record.PolyDegree, exportFormatConfig), _
                                                                                        FormatRecord(record.ProcType, exportFormatConfig))
                        writer.WriteLine(strTemp)
                    Next

                    writer.Flush()
                    writer.Close()
                    File.SetCreationTimeUtc(pathName & exportFormatConfig.FileName, Now)
                Catch ex As System.Data.OleDb.OleDbException
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportSerialReal : Could not properly connect to database. Do you have the right path?", ex.Message)
                    Return False
                Catch ex As Exception
                    Debug.Assert(False, "Error: CEL.HMI.HardwareConfiguration.ExportSerialReal : Could not export properly")
                    Return False
                End Try

                Return True
            End Function

        End Class

        ''' <summary>
        ''' Allow the selection of the export engine.
        ''' </summary>
        ''' <param name="configFileExportType"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ExportEngineSelection(ByVal configFileExportType As eExportType) As IExportEngine
            Dim engineSelection As IExportEngine = Nothing

            'Tied with the intergace IExportEngine.
            If configFileExportType = eExportType.CSVDelimited Then
                engineSelection = New CsvExportEngine
            Else
                Debug.Assert(False)
            End If

            'Dim strtest As String = [Enum].GetName(GetType(ExportInformationElement.eExportType), 0)
            Return engineSelection
        End Function

        ''' <summary>
        ''' Check if a file exist and Deletes it if it does, so a new file can be written.
        ''' </summary>
        ''' <param name="pathName"></param>
        ''' <param name="fileName"></param>
        ''' <remarks> Doesn't clear the "buffer". If a new file has the same name, often, the creation date will be the same as the old file creation date. 
        ''' Use [File.SetCreationTimeUtc(pathName & FileName, Now)] to rewrite de creation time to the correct time.</remarks>
        Public Shared Sub CheckFileExistance(ByVal pathName As String, ByVal fileName As String)

            Dim curFile As String = pathName & fileName
            Dim bool As Boolean = System.IO.File.Exists(curFile)

            If System.IO.File.Exists((pathName + fileName)) Then
                System.IO.File.Delete((pathName + fileName))
            End If

        End Sub

        ''' <summary>
        ''' Create a folder(s) if the requested pathName is non-existant.
        ''' </summary>
        ''' <param name="pathName"></param>
        ''' <remarks>There is a maximum path lenght. 260 char</remarks>
        Public Shared Sub CheckFolderExistance(ByVal pathName As String)
            Try
                If (Not System.IO.Directory.Exists(pathName)) Then
                    System.IO.Directory.CreateDirectory(pathName)
                End If
            Catch ex As Exception
                Debug.Assert(False, "CEL.HMI.HardwareConfiguration.CheckFolderExistance: ", ex.Message)
            End Try

        End Sub

        ''' <summary>
        ''' Format a string according the the very weird spec. of the workbench
        ''' </summary>
        ''' <param name="record"></param>
        ''' <param name="formatConfig"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FormatRecord(ByVal record As String, ByVal formatConfig As ExportFormatInformationItem) As String
            '°=alt248 De rien!
            If record = Nothing Then
                record = """" + """"
            End If

            record = CStr(record).Replace(".", formatConfig.DecimalSymbol)

            'add delimiter
            If formatConfig.TextDelimiter <> "none" Then
                If Char.IsLetter(CStr(record)) Or record.Contains("%") Or record.Contains("°") Then
                    record = formatConfig.TextDelimiter + record + formatConfig.TextDelimiter
                End If
            End If

            record = record.Replace("Ω"c, "O"c)

            Return record
        End Function

        ''' <summary>
        ''' Format a double with a particular number of digit after the .
        ''' </summary>
        ''' <param name="record"></param>
        ''' <param name="formatConfig"></param>
        ''' <param name="customNumberDigit"></param>
        ''' <returns>Reutnr a string with the indicated number of digit</returns>
        ''' <remarks></remarks>
        Public Shared Function FormatRecord(ByVal record As Double, ByVal formatConfig As ExportFormatInformationItem, ByVal customNumberDigit As Integer) As String

            Dim temp As String
            temp = CStr(record.ToString(("F" + customNumberDigit.ToString)))

            temp = temp.Replace(".", formatConfig.DecimalSymbol)
            Return temp
        End Function

        ''' <summary>
        ''' Format a Date element to the FTP date format (in a string).
        ''' </summary>
        ''' <param name="vbDate"></param>
        ''' <returns>A string with the date</returns>
        ''' <remarks>date follow YYYYMMDDHHMMSS format</remarks>
        Public Function VbUtcTimeFormater(ByVal vbDate As Date) As String
            Dim time As String = vbDate.ToString("yyyyMMddHHmmss")

            Return time
        End Function

        ''' <summary>
        ''' Format a FTP date format element to a date element.
        ''' </summary>
        ''' <param name="ftpDate"></param>
        ''' <returns>a date </returns>
        ''' <remarks></remarks>
        Public Function ftpUtcTimeFormater(ByVal ftpDate As String) As Date
            Dim time As Date = Date.ParseExact(ftpDate, "yyyyMMddHHmmss", _
                                               System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Return time
        End Function

        ''' <summary>
        ''' Export 1 (one) file to a FTP server.
        ''' </summary>
        ''' <param name="filePathName"></param>
        ''' <param name="pathNameOnServer"></param>
        ''' <param name="fileNameOnServer"></param>
        ''' <param name="serverIPAddress"></param>
        ''' <param name="userName"></param>
        ''' <param name="passWord"></param>
        ''' <remarks>This function doesnt do well for multiple file transfer as it close the connection each time.</remarks>
        Public Sub FtpFileUpload(ByVal filePathName As String, ByVal pathNameOnServer As String, ByVal fileNameOnServer As String, ByVal serverIPAddress As String, ByVal userName As String, ByVal passWord As String)
            Try
                Dim ftp As CEL.Net.FtpClient = InitFTPConnection(False, serverIPAddress, userName, passWord, True)

                ftp.ChangeCreateDir(pathNameOnServer)

                ftp.BinaryMode = True
                ftp.Upload(filePathName, fileNameOnServer, False)
                ftp.Close()
            Catch ex As Exception
                Console.WriteLine("Error from:CEL.HMI.HardwareConfiguration.ftpupload: Parameter probably missing.{0}", ex.Message)
                Debug.Assert(False, "Error from:CEL.HMI.HardwareConfiguration.ftpupload: Parameter probably missing.")
            End Try
        End Sub

        ''' <summary>
        ''' Upload a file. Require a configured ftp connection before calling this sub
        ''' </summary>
        ''' <param name="filePathName"></param>
        ''' <param name="pathNameOnServer"></param>
        ''' <param name="fileNameOnServer"></param>
        ''' <remarks>This function is made to upload multiple file at once since it doesn't close the FTP connection.</remarks>
        Public Sub FtpFileUpload(ByVal filePathName As String, ByVal pathNameOnServer As String, ByVal fileNameOnServer As String, ByRef ftpConnection As CEL.Net.FtpClient)
            Try
                ftpConnection.BinaryMode = True
                ftpConnection.Upload(filePathName, fileNameOnServer, False)
            Catch ex As Exception
                Debug.Assert(False)
                MsgBox(ex.Message)
            End Try
        End Sub
        ''' <summary>
        ''' Cycles trough a directory, uploading each file it contain. The name of the file will be the same as the name of the file on the server.Slow and methodical
        ''' </summary>
        ''' <param name="csvDirectoryPath"></param>
        ''' <param name="pathNameOnServer"></param>
        ''' <param name="serverName"></param>
        ''' <param name="userName"></param>
        ''' <param name="passWord"></param>
        ''' <remarks>Very slow doesn't exploit the property of the FTP protocol.</remarks>
        Public Sub FtpDirectoryUpload(ByVal csvDirectoryPath As String, ByVal pathNameOnServer As String, ByVal serverName As String, ByVal userName As String, ByVal passWord As String)
            For Each element As String In Directory.GetFiles(csvDirectoryPath)

                FtpFileUpload(element, pathNameOnServer, Path.GetFileName(element), serverName, userName, passWord)

            Next element
        End Sub

        ''' <summary>
        ''' Upload a directory to an ftp server. Will upload everything in said directory to target specified in configuration file
        ''' </summary>
        ''' <param name="DirectoryPath"></param>
        ''' <param name="pathNameOnServer"></param>
        ''' <param name="serverName"></param>
        ''' <param name="userName"></param>
        ''' <param name="passWord"></param>
        ''' <returns>True if successful, False otherwise</returns>
        ''' <remarks>Was first used for CSV transfer but could cause problem if target was something like system32. A new one was develop called : [FtpAllCsvUpload]</remarks>
        Public Function FtpFastDirectoryUpload(ByVal DirectoryPath As String, ByVal pathNameOnServer As String, ByVal serverName As String, ByVal userName As String, ByVal passWord As String, Optional ByRef backgroundWorker As BackgroundWorker = Nothing) As Boolean
            Try
                Dim ftp As CEL.Net.FtpClient = InitFTPConnection(False, serverName, userName, passWord, True)
                ftp.ChangeCreateDir(pathNameOnServer)

                Dim counter As Integer = 0
                If System.IO.Directory.Exists(DirectoryPath) Then

                    Dim i As Integer = 0
                    For Each element As String In Directory.GetFiles(DirectoryPath)
                        If ConstantCreationTimeFtpUpload(element, Path.GetFileName(element), ftp) Then
                            If backgroundWorker IsNot Nothing Then
                                backgroundWorker.ReportProgress(i, Path.GetFileName(element))
                                i += 1
                            End If
                        Else
                            Debug.Assert(False, "There was a problem uploading the file. Either the file doesn't exist or the server doesn't exist.")
                        End If
                        counter += 1
                    Next element

                    If counter = 0 Then
                        Debug.Assert(False, "The Directory you are trying to Upload appears empty.")
                    End If
                End If
                If Not System.IO.Directory.Exists(DirectoryPath) Then
                    MsgBox("The Folder/Directory you are trying to upload FROM doesn't seem to exist.")
                End If
                ftp.Close()
            Catch ex As Exception
                Debug.Assert(False, "Error from: CEL.HMI.HardwareConfiguration.FtpFastDirectoryUpload: Upload Error: ", ex.Message)
                Return False
            End Try

            Return True
        End Function

        ''' <summary>
        ''' Upload the csv files from the configfile
        ''' </summary>
        ''' <param name="DirectoryPath"></param>
        ''' <param name="pathNameOnServer"></param>
        ''' <param name="serverName"></param>
        ''' <param name="userName"></param>
        ''' <param name="passWord"></param>
        ''' <param name="backgroundWorker"></param>
        ''' <returns>True if succesful; False otherwise</returns>
        ''' <remarks>This will cycle trough the csv config section of the config file and only upload the files with a filename associated with them.</remarks>
        Public Function FtpAllCsvUpload(ByVal DirectoryPath As String, ByVal pathNameOnServer As String, ByVal serverName As String, ByVal userName As String, ByVal passWord As String, Optional ByRef backgroundWorker As BackgroundWorker = Nothing) As Boolean
            Try
                'Ouverture de connection ftp
                Dim ftp As CEL.Net.FtpClient = InitFTPConnection(False, serverName, userName, passWord, True)
                ftp.ChangeCreateDir(pathNameOnServer)

                'Check to see if the file exist.
                If System.IO.Directory.Exists(DirectoryPath) Then
                    Dim i As Integer = 0

                    'cycle through the csv file name in the configuration file.
                    For Each csvFile As ExportFormatInformationItem In m_CELProject.LoadFormatCollection
                        'The Omnibusbox UART CSV file is now generated dynamically
                        'if the file exist, continue uploading otherwise, dont.
                        If File.Exists(DirectoryPath & csvFile.FileName) Then
                            'Upload and change the time of creation of the file that was uploaded to the time of the server.
                            If ConstantCreationTimeFtpUpload(DirectoryPath & csvFile.FileName, csvFile.FileName, ftp) Then
                                'This is used for threading the progressbar or any other backgroundWorker.
                                If backgroundWorker IsNot Nothing Then
                                    backgroundWorker.ReportProgress(i, csvFile.FileName)
                                    i += 1
                                End If
                            End If
                        Else
                            MsgBox(csvFile.FileName & " could not be uploaded since it is missing from the specified export directory" _
                                   & vbCr & "Please export the file before attempting to upload it again.")
                        End If
                    Next csvFile
                Else
                    MsgBox("The directory you have specified to upload FROM in the configuration file doesn't appear to exist. Please make sure the informations are correct.", MsgBoxStyle.OkOnly)
                End If

                ftp.Close()
            Catch ex As Exception
                Debug.Assert(False, "Error from: CEL.HMI.HardwareConfiguration.FtpAllCsvUpload: Upload Error: ", ex.Message)
                Return False
            End Try

            Return True
        End Function

        ''' <summary>
        ''' Compare 2 (two) file time created.
        ''' </summary>
        ''' <param name="FileNameSystem"></param>
        ''' <param name="FtpFileName"></param>
        ''' <param name="ftpVar"></param>
        ''' <returns>Return True if difference is detected; false otherwise</returns>
        ''' <remarks>Only work for a file system vs ftpFile. Must already be in the right directory, ftp wise.</remarks>
        Public Function FileTimeComparatorFtp(ByVal systemFilepathName As String, ByVal ftpFileName As String, ByRef ftpConnection As CEL.Net.FtpClient) As Boolean

            Dim fileCreationTime As String = My.Computer.FileSystem.GetFileInfo(systemFilepathName).CreationTimeUtc
            Dim ftpFileCreationTime As String = ftpUtcTimeFormater(ftpConnection.GetLastMod(ftpFileName))

            If ftpFileCreationTime <> fileCreationTime Then Return True
            Return False
        End Function

        ''' <summary>
        '''  Compare 2 (two) file size between an ftpFile and a systemFile
        ''' </summary>
        ''' <param name="FileNameSystem"></param>
        ''' <param name="FtpFileName"></param>
        ''' <param name="ftpVar"></param>
        ''' <returns>Return True if difference is detected; false otherwise</returns>
        ''' <remarks>Only work for a file system vs ftpFile. Must already be in the right ftp directory.
        '''  File must exist, Ensure file is present in the ftp server before calling function.<</remarks>
        Public Function FileSizeComparatorFtp(ByVal systemFilepathName As String, ByVal ftpFileName As String, ByRef ftpConnection As CEL.Net.FtpClient) As Boolean
            Try
                'This config should already be enabled but just in case we re-enable it here.
                ftpConnection.BinaryMode = True
                If System.IO.File.Exists(systemFilepathName) Then
                    'Get file size.
                    Dim fileSize As Long = My.Computer.FileSystem.GetFileInfo(systemFilepathName).Length
                    'Get file size from ftp.
                    Dim ftpFileSize As Long = ftpConnection.GetFileSize(ftpFileName)

                    If ftpFileSize <> fileSize Then Return True
                End If
                Return False
            Catch ex As Exception
                Debug.Assert(False, "Error: Cel.Net.HardwareConfiguration.FilesizeComparatorFtp: Could not find size of file for some reason. Does file exist?")
            End Try

        End Function

        ''' <summary>
        ''' Modify the creation time of a file on the system by using the creationtime of the server.
        ''' </summary>
        ''' <param name="systemFilepathName"></param>
        ''' <param name="ftpFileName"></param>
        ''' <param name="ftpVar"></param>
        ''' <remarks>This allow tracking of file by their creation date.</remarks>
        Public Sub FileTimeCorrector(ByVal systemFilepathName As String, ByVal ftpFileName As String, ByRef ftpConnection As CEL.Net.FtpClient)
            Try
                Dim serverTime As String = ftpConnection.GetLastMod(ftpFileName)
                Dim minutesString As String = serverTime.Substring(10, 2)
                ''Prevent Error since date cannot be greater then 59.
                If minutesString > 58 Then
                    serverTime = serverTime.Remove(10, 1).Insert(10, 0)
                    serverTime = serverTime.Remove(11, 1).Insert(11, 0)
                End If

                'Change creation Time of csv file on Windows, to allow tracking.
                File.SetCreationTimeUtc(systemFilepathName, ftpUtcTimeFormater(serverTime))
                'The modification time must always be > then the creation time. ALWAYS! .... I'm serious!
                'The + 100 is to make sure the mod. time is greater then creation time by 1 min.
                File.SetLastWriteTimeUtc(systemFilepathName, ftpUtcTimeFormater(serverTime + 100))
                File.SetLastAccessTimeUtc(systemFilepathName, ftpUtcTimeFormater(serverTime + 100))
            Catch es As Exception
                Debug.Assert(False, "Error: HardwareConfiguration.FileTimeCorrector: Cannot modify file time. Does file Exist?", es.Message)
            End Try

        End Sub

        ''' <summary>
        ''' Upload a file and modify the creation time on the windows platform
        ''' </summary>
        ''' <param name="filePathName"></param>
        ''' <param name="fileNameOnServer"></param>
        ''' <param name="ftpConnection"></param>
        ''' <param name="fileExist"></param>
        ''' <returns>Return True if succesful; False otherwise.</returns>
        ''' <remarks>You need to already be inside the right ftp directory.</remarks>
        Public Function ConstantCreationTimeFtpUpload(ByVal filePathName As String, ByVal fileNameOnServer As String, ByRef ftpConnection As CEL.Net.FtpClient, Optional ByVal fileExist As Boolean = False) As Boolean
            Try
                Dim reTryTime As Integer = 0
                Dim originalFileTime As Date = #1/2/3456 7:08:09 AM#
                Dim newFileTime As Date = #1/2/3456 7:08:09 AM#

                'Make sure the upload is done correctly
                Do While originalFileTime = newFileTime And reTryTime < 3

                    Dim serverTime As String = ftpConnection.GetLastMod(fileNameOnServer)
                    If serverTime <> Nothing Then
                        originalFileTime = File.GetLastWriteTimeUtc(filePathName)
                        'This intercept the function if the file has already been uploaded.
                        If ftpUtcTimeFormater(serverTime) = originalFileTime Then
                            Console.WriteLine("File as already been uploaded!! No need to reupload!")
                            Return True
                        End If
                    End If

                    'Just in case
                    ftpConnection.BinaryMode = True
                    'upload file specified in the export directory, (Name will be changed to what is specified in the cfg file).
                    ftpConnection.Upload(filePathName, fileNameOnServer, False)
                    'Correction du file creation time par rapport a la date de creation du serveur.
                    FileTimeCorrector(filePathName, fileNameOnServer, ftpConnection)

                    newFileTime = File.GetCreationTimeUtc(filePathName)
                    reTryTime += 1
                Loop

                If reTryTime = 2 Then
                    Return False
                End If

            Catch ex As Exception
                Debug.Assert(False, "ERROR: Cel.HMI.HardwareConfiguration.ConstantCreationTimeFtpUpload: Failed to alter time of Csv config file.")
                Return False
            End Try
            Dim nameFile() As String = filePathName.Split("\")
            Dim trunkaFileName As String = nameFile(nameFile.Length - 1)
            If nameFile.Length < 0 Then trunkaFileName = ""

            Console.WriteLine("[{0}]'s creation time was changed to the server creation time.", trunkaFileName)
            Return True
        End Function

        ''' <summary>
        ''' Check if size and Time match.
        ''' </summary>
        ''' <param name="csvFilePath"></param>
        ''' <param name="ftpFileName"></param>
        ''' <param name="ftpConnection"></param>
        ''' <returns>Return true if discrepancy was detected; False otherwise</returns>
        ''' <remarks>Require that the files exist. Will check both size and time of creation. If a problem is detected return true, False otherwise</remarks>
        Public Function ConfigFileDiscrepancyDetector(ByVal filePath As String, ByVal ftpFileName As String, ByRef ftpConnection As CEL.Net.FtpClient) As Boolean

            If FileSizeComparatorFtp(filePath, ftpFileName, ftpConnection) Then Return True

            If FileTimeComparatorFtp(filePath, ftpFileName, ftpConnection) Then Return True

            Return False
        End Function
    End Class
End Namespace
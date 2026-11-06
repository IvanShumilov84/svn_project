Imports FileHelpers
'Imports FileHelpers.Events
Imports System.Windows.Forms
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Diagnostics
Imports CEL.HMI
Imports System.Threading
Imports System.Data.OleDb
Imports System.Reflection
Imports CEL.Data.BusinessObjects

Public Class ImportExportAfdxDefitionForm

    Private CELProject As CELDAS

    Public Sub New(ByRef _CELProject As CELDAS)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CELProject = _CELProject
    End Sub

    Private Sub btnSelectFile_Click(sender As Object, e As EventArgs) Handles btnSelectFile.Click

        ' Set the file dialog to filter for graphics files.
        Me.OpenFileDialog1.Filter = "ICD Definition (*.csv;)|*.csv;|" + "ICD Definition (*.xls;)|*.xls;|" + "ICD Definition (*.xlsx;)|*.xlsx;|" + "All files (*.*)|*.*"

        Me.OpenFileDialog1.Multiselect = False
        Me.OpenFileDialog1.Title = "Please Select a File"

        Dim directoryName As String = String.Empty
        If (txtFileName.Text <> String.Empty) Then
            directoryName = System.IO.Path.GetDirectoryName(txtFileName.Text) '"C:\My Projects\ISaGRAF Enhanced\Workbench\Prj"
        End If

        Me.OpenFileDialog1.InitialDirectory = directoryName

        Dim dr As DialogResult = Me.OpenFileDialog1.ShowDialog()
        If (dr = System.Windows.Forms.DialogResult.OK) Then
            ' Read the files
            Dim file As String
            For Each file In OpenFileDialog1.FileNames
                txtFileName.Text = file.ToString()
            Next file
        End If
    End Sub




    Private Sub btnImportDefinition_Click(sender As Object, e As EventArgs) Handles btnImportDefinition.Click

        If txtFileName.Text <> "" AndAlso File.Exists(txtFileName.Text) Then

            If txtFileName.Text.EndsWith("xlsx") Then
                Dim csvFileName = txtFileName.Text.Replace("xlsx", "csv")
                ExportExcelFileToCsv(txtFileName.Text, csvFileName)

                txtFileName.Text = csvFileName
            End If
            Dim pw980aImporter As New AfdxDefinitionPw980A(CELProject)
            pw980aImporter.ImportAfdxDefiniton(txtFileName.Text, Convert.ToInt32(txtDefaultMTU.Text))

            MessageBox.Show("Completed", "", MessageBoxButtons.OK)
        End If


    End Sub
    Private Sub btnExportDefinition_Click(sender As Object, e As EventArgs) Handles btnExportDefinition.Click
        Dim pw980aExporter As New AfdxDefinitionPw980A(CELProject)
        pw980aExporter.ExportAfdxDefinitonToPowerDnaFormat(txtOutputFileName.Text)
    End Sub

    'TODO deplacer dans Librairie
    Private Function ExportExcelFileToCsv(ByVal inputFileName As String, ByVal outputFileName As String) As Boolean
        ExportExcelFileToCsv = False
        Try

            'Bug excel qui ignore les regional setting lors de l<export csv!!!!!!!!!!!!!
            'http://support.microsoft.com/default.aspx?scid=kb;en-us;Q288839

            Dim excelApplication As New Excel.Application
            Dim workBook As Excel.Workbook
            excelApplication.DisplayAlerts = False


            workBook = excelApplication.Workbooks.Open(inputFileName)
            workBook.SaveAs(Filename:=outputFileName, _
            FileFormat:=Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV)

            workBook.Close()
            excelApplication.DisplayAlerts = True
            excelApplication.Quit()

            ExportExcelFileToCsv = True

        Catch ex As Exception

        End Try
    End Function
    'Private Shared Sub convertExcelToCSV(sourceFile As String, worksheetName As String, targetFile As String)
    '    Dim strConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & sourceFile & ";Extended Properties="" Excel.0;HDR=Yes;IMEX=1"""
    '    Dim conn As OleDbConnection = Nothing
    '    Dim wrtr As StreamWriter = Nothing
    '    Dim cmd As OleDbCommand = Nothing
    '    Dim da As OleDbDataAdapter = Nothing
    '    Try

    '        'conn requires MDAC 2.8 to be installed on your PC
    '        'get it at http://www.microsoft.com/en-us/download/details.aspx?id=5793
    '        conn = New OleDbConnection(strConn)
    '        conn.Open()
    '        cmd = New OleDbCommand("SELECT * FROM [" & worksheetName & "$]", conn)
    '        cmd.CommandType = CommandType.Text
    '        wrtr = New StreamWriter(targetFile)
    '        da = New OleDbDataAdapter(cmd)
    '        Dim dt As New DataTable()
    '        da.Fill(dt)
    '        For x As Integer = 0 To dt.Rows.Count - 1
    '            Dim rowString As String = ""
    '            For y As Integer = 0 To dt.Columns.Count - 1
    '                rowString += """" & dt.Rows(x)(y).ToString() & ""","
    '            Next
    '            wrtr.WriteLine(rowString)
    '        Next
    '        Console.WriteLine()
    '        Console.WriteLine("Done! Your " & sourceFile & " has been converted into " & targetFile & ".")
    '        Console.WriteLine()
    '    Catch exc As Exception
    '        Console.WriteLine(exc.ToString())
    '        Console.ReadLine()
    '    Finally
    '        If conn.State = ConnectionState.Open Then
    '            conn.Close()
    '        End If
    '        conn.Dispose()
    '        cmd.Dispose()
    '        da.Dispose()
    '        wrtr.Close()
    '        wrtr.Dispose()
    '    End Try
    'End Sub
    'Private Shared Sub ReadExcel(ByVal inputFileName As String, ByVal outputFileName As String, RowCount As Integer, ColCount As Integer, strDelimiter As String)
    '    'Write to text
    '    Dim sw As StreamWriter = New StreamWriter(outputFileName)
    '    Dim strCell As [String] = ""
    '    Dim col As Integer

    '    Dim Opt As [Object] = Missing.Value
    '    Dim app As Microsoft.Office.Interop.Excel.Application = New Excel.Application()
    '    '= new Excel.Application();
    '    'Declare Workbook
    '    Dim book As Microsoft.Office.Interop.Excel.Workbook

    '    'Excel.Worksheet sheet;
    '    Dim xlsSheet As Microsoft.Office.Interop.Excel.Worksheet
    '    Dim oRng As Microsoft.Office.Interop.Excel.Range

    '    'Open Spreadsheet
    '    book = app.Workbooks.Open(inputFileName, Opt, Opt, Opt, Opt, Opt, _
    '        Opt, Opt, Opt, Opt, Opt, Opt, _
    '        Opt, Opt, Opt)

    '    'Take the first sheet
    '    xlsSheet = DirectCast(book.Sheets(1), Microsoft.Office.Interop.Excel.Worksheet)

    '    'Read the first cell
    '    oRng = DirectCast(xlsSheet.Cells(1, 1), Microsoft.Office.Interop.Excel.Range)

    '    For row As Integer = 0 To RowCount - 1
    '        strCell = ""
    '        For col = 0 To ColCount - 1
    '            oRng = DirectCast(xlsSheet.Cells(row + 1, col + 1), Microsoft.Office.Interop.Excel.Range)
    '            If col < ColCount - 1 Then
    '                strCell = strCell + oRng.Text.ToString() + strDelimiter
    '            Else
    '                strCell = strCell + oRng.Text.ToString(),
    '            End If
    '        Next
    '        'Write to text file
    '        sw.WriteLine(strCell)
    '    Next




    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        txtFileName.Text = "C:\Users\francoist\Desktop\ICD_APU_T35_L4961SP0400926ICD1.xlsx"

        If txtFileName.Text <> "" AndAlso File.Exists(txtFileName.Text) Then

            If txtFileName.Text.EndsWith("xlsx") Then
                Dim csvFileName = txtFileName.Text.Replace("xlsx", "csv")
                ExportExcelFileToCsv(txtFileName.Text, csvFileName)

                txtFileName.Text = csvFileName
            End If

            MessageBox.Show("Completed", "", MessageBoxButtons.OK)
        End If

    End Sub


    Private Sub btnCreateCSV_Click(sender As Object, e As EventArgs) Handles btnCreateCSV.Click
        '    Dim outputFileName As String = "C:\My Projects\CELDAS 200\Workbench\DACN1_AFDX_VIRTUAL_LINK_CSV.csv"
        '    If File.Exists(outputFileName) Then
        '        File.Delete(outputFileName)
        '    End If


        '    'Dim writer As New StreamWriter(outputFileName)
        '    'Dim vlCollection As New Dacn1AfdxVirtualLinkCollection()
        '    'vlCollection.es.Connection.Name = CELProject.DefaultConnectionName
        '    'vlCollection.Query.Where(vlCollection.Query.CollectionName = "PW980A" And vlCollection.Query.ExportToCsv = True)
        '    ''vlCollection.Query.Where(vlCollection.Query.CollectionName = "PW980A")
        '    'vlCollection.Query.OrderBy(vlCollection.Query.CollectionName.Ascending)
        '    'vlCollection.Query.OrderBy(vlCollection.Query.Vlid.Ascending)
        '    'vlCollection.Query.Load()

        '    ''SELECT DACN1_AFDX_VIRTUAL_LINK.CollectionName, DACN1_AFDX_VIRTUAL_LINK.VLID, DACN1_AFDX_VIRTUAL_LINK.VLMode, DACN1_AFDX_VIRTUAL_LINK.NumberOfSubVL, DACN1_AFDX_VIRTUAL_LINK.SubVLID, DACN1_AFDX_VIRTUAL_LINK.TxRx, DACN1_AFDX_VIRTUAL_LINK.BAG, DACN1_AFDX_VIRTUAL_LINK.MTU, DACN1_AFDX_VIRTUAL_LINK.MaxJitter, DACN1_AFDX_VIRTUAL_LINK.SkewMax, DACN1_AFDX_VIRTUAL_LINK.SkewDelay, DACN1_AFDX_VIRTUAL_LINK.PrevSnRange, DACN1_AFDX_VIRTUAL_LINK.EDEEnabled, DACN1_AFDX_VIRTUAL_LINK.EDESid, DACN1_AFDX_VIRTUAL_LINK.EDESkewMax, DACN1_AFDX_VIRTUAL_LINK.EDEAgeMax, DACN1_AFDX_VIRTUAL_LINK.EDELagMax
        '    ''FROM DACN1_AFDX_VIRTUAL_LINK
        '    ''ORDER BY DACN1_AFDX_VIRTUAL_LINK.CollectionName, DACN1_AFDX_VIRTUAL_LINK.VLID;
        '    ''VIRTUAL_LINK

        '    'For Each record As Dacn1AfdxVirtualLink In vlCollection
        '    '    Dim str As String = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", record.CollectionName, _
        '    '                                      record.Vlid, _
        '    '                                      record.VLMode, _
        '    '                                      record.NumberOfSubVL, _
        '    '                                      record.SubVLID, _
        '    '                                      Convert.ToInt16(record.TxRxEnum), _
        '    '                                      record.Bag, _
        '    '                                      record.Mtu, _
        '    '                                      record.MaxJitter, _
        '    '                                      record.SkewMax, _
        '    '                                      record.SkewDelay, _
        '    '                                      record.PrevSnRange, _
        '    '                                      record.EDEEnabled, _
        '    '                                      record.EDESid, _
        '    '                                      record.EDESkewMax, _
        '    '                                      record.EDEAgeMax, _
        '    '                                      record.EDELagMax)

        '    '    writer.WriteLine(str)


        '    'Next


        '    Dim portCollection As New Dacn1AfdxPortCollection()
        '    portCollection.es.Connection.Name = CELProject.DefaultConnectionName
        '    'portCollection.Query.Where(portCollection.Query.CollectionName = "PW980A" And portCollection.Query.ExportToCsv = True)
        '    portCollection.Query.Where(portCollection.Query.CollectionName = "PW980A")
        '    portCollection.Query.OrderBy(portCollection.Query.CollectionName.Ascending)
        '    portCollection.Query.OrderBy(portCollection.Query.Vlid.Ascending)
        '    portCollection.Query.Load()

        '    '     SELECT DACN1_AFDX_PORT.CollectionName, DACN1_AFDX_PORT.VLID, DACN1_AFDX_PORT.UdpPortDestination, DACN1_AFDX_PORT.UdpPortSource, DACN1_AFDX_PORT.Name, DACN1_AFDX_PORT.TxRx, DACN1_AFDX_PORT.PortMode, DACN1_AFDX_PORT.PortID, DACN1_AFDX_PORT.Period, DACN1_AFDX_PORT.PeriodSkew, DACN1_AFDX_PORT.EndSystemSource, DACN1_AFDX_PORT.PartitionSource, DACN1_AFDX_PORT.EndSystemDestination, DACN1_AFDX_PORT.PartitionDestination, DACN1_AFDX_PORT.SubVLID, DACN1_AFDX_PORT.PortType, DACN1_AFDX_PORT.MessageLength, DACN1_AFDX_PORT.Depth, DACN1_AFDX_PORT.BufferSize
        '    'FROM DACN1_AFDX_PORT
        '    'WHERE (((DACN1_AFDX_PORT.ExportToCsv)=True))
        '    'ORDER BY DACN1_AFDX_PORT.CollectionName, DACN1_AFDX_PORT.VLID, DACN1_AFDX_PORT.UdpPortDestination, DACN1_AFDX_PORT.UdpPortSource, DACN1_AFDX_PORT.PortID;


        '    For Each record As Dacn1AfdxPort In portCollection
        '        Dim str As String = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", DACN1_AFDX_PORT.CollectionName, DACN1_AFDX_PORT.VLID, DACN1_AFDX_PORT.UdpPortDestination, DACN1_AFDX_PORT.UdpPortSource, DACN1_AFDX_PORT.Name, DACN1_AFDX_PORT.TxRx, DACN1_AFDX_PORT.PortMode, DACN1_AFDX_PORT.PortID, DACN1_AFDX_PORT.Period, DACN1_AFDX_PORT.PeriodSkew, DACN1_AFDX_PORT.EndSystemSource, DACN1_AFDX_PORT.PartitionSource, DACN1_AFDX_PORT.EndSystemDestination, DACN1_AFDX_PORT.PartitionDestination, DACN1_AFDX_PORT.SubVLID, DACN1_AFDX_PORT.PortType, DACN1_AFDX_PORT.MessageLength, DACN1_AFDX_PORT.Depth, DACN1_AFDX_PORT.BufferSize)

        '        writer.WriteLine(str)


        '    Next

        '    writer.Close()


        '    '        SELECT DACN1_AFDX_SIGNAL.CollectionName, DACN1_AFDX_SIGNAL.VLID, DACN1_AFDX_SIGNAL.UdpPortSource, DACN1_AFDX_SIGNAL.UdpPortDestination, DACN1_AFDX_SIGNAL.FdsName, DACN1_AFDX_MESSAGE.FdsAddress, DACN1_AFDX_MESSAGE.FdsLength, DACN1_AFDX_MESSAGE.FsAddress, DACN1_AFDX_SIGNAL.SignalAddress, DACN1_AFDX_SIGNAL.ParameterName, DACN1_AFDX_SIGNAL.TxRx, DACN1_AFDX_PORT.PortID, DACN1_AFDX_SIGNAL.StartBit, DACN1_AFDX_SIGNAL.StopBit, DACN1_AFDX_SIGNAL.LSBValue, DACN1_AFDX_SIGNAL.DataType, DACN1_AFDX_SIGNAL.CodingType, DACN1_AFDX_SIGNAL.MinValue, DACN1_AFDX_SIGNAL.MaxValue, DACN1_AFDX_SIGNAL.Length
        '    'FROM DACN1_AFDX_PORT INNER JOIN (DACN1_AFDX_MESSAGE INNER JOIN DACN1_AFDX_SIGNAL ON (DACN1_AFDX_MESSAGE.FdsName = DACN1_AFDX_SIGNAL.FdsName) AND (DACN1_AFDX_MESSAGE.UdpPortDestination = DACN1_AFDX_SIGNAL.UdpPortDestination) AND (DACN1_AFDX_MESSAGE.UdpPortSource = DACN1_AFDX_SIGNAL.UdpPortSource) AND (DACN1_AFDX_MESSAGE.VLID = DACN1_AFDX_SIGNAL.VLID) AND (DACN1_AFDX_MESSAGE.CollectionName = DACN1_AFDX_SIGNAL.CollectionName)) ON (DACN1_AFDX_PORT.UdpPortDestination = DACN1_AFDX_MESSAGE.UdpPortDestination) AND (DACN1_AFDX_PORT.UdpPortSource = DACN1_AFDX_MESSAGE.UdpPortSource) AND (DACN1_AFDX_PORT.VLID = DACN1_AFDX_MESSAGE.VLID) AND (DACN1_AFDX_PORT.CollectionName = DACN1_AFDX_MESSAGE.CollectionName)
        '    'ORDER BY DACN1_AFDX_SIGNAL.CollectionName, DACN1_AFDX_SIGNAL.VLID, DACN1_AFDX_SIGNAL.UdpPortSource, DACN1_AFDX_SIGNAL.UdpPortDestination, DACN1_AFDX_SIGNAL.SignalAddress, DACN1_AFDX_SIGNAL.StartBit;



    End Sub
End Class
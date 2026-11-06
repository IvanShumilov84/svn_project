Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.OleDb
Imports CEL.Library
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Diagnostics



Public Class SpecGeneratorForm

    Private CELProject As StudioDemonstration.CELDAS
    Private Sub SpecGeneratorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.CELProject = StudioDemonstration.CELDAS.Instance
        Me.CELProject.Project.Number = "YY79"
        Me.CELProject.TechnologyName = "CELDAS™ 200"
        Me.CELProject.Project.Name = "CEL Project Source"

        Me.CELProject.Database.Path = "C:\My Projects\CELDAS 200\Workbench\"
        Me.CELProject.Database.DatabaseFileName = "TestSession.mdb"

        Me.CELProject.Database.WorkGroupFileName = "Secured.mdw"
        Me.CELProject.Database.Password = "CEL9994"
        Me.CELProject.Database.UserId = "Administrator"


        ReadEngineList(CELProject, 0)
    End Sub
    Private Sub ReadEngineList(ByVal CELProject As StudioDemonstration.CELDAS, ByVal SelectStringValue As Integer)

        Dim ConnectionString As String
        Dim SelectString As String


        Dim cn As OleDbConnection = Nothing

        Try
            cboEngineList.Items.Clear()
            'Connect to the TestSession Database
            ConnectionString = CELProject.Database.OleDbConnectionString
            If SelectStringValue = 1 Then
                SelectString = "(SELECT DISTINCT Engine_Model FROM HMI_Boolean_Channels) UNION ALL " & _
                               "(SELECT DISTINCT Engine_Model FROM HMI_Numeric_Channels) UNION ALL " & _
                               "(SELECT DISTINCT Engine_Model FROM HMI_Calculated_Value_Boolean) UNION ALL " & _
                               "(SELECT DISTINCT Engine_Model FROM HMI_Calculated_Value_Numeric)"
            Else
                SelectString = "(SELECT DISTINCT Engine_Model FROM HMI_Boolean_Channels) UNION ALL " & _
                                "(SELECT DISTINCT Engine_Model FROM HMI_Numeric_Channels)"
            End If

            Dim dic As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
            If DatabaseHelper.OpenConnection(ConnectionString, cn) Then
                Dim cmd As OleDbCommand = New OleDbCommand(SelectString, cn)
                Dim reader As OleDbDataReader = cmd.ExecuteReader()

                While (reader.Read())
                    Try
                        dic.Add(reader.Item("Engine_Model").ToString(), 1) 'Petite passe passe pour garder 1 objet unique
                        cboEngineList.Items.Add(reader.Item("Engine_Model").ToString())
                    Catch ex As Exception

                    End Try

                End While
                reader.Close()
                DatabaseHelper.CloseConnection(cn)

            End If

        Catch ex As Exception
        Finally
        End Try

    End Sub

    'Private Sub btnSelectDestination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ' Set the file dialog to filter for graphics files.
    '    Me.OpenFileDialog1.Filter = "Database (*.xlsx;)|*.xlsx;|" + "All files (*.*)|*.*"


    '    Me.OpenFileDialog1.Multiselect = False
    '    Me.OpenFileDialog1.Title = "Please Select a File"

    '    Me.OpenFileDialog1.InitialDirectory = "C:\My Projects\CELDAS 200\HMI\GTCP85_129"

    '    Dim dr As DialogResult = Me.OpenFileDialog1.ShowDialog()
    '    If (dr = System.Windows.Forms.DialogResult.OK) Then
    '        ' Read the files
    '        Dim file As String
    '        For Each file In OpenFileDialog1.FileNames

    '            txtDestination.Text = file.ToString()

    '        Next file
    '    End If

    'End Sub

    Private Sub btnSelectSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectSource.Click
        ' Set the file dialog to filter for graphics files.
        Me.OpenFileDialog1.Filter = "Database (*.xlsx;)|*.xlsx;|" + "All files (*.*)|*.*"


        Me.OpenFileDialog1.Multiselect = False
        Me.OpenFileDialog1.Title = "Please Select a File"

        Me.OpenFileDialog1.InitialDirectory = "C:\My Projects\CELDAS 200\HMI\GTCP85_129"

        Dim dr As DialogResult = Me.OpenFileDialog1.ShowDialog()
        If (dr = System.Windows.Forms.DialogResult.OK) Then
            ' Read the files
            Dim file As String
            For Each file In OpenFileDialog1.FileNames

                txtSource.Text = file.ToString()

            Next file
        End If
    End Sub

    Private Sub btnStartProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartProcess.Click
        Dim NewFilename As String = String.Empty
        Dim Filename As String = String.Empty
        Dim ExcelWorkbook As Excel.Workbook = Nothing
        'The following code opens an existing workbook
        Dim WorkbookPath As String = System.IO.Path.GetFullPath(txtSource.Text)
        Try

            ExcelWorkbook = StudioDemonstration.CELDAS.ExcelApplication.Workbooks.Open(WorkbookPath, 0, False, 5, "", "", False, _
                      Excel.XlPlatform.xlWindows, "", True, False, 0, True, False, False)


            'MySheet.UsedRange.Rows.Count

            If Not (ExcelWorkbook Is Nothing) Then
                'The following gets the Worksheets collection
                Dim ExcelSheets As Excel.Sheets = ExcelWorkbook.Worksheets
                Dim CurrentSheet As String = "Sheet1"
                'Dim ExcelWorksheet As Excel.Worksheet = ExcelSheets.Item(CurrentSheet)
                Dim ExcelWorksheet As Excel.Worksheet = ExcelSheets.Item(1)

                Dim RowIndex As Integer = 1
                Dim MaxRowIndex As Integer = ExcelWorksheet.UsedRange.Rows.Count
                Dim ColumnIndex As Integer = 17 'Contient le nom de channel KineticaRT

                Dim SelectString As String

                Dim cn As New OleDbConnection

                If DatabaseHelper.OpenConnection(CELProject.Database.OleDbConnectionString, cn) Then
                    Dim cmd As OleDbCommand = Nothing
                    Dim reader As OleDbDataReader = Nothing

                    Dim channelname As String = String.Empty
                    While (RowIndex < MaxRowIndex)

                        channelname = ""
                        If Not (ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value Is Nothing) Then
                            If ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value.ToString() & "" <> "" Then
                                channelname = ExcelWorksheet.Cells(RowIndex, ColumnIndex).Value.ToString()
                            End If
                        End If

                        If channelname <> "" Then

                            'BOOLEAN CHANNELS
                            SelectString = "SELECT * FROM HMI_Boolean_Channels WHERE Engine_Model = '" & cboEngineList.Text & "' AND Name = '" & channelname & "'"
                            cmd = New OleDbCommand(SelectString, cn)
                            reader = cmd.ExecuteReader()

                            If reader.Read() Then
                                ExcelWorksheet.Cells(RowIndex, 2).Value = FormatInformation(reader, "BOOLEAN")
                            End If
                            reader.Close()


                            'NUMERIC CHANNELS
                            SelectString = "SELECT * FROM HMI_Numeric_Channels WHERE Engine_Model = '" & cboEngineList.Text & "' AND Name = '" & channelname & "'"
                            cmd = New OleDbCommand(SelectString, cn)
                            reader = cmd.ExecuteReader()

                            If reader.Read() Then
                                ExcelWorksheet.Cells(RowIndex, 2).Value = FormatInformation(reader, "NUMERIC")
                            End If
                            reader.Close()

                        End If

                        RowIndex += 1
                    End While
                    DatabaseHelper.CloseConnection(cn)
                End If

                'Sauvegarde le nouveau fichier excel
                NewFilename = Format(Now, "yyyy-MM-dd HH'h'mm'm'ss's'")
                Filename = txtSource.Text.Replace(".xlsx", "") + NewFilename + ".xlsx"

                ExcelWorkbook.SaveAs(Filename)
                ExcelWorkbook.Close()

            End If

        Catch Ex As System.Exception
            System.Diagnostics.Trace.WriteLine("Opening Excel: " + Ex.Message)
            MessageBox.Show(Ex.ToString)
        End Try
    End Sub
    Private Function FormatInformation(ByRef reader As OleDbDataReader, ByVal tablesource As String) As String
        Dim channelinfo As String = String.Empty
        If tablesource = "BOOLEAN" Then
        ElseIf tablesource = "NUMERIC" Then
            If reader.Item("Tooltip") & "" <> "" Then
                channelinfo += "Tooltip: " & reader.Item("Tooltip")
                channelinfo += vbLf
            End If
            Select Case True
                Case reader.Item("Source").ToString() & "" <> "" And reader.Item("Destination").ToString() & "" <> ""
                    channelinfo += "Source: " & reader.Item("Source").ToString()
                    channelinfo += vbLf
                    channelinfo += "Destination: " & reader.Item("Destination").ToString()
                    channelinfo += vbLf

                Case reader.Item("Source") & "" <> ""
                    channelinfo += "Source: " & reader.Item("Source").ToString()
                    channelinfo += vbLf

                Case reader.Item("Destination") & "" <> ""
                    channelinfo += "Destination: " & reader.Item("Destination").ToString()
                    channelinfo += vbLf

                Case reader.Item("Expression") & "" <> ""
                    channelinfo += "Expression: " & reader.Item("Expression").ToString()
                    channelinfo += vbLf
            End Select
            If reader.Item("DecimalDigits") <> 0 Then
                channelinfo += "Significative Numbers: " & reader.Item("DecimalDigits")
                channelinfo += vbLf
            End If
            'a changer pour Scale: from ... to ....
            If reader.Item("Minimum") & "" <> "" Then
                channelinfo += "Minimum: " & reader.Item("Minimum")
                channelinfo += vbLf
            End If
            If reader.Item("Maximum") & "" <> "" Then
                If reader.Item("Maximum") <> 0 Then
                    channelinfo += "Maximum: " & reader.Item("Maximum")
                    channelinfo += vbLf
                End If
            End If
            If reader.Item("Threshold_HighHighSource") & "" <> "" Then
                channelinfo += "Field color is " & reader.Item("Threshold_HighHighColor")
                channelinfo += " when value exceeds " & reader.Item("Threshold_HighHighSource")
                channelinfo += reader.Item("Units")
                channelinfo += vbLf
            ElseIf reader.Item("Threshold_HighHighColor") & "" <> "" Then
                channelinfo += "Field color is " & reader.Item("Threshold_HighHighColor")
                channelinfo += " when value exceeds " & reader.Item("Threshold_HighHigh")
                channelinfo += reader.Item("Units")
                channelinfo += vbLf
            End If
            If reader.Item("Threshold_HighColor") & "" <> "" Then
                channelinfo += "Field color is " & reader.Item("Threshold_HighColor")
                channelinfo += " when value exceeds " & reader.Item("Threshold_High")
                channelinfo += reader.Item("Units")
                channelinfo += vbLf
            End If
            If reader.Item("Threshold_NormalColor") & "" <> "" Then
                channelinfo += "Normal color of this field is " & reader.Item("Threshold_NormalColor")
                'channelinfo += " when value is lower than " & reader.Item("Threshold_Normal")
                channelinfo += vbLf
            End If
            If reader.Item("Threshold_LowColor") & "" <> "" Then
                channelinfo += "Field color is " & reader.Item("Threshold_LowColor")
                channelinfo += " when value is lower than " & reader.Item("Threshold_Low")
                channelinfo += reader.Item("Units")
                channelinfo += vbLf
            End If
            If reader.Item("Threshold_LowLowColor") & "" <> "" Then
                channelinfo += "Field color is " & reader.Item("Threshold_LowLowColor")
                channelinfo += " when value is lower than " & reader.Item("Threshold_LowLow")
                channelinfo += reader.Item("Units")
                channelinfo += vbLf
            End If
            If reader.Item("High_Trigger_Enabled") = True Then
                channelinfo += "Event archiver is activated when value exceeds " & reader.Item("High_Trigger")
                channelinfo += reader.Item("Units")
                channelinfo += vbLf
            End If

        Else
            Debug.Assert(False)
        End If

        Return channelinfo
    End Function
End Class